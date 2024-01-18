<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tetris
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		GameArea = New Panel()
		StartTetrisAction = New Label()
		Panel1 = New Panel()
		Panel2 = New Panel()
		ScoreValueLabel = New Label()
		Label1 = New Label()
		Panel3 = New Panel()
		TargetText8Label = New Label()
		TargetText7Label = New Label()
		TargetText6Label = New Label()
		TargetText5Label = New Label()
		TargetText4Label = New Label()
		TargetText3Label = New Label()
		TargetText2Label = New Label()
		TargetText1Label = New Label()
		BackButton = New Button()
		PausePanel = New Panel()
		Panel1.SuspendLayout()
		Panel3.SuspendLayout()
		SuspendLayout()
		' 
		' GameArea
		' 
		GameArea.BackColor = Color.Silver
		GameArea.BorderStyle = BorderStyle.Fixed3D
		GameArea.Font = New Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point)
		GameArea.Location = New Point(26, 84)
		GameArea.Margin = New Padding(2)
		GameArea.Name = "GameArea"
		GameArea.Size = New Size(280, 497)
		GameArea.TabIndex = 0
		' 
		' StartTetrisAction
		' 
		StartTetrisAction.BackColor = Color.LightGray
		StartTetrisAction.Cursor = Cursors.Hand
		StartTetrisAction.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point)
		StartTetrisAction.ForeColor = Color.Indigo
		StartTetrisAction.Location = New Point(26, 20)
		StartTetrisAction.Margin = New Padding(2, 0, 2, 0)
		StartTetrisAction.Name = "StartTetrisAction"
		StartTetrisAction.Size = New Size(425, 35)
		StartTetrisAction.TabIndex = 5
		StartTetrisAction.Text = "GAMES TETRIS"
		StartTetrisAction.TextAlign = ContentAlignment.MiddleCenter
		' 
		' Panel1
		' 
		Panel1.BorderStyle = BorderStyle.Fixed3D
		Panel1.Controls.Add(Panel2)
		Panel1.Controls.Add(ScoreValueLabel)
		Panel1.Controls.Add(Label1)
		Panel1.Location = New Point(389, 83)
		Panel1.Margin = New Padding(2)
		Panel1.Name = "Panel1"
		Panel1.Size = New Size(164, 86)
		Panel1.TabIndex = 6
		' 
		' Panel2
		' 
		Panel2.BackColor = Color.LightGray
		Panel2.Location = New Point(20, 68)
		Panel2.Margin = New Padding(2)
		Panel2.Name = "Panel2"
		Panel2.Size = New Size(122, 2)
		Panel2.TabIndex = 2
		' 
		' ScoreValueLabel
		' 
		ScoreValueLabel.BackColor = Color.Transparent
		ScoreValueLabel.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point)
		ScoreValueLabel.ForeColor = Color.LightGray
		ScoreValueLabel.Location = New Point(24, 34)
		ScoreValueLabel.Margin = New Padding(2, 0, 2, 0)
		ScoreValueLabel.Name = "ScoreValueLabel"
		ScoreValueLabel.Size = New Size(122, 32)
		ScoreValueLabel.TabIndex = 1
		ScoreValueLabel.Text = "0"
		ScoreValueLabel.TextAlign = ContentAlignment.MiddleLeft
		' 
		' Label1
		' 
		Label1.AutoSize = True
		Label1.BackColor = Color.Transparent
		Label1.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point)
		Label1.ForeColor = Color.LightGray
		Label1.Location = New Point(5, 2)
		Label1.Margin = New Padding(2, 0, 2, 0)
		Label1.Name = "Label1"
		Label1.Size = New Size(45, 19)
		Label1.TabIndex = 0
		Label1.Text = "score"
		' 
		' Panel3
		' 
		Panel3.BackColor = Color.Transparent
		Panel3.BorderStyle = BorderStyle.Fixed3D
		Panel3.Controls.Add(TargetText8Label)
		Panel3.Controls.Add(TargetText7Label)
		Panel3.Controls.Add(TargetText6Label)
		Panel3.Controls.Add(TargetText5Label)
		Panel3.Controls.Add(TargetText4Label)
		Panel3.Controls.Add(TargetText3Label)
		Panel3.Controls.Add(TargetText2Label)
		Panel3.Controls.Add(TargetText1Label)
		Panel3.ForeColor = Color.LightGray
		Panel3.Location = New Point(389, 227)
		Panel3.Margin = New Padding(2)
		Panel3.Name = "Panel3"
		Panel3.Size = New Size(164, 226)
		Panel3.TabIndex = 7
		' 
		' TargetText8Label
		' 
		TargetText8Label.AutoSize = True
		TargetText8Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText8Label.Location = New Point(5, 203)
		TargetText8Label.Margin = New Padding(2, 0, 2, 0)
		TargetText8Label.Name = "TargetText8Label"
		TargetText8Label.Size = New Size(96, 20)
		TargetText8Label.TabIndex = 7
		TargetText8Label.Text = "[ Kalimat 8 ]"
		' 
		' TargetText7Label
		' 
		TargetText7Label.AutoSize = True
		TargetText7Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText7Label.Location = New Point(5, 175)
		TargetText7Label.Margin = New Padding(2, 0, 2, 0)
		TargetText7Label.Name = "TargetText7Label"
		TargetText7Label.Size = New Size(96, 20)
		TargetText7Label.TabIndex = 6
		TargetText7Label.Text = "[ Kalimat 7 ]"
		' 
		' TargetText6Label
		' 
		TargetText6Label.AutoSize = True
		TargetText6Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText6Label.Location = New Point(5, 146)
		TargetText6Label.Margin = New Padding(2, 0, 2, 0)
		TargetText6Label.Name = "TargetText6Label"
		TargetText6Label.Size = New Size(96, 20)
		TargetText6Label.TabIndex = 5
		TargetText6Label.Text = "[ Kalimat 6 ]"
		' 
		' TargetText5Label
		' 
		TargetText5Label.AutoSize = True
		TargetText5Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText5Label.Location = New Point(5, 118)
		TargetText5Label.Margin = New Padding(2, 0, 2, 0)
		TargetText5Label.Name = "TargetText5Label"
		TargetText5Label.Size = New Size(96, 20)
		TargetText5Label.TabIndex = 4
		TargetText5Label.Text = "[ Kalimat 5 ]"
		' 
		' TargetText4Label
		' 
		TargetText4Label.AutoSize = True
		TargetText4Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText4Label.Location = New Point(5, 89)
		TargetText4Label.Margin = New Padding(2, 0, 2, 0)
		TargetText4Label.Name = "TargetText4Label"
		TargetText4Label.Size = New Size(96, 20)
		TargetText4Label.TabIndex = 3
		TargetText4Label.Text = "[ Kalimat 4 ]"
		' 
		' TargetText3Label
		' 
		TargetText3Label.AutoSize = True
		TargetText3Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText3Label.Location = New Point(5, 61)
		TargetText3Label.Margin = New Padding(2, 0, 2, 0)
		TargetText3Label.Name = "TargetText3Label"
		TargetText3Label.Size = New Size(96, 20)
		TargetText3Label.TabIndex = 2
		TargetText3Label.Text = "[ Kalimat 3 ]"
		' 
		' TargetText2Label
		' 
		TargetText2Label.AutoSize = True
		TargetText2Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText2Label.Location = New Point(5, 33)
		TargetText2Label.Margin = New Padding(2, 0, 2, 0)
		TargetText2Label.Name = "TargetText2Label"
		TargetText2Label.Size = New Size(96, 20)
		TargetText2Label.TabIndex = 1
		TargetText2Label.Text = "[ Kalimat 2 ]"
		' 
		' TargetText1Label
		' 
		TargetText1Label.AutoSize = True
		TargetText1Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		TargetText1Label.Location = New Point(5, 4)
		TargetText1Label.Margin = New Padding(2, 0, 2, 0)
		TargetText1Label.Name = "TargetText1Label"
		TargetText1Label.Size = New Size(96, 20)
		TargetText1Label.TabIndex = 0
		TargetText1Label.Text = "[ Kalimat 1 ]"
		' 
		' BackButton
		' 
		BackButton.BackColor = Color.Silver
		BackButton.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
		BackButton.Location = New Point(26, 597)
		BackButton.Margin = New Padding(2)
		BackButton.Name = "BackButton"
		BackButton.Size = New Size(63, 28)
		BackButton.TabIndex = 8
		BackButton.TabStop = False
		BackButton.Text = "Back"
		BackButton.UseVisualStyleBackColor = False
		' 
		' PausePanel
		' 
		PausePanel.BackgroundImage = My.Resources.Resources.PauseIcon
		PausePanel.BackgroundImageLayout = ImageLayout.Zoom
		PausePanel.Location = New Point(357, 84)
		PausePanel.Margin = New Padding(2)
		PausePanel.Name = "PausePanel"
		PausePanel.Size = New Size(28, 30)
		PausePanel.TabIndex = 9
		' 
		' Tetris
		' 
		AutoScaleDimensions = New SizeF(96F, 96F)
		AutoScaleMode = AutoScaleMode.Dpi
		BackColor = Color.LightGray
		ClientSize = New Size(569, 598)
		Controls.Add(PausePanel)
		Controls.Add(BackButton)
		Controls.Add(Panel3)
		Controls.Add(Panel1)
		Controls.Add(StartTetrisAction)
		Controls.Add(GameArea)
		Margin = New Padding(2)
		Name = "Tetris"
		StartPosition = FormStartPosition.CenterScreen
		Text = "Team 4 - Tetris ( Game Area )"
		Panel1.ResumeLayout(False)
		Panel1.PerformLayout()
		Panel3.ResumeLayout(False)
		Panel3.PerformLayout()
		ResumeLayout(False)
	End Sub

	Friend WithEvents GameArea As Panel
    Friend WithEvents StartTetrisAction As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ScoreValueLabel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TargetText1Label As Label
    Friend WithEvents TargetText7Label As Label
    Friend WithEvents TargetText6Label As Label
    Friend WithEvents TargetText5Label As Label
    Friend WithEvents TargetText4Label As Label
    Friend WithEvents TargetText3Label As Label
    Friend WithEvents TargetText2Label As Label
    Friend WithEvents TargetText8Label As Label
	Friend WithEvents BackButton As Button
	Friend WithEvents PausePanel As Panel
End Class
