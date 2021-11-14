Imports MySql.Data.MySqlClient

Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports MessagingToolkit.QRCode.Codec
Public Class login
    Dim reader As New QRCodeDecoder
    Dim bmp As Bitmap
    Dim camera As VideoCaptureDevice
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        registration.Show()
    End Sub
    Public Sub captured(ByVal sender As Object, ByVal eventArgs As NewFrameEventArgs)
        bmp = DirectCast(eventArgs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventArgs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm

        Try
            If cameras.ShowDialog = Windows.Forms.DialogResult.OK Then
                camera = cameras.VideoDevice
                AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf captured)
                camera.Start()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        End Try



    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim reader As New QRCodeDecoder

        Try
            text_password.Text = reader.decode(New Data.QRCodeBitmapImage(PictureBox1.Image))
            MessageBox.Show("Welcome! " & text_password.Text, "Identity Confirmed")
            Dim admin_main As New admin_dashboard
            admin_dashboard.admin_main = text_password.Text
            admin_dashboard.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Error QR Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        If text_username.Text = "" Or text_password.Text = "" Then
            MessageBox.Show("Please input values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Try
            openCon()
            cmd.Connection = conn
            cmd.CommandText = "SELECT * FROM admin WHERE email='" & text_username.Text & "' AND password='" & text_password.Text & "';"
            Dim data_adapter As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim data_table As DataTable = New DataTable()
            data_adapter.Fill(data_table)

            If (data_table.Rows.Count > 0) Then
                MessageBox.Show("Welcome " + text_username.Text)
                Dim admin_main As New admin_dashboard
                admin_dashboard.admin_main = text_username.Text



                admin_dashboard.Show()
                Me.Hide()
            Else
                MessageBox.Show("Wrong username or password", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                text_password.Text = ""
                text_username.Text = ""
            End If
            conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub text_username_MouseClick(sender As Object, e As MouseEventArgs) Handles text_username.MouseClick
        Button3.Enabled = False
    End Sub

    Private Sub text_password_MouseClick(sender As Object, e As MouseEventArgs) Handles text_password.MouseClick
        Button3.Enabled = False
    End Sub

    Private Sub login_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        Button3.Enabled = True
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim opf As New OpenFileDialog
        opf.Filter = "Choose Image(*.PNG) | *.png"

        If opf.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName)
        End If

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim SC As SendCode = New SendCode()
        SC.Show()
        Dim login As login = New login()
        login.Hide()
    End Sub
End Class
