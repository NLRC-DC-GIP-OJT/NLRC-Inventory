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
        specsbtn = New Button()
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
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1121, 47)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(1067, 8)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(35, 33)
        Panel2.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(7, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(350, 32)
        Label1.TabIndex = 1
        Label1.Text = "Update Asset Information"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(26, 67)
        Label2.Name = "Label2"
        Label2.Size = New Size(117, 23)
        Label2.TabIndex = 1
        Label2.Text = "Unit Name:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(526, 63)
        Label3.Name = "Label3"
        Label3.Size = New Size(131, 23)
        Label3.TabIndex = 2
        Label3.Text = "Assigned to:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(26, 117)
        Label4.Name = "Label4"
        Label4.Size = New Size(94, 23)
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
        deviceflowpnl.Name = "deviceflowpnl"
        deviceflowpnl.Size = New Size(473, 331)
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
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(557, 230)
        specsflowpnl.TabIndex = 19
        specsflowpnl.WrapContents = False
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(165, 63)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(308, 31)
        unitnametxt.TabIndex = 20
        ' 
        ' assigntxt
        ' 
        assigntxt.BorderStyle = BorderStyle.FixedSingle
        assigntxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigntxt.Location = New Point(694, 59)
        assigntxt.Name = "assigntxt"
        assigntxt.Size = New Size(391, 31)
        assigntxt.TabIndex = 21
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(deviceflowpnl)
        Panel3.Location = New Point(26, 238)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(473, 331)
        Panel3.TabIndex = 22
        ' 
        ' devicecb
        ' 
        devicecb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(165, 117)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(307, 31)
        devicecb.TabIndex = 25
        ' 
        ' adddevicebtn
        ' 
        adddevicebtn.BackColor = Color.SlateGray
        adddevicebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        adddevicebtn.ForeColor = Color.White
        adddevicebtn.Location = New Point(328, 168)
        adddevicebtn.Name = "adddevicebtn"
        adddevicebtn.Size = New Size(171, 46)
        adddevicebtn.TabIndex = 26
        adddevicebtn.Text = "Add Device"
        adddevicebtn.UseVisualStyleBackColor = False
        ' 
        ' savebtn
        ' 
        savebtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        savebtn.AutoSize = True
        savebtn.BackColor = Color.MediumBlue
        savebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(396, 296)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(151, 52)
        savebtn.TabIndex = 27
        savebtn.Text = "Save "
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(savebtn)
        Panel4.Controls.Add(savespecsbtn)
        Panel4.Controls.Add(specsflowpnl)
        Panel4.Location = New Point(527, 285)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(557, 355)
        Panel4.TabIndex = 28
        ' 
        ' savespecsbtn
        ' 
        savespecsbtn.FlatAppearance.BorderSize = 2
        savespecsbtn.FlatStyle = FlatStyle.Flat
        savespecsbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        savespecsbtn.Location = New Point(123, 244)
        savespecsbtn.Name = "savespecsbtn"
        savespecsbtn.Size = New Size(298, 44)
        savespecsbtn.TabIndex = 30
        savespecsbtn.Text = "Save Specification Field"
        savespecsbtn.UseVisualStyleBackColor = True
        ' 
        ' assigncb
        ' 
        assigncb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigncb.FormattingEnabled = True
        assigncb.Location = New Point(694, 104)
        assigncb.Name = "assigncb"
        assigncb.Size = New Size(294, 31)
        assigncb.TabIndex = 29
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(527, 107)
        Label6.Name = "Label6"
        Label6.Size = New Size(130, 23)
        Label6.TabIndex = 30
        Label6.Text = "New Assign:"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(527, 168)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 23)
        Label7.TabIndex = 31
        Label7.Text = "Remarks:"
        ' 
        ' remarkstxt
        ' 
        remarkstxt.BorderStyle = BorderStyle.FixedSingle
        remarkstxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        remarkstxt.Location = New Point(694, 149)
        remarkstxt.Multiline = True
        remarkstxt.Name = "remarkstxt"
        remarkstxt.Size = New Size(391, 78)
        remarkstxt.TabIndex = 32
        ' 
        ' assignbtn
        ' 
        assignbtn.FlatAppearance.BorderSize = 2
        assignbtn.FlatStyle = FlatStyle.Flat
        assignbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assignbtn.Location = New Point(994, 101)
        assignbtn.Name = "assignbtn"
        assignbtn.Size = New Size(91, 39)
        assignbtn.TabIndex = 33
        assignbtn.Text = "Add"
        assignbtn.UseVisualStyleBackColor = True
        ' 
        ' Panel5
        ' 
        Panel5.Controls.Add(specsbtn)
        Panel5.Controls.Add(Label6)
        Panel5.Controls.Add(Label7)
        Panel5.Controls.Add(devicecb)
        Panel5.Controls.Add(Label2)
        Panel5.Controls.Add(unitnametxt)
        Panel5.Controls.Add(adddevicebtn)
        Panel5.Controls.Add(Label4)
        Panel5.Controls.Add(Label3)
        Panel5.Dock = DockStyle.Fill
        Panel5.Location = New Point(0, 0)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(1121, 645)
        Panel5.TabIndex = 34
        ' 
        ' specsbtn
        ' 
        specsbtn.FlatAppearance.BorderSize = 2
        specsbtn.FlatStyle = FlatStyle.Flat
        specsbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsbtn.Location = New Point(527, 238)
        specsbtn.Name = "specsbtn"
        specsbtn.Size = New Size(558, 36)
        specsbtn.TabIndex = 33
        specsbtn.Text = "Specifications"
        specsbtn.UseVisualStyleBackColor = True
        ' 
        ' EditUnit
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(assignbtn)
        Controls.Add(remarkstxt)
        Controls.Add(assigncb)
        Controls.Add(Panel4)
        Controls.Add(Panel3)
        Controls.Add(assigntxt)
        Controls.Add(Panel1)
        Controls.Add(Panel5)
        Margin = New Padding(3, 4, 3, 4)
        Name = "EditUnit"
        Size = New Size(1121, 645)
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
    Friend WithEvents Panel2 As Panel
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
    Friend WithEvents specsbtn As Button

End Class
