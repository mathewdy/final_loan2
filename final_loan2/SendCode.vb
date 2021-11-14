Imports System.Net
Imports System.Net.Mail
Public Class SendCode
    Dim random_code As String
    Public Shared to_user As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim rand As Random = New Random()
            random_code = rand.Next(99999).ToString()
            Dim mail As MailMessage = New MailMessage()
            Dim smtp As New SmtpClient("smtp.gmail.com")
            mail.From = New MailAddress("leonidafrancisco12@gmail.com")
            mail.To.Add(TextBox1.Text)
            mail.Subject = "Password Reset"
            mail.Body = "Hello your code is " + random_code

            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential("leonidafrancisco12@gmail.com", "nidafrancisco")
            smtp.EnableSsl = True

            smtp.Send(mail)
            MessageBox.Show("Please check your email address", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (TextBox2.Text.Equals(random_code)) Then
            to_user = TextBox1.Text
            Dim Reset As Reset = New Reset()
            Reset.Show()
            Me.Hide()
        Else
            MessageBox.Show("Error Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class