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

Public Class dlgEditMovie

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwEThumbs As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwEFanarts As New System.ComponentModel.BackgroundWorker

    Private CachePath As String = String.Empty
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private lvwActorSorter As ListViewColumnSorter
    'Private lvwEThumbsSorter As ListViewColumnSorter
    'Private lvwEFanartsSorter As ListViewColumnSorter
    Private MovieBanner As New Images With {.IsEdit = True}
    Private MovieClearArt As New Images With {.IsEdit = True}
    Private MovieClearLogo As New Images With {.IsEdit = True}
    Private MovieDiscArt As New Images With {.IsEdit = True}
    Private MovieFanart As New Images With {.IsEdit = True}
    Private MovieLandscape As New Images With {.IsEdit = True}
    Private MoviePoster As New Images With {.IsEdit = True}
    Private pResults As New Containers.ImgResult
    Private PreviousFrameValue As Integer
    Private MovieTrailer As New Trailers
    Private MovieTheme As New Themes
    Private tmpRating As String = String.Empty

    'Extrathumbs
    Private etDeleteList As New List(Of String)
    Private EThumbsIndex As Integer = -1
    Private EThumbsList As New List(Of ExtraImages)
    Private EThumbsExtractor As New List(Of String)
    Private EThumbsWarning As Boolean = True
    Private hasClearedET As Boolean = False
    Private iETCounter As Integer = 0
    Private iETLeft As Integer = 1
    Private iETTop As Integer = 1
    Private pbETImage() As PictureBox
    Private pnlETImage() As Panel

    'Extrafanarts
    Private efDeleteList As New List(Of String)
    Private EFanartsIndex As Integer = -1
    Private EFanartsList As New List(Of ExtraImages)
    Private EFanartsExtractor As New List(Of String)
    Private EFanartsWarning As Boolean = True
    Private hasClearedEF As Boolean = False
    Private iEFCounter As Integer = 0
    Private iEFLeft As Integer = 1
    Private iEFTop As Integer = 1
    Private pbEFImage() As PictureBox
    Private pnlEFImage() As Panel

#End Region 'Fields

