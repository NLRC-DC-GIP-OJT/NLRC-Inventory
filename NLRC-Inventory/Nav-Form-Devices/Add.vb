Public Class Add

    Dim mdl As New model()

    ' 🔹 Renamed to avoid conflict with UserControl.Load
    Private Sub AddControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategories()

        ' Set default message for brand combo
        brandcb.DataSource = Nothing
        brandcb.Items.Clear()
        brandcb.Items.Add("Select a category first")
        brandcb.SelectedIndex = 0

        quanttxt.Text = "1"
    End Sub


    ' 🔹 Load categories from the model
    Private Sub LoadCategories()
        Dim db As New model()
        Dim categories = db.GetCategories()

        If categories.Count > 0 Then
            catcb.DataSource = categories
            catcb.DisplayMember = "CategoryName"
            catcb.ValueMember = "Pointer"
            catcb.SelectedIndex = -1

            ' Enable autocomplete
            catcb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            catcb.AutoCompleteSource = AutoCompleteSource.ListItems


        Else
            catcb.DataSource = Nothing
            catcb.Items.Clear()
            catcb.Items.Add("No categories available")
            catcb.SelectedIndex = 0
        End If
    End Sub

    ' 🔹 Load brands from the model
    Private Sub LoadBrands(categoryPointer As Integer)
        Dim db As New model()
        Dim brands = db.GetBrandsByCategory(categoryPointer)

        If brands.Count > 0 Then
            brandcb.DataSource = brands
            brandcb.DisplayMember = "BrandName"
            brandcb.ValueMember = "Pointer"
            brandcb.SelectedIndex = -1

            ' Enable autocomplete
            brandcb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            brandcb.AutoCompleteSource = AutoCompleteSource.ListItems
        Else
            brandcb.DataSource = Nothing
            brandcb.Items.Clear()
            brandcb.Items.Add("No brands available for this category")
            brandcb.SelectedIndex = 0
        End If
    End Sub


    ' 🔹 Plus button click
    Private Sub plusBtn_Click(sender As Object, e As EventArgs) Handles plusBtn.Click
        Dim quantity As Integer = Convert.ToInt32(quanttxt.Text)
        quantity += 1
        quanttxt.Text = quantity.ToString()
    End Sub

    ' 🔹 Minus button click
    Private Sub minusBtn_Click(sender As Object, e As EventArgs) Handles minusBtn.Click
        Dim quantity As Integer = Convert.ToInt32(quanttxt.Text)
        If quantity > 1 Then
            quantity -= 1
            quanttxt.Text = quantity.ToString()
        End If
    End Sub

    ' 🔹 Add device button click
    Private Sub addsbtn_Click(sender As Object, e As EventArgs) Handles addsbtn.Click
        ' Validate inputs
        If catcb.SelectedIndex = -1 OrElse brandcb.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(modeltxt.Text) Then
            MessageBox.Show("Please fill out Category, Brand, and Model before adding.",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Get quantity
        Dim quantity As Integer
        If Not Integer.TryParse(quanttxt.Text, quantity) OrElse quantity <= 0 Then
            MessageBox.Show("Please enter a valid quantity greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Ensure spec selection
        Dim selectedSpecPointer As Integer? = If(specscb.SelectedValue IsNot Nothing, CType(specscb.SelectedValue, Integer), CType(Nothing, Integer?))
        If selectedSpecPointer Is Nothing Then
            MessageBox.Show("Please select a valid specification.", "Missing Specification", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Create device object
        Dim device As New InvDevice With {
            .DevCategoryPointer = catcb.SelectedValue,
            .BrandPointer = brandcb.SelectedValue,
            .Model = modeltxt.Text.Trim(),
            .Specs = selectedSpecPointer.ToString(),
            .Notes = notetxt.Text.Trim(),
            .PurchaseDate = purchaseDatePicker.Value,
            .WarrantyExpires = warrantyDatePicker.Value,
            .Status = "Working"
        }

        ' 🔹 Save the device for the given quantity with userID
        Dim db As New model()
        Dim userID As Integer = Session.LoggedInUserPointer ' <-- FIXED

        For i As Integer = 1 To quantity
            If db.Save(device, userID) Then
                Console.WriteLine("Device inserted: " & i)
            End If
        Next

        MessageBox.Show(quantity.ToString() & " devices added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear form fields
        catcb.SelectedIndex = -1
        brandcb.SelectedIndex = -1
        modeltxt.Clear()
        specstxt.Clear()
        notetxt.Clear()
        purchaseDatePicker.Value = DateTime.Now
        warrantyDatePicker.Value = DateTime.Now
    End Sub

    Private Sub LoadSpecsForCategory(categoryPointer As Integer)
        Dim specs = mdl.GetSpecificationsForCategory(categoryPointer)

        If specs.Count > 0 Then
            specscb.DataSource = specs
            specscb.DisplayMember = "SpecName"
            specscb.ValueMember = "Pointer"
            specscb.SelectedIndex = -1
        Else
            specscb.DataSource = Nothing
            specscb.Items.Clear()
            specscb.Items.Add("No specs available")
            specscb.SelectedIndex = 0
        End If
    End Sub


    Private Sub catcb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catcb.SelectedIndexChanged
        If catcb.SelectedIndex >= 0 Then
            Dim selectedCategory = CType(catcb.SelectedItem, DeviceCategory)
            Dim selectedCategoryPointer = selectedCategory.Pointer

            ' Load specs
            LoadSpecsForCategory(selectedCategoryPointer)

            ' Load brands filtered by category
            LoadBrands(selectedCategoryPointer)
        Else
            ' Reset brands if no category selected
            brandcb.DataSource = Nothing
            brandcb.Items.Clear()
            brandcb.Items.Add("Select a category first")
            brandcb.SelectedIndex = 0
        End If
    End Sub



    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles specscb.SelectedIndexChanged
        If specscb.SelectedIndex >= 0 AndAlso specscb.SelectedItem IsNot Nothing Then
            If specscb.SelectedItem.ToString() = "No specs available" Then
                specstxt.Clear()
            Else
                Dim selectedSpec As DeviceSpecification = CType(specscb.SelectedItem, DeviceSpecification)
                specstxt.Text = selectedSpec.SpecName
            End If
        Else
            specstxt.Clear()
        End If
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then
            parentPanel.Visible = False
        End If
    End Sub
End Class
