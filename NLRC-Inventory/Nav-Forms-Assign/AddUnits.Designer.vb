<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddUnits
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddUnits))
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel1 = New Panel()
        Label3 = New Label()
        unit1pnl = New Panel()
        addbtn = New Button()
        remarktxt = New TextBox()
        Label5 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        devicecb = New ComboBox()
        assigncb = New ComboBox()
        unitnametxt = New TextBox()
        unit2pnl = New Panel()
        devicestocklbl = New Label()
        checkstocklbl = New Label()
        Label9 = New Label()
        catecb = New ComboBox()
        Panel5 = New Panel()
        quantitxt = New TextBox()
        minusQuantityBtn = New Button()
        addQuantityBtn = New Button()
        Label6 = New Label()
        addbtn1 = New Button()
        remarktxt1 = New TextBox()
        Label7 = New Label()
        Label8 = New Label()
        devicecb1 = New ComboBox()
        Panel3 = New Panel()
        unitsdgv = New DataGridView()
        savebtn = New Button()
        savepnl1 = New Panel()
        savebtn1 = New Button()
        Panel1.SuspendLayout()
        unit1pnl.SuspendLayout()
        unit2pnl.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        CType(unitsdgv, ComponentModel.ISupportInitialize).BeginInit()
        savepnl1.SuspendLayout()
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
        Panel1.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(22, 11)
        Label3.Name = "Label3"
        Label3.Size = New Size(135, 38)
        Label3.TabIndex = 8
        Label3.Text = "Add Unit"
        ' 
        ' unit1pnl
        ' 
        unit1pnl.Controls.Add(addbtn)
        unit1pnl.Controls.Add(remarktxt)
        unit1pnl.Controls.Add(Label5)
        unit1pnl.Controls.Add(Label4)
        unit1pnl.Controls.Add(Label2)
        unit1pnl.Controls.Add(Label1)
        unit1pnl.Controls.Add(devicecb)
        unit1pnl.Controls.Add(assigncb)
        unit1pnl.Controls.Add(unitnametxt)
        unit1pnl.Location = New Point(14, 73)
        unit1pnl.Name = "unit1pnl"
        unit1pnl.Size = New Size(1136, 241)
        unit1pnl.TabIndex = 1
        unit1pnl.Visible = False
        ' 
        ' addbtn
        ' 
        addbtn.BackColor = Color.CornflowerBlue
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(1005, 170)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(113, 55)
        addbtn.TabIndex = 15
        addbtn.Text = "Add"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' remarktxt
        ' 
        remarktxt.BorderStyle = BorderStyle.FixedSingle
        remarktxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        remarktxt.Location = New Point(590, 67)
        remarktxt.Multiline = True
        remarktxt.Name = "remarktxt"
        remarktxt.Size = New Size(527, 97)
        remarktxt.TabIndex = 14
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(590, 32)
        Label5.Name = "Label5"
        Label5.Size = New Size(103, 23)
        Label5.TabIndex = 13
        Label5.Text = "Remarks:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(27, 173)
        Label4.Name = "Label4"
        Label4.Size = New Size(234, 23)
        Label4.TabIndex = 12
        Label4.Text = "Devices / Components:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(27, 105)
        Label2.Name = "Label2"
        Label2.Size = New Size(210, 23)
        Label2.TabIndex = 11
        Label2.Text = "Assigned Personnel:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(27, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(122, 23)
        Label1.TabIndex = 10
        Label1.Text = "Unit Name: "
        ' 
        ' devicecb
        ' 
        devicecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(277, 164)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(250, 39)
        devicecb.TabIndex = 9
        ' 
        ' assigncb
        ' 
        assigncb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        assigncb.FormattingEnabled = True
        assigncb.Location = New Point(277, 92)
        assigncb.Name = "assigncb"
        assigncb.Size = New Size(250, 39)
        assigncb.TabIndex = 8
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(277, 24)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(248, 38)
        unitnametxt.TabIndex = 2
        ' 
        ' unit2pnl
        ' 
        unit2pnl.Controls.Add(devicestocklbl)
        unit2pnl.Controls.Add(checkstocklbl)
        unit2pnl.Controls.Add(Label9)
        unit2pnl.Controls.Add(catecb)
        unit2pnl.Controls.Add(Panel5)
        unit2pnl.Controls.Add(Label6)
        unit2pnl.Controls.Add(addbtn1)
        unit2pnl.Controls.Add(remarktxt1)
        unit2pnl.Controls.Add(Label7)
        unit2pnl.Controls.Add(Label8)
        unit2pnl.Controls.Add(devicecb1)
        unit2pnl.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        unit2pnl.Location = New Point(14, 73)
        unit2pnl.Name = "unit2pnl"
        unit2pnl.Size = New Size(1136, 258)
        unit2pnl.TabIndex = 18
        unit2pnl.Visible = False
        ' 
        ' devicestocklbl
        ' 
        devicestocklbl.AutoSize = True
        devicestocklbl.ForeColor = Color.RoyalBlue
        devicestocklbl.Location = New Point(26, 217)
        devicestocklbl.Name = "devicestocklbl"
        devicestocklbl.Size = New Size(160, 28)
        devicestocklbl.TabIndex = 29
        devicestocklbl.Text = "How many stock"
        ' 
        ' checkstocklbl
        ' 
        checkstocklbl.AutoSize = True
        checkstocklbl.ForeColor = Color.RoyalBlue
        checkstocklbl.Location = New Point(28, 92)
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
        catecb.Location = New Point(27, 45)
        catecb.Name = "catecb"
        catecb.Size = New Size(436, 39)
        catecb.TabIndex = 26
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.White
        Panel5.Controls.Add(quantitxt)
        Panel5.Controls.Add(minusQuantityBtn)
        Panel5.Controls.Add(addQuantityBtn)
        Panel5.Location = New Point(711, 10)
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
        Label6.Location = New Point(585, 26)
        Label6.Name = "Label6"
        Label6.Size = New Size(98, 23)
        Label6.TabIndex = 16
        Label6.Text = "Quantity:"
        ' 
        ' addbtn1
        ' 
        addbtn1.BackColor = Color.CornflowerBlue
        addbtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn1.ForeColor = Color.White
        addbtn1.Location = New Point(998, 198)
        addbtn1.Name = "addbtn1"
        addbtn1.Size = New Size(113, 55)
        addbtn1.TabIndex = 15
        addbtn1.Text = "Add"
        addbtn1.UseVisualStyleBackColor = False
        ' 
        ' remarktxt1
        ' 
        remarktxt1.BorderStyle = BorderStyle.FixedSingle
        remarktxt1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        remarktxt1.Location = New Point(590, 115)
        remarktxt1.Multiline = True
        remarktxt1.Name = "remarktxt1"
        remarktxt1.Size = New Size(527, 77)
        remarktxt1.TabIndex = 14
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(587, 77)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 23)
        Label7.TabIndex = 13
        Label7.Text = "Remarks:"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(28, 134)
        Label8.Name = "Label8"
        Label8.Size = New Size(234, 23)
        Label8.TabIndex = 12
        Label8.Text = "Devices / Components:"
        ' 
        ' devicecb1
        ' 
        devicecb1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb1.FormattingEnabled = True
        devicecb1.Location = New Point(28, 170)
        devicecb1.Name = "devicecb1"
        devicecb1.Size = New Size(436, 39)
        devicecb1.TabIndex = 9
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(unitsdgv)
        Panel3.Location = New Point(14, 337)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1136, 381)
        Panel3.TabIndex = 2
        ' 
        ' unitsdgv
        ' 
        unitsdgv.AllowUserToAddRows = False
        unitsdgv.AllowUserToDeleteRows = False
        unitsdgv.AllowUserToResizeColumns = False
        unitsdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle4.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        unitsdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        unitsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        unitsdgv.DefaultCellStyle = DataGridViewCellStyle5
        unitsdgv.Dock = DockStyle.Fill
        unitsdgv.Location = New Point(0, 0)
        unitsdgv.Name = "unitsdgv"
        unitsdgv.ReadOnly = True
        unitsdgv.RowHeadersVisible = False
        unitsdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        unitsdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        unitsdgv.Size = New Size(1136, 381)
        unitsdgv.TabIndex = 1
        ' 
        ' savebtn
        ' 
        savebtn.BackColor = Color.CornflowerBlue
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(1016, 731)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(113, 55)
        savebtn.TabIndex = 16
        savebtn.Text = "Save"
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' savepnl1
        ' 
        savepnl1.BackColor = Color.Transparent
        savepnl1.Controls.Add(savebtn1)
        savepnl1.Location = New Point(997, 731)
        savepnl1.Name = "savepnl1"
        savepnl1.Size = New Size(135, 59)
        savepnl1.TabIndex = 19
        savepnl1.Visible = False
        ' 
        ' savebtn1
        ' 
        savebtn1.BackColor = Color.CornflowerBlue
        savebtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn1.ForeColor = Color.White
        savebtn1.Location = New Point(11, 2)
        savebtn1.Name = "savebtn1"
        savebtn1.Size = New Size(113, 55)
        savebtn1.TabIndex = 20
        savebtn1.Text = "Save"
        savebtn1.UseVisualStyleBackColor = False
        ' 
        ' AddUnits
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(savepnl1)
        Controls.Add(savebtn)
        Controls.Add(unit2pnl)
        Controls.Add(Panel3)
        Controls.Add(unit1pnl)
        Controls.Add(Panel1)
        Name = "AddUnits"
        Size = New Size(1167, 799)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        unit1pnl.ResumeLayout(False)
        unit1pnl.PerformLayout()
        unit2pnl.ResumeLayout(False)
        unit2pnl.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel3.ResumeLayout(False)
        CType(unitsdgv, ComponentModel.ISupportInitialize).EndInit()
        savepnl1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents unit1pnl As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents unitnametxt As TextBox
    Friend WithEvents remarktxt As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents devicecb As ComboBox
    Friend WithEvents assigncb As ComboBox
    Friend WithEvents addbtn As Button
    Friend WithEvents unitsdgv As DataGridView
    Friend WithEvents savebtn As Button
    Friend WithEvents unit2pnl As Panel
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
    Friend WithEvents Label9 As Label
    Friend WithEvents catecb As ComboBox
    Friend WithEvents checkstocklbl As Label
    Friend WithEvents devicestocklbl As Label
    Friend WithEvents savepnl1 As Panel
    Friend WithEvents savebtn1 As Button

End Class
