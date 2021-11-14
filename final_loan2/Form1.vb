Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.IO

Public Class Form1
    Public Shared admin_main
    Dim num_year As Integer
    Dim imonth_pay, itotal_pay As String
    Dim interest, month_interest, loan_amount, monthly_pay, total_pay As Double

    Dim conn1 As New MySqlConnection
    Dim my_connection As String = "server=localhost;username=root;password=;database=loan_management;SslMode=none;"
    Dim command As New MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim result As Integer
    Dim image_path As String
    Dim arr_image() As Byte
    Dim query As String

    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Hide()
        admin_dashboard.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            rtb_receipt.AppendText("Loan Management Receipt" + vbNewLine)
            rtb_receipt.AppendText("----------------------------------------" + vbNewLine)
            rtb_receipt.AppendText("Enter Amount of Loan:" + vbTab + amount_loan.Text + vbNewLine)
            rtb_receipt.AppendText("Enter the number of Year: " + vbTab + number_of_years.Text + vbNewLine)
            rtb_receipt.AppendText("Enter interest Rate: " + vbTab + interest_rate.Text + vbNewLine)
            rtb_receipt.AppendText("Monthly payment: " + vbTab + monthly_payment.Text + vbNewLine)
            rtb_receipt.AppendText("Total payment: " + vbTab + vbTab + vbTab + total_payment.Text + vbNewLine)
            rtb_receipt.AppendText("----------------------------------------" + vbNewLine)
            rtb_receipt.AppendText("----------------Thank You---------------" + vbNewLine)
            rtb_receipt.AppendText("---------------UM Students---------------" + vbNewLine)

        Catch ex As Exception
            MessageBox.Show(ex.ToString())

        End Try

    End Sub

    Private Sub amount_loan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles amount_loan.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub

    Private Sub number_of_years_KeyPress(sender As Object, e As KeyPressEventArgs) Handles number_of_years.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub

    Private Sub interest_rate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles interest_rate.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub

    Private Sub contact_number_KeyPress(sender As Object, e As KeyPressEventArgs) Handles contact_number.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label12.Text = admin_main
        monthly_payment.Enabled = False
        total_payment.Enabled = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'update button
        Try
            openCon()
            cmd.Connection = conn
            cmd.CommandText = "UPDATE clients SET account_number = '" & account_number.Text & "', full_name = '" & full_name.Text & "',
            address='" & address.Text & "', email_address='" & email_address.Text & "', contact_number = '" & contact_number.Text & "', amount_loan='" & amount_loan.Text & "',
            number_of_years = '" & number_of_years.Text & "' , interest_rate = '" & interest_rate.Text & "' , monthly_payment='" & monthly_payment.Text & "',
            total_payment='" & total_payment.Text & "' WHERE account_number='" & account_number.Text & "' ;"
            cmd.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("Updated Info", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each Control As Control In Me.Controls
                If TypeOf Control Is TextBox Then
                    Control.Text = String.Empty
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            openCon()
            cmd.Connection = conn
            cmd.CommandText = "DELETE FROM clients WHERE account_number = '" & account_number.Text & "'"
            cmd.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("Info Deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            For Each Control As Control In Me.Controls
                If TypeOf Control Is TextBox Then
                    Control.Text = String.Empty
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.PrintPreviewControl.Zoom = 1
        PrintPreviewDialog1.ShowDialog()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        query = "Select * from clients where account_number=" & Val(account_number.Text)
        conn1.ConnectionString = my_connection
        conn1.Open()
        With command
            .Connection = conn1
            .CommandText = query
        End With

        Dim arr_image() As Byte
        Dim pub_table As New DataTable

        Try
            da.SelectCommand = command
            da.Fill(pub_table)
            full_name.Text = pub_table.Rows(0).Item(2)
            arr_image = pub_table.Rows(0).Item(11)

            Dim ms As New System.IO.MemoryStream(arr_image)
            PictureBox1.Image = Image.FromStream(ms)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            MessageBox.Show("No data found")
        Finally
            da.Dispose()
            conn1.Close()
        End Try
    End Sub

    Private Sub contact_number_TextChanged(sender As Object, e As EventArgs) Handles contact_number.TextChanged

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        Dim opf As New OpenFileDialog

        opf.Filter = "Choose Image(*.JPG;*.PNG;)| *.jpg;*.png"

        If opf.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName())
        End If
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        For Each Control As Control In Me.Controls
            If TypeOf Control Is TextBox Then
                Control.Text = String.Empty

            End If
        Next
        account_number.Text = "UM-A"
        rtb_receipt.Text = ""
        PictureBox1.Image = Nothing
    End Sub
    Public Sub validation(ByVal query As String)
        Try
            conn1.Open()
            With command
                .Connection = conn1
                .CommandText = query
            End With
            Dim data_table = New DataTable
            da = New MySqlDataAdapter(query, conn1)
            da.Fill(data_table)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            conn1.Close()
            da.Dispose()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If account_number.Text = "" Or full_name.Text = "" Or address.Text = "" Or email_address.Text = "" Or contact_number.Text = "" Or PictureBox1.Image Is Nothing Then
            MessageBox.Show("Please input all details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else

            conn1.ConnectionString = my_connection
            conn1.Open()
            command.Connection = conn1
            Try
                    If PictureBox1.Image Is Nothing Then
                        MessageBox.Show("Image Required")
                    End If

                    Dim ms As New MemoryStream()
                    PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                    arr_image = ms.GetBuffer()

                    Dim FileSize As UInt32
                    FileSize = ms.Length

                    ms.Close()

                    Dim mail As New MailMessage
                    Dim smtp As New SmtpClient("smtp.gmail.com")
                    mail.From = New MailAddress("leonidafrancisco12@gmail.com")
                    mail.To.Add(email_address.Text)
                    mail.Subject = "Loan Info"
                    mail.Body = "Greetings!  " & full_name.Text & " you availed the loan for students of University of Manila." & " Please be advised that your total payment is " & total_payment.Text & " your monthly payment is " & monthly_payment.Text & " for " & number_of_years.Text & "years, thank you so much"

                    smtp.Port = 587
                    smtp.Credentials = New System.Net.NetworkCredential("leonidafrancisco12@gmail.com", "nidafrancisco")
                    smtp.EnableSsl = True
                    smtp.Send(mail)

                    query = "INSERT INTO clients(account_number,full_name,address,email_address,
                    contact_number,amount_loan,number_of_years,interest_rate,monthly_payment,total_payment,image,date_time_created)
                    VALUES (@account_number,@full_name,@address,@email_address,@contact_number,@amount_loan,@number_of_years,@interest_rate,
                    @monthly_payment,@total_payment,@image,@date_time_created);"
                    command.Connection = conn1
                    command.CommandText = query
                    command.Parameters.AddWithValue("@account_number", account_number.Text)
                    command.Parameters.AddWithValue("@full_name", full_name.Text)
                    command.Parameters.AddWithValue("@address", address.Text)
                    command.Parameters.AddWithValue("@email_address", email_address.Text)
                    command.Parameters.AddWithValue("@contact_number", contact_number.Text)
                    command.Parameters.AddWithValue("@amount_loan", amount_loan.Text)
                    command.Parameters.AddWithValue("@number_of_years", number_of_years.Text)
                    command.Parameters.AddWithValue("@interest_rate", interest_rate.Text)
                    command.Parameters.AddWithValue("@monthly_payment", monthly_payment.Text)
                    command.Parameters.AddWithValue("@total_payment", total_payment.Text)
                    command.Parameters.AddWithValue("@image", arr_image)
                    command.Parameters.AddWithValue("@date_time_created", DateTimePicker1.Value())



                    result = command.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show("Loan Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No data saved")
                    End If
                    command.Parameters.Clear()
                    conn1.Close()

                    For Each Control As Control In Me.Controls
                        If TypeOf Control Is TextBox Then
                            Control.Text = String.Empty
                        End If
                    Next
                    PictureBox1.Image = Nothing
                    account_number.Text = "UM-A"
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try
            End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        monthly_payment.Text = Format(Val(monthly_payment.Text), "0.00")
        total_payment.Text = Format(Val(total_payment.Text), "0.00")

        interest = Convert.ToDouble(interest_rate.Text)
        month_interest = interest / 1200
        num_year = Convert.ToInt32(number_of_years.Text)
        loan_amount = Convert.ToDouble(amount_loan.Text)

        monthly_pay = loan_amount * month_interest / (1 - 1 / Math.Pow(1 + month_interest, num_year * 12))

        imonth_pay = Convert.ToString(monthly_pay)
        imonth_pay = "P" & Format(Val(monthly_pay), "0.00")
        monthly_payment.Text = (imonth_pay)

        total_pay = monthly_pay * num_year * 12
        itotal_pay = "P" & Format(Val(total_pay), "0.00")
        total_payment.Text = (itotal_pay)

        amount_loan.Text = (amount_loan.Text)
    End Sub
End Class
