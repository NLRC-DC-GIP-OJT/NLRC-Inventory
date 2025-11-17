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
        Panel1.Controls.Add(devicesdgv)
        Panel1.Location = New Point(15, 124)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1441, 761)
        Panel1.TabIndex = 7
        ' 
        ' devicesdgv
        ' 
        devicesdgv.AllowUserToAddRows = False
        devicesdgv.AllowUserToDeleteRows = False
        devicesdgv.AllowUserToResizeColumns = False
        devicesdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
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
        devicesdgv.Name = "devicesdgv"
        devicesdgv.ReadOnly = True
        devicesdgv.RowHeadersVisible = False
        devicesdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        devicesdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        devicesdgv.Size = New Size(1441, 761)
        devicesdgv.TabIndex = 0
        ' 
        ' addbtn
        ' 
        addbtn.BackColor = Color.CornflowerBlue
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(1283, 10)
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
        filtertxt.Location = New Point(948, 20)
        filtertxt.Name = "filtertxt"
        filtertxt.Size = New Size(301, 38)
        filtertxt.TabIndex = 9
        ' 
        ' statuscb
        ' 
        statuscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        statuscb.FormattingEnabled = True
        statuscb.Location = New Point(673, 18)
        statuscb.Name = "statuscb"
        statuscb.Size = New Size(251, 39)
        statuscb.TabIndex = 12
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(filtertxt)
        Panel2.Controls.Add(addbtn)
        Panel2.Controls.Add(catecb)
        Panel2.Controls.Add(brandscb)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(statuscb)
        Panel2.Location = New Point(15, 24)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1441, 76)
        Panel2.TabIndex = 13
        ' 
        ' catecb
        ' 
        catecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catecb.FormattingEnabled = True
        catecb.Location = New Point(152, 20)
        catecb.Name = "catecb"
        catecb.Size = New Size(218, 39)
        catecb.TabIndex = 14
        ' 
        ' brandscb
        ' 
        brandscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        brandscb.FormattingEnabled = True
        brandscb.Location = New Point(397, 18)
        brandscb.Name = "brandscb"
        brandscb.Size = New Size(250, 39)
        brandscb.TabIndex = 13
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(22, 16)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 38)
        Label3.TabIndex = 6
        Label3.Text = "Filters :"
        ' 
        ' lblPageInfo
        ' 
        lblPageInfo.AutoSize = True
        lblPageInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPageInfo.Location = New Point(746, 914)
        lblPageInfo.Name = "lblPageInfo"
        lblPageInfo.Size = New Size(109, 28)
        lblPageInfo.TabIndex = 14
        lblPageInfo.Text = "Page 1 of 1"
        ' 
        ' btnPrev
        ' 
        btnPrev.BackColor = Color.CornflowerBlue
        btnPrev.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(646, 904)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(94, 48)
        btnPrev.TabIndex = 15
        btnPrev.Text = "Previous"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' btnNext
        ' 
        btnNext.BackColor = Color.CornflowerBlue
        btnNext.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(867, 904)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(94, 48)
        btnNext.TabIndex = 16
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' addpnl
        ' 
        addpnl.Location = New Point(271, 82)
        addpnl.Name = "addpnl"
        addpnl.Size = New Size(867, 747)
        addpnl.TabIndex = 17
        addpnl.Visible = False
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel3.Controls.Add(addpnl)
        Panel3.Controls.Add(Panel2)
        Panel3.Controls.Add(btnPrev)
        Panel3.Controls.Add(btnNext)
        Panel3.Controls.Add(Panel1)
        Panel3.Controls.Add(lblPageInfo)
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1480, 963)
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
        Size = New Size(1480, 963)
        Panel1.ResumeLayout(False)
        CType(devicesdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
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
