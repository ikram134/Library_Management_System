Public Class HomeForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnMembers.Click
        Using frm As New Form1
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub BtnBooks_Click(sender As Object, e As EventArgs) Handles BtnBooks.Click
        Using frm As New Form2
            Dim dialogResult1 = frm.ShowDialog()

        End Using
    End Sub

    Private Sub BtnLoans_Click(sender As Object, e As EventArgs) Handles BtnLoans.Click
        Using frm As New Form3
            frm.ShowDialog()

        End Using
    End Sub
End Class