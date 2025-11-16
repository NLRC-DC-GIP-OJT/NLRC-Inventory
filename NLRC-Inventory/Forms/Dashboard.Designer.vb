<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Panel1 = New Panel()
        Panel4 = New Panel()
        Panel2 = New Panel()
        Panel3 = New Panel()
        Button5 = New Button()
        Button4 = New Button()
        unitbtn = New Button()
        Button2 = New Button()
        Button1 = New Button()
        mainpnl = New Panel()
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
        Label3 = New Label()
        Panel13 = New Panel()
        Panel14 = New Panel()
        Label15 = New Label()
        userlbl = New Label()
        dntlbl = New Label()
        Label4 = New Label()
        Label1 = New Label()
        Label2 = New Label()
        Timer1 = New Timer(components)
        BindingSource1 = New BindingSource(components)
        Timer2 = New Timer(components)
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel3.SuspendLayout()
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
        CType(BindingSource1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Transparent
        Panel1.BackgroundImage = My.Resources.Resources.background
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1370, 749)
        Panel1.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(Panel2)
        Panel4.Controls.Add(Panel3)
        Panel4.Controls.Add(mainpnl)
        Panel4.Controls.Add(userlbl)
        Panel4.Controls.Add(dntlbl)
        Panel4.Controls.Add(Label4)
        Panel4.Controls.Add(Label1)
        Panel4.Controls.Add(Label2)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(0, 0)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1370, 749)
        Panel4.TabIndex = 8
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(21, 12)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(102, 76)
        Panel2.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.White
        Panel3.Controls.Add(Button5)
        Panel3.Controls.Add(Button4)
        Panel3.Controls.Add(unitbtn)
        Panel3.Controls.Add(Button2)
        Panel3.Controls.Add(Button1)
        Panel3.Location = New Point(21, 118)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(270, 606)
        Panel3.TabIndex = 3
        ' 
        ' Button5
        ' 
        Button5.Dock = DockStyle.Top
        Button5.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button5.Location = New Point(0, 269)
        Button5.Margin = New Padding(3, 2, 3, 2)
        Button5.Name = "Button5"
        Button5.Size = New Size(270, 70)
        Button5.TabIndex = 4
        Button5.Text = "SETTINGS"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Dock = DockStyle.Top
        Button4.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button4.Location = New Point(0, 201)
        Button4.Margin = New Padding(3, 2, 3, 2)
        Button4.Name = "Button4"
        Button4.Size = New Size(270, 68)
        Button4.TabIndex = 3
        Button4.Text = "SETTINGS"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' unitbtn
        ' 
        unitbtn.Dock = DockStyle.Top
        unitbtn.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        unitbtn.Image = CType(resources.GetObject("unitbtn.Image"), Image)
        unitbtn.ImageAlign = ContentAlignment.MiddleLeft
        unitbtn.Location = New Point(0, 133)
        unitbtn.Margin = New Padding(3, 2, 3, 2)
        unitbtn.Name = "unitbtn"
        unitbtn.Padding = New Padding(22, 0, 0, 0)
        unitbtn.Size = New Size(270, 68)
        unitbtn.TabIndex = 2
        unitbtn.Text = "UNITS"
        unitbtn.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Dock = DockStyle.Top
        Button2.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        Button2.Image = CType(resources.GetObject("Button2.Image"), Image)
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.Location = New Point(0, 69)
        Button2.Margin = New Padding(3, 2, 3, 2)
        Button2.Name = "Button2"
        Button2.Padding = New Padding(22, 0, 0, 0)
        Button2.Size = New Size(270, 64)
        Button2.TabIndex = 1
        Button2.Text = "    DEVICES" & vbCrLf & "       COMPONENTS"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Dock = DockStyle.Top
        Button1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.ImageAlign = ContentAlignment.MiddleLeft
        Button1.Location = New Point(0, 0)
        Button1.Margin = New Padding(3, 2, 3, 2)
        Button1.Name = "Button1"
        Button1.Padding = New Padding(18, 0, 0, 0)
        Button1.Size = New Size(270, 69)
        Button1.TabIndex = 0
        Button1.Text = "            DASHBOARD"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' mainpnl
        ' 
        mainpnl.BackColor = Color.White
        mainpnl.Controls.Add(Panel12)
        mainpnl.Controls.Add(Panel11)
        mainpnl.Controls.Add(Panel9)
        mainpnl.Controls.Add(Panel15)
        mainpnl.Controls.Add(Panel10)
        mainpnl.Controls.Add(Panel7)
        mainpnl.Controls.Add(Panel6)
        mainpnl.Controls.Add(Panel5)
        mainpnl.Controls.Add(Label3)
        mainpnl.Controls.Add(Panel13)
        mainpnl.Location = New Point(334, 118)
        mainpnl.Margin = New Padding(3, 2, 3, 2)
        mainpnl.Name = "mainpnl"
        mainpnl.Size = New Size(1009, 606)
        mainpnl.TabIndex = 4
        ' 
        ' Panel12
        ' 
        Panel12.BorderStyle = BorderStyle.FixedSingle
        Panel12.Controls.Add(recentdgv)
        Panel12.Controls.Add(Label13)
        Panel12.Location = New Point(42, 397)
        Panel12.Name = "Panel12"
        Panel12.Size = New Size(524, 194)
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
        recentdgv.Location = New Point(-1, 26)
        recentdgv.Margin = New Padding(3, 2, 3, 2)
        recentdgv.Name = "recentdgv"
        recentdgv.ReadOnly = True
        recentdgv.RowHeadersVisible = False
        recentdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        recentdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        recentdgv.Size = New Size(524, 167)
        recentdgv.TabIndex = 9
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label13.Location = New Point(112, 5)
        Label13.Name = "Label13"
        Label13.Size = New Size(312, 18)
        Label13.TabIndex = 7
        Label13.Text = "RECENT UNIT ASSIGNMENT ACTIVITY"
        ' 
        ' Panel11
        ' 
        Panel11.BorderStyle = BorderStyle.FixedSingle
        Panel11.Controls.Add(catdgv)
        Panel11.Controls.Add(Label12)
        Panel11.Location = New Point(329, 175)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(386, 216)
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
        catdgv.Location = New Point(-1, 24)
        catdgv.Margin = New Padding(3, 2, 3, 2)
        catdgv.Name = "catdgv"
        catdgv.ReadOnly = True
        catdgv.RowHeadersVisible = False
        catdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        catdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        catdgv.Size = New Size(386, 191)
        catdgv.TabIndex = 8
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label12.Location = New Point(85, 3)
        Label12.Name = "Label12"
        Label12.Size = New Size(204, 18)
        Label12.TabIndex = 7
        Label12.Text = "DEVICES BY CATEGORY"
        ' 
        ' Panel9
        ' 
        Panel9.BorderStyle = BorderStyle.FixedSingle
        Panel9.Controls.Add(activitydgv)
        Panel9.Controls.Add(Label14)
        Panel9.Location = New Point(572, 397)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(400, 194)
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
        activitydgv.Location = New Point(-1, 26)
        activitydgv.Margin = New Padding(3, 2, 3, 2)
        activitydgv.Name = "activitydgv"
        activitydgv.ReadOnly = True
        activitydgv.RowHeadersVisible = False
        activitydgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle9.BackColor = Color.AliceBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.Black
        activitydgv.RowsDefaultCellStyle = DataGridViewCellStyle9
        activitydgv.Size = New Size(400, 167)
        activitydgv.TabIndex = 9
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label14.Location = New Point(100, 5)
        Label14.Name = "Label14"
        Label14.Size = New Size(172, 18)
        Label14.TabIndex = 7
        Label14.Text = "RECENT ACTIVITIES"
        ' 
        ' Panel15
        ' 
        Panel15.BorderStyle = BorderStyle.FixedSingle
        Panel15.Controls.Add(Panel8)
        Panel15.Controls.Add(Label11)
        Panel15.Location = New Point(42, 175)
        Panel15.Name = "Panel15"
        Panel15.Size = New Size(281, 216)
        Panel15.TabIndex = 12
        ' 
        ' Panel8
        ' 
        Panel8.Location = New Point(-1, 25)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(281, 190)
        Panel8.TabIndex = 13
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label11.Location = New Point(33, 4)
        Label11.Name = "Label11"
        Label11.Size = New Size(199, 18)
        Label11.TabIndex = 7
        Label11.Text = "OPERATIONAL STATUS"
        ' 
        ' Panel10
        ' 
        Panel10.BorderStyle = BorderStyle.FixedSingle
        Panel10.Controls.Add(Label16)
        Panel10.Location = New Point(721, 57)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(251, 158)
        Panel10.TabIndex = 11
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label16.Location = New Point(41, 4)
        Label16.Name = "Label16"
        Label16.Size = New Size(184, 18)
        Label16.TabIndex = 7
        Label16.Text = "SERIAL COMPLIANCE"
        ' 
        ' Panel7
        ' 
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(totpersonnel)
        Panel7.Controls.Add(Label10)
        Panel7.Location = New Point(496, 57)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(219, 112)
        Panel7.TabIndex = 10
        ' 
        ' totpersonnel
        ' 
        totpersonnel.AutoSize = True
        totpersonnel.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totpersonnel.ForeColor = SystemColors.MenuHighlight
        totpersonnel.Location = New Point(70, 51)
        totpersonnel.Name = "totpersonnel"
        totpersonnel.Size = New Size(83, 33)
        totpersonnel.TabIndex = 9
        totpersonnel.Text = "1000"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(32, 7)
        Label10.Name = "Label10"
        Label10.Size = New Size(154, 36)
        Label10.TabIndex = 7
        Label10.Text = "TOTAL ASSIGNED" & vbCrLf & "      PERSONNEL"
        ' 
        ' Panel6
        ' 
        Panel6.BorderStyle = BorderStyle.FixedSingle
        Panel6.Controls.Add(totunits)
        Panel6.Controls.Add(Label8)
        Panel6.Location = New Point(268, 57)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(222, 112)
        Panel6.TabIndex = 9
        ' 
        ' totunits
        ' 
        totunits.AutoSize = True
        totunits.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totunits.ForeColor = SystemColors.MenuHighlight
        totunits.Location = New Point(64, 37)
        totunits.Name = "totunits"
        totunits.Size = New Size(83, 33)
        totunits.TabIndex = 10
        totunits.Text = "1000"
        totunits.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(44, 9)
        Label8.Name = "Label8"
        Label8.Size = New Size(117, 18)
        Label8.TabIndex = 7
        Label8.Text = "TOTAL UNITS"
        ' 
        ' Panel5
        ' 
        Panel5.BorderStyle = BorderStyle.FixedSingle
        Panel5.Controls.Add(totdevices)
        Panel5.Controls.Add(Label7)
        Panel5.Location = New Point(42, 57)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(220, 112)
        Panel5.TabIndex = 6
        ' 
        ' totdevices
        ' 
        totdevices.AutoSize = True
        totdevices.Font = New Font("Arial Rounded MT Bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        totdevices.ForeColor = SystemColors.MenuHighlight
        totdevices.Location = New Point(69, 37)
        totdevices.Name = "totdevices"
        totdevices.Size = New Size(83, 33)
        totdevices.TabIndex = 8
        totdevices.Text = "1000"
        totdevices.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(38, 9)
        Label7.Name = "Label7"
        Label7.Size = New Size(140, 18)
        Label7.TabIndex = 7
        Label7.Text = "TOTAL DEVICES"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(42, 20)
        Label3.Name = "Label3"
        Label3.Size = New Size(149, 30)
        Label3.TabIndex = 5
        Label3.Text = "DASHBOARD"
        ' 
        ' Panel13
        ' 
        Panel13.BorderStyle = BorderStyle.FixedSingle
        Panel13.Controls.Add(Panel14)
        Panel13.Controls.Add(Label15)
        Panel13.Location = New Point(721, 221)
        Panel13.Name = "Panel13"
        Panel13.Size = New Size(251, 170)
        Panel13.TabIndex = 11
        ' 
        ' Panel14
        ' 
        Panel14.Location = New Point(-1, 26)
        Panel14.Name = "Panel14"
        Panel14.Size = New Size(251, 143)
        Panel14.TabIndex = 11
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label15.Location = New Point(22, 5)
        Label15.Name = "Label15"
        Label15.Size = New Size(210, 18)
        Label15.TabIndex = 7
        Label15.Text = "UNIT ASSIGNMENT RATE"
        ' 
        ' userlbl
        ' 
        userlbl.AutoSize = True
        userlbl.Location = New Point(1101, 22)
        userlbl.Name = "userlbl"
        userlbl.Size = New Size(81, 30)
        userlbl.TabIndex = 6
        userlbl.Text = "Label5"
        ' 
        ' dntlbl
        ' 
        dntlbl.AutoSize = True
        dntlbl.Location = New Point(677, 21)
        dntlbl.Name = "dntlbl"
        dntlbl.Size = New Size(164, 30)
        dntlbl.TabIndex = 7
        dntlbl.Text = "Date and Time"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(980, 22)
        Label4.Name = "Label4"
        Label4.Size = New Size(115, 30)
        Label4.TabIndex = 5
        Label4.Text = "Welcome!"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(129, 21)
        Label1.Name = "Label1"
        Label1.Size = New Size(397, 30)
        Label1.TabIndex = 0
        Label1.Text = "National Labor Relations Commission"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 18F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(129, 50)
        Label2.Name = "Label2"
        Label2.Size = New Size(208, 32)
        Label2.TabIndex = 2
        Label2.Text = "Inventory System"
        ' 
        ' Timer1
        ' 
        ' 
        ' Timer2
        ' 
        ' 
        ' Dashboard
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1370, 749)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Dashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Dashboard"
        WindowState = FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel3.ResumeLayout(False)
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
        CType(BindingSource1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button5 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents unitbtn As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents mainpnl As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents userlbl As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dntlbl As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents totdevices As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents totpersonnel As Label
    Friend WithEvents totunits As Label
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Panel8 As Panel
    Friend WithEvents catdgv As DataGridView
    Friend WithEvents recentdgv As DataGridView
    Friend WithEvents Panel14 As Panel
    Friend WithEvents activitydgv As DataGridView
End Class
