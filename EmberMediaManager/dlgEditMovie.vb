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

Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Net

Public Class dlgEditMovie

#Region "Events"

#End Region 'Events

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private tmpDBElement As New Database.DBElement

    Private CachePath As String = String.Empty
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private lvwActorSorter As ListViewColumnSorter
    Private ActorThumbsHasChanged As Boolean = False
    Private pResults As New Containers.ImgResult
    Private PreviousFrameValue As Integer
    Private MovieTheme As New Themes With {.isEdit = True}
    Private tmpRating As String = String.Empty
    Private AnyThemePlayerEnabled As Boolean = False
    Private AnyTrailerPlayerEnabled As Boolean = False

    'Extrafanarts
    Private EFanartsWarning As Boolean = True
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbExtrafanartsImage() As PictureBox
    Private pnlExtrafanartsImage() As Panel

    'Extrathumbs
    Private EThumbsWarning As Boolean = True
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
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBMovie As Database.DBElement) As DialogResult
        Me.tmpDBElement = DBMovie
        Return MyBase.ShowDialog()
    End Function

    Private Sub AddExtrafanartImage(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image)
        Try
            ReDim Preserve Me.pnlExtrafanartsImage(iIndex)
            ReDim Preserve Me.pbExtrafanartsImage(iIndex)
            Me.pnlExtrafanartsImage(iIndex) = New Panel()
            Me.pbExtrafanartsImage(iIndex) = New PictureBox()
            Me.pbExtrafanartsImage(iIndex).Name = iIndex.ToString
            Me.pnlExtrafanartsImage(iIndex).Name = iIndex.ToString
            Me.pnlExtrafanartsImage(iIndex).Size = New Size(128, 72)
            Me.pbExtrafanartsImage(iIndex).Size = New Size(128, 72)
            Me.pnlExtrafanartsImage(iIndex).BackColor = Color.White
            Me.pnlExtrafanartsImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbExtrafanartsImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlExtrafanartsImage(iIndex).Tag = tImage
            Me.pbExtrafanartsImage(iIndex).Tag = tImage
            Me.pbExtrafanartsImage(iIndex).Image = CType(tImage.ImageOriginal.Image.Clone(), Image)
            Me.pnlExtrafanartsImage(iIndex).Left = iEFLeft
            Me.pbExtrafanartsImage(iIndex).Left = 0
            Me.pnlExtrafanartsImage(iIndex).Top = iEFTop
            Me.pbExtrafanartsImage(iIndex).Top = 0
            Me.pnlExtrafanarts.Controls.Add(Me.pnlExtrafanartsImage(iIndex))
            Me.pnlExtrafanartsImage(iIndex).Controls.Add(Me.pbExtrafanartsImage(iIndex))
            Me.pnlExtrafanartsImage(iIndex).BringToFront()
            AddHandler pbExtrafanartsImage(iIndex).Click, AddressOf pbExtrafanartsImage_Click
            AddHandler pnlExtrafanartsImage(iIndex).Click, AddressOf pnlExtrafanartsImage_Click
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iEFTop += 74

    End Sub

    Private Sub AddExtrathumbImage(ByVal sDescription As String, ByVal iIndex As Integer, tImage As MediaContainers.Image)
        Try
            ReDim Preserve Me.pnlExtrathumbsImage(iIndex)
            ReDim Preserve Me.pbExtrathumbsImage(iIndex)
            Me.pnlExtrathumbsImage(iIndex) = New Panel()
            Me.pbExtrathumbsImage(iIndex) = New PictureBox()
            Me.pbExtrathumbsImage(iIndex).Name = iIndex.ToString
            Me.pnlExtrathumbsImage(iIndex).Name = iIndex.ToString
            Me.pnlExtrathumbsImage(iIndex).Size = New Size(128, 72)
            Me.pbExtrathumbsImage(iIndex).Size = New Size(128, 72)
            Me.pnlExtrathumbsImage(iIndex).BackColor = Color.White
            Me.pnlExtrathumbsImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbExtrathumbsImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlExtrathumbsImage(iIndex).Tag = tImage
            Me.pbExtrathumbsImage(iIndex).Tag = tImage
            Me.pbExtrathumbsImage(iIndex).Image = CType(tImage.ImageOriginal.Image.Clone(), Image)
            Me.pnlExtrathumbsImage(iIndex).Left = iETLeft
            Me.pbExtrathumbsImage(iIndex).Left = 0
            Me.pnlExtrathumbsImage(iIndex).Top = iETTop
            Me.pbExtrathumbsImage(iIndex).Top = 0
            Me.pnlExtrathumbs.Controls.Add(Me.pnlExtrathumbsImage(iIndex))
            Me.pnlExtrathumbsImage(iIndex).Controls.Add(Me.pbExtrathumbsImage(iIndex))
            Me.pnlExtrathumbsImage(iIndex).BringToFront()
            AddHandler pbExtrathumbsImage(iIndex).Click, AddressOf pbExtrathumbsImage_Click
            AddHandler pnlExtrathumbsImage(iIndex).Click, AddressOf pnlExtrathumbsImage_Click
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iETTop += 74

    End Sub

    Private Sub pbExtrafanartsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbExtrathumbsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectExtrathumb(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlExtrafanartsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectExtrafanart(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlExtrathumbsImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectExtrathumb(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub DoSelectExtrafanart(ByVal iIndex As Integer, tImg As MediaContainers.Image)
        Me.pbExtrafanarts.Image = tImg.ImageOriginal.Image
        Me.pbExtrafanarts.Tag = tImg
        Me.btnExtrafanartsSetAsFanart.Enabled = True
        Me.lblExtrafanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbExtrafanarts.Image.Width, Me.pbExtrafanarts.Image.Height)
        Me.lblExtrafanartsSize.Visible = True
    End Sub

    Private Sub DoSelectExtrathumb(ByVal iIndex As Integer, tImg As MediaContainers.Image)
        Me.currExtrathumbImage = tImg

        Me.pbExtrathumbs.Image = tImg.ImageOriginal.Image
        Me.pbExtrathumbs.Tag = tImg
        Me.btnExtrathumbsSetAsFanart.Enabled = True
        Me.lblExtrathumbsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbExtrathumbs.Image.Width, Me.pbExtrathumbs.Image.Height)
        Me.lblExtrathumbsSize.Visible = True

        If tImg.Index > 0 Then
            Me.btnEThumbsUp.Enabled = True
        Else
            Me.btnEThumbsUp.Enabled = False
        End If
        If tImg.Index < tmpDBElement.ImagesContainer.Extrathumbs.Count - 1 Then
            Me.btnEThumbsDown.Enabled = True
        Else
            Me.btnEThumbsDown.Enabled = False
        End If
    End Sub

    Private Sub btnActorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorDown.Click
        If Me.lvActors.SelectedItems.Count > 0 AndAlso Me.lvActors.SelectedItems(0) IsNot Nothing AndAlso Me.lvActors.SelectedIndices(0) < (Me.lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
            Me.lvActors.Items.Insert(iIndex + 2, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
            Me.lvActors.Items.RemoveAt(iIndex)
            Me.lvActors.Items(iIndex + 1).Selected = True
            Me.lvActors.Select()
        End If
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorUp.Click
        Try
            If Me.lvActors.SelectedItems.Count > 0 AndAlso Me.lvActors.SelectedItems(0) IsNot Nothing AndAlso Me.lvActors.SelectedIndices(0) > 0 Then
                Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
                Me.lvActors.Items.Insert(iIndex - 1, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
                Me.lvActors.Items.RemoveAt(iIndex + 1)
                Me.lvActors.Items(iIndex - 1).Selected = True
                Me.lvActors.Select()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnAddActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddActor.Click
        Try
            Dim eActor As New MediaContainers.Person
            Using dAddEditActor As New dlgAddEditActor
                eActor = dAddEditActor.ShowDialog(True)
            End Using
            If eActor IsNot Nothing Then
                Dim lvItem As ListViewItem = Me.lvActors.Items.Add(eActor.ID.ToString)
                lvItem.SubItems.Add(eActor.Name)
                lvItem.SubItems.Add(eActor.Role)
                lvItem.SubItems.Add(eActor.ThumbURL)
                ActorThumbsHasChanged = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnChangeMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMovie.Click
        Me.ThemeStop()
        Me.TrailerStop()

        Me.CleanUp()

        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub btnDLTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim aUrlList As New List(Of Themes)
        'Dim tURL As String = String.Empty
        'If Not ModulesManager.Instance.ScrapeTheme_Movie(Me.tmpDBMovie, aUrlList) Then
        '    Using dThemeSelect As New dlgThemeSelect()
        '        MovieTheme = dThemeSelect.ShowDialog(Me.tmpDBMovie, aUrlList)
        '    End Using
        'End If

        'If Not String.IsNullOrEmpty(MovieTheme.URL) Then
        '    'Me.btnPlayTheme.Enabled = True
        'End If
    End Sub

    Private Sub btnDLTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLTrailer.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)
        Dim tURL As String = String.Empty

        Try
            dlgTrlS = New dlgTrailerSelect()
            If dlgTrlS.ShowDialog(Me.tmpDBElement, tList, True, True, True) = Windows.Forms.DialogResult.OK Then
                tURL = dlgTrlS.Result.URLWebsite
            End If

            If Not String.IsNullOrEmpty(tURL) Then
                Me.btnPlayTrailer.Enabled = True
                Me.txtTrailer.Text = tURL
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnEThumbsDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsDown.Click
        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso Me.currExtrathumbImage.Index < tmpDBElement.ImagesContainer.Extrathumbs.Count - 1 Then
            Dim iIndex As Integer = Me.currExtrathumbImage.Index
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
            RefreshExtrathumbs()
            Me.DoSelectExtrathumb(iIndex + 1, CType(pnlExtrathumbsImage(iIndex + 1).Tag, MediaContainers.Image))
        End If
    End Sub

    Private Sub btnEThumbsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsUp.Click
        If tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 AndAlso Me.currExtrathumbImage.Index > 0 Then
            Dim iIndex As Integer = Me.currExtrathumbImage.Index
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
            tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = tmpDBElement.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
            RefreshExtrathumbs()
            Me.DoSelectExtrathumb(iIndex - 1, CType(pnlExtrathumbsImage(iIndex - 1).Tag, MediaContainers.Image))
        End If
    End Sub

    Private Sub btnEditActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditActor.Click
        EditActor()
    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        Try
            If dlgManualEdit.ShowDialog(Me.tmpDBElement.NfoPath) = Windows.Forms.DialogResult.OK Then
                Me.tmpDBElement.Movie = NFO.LoadMovieFromNFO(Me.tmpDBElement.NfoPath, Me.tmpDBElement.IsSingle)
                Me.FillInfo(False)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTrailer.Click
        'TODO 2013/12/18 Dekker500 - This should be re-factored to use Functions.Launch. Why is the URL different for non-windows??? Need to test first before editing
        Try

            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(Me.txtTrailer.Text) Then
                tPath = String.Concat("""", Me.txtTrailer.Text, """")
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

            If Not String.IsNullOrEmpty(Me.tmpDBElement.ThemePath) Then
                tPath = String.Concat("""", Me.tmpDBElement.ThemePath, """")
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
        Me.pbBanner.Image = Nothing
        Me.pbBanner.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearArt.Click
        Me.pbClearArt.Image = Nothing
        Me.pbClearArt.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.ClearArt = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveClearLogo.Click
        Me.pbClearLogo.Image = Nothing
        Me.pbClearLogo.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.ClearLogo = New MediaContainers.Image
    End Sub

    Private Sub btnRemoveDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveDiscArt.Click
        Me.pbDiscArt.Image = Nothing
        Me.pbDiscArt.Tag = Nothing
        Me.tmpDBElement.ImagesContainer.DiscArt = New MediaContainers.Image
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

    Private Sub btnRemoveSubtitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSubtitle.Click
        Me.DeleteSubtitle()
    End Sub

    Private Sub btnRemoveTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTheme.Click
        Me.ThemeStop()
        'Me.axVLCTheme.playlist.items.clear()
        Me.MovieTheme.Dispose()
        Me.MovieTheme.toRemove = True
    End Sub

    Private Sub btnRemoveTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTrailer.Click
        Me.TrailerStop()
        Me.TrailerPlaylistClear()
        Me.tmpDBElement.Trailer = New MediaContainers.Trailer
    End Sub

    Private Sub btnExtrafanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsRemove.Click
        RemoveExtrafanart()
    End Sub

    Private Sub btnExtrathumbsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsRemove.Click
        RemoveExtrathumb()
    End Sub

    Private Sub RemoveExtrafanart()
        If pbExtrafanarts.Tag IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Extrafanarts.Remove(DirectCast(Me.pbExtrafanarts.Tag, MediaContainers.Image))
            Me.RefreshExtrafanarts()
            Me.lblExtrafanartsSize.Text = ""
            Me.lblExtrafanartsSize.Visible = False
        End If
    End Sub

    Private Sub RemoveExtrathumb()
        If pbExtrathumbs.Tag IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Extrathumbs.Remove(DirectCast(Me.pbExtrathumbs.Tag, MediaContainers.Image))
            Me.RefreshExtrathumbs()
            Me.lblExtrafanartsSize.Text = ""
            Me.lblExtrafanartsSize.Visible = False
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        Me.ThemeStop()
        Me.TrailerStop()

        Me.CleanUp()

        Me.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Banner = tImage
                        Me.pbBanner.Image = Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                        Me.pbBanner.Tag = Me.tmpDBElement.ImagesContainer.Banner

                        Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                        Me.lblBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainBanners.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
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
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbBanner.Image = Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
                Me.pbBanner.Tag = Me.tmpDBElement.ImagesContainer.Banner

                Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
                Me.lblBannerSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.ClearArt = tImage
                        Me.pbClearArt.Image = Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                        Me.pbClearArt.Tag = Me.tmpDBElement.ImagesContainer.ClearArt

                        Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                        Me.lblClearArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainClearArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.ClearArt
                    Me.tmpDBElement.ImagesContainer.ClearArt = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbClearArt.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                        Me.lblClearArtSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearArtLocal.Click
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbClearArt.Image = Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                Me.pbClearArt.Tag = Me.tmpDBElement.ImagesContainer.ClearArt

                Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
                Me.lblClearArtSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.ClearLogo = tImage
                        Me.pbClearLogo.Image = Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                        Me.pbClearLogo.Tag = Me.tmpDBElement.ImagesContainer.ClearLogo

                        Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                        Me.lblClearLogoSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainClearLogos.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.ClearLogo
                    Me.tmpDBElement.ImagesContainer.ClearLogo = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbClearLogo.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                        Me.lblClearLogoSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetClearLogoLocal.Click
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbClearLogo.Image = Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                Me.pbClearLogo.Tag = Me.tmpDBElement.ImagesContainer.ClearLogo

                Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
                Me.lblClearLogoSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetDiscArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.DiscArt = tImage
                        Me.pbDiscArt.Image = Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
                        Me.pbDiscArt.Tag = Me.tmpDBElement.ImagesContainer.DiscArt

                        Me.lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbDiscArt.Image.Width, Me.pbDiscArt.Image.Height)
                        Me.lblDiscArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetDiscArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainDiscArts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
                    Dim pResults As MediaContainers.Image = dlgImgS.Result.ImagesContainer.DiscArt
                    Me.tmpDBElement.ImagesContainer.DiscArt = pResults
                    If pResults.ImageOriginal.Image IsNot Nothing Then
                        Me.pbDiscArt.Image = CType(pResults.ImageOriginal.Image.Clone(), Image)
                        Me.lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbDiscArt.Image.Width, Me.pbDiscArt.Image.Height)
                        Me.lblDiscArtSize.Visible = True
                    End If
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetDiscArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDiscArtLocal.Click
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbDiscArt.Image = Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
                Me.pbDiscArt.Tag = Me.tmpDBElement.ImagesContainer.DiscArt

                Me.lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbDiscArt.Image.Width, Me.pbDiscArt.Image.Height)
                Me.lblDiscArtSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnExtrafanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsSetAsFanart.Click
        If Me.pbExtrafanarts.Tag IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Fanart = DirectCast(Me.pbExtrafanarts.Tag, MediaContainers.Image)
            Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnExtrathumbsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsSetAsFanart.Click
        If Me.pbExtrathumbs.Tag IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Fanart = DirectCast(Me.pbExtrathumbs.Tag, MediaContainers.Image)
            Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetExtrafanartsScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetExtrafanartsScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrafanarts, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
                    Me.tmpDBElement.ImagesContainer.Extrafanarts = dlgImgS.Result.ImagesContainer.Extrafanarts
                    RefreshExtrafanarts()
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetExtrathumbsScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetExtrathumbsScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainExtrathumbs, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
                    Me.tmpDBElement.ImagesContainer.Extrathumbs = dlgImgS.Result.ImagesContainer.Extrathumbs
                    RefreshExtrathumbs()
                    Cursor = Cursors.Default
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Fanart = tImage
                        Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                        Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

                        Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                        Me.lblFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainFanarts.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
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
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

                Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                Me.lblFanartSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Landscape = tImage
                        Me.pbLandscape.Image = Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                        Me.pbLandscape.Tag = Me.tmpDBElement.ImagesContainer.Landscape

                        Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                        Me.lblLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainLandscapes.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
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
                MessageBox.Show(Master.eLang.GetString(972, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLandscapeLocal.Click
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbLandscape.Image = Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
                Me.pbLandscape.Tag = Me.tmpDBElement.ImagesContainer.Landscape

                Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
                Me.lblLandscapeSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As MediaContainers.Image
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If tImage.ImageOriginal.Image IsNot Nothing Then
                        Me.tmpDBElement.ImagesContainer.Poster = tImage
                        Me.pbPoster.Image = Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                        Me.pbPoster.Tag = Me.tmpDBElement.ImagesContainer.Poster

                        Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                        Me.lblPosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifier As New Structures.ScrapeModifier

        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_Movie(Me.tmpDBElement, aContainer, ScrapeModifier, True) Then
            If aContainer.MainPosters.Count > 0 Then
                Dim dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBElement, aContainer, ScrapeModifier, Enums.ContentType.Movie) = Windows.Forms.DialogResult.OK Then
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
        Try
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.FromFile(ofdLocalFiles.FileName)
                Me.pbPoster.Image = Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
                Me.pbPoster.Tag = Me.tmpDBElement.ImagesContainer.Poster

                Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
                Me.lblPosterSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    'Private Sub btnSetMovieThemeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieThemeDL.Click
    '    Dim tResults As New MediaContainers.Theme
    '    Dim dlgTheS As dlgThemeSelect
    '    Dim tList As New List(Of Themes)

    '    Try
    '        Me.ThemeStop()
    '        dlgTheS = New dlgThemeSelect()
    '        If dlgTheS.ShowDialog(Me.tmpDBMovie, tList) = Windows.Forms.DialogResult.OK Then
    '            tResults = dlgTheS.Results
    '            MovieTheme = dlgTheS.WebTheme
    '            ThemeAddToPlayer(MovieTheme)
    '        End If
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    Private Sub btnSetThemeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeScrape.Click
        Dim dlgThmS As dlgThemeSelect
        Dim aUrlList As New List(Of Themes)

        Try
            Me.ThemeStop()
            If Not ModulesManager.Instance.ScrapeTheme_Movie(Me.tmpDBElement, aUrlList) Then
                If aUrlList.Count > 0 Then
                    dlgThmS = New dlgThemeSelect()
                    If dlgThmS.ShowDialog(Me.tmpDBElement, aUrlList) = Windows.Forms.DialogResult.OK Then
                        MovieTheme = dlgThmS.Results.WebTheme
                        MovieTheme.isEdit = True
                        ThemeAddToPlayer(MovieTheme)
                    End If
                Else
                    MessageBox.Show(Master.eLang.GetString(1163, "No Themes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetThemeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetThemeLocal.Click
        Try
            Me.ThemeStop()
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(1285, "Themes") + "|*.mp3;*.wav"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                MovieTheme.FromFile(ofdLocalFiles.FileName)
                MovieTheme.isEdit = True
                ThemeAddToPlayer(MovieTheme)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetTrailerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerDL.Click
        Dim tResults As New MediaContainers.Trailer
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)

        Try
            Me.TrailerStop()
            dlgTrlS = New dlgTrailerSelect()
            If dlgTrlS.ShowDialog(Me.tmpDBElement, tList, False, True, True) = Windows.Forms.DialogResult.OK Then
                tResults = dlgTrlS.Result
                Me.tmpDBElement.Trailer = tResults
                TrailerPlaylistAdd(Me.tmpDBElement.Trailer)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetTrailerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerScrape.Click
        Dim dlgTrlS As dlgTrailerSelect
        Dim tList As New List(Of MediaContainers.Trailer)

        Try
            Me.TrailerStop()
            dlgTrlS = New dlgTrailerSelect()
            If dlgTrlS.ShowDialog(Me.tmpDBElement, tList, False, True, True) = Windows.Forms.DialogResult.OK Then
                Me.tmpDBElement.Trailer = dlgTrlS.Result
                TrailerPlaylistAdd(Me.tmpDBElement.Trailer)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetTrailerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrailerLocal.Click
        Try
            Me.TrailerStop()
            With ofdLocalFiles
                .InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
                .Filter = Master.eLang.GetString(1195, "Trailers") + "|*.mp4;*.avi"
                .FilterIndex = 0
            End With

            If ofdLocalFiles.ShowDialog() = DialogResult.OK Then
                Me.tmpDBElement.Trailer.TrailerOriginal.FromFile(ofdLocalFiles.FileName)
                Me.tmpDBElement.Trailer.TrailerOriginal.isEdit = True
                TrailerPlaylistAdd(Me.tmpDBElement.Trailer)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStudio.Click
        Using dStudio As New dlgStudioSelect
            Dim tStudio As String = dStudio.ShowDialog(Me.tmpDBElement)
            If Not String.IsNullOrEmpty(tStudio) Then
                Me.txtStudio.Text = tStudio
            End If
        End Using
    End Sub

    Private Sub btnExtrafanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrafanartsRefresh.Click
        Me.RefreshExtrafanarts()
    End Sub

    Private Sub btnExtrathumbsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtrathumbsRefresh.Click
        Me.RefreshExtrathumbs()
    End Sub

    Private Sub ThemeStart()
        'If Me.axVLCTheme.playlist.isPlaying Then
        '    Me.axVLCTheme.playlist.togglePause()
        '    Me.btnThemePlay.Text = "Play"
        'Else
        '    Me.axVLCTheme.playlist.play()
        '    Me.btnThemePlay.Text = "Pause"
        'End If
    End Sub

    Private Sub ThemeStop()
        'Me.axVLCTheme.playlist.stop()
        'Me.btnThemePlay.Text = "Play"
    End Sub

    Private Sub ThemeAddToPlayer(ByVal Theme As Themes)
        'Dim Link As String = String.Empty
        'Me.axVLCTheme.playlist.stop()
        'Me.axVLCTheme.playlist.items.clear()
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.ThemeStop()
        Me.TrailerStop()

        Me.CleanUp()

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CleanUp()
        Try
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
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DelayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDelay.Tick
        tmrDelay.Stop()
        'GrabTheFrame()
    End Sub

    Private Sub DeleteActors()
        Try
            If Me.lvActors.Items.Count > 0 Then
                While Me.lvActors.SelectedItems.Count > 0
                    Me.lvActors.Items.Remove(Me.lvActors.SelectedItems(0))
                End While
                ActorThumbsHasChanged = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgEditMovie_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.MovieTheme.Dispose()
        Me.MovieTheme = Nothing
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.pbBanner.AllowDrop = True
            Me.pbClearArt.AllowDrop = True
            Me.pbClearLogo.AllowDrop = True
            Me.pbDiscArt.AllowDrop = True
            Me.pbFanart.AllowDrop = True
            Me.pbLandscape.AllowDrop = True
            Me.pbPoster.AllowDrop = True

            Me.SetUp()
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEdit_Movie, Nothing, Me.tmpDBElement)
            Me.lvwActorSorter = New ListViewColumnSorter()
            Me.lvActors.ListViewItemSorter = Me.lvwActorSorter
            'Me.lvwEThumbsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
            'Me.lvEThumbs.ListViewItemSorter = Me.lvwEThumbsSorter
            'Me.lvwEFanartsSorter = New ListViewColumnSorter() With {.SortByText = True, .Order = SortOrder.Ascending, .NumericSort = True}
            'Me.lvEFanarts.ListViewItemSorter = Me.lvwEFanartsSorter

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            Dim dFileInfoEdit As New dlgFileInfo(tmpDBElement, False)
            dFileInfoEdit.TopLevel = False
            dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
            dFileInfoEdit.BackColor = Color.White
            dFileInfoEdit.btnClose.Visible = False
            Me.pnlFileInfo.Controls.Add(dFileInfoEdit)
            Dim oldwidth As Integer = dFileInfoEdit.Width
            dFileInfoEdit.Width = pnlFileInfo.Width
            dFileInfoEdit.Height = pnlFileInfo.Height
            dFileInfoEdit.Show()

            Me.LoadGenres()
            Me.LoadRatings()

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

            Me.FillInfo()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub EditActor()
        Try
            If Me.lvActors.SelectedItems.Count > 0 Then
                Dim lvwItem As ListViewItem = Me.lvActors.SelectedItems(0)
                Dim eActor As New MediaContainers.Person With {.ID = CInt(lvwItem.Text), .Name = lvwItem.SubItems(1).Text, .Role = lvwItem.SubItems(2).Text, .ThumbURL = lvwItem.SubItems(3).Text}
                Using dAddEditActor As New dlgAddEditActor
                    eActor = dAddEditActor.ShowDialog(False, eActor)
                End Using
                If eActor IsNot Nothing Then
                    lvwItem.Text = eActor.ID.ToString
                    lvwItem.SubItems(1).Text = eActor.Name
                    lvwItem.SubItems(2).Text = eActor.Role
                    lvwItem.SubItems(3).Text = eActor.ThumbURL
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
                    ActorThumbsHasChanged = True
                End If
                eActor = Nothing
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub FillInfo(Optional ByVal DoAll As Boolean = True)
        Try
            With Me
                If String.IsNullOrEmpty(Me.tmpDBElement.NfoPath) Then
                    .btnManual.Enabled = False
                End If

                Me.chkMark.Checked = Me.tmpDBElement.IsMark
                If Me.tmpDBElement.Movie.PlayCountSpecified Then
                    Me.chkWatched.Checked = True
                Else
                    Me.chkWatched.Checked = False
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Title) Then
                    .txtTitle.Text = Me.tmpDBElement.Movie.Title
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.OriginalTitle) Then
                    .txtOriginalTitle.Text = Me.tmpDBElement.Movie.OriginalTitle
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.SortTitle) Then
                    .txtSortTitle.Text = Me.tmpDBElement.Movie.SortTitle
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Tagline) Then
                    .txtTagline.Text = Me.tmpDBElement.Movie.Tagline
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Year) Then
                    .mtxtYear.Text = Me.tmpDBElement.Movie.Year
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Votes) Then
                    .txtVotes.Text = Me.tmpDBElement.Movie.Votes
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Outline) Then
                    .txtOutline.Text = Me.tmpDBElement.Movie.Outline
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Plot) Then
                    .txtPlot.Text = Me.tmpDBElement.Movie.Plot
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Top250) Then
                    .txtTop250.Text = Me.tmpDBElement.Movie.Top250
                End If

                If Me.tmpDBElement.Movie.Countries.Count > 0 Then
                    .txtCountry.Text = Me.tmpDBElement.Movie.Country
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Runtime) Then
                    .txtRuntime.Text = Me.tmpDBElement.Movie.Runtime
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.ReleaseDate) Then
                    .txtReleaseDate.Text = Me.tmpDBElement.Movie.ReleaseDate
                End If

                If Me.tmpDBElement.Movie.Directors.Count > 0 Then
                    .txtDirector.Text = Me.tmpDBElement.Movie.Director
                End If

                If Me.tmpDBElement.Movie.Credits.Count > 0 Then
                    .txtCredits.Text = Me.tmpDBElement.Movie.OldCredits
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.VideoSource) Then
                    .txtVideoSource.Text = Me.tmpDBElement.VideoSource
                ElseIf Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.VideoSource) Then
                    .txtVideoSource.Text = Me.tmpDBElement.Movie.VideoSource
                End If

                If Me.tmpDBElement.Movie.Certifications.Count > 0 Then
                    .txtCerts.Text = Me.tmpDBElement.Movie.Certification
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.LastPlayed) Then
                    Dim timecode As Double = 0
                    Double.TryParse(Me.tmpDBElement.Movie.LastPlayed, timecode)
                    If timecode > 0 Then
                        .txtLastPlayed.Text = Functions.ConvertFromUnixTimestamp(timecode).ToString("yyyy-MM-dd HH:mm:ss")
                    Else
                        .txtLastPlayed.Text = Me.tmpDBElement.Movie.LastPlayed
                    End If
                End If

                Me.SelectMPAA()

                If String.IsNullOrEmpty(Me.tmpDBElement.ThemePath) Then
                    '.btnPlayTheme.Enabled = False
                End If

                '.btnDLTheme.Enabled = Master.eSettings.MovieThemeEnable AndAlso Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Theme)

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.Trailer) Then
                    .txtTrailer.Text = Me.tmpDBElement.Movie.Trailer
                    .btnPlayTrailer.Enabled = True
                Else
                    .btnPlayTrailer.Enabled = False
                End If

                .btnDLTrailer.Enabled = Master.DefaultOptions_Movie.bTrailer

                If Me.tmpDBElement.Movie.Studios.Count > 0 Then
                    .txtStudio.Text = Me.tmpDBElement.Movie.Studio
                End If

                For i As Integer = 0 To .clbGenre.Items.Count - 1
                    .clbGenre.SetItemChecked(i, False)
                Next
                If Me.tmpDBElement.Movie.Genres.Count > 0 Then
                    For g As Integer = 0 To Me.tmpDBElement.Movie.Genres.Count - 1
                        If .clbGenre.FindString(Me.tmpDBElement.Movie.Genres(g).Trim) > 0 Then
                            .clbGenre.SetItemChecked(.clbGenre.FindString(Me.tmpDBElement.Movie.Genres(g).Trim), True)
                        End If
                    Next

                    If .clbGenre.CheckedItems.Count = 0 Then
                        .clbGenre.SetItemChecked(0, True)
                    End If
                Else
                    .clbGenre.SetItemChecked(0, True)
                End If

                Dim lvItem As ListViewItem
                .lvActors.Items.Clear()
                For Each imdbAct As MediaContainers.Person In Me.tmpDBElement.Movie.Actors
                    lvItem = .lvActors.Items.Add(imdbAct.ID.ToString)
                    lvItem.SubItems.Add(imdbAct.Name)
                    lvItem.SubItems.Add(imdbAct.Role)
                    lvItem.SubItems.Add(imdbAct.ThumbURL)
                Next

                If Not String.IsNullOrEmpty(Me.tmpDBElement.Filename) AndAlso String.IsNullOrEmpty(Me.tmpDBElement.Movie.VideoSource) Then
                    Dim vSource As String = APIXML.GetVideoSource(Me.tmpDBElement.Filename, False)
                    If Not String.IsNullOrEmpty(vSource) Then
                        Me.tmpDBElement.VideoSource = vSource
                        Me.tmpDBElement.Movie.VideoSource = Me.tmpDBElement.VideoSource
                    ElseIf String.IsNullOrEmpty(Me.tmpDBElement.VideoSource) AndAlso clsAdvancedSettings.GetBooleanSetting("MediaSourcesByExtension", False, "*EmberAPP") Then
                        Me.tmpDBElement.VideoSource = clsAdvancedSettings.GetSetting(String.Concat("MediaSourcesByExtension:", Path.GetExtension(Me.tmpDBElement.Filename)), String.Empty, "*EmberAPP")
                        Me.tmpDBElement.Movie.VideoSource = Me.tmpDBElement.VideoSource
                    ElseIf Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.VideoSource) Then
                        Me.tmpDBElement.VideoSource = Me.tmpDBElement.Movie.VideoSource
                    End If
                End If

                Dim tRating As Single = NumUtils.ConvertToSingle(Me.tmpDBElement.Movie.Rating)
                .tmpRating = tRating.ToString
                .pbStar1.Tag = tRating
                .pbStar2.Tag = tRating
                .pbStar3.Tag = tRating
                .pbStar4.Tag = tRating
                .pbStar5.Tag = tRating
                .pbStar6.Tag = tRating
                .pbStar7.Tag = tRating
                .pbStar8.Tag = tRating
                .pbStar9.Tag = tRating
                .pbStar10.Tag = tRating
                If tRating > 0 Then .BuildStars(tRating)

                If DoAll Then
                    Dim pExt As String = Path.GetExtension(Me.tmpDBElement.Filename).ToLower
                    If pExt = ".rar" OrElse pExt = ".iso" OrElse pExt = ".img" OrElse _
                    pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".dat" OrElse _
                    pExt = ".disc" Then
                        tcEdit.TabPages.Remove(tpFrameExtraction)
                    Else
                        If Not pExt = ".disc" Then
                            tcEdit.TabPages.Remove(tpMediaStub)
                        End If
                    End If

                    'Images and TabPages

                    'Banner
                    If Master.eSettings.MovieBannerAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner) Then
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

                    'ClearArt
                    If Master.eSettings.MovieClearArtAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt) Then
                            .btnSetClearArtScrape.Enabled = False
                        End If
                        If Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image IsNot Nothing Then
                            .pbClearArt.Image = Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
                            .pbClearArt.Tag = Me.tmpDBElement.ImagesContainer.ClearArt

                            .lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbClearArt.Image.Width, .pbClearArt.Image.Height)
                            .lblClearArtSize.Visible = True
                        End If
                    Else
                        tcEdit.TabPages.Remove(tpClearArt)
                    End If

                    'ClearLogo
                    If Master.eSettings.MovieClearLogoAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo) Then
                            .btnSetClearLogoScrape.Enabled = False
                        End If
                        If Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image IsNot Nothing Then
                            .pbClearLogo.Image = Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
                            .pbClearLogo.Tag = Me.tmpDBElement.ImagesContainer.ClearLogo

                            .lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbClearLogo.Image.Width, .pbClearLogo.Image.Height)
                            .lblClearLogoSize.Visible = True
                        End If
                    Else
                        tcEdit.TabPages.Remove(tpClearLogo)
                    End If

                    'DiscArt
                    If Master.eSettings.MovieDiscArtAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt) Then
                            .btnSetDiscArtScrape.Enabled = False
                        End If
                        If Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image IsNot Nothing Then
                            .pbDiscArt.Image = Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
                            .pbDiscArt.Tag = Me.tmpDBElement.ImagesContainer.DiscArt

                            .lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbDiscArt.Image.Width, .pbDiscArt.Image.Height)
                            .lblDiscArtSize.Visible = True
                        End If
                    Else
                        tcEdit.TabPages.Remove(tpDiscArt)
                    End If

                    'Extrafanarts
                    If Master.eSettings.MovieExtrafanartsAnyEnabled Then
                        'If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) Then
                        '.btnSetFanartScrape.Enabled = False
                        'End If
                        If Me.tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
                            Dim iIndex As Integer = 0
                            For Each img As MediaContainers.Image In Me.tmpDBElement.ImagesContainer.Extrafanarts
                                AddExtrafanartImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
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
                        If Me.tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 Then
                            Dim iIndex As Integer = 0
                            For Each img As MediaContainers.Image In Me.tmpDBElement.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                                AddExtrathumbImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                                iIndex += 1
                            Next
                        End If
                    Else
                        tcEdit.TabPages.Remove(tpExtrathumbs)
                    End If

                    'Fanart
                    If Master.eSettings.MovieFanartAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart) Then
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

                    'Landscape
                    If Master.eSettings.MovieLandscapeAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape) Then
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

                    'Poster
                    If Master.eSettings.MoviePosterAnyEnabled Then
                        If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster) Then
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

                    'Theme
                    If Not Master.eSettings.MovieThemeAnyEnabled Then
                        tcEdit.TabPages.Remove(tpTheme)
                    End If

                    If Not String.IsNullOrEmpty(Me.tmpDBElement.ThemePath) AndAlso Me.tmpDBElement.ThemePath.Substring(0, 1) = ":" Then
                        MovieTheme.FromWeb(Me.tmpDBElement.ThemePath.Substring(1, Me.tmpDBElement.ThemePath.Length - 1))
                        ThemeAddToPlayer(MovieTheme)
                    ElseIf Not String.IsNullOrEmpty(Me.tmpDBElement.ThemePath) Then
                        MovieTheme.FromFile(Me.tmpDBElement.ThemePath)
                        ThemeAddToPlayer(MovieTheme)
                    End If

                    'Trailer
                    If Master.eSettings.MovieTrailerAnyEnabled Then
                        If Not String.IsNullOrEmpty(Me.tmpDBElement.Trailer.LocalFilePath) OrElse Not String.IsNullOrEmpty(Me.tmpDBElement.Trailer.URLVideoStream) Then
                            TrailerPlaylistAdd(Me.tmpDBElement.Trailer)
                        End If
                    Else
                        tcEdit.TabPages.Remove(tpTrailer)
                    End If

                    'DiscStub
                    If Path.GetExtension(Me.tmpDBElement.Filename).ToLower = ".disc" Then
                        Dim DiscStub As New MediaStub.DiscStub
                        DiscStub = MediaStub.LoadDiscStub(Me.tmpDBElement.Filename)
                        .txtMediaStubTitle.Text = DiscStub.Title
                        .txtMediaStubMessage.Text = DiscStub.Message
                    End If

                    Me.LoadSubtitles()
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub clbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbGenre.Items.Count - 1
                Me.clbGenre.SetItemChecked(i, False)
            Next
        Else
            Me.clbGenre.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub lbMPAA_DoubleClick(sender As Object, e As EventArgs) Handles lbMPAA.DoubleClick
        If Me.lbMPAA.SelectedItems.Count = 1 Then
            If Me.lbMPAA.SelectedIndex = 0 Then
                Me.txtMPAA.Text = String.Empty
            Else
                Me.txtMPAA.Text = Me.lbMPAA.SelectedItem.ToString
            End If
        End If
    End Sub

    Private Sub LoadGenres()
        Me.clbGenre.Items.Add(Master.eLang.None)

        Me.clbGenre.Items.AddRange(APIXML.GetGenreList)
    End Sub

    Private Sub LoadRatings()
        Me.lbMPAA.Items.Add(Master.eLang.None)
        If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperMPAANotRated) Then Me.lbMPAA.Items.Add(Master.eSettings.MovieScraperMPAANotRated)
        Me.lbMPAA.Items.AddRange(APIXML.GetRatingList_Movie)
    End Sub

    Private Sub lvActors_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActors.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = Me.lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (Me.lvwActorSorter.Order = SortOrder.Ascending) Then
                    Me.lvwActorSorter.Order = SortOrder.Descending
                Else
                    Me.lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                Me.lvwActorSorter.SortColumn = e.Column
                Me.lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            Me.lvActors.Sort()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lvActors_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvActors.DoubleClick
        EditActor()
    End Sub

    Private Sub lvActors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvActors.KeyDown
        If e.KeyCode = Keys.Delete Then Me.DeleteActors()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.ThemeStop()
        Me.TrailerStop()

        Me.SetInfo()
        Me.CleanUp()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.ImageOriginal.Image IsNot Nothing Then
            Me.tmpDBElement.ImagesContainer.Banner = tImage
            Me.pbBanner.Image = Me.tmpDBElement.ImagesContainer.Banner.ImageOriginal.Image
            Me.pbBanner.Tag = Me.tmpDBElement.ImagesContainer.Banner
            Me.lblBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbBanner.Image.Width, Me.pbBanner.Image.Height)
            Me.lblBannerSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.ClearArt = tImage
            Me.pbClearArt.Image = Me.tmpDBElement.ImagesContainer.ClearArt.ImageOriginal.Image
            Me.pbClearArt.Tag = Me.tmpDBElement.ImagesContainer.ClearArt
            Me.lblClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearArt.Image.Width, Me.pbClearArt.Image.Height)
            Me.lblClearArtSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.ClearLogo = tImage
            Me.pbClearLogo.Image = Me.tmpDBElement.ImagesContainer.ClearLogo.ImageOriginal.Image
            Me.pbClearLogo.Tag = Me.tmpDBElement.ImagesContainer.ClearLogo
            Me.lblClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbClearLogo.Image.Width, Me.pbClearLogo.Image.Height)
            Me.lblClearLogoSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.DiscArt = tImage
            Me.pbDiscArt.Image = Me.tmpDBElement.ImagesContainer.DiscArt.ImageOriginal.Image
            Me.pbDiscArt.Tag = Me.tmpDBElement.ImagesContainer.DiscArt
            Me.lblDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbDiscArt.Image.Width, Me.pbDiscArt.Image.Height)
            Me.lblDiscArtSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.Fanart = tImage
            Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
            Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart
            Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
            Me.lblFanartSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.Landscape = tImage
            Me.pbLandscape.Image = Me.tmpDBElement.ImagesContainer.Landscape.ImageOriginal.Image
            Me.pbLandscape.Tag = Me.tmpDBElement.ImagesContainer.Landscape
            Me.lblLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbLandscape.Image.Width, Me.pbLandscape.Image.Height)
            Me.lblLandscapeSize.Visible = True
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
            Me.tmpDBElement.ImagesContainer.Poster = tImage
            Me.pbPoster.Image = Me.tmpDBElement.ImagesContainer.Poster.ImageOriginal.Image
            Me.pbPoster.Tag = Me.tmpDBElement.ImagesContainer.Poster
            Me.lblPosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbPoster.Image.Width, Me.pbPoster.Image.Height)
            Me.lblPosterSize.Visible = True
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
        Me.tmpRating = Me.pbStar1.Tag.ToString
    End Sub

    Private Sub pbStar1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar1.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar1.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar1.Tag = 0.5
                Me.BuildStars(0.5)
            Else
                Me.pbStar1.Tag = 1
                Me.BuildStars(1)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar2.Click
        Me.tmpRating = Me.pbStar2.Tag.ToString
    End Sub

    Private Sub pbStar2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar2.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar2.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar2.Tag = 1.5
                Me.BuildStars(1.5)
            Else
                Me.pbStar2.Tag = 2
                Me.BuildStars(2)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar3.Click
        Me.tmpRating = Me.pbStar3.Tag.ToString
    End Sub

    Private Sub pbStar3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar3.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar3.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar3.Tag = 2.5
                Me.BuildStars(2.5)
            Else
                Me.pbStar3.Tag = 3
                Me.BuildStars(3)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar4.Click
        Me.tmpRating = Me.pbStar4.Tag.ToString
    End Sub

    Private Sub pbStar4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar4.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar4_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar4.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar4.Tag = 3.5
                Me.BuildStars(3.5)
            Else
                Me.pbStar4.Tag = 4
                Me.BuildStars(4)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar5.Click
        Me.tmpRating = Me.pbStar5.Tag.ToString
    End Sub

    Private Sub pbStar5_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar5.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar5_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar5.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar5.Tag = 4.5
                Me.BuildStars(4.5)
            Else
                Me.pbStar5.Tag = 5
                Me.BuildStars(5)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar6.Click
        Me.tmpRating = Me.pbStar6.Tag.ToString
    End Sub

    Private Sub pbStar6_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar6.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar6.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar6.Tag = 5.5
                Me.BuildStars(5.5)
            Else
                Me.pbStar6.Tag = 6
                Me.BuildStars(6)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar7.Click
        Me.tmpRating = Me.pbStar7.Tag.ToString
    End Sub

    Private Sub pbStar7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar7.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar7_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar7.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar7.Tag = 6.5
                Me.BuildStars(6.5)
            Else
                Me.pbStar7.Tag = 7
                Me.BuildStars(7)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar8.Click
        Me.tmpRating = Me.pbStar8.Tag.ToString
    End Sub

    Private Sub pbStar8_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar8.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar8_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar8.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar8.Tag = 7.5
                Me.BuildStars(7.5)
            Else
                Me.pbStar8.Tag = 8
                Me.BuildStars(8)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar9.Click
        Me.tmpRating = Me.pbStar9.Tag.ToString
    End Sub

    Private Sub pbStar9_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar9.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar9_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar9.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar9.Tag = 8.5
                Me.BuildStars(8.5)
            Else
                Me.pbStar9.Tag = 9
                Me.BuildStars(9)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbStar10.Click
        Me.tmpRating = Me.pbStar10.Tag.ToString
    End Sub

    Private Sub pbStar10_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbStar10.MouseLeave
        Try
            Dim tmpDBL As Single = 0
            Single.TryParse(Me.tmpRating, tmpDBL)
            Me.BuildStars(tmpDBL)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub pbStar10_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbStar10.MouseMove
        Try
            If e.X < 12 Then
                Me.pbStar10.Tag = 9.5
                Me.BuildStars(9.5)
            Else
                Me.pbStar10.Tag = 10
                Me.BuildStars(10)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RefreshExtrafanarts()
        'Me.pnlExtrafanarts.AutoScrollPosition = New Point(0, 0)
        iEFTop = 1
        While Me.pnlExtrafanarts.Controls.Count > 0
            Me.pnlExtrafanarts.Controls(0).Dispose()
        End While

        If Me.tmpDBElement.ImagesContainer.Extrafanarts.Count > 0 Then
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In Me.tmpDBElement.ImagesContainer.Extrafanarts
                AddExtrafanartImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub RefreshExtrathumbs()
        'Me.pnlExtrathumbs.AutoScrollPosition = New Point(0, 0)
        iETTop = 1
        While Me.pnlExtrathumbs.Controls.Count > 0
            Me.pnlExtrathumbs.Controls(0).Dispose()
        End While

        If Me.tmpDBElement.ImagesContainer.Extrathumbs.Count > 0 Then
            tmpDBElement.ImagesContainer.SortExtrathumbs()
            Dim iIndex As Integer = 0
            For Each img As MediaContainers.Image In Me.tmpDBElement.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                img.Index = iIndex
                AddExtrathumbImage(String.Concat(img.Width, " x ", img.Height), iIndex, img)
                iIndex += 1
            Next
        End If
    End Sub

    Private Sub SelectMPAA()
        Try
            If Not String.IsNullOrEmpty(Me.tmpDBElement.Movie.MPAA) Then
                If Master.eSettings.MovieScraperCertOnlyValue Then
                    Dim sItem As String = String.Empty
                    For i As Integer = 0 To Me.lbMPAA.Items.Count - 1
                        sItem = Me.lbMPAA.Items(i).ToString
                        If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = Me.tmpDBElement.Movie.MPAA Then
                            Me.lbMPAA.SelectedIndex = i
                            Me.lbMPAA.TopIndex = i
                            Exit For
                        End If
                    Next
                Else
                    Dim i As Integer = 0
                    For ctr As Integer = 0 To Me.lbMPAA.Items.Count - 1
                        If Me.tmpDBElement.Movie.MPAA.ToLower.StartsWith(Me.lbMPAA.Items.Item(ctr).ToString.ToLower) Then
                            i = ctr
                            Exit For
                        End If
                    Next
                    Me.lbMPAA.SelectedIndex = i
                    Me.lbMPAA.TopIndex = i

                    Dim strMPAA As String = String.Empty
                    Dim strMPAADesc As String = String.Empty
                    If i > 0 Then
                        strMPAA = Me.lbMPAA.Items.Item(i).ToString
                        strMPAADesc = Me.tmpDBElement.Movie.MPAA.Replace(strMPAA, String.Empty).Trim
                        Me.txtMPAA.Text = strMPAA
                        Me.txtMPAADesc.Text = strMPAADesc
                    Else
                        Me.txtMPAA.Text = Me.tmpDBElement.Movie.MPAA
                    End If
                End If
            End If

            If Me.lbMPAA.SelectedItems.Count = 0 Then
                Me.lbMPAA.SelectedIndex = 0
                Me.lbMPAA.TopIndex = 0
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetInfo()
        Try
            With Me

                Me.OK_Button.Enabled = False
                Me.Cancel_Button.Enabled = False
                Me.btnRescrape.Enabled = False
                Me.btnChangeMovie.Enabled = False

                Me.tmpDBElement.IsMark = Me.chkMark.Checked

                If Not String.IsNullOrEmpty(.txtSortTitle.Text) Then
                    Me.tmpDBElement.Movie.SortTitle = .txtSortTitle.Text.Trim
                Else
                    Me.tmpDBElement.Movie.SortTitle = StringUtils.SortTokens_Movie(.txtTitle.Text.Trim)
                End If

                Me.tmpDBElement.Movie.Tagline = .txtTagline.Text.Trim
                Me.tmpDBElement.Movie.Year = .mtxtYear.Text.Trim
                Me.tmpDBElement.Movie.Votes = .txtVotes.Text.Trim
                Me.tmpDBElement.Movie.Outline = .txtOutline.Text.Trim
                Me.tmpDBElement.Movie.Plot = .txtPlot.Text.Trim
                Me.tmpDBElement.Movie.Top250 = .txtTop250.Text.Trim
                Me.tmpDBElement.Movie.Country = .txtCountry.Text.Trim
                Me.tmpDBElement.Movie.Director = .txtDirector.Text.Trim
                Me.tmpDBElement.Movie.Title = .txtTitle.Text.Trim
                Me.tmpDBElement.Movie.Certification = .txtCerts.Text.Trim
                Me.tmpDBElement.Movie.OriginalTitle = .txtOriginalTitle.Text.Trim
                Me.tmpDBElement.Movie.MPAA = String.Concat(.txtMPAA.Text, " ", .txtMPAADesc.Text).Trim
                Me.tmpDBElement.Movie.Runtime = .txtRuntime.Text.Trim
                Me.tmpDBElement.Movie.ReleaseDate = .txtReleaseDate.Text.Trim
                Me.tmpDBElement.Movie.OldCredits = .txtCredits.Text.Trim
                Me.tmpDBElement.Movie.Studio = .txtStudio.Text.Trim
                Me.tmpDBElement.VideoSource = .txtVideoSource.Text.Trim
                Me.tmpDBElement.Movie.VideoSource = .txtVideoSource.Text.Trim
                Me.tmpDBElement.Movie.Trailer = .txtTrailer.Text.Trim
                Me.tmpDBElement.ListTitle = StringUtils.ListTitle_Movie(.txtTitle.Text, .mtxtYear.Text)

                If Not .tmpRating.Trim = String.Empty AndAlso .tmpRating.Trim <> "0" Then
                    Me.tmpDBElement.Movie.Rating = .tmpRating
                Else
                    Me.tmpDBElement.Movie.Rating = String.Empty
                End If

                'cocotus, 2013/02 Playcount/Watched state support added
                'if watched-checkbox is checked -> save Playcount=1 in nfo
                If chkWatched.Checked Then
                    'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                    If Not Me.tmpDBElement.Movie.PlayCountSpecified Then
                        Me.tmpDBElement.Movie.PlayCount = 1
                        Me.tmpDBElement.Movie.LastPlayed = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    End If
                Else
                    'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                    If Me.tmpDBElement.Movie.PlayCountSpecified Then
                        Me.tmpDBElement.Movie.PlayCount = 0
                        Me.tmpDBElement.Movie.LastPlayed = String.Empty
                    End If
                End If
                'cocotus End

                If .clbGenre.CheckedItems.Count > 0 Then
                    If .clbGenre.CheckedIndices.Contains(0) Then
                        Me.tmpDBElement.Movie.Genre = String.Empty
                    Else
                        Dim strGenre As String = String.Empty
                        Dim isFirst As Boolean = True
                        Dim iChecked = From iCheck In .clbGenre.CheckedItems
                        strGenre = String.Join(" / ", iChecked.ToArray)
                        Me.tmpDBElement.Movie.Genre = strGenre.Trim
                    End If
                End If

                Me.tmpDBElement.Movie.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    Dim iOrder As Integer = 0
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.ID = CInt(lviActor.Text)
                        addActor.Name = lviActor.SubItems(1).Text.Trim
                        addActor.Role = lviActor.SubItems(2).Text.Trim
                        addActor.ThumbURL = lviActor.SubItems(3).Text.Trim
                        addActor.Order = iOrder
                        iOrder += 1
                        Me.tmpDBElement.Movie.Actors.Add(addActor)
                    Next
                End If

                If ActorThumbsHasChanged Then
                    For Each act As MediaContainers.Person In Me.tmpDBElement.Movie.Actors
                        Dim img As New Images
                        img.FromWeb(act.ThumbURL)
                        If img.Image IsNot Nothing Then
                            act.ThumbPath = img.SaveAsMovieActorThumb(act, Directory.GetParent(Me.tmpDBElement.Filename).FullName, Me.tmpDBElement)
                        Else
                            act.ThumbPath = String.Empty
                        End If
                    Next
                End If

                If Not String.IsNullOrEmpty(.MovieTheme.Extention) AndAlso Not MovieTheme.toRemove Then 'TODO: proper check, extention check is only a woraround
                    Dim tPath As String = .MovieTheme.SaveAsMovieTheme(Me.tmpDBElement)
                    Me.tmpDBElement.ThemePath = tPath
                Else
                    Themes.DeleteMovieTheme(Me.tmpDBElement)
                    Me.tmpDBElement.ThemePath = String.Empty
                End If

                If Path.GetExtension(Me.tmpDBElement.Filename) = ".disc" Then
                    Dim StubFile As String = Me.tmpDBElement.Filename
                    Dim Title As String = Me.txtMediaStubTitle.Text
                    Dim Message As String = Me.txtMediaStubMessage.Text
                    MediaStub.SaveDiscStub(StubFile, Title, Message)
                End If

                If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso pResults.Posters.Count > 0 Then Me.tmpDBElement.Movie.Thumb = pResults.Posters
                If Not Master.eSettings.MovieImagesNotSaveURLToNfo AndAlso fResults.Fanart.Thumb.Count > 0 Then Me.tmpDBElement.Movie.Fanart = pResults.Fanart

                Dim removeSubtitles As New List(Of MediaInfo.Subtitle)
                For Each Subtitle In Me.tmpDBElement.Subtitles
                    If Subtitle.toRemove Then
                        removeSubtitles.Add(Subtitle)
                    End If
                Next
                For Each Subtitle In removeSubtitles
                    If File.Exists(Subtitle.SubsPath) Then
                        File.Delete(Subtitle.SubsPath)
                    End If
                    Me.tmpDBElement.Subtitles.Remove(Subtitle)
                Next

            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        'Download
        Dim strDownload As String = Master.eLang.GetString(373, "Download")
        Me.btnSetBannerDL.Text = strDownload
        Me.btnSetClearArtDL.Text = strDownload
        Me.btnSetClearLogoDL.Text = strDownload
        Me.btnSetDiscArtDL.Text = strDownload
        Me.btnSetFanartDL.Text = strDownload
        Me.btnSetLandscapeDL.Text = strDownload
        Me.btnSetPosterDL.Text = strDownload
        Me.btnSetSubtitleDL.Text = strDownload
        Me.btnSetThemeDL.Text = strDownload
        Me.btnSetTrailerDL.Text = strDownload

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        Me.btnSetBannerLocal.Text = strLocalBrowse
        Me.btnSetClearArtLocal.Text = strLocalBrowse
        Me.btnSetClearLogoLocal.Text = strLocalBrowse
        Me.btnSetDiscArtLocal.Text = strLocalBrowse
        Me.btnSetFanartLocal.Text = strLocalBrowse
        Me.btnSetLandscapeLocal.Text = strLocalBrowse
        Me.btnSetPosterLocal.Text = strLocalBrowse
        Me.btnSetSubtitleLocal.Text = strLocalBrowse
        Me.btnSetThemeLocal.Text = strLocalBrowse
        Me.btnSetTrailerLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        Me.btnRemoveBanner.Text = strRemove
        Me.btnRemoveClearArt.Text = strRemove
        Me.btnRemoveClearLogo.Text = strRemove
        Me.btnRemoveDiscArt.Text = strRemove
        Me.btnRemoveFanart.Text = strRemove
        Me.btnRemoveLandscape.Text = strRemove
        Me.btnRemovePoster.Text = strRemove
        Me.btnRemoveSubtitle.Text = strRemove
        Me.btnRemoveTheme.Text = strRemove
        Me.btnRemoveTrailer.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        Me.btnSetBannerScrape.Text = strScrape
        Me.btnSetClearArtScrape.Text = strScrape
        Me.btnSetClearLogoScrape.Text = strScrape
        Me.btnSetDiscArtScrape.Text = strScrape
        Me.btnSetFanartScrape.Text = strScrape
        Me.btnSetLandscapeScrape.Text = strScrape
        Me.btnSetPosterScrape.Text = strScrape
        Me.btnSetSubtitleScrape.Text = strScrape
        Me.btnSetThemeScrape.Text = strScrape
        Me.btnSetTrailerScrape.Text = strScrape

        Dim mTitle As String = Me.tmpDBElement.Movie.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Text = sTitle
        Me.tsFilename.Text = Me.tmpDBElement.Filename
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.btnChangeMovie.Text = Master.eLang.GetString(32, "Change Movie")
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.btnExtrafanartsSetAsFanart.Text = Me.btnExtrathumbsSetAsFanart.Text
        Me.btnExtrathumbsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        Me.btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        Me.chkMark.Text = Master.eLang.GetString(23, "Mark")
        Me.chkWatched.Text = Master.eLang.GetString(981, "Watched")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblCerts.Text = Master.eLang.GetString(237, "Certification(s):")
        Me.lblCountry.Text = String.Concat(Master.eLang.GetString(301, "Country"), ":")
        Me.lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        Me.lblDirector.Text = Master.eLang.GetString(239, "Director:")
        Me.lblVideoSource.Text = Master.eLang.GetString(824, "Video Source:")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        Me.lblMPAADesc.Text = Master.eLang.GetString(229, "MPAA Rating Description:")
        Me.lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        Me.lblOutline.Text = Master.eLang.GetString(242, "Plot Outline:")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblRating.Text = Master.eLang.GetString(245, "Rating:")
        Me.lblReleaseDate.Text = Master.eLang.GetString(236, "Release Date:")
        Me.lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        Me.lblSortTilte.Text = String.Concat(Master.eLang.GetString(642, "Sort Title"), ":")
        Me.lblStudio.Text = Master.eLang.GetString(226, "Studio:")
        Me.lblTagline.Text = Master.eLang.GetString(243, "Tagline:")
        Me.lblTitle.Text = Master.eLang.GetString(246, "Title:")
        Me.lblTop250.Text = Master.eLang.GetString(240, "Top 250:")
        Me.lblTopDetails.Text = Master.eLang.GetString(224, "Edit the details for the selected movie.")
        Me.lblTopTitle.Text = Master.eLang.GetString(25, "Edit Movie")
        Me.lblTrailerURL.Text = Master.eLang.GetString(227, "Trailer URL:")
        Me.lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        Me.lblYear.Text = Master.eLang.GetString(49, "Year:")
        Me.tpBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.tpDetails.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.tpDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.tpExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        Me.tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.tpMetaData.Text = Master.eLang.GetString(866, "Metadata")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")
    End Sub

    Private Sub tcEditMovie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcEdit.SelectedIndexChanged
        Me.lvSubtitles.SelectedItems.Clear()
        Me.ThemeStop()
        Me.TrailerStop()
    End Sub

    Private Sub txtThumbCount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub txtThumbCount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub txtTrailer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrailer.TextChanged
        If StringUtils.isValidURL(txtTrailer.Text) Then
            Me.btnPlayTrailer.Enabled = True
        Else
            Me.btnPlayTrailer.Enabled = False
        End If
    End Sub

    Sub GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        If mType = Enums.ModuleEventType.FrameExtrator_Movie AndAlso _params IsNot Nothing Then
            If _params(0).ToString = "FanartToSave" Then
                Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.FromFile(Path.Combine(Master.TempPath, "frame.jpg"))
                If Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image IsNot Nothing Then
                    Me.pbFanart.Image = Me.tmpDBElement.ImagesContainer.Fanart.ImageOriginal.Image
                    Me.pbFanart.Tag = Me.tmpDBElement.ImagesContainer.Fanart

                    Me.lblFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbFanart.Image.Width, Me.pbFanart.Image.Height)
                    Me.lblFanartSize.Visible = True
                End If
            ElseIf _params(0).ToString = "EFanartToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    Dim eImg As New MediaContainers.Image
                    eImg.ImageOriginal.FromFile(fPath)
                    tmpDBElement.ImagesContainer.Extrafanarts.Add(eImg)
                    Me.RefreshExtrafanarts()
                End If
            ElseIf _params(0).ToString = "EThumbToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    Dim eImg As New MediaContainers.Image
                    eImg.ImageOriginal.FromFile(fPath)
                    tmpDBElement.ImagesContainer.Extrathumbs.Add(eImg)
                    Me.RefreshExtrathumbs()
                End If
            End If
        End If
    End Sub

    Private Sub txtOutline_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtOutline.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtOutline.SelectAll()
        End If
    End Sub

    Private Sub txtPlot_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtPlot.KeyDown
        If e.KeyData = (Keys.Control Or Keys.A) Then
            Me.txtPlot.SelectAll()
        End If
    End Sub

    Private Sub lvSubtitles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvSubtitles.KeyDown
        If e.KeyCode = Keys.Delete Then Me.DeleteSubtitle()
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
                txtSubtitlesPreview.Text = ReadSubtitle(Me.lvSubtitles.SelectedItems.Item(0).SubItems(1).Text.ToString)
            End If
        Else
            btnRemoveSubtitle.Enabled = False
            txtSubtitlesPreview.Clear()
        End If
    End Sub

    Private Function ReadSubtitle(ByVal sPath As String) As String
        Dim sText As String = String.Empty

        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Try
                Dim objReader As New StreamReader(sPath)

                sText = objReader.ReadToEnd

                objReader.Close()

                Return sText
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End If

        Return String.Empty
    End Function

    Private Sub EditSubtitle()
        Try
            If lvSubtitles.SelectedItems.Count > 0 Then
                Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
                Dim tmpFileInfo As New MediaInfo.Fileinfo
                tmpFileInfo.StreamDetails.Subtitle.AddRange(Me.tmpDBElement.Subtitles)
                Using dEditStream As New dlgFIStreamEditor
                    Dim stream As Object = dEditStream.ShowDialog(i.Tag.ToString, tmpFileInfo, Convert.ToInt16(i.Text))
                    If Not stream Is Nothing Then
                        If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                            Me.tmpDBElement.Subtitles(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaInfo.Subtitle)
                        End If
                        'NeedToRefresh = True
                        LoadSubtitles()
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DeleteSubtitle()
        Try
            If lvSubtitles.SelectedItems.Count > 0 Then
                Dim i As ListViewItem = lvSubtitles.SelectedItems(0)
                If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Stream") Then
                    Me.tmpDBElement.Subtitles(Convert.ToInt16(i.Text)).toRemove = True
                End If
                'NeedToRefresh = True
                LoadSubtitles()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub LoadSubtitles()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvSubtitles.Groups.Clear()
        lvSubtitles.Items.Clear()
        Try
            If Me.tmpDBElement.Subtitles.Count > 0 Then
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
                Dim s As MediaInfo.Subtitle
                For c = 0 To Me.tmpDBElement.Subtitles.Count - 1
                    s = Me.tmpDBElement.Subtitles(c)
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


        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class