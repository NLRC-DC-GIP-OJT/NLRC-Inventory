<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reports
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
        Panel1 = New Panel()
        Label1 = New Label()
        devicecb = New ComboBox()
        exportbtn = New Button()
        filtertxt = New TextBox()
        multibtn = New Button()
        unitaddbtn = New Button()
        addbtn = New Button()
        Label3 = New Label()
        btnPrev = New Button()
        lblPageInfo = New Label()
        btnNext = New Button()
        Panel2 = New Panel()
        allunitsdgv = New DataGridView()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(allunitsdgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(devicecb)
        Panel1.Controls.Add(exportbtn)
        Panel1.Controls.Add(filtertxt)
        Panel1.Controls.Add(multibtn)
        Panel1.Controls.Add(unitaddbtn)
        Panel1.Controls.Add(addbtn)
        Panel1.Controls.Add(Label3)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1567, 75)
        Panel1.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(934, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(78, 30)
        Label1.TabIndex = 29
        Label1.Text = "Filter :"
        ' 
        ' devicecb
        ' 
        devicecb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        devicecb.FormattingEnabled = True
        devicecb.Location = New Point(1018, 19)
        devicecb.Margin = New Padding(3, 2, 3, 2)
        devicecb.Name = "devicecb"
        devicecb.Size = New Size(226, 33)
        devicecb.TabIndex = 15
        ' 
        ' exportbtn
        ' 
        exportbtn.BackColor = Color.CornflowerBlue
        exportbtn.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        exportbtn.ForeColor = Color.White
        exportbtn.Location = New Point(1366, 10)
        exportbtn.Margin = New Padding(3, 2, 3, 2)
        exportbtn.Name = "exportbtn"
        exportbtn.Size = New Size(131, 50)
        exportbtn.TabIndex = 28
        exportbtn.Text = "Export"
        exportbtn.UseVisualStyleBackColor = False
        ' 
        ' filtertxt
        ' 
        filtertxt.BorderStyle = BorderStyle.FixedSingle
        filtertxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        filtertxt.Location = New Point(267, 20)
        filtertxt.Margin = New Padding(3, 2, 3, 2)
        filtertxt.Name = "filtertxt"
        filtertxt.Size = New Size(571, 32)
        filtertxt.TabIndex = 24
        ' 
        ' multibtn
        ' 
        multibtn.Anchor = AnchorStyles.None
        multibtn.BackColor = Color.CornflowerBlue
        multibtn.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        multibtn.ForeColor = Color.White
        multibtn.Location = New Point(1681, -7)
        multibtn.Margin = New Padding(3, 2, 3, 2)
        multibtn.Name = "multibtn"
        multibtn.Size = New Size(184, 40)
        multibtn.TabIndex = 22
        multibtn.Text = "Manual Editing"
        multibtn.UseVisualStyleBackColor = False
        ' 
        ' unitaddbtn
        ' 
        unitaddbtn.Anchor = AnchorStyles.Top
        unitaddbtn.BackColor = Color.CornflowerBlue
        unitaddbtn.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        unitaddbtn.ForeColor = Color.White
        unitaddbtn.Location = New Point(1878, 8)
        unitaddbtn.Margin = New Padding(3, 2, 3, 2)
        unitaddbtn.Name = "unitaddbtn"
        unitaddbtn.Size = New Size(141, 40)
        unitaddbtn.TabIndex = 21
        unitaddbtn.Text = "Create Unit"
        unitaddbtn.UseVisualStyleBackColor = False
        ' 
        ' addbtn
        ' 
        addbtn.Anchor = AnchorStyles.Top
        addbtn.BackColor = Color.CornflowerBlue
        addbtn.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(2029, 8)
        addbtn.Margin = New Padding(3, 2, 3, 2)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(122, 40)
        addbtn.TabIndex = 16
        addbtn.Text = "Add Unit"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(28, 20)
        Label3.Name = "Label3"
        Label3.Size = New Size(317, 30)
        Label3.TabIndex = 7
        Label3.Text = "Inventory Reports"
        ' 
        ' btnPrev
        ' 
        btnPrev.Anchor = AnchorStyles.Bottom
        btnPrev.AutoSize = True
        btnPrev.BackColor = Color.CornflowerBlue
        btnPrev.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(673, 818)
        btnPrev.Margin = New Padding(3, 2, 3, 2)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(85, 47)
        btnPrev.TabIndex = 21
        btnPrev.Text = "Previous"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' lblPageInfo
        ' 
        lblPageInfo.Anchor = AnchorStyles.Bottom
        lblPageInfo.AutoSize = True
        lblPageInfo.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPageInfo.Location = New Point(766, 832)
        lblPageInfo.Name = "lblPageInfo"
        lblPageInfo.Size = New Size(86, 21)
        lblPageInfo.TabIndex = 20
        lblPageInfo.Text = "Page 1 of 1"
        ' 
        ' btnNext
        ' 
        btnNext.Anchor = AnchorStyles.Bottom
        btnNext.AutoSize = True
        btnNext.BackColor = Color.CornflowerBlue
        btnNext.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(866, 819)
        btnNext.Margin = New Padding(3, 2, 3, 2)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(94, 47)
        btnNext.TabIndex = 22
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' Panel2
        ' 
        Panel2.AutoSize = True
        Panel2.Controls.Add(allunitsdgv)
        Panel2.Location = New Point(29, 99)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1503, 707)
        Panel2.TabIndex = 23
        ' 
        ' allunitsdgv
        ' 
        allunitsdgv.AllowUserToAddRows = False
        allunitsdgv.AllowUserToDeleteRows = False
        allunitsdgv.AllowUserToResizeColumns = False
        allunitsdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        allunitsdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        allunitsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9.0F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.SkyBlue
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        allunitsdgv.DefaultCellStyle = DataGridViewCellStyle2
        allunitsdgv.Dock = DockStyle.Fill
        allunitsdgv.Location = New Point(0, 0)
        allunitsdgv.Margin = New Padding(3, 2, 3, 2)
        allunitsdgv.Name = "allunitsdgv"
        allunitsdgv.ReadOnly = True
        allunitsdgv.RowHeadersVisible = False
        allunitsdgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle3.ForeColor = Color.Black
        allunitsdgv.RowsDefaultCellStyle = DataGridViewCellStyle3
        allunitsdgv.Size = New Size(1503, 707)
        allunitsdgv.TabIndex = 3
        ' 
        ' Reports
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel2)
        Controls.Add(btnPrev)
        Controls.Add(lblPageInfo)
        Controls.Add(btnNext)
        Controls.Add(Panel1)
        Name = "Reports"
        Size = New Size(1567, 882)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(allunitsdgv, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents multibtn As Button
    Friend WithEvents unitaddbtn As Button
    Friend WithEvents addbtn As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnPrev As Button
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnNext As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents exportbtn As Button
    Friend WithEvents filtertxt As TextBox
    Friend WithEvents allunitsdgv As DataGridView
    Friend WithEvents devicecb As ComboBox
    Friend WithEvents Label1 As Label

End Class
