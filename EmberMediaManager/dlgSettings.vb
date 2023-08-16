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
' #
' # Dialog size: 1400;1000
' # Move the panels (pnl*) from 0;1000 to 0;0 to edit. Move it back after editing.

Imports EmberAPI
Imports NLog
Imports System.IO
Imports System.Net

Public Class dlgSettings

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AllSettingsPanels As New List(Of Interfaces.ISettingsPanel)
    Private _CurrentButton As New ButtonTag
    Private _CurrentPanel As New Panel
    Private _DidApply As Boolean = False
    Private _NoUpdate As Boolean = True
    Private _Results As New Structures.SettingsResult

    Private dHelp As New Dictionary(Of String, String)
    Private MovieMeta As New List(Of Settings.MetadataPerType)
    Private MovieGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private MovieSetGeneralMediaListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralEpisodeListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralSeasonListSorting As New List(Of Settings.ListSorting)
    Private TVGeneralShowListSorting As New List(Of Settings.ListSorting)
    Private TVShowMatching As New List(Of Settings.regexp)
    'Private tLangList As New List(Of Containers.TVLanguage)
    Private Temp_FileSystemValidExts As ExtendedListOfString
    Private Temp_FileSystemValidSubtitlesExts As ExtendedListOfString
    Private Temp_FileSystemValidThemeExts As ExtendedListOfString
    Private Temp_MovieFilterCustom As ExtendedListOfString
    Private Temp_TVEpisodeFilterCustom As ExtendedListOfString
    Private Temp_TVScraperSeasonTitleBlacklist As ExtendedListOfString
    Private Temp_TVShowFilterCustom As ExtendedListOfString
    Private TVMeta As New List(Of Settings.MetadataPerType)

#End Region 'Fields

#Region "Events"

    Public Event LoadEnd()

#End Region 'Events

