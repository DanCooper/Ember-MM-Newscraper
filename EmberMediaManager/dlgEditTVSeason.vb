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

Imports EmberAPI
Imports NLog

Public Class dlgEditTVSeason

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVSeason As Database.DBElement) As DialogResult
        tmpDBElement = DBTVSeason
        Return ShowDialog()
    End Function

    Private Sub btnRemoveBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveBanner.Click
        pbBanner.Image = Nothing
        pbBanner.Tag = Nothing
        lblBannerSize.Text = String.Empty
        lblBannerSize.Visible = False
        tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveFanart.Click
        pbFanart.Image = Nothing
        pbFanart.Tag = Nothing
        lblFanartSize.Text = String.Empty
        lblFanartSize.Visible = False
        tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLandscape.Click
        pbLandscape.Image = Nothing
        pbLandscape.Tag = Nothing
        lblLandscapeSize.Text = String.Empty
        lblLandscapeSize.Visible = False
        tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePoster.Click
        pbPoster.Image = Nothing
        pbPoster.Tag = Nothing
        lblPosterSize.Text = String.Empty
        lblPosterSize.Visible = False
        tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Banner = tImage
                    pbBanner.Image = tImage.ImageOriginal.Image
                    pbBanner.Tag = tImage

                    lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
                    lblBannerSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsBanner, True)
        Else
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonBanner, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.SeasonBanners.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainBanners.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                    If tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Banner.ImageOriginal.LoadFromMemoryStream Then
                        pbBanner.Image = tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                        lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
                        lblBannerSize.Visible = True
                    Else
                        pbBanner.Image = Nothing
                        pbBanner.Tag = Nothing
                        lblBannerSize.Text = String.Empty
                        lblBannerSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerLocal.Click
        With ofdImage
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                tmpDBElement.ImagesContainer.Banner = tImage
                pbBanner.Image = tImage.ImageOriginal.Image
                pbBanner.Tag = tImage

                lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
                lblBannerSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Fanart = tImage
                    pbFanart.Image = tImage.ImageOriginal.Image
                    pbFanart.Tag = tImage

                    lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                    lblFanartSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsFanart, True)
        Else
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonFanart, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.SeasonFanarts.Count > 0 OrElse aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                    If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromMemoryStream Then
                        pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                        lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                        lblFanartSize.Visible = True
                    Else
                        pbFanart.Image = Nothing
                        pbFanart.Tag = Nothing
                        lblFanartSize.Text = String.Empty
                        lblFanartSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartLocal.Click
        With ofdImage
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 4
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                tmpDBElement.ImagesContainer.Fanart = tImage
                pbFanart.Image = tImage.ImageOriginal.Image
                pbFanart.Tag = tImage

                lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                lblFanartSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Landscape = tImage
                    pbLandscape.Image = tImage.ImageOriginal.Image
                    pbLandscape.Tag = tImage

                    lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
                    lblLandscapeSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsLandscape, True)
        Else
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonLandscape, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.SeasonLandscapes.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainLandscapes.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                    If tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Landscape.ImageOriginal.LoadFromMemoryStream Then
                        pbLandscape.Image = tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                        lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
                        lblLandscapeSize.Visible = True
                    Else
                        pbLandscape.Image = Nothing
                        pbLandscape.Tag = Nothing
                        lblLandscapeSize.Text = String.Empty
                        lblLandscapeSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1197, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeLocal.Click
        With ofdImage
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                tmpDBElement.ImagesContainer.Landscape = tImage
                pbLandscape.Image = tImage.ImageOriginal.Image
                pbLandscape.Tag = tImage

                lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
                lblLandscapeSize.Visible = True
            End If
        End If
    End Sub

    Private Sub btnSetSeasonPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Using dImgManual As New dlgImgManual
            If dImgManual.ShowDialog() = DialogResult.OK Then
                Dim tImage As MediaContainers.Image = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Poster = tImage
                    pbPoster.Image = tImage.ImageOriginal.Image
                    pbPoster.Tag = tImage

                    lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                    lblPosterSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        If tmpDBElement.TVSeason.Season = 999 Then
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.AllSeasonsPoster, True)
        Else
            Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.SeasonPoster, True)
        End If
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.SeasonPosters.Count > 0 OrElse (tmpDBElement.TVSeason.Season = 999 AndAlso aContainer.MainPosters.Count > 0) Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                    If tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromMemoryStream Then
                        pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                        lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                        lblPosterSize.Visible = True
                    Else
                        pbPoster.Image = Nothing
                        pbPoster.Tag = Nothing
                        lblPosterSize.Text = String.Empty
                        lblPosterSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterLocal.Click
        With ofdImage
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdImage.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                tmpDBElement.ImagesContainer.Poster = tImage
                pbPoster.Image = tImage.ImageOriginal.Image
                pbPoster.Tag = tImage

                lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                lblPosterSize.Visible = True
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub dlgEditSeason_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tmpDBElement, True) Then
            If Not Master.eSettings.TVSeasonBannerAnyEnabled Then tcEdit.TabPages.Remove(tpBanner)
            If Not Master.eSettings.TVSeasonFanartAnyEnabled Then tcEdit.TabPages.Remove(tpFanart)
            If Not Master.eSettings.TVSeasonLandscapeAnyEnabled Then tcEdit.TabPages.Remove(tpLandscape)
            If Not Master.eSettings.TVSeasonPosterAnyEnabled Then tcEdit.TabPages.Remove(tpPoster)

            pbBanner.AllowDrop = True
            pbFanart.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True

            SetUp()

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            FillInfo()
        Else
            DialogResult = System.Windows.Forms.DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub FillInfo()
        txtAired.Text = tmpDBElement.TVSeason.Aired
        txtPlot.Text = tmpDBElement.TVSeason.Plot
        txtTitle.Text = tmpDBElement.TVSeason.Title

        'Images and TabPages
        With tmpDBElement.ImagesContainer

            'Load all images to MemoryStream and Bitmap
            tmpDBElement.LoadAllImages(True, True)

            'Banner
            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)) OrElse
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner) AndAlso
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)) Then
                    btnSetBannerScrape.Enabled = False
                End If
                If .Banner.ImageOriginal.Image IsNot Nothing Then
                    pbBanner.Image = .Banner.ImageOriginal.Image
                    pbBanner.Tag = .Banner

                    lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
                    lblBannerSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpBanner)
            End If

            'Fanart
            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                    Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) OrElse
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) Then
                    btnSetFanartScrape.Enabled = False
                End If
                If .Fanart.ImageOriginal.Image IsNot Nothing Then
                    pbFanart.Image = .Fanart.ImageOriginal.Image
                    pbFanart.Tag = .Fanart

                    lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                    lblFanartSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpFanart)
            End If

            'Landscape
            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)) OrElse
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape) AndAlso
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)) Then
                    btnSetLandscapeScrape.Enabled = False
                End If
                If .Landscape.ImageOriginal.Image IsNot Nothing Then
                    pbLandscape.Image = .Landscape.ImageOriginal.Image
                    pbLandscape.Tag = .Landscape

                    lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
                    lblLandscapeSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpLandscape)
            End If

            'Poster
            If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse
                (tmpDBElement.TVSeason.Season = 999 AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled) Then
                If (Not tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)) OrElse
                    (tmpDBElement.TVSeason.Season = 999 AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster) AndAlso
                     Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)) Then
                    btnSetPosterScrape.Enabled = False
                End If
                If .Poster.ImageOriginal.Image IsNot Nothing Then
                    pbPoster.Image = .Poster.ImageOriginal.Image
                    pbPoster.Tag = .Poster

                    lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
                    lblPosterSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpPoster)
            End If
        End With
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        SetInfo()

        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub pbSeasonBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Banner = tImage
            pbBanner.Image = tImage.ImageOriginal.Image
            pbBanner.Tag = tImage
            lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
            lblBannerSize.Visible = True
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
            tmpDBElement.ImagesContainer.Fanart = tImage
            pbFanart.Image = tImage.ImageOriginal.Image
            pbFanart.Tag = tImage
            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
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
            tmpDBElement.ImagesContainer.Landscape = tImage
            pbLandscape.Image = tImage.ImageOriginal.Image
            pbLandscape.Tag = tImage
            lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
            lblLandscapeSize.Visible = True
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
            tmpDBElement.ImagesContainer.Poster = tImage
            pbPoster.Image = tImage.ImageOriginal.Image
            pbPoster.Tag = tImage
            lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
            lblPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbSeasonPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub SetInfo()
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False

        tmpDBElement.TVSeason.Aired = txtAired.Text.Trim
        tmpDBElement.TVSeason.Plot = txtPlot.Text.Trim
        tmpDBElement.TVSeason.Title = txtTitle.Text.Trim
    End Sub

    Private Sub SetUp()
        'Aired
        Dim strAired As String = String.Concat(Master.eLang.GetString(728, "Aired"), ":")
        lblAired.Text = strAired

        'Download
        Dim strDownload As String = Master.eLang.GetString(373, "Download")
        btnSetBannerDL.Text = strDownload
        btnSetFanartDL.Text = strDownload
        btnSetLandscapeDL.Text = strDownload
        btnSetPosterDL.Text = strDownload

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        btnSetBannerLocal.Text = strLocalBrowse
        btnSetFanartLocal.Text = strLocalBrowse
        btnSetLandscapeLocal.Text = strLocalBrowse
        btnSetPosterLocal.Text = strLocalBrowse

        'Plot
        Dim strPlot As String = Master.eLang.GetString(241, "Plot:")
        lblPlot.Text = strPlot

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        btnRemoveBanner.Text = strRemove
        btnRemoveFanart.Text = strRemove
        btnRemoveLandscape.Text = strRemove
        btnRemovePoster.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        btnSetBannerScrape.Text = strScrape
        btnSetFanartScrape.Text = strScrape
        btnSetLandscapeScrape.Text = strScrape
        btnSetPosterScrape.Text = strScrape

        'Title
        Dim strTitle As String = Master.eLang.GetString(21, "Title")
        lblTitle.Text = strTitle

        Text = Master.eLang.GetString(769, "Edit Season")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        lblTopTitle.Text = Text
        tblTopDetails.Text = Master.eLang.GetString(830, "Edit the details for the selected season.")
        tpBanner.Text = Master.eLang.GetString(838, "Banner")
        tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        tpLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        tpPoster.Text = Master.eLang.GetString(148, "Poster")
    End Sub

#End Region 'Methods

End Class