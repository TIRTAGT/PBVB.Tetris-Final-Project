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
        SuspendLayout()
        ' 
        ' GameArea
        ' 
        GameArea.BackColor = Color.Transparent
        GameArea.BorderStyle = BorderStyle.Fixed3D
        GameArea.Location = New Point(320, 0)
        GameArea.Name = "GameArea"
        GameArea.Size = New Size(320, 640)
        GameArea.TabIndex = 0
        ' 
        ' Tetris
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.Unsaved_Image_1
        ClientSize = New Size(949, 656)
        Controls.Add(GameArea)
        Name = "Tetris"
        Text = "Tetris"
        ResumeLayout(False)
    End Sub

    Friend WithEvents GameArea As Panel
End Class
