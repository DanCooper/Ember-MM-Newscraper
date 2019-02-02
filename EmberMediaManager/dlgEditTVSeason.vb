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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
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

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_Rescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub Setup()
        Dim mTitle As String = tmpDBElement.TVShow.Title
        Text = String.Concat(Master.eLang.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        chkLocked.Text = Master.eLang.GetString(43, "Locked")
        chkMarked.Text = Master.eLang.GetString(48, "Marked")
        lblAired.Text = String.Concat(Master.eLang.GetString(728, "Aired"), ":")
        lblBanner.Text = Master.eLang.GetString(838, "Banner")
        lblFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblPlot.Text = String.Concat(Master.eLang.GetString(65, "Plot"), ":")
        lblPoster.Text = Master.eLang.GetString(148, "Poster")
        lblTitle.Text = String.Concat(Master.eLang.GetString(21, "Title"), ":")
        lblTopDetails.Text = Master.eLang.GetString(830, "Edit the details for the selected season.")
        lblTopTitle.Text = Master.eLang.GetString(769, "Edit Season")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
    End Sub

    Public Overloads Function ShowDialog(ByVal DBTVShow As Database.DBElement) As DialogResult
        tmpDBElement = DBTVShow
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Function ConvertButtonToModifierType(ByVal sender As System.Object) As Enums.ModifierType
        Select Case True
            Case sender Is btnRemoveBanner, sender Is btnSetBannerDL, sender Is btnSetBannerLocal, sender Is btnSetBannerScrape
                Return Enums.ModifierType.SeasonBanner
            Case sender Is btnRemoveFanart, sender Is btnSetFanartDL, sender Is btnSetFanartLocal, sender Is btnSetFanartScrape
                Return Enums.ModifierType.SeasonFanart
            Case sender Is btnRemoveLandscape, sender Is btnSetLandscapeDL, sender Is btnSetLandscapeLocal, sender Is btnSetLandscapeScrape
                Return Enums.ModifierType.SeasonLandscape
            Case sender Is btnRemovePoster, sender Is btnSetPosterDL, sender Is btnSetPosterLocal, sender Is btnSetPosterScrape
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
        With tmpDBElement.TVSeason
            'Aired
            dtpAired.Text = .Aired
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
                If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVSeasonBannerAnyEnabled) OrElse
                    (tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled) Then
                    If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)) OrElse
                        (tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner) AndAlso
                        Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)) Then
                        btnSetBannerScrape.Enabled = False
                    End If
                    If .Banner.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonBanner)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlBanner.Visible = False
                End If

                'Fanart
                If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVSeasonFanartAnyEnabled) OrElse
                    (tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled) Then
                    If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                        Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) OrElse
                    (tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart) AndAlso
                    Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)) Then
                        btnSetFanartScrape.Enabled = False
                    End If
                    If .Fanart.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonFanart)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlFanart.Visible = False
                End If

                'Landscape
                If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled) OrElse
                    (tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled) Then
                    If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)) OrElse
                        (tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape) AndAlso
                        Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)) Then
                        btnSetLandscapeScrape.Enabled = False
                    End If
                    If .Landscape.ImageOriginal.Image IsNot Nothing Then
                        Image_LoadPictureBox(Enums.ModifierType.SeasonLandscape)
                    End If
                    bNeedTab_Images = True
                Else
                    pnlLandscape.Visible = False
                End If

                'Poster
                If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVSeasonPosterAnyEnabled) OrElse
                    (tmpDBElement.TVSeason.IsAllSeasons AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled) Then
                    If (Not tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)) OrElse
                        (tmpDBElement.TVSeason.IsAllSeasons AndAlso Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster) AndAlso
                        Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)) Then
                        btnSetPosterScrape.Enabled = False
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
        With tmpDBElement.TVSeason
            'Aired
            .Aired = dtpAired.Value.ToString("yyyy-MM-dd")
            'Plot
            .Plot = txtPlot.Text.Trim
            'Title
            .Title = txtTitle.Text.Trim
        End With
    End Sub

    Private Sub Image_DoubleClick(sender As Object, e As EventArgs) Handles _
        pbBanner.DoubleClick,
        pbFanart.DoubleClick,
        pbLandscape.DoubleClick,
        pbPoster.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.ImageOriginal.Image)
        End If
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Image_Download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerDL.Click,
        btnSetFanartDL.Click,
        btnSetLandscapeDL.Click,
        btnSetPosterDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
                    Select Case modType
                        Case Enums.ModifierType.SeasonBanner
                            tmpDBElement.ImagesContainer.Banner = tImage
                        Case Enums.ModifierType.SeasonFanart
                            tmpDBElement.ImagesContainer.Fanart = tImage
                        Case Enums.ModifierType.SeasonLandscape
                            tmpDBElement.ImagesContainer.Landscape = tImage
                        Case Enums.ModifierType.SeasonPoster
                            tmpDBElement.ImagesContainer.Poster = tImage
                    End Select
                    Image_LoadPictureBox(modType)
                End If
            End If
        End Using
    End Sub

    Private Sub Image_LoadPictureBox(ByVal imageType As Enums.ModifierType)
        Dim cImage As MediaContainers.Image
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Select Case imageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                cImage = tmpDBElement.ImagesContainer.Banner
                lblSize = lblBannerSize
                pbImage = pbBanner
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                cImage = tmpDBElement.ImagesContainer.Fanart
                lblSize = lblFanartSize
                pbImage = pbFanart
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                cImage = tmpDBElement.ImagesContainer.Landscape
                lblSize = lblLandscapeSize
                pbImage = pbLandscape
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                cImage = tmpDBElement.ImagesContainer.Poster
                lblSize = lblPosterSize
                pbImage = pbPoster
            Case Else
                Return
        End Select
        If cImage.ImageOriginal.Image IsNot Nothing Then
            pbImage.Image = cImage.ImageOriginal.Image
            pbImage.Tag = cImage
            lblSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbImage.Image.Width, pbImage.Image.Height)
            lblSize.Visible = True
        End If
    End Sub

    Private Sub Image_Local_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerLocal.Click,
        btnSetFanartLocal.Click,
        btnSetLandscapeLocal.Click,
        btnSetPosterLocal.Click
        With ofdLocalFiles
            .InitialDirectory = tmpDBElement.FileItem.MainPath.FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With
        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            Dim tImage As New MediaContainers.Image
            tImage.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            If tImage.ImageOriginal.Image IsNot Nothing Then
                Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
                Select Case modType
                    Case Enums.ModifierType.SeasonBanner
                        tmpDBElement.ImagesContainer.Banner = tImage
                    Case Enums.ModifierType.SeasonFanart
                        tmpDBElement.ImagesContainer.Fanart = tImage
                    Case Enums.ModifierType.SeasonLandscape
                        tmpDBElement.ImagesContainer.Landscape = tImage
                    Case Enums.ModifierType.SeasonPoster
                        tmpDBElement.ImagesContainer.Poster = tImage
                End Select
                Image_LoadPictureBox(modType)
            End If
        End If
    End Sub

    Private Sub Image_Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnRemoveBanner.Click,
        btnRemoveFanart.Click,
        btnRemoveLandscape.Click,
        btnRemovePoster.Click
        Dim lblSize As Label
        Dim pbImage As PictureBox
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Select Case modType
            Case Enums.ModifierType.MainBanner
                lblSize = lblBannerSize
                pbImage = pbBanner
                tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
            Case Enums.ModifierType.MainFanart
                lblSize = lblFanartSize
                pbImage = pbFanart
                tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
            Case Enums.ModifierType.MainLandscape
                lblSize = lblLandscapeSize
                pbImage = pbLandscape
                tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image
            Case Enums.ModifierType.MainPoster
                lblSize = lblPosterSize
                pbImage = pbPoster
                tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
            Case Else
                Return
        End Select
        lblSize.Text = String.Empty
        lblSize.Visible = False
        pbImage.Image = Nothing
        pbImage.Tag = Nothing
    End Sub

    Private Sub Image_Scrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        btnSetBannerScrape.Click,
        btnSetFanartScrape.Click,
        btnSetLandscapeScrape.Click,
        btnSetPosterScrape.Click
        Cursor = Cursors.WaitCursor
        Dim modType As Enums.ModifierType = ConvertButtonToModifierType(sender)
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers
        Select Case modType
            Case Enums.ModifierType.SeasonBanner
                If tmpDBElement.TVSeason.IsAllSeasons Then
                    modType = Enums.ModifierType.AllSeasonsBanner
                End If
            Case Enums.ModifierType.SeasonFanart
                If tmpDBElement.TVSeason.IsAllSeasons Then
                    modType = Enums.ModifierType.AllSeasonsFanart
                End If
            Case Enums.ModifierType.SeasonLandscape
                If tmpDBElement.TVSeason.IsAllSeasons Then
                    modType = Enums.ModifierType.AllSeasonsLandscape
                End If
            Case Enums.ModifierType.SeasonPoster
                If tmpDBElement.TVSeason.IsAllSeasons Then
                    modType = Enums.ModifierType.AllSeasonsPoster
                End If
        End Select
        Functions.SetScrapeModifiers(ScrapeModifiers, modType, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            Dim iImageCount = 0
            Dim strNoImagesFound As String = String.Empty
            Select Case modType
                Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                    iImageCount = aContainer.SeasonBanners.Count
                    If tmpDBElement.TVSeason.IsAllSeasons Then iImageCount += aContainer.MainBanners.Count
                    strNoImagesFound = Master.eLang.GetString(1363, "No Banners found")
                Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                    iImageCount = aContainer.SeasonFanarts.Count + aContainer.MainFanarts.Count
                    strNoImagesFound = Master.eLang.GetString(970, "No Fanarts found")
                Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                    iImageCount = aContainer.SeasonLandscapes.Count
                    If tmpDBElement.TVSeason.IsAllSeasons Then iImageCount += aContainer.MainLandscapes.Count
                    strNoImagesFound = Master.eLang.GetString(1197, "No Landscapes found")
                Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                    iImageCount = aContainer.SeasonPosters.Count
                    If tmpDBElement.TVSeason.IsAllSeasons Then iImageCount += aContainer.MainPosters.Count
                    strNoImagesFound = Master.eLang.GetString(972, "No Posters found")
            End Select
            If iImageCount > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    Select Case modType
                        Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                            tmpDBElement.ImagesContainer.Banner = dlgImgS.Result.ImagesContainer.Banner
                            If tmpDBElement.ImagesContainer.Banner.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                            tmpDBElement.ImagesContainer.Fanart = dlgImgS.Result.ImagesContainer.Fanart
                            If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                            tmpDBElement.ImagesContainer.Landscape = dlgImgS.Result.ImagesContainer.Landscape
                            If tmpDBElement.ImagesContainer.Landscape.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                        Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                            tmpDBElement.ImagesContainer.Poster = dlgImgS.Result.ImagesContainer.Poster
                            If tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromMemoryStream() Then
                                Image_LoadPictureBox(modType)
                            Else
                                Image_Remove_Click(sender, e)
                            End If
                    End Select
                End If
            Else
                MessageBox.Show(strNoImagesFound, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub PictureBox_DragEnter(sender As Object, e As DragEventArgs) Handles _
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

    Private Sub PictureBox_DragDrop(sender As Object, e As DragEventArgs) Handles _
        pbBanner.DragDrop,
        pbFanart.DragDrop,
        pbLandscape.DragDrop,
        pbPoster.DragDrop

        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDroppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Select Case True
                Case sender Is pbBanner
                    tmpDBElement.ImagesContainer.Banner = tImage
                    Image_LoadPictureBox(Enums.ModifierType.SeasonBanner)
                Case sender Is pbFanart
                    tmpDBElement.ImagesContainer.Fanart = tImage
                    Image_LoadPictureBox(Enums.ModifierType.SeasonFanart)
                Case sender Is pbLandscape
                    tmpDBElement.ImagesContainer.Landscape = tImage
                    Image_LoadPictureBox(Enums.ModifierType.SeasonLandscape)
                Case sender Is pbPoster
                    tmpDBElement.ImagesContainer.Poster = tImage
                    Image_LoadPictureBox(Enums.ModifierType.SeasonPoster)
            End Select
        End If
    End Sub

    Private Sub TextBox_NumericOnly(sender As Object, e As KeyPressEventArgs)
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_SelectAll(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

#End Region 'Methods

End Class