#Region "Dialog Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SettingsPanels_Remove_AllPanels()
    End Sub

    Private Sub Dialog_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Dim iBackground As New Bitmap(pnlSettingsTop.Width, pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(pnlSettingsCurrent.Width, pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        If tsSettingsTopMenu.Items.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'calculate the buttons width and count
            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

            'formula to calculate the count of spaces to reach the label.width
            'spaces (x) to width (y) in px: 1 = 10, 2 = 13, 3 = 16, 4 = 19, 5 = 22
            'x = 10 + ((y - 1) * 3) or
            'y = (x - 10) / 3 + 1
            sLength = Convert.ToInt32((sRest - 10) / 3 + 1)

            If Not sRest < 10 Then
                sSpacer = New String(Convert.ToChar(" "), sLength)
            Else
                sSpacer = New String(Convert.ToChar(" "), 1)
            End If

            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
            Next
        End If
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub Setup()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkMovieActorThumbsExpertBDMV.Text = strActorThumbs
        chkMovieActorThumbsExpertMulti.Text = strActorThumbs
        chkMovieActorThumbsExpertSingle.Text = strActorThumbs
        chkMovieActorThumbsExpertVTS.Text = strActorThumbs
        chkTVEpisodeActorThumbsExpert.Text = strActorThumbs
        chkTVShowActorThumbsExpert.Text = strActorThumbs
        lblMovieSourcesFilenamingKodiDefaultsActorThumbs.Text = strActorThumbs
        lblTVSourcesFilenamingKodiDefaultsActorThumbs.Text = strActorThumbs

        'Actors
        Dim strActors As String = Master.eLang.GetString(231, "Actors")
        lblMovieScraperGlobalActors.Text = strActors
        lblTVScraperGlobalActors.Text = strActors

        'Add Episode Guest Stars to Actors list
        Dim strAddEPGuestStars As String = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperEpisodeGuestStarsToActors.Text = strAddEPGuestStars

        'Add <displayseason> and <displayepisode> to special episodes
        Dim strAddDisplaySE As String = Master.eLang.GetString(976, "Add <displayseason> and <displayepisode> to special episodes")
        chkTVScraperUseDisplaySeasonEpisode.Text = strAddDisplaySE

        'Aired
        Dim strAired As String = Master.eLang.GetString(728, "Aired")
        lblTVScraperGlobalAired.Text = strAired

        'All Seasons
        Dim strAllSeasons As String = Master.eLang.GetString(1202, "All Seasons")
        tpTVImagesAllSeasons.Text = strAllSeasons
        tpTVSourcesFilenamingExpertAllSeasons.Text = strAllSeasons

        'Also Get Blank Images
        Dim strAlsoGetBlankImages As String = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkMovieSetImagesGetBlankImages.Text = strAlsoGetBlankImages
        chkTVImagesGetBlankImages.Text = strAlsoGetBlankImages

        'Also Get English Images
        Dim strAlsoGetEnglishImages As String = Master.eLang.GetString(737, "Also Get English Images")
        chkMovieSetImagesGetEnglishImages.Text = strAlsoGetEnglishImages
        chkTVImagesGetEnglishImages.Text = strAlsoGetEnglishImages

        'Ask On Click Scrape
        Dim strAskOnClickScrape As String = Master.eLang.GetString(852, "Ask On Click Scrape")
        chkMovieClickScrapeAsk.Text = strAskOnClickScrape
        chkMovieSetClickScrapeAsk.Text = strAskOnClickScrape
        chkTVGeneralClickScrapeAsk.Text = strAskOnClickScrape

        'Automatically Resize:
        Dim strAutomaticallyResize As String = Master.eLang.GetString(481, "Automatically Resize:")
        chkMovieSetBannerResize.Text = strAutomaticallyResize
        chkMovieSetFanartResize.Text = strAutomaticallyResize
        chkMovieSetKeyartResize.Text = strAutomaticallyResize
        chkMovieSetPosterResize.Text = strAutomaticallyResize
        chkTVAllSeasonsBannerResize.Text = strAutomaticallyResize
        chkTVAllSeasonsFanartResize.Text = strAutomaticallyResize
        chkTVAllSeasonsPosterResize.Text = strAutomaticallyResize
        chkTVEpisodeFanartResize.Text = strAutomaticallyResize
        chkTVEpisodePosterResize.Text = strAutomaticallyResize
        chkTVSeasonBannerResize.Text = strAutomaticallyResize
        chkTVSeasonFanartResize.Text = strAutomaticallyResize
        chkTVSeasonPosterResize.Text = strAutomaticallyResize
        chkTVShowBannerResize.Text = strAutomaticallyResize
        chkTVShowExtrafanartsResize.Text = strAutomaticallyResize
        chkTVShowFanartResize.Text = strAutomaticallyResize
        chkTVShowKeyartResize.Text = strAutomaticallyResize
        chkTVShowPosterResize.Text = strAutomaticallyResize

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        gbMovieSetImagesBannerOpts.Text = strBanner
        gbTVImagesAllSeasonsBannerOpts.Text = strBanner
        gbTVImagesSeasonBannerOpts.Text = strBanner
        gbTVImagesShowBannerOpts.Text = strBanner
        lblMovieSetSourcesFilenamingExpertSingleBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiExtendedBanner.Text = strBanner
        lblMovieSetSourcesFilenamingKodiMSAABanner.Text = strBanner
        lblMovieSourcesFilenamingExpertBDMVBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertMultiBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertSingleBanner.Text = strBanner
        lblMovieSourcesFilenamingExpertVTSBanner.Text = strBanner
        lblMovieSourcesFilenamingKodiADBanner.Text = strBanner
        lblMovieSourcesFilenamingKodiExtendedBanner.Text = strBanner
        lblMovieSourcesFilenamingNMTDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingExpertAllSeasonsBanner.Text = strBanner
        lblTVSourcesFilenamingExpertSeasonBanner.Text = strBanner
        lblTVSourcesFilenamingExpertShowBanner.Text = strBanner
        lblTVSourcesFilenamingBoxeeDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingKodiDefaultsBanner.Text = strBanner
        lblTVSourcesFilenamingNMTDefaultsBanner.Text = strBanner

        'Certifications
        Dim strCertifications As String = Master.eLang.GetString(56, "Certifications")
        gbMovieScraperCertificationOpts.Text = strCertifications
        gbTVScraperCertificationOpts.Text = strCertifications
        lblMovieScraperGlobalCertifications.Text = strCertifications
        lblTVScraperGlobalCertifications.Text = strCertifications

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        gbTVImagesShowCharacterArtOpts.Text = strCharacterArt
        lblTVShowCharacterArtExpert.Text = strCharacterArt
        lblTVSourcesFilenamingKodiADCharacterArt.Text = strCharacterArt
        lblTVSourcesFilenamingKodiExtendedCharacterArt.Text = strCharacterArt

        'Cleanup disabled fields
        Dim strCleanUpDisabledFileds As String = Master.eLang.GetString(125, "Cleanup disabled fields")
        chkMovieScraperCleanFields.Text = strCleanUpDisabledFileds
        chkTVScraperCleanFields.Text = strCleanUpDisabledFileds

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        gbMovieSetImagesClearArtOpts.Text = strClearArt
        gbTVImagesShowClearArtOpts.Text = strClearArt
        lblMovieSetSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiExtendedClearArt.Text = strClearArt
        lblMovieSetSourcesFilenamingKodiMSAAClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertBDMVClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertMultiClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertSingleClearArt.Text = strClearArt
        lblMovieSourcesFilenamingExpertVTSClearArt.Text = strClearArt
        lblMovieSourcesFileNamingKodiADClearArt.Text = strClearArt
        lblMovieSourcesFileNamingKodiExtendedClearArt.Text = strClearArt
        lblTVSourcesFilenamingExpertClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiADClearArt.Text = strClearArt
        lblTVSourcesFileNamingKodiExtendedClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        gbMovieSetImagesClearLogoOpts.Text = strClearLogo
        gbTVImagesShowClearLogoOpts.Text = strClearLogo
        lblMovieSetSourcesClearLogoExpertSingle.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        lblMovieSetSourcesFilenamingKodiMSAAClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertBDMVClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertMultiClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertSingleClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingExpertVTSClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingKodiADClearLogo.Text = strClearLogo
        lblMovieSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingExpertClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiADClearLogo.Text = strClearLogo
        lblTVSourcesFilenamingKodiExtendedClearLogo.Text = strClearLogo

        'Collection ID
        Dim strCollectionID As String = Master.eLang.GetString(1135, "Collection ID")
        lblMovieScraperGlobalCollectionID.Text = strCollectionID

        'Collections
        Dim strCollections As String = Master.eLang.GetString(424, "Collections")
        lblMovieScraperGlobalCollections.Text = strCollections

        'Column
        Dim strColumn As String = Master.eLang.GetString(1331, "Column")
        colMovieGeneralMediaListSortingLabel.Text = strColumn
        colMovieSetGeneralMediaListSortingLabel.Text = strColumn
        colTVGeneralEpisodeListSortingLabel.Text = strColumn
        colTVGeneralSeasonListSortingLabel.Text = strColumn
        colTVGeneralShowListSortingLabel.Text = strColumn

        'Countries
        Dim strCountries As String = Master.eLang.GetString(237, "Countries")
        lblMovieScraperGlobalCountries.Text = strCountries
        lblTVScraperGlobalCountries.Text = strCountries

        'Creators
        Dim strCreators As String = Master.eLang.GetString(744, "Creators")
        lblTVScraperGlobalCreators.Text = strCreators

        'Default Episode Ordering
        Dim strDefaultEpisodeOrdering As String = Master.eLang.GetString(797, "Default Episode Ordering")
        lblTVSourcesDefaultsOrdering.Text = String.Concat(strDefaultEpisodeOrdering, ":")

        'Default Language
        Dim strDefaultLanguage As String = Master.eLang.GetString(1166, "Default Language")
        lblMovieSourcesDefaultsLanguage.Text = String.Concat(strDefaultLanguage, ":")
        lblTVSourcesDefaultsLanguage.Text = String.Concat(strDefaultLanguage, ":")

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        gbMovieSourcesFilenamingBoxeeDefaultsOpts.Text = strDefaults
        gbMovieSourcesFilenamingNMTDefaultsOpts.Text = strDefaults
        gbMovieSourcesFilenamingKodiDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingBoxeeDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingNMTDefaultsOpts.Text = strDefaults
        gbTVSourcesFilenamingKodiDefaultsOpts.Text = strDefaults

        'Defaults for new Sources
        Dim strDefaultsForNewSources As String = Master.eLang.GetString(252, "Defaults for new Sources")
        gbMovieSourcesDefaultsOpts.Text = strDefaultsForNewSources
        gbTVSourcesDefaultsOpts.Text = strDefaultsForNewSources


        'Defaults by File Type
        Dim strDefaultsByFileType As String = Master.eLang.GetString(625, "Defaults by File Type")
        gbMovieScraperDefFIExtOpts.Text = strDefaultsByFileType
        gbTVScraperDefFIExtOpts.Text = strDefaultsByFileType

        'Directors
        Dim strDirectors As String = Master.eLang.GetString(940, "Directors")
        lblMovieScraperGlobalDirectors.Text = strDirectors
        lblTVScraperGlobalDirectors.Text = strDirectors

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        gbMovieSetImagesDiscArtOpts.Text = strDiscArt
        lblMovieSetSourcesFilenamingExpertSingleDiscArt.Text = strDiscArt
        lblMovieSetSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertBDMVDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertMultiDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertSingleDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingExpertVTSDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingKodiADDiscArt.Text = strDiscArt
        lblMovieSourcesFilenamingKodiExtendedDiscArt.Text = strDiscArt

        'Display best Audio Stream with the following Language
        Dim strDisplayLanguageBestAudio As String = String.Concat(Master.eLang.GetString(436, "Display best Audio Stream with the following Language"), ":")
        lblMovieLanguageOverlay.Text = strDisplayLanguageBestAudio
        lblTVLanguageOverlay.Text = strDisplayLanguageBestAudio

        'Display "Image Select" dialog while single scraping
        Dim strDisplayImgDialog As String = Master.eLang.GetString(499, "Display ""Image Select"" dialog while single scraping")
        chkMovieSetImagesDisplayImageSelect.Text = strDisplayImgDialog
        chkTVImagesDisplayImageSelect.Text = strDisplayImgDialog

        'Do not save URLs to NFO"
        Dim strNotSaveURLToNfo As String = Master.eLang.GetString(498, "Do not save URLs to NFO")
        chkTVImagesNotSaveURLToNfo.Text = strNotSaveURLToNfo

        'Duration Format
        Dim strDurationFormat As String = Master.eLang.GetString(515, "Duration Format")
        gbMovieScraperDurationFormatOpts.Text = strDurationFormat
        gbTVScraperDurationFormatOpts.Text = strDurationFormat

        'Duration Runtime Format
        Dim strDurationRuntimeFormat As String = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        lblMovieScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat
        lblTVScraperDurationRuntimeFormat.Text = strDurationRuntimeFormat

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblMovieSetSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblMovieSetSourcesFilenamingKodiMSAAEnabled.Text = strEnabled
        lblMovieSourcesFilenamingBoxeeDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiADEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblMovieSourcesFilenamingNMTDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingBoxeeDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiADEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiDefaultsEnabled.Text = strEnabled
        lblTVSourcesFilenamingKodiExtendedEnabled.Text = strEnabled
        lblTVSourcesFilenamingNMTDefaultsEnabled.Text = strEnabled
        chkMovieUseExpert.Text = strEnabled
        chkMovieSetUseExpert.Text = strEnabled
        chkMovieThemeTvTunesEnabled.Text = strEnabled
        chkTVShowThemeTvTunesEnabled.Text = strEnabled
        chkTVUseExpert.Text = strEnabled

        'Enabled Click Scrape
        Dim strEnabledClickScrape As String = Master.eLang.GetString(849, "Enable Click Scrape")
        chkMovieClickScrape.Text = strEnabledClickScrape
        chkMovieSetClickScrape.Text = strEnabledClickScrape
        chkTVGeneralClickScrape.Text = strEnabledClickScrape

        'Enable Image Caching
        Dim strEnableImageCaching As String = Master.eLang.GetString(249, "Enable Image Caching")
        chkMovieSetImagesCacheEnabled.Text = strEnableImageCaching
        chkTVImagesCacheEnabled.Text = strEnableImageCaching

        'Enable Theme Support
        Dim strEnableThemeSupport As String = Master.eLang.GetString(1082, "Enable Theme Support")

        'Episode
        Dim strEpisode As String = Master.eLang.GetString(727, "Episode")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeEpisode.Text = strEpisode
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJEpisode.Text = strEpisode
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJEpisode.Text = strEpisode
        tpTVImagesEpisode.Text = strEpisode
        tpTVSourcesFilenamingExpertEpisode.Text = strEpisode

        'Episode #
        Dim strEpisodeNR As String = Master.eLang.GetString(660, "Episode #")

        'Episode List Sorting
        Dim strEpisodeListSorting As String = Master.eLang.GetString(494, "Episode List Sorting")
        gbTVGeneralEpisodeListSorting.Text = strEpisodeListSorting

        'Episode Guide URL
        Dim strEpisodeGuideURL As String = Master.eLang.GetString(723, "Episode Guide URL")
        lblTVScraperGlobalEpisodeGuideURL.Text = strEpisodeGuideURL

        'Episodes
        Dim strEpisodes As String = Master.eLang.GetString(682, "Episodes")
        lblTVScraperGlobalHeaderEpisodes.Text = strEpisodes

        'Exclude
        Dim strExclude As String = Master.eLang.GetString(264, "Exclude")
        colMovieSourcesExclude.Text = strExclude
        colTVSourcesExclude.Text = strExclude

        'Expert
        Dim strExpert As String = Master.eLang.GetString(439, "Expert")
        tpMovieSourcesFilenamingExpert.Text = strExpert
        tpMovieSetSourcesFilenamingExpert.Text = strExpert
        tpTVSourcesFilenamingExpert.Text = strExpert

        'Expert Settings
        Dim strExpertSettings As String = Master.eLang.GetString(1181, "Expert Settings")
        gbMovieSourcesFilenamingExpertOpts.Text = strExpertSettings
        gbMovieSetSourcesFilenamingExpertOpts.Text = strExpertSettings
        gbTVSourcesFilenamingExpertOpts.Text = strExpertSettings

        'Extended Images
        Dim strExtendedImages As String = Master.eLang.GetString(822, "Extended Images")
        gbMovieSourcesFilenamingKodiExtendedOpts.Text = strExtendedImages
        gbMovieSetSourcesFilenamingKodiExtendedOpts.Text = strExtendedImages
        gbTVSourcesFilenamingKodiExtendedOpts.Text = strExtendedImages

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        chkTVShowExtrafanartsExpert.Text = strExtrafanarts
        gbTVImagesShowExtrafanartsOpts.Text = strExtrafanarts
        lblMovieSourcesFilenamingKodiDefaultsExtrafanarts.Text = strExtrafanarts
        lblTVSourcesFilenamingKodiDefaultsExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        lblMovieSourcesFilenamingKodiDefaultsExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        gbMovieSetImagesFanartOpts.Text = strFanart
        gbTVImagesAllSeasonsFanartOpts.Text = strFanart
        gbTVImagesEpisodeFanartOpts.Text = strFanart
        gbTVImagesSeasonFanartOpts.Text = strFanart
        gbTVImagesShowFanartOpts.Text = strFanart
        lblMovieSetSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiExtendedFanart.Text = strFanart
        lblMovieSetSourcesFilenamingKodiMSAAFanart.Text = strFanart
        lblMovieSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        lblMovieSourcesFilenamingNMTDefaultsFanart.Text = strFanart
        lblMovieSourcesFilenamingKodiDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingExpertAllSeasonsFanart.Text = strFanart
        lblTVSourcesFilenamingExpertEpisodeFanart.Text = strFanart
        lblTVSourcesFilenamingExpertSeasonFanart.Text = strFanart
        lblTVSourcesFilenamingExpertShowFanart.Text = strFanart
        lblTVSourcesFilenamingBoxeeDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingKodiDefaultsFanart.Text = strFanart
        lblTVSourcesFilenamingNMTDefaultsFanart.Text = strFanart

        'File Naming
        Dim strFilenaming As String = Master.eLang.GetString(471, "File Naming")
        gbMovieSourcesFilenamingOpts.Text = strFilenaming
        gbMovieSetSourcesFilenamingOpts.Text = strFilenaming
        gbTVSourcesFilenamingOpts.Text = strFilenaming

        'File Type
        Dim strFileType As String = String.Concat(Master.eLang.GetString(626, "File Type"), ":")
        lblMovieScraperDefFIExt.Text = strFileType
        lblTVScraperDefFIExt.Text = strFileType

        'Force Language
        Dim strForceLanguage As String = Master.eLang.GetString(1034, "Force Language")
        chkMovieSetImagesForceLanguage.Text = strForceLanguage
        chkTVImagesForceLanguage.Text = strForceLanguage

        'Genres
        Dim strGenres As String = Master.eLang.GetString(725, "Genres")
        lblMovieScraperGlobalGenres.Text = strGenres
        lblTVScraperGlobalGenres.Text = strGenres

        'Get Year
        Dim strGetYear As String = Master.eLang.GetString(586, "Get Year")
        colMovieSourcesGetYear.Text = strGetYear

        'Hide
        Dim strHide As String = Master.eLang.GetString(465, "Hide")
        colMovieGeneralMediaListSortingHide.Text = strHide
        colMovieSetGeneralMediaListSortingHide.Text = strHide
        colTVGeneralEpisodeListSortingHide.Text = strHide
        colTVGeneralSeasonListSortingHide.Text = strHide
        colTVGeneralShowListSortingHide.Text = strHide

        'Images
        Dim strImages As String = Master.eLang.GetString(497, "Images")
        gbMovieSetImagesOpts.Text = strImages
        gbTVImagesOpts.Text = strImages

        'Keep existing
        Dim strKeepExisting As String = Master.eLang.GetString(971, "Keep existing")
        chkMovieSetBannerKeepExisting.Text = strKeepExisting
        chkMovieSetClearArtKeepExisting.Text = strKeepExisting
        chkMovieSetClearLogoKeepExisting.Text = strKeepExisting
        chkMovieSetDiscArtKeepExisting.Text = strKeepExisting
        chkMovieSetFanartKeepExisting.Text = strKeepExisting
        chkMovieSetKeyartKeepExisting.Text = strKeepExisting
        chkMovieSetLandscapeKeepExisting.Text = strKeepExisting
        chkMovieSetPosterKeepExisting.Text = strKeepExisting
        chkMovieThemeKeepExisting.Text = strKeepExisting
        chkMovieTrailerKeepExisting.Text = strKeepExisting
        chkTVAllSeasonsBannerKeepExisting.Text = strKeepExisting
        chkTVAllSeasonsFanartKeepExisting.Text = strKeepExisting
        chkTVAllSeasonsLandscapeKeepExisting.Text = strKeepExisting
        chkTVAllSeasonsPosterKeepExisting.Text = strKeepExisting
        chkTVEpisodeFanartKeepExisting.Text = strKeepExisting
        chkTVEpisodePosterKeepExisting.Text = strKeepExisting
        chkTVSeasonBannerKeepExisting.Text = strKeepExisting
        chkTVSeasonFanartKeepExisting.Text = strKeepExisting
        chkTVSeasonLandscapeKeepExisting.Text = strKeepExisting
        chkTVSeasonPosterKeepExisting.Text = strKeepExisting
        chkTVShowBannerKeepExisting.Text = strKeepExisting
        chkTVShowCharacterArtKeepExisting.Text = strKeepExisting
        chkTVShowClearArtKeepExisting.Text = strKeepExisting
        chkTVShowClearLogoKeepExisting.Text = strKeepExisting
        chkTVShowExtrafanartsKeepExisting.Text = strKeepExisting
        chkTVShowFanartKeepExisting.Text = strKeepExisting
        chkTVShowLandscapeKeepExisting.Text = strKeepExisting
        chkTVShowKeyartKeepExisting.Text = strKeepExisting
        chkTVShowPosterKeepExisting.Text = strKeepExisting
        chkTVShowThemeKeepExisting.Text = strKeepExisting

        'Keyart
        Dim strKeyart As String = Master.eLang.GetString(1237, "Keyart")
        gbMovieSetImagesKeyartOpts.Text = strKeyart
        gbTVImagesShowKeyartOpts.Text = strKeyart
        lblMovieSourcesFilenamingExpertBDMVKeyart.Text = strKeyart
        lblMovieSourcesFilenamingExpertMultiKeyart.Text = strKeyart
        lblMovieSourcesFilenamingExpertSingleKeyart.Text = strKeyart
        lblMovieSourcesFilenamingExpertVTSKeyart.Text = strKeyart
        lblMovieSourcesFilenamingKodiExtendedKeyart.Text = strKeyart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        gbMovieSetImagesLandscapeOpts.Text = strLandscape
        gbTVImagesAllSeasonsLandscapeOpts.Text = strLandscape
        gbTVImagesSeasonLandscapeOpts.Text = strLandscape
        gbTVImagesShowLandscapeOpts.Text = strLandscape
        lblMovieSetLandscapeExpertSingle.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        lblMovieSetSourcesFilenamingKodiMSAALandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertBDMVLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertMultiLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertSingleLandscape.Text = strLandscape
        lblMovieSourcesFilenamingExpertVTSLandscape.Text = strLandscape
        lblMovieSourcesFilenamingKodiADLandscape.Text = strLandscape
        lblMovieSourcesFilenamingKodiExtendedLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertAllSeasonsLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertSeasonLandscape.Text = strLandscape
        lblTVSourcesFilenamingExpertShowLandscape.Text = strLandscape

        'Language
        Dim strLanguage As String = Master.eLang.GetString(610, "Language")
        colMovieSourcesLanguage.Text = strLanguage
        colTVSourcesLanguage.Text = strLanguage

        'Language (Audio)
        Dim strLanguageAudio As String = Master.eLang.GetString(431, "Language (Audio)")
        lblMovieScraperGlobalLanguageA.Text = strLanguageAudio
        lblTVScraperGlobalLanguageA.Text = strLanguageAudio

        'Language (Video)
        Dim strLanguageVideo As String = Master.eLang.GetString(435, "Language (Video)")
        lblMovieScraperGlobalLanguageV.Text = strLanguageVideo
        lblTVScraperGlobalLanguageV.Text = strLanguageVideo

        'Limit
        Dim strLimit As String = Master.eLang.GetString(578, "Limit")
        lblMovieScraperGlobalHeaderLimit.Text = strLimit
        lblMovieScraperOutlineLimit.Text = String.Concat(strLimit, ":")
        lblTVScraperGlobalHeaderEpisodesLimit.Text = strLimit
        lblTVScraperGlobalHeaderSeasonsLimit.Text = strLimit
        lblTVScraperGlobalHeaderShowsLimit.Text = strLimit
        lblTVShowExtrafanartsLimit.Text = String.Concat(strLimit, ":")

        'Lock
        Dim strLock As String = Master.eLang.GetString(24, "Lock")
        lblMovieScraperGlobalHeaderLock.Text = strLock
        lblMovieSetScraperGlobalHeaderLock.Text = strLock
        lblTVScraperGlobalHeaderEpisodesLock.Text = strLock
        lblTVScraperGlobalHeaderSeasonsLock.Text = strLock
        lblTVScraperGlobalHeaderShowsLock.Text = strLock

        'Main Window
        Dim strMainWindow As String = Master.eLang.GetString(1152, "Main Window")
        gbGeneralMainWindowOpts.Text = strMainWindow
        gbMovieGeneralMainWindowOpts.Text = strMainWindow
        gbTVGeneralMainWindowOpts.Text = strMainWindow

        'Max Height:
        Dim strMaxHeight As String = Master.eLang.GetString(480, "Max Height:")
        lblMovieSetBannerHeight.Text = strMaxHeight
        lblMovieSetFanartHeight.Text = strMaxHeight
        lblMovieSetKeyartHeight.Text = strMaxHeight
        lblMovieSetPosterHeight.Text = strMaxHeight
        lblTVAllSeasonsBannerHeight.Text = strMaxHeight
        lblTVAllSeasonsFanartHeight.Text = strMaxHeight
        lblTVAllSeasonsPosterHeight.Text = strMaxHeight
        lblTVEpisodeFanartHeight.Text = strMaxHeight
        lblTVEpisodePosterHeight.Text = strMaxHeight
        lblTVSeasonBannerHeight.Text = strMaxHeight
        lblTVSeasonFanartHeight.Text = strMaxHeight
        lblTVSeasonPosterHeight.Text = strMaxHeight
        lblTVShowBannerHeight.Text = strMaxHeight
        lblTVShowExtrafanartsHeight.Text = strMaxHeight
        lblTVShowFanartHeight.Text = strMaxHeight
        lblTVShowKeyartHeight.Text = strMaxHeight
        lblTVShowPosterHeight.Text = strMaxHeight

        'Max Height:
        Dim strMaxWidth As String = Master.eLang.GetString(479, "Max Width:")
        lblMovieSetBannerWidth.Text = strMaxWidth
        lblMovieSetFanartWidth.Text = strMaxWidth
        lblMovieSetKeyartWidth.Text = strMaxWidth
        lblMovieSetPosterWidth.Text = strMaxWidth
        lblTVAllSeasonsBannerWidth.Text = strMaxWidth
        lblTVAllSeasonsFanartWidth.Text = strMaxWidth
        lblTVAllSeasonsPosterWidth.Text = strMaxWidth
        lblTVEpisodeFanartWidth.Text = strMaxWidth
        lblTVEpisodePosterWidth.Text = strMaxWidth
        lblTVSeasonBannerWidth.Text = strMaxWidth
        lblTVSeasonFanartWidth.Text = strMaxWidth
        lblTVSeasonPosterWidth.Text = strMaxWidth
        lblTVShowBannerWidth.Text = strMaxWidth
        lblTVShowExtrafanartsWidth.Text = strMaxWidth
        lblTVShowFanartWidth.Text = strMaxWidth
        lblTVShowKeyartWidth.Text = strMaxWidth
        lblTVShowPosterWidth.Text = strMaxWidth

        'Meta Data
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
        gbMovieScraperMetaDataOpts.Text = strMetaData
        gbTVScraperMetaDataOpts.Text = strMetaData

        'Miscellaneous
        Dim strMiscellaneous As String = Master.eLang.GetString(429, "Miscellaneous")
        gbGeneralMiscOpts.Text = strMiscellaneous
        gbMovieGeneralMiscOpts.Text = strMiscellaneous
        gbMovieScraperMiscOpts.Text = strMiscellaneous
        gbMovieSetGeneralMiscOpts.Text = strMiscellaneous
        gbMovieSetSourcesMiscOpts.Text = strMiscellaneous
        gbMovieSourcesMiscOpts.Text = strMiscellaneous
        gbTVGeneralMiscOpts.Text = strMiscellaneous
        gbTVScraperMiscOpts.Text = strMiscellaneous
        gbTVSourcesMiscOpts.Text = strMiscellaneous

        'Missing
        Dim strMissing As String = Master.eLang.GetString(582, "Missing")

        'MPAA
        Dim strMPAA As String = Master.eLang.GetString(401, "MPAA")
        lblMovieScraperGlobalMPAA.Text = strMPAA
        lblTVScraperGlobalMPAA.Text = strMPAA

        'MPAA value if no rating is available
        Dim strMPAANotRated As String = Master.eLang.GetString(832, "MPAA value if no rating is available")
        lblTVScraperShowMPAANotRated.Text = strMPAANotRated

        'Movie List Sorting
        Dim strMovieListSorting As String = Master.eLang.GetString(490, "Movie List Sorting")
        gbMovieGeneralMediaListSorting.Text = strMovieListSorting

        'MovieSet List Sorting
        Dim strMovieSetListSorting As String = Master.eLang.GetString(491, "MovieSet List Sorting")
        gbMovieSetGeneralMediaListSorting.Text = strMovieSetListSorting

        'Name
        Dim strName As String = Master.eLang.GetString(232, "Name")
        colMovieSourcesName.Text = strName
        colTVSourcesName.Text = strName

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        lblMovieSetSourcesFilenamingExpertSingleNFO.Text = strNFO
        lblMovieSourcesFilenamingBoxeeDefaultsNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertBDMVNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertMultiNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertSingleNFO.Text = strNFO
        lblMovieSourcesFilenamingExpertVTSNFO.Text = strNFO
        lblMovieSourcesFilenamingKodiDefaultsNFO.Text = strNFO
        lblMovieSourcesFilenamingNMTDefaultsNFO.Text = strNFO
        lblTVSourcesFilenamingExpertEpisodeNFO.Text = strNFO
        lblTVSourcesFilenamingExpertShowNFO.Text = strNFO
        lblTVSourcesFilenamingKodiDefaultsNFO.Text = strNFO

        'Only
        Dim strOnly As String = Master.eLang.GetString(145, "Only")
        chkMovieSetBannerPrefOnly.Text = strOnly
        chkMovieSetFanartPrefOnly.Text = strOnly
        chkMovieSetKeyartPrefOnly.Text = strOnly
        chkMovieSetPosterPrefOnly.Text = strOnly
        chkTVAllSeasonsBannerPrefSizeOnly.Text = strOnly
        chkTVAllSeasonsFanartPrefSizeOnly.Text = strOnly
        chkTVAllSeasonsPosterPrefSizeOnly.Text = strOnly
        chkTVEpisodeFanartPrefSizeOnly.Text = strOnly
        chkTVEpisodePosterPrefSizeOnly.Text = strOnly
        chkTVSeasonBannerPrefSizeOnly.Text = strOnly
        chkTVSeasonFanartPrefSizeOnly.Text = strOnly
        chkTVSeasonPosterPrefSizeOnly.Text = strOnly
        chkTVShowBannerPrefSizeOnly.Text = strOnly
        chkTVShowExtrafanartsPrefSizeOnly.Text = strOnly
        chkTVShowFanartPrefSizeOnly.Text = strOnly
        chkTVShowKeyartPrefSizeOnly.Text = strOnly
        chkTVShowPosterPrefSizeOnly.Text = strOnly

        'Only Get Images for the Media Language
        Dim strOnlyImgMediaLang As String = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        chkMovieSetImagesMediaLanguageOnly.Text = strOnlyImgMediaLang
        chkTVImagesMediaLanguageOnly.Text = strOnlyImgMediaLang

        'Only if no MPAA is found
        Dim strOnlyIfNoMPAA As String = Master.eLang.GetString(1293, "Only if no MPAA is found")
        chkMovieScraperCertForMPAAFallback.Text = strOnlyIfNoMPAA
        chkTVScraperShowCertForMPAAFallback.Text = strOnlyIfNoMPAA

        'Only Save the Value to NFO
        Dim strOnlySaveValueToNFO As String = Master.eLang.GetString(835, "Only Save the Value to NFO")
        chkMovieScraperCertOnlyValue.Text = strOnlySaveValueToNFO
        chkTVScraperShowCertOnlyValue.Text = strOnlySaveValueToNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        gbMovieSourcesFilenamingExpertBDMVImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertMultiImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertSingleImagesOpts.Text = strOptionalImages
        gbMovieSourcesFilenamingExpertVTSImagesOpts.Text = strOptionalImages
        gbTVSourcesFilenamingExpertEpisodeImagesOpts.Text = strOptionalImages
        gbTVSourcesFilenamingExpertShowImagesOpts.Text = strOptionalImages

        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        gbMovieSourcesFilenamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertMultiOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertSingleOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingExpertVTSOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingKodiOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFilenamingNMTOptionalOpts.Text = strOptionalSettings

        'Ordering
        Dim strOrdering As String = Master.eLang.GetString(1167, "Ordering")
        colTVSourcesOrdering.Text = strOrdering

        'Original Title
        Dim strOriginalTitle As String = Master.eLang.GetString(302, "Original Title")
        lblMovieScraperGlobalOriginalTitle.Text = strOriginalTitle

        'Part of a MovieSet
        Dim strPartOfAMovieSet As String = Master.eLang.GetString(1295, "Part of a MovieSet")

        'Path
        Dim strPath As String = Master.eLang.GetString(410, "Path")
        lblMovieSetPathExpertSingle.Text = strPath
        lblMovieSetSourcesFilenamingKodiExtendedPath.Text = strPath
        lblMovieSetSourcesFilenamingKodiMSAAPath.Text = strPath
        colMovieSourcesPath.Text = strPath
        colTVSourcesPath.Text = strPath

        'Plot
        Dim strPlot As String = Master.eLang.GetString(65, "Plot")
        lblMovieScraperGlobalPlot.Text = strPlot
        lblMovieSetScraperGlobalPlot.Text = strPlot
        lblTVScraperGlobalPlot.Text = strPlot

        'Plot Outline
        Dim strPlotOutline As String = Master.eLang.GetString(64, "Plot Outline")
        lblMovieScraperGlobalOutline.Text = strPlotOutline

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        gbMovieSetImagesPosterOpts.Text = strPoster
        gbTVImagesAllSeasonsPosterOpts.Text = strPoster
        gbTVImagesEpisodePosterOpts.Text = strPoster
        gbTVImagesSeasonPosterOpts.Text = strPoster
        gbTVImagesShowPosterOpts.Text = strPoster
        lblMovieSetPosterExpertSingle.Text = strPoster
        lblMovieSetSourcesFilenamingKodiExtendedPoster.Text = strPoster
        lblMovieSetSourcesFilenamingKodiMSAAPoster.Text = strPoster
        lblMovieSourcesFilenamingExpertBDMVPoster.Text = strPoster
        lblMovieSourcesFilenamingExpertMultiPoster.Text = strPoster
        lblMovieSourcesFilenamingExpertSinglePoster.Text = strPoster
        lblMovieSourcesFilenamingExpertVTSPoster.Text = strPoster
        lblTVSourcesFilenamingBoxeeDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingKodiDefaultsPoster.Text = strPoster
        lblTVSourcesFilenamingNMTDefaultsPoster.Text = strPoster
        lblMovieSourcesFilenamingBoxeeDefaultsPoster.Text = strPoster
        lblMovieSourcesFilenamingKodiDefaultsPoster.Text = strPoster
        lblMovieSourcesFilenamingNMTDefaultsPoster.Text = strPoster
        lblTVAllSeasonsPosterExpert.Text = strPoster
        lblTVEpisodePosterExpert.Text = strPoster
        lblTVSeasonPosterExpert.Text = strPoster
        lblTVShowPosterExpert.Text = strPoster

        'Preferred Language
        Dim strPreferredLanguage As String = Master.eLang.GetString(741, "Preferred Language")
        gbMovieSetImagesLanguageOpts.Text = strPreferredLanguage
        gbTVImagesLanguageOpts.Text = strPreferredLanguage

        'Preferred Size:
        Dim strPreferredSize As String = Master.eLang.GetString(482, "Preferred Size:")
        lblMovieSetBannerSize.Text = strPreferredSize
        lblMovieSetFanartSize.Text = strPreferredSize
        lblMovieSetKeyartSize.Text = strPreferredSize
        lblMovieSetPosterSize.Text = strPreferredSize
        lblTVAllSeasonsBannerPrefSize.Text = strPreferredSize
        lblTVAllSeasonsFanartPrefSize.Text = strPreferredSize
        lblTVAllSeasonsPosterPrefSize.Text = strPreferredSize
        lblTVEpisodeFanartPrefSize.Text = strPreferredSize
        lblTVEpisodePosterPrefSize.Text = strPreferredSize
        lblTVSeasonBannerPrefSize.Text = strPreferredSize
        lblTVSeasonFanartPrefSize.Text = strPreferredSize
        lblTVSeasonPosterPrefSize.Text = strPreferredSize
        lblTVShowBannerPrefSize.Text = strPreferredSize
        lblTVShowExtrafanartsSize.Text = strPreferredSize
        lblTVShowFanartPrefSize.Text = strPreferredSize
        lblTVShowKeyartPrefSize.Text = strPreferredSize
        lblTVShowPosterPrefSize.Text = strPreferredSize

        'Preferred Type:
        Dim strPreferredType As String = Master.eLang.GetString(730, "Preferred Type:")
        lblTVAllSeasonsBannerPrefType.Text = strPreferredType
        lblTVSeasonBannerPrefType.Text = strPreferredType
        lblTVShowBannerPrefType.Text = strPreferredType

        'Preselect in "Image Select" dialog
        Dim strPreselectInImageSelectDialog As String = Master.eLang.GetString(1023, "Preselect in ""Image Select"" dialog")
        chkTVShowExtrafanartsPreselect.Text = strPreselectInImageSelectDialog

        'Recusive
        Dim strRecursive = Master.eLang.GetString(411, "Recursive")
        colMovieSourcesRecur.Text = strRecursive

        'Premiered
        Dim strPremiered As String = Master.eLang.GetString(724, "Premiered")
        lblMovieScraperGlobalPremiered.Text = strPremiered
        lblTVScraperGlobalPremiered.Text = strPremiered

        'Rating
        Dim strRatings As String = Master.eLang.GetString(1145, "Ratings")
        lblMovieScraperGlobalRatings.Text = strRatings
        lblTVScraperGlobalRatings.Text = strRatings

        'Runtime
        Dim strRuntime As String = Master.eLang.GetString(238, "Runtime")
        lblMovieScraperGlobalRuntime.Text = strRuntime
        lblTVScraperGlobalRuntime.Text = strRuntime

        'Save extended Collection information to NFO (Kodi 16.0 "Jarvis" and newer)
        Dim strSaveExtended As String = Master.eLang.GetString(1075, "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)")
        chkMovieScraperCollectionsExtendedInfo.Text = strSaveExtended

        'Scrape Only Actors With Images
        Dim strScrapeOnlyActorsWithImages As String = Master.eLang.GetString(510, "Scrape Only Actors With Images")
        chkMovieScraperCastWithImg.Text = strScrapeOnlyActorsWithImages
        chkTVScraperCastWithImg.Text = strScrapeOnlyActorsWithImages

        'Scraper Fields - Global
        Dim strScraperGlobal As String = Master.eLang.GetString(577, "Scraper Fields - Global")
        gbMovieScraperGlobalOpts.Text = strScraperGlobal
        gbMovieSetScraperGlobalOpts.Text = strScraperGlobal
        gbTVScraperGlobalOpts.Text = strScraperGlobal

        'Season
        Dim strSeason As String = Master.eLang.GetString(650, "Season")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJSeason.Text = strSeason
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixSeason.Text = strSeason
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJSeason.Text = strSeason
        tpTVImagesSeason.Text = strSeason
        tpTVSourcesFilenamingExpertSeason.Text = strSeason

        'Season #
        Dim strSeasonNR As String = Master.eLang.GetString(659, "Season #")

        'Season Landscape
        Dim strSeasonLandscape As String = Master.eLang.GetString(1018, "Season Landscape")
        lblTVSourcesFilenamingKodiADSeasonLandscape.Text = strSeasonLandscape
        lblTVSourcesFilenamingKodiExtendedSeasonLandscape.Text = strSeasonLandscape

        'Season List Sorting
        Dim strSeasonListSorting As String = Master.eLang.GetString(493, "Season List Sorting")
        gbTVGeneralSeasonListSortingOpts.Text = strSeasonListSorting

        'Seasons
        Dim strSeasons As String = Master.eLang.GetString(681, "Seasons")
        lblTVScraperGlobalHeaderSeasons.Text = strSeasons

        'Show List Sorting
        Dim strShowListSorting As String = Master.eLang.GetString(492, "Show List Sorting")
        gbTVGeneralShowListSortingOpts.Text = strShowListSorting

        'Shows
        Dim strShows As String = Master.eLang.GetString(680, "Shows")
        lblTVScraperGlobalHeaderShows.Text = strShows

        'Single Video
        Dim strSingleVideo As String = Master.eLang.GetString(413, "Single Video")
        colMovieSourcesSingle.Text = strSingleVideo

        'Sort Tokens to Ignore
        Dim strSortTokens As String = Master.eLang.GetString(463, "Sort Tokens to Ignore")
        gbGeneralSortTokensOpts.Text = strSortTokens

        'Sorting
        Dim strSorting As String = Master.eLang.GetString(895, "Sorting")
        colTVSourcesSorting.Text = strSorting

        'Status
        Dim strStatus As String = Master.eLang.GetString(215, "Status")
        lblTVScraperGlobalStatus.Text = strStatus

        'Store themes in movie directory
        Dim strStoreThemeInMovieDirectory As String = Master.eLang.GetString(1258, "Store themes in movie directory")
        chkMovieThemeTvTunesMoviePath.Text = strStoreThemeInMovieDirectory

        'Store themes in custom path
        Dim strStoreThemeInCustomPath As String = Master.eLang.GetString(1259, "Store themes in a custom path")
        chkMovieThemeTvTunesCustom.Text = strStoreThemeInCustomPath
        chkTVShowThemeTvTunesCustom.Text = strStoreThemeInCustomPath

        'Store themes in sub directories
        Dim strStoreThemeInSubDirectory As String = Master.eLang.GetString(1260, "Store themes in sub directories")
        chkMovieThemeTvTunesSub.Text = strStoreThemeInSubDirectory
        chkTVShowThemeTvTunesSub.Text = strStoreThemeInSubDirectory

        'Store themes in tv show directory
        Dim strStoreThemeInShowDirectory As String = Master.eLang.GetString(1265, "Store themes in tv show directory")
        chkTVShowThemeTvTunesShowPath.Text = strStoreThemeInShowDirectory

        'Studios
        Dim strStudio As String = Master.eLang.GetString(226, "Studios")
        lblMovieScraperGlobalStudios.Text = strStudio
        lblTVScraperGlobalStudios.Text = strStudio

        'Subtitles
        Dim strSubtitles As String = Master.eLang.GetString(152, "Subtitles")

        'Tagline
        Dim strTagline As String = Master.eLang.GetString(397, "Tagline")
        lblMovieScraperGlobalTagline.Text = strTagline
        lblTVScraperGlobalTagline.Text = strTagline

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")

        'Themes
        Dim strThemes As String = Master.eLang.GetString(1285, "Themes")
        gbMovieThemeOpts.Text = strThemes

        'Title
        Dim strTitle As String = Master.eLang.GetString(21, "Title")
        lblMovieScraperGlobalTitle.Text = strTitle
        lblMovieSetScraperGlobalTitle.Text = strTitle
        lblTVScraperGlobalTitle.Text = strTitle

        'Top250
        Dim strTop250 As String = Master.eLang.GetString(591, "Top 250")
        lblMovieScraperGlobalTop250.Text = strTop250

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        lblMovieScraperGlobalTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertBDMVTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertMultiTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertSingleTrailer.Text = strTrailer
        lblMovieSourcesFilenamingExpertVTSTrailer.Text = strTrailer
        lblMovieSourcesFilenamingKodiDefaultsTrailer.Text = strTrailer
        lblMovieSourcesFilenamingNMTDefaultsTrailer.Text = strTrailer

        'TV Show
        Dim strTVShow As String = Master.eLang.GetString(700, "TV Show")
        lblTVSourcesFilenamingBoxeeDefaultsHeaderBoxeeTVShow.Text = strTVShow
        lblTVSourcesFilenamingKodiDefaultsHeaderFrodoHelixTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderNMJTVShow.Text = strTVShow
        lblTVSourcesFilenamingNMTDefaultsHeaderYAMJTVShow.Text = strTVShow
        tpTVImagesTVShow.Text = strTVShow
        tpTVSourcesFilenamingExpertTVShow.Text = strTVShow

        'TV Show Landscape
        Dim strTVShowLandscape As String = Master.eLang.GetString(1010, "TV Show Landscape")
        lblTVSourcesFilenamingKodiADTVShowLandscape.Text = strTVShowLandscape
        lblTVSourcesFilenamingKodiExtendedTVShowLandscape.Text = strTVShowLandscape

        'Use
        Dim strUse As String = Master.eLang.GetString(872, "Use")

        'Use Certification for MPAA
        Dim strUseCertForMPAA As String = Master.eLang.GetString(511, "Use Certification for MPAA")
        chkMovieScraperCertForMPAA.Text = strUseCertForMPAA
        chkTVScraperShowCertForMPAA.Text = strUseCertForMPAA

        'Use Original Title as Title
        Dim strUseOriginalTitleAsTitle As String = Master.eLang.GetString(240, "Use Original Title as Title")
        chkMovieScraperOriginalTitleAsTitle.Text = strUseOriginalTitleAsTitle
        chkTVScraperShowOriginalTitleAsTitle.Text = strUseOriginalTitleAsTitle

        'Watched
        Dim strWatched As String = Master.eLang.GetString(981, "Watched")

        'Use Folder Name
        Dim strUseFolderName As String = Master.eLang.GetString(412, "Use Folder Name")
        colMovieSourcesFolder.Text = strUseFolderName

        'User Rating
        Dim strUserRating As String = Master.eLang.GetString(1467, "User Rating")
        lblMovieScraperGlobalUserRating.Text = strUserRating
        lblTVScraperGlobalUserRating.Text = strUserRating

        'Writers
        Dim strWriters As String = Master.eLang.GetString(394, "Writers")
        lblMovieScraperGlobalCredits.Text = strWriters
        lblTVScraperGlobalCredits.Text = strWriters

        Text = Master.eLang.GetString(420, "Settings")
        btnApply.Text = Master.eLang.GetString(276, "Apply")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnGeneralDigitGrpSymbolSettings.Text = Master.eLang.GetString(420, "Settings")
        btnMovieSetScraperTitleRenamerAdd.Text = Master.eLang.GetString(28, "Add")
        btnMovieSetScraperTitleRenamerRemove.Text = Master.eLang.GetString(30, "Remove")
        btnMovieSourceAdd.Text = Master.eLang.GetString(407, "Add Source")
        btnMovieSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        btnMovieSourceRemove.Text = Master.eLang.GetString(30, "Remove")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnRemTVSource.Text = Master.eLang.GetString(30, "Remove")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingClear.Text = Master.eLang.GetString(123, "Clear")
        btnTVSourcesRegexTVShowMatchingEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingRemove.Text = Master.eLang.GetString(30, "Remove")
        btnTVSourceEdit.Text = Master.eLang.GetString(535, "Edit Source")
        chkFileSystemCleanerWhitelist.Text = Master.eLang.GetString(440, "Whitelist Video Extensions")
        chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        chkGeneralDigitGrpSymbolVotes.Text = Master.eLang.GetString(1387, "Use digit grouping symbol for Votes count")
        chkGeneralDoubleClickScrape.Text = Master.eLang.GetString(1198, "Enable Image Scrape On Double Right Click")
        chkGeneralDisplayBanner.Text = Master.eLang.GetString(1146, "Display Banner")
        chkGeneralDisplayCharacterArt.Text = Master.eLang.GetString(1147, "Display CharacterArt")
        chkGeneralDisplayClearArt.Text = Master.eLang.GetString(1148, "Display ClearArt")
        chkGeneralDisplayClearLogo.Text = Master.eLang.GetString(1149, "Display ClearLogo")
        chkGeneralDisplayDiscArt.Text = Master.eLang.GetString(1150, "Display DiscArt")
        chkGeneralDisplayFanart.Text = Master.eLang.GetString(455, "Display Fanart")
        chkGeneralDisplayFanartSmall.Text = Master.eLang.GetString(967, "Display Small Fanart")
        chkGeneralDisplayLandscape.Text = Master.eLang.GetString(1151, "Display Landscape")
        chkGeneralDisplayPoster.Text = Master.eLang.GetString(456, "Display Poster")
        chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        chkGeneralImageFilter.Text = Master.eLang.GetString(1459, "Activate ImageFilter to avoid duplicate images")
        chkGeneralImageFilterAutoscraper.Text = Master.eLang.GetString(1457, "Autoscraper")
        chkGeneralImageFilterFanart.Text = Master.eLang.GetString(149, "Fanart")
        chkGeneralImageFilterImagedialog.Text = Master.eLang.GetString(1458, "Imagedialog")
        chkGeneralImageFilterPoster.Text = Master.eLang.GetString(148, "Poster")
        chkGeneralNotificationAdded_Movie.Text = Master.eLang.GetString(817, "New Movie Added")
        chkGeneralNotificationAdded_MovieSet.Text = Master.eLang.GetString(477, "New MovieSet Added")
        chkGeneralNotificationAdded_TVEpisode.Text = Master.eLang.GetString(818, "New Episode Added")
        chkGeneralNotificationAdded_TVShow.Text = Master.eLang.GetString(476, "New Show Added")
        chkGeneralNotificationError.Text = Master.eLang.GetString(1134, "Error")
        chkGeneralNotificationInformation.Text = Master.eLang.GetString(551, "Information")
        chkGeneralNotificationScraped_Movie.Text = Master.eLang.GetString(813, "Movie Scraped")
        chkGeneralNotificationScraped_MovieSet.Text = Master.eLang.GetString(1204, "MovieSet Scraped")
        chkGeneralNotificationScraped_TVEpisode.Text = Master.eLang.GetString(883, "Episode Scraped")
        chkGeneralNotificationScraped_TVSeason.Text = Master.eLang.GetString(247, "Season Scraped")
        chkGeneralNotificationScraped_TVShow.Text = Master.eLang.GetString(248, "Show Scraped")
        chkGeneralNotificationWarning.Text = Master.eLang.GetString(356, "Warning")
        chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        chkGeneralDisplayGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        chkGeneralDisplayLangFlags.Text = Master.eLang.GetString(489, "Display Language Flags")
        chkGeneralDisplayImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        chkGeneralDisplayImgNames.Text = Master.eLang.GetString(1255, "Display Image Names")
        chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        chkMovieSourcesBackdropsAuto.Text = Master.eLang.GetString(521, "Automatically Save Fanart To Backdrops Folder")
        chkMovieCleanDB.Text = Master.eLang.GetString(668, "Clean database after updating library")
        chkMovieGeneralIgnoreLastScan.Text = Master.eLang.GetString(669, "Ignore last scan time when updating library")
        chkMovieGeneralMarkNew.Text = Master.eLang.GetString(459, "Mark New Movies")
        chkMovieLevTolerance.Text = Master.eLang.GetString(462, "Check Title Match Confidence")
        chkMovieProperCase.Text = Master.eLang.GetString(452, "Convert Names to Proper Case")
        chkMovieRecognizeVTSExpertVTS.Text = String.Format(Master.eLang.GetString(537, "Recognize VIDEO_TS{0}without VIDEO_TS folder"), Environment.NewLine)
        chkMovieScanOrderModify.Text = Master.eLang.GetString(796, "Scan in order of last write time")
        chkMovieSetCleanFiles.Text = Master.eLang.GetString(1276, "Remove Images and NFOs with MovieSets")
        chkMovieSetGeneralMarkNew.Text = Master.eLang.GetString(1301, "Mark New MovieSets")
        chkMovieScraperCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")
        chkMovieScraperCollectionsAuto.Text = Master.eLang.GetString(1266, "Add Movie automatically to Collections")
        chkMovieScraperMetaDataIFOScan.Text = Master.eLang.GetString(628, "Enable IFO Parsing")
        chkMovieScraperMetaDataScan.Text = Master.eLang.GetString(517, "Scan Meta Data")
        chkMovieScraperPlotForOutline.Text = Master.eLang.GetString(965, "Use Plot for Plot Outline")
        chkMovieScraperPlotForOutlineIfEmpty.Text = Master.eLang.GetString(958, "Only if Plot Outline is empty")
        chkMovieScraperReleaseDateWriteNode.Text = Master.eLang.GetString(57, "Still write <releasedate> with the value of <premiered>")
        chkMovieScraperStudioWithImg.Text = Master.eLang.GetString(1280, "Scrape Only Studios With Images")
        chkMovieScraperTrailerFromTrailerScrapers.Text = Master.eLang.GetString(236, "also use Trailer Scrapers")
        chkMovieScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        chkMovieScraperXBMCTrailerFormat.Text = Master.eLang.GetString(1187, "Save YouTube-Trailer-Links in XBMC compatible format")
        chkMovieScraperCollectionsYAMJCompatibleSets.Text = Master.eLang.GetString(561, "Save YAMJ Compatible Sets to NFO")
        chkMovieSkipStackedSizeCheck.Text = Master.eLang.GetString(538, "Skip Size Check of Stacked Files")
        chkMovieSortBeforeScan.Text = Master.eLang.GetString(712, "Sort files into folder before each library update")
        chkMovieStackExpertMulti.Text = String.Format(Master.eLang.GetString(1178, "Stack {0}filename{1}"), "<", ">")
        chkMovieUnstackExpertMulti.Text = Master.eLang.GetString(1179, "also save unstacked")
        chkMovieUseBaseDirectoryExpertBDMV.Text = Master.eLang.GetString(1180, "Use Base Directory")
        chkMovieXBMCProtectVTSBDMV.Text = Master.eLang.GetString(1176, "Protect DVD/Bluray Structure")
        chkMovieYAMJWatchedFile.Text = Master.eLang.GetString(1177, "Use .watched Files")
        chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        chkTVDisplayMissingEpisodes.Text = Master.eLang.GetString(733, "Display Missing Episodes")
        chkTVEpisodeNoFilter.Text = Master.eLang.GetString(734, "Build Episode Title Instead of Filtering")
        chkTVGeneralMarkNewEpisodes.Text = Master.eLang.GetString(621, "Mark New Episodes")
        chkTVGeneralMarkNewShows.Text = Master.eLang.GetString(549, "Mark New Shows")
        chkTVScraperEpisodeGuestStarsToActors.Text = Master.eLang.GetString(974, "Add Episode Guest Stars to Actors list")
        chkTVScraperUseMDDuration.Text = Master.eLang.GetString(516, "Use Duration for Runtime")
        chkTVScraperUseSRuntimeForEp.Text = Master.eLang.GetString(1262, "Use Show Runtime for Episodes if no Episode Runtime can be found")
        dgvMovieSetScraperTitleRenamer.Columns(0).HeaderText = Master.eLang.GetString(1277, "From")
        dgvMovieSetScraperTitleRenamer.Columns(1).HeaderText = Master.eLang.GetString(1278, "To")
        gbFileSystemExcludedDirs.Text = Master.eLang.GetString(1273, "Excluded Directories")
        gbFileSystemCleanFiles.Text = Master.eLang.GetString(437, "Clean Files")
        gbFileSystemNoStackExts.Text = Master.eLang.GetString(530, "No Stack Extensions")
        gbFileSystemValidVideoExts.Text = Master.eLang.GetString(534, "Valid Video Extensions")
        gbFileSystemValidSubtitlesExts.Text = Master.eLang.GetString(1284, "Valid Subtitles Extensions")
        gbFileSystemValidThemeExts.Text = Master.eLang.GetString(1081, "Valid Theme Extensions")
        gbGeneralDaemon.Text = Master.eLang.GetString(1261, "Configuration ISO Filescanning")
        gbGeneralDateAdded.Text = Master.eLang.GetString(792, "Adding Date")
        gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        gbGeneralNotification.Text = Master.eLang.GetString(487, "Notifications")
        gbMovieGeneralCustomMarker.Text = Master.eLang.GetString(1190, "Custom Marker")
        gbMovieSourcesBackdropsFolderOpts.Text = Master.eLang.GetString(520, "Backdrops Folder")
        gbMovieGeneralFiltersOpts.Text = Master.eLang.GetString(451, "Folder/File Name Filters")
        gbMovieGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        gbMovieScraperDefFIExtOpts.Text = Master.eLang.GetString(625, "Defaults by File Type")
        gbMovieSetScraperTitleRenamerOpts.Text = Master.eLang.GetString(1279, "Title Renamer")
        gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        gbTVEpisodeFilterOpts.Text = Master.eLang.GetString(671, "Episode Folder/File Name Filters")
        gbTVGeneralMediaListOpts.Text = Master.eLang.GetString(460, "Media List Options")
        gbTVScraperDurationFormatOpts.Text = Master.eLang.GetString(515, "Duration Format")
        gbTVScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields")
        gbTVShowFilterOpts.Text = Master.eLang.GetString(670, "Show Folder/File Name Filters")
        gbTVSourcesRegexTVShowMatching.Text = Master.eLang.GetString(691, "Show Match Regex")
        lblFileSystemCleanerWarning.Text = Master.eLang.GetString(442, "WARNING: Using the Expert Mode Cleaner could potentially delete wanted files. Take care when using this tool.")
        lblFileSystemCleanerWhitelist.Text = Master.eLang.GetString(441, "Whitelisted Extensions:")
        lblGeneralDaemonDrive.Text = Master.eLang.GetString(989, "Driveletter")
        lblGeneralDaemonPath.Text = Master.eLang.GetString(990, "Path to DTAgent.exe/VCDMount.exe")
        lblGeneralImageFilterPosterMatchRate.Text = Master.eLang.GetString(148, "Poster") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralImageFilterFanartMatchRate.Text = Master.eLang.GetString(149, "Fanart") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")
        lblGeneralTheme.Text = String.Concat(Master.eLang.GetString(620, "Theme"), ":")
        lblGeneralntLang.Text = Master.eLang.GetString(430, "Interface Language:")
        lblMovieGeneralCustomMarker1.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #1")
        lblMovieGeneralCustomMarker2.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #2")
        lblMovieGeneralCustomMarker3.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #3")
        lblMovieGeneralCustomMarker4.Text = String.Concat(Master.eLang.GetString(1191, "Custom"), " #4")
        lblMovieLevTolerance.Text = Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblMovieScraperDurationRuntimeFormat.Text = String.Format(Master.eLang.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
        lblMovieScraperMPAANotRated.Text = String.Concat(Master.eLang.GetString(832, "MPAA value if no rating is available"), ":")
        lblMovieSkipLessThan.Text = Master.eLang.GetString(540, "Skip files smaller than:")
        lblMovieSkipLessThanMB.Text = Master.eLang.GetString(539, "MB")
        lblMovieTrailerDefaultSearch.Text = Master.eLang.GetString(1172, "Default Search Parameter:")
        lblMovieTrailerMinQual.Text = Master.eLang.GetString(1027, "Minimum Quality:")
        lblMovieTrailerPrefQual.Text = Master.eLang.GetString(800, "Preferred Quality:")
        lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        lblProxyPassword.Text = String.Concat(Master.eLang.CommonWordsList.Password, ":")
        lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        lblProxyUsername.Text = String.Concat(Master.eLang.CommonWordsList.Username, ":")
        lblSettingsTopDetails.Text = Master.eLang.GetString(518, "Configure Ember's appearance and operation.")
        lblTVScraperGlobalGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        lblTVSourcesRegexTVShowMatchingByDate.Text = Master.eLang.GetString(698, "by Date")
        lblTVSourcesRegexTVShowMatchingRegex.Text = Master.eLang.GetString(699, "Regex")
        lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = Master.eLang.GetString(695, "Default Season")
        tpFileSystemCleanerExpert.Text = Master.eLang.GetString(439, "Expert")
        tpFileSystemCleanerStandard.Text = Master.eLang.GetString(438, "Standard")
        tpMovieSetFilenamingExpertSingle.Text = Master.eLang.GetString(879, "Single Folder")
        tpTVSourcesGeneral.Text = Master.eLang.GetString(38, "General")
        tpTVSourcesRegex.Text = Master.eLang.GetString(699, "Regex")

        'items with text from other items
        btnTVSourceAdd.Text = btnMovieSourceAdd.Text
        chkMovieSetCleanDB.Text = chkMovieCleanDB.Text
        chkMovieStackExpertSingle.Text = chkMovieStackExpertMulti.Text
        chkMovieUnstackExpertSingle.Text = chkMovieUnstackExpertMulti.Text
        chkMovieUseBaseDirectoryExpertVTS.Text = chkMovieUseBaseDirectoryExpertBDMV.Text
        chkTVCleanDB.Text = chkMovieCleanDB.Text
        chkTVEpisodeProperCase.Text = chkMovieProperCase.Text
        chkTVGeneralIgnoreLastScan.Text = chkMovieGeneralIgnoreLastScan.Text
        chkTVScanOrderModify.Text = chkMovieScanOrderModify.Text
        chkTVScraperMetaDataScan.Text = chkMovieScraperMetaDataScan.Text
        chkTVShowProperCase.Text = chkMovieProperCase.Text
        gbMovieSetGeneralMediaListOpts.Text = gbMovieGeneralMediaListOpts.Text
        gbTVScraperDefFIExtOpts.Text = gbTVScraperDefFIExtOpts.Text
        lblSettingsTopTitle.Text = Text
        lblTVSkipLessThan.Text = lblMovieSkipLessThan.Text
        lblTVSkipLessThanMB.Text = lblMovieSkipLessThanMB.Text

        LoadGeneralDateTime()
        LoadMovieBannerSizes()
        LoadCustomScraperButtonModifierTypes_Movie()
        LoadCustomScraperButtonModifierTypes_MovieSet()
        LoadCustomScraperButtonModifierTypes_TV()
        LoadCustomScraperButtonScrapeTypes()
        LoadCustomScraperButtonSelectionTypes()
        LoadMovieFanartSizes()
        LoadMoviePosterSizes()
        LoadMovieTrailerQualities()
        LoadTVBannerSizes()
        LoadTVBannerTypes()
        LoadTVFanartSizes()
        LoadTVPosterSizes()
        LoadTVScraperOptionsOrdering()
    End Sub

    Public Overloads Function ShowDialog() As Structures.SettingsResult
        MyBase.ShowDialog()
        Return _Results
    End Function

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub Button_Apply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
        Settings_Save(True)
        Button_Apply_SetEnabled(False)
        If _Results.AnythingToDo Then _DidApply = True
    End Sub

    Private Sub Button_Apply_SetEnabled(ByVal value As Boolean)
        If Not _NoUpdate Then btnApply.Enabled = value
    End Sub

    Private Sub Button_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If Not _DidApply Then _Results.DidCancel = True
        'Master.eLang.LoadAllLanguage(Master.eSettings.Options.Global.Language, True)
        Close()
    End Sub

    Private Sub Button_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        _NoUpdate = True
        Settings_Save(False)
        Close()
    End Sub

    Private Sub Handle_NeedsDBClean_Movie()
        _Results.NeedsDBClean_Movie = True
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        _Results.NeedsDBClean_TV = True
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie(ByVal id As Long)
        _Results.NeedsDBUpdate_Movie.Add(id)
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV(ByVal id As Long)
        _Results.NeedsDBUpdate_TV.Add(id)
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        _Results.NeedsReload_Movie = True
    End Sub

    Private Sub Handle_NeedsReload_Movieset()
        _Results.NeedsReload_Movieset = True
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        _Results.NeedsReload_TVEpisode = True
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        _Results.NeedsReload_TVShow = True
    End Sub

    Private Sub Handle_NeedsRestart()
        _Results.NeedsRestart = True
    End Sub

    Private Sub Handle_SettingsChanged()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub Handle_StateChanged(ByVal uniqueId As String, ByVal state As Boolean, ByVal diffOrder As Integer)
        Dim tSetPan As Interfaces.ISettingsPanel
        Dim oSetPan As Interfaces.ISettingsPanel
        SuspendLayout()
        tSetPan = _AllSettingsPanels.FirstOrDefault(Function(f) f.UniqueId = uniqueId)

        If tSetPan IsNot Nothing Then
            Try
                Dim t() As TreeNode = tvSettingsList.Nodes.Find(uniqueId, True)

                If t.Count > 0 Then
                    If Not diffOrder = 0 Then
                        Dim p As TreeNode = t(0).Parent
                        Dim i As Integer = t(0).Index
                        If diffOrder < 0 AndAlso Not t(0).PrevNode Is Nothing Then
                            oSetPan = _AllSettingsPanels.FirstOrDefault(Function(f) f.UniqueId = t(0).PrevNode.Name)
                            If oSetPan IsNot Nothing Then oSetPan.Order = i + (diffOrder * -1)
                        End If
                        If diffOrder > 0 AndAlso Not t(0).NextNode Is Nothing Then
                            oSetPan = _AllSettingsPanels.FirstOrDefault(Function(f) f.UniqueId = t(0).NextNode.Name)
                            If oSetPan IsNot Nothing Then oSetPan.Order = i + (diffOrder * -1)
                        End If
                        p.Nodes.Remove(t(0))
                        p.Nodes.Insert(i + diffOrder, t(0))
                        t(0).TreeView.SelectedNode = t(0)
                        tSetPan.Order = i + diffOrder

                        'rearrange addons of the same type
                        Dim CurrentType = _AllSettingsPanels.FirstOrDefault(Function(f) f.UniqueId = uniqueId).ParentType
                        Dim TotalCount = _AllSettingsPanels.Where(Function(f) f.ParentType = CurrentType).Count
                        For Each Panels In _AllSettingsPanels.Where(Function(f) f.ParentType = CurrentType)
                            Panels.OrderChanged(TotalCount)
                        Next
                    End If
                    t(0).ImageIndex = If(state, 9, 10)
                    t(0).SelectedImageIndex = If(state, 9, 10)
                    pbSettingsCurrent.Image = ilSettings.Images(If(state, 9, 10))
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        ResumeLayout()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub SettingsPanels_Add_AllPanels()
        'Master Settings Panels
        _AllSettingsPanels.Add(New frmMovie_Information)
        _AllSettingsPanels.Add(New frmMovie_FileNaming)
        '_AllSettingsPanels.Add(New frmMovie_FileNaming_Renaming)
        _AllSettingsPanels.Add(New frmMovie_GUI)
        _AllSettingsPanels.Add(New frmMovie_Image)
        _AllSettingsPanels.Add(New frmMovie_Source)
        _AllSettingsPanels.Add(New frmMovie_Theme)
        _AllSettingsPanels.Add(New frmMovie_Trailer)
        _AllSettingsPanels.Add(New frmMovieset_Information)
        _AllSettingsPanels.Add(New frmMovieset_FileNaming)
        _AllSettingsPanels.Add(New frmMovieset_GUI)
        _AllSettingsPanels.Add(New frmMovieset_Image)
        _AllSettingsPanels.Add(New frmOption_AVCodecMapping)
        _AllSettingsPanels.Add(New frmOption_Connection)
        _AllSettingsPanels.Add(New frmOption_FileSystem)
        _AllSettingsPanels.Add(New frmOption_Global)
        _AllSettingsPanels.Add(New frmOption_GUI)
        '_AllSettingsPanels.Add(New frmOption_VideoSourceMapping)
        _AllSettingsPanels.Add(New frmTV_Information)
        _AllSettingsPanels.Add(New frmTV_FileNaming)
        '_AllSettingsPanels.Add(New frmTV_FileNaming_Renaming)
        _AllSettingsPanels.Add(New frmTV_GUI)
        _AllSettingsPanels.Add(New frmTV_Image)
        _AllSettingsPanels.Add(New frmTV_Source)
        _AllSettingsPanels.Add(New frmTV_Source_Regex)
        _AllSettingsPanels.Add(New frmTV_Theme)

        'Addon Panels
        Dim settingsPanel As Interfaces.ISettingsPanel = Nothing
        'Use 9000 as panel order beginning to show all generic panels after MasterPanels
        Dim iPanelCounter_Generic As Integer = 9000
        Dim iPanelCounter_Data_Movie As Integer = 1
        Dim iPanelCounter_Data_Movieset As Integer = 1
        Dim iPanelCounter_Data_TV As Integer = 1
        Dim iPanelCounter_Image_Movie As Integer = 1
        Dim iPanelCounter_Image_Movieset As Integer = 1
        Dim iPanelCounter_Image_TV As Integer = 1
        Dim iPanelCounter_Theme_Movie As Integer = 1
        Dim iPanelCounter_Theme_TV As Integer = 1
        Dim iPanelCounter_Trailer_Movie As Integer = 1
        For Each addon In Addons.Instance.Addons.OrderBy(Function(f) f.AssemblyFileName)
            addon.AddonInterface.SettingsPanels = New List(Of Interfaces.ISettingsPanel)
            addon.AddonInterface.InjectSettingsPanels()
            For Each settingsPanel In addon.AddonInterface.SettingsPanels
                If settingsPanel IsNot Nothing AndAlso settingsPanel.MainPanel IsNot Nothing Then
                    Select Case settingsPanel.ParentType
                        Case Enums.SettingsPanelType.MovieInformation
                            settingsPanel.Order = iPanelCounter_Data_Movie
                            iPanelCounter_Data_Movie += 1
                        Case Enums.SettingsPanelType.MovieImage
                            settingsPanel.Order = iPanelCounter_Image_Movie
                            iPanelCounter_Image_Movie += 1
                        Case Enums.SettingsPanelType.MovieTheme
                            settingsPanel.Order = iPanelCounter_Theme_Movie
                            iPanelCounter_Theme_Movie += 1
                        Case Enums.SettingsPanelType.MovieTrailer
                            settingsPanel.Order = iPanelCounter_Trailer_Movie
                            iPanelCounter_Trailer_Movie += 1
                        Case Enums.SettingsPanelType.MoviesetInformation
                            settingsPanel.Order = iPanelCounter_Data_Movieset
                            iPanelCounter_Data_Movieset += 1
                        Case Enums.SettingsPanelType.MoviesetImage
                            settingsPanel.Order = iPanelCounter_Image_Movieset
                            iPanelCounter_Image_Movieset += 1
                        Case Enums.SettingsPanelType.TVInformation
                            settingsPanel.Order = iPanelCounter_Data_TV
                            iPanelCounter_Data_TV += 1
                        Case Enums.SettingsPanelType.TVImage
                            settingsPanel.Order = iPanelCounter_Image_TV
                            iPanelCounter_Image_TV += 1
                        Case Enums.SettingsPanelType.TVTheme
                            settingsPanel.Order = iPanelCounter_Theme_TV
                            iPanelCounter_Theme_TV += 1
                        Case Else
                            settingsPanel.Order = iPanelCounter_Generic
                            iPanelCounter_Generic += 1
                    End Select
                    settingsPanel.UniqueId = String.Concat(addon.AssemblyName, "_", settingsPanel.MainPanel.Parent.Name)
                    If settingsPanel.ImageIndex = -1 AndAlso settingsPanel.Image IsNot Nothing Then
                        ilSettings.Images.Add(settingsPanel.UniqueId, settingsPanel.Image)
                        settingsPanel.ImageIndex = ilSettings.Images.IndexOfKey(settingsPanel.UniqueId)
                    End If
                    _AllSettingsPanels.Add(settingsPanel)
                End If
            Next
        Next

        For Each Panel In _AllSettingsPanels
            AddHandler Panel.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
            AddHandler Panel.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
            AddHandler Panel.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
            AddHandler Panel.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
            AddHandler Panel.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
            AddHandler Panel.NeedsReload_Movieset, AddressOf Handle_NeedsReload_Movieset
            AddHandler Panel.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
            AddHandler Panel.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
            AddHandler Panel.NeedsRestart, AddressOf Handle_NeedsRestart
            AddHandler Panel.SettingsChanged, AddressOf Handle_SettingsChanged
            AddHandler Panel.StateChanged, AddressOf Handle_StateChanged
        Next
    End Sub

    Private Sub SettingsPanels_Remove_AllPanels()
        For Each Panel In _AllSettingsPanels
            RemoveHandler Panel.NeedsDBClean_Movie, AddressOf Handle_NeedsDBClean_Movie
            RemoveHandler Panel.NeedsDBClean_TV, AddressOf Handle_NeedsDBClean_TV
            RemoveHandler Panel.NeedsDBUpdate_Movie, AddressOf Handle_NeedsDBUpdate_Movie
            RemoveHandler Panel.NeedsDBUpdate_TV, AddressOf Handle_NeedsDBUpdate_TV
            RemoveHandler Panel.NeedsReload_Movie, AddressOf Handle_NeedsReload_Movie
            RemoveHandler Panel.NeedsReload_Movieset, AddressOf Handle_NeedsReload_Movieset
            RemoveHandler Panel.NeedsReload_TVEpisode, AddressOf Handle_NeedsReload_TVEpisode
            RemoveHandler Panel.NeedsReload_TVShow, AddressOf Handle_NeedsReload_TVShow
            RemoveHandler Panel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler Panel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler Panel.StateChanged, AddressOf Handle_StateChanged
        Next
    End Sub

    Private Sub SettingsPanels_Remove_CurrentPanel()
        If pnlSettingsMain.Controls.Count > 0 Then
            pnlSettingsMain.Controls.Remove(_CurrentPanel)
        End If
    End Sub

    Private Sub TopMenu_AddButtons()
        Dim lstTSB As New List(Of ToolStripButton)
        Dim nTSB As ToolStripButton

        tsSettingsTopMenu.Items.Clear()

        'first create all the buttons so we can get their size to calculate the spacer
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(390, "Options"),
            .Image = My.Resources.General,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Options, .Index = 100, .Title = Master.eLang.GetString(390, "Options")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(36, "Movies"),
            .Image = My.Resources.Movie,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Movie, .Index = 200, .Title = Master.eLang.GetString(36, "Movies")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(1203, "MovieSets"),
            .Image = My.Resources.MovieSet,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Movieset, .Index = 300, .Title = Master.eLang.GetString(1203, "MovieSets")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(653, "TV Shows"),
            .Image = My.Resources.TVShows,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.TV, .Index = 400, .Title = Master.eLang.GetString(653, "TV Shows")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(802, "Addons"),
            .Image = My.Resources.modules,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Addon, .Index = 500, .Title = Master.eLang.GetString(802, "Addons")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)
        nTSB = New ToolStripButton With {
            .Text = Master.eLang.GetString(429, "Miscellaneous"),
            .Image = My.Resources.Miscellaneous,
            .TextImageRelation = TextImageRelation.ImageAboveText,
            .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            .Tag = New ButtonTag With {.PanelType = Enums.SettingsPanelType.Miscellaneous, .Index = 600, .Title = Master.eLang.GetString(429, "Miscellaneous")}
        }
        AddHandler nTSB.Click, AddressOf TopMenu_Button_Click
        lstTSB.Add(nTSB)

        If lstTSB.Count > 0 Then
            Dim ButtonsWidth As Integer = 0
            Dim ButtonsCount As Integer = 0
            Dim sLength As Integer = 0
            Dim sRest As Double = 0
            Dim sSpacer As String = String.Empty

            'add all buttons to the top horizontal menu
            For Each tButton As ToolStripButton In lstTSB.OrderBy(Function(b) DirectCast(b.Tag, ButtonTag).Index)
                tsSettingsTopMenu.Items.Add(New ToolStripLabel With {.Text = String.Empty, .Tag = "spacer"})
                tsSettingsTopMenu.Items.Add(tButton)
            Next

            'calculate the buttons width and count
            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If TypeOf item Is ToolStripButton Then
                    ButtonsWidth += item.Width
                    ButtonsCount += 1
                End If
            Next

            sRest = (tsSettingsTopMenu.Width - ButtonsWidth - 1) / (ButtonsCount + 1)

            'formula to calculate the count of spaces to reach the label.width
            'spaces (x) to width (y) in px: 1 = 10, 2 = 13, 3 = 16, 4 = 19, 5 = 22
            'x = 10 + ((y - 1) * 3) or
            'y = (x - 10) / 3 + 1
            sLength = Convert.ToInt32((sRest - 10) / 3 + 1)

            If Not sRest < 10 Then
                sSpacer = New String(Convert.ToChar(" "), sLength)
            Else
                sSpacer = New String(Convert.ToChar(" "), 1)
            End If

            For Each item As ToolStripItem In tsSettingsTopMenu.Items
                If item.Tag IsNot Nothing AndAlso item.Tag.ToString = "spacer" Then
                    item.Text = sSpacer
                End If
            Next

            'set default page
            _CurrentButton = DirectCast(lstTSB.Item(0).Tag, ButtonTag)
            TreeView_Fill(DirectCast(lstTSB.Item(0).Tag, ButtonTag).PanelType)
        End If
    End Sub

    Private Sub TopMenu_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        _CurrentButton = DirectCast(DirectCast(sender, ToolStripButton).Tag, ButtonTag)
        TreeView_Fill(_CurrentButton.PanelType)
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvSettingsList.AfterSelect
        pbSettingsCurrent.Image = ilSettings.Images(tvSettingsList.SelectedNode.ImageIndex)
        lblSettingsCurrent.Text = String.Format("{0} - {1}", _CurrentButton.Title, tvSettingsList.SelectedNode.Text)

        SettingsPanels_Remove_CurrentPanel()

        _CurrentPanel = _AllSettingsPanels.FirstOrDefault(Function(p) p.UniqueId = tvSettingsList.SelectedNode.Name).MainPanel
        _CurrentPanel.Location = New Point(0, 0)
        _CurrentPanel.Dock = DockStyle.Fill
        pnlSettingsMain.Controls.Add(_CurrentPanel)
        _CurrentPanel.Visible = True
        pnlSettingsMain.Refresh()
    End Sub

    Private Sub TreeView_Fill(ByVal panelType As Enums.SettingsPanelType)
        Dim pNode As New TreeNode

        tvSettingsList.Nodes.Clear()
        SettingsPanels_Remove_CurrentPanel()

        For Each MainPanel In _AllSettingsPanels.Where(Function(m) m.ParentType = panelType).OrderBy(Function(s) s.Order)
            pNode = New TreeNode(MainPanel.Title, MainPanel.ImageIndex, MainPanel.ImageIndex) With {.Name = MainPanel.UniqueId}
            For Each SubPanel In _AllSettingsPanels.Where(Function(s) s.ParentType = MainPanel.ChildType).OrderBy(Function(s) s.Order)
                pNode.Nodes.Add(New TreeNode(SubPanel.Title, SubPanel.ImageIndex, SubPanel.ImageIndex) With {.Name = SubPanel.UniqueId})
            Next
            tvSettingsList.Nodes.Add(pNode)
        Next

        If tvSettingsList.Nodes.Count > 0 Then
            tvSettingsList.ExpandAll()
            tvSettingsList.SelectedNode = tvSettingsList.Nodes(0)
        Else
            pbSettingsCurrent.Image = Nothing
            lblSettingsCurrent.Text = String.Empty
        End If
    End Sub

    Private Sub btnTVEpisodeFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterAdd.Click
        If Not String.IsNullOrEmpty(txtTVEpisodeFilter.Text) Then
            lstTVEpisodeFilter.Items.Add(txtTVEpisodeFilter.Text)
            txtTVEpisodeFilter.Text = String.Empty
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_TVEpisode = True
        End If

        txtTVEpisodeFilter.Focus()
    End Sub

    Private Sub btnMovieFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterAdd.Click
        If Not String.IsNullOrEmpty(txtMovieFilter.Text) Then
            lstMovieFilters.Items.Add(txtMovieFilter.Text)
            txtMovieFilter.Text = String.Empty
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_Movie = True
        End If

        txtMovieFilter.Focus()
    End Sub

    Private Sub btnFileSystemExcludedDirsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemExcludedDirsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemExcludedDirs.Text) Then
            If Not lstFileSystemExcludedDirs.Items.Contains(txtFileSystemExcludedDirs.Text.ToLower) Then
                AddExcludedDir(txtFileSystemExcludedDirs.Text)
                RefreshFileSystemExcludeDirs()
                txtFileSystemExcludedDirs.Text = String.Empty
                txtFileSystemExcludedDirs.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemExcludedDirsBrowse_Click(sender As Object, e As EventArgs) Handles btnFileSystemExcludedDirsBrowse.Click
        With fbdBrowse
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtFileSystemExcludedDirs.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnFileSystemExcludedDirsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemExcludedDirsRemove.Click
        RemoveExcludeDir()
        RefreshFileSystemExcludeDirs()
    End Sub

    Private Sub lstFileSystemExcludedDirs_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemExcludedDirs.KeyDown
        If e.KeyCode = Keys.Delete Then
            RemoveExcludeDir()
            RefreshFileSystemExcludeDirs()
        End If
    End Sub

    Private Sub AddExcludedDir(ByVal Path As String)
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLcommand.CommandText = "INSERT OR REPLACE INTO ExcludeDir (Dirname) VALUES (?);"

                Dim parDirname As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirname", DbType.String, 0, "Dirname")
                parDirname.Value = Path

                SQLcommand.ExecuteNonQuery()
            End Using
            SQLtransaction.Commit()
        End Using

        Button_Apply_SetEnabled(True)
        _Results.NeedsDBClean_Movie = True
        _Results.NeedsDBClean_TV = True
    End Sub

    Private Sub RemoveExcludeDir()
        If lstFileSystemExcludedDirs.SelectedItems.Count > 0 Then
            lstFileSystemExcludedDirs.BeginUpdate()

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim parDirname As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parDirname", DbType.String, 0, "Dirname")
                    While lstFileSystemExcludedDirs.SelectedItems.Count > 0
                        parDirname.Value = lstFileSystemExcludedDirs.SelectedItems(0).ToString
                        SQLcommand.CommandText = String.Concat("DELETE FROM ExcludeDir WHERE Dirname = (?);")
                        SQLcommand.ExecuteNonQuery()
                        lstFileSystemExcludedDirs.Items.Remove(lstFileSystemExcludedDirs.SelectedItems(0))
                    End While
                End Using
                SQLtransaction.Commit()
            End Using

            lstFileSystemExcludedDirs.EndUpdate()
            lstFileSystemExcludedDirs.Refresh()

            Button_Apply_SetEnabled(True)
            '_Results.NeedsDBUpdate_Movie = True
            '_Results.NeedsDBUpdate_TV = True
        End If
    End Sub

    Private Sub btnFileSystemValidVideoExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidVideoExts.Text) Then
            If Not txtFileSystemValidVideoExts.Text.Substring(0, 1) = "." Then txtFileSystemValidVideoExts.Text = String.Concat(".", txtFileSystemValidVideoExts.Text)
            If Not lstFileSystemValidVideoExts.Items.Contains(txtFileSystemValidVideoExts.Text.ToLower) Then
                lstFileSystemValidVideoExts.Items.Add(txtFileSystemValidVideoExts.Text.ToLower)
                Button_Apply_SetEnabled(True)
                '_Results.NeedsDBUpdate_Movie = True
                '_Results.NeedsDBUpdate_TV = True
                txtFileSystemValidVideoExts.Text = String.Empty
                txtFileSystemValidVideoExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidSubtitlesExts.Text) Then
            If Not txtFileSystemValidSubtitlesExts.Text.Substring(0, 1) = "." Then txtFileSystemValidSubtitlesExts.Text = String.Concat(".", txtFileSystemValidSubtitlesExts.Text)
            If Not lstFileSystemValidSubtitlesExts.Items.Contains(txtFileSystemValidSubtitlesExts.Text.ToLower) Then
                lstFileSystemValidSubtitlesExts.Items.Add(txtFileSystemValidSubtitlesExts.Text.ToLower)
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_Movie = True
                _Results.NeedsReload_TVEpisode = True
                txtFileSystemValidSubtitlesExts.Text = String.Empty
                txtFileSystemValidSubtitlesExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemValidThemeExts.Text) Then
            If Not txtFileSystemValidThemeExts.Text.Substring(0, 1) = "." Then txtFileSystemValidThemeExts.Text = String.Concat(".", txtFileSystemValidThemeExts.Text)
            If Not lstFileSystemValidThemeExts.Items.Contains(txtFileSystemValidThemeExts.Text.ToLower) Then
                lstFileSystemValidThemeExts.Items.Add(txtFileSystemValidThemeExts.Text.ToLower)
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_Movie = True
                _Results.NeedsReload_TVShow = True
                txtFileSystemValidThemeExts.Text = String.Empty
                txtFileSystemValidThemeExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnFileSystemNoStackExtsAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemNoStackExtsAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemNoStackExts.Text) Then
            If Not txtFileSystemNoStackExts.Text.Substring(0, 1) = "." Then txtFileSystemNoStackExts.Text = String.Concat(".", txtFileSystemNoStackExts.Text)
            If Not lstFileSystemNoStackExts.Items.Contains(txtFileSystemNoStackExts.Text) Then
                lstFileSystemNoStackExts.Items.Add(txtFileSystemNoStackExts.Text)
                Button_Apply_SetEnabled(True)
                '_Results.NeedsDBUpdate_Movie = True
                '_Results.NeedsDBUpdate_TV = True
                txtFileSystemNoStackExts.Text = String.Empty
                txtFileSystemNoStackExts.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVShowFilterAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterAdd.Click
        If Not String.IsNullOrEmpty(txtTVShowFilter.Text) Then
            lstTVShowFilter.Items.Add(txtTVShowFilter.Text)
            txtTVShowFilter.Text = String.Empty
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_TVShow = True
        End If

        txtTVShowFilter.Focus()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingAdd.Click
        If String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.regexp In TVShowMatching Select lRegex.Id).Max
            TVShowMatching.Add(New Settings.regexp With {
                               .Id = Convert.ToInt32(lID) + 1,
                               .Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text,
                               .DefaultSeason = If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text) OrElse Not Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0), -2, CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text)),
                               .ByDate = chkTVSourcesRegexTVShowMatchingByDate.Checked})
        Else
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.Id = Convert.ToInt32(btnTVSourcesRegexTVShowMatchingAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text
                selRex(0).DefaultSeason = CInt(If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text), "-2", txtTVSourcesRegexTVShowMatchingDefaultSeason.Text))
                selRex(0).ByDate = chkTVSourcesRegexTVShowMatchingByDate.Checked
            End If
        End If

        ClearTVShowMatching()
        Button_Apply_SetEnabled(True)
        LoadTVShowMatching()
    End Sub

    Private Sub btnGeneralSortTokenAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGeneralSortTokenAdd.Click
        If Not String.IsNullOrEmpty(txtGeneralSortToken.Text) Then
            If Not lstGeneralSortTokens.Items.Contains(txtGeneralSortToken.Text) Then
                lstGeneralSortTokens.Items.Add(txtGeneralSortToken.Text)
                Button_Apply_SetEnabled(True)
                txtGeneralSortToken.Text = String.Empty
                txtGeneralSortToken.Focus()
            End If
        End If
    End Sub

    Private Sub btnTVScraperSeasonTitleBlacklist_Click(sender As Object, e As EventArgs) Handles btnTVScraperSeasonTitleBlacklist.Click
        If frmTV_Information_SeasonTitleBlacklist.ShowDialog(Temp_TVScraperSeasonTitleBlacklist) = DialogResult.OK Then
            Temp_TVScraperSeasonTitleBlacklist = frmTV_Information_SeasonTitleBlacklist.Result
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnTVSourceAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourceAdd.Click
        'Using dSource As New dlgSource_TVShow
        '    If dSource.ShowDialog = DialogResult.OK Then
        '        RefreshTVSources()
        '        Button_Apply_SetEnabled(True)
        '        '_Results.NeedsDBUpdate_TV = True
        '    End If
        'End Using
    End Sub

    Private Sub btnFileSystemCleanerWhitelistAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemCleanerWhitelistAdd.Click
        If Not String.IsNullOrEmpty(txtFileSystemCleanerWhitelist.Text) Then
            If Not txtFileSystemCleanerWhitelist.Text.Substring(0, 1) = "." Then txtFileSystemCleanerWhitelist.Text = String.Concat(".", txtFileSystemCleanerWhitelist.Text)
            If Not lstFileSystemCleanerWhitelist.Items.Contains(txtFileSystemCleanerWhitelist.Text.ToLower) Then
                lstFileSystemCleanerWhitelist.Items.Add(txtFileSystemCleanerWhitelist.Text.ToLower)
                Button_Apply_SetEnabled(True)
                txtFileSystemCleanerWhitelist.Text = String.Empty
                txtFileSystemCleanerWhitelist.Focus()
            End If
        End If
    End Sub

    Private Sub btnMovieBackdropsPathBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSourcesBackdropsFolderPathBrowse.Click
        With fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(552, "Select the folder where you wish to store your backdrops...")
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtMovieSourcesBackdropsFolderPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingClear.Click
        ClearTVShowMatching()
    End Sub

    Private Sub btnMovieFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterDown.Click
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex < (lstMovieFilters.Items.Count - 1) Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex + 2, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex)
                lstMovieFilters.SelectedIndex = iIndex + 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_Movie = True
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralCustomMarker1_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker1.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker1.BackColor = .Color
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker2_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker2.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker2.BackColor = .Color
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker3_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker3.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker3.BackColor = .Color
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieGeneralCustomMarker4_Click(sender As Object, e As EventArgs) Handles btnMovieGeneralCustomMarker4.Click
        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    btnMovieGeneralCustomMarker4.BackColor = .Color
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieScraperDefFIExtEdit.Click, lstMovieScraperDefFIExt.DoubleClick
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each tMetadata As Settings.MetadataPerType In MovieMeta
                If tMetadata.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    Using dFileInfo As New dlgFileInfo(tMetadata.MetaData)
                        If dFileInfo.ShowDialog = DialogResult.OK Then
                            tMetadata.MetaData = dFileInfo.Result
                            LoadMovieMetadata()
                            Button_Apply_SetEnabled(True)
                        End If
                    End Using
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingEdit.Click
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub btnMovieSourceEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSourceEdit.Click
        'If lvMovieSources.SelectedItems.Count > 0 Then
        '    Using dMovieSource As New dlgSource_Movie
        '        If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = DialogResult.OK Then
        '            RefreshMovieSources()
        '            _Results.NeedsReload_Movie = True 'TODO: Check if we have to use Reload or DBUpdate
        '            Button_Apply_SetEnabled(True)
        '        End If
        '    End Using
        'End If
    End Sub

    Private Sub btnTVScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtEdit.Click, lstTVScraperDefFIExt.DoubleClick
        If Not txtTVScraperDefFIExt.Text.StartsWith(".") Then txtTVScraperDefFIExt.Text = String.Concat(".", txtTVScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.Fileinfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtTVScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    TVMeta.Add(m)
                    LoadTVMetadata()
                    txtTVScraperDefFIExt.Text = String.Empty
                    txtTVScraperDefFIExt.Focus()
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End Using
    End Sub

    Private Sub btnTVSourceEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourceEdit.Click
        'If lvTVSources.SelectedItems.Count > 0 Then
        '    Using dTVSource As New dlgSource_TVShow
        '        If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = DialogResult.OK Then
        '            RefreshTVSources()
        '            _Results.NeedsReload_TVShow = True
        '            Button_Apply_SetEnabled(True)
        '        End If
        '    End Using
        'End If
    End Sub

    Private Sub btnTVEpisodeFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterDown.Click
        Try
            If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso lstTVEpisodeFilter.SelectedIndex < (lstTVEpisodeFilter.Items.Count - 1) Then
                Dim iIndex As Integer = lstTVEpisodeFilter.SelectedIndices(0)
                lstTVEpisodeFilter.Items.Insert(iIndex + 2, lstTVEpisodeFilter.SelectedItems(0))
                lstTVEpisodeFilter.Items.RemoveAt(iIndex)
                lstTVEpisodeFilter.SelectedIndex = iIndex + 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_TVEpisode = True
                lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVEpisodeFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterUp.Click
        Try
            If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItem IsNot Nothing AndAlso lstTVEpisodeFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstTVEpisodeFilter.SelectedIndices(0)
                lstTVEpisodeFilter.Items.Insert(iIndex - 1, lstTVEpisodeFilter.SelectedItems(0))
                lstTVEpisodeFilter.Items.RemoveAt(iIndex + 1)
                lstTVEpisodeFilter.SelectedIndex = iIndex - 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_TVEpisode = True
                lstTVEpisodeFilter.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSourceAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSourceAdd.Click
        'Using dSource As New dlgSource_Movie
        '    If dSource.ShowDialog = DialogResult.OK Then
        '        RefreshMovieSources()
        '        Button_Apply_SetEnabled(True)
        '        '_Results.NeedsDBUpdate_Movie = True
        '    End If
        'End Using
    End Sub

    Private Sub btnMovieSourceRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSourceRemove.Click
        RemoveMovieSource()
    End Sub

    Private Sub btnMovieScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieScraperDefFIExtAdd.Click
        If Not txtMovieScraperDefFIExt.Text.StartsWith(".") Then txtMovieScraperDefFIExt.Text = String.Concat(".", txtMovieScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.Fileinfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtMovieScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    MovieMeta.Add(m)
                    LoadMovieMetadata()
                    txtMovieScraperDefFIExt.Text = String.Empty
                    txtMovieScraperDefFIExt.Focus()
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End Using
    End Sub

    Private Sub btnTVScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtAdd.Click
        If Not txtTVScraperDefFIExt.Text.StartsWith(".") Then txtTVScraperDefFIExt.Text = String.Concat(".", txtTVScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.Fileinfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtTVScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    TVMeta.Add(m)
                    LoadTVMetadata()
                    txtTVScraperDefFIExt.Text = String.Empty
                    txtTVScraperDefFIExt.Focus()
                    Button_Apply_SetEnabled(True)
                End If
            End If
        End Using
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingUp.Click
        Try
            If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso Not lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.Id = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    TVShowMatching.Remove(selItem)
                    TVShowMatching.Insert(iIndex - 1, selItem)

                    RenumberTVShowMatching()
                    LoadTVShowMatching()

                    lvTVSourcesRegexTVShowMatching.Items(selIndex - 1).Selected = True
                    lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingDown.Click
        Try
            If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index < (lvTVSourcesRegexTVShowMatching.Items.Count - 1) Then
                Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.Id = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVSourcesRegexTVShowMatching.SuspendLayout()
                    Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                    TVShowMatching.Remove(selItem)
                    TVShowMatching.Insert(iIndex + 1, selItem)

                    RenumberTVShowMatching()
                    LoadTVShowMatching()

                    lvTVSourcesRegexTVShowMatching.Items(selIndex + 1).Selected = True
                    lvTVSourcesRegexTVShowMatching.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVSourcesRegexTVShowMatching.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingUp.Click
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not lvMovieGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingUp.Click
        Try
            If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso Not lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    MovieSetGeneralMediaListSorting.Remove(selItem)
                    MovieSetGeneralMediaListSorting.Insert(iIndex - 1, selItem)

                    RenumberMovieSetGeneralMediaListSorting()
                    LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(selIndex - 3)
                    End If
                    lvMovieSetGeneralMediaListSorting.Items(selIndex - 1).Selected = True
                    lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingUp.Click
        Try
            If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralEpisodeListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    TVGeneralEpisodeListSorting.Remove(selItem)
                    TVGeneralEpisodeListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVEpisodeGeneralMediaListSorting()
                    LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralEpisodeListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingUp.Click
        Try
            If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralSeasonListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    TVGeneralSeasonListSorting.Remove(selItem)
                    TVGeneralSeasonListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVSeasonGeneralMediaListSorting()
                    LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralSeasonListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingUp.Click
        Try
            If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso Not lvTVGeneralShowListSorting.SelectedItems(0).Index = 0 Then
                Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)
                    TVGeneralShowListSorting.Remove(selItem)
                    TVGeneralShowListSorting.Insert(iIndex - 1, selItem)

                    RenumberTVShowGeneralMediaListSorting()
                    LoadTVGeneralShowListSorting()

                    If Not selIndex - 3 < 0 Then
                        lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(selIndex - 3)
                    End If
                    lvTVGeneralShowListSorting.Items(selIndex - 1).Selected = True
                    lvTVGeneralShowListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieGeneralMediaListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingDown.Click
        Try
            If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems(0).Index < (lvMovieGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)
                    MovieGeneralMediaListSorting.Remove(selItem)
                    MovieGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    RenumberMovieGeneralMediaListSorting()
                    LoadMovieGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    lvMovieGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    lvMovieGeneralMediaListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvMovieGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingDown.Click
        Try
            If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems(0).Index < (lvMovieSetGeneralMediaListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvMovieSetGeneralMediaListSorting.SuspendLayout()
                    Dim iIndex As Integer = MovieSetGeneralMediaListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)
                    MovieSetGeneralMediaListSorting.Remove(selItem)
                    MovieSetGeneralMediaListSorting.Insert(iIndex + 1, selItem)

                    RenumberMovieSetGeneralMediaListSorting()
                    LoadMovieSetGeneralMediaListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(selIndex - 2)
                    End If
                    lvMovieSetGeneralMediaListSorting.Items(selIndex + 1).Selected = True
                    lvMovieSetGeneralMediaListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvMovieSetGeneralMediaListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralEpisodeListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingDown.Click
        Try
            If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems(0).Index < (lvTVGeneralEpisodeListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralEpisodeListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralEpisodeListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)
                    TVGeneralEpisodeListSorting.Remove(selItem)
                    TVGeneralEpisodeListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVEpisodeGeneralMediaListSorting()
                    LoadTVGeneralEpisodeListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralEpisodeListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralEpisodeListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralEpisodeListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralSeasonListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingDown.Click
        Try
            If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems(0).Index < (lvTVGeneralSeasonListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralSeasonListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralSeasonListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)
                    TVGeneralSeasonListSorting.Remove(selItem)
                    TVGeneralSeasonListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVSeasonGeneralMediaListSorting()
                    LoadTVGeneralSeasonListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralSeasonListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralSeasonListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralSeasonListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVGeneralShowListSortingDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingDown.Click
        Try
            If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems(0).Index < (lvTVGeneralShowListSorting.Items.Count - 1) Then
                Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

                If selItem IsNot Nothing Then
                    lvTVGeneralShowListSorting.SuspendLayout()
                    Dim iIndex As Integer = TVGeneralShowListSorting.IndexOf(selItem)
                    Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)
                    TVGeneralShowListSorting.Remove(selItem)
                    TVGeneralShowListSorting.Insert(iIndex + 1, selItem)

                    RenumberTVShowGeneralMediaListSorting()
                    LoadTVGeneralShowListSorting()

                    If Not selIndex - 2 < 0 Then
                        lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(selIndex - 2)
                    End If
                    lvTVGeneralShowListSorting.Items(selIndex + 1).Selected = True
                    lvTVGeneralShowListSorting.ResumeLayout()
                End If

                Button_Apply_SetEnabled(True)
                lvTVGeneralShowListSorting.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvMovieGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieGeneralMediaListSorting.MouseDoubleClick
        If lvMovieGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = MovieGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieGeneralMediaListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvMovieGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvMovieGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = lvMovieGeneralMediaListSorting.SelectedIndices(0)

                LoadMovieGeneralMediaListSorting()

                lvMovieGeneralMediaListSorting.TopItem = lvMovieGeneralMediaListSorting.Items(topIndex)
                lvMovieGeneralMediaListSorting.Items(selIndex).Selected = True
                lvMovieGeneralMediaListSorting.ResumeLayout()
            End If

            Button_Apply_SetEnabled(True)
            lvMovieGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvMovieSetGeneralMediaListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvMovieSetGeneralMediaListSorting.MouseDoubleClick
        If lvMovieSetGeneralMediaListSorting.Items.Count > 0 AndAlso lvMovieSetGeneralMediaListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = MovieSetGeneralMediaListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvMovieSetGeneralMediaListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvMovieSetGeneralMediaListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvMovieSetGeneralMediaListSorting.TopItem.Index
                Dim selIndex As Integer = lvMovieSetGeneralMediaListSorting.SelectedIndices(0)

                LoadMovieSetGeneralMediaListSorting()

                lvMovieSetGeneralMediaListSorting.TopItem = lvMovieSetGeneralMediaListSorting.Items(topIndex)
                lvMovieSetGeneralMediaListSorting.Items(selIndex).Selected = True
                lvMovieSetGeneralMediaListSorting.ResumeLayout()
            End If

            Button_Apply_SetEnabled(True)
            lvMovieSetGeneralMediaListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralEpisodeListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralEpisodeListSorting.MouseDoubleClick
        If lvTVGeneralEpisodeListSorting.Items.Count > 0 AndAlso lvTVGeneralEpisodeListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralEpisodeListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralEpisodeListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralEpisodeListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralEpisodeListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralEpisodeListSorting.SelectedIndices(0)

                LoadTVGeneralEpisodeListSorting()

                lvTVGeneralEpisodeListSorting.TopItem = lvTVGeneralEpisodeListSorting.Items(topIndex)
                lvTVGeneralEpisodeListSorting.Items(selIndex).Selected = True
                lvTVGeneralEpisodeListSorting.ResumeLayout()
            End If

            Button_Apply_SetEnabled(True)
            lvTVGeneralEpisodeListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralSeasonListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralSeasonListSorting.MouseDoubleClick
        If lvTVGeneralSeasonListSorting.Items.Count > 0 AndAlso lvTVGeneralSeasonListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralSeasonListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralSeasonListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralSeasonListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralSeasonListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralSeasonListSorting.SelectedIndices(0)

                LoadTVGeneralSeasonListSorting()

                lvTVGeneralSeasonListSorting.TopItem = lvTVGeneralSeasonListSorting.Items(topIndex)
                lvTVGeneralSeasonListSorting.Items(selIndex).Selected = True
                lvTVGeneralSeasonListSorting.ResumeLayout()
            End If

            Button_Apply_SetEnabled(True)
            lvTVGeneralSeasonListSorting.Focus()
        End If
    End Sub

    Private Sub lvTVGeneralShowListSorting_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lvTVGeneralShowListSorting.MouseDoubleClick
        If lvTVGeneralShowListSorting.Items.Count > 0 AndAlso lvTVGeneralShowListSorting.SelectedItems.Count > 0 Then
            Dim selItem As Settings.ListSorting = TVGeneralShowListSorting.FirstOrDefault(Function(r) r.DisplayIndex = Convert.ToInt32(lvTVGeneralShowListSorting.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVGeneralShowListSorting.SuspendLayout()
                selItem.Hide = Not selItem.Hide
                Dim topIndex As Integer = lvTVGeneralShowListSorting.TopItem.Index
                Dim selIndex As Integer = lvTVGeneralShowListSorting.SelectedIndices(0)

                LoadTVGeneralShowListSorting()

                lvTVGeneralShowListSorting.TopItem = lvTVGeneralShowListSorting.Items(topIndex)
                lvTVGeneralShowListSorting.Items(selIndex).Selected = True
                lvTVGeneralShowListSorting.ResumeLayout()
            End If

            Button_Apply_SetEnabled(True)
            lvTVGeneralShowListSorting.Focus()
        End If
    End Sub

    Private Sub btnFileSystemValidVideoExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_FileSystemValidExts = Temp_FileSystemValidExts.GetDefaults
            RefreshFileSystemValidExts()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1283, "Are you sure you want to reset to the default list of valid subtitle extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_FileSystemValidSubtitlesExts = Temp_FileSystemValidSubtitlesExts.GetDefaults
            RefreshFileSystemValidSubtitlesExts()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnFileSystemValidThemeExtsReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsReset.Click
        If MessageBox.Show(Master.eLang.GetString(1080, "Are you sure you want to reset to the default list of valid theme extensions?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_FileSystemValidThemeExts = Temp_FileSystemValidThemeExts.GetDefaults
            RefreshFileSystemValidThemeExts()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnMovieFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(842, "Are you sure you want to reset to the default list of movie filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_MovieFilterCustom = Temp_MovieFilterCustom.GetDefaults
            RefreshMovieFilters()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnTVEpisodeFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(841, "Are you sure you want to reset to the default list of episode filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_TVEpisodeFilterCustom = Temp_TVEpisodeFilterCustom.GetDefaults
            RefreshTVEpisodeFilters()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnTVShowFilterReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterReset.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default list of show filters?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Temp_TVShowFilterCustom = Temp_TVShowFilterCustom.GetDefaults
            RefreshTVShowFilters()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingGet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingGet.Click
        Using dd As New dlgTVRegExProfiles
            If dd.ShowDialog() = DialogResult.OK Then
                TVShowMatching.Clear()
                TVShowMatching.AddRange(dd.ShowRegex)
                LoadTVShowMatching()
                Button_Apply_SetEnabled(True)
            End If
        End Using
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingReset.Click
        If MessageBox.Show(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowMatching, True)
            TVShowMatching.Clear()
            TVShowMatching.AddRange(Master.eSettings.TVShowMatching)
            LoadTVShowMatching()
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnTVSourcesRegexMultiPartMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexMultiPartMatchingReset.Click
        txtTVSourcesRegexMultiPartMatching.Text = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnMovieGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ListSorting_Movie, True)
        MovieGeneralMediaListSorting.Clear()
        MovieGeneralMediaListSorting.AddRange(Master.eSettings.MovieGeneralMediaListSorting)
        LoadMovieGeneralMediaListSorting()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnMovieSetGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetGeneralMediaListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ListSorting_Movieset, True)
        MovieSetGeneralMediaListSorting.Clear()
        MovieSetGeneralMediaListSorting.AddRange(Master.eSettings.MovieSetGeneralMediaListSorting)
        LoadMovieSetGeneralMediaListSorting()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnTVEpisodeGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralEpisodeListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ListSorting_TVEpisode, True)
        TVGeneralEpisodeListSorting.Clear()
        TVGeneralEpisodeListSorting.AddRange(Master.eSettings.TVGeneralEpisodeListSorting)
        LoadTVGeneralEpisodeListSorting()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnTVSeasonGeneralMediaListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralSeasonListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ListSorting_TVSeason, True)
        TVGeneralSeasonListSorting.Clear()
        TVGeneralSeasonListSorting.AddRange(Master.eSettings.TVGeneralSeasonListSorting)
        LoadTVGeneralSeasonListSorting()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnTVGeneralShowListSortingReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVGeneralShowListSortingReset.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.ListSorting_TVShow, True)
        TVGeneralShowListSorting.Clear()
        TVGeneralShowListSorting.AddRange(Master.eSettings.TVGeneralShowListSorting)
        LoadTVGeneralShowListSorting()
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnFileSystemValidExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidVideoExtsRemove.Click
        RemoveFileSystemValidExts()
    End Sub

    Private Sub btnFileSystemValidSubtitlesExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidSubtitlesExtsRemove.Click
        RemoveFileSystemValidSubtitlesExts()
    End Sub

    Private Sub btnFileSystemValidThemeExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemValidThemeExtsRemove.Click
        RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub btnTVEpisodeFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVEpisodeFilterRemove.Click
        RemoveTVEpisodeFilter()
    End Sub

    Private Sub btnMovieFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterRemove.Click
        RemoveMovieFilter()
    End Sub

    Private Sub btnMovieScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieScraperDefFIExtRemove.Click
        RemoveMovieMetaData()
    End Sub

    Private Sub btnFileSystemNoStackExtsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemNoStackExtsRemove.Click
        RemoveFileSystemNoStackExts()
    End Sub

    Private Sub btnTVShowFilterRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterRemove.Click
        RemoveTVShowFilter()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVSourcesRegexTVShowMatchingRemove.Click
        RemoveTVShowMatching()
    End Sub

    Private Sub btnGeneralSortTokenRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGeneralSortTokenRemove.Click
        RemoveGeneralSortToken()
    End Sub

    Private Sub btnTVScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVScraperDefFIExtRemove.Click
        RemoveTVMetaData()
    End Sub

    Private Sub btnFileSystemCleanerWhitelistRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileSystemCleanerWhitelistRemove.Click
        If lstFileSystemCleanerWhitelist.Items.Count > 0 AndAlso lstFileSystemCleanerWhitelist.SelectedItems.Count > 0 Then
            While lstFileSystemCleanerWhitelist.SelectedItems.Count > 0
                lstFileSystemCleanerWhitelist.Items.Remove(lstFileSystemCleanerWhitelist.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub btnRemTVSource_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemTVSource.Click
        RemoveTVSource()
    End Sub

    Private Sub btnTVShowFilterDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterDown.Click
        Try
            If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItem IsNot Nothing AndAlso lstTVShowFilter.SelectedIndex < (lstTVShowFilter.Items.Count - 1) Then
                Dim iIndex As Integer = lstTVShowFilter.SelectedIndices(0)
                lstTVShowFilter.Items.Insert(iIndex + 2, lstTVShowFilter.SelectedItems(0))
                lstTVShowFilter.Items.RemoveAt(iIndex)
                lstTVShowFilter.SelectedIndex = iIndex + 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_TVShow = True
                lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnTVShowFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTVShowFilterUp.Click
        Try
            If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItem IsNot Nothing AndAlso lstTVShowFilter.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstTVShowFilter.SelectedIndices(0)
                lstTVShowFilter.Items.Insert(iIndex - 1, lstTVShowFilter.SelectedItems(0))
                lstTVShowFilter.Items.RemoveAt(iIndex + 1)
                lstTVShowFilter.SelectedIndex = iIndex - 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_TVShow = True
                lstTVShowFilter.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub


    Private Sub btnMovieFilterUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieFilterUp.Click
        Try
            If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItem IsNot Nothing AndAlso lstMovieFilters.SelectedIndex > 0 Then
                Dim iIndex As Integer = lstMovieFilters.SelectedIndices(0)
                lstMovieFilters.Items.Insert(iIndex - 1, lstMovieFilters.SelectedItems(0))
                lstMovieFilters.Items.RemoveAt(iIndex + 1)
                lstMovieFilters.SelectedIndex = iIndex - 1
                Button_Apply_SetEnabled(True)
                _Results.NeedsReload_Movie = True
                lstMovieFilters.Focus()
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub cbGeneralLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbGeneralLanguage.SelectedIndexChanged
        If Not cbGeneralLanguage.SelectedItem.ToString = Master.eLang.Translations.FirstOrDefault(Function(f) f.Language = Master.eSettings.GeneralLanguage).Description Then
            Handle_NeedsRestart()
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub cbMovieTrailerPrefVideoQual_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMovieTrailerPrefVideoQual.SelectedIndexChanged
        If CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value = Enums.VideoResolution.Any Then
            cbMovieTrailerMinVideoQual.Enabled = False
        Else
            cbMovieTrailerMinVideoQual.Enabled = True
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub CheckHideSettings()
        If chkGeneralDisplayBanner.Checked OrElse chkGeneralDisplayCharacterArt.Checked OrElse chkGeneralDisplayClearArt.Checked OrElse chkGeneralDisplayClearLogo.Checked OrElse
              chkGeneralDisplayDiscArt.Checked OrElse chkGeneralDisplayFanart.Checked OrElse chkGeneralDisplayFanartSmall.Checked OrElse chkGeneralDisplayLandscape.Checked OrElse chkGeneralDisplayPoster.Checked Then
            chkGeneralImagesGlassOverlay.Enabled = True
            chkGeneralDisplayImgDims.Enabled = True
            chkGeneralDisplayImgNames.Enabled = True
        Else
            chkGeneralImagesGlassOverlay.Enabled = False
            chkGeneralDisplayImgDims.Enabled = False
            chkGeneralDisplayImgNames.Enabled = False
        End If
    End Sub

    Private Sub chkMovieClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieClickScrape.CheckedChanged
        chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkTVGeneralClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVGeneralClickScrape.CheckedChanged
        chkTVGeneralClickScrapeAsk.Enabled = chkTVGeneralClickScrape.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkMovieScraperStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperStudio.CheckedChanged
        Button_Apply_SetEnabled(True)
        chkMovieScraperStudioWithImg.Enabled = chkMovieScraperStudio.Checked
        txtMovieScraperStudioLimit.Enabled = chkMovieScraperStudio.Checked
        If Not chkMovieScraperStudio.Checked Then
            chkMovieScraperStudioWithImg.Checked = False
            txtMovieScraperStudioLimit.Text = "0"
        End If
    End Sub

    Private Sub chkTVScraperShowStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowStudio.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtTVScraperShowStudioLimit.Enabled = chkTVScraperShowStudio.Checked
        If Not chkTVScraperShowStudio.Checked Then txtTVScraperShowStudioLimit.Text = "0"
    End Sub


    Private Sub chkMovieScraperCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperCountry.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtMovieScraperCountryLimit.Enabled = chkMovieScraperCountry.Checked
        If Not chkMovieScraperCountry.Checked Then txtMovieScraperCountryLimit.Text = "0"
    End Sub

    Private Sub chkTVScraperShowCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowCountry.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtTVScraperShowCountryLimit.Enabled = chkTVScraperShowCountry.Checked
        If Not chkTVScraperShowCountry.Checked Then txtTVScraperShowCountryLimit.Text = "0"
    End Sub

    Private Sub chkMovieScraperCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperCast.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieScraperCastWithImg.Enabled = chkMovieScraperCast.Checked
        txtMovieScraperCastLimit.Enabled = chkMovieScraperCast.Checked

        If Not chkMovieScraperCast.Checked Then
            chkMovieScraperCastWithImg.Checked = False
            txtMovieScraperCastLimit.Text = "0"
        End If
    End Sub
    Private Sub TVScraperActors_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperEpisodeActors.CheckedChanged, chkTVScraperEpisodeGuestStars.CheckedChanged, chkTVScraperShowActors.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVScraperCastWithImg.Enabled = chkTVScraperEpisodeActors.Checked OrElse chkTVScraperEpisodeGuestStars.Checked OrElse chkTVScraperShowActors.Checked
        txtTVScraperEpisodeActorsLimit.Enabled = chkTVScraperEpisodeActors.Checked
        txtTVScraperEpisodeGuestStarsLimit.Enabled = chkTVScraperEpisodeGuestStars.Checked
        txtTVScraperShowActorsLimit.Enabled = chkTVScraperShowActors.Checked

        If Not chkTVScraperEpisodeActors.Checked AndAlso
            Not chkTVScraperEpisodeGuestStars.Checked AndAlso
            Not chkTVScraperShowActors.Checked Then
            chkTVScraperCastWithImg.Checked = False
        End If

        If Not chkTVScraperEpisodeActors.Checked Then txtTVScraperEpisodeActorsLimit.Text = "0"
        If Not chkTVScraperEpisodeGuestStars.Checked Then txtTVScraperEpisodeGuestStarsLimit.Text = "0"
        If Not chkTVScraperShowActors.Checked Then txtTVScraperShowActorsLimit.Text = "0"
    End Sub

    Private Sub chkMovieScraperCert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperCert.CheckedChanged
        Button_Apply_SetEnabled(True)

        If Not chkMovieScraperCert.Checked Then
            cbMovieScraperCertCountry.Enabled = False
            cbMovieScraperCertCountry.SelectedIndex = 0
            chkMovieScraperCertForMPAA.Enabled = False
            chkMovieScraperCertForMPAA.Checked = False
            chkMovieScraperCertOnlyValue.Enabled = False
            chkMovieScraperCertOnlyValue.Checked = False
        Else
            cbMovieScraperCertCountry.Enabled = True
            cbMovieScraperCertCountry.SelectedIndex = 0
            chkMovieScraperCertForMPAA.Enabled = True
            chkMovieScraperCertOnlyValue.Enabled = True
        End If
    End Sub

    Private Sub chkTVScraperShowCert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowCert.CheckedChanged
        Button_Apply_SetEnabled(True)

        If Not chkTVScraperShowCert.Checked Then
            cbTVScraperShowCertCountry.Enabled = False
            cbTVScraperShowCertCountry.SelectedIndex = 0
            chkTVScraperShowCertForMPAA.Enabled = False
            chkTVScraperShowCertForMPAA.Checked = False
            chkTVScraperShowCertOnlyValue.Enabled = False
            chkTVScraperShowCertOnlyValue.Checked = False
        Else
            cbTVScraperShowCertCountry.Enabled = True
            cbTVScraperShowCertCountry.SelectedIndex = 0
            chkTVScraperShowCertForMPAA.Enabled = True
            chkTVScraperShowCertOnlyValue.Enabled = True
        End If
    End Sub

    Private Sub chkMovieScraperCertForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperCertForMPAA.CheckedChanged
        Button_Apply_SetEnabled(True)

        If Not chkMovieScraperCertForMPAA.Checked Then
            chkMovieScraperCertForMPAAFallback.Enabled = False
            chkMovieScraperCertForMPAAFallback.Checked = False
        Else
            chkMovieScraperCertForMPAAFallback.Enabled = True
        End If
    End Sub

    Private Sub chkTVScraperShowCertForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowCertForMPAA.CheckedChanged
        Button_Apply_SetEnabled(True)

        If Not chkTVScraperShowCertForMPAA.Checked Then
            chkTVScraperShowCertForMPAAFallback.Enabled = False
            chkTVScraperShowCertForMPAAFallback.Checked = False
        Else
            chkTVScraperShowCertForMPAAFallback.Enabled = True
        End If
    End Sub
    Private Sub chkMovieLevTolerance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieLevTolerance.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieLevTolerance.Enabled = chkMovieLevTolerance.Checked
        If Not chkMovieLevTolerance.Checked Then txtMovieLevTolerance.Text = String.Empty
    End Sub

    Private Sub chkProxyCredsEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkProxyCredsEnable.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtProxyUsername.Enabled = chkProxyCredsEnable.Checked
        txtProxyPassword.Enabled = chkProxyCredsEnable.Checked
        txtProxyDomain.Enabled = chkProxyCredsEnable.Checked

        If Not chkProxyCredsEnable.Checked Then
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkProxyEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkProxyEnable.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtProxyURI.Enabled = chkProxyEnable.Checked
        txtProxyPort.Enabled = chkProxyEnable.Checked
        gbProxyCredsOpts.Enabled = chkProxyEnable.Checked

        If Not chkProxyEnable.Checked Then
            txtProxyURI.Text = String.Empty
            txtProxyPort.Text = String.Empty
            chkProxyCredsEnable.Checked = False
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeProperCase.CheckedChanged
        Button_Apply_SetEnabled(True)
        _Results.NeedsReload_TVEpisode = True
    End Sub


    Private Sub chkMovieScraperGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperGenre.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtMovieScraperGenreLimit.Enabled = chkMovieScraperGenre.Checked
        If Not chkMovieScraperGenre.Checked Then txtMovieScraperGenreLimit.Text = "0"
    End Sub


    Private Sub chkTVScraperShowGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowGenre.CheckedChanged
        Button_Apply_SetEnabled(True)
        txtTVScraperShowGenreLimit.Enabled = chkTVScraperShowGenre.Checked
        If Not chkTVScraperShowGenre.Checked Then txtTVScraperShowGenreLimit.Text = "0"
    End Sub

    Private Sub chkGeneralDisplayBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayBanner.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayCharacterArt.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayClearArt.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayClearLogo.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayDiscArt.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayFanart.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayLandscape.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayPoster.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanartSmall_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGeneralDisplayFanartSmall.CheckedChanged
        Button_Apply_SetEnabled(True)
        CheckHideSettings()
    End Sub

    Private Sub chkTVEpisodeNoFilter_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeNoFilter.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVEpisodeProperCase.Enabled = Not chkTVEpisodeNoFilter.Checked
        lstTVEpisodeFilter.Enabled = Not chkTVEpisodeNoFilter.Checked
        txtTVEpisodeFilter.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterAdd.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterUp.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterDown.Enabled = Not chkTVEpisodeNoFilter.Checked
        btnTVEpisodeFilterRemove.Enabled = Not chkTVEpisodeNoFilter.Checked
    End Sub

    Private Sub chkMovieScraperPlotForOutline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperPlotForOutline.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieScraperOutlineLimit.Enabled = chkMovieScraperPlotForOutline.Checked
        chkMovieScraperPlotForOutlineIfEmpty.Enabled = chkMovieScraperPlotForOutline.Checked
        If Not chkMovieScraperPlotForOutline.Checked Then
            txtMovieScraperOutlineLimit.Enabled = False
            chkMovieScraperPlotForOutlineIfEmpty.Checked = False
            chkMovieScraperPlotForOutlineIfEmpty.Enabled = False
        End If
    End Sub

    Private Sub chkMovieScraperTrailer_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperTrailer.CheckedChanged
        Button_Apply_SetEnabled(True)
        chkMovieScraperTrailerFromTrailerScrapers.Enabled = chkMovieScraperTrailer.Checked
        If Not chkMovieScraperTrailer.Checked Then chkMovieScraperTrailerFromTrailerScrapers.Checked = False
    End Sub

    Private Sub chkMovieScraperOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieScraperOriginalTitle.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieScraperOriginalTitleAsTitle.Enabled = chkMovieScraperOriginalTitle.Checked
        If Not chkMovieScraperOriginalTitle.Checked Then chkMovieScraperOriginalTitleAsTitle.Checked = False
    End Sub

    Private Sub chkTVScraperShowOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVScraperShowOriginalTitle.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVScraperShowOriginalTitleAsTitle.Enabled = chkTVScraperShowOriginalTitle.Checked
        If Not chkTVScraperShowOriginalTitle.Checked Then chkTVScraperShowOriginalTitleAsTitle.Checked = False
    End Sub

    Private Sub chkMovieScraperPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperPlot.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieScraperPlotForOutline.Enabled = chkMovieScraperPlot.Checked
        If Not chkMovieScraperPlot.Checked Then
            chkMovieScraperPlotForOutline.Checked = False
            txtMovieScraperOutlineLimit.Enabled = False
        End If
    End Sub

    Private Sub chkMovieProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieProperCase.CheckedChanged
        Button_Apply_SetEnabled(True)
        _Results.NeedsReload_Movie = True
    End Sub

    Private Sub chkTVAllSeasonsBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsBannerResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVAllSeasonsBannerWidth.Enabled = chkTVAllSeasonsBannerResize.Checked
        txtTVAllSeasonsBannerHeight.Enabled = chkTVAllSeasonsBannerResize.Checked

        If Not chkTVAllSeasonsBannerResize.Checked Then
            txtTVAllSeasonsBannerWidth.Text = String.Empty
            txtTVAllSeasonsBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsFanartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVAllSeasonsFanartWidth.Enabled = chkTVAllSeasonsFanartResize.Checked
        txtTVAllSeasonsFanartHeight.Enabled = chkTVAllSeasonsFanartResize.Checked

        If Not chkTVAllSeasonsFanartResize.Checked Then
            txtTVAllSeasonsFanartWidth.Text = String.Empty
            txtTVAllSeasonsFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVAllSeasonsPosterResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVAllSeasonsPosterWidth.Enabled = chkTVAllSeasonsPosterResize.Checked
        txtTVAllSeasonsPosterHeight.Enabled = chkTVAllSeasonsPosterResize.Checked

        If Not chkTVAllSeasonsPosterResize.Checked Then
            txtTVAllSeasonsPosterWidth.Text = String.Empty
            txtTVAllSeasonsPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodeFanartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVEpisodeFanartWidth.Enabled = chkTVEpisodeFanartResize.Checked
        txtTVEpisodeFanartHeight.Enabled = chkTVEpisodeFanartResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodeFanartWidth.Text = String.Empty
            txtTVEpisodeFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVEpisodePosterResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVEpisodePosterWidth.Enabled = chkTVEpisodePosterResize.Checked
        txtTVEpisodePosterHeight.Enabled = chkTVEpisodePosterResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodePosterWidth.Text = String.Empty
            txtTVEpisodePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowExtrafanartsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowExtrafanartsResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVShowExtrafanartsWidth.Enabled = chkTVShowExtrafanartsResize.Checked
        txtTVShowExtrafanartsHeight.Enabled = chkTVShowExtrafanartsResize.Checked

        If Not chkTVShowExtrafanartsResize.Checked Then
            txtTVShowExtrafanartsWidth.Text = String.Empty
            txtTVShowExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetBannerResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieSetBannerWidth.Enabled = chkMovieSetBannerResize.Checked
        txtMovieSetBannerHeight.Enabled = chkMovieSetBannerResize.Checked

        If Not chkMovieSetBannerResize.Checked Then
            txtMovieSetBannerWidth.Text = String.Empty
            txtMovieSetBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetFanartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieSetFanartWidth.Enabled = chkMovieSetFanartResize.Checked
        txtMovieSetFanartHeight.Enabled = chkMovieSetFanartResize.Checked

        If Not chkMovieSetFanartResize.Checked Then
            txtMovieSetFanartWidth.Text = String.Empty
            txtMovieSetFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetPosterResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieSetPosterWidth.Enabled = chkMovieSetPosterResize.Checked
        txtMovieSetPosterHeight.Enabled = chkMovieSetPosterResize.Checked

        If Not chkMovieSetPosterResize.Checked Then
            txtMovieSetPosterWidth.Text = String.Empty
            txtMovieSetPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetKeyartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetKeyartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtMovieSetKeyartWidth.Enabled = chkMovieSetKeyartResize.Checked
        txtMovieSetKeyartHeight.Enabled = chkMovieSetKeyartResize.Checked

        If Not chkMovieSetKeyartResize.Checked Then
            txtMovieSetKeyartWidth.Text = String.Empty
            txtMovieSetKeyartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowBannerResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVShowBannerWidth.Enabled = chkTVShowBannerResize.Checked
        txtTVShowBannerHeight.Enabled = chkTVShowBannerResize.Checked

        If Not chkTVShowBannerResize.Checked Then
            txtTVShowBannerWidth.Text = String.Empty
            txtTVShowBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowFanartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVShowFanartWidth.Enabled = chkTVShowFanartResize.Checked
        txtTVShowFanartHeight.Enabled = chkTVShowFanartResize.Checked

        If Not chkTVShowFanartResize.Checked Then
            txtTVShowFanartWidth.Text = String.Empty
            txtTVShowFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowPosterResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVShowPosterWidth.Enabled = chkTVShowPosterResize.Checked
        txtTVShowPosterHeight.Enabled = chkTVShowPosterResize.Checked

        If Not chkTVShowPosterResize.Checked Then
            txtTVShowPosterWidth.Text = String.Empty
            txtTVShowPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowKeyartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowKeyartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVShowKeyartWidth.Enabled = chkTVShowKeyartResize.Checked
        txtTVShowKeyartHeight.Enabled = chkTVShowKeyartResize.Checked

        If Not chkTVShowKeyartResize.Checked Then
            txtTVShowKeyartWidth.Text = String.Empty
            txtTVShowKeyartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVScraperShowRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperShowRuntime.CheckedChanged
        chkTVScraperUseSRuntimeForEp.Enabled = chkTVScraperShowRuntime.Checked
        If Not chkTVScraperShowRuntime.Checked Then
            chkTVScraperUseSRuntimeForEp.Checked = False
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkTVSeasonbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonBannerResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVSeasonBannerWidth.Enabled = chkTVSeasonBannerResize.Checked
        txtTVSeasonBannerHeight.Enabled = chkTVSeasonBannerResize.Checked

        If Not chkTVSeasonBannerResize.Checked Then
            txtTVSeasonBannerWidth.Text = String.Empty
            txtTVSeasonBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonFanartResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVSeasonFanartWidth.Enabled = chkTVSeasonFanartResize.Checked
        txtTVSeasonFanartHeight.Enabled = chkTVSeasonFanartResize.Checked

        If Not chkTVSeasonFanartResize.Checked Then
            txtTVSeasonFanartWidth.Text = String.Empty
            txtTVSeasonFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVSeasonPosterResize.CheckedChanged
        Button_Apply_SetEnabled(True)

        txtTVSeasonPosterWidth.Enabled = chkTVSeasonPosterResize.Checked
        txtTVSeasonPosterHeight.Enabled = chkTVSeasonPosterResize.Checked

        If Not chkTVSeasonPosterResize.Checked Then
            txtTVSeasonPosterWidth.Text = String.Empty
            txtTVSeasonPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowProperCase.CheckedChanged
        Button_Apply_SetEnabled(True)
        _Results.NeedsReload_TVShow = True
    End Sub

    Private Sub chkMovieScraperCollectionID_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperCollectionID.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieScraperCollectionsAuto.Enabled = chkMovieScraperCollectionID.Checked
        If Not chkMovieScraperCollectionID.Checked Then
            chkMovieScraperCollectionsAuto.Checked = False
        End If
    End Sub

    Private Sub chkTVScraperMetaDataScan_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperMetaDataScan.CheckedChanged
        Button_Apply_SetEnabled(True)

        cbTVLanguageOverlay.Enabled = chkTVScraperMetaDataScan.Checked

        If Not chkTVScraperMetaDataScan.Checked Then
            cbTVLanguageOverlay.SelectedIndex = 0
        End If
    End Sub

    Private Sub chkMovieUseAD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseAD.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseBoxee.CheckedChanged
        Button_Apply_SetEnabled(True)

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
        Button_Apply_SetEnabled(True)

        chkMovieBannerExtended.Enabled = chkMovieUseExtended.Checked
        chkMovieClearArtExtended.Enabled = chkMovieUseExtended.Checked
        chkMovieClearLogoExtended.Enabled = chkMovieUseExtended.Checked
        chkMovieDiscArtExtended.Enabled = chkMovieUseExtended.Checked
        chkMovieKeyartExtended.Enabled = chkMovieUseExtended.Checked
        chkMovieLandscapeExtended.Enabled = chkMovieUseExtended.Checked

        If Not chkMovieUseExtended.Checked Then
            chkMovieBannerExtended.Checked = False
            chkMovieClearArtExtended.Checked = False
            chkMovieClearLogoExtended.Checked = False
            chkMovieDiscArtExtended.Checked = False
            chkMovieKeyartExtended.Checked = False
            chkMovieLandscapeExtended.Checked = False
        Else
            chkMovieBannerExtended.Checked = True
            chkMovieClearArtExtended.Checked = True
            chkMovieClearLogoExtended.Checked = True
            chkMovieDiscArtExtended.Checked = True
            chkMovieKeyartExtended.Checked = True
            chkMovieLandscapeExtended.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseFrodo.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieActorThumbsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieExtrafanartsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieExtrathumbsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieFanartFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieNFOFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMoviePosterFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieTrailerFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieXBMCProtectVTSBDMV.Enabled = chkMovieUseFrodo.Checked AndAlso Not chkMovieUseEden.Checked

        If Not chkMovieUseFrodo.Checked Then
            chkMovieActorThumbsFrodo.Checked = False
            chkMovieExtrafanartsFrodo.Checked = False
            chkMovieExtrathumbsFrodo.Checked = False
            chkMovieFanartFrodo.Checked = False
            chkMovieNFOFrodo.Checked = False
            chkMoviePosterFrodo.Checked = False
            chkMovieTrailerFrodo.Checked = False
            chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            chkMovieActorThumbsFrodo.Checked = True
            chkMovieExtrafanartsFrodo.Checked = True
            chkMovieExtrathumbsFrodo.Checked = True
            chkMovieFanartFrodo.Checked = True
            chkMovieNFOFrodo.Checked = True
            chkMoviePosterFrodo.Checked = True
            chkMovieTrailerFrodo.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseEden_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseEden.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkMovieUseYAMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseYAMJ.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseNMJ.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkMovieSetUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseExtended.CheckedChanged
        Button_Apply_SetEnabled(True)

        btnMovieSetPathExtendedBrowse.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetBannerExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetClearArtExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetClearLogoExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetDiscArtExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetFanartExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetKeyartExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetLandscapeExtended.Enabled = chkMovieSetUseExtended.Checked
        chkMovieSetPosterExtended.Enabled = chkMovieSetUseExtended.Checked
        txtMovieSetPathExtended.Enabled = chkMovieSetUseExtended.Checked

        If Not chkMovieSetUseExtended.Checked Then
            chkMovieSetBannerExtended.Checked = False
            chkMovieSetClearArtExtended.Checked = False
            chkMovieSetClearLogoExtended.Checked = False
            chkMovieSetDiscArtExtended.Checked = False
            chkMovieSetFanartExtended.Checked = False
            chkMovieSetKeyartExtended.Checked = False
            chkMovieSetLandscapeExtended.Checked = False
            chkMovieSetPosterExtended.Checked = False
        Else
            chkMovieSetBannerExtended.Checked = True
            chkMovieSetClearArtExtended.Checked = True
            chkMovieSetClearLogoExtended.Checked = True
            chkMovieSetDiscArtExtended.Checked = True
            chkMovieSetFanartExtended.Checked = True
            chkMovieSetKeyartExtended.Checked = True
            chkMovieSetLandscapeExtended.Checked = True
            chkMovieSetPosterExtended.Checked = True
        End If
    End Sub

    Private Sub chkMovieSetUseMatrix_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseMatrix.CheckedChanged
        Button_Apply_SetEnabled(True)

        btnMovieSetPathMatrixBrowse.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetBannerMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetClearArtMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetClearLogoMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetDiscArtMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetFanartMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetKeyartMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetLandscapeMatrix.Enabled = chkMovieSetUseMatrix.Checked
        chkMovieSetPosterMatrix.Enabled = chkMovieSetUseMatrix.Checked
        txtMovieSetPathMatrix.Enabled = chkMovieSetUseMatrix.Checked

        If Not chkMovieSetUseMatrix.Checked Then
            chkMovieSetBannerMatrix.Checked = False
            chkMovieSetClearArtMatrix.Checked = False
            chkMovieSetClearLogoMatrix.Checked = False
            chkMovieSetDiscArtMatrix.Checked = False
            chkMovieSetFanartMatrix.Checked = False
            chkMovieSetKeyartMatrix.Checked = False
            chkMovieSetLandscapeMatrix.Checked = False
            chkMovieSetPosterMatrix.Checked = False
        Else
            chkMovieSetBannerMatrix.Checked = True
            chkMovieSetClearArtMatrix.Checked = True
            chkMovieSetClearLogoMatrix.Checked = True
            chkMovieSetDiscArtMatrix.Checked = True
            chkMovieSetFanartMatrix.Checked = True
            chkMovieSetKeyartMatrix.Checked = True
            chkMovieSetLandscapeMatrix.Checked = True
            chkMovieSetPosterMatrix.Checked = True
        End If
    End Sub

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseMSAA.CheckedChanged
        Button_Apply_SetEnabled(True)

        btnMovieSetPathMSAABrowse.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetBannerMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearArtMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearLogoMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetFanartMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetLandscapeMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetPosterMSAA.Enabled = chkMovieSetUseMSAA.Checked
        txtMovieSetPathMSAA.Enabled = chkMovieSetUseMSAA.Checked

        If Not chkMovieSetUseMSAA.Checked Then
            chkMovieSetBannerMSAA.Checked = False
            chkMovieSetClearArtMSAA.Checked = False
            chkMovieSetClearLogoMSAA.Checked = False
            chkMovieSetFanartMSAA.Checked = False
            chkMovieSetLandscapeMSAA.Checked = False
            chkMovieSetPosterMSAA.Checked = False
        Else
            chkMovieSetBannerMSAA.Checked = True
            chkMovieSetClearArtMSAA.Checked = True
            chkMovieSetClearLogoMSAA.Checked = True
            chkMovieSetFanartMSAA.Checked = True
            chkMovieSetLandscapeMSAA.Checked = True
            chkMovieSetPosterMSAA.Checked = True
        End If
    End Sub

    Private Sub chkMovieScraperUseMDDuration_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieScraperUseMDDuration.CheckedChanged
        txtMovieScraperDurationRuntimeFormat.Enabled = chkMovieScraperUseMDDuration.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkTVScraperUseMDDuration_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVScraperUseMDDuration.CheckedChanged
        txtTVScraperDurationRuntimeFormat.Enabled = chkTVScraperUseMDDuration.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkMovieThemeTvTunesCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieThemeTvTunesCustom.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkTVShowThemeTvTunesCustom_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesCustom.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkMovieThemeTvTunesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieThemeTvTunesEnabled.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkTVShowThemeTvTunesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesEnabled.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkMovieThemeTvTunesMoviePath_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieThemeTvTunesMoviePath.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkTVShowThemeTvTunesTVShowPath_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesShowPath.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkMovieThemeTvTunesSub_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieThemeTvTunesSub.CheckedChanged
        Button_Apply_SetEnabled(True)

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

    Private Sub chkTVShowThemeTvTunesSub_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVShowThemeTvTunesSub.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        txtMovieYAMJWatchedFolder.Enabled = chkMovieYAMJWatchedFile.Checked
        btnMovieYAMJWatchedFilesBrowse.Enabled = chkMovieYAMJWatchedFile.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub ClearTVShowMatching()
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(115, "Add Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        txtTVSourcesRegexTVShowMatchingRegex.Text = String.Empty
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = String.Empty
        chkTVSourcesRegexTVShowMatchingByDate.Checked = False
    End Sub

    Private Sub EditTVShowMatching(ByVal lItem As ListViewItem)
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(124, "Update Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = lItem.Text

        txtTVSourcesRegexTVShowMatchingRegex.Text = lItem.SubItems(1).Text.ToString
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = If(Not lItem.SubItems(2).Text = "-2", lItem.SubItems(2).Text, String.Empty)

        Select Case lItem.SubItems(3).Text
            Case "Yes"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = True
            Case "No"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = False
        End Select
    End Sub

    Private Sub FillMovieSetScraperTitleRenamer()
        For Each sett In Master.eSettings.MoviesetTitleRenaming
            Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {sett.Input, sett.MappedTo})
            If Not String.IsNullOrEmpty(sett.MappedTo) Then
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).ReadOnly = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).Style.SelectionForeColor = Drawing.Color.Red
            Else
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
            End If
        Next
        dgvMovieSetScraperTitleRenamer.ClearSelection()
    End Sub

    Private Sub SaveMovieSetScraperTitleRenamer()
        Master.eSettings.MoviesetTitleRenaming.Clear()
        For Each r As DataGridViewRow In dgvMovieSetScraperTitleRenamer.Rows
            If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) Then
                Master.eSettings.MoviesetTitleRenaming.Add(New SimpleMapping With {
                                                           .Input = r.Cells(0).Value.ToString,
                                                           .MappedTo = r.Cells(1).Value.ToString
                                                           })
            End If
        Next
    End Sub

    Private Sub Settings_Load()
        With Master.eSettings
            btnMovieGeneralCustomMarker1.BackColor = Color.FromArgb(.MovieGeneralCustomMarker1Color)
            btnMovieGeneralCustomMarker2.BackColor = Color.FromArgb(.MovieGeneralCustomMarker2Color)
            btnMovieGeneralCustomMarker3.BackColor = Color.FromArgb(.MovieGeneralCustomMarker3Color)
            btnMovieGeneralCustomMarker4.BackColor = Color.FromArgb(.MovieGeneralCustomMarker4Color)
            cbGeneralDateTime.SelectedValue = .GeneralDateTime
            Dim GeneralLanguage = Master.eLang.Translations.FirstOrDefault(Function(f) f.Language = .GeneralLanguage)
            If GeneralLanguage IsNot Nothing Then
                cbGeneralLanguage.SelectedItem = GeneralLanguage.Description
            Else
                'Translation does no longer exists, switch to default "en-US"
                Master.eSettings.GeneralLanguage = "en-US"
                cbGeneralLanguage.SelectedItem = Master.eLang.Translations.FirstOrDefault(Function(f) f.Language = "en-US").Description
            End If
            cbGeneralTheme.SelectedItem = .GeneralTheme
            cbMovieGeneralCustomScrapeButtonModifierType.SelectedValue = .MovieGeneralCustomScrapeButtonModifierType
            cbMovieGeneralCustomScrapeButtonScrapeType.SelectedValue = .MovieGeneralCustomScrapeButtonScrapeType
            cbMovieLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.MovieGeneralFlagLang), Master.eLang.CommonWordsList.Disabled, .MovieGeneralFlagLang)
            cbMovieSetGeneralCustomScrapeButtonModifierType.SelectedValue = .MovieSetGeneralCustomScrapeButtonModifierType
            cbMovieSetGeneralCustomScrapeButtonScrapeType.SelectedValue = .MovieSetGeneralCustomScrapeButtonScrapeType
            cbMovieTrailerMinVideoQual.SelectedValue = .MovieTrailerMinVideoQual
            cbMovieTrailerPrefVideoQual.SelectedValue = .MovieTrailerPrefVideoQual
            cbTVGeneralCustomScrapeButtonModifierType.SelectedValue = .TVGeneralCustomScrapeButtonModifierType
            cbTVGeneralCustomScrapeButtonScrapeType.SelectedValue = .TVGeneralCustomScrapeButtonScrapeType
            cbTVLanguageOverlay.SelectedItem = If(String.IsNullOrEmpty(.TVGeneralFlagLang), Master.eLang.CommonWordsList.Disabled, .TVGeneralFlagLang)
            cbTVScraperOptionsOrdering.SelectedValue = .TVScraperOptionsOrdering
            chkCleanDotFanartJPG.Checked = .CleanDotFanartJPG
            chkCleanExtrathumbs.Checked = .CleanExtrathumbs
            chkCleanFanartJPG.Checked = .CleanFanartJPG
            chkCleanFolderJPG.Checked = .CleanFolderJPG
            chkCleanMovieFanartJPG.Checked = .CleanMovieFanartJPG
            chkCleanMovieJPG.Checked = .CleanMovieJPG
            chkCleanMovieNFO.Checked = .CleanMovieNFO
            chkCleanMovieNFOb.Checked = .CleanMovieNFOB
            chkCleanMovieNameJPG.Checked = .CleanMovieNameJPG
            chkCleanMovieTBN.Checked = .CleanMovieTBN
            chkCleanMovieTBNb.Checked = .CleanMovieTBNB
            chkCleanPosterJPG.Checked = .CleanPosterJPG
            chkCleanPosterTBN.Checked = .CleanPosterTBN
            chkFileSystemCleanerWhitelist.Checked = .FileSystemCleanerWhitelist
            chkGeneralDateAddedIgnoreNFO.Checked = .GeneralDateAddedIgnoreNFO
            chkGeneralImageFilter.Checked = .GeneralImageFilter
            chkGeneralImageFilterAutoscraper.Checked = .GeneralImageFilterAutoscraper
            txtGeneralImageFilterFanartMatchRate.Enabled = .GeneralImageFilterFanart
            chkGeneralImageFilterFanart.Checked = .GeneralImageFilterFanart
            chkGeneralImageFilterImagedialog.Checked = .GeneralImageFilterImagedialog
            chkGeneralImageFilterPoster.Checked = .GeneralImageFilterPoster
            txtGeneralImageFilterPosterMatchRate.Enabled = .GeneralImageFilterPoster
            chkGeneralDoubleClickScrape.Checked = .GeneralDoubleClickScrape
            chkGeneralDisplayBanner.Checked = .GeneralDisplayBanner
            chkGeneralDisplayCharacterArt.Checked = .GeneralDisplayCharacterArt
            chkGeneralDisplayClearArt.Checked = .GeneralDisplayClearArt
            chkGeneralDisplayClearLogo.Checked = .GeneralDisplayClearLogo
            chkGeneralDisplayDiscArt.Checked = .GeneralDisplayDiscArt
            chkGeneralDisplayFanart.Checked = .GeneralDisplayFanartAsBackground
            chkGeneralDisplayFanartSmall.Checked = .GeneralDisplayFanart
            chkGeneralDisplayLandscape.Checked = .GeneralDisplayLandscape
            chkGeneralDisplayPoster.Checked = .GeneralDisplayPoster
            chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
            chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
            chkGeneralDisplayGenresText.Checked = .GeneralShowGenresText
            chkGeneralDisplayLangFlags.Checked = .GeneralShowLangFlags
            chkGeneralDisplayImgDims.Checked = .GeneralShowImgDims
            chkGeneralDisplayImgNames.Checked = .GeneralShowImgNames
            chkGeneralNotificationAdded_Movie.Checked = .GeneralNotificationAddedMovie
            chkGeneralNotificationAdded_MovieSet.Checked = .GeneralNotificationAddedMovieset
            chkGeneralNotificationAdded_TVEpisode.Checked = .GeneralNotificationAddedTVEpisode
            chkGeneralNotificationAdded_TVShow.Checked = .GeneralNotificationAddedTVShow
            chkGeneralNotificationError.Checked = .GeneralNotificationError
            chkGeneralNotificationInformation.Checked = .GeneralNotificationInformation
            chkGeneralNotificationScraped_Movie.Checked = .GeneralNotificationScrapedMovie
            chkGeneralNotificationScraped_MovieSet.Checked = .GeneralNotificationScrapedMovieset
            chkGeneralNotificationScraped_TVEpisode.Checked = .GeneralNotificationScrapedTVEpisode
            chkGeneralNotificationScraped_TVSeason.Checked = .GeneralNotificationScrapedTVSeason
            chkGeneralNotificationScraped_TVShow.Checked = .GeneralNotificationScrapedTVShow
            chkGeneralNotificationWarning.Checked = .GeneralNotificationWarning
            chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            chkMovieSourcesBackdropsAuto.Checked = .MovieBackdropsAuto
            chkMovieCleanDB.Checked = .MovieCleanDB
            chkMovieClickScrape.Checked = .MovieClickScrape
            chkMovieClickScrapeAsk.Checked = .MovieClickScrapeAsk
            chkMovieGeneralIgnoreLastScan.Checked = .MovieGeneralIgnoreLastScan
            chkMovieGeneralMarkNew.Checked = .MovieGeneralMarkNew
            chkMovieProperCase.Checked = .MovieProperCase
            chkMovieSetCleanDB.Checked = .MovieSetCleanDB
            chkMovieSetCleanFiles.Checked = .MovieSetCleanFiles
            chkMovieSetClickScrape.Checked = .MovieSetClickScrape
            chkMovieSetClickScrapeAsk.Checked = .MovieSetClickScrapeAsk
            chkMovieSetGeneralMarkNew.Checked = .MovieSetGeneralMarkNew
            chkMovieScanOrderModify.Checked = .MovieScanOrderModify
            chkMovieScraperMetaDataIFOScan.Checked = .MovieScraperMetaDataIFOScan
            chkMovieScraperMetaDataScan.Checked = .MovieScraperMetaDataScan
            chkMovieSkipStackedSizeCheck.Checked = .MovieSkipStackedSizeCheck
            chkMovieSortBeforeScan.Checked = .MovieSortBeforeScan
            chkMovieThemeKeepExisting.Checked = .MovieThemeKeepExisting
            chkMovieTrailerKeepExisting.Checked = .MovieTrailerKeepExisting
            chkTVCleanDB.Checked = .TVCleanDB
            chkTVDisplayMissingEpisodes.Checked = .TVDisplayMissingEpisodes
            chkTVEpisodeNoFilter.Checked = .TVEpisodeNoFilter
            chkTVEpisodeProperCase.Checked = .TVEpisodeProperCase
            chkTVGeneralClickScrape.Checked = .TVGeneralClickScrape
            chkTVGeneralClickScrapeAsk.Checked = .TVGeneralClickScrapeAsk
            chkTVGeneralMarkNewEpisodes.Checked = .TVGeneralMarkNewEpisodes
            chkTVGeneralMarkNewShows.Checked = .TVGeneralMarkNewShows
            chkTVGeneralIgnoreLastScan.Checked = .TVGeneralIgnoreLastScan
            chkTVLockEpisodeLanguageA.Checked = .TVLockEpisodeLanguageA
            chkTVLockEpisodeLanguageV.Checked = .TVLockEpisodeLanguageV
            chkTVLockEpisodePlot.Checked = .TVLockEpisodePlot
            chkTVLockEpisodeRatings.Checked = .TVLockEpisodeRating
            chkTVLockEpisodeRuntime.Checked = .TVLockEpisodeRuntime
            chkTVLockEpisodeTitle.Checked = .TVLockEpisodeTitle
            chkTVLockEpisodeUserRating.Checked = .TVLockEpisodeUserRating
            chkTVLockSeasonPlot.Checked = .TVLockSeasonPlot
            chkTVLockSeasonTitle.Checked = .TVLockSeasonTitle
            chkTVLockShowCert.Checked = .TVLockShowCert
            chkTVLockShowCountry.Checked = .TVLockShowCountry
            chkTVLockShowCreators.Checked = .TVLockShowCreators
            chkTVLockShowGenre.Checked = .TVLockShowGenre
            chkTVLockShowMPAA.Checked = .TVLockShowMPAA
            chkTVLockShowOriginalTitle.Checked = .TVLockShowOriginalTitle
            chkTVLockShowPlot.Checked = .TVLockShowPlot
            chkTVLockShowRatings.Checked = .TVLockShowRating
            chkTVLockShowRuntime.Checked = .TVLockShowRuntime
            chkTVLockShowStatus.Checked = .TVLockShowStatus
            chkTVLockShowStudio.Checked = .TVLockShowStudio
            chkTVLockShowTagline.Checked = .TVLockShowTagline
            chkTVLockShowTitle.Checked = .TVLockShowTitle
            chkTVLockShowUserRating.Checked = .TVLockShowUserRating
            chkTVScanOrderModify.Checked = .TVScanOrderModify
            chkTVScraperCastWithImg.Checked = .TVScraperCastWithImgOnly
            chkTVScraperCleanFields.Checked = .TVScraperCleanFields
            chkTVScraperEpisodeActors.Checked = .TVScraperEpisodeActors
            chkTVScraperEpisodeAired.Checked = .TVScraperEpisodeAired
            chkTVScraperEpisodeCredits.Checked = .TVScraperEpisodeCredits
            chkTVScraperEpisodeDirector.Checked = .TVScraperEpisodeDirector
            chkTVScraperEpisodeGuestStars.Checked = .TVScraperEpisodeGuestStars
            chkTVScraperEpisodeGuestStarsToActors.Checked = .TVScraperEpisodeGuestStarsToActors
            chkTVScraperEpisodePlot.Checked = .TVScraperEpisodePlot
            chkTVScraperEpisodeRatings.Checked = .TVScraperEpisodeRating
            chkTVScraperEpisodeRuntime.Checked = .TVScraperEpisodeRuntime
            chkTVScraperEpisodeTitle.Checked = .TVScraperEpisodeTitle
            chkTVScraperEpisodeUserRating.Checked = .TVScraperEpisodeUserRating
            chkTVScraperIdWriteNodeDefaultId.Checked = .TVScraperIdWriteNodeDefaultId
            chkTVScraperMetaDataScan.Checked = .TVScraperMetaDataScan
            chkTVScraperRatingVotesWriteNode.Checked = .TVScraperRatingVotesWriteNode
            chkTVScraperSeasonAired.Checked = .TVScraperSeasonAired
            chkTVScraperSeasonPlot.Checked = .TVScraperSeasonPlot
            chkTVScraperSeasonTitle.Checked = .TVScraperSeasonTitle
            chkTVScraperShowActors.Checked = .TVScraperShowActors
            chkTVScraperShowCert.Checked = .TVScraperShowCert
            chkTVScraperShowCountry.Checked = .TVScraperShowCountry
            chkTVScraperShowCreators.Checked = .TVScraperShowCreators
            chkTVScraperShowCertForMPAA.Checked = .TVScraperShowCertForMPAA
            chkTVScraperShowCertForMPAAFallback.Checked = .TVScraperShowCertForMPAAFallback
            chkTVScraperShowCertOnlyValue.Checked = .TVScraperShowCertOnlyValue
            chkTVScraperShowEpiGuideURL.Checked = .TVScraperShowEpiGuideURL
            chkTVScraperShowGenre.Checked = .TVScraperShowGenre
            chkTVScraperShowMPAA.Checked = .TVScraperShowMPAA
            chkTVScraperShowOriginalTitle.Checked = .TVScraperShowOriginalTitle
            chkTVScraperShowOriginalTitleAsTitle.Checked = .TVScraperShowOriginalTitleAsTitle
            chkTVScraperShowPlot.Checked = .TVScraperShowPlot
            chkTVScraperShowPremiered.Checked = .TVScraperShowPremiered
            chkTVScraperShowRatings.Checked = .TVScraperShowRating
            chkTVScraperShowRuntime.Checked = .TVScraperShowRuntime
            chkTVScraperShowStatus.Checked = .TVScraperShowStatus
            chkTVScraperShowStudio.Checked = .TVScraperShowStudio
            chkTVScraperShowTagline.Checked = .TVScraperShowTagline
            chkTVScraperShowTitle.Checked = .TVScraperShowTitle
            chkTVScraperShowUserRating.Checked = .TVScraperShowUserRating
            chkTVScraperUseDisplaySeasonEpisode.Checked = .TVScraperUseDisplaySeasonEpisode
            chkTVScraperUseMDDuration.Checked = .TVScraperUseMDDuration
            chkTVScraperUseSRuntimeForEp.Checked = .TVScraperUseSRuntimeForEp
            chkTVShowProperCase.Checked = .TVShowProperCase
            chkTVShowThemeKeepExisting.Checked = .TVShowThemeKeepExisting
            lstFileSystemCleanerWhitelist.Items.AddRange(.FileSystemCleanerWhitelistExts.ToArray)
            lstFileSystemNoStackExts.Items.AddRange(.FileSystemNoStackExts.ToArray)
            If .MovieGeneralCustomScrapeButtonEnabled Then
                rbMovieGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbMovieGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            If .MovieSetGeneralCustomScrapeButtonEnabled Then
                rbMovieSetGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbMovieSetGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            If .TVGeneralCustomScrapeButtonEnabled Then
                rbTVGeneralCustomScrapeButtonEnabled.Checked = True
            Else
                rbTVGeneralCustomScrapeButtonDisabled.Checked = True
            End If
            tcFileSystemCleaner.SelectedTab = If(.FileSystemExpertCleaner, tpFileSystemCleanerExpert, tpFileSystemCleanerStandard)
            txtGeneralImageFilterPosterMatchRate.Text = .GeneralImageFilterPosterMatchTolerance.ToString
            txtGeneralImageFilterFanartMatchRate.Text = .GeneralImageFilterFanartMatchTolerance.ToString
            txtMovieSourcesBackdropsFolderPath.Text = .MovieBackdropsPath
            txtMovieGeneralCustomMarker1.Text = .MovieGeneralCustomMarker1Name
            txtMovieGeneralCustomMarker2.Text = .MovieGeneralCustomMarker2Name
            txtMovieGeneralCustomMarker3.Text = .MovieGeneralCustomMarker3Name
            txtMovieGeneralCustomMarker4.Text = .MovieGeneralCustomMarker4Name
            txtMovieScraperDurationRuntimeFormat.Text = .MovieScraperDurationRuntimeFormat
            txtMovieSkipLessThan.Text = .MovieSkipLessThan.ToString
            txtMovieThemeDefaultSearch.Text = .MovieThemeDefaultSearch
            txtMovieTrailerDefaultSearch.Text = .MovieTrailerDefaultSearch
            txtTVScraperDurationRuntimeFormat.Text = .TVScraperDurationRuntimeFormat
            txtTVScraperEpisodeActorsLimit.Text = .TVScraperEpisodeActorsLimit.ToString
            txtTVScraperEpisodeGuestStarsLimit.Text = .TVScraperEpisodeGuestStarsLimit.ToString
            txtTVScraperIdDefaultType.Text = .TVScraperIdDefaultType
            txtTVScraperRatingDefaultType.Text = .TVScraperRatingDefaultType
            txtTVScraperShowActorsLimit.Text = .TVScraperShowActorsLimit.ToString
            txtTVScraperShowCountryLimit.Text = .TVScraperShowCountryLimit.ToString
            txtTVScraperShowMPAANotRated.Text = .TVScraperShowMPAANotRated
            txtTVScraperShowGenreLimit.Text = .TVScraperShowGenreLimit.ToString
            txtTVScraperShowStudioLimit.Text = .TVScraperShowStudioLimit.ToString
            txtTVShowThemeDefaultSearch.Text = .TVShowThemeDefaultSearch
            txtTVSourcesRegexMultiPartMatching.Text = .TVMultiPartMatching
            txtTVSkipLessThan.Text = .TVSkipLessThan.ToString

            FillMovieSetScraperTitleRenamer()

            Temp_MovieFilterCustom = .MovieFilterCustom
            Temp_TVEpisodeFilterCustom = .TVEpisodeFilterCustom
            Temp_TVScraperSeasonTitleBlacklist = .TVScraperSeasonTitleBlacklist
            Temp_TVShowFilterCustom = .TVShowFilterCustom

            RefreshFileSystemExcludeDirs()
            RefreshFileSystemValidExts()
            RefreshFileSystemValidSubtitlesExts()
            RefreshFileSystemValidThemeExts()
            RefreshMovieFilters()
            RefreshMovieSources()
            RefreshTVSources()
            RefreshTVEpisodeFilters()
            RefreshTVShowFilters()

            If .MovieLevTolerance > 0 Then
                chkMovieLevTolerance.Checked = True
                txtMovieLevTolerance.Enabled = True
                txtMovieLevTolerance.Text = .MovieLevTolerance.ToString
            End If

            MovieMeta.AddRange(.MovieMetadataPerFileType)
            LoadMovieMetadata()

            MovieGeneralMediaListSorting.AddRange(.MovieGeneralMediaListSorting)
            LoadMovieGeneralMediaListSorting()

            MovieSetGeneralMediaListSorting.AddRange(.MovieSetGeneralMediaListSorting)
            LoadMovieSetGeneralMediaListSorting()

            TVGeneralEpisodeListSorting.AddRange(.TVGeneralEpisodeListSorting)
            LoadTVGeneralEpisodeListSorting()

            TVGeneralSeasonListSorting.AddRange(.TVGeneralSeasonListSorting)
            LoadTVGeneralSeasonListSorting()

            TVGeneralShowListSorting.AddRange(.TVGeneralShowListSorting)
            LoadTVGeneralShowListSorting()

            TVMeta.AddRange(.TVMetadataPerFileType)
            LoadTVMetadata()

            TVShowMatching.AddRange(.TVShowMatching)
            LoadTVShowMatching()

            Try
                cbMovieGeneralLang.Items.Clear()
                cbMovieGeneralLang.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
                If cbMovieGeneralLang.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.MovieGeneralLanguage) Then
                        Dim tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .MovieGeneralLanguage)
                        If tLanguage IsNot Nothing Then
                            cbMovieGeneralLang.Text = tLanguage.Description
                        Else
                            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.MovieGeneralLanguage))
                            If tLanguage IsNot Nothing Then
                                cbMovieGeneralLang.Text = tLanguage.Description
                            Else
                                cbMovieGeneralLang.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                            End If
                        End If
                    Else
                        cbMovieGeneralLang.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Try
                cbTVScraperShowCertCountry.Items.Clear()
                cbTVScraperShowCertCountry.Items.Add(Master.eLang.CommonWordsList.All)
                cbTVScraperShowCertCountry.Items.AddRange((From lLang In Localization.Countries.Items Select lLang.Name).ToArray)
                If cbTVScraperShowCertCountry.Items.Count > 0 Then
                    If .TVScraperShowCertCountry = Master.eLang.CommonWordsList.All Then
                        cbTVScraperShowCertCountry.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.TVScraperShowCertCountry) Then
                        Dim tLanguage = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = .TVScraperShowCertCountry)
                        If tLanguage IsNot Nothing AndAlso tLanguage.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.Name) Then
                            cbTVScraperShowCertCountry.Text = tLanguage.Name
                        Else
                            cbTVScraperShowCertCountry.SelectedIndex = 0
                        End If
                    Else
                        cbTVScraperShowCertCountry.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Try
                cbTVGeneralLang.Items.Clear()
                cbTVGeneralLang.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
                If cbTVGeneralLang.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.TVGeneralLanguage) Then
                        Dim tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .TVGeneralLanguage)
                        If tLanguage IsNot Nothing AndAlso tLanguage.Description IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.Description) Then
                            cbTVGeneralLang.Text = tLanguage.Description
                        Else
                            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.TVGeneralLanguage))
                            If tLanguage IsNot Nothing Then
                                cbTVGeneralLang.Text = tLanguage.Description
                            Else
                                cbTVGeneralLang.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                            End If
                        End If
                    Else
                        cbTVGeneralLang.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
                chkProxyEnable.Checked = True
                txtProxyURI.Text = .ProxyURI
                txtProxyPort.Text = .ProxyPort.ToString

                If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
                    chkProxyCredsEnable.Checked = True
                    txtProxyUsername.Text = .ProxyCredentials.UserName
                    txtProxyPassword.Text = .ProxyCredentials.Password
                    txtProxyDomain.Text = .ProxyCredentials.Domain
                End If
            End If

            chkMovieClickScrapeAsk.Enabled = chkMovieClickScrape.Checked
            chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
            chkTVGeneralClickScrapeAsk.Enabled = chkTVGeneralClickScrape.Checked
            txtTVScraperDurationRuntimeFormat.Enabled = .TVScraperUseMDDuration

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            chkMovieUseFrodo.Checked = .MovieUseFrodo
            chkMovieActorThumbsFrodo.Checked = .MovieActorThumbsFrodo
            chkMovieExtrafanartsFrodo.Checked = .MovieExtrafanartsFrodo
            chkMovieExtrathumbsFrodo.Checked = .MovieExtrathumbsFrodo
            chkMovieFanartFrodo.Checked = .MovieFanartFrodo
            chkMovieNFOFrodo.Checked = .MovieNFOFrodo
            chkMoviePosterFrodo.Checked = .MoviePosterFrodo
            chkMovieTrailerFrodo.Checked = .MovieTrailerFrodo

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
            chkMovieBannerExtended.Checked = .MovieBannerExtended
            chkMovieClearArtExtended.Checked = .MovieClearArtExtended
            chkMovieClearLogoExtended.Checked = .MovieClearLogoExtended
            chkMovieDiscArtExtended.Checked = .MovieDiscArtExtended
            chkMovieKeyartExtended.Checked = .MovieKeyartExtended
            chkMovieLandscapeExtended.Checked = .MovieLandscapeExtended

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
            chkMovieActorThumbsExpertSingle.Checked = .MovieActorThumbsExpertSingle
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
            txtMovieKeyartExpertSingle.Text = .MovieKeyartExpertSingle
            txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
            txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
            txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
            txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle

            '******************* Expert Multi ******************
            chkMovieActorThumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
            chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti
            chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
            txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
            txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
            txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
            txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
            txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
            txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
            txtMovieKeyartExpertMulti.Text = .MovieKeyartExpertMulti
            txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
            txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
            txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
            txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti

            '******************* Expert VTS *******************
            chkMovieActorThumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
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
            txtMovieKeyartExpertVTS.Text = .MovieKeyartExpertVTS
            txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
            txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
            txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
            txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS

            '******************* Expert BDMV *******************
            chkMovieActorThumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
            chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
            chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
            chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV
            txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
            txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
            txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
            txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
            txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
            txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
            txtMovieKeyartExpertBDMV.Text = .MovieKeyartExpertBDMV
            txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
            txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
            txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
            txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '********* Kodi Matrix Images settings ***********
            chkMovieSetUseMatrix.Checked = .MovieSetUseMatrix
            chkMovieSetBannerMatrix.Checked = .MovieSetBannerMatrix
            chkMovieSetClearArtMatrix.Checked = .MovieSetClearArtMatrix
            chkMovieSetClearLogoMatrix.Checked = .MovieSetClearLogoMatrix
            chkMovieSetDiscArtMatrix.Checked = .MovieSetDiscArtMatrix
            chkMovieSetFanartMatrix.Checked = .MovieSetFanartMatrix
            chkMovieSetKeyartMatrix.Checked = .MovieSetKeyartMatrix
            chkMovieSetLandscapeMatrix.Checked = .MovieSetLandscapeMatrix
            chkMovieSetPosterMatrix.Checked = .MovieSetPosterMatrix
            txtMovieSetPathMatrix.Text = .MovieSetPathMatrix

            '********* Kodi Extended Images settings ***********
            chkMovieSetUseExtended.Checked = .MovieSetUseExtended
            chkMovieSetBannerExtended.Checked = .MovieSetBannerExtended
            chkMovieSetClearArtExtended.Checked = .MovieSetClearArtExtended
            chkMovieSetClearLogoExtended.Checked = .MovieSetClearLogoExtended
            chkMovieSetDiscArtExtended.Checked = .MovieSetDiscArtExtended
            chkMovieSetFanartExtended.Checked = .MovieSetFanartExtended
            chkMovieSetKeyartExtended.Checked = .MovieSetKeyartExtended
            chkMovieSetLandscapeExtended.Checked = .MovieSetLandscapeExtended
            chkMovieSetPosterExtended.Checked = .MovieSetPosterExtended
            txtMovieSetPathExtended.Text = .MovieSetPathExtended

            '**************** XBMC MSAA settings ***************
            chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA
            txtMovieSetPathMSAA.Text = .MovieSetPathMSAA

            '***************** Expert settings *****************
            chkMovieSetUseExpert.Checked = .MovieSetUseExpert

            '***************** Expert Single ******************
            txtMovieSetBannerExpertSingle.Text = .MovieSetBannerExpertSingle
            txtMovieSetClearArtExpertSingle.Text = .MovieSetClearArtExpertSingle
            txtMovieSetClearLogoExpertSingle.Text = .MovieSetClearLogoExpertSingle
            txtMovieSetDiscArtExpertSingle.Text = .MovieSetDiscArtExpertSingle
            txtMovieSetFanartExpertSingle.Text = .MovieSetFanartExpertSingle
            txtMovieSetKeyartExpertSingle.Text = .MovieSetKeyartExpertSingle
            txtMovieSetLandscapeExpertSingle.Text = .MovieSetLandscapeExpertSingle
            txtMovieSetNFOExpertSingle.Text = .MovieSetNFOExpertSingle
            txtMovieSetPathExpertSingle.Text = .MovieSetPathExpertSingle
            txtMovieSetPosterExpertSingle.Text = .MovieSetPosterExpertSingle


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

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
            chkTVShowKeyartExtended.Checked = .TVShowKeyartExtended
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
            chkTVEpisodeActorThumbsExpert.Checked = .TVEpisodeActorThumbsExpert
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
            chkTVShowActorThumbsExpert.Checked = .TVShowActorThumbsExpert
            txtTVShowActorThumbsExtExpert.Text = .TVShowActorThumbsExtExpert
            txtTVShowBannerExpert.Text = .TVShowBannerExpert
            txtTVShowCharacterArtExpert.Text = .TVShowCharacterArtExpert
            txtTVShowClearArtExpert.Text = .TVShowClearArtExpert
            txtTVShowClearLogoExpert.Text = .TVShowClearLogoExpert
            chkTVShowExtrafanartsExpert.Checked = .TVShowExtrafanartsExpert
            txtTVShowFanartExpert.Text = .TVShowFanartExpert
            txtTVShowKeyartExpert.Text = .TVShowKeyartExpert
            txtTVShowLandscapeExpert.Text = .TVShowLandscapeExpert
            txtTVShowNFOExpert.Text = .TVShowNFOExpert
            txtTVShowPosterExpert.Text = .TVShowPosterExpert

        End With
    End Sub

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Functions.PNLDoubleBuffer(pnlSettingsMain)
        Setup()
        SettingsPanels_Add_AllPanels()
        TopMenu_AddButtons()

        'get optimal panel size
        Dim pWidth As Integer = CInt(Width)
        Dim pHeight As Integer = CInt(Height)
        If My.Computer.Screen.WorkingArea.Width < 1120 Then
            pWidth = My.Computer.Screen.WorkingArea.Width
        End If
        If My.Computer.Screen.WorkingArea.Height < 900 Then
            pHeight = My.Computer.Screen.WorkingArea.Height
        End If
        Size = New Size(pWidth, pHeight)
        Dim pLeft As Integer
        Dim pTop As Integer
        pLeft = CInt((My.Computer.Screen.WorkingArea.Width - pWidth) / 2)
        pTop = CInt((My.Computer.Screen.WorkingArea.Height - pHeight) / 2)
        Location = New Point(pLeft, pTop)

        Dim iBackground As New Bitmap(pnlSettingsTop.Width, pnlSettingsTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsTop.ClientRectangle)
            pnlSettingsTop.BackgroundImage = iBackground
        End Using

        iBackground = New Bitmap(pnlSettingsCurrent.Width, pnlSettingsCurrent.Height)
        Using b As Graphics = Graphics.FromImage(iBackground)
            b.FillRectangle(New Drawing2D.LinearGradientBrush(pnlSettingsCurrent.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlSettingsCurrent.ClientRectangle)
            pnlSettingsCurrent.BackgroundImage = iBackground
        End Using

        LoadIntLangs()
        LoadLanguages()
        LoadThemes()
        ' Settings_Load()
        lvMovieSources.ListViewItemSorter = New ListViewItemComparer(1)
        lvTVSources.ListViewItemSorter = New ListViewItemComparer(1)
        _Results.NeedsDBClean_Movie = False
        _Results.NeedsDBClean_TV = False
        '_Results.NeedsDBUpdate_Movie = False
        '_Results.NeedsDBUpdate_TV = False
        _Results.NeedsReload_Movie = False
        _Results.NeedsReload_Movieset = False
        _Results.NeedsReload_TVEpisode = False
        _Results.NeedsReload_TVShow = False
        _Results.DidCancel = False
        _DidApply = False
        _NoUpdate = False
        RaiseEvent LoadEnd()
    End Sub

    Private Sub LoadIntLangs()
        cbGeneralLanguage.Items.Clear()
        cbGeneralLanguage.Items.AddRange(Master.eLang.Translations.Select(Function(f) f.Description).ToArray)
    End Sub

    Private Sub LoadLanguages()
        cbMovieLanguageOverlay.Items.Add(Master.eLang.CommonWordsList.Disabled)
        cbMovieLanguageOverlay.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
        cbTVLanguageOverlay.Items.Add(Master.eLang.CommonWordsList.Disabled)
        cbTVLanguageOverlay.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
    End Sub

    Private Sub LoadMovieGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        lvMovieGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In MovieGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadMovieSetGeneralMediaListSorting()
        Dim lvItem As ListViewItem
        lvMovieSetGeneralMediaListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In MovieSetGeneralMediaListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSetGeneralMediaListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralEpisodeListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralEpisodeListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralEpisodeListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralEpisodeListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralSeasonListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralSeasonListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralSeasonListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralSeasonListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadTVGeneralShowListSorting()
        Dim lvItem As ListViewItem
        lvTVGeneralShowListSorting.Items.Clear()
        For Each rColumn As Settings.ListSorting In TVGeneralShowListSorting.OrderBy(Function(f) f.DisplayIndex)
            lvItem = New ListViewItem(rColumn.DisplayIndex.ToString)
            lvItem.SubItems.Add(rColumn.Column)
            lvItem.SubItems.Add(Master.eLang.GetString(rColumn.LabelID, rColumn.LabelText))
            lvItem.SubItems.Add(If(rColumn.Hide, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVGeneralShowListSorting.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadMovieMetadata()
        lstMovieScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In MovieMeta
            lstMovieScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub LoadTVShowMatching()
        Dim lvItem As ListViewItem
        lvTVSourcesRegexTVShowMatching.Items.Clear()
        For Each rShow As Settings.regexp In TVShowMatching
            lvItem = New ListViewItem(rShow.Id.ToString)
            lvItem.SubItems.Add(rShow.Regexp)
            lvItem.SubItems.Add(If(Not rShow.DefaultSeason.ToString = "-2", rShow.DefaultSeason.ToString, String.Empty))
            lvItem.SubItems.Add(If(rShow.ByDate, "Yes", "No"))
            lvTVSourcesRegexTVShowMatching.Items.Add(lvItem)
        Next
    End Sub

    Private Sub LoadThemes()
        cbGeneralTheme.Items.Clear()
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        If diDefaults.Exists Then cbGeneralTheme.Items.AddRange(diDefaults.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)

        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diCustom.Exists Then cbGeneralTheme.Items.AddRange(diCustom.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_Movie()
        Dim items As New Dictionary(Of String, Enums.ModifierType) From {
            {Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All},
            {Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs},
            {Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner},
            {Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt},
            {Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo},
            {Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt},
            {Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts},
            {Master.eLang.GetString(74, "Extrathumbs Only"), Enums.ModifierType.MainExtrathumbs},
            {Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart},
            {Master.eLang.GetString(1238, "Keyart Only"), Enums.ModifierType.MainKeyart},
            {Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape},
            {Master.eLang.GetString(76, "Meta Data Only"), Enums.ModifierType.MainMetaData},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster},
            {Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme},
            {Master.eLang.GetString(75, "Trailer Only"), Enums.ModifierType.MainTrailer}
        }
        cbMovieGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_MovieSet()
        Dim items As New Dictionary(Of String, Enums.ModifierType) From {
            {Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All},
            {Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner},
            {Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt},
            {Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo},
            {Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt},
            {Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart},
            {Master.eLang.GetString(1238, "Keyart Only"), Enums.ModifierType.MainKeyart},
            {Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster}
        }
        cbMovieSetGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbMovieSetGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbMovieSetGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonModifierTypes_TV()
        Dim items As New Dictionary(Of String, Enums.ModifierType) From {
            {Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All},
            {Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs},
            {Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner},
            {Master.eLang.GetString(1121, "CharacterArt Only"), Enums.ModifierType.MainCharacterArt},
            {Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt},
            {Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo},
            {Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts},
            {Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart},
            {Master.eLang.GetString(1238, "Keyart Only"), Enums.ModifierType.MainKeyart},
            {Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster},
            {Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme}
        }
        cbTVGeneralCustomScrapeButtonModifierType.DataSource = items.ToList
        cbTVGeneralCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbTVGeneralCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonScrapeTypes()

        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Dim strAuto As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")

        Dim items As New Dictionary(Of String, Enums.ScrapeType) From {
            {strAuto, Enums.ScrapeType.Auto},
            {strAsk, Enums.ScrapeType.Ask},
            {strSkip, Enums.ScrapeType.Skip}
        }
        cbMovieGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
        cbMovieSetGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbMovieSetGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbMovieSetGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
        cbTVGeneralCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbTVGeneralCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbTVGeneralCustomScrapeButtonScrapeType.ValueMember = "Value"
    End Sub

    Private Sub LoadCustomScraperButtonSelectionTypes()
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Dim strFilter As String = Master.eLang.GetString(624, "Current Filter")
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        Dim strMissing As String = Master.eLang.GetString(40, "Missing Items")
        Dim strNew As String = Master.eLang.GetString(47, "New")

        Dim items As New Dictionary(Of String, Enums.SelectionType) From {
            {strAll, Enums.SelectionType.All},
            {strMissing, Enums.SelectionType.MissingContent},
            {strNew, Enums.SelectionType.[New]},
            {strMarked, Enums.SelectionType.Marked},
            {strFilter, Enums.SelectionType.Filtered}
        }
        cbMovieGeneralCustomScrapeButtonSelectionType.DataSource = items.ToList
        cbMovieGeneralCustomScrapeButtonSelectionType.DisplayMember = "Key"
        cbMovieGeneralCustomScrapeButtonSelectionType.ValueMember = "Value"
        cbMovieSetGeneralCustomScrapeButtonSelectionType.DataSource = items.ToList
        cbMovieSetGeneralCustomScrapeButtonSelectionType.DisplayMember = "Key"
        cbMovieSetGeneralCustomScrapeButtonSelectionType.ValueMember = "Value"
        cbTVGeneralCustomScrapeButtonSelectionType.DataSource = items.ToList
        cbTVGeneralCustomScrapeButtonSelectionType.DisplayMember = "Key"
        cbTVGeneralCustomScrapeButtonSelectionType.ValueMember = "Value"
    End Sub

    Private Sub LoadGeneralDateTime()
        Dim items As New Dictionary(Of String, Enums.DateTimeStamp) From {
            {Master.eLang.GetString(1210, "Current DateTime when adding"), Enums.DateTimeStamp.Now},
            {Master.eLang.GetString(1227, "ctime (fallback to mtime)"), Enums.DateTimeStamp.ctime},
            {Master.eLang.GetString(1211, "mtime (fallback to ctime)"), Enums.DateTimeStamp.mtime},
            {Master.eLang.GetString(1212, "Newer of mtime and ctime"), Enums.DateTimeStamp.Newer}
        }
        cbGeneralDateTime.DataSource = items.ToList
        cbGeneralDateTime.DisplayMember = "Key"
        cbGeneralDateTime.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieBannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x185", Enums.ImageSize.HD185}
        }
        cbMovieSetBannerPrefSize.DataSource = items.ToList
        cbMovieSetBannerPrefSize.DisplayMember = "Key"
        cbMovieSetBannerPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"2560x1440", Enums.ImageSize.QHD1440},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720}
        }
        cbMovieSetFanartPrefSize.DataSource = items.ToList
        cbMovieSetFanartPrefSize.DisplayMember = "Key"
        cbMovieSetFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMoviePosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"2000x3000", Enums.ImageSize.HD3000},
            {"1400x2100", Enums.ImageSize.HD2100},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426}
        }
        cbMovieSetKeyartPrefSize.DataSource = items.ToList
        cbMovieSetKeyartPrefSize.DisplayMember = "Key"
        cbMovieSetKeyartPrefSize.ValueMember = "Value"
        cbMovieSetPosterPrefSize.DataSource = items.ToList
        cbMovieSetPosterPrefSize.DisplayMember = "Key"
        cbMovieSetPosterPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadMovieTrailerQualities()
        Dim items As New Dictionary(Of String, Enums.VideoResolution) From {
            {Master.eLang.GetString(745, "Any"), Enums.VideoResolution.Any},
            {"2160p 60fps", Enums.VideoResolution.HD2160p60fps},
            {"2160p", Enums.VideoResolution.HD2160p},
            {"1440p", Enums.VideoResolution.HD1440p},
            {"1080p 60fps", Enums.VideoResolution.HD1080p60fps},
            {"1080p", Enums.VideoResolution.HD1080p},
            {"720p 60fps", Enums.VideoResolution.HD720p60fps},
            {"720p", Enums.VideoResolution.HD720p},
            {"480p", Enums.VideoResolution.HQ480p},
            {"360p", Enums.VideoResolution.SQ360p},
            {"240p", Enums.VideoResolution.SQ240p},
            {"144p", Enums.VideoResolution.SQ144p},
            {"144p 15fps", Enums.VideoResolution.SQ144p15fps}
        }
        cbMovieTrailerMinVideoQual.DataSource = items.ToList
        cbMovieTrailerMinVideoQual.DisplayMember = "Key"
        cbMovieTrailerMinVideoQual.ValueMember = "Value"
        cbMovieTrailerPrefVideoQual.DataSource = items.ToList
        cbMovieTrailerPrefVideoQual.DisplayMember = "Key"
        cbMovieTrailerPrefVideoQual.ValueMember = "Value"
    End Sub

    Private Sub LoadTVFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"2560x1440", Enums.ImageSize.QHD1440},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720}
        }
        cbTVAllSeasonsFanartPrefSize.DataSource = items.ToList
        cbTVAllSeasonsFanartPrefSize.DisplayMember = "Key"
        cbTVAllSeasonsFanartPrefSize.ValueMember = "Value"
        cbTVEpisodeFanartPrefSize.DataSource = items.ToList
        cbTVEpisodeFanartPrefSize.DisplayMember = "Key"
        cbTVEpisodeFanartPrefSize.ValueMember = "Value"
        cbTVSeasonFanartPrefSize.DataSource = items.ToList
        cbTVSeasonFanartPrefSize.DisplayMember = "Key"
        cbTVSeasonFanartPrefSize.ValueMember = "Value"
        cbTVShowExtrafanartsPrefSize.DataSource = items.ToList
        cbTVShowExtrafanartsPrefSize.DisplayMember = "Key"
        cbTVShowExtrafanartsPrefSize.ValueMember = "Value"
        cbTVShowFanartPrefSize.DataSource = items.ToList
        cbTVShowFanartPrefSize.DisplayMember = "Key"
        cbTVShowFanartPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVPosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"2000x3000", Enums.ImageSize.HD3000},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426},
            {"680x1000", Enums.ImageSize.HD1000}
        }
        cbTVAllSeasonsPosterPrefSize.DataSource = items.ToList
        cbTVAllSeasonsPosterPrefSize.DisplayMember = "Key"
        cbTVAllSeasonsPosterPrefSize.ValueMember = "Value"
        cbTVShowKeyartPrefSize.DataSource = items.ToList
        cbTVShowKeyartPrefSize.DisplayMember = "Key"
        cbTVShowKeyartPrefSize.ValueMember = "Value"
        cbTVShowPosterPrefSize.DataSource = items.ToList
        cbTVShowPosterPrefSize.DisplayMember = "Key"
        cbTVShowPosterPrefSize.ValueMember = "Value"

        Dim items2 As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426},
            {"400x578", Enums.ImageSize.HD578}
        }
        cbTVSeasonPosterPrefSize.DataSource = items2.ToList
        cbTVSeasonPosterPrefSize.DisplayMember = "Key"
        cbTVSeasonPosterPrefSize.ValueMember = "Value"

        Dim items3 As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720},
            {"400x225", Enums.ImageSize.SD225}
        }
        cbTVEpisodePosterPrefSize.DataSource = items3.ToList
        cbTVEpisodePosterPrefSize.DisplayMember = "Key"
        cbTVEpisodePosterPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVScraperOptionsOrdering()
        Dim items As New Dictionary(Of String, Enums.EpisodeOrdering) From {
            {Master.eLang.GetString(438, "Standard"), Enums.EpisodeOrdering.Standard},
            {Master.eLang.GetString(1067, "DVD"), Enums.EpisodeOrdering.DVD},
            {Master.eLang.GetString(839, "Absolute"), Enums.EpisodeOrdering.Absolute},
            {Master.eLang.GetString(1332, "Day Of Year"), Enums.EpisodeOrdering.DayOfYear}
        }
        cbTVScraperOptionsOrdering.DataSource = items.ToList
        cbTVScraperOptionsOrdering.DisplayMember = "Key"
        cbTVScraperOptionsOrdering.ValueMember = "Value"
    End Sub

    Private Sub LoadTVBannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x185", Enums.ImageSize.HD185},
            {"758x140", Enums.ImageSize.HD140}
        }
        cbTVAllSeasonsBannerPrefSize.DataSource = items.ToList
        cbTVAllSeasonsBannerPrefSize.DisplayMember = "Key"
        cbTVAllSeasonsBannerPrefSize.ValueMember = "Value"
        cbTVSeasonBannerPrefSize.DataSource = items.ToList
        cbTVSeasonBannerPrefSize.DisplayMember = "Key"
        cbTVSeasonBannerPrefSize.ValueMember = "Value"
        cbTVShowBannerPrefSize.DataSource = items.ToList
        cbTVShowBannerPrefSize.DisplayMember = "Key"
        cbTVShowBannerPrefSize.ValueMember = "Value"
    End Sub

    Private Sub LoadTVBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVBannerType) From {
            {Master.eLang.GetString(745, "Any"), Enums.TVBannerType.Any},
            {Master.eLang.GetString(746, "Blank"), Enums.TVBannerType.Blank},
            {Master.eLang.GetString(747, "Graphical"), Enums.TVBannerType.Graphical},
            {Master.eLang.GetString(748, "Text"), Enums.TVBannerType.Text}
        }
        cbTVAllSeasonsBannerPrefType.DataSource = items.ToList
        cbTVAllSeasonsBannerPrefType.DisplayMember = "Key"
        cbTVAllSeasonsBannerPrefType.ValueMember = "Value"
        cbTVSeasonBannerPrefType.DataSource = items.ToList
        cbTVSeasonBannerPrefType.DisplayMember = "Key"
        cbTVSeasonBannerPrefType.ValueMember = "Value"
        cbTVShowBannerPrefType.DataSource = items.ToList
        cbTVShowBannerPrefType.DisplayMember = "Key"
        cbTVShowBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadTVMetadata()
        lstTVScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In TVMeta
            lstTVScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub lstTVEpisodeFilter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVEpisodeFilter.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVEpisodeFilter()
    End Sub

    Private Sub lstMovieFilters_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstMovieFilters.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieFilter()
    End Sub

    Private Sub lstMovieScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstMovieScraperDefFIExt.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieMetaData()
    End Sub

    Private Sub lstMovieScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstMovieScraperDefFIExt.SelectedIndexChanged
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            btnMovieScraperDefFIExtEdit.Enabled = True
            btnMovieScraperDefFIExtRemove.Enabled = True
            txtMovieScraperDefFIExt.Text = String.Empty
        Else
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub lstFileSystemValidExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidVideoExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidExts()
    End Sub

    Private Sub lstFileSystemValidSubtitlesExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidSubtitlesExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidSubtitlesExts()
    End Sub

    Private Sub lstFileSystemValidThemeExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemValidThemeExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemValidThemeExts()
    End Sub

    Private Sub lstFileSystemNoStackExts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstFileSystemNoStackExts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFileSystemNoStackExts()
    End Sub

    Private Sub lstTVShowFilter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVShowFilter.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVShowFilter()
    End Sub

    Private Sub lstGeneralSortTokens_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstGeneralSortTokens.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveGeneralSortToken()
    End Sub

    Private Sub lstTVScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lstTVScraperDefFIExt.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVMetaData()
    End Sub

    Private Sub lstTVScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lstTVScraperDefFIExt.SelectedIndexChanged
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            btnTVScraperDefFIExtEdit.Enabled = True
            btnTVScraperDefFIExtRemove.Enabled = True
            txtTVScraperDefFIExt.Text = String.Empty
        Else
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub lvMovieSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvMovieSources.ColumnClick
        lvMovieSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvMovieSources_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvMovieSources.DoubleClick
        'If lvMovieSources.SelectedItems.Count > 0 Then
        '    Using dMovieSource As New dlgSource_Movie
        '        If dMovieSource.ShowDialog(Convert.ToInt32(lvMovieSources.SelectedItems(0).Text)) = DialogResult.OK Then
        '            RefreshMovieSources()
        '            _Results.NeedsReload_Movie = True 'TODO: Check if we have to use Reload or DBUpdate
        '            Button_Apply_SetEnabled(True)
        '        End If
        '    End Using
        'End If
    End Sub

    Private Sub lvMovieSources_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvMovieSources.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveMovieSource()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSourcesRegexTVShowMatching.DoubleClick
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvTVSourcesRegexTVShowMatching.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVShowMatching()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSourcesRegexTVShowMatching.SelectedIndexChanged
        If Not String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then ClearTVShowMatching()
    End Sub

    Private Sub lvTVSources_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvTVSources.ColumnClick
        lvTVSources.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvTVSources.DoubleClick
        'If lvTVSources.SelectedItems.Count > 0 Then
        '    Using dTVSource As New dlgSource_TVShow
        '        If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = DialogResult.OK Then
        '            RefreshTVSources()
        '            _Results.NeedsReload_TVShow = True
        '            Button_Apply_SetEnabled(True)
        '        End If
        '    End Using
        'End If
    End Sub

    Private Sub lvTVSources_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvTVSources.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVSource()
    End Sub

    Private Sub RefreshMovieFilters()
        lstMovieFilters.Items.Clear()
        lstMovieFilters.Items.AddRange(Temp_MovieFilterCustom.ToArray)
    End Sub

    Private Sub RefreshTVEpisodeFilters()
        lstTVEpisodeFilter.Items.Clear()
        lstTVEpisodeFilter.Items.AddRange(Temp_TVEpisodeFilterCustom.ToArray)
    End Sub

    Private Sub RefreshTVShowFilters()
        lstTVShowFilter.Items.Clear()
        lstTVShowFilter.Items.AddRange(Temp_TVShowFilterCustom.ToArray)
    End Sub

    Private Sub RefreshMovieSources()
        Dim lvItem As ListViewItem
        lvMovieSources.Items.Clear()
        For Each s As Database.DBSource In Master.DB.LoadAll_Sources_Movie
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(If(s.ScanRecursive, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.UseFolderName, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(If(s.GetYear, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvMovieSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        lvTVSources.Items.Clear()
        For Each s As Database.DBSource In Master.DB.LoadAll_Sources_TVShow
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            lvItem.SubItems.Add(s.Language)
            lvItem.SubItems.Add(s.EpisodeOrdering.ToString)
            lvItem.SubItems.Add(If(s.Exclude, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvItem.SubItems.Add(s.EpisodeSorting.ToString)
            lvItem.SubItems.Add(If(s.IsSingle, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
            lvTVSources.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshFileSystemExcludeDirs()
        lstFileSystemExcludedDirs.Items.Clear()
        lstFileSystemExcludedDirs.Items.AddRange(Master.DB.GetAll_ExcludedPaths.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidExts()
        lstFileSystemValidVideoExts.Items.Clear()
        lstFileSystemValidVideoExts.Items.AddRange(Temp_FileSystemValidExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidSubtitlesExts()
        lstFileSystemValidSubtitlesExts.Items.Clear()
        lstFileSystemValidSubtitlesExts.Items.AddRange(Temp_FileSystemValidSubtitlesExts.ToArray)
    End Sub

    Private Sub RefreshFileSystemValidThemeExts()
        lstFileSystemValidThemeExts.Items.Clear()
        lstFileSystemValidThemeExts.Items.AddRange(Temp_FileSystemValidThemeExts.ToArray)
    End Sub

    Private Sub RemoveTVEpisodeFilter()
        If lstTVEpisodeFilter.Items.Count > 0 AndAlso lstTVEpisodeFilter.SelectedItems.Count > 0 Then
            While lstTVEpisodeFilter.SelectedItems.Count > 0
                lstTVEpisodeFilter.Items.Remove(lstTVEpisodeFilter.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveMovieFilter()
        If lstMovieFilters.Items.Count > 0 AndAlso lstMovieFilters.SelectedItems.Count > 0 Then
            While lstMovieFilters.SelectedItems.Count > 0
                lstMovieFilters.Items.Remove(lstMovieFilters.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_Movie = True
        End If
    End Sub

    Private Sub RemoveMovieMetaData()
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In MovieMeta
                If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    MovieMeta.Remove(x)
                    LoadMovieMetadata()
                    Button_Apply_SetEnabled(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveFileSystemValidExts()
        If lstFileSystemValidVideoExts.Items.Count > 0 AndAlso lstFileSystemValidVideoExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidVideoExts.SelectedItems.Count > 0
                lstFileSystemValidVideoExts.Items.Remove(lstFileSystemValidVideoExts.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsDBClean_Movie = True
            _Results.NeedsDBClean_TV = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidSubtitlesExts()
        If lstFileSystemValidSubtitlesExts.Items.Count > 0 AndAlso lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidSubtitlesExts.SelectedItems.Count > 0
                lstFileSystemValidSubtitlesExts.Items.Remove(lstFileSystemValidSubtitlesExts.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_Movie = True
            _Results.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveFileSystemValidThemeExts()
        If lstFileSystemValidThemeExts.Items.Count > 0 AndAlso lstFileSystemValidThemeExts.SelectedItems.Count > 0 Then
            While lstFileSystemValidThemeExts.SelectedItems.Count > 0
                lstFileSystemValidThemeExts.Items.Remove(lstFileSystemValidThemeExts.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_Movie = True
            _Results.NeedsReload_TVEpisode = True
        End If
    End Sub

    Private Sub RemoveMovieSource()
        If lvMovieSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvMovieSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "idSource")
                        While lvMovieSources.SelectedItems.Count > 0
                            parSource.Value = lvMovieSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM moviesource WHERE idSource = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lvMovieSources.Items.Remove(lvMovieSources.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                lvMovieSources.Sort()
                lvMovieSources.EndUpdate()
                lvMovieSources.Refresh()

                Button_Apply_SetEnabled(True)
            End If
        End If
    End Sub

    Private Sub RemoveFileSystemNoStackExts()
        If lstFileSystemNoStackExts.Items.Count > 0 AndAlso lstFileSystemNoStackExts.SelectedItems.Count > 0 Then
            While lstFileSystemNoStackExts.SelectedItems.Count > 0
                lstFileSystemNoStackExts.Items.Remove(lstFileSystemNoStackExts.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            '_Results.NeedsDBUpdate_Movie = True
            '_Results.NeedsDBUpdate_TV = True
        End If
    End Sub

    Private Sub RemoveTVShowMatching()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvTVSourcesRegexTVShowMatching.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.Id = ID
            If selRex.Count > 0 Then
                TVShowMatching.Remove(selRex(0))
                Button_Apply_SetEnabled(True)
            End If
        Next
        LoadTVShowMatching()
    End Sub

    Private Sub RemoveTVShowFilter()
        If lstTVShowFilter.Items.Count > 0 AndAlso lstTVShowFilter.SelectedItems.Count > 0 Then
            While lstTVShowFilter.SelectedItems.Count > 0
                lstTVShowFilter.Items.Remove(lstTVShowFilter.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
            _Results.NeedsReload_TVShow = True
        End If
    End Sub

    Private Sub RemoveGeneralSortToken()
        If lstGeneralSortTokens.Items.Count > 0 AndAlso lstGeneralSortTokens.SelectedItems.Count > 0 Then
            While lstGeneralSortTokens.SelectedItems.Count > 0
                lstGeneralSortTokens.Items.Remove(lstGeneralSortTokens.SelectedItems(0))
            End While
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub RemoveTVMetaData()
        If lstTVScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In TVMeta
                If x.FileType = lstTVScraperDefFIExt.SelectedItems(0).ToString Then
                    TVMeta.Remove(x)
                    LoadTVMetadata()
                    Button_Apply_SetEnabled(True)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub RemoveTVSource()
        If lvTVSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(1033, "Are you sure you want to remove the selected sources? This will remove the tv shows from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvTVSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.Int64, 0, "idSource")
                        While lvTVSources.SelectedItems.Count > 0
                            parSource.Value = lvTVSources.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM tvshowsource WHERE idSource = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lvTVSources.Items.Remove(lvTVSources.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                lvTVSources.Sort()
                lvTVSources.EndUpdate()
                lvTVSources.Refresh()

                Button_Apply_SetEnabled(True)
            End If
        End If
    End Sub

    Private Sub RenumberTVShowMatching()
        For i As Integer = 0 To TVShowMatching.Count - 1
            TVShowMatching(i).Id = i
        Next
    End Sub

    Private Sub RenumberMovieGeneralMediaListSorting()
        For i As Integer = 0 To MovieGeneralMediaListSorting.Count - 1
            MovieGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberMovieSetGeneralMediaListSorting()
        For i As Integer = 0 To MovieSetGeneralMediaListSorting.Count - 1
            MovieSetGeneralMediaListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVEpisodeGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralEpisodeListSorting.Count - 1
            TVGeneralEpisodeListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVSeasonGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralSeasonListSorting.Count - 1
            TVGeneralSeasonListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub RenumberTVShowGeneralMediaListSorting()
        For i As Integer = 0 To TVGeneralShowListSorting.Count - 1
            TVGeneralShowListSorting(i).DisplayIndex = i
        Next
    End Sub

    Private Sub Settings_Save(ByVal isApply As Boolean)
        For Each addon In Addons.Instance.Addons
            Try
                addon.AddonInterface.SaveSettings(Not isApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        For Each Panel In _AllSettingsPanels
            Panel.SaveSettings()
        Next
        Addons.Instance.Settings_Save()
        Functions.CreateDefaultOptions()
    End Sub

    Private Sub Settings_Save_All_old(ByVal isApply As Boolean)
        With Master.eSettings
            .FileSystemNoStackExts.Clear()
            .FileSystemNoStackExts.AddRange(lstFileSystemNoStackExts.Items.OfType(Of String).ToList)
            .GeneralDateAddedIgnoreNFO = chkGeneralDateAddedIgnoreNFO.Checked
            .GeneralDateTime = CType(cbGeneralDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            .GeneralDoubleClickScrape = chkGeneralDoubleClickScrape.Checked
            .GeneralDisplayBanner = chkGeneralDisplayBanner.Checked
            .GeneralDisplayCharacterArt = chkGeneralDisplayCharacterArt.Checked
            .GeneralDisplayClearArt = chkGeneralDisplayClearArt.Checked
            .GeneralDisplayClearLogo = chkGeneralDisplayClearLogo.Checked
            .GeneralDisplayDiscArt = chkGeneralDisplayDiscArt.Checked
            .GeneralDisplayFanartAsBackground = chkGeneralDisplayFanart.Checked
            .GeneralDisplayFanart = chkGeneralDisplayFanartSmall.Checked
            .GeneralDisplayLandscape = chkGeneralDisplayLandscape.Checked
            .GeneralDisplayPoster = chkGeneralDisplayPoster.Checked
            .GeneralImagesGlassOverlay = chkGeneralImagesGlassOverlay.Checked
            .GeneralImageFilter = chkGeneralImageFilter.Checked
            .GeneralImageFilterAutoscraper = chkGeneralImageFilterAutoscraper.Checked
            .GeneralImageFilterFanart = chkGeneralImageFilterFanart.Checked
            .GeneralImageFilterImagedialog = chkGeneralImageFilterImagedialog.Checked
            .GeneralImageFilterPoster = chkGeneralImageFilterPoster.Checked
            If Not String.IsNullOrEmpty(txtGeneralImageFilterFanartMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterFanartMatchRate.Text, 4) Then
                .GeneralImageFilterFanartMatchTolerance = Convert.ToInt32(txtGeneralImageFilterFanartMatchRate.Text)
            Else
                .GeneralImageFilterFanartMatchTolerance = 4
            End If
            If Not String.IsNullOrEmpty(txtGeneralImageFilterPosterMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterPosterMatchRate.Text, 1) Then
                .GeneralImageFilterPosterMatchTolerance = Convert.ToInt32(txtGeneralImageFilterPosterMatchRate.Text)
            Else
                .GeneralImageFilterPosterMatchTolerance = 1
            End If
            .GeneralLanguage = Master.eLang.Translations.FirstOrDefault(Function(f) f.Description = cbGeneralLanguage.Text).Language
            .GeneralNotificationAddedMovie = chkGeneralNotificationAdded_Movie.Checked
            .GeneralNotificationAddedMovieset = chkGeneralNotificationAdded_MovieSet.Checked
            .GeneralNotificationAddedTVEpisode = chkGeneralNotificationAdded_TVEpisode.Checked
            .GeneralNotificationAddedTVShow = chkGeneralNotificationAdded_TVShow.Checked
            .GeneralNotificationError = chkGeneralNotificationError.Checked
            .GeneralNotificationInformation = chkGeneralNotificationInformation.Checked
            .GeneralNotificationScrapedMovie = chkGeneralNotificationScraped_Movie.Checked
            .GeneralNotificationScrapedMovieset = chkGeneralNotificationScraped_MovieSet.Checked
            .GeneralNotificationScrapedTVEpisode = chkGeneralNotificationScraped_TVEpisode.Checked
            .GeneralNotificationScrapedTVSeason = chkGeneralNotificationScraped_TVSeason.Checked
            .GeneralNotificationScrapedTVShow = chkGeneralNotificationScraped_TVShow.Checked
            .GeneralNotificationWarning = chkGeneralNotificationWarning.Checked
            .GeneralTheme = cbGeneralTheme.Text
            .GeneralOverwriteNfo = chkGeneralOverwriteNfo.Checked
            .GeneralShowGenresText = chkGeneralDisplayGenresText.Checked
            .GeneralShowLangFlags = chkGeneralDisplayLangFlags.Checked
            .GeneralShowImgDims = chkGeneralDisplayImgDims.Checked
            .GeneralShowImgNames = chkGeneralDisplayImgNames.Checked
            .GeneralSourceFromFolder = chkGeneralSourceFromFolder.Checked
            '.MovieActorThumbsQual = Me.tbMovieActorThumbsQual.value
            .MovieBackdropsPath = txtMovieSourcesBackdropsFolderPath.Text
            If Not String.IsNullOrEmpty(txtMovieSourcesBackdropsFolderPath.Text) Then
                .MovieBackdropsAuto = chkMovieSourcesBackdropsAuto.Checked
            Else
                .MovieBackdropsAuto = False
            End If
            .MovieCleanDB = chkMovieCleanDB.Checked
            .MovieClickScrape = chkMovieClickScrape.Checked
            .MovieClickScrapeAsk = chkMovieClickScrapeAsk.Checked
            .MovieFilterCustom.Clear()
            .MovieFilterCustom.AddRange(lstMovieFilters.Items.OfType(Of String).ToList)
            .MovieGeneralCustomMarker1Color = btnMovieGeneralCustomMarker1.BackColor.ToArgb
            .MovieGeneralCustomMarker2Color = btnMovieGeneralCustomMarker2.BackColor.ToArgb
            .MovieGeneralCustomMarker3Color = btnMovieGeneralCustomMarker3.BackColor.ToArgb
            .MovieGeneralCustomMarker4Color = btnMovieGeneralCustomMarker4.BackColor.ToArgb
            .MovieGeneralCustomMarker1Name = txtMovieGeneralCustomMarker1.Text
            .MovieGeneralCustomMarker2Name = txtMovieGeneralCustomMarker2.Text
            .MovieGeneralCustomMarker3Name = txtMovieGeneralCustomMarker3.Text
            .MovieGeneralCustomMarker4Name = txtMovieGeneralCustomMarker4.Text
            .MovieGeneralCustomScrapeButtonEnabled = rbMovieGeneralCustomScrapeButtonEnabled.Checked
            .MovieGeneralCustomScrapeButtonModifierType = CType(cbMovieGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .MovieGeneralCustomScrapeButtonScrapeType = CType(cbMovieGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .MovieGeneralCustomScrapeButtonSelectionType = CType(cbMovieGeneralCustomScrapeButtonSelectionType.SelectedItem, KeyValuePair(Of String, Enums.SelectionType)).Value
            .MovieGeneralFlagLang = If(cbMovieLanguageOverlay.Text = Master.eLang.CommonWordsList.Disabled, String.Empty, cbMovieLanguageOverlay.Text)
            .MovieGeneralIgnoreLastScan = chkMovieGeneralIgnoreLastScan.Checked
            If Not String.IsNullOrEmpty(cbMovieGeneralLang.Text) Then
                .MovieGeneralLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbMovieGeneralLang.Text).Abbreviation
            End If
            .MovieGeneralMarkNew = chkMovieGeneralMarkNew.Checked
            .MovieGeneralMediaListSorting.Clear()
            .MovieGeneralMediaListSorting.AddRange(MovieGeneralMediaListSorting)
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(txtMovieLevTolerance.Text), Convert.ToInt32(txtMovieLevTolerance.Text), 0)
            .MovieMetadataPerFileType.Clear()
            .MovieMetadataPerFileType.AddRange(MovieMeta)
            .MovieProperCase = chkMovieProperCase.Checked
            .MovieSetCleanDB = chkMovieSetCleanDB.Checked
            .MovieSetCleanFiles = chkMovieSetCleanFiles.Checked
            .MovieSetClickScrape = chkMovieSetClickScrape.Checked
            .MovieSetClickScrapeAsk = chkMovieSetClickScrapeAsk.Checked
            .MovieSetGeneralCustomScrapeButtonEnabled = rbMovieSetGeneralCustomScrapeButtonEnabled.Checked
            .MovieSetGeneralCustomScrapeButtonModifierType = CType(cbMovieSetGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .MovieSetGeneralCustomScrapeButtonScrapeType = CType(cbMovieSetGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .MovieSetGeneralCustomScrapeButtonSelectionType = CType(cbMovieSetGeneralCustomScrapeButtonSelectionType.SelectedItem, KeyValuePair(Of String, Enums.SelectionType)).Value
            .MovieSetGeneralMarkNew = chkMovieSetGeneralMarkNew.Checked
            .MovieSetGeneralMediaListSorting.Clear()
            .MovieSetGeneralMediaListSorting.AddRange(MovieSetGeneralMediaListSorting)
            .MovieScanOrderModify = chkMovieScanOrderModify.Checked
            .MovieScraperDurationRuntimeFormat = txtMovieScraperDurationRuntimeFormat.Text
            .MovieScraperMetaDataIFOScan = chkMovieScraperMetaDataIFOScan.Checked
            .MovieScraperMetaDataScan = chkMovieScraperMetaDataScan.Checked
            If Not String.IsNullOrEmpty(txtMovieSkipLessThan.Text) AndAlso Integer.TryParse(txtMovieSkipLessThan.Text, 0) Then
                .MovieSkipLessThan = Convert.ToInt32(txtMovieSkipLessThan.Text)
            Else
                .MovieSkipLessThan = 0
            End If
            .MovieSkipStackedSizeCheck = chkMovieSkipStackedSizeCheck.Checked
            .MovieSortBeforeScan = chkMovieSortBeforeScan.Checked
            .MovieThemeDefaultSearch = txtMovieThemeDefaultSearch.Text
            .MovieThemeKeepExisting = chkMovieThemeKeepExisting.Checked
            .MovieTrailerDefaultSearch = txtMovieTrailerDefaultSearch.Text
            .MovieTrailerKeepExisting = chkMovieTrailerKeepExisting.Checked
            .MovieTrailerMinVideoQual = CType(cbMovieTrailerMinVideoQual.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value
            .MovieTrailerPrefVideoQual = CType(cbMovieTrailerPrefVideoQual.SelectedItem, KeyValuePair(Of String, Enums.VideoResolution)).Value
            .TVCleanDB = chkTVCleanDB.Checked
            .TVDisplayMissingEpisodes = chkTVDisplayMissingEpisodes.Checked
            .TVEpisodeFilterCustom.Clear()
            .TVEpisodeFilterCustom.AddRange(lstTVEpisodeFilter.Items.OfType(Of String).ToList)
            .TVEpisodeNoFilter = chkTVEpisodeNoFilter.Checked
            .TVEpisodeProperCase = chkTVEpisodeProperCase.Checked
            .TVGeneralEpisodeListSorting.Clear()
            .TVGeneralEpisodeListSorting.AddRange(TVGeneralEpisodeListSorting)
            .TVGeneralFlagLang = If(cbTVLanguageOverlay.Text = Master.eLang.CommonWordsList.Disabled, String.Empty, cbTVLanguageOverlay.Text)
            .TVGeneralIgnoreLastScan = chkTVGeneralIgnoreLastScan.Checked
            'cocotus, 2014/05/21 Fixed: If cbTVGeneralLang.Text is empty it will crash here -> no AdvancedSettings.xml will be built/saved!!(happens when user has not yet set TVLanguage via Fetch language button!)
            'old:    .TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = cbTVGeneralLang.Text).ShortLang
            If Not String.IsNullOrEmpty(cbTVGeneralLang.Text) Then
                .TVGeneralLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbTVGeneralLang.Text).Abbreviation
            End If
            .TVGeneralClickScrape = chkTVGeneralClickScrape.Checked
            .TVGeneralClickScrapeAsk = chkTVGeneralClickScrapeAsk.Checked
            .TVGeneralCustomScrapeButtonEnabled = rbTVGeneralCustomScrapeButtonEnabled.Checked
            .TVGeneralCustomScrapeButtonModifierType = CType(cbTVGeneralCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .TVGeneralCustomScrapeButtonScrapeType = CType(cbTVGeneralCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .TVGeneralCustomScrapeButtonSelectionType = CType(cbTVGeneralCustomScrapeButtonSelectionType.SelectedItem, KeyValuePair(Of String, Enums.SelectionType)).Value
            .TVGeneralMarkNewEpisodes = chkTVGeneralMarkNewEpisodes.Checked
            .TVGeneralMarkNewShows = chkTVGeneralMarkNewShows.Checked
            .TVGeneralSeasonListSorting.Clear()
            .TVGeneralSeasonListSorting.AddRange(TVGeneralSeasonListSorting)
            .TVGeneralShowListSorting.Clear()
            .TVGeneralShowListSorting.AddRange(TVGeneralShowListSorting)
            .TVLockEpisodeLanguageA = chkTVLockEpisodeLanguageA.Checked
            .TVLockEpisodeLanguageV = chkTVLockEpisodeLanguageV.Checked
            .TVLockEpisodePlot = chkTVLockEpisodePlot.Checked
            .TVLockEpisodeRating = chkTVLockEpisodeRatings.Checked
            .TVLockEpisodeRuntime = chkTVLockEpisodeRuntime.Checked
            .TVLockEpisodeTitle = chkTVLockEpisodeTitle.Checked
            .TVLockEpisodeUserRating = chkTVLockEpisodeUserRating.Checked
            .TVLockSeasonPlot = chkTVLockSeasonPlot.Checked
            .TVLockSeasonTitle = chkTVLockSeasonTitle.Checked
            .TVLockShowCert = chkTVLockShowCert.Checked
            .TVLockShowCountry = chkTVLockShowCountry.Checked
            .TVLockShowCreators = chkTVLockShowCreators.Checked
            .TVLockShowGenre = chkTVLockShowGenre.Checked
            .TVLockShowMPAA = chkTVLockShowMPAA.Checked
            .TVLockShowOriginalTitle = chkTVLockShowOriginalTitle.Checked
            .TVLockShowPlot = chkTVLockShowPlot.Checked
            .TVLockShowRating = chkTVLockShowRatings.Checked
            .TVLockShowRuntime = chkTVLockShowRuntime.Checked
            .TVLockShowStatus = chkTVLockShowStatus.Checked
            .TVLockShowStudio = chkTVLockShowStudio.Checked
            .TVLockShowTagline = chkTVLockShowTagline.Checked
            .TVLockShowTitle = chkTVLockShowTitle.Checked
            .TVLockShowUserRating = chkTVLockShowUserRating.Checked
            .TVMetadataPerFileType.Clear()
            .TVMetadataPerFileType.AddRange(TVMeta)
            .TVMultiPartMatching = txtTVSourcesRegexMultiPartMatching.Text
            .TVScanOrderModify = chkTVScanOrderModify.Checked
            .TVScraperCastWithImgOnly = chkTVScraperCastWithImg.Checked
            .TVScraperCleanFields = chkTVScraperCleanFields.Checked
            .TVScraperDurationRuntimeFormat = txtTVScraperDurationRuntimeFormat.Text
            .TVScraperEpisodeActors = chkTVScraperEpisodeActors.Checked
            Integer.TryParse(txtTVScraperEpisodeActorsLimit.Text, .TVScraperEpisodeActorsLimit)
            .TVScraperEpisodeAired = chkTVScraperEpisodeAired.Checked
            .TVScraperEpisodeCredits = chkTVScraperEpisodeCredits.Checked
            .TVScraperEpisodeDirector = chkTVScraperEpisodeDirector.Checked
            .TVScraperEpisodeGuestStars = chkTVScraperEpisodeGuestStars.Checked
            Integer.TryParse(txtTVScraperEpisodeGuestStarsLimit.Text, .TVScraperEpisodeGuestStarsLimit)
            .TVScraperEpisodeGuestStarsToActors = chkTVScraperEpisodeGuestStarsToActors.Checked
            .TVScraperEpisodePlot = chkTVScraperEpisodePlot.Checked
            .TVScraperEpisodeRating = chkTVScraperEpisodeRatings.Checked
            .TVScraperEpisodeRuntime = chkTVScraperEpisodeRuntime.Checked
            .TVScraperEpisodeTitle = chkTVScraperEpisodeTitle.Checked
            .TVScraperEpisodeUserRating = chkTVScraperEpisodeUserRating.Checked
            .TVScraperMetaDataScan = chkTVScraperMetaDataScan.Checked
            .TVScraperOptionsOrdering = CType(cbTVScraperOptionsOrdering.SelectedItem, KeyValuePair(Of String, Enums.EpisodeOrdering)).Value
            .TVScraperRatingVotesWriteNode = chkTVScraperRatingVotesWriteNode.Checked
            .TVScraperSeasonAired = chkTVScraperSeasonAired.Checked
            .TVScraperSeasonPlot = chkTVScraperSeasonPlot.Checked
            .TVScraperSeasonTitle = chkTVScraperSeasonTitle.Checked
            .TVScraperSeasonTitleBlacklist = Temp_TVScraperSeasonTitleBlacklist
            .TVScraperShowActors = chkTVScraperShowActors.Checked
            Integer.TryParse(txtTVScraperShowActorsLimit.Text, .TVScraperShowActorsLimit)
            .TVScraperShowCert = chkTVScraperShowCert.Checked
            .TVScraperShowCountry = chkTVScraperShowCountry.Checked
            Integer.TryParse(txtTVScraperShowCountryLimit.Text, .TVScraperShowCountryLimit)
            .TVScraperShowCreators = chkTVScraperShowCreators.Checked
            .TVScraperShowCertForMPAA = chkTVScraperShowCertForMPAA.Checked
            .TVScraperShowCertForMPAAFallback = chkTVScraperShowCertForMPAAFallback.Checked
            .TVScraperShowCertOnlyValue = chkTVScraperShowCertOnlyValue.Checked
            If Not String.IsNullOrEmpty(cbTVScraperShowCertCountry.Text) Then
                If cbTVScraperShowCertCountry.SelectedIndex = 0 Then
                    .TVScraperShowCertCountry = Master.eLang.CommonWordsList.All
                Else
                    .TVScraperShowCertCountry = Localization.Countries.Items.FirstOrDefault(Function(l) l.Name = cbTVScraperShowCertCountry.Text).Alpha2
                End If
            End If
            .TVScraperShowEpiGuideURL = chkTVScraperShowEpiGuideURL.Checked
            .TVScraperShowGenre = chkTVScraperShowGenre.Checked
            Integer.TryParse(txtTVScraperShowGenreLimit.Text, .TVScraperShowGenreLimit)
            .TVScraperIdDefaultType = txtTVScraperIdDefaultType.Text
            .TVScraperShowMPAA = chkTVScraperShowMPAA.Checked
            .TVScraperShowOriginalTitle = chkTVScraperShowOriginalTitle.Checked
            .TVScraperShowOriginalTitleAsTitle = chkTVScraperShowOriginalTitleAsTitle.Checked
            .TVScraperShowMPAANotRated = txtTVScraperShowMPAANotRated.Text
            .TVScraperShowPlot = chkTVScraperShowPlot.Checked
            .TVScraperShowPremiered = chkTVScraperShowPremiered.Checked
            .TVScraperShowRating = chkTVScraperShowRatings.Checked
            .TVScraperRatingDefaultType = txtTVScraperRatingDefaultType.Text
            .TVScraperShowRuntime = chkTVScraperShowRuntime.Checked
            .TVScraperShowStatus = chkTVScraperShowStatus.Checked
            .TVScraperShowStudio = chkTVScraperShowStudio.Checked
            Integer.TryParse(txtTVScraperShowStudioLimit.Text, .TVScraperShowStudioLimit)
            .TVScraperShowTagline = chkTVScraperShowTagline.Checked
            .TVScraperShowTitle = chkTVScraperShowTitle.Checked
            .TVScraperShowUserRating = chkTVScraperShowUserRating.Checked
            .TVScraperUseDisplaySeasonEpisode = chkTVScraperUseDisplaySeasonEpisode.Checked
            .TVScraperUseMDDuration = chkTVScraperUseMDDuration.Checked
            .TVScraperUseSRuntimeForEp = chkTVScraperUseSRuntimeForEp.Checked
            .TVShowFilterCustom.Clear()
            .TVShowFilterCustom.AddRange(lstTVShowFilter.Items.OfType(Of String).ToList)
            .TVShowProperCase = chkTVShowProperCase.Checked
            .TVShowMatching.Clear()
            .TVShowMatching.AddRange(TVShowMatching)
            .TVShowThemeDefaultSearch = txtTVShowThemeDefaultSearch.Text
            .TVShowThemeKeepExisting = chkTVShowThemeKeepExisting.Checked
            If Not String.IsNullOrEmpty(txtTVSkipLessThan.Text) AndAlso Integer.TryParse(txtTVSkipLessThan.Text, 0) Then
                .TVSkipLessThan = Convert.ToInt32(txtTVSkipLessThan.Text)
            Else
                .TVSkipLessThan = 0
            End If

            If tcFileSystemCleaner.SelectedTab.Name = "tpFileSystemCleanerExpert" Then
                .FileSystemExpertCleaner = True
                .CleanFolderJPG = False
                .CleanMovieTBN = False
                .CleanMovieTBNB = False
                .CleanFanartJPG = False
                .CleanMovieFanartJPG = False
                .CleanMovieNFO = False
                .CleanMovieNFOB = False
                .CleanPosterTBN = False
                .CleanPosterJPG = False
                .CleanMovieJPG = False
                .CleanMovieNameJPG = False
                .CleanDotFanartJPG = False
                .CleanExtrathumbs = False
                .FileSystemCleanerWhitelist = chkFileSystemCleanerWhitelist.Checked
                .FileSystemCleanerWhitelistExts.Clear()
                .FileSystemCleanerWhitelistExts.AddRange(lstFileSystemCleanerWhitelist.Items.OfType(Of String).ToList)
            Else
                .FileSystemExpertCleaner = False
                .CleanFolderJPG = chkCleanFolderJPG.Checked
                .CleanMovieTBN = chkCleanMovieTBN.Checked
                .CleanMovieTBNB = chkCleanMovieTBNb.Checked
                .CleanFanartJPG = chkCleanFanartJPG.Checked
                .CleanMovieFanartJPG = chkCleanMovieFanartJPG.Checked
                .CleanMovieNFO = chkCleanMovieNFO.Checked
                .CleanMovieNFOB = chkCleanMovieNFOb.Checked
                .CleanPosterTBN = chkCleanPosterTBN.Checked
                .CleanPosterJPG = chkCleanPosterJPG.Checked
                .CleanMovieJPG = chkCleanMovieJPG.Checked
                .CleanMovieNameJPG = chkCleanMovieNameJPG.Checked
                .CleanDotFanartJPG = chkCleanDotFanartJPG.Checked
                .CleanExtrathumbs = chkCleanExtrathumbs.Checked
                .FileSystemCleanerWhitelist = False
                .FileSystemCleanerWhitelistExts.Clear()
            End If

            SaveMovieSetScraperTitleRenamer()

            If Not String.IsNullOrEmpty(txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPort.Text) Then
                .ProxyURI = txtProxyURI.Text
                .ProxyPort = Convert.ToInt32(txtProxyPort.Text)

                If Not String.IsNullOrEmpty(txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPassword.Text) Then
                    .ProxyCredentials.UserName = txtProxyUsername.Text
                    .ProxyCredentials.Password = txtProxyPassword.Text
                    .ProxyCredentials.Domain = txtProxyDomain.Text
                Else
                    .ProxyCredentials = New NetworkCredential
                End If
            Else
                .ProxyURI = String.Empty
                .ProxyPort = -1
            End If


            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            .MovieUseFrodo = chkMovieUseFrodo.Checked
            .MovieActorThumbsFrodo = chkMovieActorThumbsFrodo.Checked
            .MovieExtrafanartsFrodo = chkMovieExtrafanartsFrodo.Checked
            .MovieExtrathumbsFrodo = chkMovieExtrathumbsFrodo.Checked
            .MovieFanartFrodo = chkMovieFanartFrodo.Checked
            .MovieNFOFrodo = chkMovieNFOFrodo.Checked
            .MoviePosterFrodo = chkMoviePosterFrodo.Checked
            .MovieTrailerFrodo = chkMovieTrailerFrodo.Checked

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
            .MovieBannerExtended = chkMovieBannerExtended.Checked
            .MovieClearArtExtended = chkMovieClearArtExtended.Checked
            .MovieClearLogoExtended = chkMovieClearLogoExtended.Checked
            .MovieDiscArtExtended = chkMovieDiscArtExtended.Checked
            .MovieKeyartExtended = chkMovieKeyartExtended.Checked
            .MovieLandscapeExtended = chkMovieLandscapeExtended.Checked

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
            .MovieActorThumbsExpertSingle = chkMovieActorThumbsExpertSingle.Checked
            .MovieActorThumbsExtExpertSingle = txtMovieActorThumbsExtExpertSingle.Text
            .MovieBannerExpertSingle = txtMovieBannerExpertSingle.Text
            .MovieClearArtExpertSingle = txtMovieClearArtExpertSingle.Text
            .MovieClearLogoExpertSingle = txtMovieClearLogoExpertSingle.Text
            .MovieDiscArtExpertSingle = txtMovieDiscArtExpertSingle.Text
            .MovieExtrafanartsExpertSingle = chkMovieExtrafanartsExpertSingle.Checked
            .MovieExtrathumbsExpertSingle = chkMovieExtrathumbsExpertSingle.Checked
            .MovieFanartExpertSingle = txtMovieFanartExpertSingle.Text
            .MovieKeyartExpertSingle = txtMovieKeyartExpertSingle.Text
            .MovieLandscapeExpertSingle = txtMovieLandscapeExpertSingle.Text
            .MovieNFOExpertSingle = txtMovieNFOExpertSingle.Text
            .MoviePosterExpertSingle = txtMoviePosterExpertSingle.Text
            .MovieStackExpertSingle = chkMovieStackExpertSingle.Checked
            .MovieTrailerExpertSingle = txtMovieTrailerExpertSingle.Text
            .MovieUnstackExpertSingle = chkMovieUnstackExpertSingle.Checked

            '***************** Expert Multi ******************
            .MovieActorThumbsExpertMulti = chkMovieActorThumbsExpertMulti.Checked
            .MovieActorThumbsExtExpertMulti = txtMovieActorThumbsExtExpertMulti.Text
            .MovieBannerExpertMulti = txtMovieBannerExpertMulti.Text
            .MovieClearArtExpertMulti = txtMovieClearArtExpertMulti.Text
            .MovieClearLogoExpertMulti = txtMovieClearLogoExpertMulti.Text
            .MovieDiscArtExpertMulti = txtMovieDiscArtExpertMulti.Text
            .MovieFanartExpertMulti = txtMovieFanartExpertMulti.Text
            .MovieKeyartExpertMulti = txtMovieKeyartExpertMulti.Text
            .MovieLandscapeExpertMulti = txtMovieLandscapeExpertMulti.Text
            .MovieNFOExpertMulti = txtMovieNFOExpertMulti.Text
            .MoviePosterExpertMulti = txtMoviePosterExpertMulti.Text
            .MovieStackExpertMulti = chkMovieStackExpertMulti.Checked
            .MovieTrailerExpertMulti = txtMovieTrailerExpertMulti.Text
            .MovieUnstackExpertMulti = chkMovieUnstackExpertMulti.Checked

            '***************** Expert VTS ******************
            .MovieActorThumbsExpertVTS = chkMovieActorThumbsExpertVTS.Checked
            .MovieActorThumbsExtExpertVTS = txtMovieActorThumbsExtExpertVTS.Text
            .MovieBannerExpertVTS = txtMovieBannerExpertVTS.Text
            .MovieClearArtExpertVTS = txtMovieClearArtExpertVTS.Text
            .MovieClearLogoExpertVTS = txtMovieClearLogoExpertVTS.Text
            .MovieDiscArtExpertVTS = txtMovieDiscArtExpertVTS.Text
            .MovieExtrafanartsExpertVTS = chkMovieExtrafanartsExpertVTS.Checked
            .MovieExtrathumbsExpertVTS = chkMovieExtrathumbsExpertVTS.Checked
            .MovieFanartExpertVTS = txtMovieFanartExpertVTS.Text
            .MovieKeyartExpertVTS = txtMovieKeyartExpertVTS.Text
            .MovieLandscapeExpertVTS = txtMovieLandscapeExpertVTS.Text
            .MovieNFOExpertVTS = txtMovieNFOExpertVTS.Text
            .MoviePosterExpertVTS = txtMoviePosterExpertVTS.Text
            .MovieRecognizeVTSExpertVTS = chkMovieRecognizeVTSExpertVTS.Checked
            .MovieTrailerExpertVTS = txtMovieTrailerExpertVTS.Text
            .MovieUseBaseDirectoryExpertVTS = chkMovieUseBaseDirectoryExpertVTS.Checked

            '***************** Expert BDMV ******************
            .MovieActorThumbsExpertBDMV = chkMovieActorThumbsExpertBDMV.Checked
            .MovieActorThumbsExtExpertBDMV = txtMovieActorThumbsExtExpertBDMV.Text
            .MovieBannerExpertBDMV = txtMovieBannerExpertBDMV.Text
            .MovieClearArtExpertBDMV = txtMovieClearArtExpertBDMV.Text
            .MovieClearLogoExpertBDMV = txtMovieClearLogoExpertBDMV.Text
            .MovieDiscArtExpertBDMV = txtMovieDiscArtExpertBDMV.Text
            .MovieExtrafanartsExpertBDMV = chkMovieExtrafanartsExpertBDMV.Checked
            .MovieExtrathumbsExpertBDMV = chkMovieExtrathumbsExpertBDMV.Checked
            .MovieFanartExpertBDMV = txtMovieFanartExpertBDMV.Text
            .MovieKeyartExpertBDMV = txtMovieKeyartExpertBDMV.Text
            .MovieLandscapeExpertBDMV = txtMovieLandscapeExpertBDMV.Text
            .MovieNFOExpertBDMV = txtMovieNFOExpertBDMV.Text
            .MoviePosterExpertBDMV = txtMoviePosterExpertBDMV.Text
            .MovieTrailerExpertBDMV = txtMovieTrailerExpertBDMV.Text
            .MovieUseBaseDirectoryExpertBDMV = chkMovieUseBaseDirectoryExpertBDMV.Checked


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            .MovieSetUseMSAA = chkMovieSetUseMSAA.Checked
            .MovieSetBannerMSAA = chkMovieSetBannerMSAA.Checked
            .MovieSetClearArtMSAA = chkMovieSetClearArtMSAA.Checked
            .MovieSetClearLogoMSAA = chkMovieSetClearLogoMSAA.Checked
            .MovieSetFanartMSAA = chkMovieSetFanartMSAA.Checked
            .MovieSetLandscapeMSAA = chkMovieSetLandscapeMSAA.Checked
            .MovieSetPathMSAA = txtMovieSetPathMSAA.Text
            .MovieSetPosterMSAA = chkMovieSetPosterMSAA.Checked

            '********* XBMC Matrix Images settings ***********
            .MovieSetUseMatrix = chkMovieSetUseMatrix.Checked
            .MovieSetBannerMatrix = chkMovieSetBannerMatrix.Checked
            .MovieSetClearArtMatrix = chkMovieSetClearArtMatrix.Checked
            .MovieSetClearLogoMatrix = chkMovieSetClearLogoMatrix.Checked
            .MovieSetDiscArtMatrix = chkMovieSetDiscArtMatrix.Checked
            .MovieSetFanartMatrix = chkMovieSetFanartMatrix.Checked
            .MovieSetKeyartMatrix = chkMovieSetKeyartMatrix.Checked
            .MovieSetLandscapeMatrix = chkMovieSetLandscapeMatrix.Checked
            .MovieSetPathMatrix = txtMovieSetPathMatrix.Text
            .MovieSetPosterMatrix = chkMovieSetPosterMatrix.Checked

            '********* XBMC Extended Images settings ***********
            .MovieSetUseExtended = chkMovieSetUseExtended.Checked
            .MovieSetBannerExtended = chkMovieSetBannerExtended.Checked
            .MovieSetClearArtExtended = chkMovieSetClearArtExtended.Checked
            .MovieSetClearLogoExtended = chkMovieSetClearLogoExtended.Checked
            .MovieSetDiscArtExtended = chkMovieSetDiscArtExtended.Checked
            .MovieSetFanartExtended = chkMovieSetFanartExtended.Checked
            .MovieSetKeyartExtended = chkMovieSetKeyartExtended.Checked
            .MovieSetLandscapeExtended = chkMovieSetLandscapeExtended.Checked
            .MovieSetPathExtended = txtMovieSetPathExtended.Text
            .MovieSetPosterExtended = chkMovieSetPosterExtended.Checked

            '***************** Expert settings ****************
            .MovieSetUseExpert = chkMovieSetUseExpert.Checked

            '***************** Expert Single ******************
            .MovieSetBannerExpertSingle = txtMovieSetBannerExpertSingle.Text
            .MovieSetClearArtExpertSingle = txtMovieSetClearArtExpertSingle.Text
            .MovieSetClearLogoExpertSingle = txtMovieSetClearLogoExpertSingle.Text
            .MovieSetDiscArtExpertSingle = txtMovieSetDiscArtExpertSingle.Text
            .MovieSetFanartExpertSingle = txtMovieSetFanartExpertSingle.Text
            .MovieSetKeyartExpertSingle = txtMovieSetKeyartExpertSingle.Text
            .MovieSetLandscapeExpertSingle = txtMovieSetLandscapeExpertSingle.Text
            .MovieSetNFOExpertSingle = txtMovieSetNFOExpertSingle.Text
            .MovieSetPathExpertSingle = txtMovieSetPathExpertSingle.Text
            .MovieSetPosterExpertSingle = txtMovieSetPosterExpertSingle.Text


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

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
            .TVShowKeyartExtended = chkTVShowKeyartExtended.Checked
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
            .TVEpisodeActorThumbsExpert = chkTVEpisodeActorThumbsExpert.Checked
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
            .TVShowActorThumbsExpert = chkTVShowActorThumbsExpert.Checked
            .TVShowActorThumbsExtExpert = txtTVShowActorThumbsExtExpert.Text
            .TVShowBannerExpert = txtTVShowBannerExpert.Text
            .TVShowCharacterArtExpert = txtTVShowCharacterArtExpert.Text
            .TVShowClearArtExpert = txtTVShowClearArtExpert.Text
            .TVShowClearLogoExpert = txtTVShowClearLogoExpert.Text
            .TVShowExtrafanartsExpert = chkTVShowExtrafanartsExpert.Checked
            .TVShowFanartExpert = txtTVShowFanartExpert.Text
            .TVShowKeyartExpert = txtTVShowKeyartExpert.Text
            .TVShowLandscapeExpert = txtTVShowLandscapeExpert.Text
            .TVShowNFOExpert = txtTVShowNFOExpert.Text
            .TVShowPosterExpert = txtTVShowPosterExpert.Text
        End With

        For Each addon In Addons.Instance.Addons
            Try
                addon.AddonInterface.SaveSettings(Not isApply)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Next
        Addons.Instance.Settings_Save()
        Functions.CreateDefaultOptions()
    End Sub

    Private Sub TextBox_UIntegerOnly(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtMovieLevTolerance.KeyPress,
        txtMovieScraperCastLimit.KeyPress,
        txtMovieScraperCountryLimit.KeyPress,
        txtMovieScraperGenreLimit.KeyPress,
        txtMovieScraperOutlineLimit.KeyPress,
        txtMovieScraperStudioLimit.KeyPress,
        txtMovieSetBannerHeight.KeyPress,
        txtMovieSetBannerWidth.KeyPress,
        txtMovieSetFanartHeight.KeyPress,
        txtMovieSetFanartWidth.KeyPress,
        txtMovieSetKeyartHeight.KeyPress,
        txtMovieSetKeyartWidth.KeyPress,
        txtMovieSetPosterHeight.KeyPress,
        txtMovieSetPosterWidth.KeyPress,
        txtMovieSkipLessThan.KeyPress,
        txtProxyPort.KeyPress,
        txtTVAllSeasonsBannerHeight.KeyPress,
        txtTVAllSeasonsBannerWidth.KeyPress,
        txtTVAllSeasonsFanartHeight.KeyPress,
        txtTVAllSeasonsFanartWidth.KeyPress,
        txtTVAllSeasonsPosterHeight.KeyPress,
        txtTVAllSeasonsPosterWidth.KeyPress,
        txtTVEpisodeFanartHeight.KeyPress,
        txtTVEpisodeFanartWidth.KeyPress,
        txtTVEpisodePosterHeight.KeyPress,
        txtTVEpisodePosterWidth.KeyPress,
        txtTVScraperEpisodeActorsLimit.KeyPress,
        txtTVScraperEpisodeGuestStarsLimit.KeyPress,
        txtTVScraperShowActorsLimit.KeyPress,
        txtTVScraperShowGenreLimit.KeyPress,
        txtTVScraperShowStudioLimit.KeyPress,
        txtTVShowBannerHeight.KeyPress,
        txtTVShowBannerWidth.KeyPress,
        txtTVShowExtrafanartsHeight.KeyPress,
        txtTVShowExtrafanartsLimit.KeyPress,
        txtTVShowExtrafanartsWidth.KeyPress,
        txtTVShowFanartHeight.KeyPress,
        txtTVShowFanartWidth.KeyPress,
        txtTVShowKeyartHeight.KeyPress,
        txtTVShowKeyartWidth.KeyPress,
        txtTVShowPosterHeight.KeyPress,
        txtTVShowPosterWidth.KeyPress,
        txtTVSkipLessThan.KeyPress,
        txtTVScraperShowCountryLimit.KeyPress

        e.Handled = StringUtils.UIntegerOnly(e.KeyChar)
    End Sub

    Private Sub txtMovieBackdropsPath_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMovieSourcesBackdropsFolderPath.TextChanged
        Button_Apply_SetEnabled(True)

        If String.IsNullOrEmpty(txtMovieSourcesBackdropsFolderPath.Text) Then
            chkMovieSourcesBackdropsAuto.Checked = False
            chkMovieSourcesBackdropsAuto.Enabled = False
        Else
            chkMovieSourcesBackdropsAuto.Enabled = True
        End If
    End Sub

    Private Sub txtMovieScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMovieScraperDefFIExt.TextChanged
        btnMovieScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtMovieScraperDefFIExt.Text) AndAlso Not lstMovieScraperDefFIExt.Items.Contains(If(txtMovieScraperDefFIExt.Text.StartsWith("."), txtMovieScraperDefFIExt.Text, String.Concat(".", txtMovieScraperDefFIExt.Text)))
        If btnMovieScraperDefFIExtAdd.Enabled Then
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingRegex_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSourcesRegexTVShowMatchingRegex.TextChanged
        ValidateTVShowMatching()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingDefaultSeason_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSourcesRegexTVShowMatchingDefaultSeason.TextChanged
        ValidateTVShowMatching()
    End Sub

    Private Sub txtMovieSkipLessThan_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMovieSkipLessThan.TextChanged
        Button_Apply_SetEnabled(True)
        '_Results.NeedsDBClean_Movie = True
        '_Results.NeedsDBUpdate_Movie = True
    End Sub

    Private Sub txtTVSkipLessThan_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVSkipLessThan.TextChanged
        Button_Apply_SetEnabled(True)
        '_Results.NeedsDBClean_TV = True
        '_Results.NeedsDBUpdate_TV = True
    End Sub

    Private Sub txtTVScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTVScraperDefFIExt.TextChanged
        btnTVScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtTVScraperDefFIExt.Text) AndAlso Not lstTVScraperDefFIExt.Items.Contains(If(txtTVScraperDefFIExt.Text.StartsWith("."), txtTVScraperDefFIExt.Text, String.Concat(".", txtTVScraperDefFIExt.Text)))
        If btnTVScraperDefFIExtAdd.Enabled Then
            btnTVScraperDefFIExtEdit.Enabled = False
            btnTVScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub ValidateTVShowMatching()
        If Not String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingRegex.Text) AndAlso
            (String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) OrElse Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0) AndAlso
            CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) >= 0) Then
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = True
        Else
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        End If
    End Sub

    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer

        Public Sub New()
            col = 0
        End Sub

        Public Sub New(ByVal column As Integer)
            col = column
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Return String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Function
    End Class

    Private Sub btnMovieSetPathExtendedBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExtendedBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathExtended.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathMatrixBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathMatrixBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathMatrix.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathMSAABrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathMSAABrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathMSAA.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieSetPathExpertSingleBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExpertSingleBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieSetPathExpertSingle.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnMovieThemeTvTunesCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieThemeTvTunesCustomPathBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
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

    Private Sub btnMovieYAMJWatchedFilesBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieYAMJWatchedFilesBrowse.Click
        Try
            With fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1029, "Select the folder where you wish to store your watched files...")
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        txtMovieYAMJWatchedFolder.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnGeneralDaemonPathBrowse_Click(sender As Object, e As EventArgs) Handles btnGeneralDaemonPathBrowse.Click
        Try
            With fileBrowse
                .Filter = "Virtual Drive|DTAgent.exe;VCDMount.exe"
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) Then
                        txtGeneralDaemonPath.Text = .FileName
                        Button_Apply_SetEnabled(True)
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnGeneralDigitGrpSymbolSettings_Click(sender As Object, e As EventArgs) Handles btnGeneralDigitGrpSymbolSettings.Click
        Try
            Process.Start("INTL.CPL")
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkMovieUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieUseExpert.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieActorThumbsExpertBDMV.Enabled = chkMovieUseExpert.Checked
        chkMovieActorThumbsExpertMulti.Enabled = chkMovieUseExpert.Checked
        chkMovieActorThumbsExpertSingle.Enabled = chkMovieUseExpert.Checked
        chkMovieActorThumbsExpertVTS.Enabled = chkMovieUseExpert.Checked
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
        txtMovieKeyartExpertBDMV.Enabled = chkMovieUseExpert.Checked
        txtMovieKeyartExpertMulti.Enabled = chkMovieUseExpert.Checked
        txtMovieKeyartExpertSingle.Enabled = chkMovieUseExpert.Checked
        txtMovieKeyartExpertVTS.Enabled = chkMovieUseExpert.Checked
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

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieStackExpertSingle.CheckedChanged
        chkMovieUnstackExpertSingle.Enabled = chkMovieStackExpertSingle.Checked AndAlso chkMovieStackExpertSingle.Enabled
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieStackExpertMulti.CheckedChanged
        chkMovieUnstackExpertMulti.Enabled = chkMovieStackExpertMulti.Checked AndAlso chkMovieStackExpertMulti.Enabled
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        Button_Apply_SetEnabled(True)

        btnMovieSetPathExpertSingleBrowse.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetBannerExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearLogoExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetDiscArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetFanartExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetKeyartExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetLandscapeExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetNFOExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPathExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPosterExpertSingle.Enabled = chkMovieSetUseExpert.Checked
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseBoxee.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkTVUseAD_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseAD.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkTVUseExtended_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExtended.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVSeasonLandscapeExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowCharacterArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearArtExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowClearLogoExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowKeyartExtended.Enabled = chkTVUseExtended.Checked
        chkTVShowLandscapeExtended.Enabled = chkTVUseExtended.Checked

        If Not chkTVUseExtended.Checked Then
            chkTVSeasonLandscapeExtended.Checked = False
            chkTVShowCharacterArtExtended.Checked = False
            chkTVShowClearArtExtended.Checked = False
            chkTVShowClearLogoExtended.Checked = False
            chkTVShowKeyartExtended.Checked = False
            chkTVShowLandscapeExtended.Checked = False
        Else
            chkTVSeasonLandscapeExtended.Checked = True
            chkTVShowCharacterArtExtended.Checked = True
            chkTVShowClearArtExtended.Checked = True
            chkTVShowClearLogoExtended.Checked = True
            chkTVShowKeyartExtended.Checked = True
            chkTVShowLandscapeExtended.Checked = True
        End If
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseFrodo.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        Button_Apply_SetEnabled(True)

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
    End Sub

    Private Sub chkTVUseExpert_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTVUseExpert.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVEpisodeActorThumbsExpert.Enabled = chkTVUseExpert.Checked
        chkTVShowActorThumbsExpert.Enabled = chkTVUseExpert.Checked
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
        txtTVShowKeyartExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowLandscapeExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowNFOExpert.Enabled = chkTVUseExpert.Checked
        txtTVShowPosterExpert.Enabled = chkTVUseExpert.Checked
    End Sub

    Private Sub chkMovieSetClickScrape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovieSetClickScrape.CheckedChanged
        chkMovieSetClickScrapeAsk.Enabled = chkMovieSetClickScrape.Checked
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub pbMSAAInfo_Click(sender As Object, e As EventArgs) Handles pbMSAAInfo.Click
        Process.Start("http://forum.xbmc.org/showthread.php?tid=153502")
    End Sub

    Private Sub pbMSAAInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMSAAInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMSAAInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbMovieSourcesADInfo_Click(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:Artwork_Downloader")
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_Click(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.Click
        Process.Start("http://kodi.wiki/view/Add-on:TvTunes")
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesADInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMovieSourcesADInfo.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_MouseEnter(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.MouseEnter
        Cursor = Cursors.Hand
    End Sub

    Private Sub pbMovieSourcesTvTunesInfo_MouseLeave(sender As Object, e As EventArgs) Handles pbMovieSourcesTvTunesInfo.MouseLeave
        Cursor = Cursors.Default
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

    Private Sub btnMovieSetScraperMapperAdd_Click(sender As Object, e As EventArgs) Handles btnMovieSetScraperTitleRenamerAdd.Click
        Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
        dgvMovieSetScraperTitleRenamer.CurrentCell = dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0)
        dgvMovieSetScraperTitleRenamer.BeginEdit(True)
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub btnMovieSetScraperMapperRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieSetScraperTitleRenamerRemove.Click
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            dgvMovieSetScraperTitleRenamer.Rows.RemoveAt(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex)
            Button_Apply_SetEnabled(True)
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovieSetScraperTitleRenamer.CurrentCellDirtyStateChanged
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub dgvMovieSetScraperMapper_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovieSetScraperTitleRenamer.SelectionChanged
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            btnMovieSetScraperTitleRenamerRemove.Enabled = True
        Else
            btnMovieSetScraperTitleRenamerRemove.Enabled = False
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvMovieSetScraperTitleRenamer.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Private Sub chkMovieSetImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieSetImagesForceLanguage.CheckedChanged
        Button_Apply_SetEnabled(True)

        cbMovieSetImagesForcedLanguage.Enabled = chkMovieSetImagesForceLanguage.Checked
    End Sub

    Private Sub chkMovieSetImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkMovieSetImagesMediaLanguageOnly.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkMovieSetImagesGetBlankImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked
        chkMovieSetImagesGetEnglishImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked

        If Not chkMovieSetImagesMediaLanguageOnly.Checked Then
            chkMovieSetImagesGetBlankImages.Checked = False
            chkMovieSetImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkTVImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVImagesForceLanguage.CheckedChanged
        Button_Apply_SetEnabled(True)

        cbTVImagesForcedLanguage.Enabled = chkTVImagesForceLanguage.Checked
    End Sub

    Private Sub chkTVImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkTVImagesMediaLanguageOnly.CheckedChanged
        Button_Apply_SetEnabled(True)

        chkTVImagesGetBlankImages.Enabled = chkTVImagesMediaLanguageOnly.Checked
        chkTVImagesGetEnglishImages.Enabled = chkTVImagesMediaLanguageOnly.Checked

        If Not chkTVImagesMediaLanguageOnly.Checked Then
            chkTVImagesGetBlankImages.Checked = False
            chkTVImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkGeneralImageFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilter.CheckedChanged
        Button_Apply_SetEnabled(True)
        chkGeneralImageFilterAutoscraper.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterFanart.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterImagedialog.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterPoster.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
    End Sub

    Private Sub chkGeneralImageFilterAutoscraperImagedialog_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterAutoscraper.CheckedChanged, chkGeneralImageFilterImagedialog.CheckedChanged
        Button_Apply_SetEnabled(True)
        If chkGeneralImageFilterImagedialog.Checked = False AndAlso chkGeneralImageFilterAutoscraper.Checked = False Then
            chkGeneralImageFilterPoster.Enabled = False
            chkGeneralImageFilterFanart.Enabled = False
        Else
            chkGeneralImageFilterPoster.Enabled = True
            chkGeneralImageFilterFanart.Enabled = True
        End If
    End Sub
    Private Sub chkGeneralImageFilterPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterPoster.CheckedChanged
        Button_Apply_SetEnabled(True)
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
    End Sub
    Private Sub chkGeneralImageFilterFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkGeneralImageFilterFanart.CheckedChanged
        Button_Apply_SetEnabled(True)
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
    End Sub

    Private Sub txtGeneralImageFilterMatchRate_TextChanged(sender As Object, e As EventArgs) Handles txtGeneralImageFilterPosterMatchRate.LostFocus, txtGeneralImageFilterFanartMatchRate.LostFocus
        If chkGeneralImageFilter.Checked Then
            Dim txtbox As TextBox = CType(sender, TextBox)
            Dim matchfactor As Integer = 0
            Dim NotGood As Boolean = False
            If Integer.TryParse(txtbox.Text, matchfactor) Then
                If matchfactor < 0 OrElse matchfactor > 10 Then
                    NotGood = True
                End If
            Else
                NotGood = True
            End If
            If NotGood Then
                txtbox.Text = String.Empty
                MessageBox.Show(Master.eLang.GetString(1460, "Match Tolerance should be between 0 - 10 | 0 = 100% identical images, 10= different images"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbMovieGeneralCustomScrapeButtonDisabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
            cbMovieGeneralCustomScrapeButtonSelectionType.Enabled = False
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = False
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub rbMovieGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbMovieGeneralCustomScrapeButtonEnabled.Checked Then
            cbMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
            cbMovieGeneralCustomScrapeButtonSelectionType.Enabled = True
            txtMovieGeneralCustomScrapeButtonModifierType.Enabled = True
            txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub rbMovieSetGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieSetGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbMovieSetGeneralCustomScrapeButtonDisabled.Checked Then
            cbMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
            cbMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
            cbMovieSetGeneralCustomScrapeButtonSelectionType.Enabled = False
            txtMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
            txtMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub rbMovieSetGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbMovieSetGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbMovieSetGeneralCustomScrapeButtonEnabled.Checked Then
            cbMovieSetGeneralCustomScrapeButtonModifierType.Enabled = True
            cbMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = True
            cbMovieSetGeneralCustomScrapeButtonSelectionType.Enabled = True
            txtMovieSetGeneralCustomScrapeButtonModifierType.Enabled = True
            txtMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub rbTVGeneralCustomScrapeButtonDisabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbTVGeneralCustomScrapeButtonDisabled.CheckedChanged
        If rbTVGeneralCustomScrapeButtonDisabled.Checked Then
            cbTVGeneralCustomScrapeButtonModifierType.Enabled = False
            cbTVGeneralCustomScrapeButtonScrapeType.Enabled = False
            cbTVGeneralCustomScrapeButtonSelectionType.Enabled = False
            txtTVGeneralCustomScrapeButtonModifierType.Enabled = False
            txtTVGeneralCustomScrapeButtonScrapeType.Enabled = False
        End If
        Button_Apply_SetEnabled(True)
    End Sub

    Private Sub rbTVGeneralCustomScrapeButtonEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles rbTVGeneralCustomScrapeButtonEnabled.CheckedChanged
        If rbTVGeneralCustomScrapeButtonEnabled.Checked Then
            cbTVGeneralCustomScrapeButtonModifierType.Enabled = True
            cbTVGeneralCustomScrapeButtonScrapeType.Enabled = True
            cbTVGeneralCustomScrapeButtonSelectionType.Enabled = True
            txtTVGeneralCustomScrapeButtonModifierType.Enabled = True
            txtTVGeneralCustomScrapeButtonScrapeType.Enabled = True
        End If
        Button_Apply_SetEnabled(True)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure ButtonTag

        Dim Index As Integer
        Dim Title As String
        Dim PanelType As Enums.SettingsPanelType

    End Structure

#End Region 'Nested Types

End Class