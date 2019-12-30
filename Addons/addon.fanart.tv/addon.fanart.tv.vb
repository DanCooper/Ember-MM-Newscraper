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

Public Class Image_Movie
    Implements Interfaces.IScraperAddon_Image_Movie


#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_Movie

#End Region 'Fields

#Region "Properties"

    Private Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_Movie.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Image_Movie.StateChanged

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

    Sub Init() Implements Interfaces.IScraperAddon_Image_Movie.Init
        Settings_Load()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeBanner.Checked = _ConfigModifier.MainBanner
        _PnlSettingsPanel.chkScrapeClearArt.Checked = _ConfigModifier.MainClearArt
        _PnlSettingsPanel.chkScrapeClearLogo.Checked = _ConfigModifier.MainClearLogo
        _PnlSettingsPanel.chkScrapeDiscArt.Checked = _ConfigModifier.MainDiscArt
        _PnlSettingsPanel.chkScrapeLandscape.Checked = _ConfigModifier.MainLandscape
        _PnlSettingsPanel.txtApiKey.Text = _AddonSettings.ApiKey

        If Not String.IsNullOrEmpty(_AddonSettings.ApiKey) Then
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
            .Title = "Fanart.tv",
            .Type = Enums.SettingsPanelType.MovieImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return _ConfigModifier.MainBanner
            Case Enums.ModifierType.MainClearArt
                Return _ConfigModifier.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return _ConfigModifier.MainClearLogo
            Case Enums.ModifierType.MainDiscArt
                Return _ConfigModifier.MainDiscArt
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return _ConfigModifier.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_Movie.Run
        _Logger.Trace("[FanartTV_Image] [Scraper_Movie] [Start]")

        Settings_Load()
        Dim imageScraper As New Scraper(_AddonSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        If DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            ImagesContainer = imageScraper.GetImages_Movie_MovieSet(DBMovie.MainDetails.UniqueIDs.TMDbId, FilteredModifiers)
        ElseIf DBMovie.MainDetails.UniqueIDs.IMDbIdSpecified Then
            ImagesContainer = imageScraper.GetImages_Movie_MovieSet(DBMovie.MainDetails.UniqueIDs.IMDbId, FilteredModifiers)
        Else
            _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_Movie] [Abort] No TMDB and IMDB ID exist to search: ", DBMovie.MainDetails.Title))
        End If

        _Logger.Trace("[FanartTV_Image] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_Movie.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked
        _ConfigModifier.MainBanner = _PnlSettingsPanel.chkScrapeBanner.Checked
        _ConfigModifier.MainClearArt = _PnlSettingsPanel.chkScrapeClearArt.Checked
        _ConfigModifier.MainClearLogo = _PnlSettingsPanel.chkScrapeClearLogo.Checked
        _ConfigModifier.MainDiscArt = _PnlSettingsPanel.chkScrapeDiscArt.Checked
        _ConfigModifier.MainLandscape = _PnlSettingsPanel.chkScrapeLandscape.Checked
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
        _AddonSettings.ApiKey = AdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.Movie)
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainBanner = AdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainClearArt = AdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainClearLogo = AdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainDiscArt = AdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainLandscape = AdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoBanner", _ConfigModifier.MainBanner, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoClearArt", _ConfigModifier.MainClearArt, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoClearLogo", _ConfigModifier.MainClearLogo, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDiscArt", _ConfigModifier.MainDiscArt, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoLandscape", _ConfigModifier.MainLandscape, , , Enums.ContentType.Movie)
            settings.SetSetting("ApiKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Image_Movieset
    Implements Interfaces.IScraperAddon_Image_Movieset

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_MovieSet

#End Region 'Fields

#Region "Properties"

    Private Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_Movieset.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_Movieset.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_Movieset.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_Movieset.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_Movieset.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_Movieset.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Image_Movieset.StateChanged

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

    Sub Init() Implements Interfaces.IScraperAddon_Image_Movieset.Init
        LoadSettings()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_Movieset.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_MovieSet
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeBanner.Checked = _ConfigModifier.MainBanner
        _PnlSettingsPanel.chkScrapeClearArt.Checked = _ConfigModifier.MainClearArt
        _PnlSettingsPanel.chkScrapeClearLogo.Checked = _ConfigModifier.MainClearLogo
        _PnlSettingsPanel.chkScrapeDiscArt.Checked = _ConfigModifier.MainDiscArt
        _PnlSettingsPanel.chkScrapeLandscape.Checked = _ConfigModifier.MainLandscape
        _PnlSettingsPanel.txtApiKey.Text = _AddonSettings.ApiKey

        If Not String.IsNullOrEmpty(_AddonSettings.ApiKey) Then
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
            .Title = "Fanart.tv",
            .Type = Enums.SettingsPanelType.MoviesetImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_Movieset.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_Movieset.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return _ConfigModifier.MainBanner
            Case Enums.ModifierType.MainClearArt
                Return _ConfigModifier.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return _ConfigModifier.MainClearLogo
            Case Enums.ModifierType.MainDiscArt
                Return _ConfigModifier.MainDiscArt
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return _ConfigModifier.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBMovieset As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_Movieset.Run
        _Logger.Trace("[FanartTV_Image] [Scraper_MovieSet] [Start]")

        If Not DBMovieset.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso DBMovieset.MoviesInSetSpecified Then
            DBMovieset.MainDetails.UniqueIDs.TMDbId = AddonsManager.Instance.GetMovieCollectionID(DBMovieset.MoviesInSet.Item(0).DBMovie.MainDetails.UniqueIDs.IMDbId)
        End If

        If DBMovieset.MainDetails.UniqueIDs.TMDbIdSpecified Then
            LoadSettings()
            Dim imageScraper As New Scraper(_AddonSettings)

            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

            ImagesContainer = imageScraper.GetImages_Movie_MovieSet(DBMovieset.MainDetails.UniqueIDs.TMDbId, FilteredModifiers)
        End If

        _Logger.Trace("[FanartTV_Image] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_Movieset.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked
        _ConfigModifier.MainBanner = _PnlSettingsPanel.chkScrapeBanner.Checked
        _ConfigModifier.MainClearArt = _PnlSettingsPanel.chkScrapeClearArt.Checked
        _ConfigModifier.MainClearLogo = _PnlSettingsPanel.chkScrapeClearLogo.Checked
        _ConfigModifier.MainDiscArt = _PnlSettingsPanel.chkScrapeDiscArt.Checked
        _ConfigModifier.MainLandscape = _PnlSettingsPanel.chkScrapeLandscape.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings()
        _AddonSettings.ApiKey = AdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.Movieset)

        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainBanner = AdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainClearArt = AdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainClearLogo = AdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainDiscArt = AdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainLandscape = AdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.ContentType.Movieset)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoBanner", _ConfigModifier.MainBanner, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoClearArt", _ConfigModifier.MainClearArt, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoClearLogo", _ConfigModifier.MainClearLogo, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoDiscArt", _ConfigModifier.MainDiscArt, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoLandscape", _ConfigModifier.MainLandscape, , , Enums.ContentType.Movieset)

            settings.SetSetting("ApiKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movieset)
        End Using
    End Sub

#End Region 'Methods

End Class


Public Class Image_TV
    Implements Interfaces.IScraperAddon_Image_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_TV

#End Region 'Fields

#Region "Properties"

    Private Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_TV.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Image_TV.StateChanged

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

    Sub Init() Implements Interfaces.IScraperAddon_Image_TV.Init
        LoadSettings()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_TV.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeSeasonBanner.Checked = _ConfigModifier.SeasonBanner
        _PnlSettingsPanel.chkScrapeSeasonLandscape.Checked = _ConfigModifier.SeasonLandscape
        _PnlSettingsPanel.chkScrapeSeasonPoster.Checked = _ConfigModifier.SeasonPoster
        _PnlSettingsPanel.chkScrapeShowBanner.Checked = _ConfigModifier.MainBanner
        _PnlSettingsPanel.chkScrapeShowCharacterArt.Checked = _ConfigModifier.MainCharacterArt
        _PnlSettingsPanel.chkScrapeShowClearArt.Checked = _ConfigModifier.MainClearArt
        _PnlSettingsPanel.chkScrapeShowClearLogo.Checked = _ConfigModifier.MainClearLogo
        _PnlSettingsPanel.chkScrapeShowFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeShowLandscape.Checked = _ConfigModifier.MainLandscape
        _PnlSettingsPanel.chkScrapeShowPoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _AddonSettings.ApiKey

        If Not String.IsNullOrEmpty(_AddonSettings.ApiKey) Then
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
            .Title = "Fanart.tv",
            .Type = Enums.SettingsPanelType.TVImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return _ConfigModifier.MainBanner
            Case Enums.ModifierType.MainCharacterArt
                Return _ConfigModifier.MainCharacterArt
            Case Enums.ModifierType.MainClearArt
                Return _ConfigModifier.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return _ConfigModifier.MainClearLogo
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return _ConfigModifier.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
            Case Enums.ModifierType.SeasonBanner
                Return _ConfigModifier.SeasonBanner
            Case Enums.ModifierType.SeasonLandscape
                Return _ConfigModifier.SeasonLandscape
            Case Enums.ModifierType.SeasonPoster
                Return _ConfigModifier.SeasonPoster
        End Select
        Return False
    End Function

    Function Run(ByRef dbElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_TV.Run
        _Logger.Trace("[FanartTV_Image] [Scraper_TV] [Start]")

        LoadSettings()
        Dim imageScraper As New Scraper(_AddonSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        Select Case dbElement.ContentType
            Case Enums.ContentType.TVEpisode
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = imageScraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.ContentType.TVSeason
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = imageScraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.ContentType.TVShow
                If dbElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = imageScraper.GetImages_TV(dbElement.MainDetails.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Else
                _Logger.Error(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] Unhandled ContentType"))
        End Select

        _Logger.Trace("[FanartTV_Image] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_TV.SaveSetup
        _ConfigModifier.SeasonBanner = _PnlSettingsPanel.chkScrapeSeasonBanner.Checked
        _ConfigModifier.SeasonLandscape = _PnlSettingsPanel.chkScrapeSeasonLandscape.Checked
        _ConfigModifier.SeasonPoster = _PnlSettingsPanel.chkScrapeSeasonPoster.Checked
        _ConfigModifier.MainBanner = _PnlSettingsPanel.chkScrapeShowBanner.Checked
        _ConfigModifier.MainCharacterArt = _PnlSettingsPanel.chkScrapeShowCharacterArt.Checked
        _ConfigModifier.MainClearArt = _PnlSettingsPanel.chkScrapeShowClearArt.Checked
        _ConfigModifier.MainClearLogo = _PnlSettingsPanel.chkScrapeShowClearLogo.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeShowFanart.Checked
        _ConfigModifier.MainLandscape = _PnlSettingsPanel.chkScrapeShowLandscape.Checked
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapeShowPoster.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings()
        _AddonSettings.ApiKey = AdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.TV)

        _ConfigModifier.SeasonBanner = AdvancedSettings.GetBooleanSetting("DoSeasonBanner", True, , Enums.ContentType.TV)
        _ConfigModifier.SeasonLandscape = AdvancedSettings.GetBooleanSetting("DoSeasonLandscape", True, , Enums.ContentType.TV)
        _ConfigModifier.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
        _ConfigModifier.MainBanner = AdvancedSettings.GetBooleanSetting("DoShowBanner", True, , Enums.ContentType.TV)
        _ConfigModifier.MainCharacterArt = AdvancedSettings.GetBooleanSetting("DoShowCharacterArt", True, , Enums.ContentType.TV)
        _ConfigModifier.MainClearArt = AdvancedSettings.GetBooleanSetting("DoShowClearArt", True, , Enums.ContentType.TV)
        _ConfigModifier.MainClearLogo = AdvancedSettings.GetBooleanSetting("DoShowClearLogo", True, , Enums.ContentType.TV)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
        _ConfigModifier.MainLandscape = AdvancedSettings.GetBooleanSetting("DoShowLandscape", True, , Enums.ContentType.TV)
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoSeasonBanner", _ConfigModifier.SeasonBanner, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonLandscape", _ConfigModifier.SeasonLandscape, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonPoster", _ConfigModifier.SeasonPoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowBanner", _ConfigModifier.MainBanner, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowCharacterArt", _ConfigModifier.MainCharacterArt, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowClearArt", _ConfigModifier.MainClearArt, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowClearLogo", _ConfigModifier.MainClearLogo, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowLandscape", _ConfigModifier.MainLandscape, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.TV)

            settings.SetSetting("ApiKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim ApiKey As String

#End Region 'Fields

End Structure