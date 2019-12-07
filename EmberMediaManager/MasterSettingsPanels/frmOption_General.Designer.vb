<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOption_General
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_General))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSortTokens = New System.Windows.Forms.GroupBox()
        Me.tblSortTokens = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvSortTokens = New System.Windows.Forms.DataGridView()
        Me.colSortTokens = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSortTokensDefaults = New System.Windows.Forms.Button()
        Me.gbInterface = New System.Windows.Forms.GroupBox()
        Me.tblInterface = New System.Windows.Forms.TableLayoutPanel()
        Me.lblInterfaceLanguage = New System.Windows.Forms.Label()
        Me.cbInterfaceLanguage = New System.Windows.Forms.ComboBox()
        Me.lblTheme = New System.Windows.Forms.Label()
        Me.cbTheme = New System.Windows.Forms.ComboBox()
        Me.txtThemeHints = New System.Windows.Forms.TextBox()
        Me.gbMainWindow = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkDisplayImageDimension = New System.Windows.Forms.CheckBox()
        Me.chkDisplayFanartSmall = New System.Windows.Forms.CheckBox()
        Me.chkDisplayDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayImageNames = New System.Windows.Forms.CheckBox()
        Me.chkDisplayFanart = New System.Windows.Forms.CheckBox()
        Me.chkDisplayClearArt = New System.Windows.Forms.CheckBox()
        Me.chkDoubleClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkDisplayClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkDisplayBanner = New System.Windows.Forms.CheckBox()
        Me.chkImagesGlassOverlay = New System.Windows.Forms.CheckBox()
        Me.chkDisplayCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayGenresText = New System.Windows.Forms.CheckBox()
        Me.chkDisplayLangFlags = New System.Windows.Forms.CheckBox()
        Me.chkDisplayPoster = New System.Windows.Forms.CheckBox()
        Me.chkDisplayLandscape = New System.Windows.Forms.CheckBox()
        Me.chkDisplayKeyArt = New System.Windows.Forms.CheckBox()
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
        Me.chkDisplayStudioText = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbSortTokens.SuspendLayout()
        Me.tblSortTokens.SuspendLayout()
        CType(Me.dgvSortTokens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbInterface.SuspendLayout()
        Me.tblInterface.SuspendLayout()
        Me.gbMainWindow.SuspendLayout()
        Me.tblGeneralMainWindow.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(722, 594)
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
        Me.tblSettings.Controls.Add(Me.gbSortTokens, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbInterface, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbMainWindow, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbMiscellaneous, 1, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Size = New System.Drawing.Size(722, 594)
        Me.tblSettings.TabIndex = 17
        '
        'gbSortTokens
        '
        Me.gbSortTokens.AutoSize = True
        Me.gbSortTokens.Controls.Add(Me.tblSortTokens)
        Me.gbSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSortTokens.Location = New System.Drawing.Point(3, 256)
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
        'gbInterface
        '
        Me.gbInterface.AutoSize = True
        Me.gbInterface.Controls.Add(Me.tblInterface)
        Me.gbInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbInterface.Location = New System.Drawing.Point(3, 3)
        Me.gbInterface.Name = "gbInterface"
        Me.gbInterface.Size = New System.Drawing.Size(228, 247)
        Me.gbInterface.TabIndex = 0
        Me.gbInterface.TabStop = False
        Me.gbInterface.Text = "Interface"
        '
        'tblInterface
        '
        Me.tblInterface.AutoSize = True
        Me.tblInterface.ColumnCount = 2
        Me.tblInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblInterface.Controls.Add(Me.lblInterfaceLanguage, 0, 0)
        Me.tblInterface.Controls.Add(Me.cbInterfaceLanguage, 0, 1)
        Me.tblInterface.Controls.Add(Me.lblTheme, 0, 2)
        Me.tblInterface.Controls.Add(Me.cbTheme, 0, 3)
        Me.tblInterface.Controls.Add(Me.txtThemeHints, 0, 4)
        Me.tblInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblInterface.Location = New System.Drawing.Point(3, 18)
        Me.tblInterface.Name = "tblInterface"
        Me.tblInterface.RowCount = 5
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblInterface.Size = New System.Drawing.Size(222, 226)
        Me.tblInterface.TabIndex = 17
        '
        'lblInterfaceLanguage
        '
        Me.lblInterfaceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInterfaceLanguage.AutoSize = True
        Me.lblInterfaceLanguage.Location = New System.Drawing.Point(3, 3)
        Me.lblInterfaceLanguage.Name = "lblInterfaceLanguage"
        Me.lblInterfaceLanguage.Size = New System.Drawing.Size(109, 13)
        Me.lblInterfaceLanguage.TabIndex = 0
        Me.lblInterfaceLanguage.Text = "Interface Language:"
        '
        'cbInterfaceLanguage
        '
        Me.cbInterfaceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbInterfaceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInterfaceLanguage.FormattingEnabled = True
        Me.cbInterfaceLanguage.Location = New System.Drawing.Point(3, 23)
        Me.cbInterfaceLanguage.Name = "cbInterfaceLanguage"
        Me.cbInterfaceLanguage.Size = New System.Drawing.Size(216, 21)
        Me.cbInterfaceLanguage.TabIndex = 1
        '
        'lblTheme
        '
        Me.lblTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTheme.AutoSize = True
        Me.lblTheme.Location = New System.Drawing.Point(3, 50)
        Me.lblTheme.Name = "lblTheme"
        Me.lblTheme.Size = New System.Drawing.Size(43, 13)
        Me.lblTheme.TabIndex = 0
        Me.lblTheme.Text = "Theme:"
        '
        'cbTheme
        '
        Me.cbTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTheme.FormattingEnabled = True
        Me.cbTheme.Location = New System.Drawing.Point(3, 70)
        Me.cbTheme.Name = "cbTheme"
        Me.cbTheme.Size = New System.Drawing.Size(216, 21)
        Me.cbTheme.TabIndex = 1
        '
        'txtThemeHints
        '
        Me.txtThemeHints.BackColor = System.Drawing.Color.White
        Me.txtThemeHints.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtThemeHints.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtThemeHints.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtThemeHints.Enabled = False
        Me.txtThemeHints.Location = New System.Drawing.Point(3, 97)
        Me.txtThemeHints.Multiline = True
        Me.txtThemeHints.Name = "txtThemeHints"
        Me.txtThemeHints.ReadOnly = True
        Me.txtThemeHints.Size = New System.Drawing.Size(216, 126)
        Me.txtThemeHints.TabIndex = 2
        Me.txtThemeHints.Text = resources.GetString("txtThemeHints.Text")
        '
        'gbMainWindow
        '
        Me.gbMainWindow.AutoSize = True
        Me.gbMainWindow.Controls.Add(Me.tblGeneralMainWindow)
        Me.gbMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainWindow.Location = New System.Drawing.Point(237, 3)
        Me.gbMainWindow.Name = "gbMainWindow"
        Me.gbMainWindow.Size = New System.Drawing.Size(359, 247)
        Me.gbMainWindow.TabIndex = 14
        Me.gbMainWindow.TabStop = False
        Me.gbMainWindow.Text = "Main Window"
        '
        'tblGeneralMainWindow
        '
        Me.tblGeneralMainWindow.AutoSize = True
        Me.tblGeneralMainWindow.ColumnCount = 3
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayImageDimension, 0, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayFanartSmall, 1, 5)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayDiscArt, 0, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayImageNames, 1, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayFanart, 1, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayClearArt, 0, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDoubleClickScrape, 0, 0)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayClearLogo, 0, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayBanner, 0, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayCharacterArt, 0, 5)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayGenresText, 0, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayPoster, 1, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayLandscape, 1, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayKeyArt, 1, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayLangFlags, 1, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkImagesGlassOverlay, 0, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayStudioText, 1, 1)
        Me.tblGeneralMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMainWindow.Name = "tblGeneralMainWindow"
        Me.tblGeneralMainWindow.RowCount = 10
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.Size = New System.Drawing.Size(353, 226)
        Me.tblGeneralMainWindow.TabIndex = 17
        '
        'chkDisplayImageDimension
        '
        Me.chkDisplayImageDimension.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayImageDimension.AutoSize = True
        Me.chkDisplayImageDimension.Location = New System.Drawing.Point(3, 49)
        Me.chkDisplayImageDimension.Name = "chkDisplayImageDimension"
        Me.chkDisplayImageDimension.Size = New System.Drawing.Size(160, 17)
        Me.chkDisplayImageDimension.TabIndex = 8
        Me.chkDisplayImageDimension.Text = "Display Image Dimensions"
        Me.chkDisplayImageDimension.UseVisualStyleBackColor = True
        '
        'chkDisplayFanartSmall
        '
        Me.chkDisplayFanartSmall.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayFanartSmall.AutoSize = True
        Me.chkDisplayFanartSmall.Location = New System.Drawing.Point(180, 118)
        Me.chkDisplayFanartSmall.Name = "chkDisplayFanartSmall"
        Me.chkDisplayFanartSmall.Size = New System.Drawing.Size(129, 17)
        Me.chkDisplayFanartSmall.TabIndex = 11
        Me.chkDisplayFanartSmall.Text = "Display Small Fanart"
        Me.chkDisplayFanartSmall.UseVisualStyleBackColor = True
        '
        'chkDisplayDiscArt
        '
        Me.chkDisplayDiscArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayDiscArt.AutoSize = True
        Me.chkDisplayDiscArt.Location = New System.Drawing.Point(3, 187)
        Me.chkDisplayDiscArt.Name = "chkDisplayDiscArt"
        Me.chkDisplayDiscArt.Size = New System.Drawing.Size(102, 17)
        Me.chkDisplayDiscArt.TabIndex = 17
        Me.chkDisplayDiscArt.Text = "Display DiscArt"
        Me.chkDisplayDiscArt.UseVisualStyleBackColor = True
        '
        'chkDisplayImageNames
        '
        Me.chkDisplayImageNames.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayImageNames.AutoSize = True
        Me.chkDisplayImageNames.Location = New System.Drawing.Point(180, 49)
        Me.chkDisplayImageNames.Name = "chkDisplayImageNames"
        Me.chkDisplayImageNames.Size = New System.Drawing.Size(134, 17)
        Me.chkDisplayImageNames.TabIndex = 20
        Me.chkDisplayImageNames.Text = "Display Image Names"
        Me.chkDisplayImageNames.UseVisualStyleBackColor = True
        '
        'chkDisplayFanart
        '
        Me.chkDisplayFanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayFanart.AutoSize = True
        Me.chkDisplayFanart.Location = New System.Drawing.Point(180, 95)
        Me.chkDisplayFanart.Name = "chkDisplayFanart"
        Me.chkDisplayFanart.Size = New System.Drawing.Size(99, 17)
        Me.chkDisplayFanart.TabIndex = 7
        Me.chkDisplayFanart.Text = "Display Fanart"
        Me.chkDisplayFanart.UseVisualStyleBackColor = True
        '
        'chkDisplayClearArt
        '
        Me.chkDisplayClearArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayClearArt.AutoSize = True
        Me.chkDisplayClearArt.Location = New System.Drawing.Point(3, 141)
        Me.chkDisplayClearArt.Name = "chkDisplayClearArt"
        Me.chkDisplayClearArt.Size = New System.Drawing.Size(107, 17)
        Me.chkDisplayClearArt.TabIndex = 14
        Me.chkDisplayClearArt.Text = "Display ClearArt"
        Me.chkDisplayClearArt.UseVisualStyleBackColor = True
        '
        'chkDoubleClickScrape
        '
        Me.chkDoubleClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDoubleClickScrape.AutoSize = True
        Me.tblGeneralMainWindow.SetColumnSpan(Me.chkDoubleClickScrape, 2)
        Me.chkDoubleClickScrape.Location = New System.Drawing.Point(3, 3)
        Me.chkDoubleClickScrape.Name = "chkDoubleClickScrape"
        Me.chkDoubleClickScrape.Size = New System.Drawing.Size(250, 17)
        Me.chkDoubleClickScrape.TabIndex = 19
        Me.chkDoubleClickScrape.Text = "Enable Image Scrape On Double Right Click"
        Me.chkDoubleClickScrape.UseVisualStyleBackColor = True
        '
        'chkDisplayClearLogo
        '
        Me.chkDisplayClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayClearLogo.AutoSize = True
        Me.chkDisplayClearLogo.Location = New System.Drawing.Point(3, 164)
        Me.chkDisplayClearLogo.Name = "chkDisplayClearLogo"
        Me.chkDisplayClearLogo.Size = New System.Drawing.Size(118, 17)
        Me.chkDisplayClearLogo.TabIndex = 16
        Me.chkDisplayClearLogo.Text = "Display ClearLogo"
        Me.chkDisplayClearLogo.UseVisualStyleBackColor = True
        '
        'chkDisplayBanner
        '
        Me.chkDisplayBanner.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayBanner.AutoSize = True
        Me.chkDisplayBanner.Location = New System.Drawing.Point(3, 95)
        Me.chkDisplayBanner.Name = "chkDisplayBanner"
        Me.chkDisplayBanner.Size = New System.Drawing.Size(103, 17)
        Me.chkDisplayBanner.TabIndex = 13
        Me.chkDisplayBanner.Text = "Display Banner"
        Me.chkDisplayBanner.UseVisualStyleBackColor = True
        '
        'chkImagesGlassOverlay
        '
        Me.chkImagesGlassOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkImagesGlassOverlay.AutoSize = True
        Me.chkImagesGlassOverlay.Location = New System.Drawing.Point(3, 72)
        Me.chkImagesGlassOverlay.Name = "chkImagesGlassOverlay"
        Me.chkImagesGlassOverlay.Size = New System.Drawing.Size(171, 17)
        Me.chkImagesGlassOverlay.TabIndex = 12
        Me.chkImagesGlassOverlay.Text = "Enable Images Glass Overlay"
        Me.chkImagesGlassOverlay.UseVisualStyleBackColor = True
        '
        'chkDisplayCharacterArt
        '
        Me.chkDisplayCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayCharacterArt.AutoSize = True
        Me.chkDisplayCharacterArt.Location = New System.Drawing.Point(3, 118)
        Me.chkDisplayCharacterArt.Name = "chkDisplayCharacterArt"
        Me.chkDisplayCharacterArt.Size = New System.Drawing.Size(130, 17)
        Me.chkDisplayCharacterArt.TabIndex = 15
        Me.chkDisplayCharacterArt.Text = "Display CharacterArt"
        Me.chkDisplayCharacterArt.UseVisualStyleBackColor = True
        '
        'chkDisplayGenresText
        '
        Me.chkDisplayGenresText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayGenresText.AutoSize = True
        Me.chkDisplayGenresText.Location = New System.Drawing.Point(3, 26)
        Me.chkDisplayGenresText.Name = "chkDisplayGenresText"
        Me.chkDisplayGenresText.Size = New System.Drawing.Size(165, 17)
        Me.chkDisplayGenresText.TabIndex = 9
        Me.chkDisplayGenresText.Text = "Allways Display Genres Text"
        Me.chkDisplayGenresText.UseVisualStyleBackColor = True
        '
        'chkDisplayLangFlags
        '
        Me.chkDisplayLangFlags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayLangFlags.AutoSize = True
        Me.chkDisplayLangFlags.Location = New System.Drawing.Point(180, 72)
        Me.chkDisplayLangFlags.Name = "chkDisplayLangFlags"
        Me.chkDisplayLangFlags.Size = New System.Drawing.Size(147, 17)
        Me.chkDisplayLangFlags.TabIndex = 8
        Me.chkDisplayLangFlags.Text = "Display Language Flags"
        Me.chkDisplayLangFlags.UseVisualStyleBackColor = True
        '
        'chkDisplayPoster
        '
        Me.chkDisplayPoster.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayPoster.AutoSize = True
        Me.chkDisplayPoster.Location = New System.Drawing.Point(180, 187)
        Me.chkDisplayPoster.Name = "chkDisplayPoster"
        Me.chkDisplayPoster.Size = New System.Drawing.Size(98, 17)
        Me.chkDisplayPoster.TabIndex = 6
        Me.chkDisplayPoster.Text = "Display Poster"
        Me.chkDisplayPoster.UseVisualStyleBackColor = True
        '
        'chkDisplayLandscape
        '
        Me.chkDisplayLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayLandscape.AutoSize = True
        Me.chkDisplayLandscape.Location = New System.Drawing.Point(180, 164)
        Me.chkDisplayLandscape.Name = "chkDisplayLandscape"
        Me.chkDisplayLandscape.Size = New System.Drawing.Size(120, 17)
        Me.chkDisplayLandscape.TabIndex = 18
        Me.chkDisplayLandscape.Text = "Display Landscape"
        Me.chkDisplayLandscape.UseVisualStyleBackColor = True
        '
        'chkDisplayKeyArt
        '
        Me.chkDisplayKeyArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayKeyArt.AutoSize = True
        Me.chkDisplayKeyArt.Location = New System.Drawing.Point(180, 141)
        Me.chkDisplayKeyArt.Name = "chkDisplayKeyArt"
        Me.chkDisplayKeyArt.Size = New System.Drawing.Size(98, 17)
        Me.chkDisplayKeyArt.TabIndex = 18
        Me.chkDisplayKeyArt.Text = "Display KeyArt"
        Me.chkDisplayKeyArt.UseVisualStyleBackColor = True
        '
        'gbMiscellaneous
        '
        Me.gbMiscellaneous.AutoSize = True
        Me.gbMiscellaneous.Controls.Add(Me.tblGeneralMisc)
        Me.gbMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMiscellaneous.Location = New System.Drawing.Point(237, 256)
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
        'chkDisplayStudioText
        '
        Me.chkDisplayStudioText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayStudioText.AutoSize = True
        Me.chkDisplayStudioText.Location = New System.Drawing.Point(180, 26)
        Me.chkDisplayStudioText.Name = "chkDisplayStudioText"
        Me.chkDisplayStudioText.Size = New System.Drawing.Size(163, 17)
        Me.chkDisplayStudioText.TabIndex = 12
        Me.chkDisplayStudioText.Text = "Allways Display Studio Text"
        Me.chkDisplayStudioText.UseVisualStyleBackColor = True
        '
        'frmOption_General
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 594)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_General"
        Me.Text = "frmOption_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbSortTokens.ResumeLayout(False)
        Me.gbSortTokens.PerformLayout()
        Me.tblSortTokens.ResumeLayout(False)
        CType(Me.dgvSortTokens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbInterface.ResumeLayout(False)
        Me.gbInterface.PerformLayout()
        Me.tblInterface.ResumeLayout(False)
        Me.tblInterface.PerformLayout()
        Me.gbMainWindow.ResumeLayout(False)
        Me.gbMainWindow.PerformLayout()
        Me.tblGeneralMainWindow.ResumeLayout(False)
        Me.tblGeneralMainWindow.PerformLayout()
        Me.gbMiscellaneous.ResumeLayout(False)
        Me.gbMiscellaneous.PerformLayout()
        Me.tblGeneralMisc.ResumeLayout(False)
        Me.tblGeneralMisc.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbInterface As GroupBox
    Friend WithEvents tblInterface As TableLayoutPanel
    Friend WithEvents lblInterfaceLanguage As Label
    Friend WithEvents cbInterfaceLanguage As ComboBox
    Friend WithEvents lblTheme As Label
    Friend WithEvents cbTheme As ComboBox
    Friend WithEvents gbMainWindow As GroupBox
    Friend WithEvents tblGeneralMainWindow As TableLayoutPanel
    Friend WithEvents chkDisplayImageDimension As CheckBox
    Friend WithEvents chkDisplayFanartSmall As CheckBox
    Friend WithEvents chkDisplayDiscArt As CheckBox
    Friend WithEvents chkDisplayImageNames As CheckBox
    Friend WithEvents chkDisplayFanart As CheckBox
    Friend WithEvents chkDisplayClearArt As CheckBox
    Friend WithEvents chkDoubleClickScrape As CheckBox
    Friend WithEvents chkDisplayClearLogo As CheckBox
    Friend WithEvents chkDisplayBanner As CheckBox
    Friend WithEvents chkImagesGlassOverlay As CheckBox
    Friend WithEvents chkDisplayCharacterArt As CheckBox
    Friend WithEvents chkDisplayGenresText As CheckBox
    Friend WithEvents chkDisplayLangFlags As CheckBox
    Friend WithEvents chkDisplayPoster As CheckBox
    Friend WithEvents chkDisplayLandscape As CheckBox
    Friend WithEvents chkDisplayKeyArt As CheckBox
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
    Friend WithEvents txtThemeHints As TextBox
    Friend WithEvents chkShowNews As CheckBox
    Friend WithEvents gbSortTokens As GroupBox
    Friend WithEvents tblSortTokens As TableLayoutPanel
    Friend WithEvents dgvSortTokens As DataGridView
    Friend WithEvents colSortTokens As DataGridViewTextBoxColumn
    Friend WithEvents btnSortTokensDefaults As Button
    Friend WithEvents chkDisplayStudioText As CheckBox
End Class
