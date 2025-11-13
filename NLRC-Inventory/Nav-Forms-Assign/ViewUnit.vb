Imports System.Text
Imports MySql.Data.MySqlClient

Public Class ViewUnit

    Private model As New model()

    ' LoadUnit will populate the user control with unit info and devices
    Public Sub LoadUnit(unitName As String, assignedToId As Integer, devices As DataTable)
        ' Set the unit name and assigned person
        unitnametxt.Text = unitName
        assigntxt.Text = GetAssignedName(assignedToId)

        ' Clear existing UI
        deviceflowpnl.Controls.Clear()
        specsflowpnl.Controls.Clear()

        ' Initialize specs table
        Dim specsTable As New TableLayoutPanel With {
            .AutoSize = True,
            .ColumnCount = 2,
            .RowCount = 0,
            .Dock = DockStyle.Fill,
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            .Padding = New Padding(10),
            .Margin = New Padding(0)
        }

        specsTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30))
        specsTable.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 70))

        ' Configure flow panel
        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True
        deviceflowpnl.Dock = DockStyle.Fill

        ' Loop through each device
        For Each row As DataRow In devices.Rows
            Dim btnText As String = row("DeviceAndSpecs").ToString()
            Dim deviceId As Integer = CInt(row("DeviceID"))

            ' Container
            Dim container As New Panel With {
                .Height = 45,
                .Width = 380,
                .Margin = New Padding(5)
            }

            ' Device button
            Dim btn As New Button With {
                .Text = btnText,
                .Tag = deviceId,
                .Dock = DockStyle.Fill,
                .Height = 40,
                .BackColor = Color.LightGray,
                .TextAlign = ContentAlignment.MiddleLeft,
                .AutoEllipsis = True
            }

            ' On click: show specs
            AddHandler btn.Click,
                Sub(sender As Object, e As EventArgs)
                    specsTable.Controls.Clear()
                    specsTable.RowCount = 0

                    Dim specs As String = row("DeviceAndSpecs").ToString()
                    Dim specFields As String() = specs.Split(","c)

                    For Each spec As String In specFields
                        Dim parts As String() = spec.Trim().Split(":"c)

                        If parts.Length = 2 Then
                            ' =============================
                            ' FIX: Extract only last name
                            ' =============================
                            Dim rawLabel As String = parts(0).Trim()
                            Dim labelParts = rawLabel.Split("-"c)
                            Dim cleanLabel As String = labelParts.Last().Trim()

                            Dim lbl As New Label With {
                                .Text = cleanLabel & ":",
                                .AutoSize = False,
                                .Width = 100,
                                .TextAlign = ContentAlignment.MiddleLeft,
                                .Margin = New Padding(0, 3, 0, 3)
                            }

                            ' Create a non-clickable "textbox-like" label
                            Dim valueLbl As New Label With {
                                .Text = parts(1).Trim(),
                                .AutoSize = False,
                                .Dock = DockStyle.Fill,
                                .TextAlign = ContentAlignment.MiddleLeft,
                                .BackColor = Color.White,
                                .BorderStyle = BorderStyle.FixedSingle,
                                .Margin = New Padding(0, 3, 0, 3)
                            }

                            specsTable.RowCount += 1
                            specsTable.Controls.Add(lbl, 0, specsTable.RowCount - 1)
                            specsTable.Controls.Add(valueLbl, 1, specsTable.RowCount - 1)
                        End If
                    Next
                End Sub

            ' Add to UI
            container.Controls.Add(btn)
            deviceflowpnl.Controls.Add(container)
        Next

        ' Add specs table to panel
        specsflowpnl.Controls.Add(specsTable)

        ' Refresh layout
        deviceflowpnl.PerformLayout()
    End Sub

    ' Get assigned person name
    Private Function GetAssignedName(assignedToId As Integer) As String
        Dim assignments As DataTable = model.GetAssignments()
        For Each row As DataRow In assignments.Rows
            If CInt(row("user_id")) = assignedToId Then
                Return row("Full name").ToString()
            End If
        Next
        Return ""
    End Function

    ' Close panel when Panel2 is clicked
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Dim parentPanel As Panel = TryCast(Me.Parent, Panel)
        If parentPanel Is Nothing Then
            parentPanel = TryCast(Me.Parent?.Parent, Panel)
        End If
        If parentPanel IsNot Nothing Then
            parentPanel.Visible = False
        End If
    End Sub

End Class
