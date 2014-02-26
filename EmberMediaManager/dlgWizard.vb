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

    Private Sub chkMovieYAMJWatchedFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMovieYAMJWatchedFile.CheckedChanged
        Me.txtMovieYAMJWatchedFolder.Enabled = Me.chkMovieYAMJWatchedFile.Checked
        Me.btnMovieBrowseWatchedFiles.Enabled = Me.chkMovieYAMJWatchedFile.Checked
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

        '*************** XBMC Frodo settings ***************
        Me.chkMovieUseFrodo.Checked = Master.eSettings.MovieUseFrodo
        Me.chkMovieActorThumbsFrodo.Checked = Master.eSettings.MovieActorThumbsFrodo
        Me.chkMovieBannerFrodo.Checked = Master.eSettings.MovieBannerFrodo
        Me.chkMovieClearArtFrodo.Checked = Master.eSettings.MovieClearArtFrodo
        Me.chkMovieClearLogoFrodo.Checked = Master.eSettings.MovieClearLogoFrodo
        Me.chkMovieDiscArtFrodo.Checked = Master.eSettings.MovieDiscArtFrodo
        Me.chkMovieExtrafanartsFrodo.Checked = Master.eSettings.MovieExtrafanartsFrodo
        Me.chkMovieExtrathumbsFrodo.Checked = Master.eSettings.MovieExtrathumbsFrodo
        Me.chkMovieFanartFrodo.Checked = Master.eSettings.MovieFanartFrodo
        Me.chkMovieLandscapeFrodo.Checked = Master.eSettings.MovieLandscapeFrodo
        Me.chkMovieNFOFrodo.Checked = Master.eSettings.MovieNFOFrodo
        Me.chkMoviePosterFrodo.Checked = Master.eSettings.MoviePosterFrodo
        Me.chkMovieTrailerFrodo.Checked = Master.eSettings.MovieTrailerFrodo

        '*************** XBMC Eden settings ***************
        Me.chkMovieUseEden.Checked = Master.eSettings.MovieUseEden
        Me.chkMovieActorThumbsEden.Checked = Master.eSettings.MovieActorThumbsEden
        'Me.chkBannerEden.Checked = Master.eSettings.MovieBannerEden
        'Me.chkClearArtEden.Checked = Master.eSettings.MovieClearArtEden
        'Me.chkClearLogoEden.Checked = Master.eSettings.MovieClearLogoEden
        'Me.chkDiscArtEden.Checked = Master.eSettings.MovieDiscArtEden
        Me.chkMovieExtrafanartsEden.Checked = Master.eSettings.MovieExtrafanartsEden
        Me.chkMovieExtrathumbsEden.Checked = Master.eSettings.MovieExtrathumbsEden
        Me.chkMovieFanartEden.Checked = Master.eSettings.MovieFanartEden
        'Me.chkLandscapeEden.Checked = Master.eSettings.MovieLandscapeEden
        Me.chkMovieNFOEden.Checked = Master.eSettings.MovieNFOEden
        Me.chkMoviePosterEden.Checked = Master.eSettings.MoviePosterEden
        Me.chkMovieTrailerEden.Checked = Master.eSettings.MovieTrailerEden

        '************* XBMC optional settings *************
        Me.chkMovieXBMCTrailerFormat.Checked = Master.eSettings.MovieXBMCTrailerFormat
        Me.chkMovieXBMCProtectVTSBDMV.Checked = Master.eSettings.MovieXBMCProtectVTSBDMV

        '****************** YAMJ settings *****************
        Me.chkMovieUseYAMJ.Checked = Master.eSettings.MovieUseYAMJ
        'Me.chkActorThumbsYAMJ.Checked = Master.eSettings.MovieActorThumbsYAMJ
        Me.chkMovieBannerYAMJ.Checked = Master.eSettings.MovieBannerYAMJ
        'Me.chkClearArtYAMJ.Checked = Master.eSettings.MovieClearArtYAMJ
        'Me.chkClearLogoYAMJ.Checked = Master.eSettings.MovieClearLogoYAMJ
        'Me.chkDiscArtYAMJ.Checked = Master.eSettings.MovieDiscArtYAMJ
        'Me.chkExtrafanartYAMJ.Checked = Master.eSettings.MovieExtrafanartYAMJ
        'Me.chkExtrathumbsYAMJ.Checked = Master.eSettings.MovieExtrathumbsYAMJ
        Me.chkMovieFanartYAMJ.Checked = Master.eSettings.MovieFanartYAMJ
        'Me.chkLandscapeYAMJ.Checked = Master.eSettings.MovieLandscapeYAMJ
        Me.chkMovieNFOYAMJ.Checked = Master.eSettings.MovieNFOYAMJ
        Me.chkMoviePosterYAMJ.Checked = Master.eSettings.MoviePosterYAMJ
        Me.chkMovieTrailerYAMJ.Checked = Master.eSettings.MovieTrailerYAMJ

        '****************** NMJ settings ******************
        Me.chkMovieUseNMJ.Checked = Master.eSettings.MovieUseNMJ
        'Me.chkActorThumbsNMJ.Checked = Master.eSettings.MovieActorThumbsNMJ
        Me.chkMovieBannerNMJ.Checked = Master.eSettings.MovieBannerNMJ
        'Me.chkClearArtNMJ.Checked = Master.eSettings.MovieClearArtNMJ
        'Me.chkClearLogoNMJ.Checked = Master.eSettings.MovieClearLogoNMJ
        'Me.chkDiscArtNMJ.Checked = Master.eSettings.MovieDiscArtNMJ
        'Me.chkExtrafanartNMJ.Checked = Master.eSettings.MovieExtrafanartNMJ
        'Me.chkExtrathumbsNMJ.Checked = Master.eSettings.MovieExtrathumbsNMJ
        Me.chkMovieFanartNMJ.Checked = Master.eSettings.MovieFanartNMJ
        'Me.chkLandscapeNMJ.Checked = Master.eSettings.MovieLandscapeNMJ
        Me.chkMovieNFONMJ.Checked = Master.eSettings.MovieNFONMJ
        Me.chkMoviePosterNMJ.Checked = Master.eSettings.MoviePosterNMJ
        Me.chkMovieTrailerNMJ.Checked = Master.eSettings.MovieTrailerNMJ

        '************** NMJ optional settings *************
        Me.chkMovieYAMJWatchedFile.Checked = Master.eSettings.MovieYAMJWatchedFile
        Me.txtMovieYAMJWatchedFolder.Text = Master.eSettings.MovieYAMJWatchedFolder

        '***************** Expert settings ****************
        Me.chkMovieUseExpert.Checked = Master.eSettings.MovieUseExpert

        '***************** Expert Single ******************
        Me.chkMovieActorThumbsExpertSingle.Checked = Master.eSettings.MovieActorThumbsExpertSingle
        Me.txtMovieActorThumbsExtExpertSingle.Text = Master.eSettings.MovieActorThumbsExtExpertSingle
        Me.txtMovieBannerExpertSingle.Text = Master.eSettings.MovieBannerExpertSingle
        Me.txtMovieClearArtExpertSingle.Text = Master.eSettings.MovieClearArtExpertSingle
        Me.txtMovieClearLogoExpertSingle.Text = Master.eSettings.MovieClearLogoExpertSingle
        Me.txtMovieDiscArtExpertSingle.Text = Master.eSettings.MovieDiscArtExpertSingle
        Me.chkMovieExtrafanartsExpertSingle.Checked = Master.eSettings.MovieExtrafanartsExpertSingle
        Me.chkMovieExtrathumbsExpertSingle.Checked = Master.eSettings.MovieExtrathumbsExpertSingle
        Me.txtMovieFanartExpertSingle.Text = Master.eSettings.MovieFanartExpertSingle
        Me.txtMovieLandscapeExpertSingle.Text = Master.eSettings.MovieLandscapeExpertSingle
        Me.txtMovieNFOExpertSingle.Text = Master.eSettings.MovieNFOExpertSingle
        Me.txtMoviePosterExpertSingle.Text = Master.eSettings.MoviePosterExpertSingle
        Me.chkMovieStackExpertSingle.Checked = Master.eSettings.MovieStackExpertSingle
        Me.txtMovieTrailerExpertSingle.Text = Master.eSettings.MovieTrailerExpertSingle
        Me.chkMovieUnstackExpertSingle.Checked = Master.eSettings.MovieUnstackExpertSingle

        '***************** Expert Multi ******************
        Me.chkMovieActorThumbsExpertMulti.Checked = Master.eSettings.MovieActorThumbsExpertMulti
        Me.txtMovieActorThumbsExtExpertMulti.Text = Master.eSettings.MovieActorThumbsExtExpertMulti
        Me.txtMovieBannerExpertMulti.Text = Master.eSettings.MovieBannerExpertMulti
        Me.txtMovieClearArtExpertMulti.Text = Master.eSettings.MovieClearArtExpertMulti
        Me.txtMovieClearLogoExpertMulti.Text = Master.eSettings.MovieClearLogoExpertMulti
        Me.txtMovieDiscArtExpertMulti.Text = Master.eSettings.MovieDiscArtExpertMulti
        Me.txtMovieFanartExpertMulti.Text = Master.eSettings.MovieFanartExpertMulti
        Me.txtMovieLandscapeExpertMulti.Text = Master.eSettings.MovieLandscapeExpertMulti
        Me.txtMovieNFOExpertMulti.Text = Master.eSettings.MovieNFOExpertMulti
        Me.txtMoviePosterExpertMulti.Text = Master.eSettings.MoviePosterExpertMulti
        Me.chkMovieStackExpertMulti.Checked = Master.eSettings.MovieStackExpertMulti
        Me.txtMovieTrailerExpertMulti.Text = Master.eSettings.MovieTrailerExpertMulti
        Me.chkMovieUnstackExpertMulti.Checked = Master.eSettings.MovieUnstackExpertMulti

        '***************** Expert VTS ******************
        Me.chkMovieActorThumbsExpertVTS.Checked = Master.eSettings.MovieActorThumbsExpertVTS
        Me.txtMovieActorThumbsExtExpertVTS.Text = Master.eSettings.MovieActorThumbsExtExpertVTS
        Me.txtMovieBannerExpertVTS.Text = Master.eSettings.MovieBannerExpertVTS
        Me.txtMovieClearArtExpertVTS.Text = Master.eSettings.MovieClearArtExpertVTS
        Me.txtMovieClearLogoExpertVTS.Text = Master.eSettings.MovieClearLogoExpertVTS
        Me.txtMovieDiscArtExpertVTS.Text = Master.eSettings.MovieDiscArtExpertVTS
        Me.chkMovieExtrafanartsExpertVTS.Checked = Master.eSettings.MovieExtrafanartsExpertVTS
        Me.chkMovieExtrathumbsExpertVTS.Checked = Master.eSettings.MovieExtrathumbsExpertVTS
        Me.txtMovieFanartExpertVTS.Text = Master.eSettings.MovieFanartExpertVTS
        Me.txtMovieLandscapeExpertVTS.Text = Master.eSettings.MovieLandscapeExpertVTS
        Me.txtMovieNFOExpertVTS.Text = Master.eSettings.MovieNFOExpertVTS
        Me.txtMoviePosterExpertVTS.Text = Master.eSettings.MoviePosterExpertVTS
        Me.chkMovieRecognizeVTSExpertVTS.Checked = Master.eSettings.MovieRecognizeVTSExpertVTS
        Me.txtMovieTrailerExpertVTS.Text = Master.eSettings.MovieTrailerExpertVTS
        Me.chkMovieUseBaseDirectoryExpertVTS.Checked = Master.eSettings.MovieUseBaseDirectoryExpertVTS

        '***************** Expert BDMV ******************
        Me.chkMovieActorThumbsExpertBDMV.Checked = Master.eSettings.MovieActorThumbsExpertBDMV
        Me.txtMovieActorThumbsExtExpertBDMV.Text = Master.eSettings.MovieActorThumbsExtExpertBDMV
        Me.txtMovieBannerExpertBDMV.Text = Master.eSettings.MovieBannerExpertBDMV
        Me.txtMovieClearArtExpertBDMV.Text = Master.eSettings.MovieClearArtExpertBDMV
        Me.txtMovieClearLogoExpertBDMV.Text = Master.eSettings.MovieClearLogoExpertBDMV
        Me.txtMovieDiscArtExpertBDMV.Text = Master.eSettings.MovieDiscArtExpertBDMV
        Me.chkMovieExtrafanartsExpertBDMV.Checked = Master.eSettings.MovieExtrafanartsExpertBDMV
        Me.chkMovieExtrathumbsExpertBDMV.Checked = Master.eSettings.MovieExtrathumbsExpertBDMV
        Me.txtMovieFanartExpertBDMV.Text = Master.eSettings.MovieFanartExpertBDMV
        Me.txtMovieLandscapeExpertBDMV.Text = Master.eSettings.MovieLandscapeExpertBDMV
        Me.txtMovieNFOExpertBDMV.Text = Master.eSettings.MovieNFOExpertBDMV
        Me.txtMoviePosterExpertBDMV.Text = Master.eSettings.MoviePosterExpertBDMV
        Me.txtMovieTrailerExpertBDMV.Text = Master.eSettings.MovieTrailerExpertBDMV
        Me.chkMovieUseBaseDirectoryExpertBDMV.Checked = Master.eSettings.MovieUseBaseDirectoryExpertBDMV

        Me.chkSeasonAllPosterJPG.Checked = Master.eSettings.SeasonAllPosterJPG
        Me.chkSeasonXXDashPosterJPG.Checked = Master.eSettings.SeasonXXDashPosterJPG
        Me.chkSeasonXXDashFanartJPG.Checked = Master.eSettings.SeasonXXDashFanartJPG

        tLang = Master.eSettings.Language
        Me.cbIntLang.SelectedItem = Master.eSettings.Language
        Me.chkSeasonAllTBN.Checked = Master.eSettings.SeasonAllTBN
        Me.chkSeasonAllJPG.Checked = Master.eSettings.SeasonAllJPG
        Me.chkShowTBN.Checked = Master.eSettings.ShowTBN
        Me.chkShowJPG.Checked = Master.eSettings.ShowJPG
        Me.chkShowFolderJPG.Checked = Master.eSettings.ShowFolderJPG
        Me.chkShowPosterTBN.Checked = Master.eSettings.ShowPosterTBN
        Me.chkShowPosterJPG.Checked = Master.eSettings.ShowPosterJPG
        Me.chkShowFanartJPG.Checked = Master.eSettings.ShowFanartJPG
        Me.chkShowDashFanart.Checked = Master.eSettings.ShowDashFanart
        Me.chkShowDotFanart.Checked = Master.eSettings.ShowDotFanart
        Me.chkSeasonXXTBN.Checked = Master.eSettings.SeasonXX
        Me.chkSeasonXTBN.Checked = Master.eSettings.SeasonX
        Me.chkSeasonPosterTBN.Checked = Master.eSettings.SeasonPosterTBN
        Me.chkSeasonPosterJPG.Checked = Master.eSettings.SeasonPosterJPG
        Me.chkSeasonNameTBN.Checked = Master.eSettings.SeasonNameTBN
        Me.chkSeasonNameJPG.Checked = Master.eSettings.SeasonNameJPG
        Me.chkSeasonFolderJPG.Checked = Master.eSettings.SeasonFolderJPG
        Me.chkSeasonFanartJPG.Checked = Master.eSettings.SeasonFanartJPG
        Me.chkSeasonDashFanart.Checked = Master.eSettings.SeasonDashFanart
        Me.chkSeasonDotFanart.Checked = Master.eSettings.SeasonDotFanart
        Me.chkEpisodeTBN.Checked = Master.eSettings.EpisodeTBN
        Me.chkEpisodeJPG.Checked = Master.eSettings.EpisodeJPG
        Me.chkEpisodeDashThumbJPG.Checked = Master.eSettings.EpisodeDashThumbJPG
        Me.chkEpisodeDashFanart.Checked = Master.eSettings.EpisodeDashFanart
        Me.chkEpisodeDotFanart.Checked = Master.eSettings.EpisodeDotFanart
        'Me.tLangList.AddRange(Master.eSettings.TVDBLanguages)
        Me.cbTVLanguage.Items.AddRange((From lLang In Master.eSettings.Languages Select lLang.LongLang).ToArray)
        If Me.cbTVLanguage.Items.Count > 0 Then
            Me.cbTVLanguage.Text = Master.eSettings.Languages.FirstOrDefault(Function(l) l.ShortLang = AdvancedSettings.GetSetting("TVDBLanguage", "en")).LongLang
        End If
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

        '*************** XBMC Frodo settings ***************
        Master.eSettings.MovieUseFrodo = Me.chkMovieUseFrodo.Checked
        Master.eSettings.MovieActorThumbsFrodo = Me.chkMovieActorThumbsFrodo.Checked
        Master.eSettings.MovieBannerFrodo = Me.chkMovieBannerFrodo.Checked
        Master.eSettings.MovieClearArtFrodo = Me.chkMovieClearArtFrodo.Checked
        Master.eSettings.MovieClearLogoFrodo = Me.chkMovieClearLogoFrodo.Checked
        Master.eSettings.MovieDiscArtFrodo = Me.chkMovieDiscArtFrodo.Checked
        Master.eSettings.MovieExtrafanartsFrodo = Me.chkMovieExtrafanartsFrodo.Checked
        Master.eSettings.MovieExtrathumbsFrodo = Me.chkMovieExtrathumbsFrodo.Checked
        Master.eSettings.MovieFanartFrodo = Me.chkMovieFanartFrodo.Checked
        Master.eSettings.MovieLandscapeFrodo = Me.chkMovieLandscapeFrodo.Checked
        Master.eSettings.MovieNFOFrodo = Me.chkMovieNFOFrodo.Checked
        Master.eSettings.MoviePosterFrodo = Me.chkMoviePosterFrodo.Checked
        Master.eSettings.MovieTrailerFrodo = Me.chkMovieTrailerFrodo.Checked

        '*************** XBMC Eden settings ***************
        Master.eSettings.MovieUseEden = Me.chkMovieUseEden.Checked
        Master.eSettings.MovieActorThumbsEden = Me.chkMovieActorThumbsEden.Checked
        'Master.eSettings.MovieBannerEden = Me.chkBannerEden.Checked
        'Master.eSettings.MovieClearArtEden = Me.chkClearArtEden.Checked
        'Master.eSettings.MovieClearLogoEden = Me.chkClearLogoEden.Checked
        'Master.eSettings.MovieDiscArtEden = Me.chkDiscArtEden.Checked
        Master.eSettings.MovieExtrafanartsEden = Me.chkMovieExtrafanartsEden.Checked
        Master.eSettings.MovieExtrathumbsEden = Me.chkMovieExtrathumbsEden.Checked
        Master.eSettings.MovieFanartEden = Me.chkMovieFanartEden.Checked
        'Master.eSettings.MovieLandscapeEden = Me.chkLandscapeEden.Checked
        Master.eSettings.MovieNFOEden = Me.chkMovieNFOEden.Checked
        Master.eSettings.MoviePosterEden = Me.chkMoviePosterEden.Checked
        Master.eSettings.MovieTrailerEden = Me.chkMovieTrailerEden.Checked

        '************* XBMC optional settings *************
        Master.eSettings.MovieXBMCTrailerFormat = Me.chkMovieXBMCTrailerFormat.Checked
        Master.eSettings.MovieXBMCProtectVTSBDMV = Me.chkMovieXBMCProtectVTSBDMV.Checked

        '****************** YAMJ settings *****************
        Master.eSettings.MovieUseYAMJ = Me.chkMovieUseYAMJ.Checked
        'Master.eSettings.MovieActorThumbsYAMJ = Me.chkActorThumbsYAMJ.Checked
        Master.eSettings.MovieBannerYAMJ = Me.chkMovieBannerYAMJ.Checked
        'Master.eSettings.MovieClearArtYAMJ = Me.chkClearArtYAMJ.Checked
        'Master.eSettings.MovieClearLogoYAMJ = Me.chkClearLogoYAMJ.Checked
        'Master.eSettings.MovieDiscArtYAMJ = Me.chkDiscArtYAMJ.Checked
        'Master.eSettings.MovieExtrafanartYAMJ = Me.chkExtrafanartYAMJ.Checked
        'Master.eSettings.MovieExtrathumbsYAMJ = Me.chkExtrathumbsYAMJ.Checked
        Master.eSettings.MovieFanartYAMJ = Me.chkMovieFanartYAMJ.Checked
        'Master.eSettings.MovieLandscapeYAMJ = Me.chkLandscapeYAMJ.Checked
        Master.eSettings.MovieNFOYAMJ = Me.chkMovieNFOYAMJ.Checked
        Master.eSettings.MoviePosterYAMJ = Me.chkMoviePosterYAMJ.Checked
        Master.eSettings.MovieTrailerYAMJ = Me.chkMovieTrailerYAMJ.Checked

        '****************** NMJ settings *****************
        Master.eSettings.MovieUseNMJ = Me.chkMovieUseNMJ.Checked
        'Master.eSettings.MovieActorThumbsNMJ = Me.chkActorThumbsNMJ.Checked
        Master.eSettings.MovieBannerNMJ = Me.chkMovieBannerNMJ.Checked
        'Master.eSettings.MovieClearArtNMJ = Me.chkClearArtNMJ.Checked
        'Master.eSettings.MovieClearLogoNMJ = Me.chkClearLogoNMJ.Checked
        'Master.eSettings.MovieDiscArtNMJ = Me.chkDiscArtNMJ.Checked
        'Master.eSettings.MovieExtrafanartNMJ = Me.chkExtrafanartNMJ.Checked
        'Master.eSettings.MovieExtrathumbsNMJ = Me.chkExtrathumbsNMJ.Checked
        Master.eSettings.MovieFanartNMJ = Me.chkMovieFanartNMJ.Checked
        'Master.eSettings.MovieLandscapeNMJ = Me.chkLandscapeNMJ.Checked
        Master.eSettings.MovieNFONMJ = Me.chkMovieNFONMJ.Checked
        Master.eSettings.MoviePosterNMJ = Me.chkMoviePosterNMJ.Checked
        Master.eSettings.MovieTrailerNMJ = Me.chkMovieTrailerNMJ.Checked

        '************** NMJ optional settings *************
        Master.eSettings.MovieYAMJWatchedFile = Me.chkMovieYAMJWatchedFile.Checked
        Master.eSettings.MovieYAMJWatchedFolder = Me.txtMovieYAMJWatchedFolder.Text

        '***************** Expert settings ****************
        Master.eSettings.MovieUseExpert = Me.chkMovieUseExpert.Checked

        '***************** Expert Single ******************
        Master.eSettings.MovieActorThumbsExpertSingle = Me.chkMovieActorThumbsExpertSingle.Checked
        Master.eSettings.MovieActorThumbsExtExpertSingle = Me.txtMovieActorThumbsExtExpertSingle.Text
        Master.eSettings.MovieBannerExpertSingle = Me.txtMovieBannerExpertSingle.Text
        Master.eSettings.MovieClearArtExpertSingle = Me.txtMovieClearArtExpertSingle.Text
        Master.eSettings.MovieClearLogoExpertSingle = Me.txtMovieClearLogoExpertSingle.Text
        Master.eSettings.MovieDiscArtExpertSingle = Me.txtMovieDiscArtExpertSingle.Text
        Master.eSettings.MovieExtrafanartsExpertSingle = Me.chkMovieExtrafanartsExpertSingle.Checked
        Master.eSettings.MovieExtrathumbsExpertSingle = Me.chkMovieExtrathumbsExpertSingle.Checked
        Master.eSettings.MovieFanartExpertSingle = Me.txtMovieFanartExpertSingle.Text
        Master.eSettings.MovieLandscapeExpertSingle = Me.txtMovieLandscapeExpertSingle.Text
        Master.eSettings.MovieNFOExpertSingle = Me.txtMovieNFOExpertSingle.Text
        Master.eSettings.MoviePosterExpertSingle = Me.txtMoviePosterExpertSingle.Text
        Master.eSettings.MovieStackExpertSingle = Me.chkMovieStackExpertSingle.Checked
        Master.eSettings.MovieTrailerExpertSingle = Me.txtMovieTrailerExpertSingle.Text
        Master.eSettings.MovieUnstackExpertSingle = Me.chkMovieUnstackExpertSingle.Checked

        '***************** Expert Multi ******************
        Master.eSettings.MovieActorThumbsExpertMulti = Me.chkMovieActorThumbsExpertMulti.Checked
        Master.eSettings.MovieActorThumbsExtExpertMulti = Me.txtMovieActorThumbsExtExpertMulti.Text
        Master.eSettings.MovieBannerExpertMulti = Me.txtMovieBannerExpertMulti.Text
        Master.eSettings.MovieClearArtExpertMulti = Me.txtMovieClearArtExpertMulti.Text
        Master.eSettings.MovieClearLogoExpertMulti = Me.txtMovieClearLogoExpertMulti.Text
        Master.eSettings.MovieDiscArtExpertMulti = Me.txtMovieDiscArtExpertMulti.Text
        Master.eSettings.MovieFanartExpertMulti = Me.txtMovieFanartExpertMulti.Text
        Master.eSettings.MovieLandscapeExpertMulti = Me.txtMovieLandscapeExpertMulti.Text
        Master.eSettings.MovieNFOExpertMulti = Me.txtMovieNFOExpertMulti.Text
        Master.eSettings.MoviePosterExpertMulti = Me.txtMoviePosterExpertMulti.Text
        Master.eSettings.MovieStackExpertMulti = Me.chkMovieStackExpertMulti.Checked
        Master.eSettings.MovieTrailerExpertMulti = Me.txtMovieTrailerExpertMulti.Text
        Master.eSettings.MovieUnstackExpertMulti = Me.chkMovieUnstackExpertMulti.Checked

        '***************** Expert VTS ******************
        Master.eSettings.MovieActorThumbsExpertVTS = Me.chkMovieActorThumbsExpertVTS.Checked
        Master.eSettings.MovieActorThumbsExtExpertVTS = Me.txtMovieActorThumbsExtExpertVTS.Text
        Master.eSettings.MovieBannerExpertVTS = Me.txtMovieBannerExpertVTS.Text
        Master.eSettings.MovieClearArtExpertVTS = Me.txtMovieClearArtExpertVTS.Text
        Master.eSettings.MovieClearLogoExpertVTS = Me.txtMovieClearLogoExpertVTS.Text
        Master.eSettings.MovieDiscArtExpertVTS = Me.txtMovieDiscArtExpertVTS.Text
        Master.eSettings.MovieExtrafanartsExpertVTS = Me.chkMovieExtrafanartsExpertVTS.Checked
        Master.eSettings.MovieExtrathumbsExpertVTS = Me.chkMovieExtrathumbsExpertVTS.Checked
        Master.eSettings.MovieFanartExpertVTS = Me.txtMovieFanartExpertVTS.Text
        Master.eSettings.MovieLandscapeExpertVTS = Me.txtMovieLandscapeExpertVTS.Text
        Master.eSettings.MovieNFOExpertVTS = Me.txtMovieNFOExpertVTS.Text
        Master.eSettings.MoviePosterExpertVTS = Me.txtMoviePosterExpertVTS.Text
        Master.eSettings.MovieRecognizeVTSExpertVTS = Me.chkMovieRecognizeVTSExpertVTS.Checked
        Master.eSettings.MovieTrailerExpertVTS = Me.txtMovieTrailerExpertVTS.Text
        Master.eSettings.MovieUseBaseDirectoryExpertVTS = Me.chkMovieUseBaseDirectoryExpertVTS.Checked

        '***************** Expert BDMV ******************
        Master.eSettings.MovieActorThumbsExpertBDMV = Me.chkMovieActorThumbsExpertBDMV.Checked
        Master.eSettings.MovieActorThumbsExtExpertBDMV = Me.txtMovieActorThumbsExtExpertBDMV.Text
        Master.eSettings.MovieBannerExpertBDMV = Me.txtMovieBannerExpertBDMV.Text
        Master.eSettings.MovieClearArtExpertBDMV = Me.txtMovieClearArtExpertBDMV.Text
        Master.eSettings.MovieClearLogoExpertBDMV = Me.txtMovieClearLogoExpertBDMV.Text
        Master.eSettings.MovieDiscArtExpertBDMV = Me.txtMovieDiscArtExpertBDMV.Text
        Master.eSettings.MovieExtrafanartsExpertBDMV = Me.chkMovieExtrafanartsExpertBDMV.Checked
        Master.eSettings.MovieExtrathumbsExpertBDMV = Me.chkMovieExtrathumbsExpertBDMV.Checked
        Master.eSettings.MovieFanartExpertBDMV = Me.txtMovieFanartExpertBDMV.Text
        Master.eSettings.MovieLandscapeExpertBDMV = Me.txtMovieLandscapeExpertBDMV.Text
        Master.eSettings.MovieNFOExpertBDMV = Me.txtMovieNFOExpertBDMV.Text
        Master.eSettings.MoviePosterExpertBDMV = Me.txtMoviePosterExpertBDMV.Text
        Master.eSettings.MovieTrailerExpertBDMV = Me.txtMovieTrailerExpertBDMV.Text
        Master.eSettings.MovieUseBaseDirectoryExpertBDMV = Me.chkMovieUseBaseDirectoryExpertBDMV.Checked

        Master.eSettings.SeasonAllPosterJPG = Me.chkSeasonAllPosterJPG.Checked
        Master.eSettings.SeasonXXDashPosterJPG = Me.chkSeasonXXDashPosterJPG.Checked
        Master.eSettings.SeasonXXDashFanartJPG = Me.chkSeasonXXDashFanartJPG.Checked
        Master.eSettings.Language = Me.cbIntLang.Text
        Master.eSettings.SeasonAllTBN = Me.chkSeasonAllTBN.Checked
        Master.eSettings.SeasonAllJPG = Me.chkSeasonAllJPG.Checked
        Master.eSettings.ShowTBN = Me.chkShowTBN.Checked
        Master.eSettings.ShowJPG = Me.chkShowJPG.Checked
        Master.eSettings.ShowFolderJPG = Me.chkShowFolderJPG.Checked
        Master.eSettings.ShowPosterTBN = Me.chkShowPosterTBN.Checked
        Master.eSettings.ShowPosterJPG = Me.chkShowPosterJPG.Checked
        Master.eSettings.ShowFanartJPG = Me.chkShowFanartJPG.Checked
        Master.eSettings.ShowDashFanart = Me.chkShowDashFanart.Checked
        Master.eSettings.ShowDotFanart = Me.chkShowDotFanart.Checked
        Master.eSettings.SeasonXX = Me.chkSeasonXXTBN.Checked
        Master.eSettings.SeasonX = Me.chkSeasonXTBN.Checked
        Master.eSettings.SeasonPosterTBN = Me.chkSeasonPosterTBN.Checked
        Master.eSettings.SeasonPosterJPG = Me.chkSeasonPosterJPG.Checked
        Master.eSettings.SeasonNameTBN = Me.chkSeasonNameTBN.Checked
        Master.eSettings.SeasonNameJPG = Me.chkSeasonNameJPG.Checked
        Master.eSettings.SeasonFolderJPG = Me.chkSeasonFolderJPG.Checked
        Master.eSettings.SeasonFanartJPG = Me.chkSeasonFanartJPG.Checked
        Master.eSettings.SeasonDashFanart = Me.chkSeasonDashFanart.Checked
        Master.eSettings.SeasonDotFanart = Me.chkSeasonDotFanart.Checked
        Master.eSettings.EpisodeTBN = Me.chkEpisodeTBN.Checked
        Master.eSettings.EpisodeJPG = Me.chkEpisodeJPG.Checked
        Master.eSettings.EpisodeDashThumbJPG = Me.chkEpisodeDashThumbJPG.Checked
        Master.eSettings.EpisodeDashFanart = Me.chkEpisodeDashFanart.Checked
        Master.eSettings.EpisodeDotFanart = Me.chkEpisodeDotFanart.Checked

        Using settings = New AdvancedSettings()
            If Master.eSettings.Languages.Count > 0 Then
                Dim tLang As String = Master.eSettings.Languages.FirstOrDefault(Function(l) l.LongLang = Me.cbTVLanguage.Text).ShortLang
                If Not String.IsNullOrEmpty(tLang) Then
                    settings.SetSetting("TVDBLanguage", tLang)
                Else
                    settings.SetSetting("TVDBLanguage", "en")
                End If
            Else
                settings.SetSetting("TVDBLanguage", "en")
            End If
            'Master.eSettings.TVDBLanguages = Me.tLangList
        End Using
    End Sub

    Private Sub SetUp()
        Me.btnTVShowFrodo.Text = Master.eLang.GetString(867, "XBMC Frodo")
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
        Me.gbShowPosters.Text = Master.eLang.GetString(683, "Show Posters")
        Me.gbShowFanart.Text = Master.eLang.GetString(684, "Show Fanart")
        Me.gbSeasonPosters.Text = Master.eLang.GetString(685, "Season Posters")
        Me.gbSeasonFanart.Text = Master.eLang.GetString(686, "Season Fanart")
        Me.gbEpisodePosters.Text = Master.eLang.GetString(687, "Episode Posters")
        Me.gbEpisodeFanart.Text = Master.eLang.GetString(688, "Episode Fanart")
        Me.lblInsideSeason.Text = Master.eLang.GetString(834, "* Inside Season directory")
        Me.gbAllSeasonPoster.Text = Master.eLang.GetString(735, "All Season Posters")
        Me.Label10.Text = Master.eLang.GetString(113, "Now select the default language you would like Ember to look for when scraping TV Show items.")
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

	Private Sub btnTVShowFrodo_Click(sender As Object, e As EventArgs) Handles btnTVShowFrodo.Click
		Me.chkEpisodeDashFanart.Checked = False
		Me.chkEpisodeDashThumbJPG.Checked = True
		Me.chkEpisodeDotFanart.Checked = False
        Me.chkEpisodeJPG.Checked = False
		Me.chkEpisodeTBN.Checked = False
		Me.chkSeasonAllJPG.Checked = False
		Me.chkSeasonAllPosterJPG.Checked = True
		Me.chkSeasonAllTBN.Checked = False
		Me.chkSeasonDashFanart.Checked = False
		Me.chkSeasonDotFanart.Checked = False
		Me.chkSeasonFanartJPG.Checked = False
		Me.chkSeasonFolderJPG.Checked = False
		Me.chkSeasonNameJPG.Checked = False
		Me.chkSeasonNameTBN.Checked = False
		Me.chkSeasonPosterJPG.Checked = False
		Me.chkSeasonPosterTBN.Checked = False
		Me.chkSeasonXTBN.Checked = False
		Me.chkSeasonXXDashFanartJPG.Checked = True
		Me.chkSeasonXXDashPosterJPG.Checked = True
		Me.chkSeasonXXTBN.Checked = False
		'Me.chkShowBannerJPG.Checked = True (banners not implemented at time)
		Me.chkShowDashFanart.Checked = False
		Me.chkShowDotFanart.Checked = False
		Me.chkShowFanartJPG.Checked = True
		Me.chkShowFolderJPG.Checked = False
		Me.chkShowJPG.Checked = False
		Me.chkShowPosterJPG.Checked = True
		Me.chkShowPosterTBN.Checked = False
		Me.chkShowTBN.Checked = False
	End Sub

#End Region	'Methods

End Class