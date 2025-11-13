Imports System.Text
Imports MySql.Data.MySqlClient

Public Class EditUnit

    ' Create an instance of the model class to access the database methods
    Private model As New model()

    ' LoadUnit will populate the user control with unit info and devices
    Public Sub LoadUnit(unitName As String, assignedToId As Integer, devices As DataTable)
        ' Set the unit name in the textbox
        unitnametxt.Text = unitName

        ' Get the assigned person’s name
        Dim assignedName As String = GetAssignedName(assignedToId)
        assigntxt.Text = assignedName

        ' Clear previous controls
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

        ' Configure device flow panel
        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.WrapContents = False
        deviceflowpnl.AutoScroll = True
        deviceflowpnl.Dock = DockStyle.Fill

        ' Add each device row
        For Each row As DataRow In devices.Rows
            Dim btnText As String = row("DeviceAndSpecs").ToString()
            Dim deviceId As Integer = CInt(row("DeviceID"))

            ' Container for each device row
            Dim container As New Panel With {
                .Height = 45,
                .Width = 380, ' FIXED fallback width
                .Margin = New Padding(5)
            }

            ' Device button
            Dim btn As New Button With {
                .Text = btnText,
                .Tag = deviceId,
                .Width = 270,
                .Height = 40,
                .BackColor = Color.LightGray,
                .TextAlign = ContentAlignment.MiddleLeft,
                .AutoEllipsis = True,
                .Anchor = AnchorStyles.Left
            }

            ' Remove button
            Dim removeBtn As New Button With {
                .Text = "Remove",
                .Width = 90,
                .Height = 40,
                .BackColor = Color.LightCoral,
                .ForeColor = Color.White,
                .Font = New Font("Arial", 10, FontStyle.Bold),
                .Anchor = AnchorStyles.Right
            }

            ' Position buttons
            btn.Location = New Point(0, 0)
            removeBtn.Location = New Point(btn.Right + 10, 0)

            ' Remove handler
            AddHandler removeBtn.Click, Sub(s, e)
                                            Dim confirmResult As DialogResult = MessageBox.Show(
                                                "Are you sure you want to remove the device: " & btnText & "?",
                                                "Confirm Removal",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning)
                                            If confirmResult = DialogResult.Yes Then
                                                deviceflowpnl.Controls.Remove(container)
                                                specsflowpnl.Controls.Clear()
                                            End If
                                        End Sub

            ' Device button click handler
            AddHandler btn.Click, Sub(sender As Object, e As EventArgs)
                                      specsTable.Controls.Clear()
                                      specsTable.RowCount = 0
                                      Dim specs As String = row("DeviceAndSpecs").ToString()
                                      Dim specFields As String() = specs.Split(";"c)
                                      For Each spec As String In specFields
                                          Dim parts As String() = spec.Trim().Split(":"c)
                                          If parts.Length = 2 Then
                                              Dim lbl As New Label With {
                                                  .Text = parts(0).Trim() & ":",
                                                  .AutoSize = False,
                                                  .Width = 100,
                                                  .TextAlign = ContentAlignment.MiddleLeft
                                              }
                                              Dim txt As New TextBox With {
                                                  .Text = parts(1).Trim(),
                                                  .Dock = DockStyle.Fill
                                              }
                                              specsTable.RowCount += 1
                                              specsTable.Controls.Add(lbl, 0, specsTable.RowCount - 1)
                                              specsTable.Controls.Add(txt, 1, specsTable.RowCount - 1)
                                          End If
                                      Next
                                  End Sub

            ' Add to container
            container.Controls.Add(btn)
            container.Controls.Add(removeBtn)

            ' Handle resize dynamically (to fit parent width)
            AddHandler container.Resize, Sub()
                                             Dim paddingRight As Integer = 10
                                             btn.Width = container.Width - removeBtn.Width - paddingRight
                                             removeBtn.Location = New Point(container.Width - removeBtn.Width, 0)
                                         End Sub

            ' Add container to flow panel
            deviceflowpnl.Controls.Add(container)
        Next

        ' Add specs table to specs panel
        specsflowpnl.Controls.Add(specsTable)

        ' Refresh layout after all controls added
        deviceflowpnl.PerformLayout()
    End Sub

    ' Get the assigned person’s name by ID
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
