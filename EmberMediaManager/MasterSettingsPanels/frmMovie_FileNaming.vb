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
Imports EmberAPI

Public Class frmMovie_FileNaming
    Implements Interfaces.ISettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.ISettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.ISettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.ISettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.ISettingsPanel.NeedsReload_Movieset
    Public Event NeedsReload_TVEpisode() Implements Interfaces.ISettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.ISettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.ISettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.ISettingsPanel.SettingsChanged
    Public Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.ISettingsPanel.StateChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ChildType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ChildType

    Public Property Image As Image Implements Interfaces.ISettingsPanel.Image

    Public Property ImageIndex As Integer Implements Interfaces.ISettingsPanel.ImageIndex

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Master Panel Data
        ChildType = Enums.SettingsPanelType.MovieFileNaming
        IsEnabled = True
        Image = Nothing
        ImageIndex = 4
        Order = 300
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(471, "File Naming")
        ParentType = Enums.SettingsPanelType.Movie
        UniqueId = "Movie_FileNaming"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        'Actor Thumbs
        Dim strActorthumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkMovieActorthumbsExpertBDMV.Text = strActorthumbs
        chkMovieActorthumbsExpertMulti.Text = strActorthumbs
        chkMovieActorthumbsExpertSingle.Text = strActorthumbs
        chkMovieActorthumbsExpertVTS.Text = strActorthumbs
        lblMovieSourcesFilenamingKodiDefaultsActorthumbs.Text = strActorthumbs
        'also save unstacked
        Dim strAlsoSaveUnstacked = Master.eLang.GetString(1179, "also save unstacked")
        chkMovieUnstackExpertMulti.Text = strAlsoSaveUnstacked
        chkMovieUnstackExpertSingle.Text = strAlsoSaveUnstacked
        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblMovieSourcesFilenamingExpertBDMVBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertMultiBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertSingleBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertVTSBanner.Text = strBanner
        lblMovieSourcesFilenamingKodiADBanner.Text = strBanner
        lblMovieSourcesFilenamingKodiExtendedBanner.Text = strBanner
        lblMovieSourcesFilenamingNMTDefaultsBanner.Text = strBanner
        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        lblMovieSourcesFilenamingExpertBDMVClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertMultiClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertVTSClearArt.Text = strClearArt
        lblMovieSourcesFileNamingKodiADClearArt.Text = strClearArt
        lblMovieSourcesFileNamingKodiExtendedClearArt.Text = strClearArt
        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieSourcesFilenamingExpertBDMVClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertMultiClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertSingleClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertVTSClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingKodiADClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        gbMovieSourcesFilenamingBoxeeDefaultsOpts.Text = strDefaults
        gbMovieSourcesFilenamingNMTDefaultsOpts.Text = strDefaults
        gbMovieSourcesFilenamingKodiDefaultsOpts.Text = strDefaults
        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        lblMovieSourcesFilenamingExpertBDMVDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertMultiDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertSingleDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertVTSDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingKodiADDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt
        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblMovieSourcesFilenamingBoxeeDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiADEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblMovieSourcesFilenamingNMTDefaultsEnabled.Text = strEnabled
        chkMovieUseExpert.Text = strEnabled
        chkMovieThemeTvTunesEnabled.Text = strEnabled
        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        lblMovieSourcesFilenamingKodiDefaultsExtrafanarts.Text = strExtrafanarts
        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        lblMovieSourcesFilenamingKodiDefaultsExtrathumbs.Text = strExtrathumbs
        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblMovieSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        lblMovieSourcesFilenamingNMTDefaultsFanart.Text = strFanart
        lblMovieSourcesFilenamingKodiDefaultsFanart.Text = strFanart
        'KeyArt
        Dim strKeyArt As String = Master.eLang.GetString(296, "KeyArt")
        lblMovieSourcesFilenamingKodiExtendedKeyArt.Text = strKeyArt
        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblMovieSourcesFilenamingExpertBDMVLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertMultiLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertSingleLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertVTSLandscape.Text = strLandscape
        lblMovieSourcesFilenamingKodiADLandscape.Text = strLandscape
        lblMovieSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        lblMovieSourcesFilenamingBoxeeDefaultsNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertBDMVNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertMultiNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertSingleNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertVTSNFO.Text = strNFO
        lblMovieSourcesFilenamingKodiDefaultsNFO.Text = strNFO
        lblMovieSourcesFilenamingNMTDefaultsNFO.Text = strNFO
        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        gbMovieSourcesFilenamingExpertBDMVImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertMultiImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertSingleImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertVTSImagesOpts.Text = strOptionalImages
        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        gbMovieSourcesFilenamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertMultiOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertSingleOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertVTSOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingKodiOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingNMTOptionalOpts.Text = strOptionalSettings
        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblMovieSourcesFilenamingExpertBDMVPoster.Text = strPoster
        lblMovieSourcesFilenamingExpertMultiPoster.Text = strPoster
        lblMovieSourcesFilenamingExpertSinglePoster.Text = strPoster
        lblMovieSourcesFilenamingExpertVTSPoster.Text = strPoster
        lblMovieSourcesFilenamingBoxeeDefaultsPoster.Text = strPoster
        lblMovieSourcesFilenamingKodiDefaultsPoster.Text = strPoster
        lblMovieSourcesFilenamingNMTDefaultsPoster.Text = strPoster
        'Stack Filename
        Dim strStack As String = String.Format(Master.eLang.GetString(1178, "Stack {0}filename{1}"), "<", ">")
        chkMovieStackExpertMulti.Text = strStack
        chkMovieStackExpertSingle.Text = strStack
        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        lblMovieSourcesFilenamingExpertBDMVTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertMultiTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertSingleTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertVTSTrailer.Text = strTrailer
        lblMovieSourcesFilenamingKodiDefaultsTrailer.Text = strTrailer
        lblMovieSourcesFilenamingNMTDefaultsTrailer.Text = strTrailer
        'Use Base Directory
        Dim strUseBaseDirectory As String = Master.eLang.GetString(1180, "Use Base Directory")
        chkMovieUseBaseDirectoryExpertBDMV.Text = strUseBaseDirectory
        chkMovieUseBaseDirectoryExpertVTS.Text = strUseBaseDirectory

        tpMovieSourcesFilenamingExpert.Text = Master.eLang.GetString(439, "Expert")
        gbMovieSourcesFilenamingExpertOpts.Text = Master.eLang.GetString(1181, "Expert Settings")
        gbMovieSourcesFilenamingKodiExtendedOpts.Text = Master.eLang.GetString(822, "Extended Images")
        gbMovieSourcesFilenamingOpts.Text = Master.eLang.GetString(471, "File Naming")
        chkMovieThemeTvTunesMoviePath.Text = Master.eLang.GetString(1258, "Store themes in movie directory")
        chkMovieThemeTvTunesCustom.Text = Master.eLang.GetString(1259, "Store themes in a custom path")
        chkMovieThemeTvTunesSub.Text = Master.eLang.GetString(1260, "Store themes in sub directories")
        chkMovieSourcesBackdropsAuto.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        chkMovieRecognizeVTSExpertVTS.Text = String.Format(Master.eLang.GetString(537, "Recognize VIDEO_TS{0}without VIDEO_TS folder"), Environment.NewLine)
        chkMovieXBMCProtectVTSBDMV.Text = Master.eLang.GetString(1176, "Protect DVD/Bluray Structure")
        chkMovieYAMJWatchedFile.Text = Master.eLang.GetString(1177, "Use .watched Files")
        gbMovieSourcesBackdropsFolderOpts.Text = Master.eLang.GetString(520, "Backdrops Folder")
    End Sub

