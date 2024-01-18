Public Class Menu
	Dim TetrisForm As Tetris
	Dim HowToPlayForm As HowToPlay

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
		If Not (TetrisForm Is Nothing) AndAlso Not TetrisForm.IsDisposed Then
			RemoveHandler TetrisForm.FormClosed, AddressOf OnSubFormDitutup
			TetrisForm.Dispose()
			TetrisForm = Nothing
		End If

		If Not (HowToPlayForm Is Nothing) AndAlso Not HowToPlayForm.IsDisposed Then
			RemoveHandler HowToPlayForm.FormClosed, AddressOf OnSubFormDitutup
			HowToPlayForm.Dispose()
			HowToPlayForm = Nothing
		End If

		Me.Show()
	End Sub

	Private Sub HowToPlayAction_Click(sender As Object, e As EventArgs) Handles HowToPlayAction.Click
		If HowToPlayForm Is Nothing OrElse HowToPlayForm.IsDisposed Then
			HowToPlayForm = New HowToPlay()
			AddHandler HowToPlayForm.FormClosed, AddressOf OnSubFormDitutup
		End If

		HowToPlayForm.Show()
		Me.Hide()
	End Sub
End Class