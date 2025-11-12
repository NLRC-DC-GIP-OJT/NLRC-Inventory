Public Class Edit
    Private mdl As model
    Private currentDevice As InvDevice

    ' Constructor
    Public Sub New()
        InitializeComponent()
        Me.mdl = mdl
    End Sub
    Public Sub New(mdl As model)
        InitializeComponent()
        Me.mdl = mdl
    End Sub

    ' Load a device for editing
    Public Sub LoadDevice(device As InvDevice)
        Try
            currentDevice = device

            ' Load dropdowns
            LoadCategories()
            LoadBrands()

            ' Fill device info
            modeltxt.Text = device.Model
            serialtxt.Text = If(String.IsNullOrEmpty(device.SerialNumber), "", device.SerialNumber)
            purchaseDatePicker.Value = device.PurchaseDate.GetValueOrDefault(DateTime.Now)
            warrantyDatePicker.Value = device.WarrantyExpires.GetValueOrDefault(DateTime.Now)
            notetxt.Text = device.Notes

            ' Set category and brand combo boxes
            If catcb.Items.Count > 0 Then catcb.SelectedValue = device.DevCategoryPointer
            If brandcb.Items.Count > 0 Then brandcb.SelectedValue = device.BrandPointer

            ' Load specs for this category
            If catcb.SelectedIndex >= 0 Then
                Dim categoryID As Integer = Convert.ToInt32(catcb.SelectedValue)
                Dim specs = mdl.GetSpecificationsForCategory(categoryID)

                ' Load specs into ComboBox
                specscb.DataSource = specs
                specscb.DisplayMember = "SpecName"   ' 
                specscb.ValueMember = "Pointer"

                ' Show the actual spec text in the TextBox
                If Not String.IsNullOrEmpty(device.Specs) Then
                    Dim specPointer As Integer
                    If Integer.TryParse(device.Specs, specPointer) Then
                        Dim matchSpec = specs.Find(Function(s) s.Pointer = specPointer)
                        If matchSpec IsNot Nothing Then
                            specstxt.Text = matchSpec.SpecName
                            specscb.SelectedItem = matchSpec
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading device: " & ex.Message)
        End Try
    End Sub

    ' Load Categories
    Private Sub LoadCategories()
        Dim categories = mdl.GetCategories()
        If categories IsNot Nothing AndAlso categories.Count > 0 Then
            catcb.DataSource = categories
            catcb.DisplayMember = "CategoryName"
            catcb.ValueMember = "Pointer"

            ' Enable autocomplete
            catcb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            catcb.AutoCompleteSource = AutoCompleteSource.ListItems
        End If
    End Sub

    ' Load Brands
    Private Sub LoadBrands()
        Dim brands = mdl.GetBrands()
        If brands IsNot Nothing AndAlso brands.Count > 0 Then
            brandcb.DataSource = brands
            brandcb.DisplayMember = "BrandName"
            brandcb.ValueMember = "Pointer"

            ' Enable autocomplete
            brandcb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            brandcb.AutoCompleteSource = AutoCompleteSource.ListItems
        End If
    End Sub

    ' When category changes, load specs
    Private Sub catcb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catcb.SelectedIndexChanged
        Dim selectedCategory = TryCast(catcb.SelectedItem, DeviceCategory)
        If selectedCategory IsNot Nothing Then
            LoadSpecsForCategory(selectedCategory.Pointer)
        End If
    End Sub

    ' Load specs for a category
    Private Sub LoadSpecsForCategory(categoryPointer As Integer)
        Dim specs As List(Of DeviceSpecification) = mdl.GetSpecificationsForCategory(categoryPointer)

        If specs IsNot Nothing AndAlso specs.Count > 0 Then
            specscb.DataSource = specs
            specscb.DisplayMember = "Specs"
            specscb.ValueMember = "Pointer"

            ' Preselect spec matching device's Specs pointer
            If currentDevice IsNot Nothing AndAlso Not String.IsNullOrEmpty(currentDevice.Specs) Then
                Dim specID As Integer
                If Integer.TryParse(currentDevice.Specs, specID) Then
                    For Each spec As DeviceSpecification In specs
                        If spec.Pointer = specID Then
                            specscb.SelectedItem = spec
                            specstxt.Text = spec.SpecName
                            Exit For
                        End If
                    Next
                End If
            End If

        Else
            specscb.DataSource = Nothing
            specscb.Items.Clear()
            specscb.Items.Add("No specs available")
            specscb.SelectedIndex = 0
            specstxt.Clear()
        End If
    End Sub

    ' Update specstxt when user selects spec
    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles specscb.SelectedIndexChanged
        Dim selectedSpec = TryCast(specscb.SelectedItem, DeviceSpecification)
        If selectedSpec IsNot Nothing Then
            specstxt.Text = selectedSpec.SpecName
        Else
            specstxt.Clear()
        End If
    End Sub

    Private Sub savebtn_Click_1(sender As Object, e As EventArgs) Handles savebtn.Click
        Try
            If currentDevice Is Nothing Then
                MessageBox.Show("No device loaded for editing.")
                Exit Sub
            End If

            ' Update device fields
            Dim selectedCategory = TryCast(catcb.SelectedItem, DeviceCategory)
            Dim selectedBrand = TryCast(brandcb.SelectedItem, Brand)

            If selectedCategory IsNot Nothing Then currentDevice.DevCategoryPointer = selectedCategory.Pointer
            If selectedBrand IsNot Nothing Then currentDevice.BrandPointer = selectedBrand.Pointer

            currentDevice.Model = modeltxt.Text.Trim
            currentDevice.SerialNumber = serialtxt.Text.Trim
            currentDevice.PurchaseDate = purchaseDatePicker.Value
            currentDevice.WarrantyExpires = warrantyDatePicker.Value
            currentDevice.Notes = notetxt.Text.Trim

            ' Save selected spec pointer
            Dim selectedSpec = TryCast(specscb.SelectedItem, DeviceSpecification)
            If selectedSpec Is Nothing Then
                MessageBox.Show("Please select a valid specification.", "Missing Specification", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            currentDevice.Specs = selectedSpec.Pointer.ToString()

            ' 🔹 FIX: pass logged-in user ID
            Dim userID As Integer = Session.LoggedInUserPointer
            If mdl.Save(currentDevice, userID) Then
                MessageBox.Show("Device updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim parentPanel = TryCast(Parent, Panel)
                If parentPanel IsNot Nothing Then parentPanel.Visible = False
            Else
                MessageBox.Show("Error updating device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Save error: " & ex.Message)
        End Try
    End Sub


    Private Sub cancelbtn_Click_1(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Dim parentPanel = TryCast(Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
