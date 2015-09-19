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

    Private tmpDBElement As New Database.DBElement

    Private CachePath As String = String.Empty
    Private fResults As New Containers.ImgResult
    Private isAborting As Boolean = False
    Private pResults As New Containers.ImgResult

    Private sMovieID As String = String.Empty
    Private needsMovieUpdate As Boolean = False

    Private MoviesToRemove As New List(Of MovieInSet)
    Private MoviesInSet As New List(Of MovieInSet)

    Private bsMovies As New BindingSource
    Private dtMovies As New DataTable
    Private KeyBuffer As String = String.Empty

    'filter movies
    Private bDoingSearch As Boolean = False
    Private FilterArray As New List(Of String)
    Private filMoviesInSet As String = String.Empty
    Private filSearch As String = String.Empty
    Private currTextSearch As String = String.Empty
    Private prevTextSearch As String = String.Empty

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

    Public Overloads Function ShowDialog(ByVal DBMovieSet As Database.DBElement) As DialogResult
        Me.tmpDBElement = DBMovieSet
        Return MyBase.ShowDialog()
    End Function

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
                Me.tmpDBElement.MovieSet.TMDB = newColID
            End If
        End If
    End Sub

    Private Sub btnMovieUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMovieUp.Click
        'If Me.lvMoviesInSet.Items.Count > 0 AndAlso Me.lvMoviesInSet.SelectedItem IsNot Nothing AndAlso Me.lvMoviesInSet.SelectedIndex > 0 Then
        '    Me.needsMovieUpdate = True
        '    Dim iIndex As Integer = Me.lvMoviesInSet.SelectedItems(0)
        '    'Me.currSet.Movies(iIndex).Order = Me.lbMoviesInSet.SelectedIndex - 1
        '    'Me.currSet.Movies(iIndex - 1).Order += 1
        '    'Me.LoadCurrSet()
        '    Me.lvMoviesInSet.SelectedIndex = iIndex - 1
        '    Me.lvMoviesInSet.Focus()
        'End If
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

    Private Sub btnSearchMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchMovie.Click
        Me.SetControlsEnabled(False)

        Application.DoEvents()

        Me.FillMoviesFromDB()
        Me.RunFilter_Movies()
    End Sub

    Private Sub ReAddToSet()
        needsMovieUpdate = True

        Dim lMov As New MovieInSet

        If Me.lvMoviesToRemove.SelectedItems.Count > 0 Then
            Me.SetControlsEnabled(False)
            While Me.lvMoviesToRemove.SelectedItems.Count > 0
                Me.sMovieID = Me.lvMoviesToRemove.SelectedItems(0).SubItems(0).Text.ToString
                lMov = Me.MoviesToRemove.Find(AddressOf FindMovie)
                If lMov IsNot Nothing Then
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
    End Sub

    Private Sub RemoveFromSet()
        needsMovieUpdate = True

        Dim lMov As New MovieInSet

        If Me.lvMoviesInSet.SelectedItems.Count > 0 Then
            Me.SetControlsEnabled(False)
            While Me.lvMoviesInSet.SelectedItems.Count > 0
                Me.sMovieID = Me.lvMoviesInSet.SelectedItems(0).SubItems(0).Text.ToString
                lMov = Me.MoviesInSet.Find(AddressOf FindMovie)
                If lMov IsNot Nothing Then
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
        needsMovieUpdate = True

        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Me.SetControlsEnabled(False)
            For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
                Dim tmpMovie As New Database.DBElement
                tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(sRow.Cells(0).Value))
                If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                    If String.IsNullOrEmpty(Me.txtCollectionID.Text) AndAlso tmpMovie.Movie.TMDBColIDSpecified Then
                        Dim result As DialogResult = MessageBox.Show(String.Format(Master.eLang.GetString(1264, "Should the Collection ID of the movie ""{0}"" be used as ID for this Collection?"), tmpMovie.Movie.Title), Master.eLang.GetString(1263, "TMDB Collection ID found"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        If result = Windows.Forms.DialogResult.Yes Then
                            Me.txtCollectionID.Text = tmpMovie.Movie.TMDBColID
                            Me.tmpDBElement.MovieSet.TMDB = tmpMovie.Movie.TMDBColID
                        End If
                    End If
                    Dim newMovie As New MovieInSet With {.DBMovie = tmpMovie, .ListTitle = String.Concat(StringUtils.SortTokens_Movie(tmpMovie.Movie.Title), If(Not String.IsNullOrEmpty(tmpMovie.Movie.Year), String.Format(" ({0})", tmpMovie.Movie.Year), String.Empty)), .ID = tmpMovie.ID}
                    Me.MoviesInSet.Add(newMovie)
                    bsMovies.Remove(sRow.DataBoundItem)
                End If
            Next

            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing

            Me.FillMoviesInSet()
            Me.FillMoviesToRemove()
            Me.SetControlsEnabled(True)
            Me.btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Function FindMovie(ByVal lMov As MovieInSet) As Boolean
        If lMov.ID = CType(sMovieID, Long) Then Return True Else  : Return False
    End Function

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

    Private Sub btnRescrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRescrape.Click
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBElement, aContainer, ScrapeModifier) Then
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
                '.InitialDirectory = Directory.GetParent(Me.tmpDBElement.Filename).FullName
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

    Private Sub bwLoadMoviesInSet_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMoviesInSet.DoWork
        MoviesInSet.Clear()

        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim tmpMovie As New Database.DBElement
            Dim iProg As Integer = 0
            If Not (Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJCompatibleSets) Then
                If Me.tmpDBElement.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets INNER JOIN movie ON (MoviesSets.MovieID = movie.idMovie) ", _
                                                           "WHERE SetID = ", Me.tmpDBElement.ID, " ORDER BY movie.Year;")
                ElseIf Me.tmpDBElement.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets INNER JOIN movielist ON (MoviesSets.MovieID = movielist.idMovie) ", _
                                                           "WHERE SetID = ", Me.tmpDBElement.ID, " ORDER BY movielist.SortedTitle COLLATE NOCASE;")
                End If
            Else
                SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets ", _
                                                       "WHERE SetID = ", Me.tmpDBElement.ID, " ORDER BY SetOrder;")
            End If
            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        If bwLoadMoviesInSet.CancellationPending Then Return
                        tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
                        If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                            Dim tmpSetOrder As Integer = If(Not String.IsNullOrEmpty(SQLreader("SetOrder").ToString), CInt(SQLreader("SetOrder").ToString), Nothing)
                            MoviesInSet.Add(New MovieInSet With {.DBMovie = tmpMovie, .ID = tmpMovie.ID, .ListTitle = String.Concat(tmpMovie.ListTitle, If(Not String.IsNullOrEmpty(tmpMovie.Movie.Year), String.Format(" ({0})", tmpMovie.Movie.Year), String.Empty)), .Order = tmpSetOrder})
                        End If
                        Me.bwLoadMoviesInSet.ReportProgress(iProg, tmpMovie.Movie.Title)
                        iProg += 1
                    End While
                End If
            End Using
        End Using
    End Sub

    Private Sub bwLoadMoviesInSet_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMoviesInSet.RunWorkerCompleted
        If Me.MoviesInSet.Count > 0 Then
            Me.FillMoviesInSet()
        End If

        Me.lvMoviesInSet.Enabled = True
        Me.btnMovieUp.Enabled = True
        Me.btnMovieDown.Enabled = True
        Me.btnMovieRemove.Enabled = True
        Me.btnMovieAdd.Enabled = True
    End Sub

    Private Sub FillMoviesFromDB()
        Me.dgvMovies.SuspendLayout()

        Me.bsMovies.DataSource = Nothing
        Me.dgvMovies.DataSource = Nothing

        Master.DB.FillDataTable(Me.dtMovies, "SELECT * FROM movie ORDER BY ListTitle COLLATE NOCASE;")

        If Me.dtMovies.Rows.Count > 0 Then
            With Me
                .bsMovies.DataSource = .dtMovies
                .dgvMovies.DataSource = .bsMovies

                .dgvMovies.Columns(0).Visible = False
                .dgvMovies.Columns(1).Visible = False
                .dgvMovies.Columns(2).Visible = False
                .dgvMovies.Columns(3).Resizable = DataGridViewTriState.True
                .dgvMovies.Columns(3).ReadOnly = True
                .dgvMovies.Columns(3).MinimumWidth = 83
                .dgvMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvMovies.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")

                For i As Integer = 4 To .dgvMovies.Columns.Count - 1
                    .dgvMovies.Columns(i).Visible = False
                Next

                .dgvMovies.Columns(0).ValueType = GetType(Int32)

                .SetControlsEnabled(True)

            End With
        End If

        Me.dgvMovies.ResumeLayout()
        SetControlsEnabled(True)
        Me.btnMovieAdd.Enabled = False
    End Sub

    Private Sub FillMoviesInSet()
        Me.lvMoviesInSet.SuspendLayout()

        Me.lvMoviesInSet.Items.Clear()
        If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJCompatibleSets Then
            Me.MoviesInSet.Sort()
        End If

        Dim lvItem As ListViewItem
        Me.lvMoviesInSet.Items.Clear()
        For Each tMovie As MovieInSet In Me.MoviesInSet
            lvItem = Me.lvMoviesInSet.Items.Add(tMovie.ID.ToString)
            lvItem.SubItems.Add(tMovie.Order.ToString)
            lvItem.SubItems.Add(tMovie.ListTitle)
        Next

        'filter out all movies that are already in movieset
        If MoviesInSet.Count > 0 Then
            Me.FilterArray.Remove(Me.filMoviesInSet)

            Dim alMoviesInSet As New List(Of String)

            For Each movie In MoviesInSet
                alMoviesInSet.Add(movie.DBMovie.ID.ToString)
            Next

            For i As Integer = 0 To alMoviesInSet.Count - 1
                alMoviesInSet.Item(i) = String.Format("idMovie NOT = {0}", alMoviesInSet.Item(i))
            Next

            Me.filMoviesInSet = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alMoviesInSet.ToArray, " AND "))

            Me.FilterArray.Add(Me.filMoviesInSet)
            Me.RunFilter_Movies()
        Else
            If Not String.IsNullOrEmpty(Me.filMoviesInSet) Then
                Me.FilterArray.Remove(Me.filMoviesInSet)
                Me.filMoviesInSet = String.Empty
                Me.RunFilter_Movies()
            End If
        End If

        Me.lvMoviesInSet.ResumeLayout()
        Me.btnMovieUp.Enabled = False
        Me.btnMovieDown.Enabled = False
        Me.btnMovieRemove.Enabled = False
    End Sub

    Private Sub FillMoviesToRemove()
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
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.pbBanner.AllowDrop = True
        Me.pbClearArt.AllowDrop = True
        Me.pbClearLogo.AllowDrop = True
        Me.pbDiscArt.AllowDrop = True
        Me.pbFanart.AllowDrop = True
        Me.pbLandscape.AllowDrop = True
        Me.pbPoster.AllowDrop = True

        If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJCompatibleSets Then
            Me.btnMovieDown.Visible = True
            Me.btnMovieUp.Visible = True
            Me.colID.Width = 25
        End If

        Me.SetUp()

        Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            Me.pnlTop.BackgroundImage = iBackground
        End Using

        Me.FillInfo()

        If String.IsNullOrEmpty(txtTitle.Text) Then
            Me.OK_Button.Enabled = False
        Else
            Me.OK_Button.Enabled = True
        End If

        If Me.tmpDBElement.ID = -1 Then
            Me.btnRescrape.Enabled = False
        End If
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()

        Application.DoEvents()

        Me.bwLoadMoviesInSet.WorkerSupportsCancellation = True
        Me.bwLoadMoviesInSet.WorkerReportsProgress = True
        Me.bwLoadMoviesInSet.RunWorkerAsync()
    End Sub

    Private Sub FillInfo()
        With Me

            Me.cbMovieSorting.SelectedIndex = Me.tmpDBElement.SortMethod

            Me.chkMark.Checked = Me.tmpDBElement.IsMark

            RemoveHandler txtTitle.TextChanged, AddressOf txtTitle_TextChanged
            If Not String.IsNullOrEmpty(Me.tmpDBElement.MovieSet.Title) Then
                .txtTitle.Text = Me.tmpDBElement.MovieSet.Title
            End If
            AddHandler txtTitle.TextChanged, AddressOf txtTitle_TextChanged

            If Not String.IsNullOrEmpty(Me.tmpDBElement.MovieSet.Plot) Then
                .txtPlot.Text = Me.tmpDBElement.MovieSet.Plot
            End If

            If Not String.IsNullOrEmpty(Me.tmpDBElement.MovieSet.TMDB) Then
                .txtCollectionID.Text = Me.tmpDBElement.MovieSet.TMDB
            End If

            'Images and TabPages

            If Master.eSettings.MovieSetBannerAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner) Then
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

            If Master.eSettings.MovieSetClearArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt) Then
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

            If Master.eSettings.MovieSetClearLogoAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo) Then
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

            If Master.eSettings.MovieSetDiscArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt) Then
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

            If Master.eSettings.MovieSetFanartAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart) Then
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

            If Master.eSettings.MovieSetLandscapeAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape) Then
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

            If Master.eSettings.MovieSetPosterAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster) Then
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
        Me.SetInfo()

        If needsMovieUpdate Then
            SaveSetToMovies()
            RemoveSetFromMovies()
        End If

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

    Private Sub SaveSetToMovies()
        Me.SetControlsEnabled(False)

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tMovie As MovieInSet In MoviesInSet
                'If Not Master.eSettings.MovieYAMJCompatibleSets Then
                '    tMovie.DBMovie.Movie.AddSet(mSet.Set, 0)
                'Else
                tMovie.DBMovie.Movie.AddSet(Me.tmpDBElement.ID, Me.tmpDBElement.MovieSet.Title, tMovie.Order, Me.tmpDBElement.MovieSet.TMDB)
                'End If
                Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True, False)
            Next
            SQLtransaction.Commit()
        End Using

        Me.SetControlsEnabled(True)
    End Sub

    Private Sub RemoveSetFromMovies()
        Me.SetControlsEnabled(False)

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tMovie As MovieInSet In MoviesToRemove
                tMovie.DBMovie.Movie.RemoveSet(Me.tmpDBElement.ID)
                Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True, False)
            Next
            SQLtransaction.Commit()
        End Using

        Me.SetControlsEnabled(True)
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        'Me.pnlSaving.Visible = Not isEnabled
        Me.OK_Button.Enabled = isEnabled
        Me.btnSearchMovie.Enabled = isEnabled
        Me.btnMovieAdd.Enabled = isEnabled
        Me.btnMovieDown.Enabled = isEnabled
        Me.btnMovieRemove.Enabled = isEnabled
        Me.btnMovieUp.Enabled = isEnabled
        Me.btnRescrape.Enabled = isEnabled
        Me.dgvMovies.Enabled = isEnabled
        Me.lvMoviesInSet.Enabled = isEnabled
        Me.lvMoviesToRemove.Enabled = isEnabled

        Application.DoEvents()
    End Sub

    Private Sub SetInfo()
        With Me
            Me.OK_Button.Enabled = False
            Me.Cancel_Button.Enabled = False
            Me.btnSearchMovie.Enabled = False
            Me.btnRescrape.Enabled = False

            'it is important that existing NFO and images will be deleted before the new name is saved!
            If needsMovieUpdate Then
                If Me.tmpDBElement.NfoPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.tmpDBElement.NfoPath) Then
                    File.Delete(Me.tmpDBElement.NfoPath)
                End If
            End If

            Me.tmpDBElement.IsMark = Me.chkMark.Checked
            Me.tmpDBElement.SortMethod = DirectCast(Me.cbMovieSorting.SelectedIndex, Enums.SortMethod_MovieSet)

            If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                Me.tmpDBElement.ListTitle = StringUtils.SortTokens_MovieSet(.txtTitle.Text.Trim)
                Me.tmpDBElement.MovieSet.Title = .txtTitle.Text.Trim
            End If

            Me.tmpDBElement.MovieSet.TMDB = .txtCollectionID.Text.Trim
            Me.tmpDBElement.MovieSet.Plot = .txtPlot.Text.Trim
        End With
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

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        Me.btnSetBannerLocal.Text = strLocalBrowse
        Me.btnSetClearArtLocal.Text = strLocalBrowse
        Me.btnSetClearLogoLocal.Text = strLocalBrowse
        Me.btnSetDiscArtLocal.Text = strLocalBrowse
        Me.btnSetFanartLocal.Text = strLocalBrowse
        Me.btnSetLandscapeLocal.Text = strLocalBrowse
        Me.btnSetPosterLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        Me.btnRemoveBanner.Text = strRemove
        Me.btnRemoveClearArt.Text = strRemove
        Me.btnRemoveClearLogo.Text = strRemove
        Me.btnRemoveDiscArt.Text = strRemove
        Me.btnRemoveFanart.Text = strRemove
        Me.btnRemoveLandscape.Text = strRemove
        Me.btnRemovePoster.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        Me.btnSetBannerScrape.Text = strScrape
        Me.btnSetClearArtScrape.Text = strScrape
        Me.btnSetClearLogoScrape.Text = strScrape
        Me.btnSetDiscArtScrape.Text = strScrape
        Me.btnSetFanartScrape.Text = strScrape
        Me.btnSetLandscapeScrape.Text = strScrape
        Me.btnSetPosterScrape.Text = strScrape

        Dim mTitle As String = Me.tmpDBElement.MovieSet.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(1131, "Edit MovieSet"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Me.Text = sTitle
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        Me.btnSearchMovie.Text = Master.eLang.GetString(528, "Search Movie")
        Me.chkMark.Text = Master.eLang.GetString(23, "Mark")
        Me.lblCollectionID.Text = Master.eLang.GetString(1206, "Collection ID:")
        Me.lblMovieSorting.Text = String.Concat(Master.eLang.GetString(665, "Movies sorted by"), ":")
        Me.lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        Me.lblTopDetails.Text = Master.eLang.GetString(1132, "Edit the details for the selected movieset.")
        Me.lblTopTitle.Text = Master.eLang.GetString(1131, "Edit Movieset")
        Me.tpBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.tpDetails.Text = Master.eLang.GetString(26, "Details")
        Me.tpDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        Me.tpMovies.Text = Master.eLang.GetString(36, "Movies")
        Me.tpPoster.Text = Master.eLang.GetString(148, "Poster")

        Me.cbMovieSorting.Items.Clear()
        Me.cbMovieSorting.Items.AddRange(New String() {Master.eLang.GetString(278, "Year"), Master.eLang.GetString(21, "Title")})
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        Me.needsMovieUpdate = True
        If String.IsNullOrEmpty(txtTitle.Text) Then
            Me.OK_Button.Enabled = False
        Else
            Me.OK_Button.Enabled = True
        End If
    End Sub

    Private Sub FillList()
        Me.bsMovies.DataSource = Nothing
        Me.dgvMovies.DataSource = Nothing

        Master.DB.FillDataTable(Me.dtMovies, "SELECT * FROM movie ORDER BY ListTitle COLLATE NOCASE;")


        If Me.dtMovies.Rows.Count > 0 Then
            With Me
                .bsMovies.DataSource = .dtMovies
                .dgvMovies.DataSource = .bsMovies

                .dgvMovies.Columns(0).Visible = False
                .dgvMovies.Columns(1).Visible = False
                .dgvMovies.Columns(2).Visible = False
                .dgvMovies.Columns(3).Resizable = DataGridViewTriState.True
                .dgvMovies.Columns(3).ReadOnly = True
                .dgvMovies.Columns(3).MinimumWidth = 83
                .dgvMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
                .dgvMovies.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")

                For i As Integer = 4 To .dgvMovies.Columns.Count - 1
                    .dgvMovies.Columns(i).Visible = False
                Next

                .dgvMovies.Columns(0).ValueType = GetType(Int32)

                .SetControlsEnabled(True)

            End With
        End If
    End Sub

    Private Sub RunFilter_Movies()
        If Me.Visible Then

            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing

            If FilterArray.Count > 0 Then
                Dim FilterString As String = String.Empty

                FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray.ToArray, " AND ")

                bsMovies.Filter = FilterString
            Else
                bsMovies.RemoveFilter()
            End If

            Me.txtSearchMovies.Focus()
        End If
    End Sub

    Private Sub dgvMovies_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvMovies.Sorted
        If Me.dgvMovies.RowCount > 0 Then
            Me.dgvMovies.CurrentCell = Nothing
            Me.dgvMovies.ClearSelection()
            Me.dgvMovies.Rows(0).Selected = True
            Me.dgvMovies.CurrentCell = Me.dgvMovies.Rows(0).Cells(3)
        End If
    End Sub

    Private Sub dgvMovies_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMovies.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovies.Focus()
    End Sub

    Private Sub dgvMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgvMovies.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In Me.dgvMovies.Rows
                If drvRow.Cells(3).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    Me.dgvMovies.CurrentCell = drvRow.Cells(3)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub tmrKeyBuffer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub txtSearchMovies_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Me.dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearchMovies_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMovies.TextChanged
        Me.currTextSearch = Me.txtSearchMovies.Text

        Me.tmrSearchWait_Movies.Enabled = False
        Me.tmrSearch_Movies.Enabled = False
        Me.tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub tmrSearch_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearch_Movies.Tick
        Me.tmrSearchWait_Movies.Enabled = False
        Me.tmrSearch_Movies.Enabled = False
        bDoingSearch = True

        If Not String.IsNullOrEmpty(Me.txtSearchMovies.Text) Then
            Me.FilterArray.Remove(Me.filSearch)
            Me.filSearch = String.Empty

            Me.filSearch = String.Concat("Title LIKE '%", Me.txtSearchMovies.Text, "%'")
            Me.FilterArray.Add(Me.filSearch)

            Me.RunFilter_Movies()

        Else
            If Not String.IsNullOrEmpty(Me.filSearch) Then
                Me.FilterArray.Remove(Me.filSearch)
                Me.filSearch = String.Empty
            End If
            Me.RunFilter_Movies()
        End If
    End Sub

    Private Sub tmrSearchWait_Movies_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSearchWait_Movies.Tick
        Me.tmrSearch_Movies.Enabled = False
        If Me.prevTextSearch = Me.currTextSearch Then
            Me.tmrSearch_Movies.Enabled = True
        Else
            Me.prevTextSearch = Me.currTextSearch
        End If
    End Sub

    Private Sub dgvMovies_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovies.CellPainting
        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not Me.dgvMovies.Item(e.ColumnIndex, e.RowIndex).Displayed Then
            e.Handled = True
            Return
        End If

        'text
        If e.ColumnIndex = 3 AndAlso e.RowIndex >= 0 Then
            e.CellStyle.BackColor = Color.White
            e.CellStyle.ForeColor = Color.Black
            e.CellStyle.Font = New Font("Segoe UI", 8.25, FontStyle.Regular)
            e.CellStyle.SelectionForeColor = Color.FromKnownColor(KnownColor.HighlightText)
        End If
    End Sub

    Private Sub dgvMovies_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMovies.CellClick
        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Me.btnMovieAdd.Enabled = True
        Else
            Me.btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub dgvMovies_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMovies.SelectionChanged
        If Me.dgvMovies.SelectedRows.Count > 0 Then
            Me.btnMovieAdd.Enabled = True
        Else
            Me.btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub dgvMovies_DoubleClick(sender As Object, e As EventArgs) Handles dgvMovies.DoubleClick
        If Me.dgvMovies.SelectedRows.Count = 1 Then
            AddMovieToSet()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Friend Class MovieInSet
        Implements IComparable(Of MovieInSet)

#Region "Fields"

        Private _dbmovie As Database.DBElement
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

        Public Property DBMovie() As Database.DBElement
            Get
                Return Me._dbmovie
            End Get
            Set(ByVal value As Database.DBElement)
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
            Me._dbmovie = New Database.DBElement
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