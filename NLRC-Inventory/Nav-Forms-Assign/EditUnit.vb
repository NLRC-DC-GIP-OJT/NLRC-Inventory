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

    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
    ' ========================
    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        If Me.DesignMode Then Return   ' avoid running in designer

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

            ' Optional: scale fonts a bit
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

    ' Hook into Load to init scaling
    Private Sub EditUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill          ' fill parent panel
        InitializeLayoutScaling()         ' capture designer layout as base
    End Sub

    ' ===============================================================
    ' LOAD UNIT
    ' ===============================================================
    Public Sub LoadUnit(unitId As Integer, unitName As String, assignedToId As Integer, devices As DataTable)

        Me.unitId = unitId
        Me.originalUnitName = unitName
        Me.originalAssignedId = assignedToId

        LoadAssignments()
        LoadDeviceCombo()

        unitnametxt.Text = unitName

        Dim row = originalAssignList.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Integer)("user_id") = assignedToId)
        If row IsNot Nothing Then assigntxt.Text = row("Full name").ToString()

        deviceflowpnl.Controls.Clear()
        specsflowpnl.Controls.Clear()

        specsTable.Controls.Clear()
        specsflowpnl.Controls.Add(specsTable)

        currentDevices.Clear()
        removedDevices.Clear()
        addedDevices.Clear()
        editedSpecs.Clear()
        deviceDisplayNames.Clear()

        ' ================================================
        ' devices DataTable HAS: UnitID, DeviceID, DeviceAndSpecs
        ' We fetch full info per DeviceID using mdl.GetDeviceDetails
        ' ================================================
        For Each dr As DataRow In devices.Rows

            Dim devId As Integer = CInt(dr("DeviceID"))
            Dim details As DataRow = mdl.GetDeviceDetails(devId)

            Dim category As String = ""
            Dim brand As String = ""
            Dim model As String = ""
            Dim nsoc As String = ""
            Dim propNo As String = ""
            Dim specsOnly As String = ""

            If details IsNot Nothing Then
                category = details("category_name").ToString()
                brand = details("brand_name").ToString()
                model = details("model").ToString()
                nsoc = details("nsoc_name").ToString()
                propNo = details("property_number").ToString()
                specsOnly = details("device_specs").ToString()
            Else
                ' Fallback: parse original DeviceAndSpecs string
                Dim rawFullText As String = dr("DeviceAndSpecs").ToString()
                Dim baseTextTmp As String = ExtractBaseText(rawFullText)
                specsOnly = ExtractOnlySpecs(rawFullText)

                Dim parts = baseTextTmp.Split(New String() {" - "}, StringSplitOptions.None)
                If parts.Length >= 3 Then
                    category = parts(0).Trim()
                    brand = parts(1).Trim()
                    model = parts(2).Trim()
                Else
                    brand = baseTextTmp
                End If
            End If

            Dim baseText As String = $"{category} - {brand} - {model}".Trim()
            Dim normalizedSpecsOnly As String = NormalizeSpecs(specsOnly)

            ' NSOC / Property / Brand-Model
            Dim displayText As String = BuildDeviceDisplay(baseText, nsoc, propNo)

            deviceDisplayNames(devId) = displayText
            currentDevices.Add(devId)

            Dim rebuiltFull As String
            If String.IsNullOrWhiteSpace(normalizedSpecsOnly) Then
                rebuiltFull = baseText
            Else
                rebuiltFull = $"{baseText} - {normalizedSpecsOnly}"
            End If

            deviceFullSpecs(devId) = rebuiltFull

            AddDeviceToPanel(devId, displayText, rebuiltFull)
        Next

    End Sub

    ' Extract Category - Brand - Model from DeviceAndSpecs
    Private Function ExtractBaseText(fullText As String) As String
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length < 3 Then Return fullText.Trim()
        Return $"{parts(0).Trim()} - {parts(1).Trim()} - {parts(2).Trim()}"
    End Function

    Private Function ExtractOnlySpecs(fullText As String) As String
        If String.IsNullOrWhiteSpace(fullText) Then Return String.Empty

        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length <= 3 Then
            Return String.Empty
        End If

        Dim specsParts = parts.Skip(3).ToArray()
        Return String.Join(" - ", specsParts).Trim()
    End Function

    ' ===============================================================
    ' BLOCK 2 — LOAD DEVICE COMBO, MANAGE QTY, ADD DEVICE, REMOVE DEVICE
    ' ===============================================================
    Private Sub LoadDeviceCombo()

        Dim dt As DataTable = mdl.GetDevicesForUnits1(Me.unitId)

        deviceQuantities.Clear()
        deviceFullSpecs.Clear()
        deviceBaseDisplay.Clear()

        For Each row As DataRow In dt.Rows
            Dim id = CInt(row("device_id"))

            deviceQuantities(id) = CInt(row("qty"))

            Dim category = row("category_name").ToString()
            Dim brand = row("brand_name").ToString()
            Dim model = row("model").ToString()

            Dim nsoc As String = row("nsoc_name").ToString()
            Dim propNo As String = row("property_number").ToString()

            Dim rawSpecs = row("device_specs").ToString()

            Dim baseText As String = $"{category} - {brand} - {model}"

            Dim finalDisplay As String = BuildDeviceDisplay(baseText, nsoc, propNo)

            deviceBaseDisplay(id) = finalDisplay
            deviceFullSpecs(id) = rawSpecs
        Next

        UpdateComboList()
    End Sub

    Private Sub UpdateComboList()

        Dim dt As New DataTable()
        dt.Columns.Add("device_id", GetType(Integer))
        dt.Columns.Add("DeviceDisplay", GetType(String))

        For Each kv In deviceBaseDisplay
            Dim id = kv.Key
            Dim baseText = kv.Value
            Dim qty = If(deviceQuantities.ContainsKey(id), deviceQuantities(id), 0)

            dt.Rows.Add(id, $"{baseText} ({qty})")
        Next

        devicecb.DataSource = dt
        devicecb.DisplayMember = "DeviceDisplay"
        devicecb.ValueMember = "device_id"

        devicecb.SelectedIndex = -1
        devicecb.Text = "Select Device"
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

        Dim rawFullText As String = If(deviceFullSpecs.ContainsKey(devId), deviceFullSpecs(devId), String.Empty)

        addedDevices.Add(devId)
        currentDevices.Add(devId)

        deviceQuantities(devId) -= 1
        UpdateComboList()

        deviceDisplayNames(devId) = If(deviceBaseDisplay.ContainsKey(devId), deviceBaseDisplay(devId), "Unknown Device")

        Dim specsOnly As String = ExtractOnlySpecs(rawFullText)
        Dim normalizedSpecsOnly As String = NormalizeSpecs(specsOnly)
        Dim rebuilt As String = $"{deviceDisplayNames(devId)} - {normalizedSpecsOnly}"

        deviceFullSpecs(devId) = rebuilt

        AddDeviceToPanel(devId, deviceDisplayNames(devId), rebuilt)
    End Sub

    Private Sub AddDeviceToPanel(deviceId As Integer, displayText As String, fullSpecs As String)

        Dim container As New Panel With {
            .Height = 45,
            .Width = 380,
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

        AddHandler btn.Click,
            Sub()
                currentSpecDeviceId = deviceId

                Dim specsToShow As String

                If editedSpecs.ContainsKey(deviceId) Then
                    Dim list As New List(Of String)
                    For Each kv In editedSpecs(deviceId)
                        list.Add($"{kv.Key}: {kv.Value}")
                    Next
                    specsToShow = String.Join("; ", list)
                Else
                    specsToShow = deviceFullSpecs(deviceId)
                End If

                ShowSpecs(specsToShow, deviceId)
            End Sub

        Dim removeBtn As New Button With {
            .Text = "Remove",
            .Width = 80,
            .Dock = DockStyle.Right,
            .BackColor = Color.LightCoral,
            .ForeColor = Color.White
        }

        AddHandler removeBtn.Click,
            Sub()

                Dim deviceName As String = displayText

                Dim result = MessageBox.Show(
                    $"Are you sure you want to remove {deviceName}?",
                    "Confirm Removal",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                )

                If result = DialogResult.No Then Exit Sub

                deviceflowpnl.Controls.Remove(container)

                currentDevices.Remove(deviceId)
                editedSpecs.Remove(deviceId)
                removedDevices.Add(deviceId)

                If deviceQuantities.ContainsKey(deviceId) Then
                    deviceQuantities(deviceId) += 1
                End If

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

        Dim dict As Dictionary(Of String, String)

        If editedSpecs.ContainsKey(deviceId) Then
            dict = editedSpecs(deviceId)
        Else
            dict = ParseSpecs(fullSpecs)
        End If

        For Each kvp In dict
            specsTable.RowCount += 1

            specsTable.Controls.Add(New Label With {
                .Text = kvp.Key & ":",
                .Width = 120,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft
            }, 0, specsTable.RowCount - 1)

            specsTable.Controls.Add(New TextBox With {
                .Text = kvp.Value,
                .Dock = DockStyle.Fill
            }, 1, specsTable.RowCount - 1)
        Next
    End Sub

    Private Function NormalizeSpecs(raw As String) As String

        If String.IsNullOrWhiteSpace(raw) Then
            Return String.Empty
        End If

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

        Dim parts = fullSpecs.Split(New String() {" - "}, StringSplitOptions.None)

        If parts.Length < 4 Then
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
        End If

        Dim specString As String = String.Join(" - ", parts.Skip(3)).Trim()

        specString = NormalizeSpecs(specString)

        If specString.Contains(";") = False AndAlso specString.Contains(":") = False Then
            Return dict
        End If

        Dim items = specString.Split(";"c)

        For Each item As String In items
            If String.IsNullOrWhiteSpace(item) Then Continue For
            Dim p = item.Split(":"c)
            If p.Length >= 2 Then
                Dim key = p(0).Trim()
                Dim val = String.Join(":", p.Skip(1)).Trim()
                If Not dict.ContainsKey(key) Then
                    dict(key) = val
                End If
            End If
        Next

        Return dict
    End Function

    Private Sub savespecsbtn_Click(sender As Object, e As EventArgs) Handles savespecsbtn.Click

        If currentSpecDeviceId = -1 Then
            MessageBox.Show("Select a device first.")
            Return
        End If

        Dim dict As New Dictionary(Of String, String)

        For i = 0 To specsTable.RowCount - 1

            Dim lbl = TryCast(specsTable.GetControlFromPosition(0, i), Label)
            Dim txt = TryCast(specsTable.GetControlFromPosition(1, i), TextBox)

            If lbl IsNot Nothing AndAlso txt IsNot Nothing Then
                dict(lbl.Text.Replace(":", "").Trim) = txt.Text.Trim
            End If
        Next

        editedSpecs(currentSpecDeviceId) = dict

        MessageBox.Show("Specs saved temporarily!", "Saved",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub LoadAssignments()

        originalAssignList = mdl.GetAssignments()

        assigncb.DataSource = originalAssignList.Copy()
        assigncb.DisplayMember = "Full name"
        assigncb.ValueMember = "user_id"
        assigncb.SelectedIndex = -1
    End Sub

    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click

        Dim newAssignedId As Integer = If(selectedAssignedId.HasValue, selectedAssignedId.Value, originalAssignedId)

        Dim newUnitName = unitnametxt.Text.Trim()
        Dim remarksText = remarkstxt.Text.Trim()

        Dim sb As New StringBuilder()
        sb.AppendLine("You are about to save the following changes:" & vbCrLf)

        If newUnitName <> originalUnitName Then
            sb.AppendLine($"- Unit Name: {originalUnitName} → {newUnitName}")
        End If

        If newAssignedId <> originalAssignedId Then
            sb.AppendLine($"- Assigned Personnel: {GetNameById(originalAssignedId)} → {GetNameById(newAssignedId)}")
        End If

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

        If editedSpecs.Any() Then
            sb.AppendLine("- Edited Specs:")
            For Each kvp In editedSpecs
                sb.AppendLine("  • " & deviceDisplayNames(kvp.Key))
                For Each specKvp In kvp.Value
                    sb.AppendLine($"       {specKvp.Key}: {specKvp.Value}")
                Next
            Next
        End If

        sb.AppendLine(vbCrLf & "Proceed?")

        If MessageBox.Show(sb.ToString(), "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        mdl.SaveUnitChanges(
            unitId,
            newAssignedId,
            removedDevices,
            addedDevices,
            editedSpecs,
            newUnitName,
            remarksText
        )

        MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function GetNameById(userId As Integer) As String
        Dim row = originalAssignList.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Integer)("user_id") = userId)
        If row IsNot Nothing Then Return row("Full name").ToString()
        Return ""
    End Function

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    Private Sub assignbtn_Click(sender As Object, e As EventArgs) Handles assignbtn.Click

        If assigncb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a personnel first.")
            Return
        End If

        assigntxt.Text = assigncb.Text
        selectedAssignedId = CInt(assigncb.SelectedValue)
    End Sub

    ' ============================
    ' BUILD DISPLAY TITLE
    ' ============================
    Private Function BuildDeviceDisplay(baseText As String, nsoc As String, propNo As String) As String
        Dim title As String = ""

        If Not String.IsNullOrWhiteSpace(nsoc) Then
            title = nsoc.Trim()
        ElseIf Not String.IsNullOrWhiteSpace(propNo) Then
            title = "Property No: " & propNo.Trim()
        End If

        If title = "" Then
            Return baseText
        Else
            Return title & " - " & baseText
        End If
    End Function

End Class