#Region "Methods"
    Private Sub AddETImage(ByVal sDescription As String, ByVal iIndex As Integer, Extrathumb As ExtraImages)
        Try
            ReDim Preserve Me.pnlETImage(iIndex)
            ReDim Preserve Me.pbETImage(iIndex)
            Me.pnlETImage(iIndex) = New Panel()
            Me.pbETImage(iIndex) = New PictureBox()
            Me.pbETImage(iIndex).Name = iIndex.ToString
            Me.pnlETImage(iIndex).Name = iIndex.ToString
            Me.pnlETImage(iIndex).Size = New Size(128, 72)
            Me.pbETImage(iIndex).Size = New Size(128, 72)
            Me.pnlETImage(iIndex).BackColor = Color.White
            Me.pnlETImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbETImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlETImage(iIndex).Tag = Extrathumb.Image
            Me.pbETImage(iIndex).Tag = Extrathumb.Image
            Me.pbETImage(iIndex).Image = CType(Extrathumb.Image.Image.Clone(), Image)
            Me.pnlETImage(iIndex).Left = iETLeft
            Me.pbETImage(iIndex).Left = 0
            Me.pnlETImage(iIndex).Top = iETTop
            Me.pbETImage(iIndex).Top = 0
            Me.pnlMovieEThumbsBG.Controls.Add(Me.pnlETImage(iIndex))
            Me.pnlETImage(iIndex).Controls.Add(Me.pbETImage(iIndex))
            Me.pnlETImage(iIndex).BringToFront()
            AddHandler pbETImage(iIndex).Click, AddressOf pbETImage_Click
            AddHandler pnlETImage(iIndex).Click, AddressOf pnlETImage_Click
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iETTop += 74

    End Sub

    Private Sub AddEFImage(ByVal sDescription As String, ByVal iIndex As Integer, Extrafanart As ExtraImages)
        Try
            ReDim Preserve Me.pnlEFImage(iIndex)
            ReDim Preserve Me.pbEFImage(iIndex)
            Me.pnlEFImage(iIndex) = New Panel()
            Me.pbEFImage(iIndex) = New PictureBox()
            Me.pbEFImage(iIndex).Name = iIndex.ToString
            Me.pnlEFImage(iIndex).Name = iIndex.ToString
            Me.pnlEFImage(iIndex).Size = New Size(128, 72)
            Me.pbEFImage(iIndex).Size = New Size(128, 72)
            Me.pnlEFImage(iIndex).BackColor = Color.White
            Me.pnlEFImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbEFImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlEFImage(iIndex).Tag = Extrafanart.Image
            Me.pbEFImage(iIndex).Tag = Extrafanart.Image
            Me.pbEFImage(iIndex).Image = CType(Extrafanart.Image.Image.Clone(), Image)
            Me.pnlEFImage(iIndex).Left = iEFLeft
            Me.pbEFImage(iIndex).Left = 0
            Me.pnlEFImage(iIndex).Top = iEFTop
            Me.pbEFImage(iIndex).Top = 0
            Me.pnlMovieEFanartsBG.Controls.Add(Me.pnlEFImage(iIndex))
            Me.pnlEFImage(iIndex).Controls.Add(Me.pbEFImage(iIndex))
            Me.pnlEFImage(iIndex).BringToFront()
            AddHandler pbEFImage(iIndex).Click, AddressOf pbEFImage_Click
            AddHandler pnlEFImage(iIndex).Click, AddressOf pnlEFImage_Click
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iEFTop += 74

    End Sub

    Private Sub pbETImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectET(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Images))
    End Sub

    Private Sub pnlETImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectET(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Images))
    End Sub

    Private Sub pbEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, Images))
    End Sub

    Private Sub pnlEFImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectEF(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, Images))
    End Sub

    Private Sub DoSelectET(ByVal iIndex As Integer, tPoster As Images)
        Try
            Me.pbMovieEThumbs.Image = tPoster.Image
            Me.pbMovieEThumbs.Tag = tPoster
            Me.btnMovieEThumbsSetAsFanart.Enabled = True
            Me.EThumbsIndex = iIndex
            Me.lblMovieEThumbsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieEThumbs.Image.Width, Me.pbMovieEThumbs.Image.Height)
            Me.lblMovieEThumbsSize.Visible = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DoSelectEF(ByVal iIndex As Integer, tPoster As Images)
        Try
            Me.pbMovieEFanarts.Image = tPoster.Image
            Me.pbMovieEFanarts.Tag = tPoster
            Me.btnMovieEFanartsSetAsFanart.Enabled = True
            Me.EFanartsIndex = iIndex
            Me.lblMovieEFanartsSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieEFanarts.Image.Width, Me.pbMovieEFanarts.Image.Height)
            Me.lblMovieEFanartsSize.Visible = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnActorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorDown.Click
        If Me.lvActors.SelectedItems.Count > 0 AndAlso Not IsNothing(Me.lvActors.SelectedItems(0)) AndAlso Me.lvActors.SelectedIndices(0) < (Me.lvActors.Items.Count - 1) Then
            Dim iIndex As Integer = Me.lvActors.SelectedIndices(0)
            Me.lvActors.Items.Insert(iIndex + 2, DirectCast(Me.lvActors.SelectedItems(0).Clone, ListViewItem))
            Me.lvActors.Items.RemoveAt(iIndex)
            Me.lvActors.Items(iIndex + 1).Selected = True
            Me.lvActors.Select()
        End If
    End Sub

    Private Sub btnActorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActorUp.Click
        Try
            If Me.lvActors.SelectedItems.Count > 0 AndAlso Not IsNothing(Me.lvActors.SelectedItems(0)) AndAlso Me.lvActors.SelectedIndices(0) > 0 Then
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
            If Not IsNothing(eActor) Then
                Dim lvItem As ListViewItem = Me.lvActors.Items.Add(eActor.Name)
                lvItem.SubItems.Add(eActor.Role)
                lvItem.SubItems.Add(eActor.Thumb)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnChangeMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeMovie.Click
        Me.CleanUp()
        ' ***
        Me.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.Close()
    End Sub

    Private Sub btnDLTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLTheme.Click
        Dim aUrlList As New List(Of Themes)
        Dim tURL As String = String.Empty
        If Not ModulesManager.Instance.MovieScrapeTheme(Master.currMovie, aUrlList) Then
            Using dThemeSelect As New dlgThemeSelect()
                MovieTheme = dThemeSelect.ShowDialog(Master.currMovie, aUrlList)
            End Using
        End If

        If Not String.IsNullOrEmpty(MovieTheme.URL) Then
            Me.btnPlayTheme.Enabled = True
        End If
    End Sub

    Private Sub btnDLTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDLTrailer.Click
        Dim aUrlList As New List(Of Trailers)
        Dim tURL As String = String.Empty
        If Not ModulesManager.Instance.MovieScrapeTrailer(Master.currMovie, Enums.ScraperCapabilities.Trailer, aUrlList) Then
            Using dTrailerSelect As New dlgTrailerSelect()
                tURL = dTrailerSelect.ShowDialog(Master.currMovie, aUrlList)
            End Using
        End If

        If Not String.IsNullOrEmpty(tURL) Then
            Me.btnPlayTrailer.Enabled = True
            If StringUtils.isValidURL(tURL) Then
                Me.txtTrailer.Text = tURL
            Else
                Master.currMovie.TrailerPath = tURL
                Me.lblLocalTrailer.Visible = True
            End If
        End If
    End Sub

    ' temporarily disabled
    'Private Sub btnEThumbsDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsDown.Click
    '    Try
    '        If EThumbsList.Count > 0 AndAlso EThumbsIndex < (EThumbsList.Count - 1) Then
    '            Dim iIndex As Integer = EThumbsIndex
    '            EThumbsList.Item(iIndex).Index = EThumbsList.Item(iIndex).Index + 1
    '            EThumbsList.Item(iIndex + 1).Index = EThumbsList.Item(iIndex + 1).Index - 1
    '        End If
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    Private Sub btnEditActor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditActor.Click
        EditActor()
    End Sub

    Private Sub btnManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManual.Click
        Try
            If dlgManualEdit.ShowDialog(Master.currMovie.NfoPath) = Windows.Forms.DialogResult.OK Then
                Master.currMovie.Movie = NFO.LoadMovieFromNFO(Master.currMovie.NfoPath, Master.currMovie.isSingle)
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

            If Not String.IsNullOrEmpty(Master.currMovie.TrailerPath) Then
                tPath = String.Concat("""", Master.currMovie.TrailerPath, """")
            ElseIf Not String.IsNullOrEmpty(Me.txtTrailer.Text) Then
                tPath = String.Concat("""", Me.txtTrailer.Text, """")
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.isWindows Then
                    If Regex.IsMatch(tPath, "plugin:\/\/plugin\.video\.youtube\/\?action=play_video&videoid=") Then
                        tPath = Replace(tPath, "plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
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
            MsgBox(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), MsgBoxStyle.Critical, Master.eLang.GetString(271, "Error Playing Trailer"))
        End Try
    End Sub

    Private Sub btnPlayTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTheme.Click
        'TODO 2013/12/18 Dekker500 - This should be re-factored to use Functions.Launch. Why is the URL different for non-windows??? Need to test first before editing
        Try

            Dim tPath As String = String.Empty

            If Not String.IsNullOrEmpty(Master.currMovie.ThemePath) Then
                tPath = String.Concat("""", Master.currMovie.ThemePath, """")
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
            MsgBox(Master.eLang.GetString(1078, "The theme could not be played. This could due be you don't have the proper player to play the theme type."), MsgBoxStyle.Critical, Master.eLang.GetString(1079, "Error Playing Theme"))
        End Try
    End Sub

    Private Sub btnRemoveMovieBanner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieBanner.Click
        Me.pbMovieBanner.Image = Nothing
        Me.pbMovieBanner.Tag = Nothing
        Me.MovieBanner.Dispose()
    End Sub

    Private Sub btnRemoveMovieClearArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieClearArt.Click
        Me.pbMovieClearArt.Image = Nothing
        Me.pbMovieClearArt.Tag = Nothing
        Me.MovieClearArt.Dispose()
    End Sub

    Private Sub btnRemoveMovieClearLogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieClearLogo.Click
        Me.pbMovieClearLogo.Image = Nothing
        Me.pbMovieClearLogo.Tag = Nothing
        Me.MovieClearLogo.Dispose()
    End Sub

    Private Sub btnRemoveMovieDiscArt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieDiscArt.Click
        Me.pbMovieDiscArt.Image = Nothing
        Me.pbMovieDiscArt.Tag = Nothing
        Me.MovieDiscArt.Dispose()
    End Sub

    Private Sub btnRemoveMovieFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieFanart.Click
        Me.pbMovieFanart.Image = Nothing
        Me.pbMovieFanart.Tag = Nothing
        Me.MovieFanart.Dispose()
    End Sub

    Private Sub btnRemoveMovieLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMovieLandscape.Click
        Me.pbMovieLandscape.Image = Nothing
        Me.pbMovieLandscape.Tag = Nothing
        Me.MovieLandscape.Dispose()
    End Sub

    Private Sub btnRemoveMoviePoster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveMoviePoster.Click
        Me.pbMoviePoster.Image = Nothing
        Me.pbMoviePoster.Tag = Nothing
        Me.MoviePoster.Dispose()
    End Sub

    Private Sub btnMovieEThumbsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEThumbsRemove.Click
        Me.DeleteEThumbs()
        Me.RefreshEThumbs()
        Me.lblMovieEThumbsSize.Text = ""
        Me.lblMovieEThumbsSize.Visible = False
    End Sub

    Private Sub btnMovieEFanartsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEFanartsRemove.Click
        Me.DeleteEFanarts()
        Me.RefreshEFanarts()
        Me.lblMovieEFanartsSize.Text = ""
        Me.lblMovieEFanartsSize.Visible = False
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Me.DeleteActors()
    End Sub

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
        Me.CleanUp()
        ' ***
        Me.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub btnSetMovieBannerDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieBanner = tImage
                        Me.pbMovieBanner.Image = MovieBanner.Image
                        Me.pbMovieBanner.Tag = MovieBanner

                        Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
                        Me.lblMovieBannerSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "Banner.jpg")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Banner, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Banner, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMovieBanner.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
                                Me.lblMovieBannerSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MovieBanner = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(1057, "No banner images could be found. Please check to see if any banner scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(972, "No Banners Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieBanner.FromFile(ofdImage.FileName)
                Me.pbMovieBanner.Image = MovieBanner.Image
                Me.pbMovieBanner.Tag = MovieBanner

                Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
                Me.lblMovieBannerSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieClearArt = tImage
                        Me.pbMovieClearArt.Image = MovieClearArt.Image
                        Me.pbMovieClearArt.Tag = MovieClearArt

                        Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
                        Me.lblMovieClearArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "ClearArt.png")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.ClearArt, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.ClearArt, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMovieClearArt.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
                                Me.lblMovieClearArtSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MovieClearArt = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(1099, "No ClearArt images could be found. Please check to see if any ClearArt scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(1102, "No ClearArts Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieClearArt.FromFile(ofdImage.FileName)
                Me.pbMovieClearArt.Image = MovieClearArt.Image
                Me.pbMovieClearArt.Tag = MovieClearArt

                Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
                Me.lblMovieClearArtSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieClearLogo = tImage
                        Me.pbMovieClearLogo.Image = MovieClearLogo.Image
                        Me.pbMovieClearLogo.Tag = MovieClearLogo

                        Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
                        Me.lblMovieClearLogoSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "ClearLogo.png")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.ClearLogo, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.ClearLogo, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMovieClearLogo.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
                                Me.lblMovieClearLogoSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MovieClearLogo = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(1100, "No ClearLogo images could be found. Please check to see if any ClearLogo scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(1103, "No ClearLogos Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieClearLogo.FromFile(ofdImage.FileName)
                Me.pbMovieClearLogo.Image = MovieClearLogo.Image
                Me.pbMovieClearLogo.Tag = MovieClearLogo

                Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
                Me.lblMovieClearLogoSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieDiscArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieDiscArt = tImage
                        Me.pbMovieDiscArt.Image = MovieDiscArt.Image
                        Me.pbMovieDiscArt.Tag = MovieDiscArt

                        Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
                        Me.lblMovieDiscArtSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieDiscArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "DiscArt.png")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.DiscArt, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.DiscArt, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMovieDiscArt.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
                                Me.lblMovieDiscArtSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MovieDiscArt = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(1101, "No DiscArt images could be found. Please check to see if any DiscArt scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(1104, "No DiscArts Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieDiscArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieDiscArt.FromFile(ofdImage.FileName)
                Me.pbMovieDiscArt.Image = MovieDiscArt.Image
                Me.pbMovieDiscArt.Tag = MovieDiscArt

                Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
                Me.lblMovieDiscArtSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnMovieEThumbsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEThumbsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EThumbsList.Item(Me.EThumbsIndex).Path) AndAlso Me.EThumbsList.Item(Me.EThumbsIndex).Path.Substring(0, 1) = ":" Then
            MovieFanart.FromWeb(Me.EThumbsList.Item(Me.EThumbsIndex).Path.Substring(1, Me.EThumbsList.Item(Me.EThumbsIndex).Path.Length - 1))
        Else
            MovieFanart.FromFile(Me.EThumbsList.Item(Me.EThumbsIndex).Path)
        End If
        If Not IsNothing(MovieFanart.Image) Then
            Me.pbMovieFanart.Image = MovieFanart.Image
            Me.pbMovieFanart.Tag = MovieFanart

            Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
            Me.lblMovieFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnMovieEFanartsSetAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEFanartsSetAsFanart.Click
        If Not String.IsNullOrEmpty(Me.EFanartsList.Item(Me.EFanartsIndex).Path) AndAlso Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(0, 1) = ":" Then
            MovieFanart.FromWeb(Me.EFanartsList.Item(Me.EFanartsIndex).Path.Substring(1, Me.EFanartsList.Item(Me.EFanartsIndex).Path.Length - 1))
        Else
            MovieFanart.FromFile(Me.EFanartsList.Item(Me.EFanartsIndex).Path)
        End If
        If Not IsNothing(MovieFanart.Image) Then
            Me.pbMovieFanart.Image = MovieFanart.Image
            Me.pbMovieFanart.Tag = MovieFanart

            Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
            Me.lblMovieFanartSize.Visible = True
        End If
    End Sub

    Private Sub btnSetMovieFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieFanart = tImage
                        Me.pbMovieFanart.Image = MovieFanart.Image
                        Me.pbMovieFanart.Tag = MovieFanart

                        Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
                        Me.lblMovieFanartSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartScrape.Click
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim pResults As New MediaContainers.Image
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Fanart, aList, efList, etList, True) = DialogResult.OK Then
                        pResults = dlgImgS.Results
                        Master.currMovie.etList = dlgImgS.etList
                        Master.currMovie.efList = dlgImgS.efList
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            pbMovieFanart.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Cursor = Cursors.Default

                            Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
                            Me.lblMovieFanartSize.Visible = True
                        End If
                        MovieFanart = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(969, "No fanart could be found. Please check to see if any fanart scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(970, "No Fanart Found"))
                End If
            End If

            RefreshEFanarts()
            RefreshEThumbs()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 4
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieFanart.FromFile(ofdImage.FileName)
                Me.pbMovieFanart.Image = MovieFanart.Image
                Me.pbMovieFanart.Tag = MovieFanart

                Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
                Me.lblMovieFanartSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MovieLandscape = tImage
                        Me.pbMovieLandscape.Image = MovieLandscape.Image
                        Me.pbMovieLandscape.Tag = MovieLandscape

                        Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
                        Me.lblMovieLandscapeSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "Landscape.jpg")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Landscape, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Landscape, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMovieLandscape.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
                                Me.lblMovieLandscapeSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MovieLandscape = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(1058, "No landscape images could be found. Please check to see if any landscape scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(972, "No Landscapes Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MovieLandscape.FromFile(ofdImage.FileName)
                Me.pbMovieLandscape.Image = MovieLandscape.Image
                Me.pbMovieLandscape.Tag = MovieLandscape

                Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
                Me.lblMovieLandscapeSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMoviePosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterDL.Click
        Try
            Using dImgManual As New dlgImgManual
                Dim tImage As Images
                If dImgManual.ShowDialog() = DialogResult.OK Then
                    tImage = dImgManual.Results
                    If Not IsNothing(tImage.Image) Then
                        MoviePoster = tImage
                        Me.pbMoviePoster.Image = MoviePoster.Image
                        Me.pbMoviePoster.Tag = MoviePoster

                        Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
                        Me.lblMoviePosterSize.Visible = True
                    End If
                End If
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMoviePosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aList As New List(Of MediaContainers.Image)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Try
            Dim sPath As String = Path.Combine(Master.TempPath, "poster.jpg")

            'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
            If Not ModulesManager.Instance.MovieScrapeImages(Master.currMovie, Enums.ScraperCapabilities.Poster, aList) Then
                If aList.Count > 0 Then
                    dlgImgS = New dlgImgSelect()
                    If dlgImgS.ShowDialog(Master.currMovie, Enums.MovieImageType.Poster, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                        pResults = dlgImgS.Results
                        If Not String.IsNullOrEmpty(pResults.URL) Then
                            Cursor = Cursors.WaitCursor
                            pResults.WebImage.FromWeb(pResults.URL)
                            If Not IsNothing(pResults.WebImage.Image) Then
                                pbMoviePoster.Image = CType(pResults.WebImage.Image.Clone(), Image)
                                Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
                                Me.lblMoviePosterSize.Visible = True
                            End If
                            Cursor = Cursors.Default
                        End If
                        MoviePoster = pResults.WebImage
                    End If
                Else
                    MsgBox(Master.eLang.GetString(971, "No poster images could be found. Please check to see if any poster scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(972, "No Posters Found"))
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMoviePosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterLocal.Click
        Try
            With ofdImage
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = Master.eLang.GetString(497, "Images") + "|*.jpg;*.png"
                .FilterIndex = 0
            End With

            If ofdImage.ShowDialog() = DialogResult.OK Then
                MoviePoster.FromFile(ofdImage.FileName)
                Me.pbMoviePoster.Image = MoviePoster.Image
                Me.pbMoviePoster.Tag = MoviePoster

                Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
                Me.lblMoviePosterSize.Visible = True
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnStudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStudio.Click
        Using dStudio As New dlgStudioSelect
            Dim tStudio As String = dStudio.ShowDialog(Master.currMovie)
            If Not String.IsNullOrEmpty(tStudio) Then
                Me.txtStudio.Text = tStudio
            End If
        End Using
    End Sub

    Private Sub btnMovieEThumbsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEThumbsRefresh.Click
        Me.RefreshEThumbs()
    End Sub

    Private Sub btnMovieEFanartsRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieEFanartsRefresh.Click
        Me.RefreshEFanarts()
    End Sub

    ' temporarily disabled
    'Private Sub btnEThumbsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEThumbsUp.Click
    '    Try
    '        If lvEThumbs.Items.Count > 0 AndAlso lvEThumbs.SelectedIndices(0) > 0 Then
    '            Dim iIndex As Integer = lvEThumbs.SelectedIndices(0)
    '            lvEThumbs.Items(iIndex).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEThumbs.Items(iIndex).Text.Trim) - 1))
    '            lvEThumbs.Items(iIndex - 1).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEThumbs.Items(iIndex - 1).Text.Trim) + 1))
    '            lvEThumbs.Sort()
    '        End If
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    ' temporarily disabled
    'Private Sub btnEFanartsUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If lvEFanarts.Items.Count > 0 AndAlso lvEFanarts.SelectedIndices(0) > 0 Then
    '            Dim iIndex As Integer = lvEFanarts.SelectedIndices(0)
    '            lvEFanarts.Items(iIndex).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEFanarts.Items(iIndex).Text.Trim) - 1))
    '            lvEFanarts.Items(iIndex - 1).Text = String.Concat("  ", CStr(Convert.ToInt32(lvEFanarts.Items(iIndex - 1).Text.Trim) + 1))
    '            lvEFanarts.Sort()
    '        End If
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    Private Sub BuildStars(ByVal sinRating As Single)

        Try
            'f'in MS and them leaving control arrays out of VB.NET
            With Me
                .pbStar1.Image = Nothing
                .pbStar2.Image = Nothing
                .pbStar3.Image = Nothing
                .pbStar4.Image = Nothing
                .pbStar5.Image = Nothing

                If sinRating >= 0.5 Then ' if rating is less than .5 out of ten, consider it a 0
                    Select Case (sinRating / 2)
                        Case Is <= 0.5
                            .pbStar1.Image = My.Resources.starhalf
                        Case Is <= 1
                            .pbStar1.Image = My.Resources.star
                        Case Is <= 1.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.starhalf
                        Case Is <= 2
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                        Case Is <= 2.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.starhalf
                        Case Is <= 3
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                        Case Is <= 3.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.starhalf
                        Case Is <= 4
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                        Case Is <= 4.5
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.starhalf
                        Case Else
                            .pbStar1.Image = My.Resources.star
                            .pbStar2.Image = My.Resources.star
                            .pbStar3.Image = My.Resources.star
                            .pbStar4.Image = My.Resources.star
                            .pbStar5.Image = My.Resources.star
                    End Select
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwEThumbs_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEThumbs.DoWork
        If Not Master.currMovie.ClearEThumbs OrElse hasClearedET Then LoadEThumbs()
    End Sub

    Private Sub bwEFanarts_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwEFanarts.DoWork
        If Not Master.currMovie.ClearEFanarts OrElse hasClearedEF Then LoadEFanarts()
    End Sub

    Private Sub bwEThumbs_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwEThumbs.RunWorkerCompleted
        Try
            If EThumbsList.Count > 0 Then
                For Each tEThumb As ExtraImages In EThumbsList
                    AddETImage(tEThumb.Name, tEThumb.Index, tEThumb)
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwEFanarts_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwEFanarts.RunWorkerCompleted
        Try
            If EFanartsList.Count > 0 Then
                For Each tEFanart As ExtraImages In EFanartsList
                    AddEFImage(tEFanart.Name, tEFanart.Index, tEFanart)
                Next
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.CleanUp()
        Master.currMovie = Master.DB.LoadMovieFromDB(Master.currMovie.ID)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CleanUp()
        Try
            If File.Exists(Path.Combine(Master.TempPath, "poster.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "poster.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "fanart.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "fanart.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "frame.jpg"))
            End If

            If Directory.Exists(Path.Combine(Master.TempPath, "extrathumbs")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrathumbs"))
            End If

            If Directory.Exists(Path.Combine(Master.TempPath, "extrafanarts")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrafanarts"))
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
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DeleteEThumbs()
        Try
            Dim iIndex As Integer = EThumbsIndex

            If iIndex >= 0 Then
                Dim tPath As String = Me.EThumbsList.Item(iIndex).Path
                If Me.EThumbsList.Item(iIndex).Path.Substring(0, 1) = ":" Then
                    Master.currMovie.etList.RemoveAll(Function(Str) Str = tPath)
                    EThumbsList.Remove(EThumbsList.Item(iIndex))
                Else
                    etDeleteList.Add(Me.EThumbsList.Item(iIndex).Path)
                    EThumbsList.Remove(EThumbsList.Item(iIndex))
                End If
                pbMovieEThumbs.Image = Nothing
                btnMovieEThumbsSetAsFanart.Enabled = False
            End If
            RenumberEThumbs()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DeleteEFanarts()
        Try
            Dim iIndex As Integer = EFanartsIndex

            If iIndex >= 0 Then
                Dim tPath As String = Me.EFanartsList.Item(iIndex).Path
                If Me.EFanartsList.Item(iIndex).Path.Substring(0, 1) = ":" Then
                    Master.currMovie.efList.RemoveAll(Function(Str) Str = tPath)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                Else
                    efDeleteList.Add(Me.EFanartsList.Item(iIndex).Path)
                    EFanartsList.Remove(EFanartsList.Item(iIndex))
                End If
                pbMovieEFanarts.Image = Nothing
                btnMovieEFanartsSetAsFanart.Enabled = False
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgEditMovie_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Me.MovieBanner.Dispose()
        Me.MovieBanner = Nothing

        Me.MovieClearArt.Dispose()
        Me.MovieClearArt = Nothing

        Me.MovieClearLogo.Dispose()
        Me.MovieClearLogo = Nothing

        Me.MovieDiscArt.Dispose()
        Me.MovieDiscArt = Nothing

        Me.EFanartsList.Clear()
        Me.EFanartsList = Nothing

        Me.EThumbsList.Clear()
        Me.EThumbsList = Nothing

        Me.MovieFanart.Dispose()
        Me.MovieFanart = Nothing

        Me.MovieLandscape.Dispose()
        Me.MovieLandscape = Nothing

        Me.MoviePoster.Dispose()
        Me.MoviePoster = Nothing
    End Sub

    Private Sub dlgEditMovie_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.bwEThumbs.IsBusy Then Me.bwEThumbs.CancelAsync()
        While Me.bwEThumbs.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If Me.bwEFanarts.IsBusy Then Me.bwEFanarts.CancelAsync()
        While Me.bwEFanarts.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.pbMovieBanner.AllowDrop = True
            Me.pbMovieClearArt.AllowDrop = True
            Me.pbMovieClearLogo.AllowDrop = True
            Me.pbMovieDiscArt.AllowDrop = True
            Me.pbMovieFanart.AllowDrop = True
            Me.pbMovieLandscape.AllowDrop = True
            Me.pbMoviePoster.AllowDrop = True

            Me.SetUp()
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.BeforeEditMovie, Nothing, Master.currMovie)
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

            Dim dFileInfoEdit As New dlgFileInfo
            dFileInfoEdit.TopLevel = False
            dFileInfoEdit.FormBorderStyle = FormBorderStyle.None
            dFileInfoEdit.BackColor = Color.White
            dFileInfoEdit.Cancel_Button.Visible = False
            Me.pnlFileInfo.Controls.Add(dFileInfoEdit)
            Dim oldwidth As Integer = dFileInfoEdit.Width
            dFileInfoEdit.Width = pnlFileInfo.Width
            dFileInfoEdit.Height = pnlFileInfo.Height
            dFileInfoEdit.Show(False)

            Me.LoadGenres()
            Me.LoadRatings()
            Dim params As New List(Of Object)(New Object() {New Panel})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieFrameExtrator, params, Nothing, True)
            pnlFrameExtrator.Controls.Add(DirectCast(params(0), Panel))
            If String.IsNullOrEmpty(pnlFrameExtrator.Controls.Item(0).Name) Then
                tcEditMovie.TabPages.Remove(tpFrameExtraction)
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
                Dim eActor As New MediaContainers.Person With {.Name = lvwItem.Text, .Role = lvwItem.SubItems(1).Text, .Thumb = lvwItem.SubItems(2).Text}
                Using dAddEditActor As New dlgAddEditActor
                    eActor = dAddEditActor.ShowDialog(False, eActor)
                End Using
                If Not IsNothing(eActor) Then
                    lvwItem.Text = eActor.Name
                    lvwItem.SubItems(1).Text = eActor.Role
                    lvwItem.SubItems(2).Text = eActor.Thumb
                    lvwItem.Selected = True
                    lvwItem.EnsureVisible()
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
                If String.IsNullOrEmpty(Master.currMovie.NfoPath) Then
                    .btnManual.Enabled = False
                End If

                Me.chkMark.Checked = Master.currMovie.IsMark
                'cocotus, 2013/02 Playcount/Watched state support added
                'When Edit Movie-Page is loaded, checkbox will be unchecked of playcount=0 or not set at all... 
                If Master.currMovie.Movie.PlayCount = "" Or Master.currMovie.Movie.PlayCount = "0" Then
                    Me.chkWatched.Checked = False
                Else
                    'Playcount <> Empty and not 0 -> Tag filled -> Checked!
                    Me.chkWatched.Checked = True
                End If
                'cocotus end
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Title) Then
                    .txtTitle.Text = Master.currMovie.Movie.Title
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.OriginalTitle) Then
                    If Master.currMovie.Movie.OriginalTitle <> StringUtils.FilterTokens(Master.currMovie.Movie.Title) Then
                        .txtOriginalTitle.Text = Master.currMovie.Movie.OriginalTitle
                    End If
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.SortTitle) Then
                    If Master.currMovie.Movie.SortTitle <> StringUtils.FilterTokens(Master.currMovie.Movie.Title) Then
                        .txtSortTitle.Text = Master.currMovie.Movie.SortTitle
                    End If
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Tagline) Then
                    .txtTagline.Text = Master.currMovie.Movie.Tagline
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Year) Then
                    .mtxtYear.Text = Master.currMovie.Movie.Year
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Votes) Then
                    .txtVotes.Text = Master.currMovie.Movie.Votes
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Outline) Then
                    .txtOutline.Text = Master.currMovie.Movie.Outline
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Plot) Then
                    .txtPlot.Text = Master.currMovie.Movie.Plot
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Top250) Then
                    .txtTop250.Text = Master.currMovie.Movie.Top250
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Country) Then
                    .txtCountry.Text = Master.currMovie.Movie.Country
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Runtime) Then
                    .txtRuntime.Text = Master.currMovie.Movie.Runtime
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.ReleaseDate) Then
                    .txtReleaseDate.Text = Master.currMovie.Movie.ReleaseDate
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Director) Then
                    .txtDirector.Text = Master.currMovie.Movie.Director
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.OldCredits) Then
                    .txtCredits.Text = Master.currMovie.Movie.OldCredits
                End If


                If Not String.IsNullOrEmpty(Master.currMovie.FileSource) Then
                    .txtFileSource.Text = Master.currMovie.FileSource
                End If

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Certification) Then
                    If Not String.IsNullOrEmpty(Master.eSettings.MovieScraperCertLang) Then
                        Dim lCert() As String = Master.currMovie.Movie.Certification.Trim.Split(Convert.ToChar("/"))
                        Dim fCert = From eCert In lCert Where Regex.IsMatch(eCert, String.Concat(Regex.Escape(Master.eSettings.MovieScraperCertLang), "\:(.*?)"))
                        If fCert.Count > 0 Then
                            .txtCerts.Text = fCert(0).ToString.Trim
                        Else
                            .txtCerts.Text = Master.currMovie.Movie.Certification
                        End If
                    Else
                        .txtCerts.Text = Master.currMovie.Movie.Certification
                    End If
                End If

                If String.IsNullOrEmpty(Master.currMovie.ThemePath) Then
                    .btnPlayTheme.Enabled = False
                End If

                .btnDLTheme.Enabled = Master.eSettings.MovieThemeEnable AndAlso Master.eSettings.MovieThemeAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Theme)

                Me.lblLocalTrailer.Visible = Not String.IsNullOrEmpty(Master.currMovie.TrailerPath)
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Trailer) Then
                    .txtTrailer.Text = Master.currMovie.Movie.Trailer
                Else
                    If String.IsNullOrEmpty(Master.currMovie.TrailerPath) Then
                        .btnPlayTrailer.Enabled = False
                    End If
                End If

                .btnDLTrailer.Enabled = Master.eSettings.MovieTrailerEnable AndAlso Master.eSettings.MovieTrailerAnyEnabled AndAlso ModulesManager.Instance.QueryTrailerScraperCapabilities(Enums.ScraperCapabilities.Trailer)

                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Studio) Then
                    .txtStudio.Text = Master.currMovie.Movie.Studio
                End If

                Me.SelectMPAA()

                For i As Integer = 0 To .clbGenre.Items.Count - 1
                    .clbGenre.SetItemChecked(i, False)
                Next
                If Not String.IsNullOrEmpty(Master.currMovie.Movie.Genre) Then
                    Dim genreArray() As String
                    genreArray = Strings.Split(Master.currMovie.Movie.Genre, " / ")
                    For g As Integer = 0 To UBound(genreArray)
                        If .clbGenre.FindString(genreArray(g).Trim) > 0 Then
                            .clbGenre.SetItemChecked(.clbGenre.FindString(genreArray(g).Trim), True)
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
                For Each imdbAct As MediaContainers.Person In Master.currMovie.Movie.Actors
                    lvItem = .lvActors.Items.Add(imdbAct.Name)
                    lvItem.SubItems.Add(imdbAct.Role)
                    lvItem.SubItems.Add(imdbAct.Thumb)
                Next

                Dim tRating As Single = NumUtils.ConvertToSingle(Master.currMovie.Movie.Rating)
                .tmpRating = tRating.ToString
                .pbStar1.Tag = tRating
                .pbStar2.Tag = tRating
                .pbStar3.Tag = tRating
                .pbStar4.Tag = tRating
                .pbStar5.Tag = tRating
                If tRating > 0 Then .BuildStars(tRating)

                If DoAll Then

                    If Not Master.currMovie.isSingle Then
                        tcEditMovie.TabPages.Remove(tpClearArt)
                        tcEditMovie.TabPages.Remove(tpClearLogo)
                        tcEditMovie.TabPages.Remove(tpDiscArt)
                        tcEditMovie.TabPages.Remove(tpEThumbs)
                        tcEditMovie.TabPages.Remove(tpEFanarts)
                        tcEditMovie.TabPages.Remove(tpLandscape)
                    Else
                        Dim pExt As String = Path.GetExtension(Master.currMovie.Filename).ToLower
                        If pExt = ".rar" OrElse pExt = ".iso" OrElse pExt = ".img" OrElse _
                        pExt = ".bin" OrElse pExt = ".cue" OrElse pExt = ".dat" OrElse _
                        pExt = ".disc" Then
                            tcEditMovie.TabPages.Remove(tpFrameExtraction)
                        Else
                            If Not pExt = ".disc" Then
                                tcEditMovie.TabPages.Remove(tpMediaStub)
                            End If
                            .bwEThumbs.WorkerSupportsCancellation = True
                            .bwEThumbs.RunWorkerAsync()
                            .bwEFanarts.WorkerSupportsCancellation = True
                            .bwEFanarts.RunWorkerAsync()
                        End If
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.BannerPath) AndAlso Master.currMovie.BannerPath.Substring(0, 1) = ":" Then
                        MovieBanner.FromWeb(Master.currMovie.BannerPath.Substring(1, Master.currMovie.BannerPath.Length - 1))
                    Else
                        MovieBanner.FromFile(Master.currMovie.BannerPath)
                    End If
                    If Not IsNothing(MovieBanner.Image) Then
                        .pbMovieBanner.Image = MovieBanner.Image
                        .pbMovieBanner.Tag = MovieBanner

                        .lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieBanner.Image.Width, .pbMovieBanner.Image.Height)
                        .lblMovieBannerSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.ClearArtPath) AndAlso Master.currMovie.ClearArtPath.Substring(0, 1) = ":" Then
                        MovieClearArt.FromWeb(Master.currMovie.ClearArtPath.Substring(1, Master.currMovie.ClearArtPath.Length - 1))
                    Else
                        MovieClearArt.FromFile(Master.currMovie.ClearArtPath)
                    End If
                    If Not IsNothing(MovieClearArt.Image) Then
                        .pbMovieClearArt.Image = MovieClearArt.Image
                        .pbMovieClearArt.Tag = MovieClearArt

                        .lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearArt.Image.Width, .pbMovieClearArt.Image.Height)
                        .lblMovieClearArtSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.ClearLogoPath) AndAlso Master.currMovie.ClearLogoPath.Substring(0, 1) = ":" Then
                        MovieClearLogo.FromWeb(Master.currMovie.ClearLogoPath.Substring(1, Master.currMovie.ClearLogoPath.Length - 1))
                    Else
                        MovieClearLogo.FromFile(Master.currMovie.ClearLogoPath)
                    End If
                    If Not IsNothing(MovieClearLogo.Image) Then
                        .pbMovieClearLogo.Image = MovieClearLogo.Image
                        .pbMovieClearLogo.Tag = MovieClearLogo

                        .lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearLogo.Image.Width, .pbMovieClearLogo.Image.Height)
                        .lblMovieClearLogoSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.DiscArtPath) AndAlso Master.currMovie.DiscArtPath.Substring(0, 1) = ":" Then
                        MovieDiscArt.FromWeb(Master.currMovie.DiscArtPath.Substring(1, Master.currMovie.DiscArtPath.Length - 1))
                    Else
                        MovieDiscArt.FromFile(Master.currMovie.DiscArtPath)
                    End If
                    If Not IsNothing(MovieDiscArt.Image) Then
                        .pbMovieDiscArt.Image = MovieDiscArt.Image
                        .pbMovieDiscArt.Tag = MovieDiscArt

                        .lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieDiscArt.Image.Width, .pbMovieDiscArt.Image.Height)
                        .lblMovieDiscArtSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.FanartPath) AndAlso Master.currMovie.FanartPath.Substring(0, 1) = ":" Then
                        MovieFanart.FromWeb(Master.currMovie.FanartPath.Substring(1, Master.currMovie.FanartPath.Length - 1))
                    Else
                        MovieFanart.FromFile(Master.currMovie.FanartPath)
                    End If
                    If Not IsNothing(MovieFanart.Image) Then
                        .pbMovieFanart.Image = MovieFanart.Image
                        .pbMovieFanart.Tag = MovieFanart

                        .lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieFanart.Image.Width, .pbMovieFanart.Image.Height)
                        .lblMovieFanartSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.LandscapePath) AndAlso Master.currMovie.LandscapePath.Substring(0, 1) = ":" Then
                        MovieLandscape.FromWeb(Master.currMovie.LandscapePath.Substring(1, Master.currMovie.LandscapePath.Length - 1))
                    Else
                        MovieLandscape.FromFile(Master.currMovie.LandscapePath)
                    End If
                    If Not IsNothing(MovieLandscape.Image) Then
                        .pbMovieLandscape.Image = MovieLandscape.Image
                        .pbMovieLandscape.Tag = MovieLandscape

                        .lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieLandscape.Image.Width, .pbMovieLandscape.Image.Height)
                        .lblMovieLandscapeSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovie.PosterPath) AndAlso Master.currMovie.PosterPath.Substring(0, 1) = ":" Then
                        MoviePoster.FromWeb(Master.currMovie.PosterPath.Substring(1, Master.currMovie.PosterPath.Length - 1))
                    Else
                        MoviePoster.FromFile(Master.currMovie.PosterPath)
                    End If
                    If Not IsNothing(MoviePoster.Image) Then
                        .pbMoviePoster.Image = MoviePoster.Image
                        .pbMoviePoster.Tag = MoviePoster

                        .lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMoviePoster.Image.Width, .pbMoviePoster.Image.Height)
                        .lblMoviePosterSize.Visible = True
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Banner) Then
                        .btnSetMovieBannerScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearArt) Then
                        .btnSetMovieClearArtScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.ClearLogo) Then
                        .btnSetMovieClearLogoScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.DiscArt) Then
                        .btnSetMovieDiscArtScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Fanart) Then
                        .btnSetMovieFanartScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Landscape) Then
                        .btnSetMovieLandscapeScrape.Enabled = False
                    End If

                    If Not ModulesManager.Instance.QueryPostScraperCapabilities(Enums.ScraperCapabilities.Poster) Then
                        .btnSetMoviePosterScrape.Enabled = False
                    End If

                    If Path.GetExtension(Master.currMovie.Filename).ToLower = ".disc" Then
                        Dim DiscStub As New MediaStub.DiscStub
                        DiscStub = MediaStub.LoadDiscStub(Master.currMovie.Filename)
                        .txtMediaStubTitle.Text = DiscStub.Title
                        .txtMediaStubMessage.Text = DiscStub.Message
                    End If
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lbGenre_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles clbGenre.ItemCheck
        If e.Index = 0 Then
            For i As Integer = 1 To clbGenre.Items.Count - 1
                Me.clbGenre.SetItemChecked(i, False)
            Next
        Else
            Me.clbGenre.SetItemChecked(0, False)
        End If
    End Sub

    Private Sub LoadGenres()
        '//
        ' Read all the genres from the xml and load into the list
        '\\

        Me.clbGenre.Items.Add(Master.eLang.None)

        Me.clbGenre.Items.AddRange(APIXML.GetGenreList)
    End Sub

    Private Sub LoadRatings()
        '//
        ' Read all the ratings from the xml and load into the list
        '\\

        Me.lbMPAA.Items.Add(Master.eLang.None)

        Me.lbMPAA.Items.AddRange(APIXML.GetRatingList)
    End Sub

    Private Sub LoadEThumbs()
        Dim ET_tPath As String = String.Empty
        Dim ET_lFI As New List(Of String)
        Dim ET_i As Integer = 0
        Dim ET_max As Integer = 30 'limited the number of images to avoid a memory error

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.MovieUseFrodo OrElse Master.eSettings.MovieUseEden Then
            If Master.eSettings.MovieExtrathumbsFrodo OrElse Master.eSettings.MovieExtrathumbsEden Then
                If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                    ET_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                    ET_tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
                Else
                    ET_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                End If
            End If
        End If

        Try
            If Directory.Exists(ET_tPath) Then
                Try
                    ET_lFI.AddRange(Directory.GetFiles(ET_tPath, "thumb*.jpg"))
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try

                ' load local Extrathumbs
                If ET_lFI.Count > 0 Then
                    For Each thumb As String In ET_lFI
                        Dim ETImage As New Images
                        If Me.bwEThumbs.CancellationPending Then Return
                        If Not Me.etDeleteList.Contains(thumb) Then
                            ETImage.FromFile(thumb)
                            EThumbsList.Add(New ExtraImages With {.Image = ETImage, .Name = Path.GetFileName(thumb), .Index = ET_i, .Path = thumb})
                            ET_i += 1
                            If ET_i >= ET_max Then Exit For
                        End If
                    Next
                End If
            End If

            ' load scraped Extrathumbs
            If Not Master.currMovie.etList Is Nothing Then
                If Not ET_i >= ET_max Then
                    For Each thumb As String In Master.currMovie.etList
                        Dim ETImage As New Images
                        If Not String.IsNullOrEmpty(thumb) Then
                            ETImage.FromWeb(thumb.Substring(1, thumb.Length - 1))
                        End If
                        If Not IsNothing(ETImage.Image) Then
                            EThumbsList.Add(New ExtraImages With {.Image = ETImage, .Name = Path.GetFileName(thumb), .Index = ET_i, .Path = thumb})
                            ET_i += 1
                            If ET_i >= ET_max Then Exit For
                        End If
                    Next
                End If
            End If

            'load MovieExtractor Extrathumbs
            If EThumbsExtractor.Count > 0 Then
                If Not ET_i >= ET_max Then
                    For Each thumb As String In EThumbsExtractor
                        Dim ETImage As New Images
                        If Me.bwEThumbs.CancellationPending Then Return
                        If Not Me.etDeleteList.Contains(thumb) Then
                            ETImage.FromFile(thumb)
                            EThumbsList.Add(New ExtraImages With {.Image = ETImage, .Name = Path.GetFileName(thumb), .Index = ET_i, .Path = thumb})
                            ET_i += 1
                            If ET_i >= ET_max Then Exit For
                        End If
                    Next
                End If
            End If

            If ET_i >= ET_max AndAlso EThumbsWarning Then
                MsgBox(String.Format(Master.eLang.GetString(1120, "To prevent a memory overflow will not display more than {0} Extrathumbs."), ET_max), MsgBoxStyle.OkOnly, Master.eLang.GetString(356, "Warning"))
                EThumbsWarning = False 'show warning only one time
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        ET_lFI = Nothing
    End Sub

    Private Sub LoadEFanarts()
        Dim EF_tPath As String = String.Empty
        Dim EF_lFI As New List(Of String)
        Dim EF_i As Integer = 0
        Dim EF_max As Integer = 30 'limited the number of images to avoid a memory error

        '*************** XBMC Frodo & Eden settings ***************
        If Master.eSettings.MovieUseFrodo OrElse Master.eSettings.MovieUseEden Then
            If Master.eSettings.MovieExtrafanartsFrodo OrElse Master.eSettings.MovieExtrafanartsEden Then
                If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                    EF_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                    EF_tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
                Else
                    EF_tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                End If
            End If
        End If

        Try
            If Directory.Exists(EF_tPath) Then
                Try
                    EF_lFI.AddRange(Directory.GetFiles(EF_tPath, "*.jpg"))
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try

                ' load local Extrafanarts
                If EF_lFI.Count > 0 Then
                    For Each fanart As String In EF_lFI
                        Dim EFImage As New Images
                        If Me.bwEFanarts.CancellationPending Then Return
                        If Not Me.efDeleteList.Contains(fanart) Then
                            EFImage.FromFile(fanart)
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                            EF_i += 1
                            If EF_i >= EF_max Then Exit For
                        End If
                    Next
                End If
            End If

            ' load scraped Extrafanarts
            If Not Master.currMovie.efList Is Nothing Then
                If Not EF_i >= EF_max Then
                    For Each fanart As String In Master.currMovie.efList
                        Dim EFImage As New Images
                        If Not String.IsNullOrEmpty(fanart) Then
                            EFImage.FromWeb(fanart.Substring(1, fanart.Length - 1))
                        End If
                        If Not IsNothing(EFImage.Image) Then
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(fanart), .Index = EF_i, .Path = fanart})
                            EF_i += 1
                            If EF_i >= EF_max Then Exit For
                        End If
                    Next
                End If
            End If

            'load MovieExtractor Extrafanarts
            If EFanartsExtractor.Count > 0 Then
                If Not EF_i >= EF_max Then
                    For Each thumb As String In EFanartsExtractor
                        Dim EFImage As New Images
                        If Me.bwEThumbs.CancellationPending Then Return
                        If Not Me.etDeleteList.Contains(thumb) Then
                            EFImage.FromFile(thumb)
                            EFanartsList.Add(New ExtraImages With {.Image = EFImage, .Name = Path.GetFileName(thumb), .Index = EF_i, .Path = thumb})
                            EF_i += 1
                            If EF_i >= EF_max Then Exit For
                        End If
                    Next
                End If
            End If

            If EF_i >= EF_max AndAlso EFanartsWarning Then
                MsgBox(String.Format(Master.eLang.GetString(1119, "To prevent a memory overflow will not display more than {0} Extrafanarts."), EF_max), MsgBoxStyle.OkOnly, Master.eLang.GetString(356, "Warning"))
                EFanartsWarning = False 'show warning only one time
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        EF_lFI = Nothing
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

    Private Sub lvEThumbs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.DeleteEThumbs()
    End Sub

    Private Sub lvEFanart_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.DeleteEFanarts()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveMovieToDB(Master.currMovie, False, False, True)

            Me.CleanUp()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbMovieBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieBanner.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieBanner = tImage
            Me.pbMovieBanner.Image = MovieBanner.Image
            Me.pbMovieBanner.Tag = MovieBanner
            Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
            Me.lblMovieBannerSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieBanner_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieBanner.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMovieClearArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieClearArt.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieClearArt = tImage
            Me.pbMovieClearArt.Image = MovieClearArt.Image
            Me.pbMovieClearArt.Tag = MovieClearArt
            Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
            Me.lblMovieClearArtSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieClearArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieClearArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMovieClearLogo_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieClearLogo.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieClearLogo = tImage
            Me.pbMovieClearLogo.Image = MovieClearLogo.Image
            Me.pbMovieClearLogo.Tag = MovieClearLogo
            Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
            Me.lblMovieClearLogoSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieClearLogo_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieClearLogo.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMovieDiscArt_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieDiscArt.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieDiscArt = tImage
            Me.pbMovieDiscArt.Image = MovieDiscArt.Image
            Me.pbMovieDiscArt.Tag = MovieDiscArt
            Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
            Me.lblMovieDiscArtSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieDiscArt_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieDiscArt.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMovieFanart_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieFanart.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieFanart = tImage
            Me.pbMovieFanart.Image = MovieFanart.Image
            Me.pbMovieFanart.Tag = MovieFanart
            Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
            Me.lblMovieFanartSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieFanart_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieFanart.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMovieLandscape_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieLandscape.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MovieLandscape = tImage
            Me.pbMovieLandscape.Image = MovieLandscape.Image
            Me.pbMovieLandscape.Tag = MovieLandscape
            Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
            Me.lblMovieLandscapeSize.Visible = True
        End If
    End Sub

    Private Sub pbMovieLandscape_DragEnter(sender As Object, e As DragEventArgs) Handles pbMovieLandscape.DragEnter
        If FileUtils.DragAndDrop.CheckDroppedImage(e) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub pbMoviePoster_DragDrop(sender As Object, e As DragEventArgs) Handles pbMoviePoster.DragDrop
        Dim tImage As Images = FileUtils.DragAndDrop.GetDoppedImage(e)
        If Not IsNothing(tImage.Image) Then
            MoviePoster = tImage
            Me.pbMoviePoster.Image = MoviePoster.Image
            Me.pbMoviePoster.Tag = MoviePoster
            Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
            Me.lblMoviePosterSize.Visible = True
        End If
    End Sub

    Private Sub pbMoviePoster_DragEnter(sender As Object, e As DragEventArgs) Handles pbMoviePoster.DragEnter
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
                Me.pbStar1.Tag = 1
                Me.BuildStars(1)
            Else
                Me.pbStar1.Tag = 2
                Me.BuildStars(2)
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
                Me.pbStar2.Tag = 3
                Me.BuildStars(3)
            Else
                Me.pbStar2.Tag = 4
                Me.BuildStars(4)
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
                Me.pbStar3.Tag = 5
                Me.BuildStars(5)
            Else
                Me.pbStar3.Tag = 6
                Me.BuildStars(6)
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
                Me.pbStar4.Tag = 7
                Me.BuildStars(7)
            Else
                Me.pbStar4.Tag = 8
                Me.BuildStars(8)
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
                Me.pbStar5.Tag = 9
                Me.BuildStars(9)
            Else
                Me.pbStar5.Tag = 10
                Me.BuildStars(10)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RefreshEThumbs()
        Try
            If Me.bwEThumbs.IsBusy Then Me.bwEThumbs.CancelAsync()
            While Me.bwEThumbs.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            Me.iETTop = 1 ' set first image top position back to 1
            Me.EThumbsList.Clear()
            While Me.pnlMovieEThumbsBG.Controls.Count > 0
                Me.pnlMovieEThumbsBG.Controls(0).Dispose()
            End While

            Me.bwEThumbs.WorkerSupportsCancellation = True
            Me.bwEThumbs.RunWorkerAsync()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RefreshEFanarts()
        Try
            If Me.bwEFanarts.IsBusy Then Me.bwEFanarts.CancelAsync()
            While Me.bwEFanarts.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While

            Me.iEFTop = 1 ' set first image top position back to 1
            Me.EFanartsList.Clear()
            While Me.pnlMovieEFanartsBG.Controls.Count > 0
                Me.pnlMovieEFanartsBG.Controls(0).Dispose()
            End While

            Me.bwEFanarts.WorkerSupportsCancellation = True
            Me.bwEFanarts.RunWorkerAsync()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RenumberEThumbs()
        For i As Integer = 0 To EThumbsList.Count - 1
            EThumbsList.Item(i).Index = i + 1
        Next
    End Sub

    Private Sub SaveEThumbsList()
        Dim tPath As String = String.Empty
        Try
            '*************** XBMC Frodo & Eden settings ***************
            If Master.eSettings.MovieUseFrodo OrElse Master.eSettings.MovieUseEden Then
                If Master.eSettings.MovieExtrathumbsFrodo OrElse Master.eSettings.MovieExtrathumbsEden Then
                    If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                    ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrathumbs")
                    Else
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrathumbs")
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.currMovie.ClearEThumbs AndAlso Not hasClearedET Then
                    FileUtils.Delete.DeleteDirectory(tPath)
                    hasClearedET = True
                Else
                    'first delete the ones from the delete list
                    For Each del As String In etDeleteList
                        File.Delete(Path.Combine(tPath, del))
                    Next

                    'now name the rest something arbitrary so we don't get any conflicts
                    For Each lItem As ExtraImages In EThumbsList
                        If Not lItem.Path.Substring(0, 1) = ":" Then
                            FileSystem.Rename(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                        End If
                    Next

                    'now rename them properly
                    For Each lItem As ExtraImages In EThumbsList
                        Dim etPath As String = lItem.Image.SaveAsMovieExtrathumb(Master.currMovie)
                        If lItem.Index = 0 Then
                            Master.currMovie.EThumbsPath = etPath
                        End If
                    Next

                    'now remove the temp images
                    Dim tList As New List(Of String)

                    If Directory.Exists(tPath) Then
                        tList.AddRange(Directory.GetFiles(tPath, "tempthumb*.jpg"))
                        For Each tFile As String In tList
                            File.Delete(Path.Combine(tPath, tFile))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SaveEFanartsList()
        Dim tPath As String = String.Empty
        Try
            '*************** XBMC Frodo & Eden settings ***************
            If Master.eSettings.MovieUseFrodo OrElse Master.eSettings.MovieUseEden Then
                If Master.eSettings.MovieExtrafanartsFrodo OrElse Master.eSettings.MovieExtrafanartsEden Then
                    If FileUtils.Common.isVideoTS(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                    ElseIf FileUtils.Common.isBDRip(Master.currMovie.Filename) Then
                        tPath = Path.Combine(Directory.GetParent(Directory.GetParent(Master.currMovie.Filename).FullName).FullName, "extrafanart")
                    Else
                        tPath = Path.Combine(Directory.GetParent(Master.currMovie.Filename).FullName, "extrafanart")
                    End If
                End If
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.currMovie.ClearEFanarts AndAlso Not hasClearedEF Then
                    FileUtils.Delete.DeleteDirectory(tPath)
                    hasClearedEF = True
                Else
                    'first delete the ones from the delete list
                    For Each del As String In efDeleteList
                        File.Delete(Path.Combine(tPath, del))
                    Next

                    'now name the rest something arbitrary so we don't get any conflicts
                    For Each lItem As ExtraImages In EFanartsList
                        If Not lItem.Path.Substring(0, 1) = ":" Then
                            FileSystem.Rename(lItem.Path, Path.Combine(Directory.GetParent(lItem.Path).FullName, String.Concat("temp", lItem.Name)))
                        End If
                    Next

                    'now rename them properly
                    For Each lItem As ExtraImages In EFanartsList
                        Dim efPath As String = lItem.Image.SaveAsMovieExtrafanart(Master.currMovie, lItem.Name)
                        If lItem.Index = 0 Then
                            Master.currMovie.EFanartsPath = efPath
                        End If
                    Next

                    'now remove the temp images
                    Dim tList As New List(Of String)

                    If Directory.Exists(tPath) Then
                        tList.AddRange(Directory.GetFiles(tPath, "temp*.jpg"))
                        For Each tFile As String In tList
                            File.Delete(Path.Combine(tPath, tFile))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SelectMPAA()
        If Not String.IsNullOrEmpty(Master.currMovie.Movie.MPAA) Then
            Try
                If Master.eSettings.MovieScraperCertForMPAA AndAlso Not Master.eSettings.MovieScraperCertLang = "USA" AndAlso APIXML.RatingXML.movies.FindAll(Function(f) f.country = Master.eSettings.MovieScraperCertLang.ToLower).Count > 0 Then
                    If Master.eSettings.MovieScraperOnlyValueForMPAA Then
                        Dim sItem As String = String.Empty
                        For i As Integer = 0 To Me.lbMPAA.Items.Count - 1
                            sItem = Me.lbMPAA.Items(i).ToString
                            If sItem.Contains(":") AndAlso sItem.Split(Convert.ToChar(":"))(1) = Master.currMovie.Movie.MPAA Then
                                Me.lbMPAA.SelectedIndex = i
                                Me.lbMPAA.TopIndex = i
                                Exit For
                            End If
                        Next
                    Else
                        Dim l As Integer = Me.lbMPAA.FindString(Strings.Trim(Master.currMovie.Movie.MPAA))
                        Me.lbMPAA.SelectedIndex = l
                        Me.lbMPAA.TopIndex = l
                    End If

                    If Me.lbMPAA.SelectedItems.Count = 0 Then
                        Me.lbMPAA.SelectedIndex = 0
                        Me.lbMPAA.TopIndex = 0
                    End If

                    txtMPAADesc.Enabled = False
                ElseIf Me.lbMPAA.Items.Count >= 6 Then
                    Dim strMPAA As String = Master.currMovie.Movie.MPAA
                    If strMPAA.ToLower.StartsWith("rated g") Then
                        Me.lbMPAA.SelectedIndex = 1
                    ElseIf strMPAA.ToLower.StartsWith("rated pg-13") Then
                        Me.lbMPAA.SelectedIndex = 3
                    ElseIf strMPAA.ToLower.StartsWith("rated pg") Then
                        Me.lbMPAA.SelectedIndex = 2
                    ElseIf strMPAA.ToLower.StartsWith("rated r") Then
                        Me.lbMPAA.SelectedIndex = 4
                    ElseIf strMPAA.ToLower.StartsWith("rated nc-17") Then
                        Me.lbMPAA.SelectedIndex = 5
                    Else
                        Me.lbMPAA.SelectedIndex = 0
                    End If

                    If Me.lbMPAA.SelectedIndex > 0 AndAlso Not String.IsNullOrEmpty(strMPAA) Then
                        Dim strMPAADesc As String = strMPAA
                        strMPAADesc = Strings.Replace(strMPAADesc, "rated g", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated pg-13", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated pg", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated r", String.Empty, 1, -1, CompareMethod.Text).Trim
                        If Not String.IsNullOrEmpty(strMPAADesc) Then strMPAADesc = Strings.Replace(strMPAADesc, "rated nc-17", String.Empty, 1, -1, CompareMethod.Text).Trim
                        txtMPAADesc.Text = strMPAADesc
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Else
            Me.lbMPAA.SelectedIndex = 0
        End If
    End Sub

    Private Sub SetInfo()
        Try
            With Me

                Me.OK_Button.Enabled = False
                Me.Cancel_Button.Enabled = False
                Me.btnRescrape.Enabled = False
                Me.btnChangeMovie.Enabled = False

                Master.currMovie.IsMark = Me.chkMark.Checked

                If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                    If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(.mtxtYear.Text.Trim) Then
                        Master.currMovie.ListTitle = String.Format("{0} ({1})", StringUtils.FilterTokens(.txtTitle.Text.Trim), .mtxtYear.Text.Trim)
                    Else
                        Master.currMovie.ListTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                    End If
                    Master.currMovie.Movie.Title = .txtTitle.Text.Trim
                End If

                If Not String.IsNullOrEmpty(.txtOriginalTitle.Text) Then
                    Master.currMovie.Movie.OriginalTitle = .txtOriginalTitle.Text.Trim
                Else
                    Master.currMovie.Movie.OriginalTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                End If

                If Not String.IsNullOrEmpty(.txtSortTitle.Text) Then
                    Master.currMovie.Movie.SortTitle = .txtSortTitle.Text.Trim
                Else
                    Master.currMovie.Movie.SortTitle = StringUtils.FilterTokens(.txtTitle.Text.Trim)
                End If

                Master.currMovie.Movie.Tagline = .txtTagline.Text.Trim
                Master.currMovie.Movie.Year = .mtxtYear.Text.Trim
                Master.currMovie.Movie.Votes = .txtVotes.Text.Trim
                Master.currMovie.Movie.Outline = .txtOutline.Text.Trim
                Master.currMovie.Movie.Plot = .txtPlot.Text.Trim
                Master.currMovie.Movie.Top250 = .txtTop250.Text.Trim
                Master.currMovie.Movie.Country = .txtCountry.Text.Trim
                Master.currMovie.Movie.Director = .txtDirector.Text.Trim

                Master.currMovie.Movie.Certification = .txtCerts.Text.Trim

                Master.currMovie.FileSource = .txtFileSource.Text.Trim

                If .lbMPAA.SelectedIndices.Count > 0 AndAlso Not .lbMPAA.SelectedIndex <= 0 Then
                    Master.currMovie.Movie.MPAA = String.Concat(If(Master.eSettings.MovieScraperCertForMPAA AndAlso Master.eSettings.MovieScraperOnlyValueForMPAA AndAlso .lbMPAA.SelectedItem.ToString.Contains(":"), .lbMPAA.SelectedItem.ToString.Split(Convert.ToChar(":"))(1), .lbMPAA.SelectedItem.ToString), " ", .txtMPAADesc.Text).Trim
                Else
                    If Master.eSettings.MovieScraperCertForMPAA AndAlso (Not Master.eSettings.MovieScraperCertLang = "USA" OrElse (Master.eSettings.MovieScraperCertLang = "USA" AndAlso .lbMPAA.SelectedIndex = 0)) Then
                        Dim lCert() As String = .txtCerts.Text.Trim.Split(Convert.ToChar("/"))
                        Dim fCert = From eCert In lCert Where Regex.IsMatch(eCert, String.Concat(Regex.Escape(Master.eSettings.MovieScraperCertLang), "\:(.*?)"))
                        If fCert.Count > 0 Then
                            Master.currMovie.Movie.MPAA = If(Master.eSettings.MovieScraperCertLang = "USA", StringUtils.USACertToMPAA(fCert(0).ToString.Trim), If(Master.eSettings.MovieScraperOnlyValueForMPAA, fCert(0).ToString.Trim.Split(Convert.ToChar(":"))(1), fCert(0).ToString.Trim))
                        Else
                            Master.currMovie.Movie.MPAA = String.Empty
                        End If
                    Else
                        Master.currMovie.Movie.MPAA = String.Empty
                    End If
                End If

                If Not .tmpRating.Trim = String.Empty AndAlso .tmpRating.Trim <> "0" Then
                    Master.currMovie.Movie.Rating = .tmpRating
                Else
                    Master.currMovie.Movie.Rating = String.Empty
                End If

                Master.currMovie.Movie.Runtime = .txtRuntime.Text.Trim
                Master.currMovie.Movie.ReleaseDate = .txtReleaseDate.Text.Trim
                Master.currMovie.Movie.OldCredits = .txtCredits.Text.Trim
                Master.currMovie.Movie.Studio = .txtStudio.Text.Trim

                If Master.eSettings.MovieXBMCTrailerFormat Then
                    Master.currMovie.Movie.Trailer = Replace(.txtTrailer.Text.Trim, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                    Master.currMovie.Movie.Trailer = Replace(Master.currMovie.Movie.Trailer, "http://www.youtube.com/watch?hd=1&v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                Else
                    Master.currMovie.Movie.Trailer = .txtTrailer.Text.Trim
                End If

                'cocotus, 2013/02 Playcount/Watched state support added
                'if watched-checkbox is checked -> save Playcount=1 in nfo
                If chkWatched.Checked Then
                    'Only set to 1 if field was empty before (otherwise it would overwrite Playcount everytime which is not desirable)
                    If String.IsNullOrEmpty(Master.currMovie.Movie.PlayCount) Or Master.currMovie.Movie.PlayCount = "0" Then
                        Master.currMovie.Movie.PlayCount = "1"
                    End If

                    If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                        For Each a In FileUtils.GetFilenameList.Movie(Master.currMovie.Filename, Master.currMovie.isSingle, Enums.MovieModType.WatchedFile)
                            If Not File.Exists(a) Then
                                Dim fs As FileStream = File.Create(a)
                                fs.Close()
                            End If
                        Next
                    End If
                Else
                    'Unchecked Watched State -> Set Playcount back to 0, but only if it was filled before (check could save time)
                    If IsNumeric(Master.currMovie.Movie.PlayCount) AndAlso CInt(Master.currMovie.Movie.PlayCount) > 0 Then
                        Master.currMovie.Movie.PlayCount = ""
                    End If

                    If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJWatchedFile Then
                        For Each a In FileUtils.GetFilenameList.Movie(Master.currMovie.Filename, Master.currMovie.isSingle, Enums.MovieModType.WatchedFile)
                            If File.Exists(a) Then
                                File.Delete(a)
                            End If
                        Next
                    End If
                End If
                'cocotus End

                If .clbGenre.CheckedItems.Count > 0 Then

                    If .clbGenre.CheckedIndices.Contains(0) Then
                        Master.currMovie.Movie.Genre = String.Empty
                    Else
                        Dim strGenre As String = String.Empty
                        Dim isFirst As Boolean = True
                        Dim iChecked = From iCheck In .clbGenre.CheckedItems
                        strGenre = Strings.Join(iChecked.ToArray, " / ")
                        Master.currMovie.Movie.Genre = strGenre.Trim
                    End If
                End If

                Master.currMovie.Movie.Actors.Clear()

                If .lvActors.Items.Count > 0 Then
                    For Each lviActor As ListViewItem In .lvActors.Items
                        Dim addActor As New MediaContainers.Person
                        addActor.Name = lviActor.Text.Trim
                        addActor.Role = lviActor.SubItems(1).Text.Trim
                        addActor.Thumb = lviActor.SubItems(2).Text.Trim

                        Master.currMovie.Movie.Actors.Add(addActor)
                    Next
                End If

                If Master.currMovie.ClearBanner Then
                    .MovieBanner.DeleteMovieBanner(Master.currMovie)
                End If

                If Master.currMovie.ClearClearArt Then
                    .MovieClearArt.DeleteMovieClearArt(Master.currMovie)
                End If

                If Master.currMovie.ClearClearLogo Then
                    .MovieClearLogo.DeleteMovieClearLogo(Master.currMovie)
                End If

                If Master.currMovie.ClearDiscArt Then
                    .MovieDiscArt.DeleteMovieDiscArt(Master.currMovie)
                End If

                If Master.currMovie.ClearFanart Then
                    .MovieFanart.DeleteMovieFanart(Master.currMovie)
                End If

                If Master.currMovie.ClearLandscape Then
                    .MovieLandscape.DeleteMovieLandscape(Master.currMovie)
                End If

                If Master.currMovie.ClearPoster Then
                    .MoviePoster.DeleteMoviePoster(Master.currMovie)
                End If

                If Master.currMovie.ClearTheme Then
                    .MovieTheme.DeleteMovieTheme(Master.currMovie)
                End If

                If Master.currMovie.ClearTrailer Then
                    .MovieTrailer.DeleteMovieTrailer(Master.currMovie)
                End If

                If Not IsNothing(.MovieBanner.Image) Then
                    Dim fPath As String = .MovieBanner.SaveAsMovieBanner(Master.currMovie)
                    Master.currMovie.BannerPath = fPath
                Else
                    .MovieBanner.DeleteMovieBanner(Master.currMovie)
                    Master.currMovie.BannerPath = String.Empty
                End If

                If Not IsNothing(.MovieClearArt.Image) Then
                    Dim fPath As String = .MovieClearArt.SaveAsMovieClearArt(Master.currMovie)
                    Master.currMovie.ClearArtPath = fPath
                Else
                    .MovieClearArt.DeleteMovieClearArt(Master.currMovie)
                    Master.currMovie.ClearArtPath = String.Empty
                End If

                If Not IsNothing(.MovieClearLogo.Image) Then
                    Dim fPath As String = .MovieClearLogo.SaveAsMovieClearLogo(Master.currMovie)
                    Master.currMovie.ClearLogoPath = fPath
                Else
                    .MovieClearLogo.DeleteMovieClearLogo(Master.currMovie)
                    Master.currMovie.ClearLogoPath = String.Empty
                End If

                If Not IsNothing(.MovieDiscArt.Image) Then
                    Dim fPath As String = .MovieDiscArt.SaveAsMovieDiscArt(Master.currMovie)
                    Master.currMovie.DiscArtPath = fPath
                Else
                    .MovieDiscArt.DeleteMovieDiscArt(Master.currMovie)
                    Master.currMovie.DiscArtPath = String.Empty
                End If

                If Not IsNothing(.MovieFanart.Image) Then
                    Dim fPath As String = .MovieFanart.SaveAsMovieFanart(Master.currMovie)
                    Master.currMovie.FanartPath = fPath
                Else
                    .MovieFanart.DeleteMovieFanart(Master.currMovie)
                    Master.currMovie.FanartPath = String.Empty
                End If

                If Not IsNothing(.MovieLandscape.Image) Then
                    Dim fPath As String = .MovieLandscape.SaveAsMovieLandscape(Master.currMovie)
                    Master.currMovie.LandscapePath = fPath
                Else
                    .MovieLandscape.DeleteMovieLandscape(Master.currMovie)
                    Master.currMovie.LandscapePath = String.Empty
                End If

                If Not IsNothing(.MoviePoster.Image) Then
                    Dim pPath As String = .MoviePoster.SaveAsMoviePoster(Master.currMovie)
                    Master.currMovie.PosterPath = pPath
                Else
                    .MoviePoster.DeleteMoviePoster(Master.currMovie)
                    Master.currMovie.PosterPath = String.Empty
                End If

                If Master.GlobalScrapeMod.ActorThumbs AndAlso (Master.eSettings.MovieActorThumbsFrodo OrElse Master.eSettings.MovieActorThumbsEden) Then
                    For Each act As MediaContainers.Person In Master.currMovie.Movie.Actors
                        Dim img As New Images
                        img.FromWeb(act.Thumb)
                        If Not IsNothing(img.Image) Then
                            img.SaveAsMovieActorThumb(act, Directory.GetParent(Master.currMovie.Filename).FullName, Master.currMovie)
                        End If
                    Next
                End If

                If Not IsNothing(.MovieTheme) Then
                    Dim tPath As String = .MovieTheme.SaveAsMovieTheme(Master.currMovie)
                    Master.currMovie.ThemePath = tPath
                Else
                    .MovieTheme.DeleteMovieTheme(Master.currMovie)
                    Master.currMovie.ThemePath = String.Empty
                End If

                If Not IsNothing(.MovieTrailer) Then
                    Dim tPath As String = .MovieTrailer.SaveAsMovieTrailer(Master.currMovie)
                    Master.currMovie.TrailerPath = tPath
                Else
                    .MovieTrailer.DeleteMovieTrailer(Master.currMovie)
                    Master.currMovie.TrailerPath = String.Empty
                End If

                If Path.GetExtension(Master.currMovie.Filename) = ".disc" Then
                    Dim StubFile As String = Master.currMovie.Filename
                    Dim Title As String = Me.txtMediaStubTitle.Text
                    Dim Message As String = Me.txtMediaStubMessage.Text
                    MediaStub.SaveDiscStub(StubFile, Title, Message)
                End If

                If Not Master.eSettings.MovieNoSaveImagesToNfo AndAlso pResults.Posters.Count > 0 Then Master.currMovie.Movie.Thumb = pResults.Posters
                If Not Master.eSettings.MovieNoSaveImagesToNfo AndAlso fResults.Fanart.Thumb.Count > 0 Then Master.currMovie.Movie.Fanart = pResults.Fanart

                .SaveEThumbsList()

                .SaveEFanartsList()

            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Master.currMovie.Movie.Title
        Dim mPathPieces() As String = Master.currMovie.Filename.Split(Path.DirectorySeparatorChar)
        Dim mShortPath As String = Master.currMovie.Filename
        If Not String.IsNullOrEmpty(mShortPath) AndAlso FileUtils.Common.isVideoTS(mShortPath) Then
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 3), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        ElseIf Not String.IsNullOrEmpty(mShortPath) AndAlso FileUtils.Common.isBDRip(mShortPath) Then
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 4), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 3), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        Else
            mShortPath = String.Concat(Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 2), Path.DirectorySeparatorChar, mPathPieces(mPathPieces.Count - 1))
        End If
        Dim sTitle As String = String.Concat(Master.eLang.GetString(25, "Edit Movie"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)), If(String.IsNullOrEmpty(mShortPath), String.Empty, String.Concat(" | ", mShortPath)))
        Me.Text = sTitle
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.btnChangeMovie.Text = Master.eLang.GetString(32, "Change Movie")
        Me.btnManual.Text = Master.eLang.GetString(230, "Manual Edit")
        Me.btnMovieEFanartsSetAsFanart.Text = Me.btnMovieEThumbsSetAsFanart.Text
        Me.btnMovieEFanartsTransfer.Text = Me.btnMovieEThumbsTransfer.Text
        Me.btnMovieEThumbsSetAsFanart.Text = Master.eLang.GetString(255, "Set As Fanart")
        Me.btnMovieEThumbsTransfer.Text = Master.eLang.GetString(254, "Transfer Now")
        Me.btnRemoveMovieBanner.Text = Master.eLang.GetString(1024, "Remove Banner")
        Me.btnRemoveMovieClearArt.Text = Master.eLang.GetString(1087, "Remove ClearArt")
        Me.btnRemoveMovieClearLogo.Text = Master.eLang.GetString(1091, "Remove ClearLogo")
        Me.btnRemoveMovieDiscArt.Text = Master.eLang.GetString(1095, "Remove DiscArt")
        Me.btnRemoveMovieFanart.Text = Master.eLang.GetString(250, "Remove Fanart")
        Me.btnRemoveMovieLandscape.Text = Master.eLang.GetString(1034, "Remove Landscape")
        Me.btnRemoveMoviePoster.Text = Master.eLang.GetString(247, "Remove Poster")
        Me.btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        Me.btnSetMovieBannerDL.Text = Master.eLang.GetString(1023, "Change Banner (Download)")
        Me.btnSetMovieBannerLocal.Text = Master.eLang.GetString(1021, "Change Banner (Local)")
        Me.btnSetMovieBannerScrape.Text = Master.eLang.GetString(1022, "Change Banner (Scrape)")
        Me.btnSetMovieClearArtDL.Text = Master.eLang.GetString(1086, "Change ClearArt (Download)")
        Me.btnSetMovieClearArtLocal.Text = Master.eLang.GetString(1084, "Change ClearArt (Local)")
        Me.btnSetMovieClearArtScrape.Text = Master.eLang.GetString(1085, "Change ClearArt (Scrape)")
        Me.btnSetMovieClearLogoDL.Text = Master.eLang.GetString(1090, "Change ClearLogo (Download)")
        Me.btnSetMovieClearLogoLocal.Text = Master.eLang.GetString(1088, "Change ClearLogo (Local)")
        Me.btnSetMovieClearLogoScrape.Text = Master.eLang.GetString(1089, "Change ClearLogo (Scrape)")
        Me.btnSetMovieDiscArtDL.Text = Master.eLang.GetString(1094, "Change DiscArt (Download)")
        Me.btnSetMovieDiscArtLocal.Text = Master.eLang.GetString(1092, "Change DiscArt (Local)")
        Me.btnSetMovieDiscArtScrape.Text = Master.eLang.GetString(1093, "Change DiscArt (Scrape)")
        Me.btnSetMovieFanartDL.Text = Master.eLang.GetString(266, "Change Fanart (Download)")
        Me.btnSetMovieFanartLocal.Text = Master.eLang.GetString(252, "Change Fanart (Local)")
        Me.btnSetMovieFanartScrape.Text = Master.eLang.GetString(251, "Change Fanart (Scrape)")
        Me.btnSetMovieLandscapeDL.Text = Master.eLang.GetString(1033, "Change Landscape (Download)")
        Me.btnSetMovieLandscapeLocal.Text = Master.eLang.GetString(1031, "Change Landscape (Local)")
        Me.btnSetMovieLandscapeScrape.Text = Master.eLang.GetString(1032, "Change Landscape (Scrape)")
        Me.btnSetMoviePosterDL.Text = Master.eLang.GetString(265, "Change Poster (Download)")
        Me.btnSetMoviePosterLocal.Text = Master.eLang.GetString(249, "Change Poster (Local)")
        Me.btnSetMoviePosterScrape.Text = Master.eLang.GetString(248, "Change Poster (Scrape)")
        Me.chkMark.Text = Master.eLang.GetString(23, "Mark")
        Me.chkWatched.Text = Master.eLang.GetString(981, "Watched")
        Me.colName.Text = Master.eLang.GetString(232, "Name")
        Me.colRole.Text = Master.eLang.GetString(233, "Role")
        Me.colThumb.Text = Master.eLang.GetString(234, "Thumb")
        Me.lbMovieEFanartsQueue.Text = Master.eLang.GetString(974, "You have extratfanarts queued to be transferred to the movie directory.")
        Me.lbMovieEThumbsQueue.Text = Master.eLang.GetString(253, "You have extrathumbs queued to be transferred to the movie directory.")
        Me.lblActors.Text = Master.eLang.GetString(231, "Actors:")
        Me.lblCerts.Text = Master.eLang.GetString(237, "Certification(s):")
        Me.lblCountry.Text = String.Concat(Master.eLang.GetString(301, "Country"), ":")
        Me.lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        Me.lblDirector.Text = Master.eLang.GetString(239, "Director:")
        Me.lblFileSource.Text = Master.eLang.GetString(824, "Video Source:")
        Me.lblGenre.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblLocalTrailer.Text = Master.eLang.GetString(225, "Local Trailer Found")
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
        Me.tpEFanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        Me.tpEThumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpFrameExtraction.Text = Master.eLang.GetString(256, "Frame Extraction")
        Me.tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.tpMetaData.Text = Master.eLang.GetString(866, "Metadata")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")
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
        If mType = Enums.ModuleEventType.MovieFrameExtrator AndAlso Not IsNothing(_params) Then
            If _params(0).ToString = "FanartToSave" Then
                MovieFanart.FromFile(Path.Combine(Master.TempPath, "frame.jpg"))
                If Not IsNothing(MovieFanart.Image) Then
                    Me.pbMovieFanart.Image = MovieFanart.Image
                    Me.pbMovieFanart.Tag = MovieFanart

                    Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
                    Me.lblMovieFanartSize.Visible = True
                End If
            ElseIf _params(0).ToString = "EFanartToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    EFanartsExtractor.Add(fPath)
                    Me.RefreshEFanarts()
                End If
            ElseIf _params(0).ToString = "EThumbToSave" Then
                Dim fPath As String = _params(1).ToString
                If Not String.IsNullOrEmpty(fPath) AndAlso File.Exists(fPath) Then
                    EThumbsExtractor.Add(fPath)
                    Me.RefreshEThumbs()
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

#End Region 'Methods

#Region "Nested Types"

    Friend Class ExtraImages

#Region "Fields"

        Private _image As New Images
        Private _index As Integer
        Private _name As String
        Private _path As String

#End Region 'Fields

#Region "Constructors"

        Friend Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Friend Property Image() As Images
            Get
                Return _image
            End Get
            Set(ByVal value As Images)
                _image = value
            End Set
        End Property

        Friend Property Index() As Integer
            Get
                Return _index
            End Get
            Set(ByVal value As Integer)
                _index = value
            End Set
        End Property

        Friend Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Friend Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _image = Nothing
            _name = String.Empty
            _index = Nothing
            _path = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types
End Class