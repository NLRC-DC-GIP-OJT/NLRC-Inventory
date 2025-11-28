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
        userlbl = New Label()
        Panel2 = New Panel()
        Label1 = New Label()
        Label2 = New Label()
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
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1924, 1061)
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
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1924, 1061)
        Panel4.TabIndex = 8
        ' 
        ' mainpnl
        ' 
        mainpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        mainpnl.AutoSize = True
        mainpnl.BackColor = Color.White
        mainpnl.Location = New Point(334, 158)
        mainpnl.Margin = New Padding(3, 2, 3, 2)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1567, 882)
        mainpnl.TabIndex = 4
        ' 
        ' Panel7
        ' 
        Panel7.Dock = DockStyle.Fill
        Panel7.Location = New Point(315, 134)
        Panel7.Margin = New Padding(3, 2, 3, 2)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(1609, 927)
        Panel7.TabIndex = 10
        ' 
        ' Panel6
        ' 
        Panel6.Controls.Add(Panel3)
        Panel6.Dock = DockStyle.Left
        Panel6.Location = New Point(0, 134)
        Panel6.Margin = New Padding(3, 2, 3, 2)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(315, 927)
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
        Panel3.Location = New Point(24, 24)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(286, 881)
        Panel3.TabIndex = 3
        ' 
        ' Button5
        ' 
        Button5.AutoSize = True
        Button5.Dock = DockStyle.Bottom
        Button5.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button5.Image = CType(resources.GetObject("Button5.Image"), Image)
        Button5.ImageAlign = ContentAlignment.MiddleLeft
        Button5.Location = New Point(0, 772)
        Button5.Margin = New Padding(3, 2, 3, 2)
        Button5.Name = "Button5"
        Button5.Padding = New Padding(35, 0, 0, 0)
        Button5.Size = New Size(286, 109)
        Button5.TabIndex = 4
        Button5.Text = "    LOGOUT"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' confibtn
        ' 
        confibtn.AutoSize = True
        confibtn.Dock = DockStyle.Top
        confibtn.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        confibtn.Image = CType(resources.GetObject("confibtn.Image"), Image)
        confibtn.ImageAlign = ContentAlignment.MiddleLeft
        confibtn.Location = New Point(0, 340)
        confibtn.Margin = New Padding(3, 2, 3, 2)
        confibtn.Name = "confibtn"
        confibtn.Padding = New Padding(22, 0, 0, 0)
        confibtn.Size = New Size(286, 135)
        confibtn.TabIndex = 3
        confibtn.Text = "        SETTINGS"
        confibtn.UseVisualStyleBackColor = True
        ' 
        ' unitbtn
        ' 
        unitbtn.AutoSize = True
        unitbtn.Dock = DockStyle.Top
        unitbtn.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        unitbtn.Image = CType(resources.GetObject("unitbtn.Image"), Image)
        unitbtn.ImageAlign = ContentAlignment.MiddleLeft
        unitbtn.Location = New Point(0, 230)
        unitbtn.Margin = New Padding(3, 2, 3, 2)
        unitbtn.Name = "unitbtn"
        unitbtn.Padding = New Padding(22, 0, 0, 0)
        unitbtn.Size = New Size(286, 110)
        unitbtn.TabIndex = 2
        unitbtn.Text = "UNITS"
        unitbtn.UseVisualStyleBackColor = True
        ' 
        ' devicebtn
        ' 
        devicebtn.AutoSize = True
        devicebtn.Dock = DockStyle.Top
        devicebtn.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        devicebtn.Image = CType(resources.GetObject("devicebtn.Image"), Image)
        devicebtn.ImageAlign = ContentAlignment.MiddleLeft
        devicebtn.Location = New Point(0, 114)
        devicebtn.Margin = New Padding(3, 2, 3, 2)
        devicebtn.Name = "devicebtn"
        devicebtn.Padding = New Padding(19, 0, 0, 0)
        devicebtn.Size = New Size(286, 116)
        devicebtn.TabIndex = 1
        devicebtn.Text = "      DEVICES" & vbCrLf & "                COMPONENTS"
        devicebtn.UseVisualStyleBackColor = True
        ' 
        ' dashbtn
        ' 
        dashbtn.AutoSize = True
        dashbtn.Dock = DockStyle.Top
        dashbtn.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        dashbtn.Image = CType(resources.GetObject("dashbtn.Image"), Image)
        dashbtn.ImageAlign = ContentAlignment.MiddleLeft
        dashbtn.Location = New Point(0, 0)
        dashbtn.Margin = New Padding(3, 2, 3, 2)
        dashbtn.Name = "dashbtn"
        dashbtn.Padding = New Padding(18, 0, 0, 0)
        dashbtn.Size = New Size(286, 114)
        dashbtn.TabIndex = 0
        dashbtn.Text = "              DASHBOARD"
        dashbtn.UseVisualStyleBackColor = True
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(userlbl)
        Panel5.Controls.Add(Panel2)
        Panel5.Controls.Add(Label1)
        Panel5.Controls.Add(Label2)
        Panel5.Controls.Add(Label4)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(0, 0)
        Panel5.Margin = New Padding(3, 2, 3, 2)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(1924, 134)
        Panel5.TabIndex = 8
        ' 
        ' userlbl
        ' 
        userlbl.AutoSize = True
        userlbl.Font = New Font("Arial", 21.75F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        userlbl.Location = New Point(1552, 32)
        userlbl.Name = "userlbl"
        userlbl.Size = New Size(107, 34)
        userlbl.TabIndex = 6
        userlbl.Text = "Label5"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(42, 1)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(134, 131)
        Panel2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 27.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(193, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(692, 44)
        Label1.TabIndex = 0
        Label1.Text = "National Labor Relations Commission"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 27.75F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(193, 72)
        Label2.Name = "Label2"
        Label2.Size = New Size(328, 42)
        Label2.TabIndex = 2
        Label2.Text = "Inventory System"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(1378, 31)
        Label4.Name = "Label4"
        Label4.Size = New Size(168, 37)
        Label4.TabIndex = 5
        Label4.Text = "Welcome!"
        ' 
        ' dntlbl
        ' 
        dntlbl.AutoSize = True
        dntlbl.Location = New Point(544, 428)
        dntlbl.Name = "dntlbl"
        dntlbl.Size = New Size(164, 30)
        dntlbl.TabIndex = 7
        dntlbl.Text = "Date and Time"
        ' 
        ' Timer1
        ' 
        ' 
        ' Dashboard
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1061)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
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
