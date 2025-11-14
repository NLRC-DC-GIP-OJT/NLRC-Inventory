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

        For Each dr As DataRow In devices.Rows
            Dim devId As Integer = dr("DeviceID")
            Dim fullText As String = dr("DeviceAndSpecs").ToString()

            ' Button text = Category - Brand - Model only
            Dim baseText As String = ExtractBaseText(fullText)
            deviceDisplayNames(devId) = baseText

            currentDevices.Add(devId)

            AddDeviceToPanel(devId, baseText, fullText)
        Next

    End Sub

    ' Extract Category - Brand - Model from DeviceAndSpecs
    Private Function ExtractBaseText(fullText As String) As String
        ' Split by " - "
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length < 3 Then Return fullText
        Return $"{parts(0)} - {parts(1)} - {parts(2)}"
    End Function

    ' ============================================================================
    ' BLOCK 2 — LOAD DEVICE COMBO, MANAGE QTY, ADD DEVICE, REMOVE DEVICE
    ' ============================================================================

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
            deviceFullSpecs(id) = row("DeviceAndSpecs").ToString()
            deviceBaseDisplay(id) = $"{row("category_name")} - {row("brand_name")} - {row("model")}"
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
            Dim qty = deviceQuantities(id)

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
        If deviceQuantities(devId) <= 0 Then
            MessageBox.Show("No more available stock for this device.")
            Return
        End If

        Dim fullText As String = deviceFullSpecs(devId)

        addedDevices.Add(devId)
        currentDevices.Add(devId)

        ' Decrease qty ONLY for Working devices
        deviceQuantities(devId) -= 1
        UpdateComboList()

        ' Display only Category - Brand - Model
        deviceDisplayNames(devId) = deviceBaseDisplay(devId)

        AddDeviceToPanel(devId, deviceBaseDisplay(devId), fullText)

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

            deviceflowpnl.Controls.Remove(container)

            currentDevices.Remove(deviceId)
            editedSpecs.Remove(deviceId)
            removedDevices.Add(deviceId)

            ' Return to WORKING stock
            deviceQuantities(deviceId) += 1
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
            .Width = 120
        }, 0, specsTable.RowCount - 1)

            specsTable.Controls.Add(New TextBox With {
            .Text = kvp.Value,
            .Dock = DockStyle.Fill
        }, 1, specsTable.RowCount - 1)

        Next

    End Sub


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

        ' Find start of actual specs (after 3rd dash)
        ' Category - Brand - Model - SPEC1:xx; SPEC2:yy
        Dim parts = fullSpecs.Split("-"c)

        If parts.Length < 4 Then Return dict

        ' Everything after model = specs
        Dim specString As String = parts(parts.Length - 1).Trim()

        If specString.Contains(";") = False AndAlso specString.Contains(":") = False Then
            Return dict
        End If

        ' Split into pairs
        Dim items = specString.Split(";"c)

        For Each item As String In items
            Dim p = item.Split(":"c)
            If p.Length = 2 Then
                Dim key = p(0).Trim()
                Dim val = p(1).Trim()
                dict(key) = val
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

        For i As Integer = 0 To specsTable.RowCount - 1

            Dim lbl = TryCast(specsTable.GetControlFromPosition(0, i), Label)
            Dim txt = TryCast(specsTable.GetControlFromPosition(1, i), TextBox)

            If lbl IsNot Nothing AndAlso txt IsNot Nothing Then
                dict(lbl.Text.Replace(":", "").Trim()) = txt.Text.Trim()
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

        Dim newAssignedId As Object =
            If(assigncb.SelectedIndex = -1, originalAssignedId, assigncb.SelectedValue)

        Dim newUnitName = unitnametxt.Text.Trim()
        Dim remarksText = remarkstxt.Text.Trim()

        Dim sb As New StringBuilder()
        sb.AppendLine("You are about to save the following changes:" & vbCrLf)

        If newUnitName <> originalUnitName Then
            sb.AppendLine($"- Unit Name: {originalUnitName} → {newUnitName}")
        End If

        If newAssignedId <> originalAssignedId Then
            sb.AppendLine("- Assigned Personnel changed")
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

        If MessageBox.Show(sb.ToString(), "Confirm Save",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
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

    ' ===============================================================
    ' CLOSE
    ' ===============================================================
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

End Class
