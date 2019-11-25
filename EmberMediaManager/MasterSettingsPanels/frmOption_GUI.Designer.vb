<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOption_GUI
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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralInterface = New System.Windows.Forms.GroupBox()
        Me.tblGeneralInterface = New System.Windows.Forms.TableLayoutPanel()
        Me.lblGeneralIntLang = New System.Windows.Forms.Label()
        Me.cbGeneralLanguage = New System.Windows.Forms.ComboBox()
        Me.lblGeneralTheme = New System.Windows.Forms.Label()
        Me.cbGeneralTheme = New System.Windows.Forms.ComboBox()
        Me.gbGeneralDateAdded = New System.Windows.Forms.GroupBox()
        Me.tblGeneralDateAdded = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.cbGeneralDateTime = New System.Windows.Forms.ComboBox()
        Me.gbGeneralMainWindowOpts = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralDisplayImgDims = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayFanartSmall = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayImgNames = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayFanart = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayClearArt = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDoubleClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayBanner = New System.Windows.Forms.CheckBox()
        Me.chkGeneralImagesGlassOverlay = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayGenresText = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayLangFlags = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayPoster = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayLandscape = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDisplayKeyArt = New System.Windows.Forms.CheckBox()
        Me.gbGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMisc = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralImageFilterAutoscraper = New System.Windows.Forms.CheckBox()
        Me.chkGeneralImageFilterImagedialog = New System.Windows.Forms.CheckBox()
        Me.chkGeneralImageFilter = New System.Windows.Forms.CheckBox()
        Me.chkGeneralSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkGeneralCheckUpdates = New System.Windows.Forms.CheckBox()
        Me.lblGeneralOverwriteNfo = New System.Windows.Forms.Label()
        Me.chkGeneralOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.chkGeneralDigitGrpSymbolVotes = New System.Windows.Forms.CheckBox()
        Me.btnGeneralDigitGrpSymbolSettings = New System.Windows.Forms.Button()
        Me.txtGeneralImageFilterPosterMatchRate = New System.Windows.Forms.TextBox()
        Me.lblGeneralImageFilterPosterMatchRate = New System.Windows.Forms.Label()
        Me.chkGeneralImageFilterPoster = New System.Windows.Forms.CheckBox()
        Me.txtGeneralImageFilterFanartMatchRate = New System.Windows.Forms.TextBox()
        Me.lblGeneralImageFilterFanartMatchRate = New System.Windows.Forms.Label()
        Me.chkGeneralImageFilterFanart = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbGeneralInterface.SuspendLayout()
        Me.tblGeneralInterface.SuspendLayout()
        Me.gbGeneralDateAdded.SuspendLayout()
        Me.tblGeneralDateAdded.SuspendLayout()
        Me.gbGeneralMainWindowOpts.SuspendLayout()
        Me.tblGeneralMainWindow.SuspendLayout()
        Me.gbGeneralMiscOpts.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(800, 600)
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
        Me.tblSettings.Controls.Add(Me.gbGeneralInterface, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbGeneralDateAdded, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbGeneralMainWindowOpts, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbGeneralMiscOpts, 1, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 5
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 600)
        Me.tblSettings.TabIndex = 17
        '
        'gbGeneralInterface
        '
        Me.gbGeneralInterface.AutoSize = True
        Me.gbGeneralInterface.Controls.Add(Me.tblGeneralInterface)
        Me.gbGeneralInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralInterface.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbGeneralInterface.Location = New System.Drawing.Point(3, 3)
        Me.gbGeneralInterface.Name = "gbGeneralInterface"
        Me.gbGeneralInterface.Size = New System.Drawing.Size(238, 228)
        Me.gbGeneralInterface.TabIndex = 0
        Me.gbGeneralInterface.TabStop = False
        Me.gbGeneralInterface.Text = "Interface"
        '
        'tblGeneralInterface
        '
        Me.tblGeneralInterface.AutoSize = True
        Me.tblGeneralInterface.ColumnCount = 2
        Me.tblGeneralInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralInterface.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralInterface.Controls.Add(Me.lblGeneralIntLang, 0, 0)
        Me.tblGeneralInterface.Controls.Add(Me.cbGeneralLanguage, 0, 1)
        Me.tblGeneralInterface.Controls.Add(Me.lblGeneralTheme, 0, 2)
        Me.tblGeneralInterface.Controls.Add(Me.cbGeneralTheme, 0, 3)
        Me.tblGeneralInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralInterface.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralInterface.Name = "tblGeneralInterface"
        Me.tblGeneralInterface.RowCount = 5
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralInterface.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralInterface.Size = New System.Drawing.Size(232, 207)
        Me.tblGeneralInterface.TabIndex = 17
        '
        'lblGeneralIntLang
        '
        Me.lblGeneralIntLang.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralIntLang.AutoSize = True
        Me.lblGeneralIntLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralIntLang.Location = New System.Drawing.Point(3, 3)
        Me.lblGeneralIntLang.Name = "lblGeneralIntLang"
        Me.lblGeneralIntLang.Size = New System.Drawing.Size(109, 13)
        Me.lblGeneralIntLang.TabIndex = 0
        Me.lblGeneralIntLang.Text = "Interface Language:"
        '
        'cbGeneralLanguage
        '
        Me.cbGeneralLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbGeneralLanguage.FormattingEnabled = True
        Me.cbGeneralLanguage.Location = New System.Drawing.Point(3, 23)
        Me.cbGeneralLanguage.Name = "cbGeneralLanguage"
        Me.cbGeneralLanguage.Size = New System.Drawing.Size(216, 21)
        Me.cbGeneralLanguage.TabIndex = 1
        '
        'lblGeneralTheme
        '
        Me.lblGeneralTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralTheme.AutoSize = True
        Me.lblGeneralTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralTheme.Location = New System.Drawing.Point(3, 50)
        Me.lblGeneralTheme.Name = "lblGeneralTheme"
        Me.lblGeneralTheme.Size = New System.Drawing.Size(43, 13)
        Me.lblGeneralTheme.TabIndex = 0
        Me.lblGeneralTheme.Text = "Theme:"
        '
        'cbGeneralTheme
        '
        Me.cbGeneralTheme.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbGeneralTheme.FormattingEnabled = True
        Me.cbGeneralTheme.Location = New System.Drawing.Point(3, 70)
        Me.cbGeneralTheme.Name = "cbGeneralTheme"
        Me.cbGeneralTheme.Size = New System.Drawing.Size(216, 21)
        Me.cbGeneralTheme.TabIndex = 1
        '
        'gbGeneralDateAdded
        '
        Me.gbGeneralDateAdded.AutoSize = True
        Me.gbGeneralDateAdded.Controls.Add(Me.tblGeneralDateAdded)
        Me.gbGeneralDateAdded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralDateAdded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGeneralDateAdded.Location = New System.Drawing.Point(3, 237)
        Me.gbGeneralDateAdded.Name = "gbGeneralDateAdded"
        Me.gbGeneralDateAdded.Size = New System.Drawing.Size(238, 71)
        Me.gbGeneralDateAdded.TabIndex = 16
        Me.gbGeneralDateAdded.TabStop = False
        Me.gbGeneralDateAdded.Text = "Adding Date"
        '
        'tblGeneralDateAdded
        '
        Me.tblGeneralDateAdded.AutoSize = True
        Me.tblGeneralDateAdded.ColumnCount = 2
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.Controls.Add(Me.chkGeneralDateAddedIgnoreNFO, 0, 1)
        Me.tblGeneralDateAdded.Controls.Add(Me.cbGeneralDateTime, 0, 0)
        Me.tblGeneralDateAdded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralDateAdded.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralDateAdded.Name = "tblGeneralDateAdded"
        Me.tblGeneralDateAdded.RowCount = 3
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.Size = New System.Drawing.Size(232, 50)
        Me.tblGeneralDateAdded.TabIndex = 17
        '
        'chkGeneralDateAddedIgnoreNFO
        '
        Me.chkGeneralDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDateAddedIgnoreNFO.AutoSize = True
        Me.chkGeneralDateAddedIgnoreNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 30)
        Me.chkGeneralDateAddedIgnoreNFO.Name = "chkGeneralDateAddedIgnoreNFO"
        Me.chkGeneralDateAddedIgnoreNFO.Size = New System.Drawing.Size(188, 17)
        Me.chkGeneralDateAddedIgnoreNFO.TabIndex = 10
        Me.chkGeneralDateAddedIgnoreNFO.Text = "Ignore <dateadded> from NFO"
        Me.chkGeneralDateAddedIgnoreNFO.UseVisualStyleBackColor = True
        '
        'cbGeneralDateTime
        '
        Me.cbGeneralDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralDateTime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGeneralDateTime.FormattingEnabled = True
        Me.cbGeneralDateTime.Location = New System.Drawing.Point(3, 3)
        Me.cbGeneralDateTime.Name = "cbGeneralDateTime"
        Me.cbGeneralDateTime.Size = New System.Drawing.Size(226, 21)
        Me.cbGeneralDateTime.TabIndex = 11
        '
        'gbGeneralMainWindowOpts
        '
        Me.gbGeneralMainWindowOpts.AutoSize = True
        Me.gbGeneralMainWindowOpts.Controls.Add(Me.tblGeneralMainWindow)
        Me.gbGeneralMainWindowOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralMainWindowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGeneralMainWindowOpts.Location = New System.Drawing.Point(247, 3)
        Me.gbGeneralMainWindowOpts.Name = "gbGeneralMainWindowOpts"
        Me.gbGeneralMainWindowOpts.Size = New System.Drawing.Size(367, 228)
        Me.gbGeneralMainWindowOpts.TabIndex = 14
        Me.gbGeneralMainWindowOpts.TabStop = False
        Me.gbGeneralMainWindowOpts.Text = "Main Window"
        '
        'tblGeneralMainWindow
        '
        Me.tblGeneralMainWindow.AutoSize = True
        Me.tblGeneralMainWindow.ColumnCount = 3
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayImgDims, 0, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayFanartSmall, 1, 5)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayDiscArt, 0, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayImgNames, 1, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayFanart, 1, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayClearArt, 0, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDoubleClickScrape, 0, 0)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayClearLogo, 0, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayBanner, 0, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralImagesGlassOverlay, 1, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayCharacterArt, 0, 5)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayGenresText, 0, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayLangFlags, 0, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayPoster, 1, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayLandscape, 1, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkGeneralDisplayKeyArt, 1, 6)
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
        Me.tblGeneralMainWindow.Size = New System.Drawing.Size(361, 207)
        Me.tblGeneralMainWindow.TabIndex = 17
        '
        'chkGeneralDisplayImgDims
        '
        Me.chkGeneralDisplayImgDims.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayImgDims.AutoSize = True
        Me.chkGeneralDisplayImgDims.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayImgDims.Location = New System.Drawing.Point(3, 49)
        Me.chkGeneralDisplayImgDims.Name = "chkGeneralDisplayImgDims"
        Me.chkGeneralDisplayImgDims.Size = New System.Drawing.Size(160, 17)
        Me.chkGeneralDisplayImgDims.TabIndex = 8
        Me.chkGeneralDisplayImgDims.Text = "Display Image Dimensions"
        Me.chkGeneralDisplayImgDims.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayFanartSmall
        '
        Me.chkGeneralDisplayFanartSmall.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayFanartSmall.AutoSize = True
        Me.chkGeneralDisplayFanartSmall.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayFanartSmall.Location = New System.Drawing.Point(174, 118)
        Me.chkGeneralDisplayFanartSmall.Name = "chkGeneralDisplayFanartSmall"
        Me.chkGeneralDisplayFanartSmall.Size = New System.Drawing.Size(129, 17)
        Me.chkGeneralDisplayFanartSmall.TabIndex = 11
        Me.chkGeneralDisplayFanartSmall.Text = "Display Small Fanart"
        Me.chkGeneralDisplayFanartSmall.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayDiscArt
        '
        Me.chkGeneralDisplayDiscArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayDiscArt.AutoSize = True
        Me.chkGeneralDisplayDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayDiscArt.Location = New System.Drawing.Point(3, 187)
        Me.chkGeneralDisplayDiscArt.Name = "chkGeneralDisplayDiscArt"
        Me.chkGeneralDisplayDiscArt.Size = New System.Drawing.Size(102, 17)
        Me.chkGeneralDisplayDiscArt.TabIndex = 17
        Me.chkGeneralDisplayDiscArt.Text = "Display DiscArt"
        Me.chkGeneralDisplayDiscArt.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayImgNames
        '
        Me.chkGeneralDisplayImgNames.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayImgNames.AutoSize = True
        Me.chkGeneralDisplayImgNames.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayImgNames.Location = New System.Drawing.Point(174, 49)
        Me.chkGeneralDisplayImgNames.Name = "chkGeneralDisplayImgNames"
        Me.chkGeneralDisplayImgNames.Size = New System.Drawing.Size(134, 17)
        Me.chkGeneralDisplayImgNames.TabIndex = 20
        Me.chkGeneralDisplayImgNames.Text = "Display Image Names"
        Me.chkGeneralDisplayImgNames.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayFanart
        '
        Me.chkGeneralDisplayFanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayFanart.AutoSize = True
        Me.chkGeneralDisplayFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayFanart.Location = New System.Drawing.Point(174, 95)
        Me.chkGeneralDisplayFanart.Name = "chkGeneralDisplayFanart"
        Me.chkGeneralDisplayFanart.Size = New System.Drawing.Size(99, 17)
        Me.chkGeneralDisplayFanart.TabIndex = 7
        Me.chkGeneralDisplayFanart.Text = "Display Fanart"
        Me.chkGeneralDisplayFanart.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayClearArt
        '
        Me.chkGeneralDisplayClearArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayClearArt.AutoSize = True
        Me.chkGeneralDisplayClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayClearArt.Location = New System.Drawing.Point(3, 141)
        Me.chkGeneralDisplayClearArt.Name = "chkGeneralDisplayClearArt"
        Me.chkGeneralDisplayClearArt.Size = New System.Drawing.Size(107, 17)
        Me.chkGeneralDisplayClearArt.TabIndex = 14
        Me.chkGeneralDisplayClearArt.Text = "Display ClearArt"
        Me.chkGeneralDisplayClearArt.UseVisualStyleBackColor = True
        '
        'chkGeneralDoubleClickScrape
        '
        Me.chkGeneralDoubleClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDoubleClickScrape.AutoSize = True
        Me.tblGeneralMainWindow.SetColumnSpan(Me.chkGeneralDoubleClickScrape, 2)
        Me.chkGeneralDoubleClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDoubleClickScrape.Location = New System.Drawing.Point(3, 3)
        Me.chkGeneralDoubleClickScrape.Name = "chkGeneralDoubleClickScrape"
        Me.chkGeneralDoubleClickScrape.Size = New System.Drawing.Size(250, 17)
        Me.chkGeneralDoubleClickScrape.TabIndex = 19
        Me.chkGeneralDoubleClickScrape.Text = "Enable Image Scrape On Double Right Click"
        Me.chkGeneralDoubleClickScrape.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayClearLogo
        '
        Me.chkGeneralDisplayClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayClearLogo.AutoSize = True
        Me.chkGeneralDisplayClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayClearLogo.Location = New System.Drawing.Point(3, 164)
        Me.chkGeneralDisplayClearLogo.Name = "chkGeneralDisplayClearLogo"
        Me.chkGeneralDisplayClearLogo.Size = New System.Drawing.Size(118, 17)
        Me.chkGeneralDisplayClearLogo.TabIndex = 16
        Me.chkGeneralDisplayClearLogo.Text = "Display ClearLogo"
        Me.chkGeneralDisplayClearLogo.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayBanner
        '
        Me.chkGeneralDisplayBanner.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayBanner.AutoSize = True
        Me.chkGeneralDisplayBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayBanner.Location = New System.Drawing.Point(3, 95)
        Me.chkGeneralDisplayBanner.Name = "chkGeneralDisplayBanner"
        Me.chkGeneralDisplayBanner.Size = New System.Drawing.Size(103, 17)
        Me.chkGeneralDisplayBanner.TabIndex = 13
        Me.chkGeneralDisplayBanner.Text = "Display Banner"
        Me.chkGeneralDisplayBanner.UseVisualStyleBackColor = True
        '
        'chkGeneralImagesGlassOverlay
        '
        Me.chkGeneralImagesGlassOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralImagesGlassOverlay.AutoSize = True
        Me.chkGeneralImagesGlassOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImagesGlassOverlay.Location = New System.Drawing.Point(174, 26)
        Me.chkGeneralImagesGlassOverlay.Name = "chkGeneralImagesGlassOverlay"
        Me.chkGeneralImagesGlassOverlay.Size = New System.Drawing.Size(171, 17)
        Me.chkGeneralImagesGlassOverlay.TabIndex = 12
        Me.chkGeneralImagesGlassOverlay.Text = "Enable Images Glass Overlay"
        Me.chkGeneralImagesGlassOverlay.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayCharacterArt
        '
        Me.chkGeneralDisplayCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayCharacterArt.AutoSize = True
        Me.chkGeneralDisplayCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayCharacterArt.Location = New System.Drawing.Point(3, 118)
        Me.chkGeneralDisplayCharacterArt.Name = "chkGeneralDisplayCharacterArt"
        Me.chkGeneralDisplayCharacterArt.Size = New System.Drawing.Size(130, 17)
        Me.chkGeneralDisplayCharacterArt.TabIndex = 15
        Me.chkGeneralDisplayCharacterArt.Text = "Display CharacterArt"
        Me.chkGeneralDisplayCharacterArt.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayGenresText
        '
        Me.chkGeneralDisplayGenresText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayGenresText.AutoSize = True
        Me.chkGeneralDisplayGenresText.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayGenresText.Location = New System.Drawing.Point(3, 26)
        Me.chkGeneralDisplayGenresText.Name = "chkGeneralDisplayGenresText"
        Me.chkGeneralDisplayGenresText.Size = New System.Drawing.Size(165, 17)
        Me.chkGeneralDisplayGenresText.TabIndex = 9
        Me.chkGeneralDisplayGenresText.Text = "Allways Display Genres Text"
        Me.chkGeneralDisplayGenresText.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayLangFlags
        '
        Me.chkGeneralDisplayLangFlags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayLangFlags.AutoSize = True
        Me.chkGeneralDisplayLangFlags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayLangFlags.Location = New System.Drawing.Point(3, 72)
        Me.chkGeneralDisplayLangFlags.Name = "chkGeneralDisplayLangFlags"
        Me.chkGeneralDisplayLangFlags.Size = New System.Drawing.Size(147, 17)
        Me.chkGeneralDisplayLangFlags.TabIndex = 8
        Me.chkGeneralDisplayLangFlags.Text = "Display Language Flags"
        Me.chkGeneralDisplayLangFlags.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayPoster
        '
        Me.chkGeneralDisplayPoster.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayPoster.AutoSize = True
        Me.chkGeneralDisplayPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayPoster.Location = New System.Drawing.Point(174, 187)
        Me.chkGeneralDisplayPoster.Name = "chkGeneralDisplayPoster"
        Me.chkGeneralDisplayPoster.Size = New System.Drawing.Size(98, 17)
        Me.chkGeneralDisplayPoster.TabIndex = 6
        Me.chkGeneralDisplayPoster.Text = "Display Poster"
        Me.chkGeneralDisplayPoster.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayLandscape
        '
        Me.chkGeneralDisplayLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayLandscape.AutoSize = True
        Me.chkGeneralDisplayLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayLandscape.Location = New System.Drawing.Point(174, 164)
        Me.chkGeneralDisplayLandscape.Name = "chkGeneralDisplayLandscape"
        Me.chkGeneralDisplayLandscape.Size = New System.Drawing.Size(120, 17)
        Me.chkGeneralDisplayLandscape.TabIndex = 18
        Me.chkGeneralDisplayLandscape.Text = "Display Landscape"
        Me.chkGeneralDisplayLandscape.UseVisualStyleBackColor = True
        '
        'chkGeneralDisplayKeyArt
        '
        Me.chkGeneralDisplayKeyArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDisplayKeyArt.AutoSize = True
        Me.chkGeneralDisplayKeyArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDisplayKeyArt.Location = New System.Drawing.Point(174, 141)
        Me.chkGeneralDisplayKeyArt.Name = "chkGeneralDisplayKeyArt"
        Me.chkGeneralDisplayKeyArt.Size = New System.Drawing.Size(98, 17)
        Me.chkGeneralDisplayKeyArt.TabIndex = 18
        Me.chkGeneralDisplayKeyArt.Text = "Display KeyArt"
        Me.chkGeneralDisplayKeyArt.UseVisualStyleBackColor = True
        '
        'gbGeneralMiscOpts
        '
        Me.gbGeneralMiscOpts.AutoSize = True
        Me.gbGeneralMiscOpts.Controls.Add(Me.tblGeneralMisc)
        Me.gbGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbGeneralMiscOpts.Location = New System.Drawing.Point(247, 237)
        Me.gbGeneralMiscOpts.Name = "gbGeneralMiscOpts"
        Me.tblSettings.SetRowSpan(Me.gbGeneralMiscOpts, 2)
        Me.gbGeneralMiscOpts.Size = New System.Drawing.Size(367, 256)
        Me.gbGeneralMiscOpts.TabIndex = 1
        Me.gbGeneralMiscOpts.TabStop = False
        Me.gbGeneralMiscOpts.Text = "Miscellaneous"
        '
        'tblGeneralMisc
        '
        Me.tblGeneralMisc.AutoSize = True
        Me.tblGeneralMisc.ColumnCount = 4
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.tblGeneralMisc.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralImageFilterAutoscraper, 1, 7)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralImageFilterImagedialog, 1, 8)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralImageFilter, 0, 6)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralSourceFromFolder, 0, 3)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralCheckUpdates, 0, 0)
        Me.tblGeneralMisc.Controls.Add(Me.lblGeneralOverwriteNfo, 0, 2)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralOverwriteNfo, 0, 1)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralDigitGrpSymbolVotes, 0, 4)
        Me.tblGeneralMisc.Controls.Add(Me.btnGeneralDigitGrpSymbolSettings, 3, 4)
        Me.tblGeneralMisc.Controls.Add(Me.txtGeneralImageFilterPosterMatchRate, 3, 10)
        Me.tblGeneralMisc.Controls.Add(Me.lblGeneralImageFilterPosterMatchRate, 2, 10)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralImageFilterPoster, 1, 10)
        Me.tblGeneralMisc.Controls.Add(Me.txtGeneralImageFilterFanartMatchRate, 3, 12)
        Me.tblGeneralMisc.Controls.Add(Me.lblGeneralImageFilterFanartMatchRate, 2, 12)
        Me.tblGeneralMisc.Controls.Add(Me.chkGeneralImageFilterFanart, 1, 12)
        Me.tblGeneralMisc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMisc.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMisc.Name = "tblGeneralMisc"
        Me.tblGeneralMisc.RowCount = 13
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
        Me.tblGeneralMisc.Size = New System.Drawing.Size(361, 235)
        Me.tblGeneralMisc.TabIndex = 17
        '
        'chkGeneralImageFilterAutoscraper
        '
        Me.chkGeneralImageFilterAutoscraper.AutoSize = True
        Me.chkGeneralImageFilterAutoscraper.Enabled = False
        Me.chkGeneralImageFilterAutoscraper.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImageFilterAutoscraper.Location = New System.Drawing.Point(23, 136)
        Me.chkGeneralImageFilterAutoscraper.Name = "chkGeneralImageFilterAutoscraper"
        Me.chkGeneralImageFilterAutoscraper.Size = New System.Drawing.Size(88, 17)
        Me.chkGeneralImageFilterAutoscraper.TabIndex = 10
        Me.chkGeneralImageFilterAutoscraper.Text = "Autoscraper"
        Me.chkGeneralImageFilterAutoscraper.UseVisualStyleBackColor = True
        '
        'chkGeneralImageFilterImagedialog
        '
        Me.chkGeneralImageFilterImagedialog.AutoSize = True
        Me.chkGeneralImageFilterImagedialog.Enabled = False
        Me.chkGeneralImageFilterImagedialog.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImageFilterImagedialog.Location = New System.Drawing.Point(23, 159)
        Me.chkGeneralImageFilterImagedialog.Name = "chkGeneralImageFilterImagedialog"
        Me.chkGeneralImageFilterImagedialog.Size = New System.Drawing.Size(90, 17)
        Me.chkGeneralImageFilterImagedialog.TabIndex = 8
        Me.chkGeneralImageFilterImagedialog.Text = "Imagedialog"
        Me.chkGeneralImageFilterImagedialog.UseVisualStyleBackColor = True
        '
        'chkGeneralImageFilter
        '
        Me.chkGeneralImageFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralImageFilter.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkGeneralImageFilter, 4)
        Me.chkGeneralImageFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImageFilter.Location = New System.Drawing.Point(3, 113)
        Me.chkGeneralImageFilter.Name = "chkGeneralImageFilter"
        Me.chkGeneralImageFilter.Size = New System.Drawing.Size(261, 17)
        Me.chkGeneralImageFilter.TabIndex = 8
        Me.chkGeneralImageFilter.Text = "Activate ImageFilter to avoid duplicate images"
        Me.chkGeneralImageFilter.UseVisualStyleBackColor = True
        '
        'chkGeneralSourceFromFolder
        '
        Me.chkGeneralSourceFromFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralSourceFromFolder.AutoSize = True
        Me.chkGeneralSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblGeneralMisc.SetColumnSpan(Me.chkGeneralSourceFromFolder, 4)
        Me.chkGeneralSourceFromFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralSourceFromFolder.Location = New System.Drawing.Point(3, 61)
        Me.chkGeneralSourceFromFolder.Name = "chkGeneralSourceFromFolder"
        Me.chkGeneralSourceFromFolder.Size = New System.Drawing.Size(242, 17)
        Me.chkGeneralSourceFromFolder.TabIndex = 5
        Me.chkGeneralSourceFromFolder.Text = "Include Folder Name in Source Type Check"
        Me.chkGeneralSourceFromFolder.UseVisualStyleBackColor = True
        '
        'chkGeneralCheckUpdates
        '
        Me.chkGeneralCheckUpdates.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralCheckUpdates.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkGeneralCheckUpdates, 4)
        Me.chkGeneralCheckUpdates.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralCheckUpdates.Location = New System.Drawing.Point(3, 3)
        Me.chkGeneralCheckUpdates.Name = "chkGeneralCheckUpdates"
        Me.chkGeneralCheckUpdates.Size = New System.Drawing.Size(121, 17)
        Me.chkGeneralCheckUpdates.TabIndex = 0
        Me.chkGeneralCheckUpdates.Text = "Check for Updates"
        Me.chkGeneralCheckUpdates.UseVisualStyleBackColor = True
        '
        'lblGeneralOverwriteNfo
        '
        Me.lblGeneralOverwriteNfo.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.lblGeneralOverwriteNfo, 4)
        Me.lblGeneralOverwriteNfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralOverwriteNfo.Location = New System.Drawing.Point(3, 46)
        Me.lblGeneralOverwriteNfo.Name = "lblGeneralOverwriteNfo"
        Me.lblGeneralOverwriteNfo.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblGeneralOverwriteNfo.Size = New System.Drawing.Size(314, 12)
        Me.lblGeneralOverwriteNfo.TabIndex = 3
        Me.lblGeneralOverwriteNfo.Text = "(If unchecked, non-conforming nfos will be renamed to <filename>.info)"
        Me.lblGeneralOverwriteNfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkGeneralOverwriteNfo
        '
        Me.chkGeneralOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralOverwriteNfo.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkGeneralOverwriteNfo, 4)
        Me.chkGeneralOverwriteNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralOverwriteNfo.Location = New System.Drawing.Point(3, 26)
        Me.chkGeneralOverwriteNfo.Name = "chkGeneralOverwriteNfo"
        Me.chkGeneralOverwriteNfo.Size = New System.Drawing.Size(191, 17)
        Me.chkGeneralOverwriteNfo.TabIndex = 2
        Me.chkGeneralOverwriteNfo.Text = "Overwrite Non-conforming nfos"
        Me.chkGeneralOverwriteNfo.UseVisualStyleBackColor = True
        '
        'chkGeneralDigitGrpSymbolVotes
        '
        Me.chkGeneralDigitGrpSymbolVotes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDigitGrpSymbolVotes.AutoSize = True
        Me.tblGeneralMisc.SetColumnSpan(Me.chkGeneralDigitGrpSymbolVotes, 3)
        Me.chkGeneralDigitGrpSymbolVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkGeneralDigitGrpSymbolVotes.Location = New System.Drawing.Point(3, 87)
        Me.chkGeneralDigitGrpSymbolVotes.Name = "chkGeneralDigitGrpSymbolVotes"
        Me.chkGeneralDigitGrpSymbolVotes.Size = New System.Drawing.Size(245, 17)
        Me.chkGeneralDigitGrpSymbolVotes.TabIndex = 6
        Me.chkGeneralDigitGrpSymbolVotes.Text = "Use digit grouping symbol for Votes count"
        Me.chkGeneralDigitGrpSymbolVotes.UseVisualStyleBackColor = True
        '
        'btnGeneralDigitGrpSymbolSettings
        '
        Me.btnGeneralDigitGrpSymbolSettings.AutoSize = True
        Me.btnGeneralDigitGrpSymbolSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnGeneralDigitGrpSymbolSettings.Location = New System.Drawing.Point(283, 84)
        Me.btnGeneralDigitGrpSymbolSettings.Name = "btnGeneralDigitGrpSymbolSettings"
        Me.btnGeneralDigitGrpSymbolSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnGeneralDigitGrpSymbolSettings.TabIndex = 7
        Me.btnGeneralDigitGrpSymbolSettings.Text = "Settings"
        Me.btnGeneralDigitGrpSymbolSettings.UseVisualStyleBackColor = True
        '
        'txtGeneralImageFilterPosterMatchRate
        '
        Me.txtGeneralImageFilterPosterMatchRate.Enabled = False
        Me.txtGeneralImageFilterPosterMatchRate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtGeneralImageFilterPosterMatchRate.Location = New System.Drawing.Point(283, 182)
        Me.txtGeneralImageFilterPosterMatchRate.Name = "txtGeneralImageFilterPosterMatchRate"
        Me.txtGeneralImageFilterPosterMatchRate.Size = New System.Drawing.Size(44, 22)
        Me.txtGeneralImageFilterPosterMatchRate.TabIndex = 13
        '
        'lblGeneralImageFilterPosterMatchRate
        '
        Me.lblGeneralImageFilterPosterMatchRate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralImageFilterPosterMatchRate.AutoSize = True
        Me.lblGeneralImageFilterPosterMatchRate.Enabled = False
        Me.lblGeneralImageFilterPosterMatchRate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralImageFilterPosterMatchRate.Location = New System.Drawing.Point(123, 186)
        Me.lblGeneralImageFilterPosterMatchRate.Name = "lblGeneralImageFilterPosterMatchRate"
        Me.lblGeneralImageFilterPosterMatchRate.Size = New System.Drawing.Size(145, 13)
        Me.lblGeneralImageFilterPosterMatchRate.TabIndex = 14
        Me.lblGeneralImageFilterPosterMatchRate.Text = "Poster Mismatch Tolerance:"
        '
        'chkGeneralImageFilterPoster
        '
        Me.chkGeneralImageFilterPoster.AutoSize = True
        Me.chkGeneralImageFilterPoster.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkGeneralImageFilterPoster.Enabled = False
        Me.chkGeneralImageFilterPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImageFilterPoster.Location = New System.Drawing.Point(23, 182)
        Me.chkGeneralImageFilterPoster.Name = "chkGeneralImageFilterPoster"
        Me.chkGeneralImageFilterPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkGeneralImageFilterPoster.TabIndex = 17
        Me.chkGeneralImageFilterPoster.Text = "Poster"
        Me.chkGeneralImageFilterPoster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkGeneralImageFilterPoster.UseVisualStyleBackColor = True
        '
        'txtGeneralImageFilterFanartMatchRate
        '
        Me.txtGeneralImageFilterFanartMatchRate.Enabled = False
        Me.txtGeneralImageFilterFanartMatchRate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtGeneralImageFilterFanartMatchRate.Location = New System.Drawing.Point(283, 210)
        Me.txtGeneralImageFilterFanartMatchRate.Name = "txtGeneralImageFilterFanartMatchRate"
        Me.txtGeneralImageFilterFanartMatchRate.Size = New System.Drawing.Size(44, 22)
        Me.txtGeneralImageFilterFanartMatchRate.TabIndex = 15
        '
        'lblGeneralImageFilterFanartMatchRate
        '
        Me.lblGeneralImageFilterFanartMatchRate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralImageFilterFanartMatchRate.AutoSize = True
        Me.lblGeneralImageFilterFanartMatchRate.Enabled = False
        Me.lblGeneralImageFilterFanartMatchRate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralImageFilterFanartMatchRate.Location = New System.Drawing.Point(123, 214)
        Me.lblGeneralImageFilterFanartMatchRate.Name = "lblGeneralImageFilterFanartMatchRate"
        Me.lblGeneralImageFilterFanartMatchRate.Size = New System.Drawing.Size(146, 13)
        Me.lblGeneralImageFilterFanartMatchRate.TabIndex = 16
        Me.lblGeneralImageFilterFanartMatchRate.Text = "Fanart Mismatch Tolerance:"
        '
        'chkGeneralImageFilterFanart
        '
        Me.chkGeneralImageFilterFanart.AutoSize = True
        Me.chkGeneralImageFilterFanart.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkGeneralImageFilterFanart.Enabled = False
        Me.chkGeneralImageFilterFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralImageFilterFanart.Location = New System.Drawing.Point(23, 210)
        Me.chkGeneralImageFilterFanart.Name = "chkGeneralImageFilterFanart"
        Me.chkGeneralImageFilterFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkGeneralImageFilterFanart.TabIndex = 18
        Me.chkGeneralImageFilterFanart.Text = "Fanart"
        Me.chkGeneralImageFilterFanart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkGeneralImageFilterFanart.UseVisualStyleBackColor = True
        '
        'frmOption_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 600)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmOption_GUI"
        Me.Text = "frmOption_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbGeneralInterface.ResumeLayout(False)
        Me.gbGeneralInterface.PerformLayout()
        Me.tblGeneralInterface.ResumeLayout(False)
        Me.tblGeneralInterface.PerformLayout()
        Me.gbGeneralDateAdded.ResumeLayout(False)
        Me.gbGeneralDateAdded.PerformLayout()
        Me.tblGeneralDateAdded.ResumeLayout(False)
        Me.tblGeneralDateAdded.PerformLayout()
        Me.gbGeneralMainWindowOpts.ResumeLayout(False)
        Me.gbGeneralMainWindowOpts.PerformLayout()
        Me.tblGeneralMainWindow.ResumeLayout(False)
        Me.tblGeneralMainWindow.PerformLayout()
        Me.gbGeneralMiscOpts.ResumeLayout(False)
        Me.gbGeneralMiscOpts.PerformLayout()
        Me.tblGeneralMisc.ResumeLayout(False)
        Me.tblGeneralMisc.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbGeneralInterface As GroupBox
    Friend WithEvents tblGeneralInterface As TableLayoutPanel
    Friend WithEvents lblGeneralIntLang As Label
    Friend WithEvents cbGeneralLanguage As ComboBox
    Friend WithEvents lblGeneralTheme As Label
    Friend WithEvents cbGeneralTheme As ComboBox
    Friend WithEvents gbGeneralDateAdded As GroupBox
    Friend WithEvents tblGeneralDateAdded As TableLayoutPanel
    Friend WithEvents chkGeneralDateAddedIgnoreNFO As CheckBox
    Friend WithEvents cbGeneralDateTime As ComboBox
    Friend WithEvents gbGeneralMainWindowOpts As GroupBox
    Friend WithEvents tblGeneralMainWindow As TableLayoutPanel
    Friend WithEvents chkGeneralDisplayImgDims As CheckBox
    Friend WithEvents chkGeneralDisplayFanartSmall As CheckBox
    Friend WithEvents chkGeneralDisplayDiscArt As CheckBox
    Friend WithEvents chkGeneralDisplayImgNames As CheckBox
    Friend WithEvents chkGeneralDisplayFanart As CheckBox
    Friend WithEvents chkGeneralDisplayClearArt As CheckBox
    Friend WithEvents chkGeneralDoubleClickScrape As CheckBox
    Friend WithEvents chkGeneralDisplayClearLogo As CheckBox
    Friend WithEvents chkGeneralDisplayBanner As CheckBox
    Friend WithEvents chkGeneralImagesGlassOverlay As CheckBox
    Friend WithEvents chkGeneralDisplayCharacterArt As CheckBox
    Friend WithEvents chkGeneralDisplayGenresText As CheckBox
    Friend WithEvents chkGeneralDisplayLangFlags As CheckBox
    Friend WithEvents chkGeneralDisplayPoster As CheckBox
    Friend WithEvents chkGeneralDisplayLandscape As CheckBox
    Friend WithEvents chkGeneralDisplayKeyArt As CheckBox
    Friend WithEvents gbGeneralMiscOpts As GroupBox
    Friend WithEvents tblGeneralMisc As TableLayoutPanel
    Friend WithEvents chkGeneralImageFilterAutoscraper As CheckBox
    Friend WithEvents chkGeneralImageFilterImagedialog As CheckBox
    Friend WithEvents chkGeneralImageFilter As CheckBox
    Friend WithEvents chkGeneralSourceFromFolder As CheckBox
    Friend WithEvents chkGeneralCheckUpdates As CheckBox
    Friend WithEvents lblGeneralOverwriteNfo As Label
    Friend WithEvents chkGeneralOverwriteNfo As CheckBox
    Friend WithEvents chkGeneralDigitGrpSymbolVotes As CheckBox
    Friend WithEvents btnGeneralDigitGrpSymbolSettings As Button
    Friend WithEvents txtGeneralImageFilterPosterMatchRate As TextBox
    Friend WithEvents lblGeneralImageFilterPosterMatchRate As Label
    Friend WithEvents chkGeneralImageFilterPoster As CheckBox
    Friend WithEvents txtGeneralImageFilterFanartMatchRate As TextBox
    Friend WithEvents lblGeneralImageFilterFanartMatchRate As Label
    Friend WithEvents chkGeneralImageFilterFanart As CheckBox
End Class
