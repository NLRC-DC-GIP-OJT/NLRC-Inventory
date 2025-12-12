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
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel3 = New Panel()
        opstatuscb = New ComboBox()
        Label4 = New Label()
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
        Panel3.Controls.Add(opstatuscb)
        Panel3.Controls.Add(Label4)
        Panel3.Controls.Add(warrantyDatePicker)
        Panel3.Controls.Add(purchaseDatePicker)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(8, 531)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(381, 217)
        Panel3.TabIndex = 18
        ' 
        ' opstatuscb
        ' 
        opstatuscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        opstatuscb.FormattingEnabled = True
        opstatuscb.Location = New Point(129, 12)
        opstatuscb.Margin = New Padding(3, 2, 3, 2)
        opstatuscb.Name = "opstatuscb"
        opstatuscb.Size = New Size(144, 33)
        opstatuscb.TabIndex = 22
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(46, 18)
        Label4.Name = "Label4"
        Label4.Size = New Size(63, 21)
        Label4.TabIndex = 23
        Label4.Text = "Status: "
        ' 
        ' warrantyDatePicker
        ' 
        warrantyDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        warrantyDatePicker.Location = New Point(46, 153)
        warrantyDatePicker.Margin = New Padding(3, 2, 3, 2)
        warrantyDatePicker.Name = "warrantyDatePicker"
        warrantyDatePicker.Size = New Size(310, 26)
        warrantyDatePicker.TabIndex = 19
        ' 
        ' purchaseDatePicker
        ' 
        purchaseDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        purchaseDatePicker.Location = New Point(46, 89)
        purchaseDatePicker.Margin = New Padding(3, 2, 3, 2)
        purchaseDatePicker.Name = "purchaseDatePicker"
        purchaseDatePicker.Size = New Size(310, 26)
        purchaseDatePicker.TabIndex = 17
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(43, 126)
        Label9.Name = "Label9"
        Label9.Size = New Size(135, 21)
        Label9.TabIndex = 18
        Label9.Text = "Warranty Expires:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(46, 59)
        Label1.Name = "Label1"
        Label1.Size = New Size(131, 21)
        Label1.TabIndex = 17
        Label1.Text = "Purchased Date: "
        ' 
        ' cancelbtn
        ' 
        cancelbtn.BackColor = Color.SlateGray
        cancelbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cancelbtn.ForeColor = Color.White
        cancelbtn.Location = New Point(134, 239)
        cancelbtn.Margin = New Padding(3, 2, 3, 2)
        cancelbtn.Name = "cancelbtn"
        cancelbtn.Size = New Size(109, 46)
        cancelbtn.TabIndex = 14
        cancelbtn.Text = "Cancel"
        cancelbtn.UseVisualStyleBackColor = False
        ' 
        ' savebtn
        ' 
        savebtn.BackColor = Color.SlateGray
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(258, 239)
        savebtn.Margin = New Padding(3, 2, 3, 2)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(109, 46)
        savebtn.TabIndex = 13
        savebtn.Text = "Update"
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top
        Label5.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(208, 2)
        Label5.Name = "Label5"
        Label5.Size = New Size(463, 43)
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
        Panel2.Location = New Point(395, 45)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(437, 387)
        Panel2.TabIndex = 21
        ' 
        ' specsflowpnl
        ' 
        specsflowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        specsflowpnl.AutoScroll = True
        specsflowpnl.BorderStyle = BorderStyle.FixedSingle
        specsflowpnl.FlowDirection = FlowDirection.TopDown
        specsflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        specsflowpnl.Location = New Point(11, 154)
        specsflowpnl.Margin = New Padding(3, 2, 3, 2)
        specsflowpnl.Name = "specsflowpnl"
        specsflowpnl.Size = New Size(416, 228)
        specsflowpnl.TabIndex = 18
        specsflowpnl.WrapContents = False
        ' 
        ' notetxt
        ' 
        notetxt.BorderStyle = BorderStyle.FixedSingle
        notetxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        notetxt.Location = New Point(11, 31)
        notetxt.Margin = New Padding(3, 2, 3, 2)
        notetxt.Multiline = True
        notetxt.Name = "notetxt"
        notetxt.Size = New Size(416, 49)
        notetxt.TabIndex = 12
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(11, 3)
        Label7.Name = "Label7"
        Label7.Size = New Size(62, 21)
        Label7.TabIndex = 11
        Label7.Text = "Notes :"
        ' 
        ' specscb
        ' 
        specscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        specscb.FormattingEnabled = True
        specscb.Location = New Point(11, 115)
        specscb.Margin = New Padding(3, 2, 3, 2)
        specscb.Name = "specscb"
        specscb.Size = New Size(416, 33)
        specscb.TabIndex = 17
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(9, 86)
        Label6.Name = "Label6"
        Label6.Size = New Size(120, 21)
        Label6.TabIndex = 10
        Label6.Text = "Specifications :"
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Panel1.Controls.Add(Panel5)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(catcb)
        Panel1.Location = New Point(10, 45)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(379, 480)
        Panel1.TabIndex = 19
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel5.Controls.Add(deviceflowpnl)
        Panel5.Location = New Point(3, 80)
        Panel5.Margin = New Padding(3, 2, 3, 2)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(373, 398)
        Panel5.TabIndex = 23
        ' 
        ' deviceflowpnl
        ' 
        deviceflowpnl.Dock = DockStyle.Fill
        deviceflowpnl.Location = New Point(0, 0)
        deviceflowpnl.Margin = New Padding(3, 2, 3, 2)
        deviceflowpnl.Name = "deviceflowpnl"
        deviceflowpnl.Size = New Size(373, 398)
        deviceflowpnl.TabIndex = 11
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(24, 5)
        Label2.Name = "Label2"
        Label2.Size = New Size(86, 21)
        Label2.TabIndex = 4
        Label2.Text = "Category :"
        ' 
        ' catcb
        ' 
        catcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catcb.FormattingEnabled = True
        catcb.Location = New Point(62, 38)
        catcb.Margin = New Padding(3, 2, 3, 2)
        catcb.Name = "catcb"
        catcb.Size = New Size(249, 33)
        catcb.TabIndex = 5
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel4.Controls.Add(historydgv)
        Panel4.Controls.Add(savebtn)
        Panel4.Controls.Add(cancelbtn)
        Panel4.Location = New Point(399, 466)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(433, 294)
        Panel4.TabIndex = 20
        ' 
        ' historydgv
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = SystemColors.HighlightText
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle3.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True
        historydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        historydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        historydgv.DefaultCellStyle = DataGridViewCellStyle4
        historydgv.Location = New Point(0, 2)
        historydgv.Margin = New Padding(3, 2, 3, 2)
        historydgv.Name = "historydgv"
        historydgv.ReadOnly = True
        historydgv.RowHeadersVisible = False
        historydgv.RowHeadersWidth = 51
        historydgv.Size = New Size(431, 228)
        historydgv.TabIndex = 22
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(399, 438)
        Label3.Name = "Label3"
        Label3.Size = New Size(125, 21)
        Label3.TabIndex = 19
        Label3.Text = "Device History :"
        ' 
        ' Edit
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Label3)
        Controls.Add(Panel4)
        Controls.Add(Label5)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Edit"
        Size = New Size(845, 762)
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
    Friend WithEvents opstatuscb As ComboBox
    Friend WithEvents Label4 As Label

End Class
