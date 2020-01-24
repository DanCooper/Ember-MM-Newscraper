﻿' ################################################################################
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

Public Class dlgCustomScraper

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private CustomUpdater As New Structures.CustomUpdaterStruct
    Private _ContentType As Enums.ContentType

    'ScrapeModifiers
    Private mEpisodeActorThumbsAllowed As Boolean
    Private mEpisodeFanartAllowed As Boolean
    Private mEpisodeMetaDataAllowed As Boolean
    Private mEpisodeNFOAllowed As Boolean
    Private mEpisodePosterAllowed As Boolean
    Private mMainActorThumbsAllowed As Boolean
    Private mMainBannerAllowed As Boolean
    Private mMainCharacterArtAllowed As Boolean
    Private mMainClearArtAllowed As Boolean
    Private mMainClearLogoAllowed As Boolean
    Private mMainDiscArtAllowed As Boolean
    Private mMainExtrafanartsAllowed As Boolean
    Private mMainExtrathumbsAllowed As Boolean
    Private mMainFanartAllowed As Boolean
    Private mMainKeyArtAllowed As Boolean
    Private mMainLandscapeAllowed As Boolean
    Private mMainMetaDataAllowed As Boolean
    Private mMainNFOAllowed As Boolean
    Private mMainPosterAllowed As Boolean
    Private mMainThemeAllowed As Boolean
    Private mMainTrailerAllowed As Boolean
    Private mSeasonBannerAllowed As Boolean
    Private mSeasonFanartAllowed As Boolean
    Private mSeasonLandscapeAllowed As Boolean
    Private mSeasonPosterAllowed As Boolean

    'ScrapeOptions
    Private oEpisodeActorsAllowed As Boolean
    Private oEpisodeAiredAllowed As Boolean
    Private oEpisodeDirectorsAllowed As Boolean
    Private oEpisodeGuestStarsAllowed As Boolean
    Private oEpisodePlotAllowed As Boolean
    Private oEpisodeRatingAllowed As Boolean
    Private oEpisodeRuntimeAllowed As Boolean
    Private oEpisodeTitleAllowed As Boolean
    Private oEpisodeWritersAllowed As Boolean
    Private oMainActorsAllowed As Boolean
    Private oMainCertificationsAllowed As Boolean
    Private oMainCollectionAllowed As Boolean
    Private oMainCountriesAllowed As Boolean
    Private oMainCreatorsAllowed As Boolean
    Private oMainDirectorsAllowed As Boolean
    Private oMainEpisodeGuideURLAllowed As Boolean
    Private oMainGenresAllowed As Boolean
    Private oMainMPAAAllowed As Boolean
    Private oMainOriginalTitleAllowed As Boolean
    Private oMainOutlineAllowed As Boolean
    Private oMainPlotAllowed As Boolean
    Private oMainPremieredAllowed As Boolean
    Private oMainProducersAllowed As Boolean
    Private oMainRatingAllowed As Boolean
    Private oMainRuntimeAllowed As Boolean
    Private oMainStatusAllowed As Boolean
    Private oMainStudiosAllowed As Boolean
    Private oMainTaglineAllowed As Boolean
    Private oMainTitleAllowed As Boolean
    Private oMainTop250Allowed As Boolean
    Private oMainTrailerAllowed As Boolean
    Private oMainWritersAllowed As Boolean
    Private oSeasonAiredAllowed As Boolean
    Private oSeasonPlotAllowed As Boolean
    Private oSeasonTitleAllowed As Boolean

#End Region 'Fields

