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
Imports System.Windows.Forms
Imports System.Xml.Serialization


<Serializable()>
<XmlRoot("Settings")>
Public Class Settings

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Const ExtraImagesLimit As Integer = 20
    Private _movieextrafanartslimit As Integer = 4
    Private _movieextrathumbslimit As Integer = 4
    Private _tvshowextrafanartslimit As Integer  = 4

#End Region 'Fields 

#Region "Properties"
    <XmlArray("EmberModules")>
    <XmlArrayItem("Module")>
    Public Property Addons() As List(Of AddonsManager.XMLAddonClass) = New List(Of AddonsManager.XMLAddonClass)
    Public Property FileSystemNoStackExts() As List(Of String) = New List(Of String)
    Public Property FileSystemValidExts() As List(Of String) = New List(Of String)
    Public Property FileSystemValidSubtitlesExts() As List(Of String) = New List(Of String)
    Public Property FileSystemValidThemeExts() As List(Of String) = New List(Of String)
    Public Property GeneralAudioCodecMapping As List(Of CodecMapping) = New List(Of CodecMapping)
    Public Property GeneralCheckUpdates() As Boolean = False
    Public Property GeneralDateAddedIgnoreNFO() As Boolean = False
    Public Property GeneralDateTime() As Enums.DateTime = Enums.DateTime.Now
    Public Property GeneralDigitGrpSymbolVotes() As Boolean = False
    Public Property GeneralDisplayBanner() As Boolean = True
    Public Property GeneralDisplayCharacterArt() As Boolean = True
    Public Property GeneralDisplayClearArt() As Boolean = True
    Public Property GeneralDisplayClearLogo() As Boolean = True
    Public Property GeneralDisplayDiscArt() As Boolean = True
    Public Property GeneralDisplayFanart() As Boolean = True
    Public Property GeneralDisplayFanartSmall() As Boolean = True
    Public Property GeneralDisplayKeyArt() As Boolean = True
    Public Property GeneralDisplayLandscape() As Boolean = True
    Public Property GeneralDisplayPoster() As Boolean = True
    Public Property GeneralDoubleClickScrape() As Boolean = False
    Public Property GeneralFilterPanelIsRaisedMovie() As Boolean = False
    Public Property GeneralFilterPanelIsRaisedMovieSet() As Boolean = False
    Public Property GeneralFilterPanelIsRaisedTVShow() As Boolean = False
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
    Public Property GeneralVideoCodecMapping As List(Of CodecMapping) = New List(Of CodecMapping)
    Public Property GeneralVideoSourceByExtension() As List(Of VideoSourceByExtension) = New List(Of VideoSourceByExtension)
    Public Property GeneralVideoSourceByExtensionEnabled() As Boolean = False
    Public Property GeneralVideoSourceByRegex As List(Of VideoSourceByRegex) = New List(Of VideoSourceByRegex)
    Public Property GeneralVideoSourceByRegexEnabled As Boolean = True
    Public Property GeneralVirtualDriveLetter() As String = String.Empty
    Public Property GeneralVirtualDriveBinPath() As String = String.Empty
    Public Property GeneralWindowLoc() As Point = New Point(10, 10)
    Public Property GeneralWindowSize() As Size = New Size(1024, 768)
    Public Property GeneralWindowState() As FormWindowState = FormWindowState.Maximized
    Public Property MovieActorThumbsEden() As Boolean = False
    Public Property MovieActorThumbsExpertBDMV() As Boolean = False
    Public Property MovieActorThumbsExpertMulti() As Boolean = False
    Public Property MovieActorThumbsExpertSingle() As Boolean = False
    Public Property MovieActorThumbsExpertVTS() As Boolean = False
    Public Property MovieActorThumbsExtExpertBDMV() As String = ".jpg"
    Public Property MovieActorThumbsExtExpertMulti() As String = ".jpg"
    Public Property MovieActorThumbsExtExpertSingle() As String = ".jpg"
    Public Property MovieActorThumbsExtExpertVTS() As String = ".jpg"
    Public Property MovieActorThumbsFrodo() As Boolean = False
    Public Property MovieActorThumbsKeepExisting() As Boolean = False
    Public Property MovieBackdropsAuto() As Boolean = False
    Public Property MovieBackdropsPath() As String = String.Empty
    Public Property MovieBannerAD() As Boolean = False
    Public Property MovieBannerExpertBDMV() As String = String.Empty
    Public Property MovieBannerExpertMulti() As String = String.Empty
    Public Property MovieBannerExpertSingle() As String = String.Empty
    Public Property MovieBannerExpertVTS() As String = String.Empty
    Public Property MovieBannerExtended() As Boolean = False
    Public Property MovieBannerHeight() As Integer = 0
    Public Property MovieBannerKeepExisting() As Boolean = False
    Public Property MovieBannerNMJ() As Boolean = False
    Public Property MovieBannerPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieBannerPrefSizeOnly() As Boolean = False
    Public Property MovieBannerResize() As Boolean = False
    Public Property MovieBannerWidth() As Integer = 0
    Public Property MovieBannerYAMJ() As Boolean = False
    Public Property MovieCleanDB() As Boolean = False
    Public Property MovieClearArtAD() As Boolean = False
    Public Property MovieClearArtExpertBDMV() As String = String.Empty
    Public Property MovieClearArtExpertMulti() As String = String.Empty
    Public Property MovieClearArtExpertSingle() As String = String.Empty
    Public Property MovieClearArtExpertVTS() As String = String.Empty
    Public Property MovieClearArtExtended() As Boolean = False
    Public Property MovieClearArtKeepExisting() As Boolean = False
    Public Property MovieClearArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieClearArtPrefSizeOnly() As Boolean = False
    Public Property MovieClearLogoAD() As Boolean = False
    Public Property MovieClearLogoExpertBDMV() As String = String.Empty
    Public Property MovieClearLogoExpertMulti() As String = String.Empty
    Public Property MovieClearLogoExpertSingle() As String = String.Empty
    Public Property MovieClearLogoExpertVTS() As String = String.Empty
    Public Property MovieClearLogoExtended() As Boolean = False
    Public Property MovieClearLogoKeepExisting() As Boolean = False
    Public Property MovieClearLogoPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieClearLogoPrefSizeOnly() As Boolean = False
    Public Property MovieClickScrape() As Boolean = False
    Public Property MovieClickScrapeAsk() As Boolean = False
    Public Property MovieDiscArtAD() As Boolean = False
    Public Property MovieDiscArtExpertBDMV() As String = String.Empty
    Public Property MovieDiscArtExpertMulti() As String = String.Empty
    Public Property MovieDiscArtExpertSingle() As String = String.Empty
    Public Property MovieDiscArtExpertVTS() As String = String.Empty
    Public Property MovieDiscArtExtended() As Boolean = False
    Public Property MovieDiscArtKeepExisting() As Boolean = False
    Public Property MovieDiscArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieDiscArtPrefSizeOnly() As Boolean = False
    Public Property MovieExtrafanartsEden() As Boolean = False
    Public Property MovieExtrafanartsExpertBDMV() As Boolean = False
    Public Property MovieExtrafanartsExpertSingle() As Boolean = False
    Public Property MovieExtrafanartsExpertVTS() As Boolean = False
    Public Property MovieExtrafanartsFrodo() As Boolean = False
    Public Property MovieExtrafanartsHeight() As Integer = 0
    Public Property MovieExtrafanartsKeepExisting() As Boolean = False
    Public Property MovieExtrafanartsLimit() As Integer
        Get
            Return _movieextrafanartslimit
        End Get
        Set(value As Integer)
            If value > ExtraImagesLimit OrElse value = 0 Then
                _movieextrafanartslimit = 20
            Else
                _movieextrafanartslimit = value
            End If
        End Set
    End Property
    Public Property MovieExtrafanartsPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieExtrafanartsPrefSizeOnly() As Boolean = False
    Public Property MovieExtrafanartsPreselect() As Boolean = True
    Public Property MovieExtrafanartsResize() As Boolean = False
    Public Property MovieExtrafanartsWidth() As Integer = 0
    Public Property MovieExtrathumbsVideoExtraction() As Boolean = False
    Public Property MovieExtrathumbsVideoExtractionPref() As Boolean = False
    Public Property MovieExtrathumbsEden() As Boolean = False
    Public Property MovieExtrathumbsExpertBDMV() As Boolean = False
    Public Property MovieExtrathumbsExpertSingle() As Boolean = False
    Public Property MovieExtrathumbsExpertVTS() As Boolean = False
    Public Property MovieExtrathumbsFrodo() As Boolean = False
    Public Property MovieExtrathumbsHeight() As Integer = 0
    Public Property MovieExtrathumbsKeepExisting() As Boolean = False
    Public Property MovieExtrathumbsLimit() As Integer
        Get
            Return _movieextrathumbslimit
        End Get
        Set(value As Integer)
            If value > ExtraImagesLimit OrElse value = 0 Then
                _movieextrathumbslimit = 20
            Else
                _movieextrathumbslimit = value
            End If
        End Set
    End Property
    Public Property MovieExtrathumbsPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieExtrathumbsPrefSizeOnly() As Boolean = False
    Public Property MovieExtrathumbsPreselect() As Boolean = True
    Public Property MovieExtrathumbsResize() As Boolean = False
    Public Property MovieExtrathumbsWidth() As Integer = 0
    Public Property MovieFanartBoxee() As Boolean = False
    Public Property MovieFanartEden() As Boolean = False
    Public Property MovieFanartExpertBDMV() As String = String.Empty
    Public Property MovieFanartExpertMulti() As String = String.Empty
    Public Property MovieFanartExpertSingle() As String = String.Empty
    Public Property MovieFanartExpertVTS() As String = String.Empty
    Public Property MovieFanartFrodo() As Boolean = False
    Public Property MovieFanartHeight() As Integer = 0
    Public Property MovieFanartKeepExisting() As Boolean = False
    Public Property MovieFanartNMJ() As Boolean = False
    Public Property MovieFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieFanartPrefSizeOnly() As Boolean = False
    Public Property MovieFanartResize() As Boolean = False
    Public Property MovieFanartWidth() As Integer = 0
    Public Property MovieFanartYAMJ() As Boolean = False
    Public Property MovieFilterCustom() As List(Of String) = New List(Of String)
    Public Property MovieFilterCustomIsEmpty() As Boolean = False
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
    Public Property MovieGeneralLanguage() As String = "en-US"
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
    Public Property MovieKeyArtExtended() As Boolean = False
    Public Property MovieKeyArtHeight() As Integer = 0
    Public Property MovieKeyArtKeepExisting() As Boolean = False
    Public Property MovieKeyArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieKeyArtPrefSizeOnly() As Boolean = False
    Public Property MovieKeyArtResize() As Boolean = False
    Public Property MovieKeyArtWidth() As Integer = 0
    Public Property MovieLandscapeAD() As Boolean = False
    Public Property MovieLandscapeExpertBDMV() As String = String.Empty
    Public Property MovieLandscapeExpertMulti() As String = String.Empty
    Public Property MovieLandscapeExpertSingle() As String = String.Empty
    Public Property MovieLandscapeExpertVTS() As String = String.Empty
    Public Property MovieLandscapeExtended() As Boolean = False
    Public Property MovieLandscapeKeepExisting() As Boolean = False
    Public Property MovieLandscapePrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieLandscapePrefSizeOnly() As Boolean = False
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
    Public Property MovieLockRating() As Boolean = False
    Public Property MovieLockReleaseDate() As Boolean = False
    Public Property MovieLockRuntime() As Boolean = False
    Public Property MovieLockStudio() As Boolean = False
    Public Property MovieLockTagline() As Boolean = False
    Public Property MovieLockTags() As Boolean = False
    Public Property MovieLockTitle() As Boolean = False
    Public Property MovieLockTop250() As Boolean = False
    Public Property MovieLockTrailer() As Boolean = False
    Public Property MovieLockUserRating() As Boolean = False
    Public Property MovieLockYear() As Boolean = False
    Public Property MovieMetadataPerFileType() As List(Of MetadataPerType) = New List(Of MetadataPerType)
    Public Property MovieMissingBanner() As Boolean = False
    Public Property MovieMissingClearArt() As Boolean = False
    Public Property MovieMissingClearLogo() As Boolean = False
    Public Property MovieMissingDiscArt() As Boolean = False
    Public Property MovieMissingExtrafanarts() As Boolean = False
    Public Property MovieMissingExtrathumbs() As Boolean = False
    Public Property MovieMissingFanart() As Boolean = False
    Public Property MovieMissingKeyArt() As Boolean = False
    Public Property MovieMissingLandscape() As Boolean = False
    Public Property MovieMissingNFO() As Boolean = False
    Public Property MovieMissingPoster() As Boolean = False
    Public Property MovieMissingSubtitles() As Boolean = False
    Public Property MovieMissingTheme() As Boolean = False
    Public Property MovieMissingTrailer() As Boolean = False
    Public Property MovieNFOBoxee() As Boolean = False
    Public Property MovieNFOEden() As Boolean = False
    Public Property MovieNFOExpertBDMV() As String = String.Empty
    Public Property MovieNFOExpertMulti() As String = String.Empty
    Public Property MovieNFOExpertSingle() As String = String.Empty
    Public Property MovieNFOExpertVTS() As String = String.Empty
    Public Property MovieNFOFrodo() As Boolean = False
    Public Property MovieNFONMJ() As Boolean = False
    Public Property MovieNFOYAMJ() As Boolean = False
    Public Property MoviePosterBoxee() As Boolean = False
    Public Property MoviePosterEden() As Boolean = False
    Public Property MoviePosterExpertBDMV() As String = String.Empty
    Public Property MoviePosterExpertMulti() As String = String.Empty
    Public Property MoviePosterExpertSingle() As String = String.Empty
    Public Property MoviePosterExpertVTS() As String = String.Empty
    Public Property MoviePosterFrodo() As Boolean = False
    Public Property MoviePosterHeight() As Integer = 0
    Public Property MoviePosterKeepExisting() As Boolean = False
    Public Property MoviePosterNMJ() As Boolean = False
    Public Property MoviePosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MoviePosterPrefSizeOnly() As Boolean = False
    Public Property MoviePosterResize() As Boolean = False
    Public Property MoviePosterWidth() As Integer = 0
    Public Property MoviePosterYAMJ() As Boolean = False
    Public Property MovieProperCase() As Boolean = True
    Public Property MovieRecognizeVTSExpertVTS() As Boolean = False
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
    Public Property MovieScraperMPAA() As Boolean = True
    Public Property MovieScraperMPAANotRated() As String = String.Empty
    Public Property MovieScraperMetaDataIFOScan() As Boolean = True
    Public Property MovieScraperMetaDataScan() As Boolean = True
    Public Property MovieScraperOriginalTitle() As Boolean = True
    Public Property MovieScraperOriginalTitleAsTitle() As Boolean = False
    Public Property MovieScraperOutline() As Boolean = True
    Public Property MovieScraperOutlineLimit() As Integer = 350
    Public Property MovieScraperPlot() As Boolean = True
    Public Property MovieScraperPlotForOutline() As Boolean = False
    Public Property MovieScraperPlotForOutlineIfEmpty() As Boolean = False
    Public Property MovieScraperRating() As Boolean = True
    Public Property MovieScraperRelease() As Boolean = True
    Public Property MovieScraperRuntime() As Boolean = True
    Public Property MovieScraperStudio() As Boolean = True
    Public Property MovieScraperStudioLimit() As Integer = 0
    Public Property MovieScraperStudioWithImgOnly() As Boolean = False
    Public Property MovieScraperTagline() As Boolean = True
    Public Property MovieScraperTitle() As Boolean = True
    Public Property MovieScraperTop250() As Boolean = True
    Public Property MovieScraperTrailer() As Boolean = True
    Public Property MovieScraperUseDetailView() As Boolean = False
    Public Property MovieScraperUseMDDuration() As Boolean = True
    Public Property MovieScraperUserRating() As Boolean = True
    Public Property MovieScraperXBMCTrailerFormat() As Boolean = False
    Public Property MovieScraperYear() As Boolean = True
    Public Property MovieSetBannerExpertSingle() As String = String.Empty
    Public Property MovieSetBannerExtended() As Boolean = False
    Public Property MovieSetBannerHeight() As Integer = 0
    Public Property MovieSetBannerKeepExisting() As Boolean = False
    Public Property MovieSetBannerMSAA() As Boolean = False
    Public Property MovieSetBannerPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetBannerPrefSizeOnly() As Boolean = False
    Public Property MovieSetBannerResize() As Boolean = False
    Public Property MovieSetBannerWidth() As Integer = 0
    Public Property MovieSetCleanDB() As Boolean = False
    Public Property MovieSetCleanFiles() As Boolean = False
    Public Property MovieSetClearArtExpertSingle() As String = String.Empty
    Public Property MovieSetClearArtExtended() As Boolean = False
    Public Property MovieSetClearArtKeepExisting() As Boolean = False
    Public Property MovieSetClearArtMSAA() As Boolean = False
    Public Property MovieSetClearArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetClearArtPrefSizeOnly() As Boolean = False
    Public Property MovieSetClearLogoExpertSingle() As String = String.Empty
    Public Property MovieSetClearLogoExtended() As Boolean = False
    Public Property MovieSetClearLogoKeepExisting() As Boolean = False
    Public Property MovieSetClearLogoMSAA() As Boolean = False
    Public Property MovieSetClearLogoPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetClearLogoPrefSizeOnly() As Boolean = False
    Public Property MovieSetClickScrape() As Boolean = False
    Public Property MovieSetClickScrapeAsk() As Boolean = False
    Public Property MovieSetDiscArtExpertSingle() As String = String.Empty
    Public Property MovieSetDiscArtExtended() As Boolean = False
    Public Property MovieSetDiscArtKeepExisting() As Boolean = False
    Public Property MovieSetDiscArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetDiscArtPrefSizeOnly() As Boolean = False
    Public Property MovieSetFanartExpertSingle() As String = String.Empty
    Public Property MovieSetFanartExtended() As Boolean = False
    Public Property MovieSetFanartHeight() As Integer = 0
    Public Property MovieSetFanartKeepExisting() As Boolean = False
    Public Property MovieSetFanartMSAA() As Boolean = False
    Public Property MovieSetFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
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
    Public Property MovieSetKeyArtExtended() As Boolean = False
    Public Property MovieSetKeyArtHeight() As Integer = 0
    Public Property MovieSetKeyArtKeepExisting() As Boolean = False
    Public Property MovieSetKeyArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetKeyArtPrefSizeOnly() As Boolean = False
    Public Property MovieSetKeyArtResize() As Boolean = False
    Public Property MovieSetKeyArtWidth() As Integer = 0
    Public Property MovieSetLandscapeExpertSingle() As String = String.Empty
    Public Property MovieSetLandscapeExtended() As Boolean = False
    Public Property MovieSetLandscapeKeepExisting() As Boolean = False
    Public Property MovieSetLandscapeMSAA() As Boolean = False
    Public Property MovieSetLandscapePrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetLandscapePrefSizeOnly() As Boolean = False
    Public Property MovieSetLockPlot() As Boolean = False
    Public Property MovieSetLockTitle() As Boolean = False
    Public Property MovieSetMissingBanner() As Boolean = False
    Public Property MovieSetMissingClearArt() As Boolean = False
    Public Property MovieSetMissingClearLogo() As Boolean = False
    Public Property MovieSetMissingDiscArt() As Boolean = False
    Public Property MovieSetMissingFanart() As Boolean = False
    Public Property MovieSetMissingKeyArt() As Boolean = False
    Public Property MovieSetMissingLandscape() As Boolean = False
    Public Property MovieSetMissingNFO() As Boolean = False
    Public Property MovieSetMissingPoster() As Boolean = False
    Public Property MovieSetNFOExpertSingle() As String = String.Empty
    Public Property MovieSetPathExpertSingle() As String = String.Empty
    Public Property MovieSetPathExtended() As String = String.Empty
    Public Property MovieSetPathMSAA() As String = String.Empty
    Public Property MovieSetPosterExpertSingle() As String = String.Empty
    Public Property MovieSetPosterExtended() As Boolean = False
    Public Property MovieSetPosterHeight() As Integer = 0
    Public Property MovieSetPosterKeepExisting() As Boolean = False
    Public Property MovieSetPosterMSAA() As Boolean = False
    Public Property MovieSetPosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property MovieSetPosterPrefSizeOnly() As Boolean = False
    Public Property MovieSetPosterResize() As Boolean = False
    Public Property MovieSetPosterWidth() As Integer = 0
    Public Property MovieSetScraperPlot() As Boolean = True
    Public Property MovieSetScraperTitle() As Boolean = True
    Public Property MovieSetSortTokens() As List(Of String) = New List(Of String)
    Public Property MovieSetSortTokensIsEmpty() As Boolean = False
    Public Property MovieSetUseExpert() As Boolean = False
    Public Property MovieSetUseExtended() As Boolean = False
    Public Property MovieSetUseMSAA() As Boolean = False
    Public Property MovieSkipLessThan() As Integer = 0
    Public Property MovieSortBeforeScan() As Boolean = False
    Public Property MovieSortTokens() As List(Of String) = New List(Of String)
    Public Property MovieSortTokensIsEmpty() As Boolean = False
    Public Property MovieStackExpertMulti() As Boolean = False
    Public Property MovieStackExpertSingle() As Boolean = False
    Public Property MovieThemeKeepExisting() As Boolean = False
    Public Property MovieThemeTvTunesCustom() As Boolean = False
    Public Property MovieThemeTvTunesCustomPath() As String = String.Empty
    Public Property MovieThemeTvTunesEnable() As Boolean = False
    Public Property MovieThemeTvTunesMoviePath() As Boolean = False
    Public Property MovieThemeTvTunesSub() As Boolean = False
    Public Property MovieThemeTvTunesSubDir() As String = String.Empty
    Public Property MovieTrailerDefaultSearch() As String = "trailer"
    Public Property MovieTrailerEden() As Boolean = False
    Public Property MovieTrailerExpertBDMV() As String = String.Empty
    Public Property MovieTrailerExpertMulti() As String = String.Empty
    Public Property MovieTrailerExpertSingle() As String = String.Empty
    Public Property MovieTrailerExpertVTS() As String = String.Empty
    Public Property MovieTrailerFrodo() As Boolean = False
    Public Property MovieTrailerKeepExisting() As Boolean = False
    Public Property MovieTrailerMinVideoQual() As Enums.TrailerVideoQuality = Enums.TrailerVideoQuality.Any
    Public Property MovieTrailerNMJ() As Boolean = False
    Public Property MovieTrailerPrefVideoQual() As Enums.TrailerVideoQuality = Enums.TrailerVideoQuality.Any
    Public Property MovieTrailerYAMJ() As Boolean = False
    Public Property MovieUnstackExpertMulti() As Boolean = False
    Public Property MovieUnstackExpertSingle() As Boolean = False
    Public Property MovieUseAD() As Boolean = False
    Public Property MovieUseBaseDirectoryExpertBDMV() As Boolean = False
    Public Property MovieUseBaseDirectoryExpertVTS() As Boolean = False
    Public Property MovieUseBoxee() As Boolean = False
    Public Property MovieUseEden() As Boolean = False
    Public Property MovieUseExpert() As Boolean = False
    Public Property MovieUseExtended() As Boolean = False
    Public Property MovieUseFrodo() As Boolean = False
    Public Property MovieUseNMJ() As Boolean = False
    Public Property MovieUseYAMJ() As Boolean = False
    Public Property MovieXBMCProtectVTSBDMV() As Boolean = False
    Public Property MovieYAMJWatchedFile() As Boolean = False
    Public Property MovieYAMJWatchedFolder() As String = String.Empty
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
    Public Property TVAllSeasonsBannerExpert() As String = String.Empty
    Public Property TVAllSeasonsBannerHeight() As Integer = 0
    Public Property TVAllSeasonsBannerKeepExisting() As Boolean = False
    Public Property TVAllSeasonsBannerPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVAllSeasonsBannerPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVAllSeasonsBannerResize() As Boolean = False
    Public Property TVAllSeasonsBannerWidth() As Integer = 0
    Public Property TVAllSeasonsFanartExpert() As String = String.Empty
    Public Property TVAllSeasonsFanartHeight() As Integer = 0
    Public Property TVAllSeasonsFanartKeepExisting() As Boolean = False
    Public Property TVAllSeasonsFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVAllSeasonsFanartPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsFanartResize() As Boolean = False
    Public Property TVAllSeasonsFanartWidth() As Integer = 0
    Public Property TVAllSeasonsLandscapeExpert() As String = String.Empty
    Public Property TVAllSeasonsLandscapeKeepExisting() As Boolean = False
    Public Property TVAllSeasonsLandscapePrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVAllSeasonsLandscapePrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsPosterExpert() As String = String.Empty
    Public Property TVAllSeasonsPosterHeight() As Integer = 0
    Public Property TVAllSeasonsPosterKeepExisting() As Boolean = False
    Public Property TVAllSeasonsPosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVAllSeasonsPosterPrefSizeOnly() As Boolean = False
    Public Property TVAllSeasonsPosterResize() As Boolean = False
    Public Property TVAllSeasonsPosterWidth() As Integer = 0
    Public Property TVCleanDB() As Boolean = False
    Public Property TVDisplayMissingEpisodes() As Boolean = True
    Public Property TVEpisodeActorThumbsExpert() As Boolean = False
    Public Property TVEpisodeActorThumbsExtExpert() As String = ".jpg"
    Public Property TVEpisodeActorThumbsFrodo() As Boolean = False
    Public Property TVEpisodeActorThumbsKeepExisting() As Boolean = False
    Public Property TVEpisodeFanartExpert() As String = String.Empty
    Public Property TVEpisodeFanartHeight() As Integer = 0
    Public Property TVEpisodeFanartKeepExisting() As Boolean = False
    Public Property TVEpisodeFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVEpisodeFanartPrefSizeOnly() As Boolean = False
    Public Property TVEpisodeFanartResize() As Boolean = False
    Public Property TVEpisodeFanartWidth() As Integer = 0
    Public Property TVEpisodeFilterCustom() As List(Of String) = New List(Of String)
    Public Property TVEpisodeFilterCustomIsEmpty() As Boolean = False
    Public Property TVEpisodeMissingFanart() As Boolean = False
    Public Property TVEpisodeMissingNFO() As Boolean = False
    Public Property TVEpisodeMissingPoster() As Boolean = False
    Public Property TVEpisodeNFOBoxee() As Boolean = False
    Public Property TVEpisodeNFOExpert() As String = String.Empty
    Public Property TVEpisodeNFOFrodo() As Boolean = False
    Public Property TVEpisodeNFOYAMJ() As Boolean = False
    Public Property TVEpisodeNoFilter() As Boolean = True
    Public Property TVEpisodePosterBoxee() As Boolean = False
    Public Property TVEpisodePosterExpert() As String = String.Empty
    Public Property TVEpisodePosterFrodo() As Boolean = False
    Public Property TVEpisodePosterHeight() As Integer = 0
    Public Property TVEpisodePosterKeepExisting() As Boolean = False
    Public Property TVEpisodePosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVEpisodePosterPrefSizeOnly() As Boolean = False
    Public Property TVEpisodePosterResize() As Boolean = False
    Public Property TVEpisodePosterWidth() As Integer = 0
    Public Property TVEpisodePosterVideoExtraction() As Boolean = True
    Public Property TVEpisodePosterVideoExtractionPref() As Boolean = False
    Public Property TVEpisodePosterYAMJ() As Boolean = False
    Public Property TVEpisodeProperCase() As Boolean = True
    Public Property TVGeneralClickScrape() As Boolean = False
    Public Property TVGeneralClickScrapeAsk() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonEnabled() As Boolean = False
    Public Property TVGeneralCustomScrapeButtonModifierType() As Enums.ModifierType = Enums.ModifierType.All
    Public Property TVGeneralCustomScrapeButtonScrapeType() As Enums.ScrapeType = Enums.ScrapeType.NewSkip
    Public Property TVGeneralEpisodeListSorting() As List(Of ListSorting) = New List(Of ListSorting)
    Public Property TVGeneralFlagLang() As String = String.Empty
    Public Property TVGeneralLanguage() As String = "en-US"
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
    Public Property TVLockShowTitle() As Boolean = False
    Public Property TVLockShowUserRating() As Boolean = False
    Public Property TVMetadataPerFileType() As List(Of MetadataPerType) = New List(Of MetadataPerType)
    Public Property TVMultiPartMatching() As String = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
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
    Public Property TVScraperEpisodePlot() As Boolean = True
    Public Property TVScraperEpisodeRating() As Boolean = True
    Public Property TVScraperEpisodeRuntime() As Boolean = True
    Public Property TVScraperEpisodeTitle() As Boolean = True
    Public Property TVScraperEpisodeUserRating() As Boolean = True
    Public Property TVScraperMetaDataScan() As Boolean = True
    Public Property TVScraperOptionsOrdering() As Enums.EpisodeOrdering = Enums.EpisodeOrdering.Standard
    Public Property TVScraperSeasonAired() As Boolean = True
    Public Property TVScraperSeasonPlot() As Boolean = True
    Public Property TVScraperSeasonTitle() As Boolean = False
    Public Property TVScraperSeasonTitleBlacklist() As List(Of String) = New List(Of String)
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
    Public Property TVScraperShowTitle() As Boolean = True
    Public Property TVScraperShowUserRating() As Boolean = True
    Public Property TVScraperUseDisplaySeasonEpisode() As Boolean = True
    Public Property TVScraperUseMDDuration() As Boolean = True
    Public Property TVScraperUseSRuntimeForEp() As Boolean = True
    Public Property TVSeasonBannerExpert() As String = String.Empty
    Public Property TVSeasonBannerFrodo() As Boolean = False
    Public Property TVSeasonBannerHeight() As Integer = 0
    Public Property TVSeasonBannerKeepExisting() As Boolean = False
    Public Property TVSeasonBannerPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVSeasonBannerPrefSizeOnly() As Boolean = False
    Public Property TVSeasonBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVSeasonBannerResize() As Boolean = False
    Public Property TVSeasonBannerWidth() As Integer = 0
    Public Property TVSeasonBannerYAMJ() As Boolean = False
    Public Property TVSeasonFanartExpert() As String = String.Empty
    Public Property TVSeasonFanartFrodo() As Boolean = False
    Public Property TVSeasonFanartHeight() As Integer = 0
    Public Property TVSeasonFanartKeepExisting() As Boolean = False
    Public Property TVSeasonFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVSeasonFanartPrefSizeOnly() As Boolean = False
    Public Property TVSeasonFanartResize() As Boolean = False
    Public Property TVSeasonFanartWidth() As Integer = 0
    Public Property TVSeasonFanartYAMJ() As Boolean = False
    Public Property TVSeasonLandscapeAD() As Boolean = False
    Public Property TVSeasonLandscapeExpert() As String = String.Empty
    Public Property TVSeasonLandscapeExtended() As Boolean = False
    Public Property TVSeasonLandscapeKeepExisting() As Boolean = False
    Public Property TVSeasonLandscapePrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVSeasonLandscapePrefSizeOnly() As Boolean = False
    Public Property TVSeasonMissingBanner() As Boolean = False
    Public Property TVSeasonMissingFanart() As Boolean = False
    Public Property TVSeasonMissingLandscape() As Boolean = False
    Public Property TVSeasonMissingPoster() As Boolean = False
    Public Property TVSeasonPosterBoxee() As Boolean = False
    Public Property TVSeasonPosterExpert() As String = String.Empty
    Public Property TVSeasonPosterFrodo() As Boolean = False
    Public Property TVSeasonPosterHeight() As Integer = 0
    Public Property TVSeasonPosterKeepExisting() As Boolean = False
    Public Property TVSeasonPosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVSeasonPosterPrefSizeOnly() As Boolean = False
    Public Property TVSeasonPosterResize() As Boolean = False
    Public Property TVSeasonPosterWidth() As Integer = 0
    Public Property TVSeasonPosterYAMJ() As Boolean = False
    Public Property TVShowActorThumbsExpert() As Boolean = False
    Public Property TVShowActorThumbsExtExpert() As String = ".jpg"
    Public Property TVShowActorThumbsFrodo() As Boolean = False
    Public Property TVShowActorThumbsKeepExisting() As Boolean = False
    Public Property TVShowBannerBoxee() As Boolean = False
    Public Property TVShowBannerExpert() As String = String.Empty
    Public Property TVShowBannerFrodo() As Boolean = False
    Public Property TVShowBannerHeight() As Integer = 0
    Public Property TVShowBannerKeepExisting() As Boolean = False
    Public Property TVShowBannerPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowBannerPrefSizeOnly() As Boolean = False
    Public Property TVShowBannerPrefType() As Enums.TVBannerType = Enums.TVBannerType.Any
    Public Property TVShowBannerResize() As Boolean = False
    Public Property TVShowBannerWidth() As Integer = 0
    Public Property TVShowBannerYAMJ() As Boolean = False
    Public Property TVShowCharacterArtAD() As Boolean = False
    Public Property TVShowCharacterArtExpert() As String = String.Empty
    Public Property TVShowCharacterArtExtended() As Boolean = False
    Public Property TVShowCharacterArtKeepExisting() As Boolean = False
    Public Property TVShowCharacterArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowCharacterArtPrefSizeOnly() As Boolean = False
    Public Property TVShowClearArtAD() As Boolean = False
    Public Property TVShowClearArtExpert() As String = String.Empty
    Public Property TVShowClearArtExtended() As Boolean = False
    Public Property TVShowClearArtKeepExisting() As Boolean = False
    Public Property TVShowClearArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowClearArtPrefSizeOnly() As Boolean = False
    Public Property TVShowClearLogoAD() As Boolean = False
    Public Property TVShowClearLogoExpert() As String = String.Empty
    Public Property TVShowClearLogoExtended() As Boolean = False
    Public Property TVShowClearLogoKeepExisting() As Boolean = False
    Public Property TVShowClearLogoPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowClearLogoPrefSizeOnly() As Boolean = False
    Public Property TVShowExtrafanartsExpert() As Boolean = False
    Public Property TVShowExtrafanartsFrodo() As Boolean = False
    Public Property TVShowExtrafanartsHeight() As Integer = 0
    Public Property TVShowExtrafanartsKeepExisting() As Boolean = False
    Public Property TVShowExtrafanartsLimit() As Integer
        Get
            Return _tvshowextrafanartslimit
        End Get
        Set(value As Integer)
            If value > ExtraImagesLimit OrElse value = 0 Then
                _tvshowextrafanartslimit = 20
            Else
                _tvshowextrafanartslimit = value
            End If
        End Set
    End Property
    Public Property TVShowExtrafanartsPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowExtrafanartsPrefSizeOnly() As Boolean = False
    Public Property TVShowExtrafanartsPreselect() As Boolean = True
    Public Property TVShowExtrafanartsResize() As Boolean = False
    Public Property TVShowExtrafanartsWidth() As Integer = 0
    Public Property TVShowFanartBoxee() As Boolean = False
    Public Property TVShowFanartExpert() As String = String.Empty
    Public Property TVShowFanartFrodo() As Boolean = False
    Public Property TVShowFanartHeight() As Integer = 0
    Public Property TVShowFanartKeepExisting() As Boolean = False
    Public Property TVShowFanartPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowFanartPrefSizeOnly() As Boolean = False
    Public Property TVShowFanartResize() As Boolean = False
    Public Property TVShowFanartWidth() As Integer = 0
    Public Property TVShowFanartYAMJ() As Boolean = False
    Public Property TVShowFilterCustom() As List(Of String) = New List(Of String)
    Public Property TVShowFilterCustomIsEmpty() As Boolean = False
    Public Property TVShowKeyArtExtended() As Boolean = False
    Public Property TVShowKeyArtHeight() As Integer = 0
    Public Property TVShowKeyArtKeepExisting() As Boolean = False
    Public Property TVShowKeyArtPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowKeyArtPrefSizeOnly() As Boolean = False
    Public Property TVShowKeyArtResize() As Boolean = False
    Public Property TVShowKeyArtWidth() As Integer = 0
    Public Property TVShowLandscapeAD() As Boolean = False
    Public Property TVShowLandscapeExpert() As String = String.Empty
    Public Property TVShowLandscapeExtended() As Boolean = False
    Public Property TVShowLandscapeKeepExisting() As Boolean = False
    Public Property TVShowLandscapePrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowLandscapePrefSizeOnly() As Boolean = False
    Public Property TVShowMatching() As List(Of regexp) = New List(Of regexp)
    Public Property TVShowMissingBanner() As Boolean = False
    Public Property TVShowMissingCharacterArt() As Boolean = False
    Public Property TVShowMissingClearArt() As Boolean = False
    Public Property TVShowMissingClearLogo() As Boolean = False
    Public Property TVShowMissingExtrafanarts() As Boolean = False
    Public Property TVShowMissingFanart() As Boolean = False
    Public Property TVShowMissingKeyArt() As Boolean = False
    Public Property TVShowMissingLandscape() As Boolean = False
    Public Property TVShowMissingNFO() As Boolean = False
    Public Property TVShowMissingPoster() As Boolean = False
    Public Property TVShowMissingTheme() As Boolean = False
    Public Property TVShowNFOBoxee() As Boolean = False
    Public Property TVShowNFOExpert() As String = String.Empty
    Public Property TVShowNFOFrodo() As Boolean = False
    Public Property TVShowNFOYAMJ() As Boolean = False
    Public Property TVShowPosterBoxee() As Boolean = False
    Public Property TVShowPosterExpert() As String = String.Empty
    Public Property TVShowPosterFrodo() As Boolean = False
    Public Property TVShowPosterHeight() As Integer = 0
    Public Property TVShowPosterKeepExisting() As Boolean = False
    Public Property TVShowPosterPrefSize() As Enums.ImageSize = Enums.ImageSize.Any
    Public Property TVShowPosterPrefSizeOnly() As Boolean = False
    Public Property TVShowPosterResize() As Boolean = False
    Public Property TVShowPosterWidth() As Integer = 0
    Public Property TVShowPosterYAMJ() As Boolean = False
    Public Property TVShowProperCase() As Boolean = True
    Public Property TVShowThemeKeepExisting() As Boolean = False
    Public Property TVShowThemeTvTunesCustom() As Boolean = False
    Public Property TVShowThemeTvTunesCustomPath() As String = String.Empty
    Public Property TVShowThemeTvTunesEnable() As Boolean = False
    Public Property TVShowThemeTvTunesShowPath() As Boolean = False
    Public Property TVShowThemeTvTunesSub() As Boolean = False
    Public Property TVShowThemeTvTunesSubDir() As String = String.Empty
    Public Property TVSkipLessThan() As Integer = 0
    Public Property TVSortTokens() As List(Of String) = New List(Of String)
    Public Property TVSortTokensIsEmpty() As Boolean = False
    Public Property TVUseAD() As Boolean = False
    Public Property TVUseBoxee() As Boolean = False
    Public Property TVUseEden() As Boolean = False
    Public Property TVUseExpert() As Boolean = False
    Public Property TVUseExtended() As Boolean = False
    Public Property TVUseFrodo() As Boolean = False
    Public Property TVUseYAMJ() As Boolean = False
    Public Property Username() As String = String.Empty
    Public Property Version() As String = String.Empty

