Public Class DataTetris
    Private PapanPermainan As List(Of List(Of Color?))
    Private BlokAktif As New List(Of (Integer, Integer))
    Private TipeBlokAktif As String = String.Empty

    Public Sub New()
        Me.PapanPermainan = New List(Of List(Of Color?))
        Me.Inisialisasi()
    End Sub

    Public Sub New(ByRef Papan As List(Of List(Of Color?)))
        Me.PapanPermainan = Papan
        Me.Inisialisasi()
    End Sub

    Private Sub Inisialisasi()
        'Buat array/list papan permainan 10x20
        For i = 1 To 20
            Dim Kolom As New List(Of Nullable(Of Color))

            For j = 1 To 10
                Kolom.Add(Nothing)
            Next

            PapanPermainan.Add(Kolom)
        Next
    End Sub

    ''' <summary>
    ''' Duplikat isi papan permainan tanpa reference memori asli
    ''' </summary>
    ''' <param name="Papan">Papan yang ingin di duplikat</param>
    Public Function DuplikatPapan(ByRef Papan As List(Of List(Of Color?))) As List(Of List(Of Color?))
        Dim temp As New List(Of List(Of Color?))

        For i = 0 To Papan.Count - 1
            Dim Kolom As New List(Of Nullable(Of Color))

            For j = 0 To Papan(i).Count - 1
                Kolom.Add(Papan(i)(j))
            Next

            temp.Add(Kolom)
        Next

        Return temp
    End Function

    Public Function DuplikatBlokAktif(ByRef BlokAktif As List(Of (Integer, Integer))) As List(Of (Integer, Integer))
        Dim temp As New List(Of (Integer, Integer))

        For i = 0 To BlokAktif.Count - 1
            temp.Add(BlokAktif(i))
        Next

        Return temp
    End Function

    Public Sub Turunkan()
        Dim tempPapan = Me.DuplikatPapan(Me.PapanPermainan)
        Dim tempBlokAktif = Me.DuplikatBlokAktif(Me.BlokAktif)

        ' Turunkan dari baris yang hampir paling bawah (-1)
        For IndexBaris = 18 To 0 Step -1
            Dim BentrokanHimpunanMatrix As Boolean = False
            Dim JumlahBlokTerisi As Integer = 0

            For IndexKolom = 0 To 9
                ' Jika blok matrix ini tidak kosong
                If Not IsKosong(tempPapan, IndexBaris, IndexKolom) Then
                    JumlahBlokTerisi += 1

                    ' Jika blok matrix dibawahnya tidak kosong (sudah terisi)
                    If Not IsKosong(tempPapan, IndexBaris + 1, IndexKolom) Then
                        ' Ada bentrokan dan gak bisa diturunkan
                        BentrokanHimpunanMatrix = True
                        Exit For
                    End If
                End If
            Next

            ' Jika sudah tidak bisa digeser kebawah lagi, lanjut ke baris selanjutnya
            If BentrokanHimpunanMatrix Then
                Continue For
            End If

            ' Jika tidak ada yang perlu digeser, lanjut ke baris selanjutnya
            If JumlahBlokTerisi = 0 Then
                Continue For
            End If

            ' Geser semua kolom pada baris ini kebawah
            For IndexKolom = 0 To 9
                ' Jika blok matrix ini tidak bisa turun (ada bentrokan)
                tempPapan(IndexBaris + 1)(IndexKolom) = tempPapan(IndexBaris)(IndexKolom)
                tempPapan(IndexBaris)(IndexKolom) = Nothing

                ' Update juga blok aktif jika ada
                If tempBlokAktif.Count > 0 Then
                    For i = 0 To tempBlokAktif.Count - 1
                        Dim pair = tempBlokAktif(i)

                        ' Jika tidak cocok, skip
                        If (Not pair.Item1 = IndexBaris) OrElse (pair.Item2 = IndexKolom) Then
                            Continue For
                        End If

                        tempBlokAktif(i) = (IndexBaris + 1, IndexKolom)
                        Exit For
                    Next
                End If
            Next

            Me.PapanPermainan = tempPapan
            Me.BlokAktif = tempBlokAktif
        Next
    End Sub

    Public Function GeserX_BlokAktif(jumlah_gerak As Int16) As Boolean
        Dim tempPapan = Me.DuplikatPapan(Me.PapanPermainan)
        Dim tempBlokAktif = Me.DuplikatBlokAktif(Me.BlokAktif)

        Dim BentrokanHimpunanMatrix As Boolean = False
        Dim StepTraverse = 1 ' ke arah kanan
        Dim iBentrokSearch = Me.BlokAktif.Count - 1
        Dim toBentrokSearch = 0
        Dim stepBentrokSearch = -1

        ' Jika ingin digeser ke kiri
        If jumlah_gerak < 0 Then
            StepTraverse = -1 ' ke arah kiri

            ' Mulai cari bentrokan dari arah kanan
            iBentrokSearch = 0
            toBentrokSearch = Me.BlokAktif.Count - 1
            stepBentrokSearch = 1
        End If

        ' Jika tidak ada blok aktif, ignore
        If Me.BlokAktif.Count = 0 OrElse Me.TipeBlokAktif = String.Empty Then
            Return False
        End If

        For i = iBentrokSearch To toBentrokSearch Step stepBentrokSearch
            Dim pair = Me.BlokAktif(i)

            ' Pastikan tidak keluar dari area permainan
            If ((pair.Item2 + jumlah_gerak) < 0) OrElse (pair.Item2 + jumlah_gerak) > 9 Then
                BentrokanHimpunanMatrix = True
                Exit For
            End If

            ' Jika blok matrix ini tidak kosong
            If Not IsKosong(tempPapan, pair.Item1, pair.Item2) Then
                Dim StartIndex = pair.Item2 + StepTraverse
                Dim StopIndex = pair.Item2 + jumlah_gerak

                ' Cek apakah selama perjalanan ke tujuan jumlah gerak akan terjadi bentrokan
                For i2 = StartIndex To StopIndex Step StepTraverse
                    If Not IsKosong(tempPapan, pair.Item1, i2) Then
                        ' Ada bentrokan
                        BentrokanHimpunanMatrix = True
                        Exit For
                    End If
                Next

                If BentrokanHimpunanMatrix Then
                    Exit For
                End If

                ' Pindahkan bloknya
                tempPapan(pair.Item1)(pair.Item2 + jumlah_gerak) = tempPapan(pair.Item1)(pair.Item2)
                tempPapan(pair.Item1)(pair.Item2) = Nothing
                tempBlokAktif(i) = (pair.Item1, pair.Item2 + jumlah_gerak)
            End If
        Next

        If BentrokanHimpunanMatrix Then
            tempPapan.Clear()
            tempBlokAktif.Clear()
            Return False
        End If

        Me.PapanPermainan = tempPapan
        Me.BlokAktif = tempBlokAktif
        Return True
    End Function


