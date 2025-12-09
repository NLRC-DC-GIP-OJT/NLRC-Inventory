Imports System.Data
Imports System.IO
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.Drawing   ' for SystemColors
Imports System.Collections.Generic

Public Class Reports

    Private mdl As New model()          ' your data access class
    Private allUnitsView As DataView    ' for filtering

    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    Private Sub Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeLayoutScaling()
        LoadAllUnits()
        InitDeviceCombo()
    End Sub

    ' ========================
    ' DEVICE COMBO (All / Desktop / Laptop)
    ' ========================
    Private Sub InitDeviceCombo()
        If devicecb Is Nothing Then Return

        devicecb.Items.Clear()
        devicecb.Items.Add("All")
        devicecb.Items.Add("Desktop")
        devicecb.Items.Add("Laptop")
        devicecb.SelectedIndex = 0   ' default = All
    End Sub

    ' ===== auto-resize support =====
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
        For Each kvp In originalBounds
            Dim ctrl = kvp.Key
            If ctrl Is Nothing OrElse ctrl.IsDisposed Then Continue For

            Dim r = kvp.Value
            ctrl.Bounds = New Rectangle(
                CInt(r.X * scaleX),
                CInt(r.Y * scaleY),
                CInt(r.Width * scaleX),
                CInt(r.Height * scaleY)
            )

            If ctrl.Font IsNot Nothing Then
                Dim f = ctrl.Font
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
    ' DATA LOADING
    ' ========================
    Private Sub LoadAllUnits()
        Try
            Dim dt As DataTable = mdl.GetUnitDevicesNsoc()

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                allunitsdgv.DataSource = Nothing
                allUnitsView = Nothing
                Return
            End If

            allUnitsView = dt.DefaultView

            allunitsdgv.AutoGenerateColumns = True
            allunitsdgv.DataSource = allUnitsView

            ' ==== INCLUDED → hide text & add button column ====
            If allunitsdgv.Columns.Contains("INCLUDED") Then
                allunitsdgv.Columns("INCLUDED").Visible = False
            End If

            If Not allunitsdgv.Columns.Contains("IncludeButton") Then
                Dim btnCol As New DataGridViewButtonColumn()
                btnCol.Name = "IncludeButton"
                btnCol.HeaderText = "INCLUDED"
                btnCol.Text = "View…"
                btnCol.UseColumnTextForButtonValue = True
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                allunitsdgv.Columns.Add(btnCol)
            End If
            ' ================================================

            ' Fix the header text for BrandModelFeature
            If allunitsdgv.Columns.Contains("BrandModelFeature") Then
                allunitsdgv.Columns("BrandModelFeature").HeaderText =
                    "BRAND/MODEL/IDENTIFYING FEATURE (IF ANY) [INDICATE IF LAPTOP]"
            End If

            ' ===== BASIC BEHAVIOR =====
            allunitsdgv.ReadOnly = True
            allunitsdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            allunitsdgv.MultiSelect = False

            ' ===== LESS CONGESTED =====
            allunitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
            allunitsdgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None

            allunitsdgv.DefaultCellStyle.Padding = New Padding(5, 4, 5, 4)
            allunitsdgv.RowTemplate.Height = 28
            For Each row As DataGridViewRow In allunitsdgv.Rows
                row.Height = 28
            Next

            allunitsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
            allunitsdgv.ColumnHeadersHeight = 32
            allunitsdgv.ColumnHeadersDefaultCellStyle.Padding = New Padding(0, 4, 0, 4)

            allunitsdgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255)

            allunitsdgv.AllowUserToResizeRows = False
            allunitsdgv.AllowUserToResizeColumns = True

            allunitsdgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
            allunitsdgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText
            allunitsdgv.RowHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight
            allunitsdgv.RowHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText

            allunitsdgv.EnableHeadersVisualStyles = False
            allunitsdgv.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                allunitsdgv.ColumnHeadersDefaultCellStyle.BackColor
            allunitsdgv.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                allunitsdgv.ColumnHeadersDefaultCellStyle.ForeColor

        Catch ex As Exception
            MessageBox.Show("Error loading units: " & ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========================
    ' APPLY BOTH FILTERS: devicecb + filtertxt
    ' ========================
    Private Sub ApplyFilters()
        If allUnitsView Is Nothing Then Return

        Dim filters As New List(Of String)

        ' 1) Device filter (Desktop / Laptop / All)
        Dim deviceFilter As String = ""
        If devicecb IsNot Nothing AndAlso devicecb.SelectedItem IsNot Nothing Then
            Dim dev As String = devicecb.SelectedItem.ToString()
            If dev = "Desktop" OrElse dev = "Laptop" Then
                deviceFilter = "Convert([Desktop or Laptop], 'System.String') = '" &
                               dev.Replace("'", "''") & "'"
                filters.Add(deviceFilter)
            End If
        End If

        ' 2) Text search filter
        Dim search As String = filtertxt.Text.Trim()
        If search <> "" Then
            search = search.Replace("'", "''")
            Dim textFilter As String =
                "Convert([NSOC REGISTERED NAME], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([BrandModelFeature], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([Desktop or Laptop], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([PROPERTY NO.], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([PERSONNEL ASSIGNED], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([POSITION OF PERSONNEL / USAGE OF UNIT], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([DATE ACQUIRED], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([DATE ISSUED], 'System.String') LIKE '%" & search & "%' OR " &
                "Convert([INCLUDED], 'System.String') LIKE '%" & search & "%'"

            filters.Add("(" & textFilter & ")")
        End If

        If filters.Count = 0 Then
            allUnitsView.RowFilter = ""
        Else
            allUnitsView.RowFilter = String.Join(" AND ", filters)
        End If
    End Sub

    ' 🔍 Search textbox
    Private Sub filtertext_TextChanged(sender As Object, e As EventArgs) Handles filtertxt.TextChanged
        ApplyFilters()
    End Sub

    ' 🖱 Device combo (All / Desktop / Laptop)
    Private Sub devicecb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles devicecb.SelectedIndexChanged
        ApplyFilters()
    End Sub

    ' 📤 Export visible rows to CSV
    Private Sub exportbtn_Click(sender As Object, e As EventArgs) Handles exportbtn.Click
        If allunitsdgv.Rows.Count = 0 Then
            MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim sfd As New SaveFileDialog With {
            .Filter = "CSV File (*.csv)|*.csv",
            .FileName = "Units_Export.csv",
            .Title = "Save CSV File"
        }

        If sfd.ShowDialog() <> DialogResult.OK Then Return

        Try
            Using sw As New StreamWriter(sfd.FileName, False, Encoding.UTF8)
                Dim headerValues As New List(Of String)
                For Each col As DataGridViewColumn In allunitsdgv.Columns
                    headerValues.Add("""" & col.HeaderText.Replace("""", """""") & """")
                Next
                sw.WriteLine(String.Join(",", headerValues))

                For Each row As DataGridViewRow In allunitsdgv.Rows
                    If row.IsNewRow OrElse Not row.Visible Then Continue For

                    Dim cellValues As New List(Of String)
                    For Each cell As DataGridViewCell In row.Cells
                        Dim cellText As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                        cellText = cellText.Replace("""", """""")
                        cellValues.Add("""" & cellText & """")
                    Next
                    sw.WriteLine(String.Join(",", cellValues))
                Next
            End Using

            MessageBox.Show("Export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error exporting: " & ex.Message,
                            "Export Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ========= Open ViewInclude when clicking the INCLUDE button =========
    Private Sub allunitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles allunitsdgv.CellContentClick

        If e.RowIndex < 0 Then Return
        If allunitsdgv.Columns(e.ColumnIndex).Name <> "IncludeButton" Then Return

        Dim row = allunitsdgv.Rows(e.RowIndex)

        ' 1) DEVICE TYPE (Desktop / Laptop)
        Dim deviceType As String = ""
        If allunitsdgv.Columns.Contains("Desktop or Laptop") Then
            Dim cell = row.Cells("Desktop or Laptop")
            If cell.Value IsNot Nothing AndAlso cell.Value IsNot DBNull.Value Then
                deviceType = cell.Value.ToString()
            End If
        End If

        ' 2) SPECS summary (you can add/remove fields here)
        Dim specsSb As New StringBuilder()

        If allunitsdgv.Columns.Contains("NSOC REGISTERED NAME") Then
            Dim v = row.Cells("NSOC REGISTERED NAME").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("NSOC REGISTERED NAME: " & v.ToString())
            End If
        End If

        If allunitsdgv.Columns.Contains("BrandModelFeature") Then
            Dim v = row.Cells("BrandModelFeature").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("BRAND/MODEL/FEATURE: " & v.ToString())
            End If
        End If

        If allunitsdgv.Columns.Contains("PROPERTY NO.") Then
            Dim v = row.Cells("PROPERTY NO.").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("PROPERTY NO.: " & v.ToString())
            End If
        End If

        If allunitsdgv.Columns.Contains("POSITION OF PERSONNEL / USAGE OF UNIT") Then
            Dim v = row.Cells("POSITION OF PERSONNEL / USAGE OF UNIT").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("USAGE/POSITION: " & v.ToString())
            End If
        End If

        If allunitsdgv.Columns.Contains("DATE ACQUIRED") Then
            Dim v = row.Cells("DATE ACQUIRED").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("DATE ACQUIRED: " & v.ToString())
            End If
        End If

        If allunitsdgv.Columns.Contains("DATE ISSUED") Then
            Dim v = row.Cells("DATE ISSUED").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value AndAlso v.ToString().Trim() <> "" Then
                specsSb.AppendLine("DATE ISSUED: " & v.ToString())
            End If
        End If

        Dim specsText As String = specsSb.ToString().Trim()

        ' 3) INCLUDED text
        Dim includeText As String = ""
        If allunitsdgv.Columns.Contains("INCLUDED") Then
            Dim incCell = row.Cells("INCLUDED")
            If incCell IsNot Nothing AndAlso incCell.Value IsNot Nothing AndAlso incCell.Value IsNot DBNull.Value Then
                includeText = incCell.Value.ToString()
            End If
        End If

        ' Open ViewInclude with everything
        Dim uc As New ViewInclude()
        uc.Dock = DockStyle.Fill
        uc.LoadDetails(deviceType, specsText, includeText)

        Dim frm As New Form()
        frm.Text = "Device Details"
        frm.StartPosition = FormStartPosition.CenterParent
        frm.Size = New Size(600, 450)
        frm.Controls.Add(uc)

        frm.ShowDialog(Me)
    End Sub



End Class
