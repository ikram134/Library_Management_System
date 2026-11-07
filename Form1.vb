Imports System.Data.SqlClient
Public Class Form1
    Dim connection_string As String = "server=DESKTOP-H9AEF5H ;database=Library;Integrated security=true;TrustServerCertificate=True;"
    Dim connecte As New SqlConnection(connection_string)
    Dim dt As New DataTable
    Dim adp As New SqlDataAdapter

    Public Property Connection_string1 As String
        Get
            Return connection_string
        End Get
        Set(value As String)
            connection_string = value
        End Set
    End Property

    Private Sub full_name_Click(sender As Object, e As EventArgs) Handles TextBox1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()

        End If
        Dim requete As String = "select * from Members order by id_Member asc"
        Dim cmd As New SqlCommand(requete, connecte)
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(dt)
        DataGridView1.DataSource = dt
        If connecte.State = ConnectionState.Open Then
            connecte.Close()

        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim search_name As String = TextBox4.Text.Trim
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()
        End If
        Dim requete As String = "select * from Members where Full_name like @s"
        Dim cmd As New SqlCommand(requete, connecte)
        cmd.Parameters.Add("@s", SqlDbType.VarChar).Value = search_name
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(dt)
        DataGridView1.DataSource = dt
        If connecte.State = ConnectionState.Open Then
            connecte.Close()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()
        End If
        dt.Clear()

        Dim requete As String = " select * from Members order by id_Member"
        Dim cmd As New SqlCommand(requete, connecte)
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(dt)
        DataGridView1.DataSource = dt
        If connecte.State = ConnectionState.Open Then
            connecte.Close()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Full_Name As String = TextBox1.Text.Trim
        Dim Email As String = TextBox2.Text.Trim
        Dim Join_Date As Date = DateTimePicker1.Value.Date
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()
        End If
        dt.Clear()
        Dim requete As String = "insert into Members(Full_Name,Email,Join_Date)values(@Full_Name,@Email,@Join_Date)"
        Dim cmd As New SqlCommand(requete, connecte)
        cmd.Parameters.Add("@Full_Name", SqlDbType.VarChar).Value = Full_Name
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email
        cmd.Parameters.Add("@Join_Date", SqlDbType.Date).Value = Join_Date
        cmd.ExecuteNonQuery()
        If connecte.State = ConnectionState.Open Then
            connecte.Close()

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Full_Name As String = TextBox4.Text
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()

        End If
        If TextBox4.Text = "" Then
            MsgBox("enter a valid name in textbox4!")
            Exit Sub
        End If
        dt.Clear()
        Dim requete As String = " delete from Members where Full_Name = @Full_Name"
        Dim cmd As New SqlCommand(requete, connecte)
        cmd.Parameters.Add("@Full_Name", SqlDbType.VarChar).Value = Full_Name
        cmd.ExecuteNonQuery()

        If connecte.State = ConnectionState.Open Then
            connecte.Close()

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim Full_Name As String = TextBox1.Text.Trim
        Dim Email As String = TextBox2.Text.Trim
        Dim Join_Date As Date = DateTimePicker1.Value.Date
        Dim id As Integer = Convert.ToInt32(TextBox4.Text)
        If TextBox4.Text = "" Then
            MsgBox("enter a valid id in textbox4")
            Exit Sub
        End If
        If connecte.State = ConnectionState.Closed Then
            connecte.Open()

        End If
        dt.Clear()
        Dim requete As String = "update Members set Full_Name=@a,Email=@b,Join_Date=@op where id_Member=@id"
        Dim cmd As New SqlCommand(requete, connecte)
        cmd.Parameters.Add("@a", SqlDbType.VarChar).Value = Full_Name
        cmd.Parameters.Add("@b", SqlDbType.VarChar).Value = Email
        cmd.Parameters.Add("@op", SqlDbType.Date).Value = Join_Date
        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
        cmd.ExecuteNonQuery()

        If connecte.State = ConnectionState.Open Then
            connecte.Close()

        End If
    End Sub
End Class
