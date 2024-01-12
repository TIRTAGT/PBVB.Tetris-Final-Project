Public Class DataKalimat
    Private ReadOnly RNGenerator As New System.Random()

    Private ReadOnly KalimatMudah As String() = New String() {
        "If",
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
        "Roti",
        "While",
        "Titan",
        "Katon",
        "Dioba",
        "Akmal"
    }

    Private ReadOnly KalimatSulit As String() = New String() {
        "Tetris",
        "Matthew",
        "Bracken",
        "Makanan",
        "Telepon",
        "Institut",
        "Teknologi",
        "Indonesia"
    }

    Private KumpulanKalimatTerpilih As New List(Of String)

    Private KumpulanHurufTerpilih As New List(Of Char)
    Private HurufRandomSebelumnya As Char = ""
    Private JumlahHurufRandomSama As Integer = 0
    Private ReadOnly BatasJumlahHurufRandomSama As Integer = 2


    Public Sub New()
        ' Pastikan semua pilihan kalimat adalah huruf besar
        For i = 0 To (KalimatMudah.Length - 1)
            KalimatMudah(i) = KalimatMudah(i).ToUpper()
        Next

        For i = 0 To (KalimatSulit.Length - 1)
            KalimatSulit(i) = KalimatSulit(i).ToUpper()
        Next
    End Sub

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
            i += 1
        End While
    End Sub

    Public Sub RefreshHurufTerpilih(Optional RefreshSemua As Boolean = False)
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
    ''' Mengambil huruf terpilih yang statusnya tidak disable secara random
    ''' </summary>
    ''' <returns>Huruf</returns>
    Public Function AmbilHurufTerpilihRandom() As Char?
        If KumpulanHurufTerpilih.Count = 0 Then
            Return Nothing
        End If

        Dim index = RNGenerator.Next(-JumlahHurufRandomSama, KumpulanHurufTerpilih.Count - 1 + JumlahHurufRandomSama)

        ' Pastikan index random tidak keluar dari array
        index = Math.Clamp(index, 0, KumpulanHurufTerpilih.Count - 1)

        Dim a = KumpulanHurufTerpilih(index)

        If (a = HurufRandomSebelumnya) Then
            If (JumlahHurufRandomSama > BatasJumlahHurufRandomSama) Then
                Return AmbilHurufTerpilihRandom()
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

    Public Function DisableKalimat(Kalimat As String)
        For i = 0 To KumpulanKalimatTerpilih.Count - 1
            If Kalimat = KumpulanKalimatTerpilih(i) Then
                KumpulanKalimatTerpilih(i) = "~" + KumpulanKalimatTerpilih(i)
                Return True
            End If
        Next

        Return False
    End Function

    Public Function EnableKalimat(Kalimat As String)
        For i = 0 To KumpulanKalimatTerpilih.Count - 1
            If ("~" + Kalimat) = KumpulanKalimatTerpilih(i) Then
                KumpulanKalimatTerpilih(i) = KumpulanKalimatTerpilih(i).Substring(1)
                Return True
            End If
        Next

        Return False
    End Function

    Public Function IsDisabled(Kalimat As String)
        For i = 0 To KumpulanKalimatTerpilih.Count - 1
            If ("~" + Kalimat) = KumpulanKalimatTerpilih(i) Then
                KumpulanKalimatTerpilih(i) = "~" + KumpulanKalimatTerpilih(i)
                Return True
            End If
        Next
    End Function
End Class
