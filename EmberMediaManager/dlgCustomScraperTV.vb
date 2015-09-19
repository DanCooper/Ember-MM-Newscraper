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

Public Class dlgCustomScraperTV

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private CustomUpdater As New Structures.CustomUpdaterStruct

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
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
        chkModCharacterArt.Checked = False
        chkModFanart.Checked = False
        chkModLandscape.Checked = False
        chkModNFO.Checked = False
        chkModPoster.Checked = False
        chkModTheme.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnOptsNone_Click(sender As Object, e As EventArgs) Handles btnOptsNone.Click
        chkOptsAll.Checked = False
        chkModAll.Checked = False

        chkOptsMainCast.Checked = False
        chkOptsMainCert.Checked = False
        chkOptsMainCountry.Checked = False
        chkOptsMainCreator.Checked = False
        chkOptsMainGenre.Checked = False
        chkOptsMainMPAA.Checked = False
        chkOptsMainOriginalTitle.Checked = False
        chkOptsMainEpisodeGuideURL.Checked = False
        chkOptsMainPlot.Checked = False
        chkOptsMainStatus.Checked = False
        chkOptsMainRating.Checked = False
        chkOptsMainRelease.Checked = False
        chkOptsMainRuntime.Checked = False
        chkOptsMainStudio.Checked = False
        chkOptsMainTitle.Checked = False

        CheckEnable()
    End Sub

    Private Sub CheckEnable()
        Me.gbOptions.Enabled = chkModAll.Checked OrElse chkModNFO.Checked

        With Master.eSettings

            If chkModAll.Checked Then
                chkModActorThumbs.Checked = chkModAll.Checked
                chkModBanner.Checked = chkModAll.Checked AndAlso .TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                chkModClearArt.Checked = chkModAll.Checked AndAlso .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                chkModClearLogo.Checked = chkModAll.Checked AndAlso .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                chkModCharacterArt.Checked = chkModAll.Checked AndAlso .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                chkModFanart.Checked = chkModAll.Checked AndAlso .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                chkModLandscape.Checked = chkModAll.Checked AndAlso .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                chkModPoster.Checked = chkModAll.Checked AndAlso .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                chkModTheme.Checked = chkModAll.Checked AndAlso .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
                chkModNFO.Checked = chkModAll.Checked
                chkOptsAll.Checked = chkModAll.Checked
            End If

            chkModActorThumbs.Enabled = Not chkModAll.Checked
            chkModBanner.Enabled = Not chkModAll.Checked AndAlso .TVShowBannerAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
            chkModClearArt.Enabled = Not chkModAll.Checked AndAlso .TVShowClearArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
            chkModClearLogo.Enabled = Not chkModAll.Checked AndAlso .TVShowClearLogoAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
            chkModCharacterArt.Enabled = Not chkModAll.Checked AndAlso .TVShowCharacterArtAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
            chkModFanart.Enabled = Not chkModAll.Checked AndAlso .TVShowFanartAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
            chkModLandscape.Enabled = Not chkModAll.Checked AndAlso .TVShowLandscapeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
            chkModNFO.Enabled = Not chkModAll.Checked
            chkModPoster.Enabled = Not chkModAll.Checked AndAlso .TVShowPosterAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
            chkModTheme.Enabled = Not chkModAll.Checked AndAlso .TvShowThemeAnyEnabled AndAlso ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV(Enums.ModifierType.MainTheme)
            chkOptsAll.Enabled = Not chkModAll.Checked

            If Me.chkModAll.Checked Then
                Me.chkOptsAll.Enabled = False
            Else
                Me.chkOptsAll.Enabled = Me.chkModNFO.Checked
            End If

            If Me.chkOptsAll.Checked Then
                chkOptsMainCast.Checked = True
                chkOptsMainCast.Enabled = False
                chkOptsMainCert.Checked = True
                chkOptsMainCert.Enabled = False
                chkOptsMainEpisodeGuideURL.Checked = True
                chkOptsMainEpisodeGuideURL.Enabled = False
                chkOptsMainCountry.Checked = True
                chkOptsMainCountry.Enabled = False
                chkOptsMainCreator.Checked = True
                chkOptsMainCreator.Enabled = False
                chkOptsMainStatus.Checked = True
                chkOptsMainStatus.Enabled = False
                chkOptsMainGenre.Checked = True
                chkOptsMainGenre.Enabled = False
                chkOptsMainMPAA.Checked = True
                chkOptsMainMPAA.Enabled = False
                chkOptsMainOriginalTitle.Checked = True
                chkOptsMainOriginalTitle.Enabled = False
                chkOptsMainPlot.Checked = True
                chkOptsMainPlot.Enabled = False
                chkOptsMainRating.Checked = True
                chkOptsMainRating.Enabled = False
                chkOptsMainRelease.Checked = True
                chkOptsMainRelease.Enabled = False
                chkOptsMainRuntime.Checked = True
                chkOptsMainRuntime.Enabled = False
                chkOptsMainStudio.Checked = True
                chkOptsMainStudio.Enabled = False
                chkOptsMainTitle.Checked = True
                chkOptsMainTitle.Enabled = False
            Else
                chkOptsMainCast.Enabled = True
                chkOptsMainCert.Enabled = True
                chkOptsMainEpisodeGuideURL.Enabled = True
                chkOptsMainCountry.Enabled = True
                chkOptsMainCreator.Enabled = True
                chkOptsMainStatus.Enabled = True
                chkOptsMainGenre.Enabled = True
                chkOptsMainMPAA.Enabled = True
                chkOptsMainOriginalTitle.Enabled = True
                chkOptsMainPlot.Enabled = True
                chkOptsMainRating.Enabled = True
                chkOptsMainRelease.Enabled = True
                chkOptsMainRuntime.Enabled = True
                chkOptsMainStudio.Enabled = True
                chkOptsMainTitle.Enabled = True
            End If

            If chkModAll.Checked OrElse chkModNFO.Checked Then
                If chkOptsMainCast.Checked OrElse chkOptsMainCreator.Checked OrElse chkOptsMainEpisodeGuideURL.Checked OrElse chkOptsMainGenre.Checked OrElse _
                chkOptsMainMPAA.Checked OrElse chkOptsMainCert.Checked OrElse chkOptsMainStatus.Checked OrElse chkOptsMainOriginalTitle.Checked OrElse _
                chkOptsMainPlot.Checked OrElse _
                chkOptsMainRelease.Checked OrElse chkOptsMainRuntime.Checked OrElse chkOptsMainStudio.Checked OrElse _
                chkOptsMainTitle.Checked OrElse _
                chkOptsMainCountry.Checked Then
                    Update_Button.Enabled = True
                Else
                    Update_Button.Enabled = False
                End If
            ElseIf chkModActorThumbs.Checked OrElse chkModBanner.Checked OrElse chkModClearArt.Checked OrElse chkModClearLogo.Checked OrElse _
                chkModCharacterArt.Checked OrElse chkModFanart.Checked OrElse _
                chkModLandscape.Checked OrElse chkModPoster.Checked OrElse chkModTheme.Checked Then
                Update_Button.Enabled = True
            Else
                Update_Button.Enabled = False
            End If

            If Me.chkModAll.Checked Then
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.All, True)
            Else
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.All, False)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainActorThumbs, chkModActorThumbs.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainBanner, chkModBanner.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainClearArt, chkModClearArt.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainClearLogo, chkModClearLogo.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainDiscArt, chkModCharacterArt.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainFanart, chkModFanart.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainFanart, chkModFanart.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainLandscape, chkModLandscape.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainNFO, chkModNFO.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainPoster, chkModPoster.Checked)
                Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.MainTheme, chkModTheme.Checked)
            End If

        End With
    End Sub

    Private Sub CheckNewAndMark()
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idShow) AS ncount FROM tvshow WHERE new = 1;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                rbUpdateModifier_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
            End Using

            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idShow) AS mcount FROM tvshow WHERE mark = 1;")
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

    Private Sub chkModCharacterArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModCharacterArt.Click
        CheckEnable()
    End Sub

    Private Sub chkModFanart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModFanart.Click
        CheckEnable()
    End Sub

    Private Sub chkModLandscape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkModLandscape.Click
        CheckEnable()
    End Sub

    Private Sub chkModMeta_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Private Sub chkOptsAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOptsAll.Click
        CheckEnable()
    End Sub

    Private Sub chkOptsMainCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainCast.CheckedChanged
        CustomUpdater.Options.bMainActors = chkOptsMainCast.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainCert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainCert.CheckedChanged
        CustomUpdater.Options.bMainCert = chkOptsMainCert.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainCreator_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainCreator.CheckedChanged
        CustomUpdater.Options.bMainCreator = chkOptsMainCreator.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainGenre.CheckedChanged
        CustomUpdater.Options.bMainGenre = chkOptsMainGenre.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainMPAA.CheckedChanged
        CustomUpdater.Options.bMainMPAA = chkOptsMainMPAA.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainOriginalTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainOriginalTitle.CheckedChanged
        CustomUpdater.Options.bMainOriginalTitle = chkOptsMainOriginalTitle.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainPlot.CheckedChanged
        CustomUpdater.Options.bMainPlot = chkOptsMainPlot.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainRating.CheckedChanged
        CustomUpdater.Options.bMainRating = chkOptsMainRating.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainRelease.CheckedChanged
        CustomUpdater.Options.bMainPremiered = chkOptsMainRelease.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainRuntime.CheckedChanged
        CustomUpdater.Options.bMainRuntime = chkOptsMainRuntime.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainStudio.CheckedChanged
        CustomUpdater.Options.bMainStudio = chkOptsMainStudio.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainTitle.CheckedChanged
        CustomUpdater.Options.bMainTitle = chkOptsMainTitle.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainCountry.CheckedChanged
        CustomUpdater.Options.bMainCountry = chkOptsMainCountry.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainEpisodeGuideURL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainEpisodeGuideURL.CheckedChanged
        CustomUpdater.Options.bMainEpisodeGuide = chkOptsMainEpisodeGuideURL.Checked
        CheckEnable()
    End Sub

    Private Sub chkOptsMainStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptsMainStatus.CheckedChanged
        CustomUpdater.Options.bMainStatus = chkOptsMainStatus.Checked
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

            'set defaults
            CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Functions.SetScrapeModifier(CustomUpdater.ScrapeModifier, Enums.ModifierType.All, True)

            Me.CheckEnable()

            'check if there are new or marked movies
            Me.CheckNewAndMark()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
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
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
        End If
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_Marked.CheckedChanged
        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
        End If
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdateModifier_Missing.CheckedChanged
        If Me.rbUpdate_Auto.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
        ElseIf Me.rbUpdate_Ask.Checked Then
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
        Else
            Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
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
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAsk
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAsk
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAsk
            Case rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAsk
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdate_Auto.CheckedChanged
        Select Case True
            Case Me.rbUpdateModifier_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllAuto
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingAuto
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewAuto
            Case Me.rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedAuto
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdate_Skip.CheckedChanged
        Select Case True
            Case Me.rbUpdateModifier_All.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.AllSkip
            Case Me.rbUpdateModifier_Missing.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MissingSkip
            Case Me.rbUpdateModifier_New.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.NewSkip
            Case Me.rbUpdateModifier_Marked.Checked
                Me.CustomUpdater.ScrapeType = Enums.ScrapeType.MarkedSkip
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
        Me.chkModCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        Me.chkModFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.chkModLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.chkModNFO.Text = Master.eLang.GetString(150, "NFO")
        Me.chkModPoster.Text = Master.eLang.GetString(148, "Poster")
        Me.chkModTheme.Text = Master.eLang.GetString(1118, "Theme")
        Me.chkOptsAll.Text = Me.chkModAll.Text
        Me.chkOptsMainCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkOptsMainCert.Text = Master.eLang.GetString(56, "Certification")
        Me.chkOptsMainCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkOptsMainCreator.Text = Master.eLang.GetString(744, "Creator(s)")
        Me.chkOptsMainGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkOptsMainMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkOptsMainOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        Me.chkOptsMainStatus.Text = Master.eLang.GetString(215, "Status")
        Me.chkOptsMainPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkOptsMainEpisodeGuideURL.Text = Master.eLang.GetString(723, "Episode Guide URL")
        Me.chkOptsMainRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkOptsMainRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkOptsMainRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkOptsMainStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkOptsMainTitle.Text = Master.eLang.GetString(21, "Title")
        Me.gbOptions.Text = Master.eLang.GetString(390, "Options")
        Me.gbModifiers.Text = Master.eLang.GetString(388, "Modifiers")
        Me.gbUpdateModifier.Text = Master.eLang.GetString(386, "Selection Filter")
        Me.gbUpdateType.Text = Master.eLang.GetString(387, "Update Mode")
        Me.lblTopDescription.Text = Master.eLang.GetString(385, "Create a custom scraper")
        Me.lblTopTitle.Text = Me.Text
        Me.rbUpdateModifier_All.Text = Master.eLang.GetString(68, "All")
        Me.rbUpdateModifier_Marked.Text = Master.eLang.GetString(48, "Marked")
        Me.rbUpdateModifier_Missing.Text = Master.eLang.GetString(40, "Missing Items")
        Me.rbUpdateModifier_New.Text = Master.eLang.GetString(47, "New")
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