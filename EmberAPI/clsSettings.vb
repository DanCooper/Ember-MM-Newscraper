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

Imports NLog
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("Settings")>
Public Class Settings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Const _ExtraImagesLimit As Integer = 20
    Private _MovieExtrafanartsLimit As Integer = 4
    Private _MovieExtrathumbsLimit As Integer = 4
    Private _TVShowExtrafanartsLimit As Integer = 4

    Private _moviegenerallanguage As String = "en-US"
    Private _tvgenerallanguage As String = "en-US"
    Private Const _settingsversion As UInteger = 2

#End Region 'Fields

#Region "Properties"

    <XmlAttribute("version")>
    Public Property Version As UInteger = 0

    <XmlElement("options")>
    Public Property Options As OptionsBase = New OptionsBase

    <XmlElement("movie")>
    Public Property Movie As MovieBase = New MovieBase

    <XmlElement("movieset")>
    Public Property Movieset As MoviesetBase = New MoviesetBase

    <XmlElement("tvallseasons")>
    Public Property TVAllSeasons As TVSeasonBase = New TVSeasonBase

    <XmlElement("tvepisode")>
    Public Property TVEpisode As TVEpisodeBase = New TVEpisodeBase

    <XmlElement("tvseason")>
    Public Property TVSeason As TVSeasonBase = New TVSeasonBase

    <XmlElement("tvshow")>
    Public Property TVShow As TVShowBase = New TVShowBase










    Public Property CleanDotFanartJPG() As Boolean = False
    Public Property CleanExtrathumbs() As Boolean = False
    Public Property CleanFanartJPG() As Boolean = False
    Public Property CleanFolderJPG() As Boolean = False
    Public Property CleanMovieFanartJPG() As Boolean = False
    Public Property CleanMovieJPG() As Boolean = False
    Public Property CleanMovieNFO() As Boolean = False
    Public Property CleanMovieNFOB() As Boolean = False
    Public Property CleanMovieNameJPG() As Boolean = False
    Public Property CleanMovieTBN() As Boolean = False
    Public Property CleanMovieTBNB() As Boolean = False
    Public Property CleanPosterJPG() As Boolean = False
    Public Property CleanPosterTBN() As Boolean = False
    <XmlArray("addons")>
    <XmlArrayItem("addon")>
    Public Property Addons() As List(Of Addons.AddonClass) = New List(Of Addons.AddonClass)
    Public Property FileSystemCleanerWhitelist() As Boolean = False
    Public Property FileSystemCleanerWhitelistExts() As List(Of String) = New List(Of String)
    Public Property FileSystemExpertCleaner() As Boolean = False
    Public Property FileSystemNoStackExts() As List(Of String) = New List(Of String)
    Public Property GeneralDateAddedIgnoreNFO() As Boolean = False
    Public Property GeneralDateTime() As Enums.DateTimeStamp = Enums.DateTimeStamp.Now
    Public Property GeneralDisplayBanner() As Boolean = True
    Public Property GeneralDisplayCharacterArt() As Boolean = True
    Public Property GeneralDisplayClearArt() As Boolean = True
    Public Property GeneralDisplayClearLogo() As Boolean = True
    Public Property GeneralDisplayDiscArt() As Boolean = True
    Public Property GeneralDisplayFanart() As Boolean = True
    Public Property GeneralDisplayFanartAsBackground() As Boolean = True
    Public Property GeneralDisplayKeyart() As Boolean = True
    Public Property GeneralDisplayLandscape() As Boolean = True
    Public Property GeneralDisplayPoster() As Boolean = True
    Public Property GeneralDoubleClickScrape() As Boolean = False
    Public Property GeneralFilterPanelIsRaisedMovie() As Boolean = True
    Public Property GeneralFilterPanelIsRaisedMovieSet() As Boolean = True
    Public Property GeneralFilterPanelIsRaisedTVShow() As Boolean = True
    Public Property GeneralImageFilter() As Boolean = False
    Public Property GeneralImageFilterAutoscraper() As Boolean = False
    Public Property GeneralImageFilterFanart() As Boolean = False
    Public Property GeneralImageFilterFanartMatchTolerance() As Integer = 4
    Public Property GeneralImageFilterImagedialog() As Boolean = False
    Public Property GeneralImageFilterPoster() As Boolean = False
    Public Property GeneralImageFilterPosterMatchTolerance() As Integer = 1
    Public Property GeneralImagesGlassOverlay() As Boolean = False
    Public Property GeneralInfoPanelStateMovie() As Integer = 2
    Public Property GeneralInfoPanelStateMovieSet() As Integer = 2
    Public Property GeneralInfoPanelStateTVEpisode() As Integer = 2
    Public Property GeneralInfoPanelStateTVSeason() As Integer = 2
    Public Property GeneralInfoPanelStateTVShow() As Integer = 2
    Public Property GeneralLanguage() As String = "en-US"
    Public Property GeneralMainFilterSortColumn_Episodes() As Integer = 1
    Public Property GeneralMainFilterSortColumn_MovieSets() As Integer = 1
    Public Property GeneralMainFilterSortColumn_Movies() As Integer = 3
    Public Property GeneralMainFilterSortColumn_Seasons() As Integer = 1
    Public Property GeneralMainFilterSortColumn_Shows() As Integer = 1
    Public Property GeneralMainFilterSortOrder_Episodes() As Integer = 0
    Public Property GeneralMainFilterSortOrder_MovieSets() As Integer = 0
    Public Property GeneralMainFilterSortOrder_Movies() As Integer = 0
    Public Property GeneralMainFilterSortOrder_Seasons() As Integer = 0
    Public Property GeneralMainFilterSortOrder_Shows() As Integer = 0
    Public Property GeneralMainTabSorting As List(Of MainTabSorting) = New List(Of MainTabSorting)
    Public Property GeneralNotificationAddedMovie As Boolean = True
    Public Property GeneralNotificationAddedMovieset As Boolean = True
    Public Property GeneralNotificationAddedTVEpisode As Boolean = True
    Public Property GeneralNotificationAddedTVShow As Boolean = True
    Public Property GeneralNotificationError As Boolean = True
    Public Property GeneralNotificationInformation As Boolean = True
    Public Property GeneralNotificationScrapedMovie As Boolean = True
    Public Property GeneralNotificationScrapedMovieset As Boolean = True
    Public Property GeneralNotificationScrapedTVEpisode As Boolean = True
    Public Property GeneralNotificationScrapedTVSeason As Boolean = True
    Public Property GeneralNotificationScrapedTVShow As Boolean = True
    Public Property GeneralNotificationWarning As Boolean = True
    Public Property GeneralOverwriteNfo() As Boolean = False
    Public Property GeneralShowGenresText() As Boolean = True
    Public Property GeneralShowImgDims() As Boolean = True
    Public Property GeneralShowImgNames() As Boolean = True
    Public Property GeneralShowLangFlags() As Boolean = True
    Public Property GeneralSourceFromFolder() As Boolean = False
    Public Property GeneralSplitterDistanceMain() As Integer = 550
    Public Property GeneralSplitterDistanceTVSeason() As Integer = 200
    Public Property GeneralSplitterDistanceTVShow() As Integer = 200
    Public Property GeneralTheme() As String = "FullHD-Default"
    Public Property GeneralWindowLoc() As Point = New Point(10, 10)
    Public Property GeneralWindowSize() As Size = New Size(1024, 768)
    Public Property GeneralWindowState() As FormWindowState = FormWindowState.Maximized
    Public Property MovieBackdropsAuto() As Boolean = False
    Public Property MovieBackdropsPath() As String = String.Empty
    Public Property MovieCleanDB() As Boolean = False
    Public Property MovieClickScrape() As Boolean = False
    Public Property MovieClickScrapeAsk() As Boolean = False
    <XmlIgnore>
    Public Property MovieFilterCustom() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.TitleFilters_Movie).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("MovieFilterCustom")>
    Public Property MovieFilterCustomXml As String()
        Get
            Return MovieFilterCustom.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                MovieFilterCustom.Clear()
                MovieFilterCustom.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property MovieGeneralCustomMarker1Color() As Integer = -32704
    Public Property MovieGeneralCustomMarker1Name() As String = String.Empty
    Public Property MovieGeneralCustomMarker2Color() As Integer = -16776961
    Public Property MovieGeneralCustomMarker2Name() As String = String.Empty
    Public Property MovieGeneralCustomMarker3Color() As Integer = -12582784
    Public Property MovieGeneralCustomMarker3Name() As String = String.Empty
    Public Property MovieGeneralCustomMarker4Color() As Integer = -16711681
    Public Property MovieGeneralCustomMarker4Name() As String = String.Empty
    Public Property MovieGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property MovieGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property MovieGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.Skip
    Public Property MovieGeneralCustomScrapeButtonSelectionType() As Enums.SelectionType = Enums.SelectionType.New
    Public Property MovieGeneralFlagLang() As String = String.Empty
    Public Property MovieGeneralIgnoreLastScan() As Boolean = True
    Public Property MovieGeneralLanguage() As String
        Get
            Return _moviegenerallanguage
        End Get
        Set(ByVal value As String)
            _moviegenerallanguage = If(String.IsNullOrEmpty(value), "en-US", value)
        End Set
    End Property
    Public Property MovieGeneralMarkNew() As Boolean = False
    Public Property MovieGeneralMediaListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property MovieLevTolerance() As Integer = 0
    Public Property MovieMetadataPerFileType() As List(Of MetadataPerType) = New List(Of MetadataPerType)
    Public Property MovieMissingBanner() As Boolean = False
    Public Property MovieMissingClearArt() As Boolean = False
    Public Property MovieMissingClearLogo() As Boolean = False
    Public Property MovieMissingDiscArt() As Boolean = False
    Public Property MovieMissingExtrafanarts() As Boolean = False
    Public Property MovieMissingExtrathumbs() As Boolean = False
    Public Property MovieMissingFanart() As Boolean = False
    Public Property MovieMissingLandscape() As Boolean = False
    Public Property MovieMissingNFO() As Boolean = False
    Public Property MovieMissingPoster() As Boolean = False
    Public Property MovieMissingSubtitles() As Boolean = False
    Public Property MovieMissingTheme() As Boolean = False
    Public Property MovieMissingTrailer() As Boolean = False
    Public Property MovieProperCase() As Boolean = True
    Public Property MovieRecognizeVTSExpertVTS() As Boolean
    Public Property MovieScanOrderModify() As Boolean = False
    Public Property MovieScraperDurationRuntimeFormat() As String = "<m>"
    Public Property MovieScraperMetaDataIFOScan() As Boolean = True
    Public Property MovieScraperMetaDataScan() As Boolean = True
    Public Property MovieSetCleanDB() As Boolean = False
    Public Property MovieSetCleanFiles() As Boolean = False
    Public Property MovieSetClickScrape() As Boolean = False
    Public Property MovieSetClickScrapeAsk() As Boolean = False
    Public Property MovieSetGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property MovieSetGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property MovieSetGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.Skip
    Public Property MovieSetGeneralCustomScrapeButtonSelectionType() As Enums.SelectionType = Enums.SelectionType.New
    Public Property MovieSetGeneralMarkNew() As Boolean = False
    Public Property MovieSetGeneralMediaListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property MovieSetMissingBanner() As Boolean = False
    Public Property MovieSetMissingClearArt() As Boolean = False
    Public Property MovieSetMissingClearLogo() As Boolean = False
    Public Property MovieSetMissingDiscArt() As Boolean = False
    Public Property MovieSetMissingFanart() As Boolean = False
    Public Property MovieSetMissingLandscape() As Boolean = False
    Public Property MovieSetMissingNFO() As Boolean = False
    Public Property MovieSetMissingPoster() As Boolean = False
    Public Property MovieSetScraperIdDefaultType() As String = "tmdb"
    Public Property MovieSetScraperIdWriteNodeDefaultId() As Boolean = False
    Public Property MoviesetTitleRenaming() As List(Of SimpleMapping) = New List(Of SimpleMapping)
    Public Property MovieSkipLessThan() As Integer = 0
    Public Property MovieSkipStackedSizeCheck() As Boolean = False
    Public Property MovieSortBeforeScan() As Boolean = False
    Public Property MovieThemeDefaultSearch() As String = "theme soundtrack"
    Public Property MovieThemeKeepExisting() As Boolean = False
    Public Property MovieTrailerDefaultSearch() As String = "trailer"
    Public Property MovieTrailerKeepExisting() As Boolean = False
    Public Property MovieTrailerMinVideoQual() As Enums.VideoResolution = Enums.VideoResolution.Any
    Public Property MovieTrailerPrefVideoQual() As Enums.VideoResolution = Enums.VideoResolution.Any
    Public Property OMMDummyFormat() As Integer = 0
    Public Property OMMDummyTagline() As String = String.Empty
    Public Property OMMDummyTop() As String = String.Empty
    Public Property OMMDummyUseBackground() As Boolean = True
    Public Property OMMDummyUseFanart() As Boolean = True
    Public Property OMMDummyUseOverlay() As Boolean = True
    Public Property OMMMediaStubTagline() As String = String.Empty
    Public Property Password() As String = String.Empty
    Public Property ProxyCredentials() As NetworkCredential = New NetworkCredential
    Public Property ProxyPort() As Integer = 0
    Public Property ProxyURI() As String = String.Empty
    Public Property SortPath() As String = String.Empty
    Public Property TVCleanDB() As Boolean = False
    Public Property TVDisplayMissingEpisodes() As Boolean = True
    <XmlIgnore>
    Public Property TVEpisodeFilterCustom() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.TitleFilters_TVEpisode).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("TVEpisodeFilterCustom")>
    Public Property TVEpisodeFilterCustomXml As String()
        Get
            Return TVEpisodeFilterCustom.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                TVEpisodeFilterCustom.Clear()
                TVEpisodeFilterCustom.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property TVEpisodeMissingFanart() As Boolean = False
    Public Property TVEpisodeMissingNFO() As Boolean = False
    Public Property TVEpisodeMissingPoster() As Boolean = False
    Public Property TVEpisodeNoFilter() As Boolean = True
    Public Property TVEpisodeProperCase() As Boolean = True
    Public Property TVGeneralClickScrape() As Boolean = False
    Public Property TVGeneralClickScrapeAsk() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property TVGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.Skip
    Public Property TVGeneralCustomScrapeButtonSelectionType() As Enums.SelectionType = Enums.SelectionType.New
    Public Property TVGeneralEpisodeListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property TVGeneralFlagLang() As String = String.Empty
    Public Property TVGeneralIgnoreLastScan() As Boolean = True
    Public Property TVGeneralLanguage() As String
        Get
            Return _tvgenerallanguage
        End Get
        Set(ByVal value As String)
            _tvgenerallanguage = If(String.IsNullOrEmpty(value), "en-US", value)
        End Set
    End Property
    Public Property TVGeneralMarkNewEpisodes() As Boolean = False
    Public Property TVGeneralMarkNewShows() As Boolean = False
    Public Property TVGeneralSeasonListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property TVGeneralShowListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property TVLockEpisodeActors() As Boolean = False
    Public Property TVLockEpisodeAired() As Boolean = False
    Public Property TVLockEpisodeCredits() As Boolean = False
    Public Property TVLockEpisodeDirector() As Boolean = False
    Public Property TVLockEpisodeGuestStars() As Boolean = False
    Public Property TVLockEpisodeLanguageA() As Boolean = False
    Public Property TVLockEpisodeLanguageV() As Boolean = False
    Public Property TVLockEpisodePlot() As Boolean = False
    Public Property TVLockEpisodeRating() As Boolean = False
    Public Property TVLockEpisodeRuntime() As Boolean = False
    Public Property TVLockEpisodeTitle() As Boolean = False
    Public Property TVLockEpisodeUserRating() As Boolean = False
    Public Property TVLockSeasonPlot() As Boolean = False
    Public Property TVLockSeasonTitle() As Boolean = False
    Public Property TVLockShowActors() As Boolean = False
    Public Property TVLockShowCert() As Boolean = False
    Public Property TVLockShowCountry() As Boolean = False
    Public Property TVLockShowCreators() As Boolean = False
    Public Property TVLockShowGenre() As Boolean = False
    Public Property TVLockShowMPAA() As Boolean = False
    Public Property TVLockShowOriginalTitle() As Boolean = False
    Public Property TVLockShowPlot() As Boolean = False
    Public Property TVLockShowPremiered() As Boolean = False
    Public Property TVLockShowRating() As Boolean = False
    Public Property TVLockShowRuntime() As Boolean = False
    Public Property TVLockShowStatus() As Boolean = False
    Public Property TVLockShowStudio() As Boolean = False
    Public Property TVLockShowTagline() As Boolean = False
    Public Property TVLockShowTitle() As Boolean = False
    Public Property TVLockShowUserRating() As Boolean = False
    Public Property TVMetadataPerFileType() As List(Of MetadataPerType) = New List(Of MetadataPerType)
    Public Property TVMultiPartMatching() As String = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
    Public Property TVScanOrderModify() As Boolean = False
    Public Property TVScraperCastWithImgOnly() As Boolean = False
    Public Property TVScraperCleanFields() As Boolean = False
    Public Property TVScraperDurationRuntimeFormat() As String = "<m>"
    Public Property TVScraperEpisodeActors() As Boolean = True
    Public Property TVScraperEpisodeActorsLimit() As Integer = 0
    Public Property TVScraperEpisodeAired() As Boolean = True
    Public Property TVScraperEpisodeCredits() As Boolean = True
    Public Property TVScraperEpisodeDirector() As Boolean = True
    Public Property TVScraperEpisodeGuestStars() As Boolean = True
    Public Property TVScraperEpisodeGuestStarsLimit() As Integer = 0
    Public Property TVScraperEpisodeGuestStarsToActors() As Boolean = False
    Public Property TVScraperEpisodeOriginalTitle() As Boolean = True
    Public Property TVScraperEpisodePlot() As Boolean = True
    Public Property TVScraperEpisodeRating() As Boolean = True
    Public Property TVScraperEpisodeRuntime() As Boolean = True
    Public Property TVScraperEpisodeTitle() As Boolean = True
    Public Property TVScraperEpisodeUserRating() As Boolean = True
    Public Property TVScraperIdDefaultType() As String = "tvdb"
    Public Property TVScraperIdWriteNodeDefaultId() As Boolean = True
    Public Property TVScraperIdWriteNodeIMDbId() As Boolean = False
    Public Property TVScraperIdWriteNodeTMDbId() As Boolean = False
    Public Property TVScraperIdWriteNodeTVDbId() As Boolean = False
    Public Property TVScraperMetaDataScan() As Boolean = True
    Public Property TVScraperOptionsOrdering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard
    Public Property TVScraperRatingDefaultType() As String = "themoviedb"
    Public Property TVScraperRatingVotesWriteNode() As Boolean = True
    Public Property TVScraperSeasonAired() As Boolean = True
    Public Property TVScraperSeasonPlot() As Boolean = True
    Public Property TVScraperSeasonTitle() As Boolean = True
    <XmlIgnore>
    Public Property TVScraperSeasonTitleBlacklist() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.TitleBlackList_TVSeason).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("TVScraperSeasonTitleBlacklist")>
    Public Property TVScraperSeasonTitleBlacklistXml As String()
        Get
            Return TVScraperSeasonTitleBlacklist.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                TVScraperSeasonTitleBlacklist.Clear()
                TVScraperSeasonTitleBlacklist.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property TVScraperShowActors() As Boolean = True
    Public Property TVScraperShowActorsLimit() As Integer = 0
    Public Property TVScraperShowCert() As Boolean = False
    Public Property TVScraperShowCertCountry() As String = String.Empty
    Public Property TVScraperShowCertForMPAA() As Boolean = False
    Public Property TVScraperShowCertForMPAAFallback() As Boolean = False
    <Obsolete("Use ""TVScraperShowCertCountry"" instead")>
    Public Property TVScraperShowCertLang() As String = String.Empty
