Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class Configuration

    Private db As New model()
    Private editingBrandId As Integer = 0
    Private editingSpecsId As Integer = 0
    Private editingCategoryId As Integer = 0

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

    ' === Form Load ===
    Private Sub Configuration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDeviceCategories()
        LoadCategoryComboBox()
        LoadBrands()
        LoadCategoryForSpecs()
        LoadSpecsDGV()

        ' 🔁 initialize auto-resize using the designed layout
        InitializeLayoutScaling()
    End Sub

    ' === Insert / Update Category + Properties (no destxt) ===
    Private Sub catbtn_Click(sender As Object, e As EventArgs) Handles catbtn.Click
        Dim categoryName As String = cattxt.Text.Trim()
        Dim description As String = ""

        If String.IsNullOrWhiteSpace(categoryName) Then
            MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If editingCategoryId = 0 AndAlso db.IsCategoryExists(categoryName) Then
            MessageBox.Show("This category already exists!", "Duplicate Category", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userId As Integer = If(Session.LoggedInUserPointer > 0, Session.LoggedInUserPointer, 360)

        ' ===============================
        ' INSERT NEW CATEGORY
        ' ===============================
        If editingCategoryId = 0 Then

            Dim newCategoryId As Integer = db.InsertDeviceCategoryReturnId(categoryName, description, userId)
            If newCategoryId <= 0 Then
                MessageBox.Show("Failed to insert category.", "Error")
                Return
            End If

            ' Insert new properties
            Dim properties As New List(Of (Name As String, Required As Boolean, Active As Boolean))()

            For Each row As Control In propertyflowpnl.Controls
                If TypeOf row Is Panel Then

                    Dim propName As String = ""
                    Dim required As Boolean = True
                    Dim active As Boolean = True

                    For Each ctrl As Control In row.Controls
                        If TypeOf ctrl Is TextBox AndAlso ctrl.Name = "txtPropertyName" Then
                            propName = ctrl.Text.Trim()
                        ElseIf TypeOf ctrl Is CheckBox AndAlso ctrl.Name = "chkRequired" Then
                            required = CType(ctrl, CheckBox).Checked
                        ElseIf TypeOf ctrl Is CheckBox AndAlso ctrl.Name = "chkActive" Then
                            active = CType(ctrl, CheckBox).Checked
                        End If
                    Next

                    If propName <> "" Then
                        properties.Add((propName, required, active))
                    End If
                End If
            Next

            For Each p In properties
                db.InsertCategoryProperty(newCategoryId, p.Name, p.Required, p.Active, userId)
            Next

            MessageBox.Show("Category created successfully!", "Success")

        Else

            ' ===============================
            ' UPDATE EXISTING CATEGORY
            ' ===============================
            db.UpdateDeviceCategory(editingCategoryId, categoryName, description, userId)

            ' 🔥 UPDATE ONLY REQUIRED + ACTIVE FLAGS
            For Each row As Control In propertyflowpnl.Controls
                If TypeOf row Is Panel Then

                    Dim propId As Integer = Convert.ToInt32(row.Tag)
                    Dim required As Boolean = False
                    Dim active As Boolean = False

                    For Each ctrl As Control In row.Controls
                        If TypeOf ctrl Is CheckBox AndAlso ctrl.Name = "chkRequired" Then
                            required = CType(ctrl, CheckBox).Checked
                        ElseIf TypeOf ctrl Is CheckBox AndAlso ctrl.Name = "chkActive" Then
                            active = CType(ctrl, CheckBox).Checked
                        End If
                    Next

                    db.UpdateCategoryPropertyFlags(propId, required, active)

                End If
            Next

            MessageBox.Show("Category updated successfully!", "Success")

        End If

        ' RESET FORM
        cattxt.Clear()
        propertyflowpnl.Controls.Clear()
        editingCategoryId = 0
        catbtn.Text = "Insert Category"

        LoadDeviceCategories()
        LoadCategoryComboBox()
        LoadCategoryForSpecs()
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
        If e.RowIndex < 0 Then Return

        Dim selectedRow As DataGridViewRow = devicedgv.Rows(e.RowIndex)
        Dim categoryId As Integer = Convert.ToInt32(selectedRow.Cells("Pointer").Value)
        Dim categoryName As String = selectedRow.Cells("CategoryName").Value.ToString()
        Dim colName As String = devicedgv.Columns(e.ColumnIndex).Name

        If colName = "Edit" Then
            If MessageBox.Show($"Are you sure you want to edit category: {categoryName}?",
                           "Confirm Edit",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then

                cattxt.Text = selectedRow.Cells("CategoryName").Value.ToString()
                editingCategoryId = categoryId
                catbtn.Text = "Update Category"

                ' Load properties
                propertyflowpnl.Controls.Clear()
                Dim dtProps As DataTable = db.GetCategoryProperties(categoryId)

                If dtProps IsNot Nothing Then
                    For Each row As DataRow In dtProps.Rows

                        Dim propName As String = row("property_name").ToString().Trim()
                        Dim isRequired As Boolean = Convert.ToBoolean(row("required"))
                        Dim isActive As Boolean = Convert.ToBoolean(row("active"))

                        Dim rowPanel As New Panel()
                        rowPanel.Width = propertyflowpnl.ClientSize.Width - 2
                        rowPanel.Height = 30
                        rowPanel.Margin = New Padding(0, 2, 0, 2)

                        ' 🔥 STORE PROPERTY POINTER
                        rowPanel.Tag = row("pointer")

                        ' PROPERTY NAME LABEL
                        Dim lblProp As New Label()
                        lblProp.Text = propName
                        lblProp.Width = 220
                        lblProp.Location = New Point(5, 6)
                        lblProp.TextAlign = ContentAlignment.MiddleLeft

                        ' REQUIRED CHECKBOX
                        Dim chkReq As New CheckBox()
                        chkReq.Name = "chkRequired"
                        chkReq.Text = "Required"
                        chkReq.Checked = isRequired
                        chkReq.AutoSize = True
                        chkReq.Location = New Point(lblProp.Right + 10, 6)

                        ' ACTIVE CHECKBOX
                        Dim chkAct As New CheckBox()
                        chkAct.Name = "chkActive"
                        chkAct.Text = "Active"
                        chkAct.Checked = isActive
                        chkAct.AutoSize = True
                        chkAct.Location = New Point(chkReq.Right + 10, 6)

                        ' ADD CONTROLS
                        rowPanel.Controls.Add(lblProp)
                        rowPanel.Controls.Add(chkReq)
                        rowPanel.Controls.Add(chkAct)

                        propertyflowpnl.Controls.Add(rowPanel)

                    Next
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



    ' === Insert / Update Brand ===
    Private Sub brandbtn_Click(sender As Object, e As EventArgs) Handles brandbtn.Click
        Dim categoryPointer As Integer
        Dim brandName As String = brandtxt.Text.Trim()

        If catcb.SelectedIndex = -1 Then
            MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        categoryPointer = Convert.ToInt32(catcb.SelectedValue)

        If String.IsNullOrWhiteSpace(brandName) Then
            MessageBox.Show("Please enter a brand name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If brandbtn.Text = "Update Brand" Then
            ' === UPDATE ===
            Dim success As Boolean = db.UpdateBrand(editingBrandId, categoryPointer, brandName)
            If success Then
                MessageBox.Show("Brand updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to update brand.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            ' === INSERT ===
            Dim createdBy As Integer = If(Session.LoggedInUserPointer > 0, Session.LoggedInUserPointer, 360)
            Dim success As Boolean = db.InsertBrand(categoryPointer, brandName, createdBy)
            If success Then
                MessageBox.Show("Brand inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to insert brand.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        ' Refresh after insert or update
        brandtxt.Clear()
        catcb.SelectedIndex = -1
        brandbtn.Text = "Insert Brand"
        LoadBrands()
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

    ' ==========================
    ' Load specs_name into flowpnl for selected category
    ' ==========================
    Private Sub catcb1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catcb1.SelectedIndexChanged
        ' Clear previous controls
        flowpnl.Controls.Clear()
        editingSpecsId = 0
        btnInsertSpecs.Text = "Insert Specs"

        ' No category selected
        If catcb1.SelectedIndex = -1 Then Return

        ' Get selected category pointer
        Dim drv As DataRowView = TryCast(catcb1.SelectedItem, DataRowView)
        If drv Is Nothing Then Return

        Dim categoryId As Integer = Convert.ToInt32(drv("pointer"))

        ' Get specs_name list for this category
        ' Data from table inv_category_specs (specs_name column)
        Dim dtSpecs As DataTable = db.GetCategorySpecs(categoryId)
        If dtSpecs Is Nothing OrElse dtSpecs.Rows.Count = 0 Then Return

        For Each row As DataRow In dtSpecs.Rows
            Dim rawName As String = row("specs_name").ToString().Trim()
            If rawName = "" Then Continue For

            ' If someone stored "Processor: Intel i5" by mistake,
            ' only take the part BEFORE ":" → "Processor"
            Dim fieldName As String = rawName
            Dim colonIndex As Integer = rawName.IndexOf(":"c)
            If colonIndex >= 0 Then
                fieldName = rawName.Substring(0, colonIndex).Trim()
            End If

            ' === Container panel for one spec line ===
            Dim fieldPanel As New Panel()
            fieldPanel.Width = flowpnl.ClientSize.Width - 2
            fieldPanel.Height = 35
            fieldPanel.Margin = New Padding(0)

            ' === Label: "Processor:" ===
            Dim lbl As New Label()
            lbl.Text = fieldName & ":"
            lbl.Width = 120
            lbl.Location = New Point(5, 8)
            lbl.TextAlign = ContentAlignment.MiddleLeft

            ' === TextBox: EMPTY (you will type the value) ===
            Dim txt As New TextBox()
            txt.Text = "" ' no default value
            txt.Width = fieldPanel.Width - lbl.Width - 20
            txt.Location = New Point(lbl.Right + 5, 5)
            txt.Tag = fieldName              ' store spec name
            txt.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top

            ' Add controls to panel and panel to flowpnl
            fieldPanel.Controls.Add(lbl)
            fieldPanel.Controls.Add(txt)
            flowpnl.Controls.Add(fieldPanel)

            ' Keep layout correct when flowpnl resizes
            AddHandler flowpnl.Resize,
            Sub()
                fieldPanel.Width = flowpnl.ClientSize.Width - 2
                txt.Width = fieldPanel.Width - lbl.Width - 20
                txt.Location = New Point(lbl.Right + 5, 5)
            End Sub
        Next
    End Sub









    Private Sub addspecsbtn_Click(sender As Object, e As EventArgs) Handles addspecsbtn.Click
        ' === Create container panel ===
        Dim fieldPanel As New Panel
        fieldPanel.Width = flowpnl.ClientSize.Width - 2
        fieldPanel.Height = 35
        fieldPanel.Margin = New Padding(0)

        ' === Specs Name TextBox (SAVED INTO inv_category_specs) ===
        Dim txtSpecName As New TextBox
        txtSpecName.PlaceholderText = "Specs Name (e.g. Processor)"
        txtSpecName.Name = "txtSpecName"
        txtSpecName.Location = New Point(5, 5)
        txtSpecName.Width = 150

        ' === Specs Value TextBox (SAVED INTO inv_specs TEXT) ===
        Dim txtSpecValue As New TextBox
        txtSpecValue.PlaceholderText = "Specs Value (e.g. Intel i5)"
        txtSpecValue.Name = "txtSpecValue"
        txtSpecValue.Location = New Point(txtSpecName.Right + 10, 5)
        txtSpecValue.Width = fieldPanel.Width - txtSpecName.Width - 120

        ' === Remove Button ===
        Dim removeButton As New Button
        removeButton.Text = "X"
        removeButton.Width = 30
        removeButton.Height = 25
        removeButton.Location = New Point(fieldPanel.Width - removeButton.Width - 5, 5)
        AddHandler removeButton.Click, AddressOf RemoveSpecField

        ' Add controls to panel
        fieldPanel.Controls.Add(txtSpecName)
        fieldPanel.Controls.Add(txtSpecValue)
        fieldPanel.Controls.Add(removeButton)

        ' Add to flow panel
        flowpnl.Controls.Add(fieldPanel)

        ' Resize handling
        AddHandler flowpnl.Resize,
        Sub()
            fieldPanel.Width = flowpnl.ClientSize.Width - 2
            removeButton.Location = New Point(fieldPanel.Width - removeButton.Width - 5, 5)
            txtSpecValue.Width = fieldPanel.Width - txtSpecName.Width - 120
        End Sub
    End Sub


    Private Sub btnInsertSpecs_Click(sender As Object, e As EventArgs) Handles btnInsertSpecs.Click
        If catcb1.SelectedIndex = -1 Then
            MessageBox.Show("Please select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim drv As DataRowView = CType(catcb1.SelectedItem, DataRowView)
        Dim categoryId As Integer = Convert.ToInt32(drv("pointer"))
        Dim createdBy As Integer = If(Session.LoggedInUserPointer > 0, Session.LoggedInUserPointer, 360)

        Dim specsList As New List(Of String)()

        ' === Loop through each dynamic row ===
        For Each pnl As Control In flowpnl.Controls
            If TypeOf pnl Is Panel Then

                Dim specName As String = ""
                Dim specValue As String = ""

                For Each innerCtrl As Control In pnl.Controls
                    If TypeOf innerCtrl Is TextBox Then
                        If innerCtrl.Name = "txtSpecName" Then
                            specName = innerCtrl.Text.Trim()
                        ElseIf innerCtrl.Name = "txtSpecValue" Then
                            specValue = innerCtrl.Text.Trim()
                        End If
                    End If
                Next

                ' Skip empty rows
                If specName = "" Then Continue For

                ' === DUPLICATE CHECK → OPTION A ===
                If Not db.IsCategorySpecExist(categoryId, specName) Then
                    db.InsertCategorySpec(categoryId, specName, createdBy)
                End If

                ' === Build "Processor: Intel i5" string ===
                Dim oneSpec As String = specName & If(specValue = "", ":", ": " & specValue)
                specsList.Add(oneSpec)

            End If
        Next

        ' === Merge specs to a single string ===
        Dim combinedSpecs As String = String.Join("; ", specsList)

        ' === INSERT or UPDATE inv_specs ===
        If editingSpecsId > 0 Then
            If db.UpdateSpecs(editingSpecsId, categoryId, combinedSpecs) Then
                MessageBox.Show("Specifications updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to update specifications.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            If db.InsertSpecs(categoryId, combinedSpecs, createdBy) Then
                MessageBox.Show("Specifications inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to insert specifications.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        ' Reset form
        flowpnl.Controls.Clear()
        editingSpecsId = 0
        btnInsertSpecs.Text = "Insert Specs"

        LoadSpecsDGV()
    End Sub

    Private Sub RemoveSpecField(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        flowpnl.Controls.Remove(btn.Parent)
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
            Dim brandName As String = selectedRow.Cells("brand_name").Value.ToString()

            If branddgv.Columns(e.ColumnIndex).Name = "Edit" Then
                If MessageBox.Show($"Are you sure you want to edit the brand: {brandName}?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    brandtxt.Text = selectedRow.Cells("brand_name").Value.ToString()
                    catcb.SelectedValue = Convert.ToInt32(selectedRow.Cells("category_pointer").Value)
                    editingBrandId = brandId
                    brandbtn.Text = "Update Brand"
                End If
            ElseIf branddgv.Columns(e.ColumnIndex).Name = "Delete" Then
                If MessageBox.Show($"Are you sure you want to delete the brand: {brandName}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
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
            Dim specsName As String = selectedRow.Cells("specs").Value.ToString()

            If specsdgv.Columns(e.ColumnIndex).Name = "Edit" Then

                If MessageBox.Show($"Are you sure you want to edit the specification: {specsName}?",
                                   "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                    catcb1.SelectedValue = Convert.ToInt32(selectedRow.Cells("category_pointer").Value)
                    Dim specsValue As String = selectedRow.Cells("specs").Value.ToString()

                    flowpnl.Controls.Clear()

                    Dim specPairs() As String = specsValue.Split(";"c)

                    For Each sp In specPairs
                        Dim trimmed As String = sp.Trim()
                        If trimmed = "" Then Continue For

                        Dim parts() As String = trimmed.Split(":"c)
                        Dim name As String = parts(0).Trim()
                        Dim value As String = If(parts.Length > 1, parts(1).Trim(), "")

                        ' === Create panel ===
                        Dim fieldPanel As New Panel
                        fieldPanel.Width = flowpnl.ClientSize.Width - 2
                        fieldPanel.Height = 35
                        fieldPanel.Margin = New Padding(0)

                        ' === Specs Name TextBox ===
                        Dim txtSpecName As New TextBox
                        txtSpecName.Name = "txtSpecName"
                        txtSpecName.Text = name
                        txtSpecName.Width = 150
                        txtSpecName.Location = New Point(5, 5)

                        ' === Specs Value TextBox ===
                        Dim txtSpecValue As New TextBox
                        txtSpecValue.Name = "txtSpecValue"
                        txtSpecValue.Text = value
                        txtSpecValue.Width = fieldPanel.Width - txtSpecName.Width - 120
                        txtSpecValue.Location = New Point(txtSpecName.Right + 10, 5)

                        ' === Remove Button ===
                        Dim removeButton As New Button
                        removeButton.Text = "X"
                        removeButton.Width = 30
                        removeButton.Height = 25
                        removeButton.Location = New Point(fieldPanel.Width - removeButton.Width - 5, 5)
                        AddHandler removeButton.Click, AddressOf RemoveSpecField

                        ' Add controls
                        fieldPanel.Controls.Add(txtSpecName)
                        fieldPanel.Controls.Add(txtSpecValue)
                        fieldPanel.Controls.Add(removeButton)

                        flowpnl.Controls.Add(fieldPanel)

                        AddHandler flowpnl.Resize,
                            Sub()
                                fieldPanel.Width = flowpnl.ClientSize.Width - 2
                                removeButton.Location = New Point(fieldPanel.Width - removeButton.Width - 5, 5)
                                txtSpecValue.Width = fieldPanel.Width - txtSpecName.Width - 120
                            End Sub
                    Next

                    editingSpecsId = specsId
                    btnInsertSpecs.Text = "Update Specs"
                End If
            End If


        End If
    End Sub


    Private Sub btnAddProperty_Click(sender As Object, e As EventArgs)
        Dim rowPanel As New Panel
        rowPanel.Width = propertyflowpnl.ClientSize.Width - 2
        rowPanel.Height = 30
        rowPanel.Margin = New Padding(0, 2, 0, 2)

        ' === TextBox for property name ===
        Dim txtProp As New TextBox
        txtProp.Name = "txtPropertyName"
        txtProp.PlaceholderText = "Property name (e.g. NSOC Name)"
        txtProp.Location = New Point(5, 4)
        txtProp.Width = 220

        ' === Required checkbox ===
        Dim chkReq As New CheckBox
        chkReq.Name = "chkRequired"
        chkReq.Text = "Required"
        chkReq.Checked = True
        chkReq.AutoSize = True
        chkReq.Location = New Point(txtProp.Right + 10, 6)

        ' === Active checkbox ===
        Dim chkAct As New CheckBox
        chkAct.Name = "chkActive"
        chkAct.Text = "Active"
        chkAct.Checked = True
        chkAct.AutoSize = True
        chkAct.Location = New Point(chkReq.Right + 10, 6)

        ' === Remove button ===
        Dim btnRemove As New Button
        btnRemove.Text = "X"
        btnRemove.Width = 25
        btnRemove.Height = 22
        btnRemove.Location = New Point(rowPanel.Width - btnRemove.Width - 5, 4)
        AddHandler btnRemove.Click,
            Sub()
                propertyflowpnl.Controls.Remove(rowPanel)
                rowPanel.Dispose
            End Sub

        rowPanel.Controls.Add(txtProp)
        rowPanel.Controls.Add(chkReq)
        rowPanel.Controls.Add(chkAct)
        rowPanel.Controls.Add(btnRemove)

        propertyflowpnl.Controls.Add(rowPanel)

        ' Resize behavior
        AddHandler propertyflowpnl.Resize,
            Sub()
                rowPanel.Width = propertyflowpnl.ClientSize.Width - 2
                btnRemove.Location = New Point(rowPanel.Width - btnRemove.Width - 5, 4)
            End Sub
    End Sub

End Class
