Imports MySql.Data.MySqlClient
Imports MessagingToolkit.QRCode.Codec
Imports System.IO
Public Class registration
    Dim gender As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        openCon()
        If text_username.Text = "" Or text_password.Text = "" Or text_first_name.Text = "" Or text_last_name.Text = "" Then
            MessageBox.Show("Please input all details")

        End If
        Dim qr_Code As QRCodeEncoder = New QRCodeEncoder
        Dim img As Image
        Dim btm As Bitmap
        Dim str As String

        str = text_username.Text

        img = qr_Code.Encode(str)
        btm = New Bitmap(img)
        btm.Save(text_username.Text)
        PictureBox1.ImageLocation = text_username.Text
        Try
            cmd.Connection = conn
            cmd.CommandText = "INSERT INTO admin (email,password,first_name,last_name,birth_date,contact_number,gender)
            VALUES ('" & text_username.Text & "', '" & text_password.Text & "', '" & text_first_name.Text & "', '" & text_last_name.Text & "','" & birth_date.Value.ToString() & "',
            '" & text_contact_number.Text & "' ,'" & gender & "');"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data Registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            conn.Close()
            text_contact_number.Clear()
            text_first_name.Clear()
            text_last_name.Clear()
            text_password.Clear()
            text_username.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            text_contact_number.Text = "+63"
        End Try

    End Sub

    Private Sub radio_male_CheckedChanged(sender As Object, e As EventArgs) Handles radio_male.CheckedChanged

        gender = "male"
    End Sub

    Private Sub radio_female_CheckedChanged(sender As Object, e As EventArgs) Handles radio_female.CheckedChanged

        gender = "female"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim sd As New SaveFileDialog
        sd.Filter = "PNG|*.png"
        If sd.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image.Save(sd.FileName, Imaging.ImageFormat.Png)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        text_contact_number.Text = "+63"
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        login.Show()
        Me.Hide()
    End Sub

    Private Sub text_contact_number_KeyPress(sender As Object, e As KeyPressEventArgs) Handles text_contact_number.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub
End Class