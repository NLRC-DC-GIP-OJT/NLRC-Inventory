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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        mainpnl = New Panel()
        Panel16 = New Panel()
        Panel10 = New Panel()
        Label16 = New Label()
        Panel13 = New Panel()
        Panel14 = New Panel()
        historydgv = New DataGridView()
        Label15 = New Label()
        Panel17 = New Panel()
        Panel5 = New Panel()
        totdevices = New Label()
        Label7 = New Label()
        Panel6 = New Panel()
        totunits = New Label()
        Label8 = New Label()
        Panel7 = New Panel()
        totpersonnel = New Label()
        Label10 = New Label()
        Label3 = New Label()
        Panel11 = New Panel()
        catdgv = New DataGridView()
        Label12 = New Label()
        Panel15 = New Panel()
        Panel8 = New Panel()
        Label11 = New Label()
        Panel4 = New Panel()
        Panel12 = New Panel()
        Panel1 = New Panel()
        recentdgv = New DataGridView()
        Label13 = New Label()
        Panel9 = New Panel()
        Panel2 = New Panel()
        activitydgv = New DataGridView()
        Label14 = New Label()
        Timer2 = New Timer(components)
        mainpnl.SuspendLayout()
        Panel16.SuspendLayout()
        Panel10.SuspendLayout()
        Panel13.SuspendLayout()
        Panel14.SuspendLayout()
        CType(historydgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel17.SuspendLayout()
        Panel5.SuspendLayout()
        Panel6.SuspendLayout()
        Panel7.SuspendLayout()
        Panel11.SuspendLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel15.SuspendLayout()
        Panel4.SuspendLayout()
        Panel12.SuspendLayout()
        Panel1.SuspendLayout()
        CType(recentdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel9.SuspendLayout()
        Panel2.SuspendLayout()
        CType(activitydgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' mainpnl
        ' 
        mainpnl.AutoSize = True
        mainpnl.BackColor = Color.White
        mainpnl.Controls.Add(Panel16)
        mainpnl.Controls.Add(Panel17)
        mainpnl.Controls.Add(Panel4)
        mainpnl.Location = New Point(0, 0)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1483, 965)
        mainpnl.TabIndex = 5
        ' 
        ' Panel16
        ' 
        Panel16.Controls.Add(Panel11)
        Panel16.Controls.Add(Panel10)
        Panel16.Dock = DockStyle.Right
        Panel16.Location = New Point(1103, 0)
        Panel16.Margin = New Padding(3, 4, 3, 4)
        Panel16.Name = "Panel16"
        Panel16.Size = New Size(380, 608)
        Panel16.TabIndex = 16
        ' 
        ' Panel10
        ' 
        Panel10.Anchor = AnchorStyles.Top
        Panel10.BorderStyle = BorderStyle.FixedSingle
        Panel10.Controls.Add(Label16)
        Panel10.Location = New Point(3, 61)
        Panel10.Margin = New Padding(3, 4, 3, 4)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(348, 213)
        Panel10.TabIndex = 11
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label16.Location = New Point(65, 8)
        Label16.Name = "Label16"
        Label16.Size = New Size(223, 23)
        Label16.TabIndex = 7
        Label16.Text = "SERIAL COMPLIANCE"
        ' 
        ' Panel13
        ' 
        Panel13.Anchor = AnchorStyles.Top
        Panel13.BorderStyle = BorderStyle.FixedSingle
        Panel13.Controls.Add(Panel14)
        Panel13.Controls.Add(Label15)
        Panel13.Location = New Point(525, 222)
        Panel13.Margin = New Padding(3, 4, 3, 4)
        Panel13.Name = "Panel13"
        Panel13.Size = New Size(572, 382)
        Panel13.TabIndex = 11
        ' 
        ' Panel14
        ' 
        Panel14.Anchor = AnchorStyles.None
        Panel14.Controls.Add(historydgv)
        Panel14.Location = New Point(3, 35)
        Panel14.Margin = New Padding(3, 4, 3, 4)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(564, 339)
        Panel14.TabIndex = 11
        ' 
        ' historydgv
        ' 
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.HighlightText
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        historydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        historydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.HighlightText
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        historydgv.DefaultCellStyle = DataGridViewCellStyle5
        historydgv.Dock = DockStyle.Fill
        historydgv.Location = New Point(0, 0)
        historydgv.Name = "historydgv"
        historydgv.ReadOnly = True
        historydgv.RowHeadersVisible = False
        historydgv.RowHeadersWidth = 51
        historydgv.Size = New Size(564, 339)
        historydgv.TabIndex = 23
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(186, 6)
        Label15.Name = "Label15"
        Label15.Size = New Size(194, 23)
        Label15.TabIndex = 7
        Label15.Text = "DEVICES HISTORY"
        ' 
        ' Panel17
        ' 
        Panel17.Controls.Add(Panel5)
        Panel17.Controls.Add(Panel13)
        Panel17.Controls.Add(Panel6)
        Panel17.Controls.Add(Panel7)
        Panel17.Controls.Add(Label3)
        Panel17.Controls.Add(Panel15)
        Panel17.Dock = DockStyle.Fill
        Panel17.Location = New Point(0, 0)
        Panel17.Margin = New Padding(3, 4, 3, 4)
        Panel17.Name = "Panel17"
        Panel17.Size = New Size(1483, 608)
        Panel17.TabIndex = 17
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel5.BorderStyle = BorderStyle.FixedSingle
        Panel5.Controls.Add(totdevices)
        Panel5.Controls.Add(Label7)
        Panel5.Location = New Point(22, 65)
        Panel5.Margin = New Padding(3, 4, 3, 4)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(354, 149)
        Panel5.TabIndex = 6
        ' 
        ' totdevices
        ' 
        totdevices.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        totdevices.AutoSize = True
        totdevices.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totdevices.ForeColor = SystemColors.MenuHighlight
        totdevices.Location = New Point(105, 49)
        totdevices.Name = "totdevices"
        totdevices.Size = New Size(107, 43)
        totdevices.TabIndex = 8
        totdevices.Text = "1000"
        totdevices.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(65, 7)
        Label7.Name = "Label7"
        Label7.Size = New Size(172, 23)
        Label7.TabIndex = 7
        Label7.Text = "TOTAL DEVICES"
        ' 
        ' Panel6
        ' 
        Panel6.Anchor = AnchorStyles.Top
        Panel6.BorderStyle = BorderStyle.FixedSingle
        Panel6.Controls.Add(totunits)
        Panel6.Controls.Add(Label8)
        Panel6.Location = New Point(382, 65)
        Panel6.Margin = New Padding(3, 4, 3, 4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(346, 149)
        Panel6.TabIndex = 9
        ' 
        ' totunits
        ' 
        totunits.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        totunits.AutoSize = True
        totunits.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totunits.ForeColor = SystemColors.MenuHighlight
        totunits.Location = New Point(109, 49)
        totunits.Name = "totunits"
        totunits.Size = New Size(107, 43)
        totunits.TabIndex = 10
        totunits.Text = "1000"
        totunits.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(85, 7)
        Label8.Name = "Label8"
        Label8.Size = New Size(145, 23)
        Label8.TabIndex = 7
        Label8.Text = "TOTAL UNITS"
        ' 
        ' Panel7
        ' 
        Panel7.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(totpersonnel)
        Panel7.Controls.Add(Label10)
        Panel7.Location = New Point(734, 65)
        Panel7.Margin = New Padding(3, 4, 3, 4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(363, 149)
        Panel7.TabIndex = 10
        ' 
        ' totpersonnel
        ' 
        totpersonnel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        totpersonnel.AutoSize = True
        totpersonnel.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totpersonnel.ForeColor = SystemColors.MenuHighlight
        totpersonnel.Location = New Point(114, 49)
        totpersonnel.Name = "totpersonnel"
        totpersonnel.Size = New Size(107, 43)
        totpersonnel.TabIndex = 9
        totpersonnel.Text = "1000"
        ' 
        ' Label10
        ' 
        Label10.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Label10.AutoSize = True
        Label10.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(66, 7)
        Label10.Name = "Label10"
        Label10.Size = New Size(207, 23)
        Label10.TabIndex = 7
        Label10.Text = "TOTAL PERSONNEL"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.Location = New Point(22, 12)
        Label3.Name = "Label3"
        Label3.Size = New Size(193, 38)
        Label3.TabIndex = 15
        Label3.Text = "DASHBOARD"
        ' 
        ' Panel11
        ' 
        Panel11.AutoSize = True
        Panel11.BorderStyle = BorderStyle.FixedSingle
        Panel11.Controls.Add(catdgv)
        Panel11.Controls.Add(Label12)
        Panel11.Location = New Point(4, 282)
        Panel11.Margin = New Padding(3, 4, 3, 4)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(351, 323)
        Panel11.TabIndex = 13
        ' 
        ' catdgv
        ' 
        catdgv.AllowUserToAddRows = False
        catdgv.AllowUserToDeleteRows = False
        catdgv.AllowUserToResizeColumns = False
        catdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        catdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        catdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        catdgv.DefaultCellStyle = DataGridViewCellStyle2
        catdgv.Location = New Point(3, 30)
        catdgv.Name = "catdgv"
        catdgv.ReadOnly = True
        catdgv.RowHeadersVisible = False
        catdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        catdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        catdgv.Size = New Size(343, 288)
        catdgv.TabIndex = 8
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(37, 4)
        Label12.Name = "Label12"
        Label12.Size = New Size(250, 23)
        Label12.TabIndex = 7
        Label12.Text = "DEVICES BY CATEGORY"
        ' 
        ' Panel15
        ' 
        Panel15.AutoSize = True
        Panel15.BorderStyle = BorderStyle.FixedSingle
        Panel15.Controls.Add(Panel8)
        Panel15.Controls.Add(Label11)
        Panel15.Location = New Point(23, 224)
        Panel15.Margin = New Padding(3, 4, 3, 4)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(500, 378)
        Panel15.TabIndex = 12
        ' 
        ' Panel8
        ' 
        Panel8.AutoSize = True
        Panel8.Location = New Point(3, 33)
        Panel8.Margin = New Padding(3, 4, 3, 4)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(492, 339)
        Panel8.TabIndex = 13
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(94, 6)
        Label11.Name = "Label11"
        Label11.Size = New Size(242, 23)
        Label11.TabIndex = 7
        Label11.Text = "OPERATIONAL STATUS"
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Panel12)
        Panel4.Controls.Add(Panel9)
        Panel4.Dock = DockStyle.Bottom
        Panel4.Location = New Point(0, 608)
        Panel4.Margin = New Padding(3, 4, 3, 4)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1483, 357)
        Panel4.TabIndex = 15
        ' 
        ' Panel12
        ' 
        Panel12.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Panel12.BorderStyle = BorderStyle.FixedSingle
        Panel12.Controls.Add(Panel1)
        Panel12.Controls.Add(Label13)
        Panel12.Location = New Point(23, 11)
        Panel12.Margin = New Padding(3, 4, 3, 4)
        Panel12.Name = "Panel12"
        Panel12.Size = New Size(688, 333)
        Panel12.TabIndex = 14
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(recentdgv)
        Panel1.Location = New Point(-1, 35)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(688, 292)
        Panel1.TabIndex = 8
        ' 
        ' recentdgv
        ' 
        recentdgv.AllowUserToAddRows = False
        recentdgv.AllowUserToDeleteRows = False
        recentdgv.AllowUserToResizeColumns = False
        recentdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = Color.Transparent
        DataGridViewCellStyle6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.White
        DataGridViewCellStyle6.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.True
        recentdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        recentdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = Color.SkyBlue
        DataGridViewCellStyle7.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle7.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.False
        recentdgv.DefaultCellStyle = DataGridViewCellStyle7
        recentdgv.Dock = DockStyle.Fill
        recentdgv.Location = New Point(0, 0)
        recentdgv.Name = "recentdgv"
        recentdgv.ReadOnly = True
        recentdgv.RowHeadersVisible = False
        recentdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle8.BackColor = Color.AliceBlue
        DataGridViewCellStyle8.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle8.ForeColor = Color.Black
        recentdgv.RowsDefaultCellStyle = DataGridViewCellStyle8
        recentdgv.Size = New Size(688, 292)
        recentdgv.TabIndex = 9
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label13.Location = New Point(141, 5)
        Label13.Name = "Label13"
        Label13.Size = New Size(386, 23)
        Label13.TabIndex = 7
        Label13.Text = "RECENT UNIT ASSIGNMENT ACTIVITY"
        ' 
        ' Panel9
        ' 
        Panel9.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel9.AutoSize = True
        Panel9.BorderStyle = BorderStyle.FixedSingle
        Panel9.Controls.Add(Panel2)
        Panel9.Controls.Add(Label14)
        Panel9.Location = New Point(718, 12)
        Panel9.Margin = New Padding(3, 4, 3, 4)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(940, 331)
        Panel9.TabIndex = 10
        ' 
        ' Panel2
        ' 
        Panel2.AutoSize = True
        Panel2.Controls.Add(activitydgv)
        Panel2.Location = New Point(-1, 33)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(736, 293)
        Panel2.TabIndex = 8
        ' 
        ' activitydgv
        ' 
        activitydgv.AllowUserToAddRows = False
        activitydgv.AllowUserToDeleteRows = False
        activitydgv.AllowUserToResizeColumns = False
        activitydgv.AllowUserToResizeRows = False
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = Color.SkyBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.White
        DataGridViewCellStyle9.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.True
        activitydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        activitydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = SystemColors.Window
        DataGridViewCellStyle10.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle10.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = DataGridViewTriState.False
        activitydgv.DefaultCellStyle = DataGridViewCellStyle10
        activitydgv.Dock = DockStyle.Fill
        activitydgv.Location = New Point(0, 0)
        activitydgv.Name = "activitydgv"
        activitydgv.ReadOnly = True
        activitydgv.RowHeadersVisible = False
        activitydgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle11.BackColor = Color.AliceBlue
        DataGridViewCellStyle11.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle11.ForeColor = Color.Black
        activitydgv.RowsDefaultCellStyle = DataGridViewCellStyle11
        activitydgv.Size = New Size(736, 293)
        activitydgv.TabIndex = 9
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(261, 5)
        Label14.Name = "Label14"
        Label14.Size = New Size(211, 23)
        Label14.TabIndex = 7
        Label14.Text = "RECENT ACTIVITIES"
        ' 
        ' Timer2
        ' 
        ' 
        ' DashboardControl
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(mainpnl)
        Name = "DashboardControl"
        Size = New Size(1480, 963)
        mainpnl.ResumeLayout(False)
        Panel16.ResumeLayout(False)
        Panel16.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel13.ResumeLayout(False)
        Panel13.PerformLayout()
        Panel14.ResumeLayout(False)
        CType(historydgv, ComponentModel.ISupportInitialize).EndInit()
        Panel17.ResumeLayout(False)
        Panel17.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel15.ResumeLayout(False)
        Panel15.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel12.ResumeLayout(False)
        Panel12.PerformLayout()
        Panel1.ResumeLayout(False)
        CType(recentdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(activitydgv, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents mainpnl As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Panel17 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents totdevices As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents totunits As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents totpersonnel As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents catdgv As DataGridView
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents recentdgv As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents activitydgv As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents historydgv As DataGridView

End Class
