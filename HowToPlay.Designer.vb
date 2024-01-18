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
		Label1.Font = New Font("Lucida Bright", 28.0677967F, FontStyle.Bold, GraphicsUnit.Point)
		Label1.ForeColor = Color.Indigo
		Label1.Location = New Point(34, 18)
		Label1.Name = "Label1"
		Label1.Size = New Size(318, 52)
		Label1.TabIndex = 3
		Label1.Text = "How To Play"
		Label1.TextAlign = ContentAlignment.TopCenter
		' 
		' ExitAction
		' 
		ExitAction.BackColor = Color.LightGray
		ExitAction.Cursor = Cursors.Hand
		ExitAction.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point)
		ExitAction.ForeColor = Color.Indigo
		ExitAction.Location = New Point(152, 459)
		ExitAction.Name = "ExitAction"
		ExitAction.Size = New Size(215, 43)
		ExitAction.TabIndex = 6
		ExitAction.Text = "BACK TO MENU"
		ExitAction.TextAlign = ContentAlignment.MiddleCenter
		' 
		' Label2
		' 
		Label2.BackColor = Color.LightGray
		Label2.Font = New Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point)
		Label2.ForeColor = Color.Indigo
		Label2.Location = New Point(12, 101)
		Label2.Name = "Label2"
		Label2.Size = New Size(355, 334)
		Label2.TabIndex = 7
		Label2.Text = "#D / Panah Kanan = Geser Blok ke Kanan" & vbCrLf & vbCrLf & "#A / Panah Kiri = Geser Blok ke Kiri" & vbCrLf & vbCrLf & "#S / Panah Bawah = Percepat Penurunan Blok" & vbCrLf & vbCrLf & "#Spasi = Langsung Turunkan Blok"
		' 
		' HowToPlay
		' 
		AutoScaleDimensions = New SizeF(8F, 20F)
		AutoScaleMode = AutoScaleMode.Font
		BackColor = Color.LightGray
		BackgroundImageLayout = ImageLayout.Stretch
		ClientSize = New Size(373, 520)
		Controls.Add(Label2)
		Controls.Add(ExitAction)
		Controls.Add(Label1)
		DoubleBuffered = True
		Name = "HowToPlay"
		Text = "Team 4 - Tetris ( How To Play )"
		ResumeLayout(False)
		PerformLayout()
	End Sub
	Friend WithEvents Label1 As Label
    Friend WithEvents ExitAction As Label
    Friend WithEvents Label2 As Label
End Class
