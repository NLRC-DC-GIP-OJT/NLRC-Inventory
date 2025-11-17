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
        mainpnl = New Panel()
        Label3 = New Label()
        Panel12 = New Panel()
        recentdgv = New DataGridView()
        Label13 = New Label()
        Panel11 = New Panel()
        catdgv = New DataGridView()
        Label12 = New Label()
        Panel9 = New Panel()
        activitydgv = New DataGridView()
        Label14 = New Label()
        Panel15 = New Panel()
        Panel8 = New Panel()
        Label11 = New Label()
        Panel10 = New Panel()
        Label16 = New Label()
        Panel7 = New Panel()
        totpersonnel = New Label()
        Label10 = New Label()
        Panel6 = New Panel()
        totunits = New Label()
        Label8 = New Label()
        Panel5 = New Panel()
        totdevices = New Label()
        Label7 = New Label()
        Panel13 = New Panel()
        Panel14 = New Panel()
        Label15 = New Label()
        Timer2 = New Timer(components)
        mainpnl.SuspendLayout()
        Panel12.SuspendLayout()
        CType(recentdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel11.SuspendLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel9.SuspendLayout()
        CType(activitydgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel15.SuspendLayout()
        Panel10.SuspendLayout()
        Panel7.SuspendLayout()
        Panel6.SuspendLayout()
        Panel5.SuspendLayout()
        Panel13.SuspendLayout()
        SuspendLayout()
        ' 
        ' mainpnl
        ' 
        mainpnl.BackColor = Color.White
        mainpnl.Controls.Add(Label3)
        mainpnl.Controls.Add(Panel12)
        mainpnl.Controls.Add(Panel11)
        mainpnl.Controls.Add(Panel9)
        mainpnl.Controls.Add(Panel15)
        mainpnl.Controls.Add(Panel10)
        mainpnl.Controls.Add(Panel7)
        mainpnl.Controls.Add(Panel6)
        mainpnl.Controls.Add(Panel5)
        mainpnl.Controls.Add(Panel13)
        mainpnl.Dock = DockStyle.Fill
        mainpnl.Location = New Point(0, 0)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1480, 963)
        mainpnl.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.Location = New Point(38, 19)
        Label3.Name = "Label3"
        Label3.Size = New Size(193, 38)
        Label3.TabIndex = 15
        Label3.Text = "DASHBOARD"
        ' 
        ' Panel12
        ' 
        Panel12.BorderStyle = BorderStyle.FixedSingle
        Panel12.Controls.Add(recentdgv)
        Panel12.Controls.Add(Label13)
        Panel12.Location = New Point(48, 613)
        Panel12.Margin = New Padding(3, 4, 3, 4)
        Panel12.Name = "Panel12"
        Panel12.Size = New Size(684, 324)
        Panel12.TabIndex = 14
        ' 
        ' recentdgv
        ' 
        recentdgv.AllowUserToAddRows = False
        recentdgv.AllowUserToDeleteRows = False
        recentdgv.AllowUserToResizeColumns = False
        recentdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.Transparent
        DataGridViewCellStyle1.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        recentdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        recentdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.SkyBlue
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        recentdgv.DefaultCellStyle = DataGridViewCellStyle2
        recentdgv.Location = New Point(3, 31)
        recentdgv.Name = "recentdgv"
        recentdgv.ReadOnly = True
        recentdgv.RowHeadersVisible = False
        recentdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        recentdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        recentdgv.Size = New Size(684, 288)
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
        ' Panel11
        ' 
        Panel11.BorderStyle = BorderStyle.FixedSingle
        Panel11.Controls.Add(catdgv)
        Panel11.Controls.Add(Label12)
        Panel11.Location = New Point(485, 233)
        Panel11.Margin = New Padding(3, 4, 3, 4)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(600, 372)
        Panel11.TabIndex = 13
        ' 
        ' catdgv
        ' 
        catdgv.AllowUserToAddRows = False
        catdgv.AllowUserToDeleteRows = False
        catdgv.AllowUserToResizeColumns = False
        catdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle4.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        catdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        catdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        catdgv.DefaultCellStyle = DataGridViewCellStyle5
        catdgv.Location = New Point(-1, 32)
        catdgv.Name = "catdgv"
        catdgv.ReadOnly = True
        catdgv.RowHeadersVisible = False
        catdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        catdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        catdgv.Size = New Size(600, 339)
        catdgv.TabIndex = 8
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(159, 4)
        Label12.Name = "Label12"
        Label12.Size = New Size(250, 23)
        Label12.TabIndex = 7
        Label12.Text = "DEVICES BY CATEGORY"
        ' 
        ' Panel9
        ' 
        Panel9.BorderStyle = BorderStyle.FixedSingle
        Panel9.Controls.Add(activitydgv)
        Panel9.Controls.Add(Label14)
        Panel9.Location = New Point(738, 613)
        Panel9.Margin = New Padding(3, 4, 3, 4)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(712, 324)
        Panel9.TabIndex = 10
        ' 
        ' activitydgv
        ' 
        activitydgv.AllowUserToAddRows = False
        activitydgv.AllowUserToDeleteRows = False
        activitydgv.AllowUserToResizeColumns = False
        activitydgv.AllowUserToResizeRows = False
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = Color.SkyBlue
        DataGridViewCellStyle7.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle7.ForeColor = Color.White
        DataGridViewCellStyle7.SelectionBackColor = Color.Transparent
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.True
        activitydgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        activitydgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = SystemColors.Window
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle8.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.False
        activitydgv.DefaultCellStyle = DataGridViewCellStyle8
        activitydgv.Location = New Point(-1, 35)
        activitydgv.Name = "activitydgv"
        activitydgv.ReadOnly = True
        activitydgv.RowHeadersVisible = False
        activitydgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle9.BackColor = Color.AliceBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.Black
        activitydgv.RowsDefaultCellStyle = DataGridViewCellStyle9
        activitydgv.Size = New Size(712, 288)
        activitydgv.TabIndex = 9
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(261, 7)
        Label14.Name = "Label14"
        Label14.Size = New Size(211, 23)
        Label14.TabIndex = 7
        Label14.Text = "RECENT ACTIVITIES"
        ' 
        ' Panel15
        ' 
        Panel15.BorderStyle = BorderStyle.FixedSingle
        Panel15.Controls.Add(Panel8)
        Panel15.Controls.Add(Label11)
        Panel15.Location = New Point(48, 233)
        Panel15.Margin = New Padding(3, 4, 3, 4)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(431, 372)
        Panel15.TabIndex = 12
        ' 
        ' Panel8
        ' 
        Panel8.Location = New Point(-1, 33)
        Panel8.Margin = New Padding(3, 4, 3, 4)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(431, 338)
        Panel8.TabIndex = 13
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(77, 5)
        Label11.Name = "Label11"
        Label11.Size = New Size(242, 23)
        Label11.TabIndex = 7
        Label11.Text = "OPERATIONAL STATUS"
        ' 
        ' Panel10
        ' 
        Panel10.BorderStyle = BorderStyle.FixedSingle
        Panel10.Controls.Add(Label16)
        Panel10.Location = New Point(1091, 76)
        Panel10.Margin = New Padding(3, 4, 3, 4)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(359, 256)
        Panel10.TabIndex = 11
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label16.Location = New Point(73, 3)
        Label16.Name = "Label16"
        Label16.Size = New Size(223, 23)
        Label16.TabIndex = 7
        Label16.Text = "SERIAL COMPLIANCE"
        ' 
        ' Panel7
        ' 
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(totpersonnel)
        Panel7.Controls.Add(Label10)
        Panel7.Location = New Point(698, 76)
        Panel7.Margin = New Padding(3, 4, 3, 4)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(387, 149)
        Panel7.TabIndex = 10
        ' 
        ' totpersonnel
        ' 
        totpersonnel.AutoSize = True
        totpersonnel.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totpersonnel.ForeColor = SystemColors.MenuHighlight
        totpersonnel.Location = New Point(145, 49)
        totpersonnel.Name = "totpersonnel"
        totpersonnel.Size = New Size(107, 43)
        totpersonnel.TabIndex = 9
        totpersonnel.Text = "1000"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(37, 9)
        Label10.Name = "Label10"
        Label10.Size = New Size(317, 23)
        Label10.TabIndex = 7
        Label10.Text = "TOTAL ASSIGNED PERSONNEL"
        ' 
        ' Panel6
        ' 
        Panel6.BorderStyle = BorderStyle.FixedSingle
        Panel6.Controls.Add(totunits)
        Panel6.Controls.Add(Label8)
        Panel6.Location = New Point(371, 76)
        Panel6.Margin = New Padding(3, 4, 3, 4)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(321, 149)
        Panel6.TabIndex = 9
        ' 
        ' totunits
        ' 
        totunits.AutoSize = True
        totunits.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totunits.ForeColor = SystemColors.MenuHighlight
        totunits.Location = New Point(111, 49)
        totunits.Name = "totunits"
        totunits.Size = New Size(107, 43)
        totunits.TabIndex = 10
        totunits.Text = "1000"
        totunits.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(73, 9)
        Label8.Name = "Label8"
        Label8.Size = New Size(145, 23)
        Label8.TabIndex = 7
        Label8.Text = "TOTAL UNITS"
        ' 
        ' Panel5
        ' 
        Panel5.BorderStyle = BorderStyle.FixedSingle
        Panel5.Controls.Add(totdevices)
        Panel5.Controls.Add(Label7)
        Panel5.Location = New Point(48, 76)
        Panel5.Margin = New Padding(3, 4, 3, 4)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(317, 149)
        Panel5.TabIndex = 6
        ' 
        ' totdevices
        ' 
        totdevices.AutoSize = True
        totdevices.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totdevices.ForeColor = SystemColors.MenuHighlight
        totdevices.Location = New Point(110, 49)
        totdevices.Name = "totdevices"
        totdevices.Size = New Size(107, 43)
        totdevices.TabIndex = 8
        totdevices.Text = "1000"
        totdevices.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(66, 9)
        Label7.Name = "Label7"
        Label7.Size = New Size(172, 23)
        Label7.TabIndex = 7
        Label7.Text = "TOTAL DEVICES"
        ' 
        ' Panel13
        ' 
        Panel13.BorderStyle = BorderStyle.FixedSingle
        Panel13.Controls.Add(Panel14)
        Panel13.Controls.Add(Label15)
        Panel13.Location = New Point(1091, 340)
        Panel13.Margin = New Padding(3, 4, 3, 4)
        Panel13.Name = "Panel13"
        Panel13.Size = New Size(359, 265)
        Panel13.TabIndex = 11
        ' 
        ' Panel14
        ' 
        Panel14.Location = New Point(-1, 35)
        Panel14.Margin = New Padding(3, 4, 3, 4)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(359, 229)
        Panel14.TabIndex = 11
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(82, 5)
        Label15.Name = "Label15"
        Label15.Size = New Size(199, 23)
        Label15.TabIndex = 7
        Label15.Text = "UNIT ASSIGNMENT"
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
        mainpnl.PerformLayout()
        Panel12.ResumeLayout(False)
        Panel12.PerformLayout()
        CType(recentdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        CType(catdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        CType(activitydgv, ComponentModel.ISupportInitialize).EndInit()
        Panel15.ResumeLayout(False)
        Panel15.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel13.ResumeLayout(False)
        Panel13.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents mainpnl As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents recentdgv As DataGridView
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents catdgv As DataGridView
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents activitydgv As DataGridView
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents totpersonnel As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents totunits As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents totdevices As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Timer2 As Timer

End Class
