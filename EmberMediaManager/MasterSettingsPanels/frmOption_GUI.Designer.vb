<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbDisplayInMainWindow = New System.Windows.Forms.GroupBox()
        Me.tblGeneralMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkDisplayFanart = New System.Windows.Forms.CheckBox()
        Me.chkDisplayDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayFanartAsBackGround = New System.Windows.Forms.CheckBox()
        Me.chkDisplayClearArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkDisplayBanner = New System.Windows.Forms.CheckBox()
        Me.chkDisplayCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayPoster = New System.Windows.Forms.CheckBox()
        Me.chkDisplayLandscape = New System.Windows.Forms.CheckBox()
        Me.chkDisplayKeyArt = New System.Windows.Forms.CheckBox()
        Me.chkDisplayStudioFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayStudioName = New System.Windows.Forms.CheckBox()
        Me.chkDisplayGenreText = New System.Windows.Forms.CheckBox()
        Me.chkDisplayVideoSourceFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayVideoResolutionFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayVideoCodecFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayGenreFlags = New System.Windows.Forms.CheckBox()
        Me.chkDisplayImageDimension = New System.Windows.Forms.CheckBox()
        Me.chkDisplayImageNames = New System.Windows.Forms.CheckBox()
        Me.chkDisplayLanguageFlags = New System.Windows.Forms.CheckBox()
        Me.chkDisplayAudioChannelsFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayAudioCodecFlag = New System.Windows.Forms.CheckBox()
        Me.chkDisplayImagesGlassOverlay = New System.Windows.Forms.CheckBox()
        Me.chkDoubleClickScrape = New System.Windows.Forms.CheckBox()
        Me.gbInterface = New System.Windows.Forms.GroupBox()
        Me.tblInterface = New System.Windows.Forms.TableLayoutPanel()
        Me.lblInterfaceLanguage = New System.Windows.Forms.Label()
        Me.cbInterfaceLanguage = New System.Windows.Forms.ComboBox()
        Me.lblTheme = New System.Windows.Forms.Label()
        Me.cbTheme = New System.Windows.Forms.ComboBox()
        Me.txtThemeHints = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbDisplayInMainWindow.SuspendLayout()
        Me.tblGeneralMainWindow.SuspendLayout()
        Me.gbInterface.SuspendLayout()
        Me.tblInterface.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(765, 455)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 3
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbDisplayInMainWindow, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbInterface, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(765, 455)
        Me.tblSettings.TabIndex = 0
        '
        'gbDisplayInMainWindow
        '
        Me.gbDisplayInMainWindow.AutoSize = True
        Me.gbDisplayInMainWindow.Controls.Add(Me.tblGeneralMainWindow)
        Me.gbDisplayInMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbDisplayInMainWindow.Location = New System.Drawing.Point(237, 3)
        Me.gbDisplayInMainWindow.Name = "gbDisplayInMainWindow"
        Me.gbDisplayInMainWindow.Size = New System.Drawing.Size(441, 317)
        Me.gbDisplayInMainWindow.TabIndex = 15
        Me.gbDisplayInMainWindow.TabStop = False
        Me.gbDisplayInMainWindow.Text = "Display in Main Window"
        '
        'tblGeneralMainWindow
        '
        Me.tblGeneralMainWindow.AutoSize = True
        Me.tblGeneralMainWindow.ColumnCount = 3
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayFanart, 1, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayDiscArt, 0, 12)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayFanartAsBackGround, 1, 9)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayClearArt, 0, 10)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayClearLogo, 0, 11)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayBanner, 0, 8)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayCharacterArt, 0, 9)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayPoster, 1, 12)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayLandscape, 1, 11)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayKeyArt, 1, 10)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayStudioFlag, 0, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayStudioName, 1, 1)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayGenreText, 1, 0)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayVideoSourceFlag, 0, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayVideoResolutionFlag, 1, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayVideoCodecFlag, 0, 3)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayGenreFlags, 0, 0)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayImageDimension, 1, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayImageNames, 0, 7)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayLanguageFlags, 1, 2)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayAudioChannelsFlag, 1, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayAudioCodecFlag, 0, 4)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDisplayImagesGlassOverlay, 1, 6)
        Me.tblGeneralMainWindow.Controls.Add(Me.chkDoubleClickScrape, 0, 6)
        Me.tblGeneralMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralMainWindow.Name = "tblGeneralMainWindow"
        Me.tblGeneralMainWindow.RowCount = 14
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralMainWindow.Size = New System.Drawing.Size(435, 296)
        Me.tblGeneralMainWindow.TabIndex = 17
        '
        'chkDisplayFanart
        '
        Me.chkDisplayFanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayFanart.AutoSize = True
        Me.chkDisplayFanart.Location = New System.Drawing.Point(259, 184)
        Me.chkDisplayFanart.Name = "chkDisplayFanart"
        Me.chkDisplayFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkDisplayFanart.TabIndex = 11
        Me.chkDisplayFanart.Text = "Fanart"
        Me.chkDisplayFanart.UseVisualStyleBackColor = True
        '
        'chkDisplayDiscArt
        '
        Me.chkDisplayDiscArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayDiscArt.AutoSize = True
        Me.chkDisplayDiscArt.Location = New System.Drawing.Point(3, 276)
        Me.chkDisplayDiscArt.Name = "chkDisplayDiscArt"
        Me.chkDisplayDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkDisplayDiscArt.TabIndex = 17
        Me.chkDisplayDiscArt.Text = "DiscArt"
        Me.chkDisplayDiscArt.UseVisualStyleBackColor = True
        '
        'chkDisplayFanartAsBackGround
        '
        Me.chkDisplayFanartAsBackGround.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayFanartAsBackGround.AutoSize = True
        Me.chkDisplayFanartAsBackGround.Location = New System.Drawing.Point(259, 207)
        Me.chkDisplayFanartAsBackGround.Name = "chkDisplayFanartAsBackGround"
        Me.chkDisplayFanartAsBackGround.Size = New System.Drawing.Size(139, 17)
        Me.chkDisplayFanartAsBackGround.TabIndex = 7
        Me.chkDisplayFanartAsBackGround.Text = "Fanart as Background"
        Me.chkDisplayFanartAsBackGround.UseVisualStyleBackColor = True
        '
        'chkDisplayClearArt
        '
        Me.chkDisplayClearArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayClearArt.AutoSize = True
        Me.chkDisplayClearArt.Location = New System.Drawing.Point(3, 230)
        Me.chkDisplayClearArt.Name = "chkDisplayClearArt"
        Me.chkDisplayClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkDisplayClearArt.TabIndex = 14
        Me.chkDisplayClearArt.Text = "ClearArt"
        Me.chkDisplayClearArt.UseVisualStyleBackColor = True
        '
        'chkDisplayClearLogo
        '
        Me.chkDisplayClearLogo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayClearLogo.AutoSize = True
        Me.chkDisplayClearLogo.Location = New System.Drawing.Point(3, 253)
        Me.chkDisplayClearLogo.Name = "chkDisplayClearLogo"
        Me.chkDisplayClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkDisplayClearLogo.TabIndex = 16
        Me.chkDisplayClearLogo.Text = "ClearLogo"
        Me.chkDisplayClearLogo.UseVisualStyleBackColor = True
        '
        'chkDisplayBanner
        '
        Me.chkDisplayBanner.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayBanner.AutoSize = True
        Me.chkDisplayBanner.Location = New System.Drawing.Point(3, 184)
        Me.chkDisplayBanner.Name = "chkDisplayBanner"
        Me.chkDisplayBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkDisplayBanner.TabIndex = 13
        Me.chkDisplayBanner.Text = "Banner"
        Me.chkDisplayBanner.UseVisualStyleBackColor = True
        '
        'chkDisplayCharacterArt
        '
        Me.chkDisplayCharacterArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayCharacterArt.AutoSize = True
        Me.chkDisplayCharacterArt.Location = New System.Drawing.Point(3, 207)
        Me.chkDisplayCharacterArt.Name = "chkDisplayCharacterArt"
        Me.chkDisplayCharacterArt.Size = New System.Drawing.Size(90, 17)
        Me.chkDisplayCharacterArt.TabIndex = 15
        Me.chkDisplayCharacterArt.Text = "CharacterArt"
        Me.chkDisplayCharacterArt.UseVisualStyleBackColor = True
        '
        'chkDisplayPoster
        '
        Me.chkDisplayPoster.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayPoster.AutoSize = True
        Me.chkDisplayPoster.Location = New System.Drawing.Point(259, 276)
        Me.chkDisplayPoster.Name = "chkDisplayPoster"
        Me.chkDisplayPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkDisplayPoster.TabIndex = 6
        Me.chkDisplayPoster.Text = "Poster"
        Me.chkDisplayPoster.UseVisualStyleBackColor = True
        '
        'chkDisplayLandscape
        '
        Me.chkDisplayLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayLandscape.AutoSize = True
        Me.chkDisplayLandscape.Location = New System.Drawing.Point(259, 253)
        Me.chkDisplayLandscape.Name = "chkDisplayLandscape"
        Me.chkDisplayLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkDisplayLandscape.TabIndex = 18
        Me.chkDisplayLandscape.Text = "Landscape"
        Me.chkDisplayLandscape.UseVisualStyleBackColor = True
        '
        'chkDisplayKeyArt
        '
        Me.chkDisplayKeyArt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayKeyArt.AutoSize = True
        Me.chkDisplayKeyArt.Location = New System.Drawing.Point(259, 230)
        Me.chkDisplayKeyArt.Name = "chkDisplayKeyArt"
        Me.chkDisplayKeyArt.Size = New System.Drawing.Size(58, 17)
        Me.chkDisplayKeyArt.TabIndex = 18
        Me.chkDisplayKeyArt.Text = "KeyArt"
        Me.chkDisplayKeyArt.UseVisualStyleBackColor = True
        '
        'chkDisplayStudioFlag
        '
        Me.chkDisplayStudioFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayStudioFlag.AutoSize = True
        Me.chkDisplayStudioFlag.Location = New System.Drawing.Point(3, 26)
        Me.chkDisplayStudioFlag.Name = "chkDisplayStudioFlag"
        Me.chkDisplayStudioFlag.Size = New System.Drawing.Size(85, 17)
        Me.chkDisplayStudioFlag.TabIndex = 12
        Me.chkDisplayStudioFlag.Text = "Studio Flag"
        Me.chkDisplayStudioFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayStudioText
        '
        Me.chkDisplayStudioName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayStudioName.AutoSize = True
        Me.chkDisplayStudioName.Location = New System.Drawing.Point(259, 26)
        Me.chkDisplayStudioName.Name = "chkDisplayStudioText"
        Me.chkDisplayStudioName.Size = New System.Drawing.Size(173, 17)
        Me.chkDisplayStudioName.TabIndex = 12
        Me.chkDisplayStudioName.Text = "Allways Display Studio Name"
        Me.chkDisplayStudioName.UseVisualStyleBackColor = True
        '
        'chkDisplayGenreText
        '
        Me.chkDisplayGenreText.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayGenreText.AutoSize = True
        Me.chkDisplayGenreText.Location = New System.Drawing.Point(259, 3)
        Me.chkDisplayGenreText.Name = "chkDisplayGenreText"
        Me.chkDisplayGenreText.Size = New System.Drawing.Size(165, 17)
        Me.chkDisplayGenreText.TabIndex = 9
        Me.chkDisplayGenreText.Text = "Allways Display Genres Text"
        Me.chkDisplayGenreText.UseVisualStyleBackColor = True
        '
        'chkDisplayVideoSourceFlag
        '
        Me.chkDisplayVideoSourceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayVideoSourceFlag.AutoSize = True
        Me.chkDisplayVideoSourceFlag.Location = New System.Drawing.Point(3, 49)
        Me.chkDisplayVideoSourceFlag.Name = "chkDisplayVideoSourceFlag"
        Me.chkDisplayVideoSourceFlag.Size = New System.Drawing.Size(116, 17)
        Me.chkDisplayVideoSourceFlag.TabIndex = 8
        Me.chkDisplayVideoSourceFlag.Text = "VideoSource Flag"
        Me.chkDisplayVideoSourceFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayVideoResolutionFlag
        '
        Me.chkDisplayVideoResolutionFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayVideoResolutionFlag.AutoSize = True
        Me.chkDisplayVideoResolutionFlag.Location = New System.Drawing.Point(259, 72)
        Me.chkDisplayVideoResolutionFlag.Name = "chkDisplayVideoResolutionFlag"
        Me.chkDisplayVideoResolutionFlag.Size = New System.Drawing.Size(140, 17)
        Me.chkDisplayVideoResolutionFlag.TabIndex = 8
        Me.chkDisplayVideoResolutionFlag.Text = "Video Resolution Flag"
        Me.chkDisplayVideoResolutionFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayVideoCodecFlag
        '
        Me.chkDisplayVideoCodecFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayVideoCodecFlag.AutoSize = True
        Me.chkDisplayVideoCodecFlag.Location = New System.Drawing.Point(3, 72)
        Me.chkDisplayVideoCodecFlag.Name = "chkDisplayVideoCodecFlag"
        Me.chkDisplayVideoCodecFlag.Size = New System.Drawing.Size(116, 17)
        Me.chkDisplayVideoCodecFlag.TabIndex = 8
        Me.chkDisplayVideoCodecFlag.Text = "Video Codec Flag"
        Me.chkDisplayVideoCodecFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayGenreFlags
        '
        Me.chkDisplayGenreFlags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayGenreFlags.AutoSize = True
        Me.chkDisplayGenreFlags.Location = New System.Drawing.Point(3, 3)
        Me.chkDisplayGenreFlags.Name = "chkDisplayGenreFlags"
        Me.chkDisplayGenreFlags.Size = New System.Drawing.Size(87, 17)
        Me.chkDisplayGenreFlags.TabIndex = 12
        Me.chkDisplayGenreFlags.Text = "Genre Flags"
        Me.chkDisplayGenreFlags.UseVisualStyleBackColor = True
        '
        'chkDisplayImageDimension
        '
        Me.chkDisplayImageDimension.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayImageDimension.AutoSize = True
        Me.chkDisplayImageDimension.Location = New System.Drawing.Point(259, 161)
        Me.chkDisplayImageDimension.Name = "chkDisplayImageDimension"
        Me.chkDisplayImageDimension.Size = New System.Drawing.Size(120, 17)
        Me.chkDisplayImageDimension.TabIndex = 8
        Me.chkDisplayImageDimension.Text = "Image Dimensions"
        Me.chkDisplayImageDimension.UseVisualStyleBackColor = True
        '
        'chkDisplayImageNames
        '
        Me.chkDisplayImageNames.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayImageNames.AutoSize = True
        Me.chkDisplayImageNames.Location = New System.Drawing.Point(3, 161)
        Me.chkDisplayImageNames.Name = "chkDisplayImageNames"
        Me.chkDisplayImageNames.Size = New System.Drawing.Size(94, 17)
        Me.chkDisplayImageNames.TabIndex = 20
        Me.chkDisplayImageNames.Text = "Image Names"
        Me.chkDisplayImageNames.UseVisualStyleBackColor = True
        '
        'chkDisplayLanguageFlags
        '
        Me.chkDisplayLanguageFlags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayLanguageFlags.AutoSize = True
        Me.chkDisplayLanguageFlags.Location = New System.Drawing.Point(259, 49)
        Me.chkDisplayLanguageFlags.Name = "chkDisplayLanguageFlags"
        Me.chkDisplayLanguageFlags.Size = New System.Drawing.Size(107, 17)
        Me.chkDisplayLanguageFlags.TabIndex = 8
        Me.chkDisplayLanguageFlags.Text = "Language Flags"
        Me.chkDisplayLanguageFlags.UseVisualStyleBackColor = True
        '
        'chkDisplayAudioChannelsFlag
        '
        Me.chkDisplayAudioChannelsFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayAudioChannelsFlag.AutoSize = True
        Me.chkDisplayAudioChannelsFlag.Location = New System.Drawing.Point(259, 95)
        Me.chkDisplayAudioChannelsFlag.Name = "chkDisplayAudioChannelsFlag"
        Me.chkDisplayAudioChannelsFlag.Size = New System.Drawing.Size(133, 17)
        Me.chkDisplayAudioChannelsFlag.TabIndex = 8
        Me.chkDisplayAudioChannelsFlag.Text = "Audio Channels Flag"
        Me.chkDisplayAudioChannelsFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayAudioCodecFlag
        '
        Me.chkDisplayAudioCodecFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayAudioCodecFlag.AutoSize = True
        Me.chkDisplayAudioCodecFlag.Location = New System.Drawing.Point(3, 95)
        Me.chkDisplayAudioCodecFlag.Name = "chkDisplayAudioCodecFlag"
        Me.chkDisplayAudioCodecFlag.Size = New System.Drawing.Size(117, 17)
        Me.chkDisplayAudioCodecFlag.TabIndex = 8
        Me.chkDisplayAudioCodecFlag.Text = "Audio Codec Flag"
        Me.chkDisplayAudioCodecFlag.UseVisualStyleBackColor = True
        '
        'chkDisplayImagesGlassOverlay
        '
        Me.chkDisplayImagesGlassOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayImagesGlassOverlay.AutoSize = True
        Me.chkDisplayImagesGlassOverlay.Location = New System.Drawing.Point(259, 138)
        Me.chkDisplayImagesGlassOverlay.Name = "chkDisplayImagesGlassOverlay"
        Me.chkDisplayImagesGlassOverlay.Size = New System.Drawing.Size(171, 17)
        Me.chkDisplayImagesGlassOverlay.TabIndex = 12
        Me.chkDisplayImagesGlassOverlay.Text = "Enable Images Glass Overlay"
        Me.chkDisplayImagesGlassOverlay.UseVisualStyleBackColor = True
        '
        'chkDoubleClickScrape
        '
        Me.chkDoubleClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDoubleClickScrape.AutoSize = True
        Me.chkDoubleClickScrape.Location = New System.Drawing.Point(3, 138)
        Me.chkDoubleClickScrape.Name = "chkDoubleClickScrape"
        Me.chkDoubleClickScrape.Size = New System.Drawing.Size(250, 17)
        Me.chkDoubleClickScrape.TabIndex = 19
        Me.chkDoubleClickScrape.Text = "Enable Image Scrape On Double Right Click"
        Me.chkDoubleClickScrape.UseVisualStyleBackColor = True
        '
        'gbInterface
        '
        Me.gbInterface.AutoSize = True
        Me.gbInterface.Controls.Add(Me.tblInterface)
        Me.gbInterface.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbInterface.Location = New System.Drawing.Point(3, 3)
        Me.gbInterface.Name = "gbInterface"
        Me.gbInterface.Size = New System.Drawing.Size(228, 317)
        Me.gbInterface.TabIndex = 1
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
        Me.tblInterface.Size = New System.Drawing.Size(222, 296)
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
        Me.txtThemeHints.Size = New System.Drawing.Size(216, 196)
        Me.txtThemeHints.TabIndex = 2
        Me.txtThemeHints.Text = resources.GetString("txtThemeHints.Text")
        '
        'frmOption_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 455)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_GUI"
        Me.Text = "frmOption_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbDisplayInMainWindow.ResumeLayout(False)
        Me.gbDisplayInMainWindow.PerformLayout()
        Me.tblGeneralMainWindow.ResumeLayout(False)
        Me.tblGeneralMainWindow.PerformLayout()
        Me.gbInterface.ResumeLayout(False)
        Me.gbInterface.PerformLayout()
        Me.tblInterface.ResumeLayout(False)
        Me.tblInterface.PerformLayout()
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
    Friend WithEvents txtThemeHints As TextBox
    Friend WithEvents gbDisplayInMainWindow As GroupBox
    Friend WithEvents tblGeneralMainWindow As TableLayoutPanel
    Friend WithEvents chkDisplayImageDimension As CheckBox
    Friend WithEvents chkDisplayFanart As CheckBox
    Friend WithEvents chkDisplayDiscArt As CheckBox
    Friend WithEvents chkDisplayImageNames As CheckBox
    Friend WithEvents chkDisplayFanartAsBackGround As CheckBox
    Friend WithEvents chkDisplayClearArt As CheckBox
    Friend WithEvents chkDoubleClickScrape As CheckBox
    Friend WithEvents chkDisplayClearLogo As CheckBox
    Friend WithEvents chkDisplayBanner As CheckBox
    Friend WithEvents chkDisplayCharacterArt As CheckBox
    Friend WithEvents chkDisplayGenreText As CheckBox
    Friend WithEvents chkDisplayPoster As CheckBox
    Friend WithEvents chkDisplayLandscape As CheckBox
    Friend WithEvents chkDisplayKeyArt As CheckBox
    Friend WithEvents chkDisplayLanguageFlags As CheckBox
    Friend WithEvents chkDisplayImagesGlassOverlay As CheckBox
    Friend WithEvents chkDisplayStudioName As CheckBox
    Friend WithEvents chkDisplayStudioFlag As CheckBox
    Friend WithEvents chkDisplayVideoSourceFlag As CheckBox
    Friend WithEvents chkDisplayVideoResolutionFlag As CheckBox
    Friend WithEvents chkDisplayVideoCodecFlag As CheckBox
    Friend WithEvents chkDisplayGenreFlags As CheckBox
    Friend WithEvents chkDisplayAudioChannelsFlag As CheckBox
    Friend WithEvents chkDisplayAudioCodecFlag As CheckBox
End Class
