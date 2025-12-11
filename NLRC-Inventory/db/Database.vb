Imports MySql.Data.MySqlClient

Public Class Database

    ' --- main_nlrc_db connection (from Settings → Main) ---
    Public Shared ReadOnly Property ConnectionString As String
        Get
            Return My.Settings.Main
        End Get
    End Property

    Public Shared Function GetConnection() As MySqlConnection
        Return New MySqlConnection(ConnectionString)
    End Function


    ' --- db_nlrc_intranet connection (from Settings → Intranet) ---
    Public Shared ReadOnly Property IntranetConnectionString As String
        Get
            Return My.Settings.Intranet
        End Get
    End Property

    Public Shared Function GetIntranetConnection() As MySqlConnection
        Return New MySqlConnection(IntranetConnectionString)
    End Function

End Class
