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
        Me.gbMovieScraperGlobalOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperGlobalOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieLockCollectionID = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalHeaderLock = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalHeaderLimit = New System.Windows.Forms.Label()
        Me.cbMovieScraperCertLang = New System.Windows.Forms.ComboBox()
        Me.chkMovieLockRating = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTitle = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalTitle = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalRating = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalCollectionID = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalLanguageA = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalLanguageV = New System.Windows.Forms.Label()
        Me.lblMovieScraperGlobalCollections = New System.Windows.Forms.Label()
        Me.chkMovieLockLanguageA = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockLanguageV = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperRating = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCollectionID = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCollections = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalOriginalTitle = New System.Windows.Forms.Label()
        Me.chkMovieScraperOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalYear = New System.Windows.Forms.Label()
        Me.chkMovieScraperYear = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockYear = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalReleaseDate = New System.Windows.Forms.Label()
        Me.chkMovieScraperRelease = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockReleaseDate = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalPlot = New System.Windows.Forms.Label()
        Me.chkMovieScraperPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockPlot = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalOutline = New System.Windows.Forms.Label()
        Me.chkMovieScraperOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockOutline = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalTagline = New System.Windows.Forms.Label()
        Me.chkMovieScraperTagline = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTagline = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalTop250 = New System.Windows.Forms.Label()
        Me.chkMovieScraperTop250 = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTop250 = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalMPAA = New System.Windows.Forms.Label()
        Me.chkMovieScraperMPAA = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockMPAA = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalCertifications = New System.Windows.Forms.Label()
        Me.chkMovieScraperCert = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCert = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalRuntime = New System.Windows.Forms.Label()
        Me.chkMovieScraperRuntime = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockRuntime = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalStudios = New System.Windows.Forms.Label()
        Me.chkMovieScraperStudio = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockStudio = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperStudioLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperGlobalTags = New System.Windows.Forms.Label()
        Me.chkMovieScraperTags = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTags = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalTrailer = New System.Windows.Forms.Label()
        Me.chkMovieScraperTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockTrailer = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalGenres = New System.Windows.Forms.Label()
        Me.chkMovieScraperGenre = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockGenre = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperGenreLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperGlobalActors = New System.Windows.Forms.Label()
        Me.chkMovieScraperCast = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockActors = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperCastLimit = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperGlobalCountries = New System.Windows.Forms.Label()
        Me.chkMovieScraperCountry = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCountry = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalDirectors = New System.Windows.Forms.Label()
        Me.chkMovieScraperDirector = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockDirector = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalCredits = New System.Windows.Forms.Label()
        Me.chkMovieScraperCredits = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockCredits = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperGlobalUserRating = New System.Windows.Forms.Label()
        Me.chkMovieScraperUserRating = New System.Windows.Forms.CheckBox()
        Me.chkMovieLockUserRating = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperCountryLimit = New System.Windows.Forms.TextBox()
        Me.gbMovieScraperCertificationOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperCertificationOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieScraperCertOnlyValue = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCertForMPAAFallback = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCertForMPAA = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperMPAANotRated = New System.Windows.Forms.TextBox()
        Me.lblMovieScraperMPAANotRated = New System.Windows.Forms.Label()
        Me.gbMovieScraperMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieScraperCleanPlotOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCleanFields = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCastWithImg = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperDetailView = New System.Windows.Forms.CheckBox()
        Me.lblMovieScraperOutlineLimit = New System.Windows.Forms.Label()
        Me.txtMovieScraperOutlineLimit = New System.Windows.Forms.TextBox()
        Me.chkMovieScraperPlotForOutline = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperXBMCTrailerFormat = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperPlotForOutlineIfEmpty = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperOriginalTitleAsTitle = New System.Windows.Forms.CheckBox()
        Me.gbMovieScraperMetaDataOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperMetaDataOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieScraperDefFIExtOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperDefFIExtOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieScraperDefFIExtRemove = New System.Windows.Forms.Button()
        Me.txtMovieScraperDefFIExt = New System.Windows.Forms.TextBox()
        Me.btnMovieScraperDefFIExtEdit = New System.Windows.Forms.Button()
        Me.lstMovieScraperDefFIExt = New System.Windows.Forms.ListBox()
        Me.btnMovieScraperDefFIExtAdd = New System.Windows.Forms.Button()
        Me.lblMovieScraperDefFIExt = New System.Windows.Forms.Label()
        Me.chkMovieScraperMetaDataScan = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperMetaDataIFOScan = New System.Windows.Forms.CheckBox()
        Me.gbMovieScraperDurationFormatOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperDurationFormatOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMovieScraperDurationRuntimeFormat = New System.Windows.Forms.Label()
        Me.chkMovieScraperUseMDDuration = New System.Windows.Forms.CheckBox()
        Me.txtMovieScraperDurationRuntimeFormat = New System.Windows.Forms.TextBox()
        Me.gbMovieScraperCollectionOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieScraperCollectionOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieScraperCollectionsYAMJCompatibleSets = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCollectionsAuto = New System.Windows.Forms.CheckBox()
        Me.chkMovieScraperCollectionsExtendedInfo = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieScraperGlobalOpts.SuspendLayout()
        Me.tblMovieScraperGlobalOpts.SuspendLayout()
        Me.gbMovieScraperCertificationOpts.SuspendLayout()
        Me.tblMovieScraperCertificationOpts.SuspendLayout()
        Me.gbMovieScraperMiscOpts.SuspendLayout()
        Me.tblMovieScraperMiscOpts.SuspendLayout()
        Me.gbMovieScraperMetaDataOpts.SuspendLayout()
        Me.tblMovieScraperMetaDataOpts.SuspendLayout()
        Me.gbMovieScraperDefFIExtOpts.SuspendLayout()
        Me.tblMovieScraperDefFIExtOpts.SuspendLayout()
        Me.gbMovieScraperDurationFormatOpts.SuspendLayout()
        Me.tblMovieScraperDurationFormatOpts.SuspendLayout()
        Me.gbMovieScraperCollectionOpts.SuspendLayout()
        Me.tblMovieScraperCollectionOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(733, 656)
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
        Me.tblSettings.Controls.Add(Me.gbMovieScraperGlobalOpts, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbMovieScraperCertificationOpts, 1, 2)
        Me.tblSettings.Controls.Add(Me.gbMovieScraperMiscOpts, 1, 1)
        Me.tblSettings.Controls.Add(Me.gbMovieScraperMetaDataOpts, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbMovieScraperCollectionOpts, 1, 3)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 5
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(733, 656)
        Me.tblSettings.TabIndex = 69
        '
        'gbMovieScraperGlobalOpts
        '
        Me.gbMovieScraperGlobalOpts.AutoSize = True
        Me.gbMovieScraperGlobalOpts.Controls.Add(Me.tblMovieScraperGlobalOpts)
        Me.gbMovieScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperGlobalOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieScraperGlobalOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieScraperGlobalOpts.Name = "gbMovieScraperGlobalOpts"
        Me.tblSettings.SetRowSpan(Me.gbMovieScraperGlobalOpts, 4)
        Me.gbMovieScraperGlobalOpts.Size = New System.Drawing.Size(243, 599)
        Me.gbMovieScraperGlobalOpts.TabIndex = 1
        Me.gbMovieScraperGlobalOpts.TabStop = False
        Me.gbMovieScraperGlobalOpts.Text = "Scraper Fields - Global"
        '
        'tblMovieScraperGlobalOpts
        '
        Me.tblMovieScraperGlobalOpts.AutoScroll = True
        Me.tblMovieScraperGlobalOpts.AutoSize = True
        Me.tblMovieScraperGlobalOpts.ColumnCount = 5
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCollectionID, 2, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalHeaderLock, 2, 0)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalHeaderLimit, 3, 0)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.cbMovieScraperCertLang, 3, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockRating, 2, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTitle, 2, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalTitle, 0, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalRating, 0, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalCollectionID, 0, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalLanguageA, 0, 24)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalLanguageV, 0, 25)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalCollections, 0, 23)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockLanguageA, 2, 24)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockLanguageV, 2, 25)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTitle, 1, 1)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRating, 1, 8)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCollectionID, 1, 22)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCollections, 2, 23)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalOriginalTitle, 0, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperOriginalTitle, 1, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockOriginalTitle, 2, 2)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalYear, 0, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperYear, 1, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockYear, 2, 3)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalReleaseDate, 0, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRelease, 1, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockReleaseDate, 2, 4)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalPlot, 0, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperPlot, 1, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockPlot, 2, 5)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalOutline, 0, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperOutline, 1, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockOutline, 2, 6)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalTagline, 0, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTagline, 1, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTagline, 2, 7)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalTop250, 0, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTop250, 1, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTop250, 2, 10)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalMPAA, 0, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperMPAA, 1, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockMPAA, 2, 11)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalCertifications, 0, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCert, 1, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCert, 2, 12)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalRuntime, 0, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperRuntime, 1, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockRuntime, 2, 13)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalStudios, 0, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperStudio, 1, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockStudio, 2, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperStudioLimit, 3, 21)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalTags, 0, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTags, 1, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTags, 2, 14)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalTrailer, 0, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperTrailer, 1, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockTrailer, 2, 15)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalGenres, 0, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperGenre, 1, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockGenre, 2, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperGenreLimit, 3, 16)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalActors, 0, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCast, 1, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockActors, 2, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperCastLimit, 3, 17)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalCountries, 0, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCountry, 1, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCountry, 2, 20)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalDirectors, 0, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperDirector, 1, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockDirector, 2, 18)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalCredits, 0, 19)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperCredits, 1, 19)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockCredits, 2, 19)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.lblMovieScraperGlobalUserRating, 0, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieScraperUserRating, 1, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.chkMovieLockUserRating, 2, 9)
        Me.tblMovieScraperGlobalOpts.Controls.Add(Me.txtMovieScraperCountryLimit, 3, 20)
        Me.tblMovieScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperGlobalOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperGlobalOpts.Name = "tblMovieScraperGlobalOpts"
        Me.tblMovieScraperGlobalOpts.RowCount = 27
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperGlobalOpts.Size = New System.Drawing.Size(237, 578)
        Me.tblMovieScraperGlobalOpts.TabIndex = 0
        '
        'chkMovieLockCollectionID
        '
        Me.chkMovieLockCollectionID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCollectionID.AutoSize = True
        Me.chkMovieLockCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockCollectionID.Location = New System.Drawing.Point(135, 485)
        Me.chkMovieLockCollectionID.Name = "chkMovieLockCollectionID"
        Me.chkMovieLockCollectionID.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCollectionID.TabIndex = 5
        Me.chkMovieLockCollectionID.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalHeaderLock
        '
        Me.lblMovieScraperGlobalHeaderLock.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMovieScraperGlobalHeaderLock.AutoSize = True
        Me.lblMovieScraperGlobalHeaderLock.Location = New System.Drawing.Point(127, 3)
        Me.lblMovieScraperGlobalHeaderLock.Name = "lblMovieScraperGlobalHeaderLock"
        Me.lblMovieScraperGlobalHeaderLock.Size = New System.Drawing.Size(31, 13)
        Me.lblMovieScraperGlobalHeaderLock.TabIndex = 12
        Me.lblMovieScraperGlobalHeaderLock.Text = "Lock"
        '
        'lblMovieScraperGlobalHeaderLimit
        '
        Me.lblMovieScraperGlobalHeaderLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMovieScraperGlobalHeaderLimit.AutoSize = True
        Me.lblMovieScraperGlobalHeaderLimit.Location = New System.Drawing.Point(182, 3)
        Me.lblMovieScraperGlobalHeaderLimit.Name = "lblMovieScraperGlobalHeaderLimit"
        Me.lblMovieScraperGlobalHeaderLimit.Size = New System.Drawing.Size(33, 13)
        Me.lblMovieScraperGlobalHeaderLimit.TabIndex = 14
        Me.lblMovieScraperGlobalHeaderLimit.Text = "Limit"
        '
        'cbMovieScraperCertLang
        '
        Me.cbMovieScraperCertLang.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbMovieScraperCertLang.DropDownHeight = 200
        Me.cbMovieScraperCertLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieScraperCertLang.DropDownWidth = 110
        Me.cbMovieScraperCertLang.Enabled = False
        Me.cbMovieScraperCertLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieScraperCertLang.FormattingEnabled = True
        Me.cbMovieScraperCertLang.IntegralHeight = False
        Me.cbMovieScraperCertLang.Items.AddRange(New Object() {"Argentina", "Australia", "Belgium", "Brazil", "Canada", "Finland", "France", "Germany", "Hong Kong", "Hungary", "Iceland", "Ireland", "Netherlands", "New Zealand", "Peru", "Poland", "Portugal", "Serbia", "Singapore", "South Korea", "Spain", "Sweden", "Switzerland", "Turkey", "UK", "USA"})
        Me.cbMovieScraperCertLang.Location = New System.Drawing.Point(164, 246)
        Me.cbMovieScraperCertLang.Name = "cbMovieScraperCertLang"
        Me.cbMovieScraperCertLang.Size = New System.Drawing.Size(70, 21)
        Me.cbMovieScraperCertLang.Sorted = True
        Me.cbMovieScraperCertLang.TabIndex = 5
        '
        'chkMovieLockRating
        '
        Me.chkMovieLockRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockRating.AutoSize = True
        Me.chkMovieLockRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockRating.Location = New System.Drawing.Point(135, 166)
        Me.chkMovieLockRating.Name = "chkMovieLockRating"
        Me.chkMovieLockRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockRating.TabIndex = 4
        Me.chkMovieLockRating.UseVisualStyleBackColor = True
        '
        'chkMovieLockTitle
        '
        Me.chkMovieLockTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockTitle.Location = New System.Drawing.Point(135, 23)
        Me.chkMovieLockTitle.Name = "chkMovieLockTitle"
        Me.chkMovieLockTitle.Size = New System.Drawing.Size(14, 17)
        Me.chkMovieLockTitle.TabIndex = 3
        Me.chkMovieLockTitle.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalTitle
        '
        Me.lblMovieScraperGlobalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalTitle.AutoSize = True
        Me.lblMovieScraperGlobalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalTitle.Location = New System.Drawing.Point(3, 25)
        Me.lblMovieScraperGlobalTitle.Name = "lblMovieScraperGlobalTitle"
        Me.lblMovieScraperGlobalTitle.Size = New System.Drawing.Size(28, 13)
        Me.lblMovieScraperGlobalTitle.TabIndex = 67
        Me.lblMovieScraperGlobalTitle.Text = "Title"
        '
        'lblMovieScraperGlobalRating
        '
        Me.lblMovieScraperGlobalRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalRating.AutoSize = True
        Me.lblMovieScraperGlobalRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalRating.Location = New System.Drawing.Point(3, 166)
        Me.lblMovieScraperGlobalRating.Name = "lblMovieScraperGlobalRating"
        Me.lblMovieScraperGlobalRating.Size = New System.Drawing.Size(41, 13)
        Me.lblMovieScraperGlobalRating.TabIndex = 68
        Me.lblMovieScraperGlobalRating.Text = "Rating"
        '
        'lblMovieScraperGlobalCollectionID
        '
        Me.lblMovieScraperGlobalCollectionID.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalCollectionID.AutoSize = True
        Me.lblMovieScraperGlobalCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalCollectionID.Location = New System.Drawing.Point(3, 485)
        Me.lblMovieScraperGlobalCollectionID.Name = "lblMovieScraperGlobalCollectionID"
        Me.lblMovieScraperGlobalCollectionID.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperGlobalCollectionID.TabIndex = 68
        Me.lblMovieScraperGlobalCollectionID.Text = "Collection ID"
        '
        'lblMovieScraperGlobalLanguageA
        '
        Me.lblMovieScraperGlobalLanguageA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalLanguageA.AutoSize = True
        Me.lblMovieScraperGlobalLanguageA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalLanguageA.Location = New System.Drawing.Point(3, 525)
        Me.lblMovieScraperGlobalLanguageA.Name = "lblMovieScraperGlobalLanguageA"
        Me.lblMovieScraperGlobalLanguageA.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieScraperGlobalLanguageA.TabIndex = 68
        Me.lblMovieScraperGlobalLanguageA.Text = "Language (audio)"
        '
        'lblMovieScraperGlobalLanguageV
        '
        Me.lblMovieScraperGlobalLanguageV.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalLanguageV.AutoSize = True
        Me.lblMovieScraperGlobalLanguageV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalLanguageV.Location = New System.Drawing.Point(3, 545)
        Me.lblMovieScraperGlobalLanguageV.Name = "lblMovieScraperGlobalLanguageV"
        Me.lblMovieScraperGlobalLanguageV.Size = New System.Drawing.Size(95, 13)
        Me.lblMovieScraperGlobalLanguageV.TabIndex = 68
        Me.lblMovieScraperGlobalLanguageV.Text = "Language (video)"
        '
        'lblMovieScraperGlobalCollections
        '
        Me.lblMovieScraperGlobalCollections.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalCollections.AutoSize = True
        Me.lblMovieScraperGlobalCollections.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalCollections.Location = New System.Drawing.Point(3, 505)
        Me.lblMovieScraperGlobalCollections.Name = "lblMovieScraperGlobalCollections"
        Me.lblMovieScraperGlobalCollections.Size = New System.Drawing.Size(64, 13)
        Me.lblMovieScraperGlobalCollections.TabIndex = 68
        Me.lblMovieScraperGlobalCollections.Text = "Collections"
        '
        'chkMovieLockLanguageA
        '
        Me.chkMovieLockLanguageA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockLanguageA.AutoSize = True
        Me.chkMovieLockLanguageA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockLanguageA.Location = New System.Drawing.Point(135, 525)
        Me.chkMovieLockLanguageA.Name = "chkMovieLockLanguageA"
        Me.chkMovieLockLanguageA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockLanguageA.TabIndex = 48
        Me.chkMovieLockLanguageA.UseVisualStyleBackColor = True
        '
        'chkMovieLockLanguageV
        '
        Me.chkMovieLockLanguageV.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockLanguageV.AutoSize = True
        Me.chkMovieLockLanguageV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockLanguageV.Location = New System.Drawing.Point(135, 545)
        Me.chkMovieLockLanguageV.Name = "chkMovieLockLanguageV"
        Me.chkMovieLockLanguageV.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockLanguageV.TabIndex = 47
        Me.chkMovieLockLanguageV.UseVisualStyleBackColor = True
        '
        'chkMovieScraperTitle
        '
        Me.chkMovieScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTitle.AutoSize = True
        Me.chkMovieScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperTitle.Location = New System.Drawing.Point(106, 24)
        Me.chkMovieScraperTitle.Name = "chkMovieScraperTitle"
        Me.chkMovieScraperTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTitle.TabIndex = 0
        Me.chkMovieScraperTitle.UseVisualStyleBackColor = True
        '
        'chkMovieScraperRating
        '
        Me.chkMovieScraperRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRating.AutoSize = True
        Me.chkMovieScraperRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperRating.Location = New System.Drawing.Point(106, 166)
        Me.chkMovieScraperRating.Name = "chkMovieScraperRating"
        Me.chkMovieScraperRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRating.TabIndex = 4
        Me.chkMovieScraperRating.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCollectionID
        '
        Me.chkMovieScraperCollectionID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCollectionID.AutoSize = True
        Me.chkMovieScraperCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCollectionID.Location = New System.Drawing.Point(106, 485)
        Me.chkMovieScraperCollectionID.Name = "chkMovieScraperCollectionID"
        Me.chkMovieScraperCollectionID.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCollectionID.TabIndex = 26
        Me.chkMovieScraperCollectionID.UseVisualStyleBackColor = True
        '
        'chkMovieLockCollections
        '
        Me.chkMovieLockCollections.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCollections.AutoSize = True
        Me.chkMovieLockCollections.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockCollections.Location = New System.Drawing.Point(135, 505)
        Me.chkMovieLockCollections.Name = "chkMovieLockCollections"
        Me.chkMovieLockCollections.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCollections.TabIndex = 66
        Me.chkMovieLockCollections.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalOriginalTitle
        '
        Me.lblMovieScraperGlobalOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalOriginalTitle.AutoSize = True
        Me.lblMovieScraperGlobalOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalOriginalTitle.Location = New System.Drawing.Point(3, 46)
        Me.lblMovieScraperGlobalOriginalTitle.Name = "lblMovieScraperGlobalOriginalTitle"
        Me.lblMovieScraperGlobalOriginalTitle.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperGlobalOriginalTitle.TabIndex = 68
        Me.lblMovieScraperGlobalOriginalTitle.Text = "Original Title"
        '
        'chkMovieScraperOriginalTitle
        '
        Me.chkMovieScraperOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperOriginalTitle.AutoSize = True
        Me.chkMovieScraperOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperOriginalTitle.Location = New System.Drawing.Point(106, 46)
        Me.chkMovieScraperOriginalTitle.Name = "chkMovieScraperOriginalTitle"
        Me.chkMovieScraperOriginalTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperOriginalTitle.TabIndex = 29
        Me.chkMovieScraperOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkMovieLockOriginalTitle
        '
        Me.chkMovieLockOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockOriginalTitle.AutoSize = True
        Me.chkMovieLockOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockOriginalTitle.Location = New System.Drawing.Point(135, 46)
        Me.chkMovieLockOriginalTitle.Name = "chkMovieLockOriginalTitle"
        Me.chkMovieLockOriginalTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockOriginalTitle.TabIndex = 65
        Me.chkMovieLockOriginalTitle.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalYear
        '
        Me.lblMovieScraperGlobalYear.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalYear.AutoSize = True
        Me.lblMovieScraperGlobalYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalYear.Location = New System.Drawing.Point(3, 66)
        Me.lblMovieScraperGlobalYear.Name = "lblMovieScraperGlobalYear"
        Me.lblMovieScraperGlobalYear.Size = New System.Drawing.Size(27, 13)
        Me.lblMovieScraperGlobalYear.TabIndex = 68
        Me.lblMovieScraperGlobalYear.Text = "Year"
        '
        'chkMovieScraperYear
        '
        Me.chkMovieScraperYear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperYear.AutoSize = True
        Me.chkMovieScraperYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperYear.Location = New System.Drawing.Point(106, 66)
        Me.chkMovieScraperYear.Name = "chkMovieScraperYear"
        Me.chkMovieScraperYear.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperYear.TabIndex = 1
        Me.chkMovieScraperYear.UseVisualStyleBackColor = True
        '
        'chkMovieLockYear
        '
        Me.chkMovieLockYear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockYear.AutoSize = True
        Me.chkMovieLockYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockYear.Location = New System.Drawing.Point(135, 66)
        Me.chkMovieLockYear.Name = "chkMovieLockYear"
        Me.chkMovieLockYear.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockYear.TabIndex = 52
        Me.chkMovieLockYear.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalReleaseDate
        '
        Me.lblMovieScraperGlobalReleaseDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalReleaseDate.AutoSize = True
        Me.lblMovieScraperGlobalReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalReleaseDate.Location = New System.Drawing.Point(3, 86)
        Me.lblMovieScraperGlobalReleaseDate.Name = "lblMovieScraperGlobalReleaseDate"
        Me.lblMovieScraperGlobalReleaseDate.Size = New System.Drawing.Size(73, 13)
        Me.lblMovieScraperGlobalReleaseDate.TabIndex = 68
        Me.lblMovieScraperGlobalReleaseDate.Text = "Release Date"
        '
        'chkMovieScraperRelease
        '
        Me.chkMovieScraperRelease.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRelease.AutoSize = True
        Me.chkMovieScraperRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperRelease.Location = New System.Drawing.Point(106, 86)
        Me.chkMovieScraperRelease.Name = "chkMovieScraperRelease"
        Me.chkMovieScraperRelease.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRelease.TabIndex = 3
        Me.chkMovieScraperRelease.UseVisualStyleBackColor = True
        '
        'chkMovieLockReleaseDate
        '
        Me.chkMovieLockReleaseDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockReleaseDate.AutoSize = True
        Me.chkMovieLockReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockReleaseDate.Location = New System.Drawing.Point(135, 86)
        Me.chkMovieLockReleaseDate.Name = "chkMovieLockReleaseDate"
        Me.chkMovieLockReleaseDate.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockReleaseDate.TabIndex = 55
        Me.chkMovieLockReleaseDate.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalPlot
        '
        Me.lblMovieScraperGlobalPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalPlot.AutoSize = True
        Me.lblMovieScraperGlobalPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalPlot.Location = New System.Drawing.Point(3, 106)
        Me.lblMovieScraperGlobalPlot.Name = "lblMovieScraperGlobalPlot"
        Me.lblMovieScraperGlobalPlot.Size = New System.Drawing.Size(27, 13)
        Me.lblMovieScraperGlobalPlot.TabIndex = 68
        Me.lblMovieScraperGlobalPlot.Text = "Plot"
        '
        'chkMovieScraperPlot
        '
        Me.chkMovieScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperPlot.AutoSize = True
        Me.chkMovieScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperPlot.Location = New System.Drawing.Point(106, 106)
        Me.chkMovieScraperPlot.Name = "chkMovieScraperPlot"
        Me.chkMovieScraperPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperPlot.TabIndex = 12
        Me.chkMovieScraperPlot.UseVisualStyleBackColor = True
        '
        'chkMovieLockPlot
        '
        Me.chkMovieLockPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockPlot.AutoSize = True
        Me.chkMovieLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockPlot.Location = New System.Drawing.Point(135, 106)
        Me.chkMovieLockPlot.Name = "chkMovieLockPlot"
        Me.chkMovieLockPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockPlot.TabIndex = 0
        Me.chkMovieLockPlot.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalOutline
        '
        Me.lblMovieScraperGlobalOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalOutline.AutoSize = True
        Me.lblMovieScraperGlobalOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalOutline.Location = New System.Drawing.Point(3, 126)
        Me.lblMovieScraperGlobalOutline.Name = "lblMovieScraperGlobalOutline"
        Me.lblMovieScraperGlobalOutline.Size = New System.Drawing.Size(46, 13)
        Me.lblMovieScraperGlobalOutline.TabIndex = 68
        Me.lblMovieScraperGlobalOutline.Text = "Outline"
        '
        'chkMovieScraperOutline
        '
        Me.chkMovieScraperOutline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperOutline.AutoSize = True
        Me.chkMovieScraperOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperOutline.Location = New System.Drawing.Point(106, 126)
        Me.chkMovieScraperOutline.Name = "chkMovieScraperOutline"
        Me.chkMovieScraperOutline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperOutline.TabIndex = 11
        Me.chkMovieScraperOutline.UseVisualStyleBackColor = True
        '
        'chkMovieLockOutline
        '
        Me.chkMovieLockOutline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockOutline.AutoSize = True
        Me.chkMovieLockOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockOutline.Location = New System.Drawing.Point(135, 126)
        Me.chkMovieLockOutline.Name = "chkMovieLockOutline"
        Me.chkMovieLockOutline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockOutline.TabIndex = 1
        Me.chkMovieLockOutline.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalTagline
        '
        Me.lblMovieScraperGlobalTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalTagline.AutoSize = True
        Me.lblMovieScraperGlobalTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalTagline.Location = New System.Drawing.Point(3, 146)
        Me.lblMovieScraperGlobalTagline.Name = "lblMovieScraperGlobalTagline"
        Me.lblMovieScraperGlobalTagline.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieScraperGlobalTagline.TabIndex = 68
        Me.lblMovieScraperGlobalTagline.Text = "Tagline"
        '
        'chkMovieScraperTagline
        '
        Me.chkMovieScraperTagline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTagline.AutoSize = True
        Me.chkMovieScraperTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperTagline.Location = New System.Drawing.Point(106, 146)
        Me.chkMovieScraperTagline.Name = "chkMovieScraperTagline"
        Me.chkMovieScraperTagline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTagline.TabIndex = 8
        Me.chkMovieScraperTagline.UseVisualStyleBackColor = True
        '
        'chkMovieLockTagline
        '
        Me.chkMovieLockTagline.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTagline.AutoSize = True
        Me.chkMovieLockTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockTagline.Location = New System.Drawing.Point(135, 146)
        Me.chkMovieLockTagline.Name = "chkMovieLockTagline"
        Me.chkMovieLockTagline.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTagline.TabIndex = 3
        Me.chkMovieLockTagline.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalTop250
        '
        Me.lblMovieScraperGlobalTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalTop250.AutoSize = True
        Me.lblMovieScraperGlobalTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalTop250.Location = New System.Drawing.Point(3, 206)
        Me.lblMovieScraperGlobalTop250.Name = "lblMovieScraperGlobalTop250"
        Me.lblMovieScraperGlobalTop250.Size = New System.Drawing.Size(46, 13)
        Me.lblMovieScraperGlobalTop250.TabIndex = 68
        Me.lblMovieScraperGlobalTop250.Text = "Top 250"
        '
        'chkMovieScraperTop250
        '
        Me.chkMovieScraperTop250.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTop250.AutoSize = True
        Me.chkMovieScraperTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperTop250.Location = New System.Drawing.Point(106, 206)
        Me.chkMovieScraperTop250.Name = "chkMovieScraperTop250"
        Me.chkMovieScraperTop250.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTop250.TabIndex = 23
        Me.chkMovieScraperTop250.UseVisualStyleBackColor = True
        '
        'chkMovieLockTop250
        '
        Me.chkMovieLockTop250.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTop250.AutoSize = True
        Me.chkMovieLockTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockTop250.Location = New System.Drawing.Point(135, 206)
        Me.chkMovieLockTop250.Name = "chkMovieLockTop250"
        Me.chkMovieLockTop250.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTop250.TabIndex = 61
        Me.chkMovieLockTop250.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalMPAA
        '
        Me.lblMovieScraperGlobalMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalMPAA.AutoSize = True
        Me.lblMovieScraperGlobalMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalMPAA.Location = New System.Drawing.Point(3, 226)
        Me.lblMovieScraperGlobalMPAA.Name = "lblMovieScraperGlobalMPAA"
        Me.lblMovieScraperGlobalMPAA.Size = New System.Drawing.Size(36, 13)
        Me.lblMovieScraperGlobalMPAA.TabIndex = 68
        Me.lblMovieScraperGlobalMPAA.Text = "MPAA"
        '
        'chkMovieScraperMPAA
        '
        Me.chkMovieScraperMPAA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperMPAA.AutoSize = True
        Me.chkMovieScraperMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperMPAA.Location = New System.Drawing.Point(106, 226)
        Me.chkMovieScraperMPAA.Name = "chkMovieScraperMPAA"
        Me.chkMovieScraperMPAA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperMPAA.TabIndex = 24
        Me.chkMovieScraperMPAA.UseVisualStyleBackColor = True
        '
        'chkMovieLockMPAA
        '
        Me.chkMovieLockMPAA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockMPAA.AutoSize = True
        Me.chkMovieLockMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockMPAA.Location = New System.Drawing.Point(135, 226)
        Me.chkMovieLockMPAA.Name = "chkMovieLockMPAA"
        Me.chkMovieLockMPAA.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockMPAA.TabIndex = 49
        Me.chkMovieLockMPAA.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalCertifications
        '
        Me.lblMovieScraperGlobalCertifications.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalCertifications.AutoSize = True
        Me.lblMovieScraperGlobalCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalCertifications.Location = New System.Drawing.Point(3, 250)
        Me.lblMovieScraperGlobalCertifications.Name = "lblMovieScraperGlobalCertifications"
        Me.lblMovieScraperGlobalCertifications.Size = New System.Drawing.Size(75, 13)
        Me.lblMovieScraperGlobalCertifications.TabIndex = 68
        Me.lblMovieScraperGlobalCertifications.Text = "Certifications"
        '
        'chkMovieScraperCert
        '
        Me.chkMovieScraperCert.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCert.AutoSize = True
        Me.chkMovieScraperCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCert.Location = New System.Drawing.Point(106, 249)
        Me.chkMovieScraperCert.Name = "chkMovieScraperCert"
        Me.chkMovieScraperCert.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCert.TabIndex = 24
        Me.chkMovieScraperCert.UseVisualStyleBackColor = True
        '
        'chkMovieLockCert
        '
        Me.chkMovieLockCert.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCert.AutoSize = True
        Me.chkMovieLockCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockCert.Location = New System.Drawing.Point(135, 249)
        Me.chkMovieLockCert.Name = "chkMovieLockCert"
        Me.chkMovieLockCert.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCert.TabIndex = 49
        Me.chkMovieLockCert.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalRuntime
        '
        Me.lblMovieScraperGlobalRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalRuntime.AutoSize = True
        Me.lblMovieScraperGlobalRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalRuntime.Location = New System.Drawing.Point(3, 273)
        Me.lblMovieScraperGlobalRuntime.Name = "lblMovieScraperGlobalRuntime"
        Me.lblMovieScraperGlobalRuntime.Size = New System.Drawing.Size(50, 13)
        Me.lblMovieScraperGlobalRuntime.TabIndex = 68
        Me.lblMovieScraperGlobalRuntime.Text = "Runtime"
        '
        'chkMovieScraperRuntime
        '
        Me.chkMovieScraperRuntime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperRuntime.AutoSize = True
        Me.chkMovieScraperRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperRuntime.Location = New System.Drawing.Point(106, 273)
        Me.chkMovieScraperRuntime.Name = "chkMovieScraperRuntime"
        Me.chkMovieScraperRuntime.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperRuntime.TabIndex = 13
        Me.chkMovieScraperRuntime.UseVisualStyleBackColor = True
        '
        'chkMovieLockRuntime
        '
        Me.chkMovieLockRuntime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockRuntime.AutoSize = True
        Me.chkMovieLockRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockRuntime.Location = New System.Drawing.Point(135, 273)
        Me.chkMovieLockRuntime.Name = "chkMovieLockRuntime"
        Me.chkMovieLockRuntime.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockRuntime.TabIndex = 51
        Me.chkMovieLockRuntime.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalStudios
        '
        Me.lblMovieScraperGlobalStudios.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalStudios.AutoSize = True
        Me.lblMovieScraperGlobalStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalStudios.Location = New System.Drawing.Point(3, 461)
        Me.lblMovieScraperGlobalStudios.Name = "lblMovieScraperGlobalStudios"
        Me.lblMovieScraperGlobalStudios.Size = New System.Drawing.Size(46, 13)
        Me.lblMovieScraperGlobalStudios.TabIndex = 68
        Me.lblMovieScraperGlobalStudios.Text = "Studios"
        '
        'chkMovieScraperStudio
        '
        Me.chkMovieScraperStudio.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperStudio.AutoSize = True
        Me.chkMovieScraperStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperStudio.Location = New System.Drawing.Point(106, 461)
        Me.chkMovieScraperStudio.Name = "chkMovieScraperStudio"
        Me.chkMovieScraperStudio.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperStudio.TabIndex = 14
        Me.chkMovieScraperStudio.UseVisualStyleBackColor = True
        '
        'chkMovieLockStudio
        '
        Me.chkMovieLockStudio.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockStudio.AutoSize = True
        Me.chkMovieLockStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockStudio.Location = New System.Drawing.Point(135, 461)
        Me.chkMovieLockStudio.Name = "chkMovieLockStudio"
        Me.chkMovieLockStudio.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockStudio.TabIndex = 54
        Me.chkMovieLockStudio.UseVisualStyleBackColor = True
        '
        'txtMovieScraperStudioLimit
        '
        Me.txtMovieScraperStudioLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperStudioLimit.Enabled = False
        Me.txtMovieScraperStudioLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperStudioLimit.Location = New System.Drawing.Point(179, 457)
        Me.txtMovieScraperStudioLimit.Name = "txtMovieScraperStudioLimit"
        Me.txtMovieScraperStudioLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperStudioLimit.TabIndex = 30
        '
        'lblMovieScraperGlobalTags
        '
        Me.lblMovieScraperGlobalTags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalTags.AutoSize = True
        Me.lblMovieScraperGlobalTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalTags.Location = New System.Drawing.Point(3, 293)
        Me.lblMovieScraperGlobalTags.Name = "lblMovieScraperGlobalTags"
        Me.lblMovieScraperGlobalTags.Size = New System.Drawing.Size(29, 13)
        Me.lblMovieScraperGlobalTags.TabIndex = 68
        Me.lblMovieScraperGlobalTags.Text = "Tags"
        Me.lblMovieScraperGlobalTags.Visible = False
        '
        'chkMovieScraperTags
        '
        Me.chkMovieScraperTags.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTags.AutoSize = True
        Me.chkMovieScraperTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperTags.Location = New System.Drawing.Point(106, 293)
        Me.chkMovieScraperTags.Name = "chkMovieScraperTags"
        Me.chkMovieScraperTags.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTags.TabIndex = 27
        Me.chkMovieScraperTags.UseVisualStyleBackColor = True
        Me.chkMovieScraperTags.Visible = False
        '
        'chkMovieLockTags
        '
        Me.chkMovieLockTags.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTags.AutoSize = True
        Me.chkMovieLockTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockTags.Location = New System.Drawing.Point(135, 293)
        Me.chkMovieLockTags.Name = "chkMovieLockTags"
        Me.chkMovieLockTags.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTags.TabIndex = 64
        Me.chkMovieLockTags.UseVisualStyleBackColor = True
        Me.chkMovieLockTags.Visible = False
        '
        'lblMovieScraperGlobalTrailer
        '
        Me.lblMovieScraperGlobalTrailer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalTrailer.AutoSize = True
        Me.lblMovieScraperGlobalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalTrailer.Location = New System.Drawing.Point(3, 313)
        Me.lblMovieScraperGlobalTrailer.Name = "lblMovieScraperGlobalTrailer"
        Me.lblMovieScraperGlobalTrailer.Size = New System.Drawing.Size(37, 13)
        Me.lblMovieScraperGlobalTrailer.TabIndex = 68
        Me.lblMovieScraperGlobalTrailer.Text = "Trailer"
        '
        'chkMovieScraperTrailer
        '
        Me.chkMovieScraperTrailer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperTrailer.AutoSize = True
        Me.chkMovieScraperTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperTrailer.Location = New System.Drawing.Point(106, 313)
        Me.chkMovieScraperTrailer.Name = "chkMovieScraperTrailer"
        Me.chkMovieScraperTrailer.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperTrailer.TabIndex = 5
        Me.chkMovieScraperTrailer.UseVisualStyleBackColor = True
        '
        'chkMovieLockTrailer
        '
        Me.chkMovieLockTrailer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockTrailer.AutoSize = True
        Me.chkMovieLockTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockTrailer.Location = New System.Drawing.Point(135, 313)
        Me.chkMovieLockTrailer.Name = "chkMovieLockTrailer"
        Me.chkMovieLockTrailer.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockTrailer.TabIndex = 46
        Me.chkMovieLockTrailer.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalGenres
        '
        Me.lblMovieScraperGlobalGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalGenres.AutoSize = True
        Me.lblMovieScraperGlobalGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalGenres.Location = New System.Drawing.Point(3, 337)
        Me.lblMovieScraperGlobalGenres.Name = "lblMovieScraperGlobalGenres"
        Me.lblMovieScraperGlobalGenres.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieScraperGlobalGenres.TabIndex = 68
        Me.lblMovieScraperGlobalGenres.Text = "Genres"
        '
        'chkMovieScraperGenre
        '
        Me.chkMovieScraperGenre.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperGenre.AutoSize = True
        Me.chkMovieScraperGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperGenre.Location = New System.Drawing.Point(106, 337)
        Me.chkMovieScraperGenre.Name = "chkMovieScraperGenre"
        Me.chkMovieScraperGenre.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperGenre.TabIndex = 10
        Me.chkMovieScraperGenre.UseVisualStyleBackColor = True
        '
        'chkMovieLockGenre
        '
        Me.chkMovieLockGenre.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockGenre.AutoSize = True
        Me.chkMovieLockGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockGenre.Location = New System.Drawing.Point(135, 337)
        Me.chkMovieLockGenre.Name = "chkMovieLockGenre"
        Me.chkMovieLockGenre.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockGenre.TabIndex = 7
        Me.chkMovieLockGenre.UseVisualStyleBackColor = True
        '
        'txtMovieScraperGenreLimit
        '
        Me.txtMovieScraperGenreLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperGenreLimit.Enabled = False
        Me.txtMovieScraperGenreLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperGenreLimit.Location = New System.Drawing.Point(179, 333)
        Me.txtMovieScraperGenreLimit.Name = "txtMovieScraperGenreLimit"
        Me.txtMovieScraperGenreLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperGenreLimit.TabIndex = 21
        '
        'lblMovieScraperGlobalActors
        '
        Me.lblMovieScraperGlobalActors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalActors.AutoSize = True
        Me.lblMovieScraperGlobalActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalActors.Location = New System.Drawing.Point(3, 365)
        Me.lblMovieScraperGlobalActors.Name = "lblMovieScraperGlobalActors"
        Me.lblMovieScraperGlobalActors.Size = New System.Drawing.Size(39, 13)
        Me.lblMovieScraperGlobalActors.TabIndex = 68
        Me.lblMovieScraperGlobalActors.Text = "Actors"
        '
        'chkMovieScraperCast
        '
        Me.chkMovieScraperCast.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCast.AutoSize = True
        Me.chkMovieScraperCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCast.Location = New System.Drawing.Point(106, 365)
        Me.chkMovieScraperCast.Name = "chkMovieScraperCast"
        Me.chkMovieScraperCast.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCast.TabIndex = 7
        Me.chkMovieScraperCast.UseVisualStyleBackColor = True
        '
        'chkMovieLockActors
        '
        Me.chkMovieLockActors.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockActors.AutoSize = True
        Me.chkMovieLockActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockActors.Location = New System.Drawing.Point(135, 365)
        Me.chkMovieLockActors.Name = "chkMovieLockActors"
        Me.chkMovieLockActors.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockActors.TabIndex = 50
        Me.chkMovieLockActors.UseVisualStyleBackColor = True
        '
        'txtMovieScraperCastLimit
        '
        Me.txtMovieScraperCastLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperCastLimit.Enabled = False
        Me.txtMovieScraperCastLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperCastLimit.Location = New System.Drawing.Point(179, 361)
        Me.txtMovieScraperCastLimit.Name = "txtMovieScraperCastLimit"
        Me.txtMovieScraperCastLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperCastLimit.TabIndex = 19
        '
        'lblMovieScraperGlobalCountries
        '
        Me.lblMovieScraperGlobalCountries.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalCountries.AutoSize = True
        Me.lblMovieScraperGlobalCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalCountries.Location = New System.Drawing.Point(3, 433)
        Me.lblMovieScraperGlobalCountries.Name = "lblMovieScraperGlobalCountries"
        Me.lblMovieScraperGlobalCountries.Size = New System.Drawing.Size(57, 13)
        Me.lblMovieScraperGlobalCountries.TabIndex = 68
        Me.lblMovieScraperGlobalCountries.Text = "Countries"
        '
        'chkMovieScraperCountry
        '
        Me.chkMovieScraperCountry.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCountry.AutoSize = True
        Me.chkMovieScraperCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCountry.Location = New System.Drawing.Point(106, 433)
        Me.chkMovieScraperCountry.Name = "chkMovieScraperCountry"
        Me.chkMovieScraperCountry.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCountry.TabIndex = 25
        Me.chkMovieScraperCountry.UseVisualStyleBackColor = True
        '
        'chkMovieLockCountry
        '
        Me.chkMovieLockCountry.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCountry.AutoSize = True
        Me.chkMovieLockCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockCountry.Location = New System.Drawing.Point(135, 433)
        Me.chkMovieLockCountry.Name = "chkMovieLockCountry"
        Me.chkMovieLockCountry.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCountry.TabIndex = 63
        Me.chkMovieLockCountry.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalDirectors
        '
        Me.lblMovieScraperGlobalDirectors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalDirectors.AutoSize = True
        Me.lblMovieScraperGlobalDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalDirectors.Location = New System.Drawing.Point(3, 389)
        Me.lblMovieScraperGlobalDirectors.Name = "lblMovieScraperGlobalDirectors"
        Me.lblMovieScraperGlobalDirectors.Size = New System.Drawing.Size(53, 13)
        Me.lblMovieScraperGlobalDirectors.TabIndex = 68
        Me.lblMovieScraperGlobalDirectors.Text = "Directors"
        '
        'chkMovieScraperDirector
        '
        Me.chkMovieScraperDirector.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperDirector.AutoSize = True
        Me.chkMovieScraperDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperDirector.Location = New System.Drawing.Point(106, 389)
        Me.chkMovieScraperDirector.Name = "chkMovieScraperDirector"
        Me.chkMovieScraperDirector.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperDirector.TabIndex = 9
        Me.chkMovieScraperDirector.UseVisualStyleBackColor = True
        '
        'chkMovieLockDirector
        '
        Me.chkMovieLockDirector.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockDirector.AutoSize = True
        Me.chkMovieLockDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockDirector.Location = New System.Drawing.Point(135, 389)
        Me.chkMovieLockDirector.Name = "chkMovieLockDirector"
        Me.chkMovieLockDirector.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockDirector.TabIndex = 57
        Me.chkMovieLockDirector.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalCredits
        '
        Me.lblMovieScraperGlobalCredits.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalCredits.AutoSize = True
        Me.lblMovieScraperGlobalCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalCredits.Location = New System.Drawing.Point(3, 409)
        Me.lblMovieScraperGlobalCredits.Name = "lblMovieScraperGlobalCredits"
        Me.lblMovieScraperGlobalCredits.Size = New System.Drawing.Size(43, 13)
        Me.lblMovieScraperGlobalCredits.TabIndex = 68
        Me.lblMovieScraperGlobalCredits.Text = "Credits"
        '
        'chkMovieScraperCredits
        '
        Me.chkMovieScraperCredits.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperCredits.AutoSize = True
        Me.chkMovieScraperCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCredits.Location = New System.Drawing.Point(106, 409)
        Me.chkMovieScraperCredits.Name = "chkMovieScraperCredits"
        Me.chkMovieScraperCredits.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperCredits.TabIndex = 15
        Me.chkMovieScraperCredits.UseVisualStyleBackColor = True
        '
        'chkMovieLockCredits
        '
        Me.chkMovieLockCredits.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockCredits.AutoSize = True
        Me.chkMovieLockCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockCredits.Location = New System.Drawing.Point(135, 409)
        Me.chkMovieLockCredits.Name = "chkMovieLockCredits"
        Me.chkMovieLockCredits.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockCredits.TabIndex = 58
        Me.chkMovieLockCredits.UseVisualStyleBackColor = True
        '
        'lblMovieScraperGlobalUserRating
        '
        Me.lblMovieScraperGlobalUserRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperGlobalUserRating.AutoSize = True
        Me.lblMovieScraperGlobalUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieScraperGlobalUserRating.Location = New System.Drawing.Point(3, 186)
        Me.lblMovieScraperGlobalUserRating.Name = "lblMovieScraperGlobalUserRating"
        Me.lblMovieScraperGlobalUserRating.Size = New System.Drawing.Size(67, 13)
        Me.lblMovieScraperGlobalUserRating.TabIndex = 68
        Me.lblMovieScraperGlobalUserRating.Text = "User Rating"
        '
        'chkMovieScraperUserRating
        '
        Me.chkMovieScraperUserRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieScraperUserRating.AutoSize = True
        Me.chkMovieScraperUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperUserRating.Location = New System.Drawing.Point(106, 186)
        Me.chkMovieScraperUserRating.Name = "chkMovieScraperUserRating"
        Me.chkMovieScraperUserRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieScraperUserRating.TabIndex = 4
        Me.chkMovieScraperUserRating.UseVisualStyleBackColor = True
        '
        'chkMovieLockUserRating
        '
        Me.chkMovieLockUserRating.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieLockUserRating.AutoSize = True
        Me.chkMovieLockUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLockUserRating.Location = New System.Drawing.Point(135, 186)
        Me.chkMovieLockUserRating.Name = "chkMovieLockUserRating"
        Me.chkMovieLockUserRating.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieLockUserRating.TabIndex = 4
        Me.chkMovieLockUserRating.UseVisualStyleBackColor = True
        '
        'txtMovieScraperCountryLimit
        '
        Me.txtMovieScraperCountryLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMovieScraperCountryLimit.Enabled = False
        Me.txtMovieScraperCountryLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperCountryLimit.Location = New System.Drawing.Point(179, 429)
        Me.txtMovieScraperCountryLimit.Name = "txtMovieScraperCountryLimit"
        Me.txtMovieScraperCountryLimit.Size = New System.Drawing.Size(39, 22)
        Me.txtMovieScraperCountryLimit.TabIndex = 21
        '
        'gbMovieScraperCertificationOpts
        '
        Me.gbMovieScraperCertificationOpts.AutoSize = True
        Me.gbMovieScraperCertificationOpts.Controls.Add(Me.tblMovieScraperCertificationOpts)
        Me.gbMovieScraperCertificationOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperCertificationOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieScraperCertificationOpts.Location = New System.Drawing.Point(252, 388)
        Me.gbMovieScraperCertificationOpts.Name = "gbMovieScraperCertificationOpts"
        Me.gbMovieScraperCertificationOpts.Size = New System.Drawing.Size(446, 118)
        Me.gbMovieScraperCertificationOpts.TabIndex = 77
        Me.gbMovieScraperCertificationOpts.TabStop = False
        Me.gbMovieScraperCertificationOpts.Text = "Certification"
        '
        'tblMovieScraperCertificationOpts
        '
        Me.tblMovieScraperCertificationOpts.AutoSize = True
        Me.tblMovieScraperCertificationOpts.ColumnCount = 2
        Me.tblMovieScraperCertificationOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperCertificationOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertOnlyValue, 0, 2)
        Me.tblMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertForMPAAFallback, 0, 1)
        Me.tblMovieScraperCertificationOpts.Controls.Add(Me.chkMovieScraperCertForMPAA, 0, 0)
        Me.tblMovieScraperCertificationOpts.Controls.Add(Me.txtMovieScraperMPAANotRated, 1, 3)
        Me.tblMovieScraperCertificationOpts.Controls.Add(Me.lblMovieScraperMPAANotRated, 0, 3)
        Me.tblMovieScraperCertificationOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperCertificationOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperCertificationOpts.Name = "tblMovieScraperCertificationOpts"
        Me.tblMovieScraperCertificationOpts.RowCount = 5
        Me.tblMovieScraperCertificationOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCertificationOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCertificationOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCertificationOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCertificationOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCertificationOpts.Size = New System.Drawing.Size(440, 97)
        Me.tblMovieScraperCertificationOpts.TabIndex = 78
        '
        'chkMovieScraperCertOnlyValue
        '
        Me.chkMovieScraperCertOnlyValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCertOnlyValue.AutoSize = True
        Me.tblMovieScraperCertificationOpts.SetColumnSpan(Me.chkMovieScraperCertOnlyValue, 2)
        Me.chkMovieScraperCertOnlyValue.Enabled = False
        Me.chkMovieScraperCertOnlyValue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCertOnlyValue.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieScraperCertOnlyValue.Name = "chkMovieScraperCertOnlyValue"
        Me.chkMovieScraperCertOnlyValue.Size = New System.Drawing.Size(229, 17)
        Me.chkMovieScraperCertOnlyValue.TabIndex = 66
        Me.chkMovieScraperCertOnlyValue.Text = "MPAA: Save only number (only for YAMJ)"
        Me.chkMovieScraperCertOnlyValue.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCertForMPAAFallback
        '
        Me.chkMovieScraperCertForMPAAFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCertForMPAAFallback.AutoSize = True
        Me.tblMovieScraperCertificationOpts.SetColumnSpan(Me.chkMovieScraperCertForMPAAFallback, 2)
        Me.chkMovieScraperCertForMPAAFallback.Enabled = False
        Me.chkMovieScraperCertForMPAAFallback.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCertForMPAAFallback.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieScraperCertForMPAAFallback.Name = "chkMovieScraperCertForMPAAFallback"
        Me.chkMovieScraperCertForMPAAFallback.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieScraperCertForMPAAFallback.Size = New System.Drawing.Size(175, 17)
        Me.chkMovieScraperCertForMPAAFallback.TabIndex = 68
        Me.chkMovieScraperCertForMPAAFallback.Text = "Only if no MPAA is found"
        Me.chkMovieScraperCertForMPAAFallback.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCertForMPAA
        '
        Me.chkMovieScraperCertForMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCertForMPAA.AutoSize = True
        Me.tblMovieScraperCertificationOpts.SetColumnSpan(Me.chkMovieScraperCertForMPAA, 2)
        Me.chkMovieScraperCertForMPAA.Enabled = False
        Me.chkMovieScraperCertForMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCertForMPAA.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieScraperCertForMPAA.Name = "chkMovieScraperCertForMPAA"
        Me.chkMovieScraperCertForMPAA.Size = New System.Drawing.Size(230, 17)
        Me.chkMovieScraperCertForMPAA.TabIndex = 6
        Me.chkMovieScraperCertForMPAA.Text = "Use Certification for MPAA (XBMC users)"
        Me.chkMovieScraperCertForMPAA.UseVisualStyleBackColor = True
        '
        'txtMovieScraperMPAANotRated
        '
        Me.txtMovieScraperMPAANotRated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMovieScraperMPAANotRated.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperMPAANotRated.Location = New System.Drawing.Point(198, 72)
        Me.txtMovieScraperMPAANotRated.Name = "txtMovieScraperMPAANotRated"
        Me.txtMovieScraperMPAANotRated.Size = New System.Drawing.Size(239, 22)
        Me.txtMovieScraperMPAANotRated.TabIndex = 69
        '
        'lblMovieScraperMPAANotRated
        '
        Me.lblMovieScraperMPAANotRated.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperMPAANotRated.AutoSize = True
        Me.lblMovieScraperMPAANotRated.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.gbMovieScraperMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieScraperMiscOpts.Location = New System.Drawing.Point(252, 172)
        Me.gbMovieScraperMiscOpts.Name = "gbMovieScraperMiscOpts"
        Me.gbMovieScraperMiscOpts.Size = New System.Drawing.Size(446, 210)
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
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCleanPlotOutline, 0, 5)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCleanFields, 0, 0)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperCastWithImg, 0, 2)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperDetailView, 0, 1)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.lblMovieScraperOutlineLimit, 1, 3)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.txtMovieScraperOutlineLimit, 2, 3)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperPlotForOutline, 0, 3)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperXBMCTrailerFormat, 0, 7)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperPlotForOutlineIfEmpty, 0, 4)
        Me.tblMovieScraperMiscOpts.Controls.Add(Me.chkMovieScraperOriginalTitleAsTitle, 0, 6)
        Me.tblMovieScraperMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperMiscOpts.Name = "tblMovieScraperMiscOpts"
        Me.tblMovieScraperMiscOpts.RowCount = 9
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMiscOpts.Size = New System.Drawing.Size(440, 189)
        Me.tblMovieScraperMiscOpts.TabIndex = 78
        '
        'chkMovieScraperCleanPlotOutline
        '
        Me.chkMovieScraperCleanPlotOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCleanPlotOutline.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkMovieScraperCleanPlotOutline, 3)
        Me.chkMovieScraperCleanPlotOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCleanPlotOutline.Location = New System.Drawing.Point(3, 123)
        Me.chkMovieScraperCleanPlotOutline.Name = "chkMovieScraperCleanPlotOutline"
        Me.chkMovieScraperCleanPlotOutline.Size = New System.Drawing.Size(121, 17)
        Me.chkMovieScraperCleanPlotOutline.TabIndex = 76
        Me.chkMovieScraperCleanPlotOutline.Text = "Clean Plot/Outline"
        Me.chkMovieScraperCleanPlotOutline.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCleanFields
        '
        Me.chkMovieScraperCleanFields.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCleanFields.AutoSize = True
        Me.chkMovieScraperCleanFields.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCleanFields.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieScraperCleanFields.Name = "chkMovieScraperCleanFields"
        Me.chkMovieScraperCleanFields.Size = New System.Drawing.Size(147, 17)
        Me.chkMovieScraperCleanFields.TabIndex = 79
        Me.chkMovieScraperCleanFields.Text = "Cleanup disabled fields"
        Me.chkMovieScraperCleanFields.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCastWithImg
        '
        Me.chkMovieScraperCastWithImg.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCastWithImg.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkMovieScraperCastWithImg, 3)
        Me.chkMovieScraperCastWithImg.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCastWithImg.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieScraperCastWithImg.Name = "chkMovieScraperCastWithImg"
        Me.chkMovieScraperCastWithImg.Size = New System.Drawing.Size(189, 17)
        Me.chkMovieScraperCastWithImg.TabIndex = 1
        Me.chkMovieScraperCastWithImg.Text = "Scrape Only Actors With Images"
        Me.chkMovieScraperCastWithImg.UseVisualStyleBackColor = True
        '
        'chkMovieScraperDetailView
        '
        Me.chkMovieScraperDetailView.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperDetailView.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkMovieScraperDetailView, 3)
        Me.chkMovieScraperDetailView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperDetailView.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieScraperDetailView.Name = "chkMovieScraperDetailView"
        Me.chkMovieScraperDetailView.Size = New System.Drawing.Size(219, 17)
        Me.chkMovieScraperDetailView.TabIndex = 78
        Me.chkMovieScraperDetailView.Text = "Show scraped results in detailed view"
        Me.chkMovieScraperDetailView.UseVisualStyleBackColor = True
        '
        'lblMovieScraperOutlineLimit
        '
        Me.lblMovieScraperOutlineLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieScraperOutlineLimit.AutoSize = True
        Me.lblMovieScraperOutlineLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieScraperOutlineLimit.Location = New System.Drawing.Point(199, 76)
        Me.lblMovieScraperOutlineLimit.Name = "lblMovieScraperOutlineLimit"
        Me.lblMovieScraperOutlineLimit.Size = New System.Drawing.Size(34, 13)
        Me.lblMovieScraperOutlineLimit.TabIndex = 70
        Me.lblMovieScraperOutlineLimit.Text = "Limit:"
        Me.lblMovieScraperOutlineLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMovieScraperOutlineLimit
        '
        Me.txtMovieScraperOutlineLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieScraperOutlineLimit.Enabled = False
        Me.txtMovieScraperOutlineLimit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperOutlineLimit.Location = New System.Drawing.Point(239, 72)
        Me.txtMovieScraperOutlineLimit.Name = "txtMovieScraperOutlineLimit"
        Me.txtMovieScraperOutlineLimit.Size = New System.Drawing.Size(54, 22)
        Me.txtMovieScraperOutlineLimit.TabIndex = 69
        '
        'chkMovieScraperPlotForOutline
        '
        Me.chkMovieScraperPlotForOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperPlotForOutline.AutoSize = True
        Me.chkMovieScraperPlotForOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperPlotForOutline.Location = New System.Drawing.Point(3, 74)
        Me.chkMovieScraperPlotForOutline.Name = "chkMovieScraperPlotForOutline"
        Me.chkMovieScraperPlotForOutline.Size = New System.Drawing.Size(134, 17)
        Me.chkMovieScraperPlotForOutline.TabIndex = 68
        Me.chkMovieScraperPlotForOutline.Text = "Use Plot  for Outline "
        Me.chkMovieScraperPlotForOutline.UseVisualStyleBackColor = True
        '
        'chkMovieScraperXBMCTrailerFormat
        '
        Me.chkMovieScraperXBMCTrailerFormat.AutoSize = True
        Me.tblMovieScraperMiscOpts.SetColumnSpan(Me.chkMovieScraperXBMCTrailerFormat, 3)
        Me.chkMovieScraperXBMCTrailerFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkMovieScraperXBMCTrailerFormat.Location = New System.Drawing.Point(3, 169)
        Me.chkMovieScraperXBMCTrailerFormat.Name = "chkMovieScraperXBMCTrailerFormat"
        Me.chkMovieScraperXBMCTrailerFormat.Size = New System.Drawing.Size(302, 17)
        Me.chkMovieScraperXBMCTrailerFormat.TabIndex = 83
        Me.chkMovieScraperXBMCTrailerFormat.Text = "Save YouTube-Trailer-Links in XBMC compatible format"
        Me.chkMovieScraperXBMCTrailerFormat.UseVisualStyleBackColor = True
        '
        'chkMovieScraperPlotForOutlineIfEmpty
        '
        Me.chkMovieScraperPlotForOutlineIfEmpty.AutoSize = True
        Me.chkMovieScraperPlotForOutlineIfEmpty.Enabled = False
        Me.chkMovieScraperPlotForOutlineIfEmpty.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMovieScraperPlotForOutlineIfEmpty.Location = New System.Drawing.Point(3, 100)
        Me.chkMovieScraperPlotForOutlineIfEmpty.Name = "chkMovieScraperPlotForOutlineIfEmpty"
        Me.chkMovieScraperPlotForOutlineIfEmpty.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieScraperPlotForOutlineIfEmpty.Size = New System.Drawing.Size(190, 17)
        Me.chkMovieScraperPlotForOutlineIfEmpty.TabIndex = 84
        Me.chkMovieScraperPlotForOutlineIfEmpty.Text = "Only if Plot Outline is empty"
        Me.chkMovieScraperPlotForOutlineIfEmpty.UseVisualStyleBackColor = True
        '
        'chkMovieScraperOriginalTitleAsTitle
        '
        Me.chkMovieScraperOriginalTitleAsTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperOriginalTitleAsTitle.AutoSize = True
        Me.chkMovieScraperOriginalTitleAsTitle.Enabled = False
        Me.chkMovieScraperOriginalTitleAsTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperOriginalTitleAsTitle.Location = New System.Drawing.Point(3, 146)
        Me.chkMovieScraperOriginalTitleAsTitle.Name = "chkMovieScraperOriginalTitleAsTitle"
        Me.chkMovieScraperOriginalTitleAsTitle.Size = New System.Drawing.Size(152, 17)
        Me.chkMovieScraperOriginalTitleAsTitle.TabIndex = 68
        Me.chkMovieScraperOriginalTitleAsTitle.Text = "Use Original Title as Title"
        Me.chkMovieScraperOriginalTitleAsTitle.UseVisualStyleBackColor = True
        '
        'gbMovieScraperMetaDataOpts
        '
        Me.gbMovieScraperMetaDataOpts.AutoSize = True
        Me.gbMovieScraperMetaDataOpts.Controls.Add(Me.tblMovieScraperMetaDataOpts)
        Me.gbMovieScraperMetaDataOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperMetaDataOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieScraperMetaDataOpts.Location = New System.Drawing.Point(252, 3)
        Me.gbMovieScraperMetaDataOpts.Name = "gbMovieScraperMetaDataOpts"
        Me.gbMovieScraperMetaDataOpts.Size = New System.Drawing.Size(446, 163)
        Me.gbMovieScraperMetaDataOpts.TabIndex = 63
        Me.gbMovieScraperMetaDataOpts.TabStop = False
        Me.gbMovieScraperMetaDataOpts.Text = "Meta Data"
        '
        'tblMovieScraperMetaDataOpts
        '
        Me.tblMovieScraperMetaDataOpts.AutoSize = True
        Me.tblMovieScraperMetaDataOpts.ColumnCount = 3
        Me.tblMovieScraperMetaDataOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMetaDataOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMetaDataOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperMetaDataOpts.Controls.Add(Me.gbMovieScraperDefFIExtOpts, 1, 0)
        Me.tblMovieScraperMetaDataOpts.Controls.Add(Me.chkMovieScraperMetaDataScan, 0, 0)
        Me.tblMovieScraperMetaDataOpts.Controls.Add(Me.chkMovieScraperMetaDataIFOScan, 0, 1)
        Me.tblMovieScraperMetaDataOpts.Controls.Add(Me.gbMovieScraperDurationFormatOpts, 0, 2)
        Me.tblMovieScraperMetaDataOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperMetaDataOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperMetaDataOpts.Name = "tblMovieScraperMetaDataOpts"
        Me.tblMovieScraperMetaDataOpts.RowCount = 5
        Me.tblMovieScraperMetaDataOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMetaDataOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMetaDataOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMetaDataOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMetaDataOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperMetaDataOpts.Size = New System.Drawing.Size(440, 142)
        Me.tblMovieScraperMetaDataOpts.TabIndex = 78
        '
        'gbMovieScraperDefFIExtOpts
        '
        Me.gbMovieScraperDefFIExtOpts.AutoSize = True
        Me.gbMovieScraperDefFIExtOpts.Controls.Add(Me.tblMovieScraperDefFIExtOpts)
        Me.gbMovieScraperDefFIExtOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieScraperDefFIExtOpts.Location = New System.Drawing.Point(257, 3)
        Me.gbMovieScraperDefFIExtOpts.Name = "gbMovieScraperDefFIExtOpts"
        Me.tblMovieScraperMetaDataOpts.SetRowSpan(Me.gbMovieScraperDefFIExtOpts, 4)
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
        Me.txtMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lstMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.lblMovieScraperDefFIExt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieScraperDefFIExt.Location = New System.Drawing.Point(3, 69)
        Me.lblMovieScraperDefFIExt.Name = "lblMovieScraperDefFIExt"
        Me.lblMovieScraperDefFIExt.Size = New System.Drawing.Size(53, 13)
        Me.lblMovieScraperDefFIExt.TabIndex = 32
        Me.lblMovieScraperDefFIExt.Text = "File Type:"
        '
        'chkMovieScraperMetaDataScan
        '
        Me.chkMovieScraperMetaDataScan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperMetaDataScan.AutoSize = True
        Me.chkMovieScraperMetaDataScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperMetaDataScan.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieScraperMetaDataScan.Name = "chkMovieScraperMetaDataScan"
        Me.chkMovieScraperMetaDataScan.Size = New System.Drawing.Size(106, 17)
        Me.chkMovieScraperMetaDataScan.TabIndex = 7
        Me.chkMovieScraperMetaDataScan.Text = "Scan Meta Data"
        Me.chkMovieScraperMetaDataScan.UseVisualStyleBackColor = True
        '
        'chkMovieScraperMetaDataIFOScan
        '
        Me.chkMovieScraperMetaDataIFOScan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperMetaDataIFOScan.AutoSize = True
        Me.chkMovieScraperMetaDataIFOScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperMetaDataIFOScan.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieScraperMetaDataIFOScan.Name = "chkMovieScraperMetaDataIFOScan"
        Me.chkMovieScraperMetaDataIFOScan.Size = New System.Drawing.Size(123, 17)
        Me.chkMovieScraperMetaDataIFOScan.TabIndex = 18
        Me.chkMovieScraperMetaDataIFOScan.Text = "Enable IFO Parsing"
        Me.chkMovieScraperMetaDataIFOScan.UseVisualStyleBackColor = True
        '
        'gbMovieScraperDurationFormatOpts
        '
        Me.gbMovieScraperDurationFormatOpts.AutoSize = True
        Me.gbMovieScraperDurationFormatOpts.Controls.Add(Me.tblMovieScraperDurationFormatOpts)
        Me.gbMovieScraperDurationFormatOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieScraperDurationFormatOpts.Location = New System.Drawing.Point(3, 49)
        Me.gbMovieScraperDurationFormatOpts.Name = "gbMovieScraperDurationFormatOpts"
        Me.gbMovieScraperDurationFormatOpts.Size = New System.Drawing.Size(248, 72)
        Me.gbMovieScraperDurationFormatOpts.TabIndex = 9
        Me.gbMovieScraperDurationFormatOpts.TabStop = False
        Me.gbMovieScraperDurationFormatOpts.Text = "Duration Format"
        '
        'tblMovieScraperDurationFormatOpts
        '
        Me.tblMovieScraperDurationFormatOpts.AutoSize = True
        Me.tblMovieScraperDurationFormatOpts.ColumnCount = 3
        Me.tblMovieScraperDurationFormatOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDurationFormatOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDurationFormatOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperDurationFormatOpts.Controls.Add(Me.lblMovieScraperDurationRuntimeFormat, 1, 0)
        Me.tblMovieScraperDurationFormatOpts.Controls.Add(Me.chkMovieScraperUseMDDuration, 0, 0)
        Me.tblMovieScraperDurationFormatOpts.Controls.Add(Me.txtMovieScraperDurationRuntimeFormat, 0, 1)
        Me.tblMovieScraperDurationFormatOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperDurationFormatOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperDurationFormatOpts.Name = "tblMovieScraperDurationFormatOpts"
        Me.tblMovieScraperDurationFormatOpts.RowCount = 3
        Me.tblMovieScraperDurationFormatOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDurationFormatOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDurationFormatOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperDurationFormatOpts.Size = New System.Drawing.Size(242, 51)
        Me.tblMovieScraperDurationFormatOpts.TabIndex = 78
        '
        'lblMovieScraperDurationRuntimeFormat
        '
        Me.lblMovieScraperDurationRuntimeFormat.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblMovieScraperDurationRuntimeFormat.AutoSize = True
        Me.lblMovieScraperDurationRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 7.0!)
        Me.lblMovieScraperDurationRuntimeFormat.Location = New System.Drawing.Point(169, 7)
        Me.lblMovieScraperDurationRuntimeFormat.Name = "lblMovieScraperDurationRuntimeFormat"
        Me.tblMovieScraperDurationFormatOpts.SetRowSpan(Me.lblMovieScraperDurationRuntimeFormat, 2)
        Me.lblMovieScraperDurationRuntimeFormat.Size = New System.Drawing.Size(70, 36)
        Me.lblMovieScraperDurationRuntimeFormat.TabIndex = 23
        Me.lblMovieScraperDurationRuntimeFormat.Text = "<h>=Hours" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<m>=Minutes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<s>=Seconds"
        Me.lblMovieScraperDurationRuntimeFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkMovieScraperUseMDDuration
        '
        Me.chkMovieScraperUseMDDuration.AutoSize = True
        Me.chkMovieScraperUseMDDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperUseMDDuration.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieScraperUseMDDuration.Name = "chkMovieScraperUseMDDuration"
        Me.chkMovieScraperUseMDDuration.Size = New System.Drawing.Size(158, 17)
        Me.chkMovieScraperUseMDDuration.TabIndex = 8
        Me.chkMovieScraperUseMDDuration.Text = "Use Duration for Runtime"
        Me.chkMovieScraperUseMDDuration.UseVisualStyleBackColor = True
        '
        'txtMovieScraperDurationRuntimeFormat
        '
        Me.txtMovieScraperDurationRuntimeFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieScraperDurationRuntimeFormat.Location = New System.Drawing.Point(3, 26)
        Me.txtMovieScraperDurationRuntimeFormat.Name = "txtMovieScraperDurationRuntimeFormat"
        Me.txtMovieScraperDurationRuntimeFormat.Size = New System.Drawing.Size(160, 22)
        Me.txtMovieScraperDurationRuntimeFormat.TabIndex = 22
        '
        'gbMovieScraperCollectionOpts
        '
        Me.gbMovieScraperCollectionOpts.AutoSize = True
        Me.gbMovieScraperCollectionOpts.Controls.Add(Me.tblMovieScraperCollectionOpts)
        Me.gbMovieScraperCollectionOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieScraperCollectionOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieScraperCollectionOpts.Location = New System.Drawing.Point(252, 512)
        Me.gbMovieScraperCollectionOpts.Name = "gbMovieScraperCollectionOpts"
        Me.gbMovieScraperCollectionOpts.Size = New System.Drawing.Size(446, 90)
        Me.gbMovieScraperCollectionOpts.TabIndex = 78
        Me.gbMovieScraperCollectionOpts.TabStop = False
        Me.gbMovieScraperCollectionOpts.Text = "Collection"
        '
        'tblMovieScraperCollectionOpts
        '
        Me.tblMovieScraperCollectionOpts.AutoSize = True
        Me.tblMovieScraperCollectionOpts.ColumnCount = 2
        Me.tblMovieScraperCollectionOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperCollectionOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieScraperCollectionOpts.Controls.Add(Me.chkMovieScraperCollectionsYAMJCompatibleSets, 0, 2)
        Me.tblMovieScraperCollectionOpts.Controls.Add(Me.chkMovieScraperCollectionsAuto, 0, 0)
        Me.tblMovieScraperCollectionOpts.Controls.Add(Me.chkMovieScraperCollectionsExtendedInfo, 0, 1)
        Me.tblMovieScraperCollectionOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieScraperCollectionOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieScraperCollectionOpts.Name = "tblMovieScraperCollectionOpts"
        Me.tblMovieScraperCollectionOpts.RowCount = 4
        Me.tblMovieScraperCollectionOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCollectionOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCollectionOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCollectionOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieScraperCollectionOpts.Size = New System.Drawing.Size(440, 69)
        Me.tblMovieScraperCollectionOpts.TabIndex = 0
        '
        'chkMovieScraperCollectionsYAMJCompatibleSets
        '
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.AutoSize = True
        Me.tblMovieScraperCollectionOpts.SetColumnSpan(Me.chkMovieScraperCollectionsYAMJCompatibleSets, 2)
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Name = "chkMovieScraperCollectionsYAMJCompatibleSets"
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Size = New System.Drawing.Size(203, 17)
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.TabIndex = 2
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.Text = "Save YAMJ Compatible Sets to NFO"
        Me.chkMovieScraperCollectionsYAMJCompatibleSets.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCollectionsAuto
        '
        Me.chkMovieScraperCollectionsAuto.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieScraperCollectionsAuto.AutoSize = True
        Me.chkMovieScraperCollectionsAuto.Enabled = False
        Me.chkMovieScraperCollectionsAuto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCollectionsAuto.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieScraperCollectionsAuto.Name = "chkMovieScraperCollectionsAuto"
        Me.chkMovieScraperCollectionsAuto.Size = New System.Drawing.Size(226, 17)
        Me.chkMovieScraperCollectionsAuto.TabIndex = 0
        Me.chkMovieScraperCollectionsAuto.Text = "Add Movie automatically to Collections"
        Me.chkMovieScraperCollectionsAuto.UseVisualStyleBackColor = True
        '
        'chkMovieScraperCollectionsExtendedInfo
        '
        Me.chkMovieScraperCollectionsExtendedInfo.AutoSize = True
        Me.chkMovieScraperCollectionsExtendedInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieScraperCollectionsExtendedInfo.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieScraperCollectionsExtendedInfo.Name = "chkMovieScraperCollectionsExtendedInfo"
        Me.chkMovieScraperCollectionsExtendedInfo.Size = New System.Drawing.Size(411, 17)
        Me.chkMovieScraperCollectionsExtendedInfo.TabIndex = 1
        Me.chkMovieScraperCollectionsExtendedInfo.Text = "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)"
        Me.chkMovieScraperCollectionsExtendedInfo.UseVisualStyleBackColor = True
        '
        'frmMovie_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 651)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmMovie_Data"
        Me.Text = "frmMovie_Data"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieScraperGlobalOpts.ResumeLayout(False)
        Me.gbMovieScraperGlobalOpts.PerformLayout()
        Me.tblMovieScraperGlobalOpts.ResumeLayout(False)
        Me.tblMovieScraperGlobalOpts.PerformLayout()
        Me.gbMovieScraperCertificationOpts.ResumeLayout(False)
        Me.gbMovieScraperCertificationOpts.PerformLayout()
        Me.tblMovieScraperCertificationOpts.ResumeLayout(False)
        Me.tblMovieScraperCertificationOpts.PerformLayout()
        Me.gbMovieScraperMiscOpts.ResumeLayout(False)
        Me.gbMovieScraperMiscOpts.PerformLayout()
        Me.tblMovieScraperMiscOpts.ResumeLayout(False)
        Me.tblMovieScraperMiscOpts.PerformLayout()
        Me.gbMovieScraperMetaDataOpts.ResumeLayout(False)
        Me.gbMovieScraperMetaDataOpts.PerformLayout()
        Me.tblMovieScraperMetaDataOpts.ResumeLayout(False)
        Me.tblMovieScraperMetaDataOpts.PerformLayout()
        Me.gbMovieScraperDefFIExtOpts.ResumeLayout(False)
        Me.gbMovieScraperDefFIExtOpts.PerformLayout()
        Me.tblMovieScraperDefFIExtOpts.ResumeLayout(False)
        Me.tblMovieScraperDefFIExtOpts.PerformLayout()
        Me.gbMovieScraperDurationFormatOpts.ResumeLayout(False)
        Me.gbMovieScraperDurationFormatOpts.PerformLayout()
        Me.tblMovieScraperDurationFormatOpts.ResumeLayout(False)
        Me.tblMovieScraperDurationFormatOpts.PerformLayout()
        Me.gbMovieScraperCollectionOpts.ResumeLayout(False)
        Me.gbMovieScraperCollectionOpts.PerformLayout()
        Me.tblMovieScraperCollectionOpts.ResumeLayout(False)
        Me.tblMovieScraperCollectionOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMovieScraperGlobalOpts As GroupBox
    Friend WithEvents tblMovieScraperGlobalOpts As TableLayoutPanel
    Friend WithEvents chkMovieLockCollectionID As CheckBox
    Friend WithEvents lblMovieScraperGlobalHeaderLock As Label
    Friend WithEvents lblMovieScraperGlobalHeaderLimit As Label
    Friend WithEvents cbMovieScraperCertLang As ComboBox
    Friend WithEvents chkMovieLockRating As CheckBox
    Friend WithEvents chkMovieLockTitle As CheckBox
    Friend WithEvents lblMovieScraperGlobalTitle As Label
    Friend WithEvents lblMovieScraperGlobalRating As Label
    Friend WithEvents lblMovieScraperGlobalCollectionID As Label
    Friend WithEvents lblMovieScraperGlobalLanguageA As Label
    Friend WithEvents lblMovieScraperGlobalLanguageV As Label
    Friend WithEvents lblMovieScraperGlobalCollections As Label
    Friend WithEvents chkMovieLockLanguageA As CheckBox
    Friend WithEvents chkMovieLockLanguageV As CheckBox
    Friend WithEvents chkMovieScraperTitle As CheckBox
    Friend WithEvents chkMovieScraperRating As CheckBox
    Friend WithEvents chkMovieScraperCollectionID As CheckBox
    Friend WithEvents chkMovieLockCollections As CheckBox
    Friend WithEvents lblMovieScraperGlobalOriginalTitle As Label
    Friend WithEvents chkMovieScraperOriginalTitle As CheckBox
    Friend WithEvents chkMovieLockOriginalTitle As CheckBox
    Friend WithEvents lblMovieScraperGlobalYear As Label
    Friend WithEvents chkMovieScraperYear As CheckBox
    Friend WithEvents chkMovieLockYear As CheckBox
    Friend WithEvents lblMovieScraperGlobalReleaseDate As Label
    Friend WithEvents chkMovieScraperRelease As CheckBox
    Friend WithEvents chkMovieLockReleaseDate As CheckBox
    Friend WithEvents lblMovieScraperGlobalPlot As Label
    Friend WithEvents chkMovieScraperPlot As CheckBox
    Friend WithEvents chkMovieLockPlot As CheckBox
    Friend WithEvents lblMovieScraperGlobalOutline As Label
    Friend WithEvents chkMovieScraperOutline As CheckBox
    Friend WithEvents chkMovieLockOutline As CheckBox
    Friend WithEvents lblMovieScraperGlobalTagline As Label
    Friend WithEvents chkMovieScraperTagline As CheckBox
    Friend WithEvents chkMovieLockTagline As CheckBox
    Friend WithEvents lblMovieScraperGlobalTop250 As Label
    Friend WithEvents chkMovieScraperTop250 As CheckBox
    Friend WithEvents chkMovieLockTop250 As CheckBox
    Friend WithEvents lblMovieScraperGlobalMPAA As Label
    Friend WithEvents chkMovieScraperMPAA As CheckBox
    Friend WithEvents chkMovieLockMPAA As CheckBox
    Friend WithEvents lblMovieScraperGlobalCertifications As Label
    Friend WithEvents chkMovieScraperCert As CheckBox
    Friend WithEvents chkMovieLockCert As CheckBox
    Friend WithEvents lblMovieScraperGlobalRuntime As Label
    Friend WithEvents chkMovieScraperRuntime As CheckBox
    Friend WithEvents chkMovieLockRuntime As CheckBox
    Friend WithEvents lblMovieScraperGlobalStudios As Label
    Friend WithEvents chkMovieScraperStudio As CheckBox
    Friend WithEvents chkMovieLockStudio As CheckBox
    Friend WithEvents txtMovieScraperStudioLimit As TextBox
    Friend WithEvents lblMovieScraperGlobalTags As Label
    Friend WithEvents chkMovieScraperTags As CheckBox
    Friend WithEvents chkMovieLockTags As CheckBox
    Friend WithEvents lblMovieScraperGlobalTrailer As Label
    Friend WithEvents chkMovieScraperTrailer As CheckBox
    Friend WithEvents chkMovieLockTrailer As CheckBox
    Friend WithEvents lblMovieScraperGlobalGenres As Label
    Friend WithEvents chkMovieScraperGenre As CheckBox
    Friend WithEvents chkMovieLockGenre As CheckBox
    Friend WithEvents txtMovieScraperGenreLimit As TextBox
    Friend WithEvents lblMovieScraperGlobalActors As Label
    Friend WithEvents chkMovieScraperCast As CheckBox
    Friend WithEvents chkMovieLockActors As CheckBox
    Friend WithEvents txtMovieScraperCastLimit As TextBox
    Friend WithEvents lblMovieScraperGlobalCountries As Label
    Friend WithEvents chkMovieScraperCountry As CheckBox
    Friend WithEvents chkMovieLockCountry As CheckBox
    Friend WithEvents lblMovieScraperGlobalDirectors As Label
    Friend WithEvents chkMovieScraperDirector As CheckBox
    Friend WithEvents chkMovieLockDirector As CheckBox
    Friend WithEvents lblMovieScraperGlobalCredits As Label
    Friend WithEvents chkMovieScraperCredits As CheckBox
    Friend WithEvents chkMovieLockCredits As CheckBox
    Friend WithEvents lblMovieScraperGlobalUserRating As Label
    Friend WithEvents chkMovieScraperUserRating As CheckBox
    Friend WithEvents chkMovieLockUserRating As CheckBox
    Friend WithEvents txtMovieScraperCountryLimit As TextBox
    Friend WithEvents gbMovieScraperCertificationOpts As GroupBox
    Friend WithEvents tblMovieScraperCertificationOpts As TableLayoutPanel
    Friend WithEvents chkMovieScraperCertOnlyValue As CheckBox
    Friend WithEvents chkMovieScraperCertForMPAAFallback As CheckBox
    Friend WithEvents chkMovieScraperCertForMPAA As CheckBox
    Friend WithEvents txtMovieScraperMPAANotRated As TextBox
    Friend WithEvents lblMovieScraperMPAANotRated As Label
    Friend WithEvents gbMovieScraperMiscOpts As GroupBox
    Friend WithEvents tblMovieScraperMiscOpts As TableLayoutPanel
    Friend WithEvents chkMovieScraperCleanPlotOutline As CheckBox
    Friend WithEvents chkMovieScraperCleanFields As CheckBox
    Friend WithEvents chkMovieScraperCastWithImg As CheckBox
    Friend WithEvents chkMovieScraperDetailView As CheckBox
    Friend WithEvents lblMovieScraperOutlineLimit As Label
    Friend WithEvents txtMovieScraperOutlineLimit As TextBox
    Friend WithEvents chkMovieScraperPlotForOutline As CheckBox
    Friend WithEvents chkMovieScraperXBMCTrailerFormat As CheckBox
    Friend WithEvents chkMovieScraperPlotForOutlineIfEmpty As CheckBox
    Friend WithEvents chkMovieScraperOriginalTitleAsTitle As CheckBox
    Friend WithEvents gbMovieScraperMetaDataOpts As GroupBox
    Friend WithEvents tblMovieScraperMetaDataOpts As TableLayoutPanel
    Friend WithEvents gbMovieScraperDefFIExtOpts As GroupBox
    Friend WithEvents tblMovieScraperDefFIExtOpts As TableLayoutPanel
    Friend WithEvents btnMovieScraperDefFIExtRemove As Button
    Friend WithEvents txtMovieScraperDefFIExt As TextBox
    Friend WithEvents btnMovieScraperDefFIExtEdit As Button
    Friend WithEvents lstMovieScraperDefFIExt As ListBox
    Friend WithEvents btnMovieScraperDefFIExtAdd As Button
    Friend WithEvents lblMovieScraperDefFIExt As Label
    Friend WithEvents chkMovieScraperMetaDataScan As CheckBox
    Friend WithEvents chkMovieScraperMetaDataIFOScan As CheckBox
    Friend WithEvents gbMovieScraperDurationFormatOpts As GroupBox
    Friend WithEvents tblMovieScraperDurationFormatOpts As TableLayoutPanel
    Friend WithEvents lblMovieScraperDurationRuntimeFormat As Label
    Friend WithEvents chkMovieScraperUseMDDuration As CheckBox
    Friend WithEvents txtMovieScraperDurationRuntimeFormat As TextBox
    Friend WithEvents gbMovieScraperCollectionOpts As GroupBox
    Friend WithEvents tblMovieScraperCollectionOpts As TableLayoutPanel
    Friend WithEvents chkMovieScraperCollectionsYAMJCompatibleSets As CheckBox
    Friend WithEvents chkMovieScraperCollectionsAuto As CheckBox
    Friend WithEvents chkMovieScraperCollectionsExtendedInfo As CheckBox
End Class