#Disable Warning BC40000 'The type or member is obsolete.
    Public ReadOnly Property TVScraperShowCertLangSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(TVScraperShowCertLang)
        End Get
    End Property
#Enable Warning BC40000 'The type or member is obsolete.
    Public Property TVScraperShowCertOnlyValue() As Boolean = False
    Public Property TVScraperShowCountry() As Boolean = True
    Public Property TVScraperShowCountryLimit() As Integer = 0
    Public Property TVScraperShowCreators() As Boolean = True
    Public Property TVScraperShowEpiGuideURL() As Boolean = False
    Public Property TVScraperShowGenre() As Boolean = True
    Public Property TVScraperShowGenreLimit() As Integer = 0
    Public Property TVScraperShowMPAA() As Boolean = True
    Public Property TVScraperShowMPAANotRated() As String = String.Empty
    Public Property TVScraperShowOriginalTitle() As Boolean = True
    Public Property TVScraperShowOriginalTitleAsTitle() As Boolean = False
    Public Property TVScraperShowPlot() As Boolean = True
    Public Property TVScraperShowPremiered() As Boolean = True
    Public Property TVScraperShowRating() As Boolean = True
    Public Property TVScraperShowRuntime() As Boolean = True
    Public Property TVScraperShowStatus() As Boolean = True
    Public Property TVScraperShowStudio() As Boolean = True
    Public Property TVScraperShowStudioLimit() As Integer = 0
    Public Property TVScraperShowTagline() As Boolean = True
    Public Property TVScraperShowTitle() As Boolean = True
    Public Property TVScraperShowUserRating() As Boolean = True
    Public Property TVScraperUseDisplaySeasonEpisode() As Boolean = True
    Public Property TVScraperUseMDDuration() As Boolean = True
    Public Property TVScraperUseSRuntimeForEp() As Boolean = True
    Public Property TVSeasonMissingBanner() As Boolean = False
    Public Property TVSeasonMissingFanart() As Boolean = False
    Public Property TVSeasonMissingLandscape() As Boolean = False
    Public Property TVSeasonMissingPoster() As Boolean = False
    <XmlIgnore>
    Public Property TVShowFilterCustom() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.TitleFilters_TVShow).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("TVShowFilterCustom")>
    Public Property TVShowFilterCustomXml As String()
        Get
            Return TVShowFilterCustom.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                TVShowFilterCustom.Clear()
                TVShowFilterCustom.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property TVShowMatching() As List(Of regexp) = New List(Of regexp)
    Public Property TVShowMissingBanner() As Boolean = False
    Public Property TVShowMissingCharacterArt() As Boolean = False
    Public Property TVShowMissingClearArt() As Boolean = False
    Public Property TVShowMissingClearLogo() As Boolean = False
    Public Property TVShowMissingExtrafanarts() As Boolean = False
    Public Property TVShowMissingFanart() As Boolean = False
    Public Property TVShowMissingLandscape() As Boolean = False
    Public Property TVShowMissingNFO() As Boolean = False
    Public Property TVShowMissingPoster() As Boolean = False
    Public Property TVShowMissingTheme() As Boolean = False
    Public Property TVShowProperCase() As Boolean = True
    Public Property TVShowThemeDefaultSearch() As String = "theme soundtrack"
    Public Property TVShowThemeKeepExisting() As Boolean = False
    Public Property TVSkipLessThan() As Integer = 0
    Public Property Username() As String = String.Empty

#End Region 'Properties

