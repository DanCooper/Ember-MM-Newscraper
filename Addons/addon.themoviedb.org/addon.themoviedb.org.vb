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

    Private _PrivateAPIKey As String = String.Empty
    Private _AddonSettings_Movie As New AddonSettings
    Private _AddonSettings_MovieSet As New AddonSettings
    Private _AddonSettings_TV As New AddonSettings
    Private _PnlSettingsPanel As frmSettingsPanel
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Capabilities_AddonEventTypes As List(Of Enums.AddonEventType) Implements Interfaces.IAddon.Capabilities_AddonEventTypes
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {
                                                     Enums.AddonEventType.Scrape_Movie,
                                                     Enums.AddonEventType.Scrape_Movieset,
                                                     Enums.AddonEventType.Scrape_TVEpisode,
                                                     Enums.AddonEventType.Scrape_TVSeason,
                                                     Enums.AddonEventType.Scrape_TVShow,
                                                     Enums.AddonEventType.Search_Movie,
                                                     Enums.AddonEventType.Search_Movieset,
                                                     Enums.AddonEventType.Search_TVShow
                                                     })
        End Get
    End Property

    Public ReadOnly Property Capabilities_ScraperCapatibilities As List(Of Enums.ScraperCapatibility) Implements Interfaces.IAddon.Capabilities_ScraperCapatibilities
        Get
            Return New List(Of Enums.ScraperCapatibility)(New Enums.ScraperCapatibility() {
                                                          Enums.ScraperCapatibility.Movie_Data_Actors,
                                                          Enums.ScraperCapatibility.Movie_Data_Certifications,
                                                          Enums.ScraperCapatibility.Movie_Data_Collection,
                                                          Enums.ScraperCapatibility.Movie_Data_Countries,
                                                          Enums.ScraperCapatibility.Movie_Data_Credits,
                                                          Enums.ScraperCapatibility.Movie_Data_Directors,
                                                          Enums.ScraperCapatibility.Movie_Data_Genres,
                                                          Enums.ScraperCapatibility.Movie_Data_OriginalTitle,
                                                          Enums.ScraperCapatibility.Movie_Data_Plot,
                                                          Enums.ScraperCapatibility.Movie_Data_Ratings,
                                                          Enums.ScraperCapatibility.Movie_Data_Premiered,
                                                          Enums.ScraperCapatibility.Movie_Data_Runtime,
                                                          Enums.ScraperCapatibility.Movie_Data_Studios,
                                                          Enums.ScraperCapatibility.Movie_Data_Tagline,
                                                          Enums.ScraperCapatibility.Movie_Data_Title,
                                                          Enums.ScraperCapatibility.Movie_Image_Fanart,
                                                          Enums.ScraperCapatibility.Movie_Image_Poster,
                                                          Enums.ScraperCapatibility.Movie_Trailer,
                                                          Enums.ScraperCapatibility.Movieset_Data_Plot,
                                                          Enums.ScraperCapatibility.Movieset_Data_Title,
                                                          Enums.ScraperCapatibility.Movieset_Image_Fanart,
                                                          Enums.ScraperCapatibility.Movieset_Image_Poster,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Actors,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Aired,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Credits,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Directors,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_GuestStars,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Plot,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Rating,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Title,
                                                          Enums.ScraperCapatibility.TVEpisode_Image_Poster,
                                                          Enums.ScraperCapatibility.TVSeason_Data_Aired,
                                                          Enums.ScraperCapatibility.TVSeason_Data_Plot,
                                                          Enums.ScraperCapatibility.TVSeason_Data_Title,
                                                          Enums.ScraperCapatibility.TVSeason_Image_Poster,
                                                          Enums.ScraperCapatibility.TVShow_Data_Actors,
                                                          Enums.ScraperCapatibility.TVShow_Data_Certification,
                                                          Enums.ScraperCapatibility.TVShow_Data_Countries,
                                                          Enums.ScraperCapatibility.TVShow_Data_Creators,
                                                          Enums.ScraperCapatibility.TVShow_Data_Genres,
                                                          Enums.ScraperCapatibility.TVShow_Data_OriginalTitle,
                                                          Enums.ScraperCapatibility.TVShow_Data_Plot,
                                                          Enums.ScraperCapatibility.TVShow_Data_Premiered,
                                                          Enums.ScraperCapatibility.TVShow_Data_Rating,
                                                          Enums.ScraperCapatibility.TVShow_Data_Runtime,
                                                          Enums.ScraperCapatibility.TVShow_Data_Status,
                                                          Enums.ScraperCapatibility.TVShow_Data_Studios,
                                                          Enums.ScraperCapatibility.TVShow_Data_Title,
                                                          Enums.ScraperCapatibility.TVShow_Image_Fanart,
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
            Return True
        End Get
        Set(value As Boolean)
            Return
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
        Task.Run(Function() _Scraper.CreateAPI(_PrivateAPIKey))
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IAddon.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanels.Add(New Containers.SettingsPanel With {
            .Image = My.Resources._208x226_stacked_blue,
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
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
        _Logger.Trace("[TMDB] [Run] [Started]")
        Dim nAddonResult As New Interfaces.AddonResult

        Settings_Load()

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                _Scraper.Settings = _AddonSettings_Movie
            Case Enums.ContentType.Movieset
                _Scraper.Settings = _AddonSettings_MovieSet
            Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                _Scraper.Settings = _AddonSettings_TV
        End Select

        _Scraper.PreferredLanguage = dbElement.Language

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
                    nAddonResult = _Scraper.Scrape_Movie(dbElement.MainDetails.UniqueIDs.TMDbId.ToString, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                ElseIf dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    nAddonResult = _Scraper.Scrape_Movie(dbElement.MainDetails.UniqueIDs.IMDbId, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_Movieset
                If dbElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    nAddonResult = _Scraper.Scrape_Movieset(dbElement.MainDetails.UniqueIDs.TMDbId, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVEpisode
                Dim iShowID As Integer = -1
                If dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified Then
                    iShowID = dbElement.TVShowDetails.UniqueIDs.TMDbId
                ElseIf dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    iShowID = _Scraper.GetTMDbIDbyTVDbID(dbElement.TVShowDetails.UniqueIDs.TVDbId, Enums.ContentType.TVShow)
                End If
                If Not iShowID = -1 AndAlso dbElement.MainDetails.SeasonSpecified AndAlso dbElement.MainDetails.EpisodeSpecified Then
                    nAddonResult = _Scraper.Scrape_TVEpisode(iShowID, dbElement.MainDetails.Season, dbElement.MainDetails.Episode, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                ElseIf Not iShowID = -1 AndAlso dbElement.MainDetails.AiredSpecified Then
                    nAddonResult = _Scraper.Scrape_TVEpisode(iShowID, dbElement.MainDetails.Aired, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVSeason
                Dim iShowID As Integer = -1
                If dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified Then
                    iShowID = dbElement.TVShowDetails.UniqueIDs.TMDbId
                ElseIf dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified Then
                    iShowID = _Scraper.GetTMDbIDbyTVDbID(dbElement.TVShowDetails.UniqueIDs.TVDbId, Enums.ContentType.TVShow)
                End If
                If Not iShowID = -1 AndAlso dbElement.MainDetails.SeasonSpecified Then
                    nAddonResult = _Scraper.Scrape_TVSeason(iShowID, dbElement.MainDetails.Season, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVShow
                Dim iShowID As Integer = -1
                If dbElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    iShowID = dbElement.MainDetails.UniqueIDs.TMDbId
                ElseIf dbElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                    iShowID = _Scraper.GetTMDbIDbyTVDbID(dbElement.MainDetails.UniqueIDs.TVDbId, Enums.ContentType.TVShow)
                End If
                If Not iShowID = -1 Then
                    nAddonResult = _Scraper.ScrapeTVShow(iShowID, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            '
            'Searching
            '
            Case Enums.AddonEventType.Search_Movie
                If dbElement.MainDetails.TitleSpecified Then
                    nAddonResult.SearchResults = _Scraper.Search_Movie(dbElement.MainDetails.Title, dbElement.MainDetails.Premiered)
                End If
            Case Enums.AddonEventType.Search_Movieset
                If dbElement.MainDetails.TitleSpecified Then
                    nAddonResult.SearchResults = _Scraper.Search_MovieSet(dbElement.MainDetails.Title)
                End If
            Case Enums.AddonEventType.Search_TVShow
                If dbElement.MainDetails.TitleSpecified Then
                    nAddonResult.SearchResults = _Scraper.Search_TVShow(dbElement.MainDetails.Title)
                End If
        End Select

        _Logger.Trace("[TMDB] [Run] [Done]")
        Return nAddonResult
    End Function

#End Region 'Interface Methods

#Region "Methods"

    Public Sub Settings_Load()
        '
        'Global
        '
        _PrivateAPIKey = _XMLAddonSettings.GetStringSetting("ApiKey", String.Empty)
        '
        'Movie 
        '
        _AddonSettings_Movie.FallBackToEng = _XMLAddonSettings.GetBooleanSetting("FallBackToEn", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.GetAdultItems = _XMLAddonSettings.GetBooleanSetting("IncludeAdultItems", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchDeviant = _XMLAddonSettings.GetBooleanSetting("SearchDeviant", False, Enums.ContentType.Movie)
        '
        'MovieSet 
        '
        _AddonSettings_MovieSet.FallBackToEng = _XMLAddonSettings.GetBooleanSetting("FallBackToEn", False, Enums.ContentType.Movieset)
        _AddonSettings_MovieSet.GetAdultItems = _XMLAddonSettings.GetBooleanSetting("IncludeAdultItems", False, Enums.ContentType.Movieset)
        '
        'TV 
        '
        _AddonSettings_TV.FallBackToEng = _XMLAddonSettings.GetBooleanSetting("FallBackToEn", False, Enums.ContentType.TV)
        _AddonSettings_TV.GetAdultItems = _XMLAddonSettings.GetBooleanSetting("IncludeAdultItems", False, Enums.ContentType.TV)
    End Sub

    Public Sub Settings_Save()
        '
        'Global
        '
        _XMLAddonSettings.SetStringSetting("ApiKey", _PrivateAPIKey)
        '
        'Movie
        '
        _XMLAddonSettings.SetBooleanSetting("FallBackToEn", _AddonSettings_Movie.FallBackToEng, , Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("IncludeAdultItems", _AddonSettings_Movie.GetAdultItems, , Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchDeviant", _AddonSettings_Movie.SearchDeviant, , Enums.ContentType.Movie)
        '
        'MovieSet
        '
        _XMLAddonSettings.SetBooleanSetting("FallBackToEn", _AddonSettings_MovieSet.FallBackToEng, , Enums.ContentType.Movieset)
        _XMLAddonSettings.SetBooleanSetting("IncludeAdultItems", _AddonSettings_MovieSet.GetAdultItems, , Enums.ContentType.Movieset)
        '
        'TV
        '
        _XMLAddonSettings.SetBooleanSetting("FallBackToEn", _AddonSettings_TV.FallBackToEng, , Enums.ContentType.TV)
        _XMLAddonSettings.SetBooleanSetting("IncludeAdultItems", _AddonSettings_TV.GetAdultItems, , Enums.ContentType.TV)

        _XMLAddonSettings.Save()
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim FallBackToEng As Boolean
    Dim GetAdultItems As Boolean
    Dim SearchDeviant As Boolean

#End Region 'Fields

End Structure