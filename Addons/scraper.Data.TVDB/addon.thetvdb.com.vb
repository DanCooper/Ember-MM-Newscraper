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
    Implements Interfaces.IScraperModule_Data_TV


#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigOptions As New Structures.ScrapeOptions
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel

    Private strPrivateAPIKey As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Data_TV.IsEnabled

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

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        IsEnabled = State
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, DiffOrder)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IScraperModule_Data_TV.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.txtApiKey.Text = strPrivateAPIKey
        _PnlSettingsPanel.chkScraperEpisodeActors.Checked = _ConfigOptions.bEpisodeActors
        _PnlSettingsPanel.chkScraperEpisodeAired.Checked = _ConfigOptions.bEpisodeAired
        _PnlSettingsPanel.chkScraperEpisodeCredits.Checked = _ConfigOptions.bEpisodeCredits
        _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked = _ConfigOptions.bEpisodeDirectors
        _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked = _ConfigOptions.bEpisodeGuestStars
        _PnlSettingsPanel.chkScraperEpisodePlot.Checked = _ConfigOptions.bEpisodePlot
        _PnlSettingsPanel.chkScraperEpisodeRating.Checked = _ConfigOptions.bEpisodeRating
        _PnlSettingsPanel.chkScraperEpisodeTitle.Checked = _ConfigOptions.bEpisodeTitle
        _PnlSettingsPanel.chkScraperShowActors.Checked = _ConfigOptions.bMainActors
        _PnlSettingsPanel.chkScraperShowEpisodeGuide.Checked = _ConfigOptions.bMainEpisodeGuide
        _PnlSettingsPanel.chkScraperShowGenres.Checked = _ConfigOptions.bMainGenres
        _PnlSettingsPanel.chkScraperShowCertifications.Checked = _ConfigOptions.bMainCertifications
        _PnlSettingsPanel.chkScraperShowPlot.Checked = _ConfigOptions.bMainPlot
        _PnlSettingsPanel.chkScraperShowPremiered.Checked = _ConfigOptions.bMainPremiered
        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigOptions.bMainRating
        _PnlSettingsPanel.chkScraperShowRuntime.Checked = _ConfigOptions.bMainRuntime
        _PnlSettingsPanel.chkScraperShowStatus.Checked = _ConfigOptions.bMainStatus
        _PnlSettingsPanel.chkScraperShowStudios.Checked = _ConfigOptions.bMainStudios
        _PnlSettingsPanel.chkScraperShowTitle.Checked = _ConfigOptions.bMainTitle

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
            .Type = Enums.SettingsPanelType.TVData,
            .Title = "TheTVDB.com"
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run_TVEpisode(ByRef oDBTVEpisode As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperModule_Data_TV.Run_TVEpisode
        _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Start]")

        Settings_Load()

        Dim Settings As New AddonSettings
        Settings.APIKey = _AddonSettings.APIKey
        Settings.Language = oDBTVEpisode.Language_Main

        Dim nTVEpisode As New MediaContainers.EpisodeDetails
        Dim _scraper As New Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If oDBTVEpisode.TVShow.UniqueIDs.TVDbIdSpecified Then
            If Not oDBTVEpisode.TVEpisode.Episode = -1 AndAlso Not oDBTVEpisode.TVEpisode.Season = -1 Then
                nTVEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.UniqueIDs.TVDbId), oDBTVEpisode.TVEpisode.Season, oDBTVEpisode.TVEpisode.Episode, oDBTVEpisode.EpisodeOrdering, FilteredOptions)
            ElseIf oDBTVEpisode.TVEpisode.AiredSpecified Then
                nTVEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.UniqueIDs.TVDbId), oDBTVEpisode.TVEpisode.Aired, FilteredOptions)
            Else
                _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Abort] No TV Show TVDB ID and also no AiredDate available")
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
        End If

        _Logger.Trace("[TVDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Run_TVSeason(ByRef oDBTVSeason As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperModule_Data_TV.Run_TVSeason
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
    End Function
    ''' <summary>
    '''  Scrape TVShowDetails from TVDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TVDB ID for next scraper</param>
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run_TVShow(ByRef oDBTV As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperModule_Data_TV.Run_TVShow
        _Logger.Trace("[TVDB_Data] [Scraper_TV] [Start]")

        Settings_Load()

        Dim Settings As New AddonSettings
        Settings.APIKey = _AddonSettings.APIKey
        Settings.Language = oDBTV.Language_Main

        Dim nTVShow As MediaContainers.TVShow = Nothing
        Dim _scraper As New Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                'TVDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                nTVShow = _scraper.GetTVShowInfo(oDBTV.TVShow.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBTV.TVShow.UniqueIDs.IMDbIdSpecified Then
                oDBTV.TVShow.UniqueIDs.TVDbId = _scraper.GetTVDBbyIMDB(oDBTV.TVShow.UniqueIDs.IMDbId)
                If Not oDBTV.TVShow.UniqueIDs.TVDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _scraper.GetTVShowInfo(oDBTV.TVShow.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If oDBTV.TVShow.TitleSpecified Then
                    nTVShow = _scraper.GetSearchTVShowInfo(oDBTV.TVShow.Title, oDBTV, ScrapeType, ScrapeModifiers, FilteredOptions)
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
            If Not oDBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults(Settings, _scraper)
                    If dlgSearch.ShowDialog(oDBTV.TVShow.Title, oDBTV.ShowPath, ScrapeModifiers, FilteredOptions) = DialogResult.OK Then
                        nTVShow = _scraper.GetTVShowInfo(dlgSearch.Result.UniqueIDs.TVDbId, ScrapeModifiers, FilteredOptions, False)
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

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_TV.SaveSetup
        _ConfigOptions.bEpisodeActors = _PnlSettingsPanel.chkScraperEpisodeActors.Checked
        _ConfigOptions.bEpisodeAired = _PnlSettingsPanel.chkScraperEpisodeAired.Checked
        _ConfigOptions.bEpisodeCredits = _PnlSettingsPanel.chkScraperEpisodeCredits.Checked
        _ConfigOptions.bEpisodeDirectors = _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked
        _ConfigOptions.bEpisodeGuestStars = _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked
        _ConfigOptions.bEpisodePlot = _PnlSettingsPanel.chkScraperEpisodePlot.Checked
        _ConfigOptions.bEpisodeRating = _PnlSettingsPanel.chkScraperEpisodeRating.Checked
        _ConfigOptions.bEpisodeTitle = _PnlSettingsPanel.chkScraperEpisodeTitle.Checked
        _ConfigOptions.bMainActors = _PnlSettingsPanel.chkScraperShowActors.Checked
        _ConfigOptions.bMainCertifications = _PnlSettingsPanel.chkScraperShowCertifications.Checked
        _ConfigOptions.bMainEpisodeGuide = _PnlSettingsPanel.chkScraperShowEpisodeGuide.Checked
        _ConfigOptions.bMainGenres = _PnlSettingsPanel.chkScraperShowGenres.Checked
        _ConfigOptions.bMainPlot = _PnlSettingsPanel.chkScraperShowPlot.Checked
        _ConfigOptions.bMainPremiered = _PnlSettingsPanel.chkScraperShowPremiered.Checked
        _ConfigOptions.bMainRating = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigOptions.bMainRuntime = _PnlSettingsPanel.chkScraperShowRuntime.Checked
        _ConfigOptions.bMainStatus = _PnlSettingsPanel.chkScraperShowStatus.Checked
        _ConfigOptions.bMainStudios = _PnlSettingsPanel.chkScraperShowStudios.Checked
        _ConfigOptions.bMainTitle = _PnlSettingsPanel.chkScraperShowTitle.Checked
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
        _ConfigOptions.bEpisodeActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeAired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeCredits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeDirectors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeGuestStars = AdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodePlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bMainActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCertification", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainEpisodeGuide = AdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainGenres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainPremiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainStatus = AdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainStudios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        strPrivateAPIKey = AdvancedSettings.GetSetting("APIKey", "")
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
        _ConfigModifier.DoSearch = True
        _ConfigModifier.EpisodeMeta = True
        _ConfigModifier.MainNFO = True
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", _ConfigOptions.bEpisodeActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigOptions.bEpisodeAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", _ConfigOptions.bEpisodeCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", _ConfigOptions.bEpisodeDirectors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", _ConfigOptions.bEpisodeGuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.bEpisodePlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.bEpisodeRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.bEpisodeTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", _ConfigOptions.bMainActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCertification", _ConfigOptions.bMainCertifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", _ConfigOptions.bMainEpisodeGuide, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", _ConfigOptions.bMainGenres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.bMainPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", _ConfigOptions.bMainPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.bMainRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", _ConfigOptions.bMainRuntime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", _ConfigOptions.bMainStatus, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", _ConfigOptions.bMainStudios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.bMainTitle, , , Enums.ContentType.TVShow)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim APIKey As String
        Dim Language As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class