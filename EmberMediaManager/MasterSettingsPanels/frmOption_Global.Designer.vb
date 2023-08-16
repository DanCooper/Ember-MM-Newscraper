<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOption_Global
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_Global))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSortTokens = New System.Windows.Forms.GroupBox()
        Me.tblSortTokens = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvSortTokens = New System.Windows.Forms.DataGridView()
        Me.colSortTokens = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSortTokensDefaults = New System.Windows.Forms.Button()
        Me.gbMiscellaneous = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMisc = New System.Windows.Forms.TableLayoutPanel()
        Me.chkImageFilterAutoscraper = New System.Windows.Forms.CheckBox()
        Me.chklImageFilterImageDialog = New System.Windows.Forms.CheckBox()
        Me.chkImageFilter = New System.Windows.Forms.CheckBox()
        Me.chkCheckForUpdates = New System.Windows.Forms.CheckBox()
        Me.chkDigitGrpSymbolVotes = New System.Windows.Forms.CheckBox()
        Me.btnDigitGrpSymbolSettings = New System.Windows.Forms.Button()
        Me.txtImageFilterPosterMatchRate = New System.Windows.Forms.TextBox()
        Me.lblImageFilterPosterMatchRate = New System.Windows.Forms.Label()
        Me.chkImageFilterPoster = New System.Windows.Forms.CheckBox()
        Me.txtImageFilterFanartMatchRate = New System.Windows.Forms.TextBox()
        Me.lblImageFilterFanartMatchRate = New System.Windows.Forms.Label()
        Me.chkImageFilterFanart = New System.Windows.Forms.CheckBox()
        Me.chkShowNews = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbSortTokens.SuspendLayout()
        Me.tblSortTokens.SuspendLayout()
        CType(Me.dgvSortTokens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMiscellaneous.SuspendLayout()
        Me.tblGeneralMisc.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(643, 266)
        Me.pnlSettings.TabIndex = 11
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 3
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbSortTokens, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbMiscellaneous, 1, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Size = New System.Drawing.Size(643, 266)
        Me.tblSettings.TabIndex = 17
        '
        'gbSortTokens
        '
        Me.gbSortTokens.AutoSize = True
        Me.gbSortTokens.Controls.Add(Me.tblSortTokens)
        Me.gbSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSortTokens.Location = New System.Drawing.Point(3, 3)
        Me.gbSortTokens.Name = "gbSortTokens"
        Me.gbSortTokens.Size = New System.Drawing.Size(228, 221)
        Me.gbSortTokens.TabIndex = 72
        Me.gbSortTokens.TabStop = False
        Me.gbSortTokens.Text = "Sort Tokens to Ignore"
        '
        'tblSortTokens
        '
        Me.tblSortTokens.AutoSize = True
        Me.tblSortTokens.ColumnCount = 1
        Me.tblSortTokens.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSortTokens.Controls.Add(Me.dgvSortTokens, 0, 0)
        Me.tblSortTokens.Controls.Add(Me.btnSortTokensDefaults, 0, 1)
        Me.tblSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSortTokens.Location = New System.Drawing.Point(3, 18)
        Me.tblSortTokens.Name = "tblSortTokens"
        Me.tblSortTokens.RowCount = 2
        Me.tblSortTokens.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSortTokens.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSortTokens.Size = New System.Drawing.Size(222, 200)
        Me.tblSortTokens.TabIndex = 11
        '
        'dgvSortTokens
        '
        Me.dgvSortTokens.AllowUserToResizeRows = False
        Me.dgvSortTokens.BackgroundColor = System.Drawing.Color.White
        Me.dgvSortTokens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSortTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSortTokens.ColumnHeadersVisible = False
        Me.dgvSortTokens.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSortTokens})
        Me.dgvSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSortTokens.Location = New System.Drawing.Point(3, 3)
        Me.dgvSortTokens.Name = "dgvSortTokens"
        Me.dgvSortTokens.RowHeadersWidth = 25
        Me.dgvSortTokens.ShowCellErrors = False
        Me.dgvSortTokens.ShowCellToolTips = False
        Me.dgvSortTokens.ShowRowErrors = False
        Me.dgvSortTokens.Size = New System.Drawing.Size(216, 165)
        Me.dgvSortTokens.TabIndex = 10
        '
        'colSortTokens
        '
        Me.colSortTokens.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSortTokens.HeaderText = "Sort Tokens"
        Me.colSortTokens.Name = "colSortTokens"
        '
        'btnSortTokensDefaults
        '
        Me.btnSortTokensDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSortTokensDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSortTokensDefaults.Image = CType(resources.GetObject("btnSortTokensDefaults.Image"), System.Drawing.Image)
        Me.btnSortTokensDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSortTokensDefaults.Location = New System.Drawing.Point(114, 174)
        Me.btnSortTokensDefaults.Name = "btnSortTokensDefaults"
        Me.btnSortTokensDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnSortTokensDefaults.TabIndex = 2
        Me.btnSortTokensDefaults.Text = "Defaults"
        Me.btnSortTokensDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSortTokensDefaults.UseVisualStyleBackColor = True
        '
        'gbMiscellaneous
        '
        Me.gbMiscellaneous.AutoSize = True
        Me.gbMiscellaneous.Controls.Add(Me.tblGeneralMisc)
        Me.gbMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMiscellaneous.Location = New System.Drawing.Point(237, 3)
        Me.gbMiscellaneous.Name = "gbMiscellaneous"
        Me.gbMiscellaneous.Size = New System.Drawing.Size(359, 221)
        Me.gbMiscellaneous.TabIndex = 1
        Me.gbMiscellaneous.TabStop = False
        Me.gbMiscellaneous.Text = "Miscellaneous"
        '
        'tblGeneralMisc
        '
        Me.tblGeneralMisc.AutoSize = True
        Me.tblGeneralMisc.ColumnCount = 4
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMisc.Controls.Add(Me.chkImageFilterAutoscraper, 1, 8)
        Me.tblGeneralMisc.Controls.Add(Me.chklImageFilterImageDialog, 1, 9)
        Me.tblGeneralMisc.Controls.Add(Me.chkImageFilter, 0, 7)
        Me.tblGeneralMisc.Controls.Add(Me.chkCheckForUpdates, 0, 0)
        Me.tblGeneralMisc.Controls.Add(Me.chkDigitGrpSymbolVotes, 0, 5)
        Me.tblGeneralMisc.Controls.Add(Me.btnDigitGrpSymbolSettings, 3, 5)
        Me.tblGeneralMisc.Controls.Add(Me.txtImageFilterPosterMatchRate, 3, 11)
        Me.tblGeneralMisc.Controls.Add(Me.lblImageFilterPosterMatchRate, 2, 11)
        Me.tblGeneralMisc.Controls.Add(Me.chkImageFilterPoster, 1, 11)
        Me.tblGeneralMisc.Controls.Add(Me.txtImageFilterFanartMatchRate, 3, 13)
        Me.tblGeneralMisc.Controls.Add(Me.lblImageFilterFanartMatchRate, 2, 13)
        Me.tblGeneralMisc.Controls.Add(Me.chkImageFilterFanart, 1, 13)
        Me.tblGeneralMisc.Controls.Add(Me.chkShowNews, 0, 4)
        Me.tblGeneralMisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMisc.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMisc.Name = "tblGeneralMisc"
        Me.tblGeneralMisc.RowCount = 14
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMisc.Size = New System.Drawing.Size(353, 200)
        Me.tblGeneralMisc.TabIndex = 17
        '
        'chkImageFilterAutoscraper
        '
        Me.chkImageFilterAutoscraper.AutoSize = True
        Me.chkImageFilterAutoscraper.Enabled = False
        Me.chkImageFilterAutoscraper.Location = New System.Drawing.Point(23, 101)
        Me.chkImageFilterAutoscraper.Name = "chkImageFilterAutoscraper"
        Me.chkImageFilterAutoscraper.Size = New System.Drawing.Size(88, 17)
        Me.chkImageFilterAutoscraper.TabIndex = 10
        Me.chkImageFilterAutoscraper.Text = "Autoscraper"
        Me.chkImageFilterAutoscraper.UseVisualStyleBackColor = True
        '
        'chklImageFilterImageDialog
        '
        Me.chklImageFilterImageDialog.AutoSize = True
        Me.chklImageFilterImageDialog.Enabled = False
        Me.chklImageFilterImageDialog.Location = New System.Drawing.Point(23, 124)
        Me.chklImageFilterImageDialog.Name = "chklImageFilterImageDialog"
        Me.chklImageFilterImageDialog.Size = New System.Drawing.Size(90, 17)
        Me.chklImageFilterImageDialog.TabIndex = 8
        Me.chklImageFilterImageDialog.Text = "Imagedialog"
        Me.chklImageFilterImageDialog.UseVisualStyleBackColor = True
        '
        'chkImageFilter
        '
        Me.chkImageFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkImageFilter.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkImageFilter, 4)
        Me.chkImageFilter.Location = New System.Drawing.Point(3, 78)
        Me.chkImageFilter.Name = "chkImageFilter"
        Me.chkImageFilter.Size = New System.Drawing.Size(261, 17)
        Me.chkImageFilter.TabIndex = 8
        Me.chkImageFilter.Text = "Activate ImageFilter to avoid duplicate images"
        Me.chkImageFilter.UseVisualStyleBackColor = True
        '
        'chkCheckForUpdates
        '
        Me.chkCheckForUpdates.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCheckForUpdates.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkCheckForUpdates, 4)
        Me.chkCheckForUpdates.Location = New System.Drawing.Point(3, 3)
        Me.chkCheckForUpdates.Name = "chkCheckForUpdates"
        Me.chkCheckForUpdates.Size = New System.Drawing.Size(121, 17)
        Me.chkCheckForUpdates.TabIndex = 0
        Me.chkCheckForUpdates.Text = "Check for Updates"
        Me.chkCheckForUpdates.UseVisualStyleBackColor = True
        '
        'chkDigitGrpSymbolVotes
        '
        Me.chkDigitGrpSymbolVotes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDigitGrpSymbolVotes.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkDigitGrpSymbolVotes, 3)
        Me.chkDigitGrpSymbolVotes.Location = New System.Drawing.Point(3, 52)
        Me.chkDigitGrpSymbolVotes.Name = "chkDigitGrpSymbolVotes"
        Me.chkDigitGrpSymbolVotes.Size = New System.Drawing.Size(245, 17)
        Me.chkDigitGrpSymbolVotes.TabIndex = 6
        Me.chkDigitGrpSymbolVotes.Text = "Use digit grouping symbol for Votes count"
        Me.chkDigitGrpSymbolVotes.UseVisualStyleBackColor = True
        '
        'btnDigitGrpSymbolSettings
        '
        Me.btnDigitGrpSymbolSettings.AutoSize = True
        Me.btnDigitGrpSymbolSettings.Location = New System.Drawing.Point(275, 49)
        Me.btnDigitGrpSymbolSettings.Name = "btnDigitGrpSymbolSettings"
        Me.btnDigitGrpSymbolSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnDigitGrpSymbolSettings.TabIndex = 7
        Me.btnDigitGrpSymbolSettings.Text = "Settings"
        Me.btnDigitGrpSymbolSettings.UseVisualStyleBackColor = True
        '
        'txtImageFilterPosterMatchRate
        '
        Me.txtImageFilterPosterMatchRate.Enabled = False
        Me.txtImageFilterPosterMatchRate.Location = New System.Drawing.Point(275, 147)
        Me.txtImageFilterPosterMatchRate.Name = "txtImageFilterPosterMatchRate"
        Me.txtImageFilterPosterMatchRate.Size = New System.Drawing.Size(44, 22)
        Me.txtImageFilterPosterMatchRate.TabIndex = 13
        '
        'lblImageFilterPosterMatchRate
        '
        Me.lblImageFilterPosterMatchRate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblImageFilterPosterMatchRate.AutoSize = True
        Me.lblImageFilterPosterMatchRate.Enabled = False
        Me.lblImageFilterPosterMatchRate.Location = New System.Drawing.Point(123, 151)
        Me.lblImageFilterPosterMatchRate.Name = "lblImageFilterPosterMatchRate"
        Me.lblImageFilterPosterMatchRate.Size = New System.Drawing.Size(145, 13)
        Me.lblImageFilterPosterMatchRate.TabIndex = 14
        Me.lblImageFilterPosterMatchRate.Text = "Poster Mismatch Tolerance:"
        '
        'chkImageFilterPoster
        '
        Me.chkImageFilterPoster.AutoSize = True
        Me.chkImageFilterPoster.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkImageFilterPoster.Enabled = False
        Me.chkImageFilterPoster.Location = New System.Drawing.Point(23, 147)
        Me.chkImageFilterPoster.Name = "chkImageFilterPoster"
        Me.chkImageFilterPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkImageFilterPoster.TabIndex = 17
        Me.chkImageFilterPoster.Text = "Poster"
        Me.chkImageFilterPoster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkImageFilterPoster.UseVisualStyleBackColor = True
        '
        'txtImageFilterFanartMatchRate
        '
        Me.txtImageFilterFanartMatchRate.Enabled = False
        Me.txtImageFilterFanartMatchRate.Location = New System.Drawing.Point(275, 175)
        Me.txtImageFilterFanartMatchRate.Name = "txtImageFilterFanartMatchRate"
        Me.txtImageFilterFanartMatchRate.Size = New System.Drawing.Size(44, 22)
        Me.txtImageFilterFanartMatchRate.TabIndex = 15
        '
        'lblImageFilterFanartMatchRate
        '
        Me.lblImageFilterFanartMatchRate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblImageFilterFanartMatchRate.AutoSize = True
        Me.lblImageFilterFanartMatchRate.Enabled = False
        Me.lblImageFilterFanartMatchRate.Location = New System.Drawing.Point(123, 179)
        Me.lblImageFilterFanartMatchRate.Name = "lblImageFilterFanartMatchRate"
        Me.lblImageFilterFanartMatchRate.Size = New System.Drawing.Size(146, 13)
        Me.lblImageFilterFanartMatchRate.TabIndex = 16
        Me.lblImageFilterFanartMatchRate.Text = "Fanart Mismatch Tolerance:"
        '
        'chkImageFilterFanart
        '
        Me.chkImageFilterFanart.AutoSize = True
        Me.chkImageFilterFanart.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkImageFilterFanart.Enabled = False
        Me.chkImageFilterFanart.Location = New System.Drawing.Point(23, 175)
        Me.chkImageFilterFanart.Name = "chkImageFilterFanart"
        Me.chkImageFilterFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkImageFilterFanart.TabIndex = 18
        Me.chkImageFilterFanart.Text = "Fanart"
        Me.chkImageFilterFanart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkImageFilterFanart.UseVisualStyleBackColor = True
        '
        'chkShowNews
        '
        Me.chkShowNews.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkShowNews.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkShowNews, 4)
        Me.chkShowNews.Location = New System.Drawing.Point(3, 26)
        Me.chkShowNews.Name = "chkShowNews"
        Me.chkShowNews.Size = New System.Drawing.Size(227, 17)
        Me.chkShowNews.TabIndex = 0
        Me.chkShowNews.Text = "Show News and Information after Start"
        Me.chkShowNews.UseVisualStyleBackColor = True
        '
        'frmOption_Global
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 266)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_Global"
        Me.Text = "frmOption_Global"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbSortTokens.ResumeLayout(False)
        Me.gbSortTokens.PerformLayout()
        Me.tblSortTokens.ResumeLayout(False)
        CType(Me.dgvSortTokens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMiscellaneous.ResumeLayout(False)
        Me.gbMiscellaneous.PerformLayout()
        Me.tblGeneralMisc.ResumeLayout(False)
        Me.tblGeneralMisc.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMiscellaneous As GroupBox
    Friend WithEvents tblGeneralMisc As TableLayoutPanel
    Friend WithEvents chkImageFilterAutoscraper As CheckBox
    Friend WithEvents chklImageFilterImageDialog As CheckBox
    Friend WithEvents chkImageFilter As CheckBox
    Friend WithEvents chkCheckForUpdates As CheckBox
    Friend WithEvents chkDigitGrpSymbolVotes As CheckBox
    Friend WithEvents btnDigitGrpSymbolSettings As Button
    Friend WithEvents txtImageFilterPosterMatchRate As TextBox
    Friend WithEvents lblImageFilterPosterMatchRate As Label
    Friend WithEvents chkImageFilterPoster As CheckBox
    Friend WithEvents txtImageFilterFanartMatchRate As TextBox
    Friend WithEvents lblImageFilterFanartMatchRate As Label
    Friend WithEvents chkImageFilterFanart As CheckBox
    Friend WithEvents chkShowNews As CheckBox
    Friend WithEvents gbSortTokens As GroupBox
    Friend WithEvents tblSortTokens As TableLayoutPanel
    Friend WithEvents dgvSortTokens As DataGridView
    Friend WithEvents colSortTokens As DataGridViewTextBoxColumn
    Friend WithEvents btnSortTokensDefaults As Button
End Class
