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
Imports NLog

Public Class dlgCustomScraper

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private CustomUpdater As New Structures.CustomUpdaterStruct
    Private _ContentType As Enums.ContentType

    'ScrapeModifier
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
    Private oMainCollectionIDAllowed As Boolean
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
    Private oMainReleaseDateAllowed As Boolean
    Private oMainRuntimeAllowed As Boolean
    Private oMainStatusAllowed As Boolean
    Private oMainStudiosAllowed As Boolean
    Private oMainTaglineAllowed As Boolean
    Private oMainTitleAllowed As Boolean
    Private oMainTop250Allowed As Boolean
    Private oMainTrailerAllowed As Boolean
    Private oMainWritersAllowed As Boolean
    Private oMainYearAllowed As Boolean
    Private oSeasonAiredAllowed As Boolean
    Private oSeasonPlotAllowed As Boolean

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal tContentType As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual

        Me._ContentType = tContentType
    End Sub

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.CustomUpdater.Canceled = False
        Else
            Me.CustomUpdater.Canceled = True
        End If
        Return Me.CustomUpdater
    End Function

    Private Sub dlgUpdateMedia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.SetUp()

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            'set defaults
            SetParameters()

            CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            'Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.All, True)

            Me.CheckEnable()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgUpdateMedia_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
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
        chkMainOptionsCollectionID.Checked = False
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
        chkMainOptionsProducers.Checked = False
        chkMainOptionsRating.Checked = False
        chkMainOptionsReleaseDate.Checked = False
        chkMainOptionsRuntime.Checked = False
        chkMainOptionsStatus.Checked = False
        chkMainOptionsStudios.Checked = False
        chkMainOptionsTagline.Checked = False
        chkMainOptionsTitle.Checked = False
        chkMainOptionsTop250.Checked = False
        chkMainOptionsTrailer.Checked = False
        chkMainOptionsWriters.Checked = False
        chkMainOptionsYear.Checked = False

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
            chkMainModifierActorThumbs.Checked = Me.mMainActorThumbsAllowed
            chkMainModifierActorThumbs.Enabled = False
            chkMainModifierBanner.Checked = Me.mMainBannerAllowed
            chkMainModifierBanner.Enabled = False
            chkMainModifierCharacterArt.Checked = Me.mMainCharacterArtAllowed
            chkMainModifierCharacterArt.Enabled = False
            chkMainModifierClearArt.Checked = Me.mMainClearArtAllowed
            chkMainModifierClearArt.Enabled = False
            chkMainModifierClearLogo.Checked = Me.mMainClearLogoAllowed
            chkMainModifierClearLogo.Enabled = False
            chkMainModifierDiscArt.Checked = Me.mMainDiscArtAllowed
            chkMainModifierDiscArt.Enabled = False
            chkMainModifierExtrafanarts.Checked = Me.mMainExtrafanartsAllowed
            chkMainModifierExtrafanarts.Enabled = False
            chkMainModifierExtrathumbs.Checked = Me.mMainExtrathumbsAllowed
            chkMainModifierExtrathumbs.Enabled = False
            chkMainModifierFanart.Checked = Me.mMainFanartAllowed
            chkMainModifierFanart.Enabled = False
            chkMainModifierLandscape.Checked = Me.mMainLandscapeAllowed
            chkMainModifierLandscape.Enabled = False
            chkMainModifierMetaData.Checked = Me.mMainMetaDataAllowed
            chkMainModifierMetaData.Enabled = False
            chkMainModifierNFO.Checked = Me.mMainNFOAllowed
            chkMainModifierNFO.Enabled = False
            chkMainModifierPoster.Checked = Me.mMainPosterAllowed
            chkMainModifierPoster.Enabled = False
            chkMainModifierTheme.Checked = Me.mMainThemeAllowed
            chkMainModifierTheme.Enabled = False
            chkMainModifierTrailer.Checked = Me.mMainTrailerAllowed
            chkMainModifierTrailer.Enabled = False
        Else
            chkMainModifierActorThumbs.Enabled = Me.mMainActorThumbsAllowed
            chkMainModifierBanner.Enabled = Me.mMainBannerAllowed
            chkMainModifierCharacterArt.Enabled = Me.mMainCharacterArtAllowed
            chkMainModifierClearArt.Enabled = Me.mMainClearArtAllowed
            chkMainModifierClearLogo.Enabled = Me.mMainClearLogoAllowed
            chkMainModifierDiscArt.Enabled = Me.mMainDiscArtAllowed
            chkMainModifierExtrafanarts.Enabled = Me.mMainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Enabled = Me.mMainExtrathumbsAllowed
            chkMainModifierFanart.Enabled = Me.mMainFanartAllowed
            chkMainModifierLandscape.Enabled = Me.mMainLandscapeAllowed
            chkMainModifierMetaData.Enabled = Me.mMainMetaDataAllowed
            chkMainModifierNFO.Enabled = Me.mMainNFOAllowed
            chkMainModifierPoster.Enabled = Me.mMainPosterAllowed
            chkMainModifierTheme.Enabled = Me.mMainThemeAllowed
            chkMainModifierTrailer.Enabled = Me.mMainTrailerAllowed
        End If

        'Main Options
        If chkMainModifierNFO.Checked Then
            gbMainScrapeOptions.Enabled = True
            If chkMainOptionsAll.Checked Then
                chkMainOptionsActors.Checked = Me.oMainActorsAllowed
                chkMainOptionsActors.Enabled = False
                chkMainOptionsCertifications.Checked = Me.oMainCertificationsAllowed
                chkMainOptionsCertifications.Enabled = False
                chkMainOptionsCollectionID.Checked = Me.oMainCollectionIDAllowed
                chkMainOptionsCollectionID.Enabled = False
                chkMainOptionsCountries.Checked = Me.oMainCountriesAllowed
                chkMainOptionsCountries.Enabled = False
                chkMainOptionsCreators.Checked = Me.oMainCreatorsAllowed
                chkMainOptionsCreators.Enabled = False
                chkMainOptionsDirectors.Checked = Me.oMainDirectorsAllowed
                chkMainOptionsDirectors.Enabled = False
                chkMainOptionsEpisodeGuideURL.Checked = Me.oMainEpisodeGuideURLAllowed
                chkMainOptionsEpisodeGuideURL.Enabled = False
                chkMainOptionsGenres.Checked = Me.oMainGenresAllowed
                chkMainOptionsGenres.Enabled = False
                chkMainOptionsMPAA.Checked = Me.oMainMPAAAllowed
                chkMainOptionsMPAA.Enabled = False
                chkMainOptionsOriginalTitle.Checked = Me.oMainOriginalTitleAllowed
                chkMainOptionsOriginalTitle.Enabled = False
                chkMainOptionsOutline.Checked = Me.oMainOutlineAllowed
                chkMainOptionsOutline.Enabled = False
                chkMainOptionsPlot.Checked = Me.oMainPlotAllowed
                chkMainOptionsPlot.Enabled = False
                chkMainOptionsPremiered.Checked = Me.oMainPremieredAllowed
                chkMainOptionsPremiered.Enabled = False
                chkMainOptionsProducers.Checked = Me.oMainProducersAllowed
                chkMainOptionsProducers.Enabled = False
                chkMainOptionsRating.Checked = Me.oMainRatingAllowed
                chkMainOptionsRating.Enabled = False
                chkMainOptionsReleaseDate.Checked = Me.oMainReleaseDateAllowed
                chkMainOptionsReleaseDate.Enabled = False
                chkMainOptionsRuntime.Checked = Me.oMainRuntimeAllowed
                chkMainOptionsRuntime.Enabled = False
                chkMainOptionsStatus.Checked = Me.oMainStatusAllowed
                chkMainOptionsStatus.Enabled = False
                chkMainOptionsStudios.Checked = Me.oMainStudiosAllowed
                chkMainOptionsStudios.Enabled = False
                chkMainOptionsTagline.Checked = Me.oMainTaglineAllowed
                chkMainOptionsTagline.Enabled = False
                chkMainOptionsTitle.Checked = Me.oMainTitleAllowed
                chkMainOptionsTitle.Enabled = False
                chkMainOptionsTop250.Checked = Me.oMainTop250Allowed
                chkMainOptionsTop250.Enabled = False
                chkMainOptionsTrailer.Checked = Me.oMainTrailerAllowed
                chkMainOptionsTrailer.Enabled = False
                chkMainOptionsWriters.Checked = Me.oMainWritersAllowed
                chkMainOptionsWriters.Enabled = False
                chkMainOptionsYear.Checked = Me.oMainYearAllowed
                chkMainOptionsYear.Enabled = False
            Else
                chkMainOptionsActors.Enabled = Me.oMainActorsAllowed
                chkMainOptionsCertifications.Enabled = Me.oMainCertificationsAllowed
                chkMainOptionsCollectionID.Enabled = Me.oMainCollectionIDAllowed
                chkMainOptionsCountries.Enabled = Me.oMainCountriesAllowed
                chkMainOptionsCreators.Enabled = Me.oMainCreatorsAllowed
                chkMainOptionsDirectors.Enabled = Me.oMainDirectorsAllowed
                chkMainOptionsEpisodeGuideURL.Enabled = Me.oMainEpisodeGuideURLAllowed
                chkMainOptionsGenres.Enabled = Me.oMainGenresAllowed
                chkMainOptionsMPAA.Enabled = Me.oMainMPAAAllowed
                chkMainOptionsOriginalTitle.Enabled = Me.oMainOriginalTitleAllowed
                chkMainOptionsOutline.Enabled = Me.oMainOutlineAllowed
                chkMainOptionsPlot.Enabled = Me.oMainPlotAllowed
                chkMainOptionsPremiered.Enabled = Me.oMainPremieredAllowed
                chkMainOptionsProducers.Enabled = Me.oMainProducersAllowed
                chkMainOptionsRating.Enabled = Me.oMainRatingAllowed
                chkMainOptionsReleaseDate.Enabled = Me.oMainReleaseDateAllowed
                chkMainOptionsRuntime.Enabled = Me.oMainRuntimeAllowed
                chkMainOptionsStatus.Enabled = Me.oMainStatusAllowed
                chkMainOptionsStudios.Enabled = Me.oMainStudiosAllowed
                chkMainOptionsTagline.Enabled = Me.oMainTaglineAllowed
                chkMainOptionsTitle.Enabled = Me.oMainTitleAllowed
                chkMainOptionsTop250.Enabled = Me.oMainTop250Allowed
                chkMainOptionsTrailer.Enabled = Me.oMainTrailerAllowed
                chkMainOptionsWriters.Enabled = Me.oMainWritersAllowed
                chkMainOptionsYear.Enabled = Me.oMainYearAllowed
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
            gbEpisodeScrapeModifier.Enabled = True

            'Episode Modifiers
            If chkEpisodeModifierAll.Checked Then
                chkEpisodeModifierActorThumbs.Checked = Me.mEpisodeActorThumbsAllowed
                chkEpisodeModifierActorThumbs.Enabled = False
                chkEpisodeModifierFanart.Checked = Me.mEpisodeFanartAllowed
                chkEpisodeModifierFanart.Enabled = False
                chkEpisodeModifierMetaData.Checked = Me.mEpisodeMetaDataAllowed
                chkEpisodeModifierMetaData.Enabled = False
                chkEpisodeModifierNFO.Checked = Me.mEpisodeNFOAllowed
                chkEpisodeModifierNFO.Enabled = False
                chkEpisodeModifierPoster.Checked = Me.mEpisodePosterAllowed
                chkEpisodeModifierPoster.Enabled = False
            Else
                chkEpisodeModifierActorThumbs.Enabled = Me.mEpisodeActorThumbsAllowed
                chkEpisodeModifierFanart.Enabled = Me.mEpisodeFanartAllowed
                chkEpisodeModifierMetaData.Enabled = Me.mEpisodeMetaDataAllowed
                chkEpisodeModifierNFO.Enabled = Me.mEpisodeNFOAllowed
                chkEpisodeModifierPoster.Enabled = Me.mEpisodePosterAllowed
            End If

            'Episode Options
            If chkEpisodeModifierNFO.Checked Then
                gbEpisodeScrapeOptions.Enabled = True
                If chkEpisodeOptionsAll.Checked Then
                    chkEpisodeOptionsActors.Checked = Me.oEpisodeActorsAllowed
                    chkEpisodeOptionsActors.Enabled = False
                    chkEpisodeOptionsAired.Checked = Me.oEpisodeAiredAllowed
                    chkEpisodeOptionsAired.Enabled = False
                    chkEpisodeOptionsDirectors.Checked = Me.oEpisodeDirectorsAllowed
                    chkEpisodeOptionsDirectors.Enabled = False
                    chkEpisodeOptionsGuestStars.Checked = Me.oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = False
                    chkEpisodeOptionsPlot.Checked = Me.oEpisodePlotAllowed
                    chkEpisodeOptionsPlot.Enabled = False
                    chkEpisodeOptionsRating.Checked = Me.oEpisodeRatingAllowed
                    chkEpisodeOptionsRating.Enabled = False
                    chkEpisodeOptionsRuntime.Checked = Me.oEpisodeRuntimeAllowed
                    chkEpisodeOptionsRuntime.Enabled = False
                    chkEpisodeOptionsTitle.Checked = Me.oEpisodeTitleAllowed
                    chkEpisodeOptionsTitle.Enabled = False
                    chkEpisodeOptionsWriters.Checked = Me.oEpisodeWritersAllowed
                    chkEpisodeOptionsWriters.Enabled = False
                Else
                    chkEpisodeOptionsActors.Enabled = Me.oEpisodeActorsAllowed
                    chkEpisodeOptionsAired.Enabled = Me.oEpisodeAiredAllowed
                    chkEpisodeOptionsDirectors.Enabled = Me.oEpisodeDirectorsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = Me.oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsPlot.Enabled = Me.oEpisodePlotAllowed
                    chkEpisodeOptionsRating.Enabled = Me.oEpisodeRatingAllowed
                    chkEpisodeOptionsRuntime.Enabled = Me.oEpisodeRuntimeAllowed
                    chkEpisodeOptionsTitle.Enabled = Me.oEpisodeTitleAllowed
                    chkEpisodeOptionsWriters.Enabled = Me.oEpisodeWritersAllowed
                End If
            Else
                gbEpisodeScrapeOptions.Enabled = False
            End If
        Else
            gbEpisodeScrapeModifier.Enabled = False
            gbEpisodeScrapeOptions.Enabled = False
        End If

        'with Seasons
        If chkSpecialModifierWithSeasons.Checked Then
            gbSeasonScrapeModifier.Enabled = True

            'Season Modifiers
            If chkSeasonModifierAll.Checked Then
                chkSeasonModifierBanner.Checked = Me.mSeasonBannerAllowed
                chkSeasonModifierBanner.Enabled = False
                chkSeasonModifierFanart.Checked = Me.mSeasonFanartAllowed
                chkSeasonModifierFanart.Enabled = False
                chkSeasonModifierLandscape.Checked = Me.mSeasonLandscapeAllowed
                chkSeasonModifierLandscape.Enabled = False
                chkSeasonModifierPoster.Checked = Me.mSeasonPosterAllowed
                chkSeasonModifierPoster.Enabled = False
            Else
                chkSeasonModifierBanner.Enabled = Me.mSeasonBannerAllowed
                chkSeasonModifierFanart.Enabled = Me.mSeasonFanartAllowed
                chkSeasonModifierLandscape.Enabled = Me.mSeasonLandscapeAllowed
                chkSeasonModifierPoster.Enabled = Me.mSeasonPosterAllowed
            End If

            'Season Options
            If chkMainModifierNFO.Checked Then 'TODO: check. Atm we save the season infos to tv show NFO
                gbSeasonScrapeOptions.Enabled = True
                If chkSeasonOptionsAll.Checked Then
                    chkSeasonOptionsAired.Checked = Me.oSeasonAiredAllowed
                    chkSeasonOptionsAired.Enabled = False
                    chkSeasonOptionsPlot.Checked = Me.oSeasonPlotAllowed
                    chkSeasonOptionsPlot.Enabled = False
                Else
                    chkSeasonOptionsAired.Enabled = Me.oSeasonAiredAllowed
                    chkSeasonOptionsPlot.Enabled = Me.oSeasonPlotAllowed
                End If
            Else
                gbSeasonScrapeOptions.Enabled = False
            End If
        Else
            gbSeasonScrapeModifier.Enabled = False
            gbSeasonScrapeOptions.Enabled = False
        End If

        'Scrape Modifier
        CustomUpdater.ScrapeModifier.EpisodeActorThumbs = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifier.EpisodeFanart = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierFanart.Checked
        CustomUpdater.ScrapeModifier.EpisodeMeta = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierMetaData.Checked
        CustomUpdater.ScrapeModifier.EpisodeNFO = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked
        CustomUpdater.ScrapeModifier.EpisodePoster = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierPoster.Checked
        CustomUpdater.ScrapeModifier.MainActorthumbs = chkMainModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifier.MainBanner = chkMainModifierBanner.Checked
        CustomUpdater.ScrapeModifier.MainCharacterArt = chkMainModifierCharacterArt.Checked
        CustomUpdater.ScrapeModifier.MainClearArt = chkMainModifierClearArt.Checked
        CustomUpdater.ScrapeModifier.MainClearLogo = chkMainModifierClearLogo.Checked
        CustomUpdater.ScrapeModifier.MainDiscArt = chkMainModifierDiscArt.Checked
        CustomUpdater.ScrapeModifier.MainFanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifier.MainExtrathumbs = chkMainModifierExtrathumbs.Checked
        CustomUpdater.ScrapeModifier.MainExtrafanarts = chkMainModifierExtrafanarts.Checked
        CustomUpdater.ScrapeModifier.MainFanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifier.MainLandscape = chkMainModifierLandscape.Checked
        CustomUpdater.ScrapeModifier.MainMeta = chkMainModifierMetaData.Checked
        CustomUpdater.ScrapeModifier.MainNFO = chkMainModifierNFO.Checked
        CustomUpdater.ScrapeModifier.MainPoster = chkMainModifierPoster.Checked
        CustomUpdater.ScrapeModifier.MainTheme = chkMainModifierTheme.Checked
        CustomUpdater.ScrapeModifier.MainTrailer = chkMainModifierTrailer.Checked
        CustomUpdater.ScrapeModifier.SeasonBanner = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierBanner.Checked
        CustomUpdater.ScrapeModifier.SeasonFanart = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierFanart.Checked
        CustomUpdater.ScrapeModifier.SeasonLandscape = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierLandscape.Checked
        CustomUpdater.ScrapeModifier.SeasonPoster = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierPoster.Checked
        CustomUpdater.ScrapeModifier.withEpisodes = chkSpecialModifierWithEpisodes.Checked
        CustomUpdater.ScrapeModifier.withSeasons = chkSpecialModifierWithSeasons.Checked
        CustomUpdater.ScrapeModifier.AllSeasonsBanner = CustomUpdater.ScrapeModifier.SeasonBanner
        CustomUpdater.ScrapeModifier.AllSeasonsFanart = CustomUpdater.ScrapeModifier.SeasonFanart
        CustomUpdater.ScrapeModifier.AllSeasonsLandscape = CustomUpdater.ScrapeModifier.SeasonLandscape
        CustomUpdater.ScrapeModifier.AllSeasonsPoster = CustomUpdater.ScrapeModifier.SeasonPoster

        'Scrape Options
        CustomUpdater.ScrapeOptions.bEpisodeActors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeAired = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsAired.Checked
        CustomUpdater.ScrapeOptions.bEpisodeCredits = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.bEpisodeDirector = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeGuestStars = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsGuestStars.Checked
        CustomUpdater.ScrapeOptions.bEpisodePlot = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRating = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRating.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRuntime = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.bEpisodeTitle = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.bMainActors = chkMainModifierNFO.Checked AndAlso chkMainOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bMainCert = chkMainModifierNFO.Checked AndAlso chkMainOptionsCertifications.Checked
        CustomUpdater.ScrapeOptions.bMainCollectionID = chkMainModifierNFO.Checked AndAlso chkMainOptionsCollectionID.Checked
        CustomUpdater.ScrapeOptions.bMainCountry = chkMainModifierNFO.Checked AndAlso chkMainOptionsCountries.Checked
        CustomUpdater.ScrapeOptions.bMainCreator = chkMainModifierNFO.Checked AndAlso chkMainOptionsCreators.Checked
        CustomUpdater.ScrapeOptions.bMainDirector = chkMainModifierNFO.Checked AndAlso chkMainOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.bMainEpisodeGuide = chkMainModifierNFO.Checked AndAlso chkMainOptionsEpisodeGuideURL.Checked
        CustomUpdater.ScrapeOptions.bMainGenre = chkMainModifierNFO.Checked AndAlso chkMainOptionsGenres.Checked
        CustomUpdater.ScrapeOptions.bMainMPAA = chkMainModifierNFO.Checked AndAlso chkMainOptionsMPAA.Checked
        CustomUpdater.ScrapeOptions.bMainOriginalTitle = chkMainModifierNFO.Checked AndAlso chkMainOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.bMainOutline = chkMainModifierNFO.Checked AndAlso chkMainOptionsOutline.Checked
        CustomUpdater.ScrapeOptions.bMainPlot = chkMainModifierNFO.Checked AndAlso chkMainOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.bMainPremiered = chkMainModifierNFO.Checked AndAlso chkMainOptionsPremiered.Checked
        CustomUpdater.ScrapeOptions.bMainProducers = chkMainModifierNFO.Checked AndAlso chkMainOptionsProducers.Checked
        CustomUpdater.ScrapeOptions.bMainRating = chkMainModifierNFO.Checked AndAlso chkMainOptionsRating.Checked
        CustomUpdater.ScrapeOptions.bMainRelease = chkMainModifierNFO.Checked AndAlso chkMainOptionsReleaseDate.Checked
        CustomUpdater.ScrapeOptions.bMainRuntime = chkMainModifierNFO.Checked AndAlso chkMainOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.bMainStatus = chkMainModifierNFO.Checked AndAlso chkMainOptionsStatus.Checked
        CustomUpdater.ScrapeOptions.bMainStudio = chkMainModifierNFO.Checked AndAlso chkMainOptionsStudios.Checked
        CustomUpdater.ScrapeOptions.bMainTagline = chkMainModifierNFO.Checked AndAlso chkMainOptionsTagline.Checked
        CustomUpdater.ScrapeOptions.bMainTitle = chkMainModifierNFO.Checked AndAlso chkMainOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.bMainTop250 = chkMainModifierNFO.Checked AndAlso chkMainOptionsTop250.Checked
        CustomUpdater.ScrapeOptions.bMainTrailer = chkMainModifierNFO.Checked AndAlso chkMainOptionsTrailer.Checked
        CustomUpdater.ScrapeOptions.bMainWriters = chkMainModifierNFO.Checked AndAlso chkMainOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.bMainYear = chkMainModifierNFO.Checked AndAlso chkMainOptionsYear.Checked
        CustomUpdater.ScrapeOptions.bSeasonAired = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsAired.Checked   'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.bSeasonPlot = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsPlot.Checked     'TODO: check. Atm we save the season infos to tv show NFO

        If CustomUpdater.ScrapeModifier.EpisodeActorThumbs OrElse _
            CustomUpdater.ScrapeModifier.EpisodeFanart OrElse _
            CustomUpdater.ScrapeModifier.EpisodeMeta OrElse _
            CustomUpdater.ScrapeModifier.EpisodePoster OrElse _
            CustomUpdater.ScrapeModifier.EpisodeSubtitles OrElse _
            CustomUpdater.ScrapeModifier.MainActorthumbs OrElse _
            CustomUpdater.ScrapeModifier.MainBanner OrElse _
            CustomUpdater.ScrapeModifier.MainCharacterArt OrElse _
            CustomUpdater.ScrapeModifier.MainClearArt OrElse _
            CustomUpdater.ScrapeModifier.MainClearLogo OrElse _
            CustomUpdater.ScrapeModifier.MainDiscArt OrElse _
            CustomUpdater.ScrapeModifier.MainExtrafanarts OrElse _
            CustomUpdater.ScrapeModifier.MainExtrathumbs OrElse _
            CustomUpdater.ScrapeModifier.MainFanart OrElse _
            CustomUpdater.ScrapeModifier.MainLandscape OrElse _
            CustomUpdater.ScrapeModifier.MainMeta OrElse _
            CustomUpdater.ScrapeModifier.MainPoster OrElse _
            CustomUpdater.ScrapeModifier.MainSubtitles OrElse _
            CustomUpdater.ScrapeModifier.MainTheme OrElse _
            CustomUpdater.ScrapeModifier.MainTrailer OrElse _
            CustomUpdater.ScrapeModifier.SeasonBanner OrElse _
            CustomUpdater.ScrapeModifier.SeasonFanart OrElse _
            CustomUpdater.ScrapeModifier.SeasonLandscape OrElse _
            CustomUpdater.ScrapeModifier.SeasonPoster Then
            Me.btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifier.EpisodeNFO AndAlso ( _
            CustomUpdater.ScrapeOptions.bEpisodeActors OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeAired OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeCredits OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeDirector OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeGuestStars OrElse _
            CustomUpdater.ScrapeOptions.bEpisodePlot OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeRating OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeRuntime OrElse _
            CustomUpdater.ScrapeOptions.bEpisodeTitle) Then
            Me.btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifier.MainNFO AndAlso ( _
            CustomUpdater.ScrapeOptions.bMainActors OrElse _
            CustomUpdater.ScrapeOptions.bMainCert OrElse _
            CustomUpdater.ScrapeOptions.bMainCollectionID OrElse _
            CustomUpdater.ScrapeOptions.bMainCountry OrElse _
            CustomUpdater.ScrapeOptions.bMainCreator OrElse _
            CustomUpdater.ScrapeOptions.bMainDirector OrElse _
            CustomUpdater.ScrapeOptions.bMainEpisodeGuide OrElse _
            CustomUpdater.ScrapeOptions.bMainFullCrew OrElse _
            CustomUpdater.ScrapeOptions.bMainGenre OrElse _
            CustomUpdater.ScrapeOptions.bMainMPAA OrElse _
            CustomUpdater.ScrapeOptions.bMainMusicBy OrElse _
            CustomUpdater.ScrapeOptions.bMainOriginalTitle OrElse _
            CustomUpdater.ScrapeOptions.bMainOtherCrew OrElse _
            CustomUpdater.ScrapeOptions.bMainOutline OrElse _
            CustomUpdater.ScrapeOptions.bMainPlot OrElse _
            CustomUpdater.ScrapeOptions.bMainPremiered OrElse _
            CustomUpdater.ScrapeOptions.bMainProducers OrElse _
            CustomUpdater.ScrapeOptions.bMainRating OrElse _
            CustomUpdater.ScrapeOptions.bMainRelease OrElse _
            CustomUpdater.ScrapeOptions.bMainRuntime OrElse _
            CustomUpdater.ScrapeOptions.bMainStatus OrElse _
            CustomUpdater.ScrapeOptions.bMainStudio OrElse _
            CustomUpdater.ScrapeOptions.bMainTagline OrElse _
            CustomUpdater.ScrapeOptions.bMainTags OrElse _
            CustomUpdater.ScrapeOptions.bMainTitle OrElse _
            CustomUpdater.ScrapeOptions.bMainTop250 OrElse _
            CustomUpdater.ScrapeOptions.bMainTrailer OrElse _
            CustomUpdater.ScrapeOptions.bMainWriters OrElse _
            CustomUpdater.ScrapeOptions.bMainYear OrElse _
            CustomUpdater.ScrapeOptions.bSeasonAired OrElse _
            CustomUpdater.ScrapeOptions.bSeasonPlot) Then
            Me.btnOK.Enabled = True
        Else
            Me.btnOK.Enabled = False
        End If
    End Sub

    Private Sub SetParameters()
        Dim NameID As String = String.Empty
        Dim NameTable As String = String.Empty

        With Master.eSettings
            Select Case _ContentType
                Case Enums.ContentType.Movie
                    NameID = "idMovie"
                    NameTable = "movie"
                    Me.gbEpisodeScrapeModifier.Visible = False
                    Me.gbEpisodeScrapeOptions.Visible = False
                    Me.gbSeasonScrapeModifier.Visible = False
                    Me.gbSeasonScrapeOptions.Visible = False
                    Me.gbSpecialScrapeModifier.Visible = False

                    Me.mEpisodeActorThumbsAllowed = False
                    Me.mEpisodeFanartAllowed = False
                    Me.mEpisodeMetaDataAllowed = False
                    Me.mEpisodeNFOAllowed = False
                    Me.mEpisodePosterAllowed = False
                    Me.mMainActorThumbsAllowed = .MovieActorThumbsAnyEnabled
                    Me.mMainBannerAllowed = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    Me.mMainCharacterArtAllowed = False
                    Me.mMainClearArtAllowed = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    Me.mMainClearLogoAllowed = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    Me.mMainDiscArtAllowed = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    Me.mMainExtrafanartsAllowed = .MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.mMainExtrathumbsAllowed = .MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.mMainFanartAllowed = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.mMainLandscapeAllowed = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    Me.mMainMetaDataAllowed = .MovieScraperMetaDataScan
                    Me.mMainNFOAllowed = .MovieNFOAnyEnabled
                    Me.mMainPosterAllowed = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    Me.mMainThemeAllowed = .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    Me.mMainTrailerAllowed = .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    Me.mSeasonBannerAllowed = False
                    Me.mSeasonFanartAllowed = False
                    Me.mSeasonLandscapeAllowed = False
                    Me.mSeasonPosterAllowed = False

                    Me.oEpisodeActorsAllowed = False
                    Me.oEpisodeAiredAllowed = False
                    Me.oEpisodeDirectorsAllowed = False
                    Me.oEpisodeGuestStarsAllowed = False
                    Me.oEpisodePlotAllowed = False
                    Me.oEpisodeRatingAllowed = False
                    Me.oEpisodeRuntimeAllowed = False
                    Me.oEpisodeTitleAllowed = False
                    Me.oEpisodeWritersAllowed = False
                    Me.oMainActorsAllowed = .MovieScraperCast
                    Me.oMainCertificationsAllowed = .MovieScraperCert
                    Me.oMainCollectionIDAllowed = .MovieScraperCollectionID
                    Me.oMainCountriesAllowed = .MovieScraperCountry
                    Me.oMainCreatorsAllowed = False
                    Me.oMainDirectorsAllowed = .MovieScraperDirector
                    Me.oMainEpisodeGuideURLAllowed = False
                    Me.oMainGenresAllowed = .MovieScraperGenre
                    Me.oMainMPAAAllowed = .MovieScraperMPAA
                    Me.oMainOriginalTitleAllowed = .MovieScraperOriginalTitle
                    Me.oMainOutlineAllowed = .MovieScraperOutline
                    Me.oMainPlotAllowed = .MovieScraperPlot
                    Me.oMainPremieredAllowed = False
                    Me.oMainProducersAllowed = False
                    Me.oMainRatingAllowed = .MovieScraperRating
                    Me.oMainReleaseDateAllowed = .MovieScraperRelease
                    Me.oMainRuntimeAllowed = .MovieScraperRuntime
                    Me.oMainStatusAllowed = False
                    Me.oMainStudiosAllowed = .MovieScraperStudio
                    Me.oMainTaglineAllowed = .MovieScraperTagline
                    Me.oMainTitleAllowed = .MovieScraperTitle
                    Me.oMainTop250Allowed = .MovieScraperTop250
                    Me.oMainTrailerAllowed = .MovieScraperTrailer
                    Me.oMainWritersAllowed = .MovieScraperCredits
                    Me.oMainYearAllowed = .MovieScraperYear
                    Me.oSeasonAiredAllowed = False
                    Me.oSeasonPlotAllowed = False

                    Me.chkMainModifierAll.Checked = True
                    Me.chkMainOptionsAll.Checked = True

                Case Enums.ContentType.MovieSet
                    NameID = "idSet"
                    NameTable = "sets"
                    Me.gbEpisodeScrapeModifier.Visible = False
                    Me.gbEpisodeScrapeOptions.Visible = False
                    Me.gbSeasonScrapeModifier.Visible = False
                    Me.gbSeasonScrapeOptions.Visible = False
                    Me.gbSpecialScrapeModifier.Visible = False

                    Me.mEpisodeActorThumbsAllowed = False
                    Me.mEpisodeFanartAllowed = False
                    Me.mEpisodeMetaDataAllowed = False
                    Me.mEpisodeNFOAllowed = False
                    Me.mEpisodePosterAllowed = False
                    Me.mMainActorThumbsAllowed = False
                    Me.mMainBannerAllowed = .MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    Me.mMainCharacterArtAllowed = False
                    Me.mMainClearArtAllowed = .MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    Me.mMainClearLogoAllowed = .MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    Me.mMainDiscArtAllowed = .MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    Me.mMainExtrafanartsAllowed = False
                    Me.mMainExtrathumbsAllowed = False
                    Me.mMainFanartAllowed = .MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    Me.mMainLandscapeAllowed = .MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    Me.mMainMetaDataAllowed = False
                    Me.mMainNFOAllowed = .MovieSetNFOAnyEnabled
                    Me.mMainPosterAllowed = .MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    Me.mMainThemeAllowed = False
                    Me.mMainTrailerAllowed = False
                    Me.mSeasonBannerAllowed = False
                    Me.mSeasonFanartAllowed = False
                    Me.mSeasonLandscapeAllowed = False
                    Me.mSeasonPosterAllowed = False

                    Me.oEpisodeActorsAllowed = False
                    Me.oEpisodeAiredAllowed = False
                    Me.oEpisodeDirectorsAllowed = False
                    Me.oEpisodeGuestStarsAllowed = False
                    Me.oEpisodePlotAllowed = False
                    Me.oEpisodeRatingAllowed = False
                    Me.oEpisodeRuntimeAllowed = False
                    Me.oEpisodeTitleAllowed = False
                    Me.oEpisodeWritersAllowed = False
                    Me.oMainActorsAllowed = False
                    Me.oMainCertificationsAllowed = False
                    Me.oMainCollectionIDAllowed = False
                    Me.oMainCountriesAllowed = False
                    Me.oMainCreatorsAllowed = False
                    Me.oMainDirectorsAllowed = False
                    Me.oMainEpisodeGuideURLAllowed = False
                    Me.oMainGenresAllowed = False
                    Me.oMainMPAAAllowed = False
                    Me.oMainOriginalTitleAllowed = False
                    Me.oMainOutlineAllowed = False
                    Me.oMainPlotAllowed = .MovieSetScraperPlot
                    Me.oMainPremieredAllowed = False
                    Me.oMainProducersAllowed = False
                    Me.oMainRatingAllowed = False
                    Me.oMainReleaseDateAllowed = False
                    Me.oMainRuntimeAllowed = False
                    Me.oMainStatusAllowed = False
                    Me.oMainStudiosAllowed = False
                    Me.oMainTaglineAllowed = False
                    Me.oMainTitleAllowed = .MovieSetScraperTitle
                    Me.oMainTop250Allowed = False
                    Me.oMainTrailerAllowed = False
                    Me.oMainWritersAllowed = False
                    Me.oMainYearAllowed = False
                    Me.oSeasonAiredAllowed = False
                    Me.oSeasonPlotAllowed = False

                    Me.chkMainModifierAll.Checked = True
                    Me.chkMainOptionsAll.Checked = True

                Case Enums.ContentType.TV
                    NameID = "idShow"
                    NameTable = "tvshow"

                    Me.mEpisodeActorThumbsAllowed = .TVEpisodeActorThumbsAnyEnabled
                    Me.mEpisodeFanartAllowed = .TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    Me.mEpisodeMetaDataAllowed = .TVScraperMetaDataScan
                    Me.mEpisodeNFOAllowed = .TVEpisodeNFOAnyEnabled
                    Me.mEpisodePosterAllowed = .TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    Me.mMainActorThumbsAllowed = .TVShowActorThumbsAnyEnabled
                    Me.mMainBannerAllowed = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    Me.mMainCharacterArtAllowed = .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    Me.mMainClearArtAllowed = .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    Me.mMainClearLogoAllowed = .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    Me.mMainDiscArtAllowed = False
                    Me.mMainExtrafanartsAllowed = .TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    Me.mMainExtrathumbsAllowed = False
                    Me.mMainFanartAllowed = .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    Me.mMainLandscapeAllowed = .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    Me.mMainMetaDataAllowed = False
                    Me.mMainNFOAllowed = .TVShowNFOAnyEnabled
                    Me.mMainPosterAllowed = .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    Me.mMainThemeAllowed = .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    Me.mMainTrailerAllowed = False
                    Me.mSeasonBannerAllowed = .TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    Me.mSeasonFanartAllowed = .TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    Me.mSeasonLandscapeAllowed = .TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    Me.mSeasonPosterAllowed = .TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

                    Me.oEpisodeActorsAllowed = .TVScraperEpisodeActors
                    Me.oEpisodeAiredAllowed = .TVScraperEpisodeAired
                    Me.oEpisodeDirectorsAllowed = .TVScraperEpisodeDirector
                    Me.oEpisodeGuestStarsAllowed = .TVScraperEpisodeGuestStars
                    Me.oEpisodePlotAllowed = .TVScraperEpisodePlot
                    Me.oEpisodeRatingAllowed = .TVScraperEpisodeRating
                    Me.oEpisodeRuntimeAllowed = .TVScraperEpisodeRuntime
                    Me.oEpisodeTitleAllowed = .TVScraperEpisodeTitle
                    Me.oEpisodeWritersAllowed = .TVScraperEpisodeCredits
                    Me.oMainActorsAllowed = .TVScraperShowActors
                    Me.oMainCertificationsAllowed = .TVScraperShowCert
                    Me.oMainCollectionIDAllowed = False
                    Me.oMainCountriesAllowed = .TVScraperShowCountry
                    Me.oMainCreatorsAllowed = True  'TODO: add creators
                    Me.oMainDirectorsAllowed = False
                    Me.oMainEpisodeGuideURLAllowed = .TVScraperShowEpiGuideURL
                    Me.oMainGenresAllowed = .TVScraperShowGenre
                    Me.oMainMPAAAllowed = .TVScraperShowMPAA
                    Me.oMainOriginalTitleAllowed = .TVScraperShowOriginalTitle
                    Me.oMainOutlineAllowed = False
                    Me.oMainPlotAllowed = .TVScraperShowPlot
                    Me.oMainPremieredAllowed = .TVScraperShowPremiered
                    Me.oMainProducersAllowed = False
                    Me.oMainRatingAllowed = .TVScraperShowRating
                    Me.oMainReleaseDateAllowed = False
                    Me.oMainRuntimeAllowed = .TVScraperShowRuntime
                    Me.oMainStatusAllowed = .TVScraperShowStatus
                    Me.oMainStudiosAllowed = .TVScraperShowStudio
                    Me.oMainTaglineAllowed = False
                    Me.oMainTitleAllowed = .TVScraperShowTitle
                    Me.oMainTop250Allowed = False
                    Me.oMainTrailerAllowed = False
                    Me.oMainWritersAllowed = False
                    Me.oMainYearAllowed = False
                    Me.oSeasonAiredAllowed = True 'TODO: add aired
                    Me.oSeasonPlotAllowed = True 'TODO: add plot

                    Me.chkMainModifierAll.Checked = True
                    Me.chkMainOptionsAll.Checked = True
                    Me.chkSpecialModifierAll.Checked = True
                    Me.chkEpisodeModifierAll.Checked = True
                    Me.chkEpisodeOptionsAll.Checked = True
                    Me.chkSeasonModifierAll.Checked = True
                    Me.chkSeasonOptionsAll.Checked = True
            End Select
        End With

        'check if we have "New" or "Marked" medias
        If Not String.IsNullOrEmpty(NameID) AndAlso Not String.IsNullOrEmpty(NameTable) Then
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS ncount FROM {1} WHERE new = 1;", NameID, NameTable)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
                End Using

                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS mcount FROM {1} WHERE mark = 1;", NameID, NameTable)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_Marked.Enabled = Convert.ToInt32(SQLcount("mcount")) > 0
                End Using
            End Using
        End If
    End Sub

    Private Sub rbUpdateModifier_All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_All.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_Ask.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
            Case Me.rbScrapeType_Auto.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Case Me.rbScrapeType_Skip.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_Marked.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_Ask.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
            Case Me.rbScrapeType_Auto.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
            Case Me.rbScrapeType_Skip.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_Missing.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_Ask.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
            Case Me.rbScrapeType_Auto.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
            Case Me.rbScrapeType_Skip.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_New_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_New.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_Ask.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
            Case Me.rbScrapeType_Auto.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
            Case Me.rbScrapeType_Skip.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
        End Select
    End Sub

    Private Sub rbUpdate_Ask_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_Ask.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
            Case rbScrapeType_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
            Case Me.rbScrapeType_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
            Case Me.rbScrapeType_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_Auto.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Case Me.rbScrapeType_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
            Case Me.rbScrapeType_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
            Case Me.rbScrapeType_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbScrapeType_Skip.CheckedChanged
        Select Case True
            Case Me.rbScrapeType_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
            Case Me.rbScrapeType_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
            Case Me.rbScrapeType_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
            Case Me.rbScrapeType_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
        End Select
    End Sub

    Private Sub CheckBoxes_Click(sender As Object, e As EventArgs) Handles _
        chkEpisodeModifierActorThumbs.Click, _
        chkEpisodeModifierAll.Click, _
        chkEpisodeModifierFanart.Click, _
        chkEpisodeModifierMetaData.Click, _
        chkEpisodeModifierNFO.Click, _
        chkEpisodeModifierPoster.Click, _
        chkEpisodeOptionsActors.Click, _
        chkEpisodeOptionsAired.Click, _
        chkEpisodeOptionsAll.Click, _
        chkEpisodeOptionsDirectors.Click, _
        chkEpisodeOptionsGuestStars.Click, _
        chkEpisodeOptionsPlot.Click, _
        chkEpisodeOptionsRating.Click, _
        chkEpisodeOptionsRuntime.Click, _
        chkEpisodeOptionsTitle.Click, _
        chkEpisodeOptionsWriters.Click, _
        chkMainModifierActorThumbs.Click, _
        chkMainModifierAll.Click, _
        chkMainModifierBanner.Click, _
        chkMainModifierCharacterArt.Click, _
        chkMainModifierClearArt.Click, _
        chkMainModifierClearLogo.Click, _
        chkMainModifierDiscArt.Click, _
        chkMainModifierExtrafanarts.Click, _
        chkMainModifierExtrathumbs.Click, _
        chkMainModifierFanart.Click, _
        chkMainModifierLandscape.Click, _
        chkMainModifierMetaData.Click, _
        chkMainModifierNFO.Click, _
        chkMainModifierPoster.Click, _
        chkMainModifierTheme.Click, _
        chkMainModifierTrailer.Click, _
        chkMainOptionsActors.Click, _
        chkMainOptionsAll.Click, _
        chkMainOptionsCertifications.Click, _
        chkMainOptionsCollectionID.Click, _
        chkMainOptionsCountries.Click, _
        chkMainOptionsCreators.Click, _
        chkMainOptionsDirectors.Click, _
        chkMainOptionsEpisodeGuideURL.Click, _
        chkMainOptionsGenres.Click, _
        chkMainOptionsMPAA.Click, _
        chkMainOptionsOriginalTitle.Click, _
        chkMainOptionsOutline.Click, _
        chkMainOptionsPlot.Click, _
        chkMainOptionsPremiered.Click, _
        chkMainOptionsProducers.Click, _
        chkMainOptionsRating.Click, _
        chkMainOptionsReleaseDate.Click, _
        chkMainOptionsRuntime.Click, _
        chkMainOptionsStatus.Click, _
        chkMainOptionsStudios.Click, _
        chkMainOptionsTagline.Click, _
        chkMainOptionsTitle.Click, _
        chkMainOptionsTop250.Click, _
        chkMainOptionsTrailer.Click, _
        chkMainOptionsWriters.Click, _
        chkMainOptionsYear.Click, _
        chkSeasonModifierAll.Click, _
        chkSeasonModifierBanner.Click, _
        chkSeasonModifierFanart.Click, _
        chkSeasonModifierLandscape.Click, _
        chkSeasonModifierPoster.Click, _
        chkSeasonOptionsAired.Click, _
        chkSeasonOptionsAll.Click, _
        chkSeasonOptionsPlot.Click, _
        chkSpecialModifierAll.Click, _
        chkSpecialModifierWithEpisodes.Click, _
        chkSpecialModifierWithSeasons.Click

        CheckEnable()
    End Sub

    Private Sub SetUp()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkEpisodeModifierActorThumbs.Text = strActorThumbs
        Me.chkMainModifierActorThumbs.Text = strActorThumbs

        'All
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Me.rbScrapeType_All.Text = strAll

        'All Items
        Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
        Me.chkEpisodeModifierAll.Text = strAllItems
        Me.chkEpisodeOptionsAll.Text = strAllItems
        Me.chkMainModifierAll.Text = strAllItems
        Me.chkMainOptionsAll.Text = strAllItems
        Me.chkSeasonModifierAll.Text = strAllItems
        Me.chkSeasonOptionsAll.Text = strAllItems
        Me.chkSpecialModifierAll.Text = strAllItems

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        Me.chkMainModifierBanner.Text = strBanner
        Me.chkSeasonModifierBanner.Text = strBanner

        'Cancel
        Dim strCancel As String = Master.eLang.GetString(167, "Cancel")
        Me.btnCancel.Text = strCancel

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        Me.chkMainModifierCharacterArt.Text = strCharacterArt

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        Me.chkMainModifierClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        Me.chkMainModifierClearLogo.Text = strClearLogo

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        Me.chkMainModifierDiscArt.Text = strDiscArt

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkMainModifierExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkMainModifierExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        Me.chkEpisodeModifierFanart.Text = strFanart
        Me.chkMainModifierFanart.Text = strFanart
        Me.chkSeasonModifierFanart.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        Me.chkMainModifierLandscape.Text = strLandscape
        Me.chkSeasonModifierLandscape.Text = strLandscape

        'MetaData
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
        Me.chkEpisodeModifierMetaData.Text = strMetaData
        Me.chkMainModifierMetaData.Text = strMetaData

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        Me.chkEpisodeModifierNFO.Text = strNFO
        Me.chkMainModifierNFO.Text = strNFO

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        Me.chkEpisodeModifierPoster.Text = strPoster
        Me.chkMainModifierPoster.Text = strPoster
        Me.chkSeasonModifierPoster.Text = strPoster

        'Select None
        Dim strSelectNone As String = Master.eLang.GetString(1139, "Select None")
        Me.btnEpisodeScrapeModifierNone.Text = strSelectNone
        Me.btnEpisodeScrapeOptionsNone.Text = strSelectNone
        Me.btnMainScrapeModifierNone.Text = strSelectNone
        Me.btnMainScrapeOptionsNone.Text = strSelectNone
        Me.btnSeasonScrapeModifierNone.Text = strSelectNone
        Me.btnSeasonScrapeOptionsNone.Text = strSelectNone
        Me.btnSpecialScrapeModifierNone.Text = strSelectNone

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")
        Me.chkMainModifierTheme.Text = strTheme

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        Me.chkMainModifierTrailer.Text = strTrailer

        'with Episodes
        Dim strWithEpisodes As String = Master.eLang.GetString(960, "with Episodes")
        Me.chkSpecialModifierWithEpisodes.Text = strWithEpisodes

        'with Seasons
        Dim strWithSeasons As String = Master.eLang.GetString(959, "with Seasons")
        Me.chkSpecialModifierWithSeasons.Text = strWithSeasons


        Me.Text = Master.eLang.GetString(384, "Custom Scraper")
        Me.btnOK.Text = Master.eLang.GetString(389, "Begin")
        Me.chkMainOptionsActors.Text = Master.eLang.GetString(63, "Cast")
        Me.chkMainOptionsCertifications.Text = Master.eLang.GetString(56, "Certification")
        Me.chkMainOptionsCollectionID.Text = Master.eLang.GetString(1135, "Collection ID")
        Me.chkMainOptionsCountries.Text = Master.eLang.GetString(301, "Country")
        Me.chkMainOptionsDirectors.Text = Master.eLang.GetString(62, "Director")
        Me.chkMainOptionsGenres.Text = Master.eLang.GetString(20, "Genre")
        Me.chkMainOptionsMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkMainOptionsOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        Me.chkMainOptionsOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkMainOptionsPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkMainOptionsProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkMainOptionsRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkMainOptionsReleaseDate.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkMainOptionsRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkMainOptionsStudios.Text = Master.eLang.GetString(395, "Studio")
        Me.chkMainOptionsTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkMainOptionsTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkMainOptionsTop250.Text = Master.eLang.GetString(591, "Top 250")
        Me.chkMainOptionsTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkMainOptionsWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkMainOptionsYear.Text = Master.eLang.GetString(278, "Year")
        Me.gbMainScrapeOptions.Text = Master.eLang.GetString(390, "Options")
        Me.gbMainScrapeModifier.Text = Master.eLang.GetString(388, "Modifiers")
        Me.gbScrapeType_Filter.Text = Master.eLang.GetString(386, "Selection Filter")
        Me.gbScrapeType_Mode.Text = Master.eLang.GetString(387, "Update Mode")
        Me.lblTopDescription.Text = Master.eLang.GetString(385, "Create a custom scraper")
        Me.lblTopTitle.Text = Me.Text
        Me.rbScrapeType_Marked.Text = Master.eLang.GetString(48, "Marked")
        Me.rbScrapeType_Missing.Text = Master.eLang.GetString(40, "Missing Items")
        Me.rbScrapeType_New.Text = Master.eLang.GetString(47, "New")
        Me.rbScrapeType_Ask.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Me.rbScrapeType_Auto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Me.rbScrapeType_Skip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
    End Sub

#End Region 'Methods

End Class