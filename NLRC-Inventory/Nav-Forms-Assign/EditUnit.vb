Imports System.Data
Imports System.Text
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

        ' devices DataTable contains columns: DeviceID, DeviceAndSpecs (full string like "Category - Brand - Model - CPU:..., RAM:...")
        For Each dr As DataRow In devices.Rows
            Dim devId As Integer = CInt(dr("DeviceID"))
            Dim rawFullText As String = dr("DeviceAndSpecs").ToString()

            ' Button text = Category - Brand - Model only
            Dim baseText As String = ExtractBaseText(rawFullText)
            deviceDisplayNames(devId) = baseText

            currentDevices.Add(devId)

            ' Normalize the full specs string to guaranteed format:
            ' "Category - Brand - Model - spec1: x; spec2: y; ..."
            Dim specsPart As String = ExtractOnlySpecs(rawFullText)
            Dim normalizedSpecsPart As String = NormalizeSpecs(specsPart)
            Dim rebuiltFull As String = $"{baseText} - {normalizedSpecsPart}"

            ' Store normalized full string for this device (useful for later when selecting from combo)
            deviceFullSpecs(devId) = rebuiltFull

            AddDeviceToPanel(devId, baseText, rebuiltFull)
        Next

    End Sub

    ' Extract Category - Brand - Model from DeviceAndSpecs
    Private Function ExtractBaseText(fullText As String) As String
        ' Split by " - " (preserve ordering)
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length < 3 Then Return fullText.Trim()
        Return $"{parts(0).Trim()} - {parts(1).Trim()} - {parts(2).Trim()}"
    End Function

    ' Extract only the specs portion from a full string (everything after the 3rd " - ")
    Private Function ExtractOnlySpecs(fullText As String) As String
        If String.IsNullOrWhiteSpace(fullText) Then Return String.Empty

        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length <= 3 Then
            ' If there are exactly 3 or fewer parts, there may be no "- specs" separator.
            ' The specs might be appended directly after the model with or without a dash.
            ' Try to locate the fourth segment by finding the first occurrence of " - " after model.
            ' Fallback: remove the first three token-like segments (split by " - " or by spaces) — conservative approach:
            Return String.Empty
        End If

        ' Join everything after index 2 (the 3rd element) as the specs (in case specs contained " - " itself)
        Dim specsParts = parts.Skip(3).ToArray()
        Return String.Join(" - ", specsParts).Trim()
    End Function

    ' ===============================================================
    ' BLOCK 2 — LOAD DEVICE COMBO, MANAGE QTY, ADD DEVICE, REMOVE DEVICE
    ' ===============================================================

    ' ===============================================================
    ' LOAD DEVICE COMBO + QUANTITY (only WORKING devices)
    ' ===============================================================
    Private Sub LoadDeviceCombo()

        Dim dt As DataTable = mdl.GetDevicesForUnits1(Me.unitId)

        deviceQuantities.Clear()
        deviceFullSpecs.Clear()
        deviceBaseDisplay.Clear()

        For Each row As DataRow In dt.Rows
            Dim id = CInt(row("device_id"))
            deviceQuantities(id) = CInt(row("qty"))

            ' The GetDevicesForUnits1 might return DeviceAndSpecs or you may have separate category/brand/model columns.
            ' If DeviceAndSpecs column exists as e.g. "Laptop - Dell - Inspiron - CPU:..., RAM:..."
            If row.Table.Columns.Contains("DeviceAndSpecs") Then
                Dim rawFull = row("DeviceAndSpecs").ToString()
                Dim base = ExtractBaseText(rawFull)
                Dim specsOnly = ExtractOnlySpecs(rawFull)
                Dim normalizedSpecs = NormalizeSpecs(specsOnly)
                Dim rebuiltFull = $"{base} - {normalizedSpecs}"

                deviceFullSpecs(id) = rebuiltFull
                deviceBaseDisplay(id) = base
            Else
                ' If DeviceAndSpecs doesn't exist, try constructing from category_name, brand_name, model
                deviceBaseDisplay(id) = $"{row("category_name")} - {row("brand_name")} - {row("model")}"
                deviceFullSpecs(id) = deviceBaseDisplay(id) ' no specs available
            End If
        Next

        UpdateComboList()

    End Sub



    ' ===============================================================
    ' UPDATE COMBO LIST
    ' ===============================================================
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


    ' ===============================================================
    ' ADD DEVICE BUTTON CLICK
    ' ===============================================================
    Private Sub adddevicebtn_Click(sender As Object, e As EventArgs) Handles adddevicebtn.Click

        If devicecb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a device first.")
            Return
        End If

        Dim devId As Integer = CInt(devicecb.SelectedValue)

        ' Prevent duplicates
        If currentDevices.Contains(devId) Then
            MessageBox.Show("Device already assigned to this unit.")
            Return
        End If

        ' Prevent adding if no available WORKING qty
        If Not deviceQuantities.ContainsKey(devId) OrElse deviceQuantities(devId) <= 0 Then
            MessageBox.Show("No more available stock for this device.")
            Return
        End If

        Dim rawFullText As String = If(deviceFullSpecs.ContainsKey(devId), deviceFullSpecs(devId), String.Empty)

        addedDevices.Add(devId)
        currentDevices.Add(devId)

        ' Decrease qty ONLY for Working devices
        deviceQuantities(devId) -= 1
        UpdateComboList()

        ' Display only Category - Brand - Model
        deviceDisplayNames(devId) = If(deviceBaseDisplay.ContainsKey(devId), deviceBaseDisplay(devId), "Unknown Device")

        ' Build normalized full specs: ensure semicolon separators and "Base - specs" format
        Dim specsOnly As String = ExtractOnlySpecs(rawFullText)
        Dim normalizedSpecsOnly As String = NormalizeSpecs(specsOnly)
        Dim rebuilt As String = $"{deviceDisplayNames(devId)} - {normalizedSpecsOnly}"

        ' store normalized full specs for this device (so subsequent clicks use same normalized format)
        deviceFullSpecs(devId) = rebuilt

        AddDeviceToPanel(devId, deviceDisplayNames(devId), rebuilt)

    End Sub


    ' ===============================================================
    ' ADD DEVICE BUTTON TO PANEL
    ' ===============================================================
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
            ShowSpecs(fullSpecs, deviceId)
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

        ' Remove from UI
        deviceflowpnl.Controls.Remove(container)

        ' Tracking edits
        currentDevices.Remove(deviceId)
        editedSpecs.Remove(deviceId)
        removedDevices.Add(deviceId)

        ' Return quantity back to working stock
        If deviceQuantities.ContainsKey(deviceId) Then
            deviceQuantities(deviceId) += 1
        End If

        UpdateComboList()

    End Sub


        container.Controls.Add(removeBtn)
        container.Controls.Add(btn)
        deviceflowpnl.Controls.Add(container)

    End Sub



    ' ===============================================================
    ' SHOW SPECS IN THE SPECS PANEL
    ' ===============================================================
    Private Sub ShowSpecs(fullSpecs As String, deviceId As Integer)

        specsTable.Controls.Clear()
        specsTable.RowCount = 0
        currentSpecDeviceId = deviceId

        Dim dict As Dictionary(Of String, String)

        ' If user already edited → load temporary version
        If editedSpecs.ContainsKey(deviceId) Then
            dict = editedSpecs(deviceId)
        Else
            dict = ParseSpecs(fullSpecs)
        End If

        ' Create UI rows
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


    ' ===============================================================
    ' NORMALIZE SPECS
    ' Takes DB format:
    '   "CPU: Intel Core i5, RAM: 8GB, Storage: 1TB HDD, GPU: Integrated"
    '
    ' Converts to ParseSpecs() format:
    '   "CPU: Intel Core i5; RAM: 8GB; Storage: 1TB HDD; GPU: Integrated"
    ' ===============================================================
    Private Function NormalizeSpecs(raw As String) As String

        If String.IsNullOrWhiteSpace(raw) Then
            Return String.Empty
        End If

        Dim s As String = raw.Trim()

        ' Some DB entries might use commas between specs. Convert commas to semicolons.
        ' Also handle cases where semi-colons already present.
        ' First replace any comma followed by optional space with semicolon.
        s = System.Text.RegularExpressions.Regex.Replace(s, ",\s*", "; ")

        ' Remove duplicate semicolons/spaces
        s = System.Text.RegularExpressions.Regex.Replace(s, ";\s*;", ";")
        s = s.Replace(" ;", ";").Trim()

        ' Make sure we don't end with a trailing semicolon or trailing spaces
        If s.EndsWith(";") Then s = s.TrimEnd(";"c).Trim()

        Return s

    End Function

    ' ===============================================================
    ' PARSE SPECS FROM FULL STRING
    ' Example input:
    '   "Category - Brand - Model - spec1: x; spec2: y; spec3: z"
    ' Returns dictionary:
    '   spec1 → x
    '   spec2 → y
    ' ===============================================================
    Private Function ParseSpecs(fullSpecs As String) As Dictionary(Of String, String)

        Dim dict As New Dictionary(Of String, String)

        If String.IsNullOrWhiteSpace(fullSpecs) Then Return dict

        ' Find start of actual specs (after 3rd " - ")
        Dim parts = fullSpecs.Split(New String() {" - "}, StringSplitOptions.None)

        If parts.Length < 4 Then
            ' Try a fallback: maybe fullSpecs is just the specs (no base). Normalize and parse directly.
            Dim fallback = NormalizeSpecs(fullSpecs)
            If String.IsNullOrWhiteSpace(fallback) Then Return dict
            Dim fallbackItems = fallback.Split(";"c)
            For Each it As String In fallbackItems
                Dim p = it.Split(":"c)
                If p.Length >= 2 Then
                    Dim key = p(0).Trim()
                    Dim val = String.Join(":", p.Skip(1)).Trim() ' in case value contains colon
                    If Not dict.ContainsKey(key) Then dict(key) = val
                End If
            Next
            Return dict
        End If

        ' Everything after model = join remaining parts (in case specs had " - " internally)
        Dim specString As String = String.Join(" - ", parts.Skip(3)).Trim()

        ' Normalize separators (commas -> semicolons etc.)
        specString = NormalizeSpecs(specString)

        If specString.Contains(";") = False AndAlso specString.Contains(":") = False Then
            Return dict
        End If

        ' Split into pairs by semicolon
        Dim items = specString.Split(";"c)

        For Each item As String In items
            If String.IsNullOrWhiteSpace(item) Then Continue For
            Dim p = item.Split(":"c)
            If p.Length >= 2 Then
                Dim key = p(0).Trim()
                Dim val = String.Join(":", p.Skip(1)).Trim() ' preserve extra colons in value
                If Not dict.ContainsKey(key) Then
                    dict(key) = val
                End If
            End If
        Next

        Return dict

    End Function


    ' ===============================================================
    ' SAVE SPECS TEMPORARILY (NOT DATABASE)
    ' ===============================================================
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

    ' ===============================================================
    ' LOAD ASSIGNMENTS
    ' ===============================================================
    Private Sub LoadAssignments()

        originalAssignList = mdl.GetAssignments()

        assigncb.DataSource = originalAssignList.Copy()
        assigncb.DisplayMember = "Full name"
        assigncb.ValueMember = "user_id"
        assigncb.SelectedIndex = -1

    End Sub

    ' ===============================================================
    ' SAVE BUTTON
    ' ===============================================================
    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click

        ' Use selectedAssignedId if available, otherwise keep original
        Dim newAssignedId As Integer = If(selectedAssignedId.HasValue, selectedAssignedId.Value, originalAssignedId)

        Dim newUnitName = unitnametxt.Text.Trim()
        Dim remarksText = remarkstxt.Text.Trim()

        Dim sb As New StringBuilder()
        sb.AppendLine("You are about to save the following changes:" & vbCrLf)

        ' Unit name change
        If newUnitName <> originalUnitName Then
            sb.AppendLine($"- Unit Name: {originalUnitName} → {newUnitName}")
        End If

        ' Assigned personnel change
        If newAssignedId <> originalAssignedId Then
            sb.AppendLine($"- Assigned Personnel: {GetNameById(originalAssignedId)} → {GetNameById(newAssignedId)}")
        End If

        ' Removed devices
        If removedDevices.Any() Then
            sb.AppendLine("- Removed Devices:")
            For Each d In removedDevices
                sb.AppendLine("  • " & deviceDisplayNames(d))
            Next
        End If

        ' Added devices
        If addedDevices.Any() Then
            sb.AppendLine("- Added Devices:")
            For Each d In addedDevices
                sb.AppendLine("  • " & deviceDisplayNames(d))
            Next
        End If

        ' Edited specs
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


    ' ===============================================================
    ' CLOSE
    ' ===============================================================
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    Private Sub assignbtn_Click(sender As Object, e As EventArgs) Handles assignbtn.Click

        ' Make sure an item is selected
        If assigncb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a personnel first.")
            Return
        End If

        ' Transfer selected name to textbox
        assigntxt.Text = assigncb.Text

        ' ►► Store new assigned ID separately
        selectedAssignedId = CInt(assigncb.SelectedValue)

    End Sub

End Class
