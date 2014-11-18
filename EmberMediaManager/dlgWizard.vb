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
Imports System.Xml.Serialization

Public Class dlgWizard

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private tLang As String
    Private tmppath As String = String.Empty

#End Region

#Region "Methods"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Select Case True
            Case Me.pnlMovieSources.Visible
                Me.btnBack.Enabled = False
                Me.pnlMovieSources.Visible = False
                Me.pnlWelcome.Visible = True
            Case Me.pnlMovieSettings.Visible
                Me.pnlMovieSettings.Visible = False
                Me.pnlMovieSources.Visible = True
            Case Me.pnlMovieSetSettings.Visible
                Me.pnlMovieSetSettings.Visible = False
                Me.pnlMovieSettings.Visible = True
            Case Me.pnlTVShowSource.Visible
                Me.pnlTVShowSource.Visible = False
                Me.pnlMovieSetSettings.Visible = True
            Case Me.pnlTVShowSettings.Visible
                Me.pnlTVShowSettings.Visible = False
                Me.pnlTVShowSource.Visible = True
            Case Me.pnlDone.Visible
                Me.pnlDone.Visible = False
                Me.pnlTVShowSettings.Visible = True
                Me.btnNext.Enabled = True
                Me.OK_Button.Enabled = False
        End Select
    End Sub

    Private Sub btnTVGeneralLangFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVGeneralLangFetch.Click
        Master.eSettings.TVGeneralLanguages = ModulesManager.Instance.TVGetLangs("thetvdb.com")
        Me.cbTVGeneralLang.Items.Clear()
        Me.cbTVGeneralLang.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)

        If Me.cbTVGeneralLang.Items.Count > 0 Then
            Me.cbTVGeneralLang.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVGeneralLanguage).name
        End If
    End Sub

    Private Sub btnMovieAddFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieAddFolder.Click
        Using dSource As New dlgMovieSource()
            If dSource.ShowDialog(tmppath) = Windows.Forms.DialogResult.OK Then
                RefreshSources()
            End If
        End Using
    End Sub

    Private Sub btnMovieRem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieRem.Click
        Me.RemoveSource()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Select Case True
            Case Me.pnlWelcome.Visible
                Me.btnBack.Enabled = True
                Me.pnlWelcome.Visible = False
                Me.pnlMovieSources.Visible = True
            Case Me.pnlMovieSources.Visible
                Me.pnlMovieSources.Visible = False
                Me.pnlMovieSettings.Visible = True
            Case Me.pnlMovieSettings.Visible
                Me.pnlMovieSettings.Visible = False
                Me.pnlMovieSetSettings.Visible = True
            Case Me.pnlMovieSetSettings.Visible
                Me.pnlMovieSetSettings.Visible = False
                Me.pnlTVShowSource.Visible = True
            Case Me.pnlTVShowSource.Visible
                Me.pnlTVShowSource.Visible = False
                Me.pnlTVShowSettings.Visible = True
            Case Me.pnlTVShowSettings.Visible
                Me.pnlTVShowSettings.Visible = False
                Me.pnlDone.Visible = True
                Me.btnNext.Enabled = False
                Me.OK_Button.Enabled = True
        End Select
    End Sub

    Private Sub btnTVAddSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVAddSource.Click
        Using dSource As New dlgTVSource
            If dSource.ShowDialog(tmppath) = Windows.Forms.DialogResult.OK Then
                RefreshTVSources()
            End If
        End Using
    End Sub

    Private Sub btnTVRemoveSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVRemoveSource.Click
        Me.RemoveTVSource()
    End Sub

    Private Sub btnMovieSetPathMSAABrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathMSAABrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieSetPathMSAA.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieYAMJWatchedFilesBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieYAMJWatchedFilesBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1029, "Select the folder where you wish to store your watched files...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieYAMJWatchedFolder.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    Private Sub cbTVGeneralLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTVGeneralLang.SelectedIndexChanged
        If Not String.IsNullOrEmpty(Me.cbTVGeneralLang.Text) Then
            Master.eSettings.TVGeneralLanguage = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = Me.cbTVGeneralLang.Text).abbreviation
        End If
    End Sub

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBoxee.CheckedChanged

        Me.chkMovieFanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieNFOBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMoviePosterBoxee.Enabled = Me.chkMovieUseBoxee.Checked

        If Not Me.chkMovieUseBoxee.Checked Then
            Me.chkMovieFanartBoxee.Checked = False
            Me.chkMovieNFOBoxee.Checked = False
            Me.chkMoviePosterBoxee.Checked = False
        Else
            Me.chkMovieFanartBoxee.Checked = True
            Me.chkMovieNFOBoxee.Checked = True
            Me.chkMoviePosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseFrodo.CheckedChanged

        Me.chkMovieActorThumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieBannerAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrafanartsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrathumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieFanartFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeAD.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieNFOFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMoviePosterFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieTrailerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseFrodo.Checked OrElse Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked

        If Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieActorThumbsFrodo.Checked = False
            Me.chkMovieBannerAD.Checked = False
            Me.chkMovieClearArtAD.Checked = False
            Me.chkMovieClearLogoAD.Checked = False
            Me.chkMovieDiscArtAD.Checked = False
            Me.chkMovieExtrafanartsFrodo.Checked = False
            Me.chkMovieExtrathumbsFrodo.Checked = False
            Me.chkMovieFanartFrodo.Checked = False
            Me.chkMovieLandscapeAD.Checked = False
            Me.chkMovieNFOFrodo.Checked = False
            Me.chkMoviePosterFrodo.Checked = False
            Me.chkMovieTrailerFrodo.Checked = False
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            Me.chkMovieActorThumbsFrodo.Checked = True
            Me.chkMovieBannerAD.Checked = True
            Me.chkMovieClearArtAD.Checked = True
            Me.chkMovieClearLogoAD.Checked = True
            Me.chkMovieDiscArtAD.Checked = True
            Me.chkMovieExtrafanartsFrodo.Checked = True
            Me.chkMovieExtrathumbsFrodo.Checked = True
            Me.chkMovieFanartFrodo.Checked = True
            Me.chkMovieLandscapeAD.Checked = True
            Me.chkMovieNFOFrodo.Checked = True
            Me.chkMoviePosterFrodo.Checked = True
            Me.chkMovieTrailerFrodo.Checked = True
        End If

        If Not Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieXBMCThemeEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseEden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseEden.CheckedChanged

        Me.chkMovieActorThumbsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrafanartsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrathumbsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieFanartEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieNFOEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMoviePosterEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieTrailerEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseEden.Checked OrElse Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Not Me.chkMovieUseEden.Checked AndAlso Me.chkMovieUseFrodo.Checked

        If Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieActorThumbsEden.Checked = False
            Me.chkMovieExtrafanartsEden.Checked = False
            Me.chkMovieExtrathumbsEden.Checked = False
            Me.chkMovieFanartEden.Checked = False
            Me.chkMovieNFOEden.Checked = False
            Me.chkMoviePosterEden.Checked = False
            Me.chkMovieTrailerEden.Checked = False
        Else
            Me.chkMovieActorThumbsEden.Checked = True
            Me.chkMovieExtrafanartsEden.Checked = True
            Me.chkMovieExtrathumbsEden.Checked = True
            Me.chkMovieFanartEden.Checked = True
            Me.chkMovieNFOEden.Checked = True
            Me.chkMoviePosterEden.Checked = True
            Me.chkMovieTrailerEden.Checked = True
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        End If

        If Not Me.chkMovieUseEden.Checked AndAlso Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieXBMCThemeEnable.Checked = False
        End If
    End Sub

    Private Sub chkMovieUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseYAMJ.CheckedChanged

        Me.chkMovieBannerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieFanartYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieNFOYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMoviePosterYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieTrailerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieYAMJWatchedFile.Enabled = Me.chkMovieUseYAMJ.Checked

        If Not Me.chkMovieUseYAMJ.Checked Then
            Me.chkMovieBannerYAMJ.Checked = False
            Me.chkMovieFanartYAMJ.Checked = False
            Me.chkMovieNFOYAMJ.Checked = False
            Me.chkMoviePosterYAMJ.Checked = False
            Me.chkMovieTrailerYAMJ.Checked = False
            Me.chkMovieYAMJWatchedFile.Checked = False
        Else
            Me.chkMovieBannerYAMJ.Checked = True
            Me.chkMovieFanartYAMJ.Checked = True
            Me.chkMovieNFOYAMJ.Checked = True
            Me.chkMoviePosterYAMJ.Checked = True
            Me.chkMovieTrailerYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseNMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged

        Me.chkMovieBannerNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieFanartNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieNFONMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMoviePosterNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieTrailerNMJ.Enabled = Me.chkMovieUseNMJ.Checked

        If Not Me.chkMovieUseNMJ.Checked Then
            Me.chkMovieBannerNMJ.Checked = False
            Me.chkMovieFanartNMJ.Checked = False
            Me.chkMovieNFONMJ.Checked = False
            Me.chkMoviePosterNMJ.Checked = False
            Me.chkMovieTrailerNMJ.Checked = False
        Else
            Me.chkMovieBannerNMJ.Checked = True
            Me.chkMovieFanartNMJ.Checked = True
            Me.chkMovieNFONMJ.Checked = True
            Me.chkMoviePosterNMJ.Checked = True
            Me.chkMovieTrailerNMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeCustom.CheckedChanged

        Me.txtMovieXBMCThemeCustomPath.Enabled = Me.chkMovieXBMCThemeCustom.Checked
        Me.btnMovieXBMCThemeCustomPathBrowse.Enabled = Me.chkMovieXBMCThemeCustom.Checked

        If Me.chkMovieXBMCThemeCustom.Checked Then
            Me.chkMovieXBMCThemeMovie.Enabled = False
            Me.chkMovieXBMCThemeMovie.Checked = False
            Me.chkMovieXBMCThemeSub.Enabled = False
            Me.chkMovieXBMCThemeSub.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeCustom.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeMovie.Enabled = True
            Me.chkMovieXBMCThemeSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeEnable.CheckedChanged

        Me.chkMovieXBMCThemeCustom.Enabled = Me.chkMovieXBMCThemeEnable.Checked
        Me.chkMovieXBMCThemeMovie.Enabled = Me.chkMovieXBMCThemeEnable.Checked
        Me.chkMovieXBMCThemeSub.Enabled = Me.chkMovieXBMCThemeEnable.Checked

        If Not Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeMovie.Checked = False
            Me.chkMovieXBMCThemeSub.Checked = False
        Else
            Me.chkMovieXBMCThemeMovie.Checked = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeMovie.CheckedChanged

        If Me.chkMovieXBMCThemeMovie.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = False
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeSub.Enabled = False
            Me.chkMovieXBMCThemeSub.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeMovie.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = True
            Me.chkMovieXBMCThemeSub.Enabled = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeSub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeSub.CheckedChanged

        Me.txtMovieXBMCThemeSubDir.Enabled = Me.chkMovieXBMCThemeSub.Checked

        If Me.chkMovieXBMCThemeSub.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = False
            Me.chkMovieXBMCThemeCustom.Checked = False
            Me.chkMovieXBMCThemeMovie.Enabled = False
            Me.chkMovieXBMCThemeMovie.Checked = False
        End If

        If Not Me.chkMovieXBMCThemeSub.Checked AndAlso Me.chkMovieXBMCThemeEnable.Checked Then
            Me.chkMovieXBMCThemeCustom.Enabled = True
            Me.chkMovieXBMCThemeMovie.Enabled = True
        End If
    End Sub

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        Me.txtMovieYAMJWatchedFolder.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.btnMovieYAMJWatchedFilesBrowse.Enabled = Me.chkMovieYAMJWatchedFile.Checked
    End Sub

    Private Sub chkMovieStackExpertSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertSingle.CheckedChanged
        Me.chkMovieUnstackExpertSingle.Enabled = Me.chkMovieStackExpertSingle.Checked AndAlso Me.chkMovieStackExpertSingle.Enabled
    End Sub

    Private Sub chkMovieStackExpertMulti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieStackExpertMulti.CheckedChanged
        Me.chkMovieUnstackExpertMulti.Enabled = Me.chkMovieStackExpertMulti.Checked AndAlso Me.chkMovieStackExpertMulti.Enabled
    End Sub

    Private Sub chkMovieSetUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseExpert.CheckedChanged
        Me.btnMovieSetPathExpertSingleBrowse.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetBannerExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetBannerExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearArtExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearArtExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearLogoExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetClearLogoExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetFanartExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetFanartExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetLandscapeExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetLandscapeExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetNFOExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetNFOExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPathExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPosterExpertParent.Enabled = Me.chkMovieSetUseExpert.Checked
        Me.txtMovieSetPosterExpertSingle.Enabled = Me.chkMovieSetUseExpert.Checked
    End Sub

    Private Sub btnMovieSetPathExpertSingleBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieSetPathExpertSingleBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1030, "Select the folder where you wish to store your movie sets images...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieSetPathExpertSingle.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub chkMovieUseExpert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseExpert.CheckedChanged
        Me.chkMovieActorThumbsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieActorThumbsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrafanartsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieExtrathumbsExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieRecognizeVTSExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieStackExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieStackExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieUnstackExpertMulti.Enabled = Me.chkMovieStackExpertMulti.Enabled AndAlso Me.chkMovieStackExpertMulti.Checked
        Me.chkMovieUnstackExpertSingle.Enabled = Me.chkMovieStackExpertSingle.Enabled AndAlso Me.chkMovieStackExpertSingle.Checked
        Me.chkMovieUseBaseDirectoryExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.chkMovieUseBaseDirectoryExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieActorThumbsExtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieBannerExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearArtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieClearLogoExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieDiscArtExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieFanartExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieLandscapeExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieNFOExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMoviePosterExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertBDMV.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertMulti.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertSingle.Enabled = Me.chkMovieUseExpert.Checked
        Me.txtMovieTrailerExpertVTS.Enabled = Me.chkMovieUseExpert.Checked
    End Sub

    Private Sub chkMovieSetUseMSAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieSetUseMSAA.CheckedChanged
        Me.btnMovieSetPathMSAABrowse.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetBannerMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetClearArtMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetClearLogoMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetFanartMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetLandscapeMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetNFOMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.chkMovieSetPosterMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked
        Me.txtMovieSetPathMSAA.Enabled = Me.chkMovieSetUseMSAA.Checked

        If Not Me.chkMovieSetUseMSAA.Checked Then
            Me.chkMovieSetBannerMSAA.Checked = False
            Me.chkMovieSetClearArtMSAA.Checked = False
            Me.chkMovieSetClearLogoMSAA.Checked = False
            Me.chkMovieSetFanartMSAA.Checked = False
            Me.chkMovieSetLandscapeMSAA.Checked = False
            Me.chkMovieSetNFOMSAA.Checked = False
            Me.chkMovieSetPosterMSAA.Checked = False
        Else
            Me.chkMovieSetBannerMSAA.Checked = True
            Me.chkMovieSetClearArtMSAA.Checked = True
            Me.chkMovieSetClearLogoMSAA.Checked = True
            Me.chkMovieSetFanartMSAA.Checked = True
            Me.chkMovieSetLandscapeMSAA.Checked = True
            Me.chkMovieSetNFOMSAA.Checked = True
            Me.chkMovieSetPosterMSAA.Checked = True
        End If
    End Sub

    Private Sub chkTVXBMCThemeEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVXBMCThemeEnable.CheckedChanged
        Me.btnTVXBMCThemeCustomPathBrowse.Enabled = Me.chkTVXBMCThemeEnable.Checked
        Me.txtTVXBMCThemeCustomPath.Enabled = Me.chkTVXBMCThemeEnable.Checked
    End Sub

    Private Sub btnTVXBMCThemeCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnTVXBMCThemeCustomPathBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1028, "Select the folder where you wish to store your tv themes...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtTVXBMCThemeCustomPath.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub btnMovieXBMCThemeCustomPathBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieXBMCThemeCustomPathBrowse.Click
        Try
            With Me.fbdBrowse
                fbdBrowse.Description = Master.eLang.GetString(1077, "Select the folder where you wish to store your themes...")
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                        Me.txtMovieXBMCThemeCustomPath.Text = .SelectedPath.ToString
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub chkTVUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseBoxee.CheckedChanged
        Me.chkTVEpisodePosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVSeasonPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowBannerBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowFanartBoxee.Enabled = Me.chkTVUseBoxee.Checked
        Me.chkTVShowPosterBoxee.Enabled = Me.chkTVUseBoxee.Checked

        If Not Me.chkTVUseBoxee.Checked Then
            Me.chkTVEpisodePosterBoxee.Checked = False
            Me.chkTVSeasonPosterBoxee.Checked = False
            Me.chkTVShowBannerBoxee.Checked = False
            Me.chkTVShowFanartBoxee.Checked = False
            Me.chkTVShowPosterBoxee.Checked = False
        Else
            Me.chkTVEpisodePosterBoxee.Checked = True
            Me.chkTVSeasonPosterBoxee.Checked = True
            Me.chkTVShowBannerBoxee.Checked = True
            Me.chkTVShowFanartBoxee.Checked = True
            Me.chkTVShowPosterBoxee.Checked = True
        End If
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged  
        Me.chkTVEpisodeActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodePosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowExtrafanartsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeAD.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVXBMCThemeEnable.Enabled = Me.chkTVUseFrodo.Checked

        If Not Me.chkTVUseFrodo.Checked Then
            Me.chkTVEpisodeActorThumbsFrodo.Checked = False
            Me.chkTVEpisodePosterFrodo.Checked = False
            Me.chkTVSeasonBannerFrodo.Checked = False
            Me.chkTVSeasonFanartFrodo.Checked = False
            Me.chkTVSeasonLandscapeAD.Checked = False
            Me.chkTVSeasonPosterFrodo.Checked = False
            Me.chkTVShowActorThumbsFrodo.Checked = False
            Me.chkTVShowBannerFrodo.Checked = False
            Me.chkTVShowCharacterArtAD.Checked = False
            Me.chkTVShowClearArtAD.Checked = False
            Me.chkTVShowClearLogoAD.Checked = False
            Me.chkTVShowExtrafanartsFrodo.Checked = False
            Me.chkTVShowFanartFrodo.Checked = False
            Me.chkTVShowLandscapeAD.Checked = False
            Me.chkTVShowPosterFrodo.Checked = False
            Me.chkTVXBMCThemeEnable.Checked = False
        Else
            Me.chkTVEpisodeActorThumbsFrodo.Checked = True
            Me.chkTVEpisodePosterFrodo.Checked = True
            Me.chkTVSeasonBannerFrodo.Checked = True
            Me.chkTVSeasonFanartFrodo.Checked = True
            Me.chkTVSeasonLandscapeAD.Checked = True
            Me.chkTVSeasonPosterFrodo.Checked = True
            Me.chkTVShowActorThumbsFrodo.Checked = True
            Me.chkTVShowBannerFrodo.Checked = True
            Me.chkTVShowCharacterArtAD.Checked = True
            Me.chkTVShowClearArtAD.Checked = True
            Me.chkTVShowClearLogoAD.Checked = True
            Me.chkTVShowExtrafanartsFrodo.Checked = True
            Me.chkTVShowFanartFrodo.Checked = True
            Me.chkTVShowLandscapeAD.Checked = True
            Me.chkTVShowPosterFrodo.Checked = True
        End If
    End Sub

    Private Sub chkTVUseYAMJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseYAMJ.CheckedChanged
        Me.chkTVEpisodePosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVSeasonPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowBannerYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowFanartYAMJ.Enabled = Me.chkTVUseYAMJ.Checked
        Me.chkTVShowPosterYAMJ.Enabled = Me.chkTVUseYAMJ.Checked

        If Not Me.chkTVUseYAMJ.Checked Then
            Me.chkTVEpisodePosterYAMJ.Checked = False
            Me.chkTVSeasonBannerYAMJ.Checked = False
            Me.chkTVSeasonFanartYAMJ.Checked = False
            Me.chkTVSeasonPosterYAMJ.Checked = False
            Me.chkTVShowBannerYAMJ.Checked = False
            Me.chkTVShowFanartYAMJ.Checked = False
            Me.chkTVShowPosterYAMJ.Checked = False
        Else
            Me.chkTVEpisodePosterYAMJ.Checked = True
            Me.chkTVSeasonBannerYAMJ.Checked = True
            Me.chkTVSeasonFanartYAMJ.Checked = True
            Me.chkTVSeasonPosterYAMJ.Checked = True
            Me.chkTVShowBannerYAMJ.Checked = True
            Me.chkTVShowFanartYAMJ.Checked = True
            Me.chkTVShowPosterYAMJ.Checked = True
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cbIntLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIntLang.SelectedIndexChanged
        If Not String.IsNullOrEmpty(Me.cbIntLang.SelectedItem.ToString) AndAlso Not (Me.cbIntLang.SelectedItem.ToString = tLang) Then
            Master.eLang.LoadAllLanguage(Me.cbIntLang.SelectedItem.ToString, True)
            tLang = Me.cbIntLang.SelectedItem.ToString
            Me.SetUp()
        End If
    End Sub

    Private Sub dlgWizard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetUp()
        Me.LoadIntLangs()
        Me.FillSettings()
    End Sub

    Private Sub dlgWizard_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.Activate()
    End Sub

    Private Sub FillSettings()
        Me.RefreshSources()
        Me.RefreshTVSources()

        With Master.eSettings

            Me.cbTVGeneralLang.Items.Clear()
            Me.cbTVGeneralLang.Items.AddRange((From lLang In .TVGeneralLanguages.Language Select lLang.name).ToArray)
            If Me.cbTVGeneralLang.Items.Count > 0 Then
                Me.cbTVGeneralLang.Text = .TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .TVGeneralLanguage).name
            End If

            

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkMovieUseFrodo.Checked = .MovieUseFrodo
            Me.chkMovieActorThumbsFrodo.Checked = .MovieActorThumbsFrodo
            Me.chkMovieExtrafanartsFrodo.Checked = .MovieExtrafanartsFrodo
            Me.chkMovieExtrathumbsFrodo.Checked = .MovieExtrathumbsFrodo
            Me.chkMovieFanartFrodo.Checked = .MovieFanartFrodo
            Me.chkMovieNFOFrodo.Checked = .MovieNFOFrodo
            Me.chkMoviePosterFrodo.Checked = .MoviePosterFrodo
            Me.chkMovieTrailerFrodo.Checked = .MovieTrailerFrodo

            '*************** XBMC Eden settings ****************
            Me.chkMovieUseEden.Checked = .MovieUseEden
            Me.chkMovieActorThumbsEden.Checked = .MovieActorThumbsEden
            Me.chkMovieExtrafanartsEden.Checked = .MovieExtrafanartsEden
            Me.chkMovieExtrathumbsEden.Checked = .MovieExtrathumbsEden
            Me.chkMovieFanartEden.Checked = .MovieFanartEden
            Me.chkMovieNFOEden.Checked = .MovieNFOEden
            Me.chkMoviePosterEden.Checked = .MoviePosterEden
            Me.chkMovieTrailerEden.Checked = .MovieTrailerEden

            '************* XBMC optional settings **************
            Me.chkMovieXBMCProtectVTSBDMV.Checked = .MovieXBMCProtectVTSBDMV

            '******** XBMC ArtworkDownloader settings **********
            Me.chkMovieBannerAD.Checked = .MovieBannerAD
            Me.chkMovieClearArtAD.Checked = .MovieClearArtAD
            Me.chkMovieClearLogoAD.Checked = .MovieClearLogoAD
            Me.chkMovieDiscArtAD.Checked = .MovieDiscArtAD
            Me.chkMovieLandscapeAD.Checked = .MovieLandscapeAD

            '************** XBMC TvTunes settings **************
            Me.chkMovieXBMCThemeEnable.Checked = .MovieXBMCThemeEnable
            Me.chkMovieXBMCThemeCustom.Checked = .MovieXBMCThemeCustom
            Me.chkMovieXBMCThemeMovie.Checked = .MovieXBMCThemeMovie
            Me.chkMovieXBMCThemeSub.Checked = .MovieXBMCThemeSub
            Me.txtMovieXBMCThemeCustomPath.Text = .MovieXBMCThemeCustomPath
            Me.txtMovieXBMCThemeSubDir.Text = .MovieXBMCThemeSubDir

            '****************** YAMJ settings ******************
            Me.chkMovieUseYAMJ.Checked = .MovieUseYAMJ
            Me.chkMovieBannerYAMJ.Checked = .MovieBannerYAMJ
            Me.chkMovieFanartYAMJ.Checked = .MovieFanartYAMJ
            Me.chkMovieNFOYAMJ.Checked = .MovieNFOYAMJ
            Me.chkMoviePosterYAMJ.Checked = .MoviePosterYAMJ
            Me.chkMovieTrailerYAMJ.Checked = .MovieTrailerYAMJ

            '****************** NMJ settings ******************
            Me.chkMovieUseNMJ.Checked = .MovieUseNMJ
            Me.chkMovieBannerNMJ.Checked = .MovieBannerNMJ
            Me.chkMovieFanartNMJ.Checked = .MovieFanartNMJ
            Me.chkMovieNFONMJ.Checked = .MovieNFONMJ
            Me.chkMoviePosterNMJ.Checked = .MoviePosterNMJ
            Me.chkMovieTrailerNMJ.Checked = .MovieTrailerNMJ

            '************** NMT optional settings **************
            Me.chkMovieYAMJCompatibleSets.Checked = .MovieYAMJCompatibleSets
            Me.chkMovieYAMJWatchedFile.Checked = .MovieYAMJWatchedFile
            Me.txtMovieYAMJWatchedFolder.Text = .MovieYAMJWatchedFolder

            '***************** Boxee settings ******************
            Me.chkMovieUseBoxee.Checked = .MovieUseBoxee
            Me.chkMovieFanartBoxee.Checked = .MovieFanartBoxee
            Me.chkMovieNFOBoxee.Checked = .MovieNFOBoxee
            Me.chkMoviePosterBoxee.Checked = .MoviePosterBoxee

            '***************** Expert settings *****************
            Me.chkMovieUseExpert.Checked = .MovieUseExpert

            '***************** Expert Single *******************
            Me.chkMovieActorThumbsExpertSingle.Checked = .MovieActorThumbsExpertSingle
            Me.txtMovieActorThumbsExtExpertSingle.Text = .MovieActorThumbsExtExpertSingle
            Me.txtMovieBannerExpertSingle.Text = .MovieBannerExpertSingle
            Me.txtMovieClearArtExpertSingle.Text = .MovieClearArtExpertSingle
            Me.txtMovieClearLogoExpertSingle.Text = .MovieClearLogoExpertSingle
            Me.txtMovieDiscArtExpertSingle.Text = .MovieDiscArtExpertSingle
            Me.chkMovieExtrafanartsExpertSingle.Checked = .MovieExtrafanartsExpertSingle
            Me.chkMovieExtrathumbsExpertSingle.Checked = .MovieExtrathumbsExpertSingle
            Me.txtMovieFanartExpertSingle.Text = .MovieFanartExpertSingle
            Me.txtMovieLandscapeExpertSingle.Text = .MovieLandscapeExpertSingle
            Me.txtMovieNFOExpertSingle.Text = .MovieNFOExpertSingle
            Me.txtMoviePosterExpertSingle.Text = .MoviePosterExpertSingle
            Me.chkMovieStackExpertSingle.Checked = .MovieStackExpertSingle
            Me.txtMovieTrailerExpertSingle.Text = .MovieTrailerExpertSingle
            Me.chkMovieUnstackExpertSingle.Checked = .MovieUnstackExpertSingle

            '******************* Expert Multi ******************
            Me.chkMovieActorThumbsExpertMulti.Checked = .MovieActorThumbsExpertMulti
            Me.txtMovieActorThumbsExtExpertMulti.Text = .MovieActorThumbsExtExpertMulti
            Me.txtMovieBannerExpertMulti.Text = .MovieBannerExpertMulti
            Me.txtMovieClearArtExpertMulti.Text = .MovieClearArtExpertMulti
            Me.txtMovieClearLogoExpertMulti.Text = .MovieClearLogoExpertMulti
            Me.txtMovieDiscArtExpertMulti.Text = .MovieDiscArtExpertMulti
            Me.txtMovieFanartExpertMulti.Text = .MovieFanartExpertMulti
            Me.txtMovieLandscapeExpertMulti.Text = .MovieLandscapeExpertMulti
            Me.txtMovieNFOExpertMulti.Text = .MovieNFOExpertMulti
            Me.txtMoviePosterExpertMulti.Text = .MoviePosterExpertMulti
            Me.chkMovieStackExpertMulti.Checked = .MovieStackExpertMulti
            Me.txtMovieTrailerExpertMulti.Text = .MovieTrailerExpertMulti
            Me.chkMovieUnstackExpertMulti.Checked = .MovieUnstackExpertMulti

            '******************* Expert VTS *******************
            Me.chkMovieActorThumbsExpertVTS.Checked = .MovieActorThumbsExpertVTS
            Me.txtMovieActorThumbsExtExpertVTS.Text = .MovieActorThumbsExtExpertVTS
            Me.txtMovieBannerExpertVTS.Text = .MovieBannerExpertVTS
            Me.txtMovieClearArtExpertVTS.Text = .MovieClearArtExpertVTS
            Me.txtMovieClearLogoExpertVTS.Text = .MovieClearLogoExpertVTS
            Me.txtMovieDiscArtExpertVTS.Text = .MovieDiscArtExpertVTS
            Me.chkMovieExtrafanartsExpertVTS.Checked = .MovieExtrafanartsExpertVTS
            Me.chkMovieExtrathumbsExpertVTS.Checked = .MovieExtrathumbsExpertVTS
            Me.txtMovieFanartExpertVTS.Text = .MovieFanartExpertVTS
            Me.txtMovieLandscapeExpertVTS.Text = .MovieLandscapeExpertVTS
            Me.txtMovieNFOExpertVTS.Text = .MovieNFOExpertVTS
            Me.txtMoviePosterExpertVTS.Text = .MoviePosterExpertVTS
            Me.chkMovieRecognizeVTSExpertVTS.Checked = .MovieRecognizeVTSExpertVTS
            Me.txtMovieTrailerExpertVTS.Text = .MovieTrailerExpertVTS
            Me.chkMovieUseBaseDirectoryExpertVTS.Checked = .MovieUseBaseDirectoryExpertVTS

            '******************* Expert BDMV *******************
            Me.chkMovieActorThumbsExpertBDMV.Checked = .MovieActorThumbsExpertBDMV
            Me.txtMovieActorThumbsExtExpertBDMV.Text = .MovieActorThumbsExtExpertBDMV
            Me.txtMovieBannerExpertBDMV.Text = .MovieBannerExpertBDMV
            Me.txtMovieClearArtExpertBDMV.Text = .MovieClearArtExpertBDMV
            Me.txtMovieClearLogoExpertBDMV.Text = .MovieClearLogoExpertBDMV
            Me.txtMovieDiscArtExpertBDMV.Text = .MovieDiscArtExpertBDMV
            Me.chkMovieExtrafanartsExpertBDMV.Checked = .MovieExtrafanartsExpertBDMV
            Me.chkMovieExtrathumbsExpertBDMV.Checked = .MovieExtrathumbsExpertBDMV
            Me.txtMovieFanartExpertBDMV.Text = .MovieFanartExpertBDMV
            Me.txtMovieLandscapeExpertBDMV.Text = .MovieLandscapeExpertBDMV
            Me.txtMovieNFOExpertBDMV.Text = .MovieNFOExpertBDMV
            Me.txtMoviePosterExpertBDMV.Text = .MoviePosterExpertBDMV
            Me.txtMovieTrailerExpertBDMV.Text = .MovieTrailerExpertBDMV
            Me.chkMovieUseBaseDirectoryExpertBDMV.Checked = .MovieUseBaseDirectoryExpertBDMV


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            Me.chkMovieSetUseMSAA.Checked = .MovieSetUseMSAA
            Me.chkMovieSetBannerMSAA.Checked = .MovieSetBannerMSAA
            Me.chkMovieSetClearArtMSAA.Checked = .MovieSetClearArtMSAA
            Me.chkMovieSetClearLogoMSAA.Checked = .MovieSetClearLogoMSAA
            Me.chkMovieSetFanartMSAA.Checked = .MovieSetFanartMSAA
            Me.chkMovieSetLandscapeMSAA.Checked = .MovieSetLandscapeMSAA
            Me.chkMovieSetNFOMSAA.Checked = .MovieSetNFOMSAA
            Me.txtMovieSetPathMSAA.Text = .MovieSetPathMSAA.ToString
            Me.chkMovieSetPosterMSAA.Checked = .MovieSetPosterMSAA

            '***************** Expert settings *****************
            Me.chkMovieSetUseExpert.Checked = .MovieSetUseExpert

            '***************** Expert Single ******************
            Me.txtMovieSetBannerExpertSingle.Text = .MovieSetBannerExpertSingle
            Me.txtMovieSetClearArtExpertSingle.Text = .MovieSetClearArtExpertSingle
            Me.txtMovieSetClearLogoExpertSingle.Text = .MovieSetClearLogoExpertSingle
            Me.txtMovieSetFanartExpertSingle.Text = .MovieSetFanartExpertSingle
            Me.txtMovieSetLandscapeExpertSingle.Text = .MovieSetLandscapeExpertSingle
            Me.txtMovieSetNFOExpertSingle.Text = .MovieSetNFOExpertSingle
            Me.txtMovieSetPathExpertSingle.Text = .MovieSetPathExpertSingle
            Me.txtMovieSetPosterExpertSingle.Text = .MovieSetPosterExpertSingle

            '***************** Expert Parent ******************
            Me.txtMovieSetBannerExpertParent.Text = .MovieSetBannerExpertParent
            Me.txtMovieSetClearArtExpertParent.Text = .MovieSetClearArtExpertParent
            Me.txtMovieSetClearLogoExpertParent.Text = .MovieSetClearLogoExpertParent
            Me.txtMovieSetFanartExpertParent.Text = .MovieSetFanartExpertParent
            Me.txtMovieSetLandscapeExpertParent.Text = .MovieSetLandscapeExpertParent
            Me.txtMovieSetNFOExpertParent.Text = .MovieSetNFOExpertParent
            Me.txtMovieSetPosterExpertParent.Text = .MovieSetPosterExpertParent


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkTVUseFrodo.Checked = .TVUseFrodo
            Me.chkTVEpisodeActorThumbsFrodo.Checked = .TVEpisodeActorThumbsFrodo
            Me.chkTVEpisodePosterFrodo.Checked = .TVEpisodePosterFrodo
            Me.chkTVSeasonBannerFrodo.Checked = .TVSeasonBannerFrodo
            Me.chkTVSeasonFanartFrodo.Checked = .TVSeasonFanartFrodo
            Me.chkTVSeasonPosterFrodo.Checked = .TVSeasonPosterFrodo
            Me.chkTVShowActorThumbsFrodo.Checked = .TVShowActorThumbsFrodo
            Me.chkTVShowBannerFrodo.Checked = .TVShowBannerFrodo
            Me.chkTVShowExtrafanartsFrodo.Checked = .TVShowExtrafanartsFrodo
            Me.chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            Me.chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '******** XBMC ArtworkDownloader settings **********
            Me.chkTVSeasonLandscapeAD.Checked = .TVSeasonLandscapeAD
            Me.chkTVShowCharacterArtAD.Checked = .TVShowCharacterArtAD
            Me.chkTVShowClearArtAD.Checked = .TVShowClearArtAD
            Me.chkTVShowClearLogoAD.Checked = .TVShowClearLogoAD
            Me.chkTVShowLandscapeAD.Checked = .TVShowLandscapeAD

            '************* XBMC TvTunes settings ***************
            Me.chkTVXBMCThemeEnable.Checked = .TVShowTVThemeXBMC
            Me.txtTVXBMCThemeCustomPath.Text = .TVShowTVThemeFolderXBMC

            '****************** YAMJ settings ******************
            Me.chkTVUseYAMJ.Checked = .TVUseYAMJ
            Me.chkTVEpisodePosterYAMJ.Checked = .TVEpisodePosterYAMJ
            Me.chkTVSeasonBannerYAMJ.Checked = .TVSeasonBannerYAMJ
            Me.chkTVSeasonFanartYAMJ.Checked = .TVSeasonFanartYAMJ
            Me.chkTVSeasonPosterYAMJ.Checked = .TVSeasonPosterYAMJ
            Me.chkTVShowBannerYAMJ.Checked = .TVShowBannerYAMJ
            Me.chkTVShowFanartYAMJ.Checked = .TVShowFanartYAMJ
            Me.chkTVShowPosterYAMJ.Checked = .TVShowPosterYAMJ

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            Me.chkTVUseBoxee.Checked = .TVUseBoxee
            Me.chkTVEpisodePosterBoxee.Checked = .TVEpisodePosterBoxee
            Me.chkTVSeasonPosterBoxee.Checked = .TVSeasonPosterBoxee
            Me.chkTVShowBannerBoxee.Checked = .TVShowBannerBoxee
            Me.chkTVShowFanartBoxee.Checked = .TVShowFanartBoxee
            Me.chkTVShowPosterBoxee.Checked = .TVShowPosterBoxee

            '***************** Expert settings *****************

        End With

    End Sub

    Private Sub LoadIntLangs()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToArray)
            Me.cbIntLang.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub lvMovies_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMovies.DoubleClick
        If lvMovies.SelectedItems.Count > 0 Then
            Using dMovieSource As New dlgMovieSource
                If dMovieSource.ShowDialog(Convert.ToInt32(lvMovies.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshSources()
                End If
            End Using
        End If
    End Sub

    Private Sub lvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvMovies.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveSource()
    End Sub

    Private Sub lvTVSources_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTVSources.DoubleClick
        If lvTVSources.SelectedItems.Count > 0 Then
            Using dTVSource As New dlgTVSource
                If dTVSource.ShowDialog(Convert.ToInt32(lvTVSources.SelectedItems(0).Text)) = Windows.Forms.DialogResult.OK Then
                    Me.RefreshTVSources()
                End If
            End Using
        End If
    End Sub

    Private Sub lvTVSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvTVSources.KeyDown
        If e.KeyCode = Keys.Delete Then Me.RemoveTVSource()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.SaveSettings()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub RefreshSources()
        Dim lvItem As ListViewItem

        lvMovies.Items.Clear()
        Master.DB.LoadMovieSourcesFromDB()
        For Each s As Structures.MovieSource In Master.MovieSources
            lvItem = New ListViewItem(s.id)
            lvItem.SubItems.Add(s.Name)
            lvItem.SubItems.Add(s.Path)
            tmppath = s.Path
            lvItem.SubItems.Add(If(s.Recursive, "Yes", "No"))
            lvItem.SubItems.Add(If(s.UseFolderName, "Yes", "No"))
            lvItem.SubItems.Add(If(s.IsSingle, "Yes", "No"))
            lvItem.SubItems.Add(If(s.Exclude, "Yes", "No"))
            lvMovies.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.LoadTVSourcesFromDB()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT ID, Name, Path, LastScan, Language, Ordering FROM TVSources;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lvItem = New ListViewItem(SQLreader("ID").ToString)
                    lvItem.SubItems.Add(SQLreader("Name").ToString)
                    lvItem.SubItems.Add(SQLreader("Path").ToString)
                    lvItem.SubItems.Add(Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = SQLreader("Language").ToString).name)
                    lvItem.SubItems.Add(DirectCast(Convert.ToInt32(SQLreader("Ordering")), Enums.Ordering).ToString)
                    tmppath = SQLreader("Path").ToString
                    lvTVSources.Items.Add(lvItem)
                End While
            End Using
        End Using
    End Sub

    Private Sub RemoveSource()
        Try
            If Me.lvMovies.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the movies from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvMovies.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                            While Me.lvMovies.SelectedItems.Count > 0
                                parSource.Value = lvMovies.SelectedItems(0).SubItems(1).Text
                                SQLcommand.CommandText = String.Concat("DELETE FROM movies WHERE source = (?);")
                                SQLcommand.ExecuteNonQuery()
                                SQLcommand.CommandText = String.Concat("DELETE FROM sources WHERE name = (?);")
                                SQLcommand.ExecuteNonQuery()
                                lvMovies.Items.Remove(Me.lvMovies.SelectedItems(0))
                            End While
                        End Using
                        SQLtransaction.Commit()
                    End Using

                    Me.lvMovies.Sort()
                    Me.lvMovies.EndUpdate()
                    Me.lvMovies.Refresh()
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RemoveTVSource()
        Try
            If Me.lvTVSources.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvTVSources.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim parSource As SQLite.SQLiteParameter = SQLcommand.Parameters.Add("parSource", DbType.String, 0, "source")
                            While Me.lvTVSources.SelectedItems.Count > 0
                                parSource.Value = lvTVSources.SelectedItems(0).SubItems(1).Text
                                SQLcommand.CommandText = "SELECT Id FROM TVShows WHERE Source = (?);"
                                Using SQLReader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                                    While SQLReader.Read
                                        Master.DB.DeleteTVShowFromDB(Convert.ToInt64(SQLReader("ID")), True)
                                    End While
                                End Using
                                SQLcommand.CommandText = String.Concat("DELETE FROM TVSources WHERE name = (?);")
                                SQLcommand.ExecuteNonQuery()
                                lvTVSources.Items.Remove(lvTVSources.SelectedItems(0))
                            End While
                        End Using
                        SQLtransaction.Commit()
                    End Using

                    Me.lvTVSources.Sort()
                    Me.lvTVSources.EndUpdate()
                    Me.lvTVSources.Refresh()
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SaveSettings()

        With Master.eSettings

            .GeneralLanguage = tLang

            If Master.eSettings.TVGeneralLanguages.Language.Count > 0 Then
                Dim tvLang As String = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = Me.cbTVGeneralLang.Text).abbreviation
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
            .MovieUseFrodo = Me.chkMovieUseFrodo.Checked
            .MovieActorThumbsFrodo = Me.chkMovieActorThumbsFrodo.Checked
            .MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
            .MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
            .MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
            .MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
            .MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
            .MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

            '*************** XBMC Eden settings ***************
            .MovieUseEden = Me.chkMovieUseEden.Checked
            .MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
            .MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
            .MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
            .MovieFanartEden = Me.chkMovieFanartEden.Checked
            .MovieNFOEden = Me.chkMovieNFOEden.Checked
            .MoviePosterEden = Me.chkMoviePosterEden.Checked
            .MovieTrailerEden = Me.chkMovieTrailerEden.Checked

            '************* XBMC optional settings *************
            .MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

            '******** XBMC ArtworkDownloader settings **********
            .MovieBannerAD = Me.chkMovieBannerAD.Checked
            .MovieClearArtAD = Me.chkMovieClearArtAD.Checked
            .MovieClearLogoAD = Me.chkMovieClearLogoAD.Checked
            .MovieDiscArtAD = Me.chkMovieDiscArtAD.Checked
            .MovieLandscapeAD = Me.chkMovieLandscapeAD.Checked

            '************** XBMC TvTunes settings **************
            .MovieXBMCThemeCustom = Me.chkMovieXBMCThemeCustom.Checked
            .MovieXBMCThemeCustomPath = Me.txtMovieXBMCThemeCustomPath.Text
            .MovieXBMCThemeEnable = Me.chkMovieXBMCThemeEnable.Checked
            .MovieXBMCThemeMovie = Me.chkMovieXBMCThemeMovie.Checked
            .MovieXBMCThemeSub = Me.chkMovieXBMCThemeSub.Checked
            .MovieXBMCThemeSubDir = Me.txtMovieXBMCThemeSubDir.Text

            '****************** YAMJ settings *****************
            .MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
            .MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
            .MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
            .MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
            .MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
            .MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

            '****************** NMJ settings *****************
            .MovieUseNMJ = Me.chkMovieUseNMJ.Checked
            .MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
            .MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
            .MovieNFONMJ = Me.chkMovieNFONMJ.Checked
            .MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
            .MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

            '************** NMJ optional settings *************
            .MovieYAMJCompatibleSets = Me.chkMovieYAMJCompatibleSets.Checked
            .MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
            .MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

            '***************** Boxee settings *****************
            .MovieUseBoxee = Me.chkMovieUseBoxee.Checked
            .MovieFanartBoxee = Me.chkMovieFanartBoxee.Checked
            .MovieNFOBoxee = Me.chkMovieNFOBoxee.Checked
            .MoviePosterBoxee = Me.chkMoviePosterBoxee.Checked

            '***************** Expert settings ****************
            .MovieUseExpert = Me.chkMovieUseExpert.Checked

            '***************** Expert Single ******************
            .MovieActorThumbsExpertSingle = Me.chkMovieActorThumbsExpertSingle.Checked
            .MovieActorThumbsExtExpertSingle = Me.txtMovieActorThumbsExtExpertSingle.Text
            .MovieBannerExpertSingle = Me.txtMovieBannerExpertSingle.Text
            .MovieClearArtExpertSingle = Me.txtMovieClearArtExpertSingle.Text
            .MovieClearLogoExpertSingle = Me.txtMovieClearLogoExpertSingle.Text
            .MovieDiscArtExpertSingle = Me.txtMovieDiscArtExpertSingle.Text
            .MovieExtrafanartsExpertSingle = Me.chkMovieExtrafanartsExpertSingle.Checked
            .MovieExtrathumbsExpertSingle = Me.chkMovieExtrathumbsExpertSingle.Checked
            .MovieFanartExpertSingle = Me.txtMovieFanartExpertSingle.Text
            .MovieLandscapeExpertSingle = Me.txtMovieLandscapeExpertSingle.Text
            .MovieNFOExpertSingle = Me.txtMovieNFOExpertSingle.Text
            .MoviePosterExpertSingle = Me.txtMoviePosterExpertSingle.Text
            .MovieStackExpertSingle = Me.chkMovieStackExpertSingle.Checked
            .MovieTrailerExpertSingle = Me.txtMovieTrailerExpertSingle.Text
            .MovieUnstackExpertSingle = Me.chkMovieUnstackExpertSingle.Checked

            '***************** Expert Multi ******************
            .MovieActorThumbsExpertMulti = Me.chkMovieActorThumbsExpertMulti.Checked
            .MovieActorThumbsExtExpertMulti = Me.txtMovieActorThumbsExtExpertMulti.Text
            .MovieBannerExpertMulti = Me.txtMovieBannerExpertMulti.Text
            .MovieClearArtExpertMulti = Me.txtMovieClearArtExpertMulti.Text
            .MovieClearLogoExpertMulti = Me.txtMovieClearLogoExpertMulti.Text
            .MovieDiscArtExpertMulti = Me.txtMovieDiscArtExpertMulti.Text
            .MovieFanartExpertMulti = Me.txtMovieFanartExpertMulti.Text
            .MovieLandscapeExpertMulti = Me.txtMovieLandscapeExpertMulti.Text
            .MovieNFOExpertMulti = Me.txtMovieNFOExpertMulti.Text
            .MoviePosterExpertMulti = Me.txtMoviePosterExpertMulti.Text
            .MovieStackExpertMulti = Me.chkMovieStackExpertMulti.Checked
            .MovieTrailerExpertMulti = Me.txtMovieTrailerExpertMulti.Text
            .MovieUnstackExpertMulti = Me.chkMovieUnstackExpertMulti.Checked

            '***************** Expert VTS ******************
            .MovieActorThumbsExpertVTS = Me.chkMovieActorThumbsExpertVTS.Checked
            .MovieActorThumbsExtExpertVTS = Me.txtMovieActorThumbsExtExpertVTS.Text
            .MovieBannerExpertVTS = Me.txtMovieBannerExpertVTS.Text
            .MovieClearArtExpertVTS = Me.txtMovieClearArtExpertVTS.Text
            .MovieClearLogoExpertVTS = Me.txtMovieClearLogoExpertVTS.Text
            .MovieDiscArtExpertVTS = Me.txtMovieDiscArtExpertVTS.Text
            .MovieExtrafanartsExpertVTS = Me.chkMovieExtrafanartsExpertVTS.Checked
            .MovieExtrathumbsExpertVTS = Me.chkMovieExtrathumbsExpertVTS.Checked
            .MovieFanartExpertVTS = Me.txtMovieFanartExpertVTS.Text
            .MovieLandscapeExpertVTS = Me.txtMovieLandscapeExpertVTS.Text
            .MovieNFOExpertVTS = Me.txtMovieNFOExpertVTS.Text
            .MoviePosterExpertVTS = Me.txtMoviePosterExpertVTS.Text
            .MovieRecognizeVTSExpertVTS = Me.chkMovieRecognizeVTSExpertVTS.Checked
            .MovieTrailerExpertVTS = Me.txtMovieTrailerExpertVTS.Text
            .MovieUseBaseDirectoryExpertVTS = Me.chkMovieUseBaseDirectoryExpertVTS.Checked

            '***************** Expert BDMV ******************
            .MovieActorThumbsExpertBDMV = Me.chkMovieActorThumbsExpertBDMV.Checked
            .MovieActorThumbsExtExpertBDMV = Me.txtMovieActorThumbsExtExpertBDMV.Text
            .MovieBannerExpertBDMV = Me.txtMovieBannerExpertBDMV.Text
            .MovieClearArtExpertBDMV = Me.txtMovieClearArtExpertBDMV.Text
            .MovieClearLogoExpertBDMV = Me.txtMovieClearLogoExpertBDMV.Text
            .MovieDiscArtExpertBDMV = Me.txtMovieDiscArtExpertBDMV.Text
            .MovieExtrafanartsExpertBDMV = Me.chkMovieExtrafanartsExpertBDMV.Checked
            .MovieExtrathumbsExpertBDMV = Me.chkMovieExtrathumbsExpertBDMV.Checked
            .MovieFanartExpertBDMV = Me.txtMovieFanartExpertBDMV.Text
            .MovieLandscapeExpertBDMV = Me.txtMovieLandscapeExpertBDMV.Text
            .MovieNFOExpertBDMV = Me.txtMovieNFOExpertBDMV.Text
            .MoviePosterExpertBDMV = Me.txtMoviePosterExpertBDMV.Text
            .MovieTrailerExpertBDMV = Me.txtMovieTrailerExpertBDMV.Text
            .MovieUseBaseDirectoryExpertBDMV = Me.chkMovieUseBaseDirectoryExpertBDMV.Checked


            '***************************************************
            '****************** MovieSet Part ******************
            '***************************************************

            '**************** XBMC MSAA settings ***************
            .MovieSetUseMSAA = Me.chkMovieSetUseMSAA.Checked
            .MovieSetBannerMSAA = Me.chkMovieSetBannerMSAA.Checked
            .MovieSetClearArtMSAA = Me.chkMovieSetClearArtMSAA.Checked
            .MovieSetClearLogoMSAA = Me.chkMovieSetClearLogoMSAA.Checked
            .MovieSetFanartMSAA = Me.chkMovieSetFanartMSAA.Checked
            .MovieSetLandscapeMSAA = Me.chkMovieSetLandscapeMSAA.Checked
            .MovieSetNFOMSAA = Me.chkMovieSetNFOMSAA.Checked
            .MovieSetPathMSAA = Me.txtMovieSetPathMSAA.Text
            .MovieSetPosterMSAA = Me.chkMovieSetPosterMSAA.Checked

            '***************** Expert settings ****************
            .MovieSetUseExpert = Me.chkMovieSetUseExpert.Checked

            '***************** Expert Single ******************
            .MovieSetBannerExpertSingle = Me.txtMovieSetBannerExpertSingle.Text
            .MovieSetClearArtExpertSingle = Me.txtMovieSetClearArtExpertSingle.Text
            .MovieSetClearLogoExpertSingle = Me.txtMovieSetClearLogoExpertSingle.Text
            .MovieSetFanartExpertSingle = Me.txtMovieSetFanartExpertSingle.Text
            .MovieSetLandscapeExpertSingle = Me.txtMovieSetLandscapeExpertSingle.Text
            .MovieSetNFOExpertSingle = Me.txtMovieSetNFOExpertSingle.Text
            .MovieSetPathExpertSingle = Me.txtMovieSetPathExpertSingle.Text
            .MovieSetPosterExpertSingle = Me.txtMovieSetPosterExpertSingle.Text

            '***************** Expert Parent ******************
            .MovieSetBannerExpertParent = Me.txtMovieSetBannerExpertParent.Text
            .MovieSetClearArtExpertParent = Me.txtMovieSetClearArtExpertParent.Text
            .MovieSetClearLogoExpertParent = Me.txtMovieSetClearLogoExpertParent.Text
            .MovieSetFanartExpertParent = Me.txtMovieSetFanartExpertParent.Text
            .MovieSetLandscapeExpertParent = Me.txtMovieSetLandscapeExpertParent.Text
            .MovieSetNFOExpertParent = Me.txtMovieSetNFOExpertParent.Text
            .MovieSetPosterExpertParent = Me.txtMovieSetPosterExpertParent.Text


            '***************************************************
            '****************** TV Show Part *******************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            .TVUseFrodo = Me.chkTVUseFrodo.Checked
            .TVEpisodeActorThumbsFrodo = Me.chkTVEpisodeActorThumbsFrodo.Checked
            .TVEpisodePosterFrodo = Me.chkTVEpisodePosterFrodo.Checked
            .TVSeasonBannerFrodo = Me.chkTVSeasonBannerFrodo.Checked
            .TVSeasonFanartFrodo = Me.chkTVSeasonFanartFrodo.Checked
            .TVSeasonPosterFrodo = Me.chkTVSeasonPosterFrodo.Checked
            .TVShowActorThumbsFrodo = Me.chkTVShowActorThumbsFrodo.Checked
            .TVShowBannerFrodo = Me.chkTVShowBannerFrodo.Checked
            .TVShowFanartFrodo = Me.chkTVShowFanartFrodo.Checked
            .TVShowPosterFrodo = Me.chkTVShowPosterFrodo.Checked

            '*************** XBMC Eden settings ****************

            '************* XBMC optional settings **************
            .TVSeasonLandscapeAD = Me.chkTVSeasonLandscapeAD.Checked
            .TVShowCharacterArtAD = Me.chkTVShowCharacterArtAD.Checked
            .TVShowClearArtAD = Me.chkTVShowClearArtAD.Checked
            .TVShowClearLogoAD = Me.chkTVShowClearLogoAD.Checked
            .TVShowExtrafanartsFrodo = Me.chkTVShowExtrafanartsFrodo.Checked
            .TVShowLandscapeAD = Me.chkTVShowLandscapeAD.Checked
            .TVShowTVThemeXBMC = Me.chkTVXBMCThemeEnable.Checked
            .TVShowTVThemeFolderXBMC = Me.txtTVXBMCThemeCustomPath.Text

            '****************** YAMJ settings ******************
            .TVUseYAMJ = Me.chkTVUseYAMJ.Checked
            .TVEpisodePosterYAMJ = Me.chkTVEpisodePosterYAMJ.Checked
            .TVSeasonBannerYAMJ = Me.chkTVSeasonBannerYAMJ.Checked
            .TVSeasonFanartYAMJ = Me.chkTVSeasonFanartYAMJ.Checked
            .TVSeasonPosterYAMJ = Me.chkTVSeasonPosterYAMJ.Checked
            .TVShowBannerYAMJ = Me.chkTVShowBannerYAMJ.Checked
            .TVShowFanartYAMJ = Me.chkTVShowFanartYAMJ.Checked
            .TVShowPosterYAMJ = Me.chkTVShowPosterYAMJ.Checked

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Boxee settings ******************
            .TVUseBoxee = Me.chkTVUseBoxee.Checked
            .TVEpisodePosterBoxee = Me.chkTVEpisodePosterBoxee.Checked
            .TVSeasonPosterBoxee = Me.chkTVSeasonPosterBoxee.Checked
            .TVShowBannerBoxee = Me.chkTVShowBannerBoxee.Checked
            .TVShowFanartBoxee = Me.chkTVShowFanartBoxee.Checked
            .TVShowPosterBoxee = Me.chkTVShowPosterBoxee.Checked

            '***************** Expert settings *****************

        End With

    End Sub

    Private Sub SetUp()

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        Me.chkMovieActorThumbsExpertBDMV.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertMulti.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertSingle.Text = strActorThumbs
        Me.chkMovieActorThumbsExpertVTS.Text = strActorThumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs
        Me.lblTVSourcesFileNamingXBMCDefaultsActorThumbs.Text = strActorThumbs

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        Me.lblMovieSourcesFileNamingExpertBDMVBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertMultiBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertSingleBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingExpertVTSBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        Me.lblMovieSourcesFileNamingXBMCADBanner.Text = strBanner
        Me.lblTVSourcesFileNamingBoxeeDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFileNamingNMTDefaultsBanner.Text = strBanner
        Me.lblTVSourcesFileNamingXBMCDefaultsBanner.Text = strBanner

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        Me.lblMovieClearArtExpertBDMV.Text = strClearArt
        Me.lblMovieClearArtExpertMulti.Text = strClearArt
        Me.lblMovieClearArtExpertSingle.Text = strClearArt
        Me.lblMovieClearArtExpertVTS.Text = strClearArt
        Me.lblMovieSourcesFileNamingXBMCADClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        Me.lblMovieClearLogoExpertBDMV.Text = strClearLogo
        Me.lblMovieClearLogoExpertMulti.Text = strClearLogo
        Me.lblMovieClearLogoExpertSingle.Text = strClearLogo
        Me.lblMovieClearLogoExpertVTS.Text = strClearLogo
        Me.lblMovieSourcesFileNamingXBMCADClearLogo.Text = strClearLogo

        'Defaults
        Dim strDefaults As String = Master.eLang.GetString(713, "Defaults")
        Me.gbMovieSourcesFileNamingBoxeeDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingNMTDefaultsOpts.Text = strDefaults
        Me.gbMovieSourcesFileNamingXBMCDefaultsOpts.Text = strDefaults

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        Me.lblMovieDiscArtExpertBDMV.Text = strDiscArt
        Me.lblMovieDiscArtExpertMulti.Text = strDiscArt
        Me.lblMovieDiscArtExpertSingle.Text = strDiscArt
        Me.lblMovieDiscArtExpertVTS.Text = strDiscArt
        Me.lblMovieSourcesFileNamingXBMCADDiscArt.Text = strDiscArt

        'Enabled
        Dim strEnabled As String = Master.eLang.GetString(774, "Enabled")
        Me.lblMovieSourcesFileNamingBoxeeDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingNMTDefaultsEnabled.Text = strEnabled
        Me.lblMovieSourcesFileNamingXBMCDefaultsEnabled.Text = strEnabled
        Me.chkMovieUseExpert.Text = strEnabled

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        Me.chkMovieExtrafanartsExpertBDMV.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertSingle.Text = strExtrafanarts
        Me.chkMovieExtrafanartsExpertVTS.Text = strExtrafanarts
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts
        Me.lblTVSourcesFileNamingXBMCDefaultsExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        Me.chkMovieExtrathumbsExpertBDMV.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertSingle.Text = strExtrathumbs
        Me.chkMovieExtrathumbsExpertVTS.Text = strExtrathumbs
        Me.lblMovieSourcesFileNamingXBMCDefaultsExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        Me.lblMovieSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertBDMVFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertMultiFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertSingleFanart.Text = strFanart
        Me.lblMovieSourcesFilenamingExpertVTSFanart.Text = strFanart
        Me.lblMovieSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        Me.lblMovieSourcesFileNamingXBMCDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingBoxeeDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingNMTDefaultsFanart.Text = strFanart
        Me.lblTVSourcesFileNamingXBMCDefaultsFanart.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        Me.lblMovieSourcesFileNamingExpertBDMVLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertMultiLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertSingleLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingExpertVTSLandscape.Text = strLandscape
        Me.lblMovieSourcesFileNamingXBMCADLandscape.Text = strLandscape

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        Me.lblMovieSourcesFileNamingBoxeeDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertBDMVNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertMultiNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertSingleNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingExpertVTSNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingNMTDefaultsNFO.Text = strNFO
        Me.lblMovieSourcesFileNamingXBMCDefaultsNFO.Text = strNFO

        'Optional Images
        Dim strOptionalImages As String = Master.eLang.GetString(267, "Optional Images")
        Me.gbMovieSourcesFileNamingExpertBDMVImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertMultiImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertSingleImagesOpts.Text = strOptionalImages
        Me.gbMovieSourcesFileNamingExpertVTSImagesOpts.Text = strOptionalImages

        'Optional Settings
        Dim strOptionalSettings As String = Master.eLang.GetString(1175, "Optional Settings")
        Me.gbMovieSourcesFileNamingExpertBDMVOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertMultiOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertSingleOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingExpertVTSOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingNMTOptionalOpts.Text = strOptionalSettings
        Me.gbMovieSourcesFileNamingXBMCOptionalOpts.Text = strOptionalSettings

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        Me.lblMoviePosterExpertBDMV.Text = strPoster
        Me.lblMoviePosterExpertMulti.Text = strPoster
        Me.lblMoviePosterExpertSingle.Text = strPoster
        Me.lblMoviePosterExpertVTS.Text = strPoster
        Me.lblTVSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        Me.lblTVSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        Me.lblTVSourcesFileNamingXBMCDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingBoxeeDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingNMTDefaultsPoster.Text = strPoster
        Me.lblMovieSourcesFileNamingXBMCDefaultsPoster.Text = strPoster

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        Me.lblMovieTrailerExpertBDMV.Text = strTrailer
        Me.lblMovieTrailerExpertMulti.Text = strTrailer
        Me.lblMovieTrailerExpertSingle.Text = strTrailer
        Me.lblMovieTrailerExpertVTS.Text = strTrailer
        Me.lblMovieSourcesFileNamingNMTDefaultsTrailer.Text = strTrailer
        Me.lblMovieSourcesFileNamingXBMCDefaultsTrailer.Text = strTrailer


        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.Label1.Text = Master.eLang.GetString(405, "Welcome to Ember Media Manager")
        Me.Label10.Text = Master.eLang.GetString(113, "Now select the default language you would like Ember to look for when scraping TV Show items.")
        Me.Label11.Text = Master.eLang.GetString(804, "And finally, let's tell Ember Media Manager what TV Show files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Me.Label2.Text = String.Format(Master.eLang.GetString(415, "This is either your first time running Ember Media Manager or you have upgraded to a newer version.  There are a few things Ember Media Manager needs to know to work properly.  This wizard will walk you through configuring Ember Media Manager to work for your set up.{0}{0}Only a handful of settings will be covered in this wizard. You can change these or any other setting at any time by selecting ""Settings..."" from the ""Edit"" menu."), vbNewLine)
        Me.Label3.Text = Master.eLang.GetString(414, "First, let's tell Ember Media Manager where to locate all your movies. You can add as many sources as you wish.")
        Me.Label32.Text = Master.eLang.GetString(430, "Interface Language")
        Me.lblMovieSettings.Text = Master.eLang.GetString(416, "Now that Ember Media Manager knows WHERE to look for the files, we need to tell it WHAT files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Me.Label48.Text = Master.eLang.GetString(416, "Now that Ember Media Manager knows WHERE to look for the files, we need to tell it WHAT files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Me.Label6.Text = Master.eLang.GetString(408, "That wasn't so hard was it?  As mentioned earlier, you can change these or any other options in the Settings dialog.")
        Me.Label7.Text = String.Format(Master.eLang.GetString(409, "That's it!{0}Ember Media Manager is Ready!"), vbNewLine)
        Me.Label8.Text = String.Format(Master.eLang.GetString(417, "Some options you may be interested in:{0}{0}Custom Filters - If your movie files have things like ""DVDRip"", ""BluRay"", ""x264"", etc in their folder or file name and you wish to filter the names when loading into the media list, you can utilize the Custom Filter option.  The custom filter is RegEx compatible for maximum usability.{0}{0}Images - This section allows you to select which websites to ""scrape"" images from as well as select a preferred size for the images Ember Media Manager selects.{0}{0}Locks - This section allows you to ""lock"" certain information so it does not get updated even if you re-scrape the movie. This is useful if you manually edit the title, outline, or plot of a movie and wish to keep your changes."), vbNewLine)
        Me.Label9.Text = Master.eLang.GetString(803, "Next, let's tell Ember Media Manager where to locate all your TV Shows. You can add as many sources as you wish.")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Text = Master.eLang.GetString(402, "Ember Startup Wizard")
        Me.btnBack.Text = Master.eLang.GetString(403, "< Back")
        Me.btnMovieAddFolder.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnMovieRem.Text = Master.eLang.GetString(30, "Remove")
        Me.btnNext.Text = Master.eLang.GetString(404, "Next >")
        Me.btnTVAddSource.Text = Me.btnMovieAddFolder.Text
        Me.btnTVGeneralLangFetch.Text = Master.eLang.GetString(742, "Fetch Available Languages")
        Me.btnTVRemoveSource.Text = Me.btnMovieRem.Text
        Me.colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colPath.Text = Master.eLang.GetString(410, "Path")
        Me.colRecur.Text = Master.eLang.GetString(411, "Recursive")
        Me.colSingle.Text = Master.eLang.GetString(413, "Single Video")
        Me.colExclude.Text = Master.eLang.GetString(264, "Exclude")
        Me.lvTVSources.Columns(1).Text = Master.eLang.GetString(232, "Name")
        Me.lvTVSources.Columns(2).Text = Master.eLang.GetString(410, "Path")
        Me.lvTVSources.Columns(3).Text = Master.eLang.GetString(610, "Language")
        Me.lvTVSources.Columns(4).Text = Master.eLang.GetString(1167, "Ordering")
        Me.pnlWelcome.Location = New Point(166, 7)
        Me.pnlWelcome.Visible = True
        Me.pnlMovieSources.Visible = False
        Me.pnlMovieSettings.Visible = False
        Me.pnlMovieSetSettings.Visible = False
        Me.pnlTVShowSource.Visible = False
        Me.pnlTVShowSettings.Visible = False
        Me.pnlDone.Visible = False
        Me.pnlMovieSources.Location = New Point(166, 7)
        Me.pnlMovieSettings.Location = New Point(166, 7)
        Me.pnlMovieSetSettings.Location = New Point(166, 7)
        Me.pnlTVShowSource.Location = New Point(166, 7)
        Me.pnlTVShowSettings.Location = New Point(166, 7)
        Me.pnlDone.Location = New Point(166, 7)
    End Sub

#End Region 'Methods

End Class