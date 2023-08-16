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
Imports System.IO

Public Class frmTV_FileNaming
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _TmpSeasonTitleBlacklist As New List(Of String)

#End Region 'Fields

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
        ChildType = Enums.SettingsPanelType.TVFileNaming
        IsEnabled = True
        Image = Nothing
        ImageIndex = 4
        Order = 300
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(471, "File Naming")
        ParentType = Enums.SettingsPanelType.TV
        UniqueId = "TV_FileNaming"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()

        'Actor Thumbs
        Dim strActorthumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkTVEpisodeActorthumbsExpert.Text = strActorthumbs
        chkTVShowActorthumbsExpert.Text = strActorthumbs
        lblTVSourcesFilenamingKodiDefaultsActorthumbs.Text = strActorthumbs

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblTVSourcesFilenamingExpertAllSeasonsBanner.Text = strBanner
        lblTVSourcesFilenamingExpertSeasonBanner.Text = strBanner
        lblTVSourcesFilenamingExpertShowBanner.Text = strBanner
        lblTVSourcesFilenamingBoxeeDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingKodiDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingNMTDefaultsBanner.Text = strBanner

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        lblTVShowCharacterArtExpert.Text = strCharacterArt
        lblTVSourcesFilenamingKodiADCharacterArt.Text = strCharacterArt
        lblTVSourcesFilenamingKodiExtendedCharacterArt.Text = strCharacterArt

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        lblTVSourcesFilenamingExpertClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiADClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiExtendedClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        lblTVSourcesFilenamingExpertClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiADClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        gbTVSourcesFilenamingBoxeeDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingNMTDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingKodiDefaultsOpts.Text = strDefaults

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblTVSourcesFilenamingBoxeeDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiADEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblTVSourcesFilenamingNMTDefaultsEnabled.Text = strEnabled
        chkTVShowThemeTvTunesEnabled.Text = strEnabled
        chkTVUseExpert.Text = strEnabled

        'Episode
        Dim strEpisode As String = Master.eLang.GetString(727, "Episode")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeEpisode.Text = strEpisode
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJEpisode.Text = strEpisode
        tpTVSourcesFilenamingExpertEpisode.Text = strEpisode

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkTVShowExtrafanartsExpert.Text = strExtrafanarts
        lblTVSourcesFilenamingKodiDefaultsExtrafanarts.Text = strExtrafanarts

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblTVSourcesFilenamingExpertAllSeasonsFanart.Text = strFanart
        lblTVSourcesFilenamingExpertEpisodeFanart.Text = strFanart
        lblTVSourcesFilenamingExpertSeasonFanart.Text = strFanart
        lblTVSourcesFilenamingExpertShowFanart.Text = strFanart
        lblTVSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingKodiDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingNMTDefaultsFanart.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblTVSourcesFilenamingExpertAllSeasonsLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertSeasonLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertShowLandscape.Text = strLandscape

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        lblTVSourcesFilenamingExpertEpisodeNFO.Text = strNFO
        lblTVSourcesFilenamingExpertShowNFO.Text = strNFO
        lblTVSourcesFilenamingKodiDefaultsNFO.Text = strNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        gbTVSourcesFilenamingExpertEpisodeImagesOpts.Text = strOptionalImages
        gbTVSourcesFilenamingExpertShowImagesOpts.Text = strOptionalImages

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblTVAllSeasonsPosterExpert.Text = strPoster
        lblTVEpisodePosterExpert.Text = strPoster
        lblTVSeasonPosterExpert.Text = strPoster
        lblTVShowPosterExpert.Text = strPoster
        lblTVSourcesFilenamingBoxeeDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingKodiDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingNMTDefaultsPoster.Text = strPoster

        'Season
        Dim strSeason As String = Master.eLang.GetString(650, "Season")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJSeason.Text = strSeason
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJSeason.Text = strSeason
        tpTVSourcesFilenamingExpertSeason.Text = strSeason

        'Season Landscape
        Dim strSeasonLandscape As String = Master.eLang.GetString(1018, "Season Landscape")
        lblTVSourcesFilenamingKodiADSeasonLandscape.Text = strSeasonLandscape
        lblTVSourcesFilenamingKodiExtendedSeasonLandscape.Text = strSeasonLandscape

        'TV Show
        Dim strTVShow As String = Master.eLang.GetString(700, "TV Show")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeTVShow.Text = strTVShow
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJTVShow.Text = strTVShow
        tpTVSourcesFilenamingExpertTVShow.Text = strTVShow

        'TV Show Landscape
        Dim strTVShowLandscape As String = Master.eLang.GetString(1010, "TV Show Landscape")
        lblTVSourcesFilenamingKodiADTVShowLandscape.Text = strTVShowLandscape
        lblTVSourcesFilenamingKodiExtendedTVShowLandscape.Text = strTVShowLandscape

        tpTVSourcesFilenamingExpertAllSeasons.Text = Master.eLang.GetString(1202, "All Seasons")
        tpTVSourcesFilenamingExpert.Text = Master.eLang.GetString(439, "Expert")
        gbTVSourcesFilenamingExpertOpts.Text = Master.eLang.GetString(1181, "Expert Settings")
        gbTVSourcesFilenamingKodiExtendedOpts.Text = Master.eLang.GetString(822, "Extended Images")
        gbTVSourcesFilenamingOpts.Text = Master.eLang.GetString(471, "File Naming")
        lblTVSourcesFilenamingKodiExtendedKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
        chkTVShowThemeTvTunesCustom.Text = Master.eLang.GetString(1259, "Store themes in a custom path")
        chkTVShowThemeTvTunesSub.Text = Master.eLang.GetString(1260, "Store themes in sub directories")
        chkTVShowThemeTvTunesShowPath.Text = Master.eLang.GetString(1265, "Store themes in tv show directory")
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

            '*************** XBMC Frodo settings ***************
            .TVUseFrodo = chkTVUseFrodo.Checked
            .TVEpisodeActorThumbsFrodo = chkTVEpisodeActorThumbsFrodo.Checked
            .TVEpisodeNFOFrodo = chkTVEpisodeNFOFrodo.Checked
            .TVEpisodePosterFrodo = chkTVEpisodePosterFrodo.Checked
            .TVSeasonBannerFrodo = chkTVSeasonBannerFrodo.Checked
            .TVSeasonFanartFrodo = chkTVSeasonFanartFrodo.Checked
            .TVSeasonPosterFrodo = chkTVSeasonPosterFrodo.Checked
            .TVShowActorThumbsFrodo = chkTVShowActorThumbsFrodo.Checked
            .TVShowBannerFrodo = chkTVShowBannerFrodo.Checked
            .TVShowExtrafanartsFrodo = chkTVShowExtrafanartsFrodo.Checked
            .TVShowFanartFrodo = chkTVShowFanartFrodo.Checked
            .TVShowNFOFrodo = chkTVShowNFOFrodo.Checked
            .TVShowPosterFrodo = chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC ArtworkDownloader settings **************
            .TVUseAD = chkTVUseAD.Checked
            .TVSeasonLandscapeAD = chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = chkTVShowClearLogoAD.Checked
            .TVShowLandscapeAD = chkTVShowLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .TVShowThemeTvTunesEnable = chkTVShowThemeTvTunesEnabled.Checked
            .TVShowThemeTvTunesCustom = chkTVShowThemeTvTunesCustom.Checked
            .TVShowThemeTvTunesCustomPath = txtTVShowThemeTvTunesCustomPath.Text
            .TVShowThemeTvTunesShowPath = chkTVShowThemeTvTunesShowPath.Checked
            .TVShowThemeTvTunesSub = chkTVShowThemeTvTunesSub.Checked
            .TVShowThemeTvTunesSubDir = txtTVShowThemeTvTunesSubDir.Text

            '********* XBMC Extended Images settings ***********
            .TVUseExtended = chkTVUseExtended.Checked
            .TVSeasonLandscapeExtended = chkTVSeasonLandscapeExtended.Checked
            .TVShowCharacterArtExtended = chkTVShowCharacterArtExtended.Checked
            .TVShowClearArtExtended = chkTVShowClearArtExtended.Checked
            .TVShowClearLogoExtended = chkTVShowClearLogoExtended.Checked
            .TVShowKeyartExtended = chkTVShowKeyArtExtended.Checked
            .TVShowLandscapeExtended = chkTVShowLandscapeExtended.Checked

            '****************** YAMJ settings ******************
            .TVUseYAMJ = chkTVUseYAMJ.Checked
            .TVEpisodeNFOYAMJ = chkTVEpisodeNFOYAMJ.Checked
            .TVEpisodePosterYAMJ = chkTVEpisodePosterYAMJ.Checked
            .TVSeasonBannerYAMJ = chkTVSeasonBannerYAMJ.Checked
            .TVSeasonFanartYAMJ = chkTVSeasonFanartYAMJ.Checked
            .TVSeasonPosterYAMJ = chkTVSeasonPosterYAMJ.Checked
            .TVShowBannerYAMJ = chkTVShowBannerYAMJ.Checked
            .TVShowFanartYAMJ = chkTVShowFanartYAMJ.Checked
            .TVShowNFOYAMJ = chkTVShowNFOYAMJ.Checked
            .TVShowPosterYAMJ = chkTVShowPosterYAMJ.Checked

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            .TVUseBoxee = chkTVUseBoxee.Checked
            .TVEpisodeNFOBoxee = chkTVEpisodeNFOBoxee.Checked
            .TVEpisodePosterBoxee = chkTVEpisodePosterBoxee.Checked
            .TVSeasonPosterBoxee = chkTVSeasonPosterBoxee.Checked
            .TVShowBannerBoxee = chkTVShowBannerBoxee.Checked
            .TVShowFanartBoxee = chkTVShowFanartBoxee.Checked
            .TVShowNFOBoxee = chkTVShowNFOBoxee.Checked
            .TVShowPosterBoxee = chkTVShowPosterBoxee.Checked

            '***************** Expert settings ******************
            .TVUseExpert = chkTVUseExpert.Checked

            '***************** Expert AllSeasons ****************
            .TVAllSeasonsBannerExpert = txtTVAllSeasonsBannerExpert.Text
            .TVAllSeasonsFanartExpert = txtTVAllSeasonsFanartExpert.Text
            .TVAllSeasonsLandscapeExpert = txtTVAllSeasonsLandscapeExpert.Text
            .TVAllSeasonsPosterExpert = txtTVAllSeasonsPosterExpert.Text

            '***************** Expert Episode *******************
            .TVEpisodeActorThumbsExpert = chkTVEpisodeActorthumbsExpert.Checked
            .TVEpisodeActorThumbsExtExpert = txtTVEpisodeActorThumbsExtExpert.Text
            .TVEpisodeFanartExpert = txtTVEpisodeFanartExpert.Text
            .TVEpisodeNFOExpert = txtTVEpisodeNFOExpert.Text
            .TVEpisodePosterExpert = txtTVEpisodePosterExpert.Text

            '***************** Expert Season ********************
            .TVSeasonBannerExpert = txtTVSeasonBannerExpert.Text
            .TVSeasonFanartExpert = txtTVSeasonFanartExpert.Text
            .TVSeasonLandscapeExpert = txtTVSeasonLandscapeExpert.Text
            .TVSeasonPosterExpert = txtTVSeasonPosterExpert.Text

            '***************** Expert Show **********************
            .TVShowActorThumbsExpert = chkTVShowActorthumbsExpert.Checked
            .TVShowActorThumbsExtExpert = txtTVShowActorThumbsExtExpert.Text
            .TVShowBannerExpert = txtTVShowBannerExpert.Text
            .TVShowCharacterArtExpert = txtTVShowCharacterArtExpert.Text
            .TVShowClearArtExpert = txtTVShowClearArtExpert.Text
            .TVShowClearLogoExpert = txtTVShowClearLogoExpert.Text
            .TVShowExtrafanartsExpert = chkTVShowExtrafanartsExpert.Checked
            .TVShowFanartExpert = txtTVShowFanartExpert.Text
            .TVShowLandscapeExpert = txtTVShowLandscapeExpert.Text
            .TVShowNFOExpert = txtTVShowNFOExpert.Text
            .TVShowPosterExpert = txtTVShowPosterExpert.Text
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings

            '*************** XBMC Frodo settings ***************
            chkTVUseFrodo.Checked = .TVUseFrodo
            chkTVEpisodeActorThumbsFrodo.Checked = .TVEpisodeActorThumbsFrodo
            chkTVEpisodeNFOFrodo.Checked = .TVEpisodeNFOFrodo
            chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
            chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
            chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
            chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
            chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
            chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
            chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            chkTVShowNFOFrodo.Checked = .TVShowNFOFrodo
            chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            chkTVUseAD.Checked = .TVUseAD
            chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '********* XBMC Extended Images settings ***********
            chkTVUseExtended.Checked = .TVUseExtended
            chkTVSeasonLandscapeExtended.Checked = .TVSeasonLandscapeExtended
            chkTVShowCharacterArtExtended.Checked = .TVShowCharacterArtExtended
            chkTVShowClearArtExtended.Checked = .TVShowClearArtExtended
            chkTVShowClearLogoExtended.Checked = .TVShowClearLogoExtended
            chkTVShowKeyArtExtended.Checked = .TVShowKeyArtExtended
            chkTVShowLandscapeExtended.Checked = .TVShowLandscapeExtended

            '************* XBMC TvTunes settings ***************
            chkTVShowThemeTvTunesEnabled.Checked = .TVShowThemeTvTunesEnable
            chkTVShowThemeTvTunesCustom.Checked = .TVShowThemeTvTunesCustom
            chkTVShowThemeTvTunesShowPath.Checked = .TVShowThemeTvTunesShowPath
            chkTVShowThemeTvTunesSub.Checked = .TVShowThemeTvTunesSub
            txtTVShowThemeTvTunesCustomPath.Text = .TVShowThemeTvTunesCustomPath
            txtTVShowThemeTvTunesSubDir.Text = .TVShowThemeTvTunesSubDir

            '****************** YAMJ settings ******************
            chkTVUseYAMJ.Checked = .TVUseYAMJ
            chkTVEpisodeNFOYAMJ.Checked = .TVEpisodeNFOYAMJ
            chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
            chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
            chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
            chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
            chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
            chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
            chkTVShowNFOYAMJ.Checked = .TVShowNFOYAMJ
            chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            chkTVUseBoxee.Checked = .TVUseBoxee
            chkTVEpisodeNFOBoxee.Checked = .TVEpisodeNFOBoxee
            chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
            chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
            chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
            chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
            chkTVShowNFOBoxee.Checked = .TVShowNFOBoxee
            chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

            '***************** Expert settings ******************
            chkTVUseExpert.Checked = .TVUseExpert

            '***************** Expert AllSeasons ****************
            txtTVAllSeasonsBannerExpert.Text = .TVAllSeasonsBannerExpert
            txtTVAllSeasonsFanartExpert.Text = .TVAllSeasonsFanartExpert
            txtTVAllSeasonsLandscapeExpert.Text = .TVAllSeasonsLandscapeExpert
            txtTVAllSeasonsPosterExpert.Text = .TVAllSeasonsPosterExpert

            '***************** Expert Episode *******************
            chkTVEpisodeActorthumbsExpert.Checked = .TVEpisodeActorThumbsExpert
            txtTVEpisodeActorThumbsExtExpert.Text = .TVEpisodeActorThumbsExtExpert
            txtTVEpisodeFanartExpert.Text = .TVEpisodeFanartExpert
            txtTVEpisodeNFOExpert.Text = .TVEpisodeNFOExpert
            txtTVEpisodePosterExpert.Text = .TVEpisodePosterExpert

            '***************** Expert Season *******************
            txtTVSeasonBannerExpert.Text = .TVSeasonBannerExpert
            txtTVSeasonFanartExpert.Text = .TVSeasonFanartExpert
            txtTVSeasonLandscapeExpert.Text = .TVSeasonLandscapeExpert
            txtTVSeasonPosterExpert.Text = .TVSeasonPosterExpert

            '***************** Expert Show *********************
            chkTVShowActorthumbsExpert.Checked = .TVShowActorThumbsExpert
            txtTVShowActorThumbsExtExpert.Text = .TVShowActorThumbsExtExpert
            txtTVShowBannerExpert.Text = .TVShowBannerExpert
            txtTVShowCharacterArtExpert.Text = .TVShowCharacterArtExpert
            txtTVShowClearArtExpert.Text = .TVShowClearArtExpert
            txtTVShowClearLogoExpert.Text = .TVShowClearLogoExpert
            chkTVShowExtrafanartsExpert.Checked = .TVShowExtrafanartsExpert
            txtTVShowFanartExpert.Text = .TVShowFanartExpert
            txtTVShowLandscapeExpert.Text = .TVShowLandscapeExpert
            txtTVShowNFOExpert.Text = .TVShowNFOExpert
            txtTVShowPosterExpert.Text = .TVShowPosterExpert
        End With
    End Sub

    Private Sub chkTVShowThemeTvTunesCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesCustom.CheckedChanged
        txtTVShowThemeTvTunesCustomPath.Enabled = chkTVShowThemeTvTunesCustom.Checked
        btnTVShowThemeTvTunesCustomPathBrowse.Enabled = chkTVShowThemeTvTunesCustom.Checked

        If chkTVShowThemeTvTunesCustom.Checked Then
            chkTVShowThemeTvTunesShowPath.Enabled = False
            chkTVShowThemeTvTunesShowPath.Checked = False
            chkTVShowThemeTvTunesSub.Enabled = False
            chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not chkTVShowThemeTvTunesCustom.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesShowPath.Enabled = True
            chkTVShowThemeTvTunesSub.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVShowThemeTvTunesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesEnabled.CheckedChanged
        chkTVShowThemeTvTunesCustom.Enabled = chkTVShowThemeTvTunesEnabled.Checked
        chkTVShowThemeTvTunesShowPath.Enabled = chkTVShowThemeTvTunesEnabled.Checked
        chkTVShowThemeTvTunesSub.Enabled = chkTVShowThemeTvTunesEnabled.Checked

        If Not chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesShowPath.Checked = False
            chkTVShowThemeTvTunesSub.Checked = False
        Else
            chkTVShowThemeTvTunesShowPath.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVShowThemeTvTunesShowPath_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesShowPath.CheckedChanged
        If chkTVShowThemeTvTunesShowPath.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = False
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesSub.Enabled = False
            chkTVShowThemeTvTunesSub.Checked = False
        End If

        If Not chkTVShowThemeTvTunesShowPath.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = True
            chkTVShowThemeTvTunesSub.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVShowThemeTvTunesSub_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesSub.CheckedChanged
        txtTVShowThemeTvTunesSubDir.Enabled = chkTVShowThemeTvTunesSub.Checked

        If chkTVShowThemeTvTunesSub.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = False
            chkTVShowThemeTvTunesCustom.Checked = False
            chkTVShowThemeTvTunesShowPath.Enabled = False
            chkTVShowThemeTvTunesShowPath.Checked = False
        End If

        If Not chkTVShowThemeTvTunesSub.Checked AndAlso chkTVShowThemeTvTunesEnabled.Checked Then
            chkTVShowThemeTvTunesCustom.Enabled = True
            chkTVShowThemeTvTunesShowPath.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub btnTVShowThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowThemeTvTunesCustomPathBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtTVShowThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseBoxee.CheckedChanged
        chkTVEpisodeNFOBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVEpisodePosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVSeasonPosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowBannerBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowFanartBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowNFOBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowPosterBoxee.Enabled = chkTVUseBoxee.Checked

        If Not chkTVUseBoxee.Checked Then
            chkTVEpisodeNFOBoxee.Checked = False
            chkTVEpisodePosterBoxee.Checked = False
            chkTVSeasonPosterBoxee.Checked = False
            chkTVShowBannerBoxee.Checked = False
            chkTVShowFanartBoxee.Checked = False
            chkTVShowNFOBoxee.Checked = False
            chkTVShowPosterBoxee.Checked = False
        Else
            chkTVEpisodeNFOBoxee.Checked = True
            chkTVEpisodePosterBoxee.Checked = True
            chkTVSeasonPosterBoxee.Checked = True
            chkTVShowBannerBoxee.Checked = True
            chkTVShowFanartBoxee.Checked = True
            chkTVShowNFOBoxee.Checked = True
            chkTVShowPosterBoxee.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVUseAD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseAD.CheckedChanged
        chkTVSeasonLandscapeAD.Enabled = chkTVUseAD.Checked
        chkTVShowCharacterArtAD.Enabled = chkTVUseAD.Checked
        chkTVShowClearArtAD.Enabled = chkTVUseAD.Checked
        chkTVShowClearLogoAD.Enabled = chkTVUseAD.Checked
        chkTVShowLandscapeAD.Enabled = chkTVUseAD.Checked

        If Not chkTVUseAD.Checked Then
            chkTVSeasonLandscapeAD.Checked = False
            chkTVShowCharacterArtAD.Checked = False
            chkTVShowClearArtAD.Checked = False
            chkTVShowClearLogoAD.Checked = False
            chkTVShowLandscapeAD.Checked = False
        Else
            chkTVSeasonLandscapeAD.Checked = True
            chkTVShowCharacterArtAD.Checked = True
            chkTVShowClearArtAD.Checked = True
            chkTVShowClearLogoAD.Checked = True
            chkTVShowLandscapeAD.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExtended.CheckedChanged
        chkTVSeasonLandscapeExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowCharacterArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearLogoExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowKeyArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowLandscapeExtended.Enabled = chkTVUseExtended.Checked

        If Not chkTVUseExtended.Checked Then
            chkTVSeasonLandscapeExtended.Checked = False
            chkTVShowCharacterArtExtended.Checked = False
            chkTVShowClearArtExtended.Checked = False
            chkTVShowClearLogoExtended.Checked = False
            chkTVShowKeyArtExtended.Checked = False
            chkTVShowLandscapeExtended.Checked = False
        Else
            chkTVSeasonLandscapeExtended.Checked = True
            chkTVShowCharacterArtExtended.Checked = True
            chkTVShowClearArtExtended.Checked = True
            chkTVShowClearLogoExtended.Checked = True
            chkTVShowKeyArtExtended.Checked = True
            chkTVShowLandscapeExtended.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseFrodo.CheckedChanged
        chkTVEpisodeActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVEpisodeNFOFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVEpisodePosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonPosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowExtrafanartsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowNFOFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowPosterFrodo.Enabled = chkTVUseFrodo.Checked

        If Not chkTVUseFrodo.Checked Then
            chkTVEpisodeActorThumbsFrodo.Checked = False
            chkTVEpisodeNFOFrodo.Checked = False
            chkTVEpisodePosterFrodo.Checked = False
            chkTVSeasonBannerFrodo.Checked = False
            chkTVSeasonFanartFrodo.Checked = False
            chkTVSeasonPosterFrodo.Checked = False
            chkTVShowActorThumbsFrodo.Checked = False
            chkTVShowBannerFrodo.Checked = False
            chkTVShowExtrafanartsFrodo.Checked = False
            chkTVShowFanartFrodo.Checked = False
            chkTVShowNFOFrodo.Checked = False
            chkTVShowPosterFrodo.Checked = False
        Else
            chkTVEpisodeActorThumbsFrodo.Checked = True
            chkTVEpisodeNFOFrodo.Checked = True
            chkTVEpisodePosterFrodo.Checked = True
            chkTVSeasonBannerFrodo.Checked = True
            chkTVSeasonFanartFrodo.Checked = True
            chkTVSeasonPosterFrodo.Checked = True
            chkTVShowActorThumbsFrodo.Checked = True
            chkTVShowBannerFrodo.Checked = True
            chkTVShowExtrafanartsFrodo.Checked = True
            chkTVShowFanartFrodo.Checked = True
            chkTVShowNFOFrodo.Checked = True
            chkTVShowPosterFrodo.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        chkTVEpisodeNFOYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVEpisodePosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonPosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowNFOYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowPosterYAMJ.Enabled = chkTVUseYAMJ.Checked

        If Not chkTVUseYAMJ.Checked Then
            chkTVEpisodeNFOYAMJ.Checked = False
            chkTVEpisodePosterYAMJ.Checked = False
            chkTVSeasonBannerYAMJ.Checked = False
            chkTVSeasonFanartYAMJ.Checked = False
            chkTVSeasonPosterYAMJ.Checked = False
            chkTVShowBannerYAMJ.Checked = False
            chkTVShowFanartYAMJ.Checked = False
            chkTVShowNFOYAMJ.Checked = False
            chkTVShowPosterYAMJ.Checked = False
        Else
            chkTVEpisodeNFOYAMJ.Checked = True
            chkTVEpisodePosterYAMJ.Checked = True
            chkTVSeasonBannerYAMJ.Checked = True
            chkTVSeasonFanartYAMJ.Checked = True
            chkTVSeasonPosterYAMJ.Checked = True
            chkTVShowBannerYAMJ.Checked = True
            chkTVShowFanartYAMJ.Checked = True
            chkTVShowNFOYAMJ.Checked = True
            chkTVShowPosterYAMJ.Checked = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkTVUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExpert.CheckedChanged
        chkTVEpisodeActorthumbsExpert.Enabled = chkTVUseExpert.Checked
        chkTVShowActorthumbsExpert.Enabled = chkTVUseExpert.Checked
        chkTVShowExtrafanartsExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVAllSeasonsPosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeActorThumbsExtExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodeNFOExpert.Enabled = chkTVUseExpert.Checked
        txtTVEpisodePosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVSeasonPosterExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowActorThumbsExtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowBannerExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowCharacterArtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowClearArtExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowClearLogoExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowFanartExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowNFOExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowPosterExpert.Enabled = chkTVUseExpert.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub pbTVSourcesADInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader#Filenaming")
    End Sub

    Private Sub pbTVSourcesADInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesADInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesADInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_Click(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbTVSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbTVSourcesTvTunesInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

#End Region 'Methods

End Class