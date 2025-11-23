Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class Edit

    Private mdl As model
    Private currentDevice As InvDevice
    Private categoryPointer As Integer

    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    ' ========================
    ' 🔹 FORM LOAD
    ' ========================
    Public Sub New(m As model)
        InitializeComponent()
        mdl = m
    End Sub

    Private Sub Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        InitializeLayoutScaling()
    End Sub

    ' ========================
    ' 🔁 AUTO-RESIZE METHODS
    ' ========================
    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        originalSize = Me.Size
        StoreOriginalBounds(Me)
        layoutInitialized = True
    End Sub

    Private Sub StoreOriginalBounds(parent As Control)
        For Each c As Control In parent.Controls
            If Not originalBounds.ContainsKey(c) Then originalBounds(c) = c.Bounds
            If c.HasChildren Then StoreOriginalBounds(c)
        Next
    End Sub

    Private Sub ApplyScale(sx As Single, sy As Single)
        Me.SuspendLayout()
        For Each kvp In originalBounds
            Dim c As Control = kvp.Key
            Dim b As Rectangle = kvp.Value
            c.Bounds = New Rectangle(CInt(b.X * sx), CInt(b.Y * sy), CInt(b.Width * sx), CInt(b.Height * sy))
        Next
        Me.ResumeLayout()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If Not layoutInitialized Then Exit Sub
        ApplyScale(Me.Width / originalSize.Width, Me.Height / originalSize.Height)
    End Sub

    ' ========================
    ' 🔹 LOAD DEVICE
    ' ========================
    Public Sub LoadDevice(device As InvDevice)
        currentDevice = device
        categoryPointer = device.DevCategoryPointer

        LoadCategories()
        LoadSpecs()
        BuildDynamicFields()

        ' CATEGORY COMBOBOX (disabled but keeps value)
        If catcb.Items.Count > 0 Then
            catcb.SelectedValue = If(device.DevCategoryPointer.HasValue, device.DevCategoryPointer.Value, -1)
        End If
        catcb.DropDownStyle = ComboBoxStyle.DropDownList
        catcb.Enabled = False

        ' NOTES
        notetxt.Text = If(device.Notes, "")

        ' DATES
        If device.PurchaseDate.HasValue Then purchaseDatePicker.Value = device.PurchaseDate.Value
        If device.WarrantyExpires.HasValue Then warrantyDatePicker.Value = device.WarrantyExpires.Value

        ' SPECS PANEL
        specsflowpnl.Controls.Clear()
        If Not String.IsNullOrWhiteSpace(device.Specs) Then
            Dim specsArray() As String = device.Specs.Split(";"c)
            For Each spec In specsArray
                If String.IsNullOrWhiteSpace(spec) Then Continue For
                Dim parts() As String = spec.Split(":"c)
                If parts.Length < 2 Then Continue For
                Dim labelName As String = parts(0).Trim()
                Dim fieldValue As String = parts(1).Trim()
                Dim row As New Panel With {.Width = specsflowpnl.ClientSize.Width - 2, .Height = 32, .Margin = New Padding(0, 0, 0, 4)}
                Dim lbl As New Label With {.Text = labelName & ":", .AutoSize = False, .Width = 140, .Location = New Point(5, 8)}
                Dim txt As New TextBox With {.Text = fieldValue, .Tag = labelName}
                row.Controls.Add(lbl)
                row.Controls.Add(txt)
                specsflowpnl.Controls.Add(row)
            Next
        End If
        LayoutSpecsRows()
    End Sub

    ' ========================
    ' 🔹 LOAD CATEGORIES
    ' ========================
    Private Sub LoadCategories()
        Dim categories = mdl.GetCategories()
        If categories IsNot Nothing AndAlso categories.Count > 0 Then
            catcb.DataSource = categories
            catcb.DisplayMember = "CategoryName"
            catcb.ValueMember = "Pointer"
        End If
    End Sub

    ' ========================
    ' 🔹 BUILD DYNAMIC FIELDS
    ' ========================
    Private Sub BuildDynamicFields()
        deviceflowpnl.Controls.Clear()
        Dim props As DataTable = mdl.GetCategoryProperties(categoryPointer)
        Dim activeProps = props.AsEnumerable().Where(Function(r) Convert.ToBoolean(r("active")) = True).ToList()

        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True
        deviceflowpnl.Padding = New Padding(0, 10, 0, 0)

        Dim rowHeight As Integer = 58
        Dim inputHeight As Integer = 30

        For Each prop As DataRow In activeProps
            Dim rawName As String = prop("property_name").ToString()
            Dim propName As String = rawName.Trim().ToLower()
            Dim propPointer As Integer = CInt(prop("pointer"))

            Dim rowPanel As New Panel With {.Width = deviceflowpnl.ClientSize.Width - 2, .Height = rowHeight, .Margin = New Padding(0, 0, 0, 18)}

            Dim lbl As New Label With {
            .Text = rawName & ":",
            .AutoSize = False,
            .Width = 160,
            .Height = rowHeight,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
        }

            Dim inputCtrl As Control

            ' ====================
            ' BRAND FIELD FIXED
            ' ====================
            If propName.Contains("brand") Then
                ' ComboBox
                Dim cb As New ComboBox With {
                .Name = "cb_" & propPointer,
                .DropDownStyle = ComboBoxStyle.DropDownList,
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                .Height = inputHeight,
                .Width = rowPanel.Width - lbl.Width - 20
            }
                cb.Location = New Point(lbl.Right + 10, 5)

                Dim brands = mdl.GetBrandsByCategory(categoryPointer)
                If brands IsNot Nothing AndAlso brands.Count > 0 Then
                    cb.DataSource = brands
                    cb.DisplayMember = "BrandName"
                    cb.ValueMember = "Pointer"
                    cb.SelectedIndex = -1
                End If

                ' TextBox below ComboBox
                Dim txtBrand As New TextBox With {
                .Name = "txtBrand_" & propPointer,
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                .Height = inputHeight,
                .Width = cb.Width,
                .ReadOnly = True
            }
                txtBrand.Location = New Point(cb.Left, cb.Bottom + 4)

                ' Load current brand if exists
                If currentDevice.BrandPointer.HasValue Then
                    Dim b As Brand = brands.FirstOrDefault(Function(x) x.Pointer = currentDevice.BrandPointer.Value)
                    If b IsNot Nothing Then txtBrand.Text = b.BrandName
                End If

                ' ComboBox selection updates TextBox
                AddHandler cb.SelectedIndexChanged, Sub()
                                                        Dim sel As Brand = TryCast(cb.SelectedItem, Brand)
                                                        If sel IsNot Nothing Then txtBrand.Text = sel.BrandName
                                                    End Sub

                ' Adjust panel height
                rowPanel.Height = cb.Height + txtBrand.Height + 10

                rowPanel.Controls.Add(cb)
                rowPanel.Controls.Add(txtBrand)

                inputCtrl = cb

            Else
                ' OTHER FIELDS
                Dim txt As New TextBox With {.Name = "txt_" & propPointer, .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold), .Height = inputHeight}
                Select Case True
                    Case propName.Contains("model") : txt.Text = If(currentDevice?.Model, "")
                    Case propName.Contains("serial") : txt.Text = If(currentDevice?.SerialNumber, "")
                    Case propName.Contains("property") : txt.Text = If(currentDevice?.PropertyNumber, "")
                    Case propName.Contains("nsoc") : txt.Text = If(currentDevice?.NsocName, "")
                    Case propName.Contains("cost") : txt.Text = If(currentDevice?.Cost, 0).ToString()
                    Case propName.Contains("status") : txt.Text = If(currentDevice?.Status, "")
                    Case propName.Contains("notes") : txt.Text = If(currentDevice?.Notes, "")
                    Case Else : txt.Text = ""
                End Select
                inputCtrl = txt
            End If

            inputCtrl.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            rowPanel.Controls.Add(lbl)
            If Not propName.Contains("brand") Then rowPanel.Controls.Add(inputCtrl)
            deviceflowpnl.Controls.Add(rowPanel)

            ' Resize handler
            AddHandler deviceflowpnl.Resize, Sub()
                                                 rowPanel.Width = deviceflowpnl.ClientSize.Width - 2
                                                 inputCtrl.Width = rowPanel.Width - lbl.Width - 20
                                                 inputCtrl.Location = New Point(lbl.Right + 10, (rowPanel.Height - inputCtrl.Height) \ 2)
                                             End Sub
        Next
    End Sub


    ' ========================
    ' 🔹 SAVE BUTTON
    ' ========================
    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If currentDevice Is Nothing Then Exit Sub
        Dim userID As Integer = Session.LoggedInUserPointer

        ' Save category even if combo is disabled
        currentDevice.DevCategoryPointer = CInt(catcb.SelectedValue)

        ' Save dynamic fields
        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                For Each ctrl As Control In row.Controls
                    If TypeOf ctrl Is TextBox Then
                        If ctrl.Name.StartsWith("txt_") Then
                            Dim propPointer As Integer = CInt(ctrl.Name.Replace("txt_", ""))
                            Dim value As String = ctrl.Text.Trim()
                            Select Case mdl.GetCategoryPropertyName(propPointer).ToLower()
                                Case "model" : currentDevice.Model = value
                                Case "serial number" : currentDevice.SerialNumber = value
                                Case "property number" : currentDevice.PropertyNumber = value
                                Case "nsoc name" : currentDevice.NsocName = value
                            End Select
                        End If
                    ElseIf TypeOf ctrl Is ComboBox Then
                        Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
                        Dim selectedBrand As Brand = TryCast(cb.SelectedItem, Brand)
                        If selectedBrand IsNot Nothing Then
                            currentDevice.BrandPointer = selectedBrand.Pointer
                        End If
                    End If
                Next
            End If
        Next

        ' Save to database
        If mdl.Save(currentDevice, userID) Then
            MessageBox.Show("Device updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Parent.Visible = False
        Else
            MessageBox.Show("Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub


    Private Sub LayoutSpecsRows()
        Dim totalWidth As Integer = specsflowpnl.ClientSize.Width - 2
        Dim textLeft As Integer = 5 + 140 + 5 ' left padding + label width + gap

        For Each row As Control In specsflowpnl.Controls
            If TypeOf row Is Panel Then
                Dim lbl As Label = Nothing
                Dim txt As TextBox = Nothing
                For Each c As Control In row.Controls
                    If TypeOf c Is Label Then lbl = TryCast(c, Label)
                    If TypeOf c Is TextBox Then txt = TryCast(c, TextBox)
                Next
                If lbl IsNot Nothing Then lbl.Location = New Point(5, (row.Height - lbl.Height) \ 2)
                If txt IsNot Nothing Then
                    txt.Location = New Point(textLeft, (row.Height - txt.Height) \ 2)
                    txt.Width = totalWidth - textLeft - 5
                End If
            End If
        Next
    End Sub


    Private Sub LoadSpecs()
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

End Class
