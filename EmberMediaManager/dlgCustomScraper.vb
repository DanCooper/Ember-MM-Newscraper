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

    Private EpisodeActorThumbsAllowed As Boolean
    Private EpisodeFanartAllowed As Boolean
    Private EpisodeMetaDataAllowed As Boolean
    Private EpisodeNFOAllowed As Boolean
    Private EpisodePosterAllowed As Boolean
    Private MainActorThumbsAllowed As Boolean
    Private MainBannerAllowed As Boolean
    Private MainCharacterArtAllowed As Boolean
    Private MainClearArtAllowed As Boolean
    Private MainClearLogoAllowed As Boolean
    Private MainDiscArtAllowed As Boolean
    Private MainExtrafanartsAllowed As Boolean
    Private MainExtrathumbsAllowed As Boolean
    Private MainFanartAllowed As Boolean
    Private MainLandscapeAllowed As Boolean
    Private MainMetaDataAllowed As Boolean
    Private MainNFOAllowed As Boolean
    Private MainPosterAllowed As Boolean
    Private MainThemeAllowed As Boolean
    Private MainTrailerAllowed As Boolean
    Private SeasonBannerAllowed As Boolean
    Private SeasonFanartAllowed As Boolean
    Private SeasonLandscapeAllowed As Boolean
    Private SeasonPosterAllowed As Boolean

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
        Me.gbEpisodeScrapeOptions.Enabled = (chkEpisodeModifierAll.Checked OrElse chkEpisodeModifierNFO.Checked) AndAlso (chkSpecialModifierAll.Checked OrElse chkSpecialModifierWithEpisodes.Checked)
        Me.gbMainScrapeOptions.Enabled = chkMainModifierAll.Checked OrElse chkMainModifierNFO.Checked
        Me.gbSeasonScrapeOptions.Enabled = chkSeasonModifierAll.Checked AndAlso (chkSpecialModifierAll.Checked OrElse chkSpecialModifierWithEpisodes.Checked) 'OrElse chkSeasonModifierNFO.Checked

        If chkEpisodeModifierAll.Checked Then
            chkEpisodeModifierActorThumbs.Checked = chkEpisodeModifierAll.Checked AndAlso Me.EpisodeActorThumbsAllowed
            chkEpisodeModifierFanart.Checked = chkEpisodeModifierAll.Checked AndAlso Me.EpisodeFanartAllowed
            chkEpisodeModifierMetaData.Checked = chkEpisodeModifierAll.Checked AndAlso Me.EpisodeMetaDataAllowed
            chkEpisodeModifierNFO.Checked = chkEpisodeModifierAll.Checked AndAlso Me.EpisodeNFOAllowed
            chkEpisodeModifierPoster.Checked = chkEpisodeModifierAll.Checked AndAlso Me.EpisodePosterAllowed
            chkEpisodeOptionsAll.Checked = chkEpisodeModifierAll.Checked
        Else
            chkEpisodeModifierActorThumbs.Enabled = Me.EpisodeActorThumbsAllowed
            chkEpisodeModifierFanart.Enabled = Me.EpisodeFanartAllowed
            chkEpisodeModifierMetaData.Enabled = Me.EpisodeMetaDataAllowed
            chkEpisodeModifierNFO.Enabled = Me.EpisodeNFOAllowed
            chkEpisodeModifierPoster.Enabled = Me.EpisodePosterAllowed
            chkEpisodeOptionsAll.Enabled = True
        End If

        If chkMainModifierAll.Checked Then
            chkMainModifierActorThumbs.Checked = chkMainModifierAll.Checked AndAlso Me.MainActorThumbsAllowed
            chkMainModifierBanner.Checked = chkMainModifierAll.Checked AndAlso Me.MainBannerAllowed
            chkMainModifierCharacterArt.Checked = chkMainModifierAll.Checked AndAlso Me.MainCharacterArtAllowed
            chkMainModifierClearArt.Checked = chkMainModifierAll.Checked AndAlso Me.MainClearArtAllowed
            chkMainModifierClearLogo.Checked = chkMainModifierAll.Checked AndAlso Me.MainClearLogoAllowed
            chkMainModifierDiscArt.Checked = chkMainModifierAll.Checked AndAlso Me.MainDiscArtAllowed
            chkMainModifierExtrafanarts.Checked = chkMainModifierAll.Checked AndAlso Me.MainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Checked = chkMainModifierAll.Checked AndAlso Me.MainExtrathumbsAllowed
            chkMainModifierFanart.Checked = chkMainModifierAll.Checked AndAlso Me.MainFanartAllowed
            chkMainModifierLandscape.Checked = chkMainModifierAll.Checked AndAlso Me.MainLandscapeAllowed
            chkMainModifierMetaData.Checked = chkMainModifierAll.Checked AndAlso Me.MainMetaDataAllowed
            chkMainModifierNFO.Checked = chkMainModifierAll.Checked AndAlso Me.MainNFOAllowed
            chkMainModifierPoster.Checked = chkMainModifierAll.Checked AndAlso Me.MainPosterAllowed
            chkMainModifierTheme.Checked = chkMainModifierAll.Checked AndAlso Me.MainThemeAllowed
            chkMainModifierTrailer.Checked = chkMainModifierAll.Checked AndAlso Me.MainTrailerAllowed
            chkMainOptionsAll.Checked = chkMainModifierAll.Checked
        Else
            chkMainModifierActorThumbs.Enabled = Me.MainActorThumbsAllowed
            chkMainModifierBanner.Enabled = Me.MainBannerAllowed
            chkMainModifierCharacterArt.Enabled = Me.MainCharacterArtAllowed
            chkMainModifierClearArt.Enabled = Me.MainClearArtAllowed
            chkMainModifierClearLogo.Enabled = Me.MainClearLogoAllowed
            chkMainModifierDiscArt.Enabled = Me.MainDiscArtAllowed
            chkMainModifierExtrafanarts.Enabled = Me.MainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Enabled = Me.MainExtrathumbsAllowed
            chkMainModifierFanart.Enabled = Me.MainFanartAllowed
            chkMainModifierLandscape.Enabled = Me.MainLandscapeAllowed
            chkMainModifierMetaData.Enabled = Me.MainMetaDataAllowed
            chkMainModifierNFO.Enabled = Me.MainNFOAllowed
            chkMainModifierPoster.Enabled = Me.MainPosterAllowed
            chkMainModifierTheme.Enabled = Me.MainThemeAllowed
            chkMainModifierTrailer.Enabled = Me.MainTrailerAllowed
            chkMainOptionsAll.Enabled = True
        End If

        If chkSeasonModifierAll.Checked Then
            chkSeasonModifierBanner.Checked = chkSeasonModifierAll.Checked AndAlso Me.SeasonBannerAllowed
            chkSeasonModifierFanart.Checked = chkSeasonModifierAll.Checked AndAlso Me.SeasonFanartAllowed
            chkSeasonModifierLandscape.Checked = chkSeasonModifierAll.Checked AndAlso Me.SeasonLandscapeAllowed
            chkSeasonModifierPoster.Checked = chkSeasonModifierAll.Checked AndAlso Me.SeasonPosterAllowed
            chkSeasonOptionsAll.Checked = chkSeasonModifierAll.Checked
        Else
            chkSeasonModifierBanner.Enabled = Me.SeasonBannerAllowed
            chkSeasonModifierFanart.Enabled = Me.SeasonFanartAllowed
            chkSeasonModifierLandscape.Enabled = Me.SeasonLandscapeAllowed
            chkSeasonModifierPoster.Enabled = Me.SeasonPosterAllowed
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
            chkMainOptionsActors.Checked = True
            chkMainOptionsActors.Enabled = False
            chkMainOptionsCertifications.Checked = True
            chkMainOptionsCertifications.Enabled = False
            chkMainOptionsCollectionID.Checked = True
            chkMainOptionsCollectionID.Enabled = False
            chkMainOptionsCountries.Checked = True
            chkMainOptionsCountries.Enabled = False
            chkMainOptionsDirectors.Checked = True
            chkMainOptionsDirectors.Enabled = False
            chkMainOptionsGenres.Checked = True
            chkMainOptionsGenres.Enabled = False
            chkMainOptionsMPAA.Checked = True
            chkMainOptionsMPAA.Enabled = False
            chkMainOptionsOriginalTitle.Checked = True
            chkMainOptionsOriginalTitle.Enabled = False
            chkMainOptionsOutline.Checked = True
            chkMainOptionsOutline.Enabled = False
            chkMainOptionsPlot.Checked = True
            chkMainOptionsPlot.Enabled = False
            chkMainOptionsProducers.Checked = True
            chkMainOptionsProducers.Enabled = False
            chkMainOptionsRating.Checked = True
            chkMainOptionsRating.Enabled = False
            chkMainOptionsReleaseDate.Checked = True
            chkMainOptionsReleaseDate.Enabled = False
            chkMainOptionsRuntime.Checked = True
            chkMainOptionsRuntime.Enabled = False
            chkMainOptionsStudios.Checked = True
            chkMainOptionsStudios.Enabled = False
            chkMainOptionsTagline.Checked = True
            chkMainOptionsTagline.Enabled = False
            chkMainOptionsTitle.Checked = True
            chkMainOptionsTitle.Enabled = False
            chkMainOptionsTop250.Checked = True
            chkMainOptionsTop250.Enabled = False
            chkMainOptionsTrailer.Checked = True
            chkMainOptionsTrailer.Enabled = False
            chkMainOptionsWriters.Checked = True
            chkMainOptionsWriters.Enabled = False
            chkMainOptionsYear.Checked = True
            chkMainOptionsYear.Enabled = False
        Else
            chkMainOptionsActors.Enabled = True
            chkMainOptionsCertifications.Enabled = True
            chkMainOptionsCollectionID.Enabled = True
            chkMainOptionsCountries.Enabled = True
            chkMainOptionsDirectors.Enabled = True
            chkMainOptionsGenres.Enabled = True
            chkMainOptionsMPAA.Enabled = True
            chkMainOptionsOriginalTitle.Enabled = True
            chkMainOptionsOutline.Enabled = True
            chkMainOptionsPlot.Enabled = True
            chkMainOptionsProducers.Enabled = True
            chkMainOptionsRating.Enabled = True
            chkMainOptionsReleaseDate.Enabled = True
            chkMainOptionsRuntime.Enabled = True
            chkMainOptionsStudios.Enabled = True
            chkMainOptionsTagline.Enabled = True
            chkMainOptionsTitle.Enabled = True
            chkMainOptionsTop250.Enabled = True
            chkMainOptionsTrailer.Enabled = True
            chkMainOptionsWriters.Enabled = True
            chkMainOptionsYear.Enabled = True
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

                    Me.EpisodeActorThumbsAllowed = False
                    Me.EpisodeFanartAllowed = False
                    Me.EpisodeMetaDataAllowed = False
                    Me.EpisodeNFOAllowed = False
                    Me.EpisodePosterAllowed = False
                    Me.MainActorThumbsAllowed = .MovieActorThumbsAnyEnabled
                    Me.MainBannerAllowed = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    Me.MainCharacterArtAllowed = False
                    Me.MainClearArtAllowed = .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    Me.MainClearLogoAllowed = .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    Me.MainDiscArtAllowed = .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    Me.MainExtrafanartsAllowed = .MovieExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.MainExtrathumbsAllowed = .MovieExtrathumbsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.MainFanartAllowed = .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    Me.MainLandscapeAllowed = .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    Me.MainMetaDataAllowed = .MovieScraperMetaDataScan
                    Me.MainNFOAllowed = .MovieNFOAnyEnabled
                    Me.MainPosterAllowed = .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    Me.MainThemeAllowed = .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    Me.MainTrailerAllowed = .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie(Enums.ModifierType.MainTrailer)
                    Me.SeasonBannerAllowed = False
                    Me.SeasonFanartAllowed = False
                    Me.SeasonLandscapeAllowed = False
                    Me.SeasonPosterAllowed = False

                Case Enums.ContentType.MovieSet
                    NameID = "idSet"
                    NameTable = "sets"
                    Me.gbEpisodeScrapeModifier.Visible = False
                    Me.gbEpisodeScrapeOptions.Visible = False
                    Me.gbSeasonScrapeModifier.Visible = False
                    Me.gbSeasonScrapeOptions.Visible = False
                    Me.gbSpecialScrapeModifier.Visible = False

                    Me.EpisodeActorThumbsAllowed = False
                    Me.EpisodeFanartAllowed = False
                    Me.EpisodeMetaDataAllowed = False
                    Me.EpisodeNFOAllowed = False
                    Me.EpisodePosterAllowed = False
                    Me.MainActorThumbsAllowed = False
                    Me.MainBannerAllowed = .MovieSetBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner)
                    Me.MainCharacterArtAllowed = False
                    Me.MainClearArtAllowed = .MovieSetClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt)
                    Me.MainClearLogoAllowed = .MovieSetClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo)
                    Me.MainDiscArtAllowed = .MovieSetDiscArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt)
                    Me.MainExtrafanartsAllowed = False
                    Me.MainExtrathumbsAllowed = False
                    Me.MainFanartAllowed = .MovieSetFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart)
                    Me.MainLandscapeAllowed = .MovieSetLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape)
                    Me.MainMetaDataAllowed = False
                    Me.MainNFOAllowed = .MovieSetNFOAnyEnabled
                    Me.MainPosterAllowed = .MovieSetPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster)
                    Me.MainThemeAllowed = False
                    Me.MainTrailerAllowed = False
                    Me.SeasonBannerAllowed = False
                    Me.SeasonFanartAllowed = False
                    Me.SeasonLandscapeAllowed = False
                    Me.SeasonPosterAllowed = False

                Case Enums.ContentType.TV
                    NameID = "idShow"
                    NameTable = "tvshow"

                    Me.EpisodeActorThumbsAllowed = .TVEpisodeActorThumbsAnyEnabled
                    Me.EpisodeFanartAllowed = .TVEpisodeFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    Me.EpisodeMetaDataAllowed = .TVScraperMetaDataScan
                    Me.EpisodeNFOAllowed = .TVEpisodeNFOAnyEnabled
                    Me.EpisodePosterAllowed = .TVEpisodePosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    Me.MainActorThumbsAllowed = .TVShowActorThumbsAnyEnabled
                    Me.MainBannerAllowed = .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    Me.MainCharacterArtAllowed = .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    Me.MainClearArtAllowed = .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    Me.MainClearLogoAllowed = .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    Me.MainDiscArtAllowed = False
                    Me.MainExtrafanartsAllowed = .TVShowExtrafanartsAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    Me.MainExtrathumbsAllowed = False
                    Me.MainFanartAllowed = .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    Me.MainLandscapeAllowed = .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    Me.MainMetaDataAllowed = False
                    Me.MainNFOAllowed = .TVShowNFOAnyEnabled
                    Me.MainPosterAllowed = .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    Me.MainThemeAllowed = .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie(Enums.ModifierType.MainTheme)
                    Me.MainTrailerAllowed = False
                    Me.SeasonBannerAllowed = .TVSeasonBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    Me.SeasonFanartAllowed = .TVSeasonFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    Me.SeasonLandscapeAllowed = .TVSeasonLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    Me.SeasonPosterAllowed = .TVSeasonPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)
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