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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel1 = New Panel()
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
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Controls.Add(Label3)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1167, 61)
        Panel1.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(22, 11)
        Label3.Name = "Label3"
        Label3.Size = New Size(345, 38)
        Label3.TabIndex = 8
        Label3.Text = "CREATE MULTIPLE UNITS"
        ' 
        ' unit2pnl
        ' 
        unit2pnl.Controls.Add(addbtn1)
        unit2pnl.Controls.Add(devicestocklbl)
        unit2pnl.Controls.Add(checkstocklbl)
        unit2pnl.Controls.Add(Label9)
        unit2pnl.Controls.Add(catecb)
        unit2pnl.Controls.Add(Label8)
        unit2pnl.Controls.Add(devicecb1)
        unit2pnl.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        unit2pnl.Location = New Point(15, 67)
        unit2pnl.Name = "unit2pnl"
        unit2pnl.Size = New Size(1135, 154)
        unit2pnl.TabIndex = 19
        unit2pnl.Visible = False
        ' 
        ' addbtn1
        ' 
        addbtn1.BackColor = Color.CornflowerBlue
        addbtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn1.ForeColor = Color.White
        addbtn1.Location = New Point(1007, 92)
        addbtn1.Name = "addbtn1"
        addbtn1.Size = New Size(113, 55)
        addbtn1.TabIndex = 15
        addbtn1.Text = "Add"
        addbtn1.UseVisualStyleBackColor = False
        ' 
        ' devicestocklbl
        ' 
        devicestocklbl.AutoSize = True
        devicestocklbl.ForeColor = Color.RoyalBlue
        devicestocklbl.Location = New Point(586, 96)
        devicestocklbl.Name = "devicestocklbl"
        devicestocklbl.Size = New Size(160, 28)
        devicestocklbl.TabIndex = 29
        devicestocklbl.Text = "How many stock"
        ' 
        ' checkstocklbl
        ' 
        checkstocklbl.AutoSize = True
        checkstocklbl.ForeColor = Color.RoyalBlue
        checkstocklbl.Location = New Point(26, 96)
        checkstocklbl.Name = "checkstocklbl"
        checkstocklbl.Size = New Size(160, 28)
        checkstocklbl.TabIndex = 28
        checkstocklbl.Text = "How many stock"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(27, 13)
        Label9.Name = "Label9"
        Label9.Size = New Size(107, 23)
        Label9.TabIndex = 27
        Label9.Text = "Category:"
        ' 
        ' catecb
        ' 
        catecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catecb.FormattingEnabled = True
        catecb.Location = New Point(27, 46)
        catecb.Name = "catecb"
        catecb.Size = New Size(510, 39)
        catecb.TabIndex = 26
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(586, 13)
        Label8.Name = "Label8"
        Label8.Size = New Size(234, 23)
        Label8.TabIndex = 12
        Label8.Text = "Devices / Components:"
        ' 
        ' devicecb1
        ' 
        devicecb1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb1.FormattingEnabled = True
        devicecb1.Location = New Point(586, 46)
        devicecb1.Name = "devicecb1"
        devicecb1.Size = New Size(534, 39)
        devicecb1.TabIndex = 9
        ' 
        ' remarktxt1
        ' 
        remarktxt1.BorderStyle = BorderStyle.FixedSingle
        remarktxt1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        remarktxt1.Location = New Point(517, 13)
        remarktxt1.Multiline = True
        remarktxt1.Name = "remarktxt1"
        remarktxt1.Size = New Size(462, 71)
        remarktxt1.TabIndex = 14
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(383, 31)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 23)
        Label7.TabIndex = 13
        Label7.Text = "Remarks:"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.White
        Panel5.Controls.Add(quantitxt)
        Panel5.Controls.Add(minusQuantityBtn)
        Panel5.Controls.Add(addQuantityBtn)
        Panel5.Location = New Point(124, 17)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(229, 57)
        Panel5.TabIndex = 25
        ' 
        ' quantitxt
        ' 
        quantitxt.BorderStyle = BorderStyle.FixedSingle
        quantitxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        quantitxt.Location = New Point(59, 9)
        quantitxt.Name = "quantitxt"
        quantitxt.Size = New Size(106, 38)
        quantitxt.TabIndex = 21
        quantitxt.TextAlign = HorizontalAlignment.Center
        ' 
        ' minusQuantityBtn
        ' 
        minusQuantityBtn.BackgroundImage = CType(resources.GetObject("minusQuantityBtn.BackgroundImage"), Image)
        minusQuantityBtn.BackgroundImageLayout = ImageLayout.Stretch
        minusQuantityBtn.FlatAppearance.BorderSize = 0
        minusQuantityBtn.FlatStyle = FlatStyle.Flat
        minusQuantityBtn.Location = New Point(9, 9)
        minusQuantityBtn.Name = "minusQuantityBtn"
        minusQuantityBtn.Size = New Size(39, 38)
        minusQuantityBtn.TabIndex = 24
        minusQuantityBtn.UseVisualStyleBackColor = True
        ' 
        ' addQuantityBtn
        ' 
        addQuantityBtn.BackgroundImage = CType(resources.GetObject("addQuantityBtn.BackgroundImage"), Image)
        addQuantityBtn.BackgroundImageLayout = ImageLayout.Zoom
        addQuantityBtn.FlatAppearance.BorderSize = 0
        addQuantityBtn.FlatStyle = FlatStyle.Flat
        addQuantityBtn.Location = New Point(177, 8)
        addQuantityBtn.Name = "addQuantityBtn"
        addQuantityBtn.Size = New Size(36, 38)
        addQuantityBtn.TabIndex = 23
        addQuantityBtn.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(7, 34)
        Label6.Name = "Label6"
        Label6.Size = New Size(98, 23)
        Label6.TabIndex = 16
        Label6.Text = "Quantity:"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(unitsdgv1)
        Panel2.Location = New Point(15, 227)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1135, 464)
        Panel2.TabIndex = 20
        ' 
        ' unitsdgv1
        ' 
        unitsdgv1.AllowUserToAddRows = False
        unitsdgv1.AllowUserToDeleteRows = False
        unitsdgv1.AllowUserToResizeColumns = False
        unitsdgv1.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle4.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        unitsdgv1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        unitsdgv1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        unitsdgv1.DefaultCellStyle = DataGridViewCellStyle5
        unitsdgv1.Dock = DockStyle.Fill
        unitsdgv1.Location = New Point(0, 0)
        unitsdgv1.Name = "unitsdgv1"
        unitsdgv1.ReadOnly = True
        unitsdgv1.RowHeadersVisible = False
        unitsdgv1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        unitsdgv1.RowsDefaultCellStyle = DataGridViewCellStyle6
        unitsdgv1.Size = New Size(1135, 464)
        unitsdgv1.TabIndex = 2
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(savebtn1)
        Panel3.Controls.Add(Label6)
        Panel3.Controls.Add(Panel5)
        Panel3.Controls.Add(Label7)
        Panel3.Controls.Add(remarktxt1)
        Panel3.Location = New Point(15, 697)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1135, 99)
        Panel3.TabIndex = 21
        ' 
        ' savebtn1
        ' 
        savebtn1.BackColor = Color.CornflowerBlue
        savebtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn1.ForeColor = Color.White
        savebtn1.Location = New Point(1007, 19)
        savebtn1.Name = "savebtn1"
        savebtn1.Size = New Size(113, 55)
        savebtn1.TabIndex = 16
        savebtn1.Text = "Save"
        savebtn1.UseVisualStyleBackColor = False
        ' 
        ' AddNew
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(unit2pnl)
        Controls.Add(Panel1)
        Name = "AddNew"
        Size = New Size(1167, 799)
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

End Class
