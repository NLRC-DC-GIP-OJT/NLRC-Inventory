<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel3 = New Panel()
        warrantyDatePicker = New DateTimePicker()
        purchaseDatePicker = New DateTimePicker()
        Label9 = New Label()
        Label1 = New Label()
        cancelbtn = New Button()
        savebtn = New Button()
        Label5 = New Label()
        Panel2 = New Panel()
        specsflowpnl = New FlowLayoutPanel()
        notetxt = New TextBox()
        Label7 = New Label()
        specscb = New ComboBox()
        Label6 = New Label()
        Panel1 = New Panel()
        Panel5 = New Panel()
        deviceflowpnl = New FlowLayoutPanel()
        Label2 = New Label()
        catcb = New ComboBox()
        Panel4 = New Panel()
        historydgv = New DataGridView()
        Label3 = New Label()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        CType(historydgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel3.Controls.Add(warrantyDatePicker)
        Panel3.Controls.Add(purchaseDatePicker)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(14, 564)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(373, 179)
        Panel3.TabIndex = 18
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
        ' cancelbtn
        ' 
        cancelbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cancelbtn.Location = New Point(141, 236)
        cancelbtn.Name = "cancelbtn"
        cancelbtn.Size = New Size(125, 48)
        cancelbtn.TabIndex = 14
        cancelbtn.Text = "Cancel"
        cancelbtn.UseVisualStyleBackColor = True
        ' 
        ' savebtn
        ' 
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.Location = New Point(286, 237)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(125, 48)
        savebtn.TabIndex = 13
        savebtn.Text = "Update"
        savebtn.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top
        Label5.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(187, 3)
        Label5.Name = "Label5"
        Label5.Size = New Size(529, 57)
        Label5.TabIndex = 20
        Label5.Text = "Update Device Information"
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        Panel2.Controls.Add(specsflowpnl)
        Panel2.Controls.Add(notetxt)
        Panel2.Controls.Add(Label7)
        Panel2.Controls.Add(specscb)
        Panel2.Controls.Add(Label6)
        Panel2.Location = New Point(396, 60)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(454, 411)
        Panel2.TabIndex = 21
        ' 
        ' specsflowpnl
        ' 
        specsflowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        specsflowpnl.AutoScroll = True
        specsflowpnl.BorderStyle = BorderStyle.FixedSingle
        specsflowpnl.FlowDirection = FlowDirection.TopDown
        specsflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsflowpnl.Location = New Point(13, 205)
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(430, 198)
        specsflowpnl.TabIndex = 18
        specsflowpnl.WrapContents = False
        ' 
        ' notetxt
        ' 
        notetxt.BorderStyle = BorderStyle.FixedSingle
        notetxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        notetxt.Location = New Point(13, 41)
        notetxt.Multiline = True
        notetxt.Name = "notetxt"
        notetxt.Size = New Size(430, 65)
        notetxt.TabIndex = 12
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(13, 4)
        Label7.Name = "Label7"
        Label7.Size = New Size(77, 28)
        Label7.TabIndex = 11
        Label7.Text = "Notes :"
        ' 
        ' specscb
        ' 
        specscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        specscb.FormattingEnabled = True
        specscb.Location = New Point(13, 153)
        specscb.Name = "specscb"
        specscb.Size = New Size(430, 39)
        specscb.TabIndex = 17
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(10, 114)
        Label6.Name = "Label6"
        Label6.Size = New Size(147, 28)
        Label6.TabIndex = 10
        Label6.Text = "Specifications :"
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Panel1.Controls.Add(Panel5)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(catcb)
        Panel1.Location = New Point(11, 60)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(379, 475)
        Panel1.TabIndex = 19
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel5.Controls.Add(deviceflowpnl)
        Panel5.Location = New Point(3, 106)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(373, 366)
        Panel5.TabIndex = 23
        ' 
        ' deviceflowpnl
        ' 
        deviceflowpnl.Dock = DockStyle.Fill
        deviceflowpnl.Location = New Point(0, 0)
        deviceflowpnl.Name = "deviceflowpnl"
        deviceflowpnl.Size = New Size(373, 366)
        deviceflowpnl.TabIndex = 11
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(27, 7)
        Label2.Name = "Label2"
        Label2.Size = New Size(105, 28)
        Label2.TabIndex = 4
        Label2.Text = "Category :"
        ' 
        ' catcb
        ' 
        catcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catcb.FormattingEnabled = True
        catcb.Location = New Point(50, 50)
        catcb.Name = "catcb"
        catcb.Size = New Size(284, 39)
        catcb.TabIndex = 5
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel4.Controls.Add(historydgv)
        Panel4.Controls.Add(savebtn)
        Panel4.Controls.Add(cancelbtn)
        Panel4.Location = New Point(396, 517)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(454, 299)
        Panel4.TabIndex = 20
        ' 
        ' historydgv
        ' 
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.HighlightText
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle1.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        historydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        historydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        historydgv.DefaultCellStyle = DataGridViewCellStyle2
        historydgv.Location = New Point(3, 3)
        historydgv.Name = "historydgv"
        historydgv.ReadOnly = True
        historydgv.RowHeadersVisible = False
        historydgv.RowHeadersWidth = 51
        historydgv.Size = New Size(448, 222)
        historydgv.TabIndex = 22
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(406, 479)
        Label3.Name = "Label3"
        Label3.Size = New Size(155, 28)
        Label3.TabIndex = 19
        Label3.Text = "Device History :"
        ' 
        ' Edit
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Label3)
        Controls.Add(Panel4)
        Controls.Add(Label5)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        Name = "Edit"
        Size = New Size(864, 820)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        CType(historydgv, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Panel3 As Panel
    Friend WithEvents warrantyDatePicker As DateTimePicker
    Friend WithEvents purchaseDatePicker As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cancelbtn As Button
    Friend WithEvents savebtn As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents specsflowpnl As FlowLayoutPanel
    Friend WithEvents notetxt As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents specscb As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents deviceflowpnl As FlowLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents catcb As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents historydgv As DataGridView
    Friend WithEvents Label3 As Label

End Class
