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

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigModifier_MovieSet As New Structures.ScrapeModifier
    Public Shared ConfigModifier_TV As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings_Movie As New sMySettings
    Private _MySettings_MovieSet As New sMySettings
    Private _MySettings_TV As New sMySettings
    Private _Name As String = "TMDB_Image"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged

    Public Event MovieScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent

    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged

    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart

    Public Event ImagesDownloaded_Movie(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded

    Public Event ProgressUpdated_Movie(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.ModuleSettingsChanged

    Public Event MovieScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEvent

    Public Event SetupScraperChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperSetupChanged

    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.SetupNeedsRestart

    Public Event ImagesDownloaded_MovieSet(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_MovieSet.ImagesDownloaded

    Public Event ProgressUpdated_MovieSet(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ProgressUpdated

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Image_TV.ModuleSettingsChanged

    Public Event MovieScraperEvent_TV(ByVal eType As Enums.ScraperEventType_TV, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_TV.ScraperEvent

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
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Image_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
        End Set
    End Property

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEnabled
        Get
            Return _ScraperEnabled_MovieSet
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_MovieSet = value
        End Set
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Image_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities_Movie(ByVal cap As Enums.ScraperCapabilities_Movie_MovieSet) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities_Movie_MovieSet.Fanart
                Return ConfigModifier_Movie.MainFanart
            Case Enums.ScraperCapabilities_Movie_MovieSet.Poster
                Return ConfigModifier_Movie.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_MovieSet(ByVal cap As Enums.ScraperCapabilities_Movie_MovieSet) As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities_Movie_MovieSet.Fanart
                Return ConfigModifier_MovieSet.MainFanart
            Case Enums.ScraperCapabilities_Movie_MovieSet.Poster
                Return ConfigModifier_MovieSet.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_TV(ByVal cap As Enums.ScraperCapabilities_TV) As Boolean Implements Interfaces.ScraperModule_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities_TV.EpisodePoster
                Return ConfigModifier_TV.EpisodePoster
            Case Enums.ScraperCapabilities_TV.SeasonPoster
                Return ConfigModifier_TV.SeasonPoster
            Case Enums.ScraperCapabilities_TV.ShowFanart
                Return ConfigModifier_TV.MainFanart
            Case Enums.ScraperCapabilities_TV.ShowPoster
                Return ConfigModifier_TV.MainPoster
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
        _setup_Movie.API = _setup_Movie.txtApiKey.Text

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
        _setup_MovieSet.API = _setup_MovieSet.txtApiKey.Text

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
        _setup_TV.API = _setup_TV.txtApiKey.Text

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

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.Movie)
        _MySettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)

        ConfigModifier_Movie.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.Movie)
        ConfigModifier_Movie.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.Movie)
        ConfigModifier_Movie.MainEFanarts = ConfigModifier_Movie.MainFanart

    End Sub

    Sub LoadSettings_MovieSet()

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)

        ConfigModifier_MovieSet.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.MovieSet)
        ConfigModifier_MovieSet.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.MovieSet)

    End Sub

    Sub LoadSettings_TV()

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.TV)
        _MySettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)

        ConfigModifier_TV.EpisodePoster = clsAdvancedSettings.GetBooleanSetting("DoEpisodePoster", True, , Enums.Content_Type.TV)
        ConfigModifier_TV.SeasonPoster = clsAdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.Content_Type.TV)
        ConfigModifier_TV.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.Content_Type.TV)
        ConfigModifier_TV.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.Content_Type.TV)
        ConfigModifier_TV.MainEFanarts = ConfigModifier_TV.MainFanart

    End Sub

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_Movie_MovieSet, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_Movie.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_Movie()

        If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            DBMovie.Movie.TMDBID = ModulesManager.Instance.GetMovieTMDBID(DBMovie.Movie.ID)
        End If

        If Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            Dim Settings As TMDB.Scraper.MySettings
            Settings.APIKey = _MySettings_Movie.APIKey

            Dim _scraper As New TMDB.Scraper
            Dim filterModifier As Structures.ScrapeModifier = Functions.ScrapeModifierAndAlso(ScrapeModifier, ConfigModifier_TV)

            ImagesContainer = _scraper.GetImages_Movie_MovieSet(DBMovie.Movie.TMDBID, filterModifier, Settings, Enums.Content_Type.Movie)
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper(ByRef DBMovieSet As Structures.DBMovieSet, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_Movie_MovieSet, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_MovieSet.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_MovieSet()

        If String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
            If DBMovieSet.Movies IsNot Nothing AndAlso DBMovieSet.Movies.Count > 0 Then
                DBMovieSet.MovieSet.ID = ModulesManager.Instance.GetMovieCollectionID(DBMovieSet.Movies.Item(0).Movie.ID)
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
            Dim Settings As TMDB.Scraper.MySettings
            Settings.APIKey = _MySettings_MovieSet.APIKey

            Dim _scraper As New TMDB.Scraper
            Dim filterModifier As Structures.ScrapeModifier = Functions.ScrapeModifierAndAlso(ScrapeModifier, ConfigModifier_TV)

            ImagesContainer = _scraper.GetImages_Movie_MovieSet(DBMovieSet.MovieSet.ID, filterModifier, Settings, Enums.Content_Type.MovieSet)
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper(ByRef DBTV As Structures.DBTV, ByRef ImagesContainer As MediaContainers.SearchResultsContainer_TV, ByVal ScrapeModifier As Structures.ScrapeModifier) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_TV.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_TV()

        Dim Settings As TMDB.Scraper.MySettings
        Settings.APIKey = _MySettings_TV.APIKey

        Dim _scraper As New TMDB.Scraper
        Dim filterModifier As Structures.ScrapeModifier = Functions.ScrapeModifierAndAlso(ScrapeModifier, ConfigModifier_TV)

        If String.IsNullOrEmpty(DBTV.TVShow.TMDB) Then
            If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                DBTV.TVShow.TMDB = _scraper.GetTMDBbyTVDB(DBTV.TVShow.TVDB, Settings)
            End If
        End If

        If DBTV.TVEp IsNot Nothing Then
            If Not String.IsNullOrEmpty(DBTV.TVShow.TMDB) Then
                ImagesContainer = _scraper.GetImages_TVEpisode(DBTV.TVShow.TMDB, DBTV.TVEp.Season, DBTV.TVEp.Episode, Settings)
            End If
        Else
            If Not String.IsNullOrEmpty(DBTV.TVShow.TMDB) Then
                ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.TMDB, filterModifier, Settings)
            End If
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigModifier_Movie.MainPoster, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_Movie.MainFanart, , , Enums.Content_Type.Movie)

            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text, , , Enums.Content_Type.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigModifier_MovieSet.MainPoster, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_MovieSet.MainFanart, , , Enums.Content_Type.MovieSet)

            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text, , , Enums.Content_Type.MovieSet)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", ConfigModifier_TV.EpisodePoster, , , Enums.Content_Type.TV)
            settings.SetBooleanSetting("DoSeasonPoster", ConfigModifier_TV.SeasonPoster, , , Enums.Content_Type.TV)
            settings.SetBooleanSetting("DoShowFanart", ConfigModifier_TV.MainFanart, , , Enums.Content_Type.TV)
            settings.SetBooleanSetting("DoShowPoster", ConfigModifier_TV.MainPoster, , , Enums.Content_Type.TV)

            settings.SetSetting("ApiKey", _setup_TV.txtApiKey.Text, , , Enums.Content_Type.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        ConfigModifier_Movie.MainPoster = _setup_Movie.chkScrapePoster.Checked
        ConfigModifier_Movie.MainFanart = _setup_Movie.chkScrapeFanart.Checked
        SaveSettings_Movie()
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
        SaveSettings_MovieSet()
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
        SaveSettings_TV()
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

    Structure sMySettings

#Region "Fields"

        Dim APIKey As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class