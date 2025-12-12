Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class AddNew
    Private mdl As New model()
    Private previewTable As New DataTable()
    Private currentDevices As DataTable
    Public Event UnitSaved()

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS (same as AddUnits)
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

            ' ✅ FIXED: Y must use scaleY (you had scaleX before)
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
        ' behave like AddUnits inside a panel
        Me.Dock = DockStyle.Fill
        Me.AutoScaleMode = AutoScaleMode.None

        InitializeLayoutScaling()
        LoadCategoriesForAssign()

        ' Setup preview table
        previewTable.Columns.Clear()
        previewTable.Columns.Add("pointer", GetType(Integer))
        previewTable.Columns.Add("brands", GetType(String))
        previewTable.Columns.Add("model", GetType(String))
        previewTable.Columns.Add("status", GetType(String))
        previewTable.Columns.Add("total_devices", GetType(Integer))
        previewTable.Columns.Add("dev_category_pointer", GetType(Integer))
        previewTable.Columns.Add("category_name", GetType(String))

        unitsdgv1.DataSource = previewTable

        ' ✅ Prevent duplicate remove column
        If Not unitsdgv1.Columns.Contains("removeBtn") Then
            Dim removeButton As New DataGridViewButtonColumn()
            removeButton.Name = "removeBtn"
            removeButton.HeaderText = ""
            removeButton.Text = "Remove"
            removeButton.UseColumnTextForButtonValue = True
            removeButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            unitsdgv1.Columns.Add(removeButton)
        End If

        ' DataGridView styling (similar to AddUnits)
        unitsdgv1.EnableHeadersVisualStyles = False
        unitsdgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        unitsdgv1.AllowUserToAddRows = False
        unitsdgv1.AllowUserToDeleteRows = False
        unitsdgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        unitsdgv1.MultiSelect = False
        unitsdgv1.ReadOnly = True

        AddHandler unitsdgv1.DataBindingComplete,
            Sub()
                With unitsdgv1
                    If .Columns.Contains("pointer") Then .Columns("pointer").Visible = False
                    If .Columns.Contains("dev_category_pointer") Then .Columns("dev_category_pointer").Visible = False

                    If .Columns.Contains("category_name") Then .Columns("category_name").HeaderText = "Category"
                    If .Columns.Contains("brands") Then .Columns("brands").HeaderText = "Brand"
                    If .Columns.Contains("model") Then .Columns("model").HeaderText = "Model"
                    If .Columns.Contains("status") Then .Columns("status").HeaderText = "Assignment Status"
                    If .Columns.Contains("total_devices") Then .Columns("total_devices").HeaderText = "Total Devices Available"

                    If .Columns.Contains("removeBtn") Then
                        .Columns("removeBtn").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        .Columns("removeBtn").Width = 80
                    End If

                    If .Columns.Contains("category_name") Then .Columns("category_name").DisplayIndex = 0
                    If .Columns.Contains("brands") Then .Columns("brands").DisplayIndex = 1
                    If .Columns.Contains("model") Then .Columns("model").DisplayIndex = 2
                    If .Columns.Contains("status") Then .Columns("status").DisplayIndex = 3
                    If .Columns.Contains("total_devices") Then .Columns("total_devices").DisplayIndex = 4
                    If .Columns.Contains("removeBtn") Then .Columns("removeBtn").DisplayIndex = 5

                    .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
            End Sub

        quantitxt.Text = "1"
    End Sub

    ' ========================
    ' CATEGORY → DEVICES
    ' ========================
    Private Sub LoadCategoriesForAssign()
        Dim categories = mdl.GetCategories()
        If categories.Count > 0 Then
            catecb.DataSource = categories
            catecb.DisplayMember = "CategoryName"
            catecb.ValueMember = "Pointer"
            catecb.SelectedIndex = -1

            catecb.DropDownStyle = ComboBoxStyle.DropDown
            catecb.IntegralHeight = False
            catecb.DropDownHeight = 350

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

        If currentDevices Is Nothing OrElse currentDevices.Rows.Count = 0 Then
            ResetDeviceSelection()
            Return
        End If

        Dim source As DataTable = currentDevices

        ' keep only Unassigned ones
        If source.Columns.Contains("ass_status") Then
            Dim unassignedRows = source.AsEnumerable().
                Where(Function(r)
                          Dim st As String = If(r.IsNull("ass_status"), "", r.Field(Of String)("ass_status")).Trim()
                          Return String.Equals(st, "Unassigned", StringComparison.OrdinalIgnoreCase)
                      End Function)

            If unassignedRows.Any() Then
                source = unassignedRows.CopyToDataTable()
            Else
                ResetDeviceSelection()
                Return
            End If
        End If

        ' Filter out devices already in previewTable
        Dim filteredDevices As DataTable = source.Clone()
        For Each row As DataRow In source.Rows
            Dim ptr As Integer = Convert.ToInt32(row("pointer"))
            Dim exists = previewTable.AsEnumerable().Any(Function(r) r.Field(Of Integer)("pointer") = ptr)
            If Not exists Then filteredDevices.ImportRow(row)
        Next

        If filteredDevices.Rows.Count = 0 Then
            ResetDeviceSelection()
            Return
        End If

        ' display_name column
        If Not filteredDevices.Columns.Contains("display_name") Then
            filteredDevices.Columns.Add("display_name", GetType(String))
        End If

        For Each r As DataRow In filteredDevices.Rows
            Dim brand As String = If(Convert.IsDBNull(r("brands")), "", r("brands").ToString())
            Dim mdlTxt As String = If(Convert.IsDBNull(r("model")), "", r("model").ToString())
            Dim total As Integer = If(Convert.IsDBNull(r("total_devices")), 0, CInt(r("total_devices")))

            If String.IsNullOrWhiteSpace(mdlTxt) Then
                r("display_name") = $"{brand} ({total})"
            Else
                r("display_name") = $"{brand} - {mdlTxt} ({total})"
            End If
        Next

        devicecb1.DataSource = filteredDevices
        devicecb1.DisplayMember = "display_name"
        devicecb1.ValueMember = "pointer"
        devicecb1.SelectedIndex = -1
        devicecb1.Enabled = True

        devicecb1.DropDownStyle = ComboBoxStyle.DropDown
        devicecb1.IntegralHeight = False
        devicecb1.DropDownHeight = 350

        UpdateAvailableStock(filteredDevices)
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

    ' ========================
    ' ADD DEVICE TO PREVIEW
    ' ========================
    Private Sub addbtn1_Click(sender As Object, e As EventArgs) Handles addbtn1.Click
        If devicecb1.SelectedIndex < 0 OrElse devicecb1.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a device to add.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedPointer As Integer = Convert.ToInt32(devicecb1.SelectedValue)

        ' Prevent duplicates
        If previewTable.AsEnumerable().Any(Function(r) r.Field(Of Integer)("pointer") = selectedPointer) Then
            MessageBox.Show("This device is already added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim found() As DataRow = currentDevices.Select("pointer = " & selectedPointer)
        If found.Length = 0 Then Return

        Dim src As DataRow = found(0)
        Dim newRow As DataRow = previewTable.NewRow()

        newRow("pointer") = Convert.ToInt32(src("pointer"))
        newRow("brands") = If(src("brands") Is DBNull.Value, "", src("brands").ToString())
        newRow("model") = If(Convert.IsDBNull(src("model")), DBNull.Value, src("model").ToString())

        Dim assStat As String = ""
        If src.Table.Columns.Contains("ass_status") AndAlso Not IsDBNull(src("ass_status")) Then
            assStat = src("ass_status").ToString()
        End If
        newRow("status") = assStat

        newRow("total_devices") = Convert.ToInt32(src("total_devices"))

        Dim catPtrObj As Object = src("dev_category_pointer")
        If catPtrObj IsNot DBNull.Value Then
            newRow("dev_category_pointer") = Convert.ToInt32(catPtrObj)
        Else
            newRow("dev_category_pointer") = DBNull.Value
        End If

        Dim catPtr As Integer? = Nothing
        If Not IsDBNull(newRow("dev_category_pointer")) Then catPtr = Convert.ToInt32(newRow("dev_category_pointer"))
        newRow("category_name") = mdl.GetCategoryName(catPtr)

        previewTable.Rows.Add(newRow)

        ' Refresh list after adding
        Dim selectedCategory = CType(catecb.SelectedItem, DeviceCategory)
        currentDevices = mdl.GetDevicesByCategory2(selectedCategory.Pointer)
        catecb_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    ' ========================
    ' SAVE
    ' ========================
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

        Dim confirmMessage As New System.Text.StringBuilder()
        confirmMessage.AppendLine("You are about to assign the following units:")
        confirmMessage.AppendLine()

        Dim totalUnits As Integer = 0
        For Each row As DataRow In previewTable.Rows
            Dim brandName As String = row("brands").ToString()
            Dim modelName As String = If(row.IsNull("model"), "", row("model").ToString())

            Dim label As String = brandName
            If Not String.IsNullOrWhiteSpace(modelName) Then label &= " " & modelName

            confirmMessage.AppendLine($"{label}: Quantity {quantity}")
            totalUnits += quantity
        Next

        confirmMessage.AppendLine()
        confirmMessage.AppendLine($"Total devices to assign: {totalUnits}")
        confirmMessage.AppendLine("Do you want to proceed?")

        If MessageBox.Show(confirmMessage.ToString(), "Confirm Assignment",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim selectedDevices As New DataTable()
        selectedDevices.Columns.Add("pointer", GetType(Integer))
        selectedDevices.Columns.Add("brands", GetType(Integer))
        selectedDevices.Columns.Add("model", GetType(String))
        selectedDevices.Columns.Add("dev_category_pointer", GetType(Integer))

        For Each row As DataRow In previewTable.Rows
            Dim nr As DataRow = selectedDevices.NewRow()
            nr("pointer") = Convert.ToInt32(row("pointer"))

            Dim mObj As Object = row("model")
            nr("model") = If(mObj Is Nothing OrElse mObj Is DBNull.Value OrElse mObj.ToString().Trim() = "", DBNull.Value, mObj.ToString().Trim())

            nr("dev_category_pointer") = Convert.ToInt32(row("dev_category_pointer"))

            If IsNumeric(row("brands")) Then
                nr("brands") = Convert.ToInt32(row("brands"))
            Else
                Dim brandName As String = row("brands").ToString()
                Dim categoryPointer As Integer = Convert.ToInt32(row("dev_category_pointer"))
                nr("brands") = mdl.GetBrandPointerByName(brandName, categoryPointer)
            End If

            selectedDevices.Rows.Add(nr)
        Next

        Dim createdBy As Integer = If(Session.LoggedInUserPointer > 0, Session.LoggedInUserPointer, 1)

        If mdl.SaveInvUnitDevices(selectedDevices, quantity, remark, createdBy) Then
            MessageBox.Show("Units successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent UnitSaved()
            previewTable.Clear()
            quantitxt.Text = "1"
            remarktxt1.Text = ""
            catecb_SelectedIndexChanged(Nothing, Nothing)
        Else
            MessageBox.Show("Error saving units. Check stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' ========================
    ' QTY
    ' ========================
    Private Sub addQuantityBtn_Click(sender As Object, e As EventArgs) Handles addQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        quantity += 1

        For Each row As DataRow In previewTable.Rows
            Dim availableStock As Integer = Convert.ToInt32(row("total_devices"))
            Dim brandName As String = row("brands").ToString()

            If quantity > availableStock Then
                MessageBox.Show($"{brandName} only has {availableStock} stocks available!",
                                "Stock Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        quantitxt.Text = quantity.ToString()
        UpdateStockLabels()
    End Sub

    Private Sub minusQuantityBtn_Click(sender As Object, e As EventArgs) Handles minusQuantityBtn.Click
        Dim quantity As Integer = 1
        Integer.TryParse(quantitxt.Text, quantity)
        If quantity > 1 Then quantity -= 1
        quantitxt.Text = quantity.ToString()
        UpdateStockLabels()
    End Sub

    ' ========================
    ' REMOVE FROM GRID
    ' ========================
    Private Sub unitsdgv1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles unitsdgv1.CellContentClick
        If e.RowIndex < 0 Then Return
        If Not unitsdgv1.Columns.Contains("removeBtn") Then Return

        If e.ColumnIndex = unitsdgv1.Columns("removeBtn").Index Then
            Dim brandName As String = unitsdgv1.Rows(e.RowIndex).Cells("brands").Value?.ToString()
            Dim modelName As String = unitsdgv1.Rows(e.RowIndex).Cells("model").Value?.ToString()

            Dim confirm = MessageBox.Show($"Remove {brandName} {modelName} from the list?",
                                          "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                previewTable.Rows.RemoveAt(e.RowIndex)
                catecb_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    ' ========================
    ' CLOSE PANEL
    ' ========================
    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel Is Nothing Then parentPanel = TryCast(Me.Parent?.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

End Class
