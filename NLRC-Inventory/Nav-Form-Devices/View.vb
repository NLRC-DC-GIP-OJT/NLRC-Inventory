Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class View

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

    Private Sub View_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        InitializeLayoutScaling()

        ' make date pickers non-editable (if present)
        purchaseDatePicker.Enabled = False
        warrantyDatePicker.Enabled = False

        ' notes read-only (if present)
        notetxt.ReadOnly = True
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
    ' 🔹 LOAD DEVICE (READ-ONLY)
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
        BuildDynamicFields()

        ' CATEGORY COMBOBOX (display only)
        If catcb.Items.Count > 0 Then
            catcb.SelectedValue = If(device.DevCategoryPointer.HasValue, device.DevCategoryPointer.Value, -1)
        End If
        catcb.DropDownStyle = ComboBoxStyle.DropDownList
        catcb.Enabled = False

        ' NOTES (read-only text)
        notetxt.Text = If(device.Notes, "")
        notetxt.ReadOnly = True

        ' DATES (view only)
        If device.PurchaseDate.HasValue Then purchaseDatePicker.Value = device.PurchaseDate.Value
        If device.WarrantyExpires.HasValue Then warrantyDatePicker.Value = device.WarrantyExpires.Value
        purchaseDatePicker.Enabled = False
        warrantyDatePicker.Enabled = False

        ' SPECS PANEL (read-only)
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
                    .BorderStyle = BorderStyle.FixedSingle,
                    .ReadOnly = True
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
    ' 🔹 BUILD DYNAMIC FIELDS (READ-ONLY)
    ' ========================
    Private Sub BuildDynamicFields()

        deviceflowpnl.Controls.Clear()
        deviceflowpnl.AutoScroll = False
        deviceflowpnl.WrapContents = False
        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.Padding = New Padding(0, 10, 0, 0)

        ' ============================
        ' 🔹 First: add STATUS row (always visible, read-only)
        ' ============================
        Dim statusPanel As New Panel With {
            .Width = deviceflowpnl.ClientSize.Width - 2,
            .Margin = New Padding(0, 0, 0, 15),
            .Height = 40
        }

        Dim lblStatus As New Label With {
            .Text = "Status:",
            .AutoSize = False,
            .Width = 160,
            .Height = 24,
            .Location = New Point(5, 5),
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleLeft
        }

        Dim txtStatus As New TextBox With {
            .Name = "txtStatus",
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .Left = lblStatus.Right + 10,
            .Top = 5,
            .Width = statusPanel.Width - lblStatus.Width - 20,
            .AutoSize = False,
            .Height = 28,
            .BorderStyle = BorderStyle.FixedSingle,
            .ReadOnly = True,
            .Text = If(currentDevice IsNot Nothing AndAlso currentDevice.Status IsNot Nothing,
                       currentDevice.Status,
                       String.Empty)
        }

        statusPanel.Controls.Add(lblStatus)
        statusPanel.Controls.Add(txtStatus)
        deviceflowpnl.Controls.Add(statusPanel)

        ' ============================
        ' 🔹 Then: other dynamic fields (read-only)
        ' ============================
        Dim props As DataTable = mdl.GetCategoryProperties(categoryPointer)
        Dim activeProps = props.AsEnumerable().Where(Function(r) Convert.ToBoolean(r("active")) = True).ToList()

        For Each prop As DataRow In activeProps

            Dim rawName As String = prop("property_name").ToString()
            Dim propName As String = rawName.Trim().ToLower()
            Dim propPointer As Integer = CInt(prop("pointer"))

            ' Skip any legacy "status" property rows (we already show Status above)
            If propName.Contains("status") Then
                Continue For
            End If

            Dim rowPanel As New Panel With {
                .Width = deviceflowpnl.ClientSize.Width - 2,
                .Margin = New Padding(0, 0, 0, 15)
            }

            ' LABEL
            Dim lbl As New Label With {
                .Text = rawName & ":",
                .AutoSize = False,
                .Width = 160,
                .Height = 24,
                .Location = New Point(5, 5),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                .TextAlign = ContentAlignment.MiddleLeft
            }

            rowPanel.Controls.Add(lbl)

            ' BRAND FIELD (Combo + TextBox) - VIEW ONLY
            If propName.Contains("brand") Then

                rowPanel.Height = 75

                Dim cb As New ComboBox With {
                    .Name = "cb_" & propPointer,
                    .DropDownStyle = ComboBoxStyle.DropDownList,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    .Left = lbl.Right + 10,
                    .Top = 5,
                    .Width = rowPanel.Width - lbl.Width - 20,
                    .Height = 28,
                    .Enabled = False
                }

                Dim brands = mdl.GetBrandsByCategory(categoryPointer)
                cb.DataSource = brands
                cb.DisplayMember = "BrandName"
                cb.ValueMember = "Pointer"

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

                rowPanel.Controls.Add(cb)
                rowPanel.Controls.Add(txtBrand)

            Else
                ' OTHER TEXTBOX FIELDS - READ ONLY
                rowPanel.Height = 40

                Dim txt As New TextBox With {
                    .Name = "txt_" & propPointer,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                    .Left = lbl.Right + 10,
                    .Top = 5,
                    .Width = rowPanel.Width - lbl.Width - 20,
                    .AutoSize = False,
                    .Height = 28,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .ReadOnly = True
                }

                ' Load existing values
                Select Case True
                    Case propName.Contains("model") : txt.Text = currentDevice.Model
                    Case propName.Contains("serial") : txt.Text = currentDevice.SerialNumber
                    Case propName.Contains("property") : txt.Text = currentDevice.PropertyNumber
                    Case propName.Contains("nsoc") : txt.Text = currentDevice.NsocName
                    Case propName.Contains("cost") : txt.Text = If(currentDevice.Cost.HasValue, currentDevice.Cost.ToString(), "")
                    Case propName.Contains("notes") : txt.Text = currentDevice.Notes
                End Select

                rowPanel.Controls.Add(txt)
            End If

            deviceflowpnl.Controls.Add(rowPanel)
        Next

    End Sub

    ' central resize handler
    Private Sub deviceflowpnl_Resize(sender As Object, e As EventArgs)
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
    ' SPECS LAYOUT
    ' ========================
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
                    txt.ReadOnly = True
                End If
            End If
        Next
    End Sub

    Private Sub specscb_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' in View we can ignore changes or just keep as is; combo is disabled anyway
    End Sub

    ' ========================
    ' HISTORY GRID (SAME AS EDIT)
    ' ========================
    Private Sub LoadHistory(devicePointer As Integer)
        Dim dt As DataTable = mdl.GetDeviceHistory(devicePointer)
        If dt Is Nothing Then Return

        historydgv.AutoGenerateColumns = True

        ' transform specs rows: only show changed specs
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

        historydgv.DataSource = dt

        ' hide technical columns
        If historydgv.Columns.Contains("pointer") Then
            historydgv.Columns("pointer").Visible = False
        End If
        If historydgv.Columns.Contains("device_pointer") Then
            historydgv.Columns("device_pointer").Visible = False
        End If
        If historydgv.Columns.Contains("updated_by") Then
            historydgv.Columns("updated_by").Visible = False
        End If

        ' headers
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
            .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
            .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.ForeColor

            .RowHeadersDefaultCellStyle.SelectionBackColor = .RowHeadersDefaultCellStyle.BackColor
            .RowHeadersDefaultCellStyle.SelectionForeColor = .RowHeadersDefaultCellStyle.ForeColor

            .ColumnHeadersDefaultCellStyle.SelectionBackColor = .ColumnHeadersDefaultCellStyle.BackColor
            .ColumnHeadersDefaultCellStyle.SelectionForeColor = .ColumnHeadersDefaultCellStyle.ForeColor

            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            .DefaultCellStyle.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .DefaultCellStyle.Padding = New Padding(0, 0, 0, 0)
            .RowTemplate.Height = 18

            .CellBorderStyle = DataGridViewCellBorderStyle.None
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .GridColor = Me.BackColor
            .EnableHeadersVisualStyles = False

            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            .AllowUserToOrderColumns = False
            .AllowUserToResizeColumns = False
            .AllowUserToResizeRows = False
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            For Each col As DataGridViewColumn In .Columns
                col.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            Dim totalW As Integer = .ClientSize.Width

            Dim wFrom As Integer = CInt(totalW * 0.3)
            Dim wTo As Integer = CInt(totalW * 0.3)
            Dim wRemarks As Integer = 110
            Dim wDate As Integer = 110
            Dim wUser As Integer = totalW - (wFrom + wTo + wRemarks + wDate)
            If wUser < 90 Then wUser = 90

            If .Columns.Contains("updated_from") Then .Columns("updated_from").Width = wFrom
            If .Columns.Contains("updated_to") Then .Columns("updated_to").Width = wTo
            If .Columns.Contains("remarks") Then .Columns("remarks").Width = wRemarks
            If .Columns.Contains("date") Then .Columns("date").Width = wDate
            If .Columns.Contains("updated_by_name") Then .Columns("updated_by_name").Width = wUser
        End With
    End Sub

    ' === specs helpers (same as Edit) ===
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

    Private Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Dim parentPanel = TryCast(Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub
End Class
