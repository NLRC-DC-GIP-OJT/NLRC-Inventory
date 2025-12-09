Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class QRPrintView

    Private qrImage As Bitmap = Nothing
    Private encryptedText As String = ""

    ' for printing / preview
    Private WithEvents printDoc As New PrintDocument()
    Private printBitmap As Bitmap = Nothing

    ' 0..5 = TL, TR, ML, MR, BL, BR
    Public Property CurrentSlotIndex As Integer = 0

    ' ==============================
    ' Called from QRView to inject the QR image + encrypted string
    ' ==============================
    Public Sub LoadQR(qr As Bitmap, enc As String)
        If qr IsNot Nothing Then
            qrImage = DirectCast(qr.Clone(), Bitmap)
        Else
            qrImage = Nothing
        End If

        encryptedText = enc

        ' make sure we draw from the physical top-left of the page, not the printer margins
        printDoc.OriginAtMargins = False

        ' Render QR on screen
        RenderQR()

        ' Build the text layout in formatpnl (on screen only)
        SetupFormatPanel()

        ' Auto-resize QR when the panel changes size
        AddHandler qrpnl.Resize, AddressOf QRPnl_Resize
    End Sub

    ' ---------- QR on screen ----------
    Private Sub QRPnl_Resize(sender As Object, e As EventArgs)
        RenderQR()
    End Sub

    Private Sub RenderQR()
        If qrImage Is Nothing OrElse qrpnl Is Nothing Then Return

        qrpnl.Controls.Clear()

        Dim side As Integer = Math.Min(qrpnl.Width, qrpnl.Height)
        If side <= 0 Then Return

        Dim finalQR As New Bitmap(side, side)
        Using g As Graphics = Graphics.FromImage(finalQR)
            g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.Half
            g.Clear(qrpnl.BackColor)
            g.DrawImage(qrImage, 0, 0, side, side)
        End Using

        Dim pb As New PictureBox With {
            .Width = side,
            .Height = side,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Image = finalQR
        }

        pb.Left = (qrpnl.Width - pb.Width) \ 2
        pb.Top = (qrpnl.Height - pb.Height) \ 2

        qrpnl.Controls.Add(pb)
    End Sub

    ' ---------- formatpnl on screen ----------
    Private Sub SetupFormatPanel()
        If formatpnl Is Nothing Then Return

        formatpnl.Controls.Clear()

        Dim fields() As String = {
            "LOCATION :",
            "CATEGORY :",
            "PROPERTY NO. :",
            "DATE ACQUIRED :",
            "ACQUISITION COST :",
            "REMARKS :"
        }

        Dim y As Integer = 10
        For Each caption In fields
            Dim lbl As New Label With {
                .AutoSize = True,
                .Text = caption,
                .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
                .Location = New Point(10, y)
            }
            formatpnl.Controls.Add(lbl)

            Dim lineLeft As Integer = lbl.Right + 5
            Dim lineWidth As Integer = Math.Max(80, formatpnl.ClientSize.Width - lineLeft - 10)

            Dim line As New Panel With {
                .Height = 1,
                .Width = lineWidth,
                .BackColor = Color.Black,
                .Location = New Point(lineLeft, lbl.Bottom + 2),
                .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            }
            formatpnl.Controls.Add(line)

            y = lbl.Bottom + 5
        Next
    End Sub

    ' ---------- helper: capture whole sticker panel ----------
    Private Sub CaptureStickerToBitmap()
        If formatprintpnl Is Nothing OrElse formatprintpnl.Width <= 0 OrElse formatprintpnl.Height <= 0 Then
            printBitmap = Nothing
            Return
        End If

        Dim bmp As New Bitmap(formatprintpnl.Width, formatprintpnl.Height)
        formatprintpnl.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
        printBitmap = bmp
    End Sub

    ' ---------- Cancel button ----------
    Private Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    ' ---------- PRINT button → custom preview window ----------
    Private Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        CaptureStickerToBitmap()
        If printBitmap Is Nothing Then
            MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim f As New StickerPreviewForm(Me, printDoc)
        f.ShowDialog()
    End Sub

    ' ---------- PrintDocument drawing ----------
    Private Sub printDoc_PrintPage(sender As Object,
                                   e As Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        If printBitmap Is Nothing Then
            e.HasMorePages = False
            Return
        End If

        Dim g As Graphics = e.Graphics
        g.Clear(Color.White)

        ' ❗ Use the FULL PAGE, no extra margin
        Dim pageRect As Rectangle = e.PageBounds

        ' 2 columns x 3 rows grid
        Dim cols As Integer = 2
        Dim rows As Integer = 3

        Dim cellW As Integer = pageRect.Width \ cols
        Dim cellH As Integer = pageRect.Height \ rows

        Dim col As Integer = CurrentSlotIndex Mod cols   ' 0 or 1
        Dim row As Integer = CurrentSlotIndex \ cols     ' 0..2

        Dim cellRect As New Rectangle(
            pageRect.Left + col * cellW,
            pageRect.Top + row * cellH,
            cellW,
            cellH
        )

        ' ❗ No inner margin → use the whole cell for the sticker
        Dim innerRect As Rectangle = cellRect

        ' scale sticker to fit innerRect
        Dim srcW As Integer = printBitmap.Width
        Dim srcH As Integer = printBitmap.Height
        Dim ratio As Single = Math.Min(innerRect.Width / CSng(srcW),
                                       innerRect.Height / CSng(srcH))

        Dim drawW As Integer = CInt(srcW * ratio)
        Dim drawH As Integer = CInt(srcH * ratio)

        Dim x As Integer = innerRect.Left + (innerRect.Width - drawW) \ 2
        Dim y As Integer = innerRect.Top + (innerRect.Height - drawH) \ 2

        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

        g.DrawImage(printBitmap, New Rectangle(x, y, drawW, drawH))

        e.HasMorePages = False
    End Sub

    ' ==============================
    '  Custom print preview with slot selection
    ' ==============================
    Public Class StickerPreviewForm
        Inherits Form

        Private preview As New PrintPreviewControl()
        Private cboSlot As New ComboBox()
        Private btnPrint As New Button()
        Private btnClose As New Button()

        Private ownerView As QRPrintView
        Private doc As PrintDocument

        Public Sub New(owner As QRPrintView, document As PrintDocument)
            Me.ownerView = owner
            Me.doc = document

            Me.Text = "Print preview"
            Me.WindowState = FormWindowState.Maximized

            ' ---- preview control ----
            preview.Document = doc
            preview.Dock = DockStyle.Fill
            preview.Zoom = 1.0R

            ' ---- top bar ----
            Dim topPanel As New FlowLayoutPanel() With {
                .Dock = DockStyle.Top,
                .Height = 40,
                .FlowDirection = FlowDirection.LeftToRight
            }

            Dim lbl As New Label() With {
                .Text = "Position:",
                .AutoSize = True,
                .Margin = New Padding(5, 10, 0, 0)
            }

            cboSlot.DropDownStyle = ComboBoxStyle.DropDownList
            cboSlot.Items.AddRange(New Object() {
                "Top Left",
                "Top Right",
                "Middle Left",
                "Middle Right",
                "Bottom Left",
                "Bottom Right"
            })
            cboSlot.SelectedIndex = ownerView.CurrentSlotIndex

            AddHandler cboSlot.SelectedIndexChanged,
                Sub()
                    ownerView.CurrentSlotIndex = cboSlot.SelectedIndex
                    preview.InvalidatePreview()
                End Sub

            btnPrint.Text = "Print"
            btnPrint.AutoSize = True
            btnPrint.Margin = New Padding(15, 5, 0, 0)
            AddHandler btnPrint.Click,
                Sub()
                    Using dlg As New PrintDialog()
                        dlg.Document = doc
                        If dlg.ShowDialog(Me) = DialogResult.OK Then
                            doc.Print()
                        End If
                    End Using
                End Sub

            btnClose.Text = "Close"
            btnClose.AutoSize = True
            btnClose.Margin = New Padding(5, 5, 0, 0)
            AddHandler btnClose.Click,
                Sub() Me.Close()

            topPanel.Controls.Add(lbl)
            topPanel.Controls.Add(cboSlot)
            topPanel.Controls.Add(btnPrint)
            topPanel.Controls.Add(btnClose)

            Me.Controls.Add(preview)
            Me.Controls.Add(topPanel)
        End Sub

    End Class

End Class
