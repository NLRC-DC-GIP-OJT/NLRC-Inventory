Imports QRCoder
Imports System.Drawing

Public Class QRView

    Private unitId As Integer ' store the Unit ID / pointer
    Private baseQR As Bitmap = Nothing ' store generated QR so we can resize without regenerating
    Private lastEncryptedText As String = "" ' optional: keep the encrypted string

    ' Shows the QR code for the given Unit ID
    Public Sub ShowQR(id As Integer)
        unitId = id ' store the Unit ID
        ' ==============================
        ' 1) Generate QR + get ENCRYPTED text
        ' ==============================
        Dim enc As String = Nothing
        baseQR = QRGenerator.GenerateUnitQR(unitId, enc)
        lastEncryptedText = enc

        ' ==============================
        ' 2) Render QR into panel
        ' ==============================
        RenderQR()

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
        RenderQR()
    End Sub

    ' ==========================
    ' Generate and display QR from baseQR
    ' ==========================
    Private Sub RenderQR()
        If baseQR Is Nothing Then Return

        panelqr.Controls.Clear()

        ' Determine the square size to maintain aspect ratio
        Dim side As Integer = Math.Min(panelqr.Width, panelqr.Height)
        If side <= 0 Then Return

        ' Resize QR image to fit square panel
        Dim finalQR As New Bitmap(side, side)
        Using g As Graphics = Graphics.FromImage(finalQR)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
            g.Clear(panelqr.BackColor) ' keep background consistent
            g.DrawImage(baseQR, 0, 0, side, side)
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
        ' (unchanged)
        Dim hostForm As Form = Me.FindForm()
        If hostForm Is Nothing Then
            MessageBox.Show("Host form not found.", "Layout Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

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
