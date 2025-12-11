Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Units
    Private mdl As New model()
    Private personnelNames As List(Of String)
    Private currentPage As Integer = 1
    Private pageSize As Integer = 30
    Private totalPages As Integer = 1
    Private allFilteredRows As List(Of DataRow)
    Private isLoading As Boolean = True
    Private assignedToCboCol As DataGridViewComboBoxColumn
    Private WithEvents refreshTimer As New Timer() With {
        .Interval = 5000 ' 5000 ms = 5 seconds (change if you want)
    }
    Private allUnitsTable As DataTable          ' cache from DB
    Private columnsInitialized As Boolean = False

    ' === selection column + header checkbox ===
    Private selectColumnName As String = "SelectQR"
    Private selectAllCheckBox As CheckBox
    ' ===============================================


    ' ----------------- Load Units (FAST, cached) -----------------
    Private Sub LoadAllUnits(Optional search As String = "",
                         Optional reloadFromDb As Boolean = False)

        Try
            ' 1) Only hit DB when needed
            If reloadFromDb OrElse allUnitsTable Is Nothing Then
                allUnitsTable = mdl.GetUnitsSummary()

                If allUnitsTable Is Nothing Then
                    allFilteredRows = New List(Of DataRow)()
                    allunitsdgv.DataSource = Nothing
                    MessageBox.Show("Failed to load units data.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            ' 2) Filter in memory (no DB)
            Dim filtered = allUnitsTable.AsEnumerable()

            If Not String.IsNullOrWhiteSpace(search) AndAlso
               Not (filtertxt.ForeColor = Color.Gray AndAlso search = "Search") Then

                Dim s = search.ToLower()

                filtered = filtered.Where(
                    Function(r)

                        Dim hit As Boolean = False

                        ' --- explicit Unit Name search ---
                        If r.Table.Columns.Contains("Unit Name") AndAlso
                           Not IsDBNull(r("Unit Name")) AndAlso
                           r("Unit Name").ToString().ToLower().Contains(s) Then
                            hit = True
                        End If

                        ' --- explicit Assigned To (personnel) search ---
                        If Not hit AndAlso
                           r.Table.Columns.Contains("Assigned To") AndAlso
                           Not IsDBNull(r("Assigned To")) AndAlso
                           r("Assigned To").ToString().ToLower().Contains(s) Then
                            hit = True
                        End If

                        ' --- fallback: any other column ---
                        If Not hit Then
                            hit = r.ItemArray.Any(
                                Function(val) val IsNot Nothing AndAlso
                                               val.ToString().ToLower().Contains(s))
                        End If

                        Return hit
                    End Function)
            End If

            allFilteredRows = filtered.ToList()

            ' 3) Pagination
            totalPages = CInt(Math.Ceiling(allFilteredRows.Count / pageSize))
            If totalPages < 1 Then totalPages = 1
            If currentPage > totalPages Then currentPage = totalPages
            If currentPage < 1 Then currentPage = 1

            Dim pageRows = allFilteredRows.Skip((currentPage - 1) * pageSize).Take(pageSize)

            Dim bindTable As DataTable
            If pageRows.Any() Then
                bindTable = pageRows.CopyToDataTable()
            Else
                bindTable = allUnitsTable.Clone()   ' empty but has columns
            End If

            ' 4) Bind data
            allunitsdgv.SuspendLayout()
            allunitsdgv.DataSource = bindTable

            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.AllowUserToAddRows = False
            allunitsdgv.RowHeadersVisible = False

            If allunitsdgv.Columns.Contains("unit_id") Then allunitsdgv.Columns("unit_id").Visible = False
            If allunitsdgv.Columns.Contains("personnel_id") Then allunitsdgv.Columns("personnel_id").Visible = False

            ' 5) Initialize columns only once
            If Not columnsInitialized Then
                InitializeAssignedToColumn()
                InitializeEditViewColumns()
                columnsInitialized = True
            End If

            ' 6) Fix column order
            Dim fixedOrder As String() = {"Unit Name", "Device No", "Assigned To", "Created At", "Updated At"}
            For i As Integer = 0 To fixedOrder.Length - 1
                If allunitsdgv.Columns.Contains(fixedOrder(i)) Then
                    Dim safeIndex As Integer = Math.Min(i, allunitsdgv.Columns.Count - 1)
                    allunitsdgv.Columns(fixedOrder(i)).DisplayIndex = safeIndex
                End If
            Next

            ' 7) Ensure our selection column & header checkbox exist
            EnsureSelectColumn()

            ' 8) Apply read-only rules (bulk editing vs normal)
            ApplyReadOnlyState()

            UpdatePaginationControls(allFilteredRows.Count)

        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message)
        Finally
            allunitsdgv.ResumeLayout()
        End Try
    End Sub

    Private Sub filtertxt_TextChanged(sender As Object, e As EventArgs) Handles filtertxt.TextChanged
        ' fast: only filtering the cached DataTable
        LoadAllUnits(filtertxt.Text, reloadFromDb:=False)
    End Sub


    ' ---------- One-time setup of Assigned To combobox column ----------
    Private Sub InitializeAssignedToColumn()
        ' Load personnel names ONCE
        Dim dtAssignments As DataTable = mdl.GetAssignments()
        personnelNames = dtAssignments.AsEnumerable().
                     Select(Function(r) r("Full name").ToString()).
                     ToList()

        ' Remove text column if it exists
        If allunitsdgv.Columns.Contains("Assigned To") Then
            allunitsdgv.Columns.Remove("Assigned To")
        End If

        assignedToCboCol = New DataGridViewComboBoxColumn() With {
            .HeaderText = "Assigned To",
            .Name = "Assigned To",
            .DataPropertyName = "Assigned To",
            .DropDownWidth = 200,
            .FlatStyle = FlatStyle.Flat,
            .AutoComplete = True,
            .DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
        }
        assignedToCboCol.Items.AddRange(personnelNames.ToArray())

        Dim insertIndex As Integer = Math.Min(2, Math.Max(0, allunitsdgv.Columns.Count))
        allunitsdgv.Columns.Insert(insertIndex, assignedToCboCol)
    End Sub

    ' ---------- One-time setup of Edit/View button columns ----------
    Private Sub InitializeEditViewColumns()
        If Not allunitsdgv.Columns.Contains("Edit") Then
            Dim editBtn As New DataGridViewButtonColumn() With {
                .HeaderText = "",
                .Name = "Edit",
                .Text = "Edit",
                .UseColumnTextForButtonValue = True,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            }
            allunitsdgv.Columns.Add(editBtn)
        End If

        If Not allunitsdgv.Columns.Contains("View") Then
            Dim viewBtn As New DataGridViewButtonColumn() With {
                .HeaderText = "",
                .Name = "View",
                .Text = "View",
                .UseColumnTextForButtonValue = True,
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            }
            allunitsdgv.Columns.Add(viewBtn)
        End If
    End Sub



    ' ----------------- Edit/View Buttons -----------------
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles allunitsdgv.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName = allunitsdgv.Columns(e.ColumnIndex).Name
        If colName <> "Edit" AndAlso colName <> "View" Then Return

        Dim unitId = CInt(allunitsdgv.Rows(e.RowIndex).Cells("unit_id").Value)
        Dim dtUnit = mdl.GetUnitsSummary()
        Dim unitRow = dtUnit.AsEnumerable().FirstOrDefault(Function(r) CInt(r("unit_id")) = unitId)
        If unitRow Is Nothing Then
            MessageBox.Show("Unit data not found.")
            Return
        End If

        Dim unitName = If(unitRow("Unit Name") IsNot DBNull.Value, unitRow("Unit Name").ToString(), "")
        Dim assignedToId = If(unitRow("personnel_id") IsNot DBNull.Value, CInt(unitRow("personnel_id")), 0)
        Dim dtDevices = mdl.GetDeviceSpecsByUnitPointer(unitId)

        ' ============== PANEL SWITCHING (viewpnl removed) ==============
        unitpnl.Visible = True
        unitpnl.BringToFront()
        unitpnl.Controls.Clear()

        If colName = "Edit" Then
            Dim editUC As New EditUnit()
            editUC.Dock = DockStyle.Fill
            unitpnl.Controls.Add(editUC)
            editUC.LoadUnit(unitId, unitName, assignedToId, dtDevices)

        ElseIf colName = "View" Then
            Dim viewUC As New ViewUnit()
            viewUC.Dock = DockStyle.Fill
            unitpnl.Controls.Add(viewUC)
            viewUC.LoadUnit(unitId, unitName, assignedToId, dtDevices)
        End If

        ' Reset bulk edit mode when switching panels
        savedgvbtn.Visible = False
        ApplyReadOnlyState()
    End Sub


    ' ----------------- Pagination -----------------
    Private Sub UpdatePaginationControls(totalRecords As Integer)
        lblPageInfo.Text = $"Page {currentPage} of {If(totalPages < 1, 1, totalPages)}"
        btnPrev.Enabled = (currentPage > 1)
        btnNext.Enabled = (currentPage < totalPages)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If currentPage > 1 Then currentPage -= 1 : LoadAllUnits()
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentPage < totalPages Then currentPage += 1 : LoadAllUnits()
    End Sub

    ' ----------------- Load/Show Panels -----------------
    Private Sub Units_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllUnits(reloadFromDb:=True)   ' first time: hit DB and cache
        isLoading = False
        refreshTimer.Start()
    End Sub

    Private Sub unitaddbtn_Click(sender As Object, e As EventArgs) Handles unitaddbtn.Click
        unitpnl.Visible = True
        unitpnl.BringToFront()
        unitpnl.Controls.Clear()
        Dim addUnitControl As New AddNew
        addUnitControl.Dock = DockStyle.Fill
        unitpnl.Controls.Add(addUnitControl)
        addUnitControl.unit2pnl.Visible = True

        ' Reset bulk edit
        savedgvbtn.Visible = False
        ApplyReadOnlyState()
    End Sub

    Private Sub refreshTimer_Tick(sender As Object, e As EventArgs) Handles refreshTimer.Tick
        ' don’t refresh while user is editing, in panels, or using the select column
        If savedgvbtn.Visible Then Exit Sub
        If unitpnl.Visible Then Exit Sub

        ' skip refresh while user is working with the checkbox column
        If allunitsdgv.Columns.Contains(selectColumnName) Then
            If allunitsdgv.CurrentCell IsNot Nothing AndAlso
               allunitsdgv.CurrentCell.OwningColumn.Name = selectColumnName Then
                Exit Sub
            End If

            If selectAllCheckBox IsNot Nothing AndAlso selectAllCheckBox.Focused Then
                Exit Sub
            End If
        End If

        Dim searchText As String = ""
        If Not (filtertxt.ForeColor = Color.Gray AndAlso filtertxt.Text = "Search") Then
            searchText = filtertxt.Text.Trim()
        End If

        ' Reload from DB (in case there are new changes) but still apply current search
        LoadAllUnits(searchText, reloadFromDb:=True)
    End Sub


    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        unitpnl.Visible = True
        unitpnl.BringToFront()
        unitpnl.Controls.Clear()
        Dim addUnitControl As New AddUnits()
        addUnitControl.Dock = DockStyle.Fill
        unitpnl.Controls.Add(addUnitControl)
        addUnitControl.unit1pnl.Visible = True
        AddHandler addUnitControl.UnitSaved, Sub()
                                                 LoadAllUnits()
                                                 unitpnl.Visible = False
                                             End Sub

        ' Reset bulk edit
        savedgvbtn.Visible = False
        ApplyReadOnlyState()
    End Sub

    ' ----------------- Bulk Edit Toggle -----------------
    Private Sub multibtn_Click(sender As Object, e As EventArgs) Handles multibtn.Click
        If allunitsdgv.DataSource Is Nothing Then Return

        ' Commit any current cell edit to prevent InvalidOperationException
        If allunitsdgv.IsCurrentCellInEditMode Then
            allunitsdgv.EndEdit()
        End If

        ' Toggle saved button visibility (this is our "bulk edit ON/OFF" flag)
        savedgvbtn.Visible = Not savedgvbtn.Visible

        ' Apply rules: checkbox column always editable, others depend on bulk mode
        ApplyReadOnlyState()
    End Sub

    ' ----------------- Save Bulk -----------------
    Private Sub savedgvbtn_Click(sender As Object, e As EventArgs) Handles savedgvbtn.Click
        Try
            ' 🔹 Make sure any in-progress cell edit is committed
            allunitsdgv.EndEdit()

            Dim dt As DataTable = CType(allunitsdgv.DataSource, DataTable)

            ' ================================
            ' 1) CHECK DUPLICATES INSIDE GRID
            ' ================================
            Dim unitNames = dt.AsEnumerable().
                Select(Function(r) If(r("Unit Name") IsNot DBNull.Value, r("Unit Name").ToString().Trim(), "")).
                ToList()

            Dim duplicates = unitNames.
                GroupBy(Function(n) n).
                Where(Function(g) g.Count() > 1 AndAlso g.Key <> "").
                Select(Function(g) g.Key).
                ToList()

            If duplicates.Any() Then
                MessageBox.Show(
                    "Duplicate Unit Names detected in the grid: " & String.Join(", ", duplicates) & vbCrLf &
                    "Please ensure each Unit Name is unique before saving.",
                    "Duplicate Unit Names",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                ' 🔁 RESET ALL CHANGES IN THE DATATABLE + GRID
                dt.RejectChanges()
                allunitsdgv.DataSource = dt   ' optional; often not needed but safe

                Return
            End If


            ' ============================================
            ' 2) CHECK AGAINST DATABASE (GLOBAL UNIQUENESS)
            ' ============================================
            Dim existingDupes As New List(Of String)()

            For Each row As DataRow In dt.Rows
                If row.IsNull("unit_id") Then Continue For

                Dim newName As String = If(row("Unit Name") IsNot DBNull.Value, row("Unit Name").ToString().Trim(), "")
                If newName = "" Then Continue For

                Dim unitId As Integer = CInt(row("unit_id"))

                ' find original row for comparison
                Dim origRow = allFilteredRows.FirstOrDefault(Function(r) CInt(r("unit_id")) = unitId)
                Dim origName As String = If(origRow IsNot Nothing AndAlso origRow("Unit Name") IsNot DBNull.Value,
                                        origRow("Unit Name").ToString().Trim(),
                                        "")

                ' only check DB if the name actually changed (case-insensitive)
                If Not String.Equals(newName, origName, StringComparison.OrdinalIgnoreCase) Then
                    If mdl.IsUnitNameExists(newName) Then

                        ' RESET DataRow value back to original
                        row("Unit Name") = origName

                        ' RESET THE GRID CELL so you see it immediately
                        For Each gridRow As DataGridViewRow In allunitsdgv.Rows
                            If gridRow.IsNewRow Then Continue For
                            If CInt(gridRow.Cells("unit_id").Value) = unitId Then
                                gridRow.Cells("Unit Name").Value = origName
                                allunitsdgv.CurrentCell = gridRow.Cells("Unit Name")
                                Exit For
                            End If
                        Next

                        ' track duplicate name
                        If Not existingDupes.Any(Function(x) String.Equals(x, newName, StringComparison.OrdinalIgnoreCase)) Then
                            existingDupes.Add(newName)
                        End If
                    End If
                End If
            Next

            If existingDupes.Any() Then
                allunitsdgv.Refresh()

                MessageBox.Show(
                    "The following Unit Name(s) already exist in the database: " &
                    String.Join(", ", existingDupes) & vbCrLf &
                    "The grid will be reset to the last saved data.",
                    "Existing Unit Names",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )

                dt.RejectChanges()
                allunitsdgv.DataSource = dt
                Return
            End If


            ' ================================
            ' 3) BUILD SUMMARY OF CHANGES
            ' ================================
            Dim summary As New Text.StringBuilder()
            For Each row As DataRow In dt.Rows
                Dim origRow = allFilteredRows.FirstOrDefault(Function(r) CInt(r("unit_id")) = CInt(row("unit_id")))
                If origRow IsNot Nothing Then
                    Dim changes As New List(Of String)

                    ' Check Unit Name
                    If row("Unit Name").ToString() <> origRow("Unit Name").ToString() Then
                        changes.Add($"Unit Name: '{origRow("Unit Name")}' → '{row("Unit Name")}'")
                    End If

                    ' Check Assigned To
                    Dim origAssigned As String = If(origRow("Assigned To") IsNot DBNull.Value, origRow("Assigned To").ToString(), "")
                    Dim newAssigned As String = If(row("Assigned To") IsNot DBNull.Value, row("Assigned To").ToString(), "")
                    If origAssigned <> newAssigned Then
                        changes.Add($"Assigned To: '{origAssigned}' → '{newAssigned}'")
                    End If

                    If changes.Any() Then
                        summary.AppendLine($"Unit '{origRow("Unit Name")}': " & String.Join(", ", changes))
                    End If
                End If
            Next

            If summary.Length = 0 Then
                MessageBox.Show("No changes detected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' ================================
            ' 4) CONFIRM + SAVE
            ' ================================
            Dim result = MessageBox.Show(
                "The following changes will be made:" & vbCrLf &
                summary.ToString() & vbCrLf & vbCrLf &
                "Proceed?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )
            If result <> DialogResult.Yes Then
                Return
            End If

            ' Save changes
            Dim saveSummary As String = mdl.UpdateUnitsBulk(dt, personnelNames, Session.LoggedInUserPointer)

            ' Show final summary
            MessageBox.Show(saveSummary, "Update Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Reset grid
            savedgvbtn.Visible = False
            ApplyReadOnlyState()
            LoadAllUnits()

        Catch ex As Exception
            MessageBox.Show("Error saving changes: " & ex.Message)
        End Try
    End Sub



    ' ----------------- Set ComboBox style for Assigned To -----------------
    Private Sub allunitsdgv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles allunitsdgv.EditingControlShowing
        If allunitsdgv.CurrentCell IsNot Nothing AndAlso
           allunitsdgv.CurrentCell.ColumnIndex = allunitsdgv.Columns("Assigned To").Index Then
            Dim combo As ComboBox = TryCast(e.Control, ComboBox)
            If combo IsNot Nothing Then
                combo.DropDownStyle = ComboBoxStyle.DropDown
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                combo.AutoCompleteSource = AutoCompleteSource.ListItems
            End If
        End If
    End Sub

    ' ---------------- Validate Assigned To -----------------
    Private Sub allunitsdgv_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles allunitsdgv.CellValidating
        If allunitsdgv.Columns(e.ColumnIndex).HeaderText <> "Assigned To" Then Return

        Dim inputName As String = e.FormattedValue.ToString().Trim()
        If String.IsNullOrEmpty(inputName) Then Return

        Dim normalize As Func(Of String, String) = Function(s) s.ToLower().Replace(",", "").Trim()
        Dim normalizedInput As String = normalize(inputName)

        Dim matchedName As String = Nothing

        For Each fullName In personnelNames
            Dim n = normalize(fullName)

            ' exact match
            If n = normalizedInput Then
                matchedName = fullName
                Exit For
            End If

            ' allow swapped (FIRST LAST vs LAST FIRST)
            Dim parts = n.Split(" "c)
            If parts.Length >= 2 Then
                Dim swapped = parts(1) & " " & parts(0)
                If swapped = normalizedInput Then
                    matchedName = fullName
                    Exit For
                End If
            End If
        Next

        If matchedName Is Nothing Then
            MessageBox.Show($"Assigned personnel '{inputName}' does not exist in the database.",
                            "Invalid Personnel",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            allunitsdgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
            e.Cancel = True
        Else
            allunitsdgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = matchedName
        End If
    End Sub


    ' ============================================================
    '   selection column + header checkbox logic
    ' ============================================================
    Private Sub EnsureSelectColumn()
        ' --- 1. add column if needed ---
        If Not allunitsdgv.Columns.Contains(selectColumnName) Then
            Dim chkCol As New DataGridViewCheckBoxColumn() With {
            .Name = selectColumnName,
            .HeaderText = "Select All",
            .ReadOnly = False,
            .Width = 110,
            .MinimumWidth = 110,
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        }

            ' left-align header text so checkbox can sit to the right of it
            chkCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

            Dim insertIndex As Integer = 0
            If allunitsdgv.Columns.Contains("Unit Name") Then
                insertIndex = allunitsdgv.Columns("Unit Name").Index
            End If

            allunitsdgv.Columns.Insert(insertIndex, chkCol)
        End If


        ' --- 2. Force it to be BEFORE "Unit Name" visually ---
        If allunitsdgv.Columns.Contains("Unit Name") Then
            Dim unitDisp As Integer = allunitsdgv.Columns("Unit Name").DisplayIndex
            Dim selCol As DataGridViewColumn = allunitsdgv.Columns(selectColumnName)

            selCol.DisplayIndex = unitDisp
            allunitsdgv.Columns("Unit Name").DisplayIndex = unitDisp + 1
        End If

        ' --- 3. header checkbox (select all) ---
        If selectAllCheckBox Is Nothing Then
            selectAllCheckBox = New CheckBox() With {
                .Name = "chkSelectAll",
                .BackColor = Color.Transparent,
                .Size = New Size(15, 15)
            }
            AddHandler selectAllCheckBox.CheckedChanged, AddressOf SelectAllCheckBox_CheckedChanged
            allunitsdgv.Controls.Add(selectAllCheckBox)
        End If

        RepositionHeaderCheckBox()
    End Sub

    Private Sub ApplyReadOnlyState()
        ' We control per-column ReadOnly instead of full grid
        allunitsdgv.ReadOnly = False

        For Each col As DataGridViewColumn In allunitsdgv.Columns
            If col.Name = selectColumnName Then
                ' checkbox column ALWAYS editable
                col.ReadOnly = False
            ElseIf savedgvbtn.Visible AndAlso
                   (col.Name = "Unit Name" OrElse col.Name = "Assigned To") Then
                ' Bulk editing ON → Unit Name + Assigned To editable
                col.ReadOnly = False
            Else
                col.ReadOnly = True
            End If
        Next
    End Sub

    Private Sub SelectAllCheckBox_CheckedChanged(sender As Object, e As EventArgs)
        If Not allunitsdgv.Columns.Contains(selectColumnName) Then Return

        allunitsdgv.EndEdit()

        For Each row As DataGridViewRow In allunitsdgv.Rows
            If row.IsNewRow Then Continue For
            row.Cells(selectColumnName).Value = CType(sender, CheckBox).Checked
        Next
    End Sub

    Private Sub RepositionHeaderCheckBox()
        If selectAllCheckBox Is Nothing Then Return
        If Not allunitsdgv.Columns.Contains(selectColumnName) Then Return

        Dim dispIndex As Integer = allunitsdgv.Columns(selectColumnName).DisplayIndex
        Dim headerRect As Rectangle = allunitsdgv.GetCellDisplayRectangle(dispIndex, -1, True)

        ' measure the "Select All" text width in the header font
        Dim txt As String = allunitsdgv.Columns(selectColumnName).HeaderText
        Dim fnt As Font = If(allunitsdgv.ColumnHeadersDefaultCellStyle.Font, allunitsdgv.Font)
        Dim textSize As Size = TextRenderer.MeasureText(txt, fnt)

        ' X = left edge + text width + small padding
        Dim x As Integer = headerRect.X + textSize.Width + 4
        ' clamp so checkbox never goes outside the header cell
        If x + selectAllCheckBox.Width > headerRect.Right - 2 Then
            x = headerRect.Right - selectAllCheckBox.Width - 2
        End If

        Dim y As Integer = headerRect.Y + (headerRect.Height - selectAllCheckBox.Height) \ 2

        selectAllCheckBox.Location = New Point(x, y)
    End Sub



    ' keep header checkbox aligned
    Private Sub allunitsdgv_Scroll(sender As Object, e As ScrollEventArgs) Handles allunitsdgv.Scroll
        RepositionHeaderCheckBox()
    End Sub

    Private Sub allunitsdgv_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles allunitsdgv.ColumnWidthChanged
        RepositionHeaderCheckBox()
    End Sub

    Private Sub allunitsdgv_SizeChanged(sender As Object, e As EventArgs) Handles allunitsdgv.SizeChanged
        RepositionHeaderCheckBox()
    End Sub



    ' ============================================================
    '   QR GENERATION FROM CHECKED ROWS (MULTI-SELECT, MULTI-PAGE)
    ' ============================================================
    Private Sub qrbtngenerate_Click(sender As Object, e As EventArgs) Handles qrbtngenerate.Click
        If allunitsdgv.DataSource Is Nothing Then
            MessageBox.Show("No units loaded.", "QR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If Not allunitsdgv.Columns.Contains(selectColumnName) Then
            MessageBox.Show("Selection column not found.", "QR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' make sure checkbox edits are committed
        allunitsdgv.EndEdit()

        Dim selectedUnitIds As New List(Of Integer)()
        Dim collectionNames As New List(Of String)()

        For Each row As DataGridViewRow In allunitsdgv.Rows
            If row.IsNewRow Then Continue For

            Dim cellValue = row.Cells(selectColumnName).Value
            Dim isChecked As Boolean = False

            If TypeOf cellValue Is Boolean Then
                isChecked = CBool(cellValue)
            ElseIf cellValue IsNot Nothing Then
                Boolean.TryParse(cellValue.ToString(), isChecked)
            End If

            If isChecked Then
                Dim idObj = row.Cells("unit_id").Value
                If idObj IsNot Nothing AndAlso IsNumeric(idObj) Then
                    Dim uid As Integer = CInt(idObj)
                    selectedUnitIds.Add(uid)

                    Dim collName As String = ""
                    If row.Cells("Unit Name").Value IsNot Nothing Then
                        collName = row.Cells("Unit Name").Value.ToString()
                    End If
                    collectionNames.Add(collName)
                End If
            End If
        Next

        If selectedUnitIds.Count = 0 Then
            MessageBox.Show("Please check at least one unit to generate QR stickers.",
                            "QR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' --- generate QR for each selected unit ---
        Dim qrBitmaps As New List(Of Bitmap)()
        Dim encStrings As New List(Of String)()

        For i As Integer = 0 To selectedUnitIds.Count - 1
            Dim unitId As Integer = selectedUnitIds(i)

            Dim qrBmp As Bitmap = Nothing
            Dim enc As String = ""

            GenerateUnitQr(unitId, qrBmp, enc)

            If qrBmp IsNot Nothing Then
                qrBitmaps.Add(qrBmp)
                encStrings.Add(enc)
            End If
        Next

        If qrBitmaps.Count = 0 Then
            MessageBox.Show("No QR images were generated.", "QR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- send to QRPrintView for multi-page preview (6 per page) ---
        Dim qrPrint As New QRPrintView()
        qrPrint.LoadStickerBatch(qrBitmaps, encStrings, collectionNames)
        qrPrint.ShowStickerPreview()
    End Sub

    ' === uses your QRGenerator + Encrypt ===
    Private Sub GenerateUnitQr(unitId As Integer, ByRef qrBmp As Bitmap, ByRef enc As String)
        Try
            enc = ""
            qrBmp = QRGenerator.GenerateUnitQR(unitId, enc)
        Catch ex As Exception
            MessageBox.Show("Error generating QR for unit " & unitId & ": " & ex.Message,
                            "QR Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            qrBmp = Nothing
        End Try
    End Sub


End Class
