<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QRView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QRView))
        panelqr = New Panel()
        Label7 = New Label()
        printbtn = New Button()
        closepnl = New Panel()
        SuspendLayout()
        ' 
        ' panelqr
        ' 
        panelqr.Location = New Point(6, 30)
        panelqr.Margin = New Padding(3, 2, 3, 2)
        panelqr.Name = "panelqr"
        panelqr.Size = New Size(278, 206)
        panelqr.TabIndex = 0
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(84, 6)
        Label7.Name = "Label7"
        Label7.Size = New Size(121, 18)
        Label7.TabIndex = 6
        Label7.Text = "QR Code View"
        ' 
        ' printbtn
        ' 
        printbtn.Font = New Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        printbtn.Location = New Point(38, 244)
        printbtn.Name = "printbtn"
        printbtn.Size = New Size(203, 36)
        printbtn.TabIndex = 10
        printbtn.Text = "Print QR Sticker"
        printbtn.UseVisualStyleBackColor = True
        ' 
        ' closepnl
        ' 
        closepnl.BackColor = Color.Transparent
        closepnl.BackgroundImage = CType(resources.GetObject("closepnl.BackgroundImage"), Image)
        closepnl.BackgroundImageLayout = ImageLayout.Stretch
        closepnl.Location = New Point(253, 2)
        closepnl.Margin = New Padding(3, 2, 3, 2)
        closepnl.Name = "closepnl"
        closepnl.Size = New Size(31, 25)
        closepnl.TabIndex = 20
        ' 
        ' QRView
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(closepnl)
        Controls.Add(printbtn)
        Controls.Add(Label7)
        Controls.Add(panelqr)
        Margin = New Padding(3, 2, 3, 2)
        Name = "QRView"
        Size = New Size(290, 290)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents panelqr As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents printbtn As Button
    Friend WithEvents closepnl As Panel

End Class
