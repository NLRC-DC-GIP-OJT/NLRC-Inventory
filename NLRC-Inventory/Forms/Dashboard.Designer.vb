<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Panel1 = New Panel()
        Panel4 = New Panel()
        Panel2 = New Panel()
        Panel3 = New Panel()
        Button5 = New Button()
        Button4 = New Button()
        unitbtn = New Button()
        Button2 = New Button()
        Button1 = New Button()
        mainpnl = New Panel()
        Label3 = New Label()
        userlbl = New Label()
        dntlbl = New Label()
        Label4 = New Label()
        Label1 = New Label()
        Label2 = New Label()
        Timer1 = New Timer(components)
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        mainpnl.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.BackgroundImage = My.Resources.Resources.background
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1902, 1108)
        Panel1.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Panel2)
        Panel4.Controls.Add(Panel3)
        Panel4.Controls.Add(mainpnl)
        Panel4.Controls.Add(userlbl)
        Panel4.Controls.Add(dntlbl)
        Panel4.Controls.Add(Label4)
        Panel4.Controls.Add(Label1)
        Panel4.Controls.Add(Label2)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1902, 1108)
        Panel4.TabIndex = 8
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(24, 16)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(117, 101)
        Panel2.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(Button5)
        Panel3.Controls.Add(Button4)
        Panel3.Controls.Add(unitbtn)
        Panel3.Controls.Add(Button2)
        Panel3.Controls.Add(Button1)
        Panel3.Location = New Point(40, 158)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(309, 898)
        Panel3.TabIndex = 3
        ' 
        ' Button5
        ' 
        Button5.Dock = DockStyle.Top
        Button5.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button5.Location = New Point(0, 359)
        Button5.Name = "Button5"
        Button5.Size = New Size(309, 94)
        Button5.TabIndex = 4
        Button5.Text = "SETTINGS"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Dock = DockStyle.Top
        Button4.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button4.Location = New Point(0, 268)
        Button4.Name = "Button4"
        Button4.Size = New Size(309, 91)
        Button4.TabIndex = 3
        Button4.Text = "SETTINGS"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' unitbtn
        ' 
        unitbtn.Dock = DockStyle.Top
        unitbtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        unitbtn.Image = CType(resources.GetObject("unitbtn.Image"), Image)
        unitbtn.ImageAlign = ContentAlignment.MiddleLeft
        unitbtn.Location = New Point(0, 178)
        unitbtn.Name = "unitbtn"
        unitbtn.Padding = New Padding(25, 0, 0, 0)
        unitbtn.Size = New Size(309, 90)
        unitbtn.TabIndex = 2
        unitbtn.Text = "UNITS"
        unitbtn.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Dock = DockStyle.Top
        Button2.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button2.Image = CType(resources.GetObject("Button2.Image"), Image)
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.Location = New Point(0, 92)
        Button2.Name = "Button2"
        Button2.Padding = New Padding(25, 0, 0, 0)
        Button2.Size = New Size(309, 86)
        Button2.TabIndex = 1
        Button2.Text = "    DEVICES" & vbCrLf & "       COMPONENTS"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Dock = DockStyle.Top
        Button1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.ImageAlign = ContentAlignment.MiddleLeft
        Button1.Location = New Point(0, 0)
        Button1.Name = "Button1"
        Button1.Padding = New Padding(20, 0, 0, 0)
        Button1.Size = New Size(309, 92)
        Button1.TabIndex = 0
        Button1.Text = "            DASHBOARD"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' mainpnl
        ' 
        mainpnl.BackColor = Color.White
        mainpnl.Controls.Add(Label3)
        mainpnl.Location = New Point(426, 158)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1440, 898)
        mainpnl.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(48, 26)
        Label3.Name = "Label3"
        Label3.Size = New Size(193, 38)
        Label3.TabIndex = 5
        Label3.Text = "DASHBOARD"
        ' 
        ' userlbl
        ' 
        userlbl.AutoSize = True
        userlbl.Location = New Point(1513, 29)
        userlbl.Name = "userlbl"
        userlbl.Size = New Size(102, 38)
        userlbl.TabIndex = 6
        userlbl.Text = "Label5"
        ' 
        ' dntlbl
        ' 
        dntlbl.AutoSize = True
        dntlbl.Location = New Point(774, 28)
        dntlbl.Name = "dntlbl"
        dntlbl.Size = New Size(209, 38)
        dntlbl.TabIndex = 7
        dntlbl.Text = "Date and Time"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(1360, 28)
        Label4.Name = "Label4"
        Label4.Size = New Size(147, 38)
        Label4.TabIndex = 5
        Label4.Text = "Welcome!"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(147, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(508, 38)
        Label1.TabIndex = 0
        Label1.Text = "National Labor Relations Commission"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 18F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(147, 67)
        Label2.Name = "Label2"
        Label2.Size = New Size(259, 41)
        Label2.TabIndex = 2
        Label2.Text = "Inventory System"
        ' 
        ' Timer1
        ' 
        ' 
        ' Dashboard
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1902, 1108)
        Controls.Add(Panel1)
        Name = "Dashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dashboard"
        WindowState = FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
        mainpnl.ResumeLayout(False)
        mainpnl.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents unitbtn As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents mainpnl As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents userlbl As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dntlbl As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel4 As Panel
End Class
