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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(devices))
        Panel1 = New Panel()
        devicesdgv = New DataGridView()
        addbtn = New Button()
        filtertxt = New TextBox()
        statuscb = New ComboBox()
        Panel2 = New Panel()
        exportbtn = New Button()
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
        Panel1.Location = New Point(33, 95)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1496, 714)
        Panel1.TabIndex = 7
        ' 
        ' devicesdgv
        ' 
        devicesdgv.AllowUserToAddRows = False
        devicesdgv.AllowUserToDeleteRows = False
        devicesdgv.AllowUserToResizeColumns = False
        devicesdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        devicesdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        devicesdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        devicesdgv.DefaultCellStyle = DataGridViewCellStyle2
        devicesdgv.Dock = DockStyle.Fill
        devicesdgv.Location = New Point(0, 0)
        devicesdgv.Margin = New Padding(3, 2, 3, 2)
        devicesdgv.Name = "devicesdgv"
        devicesdgv.ReadOnly = True
        devicesdgv.RowHeadersVisible = False
        devicesdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        devicesdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        devicesdgv.Size = New Size(1496, 714)
        devicesdgv.TabIndex = 0
        ' 
        ' addbtn
        ' 
        addbtn.BackColor = Color.SlateGray
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(1414, 10)
        addbtn.Margin = New Padding(3, 2, 3, 2)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(114, 54)
        addbtn.TabIndex = 8
        addbtn.Text = "Add Device"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' filtertxt
        ' 
        filtertxt.BorderStyle = BorderStyle.FixedSingle
        filtertxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        filtertxt.Location = New Point(802, 22)
        filtertxt.Margin = New Padding(3, 2, 3, 2)
        filtertxt.Name = "filtertxt"
        filtertxt.Size = New Size(315, 32)
        filtertxt.TabIndex = 9
        ' 
        ' statuscb
        ' 
        statuscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        statuscb.FormattingEnabled = True
        statuscb.Location = New Point(570, 22)
        statuscb.Margin = New Padding(3, 2, 3, 2)
        statuscb.Name = "statuscb"
        statuscb.Size = New Size(226, 33)
        statuscb.TabIndex = 12
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(exportbtn)
        Panel2.Controls.Add(addbtn)
        Panel2.Controls.Add(filtertxt)
        Panel2.Controls.Add(catecb)
        Panel2.Controls.Add(brandscb)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(statuscb)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1567, 75)
        Panel2.TabIndex = 13
        ' 
        ' exportbtn
        ' 
        exportbtn.BackColor = Color.SlateGray
        exportbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        exportbtn.ForeColor = Color.White
        exportbtn.Location = New Point(1278, 10)
        exportbtn.Margin = New Padding(3, 2, 3, 2)
        exportbtn.Name = "exportbtn"
        exportbtn.Size = New Size(114, 54)
        exportbtn.TabIndex = 15
        exportbtn.Text = "Export"
        exportbtn.UseVisualStyleBackColor = False
        ' 
        ' catecb
        ' 
        catecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catecb.FormattingEnabled = True
        catecb.Location = New Point(113, 21)
        catecb.Margin = New Padding(3, 2, 3, 2)
        catecb.Name = "catecb"
        catecb.Size = New Size(226, 33)
        catecb.TabIndex = 14
        ' 
        ' brandscb
        ' 
        brandscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        brandscb.FormattingEnabled = True
        brandscb.Location = New Point(345, 22)
        brandscb.Margin = New Padding(3, 2, 3, 2)
        brandscb.Name = "brandscb"
        brandscb.Size = New Size(219, 33)
        brandscb.TabIndex = 13
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(19, 20)
        Label3.Name = "Label3"
        Label3.Size = New Size(88, 30)
        Label3.TabIndex = 6
        Label3.Text = "Filters :"
        ' 
        ' lblPageInfo
        ' 
        lblPageInfo.Anchor = AnchorStyles.Bottom
        lblPageInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPageInfo.Location = New Point(732, 832)
        lblPageInfo.Name = "lblPageInfo"
        lblPageInfo.Size = New Size(100, 30)
        lblPageInfo.TabIndex = 14
        lblPageInfo.Text = "Page 1 of 1"
        ' 
        ' btnPrev
        ' 
        btnPrev.Anchor = AnchorStyles.Bottom
        btnPrev.BackColor = Color.SlateGray
        btnPrev.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(631, 818)
        btnPrev.Margin = New Padding(3, 2, 3, 2)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(93, 49)
        btnPrev.TabIndex = 15
        btnPrev.Text = "Previous"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' btnNext
        ' 
        btnNext.Anchor = AnchorStyles.Bottom
        btnNext.BackColor = Color.SlateGray
        btnNext.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(836, 819)
        btnNext.Margin = New Padding(3, 2, 3, 2)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(89, 49)
        btnNext.TabIndex = 16
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' addpnl
        ' 
        addpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        addpnl.Location = New Point(335, 32)
        addpnl.Margin = New Padding(3, 2, 3, 2)
        addpnl.Name = "addpnl"
        addpnl.Size = New Size(845, 762)
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
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1567, 882)
        Panel3.TabIndex = 18
        ' 
        ' devices
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Panel3)
        Margin = New Padding(3, 2, 3, 2)
        Name = "devices"
        Size = New Size(1567, 882)
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
    Friend WithEvents exportbtn As Button

End Class
