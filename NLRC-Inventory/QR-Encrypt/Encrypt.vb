Imports System.Security.Cryptography
Imports System.Text

Public Class Encrypt

    Private Shared passphrase As String = "My$up3r#Strong!P@ssw0rd?"

    Private Shared aesKey As String
    Private Shared aesIV As String

    Shared Sub New()
        GenerateSuperEncryptedKeys()
    End Sub

    Private Shared Sub GenerateSuperEncryptedKeys()
        ' SHA256 the passphrase
        Dim sha As SHA256 = SHA256.Create()
        Dim hash As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(passphrase))

        ' Convert first 16 bytes to super-encrypted style (like your EncryptPassword)
        aesKey = ConvertToSuperEncrypted(hash.Take(16).ToArray())

        ' Convert last 16 bytes to super-encrypted style
        aesIV = ConvertToSuperEncrypted(hash.Skip(16).Take(16).ToArray())
    End Sub

    ' SUPER ENCRYPTED CHARSET CONVERTER
    Private Shared Function ConvertToSuperEncrypted(bytes As Byte()) As String
        Dim sb As New StringBuilder()

        For i As Integer = 0 To bytes.Length - 1
            Dim b = bytes(i)
            ' Apply your style: reverse + offset + mod 255
            Dim encryptedChar As Char = ChrW(255 - (b + (i + 1)))
            sb.Append(encryptedChar)
        Next

        Return sb.ToString()
    End Function

    ' =============================
    ' AES ENCRYPT
    ' =============================
    Public Shared Function EncryptPointer(pointer As Integer) As String
        Using aes As Aes = Aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(aesKey)
            aes.IV = Encoding.UTF8.GetBytes(aesIV)
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7

            Dim plaintext As String = pointer.ToString()
            Dim plainBytes = Encoding.UTF8.GetBytes(plaintext)

            Dim encryptor = aes.CreateEncryptor()
            Dim encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length)

            Return "ENC:" & Convert.ToBase64String(encryptedBytes)
        End Using
    End Function

End Class
