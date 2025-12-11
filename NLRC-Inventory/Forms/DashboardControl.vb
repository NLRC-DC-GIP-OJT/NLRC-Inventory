Imports System.Data
Imports System.Linq
Imports System.Drawing
Imports System.Windows.Forms

Public Class DashboardControl

    Private LoggedUser As String
    Private model As New model()

    ' ========================
    ' AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False
    Private isCatGridReady As Boolean = False


    ' ========================
    ' Constructors
    ' ========================
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(user As String)
        InitializeComponent()
        LoggedUser = user
    End Sub

    ' ========================
    ' AUTO-RESIZE SUPPORT
    ' ========================
    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        If Me.DesignMode Then Return   ' don’t run inside designer

        ' Remember the original size of the dashboard
        originalSize = Me.Size

        ' Remember original bounds of every child control
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

            ' Optional: scale fonts a bit
            If ctrl.Font IsNot Nothing Then
                Dim f As Font = ctrl.Font
                Dim newSize As Single = f.SizeInPoints * Math.Min(scaleX, scaleY)
                If newSize > 4 Then
                    ctrl.Font = New Font(f.FontFamily, newSize, f.Style)
                End If
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
    ' Dashboard Load
    ' ========================
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Make the dashboard fill its parent form / container
        Me.Dock = DockStyle.Fill

        ' Initialize scaling based on the designer layout
        InitializeLayoutScaling()

        ' Timer for auto-refreshing charts
        Timer2.Interval = 30000 ' every 30 seconds
        Timer2.Start()

        ' Defer drawing until after layout is finished
        Me.BeginInvoke(New Action(AddressOf InitializeDashboard))
    End Sub

    ' Draw everything once (first load)
    Private Sub InitializeDashboard()
        ' Draw initial charts
        DrawSerialPieChart()
        DrawDeviceAssignmentStatusGraph()
        DrawDeviceOperationalStatusGraph()

        ' Update totals
        UpdateTotals()

        ' Load DataGridView with category graphs (now correct width)
        LoadCategoryDeviceGrid()

        ' Load recent activities
        LoadRecentUnitActivities()
        DrawUnitAssignmentGraph()
        LoadRecentAddedActivity()

        LoadDeviceHistoryGrid()
    End Sub

    ' ========================
    ' Timer Tick for Charts (Auto-Refresh)
    ' ========================
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        DrawSerialPieChart()
        DrawDeviceAssignmentStatusGraph()
        DrawDeviceOperationalStatusGraph()

        ' Refresh totals
        UpdateTotals()

        ' Refresh category graphs and tables
        LoadCategoryDeviceGrid()
        LoadRecentUnitActivities()
        DrawUnitAssignmentGraph()
        LoadRecentAddedActivity()
    End Sub

    ' ========================
    ' Update total devices and units
    ' ========================
    Private Sub UpdateTotals()
        totdevices.Text = model.GetTotalDevices().ToString()
        totunits.Text = model.GetTotalUnits().ToString()
        totpersonnel.Text = model.GetTotalPersonnel().ToString()
    End Sub

    ' ========================
    ' DRAW SERIAL PIE CHART ON PANEL10
    ' ========================
    Private Sub DrawSerialPieChart()
        Dim data = model.GetSerialCounts()
        Dim withSerial As Integer = data.WithSerial
        Dim withoutSerial As Integer = data.WithoutSerial

        Dim total As Integer = withSerial + withoutSerial
        If total = 0 Then total = 1

        Dim bmp As New Bitmap(Panel10.Width, Panel10.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim padding As Integer = 40
        Dim rectSize As Integer = Math.Min(Panel10.Width, Panel10.Height) - 2 * padding
        Dim rectX As Integer = (Panel10.Width - rectSize) \ 2
        Dim rectY As Integer = (Panel10.Height - rectSize) \ 2 - 10
        Dim rect As New Rectangle(rectX, rectY, rectSize, rectSize)

        Dim angleWith As Single = CSng(withSerial) / total * 360
        Dim angleWithout As Single = CSng(withoutSerial) / total * 360

        g.FillPie(Brushes.Green, rect, 0, angleWith)
        g.DrawPie(Pens.Black, rect, 0, angleWith)

        g.FillPie(Brushes.Red, rect, angleWith, angleWithout)
        g.DrawPie(Pens.Black, rect, angleWith, angleWithout)

        Dim font As New Font("Arial", 10, FontStyle.Bold)
        g.DrawString($"With Serial: {withSerial}", font, Brushes.Black,
                     (Panel10.Width - g.MeasureString($"With Serial: {withSerial}", font).Width) / 2,
                     rectY + rectSize + 5)
        g.DrawString($"Without Serial: {withoutSerial}", font, Brushes.Black,
                     (Panel10.Width - g.MeasureString($"Without Serial: {withoutSerial}", font).Width) / 2,
                     rectY + rectSize + 25)

        Panel10.BackgroundImage = bmp
        Panel10.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    ' ========================
    ' GRAPH 1: ASSIGNMENT STATUS on Panel8
    '   Unassigned = Working + Not Working
    '   Assigned   = Assigned + Maintenance (For Repair)
    '   (For Disposal is NOT shown here)
    ' ========================
    Private Sub DrawDeviceAssignmentStatusGraph()
        Dim data = model.GetAssignmentStatusCounts()

        Dim labels As String() = {"Unassigned", "Assigned", "For Disposal"}
        Dim values As Integer() = {
        data("Unassigned"),
        data("Assigned"),
        data("For Disposal")
    }

        Dim colors As Color() = {
        Color.FromArgb(129, 212, 250),
        Color.FromArgb(128, 222, 234),
        Color.FromArgb(200, 230, 201)
    }

        Dim bmp As New Bitmap(Panel8.Width, Panel8.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim marginTop = 30, marginBottom = 50, marginLeft = 10, marginRight = 10
        Dim chartHeight = Panel8.Height - marginTop - marginBottom
        Dim chartWidth = Panel8.Width - marginLeft - marginRight

        Dim n = values.Length
        Dim gap = 5
        Dim barWidth As Integer = CInt((chartWidth - gap * (n - 1)) / n)

        Dim maxVal As Integer = values.Max()
        If maxVal = 0 Then maxVal = 1

        Dim minBarHeight = 5
        Dim labelFont As New Font("Arial", 9)
        Dim valueFont As New Font("Arial", 10, FontStyle.Bold)

        For i = 0 To n - 1
            Dim x = marginLeft + i * (barWidth + gap)
            Dim barHeight = CInt(chartHeight * values(i) / maxVal)
            If values(i) > 0 AndAlso barHeight < minBarHeight Then barHeight = minBarHeight
            Dim y = marginTop + chartHeight - barHeight

            Using b As New SolidBrush(colors(i))
                g.FillRectangle(b, x, y, barWidth, barHeight)
            End Using
            g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

            Dim valText = values(i).ToString()
            g.DrawString(valText, valueFont, Brushes.Black,
                     x + (barWidth - g.MeasureString(valText, valueFont).Width) / 2,
                     y - 20)

            Dim segmentWidth As Double = Panel8.Width / n
            Dim labelX As Double = i * segmentWidth + (segmentWidth - g.MeasureString(labels(i), labelFont).Width) / 2
            Dim labelY As Integer = marginTop + chartHeight + 5
            g.DrawString(labels(i), labelFont, Brushes.Black, CSng(labelX), labelY)
        Next

        Panel8.BackgroundImage = bmp
        Panel8.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub







    ' ========================
    ' LOAD CATEGORY DEVICE GRID WITH MULTICOLOR MINI GRAPHS AND COUNTS
    ' ========================
    Private Sub LoadCategoryDeviceGrid()
        Dim dt As DataTable = model.GetDevicesPerCategory()
        If dt Is Nothing Then
            catdgv.DataSource = Nothing
            Return
        End If

        ' Add a ColorCode column to store HEX value (hidden in grid)
        If Not dt.Columns.Contains("ColorCode") Then
            dt.Columns.Add("ColorCode", GetType(String))
        End If

        catdgv.DataSource = dt
        catdgv.RowHeadersVisible = False
        catdgv.ColumnHeadersVisible = False
        catdgv.AllowUserToAddRows = False
        catdgv.AllowUserToResizeRows = False
        catdgv.AllowUserToResizeColumns = False
        catdgv.GridColor = Color.White
        catdgv.CellBorderStyle = DataGridViewCellBorderStyle.None

        If catdgv.Columns.Contains("Count") Then
            catdgv.Columns("Count").Visible = False
        End If

        ' Add Graph column if needed
        If Not catdgv.Columns.Contains("Graph") Then
            Dim imgCol As New DataGridViewImageColumn()
            imgCol.Name = "Graph"
            imgCol.HeaderText = ""
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch
            catdgv.Columns.Add(imgCol)
        End If

        ' Hide ColorCode column – we only use it to draw text
        If catdgv.Columns.Contains("ColorCode") Then
            catdgv.Columns("ColorCode").Visible = False
        End If

        ' Set widths
        If catdgv.Columns.Contains("Category") Then
            catdgv.Columns("Category").Width = CInt(catdgv.Width * 0.35)
        End If
        If catdgv.Columns.Contains("Graph") Then
            catdgv.Columns("Graph").Width = CInt(catdgv.Width * 0.65)
        End If

        ' HSB colors – bright, non-dark
        Dim totalRows As Integer = dt.Rows.Count

        For i As Integer = 0 To totalRows - 1
            Dim cellWidth As Integer = catdgv.Columns("Graph").Width
            Dim cellHeight As Integer = catdgv.Rows(i).Height

            Dim bmp As New Bitmap(cellWidth, cellHeight)

            ' Generate bright color from HSB
            Dim barColor As Color = GetCategoryColor(i, totalRows)
            Dim colorHex As String = ColorTranslator.ToHtml(barColor)

            ' Store HEX code in the hidden column
            dt.Rows(i)("ColorCode") = colorHex

            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                g.FillRectangle(New SolidBrush(barColor), 0, 0, cellWidth - 2, cellHeight - 2)
                g.DrawRectangle(Pens.Black, 0, 0, cellWidth - 2, cellHeight - 2)

                Dim val As Integer = CInt(dt.Rows(i)("Count"))
                Dim font As New Font("Arial", 9, FontStyle.Bold)

                ' 👉 COUNT + space + HEX code inside the bar
                Dim text As String = val.ToString() & " [" & colorHex & "]"  ' two spaces

                Dim textSize As SizeF = g.MeasureString(text, font)
                Dim textX As Single = (cellWidth - textSize.Width) / 2
                Dim textY As Single = (cellHeight - textSize.Height) / 2

                g.DrawString(text, font, Brushes.White, textX, textY)
            End Using

            catdgv.Rows(i).Cells("Graph").Value = bmp
        Next

        catdgv.Columns("Graph").ReadOnly = True
    End Sub

    ' ========================
    ' GRAPH 2: OPERATIONAL STATUS on Panel16
    '   Working     = Working + Assigned
    '   Not Working = Not Working
    '   For Repair  = Maintenance
    '   For Disposal = For Disposal
    ' ========================
    Private Sub DrawDeviceOperationalStatusGraph()
        Dim data = model.GetOperationalStatusCounts()

        Dim labels As String() = {"Working", "Not Working", "For Repair"}
        Dim values As Integer() = {
        data("Working"),
        data("Not Working"),
        data("For Repair")
    }

        Dim colors As Color() = {
        Color.FromArgb(129, 199, 132),
        Color.FromArgb(239, 154, 154),
        Color.FromArgb(255, 224, 130)
    }

        Dim bmp As New Bitmap(Panel16.Width, Panel16.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim marginTop = 30, marginBottom = 50, marginLeft = 10, marginRight = 10
        Dim chartHeight = Panel16.Height - marginTop - marginBottom
        Dim chartWidth = Panel16.Width - marginLeft - marginRight

        Dim n = values.Length
        Dim gap = 5
        Dim barWidth As Integer = CInt((chartWidth - gap * (n - 1)) / n)

        Dim maxVal As Integer = values.Max()
        If maxVal = 0 Then maxVal = 1

        Dim minBarHeight = 5
        Dim labelFont As New Font("Arial", 9)
        Dim valueFont As New Font("Arial", 10, FontStyle.Bold)

        For i = 0 To n - 1
            Dim x = marginLeft + i * (barWidth + gap)
            Dim barHeight = CInt(chartHeight * values(i) / maxVal)
            If values(i) > 0 AndAlso barHeight < minBarHeight Then barHeight = minBarHeight
            Dim y = marginTop + chartHeight - barHeight

            Using b As New SolidBrush(colors(i))
                g.FillRectangle(b, x, y, barWidth, barHeight)
            End Using
            g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

            Dim valText = values(i).ToString()
            g.DrawString(valText, valueFont, Brushes.Black,
                     x + (barWidth - g.MeasureString(valText, valueFont).Width) / 2,
                     y - 20)

            Dim segmentWidth As Double = Panel16.Width / n
            Dim labelX As Double = i * segmentWidth + (segmentWidth - g.MeasureString(labels(i), labelFont).Width) / 2
            Dim labelY As Integer = marginTop + chartHeight + 5
            g.DrawString(labels(i), labelFont, Brushes.Black, CSng(labelX), labelY)
        Next

        Panel16.BackgroundImage = bmp
        Panel16.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub






    ' ========================
    ' LOAD RECENT UNIT ACTIVITIES
    ' ========================
    Private Sub LoadRecentUnitActivities()
        Dim dt As DataTable = model.GetRecentUnitActivities()

        recentdgv.DataSource = dt

        ' HEADER: no highlight on selection
        recentdgv.EnableHeadersVisualStyles = False
        With recentdgv.ColumnHeadersDefaultCellStyle
            .BackColor = SystemColors.Control
            .ForeColor = SystemColors.ControlText
            .SelectionBackColor = .BackColor
            .SelectionForeColor = .ForeColor
        End With

        recentdgv.RowHeadersVisible = False
        recentdgv.ColumnHeadersVisible = True
        recentdgv.AllowUserToAddRows = False
        recentdgv.AllowUserToResizeRows = False
        recentdgv.AllowUserToResizeColumns = True
        recentdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        recentdgv.GridColor = Color.LightGray
        recentdgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        recentdgv.ReadOnly = True
        recentdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        recentdgv.MultiSelect = False
        recentdgv.CurrentCell = Nothing

        ' 🔹 COLORS
        recentdgv.BackgroundColor = Color.White
        recentdgv.DefaultCellStyle.BackColor = Color.White
        recentdgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        recentdgv.DefaultCellStyle.SelectionBackColor = Color.SkyBlue
        recentdgv.DefaultCellStyle.SelectionForeColor = Color.Black

        recentdgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SkyBlue
        recentdgv.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black

        If dt.Columns.Contains("ActivityDate") Then
            recentdgv.Columns("ActivityDate").DefaultCellStyle.Format = "MMM dd, yyyy HH:mm"
        End If

        recentdgv.Columns("UnitName").HeaderText = "UnitName"
        recentdgv.Columns("DeviceModel").HeaderText = "Model"
        recentdgv.Columns("ActivityType").HeaderText = "Type"
        recentdgv.Columns("Remarks").HeaderText = "Remarks"
        recentdgv.Columns("ActivityDate").HeaderText = "Date"
    End Sub



    ' ========================
    ' DRAW UNIT & ASSIGNMENT GRAPH ON PANEL14
    ' ========================
    Private Sub DrawUnitAssignmentGraph()
        Dim data = model.GetUnitStats()
        Dim categories As String() = {"Completed"}
        Dim topLabels As String(,) = {
            {"Has", "No"}
        }

        Dim values(1, 1) As Integer
        values(1, 0) = data.WithPersonnel
        values(1, 1) = data.WithoutPersonnel

        Dim colors(1, 1) As Color
        colors(0, 0) = Color.Green
        colors(0, 1) = Color.Red
        colors(1, 0) = Color.Blue
        colors(1, 1) = Color.Orange

        Dim bmp As New Bitmap(Panel14.Width, Panel14.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim marginTop As Integer = 30
        Dim marginBottom As Integer = 50
        Dim marginLeft As Integer = 20
        Dim marginRight As Integer = 20

        Dim chartHeight As Integer = Panel14.Height - marginTop - marginBottom
        Dim chartWidth As Integer = Panel14.Width - marginLeft - marginRight

        Dim numberOfGroups As Integer = categories.Length
        Dim numberOfBarsPerGroup As Integer = 2
        Dim gapBetweenGroups As Integer = 20
        Dim gapBetweenBars As Integer = 5
        Dim groupWidth As Integer = (chartWidth - (gapBetweenGroups * (numberOfGroups - 1))) / numberOfGroups
        Dim barWidth As Integer = (groupWidth - gapBetweenBars * (numberOfBarsPerGroup - 1)) / numberOfBarsPerGroup

        Dim maxVal As Integer = 0
        For i As Integer = 0 To numberOfGroups - 1
            For j As Integer = 0 To numberOfBarsPerGroup - 1
                If values(i, j) > maxVal Then maxVal = values(i, j)
            Next
        Next
        If maxVal = 0 Then maxVal = 1

        Dim labelFont As New Font("Arial", 9)
        Dim valueFont As New Font("Arial", 10, FontStyle.Bold)

        For i As Integer = 0 To numberOfGroups - 1
            Dim xGroup As Integer = marginLeft + i * (groupWidth + gapBetweenGroups)
            For j As Integer = 0 To numberOfBarsPerGroup - 1
                Dim val As Integer = values(i, j)
                Dim barHeight As Integer = CInt(chartHeight * val / maxVal)
                Dim y As Integer = marginTop + chartHeight - barHeight
                Dim x As Integer = xGroup + j * (barWidth + gapBetweenBars)

                g.FillRectangle(New SolidBrush(colors(i, j)), x, y, barWidth, barHeight)
                g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

                Dim text As String = val.ToString()
                Dim textSize = g.MeasureString(text, valueFont)
                Dim textX As Single = x + (barWidth - textSize.Width) / 2
                Dim textY As Single = y + (barHeight - textSize.Height) / 2
                g.DrawString(text, valueFont, Brushes.White, textX, textY)

                Dim topLabel As String = topLabels(i, j)
                Dim labelSize = g.MeasureString(topLabel, labelFont)
                Dim labelX As Single = x + (barWidth - labelSize.Width) / 2
                Dim labelY As Single = y - labelSize.Height - 2
                g.DrawString(topLabel, labelFont, Brushes.Black, labelX, labelY)
            Next

            Dim groupLabel As String = categories(i)
            Dim labelSizeGroup = g.MeasureString(groupLabel, labelFont)
            g.DrawString(groupLabel, labelFont, Brushes.Black,
                         xGroup + (groupWidth - labelSizeGroup.Width) / 2,
                         marginTop + chartHeight + 5)
        Next

        Panel14.BackgroundImage = bmp
        Panel14.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    ' ========================
    ' DRAW UNIT ASSIGNMENT RATE GRAPH ON PANEL13
    ' ========================
    Private Sub DrawUnitAssignmentRateGraph()
        Dim data = model.GetUnitStats()
        Dim totalUnits As Integer = data.TotalUnits
        Dim assignedUnits As Integer = data.WithPersonnel
        Dim unassignedUnits As Integer = totalUnits - assignedUnits

        If totalUnits = 0 Then totalUnits = 1

        Dim assignedPct As Double = assignedUnits / totalUnits
        Dim unassignedPct As Double = unassignedUnits / totalUnits

        Dim bmp As New Bitmap(Panel13.Width, Panel13.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim marginTop As Integer = 30
        Dim marginBottom As Integer = 50
        Dim marginLeft As Integer = 20
        Dim marginRight As Integer = 20

        Dim chartHeight As Integer = Panel13.Height - marginTop - marginBottom
        Dim chartWidth As Integer = Panel13.Width - marginLeft - marginRight

        Dim categories As String() = {"Assigned", "Unassigned"}
        Dim values As Double() = {assignedPct, unassignedPct}
        Dim colors As Color() = {Color.Green, Color.Red}

        Dim numberOfBars As Integer = values.Length
        Dim gap As Integer = 20
        Dim barWidth As Integer = (chartWidth - gap * (numberOfBars - 1)) / numberOfBars

        Dim labelFont As New Font("Arial", 9)
        Dim valueFont As New Font("Arial", 10, FontStyle.Bold)

        For i As Integer = 0 To numberOfBars - 1
            Dim barHeight As Integer = CInt(chartHeight * values(i))
            Dim x As Integer = marginLeft + i * (barWidth + gap)
            Dim y As Integer = marginTop + chartHeight - barHeight

            g.FillRectangle(New SolidBrush(colors(i)), x, y, barWidth, barHeight)
            g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

            Dim pctText As String = (values(i) * 100).ToString("0") & "%"
            Dim textSize = g.MeasureString(pctText, valueFont)
            Dim textX As Single = x + (barWidth - textSize.Width) / 2
            Dim textY As Single = y + (barHeight - textSize.Height) / 2
            g.DrawString(pctText, valueFont, Brushes.White, textX, textY)

            Dim topLabel As String = If(i = 0, "Has", "No")
            Dim labelSize = g.MeasureString(topLabel, labelFont)
            Dim labelX As Single = x + (barWidth - labelSize.Width) / 2
            Dim labelY As Single = y - labelSize.Height - 2
            g.DrawString(topLabel, labelFont, Brushes.Black, labelX, labelY)

            Dim categoryLabel As String = categories(i)
            Dim categorySize = g.MeasureString(categoryLabel, labelFont)
            g.DrawString(categoryLabel, labelFont, Brushes.Black,
                         x + (barWidth - categorySize.Width) / 2,
                         marginTop + chartHeight + 5)
        Next

        Panel13.BackgroundImage = bmp
        Panel13.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    ' ========================
    ' LOAD RECENTLY ADDED DEVICES / UNITS
    ' ========================
    ' ========================
    ' LOAD RECENTLY ADDED DEVICES / UNITS
    ' ========================
    Private Sub LoadRecentAddedActivity()
        Dim dt As DataTable = model.GetRecentAddedDevicesAndUnits(10)

        activitydgv.DataSource = dt

        ' HEADER: no highlight on selection (USE activitydgv HERE)
        activitydgv.EnableHeadersVisualStyles = False
        With activitydgv.ColumnHeadersDefaultCellStyle
            .BackColor = SystemColors.Control          ' normal header background
            .ForeColor = SystemColors.ControlText      ' normal header text
            .SelectionBackColor = .BackColor           ' same as normal → no highlight
            .SelectionForeColor = .ForeColor
        End With

        activitydgv.RowHeadersVisible = False
        activitydgv.ColumnHeadersVisible = True
        activitydgv.AllowUserToAddRows = False
        activitydgv.AllowUserToResizeRows = False
        activitydgv.AllowUserToResizeColumns = True
        activitydgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        activitydgv.GridColor = Color.LightGray
        activitydgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        activitydgv.ReadOnly = True
        activitydgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        activitydgv.MultiSelect = False
        activitydgv.CurrentCell = Nothing

        ' 🔹 COLORS
        activitydgv.BackgroundColor = Color.White
        activitydgv.DefaultCellStyle.BackColor = Color.White
        activitydgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White

        activitydgv.DefaultCellStyle.SelectionBackColor = Color.SkyBlue
        activitydgv.DefaultCellStyle.SelectionForeColor = Color.Black

        activitydgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SkyBlue
        activitydgv.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black
    End Sub




    ' ========================
    ' LOAD DEVICE HISTORY INTO historydgv
    ' ========================
    Private Sub LoadDeviceHistoryGrid()
        ' You’ll create this function in model (see below)
        Dim dt As DataTable = model.GetRecentDeviceHistory(50)   ' last 50 changes, adjust as you like
        If dt Is Nothing Then
            historydgv.DataSource = Nothing
            Return
        End If

        historydgv.DataSource = dt

        ' ---- Basic grid behavior ----
        With historydgv
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .ReadOnly = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

            .GridColor = Color.LightGray
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

            ' Optional: hide selection color (like in Edit/View)
            .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
            .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.ForeColor

            ' Top-align text to avoid big top spacing
            .RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .RowTemplate.Height = 18
        End With

        ' ---- Hide technical columns ----
        If historydgv.Columns.Contains("pointer") Then
            historydgv.Columns("pointer").Visible = False
        End If

        If historydgv.Columns.Contains("device_pointer") Then
            historydgv.Columns("device_pointer").Visible = False
        End If


        If historydgv.Columns.Contains("DeviceCategory") Then
            Dim col = historydgv.Columns("DeviceCategory")
            col.HeaderText = "Device"
            col.DisplayIndex = 0
        End If

        If historydgv.Columns.Contains("updated_by_name") Then
            Dim col = historydgv.Columns("updated_by_name")
            col.HeaderText = "Updated By"
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            col.Width = 110
        End If

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
            historydgv.Columns("date").DefaultCellStyle.Format = "MMM dd, yyyy HH:mm"
        End If
    End Sub


    ' ========================
    ' HSB COLOR HELPERS
    ' ========================
    Private Function ColorFromHSB(hue As Single, sat As Single, bri As Single) As Color
        ' hue: 0–360, sat: 0–1, bri: 0–1
        If sat = 0 Then
            Dim v As Integer = CInt(bri * 255)
            Return Color.FromArgb(v, v, v)
        End If

        hue = hue Mod 360.0F
        Dim hSection As Single = hue / 60.0F
        Dim i As Integer = CInt(Math.Floor(hSection))
        Dim f As Single = hSection - i

        Dim p As Single = bri * (1.0F - sat)
        Dim q As Single = bri * (1.0F - sat * f)
        Dim t As Single = bri * (1.0F - sat * (1.0F - f))

        Dim r As Single, g As Single, b As Single

        Select Case i
            Case 0
                r = bri : g = t : b = p
            Case 1
                r = q : g = bri : b = p
            Case 2
                r = p : g = bri : b = t
            Case 3
                r = p : g = q : b = bri
            Case 4
                r = t : g = p : b = bri
            Case Else ' 5
                r = bri : g = p : b = q
        End Select

        Dim Rb As Integer = CInt(Math.Max(0, Math.Min(255, r * 255)))
        Dim Gb As Integer = CInt(Math.Max(0, Math.Min(255, g * 255)))
        Dim Bb As Integer = CInt(Math.Max(0, Math.Min(255, b * 255)))

        Return Color.FromArgb(Rb, Gb, Bb)
    End Function

    Private Function GetCategoryColor(index As Integer, total As Integer) As Color
        If total <= 0 Then Return Color.LightGray

        ' even spacing around color wheel
        Dim hue As Single = (CSng(index) / Math.Max(1, total)) * 360.0F

        ' S = 0.6, B = 0.9 ⇒ bright, not too dark
        Return ColorFromHSB(hue, 0.6F, 0.9F)
    End Function

End Class
