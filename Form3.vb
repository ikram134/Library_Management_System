Imports System.Data.SqlClient
Public Class Form3
    Private connection_string As String = "server=DESKTOP-H9AEF5H ;database=Library;Integrated security=true;TrustServerCertificate=True;"
    Private selectedloanid As Integer = -1

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker3.ShowCheckBox = True
        DateTimePicker3.Checked = False
        LoadMembers()
        LoadBooks()
        LoadLoans()
    End Sub
    Private Sub LoadMembers()
        Using con As New SqlConnection(connection_string)
            Dim requete As String = "select id_Member, Full_Name from Members"
            Dim adapter As New SqlDataAdapter(requete, con)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBox1.DataSource = dt
            ComboBox1.DisplayMember = "Full_Name"
            ComboBox1.ValueMember = "id_Member"
            ComboBox1.SelectedIndex = -1
        End Using
    End Sub

    Private Sub LoadBooks()
        Using con As New SqlConnection(connection_string)
            Dim requete As String = "select id_Book, Title from Books"
            Dim adapter As New SqlDataAdapter(requete, con)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            ComboBox2.DataSource = dt
            ComboBox2.DisplayMember = "Title"
            ComboBox2.ValueMember = "id_Book"
            ComboBox2.SelectedIndex = -1

        End Using
    End Sub
    Private Sub LoadLoans(Optional searchtext As String = "")
        Using con As New SqlConnection(connection_string)
            Dim requete As String = "select * from Loans"
            Using cmd As New SqlCommand(requete, con)
                cmd.Parameters.AddWithValue("@search", "%" & searchtext & "%")
                Dim adapter As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()

                adapter.Fill(dt)

                DataGridView1.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 OrElse ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("please select a member and a book")
            Return
        End If
        If DateTimePicker2.Value.Date < DateTimePicker1.Value.Date Then
            MessageBox.Show("due date cannot be before loan date")
            Return
        End If
        Using con As New SqlConnection(connection_string)
            Dim requete As String = "insert into Loans" & "(id_Book, id_Member, Loan_Date, Due_Date, Return_Date)" &
                "values(@book, @member, @loan, @due, @return)"
            Using cmd As New SqlCommand(requete, con)
                cmd.Parameters.AddWithValue("@book", ComboBox2.SelectedValue)
                cmd.Parameters.AddWithValue("@member", ComboBox1.SelectedValue)
                cmd.Parameters.AddWithValue("@loan", DateTimePicker1.Value.Date)
                cmd.Parameters.AddWithValue("@due", DateTimePicker2.Value.Date)

                If DateTimePicker3.Checked Then
                    cmd.Parameters.AddWithValue("@return", DateTimePicker3.Value.Date)
                Else
                    cmd.Parameters.AddWithValue("@return", DBNull.Value)
                End If

                con.Open()
                cmd.ExecuteNonQuery()

            End Using
        End Using
        MessageBox.Show("loan added successfully.")
        LoadLoans()
        ClearFields()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        selectedloanid = Convert.ToInt32(row.Cells("id_Loan").Value)
        ComboBox2.SelectedValue = row.Cells("id_Book").Value
        ComboBox1.SelectedValue = row.Cells("id_Member").Value

        DateTimePicker1.Value = Convert.ToDateTime(row.Cells("Loan_Date").Value)
        DateTimePicker1.Value = Convert.ToDateTime(row.Cells("Due_Date").Value)

        If row.Cells("return_Date").Value Is DBNull.Value Then
            DateTimePicker3.Checked = False
        Else
            DateTimePicker3.Value = Convert.ToDateTime(row.Cells("Return_Date").Value)
            DateTimePicker3.Checked = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If selectedloanid = -1 Then
            MessageBox.Show("please select a loan first")
            Return
        End If
        If ComboBox1.SelectedIndex = -1 OrElse ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("please select a member and a book")
            Return

        End If
        Using con As New SqlConnection(connection_string)
            Dim requete As String = "update loans set id_Book=@book, id_Member=@member," &
                "Loan_Date=@loan, Due_Date=@due, Return_Date=@return " &
                "where id_Loan=@id "
            Using cmd As New SqlCommand(requete, con)
                cmd.Parameters.AddWithValue("@book", ComboBox2.SelectedValue)
                cmd.Parameters.AddWithValue("@member", ComboBox1.SelectedValue)
                cmd.Parameters.AddWithValue("@loan", DateTimePicker1.Value.Date)
                cmd.Parameters.AddWithValue("@due", DateTimePicker2.Value.Date)
                cmd.Parameters.AddWithValue("@id", selectedloanid)

                If DateTimePicker3.Checked Then
                    cmd.Parameters.AddWithValue("@return", DateTimePicker3.Value.Date)
                Else
                    cmd.Parameters.AddWithValue("@return", DBNull.Value)
                End If

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
        MessageBox.Show("loan updated successfully")
        LoadLoans()
        ClearFields()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If selectedloanid = -1 Then
            MessageBox.Show("please select a loan first")
            Return
        End If
        If MessageBox.Show("are you sure you want to delete this loan?",
"confirm delete",
MessageBoxButtons.YesNo,
MessageBoxIcon.Warning) = DialogResult.Yes Then
            Using con As New SqlConnection(connection_string)
                Dim requete As String = "delete from loans where id_Loan=@id"
                Using cmd As New SqlCommand(requete, con)
                    cmd.Parameters.AddWithValue("@id", selectedloanid)

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MessageBox.Show("loan deleted successfully")
            LoadLoans()
            ClearFields()
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ClearFields()
    End Sub
    Private Sub ClearFields()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        DateTimePicker1.Value = DateTime.Today
        DateTimePicker2.Value = DateTime.Today.AddDays(14)
        DateTimePicker3.Value = DateTime.Today
        DateTimePicker3.Checked = False
        selectedloanid = -1


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        LoadLoans(TextBox1.Text.Trim())
    End Sub
End Class