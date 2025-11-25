<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class View
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
        Label5 = New Label()
        Panel5 = New Panel()
        deviceflowpnl = New FlowLayoutPanel()
        Panel4 = New Panel()
        specsflowpnl = New FlowLayoutPanel()
        Label6 = New Label()
        Panel3 = New Panel()
        cancelbtn = New Button()
        savebtn = New Button()
        warrantyDatePicker = New DateTimePicker()
        purchaseDatePicker = New DateTimePicker()
        Label9 = New Label()
        Label1 = New Label()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label5.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(209, 6)
        Label5.Name = "Label5"
        Label5.Size = New Size(470, 49)
        Label5.TabIndex = 19
        Label5.Text = "View Device Information :"
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Panel5.Controls.Add(deviceflowpnl)
        Panel5.Location = New Point(24, 69)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(380, 469)
        Panel5.TabIndex = 25
        ' 
        ' deviceflowpnl
        ' 
        deviceflowpnl.Dock = DockStyle.Fill
        deviceflowpnl.Location = New Point(0, 0)
        deviceflowpnl.Name = "deviceflowpnl"
        deviceflowpnl.Size = New Size(380, 469)
        deviceflowpnl.TabIndex = 11
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Panel4.Controls.Add(specsflowpnl)
        Panel4.Controls.Add(Label6)
        Panel4.Location = New Point(422, 69)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(409, 469)
        Panel4.TabIndex = 26
        ' 
        ' specsflowpnl
        ' 
        specsflowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        specsflowpnl.AutoScroll = True
        specsflowpnl.BorderStyle = BorderStyle.FixedSingle
        specsflowpnl.FlowDirection = FlowDirection.TopDown
        specsflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsflowpnl.Location = New Point(12, 44)
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(385, 420)
        specsflowpnl.TabIndex = 18
        specsflowpnl.WrapContents = False
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(10, 13)
        Label6.Name = "Label6"
        Label6.Size = New Size(147, 28)
        Label6.TabIndex = 10
        Label6.Text = "Specifications :"
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel3.Controls.Add(cancelbtn)
        Panel3.Controls.Add(savebtn)
        Panel3.Controls.Add(warrantyDatePicker)
        Panel3.Controls.Add(purchaseDatePicker)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(24, 544)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(806, 189)
        Panel3.TabIndex = 27
        ' 
        ' cancelbtn
        ' 
        cancelbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cancelbtn.Location = New Point(532, 115)
        cancelbtn.Name = "cancelbtn"
        cancelbtn.Size = New Size(125, 48)
        cancelbtn.TabIndex = 14
        cancelbtn.Text = "CANCEL"
        cancelbtn.UseVisualStyleBackColor = True
        ' 
        ' savebtn
        ' 
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.Location = New Point(662, 112)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(125, 48)
        savebtn.TabIndex = 13
        savebtn.Text = "ADD"
        savebtn.UseVisualStyleBackColor = True
        ' 
        ' warrantyDatePicker
        ' 
        warrantyDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        warrantyDatePicker.Location = New Point(23, 123)
        warrantyDatePicker.Name = "warrantyDatePicker"
        warrantyDatePicker.Size = New Size(331, 30)
        warrantyDatePicker.TabIndex = 19
        ' 
        ' purchaseDatePicker
        ' 
        purchaseDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        purchaseDatePicker.Location = New Point(23, 48)
        purchaseDatePicker.Name = "purchaseDatePicker"
        purchaseDatePicker.Size = New Size(331, 30)
        purchaseDatePicker.TabIndex = 17
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(23, 88)
        Label9.Name = "Label9"
        Label9.Size = New Size(168, 28)
        Label9.TabIndex = 18
        Label9.Text = "Warranty Expires:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(26, 13)
        Label1.Name = "Label1"
        Label1.Size = New Size(165, 28)
        Label1.TabIndex = 17
        Label1.Text = "Purchased Date: "
        ' 
        ' View
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel3)
        Controls.Add(Label5)
        Controls.Add(Panel4)
        Controls.Add(Panel5)
        Name = "View"
        Size = New Size(857, 757)
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents deviceflowpnl As FlowLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents catcb As ComboBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents specsflowpnl As FlowLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents cancelbtn As Button
    Friend WithEvents savebtn As Button
    Friend WithEvents warrantyDatePicker As DateTimePicker
    Friend WithEvents purchaseDatePicker As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents Label1 As Label

End Class
