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

Imports System.IO
Imports System.Xml.Serialization
Imports System.Net
Imports System.Drawing
Imports System.Windows.Forms
Imports NLog

<Serializable()>
<XmlRoot("Settings")>
Public Class Settings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _moviegenerallanguage As String = "en-US"
    Private _tvgenerallanguage As String = "en-US"

#End Region 'Fields

#Region "Properties"

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
    <XmlArray("EmberModules")>
    <XmlArrayItem("Module")>
    Public Property EmberModules() As List(Of ModulesManager._XMLEmberModuleClass) = New List(Of ModulesManager._XMLEmberModuleClass)
    Public Property FileSystemCleanerWhitelist() As Boolean = False
    Public Property FileSystemCleanerWhitelistExts() As List(Of String) = New List(Of String)
    Public Property FileSystemExpertCleaner() As Boolean = False
    Public Property FileSystemNoStackExts() As List(Of String) = New List(Of String)
    <XmlIgnore>
    Public Property FileSystemValidExts() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidVideoExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("FileSystemValidExts")>
    Public Property FileSystemValidExtsXml As String()
        Get
            Return FileSystemValidExts.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                FileSystemValidExts.Clear()
                FileSystemValidExts.AddRange(value.ToList)
            End If
        End Set
    End Property
    <XmlIgnore>
    Public Property FileSystemValidSubtitlesExts() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidSubtitleExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("FileSystemValidSubtitlesExts")>
    Public Property FileSystemValidSubtitlesExtsXml As String()
        Get
            Return FileSystemValidSubtitlesExts.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                FileSystemValidSubtitlesExts.Clear()
                FileSystemValidSubtitlesExts.AddRange(value.ToList)
            End If
        End Set
    End Property
    <XmlIgnore>
    Public Property FileSystemValidThemeExts() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.ValidThemeExts).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("FileSystemValidThemeExts")>
    Public Property FileSystemValidThemeExtsXml As String()
        Get
            Return FileSystemValidThemeExts.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                FileSystemValidThemeExts.Clear()
                FileSystemValidThemeExts.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property GeneralCheckUpdates() As Boolean = False
    Public Property GeneralDaemonDrive() As String = String.Empty
    Public Property GeneralDaemonPath() As String = String.Empty
    Public Property GeneralDateAddedIgnoreNFO() As Boolean = False
    Public Property GeneralDateTime() As Enums.DateTime = Enums.DateTime.Now
    Public Property GeneralDigitGrpSymbolVotes() As Boolean = False
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
    Public Property GeneralInfoPanelStateTVShow() As Integer = 2
    Public Property GeneralLanguage() As String = "English_(en_US)"
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
    Public Property GeneralShowImgDims() As Boolean = True
    Public Property GeneralShowImgNames() As Boolean = True
    Public Property GeneralShowLangFlags() As Boolean = True
    Public Property GeneralSourceFromFolder() As Boolean = False
    <XmlIgnore>
    Public Property GeneralSortTokens() As ExtendedListOfString = New ExtendedListOfString(Enums.DefaultType.SortTokens).GetDefaults
    ''' <summary>
    ''' It's not possible to deserialize an empty List(Of String). For this reason a String Array is used for XML.
    ''' </summary>
    ''' <returns></returns>
    <XmlArray("GeneralSortTokens")>
    Public Property GeneralSortTokensXml As String()
        Get
            Return GeneralSortTokens.ToArray
        End Get
        Set(value As String())
            If value IsNot Nothing Then
                GeneralSortTokens.Clear()
                GeneralSortTokens.AddRange(value.ToList)
            End If
        End Set
    End Property
    Public Property GeneralSplitterDistanceMain() As Integer = 550
    Public Property GeneralSplitterDistanceTVSeason() As Integer = 200
    Public Property GeneralSplitterDistanceTVShow() As Integer = 200
    Public Property GeneralTheme() As String = "FullHD-Default"
    Public Property GeneralWindowLoc() As Point = New Point(10, 10)
    Public Property GeneralWindowSize() As Size = New Size(1024, 768)
    Public Property GeneralWindowState() As FormWindowState = FormWindowState.Maximized
    Public Property MovieActorThumbsKeepExisting() As Boolean = False
    Public Property MovieBackdropsAuto() As Boolean = False
    Public Property MovieBackdropsPath() As String = String.Empty
    Public Property MovieBannerHeight() As Integer = 0
    Public Property MovieBannerKeepExisting() As Boolean = False
    Public Property MovieBannerPrefSize() As Enums.MovieBannerSize = Enums.MovieBannerSize.Any
    Public Property MovieBannerPrefSizeOnly() As Boolean = False
    Public Property MovieBannerResize() As Boolean = False
    Public Property MovieBannerWidth() As Integer = 0
    Public Property MovieCleanDB() As Boolean = False
    Public Property MovieClearArtKeepExisting() As Boolean = False
    Public Property MovieClearLogoKeepExisting() As Boolean = False
    Public Property MovieClickScrape() As Boolean = False
    Public Property MovieClickScrapeAsk() As Boolean = False
    Public Property MovieDiscArtKeepExisting() As Boolean = False
    Public Property MovieExtrafanartsHeight() As Integer = 0
    Public Property MovieExtrafanartsKeepExisting() As Boolean = False
    Public Property MovieExtrafanartsLimit() As Integer = 4
    Public Property MovieExtrafanartsPrefSize() As Enums.MovieFanartSize = Enums.MovieFanartSize.Any
    Public Property MovieExtrafanartsPrefSizeOnly() As Boolean = False
    Public Property MovieExtrafanartsPreselect() As Boolean = True
    Public Property MovieExtrafanartsResize() As Boolean = False
    Public Property MovieExtrafanartsWidth() As Integer = 0
    Public Property MovieExtrathumbsCreatorAutoThumbs() As Boolean = False
    Public Property MovieExtrathumbsCreatorNoBlackBars() As Boolean = False
    Public Property MovieExtrathumbsCreatorNoSpoilers() As Boolean = False
    Public Property MovieExtrathumbsCreatorUseETasFA() As Boolean = False
    Public Property MovieExtrathumbsHeight() As Integer = 0
    Public Property MovieExtrathumbsKeepExisting() As Boolean = False
    Public Property MovieExtrathumbsLimit() As Integer = 4
    Public Property MovieExtrathumbsPrefSize() As Enums.MovieFanartSize = Enums.MovieFanartSize.Any
    Public Property MovieExtrathumbsPrefSizeOnly() As Boolean = False
    Public Property MovieExtrathumbsPreselect() As Boolean = True
    Public Property MovieExtrathumbsResize() As Boolean = False
    Public Property MovieExtrathumbsWidth() As Integer = 0
    Public Property MovieFanartHeight() As Integer = 0
    Public Property MovieFanartKeepExisting() As Boolean = False
    Public Property MovieFanartPrefSize() As Enums.MovieFanartSize = Enums.MovieFanartSize.Any
    Public Property MovieFanartPrefSizeOnly() As Boolean = False
    Public Property MovieFanartResize() As Boolean = False
    Public Property MovieFanartWidth() As Integer = 0
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
    Public Property MovieGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.NewSkip
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
    Public Property MovieImagesCacheEnabled() As Boolean = False
    Public Property MovieImagesDisplayImageSelect() As Boolean = True
    Public Property MovieImagesForceLanguage() As Boolean = False
    Public Property MovieImagesForcedLanguage() As String = "en"
    Public Property MovieImagesGetBlankImages() As Boolean = False
    Public Property MovieImagesGetEnglishImages() As Boolean = False
    Public Property MovieImagesMediaLanguageOnly() As Boolean = False
    Public Property MovieImagesNotSaveURLToNfo() As Boolean = False
    Public Property MovieKeyartHeight() As Integer = 0
    Public Property MovieKeyartKeepExisting() As Boolean = False
    Public Property MovieKeyartPrefSize() As Enums.MoviePosterSize = Enums.MoviePosterSize.Any
    Public Property MovieKeyartPrefSizeOnly() As Boolean = False
    Public Property MovieKeyartResize() As Boolean = False
    Public Property MovieKeyartWidth() As Integer = 0
    Public Property MovieLandscapeKeepExisting() As Boolean = False
    Public Property MovieLevTolerance() As Integer = 0
    Public Property MovieLockActors() As Boolean = False
    Public Property MovieLockCert() As Boolean = False
    Public Property MovieLockCollectionID() As Boolean = False
    Public Property MovieLockCollections() As Boolean = False
    Public Property MovieLockCountry() As Boolean = False
    Public Property MovieLockCredits() As Boolean = False
    Public Property MovieLockDirector() As Boolean = False
    Public Property MovieLockGenre() As Boolean = False
    Public Property MovieLockLanguageA() As Boolean = False
    Public Property MovieLockLanguageV() As Boolean = False
    Public Property MovieLockMPAA() As Boolean = False
    Public Property MovieLockOriginalTitle() As Boolean = False
    Public Property MovieLockOutline() As Boolean = False
    Public Property MovieLockPlot() As Boolean = False
    Public Property MovieLockPremiered() As Boolean = False
    Public Property MovieLockRating() As Boolean = False
    Public Property MovieLockRuntime() As Boolean = False
    Public Property MovieLockStudio() As Boolean = False
    Public Property MovieLockTagline() As Boolean = False
    Public Property MovieLockTags() As Boolean = False
    Public Property MovieLockTitle() As Boolean = False
    Public Property MovieLockTop250() As Boolean = False
    Public Property MovieLockTrailer() As Boolean = False
    Public Property MovieLockUserRating() As Boolean = False
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
    Public Property MoviePosterHeight() As Integer = 0
    Public Property MoviePosterKeepExisting() As Boolean = False
    Public Property MoviePosterPrefSize() As Enums.MoviePosterSize = Enums.MoviePosterSize.Any
    Public Property MoviePosterPrefSizeOnly() As Boolean = False
    Public Property MoviePosterResize() As Boolean = False
    Public Property MoviePosterWidth() As Integer = 0
    Public Property MovieProperCase() As Boolean = True
    Public Property MovieRecognizeVTSExpertVTS() As Boolean
    Public Property MovieScanOrderModify() As Boolean = False
    Public Property MovieScraperCast() As Boolean = True
    Public Property MovieScraperCastLimit() As Integer = 0
    Public Property MovieScraperCastWithImgOnly() As Boolean = False
    Public Property MovieScraperCert() As Boolean = False
    Public Property MovieScraperCertForMPAA() As Boolean = False
    Public Property MovieScraperCertForMPAAFallback() As Boolean = False
    Public Property MovieScraperCertLang() As String = String.Empty
    Public Property MovieScraperCertOnlyValue() As Boolean = False
    Public Property MovieScraperCleanFields() As Boolean = False
    Public Property MovieScraperCleanPlotOutline() As Boolean = False
    Public Property MovieScraperCollectionID() As Boolean = True
    Public Property MovieScraperCollectionsAuto() As Boolean = True
    Public Property MovieScraperCollectionsExtendedInfo() As Boolean = False
    Public Property MovieScraperCollectionsYAMJCompatibleSets() As Boolean = False
    Public Property MovieScraperCountry() As Boolean = True
    Public Property MovieScraperCountryLimit() As Integer = 0
    Public Property MovieScraperCredits() As Boolean = True
    Public Property MovieScraperDirector() As Boolean = True
    Public Property MovieScraperDurationRuntimeFormat() As String = "<m>"
    Public Property MovieScraperGenre() As Boolean = True
    Public Property MovieScraperGenreLimit() As Integer = 0
    Public Property MovieScraperIdDefaultType() As String = "imdb"
    Public Property MovieScraperIdWriteNodeDefaultId() As Boolean = True
    Public Property MovieScraperIdWriteNodeTMDbCollectionId() As Boolean = False
    Public Property MovieScraperIdWriteNodeTMDbId() As Boolean = False
    Public Property MovieScraperMPAA() As Boolean = True
    Public Property MovieScraperMPAANotRated() As String = String.Empty
    <XmlIgnore>
    Public ReadOnly Property MovieScraperMPAANotRatedSpecified() As Boolean
        Get
            Return Not String.IsNullOrEmpty(MovieScraperMPAANotRated)
        End Get
    End Property
    Public Property MovieScraperMetaDataIFOScan() As Boolean = True
    Public Property MovieScraperMetaDataScan() As Boolean = True
    Public Property MovieScraperOriginalTitle() As Boolean = True
    Public Property MovieScraperOriginalTitleAsTitle() As Boolean = False
    Public Property MovieScraperOutline() As Boolean = True
    Public Property MovieScraperOutlineLimit() As Integer = 350
    Public Property MovieScraperPlot() As Boolean = True
    Public Property MovieScraperPlotForOutline() As Boolean = False
    Public Property MovieScraperPlotForOutlineIfEmpty() As Boolean = False
    Public Property MovieScraperPremiered() As Boolean = True
    Public Property MovieScraperRating() As Boolean = True
    Public Property MovieScraperRatingDefaultType() As String = "imdb"
    Public Property MovieScraperRatingVotesWriteNode() As Boolean = True
    Public Property MovieScraperReleaseDateWriteNode() As Boolean = True
    Public Property MovieScraperRuntime() As Boolean = True
    Public Property MovieScraperStudio() As Boolean = True
    Public Property MovieScraperStudioLimit() As Integer = 0
    Public Property MovieScraperStudioWithImgOnly() As Boolean = False
    Public Property MovieScraperTagline() As Boolean = True
    Public Property MovieScraperTitle() As Boolean = True
    Public Property MovieScraperTop250() As Boolean = True
    Public Property MovieScraperTrailer() As Boolean = True
    Public Property MovieScraperTrailerFromTrailerScrapers() As Boolean = False
    Public Property MovieScraperUseMDDuration() As Boolean = True
    Public Property MovieScraperUserRating() As Boolean = True
    Public Property MovieScraperXBMCTrailerFormat() As Boolean = False
    Public Property MovieSetBannerHeight() As Integer = 0
    Public Property MovieSetBannerKeepExisting() As Boolean = False
    Public Property MovieSetBannerPrefSize() As Enums.MovieBannerSize = Enums.MovieBannerSize.Any
    Public Property MovieSetBannerPrefSizeOnly() As Boolean = False
    Public Property MovieSetBannerResize() As Boolean = False
    Public Property MovieSetBannerWidth() As Integer = 0
    Public Property MovieSetCleanDB() As Boolean = False
    Public Property MovieSetCleanFiles() As Boolean = False
    Public Property MovieSetClearArtKeepExisting() As Boolean = False
    Public Property MovieSetClearLogoKeepExisting() As Boolean = False
    Public Property MovieSetClickScrape() As Boolean = False
    Public Property MovieSetClickScrapeAsk() As Boolean = False
    Public Property MovieSetDiscArtKeepExisting() As Boolean = False
    Public Property MovieSetFanartHeight() As Integer = 0
    Public Property MovieSetFanartKeepExisting() As Boolean = False
    Public Property MovieSetFanartPrefSize() As Enums.MovieFanartSize = Enums.MovieFanartSize.Any
    Public Property MovieSetFanartPrefSizeOnly() As Boolean = False
    Public Property MovieSetFanartResize() As Boolean = False
    Public Property MovieSetFanartWidth() As Integer = 0
    Public Property MovieSetGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property MovieSetGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property MovieSetGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.NewSkip
    Public Property MovieSetGeneralMarkNew() As Boolean = False
    Public Property MovieSetGeneralMediaListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property MovieSetImagesCacheEnabled() As Boolean = False
    Public Property MovieSetImagesDisplayImageSelect() As Boolean = True
    Public Property MovieSetImagesForceLanguage() As Boolean = False
    Public Property MovieSetImagesForcedLanguage() As String = "en"
    Public Property MovieSetImagesGetBlankImages() As Boolean = False
    Public Property MovieSetImagesGetEnglishImages() As Boolean = False
    Public Property MovieSetImagesMediaLanguageOnly() As Boolean = False
    Public Property MovieSetKeyartHeight() As Integer = 0
    Public Property MovieSetKeyartKeepExisting() As Boolean = False
    Public Property MovieSetKeyartPrefSize() As Enums.MoviePosterSize = Enums.MoviePosterSize.Any
    Public Property MovieSetKeyartPrefSizeOnly() As Boolean = False
    Public Property MovieSetKeyartResize() As Boolean = False
    Public Property MovieSetKeyartWidth() As Integer = 0
    Public Property MovieSetLandscapeKeepExisting() As Boolean = False
    Public Property MovieSetLockPlot() As Boolean = False
    Public Property MovieSetLockTitle() As Boolean = False
    Public Property MovieSetMissingBanner() As Boolean = False
    Public Property MovieSetMissingClearArt() As Boolean = False
    Public Property MovieSetMissingClearLogo() As Boolean = False
    Public Property MovieSetMissingDiscArt() As Boolean = False
    Public Property MovieSetMissingFanart() As Boolean = False
    Public Property MovieSetMissingLandscape() As Boolean = False
    Public Property MovieSetMissingNFO() As Boolean = False
    Public Property MovieSetMissingPoster() As Boolean = False
    Public Property MovieSetPosterHeight() As Integer = 0
    Public Property MovieSetPosterKeepExisting() As Boolean = False
    Public Property MovieSetPosterPrefSize() As Enums.MoviePosterSize = Enums.MoviePosterSize.Any
    Public Property MovieSetPosterPrefSizeOnly() As Boolean = False
    Public Property MovieSetPosterResize() As Boolean = False
    Public Property MovieSetPosterWidth() As Integer = 0
    Public Property MovieSetScraperIdDefaultType() As String = "tmdb"
    Public Property MovieSetScraperIdWriteNodeDefaultId() As Boolean = False
    Public Property MovieSetScraperPlot() As Boolean = True
    Public Property MovieSetScraperTitle() As Boolean = True
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
    Public Property TVAllSeasonsBannerHeight() As Integer = 0
    Public Property TVAllSeasonsBannerKeepExisting() As Boolean = False
    Public Property TVAllSeasonsBannerPrefSize() As Enums.TVBannerSize = Enums.TVBannerSize.Any
    Public Property TVAllSeasonsBannerPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVAllSeasonsBannerResize() As Boolean = False
    Public Property TVAllSeasonsBannerWidth() As Integer = 0
    Public Property TVAllSeasonsFanartHeight() As Integer = 0
    Public Property TVAllSeasonsFanartKeepExisting() As Boolean = False
    Public Property TVAllSeasonsFanartPrefSize() As Enums.TVFanartSize = Enums.TVFanartSize.Any
    Public Property TVAllSeasonsFanartPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsFanartResize() As Boolean = False
    Public Property TVAllSeasonsFanartWidth() As Integer = 0
    Public Property TVAllSeasonsLandscapeKeepExisting() As Boolean = False
    Public Property TVAllSeasonsPosterHeight() As Integer = 0
    Public Property TVAllSeasonsPosterKeepExisting() As Boolean = False
    Public Property TVAllSeasonsPosterPrefSize() As Enums.TVPosterSize = Enums.TVPosterSize.Any
    Public Property TVAllSeasonsPosterPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsPosterResize() As Boolean = False
    Public Property TVAllSeasonsPosterWidth() As Integer = 0
    Public Property TVCleanDB() As Boolean = False
    Public Property TVDisplayMissingEpisodes() As Boolean = True
    Public Property TVEpisodeActorThumbsKeepExisting() As Boolean = False
    Public Property TVEpisodeFanartHeight() As Integer = 0
    Public Property TVEpisodeFanartKeepExisting() As Boolean = False
    Public Property TVEpisodeFanartPrefSize() As Enums.TVFanartSize = Enums.TVFanartSize.Any
    Public Property TVEpisodeFanartPrefSizeOnly() As Boolean = False
    Public Property TVEpisodeFanartResize() As Boolean = False
    Public Property TVEpisodeFanartWidth() As Integer = 0
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
    Public Property TVEpisodePosterHeight() As Integer = 0
    Public Property TVEpisodePosterKeepExisting() As Boolean = False
    Public Property TVEpisodePosterPrefSize() As Enums.TVEpisodePosterSize = Enums.TVEpisodePosterSize.Any
    Public Property TVEpisodePosterPrefSizeOnly() As Boolean = False
    Public Property TVEpisodePosterResize() As Boolean = False
    Public Property TVEpisodePosterWidth() As Integer = 0
    Public Property TVEpisodeProperCase() As Boolean = True
    Public Property TVGeneralClickScrape() As Boolean = False
    Public Property TVGeneralClickScrapeAsk() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property TVGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.NewSkip
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
    Public Property TVImagesCacheEnabled() As Boolean = True
    Public Property TVImagesDisplayImageSelect() As Boolean = True
    Public Property TVImagesForceLanguage() As Boolean = False
    Public Property TVImagesForcedLanguage() As String = "en"
    Public Property TVImagesGetBlankImages() As Boolean = False
    Public Property TVImagesGetEnglishImages() As Boolean = False
    Public Property TVImagesMediaLanguageOnly() As Boolean = False
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
    Public Property TVScraperShowCertForMPAA() As Boolean = False
    Public Property TVScraperShowCertForMPAAFallback() As Boolean = False
    Public Property TVScraperShowCertLang() As String = String.Empty
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
    Public Property TVSeasonBannerHeight() As Integer = 0
    Public Property TVSeasonBannerKeepExisting() As Boolean = False
    Public Property TVSeasonBannerPrefSize() As Enums.TVBannerSize = Enums.TVBannerSize.Any
    Public Property TVSeasonBannerPrefSizeOnly() As Boolean = False
    Public Property TVSeasonBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVSeasonBannerResize() As Boolean = False
    Public Property TVSeasonBannerWidth() As Integer = 0
    Public Property TVSeasonFanartHeight() As Integer = 0
    Public Property TVSeasonFanartKeepExisting() As Boolean = False
    Public Property TVSeasonFanartPrefSize() As Enums.TVFanartSize = Enums.TVFanartSize.Any
    Public Property TVSeasonFanartPrefSizeOnly() As Boolean = False
    Public Property TVSeasonFanartResize() As Boolean = False
    Public Property TVSeasonFanartWidth() As Integer = 0
    Public Property TVSeasonLandscapeKeepExisting() As Boolean = False
    Public Property TVSeasonMissingBanner() As Boolean = False
    Public Property TVSeasonMissingFanart() As Boolean = False
    Public Property TVSeasonMissingLandscape() As Boolean = False
    Public Property TVSeasonMissingPoster() As Boolean = False
    Public Property TVSeasonPosterHeight() As Integer = 0
    Public Property TVSeasonPosterKeepExisting() As Boolean = False
    Public Property TVSeasonPosterPrefSize() As Enums.TVSeasonPosterSize = Enums.TVSeasonPosterSize.Any
    Public Property TVSeasonPosterPrefSizeOnly() As Boolean = False
    Public Property TVSeasonPosterResize() As Boolean = False
    Public Property TVSeasonPosterWidth() As Integer = 0
    Public Property TVShowActorThumbsKeepExisting() As Boolean = False
    Public Property TVShowBannerHeight() As Integer = 0
    Public Property TVShowBannerKeepExisting() As Boolean = False
    Public Property TVShowBannerPrefSize() As Enums.TVBannerSize = Enums.TVBannerSize.Any
    Public Property TVShowBannerPrefSizeOnly() As Boolean = False
    Public Property TVShowBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVShowBannerResize() As Boolean = False
    Public Property TVShowBannerWidth() As Integer = 0
    Public Property TVShowCharacterArtKeepExisting() As Boolean = False
    Public Property TVShowClearArtKeepExisting() As Boolean = False
    Public Property TVShowClearLogoKeepExisting() As Boolean = False
    Public Property TVShowExtrafanartsHeight() As Integer = 0
    Public Property TVShowExtrafanartsKeepExisting() As Boolean = False
    Public Property TVShowExtrafanartsLimit() As Integer = 4
    Public Property TVShowExtrafanartsPrefSize() As Enums.TVFanartSize = Enums.TVFanartSize.Any
    Public Property TVShowExtrafanartsPrefSizeOnly() As Boolean = False
    Public Property TVShowExtrafanartsPreselect() As Boolean = True
    Public Property TVShowExtrafanartsResize() As Boolean = False
    Public Property TVShowExtrafanartsWidth() As Integer = 0
    Public Property TVShowFanartHeight() As Integer = 0
    Public Property TVShowFanartKeepExisting() As Boolean = False
    Public Property TVShowFanartPrefSize() As Enums.TVFanartSize = Enums.TVFanartSize.Any
    Public Property TVShowFanartPrefSizeOnly() As Boolean = False
    Public Property TVShowFanartResize() As Boolean = False
    Public Property TVShowFanartWidth() As Integer = 0
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
    Public Property TVShowKeyartHeight() As Integer = 0
    Public Property TVShowKeyartKeepExisting() As Boolean = False
    Public Property TVShowKeyartPrefSize() As Enums.TVPosterSize = Enums.TVPosterSize.Any
    Public Property TVShowKeyartPrefSizeOnly() As Boolean = False
    Public Property TVShowKeyartResize() As Boolean = False
    Public Property TVShowKeyartWidth() As Integer = 0
    Public Property TVShowLandscapeKeepExisting() As Boolean = False
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
    Public Property TVShowPosterHeight() As Integer = 0
    Public Property TVShowPosterKeepExisting() As Boolean = False
    Public Property TVShowPosterPrefSize() As Enums.TVPosterSize = Enums.TVPosterSize.Any
    Public Property TVShowPosterPrefSizeOnly() As Boolean = False
    Public Property TVShowPosterResize() As Boolean = False
    Public Property TVShowPosterWidth() As Integer = 0
    Public Property TVShowProperCase() As Boolean = True
    Public Property TVShowThemeDefaultSearch() As String = "theme soundtrack"
    Public Property TVShowThemeKeepExisting() As Boolean = False
    Public Property TVSkipLessThan() As Integer = 0
    Public Property Username() As String = String.Empty
    Public Property Version() As String = String.Empty

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
        'Cocotus, Load from central "Settings" folder if it exists!
        Dim configpath As String = Path.Combine(Master.SettingsPath, "Settings.xml")

        Try
            If File.Exists(configpath) Then
                Dim objStreamReader As New StreamReader(configpath)
                Dim xXMLSettings As New XmlSerializer(GetType(Settings))

                Master.eSettings = CType(xXMLSettings.Deserialize(objStreamReader), Settings)
                objStreamReader.Close()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            _Logger.Info("An attempt is made to repair the Settings.xml")
            Try
                Using srSettings As New StreamReader(configpath)
                    Dim sSettings As String = srSettings.ReadToEnd
                    'old Fanart/Poster sizes
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefSize>Xlrg<", "PrefSize>Any<")
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefSize>Lrg<", "PrefSize>Any<")
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefSize>Mid<", "PrefSize>Any<")
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefSize>Small<", "PrefSize>Any<")
                    'old Trailer Audio/Video quality
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "Qual>All<", "Qual>Any<")
                    'old allseasons/season/tvshow banner type
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefType>None<", "PrefType>Any<")
                    'old seasonposter size HD1000
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "<TVSeasonPosterPrefSize>HD1000</TVSeasonPosterPrefSize>",
                                                                             "<TVSeasonPosterPrefSize>Any</TVSeasonPosterPrefSize>")

                    Dim xXMLSettings As New XmlSerializer(GetType(Settings))
                    Using reader As TextReader = New StringReader(sSettings)
                        Master.eSettings = CType(xXMLSettings.Deserialize(reader), Settings)
                    End Using
                    _Logger.Info("Settings.xml successfully repaired")
                End Using
            Catch ex2 As Exception
                _Logger.Error(ex2, New StackFrame().GetMethod().Name)
                File.Copy(configpath, String.Concat(configpath, "_backup"), True)
                Master.eSettings = New Settings
            End Try
        End Try

        SetDefaultsForLists(Enums.DefaultType.All, False)

        ' Fix added to avoid to have no movie NFO saved
        If Not (Master.eSettings.MovieUseBoxee Or Master.eSettings.MovieUseEden Or Master.eSettings.MovieUseExpert Or Master.eSettings.MovieUseFrodo Or Master.eSettings.MovieUseNMJ Or Master.eSettings.MovieUseYAMJ) Then
            Master.eSettings.MovieUseFrodo = True
            Master.eSettings.MovieActorThumbsFrodo = True
            Master.eSettings.MovieExtrafanartsFrodo = True
            Master.eSettings.MovieExtrathumbsFrodo = True
            Master.eSettings.MovieFanartFrodo = True
            Master.eSettings.MovieNFOFrodo = True
            Master.eSettings.MoviePosterFrodo = True
            Master.eSettings.MovieTrailerFrodo = True
            Master.eSettings.MovieScraperXBMCTrailerFormat = True
        End If

        ' Fix added to avoid to have no tv show NFO saved
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

    Public Sub Save()
        Try
            Dim xmlSerial As New XmlSerializer(GetType(Settings))
            Dim xmlWriter As New StreamWriter(Path.Combine(Master.SettingsPath, "Settings.xml"))
            xmlSerial.Serialize(xmlWriter, Master.eSettings)
            xmlWriter.Close()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Sub SetDefaultsForLists(ByVal type As Enums.DefaultType, ByVal force As Boolean)

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.MainTabSorting) AndAlso (force OrElse Master.eSettings.GeneralMainTabSorting.Count = 0) Then
            Master.eSettings.GeneralMainTabSorting.Clear()
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.Movie, .DefaultList = "movielist", .Order = 0, .Title = Master.eLang.GetString(36, "Movies")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.MovieSet, .DefaultList = "setslist", .Order = 1, .Title = Master.eLang.GetString(366, "Sets")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.TV, .DefaultList = "tvshowlist", .Order = 2, .Title = Master.eLang.GetString(653, "TV Shows")})
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.SortTokens) AndAlso force Then Master.eSettings.GeneralSortTokens = Master.eSettings.GeneralSortTokens.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleBlackList_TVSeason) AndAlso force Then Master.eSettings.TVScraperSeasonTitleBlacklist = Master.eSettings.TVScraperSeasonTitleBlacklist.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_Movie) AndAlso force Then Master.eSettings.MovieFilterCustom = Master.eSettings.MovieFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_TVEpisode) AndAlso force Then Master.eSettings.TVEpisodeFilterCustom = Master.eSettings.TVEpisodeFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TitleFilters_TVShow) AndAlso force Then Master.eSettings.TVShowFilterCustom = Master.eSettings.TVShowFilterCustom.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidSubtitleExts) AndAlso force Then Master.eSettings.FileSystemValidSubtitlesExts = Master.eSettings.FileSystemValidSubtitlesExts.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidThemeExts) AndAlso force Then Master.eSettings.FileSystemValidThemeExts = Master.eSettings.FileSystemValidThemeExts.GetDefaults
        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.ValidVideoExts) AndAlso force Then Master.eSettings.FileSystemValidExts = Master.eSettings.FileSystemValidExts.GetDefaults

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TVShowMatching) AndAlso (force OrElse Master.eSettings.TVShowMatching.Count <= 0) Then
            Master.eSettings.TVShowMatching.Clear()
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 0, .byDate = False, .defaultSeason = -2, .Regexp = "s([0-9]+)[ ._-]*e([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 1, .byDate = False, .defaultSeason = 1, .Regexp = "[\\._ -]()e(?:p[ ._-]?)?([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 2, .byDate = True, .defaultSeason = -2, .Regexp = "([0-9]{4})[.-]([0-9]{2})[.-]([0-9]{2})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 3, .byDate = True, .defaultSeason = -2, .Regexp = "([0-9]{2})[.-]([0-9]{2})[.-]([0-9]{4})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 4, .byDate = False, .defaultSeason = -2, .Regexp = "[\\\/._ \[\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 5, .byDate = False, .defaultSeason = -2, .Regexp = "[\\\/._ -]([0-9]+)([0-9][0-9](?:(?:[a-i]|\.[1-9])(?![0-9]))?)([._ -][^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 6, .byDate = False, .defaultSeason = 1, .Regexp = "[\\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\\/]*)$"})
        End If

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.MovieListSorting) AndAlso (force OrElse Master.eSettings.MovieGeneralMediaListSorting.Count <= 0) Then
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

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.MovieSetListSorting) AndAlso (force OrElse Master.eSettings.MovieSetGeneralMediaListSorting.Count <= 0) Then
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

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TVEpisodeListSorting) AndAlso (force OrElse Master.eSettings.TVGeneralEpisodeListSorting.Count <= 0) Then
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

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TVSeasonListSorting) AndAlso (force OrElse Master.eSettings.TVGeneralSeasonListSorting.Count <= 0) Then
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

        If (type = Enums.DefaultType.All OrElse type = Enums.DefaultType.TVShowListSorting) AndAlso (force OrElse Master.eSettings.TVGeneralShowListSorting.Count <= 0) Then
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

    Public Function GetMovieSetsArtworkPaths() As List(Of String)
        Dim Paths As New List(Of String)
        If Not String.IsNullOrEmpty(MovieSetPathExpertSingle) Then Paths.Add(MovieSetPathExpertSingle)
        If Not String.IsNullOrEmpty(MovieSetPathExtended) Then Paths.Add(MovieSetPathExtended)
        If Not String.IsNullOrEmpty(MovieSetPathMSAA) Then Paths.Add(MovieSetPathMSAA)
        Paths = Paths.Distinct().ToList() 'remove double entries
        Return Paths
    End Function

    Public Function MovieActorThumbsAnyEnabled() As Boolean
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
                        "(?i)[\W_]\(?\d{4}\)?.*",               'year in brakets
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
                        "[\W_]PAL[\W_]?.*",                     'convert dots to space
                        "\.[->] ", "_[->] "                     'convert underscore to space
                    }
                Case Enums.DefaultType.TitleFilters_TVEpisode
                    Return New ExtendedListOfString(listType) From {
                        "[\W_]\(?\d{4}\)?.*",
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
                        "[\W_]\(?\d{4}\,?.*",
                        "(?i,[\W_]blu[\W_]?ray.*",
                        "(?i,[\W_]bd[\W_]?rip.*",
                        "(?i,[\W_]dvd.*",
                        "(?i,[\W_]720.*",
                        "(?i,[\W_]1080.*",
                        "(?i,[\W_]1440.*",
                        "(?i,[\W_]2160.*",
                        "(?i,[\W_]4k.*",
                        "(?i,[\W_]ac3.*",
                        "(?i,[\W_]dts.*",
                        "(?i,[\W_]divx.*",
                        "(?i,[\W_]xvid.*",
                        "(?i,[\W_]dc[\W_]?.*",
                        "(?i,[\W_]dir(ector'?s?,?\s?cut.*",
                        "(?i,[\W_]extended.*",
                        "(?i,[\W_]hd(tv,?.*",
                        "(?i,[\W_]unrated.*",
                        "(?i,[\W_]uncut.*",
                        "(?i,[\W_]([a-z]{3}|multi,[sd]ub.*",
                        "(?i,[\W_]\[offline\].*",
                        "(?i,[\W_]ntsc.*",
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
            End Select
            Return Nothing
        End Function

#End Region 'Methods

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

#Region "Fields"

        Private _filetype As String
        Private _metadata As MediaContainers.Fileinfo

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property FileType() As String
            Get
                Return _filetype
            End Get
            Set(ByVal value As String)
                _filetype = value
            End Set
        End Property

        Public Property MetaData() As MediaContainers.Fileinfo
            Get
                Return _metadata
            End Get
            Set(ByVal value As MediaContainers.Fileinfo)
                _metadata = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _filetype = String.Empty
            _metadata = New MediaContainers.Fileinfo
        End Sub

#End Region 'Methods

    End Class

    Public Class ListSorting

#Region "Fields"

        Private _column As String
        Private _displayindex As Integer
        Private _hide As Boolean
        Private _labelid As Integer
        Private _labeltext As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"
        ''' <summary>
        ''' Column name in database (need to be exactly like column name in DB)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Column() As String
            Get
                Return _column
            End Get
            Set(ByVal value As String)
                _column = value
            End Set
        End Property

        Public Property DisplayIndex() As Integer
            Get
                Return _displayindex
            End Get
            Set(ByVal value As Integer)
                _displayindex = value
            End Set
        End Property
        ''' <summary>
        ''' Hide or show column in media lists
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Hide() As Boolean
            Get
                Return _hide
            End Get
            Set(ByVal value As Boolean)
                _hide = value
            End Set
        End Property
        ''' <summary>
        ''' ID of string in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelID() As Integer
            Get
                Return _labelid
            End Get
            Set(ByVal value As Integer)
                _labelid = value
            End Set
        End Property
        ''' <summary>
        ''' Default text for the LabelID in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelText() As String
            Get
                Return _labeltext
            End Get
            Set(ByVal value As String)
                _labeltext = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _column = String.Empty
            _displayindex = -1
            _hide = False
            _labelid = 1
            _labeltext = String.Empty
        End Sub

#End Region 'Methods

    End Class

    Public Class regexp

#Region "Fields"

        Private _bydate As Boolean
        Private _defaultSeason As Integer
        Private _id As Integer
        Private _regexp As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property byDate() As Boolean
            Get
                Return _bydate
            End Get
            Set(ByVal value As Boolean)
                _bydate = value
            End Set
        End Property

        Public Property defaultSeason() As Integer
            Get
                Return _defaultSeason
            End Get
            Set(ByVal value As Integer)
                _defaultSeason = value
            End Set
        End Property

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property

        Public Property Regexp() As String
            Get
                Return _regexp
            End Get
            Set(ByVal value As String)
                _regexp = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            _bydate = False
            _defaultSeason = -2 '-1 is reserved for "* All Seasons" entry
            _id = -1
            _regexp = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class