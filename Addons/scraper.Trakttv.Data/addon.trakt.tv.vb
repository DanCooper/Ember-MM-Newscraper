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
Imports System.Threading.Tasks

Public Class Movie
    Implements Interfaces.IScraperModule_Data_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Movie
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Data_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(
                             _AddonSettings,
                             "0679d762ec6de44d93e0ea42a3b7662cfd8c4e9f430e82550d83a2bc8e25072b",
                             "0581ff6f5075003dbe4620c959e1200d06cce7de639f268f7182b588ffdaa1a1"
                             ))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Data_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Data_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Data_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Data_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Data_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Data_Movie.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_NewToken()
        _AddonSettings.APIAccessToken = _Scraper.AccessToken
        _AddonSettings.APICreated = Functions.ConvertToUnixTimestamp(_Scraper.Created).ToString
        _AddonSettings.APIExpiresInSeconds = _Scraper.ExpiresInSeconds.ToString
        _AddonSettings.APIRefreshToken = _Scraper.RefreshToken
        Settings_Save()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        IsEnabled = State
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, DiffOrder)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse String.IsNullOrEmpty(DBMovie.Movie.UniqueIDs.IMDbId)) Then
            _Logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        Settings_Load()
        Return New Interfaces.ModuleResult
    End Function

    Public Function GetTMDBID(ByVal IMDbID As String, ByRef TMDbID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult
    End Function

    Public Sub Init_Movie() Implements Interfaces.IScraperModule_Data_Movie.Init
        Settings_Load()
        AddHandler _Scraper.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Public Sub InjectSettingsPanel_Movie() Implements Interfaces.IScraperModule_Data_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Movie
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled

        _PnlSettingsPanel.chkRating.Checked = _ConfigScrapeOptions.bMainRating
        _PnlSettingsPanel.chkUserRating.Checked = _ConfigScrapeOptions.bMainUserRating

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "Trakt.tv",
            .Type = Enums.SettingsPanelType.MovieData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Data_Movie.OrderChanged
        _PnlSettingsPanel.orderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from Trakttv
    ''' </summary>
    ''' <param name="oDBMovie">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperModule_Data_Movie.Run
        _Logger.Trace("[Tracktv_Data] [Scraper_Movie] [Start]")

        Dim nMovie As MediaContainers.Movie = Nothing
        Dim nDBElement = oDBElement
        If ScrapeModifiers.MainNFO Then
            Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)
            Dim nResult = Task.Run(Function() _Scraper.GetInfo_Movie(_Scraper.GetID_Trakt(nDBElement), FilteredOptions))
            If nResult.Exception Is Nothing AndAlso nResult.Result IsNot Nothing Then
                nMovie = nResult.Result
            ElseIf nResult.Exception IsNot Nothing Then
                _Logger.Error(String.Concat("[Tracktv_Data] [Scraper_Movie]: ", nResult.Exception.InnerException.Message))
                Task.Run(Function() _Scraper.RefreshAuthorization())
                _Logger.Error("[Tracktv_Data] [Scraper_Movie] [Abort] API error")
            End If
        End If

        _Logger.Trace("[Tracktv_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_Movie.SaveSetup
        _ConfigScrapeOptions.bMainRating = _PnlSettingsPanel.chkRating.Checked
        _ConfigScrapeOptions.bMainUserRating = _PnlSettingsPanel.chkUserRating.Checked

        Settings_Save()

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub Settings_Load()
        _ConfigScrapeOptions.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True)
        _ConfigScrapeOptions.bMainUserRating = AdvancedSettings.GetBooleanSetting("DoUserRating", True)
        _AddonSettings.APIAccessToken = AdvancedSettings.GetSetting("APIAccessToken", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.APICreated = AdvancedSettings.GetSetting("APICreatedAt", "0", , Enums.ContentType.Movie)
        _AddonSettings.APIExpiresInSeconds = AdvancedSettings.GetSetting("APIExpiresInSeconds", "0", , Enums.ContentType.Movie)
        _AddonSettings.APIRefreshToken = AdvancedSettings.GetSetting("APIRefreshToken", String.Empty, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.bMainRating, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoUserRating", _ConfigScrapeOptions.bMainUserRating, , , Enums.ContentType.Movie)
            settings.SetSetting("APIAccessToken", _AddonSettings.APIAccessToken, , , Enums.ContentType.Movie)
            settings.SetSetting("APICreatedAt", _AddonSettings.APICreated, , , Enums.ContentType.Movie)
            settings.SetSetting("APIExpiresInSeconds", _AddonSettings.APIExpiresInSeconds, , , Enums.ContentType.Movie)
            settings.SetSetting("APIRefreshToken", _AddonSettings.APIRefreshToken, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class TV
    Implements Interfaces.IScraperModule_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_TV
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Data_TV.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(
                             _AddonSettings,
                             "f91af6501263371353f6d1f5d9ff924934c796b555c0341044336e94080021f1",
                             "20ec71842c1da85dadb554d7920c22b4efb0aeb198fa606880a9e36ffd7c95c4"
                             ))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Data_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Data_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Data_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Data_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Data_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Data_TV.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_NewToken()
        _AddonSettings.APIAccessToken = _Scraper.AccessToken
        _AddonSettings.APICreated = Functions.ConvertToUnixTimestamp(_Scraper.Created).ToString
        _AddonSettings.APIExpiresInSeconds = _Scraper.ExpiresInSeconds.ToString
        _AddonSettings.APIRefreshToken = _Scraper.RefreshToken
        Settings_Save()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        IsEnabled = State
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, DiffOrder)
    End Sub


#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init_TV() Implements Interfaces.IScraperModule_Data_TV.Init
        Settings_Load()
        AddHandler _Scraper.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigScrapeOptions.bMainRating
        _PnlSettingsPanel.chkScraperShowUserRating.Checked = _ConfigScrapeOptions.bMainUserRating
        _PnlSettingsPanel.chkScraperEpisodeRating.Checked = _ConfigScrapeOptions.bEpisodeRating
        _PnlSettingsPanel.chkScraperEpisodeUserRating.Checked = _ConfigScrapeOptions.bEpisodeUserRating

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "Trakt.tv",
            .Type = Enums.SettingsPanelType.TVData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Data_TV.OrderChanged
        _PnlSettingsPanel.orderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape episode details from Trakttv
    ''' </summary>
    ''' <param name="oDBElement">TV Season to be scraped as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>modifies Database.DBElement Object which contains the scraped data</returns>
    ''' <remarks></remarks>
    Public Function Run_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperModule_Data_TV.Run_TVEpisode
        _Logger.Trace("[Tracktv_Data] [Scraper_TVEpisode] [Start]")

        Dim nTVEpisode As New MediaContainers.EpisodeDetails

        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)
        If FilteredOptions.bEpisodeRating OrElse FilteredOptions.bEpisodeUserRating Then
            Dim nResult = _Scraper.GetInfo_TVEpisode(_Scraper.GetID_Trakt(oDBElement, True), oDBElement.TVEpisode.Season, oDBElement.TVEpisode.Episode, FilteredOptions)
            While Not nResult.IsCompleted
                Threading.Thread.Sleep(50)
            End While
            If nResult.Exception Is Nothing AndAlso nResult.Result IsNot Nothing Then
                nTVEpisode = nResult.Result
            ElseIf nResult.Exception IsNot Nothing Then
                _Logger.Error(String.Concat("[Tracktv_Data] [Scraper_TVEpisode]: ", nResult.Exception.InnerException.Message))
                Task.Run(Function() _Scraper.RefreshAuthorization())
                _Logger.Error("[Tracktv_Data] [Scraper_TVEpisode] [Abort] API error")
            End If
        End If

        _Logger.Trace("[Tracktv_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function
    ''' <summary>
    '''  Scrape season details from Trakttv
    ''' </summary>
    ''' <param name="oDBElement">TV Season to be scraped as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Public Function Run_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperModule_Data_TV.Run_TVSeason
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
    End Function
    ''' <summary>
    '''  Scrape tv show details from Trakttv
    ''' </summary>
    ''' <param name="oDBElement">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeModifiers">what additional data to scrape (images, episode details, season details...)</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>modifies Database.DBElement Object which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run_TVShow(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperModule_Data_TV.Run_TVShow
        _Logger.Trace("[Tracktv_Data] [Scraper_TV] [Start]")

        Dim nTVShow As New MediaContainers.TVShow

        If ScrapeModifiers.MainNFO Then
            Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)
            Dim nResult = _Scraper.GetInfo_TVShow(_Scraper.GetID_Trakt(oDBElement), FilteredOptions, ScrapeModifiers, oDBElement.Episodes)
            While Not nResult.IsCompleted
                Threading.Thread.Sleep(50)
            End While
            If nResult.Exception Is Nothing AndAlso nResult.Result IsNot Nothing Then
                nTVShow = nResult.Result
            ElseIf nResult.Exception IsNot Nothing Then
                _Logger.Error(String.Concat("[Tracktv_Data] [Scraper_TV]: ", nResult.Exception.InnerException.Message))
                Task.Run(Function() _Scraper.RefreshAuthorization())
                _Logger.Error("[Tracktv_Data] [Scraper_TV] [Abort] API error")
            End If
        End If

        _Logger.Trace("[Tracktv_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_TV.SaveSetup
        _ConfigScrapeOptions.bEpisodeRating = _PnlSettingsPanel.chkScraperEpisodeRating.Checked
        _ConfigScrapeOptions.bEpisodeUserRating = _PnlSettingsPanel.chkScraperEpisodeUserRating.Checked
        _ConfigScrapeOptions.bMainRating = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigScrapeOptions.bMainUserRating = _PnlSettingsPanel.chkScraperShowUserRating.Checked

        Settings_Load()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub Settings_Load()
        _ConfigScrapeOptions.bEpisodeRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.bEpisodeUserRating = AdvancedSettings.GetBooleanSetting("DoUserRating", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.bMainUserRating = AdvancedSettings.GetBooleanSetting("DoUserRating", True, , Enums.ContentType.TVShow)
        _AddonSettings.APIAccessToken = AdvancedSettings.GetSetting("APIAccessToken", String.Empty, , Enums.ContentType.TV)
        _AddonSettings.APICreated = AdvancedSettings.GetSetting("APICreatedAt", "0", , Enums.ContentType.TV)
        _AddonSettings.APIExpiresInSeconds = AdvancedSettings.GetSetting("APIExpiresInSeconds", "0", , Enums.ContentType.TV)
        _AddonSettings.APIRefreshToken = AdvancedSettings.GetSetting("APIRefreshToken", String.Empty, , Enums.ContentType.TV)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.bEpisodeRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoUserRating", _ConfigScrapeOptions.bEpisodeUserRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.bMainRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoUserRating", _ConfigScrapeOptions.bMainUserRating, , , Enums.ContentType.TVShow)
            settings.SetSetting("APIAccessToken", _AddonSettings.APIAccessToken, , , Enums.ContentType.TV)
            settings.SetSetting("APICreatedAt", _AddonSettings.APICreated, , , Enums.ContentType.TV)
            settings.SetSetting("APIExpiresInSeconds", _AddonSettings.APIExpiresInSeconds, , , Enums.ContentType.TV)
            settings.SetSetting("APIRefreshToken", _AddonSettings.APIRefreshToken, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class


Public Structure AddonSettings

#Region "Fields"

    Dim APIAccessToken As String
    Dim APICreated As String
    Dim APIExpiresInSeconds As String
    Dim APIRefreshToken As String

#End Region 'Fields

End Structure