<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Units
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
        qrbtngenerate = New Button()
        multibtn = New Button()
        unitaddbtn = New Button()
        filtertxt = New TextBox()
        addbtn = New Button()
        Label3 = New Label()
        Panel2 = New Panel()
        allunitsdgv = New DataGridView()
        unitpnl = New Panel()
        btnNext = New Button()
        btnPrev = New Button()
        lblPageInfo = New Label()
        Panel3 = New Panel()
        savedgvbtn = New Button()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(allunitsdgv, ComponentModel.ISupportInitialize).BeginInit()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(qrbtngenerate)
        Panel1.Controls.Add(multibtn)
        Panel1.Controls.Add(unitaddbtn)
        Panel1.Controls.Add(filtertxt)
        Panel1.Controls.Add(addbtn)
        Panel1.Controls.Add(Label3)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1567, 75)
        Panel1.TabIndex = 0
        ' 
        ' qrbtngenerate
        ' 
        qrbtngenerate.Anchor = AnchorStyles.None
        qrbtngenerate.BackColor = Color.SlateGray
        qrbtngenerate.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        qrbtngenerate.ForeColor = Color.White
        qrbtngenerate.Location = New Point(837, 11)
        qrbtngenerate.Margin = New Padding(3, 2, 3, 2)
        qrbtngenerate.Name = "qrbtngenerate"
        qrbtngenerate.Size = New Size(184, 50)
        qrbtngenerate.TabIndex = 23
        qrbtngenerate.Text = "Generate QR"
        qrbtngenerate.UseVisualStyleBackColor = False
        ' 
        ' multibtn
        ' 
        multibtn.Anchor = AnchorStyles.None
        multibtn.BackColor = Color.SlateGray
        multibtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        multibtn.ForeColor = Color.White
        multibtn.Location = New Point(1036, 10)
        multibtn.Margin = New Padding(3, 2, 3, 2)
        multibtn.Name = "multibtn"
        multibtn.Size = New Size(184, 50)
        multibtn.TabIndex = 22
        multibtn.Text = "Multiple Editing"
        multibtn.UseVisualStyleBackColor = False
        ' 
        ' unitaddbtn
        ' 
        unitaddbtn.Anchor = AnchorStyles.Top
        unitaddbtn.BackColor = Color.SlateGray
        unitaddbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        unitaddbtn.ForeColor = Color.White
        unitaddbtn.Location = New Point(1233, 10)
        unitaddbtn.Margin = New Padding(3, 2, 3, 2)
        unitaddbtn.Name = "unitaddbtn"
        unitaddbtn.Size = New Size(141, 50)
        unitaddbtn.TabIndex = 21
        unitaddbtn.Text = "Create Unit"
        unitaddbtn.UseVisualStyleBackColor = False
        ' 
        ' filtertxt
        ' 
        filtertxt.BorderStyle = BorderStyle.FixedSingle
        filtertxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        filtertxt.Location = New Point(243, 20)
        filtertxt.Margin = New Padding(3, 2, 3, 2)
        filtertxt.Name = "filtertxt"
        filtertxt.Size = New Size(406, 32)
        filtertxt.TabIndex = 20
        ' 
        ' addbtn
        ' 
        addbtn.Anchor = AnchorStyles.Top
        addbtn.BackColor = Color.SlateGray
        addbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addbtn.ForeColor = Color.White
        addbtn.Location = New Point(1384, 11)
        addbtn.Margin = New Padding(3, 2, 3, 2)
        addbtn.Name = "addbtn"
        addbtn.Size = New Size(122, 50)
        addbtn.TabIndex = 16
        addbtn.Text = "Add Unit"
        addbtn.UseVisualStyleBackColor = False
        ' 
        ' Label3
        ' 
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold)
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(36, 19)
        Label3.Name = "Label3"
        Label3.Size = New Size(185, 30)
        Label3.TabIndex = 7
        Label3.Text = "Unit Assignment"
        ' 
        ' Panel2
        ' 
        Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Panel2.AutoSize = True
        Panel2.Controls.Add(allunitsdgv)
        Panel2.Location = New Point(37, 92)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1492, 715)
        Panel2.TabIndex = 1
        ' 
        ' allunitsdgv
        ' 
        allunitsdgv.AllowUserToAddRows = False
        allunitsdgv.AllowUserToDeleteRows = False
        allunitsdgv.AllowUserToResizeColumns = False
        allunitsdgv.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.CornflowerBlue
        DataGridViewCellStyle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = Color.White
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        allunitsdgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        allunitsdgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = SystemColors.Window
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
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
        allunitsdgv.Size = New Size(1492, 715)
        allunitsdgv.TabIndex = 1
        ' 
        ' unitpnl
        ' 
        unitpnl.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        unitpnl.AutoSize = True
        unitpnl.BorderStyle = BorderStyle.FixedSingle
        unitpnl.Location = New Point(274, 127)
        unitpnl.Margin = New Padding(3, 2, 3, 2)
        unitpnl.Name = "unitpnl"
        unitpnl.Size = New Size(1024, 602)
        unitpnl.TabIndex = 2
        unitpnl.Visible = False
        ' 
        ' btnNext
        ' 
        btnNext.Anchor = AnchorStyles.Bottom
        btnNext.AutoSize = True
        btnNext.BackColor = Color.SlateGray
        btnNext.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNext.ForeColor = Color.White
        btnNext.Location = New Point(878, 815)
        btnNext.Margin = New Padding(3, 2, 3, 2)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(94, 52)
        btnNext.TabIndex = 19
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = False
        ' 
        ' btnPrev
        ' 
        btnPrev.Anchor = AnchorStyles.Bottom
        btnPrev.AutoSize = True
        btnPrev.BackColor = Color.SlateGray
        btnPrev.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnPrev.ForeColor = Color.White
        btnPrev.Location = New Point(681, 815)
        btnPrev.Margin = New Padding(3, 2, 3, 2)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(90, 52)
        btnPrev.TabIndex = 18
        btnPrev.Text = "Previous"
        btnPrev.UseVisualStyleBackColor = False
        ' 
        ' lblPageInfo
        ' 
        lblPageInfo.Anchor = AnchorStyles.Bottom
        lblPageInfo.AutoSize = True
        lblPageInfo.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPageInfo.Location = New Point(778, 830)
        lblPageInfo.Name = "lblPageInfo"
        lblPageInfo.Size = New Size(86, 21)
        lblPageInfo.TabIndex = 17
        lblPageInfo.Text = "Page 1 of 1"
        ' 
        ' Panel3
        ' 
        Panel3.AutoSize = True
        Panel3.Controls.Add(savedgvbtn)
        Panel3.Controls.Add(unitpnl)
        Panel3.Controls.Add(Panel1)
        Panel3.Controls.Add(Panel2)
        Panel3.Controls.Add(btnPrev)
        Panel3.Controls.Add(lblPageInfo)
        Panel3.Controls.Add(btnNext)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 0)
        Panel3.Margin = New Padding(3, 2, 3, 2)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1567, 882)
        Panel3.TabIndex = 20
        ' 
        ' savedgvbtn
        ' 
        savedgvbtn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        savedgvbtn.AutoSize = True
        savedgvbtn.BackColor = Color.CornflowerBlue
        savedgvbtn.Font = New Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savedgvbtn.ForeColor = Color.White
        savedgvbtn.Location = New Point(1385, 815)
        savedgvbtn.Margin = New Padding(3, 2, 3, 2)
        savedgvbtn.Name = "savedgvbtn"
        savedgvbtn.Size = New Size(144, 56)
        savedgvbtn.TabIndex = 20
        savedgvbtn.Text = "Save"
        savedgvbtn.UseVisualStyleBackColor = False
        savedgvbtn.Visible = False
        ' 
        ' Units
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Panel3)
        Margin = New Padding(3, 2, 3, 2)
        Name = "Units"
        Size = New Size(1567, 882)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(allunitsdgv, ComponentModel.ISupportInitialize).EndInit()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents unitsdgv As DataGridView
    Friend WithEvents TabMain As TabControl
    Friend WithEvents UnitTab As TabPage
    Friend WithEvents savedgvbtn As Button
    Friend WithEvents AddUnitTab As TabPage
    Friend WithEvents unitpnl As Panel
    Friend WithEvents unitnametxt As TextBox
    Friend WithEvents savebtn As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents allunitsdgv As DataGridView
    Friend WithEvents addbtn As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents filtertxt As TextBox
    Friend WithEvents unitaddbtn As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents multibtn As Button
    Friend WithEvents qrbtngenerate As Button

End Class
