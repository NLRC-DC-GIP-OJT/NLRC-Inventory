Imports System.Collections.Generic
Imports System.Drawing

Public Class Dashboard

    Private LoggedUser As String
    Private model As New model() ' Your Model class

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

        ' ❌ Removed InitializeLayoutScaling()

        LoadDashboard()

        ' highlight Dashboard by default
        SetActiveButton(dashbtn)

        Timer1.Interval = 1000
        Timer1.Start()
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

        ' Reset them to their designer look
        For Each btn As Button In navButtons
            btn.UseVisualStyleBackColor = True      ' keeps the BackColor set in designer
            btn.FlatAppearance.BorderSize = 0
            btn.ForeColor = Color.Black             ' change if you use another default
        Next

        ' Highlight the active one
        Dim activeColor As Color = ColorTranslator.FromHtml("#5596D5")
        active.BackColor = activeColor
        active.ForeColor = Color.White
        active.FlatAppearance.BorderSize = 1
    End Sub

End Class
