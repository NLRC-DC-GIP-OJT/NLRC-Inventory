Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports QRCoder
Imports System.Security.Cryptography

Public Class ViewUnit

    Private mdl As New model()
    Private currentDevices As New List(Of Integer)
    Private deviceDisplayNames As New Dictionary(Of Integer, String)
    Private deviceFullSpecs As New Dictionary(Of Integer, String)
    Private currentUnitId As Integer = 0

    ' 🔹 Status per device (pointer → status string)
    Private deviceStatuses As New Dictionary(Of Integer, String)

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
    Public Sub LoadUnit(unitId As Integer, unitName As String, assignedToId As Integer, devices As DataTable)
        ' Set the current unit ID for QR generation
        currentUnitId = unitId

        ' Set unit name and assigned person
        unitnametxt.Text = unitName
        assigntxt.Text = GetAssignedName(assignedToId)

        ' Clear existing device and specs panels
        deviceflowpnl.Controls.Clear()
        specsTable.Controls.Clear()

        ' Clear previous device lists
        currentDevices.Clear()
        deviceDisplayNames.Clear()
        deviceFullSpecs.Clear()
        deviceStatuses.Clear()

        ' Loop through devices and populate panels
        For Each dr As DataRow In devices.Rows
            Dim devId As Integer = CInt(dr("DeviceID"))

            ' 🔹 Get device status (prefer column, fallback to model lookup)
            Dim devStatus As String = ""
            If devices.Columns.Contains("status") Then
                devStatus = SafeStr(dr, "status")
            Else
                Try
                    Dim dev = mdl.GetDeviceByPointer(devId)
                    If dev IsNot Nothing AndAlso dev.Pointer > 0 Then
                        devStatus = If(dev.Status, "").ToString().Trim()
                    End If
                Catch ex As Exception
                    devStatus = ""
                End Try
            End If
            deviceStatuses(devId) = devStatus

            ' ============================
            ' 🔹 Build display text:
            '     Category - Brand  (if no model)
            '     Category - Brand - Model  (if model exists)
            ' ============================
            Dim baseText As String = ""

            If devices.Columns.Contains("category_name") AndAlso
               devices.Columns.Contains("brand_name") Then

                Dim cat = SafeStr(dr, "category_name")
                Dim brand = SafeStr(dr, "brand_name")
                Dim model = SafeStr(dr, "model") ' "" if NULL

                If model = "" Then
                    baseText = $"{cat} - {brand}"
                Else
                    baseText = $"{cat} - {brand} - {model}"
                End If
            Else
                ' fallback if those columns are not present
                Dim deviceAndSpecs = SafeStr(dr, "DeviceAndSpecs")
                baseText = ExtractBaseText(deviceAndSpecs)
            End If

            ' ============================
            ' 🔹 Build full specs:
            '     NSOC Name + Property No + ONLY actual specs
            ' ============================
            Dim specsParts As New List(Of String)

            If dr.Table.Columns.Contains("nsoc_name") Then
                Dim nsoc = SafeStr(dr, "nsoc_name")
                If nsoc <> "" Then specsParts.Add("NSOC Name:" & nsoc)
            End If

            If dr.Table.Columns.Contains("property_number") Then
                Dim prop = SafeStr(dr, "property_number")
                If prop <> "" Then specsParts.Add("Property No:" & prop)
            End If

            ' DeviceAndSpecs is like: Category - Brand (- Model) - <SPEC STRING>
            Dim rawSpecs = SafeStr(dr, "DeviceAndSpecs")
            Dim onlySpecs = ExtractOnlySpecs(rawSpecs)

            If onlySpecs <> "" Then
                specsParts.Add(onlySpecs)
            End If

            Dim fullSpecs As String = String.Join(";", specsParts)

            ' Store in dictionaries
            deviceDisplayNames(devId) = baseText
            deviceFullSpecs(devId) = fullSpecs
            currentDevices.Add(devId)

            ' Add device to panel (read-only)
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

        AddHandler btn.Click, Sub() ShowSpecsReadOnly(deviceId, fullSpecs)

        container.Controls.Add(btn)
        deviceflowpnl.Controls.Add(container)
    End Sub

    ' ========================
    ' DISPLAY SPECS AS LABELS
    ' ========================
    Private Sub ShowSpecsReadOnly(deviceId As Integer, fullSpecs As String)
        specsTable.Controls.Clear()
        specsTable.RowCount = 0

        ' 🔹 1) Show STATUS first (if available)
        Dim statusText As String = ""
        If deviceStatuses.ContainsKey(deviceId) Then
            statusText = deviceStatuses(deviceId)
        End If

        If Not String.IsNullOrWhiteSpace(statusText) Then
            specsTable.RowCount += 1

            Dim statusLbl As New Label With {
                .Text = "Status:",
                .Width = 120,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            }

            Dim statusValLbl As New Label With {
                .Text = statusText,
                .Width = 300,
                .Dock = DockStyle.Fill,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle
            }

            specsTable.Controls.Add(statusLbl, 0, specsTable.RowCount - 1)
            specsTable.Controls.Add(statusValLbl, 1, specsTable.RowCount - 1)
        End If

        ' 🔹 2) Parse and order the rest:
        '     NSOC Name → Property No → others
        Dim dict As Dictionary(Of String, String) = ParseSpecs(fullSpecs)

        Dim nsocVal As String = Nothing
        Dim propVal As String = Nothing
        Dim others As New Dictionary(Of String, String)

        For Each kvp In dict
            Dim key As String = kvp.Key.Trim()

            If key.Equals("NSOC Name", StringComparison.OrdinalIgnoreCase) Then
                nsocVal = kvp.Value
            ElseIf key.Equals("Property No", StringComparison.OrdinalIgnoreCase) Then
                propVal = kvp.Value
            Else
                If Not others.ContainsKey(key) Then
                    others.Add(key, kvp.Value)
                End If
            End If
        Next

        ' 🔹 2a) NSOC Name (immediately under Status)
        If nsocVal IsNot Nothing Then
            specsTable.RowCount += 1

            Dim keyLbl As New Label With {
                .Text = "NSOC Name:",
                .Width = 120,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            }

            Dim valLbl As New Label With {
                .Text = nsocVal,
                .Width = 300,
                .Dock = DockStyle.Fill,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle
            }

            specsTable.Controls.Add(keyLbl, 0, specsTable.RowCount - 1)
            specsTable.Controls.Add(valLbl, 1, specsTable.RowCount - 1)
        End If

        ' 🔹 2b) Property No (after NSOC)
        If propVal IsNot Nothing Then
            specsTable.RowCount += 1

            Dim keyLbl As New Label With {
                .Text = "Property No:",
                .Width = 120,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            }

            Dim valLbl As New Label With {
                .Text = propVal,
                .Width = 300,
                .Dock = DockStyle.Fill,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleLeft,
                .BackColor = Color.White,
                .BorderStyle = BorderStyle.FixedSingle
            }

            specsTable.Controls.Add(keyLbl, 0, specsTable.RowCount - 1)
            specsTable.Controls.Add(valLbl, 1, specsTable.RowCount - 1)
        End If

        ' 🔹 2c) Other specs
        For Each kvp In others
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
        If String.IsNullOrWhiteSpace(fullText) Then Return ""

        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None) _
                        .Select(Function(p) p.Trim()) _
                        .Where(Function(p) p <> "") _
                        .ToArray()

        Select Case parts.Length
            Case 0
                Return ""
            Case 1
                Return parts(0)
            Case 2
                ' Category - Brand
                Return parts(0) & " - " & parts(1)
            Case 3
                ' Assume Category - Brand - <SPECS> (no model)
                ' 👉 base text should just be Category - Brand
                Return parts(0) & " - " & parts(1)
            Case Else
                ' Category - Brand - Model - <specs...>
                Return parts(0) & " - " & parts(1) & " - " & parts(2)
        End Select
    End Function

    Private Function ExtractOnlySpecs(fullText As String) As String
        If String.IsNullOrWhiteSpace(fullText) Then Return String.Empty

        Dim parts = fullText.Split(New String() {" - "}, StringSplitOptions.None) _
                        .Select(Function(p) p.Trim()) _
                        .Where(Function(p) p <> "") _
                        .ToArray()

        If parts.Length <= 2 Then Return String.Empty

        If parts.Length = 3 Then
            ' Format: Category - Brand - <SPECS> (no model)
            Return parts(2)
        End If

        ' 4+ → Category - Brand - Model - <SPECS...>
        Return String.Join(" - ", parts.Skip(3)).Trim()
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

    Private Sub btnGenerateQR_Click(sender As Object, e As EventArgs) Handles btnGenerateQR.Click
        If currentUnitId = 0 Then
            MessageBox.Show("Unit ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        mainpanelqr.Controls.Clear()

        Dim qrControl As New QRView() With {
            .Dock = DockStyle.Fill
        }

        mainpanelqr.Controls.Add(qrControl)

        ' Pass ONLY the raw Unit ID
        qrControl.ShowQR(currentUnitId)

        mainpanelqr.Visible = True
        mainpanelqr.BringToFront()
    End Sub

    ' ✅ Safely get a trimmed string from a DataRow (handles DBNull / missing column)
    Private Function SafeStr(row As DataRow, colName As String) As String
        If Not row.Table.Columns.Contains(colName) Then Return ""
        If IsDBNull(row(colName)) OrElse row(colName) Is Nothing Then Return ""
        Return row(colName).ToString().Trim()
    End Function

End Class
