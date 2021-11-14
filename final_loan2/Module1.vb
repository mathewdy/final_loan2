Imports MySql.Data.MySqlClient
Module Module1
    Public conn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public data_table As New DataTable
    Public data_adapter As New MySqlDataAdapter
    Public data_reader As MySqlDataReader
    Public data As New DataSet

    Sub openCon()
        conn.ConnectionString = "server=localhost;username=root;password=;database=loan_management;SslMode=none;"
        conn.Open()
    End Sub
End Module
