Imports System.Text
Imports MySql.Data.MySqlClient

Public Class Units
    Private mdl As New model()

    ' 🧮 Pagination variables (same as devices)
    Private currentPage As Integer = 1
    Private pageSize As Integer = 30
    Private totalPages As Integer = 1
    Private allFilteredRows As List(Of DataRow)

    Private isLoading As Boolean = True

    ' 🔹 Load summarized data with pagination
    Private Sub LoadAllUnits(Optional search As String = "")
        Try
            Dim dt As DataTable = mdl.GetUnitsSummary()

            ' Filter rows
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

            ' Appearance
            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.AllowUserToAddRows = False
            allunitsdgv.ReadOnly = True
            allunitsdgv.RowHeadersVisible = False

            ' Hide unit_id and personnel_id
            If allunitsdgv.Columns.Contains("unit_id") Then allunitsdgv.Columns("unit_id").Visible = False
            If allunitsdgv.Columns.Contains("personnel_id") Then allunitsdgv.Columns("personnel_id").Visible = False

            ' Add Edit/View buttons if not exist
            If Not allunitsdgv.Columns.Contains("Edit") Then
                Dim editBtn As New DataGridViewButtonColumn() With {
                    .HeaderText = "Edit",
                    .Name = "Edit",
                    .Text = "Edit",
                    .UseColumnTextForButtonValue = True,
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                }
                allunitsdgv.Columns.Add(editBtn)
            End If
            If Not allunitsdgv.Columns.Contains("View") Then
                Dim viewBtn As New DataGridViewButtonColumn() With {
                    .HeaderText = "View",
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

    ' 🔹 DataGridView button clicks
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles allunitsdgv.CellContentClick
        If e.RowIndex < 0 Then Return
        Dim colName As String = allunitsdgv.Columns(e.ColumnIndex).Name

        Dim unitName As String = allunitsdgv.Rows(e.RowIndex).Cells("Unit Name").Value.ToString()
        Dim personnelID As Integer = CInt(allunitsdgv.Rows(e.RowIndex).Cells("personnel_id").Value)
        Dim personnelName As String = allunitsdgv.Rows(e.RowIndex).Cells("Assigned To").Value.ToString()

        If colName = "View" Then
            Dim dtDevices As DataTable = mdl.GetDevicesByUnit(unitName, personnelID)

            Dim details As New StringBuilder()
            details.AppendLine($"Devices under {personnelName}")
            details.AppendLine($"Unit: {unitName}")
            details.AppendLine("")

            For Each row As DataRow In dtDevices.Rows
                details.AppendLine($"• {row("Category")} | {row("Brand")} {row("Model")} | {row("Status")}")
            Next

            MessageBox.Show(details.ToString(), $"{personnelName} Devices", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf colName = "Edit" Then
            MessageBox.Show("Edit feature not implemented yet.")
        End If
    End Sub

    ' 🔹 Add Button (shows unit1pnl + savepnl1)
    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        unitpnl.Visible = True
        unitpnl.BringToFront()
        unitpnl.Controls.Clear()

        Dim addUnitControl As New AddUnits()
        addUnitControl.Dock = DockStyle.Fill
        unitpnl.Controls.Add(addUnitControl)

        ' ✅ Show the correct panels for ADD
        addUnitControl.unit1pnl.Visible = True


        ' ❌ Hide the other set
        addUnitControl.unit2pnl.Visible = False
        addUnitControl.savepnl1.Visible = False

        AddHandler addUnitControl.UnitSaved, Sub()
                                                 LoadAllUnits()
                                                 unitpnl.Visible = False
                                             End Sub
    End Sub



    ' 🔹 Form Load
    Private Sub Units_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAllUnits()
        isLoading = False
    End Sub

    ' 🔢 Pagination controls (same naming as devices)
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

    ' 🔹 Search filter (same as devices)
    Private Sub filtertxt_KeyUp(sender As Object, e As KeyEventArgs)
        If filtertxt.ForeColor = Color.Gray Then Return
        currentPage = 1
        LoadAllUnits(filtertxt.Text.Trim())
    End Sub

    ' 🔹 Unit Add Button (shows unit2pnl + savepnl)
    Private Sub unitaddbtn_Click(sender As Object, e As EventArgs) Handles unitaddbtn.Click
        unitpnl.Visible = True
        unitpnl.BringToFront()
        unitpnl.Controls.Clear()

        Dim addUnitControl As New AddNew()
        addUnitControl.Dock = DockStyle.Fill
        unitpnl.Controls.Add(addUnitControl)

        ' ✅ Show the correct panels for UNIT ADD
        addUnitControl.unit2pnl.Visible = True



    End Sub



End Class
