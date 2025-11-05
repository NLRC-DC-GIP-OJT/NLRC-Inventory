Imports System.Data
Imports System.Linq
Imports MySql.Data.MySqlClient

Public Class AddUnits
    Private mdl As New model()
    Private originalDeviceList As DataTable
    Private originalAssignList As DataTable
    Private isFiltering As Boolean = False
    Public Property UnitsSaved As Boolean
    Public Event UnitSaved()

    Private Sub AddUnits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAssignments()
        LoadDevices()
        LoadDevicesUnit2() ' Load for unit2pnl

        AddHandler devicecb.TextChanged, AddressOf FilterDeviceCombo
        AddHandler assigncb.TextChanged, AddressOf FilterAssignCombo
        AddHandler devicecb1.TextChanged, AddressOf FilterDeviceComboUnit2

        unitsdgv.EnableHeadersVisualStyles = False
        unitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        unitsdgv.AllowUserToAddRows = False

        quantitxt.Text = "1" ' default quantity
    End Sub

    ' =========================
    ' Unit1: Assignments
    ' =========================
    Private Sub LoadAssignments()
        originalAssignList = mdl.GetAssignments()

        If originalAssignList Is Nothing OrElse originalAssignList.Rows.Count = 0 Then
            MessageBox.Show("No users found in the database.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    ' =========================
    ' Unit1: Devices
    ' =========================
    Private Sub LoadDevices()
        originalDeviceList = mdl.GetDevicesForUnits()

        If originalDeviceList Is Nothing OrElse originalDeviceList.Rows.Count = 0 Then
            MessageBox.Show("No devices found in the database.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Only working devices
        Dim workingDevices = originalDeviceList.AsEnumerable().
            Where(Function(r) r.Field(Of String)("status").Trim().ToLower() = "working").
            CopyToDataTable()

        originalDeviceList = workingDevices

        devicecb.DataSource = originalDeviceList.Copy()
        devicecb.DisplayMember = "Device"
        devicecb.ValueMember = "device_id"
        devicecb.SelectedIndex = -1
        devicecb.DropDownStyle = ComboBoxStyle.DropDown
        devicecb.IntegralHeight = False
        devicecb.DropDownHeight = 350
    End Sub

    ' =========================
    ' Unit2: Devices (unit2pnl)
    ' =========================
    Private Sub LoadDevicesUnit2()
        If originalDeviceList Is Nothing OrElse originalDeviceList.Rows.Count = 0 Then Return

        devicecb1.DataSource = originalDeviceList.Copy()
        devicecb1.DisplayMember = "Device"
        devicecb1.ValueMember = "device_id"
        devicecb1.SelectedIndex = -1
        devicecb1.DropDownStyle = ComboBoxStyle.DropDown
        devicecb1.IntegralHeight = False
        devicecb1.DropDownHeight = 350
    End Sub

    ' =========================
    ' Filter handlers
    ' =========================
    Private Sub FilterDeviceCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalDeviceList, "Device", "device_id", AddressOf FilterDeviceCombo)
    End Sub

    Private Sub FilterAssignCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalAssignList, "Full name", "user_id", AddressOf FilterAssignCombo)
    End Sub

    Private Sub FilterDeviceComboUnit2(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalDeviceList, "Device", "device_id", AddressOf FilterDeviceComboUnit2)
    End Sub

    ' =========================
    ' Universal ComboBox Filter
    ' =========================
    Private Sub SafeComboFilter(cb As ComboBox, source As DataTable, displayCol As String, valueCol As String, handler As EventHandler)
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

    ' =========================
    ' Clear fields
    ' =========================
    Private Sub ClearFields()
        remarktxt.Clear()
        devicecb.SelectedIndex = -1
    End Sub

    Private Sub ClearFieldsUnit2()
        remarktxt1.Clear()
        quantitxt.Text = "1"
        devicecb1.SelectedIndex = -1
    End Sub

    ' =========================
    ' Quantity buttons for unit2pnl
    ' =========================
    Private Sub addQuantity_Click(sender As Object, e As EventArgs) Handles addQuantityBtn.Click
        quantitxt.Text = (Convert.ToInt32(quantitxt.Text) + 1).ToString()
    End Sub

    Private Sub minusQuantity_Click(sender As Object, e As EventArgs) Handles minusQuantityBtn.Click
        Dim qty As Integer = Convert.ToInt32(quantitxt.Text)
        If qty > 1 Then
            quantitxt.Text = (qty - 1).ToString()
        End If
    End Sub

    ' =========================
    ' Validate Unit Name
    ' =========================
    ' ✅ Validate if Unit Name already exists in database when leaving textbox
    Private Sub unitnametxt_Leave(sender As Object, e As EventArgs) Handles unitnametxt.Leave
        Try
            Dim unitName As String = unitnametxt.Text.Trim()
            If String.IsNullOrWhiteSpace(unitName) Then Exit Sub

            ' ✅ Use the model’s built-in function instead of direct connection
            If mdl.IsUnitNameExists(unitName) Then
                MessageBox.Show($"The unit name '{unitName}' already exists in the database.",
                            "Duplicate Unit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                unitnametxt.Focus()
                unitnametxt.SelectAll()
            End If
        Catch ex As Exception
            MessageBox.Show("Error checking unit name: " & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' =========================
    ' Add button unit1pnl
    ' =========================

    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(unitnametxt.Text) OrElse assigncb.SelectedIndex = -1 OrElse devicecb.SelectedIndex = -1 Then
            MessageBox.Show("Please fill out all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if Unit Name already exists in DataGridView (prevent duplicates in grid only)
        ' Uncomment and use this if needed to avoid duplicate unit names in DataGridView
        'For Each row As DataGridViewRow In unitsdgv.Rows
        '    If row.Cells("unit_name").Value?.ToString().Trim().ToLower() = unitnametxt.Text.Trim().ToLower() Then
        '        MessageBox.Show($"The unit name '{unitnametxt.Text.Trim()}' already exists in the list.", "Duplicate Unit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        unitnametxt.Focus()
        '        Return
        '    End If
        'Next

        ' Add columns to DataGridView if not already present
        If unitsdgv.Columns.Count = 0 Then
            unitsdgv.Columns.Add("unit_name", "Unit Name")
            unitsdgv.Columns.Add("assigned_to", "Assigned To")
            unitsdgv.Columns.Add("device", "Device")
            unitsdgv.Columns.Add("remarks", "Remarks")
            unitsdgv.Columns.Add("device_id", "Device ID")
            unitsdgv.Columns("device_id").Visible = False ' Hide device_id column
        End If

        ' Get selected device details
        Dim selectedDeviceName As String = devicecb.Text
        Dim selectedDeviceId As Integer = CInt(devicecb.SelectedValue)

        ' Add new row to DataGridView
        unitsdgv.Rows.Add(unitnametxt.Text.Trim(), assigncb.Text, selectedDeviceName, remarktxt.Text.Trim(), selectedDeviceId)

        ' Remove the selected device from originalDeviceList (if necessary)
        Dim rowToRemove = originalDeviceList.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Integer)("device_id") = selectedDeviceId)
        If rowToRemove IsNot Nothing Then
            originalDeviceList.Rows.Remove(rowToRemove)
        End If

        ' Update DataSource of device combo box
        devicecb.DataSource = originalDeviceList.Copy() ' Ensure the combo box is updated with the new list
        devicecb.DisplayMember = "Device"
        devicecb.ValueMember = "device_id"
        devicecb.SelectedIndex = -1 ' Deselect the device combo box

        ' Clear form fields after addition
        ClearFields()

        ' Scroll to the new row in the DataGridView and select it
        unitsdgv.FirstDisplayedScrollingRowIndex = unitsdgv.RowCount - 1
        unitsdgv.Rows(unitsdgv.RowCount - 1).Selected = True
    End Sub


    ' =========================
    ' Add button unit2pnl (new)
    ' =========================
    Private Sub addbtn1_Click(sender As Object, e As EventArgs) Handles addbtn1.Click

    End Sub

    ' =========================
    ' Save button (existing)
    ' =========================
    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        ' Check if DataGridView is empty
        If unitsdgv.Rows.Count = 0 Then
            MessageBox.Show("There are no units to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Counters for success and failure
        Dim successCount As Integer = 0
        Dim failCount As Integer = 0

        ' Loop through each row in the DataGridView
        For Each row As DataGridViewRow In unitsdgv.Rows
            ' Skip empty rows
            If row.IsNewRow Then Continue For

            Try
                ' Retrieve values from each cell
                Dim unitName As String = row.Cells("unit_name").Value?.ToString().Trim()
                Dim assignedName As String = row.Cells("assigned_to").Value?.ToString().Trim()
                Dim remarks As String = row.Cells("remarks").Value?.ToString().Trim()
                Dim deviceId As Integer = Convert.ToInt32(row.Cells("device_id").Value)

                ' Skip row if unit name is empty
                If String.IsNullOrWhiteSpace(unitName) Then Continue For

                ' Find assigned ID from originalAssignList using the assigned name
                Dim assignedId = originalAssignList.AsEnumerable() _
                    .Where(Function(r) r.Field(Of String)("Full name") = assignedName) _
                    .Select(Function(r) r.Field(Of Integer)("user_id")) _
                    .FirstOrDefault()

                ' Call the SaveUnit method to save the data
                If mdl.SaveUnit(unitName, assignedId, deviceId, remarks) Then
                    successCount += 1
                Else
                    failCount += 1
                End If

            Catch ex As Exception
                ' Catch any errors and increment failCount
                failCount += 1
            End Try
        Next

        ' Show the save summary message
        MessageBox.Show($"Save completed.{vbCrLf}✔ Successful: {successCount}{vbCrLf}❌ Failed: {failCount}", "Save Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear the DataGridView and form fields after saving
        unitsdgv.Rows.Clear()
        remarktxt.Clear()
        devicecb.SelectedIndex = -1
        assigncb.SelectedIndex = -1
        unitnametxt.Clear()
    End Sub

End Class