#End Region 'Properties

#Region "Simplified Properties"

    Public ReadOnly Property DefaultOptions_Movie As Structures.ScrapeOptions
        Get
            Dim scrapeOptions As New Structures.ScrapeOptions
            With scrapeOptions
                .bMainActors = MovieScraperCast
                .bMainCertifications = MovieScraperCert
                .bMainCollectionID = MovieScraperCollectionID
                .bMainCountries = MovieScraperCountry
                .bMainDirectors = MovieScraperDirector
                .bMainGenres = MovieScraperGenre
                .bMainMPAA = MovieScraperMPAA
                .bMainOriginalTitle = MovieScraperOriginalTitle
                .bMainOutline = MovieScraperOutline
                .bMainPlot = MovieScraperPlot
                .bMainRating = MovieScraperRating
                .bMainRelease = MovieScraperRelease
                .bMainRuntime = MovieScraperRuntime
                .bMainStudios = MovieScraperStudio
                .bMainTagline = MovieScraperTagline
                .bMainTitle = MovieScraperTitle
                .bMainTop250 = MovieScraperTop250
                .bMainTrailer = MovieScraperTrailer
                .bMainUserRating = MovieScraperUserRating
                .bMainWriters = MovieScraperCredits
                .bMainYear = MovieScraperYear
            End With
            Return scrapeOptions
        End Get
    End Property

    Public ReadOnly Property DefaultOptions_MovieSet As Structures.ScrapeOptions
        Get
            Dim scrapeOptions As New Structures.ScrapeOptions
            With scrapeOptions
                .bMainPlot = MovieSetScraperPlot
                .bMainTitle = MovieSetScraperTitle
            End With
            Return scrapeOptions
        End Get
    End Property

    Public ReadOnly Property DefaultOptions_TV As Structures.ScrapeOptions
        Get
            Dim scrapeOptions As New Structures.ScrapeOptions
            With scrapeOptions
                .bEpisodeActors = TVScraperEpisodeActors
                .bEpisodeAired = TVScraperEpisodeAired
                .bEpisodeCredits = TVScraperEpisodeCredits
                .bEpisodeDirectors = TVScraperEpisodeDirector
                .bEpisodeGuestStars = TVScraperEpisodeGuestStars
                .bEpisodePlot = TVScraperEpisodePlot
                .bEpisodeRating = TVScraperEpisodeRating
                .bEpisodeRuntime = TVScraperEpisodeRuntime
                .bEpisodeTitle = TVScraperEpisodeTitle
                .bEpisodeUserRating = TVScraperEpisodeUserRating
                .bMainActors = TVScraperShowActors
                .bMainCertifications = TVScraperShowCert
                .bMainCountries = TVScraperShowCountry
                .bMainCreators = TVScraperShowCreators
                .bMainEpisodeGuide = TVScraperShowEpiGuideURL
                .bMainGenres = TVScraperShowGenre
                .bMainMPAA = TVScraperShowMPAA
                .bMainOriginalTitle = TVScraperShowOriginalTitle
                .bMainPlot = TVScraperShowPlot
                .bMainPremiered = TVScraperShowPremiered
                .bMainRating = TVScraperShowRating
                .bMainRuntime = TVScraperShowRuntime
                .bMainStatus = TVScraperShowStatus
                .bMainStudios = TVScraperShowStudio
                .bMainTitle = TVScraperShowTitle
                .bMainUserRating = TVScraperShowUserRating
                .bSeasonAired = TVScraperSeasonAired
                .bSeasonPlot = TVScraperSeasonPlot
                .bSeasonTitle = TVScraperSeasonTitle
            End With
            Return scrapeOptions
        End Get
    End Property

    Public ReadOnly Property MovieActorThumbsAnyEnabled As Boolean
        Get
            Return MovieActorThumbsEden OrElse MovieActorThumbsFrodo OrElse (MovieUseExpert AndAlso ((MovieActorThumbsExpertBDMV AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertBDMV)) OrElse (MovieActorThumbsExpertMulti AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertMulti)) OrElse (MovieActorThumbsExpertSingle AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertSingle)) OrElse (MovieActorThumbsExpertVTS AndAlso Not String.IsNullOrEmpty(MovieActorThumbsExtExpertVTS))))
        End Get
    End Property

    Public ReadOnly Property MovieBannerAnyEnabled As Boolean
        Get
            Return MovieBannerAD OrElse MovieBannerExtended OrElse MovieBannerNMJ OrElse MovieBannerYAMJ OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieBannerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieBannerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieBannerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieBannerExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieClearArtAnyEnabled As Boolean
        Get
            Return MovieClearArtAD OrElse MovieClearArtExtended OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearArtExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieClearLogoAnyEnabled As Boolean
        Get
            Return MovieClearLogoAD OrElse MovieClearLogoExtended OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieClearLogoExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertMulti) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertSingle) OrElse Not String.IsNullOrEmpty(MovieClearLogoExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieDiscArtAnyEnabled As Boolean
        Get
            Return MovieDiscArtAD OrElse MovieDiscArtExtended OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieDiscArtExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertMulti) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertSingle) OrElse Not String.IsNullOrEmpty(MovieDiscArtExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieExtrafanartsAnyEnabled As Boolean
        Get
            Return MovieExtrafanartsEden OrElse MovieExtrafanartsFrodo OrElse
                (MovieUseExpert AndAlso (MovieExtrafanartsExpertBDMV OrElse MovieExtrafanartsExpertSingle OrElse MovieExtrafanartsExpertVTS))
        End Get
    End Property

    Public ReadOnly Property MovieExtrathumbsAnyEnabled As Boolean
        Get
            Return MovieExtrathumbsEden OrElse MovieExtrathumbsFrodo OrElse
                (MovieUseExpert AndAlso (MovieExtrathumbsExpertBDMV OrElse MovieExtrathumbsExpertSingle OrElse MovieExtrathumbsExpertVTS))
        End Get
    End Property

    Public ReadOnly Property MovieFanartAnyEnabled As Boolean
        Get
            Return MovieFanartBoxee OrElse MovieFanartEden OrElse MovieFanartFrodo OrElse MovieFanartNMJ OrElse MovieFanartYAMJ OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieFanartExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieFanartExpertMulti) OrElse Not String.IsNullOrEmpty(MovieFanartExpertSingle) OrElse Not String.IsNullOrEmpty(MovieFanartExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieKeyArtAnyEnabled As Boolean
        Get
            Return MovieKeyArtExtended
        End Get
    End Property

    Public ReadOnly Property MovieLandscapeAnyEnabled As Boolean
        Get
            Return MovieLandscapeAD OrElse MovieLandscapeExtended OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieLandscapeExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertMulti) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertSingle) OrElse Not String.IsNullOrEmpty(MovieLandscapeExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieMissingItemsAnyEnabled As Boolean
        Get
            Return MovieMissingBanner OrElse
                MovieMissingClearArt OrElse
                MovieMissingClearLogo OrElse
                MovieMissingDiscArt OrElse
                MovieMissingExtrafanarts OrElse
                MovieMissingExtrathumbs OrElse
                MovieMissingFanart OrElse
                MovieMissingKeyArt OrElse
                MovieMissingLandscape OrElse
                MovieMissingNFO OrElse
                MovieMissingPoster OrElse
                MovieMissingSubtitles OrElse
                MovieMissingTheme OrElse
                MovieMissingTrailer
        End Get
    End Property

    Public ReadOnly Property MovieNFOAnyEnabled As Boolean
        Get
            Return MovieNFOBoxee OrElse MovieNFOEden OrElse MovieNFOFrodo OrElse MovieNFONMJ OrElse MovieNFOYAMJ OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieNFOExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieNFOExpertMulti) OrElse Not String.IsNullOrEmpty(MovieNFOExpertSingle) OrElse Not String.IsNullOrEmpty(MovieNFOExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MoviePosterAnyEnabled As Boolean
        Get
            Return MoviePosterBoxee OrElse MoviePosterEden OrElse MoviePosterFrodo OrElse MoviePosterNMJ OrElse MoviePosterYAMJ OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MoviePosterExpertBDMV) OrElse Not String.IsNullOrEmpty(MoviePosterExpertMulti) OrElse Not String.IsNullOrEmpty(MoviePosterExpertSingle) OrElse Not String.IsNullOrEmpty(MoviePosterExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MovieThemeAnyEnabled As Boolean
        Get
            Return MovieThemeTvTunesEnable AndAlso (MovieThemeTvTunesMoviePath OrElse (MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(MovieThemeTvTunesCustomPath) OrElse (MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(MovieThemeTvTunesSubDir))))
        End Get
    End Property

    Public ReadOnly Property MovieTrailerAnyEnabled As Boolean
        Get
            Return MovieTrailerEden OrElse MovieTrailerFrodo OrElse MovieTrailerNMJ OrElse MovieTrailerYAMJ OrElse
                (MovieUseExpert AndAlso (Not String.IsNullOrEmpty(MovieTrailerExpertBDMV) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertMulti) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertSingle) OrElse Not String.IsNullOrEmpty(MovieTrailerExpertVTS)))
        End Get
    End Property

    Public ReadOnly Property MoviesetArtworkPaths() As List(Of String)
        Get
            Dim lstPaths As New List(Of String)
            If Not String.IsNullOrEmpty(MovieSetPathExpertSingle) Then lstPaths.Add(MovieSetPathExpertSingle)
            If Not String.IsNullOrEmpty(MovieSetPathExtended) Then lstPaths.Add(MovieSetPathExtended)
            If Not String.IsNullOrEmpty(MovieSetPathMSAA) Then lstPaths.Add(MovieSetPathMSAA)
            lstPaths = lstPaths.Distinct().ToList() 'remove double entries
            Return lstPaths
        End Get
    End Property

    Public ReadOnly Property MovieSetBannerAnyEnabled As Boolean
        Get
            Return (MovieSetBannerExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetBannerMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetPosterExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetClearArtAnyEnabled As Boolean
        Get
            Return (MovieSetClearArtExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetClearArtMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetClearArtExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetClearLogoAnyEnabled As Boolean
        Get
            Return (MovieSetClearLogoExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetClearLogoMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetClearLogoExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetDiscArtAnyEnabled As Boolean
        Get
            Return (MovieSetDiscArtExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetDiscArtExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetFanartAnyEnabled As Boolean
        Get
            Return (MovieSetFanartExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetFanartMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetFanartExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetKeyArtAnyEnabled As Boolean
        Get
            Return MovieSetKeyArtExtended
        End Get
    End Property

    Public ReadOnly Property MovieSetLandscapeAnyEnabled As Boolean
        Get
            Return (MovieSetLandscapeExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetLandscapeMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetLandscapeExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetMissingItemsAnyEnabled As Boolean
        Get
            Return MovieSetMissingBanner OrElse
                MovieSetMissingClearArt OrElse
                MovieSetMissingClearLogo OrElse
                MovieSetMissingDiscArt OrElse
                MovieSetMissingFanart OrElse
                MovieSetMissingKeyArt OrElse
                MovieSetMissingLandscape OrElse
                MovieSetMissingNFO OrElse
                MovieSetMissingPoster
        End Get
    End Property

    Public ReadOnly Property MovieSetNFOAnyEnabled As Boolean
        Get
            Return (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetNFOExpertSingle))
        End Get
    End Property

    Public ReadOnly Property MovieSetPosterAnyEnabled As Boolean
        Get
            Return (MovieSetPosterExtended AndAlso Not String.IsNullOrEmpty(MovieSetPathExtended)) OrElse
                (MovieSetPosterMSAA AndAlso Not String.IsNullOrEmpty(MovieSetPathMSAA)) OrElse
                (MovieSetUseExpert AndAlso Not String.IsNullOrEmpty(MovieSetPathExpertSingle) AndAlso Not String.IsNullOrEmpty(MovieSetPosterExpertSingle))
        End Get
    End Property

    Public ReadOnly Property TVAllSeasonsAnyEnabled As Boolean
        Get
            Return TVAllSeasonsBannerAnyEnabled() OrElse TVAllSeasonsFanartAnyEnabled() OrElse TVAllSeasonsLandscapeAnyEnabled() OrElse TVAllSeasonsPosterAnyEnabled()
        End Get
    End Property

    Public ReadOnly Property TVAllSeasonsBannerAnyEnabled As Boolean
        Get
            Return TVSeasonBannerFrodo OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsBannerExpert))
        End Get
    End Property

    Public ReadOnly Property TVAllSeasonsFanartAnyEnabled As Boolean
        Get
            Return TVSeasonFanartFrodo OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsFanartExpert))
        End Get
    End Property

    Public ReadOnly Property TVAllSeasonsLandscapeAnyEnabled As Boolean
        Get
            Return TVSeasonLandscapeAD OrElse TVSeasonLandscapeExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsLandscapeExpert))
        End Get
    End Property

    Public ReadOnly Property TVAllSeasonsPosterAnyEnabled As Boolean
        Get
            Return TVSeasonPosterFrodo OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVAllSeasonsPosterExpert))
        End Get
    End Property

    Public ReadOnly Property TVEpisodeActorThumbsAnyEnabled As Boolean
        Get
            Return TVEpisodeActorThumbsFrodo OrElse
                (TVUseExpert AndAlso TVEpisodeActorThumbsExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeActorThumbsExtExpert))
        End Get
    End Property

    Public ReadOnly Property TVEpisodeFanartAnyEnabled As Boolean
        Get
            Return (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeFanartExpert))
        End Get
    End Property

    Public ReadOnly Property TVEpisodeNFOAnyEnabled As Boolean
        Get
            Return TVEpisodeNFOBoxee OrElse TVEpisodeNFOFrodo OrElse TVEpisodeNFOYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodeNFOExpert))
        End Get
    End Property

    Public ReadOnly Property TVEpisodePosterAnyEnabled As Boolean
        Get
            Return TVEpisodePosterBoxee OrElse TVEpisodePosterFrodo OrElse TVEpisodePosterYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVEpisodePosterExpert))
        End Get
    End Property

    Public ReadOnly Property TVSeasonBannerAnyEnabled As Boolean
        Get
            Return TVSeasonBannerFrodo OrElse TVSeasonBannerYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonBannerExpert))
        End Get
    End Property

    Public ReadOnly Property TVSeasonFanartAnyEnabled As Boolean
        Get
            Return TVSeasonFanartFrodo OrElse TVSeasonFanartYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonFanartExpert))
        End Get
    End Property

    Public ReadOnly Property TVSeasonLandscapeAnyEnabled As Boolean
        Get
            Return TVSeasonLandscapeAD OrElse TVSeasonLandscapeExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonLandscapeExpert))
        End Get
    End Property

    Public ReadOnly Property TVSeasonPosterAnyEnabled As Boolean
        Get
            Return TVSeasonPosterBoxee OrElse TVSeasonPosterFrodo OrElse TVSeasonPosterYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVSeasonPosterExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowActorThumbsAnyEnabled As Boolean
        Get
            Return TVShowActorThumbsFrodo OrElse
                (TVUseExpert AndAlso TVShowActorThumbsExpert AndAlso Not String.IsNullOrEmpty(TVShowActorThumbsExtExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowBannerAnyEnabled As Boolean
        Get
            Return TVShowBannerBoxee OrElse TVShowBannerFrodo OrElse TVShowBannerYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowBannerExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowCharacterArtAnyEnabled As Boolean
        Get
            Return TVShowCharacterArtAD OrElse TVShowCharacterArtExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowCharacterArtExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowClearArtAnyEnabled As Boolean
        Get
            Return TVShowClearArtAD OrElse TVShowClearArtExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowClearArtExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowClearLogoAnyEnabled As Boolean
        Get
            Return TVShowClearLogoAD OrElse TVShowClearLogoExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowClearLogoExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowExtrafanartsAnyEnabled As Boolean
        Get
            Return TVShowExtrafanartsFrodo OrElse
                (TVUseExpert AndAlso TVShowExtrafanartsExpert)
        End Get
    End Property

    Public ReadOnly Property TVShowFanartAnyEnabled As Boolean
        Get
            Return TVShowFanartBoxee OrElse TVShowFanartFrodo OrElse TVShowFanartYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowFanartExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowKeyArtAnyEnabled As Boolean
        Get
            Return TVShowKeyArtExtended
        End Get
    End Property

    Public ReadOnly Property TVShowLandscapeAnyEnabled As Boolean
        Get
            Return TVShowLandscapeAD OrElse TVShowLandscapeExtended OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowLandscapeExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowMissingItemsAnyEnabled As Boolean
        Get
            Return TVShowMissingBanner OrElse
                TVShowMissingCharacterArt OrElse
                TVShowMissingClearArt OrElse
                TVShowMissingClearLogo OrElse
                TVShowMissingExtrafanarts OrElse
                TVShowMissingFanart OrElse
                TVShowMissingKeyArt OrElse
                TVShowMissingLandscape OrElse
                TVShowMissingNFO OrElse
                TVShowMissingPoster OrElse
                TVShowMissingTheme
        End Get
    End Property

    Public ReadOnly Property TVShowNFOAnyEnabled As Boolean
        Get
            Return TVShowNFOBoxee OrElse TVShowNFOFrodo OrElse TVShowNFOYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowNFOExpert))
        End Get
    End Property

    Public ReadOnly Property TVShowPosterAnyEnabled As Boolean
        Get
            Return TVShowPosterBoxee OrElse TVShowPosterFrodo OrElse TVShowPosterYAMJ OrElse
                (TVUseExpert AndAlso Not String.IsNullOrEmpty(TVShowPosterExpert))
        End Get
    End Property

    Public ReadOnly Property TvShowThemeAnyEnabled As Boolean
        Get
            Return TVShowThemeTvTunesEnable AndAlso (TVShowThemeTvTunesShowPath OrElse (TVShowThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(TVShowThemeTvTunesCustomPath) OrElse (TVShowThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(TVShowThemeTvTunesSubDir))))
        End Get
    End Property

