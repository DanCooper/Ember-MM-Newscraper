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


Public Class dlgEditMovieSet

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwLoadMoviesInSet As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadMoviesFromDB As New System.ComponentModel.BackgroundWorker

    Private CachePath As String = String.Empty
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private MovieBanner As New Images With {.IsEdit = True}
    Private MovieClearArt As New Images With {.IsEdit = True}
    Private MovieClearLogo As New Images With {.IsEdit = True}
    Private MovieDiscArt As New Images With {.IsEdit = True}
    Private MovieFanart As New Images With {.IsEdit = True}
    Private MovieLandscape As New Images With {.IsEdit = True}
    Private MoviePoster As New Images With {.IsEdit = True}
    Private pResults As New Containers.ImgResult

    Private currMovieSet As Structures.DBMovieSet = Master.currMovieSet
    Private sMovieID As String = String.Empty
    Private needsMovieUpdate As Boolean = False

    Private MoviesInDB As New List(Of MovieInSet) 'list of all movies loaded from DB
    Private MoviesToRemove As New List(Of MovieInSet)
    Private MoviesInSet As New List(Of MovieInSet)


#End Region 'Fields

#Region "Methods"

    Private Sub btnGetTMDBColID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTMDBColID.Click
        Dim newColID As String = String.Empty

        If MoviesInSet.Count > 0 Then
            If Not String.IsNullOrEmpty(MoviesInSet.Item(0).DBMovie.Movie.TMDBColID) Then
                newColID = MoviesInSet.Item(0).DBMovie.Movie.TMDBColID
            Else
                newColID = ModulesManager.Instance.GetMovieCollectionID(MoviesInSet.Item(0).DBMovie.Movie.ID)
            End If

            If Not String.IsNullOrEmpty(newColID) Then
                Me.txtCollectionID.Text = newColID
                currMovieSet.MovieSet.ID = newColID
            End If
        End If
    End Sub

    Private Sub btnMovieUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieUp.Click
        Try
            'If Me.lvMoviesInSet.Items.Count > 0 AndAlso Not IsNothing(Me.lvMoviesInSet.SelectedItem) AndAlso Me.lvMoviesInSet.SelectedIndex > 0 Then
            '    Me.needsMovieUpdate = True
            '    Dim iIndex As Integer = Me.lvMoviesInSet.SelectedItems(0)
            '    'Me.currSet.Movies(iIndex).Order = Me.lbMoviesInSet.SelectedIndex - 1
            '    'Me.currSet.Movies(iIndex - 1).Order += 1
            '    'Me.LoadCurrSet()
            '    Me.lvMoviesInSet.SelectedIndex = iIndex - 1
            '    Me.lvMoviesInSet.Focus()
            'End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lbMoviesInSet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then Me.RemoveFromSet()
    End Sub

    Private Sub btnMovieRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieRemove.Click
        Me.RemoveFromSet()
    End Sub

    Private Sub btnMovieReAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieReAdd.Click
        Me.ReAddToSet()
    End Sub

    Private Sub btnLoadMoviesFromDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadMoviesFromDB.Click
        Try
            Me.SetControlsEnabled(False)

            btnCancel.Visible = True
            lblCompiling.Visible = True
            prbCompile.Visible = True
            prbCompile.Style = ProgressBarStyle.Continuous
            lblCanceling.Visible = False
            pnlCancel.Visible = True
            Application.DoEvents()

            Me.bwLoadMoviesFromDB.WorkerSupportsCancellation = True
            Me.bwLoadMoviesFromDB.WorkerReportsProgress = True
            Me.bwLoadMoviesFromDB.RunWorkerAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReAddToSet()
        Try
            Dim lMov As New MovieInSet

            If Me.lvMoviesToRemove.SelectedItems.Count > 0 Then
                Me.SetControlsEnabled(False)
                While Me.lvMoviesToRemove.SelectedItems.Count > 0
                    Me.sMovieID = Me.lvMoviesToRemove.SelectedItems(0).SubItems(0).Text.ToString
                    lMov = Me.MoviesToRemove.Find(AddressOf FindMovie)
                    If Not IsNothing(lMov) Then
                        Me.MoviesInSet.Add(lMov)
                        Me.MoviesToRemove.Remove(lMov)
                    Else
                        Me.lvMoviesToRemove.Items.Remove(Me.lvMoviesToRemove.SelectedItems(0))
                    End If
                End While

                Me.FillMoviesInSet()
                Me.FillMoviesToRemove()
                Me.SetControlsEnabled(True)
                Me.btnMovieUp.Enabled = False
                Me.btnMovieDown.Enabled = False
                Me.btnMovieRemove.Enabled = False
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RemoveFromSet()
        Try
            Dim lMov As New MovieInSet

            If Me.lvMoviesInSet.SelectedItems.Count > 0 Then
                Me.SetControlsEnabled(False)
                While Me.lvMoviesInSet.SelectedItems.Count > 0
                    Me.sMovieID = Me.lvMoviesInSet.SelectedItems(0).SubItems(0).Text.ToString
                    lMov = Me.MoviesInSet.Find(AddressOf FindMovie)
                    If Not IsNothing(lMov) Then
                        Me.MoviesToRemove.Add(lMov)
                        Me.MoviesInSet.Remove(lMov)
                    Else
                        Me.lvMoviesInSet.Items.Remove(Me.lvMoviesInSet.SelectedItems(0))
                    End If
                End While

                Me.FillMoviesInSet()
                Me.FillMoviesToRemove()
                Me.SetControlsEnabled(True)
                Me.btnMovieUp.Enabled = False
                Me.btnMovieDown.Enabled = False
                Me.btnMovieRemove.Enabled = False
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub lvMoviesInDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMoviesInDB.SelectedIndexChanged
        If Me.lvMoviesInDB.SelectedItems.Count > 0 Then
            Me.btnMovieAdd.Enabled = True
        Else
            Me.btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub lvMoviesInDB_DoubleClick(sender As Object, e As EventArgs) Handles lvMoviesInDB.DoubleClick
        If Me.lvMoviesInDB.SelectedItems.Count = 1 Then
            AddMovieToSet()
        End If
    End Sub

    Private Sub lvMoviesInSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMoviesInSet.SelectedIndexChanged
        If Me.lvMoviesInSet.SelectedItems.Count > 0 Then
            Me.btnMovieDown.Enabled = True
            Me.btnMovieRemove.Enabled = True
            Me.btnMovieUp.Enabled = True
        Else
            Me.btnMovieDown.Enabled = False
            Me.btnMovieRemove.Enabled = False
            Me.btnMovieUp.Enabled = False
        End If
    End Sub

    Private Sub lvMoviesToRemove_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMoviesToRemove.SelectedIndexChanged
        If Me.lvMoviesToRemove.SelectedItems.Count > 0 Then
            Me.btnMovieReAdd.Enabled = True
        Else
            Me.btnMovieReAdd.Enabled = False
        End If
    End Sub

    Private Sub btnMovieAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieAdd.Click
        AddMovieToSet()
    End Sub

    Private Sub AddMovieToSet()
        Try
            Dim lMov As New MovieInSet

            If Me.lvMoviesInDB.SelectedItems.Count > 0 Then
                Me.SetControlsEnabled(False)
                While Me.lvMoviesInDB.SelectedItems.Count > 0
                    Me.sMovieID = Me.lvMoviesInDB.SelectedItems(0).SubItems(0).Text.ToString
                    lMov = Me.MoviesInDB.Find(AddressOf FindMovie)
                    If Not IsNothing(lMov) Then
                        Me.MoviesInSet.Add(lMov)
                        Me.MoviesInDB.Remove(lMov)
                    Else
                        Me.lvMoviesInDB.Items.Remove(Me.lvMoviesInDB.SelectedItems(0))
                    End If
                End While

                Me.FillMoviesInSet()
                Me.FillMoviesToRemove()
                Me.SetControlsEnabled(True)
                Me.btnMovieAdd.Enabled = False
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function FindMovie(ByVal lMov As MovieInSet) As Boolean
        If lMov.ID = CType(sMovieID, Long) Then Return True Else  : Return False
    End Function

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

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "Banner.jpg")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                'If Not ModulesManager.Instance.MovieScrapeImages(currSet.Movies.Item(0).DBMovie, Enums.ScraperCapabilities.Banner, aList) Then
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.Banner, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.Banner, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "ClearArt.png")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.ClearArt, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.ClearArt, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "ClearLogo.png")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.ClearLogo, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.ClearLogo, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "DiscArt.png")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.DiscArt, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.DiscArt, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieDiscArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.Fanart, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.Fanart, aList, efList, etList, True) = DialogResult.OK Then
                            pResults = dlgImgS.Results
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If


        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "Landscape.jpg")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.Landscape, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.Landscape, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMovieLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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
            If Me.lvMoviesInSet.Items.Count > 0 Then
                Dim sPath As String = Path.Combine(Master.TempPath, "poster.jpg")

                'Public Function MovieScrapeImages(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Boolean
                If Not ModulesManager.Instance.MovieSetScrapeImages(currMovieSet, Enums.ScraperCapabilities.Poster, aList) Then
                    If aList.Count > 0 Then
                        dlgImgS = New dlgImgSelect()
                        If dlgImgS.ShowDialog(currMovieSet, Enums.MovieImageType.Poster, aList, efList, etList, True) = Windows.Forms.DialogResult.OK Then
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
            Else
                MsgBox("No Movie in MovieSet List", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSetMoviePosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterLocal.Click
        Try
            With ofdImage
                '.InitialDirectory = Directory.GetParent(Master.currMovieSet.Filename).FullName
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

    Private Sub bwLoadMoviesInSet_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMoviesInSet.DoWork
        '//
        ' Start thread to load movieset information from database
        '\\

        Try

            MoviesInSet.Clear()

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim tmpMovie As New Structures.DBMovie
                Dim iProg As Integer = 0
                SQLcommand.CommandText = String.Concat("SELECT MovieID, SetID, SetOrder FROM MoviesSets WHERE SetID = ", Master.currMovieSet.ID, " ORDER BY SetOrder ASC;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            If bwLoadMoviesInSet.CancellationPending Then Return
                            tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                            If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                                Dim tmpSetOrder As Integer = If(Not String.IsNullOrEmpty(SQLreader("SetOrder").ToString), CInt(SQLreader("SetOrder").ToString), Nothing)
                                MoviesInSet.Add(New MovieInSet With {.DBMovie = tmpMovie, .ID = tmpMovie.ID, .ListTitle = String.Concat(StringUtils.FilterTokens(tmpMovie.Movie.Title), If(Not String.IsNullOrEmpty(tmpMovie.Movie.Year), String.Format(" ({0})", tmpMovie.Movie.Year), String.Empty)), .Order = tmpSetOrder})
                            End If
                            Me.bwLoadMoviesInSet.ReportProgress(iProg, tmpMovie.Movie.Title)
                            iProg += 1
                        End While
                    End If
                End Using
            End Using

            'currSet.SetName = currMovieSet.SetName
            'currSet.SetID = currMovieSet.ID
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMoviesInSet_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadMoviesInSet.ProgressChanged
        If e.ProgressPercentage >= 0 Then
            Me.prbCompile.Value = e.ProgressPercentage
            Me.lblFile.Text = e.UserState.ToString
        Else
            Me.prbCompile.Maximum = Convert.ToInt32(e.UserState)
        End If
    End Sub

    Private Sub bwLoadMoviesInSet_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMoviesInSet.RunWorkerCompleted
        '//
        ' Thread finished: fill movie list
        '\\

        If Me.MoviesInSet.Count > 0 Then
            Me.FillMoviesInSet()
        End If

        Me.pnlCancel.Visible = False

        Me.lvMoviesInSet.Enabled = True
        Me.btnMovieUp.Enabled = True
        Me.btnMovieDown.Enabled = True
        Me.btnMovieRemove.Enabled = True
        Me.btnMovieAdd.Enabled = True
    End Sub

    Private Sub bwLoadMoviesFromDB_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMoviesFromDB.DoWork
        '//
        ' Start thread to load movie information from nfo
        '\\

        Try
            MoviesInDB.Clear()

            Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim tmpMovie As New Structures.DBMovie
                Dim iProg As Integer = 0
                SQLcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM movies;")
                Using SQLcount As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    SQLcount.Read()
                    Me.bwLoadMoviesFromDB.ReportProgress(-1, SQLcount("mcount"))
                End Using
                SQLcommand.CommandText = String.Concat("SELECT ID FROM movies ORDER BY ListTitle ASC;")
                Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            If bwLoadMoviesFromDB.CancellationPending Then Return
                            tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(SQLreader("ID")))
                            If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then

                                'cocotus build up tempmovie list which contains all movies belonging to one movieset! this is used to filter right side movies(used in FillList)!
                                'If tmpMovie.Movie.Sets.Count > 0 Then
                                '    lMoviesinSets.Add(String.Concat(StringUtils.FilterTokens(tmpMovie.Movie.Title), If(Not String.IsNullOrEmpty(tmpMovie.Movie.Year), String.Format(" ({0})", tmpMovie.Movie.Year), String.Empty)))
                                'End If

                                MoviesInDB.Add(New MovieInSet With {.DBMovie = tmpMovie, .ListTitle = String.Concat(StringUtils.FilterTokens(tmpMovie.Movie.Title), If(Not String.IsNullOrEmpty(tmpMovie.Movie.Year), String.Format(" ({0})", tmpMovie.Movie.Year), String.Empty)), .ID = tmpMovie.ID})

                                'If tmpMovie.Movie.Sets.Count > 0 Then
                                '    For Each mSet As MediaContainers.Set In tmpMovie.Movie.Sets
                                '        If Not alSets.Contains(mSet.Set) AndAlso Not String.IsNullOrEmpty(mSet.Set) Then
                                '            alSets.Add(mSet.Set)
                                '        End If
                                '    Next
                                'End If

                            End If
                            Me.bwLoadMoviesFromDB.ReportProgress(iProg, tmpMovie.Movie.Title)
                            iProg += 1
                        End While
                    End If
                End Using
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadMoviesFromDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadMoviesFromDB.ProgressChanged
        If e.ProgressPercentage >= 0 Then
            Me.prbCompile.Value = e.ProgressPercentage
            Me.lblFile.Text = e.UserState.ToString
        Else
            Me.prbCompile.Maximum = Convert.ToInt32(e.UserState)
        End If
    End Sub

    Private Sub bwLoadMoviesFromDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMoviesFromDB.RunWorkerCompleted
        '//
        ' Thread finished: fill movie list
        '\\

        Me.FillMoviesInDB()

        Me.pnlCancel.Visible = False

        SetControlsEnabled(True)
        Me.btnMovieAdd.Enabled = False
    End Sub

    Private Sub FillMoviesInDB()
        Try
            Me.lvMoviesInDB.SuspendLayout()

            Me.lvMoviesInDB.Items.Clear()

            Dim lvItem As ListViewItem
            For Each lMovie As MovieInSet In MoviesInDB
                'If Not Me.lbMoviesInSet.Items.Contains(lMov.ListTitle) Then Me.lbMoviesInDB.Items.Add(lMov.ListTitle)
                'If lMoviesinSets.Contains(lMov.ListTitle) = False Then
                'If Not Me.lvMoviesInSet.Items.Contains(lMov.ID) Then Me.lbMoviesInDB.Items.Add(lMov.ID)
                'End If
                If Not Me.MoviesInSet.Contains(lMovie) Then
                    lvItem = Me.lvMoviesInDB.Items.Add(lMovie.ID.ToString)
                    lvItem.SubItems.Add(lMovie.Order.ToString)
                    lvItem.SubItems.Add(lMovie.ListTitle)
                End If
            Next

            Me.lvMoviesInDB.ResumeLayout()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub FillMoviesInSet()
        Try
            Me.lvMoviesInSet.SuspendLayout()

            Me.lvMoviesInSet.Items.Clear()
            Me.MoviesInSet.Sort()

            Dim lvItem As ListViewItem
            Me.lvMoviesInSet.Items.Clear()
            For Each tMovie As MovieInSet In Me.MoviesInSet
                lvItem = Me.lvMoviesInSet.Items.Add(tMovie.ID.ToString)
                lvItem.SubItems.Add(tMovie.Order.ToString)
                lvItem.SubItems.Add(tMovie.ListTitle)
            Next

            Me.lvMoviesInSet.ResumeLayout()
            Me.btnMovieUp.Enabled = False
            Me.btnMovieDown.Enabled = False
            Me.btnMovieRemove.Enabled = False
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub FillMoviesToRemove()
        Try
            Me.lvMoviesToRemove.SuspendLayout()

            Me.lvMoviesToRemove.Items.Clear()
            Me.MoviesToRemove.Sort()

            Dim lvItem As ListViewItem
            Me.lvMoviesToRemove.Items.Clear()
            For Each tMovie As MovieInSet In Me.MoviesToRemove
                lvItem = Me.lvMoviesToRemove.Items.Add(tMovie.ID.ToString)
                lvItem.SubItems.Add(tMovie.Order.ToString)
                lvItem.SubItems.Add(tMovie.ListTitle)
            Next

            Me.lvMoviesToRemove.ResumeLayout()
            Me.btnMovieUp.Enabled = False
            Me.btnMovieDown.Enabled = False
            Me.btnMovieRemove.Enabled = False
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Master.currMovieSet = Master.DB.LoadMovieSetFromDB(Master.currMovieSet.ID)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
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

        Me.MovieFanart.Dispose()
        Me.MovieFanart = Nothing

        Me.MovieLandscape.Dispose()
        Me.MovieLandscape = Nothing

        Me.MoviePoster.Dispose()
        Me.MoviePoster = Nothing
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

            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            Me.FillInfo()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()

        ' Show Cancel Panel
        btnCancel.Visible = True
        lblCompiling.Visible = True
        prbCompile.Visible = True
        prbCompile.Style = ProgressBarStyle.Continuous
        lblCanceling.Visible = False
        pnlCancel.Visible = True
        Application.DoEvents()

        Me.bwLoadMoviesInSet.WorkerSupportsCancellation = True
        Me.bwLoadMoviesInSet.WorkerReportsProgress = True
        Me.bwLoadMoviesInSet.RunWorkerAsync()
    End Sub

    Private Sub FillInfo(Optional ByVal DoAll As Boolean = True)
        Try
            With Me
                If Not String.IsNullOrEmpty(Master.currMovieSet.ListTitle) Then
                    .txtTitle.Text = Master.currMovieSet.ListTitle
                End If

                If Not String.IsNullOrEmpty(Master.currMovieSet.MovieSet.ID) Then
                    .txtCollectionID.Text = Master.currMovieSet.MovieSet.ID
                End If

                If DoAll Then

                    If Not String.IsNullOrEmpty(Master.currMovieSet.BannerPath) AndAlso Master.currMovieSet.BannerPath.Substring(0, 1) = ":" Then
                        MovieBanner.FromWeb(Master.currMovieSet.BannerPath.Substring(1, Master.currMovieSet.BannerPath.Length - 1))
                    Else
                        MovieBanner.FromFile(Master.currMovieSet.BannerPath)
                    End If
                    If Not IsNothing(MovieBanner.Image) Then
                        .pbMovieBanner.Image = MovieBanner.Image
                        .pbMovieBanner.Tag = MovieBanner

                        .lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieBanner.Image.Width, .pbMovieBanner.Image.Height)
                        .lblMovieBannerSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.ClearArtPath) AndAlso Master.currMovieSet.ClearArtPath.Substring(0, 1) = ":" Then
                        MovieClearArt.FromWeb(Master.currMovieSet.ClearArtPath.Substring(1, Master.currMovieSet.ClearArtPath.Length - 1))
                    Else
                        MovieClearArt.FromFile(Master.currMovieSet.ClearArtPath)
                    End If
                    If Not IsNothing(MovieClearArt.Image) Then
                        .pbMovieClearArt.Image = MovieClearArt.Image
                        .pbMovieClearArt.Tag = MovieClearArt

                        .lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearArt.Image.Width, .pbMovieClearArt.Image.Height)
                        .lblMovieClearArtSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.ClearLogoPath) AndAlso Master.currMovieSet.ClearLogoPath.Substring(0, 1) = ":" Then
                        MovieClearLogo.FromWeb(Master.currMovieSet.ClearLogoPath.Substring(1, Master.currMovieSet.ClearLogoPath.Length - 1))
                    Else
                        MovieClearLogo.FromFile(Master.currMovieSet.ClearLogoPath)
                    End If
                    If Not IsNothing(MovieClearLogo.Image) Then
                        .pbMovieClearLogo.Image = MovieClearLogo.Image
                        .pbMovieClearLogo.Tag = MovieClearLogo

                        .lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearLogo.Image.Width, .pbMovieClearLogo.Image.Height)
                        .lblMovieClearLogoSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.DiscArtPath) AndAlso Master.currMovieSet.DiscArtPath.Substring(0, 1) = ":" Then
                        MovieDiscArt.FromWeb(Master.currMovieSet.DiscArtPath.Substring(1, Master.currMovieSet.DiscArtPath.Length - 1))
                    Else
                        MovieDiscArt.FromFile(Master.currMovieSet.DiscArtPath)
                    End If
                    If Not IsNothing(MovieDiscArt.Image) Then
                        .pbMovieDiscArt.Image = MovieDiscArt.Image
                        .pbMovieDiscArt.Tag = MovieDiscArt

                        .lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieDiscArt.Image.Width, .pbMovieDiscArt.Image.Height)
                        .lblMovieDiscArtSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.FanartPath) AndAlso Master.currMovieSet.FanartPath.Substring(0, 1) = ":" Then
                        MovieFanart.FromWeb(Master.currMovieSet.FanartPath.Substring(1, Master.currMovieSet.FanartPath.Length - 1))
                    Else
                        MovieFanart.FromFile(Master.currMovieSet.FanartPath)
                    End If
                    If Not IsNothing(MovieFanart.Image) Then
                        .pbMovieFanart.Image = MovieFanart.Image
                        .pbMovieFanart.Tag = MovieFanart

                        .lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieFanart.Image.Width, .pbMovieFanart.Image.Height)
                        .lblMovieFanartSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.LandscapePath) AndAlso Master.currMovieSet.LandscapePath.Substring(0, 1) = ":" Then
                        MovieLandscape.FromWeb(Master.currMovieSet.LandscapePath.Substring(1, Master.currMovieSet.LandscapePath.Length - 1))
                    Else
                        MovieLandscape.FromFile(Master.currMovieSet.LandscapePath)
                    End If
                    If Not IsNothing(MovieLandscape.Image) Then
                        .pbMovieLandscape.Image = MovieLandscape.Image
                        .pbMovieLandscape.Tag = MovieLandscape

                        .lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieLandscape.Image.Width, .pbMovieLandscape.Image.Height)
                        .lblMovieLandscapeSize.Visible = True
                    End If

                    If Not String.IsNullOrEmpty(Master.currMovieSet.PosterPath) AndAlso Master.currMovieSet.PosterPath.Substring(0, 1) = ":" Then
                        MoviePoster.FromWeb(Master.currMovieSet.PosterPath.Substring(1, Master.currMovieSet.PosterPath.Length - 1))
                    Else
                        MoviePoster.FromFile(Master.currMovieSet.PosterPath)
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
                End If

                If String.IsNullOrEmpty(Master.eSettings.MovieMoviesetsPath) Then
                    tcEditMovie.TabPages.Remove(tpBanner)
                    tcEditMovie.TabPages.Remove(tpClearArt)
                    tcEditMovie.TabPages.Remove(tpClearLogo)
                    tcEditMovie.TabPages.Remove(tpDiscArt)
                    tcEditMovie.TabPages.Remove(tpFanart)
                    tcEditMovie.TabPages.Remove(tpLandscape)
                    tcEditMovie.TabPages.Remove(tpPoster)
                End If
            End With

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Me.SetInfo()

            Master.DB.SaveMovieSetToDB(Master.currMovieSet, False, False, True)

            If needsMovieUpdate Then
                SaveSetToMovies()
                RemoveSetFromMovies()
            End If

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

    Private Sub SaveSetToMovies()
        Try
            Me.SetControlsEnabled(False)

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each tMovie As MovieInSet In MoviesInSet
                    'If Not Master.eSettings.MovieYAMJCompatibleSets Then
                    '    tMovie.DBMovie.Movie.AddSet(mSet.Set, 0)
                    'Else
                    tMovie.DBMovie.Movie.AddSet(Master.currMovieSet.ID, Master.currMovieSet.ListTitle, tMovie.Order)
                    'End If
                    Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.SetControlsEnabled(True)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub RemoveSetFromMovies()
        Try
            Me.SetControlsEnabled(False)

            Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                For Each tMovie As MovieInSet In MoviesToRemove
                    tMovie.DBMovie.Movie.RemoveSet(Master.currMovieSet.ID)
                    Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True)
                Next
                SQLtransaction.Commit()
            End Using

            Me.SetControlsEnabled(True)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        Me.pnlSaving.Visible = Not isEnabled
        Me.OK_Button.Enabled = isEnabled
        Me.btnLoadMoviesFromDB.Enabled = isEnabled
        Me.btnMovieAdd.Enabled = isEnabled
        Me.btnMovieDown.Enabled = isEnabled
        Me.btnMovieRemove.Enabled = isEnabled
        Me.btnMovieUp.Enabled = isEnabled
        Me.lvMoviesInDB.Enabled = isEnabled
        Me.lvMoviesInSet.Enabled = isEnabled
        Me.lvMoviesToRemove.Enabled = isEnabled

        Application.DoEvents()
    End Sub

    Private Sub SetInfo()
        Try
            With Me
                Me.OK_Button.Enabled = False
                Me.Cancel_Button.Enabled = False
                Me.btnLoadMoviesFromDB.Enabled = False
                Me.btnRescrape.Enabled = False

                Master.currMovieSet.ListTitle = .txtTitle.Text.Trim
                Master.currMovieSet.MovieSet.ID = .txtCollectionID.Text.Trim

                If Master.currMovieSet.RemoveBanner Then
                    .MovieBanner.DeleteMovieSetBanner(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemoveClearArt Then
                    .MovieClearArt.DeleteMovieSetClearArt(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemoveClearLogo Then
                    .MovieClearLogo.DeleteMovieSetClearLogo(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemoveDiscArt Then
                    .MovieDiscArt.DeleteMovieSetDiscArt(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemoveFanart Then
                    .MovieFanart.DeleteMovieSetFanart(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemoveLandscape Then
                    .MovieLandscape.DeleteMovieSetLandscape(Master.currMovieSet)
                End If

                If Master.currMovieSet.RemovePoster Then
                    .MoviePoster.DeleteMovieSetPoster(Master.currMovieSet)
                End If

                If Not IsNothing(.MovieBanner.Image) Then
                    Dim fPath As String = .MovieBanner.SaveAsMovieSetBanner(Master.currMovieSet)
                    Master.currMovieSet.BannerPath = fPath
                Else
                    .MovieBanner.DeleteMovieSetBanner(Master.currMovieSet)
                    Master.currMovieSet.BannerPath = String.Empty
                End If

                If Not IsNothing(.MovieClearArt.Image) Then
                    Dim fPath As String = .MovieClearArt.SaveAsMovieSetClearArt(Master.currMovieSet)
                    Master.currMovieSet.ClearArtPath = fPath
                Else
                    .MovieClearArt.DeleteMovieSetClearArt(Master.currMovieSet)
                    Master.currMovieSet.ClearArtPath = String.Empty
                End If

                If Not IsNothing(.MovieClearLogo.Image) Then
                    Dim fPath As String = .MovieClearLogo.SaveAsMovieSetClearLogo(Master.currMovieSet)
                    Master.currMovieSet.ClearLogoPath = fPath
                Else
                    .MovieClearLogo.DeleteMovieSetClearLogo(Master.currMovieSet)
                    Master.currMovieSet.ClearLogoPath = String.Empty
                End If

                If Not IsNothing(.MovieDiscArt.Image) Then
                    Dim fPath As String = .MovieDiscArt.SaveAsMovieSetDiscArt(Master.currMovieSet)
                    Master.currMovieSet.DiscArtPath = fPath
                Else
                    .MovieDiscArt.DeleteMovieSetDiscArt(Master.currMovieSet)
                    Master.currMovieSet.DiscArtPath = String.Empty
                End If

                If Not IsNothing(.MovieFanart.Image) Then
                    Dim fPath As String = .MovieFanart.SaveAsMovieSetFanart(Master.currMovieSet)
                    Master.currMovieSet.FanartPath = fPath
                Else
                    .MovieFanart.DeleteMovieSetFanart(Master.currMovieSet)
                    Master.currMovieSet.FanartPath = String.Empty
                End If

                If Not IsNothing(.MovieLandscape.Image) Then
                    Dim fPath As String = .MovieLandscape.SaveAsMovieSetLandscape(Master.currMovieSet)
                    Master.currMovieSet.LandscapePath = fPath
                Else
                    .MovieLandscape.DeleteMovieSetLandscape(Master.currMovieSet)
                    Master.currMovieSet.LandscapePath = String.Empty
                End If

                If Not IsNothing(.MoviePoster.Image) Then
                    Dim pPath As String = .MoviePoster.SaveAsMovieSetPoster(Master.currMovieSet)
                    Master.currMovieSet.PosterPath = pPath
                Else
                    .MoviePoster.DeleteMovieSetPoster(Master.currMovieSet)
                    Master.currMovieSet.PosterPath = String.Empty
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Master.currMovieSet.ListTitle
        Dim sTitle As String = String.Concat(Master.eLang.GetString(1131, "Edit Movieset"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Text = sTitle
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
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
        Me.lblTopDetails.Text = Master.eLang.GetString(1132, "Edit the details for the selected movieset.")
        Me.lblTopTitle.Text = Master.eLang.GetString(1131, "Edit Movieset")
        Me.tpBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.tpDetails.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.tpDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        Me.needsMovieUpdate = True
    End Sub


#End Region 'Methods

#Region "Nested Types"

    Friend Class MovieInSet
        Implements IComparable(Of MovieInSet)

#Region "Fields"

        Private _dbmovie As Structures.DBMovie
        Private _id As Long
        Private _listtitle As String
        Private _order As Integer

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property DBMovie() As Structures.DBMovie
            Get
                Return Me._dbmovie
            End Get
            Set(ByVal value As Structures.DBMovie)
                Me._dbmovie = value
            End Set
        End Property

        Public Property ID() As Long
            Get
                Return Me._id
            End Get
            Set(ByVal value As Long)
                Me._id = value
            End Set
        End Property

        Public Property ListTitle() As String
            Get
                Return Me._listtitle
            End Get
            Set(ByVal value As String)
                Me._listtitle = value
            End Set
        End Property

        Public Property Order() As Integer
            Get
                Return Me._order
            End Get
            Set(ByVal value As Integer)
                Me._order = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._dbmovie = New Structures.DBMovie
            Me._id = -1
            Me._order = 0
            Me._listtitle = String.Empty
        End Sub

        Public Function CompareTo(ByVal other As MovieInSet) As Integer Implements IComparable(Of MovieInSet).CompareTo
            Return (Me.Order).CompareTo(other.Order)
        End Function

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class