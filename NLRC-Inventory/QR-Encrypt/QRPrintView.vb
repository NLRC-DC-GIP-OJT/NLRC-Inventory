Public Class QRPrintView

    Private Sub btnGenerateQR_Click(sender As Object, e As EventArgs)
        ' TODO: your QR logic here...

        ' Find the host form
        Dim hostForm = FindForm
        If hostForm Is Nothing Then
            ' fallback: hide just my direct parent panel
            Dim parentPanel = TryCast(Parent, Panel)
            If parentPanel IsNot Nothing Then parentPanel.Visible = False
            Return
        End If

        ' 1️⃣ Try to hide mainqrpanel if it exists
        Dim mainFound = hostForm.Controls.Find("mainqrpanel", True)
        If mainFound IsNot Nothing AndAlso mainFound.Length > 0 Then
            Dim mainPanel = TryCast(mainFound(0), Panel)
            If mainPanel IsNot Nothing Then
                mainPanel.Visible = False
                Return
            End If
        End If

        ' 2️⃣ Otherwise, hide printqrformat panel if it exists
        Dim printFound = hostForm.Controls.Find("printqrformat", True)
        If printFound IsNot Nothing AndAlso printFound.Length > 0 Then
            Dim printPanel = TryCast(printFound(0), Panel)
            If printPanel IsNot Nothing Then
                printPanel.Visible = False
                Return
            End If
        End If

        ' 3️⃣ Last fallback: just hide my direct parent
        Dim parentP = TryCast(Parent, Panel)
        If parentP IsNot Nothing Then
            parentP.Visible = False
        End If
    End Sub

End Class

