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

Public Class dlgEditTVShow

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private lvwActorSorter As ListViewColumnSorter
    Private tmpRating As String

    'Extrafanarts
    Private ExtrafanartsWarning As Boolean = True
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbExtrafanartsImage() As PictureBox
    Private pnlExtrafanartsImage() As Panel

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

    Public Overloads Function ShowDialog(ByVal DBTVShow As Database.DBElement) As DialogResult
        tmpDBElement = DBTVShow
        Return ShowDialog()
    End Function

    Private Sub ActorEdit()
        If lvActors.SelectedItems.Count > 0 Then
            Dim lvwItem As ListViewItem = lvActors.SelectedItems(0)
            Dim eActor As MediaContainers.Person = DirectCast(lvwItem.Tag, MediaContainers.Person)
            Using dAddEditActor As New dlgAddEditActor
                If dAddEditActor.ShowDialog(eActor) = DialogResult.OK Then
                    eActor = dAddEditActor.Result
                    lvwItem.Text = eActor.ID.ToString
                    lvwItem.Tag = eActor
                    lvwItem.SubItems(1).Text = eActor.Name
                    lvwItem.SubItems(2).Text = eActor.Role
                    lvwItem.SubItems(3).Text = eActor.URLOriginal
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                End If
            End Using
        End If
    End Sub

    Private Sub ActorRemove()
        If lvActors.Items.Count > 0 Then
            While lvActors.SelectedItems.Count > 0
                lvActors.Items.Remove(lvActors.SelectedItems(0))
            End While
        End If
    End Sub

    Private Sub AddExtrafanartImage(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image)
        Try
            If tImage.ImageOriginal.Image Is Nothing Then
                tImage.LoadAndCache(Enums.ContentType.TVShow, True, True)
            End If

            ReDim Preserve pnlExtrafanartsImage(iIndex)
            ReDim Preserve pbExtrafanartsImage(iIndex)
            pnlExtrafanartsImage(iIndex) = New Panel()
            pbExtrafanartsImage(iIndex) = New PictureBox()
            pbExtrafanartsImage(iIndex).Name = iIndex.ToString
            pnlExtrafanartsImage(iIndex).Name = iIndex.ToString
            pnlExtrafanartsImage(iIndex).Size = New Size(128, 72)
            pbExtrafanartsImage(iIndex).Size = New Size(128, 72)
            pnlExtrafanartsImage(iIndex).BackColor = Color.White
            pnlExtrafanartsImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            pbExtrafanartsImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            pnlExtrafanartsImage(iIndex).Tag = tImage
            pbExtrafanartsImage(iIndex).Tag = tImage
            pbExtrafanartsImage(iIndex).Image = tImage.ImageOriginal.Image
            pnlExtrafanartsImage(iIndex).Left = iEFLeft
            pbExtrafanartsImage(iIndex).Left = 0
            pnlExtrafanartsImage(iIndex).Top = iEFTop
            pbExtrafanartsImage(iIndex).Top = 0
            pnlExtrafanarts.Controls.Add(pnlExtrafanartsImage(iIndex))
            pnlExtrafanartsImage(iIndex).Controls.Add(pbExtrafanartsImage(iIndex))
            pnlExtrafanartsImage(iIndex).BringToFront()
            AddHandler pbExtrafanartsImage(iIndex).Click, AddressOf pbExtrafanartsImage_Click
            AddHandler pnlExtrafanartsImage(iIndex).Click, AddressOf pnlExtrafanartsImage_Click
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        iEFTop += 74

    End Sub

    Private Sub pbExtrafanartsImage_Click(ByVal sender As Object, ByVal e As EventArgs)
        DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlExtrafanartsImage_Click(ByVal sender As Object, ByVal e As EventArgs)
        DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub DoSelectExtrafanart(ByVal iIndex As Integer, tImg As MediaContainers.Image)
        pbExtrafanarts.Image = tImg.ImageOriginal.Image
        pbExtrafanarts.Tag = tImg
        btnExtrafanartsSetAsFanart.Enabled = True
        lblExtrafanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbExtrafanarts.Image.Width, pbExtrafanarts.Image.Height)
        lblExtrafanartsSize.Visible = True
    End Sub

    Private Sub btnExtrafanartsRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExtrafanartsRemove.Click
        RemoveExtrafanart()
    End Sub

    Private Sub RemoveExtrafanart()
        If pbExtrafanarts.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Extrafanarts.Remove(DirectCast(pbExtrafanarts.Tag, MediaContainers.Image))
            RefreshExtrafanarts()
            lblExtrafanartsSize.Text = ""
            lblExtrafanartsSize.Visible = False
        End If
    End Sub

    Private Sub btnExtrafanartsSetAsFanart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExtrafanartsSetAsFanart.Click
        If pbExtrafanarts.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = DirectCast(pbExtrafanarts.Tag, MediaContainers.Image)
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnExtrafanartsRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExtrafanartsRefresh.Click
        RefreshExtrafanarts()
    End Sub

    Private Sub btnActorAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorAdd.Click
        Using dAddEditActor As New dlgAddEditActor
            If dAddEditActor.ShowDialog() = DialogResult.OK Then
                Dim nActor As MediaContainers.Person = dAddEditActor.Result
                Dim lvItem As ListViewItem = lvActors.Items.Add(nActor.ID.ToString)
                lvItem.Tag = nActor
                lvItem.SubItems.Add(nActor.Name)
                lvItem.SubItems.Add(nActor.Role)
                lvItem.SubItems.Add(nActor.URLOriginal)
            End If
        End Using
    End Sub

    Private Sub btnActorDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorDown.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) < (lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex + 2, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex)
            lvActors.Items(iIndex + 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnActorEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorEdit.Click
        ActorEdit()
    End Sub

    Private Sub btnActorRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorRemove.Click
        ActorRemove()
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnActorUp.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex - 1, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex + 1)
            lvActors.Items(iIndex - 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnManual_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnManual.Click
        If dlgManualEdit.ShowDialog(tmpDBElement.NfoPath) = DialogResult.OK Then
            tmpDBElement.TVShow = NFO.LoadFromNFO_TVShow(tmpDBElement.NfoPath)
            FillInfo()
        End If
    End Sub

    Private Sub btnRemoveBanner_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveBanner.Click
        pbBanner.Image = Nothing
        pbBanner.Tag = Nothing
        lblBannerSize.Text = String.Empty
        lblBannerSize.Visible = False
        tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveCharacterArt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveCharacterArt.Click
        pbCharacterArt.Image = Nothing
        pbCharacterArt.Tag = Nothing
        lblCharacterArtSize.Text = String.Empty
        lblCharacterArtSize.Visible = False
        tmpDBElement.ImagesContainer.CharacterArt = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearArt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveClearArt.Click
        pbClearArt.Image = Nothing
        pbClearArt.Tag = Nothing
        lblClearArtSize.Text = String.Empty
        lblClearArtSize.Visible = False
        tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearLogo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveClearLogo.Click
        pbClearLogo.Image = Nothing
        pbClearLogo.Tag = Nothing
        lblClearLogoSize.Text = String.Empty
        lblClearLogoSize.Visible = False
        tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveFanart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveFanart.Click
        pbFanart.Image = Nothing
        pbFanart.Tag = Nothing
        lblFanartSize.Text = String.Empty
        lblFanartSize.Visible = False
        tmpDBElement.ImagesContainer.Fanart = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveLandscape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveLandscape.Click
        pbLandscape.Image = Nothing
        pbLandscape.Tag = Nothing
        lblLandscapeSize.Text = String.Empty
        lblLandscapeSize.Visible = False
        tmpDBElement.ImagesContainer.Landscape = New MediaContainers.Image
    End Sub

    Private Sub btnRemovePoster_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemovePoster.Click
        pbPoster.Image = Nothing
        pbPoster.Tag = Nothing
        lblPosterSize.Text = String.Empty
        lblPosterSize.Visible = False
        tmpDBElement.ImagesContainer.Poster = New MediaContainers.Image
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainBanners.Count > 0 Then
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

    Private Sub btnSetBannerLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBannerLocal.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBannerDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetCharacterArtScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetCharacterArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainCharacterArt, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainCharacterArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.CharacterArt = dlgImgS.Result.ImagesContainer.CharacterArt
                    If tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.LoadFromMemoryStream Then
                        pbCharacterArt.Image = tmpDBElement.ImagesContainer.CharacterArt.ImageOriginal.Image
                        lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbCharacterArt.Image.Width, pbCharacterArt.Image.Height)
                        lblCharacterArtSize.Visible = True
                    Else
                        pbCharacterArt.Image = Nothing
                        pbCharacterArt.Tag = Nothing
                        lblCharacterArtSize.Text = String.Empty
                        lblCharacterArtSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1343, "No CharacterArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetCharacterArtLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetCharacterArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.CharacterArt = tImage
                    pbCharacterArt.Image = tImage.ImageOriginal.Image
                    pbCharacterArt.Tag = tImage

                    lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbCharacterArt.Image.Width, pbCharacterArt.Image.Height)
                    lblCharacterArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetCharacterArtDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetCharacterArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        tmpDBElement.ImagesContainer.CharacterArt = tImage
                        pbCharacterArt.Image = tImage.ImageOriginal.Image
                        pbCharacterArt.Tag = tImage

                        lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbCharacterArt.Image.Width, pbCharacterArt.Image.Height)
                        lblCharacterArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearArtScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainClearArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.ClearArt = dlgImgS.Result.ImagesContainer.ClearArt
                    If tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.LoadFromMemoryStream Then
                        pbClearArt.Image = tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                        lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
                        lblClearArtSize.Visible = True
                    Else
                        pbClearArt.Image = Nothing
                        pbClearArt.Tag = Nothing
                        lblClearArtSize.Text = String.Empty
                        lblClearArtSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetClearArtLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.ClearArt = tImage
                    pbClearArt.Image = tImage.ImageOriginal.Image
                    pbClearArt.Tag = tImage

                    lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
                    lblClearArtSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearArtDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        tmpDBElement.ImagesContainer.ClearArt = tImage
                        pbClearArt.Image = tImage.ImageOriginal.Image
                        pbClearArt.Tag = tImage

                        lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
                        lblClearArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearLogoScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearLogoScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainClearLogos.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.ClearLogo = dlgImgS.Result.ImagesContainer.ClearLogo
                    If dlgImgS.Result.ImagesContainer.ClearLogo.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.LoadFromMemoryStream Then
                        pbClearLogo.Image = tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                        lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
                        lblClearLogoSize.Visible = True
                    Else
                        pbClearLogo.Image = Nothing
                        pbClearLogo.Tag = Nothing
                        lblClearLogoSize.Text = String.Empty
                        lblClearLogoSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetClearLogoLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearLogoLocal.Click
        Try
            With ofdImage
                .InitialDirectory = tmpDBElement.ShowPath
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                Dim tImage As New MediaContainers.Image
                tImage.ImageOriginal.LoadFromFile(ofdImage.FileName, True)
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.ClearLogo = tImage
                    pbClearLogo.Image = tImage.ImageOriginal.Image
                    pbClearLogo.Tag = tImage

                    lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
                    lblClearLogoSize.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearLogoDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearLogoDL.Click
        Try
            Using dImgManual As New dlgImgManual
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    Dim tImage As MediaContainers.Image = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        tmpDBElement.ImagesContainer.ClearLogo = tImage
                        pbClearLogo.Image = tImage.ImageOriginal.Image
                        pbClearLogo.Tag = tImage

                        lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
                        lblClearLogoSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainFanarts.Count > 0 Then
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

    Private Sub btnSetFanartLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartLocal.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainLandscapes.Count > 0 Then
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

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetLandscapeLocal.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetLandscapeDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_TV(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainPosters.Count > 0 Then
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

    Private Sub btnSetPosterLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterLocal.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
        Try
            'f'in MS and them leaving control arrays out of VB.NET
            With Me
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing
                .pbStar6.Image = Nothing
                .pbStar7.Image = Nothing
                .pbStar8.Image = Nothing
                .pbStar9.Image = Nothing
                .pbStar10.Image = Nothing

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starempty
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starempty
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starempty
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starempty
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starempty
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 5.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.starhalf
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starempty
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 6.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.starhalf
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starempty
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 7.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.starhalf
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starempty
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 8.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.starhalf
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starempty
                        Case Is <= 9.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                            .pbStar6.Image = My.Resources.star
                            .pbStar7.Image = My.Resources.star
                            .pbStar8.Image = My.Resources.star
                            .pbStar9.Image = My.Resources.star
                            .pbStar10.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub dlgEditShow_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If tmpDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tmpDBElement, True) Then
            If Not Master.eSettings.TVShowBannerAnyEnabled Then tcEdit.TabPages.Remove(tpBanner)
            If Not Master.eSettings.TVShowCharacterArtAnyEnabled Then tcEdit.TabPages.Remove(tpCharacterArt)
            If Not Master.eSettings.TVShowClearArtAnyEnabled Then tcEdit.TabPages.Remove(tpClearArt)
            If Not Master.eSettings.TVShowClearLogoAnyEnabled Then tcEdit.TabPages.Remove(tpClearLogo)
            If Not Master.eSettings.TVShowExtrafanartsAnyEnabled Then tcEdit.TabPages.Remove(tpExtrafanarts)
            If Not Master.eSettings.TVShowFanartAnyEnabled Then tcEdit.TabPages.Remove(tpFanart)
            If Not Master.eSettings.TVShowLandscapeAnyEnabled Then tcEdit.TabPages.Remove(tpLandscape)
            If Not Master.eSettings.TVShowPosterAnyEnabled Then tcEdit.TabPages.Remove(tpPoster)

            pbBanner.AllowDrop = True
            pbCharacterArt.AllowDrop = True
            pbClearArt.AllowDrop = True
            pbClearLogo.AllowDrop = True
            pbFanart.AllowDrop = True
            pbLandscape.AllowDrop = True
            pbPoster.AllowDrop = True

            SetUp()

            lvwActorSorter = New ListViewColumnSorter()
            lvActors.ListViewItemSorter = lvwActorSorter

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            LoadGenres()
            LoadRatings()

            FillInfo()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub FillInfo()
        cbOrdering.SelectedIndex = tmpDBElement.Ordering
        cbEpisodeSorting.SelectedIndex = tmpDBElement.EpisodeSorting
        If cbSourceLanguage.Items.Count > 0 Then
            Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = tmpDBElement.Language)
            If tLanguage IsNot Nothing Then
                cbSourceLanguage.Text = tLanguage.Description
            Else
                tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(tmpDBElement.Language_Main))
                If tLanguage IsNot Nothing Then
                    cbSourceLanguage.Text = tLanguage.Description
                Else
                    cbSourceLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                End If
            End If
        End If

        txtTitle.Text = tmpDBElement.TVShow.Title
        txtOriginalTitle.Text = tmpDBElement.TVShow.OriginalTitle
        txtPlot.Text = tmpDBElement.TVShow.Plot
        txtPremiered.Text = tmpDBElement.TVShow.Premiered
        txtRuntime.Text = tmpDBElement.TVShow.Runtime
        txtSortTitle.Text = tmpDBElement.TVShow.SortTitle
        txtStatus.Text = tmpDBElement.TVShow.Status
        txtStudio.Text = String.Join(" / ", tmpDBElement.TVShow.Studios.ToArray)
        txtVotes.Text = tmpDBElement.TVShow.Votes

        For i As Integer = 0 To clbGenre.Items.Count - 1
            clbGenre.SetItemChecked(i, False)
        Next
        If tmpDBElement.TVShow.GenresSpecified Then
            Dim genreArray() As String
            genreArray = tmpDBElement.TVShow.Genres.ToArray
            For g As Integer = 0 To genreArray.Count - 1
                If clbGenre.FindString(genreArray(g).Trim) > 0 Then
                    clbGenre.SetItemChecked(clbGenre.FindString(genreArray(g).Trim), True)
                End If
            Next

            If clbGenre.CheckedItems.Count = 0 Then
                clbGenre.SetItemChecked(0, True)
            End If
        Else
            clbGenre.SetItemChecked(0, True)
        End If

        'Actors
        Dim lvItem As ListViewItem
        lvActors.Items.Clear()
        For Each tActor As MediaContainers.Person In tmpDBElement.TVShow.Actors
            lvItem = lvActors.Items.Add(tActor.ID.ToString)
            lvItem.Tag = tActor
            lvItem.SubItems.Add(tActor.Name)
            lvItem.SubItems.Add(tActor.Role)
            lvItem.SubItems.Add(tActor.URLOriginal)
        Next

        Dim tRating As Single = NumUtils.ConvertToSingle(tmpDBElement.TVShow.Rating)
        tmpRating = tRating.ToString
        pbStar1.Tag = tRating
        pbStar2.Tag = tRating
        pbStar3.Tag = tRating
        pbStar4.Tag = tRating
        pbStar5.Tag = tRating
        pbStar6.Tag = tRating
        pbStar7.Tag = tRating
        pbStar8.Tag = tRating
        pbStar9.Tag = tRating
        pbStar10.Tag = tRating
        If tRating > 0 Then BuildStars(tRating)

        SelectMPAA()

        'Images and TabPages
        With tmpDBElement.ImagesContainer

            'Load all images to MemoryStream and Bitmap
            tmpDBElement.LoadAllImages(True, True)

            'Banner
            If Master.eSettings.TVShowBannerAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner) Then
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

            'CharacterArt
            If Master.eSettings.TVShowCharacterArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt) Then
                    btnSetCharacterArtScrape.Enabled = False
                End If
                If .CharacterArt.ImageOriginal.Image IsNot Nothing Then
                    pbCharacterArt.Image = .CharacterArt.ImageOriginal.Image
                    pbCharacterArt.Tag = .CharacterArt

                    lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbCharacterArt.Image.Width, pbCharacterArt.Image.Height)
                    lblCharacterArtSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpCharacterArt)
            End If

            'ClearArt
            If Master.eSettings.TVShowClearArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt) Then
                    btnSetClearArtScrape.Enabled = False
                End If
                If .ClearArt.ImageOriginal.Image IsNot Nothing Then
                    pbClearArt.Image = .ClearArt.ImageOriginal.Image
                    pbClearArt.Tag = .ClearArt

                    lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
                    lblClearArtSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpClearArt)
            End If

            'ClearLogo
            If Master.eSettings.TVShowClearLogoAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo) Then
                    btnSetClearLogoScrape.Enabled = False
                End If
                If .ClearLogo.ImageOriginal.Image IsNot Nothing Then
                    pbClearLogo.Image = .ClearLogo.ImageOriginal.Image
                    pbClearLogo.Tag = .ClearLogo

                    lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
                    lblClearLogoSize.Visible = True
                End If
            Else
                tcEdit.TabPages.Remove(tpClearLogo)
            End If

            'Extrafanarts
            If Master.eSettings.TVShowExtrafanartsAnyEnabled Then
                'If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) Then
                '.btnSetFanartScrape.Enabled = False
                'End If
                If .Extrafanarts.Count > 0 Then
                    Dim iIndex As Integer = 0
                    For Each tImg As MediaContainers.Image In .Extrafanarts
                        AddExtrafanartImage(String.Concat(tImg.Width, " x ", tImg.Height), iIndex, tImg)
                        iIndex += 1
                    Next
                End If
            Else
                tcEdit.TabPages.Remove(tpExtrafanarts)
            End If

            'Fanart
            If Master.eSettings.TVShowFanartAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart) Then
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
            If Master.eSettings.TVShowLandscapeAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape) Then
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
            If Master.eSettings.TVShowPosterAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster) Then
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

    Private Sub lbGenre_ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs) Handles clbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbGenre.Items.Count - 1
                clbGenre.SetItemChecked(i, False)
            Next
        Else
            clbGenre.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub lbMPAA_DoubleClick(sender As Object, e As EventArgs) Handles lbMPAA.DoubleClick
        If lbMPAA.SelectedItems.Count = 1 Then
            If lbMPAA.SelectedIndex = 0 Then
                txtMPAA.Text = String.Empty
            Else
                txtMPAA.Text = lbMPAA.SelectedItem.ToString
            End If
        End If
    End Sub

    Private Sub LoadGenres()
        clbGenre.Items.Add(Master.eLang.None)
        clbGenre.Items.AddRange(APIXML.GetGenreList)
    End Sub

    Private Sub LoadRatings()
        lbMPAA.Items.Add(Master.eLang.None)
        If Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then lbMPAA.Items.Add(Master.eSettings.TVScraperShowMPAANotRated)
        lbMPAA.Items.AddRange(APIXML.GetRatingList_TV)
    End Sub

    Private Sub lvActors_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActors.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvActors.DoubleClick
        ActorEdit()
    End Sub

    Private Sub lvExtrafanart_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'If e.KeyCode = Keys.Delete Then DeleteExtrafanarts()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        SetInfo()

        DialogResult = DialogResult.OK
    End Sub

    Private Sub pbBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Banner = tImage
            pbBanner.Image = tImage.ImageOriginal.Image
            pbBanner.Tag = tImage
            lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
            lblBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbCharacterArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbCharacterArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.CharacterArt = tImage
            pbCharacterArt.Image = tImage.ImageOriginal.Image
            pbCharacterArt.Tag = tImage
            lblCharacterArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbCharacterArt.Image.Width, pbCharacterArt.Image.Height)
            lblCharacterArtSize.Visible = True
        End If
    End Sub

    Private Sub pbCharacterArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbCharacterArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbClearArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbClearArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.ClearArt = tImage
            pbClearArt.Image = tImage.ImageOriginal.Image
            pbClearArt.Tag = tImage
            lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
            lblClearArtSize.Visible = True
        End If
    End Sub

    Private Sub pbClearArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbClearArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbClearLogo_DragDrop(sender As Object, e As DragEventArgs) Handles pbClearLogo.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.ClearLogo = tImage
            pbClearLogo.Image = tImage.ImageOriginal.Image
            pbClearLogo.Tag = tImage
            lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
            lblClearLogoSize.Visible = True
        End If
    End Sub

    Private Sub pbClearLogo_DragEnter(sender As Object, e As DragEventArgs) Handles pbClearLogo.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbFanart.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = tImage
            pbFanart.Image = tImage.ImageOriginal.Image
            pbFanart.Tag = tImage
            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbLandscape.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Landscape = tImage
            pbLandscape.Image = tImage.ImageOriginal.Image
            pbLandscape.Tag = tImage
            lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
            lblLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbPoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbPoster.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Poster = tImage
            pbPoster.Image = tImage.ImageOriginal.Image
            pbPoster.Tag = tImage
            lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
            lblPosterSize.Visible = True
        End If
    End Sub

    Private Sub pbPoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbPoster.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbStar1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar1.Click
        tmpRating = pbStar1.Tag.ToString
    End Sub

    Private Sub pbStar1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar1.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar1.MouseMove
        Try
            If e.X < 12 Then
                pbStar1.Tag = 0.5
                BuildStars(0.5)
            Else
                pbStar1.Tag = 1
                BuildStars(1)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar2.Click
        tmpRating = pbStar2.Tag.ToString
    End Sub

    Private Sub pbStar2_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar2.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar2.MouseMove
        Try
            If e.X < 12 Then
                pbStar2.Tag = 1.5
                BuildStars(1.5)
            Else
                pbStar2.Tag = 2
                BuildStars(2)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar3.Click
        tmpRating = pbStar3.Tag.ToString
    End Sub

    Private Sub pbStar3_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar3.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar3.MouseMove
        Try
            If e.X < 12 Then
                pbStar3.Tag = 2.5
                BuildStars(2.5)
            Else
                pbStar3.Tag = 3
                BuildStars(3)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar4.Click
        tmpRating = pbStar4.Tag.ToString
    End Sub

    Private Sub pbStar4_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar4.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar4.MouseMove
        Try
            If e.X < 12 Then
                pbStar4.Tag = 3.5
                BuildStars(3.5)
            Else
                pbStar4.Tag = 4
                BuildStars(4)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar5.Click
        tmpRating = pbStar5.Tag.ToString
    End Sub

    Private Sub pbStar5_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar5.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar5.MouseMove
        Try
            If e.X < 12 Then
                pbStar5.Tag = 4.5
                BuildStars(4.5)
            Else
                pbStar5.Tag = 5
                BuildStars(5)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar6.Click
        tmpRating = pbStar6.Tag.ToString
    End Sub

    Private Sub pbStar6_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar6.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar6.MouseMove
        Try
            If e.X < 12 Then
                pbStar6.Tag = 5.5
                BuildStars(5.5)
            Else
                pbStar6.Tag = 6
                BuildStars(6)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar7.Click
        tmpRating = pbStar7.Tag.ToString
    End Sub

    Private Sub pbStar7_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar7.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar7.MouseMove
        Try
            If e.X < 12 Then
                pbStar7.Tag = 6.5
                BuildStars(6.5)
            Else
                pbStar7.Tag = 7
                BuildStars(7)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar8.Click
        tmpRating = pbStar8.Tag.ToString
    End Sub

    Private Sub pbStar8_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar8.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar8_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar8.MouseMove
        Try
            If e.X < 12 Then
                pbStar8.Tag = 7.5
                BuildStars(7.5)
            Else
                pbStar8.Tag = 8
                BuildStars(8)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar9_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar9.Click
        tmpRating = pbStar9.Tag.ToString
    End Sub

    Private Sub pbStar9_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar9.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar9_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar9.MouseMove
        Try
            If e.X < 12 Then
                pbStar9.Tag = 8.5
                BuildStars(8.5)
            Else
                pbStar9.Tag = 9
                BuildStars(9)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar10_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar10.Click
        tmpRating = pbStar10.Tag.ToString
    End Sub

    Private Sub pbStar10_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbStar10.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(tmpRating, tmpDBL)
            BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub pbStar10_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar10.MouseMove
        Try
            If e.X < 12 Then
                pbStar10.Tag = 9.5
                BuildStars(9.5)
            Else
                pbStar10.Tag = 10
                BuildStars(10)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub RefreshExtrafanarts()
        'pnlExtrafanarts.AutoScrollPosition = New Point(0, 0)
        iEFTop = 1
        While pnlExtrafanarts.Controls.Count > 0
            pnlExtrafanarts.Controls(0).Dispose()
        End While

        If tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In tmpDBElement.ImagesContainer.Extrafanarts
                AddExtrafanartImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub SelectMPAA()
        Try
            If Not String.IsNullOrEmpty(tmpDBElement.TVShow.MPAA) Then
                Dim i As Integer = 0
                For ctr As Integer = 0 To lbMPAA.Items.Count - 1
                    If tmpDBElement.TVShow.MPAA.ToLower.StartsWith(lbMPAA.Items.Item(ctr).ToString.ToLower) Then
                        i = ctr
                        Exit For
                    End If
                Next
                lbMPAA.SelectedIndex = i
                lbMPAA.TopIndex = i
                txtMPAA.Text = tmpDBElement.TVShow.MPAA
            End If

            If lbMPAA.SelectedItems.Count = 0 Then
                lbMPAA.SelectedIndex = 0
                lbMPAA.TopIndex = 0
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            txtPlot.SelectAll()
        End If
    End Sub

    Private Sub SetInfo()
        tmpDBElement.Ordering = DirectCast(cbOrdering.SelectedIndex, Enums.EpisodeOrdering)
        tmpDBElement.EpisodeSorting = DirectCast(cbEpisodeSorting.SelectedIndex, Enums.EpisodeSorting)

        If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
            tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
        Else
            tmpDBElement.Language = "en-US"
        End If

        tmpDBElement.TVShow.Title = txtTitle.Text.Trim
        tmpDBElement.TVShow.OriginalTitle = txtOriginalTitle.Text.Trim
        tmpDBElement.TVShow.Plot = txtPlot.Text.Trim
        tmpDBElement.TVShow.Premiered = txtPremiered.Text.Trim
        tmpDBElement.TVShow.Runtime = txtRuntime.Text.Trim
        tmpDBElement.TVShow.SortTitle = txtSortTitle.Text.Trim
        tmpDBElement.TVShow.Status = txtStatus.Text.Trim
        tmpDBElement.TVShow.AddStudiosFromString(txtStudio.Text.Trim)
        tmpDBElement.TVShow.Votes = txtVotes.Text.Trim

        If Not String.IsNullOrEmpty(txtTitle.Text) Then
            If Master.eSettings.TVDisplayStatus AndAlso Not String.IsNullOrEmpty(txtStatus.Text.Trim) Then
                tmpDBElement.ListTitle = String.Format("{0} ({1})", StringUtils.SortTokens_TV(txtTitle.Text.Trim), txtStatus.Text.Trim)
            Else
                tmpDBElement.ListTitle = StringUtils.SortTokens_TV(txtTitle.Text.Trim)
            End If
        End If

        tmpDBElement.TVShow.MPAA = txtMPAA.Text.Trim

        tmpDBElement.TVShow.Rating = tmpRating

        If clbGenre.CheckedItems.Count > 0 Then

            If clbGenre.CheckedIndices.Contains(0) Then
                tmpDBElement.TVShow.Genres.Clear()
            Else
                Dim strGenre As String = String.Empty
                Dim isFirst As Boolean = True
                Dim iChecked = From iCheck In clbGenre.CheckedItems
                strGenre = String.Join(" / ", iChecked.ToArray)
                tmpDBElement.TVShow.AddGenresFromString(strGenre.Trim)
            End If
        End If

        'Actors
        tmpDBElement.TVShow.Actors.Clear()
        If lvActors.Items.Count > 0 Then
            Dim iOrder As Integer = 0
            For Each lviActor As ListViewItem In lvActors.Items
                Dim addActor As MediaContainers.Person = DirectCast(lviActor.Tag, MediaContainers.Person)
                addActor.Order = iOrder
                iOrder += 1
                tmpDBElement.TVShow.Actors.Add(addActor)
            Next
        End If
    End Sub

    Private Sub SetUp()
        'Download
        Dim strDownload As String = Master.eLang.GetString(373, "Download")
        btnSetBannerDL.Text = strDownload
        btnSetCharacterArtDL.Text = strDownload
        btnSetClearArtDL.Text = strDownload
        btnSetClearLogoDL.Text = strDownload
        btnSetFanartDL.Text = strDownload
        btnSetLandscapeDL.Text = strDownload
        btnSetPosterDL.Text = strDownload

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        btnSetBannerLocal.Text = strLocalBrowse
        btnSetCharacterArtLocal.Text = strLocalBrowse
        btnSetClearArtLocal.Text = strLocalBrowse
        btnSetClearLogoLocal.Text = strLocalBrowse
        btnSetFanartLocal.Text = strLocalBrowse
        btnSetLandscapeLocal.Text = strLocalBrowse
        btnSetPosterLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        btnRemoveBanner.Text = strRemove
        btnRemoveCharacterArt.Text = strRemove
        btnRemoveClearArt.Text = strRemove
        btnRemoveClearLogo.Text = strRemove
        btnRemoveFanart.Text = strRemove
        btnRemoveLandscape.Text = strRemove
        btnRemovePoster.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        btnSetBannerScrape.Text = strScrape
        btnSetCharacterArtScrape.Text = strScrape
        btnSetClearArtScrape.Text = strScrape
        btnSetClearLogoScrape.Text = strScrape
        btnSetFanartScrape.Text = strScrape
        btnSetLandscapeScrape.Text = strScrape
        btnSetPosterScrape.Text = strScrape

        Dim mTitle As String = tmpDBElement.TVShow.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(663, "Edit Show"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Text = sTitle
        btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        btnExtrafanartsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        colName.Text = Master.eLang.GetString(232, "Name")
        colRole.Text = Master.eLang.GetString(233, "Role")
        colThumb.Text = Master.eLang.GetString(234, "Thumb")
        lblActors.Text = String.Concat(Master.eLang.GetString(231, "Actors"), ":")
        lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        lblEpisodeSorting.Text = String.Concat(Master.eLang.GetString(364, "Show Episodes by"), ":")
        lblLanguage.Text = Master.eLang.GetString(610, "Language")
        lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        lblOrdering.Text = Master.eLang.GetString(739, "Episode Ordering:")
        lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        lblPremiered.Text = String.Concat(Master.eLang.GetString(724, "Premiered"), ":")
        lblRating.Text = Master.eLang.GetString(245, "Rating:")
        lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        lblStatus.Text = String.Concat(Master.eLang.GetString(215, "Status"), ":")
        lblStudio.Text = String.Concat(Master.eLang.GetString(395, "Studio"), ":")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        lblTopDetails.Text = Master.eLang.GetString(664, "Edit the details for the selected show.")
        lblTopTitle.Text = Master.eLang.GetString(663, "Edit Show")
        lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        tpBanner.Text = Master.eLang.GetString(838, "Banner")
        tpCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tpExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        tpLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        tpPoster.Text = Master.eLang.GetString(148, "Poster")

        cbOrdering.Items.Clear()
        cbOrdering.Items.AddRange(New String() {Master.eLang.GetString(438, "Standard"), Master.eLang.GetString(1067, "DVD"), Master.eLang.GetString(839, "Absolute"), Master.eLang.GetString(1332, "Day Of Year")})

        cbEpisodeSorting.Items.Clear()
        cbEpisodeSorting.Items.AddRange(New String() {Master.eLang.GetString(755, "Episode #"), Master.eLang.GetString(728, "Aired")})

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class