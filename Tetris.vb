Imports WMPLib
Imports System.IO
Imports System.Reflection

Public Class Tetris
    Dim MusicAudioPlayer As WindowsMediaPlayer
    Dim TickGame As Timer
    Dim PapanGame As New DataTetris

    Private Sub Tetris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim a As List(Of List(Of Nullable(Of Color))) = PapanGame.AmbilPointerData()

        a(9)(0) = Color.Red
        a(8)(0) = Color.Red
        a(7)(0) = Color.Red
        a(6)(0) = Color.Red

        a(7)(3) = Color.Yellow
        a(8)(3) = Color.Yellow
        a(8)(4) = Color.Yellow
        a(9)(4) = Color.Yellow

        a(9)(6) = Color.Purple
        a(9)(7) = Color.Purple
        a(8)(7) = Color.Purple
        a(9)(8) = Color.Purple

        a(15)(0) = Color.Maroon
        a(15)(1) = Color.Maroon
        a(15)(2) = Color.Maroon
        a(15)(3) = Color.Maroon
        a(15)(4) = Color.Maroon
        a(15)(5) = Color.Maroon
        a(15)(6) = Color.Maroon
        a(15)(7) = Color.Maroon
        a(15)(8) = Color.Maroon
        a(15)(9) = Color.Maroon

        Me.MusicAudioPlayer = New WindowsMediaPlayer
        'Me.MusicAudioPlayer.URL = Path.Combine(Application.StartupPath, "PublicResources/Tetris 99 - Main Theme.mp3")
        'Me.MusicAudioPlayer.controls.play()
        AddHandler Me.MusicAudioPlayer.EndOfStream, AddressOf Me.MusicLoop

        ' Buat timer permainan, speed: 2 FPS
        TickGame = New Timer With {
            .Interval = 450
        }
        AddHandler TickGame.Tick, AddressOf OnGameTick
        TickGame.Enabled = True

        Me.DoubleBuffered = True
        Dim flags As BindingFlags = BindingFlags.Instance Or BindingFlags.NonPublic
        Dim propertyInfo As PropertyInfo = Me.GameArea.GetType().GetProperty("DoubleBuffered", flags)
        propertyInfo.SetValue(Me.GameArea, True, Nothing)
    End Sub

    Private Sub OnGameTick()
        'TickGame.Enabled = False
        Console.WriteLine("tick")
        PapanGame.Turunkan()
        Me.GameArea.Refresh()

        ' Cek jika ada yang lengkap barisnya
        For IndexBaris = 19 To 0 Step -1
            ' Jika baris ini terisi penuh
            If PapanGame.AmbilKumpulanKolom(IndexBaris, True).Count = 10 Then
                Dim a = PapanGame.AmbilPointerData()

                ' Kosongkan baris ini
                For IndexKolom = 0 To 9
                    a(IndexBaris)(IndexKolom) = Nothing
                Next

                ' Refresh
                Me.GameArea.Refresh()
            End If
        Next
    End Sub

    Private Sub MusicLoop()
        Console.WriteLine("MusicLoop()")
        Me.MusicAudioPlayer.controls.play()
    End Sub

    Private Sub GameArea_Paint(sender As Object, e As PaintEventArgs) Handles GameArea.Paint
        Using blackPen As New Pen(Color.Black)
            For i = 32 To GameArea.Size.Width Step 32
                e.Graphics.DrawLine(blackPen, i, 0, i, GameArea.Size.Height)
            Next
        End Using

        For baris = 1 To 20
            For kolom = 1 To 10
                If Not PapanGame.IsKosong(baris - 1, kolom - 1) Then
                    Dim StartX = (kolom * 32) - 32
                    Dim StartY = (baris * 32) - 32

                    Dim a = New Rectangle(StartX, StartY, 32, 32)
                    Dim b = New SolidBrush(PapanGame.AmbilData(baris - 1, kolom - 1))
                    e.Graphics.FillRectangle(b, a)
                End If
            Next
        Next

    End Sub

    Private Sub Tetris_FormClosing()
        If Me.MusicAudioPlayer IsNot Nothing Then
            Me.MusicAudioPlayer.controls.stop()
            Me.MusicAudioPlayer.close()
            Me.MusicAudioPlayer.currentMedia = Nothing
            Me.MusicAudioPlayer = Nothing
        End If
    End Sub
End Class