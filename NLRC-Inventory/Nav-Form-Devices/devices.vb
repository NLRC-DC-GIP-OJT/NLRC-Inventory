Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms



Public Class devices
    Private mdl As New model()
    Private devicePointerColIndex As Integer = -1
    Private isLoading As Boolean = True
    Private currentPage As Integer = 1
    Private pageSize As Integer = 30
    Private totalPages As Integer = 1
    Private allFilteredRows As List(Of DataRow)

    ' 🕒 Added for auto refresh
    Private WithEvents refreshTimer As New Timer() With {.Interval = 5000} ' 5 seconds
    Private lastDeviceHash As String = ""

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    ' 🧱 base sizes for inside DataGridView
    Private Const BaseRowHeight As Integer = 35
    Private Const BaseHeaderHeight As Integer = 32
    Private Const ActionsColWidth As Integer = 80   ' narrow Edit/View column

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

    ' 🔧 specifically adjust inside of DataGridView (rows / headers / actions width)
    Private Sub AdjustDevicesGridLayout()
        If devicesdgv Is Nothing Then Return
        If originalSize.Width = 0 OrElse originalSize.Height = 0 Then Return

        ' scale relative to original usercontrol height
        Dim scaleY As Single = CSng(Me.Height) / originalSize.Height
        If scaleY <= 0 Then scaleY = 1.0F

        Dim newRowHeight As Integer = CInt(BaseRowHeight * scaleY)
        If newRowHeight < 22 Then newRowHeight = 22   ' minimum so text is visible

        Dim newHeaderHeight As Integer = CInt(BaseHeaderHeight * scaleY)
        If newHeaderHeight < 24 Then newHeaderHeight = 24

        ' apply to row template and existing rows
        devicesdgv.RowTemplate.Height = newRowHeight
        For Each row As DataGridViewRow In devicesdgv.Rows
            row.Height = newRowHeight
        Next

        devicesdgv.ColumnHeadersHeight = newHeaderHeight

        ' keep Actions column a nice narrow fixed width
        If devicesdgv.Columns.Contains("Actions") Then
            With devicesdgv.Columns("Actions")
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = ActionsColWidth
            End With
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        If Not layoutInitialized Then Return
        If originalSize.Width = 0 OrElse originalSize.Height = 0 Then Return

        Dim scaleX As Single = CSng(Me.Width) / originalSize.Width
        Dim scaleY As Single = CSng(Me.Height) / originalSize.Height

        ApplyScale(scaleX, scaleY)

        ' also resize inside the grid
        AdjustDevicesGridLayout()
    End Sub

    ' ========================
    ' LOAD
    ' ========================
    Private Sub devices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Let the usercontrol fill whatever container it’s placed in
        Me.Dock = DockStyle.Fill

        ' Initialize auto-resize based on designer layout
        InitializeLayoutScaling()

        LoadDevices()
        LoadFilterCombos()
        SetupComboBoxHints()

        ' Initialize first data hash
        Dim dt As DataTable = mdl.GetDevices()
        lastDeviceHash = ComputeDeviceHash(dt)

        ' Start auto-refresh timer
        refreshTimer.Start()
    End Sub

    Public Sub RefreshDevicesGrid()
        LoadDevices()
    End Sub

    Public Sub RefreshDevices()
        LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
    End Sub

    ' 🧩 Load Devices with filtering and pagination
    ' 🧩 Load Devices with filtering and pagination
    ' 🧩 Load Devices with filtering and pagination
    Private Sub LoadDevices(Optional category As String = "All",
                        Optional brand As String = "All",
                        Optional status As String = "All",
                        Optional search As String = "")
        Try
            Dim dt As DataTable = mdl.GetDevices()
            devicesdgv.RowTemplate.Height = BaseRowHeight

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                devicesdgv.DataSource = Nothing
                UpdatePaginationControls(0)
                Return
            End If

            ' Filter rows
            Dim filtered = dt.AsEnumerable()
            If category <> "All" AndAlso category <> "Category" Then
                filtered = filtered.Where(Function(r) r("Category").ToString().ToLower().Contains(category.ToLower()))
            End If
            If brand <> "All" AndAlso brand <> "Brands" Then
                filtered = filtered.Where(Function(r) r("Brand").ToString().ToLower().Contains(brand.ToLower()))
            End If
            If status <> "All" AndAlso status <> "Status" Then
                filtered = filtered.Where(Function(r) r("Status").ToString().ToLower().Contains(status.ToLower()))
            End If

            ' Skip filtering by search if placeholder is active or empty
            If Not String.IsNullOrWhiteSpace(search) AndAlso Not (filtertxt.ForeColor = Color.Gray AndAlso search = "Search") Then
                Dim s = search.ToLower()
                filtered = filtered.Where(Function(r) r.ItemArray.Any(Function(val) val IsNot Nothing AndAlso val.ToString().ToLower().Contains(s)))
            End If

            allFilteredRows = filtered.ToList()
            totalPages = Math.Ceiling(allFilteredRows.Count / pageSize)
            If currentPage > totalPages Then currentPage = totalPages
            If currentPage < 1 Then currentPage = 1

            Dim pageRows = allFilteredRows.Skip((currentPage - 1) * pageSize).Take(pageSize)
            If pageRows.Any() Then
                devicesdgv.DataSource = pageRows.CopyToDataTable()
            Else
                devicesdgv.DataSource = Nothing
            End If

            ' ===== HIDE TECHNICAL COLUMNS =====
            If devicesdgv.Columns.Contains("DevicePointer") Then
                devicesdgv.Columns("DevicePointer").Visible = False
                devicePointerColIndex = devicesdgv.Columns("DevicePointer").Index
            End If
            If devicesdgv.Columns.Contains("Category") Then
                devicesdgv.Columns("Category").Visible = False
            End If
            If devicesdgv.Columns.Contains("Brand") Then
                devicesdgv.Columns("Brand").Visible = False
            End If

            ' ===== BUTTON COLUMN (Actions) =====
            ' Remove any old button columns
            For Each col As DataGridViewColumn In devicesdgv.Columns.OfType(Of DataGridViewButtonColumn).ToList()
                devicesdgv.Columns.Remove(col)
            Next

            ' Add merged Actions column
            If devicesdgv.Columns.Contains("Actions") Then
                devicesdgv.Columns.Remove("Actions")
            End If

            Dim actionsCol As New DataGridViewButtonColumn() With {
            .Name = "Actions",
            .HeaderText = "",
            .UseColumnTextForButtonValue = False,
            .Width = ActionsColWidth
        }
            devicesdgv.Columns.Add(actionsCol)

            ' ===== FIX DISPLAY ORDER (PUT ACTIONS AT THE END) =====
            Dim orderedCols As String() = {
            "NSOC Name",
            "Device",
            "Property Number",
            "Specifications",
            "Status",
            "Serial Number",
            "Purchase Date",
            "Warranty Expires"
        }

            Dim displayIndex As Integer = 0
            For Each colName As String In orderedCols
                If devicesdgv.Columns.Contains(colName) Then
                    devicesdgv.Columns(colName).DisplayIndex = displayIndex
                    displayIndex += 1
                End If
            Next

            ' Always move Actions to the far right
            If devicesdgv.Columns.Contains("Actions") Then
                devicesdgv.Columns("Actions").DisplayIndex = devicesdgv.Columns.Count - 1
            End If

            ' Make non-action columns read-only
            For Each col As DataGridViewColumn In devicesdgv.Columns
                If col.Name <> "Actions" Then
                    col.ReadOnly = True
                End If
            Next

            ' === COLUMN LAYOUT – SPECS BY HEADER ONLY, OTHERS FILL ===
            devicesdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            For Each col As DataGridViewColumn In devicesdgv.Columns
                Select Case col.Name
                    Case "Specifications", "Specification", "Specs"
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                        col.MinimumWidth = 90

                    Case "Actions"
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        col.Width = ActionsColWidth

                    Case Else
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End Select
            Next

            ' Attach paint handler once (for the Edit/View drawing)
            RemoveHandler devicesdgv.CellPainting, AddressOf devicesdgv_CellPainting_Merged
            AddHandler devicesdgv.CellPainting, AddressOf devicesdgv_CellPainting_Merged

            UpdatePaginationControls(allFilteredRows.Count)

            ' 🔧 adjust row/header heights to current resolution
            AdjustDevicesGridLayout()

        Catch ex As Exception
            MessageBox.Show("Error loading devices: " & ex.Message)
        End Try
    End Sub




    ' 🎨 Draw merged Edit + View buttons inside one column
    Private Sub devicesdgv_CellPainting_Merged(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex < 0 Then Return
        If e.ColumnIndex = devicesdgv.Columns("Actions").Index Then
            e.PaintBackground(e.CellBounds, True)

            Dim g As Graphics = e.Graphics
            Dim cellRect As Rectangle = e.CellBounds
            Dim padding As Integer = 4   ' a bit tighter

            Dim buttonHeight As Integer = cellRect.Height - (padding * 2)
            Dim buttonWidth As Integer = (cellRect.Width - (padding * 3)) \ 2
            Dim buttonY As Integer = cellRect.Y + padding

            ' constant, normal font size
            Dim btnFont As New Font("Segoe UI", 9.0F, FontStyle.Regular)

            Dim editColor As Brush = New SolidBrush(Color.FromArgb(255, 128, 0))
            Dim viewColor As Brush = New SolidBrush(Color.FromArgb(0, 120, 215))

            Dim editRect As New Rectangle(cellRect.X + padding, buttonY, buttonWidth, buttonHeight)
            g.FillRectangle(editColor, editRect)
            g.DrawRectangle(Pens.Gray, editRect)
            TextRenderer.DrawText(g, "Edit", btnFont, editRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

            Dim viewRect As New Rectangle(cellRect.X + buttonWidth + (padding * 2), buttonY, buttonWidth, buttonHeight)
            g.FillRectangle(viewColor, viewRect)
            g.DrawRectangle(Pens.Gray, viewRect)
            TextRenderer.DrawText(g, "View", btnFont, viewRect, Color.White, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

            e.Handled = True
        End If
    End Sub

    ' 🖱 Handle clicks for merged Edit/View buttons
    Private Sub devicesdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles devicesdgv.CellContentClick
        If e.RowIndex < 0 Then Return
        If devicesdgv.Columns(e.ColumnIndex).Name <> "Actions" Then Return

        Dim cellRect As Rectangle = devicesdgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
        Dim x As Integer = devicesdgv.PointToClient(Cursor.Position).X - cellRect.X
        Dim buttonWidth As Integer = (cellRect.Width - 10) \ 2

        Dim selectedRow As DataGridViewRow = devicesdgv.Rows(e.RowIndex)
        Dim deviceIdObj = selectedRow.Cells("DevicePointer").Value
        Dim deviceId As Integer
        If deviceIdObj Is Nothing OrElse Not Integer.TryParse(deviceIdObj.ToString(), deviceId) Then
            MessageBox.Show("Invalid device ID.")
            Return
        End If

        If x < buttonWidth + 5 Then
            EditDevice(deviceId)
        Else
            ViewDeviceRow(deviceId)
        End If
    End Sub

    ' 🧱 Edit handler
    Private Sub EditDevice(deviceId As Integer)
        Try
            Dim device As InvDevice = mdl.GetDeviceById(deviceId)
            If device Is Nothing Then
                MessageBox.Show("Device not found.")
                Return
            End If

            addpnl.Visible = True
            addpnl.Controls.Clear()
            Dim editUC As New Edit(mdl)
            editUC.Dock = DockStyle.Fill
            editUC.LoadDevice(device)
            addpnl.Controls.Add(editUC)

        Catch ex As Exception
            MessageBox.Show("Error opening edit form: " & ex.Message)
        End Try
    End Sub

    ' 🕒 Compute Hash for detecting DB changes
    Private Function ComputeDeviceHash(dt As DataTable) As String
        Dim sb As New StringBuilder()
        For Each row As DataRow In dt.Rows
            sb.Append(String.Join("|", row.ItemArray))
        Next
        Return sb.ToString().GetHashCode().ToString()
    End Function

    ' 🕒 Timer Tick event for auto refresh
    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
        Try
            Dim dt As DataTable = mdl.GetDevices()
            Dim newHash As String = ComputeDeviceHash(dt)
            If newHash <> lastDeviceHash Then
                lastDeviceHash = newHash
                LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
            End If
        Catch ex As Exception
            Console.WriteLine("Auto-refresh error: " & ex.Message)
        End Try
    End Sub

    ' DLL imports
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindowEx(parentHandle As IntPtr, childAfter As IntPtr, lclassName As String, windowTitle As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function SetWindowText(hWnd As IntPtr, lpString As String) As Boolean
    End Function

    ' 🔄 Filters and pagination
    Private Sub LoadFilterCombos()
        Try
            Dim dt As DataTable = mdl.GetDevices()

            catecb.Items.Clear()
            catecb.Items.Add("All")
            catecb.Items.AddRange(dt.AsEnumerable().Select(Function(r) r("Category").ToString()).Distinct().ToArray())

            brandscb.Items.Clear()
            brandscb.Items.Add("All")
            brandscb.Items.AddRange(dt.AsEnumerable().Select(Function(r) r("Brand").ToString()).Distinct().ToArray())

            statuscb.Items.Clear()
            statuscb.Items.Add("All")
            statuscb.Items.AddRange(dt.AsEnumerable().Select(Function(r) r("Status").ToString()).Distinct().ToArray())

            catecb.SelectedIndex = 0
            brandscb.SelectedIndex = 0
            statuscb.SelectedIndex = 0

            isLoading = False
        Catch ex As Exception
            MessageBox.Show("Error loading filters: " & ex.Message)
        End Try
    End Sub

    Private Sub SetupComboBoxHints()
        SetComboBoxHint(catecb, "Category")
        SetComboBoxHint(brandscb, "Brands")
        SetComboBoxHint(statuscb, "Status")
        SetTextBoxHint(filtertxt, "Search")
    End Sub

    Private Sub SetTextBoxHint(tb As TextBox, hint As String)
        tb.ForeColor = Color.Gray
        tb.Text = hint
        AddHandler tb.GotFocus, Sub(sender, e)
                                    If tb.ForeColor = Color.Gray Then
                                        tb.Text = ""
                                        tb.ForeColor = Color.Black
                                    End If
                                End Sub
        AddHandler tb.LostFocus, Sub(sender, e)
                                     If String.IsNullOrWhiteSpace(tb.Text) Then
                                         tb.Text = hint
                                         tb.ForeColor = Color.Gray
                                     End If
                                 End Sub
    End Sub

    Private Sub SetComboBoxHint(cb As ComboBox, hint As String)
        cb.ForeColor = Color.Gray
        cb.Text = hint
        AddHandler cb.GotFocus, Sub(sender, e)
                                    If cb.ForeColor = Color.Gray Then cb.Text = "" : cb.ForeColor = Color.Black
                                End Sub
        AddHandler cb.LostFocus, Sub(sender, e)
                                     If String.IsNullOrWhiteSpace(cb.Text) Then cb.Text = hint : cb.ForeColor = Color.Gray
                                 End Sub
    End Sub

    ' 🧮 Filters + pagination controls
    Private Sub catecb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles catecb.SelectedIndexChanged
        If isLoading OrElse catecb.ForeColor = Color.Gray Then Return
        LoadDevices(catecb.Text, brandscb.Text, statuscb.Text)
    End Sub

    Private Sub brandscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles brandscb.SelectedIndexChanged
        If isLoading OrElse brandscb.ForeColor = Color.Gray Then Return
        LoadDevices(catecb.Text, brandscb.Text, statuscb.Text)
    End Sub

    Private Sub statuscb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles statuscb.SelectedIndexChanged
        If isLoading OrElse statuscb.ForeColor = Color.Gray Then Return
        LoadDevices(catecb.Text, brandscb.Text, statuscb.Text)
    End Sub

    Private Sub filtertxt_KeyUp(sender As Object, e As KeyEventArgs) Handles filtertxt.KeyUp
        ' Only filter if user typed actual text (ignore placeholder)
        If filtertxt.ForeColor = Color.Gray Then Return

        Dim searchText As String = filtertxt.Text.Trim()
        LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, searchText)
    End Sub

    ' ➕ Add panel toggle
    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        If addpnl.Visible = False Then
            addpnl.Visible = True
            addpnl.Controls.Clear()
            Dim addUC As New Add()
            addUC.Dock = DockStyle.Fill
            addpnl.Controls.Add(addUC)
        Else
            addpnl.Visible = False
        End If
    End Sub

    ' 🔢 Pagination
    Private Sub UpdatePaginationControls(totalRecords As Integer)
        lblPageInfo.Text = $"Page {currentPage} of {If(totalPages < 1, 1, totalPages)}"
        btnPrev.Enabled = (currentPage > 1)
        btnNext.Enabled = (currentPage < totalPages)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If currentPage > 1 Then
            currentPage -= 1
            LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentPage < totalPages Then
            currentPage += 1
            LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
        End If
    End Sub


    Private Sub ViewDeviceRow(deviceId As Integer)
        Try
            Dim device As InvDevice = mdl.GetDeviceById(deviceId)
            If device Is Nothing Then
                MessageBox.Show("Device not found.")
                Return
            End If

            addpnl.Visible = True
            addpnl.Controls.Clear()
            Dim editUC As New View(mdl)
            editUC.Dock = DockStyle.Fill
            editUC.LoadDevice(device)
            addpnl.Controls.Add(editUC)

        Catch ex As Exception
            MessageBox.Show("Error opening view form: " & ex.Message)
        End Try
    End Sub

    Private Sub exportbtn_Click(sender As Object, e As EventArgs) Handles exportbtn.Click
        Try
            If allFilteredRows Is Nothing OrElse allFilteredRows.Count = 0 Then
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim saveDlg As New SaveFileDialog With {
                .Filter = "CSV File (*.csv)|*.csv",
                .FileName = "Devices_Export.csv"
            }

            If saveDlg.ShowDialog <> DialogResult.OK Then Exit Sub

            Using sw As New IO.StreamWriter(saveDlg.FileName, False, Encoding.UTF8)

                ' HEADERS (same order as your DataGridView)
                Dim headers = {
                    "NSOC Name",
                    "Device (Category - Brand - Model)",
                    "Property Number",
                    "Specifications",
                    "Status",
                    "Serial Number",
                    "Purchase Date",
                    "Warranty Expires"
                }

                sw.WriteLine(String.Join(",", headers.Select(Function(s) $"""{s}""")))

                ' ROWS (export ALL PAGES, not only current view)
                For Each r In allFilteredRows
                    Dim line As New List(Of String)

                    line.Add($"""{r("NSOC Name").ToString}""")
                    line.Add($"""{r("Device").ToString}""")
                    line.Add($"""{r("Property Number").ToString}""")
                    line.Add($"""{r("Specifications").ToString}""")
                    line.Add($"""{r("Status").ToString}""")
                    line.Add($"""{r("Serial Number").ToString}""")
                    line.Add($"""{r("Purchase Date").ToString}""")
                    line.Add($"""{r("Warranty Expires").ToString}""")

                    sw.WriteLine(String.Join(",", line))
                Next
            End Using

            MessageBox.Show("Export successful!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error exporting: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub importbtn_Click(sender As Object, e As EventArgs)

    End Sub
End Class
