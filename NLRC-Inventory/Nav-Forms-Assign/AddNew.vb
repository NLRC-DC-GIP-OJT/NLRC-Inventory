Imports System.Data
Imports MySql.Data.MySqlClient

Public Class AddNew
    Private mdl As New model()
    Private previewTable As New DataTable()
    Private currentDevices As DataTable

    Private Sub AddNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategoriesForAssign()

        ' Setup preview table
        previewTable.Columns.Add("pointer", GetType(Integer))
        previewTable.Columns.Add("brands", GetType(String))
        previewTable.Columns.Add("model", GetType(String))
        previewTable.Columns.Add("status", GetType(String))
        previewTable.Columns.Add("total_devices", GetType(Integer))

        unitsdgv1.DataSource = previewTable

        ' Hide pointer column after binding
        AddHandler unitsdgv1.DataBindingComplete, Sub()
                                                      If unitsdgv1.Columns.Contains("pointer") Then
                                                          unitsdgv1.Columns("pointer").Visible = False
                                                      End If
                                                  End Sub

        ' Default quantity
        quantitxt.Text = "1"
    End Sub

    Private Sub LoadCategoriesForAssign()
        Dim categories = mdl.GetCategories()
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

    Private Sub catecb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catecb.SelectedIndexChanged
        If catecb.SelectedIndex < 0 OrElse catecb.SelectedItem Is Nothing Then
            ResetDeviceSelection()
            Return
        End If

        Dim selectedCategory = CType(catecb.SelectedItem, DeviceCategory)
        currentDevices = mdl.GetDevicesByCategory2(selectedCategory.Pointer)

        If currentDevices IsNot Nothing AndAlso currentDevices.Rows.Count > 0 Then
            ' Create display name column for ComboBox
            If Not currentDevices.Columns.Contains("display_name") Then
                currentDevices.Columns.Add("display_name", GetType(String), "brands + ' - ' + model")
            End If

            devicecb1.DataSource = currentDevices
            devicecb1.DisplayMember = "display_name"
            devicecb1.ValueMember = "pointer"
            devicecb1.SelectedIndex = -1
            devicecb1.Enabled = True

            ' Sum total stock for all devices in the category
            Dim totalStock As Integer = 0
            For Each row As DataRow In currentDevices.Rows
                totalStock += Convert.ToInt32(row("total_devices"))
            Next
            checkstocklbl.Text = "Available Stock: " & totalStock.ToString()
            devicestocklbl.Text = "Device Stock: 0"
        Else
            ResetDeviceSelection()
        End If
    End Sub

    Private Sub ResetDeviceSelection()
        devicecb1.DataSource = Nothing
        devicecb1.Items.Clear()
        devicecb1.Text = ""
        devicecb1.Enabled = False
        checkstocklbl.Text = "Available Stock: 0"
        devicestocklbl.Text = "Device Stock: 0"
    End Sub

    Private Sub devicecb1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devicecb1.SelectedIndexChanged
        If devicecb1.SelectedIndex < 0 OrElse devicecb1.SelectedItem Is Nothing Then
            devicestocklbl.Text = "Device Stock: 0"
            Return
        End If

        Dim selectedRow As DataRowView = CType(devicecb1.SelectedItem, DataRowView)
        Dim selectedPointer As Integer = Convert.ToInt32(selectedRow("pointer"))

        Dim found() As DataRow = currentDevices.Select("pointer = " & selectedPointer)
        If found.Length > 0 Then
            Dim stock As Integer = Convert.ToInt32(found(0)("total_devices"))
            devicestocklbl.Text = "Device Stock: " & stock.ToString()
        Else
            devicestocklbl.Text = "Device Stock: 0"
        End If
    End Sub

    Private Sub addbtn1_Click(sender As Object, e As EventArgs) Handles addbtn1.Click
        If devicecb1.SelectedIndex < 0 OrElse devicecb1.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a device to add.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedPointer As Integer = Convert.ToInt32(devicecb1.SelectedValue)

        ' Prevent duplicates
        For Each row As DataRow In previewTable.Rows
            If Convert.ToInt32(row("pointer")) = selectedPointer Then
                MessageBox.Show("This device is already added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim found() As DataRow = currentDevices.Select("pointer = " & selectedPointer)
        If found.Length > 0 Then
            Dim newRow As DataRow = previewTable.NewRow()
            newRow("pointer") = Convert.ToInt32(found(0)("pointer"))
            newRow("brands") = If(found(0)("brands") Is DBNull.Value, "", found(0)("brands").ToString())
            newRow("model") = If(found(0)("model") Is DBNull.Value, "", found(0)("model").ToString())
            newRow("status") = If(found(0)("status") Is DBNull.Value, "", found(0)("status").ToString())
            newRow("total_devices") = Convert.ToInt32(found(0)("total_devices"))
            previewTable.Rows.Add(newRow)
        End If
    End Sub

    Private Sub savebtn1_Click(sender As Object, e As EventArgs) Handles savebtn1.Click
        If previewTable.Rows.Count = 0 Then
            MessageBox.Show("No devices to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim quantity As Integer
        If Not Integer.TryParse(quantitxt.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim remark As String = remarktxt1.Text.Trim()

        ' Build confirmation message
        Dim confirmMessage As String = "You are about to assign the following units:" & Environment.NewLine & Environment.NewLine
        Dim totalUnits As Integer = 0

        For Each row As DataRow In previewTable.Rows
            ' Get brand name for display
            Dim brandName As String
            If IsNumeric(row("brands")) Then
                brandName = mdl.GetBrandPointerByName(Convert.ToInt32(row("brands"))) ' <-- Make sure you have this function
            Else
                brandName = row("brands").ToString()
            End If

            Dim modelName As String = row("model").ToString()
            confirmMessage &= $"{brandName} {modelName}: Quantity {quantity}" & Environment.NewLine
            totalUnits += quantity
        Next

        confirmMessage &= Environment.NewLine & $"Total units to assign: {totalUnits}" & Environment.NewLine
        confirmMessage &= "Do you want to proceed?"

        ' Show confirmation dialog
        Dim result As DialogResult = MessageBox.Show(confirmMessage, "Confirm Assignment", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then
            Return
        End If

        ' Prepare DataTable with correct types for saving
        Dim selectedDevices As New DataTable()
        selectedDevices.Columns.Add("pointer", GetType(Integer))
        selectedDevices.Columns.Add("brands", GetType(Integer)) ' MUST BE INT
        selectedDevices.Columns.Add("model", GetType(String))
        selectedDevices.Columns.Add("status", GetType(String))

        For Each row As DataRow In previewTable.Rows
            Dim newRow As DataRow = selectedDevices.NewRow()
            newRow("pointer") = Convert.ToInt32(row("pointer"))

            ' Convert brand name to pointer if necessary
            If IsNumeric(row("brands")) Then
                newRow("brands") = Convert.ToInt32(row("brands"))
            Else
                newRow("brands") = mdl.GetBrandPointerByName(row("brands").ToString())
            End If

            newRow("model") = row("model")
            newRow("status") = row("status")
            selectedDevices.Rows.Add(newRow)
        Next

        ' Call Model to save
        If mdl.SaveInvUnitDevices(selectedDevices, quantity, remark) Then
            MessageBox.Show("Units successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            previewTable.Clear()
            quantitxt.Text = "1"
            remarktxt1.Text = ""
        Else
            MessageBox.Show("Error saving units. Check stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub








    ' Increment quantity
    Private Sub addQuantityBtn_Click(sender As Object, e As EventArgs) Handles addQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        quantity += 1
        quantitxt.Text = quantity.ToString()
    End Sub

    ' Decrement quantity
    Private Sub minusQuantityBtn_Click(sender As Object, e As EventArgs) Handles minusQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        If quantity > 1 Then
            quantity -= 1
        End If
        quantitxt.Text = quantity.ToString()
    End Sub

End Class