<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HomeForm
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnMembers = New System.Windows.Forms.Button()
        Me.BtnBooks = New System.Windows.Forms.Button()
        Me.BtnLoans = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnMembers
        '
        Me.BtnMembers.Location = New System.Drawing.Point(127, 178)
        Me.BtnMembers.Name = "BtnMembers"
        Me.BtnMembers.Size = New System.Drawing.Size(192, 63)
        Me.BtnMembers.TabIndex = 0
        Me.BtnMembers.Text = "Members"
        Me.BtnMembers.UseVisualStyleBackColor = True
        '
        'BtnBooks
        '
        Me.BtnBooks.Location = New System.Drawing.Point(414, 178)
        Me.BtnBooks.Name = "BtnBooks"
        Me.BtnBooks.Size = New System.Drawing.Size(192, 63)
        Me.BtnBooks.TabIndex = 1
        Me.BtnBooks.Text = "Books"
        Me.BtnBooks.UseVisualStyleBackColor = True
        '
        'BtnLoans
        '
        Me.BtnLoans.Location = New System.Drawing.Point(289, 291)
        Me.BtnLoans.Name = "BtnLoans"
        Me.BtnLoans.Size = New System.Drawing.Size(192, 63)
        Me.BtnLoans.TabIndex = 2
        Me.BtnLoans.Text = "Loans"
        Me.BtnLoans.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(289, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(182, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "LIBRARY MANAGEMENT SYSTEM"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(183, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Welcome to the Library !"
        '
        'HomeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnLoans)
        Me.Controls.Add(Me.BtnBooks)
        Me.Controls.Add(Me.BtnMembers)
        Me.Name = "HomeForm"
        Me.Text = "HomeForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnMembers As Button
    Friend WithEvents BtnBooks As Button
    Friend WithEvents BtnLoans As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
