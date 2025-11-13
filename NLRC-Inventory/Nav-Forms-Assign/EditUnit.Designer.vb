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
        Label5 = New Label()
        deviceflowpnl = New FlowLayoutPanel()
        specsflowpnl = New FlowLayoutPanel()
        unitnametxt = New TextBox()
        assigntxt = New TextBox()
        Panel3 = New Panel()
        devicecb = New ComboBox()
        adddevicebtn = New Button()
        savebtn = New Button()
        Panel4 = New Panel()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(920, 47)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(863, 3)
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
        Label1.Size = New Size(329, 32)
        Label1.TabIndex = 1
        Label1.Text = "Update Unit Information"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(7, 71)
        Label2.Name = "Label2"
        Label2.Size = New Size(117, 23)
        Label2.TabIndex = 1
        Label2.Text = "Unit Name:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(521, 71)
        Label3.Name = "Label3"
        Label3.Size = New Size(131, 23)
        Label3.TabIndex = 2
        Label3.Text = "Assigned to:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(16, 115)
        Label4.Name = "Label4"
        Label4.Size = New Size(94, 23)
        Label4.TabIndex = 3
        Label4.Text = "Devices:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(521, 115)
        Label5.Name = "Label5"
        Label5.Size = New Size(77, 23)
        Label5.TabIndex = 4
        Label5.Text = "Specs:"
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
        deviceflowpnl.Size = New Size(433, 233)
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
        specsflowpnl.Size = New Size(388, 227)
        specsflowpnl.TabIndex = 19
        specsflowpnl.WrapContents = False
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(130, 69)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(329, 31)
        unitnametxt.TabIndex = 20
        ' 
        ' assigntxt
        ' 
        assigntxt.BorderStyle = BorderStyle.FixedSingle
        assigntxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigntxt.Location = New Point(658, 69)
        assigntxt.Name = "assigntxt"
        assigntxt.Size = New Size(240, 31)
        assigntxt.TabIndex = 21
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(deviceflowpnl)
        Panel3.Location = New Point(26, 216)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(433, 233)
        Panel3.TabIndex = 22
        ' 
        ' devicecb
        ' 
        devicecb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(130, 112)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(329, 31)
        devicecb.TabIndex = 25
        ' 
        ' adddevicebtn
        ' 
        adddevicebtn.BackColor = Color.SlateGray
        adddevicebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        adddevicebtn.ForeColor = Color.White
        adddevicebtn.Location = New Point(287, 165)
        adddevicebtn.Name = "adddevicebtn"
        adddevicebtn.Size = New Size(172, 44)
        adddevicebtn.TabIndex = 26
        adddevicebtn.Text = "Add Device"
        adddevicebtn.UseVisualStyleBackColor = False
        ' 
        ' savebtn
        ' 
        savebtn.BackColor = Color.MediumBlue
        savebtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(747, 429)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(151, 44)
        savebtn.TabIndex = 27
        savebtn.Text = "Save "
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(specsflowpnl)
        Panel4.Location = New Point(510, 141)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(388, 282)
        Panel4.TabIndex = 28
        ' 
        ' EditUnit
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel4)
        Controls.Add(savebtn)
        Controls.Add(adddevicebtn)
        Controls.Add(devicecb)
        Controls.Add(Panel3)
        Controls.Add(assigntxt)
        Controls.Add(unitnametxt)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Panel1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "EditUnit"
        Size = New Size(920, 487)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
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

End Class
