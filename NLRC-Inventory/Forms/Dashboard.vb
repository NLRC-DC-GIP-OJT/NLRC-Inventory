Imports System.Collections.Generic
Imports System.Drawing

Public Class Dashboard

    Private LoggedUser As String
    Private model As New model() ' Your Model class

    ' ========================
    ' 🔁 AUTO-RESIZE FIELDS
    ' ========================
    Private originalSize As Size
    Private originalBounds As New Dictionary(Of Control, Rectangle)
    Private layoutInitialized As Boolean = False

    ' ========================
    ' Constructors
    ' ========================
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(user As String)
        InitializeComponent()
        LoggedUser = user
    End Sub

    ' ========================
    ' Dashboard Load
    ' ========================
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        userlbl.Text = LoggedUser

        InitializeLayoutScaling()
        LoadDashboard()

        ' highlight Dashboard by default
        SetActiveButton(dashbtn)

        Timer1.Interval = 1000
        Timer1.Start()
    End Sub


    ' ========================
    ' 🔁 AUTO-RESIZE SUPPORT
    ' ========================
    Private Sub InitializeLayoutScaling()
        If layoutInitialized Then Return
        If Me.DesignMode Then Return ' avoid running in designer

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

    ' ========================
    ' Generic method to load any UserControl
    ' ========================
    Private Sub LoadUserControl(ctrl As UserControl)
        mainpnl.Controls.Clear()
        ctrl.Dock = DockStyle.Fill
        mainpnl.Controls.Add(ctrl)
    End Sub

    ' ========================
    ' Load DashboardControl by default
    ' ========================
    Private Sub LoadDashboard()
        LoadUserControl(New DashboardControl())
    End Sub

    ' ========================
    ' Timer Tick for Clock
    ' ========================
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        dntlbl.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt")
    End Sub

    ' ========================
    ' Button Clicks to load UserControls
    ' ========================
    Private Sub dashbtn_Click(sender As Object, e As EventArgs) Handles dashbtn.Click
        SetActiveButton(dashbtn)
        LoadDashboard()
    End Sub

    Private Sub devicebtn_Click(sender As Object, e As EventArgs) Handles devicebtn.Click
        SetActiveButton(devicebtn)
        LoadUserControl(New devices())
    End Sub

    Private Sub unitbtn_Click(sender As Object, e As EventArgs) Handles unitbtn.Click
        SetActiveButton(unitbtn)
        LoadUserControl(New Units())
    End Sub

    Private Sub confibtn_Click(sender As Object, e As EventArgs) Handles confibtn.Click
        SetActiveButton(confibtn)
        LoadUserControl(New Configuration())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SetActiveButton(Button1)
        LoadUserControl(New Reports())
    End Sub



    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim result = MessageBox.Show("Are you sure?", "You want to Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then

            ' 🔴 CLEAR SESSION
            Session.LoggedInUserPointer = 0
            Session.LoggedInRole = Nothing

            ' 🔄 SHOW LOGIN FORM AGAIN
            Dim login As New Login()
            login.Show()

            ' ❌ CLOSE THIS DASHBOARD
            Me.Close()
        End If
    End Sub
    ' ========================
    ' NAV BUTTON HIGHLIGHT
    ' ========================
    Private Sub SetActiveButton(active As Button)
        ' List all nav buttons that you want to highlight
        Dim navButtons() As Button = {dashbtn, devicebtn, unitbtn, confibtn, Button1}

        For Each btn As Button In navButtons
            btn.BackColor = SystemColors.Control    ' normal background
            btn.ForeColor = Color.Black             ' normal text
            btn.FlatAppearance.BorderSize = 0
        Next

        ' Highlight the active one (SKY BLUE)
        active.BackColor = Color.SkyBlue
        active.ForeColor = Color.White
        active.FlatAppearance.BorderSize = 1
    End Sub


End Class
