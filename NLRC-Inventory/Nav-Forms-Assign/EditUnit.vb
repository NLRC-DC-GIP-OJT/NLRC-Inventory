Imports System.Data
Imports System.Text

Public Class EditUnit
    Private mdl As New model()
    Private unitId As Integer
    Private originalAssignList As DataTable
    Private isFiltering As Boolean = False

    ' Track original values
    Private originalUnitName As String
    Private originalAssignedId As Integer

    ' Track device changes
    Private currentDevices As New List(Of Integer)
    Private removedDevices As New List(Of Integer)
    Private addedDevices As New List(Of Integer)

    ' Track edited specs: DeviceID -> (SpecName -> NewValue)
    Private editedSpecs As New Dictionary(Of Integer, Dictionary(Of String, String))()

    ' Specs Table
    Private specsTable As New TableLayoutPanel With {
        .AutoSize = True,
        .ColumnCount = 2,
        .RowCount = 0,
        .Dock = DockStyle.Fill,
        .CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
        .Padding = New Padding(10),
        .Margin = New Padding(0)
    }

    ' ---------------------------
    ' Load Unit into form
    ' ---------------------------
    Public Sub LoadUnit(unitId As Integer, unitName As String, assignedToId As Integer, devices As DataTable)
        Me.unitId = unitId
        Me.originalUnitName = unitName
        Me.originalAssignedId = assignedToId

        LoadAssignments()
        unitnametxt.Text = unitName

        Dim userRow = originalAssignList.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of Integer)("user_id") = assignedToId)
        If userRow IsNot Nothing Then assigntxt.Text = userRow.Field(Of String)("Full name")

        deviceflowpnl.Controls.Clear()
        specsflowpnl.Controls.Clear()
        specsTable.Controls.Clear()
        specsTable.RowCount = 0

        currentDevices.Clear() : removedDevices.Clear() : addedDevices.Clear() : editedSpecs.Clear()

        For Each row As DataRow In devices.Rows
            Dim btnText As String = row("DeviceAndSpecs").ToString()
            Dim deviceId As Integer = CInt(row("DeviceID"))
            currentDevices.Add(deviceId)

            Dim container As New Panel With {.Height = 45, .Width = 380, .Margin = New Padding(5)}
            Dim btn As New Button With {.Text = btnText, .Tag = deviceId, .Width = 270, .Height = 40, .BackColor = Color.LightGray, .TextAlign = ContentAlignment.MiddleLeft, .AutoEllipsis = True, .Anchor = AnchorStyles.Left}
            Dim removeBtn As New Button With {.Text = "Remove", .Width = 90, .Height = 40, .BackColor = Color.LightCoral, .ForeColor = Color.White, .Font = New Font("Arial", 10, FontStyle.Bold), .Anchor = AnchorStyles.Right}

            ' Remove device
            AddHandler removeBtn.Click, Sub()
                                            If MessageBox.Show("Remove device: " & btnText & "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                                                deviceflowpnl.Controls.Remove(container)
                                                specsTable.Controls.Clear()
                                                specsTable.RowCount = 0
                                                removedDevices.Add(deviceId)
                                                currentDevices.Remove(deviceId)
                                                If editedSpecs.ContainsKey(deviceId) Then editedSpecs.Remove(deviceId)
                                            End If
                                        End Sub

            ' Show specs
            AddHandler btn.Click, Sub()
                                      specsTable.Controls.Clear()
                                      specsTable.RowCount = 0
                                      Dim specs As String = row("DeviceAndSpecs").ToString()
                                      Dim specFields As String() = specs.Split(","c)
                                      Dim deviceSpecDict As New Dictionary(Of String, String)()

                                      For Each spec As String In specFields
                                          Dim parts = spec.Trim().Split(":"c)
                                          If parts.Length = 2 Then
                                              Dim lbl As New Label With {.Text = parts(0).Trim() & ":", .Width = 100, .TextAlign = ContentAlignment.MiddleLeft}
                                              Dim txt As New TextBox With {.Text = parts(1).Trim(), .Dock = DockStyle.Fill}
                                              specsTable.RowCount += 1
                                              specsTable.Controls.Add(lbl, 0, specsTable.RowCount - 1)
                                              specsTable.Controls.Add(txt, 1, specsTable.RowCount - 1)

                                              ' Track in memory
                                              deviceSpecDict(parts(0).Trim()) = parts(1).Trim()
                                          End If
                                      Next

                                      ' Save to editedSpecs dictionary
                                      editedSpecs(deviceId) = deviceSpecDict
                                  End Sub

            container.Controls.Add(btn)
            container.Controls.Add(removeBtn)
            deviceflowpnl.Controls.Add(container)
        Next

        specsflowpnl.Controls.Add(specsTable)
    End Sub

    ' ---------------------------
    ' Load assignments
    ' ---------------------------
    Private Sub LoadAssignments()
        originalAssignList = mdl.GetAssignments()
        If originalAssignList Is Nothing OrElse originalAssignList.Rows.Count = 0 Then Return

        assigncb.DataSource = originalAssignList.Copy()
        assigncb.DisplayMember = "Full name"
        assigncb.ValueMember = "user_id"
        assigncb.SelectedIndex = -1
        assigncb.Text = "Select Personnel"

        AddHandler assigncb.TextChanged, AddressOf FilterAssignCombo
    End Sub

    Private Sub assigncb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles assigncb.SelectedIndexChanged
        Dim drv = TryCast(assigncb.SelectedItem, DataRowView)
        If drv IsNot Nothing Then assigntxt.Text = drv("Full name").ToString()
    End Sub

    Private Sub FilterAssignCombo(sender As Object, e As EventArgs)
        SafeComboFilter(DirectCast(sender, ComboBox), originalAssignList, "Full name", "user_id", AddressOf FilterAssignCombo)
    End Sub

    Private Sub SafeComboFilter(cb As ComboBox, source As DataTable, displayCol As String, valueCol As String, handler As EventHandler)
        If isFiltering Then Exit Sub
        isFiltering = True
        Try
            If Not cb.Focused OrElse source Is Nothing OrElse source.Rows.Count = 0 Then Exit Sub
            Dim searchText = cb.Text.Trim().ToLower()
            Dim filtered As DataTable
            If String.IsNullOrWhiteSpace(searchText) Then
                filtered = source.Copy()
            Else
                Dim rows = source.AsEnumerable().Where(Function(r) r.Field(Of String)(displayCol).ToLower().Contains(searchText))
                filtered = If(rows.Any(), rows.CopyToDataTable(), Nothing)
            End If

            Dim currentText = cb.Text, selStart = cb.SelectionStart
            RemoveHandler cb.TextChanged, handler
            cb.BeginUpdate()
            If filtered IsNot Nothing Then
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
            cb.Text = currentText
            cb.SelectionStart = Math.Min(selStart, cb.Text.Length)
            cb.SelectionLength = 0
            AddHandler cb.TextChanged, handler
        Finally
            isFiltering = False
        End Try
    End Sub

    ' ---------------------------
    ' Save specs button click
    ' ---------------------------
    Private Sub savespecsbtn_Click(sender As Object, e As EventArgs) Handles savespecsbtn.Click
        MessageBox.Show("Specs saved in memory. Toggle other devices or click Save to commit to database.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ---------------------------
    ' Save button click (commits to database)
    ' ---------------------------
    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Try
            Dim newUnitName = unitnametxt.Text.Trim()
            Dim newAssignedId As Object = If(assigncb.SelectedValue, DBNull.Value)

            ' Build friendly summary
            Dim sb As New StringBuilder()
            If newUnitName <> originalUnitName Then sb.AppendLine($"Unit Name: '{originalUnitName}' → '{newUnitName}'")
            If newAssignedId IsNot Nothing AndAlso newAssignedId <> originalAssignedId Then sb.AppendLine($"Assigned Personnel: '{assigntxt.Text}'")
            If removedDevices.Any() Then sb.AppendLine("Removed Devices: " & String.Join(", ", removedDevices))
            If addedDevices.Any() Then sb.AppendLine("Added Devices: " & String.Join(", ", addedDevices))
            If editedSpecs.Any() Then
                sb.AppendLine("Edited Specs:")
                For Each kvp In editedSpecs
                    sb.AppendLine($"Device {kvp.Key}:")
                    For Each specKvp In kvp.Value
                        sb.AppendLine($"   {specKvp.Key}: {specKvp.Value}")
                    Next
                Next
            End If

            If MessageBox.Show("You are about to save the following changes:" & vbCrLf & vbCrLf &
                               sb.ToString() & vbCrLf & "Proceed?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            ' Commit to database
            mdl.SaveUnitChanges(unitId, newAssignedId, removedDevices, addedDevices, editedSpecs, newUnitName)

            MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Reset tracking
            originalUnitName = newUnitName
            originalAssignedId = If(newAssignedId, 0)
            removedDevices.Clear()
            addedDevices.Clear()
            editedSpecs.Clear()
        Catch ex As Exception
            MessageBox.Show("Error saving changes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ---------------------------
    ' Close panel
    ' ---------------------------
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel Is Nothing Then parentPanel = TryCast(Me.Parent?.Parent, Panel)
        If parentPanel IsNot Nothing Then parentPanel.Visible = False
    End Sub
End Class
