Imports System.Runtime.InteropServices
Imports System.Text

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

    Private Sub devices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub LoadDevices(Optional category As String = "All", Optional brand As String = "All", Optional status As String = "All", Optional search As String = "")
        Try
            Dim dt As DataTable = mdl.GetDevices()
            devicesdgv.RowTemplate.Height = 35

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

            ' Hide DevicePointer
            If devicesdgv.Columns.Contains("DevicePointer") Then
                devicesdgv.Columns("DevicePointer").Visible = False
                devicePointerColIndex = devicesdgv.Columns("DevicePointer").Index
            End If

            ' Autosize columns except Specs
            For Each col As DataGridViewColumn In devicesdgv.Columns
                If col.Name <> "Specifications" Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End If
            Next
            If devicesdgv.Columns.Contains("Specifications") Then
                devicesdgv.Columns("Specifications").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If

            ' Remove old button columns
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
                .Width = 130
            }
            devicesdgv.Columns.Add(actionsCol)

            ' Make non-action columns read-only
            For Each col As DataGridViewColumn In devicesdgv.Columns
                If col.Name <> "Actions" Then
                    col.ReadOnly = True
                End If
            Next

            ' Attach paint handler once
            RemoveHandler devicesdgv.CellPainting, AddressOf devicesdgv_CellPainting_Merged
            AddHandler devicesdgv.CellPainting, AddressOf devicesdgv_CellPainting_Merged

            UpdatePaginationControls(allFilteredRows.Count)

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
            Dim padding As Integer = 6

            Dim buttonHeight As Integer = cellRect.Height - (padding * 2)
            Dim buttonWidth As Integer = (cellRect.Width - (padding * 3)) \ 2
            Dim buttonY As Integer = cellRect.Y + padding

            Dim btnFont As New Font("Segoe UI", 9.5, FontStyle.Regular)

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
            ViewDeviceRow(selectedRow)
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
        If currentPage > 1 Then currentPage -= 1 : LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentPage < totalPages Then currentPage += 1 : LoadDevices(catecb.Text, brandscb.Text, statuscb.Text, filtertxt.Text)
    End Sub




    'CUSTOMIZED CODE AREA - DO NOT DELETE
    Private Sub ViewDeviceRow(row As DataGridViewRow)
        ' Collect data
        Dim details As New List(Of (Header As String, Value As String))

        For Each cell As DataGridViewCell In row.Cells
            Dim col As DataGridViewColumn = devicesdgv.Columns(cell.ColumnIndex)
            If Not col.Visible Then Continue For

            Dim headerText As String = col.HeaderText.Trim()
            Dim valueText As String = If(cell.Value IsNot Nothing, cell.Value.ToString().Trim(), "")

            If headerText.Equals("DevicePointer", StringComparison.OrdinalIgnoreCase) OrElse headerText = "" Then Continue For

            If headerText.Equals("Purchase_Date", StringComparison.OrdinalIgnoreCase) OrElse
           headerText.Equals("Warranty_Expires", StringComparison.OrdinalIgnoreCase) Then
                Dim parsedDate As Date
                If Date.TryParse(valueText, parsedDate) Then
                    valueText = parsedDate.ToString("MM/dd/yyyy")
                End If
            End If

            details.Add((headerText, valueText))
        Next

        ' --- Main form setup ---
        Dim viewForm As New Form With {
        .Text = "Device Details",
        .Size = New Size(650, 600),
        .StartPosition = FormStartPosition.CenterParent,
        .BackColor = Color.White,
        .FormBorderStyle = FormBorderStyle.None
    }

        ' Enable double buffering via reflection (for smoother redraw)
        Dim formType = GetType(Form)
        formType.InvokeMember("DoubleBuffered",
                          Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic,
                          Nothing, viewForm, New Object() {True})

        ' --- Title ---
        Dim titleLbl As New Label With {
        .Text = "Device Details",
        .Font = New Font("Segoe UI Semibold", 14, FontStyle.Bold),
        .AutoSize = True,
        .Location = New Point(30, 20),
        .ForeColor = Color.FromArgb(40, 40, 40)
    }
        viewForm.Controls.Add(titleLbl)

        ' --- Add detail labels ---
        Dim startY As Integer = 70
        For Each item In details
            Dim lblHeader As New Label With {
            .Text = item.Header & ":",
            .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
            .ForeColor = Color.FromArgb(50, 50, 50),
            .AutoSize = True,
            .Location = New Point(40, startY)
        }

            Dim lblValue As New Label With {
            .Text = item.Value,
            .Font = New Font("Segoe UI", 10),
            .ForeColor = Color.FromArgb(80, 80, 80),
            .MaximumSize = New Size(400, 0),
            .Location = New Point(200, startY),
            .AutoSize = True
        }

            viewForm.Controls.Add(lblHeader)
            viewForm.Controls.Add(lblValue)

            startY = lblValue.Bottom + 15
        Next

        ' --- Buttons directly on form ---
        Dim btnOk As New Button With {
        .Text = "OK",
        .Width = 100,
        .Height = 35,
        .BackColor = Color.FromArgb(230, 230, 230),
        .FlatStyle = FlatStyle.Flat,
        .Font = New Font("Segoe UI", 10),
        .Location = New Point(viewForm.ClientSize.Width - 230, viewForm.ClientSize.Height - 60),
        .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
    }
        btnOk.FlatAppearance.BorderSize = 0

        Dim btnEdit As New Button With {
        .Text = "Edit",
        .Width = 100,
        .Height = 35,
        .BackColor = Color.RoyalBlue,
        .ForeColor = Color.White,
        .FlatStyle = FlatStyle.Flat,
        .Font = New Font("Segoe UI", 10),
        .Location = New Point(viewForm.ClientSize.Width - 120, viewForm.ClientSize.Height - 60),
        .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
    }
        btnEdit.FlatAppearance.BorderSize = 0

        viewForm.Controls.Add(btnOk)
        viewForm.Controls.Add(btnEdit)

        ' --- Close (X) button --- 
        Dim closeBtn As New Button With {
        .Text = "✕",
        .FlatStyle = FlatStyle.Flat,
        .Font = New Font("Segoe UI", 10, FontStyle.Bold),
        .Size = New Size(30, 30),
        .ForeColor = Color.Gray,
        .BackColor = Color.Transparent,
        .Anchor = AnchorStyles.Top Or AnchorStyles.Right
    }
        closeBtn.FlatAppearance.BorderSize = 0

        ' Move it 25px from right and 20px from top
        closeBtn.Location = New Point(viewForm.ClientSize.Width - closeBtn.Width - 25, 20)

        AddHandler closeBtn.Click, Sub() viewForm.Close()
        viewForm.Controls.Add(closeBtn)

        ' --- Edit logic ---
        Dim deviceIdObj = row.Cells("DevicePointer").Value
        Dim deviceId As Integer
        Integer.TryParse(If(deviceIdObj IsNot Nothing, deviceIdObj.ToString(), "0"), deviceId)

        AddHandler btnOk.Click, Sub() viewForm.Close()
        AddHandler btnEdit.Click, Sub()
                                      viewForm.Close()
                                      EditDevice(deviceId)
                                  End Sub

        ' --- Draw visible black border ---
        AddHandler viewForm.Paint,
        Sub(sender As Object, e As PaintEventArgs)
            Dim borderColor As Color = Color.Black
            Dim borderThickness As Integer = 2
            Dim rect As Rectangle = viewForm.ClientRectangle
            rect.Inflate(-1, -1)
            Using pen As New Pen(borderColor, borderThickness)
                e.Graphics.DrawRectangle(pen, rect)
            End Using
        End Sub

        ' --- Show the form ---
        viewForm.ShowDialog()
    End Sub



End Class





















