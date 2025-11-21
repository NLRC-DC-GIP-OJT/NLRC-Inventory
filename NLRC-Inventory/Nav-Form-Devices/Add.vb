Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class Add

    Dim mdl As New model()

    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
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
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
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

            If ctrl.Font IsNot Nothing Then
                Dim f As Font = ctrl.Font
                Dim newSize As Single = f.SizeInPoints * Math.Min(scaleX, scaleY)
                If newSize > 4 Then ctrl.Font = New Font(f.FontFamily, newSize, f.Style)
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

        AdjustDynamicControlsWidth()
    End Sub

    Private Sub AdjustDynamicControlsWidth()
        ' Use DisplayRectangle so controls can use full usable width of FlowLayoutPanel
        Dim usableWidth As Integer = deviceflowpnl.DisplayRectangle.Width - 10
        If usableWidth <= 0 Then
            usableWidth = deviceflowpnl.ClientSize.Width - 10
        End If

        For Each ctrl As Control In deviceflowpnl.Controls
            If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                ctrl.Width = usableWidth
            End If
        Next
    End Sub

    ' ========================
    ' 🔹 FORM LOAD
    ' ========================
    Private Sub AddControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill

        ' REMOVED deviceflowpnl.Dock = DockStyle.Fill so it doesn't cover catcb

        InitializeLayoutScaling()
        LoadCategories()
        quanttxt.Text = "1"
    End Sub

    Private ReadOnly Property CategoriesWithPropertyNumbers As List(Of String)
        Get
            Return New List(Of String) From {"Desktop", "Laptop", "Monitor", "Printer"}
        End Get
    End Property

    ' ========================
    ' 🔹 LOAD CATEGORIES
    ' ========================
    Private Sub LoadCategories()
        Dim categories = mdl.GetCategories()
        If categories.Count > 0 Then
            catcb.DataSource = categories
            catcb.DisplayMember = "CategoryName"
            catcb.ValueMember = "Pointer"
            catcb.SelectedIndex = -1
            catcb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            catcb.AutoCompleteSource = AutoCompleteSource.ListItems
        Else
            catcb.DataSource = Nothing
            catcb.Items.Clear()
            catcb.Items.Add("No categories available")
            catcb.SelectedIndex = 0
        End If
    End Sub

    ' ========================
    ' 🔹 PLUS / MINUS QUANTITY
    ' ========================
    Private Sub plusBtn_Click(sender As Object, e As EventArgs) Handles plusBtn.Click
        Dim quantity As Integer = Convert.ToInt32(quanttxt.Text)
        quantity += 1
        quanttxt.Text = quantity.ToString()
    End Sub

    Private Sub minusBtn_Click(sender As Object, e As EventArgs) Handles minusBtn.Click
        Dim quantity As Integer = Convert.ToInt32(quanttxt.Text)
        If quantity > 1 Then
            quantity -= 1
            quanttxt.Text = quantity.ToString()
        End If
    End Sub

    ' ========================
    ' 🔹 CATEGORY CHANGED
    ' ========================
    Private Sub catcb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catcb.SelectedIndexChanged
        deviceflowpnl.Controls.Clear()
        If catcb.SelectedIndex < 0 Then Return

        Dim selectedCategory As DeviceCategory = CType(catcb.SelectedItem, DeviceCategory)
        Dim categoryPointer As Integer = selectedCategory.Pointer

        Dim props As DataTable = mdl.GetCategoryProperties(categoryPointer)

        ' 🔥 FIX: Correct filtering regardless of 0/1 vs True/False
        Dim activeProps = props.AsEnumerable().
        Where(Function(r) Convert.ToBoolean(r("active")) = True).
        ToList()

        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True

        ' 🔥 If no ACTIVE properties exist
        If activeProps.Count = 0 Then
            Dim lblNoFields As New Label()
            lblNoFields.Text = "No fields in this category"
            lblNoFields.AutoSize = True
            lblNoFields.ForeColor = Color.Red
            lblNoFields.Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
            deviceflowpnl.Controls.Add(lblNoFields)
            Return
        End If

        ' 🔥 Show ONLY active properties
        For Each prop As DataRow In activeProps
            Dim propName As String = prop("property_name").ToString().ToLower()

            Dim rowPanel As New Panel()
            rowPanel.Width = deviceflowpnl.ClientSize.Width - 2
            rowPanel.Height = 40
            rowPanel.Margin = New Padding(0, 0, 0, 6)

            Dim lbl As New Label()
            lbl.Text = prop("property_name").ToString() & ":"
            lbl.AutoSize = False
            lbl.Width = 160
            lbl.Location = New Point(5, 8)
            lbl.TextAlign = ContentAlignment.MiddleLeft
            lbl.Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)

            Dim inputCtrl As Control

            If propName = "brand" Then
                Dim cb As New ComboBox()
                cb.Name = "cb_" & prop("pointer").ToString()
                cb.DropDownStyle = ComboBoxStyle.DropDownList
                cb.Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)

                Dim brands = mdl.GetBrandsByCategory(categoryPointer)
                If brands.Count > 0 Then
                    cb.DataSource = brands
                    cb.DisplayMember = "BrandName"
                    cb.ValueMember = "Pointer"
                    cb.SelectedIndex = -1
                End If

                inputCtrl = cb
            Else
                Dim txt As New TextBox()
                txt.Name = "txt_" & prop("pointer").ToString()
                txt.Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
                inputCtrl = txt
            End If

            inputCtrl.Width = rowPanel.Width - lbl.Width - 20
            inputCtrl.Location = New Point(lbl.Right + 10, 5)
            inputCtrl.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top

            rowPanel.Controls.Add(lbl)
            rowPanel.Controls.Add(inputCtrl)
            deviceflowpnl.Controls.Add(rowPanel)

            AddHandler deviceflowpnl.Resize,
        Sub()
            rowPanel.Width = deviceflowpnl.ClientSize.Width - 2
            inputCtrl.Width = rowPanel.Width - lbl.Width - 20
        End Sub
        Next

        AdjustDynamicControlsWidth()
        LoadSpecsForCategory(categoryPointer)
    End Sub





    ' ========================
    ' 🔹 LOAD SPECS
    ' ========================
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



    ' put these at the top of your form (class level)
    Private Const SPECS_LABEL_WIDTH As Integer = 140
    Private Const SPECS_LEFT_PADDING As Integer = 5
    Private Const SPECS_GAP As Integer = 5

    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles specscb.SelectedIndexChanged
        specsflowpnl.Controls.Clear()

        If specscb.SelectedIndex < 0 OrElse specscb.SelectedItem Is Nothing Then Exit Sub

        Dim selectedSpec As DeviceSpecification = CType(specscb.SelectedItem, DeviceSpecification)
        Dim specsString As String = selectedSpec.SpecName
        If String.IsNullOrWhiteSpace(specsString) Then Exit Sub

        Dim specsArray() As String = specsString.Split(";"c)

        For Each spec In specsArray
            If String.IsNullOrWhiteSpace(spec) Then Continue For

            Dim parts() As String = spec.Split(":"c)
            Dim labelName As String = parts(0).Trim()
            Dim fieldValue As String = If(parts.Length > 1, parts(1).Trim(), "")

            ' one row container
            Dim rowPanel As New Panel With {
            .Width = specsflowpnl.ClientSize.Width - 2,
            .Height = 32,
            .Margin = New Padding(0, 0, 0, 4)
        }

            ' label
            Dim lbl As New Label With {
            .Text = labelName & ":",
            .AutoSize = False,
            .Width = SPECS_LABEL_WIDTH
        }

            ' textbox
            Dim txt As New TextBox With {
            .Text = fieldValue,
            .Tag = labelName
        }

            rowPanel.Controls.Add(lbl)
            rowPanel.Controls.Add(txt)
            specsflowpnl.Controls.Add(rowPanel)
        Next

        LayoutSpecsRows()
    End Sub


    Private Sub LayoutSpecsRows()
        Dim totalWidth As Integer = specsflowpnl.ClientSize.Width - 2
        Dim textLeft As Integer = SPECS_LEFT_PADDING + SPECS_LABEL_WIDTH + SPECS_GAP

        For Each row As Control In specsflowpnl.Controls
            If TypeOf row Is Panel Then
                Dim rowPanel As Panel = DirectCast(row, Panel)
                rowPanel.Width = totalWidth

                Dim lbl As Label = Nothing
                Dim txt As TextBox = Nothing

                For Each c As Control In rowPanel.Controls
                    If TypeOf c Is Label Then lbl = DirectCast(c, Label)
                    If TypeOf c Is TextBox Then txt = DirectCast(c, TextBox)
                Next

                If lbl IsNot Nothing Then
                    lbl.Location = New Point(SPECS_LEFT_PADDING, (rowPanel.Height - lbl.Height) \ 2)
                End If

                If txt IsNot Nothing Then
                    txt.Location = New Point(textLeft, (rowPanel.Height - txt.Height) \ 2)
                    txt.Width = totalWidth - textLeft - SPECS_LEFT_PADDING
                End If
            End If
        Next
    End Sub


    Private Sub specsflowpnl_Resize(sender As Object, e As EventArgs) Handles specsflowpnl.Resize
        LayoutSpecsRows()
    End Sub

    ' ========================
    ' 🔹 SAVE DEVICE
    ' ========================
    Private Sub addsbtn_Click(sender As Object, e As EventArgs) Handles addsbtn.Click
        If catcb.SelectedIndex < 0 Then
            MessageBox.Show("Select a category first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim userID As Integer = Session.LoggedInUserPointer
        Dim db As New model()
        Dim quantity As Integer = If(Integer.TryParse(quanttxt.Text, 0), CInt(quanttxt.Text), 1)
        Dim selectedCategory As DeviceCategory = CType(catcb.SelectedItem, DeviceCategory)
        Dim selectedSpecPointer As Integer? = If(specscb.SelectedValue IsNot Nothing, CType(specscb.SelectedValue, Integer), CType(Nothing, Integer?))

        For i As Integer = 1 To quantity

            Dim device As New InvDevice With {
                .DevCategoryPointer = selectedCategory.Pointer,
                .Specs = If(selectedSpecPointer.HasValue, selectedSpecPointer.ToString(), Nothing),
                .Notes = If(notetxt.Text.Trim() <> "", notetxt.Text.Trim(), Nothing),
                .Status = "Working",
                .PurchaseDate = purchaseDatePicker.Value,
                .WarrantyExpires = warrantyDatePicker.Value
            }

            Dim specList As New List(Of String)

            For Each row As Control In specsflowpnl.Controls
                If TypeOf row Is Panel Then
                    Dim lbl As Label = Nothing
                    Dim txt As TextBox = Nothing

                    For Each c As Control In row.Controls
                        If TypeOf c Is Label Then lbl = CType(c, Label)
                        If TypeOf c Is TextBox Then txt = CType(c, TextBox)
                    Next

                    If lbl IsNot Nothing Then
                        Dim labelName As String = lbl.Text.Trim().TrimEnd(":"c)
                        Dim value As String = txt.Text.Trim()
                        specList.Add(labelName & ": " & value)
                    End If
                End If
            Next

            ' Combine parts back into DB string
            Dim finalSpecs As String = String.Join("; ", specList)
            device.Specs = finalSpecs


            ' ===========================================
            ' FIXED: LOOP PANELS → THEN LOOP INSIDE THEM
            ' ===========================================
            For Each row As Control In deviceflowpnl.Controls

                If TypeOf row Is Panel Then

                    For Each ctrl As Control In row.Controls

                        ' ===== Textboxes (Model, Serial, Property, NSOC) =====
                        If TypeOf ctrl Is TextBox Then
                            Dim propPointer As Integer = Convert.ToInt32(ctrl.Name.Replace("txt_", ""))
                            Dim value As String = ctrl.Text.Trim()

                            Select Case mdl.GetCategoryPropertyName(propPointer).ToLower()
                                Case "model" : device.Model = If(value <> "", value, Nothing)
                                Case "serial number" : device.SerialNumber = If(value <> "", value, Nothing)
                                Case "property number" : device.PropertyNumber = If(value <> "", value, Nothing)
                                Case "nsoc name" : device.NsocName = If(value <> "", value, Nothing)
                            End Select
                        End If

                        ' ===== ComboBox (Brand) =====
                        If TypeOf ctrl Is ComboBox Then
                            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
                            Dim selectedBrand As Brand = TryCast(cb.SelectedItem, Brand)

                            If selectedBrand IsNot Nothing Then
                                device.BrandPointer = selectedBrand.Pointer
                            End If
                        End If

                    Next

                End If

            Next

            db.Save(device, userID)
        Next

        MessageBox.Show(quantity.ToString() & " device(s) added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        catcb.SelectedIndex = -1
        deviceflowpnl.Controls.Clear()
        specscb.DataSource = Nothing
        notetxt.Clear()
        quanttxt.Text = "1"
    End Sub


    ' ========================
    ' 🔹 CLOSE BUTTON
    ' ========================
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    ' ⭐ AUTO RESIZE WHEN PANEL CHANGES
    Private Sub deviceflowpnl_SizeChanged(sender As Object, e As EventArgs) Handles deviceflowpnl.SizeChanged
        AdjustDynamicControlsWidth()
    End Sub

    ' ⭐ REAL FIX: FORCE WIDTH AFTER FLOWLAYOUT FINISHES LAYOUT ⭐
    Private Sub deviceflowpnl_Layout(sender As Object, e As LayoutEventArgs) Handles deviceflowpnl.Layout
        AdjustDynamicControlsWidth()
    End Sub

End Class
