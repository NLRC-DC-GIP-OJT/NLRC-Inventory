Public Class Dashboard

    Private LoggedUser As String

    ' ✅ Default constructor (needed for designer)
    Public Sub New()
        InitializeComponent()
    End Sub

    ' ✅ Constructor with username parameter
    Public Sub New(user As String)
        InitializeComponent()
        LoggedUser = user
    End Sub

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        userlbl.Text = LoggedUser

        ' ✅ Timer starts
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        dntlbl.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        mainpnl.Controls.Clear()
        Dim uc As New devices()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
    End Sub

    Private Sub unitbtn_Click(sender As Object, e As EventArgs) Handles unitbtn.Click
        mainpnl.Controls.Clear()
        Dim uc As New Units()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        mainpnl.Controls.Clear()
        Dim uc As New Configuration()
        uc.Dock = DockStyle.Fill
        mainpnl.Controls.Add(uc)
    End Sub
End Class
