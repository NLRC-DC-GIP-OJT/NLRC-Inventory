Imports MySql.Data.MySqlClient

Public Class Login

    Private mdl As New model()

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set placeholder text
        usertxt.Text = "Enter Username"
        usertxt.ForeColor = Color.Gray

        passtxt.Text = "Enter Password"
        passtxt.ForeColor = Color.Gray
        passtxt.UseSystemPasswordChar = False
    End Sub

    ' ===== Username Placeholder Events =====
    Private Sub usertxt_Enter(sender As Object, e As EventArgs) Handles usertxt.Enter
        If usertxt.Text = "Enter Username" Then
            usertxt.Text = ""
            usertxt.ForeColor = Color.Black
        End If
    End Sub

    Private Sub usertxt_Leave(sender As Object, e As EventArgs) Handles usertxt.Leave
        If usertxt.Text = "" Then
            usertxt.Text = "Enter Username"
            usertxt.ForeColor = Color.Gray
        End If
    End Sub

    ' ===== Password Placeholder Events =====
    Private Sub passtxt_Enter(sender As Object, e As EventArgs) Handles passtxt.Enter
        If passtxt.Text = "Enter Password" Then
            passtxt.Text = ""
            passtxt.ForeColor = Color.Black
            passtxt.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub passtxt_Leave(sender As Object, e As EventArgs) Handles passtxt.Leave
        If passtxt.Text = "" Then
            passtxt.Text = "Enter Password"
            passtxt.ForeColor = Color.Gray
            passtxt.UseSystemPasswordChar = False
        End If
    End Sub

    ' ===== Login Button =====
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = usertxt.Text
        Dim password As String = passtxt.Text

        ' Prevent placeholder login
        If username = "" Or username = "Enter Username" Or
       password = "" Or password = "Enter Password" Then

            CustomMsg("Please enter username and password.", "⚠️ Warning")
            Return
        End If

        Try
            ' LoginUser now returns the pointer of the logged-in user
            Dim userPointer As Integer? = mdl.LoginUser(username, password)

            If userPointer Is Nothing Then
                CustomMsg("Invalid username or password.", "❌ Login Failed")
                Return
            End If

            ' Save globally
            Session.LoggedInUserPointer = userPointer.Value
            Session.LoggedInRole = mdl.GetRoleByPointer(userPointer.Value)

            Select Case Session.LoggedInRole
                Case "IVA"
                    CustomMsg("Welcome Admin!", "✅ Login Success")
                    Dim d As New Dashboard(username)
                    d.Show()
                    Me.Hide()

                Case "IVE"
                    CustomMsg("Welcome Editor!", "✅ Login Success")

                Case "IVV"
                    CustomMsg("Welcome Viewer!", "✅ Login Success")

                Case Else
                    CustomMsg("Unknown role detected.", "⚠️ Access Denied")
            End Select

        Catch ex As Exception
            CustomMsg("Error: " & ex.Message, "⚠️ System Error")
        End Try
    End Sub



    ' ===== Show / Hide Password =====
    Private Sub chkShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPass.CheckedChanged
        If passtxt.Text <> "Enter Password" Then
            passtxt.UseSystemPasswordChar = Not chkShowPass.Checked
        End If
    End Sub



    ' ===== Custom Big Message Box =====
    Private Sub CustomMsg(msg As String, title As String)
        Dim f As New Form With {
            .Width = 420,
            .Height = 220,
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .StartPosition = FormStartPosition.CenterScreen,
            .Text = title,
            .ControlBox = False
        }

        Dim lbl As New Label With {
            .Text = msg,
            .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .TextAlign = ContentAlignment.MiddleCenter
        }

        Dim btn As New Button With {
            .Text = "OK",
            .Dock = DockStyle.Bottom,
            .Height = 45
        }
        AddHandler btn.Click, Sub() f.Close()

        f.Controls.Add(lbl)
        f.Controls.Add(btn)
        f.ShowDialog()
    End Sub

    Private Sub Panel5_Click(sender As Object, e As EventArgs) Handles Panel5.Click
        Close()
    End Sub
End Class
