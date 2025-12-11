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

    ' ========================  
    ' 🔹 FIELD INFO CLASS  
    ' ========================  
    Public Class FieldInfo
        Public Property Required As Boolean
        Public Property PropName As String
    End Class

    ' ========================  
    ' 🔁 AUTO-RESIZE METHODS  
    ' ========================  
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
            If Not originalBounds.ContainsKey(ctrl) Then originalBounds.Add(ctrl, ctrl.Bounds)
            If ctrl.HasChildren Then StoreOriginalBounds(ctrl)
        Next
    End Sub

    Private Sub ApplyScale(scaleX As Single, scaleY As Single)
        Me.SuspendLayout()
        For Each kvp As KeyValuePair(Of Control, Rectangle) In originalBounds
            Dim ctrl As Control = kvp.Key
            If ctrl Is Nothing OrElse ctrl.IsDisposed Then Continue For

            Dim r As Rectangle = kvp.Value
            ctrl.Bounds = New Rectangle(CInt(r.X * scaleX), CInt(r.Y * scaleY), CInt(r.Width * scaleX), CInt(r.Height * scaleY))

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
        Dim usableWidth As Integer = deviceflowpnl.DisplayRectangle.Width - 10
        If usableWidth <= 0 Then usableWidth = deviceflowpnl.ClientSize.Width - 10

        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                For Each ctrl As Control In row.Controls
                    If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                        ctrl.Width = usableWidth - 160 - 20
                    End If
                Next
            End If
        Next
    End Sub

    ' ========================  
    ' 🔹 FORM LOAD  
    ' ========================  
    Private Sub AddControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        InitializeLayoutScaling()
        LoadCategories()
        quanttxt.Text = "1"
    End Sub

    Private Sub LoadCategories()
        Dim categories = mdl.GetCategories()
        If categories IsNot Nothing AndAlso categories.Count > 0 Then
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

        Dim selectedCategory = TryCast(catcb.SelectedItem, DeviceCategory)
        If selectedCategory Is Nothing Then Return

        Dim props = mdl.GetCategoryProperties(selectedCategory.Pointer)
        Dim activeProps = props.AsEnumerable.Where(Function(r) Convert.ToBoolean(r("active")) = True).ToList

        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True

        If activeProps.Count = 0 Then
            Dim lblNoFields As New Label With {
                .Text = "No fields in this category",
                .AutoSize = True,
                .ForeColor = Color.Red,
                .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold)
            }
            deviceflowpnl.Controls.Add(lblNoFields)
            Return
        End If

        For Each prop In activeProps
            Dim propName = prop("property_name").ToString.ToLower
            Dim isRequired = Convert.ToBoolean(prop("required"))

            Dim rowPanel As New Panel With {.Width = deviceflowpnl.ClientSize.Width - 2, .Height = 40, .Margin = New Padding(0, 0, 0, 6)}

            Dim labelText = prop("property_name").ToString

            ' 👉 Add peso sign to label when this is the Cost field
            If propName = "cost" Then
                labelText &= " (₱)"
            End If

            If isRequired Then
                labelText &= " *"
            End If

            Dim lbl As New Label With {
            .Text = labelText,
            .AutoSize = False,
            .Width = 160,
            .Location = New Point(5, 8),
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .ForeColor = If(isRequired, Color.Red, Color.Black)
        }


            Dim inputCtrl As Control
            If propName = "brand" Then
                Dim cb As New ComboBox With {
                    .Name = "cb_" & prop("pointer").ToString,
                    .DropDownStyle = ComboBoxStyle.DropDownList,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
                }
                Dim brands = mdl.GetBrandsByCategory(selectedCategory.Pointer)
                If brands IsNot Nothing AndAlso brands.Count > 0 Then
                    cb.DataSource = brands
                    cb.DisplayMember = "BrandName"
                    cb.ValueMember = "Pointer"
                    cb.SelectedIndex = -1
                End If
                inputCtrl = cb
            Else
                Dim txt As New TextBox With {
            .Name = "txt_" & prop("pointer").ToString,
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .Height = 26
        }

                ' 👉 If this is the Cost field, format & validate as numeric
                If propName = "cost" Then
                    txt.TextAlign = HorizontalAlignment.Left
                    AddHandler txt.KeyPress, AddressOf CostTextBox_KeyPress
                    AddHandler txt.Leave, AddressOf CostTextBox_Leave
                End If

                inputCtrl = txt
            End If


            ' Store field info for validation
            inputCtrl.Tag = New FieldInfo With {.Required = isRequired, .PropName = prop("property_name").ToString}

            inputCtrl.Width = rowPanel.Width - lbl.Width - 20
            inputCtrl.Location = New Point(lbl.Right + 10, 5)
            inputCtrl.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top

            rowPanel.Controls.Add(lbl)
            rowPanel.Controls.Add(inputCtrl)
            deviceflowpnl.Controls.Add(rowPanel)

            AddHandler deviceflowpnl.Resize, Sub()
                                                 rowPanel.Width = deviceflowpnl.ClientSize.Width - 2
                                                 inputCtrl.Width = rowPanel.Width - lbl.Width - 20
                                             End Sub
        Next

        AdjustDynamicControlsWidth()
        LoadSpecsForCategory(selectedCategory.Pointer)
    End Sub


    ' ========================
    ' 🔹 COST TEXTBOX HANDLERS
    ' ========================
    Private Sub CostTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim txt As TextBox = DirectCast(sender, TextBox)

        ' Allow control keys (Backspace, Delete, arrows, etc.)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        ' Allow digits
        If Char.IsDigit(e.KeyChar) Then
            Return
        End If

        ' Allow one decimal separator (.)
        If e.KeyChar = "."c Then
            If txt.Text.Contains("."c) Then
                e.Handled = True
            End If
            Return
        End If

        ' Block anything else
        e.Handled = True
    End Sub

    Private Sub CostTextBox_Leave(sender As Object, e As EventArgs)
        Dim txt As TextBox = DirectCast(sender, TextBox)
        If String.IsNullOrWhiteSpace(txt.Text) Then Exit Sub

        Dim amount As Decimal
        If Decimal.TryParse(txt.Text, amount) Then
            ' Format as 2 decimal places when leaving the field
            txt.Text = amount.ToString("N2") ' e.g. 1,234.50 based on current culture
        Else
            MessageBox.Show("Please enter a valid amount for Cost.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txt.Focus()
        End If
    End Sub




    ' ========================  
    ' 🔹 LOAD SPECS  
    ' ========================  
    Private Sub LoadSpecsForCategory(categoryPointer As Integer)
        Dim specs = mdl.GetSpecificationsForCategory(categoryPointer)
        If specs IsNot Nothing AndAlso specs.Count > 0 Then
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

    ' ========================  
    ' 🔹 SPECS SELECTED  
    ' ========================  
    Private Const SPECS_LABEL_WIDTH As Integer = 140
    Private Const SPECS_LEFT_PADDING As Integer = 5
    Private Const SPECS_GAP As Integer = 5

    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles specscb.SelectedIndexChanged
        specsflowpnl.Controls.Clear()
        Dim selectedSpec As DeviceSpecification = TryCast(specscb.SelectedItem, DeviceSpecification)
        If selectedSpec Is Nothing Then Return

        Dim specsString As String = selectedSpec.SpecName
        If String.IsNullOrWhiteSpace(specsString) Then Return

        For Each spec In specsString.Split(";"c)
            If String.IsNullOrWhiteSpace(spec) Then Continue For

            Dim parts() As String = spec.Split(":"c)
            Dim labelName As String = parts(0).Trim()
            Dim fieldValue As String = If(parts.Length > 1, parts(1).Trim(), "")

            Dim rowPanel As New Panel With {.Width = specsflowpnl.ClientSize.Width - 2, .Height = 32, .Margin = New Padding(0, 0, 0, 4)}
            Dim lbl As New Label With {.Text = labelName & ":", .AutoSize = False, .Width = SPECS_LABEL_WIDTH}
            Dim txt As New TextBox With {.Text = fieldValue, .Tag = labelName}

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
                Dim lbl As Label = Nothing
                Dim txt As TextBox = Nothing

                For Each c As Control In row.Controls
                    If TypeOf c Is Label Then lbl = TryCast(c, Label)
                    If TypeOf c Is TextBox Then txt = TryCast(c, TextBox)
                Next

                If lbl IsNot Nothing Then lbl.Location = New Point(SPECS_LEFT_PADDING, (row.Height - lbl.Height) \ 2)
                If txt IsNot Nothing Then
                    txt.Location = New Point(textLeft, (row.Height - txt.Height) \ 2)
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

        ' ===== CATEGORY CHECK =====
        If catcb.SelectedIndex < 0 Then
            MessageBox.Show("Select a category first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim userID As Integer = Session.LoggedInUserPointer
        Dim db As New model()
        Dim quantity As Integer = If(Integer.TryParse(quanttxt.Text, 0), CInt(quanttxt.Text), 1)
        Dim selectedCategory As DeviceCategory = TryCast(catcb.SelectedItem, DeviceCategory)
        If selectedCategory Is Nothing Then Return

        ' ===== VALIDATE REQUIRED FIELDS =====
        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                For Each ctrl As Control In row.Controls
                    If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                        Dim info As FieldInfo = TryCast(ctrl.Tag, FieldInfo)
                        If info IsNot Nothing AndAlso info.Required Then
                            Dim hasValue As Boolean = False
                            If TypeOf ctrl Is TextBox Then hasValue = Not String.IsNullOrWhiteSpace(DirectCast(ctrl, TextBox).Text)
                            If TypeOf ctrl Is ComboBox Then hasValue = DirectCast(ctrl, ComboBox).SelectedIndex >= 0

                            If Not hasValue Then
                                MessageBox.Show(info.PropName & " is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                ctrl.Focus()
                                Return
                            End If
                        End If
                    End If
                Next
            End If
        Next

        ' ===== LOOP TO SAVE DEVICES =====
        For i As Integer = 1 To quantity
            Dim device As New InvDevice With {
            .DevCategoryPointer = selectedCategory.Pointer,
            .Notes = If(notetxt.Text.Trim() <> "", notetxt.Text.Trim(), Nothing),
            .Status = "Working",
            .Ass_Status = "Unassigned",
            .Unit_Status = "Unassigned",   ' 👈 NEW: unit_status column
            .PurchaseDate = purchaseDatePicker.Value,
            .WarrantyExpires = warrantyDatePicker.Value
        }

            ' ===== SPECS FIELDS =====
            Dim specList As New List(Of String)

            If specscb.SelectedIndex >= 0 AndAlso TypeOf specscb.SelectedItem Is DeviceSpecification Then

                For Each row As Control In specsflowpnl.Controls
                    If TypeOf row Is Panel Then
                        Dim lbl As Label = Nothing
                        Dim txt As TextBox = Nothing

                        For Each c As Control In row.Controls
                            If TypeOf c Is Label Then lbl = TryCast(c, Label)
                            If TypeOf c Is TextBox Then txt = TryCast(c, TextBox)
                        Next

                        If lbl IsNot Nothing AndAlso txt IsNot Nothing Then
                            specList.Add(lbl.Text.TrimEnd(":"c) & ": " & txt.Text.Trim())
                        End If
                    End If
                Next

                If specList.Count > 0 Then
                    device.Specs = String.Join("; ", specList)
                Else
                    device.Specs = Nothing
                End If
            Else
                device.Specs = Nothing
            End If

            ' ===== DEVICEFLOWPANEL FIELDS =====
            For Each row As Control In deviceflowpnl.Controls
                If TypeOf row Is Panel Then
                    For Each ctrl As Control In row.Controls
                        If TypeOf ctrl Is TextBox Then
                            Dim propPointer As Integer = Convert.ToInt32(ctrl.Name.Replace("txt_", ""))
                            Dim value As String = ctrl.Text.Trim()
                            Select Case mdl.GetCategoryPropertyName(propPointer).ToLower()
                                Case "model" : device.Model = If(value <> "", value, Nothing)
                                Case "serial number" : device.SerialNumber = If(value <> "", value, Nothing)
                                Case "property number" : device.PropertyNumber = If(value <> "", value, Nothing)
                                Case "nsoc name" : device.NsocName = If(value <> "", value, Nothing)
                                Case "cost"
                                    If value <> "" Then
                                        Dim amount As Decimal
                                        If Decimal.TryParse(value, amount) Then
                                            device.Cost = amount
                                        Else
                                            MessageBox.Show("Invalid amount for Cost.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                            ctrl.Focus()
                                            Return
                                        End If
                                    Else
                                        device.Cost = Nothing
                                    End If
                            End Select
                        ElseIf TypeOf ctrl Is ComboBox Then
                            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
                            Dim selectedBrand As Brand = TryCast(cb.SelectedItem, Brand)
                            If selectedBrand IsNot Nothing Then device.BrandPointer = selectedBrand.Pointer
                        End If
                    Next
                End If
            Next

            db.Save(device, userID)
        Next

        MessageBox.Show(quantity.ToString() & " device(s) added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' ===== RESET FORM =====
        catcb.SelectedIndex = -1
        deviceflowpnl.Controls.Clear()
        specscb.DataSource = Nothing
        specsflowpnl.Controls.Clear()
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

    ' ========================  
    ' 🔹 AUTO RESIZE EVENTS  
    ' ========================  
    Private Sub deviceflowpnl_SizeChanged(sender As Object, e As EventArgs) Handles deviceflowpnl.SizeChanged
        AdjustDynamicControlsWidth()
    End Sub

    Private Sub deviceflowpnl_Layout(sender As Object, e As LayoutEventArgs) Handles deviceflowpnl.Layout
        AdjustDynamicControlsWidth()
    End Sub


End Class