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
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Magneto", 23.7966118F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.ForeColor = Color.WhiteSmoke
        Label1.Location = New Point(163, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(458, 48)
        Label1.TabIndex = 3
        Label1.Text = "Kelompok 4 - Tetris"
        ' 
        ' StartTetrisAction
        ' 
        StartTetrisAction.AutoSize = True
        StartTetrisAction.BackColor = Color.Transparent
        StartTetrisAction.Cursor = Cursors.Hand
        StartTetrisAction.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        StartTetrisAction.Location = New Point(282, 140)
        StartTetrisAction.Name = "StartTetrisAction"
        StartTetrisAction.Size = New Size(234, 37)
        StartTetrisAction.TabIndex = 4
        StartTetrisAction.Text = "Mulai Permainan"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Cursor = Cursors.Hand
        Label3.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.Location = New Point(303, 203)
        Label3.Name = "Label3"
        Label3.Size = New Size(189, 37)
        Label3.TabIndex = 5
        Label3.Text = "Cara Bermain"
        ' 
        ' ExitAction
        ' 
        ExitAction.AutoSize = True
        ExitAction.BackColor = Color.Transparent
        ExitAction.Cursor = Cursors.Hand
        ExitAction.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        ExitAction.Location = New Point(360, 329)
        ExitAction.Name = "ExitAction"
        ExitAction.Size = New Size(65, 37)
        ExitAction.TabIndex = 6
        ExitAction.Text = "Exit"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Cursor = Cursors.Hand
        Label5.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point)
        Label5.Location = New Point(335, 269)
        Label5.Name = "Label5"
        Label5.Size = New Size(122, 37)
        Label5.TabIndex = 7
        Label5.Text = "Settings"
        ' 
        ' Menu
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Unsaved_Image_1
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(760, 428)
        Controls.Add(Label5)
        Controls.Add(ExitAction)
        Controls.Add(Label3)
        Controls.Add(StartTetrisAction)
        Controls.Add(Label1)
        DoubleBuffered = True
        Name = "Menu"
        Text = "Kelompok 4 - Tetris ( Menu )"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents StartTetrisAction As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ExitAction As Label
    Friend WithEvents Label5 As Label
End Class
