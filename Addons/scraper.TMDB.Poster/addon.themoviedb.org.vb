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

Imports EmberAPI
Imports NLog

Public Class Movie
    Implements Interfaces.IScraperModule_Image_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Movie
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

    Private Const _EmberAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Image_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Image_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Image_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Image_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Image_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Image_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Image_Movie.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
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

    Public Sub Init() Implements Interfaces.IScraperModule_Image_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Image_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MovieImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Image_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Public Function Run(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Image_Movie.Run
        _Logger.Trace("[TMDB_Image] [Scraper_Movie] [Start]")
        If Not DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            DBMovie.Movie.UniqueIDs.TMDbId = ModulesManager.Instance.GetMovieTMDBID(DBMovie.Movie.UniqueIDs.IMDbId)
        End If

        If DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)
            ImagesContainer = _Scraper.GetImages_Movie_MovieSet(DBMovie.Movie.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.Movie)
        End If

        _Logger.Trace("[TMDB_Image] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Image_Movie.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movie)
        End Using
    End Sub
#End Region 'Methods

End Class

Public Class Movieset
    Implements Interfaces.IScraperModule_Image_Movieset

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PrivateAPIKey As String = String.Empty
    Private _PnlSettingsPanel As frmSettingsPanel_MovieSet
    Private _Scraper As New Scraper

    Private Const _EmberAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Image_Movieset.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Image_Movieset.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Image_Movieset.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Image_Movieset.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Image_Movieset.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Image_Movieset.SettingsChanged
    Public Event StateChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IScraperModule_Image_Movieset.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
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

    Sub Init() Implements Interfaces.IScraperModule_Image_Movieset.Init
        LoadSettings_MovieSet()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Image_Movieset.InjectSettingsPanel
        LoadSettings_MovieSet()
        _PnlSettingsPanel = New frmSettingsPanel_MovieSet
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MoviesetImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Image_Movieset.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperModule_Image_Movieset.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBMovieSet As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Image_Movieset.Run
        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Start]")
        If Not DBMovieSet.MovieSet.UniqueIDs.TMDbIdSpecified AndAlso DBMovieSet.MoviesInSetSpecified Then
            DBMovieSet.MovieSet.UniqueIDs.TMDbId = ModulesManager.Instance.GetMovieCollectionID(DBMovieSet.MoviesInSet.Item(0).DBMovie.Movie.UniqueIDs.IMDbId)
        End If

        If DBMovieSet.MovieSet.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)
            ImagesContainer = _Scraper.GetImages_Movie_MovieSet(DBMovieSet.MovieSet.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.Movieset)
        End If

        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Image_Movieset.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)

        SaveSettings_MovieSet()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings_MovieSet()
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movieset)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movieset)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movieset)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movieset)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class TV
    Implements Interfaces.IScraperModule_Image_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PrivateAPIKey As String = String.Empty
    Private _PnlSettingsPanel As frmSettingsPanel_TV
    Private _Scraper As New Scraper

    Private Const _EmberAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Image_TV.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Image_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Image_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Image_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Image_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Image_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Image_TV.StateChanged


#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
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

    Sub Init() Implements Interfaces.IScraperModule_Image_TV.Init
        LoadSettings_TV()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Image_TV.InjectSettingsPanel
        LoadSettings_TV()
        _PnlSettingsPanel = New frmSettingsPanel_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeEpisodePoster.Checked = _ConfigModifier.EpisodePoster
        _PnlSettingsPanel.chkScrapeSeasonPoster.Checked = _ConfigModifier.SeasonPoster
        _PnlSettingsPanel.chkScrapeShowFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeShowPoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.TVImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Image_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperModule_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.EpisodePoster
                Return _ConfigModifier.EpisodePoster
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
            Case Enums.ModifierType.SeasonPoster
                Return _ConfigModifier.SeasonPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Image_TV.Run
        _Logger.Trace("[TMDB_Image] [Scraper_TV] [Start]")
        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        If DBTV.TVShow IsNot Nothing AndAlso Not DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
            If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                DBTV.TVShow.UniqueIDs.TMDbId = _Scraper.GetTMDBbyTVDB(DBTV.TVShow.UniqueIDs.TVDbId)
            ElseIf DBTV.TVShow.UniqueIDs.IMDbIdSpecified Then
                DBTV.TVShow.UniqueIDs.TMDbId = _Scraper.GetTMDBbyIMDB(DBTV.TVShow.UniqueIDs.IMDbId)
            End If
        End If

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TVEpisode(DBTV.TVShow.UniqueIDs.TMDbId, DBTV.TVEpisode.Season, DBTV.TVEpisode.Episode, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _Scraper.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TMDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVSeason
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVShow
                If DBTV.TVShow.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TVShow(DBTV.TVShow.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Else
                _Logger.Error(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] Unhandled ContentType"))
        End Select

        _Logger.Trace("[TMDB_Image] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Image_TV.SaveSetup
        _ConfigModifier.EpisodePoster = _PnlSettingsPanel.chkScrapeEpisodePoster.Checked
        _ConfigModifier.SeasonPoster = _PnlSettingsPanel.chkScrapeSeasonPoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeShowFanart.Checked
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapeShowPoster.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)

        SaveSettings_TV()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings_TV()
        _ConfigModifier.EpisodePoster = AdvancedSettings.GetBooleanSetting("DoEpisodePoster", True, , Enums.ContentType.TV)
        _ConfigModifier.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", _ConfigModifier.EpisodePoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonPoster", _ConfigModifier.SeasonPoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim APIKey As String

#End Region 'Fields

End Structure