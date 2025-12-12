<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddNew))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel1 = New Panel()
        Panel4 = New Panel()
        Label3 = New Label()
        unit2pnl = New Panel()
        addbtn1 = New Button()
        devicestocklbl = New Label()
        checkstocklbl = New Label()
        Label9 = New Label()
        catecb = New ComboBox()
        Label8 = New Label()
        devicecb1 = New ComboBox()
        remarktxt1 = New TextBox()
        Label7 = New Label()
        Panel5 = New Panel()
        quantitxt = New TextBox()
        minusQuantityBtn = New Button()
        addQuantityBtn = New Button()
        Label6 = New Label()
        Panel2 = New Panel()
        unitsdgv1 = New DataGridView()
        Panel3 = New Panel()
        savebtn1 = New Button()
        Panel1.SuspendLayout()
        unit2pnl.SuspendLayout()
        Panel5.SuspendLayout()
        Panel2.SuspendLayout()
        CType(unitsdgv1, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Controls.Add(Panel4)
        Panel1.Controls.Add(Label3)
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1024, 46)
        Panel1.TabIndex = 1
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Transparent
        Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), Image)
        Panel4.BackgroundImageLayout = ImageLayout.Stretch
        Panel4.Location = New Point(975, 9)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(31, 25)
        Panel4.TabIndex = 30
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(19, 8)
        Label3.Name = "Label3"
        Label3.Size = New Size(283, 30)
        Label3.TabIndex = 8
        Label3.Text = "CREATE MULTIPLE ASSETS"
        ' 
        ' unit2pnl
        ' 
        unit2pnl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        unit2pnl.Controls.Add(addbtn1)
        unit2pnl.Controls.Add(devicestocklbl)
        unit2pnl.Controls.Add(checkstocklbl)
        unit2pnl.Controls.Add(Label9)
        unit2pnl.Controls.Add(catecb)
        unit2pnl.Controls.Add(Label8)
        unit2pnl.Controls.Add(devicecb1)
        unit2pnl.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        unit2pnl.Location = New Point(13, 50)
        unit2pnl.Margin = New Padding(3, 2, 3, 2)
        unit2pnl.Name = "unit2pnl"
        unit2pnl.Size = New Size(996, 116)
        unit2pnl.TabIndex = 19
        unit2pnl.Visible = False
        ' 
        ' addbtn1
        ' 
        addbtn1.BackColor = Color.SlateGray
        addbtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn1.ForeColor = Color.White
        addbtn1.Location = New Point(881, 69)
        addbtn1.Margin = New Padding(3, 2, 3, 2)
        addbtn1.Name = "addbtn1"
        addbtn1.Size = New Size(99, 41)
        addbtn1.TabIndex = 15
        addbtn1.Text = "Add"
        addbtn1.UseVisualStyleBackColor = False
        ' 
        ' devicestocklbl
        ' 
        devicestocklbl.AutoSize = True
        devicestocklbl.ForeColor = Color.RoyalBlue
        devicestocklbl.Location = New Point(513, 72)
        devicestocklbl.Name = "devicestocklbl"
        devicestocklbl.Size = New Size(128, 21)
        devicestocklbl.TabIndex = 29
        devicestocklbl.Text = "How many stock"
        ' 
        ' checkstocklbl
        ' 
        checkstocklbl.AutoSize = True
        checkstocklbl.ForeColor = Color.RoyalBlue
        checkstocklbl.Location = New Point(23, 72)
        checkstocklbl.Name = "checkstocklbl"
        checkstocklbl.Size = New Size(128, 21)
        checkstocklbl.TabIndex = 28
        checkstocklbl.Text = "How many stock"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(24, 10)
        Label9.Name = "Label9"
        Label9.Size = New Size(87, 18)
        Label9.TabIndex = 27
        Label9.Text = "Category:"
        ' 
        ' catecb
        ' 
        catecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catecb.FormattingEnabled = True
        catecb.Location = New Point(24, 34)
        catecb.Margin = New Padding(3, 2, 3, 2)
        catecb.Name = "catecb"
        catecb.Size = New Size(447, 33)
        catecb.TabIndex = 26
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(513, 10)
        Label8.Name = "Label8"
        Label8.Size = New Size(190, 18)
        Label8.TabIndex = 12
        Label8.Text = "Devices / Components:"
        ' 
        ' devicecb1
        ' 
        devicecb1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb1.FormattingEnabled = True
        devicecb1.Location = New Point(513, 34)
        devicecb1.Margin = New Padding(3, 2, 3, 2)
        devicecb1.Name = "devicecb1"
        devicecb1.Size = New Size(468, 33)
        devicecb1.TabIndex = 9
        ' 
        ' remarktxt1
        ' 
        remarktxt1.BorderStyle = BorderStyle.FixedSingle
        remarktxt1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        remarktxt1.Location = New Point(452, 10)
        remarktxt1.Margin = New Padding(3, 2, 3, 2)
        remarktxt1.Multiline = True
        remarktxt1.Name = "remarktxt1"
        remarktxt1.Size = New Size(404, 54)
        remarktxt1.TabIndex = 14
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(335, 23)
        Label7.Name = "Label7"
        Label7.Size = New Size(84, 18)
        Label7.TabIndex = 13
        Label7.Text = "Remarks:"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.White
        Panel5.Controls.Add(quantitxt)
        Panel5.Controls.Add(minusQuantityBtn)
        Panel5.Controls.Add(addQuantityBtn)
        Panel5.Location = New Point(108, 13)
        Panel5.Margin = New Padding(3, 2, 3, 2)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(200, 43)
        Panel5.TabIndex = 25
        ' 
        ' quantitxt
        ' 
        quantitxt.BorderStyle = BorderStyle.FixedSingle
        quantitxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        quantitxt.Location = New Point(52, 7)
        quantitxt.Margin = New Padding(3, 2, 3, 2)
        quantitxt.Name = "quantitxt"
        quantitxt.Size = New Size(93, 32)
        quantitxt.TabIndex = 21
        quantitxt.TextAlign = HorizontalAlignment.Center
        ' 
        ' minusQuantityBtn
        ' 
        minusQuantityBtn.BackgroundImage = CType(resources.GetObject("minusQuantityBtn.BackgroundImage"), Image)
        minusQuantityBtn.BackgroundImageLayout = ImageLayout.Stretch
        minusQuantityBtn.FlatAppearance.BorderSize = 0
        minusQuantityBtn.FlatStyle = FlatStyle.Flat
        minusQuantityBtn.Location = New Point(8, 7)
        minusQuantityBtn.Margin = New Padding(3, 2, 3, 2)
        minusQuantityBtn.Name = "minusQuantityBtn"
        minusQuantityBtn.Size = New Size(34, 28)
        minusQuantityBtn.TabIndex = 24
        minusQuantityBtn.UseVisualStyleBackColor = True
        ' 
        ' addQuantityBtn
        ' 
        addQuantityBtn.BackgroundImage = CType(resources.GetObject("addQuantityBtn.BackgroundImage"), Image)
        addQuantityBtn.BackgroundImageLayout = ImageLayout.Zoom
        addQuantityBtn.FlatAppearance.BorderSize = 0
        addQuantityBtn.FlatStyle = FlatStyle.Flat
        addQuantityBtn.Location = New Point(155, 6)
        addQuantityBtn.Margin = New Padding(3, 2, 3, 2)
        addQuantityBtn.Name = "addQuantityBtn"
        addQuantityBtn.Size = New Size(32, 28)
        addQuantityBtn.TabIndex = 23
        addQuantityBtn.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(6, 26)
        Label6.Name = "Label6"
        Label6.Size = New Size(81, 18)
        Label6.TabIndex = 16
        Label6.Text = "Quantity:"
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.Controls.Add(unitsdgv1)
        Panel2.Location = New Point(13, 170)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(996, 331)
        Panel2.TabIndex = 20
        ' 
        ' unitsdgv1
        ' 
        unitsdgv1.AllowUserToAddRows = False
        unitsdgv1.AllowUserToDeleteRows = False
        unitsdgv1.AllowUserToResizeColumns = False
        unitsdgv1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        unitsdgv1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        unitsdgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        unitsdgv1.DefaultCellStyle = DataGridViewCellStyle2
        unitsdgv1.Dock = DockStyle.Fill
        unitsdgv1.Location = New Point(0, 0)
        unitsdgv1.Margin = New Padding(3, 2, 3, 2)
        unitsdgv1.Name = "unitsdgv1"
        unitsdgv1.ReadOnly = True
        unitsdgv1.RowHeadersVisible = False
        unitsdgv1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        unitsdgv1.RowsDefaultCellStyle = DataGridViewCellStyle3
        unitsdgv1.Size = New Size(996, 331)
        unitsdgv1.TabIndex = 2
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(savebtn1)
        Panel3.Controls.Add(Label6)
        Panel3.Controls.Add(Panel5)
        Panel3.Controls.Add(Label7)
        Panel3.Controls.Add(remarktxt1)
        Panel3.Location = New Point(13, 512)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(993, 74)
        Panel3.TabIndex = 21
        ' 
        ' savebtn1
        ' 
        savebtn1.BackColor = Color.SlateGray
        savebtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn1.ForeColor = Color.White
        savebtn1.Location = New Point(881, 14)
        savebtn1.Margin = New Padding(3, 2, 3, 2)
        savebtn1.Name = "savebtn1"
        savebtn1.Size = New Size(99, 41)
        savebtn1.TabIndex = 16
        savebtn1.Text = "Save"
        savebtn1.UseVisualStyleBackColor = False
        ' 
        ' AddNew
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(unit2pnl)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "AddNew"
        Size = New Size(1024, 602)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        unit2pnl.ResumeLayout(False)
        unit2pnl.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(unitsdgv1, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents unit2pnl As Panel
    Friend WithEvents devicestocklbl As Label
    Friend WithEvents checkstocklbl As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents catecb As ComboBox
    Friend WithEvents Panel5 As Panel
    Friend WithEvents quantitxt As TextBox
    Friend WithEvents minusQuantityBtn As Button
    Friend WithEvents addQuantityBtn As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents addbtn1 As Button
    Friend WithEvents remarktxt1 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents devicecb1 As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents savebtn1 As Button
    Friend WithEvents unitsdgv1 As DataGridView
    Friend WithEvents Panel4 As Panel

End Class
