Imports System.Data
Imports MySql.Data.MySqlClient

Public Class Reset
    Dim username As String = SendCode.to_user
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (password1.Text.Equals(password2.Text)) Then
            Try
                openCon()
                cmd.Connection = conn
                cmd.CommandText = "UPDATE admin SET password='" & password2.Text & "' WHERE email= '" & username & "' "
                cmd.ExecuteNonQuery()
                MessageBox.Show("Password reseted successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conn.Close()
                login.Show()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message())
            End Try
        Else
            MessageBox.Show("New Password and Verify Password must be Equal")
        End If

    End Sub


End Class