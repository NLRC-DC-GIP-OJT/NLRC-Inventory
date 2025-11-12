Imports MySql.Data.MySqlClient

Public Class Configuration

    Private db As New model()
    Private editingBrandId As Integer = 0
    Private editingSpecsId As Integer = 0

    ' === Form Load ===
    Private Sub Configuration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDeviceCategories()
        LoadCategoryComboBox()
        LoadBrands()
        LoadCategoryForSpecs()
        LoadSpecsDGV()
    End Sub

    ' === Insert Category ===
    Private Sub catbtn_Click(sender As Object, e As EventArgs) Handles catbtn.Click
        Dim categoryName As String = cattxt.Text.Trim()
        Dim description As String = destxt.Text.Trim()

        If String.IsNullOrWhiteSpace(categoryName) Then
            MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Check if the category already exists
        If db.IsCategoryExists(categoryName) Then
            MessageBox.Show("This category already exists!", "Duplicate Category", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim createdBy As Integer = If(Session.LoggedInUserPointer > 0, Session.LoggedInUserPointer, 360)
        Dim success As Boolean = db.InsertDeviceCategory(categoryName, description, createdBy)

        If success Then
            MessageBox.Show("Device category inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cattxt.Clear()
            destxt.Clear()
            LoadDeviceCategories()
            LoadCategoryComboBox()
            LoadCategoryForSpecs()
        Else
            MessageBox.Show("Failed to insert device category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    ' === Load Device Categories to devicedgv ===
    Private Sub LoadDeviceCategories()
        Try
            Dim categoryList As List(Of DeviceCategory) = db.GetDeviceCategories()

            If categoryList IsNot Nothing Then
                devicedgv.DataSource = Nothing
                devicedgv.DataSource = categoryList

                ' Hide unwanted columns
                If devicedgv.Columns.Contains("Pointer") Then devicedgv.Columns("Pointer").Visible = False
                If devicedgv.Columns.Contains("CreatedAt") Then devicedgv.Columns("CreatedAt").Visible = False
                If devicedgv.Columns.Contains("UpdatedAt") Then devicedgv.Columns("UpdatedAt").Visible = False

                ' Rename columns for consistency
                If devicedgv.Columns.Contains("CategoryName") Then
                    devicedgv.Columns("CategoryName").HeaderText = "Category Name"
                End If
                If devicedgv.Columns.Contains("Description") Then
                    devicedgv.Columns("Description").HeaderText = "Description"
                End If

                ' === Add Edit button ===
                If Not devicedgv.Columns.Contains("Edit") Then
                    Dim editColumn As New DataGridViewButtonColumn()
                    editColumn.HeaderText = ""
                    editColumn.Name = "Edit"
                    editColumn.Text = "Edit"
                    editColumn.UseColumnTextForButtonValue = True
                    devicedgv.Columns.Add(editColumn)
                End If

                ' === Add Delete button ===
                If Not devicedgv.Columns.Contains("Delete") Then
                    Dim deleteColumn As New DataGridViewButtonColumn()
                    deleteColumn.HeaderText = ""
                    deleteColumn.Name = "Delete"
                    deleteColumn.Text = "Delete"
                    deleteColumn.UseColumnTextForButtonValue = True
                    devicedgv.Columns.Add(deleteColumn)
                End If

                ' === Layout (same as other DGVs) ===
                devicedgv.Dock = DockStyle.Fill
                devicedgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                devicedgv.RowHeadersVisible = False

                ' Make Edit/Delete small, others flexible
                For Each col As DataGridViewColumn In devicedgv.Columns
                    If col.Name = "Edit" Or col.Name = "Delete" Then
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        col.Width = 60
                    Else
                        col.FillWeight = 50
                    End If
                Next

                ' Ensure Edit/Delete always appear at the end
                devicedgv.Columns("Edit").DisplayIndex = devicedgv.Columns.Count - 2
                devicedgv.Columns("Delete").DisplayIndex = devicedgv.Columns.Count - 1
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading device categories: " & ex.Message)
        End Try
    End Sub


    ' === Category Edit/Delete handling ===
    Private Sub devicedgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles devicedgv.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = devicedgv.Rows(e.RowIndex)
            Dim categoryId As Integer = Convert.ToInt32(selectedRow.Cells("Pointer").Value)

            If devicedgv.Columns(e.ColumnIndex).Name = "Edit" Then
                cattxt.Text = selectedRow.Cells("CategoryName").Value.ToString()
                destxt.Text = selectedRow.Cells("Description").Value.ToString()
                editingBrandId = categoryId
                catbtn.Text = "Update Category"

            ElseIf devicedgv.Columns(e.ColumnIndex).Name = "Delete" Then
                If MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    If db.DeleteCategory(categoryId) Then
                        MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadDeviceCategories()
                        LoadCategoryComboBox()
                        LoadCategoryForSpecs()
                    Else
                        MessageBox.Show("Failed to delete category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If
    End Sub


    ' === Load Category Names into ComboBox (catcb) ===
    Private Sub LoadCategoryComboBox()
        Dim dt As DataTable = db.GetAllCategories()
        If dt IsNot Nothing Then
            catcb.DataSource = dt
            catcb.DisplayMember = "category_name"
            catcb.ValueMember = "pointer"
            catcb.SelectedIndex = -1
        End If
    End Sub


    ' === Insert Brand ===
    Private Sub brandbtn_Click(sender As Object, e As EventArgs) Handles brandbtn.Click
        If brandbtn.Text = "Update Brand" Then
            Dim categoryPointer As Integer = Convert.ToInt32(catcb.SelectedValue)
            Dim brandName As String = brandtxt.Text.Trim()

            If String.IsNullOrWhiteSpace(brandName) Then
                MessageBox.Show("Please enter a brand name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim success As Boolean = db.UpdateBrand(editingBrandId, categoryPointer, brandName)

            If success Then
                MessageBox.Show("Brand updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBrands()
                brandtxt.Clear()
                catcb.SelectedIndex = -1
                brandbtn.Text = "Insert Brand"
            Else
                MessageBox.Show("Failed to update brand.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub


    ' === Load All Brands into branddgv ===
    Private Sub LoadBrands()
        Dim dt As DataTable = db.GetAllBrands()

        If dt IsNot Nothing Then
            branddgv.DataSource = Nothing
            branddgv.DataSource = dt

            If branddgv.Columns.Contains("pointer") Then branddgv.Columns("pointer").Visible = False
            If branddgv.Columns.Contains("created_at") Then branddgv.Columns("created_at").Visible = False
            If branddgv.Columns.Contains("updated_at") Then branddgv.Columns("updated_at").Visible = False
            If branddgv.Columns.Contains("category_pointer") Then branddgv.Columns("category_pointer").Visible = False

            branddgv.Columns("category_name").HeaderText = "Category"
            branddgv.Columns("brand_name").HeaderText = "Brand Name"

            ' Add Edit button
            If Not branddgv.Columns.Contains("Edit") Then
                Dim editColumn As New DataGridViewButtonColumn()
                editColumn.HeaderText = ""
                editColumn.Name = "Edit"
                editColumn.Text = "Edit"
                editColumn.UseColumnTextForButtonValue = True
                branddgv.Columns.Add(editColumn)
            End If

            ' Add Delete button
            If Not branddgv.Columns.Contains("Delete") Then
                Dim deleteColumn As New DataGridViewButtonColumn()
                deleteColumn.HeaderText = ""
                deleteColumn.Name = "Delete"
                deleteColumn.Text = "Delete"
                deleteColumn.UseColumnTextForButtonValue = True
                branddgv.Columns.Add(deleteColumn)
            End If

            ' === Layout settings ===
            branddgv.Dock = DockStyle.Fill
            branddgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            branddgv.RowHeadersVisible = False

            ' Keep Edit/Delete small, rest fill space
            For Each col As DataGridViewColumn In branddgv.Columns
                If col.Name = "Edit" Or col.Name = "Delete" Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    col.Width = 60
                Else
                    col.FillWeight = 50
                End If
            Next

            branddgv.Columns("Edit").DisplayIndex = branddgv.Columns.Count - 2
            branddgv.Columns("Delete").DisplayIndex = branddgv.Columns.Count - 1
        End If
    End Sub



    ' ================= Add Specification Section =====================

    Private Sub LoadCategoryForSpecs()
        Dim dt As DataTable = db.GetAllCategories()
        If dt IsNot Nothing Then
            catcb1.DataSource = dt
            catcb1.DisplayMember = "category_name"
            catcb1.ValueMember = "pointer"
            catcb1.SelectedIndex = -1
        End If
    End Sub


    Private Sub addspecsbtn_Click(sender As Object, e As EventArgs) Handles addspecsbtn.Click
        Dim newSpecBox As New TextBox()
        newSpecBox.Width = flowpnl.Width - 25
        newSpecBox.Margin = New Padding(3, 3, 3, 3)
        newSpecBox.PlaceholderText = "Enter specification detail..."
        flowpnl.Controls.Add(newSpecBox)
    End Sub


    Private Sub btnInsertSpecs_Click(sender As Object, e As EventArgs) Handles btnInsertSpecs.Click
        If btnInsertSpecs.Text = "Update Specs" Then
            Dim categoryId As Integer = Convert.ToInt32(catcb1.SelectedValue)
            Dim specsList As New List(Of String)

            For Each ctrl As Control In flowpnl.Controls
                If TypeOf ctrl Is TextBox Then
                    Dim txt As TextBox = DirectCast(ctrl, TextBox)
                    If Not String.IsNullOrWhiteSpace(txt.Text) Then
                        specsList.Add(txt.Text.Trim())
                    End If
                End If
            Next

            If specsList.Count = 0 Then
                MessageBox.Show("Please add at least one specification.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim combinedSpecs As String = String.Join(";", specsList)
            Dim success As Boolean = db.UpdateSpecs(editingSpecsId, categoryId, combinedSpecs)

            If success Then
                MessageBox.Show("Specifications updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                flowpnl.Controls.Clear()
                LoadSpecsDGV()
                btnInsertSpecs.Text = "Insert Specs"
            Else
                MessageBox.Show("Failed to update specifications.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub


    ' === Load specs into specsdgv ===
    Private Sub LoadSpecsDGV()
        Dim dt As DataTable = db.GetAllSpecs()

        If dt IsNot Nothing Then
            specsdgv.DataSource = Nothing
            specsdgv.DataSource = dt

            If specsdgv.Columns.Contains("pointer") Then specsdgv.Columns("pointer").Visible = False
            If specsdgv.Columns.Contains("created_at") Then specsdgv.Columns("created_at").Visible = False
            If specsdgv.Columns.Contains("updated_at") Then specsdgv.Columns("updated_at").Visible = False
            If specsdgv.Columns.Contains("category_pointer") Then specsdgv.Columns("category_pointer").Visible = False

            specsdgv.Columns("category_name").HeaderText = "Category"
            specsdgv.Columns("specs").HeaderText = "Specifications"

            ' Add Edit button
            If Not specsdgv.Columns.Contains("Edit") Then
                Dim editColumn As New DataGridViewButtonColumn()
                editColumn.HeaderText = ""
                editColumn.Name = "Edit"
                editColumn.Text = "Edit"
                editColumn.UseColumnTextForButtonValue = True
                specsdgv.Columns.Add(editColumn)
            End If

            ' Add Delete button
            If Not specsdgv.Columns.Contains("Delete") Then
                Dim deleteColumn As New DataGridViewButtonColumn()
                deleteColumn.HeaderText = ""
                deleteColumn.Name = "Delete"
                deleteColumn.Text = "Delete"
                deleteColumn.UseColumnTextForButtonValue = True
                specsdgv.Columns.Add(deleteColumn)
            End If

            ' === Layout settings ===
            specsdgv.Dock = DockStyle.Fill
            specsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            specsdgv.RowHeadersVisible = False

            For Each col As DataGridViewColumn In specsdgv.Columns
                If col.Name = "Edit" Or col.Name = "Delete" Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    col.Width = 60
                Else
                    col.FillWeight = 50
                End If
            Next

            specsdgv.Columns("Edit").DisplayIndex = specsdgv.Columns.Count - 2
            specsdgv.Columns("Delete").DisplayIndex = specsdgv.Columns.Count - 1
        End If
    End Sub



    ' === Brand Edit/Delete handling ===
    Private Sub branddgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles branddgv.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = branddgv.Rows(e.RowIndex)
            Dim brandId As Integer = Convert.ToInt32(selectedRow.Cells("pointer").Value)
            Dim categoryPointer As Integer = Convert.ToInt32(selectedRow.Cells("category_pointer").Value)

            If branddgv.Columns(e.ColumnIndex).Name = "Edit" Then
                brandtxt.Text = selectedRow.Cells("brand_name").Value.ToString()
                catcb.SelectedValue = categoryPointer
                editingBrandId = brandId
                brandbtn.Text = "Update Brand"
            ElseIf branddgv.Columns(e.ColumnIndex).Name = "Delete" Then
                If MessageBox.Show("Are you sure you want to delete this brand?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    If db.DeleteBrand(brandId) Then
                        MessageBox.Show("Brand deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadBrands()
                    Else
                        MessageBox.Show("Failed to delete brand.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If
    End Sub


    ' === Specs Edit/Delete handling ===
    Private Sub specsdgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles specsdgv.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = specsdgv.Rows(e.RowIndex)
            Dim specsId As Integer = Convert.ToInt32(selectedRow.Cells("pointer").Value)
            Dim categoryPointer As Integer = Convert.ToInt32(selectedRow.Cells("category_pointer").Value)

            If specsdgv.Columns(e.ColumnIndex).Name = "Edit" Then
                catcb1.SelectedValue = categoryPointer
                Dim specsValue As String = selectedRow.Cells("specs").Value.ToString()
                Dim specValues() As String = specsValue.Split(";"c)
                flowpnl.Controls.Clear()

                For Each spec In specValues
                    Dim txt As New TextBox() With {
                        .Text = spec.Trim(),
                        .Width = flowpnl.Width - 25,
                        .Margin = New Padding(3, 3, 3, 3)
                    }
                    flowpnl.Controls.Add(txt)
                Next

                editingSpecsId = specsId
                btnInsertSpecs.Text = "Update Specs"
            ElseIf specsdgv.Columns(e.ColumnIndex).Name = "Delete" Then
                If MessageBox.Show("Are you sure you want to delete this specification?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                    If db.DeleteSpecs(specsId) Then
                        MessageBox.Show("Specification deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadSpecsDGV()
                    Else
                        MessageBox.Show("Failed to delete specification.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            End If
        End If
    End Sub

End Class
