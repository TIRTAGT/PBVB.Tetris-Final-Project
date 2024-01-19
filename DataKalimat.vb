Public Class DataKalimat
	Private ReadOnly RNGenerator As New System.Random()

	Private ReadOnly KalimatMudah As String() = New String() {
		"If",
		"Hp",
		"Ada",
		"Apa",
		"iTi",
		"Asap",
		"Else",
		"Buku",
		"Kuku",
		"True",
		"Mata",
		"Sari",
		"Roti"
	}

	Private ReadOnly KalimatSulit As String() = New String() {
		"While",
		"Titan",
		"Katon",
		"False",
		"Dioba",
		"Akmal",
		"Makan",
		"Tetris",
		"Matthew",
		"Bracken",
		"Telepon"
	}

	Private KumpulanKalimatTerpilih As New List(Of String)

	Private KumpulanHurufTerpilih As New List(Of Char)
	Private HurufRandomSebelumnya As Char = ""
	Private JumlahHurufRandomSama As Integer = 0
	Private ReadOnly BatasJumlahHurufRandomSama As Integer = 2
	''' <summary>
	''' Jika True, pemilihan huruf dilakukan secara prediktif
	'''         Game akan lebih sering memilih huruf yang akan membantu pemain pada saat itu
	'''         
	''' Jika false, huruf yang dipilih akan selalau random
	'''     Dikarena sangat random, ada kemungkinan pemain kalah karena tidak mendapat huruf yang dibutuhkan
	''' </summary>
	Public ReadOnly GunakanPemilihanHurufPrediktif As Boolean = True
	Private ArrayPemilihanPrediktif As (String, Short)()


	Public Sub New()
		' Pastikan semua pilihan kalimat adalah huruf besar
		For i = 0 To (KalimatMudah.Length - 1)
			KalimatMudah(i) = KalimatMudah(i).ToUpper()
		Next

		For i = 0 To (KalimatSulit.Length - 1)
			KalimatSulit(i) = KalimatSulit(i).ToUpper()
		Next
	End Sub

	''' <summary>
	''' Ambil kalimat yang terpilih oleh random generator
	''' </summary>
	''' <param name="HanyaAktif">True jika ingin mengambil kalimat terpilih yang aktif (belum diselesaikan)</param>
	''' <returns></returns>
	Public Function GetKalimatTerpilih(Optional HanyaAktif As Boolean = False) As List(Of String)
		If Not HanyaAktif Then
			Return KumpulanKalimatTerpilih
		End If

		Dim a As New List(Of String)

		For i = 0 To KumpulanKalimatTerpilih.Count - 1
			If KumpulanKalimatTerpilih(i)(0) <> "~" Then
				a.Add(KumpulanKalimatTerpilih(i))
			End If
		Next

		Return a
	End Function

	''' <summary>
	''' Pilih/Generate kalimat yang akan dipilih pada permainan
	''' </summary>
	''' <param name="JumlahKalimat">Jumlah kalimat yang akan dipilih/generate</param>
	''' <param name="Rasio">Probabilitas mendapatkan kalimat mudah (Rumus: 1/1+Rasio) </param>
	Public Sub GenerateKalimatTerpilih(JumlahKalimat As UInteger, Rasio As Single)
		Dim i As UInteger = 0
		KumpulanKalimatTerpilih.Clear()
		ArrayPemilihanPrediktif = New(String, Short)(JumlahKalimat - 1) {}

		While True
			If i >= JumlahKalimat Then
				Exit While
			End If

			' Tentukan kesulitan kalimat saat ini secara random berdasarkan rasio
			Dim a = RNGenerator.Next(0, Rasio)
			Dim KalimatTerpilih As String

			If (a = 0) Then
				KalimatTerpilih = KalimatMudah(RNGenerator.Next(0, KalimatMudah.Length))
			Else
				KalimatTerpilih = KalimatSulit(RNGenerator.Next(0, KalimatSulit.Length))
			End If

			Dim KalimatDuplikat As Boolean = False

			' Cek apakah kita sudah pakai kalimat ini
			For ii = 0 To KumpulanKalimatTerpilih.Count - 1
				If KalimatTerpilih = KumpulanKalimatTerpilih(ii) Then
					KalimatDuplikat = True
					Exit For
				End If
			Next

			If KalimatDuplikat Then
				Continue While
			End If

			Me.KumpulanKalimatTerpilih.Add(KalimatTerpilih)
			ArrayPemilihanPrediktif(i) = (KalimatTerpilih, 0)
			i += 1
		End While
	End Sub

	''' <summary>
	''' Siapkan/Generate huruf yang akan digunakan untuk menyusun kalimat
	''' </summary>
	''' <param name="RefreshSemua">Jika true seluruh kalimat akan disiapkan, Jika false hanya kalimat aktif yang akan disiapkan</param>
	Public Sub RefreshHurufTerpilih(Optional RefreshSemua As Boolean = False)
		If GunakanPemilihanHurufPrediktif Then
			For i = 0 To KumpulanKalimatTerpilih.Count - 1
				Dim KalimatTerpilih = KumpulanKalimatTerpilih(i)

				' Skip kata yang di disable jika tidak ada perintah refresh semua
				If Not RefreshSemua AndAlso KalimatTerpilih(0) = "~" Then
					ArrayPemilihanPrediktif(i) = (ArrayPemilihanPrediktif(i).Item1, -1)
					Continue For
				End If
			Next
		End If

		KumpulanHurufTerpilih.Clear()

		For i = 0 To KumpulanKalimatTerpilih.Count - 1
			Dim KalimatTerpilih = KumpulanKalimatTerpilih(i)

			' Skip kata yang di disable jika tidak ada perintah refresh semua
			If Not RefreshSemua AndAlso KalimatTerpilih(0) = "~" Then
				Continue For
			End If

			' Ekstrak semua huruf pada kalimat  
			For ii = 0 To KalimatTerpilih.Length - 1
				Dim HurufDuplikat As Boolean = False

				' Jangan izinkan ada duplikat
				For iii = 0 To KumpulanHurufTerpilih.Count - 1
					If KalimatTerpilih(ii) = KumpulanHurufTerpilih(iii) Then
						HurufDuplikat = True
						Exit For
					End If
				Next

				If Not HurufDuplikat Then
					KumpulanHurufTerpilih.Add(KalimatTerpilih(ii))
				End If
			Next
		Next
	End Sub

	''' <summary>
	''' Mengambil huruf terpilih yang statusnya tidak disable
	''' </summary>
	''' <returns>Huruf</returns>
	Public Function AmbilHurufTerpilih(Optional StackLimit As Integer = 0) As Char?
		If KumpulanHurufTerpilih.Count = 0 Then
			Return Nothing
		End If

		Dim a As Char? = Nothing

		Dim PrediktifTertinggi = 1
		For i = 0 To ArrayPemilihanPrediktif.Length - 1
			Dim b = ArrayPemilihanPrediktif(i)

			If (b.Item2 > PrediktifTertinggi) Then
				PrediktifTertinggi = b.Item2

				a = b.Item1(b.Item2)
			End If
		Next

		' Pilih random hanya jika prediktif tidak bisa dilakukan
		If (a Is Nothing) Then
			Dim index = RNGenerator.Next(-JumlahHurufRandomSama, KumpulanHurufTerpilih.Count - 1 + JumlahHurufRandomSama)

			' Pastikan index random tidak keluar dari array
			index = Math.Clamp(index, 0, KumpulanHurufTerpilih.Count - 1)

			a = KumpulanHurufTerpilih(index)
		End If

		If (a = HurufRandomSebelumnya) Then
			If (JumlahHurufRandomSama > BatasJumlahHurufRandomSama) AndAlso (StackLimit < 100) Then
				Return AmbilHurufTerpilih()
			End If

			JumlahHurufRandomSama += 1
		Else
			HurufRandomSebelumnya = a
			JumlahHurufRandomSama = 0
		End If

		Return a
	End Function

	''' <summary>
	''' Mencari apakah kalimat yang diberikan ada pada KumpulanKalimatTerpilih
	''' </summary>
	''' <param name="Kalimat">Kalimat yang ingin dicaari</param>
	''' <param name="CobaPrediksi">Jika false, Kalimat akan dicari secara pasti (exact).<br></br>Jika true, Kalimat akan dicari secara mulai dari (starts with).</param>
	''' <returns>True jika ditemukan, False jika tidak ditemukan</returns>
	Public Function SearchKalimat(Kalimat As String, CobaPrediksi As Boolean) As Boolean
		If CobaPrediksi Then
			For i = 0 To KumpulanKalimatTerpilih.Count - 1
				If KumpulanKalimatTerpilih(i).StartsWith(Kalimat) Then
					Return True
				End If
			Next

			Return False
		End If

		For i = 0 To KumpulanKalimatTerpilih.Count - 1
			If Kalimat = KumpulanKalimatTerpilih(i) Then
				Return True
			End If
		Next

		Return False
	End Function

	''' <summary>
	''' Mencari kalimat yang mirip KumpulanKalimatTerpilih
	''' </summary>
	''' <param name="Kalimat">Kalimat yang ingin dicaari</param>
	''' <returns>Kalimatnya jika ditemukan, Kosong jika tidak ditemukan</returns>
	Public Function GetPrediksiKalimat(Kalimat As String) As String
		For i = 0 To KumpulanKalimatTerpilih.Count - 1
			If KumpulanKalimatTerpilih(i).StartsWith(Kalimat) Then
				Return KumpulanKalimatTerpilih(i)
			End If
		Next

		Return String.Empty
	End Function

	Public Function DisableKalimat(Kalimat As String)
		For i = 0 To KumpulanKalimatTerpilih.Count - 1
			If Kalimat = KumpulanKalimatTerpilih(i) Then
				KumpulanKalimatTerpilih(i) = "~" + KumpulanKalimatTerpilih(i)
				Return True
			End If
		Next

		Return False
	End Function

	Public Sub TambahMasukan_PemilihanPrediktif(Kalimat As String, JumlahHurufTerangkai As Short)
		For i = 0 To ArrayPemilihanPrediktif.Length - 1
			Dim a = ArrayPemilihanPrediktif(i)

			If (a.Item1 = Kalimat) Then
				Dim b = ArrayPemilihanPrediktif(i).Item2

				If (JumlahHurufTerangkai > b) Then
					ArrayPemilihanPrediktif(i) = (Kalimat, JumlahHurufTerangkai)
				End If
			End If
		Next
	End Sub

	Public Sub BersihkanMasukan_PemilihanPrediktif()
		For i = 0 To ArrayPemilihanPrediktif.Length - 1
			ArrayPemilihanPrediktif(i) = (ArrayPemilihanPrediktif(i).Item1, 0)
		Next
	End Sub
End Class
