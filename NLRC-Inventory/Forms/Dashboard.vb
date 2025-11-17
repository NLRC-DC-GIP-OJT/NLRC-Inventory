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
        LoadDashboard()

        ' Start timer for clock
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
        LoadDashboard()
    End Sub

    Private Sub devicebtn_Click(sender As Object, e As EventArgs) Handles devicebtn.Click
        LoadUserControl(New devices())
    End Sub

    Private Sub unitbtn_Click(sender As Object, e As EventArgs) Handles unitbtn.Click
        LoadUserControl(New Units())
    End Sub

    Private Sub confibtn_Click(sender As Object, e As EventArgs) Handles confibtn.Click
        LoadUserControl(New Configuration())
    End Sub

End Class
