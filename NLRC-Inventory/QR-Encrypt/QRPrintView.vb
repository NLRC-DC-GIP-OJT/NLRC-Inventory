Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class QRPrintView

    Private qrImage As Bitmap = Nothing
    Private encryptedText As String = ""

    ' for printing / preview
    Private WithEvents printDoc As New PrintDocument()

    ' === single-sticker mode (old behaviour) ===
    Private singleStickerBitmap As Bitmap = Nothing

    ' === batch mode (many units, 6 per page, multi-page) ===
    Private allStickers As New List(Of Bitmap)()
    Private currentStickerIndex As Integer = 0
    Private multiBatch As Boolean = False

    ' 0..5 = TL, TR, ML, MR, BL, BR (single-sticker mode)
    Public Property CurrentSlotIndex As Integer = 6   ' 6 = all 6 slots

    ' collection name (unit name) to show on the sticker
    Private currentCollectionName As String = ""

    ' 🔹 total pages for preview (batch)
    Private totalPages As Integer = 1


    ' ==============================
    ' Called to inject QR + encrypted string + collection name
    ' ==============================
    Public Sub LoadQR(qr As Bitmap, enc As String, Optional collectionName As String = "")
        If qr IsNot Nothing Then
            qrImage = DirectCast(qr.Clone(), Bitmap)
        Else
            qrImage = Nothing
        End If

        encryptedText = enc
        currentCollectionName = collectionName

        ' draw from physical top-left of the page, not printer margins
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

    ' ============================================================
    '   TEXT + LINES ON LEFT SIDE (formatpnl)
    '   COLLECTION NAME:  JOCE
    '                      ─────────────
    '   LOCATION : ______
    '   ...
    ' ============================================================
    Private Sub SetupFormatPanel()
        If formatpnl Is Nothing Then Return

        formatpnl.Controls.Clear()

        Dim y As Integer = 10

        ' First field is COLLECTION NAME, rest are the usual ones
        Dim fields() As String = {
            "COLLECTION NAME :",
            "LOCATION :",
            "CATEGORY :",
            "PROPERTY NO. :",
            "DATE ACQUIRED :",
            "ACQUISITION COST :",
            "REMARKS :"
        }

        For Each caption In fields
            ' caption label
            Dim lbl As New Label With {
                .AutoSize = True,
                .Text = caption,
                .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                .Location = New Point(10, y)
            }
            formatpnl.Controls.Add(lbl)

            ' underline
            Dim lineLeft As Integer = lbl.Right + 5
            Dim lineTop As Integer = lbl.Bottom + 2
            Dim lineWidth As Integer = Math.Max(80, formatpnl.ClientSize.Width - lineLeft - 10)

            Dim line As New Panel With {
                .Height = 1,
                .Width = lineWidth,
                .BackColor = Color.Black,
                .Location = New Point(lineLeft, lineTop),
                .Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            }
            formatpnl.Controls.Add(line)

            ' SPECIAL CASE: COLLECTION NAME → draw value OVER the line, CENTERED
            If caption.StartsWith("COLLECTION NAME") AndAlso
               Not String.IsNullOrWhiteSpace(currentCollectionName) Then

                Dim nameLbl As New Label With {
                    .AutoSize = True,
                    .Text = currentCollectionName,
                    .Font = New Font("Segoe UI", 10.0F, FontStyle.Bold),
                    .BackColor = formatpnl.BackColor   ' hide line behind the text
                }

                Dim textSize As Size = TextRenderer.MeasureText(nameLbl.Text, nameLbl.Font)

                ' center text within the underline
                Dim nameX As Integer = lineLeft + (lineWidth - textSize.Width) \ 2
                If nameX < lineLeft Then nameX = lineLeft   ' safety

                ' small gap between text and line
                Dim nameY As Integer = lineTop - textSize.Height - 2

                nameLbl.Location = New Point(nameX, nameY)
                formatpnl.Controls.Add(nameLbl)
            End If

            y = line.Bottom + 5
        Next
    End Sub


    ' ---------- helper: capture whole sticker panel ----------
    Private Function CaptureStickerToBitmap() As Bitmap
        If formatprintpnl Is Nothing OrElse formatprintpnl.Width <= 0 OrElse formatprintpnl.Height <= 0 Then
            Return Nothing
        End If

        Dim bmp As New Bitmap(formatprintpnl.Width, formatprintpnl.Height)
        formatprintpnl.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
        Return bmp
    End Function

    ' ---------- Cancel button ----------
    Private Sub cancelbtn_Click(sender As Object, e As EventArgs) Handles cancelbtn.Click
        Dim parentPanel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub


    ' ============================================================
    '   PUBLIC HELPERS
    ' ============================================================

    ' Single sticker (your existing behaviour: 1 design, 6 copies per page)
    Public Sub LoadSingleSticker(qr As Bitmap, enc As String, Optional collectionName As String = "")
        multiBatch = False
        CurrentSlotIndex = 6   ' default = all 6 copies

        LoadQR(qr, enc, collectionName)
        singleStickerBitmap = CaptureStickerToBitmap()
    End Sub

    ' Batch stickers (Units form → multiple checked rows)
    Public Sub LoadStickerBatch(qrBitmaps As List(Of Bitmap),
                                encStrings As List(Of String),
                                collectionNames As List(Of String))

        multiBatch = True
        CurrentSlotIndex = 6   ' ignored in batch mode

        allStickers.Clear()
        currentStickerIndex = 0

        If qrBitmaps Is Nothing OrElse qrBitmaps.Count = 0 Then Exit Sub

        Dim cnt As Integer = qrBitmaps.Count

        For i As Integer = 0 To cnt - 1
            Dim qr As Bitmap = qrBitmaps(i)
            Dim enc As String = If(encStrings IsNot Nothing AndAlso i < encStrings.Count,
                                   encStrings(i),
                                   String.Empty)
            Dim coll As String = If(collectionNames IsNot Nothing AndAlso i < collectionNames.Count,
                                    collectionNames(i),
                                    String.Empty)

            ' Build layout for THIS unit (QR + collection name), then capture it
            LoadQR(qr, enc, coll)
            Dim sticker As Bitmap = CaptureStickerToBitmap()
            If sticker IsNot Nothing Then
                allStickers.Add(sticker)
            End If
        Next
    End Sub

    ' Open preview window
    Public Sub ShowStickerPreview()
        If multiBatch Then
            If allStickers Is Nothing OrElse allStickers.Count = 0 Then
                MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            currentStickerIndex = 0 ' reset for new preview

            ' 🔹 compute total pages for batch (6 stickers per page)
            totalPages = CInt(Math.Ceiling(allStickers.Count / 6.0R))
        Else
            If singleStickerBitmap Is Nothing Then
                MessageBox.Show("Nothing to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            totalPages = 1
        End If

        Dim f As New StickerPreviewForm(Me, printDoc, totalPages)
        f.ShowDialog()
    End Sub

    ' existing PRINT button on this control (single-unit screen)
    Private Sub printbtn_Click(sender As Object, e As EventArgs) Handles printbtn.Click
        multiBatch = False
        singleStickerBitmap = CaptureStickerToBitmap()
        ShowStickerPreview()
    End Sub

    ' ---------- PrintDocument drawing ----------
    Private Sub printDoc_PrintPage(sender As Object,
                               e As Printing.PrintPageEventArgs) _
                               Handles printDoc.PrintPage

        Dim g As Graphics = e.Graphics
        g.Clear(Color.White)

        Dim pageRect As Rectangle = e.PageBounds

        ' 2 columns x 3 rows grid
        Dim cols As Integer = 2
        Dim rows As Integer = 3

        Dim cellW As Integer = pageRect.Width \ cols
        Dim cellH As Integer = pageRect.Height \ rows

        If multiBatch Then
            ' ---------- BATCH MODE (Units form) ----------
            If allStickers Is Nothing OrElse allStickers.Count = 0 Then
                e.HasMorePages = False
                Return
            End If

            For row As Integer = 0 To rows - 1
                For col As Integer = 0 To cols - 1

                    If currentStickerIndex >= allStickers.Count Then
                        Exit For
                    End If

                    Dim bmp As Bitmap = allStickers(currentStickerIndex)
                    currentStickerIndex += 1
                    If bmp Is Nothing Then Continue For

                    Dim cellRect As New Rectangle(
                        pageRect.Left + col * cellW,
                        pageRect.Top + row * cellH,
                        cellW,
                        cellH
                    )

                    Dim pad As Integer = 4
                    Dim innerRect As New Rectangle(
                        cellRect.Left + pad,
                        cellRect.Top + pad,
                        cellRect.Width - pad * 2,
                        cellRect.Height - pad * 2
                    )

                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

                    ' stretch sticker to fill the slot
                    g.DrawImage(bmp, innerRect)
                Next
            Next

            ' More pages if still stickers left
            e.HasMorePages = (currentStickerIndex < allStickers.Count)

        Else
            ' ---------- SINGLE MODE (1 design duplicated) ----------
            If singleStickerBitmap Is Nothing Then
                e.HasMorePages = False
                Return
            End If

            Dim bmp As Bitmap = singleStickerBitmap
            Dim singleSlot As Boolean = (CurrentSlotIndex >= 0 AndAlso CurrentSlotIndex <= 5)

            For row As Integer = 0 To rows - 1
                For col As Integer = 0 To cols - 1

                    Dim slotIndex As Integer = row * cols + col
                    If singleSlot AndAlso slotIndex <> CurrentSlotIndex Then
                        Continue For
                    End If

                    Dim cellRect As New Rectangle(
                        pageRect.Left + col * cellW,
                        pageRect.Top + row * cellH,
                        cellW,
                        cellH
                    )

                    Dim pad As Integer = 4
                    Dim innerRect As New Rectangle(
                        cellRect.Left + pad,
                        cellRect.Top + pad,
                        cellRect.Width - pad * 2,
                        cellRect.Height - pad * 2
                    )

                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

                    ' stretch sticker to fill the slot
                    g.DrawImage(bmp, innerRect)
                Next
            Next

            e.HasMorePages = False
        End If
    End Sub


    ' ==============================
    '  Custom print preview with slot selection + page nav + zoom
    ' ==============================
    Public Class StickerPreviewForm
        Inherits Form

        Private preview As New PrintPreviewControl()
        Private cboSlot As New ComboBox()
        Private btnPrint As New Button()
        Private btnClose As New Button()

        Private btnZoomIn As New Button()
        Private btnZoomOut As New Button()
        Private lblZoom As New Label()

        Private btnPrevPage As New Button()
        Private btnNextPage As New Button()
        Private lblPage As New Label()

        Private ownerView As QRPrintView
        Private doc As PrintDocument

        Private totalPages As Integer
        Private currentPage As Integer = 0   ' 0-based index

        Public Sub New(owner As QRPrintView, document As PrintDocument, totalPages As Integer)
            Me.ownerView = owner
            Me.doc = document
            Me.totalPages = Math.Max(1, totalPages)

            Me.Text = "Print preview"
            Me.WindowState = FormWindowState.Maximized

            ' ---- PREVIEW CONTROL ----
            preview.Document = doc
            preview.Dock = DockStyle.Fill

            preview.AutoZoom = False
            preview.Zoom = 1.0R   ' 100%

            ' start on first page
            preview.StartPage = 0

            ' ---- TOP BAR ----
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
                "Top Left",          ' 0
                "Top Right",         ' 1
                "Middle Left",       ' 2
                "Middle Right",      ' 3
                "Bottom Left",       ' 4
                "Bottom Right",      ' 5
                "All (6 stickers)"   ' 6
            })

            If ownerView.CurrentSlotIndex >= 0 AndAlso
               ownerView.CurrentSlotIndex < cboSlot.Items.Count Then
                cboSlot.SelectedIndex = ownerView.CurrentSlotIndex
            Else
                cboSlot.SelectedIndex = 6
            End If

            AddHandler cboSlot.SelectedIndexChanged,
                Sub()
                    ownerView.CurrentSlotIndex = cboSlot.SelectedIndex
                    preview.InvalidatePreview()
                End Sub

            ' ---- PAGE NAV BUTTONS ----
            btnPrevPage.Text = "<"
            btnPrevPage.Width = 30
            btnPrevPage.Height = 24
            btnPrevPage.Margin = New Padding(15, 7, 0, 0)

            btnNextPage.Text = ">"
            btnNextPage.Width = 30
            btnNextPage.Height = 24
            btnNextPage.Margin = New Padding(3, 7, 0, 0)

            lblPage.AutoSize = True
            lblPage.Margin = New Padding(5, 10, 0, 0)

            AddHandler btnPrevPage.Click,
                Sub()
                    If currentPage > 0 Then
                        currentPage -= 1
                        preview.StartPage = currentPage
                        UpdatePageLabel()
                    End If
                End Sub

            AddHandler btnNextPage.Click,
                Sub()
                    If currentPage < totalPages - 1 Then
                        currentPage += 1
                        preview.StartPage = currentPage
                        UpdatePageLabel()
                    End If
                End Sub

            ' ---- ZOOM BUTTONS ----
            btnZoomOut.Text = "−"
            btnZoomOut.Width = 30
            btnZoomOut.Height = 24
            btnZoomOut.Margin = New Padding(15, 7, 0, 0)

            btnZoomIn.Text = "+"
            btnZoomIn.Width = 30
            btnZoomIn.Height = 24
            btnZoomIn.Margin = New Padding(3, 7, 0, 0)

            lblZoom.AutoSize = True
            lblZoom.Margin = New Padding(5, 10, 0, 0)
            lblZoom.Text = "100%"

            AddHandler btnZoomIn.Click,
                Sub()
                    Dim z As Double = preview.Zoom * 1.1R
                    If z > 5.0R Then z = 5.0R
                    preview.Zoom = z
                    UpdateZoomLabel()
                End Sub

            AddHandler btnZoomOut.Click,
                Sub()
                    Dim z As Double = preview.Zoom / 1.1R
                    If z < 0.1R Then z = 0.1R
                    preview.Zoom = z
                    UpdateZoomLabel()
                End Sub

            ' ---- PRINT / CLOSE ----
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

            ' ---- INIT LABELS ----
            UpdatePageLabel()
            UpdateZoomLabel()

            ' ---- ADD CONTROLS TO TOP PANEL ----
            topPanel.Controls.Add(lbl)
            topPanel.Controls.Add(cboSlot)

            ' page nav: < Page X of Y >
            topPanel.Controls.Add(btnPrevPage)
            topPanel.Controls.Add(lblPage)
            topPanel.Controls.Add(btnNextPage)

            ' zoom: - 110% +
            topPanel.Controls.Add(btnZoomOut)
            topPanel.Controls.Add(lblZoom)
            topPanel.Controls.Add(btnZoomIn)

            topPanel.Controls.Add(btnPrint)
            topPanel.Controls.Add(btnClose)


            Me.Controls.Add(preview)
            Me.Controls.Add(topPanel)
        End Sub

        Private Sub UpdateZoomLabel()
            lblZoom.Text = CInt(preview.Zoom * 100).ToString() & "%"
        End Sub

        Private Sub UpdatePageLabel()
            lblPage.Text = $"Page {currentPage + 1} of {totalPages}"
        End Sub

    End Class

End Class