#Region "File Name Properties"

    '***************************************************
    '******************* Movie Part ********************
    '***************************************************

    '*************** XBMC Frodo settings ***************
    Public Property MovieUseFrodo() As Boolean = False
    Public Property MovieActorThumbsFrodo() As Boolean = False
    Public Property MovieExtrafanartsFrodo() As Boolean = False
    Public Property MovieExtrathumbsFrodo() As Boolean = False
    Public Property MovieFanartFrodo() As Boolean = False
    Public Property MovieNFOFrodo() As Boolean = False
    Public Property MoviePosterFrodo() As Boolean = False
    Public Property MovieTrailerFrodo() As Boolean = False

    '*************** XBMC Eden settings ***************
    Public Property MovieUseEden() As Boolean = False
    Public Property MovieActorThumbsEden() As Boolean = False
    Public Property MovieExtrafanartsEden() As Boolean = False
    Public Property MovieExtrathumbsEden() As Boolean = False
    Public Property MovieFanartEden() As Boolean = False
    Public Property MovieNFOEden() As Boolean = False
    Public Property MoviePosterEden() As Boolean = False
    Public Property MovieTrailerEden() As Boolean = False

    '************* XBMC optional settings *************
    Public Property MovieXBMCProtectVTSBDMV() As Boolean = False

    '******** XBMC ArtworkDownloader settings **********
    Public Property MovieUseAD() As Boolean = False
    Public Property MovieBannerAD() As Boolean = False
    Public Property MovieClearArtAD() As Boolean = False
    Public Property MovieClearLogoAD() As Boolean = False
    Public Property MovieDiscArtAD() As Boolean = False
    Public Property MovieLandscapeAD() As Boolean = False

    '********* XBMC Extended Images settings ***********
    Public Property MovieUseExtended() As Boolean = False
    Public Property MovieBannerExtended() As Boolean = False
    Public Property MovieClearArtExtended() As Boolean = False
    Public Property MovieClearLogoExtended() As Boolean = False
    Public Property MovieDiscArtExtended() As Boolean = False
    Public Property MovieKeyartExtended() As Boolean = False
    Public Property MovieLandscapeExtended() As Boolean = False

    '************** XBMC TvTunes settings **************
    Public Property MovieThemeTvTunesEnable() As Boolean = False
    Public Property MovieThemeTvTunesCustom() As Boolean = False
    Public Property MovieThemeTvTunesCustomPath() As String = String.Empty
    Public Property MovieThemeTvTunesMoviePath() As Boolean = False
    Public Property MovieThemeTvTunesSub() As Boolean = False
    Public Property MovieThemeTvTunesSubDir() As String = String.Empty

    '****************** YAMJ settings *****************
    Public Property MovieUseYAMJ() As Boolean = False
    Public Property MovieBannerYAMJ() As Boolean = False
    Public Property MovieFanartYAMJ() As Boolean = False
    Public Property MovieNFOYAMJ() As Boolean = False
    Public Property MoviePosterYAMJ() As Boolean = False
    Public Property MovieTrailerYAMJ() As Boolean = False

    '****************** NMJ settings *****************
    Public Property MovieUseNMJ() As Boolean = False
    Public Property MovieBannerNMJ() As Boolean = False
    Public Property MovieFanartNMJ() As Boolean = False
    Public Property MovieNFONMJ() As Boolean = False
    Public Property MoviePosterNMJ() As Boolean = False
    Public Property MovieTrailerNMJ() As Boolean = False

    '************** NMJ optional settings *************
    Public Property MovieYAMJWatchedFile() As Boolean = False
    Public Property MovieYAMJWatchedFolder() As String = String.Empty

    '***************** Boxee settings *****************
    Public Property MovieUseBoxee() As Boolean = False
    Public Property MovieFanartBoxee() As Boolean = False
    Public Property MovieNFOBoxee() As Boolean = False
    Public Property MoviePosterBoxee() As Boolean = False

    '***************** Expert settings ****************
    Public Property MovieUseExpert() As Boolean = False

    '***************** Expert Single ******************
    Public Property MovieActorThumbsExpertSingle() As Boolean = False
    Public Property MovieActorThumbsExtExpertSingle() As String = ".jpg"
    Public Property MovieBannerExpertSingle() As String = String.Empty
    Public Property MovieClearArtExpertSingle() As String = String.Empty
    Public Property MovieClearLogoExpertSingle() As String = String.Empty
    Public Property MovieDiscArtExpertSingle() As String = String.Empty
    Public Property MovieExtrafanartsExpertSingle() As Boolean = False
    Public Property MovieExtrathumbsExpertSingle() As Boolean = False
    Public Property MovieFanartExpertSingle() As String = String.Empty
    Public Property MovieKeyartExpertSingle() As String = String.Empty
    Public Property MovieLandscapeExpertSingle() As String = String.Empty
    Public Property MovieNFOExpertSingle() As String = String.Empty
    Public Property MoviePosterExpertSingle() As String = String.Empty
    Public Property MovieStackExpertSingle() As Boolean = True
    Public Property MovieTrailerExpertSingle() As String = String.Empty
    Public Property MovieUnstackExpertSingle() As Boolean = False

    '***************** Expert Multi ******************
    Public Property MovieActorThumbsExpertMulti() As Boolean = False
    Public Property MovieActorThumbsExtExpertMulti() As String = ".jpg"
    Public Property MovieBannerExpertMulti() As String = String.Empty
    Public Property MovieClearArtExpertMulti() As String = String.Empty
    Public Property MovieClearLogoExpertMulti() As String = String.Empty
    Public Property MovieDiscArtExpertMulti() As String = String.Empty
    Public Property MovieFanartExpertMulti() As String = String.Empty
    Public Property MovieKeyartExpertMulti() As String = String.Empty
    Public Property MovieLandscapeExpertMulti() As String = String.Empty
    Public Property MovieNFOExpertMulti() As String = String.Empty
    Public Property MoviePosterExpertMulti() As String = String.Empty
    Public Property MovieStackExpertMulti() As Boolean = True
    Public Property MovieTrailerExpertMulti() As String = String.Empty
    Public Property MovieUnstackExpertMulti() As Boolean = False

    '***************** Expert VTS ******************
    Public Property MovieActorThumbsExpertVTS() As Boolean = False
    Public Property MovieActorThumbsExtExpertVTS() As String = ".jpg"
    Public Property MovieBannerExpertVTS() As String = String.Empty
    Public Property MovieClearArtExpertVTS() As String = String.Empty
    Public Property MovieClearLogoExpertVTS() As String = String.Empty
    Public Property MovieDiscArtExpertVTS() As String = String.Empty
    Public Property MovieExtrafanartsExpertVTS() As Boolean = False
    Public Property MovieExtrathumbsExpertVTS() As Boolean = False
    Public Property MovieFanartExpertVTS() As String = String.Empty
    Public Property MovieKeyartExpertVTS() As String = String.Empty
    Public Property MovieLandscapeExpertVTS() As String = String.Empty
    Public Property MovieNFOExpertVTS() As String = String.Empty
    Public Property MoviePosterExpertVTS() As String = String.Empty
    Public Property MovieTrailerExpertVTS() As String = String.Empty
    Public Property MovieUseBaseDirectoryExpertVTS() As Boolean = True

    '***************** Expert BDMV ******************
    Public Property MovieActorThumbsExpertBDMV() As Boolean = False
    Public Property MovieActorThumbsExtExpertBDMV() As String = ".jpg"
    Public Property MovieBannerExpertBDMV() As String = String.Empty
    Public Property MovieClearArtExpertBDMV() As String = String.Empty
    Public Property MovieClearLogoExpertBDMV() As String = String.Empty
    Public Property MovieDiscArtExpertBDMV() As String = String.Empty
    Public Property MovieExtrafanartsExpertBDMV() As Boolean = False
    Public Property MovieExtrathumbsExpertBDMV() As Boolean = False
    Public Property MovieFanartExpertBDMV() As String = String.Empty
    Public Property MovieKeyartExpertBDMV() As String = String.Empty
    Public Property MovieLandscapeExpertBDMV() As String = String.Empty
    Public Property MovieNFOExpertBDMV() As String = String.Empty
    Public Property MoviePosterExpertBDMV() As String = String.Empty
    Public Property MovieTrailerExpertBDMV() As String = String.Empty
    Public Property MovieUseBaseDirectoryExpertBDMV() As Boolean = True


    '***************************************************
    '****************** MovieSet Part ******************
    '***************************************************

    '**************** Kodi Matrix settings *************
    Public Property MovieSetUseMatrix() As Boolean = False
    Public Property MovieSetBannerMatrix() As Boolean = False
    Public Property MovieSetClearArtMatrix() As Boolean = False
    Public Property MovieSetClearLogoMatrix() As Boolean = False
    Public Property MovieSetDiscArtMatrix() As Boolean = False
    Public Property MovieSetFanartMatrix() As Boolean = False
    Public Property MovieSetKeyartMatrix() As Boolean = False
    Public Property MovieSetLandscapeMatrix() As Boolean = False
    Public Property MovieSetPathMatrix() As String = String.Empty
    Public Property MovieSetPosterMatrix() As Boolean = False

    '**************** XBMC MSAA settings ***************
    Public Property MovieSetUseMSAA() As Boolean = False
    Public Property MovieSetBannerMSAA() As Boolean = False
    Public Property MovieSetClearArtMSAA() As Boolean = False
    Public Property MovieSetClearLogoMSAA() As Boolean = False
    Public Property MovieSetFanartMSAA() As Boolean = False
    Public Property MovieSetLandscapeMSAA() As Boolean = False
    Public Property MovieSetPathMSAA() As String = String.Empty
    Public Property MovieSetPosterMSAA() As Boolean = False

    '********* XBMC Extended Images settings ***********
    Public Property MovieSetUseExtended() As Boolean = False
    Public Property MovieSetBannerExtended() As Boolean = False
    Public Property MovieSetClearArtExtended() As Boolean = False
    Public Property MovieSetClearLogoExtended() As Boolean = False
    Public Property MovieSetDiscArtExtended() As Boolean = False
    Public Property MovieSetFanartExtended() As Boolean = False
    Public Property MovieSetKeyartExtended() As Boolean = False
    Public Property MovieSetLandscapeExtended() As Boolean = False
    Public Property MovieSetPathExtended() As String = String.Empty
    Public Property MovieSetPosterExtended() As Boolean = False

    '***************** Expert settings ****************
    Public Property MovieSetUseExpert() As Boolean = False

    '***************** Expert Single ******************
    Public Property MovieSetBannerExpertSingle() As String = String.Empty
    Public Property MovieSetClearArtExpertSingle() As String = String.Empty
    Public Property MovieSetClearLogoExpertSingle() As String = String.Empty
    Public Property MovieSetDiscArtExpertSingle() As String = String.Empty
    Public Property MovieSetFanartExpertSingle() As String = String.Empty
    Public Property MovieSetKeyartExpertSingle() As String = String.Empty
    Public Property MovieSetLandscapeExpertSingle() As String = String.Empty
    Public Property MovieSetNFOExpertSingle() As String = String.Empty
    Public Property MovieSetPathExpertSingle() As String = String.Empty
    Public Property MovieSetPosterExpertSingle() As String = String.Empty


    '***************************************************
    '****************** TV Show Part *******************
    '***************************************************

    '*************** XBMC Frodo settings ***************
    Public Property TVUseFrodo() As Boolean = False
    Public Property TVEpisodeActorThumbsFrodo() As Boolean = False
    Public Property TVEpisodeNFOFrodo() As Boolean = False
    Public Property TVEpisodePosterFrodo() As Boolean = False
    Public Property TVSeasonBannerFrodo() As Boolean = False
    Public Property TVSeasonFanartFrodo() As Boolean = False
    Public Property TVSeasonPosterFrodo() As Boolean = False
    Public Property TVShowActorThumbsFrodo() As Boolean = False
    Public Property TVShowBannerFrodo() As Boolean = False
    Public Property TVShowExtrafanartsFrodo() As Boolean = False
    Public Property TVShowFanartFrodo() As Boolean = False
    Public Property TVShowNFOFrodo() As Boolean = False
    Public Property TVShowPosterFrodo() As Boolean = False

    '******** XBMC ArtworkDownloader settings **********
    Public Property TVUseAD() As Boolean = False
    Public Property TVSeasonLandscapeAD() As Boolean = False
    Public Property TVShowCharacterArtAD() As Boolean = False
    Public Property TVShowClearArtAD() As Boolean = False
    Public Property TVShowClearLogoAD() As Boolean = False
    Public Property TVShowLandscapeAD() As Boolean = False

    '********* XBMC Extended Images settings ***********
    Public Property TVUseExtended() As Boolean = False
    Public Property TVSeasonLandscapeExtended() As Boolean = False
    Public Property TVShowCharacterArtExtended() As Boolean = False
    Public Property TVShowClearArtExtended() As Boolean = False
    Public Property TVShowClearLogoExtended() As Boolean = False
    Public Property TVShowKeyartExtended() As Boolean = False
    Public Property TVShowLandscapeExtended() As Boolean = False

    '************* XBMC TvTunes settings ***************
    Public Property TVShowThemeTvTunesEnable() As Boolean = False
    Public Property TVShowThemeTvTunesCustom() As Boolean = False
    Public Property TVShowThemeTvTunesCustomPath() As String = String.Empty
    Public Property TVShowThemeTvTunesShowPath() As Boolean = False
    Public Property TVShowThemeTvTunesSub() As Boolean = False
    Public Property TVShowThemeTvTunesSubDir() As String = String.Empty

    '****************** YAMJ settings ******************
    Public Property TVUseYAMJ() As Boolean = False
    Public Property TVEpisodeNFOYAMJ() As Boolean = False
    Public Property TVEpisodePosterYAMJ() As Boolean = False
    Public Property TVSeasonBannerYAMJ() As Boolean = False
    Public Property TVSeasonFanartYAMJ() As Boolean = False
    Public Property TVSeasonPosterYAMJ() As Boolean = False
    Public Property TVShowBannerYAMJ() As Boolean = False
    Public Property TVShowFanartYAMJ() As Boolean = False
    Public Property TVShowNFOYAMJ() As Boolean = False
    Public Property TVShowPosterYAMJ() As Boolean = False

    '***************** Boxee settings ******************
    Public Property TVUseBoxee() As Boolean = False
    Public Property TVEpisodeNFOBoxee() As Boolean = False
    Public Property TVEpisodePosterBoxee() As Boolean = False
    Public Property TVSeasonPosterBoxee() As Boolean = False
    Public Property TVShowBannerBoxee() As Boolean = False
    Public Property TVShowFanartBoxee() As Boolean = False
    Public Property TVShowNFOBoxee() As Boolean = False
    Public Property TVShowPosterBoxee() As Boolean = False

    '***************** Expert settings ******************
    Public Property TVUseExpert() As Boolean = False

    '***************** Expert AllSeasons ****************
    Public Property TVAllSeasonsBannerExpert() As String = String.Empty
    Public Property TVAllSeasonsFanartExpert() As String = String.Empty
    Public Property TVAllSeasonsLandscapeExpert() As String = String.Empty
    Public Property TVAllSeasonsPosterExpert() As String = String.Empty

    '***************** Expert Episode *******************
    Public Property TVEpisodeActorThumbsExpert() As Boolean = False
    Public Property TVEpisodeActorThumbsExtExpert() As String = ".jpg"
    Public Property TVEpisodeFanartExpert() As String = String.Empty
    Public Property TVEpisodeNFOExpert() As String = String.Empty
    Public Property TVEpisodePosterExpert() As String = String.Empty

    '***************** Expert Season *******************
    Public Property TVSeasonBannerExpert() As String = String.Empty
    Public Property TVSeasonFanartExpert() As String = String.Empty
    Public Property TVSeasonLandscapeExpert() As String = String.Empty
    Public Property TVSeasonPosterExpert() As String = String.Empty

    '***************** Expert Show *********************
    Public Property TVShowActorThumbsExpert() As Boolean = False
    Public Property TVShowActorThumbsExtExpert() As String = ".jpg"
    Public Property TVShowBannerExpert() As String = String.Empty
    Public Property TVShowCharacterArtExpert() As String = String.Empty
    Public Property TVShowClearArtExpert() As String = String.Empty
    Public Property TVShowClearLogoExpert() As String = String.Empty
    Public Property TVShowExtrafanartsExpert() As Boolean = False
    Public Property TVShowFanartExpert() As String = String.Empty
    Public Property TVShowKeyartExpert() As String = String.Empty
    Public Property TVShowLandscapeExpert() As String = String.Empty
    Public Property TVShowNFOExpert() As String = String.Empty
    Public Property TVShowPosterExpert() As String = String.Empty

#End Region 'File Name Properties

#Region "Methods"

    Public Sub Load()
        Dim configpath As String = Path.Combine(Master.SettingsPath, "settings.xml")
        If File.Exists(configpath) Then
            Dim xmlReader As New StreamReader(configpath)

            Try
                Dim xXMLSettings As New XmlSerializer(GetType(Settings))
                Master.eSettings = CType(xXMLSettings.Deserialize(xmlReader), Settings)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                _Logger.Info("An attempt is made to repair the settings.xml")
                Try
                    Dim sSettings As String = xmlReader.ReadToEnd
                    'old Fanart/Poster sizes
                    sSettings = Regex.Replace(sSettings, "PrefSize>Xlrg<", "PrefSize>Any<")
                    sSettings = Regex.Replace(sSettings, "PrefSize>Lrg<", "PrefSize>Any<")
                    sSettings = Regex.Replace(sSettings, "PrefSize>Mid<", "PrefSize>Any<")
                    sSettings = Regex.Replace(sSettings, "PrefSize>Small<", "PrefSize>Any<")
                    'old Trailer Audio/Video quality
                    sSettings = Regex.Replace(sSettings, "Qual>All<", "Qual>Any<")
                    'old allseasons/season/tvshow banner type
                    sSettings = Regex.Replace(sSettings, "PrefType>None<", "PrefType>Any<")
                    'old seasonposter size HD1000
                    sSettings = Regex.Replace(sSettings, "<TVSeasonPosterPrefSize>HD1000</TVSeasonPosterPrefSize>", "<TVSeasonPosterPrefSize>Any</TVSeasonPosterPrefSize>")

                    Dim xXMLSettings As New XmlSerializer(GetType(Settings))
                    Using reader As TextReader = New StringReader(sSettings)
                        Master.eSettings = CType(xXMLSettings.Deserialize(reader), Settings)
                    End Using
                    _Logger.Info("Settings.xml successfully repaired")
                Catch ex2 As Exception
                    _Logger.Error(ex2, New StackFrame().GetMethod().Name)
                    File.Copy(configpath, String.Concat(configpath, "_backup"), True)
                    Master.eSettings = New Settings
                    _Logger.Warn("Attempt to repair the settings.xml has failed. The file has been backed up and the default settings has been loaded.")
                End Try
            End Try
            xmlReader.Close()
        Else
            Master.eSettings = New Settings
        End If

        SetDefaultsForLists(Enums.DefaultType.All, False)

        'Patch older settings files if needed
        PatchSettings()

        'Fix added to avoid to have no movie NFO saved
        If Not (Master.eSettings.MovieUseBoxee Or Master.eSettings.MovieUseEden Or Master.eSettings.MovieUseExpert Or Master.eSettings.MovieUseFrodo Or Master.eSettings.MovieUseNMJ Or Master.eSettings.MovieUseYAMJ) Then
            Master.eSettings.MovieUseFrodo = True
            Master.eSettings.MovieActorThumbsFrodo = True
            Master.eSettings.MovieExtrafanartsFrodo = True
            Master.eSettings.MovieExtrathumbsFrodo = True
            Master.eSettings.MovieFanartFrodo = True
            Master.eSettings.MovieNFOFrodo = True
            Master.eSettings.MoviePosterFrodo = True
            Master.eSettings.MovieTrailerFrodo = True
        End If

        'Fix added to avoid to have no tv show NFO saved
        If Not (Master.eSettings.TVUseBoxee OrElse Master.eSettings.TVUseExpert OrElse Master.eSettings.TVUseFrodo OrElse Master.eSettings.TVUseYAMJ) Then
            Master.eSettings.TVUseFrodo = True
            Master.eSettings.TVEpisodeActorThumbsFrodo = True
            Master.eSettings.TVEpisodeNFOFrodo = True
            Master.eSettings.TVEpisodePosterFrodo = True
            Master.eSettings.TVSeasonBannerFrodo = True
            Master.eSettings.TVSeasonFanartFrodo = True
            Master.eSettings.TVSeasonPosterFrodo = True
            Master.eSettings.TVShowActorThumbsFrodo = True
            Master.eSettings.TVShowBannerFrodo = True
            Master.eSettings.TVShowExtrafanartsFrodo = True
            Master.eSettings.TVShowFanartFrodo = True
            Master.eSettings.TVShowNFOFrodo = True
            Master.eSettings.TVShowPosterFrodo = True
        End If
    End Sub

    Private Sub PatchSettings()
