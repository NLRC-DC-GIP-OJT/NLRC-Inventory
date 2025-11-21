Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Public Class ViewUnit

    Private model As New model()

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    ' keep resize handler for deviceflowpnl from being added multiple times
    Private deviceFlowResizeHooked As Boolean = False

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

            ' Optional font scaling
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

    ' ========================
    ' Load (init + auto-resize hook)
    ' ========================
    Private Sub ViewUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Fill the parent panel when used inside a container
        Me.Dock = DockStyle.Fill

        ' Initialize scaling based on the designed layout
        InitializeLayoutScaling()
    End Sub

    ' ========================
    ' PUBLIC API
    ' ========================

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

        ' Ensure we only hook the resize event once
        If Not deviceFlowResizeHooked Then
            AddHandler deviceflowpnl.Resize, AddressOf DeviceFlow_Resize
            deviceFlowResizeHooked = True
        End If

        ' Loop through each device
        For Each row As DataRow In devices.Rows
            Dim btnText As String = row("DeviceAndSpecs").ToString()
            Dim deviceId As Integer = CInt(row("DeviceID"))

            ' Container – width based on current flow panel width
            Dim container As New Panel With {
                .Height = 45,
                .Width = Math.Max(200, deviceflowpnl.ClientSize.Width - 15),
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
                    Dim specFields As String() = specs.Split(";"c)

                    For Each spec As String In specFields
                        Dim parts As String() = spec.Trim().Split(":"c)

                        If parts.Length = 2 Then
                            ' Extract only last name after any dash
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

                            ' Non-clickable "textbox-like" label
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

    ' Resize handler so all device panels track the flow panel width
    Private Sub DeviceFlow_Resize(sender As Object, e As EventArgs)
        For Each ctl As Control In deviceflowpnl.Controls
            ctl.Width = Math.Max(200, deviceflowpnl.ClientSize.Width - 15)
        Next
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

    Private Sub nsocbtn_Click(sender As Object, e As EventArgs)

    End Sub
End Class
