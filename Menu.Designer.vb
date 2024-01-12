<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Menu
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
        Label1 = New Label()
        StartTetrisAction = New Label()
        Label3 = New Label()
        ExitAction = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Lucida Bright", 28.0677967F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.Indigo
        Label1.Location = New Point(82, 80)
        Label1.Name = "Label1"
        Label1.Size = New Size(194, 104)
        Label1.TabIndex = 3
        Label1.Text = "Team 4" & vbCrLf & "Tetris"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' StartTetrisAction
        ' 
        StartTetrisAction.BackColor = Color.LightGray
        StartTetrisAction.Cursor = Cursors.Hand
        StartTetrisAction.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        StartTetrisAction.ForeColor = Color.Indigo
        StartTetrisAction.Location = New Point(76, 234)
        StartTetrisAction.Name = "StartTetrisAction"
        StartTetrisAction.Size = New Size(215, 43)
        StartTetrisAction.TabIndex = 4
        StartTetrisAction.Text = "START"
        StartTetrisAction.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label3
        ' 
        Label3.BackColor = Color.LightGray
        Label3.Cursor = Cursors.Hand
        Label3.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.ForeColor = Color.Indigo
        Label3.Location = New Point(76, 306)
        Label3.Name = "Label3"
        Label3.Size = New Size(215, 43)
        Label3.TabIndex = 5
        Label3.Text = "HOW TO PLAY"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ExitAction
        ' 
        ExitAction.BackColor = Color.LightGray
        ExitAction.Cursor = Cursors.Hand
        ExitAction.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        ExitAction.ForeColor = Color.Indigo
        ExitAction.Location = New Point(76, 380)
        ExitAction.Name = "ExitAction"
        ExitAction.Size = New Size(215, 43)
        ExitAction.TabIndex = 6
        ExitAction.Text = "EXIT"
        ExitAction.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Menu
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LightGray
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(373, 520)
        Controls.Add(ExitAction)
        Controls.Add(Label3)
        Controls.Add(StartTetrisAction)
        Controls.Add(Label1)
        DoubleBuffered = True
        Name = "Menu"
        Text = "Team 4 - Tetris ( Menu )"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents StartTetrisAction As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ExitAction As Label
End Class