#Disable Warning BC40000 'The type or member is obsolete.
        Dim SettingsNeedsSave As Boolean
        Dim AdvancedSettingsNeedsSave As Boolean
        If Master.eSettings.Version < 1 Then
            'change "Settings.GeneralLanguage" from e.g. "English_(en_US)" to "en-US"
            Dim currentLanguage = Regex.Match(Master.eSettings.GeneralLanguage, "\((.*)\)")
            If currentLanguage.Success Then
                If currentLanguage.Groups(1).Value.Length = 5 Then
                    Master.eSettings.GeneralLanguage = currentLanguage.Groups(1).Value.Replace("_", "-")
                ElseIf currentLanguage.Groups(1).Value = "ch" Then
                    'change "ch" to "ch-CN"
                    Master.eSettings.GeneralLanguage = "ch-CN"
                ElseIf currentLanguage.Groups(1).Value = "cs" Then
                    'change "cs" to "cs-CZ"
                    Master.eSettings.GeneralLanguage = "cs-CZ"
                Else
                    Master.eSettings.GeneralLanguage = String.Format("{0}-{1}", currentLanguage.Groups(1).Value, currentLanguage.Groups(1).Value.ToUpper)
                End If
            ElseIf Not Master.eSettings.GeneralLanguage.Length = 5 Then
                Master.eSettings.GeneralLanguage = GeneralLanguage
            End If
            'copy "Settings.MovieScraperCertLang" value to "Settings.MovieScraperCertCountry" and change it to upper case
            'If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperCertLang) Then
            '    Master.eSettings.MovieScraperCertCountry = Master.eSettings.MovieScraperCertLang.ToUpper
            '    Master.eSettings.MovieScraperCertLang = String.Empty
            'End If
            'copy "Settings.TVScraperShowCertLang" value to "Settings.TVScraperShowCertCountry" and change it to upper case
            If Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowCertLang) Then
                Master.eSettings.TVScraperShowCertCountry = Master.eSettings.TVScraperShowCertLang.ToUpper
                Master.eSettings.TVScraperShowCertLang = String.Empty
            End If
            SettingsNeedsSave = True
        ElseIf Master.eSettings.Version < 2 Then
            'load advancedsettings.xml
            Dim OldAdvancedSettings = New XmlAdvancedSettings(Path.Combine(Master.SettingsPath, "advancedsettings.xml"))
            OldAdvancedSettings.Load()

            'move "AdvancedSettings.MovieSetTitleRenamer" to "Settings.MoviesetTitleRenaming
            While OldAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("MovieSetTitleRenamer:")).Count > 0
                Master.eSettings.MoviesetTitleRenaming.Add(New SimpleMapping With {
                                                           .Input = OldAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("MovieSetTitleRenamer:"))(0).Name.Substring(21),
                                                           .MappedTo = OldAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("MovieSetTitleRenamer:"))(0).Value
                                                           })
                OldAdvancedSettings.RemoveStringSetting(OldAdvancedSettings.Settings.Where(Function(f) f.Name.StartsWith("MovieSetTitleRenamer:"))(0).Name, "*EmberAPP")
                AdvancedSettingsNeedsSave = True
                SettingsNeedsSave = True
            End While

            If AdvancedSettingsNeedsSave Then
                FileUtils.Common.CreateFileBackup(Path.Combine(Master.SettingsPath, "advancedsettings.xml"))
                OldAdvancedSettings.Save()
            End If
        End If
        If SettingsNeedsSave Then
            FileUtils.Common.CreateFileBackup(Path.Combine(Master.SettingsPath, "settings.xml"))
            Master.eSettings.Save()
        End If
