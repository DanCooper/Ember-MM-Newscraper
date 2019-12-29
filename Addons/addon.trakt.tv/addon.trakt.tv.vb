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
Imports System.Drawing
Imports System.IO
Imports System.Xml.Serialization
Imports System.Threading.Tasks
Imports TraktApiSharp

Public Class Data_Movie
    Implements Interfaces.IScraperAddon_Data_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Data_Movie
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_Movie.IsEnabled
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

    Property Order As Integer Implements Interfaces.IScraperAddon_Data_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Data_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Data_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Data_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Data_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Data_Movie.StateChanged

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

    Public Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse String.IsNullOrEmpty(DBMovie.Movie.UniqueIDs.IMDbId)) Then
            _Logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        Settings_Load()
        Return New Interfaces.ModuleResult
    End Function

    Public Function GetTMDbID(ByVal IMDbID As String, ByRef TMDbID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetTMDbID
        Return New Interfaces.ModuleResult
    End Function

    Public Sub Init_Movie() Implements Interfaces.IScraperAddon_Data_Movie.Init
        Settings_Load()
        AddHandler _Scraper.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Public Sub InjectSettingsPanel_Movie() Implements Interfaces.IScraperAddon_Data_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_Movie
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled

        _PnlSettingsPanel.chkRating.Checked = _ConfigScrapeOptions.bMainRatings
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

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from Trakttv
    ''' </summary>
    ''' <param name="oDBMovie">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperAddon_Data_Movie.Run
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

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_Movie.SaveSetup
        _ConfigScrapeOptions.bMainRatings = _PnlSettingsPanel.chkRating.Checked
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
        _ConfigScrapeOptions.bMainRatings = AdvancedSettings.GetBooleanSetting("DoRating", True)
        _ConfigScrapeOptions.bMainUserRating = AdvancedSettings.GetBooleanSetting("DoUserRating", True)
        _AddonSettings.APIAccessToken = AdvancedSettings.GetSetting("APIAccessToken", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.APICreated = AdvancedSettings.GetSetting("APICreatedAt", "0", , Enums.ContentType.Movie)
        _AddonSettings.APIExpiresInSeconds = AdvancedSettings.GetSetting("APIExpiresInSeconds", "0", , Enums.ContentType.Movie)
        _AddonSettings.APIRefreshToken = AdvancedSettings.GetSetting("APIRefreshToken", String.Empty, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.bMainRatings, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoUserRating", _ConfigScrapeOptions.bMainUserRating, , , Enums.ContentType.Movie)
            settings.SetSetting("APIAccessToken", _AddonSettings.APIAccessToken, , , Enums.ContentType.Movie)
            settings.SetSetting("APICreatedAt", _AddonSettings.APICreated, , , Enums.ContentType.Movie)
            settings.SetSetting("APIExpiresInSeconds", _AddonSettings.APIExpiresInSeconds, , , Enums.ContentType.Movie)
            settings.SetSetting("APIRefreshToken", _AddonSettings.APIRefreshToken, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class Data_TV
    Implements Interfaces.IScraperAddon_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Data_TV
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_TV.IsEnabled
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

    Property Order As Integer Implements Interfaces.IScraperAddon_Data_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Data_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Data_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Data_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Data_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Data_TV.StateChanged

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

    Public Sub Init_TV() Implements Interfaces.IScraperAddon_Data_TV.Init
        Settings_Load()
        AddHandler _Scraper.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigScrapeOptions.bMainRatings
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

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape episode details from Trakttv
    ''' </summary>
    ''' <param name="oDBElement">TV Season to be scraped as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>modifies Database.DBElement Object which contains the scraped data</returns>
    ''' <remarks></remarks>
    Public Function Run_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperAddon_Data_TV.Run_TVEpisode
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
    Public Function Run_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperAddon_Data_TV.Run_TVSeason
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
    Function Run_TVShow(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperAddon_Data_TV.Run_TVShow
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

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_TV.SaveSetup
        _ConfigScrapeOptions.bEpisodeRating = _PnlSettingsPanel.chkScraperEpisodeRating.Checked
        _ConfigScrapeOptions.bEpisodeUserRating = _PnlSettingsPanel.chkScraperEpisodeUserRating.Checked
        _ConfigScrapeOptions.bMainRatings = _PnlSettingsPanel.chkScraperShowRating.Checked
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
        _ConfigScrapeOptions.bMainRatings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
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
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.bMainRatings, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoUserRating", _ConfigScrapeOptions.bMainUserRating, , , Enums.ContentType.TVShow)
            settings.SetSetting("APIAccessToken", _AddonSettings.APIAccessToken, , , Enums.ContentType.TV)
            settings.SetSetting("APICreatedAt", _AddonSettings.APICreated, , , Enums.ContentType.TV)
            settings.SetSetting("APIExpiresInSeconds", _AddonSettings.APIExpiresInSeconds, , , Enums.ContentType.TV)
            settings.SetSetting("APIRefreshToken", _AddonSettings.APIRefreshToken, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Generic
    Implements Interfaces.IGenericAddon

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Cmnu_MovieSets As New ToolStripMenuItem
    Private _Cmnu_Movies As New ToolStripMenuItem
    Private _Cmnu_TVEpisodes As New ToolStripMenuItem
    Private _Cmnu_TVSeasons As New ToolStripMenuItem
    Private _Cmnu_TVShows As New ToolStripMenuItem
    Private _CmnuSep_MovieSets As New ToolStripSeparator
    Private _CmnuSep_Movies As New ToolStripSeparator
    Private _CmnuSep_TVEpisodes As New ToolStripSeparator
    Private _CmnuSep_TVSeasons As New ToolStripSeparator
    Private _CmnuSep_TVShows As New ToolStripSeparator
    Private _CmnuTrayToolsTrakt As New ToolStripMenuItem
    Private _MnuMainToolsTrakt As New ToolStripMenuItem
    Private _Enabled As Boolean = False
    Private _AddonSettings As New AddonSettings
    Private _PnlSettingsPanel As frmSettingsPanel_Generic
    Private _TraktAPI As Scraper
    Private _XmlSettingsPath As String = Path.Combine(Master.SettingsPath, "Interface.Trakt.xml")

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Property IsEnabled() As Boolean Implements Interfaces.IGenericAddon.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            If _Enabled = value Then Return
            _Enabled = value
            If _Enabled Then
                ToolsStripMenu_Enable()
            Else
                ToolsStripMenu_Disable()
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IGenericAddon.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericAddon.SettingsPanel

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericAddon.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {
                                                      Enums.ModuleEventType.BeforeEdit_Movie,
                                                      Enums.ModuleEventType.BeforeEdit_TVEpisode,
                                                      Enums.ModuleEventType.BeforeEdit_TVShow,
                                                      Enums.ModuleEventType.CommandLine,
                                                      Enums.ModuleEventType.Generic,
                                                      Enums.ModuleEventType.Remove_Movie,
                                                      Enums.ModuleEventType.Remove_TVEpisode,
                                                      Enums.ModuleEventType.Remove_TVSeason,
                                                      Enums.ModuleEventType.Remove_TVShow,
                                                      Enums.ModuleEventType.ScraperMulti_Movie,
                                                      Enums.ModuleEventType.ScraperMulti_TVEpisode,
                                                      Enums.ModuleEventType.ScraperMulti_TVSeason,
                                                      Enums.ModuleEventType.ScraperMulti_TVShow,
                                                      Enums.ModuleEventType.ScraperSingle_Movie,
                                                      Enums.ModuleEventType.ScraperSingle_TVEpisode,
                                                      Enums.ModuleEventType.ScraperSingle_TVSeason,
                                                      Enums.ModuleEventType.ScraperSingle_TVShow})
        End Get
    End Property

#End Region 'Properties

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(Control As ToolStripMenuItem, Value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(Control As ToolStripMenuItem, Value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolStripItem(Value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolStripItem(Value As ToolStripItem)

#End Region 'Delegates

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericAddon.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericAddon.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericAddon.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericAddon.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef params As List(Of Object))
        RaiseEvent GenericEvent(mType, params)
    End Sub

    Private Sub Handle_NewToken()
        _AddonSettings.APIAccessToken = _TraktAPI.AccessToken
        _AddonSettings.APICreated = Functions.ConvertToUnixTimestamp(_TraktAPI.Created).ToString
        _AddonSettings.APIExpiresInSeconds = _TraktAPI.ExpiresInSeconds.ToString
        _AddonSettings.APIRefreshToken = _TraktAPI.RefreshToken
        Settings_Save()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean)
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, 0)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IGenericAddon.Init
        Settings_Load()
        _TraktAPI = New Scraper
        AddHandler _TraktAPI.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericAddon.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Generic
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_Movie.Checked = _AddonSettings.GetWatchedStateBeforeEdit_Movie
        _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_TVEpisode.Checked = _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode
        _PnlSettingsPanel.chkGetWatchedStateScraperMulti_Movie.Checked = _AddonSettings.GetWatchedStateScraperMulti_Movie
        _PnlSettingsPanel.chkGetWatchedStateScraperMulti_TVEpisode.Checked = _AddonSettings.GetWatchedStateScraperMulti_TVEpisode
        _PnlSettingsPanel.chkGetWatchedStateScraperSingle_Movie.Checked = _AddonSettings.GetWatchedStateScraperSingle_Movie
        _PnlSettingsPanel.chkGetWatchedStateScraperSingle_TVEpisode.Checked = _AddonSettings.GetWatchedStateScraperSingle_TVEpisode
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "Trakt.tv Interface",
            .Type = Enums.SettingsPanelType.Addon
        }
    End Sub

    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        Select Case ModuleEventType
            Case Enums.ModuleEventType.BeforeEdit_Movie
                If _AddonSettings.GetWatchedStateBeforeEdit_Movie AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(DBElement)
                End If
            Case Enums.ModuleEventType.BeforeEdit_TVEpisode
                If _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(DBElement)
                End If
            Case Enums.ModuleEventType.CommandLine
                '_TraktAPI.SyncToEmber_All()
            Case Enums.ModuleEventType.ScraperMulti_Movie
                If _AddonSettings.GetWatchedStateScraperMulti_Movie AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(DBElement)
                End If
            Case Enums.ModuleEventType.Remove_Movie
                If _AddonSettings.CollectionRemove_Movie AndAlso DBElement IsNot Nothing Then
                    '_TraktAPI.RemoveFromCollection_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperMulti_TVEpisode
                If _AddonSettings.GetWatchedStateScraperMulti_TVEpisode AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(DBElement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_Movie
                If _AddonSettings.GetWatchedStateScraperSingle_Movie AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(DBElement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVShow, Enums.ModuleEventType.ScraperSingle_TVEpisode
                If _AddonSettings.GetWatchedStateScraperSingle_TVEpisode AndAlso DBElement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(DBElement)
                End If
        End Select

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        _AddonSettings.GetWatchedStateBeforeEdit_Movie = _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_Movie.Checked
        _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_TVEpisode.Checked
        _AddonSettings.GetWatchedStateScraperMulti_Movie = _PnlSettingsPanel.chkGetWatchedStateScraperMulti_Movie.Checked
        _AddonSettings.GetWatchedStateScraperMulti_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateScraperMulti_TVEpisode.Checked
        _AddonSettings.GetWatchedStateScraperSingle_Movie = _PnlSettingsPanel.chkGetWatchedStateScraperSingle_Movie.Checked
        _AddonSettings.GetWatchedStateScraperSingle_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateScraperSingle_TVEpisode.Checked

        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolStripItem(tsi, _MnuMainToolsTrakt)

        'cmnuTrayTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolStripItem(tsi, _CmnuTrayToolsTrakt)
    End Sub

    Sub ToolsStripMenu_Enable()
        _TraktAPI.CreateAPI(_AddonSettings, "77bfb26ae2b5e2217151bbb3a20309dbd5e638c5a85e17505457edd14c6399fa", "240bd5554e2c8065dfe3cf7027b292193a113d96106a5f04a0b088f9d2371757")
        PopulateMenus()
    End Sub

    Public Sub RemoveToolStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_MovieSets(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_MovieSets), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVEpisodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVEpisodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVSeasons(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVSeasons), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVShows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVShows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Public Sub AddToolStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_MovieSets(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_MovieSets), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVEpisodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVEpisodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVSeasons(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVSeasons), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVShows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVShows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
        End If
    End Sub

    Sub Settings_Load()
        _AddonSettings = New AddonSettings
        If File.Exists(_XmlSettingsPath) Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As StreamReader = New StreamReader(_XmlSettingsPath)
                xmlSer = New XmlSerializer(GetType(AddonSettings))
                _AddonSettings = DirectCast(xmlSer.Deserialize(xmlSR), AddonSettings)
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Get movie watched state from Trakt
    ''' </summary>
    ''' <param name="sender">context menu "Get Movie WatchedState"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedMovies = _TraktAPI.GetWatched_Movies
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                If _TraktAPI.GetWatchedState_Movie(DBElement, lstWatchedMovies) Then
                    Master.DB.Save_Movie(DBElement, False, True, False, True, False)
                    _Logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_Movie: ""{0}"" | Synced to Ember", DBElement.Movie.Title))
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {DBElement.ID}))
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Get episode watched state from Trakt
    ''' </summary>
    ''' <param name="sender">context menu "Get TVEpisode WatchedState"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                    Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                    _Logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                               DBElement.TVShow.Title,
                                               DBElement.TVEpisode.Season,
                                               DBElement.TVEpisode.Episode,
                                               DBElement.TVEpisode.Title))
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole season on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get TVSeason Playcount"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of season in Kodi DB
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
            Dim DBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
            For Each DBElement In DBTVSeason.Episodes
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                        Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                        _Logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                                   DBElement.TVShow.Title,
                                                   DBElement.TVEpisode.Season,
                                                   DBElement.TVEpisode.Episode,
                                                   DBElement.TVEpisode.Title))
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                    End If
                End If
            Next
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVSeason, New List(Of Object)(New Object() {DBTVSeason.ID}))
        Next
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole tv show on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get Tvshow Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
            Dim DBTVSShow As Database.DBElement = Master.DB.Load_TVShow(ID, False, True)
            For Each DBElement In DBTVSShow.Episodes
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                        Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                        _Logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                                   DBElement.TVShow.Title,
                                                   DBElement.TVEpisode.Season,
                                                   DBElement.TVEpisode.Episode,
                                                   DBElement.TVEpisode.Title))
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                    End If
                End If
            Next
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {DBTVSShow.ID}))
        Next
    End Sub
    ''' <summary>
    '''  Get WatchedState of all movies of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuGetPlaycount_Movies_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim nDialog As New dlgWorker(_TraktAPI, Enums.ContentType.Movie)
        AddHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
        nDialog.ShowDialog()
        RemoveHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
    End Sub
    ''' <summary>
    '''  Get WatchedState of all tv shows of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuGetPlaycount_TVEpisodes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim nDialog As New dlgWorker(_TraktAPI, Enums.ContentType.TVEpisode)
        AddHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
        nDialog.ShowDialog()
        RemoveHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
    End Sub

    Private Sub mnuTrakttvManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub PopulateMenus()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        _MnuMainToolsTrakt.DropDownItems.Clear()
        _MnuMainToolsTrakt.Image = New Bitmap(My.Resources.icon)
        _MnuMainToolsTrakt.Text = "Trakt.tv"
        _MnuMainToolsTrakt.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        CreateToolsMenu(_MnuMainToolsTrakt)
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, _MnuMainToolsTrakt)

        'mnuTrayTools
        _CmnuTrayToolsTrakt.DropDownItems.Clear()
        _CmnuTrayToolsTrakt.Image = New Bitmap(My.Resources.icon)
        _CmnuTrayToolsTrakt.Text = "Trakt.tv"
        CreateToolsMenu(_CmnuTrayToolsTrakt)
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, _CmnuTrayToolsTrakt)

        'cmnuMovies
        _Cmnu_Movies.DropDownItems.Clear()
        _Cmnu_Movies.Image = New Bitmap(My.Resources.icon)
        _Cmnu_Movies.Text = "Trakt.tv"
        CreateContextMenu(_Cmnu_Movies, Enums.ContentType.Movie)
        AddToolStripItem_Movies(_CmnuSep_Movies)
        AddToolStripItem_Movies(_Cmnu_Movies)

        'cmnuTVEpisodes
        _Cmnu_TVEpisodes.DropDownItems.Clear()
        _Cmnu_TVEpisodes.Image = New Bitmap(My.Resources.icon)
        _Cmnu_TVEpisodes.Text = "Trakt.tv"
        CreateContextMenu(_Cmnu_TVEpisodes, Enums.ContentType.TVEpisode)
        AddToolStripItem_TVEpisodes(_CmnuSep_TVEpisodes)
        AddToolStripItem_TVEpisodes(_Cmnu_TVEpisodes)

        'cmnuTVSeasons
        _Cmnu_TVSeasons.DropDownItems.Clear()
        _Cmnu_TVSeasons.Image = New Bitmap(My.Resources.icon)
        _Cmnu_TVSeasons.Text = "Trakt.tv"
        CreateContextMenu(_Cmnu_TVSeasons, Enums.ContentType.TVSeason)
        AddToolStripItem_TVSeasons(_CmnuSep_TVSeasons)
        AddToolStripItem_TVSeasons(_Cmnu_TVSeasons)

        'cmnuTVShows
        _Cmnu_TVShows.DropDownItems.Clear()
        _Cmnu_TVShows.Image = New Bitmap(My.Resources.icon)
        _Cmnu_TVShows.Text = "Trakt.tv"
        CreateContextMenu(_Cmnu_TVShows, Enums.ContentType.TVShow)
        AddToolStripItem_TVShows(_CmnuSep_TVShows)
        AddToolStripItem_TVShows(_Cmnu_TVShows)
    End Sub

    Private Sub CreateContextMenu(ByRef tMenu As ToolStripMenuItem, ByVal tContentType As Enums.ContentType)
        'Dim mnuHostSyncItem As New ToolStripMenuItem
        'mnuHostSyncItem.Image = New Bitmap(My.Resources.menuSync)
        'mnuHostSyncItem.Text = Master.eLang.GetString(1446, "Sync")
        'Select Case tContentType
        '    Case Enums.ContentType.Movie
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_Movie_Click
        '    Case Enums.ContentType.MovieSet
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
        '    Case Enums.ContentType.TVEpisode
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
        '    Case Enums.ContentType.TVSeason
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
        '    Case Enums.ContentType.TVShow
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
        'End Select
        'tMenu.DropDownItems.Add(mnuHostSyncItem)
        'If tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
        '    Dim mnuHostSyncFullItem As New ToolStripMenuItem
        '    mnuHostSyncFullItem.Image = New Bitmap(My.Resources.menuSync)
        '    mnuHostSyncFullItem.Text = Master.eLang.GetString(1008, "Sync Full")
        '    Select Case tContentType
        '        Case Enums.ContentType.TVSeason
        '            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVSeason_Click
        '        Case Enums.ContentType.TVShow
        '            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVShow_Click
        '    End Select
        '    tMenu.DropDownItems.Add(mnuHostSyncFullItem)
        'End If
        If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
            Dim mnuGetWatchedState As New ToolStripMenuItem
            mnuGetWatchedState.Image = New Bitmap(My.Resources.menuWatchedState)
            mnuGetWatchedState.Text = Master.eLang.GetString(1070, "Get Watched State")
            Select Case tContentType
                Case Enums.ContentType.Movie
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_Movie_Click
                Case Enums.ContentType.TVEpisode
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVEpisode_Click
                Case Enums.ContentType.TVSeason
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVSeason_Click
                Case Enums.ContentType.TVShow
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVShow_Click
            End Select
            tMenu.DropDownItems.Add(mnuGetWatchedState)
        End If
        'If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVShow Then
        '    Dim mnuHostRemoveItem As New ToolStripMenuItem
        '    mnuHostRemoveItem.Image = New Bitmap(My.Resources.menuRemove)
        '    mnuHostRemoveItem.Text = Master.eLang.GetString(30, "Remove")
        '    Select Case tContentType
        '        Case Enums.ContentType.Movie
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_Movie_Click
        '        Case Enums.ContentType.TVEpisode
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVEpisode_Click
        '        Case Enums.ContentType.TVShow
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVShow_Click
        '    End Select
        '    tMenu.DropDownItems.Add(mnuHostRemoveItem)
        'End If
    End Sub

    Private Sub CreateToolsMenu(ByRef tMenu As ToolStripMenuItem)
        Dim mnuTrakttvManager As New ToolStripMenuItem
        mnuTrakttvManager.Image = New Bitmap(My.Resources.icon)
        mnuTrakttvManager.Text = String.Concat(Master.eLang.GetString(871, "Trakt.tv Manager"), " (temp. disabled for rework)")
        mnuTrakttvManager.Enabled = False
        AddHandler mnuTrakttvManager.Click, AddressOf mnuTrakttvManager_Click
        tMenu.DropDownItems.Add(mnuTrakttvManager)
        tMenu.DropDownItems.Add(New ToolStripSeparator)

        Dim mnuGetPlaycount_Movies As New ToolStripMenuItem
        mnuGetPlaycount_Movies.Image = New Bitmap(My.Resources.menuWatchedState)
        mnuGetPlaycount_Movies.Text = String.Format("{0} - {1}",
                                                        Master.eLang.GetString(1070, "Get Watched State"),
                                                        Master.eLang.GetString(36, "Movies"))
        AddHandler mnuGetPlaycount_Movies.Click, AddressOf mnuGetPlaycount_Movies_Click
        tMenu.DropDownItems.Add(mnuGetPlaycount_Movies)

        Dim mnuGetPlaycount_TVEpisodes As New ToolStripMenuItem
        mnuGetPlaycount_TVEpisodes.Image = New Bitmap(My.Resources.menuWatchedState)
        mnuGetPlaycount_TVEpisodes.Text = String.Format("{0} - {1}",
                                                            Master.eLang.GetString(1070, "Get Watched State"),
                                                            Master.eLang.GetString(682, "Episodes"))
        AddHandler mnuGetPlaycount_TVEpisodes.Click, AddressOf mnuGetPlaycount_TVEpisodes_Click
        tMenu.DropDownItems.Add(mnuGetPlaycount_TVEpisodes)
    End Sub

    Sub Settings_Save()
        If Not File.Exists(_XmlSettingsPath) OrElse (Not CBool(File.GetAttributes(_XmlSettingsPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(_XmlSettingsPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(_XmlSettingsPath)
                Try
                    File.SetAttributes(_XmlSettingsPath, FileAttributes.Normal)
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
            Using xmlSW As New StreamWriter(_XmlSettingsPath)
                Dim xmlSer As New XmlSerializer(GetType(AddonSettings))
                xmlSer.Serialize(xmlSW, _AddonSettings)
            End Using
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"
    ''' <summary>
    ''' structure used to read setting file of Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    <XmlRoot("interface.kodi")>
    Class KodiSettings

#Region "Fields"

        Private _hosts As New List(Of Host)
        Private _sendnotifications As Boolean
        Private _syncplaycounts As Boolean
        Private _syncplaycountshost As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("sendnotifications")>
        Public Property SendNotifications() As Boolean
            Get
                Return _sendnotifications
            End Get
            Set(ByVal value As Boolean)
                _sendnotifications = value
            End Set
        End Property

        <XmlElement("syncplaycounts")>
        Public Property SyncPlayCounts() As Boolean
            Get
                Return _syncplaycounts
            End Get
            Set(ByVal value As Boolean)
                _syncplaycounts = value
            End Set
        End Property

        <XmlElement("syncplaycountshost")>
        Public Property SyncPlayCountsHost() As String
            Get
                Return _syncplaycountshost
            End Get
            Set(ByVal value As String)
                _syncplaycountshost = value
            End Set
        End Property

        <XmlElement("host")>
        Public Property Hosts() As List(Of Host)
            Get
                Return _hosts
            End Get
            Set(ByVal value As List(Of Host))
                _hosts = value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    Class Host

#Region "Fields"

        Private _address As String
        Private _label As String
        Private _moviesetartworkspath As String
        Private _password As String
        Private _port As Integer
        Private _realtimesync As Boolean
        Private _sources As New List(Of Source)
        Private _username As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("label")>
        Public Property Label() As String
            Get
                Return _label
            End Get
            Set(ByVal value As String)
                _label = value
            End Set
        End Property

        <XmlElement("address")>
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        <XmlElement("port")>
        Public Property Port() As Integer
            Get
                Return _port
            End Get
            Set(ByVal value As Integer)
                _port = value
            End Set
        End Property

        <XmlElement("username")>
        Public Property Username() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        <XmlElement("password")>
        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        <XmlElement("realtimesync")>
        Public Property RealTimeSync() As Boolean
            Get
                Return _realtimesync
            End Get
            Set(ByVal value As Boolean)
                _realtimesync = value
            End Set
        End Property

        <XmlElement("moviesetartworkspath")>
        Public Property MovieSetArtworksPath() As String
            Get
                Return _moviesetartworkspath
            End Get
            Set(ByVal value As String)
                _moviesetartworkspath = value
            End Set
        End Property

        <XmlElement("source")>
        Public Property Sources() As List(Of Source)
            Get
                Return _sources
            End Get
            Set(ByVal value As List(Of Source))
                _sources = value
            End Set
        End Property

#End Region 'Properties

    End Class


    <Serializable()>
    Class Source

#Region "Fields"

        Private _contenttype As Enums.ContentType
        Private _localpath As String
        Private _remotepath As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("contenttype")>
        Public Property ContentType() As Enums.ContentType
            Get
                Return _contenttype
            End Get
            Set(ByVal value As Enums.ContentType)
                _contenttype = value
            End Set
        End Property

        <XmlElement("localpath")>
        Public Property LocalPath() As String
            Get
                Return _localpath
            End Get
            Set(ByVal value As String)
                _localpath = value
            End Set
        End Property

        <XmlElement("remotepath")>
        Public Property RemotePath() As String
            Get
                Return _remotepath
            End Get
            Set(ByVal value As String)
                _remotepath = value
            End Set
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class

<Serializable()>
<XmlRoot("addon.trakt.tv")>
Public Class AddonSettings

#Region "Properties"

    <XmlElement("apiaccesstoken")>
    Public Property APIAccessToken() As String = String.Empty

    <XmlElement("apicreated")>
    Public Property APICreated() As String = String.Empty

    <XmlElement("apiexpiresinseconds")>
    Public Property APIExpiresInSeconds() As String = String.Empty

    <XmlElement("apirefreshtoken")>
    Public Property APIRefreshToken() As String = String.Empty

    <XmlElement("collectionremove_movie")>
    Public Property CollectionRemove_Movie() As Boolean

    <XmlElement("getshowprogress")>
    Public Property GetShowProgress() As Boolean

    <XmlElement("getwatchedstatebeforeedit_movie")>
    Public Property GetWatchedStateBeforeEdit_Movie() As Boolean

    <XmlElement("getwatchedstatebeforeedit_tvepisode")>
    Public Property GetWatchedStateBeforeEdit_TVEpisode() As Boolean

    <XmlElement("getwatchedstatescrapermulti_movie")>
    Public Property GetWatchedStateScraperMulti_Movie() As Boolean

    <XmlElement("getwatchedstatescrapermulti_tvepisode")>
    Public Property GetWatchedStateScraperMulti_TVEpisode() As Boolean

    <XmlElement("getwatchedstatescrapersingle_movie")>
    Public Property GetWatchedStateScraperSingle_Movie() As Boolean

    <XmlElement("getwatchedstatescrapersingle_tvepisode")>
    Public Property GetWatchedStateScraperSingle_TVEpisode() As Boolean

#End Region 'Properties

End Class