#End Region 'Dialog Methods 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        With Master.eSettings
            .MovieBackdropsPath = txtMovieSourcesBackdropsFolderPath.Text
            If Not String.IsNullOrEmpty(txtMovieSourcesBackdropsFolderPath.Text) Then
                .MovieBackdropsAuto = chkMovieSourcesBackdropsAuto.Checked
            Else
                .MovieBackdropsAuto = False
            End If

            '*************** XBMC Frodo settings ***************
            .MovieUseFrodo = chkMovieUseFrodo.Checked
            .MovieActorThumbsFrodo = chkActorthumbs_Kodi.Checked
            .MovieExtrafanartsFrodo = chkExtrafanarts_Kodi.Checked
            .MovieExtrathumbsFrodo = chkExtrathumbs_Kodi.Checked
            .MovieFanartFrodo = chkFanart_Kodi.Checked
            .MovieNFOFrodo = chkNfo_Kodi.Checked
            .MoviePosterFrodo = chkPoster_Kodi.Checked
            .MovieTrailerFrodo = chkTrailer_Kodi.Checked

            '*************** XBMC Eden settings ***************
            .MovieUseEden = chkMovieUseEden.Checked
            .MovieActorThumbsEden = chkMovieActorThumbsEden.Checked
            .MovieExtrafanartsEden = chkMovieExtrafanartsEden.Checked
            .MovieExtrathumbsEden = chkMovieExtrathumbsEden.Checked
            .MovieFanartEden = chkMovieFanartEden.Checked
            .MovieNFOEden = chkMovieNFOEden.Checked
            .MoviePosterEden = chkMoviePosterEden.Checked
            .MovieTrailerEden = chkMovieTrailerEden.Checked

            '************* XBMC optional settings *************
            .MovieXBMCProtectVTSBDMV = chkMovieXBMCProtectVTSBDMV.Checked

            '******** XBMC ArtworkDownloader settings **********
            .MovieUseAD = chkMovieUseAD.Checked
            .MovieBannerAD = chkMovieBannerAD.Checked
            .MovieClearArtAD = chkMovieClearArtAD.Checked
            .MovieClearLogoAD = chkMovieClearLogoAD.Checked
            .MovieDiscArtAD = chkMovieDiscArtAD.Checked
            .MovieLandscapeAD = chkMovieLandscapeAD.Checked

            '********* XBMC Extended Images settings ***********
            .MovieUseExtended = chkMovieUseExtended.Checked
            .MovieBannerExtended = chkBanner_Kodi.Checked
            .MovieClearArtExtended = chkClearart_Kodi.Checked
            .MovieClearLogoExtended = chkClearlogo_Kodi.Checked
            .MovieDiscArtExtended = chkDiscart_Kodi.Checked
            .MovieKeyartExtended = chkKeyart_Kodi.Checked
            .MovieLandscapeExtended = chkLandscape_Kodi.Checked

            '************** XBMC TvTunes settings **************
            .MovieThemeTvTunesCustom = chkMovieThemeTvTunesCustom.Checked
            .MovieThemeTvTunesCustomPath = txtMovieThemeTvTunesCustomPath.Text
            .MovieThemeTvTunesEnable = chkMovieThemeTvTunesEnabled.Checked
            .MovieThemeTvTunesMoviePath = chkMovieThemeTvTunesMoviePath.Checked
            .MovieThemeTvTunesSub = chkMovieThemeTvTunesSub.Checked
            .MovieThemeTvTunesSubDir = txtMovieThemeTvTunesSubDir.Text

            '****************** YAMJ settings *****************
            .MovieUseYAMJ = chkMovieUseYAMJ.Checked
            .MovieBannerYAMJ = chkMovieBannerYAMJ.Checked
            .MovieFanartYAMJ = chkMovieFanartYAMJ.Checked
            .MovieNFOYAMJ = chkMovieNFOYAMJ.Checked
            .MoviePosterYAMJ = chkMoviePosterYAMJ.Checked
            .MovieTrailerYAMJ = chkMovieTrailerYAMJ.Checked

            '****************** NMJ settings *****************
            .MovieUseNMJ = chkMovieUseNMJ.Checked
            .MovieBannerNMJ = chkMovieBannerNMJ.Checked
            .MovieFanartNMJ = chkMovieFanartNMJ.Checked
            .MovieNFONMJ = chkMovieNFONMJ.Checked
            .MoviePosterNMJ = chkMoviePosterNMJ.Checked
            .MovieTrailerNMJ = chkMovieTrailerNMJ.Checked

            '************** NMJ optional settings *************
            .MovieYAMJWatchedFile = chkMovieYAMJWatchedFile.Checked
            .MovieYAMJWatchedFolder = txtMovieYAMJWatchedFolder.Text

            '***************** Boxee settings *****************
            .MovieUseBoxee = chkMovieUseBoxee.Checked
            .MovieFanartBoxee = chkMovieFanartBoxee.Checked
            .MovieNFOBoxee = chkMovieNFOBoxee.Checked
            .MoviePosterBoxee = chkMoviePosterBoxee.Checked

            '***************** Expert settings ****************
            .MovieUseExpert = chkMovieUseExpert.Checked

            '***************** Expert Single ******************
            .MovieActorThumbsExpertSingle = chkMovieActorthumbsExpertSingle.Checked
            .MovieActorThumbsExtExpertSingle = txtMovieActorThumbsExtExpertSingle.Text
            .MovieBannerExpertSingle = txtMovieBannerExpertSingle.Text
            .MovieClearArtExpertSingle = txtMovieClearArtExpertSingle.Text
            .MovieClearLogoExpertSingle = txtMovieClearLogoExpertSingle.Text
            .MovieDiscArtExpertSingle = txtMovieDiscArtExpertSingle.Text
            .MovieExtrafanartsExpertSingle = chkMovieExtrafanartsExpertSingle.Checked
            .MovieExtrathumbsExpertSingle = chkMovieExtrathumbsExpertSingle.Checked
            .MovieFanartExpertSingle = txtMovieFanartExpertSingle.Text
            .MovieLandscapeExpertSingle = txtMovieLandscapeExpertSingle.Text
            .MovieNFOExpertSingle = txtMovieNFOExpertSingle.Text
            .MoviePosterExpertSingle = txtMoviePosterExpertSingle.Text
            .MovieStackExpertSingle = chkMovieStackExpertSingle.Checked
            .MovieTrailerExpertSingle = txtMovieTrailerExpertSingle.Text
            .MovieUnstackExpertSingle = chkMovieUnstackExpertSingle.Checked

            '***************** Expert Multi ******************
            .MovieActorThumbsExpertMulti = chkMovieActorthumbsExpertMulti.Checked
            .MovieActorThumbsExtExpertMulti = txtMovieActorThumbsExtExpertMulti.Text
            .MovieBannerExpertMulti = txtMovieBannerExpertMulti.Text
            .MovieClearArtExpertMulti = txtMovieClearArtExpertMulti.Text
            .MovieClearLogoExpertMulti = txtMovieClearLogoExpertMulti.Text
            .MovieDiscArtExpertMulti = txtMovieDiscArtExpertMulti.Text
            .MovieFanartExpertMulti = txtMovieFanartExpertMulti.Text
            .MovieLandscapeExpertMulti = txtMovieLandscapeExpertMulti.Text
            .MovieNFOExpertMulti = txtMovieNFOExpertMulti.Text
            .MoviePosterExpertMulti = txtMoviePosterExpertMulti.Text
            .MovieStackExpertMulti = chkMovieStackExpertMulti.Checked
            .MovieTrailerExpertMulti = txtMovieTrailerExpertMulti.Text
            .MovieUnstackExpertMulti = chkMovieUnstackExpertMulti.Checked

            '***************** Expert VTS ******************
            .MovieActorThumbsExpertVTS = chkMovieActorthumbsExpertVTS.Checked
            .MovieActorThumbsExtExpertVTS = txtMovieActorThumbsExtExpertVTS.Text
            .MovieBannerExpertVTS = txtMovieBannerExpertVTS.Text
            .MovieClearArtExpertVTS = txtMovieClearArtExpertVTS.Text
            .MovieClearLogoExpertVTS = txtMovieClearLogoExpertVTS.Text
            .MovieDiscArtExpertVTS = txtMovieDiscArtExpertVTS.Text
            .MovieExtrafanartsExpertVTS = chkMovieExtrafanartsExpertVTS.Checked
            .MovieExtrathumbsExpertVTS = chkMovieExtrathumbsExpertVTS.Checked
            .MovieFanartExpertVTS = txtMovieFanartExpertVTS.Text
            .MovieLandscapeExpertVTS = txtMovieLandscapeExpertVTS.Text
            .MovieNFOExpertVTS = txtMovieNFOExpertVTS.Text
            .MoviePosterExpertVTS = txtMoviePosterExpertVTS.Text
            .MovieRecognizeVTSExpertVTS = chkMovieRecognizeVTSExpertVTS.Checked
            .MovieTrailerExpertVTS = txtMovieTrailerExpertVTS.Text
            .MovieUseBaseDirectoryExpertVTS = chkMovieUseBaseDirectoryExpertVTS.Checked

            '***************** Expert BDMV ******************
            .MovieActorThumbsExpertBDMV = chkMovieActorthumbsExpertBDMV.Checked
            .MovieActorThumbsExtExpertBDMV = txtMovieActorThumbsExtExpertBDMV.Text
            .MovieBannerExpertBDMV = txtMovieBannerExpertBDMV.Text
            .MovieClearArtExpertBDMV = txtMovieClearArtExpertBDMV.Text
            .MovieClearLogoExpertBDMV = txtMovieClearLogoExpertBDMV.Text
            .MovieDiscArtExpertBDMV = txtMovieDiscArtExpertBDMV.Text
            .MovieExtrafanartsExpertBDMV = chkMovieExtrafanartsExpertBDMV.Checked
            .MovieExtrathumbsExpertBDMV = chkMovieExtrathumbsExpertBDMV.Checked
            .MovieFanartExpertBDMV = txtMovieFanartExpertBDMV.Text
            .MovieLandscapeExpertBDMV = txtMovieLandscapeExpertBDMV.Text
            .MovieNFOExpertBDMV = txtMovieNFOExpertBDMV.Text
            .MoviePosterExpertBDMV = txtMoviePosterExpertBDMV.Text
            .MovieTrailerExpertBDMV = txtMovieTrailerExpertBDMV.Text
            .MovieUseBaseDirectoryExpertBDMV = chkMovieUseBaseDirectoryExpertBDMV.Checked
        End With
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            chkMovieSourcesBackdropsAuto.Checked = .MovieBackdropsAuto
            txtMovieSourcesBackdropsFolderPath.Text = .MovieBackdropsPath

            '*************** XBMC Frodo settings ***************
            chkMovieUseFrodo.Checked = .MovieUseFrodo
            chkActorthumbs_Kodi.Checked = .MovieActorThumbsFrodo
            chkExtrafanarts_Kodi.Checked = .MovieExtrafanartsFrodo
            chkExtrathumbs_Kodi.Checked = .MovieExtrathumbsFrodo
            chkFanart_Kodi.Checked = .MovieFanartFrodo
            chkNfo_Kodi.Checked = .MovieNFOFrodo
            chkPoster_Kodi.Checked = .MoviePosterFrodo
            chkTrailer_Kodi.Checked = .MovieTrailerFrodo

            '*************** XBMC Eden settings ****************
            chkMovieUseEden.Checked = .MovieUseEden
            chkMovieActorThumbsEden.Checked = .MovieActorThumbsEden
            chkMovieExtrafanartsEden.Checked = .MovieExtrafanartsEden
            chkMovieExtrathumbsEden.Checked = .MovieExtrathumbsEden
            chkMovieFanartEden.Checked = .MovieFanartEden
            chkMovieNFOEden.Checked = .MovieNFOEden
            chkMoviePosterEden.Checked = .MoviePosterEden
            chkMovieTrailerEden.Checked = .MovieTrailerEden

            '************* XBMC optional settings **************
            chkMovieXBMCProtectVTSBDMV.Checked = .MovieXBMCProtectVTSBDMV

            '******** XBMC ArtworkDownloader settings **********
            chkMovieUseAD.Checked = .MovieUseAD
            chkMovieBannerAD.Checked = .MovieBannerAD
            chkMovieClearArtAD.Checked = .MovieClearArtAD
            chkMovieClearLogoAD.Checked = .MovieClearLogoAD
            chkMovieDiscArtAD.Checked = .MovieDiscArtAD
            chkMovieLandscapeAD.Checked = .MovieLandscapeAD

            '********* XBMC Extended Images settings ***********
            chkMovieUseExtended.Checked = .MovieUseExtended
            chkBanner_Kodi.Checked = .MovieBannerExtended
            chkClearart_Kodi.Checked = .MovieClearArtExtended
            chkClearlogo_Kodi.Checked = .MovieClearLogoExtended
            chkDiscart_Kodi.Checked = .MovieDiscArtExtended
            chkKeyart_Kodi.Checked = .MovieKeyartExtended
            chkLandscape_Kodi.Checked = .MovieLandscapeExtended

            '************** XBMC TvTunes settings **************
            chkMovieThemeTvTunesEnabled.Checked = .MovieThemeTvTunesEnable
            chkMovieThemeTvTunesCustom.Checked = .MovieThemeTvTunesCustom
            chkMovieThemeTvTunesMoviePath.Checked = .MovieThemeTvTunesMoviePath
            chkMovieThemeTvTunesSub.Checked = .MovieThemeTvTunesSub
            txtMovieThemeTvTunesCustomPath.Text = .MovieThemeTvTunesCustomPath
            txtMovieThemeTvTunesSubDir.Text = .MovieThemeTvTunesSubDir

            '****************** YAMJ settings ******************
            chkMovieUseYAMJ.Checked = .MovieUseYAMJ
            chkMovieBannerYAMJ.Checked = .MovieBannerYAMJ
            chkMovieFanartYAMJ.Checked = .MovieFanartYAMJ
            chkMovieNFOYAMJ.Checked = .MovieNFOYAMJ
            chkMoviePosterYAMJ.Checked = .MoviePosterYAMJ
            chkMovieTrailerYAMJ.Checked = .MovieTrailerYAMJ

            '****************** NMJ settings ******************
            chkMovieUseNMJ.Checked = .MovieUseNMJ
            chkMovieBannerNMJ.Checked = .MovieBannerNMJ
            chkMovieFanartNMJ.Checked = .MovieFanartNMJ
            chkMovieNFONMJ.Checked = .MovieNFONMJ
            chkMoviePosterNMJ.Checked = .MoviePosterNMJ
            chkMovieTrailerNMJ.Checked = .MovieTrailerNMJ

            '************** NMT optional settings **************
            chkMovieYAMJWatchedFile.Checked = .MovieYAMJWatchedFile
            txtMovieYAMJWatchedFolder.Text = .MovieYAMJWatchedFolder

            '***************** Boxee settings ******************
            chkMovieUseBoxee.Checked = .MovieUseBoxee
            chkMovieFanartBoxee.Checked = .MovieFanartBoxee
            chkMovieNFOBoxee.Checked = .MovieNFOBoxee
            chkMoviePosterBoxee.Checked = .MoviePosterBoxee

            '***************** Expert settings *****************
            chkMovieUseExpert.Checked = .MovieUseExpert

            '***************** Expert Single *******************
            chkMovieActorthumbsExpertSingle.Checked = .MovieActorThumbsExpertSingle
            chkMovieExtrafanartsExpertSingle.Checked = .MovieExtrafanartsExpertSingle
            chkMovieExtrathumbsExpertSingle.Checked = .MovieExtrathumbsExpertSingle
            chkMovieStackExpertSingle.Checked = .MovieStackExpertSingle
            chkMovieUnstackExpertSingle.Checked = .MovieUnstackExpertSingle
            txtMovieActorThumbsExtExpertSingle.Text = .MovieActorThumbsExtExpertSingle
            txtMovieBannerExpertSingle.Text = .MovieBannerExpertSingle
            txtMovieClearArtExpertSingle.Text = .MovieClearArtExpertSingle
            txtMovieClearLogoExpertSingle.Text = .MovieClearLogoExpertSingle
            txtMovieDiscArtExpertSingle.Text = .MovieDiscArtExpertSingle
            txtMovieFanartExpertSingle.Text = .MovieFanartExpertSingle
            txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
            txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
            txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
            txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle

            '******************* Expert Multi ******************
            chkMovieActorthumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
            chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti
            chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
            txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
            txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
            txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
            txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
            txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
            txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
            txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
            txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
            txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
            txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti

            '******************* Expert VTS *******************
            chkMovieActorthumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
            chkMovieExtrafanartsExpertVTS.Checked = .MovieExtrafanartsExpertVTS
            chkMovieExtrathumbsExpertVTS.Checked = .MovieExtrathumbsExpertVTS
            chkMovieRecognizeVTSExpertVTS.Checked = .MovieRecognizeVTSExpertVTS
            chkMovieUseBaseDirectoryExpertVTS.Checked = .MovieUseBaseDirectoryExpertVTS
            txtMovieActorThumbsExtExpertVTS.Text = .MovieActorThumbsExtExpertVTS
            txtMovieBannerExpertVTS.Text = .MovieBannerExpertVTS
            txtMovieClearArtExpertVTS.Text = .MovieClearArtExpertVTS
            txtMovieClearLogoExpertVTS.Text = .MovieClearLogoExpertVTS
            txtMovieDiscArtExpertVTS.Text = .MovieDiscArtExpertVTS
            txtMovieFanartExpertVTS.Text = .MovieFanartExpertVTS
            txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
            txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
            txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
            txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS

            '******************* Expert BDMV *******************
            chkMovieActorthumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
            chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
            chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
            chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV
            txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
            txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
            txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
            txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
            txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
            txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
            txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
            txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
            txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
            txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV
        End With
    End Sub

    Private Sub btnMovieBackdropsPathBrowse_Click(ByVal sender As Object, ByVal e As EventArgs)
        With fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(552, "Select the folder where you wish to store your backdrops...")
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtMovieSourcesBackdropsFolderPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub chkMovieUseAD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieBannerAD.Enabled = chkMovieUseAD.Checked
        chkMovieClearArtAD.Enabled = chkMovieUseAD.Checked
        chkMovieClearLogoAD.Enabled = chkMovieUseAD.Checked
        chkMovieDiscArtAD.Enabled = chkMovieUseAD.Checked
        chkMovieLandscapeAD.Enabled = chkMovieUseAD.Checked

        If Not chkMovieUseAD.Checked Then
            chkMovieBannerAD.Checked = False
            chkMovieClearArtAD.Checked = False
            chkMovieClearLogoAD.Checked = False
            chkMovieDiscArtAD.Checked = False
            chkMovieLandscapeAD.Checked = False
        Else
            chkMovieBannerAD.Checked = True
            chkMovieClearArtAD.Checked = True
            chkMovieClearLogoAD.Checked = True
            chkMovieDiscArtAD.Checked = True
            chkMovieLandscapeAD.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieFanartBoxee.Enabled = chkMovieUseBoxee.Checked
        chkMovieNFOBoxee.Enabled = chkMovieUseBoxee.Checked
        chkMoviePosterBoxee.Enabled = chkMovieUseBoxee.Checked

        If Not chkMovieUseBoxee.Checked Then
            chkMovieFanartBoxee.Checked = False
            chkMovieNFOBoxee.Checked = False
            chkMoviePosterBoxee.Checked = False
        Else
            chkMovieFanartBoxee.Checked = True
            chkMovieNFOBoxee.Checked = True
            chkMoviePosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseKodiExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseExtended.CheckedChanged
        RaiseEvent SettingsChanged()

        chkBanner_Kodi.Enabled = chkMovieUseExtended.Checked
        chkClearart_Kodi.Enabled = chkMovieUseExtended.Checked
        chkClearlogo_Kodi.Enabled = chkMovieUseExtended.Checked
        chkDiscart_Kodi.Enabled = chkMovieUseExtended.Checked
        chkKeyart_Kodi.Enabled = chkMovieUseExtended.Checked
        chkLandscape_Kodi.Enabled = chkMovieUseExtended.Checked

        If Not chkMovieUseExtended.Checked Then
            chkBanner_Kodi.Checked = False
            chkClearart_Kodi.Checked = False
            chkClearlogo_Kodi.Checked = False
            chkDiscart_Kodi.Checked = False
            chkKeyart_Kodi.Checked = False
            chkLandscape_Kodi.Checked = False
        Else
            chkBanner_Kodi.Checked = True
            chkClearart_Kodi.Checked = True
            chkClearlogo_Kodi.Checked = True
            chkDiscart_Kodi.Checked = True
            chkKeyart_Kodi.Checked = True
            chkLandscape_Kodi.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseFrodo.CheckedChanged
        RaiseEvent SettingsChanged()

        chkActorthumbs_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkExtrafanarts_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkExtrathumbs_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkFanart_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkNfo_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkPoster_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkTrailer_Kodi.Enabled = chkMovieUseFrodo.Checked
        chkMovieXBMCProtectVTSBDMV.Enabled = chkMovieUseFrodo.Checked AndAlso Not chkMovieUseEden.Checked

        If Not chkMovieUseFrodo.Checked Then
            chkActorthumbs_Kodi.Checked = False
            chkExtrafanarts_Kodi.Checked = False
            chkExtrathumbs_Kodi.Checked = False
            chkFanart_Kodi.Checked = False
            chkNfo_Kodi.Checked = False
            chkPoster_Kodi.Checked = False
            chkTrailer_Kodi.Checked = False
            chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            chkActorthumbs_Kodi.Checked = True
            chkExtrafanarts_Kodi.Checked = True
            chkExtrathumbs_Kodi.Checked = True
            chkFanart_Kodi.Checked = True
            chkNfo_Kodi.Checked = True
            chkPoster_Kodi.Checked = True
            chkTrailer_Kodi.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseEden_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieActorThumbsEden.Enabled = chkMovieUseEden.Checked
        chkMovieExtrafanartsEden.Enabled = chkMovieUseEden.Checked
        chkMovieExtrathumbsEden.Enabled = chkMovieUseEden.Checked
        chkMovieFanartEden.Enabled = chkMovieUseEden.Checked
        chkMovieNFOEden.Enabled = chkMovieUseEden.Checked
        chkMoviePosterEden.Enabled = chkMovieUseEden.Checked
        chkMovieTrailerEden.Enabled = chkMovieUseEden.Checked
        chkMovieXBMCProtectVTSBDMV.Enabled = Not chkMovieUseEden.Checked AndAlso chkMovieUseFrodo.Checked

        If Not chkMovieUseEden.Checked Then
            chkMovieActorThumbsEden.Checked = False
            chkMovieExtrafanartsEden.Checked = False
            chkMovieExtrathumbsEden.Checked = False
            chkMovieFanartEden.Checked = False
            chkMovieNFOEden.Checked = False
            chkMoviePosterEden.Checked = False
            chkMovieTrailerEden.Checked = False
        Else
            chkMovieActorThumbsEden.Checked = True
            chkMovieExtrafanartsEden.Checked = True
            chkMovieExtrathumbsEden.Checked = True
            chkMovieFanartEden.Checked = True
            chkMovieNFOEden.Checked = True
            chkMoviePosterEden.Checked = True
            chkMovieTrailerEden.Checked = True
            chkMovieXBMCProtectVTSBDMV.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseYAMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieBannerYAMJ.Enabled = chkMovieUseYAMJ.Checked
        chkMovieFanartYAMJ.Enabled = chkMovieUseYAMJ.Checked
        chkMovieNFOYAMJ.Enabled = chkMovieUseYAMJ.Checked
        chkMoviePosterYAMJ.Enabled = chkMovieUseYAMJ.Checked
        chkMovieTrailerYAMJ.Enabled = chkMovieUseYAMJ.Checked
        chkMovieYAMJWatchedFile.Enabled = chkMovieUseYAMJ.Checked

        If Not chkMovieUseYAMJ.Checked Then
            chkMovieBannerYAMJ.Checked = False
            chkMovieFanartYAMJ.Checked = False
            chkMovieNFOYAMJ.Checked = False
            chkMoviePosterYAMJ.Checked = False
            chkMovieTrailerYAMJ.Checked = False
            chkMovieYAMJWatchedFile.Checked = False
        Else
            chkMovieBannerYAMJ.Checked = True
            chkMovieFanartYAMJ.Checked = True
            chkMovieNFOYAMJ.Checked = True
            chkMoviePosterYAMJ.Checked = True
            chkMovieTrailerYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieBannerNMJ.Enabled = chkMovieUseNMJ.Checked
        chkMovieFanartNMJ.Enabled = chkMovieUseNMJ.Checked
        chkMovieNFONMJ.Enabled = chkMovieUseNMJ.Checked
        chkMoviePosterNMJ.Enabled = chkMovieUseNMJ.Checked
        chkMovieTrailerNMJ.Enabled = chkMovieUseNMJ.Checked

        If Not chkMovieUseNMJ.Checked Then
            chkMovieBannerNMJ.Checked = False
            chkMovieFanartNMJ.Checked = False
            chkMovieNFONMJ.Checked = False
            chkMoviePosterNMJ.Checked = False
            chkMovieTrailerNMJ.Checked = False
        Else
            chkMovieBannerNMJ.Checked = True
            chkMovieFanartNMJ.Checked = True
            chkMovieNFONMJ.Checked = True
            chkMoviePosterNMJ.Checked = True
            chkMovieTrailerNMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtMovieThemeTvTunesCustomPath.Enabled = chkMovieThemeTvTunesCustom.Checked
        btnMovieThemeTvTunesCustomPathBrowse.Enabled = chkMovieThemeTvTunesCustom.Checked

        If chkMovieThemeTvTunesCustom.Checked Then
            chkMovieThemeTvTunesMoviePath.Enabled = False
            chkMovieThemeTvTunesMoviePath.Checked = False
            chkMovieThemeTvTunesSub.Enabled = False
            chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not chkMovieThemeTvTunesCustom.Checked AndAlso chkMovieThemeTvTunesEnabled.Checked Then
            chkMovieThemeTvTunesMoviePath.Enabled = True
            chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieThemeTvTunesCustom.Enabled = chkMovieThemeTvTunesEnabled.Checked
        chkMovieThemeTvTunesMoviePath.Enabled = chkMovieThemeTvTunesEnabled.Checked
        chkMovieThemeTvTunesSub.Enabled = chkMovieThemeTvTunesEnabled.Checked

        If Not chkMovieThemeTvTunesEnabled.Checked Then
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesMoviePath.Checked = False
            chkMovieThemeTvTunesSub.Checked = False
        Else
            chkMovieThemeTvTunesMoviePath.Checked = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesMoviePath_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        If chkMovieThemeTvTunesMoviePath.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = False
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesSub.Enabled = False
            chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not chkMovieThemeTvTunesMoviePath.Checked AndAlso chkMovieThemeTvTunesEnabled.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = True
            chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieThemeTvTunesSub_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtMovieThemeTvTunesSubDir.Enabled = chkMovieThemeTvTunesSub.Checked

        If chkMovieThemeTvTunesSub.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = False
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesMoviePath.Enabled = False
            chkMovieThemeTvTunesMoviePath.Checked = False
        End If

        If Not chkMovieThemeTvTunesSub.Checked AndAlso chkMovieThemeTvTunesEnabled.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = True
            chkMovieThemeTvTunesMoviePath.Enabled = True
        End If
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        txtMovieYAMJWatchedFolder.Enabled = chkMovieYAMJWatchedFile.Checked
        btnMovieYAMJWatchedFilesBrowse.Enabled = chkMovieYAMJWatchedFile.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub txtMovieBackdropsPath_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        If String.IsNullOrEmpty(txtMovieSourcesBackdropsFolderPath.Text) Then
            chkMovieSourcesBackdropsAuto.Checked = False
            chkMovieSourcesBackdropsAuto.Enabled = False
        Else
            chkMovieSourcesBackdropsAuto.Enabled = True
        End If
    End Sub

    Private Sub btnMovieThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs)
        With fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtMovieThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieYAMJWatchedFilesBrowse_Click(sender As Object, e As EventArgs)
        With fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1029, "Select the folder where you wish to store your watched files...")
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtMovieYAMJWatchedFolder.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub chkMovieUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        chkMovieActorthumbsExpertBDMV.Enabled = chkMovieUseExpert.Checked
        chkMovieActorthumbsExpertMulti.Enabled = chkMovieUseExpert.Checked
        chkMovieActorthumbsExpertSingle.Enabled = chkMovieUseExpert.Checked
        chkMovieActorthumbsExpertVTS.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrafanartsExpertBDMV.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrafanartsExpertSingle.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrafanartsExpertVTS.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrathumbsExpertBDMV.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrathumbsExpertSingle.Enabled = chkMovieUseExpert.Checked
        chkMovieExtrathumbsExpertVTS.Enabled = chkMovieUseExpert.Checked
        chkMovieRecognizeVTSExpertVTS.Enabled = chkMovieUseExpert.Checked
        chkMovieStackExpertMulti.Enabled = chkMovieUseExpert.Checked
        chkMovieStackExpertSingle.Enabled = chkMovieUseExpert.Checked
        chkMovieUnstackExpertMulti.Enabled = chkMovieStackExpertMulti.Enabled AndAlso chkMovieStackExpertMulti.Checked
        chkMovieUnstackExpertSingle.Enabled = chkMovieStackExpertSingle.Enabled AndAlso chkMovieStackExpertSingle.Checked
        chkMovieUseBaseDirectoryExpertBDMV.Enabled = chkMovieUseExpert.Checked
        chkMovieUseBaseDirectoryExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieActorThumbsExtExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieActorThumbsExtExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieActorThumbsExtExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieActorThumbsExtExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieBannerExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieBannerExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieBannerExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieBannerExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieClearArtExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieClearArtExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieClearArtExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieClearArtExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieClearLogoExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieClearLogoExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieClearLogoExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieClearLogoExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieDiscArtExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieDiscArtExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieDiscArtExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieDiscArtExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieFanartExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieFanartExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieFanartExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieFanartExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieLandscapeExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieLandscapeExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieLandscapeExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieLandscapeExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieNFOExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieNFOExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieNFOExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieNFOExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMoviePosterExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMoviePosterExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMoviePosterExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMoviePosterExpertVTS.Enabled = chkMovieUseExpert.Checked
        txtMovieTrailerExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieTrailerExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieTrailerExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieTrailerExpertVTS.Enabled = chkMovieUseExpert.Checked
    End Sub

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkMovieUnstackExpertSingle.Enabled = chkMovieStackExpertSingle.Checked AndAlso chkMovieStackExpertSingle.Enabled
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkMovieUnstackExpertMulti.Enabled = chkMovieStackExpertMulti.Checked AndAlso chkMovieStackExpertMulti.Enabled
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub pbMovieSourcesADInfo_Click(sender As Object, e As EventArgs)
        Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader")
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_Click(sender As Object, e As EventArgs)
        Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseEnter(sender As Object, e As EventArgs)
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseLeave(sender As Object, e As EventArgs)
        Cursor = Cursors.Default
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs)
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs)
        Cursor = Cursors.Default
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class