#Enable Warning BC40000 'The type or member is obsolete.
    End Sub

    Public Sub Save()
        'set settings version
        Version = _settingsversion
        Dim xmlWriter As StreamWriter = Nothing
        Try
            xmlWriter = New StreamWriter(Path.Combine(Master.SettingsPath, "settings.xml"))
            Dim xmlSerializer As New XmlSerializer(GetType(Settings))
            xmlSerializer.Serialize(xmlWriter, Master.eSettings)
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        If xmlWriter IsNot Nothing Then xmlWriter.Close()
    End Sub

    Public Sub SetDefaultsForLists(ByVal type As Enums.DefaultType, ByVal force As Boolean)

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.MainTabSorting) AndAlso (force OrElse Master.eSettings.GeneralMainTabSorting.Count = 0) Then
            Master.eSettings.GeneralMainTabSorting.Clear()
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.Movie, .DefaultList = "movielist", .Order = 0, .Title = Master.eLang.GetString(36, "Movies")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.Movieset, .DefaultList = "setslist", .Order = 1, .Title = Master.eLang.GetString(366, "Sets")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.TV, .DefaultList = "tvshowlist", .Order = 2, .Title = Master.eLang.GetString(653, "TV Shows")})
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.SortTokens) AndAlso force Then Master.eSettings.Options.Global.SortTokens = Master.eSettings.Options.Global.SortTokens.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleBlackList_TVSeason) AndAlso force Then Master.eSettings.TVScraperSeasonTitleBlacklist = Master.eSettings.TVScraperSeasonTitleBlacklist.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_Movie) AndAlso force Then Master.eSettings.MovieFilterCustom = Master.eSettings.MovieFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_TVEpisode) AndAlso force Then Master.eSettings.TVEpisodeFilterCustom = Master.eSettings.TVEpisodeFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_TVShow) AndAlso force Then Master.eSettings.TVShowFilterCustom = Master.eSettings.TVShowFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidSubtitleExts) AndAlso force Then Master.eSettings.Options.FileSystem.ValidSubtitleExtensions = Master.eSettings.Options.FileSystem.ValidSubtitleExtensions.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidThemeExts) AndAlso force Then Master.eSettings.Options.FileSystem.ValidThemeExtensions = Master.eSettings.Options.FileSystem.ValidThemeExtensions.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidVideoExts) AndAlso force Then Master.eSettings.Options.FileSystem.ValidVideoExtensions = Master.eSettings.Options.FileSystem.ValidVideoExtensions.GetDefaults

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TVShowMatching) AndAlso (force OrElse Master.eSettings.TVShowMatching.Count <= 0) Then
            Master.eSettings.TVShowMatching.Clear()
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 0, .ByDate = False, .DefaultSeason = -2, .Regexp = "s([0-9]+)[ ._-]*e([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 1, .ByDate = False, .DefaultSeason = 1, .Regexp = "[\\._ -]()e(?:p[ ._-]?)?([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 2, .ByDate = True, .DefaultSeason = -2, .Regexp = "([0-9]{4})[.-]([0-9]{2})[.-]([0-9]{2})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 3, .ByDate = True, .DefaultSeason = -2, .Regexp = "([0-9]{2})[.-]([0-9]{2})[.-]([0-9]{4})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 4, .ByDate = False, .DefaultSeason = -2, .Regexp = "[\\\/._ \[\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 5, .ByDate = False, .DefaultSeason = -2, .Regexp = "[\\\/._ -]([0-9]+)([0-9][0-9](?:(?:[a-i]|\.[1-9])(?![0-9]))?)([._ -][^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.Id = 6, .ByDate = False, .DefaultSeason = 1, .Regexp = "[\\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\\/]*)$"})
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ListSorting_Movie) AndAlso (force OrElse Master.eSettings.MovieGeneralMediaListSorting.Count <= 0) Then
            Master.eSettings.MovieGeneralMediaListSorting.Clear()
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ListTitle", .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "OriginalTitle", .LabelID = 302, .LabelText = "Original Title"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "edition", .LabelID = 308, .LabelText = "Edition"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Certification", .LabelID = 722, .LabelText = "Certification"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Credits", .LabelID = 729, .LabelText = "Credits"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Country", .LabelID = 301, .LabelText = "Country"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Director", .LabelID = 62, .LabelText = "Director"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Genre", .LabelID = 20, .LabelText = "Genre"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Language", .LabelID = 610, .LabelText = "Language"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "MPAA", .LabelID = 401, .LabelText = "MPAA"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "premiered", .LabelID = 724, .LabelText = "Premiered"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Runtime", .LabelID = 238, .LabelText = "Runtime"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Studio", .LabelID = 395, .LabelText = "Studio"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "VideoSource", .LabelID = 824, .LabelText = "Video Source"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Year", .LabelID = 278, .LabelText = "Year"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Imdb", .LabelID = 61, .LabelText = "IMDB ID"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "TMDB", .LabelID = 933, .LabelText = "TMDB ID"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Top250", .LabelID = 591, .LabelText = "Top 250"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Rating", .LabelID = 400, .LabelText = "Rating"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "iUserRating", .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "NfoPath", .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "BannerPath", .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearArtPath", .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearLogoPath", .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "DiscArtPath", .LabelID = 1098, .LabelText = "DiscArt"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "EFanartsPath", .LabelID = 992, .LabelText = "Extrafanarts"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "EThumbsPath", .LabelID = 153, .LabelText = "Extrathumbs"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "FanartPath", .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "KeyartPath", .LabelID = 1237, .LabelText = "Keyart"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "LandscapePath", .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "PosterPath", .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "HasSub", .LabelID = 152, .LabelText = "Subtitles"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ThemePath", .LabelID = 1118, .LabelText = "Theme"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "TrailerPath", .LabelID = 151, .LabelText = "Trailer"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "HasSet", .LabelID = 1295, .LabelText = "Part of a MovieSet"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "iLastPlayed", .LabelID = 981, .LabelText = "Watched"})
            Dim c As Integer = 0
            For Each n In Master.eSettings.MovieGeneralMediaListSorting
                n.DisplayIndex = c
                c += 1
            Next
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ListSorting_Movieset) AndAlso (force OrElse Master.eSettings.MovieSetGeneralMediaListSorting.Count <= 0) Then
            Master.eSettings.MovieSetGeneralMediaListSorting.Clear()
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ListTitle", .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "Language", .LabelID = 610, .LabelText = "Language"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = True, .Column = "TMDBColID", .LabelID = 1135, .LabelText = "Collection ID"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "NfoPath", .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "BannerPath", .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearArtPath", .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearLogoPath", .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "DiscArtPath", .LabelID = 1098, .LabelText = "DiscArt"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "FanartPath", .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "KeyartPath", .LabelID = 1237, .LabelText = "Keyart"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "LandscapePath", .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.Hide = False, .Column = "PosterPath", .LabelID = 148, .LabelText = "Poster"})
            Dim c As Integer = 0
            For Each n In Master.eSettings.MovieSetGeneralMediaListSorting
                n.DisplayIndex = c
                c += 1
            Next
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ListSorting_TVEpisode) AndAlso (force OrElse Master.eSettings.TVGeneralEpisodeListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralEpisodeListSorting.Clear()
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "Title", .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "Aired", .LabelID = 728, .LabelText = "Aired"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "Credits", .LabelID = 729, .LabelText = "Credits"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "Director", .LabelID = 62, .LabelText = "Director"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "Runtime", .LabelID = 238, .LabelText = "Runtime"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "VideoSource", .LabelID = 824, .LabelText = "Video Source"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "strIMDB", .LabelID = 61, .LabelText = "IMDB ID"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "strTMDB", .LabelID = 933, .LabelText = "TMDB ID"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "strTVDB", .LabelID = 941, .LabelText = "TVDB ID"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "Rating", .LabelID = 400, .LabelText = "Rating"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "iUserRating", .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "NfoPath", .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = True, .Column = "FanartPath", .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "PosterPath", .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "HasSub", .LabelID = 152, .LabelText = "Subtitles"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.Hide = False, .Column = "Playcount", .LabelID = 981, .LabelText = "Watched"})
            Dim c As Integer = 0
            For Each n In Master.eSettings.TVGeneralEpisodeListSorting
                n.DisplayIndex = c
                c += 1
            Next
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ListSorting_TVSeason) AndAlso (force OrElse Master.eSettings.TVGeneralSeasonListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralSeasonListSorting.Clear()
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "Title", .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "strAired", .LabelID = 728, .LabelText = "Aired"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = True, .Column = "strTMDB", .LabelID = 933, .LabelText = "TMDB ID"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = True, .Column = "strTVDB", .LabelID = 941, .LabelText = "TVDB ID"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = True, .Column = "Episodes", .LabelID = 682, .LabelText = "Episodes"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "BannerPath", .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "FanartPath", .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "LandscapePath", .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "PosterPath", .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.Hide = False, .Column = "HasWatched", .LabelID = 981, .LabelText = "Watched"})
            Dim c As Integer = 0
            For Each n In Master.eSettings.TVGeneralSeasonListSorting
                n.DisplayIndex = c
                c += 1
            Next
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ListSorting_TVShow) AndAlso (force OrElse Master.eSettings.TVGeneralShowListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralShowListSorting.Clear()
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "ListTitle", .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "strOriginalTitle", .LabelID = 302, .LabelText = "Original Title"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Country", .LabelID = 301, .LabelText = "Country"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Creator", .LabelID = 744, .LabelText = "Creator"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Genre", .LabelID = 20, .LabelText = "Genre"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Language", .LabelID = 610, .LabelText = "Language"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "MPAA", .LabelID = 401, .LabelText = "MPAA"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Premiered", .LabelID = 724, .LabelText = "Premiered"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Runtime", .LabelID = 238, .LabelText = "Runtime"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Status", .LabelID = 215, .LabelText = "Status"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Studio", .LabelID = 395, .LabelText = "Studio"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "strIMDB", .LabelID = 61, .LabelText = "IMDB ID"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "strTMDB", .LabelID = 933, .LabelText = "TMDB ID"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "TVDB", .LabelID = 941, .LabelText = "TVDB ID"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Episodes", .LabelID = 682, .LabelText = "Episodes"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "Rating", .LabelID = 400, .LabelText = "Rating"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = True, .Column = "iUserRating", .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "NfoPath", .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "BannerPath", .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "CharacterArtPath", .LabelID = 1140, .LabelText = "CharacterArt"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearArtPath", .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "ClearLogoPath", .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "EFanartsPath", .LabelID = 992, .LabelText = "Extrafanarts"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "FanartPath", .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "KeyartPath", .LabelID = 1237, .LabelText = "Keyart"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "LandscapePath", .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "PosterPath", .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "ThemePath", .LabelID = 1118, .LabelText = "Theme"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.Hide = False, .Column = "HasWatched", .LabelID = 981, .LabelText = "Watched"})
            Dim c As Integer = 0
            For Each n In Master.eSettings.TVGeneralShowListSorting
                n.DisplayIndex = c
                c += 1
            Next
        End If
    End Sub

    Public Function GetMoviesetsArtworkPaths() As List(Of String)
        Dim Paths As New List(Of String)
        If Not String.IsNullOrEmpty(MovieSetPathExpertSingle) Then Paths.Add(MovieSetPathExpertSingle)
        If Not String.IsNullOrEmpty(MovieSetPathExtended) Then Paths.Add(MovieSetPathExtended)
        If Not String.IsNullOrEmpty(MovieSetPathMSAA) Then Paths.Add(MovieSetPathMSAA)
        Paths = Paths.Distinct().ToList() 'remove double entries
        Return Paths
    End Function

    Public Function MovieActorthumbsAnyEnabled() As Boolean
        Return MovieActorThumbsEden OrElse MovieActorThumbsFrodo OrElse
            (MovieUseExpert AndAlso ((MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertBDMV)) OrElse (MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertMulti)) OrElse (MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertSingle)) OrElse (MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertVTS))))
    End Function

    Public Function MovieBannerAnyEnabled() As Boolean
        Return MovieBannerAD OrElse MovieBannerExtended OrElse MovieBannerNMJ OrElse MovieBannerYAMJ OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieBannerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieBannerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieBannerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieBannerExpertVTS)))
    End Function

    Public Function MovieClearArtAnyEnabled() As Boolean
        Return MovieClearArtAD OrElse MovieClearArtExtended OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertVTS)))
    End Function

    Public Function MovieClearLogoAnyEnabled() As Boolean
        Return MovieClearLogoAD OrElse MovieClearLogoExtended OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearLogoExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertVTS)))
    End Function

    Public Function MovieDiscArtAnyEnabled() As Boolean
        Return MovieDiscArtAD OrElse MovieDiscArtExtended OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieDiscArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertVTS)))
    End Function

    Public Function MovieExtrafanartsAnyEnabled() As Boolean
        Return MovieExtrafanartsEden OrElse MovieExtrafanartsFrodo OrElse
            (MovieUseExpert AndAlso (MovieExtrafanartsExpertBDMV OrElse MovieExtrafanartsExpertSingle OrElse MovieExtrafanartsExpertVTS))
    End Function

    Public Function MovieExtrathumbsAnyEnabled() As Boolean
        Return MovieExtrathumbsEden OrElse MovieExtrathumbsFrodo OrElse
            (MovieUseExpert AndAlso (MovieExtrathumbsExpertBDMV OrElse MovieExtrathumbsExpertSingle OrElse MovieExtrathumbsExpertVTS))
    End Function

    Public Function MovieFanartAnyEnabled() As Boolean
        Return MovieFanartBoxee OrElse MovieFanartEden OrElse MovieFanartFrodo OrElse MovieFanartNMJ OrElse MovieFanartYAMJ OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieFanartExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieFanartExpertMulti) OrElse Not String.IsNullOrEmpty(MovieFanartExpertSingle) OrElse Not String.IsNullOrEmpty(MovieFanartExpertVTS)))
    End Function

    Public Function MovieKeyartAnyEnabled() As Boolean
        Return MovieKeyartExtended OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieKeyartExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieKeyartExpertMulti) OrElse Not String.IsNullOrEmpty(MovieKeyartExpertSingle) OrElse Not String.IsNullOrEmpty(MovieKeyartExpertVTS)))
    End Function

    Public Function MovieLandscapeAnyEnabled() As Boolean
        Return MovieLandscapeAD OrElse MovieLandscapeExtended OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieLandscapeExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertMulti) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertSingle) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertVTS)))
    End Function

    Public Function MovieMissingItemsAnyEnabled() As Boolean
        Return MovieMissingBanner OrElse MovieMissingClearArt OrElse MovieMissingClearLogo OrElse MovieMissingDiscArt OrElse MovieMissingExtrafanarts OrElse
            MovieMissingExtrathumbs OrElse MovieMissingFanart OrElse MovieMissingLandscape OrElse MovieMissingNFO OrElse MovieMissingPoster OrElse
            MovieMissingSubtitles OrElse MovieMissingTheme OrElse MovieMissingTrailer
    End Function

    Public Function MovieNFOAnyEnabled() As Boolean
        Return MovieNFOBoxee OrElse MovieNFOEden OrElse MovieNFOFrodo OrElse MovieNFONMJ OrElse MovieNFOYAMJ OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieNFOExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieNFOExpertMulti) OrElse Not String.IsNullOrEmpty(MovieNFOExpertSingle) OrElse Not String.IsNullOrEmpty(MovieNFOExpertVTS)))
    End Function

    Public Function MoviePosterAnyEnabled() As Boolean
        Return MoviePosterBoxee OrElse MoviePosterEden OrElse MoviePosterFrodo OrElse MoviePosterNMJ OrElse MoviePosterYAMJ OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MoviePosterExpertBDMV) OrElse Not String.IsNullOrEmpty(MoviePosterExpertMulti) OrElse Not String.IsNullOrEmpty(MoviePosterExpertSingle) OrElse Not String.IsNullOrEmpty(MoviePosterExpertVTS)))
    End Function

    Public Function MovieThemeAnyEnabled() As Boolean
        Return MovieThemeTvTunesEnable AndAlso (MovieThemeTvTunesMoviePath OrElse (MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(MovieThemeTvTunesCustomPath) OrElse (MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(MovieThemeTvTunesSubDir))))
    End Function

    Public Function MovieTrailerAnyEnabled() As Boolean
        Return MovieTrailerEden OrElse MovieTrailerFrodo OrElse MovieTrailerNMJ OrElse MovieTrailerYAMJ OrElse
            (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieTrailerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertVTS)))
    End Function

    Public Function MovieSetBannerAnyEnabled() As Boolean
        Return (MovieSetBannerExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetBannerMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetBannerMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetPosterExpertSingle))
    End Function

    Public Function MovieSetClearArtAnyEnabled() As Boolean
        Return (MovieSetClearArtExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetClearArtMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetClearArtMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetClearArtExpertSingle))
    End Function

    Public Function MovieSetClearLogoAnyEnabled() As Boolean
        Return (MovieSetClearLogoExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetClearLogoMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetClearLogoMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetClearLogoExpertSingle))
    End Function

    Public Function MovieSetDiscArtAnyEnabled() As Boolean
        Return (MovieSetDiscArtExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetDiscArtMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetDiscArtExpertSingle))
    End Function

    Public Function MovieSetFanartAnyEnabled() As Boolean
        Return (MovieSetFanartExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetFanartMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetFanartMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetFanartExpertSingle))
    End Function

    Public Function MovieSetKeyartAnyEnabled() As Boolean
        Return (MovieSetKeyartExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetKeyartMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetKeyartExpertSingle))
    End Function

    Public Function MovieSetLandscapeAnyEnabled() As Boolean
        Return (MovieSetLandscapeExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetLandscapeMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetLandscapeMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetLandscapeExpertSingle))
    End Function

    Public Function MovieSetMissingItemsAnyEnabled() As Boolean
        Return MovieSetMissingBanner OrElse MovieSetMissingClearArt OrElse MovieSetMissingClearLogo OrElse MovieSetMissingDiscArt OrElse
            MovieSetMissingFanart OrElse MovieSetMissingLandscape OrElse MovieSetMissingNFO OrElse MovieSetMissingPoster
    End Function

    Public Function MovieSetNFOAnyEnabled() As Boolean
        Return (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetNFOExpertSingle))
    End Function

    Public Function MovieSetPosterAnyEnabled() As Boolean
        Return (MovieSetPosterExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
            (MovieSetPosterMatrix AndAlso Not String.IsNullOrEmpty(MovieSetPathMatrix)) OrElse
            (MovieSetPosterMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
            (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetPosterExpertSingle))
    End Function

    Public Function TVAllSeasonsAnyEnabled() As Boolean
        Return TVAllSeasonsBannerAnyEnabled() OrElse TVAllSeasonsFanartAnyEnabled() OrElse TVAllSeasonsLandscapeAnyEnabled() OrElse TVAllSeasonsPosterAnyEnabled()
    End Function

    Public Function TVAllSeasonsBannerAnyEnabled() As Boolean
        Return TVSeasonBannerFrodo OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsBannerExpert))
    End Function

    Public Function TVAllSeasonsFanartAnyEnabled() As Boolean
        Return TVSeasonFanartFrodo OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsFanartExpert))
    End Function

    Public Function TVAllSeasonsLandscapeAnyEnabled() As Boolean
        Return TVSeasonLandscapeAD OrElse TVSeasonLandscapeExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsLandscapeExpert))
    End Function

    Public Function TVAllSeasonsPosterAnyEnabled() As Boolean
        Return TVSeasonPosterFrodo OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsPosterExpert))
    End Function

    Public Function TVEpisodeActorThumbsAnyEnabled() As Boolean
        Return TVEpisodeActorThumbsFrodo OrElse
            (TVUseExpert AndAlso TVEpisodeActorThumbsExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeActorThumbsExtExpert))
    End Function

    Public Function TVEpisodeFanartAnyEnabled() As Boolean
        Return (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeFanartExpert))
    End Function

    Public Function TVEpisodeNFOAnyEnabled() As Boolean
        Return TVEpisodeNFOBoxee OrElse TVEpisodeNFOFrodo OrElse TVEpisodeNFOYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeNFOExpert))
    End Function

    Public Function TVEpisodePosterAnyEnabled() As Boolean
        Return TVEpisodePosterBoxee OrElse TVEpisodePosterFrodo OrElse TVEpisodePosterYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodePosterExpert))
    End Function

    Public Function TVSeasonBannerAnyEnabled() As Boolean
        Return TVSeasonBannerFrodo OrElse TVSeasonBannerYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonBannerExpert))
    End Function

    Public Function TVSeasonFanartAnyEnabled() As Boolean
        Return TVSeasonFanartFrodo OrElse TVSeasonFanartYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonFanartExpert))
    End Function

    Public Function TVSeasonLandscapeAnyEnabled() As Boolean
        Return TVSeasonLandscapeAD OrElse TVSeasonLandscapeExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonLandscapeExpert))
    End Function

    Public Function TVSeasonPosterAnyEnabled() As Boolean
        Return TVSeasonPosterBoxee OrElse TVSeasonPosterFrodo OrElse TVSeasonPosterYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonPosterExpert))
    End Function

    Public Function TVShowActorThumbsAnyEnabled() As Boolean
        Return TVShowActorThumbsFrodo OrElse
            (TVUseExpert AndAlso TVShowActorThumbsExpert AndAlso Not String.IsNullOrEmpty(TVShowActorThumbsExtExpert))
    End Function

    Public Function TVShowBannerAnyEnabled() As Boolean
        Return TVShowBannerBoxee OrElse TVShowBannerFrodo OrElse TVShowBannerYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowBannerExpert))
    End Function

    Public Function TVShowCharacterArtAnyEnabled() As Boolean
        Return TVShowCharacterArtAD OrElse TVShowCharacterArtExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowCharacterArtExpert))
    End Function

    Public Function TVShowClearArtAnyEnabled() As Boolean
        Return TVShowClearArtAD OrElse TVShowClearArtExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowClearArtExpert))
    End Function

    Public Function TVShowClearLogoAnyEnabled() As Boolean
        Return TVShowClearLogoAD OrElse TVShowClearLogoExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowClearLogoExpert))
    End Function

    Public Function TVShowExtrafanartsAnyEnabled() As Boolean
        Return TVShowExtrafanartsFrodo OrElse
            (TVUseExpert AndAlso TVShowExtrafanartsExpert)
    End Function

    Public Function TVShowFanartAnyEnabled() As Boolean
        Return TVShowFanartBoxee OrElse TVShowFanartFrodo OrElse TVShowFanartYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowFanartExpert))
    End Function

    Public Function TVShowKeyartAnyEnabled() As Boolean
        Return TVShowKeyartExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowKeyartExpert))
    End Function

    Public Function TVShowLandscapeAnyEnabled() As Boolean
        Return TVShowLandscapeAD OrElse TVShowLandscapeExtended OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowLandscapeExpert))
    End Function

    Public Function TVShowMissingItemsAnyEnabled() As Boolean
        Return TVShowMissingBanner OrElse TVShowMissingCharacterArt OrElse TVShowMissingClearArt OrElse TVShowMissingClearLogo OrElse
            TVShowMissingExtrafanarts OrElse TVShowMissingFanart OrElse TVShowMissingLandscape OrElse TVShowMissingNFO OrElse
            TVShowMissingPoster OrElse TVShowMissingTheme
    End Function

    Public Function TVShowNFOAnyEnabled() As Boolean
        Return TVShowNFOBoxee OrElse TVShowNFOFrodo OrElse TVShowNFOYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowNFOExpert))
    End Function

    Public Function TVShowPosterAnyEnabled() As Boolean
        Return TVShowPosterBoxee OrElse TVShowPosterFrodo OrElse TVShowPosterYAMJ OrElse
            (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowPosterExpert))
    End Function

    Public Function TvShowThemeAnyEnabled() As Boolean
        Return TVShowThemeTvTunesEnable AndAlso (TVShowThemeTvTunesShowPath OrElse (TVShowThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(TVShowThemeTvTunesCustomPath) OrElse (TVShowThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(TVShowThemeTvTunesSubDir))))
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class Helpers

        Public Shared Function GetImageSettings(ByVal contentType As Enums.ContentType) As ImageBase
            Select Case contentType
                Case Enums.ContentType.Movie
                    Return Master.eSettings.Movie.ImageSettings
                Case Enums.ContentType.Movieset
                    Return Master.eSettings.Movieset.ImageSettings
                Case Enums.ContentType.TVEpisode
                    Return Master.eSettings.TVEpisode.ImageSettings
                Case Enums.ContentType.TVSeason
                    Return Master.eSettings.TVSeason.ImageSettings
                Case Enums.ContentType.TVShow
                    Return Master.eSettings.TVShow.ImageSettings
            End Select
            Return Nothing
        End Function

        Public Shared Function GetImageSpecificationItem(ByVal contentType As Enums.ContentType, imageType As Enums.ModifierType) As ImageItemBase
            Dim ImageSettings = GetImageSettings(contentType)
            Select Case imageType
                'TODO: AllSeasons settings
                'TODO: combine modtypes
                Case Enums.ModifierType.AllSeasonsBanner
                    Return ImageSettings.Banner
                Case Enums.ModifierType.AllSeasonsFanart
                    Return ImageSettings.Fanart
                Case Enums.ModifierType.AllSeasonsLandscape
                    Return ImageSettings.Landscape
                Case Enums.ModifierType.AllSeasonsPoster
                    Return ImageSettings.Poster
                Case Enums.ModifierType.EpisodeActorThumbs
                    Return ImageSettings.Actorthumbs
                Case Enums.ModifierType.EpisodeFanart
                    Return ImageSettings.Fanart
                Case Enums.ModifierType.EpisodePoster
                    Return ImageSettings.Poster
                Case Enums.ModifierType.MainActorThumbs
                    Return ImageSettings.Actorthumbs
                Case Enums.ModifierType.MainBanner
                    Return ImageSettings.Banner
                Case Enums.ModifierType.MainCharacterArt
                    Return ImageSettings.Characterart
                Case Enums.ModifierType.MainClearArt
                    Return ImageSettings.Clearart
                Case Enums.ModifierType.MainClearLogo
                    Return ImageSettings.Clearlogo
                Case Enums.ModifierType.MainDiscArt
                    Return ImageSettings.Discart
                Case Enums.ModifierType.MainExtrafanarts
                    Return ImageSettings.Extrafanarts
                Case Enums.ModifierType.MainExtrathumbs
                    Return ImageSettings.Extrathumbs
                Case Enums.ModifierType.MainFanart
                    Return ImageSettings.Fanart
                Case Enums.ModifierType.MainKeyart
                    Return ImageSettings.Keyart
                Case Enums.ModifierType.MainLandscape
                    Return ImageSettings.Landscape
                Case Enums.ModifierType.MainPoster
                    Return ImageSettings.Poster
                Case Enums.ModifierType.SeasonBanner
                    Return ImageSettings.Banner
                Case Enums.ModifierType.SeasonFanart
                    Return ImageSettings.Fanart
                Case Enums.ModifierType.SeasonLandscape
                    Return ImageSettings.Landscape
                Case Enums.ModifierType.SeasonPoster
                    Return ImageSettings.Poster
            End Select
            Return Nothing
        End Function
    End Class

    Public Class MainTabSorting

