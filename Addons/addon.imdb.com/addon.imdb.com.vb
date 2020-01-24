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

    Private _AddonSettings_Movie As New AddonSettings
    Private _AddonSettings_TV As New AddonSettings
    Private _PnlSettingsPanel_Movie_Data As frmSettingsPanel_Movie_Data
    Private _PnlSettingsPanel_Movie_Search As frmSettingsPanel_Movie_Search
    Private _PnlSettingsPanel_TV_Data As frmSettingsPanel_TV_Data
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Capabilities_AddonEventTypes As List(Of Enums.AddonEventType) Implements Interfaces.IAddon.Capabilities_AddonEventTypes
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {
                                                     Enums.AddonEventType.Scrape_Movie,
                                                     Enums.AddonEventType.Scrape_TVEpisode,
                                                     Enums.AddonEventType.Scrape_TVShow,
                                                     Enums.AddonEventType.Search_Movie,
                                                     Enums.AddonEventType.Search_TVShow
                                                     })
        End Get
    End Property

    Public ReadOnly Property Capabilities_ScraperCapatibilities As List(Of Enums.ScraperCapatibility) Implements Interfaces.IAddon.Capabilities_ScraperCapatibilities
        Get
            Return New List(Of Enums.ScraperCapatibility)(New Enums.ScraperCapatibility() {
                                                          Enums.ScraperCapatibility.Movie_Data_Actors,
                                                          Enums.ScraperCapatibility.Movie_Data_Certifications,
                                                          Enums.ScraperCapatibility.Movie_Data_Countries,
                                                          Enums.ScraperCapatibility.Movie_Data_Credits,
                                                          Enums.ScraperCapatibility.Movie_Data_Directors,
                                                          Enums.ScraperCapatibility.Movie_Data_Genres,
                                                          Enums.ScraperCapatibility.Movie_Data_MPAA,
                                                          Enums.ScraperCapatibility.Movie_Data_OriginalTitle,
                                                          Enums.ScraperCapatibility.Movie_Data_Plot,
                                                          Enums.ScraperCapatibility.Movie_Data_Ratings,
                                                          Enums.ScraperCapatibility.Movie_Data_Premiered,
                                                          Enums.ScraperCapatibility.Movie_Data_Runtime,
                                                          Enums.ScraperCapatibility.Movie_Data_Studios,
                                                          Enums.ScraperCapatibility.Movie_Data_Tagline,
                                                          Enums.ScraperCapatibility.Movie_Data_Title,
                                                          Enums.ScraperCapatibility.Movie_Data_Top250,
                                                          Enums.ScraperCapatibility.Movie_Trailer,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Actors,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Aired,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Credits,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Directors,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Plot,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Rating,
                                                          Enums.ScraperCapatibility.TVEpisode_Data_Title,
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
                                                          Enums.ScraperCapatibility.TVShow_Data_Studios,
                                                          Enums.ScraperCapatibility.TVShow_Data_Title
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
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IAddon.InjectSettingsPanel
        Settings_Load()
        '
        'Movie Data
        '
        _PnlSettingsPanel_Movie_Data = New frmSettingsPanel_Movie_Data
        _PnlSettingsPanel_Movie_Data.cbForceTitleLanguage.Text = _AddonSettings_Movie.ForceTitleLanguage
        _PnlSettingsPanel_Movie_Data.chkMPAADescription.Checked = _AddonSettings_Movie.MPAADescription
        _PnlSettingsPanel_Movie_Data.chkForceTitleLanguageFallback.Checked = _AddonSettings_Movie.ForceTitleLanguageFallback
        AddHandler _PnlSettingsPanel_Movie_Data.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel_Movie_Data.SettingsChanged, AddressOf Handle_SettingsChanged
        SettingsPanels.Add(New Containers.SettingsPanel With {
            .Panel = _PnlSettingsPanel_Movie_Data.pnlSettings,
            .Title = "imdb.com",
            .Type = Enums.SettingsPanelType.MovieData
        })
        '
        'Movie Search
        '
        _PnlSettingsPanel_Movie_Search = New frmSettingsPanel_Movie_Search
        _PnlSettingsPanel_Movie_Search.chkPartialTitles.Checked = _AddonSettings_Movie.SearchPartialTitles
        _PnlSettingsPanel_Movie_Search.chkPopularTitles.Checked = _AddonSettings_Movie.SearchPopularTitles
        _PnlSettingsPanel_Movie_Search.chkShortTitles.Checked = _AddonSettings_Movie.SearchShortTitles
        _PnlSettingsPanel_Movie_Search.chkTvTitles.Checked = _AddonSettings_Movie.SearchTvTitles
        _PnlSettingsPanel_Movie_Search.chkVideoTitles.Checked = _AddonSettings_Movie.SearchVideoTitles
        AddHandler _PnlSettingsPanel_Movie_Search.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel_Movie_Search.SettingsChanged, AddressOf Handle_SettingsChanged
        SettingsPanels.Add(New Containers.SettingsPanel With {
            .Panel = _PnlSettingsPanel_Movie_Search.pnlSettings,
            .Title = "imdb.com",
            .Type = Enums.SettingsPanelType.MovieSearch
        })
        '
        'TV Data
        '
        _PnlSettingsPanel_TV_Data = New frmSettingsPanel_TV_Data
        _PnlSettingsPanel_TV_Data.cbForceTitleLanguage.Text = _AddonSettings_TV.ForceTitleLanguage
        _PnlSettingsPanel_TV_Data.chkForceTitleLanguageFallback.Checked = _AddonSettings_TV.ForceTitleLanguageFallback
        AddHandler _PnlSettingsPanel_TV_Data.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel_TV_Data.SettingsChanged, AddressOf Handle_SettingsChanged
        SettingsPanels.Add(New Containers.SettingsPanel With {
            .Panel = _PnlSettingsPanel_TV_Data.pnlSettings,
            .Title = "imdb.com",
            .Type = Enums.SettingsPanelType.TVData
        })
    End Sub

    Public Sub SaveSetup(DoDispose As Boolean) Implements Interfaces.IAddon.SaveSetup
        '
        'Movie Data
        '
        _AddonSettings_Movie.ForceTitleLanguage = _PnlSettingsPanel_Movie_Data.cbForceTitleLanguage.Text
        _AddonSettings_Movie.ForceTitleLanguageFallback = _PnlSettingsPanel_Movie_Data.chkForceTitleLanguageFallback.Checked
        _AddonSettings_Movie.MPAADescription = _PnlSettingsPanel_Movie_Data.chkMPAADescription.Checked
        '
        'Movie Search
        '
        _AddonSettings_Movie.SearchPartialTitles = _PnlSettingsPanel_Movie_Search.chkPartialTitles.Checked
        _AddonSettings_Movie.SearchPopularTitles = _PnlSettingsPanel_Movie_Search.chkPopularTitles.Checked
        _AddonSettings_Movie.SearchShortTitles = _PnlSettingsPanel_Movie_Search.chkShortTitles.Checked
        _AddonSettings_Movie.SearchTvTitles = _PnlSettingsPanel_Movie_Search.chkTvTitles.Checked
        _AddonSettings_Movie.SearchVideoTitles = _PnlSettingsPanel_Movie_Search.chkVideoTitles.Checked
        '
        'TV
        '
        _AddonSettings_TV.ForceTitleLanguage = _PnlSettingsPanel_TV_Data.cbForceTitleLanguage.Text
        _AddonSettings_TV.ForceTitleLanguageFallback = _PnlSettingsPanel_TV_Data.chkForceTitleLanguageFallback.Checked
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel_Movie_Data.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel_Movie_Data.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel_Movie_Data.Dispose()
            RemoveHandler _PnlSettingsPanel_Movie_Search.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel_Movie_Search.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel_Movie_Search.Dispose()
            RemoveHandler _PnlSettingsPanel_TV_Data.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel_TV_Data.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel_TV_Data.Dispose()
        End If
    End Sub

    Public Function Run(ByRef dbElement As Database.DBElement, type As Enums.AddonEventType, lstCommandLineParams As List(Of Object)) As Interfaces.AddonResult Implements Interfaces.IAddon.Run
        _Logger.Trace("[IMDB] [Run] [Started]")
        Dim nAddonResult As New Interfaces.AddonResult

        Settings_Load()

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                _Scraper.Settings = _AddonSettings_Movie
            Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                _Scraper.Settings = _AddonSettings_TV
        End Select

        _Scraper.PreferredLanguage = dbElement.Language

        Select Case type
            '
            'PreCheck
            '
            Case Enums.AddonEventType.Scrape_Movie_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.IMDbIdSpecified
            Case Enums.AddonEventType.Scrape_TVEpisode_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.TVShowDetails.UniqueIDs.IMDbIdSpecified OrElse
                    (dbElement.TVShowDetails.UniqueIDs.IMDbIdSpecified AndAlso dbElement.MainDetails.SeasonSpecified AndAlso dbElement.MainDetails.EpisodeSpecified)
            Case Enums.AddonEventType.Scrape_TVShow_PreCheck
                nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.IMDbIdSpecified
            '
            'Scraping
            '
            Case Enums.AddonEventType.Scrape_Movie
                If dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    nAddonResult = _Scraper.GetInfo_Movie(dbElement.MainDetails.UniqueIDs.IMDbId.ToString, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVEpisode
                If dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    nAddonResult.ScraperResult_Data = _Scraper.GetInfo_TVEpisode(dbElement.MainDetails.UniqueIDs.IMDbId, dbElement.ScrapeOptions)
                ElseIf dbElement.TVShowDetails.UniqueIDs.IMDbIdSpecified AndAlso dbElement.MainDetails.SeasonSpecified AndAlso dbElement.MainDetails.EpisodeSpecified Then
                    nAddonResult.ScraperResult_Data = _Scraper.GetInfo_TVEpisode(dbElement.TVShowDetails.UniqueIDs.IMDbId, dbElement.MainDetails.Season, dbElement.MainDetails.Episode, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVShow
                If dbElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                    nAddonResult.ScraperResult_Data = _Scraper.GetInfo_TVShow(dbElement.MainDetails.UniqueIDs.IMDbId, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            '
            'Searching
            '
            Case Enums.AddonEventType.Search_Movie
                If dbElement.MainDetails.TitleSpecified Then
                    nAddonResult.SearchResults = _Scraper.Search_Movie(dbElement.MainDetails.Title, dbElement.MainDetails.Year)
                End If
            Case Enums.AddonEventType.Search_TVShow
                If dbElement.MainDetails.TitleSpecified Then
                    nAddonResult.SearchResults = _Scraper.Search_TVShow(dbElement.MainDetails.Title)
                End If
        End Select

        _Logger.Trace("[IMDB] [Run] [Started]")
        Return nAddonResult
    End Function

#End Region 'Interface Methods

#Region "Methods"

    Public Sub Settings_Load()
        '
        'Movie
        '
        _AddonSettings_Movie.MPAADescription = _XMLAddonSettings.GetBooleanSetting("MPAADescription", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.ForceTitleLanguage = _XMLAddonSettings.GetStringSetting("ForceTitleLanguage", String.Empty, Enums.ContentType.Movie)
        _AddonSettings_Movie.ForceTitleLanguageFallback = _XMLAddonSettings.GetBooleanSetting("ForceTitleLanguageFallback", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchPartialTitles = _XMLAddonSettings.GetBooleanSetting("SearchPartialTitles", True, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchPopularTitles = _XMLAddonSettings.GetBooleanSetting("SearchPopularTitles", True, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchTvTitles = _XMLAddonSettings.GetBooleanSetting("SearchTvTitles", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchVideoTitles = _XMLAddonSettings.GetBooleanSetting("SearchVideoTitles", False, Enums.ContentType.Movie)
        _AddonSettings_Movie.SearchShortTitles = _XMLAddonSettings.GetBooleanSetting("SearchShortTitles", False, Enums.ContentType.Movie)
        '
        'TV
        '
        _AddonSettings_TV.ForceTitleLanguage = _XMLAddonSettings.GetStringSetting("ForceTitleLanguage", String.Empty, Enums.ContentType.TV)
        _AddonSettings_TV.ForceTitleLanguageFallback = _XMLAddonSettings.GetBooleanSetting("ForceTitleLanguageFallback", False, Enums.ContentType.TV)
    End Sub

    Public Sub Settings_Save()
        '
        'Movie
        '
        _XMLAddonSettings.SetBooleanSetting("MPAADescription", _AddonSettings_Movie.MPAADescription,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("ForceTitleLanguageFallback", _AddonSettings_Movie.ForceTitleLanguageFallback,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("MPAADescription", _AddonSettings_Movie.MPAADescription,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchPartialTitles", _AddonSettings_Movie.SearchPartialTitles,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchPopularTitles", _AddonSettings_Movie.SearchPopularTitles,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchTvTitles", _AddonSettings_Movie.SearchTvTitles,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchVideoTitles", _AddonSettings_Movie.SearchVideoTitles,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetBooleanSetting("SearchShortTitles", _AddonSettings_Movie.SearchShortTitles,, Enums.ContentType.Movie)
        _XMLAddonSettings.SetStringSetting("ForceTitleLanguage", _AddonSettings_Movie.ForceTitleLanguage,, Enums.ContentType.Movie)
        '
        'TV
        '
        _XMLAddonSettings.SetBooleanSetting("ForceTitleLanguageFallback", _AddonSettings_TV.ForceTitleLanguageFallback,, Enums.ContentType.TV)
        _XMLAddonSettings.SetStringSetting("ForceTitleLanguage", _AddonSettings_TV.ForceTitleLanguage,, Enums.ContentType.TV)

        _XMLAddonSettings.Save()
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim ForceTitleLanguage As String
    Dim ForceTitleLanguageFallback As Boolean
    Dim MPAADescription As Boolean
    Dim SearchPartialTitles As Boolean
    Dim SearchPopularTitles As Boolean
    Dim SearchTvTitles As Boolean
    Dim SearchVideoTitles As Boolean
    Dim SearchShortTitles As Boolean
    Dim StudiosWithDistributors As Boolean

#End Region 'Fields

End Structure