﻿' ################################################################################
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

Public Class TMDB_Data
    Implements Interfaces.ScraperModule_Data_Movie
    Implements Interfaces.ScraperModule_Data_MovieSet
    Implements Interfaces.ScraperModule_Data_TV


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigScrapeOptions_Movie As New Structures.ScrapeOptions
    Public Shared ConfigScrapeOptions_MovieSet As New Structures.ScrapeOptions
    Public Shared ConfigScrapeOptions_TV As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_MovieSet As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifiers

    Private strPrivateAPIKey As String = String.Empty
    Private _SpecialSettings_Movie As New SpecialSettings
    Private _SpecialSettings_MovieSet As New SpecialSettings
    Private _SpecialSettings_TV As New SpecialSettings
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV
    Private _TMDBAPI_Movie As New Scraper
    Private _TMDBAPI_MovieSet As New Scraper
    Private _TMDBAPI_TV As New Scraper

    Private Const _strAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged
    Public Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_Movie.ScraperEvent
    Public Event ScraperSetupChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Data_Movie.SetupNeedsRestart

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.ModuleSettingsChanged
    Public Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperEvent
    Public Event ScraperSetupChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperSetupChanged
    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.SetupNeedsRestart

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
    Public Event ScraperSetupChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.ScraperModule_Data_TV.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleName, Interfaces.ScraperModule_Data_MovieSet.ModuleName, Interfaces.ScraperModule_Data_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleVersion, Interfaces.ScraperModule_Data_MovieSet.ModuleVersion, Interfaces.ScraperModule_Data_TV.ModuleVersion
        Get
            Return Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Data_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
            If _ScraperEnabled_Movie Then
                Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))
            End If
        End Set
    End Property

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Data_MovieSet.ScraperEnabled
        Get
            Return _ScraperEnabled_MovieSet
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_MovieSet = value
            If _ScraperEnabled_MovieSet Then
                Task.Run(Function() _TMDBAPI_MovieSet.CreateAPI(_SpecialSettings_MovieSet))
            End If
        End Set
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Data_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
            If _ScraperEnabled_TV Then
                Task.Run(Function() _TMDBAPI_TV.CreateAPI(_SpecialSettings_TV))
            End If
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub Handle_ModuleSettingsChanged_Movie()
        RaiseEvent ModuleSettingsChanged_Movie()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_MovieSet()
        RaiseEvent ModuleSettingsChanged_MovieSet()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_TV()
        RaiseEvent ModuleSettingsChanged_TV()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_MovieSet()
        RaiseEvent SetupNeedsRestart_MovieSet()
    End Sub

    Private Sub Handle_SetupNeedsRestart_TV()
        RaiseEvent SetupNeedsRestart_TV()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(_Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent ScraperSetupChanged_MovieSet(String.Concat(_Name, "_MovieSet"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent ScraperSetupChanged_TV(String.Concat(_Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_Movie.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkActors.Checked = ConfigScrapeOptions_Movie.bMainActors
        _setup_Movie.chkCollectionID.Checked = ConfigScrapeOptions_Movie.bMainCollectionID
        _setup_Movie.chkCountries.Checked = ConfigScrapeOptions_Movie.bMainCountries
        _setup_Movie.chkDirectors.Checked = ConfigScrapeOptions_Movie.bMainDirectors
        _setup_Movie.chkFallBackEng.Checked = _SpecialSettings_Movie.FallBackEng
        _setup_Movie.chkGenres.Checked = ConfigScrapeOptions_Movie.bMainGenres
        _setup_Movie.chkGetAdultItems.Checked = _SpecialSettings_Movie.GetAdultItems
        _setup_Movie.chkCertifications.Checked = ConfigScrapeOptions_Movie.bMainMPAA
        _setup_Movie.chkOriginalTitle.Checked = ConfigScrapeOptions_Movie.bMainOriginalTitle
        _setup_Movie.chkPlot.Checked = ConfigScrapeOptions_Movie.bMainPlot
        _setup_Movie.chkPremiered.Checked = ConfigScrapeOptions_Movie.bMainPremiered
        _setup_Movie.chkRating.Checked = ConfigScrapeOptions_Movie.bMainRating
        _setup_Movie.chkRuntime.Checked = ConfigScrapeOptions_Movie.bMainRuntime
        _setup_Movie.chkSearchDeviant.Checked = _SpecialSettings_Movie.SearchDeviant
        _setup_Movie.chkStudios.Checked = ConfigScrapeOptions_Movie.bMainStudios
        _setup_Movie.chkTagline.Checked = ConfigScrapeOptions_Movie.bMainTagline
        _setup_Movie.chkTitle.Checked = ConfigScrapeOptions_Movie.bMainTitle
        _setup_Movie.chkTrailer.Checked = ConfigScrapeOptions_Movie.bMainTrailer
        _setup_Movie.chkWriters.Checked = ConfigScrapeOptions_Movie.bMainWriters
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_Movie.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_Movie.lblEMMAPI.Visible = False
            _setup_Movie.txtApiKey.Enabled = True
        End If

        _setup_Movie.orderChanged()

        SPanel.Name = String.Concat(_Name, "_Movie")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBMovieInfo_"
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

    Function InjectSetupScraper_MovieSet() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_MovieSet.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_MovieSet = New frmSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.chkEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.chkFallBackEng.Checked = _SpecialSettings_MovieSet.FallBackEng
        _setup_MovieSet.chkGetAdultItems.Checked = _SpecialSettings_MovieSet.GetAdultItems
        _setup_MovieSet.chkPlot.Checked = ConfigScrapeOptions_MovieSet.bMainPlot
        _setup_MovieSet.chkTitle.Checked = ConfigScrapeOptions_MovieSet.bMainTitle
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_MovieSet.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_MovieSet.lblEMMAPI.Visible = False
            _setup_MovieSet.txtApiKey.Enabled = True
        End If

        _setup_MovieSet.orderChanged()

        SPanel.Name = String.Concat(_Name, "_MovieSet")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBMovieSetInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieSetData"
        SPanel.Type = Master.eLang.GetString(1203, "MovieSets")
        SPanel.ImageIndex = If(_ScraperEnabled_MovieSet, 9, 10)
        SPanel.Panel = _setup_MovieSet.pnlSettings

        AddHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
        AddHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
        AddHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
        Return SPanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkFallBackEng.Checked = _SpecialSettings_TV.FallBackEng
        _setup_TV.chkGetAdultItems.Checked = _SpecialSettings_TV.GetAdultItems
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
        _setup_TV.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_TV.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_TV.lblEMMAPI.Visible = False
            _setup_TV.txtApiKey.Enabled = True
        End If

        _setup_TV.orderChanged()

        SPanel.Name = String.Concat(_Name, "_TV")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBTVInfo_"
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
        ConfigScrapeOptions_Movie.bMainActors = AdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainCollectionID = AdvancedSettings.GetBooleanSetting("DoCollectionID", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainCountries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainDirectors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainGenres = AdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainOriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainPremiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainStudios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        ConfigScrapeOptions_Movie.bMainWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
        _SpecialSettings_Movie.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.SearchDeviant = AdvancedSettings.GetBooleanSetting("SearchDeviant", False, , Enums.ContentType.Movie)
    End Sub

    Sub LoadSettings_MovieSet()
        ConfigScrapeOptions_MovieSet.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.MovieSet)
        ConfigScrapeOptions_MovieSet.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.MovieSet)

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
    End Sub

    Sub LoadSettings_TV()
        ConfigScrapeOptions_TV.bEpisodeActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeAired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeCredits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeDirectors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeGuestStars = AdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodePlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bEpisodeTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        ConfigScrapeOptions_TV.bSeasonAired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bSeasonPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bSeasonTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVSeason)
        ConfigScrapeOptions_TV.bMainActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCountries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainCreators = AdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainEpisodeGuide = AdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainGenres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainOriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainPremiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainStatus = AdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainStudios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.TVShow)
        ConfigScrapeOptions_TV.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _SpecialSettings_TV.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.TV)
        _SpecialSettings_TV.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.TV)
        _SpecialSettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigScrapeOptions_Movie.bMainActors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", ConfigScrapeOptions_Movie.bMainCertifications, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCollectionID", ConfigScrapeOptions_Movie.bMainCollectionID, , , Enums.ContentType.Movie)
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
            settings.SetBooleanSetting("FallBackEn", _SpecialSettings_Movie.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_Movie.GetAdultItems, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchDeviant", _SpecialSettings_Movie.SearchDeviant, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text.Trim, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions_MovieSet.bMainPlot, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoTitle", ConfigScrapeOptions_MovieSet.bMainTitle, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_MovieSet.GetAdultItems, , , Enums.ContentType.MovieSet)
            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text.Trim, , , Enums.ContentType.MovieSet)
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
            settings.SetBooleanSetting("FallBackEn", _SpecialSettings_TV.FallBackEng, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_TV.GetAdultItems, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _setup_TV.txtApiKey.Text.Trim, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigScrapeOptions_Movie.bMainActors = _setup_Movie.chkActors.Checked
        ConfigScrapeOptions_Movie.bMainCertifications = _setup_Movie.chkCertifications.Checked
        ConfigScrapeOptions_Movie.bMainCollectionID = _setup_Movie.chkCollectionID.Checked
        ConfigScrapeOptions_Movie.bMainCountries = _setup_Movie.chkCountries.Checked
        ConfigScrapeOptions_Movie.bMainDirectors = _setup_Movie.chkDirectors.Checked
        ConfigScrapeOptions_Movie.bMainGenres = _setup_Movie.chkGenres.Checked
        ConfigScrapeOptions_Movie.bMainMPAA = _setup_Movie.chkCertifications.Checked
        ConfigScrapeOptions_Movie.bMainOriginalTitle = _setup_Movie.chkOriginalTitle.Checked
        ConfigScrapeOptions_Movie.bMainOutline = _setup_Movie.chkPlot.Checked
        ConfigScrapeOptions_Movie.bMainPlot = _setup_Movie.chkPlot.Checked
        ConfigScrapeOptions_Movie.bMainPremiered = _setup_Movie.chkPremiered.Checked
        ConfigScrapeOptions_Movie.bMainRating = _setup_Movie.chkRating.Checked
        ConfigScrapeOptions_Movie.bMainRuntime = _setup_Movie.chkRuntime.Checked
        ConfigScrapeOptions_Movie.bMainStudios = _setup_Movie.chkStudios.Checked
        ConfigScrapeOptions_Movie.bMainTagline = _setup_Movie.chkTagline.Checked
        ConfigScrapeOptions_Movie.bMainTitle = _setup_Movie.chkTitle.Checked
        ConfigScrapeOptions_Movie.bMainTop250 = False
        ConfigScrapeOptions_Movie.bMainTrailer = _setup_Movie.chkTrailer.Checked
        ConfigScrapeOptions_Movie.bMainWriters = _setup_Movie.chkWriters.Checked

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_Movie.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_Movie.txtApiKey.Text.Trim
        _SpecialSettings_Movie.FallBackEng = _setup_Movie.chkFallBackEng.Checked
        _SpecialSettings_Movie.GetAdultItems = _setup_Movie.chkGetAdultItems.Checked
        _SpecialSettings_Movie.SearchDeviant = _setup_Movie.chkSearchDeviant.Checked
        _SpecialSettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_Movie()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))

        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_MovieSet.SaveSetupScraper
        ConfigScrapeOptions_MovieSet.bMainPlot = _setup_MovieSet.chkPlot.Checked
        ConfigScrapeOptions_MovieSet.bMainTitle = _setup_MovieSet.chkTitle.Checked

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_MovieSet.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_MovieSet.txtApiKey.Text.Trim
        _SpecialSettings_MovieSet.FallBackEng = _setup_MovieSet.chkFallBackEng.Checked
        _SpecialSettings_MovieSet.GetAdultItems = _setup_MovieSet.chkGetAdultItems.Checked
        _SpecialSettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_MovieSet()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_MovieSet.CreateAPI(_SpecialSettings_MovieSet))

        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movieset
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_TV.SaveSetupScraper
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

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_TV.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_TV.txtApiKey.Text.Trim
        _SpecialSettings_TV.FallBackEng = _setup_TV.chkFallBackEng.Checked
        _SpecialSettings_TV.GetAdultItems = _setup_TV.chkGetAdultItems.Checked
        _SpecialSettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_TV()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_TV.CreateAPI(_SpecialSettings_TV))

        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef sStudio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        logger.Trace("[TMDB_Data] [GetMovieStudio] [Start]")
        If Not DBMovie.Movie.UniqueIDsSpecified Then
            logger.Trace("[TMDB_Data] [GetMovieStudio] [Abort] Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If
        If Not _TMDBAPI_Movie.IsClientCreated Then
            Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))
        End If
        If _TMDBAPI_Movie.IsClientCreated Then
            If DBMovie.Movie.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID is available
                sStudio.AddRange(_TMDBAPI_Movie.GetMovieStudios(DBMovie.Movie.UniqueIDs.IMDbId))
            ElseIf DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID is available
                sStudio.AddRange(_TMDBAPI_Movie.GetMovieStudios(DBMovie.Movie.UniqueIDs.TMDbId.ToString))
            End If
        End If
        logger.Trace("[TMDB_Data] [GetMovieStudio] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDbIdByIMDbId(ByVal imdbId As String, ByRef tmdbId As Integer) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDbIdByIMDbId
        logger.Trace("[TMDB_Data] [GetTMDBID] [Start]")
        If Not String.IsNullOrEmpty(imdbId) Then
            If Not _TMDBAPI_Movie.IsClientCreated Then
                Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))
            End If
            If _TMDBAPI_Movie.IsClientCreated Then
                tmdbId = _TMDBAPI_Movie.GetMovieID(imdbId)
            End If
        Else
            logger.Trace("[TMDB_Data] [GetTMDBID] [Abort] No IMDB ID to get the TMDB ID")
        End If
        logger.Trace("[TMDB_Data] [GetTMDBID] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetCollectionID(ByVal imdbIdOrTmdbId As String, ByRef tmdbCollectionId As Integer) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.GetTMDbCollectionId
        logger.Trace("[TMDB_Data] [GetCollectionID] [Start]")
        If Not String.IsNullOrEmpty(imdbIdOrTmdbId) Then
            If Not _TMDBAPI_Movie.IsClientCreated Then
                Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))
            End If
            If _TMDBAPI_Movie.IsClientCreated Then
                tmdbCollectionId = _TMDBAPI_MovieSet.GetMovieCollectionID(imdbIdOrTmdbId)
                If tmdbCollectionId = -1 Then
                    logger.Trace("[TMDB_Data] [GetCollectionID] [Abort] nor search result")
                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                End If
            End If
        End If
        logger.Trace("[TMDB_Data] [GetCollectionID] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_Movie(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.ScraperModule_Data_Movie.Scraper_Movie
        logger.Trace("[TMDB_Data] [Scraper_Movie] [Start]")
        Dim nMovie As MediaContainers.Movie = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_Movie)

        _TMDBAPI_Movie.DefaultLanguage = oDBElement.Language

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.Movie.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _TMDBAPI_Movie.GetInfo_Movie(oDBElement.Movie.UniqueIDs.TMDbId.ToString, FilteredOptions, False)
            ElseIf oDBElement.Movie.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _TMDBAPI_Movie.GetInfo_Movie(oDBElement.Movie.UniqueIDs.IMDbId, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID or TMDB-ID for movie --> search first and try to get ID!
                If oDBElement.Movie.TitleSpecified Then
                    nMovie = _TMDBAPI_Movie.GetSearchMovieInfo(oDBElement.Movie.Title, oDBElement, ScrapeType, FilteredOptions)
                End If
                'if still no search result -> exit
                If nMovie Is Nothing Then
                    logger.Trace("[TMDB_Data] [Scraper_Movie] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
                End If
            End If
        End If

        If nMovie Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    logger.Trace("[TMDB_Data] [Scraper_Movie] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
            End Select
        Else
            logger.Trace("[TMDB_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.Movie.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgTMDBSearchResults_Movie(_SpecialSettings_Movie, _TMDBAPI_Movie)
                    If dlgSearch.ShowDialog(oDBElement.Movie.Title, oDBElement.Filename, FilteredOptions, oDBElement.Movie.Year) = DialogResult.OK Then
                        nMovie = _TMDBAPI_Movie.GetInfo_Movie(dlgSearch.Result.UniqueIDs.TMDbId.ToString, FilteredOptions, False)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        logger.Trace(String.Format("[TMDB_Data] [Scraper_Movie] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_Movie With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        logger.Trace("[TMDB_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function

    Function Scraper_MovieSet(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_MovieSet Implements Interfaces.ScraperModule_Data_MovieSet.Scraper
        logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Start]")
        Dim nMovieSet As MediaContainers.Movieset = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_MovieSet)

        _TMDBAPI_MovieSet.DefaultLanguage = oDBElement.Language

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.MovieSet.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty movieset container (nMovieSet)
                nMovieSet = _TMDBAPI_MovieSet.GetInfo_Movieset(oDBElement.MovieSet.UniqueIDs.TMDbId, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no ITMDB-ID for movieset --> search first and try to get ID!
                If oDBElement.MovieSet.TitleSpecified Then
                    nMovieSet = _TMDBAPI_MovieSet.GetSearchMovieSetInfo(oDBElement.MovieSet.Title, oDBElement, ScrapeType, FilteredOptions)
                End If
                'if still no search result -> exit
                If nMovieSet Is Nothing Then
                    logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = Nothing}
                End If
            End If
        End If

        If nMovieSet Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = Nothing}
            End Select
        Else
            logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Done]")
            Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = nMovieSet}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.MovieSet.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgTMDBSearchResults_MovieSet(_SpecialSettings_MovieSet, _TMDBAPI_MovieSet)
                    If dlgSearch.ShowDialog(oDBElement.MovieSet.Title, FilteredOptions) = DialogResult.OK Then
                        nMovieSet = _TMDBAPI_MovieSet.GetInfo_Movieset(dlgSearch.Result.UniqueIDs.TMDbId, FilteredOptions, False)
                        'if a movieset is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_MovieSet With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = nMovieSet}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_TV(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("[TMDB_Data] [Scraper_TV] [Start]")
        Dim nTVShow As MediaContainers.TVShow = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        _TMDBAPI_TV.DefaultLanguage = oDBElement.Language

        If Not ScrapeModifiers.DoSearch AndAlso
            (ScrapeModifiers.MainNFO OrElse
            (ScrapeModifiers.withEpisodes AndAlso ScrapeModifiers.EpisodeNFO) OrElse
            (ScrapeModifiers.withSeasons AndAlso ScrapeModifiers.SeasonNFO)) Then
            If oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                nTVShow = _TMDBAPI_TV.GetInfo_TVShow(oDBElement.TVShow.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBElement.TVShow.UniqueIDs.TVDbIdSpecified Then
                oDBElement.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
                If Not oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _TMDBAPI_TV.GetInfo_TVShow(oDBElement.TVShow.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                oDBElement.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyIMDB(oDBElement.TVShow.UniqueIDs.IMDbId)
                If Not oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _TMDBAPI_TV.GetInfo_TVShow(oDBElement.TVShow.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If oDBElement.TVShow.TitleSpecified Then
                    nTVShow = _TMDBAPI_TV.GetSearchTVShowInfo(oDBElement.TVShow.Title, oDBElement, ScrapeType, ScrapeModifiers, FilteredOptions)
                End If
                'if still no search result -> exit
                If nTVShow Is Nothing Then
                    logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                End If
            End If
        End If

        If nTVShow Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End Select
        Else
            logger.Trace("[TMDB_Data] [Scraper_TV] [Done]")
            Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgTMDBSearchResults_TV(_SpecialSettings_TV, _TMDBAPI_TV)
                    If dlgSearch.ShowDialog(oDBElement.TVShow.Title, oDBElement.ShowPath, FilteredOptions) = DialogResult.OK Then
                        nTVShow = _TMDBAPI_TV.GetInfo_TVShow(dlgSearch.Result.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_TVShow With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        logger.Trace("[TMDB_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
    End Function

    Public Function Scraper_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("[TMDB_Data] [Scraper_TVEpisode] [Start]")
        Dim nTVEpisode As New MediaContainers.EpisodeDetails
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        _TMDBAPI_TV.DefaultLanguage = oDBElement.Language

        If Not oDBElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso oDBElement.TVShow.UniqueIDs.TVDbIdSpecified Then
            oDBElement.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
        End If

        If oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then
            If Not oDBElement.TVEpisode.Episode = -1 AndAlso Not oDBElement.TVEpisode.Season = -1 Then
                nTVEpisode = _TMDBAPI_TV.GetInfo_TVEpisode(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVEpisode.Season, oDBElement.TVEpisode.Episode, FilteredOptions)
            ElseIf oDBElement.TVEpisode.AiredSpecified Then
                nTVEpisode = _TMDBAPI_TV.GetInfo_TVEpisode(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVEpisode.Aired, FilteredOptions)
            Else
                logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
            'if still no search result -> exit
            If nTVEpisode Is Nothing Then
                logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
        Else
            logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No TV Show TMDB ID available"))
            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        End If

        logger.Trace("[TMDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Scraper_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.ScraperModule_Data_TV.Scraper_TVSeason
        logger.Trace("[TMDB_Data] [Scraper_TVSeason] [Start]")
        Dim nTVSeason As New MediaContainers.SeasonDetails
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        _TMDBAPI_TV.DefaultLanguage = oDBElement.Language

        If Not oDBElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso oDBElement.TVShow.UniqueIDs.TVDbIdSpecified Then
            oDBElement.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyTVDB(oDBElement.TVShow.UniqueIDs.TVDbId)
        End If

        If oDBElement.TVShow.UniqueIDs.TMDbIdSpecified Then
            If oDBElement.TVSeason.SeasonSpecified Then
                nTVSeason = _TMDBAPI_TV.GetInfo_TVSeason(oDBElement.TVShow.UniqueIDs.TMDbId, oDBElement.TVSeason.Season, FilteredOptions)
            Else
                logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] Season is not specified"))
                Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
            End If
            'if still no search result -> exit
            If nTVSeason Is Nothing Then
                logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
            End If
        Else
            logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] No TV Show TMDB ID available"))
            Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
        End If

        logger.Trace("[TMDB_Data] [Scraper_TVSeason] [Done]")
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = nTVSeason}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure SpecialSettings

#Region "Fields"

        Dim APIKey As String
        Dim FallBackEng As Boolean
        Dim GetAdultItems As Boolean
        Dim SearchDeviant As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class