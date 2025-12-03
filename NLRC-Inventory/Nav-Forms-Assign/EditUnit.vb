Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports System.Drawing
Imports MySql.Data.MySqlClient

Public Class EditUnit

    Private mdl As New model()
    Private unitId As Integer
    Private originalAssignList As DataTable

    ' Tracking
    Private originalUnitName As String
    Private originalAssignedId As Integer
    Private selectedAssignedId As Integer? = Nothing

    Private currentDevices As New List(Of Integer)
    Private removedDevices As New List(Of Integer)
    Private addedDevices As New List(Of Integer)
    Public Event UnitSaved()

    Private originalDeviceList As DataTable          ' for devicecb filtering
    Private isFiltering As Boolean = False           ' shared flag for both combos

    Private deviceCategory As New Dictionary(Of Integer, Integer) ' device_id -> category_pointer

    ' TEMP SPECS STORAGE
    Private editedSpecs As New Dictionary(Of Integer, Dictionary(Of String, String))()
    Private currentSpecDeviceId As Integer = -1

    ' Display name for confirmation
    Private deviceDisplayNames As New Dictionary(Of Integer, String)

    ' Quantity + Specs storage
    Private deviceQuantities As New Dictionary(Of Integer, Integer)
    Private deviceFullSpecs As New Dictionary(Of Integer, String)
    Private deviceBaseDisplay As New Dictionary(Of Integer, String)  ' Category - Brand - Model

    Private specsTable As New TableLayoutPanel With {
            .AutoSize = True,
            .ColumnCount = 2,
            .RowCount = 0,
            .Dock = DockStyle.Fill,
            .Padding = New Padding(10)
        }

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        If Me.DesignMode Then Return

        originalSize = Me.Size
        originalBounds.Clear()
        StoreOriginalBounds(Me)
        layoutInitialized = True
    End Sub

    Private Sub StoreOriginalBounds(parent As Control)
        For Each ctrl As Control In parent.Controls
            If Not originalBounds.ContainsKey(ctrl) Then
                originalBounds.Add(ctrl, ctrl.Bounds)
            End If

            If ctrl.HasChildren Then
                StoreOriginalBounds(ctrl)
            End If
        Next
    End Sub

    Private Sub ApplyScale(scaleX As Single, scaleY As Single)
        Me.SuspendLayout()

        For Each kvp As KeyValuePair(Of Control, Rectangle) In originalBounds
            Dim ctrl As Control = kvp.Key
            If ctrl Is Nothing OrElse ctrl.IsDisposed Then Continue For

            Dim r As Rectangle = kvp.Value
            ctrl.Bounds = New Rectangle(
                        CInt(r.X * scaleX),
                        CInt(r.Y * scaleY),
                        CInt(r.Width * scaleX),
                        CInt(r.Height * scaleY)
                    )

            If ctrl.Font IsNot Nothing Then
                Dim f As Font = ctrl.Font
                Dim newSize As Single = f.SizeInPoints * Math.Min(scaleX, scaleY)
                If newSize > 4 Then
                    ctrl.Font = New Font(f.FontFamily, newSize, f.Style)
                End If
            End If
        Next

        Me.ResumeLayout()
    End Sub


    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        If Not layoutInitialized Then Return
        If originalSize.Width = 0 OrElse originalSize.Height = 0 Then Return

        Dim scaleX As Single = CSng(Me.Width) / originalSize.Width
        Dim scaleY As Single = CSng(Me.Height) / originalSize.Height

        ApplyScale(scaleX, scaleY)
    End Sub

    Private Sub EditUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        InitializeLayoutScaling()

        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True

        AddHandler devicecb.TextChanged, AddressOf FilterDeviceCombo
        AddHandler assigncb.TextChanged, AddressOf FilterAssignCombo

        ' 🔽 ADD THESE
        AddHandler deviceflowpnl.Resize, AddressOf DeviceFlowPanel_Resize
        AddHandler specsflowpnl.Resize, AddressOf SpecsFlowPanel_Resize

        ' run once so initial layout is stretched
        DeviceFlowPanel_Resize(Nothing, EventArgs.Empty)
        SpecsFlowPanel_Resize(Nothing, EventArgs.Empty)
    End Sub

    ' 🔁 Stretch all spec textboxes when the specs panel resizes
    Private Sub SpecsFlowPanel_Resize(sender As Object, e As EventArgs)
        Dim available As Integer = specsflowpnl.ClientSize.Width - 200  ' space for labels + padding
        If available < 100 Then available = 100

        For Each ctl As Control In specsTable.Controls
            Dim tb As TextBox = TryCast(ctl, TextBox)
            If tb IsNot Nothing Then
                tb.Width = available
            End If
        Next
    End Sub

    ' 🔁 Stretch all device panels when the device list panel resizes
    Private Sub DeviceFlowPanel_Resize(sender As Object, e As EventArgs)
        Dim w As Integer = GetDeviceRowWidth()

        For Each row As Control In deviceflowpnl.Controls
            row.Width = w
        Next
    End Sub




    ' ========================
    ' LOAD UNIT
    ' ========================
    Public Sub LoadUnit(unitId As Integer, unitName As String, assignedToId As Integer, devices As DataTable)
        Me.unitId = unitId
        Me.originalUnitName = unitName
        Me.originalAssignedId = assignedToId

        LoadAssignments()
        LoadDeviceCombo()

        unitnametxt.Text = unitName

        Dim row = originalAssignList.AsEnumerable().
            FirstOrDefault(Function(r) r.Field(Of Integer)("user_id") = assignedToId)
        If row IsNot Nothing Then assigntxt.Text = row("Full name").ToString()

        ' reset UI
        deviceflowpnl.Controls.Clear()
        specsflowpnl.Controls.Clear()
        specsTable.Controls.Clear()
        specsflowpnl.Controls.Add(specsTable)

        ' reset tracking
        currentDevices.Clear()
        removedDevices.Clear()
        addedDevices.Clear()
        editedSpecs.Clear()
        deviceDisplayNames.Clear()
        deviceFullSpecs.Clear()

        ' 👇 Existing unit devices
        For Each dr As DataRow In devices.Rows
            Dim devId As Integer = CInt(dr("DeviceID"))

            ' ✅ store category_pointer from GetDeviceSpecsByUnitPointer
            If devices.Columns.Contains("category_pointer") AndAlso
               Not IsDBNull(dr("category_pointer")) Then

                If Not deviceCategory.ContainsKey(devId) Then
                    deviceCategory(devId) = CInt(dr("category_pointer"))
                End If
            End If

            ' Button text: Category - Brand - Model
            Dim baseText As String = ExtractBaseText(dr("DeviceAndSpecs").ToString())

            ' Build full specs: NSOC Name + Property No + ONLY actual specs
            Dim specsParts As New List(Of String)

            If dr.Table.Columns.Contains("nsoc_name") AndAlso
                Not IsDBNull(dr("nsoc_name")) AndAlso dr("nsoc_name").ToString().Trim() <> "" Then
                specsParts.Add("NSOC Name:" & dr("nsoc_name").ToString().Trim())
            End If

            If dr.Table.Columns.Contains("property_number") AndAlso
                Not IsDBNull(dr("property_number")) AndAlso dr("property_number").ToString().Trim() <> "" Then
                specsParts.Add("Property No:" & dr("property_number").ToString().Trim())
            End If

            ' DeviceAndSpecs now ends with either s.specs OR 'No specs available'
            Dim rawSpecs = dr("DeviceAndSpecs").ToString().Trim()
            Dim onlySpecs = ExtractOnlySpecs(rawSpecs)

            ' if onlySpecs = "No specs available" it will NOT create any key in ParseSpecs
            If onlySpecs <> "" AndAlso Not onlySpecs.Equals("No specs available", StringComparison.OrdinalIgnoreCase) Then
                specsParts.Add(onlySpecs)
            End If

            Dim fullSpecs As String = String.Join(";", specsParts)

            deviceDisplayNames(devId) = baseText
            deviceFullSpecs(devId) = fullSpecs
            currentDevices.Add(devId)

            If Not editedSpecs.ContainsKey(devId) Then
                editedSpecs(devId) = ParseSpecs(fullSpecs)
            End If

            AddDeviceToPanel(devId, baseText, fullSpecs)

            ' 🔁 ensure ALL rows are stretched (including the ones already in the panel)
            DeviceFlowPanel_Resize(Nothing, EventArgs.Empty)
        Next
    End Sub


    Private Function ExtractBaseText(fullText As String) As String
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length < 3 Then Return fullText.Trim()
        Return $"{parts(0).Trim()} - {parts(1).Trim()} - {parts(2).Trim()}"
    End Function

    Private Function ExtractOnlySpecs(fullText As String) As String
        If String.IsNullOrWhiteSpace(fullText) Then Return String.Empty
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length <= 3 Then Return String.Empty
        Return String.Join(" - ", parts.Skip(3)).Trim()
    End Function

    ' ========================
    ' LOAD DEVICE COMBO
    ' ========================
    Private Sub LoadDeviceCombo()
        Dim dt As DataTable = mdl.GetDevicesForUnits1(Me.unitId)
        deviceQuantities.Clear()
        deviceFullSpecs.Clear()
        deviceBaseDisplay.Clear()
        deviceCategory.Clear()

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("No devices found in the database.", "Notice",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not dt.Columns.Contains("HiddenSpecs") Then
            dt.Columns.Add("HiddenSpecs", GetType(String))
        End If

        For Each row As DataRow In dt.Rows
            Dim id = CInt(row("device_id"))
            deviceQuantities(id) = CInt(row("qty"))

            Dim base = $"{row("category_name")} - {row("brand_name")} - {row("model")}"
            Dim hiddenSpecs As New List(Of String)

            If Not IsDBNull(row("nsoc_name")) AndAlso row("nsoc_name").ToString().Trim() <> "" Then
                hiddenSpecs.Add("NSOC Name:" & row("nsoc_name").ToString())
            End If
            If Not IsDBNull(row("property_number")) AndAlso row("property_number").ToString().Trim() <> "" Then
                hiddenSpecs.Add("Property No:" & row("property_number").ToString())
            End If
            If Not IsDBNull(row("actual_specs")) AndAlso row("actual_specs").ToString().Trim() <> "" Then
                hiddenSpecs.Add(row("actual_specs").ToString())
            End If

            row("HiddenSpecs") = String.Join(";", hiddenSpecs)
            deviceFullSpecs(id) = row("HiddenSpecs").ToString()
            deviceBaseDisplay(id) = base

            ' ✅ category_pointer from GetDevicesForUnits1
            If dt.Columns.Contains("category_pointer") AndAlso
               Not IsDBNull(row("category_pointer")) Then

                deviceCategory(id) = CInt(row("category_pointer"))
            End If
        Next

        devicecb.DataSource = dt
        devicecb.DisplayMember = "DeviceDisplay"
        devicecb.ValueMember = "device_id"
        devicecb.SelectedIndex = -1
        devicecb.Text = "Select Device"

        devicecb.DropDownStyle = ComboBoxStyle.DropDown
        devicecb.IntegralHeight = False
        devicecb.DropDownHeight = 350

        originalDeviceList = dt.Copy()
    End Sub


    Private Sub UpdateComboList()
        Dim dt As New DataTable()
        dt.Columns.Add("device_id", GetType(Integer))
        dt.Columns.Add("DeviceDisplay", GetType(String))
        dt.Columns.Add("DeviceFullSpecs", GetType(String))
        dt.Columns.Add("HiddenSpecs", GetType(String))

        For Each kv In deviceBaseDisplay
            Dim id = kv.Key
            Dim baseText = kv.Value
            Dim qty = If(deviceQuantities.ContainsKey(id), deviceQuantities(id), 0)
            Dim fullSpecs = If(deviceFullSpecs.ContainsKey(id), deviceFullSpecs(id), baseText)
            dt.Rows.Add(id, $"{baseText} ({qty})", fullSpecs, fullSpecs)
        Next

        devicecb.DataSource = dt
        devicecb.DisplayMember = "DeviceDisplay"
        devicecb.ValueMember = "device_id"

        originalDeviceList = dt.Copy()

        devicecb.DropDownStyle = ComboBoxStyle.DropDown
        devicecb.IntegralHeight = False
        devicecb.DropDownHeight = 350

        DeviceFlowPanel_Resize(Nothing, EventArgs.Empty)

    End Sub

    Private Sub adddevicebtn_Click(sender As Object, e As EventArgs) Handles adddevicebtn.Click
        If devicecb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a device first.")
            Return
        End If

        Dim devId As Integer = CInt(devicecb.SelectedValue)
        If currentDevices.Contains(devId) Then
            MessageBox.Show("Device already assigned to this unit.")
            Return
        End If

        If Not deviceQuantities.ContainsKey(devId) OrElse deviceQuantities(devId) <= 0 Then
            MessageBox.Show("No more available stock for this device.")
            Return
        End If

        Dim baseText As String = If(deviceBaseDisplay.ContainsKey(devId), deviceBaseDisplay(devId), "Unknown Device")

        Dim drv As DataRowView = CType(devicecb.SelectedItem, DataRowView)
        Dim specsString As String = If(drv IsNot Nothing AndAlso drv.Row.Table.Columns.Contains("HiddenSpecs"),
                                    CleanSpecs(drv("HiddenSpecs").ToString()), "")

        addedDevices.Add(devId)
        currentDevices.Add(devId)
        deviceQuantities(devId) -= 1
        deviceDisplayNames(devId) = baseText
        deviceFullSpecs(devId) = specsString

        If Not editedSpecs.ContainsKey(devId) Then
            editedSpecs(devId) = ParseSpecs(specsString)
        End If

        UpdateComboList()
        AddDeviceToPanel(devId, baseText, specsString)

        ' ✅ make the new row match deviceflowpnl width
        DeviceFlowPanel_Resize(Nothing, EventArgs.Empty)
    End Sub


    Private Sub AddDeviceToPanel(deviceId As Integer, displayText As String, fullSpecs As String)
        Dim container As New Panel With {
                .Height = 45,
                .Width = 470,
                .Margin = New Padding(5)
            }

        Dim btn As New Button With {
                .Text = displayText,
                .Tag = fullSpecs,
                .Dock = DockStyle.Fill,
                .Height = 40,
                .BackColor = Color.LightGray,
                .TextAlign = ContentAlignment.MiddleLeft,
                .AutoEllipsis = True
            }

        AddHandler btn.Click, Sub() ShowSpecs(fullSpecs, deviceId)

        Dim removeBtn As New Button With {
                .Text = "Remove",
                .Width = 120,
                .Dock = DockStyle.Right,
                .BackColor = Color.LightCoral,
                .ForeColor = Color.White
            }

        AddHandler removeBtn.Click,
                Sub()
                    Dim result = MessageBox.Show(
                            $"Are you sure you want to remove {displayText}?",
                            "Confirm Removal",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning)
                    If result = DialogResult.No Then Exit Sub

                    deviceflowpnl.Controls.Remove(container)
                    currentDevices.Remove(deviceId)
                    editedSpecs.Remove(deviceId)
                    removedDevices.Add(deviceId)
                    If deviceQuantities.ContainsKey(deviceId) Then deviceQuantities(deviceId) += 1
                    UpdateComboList()
                End Sub

        container.Controls.Add(removeBtn)
        container.Controls.Add(btn)
        deviceflowpnl.Controls.Add(container)
    End Sub

    Private Sub ShowSpecs(fullSpecs As String, deviceId As Integer)
        specsTable.Controls.Clear()
        specsTable.RowCount = 0
        currentSpecDeviceId = deviceId

        ' 1) Get current specs dictionary
        Dim dict As Dictionary(Of String, String) = Nothing

        If editedSpecs.ContainsKey(deviceId) Then
            dict = editedSpecs(deviceId)
        Else
            dict = ParseSpecs(fullSpecs)
        End If

        ' 2) Separate NSOC/Property from "real" specs
        Dim nsocProp As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Dim hasRealSpecs As Boolean = False

        If dict IsNot Nothing Then
            For Each kv In dict.ToList()
                Dim key As String = kv.Key.Trim()

                If key.Equals("NSOC Name", StringComparison.OrdinalIgnoreCase) _
                   OrElse key.Equals("Property No", StringComparison.OrdinalIgnoreCase) Then

                    ' keep NSOC / Property values
                    nsocProp(key) = kv.Value
                Else
                    ' any other key means we already have real specs
                    hasRealSpecs = True
                End If
            Next
        End If

        ' 3) If there are NO REAL SPECS at all (either empty, or only NSOC/Property),
        '    then load template fields from inv_category_specs
        If Not hasRealSpecs Then
            Try
                Dim specsDt As DataTable = Nothing

                If deviceCategory.ContainsKey(deviceId) Then
                    Dim catId As Integer = deviceCategory(deviceId)
                    specsDt = mdl.GetCategorySpecs(catId)
                End If

                Dim newDict As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

                ' 3a) keep NSOC / Property fields (if any)
                For Each kv In nsocProp
                    newDict(kv.Key) = kv.Value
                Next

                ' 3b) add template fields from inv_category_specs (empty values)
                If specsDt IsNot Nothing AndAlso specsDt.Rows.Count > 0 Then
                    For Each r As DataRow In specsDt.Rows
                        Dim specName As String = If(r("specs_name") IsNot DBNull.Value,
                                                    r("specs_name").ToString().Trim(),
                                                    "")
                        If specName <> "" AndAlso Not newDict.ContainsKey(specName) Then
                            newDict(specName) = ""   ' empty, ready for user input
                        End If
                    Next
                End If

                dict = newDict
                editedSpecs(deviceId) = dict

            Catch ex As Exception
                MessageBox.Show("Error loading category specs: " & ex.Message,
                                "Specs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ' 4) Still nothing? show info label
        If dict Is Nothing OrElse dict.Count = 0 Then
            Dim lblInfo As New Label With {
                .Text = "No specs defined for this device/category.",
                .AutoSize = True,
                .Font = New Font("Segoe UI", 9, FontStyle.Italic)
            }
            specsTable.Controls.Add(lblInfo, 0, 0)
            specsTable.SetColumnSpan(lblInfo, 2)
            Return
        End If

        ' 5) Build the UI controls from dict
        For Each kvp In dict
            specsTable.RowCount += 1

            Dim lbl As New Label With {
                .Text = kvp.Key & ":",
                .Width = 150,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            }
            specsTable.Controls.Add(lbl, 0, specsTable.RowCount - 1)

            Dim tb As New TextBox With {
                .Text = kvp.Value,
                .Width = 350,
                .Height = 25,
                .Font = New Font("Segoe UI", 9),
                .Multiline = False,
                .Dock = DockStyle.None
            }
            specsTable.Controls.Add(tb, 1, specsTable.RowCount - 1)
        Next
    End Sub




    Private Function NormalizeSpecs(raw As String) As String
        If String.IsNullOrWhiteSpace(raw) Then Return String.Empty
        Dim s As String = raw.Trim()
        s = System.Text.RegularExpressions.Regex.Replace(s, ",\s*", "; ")
        s = System.Text.RegularExpressions.Regex.Replace(s, ";\s*;", ";")
        s = s.Replace(" ;", ";").Trim()
        If s.EndsWith(";") Then s = s.TrimEnd(";"c).Trim()
        Return s
    End Function

    Private Function ParseSpecs(fullSpecs As String) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)
        If String.IsNullOrWhiteSpace(fullSpecs) Then Return dict

        Dim fallback = NormalizeSpecs(fullSpecs)
        If String.IsNullOrWhiteSpace(fallback) Then Return dict

        Dim fallbackItems = fallback.Split(";"c)
        For Each it As String In fallbackItems
            Dim p = it.Split(":"c)
            If p.Length >= 2 Then
                Dim key = p(0).Trim()
                Dim val = String.Join(":", p.Skip(1)).Trim()
                If Not dict.ContainsKey(key) Then dict(key) = val
            End If
        Next
        Return dict
    End Function

    Private Sub savespecsbtn_Click(sender As Object, e As EventArgs) Handles savespecsbtn.Click
        If currentSpecDeviceId = -1 Then
            MessageBox.Show("Select a device first.")
            Return
        End If

        Dim currentDict As New Dictionary(Of String, String)
        For i = 0 To specsTable.RowCount - 1
            Dim lbl = TryCast(specsTable.GetControlFromPosition(0, i), Label)
            Dim txt = TryCast(specsTable.GetControlFromPosition(1, i), TextBox)
            If lbl IsNot Nothing AndAlso txt IsNot Nothing Then
                currentDict(lbl.Text.Replace(":", "").Trim) = txt.Text.Trim()
            End If
        Next

        Dim originalDict As Dictionary(Of String, String) =
                If(editedSpecs.ContainsKey(currentSpecDeviceId),
                   editedSpecs(currentSpecDeviceId),
                   New Dictionary(Of String, String))

        Dim hasChanges As Boolean = False
        For Each kvp In currentDict
            If Not originalDict.ContainsKey(kvp.Key) OrElse originalDict(kvp.Key).Trim() <> kvp.Value.Trim() Then
                hasChanges = True
                Exit For
            End If
        Next

        If hasChanges Then
            editedSpecs(currentSpecDeviceId) = currentDict
            MessageBox.Show("Specs saved temporarily!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("No changes detected. Specs not updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Dim newUnitName As String = unitnametxt.Text.Trim()

        If String.IsNullOrWhiteSpace(newUnitName) Then
            MessageBox.Show("Unit name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            unitnametxt.Focus()
            Return
        End If

        If Not newUnitName.Equals(originalUnitName, StringComparison.OrdinalIgnoreCase) Then
            If mdl.IsUnitNameExists(newUnitName) Then
                MessageBox.Show($"The unit name '{newUnitName}' already exists in the database.",
                                    "Duplicate Unit",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning)
                unitnametxt.Focus()
                unitnametxt.SelectAll()
                Return
            End If
        End If

        Dim newAssignedId As Integer = If(selectedAssignedId.HasValue, selectedAssignedId.Value, originalAssignedId)
        Dim remarksText = remarkstxt.Text.Trim()
        Dim sb As New StringBuilder()
        sb.AppendLine("You are about to save the following changes:" & vbCrLf)

        If newUnitName <> originalUnitName Then sb.AppendLine($"- Unit Name: {originalUnitName} → {newUnitName}")
        If newAssignedId <> originalAssignedId Then sb.AppendLine($"- Assigned Personnel: {GetNameById(originalAssignedId)} → {GetNameById(newAssignedId)}")

        If removedDevices.Any() Then
            sb.AppendLine("- Removed Devices:")
            For Each d In removedDevices
                sb.AppendLine("  • " & deviceDisplayNames(d))
            Next
        End If

        If addedDevices.Any() Then
            sb.AppendLine("- Added Devices:")
            For Each d In addedDevices
                sb.AppendLine("  • " & deviceDisplayNames(d))
            Next
        End If

        Dim editedDevicesWithChanges = editedSpecs.
                Where(Function(kvp)
                          Dim originalDict = ParseSpecs(deviceFullSpecs(kvp.Key))
                          Return Not DictionariesAreEqual(originalDict, kvp.Value)
                      End Function).ToList()

        If editedDevicesWithChanges.Any() Then
            sb.AppendLine("- Edited Specs:")
            For Each kvp In editedDevicesWithChanges
                sb.AppendLine("  • " & deviceDisplayNames(kvp.Key))
                For Each specKvp In kvp.Value
                    sb.AppendLine($"       {specKvp.Key}: {specKvp.Value}")
                Next
            Next
        End If

        sb.AppendLine(vbCrLf & "Proceed?")
        If MessageBox.Show(sb.ToString(), "Confirm Save",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        Dim userId As Integer = Session.LoggedInUserPointer

        For Each kvp In editedDevicesWithChanges
            Dim deviceId As Integer = kvp.Key
            Dim newDict As Dictionary(Of String, String) = kvp.Value

            Dim originalDict As Dictionary(Of String, String) =
                    ParseSpecs(If(deviceFullSpecs.ContainsKey(deviceId), deviceFullSpecs(deviceId), ""))

            Dim allKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
            For Each k In originalDict.Keys
                allKeys.Add(k)
            Next
            For Each k In newDict.Keys
                allKeys.Add(k)
            Next

            Dim oldLines As New List(Of String)()
            Dim newLines As New List(Of String)()

            For Each key In allKeys
                Dim oldVal As String = If(originalDict.ContainsKey(key), originalDict(key), "").Trim()
                Dim newVal As String = If(newDict.ContainsKey(key), newDict(key), "").Trim()

                If oldVal <> newVal Then
                    If oldVal <> "" Then oldLines.Add($"{key}: {oldVal}")
                    If newVal <> "" Then newLines.Add($"{key}: {newVal}")
                End If
            Next

            Dim oldDisplay As String = String.Join("; ", oldLines)
            Dim newDisplay As String = String.Join("; ", newLines)

            mdl.InsertDeviceHistory(
                    deviceId,
                    "Specs updated via EditUnit",
                    oldDisplay,
                    newDisplay,
                    userId
                )

            deviceFullSpecs(deviceId) = SpecsDictToString(newDict)
        Next

        For Each devId In addedDevices
            mdl.InsertDeviceHistory(
                    devId,
                    $"Assigned to unit '{newUnitName}' via EditUnit",
                    "",
                    newUnitName,
                    userId
                )
        Next

        For Each devId In removedDevices
            mdl.InsertDeviceHistory(
                    devId,
                    $"Removed from unit '{originalUnitName}' via EditUnit",
                    originalUnitName,
                    "",
                    userId
                )
        Next

        mdl.SaveUnitChanges(
        unitId,
        newAssignedId,
        removedDevices,
        addedDevices,
        editedDevicesWithChanges.ToDictionary(Function(k) k.Key, Function(k) k.Value),
        newUnitName,
        remarksText,
        userId
    )
        ' give feedback to parent form
        RaiseEvent UnitSaved()

        MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function DictionariesAreEqual(dict1 As Dictionary(Of String, String),
                                      dict2 As Dictionary(Of String, String)) As Boolean
        ' Treat:
        '   - missing key == empty string
        '   - auto-created empty fields as "no change"
        Dim allKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each k In dict1.Keys
            allKeys.Add(k)
        Next
        For Each k In dict2.Keys
            allKeys.Add(k)
        Next

        For Each key In allKeys
            Dim v1 As String = ""
            Dim v2 As String = ""

            If dict1 IsNot Nothing AndAlso dict1.ContainsKey(key) Then
                v1 = If(dict1(key), "").Trim()
            End If
            If dict2 IsNot Nothing AndAlso dict2.ContainsKey(key) Then
                v2 = If(dict2(key), "").Trim()
            End If

            ' If both old and new are empty/missing → no real difference
            If v1 = "" AndAlso v2 = "" Then
                Continue For
            End If

            ' Real change
            If Not String.Equals(v1, v2, StringComparison.Ordinal) Then
                Return False
            End If
        Next

        Return True
    End Function


    Private Sub LoadAssignments()
        originalAssignList = mdl.GetAssignments()

        If originalAssignList Is Nothing OrElse originalAssignList.Rows.Count = 0 Then
            MessageBox.Show("No users found in the database.", "Notice",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        assigncb.DataSource = originalAssignList.Copy()
        assigncb.DisplayMember = "Full name"
        assigncb.ValueMember = "user_id"
        assigncb.SelectedIndex = -1

        assigncb.DropDownStyle = ComboBoxStyle.DropDown
        assigncb.IntegralHeight = False
        assigncb.DropDownHeight = 350
    End Sub

    Private Function GetNameById(userId As Integer) As String
        Dim row = originalAssignList.AsEnumerable().
                FirstOrDefault(Function(r) r.Field(Of Integer)("user_id") = userId)
        If row IsNot Nothing Then Return row("Full name").ToString()
        Return ""
    End Function


    Private Sub assignbtn_Click(sender As Object, e As EventArgs) Handles assignbtn.Click
        If assigncb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a personnel first.")
            Return
        End If

        assigntxt.Text = assigncb.Text
        selectedAssignedId = CInt(assigncb.SelectedValue)
    End Sub

    Private Function CleanSpecs(rawSpecs As String) As String
        If String.IsNullOrWhiteSpace(rawSpecs) Then Return ""

        Dim cleaned As String = System.Text.RegularExpressions.Regex.Replace(rawSpecs,
                "(?i)\s*NSOC\s*[:=][^;]+", "")
        cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned,
                "(?i)\s*Property\s*[:=][^;]+", "")

        cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, ";;+", ";")
        cleaned = cleaned.Trim(" "c, ";"c)

        Return cleaned
    End Function

    Private Sub FilterDeviceCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalDeviceList,
                            "DeviceDisplay", "device_id", AddressOf FilterDeviceCombo)
    End Sub

    Private Sub FilterAssignCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalAssignList,
                            "Full name", "user_id", AddressOf FilterAssignCombo)
    End Sub

    Private Sub SafeComboFilter(cb As ComboBox, source As DataTable,
                                    displayCol As String, valueCol As String,
                                    handler As EventHandler)

        If isFiltering Then Exit Sub
        isFiltering = True

        Try
            If Not cb.Focused OrElse source Is Nothing OrElse source.Rows.Count = 0 Then Exit Sub

            Dim searchText As String = cb.Text.Trim().ToLower()
            Dim filtered As DataTable

            If String.IsNullOrWhiteSpace(searchText) Then
                filtered = source.Copy()
            Else
                Dim rows = source.AsEnumerable().
                        Where(Function(r) r.Field(Of String)(displayCol).ToLower().Contains(searchText))
                filtered = If(rows.Any(), rows.CopyToDataTable(), Nothing)
            End If

            Dim currentText As String = cb.Text
            Dim selStart As Integer = cb.SelectionStart

            RemoveHandler cb.TextChanged, handler
            cb.BeginUpdate()

            If filtered IsNot Nothing Then
                If Not filtered.Columns.Contains(valueCol) Then
                    filtered.Columns.Add(valueCol, GetType(Integer))
                End If
                cb.DataSource = filtered
            Else
                Dim noResult As New DataTable()
                noResult.Columns.Add(displayCol, GetType(String))
                noResult.Columns.Add(valueCol, GetType(Integer))
                noResult.Rows.Add("No results found", -1)
                cb.DataSource = noResult
            End If

            cb.DisplayMember = displayCol
            cb.ValueMember = valueCol
            cb.EndUpdate()

            cb.DroppedDown = True
            Cursor.Current = Cursors.IBeam
            cb.Text = currentText
            cb.SelectionStart = Math.Min(selStart, cb.Text.Length)
            cb.SelectionLength = 0

            Application.DoEvents()
            cb.DroppedDown = True

            AddHandler cb.TextChanged, handler
        Finally
            isFiltering = False
        End Try
    End Sub

    Private Function SpecsDictToString(specs As Dictionary(Of String, String)) As String
        If specs Is Nothing OrElse specs.Count = 0 Then Return ""
        Dim parts As New List(Of String)
        For Each kvp In specs
            parts.Add(kvp.Key & ": " & kvp.Value)
        Next
        Return String.Join("; ", parts)
    End Function

    ' --- width of one device row inside deviceflowpnl ---
    Private Function GetDeviceRowWidth() As Integer
        ' inner width of the flow panel minus its own padding
        Dim inner As Integer = deviceflowpnl.ClientSize.Width - deviceflowpnl.Padding.Horizontal

        ' each row has Margin(5) left + right = 10 total
        Dim w As Integer = inner - 10
        If w < 100 Then w = 100
        Return w
    End Function

    Private Sub Panel2_Paint(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel = TryCast(Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

End Class
