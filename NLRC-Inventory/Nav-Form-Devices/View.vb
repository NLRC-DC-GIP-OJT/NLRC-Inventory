Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data

Public Class View

    Private mdl As model
    Private currentDevice As InvDevice

    Private Const SPECS_LABEL_WIDTH As Integer = 140
    Private Const SPECS_LEFT_PADDING As Integer = 5
    Private Const SPECS_GAP As Integer = 5

    Public Sub New(m As model)
        InitializeComponent()
        mdl = m
    End Sub

    ' ================================
    ' LOAD DEVICE (MAIN ENTRY POINT)
    ' ================================
    Public Sub LoadDevice(device As InvDevice)

        ' ================================================
        ' 🔥 Convert specs pointer (ex: "5") → actual text
        ' ================================================
        Dim specPointer As Integer
        If Integer.TryParse(device.Specs, specPointer) Then
            device.Specs = mdl.GetSpecsByPointer(specPointer)
        End If

        currentDevice = device
        deviceflowpnl.Controls.Clear()
        specsflowpnl.Controls.Clear()

        Dim lblWidth As Integer = 160

        ' ================================
        ' CATEGORY
        ' ================================
        Dim catPanel As New Panel With {
            .Width = deviceflowpnl.ClientSize.Width - 2,
            .Height = 40,
            .Margin = New Padding(0, 0, 0, 8)
        }

        Dim catlbl As New Label With {
            .Text = "Category:",
            .Width = lblWidth,
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .Location = New Point(5, 8)
        }

        Dim cattxt As New TextBox With {
            .Text = mdl.GetCategoryName(device.DevCategoryPointer),
            .ReadOnly = True,
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .Width = catPanel.Width - lblWidth - 20,
            .Location = New Point(catlbl.Right + 10, 5)
        }

        catPanel.Controls.Add(catlbl)
        catPanel.Controls.Add(cattxt)
        deviceflowpnl.Controls.Add(catPanel)

        ' ================================
        ' LOAD ACTIVE PROPERTIES
        ' ================================
        Dim props As DataTable = mdl.GetCategoryProperties(device.DevCategoryPointer)

        Dim activeProps = props.AsEnumerable().
            Where(Function(r) Convert.ToBoolean(r("active")) = True).
            ToList()

        For Each prop As DataRow In activeProps

            Dim propName As String = prop("property_name").ToString().ToLower()

            Dim rowPanel As New Panel With {
                .Width = deviceflowpnl.ClientSize.Width - 2,
                .Height = 40,
                .Margin = New Padding(0, 0, 0, 5)
            }

            Dim lbl As New Label With {
                .Text = prop("property_name").ToString() & ":",
                .Width = lblWidth,
                .Location = New Point(5, 8),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
            }

            Dim inputCtrl As Control
            Dim value As String = GetDeviceProperty(device, propName)

            If propName = "brand" Then
                Dim cb As New ComboBox With {
                    .DropDownStyle = ComboBoxStyle.DropDownList,
                    .Enabled = False,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
                }
                Dim brands = mdl.GetBrandsByCategory(device.DevCategoryPointer)
                cb.DataSource = brands
                cb.DisplayMember = "BrandName"
                cb.ValueMember = "Pointer"
                cb.SelectedValue = device.BrandPointer
                inputCtrl = cb
            Else
                Dim txt As New TextBox With {
                    .ReadOnly = True,
                    .Text = value,
                    .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
                }
                inputCtrl = txt
            End If

            inputCtrl.Width = rowPanel.Width - lblWidth - 20
            inputCtrl.Location = New Point(lbl.Right + 10, 5)

            rowPanel.Controls.Add(lbl)
            rowPanel.Controls.Add(inputCtrl)
            deviceflowpnl.Controls.Add(rowPanel)
        Next

        ' ================================
        ' LOAD SPECS (READ ONLY)
        ' ================================
        LoadSpecsIntoPanel(device.Specs)

        ' ================================
        ' NOTES
        ' ================================
        If Not String.IsNullOrEmpty(device.Notes) Then

            Dim notesPanel As New Panel With {
                .Width = deviceflowpnl.ClientSize.Width - 2,
                .Height = 60,
                .Margin = New Padding(0, 5, 0, 5)
            }

            Dim notesLbl As New Label With {
                .Text = "Notes:",
                .Width = lblWidth,
                .Location = New Point(5, 5),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
            }

            Dim notesTxt As New TextBox With {
                .ReadOnly = True,
                .Multiline = True,
                .Text = device.Notes,
                .Width = notesPanel.Width - lblWidth - 20,
                .Height = 50,
                .Location = New Point(notesLbl.Right + 10, 5),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold)
            }

            notesPanel.Controls.Add(notesLbl)
            notesPanel.Controls.Add(notesTxt)
            deviceflowpnl.Controls.Add(notesPanel)
        End If

    End Sub

    ' ================================
    ' ONLY FOR VIEW
    ' PARSE device.Specs STRING
    ' AND LOAD INTO PANEL
    ' ================================
    Private Sub LoadSpecsIntoPanel(specString As String)

        specsflowpnl.Controls.Clear()

        If String.IsNullOrWhiteSpace(specString) Then Exit Sub

        Dim parts() As String = specString.Split(";"c)

        For Each s In parts
            If String.IsNullOrWhiteSpace(s) Then Continue For

            Dim item() As String = s.Split(":"c)
            Dim labelName As String = item(0).Trim()
            Dim value As String = If(item.Length > 1, item(1).Trim(), "")

            Dim row As New Panel With {
                .Width = specsflowpnl.ClientSize.Width - 2,
                .Height = 32,
                .Margin = New Padding(0, 0, 0, 4)
            }

            Dim lbl As New Label With {
                .Text = labelName & ":",
                .AutoSize = False,
                .Width = SPECS_LABEL_WIDTH,
                .Location = New Point(SPECS_LEFT_PADDING, 8)
            }

            Dim txt As New TextBox With {
                .ReadOnly = True,
                .Text = value,
                .Location = New Point(SPECS_LEFT_PADDING + SPECS_LABEL_WIDTH + SPECS_GAP, 4),
                .Width = row.Width - (SPECS_LABEL_WIDTH + SPECS_LEFT_PADDING + 15)
            }

            row.Controls.Add(lbl)
            row.Controls.Add(txt)
            specsflowpnl.Controls.Add(row)
        Next

    End Sub

    ' ================================
    ' GET VALUE OF PROPERTY
    ' ================================
    Private Function GetDeviceProperty(device As InvDevice, propName As String) As String

        Select Case propName
            Case "model" : Return device.Model
            Case "serial number" : Return device.SerialNumber
            Case "property number" : Return device.PropertyNumber
            Case "nsoc name" : Return device.NsocName
            Case Else : Return ""
        End Select

    End Function

    Private Sub cancelbtn_Click(sender As Object, e As EventArgs)
        Dim parentPanel = TryCast(Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

End Class