#Region "Properties"

        Public Property ContentType As Enums.ContentType = Enums.ContentType.None

        Public Property DefaultList As String = String.Empty

        Public Property Order As Integer = -1

        Public Property Title As String = String.Empty

#End Region 'Properties

    End Class

    Public Class MetadataPerType

#Region "Properties"

        Public Property FileType As String = String.Empty

        Public Property MetaData As MediaContainers.Fileinfo = New MediaContainers.Fileinfo

#End Region 'Properties

    End Class

    Public Class ListSorting

#Region "Properties"
        ''' <summary>
        ''' Column name in database (need to be exactly like column name in DB)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Column As String = String.Empty

        Public Property DisplayIndex As Integer = -1
        ''' <summary>
        ''' Hide or show column in media lists
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Hide As Boolean = False
        ''' <summary>
        ''' ID of string in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelID As UInteger = 1
        ''' <summary>
        ''' Default text for the LabelID in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelText As String = String.Empty

#End Region 'Properties

    End Class

    Public Class regexp

#Region "Properties"

        <XmlElement("byDate")>
        Public Property ByDate As Boolean = False

        <XmlElement("defaultSeason")>
        Public Property DefaultSeason As Integer = -2 '-1 is reserved for "* All Seasons" entry 

        <XmlElement("ID")>
        Public Property Id As Integer = -1

        Public Property Regexp As String = String.Empty

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class

<Serializable()>
Public Class AVCodecMapping

#Region "Properties"

    Public Property Audio As New List(Of CodecMapping)

    <XmlIgnore>
    Public ReadOnly Property AudioSpecified As Boolean
        Get
            Return Audio.Count > 0
        End Get
    End Property

    Public Property Video As New List(Of CodecMapping)

    <XmlIgnore>
    Public ReadOnly Property VideoSpecified As Boolean
        Get
            Return Video.Count > 0
        End Get
    End Property


#End Region 'Properties

#Region "Methods"

    Public Function GetDefaults(ByVal type As Enums.DefaultType) As List(Of CodecMapping)
        Select Case type
            Case Enums.DefaultType.AudioCodecMapping
                Return New List(Of CodecMapping) From {
                        New CodecMapping With {.Codec = "aac lc", .Mapping = "aac", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "ac-3 mlp fba 16-ch", .Mapping = "truehd", .AdditionalFeatures = "atmos"},
                        New CodecMapping With {.Codec = "ac-3", .Mapping = "ac3", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "dts xbr", .Mapping = "dtshd_hra", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "dts xll x", .Mapping = "dtshd_ma", .AdditionalFeatures = "x"},
                        New CodecMapping With {.Codec = "dts xll", .Mapping = "dtshd_ma", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "dts", .Mapping = "dca", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "e-ac-3", .Mapping = "eac3", .AdditionalFeatures = ""},
                        New CodecMapping With {.Codec = "mlp fba", .Mapping = "truehd", .AdditionalFeatures = ""}
                    }
            Case Enums.DefaultType.VideoCodecMapping
                Return New List(Of CodecMapping) From {
                        New CodecMapping With {.Codec = "avc", .Mapping = "h264"},
                        New CodecMapping With {.Codec = "hvc1", .Mapping = "hevc"},
                        New CodecMapping With {.Codec = "v_mpeg2", .Mapping = "h264"},
                        New CodecMapping With {.Codec = "v_mpegh/iso/hevc", .Mapping = "hevc"},
                        New CodecMapping With {.Codec = "v_ms/vfw/fourcc / dx50", .Mapping = "dx50"},
                        New CodecMapping With {.Codec = "v_ms/vfw/fourcc / xvid", .Mapping = "xvid"},
                        New CodecMapping With {.Codec = "v_vp7", .Mapping = "vp7"},
                        New CodecMapping With {.Codec = "v_vp8", .Mapping = "vp8"},
                        New CodecMapping With {.Codec = "v_vp9", .Mapping = "vp9"},
                        New CodecMapping With {.Codec = "x264", .Mapping = "h264"}
                    }
        End Select
        Return New List(Of CodecMapping)
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class CodecMapping

#Region "Properties"

        <XmlAttribute>
        Public Property Codec As String = String.Empty

        Public Property Mapping As String = String.Empty

        Public Property AdditionalFeatures As String = String.Empty

        <XmlIgnore>
        Public ReadOnly Property AdditionalFeaturesSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(AdditionalFeatures)
            End Get
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class


<Serializable()>
Public Class ConnectionBase

#Region "Properties"

    Public Property ProxyCredentials As New NetworkCredential

    Public Property ProxyPort As Integer = 0

    <XmlIgnore>
    Public ReadOnly Property ProxyPortSpecified As Boolean
        Get
            Return ProxyPort > 0
        End Get
    End Property

    Public Property ProxyURI As String = String.Empty

    <XmlIgnore>
    Public ReadOnly Property ProxyURISpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(ProxyURI)
        End Get
    End Property

#End Region 'Properties 

End Class

<Serializable()>
Public Class FilesystemBase

#Region "Properties"
    <XmlIgnore>
    Public Property ValidSubtitleExtensions() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidSubtitleExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("ValidSubtitleExtensions")>
    Public Property ValidSubtitleExtensionsXml As String()
        Get
            Return ValidSubtitleExtensions.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                ValidSubtitleExtensions.Clear()
                ValidSubtitleExtensions.AddRange(value.ToList)
            End If
        End Set
    End Property
    <XmlIgnore>
    Public ReadOnly Property ValidSubtitleExtensionsSpecified As Boolean
        Get
            Return ValidSubtitleExtensions.Count > 0
        End Get
    End Property
    <XmlIgnore>
    Public Property ValidThemeExtensions() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidThemeExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("ValidThemeExtensions")>
    Public Property ValidThemeExtensionsXml As String()
        Get
            Return ValidThemeExtensions.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                ValidThemeExtensions.Clear()
                ValidThemeExtensions.AddRange(value.ToList)
            End If
        End Set
    End Property
    <XmlIgnore>
    Public ReadOnly Property ValidThemeExtensionsSpecified As Boolean
        Get
            Return ValidSubtitleExtensions.Count > 0
        End Get
    End Property
    <XmlIgnore>
    Public Property ValidVideoExtensions() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidVideoExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("ValidVideoExtensions")>
    Public Property ValidVideoExtensionsXml As String()
        Get
            Return ValidVideoExtensions.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                ValidVideoExtensions.Clear()
                ValidVideoExtensions.AddRange(value.ToList)
            End If
        End Set
    End Property
    <XmlIgnore>
    Public ReadOnly Property ValidVideoExtensionsSpecified As Boolean
        Get
            Return ValidSubtitleExtensions.Count > 0
        End Get
    End Property

    Public Property VirtualDriveBinPath As String = String.Empty

    Public Property VirtualDriveDriveLetter As String = String.Empty

#End Region 'Properties

End Class


<Serializable()>
Public Class GlobalBase

#Region "Properties"

    Public Property CheckForUpdates As Boolean = False

    Public Property DigitGrpSymbolVotesEnabled As Boolean = False

    Public Property Language As String = "English_(en_US)"

    Public Property ShowNews As Boolean = True

    <XmlIgnore>
    Public Property SortTokens As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.SortTokens).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("SortTokens")>
    Public Property SortTokensXml As String()
        Get
            Return SortTokens.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                SortTokens.Clear()
                SortTokens.AddRange(value.ToList)
            End If
        End Set
    End Property

    <XmlIgnore>
    Public ReadOnly Property SortTokensSpecified As Boolean
        Get
            Return SortTokens.Count > 0
        End Get
    End Property

#End Region 'Properties

End Class

<Serializable()>
Public Class MovieBase

#Region "Properties"

    Public Property Filenaming As New FilenameBase

    Public Property ImageSettings As ImageBase = New ImageBase

    Public Property InformationSettings As InformationBase = New InformationBase With {
        .Ratings = New InformationItem_Rating With {.DefaultType = "imdb"},
        .UniqueId = New UniqueIds With {.DefaultType = "imdb"}
    }

    Public Property SourceSettings As New SourceBase

    Public Property ThemeSettings As New ThemeBase

    Public Property TrailerSettings As New TrailerBase

#End Region 'Properties

End Class

<Serializable()>
Public Class MoviesetBase

#Region "Properties"

    Public Property Filenaming As New FilenameBase

    Public Property ImageSettings As New ImageBase

    Public Property InformationSettings As New InformationBase

    Public Property SourceSettings As New SourceBase

#End Region 'Properties

End Class

<Serializable()>
Public Class OptionsBase

#Region "Properties"

    Public Property AVCodecMapping As New AVCodecMapping

    <XmlElement("connection")>
    Public Property Connection As New ConnectionBase

    <XmlElement("filesystem")>
    Public Property FileSystem As New FilesystemBase

    'Public Property GUI As GUISettings

    <XmlElement("global")>
    Public Property [Global] As New GlobalBase

    Public Property VideoSourceMapping As New VideoSourceMappingBase

#End Region 'Properties

End Class

<Serializable()>
Public Class EpisodeMatchingSpecification
    Inherits List(Of EpisodeMatchingSpecificationItem)

