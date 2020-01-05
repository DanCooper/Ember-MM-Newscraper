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
    Implements Interfaces.IAddon

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _XMLAddonSettings As New XMLAddonSettings
    Private _Enabled As Boolean = True

    Private _PrivateAPIKey As String = String.Empty
    Private _PnlSettingsPanel As frmSettingsPanel
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Capabilities_AddonEventTypes As List(Of Enums.AddonEventType) Implements Interfaces.IAddon.Capabilities_AddonEventTypes
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {
                                                     Enums.AddonEventType.Scrape_Movie,
                                                     Enums.AddonEventType.Scrape_Movieset,
                                                     Enums.AddonEventType.Scrape_TVSeason,
                                                     Enums.AddonEventType.Scrape_TVShow
                                                     })
        End Get
    End Property

    Public ReadOnly Property Capabilities_ScraperCapatibilities As List(Of Enums.ScraperCapatibility) Implements Interfaces.IAddon.Capabilities_ScraperCapatibilities
        Get
            Return New List(Of Enums.ScraperCapatibility)(New Enums.ScraperCapatibility() {
                                                          Enums.ScraperCapatibility.Movie_Image_Banner,
                                                          Enums.ScraperCapatibility.Movie_Image_ClearArt,
                                                          Enums.ScraperCapatibility.Movie_Image_ClearLogo,
                                                          Enums.ScraperCapatibility.Movie_Image_DiscArt,
                                                          Enums.ScraperCapatibility.Movie_Image_Fanart,
                                                          Enums.ScraperCapatibility.Movie_Image_Landscape,
                                                          Enums.ScraperCapatibility.Movie_Image_Poster,
                                                          Enums.ScraperCapatibility.Movieset_Image_Banner,
                                                          Enums.ScraperCapatibility.Movieset_Image_ClearArt,
                                                          Enums.ScraperCapatibility.Movieset_Image_ClearLogo,
                                                          Enums.ScraperCapatibility.Movieset_Image_DiscArt,
                                                          Enums.ScraperCapatibility.Movieset_Image_Fanart,
                                                          Enums.ScraperCapatibility.Movieset_Image_Landscape,
                                                          Enums.ScraperCapatibility.Movieset_Image_Poster,
                                                          Enums.ScraperCapatibility.TVSeason_Image_Banner,
                                                          Enums.ScraperCapatibility.TVSeason_Image_Landscape,
                                                          Enums.ScraperCapatibility.TVSeason_Image_Poster,
                                                          Enums.ScraperCapatibility.TVShow_Image_Banner,
                                                          Enums.ScraperCapatibility.TVShow_Image_CharacterArt,
                                                          Enums.ScraperCapatibility.TVShow_Image_ClearArt,
                                                          Enums.ScraperCapatibility.TVShow_Image_ClearLogo,
                                                          Enums.ScraperCapatibility.TVShow_Image_Fanart,
                                                          Enums.ScraperCapatibility.TVShow_Image_Landscape,
                                                          Enums.ScraperCapatibility.TVShow_Image_Poster
                                                          })
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements Interfaces.IAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled As Boolean Implements Interfaces.IAddon.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(value As Boolean)
            _Enabled = value
        End Set
    End Property

    Public Property SettingsPanels As New List(Of Containers.SettingsPanel) Implements Interfaces.IAddon.SettingsPanels

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart As Interfaces.IAddon.NeedsRestartEventHandler Implements Interfaces.IAddon.NeedsRestart
    Public Event SettingsChanged As Interfaces.IAddon.SettingsChangedEventHandler Implements Interfaces.IAddon.SettingsChanged
    Public Event StateChanged As Interfaces.IAddon.StateChangedEventHandler Implements Interfaces.IAddon.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IAddon.Init
        Settings_Load()
        _Scraper.CreateAPI(_PrivateAPIKey)
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IAddon.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanels.Add(New Containers.SettingsPanel With {
            .Image = My.Resources.icon,
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "Fanart.tv",
            .Type = Enums.SettingsPanelType.Addon
        })
    End Sub

    Public Sub SaveSetup(DoDispose As Boolean) Implements Interfaces.IAddon.SaveSetup
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

    Public Function Run(ByRef dbElement As Database.DBElement, type As Enums.AddonEventType, lstCommandLineParams As List(Of Object)) As Interfaces.AddonResult Implements Interfaces.IAddon.Run
        _Logger.Trace("[Fanart.tv] [Run] [Started]")
        Settings_Load()
        Dim nAddonResult As New Interfaces.AddonResult

        Select Case type
            '
            'PreCheck
            '
            Case Enums.AddonEventType.Scrape_Movie_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.IMDbIdSpecified OrElse dbElement.MainDetails.UniqueIDs.TMDbIdSpecified
            Case Enums.AddonEventType.Scrape_Movieset_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.TMDbIdSpecified
            Case Enums.AddonEventType.Scrape_TVEpisode_PreCheck
                nAddonResult.bPreCheckSuccessful = (dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified) AndAlso
                    dbElement.MainDetails.SeasonSpecified AndAlso dbElement.MainDetails.EpisodeSpecified
            Case Enums.AddonEventType.Scrape_TVSeason_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified AndAlso
                    dbElement.MainDetails.SeasonSpecified
            Case Enums.AddonEventType.Scrape_TVShow_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.MainDetails.UniqueIDs.TVDbIdSpecified
            '
            'Scraping
            '
            Case Enums.AddonEventType.Scrape_Movie
                If dbElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    nAddonResult.ScraperResult_ImageContainer = _Scraper.GetImages_Movie_MovieSet(dbElement.MainDetails.UniqueIDs.TMDbId.ToString, dbElement.ScrapeModifiers)
                ElseIf dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    nAddonResult.ScraperResult_ImageContainer = _Scraper.GetImages_Movie_MovieSet(dbElement.MainDetails.UniqueIDs.IMDbId, dbElement.ScrapeModifiers)
                Else
                    _Logger.Trace(String.Concat("[Fanart.tv] [Run] [Abort] No TMDB and IMDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.AddonEventType.Scrape_Movieset
                If Not dbElement.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso dbElement.MoviesInSetSpecified Then
                    dbElement.MainDetails.UniqueIDs.TMDbId = AddonsManager.Instance.GetMovieCollectionID(dbElement.MoviesInSet.Item(0).DBMovie.MainDetails.UniqueIDs.IMDbId)
                End If

                If dbElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    nAddonResult.ScraperResult_ImageContainer = _Scraper.GetImages_Movie_MovieSet(dbElement.MainDetails.UniqueIDs.TMDbId.ToString, dbElement.ScrapeModifiers)
                End If
            Case Enums.AddonEventType.Scrape_TVEpisode
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    If dbElement.ScrapeModifiers.MainFanart Then
                        nAddonResult.ScraperResult_ImageContainer.MainFanarts = _Scraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, dbElement.ScrapeModifiers).MainFanarts
                    End If
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.AddonEventType.Scrape_TVSeason
                If dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    nAddonResult.ScraperResult_ImageContainer = _Scraper.GetImages_TV(dbElement.TVShowDetails.UniqueIDs.TVDbId, dbElement.ScrapeModifiers)
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
            Case Enums.AddonEventType.Scrape_TVShow
                If dbElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                    nAddonResult.ScraperResult_ImageContainer = _Scraper.GetImages_TV(dbElement.MainDetails.UniqueIDs.TVDbId, dbElement.ScrapeModifiers)
                Else
                    _Logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", dbElement.MainDetails.Title))
                End If
        End Select

        _Logger.Trace("[Fanart.tv] [Run] [Done]")
        Return nAddonResult
    End Function

#End Region 'Interface Methods

#Region "Methodes"

    Private Sub Settings_Load()
        _PrivateAPIKey = _XMLAddonSettings.GetStringSetting("ApiKey", String.Empty)
    End Sub

    Private Sub Settings_Save()
        _XMLAddonSettings.SetStringSetting("ApiKey", _PrivateAPIKey)
        _XMLAddonSettings.Save()
    End Sub

#End Region 'Methodes

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim ApiKey As String

#End Region 'Fields

End Structure