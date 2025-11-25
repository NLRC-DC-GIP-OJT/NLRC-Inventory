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
        mainpnl = New Panel()
        Panel7 = New Panel()
        Panel6 = New Panel()
        Panel3 = New Panel()
        Button5 = New Button()
        confibtn = New Button()
        unitbtn = New Button()
        devicebtn = New Button()
        dashbtn = New Button()
        Panel5 = New Panel()
        Panel2 = New Panel()
        Label1 = New Label()
        Label2 = New Label()
        userlbl = New Label()
        Label4 = New Label()
        dntlbl = New Label()
        Timer1 = New Timer(components)
        BindingSource1 = New BindingSource(components)
        Timer2 = New Timer(components)
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel6.SuspendLayout()
        Panel3.SuspendLayout()
        Panel5.SuspendLayout()
        CType(BindingSource1, ComponentModel.ISupportInitialize).BeginInit()
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
        Panel1.Size = New Size(1760, 1055)
        Panel1.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel4.Controls.Add(mainpnl)
        Panel4.Controls.Add(Panel7)
        Panel4.Controls.Add(Panel6)
        Panel4.Controls.Add(Panel5)
        Panel4.Controls.Add(dntlbl)
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1760, 1055)
        Panel4.TabIndex = 8
        ' 
        ' mainpnl
        ' 
        mainpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        mainpnl.AutoSize = True
        mainpnl.BackColor = Color.White
        mainpnl.Location = New Point(382, 157)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1353, 869)
        mainpnl.TabIndex = 4
        ' 
        ' Panel7
        ' 
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(360, 125)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(1400, 930)
        Panel7.TabIndex = 10
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(Panel3)
        Panel6.Dock = DockStyle.Left
        Panel6.Location = New Point(0, 125)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(360, 930)
        Panel6.TabIndex = 9
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Panel3.AutoSize = True
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(Button5)
        Panel3.Controls.Add(confibtn)
        Panel3.Controls.Add(unitbtn)
        Panel3.Controls.Add(devicebtn)
        Panel3.Controls.Add(dashbtn)
        Panel3.Location = New Point(27, 32)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(327, 870)
        Panel3.TabIndex = 3
        ' 
        ' Button5
        ' 
        Button5.AutoSize = True
        Button5.Dock = DockStyle.Bottom
        Button5.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button5.Location = New Point(0, 777)
        Button5.Name = "Button5"
        Button5.Size = New Size(327, 93)
        Button5.TabIndex = 4
        Button5.Text = "LOGOUT"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' confibtn
        ' 
        confibtn.AutoSize = True
        confibtn.Dock = DockStyle.Top
        confibtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        confibtn.Image = CType(resources.GetObject("confibtn.Image"), Image)
        confibtn.ImageAlign = ContentAlignment.MiddleLeft
        confibtn.Location = New Point(0, 282)
        confibtn.Name = "confibtn"
        confibtn.Padding = New Padding(25, 0, 0, 0)
        confibtn.Size = New Size(327, 91)
        confibtn.TabIndex = 3
        confibtn.Text = "       SETTINGS"
        confibtn.UseVisualStyleBackColor = True
        ' 
        ' unitbtn
        ' 
        unitbtn.AutoSize = True
        unitbtn.Dock = DockStyle.Top
        unitbtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        unitbtn.Image = CType(resources.GetObject("unitbtn.Image"), Image)
        unitbtn.ImageAlign = ContentAlignment.MiddleLeft
        unitbtn.Location = New Point(0, 189)
        unitbtn.Name = "unitbtn"
        unitbtn.Padding = New Padding(25, 0, 0, 0)
        unitbtn.Size = New Size(327, 93)
        unitbtn.TabIndex = 2
        unitbtn.Text = "UNITS"
        unitbtn.UseVisualStyleBackColor = True
        ' 
        ' devicebtn
        ' 
        devicebtn.AutoSize = True
        devicebtn.Dock = DockStyle.Top
        devicebtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicebtn.Image = CType(resources.GetObject("devicebtn.Image"), Image)
        devicebtn.ImageAlign = ContentAlignment.MiddleLeft
        devicebtn.Location = New Point(0, 93)
        devicebtn.Name = "devicebtn"
        devicebtn.Padding = New Padding(22, 0, 0, 0)
        devicebtn.Size = New Size(327, 96)
        devicebtn.TabIndex = 1
        devicebtn.Text = "    DEVICES" & vbCrLf & "                COMPONENTS"
        devicebtn.UseVisualStyleBackColor = True
        ' 
        ' dashbtn
        ' 
        dashbtn.AutoSize = True
        dashbtn.Dock = DockStyle.Top
        dashbtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        dashbtn.Image = CType(resources.GetObject("dashbtn.Image"), Image)
        dashbtn.ImageAlign = ContentAlignment.MiddleLeft
        dashbtn.Location = New Point(0, 0)
        dashbtn.Name = "dashbtn"
        dashbtn.Padding = New Padding(21, 0, 0, 0)
        dashbtn.Size = New Size(327, 93)
        dashbtn.TabIndex = 0
        dashbtn.Text = "              DASHBOARD"
        dashbtn.UseVisualStyleBackColor = True
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(Panel2)
        Panel5.Controls.Add(Label1)
        Panel5.Controls.Add(Label2)
        Panel5.Controls.Add(userlbl)
        Panel5.Controls.Add(Label4)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(0, 0)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(1760, 125)
        Panel5.TabIndex = 8
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(53, 12)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(117, 101)
        Panel2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Rounded MT Bold", 22.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(195, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(687, 43)
        Label1.TabIndex = 0
        Label1.Text = "National Labor Relations Commission"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 22.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(195, 67)
        Label2.Name = "Label2"
        Label2.Size = New Size(330, 43)
        Label2.TabIndex = 2
        Label2.Text = "Inventory System"
        ' 
        ' userlbl
        ' 
        userlbl.AutoSize = True
        userlbl.Location = New Point(1367, 20)
        userlbl.Name = "userlbl"
        userlbl.Size = New Size(102, 38)
        userlbl.TabIndex = 6
        userlbl.Text = "Label5"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(1214, 20)
        Label4.Name = "Label4"
        Label4.Size = New Size(147, 38)
        Label4.TabIndex = 5
        Label4.Text = "Welcome!"
        ' 
        ' dntlbl
        ' 
        dntlbl.AutoSize = True
        dntlbl.Location = New Point(622, 571)
        dntlbl.Name = "dntlbl"
        dntlbl.Size = New Size(209, 38)
        dntlbl.TabIndex = 7
        dntlbl.Text = "Date and Time"
        ' 
        ' Timer1
        ' 
        ' 
        ' Dashboard
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1760, 1055)
        Controls.Add(Panel1)
        Name = "Dashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dashboard"
        WindowState = FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        CType(BindingSource1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button5 As Button
    Friend WithEvents confibtn As Button
    Friend WithEvents unitbtn As Button
    Friend WithEvents devicebtn As Button
    Friend WithEvents dashbtn As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents mainpnl As Panel
    Friend WithEvents userlbl As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dntlbl As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel4 As Panel
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
End Class
