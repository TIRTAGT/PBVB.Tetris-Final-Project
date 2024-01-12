Public Class Menu
    Dim TetrisForm As Tetris

    Private Sub ExitAction_Click(sender As Object, e As EventArgs) Handles ExitAction.Click
        Application.Exit()
    End Sub

    Private Sub StartTetrisAction_Click(sender As Object, e As EventArgs) Handles StartTetrisAction.Click
        If TetrisForm Is Nothing OrElse TetrisForm.IsDisposed Then
            TetrisForm = New Tetris()
            AddHandler TetrisForm.FormClosed, AddressOf OnSubFormDitutup
        End If

        TetrisForm.Show()
        Me.Hide()
    End Sub

    Private Sub OnSubFormDitutup(sender As Object, e As FormClosedEventArgs)
        If Not (Tetris Is Nothing) AndAlso Not TetrisForm.IsDisposed Then
            RemoveHandler TetrisForm.FormClosed, AddressOf OnSubFormDitutup
            TetrisForm.Dispose()
            TetrisForm = Nothing
        End If

        Me.Show()
    End Sub
End Class