#End Region 'Simplified Properties

#Region "Methods"

    Public Sub Load()
        Dim configpath As String = Path.Combine(Master.SettingsPath, "Settings.xml")

        Try
            If File.Exists(configpath) Then
                Dim xmlSer As New XmlSerializer(GetType(Settings))
                Using xmlSR As StreamReader = New StreamReader(configpath)
                    Master.eSettings = DirectCast(xmlSer.Deserialize(xmlSR), Settings)
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            logger.Info("An attempt is made to repair the Settings.xml")
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
                    'replace no longer used "Enums.ImageSize.Thumb" with "Enums.ImageSize.Any"
                    sSettings = System.Text.RegularExpressions.Regex.Replace(sSettings, "PrefSize>Thumb<", "PrefSize>Any<")
                    Dim xmlSer As New XmlSerializer(GetType(Settings))
                    Using txtReader As TextReader = New StringReader(sSettings)
                        Master.eSettings = DirectCast(xmlSer.Deserialize(txtReader), Settings)
                    End Using
                    logger.Info("Settings.xml successfully repaired")
                End Using
            Catch ex2 As Exception
                logger.Error(ex2, New StackFrame().GetMethod().Name)
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
        If Not (Master.eSettings.TVUseBoxee OrElse Master.eSettings.TVUseEden OrElse Master.eSettings.TVUseExpert OrElse Master.eSettings.TVUseFrodo OrElse Master.eSettings.TVUseYAMJ) Then
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetDefaultsForList_AudioCodecMapping() As List(Of CodecMapping)
        Dim nList As New List(Of CodecMapping)
        nList.Add(New CodecMapping With {.Codec = "aac lc", .Mapping = "aac", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "ac-3 mlp fba 16-ch", .Mapping = "truehd", .AdditionalFeatures = "atmos"})
        nList.Add(New CodecMapping With {.Codec = "ac-3", .Mapping = "ac3", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "dts xbr", .Mapping = "dtshd_hra", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "dts xll x", .Mapping = "dtshd_ma", .AdditionalFeatures = "x"})
        nList.Add(New CodecMapping With {.Codec = "dts xll", .Mapping = "dtshd_ma", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "dts", .Mapping = "dca", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "e-ac-3", .Mapping = "eac3", .AdditionalFeatures = ""})
        nList.Add(New CodecMapping With {.Codec = "mlp fba", .Mapping = "truehd", .AdditionalFeatures = ""})
        Return nList
    End Function

    Public Function GetDefaultsForList_VideoCodecMapping() As List(Of CodecMapping)
        Dim nList As New List(Of CodecMapping)
        nList.Add(New CodecMapping With {.Codec = "avc", .Mapping = "h264"})
        nList.Add(New CodecMapping With {.Codec = "hvc1", .Mapping = "hevc"})
        nList.Add(New CodecMapping With {.Codec = "v_mpeg2", .Mapping = "h264"})
        nList.Add(New CodecMapping With {.Codec = "v_mpegh/iso/hevc", .Mapping = "hevc"})
        nList.Add(New CodecMapping With {.Codec = "v_ms/vfw/fourcc / dx50", .Mapping = "dx50"})
        nList.Add(New CodecMapping With {.Codec = "v_ms/vfw/fourcc / xvid", .Mapping = "xvid"})
        nList.Add(New CodecMapping With {.Codec = "v_vp7", .Mapping = "vp7"})
        nList.Add(New CodecMapping With {.Codec = "v_vp8", .Mapping = "vp8"})
        nList.Add(New CodecMapping With {.Codec = "v_vp9", .Mapping = "vp9"})
        nList.Add(New CodecMapping With {.Codec = "x264", .Mapping = "h264"})
        Return nList
    End Function

    Public Function GetDefaultsForList_VideoSourceMappingByRegex() As List(Of VideoSourceByRegex)
        Dim nList As New List(Of VideoSourceByRegex)
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]3dbd[\W_]", .Videosource = "3dbd"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]azhd|amazon[\W_]", .Videosource = "amazon"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_](b[dr][-\s]?rip|blu[-\s]?ray)[\W_]", .Videosource = "bluray"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]bd25[\W_]", .Videosource = "bluray"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]bd50[\W_]", .Videosource = "bluray"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dvd5[\W_]", .Videosource = "dvd"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dvd9[\W_]", .Videosource = "dvd"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_](sd[-\s]?)?dvd([-\s]?rip)?[\W_]", .Videosource = "dvd"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hd[-\s]?dvd[\W_]", .Videosource = "hddvd"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hd[-\s]?tv[\W_]", .Videosource = "hdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]ithd|itunes[\W_]", .Videosource = "itunes"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]nfu?hd|netflix[\W_]", .Videosource = "netflix"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]pdtv[\W_]", .Videosource = "sdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]dsr[\W_]", .Videosource = "sdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]ntsc[\W_]", .Videosource = "sdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]sd[-\s]?tv[\W_]", .Videosource = "sdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]tvrip[\W_]", .Videosource = "sdtv"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]vhs[\W_]", .Videosource = "vhs"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]hddl[\W_]", .Videosource = "webdl"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]web-?dl[\W_]", .Videosource = "webdl"})
        nList.Add(New VideoSourceByRegex With {.Regexp = "(?i)[\W_]web-?rip[\W_]", .Videosource = "webdl"})
        Return nList
    End Function

    Public Sub SetDefaultsForLists(ByVal Type As Enums.DefaultType, ByVal Force As Boolean)
        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.AudioCodecMapping) AndAlso (Force OrElse Master.eSettings.GeneralAudioCodecMapping.Count = 0) Then
            Master.eSettings.GeneralAudioCodecMapping = GetDefaultsForList_AudioCodecMapping()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MainTabSorting) AndAlso (Force OrElse Master.eSettings.GeneralMainTabSorting.Count = 0) Then
            Master.eSettings.GeneralMainTabSorting.Clear()
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.Movie, .DefaultList = "movielist", .Order = 0, .Title = Master.eLang.GetString(36, "Movies")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.Movieset, .DefaultList = "moviesetlist", .Order = 1, .Title = Master.eLang.GetString(366, "Sets")})
            Master.eSettings.GeneralMainTabSorting.Add(New MainTabSorting With {.ContentType = Enums.ContentType.TV, .DefaultList = "tvshowlist", .Order = 2, .Title = Master.eLang.GetString(653, "TV Shows")})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MovieFilters) AndAlso (Force OrElse (Master.eSettings.MovieFilterCustom.Count <= 0 AndAlso Not Master.eSettings.MovieFilterCustomIsEmpty)) Then
            Master.eSettings.MovieFilterCustom.Clear()
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]\(?\d{4}\)?.*")    'year in brakets
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]tt\d*")            'IMDB ID
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]3d.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]1080.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]1440.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]2160.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]4k.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]german.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.MovieFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.MovieFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.MovieFilterCustom.Add("\.[->] ")                   'convert dots to space
            Master.eSettings.MovieFilterCustom.Add("_[->] ")                    'convert underscore to space
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVShowFilters) AndAlso (Force OrElse (Master.eSettings.TVShowFilterCustom.Count <= 0 AndAlso Not Master.eSettings.TVShowFilterCustomIsEmpty)) Then
            Master.eSettings.TVShowFilterCustom.Clear()
            Master.eSettings.TVShowFilterCustom.Add("[\W_]\(?\d{4}\)?.*")
            'would there ever be season or episode info in the show folder name??
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?s[0-9]+[\W_]*e[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?[0-9]+x[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?s(eason)?[\W_]*[0-9]+(\])*")
            'Master.eSettings.TVShowFilterCustom.Add("(?i)([\W_]+\s?)?e(pisode)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]1080.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]1440.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]2160.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]4k.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.TVShowFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.TVShowFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.TVShowFilterCustom.Add("\.[->] ")                  'convert dots to space
            Master.eSettings.TVShowFilterCustom.Add("_[->] ")                   'convert underscore to space
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVEpisodeFilters) AndAlso (Force OrElse (Master.eSettings.TVEpisodeFilterCustom.Count <= 0 AndAlso Not Master.eSettings.TVEpisodeFilterCustomIsEmpty)) Then
            Master.eSettings.TVEpisodeFilterCustom.Clear()
            Master.eSettings.TVEpisodeFilterCustom.Add("[\W_]\(?\d{4}\)?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?s[0-9]+[\W_]*([-e][0-9]+)+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?[0-9]+([-x][0-9]+)+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?s(eason)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)([\W_]+\s?)?e(pisode)?[\W_]*[0-9]+(\])*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]blu[\W_]?ray.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]bd[\W_]?rip.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dvd.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]720.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]1080.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]1440.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]2160.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]4k.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]ac3.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dts.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]divx.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]xvid.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dc[\W_]?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]dir(ector'?s?)?\s?cut.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]extended.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]hd(tv)?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]unrated.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]uncut.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]([a-z]{3}|multi)[sd]ub.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]\[offline\].*")
            Master.eSettings.TVEpisodeFilterCustom.Add("(?i)[\W_]ntsc.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("[\W_]PAL[\W_]?.*")
            Master.eSettings.TVEpisodeFilterCustom.Add("\.[->] ")               'convert dots to space
            Master.eSettings.TVEpisodeFilterCustom.Add("_[->] ")                'convert underscore to space
            Master.eSettings.TVEpisodeFilterCustom.Add(" - [->] ")              'convert space-minus-space to space
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MovieSortTokens) AndAlso (Force OrElse (Master.eSettings.MovieSortTokens.Count <= 0 AndAlso Not Master.eSettings.MovieSortTokensIsEmpty)) Then
            Master.eSettings.MovieSortTokens.Clear()
            Master.eSettings.MovieSortTokens.Add("a\s")
            Master.eSettings.MovieSortTokens.Add("an\s")
            Master.eSettings.MovieSortTokens.Add("das\s")
            Master.eSettings.MovieSortTokens.Add("der\s")
            Master.eSettings.MovieSortTokens.Add("die\s")
            Master.eSettings.MovieSortTokens.Add("the\s")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MoviesetSortTokens) AndAlso (Force OrElse (Master.eSettings.MovieSetSortTokens.Count <= 0 AndAlso Not Master.eSettings.MovieSetSortTokensIsEmpty)) Then
            Master.eSettings.MovieSetSortTokens.Clear()
            Master.eSettings.MovieSetSortTokens.Add("a\s")
            Master.eSettings.MovieSetSortTokens.Add("an\s")
            Master.eSettings.MovieSetSortTokens.Add("das\s")
            Master.eSettings.MovieSetSortTokens.Add("der\s")
            Master.eSettings.MovieSetSortTokens.Add("die\s")
            Master.eSettings.MovieSetSortTokens.Add("the\s")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVShowSortTokens) AndAlso (Force OrElse (Master.eSettings.TVSortTokens.Count <= 0 AndAlso Not Master.eSettings.TVSortTokensIsEmpty)) Then
            Master.eSettings.TVSortTokens.Clear()
            Master.eSettings.TVSortTokens.Add("a\s")
            Master.eSettings.TVSortTokens.Add("an\s")
            Master.eSettings.TVSortTokens.Add("das\s")
            Master.eSettings.TVSortTokens.Add("der\s")
            Master.eSettings.TVSortTokens.Add("die\s")
            Master.eSettings.TVSortTokens.Add("the\s")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVSeasonTitleBlacklist) AndAlso (Force OrElse Master.eSettings.TVScraperSeasonTitleBlacklist.Count <= 0) Then
            Master.eSettings.TVScraperSeasonTitleBlacklist.Clear()
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("%{season_number}. sezóna")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("%{season_number}. évad")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("%{season_number}.ª Temporada")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("%{season_number}ª Temporada")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("%{season_number}ος κύκλος")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Kausi %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Musim ke %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Saison %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Season %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Seizoen %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Series %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Sezon %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Sezonas %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Sezonul %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Staffel %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Stagione %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Säsong %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Séria %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Tempada %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Temporada %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Сезон %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("Сезона %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("עונה %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("الموسم %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("فصل %{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("シーズン%{season_number}")
            Master.eSettings.TVScraperSeasonTitleBlacklist.Add("第 %{season_number} 季")
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVShowMatching) AndAlso (Force OrElse Master.eSettings.TVShowMatching.Count <= 0) Then
            Master.eSettings.TVShowMatching.Clear()
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 0, .byDate = False, .defaultSeason = -2, .Regexp = "s([0-9]+)[ ._-]*e([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 1, .byDate = False, .defaultSeason = 1, .Regexp = "[\\._ -]()e(?:p[ ._-]?)?([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 2, .byDate = True, .defaultSeason = -2, .Regexp = "([0-9]{4})[.-]([0-9]{2})[.-]([0-9]{2})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 3, .byDate = True, .defaultSeason = -2, .Regexp = "([0-9]{2})[.-]([0-9]{2})[.-]([0-9]{4})"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 4, .byDate = False, .defaultSeason = -2, .Regexp = "[\\\/._ \[\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)([^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 5, .byDate = False, .defaultSeason = -2, .Regexp = "[\\\/._ -]([0-9]+)([0-9][0-9](?:(?:[a-i]|\.[1-9])(?![0-9]))?)([._ -][^\\\/]*)$"})
            Master.eSettings.TVShowMatching.Add(New regexp With {.ID = 6, .byDate = False, .defaultSeason = 1, .Regexp = "[\\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\\/]*)$"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MovieListSorting) AndAlso (Force OrElse Master.eSettings.MovieGeneralMediaListSorting.Count <= 0) Then
            Master.eSettings.MovieGeneralMediaListSorting.Clear()
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 0, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 1, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 2, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Year), .LabelID = 278, .LabelText = "Year"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 3, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.MPAA), .LabelID = 401, .LabelText = "MPAA"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 4, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 5, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Top250), .LabelID = -1, .LabelText = "Top250"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 6, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 7, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 8, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 9, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 10, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath), .LabelID = 1098, .LabelText = "DiscArt"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 11, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath), .LabelID = 992, .LabelText = "Extrafanarts"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 12, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrathumbsPath), .LabelID = 153, .LabelText = "Extrathumbs"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 13, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 14, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 15, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 16, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 17, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles), .LabelID = 152, .LabelText = "Subtitles"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 18, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ThemePath), .LabelID = 1118, .LabelText = "Theme"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 19, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.TrailerPath), .LabelID = 151, .LabelText = "Trailer"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 20, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasMovieset), .LabelID = 1295, .LabelText = "Part of a MovieSet"})
            Master.eSettings.MovieGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 21, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed), .LabelID = 981, .LabelText = "Watched"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.MoviesetListSorting) AndAlso (Force OrElse Master.eSettings.MovieSetGeneralMediaListSorting.Count <= 0) Then
            Master.eSettings.MovieSetGeneralMediaListSorting.Clear()
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 0, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 1, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 2, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 3, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 4, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 5, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.DiscArtPath), .LabelID = 1098, .LabelText = "DiscArt"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 6, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 7, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 8, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.MovieSetGeneralMediaListSorting.Add(New ListSorting With {.DisplayIndex = 9, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVEpisodeListSorting) AndAlso (Force OrElse Master.eSettings.TVGeneralEpisodeListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralEpisodeListSorting.Clear()
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 0, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Title), .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 1, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 2, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 3, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 4, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 5, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 6, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasSubtitles), .LabelID = 152, .LabelText = "Subtitles"})
            Master.eSettings.TVGeneralEpisodeListSorting.Add(New ListSorting With {.DisplayIndex = 7, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LastPlayed), .LabelID = 981, .LabelText = "Watched"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVSeasonListSorting) AndAlso (Force OrElse Master.eSettings.TVGeneralSeasonListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralSeasonListSorting.Clear()
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 0, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Title), .LabelID = 650, .LabelText = "Season"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 1, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount), .LabelID = 682, .LabelText = "Episodes"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 2, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 3, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 4, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 5, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralSeasonListSorting.Add(New ListSorting With {.DisplayIndex = 6, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasWatched), .LabelID = 981, .LabelText = "Watched"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.TVShowListSorting) AndAlso (Force OrElse Master.eSettings.TVGeneralShowListSorting.Count <= 0) Then
            Master.eSettings.TVGeneralShowListSorting.Clear()
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 0, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ListTitle), .LabelID = 21, .LabelText = "Title"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 1, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.OriginalTitle), .LabelID = 302, .LabelText = "Original Title"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 2, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.Status), .LabelID = 215, .LabelText = "Status"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 3, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.MPAA), .LabelID = 401, .LabelText = "MPAA"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 4, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.UserRating), .LabelID = 1467, .LabelText = "User Rating"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 5, .Hide = True, .Column = Database.Helpers.GetColumnName(Database.ColumnName.EpisodeCount), .LabelID = 682, .LabelText = "Episodes"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 6, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.NfoPath), .LabelID = 150, .LabelText = "NFO"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 7, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.BannerPath), .LabelID = 838, .LabelText = "Banner"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 8, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.CharacterArtPath), .LabelID = 1140, .LabelText = "CharacterArt"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 9, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearArtPath), .LabelID = 1096, .LabelText = "ClearArt"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 10, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ClearLogoPath), .LabelID = 1097, .LabelText = "ClearLogo"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 11, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ExtrafanartsPath), .LabelID = 992, .LabelText = "Extrafanarts"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 12, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.FanartPath), .LabelID = 149, .LabelText = "Fanart"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 13, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.KeyArtPath), .LabelID = 296, .LabelText = "KeyArt"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 14, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.LandscapePath), .LabelID = 1035, .LabelText = "Landscape"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 15, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.PosterPath), .LabelID = 148, .LabelText = "Poster"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 16, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.ThemePath), .LabelID = 1118, .LabelText = "Theme"})
            Master.eSettings.TVGeneralShowListSorting.Add(New ListSorting With {.DisplayIndex = 17, .Hide = False, .Column = Database.Helpers.GetColumnName(Database.ColumnName.HasWatched), .LabelID = 981, .LabelText = "Watched"})
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidExts.Count <= 0) Then
            Master.eSettings.FileSystemValidExts.Clear()
            Master.eSettings.FileSystemValidExts.AddRange(".avi,.bdmv,.divx,.mkv,.iso,.mpg,.mp4,.mpeg,.wmv,.wma,.mov,.mts,.m2t,.img,.dat,.bin,.cue,.ifo,.vob,.dvb,.evo,.asf,.asx,.avs,.nsv,.ram,.ogg,.ogm,.ogv,.flv,.swf,.nut,.viv,.rar,.m2ts,.dvr-ms,.ts,.m4v,.rmvb,.webm,.disc,.3gpp".Split(","c))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidSubtitleExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidSubtitlesExts.Count <= 0) Then
            Master.eSettings.FileSystemValidSubtitlesExts.Clear()
            Master.eSettings.FileSystemValidSubtitlesExts.AddRange(".sst,.srt,.sub,.ssa,.aqt,.smi,.sami,.jss,.mpl,.rt,.idx,.ass".Split(","c))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.ValidThemeExts) AndAlso (Force OrElse Master.eSettings.FileSystemValidThemeExts.Count <= 0) Then
            Master.eSettings.FileSystemValidThemeExts.Clear()
            Master.eSettings.FileSystemValidThemeExts.AddRange(".flac,.m4a,.mp3,.wav,.wma".Split(","c))
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.VideoCodecMapping) AndAlso (Force OrElse Master.eSettings.GeneralVideoCodecMapping.Count = 0) Then
            Master.eSettings.GeneralVideoCodecMapping = GetDefaultsForList_VideoCodecMapping()
        End If

        If (Type = Enums.DefaultType.All OrElse Type = Enums.DefaultType.VideosourceMappingByRegex) AndAlso (Force OrElse Master.eSettings.GeneralVideoSourceByRegex.Count = 0) Then
            Master.eSettings.GeneralVideoSourceByRegex = GetDefaultsForList_VideoSourceMappingByRegex()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class Helpers

        Public Shared Function GetImageSettings(ByVal ContentType As Enums.ContentType, ImageType As Enums.ModifierType) As ImageSettingSpecifications
            Select Case ContentType
                Case Enums.ContentType.Movie
                    Select Case ImageType
                        Case Enums.ModifierType.MainBanner
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieBannerKeepExisting,
                                .MaxHeight = Master.eSettings.MovieBannerHeight,
                                .MaxWidth = Master.eSettings.MovieBannerWidth,
                                .PrefSize = Master.eSettings.MovieBannerPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieBannerPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieBannerResize
                            }
                        Case Enums.ModifierType.MainClearArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieClearArtKeepExisting,
                                .PrefSize = Master.eSettings.MovieClearArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieClearArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainClearLogo
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieClearLogoKeepExisting,
                                .PrefSize = Master.eSettings.MovieClearLogoPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieClearLogoPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainDiscArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieDiscArtKeepExisting,
                                .PrefSize = Master.eSettings.MovieDiscArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieDiscArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainExtrafanarts
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieExtrafanartsKeepExisting,
                                .MaxHeight = Master.eSettings.MovieExtrafanartsHeight,
                                .MaxWidth = Master.eSettings.MovieExtrafanartsWidth,
                                .Limit = Master.eSettings.MovieExtrafanartsLimit,
                                .PrefSize = Master.eSettings.MovieExtrafanartsPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieExtrafanartsPrefSizeOnly,
                                .Preselect = Master.eSettings.MovieExtrafanartsPreselect,
                                .Quality = CInt(AdvancedSettings.GetSetting("ExtrafanartsQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieExtrafanartsResize
                            }
                        Case Enums.ModifierType.MainExtrathumbs
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieBannerKeepExisting,
                                .MaxHeight = Master.eSettings.MovieExtrathumbsHeight,
                                .MaxWidth = Master.eSettings.MovieExtrathumbsWidth,
                                .Limit = Master.eSettings.MovieExtrathumbsLimit,
                                .PrefSize = Master.eSettings.MovieExtrathumbsPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieExtrathumbsPrefSizeOnly,
                                .Preselect = Master.eSettings.MovieExtrathumbsPreselect,
                                .Quality = CInt(AdvancedSettings.GetSetting("ExtrathumbsQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieExtrathumbsResize
                            }
                        Case Enums.ModifierType.MainFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieFanartKeepExisting,
                                .MaxHeight = Master.eSettings.MovieFanartHeight,
                                .MaxWidth = Master.eSettings.MovieFanartWidth,
                                .PrefSize = Master.eSettings.MovieFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieFanartResize
                            }
                        Case Enums.ModifierType.MainKeyArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieKeyArtKeepExisting,
                                .MaxHeight = Master.eSettings.MovieKeyArtHeight,
                                .MaxWidth = Master.eSettings.MovieKeyArtWidth,
                                .PrefSize = Master.eSettings.MovieKeyArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieKeyArtPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("KeyArtQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieKeyArtResize
                            }
                        Case Enums.ModifierType.MainLandscape
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieLandscapeKeepExisting,
                                .PrefSize = Master.eSettings.MovieLandscapePrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieLandscapePrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("LandscapeQuality", "100", , ContentType))
                            }
                        Case Enums.ModifierType.MainPoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MoviePosterKeepExisting,
                                .MaxHeight = Master.eSettings.MoviePosterHeight,
                                .MaxWidth = Master.eSettings.MoviePosterWidth,
                                .PrefSize = Master.eSettings.MoviePosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.MoviePosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MoviePosterResize
                            }
                    End Select
                Case Enums.ContentType.Movieset
                    Select Case ImageType
                        Case Enums.ModifierType.MainBanner
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetBannerKeepExisting,
                                .MaxHeight = Master.eSettings.MovieSetBannerHeight,
                                .MaxWidth = Master.eSettings.MovieSetBannerWidth,
                                .PrefSize = Master.eSettings.MovieSetBannerPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetBannerPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieSetBannerResize
                            }
                        Case Enums.ModifierType.MainClearArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetClearArtKeepExisting,
                                .PrefSize = Master.eSettings.MovieSetClearArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetClearArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainClearLogo
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetClearLogoKeepExisting,
                                .PrefSize = Master.eSettings.MovieSetClearLogoPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetClearLogoPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainDiscArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetDiscArtKeepExisting,
                                .PrefSize = Master.eSettings.MovieSetDiscArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetDiscArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetFanartKeepExisting,
                                .MaxHeight = Master.eSettings.MovieSetFanartHeight,
                                .MaxWidth = Master.eSettings.MovieSetFanartWidth,
                                .PrefSize = Master.eSettings.MovieSetFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieSetFanartResize
                            }
                        Case Enums.ModifierType.MainKeyArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetKeyArtKeepExisting,
                                .MaxHeight = Master.eSettings.MovieSetKeyArtHeight,
                                .MaxWidth = Master.eSettings.MovieSetKeyArtWidth,
                                .PrefSize = Master.eSettings.MovieSetKeyArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetKeyArtPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("KeyArtQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieSetKeyArtResize
                            }
                        Case Enums.ModifierType.MainLandscape
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetLandscapeKeepExisting,
                                .PrefSize = Master.eSettings.MovieSetLandscapePrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetLandscapePrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("LandscapeQuality", "100", , ContentType))
                            }
                        Case Enums.ModifierType.MainPoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.MovieSetPosterKeepExisting,
                                .MaxHeight = Master.eSettings.MovieSetPosterHeight,
                                .MaxWidth = Master.eSettings.MovieSetPosterWidth,
                                .PrefSize = Master.eSettings.MovieSetPosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.MovieSetPosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.MovieSetPosterResize
                            }
                    End Select
                Case Enums.ContentType.TVEpisode
                    Select Case ImageType
                        Case Enums.ModifierType.EpisodeFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVEpisodeFanartKeepExisting,
                                .MaxHeight = Master.eSettings.TVEpisodeFanartHeight,
                                .MaxWidth = Master.eSettings.TVEpisodeFanartWidth,
                                .PrefSize = Master.eSettings.TVEpisodeFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVEpisodeFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVEpisodeFanartResize
                            }
                        Case Enums.ModifierType.EpisodePoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVEpisodePosterKeepExisting,
                                .MaxHeight = Master.eSettings.TVEpisodePosterHeight,
                                .MaxWidth = Master.eSettings.TVEpisodePosterWidth,
                                .PrefSize = Master.eSettings.TVEpisodePosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVEpisodePosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVEpisodePosterResize
                            }
                    End Select
                Case Enums.ContentType.TVSeason
                    Select Case ImageType
                        Case Enums.ModifierType.AllSeasonsBanner
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVAllSeasonsBannerKeepExisting,
                                .MaxHeight = Master.eSettings.TVAllSeasonsBannerHeight,
                                .MaxWidth = Master.eSettings.TVAllSeasonsBannerWidth,
                                .PrefSize = Master.eSettings.TVAllSeasonsBannerPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVAllSeasonsBannerPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVAllSeasonsBannerResize
                            }
                        Case Enums.ModifierType.AllSeasonsFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVAllSeasonsFanartKeepExisting,
                                .MaxHeight = Master.eSettings.TVAllSeasonsFanartHeight,
                                .MaxWidth = Master.eSettings.TVAllSeasonsFanartWidth,
                                .PrefSize = Master.eSettings.TVAllSeasonsFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVAllSeasonsFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVAllSeasonsFanartResize
                            }
                        Case Enums.ModifierType.AllSeasonsLandscape
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVAllSeasonsLandscapeKeepExisting,
                                .PrefSize = Master.eSettings.TVAllSeasonsLandscapePrefSize,
                                .PrefSizeOnly = Master.eSettings.TVAllSeasonsLandscapePrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("LandscapeQuality", "100", , ContentType))
                            }
                        Case Enums.ModifierType.AllSeasonsPoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVAllSeasonsPosterKeepExisting,
                                .MaxHeight = Master.eSettings.TVAllSeasonsPosterHeight,
                                .MaxWidth = Master.eSettings.TVAllSeasonsPosterWidth,
                                .PrefSize = Master.eSettings.TVAllSeasonsPosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVAllSeasonsPosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVAllSeasonsPosterResize
                            }
                        Case Enums.ModifierType.SeasonBanner
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVSeasonBannerKeepExisting,
                                .MaxHeight = Master.eSettings.TVSeasonBannerHeight,
                                .MaxWidth = Master.eSettings.TVSeasonBannerWidth,
                                .PrefSize = Master.eSettings.TVSeasonBannerPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVSeasonBannerPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVSeasonBannerResize
                            }
                        Case Enums.ModifierType.SeasonFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVSeasonFanartKeepExisting,
                                .MaxHeight = Master.eSettings.TVSeasonFanartHeight,
                                .MaxWidth = Master.eSettings.TVSeasonFanartWidth,
                                .PrefSize = Master.eSettings.TVSeasonFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVSeasonFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVSeasonFanartResize
                            }
                        Case Enums.ModifierType.SeasonLandscape
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVSeasonLandscapeKeepExisting,
                                .PrefSize = Master.eSettings.TVSeasonLandscapePrefSize,
                                .PrefSizeOnly = Master.eSettings.TVSeasonLandscapePrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("LandscapeQuality", "100", , ContentType))
                            }
                        Case Enums.ModifierType.SeasonPoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVSeasonPosterKeepExisting,
                                .MaxHeight = Master.eSettings.TVSeasonPosterHeight,
                                .MaxWidth = Master.eSettings.TVSeasonPosterWidth,
                                .PrefSize = Master.eSettings.TVSeasonPosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVSeasonPosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVSeasonPosterResize
                            }
                    End Select
                Case Enums.ContentType.TVShow
                    Select Case ImageType
                        Case Enums.ModifierType.MainBanner
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowBannerKeepExisting,
                                .MaxHeight = Master.eSettings.TVShowBannerHeight,
                                .MaxWidth = Master.eSettings.TVShowBannerWidth,
                                .PrefSize = Master.eSettings.TVShowBannerPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowBannerPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("BannerQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVShowBannerResize
                            }
                        Case Enums.ModifierType.MainCharacterArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowCharacterArtKeepExisting,
                                .PrefSize = Master.eSettings.TVShowCharacterArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowCharacterArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainClearArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowClearArtKeepExisting,
                                .PrefSize = Master.eSettings.TVShowClearArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowClearArtPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainClearLogo
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowClearLogoKeepExisting,
                                .PrefSize = Master.eSettings.TVShowClearLogoPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowClearLogoPrefSizeOnly
                            }
                        Case Enums.ModifierType.MainExtrafanarts
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowExtrafanartsKeepExisting,
                                .MaxHeight = Master.eSettings.TVShowExtrafanartsHeight,
                                .MaxWidth = Master.eSettings.TVShowExtrafanartsWidth,
                                .Limit = Master.eSettings.TVShowExtrafanartsLimit,
                                .PrefSize = Master.eSettings.TVShowExtrafanartsPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowExtrafanartsPrefSizeOnly,
                                .Preselect = Master.eSettings.TVShowExtrafanartsPreselect,
                                .Quality = CInt(AdvancedSettings.GetSetting("ExtrafanartsQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVShowExtrafanartsResize
                            }
                        Case Enums.ModifierType.MainFanart
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowFanartKeepExisting,
                                .MaxHeight = Master.eSettings.TVShowFanartHeight,
                                .MaxWidth = Master.eSettings.TVShowFanartWidth,
                                .PrefSize = Master.eSettings.TVShowFanartPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowFanartPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("FanartQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVShowFanartResize
                            }
                        Case Enums.ModifierType.MainKeyArt
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowKeyArtKeepExisting,
                                .MaxHeight = Master.eSettings.TVShowKeyArtHeight,
                                .MaxWidth = Master.eSettings.TVShowKeyArtWidth,
                                .PrefSize = Master.eSettings.TVShowKeyArtPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowKeyArtPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("KeyArtQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVShowKeyArtResize
                            }
                        Case Enums.ModifierType.MainLandscape
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowLandscapeKeepExisting,
                                .PrefSize = Master.eSettings.TVShowLandscapePrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowLandscapePrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("LandscapeQuality", "100", , ContentType))
                            }
                        Case Enums.ModifierType.MainPoster
                            Return New ImageSettingSpecifications With {
                                .KeepExisting = Master.eSettings.TVShowPosterKeepExisting,
                                .MaxHeight = Master.eSettings.TVShowPosterHeight,
                                .MaxWidth = Master.eSettings.TVShowPosterWidth,
                                .PrefSize = Master.eSettings.TVShowPosterPrefSize,
                                .PrefSizeOnly = Master.eSettings.TVShowPosterPrefSizeOnly,
                                .Quality = CInt(AdvancedSettings.GetSetting("PosterQuality", "100", , ContentType)),
                                .Resize = Master.eSettings.TVShowPosterResize
                            }
                    End Select
            End Select
            Return Nothing
        End Function

