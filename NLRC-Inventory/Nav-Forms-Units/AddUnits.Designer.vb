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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel1 = New Panel()
        Panel2 = New Panel()
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
        Panel3 = New Panel()
        unitsdgv = New DataGridView()
        savebtn = New Button()
        Panel1.SuspendLayout()
        unit1pnl.SuspendLayout()
        Panel3.SuspendLayout()
        CType(unitsdgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label3)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1024, 41)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.AutoSize = True
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(967, 8)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(39, 32)
        Panel2.TabIndex = 30
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(19, 8)
        Label3.Name = "Label3"
        Label3.Size = New Size(227, 30)
        Label3.TabIndex = 8
        Label3.Text = "Add Asset Collection"
        ' 
        ' unit1pnl
        ' 
        unit1pnl.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        unit1pnl.Controls.Add(addbtn)
        unit1pnl.Controls.Add(remarktxt)
        unit1pnl.Controls.Add(Label5)
        unit1pnl.Controls.Add(Label4)
        unit1pnl.Controls.Add(Label2)
        unit1pnl.Controls.Add(Label1)
        unit1pnl.Controls.Add(devicecb)
        unit1pnl.Controls.Add(assigncb)
        unit1pnl.Controls.Add(unitnametxt)
        unit1pnl.Location = New Point(12, 56)
        unit1pnl.Margin = New Padding(3, 2, 3, 2)
        unit1pnl.Name = "unit1pnl"
        unit1pnl.Size = New Size(994, 174)
        unit1pnl.TabIndex = 1
        unit1pnl.Visible = False
        ' 
        ' addbtn
        ' 
        addbtn.BackColor = Color.CornflowerBlue
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(878, 122)
        addbtn.Margin = New Padding(3, 2, 3, 2)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(99, 41)
        addbtn.TabIndex = 15
        addbtn.Text = "Add"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' remarktxt
        ' 
        remarktxt.BorderStyle = BorderStyle.FixedSingle
        remarktxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        remarktxt.Location = New Point(618, 37)
        remarktxt.Margin = New Padding(3, 2, 3, 2)
        remarktxt.Multiline = True
        remarktxt.Name = "remarktxt"
        remarktxt.Size = New Size(360, 73)
        remarktxt.TabIndex = 14
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(618, 11)
        Label5.Name = "Label5"
        Label5.Size = New Size(84, 18)
        Label5.TabIndex = 13
        Label5.Text = "Remarks:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(24, 115)
        Label4.Name = "Label4"
        Label4.Size = New Size(190, 18)
        Label4.TabIndex = 12
        Label4.Text = "Devices / Components:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(24, 66)
        Label2.Name = "Label2"
        Label2.Size = New Size(172, 18)
        Label2.TabIndex = 11
        Label2.Text = "Assigned Personnel:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(24, 17)
        Label1.Name = "Label1"
        Label1.Size = New Size(99, 18)
        Label1.TabIndex = 10
        Label1.Text = "Unit Name: "
        ' 
        ' devicecb
        ' 
        devicecb.DropDownStyle = ComboBoxStyle.Simple
        devicecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(242, 108)
        devicecb.Margin = New Padding(3, 2, 3, 2)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(347, 26)
        devicecb.TabIndex = 9
        ' 
        ' assigncb
        ' 
        assigncb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        assigncb.FormattingEnabled = True
        assigncb.Location = New Point(242, 59)
        assigncb.Margin = New Padding(3, 2, 3, 2)
        assigncb.Name = "assigncb"
        assigncb.Size = New Size(347, 33)
        assigncb.TabIndex = 8
        ' 
        ' unitnametxt
        ' 
        unitnametxt.BorderStyle = BorderStyle.FixedSingle
        unitnametxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        unitnametxt.Location = New Point(242, 11)
        unitnametxt.Margin = New Padding(3, 2, 3, 2)
        unitnametxt.Name = "unitnametxt"
        unitnametxt.Size = New Size(347, 32)
        unitnametxt.TabIndex = 2
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel3.Controls.Add(unitsdgv)
        Panel3.Location = New Point(12, 255)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(994, 286)
        Panel3.TabIndex = 2
        ' 
        ' unitsdgv
        ' 
        unitsdgv.AllowUserToAddRows = False
        unitsdgv.AllowUserToDeleteRows = False
        unitsdgv.AllowUserToResizeColumns = False
        unitsdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        unitsdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        unitsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        unitsdgv.DefaultCellStyle = DataGridViewCellStyle2
        unitsdgv.Dock = DockStyle.Fill
        unitsdgv.Location = New Point(0, 0)
        unitsdgv.Margin = New Padding(3, 2, 3, 2)
        unitsdgv.Name = "unitsdgv"
        unitsdgv.ReadOnly = True
        unitsdgv.RowHeadersVisible = False
        unitsdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        unitsdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        unitsdgv.Size = New Size(994, 286)
        unitsdgv.TabIndex = 1
        ' 
        ' savebtn
        ' 
        savebtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        savebtn.BackColor = Color.CornflowerBlue
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.ForeColor = Color.White
        savebtn.Location = New Point(889, 550)
        savebtn.Margin = New Padding(3, 2, 3, 2)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(99, 41)
        savebtn.TabIndex = 16
        savebtn.Text = "Save"
        savebtn.UseVisualStyleBackColor = False
        ' 
        ' AddUnits
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(savebtn)
        Controls.Add(Panel3)
        Controls.Add(unit1pnl)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "AddUnits"
        Size = New Size(1024, 602)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        unit1pnl.ResumeLayout(False)
        unit1pnl.PerformLayout()
        Panel3.ResumeLayout(False)
        CType(unitsdgv, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Panel2 As Panel

End Class
