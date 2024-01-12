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
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' GameArea
        ' 
        GameArea.BackColor = Color.Silver
        GameArea.BorderStyle = BorderStyle.Fixed3D
        GameArea.Font = New Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point)
        GameArea.Location = New Point(32, 103)
        GameArea.Name = "GameArea"
        GameArea.Size = New Size(278, 494)
        GameArea.TabIndex = 0
        ' 
        ' StartTetrisAction
        ' 
        StartTetrisAction.BackColor = Color.LightGray
        StartTetrisAction.Cursor = Cursors.Hand
        StartTetrisAction.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point)
        StartTetrisAction.ForeColor = Color.Indigo
        StartTetrisAction.Location = New Point(32, 25)
        StartTetrisAction.Name = "StartTetrisAction"
        StartTetrisAction.Size = New Size(522, 43)
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
        Panel1.Location = New Point(353, 103)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(201, 105)
        Panel1.TabIndex = 6
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.LightGray
        Panel2.Location = New Point(25, 84)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(150, 3)
        Panel2.TabIndex = 2
        ' 
        ' ScoreValueLabel
        ' 
        ScoreValueLabel.BackColor = Color.Transparent
        ScoreValueLabel.Font = New Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point)
        ScoreValueLabel.ForeColor = Color.LightGray
        ScoreValueLabel.Location = New Point(30, 42)
        ScoreValueLabel.Name = "ScoreValueLabel"
        ScoreValueLabel.Size = New Size(150, 39)
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
        Label1.Location = New Point(6, 2)
        Label1.Name = "Label1"
        Label1.Size = New Size(57, 25)
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
        Panel3.Location = New Point(353, 280)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(201, 277)
        Panel3.TabIndex = 7
        ' 
        ' TargetText8Label
        ' 
        TargetText8Label.AutoSize = True
        TargetText8Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText8Label.Location = New Point(6, 250)
        TargetText8Label.Name = "TargetText8Label"
        TargetText8Label.Size = New Size(118, 25)
        TargetText8Label.TabIndex = 7
        TargetText8Label.Text = "[ Kalimat 8 ]"
        ' 
        ' TargetText7Label
        ' 
        TargetText7Label.AutoSize = True
        TargetText7Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText7Label.Location = New Point(6, 215)
        TargetText7Label.Name = "TargetText7Label"
        TargetText7Label.Size = New Size(118, 25)
        TargetText7Label.TabIndex = 6
        TargetText7Label.Text = "[ Kalimat 7 ]"
        ' 
        ' TargetText6Label
        ' 
        TargetText6Label.AutoSize = True
        TargetText6Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText6Label.Location = New Point(6, 180)
        TargetText6Label.Name = "TargetText6Label"
        TargetText6Label.Size = New Size(118, 25)
        TargetText6Label.TabIndex = 5
        TargetText6Label.Text = "[ Kalimat 6 ]"
        ' 
        ' TargetText5Label
        ' 
        TargetText5Label.AutoSize = True
        TargetText5Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText5Label.Location = New Point(6, 145)
        TargetText5Label.Name = "TargetText5Label"
        TargetText5Label.Size = New Size(118, 25)
        TargetText5Label.TabIndex = 4
        TargetText5Label.Text = "[ Kalimat 5 ]"
        ' 
        ' TargetText4Label
        ' 
        TargetText4Label.AutoSize = True
        TargetText4Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText4Label.Location = New Point(6, 110)
        TargetText4Label.Name = "TargetText4Label"
        TargetText4Label.Size = New Size(118, 25)
        TargetText4Label.TabIndex = 3
        TargetText4Label.Text = "[ Kalimat 4 ]"
        ' 
        ' TargetText3Label
        ' 
        TargetText3Label.AutoSize = True
        TargetText3Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText3Label.Location = New Point(6, 75)
        TargetText3Label.Name = "TargetText3Label"
        TargetText3Label.Size = New Size(118, 25)
        TargetText3Label.TabIndex = 2
        TargetText3Label.Text = "[ Kalimat 3 ]"
        ' 
        ' TargetText2Label
        ' 
        TargetText2Label.AutoSize = True
        TargetText2Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText2Label.Location = New Point(6, 40)
        TargetText2Label.Name = "TargetText2Label"
        TargetText2Label.Size = New Size(118, 25)
        TargetText2Label.TabIndex = 1
        TargetText2Label.Text = "[ Kalimat 2 ]"
        ' 
        ' TargetText1Label
        ' 
        TargetText1Label.AutoSize = True
        TargetText1Label.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point)
        TargetText1Label.Location = New Point(6, 5)
        TargetText1Label.Name = "TargetText1Label"
        TargetText1Label.Size = New Size(118, 25)
        TargetText1Label.TabIndex = 0
        TargetText1Label.Text = "[ Kalimat 1 ]"
        ' 
        ' Tetris
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LightGray
        ClientSize = New Size(576, 659)
        Controls.Add(Panel3)
        Controls.Add(Panel1)
        Controls.Add(StartTetrisAction)
        Controls.Add(GameArea)
        Name = "Tetris"
        Text = "Tetris"
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
End Class