#Region "Methods"

    Public Function GetDefaults(ByVal type As Enums.ContentType) As EpisodeMatchingSpecification
        Select Case type
            Case Enums.ContentType.TVEpisode
                Return New EpisodeMatchingSpecification From {
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = -1, .RegExp = "s([0-9]+)[ ._-]*e([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = 1, .RegExp = "[\\._ -]()e(?:p[ ._-]?)?([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = True, .DefaultSeason = -1, .RegExp = "([0-9]{4},[.-]([0-9]{2},[.-]([0-9]{2})"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = True, .DefaultSeason = -1, .RegExp = "([0-9]{2},[.-]([0-9]{2},[.-]([0-9]{4})"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = -1, .RegExp = "[\\\/._ \[\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = -1, .RegExp = "[\\\/._ -]([0-9]+)([0-9][0-9](?:(?:[a-i]|\.[1-9])(?![0-9]))?)([._ -][^\\\/]*)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = 1, .RegExp = "[\\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\\/]*)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = -1, .RegExp = "[Ss]([0-9]+)[ ._-]*[Ee]([0-9]+)([^\\/]*)(?:(?:[\\/]VIDEO_TS)?[\\/]VIDEO_TS\.IFO)$"},
                    New EpisodeMatchingSpecificationItem With {.ByDate = False, .DefaultSeason = -1, .RegExp = "[Ss]([0-9]+)[ ._-]*[Ee]([0-9]+)([^\\/]*)(?:(?:[\\/]bdmv)?[\\/]index\.bdmv)$"}
                }
        End Select
        Return New EpisodeMatchingSpecification
    End Function

#End Region 'Methods

End Class

<Serializable()>
Public Class EpisodeMatchingSpecificationItem

#Region "Properties"

    Public Property ByDate As Boolean = False

    Public Property DefaultSeason As Integer = -1

    <XmlIgnore>
    Public ReadOnly Property DefaultSeasonSpecified() As Boolean
        Get
            Return DefaultSeason >= 0
        End Get
    End Property

    Public Property RegExp As String = String.Empty

#End Region 'Properties

End Class

<Serializable()>
Public Class EpisodeMultipartMatchingSpecification

#Region "Properties"

    Public Property Regex As String = String.Empty

#End Region 'Properties 

#Region "Methods"

    Public Function GetDefaults(ByVal type As Enums.ContentType) As String
        Select Case type
            Case Enums.ContentType.TVEpisode
                Return "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
            Case Else
        End Select
        Return String.Empty
    End Function

#End Region 'Methods

End Class


<Serializable()>
Public Class ExtendedListOfString
    Inherits List(Of String)

#Region "Fields"

    Private ReadOnly listType As Enums.DefaultType

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal type As Enums.DefaultType)
        listType = type
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Function GetDefaults() As ExtendedListOfString
        Select Case listType
            Case Enums.DefaultType.SortTokens
                Return New ExtendedListOfString(listType) From {
                        "a\s", "an\s", "das\s", "der\s", "die\s", "the\s"
                    }
            Case Enums.DefaultType.TitleBlackList_TVSeason
                'generic season titles based on         https://app.localeapp.com/projects/8267/translations/5666383
                'generic specials season title based on https://app.localeapp.com/projects/8267/translations/5429982
                Return New ExtendedListOfString(listType) From {
                        "^%{season_number}. sezóna\z",
                        "^%{season_number}. évad\z",
                        "^%{season_number}.ª Temporada\z",
                        "^%{season_number}ª Temporada\z",
                        "^%{season_number}ος κύκλος\z",
                        "^Kausi %{season_number}\z",
                        "^Musim ke %{season_number}\z",
                        "^Saison %{season_number}\z",
                        "^Season %{season_number}\z",
                        "^Seizoen %{season_number}\z",
                        "^Series %{season_number}\z",
                        "^Sezon %{season_number}\z",
                        "^Sezonas %{season_number}\z",
                        "^Sezonul %{season_number}\z",
                        "^Staffel %{season_number}\z",
                        "^Stagione %{season_number}\z",
                        "^Säsong %{season_number}\z",
                        "^Séria %{season_number}\z",
                        "^Tempada %{season_number}\z",
                        "^Temporada %{season_number}\z",
                        "^Сезон %{season_number}\z",
                        "^Сезона %{season_number}\z",
                        "^עונה %{season_number}\z",
                        "^الموسم %{season_number}\z",
                        "^فصل %{season_number}\z",
                        "^シーズン%{season_number}\z",
                        "^第 %{season_number} 季\z",
                        "^Arnaıylar\z",
                        "^Bereziak\z",
                        "^Erikoisjaksot\z",
                        "^Especiais\z",
                        "^Especiales\z",
                        "^Extras\z",
                        "^Odcinki specjalne\z",
                        "^Speciale\z",
                        "^Speciali\z",
                        "^Specials\z",
                        "^Specialūs pasiūlymai\z",
                        "^Speciális epizódok\z",
                        "^Speciály\z",
                        "^Spéciaux\z",
                        "^Épisodes spéciaux\z",
                        "^Špeciály\z",
                        "^Σπέσιαλ επεισόδια\z",
                        "^Специални\z",
                        "^Специјали\z",
                        "^Спецматериалы\z",
                        "^Спеціальне\z",
                        "^特別篇\z",
                        "^特別編\z",
                        "^特别篇\z",
                        "^特殊貢獻\z"
                        }
            Case Enums.DefaultType.TitleFilters_Movie
                Return New ExtendedListOfString(listType) From {
                        "[\W_]\(?\d{4}\)?.*",                   'year in brakets
                        "(?i)[\W_]tt\d*",                       'IMDB ID
                        "(?i)[\W_]blu[\W_]?ray.*",
                        "(?i)[\W_]bd[\W_]?rip.*",
                        "(?i)[\W_]3d.*",
                        "(?i)[\W_]dvd.*",
                        "(?i)[\W_]720.*",
                        "(?i)[\W_]1080.*",
                        "(?i)[\W_]1440.*",
                        "(?i)[\W_]2160.*",
                        "(?i)[\W_]4k.*",
                        "(?i)[\W_]ac3.*",
                        "(?i)[\W_]dts.*",
                        "(?i)[\W_]divx.*",
                        "(?i)[\W_]xvid.*",
                        "(?i)[\W_]dc[\W_]?.*",
                        "(?i)[\W_]dir(ector'?s?)?\s?cut.*",
                        "(?i)[\W_]extended.*",
                        "(?i)[\W_]hd(tv)?.*",
                        "(?i)[\W_]unrated.*",
                        "(?i)[\W_]uncut.*",
                        "(?i)[\W_]german.*",
                        "(?i)[\W_]([a-z]{3}|multi)[sd]ub.*",
                        "(?i)[\W_]\[offline\].*",
                        "(?i)[\W_]ntsc.*",
                        "[\W_]PAL[\W_]?.*",
                        "\.[->] ",                              'convert dots to space
                        "_[->] "                                'convert underscore to space
                    }
            Case Enums.DefaultType.TitleFilters_TVEpisode
                Return New ExtendedListOfString(listType) From {
                        "[\W_]\(?\d{4}\)?.*",                   'year in brakets
                        "(?i)([\W_]+\s?)?s[0-9]+[\W_]*([-e][0-9]+)+(\])*",
                        "(?i)([\W_]+\s?)?[0-9]+([-x][0-9]+)+(\])*",
                        "(?i)([\W_]+\s?)?s(eason)?[\W_]*[0-9]+(\])*",
                        "(?i)([\W_]+\s?)?e(pisode)?[\W_]*[0-9]+(\])*",
                        "(?i)[\W_]blu[\W_]?ray.*",
                        "(?i)[\W_]bd[\W_]?rip.*",
                        "(?i)[\W_]dvd.*",
                        "(?i)[\W_]720.*",
                        "(?i)[\W_]1080.*",
                        "(?i)[\W_]1440.*",
                        "(?i)[\W_]2160.*",
                        "(?i)[\W_]4k.*",
                        "(?i)[\W_]ac3.*",
                        "(?i)[\W_]dts.*",
                        "(?i)[\W_]divx.*",
                        "(?i)[\W_]xvid.*",
                        "(?i)[\W_]dc[\W_]?.*",
                        "(?i)[\W_]dir(ector'?s?)?\s?cut.*",
                        "(?i)[\W_]extended.*",
                        "(?i)[\W_]hd(tv)?.*",
                        "(?i)[\W_]unrated.*",
                        "(?i)[\W_]uncut.*",
                        "(?i)[\W_]([a-z]{3}|multi)[sd]ub.*",
                        "(?i)[\W_]\[offline\].*",
                        "(?i)[\W_]ntsc.*",
                        "[\W_]PAL[\W_]?.*",
                        "\.[->] ",                              'convert dots to space
                        "_[->] ",                               'convert underscore to space
                        " - [->] "                              'convert space-minus-space to space
                    }
            Case Enums.DefaultType.TitleFilters_TVShow
                Return New ExtendedListOfString(listType) From {
                        "[\W_]\(?\d{4}\)?.*",                   'year in brakets
                        "(?i)[\W_]blu[\W_]?ray.*",
                        "(?i)[\W_]bd[\W_]?rip.*",
                        "(?i)[\W_]dvd.*",
                        "(?i)[\W_]720.*",
                        "(?i)[\W_]1080.*",
                        "(?i)[\W_]1440.*",
                        "(?i)[\W_]2160.*",
                        "(?i)[\W_]4k.*",
                        "(?i)[\W_]ac3.*",
                        "(?i)[\W_]dts.*",
                        "(?i)[\W_]divx.*",
                        "(?i)[\W_]xvid.*",
                        "(?i)[\W_]dc[\W_]?.*",
                        "(?i)[\W_]dir(ector'?s?)?\s?cut.*",
                        "(?i)[\W_]extended.*",
                        "(?i)[\W_]hd(tv)?.*",
                        "(?i)[\W_]unrated.*",
                        "(?i)[\W_]uncut.*",
                        "(?i)[\W_]([a-z]{3}|multi)[sd]ub.*",
                        "(?i)[\W_]\[offline\].*",
                        "(?i)[\W_]ntsc.*",
                        "[\W_]PAL[\W_]?.*",
                        "\.[->] ",                              'convert dots to space
                        "_[->] ",                               'convert underscore to space
                        " - [->] "                              'convert space-minus-space to space
                    }
            Case Enums.DefaultType.ValidSubtitleExts
                Return New ExtendedListOfString(listType) From {
                        ".aqt", ".ass", ".idx", ".jss", ".mpl", ".rt", ".sami", ".smi", ".srt", ".ssa", ".sst", ".sub"
                    }
            Case Enums.DefaultType.ValidThemeExts
                Return New ExtendedListOfString(listType) From {
                        ".flac", ".m4a", ".mp3", ".wav", ".wma"
                    }
            Case Enums.DefaultType.ValidVideoExts
                Return New ExtendedListOfString(listType) From {
                        ".3gpp", ".asf", ".asx", ".avi", ".avs", ".bin", ".cue", ".dat", ".disc",
                        ".divx", ".dvb", ".dvr-ms", ".evo", ".flv", ".ifo", ".img", ".iso", ".m2t", ".m2ts",
                        ".m4v", ".mkv", ".mov", ".mp4", ".mpeg", ".mpg", ".mts", ".nsv", ".nut", ".ogg",
                        ".ogm", ".ogv", ".ram", ".rar", ".rmvb", ".swf", ".ts", ".viv", ".vob", ".webm",
                        ".wma", ".wmv"
                    }
            Case Else
                Return New ExtendedListOfString(listType)
        End Select
        Return Nothing
    End Function

#End Region 'Methods

End Class

<Serializable>
Public Class FilenameBase

#Region "Properties"

    Public Property Actortumbs As FilenameItemBase = New FilenameItemBase

    Public Property Banner As FilenameItemBase = New FilenameItemBase

    Public Property Characterart As FilenameItemBase = New FilenameItemBase

    Public Property Clearart As FilenameItemBase = New FilenameItemBase

    Public Property Clearlogo As FilenameItemBase = New FilenameItemBase

    Public Property Discart As FilenameItemBase = New FilenameItemBase

    Public Property Extrafanarts As FilenameItemBase = New FilenameItemBase

    Public Property Extrathumbs As FilenameItemBase = New FilenameItemBase

    Public Property Fanart As FilenameItemBase = New FilenameItemBase

    Public Property Keyart As FilenameItemBase = New FilenameItemBase

    Public Property Landscape As FilenameItemBase = New FilenameItemBase

    Public Property Nfo As FilenameItemBase = New FilenameItemBase

    Public Property Poster As FilenameItemBase = New FilenameItemBase

    Public Property Theme As FilenameItemBase = New FilenameItemBase

    Public Property Trailer As FilenameItemBase = New FilenameItemBase

#End Region 'Properties

End Class


<Serializable>
Public Class FilenameItemBase

#Region "Properties"

    Public Property BDMV As List(Of String)

    Public Property Boxee As Boolean = False

    Public Property Kodi As Boolean = False

    Public Property Multi As List(Of String)

    Public Property NMJ As Boolean = False

    Public Property VTS As List(Of String)

    Public Property YAMJ As Boolean = False

    Public Property [Single] As List(Of String)

#End Region 'Properties

End Class

<Serializable()>
Public Class ImageBase

#Region "Properties"

    Public Property Actorthumbs As ImageItemBase = New ImageItemBase

    Public Property Banner As ImageItemBase = New ImageItemBase

    Public Property CacheEnabled As Boolean = False

    Public Property Characterart As ImageItemBase = New ImageItemBase

    Public Property Clearart As ImageItemBase = New ImageItemBase

    Public Property Clearlogo As ImageItemBase = New ImageItemBase

    Public Property Discart As ImageItemBase = New ImageItemBase

    Public Property DisplayImageSelectDialog As Boolean = True

    Public Property Extrafanarts As ImageItemBase = New ImageItemBase

    Public Property Extrathumbs As ImageItemBase = New ImageItemBase

    Public Property Fanart As ImageItemBase = New ImageItemBase

    Public Property FilterMediaLanguage As Boolean = False

    Public Property FilterGetEnglishImages As Boolean = False

    Public Property FilterGetBlankImages As Boolean = False

    Public Property ForceLanguage As Boolean = False

    Public Property ForcedLanguage As String = String.Empty

    <XmlIgnore>
    Public ReadOnly Property ForcedLanguageSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(ForcedLanguage)
        End Get
    End Property

    Public Property Keyart As ImageItemBase = New ImageItemBase

    Public Property Landscape As ImageItemBase = New ImageItemBase

    Public Property Poster As ImageItemBase = New ImageItemBase

#End Region 'Properties

End Class


<Serializable()>
Public Class ImageItemBase

#Region "Properties"

    Public Property KeepExisting As Boolean = False

    Public Property Limit As Integer = 4

    Public Property MaxHeight As Integer = 0

    Public Property MaxWidth As Integer = 0

    Public Property PreferredSize As Enums.ImageSize = Enums.ImageSize.Any

    Public Property PreferredSizeOnly As Boolean = False

    Public Property Preselect As Boolean = True

    Public Property Quality As Integer = 100

    Public Property Resize As Boolean = False

    Public Property VideoExtraction As Boolean = False

    Public Property VideoExtractionPreferred As Boolean = False

#End Region 'Properties

End Class


<Serializable()>
Public Class InformationBase

#Region "Properties"

    Public Property Actors As New InformationItem_Actor
    Public Property Aired As New InformationItemBase
    Public Property Certifications As New InformationItemBase
    Public Property CertificationsForMPAA As Boolean = False
    Public Property CertificationsForMPAAFallback As Boolean = False
    Public Property CertificationsOnlyValue As Boolean = False
    Public Property CleanPlotAndOutline As Boolean = False
    Public Property ClearDisabledFields As Boolean = False
    Public Property Collection As New InformationItem_Collection
    Public Property Countries As New InformationItemBase
    Public Property Creators As New InformationItemBase
    Public Property Credits As New InformationItemBase
    Public Property Directors As New InformationItemBase
    Public Property EpisodeGuideURL As New InformationItemBase
    Public Property Genres As New InformationItemBase
    Public Property GuestStars As New InformationItem_GuestStar
    Public Property LockAudioLanguage As Boolean = False
    Public Property LockVideoLanguage As Boolean = False
    Public Property MetadataScan As New DataSpecificationItem_Metadata
    Public Property MPAA As New InformationItem_MPAA
    Public Property OriginalTitle As New InformationItemBase
    Public Property Outline As New InformationItem_Outline With {.Limit = 350}
    Public Property Plot As New InformationItemBase
    Public Property Premiered As New InformationItemBase
    Public Property Ratings As New InformationItem_Rating
    Public Property Runtime As New InformationItemBase
    Public Property Status As New InformationItemBase
    Public Property Studios As New InformationItemBase
    Public Property Tagline As New InformationItemBase
    Public Property Tags As New InformationItemBase
    Public Property Title As New InformationItem_Title
    Public Property Top250 As New InformationItemBase
    Public Property TrailerLink As New InformationItem_Trailer
    Public Property UniqueId As New UniqueIds
    Public Property UserRating As New InformationItemBase

#End Region 'Properties 

End Class

<Serializable()>
Public Class InformationItemBase

#Region "Properties"

    Public Property Enabled() As Boolean = True

    Public Property Filter() As String = String.Empty

    <XmlIgnore()>
    Public ReadOnly Property FilterSpecified() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Filter)
        End Get
    End Property

    Public Property Limit() As Integer = 0

    <XmlIgnore()>
    Public ReadOnly Property LimitSpecified() As Boolean
        Get
            Return Limit > 0
        End Get
    End Property

    Public Property LimitAsList() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.Generic)

    <XmlIgnore()>
    Public ReadOnly Property LimitAsListSpecified() As Boolean
        Get
            Return LimitAsList.Count > 0
        End Get
    End Property

    Public Property Locked() As Boolean = False

    Public Property ScraperSettings As New List(Of String)

    <XmlIgnore()>
    Public ReadOnly Property ScraperSettingsSpecified() As Boolean
        Get
            Return ScraperSettings.Count > 0
        End Get
    End Property

