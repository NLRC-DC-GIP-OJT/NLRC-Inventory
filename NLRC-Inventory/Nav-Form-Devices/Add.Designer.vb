<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Add
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Add))
        modeltxt = New TextBox()
        Label2 = New Label()
        catcb = New ComboBox()
        Label3 = New Label()
        brandcb = New ComboBox()
        Label4 = New Label()
        Panel1 = New Panel()
        Button5 = New Button()
        Label7 = New Label()
        notetxt = New TextBox()
        Button6 = New Button()
        Label5 = New Label()
        Panel2 = New Panel()
        specscb = New ComboBox()
        Label6 = New Label()
        specstxt = New TextBox()
        Panel3 = New Panel()
        warrantyDatePicker = New DateTimePicker()
        purchaseDatePicker = New DateTimePicker()
        Label9 = New Label()
        Label1 = New Label()
        Button4 = New Button()
        addsbtn = New Button()
        plusBtn = New Button()
        minusBtn = New Button()
        quanttxt = New TextBox()
        Label8 = New Label()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' modeltxt
        ' 
        modeltxt.BorderStyle = BorderStyle.FixedSingle
        modeltxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        modeltxt.Location = New Point(22, 245)
        modeltxt.Name = "modeltxt"
        modeltxt.Size = New Size(248, 38)
        modeltxt.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(19, 15)
        Label2.Name = "Label2"
        Label2.Size = New Size(105, 28)
        Label2.TabIndex = 4
        Label2.Text = "Category :"
        ' 
        ' catcb
        ' 
        catcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        catcb.FormattingEnabled = True
        catcb.Location = New Point(23, 56)
        catcb.Name = "catcb"
        catcb.Size = New Size(250, 39)
        catcb.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(21, 111)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 28)
        Label3.TabIndex = 6
        Label3.Text = "Brand :"
        ' 
        ' brandcb
        ' 
        brandcb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        brandcb.FormattingEnabled = True
        brandcb.Location = New Point(21, 151)
        brandcb.Name = "brandcb"
        brandcb.Size = New Size(250, 39)
        brandcb.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(21, 204)
        Label4.Name = "Label4"
        Label4.Size = New Size(87, 28)
        Label4.TabIndex = 8
        Label4.Text = "Model : "
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Button5)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(notetxt)
        Panel1.Controls.Add(Button6)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(modeltxt)
        Panel1.Controls.Add(catcb)
        Panel1.Controls.Add(brandcb)
        Panel1.Controls.Add(Label3)
        Panel1.Location = New Point(27, 82)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(335, 452)
        Panel1.TabIndex = 9
        ' 
        ' Button5
        ' 
        Button5.BackgroundImage = CType(resources.GetObject("Button5.BackgroundImage"), Image)
        Button5.BackgroundImageLayout = ImageLayout.Stretch
        Button5.FlatAppearance.BorderSize = 0
        Button5.Location = New Point(281, 151)
        Button5.Name = "Button5"
        Button5.Size = New Size(38, 34)
        Button5.TabIndex = 15
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(22, 298)
        Label7.Name = "Label7"
        Label7.Size = New Size(77, 28)
        Label7.TabIndex = 11
        Label7.Text = "Notes :"
        ' 
        ' notetxt
        ' 
        notetxt.BorderStyle = BorderStyle.FixedSingle
        notetxt.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        notetxt.Location = New Point(23, 338)
        notetxt.Multiline = True
        notetxt.Name = "notetxt"
        notetxt.Size = New Size(284, 101)
        notetxt.TabIndex = 12
        ' 
        ' Button6
        ' 
        Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), Image)
        Button6.BackgroundImageLayout = ImageLayout.Stretch
        Button6.FlatAppearance.BorderSize = 0
        Button6.Location = New Point(279, 58)
        Button6.Name = "Button6"
        Button6.Size = New Size(38, 34)
        Button6.TabIndex = 16
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Black", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(210, 20)
        Label5.Name = "Label5"
        Label5.Size = New Size(363, 46)
        Label5.TabIndex = 10
        Label5.Text = "Device Information :"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(specscb)
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(specstxt)
        Panel2.Location = New Point(381, 82)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(456, 452)
        Panel2.TabIndex = 11
        ' 
        ' specscb
        ' 
        specscb.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold)
        specscb.FormattingEnabled = True
        specscb.Location = New Point(13, 55)
        specscb.Name = "specscb"
        specscb.Size = New Size(426, 39)
        specscb.TabIndex = 17
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(13, 15)
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
        ' Panel3
        ' 
        Panel3.Controls.Add(warrantyDatePicker)
        Panel3.Controls.Add(purchaseDatePicker)
        Panel3.Controls.Add(Label9)
        Panel3.Controls.Add(Label1)
        Panel3.Controls.Add(Button4)
        Panel3.Controls.Add(addsbtn)
        Panel3.Controls.Add(plusBtn)
        Panel3.Controls.Add(minusBtn)
        Panel3.Controls.Add(quanttxt)
        Panel3.Controls.Add(Label8)
        Panel3.Location = New Point(27, 554)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(810, 189)
        Panel3.TabIndex = 12
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
        ' Button4
        ' 
        Button4.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button4.Location = New Point(452, 114)
        Button4.Name = "Button4"
        Button4.Size = New Size(125, 48)
        Button4.TabIndex = 14
        Button4.Text = "CANCEL"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' addsbtn
        ' 
        addsbtn.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        addsbtn.Location = New Point(602, 114)
        addsbtn.Name = "addsbtn"
        addsbtn.Size = New Size(125, 48)
        addsbtn.TabIndex = 13
        addsbtn.Text = "ADD"
        addsbtn.UseVisualStyleBackColor = True
        ' 
        ' plusBtn
        ' 
        plusBtn.BackgroundImage = CType(resources.GetObject("plusBtn.BackgroundImage"), Image)
        plusBtn.BackgroundImageLayout = ImageLayout.Stretch
        plusBtn.FlatAppearance.BorderSize = 0
        plusBtn.FlatStyle = FlatStyle.Flat
        plusBtn.Location = New Point(705, 37)
        plusBtn.Name = "plusBtn"
        plusBtn.Size = New Size(26, 24)
        plusBtn.TabIndex = 12
        plusBtn.UseVisualStyleBackColor = True
        ' 
        ' minusBtn
        ' 
        minusBtn.BackgroundImage = CType(resources.GetObject("minusBtn.BackgroundImage"), Image)
        minusBtn.BackgroundImageLayout = ImageLayout.Stretch
        minusBtn.FlatAppearance.BorderSize = 0
        minusBtn.FlatStyle = FlatStyle.Flat
        minusBtn.Location = New Point(573, 30)
        minusBtn.Name = "minusBtn"
        minusBtn.Size = New Size(27, 35)
        minusBtn.TabIndex = 11
        minusBtn.UseVisualStyleBackColor = True
        ' 
        ' quanttxt
        ' 
        quanttxt.BorderStyle = BorderStyle.FixedSingle
        quanttxt.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        quanttxt.Location = New Point(625, 31)
        quanttxt.Name = "quanttxt"
        quanttxt.Size = New Size(64, 34)
        quanttxt.TabIndex = 10
        quanttxt.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(456, 31)
        Label8.Name = "Label8"
        Label8.Size = New Size(107, 28)
        Label8.TabIndex = 9
        Label8.Text = "Quantity : "
        ' 
        ' Add
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        BorderStyle = BorderStyle.FixedSingle
        Controls.Add(Panel3)
        Controls.Add(Label5)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "Add"
        Size = New Size(867, 759)
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents modeltxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents catcb As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents brandcb As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents specstxt As TextBox
    Friend WithEvents notetxt As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents plusBtn As Button
    Friend WithEvents minusBtn As Button
    Friend WithEvents quanttxt As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents addsbtn As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents warrantyDatePicker As DateTimePicker
    Friend WithEvents purchaseDatePicker As DateTimePicker
    Friend WithEvents specscb As ComboBox

End Class
