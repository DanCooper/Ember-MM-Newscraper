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
Imports System.Diagnostics
Public Class dlgEditTVSeason

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private tmpDBElement As New Database.DBElement

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVSeason As Database.DBElement) As DialogResult
        Me.tmpDBElement = DBTVSeason
        Return MyBase.ShowDialog()
    End Function

    Private Sub btnRemoveBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveBanner.Click
        Me.pbBanner.Image = Nothing
        Me.pbBanner.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFanart.Click
        Me.pbFanart.Image = Nothing
        Me.pbFanart.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLandscape.Click
        Me.pbLandscape.Image = Nothing
        Me.pbLandscape.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePoster.Click
        Me.pbPoster.Image = Nothing
        Me.pbPoster.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Banner = tImage
                    Me.pbBanner.Image = tImage.ImageOriginal.Image
                    Me.pbBanner.Tag = tImage

                    Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                    Me.lblBannerSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsBanner, True)
        Else
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonBanner, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.SeasonBanners.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainBanners.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Banner
                    Me.tmpDBElement.ImagesContainer.Banner = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbBanner.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                        Me.lblBannerSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerLocal.Click
        With ofdImage
            .InitialDirectory = Me.tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.FromFile(ofdImage.FileName)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Me.tmpDBElement.ImagesContainer.Banner = tImage
                Me.pbBanner.Image = tImage.ImageOriginal.Image
                Me.pbBanner.Tag = tImage

                Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                Me.lblBannerSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Fanart = tImage
                    Me.pbFanart.Image = tImage.ImageOriginal.Image
                    Me.pbFanart.Tag = tImage

                    Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                    Me.lblFanartSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsFanart, True)
        Else
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonFanart, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.SeasonFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Fanart
                    Me.tmpDBElement.ImagesContainer.Fanart = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbFanart.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                        Me.lblFanartSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartLocal.Click
        With ofdImage
            .InitialDirectory = Me.tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 4
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.FromFile(ofdImage.FileName)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Me.tmpDBElement.ImagesContainer.Fanart = tImage
                Me.pbFanart.Image = tImage.ImageOriginal.Image
                Me.pbFanart.Tag = tImage

                Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                Me.lblFanartSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Landscape = tImage
                    Me.pbLandscape.Image = tImage.ImageOriginal.Image
                    Me.pbLandscape.Tag = tImage

                    Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                    Me.lblLandscapeSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsLandscape, True)
        Else
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonLandscape, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.SeasonLandscapes.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainLandscapes.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Landscape
                    Me.tmpDBElement.ImagesContainer.Landscape = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbLandscape.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                        Me.lblLandscapeSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeLocal.Click
        With ofdImage
            .InitialDirectory = Me.tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.FromFile(ofdImage.FileName)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Me.tmpDBElement.ImagesContainer.Landscape = tImage
                Me.pbLandscape.Image = tImage.ImageOriginal.Image
                Me.pbLandscape.Tag = tImage

                Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                Me.lblLandscapeSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetSeasonPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Me.tmpDBElement.ImagesContainer.Poster = tImage
                    Me.pbPoster.Image = tImage.ImageOriginal.Image
                    Me.pbPoster.Tag = tImage

                    Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                    Me.lblPosterSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.AllSeasonsPoster, True)
        Else
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.SeasonPoster, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.SeasonPosters.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainPosters.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.TVSeason) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.Poster
                    Me.tmpDBElement.ImagesContainer.Poster = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbPoster.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                        Me.lblPosterSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterLocal.Click
        With ofdImage
            .InitialDirectory = Me.tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.FromFile(ofdImage.FileName)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Me.tmpDBElement.ImagesContainer.Poster = tImage
                Me.pbPoster.Image = tImage.ImageOriginal.Image
                Me.pbPoster.Tag = tImage

                Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                Me.lblPosterSize.Visible = True
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgEditSeason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(Me.tmpDBElement, True) Then
            If Not Master.eSettings.TVSeasonBannerAnyEnabled Then tcEdit.TabPages.Remove(tpBanner)
            If Not Master.eSettings.TVSeasonFanartAnyEnabled Then tcEdit.TabPages.Remove(tpFanart)
            If Not Master.eSettings.TVSeasonLandscapeAnyEnabled Then tcEdit.TabPages.Remove(tpLandscape)
            If Not Master.eSettings.TVSeasonPosterAnyEnabled Then tcEdit.TabPages.Remove(tpPoster)

            Me.pbBanner.AllowDrop = True
            Me.pbFanart.AllowDrop = True
            Me.pbLandscape.AllowDrop = True
            Me.pbPoster.AllowDrop = True

            Me.SetUp()

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            Me.FillInfo()
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub

    Private Sub FillInfo()
        With Me

            'Images and TabPages

            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse _
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)) OrElse _
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner) AndAlso _
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)) Then
                    .btnSetBannerScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image IsNot Nothing Then
                    .pbBanner.Image = Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                    .pbBanner.Tag = Me.tmpDBElement.ImagesContainer.Banner

                    .lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbBanner.Image.Width, .pbBanner.Image.Height)
                    .lblBannerSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpBanner)
            End If

            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse _
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso _
                    Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) OrElse _
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso _
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) Then
                    .btnSetFanartScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                    .pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    .pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

                    .lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbFanart.Image.Width, .pbFanart.Image.Height)
                    .lblFanartSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpFanart)
            End If

            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse _
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)) OrElse _
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape) AndAlso _
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)) Then
                    .btnSetLandscapeScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image IsNot Nothing Then
                    .pbLandscape.Image = Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                    .pbLandscape.Tag = Me.tmpDBElement.ImagesContainer.Landscape

                    .lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbLandscape.Image.Width, .pbLandscape.Image.Height)
                    .lblLandscapeSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpLandscape)
            End If

            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse _
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)) OrElse _
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster) AndAlso _
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)) Then
                    .btnSetPosterScrape.Enabled = False
                End If
                If Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing Then
                    .pbPoster.Image = Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                    .pbPoster.Tag = Me.tmpDBElement.ImagesContainer.Poster

                    .lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbPoster.Image.Width, .pbPoster.Image.Height)
                    .lblPosterSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpPoster)
            End If
        End With
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Master.DB.SaveTVSeasonToDB(Me.tmpDBElement, False)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbSeasonBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Banner = tImage
            Me.pbBanner.Image = tImage.ImageOriginal.Image
            Me.pbBanner.Tag = tImage
            Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
            Me.lblBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbSeasonFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbFanart.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Fanart = tImage
            Me.pbFanart.Image = tImage.ImageOriginal.Image
            Me.pbFanart.Tag = tImage
            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbSeasonLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbLandscape.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Landscape = tImage
            Me.pbLandscape.Image = tImage.ImageOriginal.Image
            Me.pbLandscape.Tag = tImage
            Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
            Me.lblLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbSeasonPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Poster = tImage
            Me.pbPoster.Image = tImage.ImageOriginal.Image
            Me.pbPoster.Tag = tImage
            Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
            Me.lblPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(769, "Edit Season")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.btnRemoveBanner.Text = Master.eLang.GetString(1024, "Remove Banner")
        Me.btnRemoveFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveLandscape.Text = Master.eLang.GetString(1034, "Remove Landscape")
        Me.btnRemovePoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnSetBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetLandscapeDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetLandscapeLocal.Text = Master.eLang.GetString(1031, "Change Landscape (Local)/")
        Me.btnSetLandscapeScrape.Text = Master.eLang.GetString(1032, "Change Landscape (Scrape)")
        Me.btnSetPosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetPosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetPosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.lblTopTitle.Text = Me.Text
        Me.tblTopDetails.Text = Master.eLang.GetString(830, "Edit the details for the selected season.")
        Me.tpBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")
    End Sub

#End Region 'Methods

End Class