#Region "Dialog"

    Public Sub New(ByVal tContentType As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)

        _ContentType = tContentType
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Setup()

        Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            pnlTop.BackgroundImage = iBackground
        End Using

        'set defaults
        SetParameters()
        CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
        CustomUpdater.SelectionType = Enums.SelectionType.All
        Functions.SetScrapeModifiers(CustomUpdater.ScrapeModifiers, Enums.ModifierType.All, True)

        CheckEnable()
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
        If MyBase.ShowDialog() = DialogResult.OK Then
            CustomUpdater.Canceled = False
        Else
            CustomUpdater.Canceled = True
        End If
        Return CustomUpdater
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub btnEpisodeScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnEpisodeScrapeModifierNone.Click
        chkEpisodeModifierAll.Checked = False

        chkEpisodeModifierActorThumbs.Checked = False
        chkEpisodeModifierFanart.Checked = False
        chkEpisodeModifierMetaData.Checked = False
        chkEpisodeModifierNFO.Checked = False
        chkEpisodeModifierPoster.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnEpisodeScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnEpisodeScrapeOptionsNone.Click
        chkEpisodeOptionsAll.Checked = False

        chkEpisodeOptionsActors.Checked = False
        chkEpisodeOptionsAired.Checked = False
        chkEpisodeOptionsDirectors.Checked = False
        chkEpisodeOptionsGuestStars.Checked = False
        chkEpisodeOptionsPlot.Checked = False
        chkEpisodeOptionsRating.Checked = False
        chkEpisodeOptionsRuntime.Checked = False
        chkEpisodeOptionsTitle.Checked = False
        chkEpisodeOptionsWriters.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnMainScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnMainScrapeModifierNone.Click
        chkMainModifierAll.Checked = False

        chkMainModifierActorThumbs.Checked = False
        chkMainModifierBanner.Checked = False
        chkMainModifierCharacterArt.Checked = False
        chkMainModifierClearArt.Checked = False
        chkMainModifierClearLogo.Checked = False
        chkMainModifierDiscArt.Checked = False
        chkMainModifierExtrafanarts.Checked = False
        chkMainModifierExtrathumbs.Checked = False
        chkMainModifierFanart.Checked = False
        chkMainModifierKeyArt.Checked = False
        chkMainModifierLandscape.Checked = False
        chkMainModifierMetaData.Checked = False
        chkMainModifierNFO.Checked = False
        chkMainModifierPoster.Checked = False
        chkMainModifierTheme.Checked = False
        chkMainModifierTrailer.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnMainScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnMainScrapeOptionsNone.Click
        chkMainOptionsAll.Checked = False

        chkMainOptionsActors.Checked = False
        chkMainOptionsCertifications.Checked = False
        chkMainOptionsCollection.Checked = False
        chkMainOptionsCountries.Checked = False
        chkMainOptionsCreators.Checked = False
        chkMainOptionsDirectors.Checked = False
        chkMainOptionsEpisodeGuideURL.Checked = False
        chkMainOptionsGenres.Checked = False
        chkMainOptionsMPAA.Checked = False
        chkMainOptionsOriginalTitle.Checked = False
        chkMainOptionsOutline.Checked = False
        chkMainOptionsPlot.Checked = False
        chkMainOptionsPremiered.Checked = False
        chkMainOptionsRating.Checked = False
        chkMainOptionsRuntime.Checked = False
        chkMainOptionsStatus.Checked = False
        chkMainOptionsStudios.Checked = False
        chkMainOptionsTagline.Checked = False
        chkMainOptionsTitle.Checked = False
        chkMainOptionsTop250.Checked = False
        chkMainOptionsTrailer.Checked = False
        chkMainOptionsWriters.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSeasonScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnSeasonScrapeModifierNone.Click
        chkSeasonModifierAll.Checked = False

        chkSeasonModifierBanner.Checked = False
        chkSeasonModifierFanart.Checked = False
        chkSeasonModifierLandscape.Checked = False
        chkSeasonModifierPoster.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSeasonScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnSeasonScrapeOptionsNone.Click
        chkSeasonOptionsAll.Checked = False

        chkSeasonOptionsAired.Checked = False
        chkSeasonOptionsPlot.Checked = False
        chkSeasonOptionsTitle.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSpecialScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnSpecialScrapeModifierNone.Click
        chkSpecialModifierAll.Checked = False

        chkSpecialModifierWithEpisodes.Checked = False
        chkSpecialModifierWithSeasons.Checked = False

        CheckEnable()
    End Sub

    Private Sub CheckEnable()

        'Main Modifiers
        If chkMainModifierAll.Checked Then
            chkMainModifierActorThumbs.Checked = mMainActorThumbsAllowed
            chkMainModifierActorThumbs.Enabled = False
            chkMainModifierBanner.Checked = mMainBannerAllowed
            chkMainModifierBanner.Enabled = False
            chkMainModifierCharacterArt.Checked = mMainCharacterArtAllowed
            chkMainModifierCharacterArt.Enabled = False
            chkMainModifierClearArt.Checked = mMainClearArtAllowed
            chkMainModifierClearArt.Enabled = False
            chkMainModifierClearLogo.Checked = mMainClearLogoAllowed
            chkMainModifierClearLogo.Enabled = False
            chkMainModifierDiscArt.Checked = mMainDiscArtAllowed
            chkMainModifierDiscArt.Enabled = False
            chkMainModifierExtrafanarts.Checked = mMainExtrafanartsAllowed
            chkMainModifierExtrafanarts.Enabled = False
            chkMainModifierExtrathumbs.Checked = mMainExtrathumbsAllowed
            chkMainModifierExtrathumbs.Enabled = False
            chkMainModifierFanart.Checked = mMainFanartAllowed
            chkMainModifierFanart.Enabled = False
            chkMainModifierKeyArt.Checked = mMainKeyArtAllowed
            chkMainModifierKeyArt.Enabled = False
            chkMainModifierLandscape.Checked = mMainLandscapeAllowed
            chkMainModifierLandscape.Enabled = False
            chkMainModifierMetaData.Checked = mMainMetaDataAllowed
            chkMainModifierMetaData.Enabled = False
            chkMainModifierNFO.Checked = mMainNFOAllowed
            chkMainModifierNFO.Enabled = False
            chkMainModifierPoster.Checked = mMainPosterAllowed
            chkMainModifierPoster.Enabled = False
            chkMainModifierTheme.Checked = mMainThemeAllowed
            chkMainModifierTheme.Enabled = False
            chkMainModifierTrailer.Checked = mMainTrailerAllowed
            chkMainModifierTrailer.Enabled = False
        Else
            chkMainModifierActorThumbs.Enabled = mMainActorThumbsAllowed
            chkMainModifierBanner.Enabled = mMainBannerAllowed
            chkMainModifierCharacterArt.Enabled = mMainCharacterArtAllowed
            chkMainModifierClearArt.Enabled = mMainClearArtAllowed
            chkMainModifierClearLogo.Enabled = mMainClearLogoAllowed
            chkMainModifierDiscArt.Enabled = mMainDiscArtAllowed
            chkMainModifierExtrafanarts.Enabled = mMainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Enabled = mMainExtrathumbsAllowed
            chkMainModifierFanart.Enabled = mMainFanartAllowed
            chkMainModifierKeyArt.Enabled = mMainKeyArtAllowed
            chkMainModifierLandscape.Enabled = mMainLandscapeAllowed
            chkMainModifierMetaData.Enabled = mMainMetaDataAllowed
            chkMainModifierNFO.Enabled = mMainNFOAllowed
            chkMainModifierPoster.Enabled = mMainPosterAllowed
            chkMainModifierTheme.Enabled = mMainThemeAllowed
            chkMainModifierTrailer.Enabled = mMainTrailerAllowed
        End If

        'Main Options
        If chkMainModifierNFO.Checked Then
            gbMainScrapeOptions.Enabled = True
            If chkMainOptionsAll.Checked Then
                chkMainOptionsActors.Checked = oMainActorsAllowed
                chkMainOptionsActors.Enabled = False
                chkMainOptionsCertifications.Checked = oMainCertificationsAllowed
                chkMainOptionsCertifications.Enabled = False
                chkMainOptionsCollection.Checked = oMainCollectionAllowed
                chkMainOptionsCollection.Enabled = False
                chkMainOptionsCountries.Checked = oMainCountriesAllowed
                chkMainOptionsCountries.Enabled = False
                chkMainOptionsCreators.Checked = oMainCreatorsAllowed
                chkMainOptionsCreators.Enabled = False
                chkMainOptionsDirectors.Checked = oMainDirectorsAllowed
                chkMainOptionsDirectors.Enabled = False
                chkMainOptionsEpisodeGuideURL.Checked = oMainEpisodeGuideURLAllowed
                chkMainOptionsEpisodeGuideURL.Enabled = False
                chkMainOptionsGenres.Checked = oMainGenresAllowed
                chkMainOptionsGenres.Enabled = False
                chkMainOptionsMPAA.Checked = oMainMPAAAllowed
                chkMainOptionsMPAA.Enabled = False
                chkMainOptionsOriginalTitle.Checked = oMainOriginalTitleAllowed
                chkMainOptionsOriginalTitle.Enabled = False
                chkMainOptionsOutline.Checked = oMainOutlineAllowed
                chkMainOptionsOutline.Enabled = False
                chkMainOptionsPlot.Checked = oMainPlotAllowed
                chkMainOptionsPlot.Enabled = False
                chkMainOptionsPremiered.Checked = oMainPremieredAllowed
                chkMainOptionsPremiered.Enabled = False
                chkMainOptionsRating.Checked = oMainRatingAllowed
                chkMainOptionsRating.Enabled = False
                chkMainOptionsRuntime.Checked = oMainRuntimeAllowed
                chkMainOptionsRuntime.Enabled = False
                chkMainOptionsStatus.Checked = oMainStatusAllowed
                chkMainOptionsStatus.Enabled = False
                chkMainOptionsStudios.Checked = oMainStudiosAllowed
                chkMainOptionsStudios.Enabled = False
                chkMainOptionsTagline.Checked = oMainTaglineAllowed
                chkMainOptionsTagline.Enabled = False
                chkMainOptionsTitle.Checked = oMainTitleAllowed
                chkMainOptionsTitle.Enabled = False
                chkMainOptionsTop250.Checked = oMainTop250Allowed
                chkMainOptionsTop250.Enabled = False
                chkMainOptionsTrailer.Checked = oMainTrailerAllowed
                chkMainOptionsTrailer.Enabled = False
                chkMainOptionsWriters.Checked = oMainWritersAllowed
                chkMainOptionsWriters.Enabled = False
            Else
                chkMainOptionsActors.Enabled = oMainActorsAllowed
                chkMainOptionsCertifications.Enabled = oMainCertificationsAllowed
                chkMainOptionsCollection.Enabled = oMainCollectionAllowed
                chkMainOptionsCountries.Enabled = oMainCountriesAllowed
                chkMainOptionsCreators.Enabled = oMainCreatorsAllowed
                chkMainOptionsDirectors.Enabled = oMainDirectorsAllowed
                chkMainOptionsEpisodeGuideURL.Enabled = oMainEpisodeGuideURLAllowed
                chkMainOptionsGenres.Enabled = oMainGenresAllowed
                chkMainOptionsMPAA.Enabled = oMainMPAAAllowed
                chkMainOptionsOriginalTitle.Enabled = oMainOriginalTitleAllowed
                chkMainOptionsOutline.Enabled = oMainOutlineAllowed
                chkMainOptionsPlot.Enabled = oMainPlotAllowed
                chkMainOptionsPremiered.Enabled = oMainPremieredAllowed
                chkMainOptionsRating.Enabled = oMainRatingAllowed
                chkMainOptionsRuntime.Enabled = oMainRuntimeAllowed
                chkMainOptionsStatus.Enabled = oMainStatusAllowed
                chkMainOptionsStudios.Enabled = oMainStudiosAllowed
                chkMainOptionsTagline.Enabled = oMainTaglineAllowed
                chkMainOptionsTitle.Enabled = oMainTitleAllowed
                chkMainOptionsTop250.Enabled = oMainTop250Allowed
                chkMainOptionsTrailer.Enabled = oMainTrailerAllowed
                chkMainOptionsWriters.Enabled = oMainWritersAllowed
            End If
        Else
            gbMainScrapeOptions.Enabled = False
        End If

        'Special Modifiers
        If chkSpecialModifierAll.Checked Then
            chkSpecialModifierWithEpisodes.Checked = True
            chkSpecialModifierWithEpisodes.Enabled = False
            chkSpecialModifierWithSeasons.Checked = True
            chkSpecialModifierWithSeasons.Enabled = False
        Else
            chkSpecialModifierWithEpisodes.Enabled = True
            chkSpecialModifierWithSeasons.Enabled = True
        End If

        'with Episodes
        If chkSpecialModifierWithEpisodes.Checked Then
            gbEpisodeScrapeModifiers.Enabled = True

            'Episode Modifiers
            If chkEpisodeModifierAll.Checked Then
                chkEpisodeModifierActorThumbs.Checked = mEpisodeActorThumbsAllowed
                chkEpisodeModifierActorThumbs.Enabled = False
                chkEpisodeModifierFanart.Checked = mEpisodeFanartAllowed
                chkEpisodeModifierFanart.Enabled = False
                chkEpisodeModifierMetaData.Checked = mEpisodeMetaDataAllowed
                chkEpisodeModifierMetaData.Enabled = False
                chkEpisodeModifierNFO.Checked = mEpisodeNFOAllowed
                chkEpisodeModifierNFO.Enabled = False
                chkEpisodeModifierPoster.Checked = mEpisodePosterAllowed
                chkEpisodeModifierPoster.Enabled = False
            Else
                chkEpisodeModifierActorThumbs.Enabled = mEpisodeActorThumbsAllowed
                chkEpisodeModifierFanart.Enabled = mEpisodeFanartAllowed
                chkEpisodeModifierMetaData.Enabled = mEpisodeMetaDataAllowed
                chkEpisodeModifierNFO.Enabled = mEpisodeNFOAllowed
                chkEpisodeModifierPoster.Enabled = mEpisodePosterAllowed
            End If

            'Episode Options
            If chkEpisodeModifierNFO.Checked Then
                gbEpisodeScrapeOptions.Enabled = True
                If chkEpisodeOptionsAll.Checked Then
                    chkEpisodeOptionsActors.Checked = oEpisodeActorsAllowed
                    chkEpisodeOptionsActors.Enabled = False
                    chkEpisodeOptionsAired.Checked = oEpisodeAiredAllowed
                    chkEpisodeOptionsAired.Enabled = False
                    chkEpisodeOptionsDirectors.Checked = oEpisodeDirectorsAllowed
                    chkEpisodeOptionsDirectors.Enabled = False
                    chkEpisodeOptionsGuestStars.Checked = oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = False
                    chkEpisodeOptionsPlot.Checked = oEpisodePlotAllowed
                    chkEpisodeOptionsPlot.Enabled = False
                    chkEpisodeOptionsRating.Checked = oEpisodeRatingAllowed
                    chkEpisodeOptionsRating.Enabled = False
                    chkEpisodeOptionsRuntime.Checked = oEpisodeRuntimeAllowed
                    chkEpisodeOptionsRuntime.Enabled = False
                    chkEpisodeOptionsTitle.Checked = oEpisodeTitleAllowed
                    chkEpisodeOptionsTitle.Enabled = False
                    chkEpisodeOptionsWriters.Checked = oEpisodeWritersAllowed
                    chkEpisodeOptionsWriters.Enabled = False
                Else
                    chkEpisodeOptionsActors.Enabled = oEpisodeActorsAllowed
                    chkEpisodeOptionsAired.Enabled = oEpisodeAiredAllowed
                    chkEpisodeOptionsDirectors.Enabled = oEpisodeDirectorsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsPlot.Enabled = oEpisodePlotAllowed
                    chkEpisodeOptionsRating.Enabled = oEpisodeRatingAllowed
                    chkEpisodeOptionsRuntime.Enabled = oEpisodeRuntimeAllowed
                    chkEpisodeOptionsTitle.Enabled = oEpisodeTitleAllowed
                    chkEpisodeOptionsWriters.Enabled = oEpisodeWritersAllowed
                End If
            Else
                gbEpisodeScrapeOptions.Enabled = False
            End If
        Else
            gbEpisodeScrapeModifiers.Enabled = False
            gbEpisodeScrapeOptions.Enabled = False
        End If

        'with Seasons
        If chkSpecialModifierWithSeasons.Checked Then
            gbSeasonScrapeModifiers.Enabled = True

            'Season Modifiers
            If chkSeasonModifierAll.Checked Then
                chkSeasonModifierBanner.Checked = mSeasonBannerAllowed
                chkSeasonModifierBanner.Enabled = False
                chkSeasonModifierFanart.Checked = mSeasonFanartAllowed
                chkSeasonModifierFanart.Enabled = False
                chkSeasonModifierLandscape.Checked = mSeasonLandscapeAllowed
                chkSeasonModifierLandscape.Enabled = False
                chkSeasonModifierPoster.Checked = mSeasonPosterAllowed
                chkSeasonModifierPoster.Enabled = False
            Else
                chkSeasonModifierBanner.Enabled = mSeasonBannerAllowed
                chkSeasonModifierFanart.Enabled = mSeasonFanartAllowed
                chkSeasonModifierLandscape.Enabled = mSeasonLandscapeAllowed
                chkSeasonModifierPoster.Enabled = mSeasonPosterAllowed
            End If

            'Season Options
            If chkMainModifierNFO.Checked Then 'TODO: check. Atm we save the season infos to tv show NFO
                gbSeasonScrapeOptions.Enabled = True
                If chkSeasonOptionsAll.Checked Then
                    chkSeasonOptionsAired.Checked = oSeasonAiredAllowed
                    chkSeasonOptionsAired.Enabled = False
                    chkSeasonOptionsPlot.Checked = oSeasonPlotAllowed
                    chkSeasonOptionsPlot.Enabled = False
                    chkSeasonOptionsTitle.Checked = oSeasonTitleAllowed
                    chkSeasonOptionsTitle.Enabled = False
                Else
                    chkSeasonOptionsAired.Enabled = oSeasonAiredAllowed
                    chkSeasonOptionsPlot.Enabled = oSeasonPlotAllowed
                    chkSeasonOptionsTitle.Enabled = oSeasonTitleAllowed
                End If
            Else
                gbSeasonScrapeOptions.Enabled = False
            End If
        Else
            gbSeasonScrapeModifiers.Enabled = False
            gbSeasonScrapeOptions.Enabled = False
        End If

        'Scrape Modifiers
        CustomUpdater.ScrapeModifiers.EpisodeActorThumbs = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifiers.EpisodeFanart = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.EpisodeMeta = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierMetaData.Checked
        CustomUpdater.ScrapeModifiers.EpisodeNFO = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked
        CustomUpdater.ScrapeModifiers.EpisodePoster = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.MainActorthumbs = chkMainModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifiers.MainBanner = chkMainModifierBanner.Checked
        CustomUpdater.ScrapeModifiers.MainCharacterArt = chkMainModifierCharacterArt.Checked
        CustomUpdater.ScrapeModifiers.MainClearArt = chkMainModifierClearArt.Checked
        CustomUpdater.ScrapeModifiers.MainClearLogo = chkMainModifierClearLogo.Checked
        CustomUpdater.ScrapeModifiers.MainDiscArt = chkMainModifierDiscArt.Checked
        CustomUpdater.ScrapeModifiers.MainFanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.MainExtrathumbs = chkMainModifierExtrathumbs.Checked
        CustomUpdater.ScrapeModifiers.MainExtrafanarts = chkMainModifierExtrafanarts.Checked
        CustomUpdater.ScrapeModifiers.MainFanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.MainKeyArt = chkMainModifierKeyArt.Checked
        CustomUpdater.ScrapeModifiers.MainLandscape = chkMainModifierLandscape.Checked
        CustomUpdater.ScrapeModifiers.MainMeta = chkMainModifierMetaData.Checked
        CustomUpdater.ScrapeModifiers.MainNFO = chkMainModifierNFO.Checked
        CustomUpdater.ScrapeModifiers.MainPoster = chkMainModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.MainTheme = chkMainModifierTheme.Checked
        CustomUpdater.ScrapeModifiers.MainTrailer = chkMainModifierTrailer.Checked
        CustomUpdater.ScrapeModifiers.SeasonBanner = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierBanner.Checked
        CustomUpdater.ScrapeModifiers.SeasonFanart = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.SeasonLandscape = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierLandscape.Checked
        CustomUpdater.ScrapeModifiers.SeasonPoster = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.withEpisodes = chkSpecialModifierWithEpisodes.Checked
        CustomUpdater.ScrapeModifiers.withSeasons = chkSpecialModifierWithSeasons.Checked
        CustomUpdater.ScrapeModifiers.AllSeasonsBanner = CustomUpdater.ScrapeModifiers.SeasonBanner
        CustomUpdater.ScrapeModifiers.AllSeasonsFanart = CustomUpdater.ScrapeModifiers.SeasonFanart
        CustomUpdater.ScrapeModifiers.AllSeasonsLandscape = CustomUpdater.ScrapeModifiers.SeasonLandscape
        CustomUpdater.ScrapeModifiers.AllSeasonsPoster = CustomUpdater.ScrapeModifiers.SeasonPoster

        'Scrape Options
        CustomUpdater.ScrapeOptions.Episodes.Actors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.Episodes.Aired = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsAired.Checked
        CustomUpdater.ScrapeOptions.Episodes.Credits = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.Episodes.Directors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.Episodes.GuestStars = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsGuestStars.Checked
        CustomUpdater.ScrapeOptions.Episodes.Plot = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.Episodes.Ratings = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRating.Checked
        CustomUpdater.ScrapeOptions.Episodes.Runtime = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.Episodes.Title = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.Actors = chkMainModifierNFO.Checked AndAlso chkMainOptionsActors.Checked
        CustomUpdater.ScrapeOptions.Certifications = chkMainModifierNFO.Checked AndAlso chkMainOptionsCertifications.Checked
        CustomUpdater.ScrapeOptions.Collection = chkMainModifierNFO.Checked AndAlso chkMainOptionsCollection.Checked
        CustomUpdater.ScrapeOptions.Countries = chkMainModifierNFO.Checked AndAlso chkMainOptionsCountries.Checked
        CustomUpdater.ScrapeOptions.Creators = chkMainModifierNFO.Checked AndAlso chkMainOptionsCreators.Checked
        CustomUpdater.ScrapeOptions.Directors = chkMainModifierNFO.Checked AndAlso chkMainOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.EpisodeGuideURL = chkMainModifierNFO.Checked AndAlso chkMainOptionsEpisodeGuideURL.Checked
        CustomUpdater.ScrapeOptions.Genres = chkMainModifierNFO.Checked AndAlso chkMainOptionsGenres.Checked
        CustomUpdater.ScrapeOptions.MPAA = chkMainModifierNFO.Checked AndAlso chkMainOptionsMPAA.Checked
        CustomUpdater.ScrapeOptions.OriginalTitle = chkMainModifierNFO.Checked AndAlso chkMainOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.Outline = chkMainModifierNFO.Checked AndAlso chkMainOptionsOutline.Checked
        CustomUpdater.ScrapeOptions.Plot = chkMainModifierNFO.Checked AndAlso chkMainOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.Premiered = chkMainModifierNFO.Checked AndAlso chkMainOptionsPremiered.Checked
        CustomUpdater.ScrapeOptions.Ratings = chkMainModifierNFO.Checked AndAlso chkMainOptionsRating.Checked
        CustomUpdater.ScrapeOptions.Runtime = chkMainModifierNFO.Checked AndAlso chkMainOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.Status = chkMainModifierNFO.Checked AndAlso chkMainOptionsStatus.Checked
        CustomUpdater.ScrapeOptions.Studios = chkMainModifierNFO.Checked AndAlso chkMainOptionsStudios.Checked
        CustomUpdater.ScrapeOptions.Tagline = chkMainModifierNFO.Checked AndAlso chkMainOptionsTagline.Checked
        CustomUpdater.ScrapeOptions.Title = chkMainModifierNFO.Checked AndAlso chkMainOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.Top250 = chkMainModifierNFO.Checked AndAlso chkMainOptionsTop250.Checked
        CustomUpdater.ScrapeOptions.Trailer = chkMainModifierNFO.Checked AndAlso chkMainOptionsTrailer.Checked
        CustomUpdater.ScrapeOptions.Credits = chkMainModifierNFO.Checked AndAlso chkMainOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.Seasons.Aired = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsAired.Checked   'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.Seasons.Plot = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsPlot.Checked     'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.Seasons.Title = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsTitle.Checked     'TODO: check. Atm we save the season infos to tv show NFO

        If CustomUpdater.ScrapeModifiers.EpisodeActorThumbs OrElse
            CustomUpdater.ScrapeModifiers.EpisodeFanart OrElse
            CustomUpdater.ScrapeModifiers.EpisodeMeta OrElse
            CustomUpdater.ScrapeModifiers.EpisodePoster OrElse
            CustomUpdater.ScrapeModifiers.EpisodeSubtitles OrElse
            CustomUpdater.ScrapeModifiers.MainActorthumbs OrElse
            CustomUpdater.ScrapeModifiers.MainBanner OrElse
            CustomUpdater.ScrapeModifiers.MainCharacterArt OrElse
            CustomUpdater.ScrapeModifiers.MainClearArt OrElse
            CustomUpdater.ScrapeModifiers.MainClearLogo OrElse
            CustomUpdater.ScrapeModifiers.MainDiscArt OrElse
            CustomUpdater.ScrapeModifiers.MainExtrafanarts OrElse
            CustomUpdater.ScrapeModifiers.MainExtrathumbs OrElse
            CustomUpdater.ScrapeModifiers.MainFanart OrElse
            CustomUpdater.ScrapeModifiers.MainKeyArt OrElse
            CustomUpdater.ScrapeModifiers.MainLandscape OrElse
            CustomUpdater.ScrapeModifiers.MainMeta OrElse
            CustomUpdater.ScrapeModifiers.MainPoster OrElse
            CustomUpdater.ScrapeModifiers.MainSubtitles OrElse
            CustomUpdater.ScrapeModifiers.MainTheme OrElse
            CustomUpdater.ScrapeModifiers.MainTrailer OrElse
            CustomUpdater.ScrapeModifiers.SeasonBanner OrElse
            CustomUpdater.ScrapeModifiers.SeasonFanart OrElse
            CustomUpdater.ScrapeModifiers.SeasonLandscape OrElse
            CustomUpdater.ScrapeModifiers.SeasonPoster Then
            btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifiers.EpisodeNFO AndAlso (
            CustomUpdater.ScrapeOptions.Episodes.Actors OrElse
            CustomUpdater.ScrapeOptions.Episodes.Aired OrElse
            CustomUpdater.ScrapeOptions.Episodes.Credits OrElse
            CustomUpdater.ScrapeOptions.Episodes.Directors OrElse
            CustomUpdater.ScrapeOptions.Episodes.GuestStars OrElse
            CustomUpdater.ScrapeOptions.Episodes.Plot OrElse
            CustomUpdater.ScrapeOptions.Episodes.Ratings OrElse
            CustomUpdater.ScrapeOptions.Episodes.Runtime OrElse
            CustomUpdater.ScrapeOptions.Episodes.Title) Then
            btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifiers.MainNFO AndAlso (
            CustomUpdater.ScrapeOptions.Actors OrElse
            CustomUpdater.ScrapeOptions.Certifications OrElse
            CustomUpdater.ScrapeOptions.Collection OrElse
            CustomUpdater.ScrapeOptions.Countries OrElse
            CustomUpdater.ScrapeOptions.Creators OrElse
            CustomUpdater.ScrapeOptions.Directors OrElse
            CustomUpdater.ScrapeOptions.EpisodeGuideURL OrElse
            CustomUpdater.ScrapeOptions.Genres OrElse
            CustomUpdater.ScrapeOptions.MPAA OrElse
            CustomUpdater.ScrapeOptions.OriginalTitle OrElse
            CustomUpdater.ScrapeOptions.Outline OrElse
            CustomUpdater.ScrapeOptions.Plot OrElse
            CustomUpdater.ScrapeOptions.Premiered OrElse
            CustomUpdater.ScrapeOptions.Ratings OrElse
            CustomUpdater.ScrapeOptions.Runtime OrElse
            CustomUpdater.ScrapeOptions.Status OrElse
            CustomUpdater.ScrapeOptions.Studios OrElse
            CustomUpdater.ScrapeOptions.Tagline OrElse
            CustomUpdater.ScrapeOptions.Tags OrElse
            CustomUpdater.ScrapeOptions.Title OrElse
            CustomUpdater.ScrapeOptions.Top250 OrElse
            CustomUpdater.ScrapeOptions.Trailer OrElse
            CustomUpdater.ScrapeOptions.Credits OrElse
            CustomUpdater.ScrapeOptions.Seasons.Aired OrElse
            CustomUpdater.ScrapeOptions.Seasons.Plot OrElse
            CustomUpdater.ScrapeOptions.Seasons.Title) Then
            btnOK.Enabled = True
        Else
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub SetParameters()
        Dim NameID As String = String.Empty
        Dim NameTable As String = String.Empty

        With Master.eSettings
            Select Case _ContentType
                Case Enums.ContentType.Movie
                    NameID = Database.Helpers.GetMainIdName(Database.TableName.movie)
                    NameTable = Database.Helpers.GetTableName(Database.TableName.movie)
                    gbEpisodeScrapeModifiers.Visible = False
                    gbEpisodeScrapeOptions.Visible = False
                    gbSeasonScrapeModifiers.Visible = False
                    gbSeasonScrapeOptions.Visible = False
                    gbSpecialScrapeModifiers.Visible = False

                    mEpisodeActorThumbsAllowed = False
                    mEpisodeFanartAllowed = False
                    mEpisodeMetaDataAllowed = False
                    mEpisodeNFOAllowed = False
                    mEpisodePosterAllowed = False
                    mMainActorThumbsAllowed = .MovieActorThumbsAnyEnabled
                    mMainBannerAllowed = .MovieBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = .MovieExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = .MovieExtrathumbsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainFanartAllowed = .MovieFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainKeyArtAllowed = .MovieKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mMainLandscapeAllowed = .MovieLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = .Movie.DataSettings.MetadataScan.Enabled
                    mMainNFOAllowed = .MovieNFOAnyEnabled
                    mMainPosterAllowed = .MoviePosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .MovieThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    mMainTrailerAllowed = .MovieTrailerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    mSeasonBannerAllowed = False
                    mSeasonFanartAllowed = False
                    mSeasonLandscapeAllowed = False
                    mSeasonPosterAllowed = False

                    With Master.eSettings.Movie.DataSettings
                        oEpisodeActorsAllowed = False
                        oEpisodeAiredAllowed = False
                        oEpisodeDirectorsAllowed = False
                        oEpisodeGuestStarsAllowed = False
                        oEpisodePlotAllowed = False
                        oEpisodeRatingAllowed = False
                        oEpisodeRuntimeAllowed = False
                        oEpisodeTitleAllowed = False
                        oEpisodeWritersAllowed = False
                        oMainActorsAllowed = .Actors.Enabled
                        oMainCertificationsAllowed = .Certifications.Enabled
                        oMainCollectionAllowed = .Collection.Enabled
                        oMainCountriesAllowed = .Countries.Enabled
                        oMainCreatorsAllowed = False
                        oMainDirectorsAllowed = .Directors.Enabled
                        oMainEpisodeGuideURLAllowed = False
                        oMainGenresAllowed = .Genres.Enabled
                        oMainMPAAAllowed = .MPAA.Enabled
                        oMainOriginalTitleAllowed = .OriginalTitle.Enabled
                        oMainOutlineAllowed = .Outline.Enabled
                        oMainPlotAllowed = .Plot.Enabled
                        oMainPremieredAllowed = .Premiered.Enabled
                        oMainProducersAllowed = False
                        oMainRatingAllowed = .Ratings.Enabled
                        oMainRuntimeAllowed = .Runtime.Enabled
                        oMainStatusAllowed = False
                        oMainStudiosAllowed = .Studios.Enabled
                        oMainTaglineAllowed = .Tagline.Enabled
                        oMainTitleAllowed = .Title.Enabled
                        oMainTop250Allowed = .Top250.Enabled
                        oMainTrailerAllowed = .TrailerLink.Enabled
                        oMainWritersAllowed = .Credits.Enabled
                        oSeasonAiredAllowed = False
                        oSeasonPlotAllowed = False
                        oSeasonTitleAllowed = False
                    End With

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListMovies.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListMovies.Rows.Count)
                    rbScrapeType_Selected.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count)

                Case Enums.ContentType.Movieset
                    NameID = Database.Helpers.GetMainIdName(Database.TableName.movieset)
                    NameTable = Database.Helpers.GetTableName(Database.TableName.movieset)
                    gbEpisodeScrapeModifiers.Visible = False
                    gbEpisodeScrapeOptions.Visible = False
                    gbSeasonScrapeModifiers.Visible = False
                    gbSeasonScrapeOptions.Visible = False
                    gbSpecialScrapeModifiers.Visible = False

                    mEpisodeActorThumbsAllowed = False
                    mEpisodeFanartAllowed = False
                    mEpisodeMetaDataAllowed = False
                    mEpisodeNFOAllowed = False
                    mEpisodePosterAllowed = False
                    mMainActorThumbsAllowed = False
                    mMainBannerAllowed = .MovieSetBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieSetClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieSetClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieSetDiscArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = False
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .MovieSetFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    mMainKeyArtAllowed = False
                    mMainLandscapeAllowed = .MovieSetLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .MovieSetNFOAnyEnabled
                    mMainPosterAllowed = .MovieSetPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = False
                    mMainTrailerAllowed = False
                    mSeasonBannerAllowed = False
                    mSeasonFanartAllowed = False
                    mSeasonLandscapeAllowed = False
                    mSeasonPosterAllowed = False

                    oEpisodeActorsAllowed = False
                    oEpisodeAiredAllowed = False
                    oEpisodeDirectorsAllowed = False
                    oEpisodeGuestStarsAllowed = False
                    oEpisodePlotAllowed = False
                    oEpisodeRatingAllowed = False
                    oEpisodeRuntimeAllowed = False
                    oEpisodeTitleAllowed = False
                    oEpisodeWritersAllowed = False
                    oMainActorsAllowed = False
                    oMainCertificationsAllowed = False
                    oMainCollectionAllowed = False
                    oMainCountriesAllowed = False
                    oMainCreatorsAllowed = False
                    oMainDirectorsAllowed = False
                    oMainEpisodeGuideURLAllowed = False
                    oMainGenresAllowed = False
                    oMainMPAAAllowed = False
                    oMainOriginalTitleAllowed = False
                    oMainOutlineAllowed = False
                    oMainPlotAllowed = .MoviesetScraperPlot
                    oMainPremieredAllowed = False
                    oMainProducersAllowed = False
                    oMainRatingAllowed = False
                    oMainRuntimeAllowed = False
                    oMainStatusAllowed = False
                    oMainStudiosAllowed = False
                    oMainTaglineAllowed = False
                    oMainTitleAllowed = .MoviesetScraperTitle
                    oMainTop250Allowed = False
                    oMainTrailerAllowed = False
                    oMainWritersAllowed = False
                    oSeasonAiredAllowed = False
                    oSeasonPlotAllowed = False
                    oSeasonTitleAllowed = False

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count)
                    rbScrapeType_Selected.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count)

                Case Enums.ContentType.TV
                    NameID = Database.Helpers.GetMainIdName(Database.TableName.tvshow)
                    NameTable = Database.Helpers.GetTableName(Database.TableName.tvshow)

                    mEpisodeActorThumbsAllowed = .TVEpisodeActorThumbsAnyEnabled
                    mEpisodeFanartAllowed = .TVEpisodeFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mEpisodeMetaDataAllowed = .TVScraperMetaDataScan
                    mEpisodeNFOAllowed = .TVEpisodeNFOAnyEnabled
                    mEpisodePosterAllowed = .TVEpisodePosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    mMainActorThumbsAllowed = .TVShowActorThumbsAnyEnabled
                    mMainBannerAllowed = .TVShowBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = .TVShowCharacterArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mMainClearArtAllowed = .TVShowClearArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .TVShowClearLogoAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = False
                    mMainExtrafanartsAllowed = .TVShowExtrafanartsAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .TVShowFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainKeyArtAllowed = .TVShowKeyArtAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mMainLandscapeAllowed = .TVShowLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .TVShowNFOAnyEnabled
                    mMainPosterAllowed = .TVShowPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .TvShowThemeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                    mMainTrailerAllowed = False
                    mSeasonBannerAllowed = .TVSeasonBannerAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    mSeasonFanartAllowed = .TVSeasonFanartAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mSeasonLandscapeAllowed = .TVSeasonLandscapeAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mSeasonPosterAllowed = .TVSeasonPosterAnyEnabled AndAlso AddonsManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

                    oEpisodeActorsAllowed = .TVScraperEpisodeActors
                    oEpisodeAiredAllowed = .TVScraperEpisodeAired
                    oEpisodeDirectorsAllowed = .TVScraperEpisodeDirector
                    oEpisodeGuestStarsAllowed = .TVScraperEpisodeGuestStars
                    oEpisodePlotAllowed = .TVScraperEpisodePlot
                    oEpisodeRatingAllowed = .TVScraperEpisodeRating
                    oEpisodeRuntimeAllowed = .TVScraperEpisodeRuntime
                    oEpisodeTitleAllowed = .TVScraperEpisodeTitle
                    oEpisodeWritersAllowed = .TVScraperEpisodeCredits
                    oMainActorsAllowed = .TVScraperShowActors
                    oMainCertificationsAllowed = .TVScraperShowCert
                    oMainCollectionAllowed = False
                    oMainCountriesAllowed = .TVScraperShowCountry
                    oMainCreatorsAllowed = .TVScraperShowCreators
                    oMainDirectorsAllowed = False
                    oMainEpisodeGuideURLAllowed = .TVScraperShowEpiGuideURL
                    oMainGenresAllowed = .TVScraperShowGenre
                    oMainMPAAAllowed = .TVScraperShowMPAA
                    oMainOriginalTitleAllowed = .TVScraperShowOriginalTitle
                    oMainOutlineAllowed = False
                    oMainPlotAllowed = .TVScraperShowPlot
                    oMainPremieredAllowed = .TVScraperShowPremiered
                    oMainProducersAllowed = False
                    oMainRatingAllowed = .TVScraperShowRating
                    oMainRuntimeAllowed = .TVScraperShowRuntime
                    oMainStatusAllowed = .TVScraperShowStatus
                    oMainStudiosAllowed = .TVScraperShowStudio
                    oMainTaglineAllowed = False
                    oMainTitleAllowed = .TVScraperShowTitle
                    oMainTop250Allowed = False
                    oMainTrailerAllowed = False
                    oMainWritersAllowed = False
                    oSeasonAiredAllowed = .TVScraperSeasonAired
                    oSeasonPlotAllowed = .TVScraperSeasonPlot
                    oSeasonTitleAllowed = .TVScraperSeasonTitle

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True
                    chkSpecialModifierAll.Checked = True
                    chkEpisodeModifierAll.Checked = True
                    chkEpisodeOptionsAll.Checked = True
                    chkSeasonModifierAll.Checked = True
                    chkSeasonOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListTVShows.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListTVShows.Rows.Count)
                    rbScrapeType_Selected.Enabled = AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count)
            End Select
        End With

        'check if we have "New" or "Marked" medias
        If Not String.IsNullOrEmpty(NameID) AndAlso Not String.IsNullOrEmpty(NameTable) Then
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS ncount FROM {1} WHERE {2} = 1;", NameID, NameTable, Database.Helpers.GetColumnName(Database.ColumnName.New))
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
                    rbScrapeType_New.Text = String.Format(String.Concat(Master.eLang.GetString(47, "New"), " ({0})"), Convert.ToInt32(SQLcount("ncount")))
                End Using

                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS mcount FROM {1} WHERE {2} = 1;", NameID, NameTable, Database.Helpers.GetColumnName(Database.ColumnName.Marked))
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_Marked.Enabled = Convert.ToInt32(SQLcount("mcount")) > 0
                    rbScrapeType_Marked.Text = String.Format(String.Concat(Master.eLang.GetString(48, "Marked"), " ({0})"), Convert.ToInt32(SQLcount("mcount")))
                End Using
            End Using
        End If
    End Sub

    Private Sub rbUpdateModifier_All_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_All.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.All
        End Select
    End Sub

    Private Sub rbUpdateModifier_Filter_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Filter.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
        End Select
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Marked.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
        End Select
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Missing.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
        End Select
    End Sub

    Private Sub rbUpdateModifier_New_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_New.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
        End Select
    End Sub

    Private Sub rbUpdateModifier_Selected_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Selected.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Ask_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Ask.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Auto.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Skip.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Missing
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub CheckBoxes_Click(sender As Object, e As EventArgs) Handles _
        chkEpisodeModifierActorThumbs.Click,
        chkEpisodeModifierAll.Click,
        chkEpisodeModifierFanart.Click,
        chkEpisodeModifierMetaData.Click,
        chkEpisodeModifierNFO.Click,
        chkEpisodeModifierPoster.Click,
        chkEpisodeOptionsActors.Click,
        chkEpisodeOptionsAired.Click,
        chkEpisodeOptionsAll.Click,
        chkEpisodeOptionsDirectors.Click,
        chkEpisodeOptionsGuestStars.Click,
        chkEpisodeOptionsPlot.Click,
        chkEpisodeOptionsRating.Click,
        chkEpisodeOptionsRuntime.Click,
        chkEpisodeOptionsTitle.Click,
        chkEpisodeOptionsWriters.Click,
        chkMainModifierActorThumbs.Click,
        chkMainModifierAll.Click,
        chkMainModifierBanner.Click,
        chkMainModifierCharacterArt.Click,
        chkMainModifierClearArt.Click,
        chkMainModifierClearLogo.Click,
        chkMainModifierDiscArt.Click,
        chkMainModifierExtrafanarts.Click,
        chkMainModifierExtrathumbs.Click,
        chkMainModifierFanart.Click,
        chkMainModifierKeyArt.Click,
        chkMainModifierLandscape.Click,
        chkMainModifierMetaData.Click,
        chkMainModifierNFO.Click,
        chkMainModifierPoster.Click,
        chkMainModifierTheme.Click,
        chkMainModifierTrailer.Click,
        chkMainOptionsActors.Click,
        chkMainOptionsAll.Click,
        chkMainOptionsCertifications.Click,
        chkMainOptionsCollection.Click,
        chkMainOptionsCountries.Click,
        chkMainOptionsCreators.Click,
        chkMainOptionsDirectors.Click,
        chkMainOptionsEpisodeGuideURL.Click,
        chkMainOptionsGenres.Click,
        chkMainOptionsMPAA.Click,
        chkMainOptionsOriginalTitle.Click,
        chkMainOptionsOutline.Click,
        chkMainOptionsPlot.Click,
        chkMainOptionsPremiered.Click,
        chkMainOptionsRating.Click,
        chkMainOptionsRuntime.Click,
        chkMainOptionsStatus.Click,
        chkMainOptionsStudios.Click,
        chkMainOptionsTagline.Click,
        chkMainOptionsTitle.Click,
        chkMainOptionsTop250.Click,
        chkMainOptionsTrailer.Click,
        chkMainOptionsWriters.Click,
        chkSeasonModifierAll.Click,
        chkSeasonModifierBanner.Click,
        chkSeasonModifierFanart.Click,
        chkSeasonModifierLandscape.Click,
        chkSeasonModifierPoster.Click,
        chkSeasonOptionsAired.Click,
        chkSeasonOptionsAll.Click,
        chkSeasonOptionsPlot.Click,
        chkSeasonOptionsTitle.Click,
        chkSpecialModifierAll.Click,
        chkSpecialModifierWithEpisodes.Click,
        chkSpecialModifierWithSeasons.Click

        CheckEnable()
    End Sub

    Private Sub Setup()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkEpisodeModifierActorThumbs.Text = strActorThumbs
        chkMainModifierActorThumbs.Text = strActorThumbs

        'All
        Dim strAll As String = Master.eLang.GetString(68, "All")
        rbScrapeType_All.Text = strAll

        'All Items
        Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
        chkEpisodeModifierAll.Text = strAllItems
        chkEpisodeOptionsAll.Text = strAllItems
        chkMainModifierAll.Text = strAllItems
        chkMainOptionsAll.Text = strAllItems
        chkSeasonModifierAll.Text = strAllItems
        chkSeasonOptionsAll.Text = strAllItems
        chkSpecialModifierAll.Text = strAllItems

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        chkMainModifierBanner.Text = strBanner
        chkSeasonModifierBanner.Text = strBanner

        'Cancel
        Dim strCancel As String = Master.eLang.Cancel
        btnCancel.Text = strCancel

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        chkMainModifierCharacterArt.Text = strCharacterArt

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        chkMainModifierClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        chkMainModifierClearLogo.Text = strClearLogo

        'Creators
        Dim strCreators As String = Master.eLang.GetString(744, "Creators")
        chkMainOptionsCreators.Text = strCreators

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        chkMainModifierDiscArt.Text = strDiscArt

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkMainModifierExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        chkMainModifierExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        chkEpisodeModifierFanart.Text = strFanart
        chkMainModifierFanart.Text = strFanart
        chkSeasonModifierFanart.Text = strFanart

        'KeyArt
        Dim strKeyArt As String = Master.eLang.GetString(296, "KeyArt")
        chkMainModifierKeyArt.Text = strKeyArt

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        chkMainModifierLandscape.Text = strLandscape
        chkSeasonModifierLandscape.Text = strLandscape

        'MetaData
        Dim strMetaData As String = Master.eLang.GetString(59, "Metadata")
        chkEpisodeModifierMetaData.Text = strMetaData
        chkMainModifierMetaData.Text = strMetaData

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        chkEpisodeModifierNFO.Text = strNFO
        chkMainModifierNFO.Text = strNFO

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        chkEpisodeModifierPoster.Text = strPoster
        chkMainModifierPoster.Text = strPoster
        chkSeasonModifierPoster.Text = strPoster

        'Select None
        Dim strSelectNone As String = Master.eLang.GetString(1139, "Select None")
        btnEpisodeScrapeModifierNone.Text = strSelectNone
        btnEpisodeScrapeOptionsNone.Text = strSelectNone
        btnMainScrapeModifierNone.Text = strSelectNone
        btnMainScrapeOptionsNone.Text = strSelectNone
        btnSeasonScrapeModifierNone.Text = strSelectNone
        btnSeasonScrapeOptionsNone.Text = strSelectNone
        btnSpecialScrapeModifierNone.Text = strSelectNone

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")
        chkMainModifierTheme.Text = strTheme

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        chkMainModifierTrailer.Text = strTrailer

        'with Episodes
        Dim strWithEpisodes As String = Master.eLang.GetString(960, "with Episodes")
        chkSpecialModifierWithEpisodes.Text = strWithEpisodes

        'with Seasons
        Dim strWithSeasons As String = Master.eLang.GetString(959, "with Seasons")
        chkSpecialModifierWithSeasons.Text = strWithSeasons


        Text = Master.eLang.GetString(384, "Custom Scraper")
        btnOK.Text = Master.eLang.GetString(389, "Begin")
        chkMainOptionsActors.Text = Master.eLang.GetString(231, "Actors")
        chkMainOptionsCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkMainOptionsCollection.Text = Master.eLang.GetString(424, "Collection")
        chkMainOptionsCountries.Text = Master.eLang.GetString(237, "Countries")
        chkMainOptionsDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkMainOptionsEpisodeGuideURL.Text = Master.eLang.GetString(723, "Episode Guide URL")
        chkMainOptionsGenres.Text = Master.eLang.GetString(725, "Genres")
        chkMainOptionsMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkMainOptionsOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkMainOptionsOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        chkMainOptionsPlot.Text = Master.eLang.GetString(65, "Plot")
        chkMainOptionsRating.Text = Master.eLang.GetString(400, "Rating")
        chkMainOptionsRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkMainOptionsStudios.Text = Master.eLang.GetString(226, "Studios")
        chkMainOptionsTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkMainOptionsTitle.Text = Master.eLang.GetString(21, "Title")
        chkMainOptionsTop250.Text = Master.eLang.GetString(591, "Top 250")
        chkMainOptionsTrailer.Text = Master.eLang.GetString(151, "Trailer")
        chkMainOptionsWriters.Text = Master.eLang.GetString(394, "Writers")
        gbMainScrapeOptions.Text = Master.eLang.GetString(390, "Options")
        gbMainScrapeModifiers.Text = Master.eLang.GetString(388, "Modifiers")
        gbScrapeType_Filter.Text = Master.eLang.GetString(386, "Selection Filter")
        gbScrapeType_Mode.Text = Master.eLang.GetString(387, "Update Mode")
        lblTopDescription.Text = Master.eLang.GetString(385, "Create a custom scraper")
        lblTopTitle.Text = Text
        rbScrapeType_Filter.Text = Master.eLang.GetString(624, "Current Filter")
        rbScrapeType_Marked.Text = Master.eLang.GetString(48, "Marked")
        rbScrapeType_Missing.Text = Master.eLang.GetString(40, "Missing Items")
        rbScrapeType_New.Text = Master.eLang.GetString(47, "New")
        rbScrapeType_Ask.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        rbScrapeType_Auto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        rbScrapeType_Selected.Text = Master.eLang.GetString(1076, "Selected")
        rbScrapeType_Skip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
    End Sub

#End Region 'Methods

End Class