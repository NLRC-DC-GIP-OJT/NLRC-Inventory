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

    '' This function loads the summarized data for units into the DataGridView
    Private Sub LoadAllUnits(Optional search As String = "")
        Try
            ' Get all unit summaries
            Dim dt As DataTable = mdl.GetUnitsSummary()

            ' Filter rows if search criteria is provided
            Dim filtered = dt.AsEnumerable()
            If Not String.IsNullOrWhiteSpace(search) AndAlso Not (filtertxt.ForeColor = Color.Gray AndAlso search = "Search") Then
                Dim s = search.ToLower()
                filtered = filtered.Where(Function(r) r.ItemArray.Any(Function(val) val IsNot Nothing AndAlso val.ToString().ToLower().Contains(s)))
            End If

            ' Update pagination
            allFilteredRows = filtered.ToList()
            totalPages = Math.Ceiling(allFilteredRows.Count / pageSize)
            If currentPage > totalPages Then currentPage = totalPages
            If currentPage < 1 Then currentPage = 1

            ' Load data for the current page
            Dim pageRows = allFilteredRows.Skip((currentPage - 1) * pageSize).Take(pageSize)
            If pageRows.Any() Then
                allunitsdgv.DataSource = pageRows.CopyToDataTable()
            Else
                allunitsdgv.DataSource = Nothing
            End If

            ' Grid appearance settings
            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.AllowUserToAddRows = False
            allunitsdgv.ReadOnly = True
            allunitsdgv.RowHeadersVisible = False

            ' Hide unit_id and personnel_id columns
            If allunitsdgv.Columns.Contains("unit_id") Then allunitsdgv.Columns("unit_id").Visible = False
            If allunitsdgv.Columns.Contains("personnel_id") Then allunitsdgv.Columns("personnel_id").Visible = False

            ' Add Edit/View buttons if not already present
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

            ' Update pagination controls
            UpdatePaginationControls(allFilteredRows.Count)

        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message)
        End Try
    End Sub

    ' This method is triggered when a button is clicked in the DataGridView (Edit/View)
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles allunitsdgv.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colName As String = allunitsdgv.Columns(e.ColumnIndex).Name
        Dim unitPointer As Integer = CInt(allunitsdgv.Rows(e.RowIndex).Cells("unit_id").Value)

        If colName = "View" Then
            ' Fetch unit details
            Dim dtUnit As DataTable = mdl.GetUnitsSummary()  ' Fetch the unit summary
            Dim unitRow As DataRow = dtUnit.AsEnumerable().FirstOrDefault(Function(r) CInt(r("unit_id")) = unitPointer)

            ' Fetch device specs for the selected unit using the updated function
            Dim dtDeviceSpecs As DataTable = mdl.GetDeviceSpecsByUnitPointer(unitPointer)

            ' Prepare the details message
            Dim details As New StringBuilder()

            ' Show unit information (Unit Name and Assigned Personnel)
            If unitRow IsNot Nothing Then
                Dim unitName = If(unitRow("Unit Name") IsNot DBNull.Value, unitRow("Unit Name").ToString(), "None")
                Dim assignedTo = If(unitRow("Assigned To") IsNot DBNull.Value, unitRow("Assigned To").ToString(), "None")

                details.AppendLine($"Unit: {unitName}")
                details.AppendLine($"Assigned To: {assignedTo}")
            Else
                details.AppendLine("Unit information not available.")
            End If

            details.AppendLine("") ' Add an empty line for spacing

            ' Show devices and specs for the selected unit
            details.AppendLine("Devices and Specs:")
            details.AppendLine("") ' Add an empty line for spacing

            If dtDeviceSpecs.Rows.Count = 0 Then
                details.AppendLine("No devices assigned to this unit or no specs available.")
            Else
                ' List devices and their specs
                For Each row As DataRow In dtDeviceSpecs.Rows
                    details.AppendLine($"• {row("DeviceAndSpecs")}")
                Next
            End If

            ' Show the message box with unit, device, and specs details
            MessageBox.Show(details.ToString(), "Unit and Devices", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
