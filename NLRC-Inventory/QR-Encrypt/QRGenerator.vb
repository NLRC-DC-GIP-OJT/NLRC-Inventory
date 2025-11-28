Imports QRCoder
Imports System.Drawing

Public Class QRGenerator
    Public Shared Function GenerateUnitQR(unitId As Integer) As Bitmap

        ' Encrypt the unit ID
        Dim encryptedText As String = Encrypt.EncryptPointer(unitId)

        ' Generate QR
        Dim qrGen As New QRCodeGenerator()
        Dim qrData = qrGen.CreateQrCode(encryptedText, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrData)

        ' Create QR image
        Dim qrImage As Bitmap = qrCode.GetGraphic(20)

        Return qrImage
    End Function
End Class