#End Region 'Properties 

End Class

<Serializable()>
Public Class InformationItem_Actor
    Inherits InformationItemBase

#Region "Properties"

    Public Property WithImageOnly() As Boolean = False

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        SetDefaults()
    End Sub

#End Region 'Constructors

#Region "Methods"
    ''' <summary>
    ''' Defines all default settings for a new installation
    ''' </summary>
    ''' <remarks></remarks>
    Public Overloads Sub SetDefaults()
        Enabled = True
        Filter = String.Empty
        Limit = 0
        Locked = False
        WithImageOnly = False
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class InformationItem_Collection
    Inherits InformationItemBase

#Region "Properties"

    Public Property AddAutomaticallyToCollection As Boolean = True

    Public Property SaveExtendedInformation As Boolean = True

    Public Property SaveYAMJCompatible As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class InformationItem_GuestStar
    Inherits InformationItem_Actor

#Region "Properties"

    Public Property AddToActors() As Boolean = False

#End Region 'Properties 

End Class

<Serializable()>
Public Class InformationItem_MPAA
    Inherits InformationItemBase

#Region "Properties"

    Public Property NotRatedValue As String = String.Empty

    <XmlIgnore>
    Public ReadOnly Property NotRatedValueSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(NotRatedValue)
        End Get
    End Property

    Public Property UsePlotAsFallback As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class InformationItem_Outline
    Inherits InformationItemBase

#Region "Properties"

    Public Property UsePlot() As Boolean = False

    Public Property UsePlotAsFallback As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class InformationItem_Rating
    Inherits InformationItemBase

#Region "Properties"

    Public Property DefaultType As String = String.Empty

    Public Property CreateNodes As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class InformationItem_Title
    Inherits InformationItemBase

#Region "Properties"

    Public Property UseOriginalTitle() As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class InformationItem_Trailer
    Inherits InformationItemBase

#Region "Properties"

    Public Property SaveKodiCompatible() As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class DataSpecificationItem_Metadata

#Region "Properties"

    Public Property Enabled() As Boolean = True

    Public Property DurationForRuntimeEnabled() As Boolean = True

    Public Property DurationForRuntimeFormat() As String = "<m>"

    Public Property LockAudioLanguage() As Boolean = False

    Public Property LockVideoLanguage() As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class SourceBase

#Region "Properties"

    Public Property AutoScrapeOnImportEnabled As Boolean = False

    Public Property AutoScrapeOnImportScrapeType As Enums.ScrapeType = Enums.ScrapeType.Auto

    Public Property AutoScrapeOnImportMissingItemsOnly As Boolean = False

    Public Property CleanLibraryAfterUpdate As Boolean = False

    Public Property DateAddedDateTime As Enums.DateTimeStamp = Enums.DateTimeStamp.Now

    Public Property DateAddedIgnoreNfo As Boolean = False

    Public Property DefaultLanguage As String = "en-US"

    Public Property DefaultEpisodeOrdering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard

    Public Property EpisodeMatching As New EpisodeMatchingSpecification

    <XmlIgnore>
    Public ReadOnly Property EpisodeMatchingSepcified As Boolean
        Get
            Return EpisodeMatching.Count > 0
        End Get
    End Property

    Public Property EpisodeMultiPartMatching As New EpisodeMultipartMatchingSpecification

    <XmlIgnore>
    Public ReadOnly Property EpisodeMultiPartMatchingSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(EpisodeMultiPartMatching.Regex)
        End Get
    End Property

    Public Property LevTolerance() As Integer = 0

    <XmlIgnore()>
    Public ReadOnly Property LevToleranceSpecified As Boolean
        Get
            Return LevTolerance > 0
        End Get
    End Property

    Public Property MarkNewAsMarked As Boolean = False

    Public Property MarkNewAsMarkedWithoutNFO As Boolean = False

    Public Property MarkNewAsNew As Boolean = True

    Public Property MarkNewAsNewWithoutNFO As Boolean = False

    Public Property OverWriteNfo As Boolean = False

    Public Property SkipLessThan As Integer = 0

    <XmlIgnore>
    Public ReadOnly Property SkipLessThanSpecified As Boolean
        Get
            Return SkipLessThan > 0
        End Get
    End Property

    Public Property SortBeforeScan As Boolean

    Public Property TitleFilters As New ExtendedListOfString(Enums.DefaultType.Generic)

    <XmlIgnore>
    Public ReadOnly Property TitleFiltersSpecified As Boolean
        Get
            Return TitleFilters.Count > 0
        End Get
    End Property

    Public Property TitleFiltersEnabled As Boolean = True

    Public Property TitleProperCase As Boolean = True

    Public Property UnmarkNewAfterScraping As Boolean = True

    Public Property ResetNewBeforeDBUpdate As Boolean = True

    Public Property ResetNewOnExit As Boolean = True

    Public Property VideoSourceFromFolder As Boolean = False

#End Region 'Properties 

End Class


<Serializable()>
Public Class ThemeBase

#Region "Properties"

    Public Property DefaultSearchParameter() As String = "theme"

    Public Property KeepExisting() As Boolean = False

#End Region 'Properties

End Class

<Serializable()>
Public Class TrailerBase

#Region "Properties"

    Public Property DefaultSearchParameter As String = "trailer"

    Public Property FilterMediaLanguage As Boolean = False

    Public Property ForceLanguage As Boolean = False

    Public Property ForcedLanguage As String = String.Empty

    <XmlIgnore>
    Public ReadOnly Property ForcedLanguageSpecified As Boolean
        Get
            Return Not String.IsNullOrEmpty(ForcedLanguage)
        End Get
    End Property

    Public Property KeepExisting As Boolean = False

    Public Property MinimumVideoQuality As Enums.VideoResolution = Enums.VideoResolution.Any

    Public Property PreferredVideoQuality As Enums.VideoResolution = Enums.VideoResolution.Any

    Public Property PreferredType As Enums.VideoType = Enums.VideoType.Any

    Public Property PreferredTypeOnly As Boolean = False

    Public Property ScraperSettings As New List(Of String)

#End Region 'Properties

End Class

<Serializable()>
Public Class TVEpisodeBase

#Region "Properties"

    Public Property Filenaming As New FilenameBase

    Public Property ImageSettings As ImageBase = New ImageBase With {.CacheEnabled = True}

    Public Property InformationSettings As New InformationBase

    Public Property SourceSettings As New SourceBase

#End Region 'Properties

End Class

<Serializable()>
Public Class TVSeasonBase

#Region "Properties"

    Public Property Filenaming As New FilenameBase

    Public Property ImageSettings As ImageBase = New ImageBase With {.CacheEnabled = True}

    Public Property InformationSettings As New InformationBase

    Public Property SourceSettings As New SourceBase

#End Region 'Properties

End Class

<Serializable()>
Public Class TVShowBase

#Region "Properties"

    Public Property Filenaming As New FilenameBase

    Public Property ImageSettings As ImageBase = New ImageBase With {.CacheEnabled = True}

    Public Property InformationSettings As New InformationBase

    Public Property SourceSettings As New SourceBase

    Public Property ThemeSettings As New ThemeBase

#End Region 'Properties

End Class

<Serializable>
Public Class UniqueIds

#Region "Properties"

    Public Property DefaultType As String = String.Empty

    Public Property CreateNodeId As Boolean = True

    Public Property CreateNodeTmdb As Boolean = False

    Public Property CreateNodeTmdbColId As Boolean = False

#End Region 'Properties

End Class

Public Class VideoSourceMappingBase

#Region "Properties"

    Public Property ByExtension As New List(Of VideoSourceByExtension)

    <XmlIgnore>
    Public ReadOnly Property ByExtensionSpecified As Boolean
        Get
            Return ByExtension.Count > 0
        End Get
    End Property

    Public Property ByExtensionEnabled As Boolean = False

    Public Property ByRegex As New List(Of VideoSourceByRegex)

    <XmlIgnore>
    Public ReadOnly Property ByRegexSpecified As Boolean
        Get
            Return ByRegex.Count > 0
        End Get
    End Property

    Public Property ByRegexEnabled As Boolean = True

#End Region 'Properties

#Region "Methods"

    Public Function GetDefaults() As List(Of VideoSourceByRegex)
        Return New List(Of VideoSourceByRegex) From {
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]3dbd[\W_]", .Videosource = "3dbd"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]azhd|amazon[\W_]", .Videosource = "amazon"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_](b[dr][-\s]?rip|blu[-\s]?ray)[\W_]", .Videosource = "bluray"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]bd25[\W_]", .Videosource = "bluray"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]bd50[\W_]", .Videosource = "bluray"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dvd5[\W_]", .Videosource = "dvd"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dvd9[\W_]", .Videosource = "dvd"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_](sd[-\s]?)?dvd([-\s]?rip)?[\W_]", .Videosource = "dvd"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hd[-\s]?dvd[\W_]", .Videosource = "hddvd"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hd[-\s]?tv[\W_]", .Videosource = "hdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]ithd|itunes[\W_]", .Videosource = "itunes"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]nfu?hd|netflix[\W_]", .Videosource = "netflix"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]pdtv[\W_]", .Videosource = "sdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dsr[\W_]", .Videosource = "sdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]ntsc[\W_]", .Videosource = "sdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]sd[-\s]?tv[\W_]", .Videosource = "sdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]tvrip[\W_]", .Videosource = "sdtv"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]vhs[\W_]", .Videosource = "vhs"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hddl[\W_]", .Videosource = "webdl"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]web-?dl[\W_]", .Videosource = "webdl"},
            New VideoSourceByRegex With {.Regexp = "(?i)[\W_]web-?rip[\W_]", .Videosource = "webdl"}
        }
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class VideoSourceByExtension

#Region "Fields"

        Private _extension As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlAttribute>
        Public Property Extension As String
            Get
                Return _extension
            End Get
            Set(value As String)
                _extension = StringUtils.CleanFileExtension(value)
            End Set
        End Property

        <XmlText>
        Public Property VideoSource As String = String.Empty

#End Region 'Properties

    End Class

    Public Class VideoSourceByRegex

#Region "Properties"

        <XmlAttribute>
        Public Property Regexp As String = String.Empty

        <XmlText>
        Public Property Videosource As String = String.Empty

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class