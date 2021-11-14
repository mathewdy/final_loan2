Imports System.IO
Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Public Class sample
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        For Each Control As Control In Me.Controls
            If TypeOf Control Is TextBox Then
                Control.Text = String.Empty

            End If
        Next
        account_number.Text = ""
        rtb_receipt.Text = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub contact_number_KeyPress(sender As Object, e As KeyPressEventArgs) Handles contact_number.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            query = "UPDATE clients SET account_number = @account_number, full_name=@full_name,address=@address,email_address=@email_address,
            contact_number=@contact_number,amount_loan=@amount_loan,number_of_years=@number_of_years,interest_rate=@interest_rate,monthly_payment=@monthly_payment,total_payment=@total_payment,
            image=@image, date_time_created=@date_time_created WHERE account_number=@account_number"
            conn1.ConnectionString = my_connection
            conn1.Open()
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
            command.Parameters.AddWithValue("@date_time_created", DateTimePicker1.Value)

            command.ExecuteNonQuery()

            MessageBox.Show("Data updated")

            command.Parameters.Clear()
            conn1.Close()

            For Each Control As Control In Me.Controls
                If TypeOf Control Is TextBox Then
                    Control.Text = String.Empty
                End If
            Next
            PictureBox1.Image = Nothing
            account_number.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            query = "DELETE FROM clients WHERE account_number=" & account_number.Text
            conn1.ConnectionString = my_connection
            conn1.Open()
            command.CommandText = query

            command.ExecuteNonQuery()

            MessageBox.Show("Data Deleted")

            command.Parameters.Clear()
            conn1.Close()

            For Each Control As Control In Me.Controls
                If TypeOf Control Is TextBox Then
                    Control.Text = String.Empty
                End If
            Next
            PictureBox1.Image = Nothing
            account_number.Text = ""


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub account_number_KeyPress(sender As Object, e As KeyPressEventArgs) Handles account_number.KeyPress
        If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
            e.Handled = True
            MessageBox.Show("Please input number only")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            rtb_receipt.AppendText("Loan Management Receipt" + vbNewLine)
            rtb_receipt.AppendText("----------------------------------------" + vbNewLine)
            rtb_receipt.AppendText("Enter Amount of Loan:" + vbTab + amount_loan.Text + vbNewLine)
            rtb_receipt.AppendText("Enter the number of Year: " + vbTab + number_of_years.Text + vbNewLine)
            rtb_receipt.AppendText("Enter interest Rate: " + vbTab + interest_rate.Text + vbNewLine)
            rtb_receipt.AppendText("Monthly payment: " + vbTab + monthly_payment.Text + vbNewLine)
            rtb_receipt.AppendText("Total payment: " + vbTab + total_payment.Text + vbNewLine)
            rtb_receipt.AppendText("----------------------------------------" + vbNewLine)
            rtb_receipt.AppendText("----------------Thank You---------------" + vbNewLine)
            rtb_receipt.AppendText("---------------UM Students---------------" + vbNewLine)
            rtb_receipt.AppendText("Date & Time: " + DateTimePicker1.Value + vbNewLine)
            rtb_receipt.AppendText("Admin: " + "Mathew Dy" + vbNewLine)


        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            PrintPreviewDialog1.ShowDialog()
            PrintDocument1.Print()
        End Try

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.rtb_receipt.Width, Me.rtb_receipt.Height)
        rtb_receipt.DrawToBitmap(bm, New Rectangle(30, 30, Me.rtb_receipt.Width, Me.rtb_receipt.Height))
        e.Graphics.DrawImage(bm, 0, 0)
    End Sub

    Private Sub sample_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub sample_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Hide()
        admin_dashboard.Show()

    End Sub

    Dim query As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim opf As New OpenFileDialog

        opf.Filter = "Choose Image(*.JPG;*.PNG;)| *.jpg;*.png"

        If opf.ShowDialog() = DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(opf.FileName())
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim data_reader As MySqlDataReader

        If account_number.Text = "" Or full_name.Text = "" Or address.Text = "" Or email_address.Text = "" Or contact_number.Text = "" Or PictureBox1.Image Is Nothing Then
            MessageBox.Show("Please input all details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


        Try
            conn1.ConnectionString = my_connection
            conn1.Open()

            Using command As New MySqlCommand("SELECT * FROM clients WHERE account_number = '" & account_number.Text & "' Or email_address='" & email_address.Text & "'", conn1)
                command.Parameters.AddWithValue("@account_number", MySqlDbType.VarChar).Value = account_number.Text.Trim
                command.Parameters.AddWithValue("@email_address", MySqlDbType.VarChar).Value = email_address.Text.Trim
                data_reader = command.ExecuteReader()

                While data_reader.Read
                    MessageBox.Show("user already exists")
                    conn1.Close()
                    Exit Sub
                End While
                conn1.Close()
            End Using

            If PictureBox1.Image Is Nothing Then
                MessageBox.Show("Image Required")
            End If

            Dim ms As New MemoryStream()
            PictureBox1.Image.Save(ms, Imaging.ImageFormat.Jpeg)
            arr_image = ms.GetBuffer()
            Dim file_size As UInt32
            file_size = ms.Length

            ms.Close()

            conn1.ConnectionString = my_connection
            conn1.Open()

            Dim mail As New MailMessage
            Dim smtp As New SmtpClient("smtp.gmail.com")
            mail.From = New MailAddress("leonidafrancisco12@gmail.com")
            mail.To.Add(email_address.Text)
            mail.Subject = "Loan Info"
            mail.Body = "Greetings! " & full_name.Text & "Your account Number is 202120" & account_number.Text & "  " & " you availed the loan for students of University of Manila." & " Please be advised that your total payment is " & total_payment.Text & " your monthly payment is " & monthly_payment.Text & " for " & number_of_years.Text & "years, thank you so much"

            smtp.Port = 587
            smtp.Credentials = New System.Net.NetworkCredential("leonidafrancisco12@gmail.com", "nidafrancisco")
            smtp.EnableSsl = True
            smtp.Send(mail)

            query = "INSERT INTO clients (account_number,full_name,address,email_address,contact_number,amount_loan,
            number_of_years,interest_rate,monthly_payment,total_payment,image,date_time_created) VALUES (@account_number,@full_name,
            @address,@email_address,@contact_number,@amount_loan,@number_of_years,@interest_rate,@monthly_payment,@total_payment,@image,@date_time_created)"
            command.Connection = conn1
            command.CommandText = query
            command.Parameters.AddWithValue("@account_number", "202120" + account_number.Text)
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
            command.Parameters.AddWithValue("@date_time_created", DateTimePicker1.Value)
            Dim record As Integer
            record = command.ExecuteNonQuery()


            If record > 0 Then
                MessageBox.Show("Inserted")
            Else
                MessageBox.Show("no record has been save")
            End If
            command.Parameters.Clear()
            conn1.Close()

            For Each Control As Control In Me.Controls
                If TypeOf Control Is TextBox Then
                    Control.Text = String.Empty
                End If
            Next
            PictureBox1.Image = Nothing
            account_number.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        conn1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If account_number.Text = "" Then
            PictureBox1.Image = Nothing
            full_name.Clear()
            address.Clear()
            email_address.Clear()
            contact_number.Clear()
            amount_loan.Clear()
            number_of_years.Clear()
            interest_rate.Clear()
            monthly_payment.Clear()
            total_payment.Clear()
        End If

        query = "SELECT * FROM clients WHERE account_number=" & Val(account_number.Text)
        conn1.ConnectionString = my_connection
        conn1.Open()

        With command
            .Connection = conn1
            .CommandText = query
        End With

        Dim public_table As New DataTable
        Try
            da.SelectCommand = command
            da.Fill(public_table)
            full_name.Text = public_table.Rows(0).Item(2)
            address.Text = public_table.Rows(0).Item(3)
            email_address.Text = public_table.Rows(0).Item(4)
            contact_number.Text = public_table.Rows(0).Item(5)
            amount_loan.Text = public_table.Rows(0).Item(6)
            number_of_years.Text = public_table.Rows(0).Item(7)
            interest_rate.Text = public_table.Rows(0).Item(8)
            monthly_payment.Text = public_table.Rows(0).Item(9)
            total_payment.Text = public_table.Rows(0).Item(10)
            arr_image = public_table.Rows(0).Item(11)
            Dim ms As New MemoryStream(arr_image)
            PictureBox1.Image = Image.FromStream(ms)
        Catch ex As Exception
            MessageBox.Show("No data")
        Finally
            da.Dispose()
            conn1.Close()
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
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