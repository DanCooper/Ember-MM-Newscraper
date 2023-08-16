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

Public Class dlgEdit_TVSeason

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As Database.DBElement
        Get
            Return tmpDBElement
        End Get
    End Property

#End Region 'Properties

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tmpDBElement, True) Then
            pbBanner.AllowDrop = True
            pbFanart.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            Setup()
            Data_Fill()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_Rescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRescrape.Click
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub Setup()
        With Master.eLang
            Dim mTitle As String = tmpDBElement.MainDetails.Title
            Text = String.Concat(.GetString(769, "Edit Season"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
            btnCancel.Text = .CommonWordsList.Cancel
            btnOK.Text = .CommonWordsList.OK
            btnRescrape.Text = .GetString(716, "Re-Scrape")
            chkLocked.Text = .GetString(43, "Locked")
            chkMarked.Text = .GetString(48, "Marked")
            lblAired.Text = String.Concat(.GetString(728, "Aired"), ":")
            lblBanner.Text = .GetString(838, "Banner")
            lblFanart.Text = .GetString(149, "Fanart")
            lblLandscape.Text = .GetString(1059, "Landscape")
            lblPlot.Text = String.Concat(.GetString(65, "Plot"), ":")
            lblPoster.Text = .GetString(148, "Poster")
            lblTitle.Text = String.Concat(.GetString(21, "Title"), ":")
            lblTopDetails.Text = .GetString(830, "Edit the details for the selected season.")
            lblTopTitle.Text = .GetString(769, "Edit Season")
            tpDetails.Text = .GetString(26, "Details")
            tpImages.Text = .GetString(497, "Images")
        End With
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVShow As Database.DBElement) As DialogResult
        tmpDBElement = DBTVShow
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Function ConvertControlToImageType(ByVal sender As Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveBanner, sender Is btnDLBanner, sender Is btnLocalBanner, sender Is btnScrapeBanner, sender Is btnClipboardBanner
                Return Enums.ModifierType.SeasonBanner
            Case sender Is btnRemoveFanart, sender Is btnDLFanart, sender Is btnLocalFanart, sender Is btnScrapeFanart, sender Is btnClipboardFanart
                Return Enums.ModifierType.SeasonFanart
            Case sender Is btnRemoveLandscape, sender Is btnDLLandscape, sender Is btnLocalLandscape, sender Is btnScrapeLandscape, sender Is btnClipboardLandscape
                Return Enums.ModifierType.SeasonLandscape
            Case sender Is btnRemovePoster, sender Is btnDLPoster, sender Is btnLocalPoster, sender Is btnScrapePoster, sender Is btnClipboardPoster
                Return Enums.ModifierType.SeasonPoster
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub Data_Fill(Optional ByVal DoAll As Boolean = True)
        'Database related part
        With tmpDBElement
            chkLocked.Checked = .IsLocked
            chkMarked.Checked = .IsMarked
        End With

        'Information part
        With tmpDBElement.MainDetails
            'Aired
            If .AiredSpecified Then
                dtpAired.Text = .Aired
                dtpAired.Checked = True
            End If
            'Plot
            txtPlot.Text = .Plot
            'Title
            txtTitle.Text = .Title
        End With

        If DoAll Then
            'Images and TabPages/Panels controll
            Dim bNeedTab_Images As Boolean

            With tmpDBElement.ImagesContainer
                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Banner
                If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse
                    (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled) Then
                    If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)) OrElse
                        (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner) AndAlso
                        Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)) Then
                        btnScrapeBanner.Enabled = False
                    End If
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'Fanart
                If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse
                    (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled) Then
                    If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                        Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) OrElse
                    (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                    Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) Then
                        btnScrapeFanart.Enabled = False
                    End If
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'Landscape
                If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse
                    (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled) Then
                    If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)) OrElse
                        (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape) AndAlso
                        Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)) Then
                        btnScrapeLandscape.Enabled = False
                    End If
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse
                    (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled) Then
                    If (Not tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)) OrElse
                        (tmpDBElement.MainDetails.Season_IsAllSeasons AndAlso Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster) AndAlso
                        Not Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)) Then
                        btnScrapePoster.Enabled = False
                    End If
                    If .Poster.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonPoster)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlPoster.Visible = False
                End If
            End With

            'Remove empty tab pages
            If Not bNeedTab_Images Then
                tcEdit.TabPages.Remove(tpImages)
            End If
        End If
    End Sub

    Private Sub Data_Save()
        btnOK.Enabled = False
        btnCancel.Enabled = False
        btnRescrape.Enabled = False

        'Database related part
        With tmpDBElement
            .IsLocked = chkLocked.Checked
            .IsMarked = chkMarked.Checked
        End With

        'Information part
        With tmpDBElement.MainDetails
            'Aired
            If dtpAired.Checked Then
                .Aired = dtpAired.Value.ToString("yyyy-MM-dd")
            Else
                .Aired = String.Empty
            End If
            'Plot
            .Plot = txtPlot.Text.Trim
            'Title
            .Title = txtTitle.Text.Trim
        End With
    End Sub

    Private Sub Image_Clipboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnClipboardBanner.Click,
        btnClipboardFanart.Click,
        btnClipboardLandscape.Click,
        btnClipboardPoster.Click

        Dim lstImages = FileUtils.ClipboardHandler.GetImagesFromClipboard
        If lstImages.Count > 0 Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(lstImages(0), eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbFanart.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            dlgImageViewer.ShowDialog(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnDLBanner.Click,
        btnDLFanart.Click,
        btnDLLandscape.Click,
        btnDLPoster.Click
        Using dImgManual As New dlgImageManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                    tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                    Image_LoadPictureBox(eImageType)
                End If
            End If
        End Using
    End Sub

    Private Sub Image_DragDrop(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragDrop,
        pbFanart.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
            tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
            Image_LoadPictureBox(eImageType)
        End If
    End Sub

    Private Sub Image_DragEnter(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragEnter,
        pbFanart.DragEnter,
        pbLandscape.DragEnter,
        pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Image_LoadPictureBox(ByVal ImageType As Enums.ModifierType)
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case ImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
            Case Else
                Return
        End Select
        Dim cImage = tmpDBElement.ImagesContainer.GetImageByType(ImageType)
        If cImage.ImageOriginal.Image IsNot Nothing Then
            pbImage.Image = cImage.ImageOriginal.Image
            pbImage.Tag = cImage
            lblSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbImage.Image.Width, pbImage.Image.Height)
            lblSize.Visible = True
        End If
    End Sub

    Private Sub Image_Local_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnLocalBanner.Click,
        btnLocalFanart.Click,
        btnLocalLandscape.Click,
        btnLocalPoster.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.ShowPath
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
                tmpDBElement.ImagesContainer.SetImageByType(tImage, eImageType)
                Image_LoadPictureBox(eImageType)
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveFanart.Click,
        btnRemoveLandscape.Click,
        btnRemovePoster.Click
        Dim lblSize As Label = Nothing
        Dim pbImage As PictureBox = Nothing
        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Select Case eImageType
            Case Enums.ModifierType.SeasonBanner
                lblSize = lblSizeBanner
                pbImage = pbBanner
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.SeasonFanart
                lblSize = lblSizeFanart
                pbImage = pbFanart
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.SeasonLandscape
                lblSize = lblSizeLandscape
                pbImage = pbLandscape
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Enums.ModifierType.SeasonPoster
                lblSize = lblSizePoster
                pbImage = pbPoster
                tmpDBElement.ImagesContainer.SetImageByType(New MediaContainers.Image, eImageType)
            Case Else
                Return
        End Select
        If lblSize IsNot Nothing Then
            lblSize.Text = String.Empty
            lblSize.Visible = False
        End If
        If pbImage IsNot Nothing Then
            pbImage.Image = Nothing
            pbImage.Tag = Nothing
        End If
    End Sub

    Private Sub Image_Scrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _
        btnScrapeBanner.Click,
        btnScrapeFanart.Click,
        btnScrapeLandscape.Click,
        btnScrapePoster.Click

        Cursor = Cursors.WaitCursor

        Dim eImageType As Enums.ModifierType = ConvertControlToImageType(sender)
        Select Case eImageType
            Case Enums.ModifierType.SeasonBanner
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then
                    eImageType = Enums.ModifierType.AllSeasonsBanner
                End If
            Case Enums.ModifierType.SeasonFanart
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then
                    eImageType = Enums.ModifierType.AllSeasonsFanart
                End If
            Case Enums.ModifierType.SeasonLandscape
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then
                    eImageType = Enums.ModifierType.AllSeasonsLandscape
                End If
            Case Enums.ModifierType.SeasonPoster
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then
                    eImageType = Enums.ModifierType.AllSeasonsPoster
                End If
        End Select

        Functions.SetScrapeModifiers(tmpDBElement.ScrapeModifiers, eImageType, True)
        Dim ScrapeResults = Scraper.Run(tmpDBElement)
        Dim iImageCount = 0
        Dim strNoImagesFound As String = String.Empty
        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                iImageCount = ScrapeResults.Images.SeasonBanners.Count
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then iImageCount += ScrapeResults.Images.MainBanners.Count
                strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                iImageCount = ScrapeResults.Images.SeasonFanarts.Count + ScrapeResults.Images.MainFanarts.Count
                strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                iImageCount = ScrapeResults.Images.SeasonLandscapes.Count
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then iImageCount += ScrapeResults.Images.MainLandscapes.Count
                strNoImagesFound = Master.eLang.GetString(1197, "No Landscapes found")
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                iImageCount = ScrapeResults.Images.SeasonPosters.Count
                If tmpDBElement.MainDetails.Season_IsAllSeasons Then iImageCount += ScrapeResults.Images.MainPosters.Count
                strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
        End Select
        If iImageCount > 0 Then
            Dim dlgImgS = New dlgImgSelect()
            If dlgImgS.ShowDialog(tmpDBElement, ScrapeResults.Images) = DialogResult.OK Then
                tmpDBElement.ImagesContainer.SetImageByType(dlgImgS.Result.ImagesContainer.GetImageByType(eImageType), eImageType)
                If tmpDBElement.ImagesContainer.GetImageByType(eImageType) IsNot Nothing AndAlso
                        tmpDBElement.ImagesContainer.GetImageByType(eImageType).ImageOriginal.LoadFromMemoryStream Then
                    Image_LoadPictureBox(eImageType)
                Else
                    Image_Remove_Click(sender, e)
                End If
            End If
        Else
            MessageBox.Show(strNoImagesFound, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

#End Region 'Methods

End Class