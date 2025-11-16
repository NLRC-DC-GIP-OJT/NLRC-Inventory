Public Class Dashboard

    Private LoggedUser As String
    Private model As New model() ' Your Model class

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
    ' Dashboard Load
    ' ========================
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        userlbl.Text = LoggedUser

        ' Start timer for clock
        Timer1.Interval = 1000
        Timer1.Start()

        ' Timer for auto-refreshing charts
        Timer2.Interval = 15000 ' every 15 seconds
        Timer2.Start()

        ' Draw initial charts
        DrawSerialPieChart()
        DrawDeviceStatusBarGraph()

        ' Update totals
        UpdateTotals()

        ' Load DataGridView with category graphs
        LoadCategoryDeviceGrid()
        ' Load recent activities
        LoadRecentUnitActivities()
        DrawUnitAssignmentGraph()
        LoadRecentAddedActivity()
    End Sub

    ' ========================
    ' Timer Tick for Clock
    ' ========================
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        dntlbl.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt")
    End Sub

    ' ========================
    ' Timer Tick for Charts (Auto-Refresh)
    ' ========================
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        DrawSerialPieChart()
        DrawDeviceStatusBarGraph()

        ' Refresh totals
        UpdateTotals()

        ' Refresh category graphs
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
    End Sub

    ' ========================
    ' Navigation Buttons
    ' ========================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        mainpnl.Controls.Clear()
        Dim uc As New devices()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
    End Sub

    Private Sub unitbtn_Click(sender As Object, e As EventArgs) Handles unitbtn.Click
        mainpnl.Controls.Clear()
        Dim uc As New Units()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        mainpnl.Controls.Clear()
        Dim uc As New Configuration()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
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
        g.DrawString($"With Serial: {withSerial}", font, Brushes.Black, (Panel10.Width - g.MeasureString($"With Serial: {withSerial}", font).Width) / 2, rectY + rectSize + 5)
        g.DrawString($"Without Serial: {withoutSerial}", font, Brushes.Black, (Panel10.Width - g.MeasureString($"Without Serial: {withoutSerial}", font).Width) / 2, rectY + rectSize + 25)

        Panel10.BackgroundImage = bmp
        Panel10.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    ' ========================
    ' DRAW DEVICE STATUS BAR GRAPH ON PANEL8
    ' ========================
    Private Sub DrawDeviceStatusBarGraph()
        Dim data = model.GetDeviceStatusCounts()
        Dim statuses As String() = {"Working", "Assigned", "Maintenance", "Not Working"}
        Dim values As Integer() = {
            data("Working"),
            data("Assigned"),
            data("Maintenance"),
            data("Not Working")
        }
        Dim colors As Color() = {Color.Green, Color.Blue, Color.Orange, Color.Red}

        Dim bmp As New Bitmap(Panel8.Width, Panel8.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim marginTop As Integer = 30
        Dim marginBottom As Integer = 50
        Dim marginLeft As Integer = 10
        Dim marginRight As Integer = 10

        Dim chartHeight As Integer = Panel8.Height - marginTop - marginBottom
        Dim chartWidth As Integer = Panel8.Width - marginLeft - marginRight

        Dim numberOfBars As Integer = values.Length
        Dim gap As Integer = 5
        Dim barWidth As Integer = CInt((chartWidth - (gap * (numberOfBars - 1))) / numberOfBars)

        Dim maxVal As Integer = values.Max()
        If maxVal = 0 Then maxVal = 1

        Dim minBarHeight As Integer = 5
        Dim labelFont As New Font("Arial", 9)

        For i As Integer = 0 To numberOfBars - 1
            Dim x As Integer = marginLeft + i * (barWidth + gap)
            Dim barHeight As Integer = CInt(chartHeight * values(i) / maxVal)
            If values(i) > 0 AndAlso barHeight < minBarHeight Then barHeight = minBarHeight

            Dim y As Integer = marginTop + chartHeight - barHeight

            g.FillRectangle(New SolidBrush(colors(i)), x, y, barWidth, barHeight)
            g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

            g.DrawString(values(i).ToString(), New Font("Arial", 10, FontStyle.Bold), Brushes.Black, x + (barWidth - g.MeasureString(values(i).ToString(), New Font("Arial", 10, FontStyle.Bold)).Width) / 2, y - 20)

            ' Full width label fix
            Dim segmentWidth As Double = Panel8.Width / numberOfBars
            Dim labelX As Double = i * segmentWidth + (segmentWidth - g.MeasureString(statuses(i), labelFont).Width) / 2
            Dim labelY As Integer = marginTop + chartHeight + 5
            g.DrawString(statuses(i), labelFont, Brushes.Black, CSng(labelX), labelY)
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

        catdgv.DataSource = dt
        catdgv.Columns("Count").Visible = False
        catdgv.RowHeadersVisible = False
        catdgv.ColumnHeadersVisible = False
        catdgv.AllowUserToAddRows = False
        catdgv.AllowUserToResizeRows = False
        catdgv.AllowUserToResizeColumns = False
        catdgv.GridColor = Color.White
        catdgv.CellBorderStyle = DataGridViewCellBorderStyle.None

        If Not catdgv.Columns.Contains("Graph") Then
            Dim imgCol As New DataGridViewImageColumn()
            imgCol.Name = "Graph"
            imgCol.HeaderText = ""
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch
            catdgv.Columns.Add(imgCol)
        End If

        catdgv.Columns("Category").Width = CInt(catdgv.Width * 0.35)
        catdgv.Columns("Graph").Width = CInt(catdgv.Width * 0.65)

        Dim maxCount As Integer = dt.AsEnumerable().Select(Function(r) CInt(r("Count"))).Max()
        If maxCount = 0 Then maxCount = 1

        Dim colors As Color() = {Color.Green, Color.Blue, Color.Orange, Color.Red, Color.Purple, Color.CadetBlue, Color.Brown}

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim bmp As New Bitmap(catdgv.Columns("Graph").Width, catdgv.Rows(i).Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                Dim val As Integer = CInt(dt.Rows(i)("Count"))
                Dim barWidth As Integer = CInt(bmp.Width * val / maxCount)
                Dim barColor As Color = colors(i Mod colors.Length)
                g.FillRectangle(New SolidBrush(barColor), 0, 0, barWidth, bmp.Height)
                g.DrawRectangle(Pens.Black, 0, 0, barWidth, bmp.Height)

                Dim font As New Font("Arial", 9, FontStyle.Bold)
                Dim text As String = val.ToString()
                Dim textSize = g.MeasureString(text, font)
                Dim textX As Single = Math.Min(barWidth - textSize.Width - 2, 2)
                If textX < 2 Then textX = 2
                Dim textY As Single = (bmp.Height - textSize.Height) / 2
                g.DrawString(text, font, Brushes.White, textX, textY)
            End Using
            catdgv.Rows(i).Cells("Graph").Value = bmp
        Next

        catdgv.Columns("Graph").ReadOnly = True
    End Sub


    Private Sub LoadRecentUnitActivities()
        ' 1️⃣ Get data from Model
        Dim dt As DataTable = model.GetRecentUnitActivities()

        ' ======= recentdgv =======
        recentdgv.DataSource = dt
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

        ' Optional: format date column
        If dt.Columns.Contains("ActivityDate") Then
            recentdgv.Columns("ActivityDate").DefaultCellStyle.Format = "MMM dd, yyyy HH:mm"
        End If

        ' Optional: header text cleanup
        recentdgv.Columns("UnitName").HeaderText = "Unit Name"
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
        Dim categories As String() = {"Unit Name", "Assigned"}
        Dim topLabels As String(,) = {
            {"Has", "No"},
            {"Has", "No"}
        }

        Dim values(1, 1) As Integer
        values(0, 0) = data.WithName
        values(0, 1) = data.WithoutName
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

        Dim marginTop As Integer = 30 ' space for top labels
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

        ' Find max value to scale bars
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

                ' Draw bar
                g.FillRectangle(New SolidBrush(colors(i, j)), x, y, barWidth, barHeight)
                g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

                ' Draw value inside bar
                Dim text As String = val.ToString()
                Dim textSize = g.MeasureString(text, valueFont)
                Dim textX As Single = x + (barWidth - textSize.Width) / 2
                Dim textY As Single = y + (barHeight - textSize.Height) / 2
                g.DrawString(text, valueFont, Brushes.White, textX, textY)

                ' Draw short top label ("Has"/"No")
                Dim topLabel As String = topLabels(i, j)
                Dim labelSize = g.MeasureString(topLabel, labelFont)
                Dim labelX As Single = x + (barWidth - labelSize.Width) / 2
                Dim labelY As Single = y - labelSize.Height - 2
                g.DrawString(topLabel, labelFont, Brushes.Black, labelX, labelY)
            Next

            ' Draw group label below bars
            Dim groupLabel As String = categories(i)
            Dim labelSizeGroup = g.MeasureString(groupLabel, labelFont)
            g.DrawString(groupLabel, labelFont, Brushes.Black, xGroup + (groupWidth - labelSizeGroup.Width) / 2, marginTop + chartHeight + 5)
        Next

        Panel14.BackgroundImage = bmp
        Panel14.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    ' ========================
    ' DRAW UNIT ASSIGNMENT RATE GRAPH ON PANEL13
    ' ========================
    Private Sub DrawUnitAssignmentRateGraph()
        ' Get data from model
        Dim data = model.GetUnitStats() ' Make sure model returns total units and assigned units
        Dim totalUnits As Integer = data.TotalUnits
        Dim assignedUnits As Integer = data.WithPersonnel
        Dim unassignedUnits As Integer = totalUnits - assignedUnits

        ' Avoid division by zero
        If totalUnits = 0 Then totalUnits = 1

        ' Percentages
        Dim assignedPct As Double = assignedUnits / totalUnits
        Dim unassignedPct As Double = unassignedUnits / totalUnits

        ' Setup graphics
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

            ' Draw bar
            g.FillRectangle(New SolidBrush(colors(i)), x, y, barWidth, barHeight)
            g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight)

            ' Draw percentage inside bar
            Dim pctText As String = (values(i) * 100).ToString("0") & "%"
            Dim textSize = g.MeasureString(pctText, valueFont)
            Dim textX As Single = x + (barWidth - textSize.Width) / 2
            Dim textY As Single = y + (barHeight - textSize.Height) / 2
            g.DrawString(pctText, valueFont, Brushes.White, textX, textY)

            ' Draw top label "Has" / "No"
            Dim topLabel As String = If(i = 0, "Has", "No")
            Dim labelSize = g.MeasureString(topLabel, labelFont)
            Dim labelX As Single = x + (barWidth - labelSize.Width) / 2
            Dim labelY As Single = y - labelSize.Height - 2
            g.DrawString(topLabel, labelFont, Brushes.Black, labelX, labelY)

            ' Draw bottom category label
            Dim categoryLabel As String = categories(i)
            Dim categorySize = g.MeasureString(categoryLabel, labelFont)
            g.DrawString(categoryLabel, labelFont, Brushes.Black, x + (barWidth - categorySize.Width) / 2, marginTop + chartHeight + 5)
        Next

        Panel13.BackgroundImage = bmp
        Panel13.BackgroundImageLayout = ImageLayout.None
        g.Dispose()
    End Sub

    Private Sub LoadRecentAddedActivity()
        Dim dt As DataTable = model.GetRecentAddedDevicesAndUnits(10)

        ' ======= activitydgv =======
        activitydgv.DataSource = dt
        activitydgv.RowHeadersVisible = False
        activitydgv.ColumnHeadersVisible = False
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
    End Sub





End Class
