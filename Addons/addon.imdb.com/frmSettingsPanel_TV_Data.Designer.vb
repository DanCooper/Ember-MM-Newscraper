<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsPanel_TV_Data
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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraper = New System.Windows.Forms.GroupBox()
        Me.tblScraper = New System.Windows.Forms.TableLayoutPanel()
        Me.lblForceTitleLanguage = New System.Windows.Forms.Label()
        Me.cbForceTitleLanguage = New System.Windows.Forms.ComboBox()
        Me.chkForceTitleLanguageFallback = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbScraper.SuspendLayout()
        Me.tblScraper.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(501, 444)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbScraper, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(501, 444)
        Me.tblSettings.TabIndex = 1
        '
        'gbScraper
        '
        Me.gbScraper.AutoSize = True
        Me.gbScraper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbScraper.Controls.Add(Me.tblScraper)
        Me.gbScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraper.Location = New System.Drawing.Point(3, 3)
        Me.gbScraper.Name = "gbScraper"
        Me.gbScraper.Size = New System.Drawing.Size(265, 71)
        Me.gbScraper.TabIndex = 98
        Me.gbScraper.TabStop = False
        Me.gbScraper.Text = "Scraper specific settings"
        '
        'tblScraper
        '
        Me.tblScraper.AutoSize = True
        Me.tblScraper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblScraper.ColumnCount = 2
        Me.tblScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraper.Controls.Add(Me.chkForceTitleLanguageFallback, 0, 1)
        Me.tblScraper.Controls.Add(Me.lblForceTitleLanguage, 0, 0)
        Me.tblScraper.Controls.Add(Me.cbForceTitleLanguage, 1, 0)
        Me.tblScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraper.Location = New System.Drawing.Point(3, 18)
        Me.tblScraper.Name = "tblScraper"
        Me.tblScraper.RowCount = 2
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.Size = New System.Drawing.Size(259, 50)
        Me.tblScraper.TabIndex = 1
        '
        'lblForceTitleLanguage
        '
        Me.lblForceTitleLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblForceTitleLanguage.AutoSize = True
        Me.lblForceTitleLanguage.Location = New System.Drawing.Point(3, 7)
        Me.lblForceTitleLanguage.Name = "lblForceTitleLanguage"
        Me.lblForceTitleLanguage.Size = New System.Drawing.Size(116, 13)
        Me.lblForceTitleLanguage.TabIndex = 4
        Me.lblForceTitleLanguage.Text = "Force Title Language:"
        '
        'cbForceTitleLanguage
        '
        Me.cbForceTitleLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbForceTitleLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForceTitleLanguage.FormattingEnabled = True
        Me.cbForceTitleLanguage.Items.AddRange(New Object() {"", "Argentina", "Australia", "Azerbaijan", "Belgium", "Brazil", "Bulgaria (Bulgarian title)", "Canada (English title)", "Canada (French title)", "Chile", "China (Mandarin title)", "Colombia", "Croatia", "Czech Republic", "Denmark", "Estonia", "Finland", "Finland (Swedish title)", "France", "Georgia", "Germany", "Greece", "Hong Kong", "Hong Kong (Cantonese title)", "Hong Kong (Mandarin title)", "Hungary", "Iceland", "India (Hindi title)", "Ireland", "Israel (Hebrew title)", "Italy", "Japan", "Japan (English title)", "Latvia", "Lithuania", "Mexico", "Netherlands", "New Zealand", "Panama", "Peru", "Poland", "Portugal", "Romania", "Russia", "Serbia", "Singapore", "Slovakia", "Slovenia", "South Korea", "Spain", "Sweden", "Switzerland", "Taiwan", "Turkey (Turkish title)", "UK", "Ukraine", "Uruguay", "USA", "Venezuela", "Vietnam"})
        Me.cbForceTitleLanguage.Location = New System.Drawing.Point(125, 3)
        Me.cbForceTitleLanguage.Name = "cbForceTitleLanguage"
        Me.cbForceTitleLanguage.Size = New System.Drawing.Size(131, 21)
        Me.cbForceTitleLanguage.Sorted = True
        Me.cbForceTitleLanguage.TabIndex = 77
        '
        'chkForceTitleLanguageFallback
        '
        Me.chkForceTitleLanguageFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkForceTitleLanguageFallback.AutoSize = True
        Me.tblScraper.SetColumnSpan(Me.chkForceTitleLanguageFallback, 2)
        Me.chkForceTitleLanguageFallback.Enabled = False
        Me.chkForceTitleLanguageFallback.Location = New System.Drawing.Point(3, 30)
        Me.chkForceTitleLanguageFallback.Name = "chkForceTitleLanguageFallback"
        Me.chkForceTitleLanguageFallback.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkForceTitleLanguageFallback.Size = New System.Drawing.Size(189, 17)
        Me.chkForceTitleLanguageFallback.TabIndex = 79
        Me.chkForceTitleLanguageFallback.Text = "Fall back on worldwide title"
        Me.chkForceTitleLanguageFallback.UseVisualStyleBackColor = True
        '
        'frmSettingsPanel_TV_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(501, 444)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsPanel_TV_Data"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbScraper.ResumeLayout(False)
        Me.gbScraper.PerformLayout()
        Me.tblScraper.ResumeLayout(False)
        Me.tblScraper.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbScraper As GroupBox
    Friend WithEvents tblScraper As TableLayoutPanel
    Friend WithEvents lblForceTitleLanguage As Label
    Friend WithEvents cbForceTitleLanguage As ComboBox
    Friend WithEvents chkForceTitleLanguageFallback As CheckBox
End Class