#Region "Nested Types"

        Public Class ImageSettingSpecifications

#Region "Properties"

            Public Property KeepExisting As Boolean = False
            Public Property Limit As Integer = 0
            Public Property MaxHeight As Integer = 0
            Public Property MaxWidth As Integer = 0
            Public Property PrefSize As Enums.ImageSize = Enums.ImageSize.Any
            Public Property PrefSizeOnly As Boolean = False
            Public Property Preselect As Boolean = True
            Public Property Resize As Boolean = False
            Public Property Quality As Integer = 100

#End Region 'Properties

        End Class

#End Region 'Nested Types

    End Class

    Public Class CodecMapping

#Region "Properties"

        <XmlAttribute>
        Public Property Codec As String = String.Empty

        Public Property Mapping As String = String.Empty

        Public Property AdditionalFeatures As String = String.Empty

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
        Public Property Column() As String = String.Empty
        ''' <summary>
        ''' Position of the column in the media list
        ''' </summary>
        ''' <returns></returns>
        Public Property DisplayIndex() As Integer = -1
        ''' <summary>
        ''' Hide or show column in the media list
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Hide() As Boolean = False
        ''' <summary>
        ''' ID of string in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelID() As Integer = -1
        ''' <summary>
        ''' Default text for the LabelID in Master.eLangs.GetString
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property LabelText() As String = String.Empty

#End Region 'Properties

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

        Public Property FileType() As String = String.Empty

        Public Property MetaData() As MediaContainers.FileInfo = New MediaContainers.FileInfo

#End Region 'Properties

    End Class

    Public Class regexp

#Region "Properties"

        Public Property byDate() As Boolean = False

        Public Property defaultSeason() As Integer = -2 '-1 is reserved for "* All Seasons" entry

        Public Property ID() As Integer = -1

        Public Property Regexp() As String = String.Empty

#End Region 'Properties

    End Class

    Public Class VideoSourceByExtension

#Region "Fields"

        Private _extension As String = String.Empty

#End Region 'Fields

#Region "Properties"

        <XmlAttribute>
        Public Property Extension() As String
            Get
                Return _extension
            End Get
            Set(value As String)
                _extension = StringUtils.CleanFileExtension(value)
            End Set
        End Property

        <XmlText>
        Public Property VideoSource() As String = String.Empty

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