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
' ###############################################################################

Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class TMDB_Image
    Implements Interfaces.ScraperModule_Image_Movie
    Implements Interfaces.ScraperModule_Image_MovieSet
    Implements Interfaces.ScraperModule_Image_TV


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigModifier_MovieSet As New Structures.ScrapeModifiers
    Public Shared ConfigModifier_TV As New Structures.ScrapeModifiers

    Private strPrivateAPIKey As String = String.Empty
    Private _SpecialSettings_Movie As New AddonSettings
    Private _SpecialSettings_MovieSet As New AddonSettings
    Private _SpecialSettings_TV As New AddonSettings
    Private _Name As String = "TMDB_Image"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV
    Private _TMDBAPI_Movie As New clsAPITMDB
    Private _TMDBAPI_MovieSet As New clsAPITMDB
    Private _TMDBAPI_TV As New clsAPITMDB

    Private Const _strAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged
    Public Event MovieScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent
    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart
    Public Event ImagesDownloaded_Movie(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded
    Public Event ProgressUpdated_Movie(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.ModuleSettingsChanged
    Public Event MovieScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEvent
    Public Event SetupScraperChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperSetupChanged
    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.SetupNeedsRestart
    Public Event ImagesDownloaded_MovieSet(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_MovieSet.ImagesDownloaded
    Public Event ProgressUpdated_MovieSet(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ProgressUpdated

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Image_TV.ModuleSettingsChanged
    Public Event MovieScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_TV.ScraperEvent
    Public Event SetupScraperChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.ScraperModule_Image_TV.SetupNeedsRestart
    Public Event ImagesDownloaded_TV(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_TV.ImagesDownloaded
    Public Event ProgressUpdated_TV(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_TV.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleName, Interfaces.ScraperModule_Image_MovieSet.ModuleName, Interfaces.ScraperModule_Image_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleVersion, Interfaces.ScraperModule_Image_MovieSet.ModuleVersion, Interfaces.ScraperModule_Image_TV.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Image_Movie.ScraperEnabled
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

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEnabled
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

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Image_TV.ScraperEnabled
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

    Function QueryScraperCapabilities_Movie(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_Movie.MainFanart
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_Movie.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_MovieSet(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_MovieSet.MainFanart
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_MovieSet.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_TV(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.EpisodePoster
                Return ConfigModifier_TV.EpisodePoster
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_TV.MainFanart
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_TV.MainPoster
            Case Enums.ModifierType.SeasonPoster
                Return ConfigModifier_TV.SeasonPoster
        End Select
        Return False
    End Function

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
        RaiseEvent SetupScraperChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent SetupScraperChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent SetupScraperChanged_TV(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_Movie.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkScrapeFanart.Checked = ConfigModifier_Movie.MainFanart
        _setup_Movie.chkScrapePoster.Checked = ConfigModifier_Movie.MainPoster
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_Movie.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_Movie.lblEMMAPI.Visible = False
            _setup_Movie.txtApiKey.Enabled = True
        End If

        _setup_Movie.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_Movie")
        Spanel.Text = "TMDB"
        Spanel.Prefix = "TMDBMovieMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieMedia"
        Spanel.Type = Master.eLang.GetString(36, "Movies")
        Spanel.ImageIndex = If(Me._ScraperEnabled_Movie, 9, 10)
        Spanel.Panel = Me._setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        AddHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
        Return Spanel
    End Function

    Function InjectSetupScraper_MovieSet() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_MovieSet.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_MovieSet = New frmSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.chkEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.chkScrapeFanart.Checked = ConfigModifier_MovieSet.MainFanart
        _setup_MovieSet.chkScrapePoster.Checked = ConfigModifier_MovieSet.MainPoster
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_MovieSet.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_MovieSet.lblEMMAPI.Visible = False
            _setup_MovieSet.txtApiKey.Enabled = True
        End If

        _setup_MovieSet.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_MovieSet")
        Spanel.Text = "TMDB"
        Spanel.Prefix = "TMDBMovieSetMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieSetMedia"
        Spanel.Type = Master.eLang.GetString(1203, "MovieSets")
        Spanel.ImageIndex = If(Me._ScraperEnabled_MovieSet, 9, 10)
        Spanel.Panel = Me._setup_MovieSet.pnlSettings

        AddHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
        AddHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
        AddHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
        Return Spanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_TV.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkScrapeEpisodePoster.Checked = ConfigModifier_TV.EpisodePoster
        _setup_TV.chkScrapeSeasonPoster.Checked = ConfigModifier_TV.SeasonPoster
        _setup_TV.chkScrapeShowFanart.Checked = ConfigModifier_TV.MainFanart
        _setup_TV.chkScrapeShowPoster.Checked = ConfigModifier_TV.MainPoster
        _setup_TV.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_TV.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_TV.lblEMMAPI.Visible = False
            _setup_TV.txtApiKey.Enabled = True
        End If

        _setup_TV.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_TV")
        Spanel.Text = "TMDB"
        Spanel.Prefix = "TMDBTVMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlTVMedia"
        Spanel.Type = Master.eLang.GetString(653, "TV Shows")
        Spanel.ImageIndex = If(Me._ScraperEnabled_TV, 9, 10)
        Spanel.Panel = Me._setup_TV.pnlSettings

        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        AddHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
        Return Spanel
    End Function

    Sub LoadSettings_Movie()
        ConfigModifier_Movie.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainExtrafanarts = ConfigModifier_Movie.MainFanart
        ConfigModifier_Movie.MainExtrathumbs = ConfigModifier_Movie.MainFanart
        ConfigModifier_Movie.MainKeyart = ConfigModifier_Movie.MainPoster

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
    End Sub

    Sub LoadSettings_MovieSet()
        ConfigModifier_MovieSet.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainExtrafanarts = ConfigModifier_MovieSet.MainFanart
        ConfigModifier_MovieSet.MainKeyart = ConfigModifier_MovieSet.MainPoster

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
    End Sub

    Sub LoadSettings_TV()
        ConfigModifier_TV.EpisodePoster = AdvancedSettings.GetBooleanSetting("DoEpisodePoster", True, , Enums.ContentType.TV)
        ConfigModifier_TV.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainExtrafanarts = ConfigModifier_TV.MainFanart
        ConfigModifier_TV.MainKeyart = ConfigModifier_TV.MainPoster

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _SpecialSettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)
    End Sub

    Function Scraper_Movie(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_Movie.Scraper
        logger.Trace("[TMDB_Image] [Scraper_Movie] [Start]")
        If Not DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            DBMovie.Movie.UniqueIDs.TMDbId = ModulesManager.Instance.GetMovieTMDbIdByIMDbId(DBMovie.Movie.UniqueIDs.IMDbId)
        End If

        If DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_Movie)
            ImagesContainer = _TMDBAPI_Movie.GetImages_Movie_MovieSet(DBMovie.Movie.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.Movie)
        End If

        logger.Trace("[TMDB_Image] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_MovieSet(ByRef DBMovieSet As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_MovieSet.Scraper
        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Start]")
        If Not DBMovieSet.MovieSet.UniqueIDs.TMDbIdSpecified AndAlso DBMovieSet.MoviesInSetSpecified Then
            DBMovieSet.MovieSet.UniqueIDs.TMDbId = ModulesManager.Instance.GetMovieCollectionID(DBMovieSet.MoviesInSet.Item(0).DBMovie.Movie.UniqueIDs.IMDbId)
        End If

        If DBMovieSet.MovieSet.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_MovieSet)
            ImagesContainer = _TMDBAPI_MovieSet.GetImages_Movie_MovieSet(DBMovieSet.MovieSet.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.MovieSet)
        End If

        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_TV(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_TV.Scraper
        logger.Trace("[TMDB_Image] [Scraper_TV] [Start]")
        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_TV)

        If DBTV.TVShow IsNot Nothing AndAlso Not DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
            If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                DBTV.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyTVDB(DBTV.TVShow.UniqueIDs.TVDbId)
            ElseIf DBTV.TVShow.UniqueIDs.IMDbIdSpecified Then
                DBTV.TVShow.UniqueIDs.TMDbId = _TMDBAPI_TV.GetTMDBbyIMDB(DBTV.TVShow.UniqueIDs.IMDbId)
            End If
        End If

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _TMDBAPI_TV.GetImages_TVEpisode(DBTV.TVShow.UniqueIDs.TMDbId, DBTV.TVEpisode.Season, DBTV.TVEpisode.Episode, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _TMDBAPI_TV.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TMDB ID exist to search: ", DBTV.TVEpisode.Title))
                End If
            Case Enums.ContentType.TVSeason
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _TMDBAPI_TV.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.TVSeason.Title))
                End If
            Case Enums.ContentType.TVShow
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _TMDBAPI_TV.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.TVShow.Title))
                End If
            Case Else
                logger.Error(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] Unhandled ContentType"))
        End Select

        logger.Trace("[TMDB_Image] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSettings_Movie()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigModifier_Movie.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_Movie.MainFanart, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigModifier_MovieSet.MainPoster, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_MovieSet.MainFanart, , , Enums.ContentType.MovieSet)
            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text, , , Enums.ContentType.MovieSet)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", ConfigModifier_TV.EpisodePoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonPoster", ConfigModifier_TV.SeasonPoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowFanart", ConfigModifier_TV.MainFanart, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowPoster", ConfigModifier_TV.MainPoster, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _setup_TV.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        ConfigModifier_Movie.MainPoster = _setup_Movie.chkScrapePoster.Checked
        ConfigModifier_Movie.MainFanart = _setup_Movie.chkScrapeFanart.Checked
        ConfigModifier_Movie.MainExtrafanarts = ConfigModifier_Movie.MainFanart
        ConfigModifier_Movie.MainExtrathumbs = ConfigModifier_Movie.MainFanart
        ConfigModifier_Movie.MainKeyart = ConfigModifier_Movie.MainPoster

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_Movie.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_Movie.txtApiKey.Text.Trim
        _SpecialSettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_Movie()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_Movie.CreateAPI(_SpecialSettings_Movie))

        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            RemoveHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_MovieSet.SaveSetupScraper
        ConfigModifier_MovieSet.MainPoster = _setup_MovieSet.chkScrapePoster.Checked
        ConfigModifier_MovieSet.MainFanart = _setup_MovieSet.chkScrapeFanart.Checked
        ConfigModifier_MovieSet.MainExtrafanarts = ConfigModifier_MovieSet.MainFanart
        ConfigModifier_MovieSet.MainKeyart = ConfigModifier_MovieSet.MainPoster

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_MovieSet.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_MovieSet.txtApiKey.Text.Trim
        _SpecialSettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_MovieSet()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_MovieSet.CreateAPI(_SpecialSettings_MovieSet))

        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            RemoveHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_TV.SaveSetupScraper
        ConfigModifier_TV.EpisodePoster = _setup_TV.chkScrapeEpisodePoster.Checked
        ConfigModifier_TV.SeasonPoster = _setup_TV.chkScrapeSeasonPoster.Checked
        ConfigModifier_TV.MainFanart = _setup_TV.chkScrapeShowFanart.Checked
        ConfigModifier_TV.MainPoster = _setup_TV.chkScrapeShowPoster.Checked
        ConfigModifier_TV.MainExtrafanarts = ConfigModifier_TV.MainFanart
        ConfigModifier_TV.MainKeyart = ConfigModifier_TV.MainPoster

        Dim bAPIKeyChanged = Not strPrivateAPIKey = _setup_TV.txtApiKey.Text.Trim
        strPrivateAPIKey = _setup_TV.txtApiKey.Text.Trim
        _SpecialSettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), _strAPIKey, strPrivateAPIKey)

        SaveSettings_TV()

        If bAPIKeyChanged Then Task.Run(Function() _TMDBAPI_TV.CreateAPI(_SpecialSettings_TV))
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            RemoveHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Image_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Image_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements EmberAPI.Interfaces.ScraperModule_Image_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim APIKey As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class