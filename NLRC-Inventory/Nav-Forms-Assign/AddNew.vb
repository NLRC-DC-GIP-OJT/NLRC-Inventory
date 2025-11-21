Imports System.Data
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView
Imports MySql.Data.MySqlClient

Public Class AddNew
    Private mdl As New model()
    Private previewTable As New DataTable()
    Private currentDevices As DataTable

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

            ' Optional: scale fonts
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

    ' ========================
    ' LOAD
    ' ========================
    Private Sub AddNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Fill parent panel / form area
        Me.Dock = DockStyle.Fill

        ' initialize auto-resize
        InitializeLayoutScaling()

        LoadCategoriesForAssign()

        ' Setup preview table
        previewTable.Columns.Add("pointer", GetType(Integer))
        previewTable.Columns.Add("brands", GetType(String))
        previewTable.Columns.Add("model", GetType(String))
        previewTable.Columns.Add("status", GetType(String))
        previewTable.Columns.Add("total_devices", GetType(Integer))
        previewTable.Columns.Add("dev_category_pointer", GetType(Integer))

        unitsdgv1.DataSource = previewTable

        ' Add a "Remove" button column (no header)
        Dim removeButton As New DataGridViewButtonColumn()
        removeButton.Name = "removeBtn"
        removeButton.HeaderText = ""
        removeButton.Text = "Remove"
        removeButton.UseColumnTextForButtonValue = True
        removeButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        unitsdgv1.Columns.Add(removeButton)

        ' Customize DataGridView appearance after binding
        AddHandler unitsdgv1.DataBindingComplete,
            Sub()
                With unitsdgv1
                    ' Hide internal columns
                    If .Columns.Contains("pointer") Then .Columns("pointer").Visible = False
                    If .Columns.Contains("dev_category_pointer") Then .Columns("dev_category_pointer").Visible = False

                    ' Rename headers
                    If .Columns.Contains("brands") Then .Columns("brands").HeaderText = "Brand"
                    If .Columns.Contains("model") Then .Columns("model").HeaderText = "Model"
                    If .Columns.Contains("status") Then .Columns("status").HeaderText = "Status"
                    If .Columns.Contains("total_devices") Then .Columns("total_devices").HeaderText = "Total Devices Available"
                    If .Columns.Contains("removeBtn") Then
                        .Columns("removeBtn").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Columns("removeBtn").Width = 80
                    End If

                    ' Make columns fill the width of the DataGridView
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                    ' Style header
                    .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                    ' Make rows read-only and prevent adding/deleting manually
                    .ReadOnly = True
                    .AllowUserToAddRows = False
                    .AllowUserToDeleteRows = False
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .MultiSelect = False
                End With
            End Sub

        ' Default quantity
        quantitxt.Text = "1"
    End Sub

    ' ========================
    ' EXISTING LOGIC (unchanged)
    ' ========================
    Private Sub LoadCategoriesForAssign()
        Dim categories = mdl.GetCategories()
        If categories.Count > 0 Then
            catecb.DataSource = categories
            catecb.DisplayMember = "CategoryName"
            catecb.ValueMember = "Pointer"
            catecb.SelectedIndex = -1

            ' Enable autocomplete
            catecb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            catecb.AutoCompleteSource = AutoCompleteSource.ListItems
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
            ' Filter out devices already in previewTable
            Dim filteredDevices As DataTable = currentDevices.Clone()
            For Each row As DataRow In currentDevices.Rows
                Dim ptr As Integer = Convert.ToInt32(row("pointer"))
                Dim exists = previewTable.AsEnumerable().Any(Function(r) r.Field(Of Integer)("pointer") = ptr)
                If Not exists Then
                    filteredDevices.ImportRow(row)
                End If
            Next

            ' Add display_name column showing Brand - Model (Total)
            If Not filteredDevices.Columns.Contains("display_name") Then
                filteredDevices.Columns.Add(
                    "display_name",
                    GetType(String),
                    "brands + ' - ' + model + ' (' + Convert(total_devices, 'System.String') + ')' "
                )
            End If

            ' Bind to ComboBox
            devicecb1.DataSource = filteredDevices
            devicecb1.DisplayMember = "display_name"
            devicecb1.ValueMember = "pointer"
            devicecb1.SelectedIndex = -1
            devicecb1.Enabled = True

            ' Update stock label
            UpdateAvailableStock(filteredDevices)
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

    Private Sub UpdateAvailableStock(filteredDevices As DataTable)
        Dim totalStock As Integer = 0
        For Each row As DataRow In filteredDevices.Rows
            totalStock += Convert.ToInt32(row("total_devices"))
        Next
        checkstocklbl.Text = "Available Stock: " & totalStock.ToString()
    End Sub

    Private Sub UpdateStockLabels()
        If devicecb1.SelectedIndex < 0 OrElse devicecb1.SelectedItem Is Nothing Then
            devicestocklbl.Text = "Device Stock: 0"
            checkstocklbl.Text = "Available Stock: 0"
            Return
        End If

        Dim selectedRow As DataRowView = CType(devicecb1.SelectedItem, DataRowView)
        Dim stock As Integer = Convert.ToInt32(selectedRow("total_devices"))

        devicestocklbl.Text = $"Device Stock: {stock}"

        Dim filteredDevices = CType(devicecb1.DataSource, DataTable)
        UpdateAvailableStock(filteredDevices)
    End Sub

    Private Sub devicecb1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devicecb1.SelectedIndexChanged
        UpdateStockLabels()
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
            newRow("dev_category_pointer") = Convert.ToInt32(found(0)("dev_category_pointer"))
            previewTable.Rows.Add(newRow)
        End If

        ' Refresh ComboBox and available stock after adding
        Dim selectedCategory = CType(catecb.SelectedItem, DeviceCategory)
        currentDevices = mdl.GetDevicesByCategory2(selectedCategory.Pointer)
        catecb_SelectedIndexChanged(Nothing, Nothing)
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
            Dim brandName As String = row("brands").ToString()
            Dim modelName As String = row("model").ToString()
            confirmMessage &= $"{brandName} {modelName}: Quantity {quantity}" & Environment.NewLine
            totalUnits += quantity
        Next

        confirmMessage &= Environment.NewLine & $"Total devices to assign: {totalUnits}" & Environment.NewLine
        confirmMessage &= "Do you want to proceed?"

        Dim result As DialogResult = MessageBox.Show(confirmMessage, "Confirm Assignment", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result <> DialogResult.Yes Then
            Return
        End If

        ' Prepare DataTable for saving
        Dim selectedDevices As New DataTable()
        selectedDevices.Columns.Add("pointer", GetType(Integer))
        selectedDevices.Columns.Add("brands", GetType(Integer))
        selectedDevices.Columns.Add("model", GetType(String))
        selectedDevices.Columns.Add("status", GetType(String))
        selectedDevices.Columns.Add("dev_category_pointer", GetType(Integer))

        For Each row As DataRow In previewTable.Rows
            Dim newRow As DataRow = selectedDevices.NewRow()
            newRow("pointer") = Convert.ToInt32(row("pointer"))
            newRow("model") = row("model").ToString()
            newRow("status") = row("status").ToString()
            newRow("dev_category_pointer") = Convert.ToInt32(row("dev_category_pointer"))

            If IsNumeric(row("brands")) Then
                newRow("brands") = Convert.ToInt32(row("brands"))
            Else
                Dim brandName As String = row("brands").ToString()
                Dim categoryPointer As Integer = Convert.ToInt32(row("dev_category_pointer"))
                newRow("brands") = mdl.GetBrandPointerByName(brandName, categoryPointer)
            End If

            selectedDevices.Rows.Add(newRow)
        Next

        ' Save units
        If mdl.SaveInvUnitDevices(selectedDevices, quantity, remark) Then
            MessageBox.Show("Units successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            previewTable.Clear()
            quantitxt.Text = "1"
            remarktxt1.Text = ""
            ' Refresh ComboBox and stock after save
            catecb_SelectedIndexChanged(Nothing, Nothing)
        Else
            MessageBox.Show("Error saving units. Check stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Increment quantity with stock limit check
    Private Sub addQuantityBtn_Click(sender As Object, e As EventArgs) Handles addQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        quantity += 1

        ' Check each device in previewTable if it still has enough stock
        For Each row As DataRow In previewTable.Rows
            Dim availableStock As Integer = Convert.ToInt32(row("total_devices"))
            Dim brandName As String = row("brands").ToString()

            If quantity > availableStock Then
                MessageBox.Show($"{brandName} only has {availableStock} stocks available!", "Stock Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        quantitxt.Text = quantity.ToString()
        UpdateStockLabels()
    End Sub

    ' Decrement quantity
    Private Sub minusQuantityBtn_Click(sender As Object, e As EventArgs) Handles minusQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        If quantity > 1 Then
            quantity -= 1
        End If
        quantitxt.Text = quantity.ToString()
        UpdateStockLabels()
    End Sub

    Private Sub unitsdgv1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles unitsdgv1.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = unitsdgv1.Columns("removeBtn").Index Then
            Dim brandName As String = unitsdgv1.Rows(e.RowIndex).Cells("brands").Value.ToString()
            Dim modelName As String = unitsdgv1.Rows(e.RowIndex).Cells("model").Value.ToString()

            Dim confirm = MessageBox.Show($"Remove {brandName} {modelName} from the list?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                previewTable.Rows.RemoveAt(e.RowIndex)

                ' Refresh available stock after removing
                catecb_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        ' Try to get the parent Panel of the UserControl
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)

        ' If immediate parent is not a Panel, look further up
        If parentPanel Is Nothing Then
            parentPanel = TryCast(Me.Parent?.Parent, Panel)
        End If

        ' Hide the panel if found
        If parentPanel IsNot Nothing Then
            parentPanel.Visible = False
        End If
    End Sub

End Class
