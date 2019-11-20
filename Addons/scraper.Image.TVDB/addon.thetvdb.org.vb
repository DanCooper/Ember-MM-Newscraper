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
Imports ScraperModule.TVDBs

Public Class TV
    Implements Interfaces.IScraperModule_Image_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_TV

    Private strPrivateAPIKey As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Image_TV.IsEnabled

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
        LoadSettings()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Image_TV.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_TV
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

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Image_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperModule_Image_TV.QueryScraperCapabilities
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

    Function Run(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Image_TV.Run
        logger.Trace("[TVDB_Image] [Scraper] [Start]")

        LoadSettings()
        Dim _scraper As New Scraper(_AddonSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TVEpisode(DBTV.TVShow.UniqueIDs.TVDbId, DBTV.TVEpisode.Season, DBTV.TVEpisode.Episode, DBTV.EpisodeOrdering, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _scraper.GetImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVSeason
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVShow
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Else
        End Select

        logger.Trace("[TVDB_Image] [Scraper] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Image_TV.SaveSetup
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
        _AddonSettings.ApiKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
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

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim ApiKey As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class
