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

Public Class dlgUpdateMedia

#Region "Fields"

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

            If chkModAll.Checked OrElse chkModNFO.Checked Then
                If chkCast.Checked OrElse chkCrew.Checked OrElse chkDirector.Checked OrElse chkGenre.Checked OrElse _
                chkMPAA.Checked OrElse chkCert.Checked OrElse chkMusicBy.Checked OrElse chkOutline.Checked OrElse chkPlot.Checked OrElse _
                chkProducers.Checked OrElse chkRating.Checked OrElse chkRelease.Checked OrElse chkRuntime.Checked OrElse _
                chkStudio.Checked OrElse chkTagline.Checked OrElse chkTitle.Checked OrElse chkTrailer.Checked OrElse _
                chkVotes.Checked OrElse chkVotes.Checked OrElse chkWriters.Checked OrElse chkYear.Checked OrElse chkTop250.Checked OrElse chkCountry.Checked Then
                    Update_Button.Enabled = True
                Else
                    Update_Button.Enabled = False
                End If
            ElseIf chkModPoster.Checked OrElse chkModFanart.Checked OrElse chkModMeta.Checked OrElse chkModEFanarts.Checked OrElse chkModEThumbs.Checked OrElse chkModTrailer.Checked Then
                Update_Button.Enabled = True
            Else
                Update_Button.Enabled = False
            End If

            If Me.chkModAll.Checked Then
                Functions.SetScraperMod(Enums.ModType.All, True)
            Else
                Functions.SetScraperMod(Enums.ModType.ActorThumbs, chkModActorThumbs.Checked, False)
                Functions.SetScraperMod(Enums.ModType.ClearArt, chkModClearArt.Checked, False)
                Functions.SetScraperMod(Enums.ModType.ClearLogo, chkModClearLogo.Checked, False)
                Functions.SetScraperMod(Enums.ModType.DiscArt, chkModDiscArt.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Fanart, chkModFanart.Checked, False)
                Functions.SetScraperMod(Enums.ModType.EThumbs, chkModEThumbs.Checked, False)
                Functions.SetScraperMod(Enums.ModType.EFanarts, chkModEFanarts.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Fanart, chkModFanart.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Landscape, chkModLandscape.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Meta, chkModMeta.Checked, False)
                Functions.SetScraperMod(Enums.ModType.NFO, chkModNFO.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Poster, chkModPoster.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Theme, chkModTheme.Checked, False)
                Functions.SetScraperMod(Enums.ModType.Trailer, chkModTrailer.Checked, False)
            End If

        End With
    End Sub

    Private Sub CheckNewAndMark()
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
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

    Private Sub chkCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCast.CheckedChanged
        CustomUpdater.Options.bCast = chkCast.Checked
        CheckEnable()
    End Sub

    Private Sub chkCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCert.CheckedChanged
        CustomUpdater.Options.bCert = chkCert.Checked
        CheckEnable()
    End Sub

    Private Sub chkCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCrew.CheckedChanged
        CustomUpdater.Options.bOtherCrew = chkCrew.Checked
        CheckEnable()
    End Sub

    Private Sub chkDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDirector.CheckedChanged
        CustomUpdater.Options.bDirector = chkDirector.Checked
        CheckEnable()
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGenre.CheckedChanged
        CustomUpdater.Options.bGenre = chkGenre.Checked
        CheckEnable()
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

    Private Sub chkMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMPAA.CheckedChanged
        CustomUpdater.Options.bMPAA = chkMPAA.Checked
        CheckEnable()
    End Sub

    Private Sub chkMusicBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMusicBy.CheckedChanged
        CustomUpdater.Options.bMusicBy = chkMusicBy.Checked
        CheckEnable()
    End Sub

    Private Sub chkOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutline.CheckedChanged
        CustomUpdater.Options.bOutline = chkOutline.Checked
        CheckEnable()
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlot.CheckedChanged
        CustomUpdater.Options.bPlot = chkPlot.Checked
        CheckEnable()
    End Sub

    Private Sub chkProducers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProducers.CheckedChanged
        CustomUpdater.Options.bProducers = chkProducers.Checked
        CheckEnable()
    End Sub

    Private Sub chkRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRating.CheckedChanged
        CustomUpdater.Options.bRating = chkRating.Checked
        CheckEnable()
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelease.CheckedChanged
        CustomUpdater.Options.bRelease = chkRelease.Checked
        CheckEnable()
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuntime.CheckedChanged
        CustomUpdater.Options.bRuntime = chkRuntime.Checked
        CheckEnable()
    End Sub

    Private Sub chkStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStudio.CheckedChanged
        CustomUpdater.Options.bStudio = chkStudio.Checked
        CheckEnable()
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTagline.CheckedChanged
        CustomUpdater.Options.bTagline = chkTagline.Checked
        CheckEnable()
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTitle.CheckedChanged
        CustomUpdater.Options.bTitle = chkTitle.Checked
        CheckEnable()
    End Sub

    Private Sub chkTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTop250.CheckedChanged
        CustomUpdater.Options.bTop250 = chkTop250.Checked
        CheckEnable()
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCountry.CheckedChanged
        CustomUpdater.Options.bCountry = chkCountry.Checked
        CheckEnable()
    End Sub

    Private Sub chkTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrailer.CheckedChanged
        CustomUpdater.Options.bTrailer = chkTrailer.Checked
        CheckEnable()
    End Sub

    Private Sub chkVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVotes.CheckedChanged
        CustomUpdater.Options.bVotes = chkVotes.Checked
        CheckEnable()
    End Sub

    Private Sub chkWriters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWriters.CheckedChanged
        CustomUpdater.Options.bWriters = chkWriters.Checked
        CheckEnable()
    End Sub

    Private Sub chkYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkYear.CheckedChanged
        CustomUpdater.Options.bYear = chkYear.Checked
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
            Me.chkPlot.Enabled = Not Master.eSettings.MovieLockPlot
            Me.chkPlot.Checked = Not Master.eSettings.MovieLockPlot
            Me.chkOutline.Enabled = Not Master.eSettings.MovieLockOutline
            Me.chkOutline.Checked = Not Master.eSettings.MovieLockOutline
            Me.chkTitle.Enabled = Not Master.eSettings.MovieLockTitle
            Me.chkTitle.Checked = Not Master.eSettings.MovieLockTitle
            Me.chkTagline.Enabled = Not Master.eSettings.MovieLockTagline
            Me.chkTagline.Checked = Not Master.eSettings.MovieLockTagline
            Me.chkRating.Enabled = Not Master.eSettings.MovieLockRating
            Me.chkRating.Checked = Not Master.eSettings.MovieLockRating
            Me.chkStudio.Enabled = Not Master.eSettings.MovieLockStudio
            Me.chkStudio.Checked = Not Master.eSettings.MovieLockStudio
            Me.chkGenre.Enabled = Not Master.eSettings.MovieLockGenre
            Me.chkGenre.Checked = Not Master.eSettings.MovieLockGenre
            Me.chkTrailer.Enabled = Not Master.eSettings.MovieLockTrailer
            Me.chkTrailer.Checked = Not Master.eSettings.MovieLockTrailer

            'set defaults
            CustomUpdater.ScrapeType = Enums.ScrapeType.FullAuto
            Functions.SetScraperMod(Enums.ModType.All, True)

            Me.CheckEnable()

            'check if there are new or marked movies
            Me.CheckNewAndMark()

        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
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
        Me.chkCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkCert.Text = Master.eLang.GetString(722, "Certification")
        Me.chkCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkCrew.Text = Master.eLang.GetString(391, "Other Crew")
        Me.chkDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkMPAA.Text = Master.eLang.GetString(401, "MPAA")
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
        Me.chkMusicBy.Text = Master.eLang.GetString(392, "Music By")
        Me.chkOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkTop250.Text = Master.eLang.GetString(591, "Top 250")
        Me.chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkVotes.Text = Master.eLang.GetString(399, "Votes")
        Me.chkWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkYear.Text = Master.eLang.GetString(278, "Year")
        Me.gbOptions.Text = Master.eLang.GetString(390, "Options")
        Me.gbUpdateItems.Text = Master.eLang.GetString(388, "Modifiers")
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