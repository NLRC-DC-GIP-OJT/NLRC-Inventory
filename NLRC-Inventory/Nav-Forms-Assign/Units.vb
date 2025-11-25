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

    ' ----------------- Load Units -----------------
    Private Sub LoadAllUnits(Optional search As String = "")
        Try
            Dim dt As DataTable = mdl.GetUnitsSummary()

            ' Filter search
            Dim filtered = dt.AsEnumerable()
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
                allunitsdgv.DataSource = pageRows.CopyToDataTable()
            Else
                allunitsdgv.DataSource = Nothing
            End If

            ' Setup grid
            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.AllowUserToAddRows = False
            allunitsdgv.ReadOnly = True
            allunitsdgv.RowHeadersVisible = False

            ' Hide ID columns
            If allunitsdgv.Columns.Contains("unit_id") Then allunitsdgv.Columns("unit_id").Visible = False
            If allunitsdgv.Columns.Contains("personnel_id") Then allunitsdgv.Columns("personnel_id").Visible = False

            ' Load personnel names
            personnelNames = mdl.GetAssignments().AsEnumerable().[Select](Function(r) r("Full name").ToString()).ToList()

            ' Replace Assigned To column with ComboBox
            If allunitsdgv.Columns.Contains("Assigned To") Then
                allunitsdgv.Columns.Remove("Assigned To")
            End If

            assignedToCboCol = New DataGridViewComboBoxColumn()
            assignedToCboCol.HeaderText = "Assigned To"
            assignedToCboCol.Name = "Assigned To"
            assignedToCboCol.DataPropertyName = "Assigned To"
            assignedToCboCol.DropDownWidth = 200
            assignedToCboCol.FlatStyle = FlatStyle.Flat
            assignedToCboCol.AutoComplete = True
            assignedToCboCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            assignedToCboCol.Items.AddRange(personnelNames.ToArray())

            ' >>> CHANGED PART HERE <<<
            'allunitsdgv.Columns.Insert(2, assignedToCboCol) ' OLD LINE
            Dim insertIndex As Integer = Math.Min(2, allunitsdgv.Columns.Count)
            allunitsdgv.Columns.Insert(insertIndex, assignedToCboCol)
            ' >>> END OF CHANGE <<<

            ' --- Fix column order: Unit Name | Device No | Assigned To | Created At | Updated At ---
            Dim fixedOrder As String() = {"Unit Name", "Device No", "Assigned To", "Created At", "Updated At"}
            For i As Integer = 0 To fixedOrder.Length - 1
                If allunitsdgv.Columns.Contains(fixedOrder(i)) Then
                    allunitsdgv.Columns(fixedOrder(i)).DisplayIndex = i
                End If
            Next

            ' Add Edit/View buttons at the far right
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

            UpdatePaginationControls(allFilteredRows.Count)

        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message)
        End Try
    End Sub


    ' ----------------- Edit/View Buttons -----------------
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles allunitsdgv.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName = allunitsdgv.Columns(e.ColumnIndex).Name
        If colName <> "Edit" AndAlso colName <> "View" Then Return

        Dim unitId = CInt(allunitsdgv.Rows(e.RowIndex).Cells("unit_id").Value)
        Dim dtUnit = mdl.GetUnitsSummary()
        Dim unitRow = dtUnit.AsEnumerable().FirstOrDefault(Function(r) CInt(r("unit_id")) = unitId)
        If unitRow Is Nothing Then MessageBox.Show("Unit data not found.") : Return

        Dim unitName = If(unitRow("Unit Name") IsNot DBNull.Value, unitRow("Unit Name").ToString(), "")
        Dim assignedToId = If(unitRow("personnel_id") IsNot DBNull.Value, CInt(unitRow("personnel_id")), 0)
        Dim dtDevices = mdl.GetDeviceSpecsByUnitPointer(unitId)

        unitpnl.Visible = False
        viewpnl.Controls.Clear()
        viewpnl.Visible = True
        viewpnl.BringToFront()

        If colName = "Edit" Then
            Dim editUC As New EditUnit()
            editUC.Dock = DockStyle.Fill
            viewpnl.Controls.Add(editUC)
            editUC.LoadUnit(unitId, unitName, assignedToId, dtDevices)
        ElseIf colName = "View" Then
            Dim viewUC As New ViewUnit()
            viewUC.Dock = DockStyle.Fill
            viewpnl.Controls.Add(viewUC)
            viewUC.LoadUnit(unitName, assignedToId, dtDevices)
        End If

        ' Reset bulk edit mode when switching panels
        allunitsdgv.ReadOnly = True
        savedgvbtn.Visible = False
        For Each col As DataGridViewColumn In allunitsdgv.Columns
            col.ReadOnly = True
        Next
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
        LoadAllUnits()
        isLoading = False
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
        allunitsdgv.ReadOnly = True
        savedgvbtn.Visible = False
        For Each col As DataGridViewColumn In allunitsdgv.Columns
            col.ReadOnly = True
        Next
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
        allunitsdgv.ReadOnly = True
        savedgvbtn.Visible = False
        For Each col As DataGridViewColumn In allunitsdgv.Columns
            col.ReadOnly = True
        Next
    End Sub

    ' ----------------- Bulk Edit Toggle -----------------
    Private Sub multibtn_Click(sender As Object, e As EventArgs) Handles multibtn.Click
        If allunitsdgv.DataSource Is Nothing Then Return

        ' Commit any current cell edit to prevent InvalidOperationException
        If allunitsdgv.IsCurrentCellInEditMode Then
            allunitsdgv.EndEdit()
        End If

        ' Toggle saved button visibility
        savedgvbtn.Visible = Not savedgvbtn.Visible

        ' Toggle ReadOnly state
        allunitsdgv.ReadOnly = Not savedgvbtn.Visible

        ' Set editable columns
        For Each col As DataGridViewColumn In allunitsdgv.Columns
            If col.Name = "Unit Name" OrElse col.Name = "Assigned To" Then
                col.ReadOnly = Not savedgvbtn.Visible
            Else
                col.ReadOnly = True
            End If
        Next
    End Sub

    ' ----------------- Save Bulk -----------------
    Private Sub savedgvbtn_Click(sender As Object, e As EventArgs) Handles savedgvbtn.Click
        Try
            Dim dt As DataTable = CType(allunitsdgv.DataSource, DataTable)

            ' Check for duplicate Unit Names
            Dim unitNames = dt.AsEnumerable().Select(Function(r) If(r("Unit Name") IsNot DBNull.Value, r("Unit Name").ToString().Trim(), "")).ToList()
            Dim duplicates = unitNames.GroupBy(Function(n) n).Where(Function(g) g.Count() > 1 AndAlso g.Key <> "").Select(Function(g) g.Key).ToList()

            If duplicates.Any() Then
                MessageBox.Show("Duplicate Unit Names detected: " & String.Join(", ", duplicates) & vbCrLf &
                            "Please ensure each Unit Name is unique before saving.", "Duplicate Unit Names", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Build summary of changes (show Unit Name, no Unit ID)
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

                    ' If there are any changes, show them under the unit
                    If changes.Any() Then
                        summary.AppendLine($"Unit '{origRow("Unit Name")}': " & String.Join(", ", changes))
                    End If
                End If
            Next

            If summary.Length = 0 Then
                MessageBox.Show("No changes detected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Ask for confirmation with summary
            Dim result = MessageBox.Show("The following changes will be made:" & vbCrLf & summary.ToString() & vbCrLf & vbCrLf & "Proceed?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result <> DialogResult.Yes Then
                Return
            End If

            ' Save changes
            Dim saveSummary As String = mdl.UpdateUnitsBulk(dt, personnelNames, Session.LoggedInUserPointer)

            ' Show final summary
            MessageBox.Show(saveSummary, "Update Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Reset grid
            allunitsdgv.ReadOnly = True
            savedgvbtn.Visible = False
            LoadAllUnits()

        Catch ex As Exception
            MessageBox.Show("Error saving changes: " & ex.Message)
        End Try
    End Sub




    ' ----------------- Set ComboBox style for Assigned To -----------------
    Private Sub allunitsdgv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles allunitsdgv.EditingControlShowing
        If allunitsdgv.CurrentCell.ColumnIndex = allunitsdgv.Columns("Assigned To").Index Then
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
        If allunitsdgv.Columns(e.ColumnIndex).HeaderText = "Assigned To" Then
            Dim inputName As String = e.FormattedValue.ToString().Trim()
            If String.IsNullOrEmpty(inputName) Then Return

            Dim normalize As Func(Of String, String) = Function(s) s.ToLower().Replace(",", "").Trim()
            Dim normalizedInput As String = normalize(inputName)

            Dim isValid As Boolean = personnelNames.Any(Function(fullName)
                                                            Dim n = normalize(fullName)
                                                            If n = normalizedInput Then Return True
                                                            Dim parts = n.Split(" "c)
                                                            If parts.Length >= 2 Then
                                                                Dim swapped = parts(1) & " " & parts(0)
                                                                If swapped = normalizedInput Then Return True
                                                            End If
                                                            Return False
                                                        End Function)

            If Not isValid Then
                MessageBox.Show($"Assigned personnel '{inputName}' does not exist in the database.", "Invalid Personnel", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                allunitsdgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
                e.Cancel = True
            End If
        End If
    End Sub
End Class