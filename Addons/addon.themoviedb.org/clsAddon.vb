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

    Private _XmlAddonSettings As New AddonSettings(Enums.ContentType.Movie, "scraper.Data.TMDB")

    Private _PrivateApiKey As String = String.Empty
    Private _Scraper As New Scraper

    Private _InternalSettings_Movie As New InternalSettings
    Private _InternalSettings_Movieset As New InternalSettings
    Private _InternalSettings_TV As New InternalSettings

    Private _SettingsPanel_Addon As frmSettingsPanel

    Private _SettingsPanel_Data_Movie As frmSettingsPanel_Information_Movie
    Private _SettingsPanel_Data_Movieset As frmSettingsPanel_Information_Movieset
    Private _SettingsPanel_Data_TV As frmSettingsPanel_Information_TV

    Private _SettingsPanel_Image_Movie As frmSettingsPanel_Image_Movie
    Private _SettingsPanel_Image_Movieset As frmSettingsPanel_Image_MovieSet
    Private _SettingsPanel_Image_TV As frmSettingsPanel_Image_TV







    Public Shared _AssemblyName As String

    Public Shared _ScrapeModifier_Movie As New Structures.ScrapeModifiers
    Public Shared _ScrapeModifier_Movieset As New Structures.ScrapeModifiers
    Public Shared _ScrapeModifier_TV As New Structures.ScrapeModifiers

    Public Shared _ScrapeOptions_Movie As New Structures.ScrapeOptions
    Public Shared _ScrapeOptions_Movieset As New Structures.ScrapeOptions
    Public Shared _ScrapeOptions_TV As New Structures.ScrapeOptions

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
                                                          Enums.ScraperCapatibility.Movie_Data_Tags,
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
                                                          Enums.ScraperCapatibility.TVShow_Data_Tags,
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

    Public Property IsEnabled_Generic As Boolean Implements Interfaces.IAddon.IsEnabled_Generic

    Public Property IsEnabled_Data_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Data_Movie

    Public Property IsEnabled_Data_Movieset As Boolean Implements Interfaces.IAddon.IsEnabled_Data_Movieset

    Public Property IsEnabled_Data_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Data_TV

    Public Property IsEnabled_Image_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Image_Movie

    Public Property IsEnabled_Image_Movieset As Boolean Implements Interfaces.IAddon.IsEnabled_Image_Movieset

    Public Property IsEnabled_Image_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Image_TV

    Public Property IsEnabled_Theme_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Theme_Movie

    Public Property IsEnabled_Theme_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Theme_TV

    Public Property IsEnabled_Trailer_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Trailer_Movie

    Public Property SettingsPanels As New List(Of Interfaces.ISettingsPanel) Implements Interfaces.IAddon.SettingsPanels

