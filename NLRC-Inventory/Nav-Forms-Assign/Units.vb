Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Units
    Private mdl As New model()

    Private currentPage As Integer = 1
    Private pageSize As Integer = 30
    Private totalPages As Integer = 1
    Private allFilteredRows As List(Of DataRow)

    Private isLoading As Boolean = True

    ' Load units with optional search
    Private Sub LoadAllUnits(Optional search As String = "")
        Try
            Dim dt As DataTable = mdl.GetUnitsSummary()

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

            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.AllowUserToAddRows = False
            allunitsdgv.ReadOnly = True
            allunitsdgv.RowHeadersVisible = False

            If allunitsdgv.Columns.Contains("unit_id") Then allunitsdgv.Columns("unit_id").Visible = False
            If allunitsdgv.Columns.Contains("personnel_id") Then allunitsdgv.Columns("personnel_id").Visible = False

            ' Add Edit/View buttons
            If Not allunitsdgv.Columns.Contains("Edit") Then
                Dim editBtn As New DataGridViewButtonColumn() With {.HeaderText = "Edit", .Name = "Edit", .Text = "Edit", .UseColumnTextForButtonValue = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells}
                allunitsdgv.Columns.Add(editBtn)
            End If
            If Not allunitsdgv.Columns.Contains("View") Then
                Dim viewBtn As New DataGridViewButtonColumn() With {.HeaderText = "View", .Name = "View", .Text = "View", .UseColumnTextForButtonValue = True, .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells}
                allunitsdgv.Columns.Add(viewBtn)
            End If

            UpdatePaginationControls(allFilteredRows.Count)
        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message)
        End Try
    End Sub

    ' Handle Edit/View button click
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles allunitsdgv.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName = allunitsdgv.Columns(e.ColumnIndex).Name
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
            ' EditUnit expects 4 arguments (unitId, unitName, assignedToId, dtDevices)
            Dim editUC As New EditUnit()
            editUC.Dock = DockStyle.Fill
            viewpnl.Controls.Add(editUC)
            editUC.LoadUnit(unitId, unitName, assignedToId, dtDevices)
        ElseIf colName = "View" Then
            ' ViewUnit expects 3 arguments (unitName, assignedToId, dtDevices)
            Dim viewUC As New ViewUnit()
            viewUC.Dock = DockStyle.Fill
            viewpnl.Controls.Add(viewUC)
            viewUC.LoadUnit(unitName, assignedToId, dtDevices)
        End If

    End Sub

    ' Pagination
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

    Private Sub Units_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllUnits()
        isLoading = False
    End Sub
End Class
