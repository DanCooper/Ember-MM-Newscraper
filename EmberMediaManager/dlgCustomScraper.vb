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
    Private oSeasonTitleAllowed As Boolean

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal tContentType As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual

        _ContentType = tContentType
    End Sub

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
        If MyBase.ShowDialog() = DialogResult.OK Then
            CustomUpdater.Canceled = False
        Else
            CustomUpdater.Canceled = True
        End If
        Return CustomUpdater
    End Function

    Private Sub dlgUpdateMedia_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            SetUp()

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            'set defaults
            SetParameters()

            CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            'Functions.SetScrapeModifiers(CustomUpdater.ScrapeModifiers, Enums.ModifierType.All, True)

            CheckEnable()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub dlgUpdateMedia_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

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
                chkMainOptionsCollectionID.Checked = oMainCollectionIDAllowed
                chkMainOptionsCollectionID.Enabled = False
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
                chkMainOptionsReleaseDate.Checked = oMainReleaseDateAllowed
                chkMainOptionsReleaseDate.Enabled = False
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
                chkMainOptionsYear.Checked = oMainYearAllowed
                chkMainOptionsYear.Enabled = False
            Else
                chkMainOptionsActors.Enabled = oMainActorsAllowed
                chkMainOptionsCertifications.Enabled = oMainCertificationsAllowed
                chkMainOptionsCollectionID.Enabled = oMainCollectionIDAllowed
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
                chkMainOptionsReleaseDate.Enabled = oMainReleaseDateAllowed
                chkMainOptionsRuntime.Enabled = oMainRuntimeAllowed
                chkMainOptionsStatus.Enabled = oMainStatusAllowed
                chkMainOptionsStudios.Enabled = oMainStudiosAllowed
                chkMainOptionsTagline.Enabled = oMainTaglineAllowed
                chkMainOptionsTitle.Enabled = oMainTitleAllowed
                chkMainOptionsTop250.Enabled = oMainTop250Allowed
                chkMainOptionsTrailer.Enabled = oMainTrailerAllowed
                chkMainOptionsWriters.Enabled = oMainWritersAllowed
                chkMainOptionsYear.Enabled = oMainYearAllowed
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
        CustomUpdater.ScrapeOptions.bEpisodeActors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeAired = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsAired.Checked
        CustomUpdater.ScrapeOptions.bEpisodeCredits = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.bEpisodeDirectors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeGuestStars = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsGuestStars.Checked
        CustomUpdater.ScrapeOptions.bEpisodePlot = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRating = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRating.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRuntime = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.bEpisodeTitle = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNFO.Checked AndAlso chkEpisodeOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.bMainActors = chkMainModifierNFO.Checked AndAlso chkMainOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bMainCertifications = chkMainModifierNFO.Checked AndAlso chkMainOptionsCertifications.Checked
        CustomUpdater.ScrapeOptions.bMainCollectionID = chkMainModifierNFO.Checked AndAlso chkMainOptionsCollectionID.Checked
        CustomUpdater.ScrapeOptions.bMainCountries = chkMainModifierNFO.Checked AndAlso chkMainOptionsCountries.Checked
        CustomUpdater.ScrapeOptions.bMainCreators = chkMainModifierNFO.Checked AndAlso chkMainOptionsCreators.Checked
        CustomUpdater.ScrapeOptions.bMainDirectors = chkMainModifierNFO.Checked AndAlso chkMainOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.bMainEpisodeGuide = chkMainModifierNFO.Checked AndAlso chkMainOptionsEpisodeGuideURL.Checked
        CustomUpdater.ScrapeOptions.bMainGenres = chkMainModifierNFO.Checked AndAlso chkMainOptionsGenres.Checked
        CustomUpdater.ScrapeOptions.bMainMPAA = chkMainModifierNFO.Checked AndAlso chkMainOptionsMPAA.Checked
        CustomUpdater.ScrapeOptions.bMainOriginalTitle = chkMainModifierNFO.Checked AndAlso chkMainOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.bMainOutline = chkMainModifierNFO.Checked AndAlso chkMainOptionsOutline.Checked
        CustomUpdater.ScrapeOptions.bMainPlot = chkMainModifierNFO.Checked AndAlso chkMainOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.bMainPremiered = chkMainModifierNFO.Checked AndAlso chkMainOptionsPremiered.Checked
        CustomUpdater.ScrapeOptions.bMainRating = chkMainModifierNFO.Checked AndAlso chkMainOptionsRating.Checked
        CustomUpdater.ScrapeOptions.bMainRelease = chkMainModifierNFO.Checked AndAlso chkMainOptionsReleaseDate.Checked
        CustomUpdater.ScrapeOptions.bMainRuntime = chkMainModifierNFO.Checked AndAlso chkMainOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.bMainStatus = chkMainModifierNFO.Checked AndAlso chkMainOptionsStatus.Checked
        CustomUpdater.ScrapeOptions.bMainStudios = chkMainModifierNFO.Checked AndAlso chkMainOptionsStudios.Checked
        CustomUpdater.ScrapeOptions.bMainTagline = chkMainModifierNFO.Checked AndAlso chkMainOptionsTagline.Checked
        CustomUpdater.ScrapeOptions.bMainTitle = chkMainModifierNFO.Checked AndAlso chkMainOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.bMainTop250 = chkMainModifierNFO.Checked AndAlso chkMainOptionsTop250.Checked
        CustomUpdater.ScrapeOptions.bMainTrailer = chkMainModifierNFO.Checked AndAlso chkMainOptionsTrailer.Checked
        CustomUpdater.ScrapeOptions.bMainWriters = chkMainModifierNFO.Checked AndAlso chkMainOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.bMainYear = chkMainModifierNFO.Checked AndAlso chkMainOptionsYear.Checked
        CustomUpdater.ScrapeOptions.bSeasonAired = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsAired.Checked   'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.bSeasonPlot = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsPlot.Checked     'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.bSeasonTitle = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNFO.Checked AndAlso chkSeasonOptionsTitle.Checked     'TODO: check. Atm we save the season infos to tv show NFO

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
            CustomUpdater.ScrapeOptions.bEpisodeActors OrElse
            CustomUpdater.ScrapeOptions.bEpisodeAired OrElse
            CustomUpdater.ScrapeOptions.bEpisodeCredits OrElse
            CustomUpdater.ScrapeOptions.bEpisodeDirectors OrElse
            CustomUpdater.ScrapeOptions.bEpisodeGuestStars OrElse
            CustomUpdater.ScrapeOptions.bEpisodePlot OrElse
            CustomUpdater.ScrapeOptions.bEpisodeRating OrElse
            CustomUpdater.ScrapeOptions.bEpisodeRuntime OrElse
            CustomUpdater.ScrapeOptions.bEpisodeTitle) Then
            btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifiers.MainNFO AndAlso (
            CustomUpdater.ScrapeOptions.bMainActors OrElse
            CustomUpdater.ScrapeOptions.bMainCertifications OrElse
            CustomUpdater.ScrapeOptions.bMainCollectionID OrElse
            CustomUpdater.ScrapeOptions.bMainCountries OrElse
            CustomUpdater.ScrapeOptions.bMainCreators OrElse
            CustomUpdater.ScrapeOptions.bMainDirectors OrElse
            CustomUpdater.ScrapeOptions.bMainEpisodeGuide OrElse
            CustomUpdater.ScrapeOptions.bMainGenres OrElse
            CustomUpdater.ScrapeOptions.bMainMPAA OrElse
            CustomUpdater.ScrapeOptions.bMainOriginalTitle OrElse
            CustomUpdater.ScrapeOptions.bMainOutline OrElse
            CustomUpdater.ScrapeOptions.bMainPlot OrElse
            CustomUpdater.ScrapeOptions.bMainPremiered OrElse
            CustomUpdater.ScrapeOptions.bMainRating OrElse
            CustomUpdater.ScrapeOptions.bMainRelease OrElse
            CustomUpdater.ScrapeOptions.bMainRuntime OrElse
            CustomUpdater.ScrapeOptions.bMainStatus OrElse
            CustomUpdater.ScrapeOptions.bMainStudios OrElse
            CustomUpdater.ScrapeOptions.bMainTagline OrElse
            CustomUpdater.ScrapeOptions.bMainTags OrElse
            CustomUpdater.ScrapeOptions.bMainTitle OrElse
            CustomUpdater.ScrapeOptions.bMainTop250 OrElse
            CustomUpdater.ScrapeOptions.bMainTrailer OrElse
            CustomUpdater.ScrapeOptions.bMainWriters OrElse
            CustomUpdater.ScrapeOptions.bMainYear OrElse
            CustomUpdater.ScrapeOptions.bSeasonAired OrElse
            CustomUpdater.ScrapeOptions.bSeasonPlot) Then
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
                    NameID = "idMovie"
                    NameTable = "movie"
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
                    mMainBannerAllowed = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = .MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = .MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainFanartAllowed = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainLandscapeAllowed = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = .MovieScraperMetaDataScan
                    mMainNFOAllowed = .MovieNFOAnyEnabled
                    mMainPosterAllowed = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    mMainTrailerAllowed = .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
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
                    oMainActorsAllowed = .MovieScraperCast
                    oMainCertificationsAllowed = .MovieScraperCert
                    oMainCollectionIDAllowed = .MovieScraperCollectionID
                    oMainCountriesAllowed = .MovieScraperCountry
                    oMainCreatorsAllowed = False
                    oMainDirectorsAllowed = .MovieScraperDirector
                    oMainEpisodeGuideURLAllowed = False
                    oMainGenresAllowed = .MovieScraperGenre
                    oMainMPAAAllowed = .MovieScraperMPAA
                    oMainOriginalTitleAllowed = .MovieScraperOriginalTitle
                    oMainOutlineAllowed = .MovieScraperOutline
                    oMainPlotAllowed = .MovieScraperPlot
                    oMainPremieredAllowed = False
                    oMainProducersAllowed = False
                    oMainRatingAllowed = .MovieScraperRating
                    oMainReleaseDateAllowed = .MovieScraperRelease
                    oMainRuntimeAllowed = .MovieScraperRuntime
                    oMainStatusAllowed = False
                    oMainStudiosAllowed = .MovieScraperStudio
                    oMainTaglineAllowed = .MovieScraperTagline
                    oMainTitleAllowed = .MovieScraperTitle
                    oMainTop250Allowed = .MovieScraperTop250
                    oMainTrailerAllowed = .MovieScraperTrailer
                    oMainWritersAllowed = .MovieScraperCredits
                    oMainYearAllowed = .MovieScraperYear
                    oSeasonAiredAllowed = False
                    oSeasonPlotAllowed = False
                    oSeasonTitleAllowed = False

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows.Count)
                    rbScrapeType_Selected.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count)

                Case Enums.ContentType.MovieSet
                    NameID = "idSet"
                    NameTable = "sets"
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
                    mMainBannerAllowed = .MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = False
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    mMainLandscapeAllowed = .MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .MovieSetNFOAnyEnabled
                    mMainPosterAllowed = .MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
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
                    oMainCollectionIDAllowed = False
                    oMainCountriesAllowed = False
                    oMainCreatorsAllowed = False
                    oMainDirectorsAllowed = False
                    oMainEpisodeGuideURLAllowed = False
                    oMainGenresAllowed = False
                    oMainMPAAAllowed = False
                    oMainOriginalTitleAllowed = False
                    oMainOutlineAllowed = False
                    oMainPlotAllowed = .MovieSetScraperPlot
                    oMainPremieredAllowed = False
                    oMainProducersAllowed = False
                    oMainRatingAllowed = False
                    oMainReleaseDateAllowed = False
                    oMainRuntimeAllowed = False
                    oMainStatusAllowed = False
                    oMainStudiosAllowed = False
                    oMainTaglineAllowed = False
                    oMainTitleAllowed = .MovieSetScraperTitle
                    oMainTop250Allowed = False
                    oMainTrailerAllowed = False
                    oMainWritersAllowed = False
                    oMainYearAllowed = False
                    oSeasonAiredAllowed = False
                    oSeasonPlotAllowed = False
                    oSeasonTitleAllowed = False

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count)
                    rbScrapeType_Selected.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count)

                Case Enums.ContentType.TV
                    NameID = "idShow"
                    NameTable = "tvshow"

                    mEpisodeActorThumbsAllowed = .TVEpisodeActorThumbsAnyEnabled
                    mEpisodeFanartAllowed = .TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mEpisodeMetaDataAllowed = .TVScraperMetaDataScan
                    mEpisodeNFOAllowed = .TVEpisodeNFOAnyEnabled
                    mEpisodePosterAllowed = .TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    mMainActorThumbsAllowed = .TVShowActorThumbsAnyEnabled
                    mMainBannerAllowed = .TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mMainClearArtAllowed = .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = False
                    mMainExtrafanartsAllowed = .TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainLandscapeAllowed = .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .TVShowNFOAnyEnabled
                    mMainPosterAllowed = .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                    mMainTrailerAllowed = False
                    mSeasonBannerAllowed = .TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    mSeasonFanartAllowed = .TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mSeasonLandscapeAllowed = .TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mSeasonPosterAllowed = .TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

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
                    oMainCollectionIDAllowed = False
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
                    oMainReleaseDateAllowed = False
                    oMainRuntimeAllowed = .TVScraperShowRuntime
                    oMainStatusAllowed = .TVScraperShowStatus
                    oMainStudiosAllowed = .TVScraperShowStudio
                    oMainTaglineAllowed = False
                    oMainTitleAllowed = .TVScraperShowTitle
                    oMainTop250Allowed = False
                    oMainTrailerAllowed = False
                    oMainWritersAllowed = False
                    oMainYearAllowed = False
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

                    rbScrapeType_Filter.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListTVShows.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListTVShows.Rows.Count)
                    rbScrapeType_Selected.Enabled = ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count)
            End Select
        End With

        'check if we have "New" or "Marked" medias
        If Not String.IsNullOrEmpty(NameID) AndAlso Not String.IsNullOrEmpty(NameTable) Then
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS ncount FROM {1} WHERE new = 1;", NameID, NameTable)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
                    rbScrapeType_New.Text = String.Format(String.Concat(Master.eLang.GetString(47, "New"), " ({0})"), Convert.ToInt32(SQLcount("ncount")))
                End Using

                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS mcount FROM {1} WHERE mark = 1;", NameID, NameTable)
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
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Filter_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Filter.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Marked.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Missing.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_New_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_New.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
        End Select
    End Sub

    Private Sub rbUpdateModifier_Selected_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Selected.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedAsk
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedAuto
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedSkip
        End Select
    End Sub

    Private Sub rbUpdate_Ask_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Ask.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterAsk
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedAsk
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Auto.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterAuto
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedAuto
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rbScrapeType_Skip.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.FilterSkip
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.SelectedSkip
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
        chkMainModifierLandscape.Click,
        chkMainModifierMetaData.Click,
        chkMainModifierNFO.Click,
        chkMainModifierPoster.Click,
        chkMainModifierTheme.Click,
        chkMainModifierTrailer.Click,
        chkMainOptionsActors.Click,
        chkMainOptionsAll.Click,
        chkMainOptionsCertifications.Click,
        chkMainOptionsCollectionID.Click,
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
        chkMainOptionsReleaseDate.Click,
        chkMainOptionsRuntime.Click,
        chkMainOptionsStatus.Click,
        chkMainOptionsStudios.Click,
        chkMainOptionsTagline.Click,
        chkMainOptionsTitle.Click,
        chkMainOptionsTop250.Click,
        chkMainOptionsTrailer.Click,
        chkMainOptionsWriters.Click,
        chkMainOptionsYear.Click,
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

    Private Sub SetUp()

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
        Dim strCancel As String = Master.eLang.GetString(167, "Cancel")
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

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        chkMainModifierLandscape.Text = strLandscape
        chkSeasonModifierLandscape.Text = strLandscape

        'MetaData
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
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
        chkMainOptionsCollectionID.Text = Master.eLang.GetString(1135, "Collection ID")
        chkMainOptionsCountries.Text = Master.eLang.GetString(237, "Countries")
        chkMainOptionsDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkMainOptionsEpisodeGuideURL.Text = Master.eLang.GetString(723, "Episode Guide URL")
        chkMainOptionsGenres.Text = Master.eLang.GetString(725, "Genres")
        chkMainOptionsMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkMainOptionsOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkMainOptionsOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        chkMainOptionsPlot.Text = Master.eLang.GetString(65, "Plot")
        chkMainOptionsRating.Text = Master.eLang.GetString(400, "Rating")
        chkMainOptionsReleaseDate.Text = Master.eLang.GetString(57, "Release Date")
        chkMainOptionsRuntime.Text = Master.eLang.GetString(396, "Runtime")
        chkMainOptionsStudios.Text = Master.eLang.GetString(226, "Studios")
        chkMainOptionsTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkMainOptionsTitle.Text = Master.eLang.GetString(21, "Title")
        chkMainOptionsTop250.Text = Master.eLang.GetString(591, "Top 250")
        chkMainOptionsTrailer.Text = Master.eLang.GetString(151, "Trailer")
        chkMainOptionsWriters.Text = Master.eLang.GetString(394, "Writers")
        chkMainOptionsYear.Text = Master.eLang.GetString(278, "Year")
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