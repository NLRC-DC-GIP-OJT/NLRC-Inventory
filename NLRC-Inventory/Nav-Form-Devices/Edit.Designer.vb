<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Edit))
        Label5 = New Label()
        Panel3 = New Panel()
        warrantyDatePicker = New DateTimePicker()
        purchaseDatePicker = New DateTimePicker()
        Label9 = New Label()
        Label1 = New Label()
        cancelbtn = New Button()
        savebtn = New Button()
        Panel2 = New Panel()
        specscb = New ComboBox()
        Label6 = New Label()
        specstxt = New TextBox()
        Panel1 = New Panel()
        serialstxt = New Label()
        serialtxt = New TextBox()
        Button5 = New Button()
        Label7 = New Label()
        notetxt = New TextBox()
        Button6 = New Button()
        Label3 = New Label()
        Label4 = New Label()
        modeltxt = New TextBox()
        catcb = New ComboBox()
        brandcb = New ComboBox()
        Label10 = New Label()
        Panel3.SuspendLayout()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(191, 20)
        Label5.Name = "Label5"
        Label5.Size = New Size(437, 46)
        Label5.TabIndex = 14
        Label5.Text = "Edit Device Information :"
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(warrantyDatePicker)
        Panel3.Controls.Add(purchaseDatePicker)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(cancelbtn)
        Panel3.Controls.Add(savebtn)
        Panel3.Location = New Point(28, 552)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(810, 189)
        Panel3.TabIndex = 18
        ' 
        ' warrantyDatePicker
        ' 
        warrantyDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        warrantyDatePicker.Location = New Point(23, 122)
        warrantyDatePicker.Name = "warrantyDatePicker"
        warrantyDatePicker.Size = New Size(331, 30)
        warrantyDatePicker.TabIndex = 19
        ' 
        ' purchaseDatePicker
        ' 
        purchaseDatePicker.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        purchaseDatePicker.Location = New Point(23, 48)
        purchaseDatePicker.Name = "purchaseDatePicker"
        purchaseDatePicker.Size = New Size(331, 30)
        purchaseDatePicker.TabIndex = 17
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(23, 88)
        Label9.Name = "Label9"
        Label9.Size = New Size(168, 28)
        Label9.TabIndex = 18
        Label9.Text = "Warranty Expires:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(26, 13)
        Label1.Name = "Label1"
        Label1.Size = New Size(165, 28)
        Label1.TabIndex = 17
        Label1.Text = "Purchased Date: "
        ' 
        ' cancelbtn
        ' 
        cancelbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cancelbtn.Location = New Point(452, 114)
        cancelbtn.Name = "cancelbtn"
        cancelbtn.Size = New Size(125, 48)
        cancelbtn.TabIndex = 14
        cancelbtn.Text = "CANCEL"
        cancelbtn.UseVisualStyleBackColor = True
        ' 
        ' savebtn
        ' 
        savebtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        savebtn.Location = New Point(602, 114)
        savebtn.Name = "savebtn"
        savebtn.Size = New Size(125, 48)
        savebtn.TabIndex = 13
        savebtn.Text = "ADD"
        savebtn.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(specscb)
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(specstxt)
        Panel2.Location = New Point(382, 80)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(456, 452)
        Panel2.TabIndex = 17
        ' 
        ' specscb
        ' 
        specscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        specscb.FormattingEnabled = True
        specscb.Location = New Point(13, 50)
        specscb.Name = "specscb"
        specscb.Size = New Size(426, 39)
        specscb.TabIndex = 17
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(13, 11)
        Label6.Name = "Label6"
        Label6.Size = New Size(147, 28)
        Label6.TabIndex = 10
        Label6.Text = "Specifications :"
        ' 
        ' specstxt
        ' 
        specstxt.BorderStyle = BorderStyle.FixedSingle
        specstxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        specstxt.Location = New Point(13, 105)
        specstxt.Multiline = True
        specstxt.Name = "specstxt"
        specstxt.Size = New Size(426, 334)
        specstxt.TabIndex = 1
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(serialstxt)
        Panel1.Controls.Add(serialtxt)
        Panel1.Controls.Add(Button5)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(notetxt)
        Panel1.Controls.Add(Button6)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(modeltxt)
        Panel1.Controls.Add(catcb)
        Panel1.Controls.Add(brandcb)
        Panel1.Controls.Add(Label10)
        Panel1.Location = New Point(28, 81)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(335, 452)
        Panel1.TabIndex = 15
        ' 
        ' serialstxt
        ' 
        serialstxt.AutoSize = True
        serialstxt.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        serialstxt.Location = New Point(24, 246)
        serialstxt.Name = "serialstxt"
        serialstxt.Size = New Size(153, 28)
        serialstxt.TabIndex = 18
        serialstxt.Text = "Serial Number :"
        ' 
        ' serialtxt
        ' 
        serialtxt.BorderStyle = BorderStyle.FixedSingle
        serialtxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        serialtxt.Location = New Point(23, 278)
        serialtxt.Name = "serialtxt"
        serialtxt.Size = New Size(248, 38)
        serialtxt.TabIndex = 17
        ' 
        ' Button5
        ' 
        Button5.BackgroundImage = CType(resources.GetObject("Button5.BackgroundImage"), Image)
        Button5.BackgroundImageLayout = ImageLayout.Stretch
        Button5.FlatAppearance.BorderSize = 0
        Button5.Location = New Point(281, 124)
        Button5.Name = "Button5"
        Button5.Size = New Size(38, 34)
        Button5.TabIndex = 15
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(25, 320)
        Label7.Name = "Label7"
        Label7.Size = New Size(77, 28)
        Label7.TabIndex = 11
        Label7.Text = "Notes :"
        ' 
        ' notetxt
        ' 
        notetxt.BorderStyle = BorderStyle.FixedSingle
        notetxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        notetxt.Location = New Point(23, 354)
        notetxt.Multiline = True
        notetxt.Name = "notetxt"
        notetxt.Size = New Size(284, 85)
        notetxt.TabIndex = 12
        ' 
        ' Button6
        ' 
        Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), Image)
        Button6.BackgroundImageLayout = ImageLayout.Stretch
        Button6.FlatAppearance.BorderSize = 0
        Button6.Location = New Point(281, 48)
        Button6.Name = "Button6"
        Button6.Size = New Size(38, 34)
        Button6.TabIndex = 16
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(19, 11)
        Label3.Name = "Label3"
        Label3.Size = New Size(105, 28)
        Label3.TabIndex = 4
        Label3.Text = "Category :"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(23, 168)
        Label4.Name = "Label4"
        Label4.Size = New Size(87, 28)
        Label4.TabIndex = 8
        Label4.Text = "Model : "
        ' 
        ' modeltxt
        ' 
        modeltxt.BorderStyle = BorderStyle.FixedSingle
        modeltxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        modeltxt.Location = New Point(23, 203)
        modeltxt.Name = "modeltxt"
        modeltxt.Size = New Size(248, 38)
        modeltxt.TabIndex = 1
        ' 
        ' catcb
        ' 
        catcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catcb.FormattingEnabled = True
        catcb.Location = New Point(23, 44)
        catcb.Name = "catcb"
        catcb.Size = New Size(250, 39)
        catcb.TabIndex = 5
        ' 
        ' brandcb
        ' 
        brandcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        brandcb.FormattingEnabled = True
        brandcb.Location = New Point(23, 121)
        brandcb.Name = "brandcb"
        brandcb.Size = New Size(250, 39)
        brandcb.TabIndex = 7
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(23, 88)
        Label10.Name = "Label10"
        Label10.Size = New Size(76, 28)
        Label10.TabIndex = 6
        Label10.Text = "Brand :"
        ' 
        ' Edit
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Controls.Add(Label5)
        Name = "Edit"
        Size = New Size(865, 757)
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents warrantyDatePicker As DateTimePicker
    Friend WithEvents purchaseDatePicker As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cancelbtn As Button
    Friend WithEvents savebtn As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents specscb As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents specstxt As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents serialstxt As Label
    Friend WithEvents serialtxt As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents notetxt As TextBox
    Friend WithEvents Button6 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents modeltxt As TextBox
    Friend WithEvents catcb As ComboBox
    Friend WithEvents brandcb As ComboBox
    Friend WithEvents Label10 As Label

End Class
