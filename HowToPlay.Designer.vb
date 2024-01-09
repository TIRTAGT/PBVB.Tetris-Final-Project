<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HowToPlay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        ExitAction = New Label()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Magneto", 23.7966118F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.WhiteSmoke
        Label1.Location = New Point(34, 18)
        Label1.Name = "Label1"
        Label1.Size = New Size(301, 48)
        Label1.TabIndex = 3
        Label1.Text = "How To Play"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' ExitAction
        ' 
        ExitAction.BackColor = Color.LightGray
        ExitAction.Cursor = Cursors.Hand
        ExitAction.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point)
        ExitAction.Location = New Point(120, 438)
        ExitAction.Name = "ExitAction"
        ExitAction.Size = New Size(215, 43)
        ExitAction.TabIndex = 6
        ExitAction.Text = "BACK TO MENU"
        ExitAction.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label2
        ' 
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point)
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(12, 79)
        Label2.Name = "Label2"
        Label2.Size = New Size(355, 337)
        Label2.TabIndex = 7
        Label2.Text = "D / Panah Kanan = Geser blok ke Kanan" & vbCrLf & "A / Panah Kiri = Geser blok ke Kiri" & vbCrLf & "S / Panah Bawah = Percepat penurunan blok" & vbCrLf & "Q = Putar blok -90 derajat" & vbCrLf & "E = Putar blok 90 derajat"
        ' 
        ' HowToPlay
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Unsaved_Image_1
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(373, 520)
        Controls.Add(Label2)
        Controls.Add(ExitAction)
        Controls.Add(Label1)
        DoubleBuffered = True
        Name = "HowToPlay"
        Text = "Kelompok 4 - Tetris ( Menu )"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents ExitAction As Label
    Friend WithEvents Label2 As Label
End Class
