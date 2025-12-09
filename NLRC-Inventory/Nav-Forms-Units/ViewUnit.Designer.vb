<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewUnit))
        Panel3 = New Panel()
        deviceflowpnl = New FlowLayoutPanel()
        assigntxt = New TextBox()
        unitnametxt = New TextBox()
        specsflowpnl = New FlowLayoutPanel()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Label1 = New Label()
        specsbtn = New Button()
        btnGenerateQR = New Button()
        mainpanelqr = New Panel()
        printqrformat = New Panel()
        Panel3.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(deviceflowpnl)
        Panel3.Location = New Point(18, 123)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(480, 419)
        Panel3.TabIndex = 31
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
        deviceflowpnl.Size = New Size(480, 419)
        deviceflowpnl.TabIndex = 18
        deviceflowpnl.WrapContents = False
        ' 
        ' assigntxt
        ' 
        assigntxt.BorderStyle = BorderStyle.FixedSingle
        assigntxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        assigntxt.Location = New Point(665, 48)
        assigntxt.Margin = New Padding(3, 2, 3, 2)
        assigntxt.Name = "assigntxt"
        assigntxt.Size = New Size(298, 26)
        assigntxt.TabIndex = 30
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(140, 49)
        unitnametxt.Margin = New Padding(3, 2, 3, 2)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(358, 26)
        unitnametxt.TabIndex = 29
        ' 
        ' specsflowpnl
        ' 
        specsflowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        specsflowpnl.AutoScroll = True
        specsflowpnl.BorderStyle = BorderStyle.FixedSingle
        specsflowpnl.FlowDirection = FlowDirection.TopDown
        specsflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsflowpnl.Location = New Point(524, 123)
        specsflowpnl.Margin = New Padding(3, 2, 3, 2)
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(480, 419)
        specsflowpnl.TabIndex = 28
        specsflowpnl.WrapContents = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(22, 96)
        Label4.Name = "Label4"
        Label4.Size = New Size(77, 18)
        Label4.TabIndex = 26
        Label4.Text = "Devices:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(534, 51)
        Label3.Name = "Label3"
        Label3.Size = New Size(107, 18)
        Label3.TabIndex = 25
        Label3.Text = "Assigned to:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(18, 53)
        Label2.Name = "Label2"
        Label2.Size = New Size(95, 18)
        Label2.TabIndex = 24
        Label2.Text = "Unit Name:"
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1024, 35)
        Panel1.TabIndex = 23
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(975, 5)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(31, 25)
        Panel2.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(6, 6)
        Label1.Name = "Label1"
        Label1.Size = New Size(230, 24)
        Label1.TabIndex = 1
        Label1.Text = "View Unit Information"
        ' 
        ' specsbtn
        ' 
        specsbtn.FlatAppearance.BorderSize = 2
        specsbtn.FlatStyle = FlatStyle.Flat
        specsbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsbtn.Location = New Point(524, 86)
        specsbtn.Margin = New Padding(3, 2, 3, 2)
        specsbtn.Name = "specsbtn"
        specsbtn.Size = New Size(480, 27)
        specsbtn.TabIndex = 35
        specsbtn.Text = "Specifications"
        specsbtn.UseVisualStyleBackColor = True
        ' 
        ' btnGenerateQR
        ' 
        btnGenerateQR.BackColor = Color.CornflowerBlue
        btnGenerateQR.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnGenerateQR.ForeColor = Color.White
        btnGenerateQR.Location = New Point(824, 551)
        btnGenerateQR.Margin = New Padding(3, 2, 3, 2)
        btnGenerateQR.Name = "btnGenerateQR"
        btnGenerateQR.Size = New Size(180, 36)
        btnGenerateQR.TabIndex = 36
        btnGenerateQR.Text = "Generate QR"
        btnGenerateQR.UseVisualStyleBackColor = False
        ' 
        ' mainpanelqr
        ' 
        mainpanelqr.BorderStyle = BorderStyle.FixedSingle
        mainpanelqr.Location = New Point(355, 96)
        mainpanelqr.Margin = New Padding(3, 2, 3, 2)
        mainpanelqr.Name = "mainpanelqr"
        mainpanelqr.Size = New Size(297, 320)
        mainpanelqr.TabIndex = 37
        mainpanelqr.Visible = False
        ' 
        ' printqrformat
        ' 
        printqrformat.BorderStyle = BorderStyle.FixedSingle
        printqrformat.Location = New Point(182, 51)
        printqrformat.Margin = New Padding(3, 2, 3, 2)
        printqrformat.Name = "printqrformat"
        printqrformat.Size = New Size(636, 510)
        printqrformat.TabIndex = 38
        printqrformat.Visible = False
        ' 
        ' ViewUnit
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(printqrformat)
        Controls.Add(mainpanelqr)
        Controls.Add(btnGenerateQR)
        Controls.Add(specsbtn)
        Controls.Add(Panel3)
        Controls.Add(assigntxt)
        Controls.Add(unitnametxt)
        Controls.Add(specsflowpnl)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "ViewUnit"
        Size = New Size(1024, 602)
        Panel3.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel3 As Panel
    Friend WithEvents deviceflowpnl As FlowLayoutPanel
    Friend WithEvents assigntxt As TextBox
    Friend WithEvents unitnametxt As TextBox
    Friend WithEvents specsflowpnl As FlowLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents specsbtn As Button
    Friend WithEvents btnGenerateQR As Button
    Friend WithEvents mainpanelqr As Panel
    Friend WithEvents printqrformat As Panel

End Class
