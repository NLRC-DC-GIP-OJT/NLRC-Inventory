Imports QRCoder
Imports System.Drawing

Public Class QRView

    Private currentEncryptedText As String = ""

    ' Shows the QR code for the given encrypted text
    Public Sub ShowQR(encryptedText As String)
        currentEncryptedText = encryptedText
        GenerateQR()
        ' Attach resize handler for auto-scaling
        AddHandler panelqr.Resize, AddressOf PanelQR_Resize
    End Sub

    ' Hides the QR code
    Public Sub HideQR()
        RemoveHandler panelqr.Resize, AddressOf PanelQR_Resize
        panelqr.Controls.Clear()
        panelqr.Visible = False
    End Sub

    ' ==========================
    ' Auto-resize QR on panel resize
    ' ==========================
    Private Sub PanelQR_Resize(sender As Object, e As EventArgs)
        GenerateQR()
    End Sub

    ' ==========================
    ' Generate and display QR
    ' ==========================
    Private Sub GenerateQR()
        If String.IsNullOrEmpty(currentEncryptedText) Then Return
        panelqr.Controls.Clear()

        ' Generate QR code
        Dim qrGen As New QRCodeGenerator()
        Dim qrData = qrGen.CreateQrCode(currentEncryptedText, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrData)

        ' Determine the square size to maintain aspect ratio
        Dim side As Integer = Math.Min(panelqr.Width, panelqr.Height)

        ' Create minimal QR image
        Dim qrImage As Bitmap = qrCode.GetGraphic(1)

        ' Resize QR image to fit square panel
        Dim finalQR As New Bitmap(side, side)
        Using g As Graphics = Graphics.FromImage(finalQR)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
            g.Clear(panelqr.BackColor) ' keep background consistent
            g.DrawImage(qrImage, 0, 0, side, side)
        End Using

        ' Add PictureBox centered in panel
        Dim pb As New PictureBox With {
            .Width = side,
            .Height = side,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Image = finalQR
        }

        ' Center PictureBox in panel
        pb.Left = (panelqr.Width - pb.Width) \ 2
        pb.Top = (panelqr.Height - pb.Height) \ 2

        panelqr.Controls.Add(pb)
        panelqr.Visible = True
    End Sub

    Private Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        ' Find the form that hosts this EditUnit
        Dim hostForm As Form = Me.FindForm()
        If hostForm Is Nothing Then
            MessageBox.Show("Host form not found.", "Layout Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 🔎 Find the panel named "printqrformat" anywhere in the form
        Dim found() As Control = hostForm.Controls.Find("printqrformat", True)

        If found Is Nothing OrElse found.Length = 0 Then
            MessageBox.Show("Panel 'printqrformat' not found on the form.", "Layout Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim printPanel As Panel = TryCast(found(0), Panel)
        If printPanel Is Nothing Then
            MessageBox.Show("'printqrformat' is not a Panel.", "Layout Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' ✅ Show QRPrintView inside printqrformat panel
        printPanel.Visible = True
        printPanel.BringToFront()
        printPanel.Controls.Clear()

        Dim qrView As New QRPrintView()
        qrView.Dock = DockStyle.Fill
        printPanel.Controls.Add(qrView)
        qrView.BringToFront()
    End Sub

    Private Sub closepnl_Paint(sender As Object, e As EventArgs) Handles closepnl.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub


End Class
