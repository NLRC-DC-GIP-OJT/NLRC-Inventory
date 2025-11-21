Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports System.Drawing
Imports MySql.Data.MySqlClient

Public Class AddUnits
    Private mdl As New model()
    Private originalDeviceList As DataTable
    Private originalAssignList As DataTable
    Private isFiltering As Boolean = False
    Public Property UnitsSaved As Boolean
    Public Event UnitSaved()

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
    ' ========================
    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        If Me.DesignMode Then Return   ' avoid running in designer

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

            ' Optional: scale fonts
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

    ' =========================
    ' LOAD
    ' =========================
    Private Sub AddUnits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Fill parent container
        Me.Dock = DockStyle.Fill

        ' init auto-resize based on designed layout
        InitializeLayoutScaling()

        LoadAssignments()
        LoadDevices()

        AddHandler devicecb.TextChanged, AddressOf FilterDeviceCombo
        AddHandler assigncb.TextChanged, AddressOf FilterAssignCombo

        unitsdgv.EnableHeadersVisualStyles = False
        unitsdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        unitsdgv.AllowUserToAddRows = False
    End Sub

    ' =========================
    ' Unit1: Assignments
    ' =========================
    Private Sub LoadAssignments()
        originalAssignList = mdl.GetAssignments()

        If originalAssignList Is Nothing OrElse originalAssignList.Rows.Count = 0 Then
            MessageBox.Show("No users found in the database.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        assigncb.DataSource = originalAssignList.Copy()
        assigncb.DisplayMember = "Full name"
        assigncb.ValueMember = "user_id"
        assigncb.SelectedIndex = -1
        assigncb.DropDownStyle = ComboBoxStyle.DropDown
        assigncb.IntegralHeight = False
        assigncb.DropDownHeight = 350
    End Sub

    ' =========================
    ' Unit1: Devices
    ' =========================
    Private Sub LoadDevices()
        ' Get devices from the database with Category-Brand-Model
        originalDeviceList = mdl.GetDevicesForUnits()

        If originalDeviceList Is Nothing OrElse originalDeviceList.Rows.Count = 0 Then
            MessageBox.Show("No devices found in the database.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Only working devices
        Dim workingDevices As DataTable
        Try
            workingDevices = originalDeviceList.AsEnumerable() _
                .Where(Function(r) r.Field(Of String)("status").Trim().ToLower() = "working") _
                .CopyToDataTable()
        Catch ex As InvalidOperationException
            ' No working devices
            workingDevices = originalDeviceList.Clone()
        End Try

        ' Remove duplicates based on Category-Brand-Model (Device column)
        Dim distinctDevices As DataTable
        Try
            distinctDevices = workingDevices.AsEnumerable() _
                .GroupBy(Function(r) r.Field(Of String)("Device")) _
                .Select(Function(g) g.First()) _
                .CopyToDataTable()
        Catch ex As InvalidOperationException
            ' No distinct devices
            distinctDevices = workingDevices.Clone()
        End Try

        originalDeviceList = distinctDevices

        ' Bind to combo box
        devicecb.DataSource = originalDeviceList.Copy()
        devicecb.DisplayMember = "Device"
        devicecb.ValueMember = "device_id"
        devicecb.SelectedIndex = -1
        devicecb.DropDownStyle = ComboBoxStyle.DropDown
        devicecb.IntegralHeight = False
        devicecb.DropDownHeight = 350
    End Sub

    ' =========================
    ' Filter handlers
    ' =========================
    Private Sub FilterDeviceCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalDeviceList, "Device", "device_id", AddressOf FilterDeviceCombo)
    End Sub

    Private Sub FilterAssignCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalAssignList, "Full name", "user_id", AddressOf FilterAssignCombo)
    End Sub

    Private Sub SafeComboFilter(cb As ComboBox, source As DataTable, displayCol As String, valueCol As String, handler As EventHandler)
        If isFiltering Then Exit Sub
        isFiltering = True

        Try
            If cb Is Nothing OrElse Not cb.Focused OrElse source Is Nothing OrElse source.Rows.Count = 0 Then Exit Sub

            Dim searchText As String = cb.Text.Trim().ToLower()
            Dim filtered As DataTable

            If String.IsNullOrWhiteSpace(searchText) Then
                filtered = source.Copy()
            Else
                ' Filter safely, skip DBNull values
                Dim rows = source.AsEnumerable().Where(Function(r)
                                                           Dim valObj = r(displayCol)
                                                           If valObj Is DBNull.Value OrElse valObj Is Nothing Then Return False
                                                           Return valObj.ToString().ToLower().Contains(searchText)
                                                       End Function)

                filtered = If(rows.Any(), rows.CopyToDataTable(), Nothing)
            End If

            Dim currentText As String = cb.Text
            Dim selStart As Integer = cb.SelectionStart

            RemoveHandler cb.TextChanged, handler
            cb.BeginUpdate()

            If filtered IsNot Nothing Then
                If Not filtered.Columns.Contains(valueCol) Then
                    filtered.Columns.Add(valueCol, GetType(Integer))
                End If

                cb.DataSource = filtered
            Else
                Dim noResult As New DataTable()
                noResult.Columns.Add(displayCol, GetType(String))
                noResult.Columns.Add(valueCol, GetType(Integer))
                noResult.Rows.Add("No results found", -1)
                cb.DataSource = noResult
            End If

            cb.DisplayMember = displayCol
            cb.ValueMember = valueCol
            cb.EndUpdate()

            cb.DroppedDown = True
            Cursor.Current = Cursors.IBeam
            cb.Text = currentText
            cb.SelectionStart = Math.Min(selStart, cb.Text.Length)
            cb.SelectionLength = 0

            Application.DoEvents()
            cb.DroppedDown = True

            AddHandler cb.TextChanged, handler
        Finally
            isFiltering = False
        End Try
    End Sub


    ' =========================
    ' Clear fields
    ' =========================
    Private Sub ClearFields()
        remarktxt.Clear()
        devicecb.SelectedIndex = -1
    End Sub

    ' =========================
    ' Validate Unit Name
    ' =========================
    Private Sub unitnametxt_Leave(sender As Object, e As EventArgs) Handles unitnametxt.Leave
        Try
            Dim unitName As String = unitnametxt.Text.Trim()
            If String.IsNullOrWhiteSpace(unitName) Then Exit Sub

            If mdl.IsUnitNameExists(unitName) Then
                MessageBox.Show($"The unit name '{unitName}' already exists in the database.",
                                "Duplicate Unit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                unitnametxt.Focus()
                unitnametxt.SelectAll()
            End If
        Catch ex As Exception
            MessageBox.Show("Error checking unit name: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' =========================
    ' Add button unit1pnl
    ' =========================
    Private Sub addbtn_Click(sender As Object, e As EventArgs) Handles addbtn.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(unitnametxt.Text) OrElse assigncb.SelectedIndex = -1 OrElse devicecb.SelectedIndex = -1 Then
            MessageBox.Show("Please fill out all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Add columns to DataGridView if not already present AND remove button
        If unitsdgv.Columns.Count = 0 Then
            unitsdgv.Columns.Add("unit_name", "Unit Name")
            unitsdgv.Columns.Add("assigned_to", "Assigned To")
            unitsdgv.Columns.Add("device", "Device")
            unitsdgv.Columns.Add("remarks", "Remarks")
            unitsdgv.Columns.Add("device_id", "Device ID")
            unitsdgv.Columns("device_id").Visible = False

            ' Add Remove button column
            Dim removeBtn As New DataGridViewButtonColumn()
            removeBtn.HeaderText = "Remove"
            removeBtn.Name = "remove"
            removeBtn.Text = "Remove"
            removeBtn.UseColumnTextForButtonValue = True
            unitsdgv.Columns.Add(removeBtn)
        End If

        ' Get selected device details
        Dim selectedDeviceName As String = devicecb.Text
        Dim selectedDeviceId As Integer = CInt(devicecb.SelectedValue)

        ' Add new row to DataGridView
        unitsdgv.Rows.Add(unitnametxt.Text.Trim(), assigncb.Text, selectedDeviceName, remarktxt.Text.Trim(), selectedDeviceId)

        ' Remove the selected device from originalDeviceList
        Dim rowToRemove = originalDeviceList.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Integer)("device_id") = selectedDeviceId)
        If rowToRemove IsNot Nothing Then
            originalDeviceList.Rows.Remove(rowToRemove)
        End If

        ' Update DataSource of device combo box
        devicecb.DataSource = originalDeviceList.Copy()
        devicecb.DisplayMember = "Device"
        devicecb.ValueMember = "device_id"
        devicecb.SelectedIndex = -1

        ' Clear form fields after addition
        ClearFields()

        ' Scroll to the new row in the DataGridView and select it
        unitsdgv.FirstDisplayedScrollingRowIndex = unitsdgv.RowCount - 1
        unitsdgv.Rows(unitsdgv.RowCount - 1).Selected = True
    End Sub

    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        If unitsdgv.Rows.Count = 0 Then
            MessageBox.Show("No devices to save.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim unitName As String = Nothing
        Dim assignedPersonnel As Integer? = Nothing
        Dim devicePointers As New List(Of Integer)
        Dim remarksList As New List(Of String)

        ' Iterate over each row in DataGridView
        For Each row As DataGridViewRow In unitsdgv.Rows
            If row.IsNewRow Then Continue For

            ' Capture unit name (take from first row)
            If unitName Is Nothing AndAlso unitsdgv.Columns.Contains("unit_name") Then
                unitName = If(row.Cells("unit_name").Value?.ToString(), Nothing)
            End If

            ' Capture assigned personnel (first row with value)
            If assignedPersonnel Is Nothing AndAlso unitsdgv.Columns.Contains("assigned_to") Then
                If row.Cells("assigned_to") IsNot Nothing AndAlso row.Cells("assigned_to").Value IsNot Nothing Then
                    Dim fullName = row.Cells("assigned_to").Value.ToString()
                    Dim userRow = mdl.GetAssignments().AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("Full name") = fullName)
                    If userRow IsNot Nothing Then
                        assignedPersonnel = userRow.Field(Of Integer)("user_id")
                    End If
                End If
            End If

            ' Get quantity for this row
            Dim quantity As Integer = 1
            If unitsdgv.Columns.Contains("quantity") AndAlso row.Cells("quantity") IsNot Nothing AndAlso row.Cells("quantity").Value IsNot Nothing Then
                Integer.TryParse(row.Cells("quantity").Value.ToString(), quantity)
            End If

            ' Add device pointer multiple times based on quantity
            If unitsdgv.Columns.Contains("device_id") AndAlso row.Cells("device_id") IsNot Nothing AndAlso row.Cells("device_id").Value IsNot Nothing Then
                Dim deviceId As Integer = Convert.ToInt32(row.Cells("device_id").Value)
                For i As Integer = 1 To quantity
                    devicePointers.Add(deviceId)
                Next
            End If

            ' Capture remarks
            If unitsdgv.Columns.Contains("remarks") AndAlso row.Cells("remarks") IsNot Nothing AndAlso row.Cells("remarks").Value IsNot Nothing Then
                remarksList.Add(row.Cells("remarks").Value.ToString())
            End If
        Next

        ' Combine remarks
        Dim allRemarks As String = String.Join("; ", remarksList)

        ' Call SaveUnit
        Dim success As Boolean = mdl.SaveUnit(unitName, assignedPersonnel, devicePointers, allRemarks)

        If success Then
            MessageBox.Show("Unit saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            unitsdgv.Rows.Clear()
            unitnametxt.Clear()

            LoadDevices() ' Refresh device list
        Else
            MessageBox.Show("Failed to save unit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel Is Nothing Then parentPanel = TryCast(Me.Parent?.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub

    Private Sub unitsdgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles unitsdgv.CellContentClick
        ' Check if Remove button column was clicked
        If e.ColumnIndex >= 0 AndAlso unitsdgv.Columns(e.ColumnIndex).Name = "remove" AndAlso e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = unitsdgv.Rows(e.RowIndex)
            Dim unitName As String = row.Cells("unit_name").Value?.ToString()

            Dim result As DialogResult = MessageBox.Show(
                $"Are you sure you want to remove the unit '{unitName}'?",
                "Confirm Remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            )

            If result = DialogResult.Yes Then
                ' Optional: put device back into device combo list
                If row.Cells("device_id").Value IsNot Nothing Then
                    Dim deviceId As Integer = Convert.ToInt32(row.Cells("device_id").Value)
                    Dim deviceRow = mdl.GetDevicesForUnits().AsEnumerable() _
                                .FirstOrDefault(Function(r) r.Field(Of Integer)("device_id") = deviceId)
                    If deviceRow IsNot Nothing Then
                        originalDeviceList.Rows.Add(deviceRow.ItemArray)
                        devicecb.DataSource = originalDeviceList.Copy()
                        devicecb.DisplayMember = "Device"
                        devicecb.ValueMember = "device_id"
                    End If
                End If

                ' Remove the row from DataGridView
                unitsdgv.Rows.RemoveAt(e.RowIndex)

                ' Clear the device combo box selection
                devicecb.SelectedIndex = -1
                devicecb.Text = ""
            End If
        End If
    End Sub


End Class
