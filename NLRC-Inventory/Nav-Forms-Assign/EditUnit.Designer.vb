<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditUnit))
        Panel1 = New Panel()
        Panel2 = New Panel()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        TextBox1 = New TextBox()
        ComboBox2 = New ComboBox()
        TextBox2 = New TextBox()
        ComboBox5 = New ComboBox()
        ListView1 = New ListView()
        savebtn1 = New Button()
        ListView2 = New ListView()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = My.Resources.Resources.BG
        Panel1.Controls.Add(Panel2)
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Margin = New Padding(3, 4, 3, 4)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(992, 47)
        Panel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Location = New Point(945, 7)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(35, 33)
        Panel2.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Arial Rounded MT Bold", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(7, 8)
        Label1.Name = "Label1"
        Label1.Size = New Size(284, 32)
        Label1.TabIndex = 1
        Label1.Text = "Edit Unit Information"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(22, 64)
        Label2.Name = "Label2"
        Label2.Size = New Size(117, 23)
        Label2.TabIndex = 1
        Label2.Text = "Unit Name:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(490, 69)
        Label3.Name = "Label3"
        Label3.Size = New Size(131, 23)
        Label3.TabIndex = 2
        Label3.Text = "Assigned to:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(26, 129)
        Label4.Name = "Label4"
        Label4.Size = New Size(94, 23)
        Label4.TabIndex = 3
        Label4.Text = "Devices:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(490, 129)
        Label5.Name = "Label5"
        Label5.Size = New Size(77, 23)
        Label5.TabIndex = 4
        Label5.Text = "Specs:"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(490, 265)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(448, 157)
        TextBox1.TabIndex = 5
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(703, 64)
        ComboBox2.Margin = New Padding(3, 4, 3, 4)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(229, 28)
        ComboBox2.TabIndex = 7
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(151, 64)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(229, 27)
        TextBox2.TabIndex = 8
        ' 
        ' ComboBox5
        ' 
        ComboBox5.FormattingEnabled = True
        ComboBox5.Location = New Point(490, 175)
        ComboBox5.Margin = New Padding(3, 4, 3, 4)
        ComboBox5.Name = "ComboBox5"
        ComboBox5.Size = New Size(448, 28)
        ComboBox5.TabIndex = 12
        ' 
        ' ListView1
        ' 
        ListView1.Location = New Point(26, 156)
        ListView1.Margin = New Padding(3, 4, 3, 4)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(399, 177)
        ListView1.TabIndex = 13
        ListView1.UseCompatibleStateImageBehavior = False
        ' 
        ' savebtn1
        ' 
        savebtn1.BackColor = Color.CornflowerBlue
        savebtn1.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn1.ForeColor = Color.White
        savebtn1.Location = New Point(865, 589)
        savebtn1.Name = "savebtn1"
        savebtn1.Size = New Size(89, 52)
        savebtn1.TabIndex = 17
        savebtn1.Text = "Save"
        savebtn1.UseVisualStyleBackColor = False
        ' 
        ' ListView2
        ' 
        ListView2.Location = New Point(26, 374)
        ListView2.Margin = New Padding(3, 4, 3, 4)
        ListView2.Name = "ListView2"
        ListView2.Size = New Size(399, 207)
        ListView2.TabIndex = 18
        ListView2.UseCompatibleStateImageBehavior = False
        ' 
        ' EditUnit
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(ListView2)
        Controls.Add(savebtn1)
        Controls.Add(ListView1)
        Controls.Add(ComboBox5)
        Controls.Add(TextBox2)
        Controls.Add(ComboBox2)
        Controls.Add(TextBox1)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Panel1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "EditUnit"
        Size = New Size(992, 682)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents savebtn1 As Button
    Friend WithEvents ListView2 As ListView
    Friend WithEvents Panel2 As Panel

End Class
