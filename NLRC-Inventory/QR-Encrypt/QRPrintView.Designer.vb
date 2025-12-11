<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QRPrintView
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
        Panel1 = New Panel()
        formatpnl = New Panel()
        qrpnl = New Panel()
        Label5 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Panel2 = New Panel()
        Label6 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        formatprintpnl = New Panel()
        cancelbtn = New Button()
        printbtn = New Button()
        Panel1.SuspendLayout()
        formatprintpnl.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.Controls.Add(formatpnl)
        Panel1.Controls.Add(qrpnl)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label6)
        Panel1.Location = New Point(12, 14)
        Panel1.Margin = New Padding(3, 2, 3, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(581, 326)
        Panel1.TabIndex = 0
        ' 
        ' formatpnl
        ' 
        formatpnl.Location = New Point(10, 119)
        formatpnl.Margin = New Padding(3, 2, 3, 2)
        formatpnl.Name = "formatpnl"
        formatpnl.Size = New Size(334, 200)
        formatpnl.TabIndex = 31
        ' 
        ' qrpnl
        ' 
        qrpnl.Location = New Point(348, 120)
        qrpnl.Margin = New Padding(3, 2, 3, 2)
        qrpnl.Name = "qrpnl"
        qrpnl.Size = New Size(233, 205)
        qrpnl.TabIndex = 30
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(130, 53)
        Label5.Name = "Label5"
        Label5.Size = New Size(369, 18)
        Label5.TabIndex = 28
        Label5.Text = "NATIONAL LABOR RELATIONS COMMISSION"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(150, 29)
        Label3.Name = "Label3"
        Label3.Size = New Size(309, 18)
        Label3.TabIndex = 27
        Label3.Text = "Department of Labor and Employment"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(204, 8)
        Label4.Name = "Label4"
        Label4.Size = New Size(217, 18)
        Label4.TabIndex = 26
        Label4.Text = "Republic of the Philippines"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = My.Resources.Resources.NLRCLogo
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(0, 0)
        Panel2.Margin = New Padding(3, 2, 3, 2)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(107, 86)
        Panel2.TabIndex = 1
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.ForeColor = Color.Red
        Label6.Location = New Point(39, 76)
        Label6.Name = "Label6"
        Label6.Size = New Size(505, 45)
        Label6.TabIndex = 29
        Label6.Text = "PROPERTY INVENTORY STICKER"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.White
        Label2.Location = New Point(17, 346)
        Label2.Name = "Label2"
        Label2.Size = New Size(70, 25)
        Label2.TabIndex = 25
        Label2.Text = "NOTE:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(88, 347)
        Label1.Name = "Label1"
        Label1.Size = New Size(252, 25)
        Label1.TabIndex = 26
        Label1.Text = "PLEASE DO NOT REMOVE!"
        ' 
        ' formatprintpnl
        ' 
        formatprintpnl.BackColor = Color.Navy
        formatprintpnl.Controls.Add(Label2)
        formatprintpnl.Controls.Add(Label1)
        formatprintpnl.Controls.Add(Panel1)
        formatprintpnl.Location = New Point(10, 10)
        formatprintpnl.Margin = New Padding(3, 2, 3, 2)
        formatprintpnl.Name = "formatprintpnl"
        formatprintpnl.Size = New Size(607, 384)
        formatprintpnl.TabIndex = 27
        ' 
        ' cancelbtn
        ' 
        cancelbtn.FlatAppearance.BorderSize = 2
        cancelbtn.FlatStyle = FlatStyle.Flat
        cancelbtn.Font = New Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cancelbtn.Location = New Point(325, 403)
        cancelbtn.Name = "cancelbtn"
        cancelbtn.Size = New Size(292, 43)
        cancelbtn.TabIndex = 39
        cancelbtn.Text = "Cancel"
        cancelbtn.UseVisualStyleBackColor = True
        ' 
        ' printbtn
        ' 
        printbtn.FlatAppearance.BorderSize = 2
        printbtn.FlatStyle = FlatStyle.Flat
        printbtn.Font = New Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        printbtn.Location = New Point(10, 403)
        printbtn.Name = "printbtn"
        printbtn.Size = New Size(292, 43)
        printbtn.TabIndex = 40
        printbtn.Text = "View Print Preview"
        printbtn.UseVisualStyleBackColor = True
        ' 
        ' QRPrintView
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(printbtn)
        Controls.Add(cancelbtn)
        Controls.Add(formatprintpnl)
        Margin = New Padding(3, 2, 3, 2)
        Name = "QRPrintView"
        Size = New Size(626, 454)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        formatprintpnl.ResumeLayout(False)
        formatprintpnl.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents qrpnl As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents formatpnl As Panel
    Friend WithEvents formatprintpnl As Panel
    Friend WithEvents cancelbtn As Button
    Friend WithEvents printbtn As Button

End Class
