<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Panel1 = New Panel()
        Label1 = New Label()
        Label2 = New Label()
        Panel2 = New Panel()
        Label3 = New Label()
        Panel3 = New Panel()
        chkShowPass = New CheckBox()
        Button1 = New Button()
        passtxt = New TextBox()
        usertxt = New TextBox()
        Panel4 = New Panel()
        Label4 = New Label()
        Panel5 = New Panel()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Location = New Point(106, 29)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(250, 234)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.MediumBlue
        Label1.Location = New Point(91, 280)
        Label1.Name = "Label1"
        Label1.Size = New Size(319, 46)
        Label1.TabIndex = 1
        Label1.Text = "Welcome to NLRC" & vbCrLf
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI Black", 22.2F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(91, 326)
        Label2.Name = "Label2"
        Label2.Size = New Size(337, 50)
        Label2.TabIndex = 2
        Label2.Text = "Inventory System"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.CornflowerBlue
        Panel2.Controls.Add(Label3)
        Panel2.Location = New Point(55, 409)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(392, 77)
        Panel2.TabIndex = 3
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.White
        Label3.Location = New Point(33, 27)
        Label3.Name = "Label3"
        Label3.Size = New Size(329, 24)
        Label3.TabIndex = 0
        Label3.Text = "Please login to manage resources"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Transparent
        Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), Image)
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Controls.Add(chkShowPass)
        Panel3.Controls.Add(Button1)
        Panel3.Controls.Add(passtxt)
        Panel3.Controls.Add(usertxt)
        Panel3.Controls.Add(Panel4)
        Panel3.Location = New Point(527, 76)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(363, 394)
        Panel3.TabIndex = 4
        ' 
        ' chkShowPass
        ' 
        chkShowPass.AutoSize = True
        chkShowPass.BackColor = SystemColors.Window
        chkShowPass.Location = New Point(258, 208)
        chkShowPass.Name = "chkShowPass"
        chkShowPass.Size = New Size(67, 24)
        chkShowPass.TabIndex = 8
        chkShowPass.Text = "Show"
        chkShowPass.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DarkBlue
        Button1.Font = New Font("Arial Black", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = Color.White
        Button1.Location = New Point(101, 288)
        Button1.Name = "Button1"
        Button1.Size = New Size(175, 55)
        Button1.TabIndex = 7
        Button1.Text = "LOGIN"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' passtxt
        ' 
        passtxt.BorderStyle = BorderStyle.FixedSingle
        passtxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        passtxt.Location = New Point(38, 201)
        passtxt.Name = "passtxt"
        passtxt.Size = New Size(295, 38)
        passtxt.TabIndex = 6
        ' 
        ' usertxt
        ' 
        usertxt.BorderStyle = BorderStyle.FixedSingle
        usertxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        usertxt.Location = New Point(38, 136)
        usertxt.Name = "usertxt"
        usertxt.Size = New Size(295, 38)
        usertxt.TabIndex = 5
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.CornflowerBlue
        Panel4.Controls.Add(Label4)
        Panel4.Location = New Point(35, 43)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(298, 54)
        Panel4.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.White
        Label4.Location = New Point(48, 15)
        Label4.Name = "Label4"
        Label4.Size = New Size(193, 24)
        Label4.TabIndex = 0
        Label4.Text = "Login your account"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.Transparent
        Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), Image)
        Panel5.BackgroundImageLayout = ImageLayout.Stretch
        Panel5.Location = New Point(884, 12)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(35, 33)
        Panel5.TabIndex = 20
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.background
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(941, 533)
        Controls.Add(Panel5)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(Panel1)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.None
        Name = "Login"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form1"
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents passtxt As TextBox
    Friend WithEvents usertxt As TextBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents chkShowPass As CheckBox
    Friend WithEvents Panel5 As Panel

End Class
