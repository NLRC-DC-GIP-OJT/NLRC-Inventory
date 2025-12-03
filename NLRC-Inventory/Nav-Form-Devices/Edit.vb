Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms


Public Class Edit

    Private Class DeviceChange
        Public Property FieldName As String
        Public Property OldValue As String
        Public Property NewValue As String
    End Class

    Private Class FieldInfo
        Public Property Required As Boolean
        Public Property PropName As String
    End Class



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

        ' Convert specs pointer -> real string
        Dim specPointer As Integer
        If Integer.TryParse(device.Specs, specPointer) Then
            device.Specs = mdl.GetSpecsByPointer(specPointer)
        End If

        currentDevice = device
        categoryPointer = device.DevCategoryPointer

        ' 🔹 LOAD HISTORY FOR THIS DEVICE
        LoadHistory(currentDevice.Pointer)

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
                Dim txt As New TextBox With {
                    .Text = fieldValue,
                    .Tag = labelName,
                    .BorderStyle = BorderStyle.FixedSingle
                }

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
        deviceflowpnl.AutoScroll = False
        deviceflowpnl.WrapContents = False
        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.Padding = New Padding(0, 10, 0, 0)

        Dim props As DataTable = mdl.GetCategoryProperties(categoryPointer)
        Dim activeProps = props.AsEnumerable().Where(Function(r) Convert.ToBoolean(r("active")) = True).ToList()

        For Each prop As DataRow In activeProps

            Dim rawName As String = prop("property_name").ToString()
            Dim propName As String = rawName.Trim().ToLower()
            Dim propPointer As Integer = CInt(prop("pointer"))

            ' read "required" from table (if it exists)
            Dim isRequired As Boolean = False
            If props.Columns.Contains("required") Then
                isRequired = Convert.ToBoolean(prop("required"))
            End If

            Dim rowPanel As New Panel With {
                .Width = deviceflowpnl.ClientSize.Width - 2,
                .Margin = New Padding(0, 0, 0, 15)
            }

            ' LABEL
            Dim lbl As New Label With {
                .Text = rawName & If(isRequired, " *", "") & ":",
                .AutoSize = False,
                .Width = 160,
                .Height = 24,
                .Location = New Point(5, 5),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleLeft,
                .ForeColor = If(isRequired, Color.Red, Color.Black)
            }

            rowPanel.Controls.Add(lbl)

            ' ===========================
            ' BRAND FIELD (Combo + TextBox)
            ' ===========================
            If propName.Contains("brand") Then

                rowPanel.Height = 75

                Dim cb As New ComboBox With {
                    .Name = "cb_" & propPointer,
                    .DropDownStyle = ComboBoxStyle.DropDownList,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    .Left = lbl.Right + 10,
                    .Top = 5,
                    .Width = rowPanel.Width - lbl.Width - 20,
                    .Height = 28
                }

                Dim brands = mdl.GetBrandsByCategory(categoryPointer)
                cb.DataSource = brands
                cb.DisplayMember = "BrandName"
                cb.ValueMember = "Pointer"

                ' store required info (no color change)
                cb.Tag = New FieldInfo With {.Required = isRequired, .PropName = rawName}

                Dim txtBrand As New TextBox With {
                    .Name = "txtBrand_" & propPointer,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    .Top = cb.Bottom + 5,
                    .Left = cb.Left,
                    .Width = cb.Width,
                    .Height = 28,
                    .ReadOnly = True
                }

                ' Load existing brand
                If currentDevice.BrandPointer.HasValue Then
                    cb.SelectedValue = currentDevice.BrandPointer.Value
                    Dim bn = brands.FirstOrDefault(Function(x) x.Pointer = currentDevice.BrandPointer.Value)
                    If bn IsNot Nothing Then txtBrand.Text = bn.BrandName
                End If

                AddHandler cb.SelectedIndexChanged,
                    Sub()
                        Dim sel As Brand = TryCast(cb.SelectedItem, Brand)
                        If sel IsNot Nothing Then txtBrand.Text = sel.BrandName
                    End Sub

                rowPanel.Controls.Add(cb)
                rowPanel.Controls.Add(txtBrand)

            Else
                ' ===========================
                ' OTHER TEXTBOX FIELDS
                ' ===========================
                rowPanel.Height = 40

                Dim txt As New TextBox With {
                    .Name = "txt_" & propPointer,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    .Left = lbl.Right + 10,
                    .Top = 5,
                    .Width = rowPanel.Width - lbl.Width - 20,
                    .AutoSize = False,
                    .Height = 28,
                    .BorderStyle = BorderStyle.FixedSingle
                }

                ' store required info (no color change)
                txt.Tag = New FieldInfo With {.Required = isRequired, .PropName = rawName}

                ' Load existing values
                Select Case True
                    Case propName.Contains("model") : txt.Text = currentDevice.Model
                    Case propName.Contains("serial") : txt.Text = currentDevice.SerialNumber
                    Case propName.Contains("property") : txt.Text = currentDevice.PropertyNumber
                    Case propName.Contains("nsoc") : txt.Text = currentDevice.NsocName
                    Case propName.Contains("cost") : txt.Text = If(currentDevice.Cost.HasValue, currentDevice.Cost.ToString(), "")
                    Case propName.Contains("status") : txt.Text = currentDevice.Status
                    Case propName.Contains("notes") : txt.Text = currentDevice.Notes
                End Select

                rowPanel.Controls.Add(txt)
            End If

            deviceflowpnl.Controls.Add(rowPanel)
        Next

    End Sub



    ' ★★ FIX: one central resize handler so textboxes resize properly
    Private Sub deviceflowpnl_Resize(sender As Object, e As EventArgs) Handles deviceflowpnl.Resize
        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                Dim lbl As Label = Nothing
                For Each c As Control In row.Controls
                    If TypeOf c Is Label Then
                        lbl = DirectCast(c, Label)
                        Exit For
                    End If
                Next
                If lbl Is Nothing Then Continue For

                For Each c As Control In row.Controls
                    If TypeOf c Is TextBox OrElse TypeOf c Is ComboBox Then
                        c.Width = row.Width - lbl.Width - 20
                        If TypeOf c Is TextBox Then
                            c.Height = 28
                            c.Top = (row.Height - c.Height) \ 2
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    ' ========================
    ' 🔹 SAVE BUTTON
    ' ========================
    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If currentDevice Is Nothing Then Exit Sub
        Dim userID As Integer = Session.LoggedInUserPointer

        ' ✅ Validate required fields first
        If Not ValidateRequiredFields() Then
            ' validation already showed which field is missing
            Exit Sub
        End If

        ' ============================
        ' 1. BUILD A "NEW DEVICE" SNAPSHOT
        '    (DO NOT TOUCH currentDevice YET)
        ' ============================
        Dim newDevice As New InvDevice With {
            .Pointer = currentDevice.Pointer,
            .DevCategoryPointer = CInt(catcb.SelectedValue),
            .BrandPointer = currentDevice.BrandPointer,
            .Model = currentDevice.Model,
            .SerialNumber = currentDevice.SerialNumber,
            .PropertyNumber = currentDevice.PropertyNumber,
            .NsocName = currentDevice.NsocName,
            .Status = currentDevice.Status,
            .Cost = currentDevice.Cost,
            .Notes = currentDevice.Notes,
            .PurchaseDate = purchaseDatePicker.Value,      ' take from picker
            .WarrantyExpires = warrantyDatePicker.Value,   ' take from picker
            .Specs = currentDevice.Specs
        }

        ' ============================
        ' 2. READ DYNAMIC FIELDS → newDevice
        ' ============================
        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                For Each ctrl As Control In row.Controls

                    ' TextBox fields
                    If TypeOf ctrl Is TextBox AndAlso ctrl.Name.StartsWith("txt_") Then
                        Dim propPointer As Integer = CInt(ctrl.Name.Replace("txt_", ""))
                        Dim value As String = ctrl.Text.Trim()
                        Dim propName As String = mdl.GetCategoryPropertyName(propPointer).ToLower()

                        Select Case propName
                            Case "model"
                                newDevice.Model = value
                            Case "serial number"
                                newDevice.SerialNumber = value
                            Case "property number"
                                newDevice.PropertyNumber = value
                            Case "nsoc name"
                                newDevice.NsocName = value
                            Case "status"
                                newDevice.Status = value
                            Case "cost"
                                Dim decVal As Decimal
                                If Decimal.TryParse(value, decVal) Then
                                    newDevice.Cost = decVal
                                Else
                                    newDevice.Cost = Nothing
                                End If
                            Case "notes"
                                newDevice.Notes = value
                        End Select

                        ' Brand combobox
                    ElseIf TypeOf ctrl Is ComboBox Then
                        Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
                        Dim selectedBrand As Brand = TryCast(cb.SelectedItem, Brand)
                        If selectedBrand IsNot Nothing Then
                            newDevice.BrandPointer = selectedBrand.Pointer
                        End If
                    End If

                Next
            End If
        Next

        ' 3. BUILD NEW SPECS STRING → newDevice.Specs
        Dim newSpecList As New List(Of String)
        For Each row As Control In specsflowpnl.Controls
            If TypeOf row Is Panel Then
                Dim lbl As Label = Nothing
                Dim txt As TextBox = Nothing

                For Each c As Control In row.Controls
                    If TypeOf c Is Label Then lbl = DirectCast(c, Label)
                    If TypeOf c Is TextBox Then txt = DirectCast(c, TextBox)
                Next

                If lbl IsNot Nothing AndAlso txt IsNot Nothing Then
                    newSpecList.Add(lbl.Text.TrimEnd(":"c) & ": " & txt.Text.Trim())
                End If
            End If
        Next
        newDevice.Specs = String.Join("; ", newSpecList)

        ' 🔹 MAKE NOTES COME FROM NOTETXT
        newDevice.Notes = notetxt.Text.Trim()


        ' ============================
        ' 4. BUILD CHANGE SUMMARY + LIST OF CHANGES
        ' ============================
        Dim sb As New StringBuilder()
        Dim changes As New List(Of DeviceChange)()

        ' helper for dates
        Dim fmtDate As Func(Of Date?, String) =
            Function(d As Date?) If(d.HasValue, d.Value.ToString("yyyy-MM-dd"), "N/A")

        ' MODEL
        If currentDevice.Model <> newDevice.Model Then
            sb.AppendLine($"Model: {currentDevice.Model}  →  {newDevice.Model}")
            changes.Add(New DeviceChange With {
                .FieldName = "Model",
                .OldValue = currentDevice.Model,
                .NewValue = newDevice.Model
            })
        End If

        ' SERIAL
        If currentDevice.SerialNumber <> newDevice.SerialNumber Then
            sb.AppendLine($"Serial No.: {currentDevice.SerialNumber}  →  {newDevice.SerialNumber}")
            changes.Add(New DeviceChange With {
                .FieldName = "Serial No.",
                .OldValue = currentDevice.SerialNumber,
                .NewValue = newDevice.SerialNumber
            })
        End If

        ' PROPERTY NUMBER
        If currentDevice.PropertyNumber <> newDevice.PropertyNumber Then
            sb.AppendLine($"Property No.: {currentDevice.PropertyNumber}  →  {newDevice.PropertyNumber}")
            changes.Add(New DeviceChange With {
                .FieldName = "Property No.",
                .OldValue = currentDevice.PropertyNumber,
                .NewValue = newDevice.PropertyNumber
            })
        End If

        ' NSOC NAME
        If currentDevice.NsocName <> newDevice.NsocName Then
            sb.AppendLine($"NSOC Name: {currentDevice.NsocName}  →  {newDevice.NsocName}")
            changes.Add(New DeviceChange With {
                .FieldName = "NSOC Name",
                .OldValue = currentDevice.NsocName,
                .NewValue = newDevice.NsocName
            })
        End If

        ' PURCHASE DATE
        If Not Nullable.Equals(currentDevice.PurchaseDate, newDevice.PurchaseDate) Then
            sb.AppendLine($"Purchase Date: {fmtDate(currentDevice.PurchaseDate)}  →  {fmtDate(newDevice.PurchaseDate)}")
            changes.Add(New DeviceChange With {
                .FieldName = "Purchase Date",
                .OldValue = fmtDate(currentDevice.PurchaseDate),
                .NewValue = fmtDate(newDevice.PurchaseDate)
            })
        End If

        ' WARRANTY DATE
        If Not Nullable.Equals(currentDevice.WarrantyExpires, newDevice.WarrantyExpires) Then
            sb.AppendLine($"Warranty Expires: {fmtDate(currentDevice.WarrantyExpires)}  →  {fmtDate(newDevice.WarrantyExpires)}")
            changes.Add(New DeviceChange With {
                .FieldName = "Warranty Expires",
                .OldValue = fmtDate(currentDevice.WarrantyExpires),
                .NewValue = fmtDate(newDevice.WarrantyExpires)
            })
        End If

        ' STATUS
        If currentDevice.Status <> newDevice.Status Then
            sb.AppendLine($"Status: {currentDevice.Status}  →  {newDevice.Status}")
            changes.Add(New DeviceChange With {
                .FieldName = "Status",
                .OldValue = currentDevice.Status,
                .NewValue = newDevice.Status
            })
        End If

        ' COST
        Dim oldCostStr As String = If(currentDevice.Cost.HasValue, currentDevice.Cost.Value.ToString("N2"), "")
        Dim newCostStr As String = If(newDevice.Cost.HasValue, newDevice.Cost.Value.ToString("N2"), "")
        If oldCostStr <> newCostStr Then
            sb.AppendLine($"Cost: {oldCostStr}  →  {newCostStr}")
            changes.Add(New DeviceChange With {
                .FieldName = "Cost",
                .OldValue = oldCostStr,
                .NewValue = newCostStr
            })
        End If

        ' NOTES
        If currentDevice.Notes <> newDevice.Notes Then
            sb.AppendLine("Notes: (changed)")
            changes.Add(New DeviceChange With {
                .FieldName = "Notes",
                .OldValue = currentDevice.Notes,
                .NewValue = newDevice.Notes
            })
        End If

        ' SPECS (optional, one row, still shorter than whole state)
        If currentDevice.Specs <> newDevice.Specs Then
            sb.AppendLine("Specs: (changed)")
            changes.Add(New DeviceChange With {
                .FieldName = "Specs",
                .OldValue = currentDevice.Specs,
                .NewValue = newDevice.Specs
            })
        End If

        ' BRAND (by name)
        If currentDevice.BrandPointer <> newDevice.BrandPointer Then
            Dim oldBrandName As String =
                If(currentDevice.BrandPointer.HasValue AndAlso currentDevice.BrandPointer.Value > 0,
                   mdl.GetBrandName(currentDevice.BrandPointer.Value),
                   "N/A")

            Dim newBrandName As String =
                If(newDevice.BrandPointer.HasValue AndAlso newDevice.BrandPointer.Value > 0,
                   mdl.GetBrandName(newDevice.BrandPointer.Value),
                   "N/A")

            sb.AppendLine($"Brand: {oldBrandName}  →  {newBrandName}")
            changes.Add(New DeviceChange With {
                .FieldName = "Brand",
                .OldValue = oldBrandName,
                .NewValue = newBrandName
            })
        End If

        ' ============================
        ' 5. IF NO CHANGES → STOP
        ' ============================
        If sb.Length = 0 Then
            MessageBox.Show("No changes detected. Nothing to update.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' ============================
        ' 6. SHOW CONFIRMATION MESSAGEBOX
        ' ============================
        Dim summaryText As String =
            "You are about to update this device with the following changes:" &
            vbCrLf & vbCrLf &
            sb.ToString() &
            vbCrLf &
            "Are you sure you want to update these fields?"

        Dim result = MessageBox.Show(summaryText,
                                     "Confirm Update",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question)

        If result <> DialogResult.Yes Then
            ' user cancelled
            Exit Sub
        End If

        ' ============================
        ' 7. USER CONFIRMED → COPY NEW VALUES BACK TO currentDevice
        ' ============================
        currentDevice.DevCategoryPointer = newDevice.DevCategoryPointer
        currentDevice.BrandPointer = newDevice.BrandPointer
        currentDevice.Model = newDevice.Model
        currentDevice.SerialNumber = newDevice.SerialNumber
        currentDevice.PropertyNumber = newDevice.PropertyNumber
        currentDevice.NsocName = newDevice.NsocName
        currentDevice.Status = newDevice.Status
        currentDevice.Cost = newDevice.Cost
        currentDevice.Notes = newDevice.Notes
        currentDevice.PurchaseDate = newDevice.PurchaseDate
        currentDevice.WarrantyExpires = newDevice.WarrantyExpires
        currentDevice.Specs = newDevice.Specs

        ' ============================
        ' 8. SAVE TO DATABASE + PER-FIELD HISTORY
        ' ============================
        If mdl.Save(currentDevice, userID) Then
            ' one history row per changed field
            For Each ch In changes
                mdl.InsertDeviceHistory(
                        currentDevice.Pointer,
                        ch.FieldName,
                        ch.OldValue,
                        ch.NewValue,
                        userID)
            Next

            ' 🔄 REFRESH HISTORY GRID SO NEW ROWS APPEAR IMMEDIATELY
            LoadHistory(currentDevice.Pointer)

            MessageBox.Show("Device updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' If you still want to close the Edit panel after save,
            ' leave this line. If you want to stay and view history, remove it.
            'Me.Parent.Visible = False
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
        Dim textLeft As Integer = 5 + 140 + 5

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

    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs)
        specsflowpnl.Controls.Clear()

        Dim selectedSpec = TryCast(specscb.SelectedItem, DeviceSpecification)
        If selectedSpec Is Nothing Then Return

        Dim specsText = selectedSpec.SpecName
        If String.IsNullOrWhiteSpace(specsText) Then Return

        For Each spec In specsText.Split(";"c)
            If String.IsNullOrWhiteSpace(spec) Then Continue For

            Dim parts = spec.Split(":"c)
            Dim labelName = parts(0).Trim
            Dim fieldValue = If(parts.Length > 1, parts(1).Trim, "")

            Dim rowPanel As New Panel With {.Width = specsflowpnl.ClientSize.Width - 2, .Height = 32, .Margin = New Padding(0, 0, 0, 4)}
            Dim lbl As New Label With {.Text = labelName & ":", .AutoSize = False, .Width = 140}
            Dim txt As New TextBox With {
                .Text = fieldValue,
                .Tag = labelName,
                .BorderStyle = BorderStyle.FixedSingle
                }


            rowPanel.Controls.Add(lbl)
            rowPanel.Controls.Add(txt)
            specsflowpnl.Controls.Add(rowPanel)
        Next

        LayoutSpecsRows()
    End Sub


    Private Function BuildDeviceStateString(d As InvDevice) As String
        Dim sb As New StringBuilder()

        sb.AppendLine($"Model: {d.Model}")
        sb.AppendLine($"Serial No.: {d.SerialNumber}")
        sb.AppendLine($"Property No.: {d.PropertyNumber}")
        sb.AppendLine($"NSOC Name: {d.NsocName}")

        Dim fmtDate As Func(Of Date?, String) =
            Function(x) If(x.HasValue, x.Value.ToString("yyyy-MM-dd"), "N/A")

        sb.AppendLine($"Purchase Date: {fmtDate(d.PurchaseDate)}")
        sb.AppendLine($"Warranty Expires: {fmtDate(d.WarrantyExpires)}")
        sb.AppendLine($"Status: {d.Status}")
        sb.AppendLine($"Cost: {If(d.Cost.HasValue, d.Cost.Value.ToString(), "N/A")}")
        sb.AppendLine($"Brand Pointer: {If(d.BrandPointer.HasValue, d.BrandPointer.Value.ToString(), "N/A")}")
        sb.AppendLine($"Specs: {d.Specs}")
        sb.AppendLine($"Notes: {d.Notes}")

        Return sb.ToString()
    End Function


    Private Sub LoadHistory(devicePointer As Integer)
        Dim dt As DataTable = mdl.GetDeviceHistory(devicePointer)

        ' ==== transform specs rows: only show changed specs ====
        For Each row As DataRow In dt.Rows
            Dim remarks As String = If(row("remarks") Is DBNull.Value, "", row("remarks").ToString())

            If remarks.ToLower().Contains("specs") Then
                Dim oldRaw As String = If(row("updated_from") Is DBNull.Value, "", row("updated_from").ToString())
                Dim newRaw As String = If(row("updated_to") Is DBNull.Value, "", row("updated_to").ToString())

                Dim oldDisp As String = ""
                Dim newDisp As String = ""

                BuildSpecsDiffDisplay(oldRaw, newRaw, oldDisp, newDisp)

                row("updated_from") = oldDisp
                row("updated_to") = newDisp
            End If
        Next

        ' ==== bind AFTER transformation ====
        historydgv.DataSource = dt

        ' Hide technical columns
        If historydgv.Columns.Contains("pointer") Then
            historydgv.Columns("pointer").Visible = False
        End If
        If historydgv.Columns.Contains("device_pointer") Then
            historydgv.Columns("device_pointer").Visible = False
        End If
        If historydgv.Columns.Contains("updated_by") Then
            historydgv.Columns("updated_by").Visible = False
        End If

        ' Nice headers
        If historydgv.Columns.Contains("updated_from") Then
            historydgv.Columns("updated_from").HeaderText = "From"
        End If
        If historydgv.Columns.Contains("updated_to") Then
            historydgv.Columns("updated_to").HeaderText = "To"
        End If
        If historydgv.Columns.Contains("remarks") Then
            historydgv.Columns("remarks").HeaderText = "Remarks"
        End If
        If historydgv.Columns.Contains("date") Then
            historydgv.Columns("date").HeaderText = "Date"
        End If
        If historydgv.Columns.Contains("updated_by_name") Then
            historydgv.Columns("updated_by_name").HeaderText = "Updated By"
        End If

        With historydgv

            ' 🔹 Remove visible selection color
            .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
            .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.ForeColor

            .RowHeadersDefaultCellStyle.SelectionBackColor = .RowHeadersDefaultCellStyle.BackColor
            .RowHeadersDefaultCellStyle.SelectionForeColor = .RowHeadersDefaultCellStyle.ForeColor

            .ColumnHeadersDefaultCellStyle.SelectionBackColor = .ColumnHeadersDefaultCellStyle.BackColor
            .ColumnHeadersDefaultCellStyle.SelectionForeColor = .ColumnHeadersDefaultCellStyle.ForeColor

            ' ==== BASIC LOOK ====
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            .DefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            ' ⭐ FIX: remove big top space / top-align cells
            .RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
            .RowTemplate.Height = 18   ' tweak if you want taller rows

            .CellBorderStyle = DataGridViewCellBorderStyle.None
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .GridColor = Me.BackColor
            .EnableHeadersVisualStyles = False

            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False

            ' we control widths manually
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            For Each col As DataGridViewColumn In .Columns
                col.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            ' ==== WIDTHS: give Remarks / Date / Updated By enough space ====
            Dim totalW As Integer = .ClientSize.Width

            Dim wFrom As Integer = CInt(totalW * 0.3)
            Dim wTo As Integer = CInt(totalW * 0.3)
            Dim wRemarks As Integer = 110      ' enough to show "Remarks"
            Dim wDate As Integer = 110         ' enough to show full date header
            Dim wUser As Integer = totalW - (wFrom + wTo + wRemarks + wDate)
            If wUser < 90 Then wUser = 90      ' minimum width for "Updated By"

            If .Columns.Contains("updated_from") Then .Columns("updated_from").Width = wFrom
            If .Columns.Contains("updated_to") Then .Columns("updated_to").Width = wTo
            If .Columns.Contains("remarks") Then .Columns("remarks").Width = wRemarks
            If .Columns.Contains("date") Then .Columns("date").Width = wDate
            If .Columns.Contains("updated_by_name") Then .Columns("updated_by_name").Width = wUser
        End With
    End Sub





    ' Parse "Label: value; Label2: value2" → Dictionary(label → value)
    Private Function ParseSpecsToDict(specString As String) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        If String.IsNullOrWhiteSpace(specString) Then Return dict

        Dim parts() As String = specString.Split(";"c)

        For Each raw In parts
            Dim part As String = raw.Trim()
            If part = "" Then Continue For

            Dim kv() As String = part.Split(":"c)
            Dim label As String = kv(0).Trim()
            Dim value As String = If(kv.Length > 1, kv(1).Trim(), "")

            If Not dict.ContainsKey(label) Then
                dict.Add(label, value)
            Else
                dict(label) = value
            End If
        Next

        Return dict
    End Function

    ' Build *display* text for old/new specs showing ONLY changed items
    Private Sub BuildSpecsDiffDisplay(oldSpec As String,
                                      newSpec As String,
                                      ByRef oldDisplay As String,
                                      ByRef newDisplay As String)

        Dim oldDict = ParseSpecsToDict(oldSpec)
        Dim newDict = ParseSpecsToDict(newSpec)

        Dim allKeys As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each k In oldDict.Keys
            allKeys.Add(k)
        Next
        For Each k In newDict.Keys
            allKeys.Add(k)
        Next

        Dim sbOld As New StringBuilder()
        Dim sbNew As New StringBuilder()

        For Each label In allKeys
            Dim oldVal As String = If(oldDict.ContainsKey(label), oldDict(label), "")
            Dim newVal As String = If(newDict.ContainsKey(label), newDict(label), "")

            ' only show specs that actually changed
            If oldVal <> newVal Then
                If oldVal <> "" Then
                    If sbOld.Length > 0 Then sbOld.AppendLine()
                    sbOld.Append($"{label}: {oldVal}")
                End If

                If newVal <> "" Then
                    If sbNew.Length > 0 Then sbNew.AppendLine()
                    sbNew.Append($"{label}: {newVal}")
                End If
            End If
        Next

        oldDisplay = sbOld.ToString()
        newDisplay = sbNew.ToString()
    End Sub





    Private Function FormatSpecsForDisplay(specString As String) As String
        If String.IsNullOrWhiteSpace(specString) Then Return ""

        Dim sb As New StringBuilder()
        Dim parts() As String = specString.Split(";"c)

        For Each raw In parts
            Dim part As String = raw.Trim()
            If part = "" Then Continue For

            If sb.Length > 0 Then sb.AppendLine()
            sb.Append(part)   ' keeps "Processor: value"
        Next

        Return sb.ToString()
    End Function


    ' ========================
    ' 🔍 VALIDATE REQUIRED FIELDS (EDIT) - NO COLOR
    ' ========================
    Private Function ValidateRequiredFields() As Boolean
        For Each row As Control In deviceflowpnl.Controls
            If TypeOf row Is Panel Then
                For Each ctrl As Control In row.Controls

                    If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
                        Dim info As FieldInfo = TryCast(ctrl.Tag, FieldInfo)

                        ' only check controls that have FieldInfo and are required
                        If info Is Nothing OrElse Not info.Required Then Continue For

                        Dim hasValue As Boolean = False

                        If TypeOf ctrl Is TextBox Then
                            Dim txt As TextBox = DirectCast(ctrl, TextBox)
                            hasValue = Not String.IsNullOrWhiteSpace(txt.Text)

                        ElseIf TypeOf ctrl Is ComboBox Then
                            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)
                            hasValue = (cb.SelectedIndex >= 0)
                        End If

                        If Not hasValue Then
                            MessageBox.Show(info.PropName & " is required.",
                                                "Validation Error",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning)
                            ctrl.Focus()
                            Return False
                        End If
                    End If

                Next
            End If
        Next

        Return True
    End Function




End Class
