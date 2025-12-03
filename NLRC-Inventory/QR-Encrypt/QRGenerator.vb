Imports QRCoder
Imports System.Drawing

Public Class QRGenerator
    Public Shared Function GenerateUnitQR(unitId As Integer, ByRef encryptedText As String) As Bitmap

        ' Encrypt the unit ID (JS-style AES)
        encryptedText = Encrypt.EncryptPointer(unitId)

        ' Generate QR from encrypted text (this is the QR payload)
        Dim qrGen As New QRCodeGenerator()
        Dim qrData = qrGen.CreateQrCode(encryptedText, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrData)

        ' Base QR image (scale = 1; we resize in QRView)
        Dim qrImage As Bitmap = qrCode.GetGraphic(1)

        Return qrImage
    End Function
End Class
