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

Public Class Data_TV
    Implements Interfaces.IScraperAddon_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigScrapeModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _PnlSettingsPanel As frmSettingsPanel_Data_TV
    Private _PrivateAPIKey As String = String.Empty


#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_TV.IsEnabled

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

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        IsEnabled = State
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, DiffOrder)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IScraperAddon_Data_TV.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey
        _PnlSettingsPanel.chkScraperEpisodeActors.Checked = _ConfigScrapeOptions.Episodes.Actors
        _PnlSettingsPanel.chkScraperEpisodeAired.Checked = _ConfigScrapeOptions.Episodes.Aired
        _PnlSettingsPanel.chkScraperEpisodeCredits.Checked = _ConfigScrapeOptions.Episodes.Credits
        _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked = _ConfigScrapeOptions.Episodes.Directors
        _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked = _ConfigScrapeOptions.Episodes.GuestStars
        _PnlSettingsPanel.chkScraperEpisodePlot.Checked = _ConfigScrapeOptions.Episodes.Plot
        _PnlSettingsPanel.chkScraperEpisodeRating.Checked = _ConfigScrapeOptions.Episodes.Ratings
        _PnlSettingsPanel.chkScraperEpisodeTitle.Checked = _ConfigScrapeOptions.Episodes.Title
        _PnlSettingsPanel.chkScraperShowActors.Checked = _ConfigScrapeOptions.Actors
        _PnlSettingsPanel.chkScraperShowEpisodeGuide.Checked = _ConfigScrapeOptions.EpisodeGuideURL
        _PnlSettingsPanel.chkScraperShowGenres.Checked = _ConfigScrapeOptions.Genres
        _PnlSettingsPanel.chkScraperShowCertifications.Checked = _ConfigScrapeOptions.Certifications
        _PnlSettingsPanel.chkScraperShowPlot.Checked = _ConfigScrapeOptions.Plot
        _PnlSettingsPanel.chkScraperShowPremiered.Checked = _ConfigScrapeOptions.Premiered
        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigScrapeOptions.Ratings
        _PnlSettingsPanel.chkScraperShowRuntime.Checked = _ConfigScrapeOptions.Runtime
        _PnlSettingsPanel.chkScraperShowStatus.Checked = _ConfigScrapeOptions.Status
        _PnlSettingsPanel.chkScraperShowStudios.Checked = _ConfigScrapeOptions.Studios
        _PnlSettingsPanel.chkScraperShowTitle.Checked = _ConfigScrapeOptions.Title

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
            .Type = Enums.SettingsPanelType.TVData,
            .Title = "TheTVDB.com"
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run_TVEpisode(ByRef oDBTVEpisode As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperAddon_Data_TV.Run_TVEpisode
        _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Start]")

        Settings_Load()

        Dim Settings As New AddonSettings
        Settings.APIKey = _AddonSettings.APIKey
        Settings.Language = oDBTVEpisode.Language_Main

        Dim nTVEpisode As New MediaContainers.MainDetails
        Dim _scraper As New Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        If oDBTVEpisode.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
            If Not oDBTVEpisode.MainDetails.Episode = -1 AndAlso Not oDBTVEpisode.MainDetails.Season = -1 Then
                nTVEpisode = _scraper.GetData_TVEpisode(CInt(oDBTVEpisode.TVShowDetails.UniqueIDs.TVDbId), oDBTVEpisode.MainDetails.Season, oDBTVEpisode.MainDetails.Episode, oDBTVEpisode.EpisodeOrdering, FilteredOptions)
            ElseIf oDBTVEpisode.MainDetails.AiredSpecified Then
                nTVEpisode = _scraper.GetData_TVEpisode(CInt(oDBTVEpisode.TVShowDetails.UniqueIDs.TVDbId), oDBTVEpisode.MainDetails.Aired, FilteredOptions)
            Else
                _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Abort] No TV Show TVDB ID and also no AiredDate available")
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
        End If

        _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Run_TVSeason(ByRef oDBTVSeason As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperAddon_Data_TV.Run_TVSeason
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
    End Function
    ''' <summary>
    '''  Scrape TVShowDetails from TVDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run_TVShow(ByRef oDBTV As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperAddon_Data_TV.Run_TVShow
        _Logger.Trace("[TVDB_Data] [Scraper_TV] [Start]")

        Settings_Load()

        Dim Settings As New AddonSettings
        Settings.APIKey = _AddonSettings.APIKey
        Settings.Language = oDBTV.Language_Main

        Dim nTVShow As MediaContainers.MainDetails = Nothing
        Dim _scraper As New Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBTV.MainDetails.UniqueIDs.TVDbIdSpecified Then
                'TVDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                nTVShow = _scraper.GetData_TV(oDBTV.MainDetails.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBTV.MainDetails.UniqueIDs.IMDbIdSpecified Then
                oDBTV.MainDetails.UniqueIDs.TVDbId = _scraper.Get_TVDbID_By_IMDbID(oDBTV.MainDetails.UniqueIDs.IMDbId)
                If Not oDBTV.MainDetails.UniqueIDs.TVDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _scraper.GetData_TV(oDBTV.MainDetails.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If oDBTV.MainDetails.TitleSpecified Then
                    nTVShow = _scraper.GetSearchTVShowInfo(oDBTV.MainDetails.Title, oDBTV, ScrapeType, ScrapeModifiers, FilteredOptions)
                End If
                'if still no search result -> exit
                If nTVShow Is Nothing Then
                    _Logger.Trace("[TVDB_Data] [Scraper_TV] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                End If
            End If
        End If

        If nTVShow Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace("[TVDB_Data] [Scraper_TV] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[TVDB_Data] [Scraper_TV] [Done]")
            Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBTV.MainDetails.UniqueIDs.TVDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults(Settings, _scraper)
                    If dlgSearch.ShowDialog(oDBTV.MainDetails.Title, oDBTV.ShowPath, ScrapeModifiers, FilteredOptions) = DialogResult.OK Then
                        nTVShow = _scraper.GetData_TV(dlgSearch.Result.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace("[TVDB_Data] [Scraper_TV] [Abort] [Cancelled] Cancelled by user")
                        Return New Interfaces.ModuleResult_Data_TVShow With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[TVDB_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_TV.SaveSetup
        _ConfigScrapeOptions.Episodes.Actors = _PnlSettingsPanel.chkScraperEpisodeActors.Checked
        _ConfigScrapeOptions.Episodes.Aired = _PnlSettingsPanel.chkScraperEpisodeAired.Checked
        _ConfigScrapeOptions.Episodes.Credits = _PnlSettingsPanel.chkScraperEpisodeCredits.Checked
        _ConfigScrapeOptions.Episodes.Directors = _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked
        _ConfigScrapeOptions.Episodes.GuestStars = _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked
        _ConfigScrapeOptions.Episodes.Plot = _PnlSettingsPanel.chkScraperEpisodePlot.Checked
        _ConfigScrapeOptions.Episodes.Ratings = _PnlSettingsPanel.chkScraperEpisodeRating.Checked
        _ConfigScrapeOptions.Episodes.Title = _PnlSettingsPanel.chkScraperEpisodeTitle.Checked
        _ConfigScrapeOptions.Actors = _PnlSettingsPanel.chkScraperShowActors.Checked
        _ConfigScrapeOptions.Certifications = _PnlSettingsPanel.chkScraperShowCertifications.Checked
        _ConfigScrapeOptions.EpisodeGuideURL = _PnlSettingsPanel.chkScraperShowEpisodeGuide.Checked
        _ConfigScrapeOptions.Genres = _PnlSettingsPanel.chkScraperShowGenres.Checked
        _ConfigScrapeOptions.Plot = _PnlSettingsPanel.chkScraperShowPlot.Checked
        _ConfigScrapeOptions.Premiered = _PnlSettingsPanel.chkScraperShowPremiered.Checked
        _ConfigScrapeOptions.Ratings = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigScrapeOptions.Runtime = _PnlSettingsPanel.chkScraperShowRuntime.Checked
        _ConfigScrapeOptions.Status = _PnlSettingsPanel.chkScraperShowStatus.Checked
        _ConfigScrapeOptions.Studios = _PnlSettingsPanel.chkScraperShowStudios.Checked
        _ConfigScrapeOptions.Title = _PnlSettingsPanel.chkScraperShowTitle.Checked

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
        _ConfigScrapeOptions.Episodes.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Aired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Credits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Directors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.GuestStars = AdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Certifications = AdvancedSettings.GetBooleanSetting("DoCertification", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.EpisodeGuideURL = AdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Genres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Premiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Runtime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Status = AdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Studios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", "")
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
        _ConfigScrapeModifier.DoSearch = True
        _ConfigScrapeModifier.EpisodeMeta = True
        _ConfigScrapeModifier.MainNFO = True
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", _ConfigScrapeOptions.Episodes.Actors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigScrapeOptions.Episodes.Aired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", _ConfigScrapeOptions.Episodes.Credits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", _ConfigScrapeOptions.Episodes.Directors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", _ConfigScrapeOptions.Episodes.GuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Episodes.Plot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Episodes.Ratings, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Episodes.Title, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", _ConfigScrapeOptions.Actors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCertification", _ConfigScrapeOptions.Certifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", _ConfigScrapeOptions.EpisodeGuideURL, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", _ConfigScrapeOptions.Genres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Plot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", _ConfigScrapeOptions.Premiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Ratings, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", _ConfigScrapeOptions.Runtime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", _ConfigScrapeOptions.Status, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", _ConfigScrapeOptions.Studios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Title, , , Enums.ContentType.TVShow)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class Image_TV
    Implements Interfaces.IScraperAddon_Image_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_Image_TV

    Private strPrivateAPIKey As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_TV.IsEnabled

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
        _PnlSettingsPanel = New frmSettingsPanel_Image_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeEpisodePoster.Checked = _ConfigModifier.EpisodePoster
        _PnlSettingsPanel.chkScrapeSeasonBanner.Checked = _ConfigModifier.SeasonBanner
        _PnlSettingsPanel.chkScrapeSeasonPoster.Checked = _ConfigModifier.SeasonPoster
        _PnlSettingsPanel.chkScrapeShowBanner.Checked = _ConfigModifier.MainBanner
        _PnlSettingsPanel.chkScrapeShowFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeShowPoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
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
            .Title = "TheTVDb.com",
            .Type = Enums.SettingsPanelType.TVImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.EpisodePoster
                Return _ConfigModifier.EpisodePoster
            Case Enums.ModifierType.SeasonBanner
                Return _ConfigModifier.SeasonBanner
            Case Enums.ModifierType.SeasonPoster
                Return _ConfigModifier.SeasonPoster
            Case Enums.ModifierType.MainBanner
                Return _ConfigModifier.MainBanner
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Function Run(ByRef dbElement As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_TV.Run
        logger.Trace("[TVDB_Image] [Scraper] [Start]")

        LoadSettings()
        Dim _scraper As New Scraper(_AddonSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        Select Case dbElement.ContentType
            Case Enums.ContentType.TVEpisode
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TVEpisode(dbElement.TVShowDetails.UniqueIDs.TVDbId, dbElement.MainDetails.Season, dbElement.MainDetails.Episode, dbElement.EpisodeOrdering, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _scraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.ContentType.TVSeason
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.ContentType.TVShow
                If dbElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TV(dbElement.MainDetails.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Else
        End Select

        logger.Trace("[TVDB_Image] [Scraper] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_TV.SaveSetup
        _ConfigModifier.EpisodePoster = _PnlSettingsPanel.chkScrapeEpisodePoster.Checked
        _ConfigModifier.SeasonBanner = _PnlSettingsPanel.chkScrapeSeasonBanner.Checked
        _ConfigModifier.SeasonPoster = _PnlSettingsPanel.chkScrapeSeasonPoster.Checked
        _ConfigModifier.MainBanner = _PnlSettingsPanel.chkScrapeShowBanner.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeShowFanart.Checked
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
        _ConfigModifier.EpisodePoster = AdvancedSettings.GetBooleanSetting("DoEpisodePoster", True)
        _ConfigModifier.SeasonBanner = AdvancedSettings.GetBooleanSetting("DoSeasonBanner", True)
        _ConfigModifier.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True)
        _ConfigModifier.MainBanner = AdvancedSettings.GetBooleanSetting("DoShowBanner", True)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True)
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True)

        strPrivateAPIKey = AdvancedSettings.GetSetting("ApiKey", "")
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), My.Resources.EmberAPIKey, strPrivateAPIKey)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", _ConfigModifier.EpisodePoster)
            settings.SetBooleanSetting("DoSeasonBanner", _ConfigModifier.SeasonBanner)
            settings.SetBooleanSetting("DoSeasonPoster", _ConfigModifier.SeasonPoster)
            settings.SetBooleanSetting("DoShowBanner", _ConfigModifier.MainBanner)
            settings.SetBooleanSetting("DoShowFanart", _ConfigModifier.MainFanart)
            settings.SetBooleanSetting("DoShowPoster", _ConfigModifier.MainPoster)

            settings.SetSetting("ApiKey", _PnlSettingsPanel.txtApiKey.Text)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim APIKey As String
    Dim Language As String

#End Region 'Fields

End Structure