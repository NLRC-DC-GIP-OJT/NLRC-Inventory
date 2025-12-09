Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text


Public Class ViewInclude
    Inherits UserControl

    ' Top-level UI pieces
    Private main As TableLayoutPanel
    Private lblDeviceType As Label
    Private lblSpecsWhere As Label
    Private lblSpecs As Label
    Private lblIncludesTitle As Label
    Private flow As FlowLayoutPanel

    Public Sub New()
        InitializeComponent()
        SetupLayout()
    End Sub

    Private Sub SetupLayout()
        Me.BackColor = Color.White

        ' Main layout: 1 column, several rows
        main = New TableLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 1,
            .RowCount = 4,
            .Padding = New Padding(12),
            .AutoScroll = True
        }

        main.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Device type
        main.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Specs (title + body)
        main.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Includes title
        main.RowStyles.Add(New RowStyle(SizeType.Percent, 100)) ' Includes flow

        ' 1) Device type label (Desktop / Laptop)
        lblDeviceType = New Label() With {
            .AutoSize = True,
            .Font = New Font(Me.Font.FontFamily, Me.Font.Size + 3, FontStyle.Bold),
            .ForeColor = Color.Black,
            .Text = ""
        }

        ' 2) Specs title + body
        lblSpecsWhere = New Label() With {
            .AutoSize = True,
            .Font = New Font(Me.Font, FontStyle.Bold),
            .Text = "SPECS:"
        }

        lblSpecs = New Label() With {
            .AutoSize = True,
            .MaximumSize = New Size(550, 0),
            .Text = ""
        }

        Dim specsPanel As New Panel() With {
            .AutoSize = True,
            .AutoSizeMode = AutoSizeMode.GrowAndShrink
        }
        lblSpecsWhere.Location = New Point(0, 0)
        specsPanel.Controls.Add(lblSpecsWhere)
        lblSpecs.Location = New Point(10, lblSpecsWhere.Bottom + 3)
        specsPanel.Controls.Add(lblSpecs)

        ' 3) Includes title
        lblIncludesTitle = New Label() With {
            .AutoSize = True,
            .Font = New Font(Me.Font, FontStyle.Bold),
            .Text = "INCLUDED:"
        }

        ' 4) FlowLayoutPanel for includes (your old flow)
        flow = New FlowLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .AutoScroll = True,
            .FlowDirection = FlowDirection.TopDown,
            .WrapContents = False,
            .Padding = New Padding(5)
        }

        main.Controls.Add(lblDeviceType, 0, 0)
        main.Controls.Add(specsPanel, 0, 1)
        main.Controls.Add(lblIncludesTitle, 0, 2)
        main.Controls.Add(flow, 0, 3)

        Me.Controls.Add(main)
    End Sub

    ' === Public method called from Reports ===
    ' deviceType = "Desktop" / "Laptop"
    ' specsText  = built summary specs (NSOC + BRAND/MODEL/FEATURE + other lines)
    ' includeText = your INCLUDED column text
    Public Sub LoadDetails(deviceType As String, specsText As String, includeText As String)
        ' DEVICE TYPE
        If String.IsNullOrWhiteSpace(deviceType) Then
            lblDeviceType.Text = "(Unknown device type)"
        Else
            lblDeviceType.Text = deviceType.ToUpperInvariant()
        End If

        ' SPECS — reformat here (Brand, Processor, RAM, etc.)
        Dim formattedSpecs As String = FormatSpecs(specsText)

        If String.IsNullOrWhiteSpace(formattedSpecs) Then
            lblSpecs.Text = "No specs available."
        Else
            lblSpecs.Text = formattedSpecs
        End If

        ' INCLUDED (with your checkbox layout)
        LoadIncludes(includeText)
    End Sub

    ' Reformat the raw specsText coming from Reports
    Private Function FormatSpecs(specsText As String) As String
        If String.IsNullOrWhiteSpace(specsText) Then Return ""

        Dim sb As New StringBuilder()

        ' Normalize line breaks then process line by line
        Dim lines = specsText.Replace(vbCrLf, vbLf).Split(
        New String() {vbLf},
        StringSplitOptions.None
    )

        For Each rawLine In lines
            Dim line = rawLine.Trim()
            If line = "" Then Continue For

            ' Only special-handle the BRAND/MODEL/FEATURE line
            If line.StartsWith("BRAND/MODEL/FEATURE:", StringComparison.OrdinalIgnoreCase) Then

                Dim rawBmf As String = line.Substring("BRAND/MODEL/FEATURE:".Length).Trim()
                If rawBmf = "" Then Continue For

                ' Split into ; segments
                Dim partsRaw() As String = rawBmf.Split(";"c)
                Dim parts As New List(Of String)
                For Each p In partsRaw
                    Dim t = p.Trim()
                    If t <> "" Then parts.Add(t)
                Next
                If parts.Count = 0 Then Continue For

                Dim procMarker As String = " - Processor:"
                Dim firstPart As String = parts(0)

                Dim brand As String = ""
                Dim model As String = ""
                Dim processor As String = ""

                ' First segment, usually "Brand Model ... - Processor: ..."
                Dim idxProc As Integer = firstPart.IndexOf(procMarker, StringComparison.OrdinalIgnoreCase)
                If idxProc >= 0 Then
                    Dim left As String = firstPart.Substring(0, idxProc).Trim()
                    Dim right As String = firstPart.Substring(idxProc + procMarker.Length).Trim()

                    ' LEFT → Brand (first word) + Model (rest)
                    If left <> "" Then
                        Dim tokens() As String = left.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                        If tokens.Length > 0 Then
                            brand = tokens(0)
                            If tokens.Length > 1 Then
                                Dim b As New StringBuilder()
                                For i As Integer = 1 To tokens.Length - 1
                                    If b.Length > 0 Then b.Append(" ")
                                    b.Append(tokens(i))
                                Next
                                model = b.ToString()
                            End If
                        Else
                            brand = left
                        End If
                    End If

                    ' RIGHT → Processor
                    If right <> "" Then
                        If right.StartsWith("Processor", StringComparison.OrdinalIgnoreCase) Then
                            processor = right
                        Else
                            processor = "Processor: " & right
                        End If
                    End If
                Else
                    ' No " - Processor:" → treat first part as Brand+Model only
                    Dim tokens() As String = firstPart.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                    If tokens.Length > 0 Then
                        brand = tokens(0)
                        If tokens.Length > 1 Then
                            Dim b As New StringBuilder()
                            For i As Integer = 1 To tokens.Length - 1
                                If b.Length > 0 Then b.Append(" ")
                                b.Append(tokens(i))
                            Next
                            model = b.ToString()
                        End If
                    Else
                        brand = firstPart
                    End If
                End If

                ' Append nicely formatted lines
                If brand <> "" Then sb.AppendLine("Brand: " & brand)
                If model <> "" Then sb.AppendLine("Model: " & model)
                If processor <> "" Then sb.AppendLine(processor)

                ' Remaining parts: RAM, Storage, Graphics, etc.
                For i As Integer = 1 To parts.Count - 1
                    Dim seg As String = parts(i)
                    If seg <> "" Then sb.AppendLine(seg)
                Next

            Else
                ' All other lines unchanged (NSOC, PROPERTY NO., DATEs, etc.)
                sb.AppendLine(line)
            End If
        Next

        Return sb.ToString().TrimEnd()
    End Function




    ' Your old method, now used internally
    Private Sub LoadIncludes(includeText As String)
        flow.Controls.Clear()

        If String.IsNullOrWhiteSpace(includeText) Then
            Dim lbl As New Label() With {
                .Text = "No included items recorded.",
                .AutoSize = True,
                .ForeColor = Color.Gray
            }
            flow.Controls.Add(lbl)
            Return
        End If

        ' known device templates for nicer labels
        Dim templates As New Dictionary(Of String, String())(StringComparer.OrdinalIgnoreCase) From {
            {"monitor", New String() {
                "Clean monitor",
                "Working ports",
                "No cracks / physical damage"
            }},
            {"mouse", New String() {
                "Left/right click working",
                "Scroll wheel working",
                "Cable / wireless connection OK"
            }},
            {"keyboard", New String() {
                "All keys working",
                "No stuck keys",
                "Legends / letters readable"
            }},
            {"headset", New String() {
                "Left/right speakers working",
                "Microphone working",
                "No loose wiring"
            }}
        }

        ' Example INCLUDED formats: "Monitor, Mouse, Keyboard" or "Monitor;Mouse"
        Dim rawParts = includeText.Split(
            New Char() {","c, ";"c},
            StringSplitOptions.RemoveEmptyEntries
        )

        For Each raw In rawParts
            Dim item = raw.Trim()
            If item = "" Then Continue For

            ' panel per included device (Monitor, Mouse, etc.)
            Dim container As New Panel() With {
                .AutoSize = True,
                .AutoSizeMode = AutoSizeMode.GrowAndShrink,
                .Margin = New Padding(0, 0, 0, 10)
            }

            ' device name label (bold)
            Dim title As New Label() With {
                .Text = item,
                .Font = New Font(Me.Font, FontStyle.Bold),
                .AutoSize = True
            }
            title.Location = New Point(0, 0)
            container.Controls.Add(title)

            Dim top As Integer = title.Bottom + 4

            Dim subLabels() As String = Nothing
            If templates.TryGetValue(item, subLabels) Then
                ' known device: show detailed checkboxes
                For Each s In subLabels
                    Dim chk As New CheckBox() With {
                        .Text = s,
                        .AutoSize = True,
                        .Location = New Point(15, top),
                        .Checked = True       ' start all checked
                    }
                    container.Controls.Add(chk)
                    top = chk.Bottom + 2
                Next
            Else
                ' unknown item: generic one checkbox
                Dim chk As New CheckBox() With {
                    .Text = "Included / OK",
                    .AutoSize = True,
                    .Location = New Point(15, top),
                    .Checked = True
                }
                container.Controls.Add(chk)
            End If

            flow.Controls.Add(container)
        Next
    End Sub

End Class
