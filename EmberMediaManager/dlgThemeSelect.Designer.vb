<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgThemeSelect
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgThemeSelect))
        Me.vlcPlayer = New AxAXVLC.AxVLCPlugin2()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.gbSelectTheme = New System.Windows.Forms.GroupBox()
        Me.lvThemes = New System.Windows.Forms.ListView()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        CType(Me.vlcPlayer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbSelectTheme.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'vlcPlayer
        '
        Me.vlcPlayer.Enabled = True
        Me.vlcPlayer.Location = New System.Drawing.Point(12, 258)
        Me.vlcPlayer.Name = "vlcPlayer"
        Me.vlcPlayer.OcxState = CType(resources.GetObject("vlcPlayer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.vlcPlayer.Size = New System.Drawing.Size(460, 56)
        Me.vlcPlayer.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(470, 342)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(75, 23)
        Me.OK_Button.TabIndex = 1
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = True
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(551, 342)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Cancel_Button.TabIndex = 2
        Me.Cancel_Button.Text = "Cancel"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'gbSelectTheme
        '
        Me.gbSelectTheme.Controls.Add(Me.lvThemes)
        Me.gbSelectTheme.Location = New System.Drawing.Point(12, 12)
        Me.gbSelectTheme.Name = "gbSelectTheme"
        Me.gbSelectTheme.Size = New System.Drawing.Size(614, 240)
        Me.gbSelectTheme.TabIndex = 3
        Me.gbSelectTheme.TabStop = False
        Me.gbSelectTheme.Text = "Select Theme to Scrape"
        '
        'lvThemes
        '
        Me.lvThemes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvThemes.Location = New System.Drawing.Point(6, 19)
        Me.lvThemes.Name = "lvThemes"
        Me.lvThemes.Size = New System.Drawing.Size(602, 215)
        Me.lvThemes.TabIndex = 5
        Me.lvThemes.UseCompatibleStateImageBehavior = False
        Me.lvThemes.View = System.Windows.Forms.View.Details
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.White
        Me.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.pbStatus)
        Me.pnlStatus.Location = New System.Drawing.Point(218, 87)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(200, 54)
        Me.pnlStatus.TabIndex = 4
        Me.pnlStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(3, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(121, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Compiling trailer list..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(3, 32)
        Me.pbStatus.MarqueeAnimationSpeed = 25
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(192, 17)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbStatus.TabIndex = 1
        '
        'dlgThemeSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(638, 377)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.gbSelectTheme)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.vlcPlayer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgThemeSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "dlgThemeSelect"
        CType(Me.vlcPlayer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbSelectTheme.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents vlcPlayer As AxAXVLC.AxVLCPlugin2
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents gbSelectTheme As System.Windows.Forms.GroupBox
    Friend WithEvents lvThemes As System.Windows.Forms.ListView
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
End Class
