<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_Data
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovie_Data))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.tblScraperFields = New System.Windows.Forms.TableLayoutPanel()
        Me.lblScraperFieldsHeaderLocked = New System.Windows.Forms.Label()
        Me.lblScraperFieldsHeaderLimit = New System.Windows.Forms.Label()
        Me.chkTitleUseOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.cbCertificationsLimit = New System.Windows.Forms.ComboBox()
        Me.chkRatingsLocked = New System.Windows.Forms.CheckBox()
        Me.chkTitleLock = New System.Windows.Forms.CheckBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblRatings = New System.Windows.Forms.Label()
        Me.lblLanguageAudio = New System.Windows.Forms.Label()
        Me.lblLanguageVideo = New System.Windows.Forms.Label()
        Me.lblCollection = New System.Windows.Forms.Label()
        Me.chkMetadataScanLockAudioLanguage = New System.Windows.Forms.CheckBox()
        Me.chkMetadataScanLockVideoLanguage = New System.Windows.Forms.CheckBox()
        Me.chkTitleEnabled = New System.Windows.Forms.CheckBox()
        Me.chkRatingsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCollectionLocked = New System.Windows.Forms.CheckBox()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.chkOriginalTitleEnabled = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitleLocked = New System.Windows.Forms.CheckBox()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.chkPremieredEnabled = New System.Windows.Forms.CheckBox()
        Me.chkPremieredLocked = New System.Windows.Forms.CheckBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.chkPlotEnabled = New System.Windows.Forms.CheckBox()
        Me.chkPlotLocked = New System.Windows.Forms.CheckBox()
        Me.lblOutline = New System.Windows.Forms.Label()
        Me.chkOutlineEnabled = New System.Windows.Forms.CheckBox()
        Me.chkOutlineLocked = New System.Windows.Forms.CheckBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.chkTaglineEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTaglineLocked = New System.Windows.Forms.CheckBox()
        Me.lblTop250 = New System.Windows.Forms.Label()
        Me.chkTop250Enabled = New System.Windows.Forms.CheckBox()
        Me.chkTop250Locked = New System.Windows.Forms.CheckBox()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.chkMPAAEnabled = New System.Windows.Forms.CheckBox()
        Me.chkMPAALocked = New System.Windows.Forms.CheckBox()
        Me.lblCertifications = New System.Windows.Forms.Label()
        Me.chkCertificationsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsLocked = New System.Windows.Forms.CheckBox()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.chkRuntimeEnabled = New System.Windows.Forms.CheckBox()
        Me.chkRuntimeLocked = New System.Windows.Forms.CheckBox()
        Me.lblStudios = New System.Windows.Forms.Label()
        Me.chkStudiosEnabled = New System.Windows.Forms.CheckBox()
        Me.chkStudiosLocked = New System.Windows.Forms.CheckBox()
        Me.txtStudiosLimit = New System.Windows.Forms.TextBox()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.chkTagsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTagsLocked = New System.Windows.Forms.CheckBox()
        Me.lblTrailerLink = New System.Windows.Forms.Label()
        Me.chkTrailerLinkEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTrailerLinkLocked = New System.Windows.Forms.CheckBox()
        Me.lblGenres = New System.Windows.Forms.Label()
        Me.chkGenresEnabled = New System.Windows.Forms.CheckBox()
        Me.chkGenresLocked = New System.Windows.Forms.CheckBox()
        Me.txtGenresLimit = New System.Windows.Forms.TextBox()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.chkActorsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkActorsLocked = New System.Windows.Forms.CheckBox()
        Me.txtActorsLimit = New System.Windows.Forms.TextBox()
        Me.lblCountries = New System.Windows.Forms.Label()
        Me.chkCountriesEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCountriesLocked = New System.Windows.Forms.CheckBox()
        Me.lblDirectors = New System.Windows.Forms.Label()
        Me.chkDirectorsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkDirectorsLocked = New System.Windows.Forms.CheckBox()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.chkCreditsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCreditsLocked = New System.Windows.Forms.CheckBox()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.chkUserRatingEnabled = New System.Windows.Forms.CheckBox()
        Me.chkUserRatingLocked = New System.Windows.Forms.CheckBox()
        Me.txtCountriesLimit = New System.Windows.Forms.TextBox()
        Me.btnTagsWhitelist = New System.Windows.Forms.Button()
        Me.lblOriginalTitleAsTitle = New System.Windows.Forms.Label()
        Me.chkCollectionEnabled = New System.Windows.Forms.CheckBox()
        Me.lblPlotForOutline = New System.Windows.Forms.Label()
        Me.chkOutlineUsePlot = New System.Windows.Forms.CheckBox()
        Me.txtOutlineLimit = New System.Windows.Forms.TextBox()
        Me.lblPlotForOutlineAsFallback = New System.Windows.Forms.Label()
        Me.chkOutlineUsePlotAsFallback = New System.Windows.Forms.CheckBox()
        Me.gbMPAA = New System.Windows.Forms.GroupBox()
        Me.tblMPAA = New System.Windows.Forms.TableLayoutPanel()
        Me.chkCertificationsOnlyValue = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsForMPAAFallback = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsForMPAA = New System.Windows.Forms.CheckBox()
        Me.txtMPAANotRatedValue = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperMPAANotRated = New System.Windows.Forms.Label()
        Me.gbMovieScraperMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkCleanPlotAndOutline = New System.Windows.Forms.CheckBox()
        Me.chkClearDisabledFields = New System.Windows.Forms.CheckBox()
        Me.chkTrailerLinkSaveKodiCompatible = New System.Windows.Forms.CheckBox()
        Me.chkActorsWithImageOnly = New System.Windows.Forms.CheckBox()
        Me.gbMetadata = New System.Windows.Forms.GroupBox()
        Me.tblMetaData = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMetadataScanDurationForRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.gbMovieScraperDefFIExtOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperDefFIExtOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieScraperDefFIExtRemove = New System.Windows.Forms.Button()
        Me.txtMovieScraperDefFIExt = New System.Windows.Forms.TextBox()
        Me.btnMovieScraperDefFIExtEdit = New System.Windows.Forms.Button()
        Me.lstMovieScraperDefFIExt = New System.Windows.Forms.ListBox()
        Me.btnMovieScraperDefFIExtAdd = New System.Windows.Forms.Button()
        Me.lblMovieScraperDefFIExt = New System.Windows.Forms.Label()
        Me.chkMetaDataScanEnabled = New System.Windows.Forms.CheckBox()
        Me.lblDurationForRuntimeFormat = New System.Windows.Forms.Label()
        Me.chkMetadataScanDurationForRuntimeEnabled = New System.Windows.Forms.CheckBox()
        Me.gbCollection = New System.Windows.Forms.GroupBox()
        Me.tblCollection = New System.Windows.Forms.TableLayoutPanel()
        Me.chkCollectionSaveYAMJCompatible = New System.Windows.Forms.CheckBox()
        Me.chkCollectionAutoAddToCollection = New System.Windows.Forms.CheckBox()
        Me.chkCollectionSaveExtendedInformation = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbScraperFields.SuspendLayout()
        Me.tblScraperFields.SuspendLayout()
        Me.gbMPAA.SuspendLayout()
        Me.tblMPAA.SuspendLayout()
        Me.gbMovieScraperMiscOpts.SuspendLayout()
        Me.tblMovieScraperMiscOpts.SuspendLayout()
        Me.gbMetadata.SuspendLayout()
        Me.tblMetaData.SuspendLayout()
        Me.gbMovieScraperDefFIExtOpts.SuspendLayout()
        Me.tblMovieScraperDefFIExtOpts.SuspendLayout()
        Me.gbCollection.SuspendLayout()
        Me.tblCollection.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(840, 712)
        Me.pnlSettings.TabIndex = 15
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
        Me.tblSettings.Controls.Add(Me.gbScraperFields, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbMPAA, 1, 3)
        Me.tblSettings.Controls.Add(Me.gbMovieScraperMiscOpts, 1, 2)
        Me.tblSettings.Controls.Add(Me.gbMetadata, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbCollection, 1, 4)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 6
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(840, 712)
        Me.tblSettings.TabIndex = 69
        '
        'gbScraperFields
        '
        Me.gbScraperFields.AutoSize = True
        Me.gbScraperFields.Controls.Add(Me.tblScraperFields)
        Me.gbScraperFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFields.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.tblSettings.SetRowSpan(Me.gbScraperFields, 5)
        Me.gbScraperFields.Size = New System.Drawing.Size(311, 620)
        Me.gbScraperFields.TabIndex = 1
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields - Global"
        '
        'tblScraperFields
        '
        Me.tblScraperFields.AutoScroll = True
        Me.tblScraperFields.AutoSize = True
        Me.tblScraperFields.ColumnCount = 5
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.Controls.Add(Me.lblScraperFieldsHeaderLocked, 2, 0)
        Me.tblScraperFields.Controls.Add(Me.lblScraperFieldsHeaderLimit, 3, 0)
        Me.tblScraperFields.Controls.Add(Me.cbCertificationsLimit, 3, 14)
        Me.tblScraperFields.Controls.Add(Me.chkRatingsLocked, 2, 10)
        Me.tblScraperFields.Controls.Add(Me.chkTitleLock, 2, 1)
        Me.tblScraperFields.Controls.Add(Me.lblTitle, 0, 1)
        Me.tblScraperFields.Controls.Add(Me.lblRatings, 0, 10)
        Me.tblScraperFields.Controls.Add(Me.lblLanguageAudio, 0, 25)
        Me.tblScraperFields.Controls.Add(Me.lblLanguageVideo, 0, 26)
        Me.tblScraperFields.Controls.Add(Me.lblCollection, 0, 24)
        Me.tblScraperFields.Controls.Add(Me.chkMetadataScanLockAudioLanguage, 2, 25)
        Me.tblScraperFields.Controls.Add(Me.chkMetadataScanLockVideoLanguage, 2, 26)
        Me.tblScraperFields.Controls.Add(Me.chkTitleEnabled, 1, 1)
        Me.tblScraperFields.Controls.Add(Me.chkRatingsEnabled, 1, 10)
        Me.tblScraperFields.Controls.Add(Me.chkCollectionLocked, 2, 24)
        Me.tblScraperFields.Controls.Add(Me.lblOriginalTitle, 0, 3)
        Me.tblScraperFields.Controls.Add(Me.chkOriginalTitleEnabled, 1, 3)
        Me.tblScraperFields.Controls.Add(Me.chkOriginalTitleLocked, 2, 3)
        Me.tblScraperFields.Controls.Add(Me.lblPremiered, 0, 4)
        Me.tblScraperFields.Controls.Add(Me.chkPremieredEnabled, 1, 4)
        Me.tblScraperFields.Controls.Add(Me.chkPremieredLocked, 2, 4)
        Me.tblScraperFields.Controls.Add(Me.lblPlot, 0, 5)
        Me.tblScraperFields.Controls.Add(Me.chkPlotEnabled, 1, 5)
        Me.tblScraperFields.Controls.Add(Me.chkPlotLocked, 2, 5)
        Me.tblScraperFields.Controls.Add(Me.lblOutline, 0, 6)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineEnabled, 1, 6)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineLocked, 2, 6)
        Me.tblScraperFields.Controls.Add(Me.lblTagline, 0, 9)
        Me.tblScraperFields.Controls.Add(Me.chkTaglineEnabled, 1, 9)
        Me.tblScraperFields.Controls.Add(Me.chkTaglineLocked, 2, 9)
        Me.tblScraperFields.Controls.Add(Me.lblTop250, 0, 12)
        Me.tblScraperFields.Controls.Add(Me.chkTop250Enabled, 1, 12)
        Me.tblScraperFields.Controls.Add(Me.chkTop250Locked, 2, 12)
        Me.tblScraperFields.Controls.Add(Me.lblMPAA, 0, 13)
        Me.tblScraperFields.Controls.Add(Me.chkMPAAEnabled, 1, 13)
        Me.tblScraperFields.Controls.Add(Me.chkMPAALocked, 2, 13)
        Me.tblScraperFields.Controls.Add(Me.lblCertifications, 0, 14)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsEnabled, 1, 14)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsLocked, 2, 14)
        Me.tblScraperFields.Controls.Add(Me.lblRuntime, 0, 15)
        Me.tblScraperFields.Controls.Add(Me.chkRuntimeEnabled, 1, 15)
        Me.tblScraperFields.Controls.Add(Me.chkRuntimeLocked, 2, 15)
        Me.tblScraperFields.Controls.Add(Me.lblStudios, 0, 23)
        Me.tblScraperFields.Controls.Add(Me.chkStudiosEnabled, 1, 23)
        Me.tblScraperFields.Controls.Add(Me.chkStudiosLocked, 2, 23)
        Me.tblScraperFields.Controls.Add(Me.txtStudiosLimit, 3, 23)
        Me.tblScraperFields.Controls.Add(Me.lblTags, 0, 16)
        Me.tblScraperFields.Controls.Add(Me.chkTagsEnabled, 1, 16)
        Me.tblScraperFields.Controls.Add(Me.chkTagsLocked, 2, 16)
        Me.tblScraperFields.Controls.Add(Me.lblTrailerLink, 0, 17)
        Me.tblScraperFields.Controls.Add(Me.chkTrailerLinkEnabled, 1, 17)
        Me.tblScraperFields.Controls.Add(Me.chkTrailerLinkLocked, 2, 17)
        Me.tblScraperFields.Controls.Add(Me.lblGenres, 0, 18)
        Me.tblScraperFields.Controls.Add(Me.chkGenresEnabled, 1, 18)
        Me.tblScraperFields.Controls.Add(Me.chkGenresLocked, 2, 18)
        Me.tblScraperFields.Controls.Add(Me.txtGenresLimit, 3, 18)
        Me.tblScraperFields.Controls.Add(Me.lblActors, 0, 19)
        Me.tblScraperFields.Controls.Add(Me.chkActorsEnabled, 1, 19)
        Me.tblScraperFields.Controls.Add(Me.chkActorsLocked, 2, 19)
        Me.tblScraperFields.Controls.Add(Me.txtActorsLimit, 3, 19)
        Me.tblScraperFields.Controls.Add(Me.lblCountries, 0, 22)
        Me.tblScraperFields.Controls.Add(Me.chkCountriesEnabled, 1, 22)
        Me.tblScraperFields.Controls.Add(Me.chkCountriesLocked, 2, 22)
        Me.tblScraperFields.Controls.Add(Me.lblDirectors, 0, 20)
        Me.tblScraperFields.Controls.Add(Me.chkDirectorsEnabled, 1, 20)
        Me.tblScraperFields.Controls.Add(Me.chkDirectorsLocked, 2, 20)
        Me.tblScraperFields.Controls.Add(Me.lblCredits, 0, 21)
        Me.tblScraperFields.Controls.Add(Me.chkCreditsEnabled, 1, 21)
        Me.tblScraperFields.Controls.Add(Me.chkCreditsLocked, 2, 21)
        Me.tblScraperFields.Controls.Add(Me.lblUserRating, 0, 11)
        Me.tblScraperFields.Controls.Add(Me.chkUserRatingEnabled, 1, 11)
        Me.tblScraperFields.Controls.Add(Me.chkUserRatingLocked, 2, 11)
        Me.tblScraperFields.Controls.Add(Me.txtCountriesLimit, 3, 22)
        Me.tblScraperFields.Controls.Add(Me.btnTagsWhitelist, 3, 16)
        Me.tblScraperFields.Controls.Add(Me.chkCollectionEnabled, 1, 24)
        Me.tblScraperFields.Controls.Add(Me.lblPlotForOutline, 0, 7)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineUsePlot, 1, 7)
        Me.tblScraperFields.Controls.Add(Me.txtOutlineLimit, 3, 6)
        Me.tblScraperFields.Controls.Add(Me.lblPlotForOutlineAsFallback, 0, 8)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineUsePlotAsFallback, 1, 8)
        Me.tblScraperFields.Controls.Add(Me.lblOriginalTitleAsTitle, 0, 2)
        Me.tblScraperFields.Controls.Add(Me.chkTitleUseOriginalTitle, 1, 2)
        Me.tblScraperFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFields.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFields.Name = "tblScraperFields"
        Me.tblScraperFields.RowCount = 28
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.Size = New System.Drawing.Size(305, 599)
        Me.tblScraperFields.TabIndex = 0
        '
        'lblScraperFieldsHeaderLocked
        '
        Me.lblScraperFieldsHeaderLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblScraperFieldsHeaderLocked.AutoSize = True
        Me.lblScraperFieldsHeaderLocked.Location = New System.Drawing.Point(183, 3)
        Me.lblScraperFieldsHeaderLocked.Name = "lblScraperFieldsHeaderLocked"
        Me.lblScraperFieldsHeaderLocked.Size = New System.Drawing.Size(43, 13)
        Me.lblScraperFieldsHeaderLocked.TabIndex = 12
        Me.lblScraperFieldsHeaderLocked.Text = "Locked"
        '
        'lblScraperFieldsHeaderLimit
        '
        Me.lblScraperFieldsHeaderLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblScraperFieldsHeaderLimit.AutoSize = True
        Me.lblScraperFieldsHeaderLimit.Location = New System.Drawing.Point(251, 3)
        Me.lblScraperFieldsHeaderLimit.Name = "lblScraperFieldsHeaderLimit"
        Me.lblScraperFieldsHeaderLimit.Size = New System.Drawing.Size(31, 13)
        Me.lblScraperFieldsHeaderLimit.TabIndex = 14
        Me.lblScraperFieldsHeaderLimit.Text = "Limit"
        '
        'chkTitleUseOriginalTitle
        '
        Me.chkTitleUseOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTitleUseOriginalTitle.AutoSize = True
        Me.chkTitleUseOriginalTitle.Enabled = False
        Me.chkTitleUseOriginalTitle.Location = New System.Drawing.Point(162, 46)
        Me.chkTitleUseOriginalTitle.Name = "chkTitleUseOriginalTitle"
        Me.chkTitleUseOriginalTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkTitleUseOriginalTitle.TabIndex = 68
        Me.chkTitleUseOriginalTitle.UseVisualStyleBackColor = True
        '
        'cbCertificationsLimit
        '
        Me.cbCertificationsLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCertificationsLimit.DropDownHeight = 200
        Me.cbCertificationsLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCertificationsLimit.DropDownWidth = 110
        Me.cbCertificationsLimit.Enabled = False
        Me.cbCertificationsLimit.FormattingEnabled = True
        Me.cbCertificationsLimit.IntegralHeight = False
        Me.cbCertificationsLimit.Items.AddRange(New Object() {"Argentina", "Australia", "Belgium", "Brazil", "Canada", "Finland", "France", "Germany", "Hong Kong", "Hungary", "Iceland", "Ireland", "Netherlands", "New Zealand", "Peru", "Poland", "Portugal", "Serbia", "Singapore", "South Korea", "Spain", "Sweden", "Switzerland", "Turkey", "UK", "USA"})
        Me.cbCertificationsLimit.Location = New System.Drawing.Point(232, 294)
        Me.cbCertificationsLimit.Name = "cbCertificationsLimit"
        Me.cbCertificationsLimit.Size = New System.Drawing.Size(70, 21)
        Me.cbCertificationsLimit.Sorted = True
        Me.cbCertificationsLimit.TabIndex = 5
        '
        'chkRatingsLocked
        '
        Me.chkRatingsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRatingsLocked.AutoSize = True
        Me.chkRatingsLocked.Location = New System.Drawing.Point(197, 214)
        Me.chkRatingsLocked.Name = "chkRatingsLocked"
        Me.chkRatingsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkRatingsLocked.TabIndex = 4
        Me.chkRatingsLocked.UseVisualStyleBackColor = True
        '
        'chkTitleLock
        '
        Me.chkTitleLock.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTitleLock.Location = New System.Drawing.Point(197, 23)
        Me.chkTitleLock.Name = "chkTitleLock"
        Me.chkTitleLock.Size = New System.Drawing.Size(14, 17)
        Me.chkTitleLock.TabIndex = 3
        Me.chkTitleLock.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(3, 25)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(28, 13)
        Me.lblTitle.TabIndex = 67
        Me.lblTitle.Text = "Title"
        '
        'lblRatings
        '
        Me.lblRatings.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRatings.AutoSize = True
        Me.lblRatings.Location = New System.Drawing.Point(3, 214)
        Me.lblRatings.Name = "lblRatings"
        Me.lblRatings.Size = New System.Drawing.Size(46, 13)
        Me.lblRatings.TabIndex = 68
        Me.lblRatings.Text = "Ratings"
        '
        'lblLanguageAudio
        '
        Me.lblLanguageAudio.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLanguageAudio.AutoSize = True
        Me.lblLanguageAudio.Location = New System.Drawing.Point(3, 562)
        Me.lblLanguageAudio.Name = "lblLanguageAudio"
        Me.lblLanguageAudio.Size = New System.Drawing.Size(144, 13)
        Me.lblLanguageAudio.TabIndex = 68
        Me.lblLanguageAudio.Text = "Metadata Audio Language"
        '
        'lblLanguageVideo
        '
        Me.lblLanguageVideo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLanguageVideo.AutoSize = True
        Me.lblLanguageVideo.Location = New System.Drawing.Point(3, 582)
        Me.lblLanguageVideo.Name = "lblLanguageVideo"
        Me.lblLanguageVideo.Size = New System.Drawing.Size(143, 13)
        Me.lblLanguageVideo.TabIndex = 68
        Me.lblLanguageVideo.Text = "Metadata Video Language"
        '
        'lblCollection
        '
        Me.lblCollection.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCollection.AutoSize = True
        Me.lblCollection.Location = New System.Drawing.Point(3, 542)
        Me.lblCollection.Name = "lblCollection"
        Me.lblCollection.Size = New System.Drawing.Size(59, 13)
        Me.lblCollection.TabIndex = 68
        Me.lblCollection.Text = "Collection"
        '
        'chkMetadataScanLockAudioLanguage
        '
        Me.chkMetadataScanLockAudioLanguage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMetadataScanLockAudioLanguage.AutoSize = True
        Me.chkMetadataScanLockAudioLanguage.Location = New System.Drawing.Point(197, 562)
        Me.chkMetadataScanLockAudioLanguage.Name = "chkMetadataScanLockAudioLanguage"
        Me.chkMetadataScanLockAudioLanguage.Size = New System.Drawing.Size(15, 14)
        Me.chkMetadataScanLockAudioLanguage.TabIndex = 48
        Me.chkMetadataScanLockAudioLanguage.UseVisualStyleBackColor = True
        '
        'chkMetadataScanLockVideoLanguage
        '
        Me.chkMetadataScanLockVideoLanguage.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMetadataScanLockVideoLanguage.AutoSize = True
        Me.chkMetadataScanLockVideoLanguage.Location = New System.Drawing.Point(197, 582)
        Me.chkMetadataScanLockVideoLanguage.Name = "chkMetadataScanLockVideoLanguage"
        Me.chkMetadataScanLockVideoLanguage.Size = New System.Drawing.Size(15, 14)
        Me.chkMetadataScanLockVideoLanguage.TabIndex = 47
        Me.chkMetadataScanLockVideoLanguage.UseVisualStyleBackColor = True
        '
        'chkTitleEnabled
        '
        Me.chkTitleEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTitleEnabled.AutoSize = True
        Me.chkTitleEnabled.Location = New System.Drawing.Point(162, 24)
        Me.chkTitleEnabled.Name = "chkTitleEnabled"
        Me.chkTitleEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkTitleEnabled.TabIndex = 0
        Me.chkTitleEnabled.UseVisualStyleBackColor = True
        '
        'chkRatingsEnabled
        '
        Me.chkRatingsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRatingsEnabled.AutoSize = True
        Me.chkRatingsEnabled.Location = New System.Drawing.Point(162, 214)
        Me.chkRatingsEnabled.Name = "chkRatingsEnabled"
        Me.chkRatingsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkRatingsEnabled.TabIndex = 4
        Me.chkRatingsEnabled.UseVisualStyleBackColor = True
        '
        'chkCollectionLocked
        '
        Me.chkCollectionLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCollectionLocked.AutoSize = True
        Me.chkCollectionLocked.Location = New System.Drawing.Point(197, 542)
        Me.chkCollectionLocked.Name = "chkCollectionLocked"
        Me.chkCollectionLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCollectionLocked.TabIndex = 66
        Me.chkCollectionLocked.UseVisualStyleBackColor = True
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.Location = New System.Drawing.Point(3, 66)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(73, 13)
        Me.lblOriginalTitle.TabIndex = 68
        Me.lblOriginalTitle.Text = "Original Title"
        '
        'chkOriginalTitleEnabled
        '
        Me.chkOriginalTitleEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOriginalTitleEnabled.AutoSize = True
        Me.chkOriginalTitleEnabled.Location = New System.Drawing.Point(162, 66)
        Me.chkOriginalTitleEnabled.Name = "chkOriginalTitleEnabled"
        Me.chkOriginalTitleEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkOriginalTitleEnabled.TabIndex = 29
        Me.chkOriginalTitleEnabled.UseVisualStyleBackColor = True
        '
        'chkOriginalTitleLocked
        '
        Me.chkOriginalTitleLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOriginalTitleLocked.AutoSize = True
        Me.chkOriginalTitleLocked.Location = New System.Drawing.Point(197, 66)
        Me.chkOriginalTitleLocked.Name = "chkOriginalTitleLocked"
        Me.chkOriginalTitleLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkOriginalTitleLocked.TabIndex = 65
        Me.chkOriginalTitleLocked.UseVisualStyleBackColor = True
        '
        'lblPremiered
        '
        Me.lblPremiered.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Location = New System.Drawing.Point(3, 86)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(58, 13)
        Me.lblPremiered.TabIndex = 68
        Me.lblPremiered.Text = "Premiered"
        '
        'chkPremieredEnabled
        '
        Me.chkPremieredEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPremieredEnabled.AutoSize = True
        Me.chkPremieredEnabled.Location = New System.Drawing.Point(162, 86)
        Me.chkPremieredEnabled.Name = "chkPremieredEnabled"
        Me.chkPremieredEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkPremieredEnabled.TabIndex = 3
        Me.chkPremieredEnabled.UseVisualStyleBackColor = True
        '
        'chkPremieredLocked
        '
        Me.chkPremieredLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPremieredLocked.AutoSize = True
        Me.chkPremieredLocked.Location = New System.Drawing.Point(197, 86)
        Me.chkPremieredLocked.Name = "chkPremieredLocked"
        Me.chkPremieredLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkPremieredLocked.TabIndex = 55
        Me.chkPremieredLocked.UseVisualStyleBackColor = True
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Location = New System.Drawing.Point(3, 106)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(27, 13)
        Me.lblPlot.TabIndex = 68
        Me.lblPlot.Text = "Plot"
        '
        'chkPlotEnabled
        '
        Me.chkPlotEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPlotEnabled.AutoSize = True
        Me.chkPlotEnabled.Location = New System.Drawing.Point(162, 106)
        Me.chkPlotEnabled.Name = "chkPlotEnabled"
        Me.chkPlotEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkPlotEnabled.TabIndex = 12
        Me.chkPlotEnabled.UseVisualStyleBackColor = True
        '
        'chkPlotLocked
        '
        Me.chkPlotLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPlotLocked.AutoSize = True
        Me.chkPlotLocked.Location = New System.Drawing.Point(197, 106)
        Me.chkPlotLocked.Name = "chkPlotLocked"
        Me.chkPlotLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkPlotLocked.TabIndex = 0
        Me.chkPlotLocked.UseVisualStyleBackColor = True
        '
        'lblOutline
        '
        Me.lblOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOutline.AutoSize = True
        Me.lblOutline.Location = New System.Drawing.Point(3, 130)
        Me.lblOutline.Name = "lblOutline"
        Me.lblOutline.Size = New System.Drawing.Size(46, 13)
        Me.lblOutline.TabIndex = 68
        Me.lblOutline.Text = "Outline"
        '
        'chkOutlineEnabled
        '
        Me.chkOutlineEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOutlineEnabled.AutoSize = True
        Me.chkOutlineEnabled.Location = New System.Drawing.Point(162, 130)
        Me.chkOutlineEnabled.Name = "chkOutlineEnabled"
        Me.chkOutlineEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkOutlineEnabled.TabIndex = 11
        Me.chkOutlineEnabled.UseVisualStyleBackColor = True
        '
        'chkOutlineLocked
        '
        Me.chkOutlineLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOutlineLocked.AutoSize = True
        Me.chkOutlineLocked.Location = New System.Drawing.Point(197, 130)
        Me.chkOutlineLocked.Name = "chkOutlineLocked"
        Me.chkOutlineLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkOutlineLocked.TabIndex = 1
        Me.chkOutlineLocked.UseVisualStyleBackColor = True
        '
        'lblTagline
        '
        Me.lblTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Location = New System.Drawing.Point(3, 194)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(43, 13)
        Me.lblTagline.TabIndex = 68
        Me.lblTagline.Text = "Tagline"
        '
        'chkTaglineEnabled
        '
        Me.chkTaglineEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTaglineEnabled.AutoSize = True
        Me.chkTaglineEnabled.Location = New System.Drawing.Point(162, 194)
        Me.chkTaglineEnabled.Name = "chkTaglineEnabled"
        Me.chkTaglineEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkTaglineEnabled.TabIndex = 8
        Me.chkTaglineEnabled.UseVisualStyleBackColor = True
        '
        'chkTaglineLocked
        '
        Me.chkTaglineLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTaglineLocked.AutoSize = True
        Me.chkTaglineLocked.Location = New System.Drawing.Point(197, 194)
        Me.chkTaglineLocked.Name = "chkTaglineLocked"
        Me.chkTaglineLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTaglineLocked.TabIndex = 3
        Me.chkTaglineLocked.UseVisualStyleBackColor = True
        '
        'lblTop250
        '
        Me.lblTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTop250.AutoSize = True
        Me.lblTop250.Location = New System.Drawing.Point(3, 254)
        Me.lblTop250.Name = "lblTop250"
        Me.lblTop250.Size = New System.Drawing.Size(46, 13)
        Me.lblTop250.TabIndex = 68
        Me.lblTop250.Text = "Top 250"
        '
        'chkTop250Enabled
        '
        Me.chkTop250Enabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTop250Enabled.AutoSize = True
        Me.chkTop250Enabled.Location = New System.Drawing.Point(162, 254)
        Me.chkTop250Enabled.Name = "chkTop250Enabled"
        Me.chkTop250Enabled.Size = New System.Drawing.Size(15, 14)
        Me.chkTop250Enabled.TabIndex = 23
        Me.chkTop250Enabled.UseVisualStyleBackColor = True
        '
        'chkTop250Locked
        '
        Me.chkTop250Locked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTop250Locked.AutoSize = True
        Me.chkTop250Locked.Location = New System.Drawing.Point(197, 254)
        Me.chkTop250Locked.Name = "chkTop250Locked"
        Me.chkTop250Locked.Size = New System.Drawing.Size(15, 14)
        Me.chkTop250Locked.TabIndex = 61
        Me.chkTop250Locked.UseVisualStyleBackColor = True
        '
        'lblMPAA
        '
        Me.lblMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMPAA.AutoSize = True
        Me.lblMPAA.Location = New System.Drawing.Point(3, 274)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(36, 13)
        Me.lblMPAA.TabIndex = 68
        Me.lblMPAA.Text = "MPAA"
        '
        'chkMPAAEnabled
        '
        Me.chkMPAAEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMPAAEnabled.AutoSize = True
        Me.chkMPAAEnabled.Location = New System.Drawing.Point(162, 274)
        Me.chkMPAAEnabled.Name = "chkMPAAEnabled"
        Me.chkMPAAEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkMPAAEnabled.TabIndex = 24
        Me.chkMPAAEnabled.UseVisualStyleBackColor = True
        '
        'chkMPAALocked
        '
        Me.chkMPAALocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMPAALocked.AutoSize = True
        Me.chkMPAALocked.Location = New System.Drawing.Point(197, 274)
        Me.chkMPAALocked.Name = "chkMPAALocked"
        Me.chkMPAALocked.Size = New System.Drawing.Size(15, 14)
        Me.chkMPAALocked.TabIndex = 49
        Me.chkMPAALocked.UseVisualStyleBackColor = True
        '
        'lblCertifications
        '
        Me.lblCertifications.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCertifications.AutoSize = True
        Me.lblCertifications.Location = New System.Drawing.Point(3, 298)
        Me.lblCertifications.Name = "lblCertifications"
        Me.lblCertifications.Size = New System.Drawing.Size(75, 13)
        Me.lblCertifications.TabIndex = 68
        Me.lblCertifications.Text = "Certifications"
        '
        'chkCertificationsEnabled
        '
        Me.chkCertificationsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCertificationsEnabled.AutoSize = True
        Me.chkCertificationsEnabled.Location = New System.Drawing.Point(162, 297)
        Me.chkCertificationsEnabled.Name = "chkCertificationsEnabled"
        Me.chkCertificationsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkCertificationsEnabled.TabIndex = 24
        Me.chkCertificationsEnabled.UseVisualStyleBackColor = True
        '
        'chkCertificationsLocked
        '
        Me.chkCertificationsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCertificationsLocked.AutoSize = True
        Me.chkCertificationsLocked.Location = New System.Drawing.Point(197, 297)
        Me.chkCertificationsLocked.Name = "chkCertificationsLocked"
        Me.chkCertificationsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCertificationsLocked.TabIndex = 49
        Me.chkCertificationsLocked.UseVisualStyleBackColor = True
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Location = New System.Drawing.Point(3, 321)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(50, 13)
        Me.lblRuntime.TabIndex = 68
        Me.lblRuntime.Text = "Runtime"
        '
        'chkRuntimeEnabled
        '
        Me.chkRuntimeEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRuntimeEnabled.AutoSize = True
        Me.chkRuntimeEnabled.Location = New System.Drawing.Point(162, 321)
        Me.chkRuntimeEnabled.Name = "chkRuntimeEnabled"
        Me.chkRuntimeEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkRuntimeEnabled.TabIndex = 13
        Me.chkRuntimeEnabled.UseVisualStyleBackColor = True
        '
        'chkRuntimeLocked
        '
        Me.chkRuntimeLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRuntimeLocked.AutoSize = True
        Me.chkRuntimeLocked.Location = New System.Drawing.Point(197, 321)
        Me.chkRuntimeLocked.Name = "chkRuntimeLocked"
        Me.chkRuntimeLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkRuntimeLocked.TabIndex = 51
        Me.chkRuntimeLocked.UseVisualStyleBackColor = True
        '
        'lblStudios
        '
        Me.lblStudios.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblStudios.AutoSize = True
        Me.lblStudios.Location = New System.Drawing.Point(3, 518)
        Me.lblStudios.Name = "lblStudios"
        Me.lblStudios.Size = New System.Drawing.Size(46, 13)
        Me.lblStudios.TabIndex = 68
        Me.lblStudios.Text = "Studios"
        '
        'chkStudiosEnabled
        '
        Me.chkStudiosEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkStudiosEnabled.AutoSize = True
        Me.chkStudiosEnabled.Location = New System.Drawing.Point(162, 518)
        Me.chkStudiosEnabled.Name = "chkStudiosEnabled"
        Me.chkStudiosEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkStudiosEnabled.TabIndex = 14
        Me.chkStudiosEnabled.UseVisualStyleBackColor = True
        '
        'chkStudiosLocked
        '
        Me.chkStudiosLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkStudiosLocked.AutoSize = True
        Me.chkStudiosLocked.Location = New System.Drawing.Point(197, 518)
        Me.chkStudiosLocked.Name = "chkStudiosLocked"
        Me.chkStudiosLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkStudiosLocked.TabIndex = 54
        Me.chkStudiosLocked.UseVisualStyleBackColor = True
        '
        'txtStudiosLimit
        '
        Me.txtStudiosLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStudiosLimit.Enabled = False
        Me.txtStudiosLimit.Location = New System.Drawing.Point(232, 514)
        Me.txtStudiosLimit.Name = "txtStudiosLimit"
        Me.txtStudiosLimit.Size = New System.Drawing.Size(70, 22)
        Me.txtStudiosLimit.TabIndex = 30
        '
        'lblTags
        '
        Me.lblTags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTags.AutoSize = True
        Me.lblTags.Location = New System.Drawing.Point(3, 346)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(29, 13)
        Me.lblTags.TabIndex = 68
        Me.lblTags.Text = "Tags"
        '
        'chkTagsEnabled
        '
        Me.chkTagsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTagsEnabled.AutoSize = True
        Me.chkTagsEnabled.Location = New System.Drawing.Point(162, 345)
        Me.chkTagsEnabled.Name = "chkTagsEnabled"
        Me.chkTagsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkTagsEnabled.TabIndex = 27
        Me.chkTagsEnabled.UseVisualStyleBackColor = True
        '
        'chkTagsLocked
        '
        Me.chkTagsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTagsLocked.AutoSize = True
        Me.chkTagsLocked.Location = New System.Drawing.Point(197, 345)
        Me.chkTagsLocked.Name = "chkTagsLocked"
        Me.chkTagsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTagsLocked.TabIndex = 64
        Me.chkTagsLocked.UseVisualStyleBackColor = True
        '
        'lblTrailerLink
        '
        Me.lblTrailerLink.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTrailerLink.AutoSize = True
        Me.lblTrailerLink.Location = New System.Drawing.Point(3, 370)
        Me.lblTrailerLink.Name = "lblTrailerLink"
        Me.lblTrailerLink.Size = New System.Drawing.Size(62, 13)
        Me.lblTrailerLink.TabIndex = 68
        Me.lblTrailerLink.Text = "Trailer-Link"
        '
        'chkTrailerLinkEnabled
        '
        Me.chkTrailerLinkEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTrailerLinkEnabled.AutoSize = True
        Me.chkTrailerLinkEnabled.Location = New System.Drawing.Point(162, 370)
        Me.chkTrailerLinkEnabled.Name = "chkTrailerLinkEnabled"
        Me.chkTrailerLinkEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkTrailerLinkEnabled.TabIndex = 5
        Me.chkTrailerLinkEnabled.UseVisualStyleBackColor = True
        '
        'chkTrailerLinkLocked
        '
        Me.chkTrailerLinkLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTrailerLinkLocked.AutoSize = True
        Me.chkTrailerLinkLocked.Location = New System.Drawing.Point(197, 370)
        Me.chkTrailerLinkLocked.Name = "chkTrailerLinkLocked"
        Me.chkTrailerLinkLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTrailerLinkLocked.TabIndex = 46
        Me.chkTrailerLinkLocked.UseVisualStyleBackColor = True
        '
        'lblGenres
        '
        Me.lblGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGenres.AutoSize = True
        Me.lblGenres.Location = New System.Drawing.Point(3, 394)
        Me.lblGenres.Name = "lblGenres"
        Me.lblGenres.Size = New System.Drawing.Size(43, 13)
        Me.lblGenres.TabIndex = 68
        Me.lblGenres.Text = "Genres"
        '
        'chkGenresEnabled
        '
        Me.chkGenresEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkGenresEnabled.AutoSize = True
        Me.chkGenresEnabled.Location = New System.Drawing.Point(162, 394)
        Me.chkGenresEnabled.Name = "chkGenresEnabled"
        Me.chkGenresEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkGenresEnabled.TabIndex = 10
        Me.chkGenresEnabled.UseVisualStyleBackColor = True
        '
        'chkGenresLocked
        '
        Me.chkGenresLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkGenresLocked.AutoSize = True
        Me.chkGenresLocked.Location = New System.Drawing.Point(197, 394)
        Me.chkGenresLocked.Name = "chkGenresLocked"
        Me.chkGenresLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkGenresLocked.TabIndex = 7
        Me.chkGenresLocked.UseVisualStyleBackColor = True
        '
        'txtGenresLimit
        '
        Me.txtGenresLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGenresLimit.Enabled = False
        Me.txtGenresLimit.Location = New System.Drawing.Point(232, 390)
        Me.txtGenresLimit.Name = "txtGenresLimit"
        Me.txtGenresLimit.Size = New System.Drawing.Size(70, 22)
        Me.txtGenresLimit.TabIndex = 21
        '
        'lblActors
        '
        Me.lblActors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblActors.AutoSize = True
        Me.lblActors.Location = New System.Drawing.Point(3, 422)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(39, 13)
        Me.lblActors.TabIndex = 68
        Me.lblActors.Text = "Actors"
        '
        'chkActorsEnabled
        '
        Me.chkActorsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkActorsEnabled.AutoSize = True
        Me.chkActorsEnabled.Location = New System.Drawing.Point(162, 422)
        Me.chkActorsEnabled.Name = "chkActorsEnabled"
        Me.chkActorsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkActorsEnabled.TabIndex = 7
        Me.chkActorsEnabled.UseVisualStyleBackColor = True
        '
        'chkActorsLocked
        '
        Me.chkActorsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkActorsLocked.AutoSize = True
        Me.chkActorsLocked.Location = New System.Drawing.Point(197, 422)
        Me.chkActorsLocked.Name = "chkActorsLocked"
        Me.chkActorsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkActorsLocked.TabIndex = 50
        Me.chkActorsLocked.UseVisualStyleBackColor = True
        '
        'txtActorsLimit
        '
        Me.txtActorsLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtActorsLimit.Enabled = False
        Me.txtActorsLimit.Location = New System.Drawing.Point(232, 418)
        Me.txtActorsLimit.Name = "txtActorsLimit"
        Me.txtActorsLimit.Size = New System.Drawing.Size(70, 22)
        Me.txtActorsLimit.TabIndex = 19
        '
        'lblCountries
        '
        Me.lblCountries.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCountries.AutoSize = True
        Me.lblCountries.Location = New System.Drawing.Point(3, 490)
        Me.lblCountries.Name = "lblCountries"
        Me.lblCountries.Size = New System.Drawing.Size(57, 13)
        Me.lblCountries.TabIndex = 68
        Me.lblCountries.Text = "Countries"
        '
        'chkCountriesEnabled
        '
        Me.chkCountriesEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCountriesEnabled.AutoSize = True
        Me.chkCountriesEnabled.Location = New System.Drawing.Point(162, 490)
        Me.chkCountriesEnabled.Name = "chkCountriesEnabled"
        Me.chkCountriesEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkCountriesEnabled.TabIndex = 25
        Me.chkCountriesEnabled.UseVisualStyleBackColor = True
        '
        'chkCountriesLocked
        '
        Me.chkCountriesLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCountriesLocked.AutoSize = True
        Me.chkCountriesLocked.Location = New System.Drawing.Point(197, 490)
        Me.chkCountriesLocked.Name = "chkCountriesLocked"
        Me.chkCountriesLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCountriesLocked.TabIndex = 63
        Me.chkCountriesLocked.UseVisualStyleBackColor = True
        '
        'lblDirectors
        '
        Me.lblDirectors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDirectors.AutoSize = True
        Me.lblDirectors.Location = New System.Drawing.Point(3, 446)
        Me.lblDirectors.Name = "lblDirectors"
        Me.lblDirectors.Size = New System.Drawing.Size(53, 13)
        Me.lblDirectors.TabIndex = 68
        Me.lblDirectors.Text = "Directors"
        '
        'chkDirectorsEnabled
        '
        Me.chkDirectorsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDirectorsEnabled.AutoSize = True
        Me.chkDirectorsEnabled.Location = New System.Drawing.Point(162, 446)
        Me.chkDirectorsEnabled.Name = "chkDirectorsEnabled"
        Me.chkDirectorsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkDirectorsEnabled.TabIndex = 9
        Me.chkDirectorsEnabled.UseVisualStyleBackColor = True
        '
        'chkDirectorsLocked
        '
        Me.chkDirectorsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDirectorsLocked.AutoSize = True
        Me.chkDirectorsLocked.Location = New System.Drawing.Point(197, 446)
        Me.chkDirectorsLocked.Name = "chkDirectorsLocked"
        Me.chkDirectorsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkDirectorsLocked.TabIndex = 57
        Me.chkDirectorsLocked.UseVisualStyleBackColor = True
        '
        'lblCredits
        '
        Me.lblCredits.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Location = New System.Drawing.Point(3, 466)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(43, 13)
        Me.lblCredits.TabIndex = 68
        Me.lblCredits.Text = "Credits"
        '
        'chkCreditsEnabled
        '
        Me.chkCreditsEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCreditsEnabled.AutoSize = True
        Me.chkCreditsEnabled.Location = New System.Drawing.Point(162, 466)
        Me.chkCreditsEnabled.Name = "chkCreditsEnabled"
        Me.chkCreditsEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkCreditsEnabled.TabIndex = 15
        Me.chkCreditsEnabled.UseVisualStyleBackColor = True
        '
        'chkCreditsLocked
        '
        Me.chkCreditsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCreditsLocked.AutoSize = True
        Me.chkCreditsLocked.Location = New System.Drawing.Point(197, 466)
        Me.chkCreditsLocked.Name = "chkCreditsLocked"
        Me.chkCreditsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCreditsLocked.TabIndex = 58
        Me.chkCreditsLocked.UseVisualStyleBackColor = True
        '
        'lblUserRating
        '
        Me.lblUserRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUserRating.AutoSize = True
        Me.lblUserRating.Location = New System.Drawing.Point(3, 234)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(67, 13)
        Me.lblUserRating.TabIndex = 68
        Me.lblUserRating.Text = "User Rating"
        '
        'chkUserRatingEnabled
        '
        Me.chkUserRatingEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkUserRatingEnabled.AutoSize = True
        Me.chkUserRatingEnabled.Location = New System.Drawing.Point(162, 234)
        Me.chkUserRatingEnabled.Name = "chkUserRatingEnabled"
        Me.chkUserRatingEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkUserRatingEnabled.TabIndex = 4
        Me.chkUserRatingEnabled.UseVisualStyleBackColor = True
        '
        'chkUserRatingLocked
        '
        Me.chkUserRatingLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkUserRatingLocked.AutoSize = True
        Me.chkUserRatingLocked.Location = New System.Drawing.Point(197, 234)
        Me.chkUserRatingLocked.Name = "chkUserRatingLocked"
        Me.chkUserRatingLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkUserRatingLocked.TabIndex = 4
        Me.chkUserRatingLocked.UseVisualStyleBackColor = True
        '
        'txtCountriesLimit
        '
        Me.txtCountriesLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCountriesLimit.Enabled = False
        Me.txtCountriesLimit.Location = New System.Drawing.Point(232, 486)
        Me.txtCountriesLimit.Name = "txtCountriesLimit"
        Me.txtCountriesLimit.Size = New System.Drawing.Size(70, 22)
        Me.txtCountriesLimit.TabIndex = 21
        '
        'btnTagsWhitelist
        '
        Me.btnTagsWhitelist.AutoSize = True
        Me.btnTagsWhitelist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTagsWhitelist.Enabled = False
        Me.btnTagsWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTagsWhitelist.Location = New System.Drawing.Point(232, 341)
        Me.btnTagsWhitelist.Name = "btnTagsWhitelist"
        Me.btnTagsWhitelist.Size = New System.Drawing.Size(70, 23)
        Me.btnTagsWhitelist.TabIndex = 23
        Me.btnTagsWhitelist.Text = "Whitelist"
        Me.btnTagsWhitelist.UseVisualStyleBackColor = True
        '
        'lblOriginalTitleAsTitle
        '
        Me.lblOriginalTitleAsTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitleAsTitle.AutoSize = True
        Me.lblOriginalTitleAsTitle.Location = New System.Drawing.Point(3, 46)
        Me.lblOriginalTitleAsTitle.Name = "lblOriginalTitleAsTitle"
        Me.lblOriginalTitleAsTitle.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblOriginalTitleAsTitle.Size = New System.Drawing.Size(153, 13)
        Me.lblOriginalTitleAsTitle.TabIndex = 68
        Me.lblOriginalTitleAsTitle.Text = "Use Original Title as Title"
        '
        'chkCollectionEnabled
        '
        Me.chkCollectionEnabled.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCollectionEnabled.AutoSize = True
        Me.chkCollectionEnabled.Location = New System.Drawing.Point(162, 542)
        Me.chkCollectionEnabled.Name = "chkCollectionEnabled"
        Me.chkCollectionEnabled.Size = New System.Drawing.Size(15, 14)
        Me.chkCollectionEnabled.TabIndex = 26
        Me.chkCollectionEnabled.UseVisualStyleBackColor = True
        '
        'lblPlotForOutline
        '
        Me.lblPlotForOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlotForOutline.AutoSize = True
        Me.lblPlotForOutline.Location = New System.Drawing.Point(3, 154)
        Me.lblPlotForOutline.Name = "lblPlotForOutline"
        Me.lblPlotForOutline.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblPlotForOutline.Size = New System.Drawing.Size(132, 13)
        Me.lblPlotForOutline.TabIndex = 68
        Me.lblPlotForOutline.Text = "Use Plot for Outline "
        '
        'chkOutlineUsePlot
        '
        Me.chkOutlineUsePlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOutlineUsePlot.AutoSize = True
        Me.chkOutlineUsePlot.Enabled = False
        Me.chkOutlineUsePlot.Location = New System.Drawing.Point(162, 154)
        Me.chkOutlineUsePlot.Name = "chkOutlineUsePlot"
        Me.chkOutlineUsePlot.Size = New System.Drawing.Size(15, 14)
        Me.chkOutlineUsePlot.TabIndex = 68
        Me.chkOutlineUsePlot.UseVisualStyleBackColor = True
        '
        'txtOutlineLimit
        '
        Me.txtOutlineLimit.Enabled = False
        Me.txtOutlineLimit.Location = New System.Drawing.Point(232, 126)
        Me.txtOutlineLimit.Name = "txtOutlineLimit"
        Me.txtOutlineLimit.Size = New System.Drawing.Size(70, 22)
        Me.txtOutlineLimit.TabIndex = 69
        '
        'lblPlotForOutlineAsFallback
        '
        Me.lblPlotForOutlineAsFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlotForOutlineAsFallback.AutoSize = True
        Me.lblPlotForOutlineAsFallback.Location = New System.Drawing.Point(3, 174)
        Me.lblPlotForOutlineAsFallback.Name = "lblPlotForOutlineAsFallback"
        Me.lblPlotForOutlineAsFallback.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblPlotForOutlineAsFallback.Size = New System.Drawing.Size(148, 13)
        Me.lblPlotForOutlineAsFallback.TabIndex = 68
        Me.lblPlotForOutlineAsFallback.Text = "Only if Outline is empty"
        '
        'chkOutlineUsePlotAsFallback
        '
        Me.chkOutlineUsePlotAsFallback.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOutlineUsePlotAsFallback.AutoSize = True
        Me.chkOutlineUsePlotAsFallback.Enabled = False
        Me.chkOutlineUsePlotAsFallback.Location = New System.Drawing.Point(162, 174)
        Me.chkOutlineUsePlotAsFallback.Name = "chkOutlineUsePlotAsFallback"
        Me.chkOutlineUsePlotAsFallback.Size = New System.Drawing.Size(15, 14)
        Me.chkOutlineUsePlotAsFallback.TabIndex = 84
        Me.chkOutlineUsePlotAsFallback.UseVisualStyleBackColor = True
        '
        'gbMPAA
        '
        Me.gbMPAA.AutoSize = True
        Me.gbMPAA.Controls.Add(Me.tblMPAA)
        Me.gbMPAA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMPAA.Location = New System.Drawing.Point(320, 311)
        Me.gbMPAA.Name = "gbMPAA"
        Me.gbMPAA.Size = New System.Drawing.Size(458, 118)
        Me.gbMPAA.TabIndex = 77
        Me.gbMPAA.TabStop = False
        Me.gbMPAA.Text = "MPAA"
        '
        'tblMPAA
        '
        Me.tblMPAA.AutoSize = True
        Me.tblMPAA.ColumnCount = 2
        Me.tblMPAA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMPAA.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMPAA.Controls.Add(Me.chkCertificationsOnlyValue, 0, 2)
        Me.tblMPAA.Controls.Add(Me.chkCertificationsForMPAAFallback, 0, 1)
        Me.tblMPAA.Controls.Add(Me.chkCertificationsForMPAA, 0, 0)
        Me.tblMPAA.Controls.Add(Me.txtMPAANotRatedValue, 1, 3)
        Me.tblMPAA.Controls.Add(Me.lblMovieScraperMPAANotRated, 0, 3)
        Me.tblMPAA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMPAA.Location = New System.Drawing.Point(3, 18)
        Me.tblMPAA.Name = "tblMPAA"
        Me.tblMPAA.RowCount = 5
        Me.tblMPAA.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMPAA.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMPAA.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMPAA.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMPAA.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMPAA.Size = New System.Drawing.Size(452, 97)
        Me.tblMPAA.TabIndex = 78
        '
        'chkCertificationsOnlyValue
        '
        Me.chkCertificationsOnlyValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsOnlyValue.AutoSize = True
        Me.tblMPAA.SetColumnSpan(Me.chkCertificationsOnlyValue, 2)
        Me.chkCertificationsOnlyValue.Enabled = False
        Me.chkCertificationsOnlyValue.Location = New System.Drawing.Point(3, 49)
        Me.chkCertificationsOnlyValue.Name = "chkCertificationsOnlyValue"
        Me.chkCertificationsOnlyValue.Size = New System.Drawing.Size(104, 17)
        Me.chkCertificationsOnlyValue.TabIndex = 66
        Me.chkCertificationsOnlyValue.Text = "Save value only"
        Me.chkCertificationsOnlyValue.UseVisualStyleBackColor = True
        '
        'chkCertificationsForMPAAFallback
        '
        Me.chkCertificationsForMPAAFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsForMPAAFallback.AutoSize = True
        Me.tblMPAA.SetColumnSpan(Me.chkCertificationsForMPAAFallback, 2)
        Me.chkCertificationsForMPAAFallback.Enabled = False
        Me.chkCertificationsForMPAAFallback.Location = New System.Drawing.Point(3, 26)
        Me.chkCertificationsForMPAAFallback.Name = "chkCertificationsForMPAAFallback"
        Me.chkCertificationsForMPAAFallback.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkCertificationsForMPAAFallback.Size = New System.Drawing.Size(175, 17)
        Me.chkCertificationsForMPAAFallback.TabIndex = 68
        Me.chkCertificationsForMPAAFallback.Text = "Only if no MPAA is found"
        Me.chkCertificationsForMPAAFallback.UseVisualStyleBackColor = True
        '
        'chkCertificationsForMPAA
        '
        Me.chkCertificationsForMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsForMPAA.AutoSize = True
        Me.tblMPAA.SetColumnSpan(Me.chkCertificationsForMPAA, 2)
        Me.chkCertificationsForMPAA.Enabled = False
        Me.chkCertificationsForMPAA.Location = New System.Drawing.Point(3, 3)
        Me.chkCertificationsForMPAA.Name = "chkCertificationsForMPAA"
        Me.chkCertificationsForMPAA.Size = New System.Drawing.Size(161, 17)
        Me.chkCertificationsForMPAA.TabIndex = 6
        Me.chkCertificationsForMPAA.Text = "Use Certification for MPAA"
        Me.chkCertificationsForMPAA.UseVisualStyleBackColor = True
        '
        'txtMPAANotRatedValue
        '
        Me.txtMPAANotRatedValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMPAANotRatedValue.Location = New System.Drawing.Point(198, 72)
        Me.txtMPAANotRatedValue.Name = "txtMPAANotRatedValue"
        Me.txtMPAANotRatedValue.Size = New System.Drawing.Size(251, 22)
        Me.txtMPAANotRatedValue.TabIndex = 69
        '
        'lblMovieScraperMPAANotRated
        '
        Me.lblMovieScraperMPAANotRated.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperMPAANotRated.AutoSize = True
        Me.lblMovieScraperMPAANotRated.Location = New System.Drawing.Point(3, 76)
        Me.lblMovieScraperMPAANotRated.Name = "lblMovieScraperMPAANotRated"
        Me.lblMovieScraperMPAANotRated.Size = New System.Drawing.Size(189, 13)
        Me.lblMovieScraperMPAANotRated.TabIndex = 68
        Me.lblMovieScraperMPAANotRated.Text = "MPAA value if no rating is available:"
        '
        'gbMovieScraperMiscOpts
        '
        Me.gbMovieScraperMiscOpts.AutoSize = True
        Me.gbMovieScraperMiscOpts.Controls.Add(Me.tblMovieScraperMiscOpts)
        Me.gbMovieScraperMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperMiscOpts.Location = New System.Drawing.Point(320, 192)
        Me.gbMovieScraperMiscOpts.Name = "gbMovieScraperMiscOpts"
        Me.gbMovieScraperMiscOpts.Size = New System.Drawing.Size(458, 113)
        Me.gbMovieScraperMiscOpts.TabIndex = 0
        Me.gbMovieScraperMiscOpts.TabStop = False
        Me.gbMovieScraperMiscOpts.Text = "Miscellaneous"
        '
        'tblMovieScraperMiscOpts
        '
        Me.tblMovieScraperMiscOpts.AutoSize = True
        Me.tblMovieScraperMiscOpts.ColumnCount = 4
        Me.tblMovieScraperMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkCleanPlotAndOutline, 0, 3)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkClearDisabledFields, 0, 0)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkTrailerLinkSaveKodiCompatible, 0, 5)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkActorsWithImageOnly, 0, 1)
        Me.tblMovieScraperMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperMiscOpts.Name = "tblMovieScraperMiscOpts"
        Me.tblMovieScraperMiscOpts.RowCount = 7
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.Size = New System.Drawing.Size(452, 92)
        Me.tblMovieScraperMiscOpts.TabIndex = 78
        '
        'chkCleanPlotAndOutline
        '
        Me.chkCleanPlotAndOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanPlotAndOutline.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkCleanPlotAndOutline, 3)
        Me.chkCleanPlotAndOutline.Location = New System.Drawing.Point(3, 49)
        Me.chkCleanPlotAndOutline.Name = "chkCleanPlotAndOutline"
        Me.chkCleanPlotAndOutline.Size = New System.Drawing.Size(121, 17)
        Me.chkCleanPlotAndOutline.TabIndex = 76
        Me.chkCleanPlotAndOutline.Text = "Clean Plot/Outline"
        Me.chkCleanPlotAndOutline.UseVisualStyleBackColor = True
        '
        'chkClearDisabledFields
        '
        Me.chkClearDisabledFields.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClearDisabledFields.AutoSize = True
        Me.chkClearDisabledFields.Location = New System.Drawing.Point(3, 3)
        Me.chkClearDisabledFields.Name = "chkClearDisabledFields"
        Me.chkClearDisabledFields.Size = New System.Drawing.Size(130, 17)
        Me.chkClearDisabledFields.TabIndex = 79
        Me.chkClearDisabledFields.Text = "Clear disabled fields"
        Me.chkClearDisabledFields.UseVisualStyleBackColor = True
        '
        'chkTrailerLinkSaveKodiCompatible
        '
        Me.chkTrailerLinkSaveKodiCompatible.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkTrailerLinkSaveKodiCompatible, 3)
        Me.chkTrailerLinkSaveKodiCompatible.Location = New System.Drawing.Point(3, 72)
        Me.chkTrailerLinkSaveKodiCompatible.Name = "chkTrailerLinkSaveKodiCompatible"
        Me.chkTrailerLinkSaveKodiCompatible.Size = New System.Drawing.Size(295, 17)
        Me.chkTrailerLinkSaveKodiCompatible.TabIndex = 83
        Me.chkTrailerLinkSaveKodiCompatible.Text = "Save YouTube-Trailer-Links in Kodi compatible format"
        Me.chkTrailerLinkSaveKodiCompatible.UseVisualStyleBackColor = True
        '
        'chkActorsWithImageOnly
        '
        Me.chkActorsWithImageOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkActorsWithImageOnly.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkActorsWithImageOnly, 3)
        Me.chkActorsWithImageOnly.Location = New System.Drawing.Point(3, 26)
        Me.chkActorsWithImageOnly.Name = "chkActorsWithImageOnly"
        Me.chkActorsWithImageOnly.Size = New System.Drawing.Size(189, 17)
        Me.chkActorsWithImageOnly.TabIndex = 1
        Me.chkActorsWithImageOnly.Text = "Scrape Only Actors With Images"
        Me.chkActorsWithImageOnly.UseVisualStyleBackColor = True
        '
        'gbMetadata
        '
        Me.gbMetadata.AutoSize = True
        Me.gbMetadata.Controls.Add(Me.tblMetaData)
        Me.gbMetadata.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMetadata.Location = New System.Drawing.Point(320, 3)
        Me.gbMetadata.Name = "gbMetadata"
        Me.gbMetadata.Size = New System.Drawing.Size(458, 163)
        Me.gbMetadata.TabIndex = 63
        Me.gbMetadata.TabStop = False
        Me.gbMetadata.Text = "Metadata"
        '
        'tblMetaData
        '
        Me.tblMetaData.AutoSize = True
        Me.tblMetaData.ColumnCount = 4
        Me.tblMetaData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMetaData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMetaData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMetaData.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMetaData.Controls.Add(Me.txtMetadataScanDurationForRuntimeFormat, 0, 2)
        Me.tblMetaData.Controls.Add(Me.gbMovieScraperDefFIExtOpts, 2, 0)
        Me.tblMetaData.Controls.Add(Me.chkMetaDataScanEnabled, 0, 0)
        Me.tblMetaData.Controls.Add(Me.lblDurationForRuntimeFormat, 1, 1)
        Me.tblMetaData.Controls.Add(Me.chkMetadataScanDurationForRuntimeEnabled, 0, 1)
        Me.tblMetaData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMetaData.Location = New System.Drawing.Point(3, 18)
        Me.tblMetaData.Name = "tblMetaData"
        Me.tblMetaData.RowCount = 4
        Me.tblMetaData.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMetaData.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMetaData.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMetaData.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMetaData.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMetaData.Size = New System.Drawing.Size(452, 142)
        Me.tblMetaData.TabIndex = 78
        '
        'txtMetadataScanDurationForRuntimeFormat
        '
        Me.txtMetadataScanDurationForRuntimeFormat.Location = New System.Drawing.Point(3, 49)
        Me.txtMetadataScanDurationForRuntimeFormat.Name = "txtMetadataScanDurationForRuntimeFormat"
        Me.txtMetadataScanDurationForRuntimeFormat.Size = New System.Drawing.Size(160, 22)
        Me.txtMetadataScanDurationForRuntimeFormat.TabIndex = 23
        '
        'gbMovieScraperDefFIExtOpts
        '
        Me.gbMovieScraperDefFIExtOpts.AutoSize = True
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.tblMovieScraperDefFIExtOpts)
        Me.gbMovieScraperDefFIExtOpts.Location = New System.Drawing.Point(257, 3)
        Me.gbMovieScraperDefFIExtOpts.Name = "gbMovieScraperDefFIExtOpts"
        Me.tblMetaData.SetRowSpan(Me.gbMovieScraperDefFIExtOpts, 4)
        Me.gbMovieScraperDefFIExtOpts.Size = New System.Drawing.Size(180, 136)
        Me.gbMovieScraperDefFIExtOpts.TabIndex = 8
        Me.gbMovieScraperDefFIExtOpts.TabStop = False
        Me.gbMovieScraperDefFIExtOpts.Text = "Defaults by File Type"
        '
        'tblMovieScraperDefFIExtOpts
        '
        Me.tblMovieScraperDefFIExtOpts.AutoSize = True
        Me.tblMovieScraperDefFIExtOpts.ColumnCount = 5
        Me.tblMovieScraperDefFIExtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDefFIExtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDefFIExtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDefFIExtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDefFIExtOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtRemove, 3, 2)
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.txtMovieScraperDefFIExt, 0, 2)
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtEdit, 2, 2)
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.lstMovieScraperDefFIExt, 0, 0)
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.btnMovieScraperDefFIExtAdd, 1, 2)
        Me.tblMovieScraperDefFIExtOpts.Controls.Add(Me.lblMovieScraperDefFIExt, 0, 1)
        Me.tblMovieScraperDefFIExtOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperDefFIExtOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperDefFIExtOpts.Name = "tblMovieScraperDefFIExtOpts"
        Me.tblMovieScraperDefFIExtOpts.RowCount = 4
        Me.tblMovieScraperDefFIExtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDefFIExtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieScraperDefFIExtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDefFIExtOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDefFIExtOpts.Size = New System.Drawing.Size(174, 115)
        Me.tblMovieScraperDefFIExtOpts.TabIndex = 78
        '
        'btnMovieScraperDefFIExtRemove
        '
        Me.btnMovieScraperDefFIExtRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieScraperDefFIExtRemove.Enabled = False
        Me.btnMovieScraperDefFIExtRemove.Image = CType(resources.GetObject("btnMovieScraperDefFIExtRemove.Image"), System.Drawing.Image)
        Me.btnMovieScraperDefFIExtRemove.Location = New System.Drawing.Point(148, 89)
        Me.btnMovieScraperDefFIExtRemove.Name = "btnMovieScraperDefFIExtRemove"
        Me.btnMovieScraperDefFIExtRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtRemove.TabIndex = 31
        Me.btnMovieScraperDefFIExtRemove.UseVisualStyleBackColor = True
        '
        'txtMovieScraperDefFIExt
        '
        Me.txtMovieScraperDefFIExt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieScraperDefFIExt.Location = New System.Drawing.Point(3, 89)
        Me.txtMovieScraperDefFIExt.Name = "txtMovieScraperDefFIExt"
        Me.txtMovieScraperDefFIExt.Size = New System.Drawing.Size(80, 22)
        Me.txtMovieScraperDefFIExt.TabIndex = 33
        '
        'btnMovieScraperDefFIExtEdit
        '
        Me.btnMovieScraperDefFIExtEdit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieScraperDefFIExtEdit.Enabled = False
        Me.btnMovieScraperDefFIExtEdit.Image = CType(resources.GetObject("btnMovieScraperDefFIExtEdit.Image"), System.Drawing.Image)
        Me.btnMovieScraperDefFIExtEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieScraperDefFIExtEdit.Location = New System.Drawing.Point(118, 89)
        Me.btnMovieScraperDefFIExtEdit.Name = "btnMovieScraperDefFIExtEdit"
        Me.btnMovieScraperDefFIExtEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtEdit.TabIndex = 30
        Me.btnMovieScraperDefFIExtEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieScraperDefFIExtEdit.UseVisualStyleBackColor = True
        '
        'lstMovieScraperDefFIExt
        '
        Me.tblMovieScraperDefFIExtOpts.SetColumnSpan(Me.lstMovieScraperDefFIExt, 4)
        Me.lstMovieScraperDefFIExt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMovieScraperDefFIExt.FormattingEnabled = True
        Me.lstMovieScraperDefFIExt.Location = New System.Drawing.Point(3, 3)
        Me.lstMovieScraperDefFIExt.Name = "lstMovieScraperDefFIExt"
        Me.lstMovieScraperDefFIExt.Size = New System.Drawing.Size(168, 60)
        Me.lstMovieScraperDefFIExt.TabIndex = 34
        '
        'btnMovieScraperDefFIExtAdd
        '
        Me.btnMovieScraperDefFIExtAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieScraperDefFIExtAdd.Enabled = False
        Me.btnMovieScraperDefFIExtAdd.Image = CType(resources.GetObject("btnMovieScraperDefFIExtAdd.Image"), System.Drawing.Image)
        Me.btnMovieScraperDefFIExtAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieScraperDefFIExtAdd.Location = New System.Drawing.Point(89, 89)
        Me.btnMovieScraperDefFIExtAdd.Name = "btnMovieScraperDefFIExtAdd"
        Me.btnMovieScraperDefFIExtAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieScraperDefFIExtAdd.TabIndex = 29
        Me.btnMovieScraperDefFIExtAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieScraperDefFIExtAdd.UseVisualStyleBackColor = True
        '
        'lblMovieScraperDefFIExt
        '
        Me.lblMovieScraperDefFIExt.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperDefFIExt.AutoSize = True
        Me.tblMovieScraperDefFIExtOpts.SetColumnSpan(Me.lblMovieScraperDefFIExt, 4)
        Me.lblMovieScraperDefFIExt.Location = New System.Drawing.Point(3, 69)
        Me.lblMovieScraperDefFIExt.Name = "lblMovieScraperDefFIExt"
        Me.lblMovieScraperDefFIExt.Size = New System.Drawing.Size(53, 13)
        Me.lblMovieScraperDefFIExt.TabIndex = 32
        Me.lblMovieScraperDefFIExt.Text = "File Type:"
        '
        'chkMetaDataScanEnabled
        '
        Me.chkMetaDataScanEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMetaDataScanEnabled.AutoSize = True
        Me.chkMetaDataScanEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkMetaDataScanEnabled.Name = "chkMetaDataScanEnabled"
        Me.chkMetaDataScanEnabled.Size = New System.Drawing.Size(102, 17)
        Me.chkMetaDataScanEnabled.TabIndex = 7
        Me.chkMetaDataScanEnabled.Text = "Scan Metadata"
        Me.chkMetaDataScanEnabled.UseVisualStyleBackColor = True
        '
        'lblDurationForRuntimeFormat
        '
        Me.lblDurationForRuntimeFormat.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDurationForRuntimeFormat.AutoSize = True
        Me.lblDurationForRuntimeFormat.Location = New System.Drawing.Point(169, 29)
        Me.lblDurationForRuntimeFormat.Name = "lblDurationForRuntimeFormat"
        Me.tblMetaData.SetRowSpan(Me.lblDurationForRuntimeFormat, 2)
        Me.lblDurationForRuntimeFormat.Size = New System.Drawing.Size(82, 39)
        Me.lblDurationForRuntimeFormat.TabIndex = 23
        Me.lblDurationForRuntimeFormat.Text = "<h>=Hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<m>=Minutes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<s>=Seconds"
        Me.lblDurationForRuntimeFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkMetadataScanDurationForRuntimeEnabled
        '
        Me.chkMetadataScanDurationForRuntimeEnabled.AutoSize = True
        Me.chkMetadataScanDurationForRuntimeEnabled.Location = New System.Drawing.Point(3, 26)
        Me.chkMetadataScanDurationForRuntimeEnabled.Name = "chkMetadataScanDurationForRuntimeEnabled"
        Me.chkMetadataScanDurationForRuntimeEnabled.Size = New System.Drawing.Size(158, 17)
        Me.chkMetadataScanDurationForRuntimeEnabled.TabIndex = 8
        Me.chkMetadataScanDurationForRuntimeEnabled.Text = "Use Duration for Runtime"
        Me.chkMetadataScanDurationForRuntimeEnabled.UseVisualStyleBackColor = True
        '
        'gbCollection
        '
        Me.gbCollection.AutoSize = True
        Me.gbCollection.Controls.Add(Me.tblCollection)
        Me.gbCollection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCollection.Location = New System.Drawing.Point(320, 435)
        Me.gbCollection.Name = "gbCollection"
        Me.gbCollection.Size = New System.Drawing.Size(458, 188)
        Me.gbCollection.TabIndex = 78
        Me.gbCollection.TabStop = False
        Me.gbCollection.Text = "Collection"
        '
        'tblCollection
        '
        Me.tblCollection.AutoSize = True
        Me.tblCollection.ColumnCount = 2
        Me.tblCollection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCollection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCollection.Controls.Add(Me.chkCollectionSaveYAMJCompatible, 0, 2)
        Me.tblCollection.Controls.Add(Me.chkCollectionAutoAddToCollection, 0, 0)
        Me.tblCollection.Controls.Add(Me.chkCollectionSaveExtendedInformation, 0, 1)
        Me.tblCollection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCollection.Location = New System.Drawing.Point(3, 18)
        Me.tblCollection.Name = "tblCollection"
        Me.tblCollection.RowCount = 4
        Me.tblCollection.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCollection.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCollection.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCollection.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCollection.Size = New System.Drawing.Size(452, 167)
        Me.tblCollection.TabIndex = 0
        '
        'chkCollectionSaveYAMJCompatible
        '
        Me.chkCollectionSaveYAMJCompatible.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCollectionSaveYAMJCompatible.AutoSize = True
        Me.tblCollection.SetColumnSpan(Me.chkCollectionSaveYAMJCompatible, 2)
        Me.chkCollectionSaveYAMJCompatible.Location = New System.Drawing.Point(3, 49)
        Me.chkCollectionSaveYAMJCompatible.Name = "chkCollectionSaveYAMJCompatible"
        Me.chkCollectionSaveYAMJCompatible.Size = New System.Drawing.Size(203, 17)
        Me.chkCollectionSaveYAMJCompatible.TabIndex = 2
        Me.chkCollectionSaveYAMJCompatible.Text = "Save YAMJ Compatible Sets to NFO"
        Me.chkCollectionSaveYAMJCompatible.UseVisualStyleBackColor = True
        '
        'chkCollectionAutoAddToCollection
        '
        Me.chkCollectionAutoAddToCollection.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCollectionAutoAddToCollection.AutoSize = True
        Me.chkCollectionAutoAddToCollection.Enabled = False
        Me.chkCollectionAutoAddToCollection.Location = New System.Drawing.Point(3, 3)
        Me.chkCollectionAutoAddToCollection.Name = "chkCollectionAutoAddToCollection"
        Me.chkCollectionAutoAddToCollection.Size = New System.Drawing.Size(226, 17)
        Me.chkCollectionAutoAddToCollection.TabIndex = 0
        Me.chkCollectionAutoAddToCollection.Text = "Add Movie automatically to Collections"
        Me.chkCollectionAutoAddToCollection.UseVisualStyleBackColor = True
        '
        'chkCollectionSaveExtendedInformation
        '
        Me.chkCollectionSaveExtendedInformation.AutoSize = True
        Me.chkCollectionSaveExtendedInformation.Location = New System.Drawing.Point(3, 26)
        Me.chkCollectionSaveExtendedInformation.Name = "chkCollectionSaveExtendedInformation"
        Me.chkCollectionSaveExtendedInformation.Size = New System.Drawing.Size(411, 17)
        Me.chkCollectionSaveExtendedInformation.TabIndex = 1
        Me.chkCollectionSaveExtendedInformation.Text = "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)"
        Me.chkCollectionSaveExtendedInformation.UseVisualStyleBackColor = True
        '
        'frmMovie_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(840, 712)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_Data"
        Me.Text = "frmMovie_Data"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFields.PerformLayout()
        Me.tblScraperFields.ResumeLayout(False)
        Me.tblScraperFields.PerformLayout()
        Me.gbMPAA.ResumeLayout(False)
        Me.gbMPAA.PerformLayout()
        Me.tblMPAA.ResumeLayout(False)
        Me.tblMPAA.PerformLayout()
        Me.gbMovieScraperMiscOpts.ResumeLayout(False)
        Me.gbMovieScraperMiscOpts.PerformLayout()
        Me.tblMovieScraperMiscOpts.ResumeLayout(False)
        Me.tblMovieScraperMiscOpts.PerformLayout()
        Me.gbMetadata.ResumeLayout(False)
        Me.gbMetadata.PerformLayout()
        Me.tblMetaData.ResumeLayout(False)
        Me.tblMetaData.PerformLayout()
        Me.gbMovieScraperDefFIExtOpts.ResumeLayout(False)
        Me.gbMovieScraperDefFIExtOpts.PerformLayout()
        Me.tblMovieScraperDefFIExtOpts.ResumeLayout(False)
        Me.tblMovieScraperDefFIExtOpts.PerformLayout()
        Me.gbCollection.ResumeLayout(False)
        Me.gbCollection.PerformLayout()
        Me.tblCollection.ResumeLayout(False)
        Me.tblCollection.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbScraperFields As GroupBox
    Friend WithEvents tblScraperFields As TableLayoutPanel
    Friend WithEvents lblScraperFieldsHeaderLocked As Label
    Friend WithEvents lblScraperFieldsHeaderLimit As Label
    Friend WithEvents cbCertificationsLimit As ComboBox
    Friend WithEvents chkRatingsLocked As CheckBox
    Friend WithEvents chkTitleLock As CheckBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblRatings As Label
    Friend WithEvents lblLanguageAudio As Label
    Friend WithEvents lblLanguageVideo As Label
    Friend WithEvents lblCollection As Label
    Friend WithEvents chkMetadataScanLockAudioLanguage As CheckBox
    Friend WithEvents chkMetadataScanLockVideoLanguage As CheckBox
    Friend WithEvents chkTitleEnabled As CheckBox
    Friend WithEvents chkRatingsEnabled As CheckBox
    Friend WithEvents chkCollectionEnabled As CheckBox
    Friend WithEvents chkCollectionLocked As CheckBox
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents chkOriginalTitleEnabled As CheckBox
    Friend WithEvents chkOriginalTitleLocked As CheckBox
    Friend WithEvents lblPremiered As Label
    Friend WithEvents chkPremieredEnabled As CheckBox
    Friend WithEvents chkPremieredLocked As CheckBox
    Friend WithEvents lblPlot As Label
    Friend WithEvents chkPlotEnabled As CheckBox
    Friend WithEvents chkPlotLocked As CheckBox
    Friend WithEvents lblOutline As Label
    Friend WithEvents chkOutlineEnabled As CheckBox
    Friend WithEvents chkOutlineLocked As CheckBox
    Friend WithEvents lblTagline As Label
    Friend WithEvents chkTaglineEnabled As CheckBox
    Friend WithEvents chkTaglineLocked As CheckBox
    Friend WithEvents lblTop250 As Label
    Friend WithEvents chkTop250Enabled As CheckBox
    Friend WithEvents chkTop250Locked As CheckBox
    Friend WithEvents lblMPAA As Label
    Friend WithEvents chkMPAAEnabled As CheckBox
    Friend WithEvents chkMPAALocked As CheckBox
    Friend WithEvents lblCertifications As Label
    Friend WithEvents chkCertificationsEnabled As CheckBox
    Friend WithEvents chkCertificationsLocked As CheckBox
    Friend WithEvents lblRuntime As Label
    Friend WithEvents chkRuntimeEnabled As CheckBox
    Friend WithEvents chkRuntimeLocked As CheckBox
    Friend WithEvents lblStudios As Label
    Friend WithEvents chkStudiosEnabled As CheckBox
    Friend WithEvents chkStudiosLocked As CheckBox
    Friend WithEvents txtStudiosLimit As TextBox
    Friend WithEvents lblTags As Label
    Friend WithEvents chkTagsEnabled As CheckBox
    Friend WithEvents chkTagsLocked As CheckBox
    Friend WithEvents lblTrailerLink As Label
    Friend WithEvents chkTrailerLinkEnabled As CheckBox
    Friend WithEvents chkTrailerLinkLocked As CheckBox
    Friend WithEvents lblGenres As Label
    Friend WithEvents chkGenresEnabled As CheckBox
    Friend WithEvents chkGenresLocked As CheckBox
    Friend WithEvents txtGenresLimit As TextBox
    Friend WithEvents lblActors As Label
    Friend WithEvents chkActorsEnabled As CheckBox
    Friend WithEvents chkActorsLocked As CheckBox
    Friend WithEvents txtActorsLimit As TextBox
    Friend WithEvents lblCountries As Label
    Friend WithEvents chkCountriesEnabled As CheckBox
    Friend WithEvents chkCountriesLocked As CheckBox
    Friend WithEvents lblDirectors As Label
    Friend WithEvents chkDirectorsEnabled As CheckBox
    Friend WithEvents chkDirectorsLocked As CheckBox
    Friend WithEvents lblCredits As Label
    Friend WithEvents chkCreditsEnabled As CheckBox
    Friend WithEvents chkCreditsLocked As CheckBox
    Friend WithEvents lblUserRating As Label
    Friend WithEvents chkUserRatingEnabled As CheckBox
    Friend WithEvents chkUserRatingLocked As CheckBox
    Friend WithEvents txtCountriesLimit As TextBox
    Friend WithEvents gbMPAA As GroupBox
    Friend WithEvents tblMPAA As TableLayoutPanel
    Friend WithEvents chkCertificationsOnlyValue As CheckBox
    Friend WithEvents chkCertificationsForMPAAFallback As CheckBox
    Friend WithEvents chkCertificationsForMPAA As CheckBox
    Friend WithEvents txtMPAANotRatedValue As TextBox
    Friend WithEvents lblMovieScraperMPAANotRated As Label
    Friend WithEvents gbMovieScraperMiscOpts As GroupBox
    Friend WithEvents tblMovieScraperMiscOpts As TableLayoutPanel
    Friend WithEvents chkCleanPlotAndOutline As CheckBox
    Friend WithEvents chkClearDisabledFields As CheckBox
    Friend WithEvents chkActorsWithImageOnly As CheckBox
    Friend WithEvents txtOutlineLimit As TextBox
    Friend WithEvents chkOutlineUsePlot As CheckBox
    Friend WithEvents chkTrailerLinkSaveKodiCompatible As CheckBox
    Friend WithEvents chkOutlineUsePlotAsFallback As CheckBox
    Friend WithEvents chkTitleUseOriginalTitle As CheckBox
    Friend WithEvents gbMetadata As GroupBox
    Friend WithEvents tblMetaData As TableLayoutPanel
    Friend WithEvents gbMovieScraperDefFIExtOpts As GroupBox
    Friend WithEvents tblMovieScraperDefFIExtOpts As TableLayoutPanel
    Friend WithEvents btnMovieScraperDefFIExtRemove As Button
    Friend WithEvents txtMovieScraperDefFIExt As TextBox
    Friend WithEvents btnMovieScraperDefFIExtEdit As Button
    Friend WithEvents lstMovieScraperDefFIExt As ListBox
    Friend WithEvents btnMovieScraperDefFIExtAdd As Button
    Friend WithEvents lblMovieScraperDefFIExt As Label
    Friend WithEvents chkMetaDataScanEnabled As CheckBox
    Friend WithEvents lblDurationForRuntimeFormat As Label
    Friend WithEvents chkMetadataScanDurationForRuntimeEnabled As CheckBox
    Friend WithEvents gbCollection As GroupBox
    Friend WithEvents tblCollection As TableLayoutPanel
    Friend WithEvents chkCollectionSaveYAMJCompatible As CheckBox
    Friend WithEvents chkCollectionAutoAddToCollection As CheckBox
    Friend WithEvents chkCollectionSaveExtendedInformation As CheckBox
    Friend WithEvents btnTagsWhitelist As Button
    Friend WithEvents lblPlotForOutline As Label
    Friend WithEvents lblPlotForOutlineAsFallback As Label
    Friend WithEvents lblOriginalTitleAsTitle As Label
    Friend WithEvents txtMetadataScanDurationForRuntimeFormat As TextBox
End Class
