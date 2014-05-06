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
' # Dialog size: 720; 570
' # Enlarge it to see all the panels.

Imports System.IO
Imports EmberAPI

Public Class dlgWizard

    Private tLang As String
    Private tmppath As String = String.Empty

#Region "Methods"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Select Case True
            Case Me.pnlMovieSource.Visible
                Me.btnBack.Enabled = False
                Me.pnlMovieSource.Visible = False
                Me.pnlWelcome.Visible = True
            Case Me.pnlMovieSettings.Visible
                Me.pnlMovieSettings.Visible = False
                Me.pnlMovieSource.Visible = True
            Case Me.pnlTVShowSource.Visible
                Me.pnlTVShowSource.Visible = False
                Me.pnlMovieSettings.Visible = True
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

    Private Sub btnTVLanguageFetch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTVLanguageFetch.Click
        Master.eSettings.TVGeneralLanguages.Clear()
        Master.eSettings.TVGeneralLanguages.AddRange(ModulesManager.Instance.TVGetLangs("thetvdb.com"))
        Me.cbTVLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages Select lLang.LongLang).ToArray)

        If Me.cbTVLanguage.Items.Count > 0 Then
            Me.cbTVLanguage.Text = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.ShortLang = Master.eSettings.TVGeneralLanguage).LongLang
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
                Me.pnlMovieSource.Visible = True
            Case Me.pnlMovieSource.Visible
                Me.pnlMovieSource.Visible = False
                Me.pnlMovieSettings.Visible = True
            Case Me.pnlMovieSettings.Visible
                Me.pnlMovieSettings.Visible = False
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub chkMovieUseBoxee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseBoxee.CheckedChanged

        'Me.chkActorThumbsBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkMovieBannerBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkClearArtBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkClearLogoBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkExtrafanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkExtrathumbsBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkDiscArtBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieFanartBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkLandscapeBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMovieNFOBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        Me.chkMoviePosterBoxee.Enabled = Me.chkMovieUseBoxee.Checked
        'Me.chkMovieTrailerBoxee.Enabled = Me.chkMovieUseBoxee.Checked

        If Not Me.chkMovieUseBoxee.Checked Then
            ' Me.chkActorThumbsBoxee.Checked = False
            'Me.chkMovieBannerBoxee.Checked = False
            'Me.chkClearArtBoxee.Checked = False
            'Me.chkClearLogoBoxee.Checked = False
            'Me.chkDiscArtBoxee.Checked = False
            'Me.chkExtrafanartBoxee.Checked = False
            'Me.chkExtrathumbsBoxee.Checked = False
            Me.chkMovieFanartBoxee.Checked = False
            'Me.chkLandscapeBoxee.Checked = False
            Me.chkMovieNFOBoxee.Checked = False
            Me.chkMoviePosterBoxee.Checked = False
            'Me.chkMovieTrailerBoxee.Checked = False
        Else
            'Me.chkActorThumbsBoxee.Checked = True
            'Me.chkMovieBannerBoxee.Checked = True
            'Me.chkClearArtBoxee.Checked = True
            'Me.chkClearLogoBoxee.Checked = True
            'Me.chkDiscArtBoxee.Checked = True
            'Me.chkExtrafanartBoxee.Checked = True
            'Me.chkExtrathumbsBoxee.Checked = True
            Me.chkMovieFanartBoxee.Checked = True
            'Me.chkLandscapeBoxee.Checked = True
            Me.chkMovieNFOBoxee.Checked = True
            Me.chkMoviePosterBoxee.Checked = True
            'Me.chkMovieTrailerBoxee.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseFrodo.CheckedChanged

        Me.chkMovieActorThumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieBannerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearArtFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieClearLogoFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrafanartsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieExtrathumbsFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieDiscArtFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieFanartFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieLandscapeFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieNFOFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMoviePosterFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieTrailerFrodo.Enabled = Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseFrodo.Checked OrElse Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Me.chkMovieUseFrodo.Checked AndAlso Not Me.chkMovieUseEden.Checked

        If Not Me.chkMovieUseFrodo.Checked Then
            Me.chkMovieActorThumbsFrodo.Checked = False
            Me.chkMovieBannerFrodo.Checked = False
            Me.chkMovieClearArtFrodo.Checked = False
            Me.chkMovieClearLogoFrodo.Checked = False
            Me.chkMovieDiscArtFrodo.Checked = False
            Me.chkMovieExtrafanartsFrodo.Checked = False
            Me.chkMovieExtrathumbsFrodo.Checked = False
            Me.chkMovieFanartFrodo.Checked = False
            Me.chkMovieLandscapeFrodo.Checked = False
            Me.chkMovieNFOFrodo.Checked = False
            Me.chkMoviePosterFrodo.Checked = False
            Me.chkMovieTrailerFrodo.Checked = False
            Me.chkMovieXBMCProtectVTSBDMV.Checked = False
        Else
            Me.chkMovieActorThumbsFrodo.Checked = True
            Me.chkMovieBannerFrodo.Checked = True
            Me.chkMovieClearArtFrodo.Checked = True
            Me.chkMovieClearLogoFrodo.Checked = True
            Me.chkMovieDiscArtFrodo.Checked = True
            Me.chkMovieExtrafanartsFrodo.Checked = True
            Me.chkMovieExtrathumbsFrodo.Checked = True
            Me.chkMovieFanartFrodo.Checked = True
            Me.chkMovieLandscapeFrodo.Checked = True
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
        'Me.chkBannerEden.Enabled = Me.chkUseEden.Checked
        'Me.chkClearArtEden.Enabled = Me.chkUseEden.Checked
        'Me.chkClearLogoEden.Enabled = Me.chkUseEden.Checked
        Me.chkMovieExtrafanartsEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieExtrathumbsEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkDiscArtEden.Enabled = Me.chkUseEden.Checked
        Me.chkMovieFanartEden.Enabled = Me.chkMovieUseEden.Checked
        'Me.chkLandscapeEden.Enabled = Me.chkUseEden.Checked
        Me.chkMovieNFOEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMoviePosterEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieTrailerEden.Enabled = Me.chkMovieUseEden.Checked
        Me.chkMovieXBMCThemeEnable.Enabled = Me.chkMovieUseEden.Checked OrElse Me.chkMovieUseFrodo.Checked
        Me.chkMovieXBMCProtectVTSBDMV.Enabled = Not Me.chkMovieUseEden.Checked AndAlso Me.chkMovieUseFrodo.Checked

        If Not Me.chkMovieUseEden.Checked Then
            Me.chkMovieActorThumbsEden.Checked = False
            'Me.chkBannerEden.Checked = False
            'Me.chkClearArtEden.Checked = False
            'Me.chkClearLogoEden.Checked = False
            'Me.chkDiscArtEden.Checked = False
            Me.chkMovieExtrafanartsEden.Checked = False
            Me.chkMovieExtrathumbsEden.Checked = False
            Me.chkMovieFanartEden.Checked = False
            'Me.chkLandscapeEden.Checked = False
            Me.chkMovieNFOEden.Checked = False
            Me.chkMoviePosterEden.Checked = False
            Me.chkMovieTrailerEden.Checked = False
        Else
            Me.chkMovieActorThumbsEden.Checked = True
            'Me.chkBannerEden.Checked = True
            'Me.chkClearArtEden.Checked = True
            'Me.chkClearLogoEden.Checked = True
            'Me.chkDiscArtEden.Checked = True
            Me.chkMovieExtrafanartsEden.Checked = True
            Me.chkMovieExtrathumbsEden.Checked = True
            Me.chkMovieFanartEden.Checked = True
            'Me.chkLandscapeEden.Checked = True
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

        'Me.chkActorThumbsYAMJ.Enabled = Me.chkUseYAMJ.Checked
        Me.chkMovieBannerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkClearArtYAMJ.Enabled = Me.chkUseYAMJ.Checked
        'Me.chkClearLogoYAMJ.Enabled = Me.chkUseYAMJ.Checked
        'Me.chkExtrafanartYAMJ.Enabled = Me.chkUseYAMJ.Checked
        'Me.chkExtrathumbsYAMJ.Enabled = Me.chkUseYAMJ.Checked
        'Me.chkDiscArtYAMJ.Enabled = Me.chkUseYAMJ.Checked
        Me.chkMovieFanartYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        'Me.chkLandscapeYAMJ.Enabled = Me.chkUseYAMJ.Checked
        Me.chkMovieNFOYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMoviePosterYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieTrailerYAMJ.Enabled = Me.chkMovieUseYAMJ.Checked
        Me.chkMovieYAMJWatchedFile.Enabled = Me.chkMovieUseYAMJ.Checked

        If Not Me.chkMovieUseYAMJ.Checked Then
            ' Me.chkActorThumbsYAMJ.Checked = False
            Me.chkMovieBannerYAMJ.Checked = False
            'Me.chkClearArtYAMJ.Checked = False
            'Me.chkClearLogoYAMJ.Checked = False
            'Me.chkDiscArtYAMJ.Checked = False
            'Me.chkExtrafanartYAMJ.Checked = False
            'Me.chkExtrathumbsYAMJ.Checked = False
            Me.chkMovieFanartYAMJ.Checked = False
            'Me.chkLandscapeYAMJ.Checked = False
            Me.chkMovieNFOYAMJ.Checked = False
            Me.chkMoviePosterYAMJ.Checked = False
            Me.chkMovieTrailerYAMJ.Checked = False
            Me.chkMovieYAMJWatchedFile.Checked = False
        Else
            'Me.chkActorThumbsYAMJ.Checked = True
            Me.chkMovieBannerYAMJ.Checked = True
            'Me.chkClearArtYAMJ.Checked = True
            'Me.chkClearLogoYAMJ.Checked = True
            'Me.chkDiscArtYAMJ.Checked = True
            'Me.chkExtrafanartYAMJ.Checked = True
            'Me.chkExtrathumbsYAMJ.Checked = True
            Me.chkMovieFanartYAMJ.Checked = True
            'Me.chkLandscapeYAMJ.Checked = True
            Me.chkMovieNFOYAMJ.Checked = True
            Me.chkMoviePosterYAMJ.Checked = True
            Me.chkMovieTrailerYAMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieUseNMJCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieUseNMJ.CheckedChanged

        'Me.chkActorThumbsNMJ.Enabled = Me.chkUseNMJ.Checked
        Me.chkMovieBannerNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkClearArtNMJ.Enabled = Me.chkUseNMJ.Checked
        'Me.chkClearLogoNMJ.Enabled = Me.chkUseNMJ.Checked
        'Me.chkExtrafanartNMJ.Enabled = Me.chkUseNMJ.Checked
        'Me.chkExtrathumbsNMJ.Enabled = Me.chkUseNMJ.Checked
        'Me.chkDiscArtNMJ.Enabled = Me.chkUseNMJ.Checked
        Me.chkMovieFanartNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        'Me.chkLandscapeNMJ.Enabled = Me.chkUseNMJ.Checked
        Me.chkMovieNFONMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMoviePosterNMJ.Enabled = Me.chkMovieUseNMJ.Checked
        Me.chkMovieTrailerNMJ.Enabled = Me.chkMovieUseNMJ.Checked

        If Not Me.chkMovieUseNMJ.Checked Then
            ' Me.chkActorThumbsNMJ.Checked = False
            Me.chkMovieBannerNMJ.Checked = False
            'Me.chkClearArtNMJ.Checked = False
            'Me.chkClearLogoNMJ.Checked = False
            'Me.chkDiscArtNMJ.Checked = False
            'Me.chkExtrafanartNMJ.Checked = False
            'Me.chkExtrathumbsNMJ.Checked = False
            Me.chkMovieFanartNMJ.Checked = False
            'Me.chkLandscapeNMJ.Checked = False
            Me.chkMovieNFONMJ.Checked = False
            Me.chkMoviePosterNMJ.Checked = False
            Me.chkMovieTrailerNMJ.Checked = False
        Else
            'Me.chkActorThumbsNMJ.Checked = True
            Me.chkMovieBannerNMJ.Checked = True
            'Me.chkClearArtNMJ.Checked = True
            'Me.chkClearLogoNMJ.Checked = True
            'Me.chkDiscArtNMJ.Checked = True
            'Me.chkExtrafanartNMJ.Checked = True
            'Me.chkExtrathumbsNMJ.Checked = True
            Me.chkMovieFanartNMJ.Checked = True
            'Me.chkLandscapeNMJ.Checked = True
            Me.chkMovieNFONMJ.Checked = True
            Me.chkMoviePosterNMJ.Checked = True
            Me.chkMovieTrailerNMJ.Checked = True
        End If
    End Sub

    Private Sub chkMovieXBMCThemeCustomPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeCustom.CheckedChanged

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

    Private Sub chkMovieXBMCThemeSubPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieXBMCThemeSub.CheckedChanged

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

    Private Sub chkTVShowTVThemeXBMC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVShowTVThemeXBMC.CheckedChanged
        Me.btnTVShowTVThemeBrowse.Enabled = Me.chkTVShowTVThemeXBMC.Checked
        Me.txtTVShowTVThemeFolderXBMC.Enabled = Me.chkTVShowTVThemeXBMC.Checked
    End Sub

    Private Sub btnTVShowTVThemeBrowse_Click(sender As Object, e As EventArgs) Handles btnTVShowTVThemeBrowse.Click
        With Me.fbdBrowse
            fbdBrowse.Description = Master.eLang.GetString(1028, "Select the folder where you wish to store your tv themes...")
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not String.IsNullOrEmpty(.SelectedPath.ToString) AndAlso Directory.Exists(.SelectedPath) Then
                    Me.txtTVShowTVThemeFolderXBMC.Text = .SelectedPath.ToString
                End If
            End If
        End With
    End Sub

    Private Sub chkTVUseFrodo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTVUseFrodo.CheckedChanged

        Me.chkTVEpisodeActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVEpisodePosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonLandscapeXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVSeasonPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowActorThumbsFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowBannerFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowCharacterArtXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearArtXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowClearLogoXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowFanartFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowLandscapeXBMC.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowPosterFrodo.Enabled = Me.chkTVUseFrodo.Checked
        Me.chkTVShowTVThemeXBMC.Enabled = Me.chkTVUseFrodo.Checked

        If Not Me.chkTVUseFrodo.Checked Then
            Me.chkTVEpisodeActorThumbsFrodo.Checked = False
            Me.chkTVEpisodePosterFrodo.Checked = False
            Me.chkTVSeasonBannerFrodo.Checked = False
            Me.chkTVSeasonFanartFrodo.Checked = False
            Me.chkTVSeasonLandscapeXBMC.Checked = False
            Me.chkTVSeasonPosterFrodo.Checked = False
            Me.chkTVShowActorThumbsFrodo.Checked = False
            Me.chkTVShowBannerFrodo.Checked = False
            Me.chkTVShowCharacterArtXBMC.Checked = False
            Me.chkTVShowClearArtXBMC.Checked = False
            Me.chkTVShowClearLogoXBMC.Checked = False
            Me.chkTVShowFanartFrodo.Checked = False
            Me.chkTVShowLandscapeXBMC.Checked = False
            Me.chkTVShowPosterFrodo.Checked = False
            Me.chkTVShowTVThemeXBMC.Checked = False
        Else
            Me.chkTVEpisodeActorThumbsFrodo.Checked = True
            Me.chkTVEpisodePosterFrodo.Checked = True
            Me.chkTVSeasonBannerFrodo.Checked = True
            Me.chkTVSeasonFanartFrodo.Checked = True
            Me.chkTVSeasonLandscapeXBMC.Checked = True
            Me.chkTVSeasonPosterFrodo.Checked = True
            Me.chkTVShowActorThumbsFrodo.Checked = True
            Me.chkTVShowBannerFrodo.Checked = True
            Me.chkTVShowCharacterArtXBMC.Checked = True
            Me.chkTVShowClearArtXBMC.Checked = True
            Me.chkTVShowClearLogoXBMC.Checked = True
            Me.chkTVShowFanartFrodo.Checked = True
            Me.chkTVShowLandscapeXBMC.Checked = True
            Me.chkTVShowPosterFrodo.Checked = True
            'Me.chkTVShowTVThemeXBMC.Checked = True
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

        Me.cbTVLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages Select lLang.LongLang).ToArray)
        If Me.cbTVLanguage.Items.Count > 0 Then
            Me.cbTVLanguage.Text = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.ShortLang = Master.eSettings.TVGeneralLanguage).LongLang
        End If

        With Master.eSettings

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            Me.chkMovieUseFrodo.Checked = .MovieUseFrodo
            Me.chkMovieActorThumbsFrodo.Checked = .MovieActorThumbsFrodo
            Me.chkMovieBannerFrodo.Checked = .MovieBannerFrodo
            Me.chkMovieClearArtFrodo.Checked = .MovieClearArtFrodo
            Me.chkMovieClearLogoFrodo.Checked = .MovieClearLogoFrodo
            Me.chkMovieDiscArtFrodo.Checked = .MovieDiscArtFrodo
            Me.chkMovieExtrafanartsFrodo.Checked = .MovieExtrafanartsFrodo
            Me.chkMovieExtrathumbsFrodo.Checked = .MovieExtrathumbsFrodo
            Me.chkMovieFanartFrodo.Checked = .MovieFanartFrodo
            Me.chkMovieLandscapeFrodo.Checked = .MovieLandscapeFrodo
            Me.chkMovieNFOFrodo.Checked = .MovieNFOFrodo
            Me.chkMoviePosterFrodo.Checked = .MoviePosterFrodo
            Me.chkMovieTrailerFrodo.Checked = .MovieTrailerFrodo

            '*************** XBMC Eden settings ***************
            Me.chkMovieUseEden.Checked = .MovieUseEden
            Me.chkMovieActorThumbsEden.Checked = .MovieActorThumbsEden
            'Me.chkBannerEden.Checked = .MovieBannerEden
            'Me.chkClearArtEden.Checked = .MovieClearArtEden
            'Me.chkClearLogoEden.Checked = .MovieClearLogoEden
            'Me.chkDiscArtEden.Checked = .MovieDiscArtEden
            Me.chkMovieExtrafanartsEden.Checked = .MovieExtrafanartsEden
            Me.chkMovieExtrathumbsEden.Checked = .MovieExtrathumbsEden
            Me.chkMovieFanartEden.Checked = .MovieFanartEden
            'Me.chkLandscapeEden.Checked = .MovieLandscapeEden
            Me.chkMovieNFOEden.Checked = .MovieNFOEden
            Me.chkMoviePosterEden.Checked = .MoviePosterEden
            Me.chkMovieTrailerEden.Checked = .MovieTrailerEden

            '************* XBMC optional settings *************
            Me.chkMovieXBMCTrailerFormat.Checked = .MovieXBMCTrailerFormat
            Me.chkMovieXBMCProtectVTSBDMV.Checked = .MovieXBMCProtectVTSBDMV

            '*************** XBMC theme settings ***************
            Me.chkMovieXBMCThemeEnable.Checked = .MovieXBMCThemeEnable
            Me.chkMovieXBMCThemeCustom.Checked = .MovieXBMCThemeCustom
            Me.chkMovieXBMCThemeMovie.Checked = .MovieXBMCThemeMovie
            Me.chkMovieXBMCThemeSub.Checked = .MovieXBMCThemeSub
            Me.txtMovieXBMCThemeCustomPath.Text = .MovieXBMCThemeCustomPath
            Me.txtMovieXBMCThemeSubDir.Text = .MovieXBMCThemeSubDir

            '****************** YAMJ settings *****************
            Me.chkMovieUseYAMJ.Checked = .MovieUseYAMJ
            'Me.chkActorThumbsYAMJ.Checked = .MovieActorThumbsYAMJ
            Me.chkMovieBannerYAMJ.Checked = .MovieBannerYAMJ
            'Me.chkClearArtYAMJ.Checked = .MovieClearArtYAMJ
            'Me.chkClearLogoYAMJ.Checked = .MovieClearLogoYAMJ
            'Me.chkDiscArtYAMJ.Checked = .MovieDiscArtYAMJ
            'Me.chkExtrafanartYAMJ.Checked = .MovieExtrafanartYAMJ
            'Me.chkExtrathumbsYAMJ.Checked = .MovieExtrathumbsYAMJ
            Me.chkMovieFanartYAMJ.Checked = .MovieFanartYAMJ
            'Me.chkLandscapeYAMJ.Checked = .MovieLandscapeYAMJ
            Me.chkMovieNFOYAMJ.Checked = .MovieNFOYAMJ
            Me.chkMoviePosterYAMJ.Checked = .MoviePosterYAMJ
            Me.chkMovieTrailerYAMJ.Checked = .MovieTrailerYAMJ

            '****************** NMJ settings ******************
            Me.chkMovieUseNMJ.Checked = .MovieUseNMJ
            'Me.chkActorThumbsNMJ.Checked = .MovieActorThumbsNMJ
            Me.chkMovieBannerNMJ.Checked = .MovieBannerNMJ
            'Me.chkClearArtNMJ.Checked = .MovieClearArtNMJ
            'Me.chkClearLogoNMJ.Checked = .MovieClearLogoNMJ
            'Me.chkDiscArtNMJ.Checked = .MovieDiscArtNMJ
            'Me.chkExtrafanartNMJ.Checked = .MovieExtrafanartNMJ
            'Me.chkExtrathumbsNMJ.Checked = .MovieExtrathumbsNMJ
            Me.chkMovieFanartNMJ.Checked = .MovieFanartNMJ
            'Me.chkLandscapeNMJ.Checked = .MovieLandscapeNMJ
            Me.chkMovieNFONMJ.Checked = .MovieNFONMJ
            Me.chkMoviePosterNMJ.Checked = .MoviePosterNMJ
            Me.chkMovieTrailerNMJ.Checked = .MovieTrailerNMJ

            '************** NMJ optional settings *************
            Me.chkMovieYAMJWatchedFile.Checked = .MovieYAMJWatchedFile
            Me.txtMovieYAMJWatchedFolder.Text = .MovieYAMJWatchedFolder

            '***************** Boxee settings ******************
            Me.chkMovieUseBoxee.Checked = .MovieUseBoxee
            'Me.chkActorThumbsBoxee.Checked = .MovieActorThumbsBoxee
            'Me.chkMovieBannerBoxee.Checked = .MovieBannerBoxee
            'Me.chkClearArtBoxee.Checked = .MovieClearArtBoxee
            'Me.chkClearLogoBoxee.Checked = .MovieClearLogoBoxee
            'Me.chkDiscArtBoxee.Checked = .MovieDiscArtBoxee
            'Me.chkExtrafanartBoxee.Checked = .MovieExtrafanartBoxee
            'Me.chkExtrathumbsBoxee.Checked = .MovieExtrathumbsBoxee
            Me.chkMovieFanartBoxee.Checked = .MovieFanartBoxee
            'Me.chkLandscapeBoxee.Checked = .MovieLandscapeBoxee
            Me.chkMovieNFOBoxee.Checked = .MovieNFOBoxee
            Me.chkMoviePosterBoxee.Checked = .MoviePosterBoxee
            'Me.chkMovieTrailerBoxee.Checked = .MovieTrailerBoxee

            '***************** Expert settings ****************
            Me.chkMovieUseExpert.Checked = .MovieUseExpert

            '***************** Expert Single ******************
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

            '***************** Expert Multi ******************
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

            '***************** Expert VTS ******************
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

            '***************** Expert BDMV ******************
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
            Me.chkTVShowFanartFrodo.Checked = .TVShowFanartFrodo
            Me.chkTVShowPosterFrodo.Checked = .TVShowPosterFrodo

            '*************** XBMC Eden settings ****************

            '************* XBMC optional settings **************
            Me.chkTVSeasonLandscapeXBMC.Checked = .TVSeasonLandscapeXBMC
            Me.chkTVShowCharacterArtXBMC.Checked = .TVShowCharacterArtXBMC
            Me.chkTVShowClearArtXBMC.Checked = .TVShowClearArtXBMC
            Me.chkTVShowClearLogoXBMC.Checked = .TVShowClearLogoXBMC
            Me.chkTVShowLandscapeXBMC.Checked = .TVShowLandscapeXBMC
            Me.chkTVShowTVThemeXBMC.Checked = .TVShowTVThemeXBMC
            Me.txtTVShowTVThemeFolderXBMC.Text = .TVShowTVThemeFolderXBMC

            '****************** YAMJ settings ******************

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

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
            lvMovies.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RefreshTVSources()
        Dim lvItem As ListViewItem
        Master.DB.LoadTVSourcesFromDB()
        lvTVSources.Items.Clear()
        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
            SQLcommand.CommandText = "SELECT ID, Name, path, LastScan FROM TVSources;"
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                While SQLreader.Read
                    lvItem = New ListViewItem(SQLreader("ID").ToString)
                    lvItem.SubItems.Add(SQLreader("Name").ToString)
                    lvItem.SubItems.Add(SQLreader("Path").ToString)
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

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MediaDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub RemoveTVSource()
        Try
            If Me.lvTVSources.SelectedItems.Count > 0 Then
                If MsgBox(Master.eLang.GetString(418, "Are you sure you want to remove the selected sources? This will remove the TV Shows from these sources from the Ember database."), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Master.eLang.GetString(104, "Are You Sure?")) = MsgBoxResult.Yes Then
                    Me.lvTVSources.BeginUpdate()

                    Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MediaDBConn.BeginTransaction()
                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SaveSettings()

        With Master.eSettings

            .GeneralLanguage = tLang

            If Master.eSettings.TVGeneralLanguages.Count > 0 Then
                Dim tLang As String = Master.eSettings.TVGeneralLanguages.FirstOrDefault(Function(l) l.LongLang = Me.cbTVLanguage.Text).ShortLang
                If Not String.IsNullOrEmpty(tLang) Then
                    Master.eSettings.TVGeneralLanguage = tLang
                Else
                    Master.eSettings.TVGeneralLanguage = "en"
                End If
            Else
                Master.eSettings.TVGeneralLanguage = "en"
            End If

            '***************************************************
            '******************* Movie Part ********************
            '***************************************************

            '*************** XBMC Frodo settings ***************
            .MovieUseFrodo = Me.chkMovieUseFrodo.Checked
            .MovieActorThumbsFrodo = Me.chkMovieActorThumbsFrodo.Checked
            .MovieBannerFrodo = Me.chkMovieBannerFrodo.Checked
            .MovieClearArtFrodo = Me.chkMovieClearArtFrodo.Checked
            .MovieClearLogoFrodo = Me.chkMovieClearLogoFrodo.Checked
            .MovieDiscArtFrodo = Me.chkMovieDiscArtFrodo.Checked
            .MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
            .MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
            .MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
            .MovieLandscapeFrodo = Me.chkMovieLandscapeFrodo.Checked
            .MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
            .MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
            .MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

            '*************** XBMC Eden settings ***************
            .MovieUseEden = Me.chkMovieUseEden.Checked
            .MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
            '.MovieBannerEden = Me.chkBannerEden.Checked
            '.MovieClearArtEden = Me.chkClearArtEden.Checked
            '.MovieClearLogoEden = Me.chkClearLogoEden.Checked
            '.MovieDiscArtEden = Me.chkDiscArtEden.Checked
            .MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
            .MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
            .MovieFanartEden = Me.chkMovieFanartEden.Checked
            '.MovieLandscapeEden = Me.chkLandscapeEden.Checked
            .MovieNFOEden = Me.chkMovieNFOEden.Checked
            .MoviePosterEden = Me.chkMoviePosterEden.Checked
            .MovieTrailerEden = Me.chkMovieTrailerEden.Checked

            '************* XBMC optional settings *************
            .MovieXBMCTrailerFormat = Me.chkMovieXBMCTrailerFormat.Checked
            .MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

            '*************** XBMC theme settings ***************
            .MovieXBMCThemeCustom = Me.chkMovieXBMCThemeCustom.Checked
            .MovieXBMCThemeCustomPath = Me.txtMovieXBMCThemeCustomPath.Text
            .MovieXBMCThemeEnable = Me.chkMovieXBMCThemeEnable.Checked
            .MovieXBMCThemeMovie = Me.chkMovieXBMCThemeMovie.Checked
            .MovieXBMCThemeSub = Me.chkMovieXBMCThemeSub.Checked
            .MovieXBMCThemeSubDir = Me.txtMovieXBMCThemeSubDir.Text

            '****************** YAMJ settings *****************
            .MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
            '.MovieActorThumbsYAMJ = Me.chkActorThumbsYAMJ.Checked
            .MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
            '.MovieClearArtYAMJ = Me.chkClearArtYAMJ.Checked
            '.MovieClearLogoYAMJ = Me.chkClearLogoYAMJ.Checked
            '.MovieDiscArtYAMJ = Me.chkDiscArtYAMJ.Checked
            '.MovieExtrafanartYAMJ = Me.chkExtrafanartYAMJ.Checked
            '.MovieExtrathumbsYAMJ = Me.chkExtrathumbsYAMJ.Checked
            .MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
            '.MovieLandscapeYAMJ = Me.chkLandscapeYAMJ.Checked
            .MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
            .MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
            .MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

            '****************** NMJ settings *****************
            .MovieUseNMJ = Me.chkMovieUseNMJ.Checked
            '.MovieActorThumbsNMJ = Me.chkActorThumbsNMJ.Checked
            .MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
            '.MovieClearArtNMJ = Me.chkClearArtNMJ.Checked
            '.MovieClearLogoNMJ = Me.chkClearLogoNMJ.Checked
            '.MovieDiscArtNMJ = Me.chkDiscArtNMJ.Checked
            '.MovieExtrafanartNMJ = Me.chkExtrafanartNMJ.Checked
            '.MovieExtrathumbsNMJ = Me.chkExtrathumbsNMJ.Checked
            .MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
            '.MovieLandscapeNMJ = Me.chkLandscapeNMJ.Checked
            .MovieNFONMJ = Me.chkMovieNFONMJ.Checked
            .MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
            .MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

            '************** NMJ optional settings *************
            .MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
            .MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

            '***************** Boxee settings *****************
            .MovieUseBoxee = Me.chkMovieUseBoxee.Checked
            '.MovieActorThumbsBoxee = Me.chkActorThumbsBoxee.Checked
            '.MovieBannerBoxee = Me.chkMovieBannerBoxee.Checked
            '.MovieClearArtBoxee = Me.chkClearArtBoxee.Checked
            '.MovieClearLogoBoxee = Me.chkClearLogoBoxee.Checked
            '.MovieDiscArtBoxee = Me.chkDiscArtBoxee.Checked
            '.MovieExtrafanartBoxee = Me.chkExtrafanartBoxee.Checked
            '.MovieExtrathumbsBoxee = Me.chkExtrathumbsBoxee.Checked
            .MovieFanartBoxee = Me.chkMovieFanartBoxee.Checked
            '.MovieLandscapeBoxee = Me.chkLandscapeBoxee.Checked
            .MovieNFOBoxee = Me.chkMovieNFOBoxee.Checked
            .MoviePosterBoxee = Me.chkMoviePosterBoxee.Checked
            '.MovieTrailerBoxee = Me.chkMovieTrailerBoxee.Checked

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
            .TVSeasonLandscapeXBMC = Me.chkTVSeasonLandscapeXBMC.Checked
            .TVShowCharacterArtXBMC = Me.chkTVShowCharacterArtXBMC.Checked
            .TVShowClearArtXBMC = Me.chkTVShowClearArtXBMC.Checked
            .TVShowClearLogoXBMC = Me.chkTVShowClearLogoXBMC.Checked
            .TVShowLandscapeXBMC = Me.chkTVShowLandscapeXBMC.Checked
            .TVShowTVThemeXBMC = Me.chkTVShowTVThemeXBMC.Checked
            .TVShowTVThemeFolderXBMC = Me.txtTVShowTVThemeFolderXBMC.Text

            '****************** YAMJ settings ******************

            '****************** NMJ settings *******************

            '************** NMT optional settings **************

            '***************** Expert settings *****************

        End With

    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(402, "Ember Startup Wizard")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnBack.Text = Master.eLang.GetString(403, "< Back")
        Me.btnNext.Text = Master.eLang.GetString(404, "Next >")
        Me.Label1.Text = Master.eLang.GetString(405, "Welcome to Ember Media Manager")
        Me.btnMovieRem.Text = Master.eLang.GetString(30, "Remove")
        Me.btnTVRemoveSource.Text = Me.btnMovieRem.Text
        Me.btnMovieAddFolder.Text = Master.eLang.GetString(407, "Add Source")
        Me.btnTVAddSource.Text = Me.btnMovieAddFolder.Text
        Me.Label6.Text = Master.eLang.GetString(408, "That wasn't so hard was it?  As mentioned earlier, you can change these or any other options in the Settings dialog.")
        Me.Label7.Text = String.Format(Master.eLang.GetString(409, "That's it!{0}Ember Media Manager is Ready!"), vbNewLine)
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colPath.Text = Master.eLang.GetString(410, "Path")
        Me.colRecur.Text = Master.eLang.GetString(411, "Recursive")
        Me.colFolder.Text = Master.eLang.GetString(412, "Use Folder Name")
        Me.colSingle.Text = Master.eLang.GetString(413, "Single Video")
        Me.Label2.Text = String.Format(Master.eLang.GetString(415, "This is either your first time running Ember Media Manager or you have upgraded to a newer version.  There are a few things Ember Media Manager needs to know to work properly.  This wizard will walk you through configuring Ember Media Manager to work for your set up.{0}{0}Only a handful of settings will be covered in this wizard. You can change these or any other setting at any time by selecting ""Settings..."" from the ""Edit"" menu."), vbNewLine)
        Me.Label4.Text = Master.eLang.GetString(416, "Now that Ember Media Manager knows WHERE to look for the files, we need to tell it WHAT files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Me.Label3.Text = Master.eLang.GetString(414, "First, let's tell Ember Media Manager where to locate all your movies. You can add as many sources as you wish.")
        Me.Label8.Text = String.Format(Master.eLang.GetString(417, "Some options you may be interested in:{0}{0}Custom Filters - If your movie files have things like ""DVDRip"", ""BluRay"", ""x264"", etc in their folder or file name and you wish to filter the names when loading into the media list, you can utilize the Custom Filter option.  The custom filter is RegEx compatible for maximum usability.{0}{0}Images - This section allows you to select which websites to ""scrape"" images from as well as select a preferred size for the images Ember Media Manager selects.{0}{0}Locks - This section allows you to ""lock"" certain information so it does not get updated even if you re-scrape the movie. This is useful if you manually edit the title, outline, or plot of a movie and wish to keep your changes."), vbNewLine)
        Me.Label32.Text = Master.eLang.GetString(430, "Interface Language")
        Me.Label9.Text = Master.eLang.GetString(803, "Next, let's tell Ember Media Manager where to locate all your TV Shows. You can add as many sources as you wish.")
        Me.Label11.Text = Master.eLang.GetString(804, "And finally, let's tell Ember Media Manager what TV Show files to look for.  Simply select any combination of files type you wish Ember Media Manager to load from and save to.  You can select more than one from each section if you wish.")
        Me.Label10.Text = Master.eLang.GetString(113, "Now select the default language you would like Ember to look for when scraping TV Show items.")
        Me.btnTVLanguageFetch.Text = Master.eLang.GetString(742, "Fetch Available Languages")
        Me.pnlWelcome.Location = New Point(166, 7)
        Me.pnlWelcome.Visible = True
        Me.pnlMovieSource.Visible = False
        Me.pnlMovieSettings.Visible = False
        Me.pnlTVShowSource.Visible = False
        Me.pnlTVShowSettings.Visible = False
        Me.pnlDone.Visible = False
        Me.pnlMovieSource.Location = New Point(166, 7)
        Me.pnlMovieSettings.Location = New Point(166, 7)
        Me.pnlTVShowSource.Location = New Point(166, 7)
        Me.pnlTVShowSettings.Location = New Point(166, 7)
        Me.pnlDone.Location = New Point(166, 7)
    End Sub

#End Region 'Methods

End Class