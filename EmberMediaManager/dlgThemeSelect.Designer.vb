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
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.gbSelectTheme = New System.Windows.Forms.GroupBox()
        Me.lvThemes = New System.Windows.Forms.ListView()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.pnlThemePreview = New System.Windows.Forms.Panel()
        Me.pnlThemePreviewNoPlayer = New System.Windows.Forms.Panel()
        Me.tblThemePreviewNoPlayer = New System.Windows.Forms.TableLayoutPanel()
        Me.lblThemePreviewNoPlayer = New System.Windows.Forms.Label()
        Me.gbSelectTheme.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.pnlThemePreview.SuspendLayout()
        Me.pnlThemePreviewNoPlayer.SuspendLayout()
        Me.tblThemePreviewNoPlayer.SuspendLayout()
        Me.SuspendLayout()
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
        'pnlThemePreview
        '
        Me.pnlThemePreview.BackColor = System.Drawing.Color.DimGray
        Me.pnlThemePreview.Controls.Add(Me.pnlThemePreviewNoPlayer)
        Me.pnlThemePreview.Location = New System.Drawing.Point(12, 258)
        Me.pnlThemePreview.Name = "pnlThemePreview"
        Me.pnlThemePreview.Size = New System.Drawing.Size(441, 107)
        Me.pnlThemePreview.TabIndex = 15
        '
        'pnlThemePreviewNoPlayer
        '
        Me.pnlThemePreviewNoPlayer.BackColor = System.Drawing.Color.White
        Me.pnlThemePreviewNoPlayer.Controls.Add(Me.tblThemePreviewNoPlayer)
        Me.pnlThemePreviewNoPlayer.Location = New System.Drawing.Point(100, 27)
        Me.pnlThemePreviewNoPlayer.Name = "pnlThemePreviewNoPlayer"
        Me.pnlThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.pnlThemePreviewNoPlayer.TabIndex = 0
        '
        'tblThemePreviewNoPlayer
        '
        Me.tblThemePreviewNoPlayer.AutoSize = True
        Me.tblThemePreviewNoPlayer.ColumnCount = 1
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblThemePreviewNoPlayer.Controls.Add(Me.lblThemePreviewNoPlayer, 0, 0)
        Me.tblThemePreviewNoPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblThemePreviewNoPlayer.Location = New System.Drawing.Point(0, 0)
        Me.tblThemePreviewNoPlayer.Name = "tblThemePreviewNoPlayer"
        Me.tblThemePreviewNoPlayer.RowCount = 1
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.tblThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.tblThemePreviewNoPlayer.TabIndex = 0
        '
        'lblThemePreviewNoPlayer
        '
        Me.lblThemePreviewNoPlayer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblThemePreviewNoPlayer.AutoSize = True
        Me.lblThemePreviewNoPlayer.Location = New System.Drawing.Point(59, 21)
        Me.lblThemePreviewNoPlayer.Name = "lblThemePreviewNoPlayer"
        Me.lblThemePreviewNoPlayer.Size = New System.Drawing.Size(124, 13)
        Me.lblThemePreviewNoPlayer.TabIndex = 0
        Me.lblThemePreviewNoPlayer.Text = "no Media Player enabled"
        '
        'dlgThemeSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(638, 377)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlThemePreview)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.gbSelectTheme)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgThemeSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "dlgThemeSelect"
        Me.gbSelectTheme.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.pnlThemePreview.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.PerformLayout()
        Me.tblThemePreviewNoPlayer.ResumeLayout(False)
        Me.tblThemePreviewNoPlayer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents gbSelectTheme As System.Windows.Forms.GroupBox
    Friend WithEvents lvThemes As System.Windows.Forms.ListView
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlThemePreview As System.Windows.Forms.Panel
    Friend WithEvents pnlThemePreviewNoPlayer As System.Windows.Forms.Panel
    Friend WithEvents tblThemePreviewNoPlayer As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblThemePreviewNoPlayer As System.Windows.Forms.Label
End Class
