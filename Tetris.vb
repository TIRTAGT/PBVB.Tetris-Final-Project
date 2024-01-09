Imports WMPLib
Imports System.IO
Imports System.Reflection

Public Class Tetris
    Private KumpulanKalimatTarget As String() = New String(3) {}
    Dim MusicAudioPlayer As WindowsMediaPlayer
    Dim TickGame As Timer
    Dim PapanGame As New DataTetris
    Dim ObjekKalimat As New DataKalimat
    Dim TotalScore As UInteger = 0

    Private Sub Tetris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GameArea.Font = New Font(FontFamily.GenericMonospace, GameArea.Font.Size, GameArea.Font.Style)
        Dim a = PapanGame.AmbilPointerData()
        'a(0)(0) = "A"
        'a(0)(1) = "B"
        'a(0)(2) = "C"
        'a(0)(3) = "D"
        'a(0)(4) = "E"
        'a(0)(5) = "F"
        'a(1)(0) = "G"
        'a(1)(1) = "H"
        'a(1)(2) = "I"
        'a(1)(3) = "J"
        'a(1)(4) = "K"
        'a(1)(5) = "L"
        'a(2)(0) = "M"
        'a(2)(1) = "N"
        'a(2)(2) = "O"
        'a(2)(3) = "P"
        'a(2)(4) = "Q"
        'a(2)(5) = "R"
        'a(3)(0) = "S"
        'a(3)(1) = "T"
        'a(3)(2) = "U"
        'a(3)(3) = "V"
        'a(3)(4) = "W"
        'a(3)(5) = "X"
        'a(4)(0) = "Y"
        'a(4)(1) = "Z"

        'Me.PapanGame.SetBlokAktif((0, 0))

        ' Generate kalimat dasar
        ObjekKalimat.GenerateKalimatTerpilih(2, 0)
        ObjekKalimat.RefreshHurufTerpilih()
        RefreshUIListKalimat()

        Me.MusicAudioPlayer = New WindowsMediaPlayer
        'Me.MusicAudioPlayer.URL = Path.Combine(Application.StartupPath, "PublicResources/Tetris 99 - Main Theme.mp3")
        'Me.MusicAudioPlayer.controls.play()
        AddHandler Me.MusicAudioPlayer.EndOfStream, AddressOf Me.MusicLoop

        ' Buat timer permainan, speed: 2 FPS
        Me.TickGame = New Timer With {
            .Interval = 550
        }
        AddHandler Me.TickGame.Tick, AddressOf OnGameTick
        Me.TickGame.Enabled = True

        Me.DoubleBuffered = True
        Dim flags As BindingFlags = BindingFlags.Instance Or BindingFlags.NonPublic
        Dim propertyInfo As PropertyInfo = Me.GameArea.GetType().GetProperty("DoubleBuffered", flags)
        propertyInfo.SetValue(Me.GameArea, True, Nothing)
    End Sub

    Private Sub OnGameTick()
        Console.WriteLine("tick")
        'PapanGame.Turunkan()

        ' Jika kehabisan kalimat, refresh
        Dim JumlahKalimatTerpilih = ObjekKalimat.GetKalimatTerpilih().Count
        If JumlahKalimatTerpilih = 0 Then
            ObjekKalimat.GenerateKalimatTerpilih(2, 0)
            ObjekKalimat.RefreshHurufTerpilih()
            RefreshUIListKalimat()
        End If

        ' Jika blok aktif sudah tidak bisa diturunkan, kunci.
        Dim BlokAktifSaatIni = PapanGame.GetBlokAktif()
        If BlokAktifSaatIni.HasValue Then
            If PapanGame.BlokBisaDiturunkan(BlokAktifSaatIni.Value.Item1, BlokAktifSaatIni.Value.Item2) Then
                PapanGame.GeserY_BlokAktif(1)
            Else
                Dim Nilai = DeteksiKataCocok()

                ' Jika ada blok yang cocok, isi semua blok kosong dengan blok ada (turun)
                If Nilai > 0 Then
                    ' Update score
                    TotalScore += Nilai
                    ScoreValueLabel.Text = TotalScore
                    RefreshUIListKalimat()

                    ' Turunkan semua baris yang bisa diturunkan
                    Dim b = PapanGame.Turunkan()
                    While b > 0
                        b = PapanGame.Turunkan()
                    End While
                Else
                    RefreshBlokSelanjutnya()
                End If
            End If
        Else
            RefreshBlokSelanjutnya()
        End If

        ' Refresh area permainan (saat ini 1.8 FPS)
        Me.GameArea.Refresh()

        BlokAktifSaatIni = PapanGame.GetBlokAktif()
        If Not BlokAktifSaatIni.HasValue Then
            If ObjekKalimat.GetKalimatTerpilih(True).Count > 0 Then
                ' Masih ada kalimat tapi tidak bisa menaruh blok, anggap papan permainan penuh dan Game Over

                ' TODO: Aku bingung buat UI bagus untuk game over, tunggu katonisasi aja.
                ' Untuk sementara, end task diri sendiri
                Application.Exit()
            Else

            End If
        End If
    End Sub

    Private Sub RefreshUIListKalimat()
        TargetText1Label.Text = ""
        TargetText2Label.Text = ""
        TargetText3Label.Text = ""
        TargetText4Label.Text = ""
        TargetText5Label.Text = ""
        TargetText6Label.Text = ""
        TargetText7Label.Text = ""
        TargetText8Label.Text = ""

        Dim a = ObjekKalimat.GetKalimatTerpilih()

        TargetText1Label.Text = a(0)
        If a(0)(0) = "~" Then
            TargetText1Label.Font = New Font(TargetText1Label.Font, FontStyle.Strikeout)
            TargetText1Label.Text = TargetText1Label.Text.Substring(1)
        End If
        If a.Count < 2 Then Return

        TargetText2Label.Text = a(1)
        If a(1)(0) = "~" Then
            TargetText2Label.Font = New Font(TargetText2Label.Font, FontStyle.Strikeout)
            TargetText2Label.Text = TargetText2Label.Text.Substring(1)
        End If
        If a.Count < 3 Then Return

        TargetText3Label.Text = a(2)
        If a(2)(0) = "~" Then
            TargetText3Label.Font = New Font(TargetText3Label.Font, FontStyle.Strikeout)
            TargetText3Label.Text = TargetText3Label.Text.Substring(1)
        End If
        If a.Count < 4 Then Return

        TargetText4Label.Text = a(3)
        If a(3)(0) = "~" Then
            TargetText4Label.Font = New Font(TargetText4Label.Font, FontStyle.Strikeout)
            TargetText4Label.Text = TargetText4Label.Text.Substring(1)
        End If
        If a.Count < 5 Then Return

        TargetText5Label.Text = a(4)
        If a(4)(0) = "~" Then
            TargetText5Label.Font = New Font(TargetText5Label.Font, FontStyle.Strikeout)
            TargetText5Label.Text = TargetText5Label.Text.Substring(1)
        End If
        If a.Count < 6 Then Return

        TargetText6Label.Text = a(5)
        If a(5)(0) = "~" Then
            TargetText6Label.Font = New Font(TargetText6Label.Font, FontStyle.Strikeout)
            TargetText6Label.Text = TargetText6Label.Text.Substring(1)
        End If
        If a.Count < 7 Then Return

        TargetText7Label.Text = a(6)
        If a(6)(0) = "~" Then
            TargetText7Label.Font = New Font(TargetText7Label.Font, FontStyle.Strikeout)
            TargetText7Label.Text = TargetText7Label.Text.Substring(1)
        End If
        If a.Count < 8 Then Return

        TargetText8Label.Text = a(7)
        If a(7)(0) = "~" Then
            TargetText8Label.Font = New Font(TargetText8Label.Font, FontStyle.Strikeout)
            TargetText8Label.Text = TargetText8Label.Text.Substring(1)
        End If
    End Sub

    Private Sub RefreshBlokSelanjutnya()
        PapanGame.SetBlokAktif(Nothing)

        Dim a = PapanGame.AmbilPointerData()

        If PapanGame.IsKosong(0, 2) Then
            Dim b = ObjekKalimat.AmbilHurufTerpilihRandom()

            If Not b.HasValue Then
                Exit Sub
            End If

            a(0)(2) = b
            PapanGame.SetBlokAktif((0, 2))
        End If
    End Sub

    Private Function DeteksiKataCocok() As Integer
        Dim a = PapanGame.AmbilPointerData()
        Dim TotalPoin = 0

        For baris = 0 To a.Count - 1
            For kolom = 0 To a(baris).Count - 1

                ' Cek apakah ada kalimat terbentuk ke kanan
                Dim KalimatTerbentuk_AxisX As String = String.Empty
                Dim KalimatDitemukan As Boolean = False

                If Not a(baris)(kolom).HasValue Then
                    Continue For
                End If

                KalimatTerbentuk_AxisX += a(baris)(kolom)

                If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, True) Then
                    For LookupKolom = (kolom + 1) To a(baris).Count - 1
                        Dim LookedKolom = a(baris)(LookupKolom)

                        If Not LookedKolom.HasValue Then
                            Exit For
                        End If

                        KalimatTerbentuk_AxisX += a(baris)(LookupKolom)
                        If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, False) Then
                            ' Ditemukan kalimat yang sangat cocok
                            KalimatDitemukan = True
                            ObjekKalimat.DisableKalimat(KalimatTerbentuk_AxisX)
                            ObjekKalimat.RefreshHurufTerpilih()
                            Exit For
                        ElseIf ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, True) Then
                            ' Ditemukan kalimat yang diawali dengan huruf yang sama
                        Else
                            ' Tidak ditemukan
                            Exit For
                        End If
                    Next
                End If

                If KalimatDitemukan Then
                    ' Hapus semua kolom pada baris kalimat ini
                    For i = 0 To a(baris).Count - 1
                        If a(baris)(i).HasValue Then
                            TotalPoin += 1
                            a(baris)(i) = Nothing
                        End If
                    Next

                    Exit For
                End If

                ' Cek apakah ada kalimat terbentuk ke atas
                Dim KalimatTerbentuk_AxisY As String = String.Empty

                If Not a(baris)(kolom).HasValue Then
                    Continue For
                End If

                KalimatTerbentuk_AxisY += a(baris)(kolom)

                If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, True) Then
                    For LookupBaris = (baris + 1) To a.Count - 1
                        Dim LookedKolom = a(LookupBaris)(kolom)

                        If Not LookedKolom.HasValue Then
                            Exit For
                        End If

                        KalimatTerbentuk_AxisY += a(LookupBaris)(kolom)
                        If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, False) Then
                            ' Ditemukan kalimat yang sangat cocok
                            KalimatDitemukan = True
                            ObjekKalimat.DisableKalimat(KalimatTerbentuk_AxisY)
                            ObjekKalimat.RefreshHurufTerpilih()
                            Exit For
                        ElseIf ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, True) Then
                            ' Ditemukan kalimat yang diawali dengan huruf yang sama
                        Else
                            ' Tidak ditemukan
                            Exit For
                        End If
                    Next
                End If

                If KalimatDitemukan Then
                    TotalPoin += 1
                    ' Hapus semua kolom selanjutnya pada baris yang ditemukan kalimat ini
                    For i = baris To (baris + KalimatTerbentuk_AxisY.Length - 1)
                        For i2 = kolom To a(i).Count - 1

                            If a(i)(i2).HasValue Then
                                TotalPoin += 1
                                a(i)(i2) = Nothing
                            End If
                        Next
                    Next
                    Exit For
                End If
            Next
        Next

        Return TotalPoin
    End Function

    Private Sub MusicLoop()
        Console.WriteLine("MusicLoop()")
        Me.MusicAudioPlayer.controls.play()
    End Sub

    Private Sub GameArea_Paint(sender As Object, e As PaintEventArgs) Handles GameArea.Paint
        Dim MarginLeft As Integer = 5
        Dim BlockBorderSize As Integer = 4

        Using blackPen As New Pen(Color.Black)
            e.Graphics.DrawLine(blackPen, MarginLeft, 0, MarginLeft, GameArea.Size.Height)

            For i = (MarginLeft + 44) To GameArea.Size.Width Step 44
                e.Graphics.DrawLine(blackPen, i, 0, i, GameArea.Size.Height)
            Next
        End Using

        For baris = 1 To PapanGame.TotalBaris
            For kolom = 1 To PapanGame.TotalKolom
                If Not PapanGame.IsKosong(baris - 1, kolom - 1) Then
                    Dim StartX = (kolom * 44) - 44 + MarginLeft
                    Dim StartY = (baris * 45) - 45

                    ' Buat border boxnya
                    Dim BorderRects(4) As Rectangle
                    BorderRects(0) = New Rectangle(StartX, StartY, 44, BlockBorderSize) ' Top
                    BorderRects(1) = New Rectangle(StartX, StartY, BlockBorderSize, 45) ' Left
                    BorderRects(2) = New Rectangle(StartX + 44 - BlockBorderSize + 1, StartY, BlockBorderSize, 45) ' Right
                    BorderRects(3) = New Rectangle(StartX, StartY + 45 - BlockBorderSize, 44, BlockBorderSize) ' Bottom
                    e.Graphics.FillRectangles(New SolidBrush(Color.Black), BorderRects)

                    Dim text = PapanGame.AmbilData(baris - 1, kolom - 1)
                    e.Graphics.DrawString(text, GameArea.Font, New SolidBrush(Color.Black), StartX + 7, StartY + 5)
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

    Private Sub Tetris_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.D Then
            Me.TickGame.Enabled = True
            Me.PapanGame.GeserX_BlokAktif(1)
            Me.GameArea.Refresh()
            Return
        End If

        If e.KeyCode = Keys.A Then
            Me.TickGame.Enabled = True
            Me.PapanGame.GeserX_BlokAktif(-1)
            Me.GameArea.Refresh()
            Return
        End If

        If e.KeyCode = Keys.S Then
            Me.TickGame.Enabled = False
            Me.OnGameTick()
            Me.GameArea.Refresh()
        End If
    End Sub

    Private Sub Tetris_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        Me.TickGame.Enabled = True
    End Sub
End Class