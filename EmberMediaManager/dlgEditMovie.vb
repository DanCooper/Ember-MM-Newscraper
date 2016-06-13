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
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgEditMovie

#Region "Events"

#End Region 'Events

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private CachePath As String = String.Empty
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private lvwActorSorter As ListViewColumnSorter
    Private pResults As New Containers.ImgResult
    Private PreviousFrameValue As Integer
    Private MovieTheme As New Themes With {.isEdit = True}
    Private tmpRating As String = String.Empty
    Private AnyThemePlayerEnabled As Boolean = False
    Private AnyTrailerPlayerEnabled As Boolean = False

    'Extrafanarts
    Private ExtrafanartsWarning As Boolean = True
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbExtrafanartsImage() As PictureBox
    Private pnlExtrafanartsImage() As Panel

    'Extrathumbs
    Private ExtrathumbsWarning As Boolean = True
    Private iETCounter As Integer = 0
    Private iETLeft As Integer = 1
    Private iETTop As Integer = 1
    Private pbExtrathumbsImage() As PictureBox
    Private pnlExtrathumbsImage() As Panel
    Private currExtrathumbImage As New MediaContainers.Image

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

    Public Overloads Function ShowDialog(ByVal DBMovie As Database.DBElement) As DialogResult
        tmpDBElement = DBMovie
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
                tImage.LoadAndCache(Enums.ContentType.Movie, True, True)
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

    Private Sub AddExtrathumbImage(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image)
        Try
            If tImage.ImageOriginal.Image Is Nothing Then
                tImage.LoadAndCache(Enums.ContentType.Movie, True, True)
            End If

            ReDim Preserve pnlExtrathumbsImage(iIndex)
            ReDim Preserve pbExtrathumbsImage(iIndex)
            pnlExtrathumbsImage(iIndex) = New Panel()
            pbExtrathumbsImage(iIndex) = New PictureBox()
            pbExtrathumbsImage(iIndex).Name = iIndex.ToString
            pnlExtrathumbsImage(iIndex).Name = iIndex.ToString
            pnlExtrathumbsImage(iIndex).Size = New Size(128, 72)
            pbExtrathumbsImage(iIndex).Size = New Size(128, 72)
            pnlExtrathumbsImage(iIndex).BackColor = Color.White
            pnlExtrathumbsImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            pbExtrathumbsImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            pnlExtrathumbsImage(iIndex).Tag = tImage
            pbExtrathumbsImage(iIndex).Tag = tImage
            pbExtrathumbsImage(iIndex).Image = tImage.ImageOriginal.Image
            pnlExtrathumbsImage(iIndex).Left = iETLeft
            pbExtrathumbsImage(iIndex).Left = 0
            pnlExtrathumbsImage(iIndex).Top = iETTop
            pbExtrathumbsImage(iIndex).Top = 0
            pnlExtrathumbs.Controls.Add(pnlExtrathumbsImage(iIndex))
            pnlExtrathumbsImage(iIndex).Controls.Add(pbExtrathumbsImage(iIndex))
            pnlExtrathumbsImage(iIndex).BringToFront()
            AddHandler pbExtrathumbsImage(iIndex).Click, AddressOf pbExtrathumbsImage_Click
            AddHandler pnlExtrathumbsImage(iIndex).Click, AddressOf pnlExtrathumbsImage_Click
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        iETTop += 74

    End Sub

    Private Sub pbExtrafanartsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbExtrathumbsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectExtrathumb(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlExtrafanartsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlExtrathumbsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSelectExtrathumb(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub DoSelectExtrafanart(ByVal iIndex As Integer, tImg As MediaContainers.Image)
        pbExtrafanarts.Image = tImg.ImageOriginal.Image
        pbExtrafanarts.Tag = tImg
        btnExtrafanartsSetAsFanart.Enabled = True
        lblExtrafanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbExtrafanarts.Image.Width, pbExtrafanarts.Image.Height)
        lblExtrafanartsSize.Visible = True
    End Sub

    Private Sub DoSelectExtrathumb(ByVal iIndex As Integer, tImg As MediaContainers.Image)
        currExtrathumbImage = tImg

        pbExtrathumbs.Image = tImg.ImageOriginal.Image
        pbExtrathumbs.Tag = tImg
        btnExtrathumbsSetAsFanart.Enabled = True
        lblExtrathumbsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbExtrathumbs.Image.Width, pbExtrathumbs.Image.Height)
        lblExtrathumbsSize.Visible = True

        If tImg.Index > 0 Then
            btnExtrathumbsUp.Enabled = True
        Else
            btnExtrathumbsUp.Enabled = False
        End If
        If tImg.Index < tmpDBElement.ImagesContainer.Extrathumbs.Count - 1 Then
            btnExtrathumbsDown.Enabled = True
        Else
            btnExtrathumbsDown.Enabled = False
        End If
    End Sub

    Private Sub btnActorAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorAdd.Click
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

    Private Sub btnActorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorDown.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) < (lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex + 2, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex)
            lvActors.Items(iIndex + 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnActorEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorEdit.Click
        ActorEdit()
    End Sub

    Private Sub btnActorRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorRemove.Click
        ActorRemove()
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorUp.Click
        If lvActors.SelectedItems.Count > 0 AndAlso lvActors.SelectedItems(0) IsNot Nothing AndAlso lvActors.SelectedIndices(0) > 0 Then
            Dim iIndex As Integer = lvActors.SelectedIndices(0)
            lvActors.Items.Insert(iIndex - 1, DirectCast(lvActors.SelectedItems(0).Clone, ListViewItem))
            lvActors.Items.RemoveAt(iIndex + 1)
            lvActors.Items(iIndex - 1).Selected = True
            lvActors.Select()
        End If
    End Sub

    Private Sub btnChangeMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMovie.Click
        ThemeStop()
        TrailerStop()
        CleanUp()
        DialogResult = DialogResult.Abort
    End Sub

    Private Sub btnDLTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim aUrlList As New List(Of Themes)
        'Dim tURL As String = String.Empty
        'If Not ModulesManager.Instance.ScrapeTheme_Movie(tmpDBMovie, aUrlList) Then
        '    Using dThemeSelect As New dlgThemeSelect()
        '        MovieTheme = dThemeSelect.ShowDialog(tmpDBMovie, aUrlList)
        '    End Using
        'End If

        'If Not String.IsNullOrEmpty(MovieTheme.URL) Then
        '    'btnPlayTheme.Enabled = True
        'End If
    End Sub

    Private Sub btnDLTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLTrailer.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)
        Dim tURL As String = String.Empty

        Try
            dlgTrlS = New dlgTrailerSelect()
            If dlgTrlS.ShowDialog(tmpDBElement, tList, True, True, True) = DialogResult.OK Then
                tURL = dlgTrlS.Result.URLWebsite
            End If

            If Not String.IsNullOrEmpty(tURL) Then
                btnPlayTrailer.Enabled = True
                txtTrailer.Text = tURL
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnExtrathumbsDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsDown.Click
        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso currExtrathumbImage.Index < tmpDBElement.ImagesContainer.Extrathumbs.Count - 1 Then
            Dim iIndex As Integer = currExtrathumbImage.Index
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
            RefreshExtrathumbs()
            DoSelectExtrathumb(iIndex + 1, CType(pnlExtrathumbsImage(iIndex + 1).Tag, MediaContainers.Image))
        End If
    End Sub

    Private Sub btnExtrathumbsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsUp.Click
        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso currExtrathumbImage.Index > 0 Then
            Dim iIndex As Integer = currExtrathumbImage.Index
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
            RefreshExtrathumbs()
            DoSelectExtrathumb(iIndex - 1, CType(pnlExtrathumbsImage(iIndex - 1).Tag, MediaContainers.Image))
        End If
    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        If dlgManualEdit.ShowDialog(tmpDBElement.NfoPath) = DialogResult.OK Then
            tmpDBElement.Movie = NFO.LoadFromNFO_Movie(tmpDBElement.NfoPath, tmpDBElement.IsSingle)
            FillInfo(False)
        End If
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTrailer.Click
        'TODO 2013/12/18 Dekker500 - This should be re-factored to use Functions.Launch. Why is the URL different for non-windows??? Need to test first before editing
        Try

            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(txtTrailer.Text) Then
                tPath = String.Concat("""", txtTrailer.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.isWindows Then
                    If Regex.IsMatch(tPath, "plugin:\/\/plugin\.video\.youtube\/\?action=play_video&videoid=") Then
                        tPath = tPath.Replace("plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
                    End If
                    Process.Start(tPath)
                Else
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "xdg-open"
                        Explorer.StartInfo.Arguments = tPath
                        Explorer.Start()
                    End Using
                End If
            End If

        Catch
            MessageBox.Show(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), Master.eLang.GetString(271, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnPlayTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TODO 2013/12/18 Dekker500 - This should be re-factored to use Functions.Launch. Why is the URL different for non-windows??? Need to test first before editing
        Try

            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(tmpDBElement.ThemePath) Then
                tPath = String.Concat("""", tmpDBElement.ThemePath, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.isWindows Then
                    Process.Start(tPath)
                Else
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "xdg-open"
                        Explorer.StartInfo.Arguments = tPath
                        Explorer.Start()
                    End Using
                End If
            End If

        Catch
            MessageBox.Show(Master.eLang.GetString(1078, "The theme could not be played. This could due be you don't have the proper player to play the theme type."), Master.eLang.GetString(1079, "Error Playing Theme"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnRemoveBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveBanner.Click
        pbBanner.Image = Nothing
        pbBanner.Tag = Nothing
        lblBannerSize.Text = String.Empty
        lblBannerSize.Visible = False
        tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearArt.Click
        pbClearArt.Image = Nothing
        pbClearArt.Tag = Nothing
        lblClearArtSize.Text = String.Empty
        lblClearArtSize.Visible = False
        tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearLogo.Click
        pbClearLogo.Image = Nothing
        pbClearLogo.Tag = Nothing
        lblClearLogoSize.Text = String.Empty
        lblClearLogoSize.Visible = False
        tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveDiscArt.Click
        pbDiscArt.Image = Nothing
        pbDiscArt.Tag = Nothing
        lblDiscArtSize.Text = String.Empty
        lblDiscArtSize.Visible = False
        tmpDBElement.ImagesContainer.DiscArt = New MediaContainers.Image
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

    Private Sub btnRemoveSubtitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSubtitle.Click
        DeleteSubtitle()
    End Sub

    Private Sub btnRemoveTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTheme.Click
        ThemeStop()
        'axVLCTheme.playlist.items.clear()
        MovieTheme.Dispose()
        MovieTheme.toRemove = True
    End Sub

    Private Sub btnRemoveTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTrailer.Click
        TrailerStop()
        TrailerPlaylistClear()
        tmpDBElement.Trailer = New MediaContainers.Trailer
    End Sub

    Private Sub btnExtrafanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsRemove.Click
        RemoveExtrafanart()
    End Sub

    Private Sub btnExtrathumbsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsRemove.Click
        RemoveExtrathumb()
    End Sub

    Private Sub RemoveExtrafanart()
        If pbExtrafanarts.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Extrafanarts.Remove(DirectCast(pbExtrafanarts.Tag, MediaContainers.Image))
            RefreshExtrafanarts()
            lblExtrafanartsSize.Text = String.Empty
            lblExtrafanartsSize.Visible = False
        End If
    End Sub

    Private Sub RemoveExtrathumb()
        If pbExtrathumbs.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Extrathumbs.Remove(DirectCast(pbExtrathumbs.Tag, MediaContainers.Image))
            RefreshExtrathumbs()
            lblExtrafanartsSize.Text = String.Empty
            lblExtrafanartsSize.Visible = False
        End If
    End Sub

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        ThemeStop()
        TrailerStop()
        CleanUp()
        DialogResult = DialogResult.Retry
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Banner = tImage
                    pbBanner.Image = tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                    pbBanner.Tag = tmpDBElement.ImagesContainer.Banner

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
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.Banner.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbBanner.Image = tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
            pbBanner.Tag = tmpDBElement.ImagesContainer.Banner

            lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbBanner.Image.Width, pbBanner.Image.Height)
            lblBannerSize.Visible = True
        End If
    End Sub

    Private Sub btnSetClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.ClearArt = tImage
                    pbClearArt.Image = tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                    pbClearArt.Tag = tmpDBElement.ImagesContainer.ClearArt

                    lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
                    lblClearArtSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbClearArt.Image = tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
            pbClearArt.Tag = tmpDBElement.ImagesContainer.ClearArt

            lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearArt.Image.Width, pbClearArt.Image.Height)
            lblClearArtSize.Visible = True
        End If
    End Sub

    Private Sub btnSetClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.ClearLogo = tImage
                    pbClearLogo.Image = tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                    pbClearLogo.Tag = tmpDBElement.ImagesContainer.ClearLogo

                    lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
                    lblClearLogoSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbClearLogo.Image = tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
            pbClearLogo.Tag = tmpDBElement.ImagesContainer.ClearLogo

            lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbClearLogo.Image.Width, pbClearLogo.Image.Height)
            lblClearLogoSize.Visible = True
        End If
    End Sub

    Private Sub btnSetDiscArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.DiscArt = tImage
                    pbDiscArt.Image = tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
                    pbDiscArt.Tag = tmpDBElement.ImagesContainer.DiscArt

                    lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbDiscArt.Image.Width, pbDiscArt.Image.Height)
                    lblDiscArtSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetDiscArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainDiscArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.DiscArt = dlgImgS.Result.ImagesContainer.DiscArt
                    If tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image IsNot Nothing OrElse tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.LoadFromMemoryStream Then
                        pbDiscArt.Image = tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
                        lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbDiscArt.Image.Width, pbDiscArt.Image.Height)
                        lblDiscArtSize.Visible = True
                    Else
                        pbDiscArt.Image = Nothing
                        pbDiscArt.Tag = Nothing
                        lblDiscArtSize.Text = String.Empty
                        lblDiscArtSize.Visible = False
                    End If
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetDiscArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtLocal.Click

        With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbDiscArt.Image = tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
            pbDiscArt.Tag = tmpDBElement.ImagesContainer.DiscArt

            lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbDiscArt.Image.Width, pbDiscArt.Image.Height)
            lblDiscArtSize.Visible = True
        End If
    End Sub

    Private Sub btnExtrafanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsSetAsFanart.Click
        If pbExtrafanarts.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = DirectCast(pbExtrafanarts.Tag, MediaContainers.Image)
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnExtrathumbsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsSetAsFanart.Click
        If pbExtrathumbs.Tag IsNot Nothing Then
            tmpDBElement.ImagesContainer.Fanart = DirectCast(pbExtrathumbs.Tag, MediaContainers.Image)
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetExtrafanartsScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetExtrafanartsScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrafanarts, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                    RefreshExtrafanarts()
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetExtrathumbsScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetExtrathumbsScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainExtrathumbs, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(tmpDBElement, aContainer, ScrapeModifiers) = DialogResult.OK Then
                    tmpDBElement.ImagesContainer.Extrathumbs = dlgImgS.Result.ImagesContainer.Extrathumbs
                    RefreshExtrathumbs()
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Fanart = tImage
                    pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

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
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 4
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

            lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
            lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Landscape = tImage
                    pbLandscape.Image = tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                    pbLandscape.Tag = tmpDBElement.ImagesContainer.Landscape

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
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.Landscape.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbLandscape.Image = tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
            pbLandscape.Tag = tmpDBElement.ImagesContainer.Landscape

            lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbLandscape.Image.Width, pbLandscape.Image.Height)
            lblLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.ImageOriginal.Image IsNot Nothing Then
                    tmpDBElement.ImagesContainer.Poster = tImage
                    pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                    pbPoster.Tag = tmpDBElement.ImagesContainer.Poster

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
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(tmpDBElement, aContainer, ScrapeModifiers, True) Then
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

    Private Sub btnSetPosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterLocal.Click
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.ImagesContainer.Poster.ImageOriginal.LoadFromFile(ofdLocalFiles.FileName, True)
            pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
            pbPoster.Tag = tmpDBElement.ImagesContainer.Poster

            lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbPoster.Image.Width, pbPoster.Image.Height)
            lblPosterSize.Visible = True
        End If
    End Sub

    'Private Sub btnSetMovieThemeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieThemeDL.Click
    '    Dim tResults As New MediaContainers.Theme
    '    Dim dlgTheS As dlgThemeSelect
    '    Dim tList As New List(Of Themes)

    '    Try
    '        ThemeStop()
    '        dlgTheS = New dlgThemeSelect()
    '        If dlgTheS.ShowDialog(tmpDBMovie, tList) = Windows.Forms.DialogResult.OK Then
    '            tResults = dlgTheS.Results
    '            MovieTheme = dlgTheS.WebTheme
    '            ThemeAddToPlayer(MovieTheme)
    '        End If
    '    Catch ex As Exception
    '        logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    'End Sub

    Private Sub btnSetThemeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeScrape.Click
        Dim dlgThmS As dlgThemeSelect
        Dim aUrlList As New List(Of Themes)

        ThemeStop()
        If Not ModulesManager.Instance.ScrapeTheme_Movie(tmpDBElement, aUrlList) Then
            If aUrlList.Count > 0 Then
                dlgThmS = New dlgThemeSelect()
                If dlgThmS.ShowDialog(tmpDBElement, aUrlList) = DialogResult.OK Then
                    MovieTheme = dlgThmS.Results.WebTheme
                    MovieTheme.isEdit = True
                    ThemeAddToPlayer(MovieTheme)
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1163, "No Themes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetThemeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeLocal.Click
        ThemeStop()
        With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(1285, "Themes") + "|*.mp3;*.wav"
                .FilterIndex = 0
            End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            MovieTheme.FromFile(ofdLocalFiles.FileName)
            MovieTheme.isEdit = True
            ThemeAddToPlayer(MovieTheme)
        End If
    End Sub

    Private Sub btnSetTrailerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerDL.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)

        TrailerStop()
        dlgTrlS = New dlgTrailerSelect()
        If dlgTrlS.ShowDialog(tmpDBElement, tList, False, True, True) = DialogResult.OK Then
            tResults = dlgTrlS.Result
            tmpDBElement.Trailer = tResults
            TrailerPlaylistAdd(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub btnSetTrailerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerScrape.Click
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)

        TrailerStop()
            dlgTrlS = New dlgTrailerSelect()
        If dlgTrlS.ShowDialog(tmpDBElement, tList, False, True, True) = DialogResult.OK Then
            tmpDBElement.Trailer = dlgTrlS.Result
            TrailerPlaylistAdd(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub btnSetTrailerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerLocal.Click
        TrailerStop()
        With ofdLocalFiles
            .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
            .Filter = Master.eLang.GetString(1195, "Trailers") + "|*.mp4;*.avi"
            .FilterIndex = 0
        End With

        If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
            tmpDBElement.Trailer.TrailerOriginal.FromFile(ofdLocalFiles.FileName)
            tmpDBElement.Trailer.TrailerOriginal.isEdit = True
            TrailerPlaylistAdd(tmpDBElement.Trailer)
        End If
    End Sub

    Private Sub btnStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStudio.Click
        Using dStudio As New dlgStudioSelect
            Dim tStudio As String = dStudio.ShowDialog(tmpDBElement)
            If Not String.IsNullOrEmpty(tStudio) Then
                txtStudio.Text = tStudio
            End If
        End Using
    End Sub

    Private Sub btnExtrafanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsRefresh.Click
        RefreshExtrafanarts()
    End Sub

    Private Sub btnExtrathumbsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsRefresh.Click
        RefreshExtrathumbs()
    End Sub

    Private Sub ThemeStart()
        'If axVLCTheme.playlist.isPlaying Then
        '    axVLCTheme.playlist.togglePause()
        '    btnThemePlay.Text = "Play"
        'Else
        '    axVLCTheme.playlist.play()
        '    btnThemePlay.Text = "Pause"
        'End If
    End Sub

    Private Sub ThemeStop()
        'axVLCTheme.playlist.stop()
        'btnThemePlay.Text = "Play"
    End Sub

    Private Sub ThemeAddToPlayer(ByVal Theme As Themes)
        'Dim Link As String = String.Empty
        'axVLCTheme.playlist.stop()
        'axVLCTheme.playlist.items.clear()
    End Sub

    Private Sub TrailerPlaylistAdd(ByVal Trailer As MediaContainers.Trailer)
        If AnyTrailerPlayerEnabled Then
            Dim paramsTrailerPreview As New List(Of Object)(New String() {Trailer.URLVideoStream})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayerPlaylistAdd_Video, paramsTrailerPreview, Nothing, True)
        End If
    End Sub

    Private Sub TrailerPlaylistClear()
        If AnyTrailerPlayerEnabled Then
            Dim paramsTrailerPreview As New List(Of Object)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayerPlaylistClear_Video, Nothing, Nothing, True)
        End If
    End Sub

    Private Sub TrailerStop()
        If AnyTrailerPlayerEnabled Then
            Dim paramsTrailerPreview As New List(Of Object)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayerStop_Video, Nothing, Nothing, True)
        End If
    End Sub

    Private Sub BuildStars(ByVal sinRating As Single)
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
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        ThemeStop()
        TrailerStop()
        CleanUp()
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub CleanUp()
        If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
            File.Delete(Path.Combine(Master.TempPath, "frame.jpg"))
        End If

        If Directory.Exists(Path.Combine(Master.TempPath, "extrathumbs")) Then
            FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrathumbs"))
        End If

        If Directory.Exists(Path.Combine(Master.TempPath, "extrafanarts")) Then
            FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrafanarts"))
        End If

        If Directory.Exists(Path.Combine(Master.TempPath, "DashTrailer")) Then
            FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "DashTrailer"))
        End If
    End Sub

    Private Sub DelayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDelay.Tick
        tmrDelay.Stop()
        'GrabTheFrame()
    End Sub

    Private Sub dlgEditMovie_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        MovieTheme.Dispose()
        MovieTheme = Nothing
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pbBanner.AllowDrop = True
        pbClearArt.AllowDrop = True
        pbClearLogo.AllowDrop = True
        pbDiscArt.AllowDrop = True
        pbFanart.AllowDrop = True
        pbLandscape.AllowDrop = True
        pbPoster.AllowDrop = True

        SetUp()
        lvwActorSorter = New ListViewColumnSorter()
        lvActors.ListViewItemSorter = lvwActorSorter
        'lvwExtrathumbsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
        'lvExtrathumbs.ListViewItemSorter = lvwExtrathumbsSorter
        'lvwExtrafanartsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
        'lvExtrafanarts.ListViewItemSorter = lvwExtrafanartsSorter

        Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            pnlTop.BackgroundImage = iBackground
        End Using

        Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement, False)
        dFileInfoEdit.TopLevel = False
        dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
        dFileInfoEdit.BackColor = Color.White
        dFileInfoEdit.btnClose.Visible = False
        pnlFileInfo.Controls.Add(dFileInfoEdit)
        Dim oldwidth As Integer = dFileInfoEdit.Width
        dFileInfoEdit.Width = pnlFileInfo.Width
        dFileInfoEdit.Height = pnlFileInfo.Height
        dFileInfoEdit.Show()

        LoadGenres()
        LoadRatings()

        Dim paramsFrameExtractor As New List(Of Object)(New Object() {New Panel})
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.FrameExtrator_Movie, paramsFrameExtractor, Nothing, True, tmpDBElement)
        pnlFrameExtrator.Controls.Add(DirectCast(paramsFrameExtractor(0), Panel))
        If String.IsNullOrEmpty(pnlFrameExtrator.Controls.Item(0).Name) Then
            tcEdit.TabPages.Remove(tpFrameExtraction)
        End If

        Dim paramsThemePreview As New List(Of Object)(New Object() {New Panel})
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayer_Audio, paramsThemePreview, Nothing, True)
        pnlThemePreview.Controls.Add(DirectCast(paramsThemePreview(0), Panel))
        If Not String.IsNullOrEmpty(pnlThemePreview.Controls.Item(1).Name) Then
            AnyThemePlayerEnabled = True
            pnlThemePreviewNoPlayer.Visible = False
        End If

        Dim paramsTrailerPreview As New List(Of Object)(New Object() {New Panel})
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayer_Video, paramsTrailerPreview, Nothing, True)
        pnlTrailerPreview.Controls.Add(DirectCast(paramsTrailerPreview(0), Panel))
        If Not String.IsNullOrEmpty(pnlTrailerPreview.Controls.Item(1).Name) Then
            AnyTrailerPlayerEnabled = True
            pnlTrailerPreviewNoPlayer.Visible = False
        End If

        FillInfo()
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub FillInfo(Optional ByVal DoAll As Boolean = True)

        If String.IsNullOrEmpty(tmpDBElement.NfoPath) Then
            btnManual.Enabled = False
        End If

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

        chkMark.Checked = tmpDBElement.IsMark
        If tmpDBElement.Movie.PlayCountSpecified Then
            chkWatched.Checked = True
        Else
            chkWatched.Checked = False
        End If

        txtCerts.Text = String.Join(" / ", tmpDBElement.Movie.Certifications.ToArray)
        txtCountries.Text = String.Join(" / ", tmpDBElement.Movie.Countries.ToArray)
        txtCredits.Text = String.Join(" / ", tmpDBElement.Movie.Credits.ToArray)
        txtDirectors.Text = String.Join(" / ", tmpDBElement.Movie.Directors.ToArray)
        txtOriginalTitle.Text = tmpDBElement.Movie.OriginalTitle
        txtOutline.Text = tmpDBElement.Movie.Outline
        txtPlot.Text = tmpDBElement.Movie.Plot
        txtReleaseDate.Text = tmpDBElement.Movie.ReleaseDate
        txtRuntime.Text = tmpDBElement.Movie.Runtime
        txtSortTitle.Text = tmpDBElement.Movie.SortTitle
        txtStudio.Text = String.Join(" / ", tmpDBElement.Movie.Studios.ToArray)
        txtTagline.Text = tmpDBElement.Movie.Tagline
        txtTitle.Text = tmpDBElement.Movie.Title
        txtTop250.Text = tmpDBElement.Movie.Top250
        txtVideoSource.Text = tmpDBElement.Movie.VideoSource
        txtVotes.Text = tmpDBElement.Movie.Votes
        txtYear.Text = tmpDBElement.Movie.Year

        If Not String.IsNullOrEmpty(tmpDBElement.Movie.LastPlayed) Then
            Dim timecode As Double = 0
            Double.TryParse(tmpDBElement.Movie.LastPlayed, timecode)
            If timecode > 0 Then
                txtLastPlayed.Text = Functions.ConvertFromUnixTimestamp(timecode).ToString("yyyy-MM-dd HH:mm:ss")
            Else
                txtLastPlayed.Text = tmpDBElement.Movie.LastPlayed
            End If
        End If

        SelectMPAA()

        If String.IsNullOrEmpty(tmpDBElement.ThemePath) Then
            '.btnPlayTheme.Enabled = False
        End If

        '.btnDLTheme.Enabled = Master.eSettings.MovieThemeEnable AndAlso Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Theme)

        If Not String.IsNullOrEmpty(tmpDBElement.Movie.Trailer) Then
            txtTrailer.Text = tmpDBElement.Movie.Trailer
            btnPlayTrailer.Enabled = True
        Else
            btnPlayTrailer.Enabled = False
        End If

        btnDLTrailer.Enabled = Master.DefaultOptions_Movie.bMainTrailer

        For i As Integer = 0 To clbGenre.Items.Count - 1
            clbGenre.SetItemChecked(i, False)
        Next
        If tmpDBElement.Movie.Genres.Count > 0 Then
            For g As Integer = 0 To tmpDBElement.Movie.Genres.Count - 1
                If clbGenre.FindString(tmpDBElement.Movie.Genres(g).Trim) > 0 Then
                    clbGenre.SetItemChecked(clbGenre.FindString(tmpDBElement.Movie.Genres(g).Trim), True)
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
        For Each tActor As MediaContainers.Person In tmpDBElement.Movie.Actors
            lvItem = lvActors.Items.Add(tActor.ID.ToString)
            lvItem.Tag = tActor
            lvItem.SubItems.Add(tActor.Name)
            lvItem.SubItems.Add(tActor.Role)
            lvItem.SubItems.Add(tActor.URLOriginal)
        Next

        If Not String.IsNullOrEmpty(tmpDBElement.Filename) AndAlso String.IsNullOrEmpty(tmpDBElement.Movie.VideoSource) Then
            Dim vSource As String = APIXML.GetVideoSource(tmpDBElement.Filename, False)
            If Not String.IsNullOrEmpty(vSource) Then
                tmpDBElement.VideoSource = vSource
                tmpDBElement.Movie.VideoSource = tmpDBElement.VideoSource
            ElseIf String.IsNullOrEmpty(tmpDBElement.VideoSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                tmpDBElement.VideoSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(tmpDBElement.Filename)), String.Empty, "*EmberAPP")
                tmpDBElement.Movie.VideoSource = tmpDBElement.VideoSource
            ElseIf Not String.IsNullOrEmpty(tmpDBElement.Movie.VideoSource) Then
                tmpDBElement.VideoSource = tmpDBElement.Movie.VideoSource
            End If
        End If

        Dim tRating As Single = NumUtils.ConvertToSingle(tmpDBElement.Movie.Rating)
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

        If DoAll Then
            Dim pExt As String = Path.GetExtension(tmpDBElement.Filename).ToLower
            If pExt = ".rar" OrElse pExt = ".iso" OrElse pExt = ".img" OrElse
            pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".dat" OrElse
            pExt = ".disc" Then
                tcEdit.TabPages.Remove(tpFrameExtraction)
            Else
                If Not pExt = ".disc" Then
                    tcEdit.TabPages.Remove(tpMediaStub)
                End If
            End If

            'Images and TabPages
            With tmpDBElement.ImagesContainer

                'Load all images to MemoryStream and Bitmap
                tmpDBElement.LoadAllImages(True, True)

                'Banner
                If Master.eSettings.MovieBannerAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner) Then
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

                'ClearArt
                If Master.eSettings.MovieClearArtAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt) Then
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
                If Master.eSettings.MovieClearLogoAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo) Then
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

                'DiscArt
                If Master.eSettings.MovieDiscArtAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt) Then
                        btnSetDiscArtScrape.Enabled = False
                    End If
                    If .DiscArt.ImageOriginal.Image IsNot Nothing Then
                        pbDiscArt.Image = .DiscArt.ImageOriginal.Image
                        pbDiscArt.Tag = .DiscArt

                        lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbDiscArt.Image.Width, pbDiscArt.Image.Height)
                        lblDiscArtSize.Visible = True
                    End If
                Else
                    tcEdit.TabPages.Remove(tpDiscArt)
                End If

                'Extrafanarts
                If Master.eSettings.MovieExtrafanartsAnyEnabled Then
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

                'Extrathumbs
                If Master.eSettings.MovieExtrathumbsAnyEnabled Then
                    'If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) Then
                    '.btnSetFanartScrape.Enabled = False
                    'End If
                    If .Extrathumbs.Count > 0 Then
                        Dim iIndex As Integer = 0
                        For Each tImg As MediaContainers.Image In .Extrathumbs.OrderBy(Function(f) f.Index)
                            AddExtrathumbImage(String.Concat(tImg.Width, " x ", tImg.Height), iIndex, tImg)
                            iIndex += 1
                        Next
                    End If
                Else
                    tcEdit.TabPages.Remove(tpExtrathumbs)
                End If

                'Fanart
                If Master.eSettings.MovieFanartAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) Then
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
                If Master.eSettings.MovieLandscapeAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape) Then
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
                If Master.eSettings.MoviePosterAnyEnabled Then
                    If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster) Then
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

            'Theme
            If Not Master.eSettings.MovieThemeAnyEnabled Then
                tcEdit.TabPages.Remove(tpTheme)
            End If

            If Not String.IsNullOrEmpty(tmpDBElement.ThemePath) AndAlso tmpDBElement.ThemePath.Substring(0, 1) = ":" Then
                MovieTheme.FromWeb(tmpDBElement.ThemePath.Substring(1, tmpDBElement.ThemePath.Length - 1))
                ThemeAddToPlayer(MovieTheme)
            ElseIf Not String.IsNullOrEmpty(tmpDBElement.ThemePath) Then
                MovieTheme.FromFile(tmpDBElement.ThemePath)
                ThemeAddToPlayer(MovieTheme)
            End If

            'Trailer
            If Master.eSettings.MovieTrailerAnyEnabled Then
                If Not String.IsNullOrEmpty(tmpDBElement.Trailer.LocalFilePath) OrElse Not String.IsNullOrEmpty(tmpDBElement.Trailer.URLVideoStream) Then
                    TrailerPlaylistAdd(tmpDBElement.Trailer)
                End If
            Else
                tcEdit.TabPages.Remove(tpTrailer)
            End If

            'DiscStub
            If Path.GetExtension(tmpDBElement.Filename).ToLower = ".disc" Then
                Dim DiscStub As New MediaStub.DiscStub
                DiscStub = MediaStub.LoadDiscStub(tmpDBElement.Filename)
                txtMediaStubTitle.Text = DiscStub.Title
                txtMediaStubMessage.Text = DiscStub.Message
            End If

            LoadSubtitles()
        End If
    End Sub

    Private Sub clbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbGenre.ItemCheck
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
        If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperMPAANotRated) Then lbMPAA.Items.Add(Master.eSettings.MovieScraperMPAANotRated)
        lbMPAA.Items.AddRange(APIXML.GetRatingList_Movie)
    End Sub

    Private Sub lvActors_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.

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
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        ActorEdit()
    End Sub

    Private Sub lvActors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvActors.KeyDown
        If e.KeyCode = Keys.Delete Then ActorRemove()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ThemeStop()
        TrailerStop()

        SetInfo()
        CleanUp()

        DialogResult = DialogResult.OK
    End Sub

    Private Sub pbBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.Banner = tImage
            pbBanner.Image = tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
            pbBanner.Tag = tmpDBElement.ImagesContainer.Banner
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

    Private Sub pbClearArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbClearArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.ClearArt = tImage
            pbClearArt.Image = tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
            pbClearArt.Tag = tmpDBElement.ImagesContainer.ClearArt
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
            pbClearLogo.Image = tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
            pbClearLogo.Tag = tmpDBElement.ImagesContainer.ClearLogo
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

    Private Sub pbDiscArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbDiscArt.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            tmpDBElement.ImagesContainer.DiscArt = tImage
            pbDiscArt.Image = tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
            pbDiscArt.Tag = tmpDBElement.ImagesContainer.DiscArt
            lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbDiscArt.Image.Width, pbDiscArt.Image.Height)
            lblDiscArtSize.Visible = True
        End If
    End Sub

    Private Sub pbDiscArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbDiscArt.DragEnter
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
            pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart
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
            pbLandscape.Image = tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
            pbLandscape.Tag = tmpDBElement.ImagesContainer.Landscape
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
            pbPoster.Image = tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
            pbPoster.Tag = tmpDBElement.ImagesContainer.Poster
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

    Private Sub pbStar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar1.Click
        tmpRating = pbStar1.Tag.ToString
    End Sub

    Private Sub pbStar1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar1.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar1.MouseMove
        If e.X < 12 Then
            pbStar1.Tag = 0.5
            BuildStars(0.5)
        Else
            pbStar1.Tag = 1
            BuildStars(1)
        End If
    End Sub

    Private Sub pbStar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar2.Click
        tmpRating = pbStar2.Tag.ToString
    End Sub

    Private Sub pbStar2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar2.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar2.MouseMove
        If e.X < 12 Then
            pbStar2.Tag = 1.5
            BuildStars(1.5)
        Else
            pbStar2.Tag = 2
            BuildStars(2)
        End If
    End Sub

    Private Sub pbStar3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar3.Click
        tmpRating = pbStar3.Tag.ToString
    End Sub

    Private Sub pbStar3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar3.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar3.MouseMove
        If e.X < 12 Then
            pbStar3.Tag = 2.5
            BuildStars(2.5)
        Else
            pbStar3.Tag = 3
            BuildStars(3)
        End If
    End Sub

    Private Sub pbStar4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar4.Click
        tmpRating = pbStar4.Tag.ToString
    End Sub

    Private Sub pbStar4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar4.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar4.MouseMove
        If e.X < 12 Then
            pbStar4.Tag = 3.5
            BuildStars(3.5)
        Else
            pbStar4.Tag = 4
            BuildStars(4)
        End If
    End Sub

    Private Sub pbStar5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar5.Click
        tmpRating = pbStar5.Tag.ToString
    End Sub

    Private Sub pbStar5_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar5.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar5.MouseMove
        If e.X < 12 Then
            pbStar5.Tag = 4.5
            BuildStars(4.5)
        Else
            pbStar5.Tag = 5
            BuildStars(5)
        End If
    End Sub

    Private Sub pbStar6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar6.Click
        tmpRating = pbStar6.Tag.ToString
    End Sub

    Private Sub pbStar6_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar6.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar6.MouseMove
        If e.X < 12 Then
            pbStar6.Tag = 5.5
            BuildStars(5.5)
        Else
            pbStar6.Tag = 6
            BuildStars(6)
        End If
    End Sub

    Private Sub pbStar7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar7.Click
        tmpRating = pbStar7.Tag.ToString
    End Sub

    Private Sub pbStar7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar7.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar7.MouseMove
        If e.X < 12 Then
            pbStar7.Tag = 6.5
            BuildStars(6.5)
        Else
            pbStar7.Tag = 7
            BuildStars(7)
        End If
    End Sub

    Private Sub pbStar8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar8.Click
        tmpRating = pbStar8.Tag.ToString
    End Sub

    Private Sub pbStar8_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar8.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar8_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar8.MouseMove
        If e.X < 12 Then
            pbStar8.Tag = 7.5
            BuildStars(7.5)
        Else
            pbStar8.Tag = 8
            BuildStars(8)
        End If
    End Sub

    Private Sub pbStar9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar9.Click
        tmpRating = pbStar9.Tag.ToString
    End Sub

    Private Sub pbStar9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar9.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar9_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar9.MouseMove
        If e.X < 12 Then
            pbStar9.Tag = 8.5
            BuildStars(8.5)
        Else
            pbStar9.Tag = 9
            BuildStars(9)
        End If
    End Sub

    Private Sub pbStar10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar10.Click
        tmpRating = pbStar10.Tag.ToString
    End Sub

    Private Sub pbStar10_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar10.MouseLeave
        Dim tmpDBL As Single = 0
        Single.TryParse(tmpRating, tmpDBL)
        BuildStars(tmpDBL)
    End Sub

    Private Sub pbStar10_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar10.MouseMove
        If e.X < 12 Then
            pbStar10.Tag = 9.5
            BuildStars(9.5)
        Else
            pbStar10.Tag = 10
            BuildStars(10)
        End If
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

    Private Sub RefreshExtrathumbs()
        'pnlExtrathumbs.AutoScrollPosition = New Point(0, 0)
        iETTop = 1
        While pnlExtrathumbs.Controls.Count > 0
            pnlExtrathumbs.Controls(0).Dispose()
        End While

        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 Then
            tmpDBElement.ImagesContainer.SortExtrathumbs()
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In tmpDBElement.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                img.Index = iIndex
                AddExtrathumbImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub SelectMPAA()
        If Not String.IsNullOrEmpty(tmpDBElement.Movie.MPAA) Then
            If Master.eSettings.MovieScraperCertOnlyValue Then
                Dim sItem As String = String.Empty
                For i As Integer = 0 To lbMPAA.Items.Count - 1
                    sItem = lbMPAA.Items(i).ToString
                    If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = tmpDBElement.Movie.MPAA Then
                        lbMPAA.SelectedIndex = i
                        lbMPAA.TopIndex = i
                        Exit For
                    End If
                Next
            Else
                Dim i As Integer = 0
                For ctr As Integer = 0 To lbMPAA.Items.Count - 1
                    If tmpDBElement.Movie.MPAA.ToLower.StartsWith(lbMPAA.Items.Item(ctr).ToString.ToLower) Then
                        i = ctr
                        Exit For
                    End If
                Next
                lbMPAA.SelectedIndex = i
                lbMPAA.TopIndex = i

                Dim strMPAA As String = String.Empty
                Dim strMPAADesc As String = String.Empty
                If i > 0 Then
                    strMPAA = lbMPAA.Items.Item(i).ToString
                    strMPAADesc = tmpDBElement.Movie.MPAA.Replace(strMPAA, String.Empty).Trim
                    txtMPAA.Text = strMPAA
                    txtMPAADesc.Text = strMPAADesc
                Else
                    txtMPAA.Text = tmpDBElement.Movie.MPAA
                End If
            End If
        End If

        If lbMPAA.SelectedItems.Count = 0 Then
            lbMPAA.SelectedIndex = 0
            lbMPAA.TopIndex = 0
        End If
    End Sub

    Private Sub SetInfo()
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False
        btnRescrape.Enabled = False
        btnChangeMovie.Enabled = False

        If Not String.IsNullOrEmpty(cbSourceLanguage.Text) Then
            tmpDBElement.Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Description = cbSourceLanguage.Text).Abbreviation
        Else
            tmpDBElement.Language = "en-US"
        End If

        tmpDBElement.IsMark = chkMark.Checked

        tmpDBElement.Movie.SortTitle = txtSortTitle.Text.Trim
        tmpDBElement.Movie.Tagline = txtTagline.Text.Trim
        tmpDBElement.Movie.Year = txtYear.Text.Trim
        tmpDBElement.Movie.Votes = txtVotes.Text.Trim
        tmpDBElement.Movie.Outline = txtOutline.Text.Trim
        tmpDBElement.Movie.Plot = txtPlot.Text.Trim
        tmpDBElement.Movie.Top250 = txtTop250.Text.Trim
        tmpDBElement.Movie.AddCountriesFromString(txtCountries.Text.Trim)
        tmpDBElement.Movie.AddDirectorsFromString(txtDirectors.Text.Trim)
        tmpDBElement.Movie.Title = txtTitle.Text.Trim
        tmpDBElement.Movie.AddCertificationsFromString(txtCerts.Text.Trim)
        tmpDBElement.Movie.OriginalTitle = txtOriginalTitle.Text.Trim
        tmpDBElement.Movie.MPAA = String.Concat(txtMPAA.Text, " ", txtMPAADesc.Text).Trim
        tmpDBElement.Movie.Runtime = txtRuntime.Text.Trim
        tmpDBElement.Movie.ReleaseDate = txtReleaseDate.Text.Trim
        tmpDBElement.Movie.AddCreditsFromString(txtCredits.Text.Trim)
        tmpDBElement.Movie.AddStudiosFromString(txtStudio.Text.Trim)
        tmpDBElement.VideoSource = txtVideoSource.Text.Trim
        tmpDBElement.Movie.VideoSource = txtVideoSource.Text.Trim
        tmpDBElement.Movie.Trailer = txtTrailer.Text.Trim
        tmpDBElement.ListTitle = StringUtils.ListTitle_Movie(txtTitle.Text, txtYear.Text)

        If Not tmpRating.Trim = String.Empty AndAlso tmpRating.Trim <> "0" Then
            tmpDBElement.Movie.Rating = tmpRating
        Else
            tmpDBElement.Movie.Rating = String.Empty
        End If

        'cocotus, 2013/02 Playcount/Watched state support added
        'if watched-checkbox is checked -> save Playcount=1 in nfo
        If chkWatched.Checked Then
            'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
            If Not tmpDBElement.Movie.PlayCountSpecified Then
                tmpDBElement.Movie.PlayCount = 1
                tmpDBElement.Movie.LastPlayed = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
            End If
        Else
            'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
            If tmpDBElement.Movie.PlayCountSpecified Then
                tmpDBElement.Movie.PlayCount = 0
                tmpDBElement.Movie.LastPlayed = String.Empty
            End If
        End If
        'cocotus End

        If clbGenre.CheckedItems.Count > 0 Then
            If clbGenre.CheckedIndices.Contains(0) Then
                tmpDBElement.Movie.Genres.Clear()
            Else
                Dim strGenre As String = String.Empty
                Dim isFirst As Boolean = True
                Dim iChecked = From iCheck In clbGenre.CheckedItems
                strGenre = String.Join(" / ", iChecked.ToArray)
                tmpDBElement.Movie.AddGenresFromString(strGenre)
            End If
        End If

        'Actors
        tmpDBElement.Movie.Actors.Clear()
        If lvActors.Items.Count > 0 Then
            Dim iOrder As Integer = 0
            For Each lviActor As ListViewItem In lvActors.Items
                Dim addActor As MediaContainers.Person = DirectCast(lviActor.Tag, MediaContainers.Person)
                addActor.Order = iOrder
                iOrder += 1
                tmpDBElement.Movie.Actors.Add(addActor)
            Next
        End If

        If Not String.IsNullOrEmpty(MovieTheme.Extention) AndAlso Not MovieTheme.toRemove Then 'TODO: proper check, extention check is only a woraround
            Dim tPath As String = MovieTheme.SaveAsMovieTheme(tmpDBElement)
            tmpDBElement.ThemePath = tPath
        Else
            Themes.DeleteMovieTheme(tmpDBElement)
            tmpDBElement.ThemePath = String.Empty
        End If

        If Path.GetExtension(tmpDBElement.Filename) = ".disc" Then
            Dim StubFile As String = tmpDBElement.Filename
            Dim Title As String = txtMediaStubTitle.Text
            Dim Message As String = txtMediaStubMessage.Text
            MediaStub.SaveDiscStub(StubFile, Title, Message)
        End If

        If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso pResults.Posters.Count > 0 Then tmpDBElement.Movie.Thumb = pResults.Posters
        If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso fResults.Fanart.Thumb.Count > 0 Then tmpDBElement.Movie.Fanart = pResults.Fanart

        Dim removeSubtitles As New List(Of MediaContainers.Subtitle)
        For Each Subtitle In tmpDBElement.Subtitles
            If Subtitle.toRemove Then
                removeSubtitles.Add(Subtitle)
            End If
        Next
        For Each Subtitle In removeSubtitles
            If File.Exists(Subtitle.SubsPath) Then
                File.Delete(Subtitle.SubsPath)
            End If
            tmpDBElement.Subtitles.Remove(Subtitle)
        Next
    End Sub

    Private Sub SetUp()
        'Download
        Dim strDownload As String = Master.eLang.GetString(373, "Download")
        btnSetBannerDL.Text = strDownload
        btnSetClearArtDL.Text = strDownload
        btnSetClearLogoDL.Text = strDownload
        btnSetDiscArtDL.Text = strDownload
        btnSetFanartDL.Text = strDownload
        btnSetLandscapeDL.Text = strDownload
        btnSetPosterDL.Text = strDownload
        btnSetSubtitleDL.Text = strDownload
        btnSetThemeDL.Text = strDownload
        btnSetTrailerDL.Text = strDownload

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        btnSetBannerLocal.Text = strLocalBrowse
        btnSetClearArtLocal.Text = strLocalBrowse
        btnSetClearLogoLocal.Text = strLocalBrowse
        btnSetDiscArtLocal.Text = strLocalBrowse
        btnSetFanartLocal.Text = strLocalBrowse
        btnSetLandscapeLocal.Text = strLocalBrowse
        btnSetPosterLocal.Text = strLocalBrowse
        btnSetSubtitleLocal.Text = strLocalBrowse
        btnSetThemeLocal.Text = strLocalBrowse
        btnSetTrailerLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        btnRemoveBanner.Text = strRemove
        btnRemoveClearArt.Text = strRemove
        btnRemoveClearLogo.Text = strRemove
        btnRemoveDiscArt.Text = strRemove
        btnRemoveFanart.Text = strRemove
        btnRemoveLandscape.Text = strRemove
        btnRemovePoster.Text = strRemove
        btnRemoveSubtitle.Text = strRemove
        btnRemoveTheme.Text = strRemove
        btnRemoveTrailer.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        btnSetBannerScrape.Text = strScrape
        btnSetClearArtScrape.Text = strScrape
        btnSetClearLogoScrape.Text = strScrape
        btnSetDiscArtScrape.Text = strScrape
        btnSetFanartScrape.Text = strScrape
        btnSetLandscapeScrape.Text = strScrape
        btnSetPosterScrape.Text = strScrape
        btnSetSubtitleScrape.Text = strScrape
        btnSetThemeScrape.Text = strScrape
        btnSetTrailerScrape.Text = strScrape

        Dim mTitle As String = tmpDBElement.Movie.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Text = sTitle
        tsFilename.Text = tmpDBElement.Filename
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        btnChangeMovie.Text = Master.eLang.GetString(32, "Change Movie")
        btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        btnExtrafanartsSetAsFanart.Text = btnExtrathumbsSetAsFanart.Text
        btnExtrathumbsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        chkMark.Text = Master.eLang.GetString(23, "Mark")
        chkWatched.Text = Master.eLang.GetString(981, "Watched")
        colName.Text = Master.eLang.GetString(232, "Name")
        colRole.Text = Master.eLang.GetString(233, "Role")
        colThumb.Text = Master.eLang.GetString(234, "Thumb")
        lblActors.Text = String.Concat(Master.eLang.GetString(231, "Actors"), ":")
        lblCerts.Text = String.Concat(Master.eLang.GetString(56, "Certifications"), ":")
        lblCountries.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
        lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        lblDirectors.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblVideoSource.Text = Master.eLang.GetString(824, "Video Source:")
        lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        lblLanguage.Text = Master.eLang.GetString(610, "Language")
        lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        lblMPAADesc.Text = Master.eLang.GetString(229, "MPAA Rating Description:")
        lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        lblOutline.Text = Master.eLang.GetString(242, "Plot Outline:")
        lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        lblRating.Text = Master.eLang.GetString(245, "Rating:")
        lblReleaseDate.Text = Master.eLang.GetString(236, "Release Date:")
        lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        lblSortTilte.Text = String.Concat(Master.eLang.GetString(642, "Sort Title"), ":")
        lblStudio.Text = String.Concat(Master.eLang.GetString(395, "Studio"), ":")
        lblTagline.Text = Master.eLang.GetString(243, "Tagline:")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        lblTop250.Text = Master.eLang.GetString(240, "Top 250:")
        lblTopDetails.Text = Master.eLang.GetString(224, "Edit the details for the selected movie.")
        lblTopTitle.Text = Master.eLang.GetString(25, "Edit Movie")
        lblTrailerURL.Text = Master.eLang.GetString(227, "Trailer URL:")
        lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        lblYear.Text = Master.eLang.GetString(49, "Year:")
        tpBanner.Text = Master.eLang.GetString(838, "Banner")
        tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        tpDetails.Text = Master.eLang.GetString(1098, "DiscArt")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tpExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        tpExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        tpMetaData.Text = Master.eLang.GetString(866, "Metadata")
        tpPoster.Text = Master.eLang.GetString(148, "Poster")

        cbSourceLanguage.Items.Clear()
        cbSourceLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Description).ToArray)
    End Sub

    Private Sub tcEditMovie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcEdit.SelectedIndexChanged
        lvSubtitles.SelectedItems.Clear()
        ThemeStop()
        TrailerStop()
    End Sub

    Private Sub txtThumbCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtThumbCount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        AcceptButton = OK_Button
    End Sub

    Private Sub txtTrailer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrailer.TextChanged
        If StringUtils.isValidURL(txtTrailer.Text) Then
            btnPlayTrailer.Enabled = True
        Else
            btnPlayTrailer.Enabled = False
        End If
    End Sub

    Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.FrameExtrator_Movie AndAlso _params IsNot Nothing Then
            If _params(0).ToString = "FanartToSave" Then
                tmpDBElement.ImagesContainer.Fanart.ImageOriginal.LoadFromFile(Path.Combine(Master.TempPath, "frame.jpg"), True)
                If tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                    pbFanart.Image = tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    pbFanart.Tag = tmpDBElement.ImagesContainer.Fanart

                    lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), pbFanart.Image.Width, pbFanart.Image.Height)
                    lblFanartSize.Visible = True
                End If
            ElseIf _params(0).ToString = "ExtrafanartToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    Dim eImg As New MediaContainers.Image
                    eImg.ImageOriginal.LoadFromFile(fPath, True)
                    tmpDBElement.ImagesContainer.Extrafanarts.Add(eImg)
                    RefreshExtrafanarts()
                End If
            ElseIf _params(0).ToString = "ExtrathumbToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    Dim eImg As New MediaContainers.Image
                    eImg.ImageOriginal.LoadFromFile(fPath, True)
                    tmpDBElement.ImagesContainer.Extrathumbs.Add(eImg)
                    RefreshExtrathumbs()
                End If
            End If
        End If
    End Sub

    Private Sub txtOutline_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtOutline.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            txtOutline.SelectAll()
        End If
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            txtPlot.SelectAll()
        End If
    End Sub

    Private Sub lvSubtitles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSubtitles.KeyDown
        If e.KeyCode = Keys.Delete Then DeleteSubtitle()
    End Sub

    Private Sub lvSubtitles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvSubtitles.DoubleClick
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString <> "Header" Then
                EditSubtitle()
            End If
        End If
    End Sub

    Private Sub lvSubtitles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvSubtitles.SelectedIndexChanged
        If lvSubtitles.SelectedItems.Count > 0 Then
            If lvSubtitles.SelectedItems.Item(0).Tag.ToString = "Header" Then
                lvSubtitles.SelectedItems.Clear()
                btnRemoveSubtitle.Enabled = False
                txtSubtitlesPreview.Clear()
            Else
                btnRemoveSubtitle.Enabled = True
                txtSubtitlesPreview.Text = ReadSubtitle(lvSubtitles.SelectedItems.Item(0).SubItems(1).Text.ToString)
            End If
        Else
            btnRemoveSubtitle.Enabled = False
            txtSubtitlesPreview.Clear()
        End If
    End Sub

    Private Function ReadSubtitle(ByVal sPath As String) As String
        Dim sText As String = String.Empty

        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Dim objReader As New StreamReader(sPath)

            sText = objReader.ReadToEnd

            objReader.Close()

            Return sText
        End If

        Return String.Empty
    End Function

    Private Sub EditSubtitle()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            Dim tmpFileInfo As New MediaContainers.Fileinfo
            tmpFileInfo.StreamDetails.Subtitle.AddRange(tmpDBElement.Subtitles)
            Using dEditStream As New dlgFIStreamEditor
                Dim stream As Object = dEditStream.ShowDialog(i.Tag.ToString, tmpFileInfo, Convert.ToInt16(i.Text))
                If Not stream Is Nothing Then
                    If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                        tmpDBElement.Subtitles(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Subtitle)
                    End If
                    'NeedToRefresh = True
                    LoadSubtitles()
                End If
            End Using
        End If
    End Sub

    Private Sub DeleteSubtitle()
        If lvSubtitles.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
            If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                tmpDBElement.Subtitles(Convert.ToInt16(i.Text)).toRemove = True
            End If
            'NeedToRefresh = True
            LoadSubtitles()
        End If
    End Sub

    Private Sub LoadSubtitles()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvSubtitles.Groups.Clear()
        lvSubtitles.Items.Clear()

        If tmpDBElement.Subtitles.Count > 0 Then
            g = New ListViewGroup
            g.Header = Master.eLang.GetString(597, "Subtitle Stream")
            lvSubtitles.Groups.Add(g)
            c = 1
            ' Fake Group Header
            i = New ListViewItem
            'i.UseItemStyleForSubItems = False
            i.ForeColor = Color.DarkBlue
            i.Tag = "Header"
            i.Text = String.Empty
            i.SubItems.Add(Master.eLang.GetString(60, "File Path"))
            i.SubItems.Add(Master.eLang.GetString(610, "Language"))
            i.SubItems.Add(Master.eLang.GetString(1288, "Type"))
            i.SubItems.Add(Master.eLang.GetString(1287, "Forced"))

            g.Items.Add(i)
            lvSubtitles.Items.Add(i)
            Dim s As MediaContainers.Subtitle
            For c = 0 To tmpDBElement.Subtitles.Count - 1
                s = tmpDBElement.Subtitles(c)
                If Not s Is Nothing Then
                    i = New ListViewItem
                    i.Tag = Master.eLang.GetString(597, "Subtitle Stream")
                    i.Text = c.ToString
                    i.SubItems.Add(s.SubsPath)
                    i.SubItems.Add(s.LongLanguage)
                    i.SubItems.Add(s.SubsType)
                    i.SubItems.Add(If(s.SubsForced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                    If s.toRemove Then
                        i.ForeColor = Color.Red
                    End If

                    g.Items.Add(i)
                    lvSubtitles.Items.Add(i)
                End If
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class