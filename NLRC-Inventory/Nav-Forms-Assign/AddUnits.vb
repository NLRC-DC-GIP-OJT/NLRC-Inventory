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



    ' 🔹 Load categories for catecb
    Private Sub LoadCategoriesForAssign()
        Dim db As New model()
        Dim categories = db.GetCategories()

        If categories.Count > 0 Then
            catecb.DataSource = categories
            catecb.DisplayMember = "CategoryName"
            catecb.ValueMember = "Pointer"
            catecb.SelectedIndex = -1
        Else
            catecb.DataSource = Nothing
            catecb.Items.Clear()
            catecb.Items.Add("No categories available")
            catecb.SelectedIndex = 0
        End If
    End Sub


    Private Sub AddUnits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAssignments()
        LoadDevices()
        LoadDevicesUnit2()
        LoadCategoriesForAssign()
        devicestocklbl.Visible = False

        AddHandler devicecb.TextChanged, AddressOf FilterDeviceCombo
        AddHandler assigncb.TextChanged, AddressOf FilterAssignCombo


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
    ' Category ComboBox (catecb)
    ' =========================
    Private Sub catecb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catecb.SelectedIndexChanged
        ' Hide the stock label initially
        devicestocklbl.Visible = False

        ' If no category is selected, clear and exit
        If catecb.SelectedIndex < 0 OrElse catecb.SelectedItem Is Nothing Then
            devicecb1.DataSource = Nothing
            devicecb1.Items.Clear()
            devicecb1.Text = ""
            checkstocklbl.Text = "Available Stock:"
            Exit Sub
        End If

        ' Get selected category
        Dim selectedCategory = CType(catecb.SelectedItem, DeviceCategory)
        Dim selectedCategoryPointer = selectedCategory.Pointer

        ' Get all working devices under this category
        Dim devices As DataTable = mdl.GetDevicesByCategory(selectedCategoryPointer)

        ' Update devicecb1
        If devices IsNot Nothing AndAlso devices.Rows.Count > 0 Then
            devicecb1.DataSource = devices
            devicecb1.DisplayMember = "display_name"  ' ✅ should be brand + model
            devicecb1.ValueMember = "display_name"
            devicecb1.SelectedIndex = -1
            devicecb1.Enabled = True
            checkstocklbl.Text = "Available Stock: " & devices.Rows.Count.ToString()
        Else
            devicecb1.DataSource = Nothing
            devicecb1.Items.Clear()
            devicecb1.Items.Add("No working devices available")
            devicecb1.SelectedIndex = 0
            devicecb1.Enabled = False
            checkstocklbl.Text = "Available Stock: 0"
        End If
    End Sub



    Private Sub catecb_Click(sender As Object, e As EventArgs) Handles catecb.Click
        LoadCategoriesForAssign()
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

    ''Private Sub FilterDeviceComboUnit2(sender As Object, e As EventArgs)
    ''SafeComboFilter(DirectCast(sender, ComboBox), originalDeviceList, "Device", "device_id", AddressOf FilterDeviceComboUnit2)
    ''End Sub

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
        ' Parse current quantity
        Dim currentQty As Integer = Convert.ToInt32(quantitxt.Text)

        ' Make sure devicestocklbl has a valid stock number
        If devicestocklbl.Visible AndAlso devicestocklbl.Text.Contains(":") Then
            ' Extract the stock number
            Dim stockText As String = devicestocklbl.Text.Split(":"c)(1).Trim()
            Dim availableStock As Integer

            If Integer.TryParse(stockText, availableStock) Then
                ' Prevent exceeding available stock
                If currentQty < availableStock Then
                    quantitxt.Text = (currentQty + 1).ToString()
                Else
                    MessageBox.Show("You cannot add more than the available stock.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            ' Fallback: allow normal increment if label not visible or invalid
            quantitxt.Text = (currentQty + 1).ToString()
        End If
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
    ' Add button unit2pnl (quantity supported)
    ' =========================
    Private Sub addbtn1_Click(sender As Object, e As EventArgs) Handles addbtn1.Click
        ' Validate device selection
        If devicecb1.SelectedIndex = -1 OrElse devicecb1.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a device.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validate quantity
        Dim quantity As Integer
        If Not Integer.TryParse(quantitxt.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Device info
        Dim deviceName As String = devicecb1.Text
        Dim deviceId As Integer = CInt(devicecb1.SelectedValue)
        Dim remarks As String = remarktxt1.Text.Trim()
        Dim unitName As String = If(String.IsNullOrWhiteSpace(unitnametxt.Text), Nothing, unitnametxt.Text.Trim())

        ' Add columns if not present
        If unitsdgv.Columns.Count = 0 Then
            unitsdgv.Columns.Add("unit_name", "Unit Name")
            unitsdgv.Columns.Add("device_name", "Device")
            unitsdgv.Columns.Add("remarks", "Remarks")
            unitsdgv.Columns.Add("quantity", "Quantity")
            unitsdgv.Columns.Add("device_id", "Device ID")
            unitsdgv.Columns("device_id").Visible = False
        End If

        ' Add row to DataGridView
        unitsdgv.Rows.Add(unitName, deviceName, remarks, quantity, deviceId)

        ' Clear input fields
        ClearFieldsUnit2()
    End Sub

    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If unitsdgv.Rows.Count = 0 Then
            MessageBox.Show("No devices to save.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim unitName As String = Nothing
        Dim assignedPersonnel As Integer? = Nothing
        Dim devicePointers As New List(Of Integer)
        Dim remarksList As New List(Of String)

        ' Iterate over each row in DataGridView
        For Each row As DataGridViewRow In unitsdgv.Rows
            If row.IsNewRow Then Continue For

            ' Capture unit name (take from first row)
            If unitName Is Nothing AndAlso unitsdgv.Columns.Contains("unit_name") Then
                unitName = If(row.Cells("unit_name").Value?.ToString(), Nothing)
            End If

            ' Capture assigned personnel (first row with value)
            If assignedPersonnel Is Nothing AndAlso unitsdgv.Columns.Contains("assigned_to") Then
                If row.Cells("assigned_to") IsNot Nothing AndAlso row.Cells("assigned_to").Value IsNot Nothing Then
                    Dim fullName = row.Cells("assigned_to").Value.ToString()
                    Dim userRow = mdl.GetAssignments().AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Full name") = fullName)
                    If userRow IsNot Nothing Then
                        assignedPersonnel = userRow.Field(Of Integer)("user_id")
                    End If
                End If
            End If

            ' Get quantity for this row
            Dim quantity As Integer = 1
            If unitsdgv.Columns.Contains("quantity") AndAlso row.Cells("quantity") IsNot Nothing AndAlso row.Cells("quantity").Value IsNot Nothing Then
                Integer.TryParse(row.Cells("quantity").Value.ToString(), quantity)
            End If

            ' Add device pointer multiple times based on quantity
            If unitsdgv.Columns.Contains("device_id") AndAlso row.Cells("device_id") IsNot Nothing AndAlso row.Cells("device_id").Value IsNot Nothing Then
                Dim deviceId As Integer = Convert.ToInt32(row.Cells("device_id").Value)
                For i As Integer = 1 To quantity
                    devicePointers.Add(deviceId)
                Next
            End If

            ' Capture remarks
            If unitsdgv.Columns.Contains("remarks") AndAlso row.Cells("remarks") IsNot Nothing AndAlso row.Cells("remarks").Value IsNot Nothing Then
                remarksList.Add(row.Cells("remarks").Value.ToString())
            End If
        Next

        ' Combine remarks
        Dim allRemarks As String = String.Join("; ", remarksList)

        ' Call SaveUnit
        Dim success As Boolean = mdl.SaveUnit(unitName, assignedPersonnel, devicePointers, allRemarks)

        If success Then
            MessageBox.Show("Unit saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            unitsdgv.Rows.Clear()
            LoadDevices() ' Refresh device list
        Else
            MessageBox.Show("Failed to save unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    ' =========================
    ' Save button (handles temp unit names)
    ' =========================







    Private Sub devicecb1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devicecb1.SelectedIndexChanged
        If devicecb1.SelectedIndex = -1 OrElse devicecb1.SelectedValue Is Nothing Then
            devicestocklbl.Visible = False
            Exit Sub
        End If

        Dim devicePointer As Integer
        If Integer.TryParse(devicecb1.SelectedValue.ToString(), devicePointer) Then
            Dim stockCount As Integer = mdl.GetDeviceStock(devicePointer)

            devicestocklbl.Visible = True
            If stockCount > 0 Then
                devicestocklbl.Text = $"Available Stocks: {stockCount}"
            Else
                devicestocklbl.Text = "Out of Stock"
            End If
        End If
    End Sub

    Private Sub savebtn1_Click(sender As Object, e As EventArgs) Handles savebtn1.Click
        If unitsdgv.Rows.Count = 0 Then
            MessageBox.Show("No devices to save.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim unitName As String = Nothing
        Dim assignedPersonnel As Integer? = Nothing
        Dim devicePointers As New List(Of Integer)
        Dim remarksList As New List(Of String)

        ' Iterate over each row in DataGridView
        For Each row As DataGridViewRow In unitsdgv.Rows
            If row.IsNewRow Then Continue For

            ' Capture unit name (take from first row)
            If unitName Is Nothing AndAlso unitsdgv.Columns.Contains("unit_name") Then
                unitName = If(row.Cells("unit_name").Value?.ToString(), Nothing)
            End If

            ' Capture assigned personnel (first row with value)
            If assignedPersonnel Is Nothing AndAlso unitsdgv.Columns.Contains("assigned_to") Then
                If row.Cells("assigned_to") IsNot Nothing AndAlso row.Cells("assigned_to").Value IsNot Nothing Then
                    Dim fullName = row.Cells("assigned_to").Value.ToString()
                    Dim userRow = mdl.GetAssignments().AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Full name") = fullName)
                    If userRow IsNot Nothing Then
                        assignedPersonnel = userRow.Field(Of Integer)("user_id")
                    End If
                End If
            End If

            ' Get quantity for this row
            Dim quantity As Integer = 1
            If unitsdgv.Columns.Contains("quantity") AndAlso row.Cells("quantity") IsNot Nothing AndAlso row.Cells("quantity").Value IsNot Nothing Then
                Integer.TryParse(row.Cells("quantity").Value.ToString(), quantity)
            End If

            ' Add device pointer multiple times based on quantity
            If unitsdgv.Columns.Contains("device_id") AndAlso row.Cells("device_id") IsNot Nothing AndAlso row.Cells("device_id").Value IsNot Nothing Then
                Dim deviceId As Integer = Convert.ToInt32(row.Cells("device_id").Value)
                For i As Integer = 1 To quantity
                    devicePointers.Add(deviceId)
                Next
            End If

            ' Capture remarks
            If unitsdgv.Columns.Contains("remarks") AndAlso row.Cells("remarks") IsNot Nothing AndAlso row.Cells("remarks").Value IsNot Nothing Then
                remarksList.Add(row.Cells("remarks").Value.ToString())
            End If
        Next

        ' Combine remarks
        Dim allRemarks As String = String.Join("; ", remarksList)

        ' Call SaveUnit
        Dim success As Boolean = mdl.SaveUnit(unitName, assignedPersonnel, devicePointers, allRemarks)

        If success Then
            MessageBox.Show("Unit saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            unitsdgv.Rows.Clear()
            LoadDevices() ' Refresh device list
        Else
            MessageBox.Show("Failed to save unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub



End Class
