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
' # Dialog size: 750; 570
' # Enlarge it to see all the panels.

Imports System.IO
Imports EmberAPI
Imports NLog

Public Class dlgWizard

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private tLang As String
    Private tmppath As String = String.Empty

#End Region

#Region "Methods"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Select Case True
            Case pnlMovieSources.Visible
                btnBack.Enabled = False
                pnlMovieSources.Visible = False
                pnlWelcome.Visible = True
            Case pnlMovieSettings.Visible
                pnlMovieSettings.Visible = False
                pnlMovieSources.Visible = True
            Case pnlMovieSetSettings.Visible
                pnlMovieSetSettings.Visible = False
                pnlMovieSettings.Visible = True
            Case pnlTVShowSource.Visible
                pnlTVShowSource.Visible = False
                pnlMovieSetSettings.Visible = True
            Case pnlTVShowSettings.Visible
                pnlTVShowSettings.Visible = False
                pnlTVShowSource.Visible = True
            Case pnlDone.Visible
                pnlDone.Visible = False
                pnlTVShowSettings.Visible = True
                btnNext.Enabled = True
                OK_Button.Enabled = False
        End Select
    End Sub

    Private Sub btnTVGeneralLangFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralLangFetch.Click
        Master.eSettings.TVGeneralLanguages = ModulesManager.Instance.GetTVLanguages()
        cbTVGeneralLang.Items.Clear()
        cbTVGeneralLang.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

        If cbTVGeneralLang.Items.Count > 0 Then
            cbTVGeneralLang.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVGeneralLanguage).name
        End If
    End Sub

    Private Sub btnMovieAddFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieAddFolder.Click
        Using dSource As New dlgSourceMovie()
            If dSource.ShowDialog(tmppath) = DialogResult.OK Then
                RefreshSources()
            End If
        End Using
    End Sub

    Private Sub btnMovieRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieRem.Click
        RemoveSource()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Select Case True
            Case pnlWelcome.Visible
                btnBack.Enabled = True
                pnlWelcome.Visible = False
                pnlMovieSources.Visible = True
            Case pnlMovieSources.Visible
                pnlMovieSources.Visible = False
                pnlMovieSettings.Visible = True
            Case pnlMovieSettings.Visible
                pnlMovieSettings.Visible = False
                pnlMovieSetSettings.Visible = True
            Case pnlMovieSetSettings.Visible
                pnlMovieSetSettings.Visible = False
                pnlTVShowSource.Visible = True
            Case pnlTVShowSource.Visible
                pnlTVShowSource.Visible = False
                pnlTVShowSettings.Visible = True
            Case pnlTVShowSettings.Visible
                pnlTVShowSettings.Visible = False
                pnlDone.Visible = True
                btnNext.Enabled = False
                OK_Button.Enabled = True
        End Select
    End Sub

    Private Sub btnTVAddSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVAddSource.Click
        Using dSource As New dlgSourceTVShow
            If dSource.ShowDialog(tmppath) = DialogResult.OK Then
                RefreshTVSources()
            End If
        End Using
    End Sub

    Private Sub btnTVRemoveSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVRemoveSource.Click
        RemoveTVSource()
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    Private Sub cbTVGeneralLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTVGeneralLang.SelectedIndexChanged
        If Not String.IsNullOrEmpty(cbTVGeneralLang.Text) Then
            Master.eSettings.TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbTVGeneralLang.Text).abbreviation
        End If
    End Sub

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBoxee.CheckedChanged

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

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseFrodo.CheckedChanged

        chkMovieActorThumbsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieBannerAD.Enabled = chkMovieUseFrodo.Checked
        chkMovieClearArtAD.Enabled = chkMovieUseFrodo.Checked
        chkMovieClearLogoAD.Enabled = chkMovieUseFrodo.Checked
        chkMovieExtrafanartsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieExtrathumbsFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieDiscArtAD.Enabled = chkMovieUseFrodo.Checked
        chkMovieFanartFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieLandscapeAD.Enabled = chkMovieUseFrodo.Checked
        chkMovieNFOFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMoviePosterFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieTrailerFrodo.Enabled = chkMovieUseFrodo.Checked
        chkMovieThemeTvTunesEnable.Enabled = chkMovieUseFrodo.Checked OrElse chkMovieUseEden.Checked
        chkMovieXBMCProtectVTSBDMV.Enabled = chkMovieUseFrodo.Checked AndAlso Not chkMovieUseEden.Checked

        If Not chkMovieUseFrodo.Checked Then
            chkMovieActorThumbsFrodo.Checked = False
            chkMovieBannerAD.Checked = False
            chkMovieClearArtAD.Checked = False
            chkMovieClearLogoAD.Checked = False
            chkMovieDiscArtAD.Checked = False
            chkMovieExtrafanartsFrodo.Checked = False
            chkMovieExtrathumbsFrodo.Checked = False
            chkMovieFanartFrodo.Checked = False
            chkMovieLandscapeAD.Checked = False
            chkMovieNFOFrodo.Checked = False
            chkMoviePosterFrodo.Checked = False
            chkMovieTrailerFrodo.Checked = False
            chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            chkMovieActorThumbsFrodo.Checked = True
            chkMovieBannerAD.Checked = True
            chkMovieClearArtAD.Checked = True
            chkMovieClearLogoAD.Checked = True
            chkMovieDiscArtAD.Checked = True
            chkMovieExtrafanartsFrodo.Checked = True
            chkMovieExtrathumbsFrodo.Checked = True
            chkMovieFanartFrodo.Checked = True
            chkMovieLandscapeAD.Checked = True
            chkMovieNFOFrodo.Checked = True
            chkMoviePosterFrodo.Checked = True
            chkMovieTrailerFrodo.Checked = True
        End If

        If Not chkMovieUseFrodo.Checked AndAlso Not chkMovieUseEden.Checked Then
            chkMovieThemeTvTunesEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseEden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseEden.CheckedChanged

        chkMovieActorThumbsEden.Enabled = chkMovieUseEden.Checked
        chkMovieExtrafanartsEden.Enabled = chkMovieUseEden.Checked
        chkMovieExtrathumbsEden.Enabled = chkMovieUseEden.Checked
        chkMovieFanartEden.Enabled = chkMovieUseEden.Checked
        chkMovieNFOEden.Enabled = chkMovieUseEden.Checked
        chkMoviePosterEden.Enabled = chkMovieUseEden.Checked
        chkMovieTrailerEden.Enabled = chkMovieUseEden.Checked
        chkMovieThemeTvTunesEnable.Enabled = chkMovieUseEden.Checked OrElse chkMovieUseFrodo.Checked
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

        If Not chkMovieUseEden.Checked AndAlso Not chkMovieUseFrodo.Checked Then
            chkMovieThemeTvTunesEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseYAMJ.CheckedChanged

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

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged

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

    Private Sub chkMovieXBMCThemeCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesCustom.CheckedChanged

        txtMovieThemeTvTunesCustomPath.Enabled = chkMovieThemeTvTunesCustom.Checked
        btnMovieXBMCThemeCustomPathBrowse.Enabled = chkMovieThemeTvTunesCustom.Checked

        If chkMovieThemeTvTunesCustom.Checked Then
            chkMovieThemeTvTunesMoviePath.Enabled = False
            chkMovieThemeTvTunesMoviePath.Checked = False
            chkMovieThemeTvTunesSub.Enabled = False
            chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not chkMovieThemeTvTunesCustom.Checked AndAlso chkMovieThemeTvTunesEnable.Checked Then
            chkMovieThemeTvTunesMoviePath.Enabled = True
            chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesEnable.CheckedChanged

        chkMovieThemeTvTunesCustom.Enabled = chkMovieThemeTvTunesEnable.Checked
        chkMovieThemeTvTunesMoviePath.Enabled = chkMovieThemeTvTunesEnable.Checked
        chkMovieThemeTvTunesSub.Enabled = chkMovieThemeTvTunesEnable.Checked

        If Not chkMovieThemeTvTunesEnable.Checked Then
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesMoviePath.Checked = False
            chkMovieThemeTvTunesSub.Checked = False
        Else
            chkMovieThemeTvTunesMoviePath.Checked = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesMoviePath.CheckedChanged

        If chkMovieThemeTvTunesMoviePath.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = False
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesSub.Enabled = False
            chkMovieThemeTvTunesSub.Checked = False
        End If

        If Not chkMovieThemeTvTunesMoviePath.Checked AndAlso chkMovieThemeTvTunesEnable.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = True
            chkMovieThemeTvTunesSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeSub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieThemeTvTunesSub.CheckedChanged

        txtMovieThemeTvTunesSubDir.Enabled = chkMovieThemeTvTunesSub.Checked

        If chkMovieThemeTvTunesSub.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = False
            chkMovieThemeTvTunesCustom.Checked = False
            chkMovieThemeTvTunesMoviePath.Enabled = False
            chkMovieThemeTvTunesMoviePath.Checked = False
        End If

        If Not chkMovieThemeTvTunesSub.Checked AndAlso chkMovieThemeTvTunesEnable.Checked Then
            chkMovieThemeTvTunesCustom.Enabled = True
            chkMovieThemeTvTunesMoviePath.Enabled = True
        End If
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        txtMovieYAMJWatchedFolder.Enabled = chkMovieYAMJWatchedFile.Checked
        btnMovieYAMJWatchedFilesBrowse.Enabled = chkMovieYAMJWatchedFile.Checked
    End Sub

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertSingle.CheckedChanged
        chkMovieUnstackExpertSingle.Enabled = chkMovieStackExpertSingle.Checked AndAlso chkMovieStackExpertSingle.Enabled
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertMulti.CheckedChanged
        chkMovieUnstackExpertMulti.Enabled = chkMovieStackExpertMulti.Checked AndAlso chkMovieStackExpertMulti.Enabled
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        btnMovieSetPathExpertSingleBrowse.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetBannerExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetBannerExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearArtExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearArtExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearLogoExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetClearLogoExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetFanartExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetFanartExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetLandscapeExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetLandscapeExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetNFOExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetNFOExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPathExpertSingle.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPosterExpertParent.Enabled = chkMovieSetUseExpert.Checked
        txtMovieSetPosterExpertSingle.Enabled = chkMovieSetUseExpert.Checked
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkMovieUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseExpert.CheckedChanged
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

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseMSAA.CheckedChanged
        btnMovieSetPathMSAABrowse.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetBannerMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearArtMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetClearLogoMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetFanartMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetLandscapeMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetNFOMSAA.Enabled = chkMovieSetUseMSAA.Checked
        chkMovieSetPosterMSAA.Enabled = chkMovieSetUseMSAA.Checked
        txtMovieSetPathMSAA.Enabled = chkMovieSetUseMSAA.Checked

        If Not chkMovieSetUseMSAA.Checked Then
            chkMovieSetBannerMSAA.Checked = False
            chkMovieSetClearArtMSAA.Checked = False
            chkMovieSetClearLogoMSAA.Checked = False
            chkMovieSetFanartMSAA.Checked = False
            chkMovieSetLandscapeMSAA.Checked = False
            chkMovieSetNFOMSAA.Checked = False
            chkMovieSetPosterMSAA.Checked = False
        Else
            chkMovieSetBannerMSAA.Checked = True
            chkMovieSetClearArtMSAA.Checked = True
            chkMovieSetClearLogoMSAA.Checked = True
            chkMovieSetFanartMSAA.Checked = True
            chkMovieSetLandscapeMSAA.Checked = True
            chkMovieSetNFOMSAA.Checked = True
            chkMovieSetPosterMSAA.Checked = True
        End If
    End Sub

    Private Sub chkTVXBMCThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowThemeTvTunesEnable.CheckedChanged
        btnTVShowThemeTvTunesCustomPathBrowse.Enabled = chkTVShowThemeTvTunesEnable.Checked
        txtTVShowThemeTvTunesCustomPath.Enabled = chkTVShowThemeTvTunesEnable.Checked
    End Sub

    Private Sub btnTVXBMCThemeCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowThemeTvTunesCustomPathBrowse.Click
        With fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1028, "Select the folder where you wish to store your tv themes...")
            If .ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    txtTVShowThemeTvTunesCustomPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieXBMCThemeCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieXBMCThemeCustomPathBrowse.Click
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseBoxee.CheckedChanged
        chkTVEpisodePosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVSeasonPosterBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowBannerBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowFanartBoxee.Enabled = chkTVUseBoxee.Checked
        chkTVShowPosterBoxee.Enabled = chkTVUseBoxee.Checked

        If Not chkTVUseBoxee.Checked Then
            chkTVEpisodePosterBoxee.Checked = False
            chkTVSeasonPosterBoxee.Checked = False
            chkTVShowBannerBoxee.Checked = False
            chkTVShowFanartBoxee.Checked = False
            chkTVShowPosterBoxee.Checked = False
        Else
            chkTVEpisodePosterBoxee.Checked = True
            chkTVSeasonPosterBoxee.Checked = True
            chkTVShowBannerBoxee.Checked = True
            chkTVShowFanartBoxee.Checked = True
            chkTVShowPosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged
        chkTVEpisodeActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVEpisodePosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonLandscapeAD.Enabled = chkTVUseFrodo.Checked
        chkTVSeasonPosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowActorThumbsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowBannerFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowCharacterArtAD.Enabled = chkTVUseFrodo.Checked
        chkTVShowClearArtAD.Enabled = chkTVUseFrodo.Checked
        chkTVShowClearLogoAD.Enabled = chkTVUseFrodo.Checked
        chkTVShowExtrafanartsFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowFanartFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowLandscapeAD.Enabled = chkTVUseFrodo.Checked
        chkTVShowPosterFrodo.Enabled = chkTVUseFrodo.Checked
        chkTVShowThemeTvTunesEnable.Enabled = chkTVUseFrodo.Checked

        If Not chkTVUseFrodo.Checked Then
            chkTVEpisodeActorThumbsFrodo.Checked = False
            chkTVEpisodePosterFrodo.Checked = False
            chkTVSeasonBannerFrodo.Checked = False
            chkTVSeasonFanartFrodo.Checked = False
            chkTVSeasonLandscapeAD.Checked = False
            chkTVSeasonPosterFrodo.Checked = False
            chkTVShowActorThumbsFrodo.Checked = False
            chkTVShowBannerFrodo.Checked = False
            chkTVShowCharacterArtAD.Checked = False
            chkTVShowClearArtAD.Checked = False
            chkTVShowClearLogoAD.Checked = False
            chkTVShowExtrafanartsFrodo.Checked = False
            chkTVShowFanartFrodo.Checked = False
            chkTVShowLandscapeAD.Checked = False
            chkTVShowPosterFrodo.Checked = False
            chkTVShowThemeTvTunesEnable.Checked = False
        Else
            chkTVEpisodeActorThumbsFrodo.Checked = True
            chkTVEpisodePosterFrodo.Checked = True
            chkTVSeasonBannerFrodo.Checked = True
            chkTVSeasonFanartFrodo.Checked = True
            chkTVSeasonLandscapeAD.Checked = True
            chkTVSeasonPosterFrodo.Checked = True
            chkTVShowActorThumbsFrodo.Checked = True
            chkTVShowBannerFrodo.Checked = True
            chkTVShowCharacterArtAD.Checked = True
            chkTVShowClearArtAD.Checked = True
            chkTVShowClearLogoAD.Checked = True
            chkTVShowExtrafanartsFrodo.Checked = True
            chkTVShowFanartFrodo.Checked = True
            chkTVShowLandscapeAD.Checked = True
            chkTVShowPosterFrodo.Checked = True
        End If
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        chkTVEpisodePosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVSeasonPosterYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowBannerYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowFanartYAMJ.Enabled = chkTVUseYAMJ.Checked
        chkTVShowPosterYAMJ.Enabled = chkTVUseYAMJ.Checked

        If Not chkTVUseYAMJ.Checked Then
            chkTVEpisodePosterYAMJ.Checked = False
            chkTVSeasonBannerYAMJ.Checked = False
            chkTVSeasonFanartYAMJ.Checked = False
            chkTVSeasonPosterYAMJ.Checked = False
            chkTVShowBannerYAMJ.Checked = False
            chkTVShowFanartYAMJ.Checked = False
            chkTVShowPosterYAMJ.Checked = False
        Else
            chkTVEpisodePosterYAMJ.Checked = True
            chkTVSeasonBannerYAMJ.Checked = True
            chkTVSeasonFanartYAMJ.Checked = True
            chkTVSeasonPosterYAMJ.Checked = True
            chkTVShowBannerYAMJ.Checked = True
            chkTVShowFanartYAMJ.Checked = True
            chkTVShowPosterYAMJ.Checked = True
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub cbIntLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIntLang.SelectedIndexChanged
        If Not cbIntLang.SelectedIndex = -1 AndAlso Not String.IsNullOrEmpty(cbIntLang.SelectedItem.ToString) AndAlso Not (cbIntLang.SelectedItem.ToString = tLang) Then
            Master.eLang.LoadAllLanguage(cbIntLang.SelectedItem.ToString, True)
            tLang = cbIntLang.SelectedItem.ToString
            SetUp()
        End If
    End Sub

    Private Sub dlgWizard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUp()
        LoadIntLangs()
        FillSettings()
    End Sub

    Private Sub dlgWizard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Activate()
    End Sub

    Private Sub FillSettings()
        RefreshSources()
        RefreshTVSources()

        With Master.eSettings

            cbTVGeneralLang.Items.Clear()
            cbTVGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages.Language Select lLang.name).ToArray)
            If cbTVGeneralLang.Items.Count > 0 Then
                cbTVGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .TVGeneralLanguage).name
            End If



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
            chkMovieBannerAD.Checked = .MovieBannerAD
            chkMovieClearArtAD.Checked = .MovieClearArtAD
            chkMovieClearLogoAD.Checked = .MovieClearLogoAD
            chkMovieDiscArtAD.Checked = .MovieDiscArtAD
            chkMovieLandscapeAD.Checked = .MovieLandscapeAD

            '************** XBMC TvTunes settings **************
            chkMovieThemeTvTunesEnable.Checked = .MovieThemeTvTunesEnable
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
            chkMovieYAMJCompatibleSets.Checked = .MovieYAMJCompatibleSets
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
            txtMovieActorThumbsExtExpertSingle.Text = .MovieActorThumbsExtExpertSingle
            txtMovieBannerExpertSingle.Text = .MovieBannerExpertSingle
            txtMovieClearArtExpertSingle.Text = .MovieClearArtExpertSingle
            txtMovieClearLogoExpertSingle.Text = .MovieClearLogoExpertSingle
            txtMovieDiscArtExpertSingle.Text = .MovieDiscArtExpertSingle
            chkMovieExtrafanartsExpertSingle.Checked = .MovieExtrafanartsExpertSingle
            chkMovieExtrathumbsExpertSingle.Checked = .MovieExtrathumbsExpertSingle
            txtMovieFanartExpertSingle.Text = .MovieFanartExpertSingle
            txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
            txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
            txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
            chkMovieStackExpertSingle.Checked = .MovieStackExpertSingle
            txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle
            chkMovieUnstackExpertSingle.Checked = .MovieUnstackExpertSingle

            '******************* Expert Multi ******************
            chkMovieActorThumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
            txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
            txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
            txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
            txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
            txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
            txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
            txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
            txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
            txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
            chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
            txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti
            chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti

            '******************* Expert VTS *******************
            chkMovieActorThumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
            txtMovieActorThumbsExtExpertVTS.Text = .MovieActorThumbsExtExpertVTS
            txtMovieBannerExpertVTS.Text = .MovieBannerExpertVTS
            txtMovieClearArtExpertVTS.Text = .MovieClearArtExpertVTS
            txtMovieClearLogoExpertVTS.Text = .MovieClearLogoExpertVTS
            txtMovieDiscArtExpertVTS.Text = .MovieDiscArtExpertVTS
            chkMovieExtrafanartsExpertVTS.Checked = .MovieExtrafanartsExpertVTS
            chkMovieExtrathumbsExpertVTS.Checked = .MovieExtrathumbsExpertVTS
            txtMovieFanartExpertVTS.Text = .MovieFanartExpertVTS
            txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
            txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
            txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
            chkMovieRecognizeVTSExpertVTS.Checked = .MovieRecognizeVTSExpertVTS
            txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS
            chkMovieUseBaseDirectoryExpertVTS.Checked = .MovieUseBaseDirectoryExpertVTS

            '******************* Expert BDMV *******************
            chkMovieActorThumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
            txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
            txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
            txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
            txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
            txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
            chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
            chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
            txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
            txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
            txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
            txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
            txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV
            chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            txtMovieSetPathMSAA.Text = .MovieSetPathMSAA
            chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA

            '***************** Expert settings *****************
            chkMovieSetUseExpert.Checked = .MovieSetUseExpert

            '***************** Expert Single ******************
            txtMovieSetBannerExpertSingle.Text = .MovieSetBannerExpertSingle
            txtMovieSetClearArtExpertSingle.Text = .MovieSetClearArtExpertSingle
            txtMovieSetClearLogoExpertSingle.Text = .MovieSetClearLogoExpertSingle
            txtMovieSetFanartExpertSingle.Text = .MovieSetFanartExpertSingle
            txtMovieSetLandscapeExpertSingle.Text = .MovieSetLandscapeExpertSingle
            txtMovieSetNFOExpertSingle.Text = .MovieSetNFOExpertSingle
            txtMovieSetPathExpertSingle.Text = .MovieSetPathExpertSingle
            txtMovieSetPosterExpertSingle.Text = .MovieSetPosterExpertSingle

            '***************** Expert Parent ******************
            txtMovieSetBannerExpertParent.Text = .MovieSetBannerExpertParent
            txtMovieSetClearArtExpertParent.Text = .MovieSetClearArtExpertParent
            txtMovieSetClearLogoExpertParent.Text = .MovieSetClearLogoExpertParent
            txtMovieSetFanartExpertParent.Text = .MovieSetFanartExpertParent
            txtMovieSetLandscapeExpertParent.Text = .MovieSetLandscapeExpertParent
            txtMovieSetNFOExpertParent.Text = .MovieSetNFOExpertParent
            txtMovieSetPosterExpertParent.Text = .MovieSetPosterExpertParent


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            chkTVUseFrodo.Checked = .TVUseFrodo
            chkTVEpisodeActorThumbsFrodo.Checked = .TVEpisodeActorThumbsFrodo
            chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
            chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
            chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
            chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
            chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
            chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
            chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '************** XBMC TvTunes settings **************
            chkTVShowThemeTvTunesEnable.Checked = .TVShowThemeTvTunesEnable
            chkTVShowThemeTvTunesCustom.Checked = .TVShowThemeTvTunesCustom
            chkTVShowThemeTvTunesShowPath.Checked = .TVShowThemeTvTunesShowPath
            chkTVShowThemeTvTunesSub.Checked = .TVShowThemeTvTunesSub
            txtTVShowThemeTvTunesCustomPath.Text = .TVShowThemeTvTunesCustomPath
            txtTVShowThemeTvTunesSubDir.Text = .TVShowThemeTvTunesSubDir

            '****************** YAMJ settings ******************
            chkTVUseYAMJ.Checked = .TVUseYAMJ
            chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
            chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
            chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
            chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
            chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
            chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
            chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            chkTVUseBoxee.Checked = .TVUseBoxee
            chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
            chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
            chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
            chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
            chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

            '***************** Expert settings *****************

        End With

    End Sub

    Private Sub LoadIntLangs()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToArray)
            cbIntLang.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub lvMovies_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMovies.DoubleClick
        If lvMovies.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgSourceMovie
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovies.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshSources()
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvMovies.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveSource()
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSources.DoubleClick
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgSourceTVShow
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = DialogResult.OK Then
                    RefreshTVSources()
                End If
            End Using
        End If
    End Sub

    Private Sub lvTVSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTVSources.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveTVSource()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SaveSettings()
        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub RefreshSources()
        Dim lvItem As ListViewItem

        lvMovies.Items.Clear()
        Master.DB.Load_Sources_Movie()
        For Each s As Database.DBSource In Master.MovieSources
            lvItem = New ListViewItem(CStr(s.ID))
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            tmppath = s.Path
            lvItem.SubItems.Add(If(s.Recursive, "Yes", "No"))
            lvItem.SubItems.Add(If(s.UseFolderName, "Yes", "No"))
            lvItem.SubItems.Add(If(s.IsSingle, "Yes", "No"))
            lvItem.SubItems.Add(If(s.Exclude, "Yes", "No"))
            lvItem.SubItems.Add(If(s.GetYear, "Yes", "No"))
            lvMovies.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.Load_Sources_TVShow()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT * FROM tvshowsource;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lvItem = New ListViewItem(SQLreader("idSource").ToString)
                    lvItem.SubItems.Add(SQLreader("strName").ToString)
                    lvItem.SubItems.Add(SQLreader("strPath").ToString)
                    lvItem.SubItems.Add(Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = SQLreader("strLanguage").ToString).name)
                    lvItem.SubItems.Add(DirectCast(Convert.ToInt32(SQLreader("iOrdering")), Enums.Ordering).ToString)
                    lvItem.SubItems.Add(If(Convert.ToBoolean(SQLreader("bExclude")), Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))
                    lvItem.SubItems.Add(DirectCast(Convert.ToInt32(SQLreader("iEpisodeSorting")), Enums.EpisodeSorting).ToString)
                    tmppath = SQLreader("strPath").ToString
                    lvTVSources.Items.Add(lvItem)
                End While
            End Using
        End Using
    End Sub

    Private Sub RemoveSource()
        If lvMovies.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvMovies.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.UInt64, 0, "idSource")
                        While lvMovies.SelectedItems.Count > 0
                            parSource.Value = lvMovies.SelectedItems(0).SubItems(0).Text
                            SQLcommand.CommandText = String.Concat("DELETE FROM moviesource WHERE idSource = (?);")
                            SQLcommand.ExecuteNonQuery()
                            lvMovies.Items.Remove(lvMovies.SelectedItems(0))
                        End While
                    End Using
                    SQLtransaction.Commit()
                End Using

                lvMovies.Sort()
                lvMovies.EndUpdate()
                lvMovies.Refresh()
            End If
        End If
    End Sub

    Private Sub RemoveTVSource()
        If lvTVSources.SelectedItems.Count > 0 Then
            If MessageBox.Show(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lvTVSources.BeginUpdate()

                Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                    Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.UInt64, 0, "idSource")
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
            End If
        End If
    End Sub

    Private Sub SaveSettings()

        With Master.eSettings

            .GeneralLanguage = tLang

            If Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                Dim tvLang As String = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = cbTVGeneralLang.Text).abbreviation
                If Not String.IsNullOrEmpty(tvLang) Then
                    .TVGeneralLanguage = tvLang
                Else
                    .TVGeneralLanguage = "en"
                End If
            Else
                .TVGeneralLanguage = "en"
            End If

            'Workaround for tvdb scraper language (TODO: proper solution)
            Using settings = New clsAdvancedSettings()
                settings.SetSetting("TVDBLanguage", .TVGeneralLanguage, "scraper.TVDB")
            End Using

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
            .MovieBannerAD = chkMovieBannerAD.Checked
            .MovieClearArtAD = chkMovieClearArtAD.Checked
            .MovieClearLogoAD = chkMovieClearLogoAD.Checked
            .MovieDiscArtAD = chkMovieDiscArtAD.Checked
            .MovieLandscapeAD = chkMovieLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .MovieThemeTvTunesEnable = chkMovieThemeTvTunesEnable.Checked
            .MovieThemeTvTunesCustom = chkMovieThemeTvTunesCustom.Checked
            .MovieThemeTvTunesCustomPath = txtMovieThemeTvTunesCustomPath.Text
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
            .MovieYAMJCompatibleSets = chkMovieYAMJCompatibleSets.Checked
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

            '***************** Expert settings ****************
            .MovieSetUseExpert = chkMovieSetUseExpert.Checked

            '***************** Expert Single ******************
            .MovieSetBannerExpertSingle = txtMovieSetBannerExpertSingle.Text
            .MovieSetClearArtExpertSingle = txtMovieSetClearArtExpertSingle.Text
            .MovieSetClearLogoExpertSingle = txtMovieSetClearLogoExpertSingle.Text
            .MovieSetFanartExpertSingle = txtMovieSetFanartExpertSingle.Text
            .MovieSetLandscapeExpertSingle = txtMovieSetLandscapeExpertSingle.Text
            .MovieSetNFOExpertSingle = txtMovieSetNFOExpertSingle.Text
            .MovieSetPathExpertSingle = txtMovieSetPathExpertSingle.Text
            .MovieSetPosterExpertSingle = txtMovieSetPosterExpertSingle.Text

            '***************** Expert Parent ******************
            .MovieSetBannerExpertParent = txtMovieSetBannerExpertParent.Text
            .MovieSetClearArtExpertParent = txtMovieSetClearArtExpertParent.Text
            .MovieSetClearLogoExpertParent = txtMovieSetClearLogoExpertParent.Text
            .MovieSetFanartExpertParent = txtMovieSetFanartExpertParent.Text
            .MovieSetLandscapeExpertParent = txtMovieSetLandscapeExpertParent.Text
            .MovieSetNFOExpertParent = txtMovieSetNFOExpertParent.Text
            .MovieSetPosterExpertParent = txtMovieSetPosterExpertParent.Text


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            .TVUseFrodo = chkTVUseFrodo.Checked
            .TVEpisodeActorThumbsFrodo = chkTVEpisodeActorThumbsFrodo.Checked
            .TVEpisodePosterFrodo = chkTVEpisodePosterFrodo.Checked
            .TVSeasonBannerFrodo = chkTVSeasonBannerFrodo.Checked
            .TVSeasonFanartFrodo = chkTVSeasonFanartFrodo.Checked
            .TVSeasonPosterFrodo = chkTVSeasonPosterFrodo.Checked
            .TVShowActorThumbsFrodo = chkTVShowActorThumbsFrodo.Checked
            .TVShowBannerFrodo = chkTVShowBannerFrodo.Checked
            .TVShowFanartFrodo = chkTVShowFanartFrodo.Checked
            .TVShowPosterFrodo = chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC optional settings **************
            .TVSeasonLandscapeAD = chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = chkTVShowClearLogoAD.Checked
            .TVShowExtrafanartsFrodo = chkTVShowExtrafanartsFrodo.Checked
            .TVShowLandscapeAD = chkTVShowLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .TVShowThemeTvTunesEnable = chkTVShowThemeTvTunesEnable.Checked
            .TVShowThemeTvTunesCustom = chkTVShowThemeTvTunesCustom.Checked
            .TVShowThemeTvTunesCustomPath = txtTVShowThemeTvTunesCustomPath.Text
            .TVShowThemeTvTunesShowPath = chkTVShowThemeTvTunesShowPath.Checked
            .TVShowThemeTvTunesSub = chkTVShowThemeTvTunesSub.Checked
            .TVShowThemeTvTunesSubDir = txtTVShowThemeTvTunesSubDir.Text

            '****************** YAMJ settings ******************
            .TVUseYAMJ = chkTVUseYAMJ.Checked
            .TVEpisodePosterYAMJ = chkTVEpisodePosterYAMJ.Checked
            .TVSeasonBannerYAMJ = chkTVSeasonBannerYAMJ.Checked
            .TVSeasonFanartYAMJ = chkTVSeasonFanartYAMJ.Checked
            .TVSeasonPosterYAMJ = chkTVSeasonPosterYAMJ.Checked
            .TVShowBannerYAMJ = chkTVShowBannerYAMJ.Checked
            .TVShowFanartYAMJ = chkTVShowFanartYAMJ.Checked
            .TVShowPosterYAMJ = chkTVShowPosterYAMJ.Checked

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            .TVUseBoxee = chkTVUseBoxee.Checked
            .TVEpisodePosterBoxee = chkTVEpisodePosterBoxee.Checked
            .TVSeasonPosterBoxee = chkTVSeasonPosterBoxee.Checked
            .TVShowBannerBoxee = chkTVShowBannerBoxee.Checked
            .TVShowFanartBoxee = chkTVShowFanartBoxee.Checked
            .TVShowPosterBoxee = chkTVShowPosterBoxee.Checked

            '***************** Expert settings *****************

        End With

    End Sub

    Private Sub SetUp()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkMovieActorThumbsExpertBDMV.Text = strActorThumbs
        chkMovieActorThumbsExpertMulti.Text = strActorThumbs
        chkMovieActorThumbsExpertSingle.Text = strActorThumbs
        chkMovieActorThumbsExpertVTS.Text = strActorThumbs
        lblMovieSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs
        lblTVSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblMovieSourcesFileNamingExpertBDMVBanner.Text = strBanner
        lblMovieSourcesFileNamingExpertMultiBanner.Text = strBanner
        lblMovieSourcesFileNamingExpertSingleBanner.Text = strBanner
        lblMovieSourcesFileNamingExpertVTSBanner.Text = strBanner
        lblMovieSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        lblMovieSourcesFileNamingXBMCADBanner.Text = strBanner
        lblTVSourcesFileNamingBoxeeDefaultsBanner.Text = strBanner
        lblTVSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        lblTVSourcesFileNamingXBMCDefaultsBanner.Text = strBanner

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        lblMovieClearArtExpertBDMV.Text = strClearArt
        lblMovieClearArtExpertMulti.Text = strClearArt
        lblMovieClearArtExpertSingle.Text = strClearArt
        lblMovieClearArtExpertVTS.Text = strClearArt
        lblMovieSourcesFileNamingXBMCADClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieClearLogoExpertBDMV.Text = strClearLogo
        lblMovieClearLogoExpertMulti.Text = strClearLogo
        lblMovieClearLogoExpertSingle.Text = strClearLogo
        lblMovieClearLogoExpertVTS.Text = strClearLogo
        lblMovieSourcesFileNamingXBMCADClearLogo.Text = strClearLogo

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        gbMovieSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        gbMovieSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        gbMovieSourcesFileNamingXBMCDefaultsOpts.Text = strDefaults

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        lblMovieDiscArtExpertBDMV.Text = strDiscArt
        lblMovieDiscArtExpertMulti.Text = strDiscArt
        lblMovieDiscArtExpertSingle.Text = strDiscArt
        lblMovieDiscArtExpertVTS.Text = strDiscArt
        lblMovieSourcesFileNamingXBMCADDiscArt.Text = strDiscArt

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        lblMovieSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        lblMovieSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        chkMovieUseExpert.Text = strEnabled

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        lblMovieSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts
        lblTVSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        lblMovieSourcesFileNamingXBMCDefaultsExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblMovieSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        lblMovieSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        lblMovieSourcesFileNamingXBMCDefaultsFanart.Text = strFanart
        lblTVSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        lblTVSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        lblTVSourcesFileNamingXBMCDefaultsFanart.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblMovieSourcesFileNamingExpertBDMVLandscape.Text = strLandscape
        lblMovieSourcesFileNamingExpertMultiLandscape.Text = strLandscape
        lblMovieSourcesFileNamingExpertSingleLandscape.Text = strLandscape
        lblMovieSourcesFileNamingExpertVTSLandscape.Text = strLandscape
        lblMovieSourcesFileNamingXBMCADLandscape.Text = strLandscape

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        lblMovieSourcesFileNamingBoxeeDefaultsNFO.Text = strNFO
        lblMovieSourcesFileNamingExpertBDMVNFO.Text = strNFO
        lblMovieSourcesFileNamingExpertMultiNFO.Text = strNFO
        lblMovieSourcesFileNamingExpertSingleNFO.Text = strNFO
        lblMovieSourcesFileNamingExpertVTSNFO.Text = strNFO
        lblMovieSourcesFileNamingNMTDefaultsNFO.Text = strNFO
        lblMovieSourcesFileNamingXBMCDefaultsNFO.Text = strNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        gbMovieSourcesFileNamingExpertBDMVImagesOpts.Text = strOptionalImages
        gbMovieSourcesFileNamingExpertMultiImagesOpts.Text = strOptionalImages
        gbMovieSourcesFileNamingExpertSingleImagesOpts.Text = strOptionalImages
        gbMovieSourcesFileNamingExpertVTSImagesOpts.Text = strOptionalImages

        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        gbMovieSourcesFileNamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFileNamingExpertMultiOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFileNamingExpertSingleOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFileNamingExpertVTSOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFileNamingNMTOptionalOpts.Text = strOptionalSettings
        gbMovieSourcesFileNamingXBMCOptionalOpts.Text = strOptionalSettings

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblMoviePosterExpertBDMV.Text = strPoster
        lblMoviePosterExpertMulti.Text = strPoster
        lblMoviePosterExpertSingle.Text = strPoster
        lblMoviePosterExpertVTS.Text = strPoster
        lblTVSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        lblTVSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        lblTVSourcesFileNamingXBMCDefaultsPoster.Text = strPoster
        lblMovieSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        lblMovieSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        lblMovieSourcesFileNamingXBMCDefaultsPoster.Text = strPoster

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        lblMovieTrailerExpertBDMV.Text = strTrailer
        lblMovieTrailerExpertMulti.Text = strTrailer
        lblMovieTrailerExpertSingle.Text = strTrailer
        lblMovieTrailerExpertVTS.Text = strTrailer
        lblMovieSourcesFileNamingNMTDefaultsTrailer.Text = strTrailer
        lblMovieSourcesFileNamingXBMCDefaultsTrailer.Text = strTrailer


        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Label1.Text = Master.eLang.GetString(405, "Welcome to Ember Media Manager")
        Label10.Text = Master.eLang.GetString(113, "Now select the default language you would like Ember to look for when scraping TV Show items.")
        Label11.Text = Master.eLang.GetString(804, "And finally, let's tell Ember Media Manager what TV Show files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Label2.Text = String.Format(Master.eLang.GetString(415, "This is either your first time running Ember Media Manager or you have upgraded to a newer version.  There are a few things Ember Media Manager needs to know to work properly.  This wizard will walk you through configuring Ember Media Manager to work for your set up.{0}{0}Only a handful of settings will be covered in this wizard. You can change these or any other setting at any time by selecting ""Settings..."" from the ""Edit"" menu."), Environment.NewLine)
        Label3.Text = Master.eLang.GetString(414, "First, let's tell Ember Media Manager where to locate all your movies. You can add as many sources as you wish.")
        Label32.Text = Master.eLang.GetString(430, "Interface Language")
        lblMovieSettings.Text = Master.eLang.GetString(416, "Now that Ember Media Manager knows WHERE to look for the files, we need to tell it WHAT files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Label48.Text = Master.eLang.GetString(416, "Now that Ember Media Manager knows WHERE to look for the files, we need to tell it WHAT files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Label6.Text = Master.eLang.GetString(408, "That wasn't so hard was it?  As mentioned earlier, you can change these or any other options in the Settings dialog.")
        Label7.Text = String.Format(Master.eLang.GetString(409, "That's it!{0}Ember Media Manager is Ready!"), Environment.NewLine)
        Label8.Text = String.Format(Master.eLang.GetString(417, "Some options you may be interested in:{0}{0}Custom Filters - If your movie files have things like ""DVDRip"", ""BluRay"", ""x264"", etc in their folder or file name and you wish to filter the names when loading into the media list, you can utilize the Custom Filter option.  The custom filter is RegEx compatible for maximum usability.{0}{0}Images - This section allows you to select which websites to ""scrape"" images from as well as select a preferred size for the images Ember Media Manager selects.{0}{0}Locks - This section allows you to ""lock"" certain information so it does not get updated even if you re-scrape the movie. This is useful if you manually edit the title, outline, or plot of a movie and wish to keep your changes."), Environment.NewLine)
        Label9.Text = Master.eLang.GetString(803, "Next, let's tell Ember Media Manager where to locate all your TV Shows. You can add as many sources as you wish.")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Text = Master.eLang.GetString(402, "Ember Startup Wizard")
        btnBack.Text = Master.eLang.GetString(403, "< Back")
        btnMovieAddFolder.Text = Master.eLang.GetString(407, "Add Source")
        btnMovieRem.Text = Master.eLang.GetString(30, "Remove")
        btnNext.Text = Master.eLang.GetString(404, "Next >")
        btnTVAddSource.Text = btnMovieAddFolder.Text
        btnTVGeneralLangFetch.Text = Master.eLang.GetString(742, "Fetch Available Languages")
        btnTVRemoveSource.Text = btnMovieRem.Text
        colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        colName.Text = Master.eLang.GetString(232, "Name")
        colPath.Text = Master.eLang.GetString(410, "Path")
        colRecur.Text = Master.eLang.GetString(411, "Recursive")
        colSingle.Text = Master.eLang.GetString(413, "Single Video")
        colExclude.Text = Master.eLang.GetString(264, "Exclude")
        colGetYear.Text = Master.eLang.GetString(586, "Get Year")
        lvTVSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        lvTVSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        lvTVSources.Columns(3).Text = Master.eLang.GetString(610, "Language")
        lvTVSources.Columns(4).Text = Master.eLang.GetString(1167, "Ordering")
        lvTVSources.Columns(5).Text = Master.eLang.GetString(264, "Exclude")
        lvTVSources.Columns(6).Text = "Sorting"
        pnlWelcome.Location = New Point(166, 7)
        pnlWelcome.Visible = True
        pnlMovieSources.Visible = False
        pnlMovieSettings.Visible = False
        pnlMovieSetSettings.Visible = False
        pnlTVShowSource.Visible = False
        pnlTVShowSettings.Visible = False
        pnlDone.Visible = False
        pnlMovieSources.Location = New Point(166, 7)
        pnlMovieSettings.Location = New Point(166, 7)
        pnlMovieSetSettings.Location = New Point(166, 7)
        pnlTVShowSource.Location = New Point(166, 7)
        pnlTVShowSettings.Location = New Point(166, 7)
        pnlDone.Location = New Point(166, 7)
    End Sub

#End Region 'Methods

End Class