#End Region 'Properties

    '#Region "Properties"

    '    ReadOnly Property Name() As String Implements _
    '        Interfaces.IAddon_Data_Scraper_Movie.Name,
    '        Interfaces.IAddon_Data_Scraper_Movieset.Name,
    '        Interfaces.IAddon_Data_Scraper_TV.Name,
    '        Interfaces.IAddon_Image_Scraper_Movie.ModuleName,
    '        Interfaces.IAddon_Image_Scraper_Movieset.ModuleName,
    '        Interfaces.IAddon_Image_Scraper_TV.ModuleName
    '        Get
    '            Return _AssemblyName
    '        End Get
    '    End Property

    '    ReadOnly Property Version() As String Implements _
    '        Interfaces.IAddon_Data_Scraper_Movie.Version,
    '        Interfaces.IAddon_Data_Scraper_Movieset.Version,
    '        Interfaces.IAddon_Data_Scraper_TV.Version,
    '        Interfaces.IAddon_Image_Scraper_Movie.ModuleVersion,
    '        Interfaces.IAddon_Image_Scraper_Movieset.ModuleVersion,
    '        Interfaces.IAddon_Image_Scraper_TV.ModuleVersion
    '        Get
    '            Return Diagnostics.FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
    '        End Get
    '    End Property

    '    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.IAddon_Data_Scraper_Movie.Enabled
    '        Get
    '            Return _Enabled_Data_Movie
    '        End Get
    '        Set(ByVal value As Boolean)
    '            _Enabled_Data_Movie = value
    '            If _Enabled_Data_Movie Then
    '                Task.Run(Function() _Scraper.CreateAPI(_SpecialSettings_Movie))
    '            End If
    '        End Set
    '    End Property

    '    Property ScraperEnabled_Movieset() As Boolean Implements Interfaces.IAddon_Data_Scraper_Movieset.ScraperEnabled
    '        Get
    '            Return _Enabled_Data_Movieset
    '        End Get
    '        Set(ByVal value As Boolean)
    '            _Enabled_Data_Movieset = value
    '            If _Enabled_Data_Movieset Then
    '                Task.Run(Function() _TMDbApi_Movieset.CreateAPI(_SpecialSettings_Movieset))
    '            End If
    '        End Set
    '    End Property

    '    Property ScraperEnabled_TV() As Boolean Implements Interfaces.IAddon_Data_Scraper_TV.ScraperEnabled
    '        Get
    '            Return _Enabled_Data_TV
    '        End Get
    '        Set(ByVal value As Boolean)
    '            _Enabled_Data_TV = value
    '            If _Enabled_Data_TV Then
    '                Task.Run(Function() _TMDbApi_TV.CreateAPI(_SpecialSettings_TV))
    '            End If
    '        End Set
    '    End Property

    '#End Region 'Properties

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IAddon.Init
        Settings_Load()
        Task.Run(Function() _Scraper.CreateAPI(_PrivateApiKey))
    End Sub

    Public Sub InjectSettingsPanels() Implements Interfaces.IAddon.InjectSettingsPanels
        Settings_Load()

        'Addon global
        _SettingsPanel_Addon = New frmSettingsPanel
        _SettingsPanel_Addon.IsEnabled = True
        _SettingsPanel_Addon.txtApiKey.Text = _PrivateApiKey
        SettingsPanels.Add(_SettingsPanel_Addon)

        'Data Movie
        _SettingsPanel_Data_Movie = New frmSettingsPanel_Information_Movie
        _SettingsPanel_Data_Movie.chkEnabled.Checked = IsEnabled_Data_Movie
        _SettingsPanel_Data_Movie.chkActors.Checked = _ScrapeOptions_Movie.Actors
        _SettingsPanel_Data_Movie.chkCollectionID.Checked = _ScrapeOptions_Movie.Collection
        _SettingsPanel_Data_Movie.chkCountries.Checked = _ScrapeOptions_Movie.Countries
        _SettingsPanel_Data_Movie.chkCredits.Checked = _ScrapeOptions_Movie.Credits
        _SettingsPanel_Data_Movie.chkDirectors.Checked = _ScrapeOptions_Movie.Directors
        _SettingsPanel_Data_Movie.chkFallBackEng.Checked = _InternalSettings_Movie.FallBackToEng
        _SettingsPanel_Data_Movie.chkGenres.Checked = _ScrapeOptions_Movie.Genres
        _SettingsPanel_Data_Movie.chkGetAdultItems.Checked = _InternalSettings_Movie.GetAdultItems
        _SettingsPanel_Data_Movie.chkCertifications.Checked = _ScrapeOptions_Movie.MPAA
        _SettingsPanel_Data_Movie.chkOriginalTitle.Checked = _ScrapeOptions_Movie.OriginalTitle
        _SettingsPanel_Data_Movie.chkPlot.Checked = _ScrapeOptions_Movie.Plot
        _SettingsPanel_Data_Movie.chkPremiered.Checked = _ScrapeOptions_Movie.Premiered
        _SettingsPanel_Data_Movie.chkRating.Checked = _ScrapeOptions_Movie.Ratings
        _SettingsPanel_Data_Movie.chkRuntime.Checked = _ScrapeOptions_Movie.Runtime
        _SettingsPanel_Data_Movie.chkSearchDeviant.Checked = _InternalSettings_Movie.SearchDeviant
        _SettingsPanel_Data_Movie.chkStudios.Checked = _ScrapeOptions_Movie.Studios
        _SettingsPanel_Data_Movie.chkTagline.Checked = _ScrapeOptions_Movie.Tagline
        _SettingsPanel_Data_Movie.chkTitle.Checked = _ScrapeOptions_Movie.Title
        _SettingsPanel_Data_Movie.chkTrailer.Checked = _ScrapeOptions_Movie.TrailerLink
        SettingsPanels.Add(_SettingsPanel_Data_Movie)

        'Data Movieset
        _SettingsPanel_Data_Movieset = New frmSettingsPanel_Information_Movieset
        _SettingsPanel_Data_Movieset.chkEnabled.Checked = IsEnabled_Data_Movieset
        _SettingsPanel_Data_Movieset.chkFallBackEng.Checked = _InternalSettings_Movieset.FallBackToEng
        _SettingsPanel_Data_Movieset.chkGetAdultItems.Checked = _InternalSettings_Movieset.GetAdultItems
        _SettingsPanel_Data_Movieset.chkPlot.Checked = _ScrapeOptions_Movieset.Plot
        _SettingsPanel_Data_Movieset.chkTitle.Checked = _ScrapeOptions_Movieset.Title
        SettingsPanels.Add(_SettingsPanel_Data_Movieset)

        'Data TV
        _SettingsPanel_Data_TV = New frmSettingsPanel_Information_TV
        _SettingsPanel_Data_TV.chkEnabled.Checked = IsEnabled_Data_TV
        _SettingsPanel_Data_TV.chkFallBackEng.Checked = _InternalSettings_TV.FallBackToEng
        _SettingsPanel_Data_TV.chkGetAdultItems.Checked = _InternalSettings_TV.GetAdultItems
        _SettingsPanel_Data_TV.chkScraperEpisodeActors.Checked = _ScrapeOptions_TV.Episodes.Actors
        _SettingsPanel_Data_TV.chkScraperEpisodeAired.Checked = _ScrapeOptions_TV.Episodes.Aired
        _SettingsPanel_Data_TV.chkScraperEpisodeCredits.Checked = _ScrapeOptions_TV.Episodes.Credits
        _SettingsPanel_Data_TV.chkScraperEpisodeDirectors.Checked = _ScrapeOptions_TV.Episodes.Directors
        _SettingsPanel_Data_TV.chkScraperEpisodeGuestStars.Checked = _ScrapeOptions_TV.Episodes.GuestStars
        _SettingsPanel_Data_TV.chkScraperEpisodePlot.Checked = _ScrapeOptions_TV.Episodes.Plot
        _SettingsPanel_Data_TV.chkScraperEpisodeRating.Checked = _ScrapeOptions_TV.Episodes.Ratings
        _SettingsPanel_Data_TV.chkScraperEpisodeTitle.Checked = _ScrapeOptions_TV.Episodes.Title
        _SettingsPanel_Data_TV.chkScraperSeasonAired.Checked = _ScrapeOptions_TV.Seasons.Aired
        _SettingsPanel_Data_TV.chkScraperSeasonPlot.Checked = _ScrapeOptions_TV.Seasons.Plot
        _SettingsPanel_Data_TV.chkScraperSeasonTitle.Checked = _ScrapeOptions_TV.Seasons.Title
        _SettingsPanel_Data_TV.chkScraperShowActors.Checked = _ScrapeOptions_TV.Actors
        _SettingsPanel_Data_TV.chkScraperShowCertifications.Checked = _ScrapeOptions_TV.Certifications
        _SettingsPanel_Data_TV.chkScraperShowCountries.Checked = _ScrapeOptions_TV.Countries
        _SettingsPanel_Data_TV.chkScraperShowCreators.Checked = _ScrapeOptions_TV.Creators
        _SettingsPanel_Data_TV.chkScraperShowGenres.Checked = _ScrapeOptions_TV.Genres
        _SettingsPanel_Data_TV.chkScraperShowOriginalTitle.Checked = _ScrapeOptions_TV.OriginalTitle
        _SettingsPanel_Data_TV.chkScraperShowPlot.Checked = _ScrapeOptions_TV.Plot
        _SettingsPanel_Data_TV.chkScraperShowPremiered.Checked = _ScrapeOptions_TV.Premiered
        _SettingsPanel_Data_TV.chkScraperShowRating.Checked = _ScrapeOptions_TV.Ratings
        _SettingsPanel_Data_TV.chkScraperShowRuntime.Checked = _ScrapeOptions_TV.Runtime
        _SettingsPanel_Data_TV.chkScraperShowStatus.Checked = _ScrapeOptions_TV.Status
        _SettingsPanel_Data_TV.chkScraperShowStudios.Checked = _ScrapeOptions_TV.Studios
        _SettingsPanel_Data_TV.chkScraperShowTagline.Checked = _ScrapeOptions_TV.Tagline
        _SettingsPanel_Data_TV.chkScraperShowTitle.Checked = _ScrapeOptions_TV.Title
        SettingsPanels.Add(_SettingsPanel_Data_TV)

        'Image Movie
        _SettingsPanel_Image_Movie = New frmSettingsPanel_Image_Movie
        _SettingsPanel_Image_Movie.chkEnabled.Checked = IsEnabled_Image_Movie
        _SettingsPanel_Image_Movie.chkMainFanart.Checked = _ScrapeModifier_Movie.Fanart
        _SettingsPanel_Image_Movie.chkMainPoster.Checked = _ScrapeModifier_Movie.Poster
        SettingsPanels.Add(_SettingsPanel_Image_Movie)

        'Image Movieset
        _SettingsPanel_Image_Movieset = New frmSettingsPanel_Image_Movieset
        _SettingsPanel_Image_Movieset.chkEnabled.Checked = IsEnabled_Image_Movieset
        _SettingsPanel_Image_Movieset.chkMainFanart.Checked = _ScrapeModifier_Movieset.Fanart
        _SettingsPanel_Image_Movieset.chkMainPoster.Checked = _ScrapeModifier_Movieset.Poster
        SettingsPanels.Add(_SettingsPanel_Image_Movieset)

        'Image TV
        _SettingsPanel_Image_TV = New frmSettingsPanel_Image_TV
        _SettingsPanel_Image_TV.chkEnabled.Checked = IsEnabled_Image_TV
        _SettingsPanel_Image_TV.chkScrapeEpisodePoster.Checked = _ScrapeModifier_TV.Episodes.Poster
        _SettingsPanel_Image_TV.chkScrapeSeasonPoster.Checked = _ScrapeModifier_TV.Seasons.Poster
        _SettingsPanel_Image_TV.chkScrapeShowFanart.Checked = _ScrapeModifier_TV.Fanart
        _SettingsPanel_Image_TV.chkScrapeShowPoster.Checked = _ScrapeModifier_TV.Poster
        SettingsPanels.Add(_SettingsPanel_Image_TV)
    End Sub

    Public Sub SaveSettings(ByVal doDispose As Boolean) Implements Interfaces.IAddon.SaveSettings
        'Global
        _PrivateApiKey = _SettingsPanel_Addon.txtApiKey.Text.Trim

        'Data Movie
        IsEnabled_Data_Movie = _SettingsPanel_Data_Movie.IsEnabled
        _ScrapeOptions_Movie.Actors = _SettingsPanel_Data_Movie.chkActors.Checked
        _ScrapeOptions_Movie.Certifications = _SettingsPanel_Data_Movie.chkCertifications.Checked
        _ScrapeOptions_Movie.Collection = _SettingsPanel_Data_Movie.chkCollectionID.Checked
        _ScrapeOptions_Movie.Countries = _SettingsPanel_Data_Movie.chkCountries.Checked
        _ScrapeOptions_Movie.Credits = _SettingsPanel_Data_Movie.chkCredits.Checked
        _ScrapeOptions_Movie.Directors = _SettingsPanel_Data_Movie.chkDirectors.Checked
        _ScrapeOptions_Movie.Genres = _SettingsPanel_Data_Movie.chkGenres.Checked
        _ScrapeOptions_Movie.MPAA = _SettingsPanel_Data_Movie.chkCertifications.Checked
        _ScrapeOptions_Movie.OriginalTitle = _SettingsPanel_Data_Movie.chkOriginalTitle.Checked
        _ScrapeOptions_Movie.Outline = _SettingsPanel_Data_Movie.chkPlot.Checked
        _ScrapeOptions_Movie.Plot = _SettingsPanel_Data_Movie.chkPlot.Checked
        _ScrapeOptions_Movie.Premiered = _SettingsPanel_Data_Movie.chkPremiered.Checked
        _ScrapeOptions_Movie.Ratings = _SettingsPanel_Data_Movie.chkRating.Checked
        _ScrapeOptions_Movie.Runtime = _SettingsPanel_Data_Movie.chkRuntime.Checked
        _ScrapeOptions_Movie.Studios = _SettingsPanel_Data_Movie.chkStudios.Checked
        _ScrapeOptions_Movie.Tagline = _SettingsPanel_Data_Movie.chkTagline.Checked
        _ScrapeOptions_Movie.Title = _SettingsPanel_Data_Movie.chkTitle.Checked
        _ScrapeOptions_Movie.Top250 = False
        _ScrapeOptions_Movie.TrailerLink = _SettingsPanel_Data_Movie.chkTrailer.Checked
        _InternalSettings_Movie.FallBackToEng = _SettingsPanel_Data_Movie.chkFallBackEng.Checked
        _InternalSettings_Movie.GetAdultItems = _SettingsPanel_Data_Movie.chkGetAdultItems.Checked
        _InternalSettings_Movie.SearchDeviant = _SettingsPanel_Data_Movie.chkSearchDeviant.Checked

        'Data Movieset
        IsEnabled_Data_Movieset = _SettingsPanel_Data_Movieset.IsEnabled
        _ScrapeOptions_Movieset.Plot = _SettingsPanel_Data_Movieset.chkPlot.Checked
        _ScrapeOptions_Movieset.Title = _SettingsPanel_Data_Movieset.chkTitle.Checked
        _InternalSettings_Movieset.FallBackToEng = _SettingsPanel_Data_Movieset.chkFallBackEng.Checked
        _InternalSettings_Movieset.GetAdultItems = _SettingsPanel_Data_Movieset.chkGetAdultItems.Checked

        'Data TV
        IsEnabled_Data_TV = _SettingsPanel_Data_TV.IsEnabled
        _ScrapeOptions_TV.Episodes.Actors = _SettingsPanel_Data_TV.chkScraperEpisodeActors.Checked
        _ScrapeOptions_TV.Episodes.Aired = _SettingsPanel_Data_TV.chkScraperEpisodeAired.Checked
        _ScrapeOptions_TV.Episodes.Credits = _SettingsPanel_Data_TV.chkScraperEpisodeCredits.Checked
        _ScrapeOptions_TV.Episodes.Directors = _SettingsPanel_Data_TV.chkScraperEpisodeDirectors.Checked
        _ScrapeOptions_TV.Episodes.GuestStars = _SettingsPanel_Data_TV.chkScraperEpisodeGuestStars.Checked
        _ScrapeOptions_TV.Episodes.Plot = _SettingsPanel_Data_TV.chkScraperEpisodePlot.Checked
        _ScrapeOptions_TV.Episodes.Ratings = _SettingsPanel_Data_TV.chkScraperEpisodeRating.Checked
        _ScrapeOptions_TV.Episodes.Title = _SettingsPanel_Data_TV.chkScraperEpisodeTitle.Checked
        _ScrapeOptions_TV.Actors = _SettingsPanel_Data_TV.chkScraperShowActors.Checked
        _ScrapeOptions_TV.Certifications = _SettingsPanel_Data_TV.chkScraperShowCertifications.Checked
        _ScrapeOptions_TV.Creators = _SettingsPanel_Data_TV.chkScraperShowCreators.Checked
        _ScrapeOptions_TV.Countries = _SettingsPanel_Data_TV.chkScraperShowCountries.Checked
        _ScrapeOptions_TV.Genres = _SettingsPanel_Data_TV.chkScraperShowGenres.Checked
        _ScrapeOptions_TV.OriginalTitle = _SettingsPanel_Data_TV.chkScraperShowOriginalTitle.Checked
        _ScrapeOptions_TV.Plot = _SettingsPanel_Data_TV.chkScraperShowPlot.Checked
        _ScrapeOptions_TV.Premiered = _SettingsPanel_Data_TV.chkScraperShowPremiered.Checked
        _ScrapeOptions_TV.Ratings = _SettingsPanel_Data_TV.chkScraperShowRating.Checked
        _ScrapeOptions_TV.Runtime = _SettingsPanel_Data_TV.chkScraperShowRuntime.Checked
        _ScrapeOptions_TV.Status = _SettingsPanel_Data_TV.chkScraperShowStatus.Checked
        _ScrapeOptions_TV.Studios = _SettingsPanel_Data_TV.chkScraperShowStudios.Checked
        _ScrapeOptions_TV.Tagline = _SettingsPanel_Data_TV.chkScraperShowTagline.Checked
        _ScrapeOptions_TV.Title = _SettingsPanel_Data_TV.chkScraperShowTitle.Checked
        _ScrapeOptions_TV.Seasons.Aired = _SettingsPanel_Data_TV.chkScraperSeasonAired.Checked
        _ScrapeOptions_TV.Seasons.Plot = _SettingsPanel_Data_TV.chkScraperSeasonPlot.Checked
        _ScrapeOptions_TV.Seasons.Title = _SettingsPanel_Data_TV.chkScraperSeasonTitle.Checked
        _InternalSettings_TV.FallBackToEng = _SettingsPanel_Data_TV.chkFallBackEng.Checked
        _InternalSettings_TV.GetAdultItems = _SettingsPanel_Data_TV.chkGetAdultItems.Checked

        'Image Movie
        IsEnabled_Image_Movie = _SettingsPanel_Image_Movie.IsEnabled
        _ScrapeModifier_Movie.Poster = _SettingsPanel_Image_Movie.chkMainPoster.Checked
        _ScrapeModifier_Movie.Fanart = _SettingsPanel_Image_Movie.chkMainFanart.Checked
        _ScrapeModifier_Movie.Extrafanarts = _ScrapeModifier_Movie.Fanart
        _ScrapeModifier_Movie.Extrathumbs = _ScrapeModifier_Movie.Fanart
        _ScrapeModifier_Movie.Keyart = _ScrapeModifier_Movie.Poster

        'Image Movieset
        IsEnabled_Image_Movieset = _SettingsPanel_Image_Movieset.IsEnabled
        _ScrapeModifier_Movieset.Poster = _SettingsPanel_Image_Movieset.chkMainPoster.Checked
        _ScrapeModifier_Movieset.Fanart = _SettingsPanel_Image_Movieset.chkMainFanart.Checked
        _ScrapeModifier_Movieset.Extrafanarts = _ScrapeModifier_Movieset.Fanart
        _ScrapeModifier_Movieset.Keyart = _ScrapeModifier_Movieset.Poster

        'Image TV
        IsEnabled_Image_TV = _SettingsPanel_Image_TV.IsEnabled
        _ScrapeModifier_TV.Episodes.Poster = _SettingsPanel_Image_TV.chkScrapeEpisodePoster.Checked
        _ScrapeModifier_TV.Seasons.Poster = _SettingsPanel_Image_TV.chkScrapeSeasonPoster.Checked
        _ScrapeModifier_TV.Fanart = _SettingsPanel_Image_TV.chkScrapeShowFanart.Checked
        _ScrapeModifier_TV.Poster = _SettingsPanel_Image_TV.chkScrapeShowPoster.Checked
        _ScrapeModifier_TV.Extrafanarts = _ScrapeModifier_TV.Fanart
        _ScrapeModifier_TV.Keyart = _ScrapeModifier_TV.Poster

        Settings_Save()
        If doDispose Then
            _SettingsPanel_Addon.DoDispose()
            _SettingsPanel_Data_Movie.Dispose()
            _SettingsPanel_Data_Movieset.Dispose()
            _SettingsPanel_Data_TV.Dispose()
            _SettingsPanel_Image_Movie.Dispose()
            _SettingsPanel_Image_Movieset.Dispose()
            _SettingsPanel_Image_TV.Dispose()
        End If
    End Sub

    Public Function Run(ByRef dbElement As Database.DBElement, type As Enums.AddonEventType, lstCommandLineParams As List(Of Object)) As Interfaces.AddonResult Implements Interfaces.IAddon.Run
        _Logger.Trace("[TMDB] [Run] [Started]")
        Dim nAddonResult As New Interfaces.AddonResult

        Settings_Load()

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                _Scraper.ScraperSettings = _InternalSettings_Movie
            Case Enums.ContentType.MovieSet
                _Scraper.ScraperSettings = _InternalSettings_Movieset
            Case Enums.ContentType.TV, Enums.ContentType.TVEpisode, Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                _Scraper.ScraperSettings = _InternalSettings_TV
        End Select

        _Scraper.PreferredLanguage = dbElement.Language

        Select Case type
            '
            'PreCheck
            '
            'Case Enums.AddonEventType.Scrape_Movie_PreCheck
            '    nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.IMDbIdSpecified OrElse dbElement.MainDetails.UniqueIDs.TMDbIdSpecified
            'Case Enums.AddonEventType.Scrape_Movieset_PreCheck
            '    nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.TMDbIdSpecified
            'Case Enums.AddonEventType.Scrape_TVEpisode_PreCheck
            '    nAddonResult.bPreCheckSuccessful = (dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified) AndAlso
            '        dbElement.MainDetails.SeasonSpecified AndAlso dbElement.MainDetails.EpisodeSpecified
            'Case Enums.AddonEventType.Scrape_TVSeason_PreCheck
            '    nAddonResult.bPreCheckSuccessful = dbElement.TVShowDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.TVShowDetails.UniqueIDs.TVDbIdSpecified AndAlso
            '        dbElement.MainDetails.SeasonSpecified
            'Case Enums.AddonEventType.Scrape_TVShow_PreCheck
            '    nAddonResult.bPreCheckSuccessful = dbElement.MainDetails.UniqueIDs.TMDbIdSpecified OrElse dbElement.MainDetails.UniqueIDs.TVDbIdSpecified
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
                    iShowID = _Scraper.GetTMDbByTVDb(dbElement.TVShowDetails.UniqueIDs.TVDbId)
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
                    iShowID = _Scraper.GetTMDbByTVDb(dbElement.TVShowDetails.UniqueIDs.TVDbId)
                End If
                If Not iShowID = -1 AndAlso dbElement.MainDetails.SeasonSpecified Then
                    nAddonResult = _Scraper.Scrape_TVSeason(iShowID, dbElement.MainDetails.Season, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
            Case Enums.AddonEventType.Scrape_TVShow
                Dim iShowID As Integer = -1
                If dbElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    iShowID = dbElement.MainDetails.UniqueIDs.TMDbId
                ElseIf dbElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                    iShowID = _Scraper.GetTMDbByTVDb(dbElement.MainDetails.UniqueIDs.TVDbId)
                End If
                If Not iShowID = -1 Then
                    nAddonResult = _Scraper.ScrapeTVShow(iShowID, dbElement.ScrapeModifiers, dbElement.ScrapeOptions)
                End If
                '
                'Searching
                '
                'Case Enums.AddonEventType.Search_Movie
                '    If dbElement.MainDetails.TitleSpecified Then
                '        nAddonResult.SearchResults = _Scraper.Search_By_Title_Movie(dbElement.MainDetails.Title, dbElement.MainDetails.Year)
                '    End If
                'Case Enums.AddonEventType.Search_Movieset
                '    If dbElement.MainDetails.TitleSpecified Then
                '        nAddonResult.SearchResults = _Scraper.Search_By_Title_Movieset(dbElement.MainDetails.Title)
                '    End If
                'Case Enums.AddonEventType.Search_TVShow
                '    If dbElement.MainDetails.TitleSpecified Then
                '        nAddonResult.SearchResults = _Scraper.Search_By_Title_TVShow(dbElement.MainDetails.Title)
                '    End If
        End Select

        _Logger.Trace("[TMDB] [Run] [Done]")
        Return nAddonResult
    End Function

#End Region 'Interface Methods

#Region "Methods"

    Public Sub Settings_Load()
        With _XmlAddonSettings

            'Global
            _PrivateApiKey = .GetStringSetting("ApiKey", String.Empty)

            'Data Movie
            _ScrapeOptions_Movie.Actors = .GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Certifications = .GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Collection = .GetBooleanSetting("DoCollectionID", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Countries = .GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Credits = .GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Directors = .GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Genres = .GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.MPAA = .GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.OriginalTitle = .GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Outline = .GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Plot = .GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Premiered = .GetBooleanSetting("DoPremiered", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Ratings = .GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Runtime = .GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Studios = .GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Tagline = .GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Title = .GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.Top250 = .GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
            _ScrapeOptions_Movie.TrailerLink = .GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
            _InternalSettings_Movie.FallBackToEng = .GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
            _InternalSettings_Movie.GetAdultItems = .GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.Movie)
            _InternalSettings_Movie.SearchDeviant = .GetBooleanSetting("SearchDeviant", False, , Enums.ContentType.Movie)

            'Data Movieset
            _ScrapeOptions_Movieset.Plot = .GetBooleanSetting("DoPlot", True, , Enums.ContentType.MovieSet)
            _ScrapeOptions_Movieset.Title = .GetBooleanSetting("DoTitle", True, , Enums.ContentType.MovieSet)
            _InternalSettings_Movieset.FallBackToEng = .GetBooleanSetting("FallBackEn", False, , Enums.ContentType.MovieSet)
            _InternalSettings_Movieset.GetAdultItems = .GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.MovieSet)

            'Data TV
            _ScrapeOptions_TV.Episodes.Actors = .GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Aired = .GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Credits = .GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Directors = .GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.GuestStars = .GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Plot = .GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Ratings = .GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Episodes.Title = .GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
            _ScrapeOptions_TV.Seasons.Aired = .GetBooleanSetting("DoAired", True, , Enums.ContentType.TVSeason)
            _ScrapeOptions_TV.Seasons.Plot = .GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVSeason)
            _ScrapeOptions_TV.Seasons.Title = .GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVSeason)
            _ScrapeOptions_TV.Actors = .GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Certifications = .GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Countries = .GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Creators = .GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.EpisodeGuideURL = .GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Genres = .GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.OriginalTitle = .GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Plot = .GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Premiered = .GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Ratings = .GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Runtime = .GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Status = .GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Studios = .GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Tagline = .GetBooleanSetting("DoTagline", True, , Enums.ContentType.TVShow)
            _ScrapeOptions_TV.Title = .GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)
            _InternalSettings_TV.FallBackToEng = .GetBooleanSetting("FallBackEn", False, , Enums.ContentType.TV)
            _InternalSettings_TV.GetAdultItems = .GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.TV)

            'Image Movie
            _ScrapeModifier_Movie.Poster = .GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
            _ScrapeModifier_Movie.Fanart = .GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)
            _ScrapeModifier_Movie.Extrafanarts = _ScrapeModifier_Movie.Fanart
            _ScrapeModifier_Movie.Extrathumbs = _ScrapeModifier_Movie.Fanart
            _ScrapeModifier_Movie.Keyart = _ScrapeModifier_Movie.Poster

            'Image Movieset
            _ScrapeModifier_Movieset.Poster = .GetBooleanSetting("DoPoster", True, , Enums.ContentType.MovieSet)
            _ScrapeModifier_Movieset.Fanart = .GetBooleanSetting("DoFanart", True, , Enums.ContentType.MovieSet)
            _ScrapeModifier_Movieset.Extrafanarts = _ScrapeModifier_Movieset.Fanart
            _ScrapeModifier_Movieset.Keyart = _ScrapeModifier_Movieset.Poster

            'Image TV
            _ScrapeModifier_TV.Episodes.Poster = .GetBooleanSetting("DoEpisodePoster", True, , Enums.ContentType.TV)
            _ScrapeModifier_TV.Seasons.Poster = .GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
            _ScrapeModifier_TV.Fanart = .GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
            _ScrapeModifier_TV.Poster = .GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)
            _ScrapeModifier_TV.Extrafanarts = _ScrapeModifier_TV.Fanart
            _ScrapeModifier_TV.Keyart = _ScrapeModifier_TV.Poster
        End With
    End Sub

    Public Sub Settings_Save()
        With _XmlAddonSettings

            'Global
            .SetStringSetting("ApiKey", _PrivateApiKey)

            'Data Movie
            .SetBooleanSetting("DoCast", _ScrapeOptions_Movie.Actors, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoCert", _ScrapeOptions_Movie.Certifications, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoCollectionID", _ScrapeOptions_Movie.Collection, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoCountry", _ScrapeOptions_Movie.Countries, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoWriters", _ScrapeOptions_Movie.Credits, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoDirector", _ScrapeOptions_Movie.Directors, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoFanart", _ScrapeModifier_Movie.Fanart,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoGenres", _ScrapeOptions_Movie.Genres,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoMPAA", _ScrapeOptions_Movie.MPAA, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoOriginalTitle", _ScrapeOptions_Movie.OriginalTitle, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoOutline", _ScrapeOptions_Movie.Outline,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoPlot", _ScrapeOptions_Movie.Plot,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoPoster", _ScrapeModifier_Movie.Poster,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoPremiered", _ScrapeOptions_Movie.Premiered,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoRating", _ScrapeOptions_Movie.Ratings,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoRuntime", _ScrapeOptions_Movie.Runtime, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoStudio", _ScrapeOptions_Movie.Studios,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoTagline", _ScrapeOptions_Movie.Tagline,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoTitle", _ScrapeOptions_Movie.Title,  , Enums.ContentType.Movie)
            .SetBooleanSetting("DoTop250", _ScrapeOptions_Movie.Top250, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoTrailer", _ScrapeOptions_Movie.TrailerLink, , Enums.ContentType.Movie)
            .SetBooleanSetting("FallBackEn", _InternalSettings_Movie.FallBackToEng, , Enums.ContentType.Movie)
            .SetBooleanSetting("GetAdultItems", _InternalSettings_Movie.GetAdultItems, , Enums.ContentType.Movie)
            .SetBooleanSetting("SearchDeviant", _InternalSettings_Movie.SearchDeviant, , Enums.ContentType.Movie)

            'Data Movieset 
            .SetBooleanSetting("DoPlot", _ScrapeOptions_Movieset.Plot, , Enums.ContentType.MovieSet)
            .SetBooleanSetting("DoTitle", _ScrapeOptions_Movieset.Title, , Enums.ContentType.MovieSet)
            .SetBooleanSetting("GetAdultItems", _InternalSettings_Movieset.GetAdultItems, , Enums.ContentType.MovieSet)

            'Data TV 
            .SetBooleanSetting("DoActors", _ScrapeOptions_TV.Episodes.Actors, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoAired", _ScrapeOptions_TV.Episodes.Aired, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoCredits", _ScrapeOptions_TV.Episodes.Credits, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoDirector", _ScrapeOptions_TV.Episodes.Directors, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoGuestStars", _ScrapeOptions_TV.Episodes.GuestStars, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoPlot", _ScrapeOptions_TV.Episodes.Plot, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoRating", _ScrapeOptions_TV.Episodes.Ratings, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoTitle", _ScrapeOptions_TV.Episodes.Title, , Enums.ContentType.TVEpisode)
            .SetBooleanSetting("DoAired", _ScrapeOptions_TV.Seasons.Aired, , Enums.ContentType.TVSeason)
            .SetBooleanSetting("DoPlot", _ScrapeOptions_TV.Seasons.Plot, , Enums.ContentType.TVSeason)
            .SetBooleanSetting("DoTitle", _ScrapeOptions_TV.Seasons.Title, , Enums.ContentType.TVSeason)
            .SetBooleanSetting("DoActors", _ScrapeOptions_TV.Actors, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoCert", _ScrapeOptions_TV.Certifications, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoCountry", _ScrapeOptions_TV.Countries, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoCreator", _ScrapeOptions_TV.Creators, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoEpisodeGuide", _ScrapeOptions_TV.EpisodeGuideURL, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoGenre", _ScrapeOptions_TV.Genres, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoOriginalTitle", _ScrapeOptions_TV.OriginalTitle, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoPlot", _ScrapeOptions_TV.Plot, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoPremiered", _ScrapeOptions_TV.Premiered, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoRating", _ScrapeOptions_TV.Ratings, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoRuntime", _ScrapeOptions_TV.Runtime, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoStatus", _ScrapeOptions_TV.Status, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoStudio", _ScrapeOptions_TV.Studios, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoTagline", _ScrapeOptions_TV.Tagline, , Enums.ContentType.TVShow)
            .SetBooleanSetting("DoTitle", _ScrapeOptions_TV.Title, , Enums.ContentType.TVShow)
            .SetBooleanSetting("FallBackEn", _InternalSettings_TV.FallBackToEng, , Enums.ContentType.TV)
            .SetBooleanSetting("GetAdultItems", _InternalSettings_TV.GetAdultItems, , Enums.ContentType.TV)

            'Image Movie
            .SetBooleanSetting("DoPoster", _ScrapeModifier_Movie.Poster, , Enums.ContentType.Movie)
            .SetBooleanSetting("DoFanart", _ScrapeModifier_Movie.Fanart, , Enums.ContentType.Movie)

            'Image Movieset
            .SetBooleanSetting("DoPoster", _ScrapeModifier_Movieset.Poster, , Enums.ContentType.MovieSet)
            .SetBooleanSetting("DoFanart", _ScrapeModifier_Movieset.Fanart, , Enums.ContentType.MovieSet)

            'Image TV
            .SetBooleanSetting("DoEpisodePoster", _ScrapeModifier_TV.Episodes.Poster, , Enums.ContentType.TV)
            .SetBooleanSetting("DoSeasonPoster", _ScrapeModifier_TV.Seasons.Poster, , Enums.ContentType.TV)
            .SetBooleanSetting("DoShowFanart", _ScrapeModifier_TV.Fanart, , Enums.ContentType.TV)
            .SetBooleanSetting("DoShowPoster", _ScrapeModifier_TV.Poster, , Enums.ContentType.TV)
        End With

        _XmlAddonSettings.Save()
    End Sub

    'Function GetTMDbIdByIMDbId(ByVal imdbId As String, ByRef tmdbId As Integer) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Data_Scraper_Movie.GetTMDbIdByIMDbId
    '    logger.Trace("[TMDb_Data] [GetTMDBID] [Start]")
    '    If Not String.IsNullOrEmpty(imdbId) Then
    '        If Not _Scraper.IsClientCreated Then
    '            Task.Run(Function() _Scraper.CreateAPI(_SpecialSettings_Movie))
    '        End If
    '        If _Scraper.IsClientCreated Then
    '            tmdbId = _Scraper.GetId_Movie(imdbId)
    '        End If
    '    Else
    '        logger.Trace("[TMDb_Data] [GetTMDBID] [Abort] No IMDB ID to get the TMDB ID")
    '    End If
    '    logger.Trace("[TMDb_Data] [GetTMDBID] [Done]")
    '    Return New Interfaces.AddonResult_Generic
    'End Function

    'Function GetCollectionID(ByVal imdbIdOrTmdbId As String, ByRef tmdbCollectionId As Integer) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Data_Scraper_Movieset.GetTMDbCollectionId
    '    logger.Trace("[TMDb_Data] [GetCollectionID] [Start]")
    '    If Not String.IsNullOrEmpty(imdbIdOrTmdbId) Then
    '        If Not _Scraper.IsClientCreated Then
    '            Task.Run(Function() _Scraper.CreateAPI(_SpecialSettings_Movie))
    '        End If
    '        If _Scraper.IsClientCreated Then
    '            tmdbCollectionId = _TMDbApi_Movieset.GetCollectionIdByMovieId(imdbIdOrTmdbId)
    '            If tmdbCollectionId = -1 Then
    '                logger.Trace("[TMDb_Data] [GetCollectionID] [Abort] nor search result")
    '                Return New Interfaces.AddonResult_Generic With {.Status = Interfaces.ResultStatus.Cancelled}
    '            End If
    '        End If
    '    End If
    '    logger.Trace("[TMDb_Data] [GetCollectionID] [Done]")
    '    Return New Interfaces.AddonResult_Generic
    'End Function

    'Function Scraper_Movie(ByRef dbElement As Database.DBElement,
    '                       ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                       ByRef scrapeType As Enums.ScrapeType,
    '                       ByRef scrapeOptions As Structures.ScrapeOptions
    '                       ) As Interfaces.AddonResult_Data_Scraper_Movie Implements Interfaces.IAddon_Data_Scraper_Movie.Scraper_Movie
    '    logger.Trace("[TMDb_Data] [Scraper_Movie] [Start]")
    '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, Addon_ScrapeOptions_Movie)
    '    Dim Result As MediaContainers.Movie = Nothing

    '    _Scraper.DefaultLanguage = dbElement.Language

    '    If scrapeModifiers.MainNFO AndAlso Not scrapeModifiers.DoSearch Then
    '        If dbElement.Movie.UniqueIDs.TMDbIdSpecified Then
    '            'TMDb-ID already available -> scrape and save data into an empty movie container (nMovie)
    '            Result = _Scraper.GetInfo_Movie(dbElement.Movie.UniqueIDs.TMDbId.ToString, FilteredOptions)
    '        ElseIf dbElement.Movie.UniqueIDs.IMDbIdSpecified Then
    '            'IMDb-ID already available -> scrape and save data into an empty movie container (nMovie)
    '            Result = _Scraper.GetInfo_Movie(dbElement.Movie.UniqueIDs.IMDbId, FilteredOptions)
    '        ElseIf Not scrapeType = Enums.ScrapeType.SingleScrape Then
    '            'no IMDb-ID or TMDb-ID for movie --> search first and try to get ID!
    '            If dbElement.Movie.TitleSpecified Then
    '                Result = _Scraper.Process_SearchResults_Movie(dbElement.Movie.Title, dbElement, scrapeType, FilteredOptions)
    '            End If
    '            'if still no search result -> exit
    '            If Result Is Nothing Then
    '                logger.Trace("[TMDb_Data] [Scraper_Movie] [Abort] No result found")
    '                Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
    '            End If
    '        End If
    '    End If

    '    If Result Is Nothing Then
    '        Select Case scrapeType
    '            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
    '                logger.Trace("[TMDb_Data] [Scraper_Movie] [Abort] No result found")
    '                Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
    '        End Select
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_Movie] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movie(Result)
    '    End If

    '    If scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto Then
    '        If Not dbElement.Movie.UniqueIDs.TMDbIdSpecified Then
    '            Using dlgSearch As New dlgSearchResults(_Scraper, "tmdb", New List(Of String) From {"TMDb", "IMDb"}, Enums.ContentType.Movie)
    '                Select Case dlgSearch.ShowDialog(dbElement.Movie.Title, dbElement.Filename, dbElement.Movie.Year)
    '                    Case DialogResult.Cancel
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_Movie] [Cancelled] Cancelled by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.Cancelled)
    '                    Case DialogResult.OK
    '                        logger.Trace("[TMDb_Data] [Scraper_Movie] [Done]")
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movie(_Scraper.GetInfo_Movie(dlgSearch.Result_Movie.UniqueIDs.TMDbId.ToString, FilteredOptions))
    '                    Case DialogResult.Retry
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_Movie] [Skipped] Skipped by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.Skipped)
    '                End Select
    '            End Using
    '        End If
    '    End If

    '    If Result IsNot Nothing Then
    '        logger.Trace("[TMDb_Data] [Scraper_Movie] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movie(Result)
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_Movie] [Abort] No result found")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
    '    End If
    'End Function

    'Function Scraper_Movieset(ByRef dbElement As Database.DBElement,
    '                          ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                          ByRef scrapeType As Enums.ScrapeType,
    '                          ByRef scrapeOptions As Structures.ScrapeOptions
    '                          ) As Interfaces.AddonResult_Data_Scraper_Movieset Implements Interfaces.IAddon_Data_Scraper_Movieset.Scraper
    '    logger.Trace("[TMDb_Data] [Scraper_Movieset] [Start]")
    '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, Addon_ScrapeOptions_Movieset)
    '    Dim Result As MediaContainers.Movieset = Nothing

    '    _TMDbApi_Movieset.DefaultLanguage = dbElement.Language

    '    If scrapeModifiers.MainNFO AndAlso Not scrapeModifiers.DoSearch Then
    '        If dbElement.MovieSet.UniqueIDs.TMDbIdSpecified Then
    '            'TMDB-ID already available -> scrape and save data into an empty movieset container (nMovieSet)
    '            Result = _TMDbApi_Movieset.GetInfo_Movieset(dbElement.MovieSet.UniqueIDs.TMDbId, FilteredOptions)
    '        ElseIf Not scrapeType = Enums.ScrapeType.SingleScrape Then
    '            'no ITMDB-ID for movieset --> search first and try to get ID!
    '            If dbElement.MovieSet.TitleSpecified Then
    '                Result = _TMDbApi_Movieset.Process_SearchResults_Movieset(dbElement.MovieSet.Title, dbElement, scrapeType, FilteredOptions)
    '            End If
    '            'if still no search result -> exit
    '            If Result Is Nothing Then
    '                logger.Trace("[TMDb_Data] [Scraper_Movieset] [Abort] No result found")
    '                Return New Interfaces.AddonResult_Data_Scraper_Movieset(Interfaces.ResultStatus.NoResult)
    '            End If
    '        End If
    '    End If

    '    If Result Is Nothing Then
    '        Select Case scrapeType
    '            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
    '                logger.Trace("[TMDb_Data] [Scraper_Movieset] [Abort] No result found")
    '                Return New Interfaces.AddonResult_Data_Scraper_Movieset(Interfaces.ResultStatus.NoResult)
    '        End Select
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_Movieset] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movieset(Result)
    '    End If

    '    If scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto Then
    '        If Not dbElement.MovieSet.UniqueIDs.TMDbIdSpecified Then
    '            Using dlgSearch As New dlgSearchResults(_TMDbApi_Movieset, "tmdb", New List(Of String) From {"TMDb"}, Enums.ContentType.MovieSet)
    '                Select Case dlgSearch.ShowDialog(dbElement.MovieSet.Title)
    '                    Case DialogResult.Cancel
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_Movieset] [Cancelled] Cancelled by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movieset(Interfaces.ResultStatus.Cancelled)
    '                    Case DialogResult.OK
    '                        logger.Trace("[TMDb_Data] [Scraper_Movieset] [Done]")
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movieset(_TMDbApi_Movieset.GetInfo_Movieset(dlgSearch.Result_Movieset.UniqueIDs.TMDbId, FilteredOptions))
    '                    Case DialogResult.Retry
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_Movieset] [Skipped] Skipped by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_Movieset(Interfaces.ResultStatus.Skipped)
    '                End Select
    '            End Using
    '        End If
    '    End If

    '    If Result IsNot Nothing Then
    '        logger.Trace("[TMDb_Data] [Scraper_Movieset] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movieset(Result)
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_Movieset] [Abort] No result found")
    '        Return New Interfaces.AddonResult_Data_Scraper_Movieset(Interfaces.ResultStatus.NoResult)
    '    End If
    'End Function

    'Public Function Scraper_TVEpisode(ByRef dbElement As Database.DBElement,
    '                                  ByVal scrapeOptions As Structures.ScrapeOptions
    '                                  ) As Interfaces.AddonResult_Data_Scraper_TVEpisode Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVEpisode
    '    logger.Trace("[TMDb_Data] [Scraper_TVEpisode] [Start]")
    '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, Addon_ScrapeOptions_TV)
    '    Dim Result As New MediaContainers.EpisodeDetails

    '    _TMDbApi_TV.DefaultLanguage = dbElement.Language

    '    If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
    '        dbElement.TVShow.UniqueIDs.TMDbId = _TMDbApi_TV.GetTMDbByTVDb(dbElement.TVShow.UniqueIDs.TVDbId)
    '    End If

    '    If dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
    '        If Not dbElement.TVEpisode.Episode = -1 AndAlso Not dbElement.TVEpisode.Season = -1 Then
    '            Result = _TMDbApi_TV.GetInfo_TVEpisode(dbElement.TVShow.UniqueIDs.TMDbId, dbElement.TVEpisode.Season, dbElement.TVEpisode.Episode, FilteredOptions)
    '        ElseIf dbElement.TVEpisode.AiredSpecified Then
    '            Result = _TMDbApi_TV.GetInfo_TVEpisode(dbElement.TVShow.UniqueIDs.TMDbId, dbElement.TVEpisode.Aired, FilteredOptions)
    '        Else
    '            logger.Trace(String.Format("[TMDb_Data] [Scraper_TVEpisode] [Abort] No result found"))
    '            Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
    '        End If
    '        'if still no search result -> exit
    '        If Result Is Nothing Then
    '            logger.Trace(String.Format("[TMDb_Data] [Scraper_TVEpisode] [Abort] No result found"))
    '            Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
    '        End If
    '    Else
    '        logger.Trace(String.Format("[TMDb_Data] [Scraper_TVEpisode] [Abort] No TV Show TMDb ID available"))
    '        Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
    '    End If

    '    If Result IsNot Nothing Then
    '        logger.Trace("[TMDb_Data] [Scraper_TVEpisode] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Result)
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_TVEpisode] [Abort] No result found")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVEpisode(Interfaces.ResultStatus.NoResult)
    '    End If
    'End Function

    'Public Function Scraper_TVSeason(ByRef dbElement As Database.DBElement,
    '                                 ByVal scrapeOptions As Structures.ScrapeOptions
    '                                 ) As Interfaces.AddonResult_Data_Scraper_TVSeason Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVSeason
    '    logger.Trace("[TMDb_Data] [Scraper_TVSeason] [Start]")
    '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, Addon_ScrapeOptions_TV)
    '    Dim Result As New MediaContainers.SeasonDetails

    '    _TMDbApi_TV.DefaultLanguage = dbElement.Language

    '    If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified AndAlso dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
    '        dbElement.TVShow.UniqueIDs.TMDbId = _TMDbApi_TV.GetTMDbByTVDb(dbElement.TVShow.UniqueIDs.TVDbId)
    '    End If

    '    If dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
    '        If dbElement.TVSeason.SeasonSpecified Then
    '            Result = _TMDbApi_TV.GetInfo_TVSeason(dbElement.TVShow.UniqueIDs.TMDbId, dbElement.TVSeason.Season, FilteredOptions)
    '        Else
    '            logger.Trace(String.Format("[TMDb_Data] [Scraper_TVSeason] [Abort] Season number is not specified"))
    '            Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
    '        End If
    '        'if still no search result -> exit
    '        If Result Is Nothing Then
    '            logger.Trace(String.Format("[TMDb_Data] [Scraper_TVSeason] [Abort] No result found"))
    '            Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
    '        End If
    '    Else
    '        logger.Trace(String.Format("[TMDb_Data] [Scraper_TVSeason] [Abort] No TV Show TMDb ID available"))
    '        Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
    '    End If

    '    If Result IsNot Nothing Then
    '        logger.Trace("[TMDb_Data] [Scraper_TVSeason] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Result)
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_TVSeason] [Abort] No result found")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVSeason(Interfaces.ResultStatus.NoResult)
    '    End If
    'End Function

    'Function Scraper_TVShow(ByRef dbElement As Database.DBElement,
    '                        ByRef scrapeModifiers As Structures.ScrapeModifiers,
    '                        ByRef scrapeType As Enums.ScrapeType,
    '                        ByRef scrapeOptions As Structures.ScrapeOptions
    '                        ) As Interfaces.AddonResult_Data_Scraper_TVShow Implements Interfaces.IAddon_Data_Scraper_TV.Scraper_TVShow
    '    logger.Trace("[TMDb_Data] [Scraper_TV] [Start]")
    '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, Addon_ScrapeOptions_TV)
    '    Dim Result As MediaContainers.TVShow = Nothing

    '    _TMDbApi_TV.DefaultLanguage = dbElement.Language

    '    If Not scrapeModifiers.DoSearch AndAlso
    '        (scrapeModifiers.MainNFO OrElse
    '        (scrapeModifiers.withEpisodes AndAlso scrapeModifiers.EpisodeNFO) OrElse
    '        (scrapeModifiers.withSeasons AndAlso scrapeModifiers.SeasonNFO)) Then
    '        If dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
    '            'TMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
    '            Result = _TMDbApi_TV.GetInfo_TVShow(dbElement.TVShow.UniqueIDs.TMDbId, FilteredOptions, scrapeModifiers)
    '        ElseIf dbElement.TVShow.UniqueIDs.TVDbIdSpecified Then
    '            dbElement.TVShow.UniqueIDs.TMDbId = _TMDbApi_TV.GetTMDbByTVDb(dbElement.TVShow.UniqueIDs.TVDbId)
    '            If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
    '            Result = _TMDbApi_TV.GetInfo_TVShow(dbElement.TVShow.UniqueIDs.TMDbId, FilteredOptions, scrapeModifiers)
    '        ElseIf dbElement.TVShow.UniqueIDs.IMDbIdSpecified Then
    '            dbElement.TVShow.UniqueIDs.TMDbId = _TMDbApi_TV.GetTMDbByIMDb(dbElement.TVShow.UniqueIDs.IMDbId)
    '            If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
    '            Result = _TMDbApi_TV.GetInfo_TVShow(dbElement.TVShow.UniqueIDs.TMDbId, FilteredOptions, scrapeModifiers)
    '        ElseIf Not scrapeType = Enums.ScrapeType.SingleScrape Then
    '            'no TVDB-ID for tv show --> search first and try to get ID!
    '            If dbElement.TVShow.TitleSpecified Then
    '                Result = _TMDbApi_TV.Process_SearchResults_TVShow(dbElement.TVShow.Title, dbElement, scrapeType, FilteredOptions, scrapeModifiers)
    '            End If
    '            'if still no search result -> exit
    '            If Result Is Nothing Then
    '                logger.Trace("[TMDb_Data] [Scraper_TV] [Abort] No result found")
    '                Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
    '            End If
    '        End If
    '    End If

    '    If Result Is Nothing Then
    '        Select Case scrapeType
    '            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
    '                logger.Trace(String.Format("[TMDb_Data] [Scraper_TV] [Abort] No result found"))
    '                Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
    '        End Select
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_TV] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVShow(Result)
    '    End If

    '    If scrapeType = Enums.ScrapeType.SingleScrape OrElse scrapeType = Enums.ScrapeType.SingleAuto Then
    '        If Not dbElement.TVShow.UniqueIDs.TMDbIdSpecified Then
    '            Using dlgSearch As New dlgSearchResults(_TMDbApi_TV, "tmdb", New List(Of String) From {"TMDb", "TVDb", "IMDb"}, Enums.ContentType.TVShow)
    '                Select Case dlgSearch.ShowDialog(dbElement.TVShow.Title, dbElement.ShowPath)
    '                    Case DialogResult.Cancel
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_TV] [Cancelled] Cancelled by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.Cancelled)
    '                    Case DialogResult.OK
    '                        logger.Trace("[TMDb_Data] [Scraper_TV] [Done]")
    '                        Return New Interfaces.AddonResult_Data_Scraper_TVShow(_TMDbApi_TV.GetInfo_TVShow(dlgSearch.Result_TVShow.UniqueIDs.TMDbId, FilteredOptions, scrapeModifiers))
    '                    Case DialogResult.Retry
    '                        logger.Trace(String.Format("[TMDb_Data] [Scraper_TV] [Skipped] Skipped by user"))
    '                        Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.Skipped)
    '                End Select
    '            End Using
    '        End If
    '    End If

    '    If Result IsNot Nothing Then
    '        logger.Trace("[TMDb_Data] [Scraper_TV] [Done]")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVShow(Result)
    '    Else
    '        logger.Trace("[TMDb_Data] [Scraper_TV] [Abort] No result found")
    '        Return New Interfaces.AddonResult_Data_Scraper_TVShow(Interfaces.ResultStatus.NoResult)
    '    End If
    'End Function

#End Region 'Methods

#Region "Nested Types"

    Structure InternalSettings

#Region "Fields"

        Dim FallBackToEng As Boolean
        Dim GetAdultItems As Boolean
        Dim SearchDeviant As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class