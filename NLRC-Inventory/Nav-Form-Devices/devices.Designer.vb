<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class devices
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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(devices))
        Panel1 = New Panel()
        devicesdgv = New DataGridView()
        addbtn = New Button()
        filtertxt = New TextBox()
        statuscb = New ComboBox()
        Panel2 = New Panel()
        catecb = New ComboBox()
        brandscb = New ComboBox()
        Label3 = New Label()
        lblPageInfo = New Label()
        btnPrev = New Button()
        btnNext = New Button()
        addpnl = New Panel()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        CType(devicesdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.Controls.Add(devicesdgv)
        Panel1.Location = New Point(15, 124)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1318, 743)
        Panel1.TabIndex = 7
        ' 
        ' devicesdgv
        ' 
        devicesdgv.AllowUserToAddRows = False
        devicesdgv.AllowUserToDeleteRows = False
        devicesdgv.AllowUserToResizeColumns = False
        devicesdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle4.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        devicesdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        devicesdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        devicesdgv.DefaultCellStyle = DataGridViewCellStyle5
        devicesdgv.Dock = DockStyle.Fill
        devicesdgv.Location = New Point(0, 0)
        devicesdgv.Name = "devicesdgv"
        devicesdgv.ReadOnly = True
        devicesdgv.RowHeadersVisible = False
        devicesdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        devicesdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        devicesdgv.Size = New Size(1318, 743)
        devicesdgv.TabIndex = 0
        ' 
        ' addbtn
        ' 
        addbtn.BackColor = Color.CornflowerBlue
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(1149, 11)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(139, 55)
        addbtn.TabIndex = 8
        addbtn.Text = "Add Device"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' filtertxt
        ' 
        filtertxt.BorderStyle = BorderStyle.FixedSingle
        filtertxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        filtertxt.Location = New Point(840, 16)
        filtertxt.Name = "filtertxt"
        filtertxt.Size = New Size(301, 38)
        filtertxt.TabIndex = 9
        ' 
        ' statuscb
        ' 
        statuscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        statuscb.FormattingEnabled = True
        statuscb.Location = New Point(551, 17)
        statuscb.Name = "statuscb"
        statuscb.Size = New Size(199, 39)
        statuscb.TabIndex = 12
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(addbtn)
        Panel2.Controls.Add(filtertxt)
        Panel2.Controls.Add(catecb)
        Panel2.Controls.Add(brandscb)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(statuscb)
        Panel2.Location = New Point(15, 24)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1314, 77)
        Panel2.TabIndex = 13
        ' 
        ' catecb
        ' 
        catecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catecb.FormattingEnabled = True
        catecb.Location = New Point(152, 16)
        catecb.Name = "catecb"
        catecb.Size = New Size(183, 39)
        catecb.TabIndex = 14
        ' 
        ' brandscb
        ' 
        brandscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        brandscb.FormattingEnabled = True
        brandscb.Location = New Point(343, 17)
        brandscb.Name = "brandscb"
        brandscb.Size = New Size(201, 39)
        brandscb.TabIndex = 13
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(22, 13)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 38)
        Label3.TabIndex = 6
        Label3.Text = "Filters :"
        ' 
        ' lblPageInfo
        ' 
        lblPageInfo.Anchor = AnchorStyles.Bottom
        lblPageInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPageInfo.Location = New Point(615, 884)
        lblPageInfo.Name = "lblPageInfo"
        lblPageInfo.Size = New Size(114, 40)
        lblPageInfo.TabIndex = 14
        lblPageInfo.Text = "Page 1 of 1"
        ' 
        ' btnPrev
        ' 
        btnPrev.Anchor = AnchorStyles.Bottom
        btnPrev.BackColor = Color.CornflowerBlue
        btnPrev.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(515, 879)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(94, 48)
        btnPrev.TabIndex = 15
        btnPrev.Text = "Previous"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' btnNext
        ' 
        btnNext.Anchor = AnchorStyles.Bottom
        btnNext.BackColor = Color.CornflowerBlue
        btnNext.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(736, 879)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(94, 48)
        btnNext.TabIndex = 16
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' addpnl
        ' 
        addpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        addpnl.Location = New Point(271, 104)
        addpnl.Name = "addpnl"
        addpnl.Size = New Size(738, 700)
        addpnl.TabIndex = 17
        addpnl.Visible = False
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(addpnl)
        Panel3.Controls.Add(Panel2)
        Panel3.Controls.Add(btnPrev)
        Panel3.Controls.Add(btnNext)
        Panel3.Controls.Add(Panel1)
        Panel3.Controls.Add(lblPageInfo)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1353, 941)
        Panel3.TabIndex = 18
        ' 
        ' devices
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Panel3)
        Name = "devices"
        Size = New Size(1353, 941)
        Panel1.ResumeLayout(False)
        CType(devicesdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents devicesdgv As DataGridView
    Friend WithEvents addbtn As Button
    Friend WithEvents filtertxt As TextBox
    Friend WithEvents headercb As ComboBox
    Friend WithEvents insidecb As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents statuscb As ComboBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents catecb As ComboBox
    Friend WithEvents brandscb As ComboBox
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnPrev As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents addpnl As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel3 As Panel

End Class
