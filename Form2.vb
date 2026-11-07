Imports System.Data.SqlClient

Public Class Form2
    Private connectionstring As String = "server=DESKTOP-H9AEF5H;database=Library;integrated security=true"
    Private selectedbookid As Integer
    Private DataGridView_Books As Object

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadBooks()

    End Sub
    Private Sub LoadBooks(Optional searchtext As String = "")
        Try
            Using con As New SqlConnection(connectionstring)
                Dim requete As String = "select id_book, Title, Author, Available_Copies from Books"

                Using cmd As New SqlCommand(requete, con)
                    cmd.Parameters.Add("@search", SqlDbType.NVarChar, 200).Value = "%" & TextBox4.Text.Trim() & "%"
                    Using adapter As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()

                        adapter.Fill(dt)

                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using

            DataGridView1.ReadOnly = True
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect
            DataGridView1.MultiSelect = False
            DataGridView1.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnMode.Fill

        Catch ex As Exception
            MessageBox.Show("Error loading Books:" & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Available_Copies As Integer
        If TextBox1.Text.Trim() = "" OrElse
            TextBox2.Text.Trim() = "" Then
            MessageBox.Show("enter the book title and author.")
            Return
        End If
        If Not Integer.TryParse(TextBox3.Text, Available_Copies) _
            OrElse Available_Copies < 0 Then
            MessageBox.Show("enter a valid available copies.")
            Return

        End If

        Try
            Using con As New SqlConnection(connectionstring)
                Dim requete As String = "insert into Books(Title, Author, Available_Copies)" &
                                        "values (@Title, @Author, @Available_Copies)"
                Using cmd As New SqlCommand(requete, con)
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = TextBox1.Text.Trim()
                    cmd.Parameters.Add("@Author", SqlDbType.NVarChar, 150).Value = TextBox2.Text.Trim()
                    cmd.Parameters.Add("@Available_Copies", SqlDbType.Int).Value = TextBox3.Text.Trim()
                    con.Open()
                    cmd.ExecuteNonQuery()

                End Using
            End Using
            MessageBox.Show("Book added successfully.")
            clearfields()
            LoadBooks()
        Catch ex As Exception
            MessageBox.Show("error adding book:" & ex.Message)
        End Try
    End Sub

    Private Sub datagridview_Books_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then Return
        If DataGridView1.Rows(e.RowIndex).IsNewRow Then Return
        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If row.Cells("id_Book").Value Is Nothing OrElse
                row.Cells("id_Book").Value Is DBNull.Value Then
            Return
        End If

        selectedbookid = Convert.ToInt32(row.Cells("id_Book").Value)
        TextBox1.Text = If(row.Cells("Title").Value Is DBNull.Value,
        "",
        Convert.ToString(row.Cells("Title").Value))

        TextBox2.Text = If(row.Cells("Author").Value Is DBNull.Value,
            "",
        Convert.ToString(row.Cells("Author").Value))

        TextBox3.Text = If(row.Cells("Available_Copies").Value Is DBNull.Value,
        "",
        Convert.ToString(row.Cells("Available_Copies").Value))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Available_Copies As Integer
        If selectedbookid = 0 Then
            MessageBox.Show("select a book from the grid first.")
            Return
        End If
        If TextBox1.Text.Trim() = "" OrElse
                TextBox2.Text.Trim() = "" Then
            MessageBox.Show("enter the title and author.")
            Return
        End If
        If Not Integer.TryParse(TextBox3.Text, Available_Copies) OrElse Available_Copies < 0 Then
            MessageBox.Show("enter a valid available_copies.")
            Return
        End If
        Try
            Using con As New SqlConnection(connectionstring)
                Dim requete As String = " update Books set Title=@Title, Author=@Author, " & "Available_Copies=@Available_Copies where id_Book=@id"
                Using cmd As New SqlCommand(requete, con)
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = TextBox1.Text.Trim()
                    cmd.Parameters.Add("@Author", SqlDbType.NVarChar, 150).Value = TextBox2.Text.Trim()
                    cmd.Parameters.Add("@Available_Copies", SqlDbType.Int).Value = TextBox3.Text.Trim()
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = selectedbookid
                    con.Open()
                    cmd.ExecuteNonQuery()

                End Using
            End Using
            MessageBox.Show("book updated successfully.")
            clearfields()
            LoadBooks()

        Catch ex As Exception
            MessageBox.Show("error updating book:" & ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If selectedbookid = 0 Then
            MessageBox.Show("select a book first.")
            Return
        End If

        Try
            Using con As New SqlConnection(connectionstring)
                Dim requete As String = "delete from books where id_Book=@id"
                Using cmd As New SqlCommand(requete, con)
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = selectedbookid
                    con.Open()
                    cmd.ExecuteNonQuery()

                End Using
            End Using
            MessageBox.Show("book deleted successfully.")
            clearfields()
            LoadBooks()

        Catch ex As Exception
            MessageBox.Show(
                "error deleting book. check whether it is linked to a loan. " & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        LoadBooks(TextBox4.Text.Trim)

    End Sub
    Private Sub clearfields()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        selectedbookid = 0


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Using con As New SqlConnection(connectionstring)
            Dim requete As String = "select * from Books where Title like @search or Author like @search"
            Using cmd As New SqlCommand(requete, con)
                cmd.Parameters.AddWithValue("@search", "%" & TextBox4.Text.Trim() & "%")

                Dim adapter As New SqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                DataGridView1.DataSource = dt
            End Using
        End Using
    End Sub
End Class