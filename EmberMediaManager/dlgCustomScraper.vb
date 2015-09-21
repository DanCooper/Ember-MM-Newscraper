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
        chkEpisodeModifierAll.Checked = False

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
        chkMainModifierAll.Checked = False

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
        chkSeasonModifierAll.Checked = False

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
        Me.gbMainScrapeOptions.Enabled = chkMainModifierAll.Checked OrElse chkMainModifierNFO.Checked

        Me.gbEpisodeScrapeModifier.Enabled = chkSpecialModifierWithEpisodes.Checked AndAlso Me._ContentType = Enums.ContentType.TV
        Me.gbSeasonScrapeModifier.Enabled = chkSpecialModifierWithSeasons.Checked AndAlso Me._ContentType = Enums.ContentType.TV

        Me.gbEpisodeScrapeOptions.Enabled = (chkEpisodeModifierAll.Checked OrElse chkEpisodeModifierNFO.Checked) AndAlso (chkSpecialModifierAll.Checked OrElse chkSpecialModifierWithEpisodes.Checked)
        Me.gbSeasonScrapeOptions.Enabled = chkSeasonModifierAll.Checked AndAlso (chkSpecialModifierAll.Checked OrElse chkSpecialModifierWithSeasons.Checked) 'OrElse chkSeasonModifierNFO.Checked

        If chkEpisodeModifierAll.Checked Then
            chkEpisodeModifierActorThumbs.Checked = chkEpisodeModifierAll.Checked AndAlso Me.mEpisodeActorThumbsAllowed
            chkEpisodeModifierFanart.Checked = chkEpisodeModifierAll.Checked AndAlso Me.mEpisodeFanartAllowed
            chkEpisodeModifierMetaData.Checked = chkEpisodeModifierAll.Checked AndAlso Me.mEpisodeMetaDataAllowed
            chkEpisodeModifierNFO.Checked = chkEpisodeModifierAll.Checked AndAlso Me.mEpisodeNFOAllowed
            chkEpisodeModifierPoster.Checked = chkEpisodeModifierAll.Checked AndAlso Me.mEpisodePosterAllowed
            chkEpisodeOptionsAll.Checked = chkEpisodeModifierAll.Checked
        Else
            chkEpisodeModifierActorThumbs.Enabled = Me.mEpisodeActorThumbsAllowed
            chkEpisodeModifierFanart.Enabled = Me.mEpisodeFanartAllowed
            chkEpisodeModifierMetaData.Enabled = Me.mEpisodeMetaDataAllowed
            chkEpisodeModifierNFO.Enabled = Me.mEpisodeNFOAllowed
            chkEpisodeModifierPoster.Enabled = Me.mEpisodePosterAllowed
            chkEpisodeOptionsAll.Enabled = True
        End If

        If chkMainModifierAll.Checked Then
            chkMainModifierActorThumbs.Checked = chkMainModifierAll.Checked AndAlso Me.mMainActorThumbsAllowed
            chkMainModifierBanner.Checked = chkMainModifierAll.Checked AndAlso Me.mMainBannerAllowed
            chkMainModifierCharacterArt.Checked = chkMainModifierAll.Checked AndAlso Me.mMainCharacterArtAllowed
            chkMainModifierClearArt.Checked = chkMainModifierAll.Checked AndAlso Me.mMainClearArtAllowed
            chkMainModifierClearLogo.Checked = chkMainModifierAll.Checked AndAlso Me.mMainClearLogoAllowed
            chkMainModifierDiscArt.Checked = chkMainModifierAll.Checked AndAlso Me.mMainDiscArtAllowed
            chkMainModifierExtrafanarts.Checked = chkMainModifierAll.Checked AndAlso Me.mMainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Checked = chkMainModifierAll.Checked AndAlso Me.mMainExtrathumbsAllowed
            chkMainModifierFanart.Checked = chkMainModifierAll.Checked AndAlso Me.mMainFanartAllowed
            chkMainModifierLandscape.Checked = chkMainModifierAll.Checked AndAlso Me.mMainLandscapeAllowed
            chkMainModifierMetaData.Checked = chkMainModifierAll.Checked AndAlso Me.mMainMetaDataAllowed
            chkMainModifierNFO.Checked = chkMainModifierAll.Checked AndAlso Me.mMainNFOAllowed
            chkMainModifierPoster.Checked = chkMainModifierAll.Checked AndAlso Me.mMainPosterAllowed
            chkMainModifierTheme.Checked = chkMainModifierAll.Checked AndAlso Me.mMainThemeAllowed
            chkMainModifierTrailer.Checked = chkMainModifierAll.Checked AndAlso Me.mMainTrailerAllowed
            chkMainOptionsAll.Checked = chkMainModifierAll.Checked
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
            chkMainOptionsAll.Enabled = True
        End If

        If chkSeasonModifierAll.Checked Then
            chkSeasonModifierBanner.Checked = chkSeasonModifierAll.Checked AndAlso Me.mSeasonBannerAllowed
            chkSeasonModifierFanart.Checked = chkSeasonModifierAll.Checked AndAlso Me.mSeasonFanartAllowed
            chkSeasonModifierLandscape.Checked = chkSeasonModifierAll.Checked AndAlso Me.mSeasonLandscapeAllowed
            chkSeasonModifierPoster.Checked = chkSeasonModifierAll.Checked AndAlso Me.mSeasonPosterAllowed
            chkSeasonOptionsAll.Checked = chkSeasonModifierAll.Checked
        Else
            chkSeasonModifierBanner.Enabled = Me.mSeasonBannerAllowed
            chkSeasonModifierFanart.Enabled = Me.mSeasonFanartAllowed
            chkSeasonModifierLandscape.Enabled = Me.mSeasonLandscapeAllowed
            chkSeasonModifierPoster.Enabled = Me.mSeasonPosterAllowed
            chkSeasonOptionsAll.Enabled = True
        End If

        If chkSpecialModifierAll.Checked Then
            chkSpecialModifierWithEpisodes.Checked = chkSpecialModifierAll.Checked AndAlso _ContentType = Enums.ContentType.TV
            chkSpecialModifierWithSeasons.Checked = chkSpecialModifierAll.Checked AndAlso _ContentType = Enums.ContentType.TV
        Else
            chkSpecialModifierWithEpisodes.Enabled = _ContentType = Enums.ContentType.TV
            chkSpecialModifierWithSeasons.Enabled = _ContentType = Enums.ContentType.TV
        End If

        If Me.chkMainModifierAll.Checked Then
            Me.chkMainOptionsAll.Enabled = False
        Else
            Me.chkMainOptionsAll.Enabled = Me.chkMainModifierNFO.Checked
        End If

        If Me.chkMainOptionsAll.Checked Then
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

        If chkMainModifierAll.Checked OrElse chkMainModifierNFO.Checked Then
            If chkMainOptionsActors.Checked OrElse _
                chkMainOptionsDirectors.Checked OrElse _
                chkMainOptionsGenres.Checked OrElse _
                chkMainOptionsMPAA.Checked OrElse _
                chkMainOptionsCertifications.Checked OrElse _
                chkMainOptionsOriginalTitle.Checked OrElse _
                chkMainOptionsOutline.Checked OrElse _
                chkMainOptionsPlot.Checked OrElse _
                chkMainOptionsProducers.Checked OrElse _
                chkMainOptionsRating.Checked OrElse _
                chkMainOptionsReleaseDate.Checked OrElse _
                chkMainOptionsRuntime.Checked OrElse _
                chkMainOptionsStudios.Checked OrElse _
                chkMainOptionsTagline.Checked OrElse _
                chkMainOptionsTitle.Checked OrElse _
                chkMainOptionsTrailer.Checked OrElse _
                chkMainOptionsWriters.Checked OrElse _
                chkMainOptionsYear.Checked OrElse _
                chkMainOptionsTop250.Checked OrElse _
                chkMainOptionsCountries.Checked OrElse _
                chkMainOptionsCollectionID.Checked Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If
        ElseIf chkMainModifierActorThumbs.Checked OrElse _
            chkMainModifierBanner.Checked OrElse _
            chkMainModifierClearLogo.Checked OrElse _
            chkMainModifierClearArt.Checked OrElse _
            chkMainModifierClearLogo.Checked OrElse _
            chkMainModifierDiscArt.Checked OrElse _
            chkMainModifierExtrafanarts.Checked OrElse _
            chkMainModifierExtrathumbs.Checked OrElse _
            chkMainModifierFanart.Checked OrElse _
            chkMainModifierLandscape.Checked OrElse _
            chkMainModifierMetaData.Checked OrElse _
            chkMainModifierPoster.Checked OrElse _
            chkMainModifierTheme.Checked OrElse _
            chkMainModifierTrailer.Checked Then
            btnOK.Enabled = True
        Else
            btnOK.Enabled = False
        End If

        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.All, False)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.EpisodeActorThumbs, chkEpisodeModifierActorThumbs.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.EpisodeFanart, chkEpisodeModifierFanart.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.EpisodeMeta, chkEpisodeModifierMetaData.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.EpisodeNFO, chkEpisodeModifierNFO.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.EpisodePoster, chkEpisodeModifierPoster.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainActorThumbs, chkMainModifierActorThumbs.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainBanner, chkMainModifierBanner.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainCharacterArt, chkMainModifierCharacterArt.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainClearArt, chkMainModifierClearArt.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainClearLogo, chkMainModifierClearLogo.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainDiscArt, chkMainModifierDiscArt.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainFanart, chkMainModifierFanart.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainExtrathumbs, chkMainModifierExtrathumbs.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainExtrafanarts, chkMainModifierExtrafanarts.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainFanart, chkMainModifierFanart.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainLandscape, chkMainModifierLandscape.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainMeta, chkMainModifierMetaData.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainNFO, chkMainModifierNFO.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainPoster, chkMainModifierPoster.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainTheme, chkMainModifierTheme.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainTrailer, chkMainModifierTrailer.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.SeasonBanner, chkSeasonModifierBanner.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.SeasonFanart, chkSeasonModifierFanart.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.SeasonLandscape, chkSeasonModifierLandscape.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.SeasonPoster, chkSeasonModifierPoster.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.withEpisodes, chkSpecialModifierWithEpisodes.Checked)
        Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.withSeasons, chkSpecialModifierWithSeasons.Checked)

        CustomUpdater.ScrapeOptions.bEpisodeActors = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeAired = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeCredits = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeDirector = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeGuestStars = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodePlot = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRating = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeRuntime = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bEpisodeTitle = chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bMainActors = chkMainOptionsActors.Checked
        CustomUpdater.ScrapeOptions.bMainCert = chkMainOptionsCertifications.Checked
        CustomUpdater.ScrapeOptions.bMainCollectionID = chkMainOptionsCollectionID.Checked
        CustomUpdater.ScrapeOptions.bMainCountry = chkMainOptionsCountries.Checked
        CustomUpdater.ScrapeOptions.bMainCreator = chkMainOptionsCreators.Checked
        CustomUpdater.ScrapeOptions.bMainDirector = chkMainOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.bMainEpisodeGuide = chkMainOptionsEpisodeGuideURL.Checked
        CustomUpdater.ScrapeOptions.bMainGenre = chkMainOptionsGenres.Checked
        CustomUpdater.ScrapeOptions.bMainMPAA = chkMainOptionsMPAA.Checked
        CustomUpdater.ScrapeOptions.bMainOriginalTitle = chkMainOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.bMainOutline = chkMainOptionsOutline.Checked
        CustomUpdater.ScrapeOptions.bMainPlot = chkMainOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.bMainPremiered = chkMainOptionsPremiered.Checked
        CustomUpdater.ScrapeOptions.bMainProducers = chkMainOptionsProducers.Checked
        CustomUpdater.ScrapeOptions.bMainRating = chkMainOptionsRating.Checked
        CustomUpdater.ScrapeOptions.bMainRelease = chkMainOptionsReleaseDate.Checked
        CustomUpdater.ScrapeOptions.bMainRuntime = chkMainOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.bMainStatus = chkMainOptionsStatus.Checked
        CustomUpdater.ScrapeOptions.bMainStudio = chkMainOptionsStudios.Checked
        CustomUpdater.ScrapeOptions.bMainTagline = chkMainOptionsTagline.Checked
        CustomUpdater.ScrapeOptions.bMainTitle = chkMainOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.bMainTop250 = chkMainOptionsTop250.Checked
        CustomUpdater.ScrapeOptions.bMainTrailer = chkMainOptionsTrailer.Checked
        CustomUpdater.ScrapeOptions.bMainWriters = chkMainOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.bMainYear = chkMainOptionsYear.Checked
        CustomUpdater.ScrapeOptions.bSeasonAired = chkSeasonOptionsAired.Checked
        CustomUpdater.ScrapeOptions.bSeasonPlot = chkSeasonOptionsPlot.Checked
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
                    Me.oMainCreatorsAllowed = False 'TODO: add creators
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
                    Me.oSeasonAiredAllowed = False 'TODO: add aired
                    Me.oSeasonPlotAllowed = False 'TODO: add plot
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
        chkSpecialModifierWithSeasons.Click, chkEpisodeModifierMetaData.Click

        CheckEnable()
    End Sub

    Private Sub SetUp()
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.Text = Master.eLang.GetString(384, "Custom Scraper")
        Me.btnOK.Text = Master.eLang.GetString(389, "Begin")
        Me.btnMainScrapeModifierNone.Text = Master.eLang.GetString(1139, "Select None")
        Me.btnMainScrapeOptionsNone.Text = Me.btnMainScrapeModifierNone.Text
        Me.chkMainModifierActorThumbs.Text = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkMainModifierAll.Text = Master.eLang.GetString(70, "All Items")
        Me.chkMainModifierBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.chkMainModifierClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.chkMainModifierClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.chkMainModifierDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.chkMainModifierExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkMainModifierExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkMainModifierFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.chkMainModifierLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.chkMainModifierMetaData.Text = Master.eLang.GetString(59, "Meta Data")
        Me.chkMainModifierNFO.Text = Master.eLang.GetString(150, "NFO")
        Me.chkMainModifierPoster.Text = Master.eLang.GetString(148, "Poster")
        Me.chkMainModifierTheme.Text = Master.eLang.GetString(1118, "Theme")
        Me.chkMainModifierTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkMainOptionsAll.Text = Me.chkMainModifierAll.Text
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
        Me.rbScrapeType_All.Text = Master.eLang.GetString(68, "All")
        Me.rbScrapeType_Marked.Text = Master.eLang.GetString(48, "Marked")
        Me.rbScrapeType_Missing.Text = Master.eLang.GetString(40, "Missing Items")
        Me.rbScrapeType_New.Text = Master.eLang.GetString(47, "New")
        Me.rbScrapeType_Ask.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Me.rbScrapeType_Auto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Me.rbScrapeType_Skip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
    End Sub

#End Region 'Methods

End Class