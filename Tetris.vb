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
	Dim Level As Integer = 1
	Dim IsPaused As Boolean = False
	Dim GameTickSebelumPause As UInteger = 0

	' NOTE: Semua game tick dihitung dalam milisecond (ms)
	ReadOnly GameTickAwal = 1100
	ReadOnly ScalingGameTickPerLevel = -60 ' Rumus perubahan tick setiap level: GameTick + (ScalingGameTickPerLevel * Level)

	Private Sub Tetris_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		' Paksa font area permainan agar sama lebarnya (Monospace)
		GameArea.Font = New Font(FontFamily.GenericMonospace, GameArea.Font.Size, GameArea.Font.Style)

		' Ubah warna elemen menggunakan nilai RGB secara langsung
		GameArea.BackColor = Color.FromArgb(56, 0, 102)
		Panel1.BackColor = Color.FromArgb(56, 0, 102)
		Panel3.BackColor = Color.FromArgb(56, 0, 102)

		' Generate kalimat awal game
		ObjekKalimat.GenerateKalimatTerpilih(1, 0)

		ObjekKalimat.RefreshHurufTerpilih()
		RefreshUIListKalimat()

		' Siapkan music player
		Me.MusicAudioPlayer = New WindowsMediaPlayer
		Me.MusicAudioPlayer.URL = Path.Combine(Application.StartupPath, "PublicResources/Tom and Jerry at MGM performed by the John Wilson Orchestra 2013.mp3")
		Me.MusicAudioPlayer.controls.play()
		AddHandler Me.MusicAudioPlayer.PlayStateChange, AddressOf Me.MusicLoop

		' Buat timer untuk refresh/update ui permainan
		Me.TickGame = New Timer With {
			.Interval = GameTickAwal
		}
		AddHandler Me.TickGame.Tick, AddressOf OnGameTick
		Me.TickGame.Enabled = True


		' Nyalakan cache form ini (seharusnya tidak perlu, tapi takut sewaktu waktu terpengaruh oleh GameArea...)
		Me.DoubleBuffered = True

		' Paksa cache GameArea agar tidak berkedip saat digambar manual
		' Secara default, properti cache panel tidak boleh diubah manual, tapi bisa dipaksa 
		' (source: https://stackoverflow.com/questions/4777135/how-can-i-draw-on-panel-so-it-does-not-blink)
		Dim flags As BindingFlags = BindingFlags.Instance Or BindingFlags.NonPublic
		Dim propertyInfo As PropertyInfo = Me.GameArea.GetType().GetProperty("DoubleBuffered", flags)
		propertyInfo.SetValue(Me.GameArea, True, Nothing)
	End Sub

	''' <summary>
	''' Function yang akan dipanggil setiap refresh game (seperti Tick pada NodeJS, Update pada Unity)
	''' </summary>
	Private Sub OnGameTick()
		If IsPaused Then
			PausePanel.Visible = Not PausePanel.Visible
			Return
		End If

		Dim BlokAktifSaatIni = PapanGame.GetBlokAktif()

		' Jika ada blok akhir
		If BlokAktifSaatIni.HasValue Then
			' Apakah blok saat ini masih bisa diturunkan
			If PapanGame.BlokBisaDiturunkan(BlokAktifSaatIni.Value.Item1, BlokAktifSaatIni.Value.Item2) Then
				' Turunkan blok aktif kebawah
				PapanGame.GeserY_BlokAktif(1)
			Else
				ObjekKalimat.BersihkanMasukan_PemilihanPrediktif()

				' Cek apakah ada kalimat yang terbentuk
				Dim Nilai = DeteksiKataCocok()

				' Jika ada kalimat yang terbentuk oleh sebuah blok
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
					' Ganti ke blok selanjutnya
					RefreshBlokSelanjutnya()
				End If
			End If
		Else
			' Ganti ke blok selanjutnya
			RefreshBlokSelanjutnya()
		End If

		' Refresh area permainan
		Me.GameArea.Refresh()

		' Cek lagi status blok aktif saat ini
		BlokAktifSaatIni = PapanGame.GetBlokAktif()

		' Jika tidak ada blok aktif
		If Not BlokAktifSaatIni.HasValue Then
			' Cek apakah masih ada kalimat target yang tersisa
			If ObjekKalimat.GetKalimatTerpilih(True).Count > 0 Then
				' Masih ada kalimat tapi tidak bisa menaruh blok, anggap papan permainan penuh dan Game Over

				If Not IsPaused Then
					PausePanel_MouseClick(Nothing, Nothing)
				End If

				Me.MusicAudioPlayer.close()

				' Matikan music dan keluar
				MessageBox.Show("Game Over", "Team 4 - Tetris")
				Return
			Else
				' Seluruh kalimat target pada level ini habis, generate baru lagi

				' Pastikan tidak melebihi 8 kalimat pada satu ronde
				Level += 1
				Dim JumlahKalimatTarget = (Level / 4)

				ObjekKalimat.GenerateKalimatTerpilih(Math.Clamp(JumlahKalimatTarget, 1, 8), TotalScore / 40)
				ObjekKalimat.RefreshHurufTerpilih()
				RefreshUIListKalimat()

				' Percepat tick/refresh game untuk level yang lebih tinggi
				Me.TickGame.Interval = Math.Clamp(GameTickAwal + (ScalingGameTickPerLevel * Level), 300, GameTickAwal)
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
		Else
			TargetText1Label.Font = New Font(TargetText1Label.Font, FontStyle.Bold)
		End If
		If a.Count < 2 Then Return

		TargetText2Label.Text = a(1)
		If a(1)(0) = "~" Then
			TargetText2Label.Font = New Font(TargetText2Label.Font, FontStyle.Strikeout)
			TargetText2Label.Text = TargetText2Label.Text.Substring(1)
		Else
			TargetText2Label.Font = New Font(TargetText2Label.Font, FontStyle.Bold)
		End If
		If a.Count < 3 Then Return

		TargetText3Label.Text = a(2)
		If a(2)(0) = "~" Then
			TargetText3Label.Font = New Font(TargetText3Label.Font, FontStyle.Strikeout)
			TargetText3Label.Text = TargetText3Label.Text.Substring(1)
		Else
			TargetText3Label.Font = New Font(TargetText3Label.Font, FontStyle.Bold)
		End If
		If a.Count < 4 Then Return

		TargetText4Label.Text = a(3)
		If a(3)(0) = "~" Then
			TargetText4Label.Font = New Font(TargetText4Label.Font, FontStyle.Strikeout)
			TargetText4Label.Text = TargetText4Label.Text.Substring(1)
		Else
			TargetText4Label.Font = New Font(TargetText4Label.Font, FontStyle.Bold)
		End If
		If a.Count < 5 Then Return

		TargetText5Label.Text = a(4)
		If a(4)(0) = "~" Then
			TargetText5Label.Font = New Font(TargetText5Label.Font, FontStyle.Strikeout)
			TargetText5Label.Text = TargetText5Label.Text.Substring(1)
		Else
			TargetText5Label.Font = New Font(TargetText5Label.Font, FontStyle.Bold)
		End If
		If a.Count < 6 Then Return

		TargetText6Label.Text = a(5)
		If a(5)(0) = "~" Then
			TargetText6Label.Font = New Font(TargetText6Label.Font, FontStyle.Strikeout)
			TargetText6Label.Text = TargetText6Label.Text.Substring(1)
		Else
			TargetText6Label.Font = New Font(TargetText6Label.Font, FontStyle.Bold)
		End If
		If a.Count < 7 Then Return

		TargetText7Label.Text = a(6)
		If a(6)(0) = "~" Then
			TargetText7Label.Font = New Font(TargetText7Label.Font, FontStyle.Strikeout)
			TargetText7Label.Text = TargetText7Label.Text.Substring(1)
		Else
			TargetText7Label.Font = New Font(TargetText7Label.Font, FontStyle.Bold)
		End If
		If a.Count < 8 Then Return

		TargetText8Label.Text = a(7)
		If a(7)(0) = "~" Then
			TargetText8Label.Font = New Font(TargetText8Label.Font, FontStyle.Strikeout)
			TargetText8Label.Text = TargetText8Label.Text.Substring(1)
		Else
			TargetText8Label.Font = New Font(TargetText8Label.Font, FontStyle.Bold)
		End If
	End Sub

	Private Sub RefreshBlokSelanjutnya()
		PapanGame.SetBlokAktif(Nothing)

		Dim a = PapanGame.AmbilPointerData()

		If PapanGame.IsKosong(0, 2) Then
			Dim b As Char? = ObjekKalimat.AmbilHurufTerpilih()

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

#Region "Cek apakah ada kalimat yang terbentuk dari kiri ke kanan"
				Dim KalimatTerbentuk_AxisX As String = String.Empty
				Dim KalimatDitemukan As Boolean = False

				If Not a(baris)(kolom).HasValue Then
					Continue For
				End If

				KalimatTerbentuk_AxisX += a(baris)(kolom)

				If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, True) Then
					Dim SusunanDiawali = False

					For LookupKolom = (kolom + 1) To a(baris).Count - 1
						Dim LookedKolom = a(baris)(LookupKolom)

						If Not LookedKolom.HasValue Then
							Exit For
						End If

						KalimatTerbentuk_AxisX += a(baris)(LookupKolom)
						If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, False) Then
							' Ditemukan kalimat yang sangat cocok
							KalimatDitemukan = True
							SusunanDiawali = False
							ObjekKalimat.DisableKalimat(KalimatTerbentuk_AxisX)
							ObjekKalimat.RefreshHurufTerpilih()
							Exit For
						ElseIf ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisX, True) Then
							' Ditemukan kalimat yang diawali dengan huruf yang sama
							SusunanDiawali = True
						Else
							' Tidak ditemukan
							Exit For
						End If
					Next

					If SusunanDiawali Then
						Dim KalimatTarget = ObjekKalimat.GetPrediksiKalimat(KalimatTerbentuk_AxisX)

						If PrediksiCekApakahMuat(kolom, baris, KalimatTarget, KalimatTerbentuk_AxisX, "Kanan") Then
							ObjekKalimat.TambahMasukan_PemilihanPrediktif(KalimatTarget, KalimatTerbentuk_AxisX.Length)
						End If
					End If
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

#End Region

#Region "Cek apakah ada kalimat yang terbentuk dari atas ke bawah"
				Dim KalimatTerbentuk_AxisY As String = String.Empty

				KalimatTerbentuk_AxisY += a(baris)(kolom)

				If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, True) Then
					Dim SusunanDiawali = False

					For LookupBaris = (baris + 1) To a.Count - 1
						Dim LookedKolom = a(LookupBaris)(kolom)

						If Not LookedKolom.HasValue Then
							Exit For
						End If

						KalimatTerbentuk_AxisY += a(LookupBaris)(kolom)
						If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, False) Then
							' Ditemukan kalimat yang sangat cocok
							KalimatDitemukan = True
							SusunanDiawali = False
							ObjekKalimat.DisableKalimat(KalimatTerbentuk_AxisY)
							ObjekKalimat.RefreshHurufTerpilih()
							Exit For
						ElseIf ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisY, True) Then
							' Ditemukan kalimat yang diawali dengan huruf yang sama
							SusunanDiawali = True
						Else
							' Tidak ditemukan
							Exit For
						End If
					Next

					If SusunanDiawali Then
						Dim KalimatTarget = ObjekKalimat.GetPrediksiKalimat(KalimatTerbentuk_AxisY)

						If PrediksiCekApakahMuat(kolom, baris, KalimatTarget, KalimatTerbentuk_AxisY, "Bawah") Then
							ObjekKalimat.TambahMasukan_PemilihanPrediktif(KalimatTarget, KalimatTerbentuk_AxisY.Length)
						End If
					End If
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
#End Region

#Region "Cek apakah ada kalimat yang terbentuk dari bawah ke atas"
				Dim KalimatTerbentuk_AxisYReverse As String = String.Empty

				KalimatTerbentuk_AxisYReverse += a(baris)(kolom)

				If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisYReverse, True) Then
					For LookupBaris = (baris - 1) To 0 Step -1
						Dim LookedKolom = a(LookupBaris)(kolom)

						If Not LookedKolom.HasValue Then
							Exit For
						End If

						KalimatTerbentuk_AxisYReverse += a(LookupBaris)(kolom)
						If ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisYReverse, False) Then
							' Ditemukan kalimat yang sangat cocok
							KalimatDitemukan = True
							ObjekKalimat.DisableKalimat(KalimatTerbentuk_AxisYReverse)
							ObjekKalimat.RefreshHurufTerpilih()
							Exit For
						ElseIf ObjekKalimat.SearchKalimat(KalimatTerbentuk_AxisYReverse, True) Then
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
					For i = baris To (baris - KalimatTerbentuk_AxisYReverse.Length) Step -1
						For i2 = kolom To a(i).Count - 1

							If a(i)(i2).HasValue Then
								TotalPoin += 1
								a(i)(i2) = Nothing
							End If
						Next
					Next
					Exit For
				End If
#End Region
			Next
		Next

		Return TotalPoin
	End Function

	Private Sub MusicLoop(e As Integer)
		' Jika musik sudah berhenti, mulai dari awal lagi
		If e = WMPPlayState.wmppsStopped Then
			Me.MusicAudioPlayer.controls.play()
		End If
	End Sub

	Private Sub GameArea_Paint(sender As Object, e As PaintEventArgs) Handles GameArea.Paint
		Dim MarginLeft As Integer = 5
		Dim BlockBorderSize As Integer = 4

		Using GuideLinePen As New Pen(Color.LightGray)
			e.Graphics.DrawLine(GuideLinePen, MarginLeft, 0, MarginLeft, GameArea.Size.Height)

			For i = (MarginLeft + 44) To GameArea.Size.Width Step 44
				e.Graphics.DrawLine(GuideLinePen, i, 0, i, GameArea.Size.Height)
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
					e.Graphics.FillRectangles(New SolidBrush(Color.LightGray), BorderRects)

					Dim text = PapanGame.AmbilData(baris - 1, kolom - 1)
					e.Graphics.DrawString(text, GameArea.Font, New SolidBrush(Color.LightGray), StartX + 7, StartY + 5)
				End If
			Next
		Next
	End Sub

	Private Sub Tetris_FormClosing()
		' Jika objek musik masih ada, hentikan dan hapus dari memori.
		If Me.MusicAudioPlayer IsNot Nothing Then
			Me.MusicAudioPlayer.controls.stop()
			Me.MusicAudioPlayer.close()
			Me.MusicAudioPlayer = Nothing
		End If
	End Sub

	Private Sub Tetris_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
		' Jika D/Right ditekan, geser blok aktif ke kanan
		If e.KeyCode = Keys.D OrElse e.KeyCode = Keys.Right Then
			Me.PapanGame.GeserX_BlokAktif(1)
			Me.GameArea.Refresh()
			Return
		End If

		' Jika A/Left ditekan, geser blok aktif ke kiri
		If e.KeyCode = Keys.A OrElse e.KeyCode = Keys.Left Then
			Me.PapanGame.GeserX_BlokAktif(-1)
			Me.GameArea.Refresh()
			Return
		End If

		' Jika S ditekan, geser blok aktif ke bawah
		If e.KeyCode = Keys.S OrElse e.KeyCode = Keys.Down Then
			Me.TickGame.Enabled = False ' Matikan tick/refresh otomatis
			Me.OnGameTick() ' Jalankan tick/refresh seperti sekali agak blok turun
		End If

		' Jika spasi ditekan, turunkan blok secara langsung
		If e.KeyCode = Keys.Space Then
			Dim a = PapanGame.Turunkan()

			' Selama masih bisa diturunkan, turunkan lagi
			While a
				a = PapanGame.Turunkan()
			End While

			Me.OnGameTick()
		End If
	End Sub

	Private Sub Tetris_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
		' Jika tombol yang ditekan sudah diangkat, nyalakan lagi tick/refresh otomatis
		Me.TickGame.Enabled = True
	End Sub

	Private Function PrediksiCekApakahMuat(kolom As Integer, baris As Integer, TargetKalimat As String, RangkaianKalimat As String, Arah As String) As Boolean
		Dim JumlahKurangKarakter = TargetKalimat.Length - RangkaianKalimat.Length
		Dim c = RangkaianKalimat.Length
		Dim d = TargetKalimat.Length

		If (c = 0 OrElse d = 0) Then
			Return False
		End If

		' Cek apakah ada ruang untuk penyusunan di kanan (dari kiri)
		If Arah = "Kanan" Then
			Dim temp = RangkaianKalimat
			Dim TempIndex = RangkaianKalimat.Length

			For i = (kolom + c) To (kolom + d - 1)
				If baris >= PapanGame.TotalBaris OrElse i >= PapanGame.TotalKolom Then
					Return False
				End If

				Dim b = PapanGame.AmbilPointerData()(baris)(i)

				If b IsNot Nothing Then
					If Not TargetKalimat.StartsWith(temp + b) Then
						Return False
					End If

					temp += b
					TempIndex += 1
					Continue For
				End If

				temp += TargetKalimat(TempIndex)
				TempIndex += 1
			Next

			Return True
		End If

		If Arah = "Atas" Then
			' Cek apakah ada ruang untuk penyusunan di atas (dari bawah)
			For i = (baris - c) To (baris - d + 1) Step -1
				Dim b = PapanGame.IsKosong(i, kolom)

				If Not b Then
					Return False
				End If
			Next

			Return True
		End If

		Return False
	End Function

	Private Sub PausePanel_MouseClick(sender As Object, e As MouseEventArgs) Handles PausePanel.MouseClick
		If Not IsPaused Then
			TickGame.Enabled = False
			GameTickSebelumPause = TickGame.Interval
			IsPaused = True
			TickGame.Interval = 800
			TickGame.Enabled = True
			OnGameTick()
			Return
		End If

		TickGame.Enabled = False
		TickGame.Interval = GameTickSebelumPause
		IsPaused = False
		TickGame.Enabled = True
	End Sub

	Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
		Tetris_FormClosing()
		Me.Close()
		Me.Dispose()
	End Sub
End Class