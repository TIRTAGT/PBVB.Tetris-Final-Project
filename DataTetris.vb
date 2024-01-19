Public Class DataTetris
	Private PapanPermainan As List(Of List(Of Char?))
	Public BlokAktif As (Integer, Integer)?
	Public TotalKolom As Integer = 0
	Public TotalBaris As Integer = 0

	''' <summary>
	''' Inisialisasi Papan Game Tetris Baru
	''' </summary>
	Public Sub New()
		Me.PapanPermainan = New List(Of List(Of Char?))
		
		'Buat array/list papan permainan 6.31x10.97 (tapi dibulatkan)
		TotalKolom = 6
		TotalBaris = 11

		For i = 0 To TotalBaris - 1
			Dim Kolom As New List(Of Char?)

			For j = 0 To TotalKolom - 1
				Kolom.Add(Nothing)
			Next

			PapanPermainan.Add(Kolom)
		Next
	End Sub

	''' <summary>
	'''  Inisialisasi Papan Game Tetris berdasarkan pointer ke papan yang sudah ada
	''' </summary>
	''' <param name="Papan"></param>
	Public Sub New(ByRef Papan As List(Of List(Of Char?)))
		Me.PapanPermainan = Papan
	End Sub

	''' <summary>
	''' Duplikat isi papan permainan tanpa reference memori asli
	''' </summary>
	''' <param name="Papan">Papan yang ingin di duplikat</param>
	Private Function DuplikatPapan(ByRef Papan As List(Of List(Of Char?))) As List(Of List(Of Char?))
		Dim temp As New List(Of List(Of Char?))

		For i = 0 To Papan.Count - 1
			Dim Kolom As New List(Of Char?)

			For j = 0 To Papan(i).Count - 1
				Kolom.Add(Papan(i)(j))
			Next

			temp.Add(Kolom)
		Next

		Return temp
	End Function

	''' <summary>
	''' Duplikat tuple yang merepresentasikan blok aktif
	''' </summary>
	''' <param name="BlokAktif">Tuple asli</param>
	''' <returns>Tuple baru dengan nilai yang sama</returns>
	Private Function DuplikatBlokAktif(ByRef BlokAktif As (Integer, Integer)?) As (Integer, Integer)?
		Dim temp As (Integer, Integer)?

		If BlokAktif.HasValue Then
			temp = (BlokAktif.Value.Item1, BlokAktif.Value.Item2)
		End If

		Return temp
	End Function

	''' <summary>
	''' Turunkan blok apapaun yang masih bisa diturunkan
	''' </summary>
	''' <returns>Jumlah blok yang berhasil diturunkan</returns>
	Public Function Turunkan() As Integer
		Dim JumlahBlokTurun = 0
		Dim tempPapan = Me.DuplikatPapan(Me.PapanPermainan)
		Dim tempBlokAktif = Me.DuplikatBlokAktif(Me.BlokAktif)

		' Turunkan dari baris yang hampir paling bawah (-1)
		For IndexBaris = (Me.TotalBaris - 2) To 0 Step -1
			For IndexKolom = 0 To (Me.TotalKolom - 1)
				Dim BentrokanHimpunanMatrix As Boolean = False

				' Jika blok matrix ini tidak kosong
				If Not IsKosong(tempPapan, IndexBaris, IndexKolom) Then

					' Jika blok matrix dibawahnya tidak kosong (sudah terisi)
					If Not IsKosong(tempPapan, IndexBaris + 1, IndexKolom) Then
						' Ada bentrokan dan gak bisa diturunkan
						BentrokanHimpunanMatrix = True
					End If
				Else
					' Jika kosong, tidak perlu diturunkan
					Continue For
				End If

				' Jika sudah tidak bisa digeser kebawah lagi, lanjut ke baris selanjutnya
				If BentrokanHimpunanMatrix Then
					Continue For
				End If

				JumlahBlokTurun += 1
				tempPapan(IndexBaris + 1)(IndexKolom) = tempPapan(IndexBaris)(IndexKolom)
				tempPapan(IndexBaris)(IndexKolom) = Nothing

				' Update juga lokasi blok aktif jika ada
				If tempBlokAktif.HasValue Then
					' Jika yang diturunkan bukan blok aktif, jangan update lokasi blok aktif
					If (Not tempBlokAktif.Value.Item1 = IndexBaris) OrElse (Not tempBlokAktif.Value.Item2 = IndexKolom) Then
						Continue For
					End If

					tempBlokAktif = (IndexBaris + 1, IndexKolom)
				End If
			Next

			Me.PapanPermainan = tempPapan
			Me.BlokAktif = tempBlokAktif
		Next

		Return JumlahBlokTurun
	End Function

	''' <summary>
	''' Geser blok yang sedang aktif ke Axis X (Samping)
	''' </summary>
	''' <param name="jumlah_gerak">Jumlah gerakan dalam satuan blok</param>
	''' <returns>True jika berhasil, False jika gagal</returns>
	Public Function GeserX_BlokAktif(jumlah_gerak As Short) As Boolean
		Dim tempPapan = Me.DuplikatPapan(Me.PapanPermainan)
		Dim tempBlokAktif = Me.DuplikatBlokAktif(Me.BlokAktif)

		Dim BentrokanHimpunanMatrix As Boolean = False
		Dim StepTraverse = 1 ' ke arah kanan

		' Jika ingin digeser ke kiri
		If jumlah_gerak < 0 Then
			StepTraverse = -1 ' ke arah kiri
		End If

		' Jika tidak ada blok aktif, ignore
		If Not Me.BlokAktif.HasValue Then
			Return False
		End If

		Dim BlokAktif = Me.BlokAktif

		' Pastikan tidak keluar dari area permainan
		If ((BlokAktif.Value.Item2 + jumlah_gerak) < 0) OrElse (BlokAktif.Value.Item2 + jumlah_gerak) >= Me.TotalKolom Then
			Return False
		End If

		' Jika blok matrix ini tidak kosong
		If Not IsKosong(tempPapan, BlokAktif.Value.Item1, BlokAktif.Value.Item2) Then
			Dim StartIndex = BlokAktif.Value.Item2 + StepTraverse
			Dim StopIndex = BlokAktif.Value.Item2 + jumlah_gerak

			' Cek apakah selama perjalanan ke tujuan jumlah gerak akan terjadi bentrokan
			For i2 = StartIndex To StopIndex Step StepTraverse
				If Not IsKosong(tempPapan, BlokAktif.Value.Item1, i2) Then
					' Ada bentrokan
					BentrokanHimpunanMatrix = True
					Exit For
				End If
			Next

			If BentrokanHimpunanMatrix Then
				Return False
			End If

			' Pindahkan bloknya
			tempPapan(BlokAktif.Value.Item1)(BlokAktif.Value.Item2 + jumlah_gerak) = tempPapan(BlokAktif.Value.Item1)(BlokAktif.Value.Item2)
			tempPapan(BlokAktif.Value.Item1)(BlokAktif.Value.Item2) = Nothing
			tempBlokAktif = (BlokAktif.Value.Item1, BlokAktif.Value.Item2 + jumlah_gerak)
		End If

		If BentrokanHimpunanMatrix Then
			tempPapan.Clear()
			Return False
		End If

		Me.PapanPermainan = tempPapan
		Me.BlokAktif = tempBlokAktif
		Return True
	End Function

	''' <summary>
	''' Geser blok yang sedang aktif ke Axis Y (Tinggi/Tegak Lurus)
	''' </summary>
	''' <param name="jumlah_gerak">Jumlah gerakan dalam satuan blok</param>
	''' <returns>True jika berhasil, False jika gagal</returns>
	Public Function GeserY_BlokAktif(jumlah_gerak As Short) As Boolean
		Dim tempPapan = Me.DuplikatPapan(Me.PapanPermainan)
		Dim tempBlokAktif = Me.DuplikatBlokAktif(Me.BlokAktif)

		Dim BentrokanHimpunanMatrix As Boolean = False
		Dim StepTraverse = 1 ' ke arah bawah

		' Jika ingin digeser ke kiri
		If jumlah_gerak < 0 Then
			StepTraverse = -1 ' ke arah atas
		End If

		' Jika tidak ada blok aktif, ignore
		If Not Me.BlokAktif.HasValue Then
			Return False
		End If

		Dim BlokAktif = Me.BlokAktif

		' Pastikan tidak keluar dari area permainan
		If ((BlokAktif.Value.Item1 + jumlah_gerak) < 0) OrElse (BlokAktif.Value.Item1 + jumlah_gerak) >= Me.TotalBaris Then
			Return False
		End If

		' Jika blok matrix ini tidak kosong
		If Not IsKosong(tempPapan, BlokAktif.Value.Item1, BlokAktif.Value.Item2) Then
			Dim StartIndex = BlokAktif.Value.Item1 + StepTraverse
			Dim StopIndex = BlokAktif.Value.Item1 + jumlah_gerak

			' Cek apakah selama perjalanan ke tujuan jumlah gerak akan terjadi bentrokan
			For i2 = StartIndex To StopIndex Step StepTraverse
				If Not IsKosong(tempPapan, i2, BlokAktif.Value.Item2) Then
					' Ada bentrokan
					BentrokanHimpunanMatrix = True
					Exit For
				End If
			Next

			If BentrokanHimpunanMatrix Then
				Return False
			End If

			' Pindahkan bloknya
			tempPapan(BlokAktif.Value.Item1 + jumlah_gerak)(BlokAktif.Value.Item2) = tempPapan(BlokAktif.Value.Item1)(BlokAktif.Value.Item2)
			tempPapan(BlokAktif.Value.Item1)(BlokAktif.Value.Item2) = Nothing
			tempBlokAktif = (BlokAktif.Value.Item1 + jumlah_gerak, BlokAktif.Value.Item2)
		End If

		If BentrokanHimpunanMatrix Then
			tempPapan.Clear()
			Return False
		End If

		Me.PapanPermainan = tempPapan
		Me.BlokAktif = tempBlokAktif
		Return True
	End Function

	''' <summary>
	'''  Memeriksa apakah blok pada baris dan kolom spesifik pada PapanTetris saat ini bisa diturunkan
	''' </summary>
	''' <param name="IndexBaris">Index baris blok</param>
	''' <param name="IndexKolom">Index kolom blok</param>
	''' <returns>True jika bisa diturunkan, False jika tidak</returns>
	Public Function BlokBisaDiturunkan(IndexBaris As Integer, IndexKolom As Integer) As Boolean
		Return Me.BlokBisaDiturunkan(Me.PapanPermainan, IndexBaris, IndexKolom)
	End Function

	''' <summary>
	'''  Memeriksa apakah blok pada baris dan kolom spesifik pada PapanTetris saat ini bisa diturunkan
	''' </summary>
	''' <param name="tempPapan">PapanTetris / Papan Permainan yang akan dicek</param>
	''' <param name="IndexBaris">Index baris blok</param>
	''' <param name="IndexKolom">Index kolom blok</param>
	''' <returns>True jika bisa diturunkan, False jika tidak</returns>
	Public Function BlokBisaDiturunkan(ByRef tempPapan As List(Of List(Of Char?)), IndexBaris As Integer, IndexKolom As Integer) As Boolean
		' Jika blok matrix ini tidak kosong
		If Not IsKosong(tempPapan, IndexBaris, IndexKolom) Then

			If IndexBaris >= (Me.TotalBaris - 1) Then
				' Tidak bisa diturunkan, sudah keluar area permainan
				Return False
			End If

			' Jika blok matrix dibawahnya tidak kosong (sudah terisi)
			If Not IsKosong(tempPapan, IndexBaris + 1, IndexKolom) Then
				' Ada bentrokan dan gak bisa diturunkan
				Return False
			End If

			Return True
		Else
			' Jika kosong, tidak perlu diturunkan
			Return False
		End If
	End Function

#Region "Function Pembantu"
	Public Function IsKosong(ByRef blok As Char?) As Boolean
		Return (blok Is Nothing)
	End Function

	Public Function IsKosong(IndexBaris As Integer, IndexKolom As Integer) As Boolean
		Return Me.IsKosong(Me.PapanPermainan, IndexBaris, IndexKolom)
	End Function

	Public Function IsKosong(Papan As List(Of List(Of Char?)), IndexBaris As Integer, IndexKolom As Integer) As Boolean
		Return Me.IsKosong(Papan(IndexBaris)(IndexKolom))
	End Function

	''' <summary>
	''' Ambil pointer ke variable papan permainan pada class DataTetris.<br></br>
	''' Perubahan akan langsung mempengaruhi variable internal class DataTetris
	''' </summary>
	''' <returns></returns>
	Public Function AmbilPointerData() As List(Of List(Of Char?))
		Return PapanPermainan
	End Function
#End Region

End Class
