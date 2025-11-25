Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms

Public Class ViewUnit

    Private mdl As New model()
    Private currentDevices As New List(Of Integer)
    Private deviceDisplayNames As New Dictionary(Of Integer, String)
    Private deviceFullSpecs As New Dictionary(Of Integer, String)

    ' Specs table
    Private specsTable As New TableLayoutPanel With {
        .AutoSize = True,
        .ColumnCount = 2,
        .RowCount = 0,
        .Dock = DockStyle.Fill,
        .Padding = New Padding(10)
    }

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    Private Sub ViewUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill
        InitializeLayoutScaling()

        specsflowpnl.Controls.Clear()
        specsflowpnl.Controls.Add(specsTable)
    End Sub

    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
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
            If Not originalBounds.ContainsKey(ctrl) Then
                originalBounds.Add(ctrl, ctrl.Bounds)
            End If

            If ctrl.HasChildren Then
                StoreOriginalBounds(ctrl)
            End If
        Next
    End Sub

    Private Sub ApplyScale(scaleX As Single, scaleY As Single)
        Me.SuspendLayout()

        For Each kvp As KeyValuePair(Of Control, Rectangle) In originalBounds
            Dim ctrl As Control = kvp.Key
            If ctrl Is Nothing OrElse ctrl.IsDisposed Then Continue For

            Dim r As Rectangle = kvp.Value
            ctrl.Bounds = New Rectangle(
                CInt(r.X * scaleX),
                CInt(r.Y * scaleY),
                CInt(r.Width * scaleX),
                CInt(r.Height * scaleY)
            )

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
    End Sub

    ' ========================
    ' PUBLIC API: Load Unit
    ' ========================
    Public Sub LoadUnit(unitName As String, assignedToId As Integer, devices As DataTable)
        unitnametxt.Text = unitName
        assigntxt.Text = GetAssignedName(assignedToId)

        deviceflowpnl.Controls.Clear()
        specsTable.Controls.Clear()

        currentDevices.Clear()
        deviceDisplayNames.Clear()
        deviceFullSpecs.Clear()

        For Each dr As DataRow In devices.Rows
            Dim devId As Integer = CInt(dr("DeviceID"))
            Dim baseText As String = ExtractBaseText(dr("DeviceAndSpecs").ToString())

            ' Build full specs: NSOC Name + Property# + remaining specs
            Dim specsParts As New List(Of String)
            If dr.Table.Columns.Contains("nsoc_name") AndAlso Not IsDBNull(dr("nsoc_name")) AndAlso dr("nsoc_name").ToString().Trim() <> "" Then
                specsParts.Add("NSOC Name:" & dr("nsoc_name").ToString().Trim())
            End If
            If dr.Table.Columns.Contains("property_number") AndAlso Not IsDBNull(dr("property_number")) AndAlso dr("property_number").ToString().Trim() <> "" Then
                specsParts.Add("Property No:" & dr("property_number").ToString().Trim())
            End If
            Dim rawSpecs = dr("DeviceAndSpecs").ToString()
            Dim specsOnly = CleanSpecs(rawSpecs)
            If specsOnly <> "" Then specsParts.Add(specsOnly)

            Dim fullSpecs As String = String.Join(";", specsParts)

            deviceDisplayNames(devId) = baseText
            deviceFullSpecs(devId) = fullSpecs
            currentDevices.Add(devId)

            AddDeviceToPanelReadOnly(devId, baseText, fullSpecs)
        Next
    End Sub

    ' ========================
    ' READ-ONLY DEVICE PANEL
    ' ========================
    Private Sub AddDeviceToPanelReadOnly(deviceId As Integer, displayText As String, fullSpecs As String)
        Dim container As New Panel With {
            .Height = 45,
            .Width = Math.Max(200, deviceflowpnl.ClientSize.Width - 15),
            .Margin = New Padding(5)
        }

        Dim btn As New Button With {
            .Text = displayText,
            .Tag = fullSpecs,
            .Dock = DockStyle.Fill,
            .Height = 40,
            .BackColor = Color.LightGray,
            .TextAlign = ContentAlignment.MiddleLeft,
            .AutoEllipsis = True
        }

        AddHandler btn.Click, Sub() ShowSpecsReadOnly(fullSpecs)

        container.Controls.Add(btn)
        deviceflowpnl.Controls.Add(container)
    End Sub

    ' ========================
    ' DISPLAY SPECS AS LABELS
    ' ========================
    Private Sub ShowSpecsReadOnly(fullSpecs As String)
        specsTable.Controls.Clear()
        specsTable.RowCount = 0

        Dim dict As Dictionary(Of String, String) = ParseSpecs(fullSpecs)

        For Each kvp In dict
            specsTable.RowCount += 1

            Dim keyLbl As New Label With {
                .Text = kvp.Key & ":",
                .Width = 120,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            }

            Dim valLbl As New Label With {
                .Text = kvp.Value,
                .Width = 300,
                .Dock = DockStyle.Fill,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle
            }

            specsTable.Controls.Add(keyLbl, 0, specsTable.RowCount - 1)
            specsTable.Controls.Add(valLbl, 1, specsTable.RowCount - 1)
        Next
    End Sub

    ' ========================
    ' HELPERS
    ' ========================
    Private Function ParseSpecs(fullSpecs As String) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)
        If String.IsNullOrWhiteSpace(fullSpecs) Then Return dict

        Dim items = fullSpecs.Split(";"c)
        For Each it As String In items
            Dim p = it.Split(":"c)
            If p.Length >= 2 Then
                Dim key = p(0).Trim()
                Dim val = String.Join(":", p.Skip(1)).Trim()
                If Not dict.ContainsKey(key) Then dict(key) = val
            End If
        Next
        Return dict
    End Function

    Private Function ExtractBaseText(fullText As String) As String
        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None)
        If parts.Length < 3 Then Return fullText.Trim()
        Return $"{parts(0).Trim()} - {parts(1).Trim()} - {parts(2).Trim()}"
    End Function

    Private Function CleanSpecs(rawSpecs As String) As String
        If String.IsNullOrWhiteSpace(rawSpecs) Then Return ""
        Dim cleaned As String = System.Text.RegularExpressions.Regex.Replace(rawSpecs, "(?i)\s*NSOC\s*[:=][^;]+", "")
        cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, "(?i)\s*Property\s*[:=][^;]+", "")
        cleaned = System.Text.RegularExpressions.Regex.Replace(cleaned, ";;+", ";")
        Return cleaned.Trim(" "c, ";"c)
    End Function

    Private Function GetAssignedName(assignedToId As Integer) As String
        Dim assignments As DataTable = mdl.GetAssignments()
        For Each row As DataRow In assignments.Rows
            If CInt(row("user_id")) = assignedToId Then Return row("Full name").ToString()
        Next
        Return ""
    End Function

    ' ========================
    ' CLOSE PANEL
    ' ========================
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

End Class
