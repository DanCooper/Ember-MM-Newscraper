' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports EmberAPI
Imports NLog

Public Class Addon
    Implements Interfaces.IAddon_Data_Scraper_Movie
    Implements Interfaces.IAddon_Data_Scraper_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigScrapeOptions_Movie As New Structures.ScrapeOptions
    Public Shared ConfigScrapeOptions_TV As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifiers

    Private _AddonSettings_Movie As New AddonSettings
    Private _AddonSettings_TV As New AddonSettings
    Private _Name As String = "TVDbv4_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_TV As frmSettingsHolder_TV
    Private _TVDbApi_Movie As New Scraper
    Private _TVDbApi_TV As New Scraper

    Public Shared Localisation As New XmlTranslations

    Public Const _strApiKey As String = "c7b3aa36-fbf5-40e3-9f04-ed2cadfe9e70"

    Private _AddonSettings As New AddonSettings

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.IAddon_Data_Scraper_Movie.AddonSettingsChanged
    Public Event ScraperSetupChanged_Movie(ByVal name As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.IAddon_Data_Scraper_Movie.AddonStateChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.IAddon_Data_Scraper_Movie.AddonNeedsRestart

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.IAddon_Data_Scraper_TV.AddonSettingsChanged
    Public Event ScraperSetupChanged_TV(ByVal name As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.IAddon_Data_Scraper_TV.AddonStateChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.IAddon_Data_Scraper_TV.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property Name() As String Implements Interfaces.IAddon_Data_Scraper_Movie.Name, Interfaces.IAddon_Data_Scraper_TV.Name
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property Version() As String Implements Interfaces.IAddon_Data_Scraper_Movie.Version, Interfaces.IAddon_Data_Scraper_TV.Version
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.IAddon_Data_Scraper_Movie.Enabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
            If _ScraperEnabled_Movie Then
                Task.Run(Function() _TVDbApi_Movie.CreateAPI(_AddonSettings_Movie))
            End If
        End Set
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.IAddon_Data_Scraper_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
            If _ScraperEnabled_TV Then
                Task.Run(Function() _TVDbApi_TV.CreateAPI(_AddonSettings_TV))
            End If
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub Handle_ModuleSettingsChanged_Movie()
        RaiseEvent ModuleSettingsChanged_Movie()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_TV()
        RaiseEvent ModuleSettingsChanged_TV()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_TV()
        RaiseEvent SetupNeedsRestart_TV()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(_Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent ScraperSetupChanged_TV(String.Concat(_Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Data_Scraper_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Data_Scraper_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.IAddon_Data_Scraper_Movie.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkActors.Checked = ConfigScrapeOptions_Movie.bMainActors
        _setup_Movie.chkCountries.Checked = ConfigScrapeOptions_Movie.bMainCountries
        _setup_Movie.chkDirectors.Checked = ConfigScrapeOptions_Movie.bMainDirectors
        _setup_Movie.chkFallBackEng.Checked = _AddonSettings_Movie.FallBackEng
        _setup_Movie.chkGenres.Checked = ConfigScrapeOptions_Movie.bMainGenres
        _setup_Movie.chkCertifications.Checked = ConfigScrapeOptions_Movie.bMainMPAA
        _setup_Movie.chkOriginalTitle.Checked = ConfigScrapeOptions_Movie.bMainOriginalTitle
        _setup_Movie.chkPlot.Checked = ConfigScrapeOptions_Movie.bMainPlot
        _setup_Movie.chkPremiered.Checked = ConfigScrapeOptions_Movie.bMainPremiered
        _setup_Movie.chkRuntime.Checked = ConfigScrapeOptions_Movie.bMainRuntime
        _setup_Movie.chkStudios.Checked = ConfigScrapeOptions_Movie.bMainStudios
        _setup_Movie.chkTagline.Checked = ConfigScrapeOptions_Movie.bMainTagline
        _setup_Movie.chkTitle.Checked = ConfigScrapeOptions_Movie.bMainTitle
        _setup_Movie.chkTrailer.Checked = ConfigScrapeOptions_Movie.bMainTrailer
        _setup_Movie.chkWriters.Checked = ConfigScrapeOptions_Movie.bMainWriters
        _setup_Movie.txtApiPin.Text = _AddonSettings_Movie.ApiPin

        _setup_Movie.OrderChanged()

        SPanel.UniqueName = String.Concat(_Name, "_Movie")
        SPanel.Title = "TheTVDb.com"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled_Movie, 9, 10)
        SPanel.Panel = _setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        AddHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
        Return SPanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.IAddon_Data_Scraper_TV.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkFallBackEng.Checked = _AddonSettings_TV.FallBackEng
        _setup_TV.chkScraperEpisodeActors.Checked = ConfigScrapeOptions_TV.bEpisodeActors
        _setup_TV.chkScraperEpisodeAired.Checked = ConfigScrapeOptions_TV.bEpisodeAired
        _setup_TV.chkScraperEpisodeCredits.Checked = ConfigScrapeOptions_TV.bEpisodeCredits
        _setup_TV.chkScraperEpisodeDirectors.Checked = ConfigScrapeOptions_TV.bEpisodeDirectors
        _setup_TV.chkScraperEpisodeGuestStars.Checked = ConfigScrapeOptions_TV.bEpisodeGuestStars
        _setup_TV.chkScraperEpisodePlot.Checked = ConfigScrapeOptions_TV.bEpisodePlot
        _setup_TV.chkScraperEpisodeRating.Checked = ConfigScrapeOptions_TV.bEpisodeRating
        _setup_TV.chkScraperEpisodeTitle.Checked = ConfigScrapeOptions_TV.bEpisodeTitle
        _setup_TV.chkScraperSeasonAired.Checked = ConfigScrapeOptions_TV.bSeasonAired
        _setup_TV.chkScraperSeasonPlot.Checked = ConfigScrapeOptions_TV.bSeasonPlot
        _setup_TV.chkScraperSeasonTitle.Checked = ConfigScrapeOptions_TV.bSeasonTitle
        _setup_TV.chkScraperShowActors.Checked = ConfigScrapeOptions_TV.bMainActors
        _setup_TV.chkScraperShowCertifications.Checked = ConfigScrapeOptions_TV.bMainCertifications
        _setup_TV.chkScraperShowCountries.Checked = ConfigScrapeOptions_TV.bMainCountries
        _setup_TV.chkScraperShowCreators.Checked = ConfigScrapeOptions_TV.bMainCreators
        _setup_TV.chkScraperShowGenres.Checked = ConfigScrapeOptions_TV.bMainGenres
        _setup_TV.chkScraperShowOriginalTitle.Checked = ConfigScrapeOptions_TV.bMainOriginalTitle
        _setup_TV.chkScraperShowPlot.Checked = ConfigScrapeOptions_TV.bMainPlot
        _setup_TV.chkScraperShowPremiered.Checked = ConfigScrapeOptions_TV.bMainPremiered
        _setup_TV.chkScraperShowRating.Checked = ConfigScrapeOptions_TV.bMainRating
        _setup_TV.chkScraperShowRuntime.Checked = ConfigScrapeOptions_TV.bMainRuntime
        _setup_TV.chkScraperShowStatus.Checked = ConfigScrapeOptions_TV.bMainStatus
        _setup_TV.chkScraperShowStudios.Checked = ConfigScrapeOptions_TV.bMainStudios
        _setup_TV.chkScraperShowTagline.Checked = ConfigScrapeOptions_TV.bMainTagline
        _setup_TV.chkScraperShowTitle.Checked = ConfigScrapeOptions_TV.bMainTitle
        _setup_TV.txtApiPin.Text = _AddonSettings_TV.ApiPin

        _setup_TV.Order_Changed()

        SPanel.UniqueName = String.Concat(_Name, "_TV")
        SPanel.Title = "TheTVDb.com"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVData"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled_TV, 9, 10)
        SPanel.Panel = _setup_TV.pnlSettings

        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        AddHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
        Return SPanel
    End Function

    Sub LoadSettings_Movie()
        ConfigScrapeOptions_Movie.bMainActors = Master.eAdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainCertifications = Master.eAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainCountries = Master.eAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainDirectors = Master.eAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainGenres = Master.eAdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainMPAA = Master.eAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainOriginalTitle = Master.eAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainOutline = Master.eAdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainPlot = Master.eAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainPremiered = Master.eAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainRating = Master.eAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainRuntime = Master.eAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainStudios = Master.eAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTagline = Master.eAdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTitle = Master.eAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTop250 = Master.eAdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTrailer = Master.eAdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainWriters = Master.eAdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)

        _AddonSettings_Movie.ApiPin = Master.eAdvancedSettings.GetStringSetting("ApiPin", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings_Movie.FallBackEng = Master.eAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
    End Sub

    Sub LoadSettings_TV()
        ConfigScrapeOptions_TV.bEpisodeActors = Master.eAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeAired = Master.eAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeCredits = Master.eAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeDirectors = Master.eAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeGuestStars = Master.eAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodePlot = Master.eAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeRating = Master.eAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeTitle = Master.eAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bSeasonAired = Master.eAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bSeasonPlot = Master.eAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bSeasonTitle = Master.eAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bMainActors = Master.eAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCertifications = Master.eAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCountries = Master.eAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCreators = Master.eAdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainEpisodeGuide = Master.eAdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainGenres = Master.eAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainOriginalTitle = Master.eAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainPlot = Master.eAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainPremiered = Master.eAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainRating = Master.eAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainRuntime = Master.eAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainStatus = Master.eAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainStudios = Master.eAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainTagline = Master.eAdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainTitle = Master.eAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        _AddonSettings_TV.ApiPin = Master.eAdvancedSettings.GetStringSetting("ApiPin", String.Empty, , Enums.ContentType.TV)
        _AddonSettings_TV.FallBackEng = Master.eAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.TV)
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigScrapeOptions_Movie.bMainActors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", ConfigScrapeOptions_Movie.bMainCertifications, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCountry", ConfigScrapeOptions_Movie.bMainCountries, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDirector", ConfigScrapeOptions_Movie.bMainDirectors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.MainFanart, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoGenres", ConfigScrapeOptions_Movie.bMainGenres, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMPAA", ConfigScrapeOptions_Movie.bMainMPAA, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigScrapeOptions_Movie.bMainOriginalTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOutline", ConfigScrapeOptions_Movie.bMainOutline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions_Movie.bMainPlot, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPremiered", ConfigScrapeOptions_Movie.bMainPremiered, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRating", ConfigScrapeOptions_Movie.bMainRating, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRuntime", ConfigScrapeOptions_Movie.bMainRuntime, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoStudio", ConfigScrapeOptions_Movie.bMainStudios, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTagline", ConfigScrapeOptions_Movie.bMainTagline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTitle", ConfigScrapeOptions_Movie.bMainTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTop250", ConfigScrapeOptions_Movie.bMainTop250, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", ConfigScrapeOptions_Movie.bMainTrailer, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoWriters", ConfigScrapeOptions_Movie.bMainWriters, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings_Movie.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetStringSetting("ApiPin", _setup_Movie.txtApiPin.Text.Trim, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigScrapeOptions_TV.bEpisodeActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigScrapeOptions_TV.bEpisodeAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigScrapeOptions_TV.bEpisodeCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigScrapeOptions_TV.bEpisodeDirectors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigScrapeOptions_TV.bEpisodeGuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions_TV.bEpisodePlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigScrapeOptions_TV.bEpisodeRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigScrapeOptions_TV.bEpisodeTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigScrapeOptions_TV.bSeasonAired, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions_TV.bSeasonPlot, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoTitle", ConfigScrapeOptions_TV.bSeasonTitle, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoActors", ConfigScrapeOptions_TV.bMainActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", ConfigScrapeOptions_TV.bMainCertifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", ConfigScrapeOptions_TV.bMainCountries, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", ConfigScrapeOptions_TV.bMainCreators, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", ConfigScrapeOptions_TV.bMainEpisodeGuide, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigScrapeOptions_TV.bMainGenres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigScrapeOptions_TV.bMainOriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions_TV.bMainPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigScrapeOptions_TV.bMainPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigScrapeOptions_TV.bMainRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", ConfigScrapeOptions_TV.bMainRuntime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigScrapeOptions_TV.bMainStatus, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigScrapeOptions_TV.bMainStudios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTagline", ConfigScrapeOptions_TV.bMainTagline, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigScrapeOptions_TV.bMainTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings_TV.FallBackEng, , , Enums.ContentType.TV)
            settings.SetStringSetting("ApiPin", _setup_TV.txtApiPin.Text.Trim, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Data_Scraper_Movie.SaveSettings
        ConfigScrapeOptions_Movie.bMainActors = _setup_Movie.chkActors.Checked
        ConfigScrapeOptions_Movie.bMainCertifications = _setup_Movie.chkCertifications.Checked
        ConfigScrapeOptions_Movie.bMainCountries = _setup_Movie.chkCountries.Checked
        ConfigScrapeOptions_Movie.bMainDirectors = _setup_Movie.chkDirectors.Checked
        ConfigScrapeOptions_Movie.bMainGenres = _setup_Movie.chkGenres.Checked
        ConfigScrapeOptions_Movie.bMainMPAA = _setup_Movie.chkCertifications.Checked
        ConfigScrapeOptions_Movie.bMainOriginalTitle = _setup_Movie.chkOriginalTitle.Checked
        ConfigScrapeOptions_Movie.bMainOutline = _setup_Movie.chkPlot.Checked
        ConfigScrapeOptions_Movie.bMainPlot = _setup_Movie.chkPlot.Checked
        ConfigScrapeOptions_Movie.bMainPremiered = _setup_Movie.chkPremiered.Checked
        ConfigScrapeOptions_Movie.bMainRuntime = _setup_Movie.chkRuntime.Checked
        ConfigScrapeOptions_Movie.bMainStudios = _setup_Movie.chkStudios.Checked
        ConfigScrapeOptions_Movie.bMainTagline = _setup_Movie.chkTagline.Checked
        ConfigScrapeOptions_Movie.bMainTitle = _setup_Movie.chkTitle.Checked
        ConfigScrapeOptions_Movie.bMainTop250 = False
        ConfigScrapeOptions_Movie.bMainTrailer = _setup_Movie.chkTrailer.Checked
        ConfigScrapeOptions_Movie.bMainWriters = _setup_Movie.chkWriters.Checked

        Dim ApiPinHasChanged = Not _AddonSettings_Movie.ApiPin = _setup_Movie.txtApiPin.Text.Trim
        _AddonSettings_Movie.ApiPin = _setup_Movie.txtApiPin.Text.Trim
        _AddonSettings_Movie.FallBackEng = _setup_Movie.chkFallBackEng.Checked

        SaveSettings_Movie()

        If ApiPinHasChanged Then Task.Run(Function() _TVDbApi_Movie.CreateAPI(_AddonSettings_Movie))

        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Data_Scraper_TV.SaveSettings
        ConfigScrapeOptions_TV.bEpisodeActors = _setup_TV.chkScraperEpisodeActors.Checked
        ConfigScrapeOptions_TV.bEpisodeAired = _setup_TV.chkScraperEpisodeAired.Checked
        ConfigScrapeOptions_TV.bEpisodeCredits = _setup_TV.chkScraperEpisodeCredits.Checked
        ConfigScrapeOptions_TV.bEpisodeDirectors = _setup_TV.chkScraperEpisodeDirectors.Checked
        ConfigScrapeOptions_TV.bEpisodeGuestStars = _setup_TV.chkScraperEpisodeGuestStars.Checked
        ConfigScrapeOptions_TV.bEpisodePlot = _setup_TV.chkScraperEpisodePlot.Checked
        ConfigScrapeOptions_TV.bEpisodeRating = _setup_TV.chkScraperEpisodeRating.Checked
        ConfigScrapeOptions_TV.bEpisodeTitle = _setup_TV.chkScraperEpisodeTitle.Checked
        ConfigScrapeOptions_TV.bMainActors = _setup_TV.chkScraperShowActors.Checked
        ConfigScrapeOptions_TV.bMainCertifications = _setup_TV.chkScraperShowCertifications.Checked
        ConfigScrapeOptions_TV.bMainCreators = _setup_TV.chkScraperShowCreators.Checked
        ConfigScrapeOptions_TV.bMainCountries = _setup_TV.chkScraperShowCountries.Checked
        ConfigScrapeOptions_TV.bMainGenres = _setup_TV.chkScraperShowGenres.Checked
        ConfigScrapeOptions_TV.bMainOriginalTitle = _setup_TV.chkScraperShowOriginalTitle.Checked
        ConfigScrapeOptions_TV.bMainPlot = _setup_TV.chkScraperShowPlot.Checked
        ConfigScrapeOptions_TV.bMainPremiered = _setup_TV.chkScraperShowPremiered.Checked
        ConfigScrapeOptions_TV.bMainRating = _setup_TV.chkScraperShowRating.Checked
        ConfigScrapeOptions_TV.bMainRuntime = _setup_TV.chkScraperShowRuntime.Checked
        ConfigScrapeOptions_TV.bMainStatus = _setup_TV.chkScraperShowStatus.Checked
        ConfigScrapeOptions_TV.bMainStudios = _setup_TV.chkScraperShowStudios.Checked
        ConfigScrapeOptions_TV.bMainTagline = _setup_TV.chkScraperShowTagline.Checked
        ConfigScrapeOptions_TV.bMainTitle = _setup_TV.chkScraperShowTitle.Checked
        ConfigScrapeOptions_TV.bSeasonAired = _setup_TV.chkScraperSeasonAired.Checked
        ConfigScrapeOptions_TV.bSeasonPlot = _setup_TV.chkScraperSeasonPlot.Checked
        ConfigScrapeOptions_TV.bSeasonTitle = _setup_TV.chkScraperSeasonTitle.Checked

        Dim ApiPinHasChanged = Not _AddonSettings_TV.ApiPin = _setup_TV.txtApiPin.Text.Trim
        _AddonSettings_TV.ApiPin = _setup_TV.txtApiPin.Text.Trim
        _AddonSettings_TV.FallBackEng = _setup_TV.chkFallBackEng.Checked

        SaveSettings_TV()

        If ApiPinHasChanged Then Task.Run(Function() _TVDbApi_TV.CreateAPI(_AddonSettings_TV))

        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function GetTMDbIdByIMDbId(ByVal imdbId As String, ByRef tmdbId As Integer) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Data_Scraper_Movie.GetTMDbIdByIMDbId
        logger.Trace("[TVDbv4_Data] [GetTMDBID] [Start]")
        If Not String.IsNullOrEmpty(imdbId) Then
            If Not _TVDbApi_Movie.IsClientCreated Then
                Task.Run(Function() _TVDbApi_Movie.CreateAPI(_AddonSettings_Movie))
            End If
            If _TVDbApi_Movie.IsClientCreated Then
                'tmdbId = _TVDbApi_Movie.GetMovieID(imdbId)
            End If
        Else
            logger.Trace("[TVDbv4_Data] [GetTMDBID] [Abort] No IMDB ID to get the TMDB ID")
        End If
        logger.Trace("[TVDbv4_Data] [GetTMDBID] [Done]")
        Return New Interfaces.AddonResult_Generic
    End Function

    Function Scraper_Movie(ByRef dbElement As Database.DBElement,
                           ByRef scrapeModifiers As Structures.ScrapeModifiers,
                           ByRef scrapeType As Enums.ScrapeType,
                           ByRef scrapeOptions As Structures.ScrapeOptions
                           ) As Interfaces.AddonResult_Data_Scraper_Movie Implements Interfaces.IAddon_Data_Scraper_Movie.Scraper_Movie
        logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Start]")
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, ConfigScrapeOptions_Movie)
        Dim Result As MediaContainers.Movie = Nothing

        _TVDbApi_Movie.DefaultLanguage = dbElement.Language_Main

        If scrapeModifiers.MainNFO AndAlso Not scrapeModifiers.DoSearch Then
            If dbElement.Movie.UniqueIDs.TVDbIdSpecified Then
                'TVDb ID already available -> scrape and save data into an empty movie container (nMovie)
                Result = _TVDbApi_Movie.GetInfo_Movie(dbElement.Movie.UniqueIDs.TVDbId, FilteredOptions)
            ElseIf dbElement.Movie.UniqueIDs.IMDbIdSpecified Then
                'IMDb ID already available -> scrape and save data into an empty movie container (nMovie)
                Result = _TVDbApi_Movie.GetInfo_Movie_By_IMDbId(dbElement.Movie.UniqueIDs.IMDbId, FilteredOptions)
            ElseIf dbElement.Movie.UniqueIDs.TMDbIdSpecified Then
                'TMDb ID already available -> scrape and save data into an empty movie container (nMovie)
                Result = _TVDbApi_Movie.GetInfo_Movie_By_TMDbId(dbElement.Movie.UniqueIDs.TMDbId, FilteredOptions)
            ElseIf Not scrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDb, IMDb or TMDb ID for movie --> search first and try to get ID!
                If dbElement.Movie.TitleSpecified Then
                    Result = _TVDbApi_Movie.Process_SearchResults_Movie(dbElement.Movie.Title, dbElement, scrapeType, FilteredOptions)
                End If
                'if still no search result -> exit
                If Result Is Nothing Then
                    logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Abort] No result found")
                    Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
                End If
            End If
        End If

        If Result Is Nothing Then
            Select Case scrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Abort] No result found")
                    Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
            End Select
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_Movie(Result)
        End If

        If scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto Then
            If Not dbElement.Movie.UniqueIDs.TVDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults(_TVDbApi_Movie, "tvdb", New List(Of String) From {"TVDb", "IMDb"}, Enums.ContentType.Movie)
                    Select Case dlgSearch.ShowDialog(dbElement.Movie.Title, dbElement.Filename, dbElement.Movie.Year)
                        Case DialogResult.Cancel
                            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_Movie] [Cancelled] Cancelled by user"))
                            Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.Cancelled)
                        Case DialogResult.OK
                            logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Done]")
                            Return New Interfaces.AddonResult_Data_Scraper_Movie(_TVDbApi_Movie.GetInfo_Movie(dlgSearch.Result_Movie.UniqueIDs.TVDbId.ToString, FilteredOptions))
                        Case DialogResult.Retry
                            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_Movie] [Skipped] Skipped by user"))
                            Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.Skipped)
                    End Select
                End Using
            End If
        End If

        If Result IsNot Nothing Then
            logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_Movie(Result)
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_Movie] [Abort] No result found")
            Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
        End If
    End Function

    Public Function Scraper_TVEpisode(ByRef dbElement As Database.DBElement,
                                      ByVal scrapeOptions As Structures.ScrapeOptions
                                      ) As Interfaces.AddonResult_Data_Scraper_TVEpisode Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVEpisode
        logger.Trace("[TVDbv4_Data] [Scraper_TVEpisode] [Start]")
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, ConfigScrapeOptions_TV)
        Dim Result As New MediaContainers.EpisodeDetails

        _TVDbApi_TV.DefaultLanguage = dbElement.Language_Main

        'If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
        '    'oDBElement.TVShow.UniqueIDs.TMDbId = _TVDbApi_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
        'End If

        If dbElement.TVEpisode.UniqueIDs.TVDbIdSpecified Then
            'get episode info by episode TVDb ID
            Result = _TVDbApi_TV.GetInfo_TVEpisode(dbElement.TVEpisode.UniqueIDs.TVDbId, FilteredOptions)
        ElseIf dbElement.TVEpisode.EpisodeSpecified AndAlso dbElement.TVEpisode.SeasonSpecified Then
            'get episode info by season and episode number
        ElseIf dbElement.TVEpisode.AiredSpecified Then
            'get episode info by episode aired date
            Result = _TVDbApi_TV.GetInfo_TVEpisode_By_Aired(dbElement.TVShow.UniqueIDs.TVDbId, dbElement.TVEpisode.Aired, FilteredOptions)
        Else
            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVEpisode] [Abort] No result found"))
            Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
        End If

        'If dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
        '    If dbElement.TVEpisode.UniqueIDs.TVDbIdSpecified Then
        '        Result = _TVDbApi_TV.GetInfo_TVEpisode(dbElement.TVEpisode.UniqueIDs.TVDbId, FilteredOptions)
        '    ElseIf Not dbElement.TVEpisode.Episode = -1 AndAlso Not dbElement.TVEpisode.Season = -1 Then
        '        'nTVEpisode = _TVDbApi_TV.GetInfo_TVEpisode(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVEpisode.Season, oDBElement.TVEpisode.Episode, FilteredOptions)
        '    ElseIf dbElement.TVEpisode.AiredSpecified Then
        '        'nTVEpisode = _TVDbApi_TV.GetInfo_TVEpisode(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVEpisode.Aired, FilteredOptions)
        '    Else
        '        logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVEpisode] [Abort] No result found"))
        '        Return New Interfaces.ModuleResult_Data_TVEpisode(Interfaces.ResultStatus.NoResult)
        '    End If
        '    'if still no search result -> exit
        '    If Result Is Nothing Then
        '        logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVEpisode] [Abort] No result found"))
        '        Return New Interfaces.ModuleResult_Data_TVEpisode(Interfaces.ResultStatus.NoResult)
        '    End If
        'Else
        '    logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVEpisode] [Abort] No TV Show TMDb ID available"))
        '    Return New Interfaces.ModuleResult_Data_TVEpisode(Interfaces.ResultStatus.NoResult)
        'End If

        If Result IsNot Nothing Then
            logger.Trace("[TVDbv4_Data] [Scraper_TVEpisode] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Result)
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_TVEpisode] [Abort] No result found")
            Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
        End If
    End Function

    Public Function Scraper_TVSeason(ByRef dbElement As Database.DBElement,
                                     ByVal scrapeOptions As Structures.ScrapeOptions
                                     ) As Interfaces.AddonResult_Data_Scraper_TVSeason Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVSeason
        logger.Trace("[TVDbv4_Data] [Scraper_TVSeason] [Start]")
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, ConfigScrapeOptions_TV)
        Dim Result As New MediaContainers.SeasonDetails

        _TVDbApi_TV.DefaultLanguage = dbElement.Language_Main

        If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
            'oDBElement.TVShow.UniqueIDs.TMDbId = _TVDbApi_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
        End If

        If dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
            If dbElement.TVSeason.SeasonSpecified Then
                'nTVSeason = _TVDbApi_TV.GetInfo_TVSeason(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVSeason.Season, FilteredOptions)
            Else
                logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVSeason] [Abort] Season number is not specified"))
                Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
            End If
            'if still no search result -> exit
            If Result Is Nothing Then
                logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVSeason] [Abort] No result found"))
                Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
            End If
        Else
            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TVSeason] [Abort] No TV Show TVDb ID available"))
            Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
        End If

        If Result IsNot Nothing Then
            logger.Trace("[TVDbv4_Data] [Scraper_TVSeason] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Result)
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_TVSeason] [Abort] No result found")
            Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
        End If
    End Function

    Function Scraper_TVShow(ByRef dbElement As Database.DBElement,
                            ByRef scrapeModifiers As Structures.ScrapeModifiers,
                            ByRef scrapeType As Enums.ScrapeType,
                            ByRef scrapeOptions As Structures.ScrapeOptions
                            ) As Interfaces.AddonResult_Data_Scraper_TVShow Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVShow
        logger.Trace("[TVDbv4_Data] [Scraper_TV] [Start]")
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, ConfigScrapeOptions_TV)
        Dim Result As MediaContainers.TVShow = Nothing

        _TVDbApi_TV.DefaultLanguage = dbElement.Language_Main

        If Not scrapeModifiers.DoSearch AndAlso
            (scrapeModifiers.MainNFO OrElse
            (scrapeModifiers.withEpisodes AndAlso scrapeModifiers.EpisodeNFO) OrElse
            (scrapeModifiers.withSeasons AndAlso scrapeModifiers.SeasonNFO)) Then
            If dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
                'TVDb ID already available -> scrape and save data into an empty tv show container (nShow)
                Result = _TVDbApi_TV.GetInfo_TVShow(dbElement.TVShow.UniqueIDs.TVDbId, FilteredOptions, scrapeModifiers)
            ElseIf dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
                'oDBElement.TVShow.UniqueIDs.TMDbId = _TVDbApi_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
                If Not dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
                'nTVShow = _TVDbApi_TV.GetInfo_TVShow(oDBElement.TVShow.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf dbElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                'oDBElement.TVShow.UniqueIDs.TMDbId = _TVDbApi_TV.GetTMDBbyIMDB(oDBElement.TVShow.UniqueIDs.IMDbId)
                If Not dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
                'nTVShow = _TVDbApi_TV.GetInfo_TVShow(oDBElement.TVShow.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not scrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If dbElement.TVShow.TitleSpecified Then
                    'nTVShow = _TVDbApi_TV.GetSearchTVShowInfo(oDBElement.TVShow.Title, oDBElement, ScrapeType, ScrapeModifiers, FilteredOptions)
                End If
                'if still no search result -> exit
                If Result Is Nothing Then
                    logger.Trace("[TVDbv4_Data] [Scraper_TV] [Abort] No result found")
                    Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
                End If
            End If
        End If

        If Result Is Nothing Then
            Select Case scrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TV] [Abort] No result found"))
                    Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
            End Select
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_TV] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_TVShow(Result)
        End If

        If scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto Then
            If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults(_TVDbApi_TV, "tvdb", New List(Of String) From {"TVDb", "IMDb"}, Enums.ContentType.TVShow)
                    Select Case dlgSearch.ShowDialog(dbElement.TVShow.Title, dbElement.ShowPath)
                        Case DialogResult.Cancel
                            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TV] [Cancelled] Cancelled by user"))
                            Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.Cancelled)
                        Case DialogResult.OK
                            logger.Trace("[TVDbv4_Data] [Scraper_TV] [Done]")
                            Return New Interfaces.AddonResult_Data_Scraper_TVShow(_TVDbApi_TV.GetInfo_TVShow(dlgSearch.Result_TVShow.UniqueIDs.TVDbId, FilteredOptions, scrapeModifiers))
                        Case DialogResult.Retry
                            logger.Trace(String.Format("[TVDbv4_Data] [Scraper_TV] [Skipped] Skipped by user"))
                            Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.Skipped)
                    End Select
                End Using
            End If
        End If

        If Result IsNot Nothing Then
            logger.Trace("[TVDbv4_Data] [Scraper_TV] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_TVShow(Result)
        Else
            logger.Trace("[TVDbv4_Data] [Scraper_TV] [Abort] No result found")
            Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
        End If
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements Interfaces.IAddon_Data_Scraper_Movie.OrderChanged
        _setup_Movie.OrderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements Interfaces.IAddon_Data_Scraper_TV.ScraperOrderChanged
        _setup_TV.Order_Changed()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim ApiPin As String
        Dim FallBackEng As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class