<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditUnit
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditUnit))
        Panel1 = New Panel()
        Panel2 = New Panel()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        deviceflowpnl = New FlowLayoutPanel()
        specsflowpnl = New FlowLayoutPanel()
        unitnametxt = New TextBox()
        assigntxt = New TextBox()
        Panel3 = New Panel()
        devicecb = New ComboBox()
        adddevicebtn = New Button()
        savebtn = New Button()
        Panel4 = New Panel()
        savespecsbtn = New Button()
        assigncb = New ComboBox()
        Label6 = New Label()
        Label7 = New Label()
        remarkstxt = New TextBox()
        assignbtn = New Button()
        Panel5 = New Panel()
        disposalbtn = New Button()
        Label5 = New Label()
        statuscombo = New ComboBox()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1009, 35)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(969, 4)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(31, 25)
        Panel2.TabIndex = 21
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(6, 6)
        Label1.Name = "Label1"
        Label1.Size = New Size(268, 24)
        Label1.TabIndex = 1
        Label1.Text = "Update Asset Information"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(23, 57)
        Label2.Name = "Label2"
        Label2.Size = New Size(95, 18)
        Label2.TabIndex = 1
        Label2.Text = "Unit Name:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(482, 60)
        Label3.Name = "Label3"
        Label3.Size = New Size(107, 18)
        Label3.TabIndex = 2
        Label3.Text = "Assigned to:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(23, 95)
        Label4.Name = "Label4"
        Label4.Size = New Size(77, 18)
        Label4.TabIndex = 3
        Label4.Text = "Devices:"
        ' 
        ' deviceflowpnl
        ' 
        deviceflowpnl.AutoScroll = True
        deviceflowpnl.BorderStyle = BorderStyle.FixedSingle
        deviceflowpnl.Dock = DockStyle.Fill
        deviceflowpnl.FlowDirection = FlowDirection.TopDown
        deviceflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        deviceflowpnl.Location = New Point(0, 0)
        deviceflowpnl.Margin = New Padding(3, 2, 3, 2)
        deviceflowpnl.Name = "deviceflowpnl"
        deviceflowpnl.Size = New Size(432, 298)
        deviceflowpnl.TabIndex = 18
        deviceflowpnl.WrapContents = False
        ' 
        ' specsflowpnl
        ' 
        specsflowpnl.AutoScroll = True
        specsflowpnl.BorderStyle = BorderStyle.FixedSingle
        specsflowpnl.Dock = DockStyle.Top
        specsflowpnl.FlowDirection = FlowDirection.TopDown
        specsflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsflowpnl.Location = New Point(0, 0)
        specsflowpnl.Margin = New Padding(3, 2, 3, 2)
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(495, 236)
        specsflowpnl.TabIndex = 19
        specsflowpnl.WrapContents = False
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(144, 54)
        unitnametxt.Margin = New Padding(3, 2, 3, 2)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(312, 26)
        unitnametxt.TabIndex = 20
        ' 
        ' assigntxt
        ' 
        assigntxt.BorderStyle = BorderStyle.FixedSingle
        assigntxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigntxt.Location = New Point(607, 56)
        assigntxt.Margin = New Padding(3, 2, 3, 2)
        assigntxt.Name = "assigntxt"
        assigntxt.Size = New Size(376, 26)
        assigntxt.TabIndex = 21
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(deviceflowpnl)
        Panel3.Location = New Point(23, 178)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(432, 298)
        Panel3.TabIndex = 22
        ' 
        ' devicecb
        ' 
        devicecb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(144, 95)
        devicecb.Margin = New Padding(3, 2, 3, 2)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(311, 26)
        devicecb.TabIndex = 25
        ' 
        ' adddevicebtn
        ' 
        adddevicebtn.BackColor = Color.SlateGray
        adddevicebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        adddevicebtn.ForeColor = Color.White
        adddevicebtn.Location = New Point(305, 126)
        adddevicebtn.Margin = New Padding(3, 2, 3, 2)
        adddevicebtn.Name = "adddevicebtn"
        adddevicebtn.Size = New Size(150, 45)
        adddevicebtn.TabIndex = 26
        adddevicebtn.Text = "Add Device"
        adddevicebtn.UseVisualStyleBackColor = False
        ' 
        ' savebtn
        ' 
        savebtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        savebtn.AutoSize = True
        savebtn.BackColor = Color.SlateGray
        savebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(354, 282)
        savebtn.Margin = New Padding(3, 2, 3, 2)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(132, 39)
        savebtn.TabIndex = 27
        savebtn.Text = "Save "
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(savebtn)
        Panel4.Controls.Add(savespecsbtn)
        Panel4.Controls.Add(specsflowpnl)
        Panel4.Location = New Point(488, 203)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(495, 326)
        Panel4.TabIndex = 28
        ' 
        ' savespecsbtn
        ' 
        savespecsbtn.FlatAppearance.BorderSize = 2
        savespecsbtn.FlatStyle = FlatStyle.Flat
        savespecsbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        savespecsbtn.Location = New Point(139, 245)
        savespecsbtn.Margin = New Padding(3, 2, 3, 2)
        savespecsbtn.Name = "savespecsbtn"
        savespecsbtn.Size = New Size(261, 33)
        savespecsbtn.TabIndex = 30
        savespecsbtn.Text = "Save Specification Field"
        savespecsbtn.UseVisualStyleBackColor = True
        ' 
        ' assigncb
        ' 
        assigncb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigncb.FormattingEnabled = True
        assigncb.Location = New Point(607, 95)
        assigncb.Margin = New Padding(3, 2, 3, 2)
        assigncb.Name = "assigncb"
        assigncb.Size = New Size(281, 26)
        assigncb.TabIndex = 29
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(483, 97)
        Label6.Name = "Label6"
        Label6.Size = New Size(106, 18)
        Label6.TabIndex = 30
        Label6.Text = "New Assign:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(483, 148)
        Label7.Name = "Label7"
        Label7.Size = New Size(84, 18)
        Label7.TabIndex = 31
        Label7.Text = "Remarks:"
        ' 
        ' remarkstxt
        ' 
        remarkstxt.BorderStyle = BorderStyle.FixedSingle
        remarkstxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        remarkstxt.Location = New Point(607, 137)
        remarkstxt.Margin = New Padding(3, 2, 3, 2)
        remarkstxt.Multiline = True
        remarkstxt.Name = "remarkstxt"
        remarkstxt.Size = New Size(376, 59)
        remarkstxt.TabIndex = 32
        ' 
        ' assignbtn
        ' 
        assignbtn.FlatAppearance.BorderSize = 2
        assignbtn.FlatStyle = FlatStyle.Flat
        assignbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assignbtn.Location = New Point(894, 92)
        assignbtn.Margin = New Padding(3, 2, 3, 2)
        assignbtn.Name = "assignbtn"
        assignbtn.Size = New Size(89, 29)
        assignbtn.TabIndex = 33
        assignbtn.Text = "Add"
        assignbtn.UseVisualStyleBackColor = True
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(statuscombo)
        Panel5.Controls.Add(Label5)
        Panel5.Controls.Add(disposalbtn)
        Panel5.Controls.Add(remarkstxt)
        Panel5.Controls.Add(assignbtn)
        Panel5.Controls.Add(Label6)
        Panel5.Controls.Add(Label7)
        Panel5.Controls.Add(devicecb)
        Panel5.Controls.Add(Panel4)
        Panel5.Controls.Add(Label2)
        Panel5.Controls.Add(unitnametxt)
        Panel5.Controls.Add(adddevicebtn)
        Panel5.Controls.Add(Label4)
        Panel5.Controls.Add(Label3)
        Panel5.Dock = DockStyle.Fill
        Panel5.Location = New Point(0, 0)
        Panel5.Margin = New Padding(3, 2, 3, 2)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(1009, 557)
        Panel5.TabIndex = 34
        ' 
        ' disposalbtn
        ' 
        disposalbtn.BackColor = Color.SlateGray
        disposalbtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        disposalbtn.ForeColor = Color.White
        disposalbtn.Location = New Point(23, 485)
        disposalbtn.Margin = New Padding(3, 2, 3, 2)
        disposalbtn.Name = "disposalbtn"
        disposalbtn.Size = New Size(138, 44)
        disposalbtn.TabIndex = 34
        disposalbtn.Text = "For Disposal"
        disposalbtn.UseVisualStyleBackColor = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(23, 139)
        Label5.Name = "Label5"
        Label5.Size = New Size(65, 18)
        Label5.TabIndex = 35
        Label5.Text = "Status:"
        ' 
        ' statuscombo
        ' 
        statuscombo.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        statuscombo.FormattingEnabled = True
        statuscombo.Location = New Point(103, 136)
        statuscombo.Margin = New Padding(3, 2, 3, 2)
        statuscombo.Name = "statuscombo"
        statuscombo.Size = New Size(196, 26)
        statuscombo.TabIndex = 36
        ' 
        ' EditUnit
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(assigncb)
        Controls.Add(Panel3)
        Controls.Add(assigntxt)
        Controls.Add(Panel1)
        Controls.Add(Panel5)
        Name = "EditUnit"
        Size = New Size(1009, 557)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents deviceflowpnl As FlowLayoutPanel
    Friend WithEvents specsflowpnl As FlowLayoutPanel
    Friend WithEvents unitnametxt As TextBox
    Friend WithEvents assigntxt As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents devicecb As ComboBox
    Friend WithEvents adddevicebtn As Button
    Friend WithEvents savebtn As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents assigncb As ComboBox
    Friend WithEvents savespecsbtn As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents remarkstxt As TextBox
    Friend WithEvents assignbtn As Button
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents disposalbtn As Button
    Friend WithEvents statuscombo As ComboBox
    Friend WithEvents Label5 As Label

End Class
