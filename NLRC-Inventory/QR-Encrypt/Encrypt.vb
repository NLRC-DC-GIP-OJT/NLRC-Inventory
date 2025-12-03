Imports System.Security.Cryptography
Imports System.Text

Public Class Encrypt

    Private Shared ReadOnly passphrase As String = "My$up3r#Strong!P@ssw0rd?"
    Private Shared ReadOnly aesKey() As Byte

    Shared Sub New()
        ' aesKey = SHA256(passphrase)
        Using sha As SHA256 = SHA256.Create()
            aesKey = sha.ComputeHash(Encoding.UTF8.GetBytes(passphrase)) ' 32 bytes → AES-256
        End Using
    End Sub

    ' ===========================================
    ' VB.NET EQUIVALENT OF JS encryptPointer
    ' ===========================================
    Public Shared Function EncryptPointer(pointer As Integer) As String
        Dim plaintext As String = pointer.ToString()
        Dim plainBytes As Byte() = Encoding.UTF8.GetBytes(plaintext)

        Using aes As Aes = Aes.Create()
            aes.KeySize = 256
            aes.BlockSize = 128
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            aes.Key = aesKey

            ' Random IV per encryption (like crypto.randomBytes(16))
            aes.GenerateIV()
            Dim iv As Byte() = aes.IV

            Using encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, iv)
                Dim cipherBytes As Byte() = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length)

                ' Result = IV || ciphertext → base64
                Dim result(iv.Length + cipherBytes.Length - 1) As Byte
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length)
                Buffer.BlockCopy(cipherBytes, 0, result, iv.Length, cipherBytes.Length)

                Return Convert.ToBase64String(result)
            End Using
        End Using
    End Function

    ' ===========================================
    ' VB.NET EQUIVALENT OF JS decryptPointer
    ' ===========================================
    Public Shared Function DecryptPointer(ivEncrypted As String) As Integer
        If String.IsNullOrWhiteSpace(ivEncrypted) Then
            Throw New ArgumentException("Encrypted value is empty.")
        End If

        ' If for some reason you ever kept "ENC:" prefix, strip it
        If ivEncrypted.StartsWith("ENC:", StringComparison.OrdinalIgnoreCase) Then
            ivEncrypted = ivEncrypted.Substring(4)
        End If

        Dim data As Byte()
        Try
            data = Convert.FromBase64String(ivEncrypted)
        Catch ex As FormatException
            Throw New ArgumentException("Encrypted value is not valid Base64.", ex)
        End Try

        If data.Length <= 16 Then
            Throw New ArgumentException("Encrypted data is too short to contain IV + ciphertext.")
        End If

        ' First 16 bytes → IV, rest → ciphertext
        Dim iv(15) As Byte
        Dim cipherBytes(data.Length - 17) As Byte

        Buffer.BlockCopy(data, 0, iv, 0, 16)
        Buffer.BlockCopy(data, 16, cipherBytes, 0, cipherBytes.Length)

        Using aes As Aes = Aes.Create()
            aes.KeySize = 256
            aes.BlockSize = 128
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            aes.Key = aesKey
            aes.IV = iv

            Using decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
                Dim plainBytes As Byte()
                Try
                    plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length)
                Catch ex As CryptographicException
                    Throw New CryptographicException("Decryption failed – data may be tampered or key/IV mismatched.", ex)
                End Try

                Dim plaintext As String = Encoding.UTF8.GetString(plainBytes)

                Dim pointer As Integer
                If Not Integer.TryParse(plaintext, pointer) Then
                    Throw New FormatException("Decrypted text is not a valid integer: '" & plaintext & "'")
                End If

                Return pointer
            End Using
        End Using
    End Function

End Class