#Region "Function Pembantu"
    Public Function IsKosong(ByRef blok As Color?) As Boolean
        Return (blok Is Nothing)
    End Function

    Public Function IsKosong(IndexBaris As Integer, IndexKolom As Integer) As Boolean
        Return Me.IsKosong(Me.PapanPermainan, IndexBaris, IndexKolom)
    End Function

    Public Function IsKosong(Papan As List(Of List(Of Color?)), IndexBaris As Integer, IndexKolom As Integer) As Boolean
        Return Me.IsKosong(Papan(IndexBaris)(IndexKolom))
    End Function



    Public Function AmbilData(IndexBaris As Integer, IndexKolom As Integer) As Color?
        Return Me.AmbilData(Me.PapanPermainan, IndexBaris, IndexKolom)
    End Function

    Public Function AmbilData(Papan As List(Of List(Of Color?)), IndexBaris As Integer, IndexKolom As Integer) As Color?
        Return Papan(IndexBaris)(IndexKolom)
    End Function


    Public Function AmbilKumpulanKolom(IndexBaris As Integer, Optional TanpaBlokKosong As Boolean = False) As List(Of Color?)
        Dim a = Me.PapanPermainan(IndexBaris)

        If TanpaBlokKosong Then
            Dim b As New List(Of Color?)

            For i = 0 To a.Count - 1
                If (Me.IsKosong(a(i))) Then
                    b.Add(a(i))
                End If
            Next

            Return b
        End If

        Return a
    End Function

    Public Function AmbilPointerData() As List(Of List(Of Color?))
        Return PapanPermainan
    End Function

    Public Sub SetBlokAktif(tipe As String, blok As List(Of (Integer, Integer)))
        Me.BlokAktif = blok
        Me.TipeBlokAktif = tipe
    End Sub

    Public Function GetBlokAktif() As List(Of (Integer, Integer))
        Return Me.BlokAktif
    End Function

    Public Function GetTipeBlokAktif() As String
        Return Me.TipeBlokAktif
    End Function
#End Region

End Class
