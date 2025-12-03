<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configuration
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
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configuration))
        Label3 = New Label()
        Panel1 = New Panel()
        propertyflowpnl = New FlowLayoutPanel()
        catbtn = New Button()
        Label6 = New Label()
        cattxt = New TextBox()
        Label5 = New Label()
        Label1 = New Label()
        brandbtn = New Button()
        brandtxt = New TextBox()
        Label8 = New Label()
        catcb = New ComboBox()
        Label7 = New Label()
        Label2 = New Label()
        Panel3 = New Panel()
        flowpnl = New FlowLayoutPanel()
        btnInsertSpecs = New Button()
        Label10 = New Label()
        catcb1 = New ComboBox()
        addspecsbtn = New Button()
        Label9 = New Label()
        Label4 = New Label()
        devicedgv = New DataGridView()
        specsdgv = New DataGridView()
        branddgv = New DataGridView()
        Panel2 = New Panel()
        Panel6 = New Panel()
        Panel4 = New Panel()
        Panel5 = New Panel()
        Panel7 = New Panel()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        CType(devicedgv, ComponentModel.ISupportInitialize).BeginInit()
        CType(specsdgv, ComponentModel.ISupportInitialize).BeginInit()
        CType(branddgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel6.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        Panel7.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(7, 5)
        Label3.Name = "Label3"
        Label3.Size = New Size(238, 30)
        Label3.TabIndex = 7
        Label3.Text = "Configuration Section"
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel1.BackColor = Color.White
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(propertyflowpnl)
        Panel1.Controls.Add(catbtn)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(cattxt)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(37, 80)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(462, 365)
        Panel1.TabIndex = 8
        ' 
        ' propertyflowpnl
        ' 
        propertyflowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        propertyflowpnl.AutoScroll = True
        propertyflowpnl.BorderStyle = BorderStyle.FixedSingle
        propertyflowpnl.FlowDirection = FlowDirection.TopDown
        propertyflowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        propertyflowpnl.Location = New Point(18, 128)
        propertyflowpnl.Margin = New Padding(3, 2, 3, 2)
        propertyflowpnl.Name = "propertyflowpnl"
        propertyflowpnl.Size = New Size(422, 179)
        propertyflowpnl.TabIndex = 10
        propertyflowpnl.WrapContents = False
        ' 
        ' catbtn
        ' 
        catbtn.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        catbtn.BackColor = Color.DarkBlue
        catbtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        catbtn.ForeColor = Color.White
        catbtn.Location = New Point(99, 311)
        catbtn.Margin = New Padding(3, 2, 3, 2)
        catbtn.Name = "catbtn"
        catbtn.Size = New Size(260, 48)
        catbtn.TabIndex = 5
        catbtn.Text = "Insert Category"
        catbtn.UseVisualStyleBackColor = False
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label6.AutoSize = True
        Label6.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(18, 100)
        Label6.Name = "Label6"
        Label6.Size = New Size(97, 18)
        Label6.TabIndex = 3
        Label6.Text = "Properties:"
        ' 
        ' cattxt
        ' 
        cattxt.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        cattxt.BorderStyle = BorderStyle.FixedSingle
        cattxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        cattxt.Location = New Point(46, 67)
        cattxt.Margin = New Padding(3, 2, 3, 2)
        cattxt.Name = "cattxt"
        cattxt.Size = New Size(370, 26)
        cattxt.TabIndex = 2
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(18, 39)
        Label5.Name = "Label5"
        Label5.Size = New Size(137, 18)
        Label5.TabIndex = 1
        Label5.Text = "Category Name:"
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.RoyalBlue
        Label1.Location = New Point(129, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(196, 25)
        Label1.TabIndex = 0
        Label1.Text = "Add Device Category"
        ' 
        ' brandbtn
        ' 
        brandbtn.Anchor = AnchorStyles.None
        brandbtn.BackColor = Color.Green
        brandbtn.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        brandbtn.ForeColor = Color.White
        brandbtn.Location = New Point(177, 195)
        brandbtn.Margin = New Padding(3, 2, 3, 2)
        brandbtn.Name = "brandbtn"
        brandbtn.Size = New Size(144, 39)
        brandbtn.TabIndex = 6
        brandbtn.Text = "Insert Brand"
        brandbtn.UseVisualStyleBackColor = False
        ' 
        ' brandtxt
        ' 
        brandtxt.Anchor = AnchorStyles.None
        brandtxt.BorderStyle = BorderStyle.FixedSingle
        brandtxt.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        brandtxt.Location = New Point(91, 128)
        brandtxt.Margin = New Padding(3, 2, 3, 2)
        brandtxt.Name = "brandtxt"
        brandtxt.Size = New Size(313, 26)
        brandtxt.TabIndex = 5
        ' 
        ' Label8
        ' 
        Label8.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label8.AutoSize = True
        Label8.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(15, 102)
        Label8.Name = "Label8"
        Label8.Size = New Size(112, 18)
        Label8.TabIndex = 5
        Label8.Text = "Brand Name:"
        ' 
        ' catcb
        ' 
        catcb.Anchor = AnchorStyles.None
        catcb.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        catcb.FormattingEnabled = True
        catcb.Location = New Point(91, 68)
        catcb.Margin = New Padding(3, 2, 3, 2)
        catcb.Name = "catcb"
        catcb.Size = New Size(313, 26)
        catcb.TabIndex = 6
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(15, 41)
        Label7.Name = "Label7"
        Label7.Size = New Size(192, 18)
        Label7.TabIndex = 5
        Label7.Text = "Select Category Name:"
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.ForestGreen
        Label2.Location = New Point(203, 8)
        Label2.Name = "Label2"
        Label2.Size = New Size(105, 25)
        Label2.TabIndex = 1
        Label2.Text = "Add Brand"
        ' 
        ' Panel3
        ' 
        Panel3.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel3.BackColor = Color.White
        Panel3.BorderStyle = BorderStyle.FixedSingle
        Panel3.Controls.Add(flowpnl)
        Panel3.Controls.Add(btnInsertSpecs)
        Panel3.Controls.Add(Label10)
        Panel3.Controls.Add(catcb1)
        Panel3.Controls.Add(addspecsbtn)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label4)
        Panel3.Location = New Point(1032, 78)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(488, 365)
        Panel3.TabIndex = 9
        ' 
        ' flowpnl
        ' 
        flowpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        flowpnl.AutoScroll = True
        flowpnl.BorderStyle = BorderStyle.FixedSingle
        flowpnl.FlowDirection = FlowDirection.TopDown
        flowpnl.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        flowpnl.Location = New Point(24, 128)
        flowpnl.Margin = New Padding(3, 2, 3, 2)
        flowpnl.Name = "flowpnl"
        flowpnl.Size = New Size(438, 131)
        flowpnl.TabIndex = 9
        flowpnl.WrapContents = False
        ' 
        ' btnInsertSpecs
        ' 
        btnInsertSpecs.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        btnInsertSpecs.BackColor = Color.DarkOrchid
        btnInsertSpecs.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnInsertSpecs.ForeColor = Color.White
        btnInsertSpecs.Location = New Point(95, 306)
        btnInsertSpecs.Margin = New Padding(3, 2, 3, 2)
        btnInsertSpecs.Name = "btnInsertSpecs"
        btnInsertSpecs.Size = New Size(290, 49)
        btnInsertSpecs.TabIndex = 7
        btnInsertSpecs.Text = "Insert Specs Template"
        btnInsertSpecs.UseVisualStyleBackColor = False
        ' 
        ' Label10
        ' 
        Label10.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label10.AutoSize = True
        Label10.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(22, 100)
        Label10.Name = "Label10"
        Label10.Size = New Size(141, 18)
        Label10.TabIndex = 7
        Label10.Text = "Specs Template:"
        ' 
        ' catcb1
        ' 
        catcb1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        catcb1.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        catcb1.FormattingEnabled = True
        catcb1.Location = New Point(24, 66)
        catcb1.Margin = New Padding(3, 2, 3, 2)
        catcb1.Name = "catcb1"
        catcb1.Size = New Size(438, 26)
        catcb1.TabIndex = 7
        ' 
        ' addspecsbtn
        ' 
        addspecsbtn.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        addspecsbtn.FlatAppearance.BorderSize = 2
        addspecsbtn.FlatStyle = FlatStyle.Flat
        addspecsbtn.Font = New Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        addspecsbtn.Location = New Point(76, 269)
        addspecsbtn.Margin = New Padding(3, 2, 3, 2)
        addspecsbtn.Name = "addspecsbtn"
        addspecsbtn.Size = New Size(335, 33)
        addspecsbtn.TabIndex = 8
        addspecsbtn.Text = "+ Add Specification Field"
        addspecsbtn.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(22, 39)
        Label9.Name = "Label9"
        Label9.Size = New Size(192, 18)
        Label9.TabIndex = 7
        Label9.Text = "Select Category Name:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = Color.DarkViolet
        Label4.Location = New Point(161, 8)
        Label4.Name = "Label4"
        Label4.Size = New Size(166, 25)
        Label4.TabIndex = 2
        Label4.Text = "Add Specification"
        ' 
        ' devicedgv
        ' 
        devicedgv.AllowUserToAddRows = False
        devicedgv.AllowUserToDeleteRows = False
        devicedgv.AllowUserToResizeColumns = False
        devicedgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        devicedgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        devicedgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        devicedgv.DefaultCellStyle = DataGridViewCellStyle2
        devicedgv.Dock = DockStyle.Fill
        devicedgv.Location = New Point(0, 0)
        devicedgv.Margin = New Padding(3, 2, 3, 2)
        devicedgv.Name = "devicedgv"
        devicedgv.ReadOnly = True
        devicedgv.RowHeadersVisible = False
        devicedgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        devicedgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        devicedgv.Size = New Size(462, 391)
        devicedgv.TabIndex = 1
        ' 
        ' specsdgv
        ' 
        specsdgv.AllowUserToAddRows = False
        specsdgv.AllowUserToDeleteRows = False
        specsdgv.AllowUserToResizeColumns = False
        specsdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle4.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle4.ForeColor = Color.White
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.True
        specsdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        specsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = SystemColors.Window
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle5.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.False
        specsdgv.DefaultCellStyle = DataGridViewCellStyle5
        specsdgv.Dock = DockStyle.Fill
        specsdgv.Location = New Point(0, 0)
        specsdgv.Margin = New Padding(3, 2, 3, 2)
        specsdgv.Name = "specsdgv"
        specsdgv.ReadOnly = True
        specsdgv.RowHeadersVisible = False
        specsdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle6.ForeColor = Color.Black
        specsdgv.RowsDefaultCellStyle = DataGridViewCellStyle6
        specsdgv.Size = New Size(488, 391)
        specsdgv.TabIndex = 1
        ' 
        ' branddgv
        ' 
        branddgv.AllowUserToAddRows = False
        branddgv.AllowUserToDeleteRows = False
        branddgv.AllowUserToResizeColumns = False
        branddgv.AllowUserToResizeRows = False
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle7.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle7.ForeColor = Color.White
        DataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.True
        branddgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        branddgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = SystemColors.Window
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle8.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.False
        branddgv.DefaultCellStyle = DataGridViewCellStyle8
        branddgv.Dock = DockStyle.Fill
        branddgv.Location = New Point(0, 0)
        branddgv.Margin = New Padding(3, 2, 3, 2)
        branddgv.Name = "branddgv"
        branddgv.ReadOnly = True
        branddgv.RowHeadersVisible = False
        branddgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle9.BackColor = Color.AliceBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = Color.Black
        branddgv.RowsDefaultCellStyle = DataGridViewCellStyle9
        branddgv.Size = New Size(477, 391)
        branddgv.TabIndex = 1
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.BackColor = Color.White
        Panel2.BorderStyle = BorderStyle.FixedSingle
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(brandbtn)
        Panel2.Controls.Add(Label7)
        Panel2.Controls.Add(catcb)
        Panel2.Controls.Add(brandtxt)
        Panel2.Controls.Add(Label8)
        Panel2.Location = New Point(526, 80)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(477, 365)
        Panel2.TabIndex = 14
        ' 
        ' Panel6
        ' 
        Panel6.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel6.Controls.Add(branddgv)
        Panel6.Location = New Point(526, 458)
        Panel6.Margin = New Padding(3, 2, 3, 2)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(477, 391)
        Panel6.TabIndex = 15
        ' 
        ' Panel4
        ' 
        Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel4.Controls.Add(specsdgv)
        Panel4.Location = New Point(1032, 458)
        Panel4.Margin = New Padding(3, 2, 3, 2)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(488, 391)
        Panel4.TabIndex = 16
        ' 
        ' Panel5
        ' 
        Panel5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel5.Controls.Add(devicedgv)
        Panel5.Location = New Point(37, 458)
        Panel5.Margin = New Padding(3, 2, 3, 2)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(462, 391)
        Panel5.TabIndex = 16
        ' 
        ' Panel7
        ' 
        Panel7.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        Panel7.BackColor = Color.Transparent
        Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), Image)
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(Label3)
        Panel7.Location = New Point(18, 23)
        Panel7.Margin = New Padding(3, 2, 3, 2)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(1526, 44)
        Panel7.TabIndex = 17
        ' 
        ' Configuration
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel7)
        Controls.Add(Panel4)
        Controls.Add(Panel5)
        Controls.Add(Panel6)
        Controls.Add(Panel2)
        Controls.Add(Panel3)
        Controls.Add(Panel1)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Configuration"
        Size = New Size(1567, 882)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(devicedgv, ComponentModel.ISupportInitialize).EndInit()
        CType(specsdgv, ComponentModel.ISupportInitialize).EndInit()
        CType(branddgv, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel6.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cattxt As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents catcb As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents brandtxt As TextBox
    Friend WithEvents catcb1 As ComboBox
    Friend WithEvents catbtn As Button
    Friend WithEvents brandbtn As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents btnInsertSpecs As Button
    Friend WithEvents addspecsbtn As Button
    Friend WithEvents flowpnl As FlowLayoutPanel
    Friend WithEvents devicedgv As DataGridView
    Friend WithEvents specsdgv As DataGridView
    Friend WithEvents branddgv As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents propertyflowpnl As FlowLayoutPanel
    Friend WithEvents Panel7 As Panel

End Class
