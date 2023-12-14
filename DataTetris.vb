Public Class DataTetris
    Private PapanPermainan As List(Of List(Of Color?))

    Public Sub New()
        Me.PapanPermainan = New List(Of List(Of Nullable(Of Color)))
        Me.Inisialisasi()
    End Sub

    Public Sub New(ByRef Papan As List(Of List(Of Nullable(Of Color))))
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

    Public Sub Turunkan()
        ' Turunkan dari baris yang hampir paling bawah (-1)
        For IndexBaris = 18 To 0 Step -1
            Dim BentrokanHimpunanMatrix As Boolean = False
            Dim JumlahBlokTerisi As Integer = 0

            For IndexKolom = 0 To 9
                ' Jika blok matrix ini tidak kosong
                If Not IsKosong(IndexBaris, IndexKolom) Then
                    JumlahBlokTerisi += 1

                    ' Jika blok matrix dibawahnya tidak kosong (sudah terisi)
                    If Not IsKosong(IndexBaris + 1, IndexKolom) Then
                        ' Ada bentrokan dan gak bisa diturunkan
                        BentrokanHimpunanMatrix = True
                        Exit For
                    End If
                End If
            Next

            ' Jika sudah tidak bisa digeser kebawah lagi, stop.
            If BentrokanHimpunanMatrix Then
                Exit For
            End If

            ' Jika tidak ada yang perlu digeser, continue
            If JumlahBlokTerisi = 0 Then
                Continue For
            End If

            ' Geser semua kolom pada baris ini kebawah
            For IndexKolom = 0 To 9
                ' Jika blok matrix ini tidak bisa turun (ada bentrokan)
                Me.PapanPermainan(IndexBaris + 1)(IndexKolom) = Me.PapanPermainan(IndexBaris)(IndexKolom)
                Me.PapanPermainan(IndexBaris)(IndexKolom) = Nothing
            Next
        Next
    End Sub

    Public Function IsKosong(ByRef blok As Color?) As Boolean
        Return (blok Is Nothing)
    End Function

    Public Function IsKosong(IndexBaris As Integer, IndexKolom As Integer) As Boolean
        Return Me.IsKosong(Me.AmbilData(IndexBaris, IndexKolom))
    End Function

    Public Function AmbilData(IndexBaris As Integer, IndexKolom As Integer) As Color?
        Return PapanPermainan(IndexBaris)(IndexKolom)
    End Function

    Public Function AmbilKumpulanKolom(IndexBaris As Integer, Optional TanpaBlokKosong As Boolean = False) As List(Of Color?)
        Dim a = PapanPermainan(IndexBaris)

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
End Class
