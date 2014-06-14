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

Imports System.IO
Imports EmberAPI
Imports NLog

Public Class dlgUpdateMedia

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private CustomUpdater As New Structures.CustomUpdaterStruct

#End Region 'Fields

#Region "Methods"

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
        '//
        ' Overload to pass data
        '\\

        If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.CustomUpdater.Canceled = False
        Else
            Me.CustomUpdater.Canceled = True
        End If
        Return Me.CustomUpdater
    End Function

    Private Sub btnModNone_Click(sender As Object, e As EventArgs) Handles btnModNone.Click
        chkModAll.Checked = False

        chkModActorThumbs.Checked = False
        chkModBanner.Checked = False
        chkModClearArt.Checked = False
        chkModClearLogo.Checked = False
        chkModDiscArt.Checked = False
        chkModEFanarts.Checked = False
        chkModEThumbs.Checked = False
        chkModFanart.Checked = False
        chkModLandscape.Checked = False
        chkModMeta.Checked = False
        chkModNFO.Checked = False
        chkModPoster.Checked = False
        chkModTheme.Checked = False
        chkModTrailer.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnOptsNone_Click(sender As Object, e As EventArgs) Handles btnOptsNone.Click
        chkOptsAll.Checked = False
        chkModAll.Checked = False

        chkOptsCast.Checked = False
        chkOptsCert.Checked = False
        chkOptsCountry.Checked = False
        chkOptsCrew.Checked = False
        chkOptsDirector.Checked = False
        chkOptsGenre.Checked = False
        chkOptsMPAA.Checked = False
        chkOptsMusicBy.Checked = False
        chkOptsOutline.Checked = False
        chkOptsPlot.Checked = False
        chkOptsProducers.Checked = False
        chkOptsRating.Checked = False
        chkOptsRelease.Checked = False
        chkOptsRuntime.Checked = False
        chkOptsStudio.Checked = False
        chkOptsTagline.Checked = False
        chkOptsTitle.Checked = False
        chkOptsTop250.Checked = False
        chkOptsTrailer.Checked = False
        chkOptsVotes.Checked = False
        chkOptsWriters.Checked = False
        chkOptsYear.Checked = False

        CheckEnable()
    End Sub

    Private Sub CheckEnable()
        Me.gbOptions.Enabled = chkModAll.Checked OrElse chkModNFO.Checked

        With Master.eSettings

            If chkModAll.Checked Then
                chkModActorThumbs.Checked = chkModAll.Checked
                chkModBanner.Checked = chkModAll.Checked AndAlso .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Banner)
                chkModClearArt.Checked = chkModAll.Checked AndAlso .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearArt)
                chkModClearLogo.Checked = chkModAll.Checked AndAlso .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearLogo)
                chkModDiscArt.Checked = chkModAll.Checked AndAlso .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.DiscArt)
                chkModEFanarts.Checked = chkModAll.Checked AndAlso .MovieEFanartsAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
                chkModEThumbs.Checked = chkModAll.Checked AndAlso .MovieEThumbsAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
                chkModFanart.Checked = chkModAll.Checked AndAlso .MovieFanartAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
                chkModLandscape.Checked = chkModAll.Checked AndAlso .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Landscape)
                chkModPoster.Checked = chkModAll.Checked AndAlso .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Poster)
                chkModMeta.Checked = chkModAll.Checked AndAlso Not Me.rbUpdateModifier_Missing.Checked AndAlso .MovieScraperMetaDataScan
                chkModNFO.Checked = chkModAll.Checked
                chkModTheme.Checked = chkModAll.Checked AndAlso .MovieThemeEnable AndAlso .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Theme)
                chkModTrailer.Checked = chkModAll.Checked AndAlso .MovieTrailerEnable AndAlso .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Trailer)
                chkOptsAll.Checked = chkModAll.Checked
            Else
                If chkModMeta.Checked Then chkModMeta.Checked = Not Me.rbUpdateModifier_Missing.Checked AndAlso .MovieScraperMetaDataScan AndAlso (Not rbUpdate_Ask.Checked OrElse chkModNFO.Checked)
            End If

            chkModActorThumbs.Enabled = Not chkModAll.Checked
            chkModBanner.Enabled = Not chkModAll.Checked AndAlso .MovieBannerAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Banner)
            chkModClearArt.Enabled = Not chkModAll.Checked AndAlso .MovieClearArtAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearArt)
            chkModClearLogo.Enabled = Not chkModAll.Checked AndAlso .MovieClearLogoAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearLogo)
            chkModDiscArt.Enabled = Not chkModAll.Checked AndAlso .MovieDiscArtAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.DiscArt)
            chkModEFanarts.Enabled = Not chkModAll.Checked AndAlso .MovieEFanartsAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
            chkModEThumbs.Enabled = Not chkModAll.Checked AndAlso .MovieEThumbsAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
            chkModFanart.Enabled = Not chkModAll.Checked AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
            chkModFanart.Enabled = Not chkModAll.Checked AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart)
            chkModLandscape.Enabled = Not chkModAll.Checked AndAlso .MovieLandscapeAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Landscape)
            chkModMeta.Enabled = Not chkModAll.Checked AndAlso Not Me.rbUpdateModifier_Missing.Checked AndAlso .MovieScraperMetaDataScan AndAlso (Not rbUpdate_Ask.Checked OrElse chkModNFO.Checked)
            chkModNFO.Enabled = Not chkModAll.Checked
            chkModPoster.Enabled = Not chkModAll.Checked AndAlso .MoviePosterAnyEnabled AndAlso ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Poster)
            chkModTheme.Enabled = Not chkModAll.Checked AndAlso .MovieThemeEnable AndAlso .MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Theme)
            chkModTrailer.Enabled = Not chkModAll.Checked AndAlso .MovieTrailerEnable AndAlso .MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Trailer)
            chkOptsAll.Enabled = Not chkModAll.Checked

            If Me.chkModAll.Checked Then
                Me.chkOptsAll.Enabled = False
            Else
                Me.chkOptsAll.Enabled = Me.chkModNFO.Checked
            End If

            If Me.chkOptsAll.Checked Then
                chkOptsCast.Enabled = False
                chkOptsCert.Checked = True
                chkOptsCert.Enabled = False
                chkOptsCountry.Checked = True
                chkOptsCountry.Enabled = False
                chkOptsCrew.Checked = True
                chkOptsCrew.Enabled = False
                chkOptsDirector.Checked = True
                chkOptsDirector.Enabled = False
                chkOptsGenre.Checked = True
                chkOptsGenre.Enabled = False
                chkOptsMPAA.Checked = True
                chkOptsMPAA.Enabled = False
                chkOptsMusicBy.Checked = True
                chkOptsMusicBy.Enabled = False
                chkOptsOutline.Checked = True
                chkOptsOutline.Enabled = False
                chkOptsPlot.Checked = True
                chkOptsPlot.Enabled = False
                chkOptsProducers.Checked = True
                chkOptsProducers.Enabled = False
                chkOptsRating.Checked = True
                chkOptsRating.Enabled = False
                chkOptsRelease.Checked = True
                chkOptsRelease.Enabled = False
                chkOptsRuntime.Checked = True
                chkOptsRuntime.Enabled = False
                chkOptsStudio.Checked = True
                chkOptsStudio.Enabled = False
                chkOptsTagline.Checked = True
                chkOptsTagline.Enabled = False
                chkOptsTitle.Checked = True
                chkOptsTitle.Enabled = False
                chkOptsTop250.Checked = True
                chkOptsTop250.Enabled = False
                chkOptsTrailer.Checked = True
                chkOptsTrailer.Enabled = False
                chkOptsVotes.Checked = True
                chkOptsVotes.Enabled = False
                chkOptsWriters.Checked = True
                chkOptsWriters.Enabled = False
                chkOptsYear.Checked = True
                chkOptsYear.Enabled = False
                chkOptsCast.Checked = True
            Else
                chkOptsCast.Enabled = True
                chkOptsCert.Enabled = True
                chkOptsCountry.Enabled = True
                chkOptsCrew.Enabled = True
                chkOptsDirector.Enabled = True
                chkOptsGenre.Enabled = True
                chkOptsMPAA.Enabled = True
                chkOptsMusicBy.Enabled = True
                chkOptsOutline.Enabled = True
                chkOptsPlot.Enabled = True
                chkOptsProducers.Enabled = True
                chkOptsRating.Enabled = True
                chkOptsRelease.Enabled = True
                chkOptsRuntime.Enabled = True
                chkOptsStudio.Enabled = True
                chkOptsTagline.Enabled = True
                chkOptsTitle.Enabled = True
                chkOptsTop250.Enabled = True
                chkOptsTrailer.Enabled = True
                chkOptsVotes.Enabled = True
                chkOptsWriters.Enabled = True
                chkOptsYear.Enabled = True
            End If

            If chkModAll.Checked OrElse chkModNFO.Checked Then
                If chkOptsCast.Checked OrElse chkOptsCrew.Checked OrElse chkOptsDirector.Checked OrElse chkOptsGenre.Checked OrElse _
                chkOptsMPAA.Checked OrElse chkOptsCert.Checked OrElse chkOptsMusicBy.Checked OrElse chkOptsOutline.Checked OrElse chkOptsPlot.Checked OrElse _
                chkOptsProducers.Checked OrElse chkOptsRating.Checked OrElse chkOptsRelease.Checked OrElse chkOptsRuntime.Checked OrElse _
                chkOptsStudio.Checked OrElse chkOptsTagline.Checked OrElse chkOptsTitle.Checked OrElse chkOptsTrailer.Checked OrElse _
                chkOptsVotes.Checked OrElse chkOptsVotes.Checked OrElse chkOptsWriters.Checked OrElse chkOptsYear.Checked OrElse chkOptsTop250.Checked OrElse chkOptsCountry.Checked Then
                    Update_Button.Enabled = True
                Else
                    Update_Button.Enabled = False
                End If
            ElseIf chkModActorThumbs.Checked OrElse chkModBanner.Checked OrElse chkModClearArt.Checked OrElse chkModClearLogo.Checked OrElse _
                chkModDiscArt.Checked OrElse chkModEFanarts.Checked OrElse chkModEThumbs.Checked OrElse chkModFanart.Checked OrElse _
                chkModLandscape.Checked OrElse chkModMeta.Checked OrElse chkModPoster.Checked OrElse chkModTheme.Checked OrElse chkModTrailer.Checked Then
                Update_Button.Enabled = True
            Else
                Update_Button.Enabled = False
            End If

            If Me.chkModAll.Checked Then
                Functions.SetScraperMod(Enums.MovieModType.All, True)
            Else
                Functions.SetScraperMod(Enums.MovieModType.ActorThumbs, chkModActorThumbs.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.ClearArt, chkModClearArt.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.ClearLogo, chkModClearLogo.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.DiscArt, chkModDiscArt.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Fanart, chkModFanart.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.EThumbs, chkModEThumbs.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.EFanarts, chkModEFanarts.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Fanart, chkModFanart.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Landscape, chkModLandscape.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Meta, chkModMeta.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.NFO, chkModNFO.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Poster, chkModPoster.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Theme, chkModTheme.Checked, False)
                Functions.SetScraperMod(Enums.MovieModType.Trailer, chkModTrailer.Checked, False)
            End If

        End With
    End Sub

    Private Sub CheckNewAndMark()
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS ncount FROM movies WHERE new = 1;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                rbUpdateModifier_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
            End Using

            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM movies WHERE mark = 1;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                rbUpdateModifier_Marked.Enabled = Convert.ToInt32(SQLcount("mcount")) > 0
            End Using
        End Using
    End Sub

    Private Sub chkModActorThumbs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModActorThumbs.Click
        CheckEnable()
    End Sub

    Private Sub chkModAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModAll.Click
        CheckEnable()
    End Sub

    Private Sub chkModBanner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModBanner.Click
        CheckEnable()
    End Sub

    Private Sub chkModClearArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModClearArt.Click
        CheckEnable()
    End Sub

    Private Sub chkModClearLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModClearLogo.Click
        CheckEnable()
    End Sub

    Private Sub chkModDiscArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModDiscArt.Click
        CheckEnable()
    End Sub

    Private Sub chkModEFanarts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModEFanarts.Click
        CheckEnable()
    End Sub

    Private Sub chkModEThumbs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModEThumbs.Click
        CheckEnable()
    End Sub

    Private Sub chkModFanart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModFanart.Click
        CheckEnable()
    End Sub

    Private Sub chkModLandscape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModLandscape.Click
        CheckEnable()
    End Sub

    Private Sub chkModMeta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModMeta.Click
        CheckEnable()
    End Sub

    Private Sub chkModNFO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModNFO.Click
        CheckEnable()
    End Sub

    Private Sub chkModPoster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModPoster.Click
        CheckEnable()
    End Sub

    Private Sub chkModTheme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModTheme.Click
        CheckEnable()
    End Sub

    Private Sub chkModTrailer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModTrailer.Click
        CheckEnable()
    End Sub

    Private Sub chkOptsAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOptsAll.Click
        CheckEnable()
    End Sub

    Private Sub chkOptsCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsCast.CheckedChanged
        CustomUpdater.Options.bCast = chkOptsCast.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsCert.CheckedChanged
        CustomUpdater.Options.bCert = chkOptsCert.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsCrew.CheckedChanged
        CustomUpdater.Options.bOtherCrew = chkOptsCrew.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsDirector.CheckedChanged
        CustomUpdater.Options.bDirector = chkOptsDirector.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsGenre.CheckedChanged
        CustomUpdater.Options.bGenre = chkOptsGenre.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMPAA.CheckedChanged
        CustomUpdater.Options.bMPAA = chkOptsMPAA.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMusicBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMusicBy.CheckedChanged
        CustomUpdater.Options.bMusicBy = chkOptsMusicBy.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsOutline.CheckedChanged
        CustomUpdater.Options.bOutline = chkOptsOutline.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsPlot.CheckedChanged
        CustomUpdater.Options.bPlot = chkOptsPlot.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsProducers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsProducers.CheckedChanged
        CustomUpdater.Options.bProducers = chkOptsProducers.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsRating.CheckedChanged
        CustomUpdater.Options.bRating = chkOptsRating.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsRelease.CheckedChanged
        CustomUpdater.Options.bRelease = chkOptsRelease.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsRuntime.CheckedChanged
        CustomUpdater.Options.bRuntime = chkOptsRuntime.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsStudio.CheckedChanged
        CustomUpdater.Options.bStudio = chkOptsStudio.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsTagline.CheckedChanged
        CustomUpdater.Options.bTagline = chkOptsTagline.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsTitle.CheckedChanged
        CustomUpdater.Options.bTitle = chkOptsTitle.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsTop250.CheckedChanged
        CustomUpdater.Options.bTop250 = chkOptsTop250.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsCountry.CheckedChanged
        CustomUpdater.Options.bCountry = chkOptsCountry.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsTrailer.CheckedChanged
        CustomUpdater.Options.bTrailer = chkOptsTrailer.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsVotes.CheckedChanged
        CustomUpdater.Options.bVotes = chkOptsVotes.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsWriters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsWriters.CheckedChanged
        CustomUpdater.Options.bWriters = chkOptsWriters.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsYear.CheckedChanged
        CustomUpdater.Options.bYear = chkOptsYear.Checked
        CheckEnable()
    End Sub

    Private Sub dlgUpdateMedia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.SetUp()

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            'disable options that are locked
            Me.chkOptsPlot.Enabled = Not Master.eSettings.MovieLockPlot
            Me.chkOptsPlot.Checked = Not Master.eSettings.MovieLockPlot
            Me.chkOptsOutline.Enabled = Not Master.eSettings.MovieLockOutline
            Me.chkOptsOutline.Checked = Not Master.eSettings.MovieLockOutline
            Me.chkOptsTitle.Enabled = Not Master.eSettings.MovieLockTitle
            Me.chkOptsTitle.Checked = Not Master.eSettings.MovieLockTitle
            Me.chkOptsTagline.Enabled = Not Master.eSettings.MovieLockTagline
            Me.chkOptsTagline.Checked = Not Master.eSettings.MovieLockTagline
            Me.chkOptsRating.Enabled = Not Master.eSettings.MovieLockRating
            Me.chkOptsRating.Checked = Not Master.eSettings.MovieLockRating
            Me.chkOptsStudio.Enabled = Not Master.eSettings.MovieLockStudio
            Me.chkOptsStudio.Checked = Not Master.eSettings.MovieLockStudio
            Me.chkOptsGenre.Enabled = Not Master.eSettings.MovieLockGenre
            Me.chkOptsGenre.Checked = Not Master.eSettings.MovieLockGenre
            Me.chkOptsTrailer.Enabled = Not Master.eSettings.MovieLockTrailer
            Me.chkOptsTrailer.Checked = Not Master.eSettings.MovieLockTrailer

            'set defaults
            CustomUpdater.ScrapeType = Enums.ScrapeType.FullAuto
            Functions.SetScraperMod(Enums.MovieModType.All, True)

            Me.CheckEnable()

            'check if there are new or marked movies
            Me.CheckNewAndMark()

        Catch ex As Exception
            Logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Sub dlgUpdateMedia_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub rbUpdateModifier_All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_All.CheckedChanged
        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullSkip
        End If
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_Marked.CheckedChanged
        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkSkip
        End If
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_Missing.CheckedChanged
        If Me.rbUpdateModifier_Missing.Checked Then
            Me.chkModMeta.Checked = False
            Me.chkModMeta.Enabled = False
        End If

        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateSkip
        End If

        Me.CheckEnable()
    End Sub

    Private Sub rbUpdateModifier_New_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_New.CheckedChanged
        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
        End If
    End Sub

    Private Sub rbUpdate_Ask_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdate_Ask.CheckedChanged
        Select Case True
            Case Me.rbUpdateModifier_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullAsk
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateAsk
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
            Case rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkAsk
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdate_Auto.CheckedChanged
        Select Case True
            Case Me.rbUpdateModifier_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullAuto
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateAuto
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
            Case Me.rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkAuto
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdate_Skip.CheckedChanged
        Select Case True
            Case Me.rbUpdateModifier_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.FullSkip
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.UpdateSkip
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
            Case Me.rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkSkip
        End Select
    End Sub

    Private Sub SetUp()
        Me.OK_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.Text = Master.eLang.GetString(384, "Custom Scraper")
        Me.Update_Button.Text = Master.eLang.GetString(389, "Begin")
        Me.btnModNone.Text = Master.eLang.GetString(1139, "Select None")
        Me.btnOptsNone.Text = Me.btnModNone.Text
        Me.chkModActorThumbs.Text = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkModAll.Text = Master.eLang.GetString(70, "All Items")
        Me.chkModBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.chkModClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.chkModClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.chkModDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.chkModEFanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkModEThumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkModFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.chkModLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.chkModMeta.Text = Master.eLang.GetString(59, "Meta Data")
        Me.chkModNFO.Text = Master.eLang.GetString(150, "NFO")
        Me.chkModPoster.Text = Master.eLang.GetString(148, "Poster")
        Me.chkModTheme.Text = Master.eLang.GetString(1118, "Theme")
        Me.chkModTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkOptsAll.Text = Me.chkModAll.Text
        Me.chkOptsCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkOptsCert.Text = Master.eLang.GetString(722, "Certification")
        Me.chkOptsCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkOptsCrew.Text = Master.eLang.GetString(391, "Other Crew")
        Me.chkOptsDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkOptsGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkOptsMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkOptsMusicBy.Text = Master.eLang.GetString(392, "Music By")
        Me.chkOptsOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkOptsPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkOptsProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkOptsRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkOptsRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkOptsRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkOptsStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkOptsTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkOptsTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkOptsTop250.Text = Master.eLang.GetString(591, "Top 250")
        Me.chkOptsTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkOptsVotes.Text = Master.eLang.GetString(399, "Votes")
        Me.chkOptsWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkOptsYear.Text = Master.eLang.GetString(278, "Year")
        Me.gbOptions.Text = Master.eLang.GetString(390, "Options")
        Me.gbModifiers.Text = Master.eLang.GetString(388, "Modifiers")
        Me.gbUpdateModifier.Text = Master.eLang.GetString(386, "Selection Filter")
        Me.gbUpdateType.Text = Master.eLang.GetString(387, "Update Mode")
        Me.lblTopDescription.Text = Master.eLang.GetString(385, "Create a custom scraper")
        Me.lblTopTitle.Text = Me.Text
        Me.rbUpdateModifier_All.Text = Master.eLang.GetString(68, "All Movies")
        Me.rbUpdateModifier_Marked.Text = Master.eLang.GetString(80, "Marked Movies")
        Me.rbUpdateModifier_Missing.Text = Master.eLang.GetString(78, "Movies Missing Items")
        Me.rbUpdateModifier_New.Text = Master.eLang.GetString(79, "New Movies")
        Me.rbUpdate_Ask.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Me.rbUpdate_Auto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Me.rbUpdate_Skip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
    End Sub

    Private Sub Update_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region 'Methods

End Class