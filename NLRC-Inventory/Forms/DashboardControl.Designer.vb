<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardControl
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
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel17 = New Panel()
        Panel2 = New Panel()
        activitydgv = New DataGridView()
        Label14 = New Label()
        Panel1 = New Panel()
        recentdgv = New DataGridView()
        Label13 = New Label()
        Panel3 = New Panel()
        Panel16 = New Panel()
        Label1 = New Label()
        Panel13 = New Panel()
        Panel14 = New Panel()
        historydgv = New DataGridView()
        Label15 = New Label()
        Panel11 = New Panel()
        catdgv = New DataGridView()
        Label12 = New Label()
        Panel5 = New Panel()
        totdevices = New Label()
        Label7 = New Label()
        Panel10 = New Panel()
        Label16 = New Label()
        Panel6 = New Panel()
        totunits = New Label()
        Label8 = New Label()
        Panel7 = New Panel()
        totpersonnel = New Label()
        Label10 = New Label()
        Label3 = New Label()
        Panel15 = New Panel()
        Panel8 = New Panel()
        Label11 = New Label()
        Timer2 = New Timer(components)
        Panel17.SuspendLayout()
        Panel2.SuspendLayout()
        CType(activitydgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(recentdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        Panel13.SuspendLayout()
        Panel14.SuspendLayout()
        CType(historydgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel11.SuspendLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel5.SuspendLayout()
        Panel10.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel15.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel17
        ' 
        Panel17.BackColor = Color.White
        Panel17.Controls.Add(Panel2)
        Panel17.Controls.Add(Panel1)
        Panel17.Controls.Add(Panel3)
        Panel17.Controls.Add(Panel13)
        Panel17.Controls.Add(Panel11)
        Panel17.Controls.Add(Panel5)
        Panel17.Controls.Add(Panel10)
        Panel17.Controls.Add(Panel6)
        Panel17.Controls.Add(Panel7)
        Panel17.Controls.Add(Label3)
        Panel17.Controls.Add(Panel15)
        Panel17.Dock = DockStyle.Fill
        Panel17.Location = New Point(0, 0)
        Panel17.Name = "Panel17"
        Panel17.Size = New Size(1567, 882)
        Panel17.TabIndex = 17
        ' 
        ' Panel2
        ' 
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(activitydgv)
        Panel2.Controls.Add(Label14)
        Panel2.Location = New Point(730, 510)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(811, 305)
        Panel2.TabIndex = 26
        ' 
        ' activitydgv
        ' 
        activitydgv.AllowUserToAddRows = False
        activitydgv.AllowUserToDeleteRows = False
        activitydgv.AllowUserToResizeColumns = False
        activitydgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.SkyBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        activitydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        activitydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        activitydgv.DefaultCellStyle = DataGridViewCellStyle2
        activitydgv.Location = New Point(3, 27)
        activitydgv.Margin = New Padding(3, 2, 3, 2)
        activitydgv.Name = "activitydgv"
        activitydgv.ReadOnly = True
        activitydgv.RowHeadersVisible = False
        activitydgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        activitydgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        activitydgv.Size = New Size(803, 276)
        activitydgv.TabIndex = 9
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(325, 5)
        Label14.Name = "Label14"
        Label14.Size = New Size(172, 18)
        Label14.TabIndex = 7
        Label14.Text = "RECENT ACTIVITIES"
        ' 
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(recentdgv)
        Panel1.Controls.Add(Label13)
        Panel1.Location = New Point(18, 510)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(708, 305)
        Panel1.TabIndex = 25
        ' 
        ' recentdgv
        ' 
        recentdgv.AllowUserToAddRows = False
        recentdgv.AllowUserToDeleteRows = False
        recentdgv.AllowUserToResizeColumns = False
        recentdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = Color.Transparent
        DataGridViewCellStyle4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        recentdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        recentdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = Color.SkyBlue
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        recentdgv.DefaultCellStyle = DataGridViewCellStyle5
        recentdgv.Location = New Point(3, 29)
        recentdgv.Margin = New Padding(3, 2, 3, 2)
        recentdgv.Name = "recentdgv"
        recentdgv.ReadOnly = True
        recentdgv.RowHeadersVisible = False
        recentdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        recentdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        recentdgv.Size = New Size(702, 273)
        recentdgv.TabIndex = 9
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label13.Location = New Point(214, 5)
        Label13.Name = "Label13"
        Label13.Size = New Size(312, 18)
        Label13.TabIndex = 7
        Label13.Text = "RECENT UNIT ASSIGNMENT ACTIVITY"
        ' 
        ' Panel3
        ' 
        Panel3.AutoSize = True
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Controls.Add(Panel16)
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(506, 200)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(336, 305)
        Panel3.TabIndex = 23
        ' 
        ' Panel16
        ' 
        Panel16.AutoSize = True
        Panel16.Location = New Point(3, 35)
        Panel16.Name = "Panel16"
        Panel16.Size = New Size(328, 265)
        Panel16.TabIndex = 13
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(54, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(199, 18)
        Label1.TabIndex = 7
        Label1.Text = "OPERATIONAL STATUS"
        ' 
        ' Panel13
        ' 
        Panel13.Anchor = AnchorStyles.Top
        Panel13.BorderStyle = BorderStyle.FixedSingle
        Panel13.Controls.Add(Panel14)
        Panel13.Controls.Add(Label15)
        Panel13.Location = New Point(18, 199)
        Panel13.Name = "Panel13"
        Panel13.Size = New Size(482, 305)
        Panel13.TabIndex = 20
        ' 
        ' Panel14
        ' 
        Panel14.Anchor = AnchorStyles.None
        Panel14.Controls.Add(historydgv)
        Panel14.Location = New Point(4, 31)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(473, 269)
        Panel14.TabIndex = 11
        ' 
        ' historydgv
        ' 
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = SystemColors.HighlightText
        DataGridViewCellStyle7.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle7.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.True
        historydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        historydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = SystemColors.Window
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle8.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.False
        historydgv.DefaultCellStyle = DataGridViewCellStyle8
        historydgv.Dock = DockStyle.Fill
        historydgv.Location = New Point(0, 0)
        historydgv.Margin = New Padding(3, 2, 3, 2)
        historydgv.Name = "historydgv"
        historydgv.ReadOnly = True
        historydgv.RowHeadersVisible = False
        historydgv.RowHeadersWidth = 51
        historydgv.Size = New Size(473, 269)
        historydgv.TabIndex = 23
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(163, 8)
        Label15.Name = "Label15"
        Label15.Size = New Size(158, 18)
        Label15.TabIndex = 7
        Label15.Text = "DEVICES HISTORY"
        ' 
        ' Panel11
        ' 
        Panel11.AutoSize = True
        Panel11.BorderStyle = BorderStyle.FixedSingle
        Panel11.Controls.Add(catdgv)
        Panel11.Controls.Add(Label12)
        Panel11.Location = New Point(1113, 63)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(428, 442)
        Panel11.TabIndex = 22
        ' 
        ' catdgv
        ' 
        catdgv.AllowUserToAddRows = False
        catdgv.AllowUserToDeleteRows = False
        catdgv.AllowUserToResizeColumns = False
        catdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.White
        DataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.True
        catdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        catdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = SystemColors.Window
        DataGridViewCellStyle10.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle10.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = DataGridViewTriState.False
        catdgv.DefaultCellStyle = DataGridViewCellStyle10
        catdgv.Location = New Point(2, 26)
        catdgv.Margin = New Padding(3, 2, 3, 2)
        catdgv.Name = "catdgv"
        catdgv.ReadOnly = True
        catdgv.RowHeadersVisible = False
        catdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle11.BackColor = Color.AliceBlue
        DataGridViewCellStyle11.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle11.ForeColor = Color.Black
        catdgv.RowsDefaultCellStyle = DataGridViewCellStyle11
        catdgv.Size = New Size(421, 410)
        catdgv.TabIndex = 8
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(133, 4)
        Label12.Name = "Label12"
        Label12.Size = New Size(204, 18)
        Label12.TabIndex = 7
        Label12.Text = "DEVICES BY CATEGORY"
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel5.BorderStyle = BorderStyle.FixedSingle
        Panel5.Controls.Add(totdevices)
        Panel5.Controls.Add(Label7)
        Panel5.Location = New Point(18, 63)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(270, 130)
        Panel5.TabIndex = 16
        ' 
        ' totdevices
        ' 
        totdevices.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        totdevices.AutoSize = True
        totdevices.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totdevices.ForeColor = SystemColors.MenuHighlight
        totdevices.Location = New Point(85, 51)
        totdevices.Name = "totdevices"
        totdevices.Size = New Size(83, 33)
        totdevices.TabIndex = 8
        totdevices.Text = "1000"
        totdevices.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(54, 8)
        Label7.Name = "Label7"
        Label7.Size = New Size(140, 18)
        Label7.TabIndex = 7
        Label7.Text = "TOTAL DEVICES"
        ' 
        ' Panel10
        ' 
        Panel10.Anchor = AnchorStyles.Top
        Panel10.BorderStyle = BorderStyle.FixedSingle
        Panel10.Controls.Add(Label16)
        Panel10.Location = New Point(848, 63)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(259, 172)
        Panel10.TabIndex = 19
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label16.Location = New Point(36, 6)
        Label16.Name = "Label16"
        Label16.Size = New Size(184, 18)
        Label16.TabIndex = 7
        Label16.Text = "SERIAL COMPLIANCE"
        ' 
        ' Panel6
        ' 
        Panel6.Anchor = AnchorStyles.Top
        Panel6.BorderStyle = BorderStyle.FixedSingle
        Panel6.Controls.Add(totunits)
        Panel6.Controls.Add(Label8)
        Panel6.Location = New Point(294, 63)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(271, 130)
        Panel6.TabIndex = 17
        ' 
        ' totunits
        ' 
        totunits.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        totunits.AutoSize = True
        totunits.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totunits.ForeColor = SystemColors.MenuHighlight
        totunits.Location = New Point(98, 51)
        totunits.Name = "totunits"
        totunits.Size = New Size(83, 33)
        totunits.TabIndex = 10
        totunits.Text = "1000"
        totunits.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(69, 6)
        Label8.Name = "Label8"
        Label8.Size = New Size(117, 18)
        Label8.TabIndex = 7
        Label8.Text = "TOTAL UNITS"
        ' 
        ' Panel7
        ' 
        Panel7.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(totpersonnel)
        Panel7.Controls.Add(Label10)
        Panel7.Location = New Point(571, 63)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(271, 130)
        Panel7.TabIndex = 18
        ' 
        ' totpersonnel
        ' 
        totpersonnel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        totpersonnel.AutoSize = True
        totpersonnel.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totpersonnel.ForeColor = SystemColors.MenuHighlight
        totpersonnel.Location = New Point(89, 51)
        totpersonnel.Name = "totpersonnel"
        totpersonnel.Size = New Size(83, 33)
        totpersonnel.TabIndex = 9
        totpersonnel.Text = "1000"
        ' 
        ' Label10
        ' 
        Label10.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label10.AutoSize = True
        Label10.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(42, 8)
        Label10.Name = "Label10"
        Label10.Size = New Size(170, 18)
        Label10.TabIndex = 7
        Label10.Text = "TOTAL PERSONNEL"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.Location = New Point(18, 17)
        Label3.Name = "Label3"
        Label3.Size = New Size(149, 30)
        Label3.TabIndex = 24
        Label3.Text = "DASHBOARD"
        ' 
        ' Panel15
        ' 
        Panel15.AutoSize = True
        Panel15.BorderStyle = BorderStyle.FixedSingle
        Panel15.Controls.Add(Panel8)
        Panel15.Controls.Add(Label11)
        Panel15.Location = New Point(848, 241)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(262, 264)
        Panel15.TabIndex = 21
        ' 
        ' Panel8
        ' 
        Panel8.AutoSize = True
        Panel8.Location = New Point(3, 35)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(254, 224)
        Panel8.TabIndex = 13
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(19, 8)
        Label11.Name = "Label11"
        Label11.Size = New Size(188, 18)
        Label11.TabIndex = 7
        Label11.Text = "ASSIGNMENT STATUS"
        ' 
        ' Timer2
        ' 
        ' 
        ' DashboardControl
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel17)
        Margin = New Padding(3, 2, 3, 2)
        Name = "DashboardControl"
        Size = New Size(1567, 882)
        Panel17.ResumeLayout(False)
        Panel17.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(activitydgv, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(recentdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel13.ResumeLayout(False)
        Panel13.PerformLayout()
        Panel14.ResumeLayout(False)
        CType(historydgv, ComponentModel.ISupportInitialize).EndInit()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel15.ResumeLayout(False)
        Panel15.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Panel17 As Panel
    Friend WithEvents recentdgv As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents activitydgv As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents catdgv As DataGridView
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents totdevices As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents historydgv As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents totunits As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents totpersonnel As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel

End Class
