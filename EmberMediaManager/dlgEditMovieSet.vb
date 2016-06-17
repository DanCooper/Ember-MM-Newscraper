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

Public Class dlgEditMovieSet

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private tmpDBElement As Database.DBElement

    Private sMovieID As String = String.Empty

    Private bsMovies As New BindingSource
    Private dtMovies As New DataTable
    Private KeyBuffer As String = String.Empty

    'filter movies
    Private bDoingSearch As Boolean = False
    Private FilterArray As New List(Of String)
    Private filMoviesInSet As String = String.Empty
    Private filMoviesToRemove As String = String.Empty
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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBMovieSet As Database.DBElement) As DialogResult
        tmpDBElement = DBMovieSet
        Return ShowDialog()
    End Function

    Private Sub btnGetTMDBColID_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetTMDBColID.Click
        Dim newColID As String = String.Empty

        If tmpDBElement.MoviesInSetSpecified Then
            If Not String.IsNullOrEmpty(tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.TMDBColID) Then
                newColID = tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.TMDBColID
            Else
                newColID = ModulesManager.Instance.GetMovieCollectionID(tmpDBElement.MoviesInSet.Item(0).DBMovie.Movie.ID)
            End If

            If Not String.IsNullOrEmpty(newColID) Then
                txtCollectionID.Text = newColID
                tmpDBElement.MovieSet.TMDB = newColID
            End If
        End If
    End Sub

    Private Sub btnMovieDown_Click(sender As Object, e As EventArgs) Handles btnMovieDown.Click
        If lvMoviesInSet.Items.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Item(0).Index < lvMoviesInSet.Items.Count Then
            Dim iIndex As Integer = lvMoviesInSet.SelectedItems.Item(0).Index
            tmpDBElement.MoviesInSet(iIndex).Order += 1
            tmpDBElement.MoviesInSet(iIndex + 1).Order -= 1
            FillMoviesInSet()
            lvMoviesInSet.Items(iIndex + 1).Selected = True
            lvMoviesInSet.Focus()
        End If
    End Sub

    Private Sub btnMovieUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieUp.Click
        If lvMoviesInSet.Items.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems.Item(0).Index > 0 Then
            Dim iIndex As Integer = lvMoviesInSet.SelectedItems.Item(0).Index
            tmpDBElement.MoviesInSet(iIndex).Order -= 1
            tmpDBElement.MoviesInSet(iIndex - 1).Order += 1
            FillMoviesInSet()
            lvMoviesInSet.Items(iIndex - 1).Selected = True
            lvMoviesInSet.Focus()
        End If
    End Sub

    Private Sub lvMoviesInSet_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvMoviesInSet.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveFromSet()
    End Sub

    Private Sub btnMovieRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieRemove.Click
        RemoveFromSet()
    End Sub

    Private Sub btnSearchMovie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchMovie.Click
        SetControlsEnabled(False)

        Application.DoEvents()

        FillMoviesFromDB()
        RunFilter_Movies()
    End Sub

    Private Sub RemoveFromSet()
        Dim lMov As New MediaContainers.MovieInSet

        If lvMoviesInSet.SelectedItems.Count > 0 Then
            SetControlsEnabled(False)
            While lvMoviesInSet.SelectedItems.Count > 0
                sMovieID = lvMoviesInSet.SelectedItems(0).SubItems(0).Text.ToString
                lMov = tmpDBElement.MoviesInSet.Find(AddressOf FindMovie)
                If lMov IsNot Nothing Then
                    tmpDBElement.MoviesInSet.Remove(lMov)
                Else
                    lvMoviesInSet.Items.Remove(lvMoviesInSet.SelectedItems(0))
                End If
            End While

            FillMoviesInSet()
            RunFilter_Movies()
            SetControlsEnabled(True)
            btnMovieUp.Enabled = False
            btnMovieDown.Enabled = False
            btnMovieRemove.Enabled = False
        End If
    End Sub

    Private Sub lvMoviesInSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMoviesInSet.SelectedIndexChanged
        btnMovieDown.Enabled = lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems(0).Index < lvMoviesInSet.Items.Count - 1
        btnMovieRemove.Enabled = lvMoviesInSet.SelectedItems.Count > 0
        btnMovieUp.Enabled = lvMoviesInSet.SelectedItems.Count > 0 AndAlso lvMoviesInSet.SelectedItems(0).Index > 0
    End Sub

    Private Sub btnMovieAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMovieAdd.Click
        AddMovieToSet()
    End Sub

    Private Sub AddMovieToSet()
        If dgvMovies.SelectedRows.Count > 0 Then
            SetControlsEnabled(False)
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(0).Value))
                If String.IsNullOrEmpty(txtCollectionID.Text) AndAlso tmpMovie.Movie.TMDBColIDSpecified Then
                    If MessageBox.Show(String.Format(Master.eLang.GetString(1264, "Should the Collection ID of the movie ""{0}"" be used as ID for this Collection?"), tmpMovie.Movie.Title), Master.eLang.GetString(1263, "TMDB Collection ID found"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                        txtCollectionID.Text = tmpMovie.Movie.TMDBColID
                        tmpDBElement.MovieSet.TMDB = tmpMovie.Movie.TMDBColID
                    End If
                End If
                Dim newMovieInSet As New MediaContainers.MovieInSet With {.DBMovie = tmpMovie, .Order = tmpDBElement.MoviesInSet.Count}
                tmpDBElement.MoviesInSet.Add(newMovieInSet)
            Next

            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing

            FillMoviesInSet()
            RunFilter_Movies()
            SetControlsEnabled(True)
            btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Function FindMovie(ByVal lMov As MediaContainers.MovieInSet) As Boolean
        If lMov.DBMovie.ID = CType(sMovieID, Long) Then Return True Else : Return False
    End Function

    Private Sub btnRemoveBanner_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveBanner.Click
        pbBanner.Image = Nothing
        pbBanner.Tag = Nothing
        lblBannerSize.Text = String.Empty
        lblBannerSize.Visible = False
        tmpDBElement.ImagesContainer.Banner = New MediaContainers.Image
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

    Private Sub btnRemoveDiscArt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemoveDiscArt.Click
        pbDiscArt.Image = Nothing
        pbDiscArt.Tag = Nothing
        lblDiscArtSize.Text = String.Empty
        lblDiscArtSize.Visible = False
        tmpDBElement.ImagesContainer.DiscArt = New MediaContainers.Image
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

    Private Sub btnRescrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRescrape.Click
        DialogResult = System.Windows.Forms.DialogResult.Retry
        Close()
    End Sub

    Private Sub btnSetBannerDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBannerDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetBannerScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetBannerScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearArtDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearArtDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearArtScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearLogoDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearLogoDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetClearLogoScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetClearLogoScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetDiscArtDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetDiscArtDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetDiscArtScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetDiscArtScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainDiscArt, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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

    Private Sub btnSetDiscArtLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetDiscArtLocal.Click
        Try
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetFanartDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetFanartScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetFanartScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetLandscapeDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetLandscapeDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetLandscapeScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetLandscapeScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetPosterDL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterDL.Click
        Try
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnSetPosterScrape_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetPosterScrape.Click
        Dim aContainer As New MediaContainers.SearchResultsContainer
        Dim ScrapeModifiers As New Structures.ScrapeModifiers

        Cursor = Cursors.WaitCursor
        Functions.SetScrapeModifiers(ScrapeModifiers, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(tmpDBElement, aContainer, ScrapeModifiers) Then
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
            With ofdLocalFiles
                '.InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
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
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub FillMoviesFromDB()
        dgvMovies.SuspendLayout()

        bsMovies.DataSource = Nothing
        dgvMovies.DataSource = Nothing

        Master.DB.FillDataTable(dtMovies, "SELECT * FROM movie ORDER BY ListTitle COLLATE NOCASE;")

        If dtMovies.Rows.Count > 0 Then
            With Me
                .bsMovies.DataSource = .dtMovies
                .dgvMovies.DataSource = .bsMovies

                For i As Integer = 0 To .dgvMovies.Columns.Count - 1
                    .dgvMovies.Columns(i).Visible = False
                Next

                .dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns("ListTitle").MinimumWidth = 83
                .dgvMovies.Columns("ListTitle").ReadOnly = True
                .dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                .dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns("ListTitle").Visible = True

                .dgvMovies.Columns("idMovie").ValueType = GetType(Int32)

                .SetControlsEnabled(True)

            End With
        End If

        dgvMovies.ResumeLayout()
        SetControlsEnabled(True)
        btnMovieAdd.Enabled = False
    End Sub

    Private Sub FillMoviesInSet()
        lvMoviesInSet.SuspendLayout()

        lvMoviesInSet.Items.Clear()
        If Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets Then
            tmpDBElement.MoviesInSet.Sort()
        End If

        Dim lvItem As ListViewItem
        lvMoviesInSet.Items.Clear()
        Dim iOrder As Integer = 0
        For Each tMovie As MediaContainers.MovieInSet In tmpDBElement.MoviesInSet
            tMovie.Order = iOrder
            lvItem = lvMoviesInSet.Items.Add(tMovie.DBMovie.ID.ToString)
            lvItem.SubItems.Add(tMovie.Order.ToString)
            lvItem.SubItems.Add(tMovie.ListTitle)
            iOrder += 1
        Next

        'filter out all movies that are already in movieset
        If tmpDBElement.MoviesInSetSpecified Then
            FilterArray.Remove(filMoviesInSet)

            Dim alMoviesInSet As New List(Of String)

            For Each movie As MediaContainers.MovieInSet In tmpDBElement.MoviesInSet
                alMoviesInSet.Add(movie.DBMovie.ID.ToString)
            Next

            For i As Integer = 0 To alMoviesInSet.Count - 1
                alMoviesInSet.Item(i) = String.Format("idMovie NOT = {0}", alMoviesInSet.Item(i))
            Next

            filMoviesInSet = String.Format("({0})", Microsoft.VisualBasic.Strings.Join(alMoviesInSet.ToArray, " AND "))

            FilterArray.Add(filMoviesInSet)
        Else
            If Not String.IsNullOrEmpty(filMoviesInSet) Then
                FilterArray.Remove(filMoviesInSet)
                filMoviesInSet = String.Empty
            End If
        End If

        lvMoviesInSet.ResumeLayout()
        btnMovieUp.Enabled = False
        btnMovieDown.Enabled = False
        btnMovieRemove.Enabled = False
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub dlgEditMovie_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        pbBanner.AllowDrop = True
        pbClearArt.AllowDrop = True
        pbClearLogo.AllowDrop = True
        pbDiscArt.AllowDrop = True
        pbFanart.AllowDrop = True
        pbLandscape.AllowDrop = True
        pbPoster.AllowDrop = True

        If Master.eSettings.MovieScraperCollectionsYAMJCompatibleSets Then
            btnMovieDown.Visible = True
            btnMovieUp.Visible = True
            colOrdering.Width = 25
        End If

        SetUp()

        Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            pnlTop.BackgroundImage = iBackground
        End Using

        FillInfo()

        FillMoviesInSet()

        If Not tmpDBElement.IDSpecified Then
            btnRescrape.Enabled = False
        End If
    End Sub

    Private Sub dlgEditMovie_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        Application.DoEvents()
    End Sub

    Private Sub FillInfo()
        cbMovieSorting.SelectedIndex = tmpDBElement.SortMethod
        chkMark.Checked = tmpDBElement.IsMark
        txtCollectionID.Text = tmpDBElement.MovieSet.TMDB
        txtPlot.Text = tmpDBElement.MovieSet.Plot
        txtTitle.Text = tmpDBElement.MovieSet.Title

        'Images and TabPages
        With tmpDBElement.ImagesContainer

            'Load all images to MemoryStream and Bitmap
            tmpDBElement.LoadAllImages(True, True)

            'Banner
            If Master.eSettings.MovieSetBannerAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainBanner) Then
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
            If Master.eSettings.MovieSetClearArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearArt) Then
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
            If Master.eSettings.MovieSetClearLogoAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainClearLogo) Then
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
            If Master.eSettings.MovieSetDiscArtAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainDiscArt) Then
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

            'Fanart
            If Master.eSettings.MovieSetFanartAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainFanart) Then
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
            If Master.eSettings.MovieSetLandscapeAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainLandscape) Then
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
            If Master.eSettings.MovieSetPosterAnyEnabled Then
                If Not ModulesManager.Instance.ScraperWithCapabilityAnyEnabled_Image_MovieSet(Enums.ModifierType.MainPoster) Then
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

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        SetInfo()
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

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        OK_Button.Enabled = isEnabled
        btnSearchMovie.Enabled = isEnabled
        btnMovieAdd.Enabled = isEnabled
        btnMovieDown.Enabled = isEnabled
        btnMovieRemove.Enabled = isEnabled
        btnMovieUp.Enabled = isEnabled
        btnRescrape.Enabled = isEnabled
        dgvMovies.Enabled = isEnabled
        lvMoviesInSet.Enabled = isEnabled

        Application.DoEvents()
    End Sub

    Private Sub SetInfo()
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False
        btnSearchMovie.Enabled = False
        btnRescrape.Enabled = False

        tmpDBElement.IsMark = chkMark.Checked
        tmpDBElement.SortMethod = DirectCast(cbMovieSorting.SelectedIndex, Enums.SortMethod_MovieSet)

        If Not String.IsNullOrEmpty(txtTitle.Text) Then
            tmpDBElement.ListTitle = StringUtils.SortTokens_MovieSet(txtTitle.Text.Trim)
            tmpDBElement.MovieSet.Title = txtTitle.Text.Trim
        End If

        tmpDBElement.MovieSet.TMDB = txtCollectionID.Text.Trim
        tmpDBElement.MovieSet.Plot = txtPlot.Text.Trim
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

        'Loacal Browse
        Dim strLocalBrowse As String = Master.eLang.GetString(78, "Local Browse")
        btnSetBannerLocal.Text = strLocalBrowse
        btnSetClearArtLocal.Text = strLocalBrowse
        btnSetClearLogoLocal.Text = strLocalBrowse
        btnSetDiscArtLocal.Text = strLocalBrowse
        btnSetFanartLocal.Text = strLocalBrowse
        btnSetLandscapeLocal.Text = strLocalBrowse
        btnSetPosterLocal.Text = strLocalBrowse

        'Remove
        Dim strRemove As String = Master.eLang.GetString(30, "Remove")
        btnRemoveBanner.Text = strRemove
        btnRemoveClearArt.Text = strRemove
        btnRemoveClearLogo.Text = strRemove
        btnRemoveDiscArt.Text = strRemove
        btnRemoveFanart.Text = strRemove
        btnRemoveLandscape.Text = strRemove
        btnRemovePoster.Text = strRemove

        'Scrape
        Dim strScrape As String = Master.eLang.GetString(79, "Scrape")
        btnSetBannerScrape.Text = strScrape
        btnSetClearArtScrape.Text = strScrape
        btnSetClearLogoScrape.Text = strScrape
        btnSetDiscArtScrape.Text = strScrape
        btnSetFanartScrape.Text = strScrape
        btnSetLandscapeScrape.Text = strScrape
        btnSetPosterScrape.Text = strScrape

        Dim mTitle As String = tmpDBElement.MovieSet.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(207, "Edit MovieSet"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
        Text = sTitle
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        btnRescrape.Text = Master.eLang.GetString(716, "Re-Scrape")
        btnSearchMovie.Text = Master.eLang.GetString(528, "Search Movie")
        chkMark.Text = Master.eLang.GetString(23, "Mark")
        lblCollectionID.Text = Master.eLang.GetString(1206, "Collection ID:")
        lblMovieSorting.Text = String.Concat(Master.eLang.GetString(665, "Movies sorted by"), ":")
        lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        lblTopDetails.Text = Master.eLang.GetString(1132, "Edit the details for the selected movieset.")
        lblTopTitle.Text = Master.eLang.GetString(207, "Edit MovieSet")
        tpBanner.Text = Master.eLang.GetString(838, "Banner")
        tpClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        tpClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        tpDetails.Text = Master.eLang.GetString(26, "Details")
        tpDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        tpFanart.Text = Master.eLang.GetString(149, "Fanart")
        tpLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        tpMovies.Text = Master.eLang.GetString(36, "Movies")
        tpPoster.Text = Master.eLang.GetString(148, "Poster")

        cbMovieSorting.Items.Clear()
        cbMovieSorting.Items.AddRange(New String() {Master.eLang.GetString(278, "Year"), Master.eLang.GetString(21, "Title")})
    End Sub

    Private Sub txtTitle_TextChanged(sender As Object, e As EventArgs) Handles txtTitle.TextChanged
        OK_Button.Enabled = Not String.IsNullOrEmpty(txtTitle.Text)
    End Sub

    Private Sub FillList()
        bsMovies.DataSource = Nothing
        dgvMovies.DataSource = Nothing

        Master.DB.FillDataTable(dtMovies, "SELECT * FROM movie ORDER BY ListTitle COLLATE NOCASE;")

        If dtMovies.Rows.Count > 0 Then
            With Me
                .bsMovies.DataSource = .dtMovies
                .dgvMovies.DataSource = .bsMovies

                For i As Integer = 0 To .dgvMovies.Columns.Count - 1
                    .dgvMovies.Columns(i).Visible = False
                Next

                .dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns("ListTitle").MinimumWidth = 83
                .dgvMovies.Columns("ListTitle").ReadOnly = True
                .dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                .dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                .dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                .dgvMovies.Columns("ListTitle").Visible = True

                .dgvMovies.Columns("idMovie").ValueType = GetType(Int32)

                .SetControlsEnabled(True)
            End With
        End If
    End Sub

    Private Sub RunFilter_Movies()
        If Visible Then

            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing

            If FilterArray.Count > 0 Then
                Dim FilterString As String = String.Empty

                FilterString = Microsoft.VisualBasic.Strings.Join(FilterArray.ToArray, " AND ")

                bsMovies.Filter = FilterString
            Else
                bsMovies.RemoveFilter()
            End If

            txtSearchMovies.Focus()
        End If
    End Sub

    Private Sub dgvMovies_Sorted(ByVal sender As Object, ByVal e As EventArgs) Handles dgvMovies.Sorted
        If dgvMovies.RowCount > 0 Then
            dgvMovies.CurrentCell = Nothing
            dgvMovies.ClearSelection()
            dgvMovies.Rows(0).Selected = True
            dgvMovies.CurrentCell = dgvMovies.Rows(0).Cells("ListTitle")
        End If
    End Sub

    Private Sub dgvMovies_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvMovies.KeyDown
        'stop enter key from selecting next list item
        e.Handled = (e.KeyCode = Keys.Enter)
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.S Then txtSearchMovies.Focus()
    End Sub

    Private Sub dgvMovies_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles dgvMovies.KeyPress
        If StringUtils.AlphaNumericOnly(e.KeyChar) OrElse e.KeyChar = Convert.ToChar(Keys.Space) Then
            KeyBuffer = String.Concat(KeyBuffer, e.KeyChar.ToString.ToLower)
            tmrKeyBuffer.Start()
            For Each drvRow As DataGridViewRow In dgvMovies.Rows
                If drvRow.Cells(3).Value.ToString.ToLower.StartsWith(KeyBuffer) Then
                    drvRow.Selected = True
                    dgvMovies.CurrentCell = drvRow.Cells(3)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub tmrKeyBuffer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrKeyBuffer.Tick
        tmrKeyBuffer.Enabled = False
        KeyBuffer = String.Empty
    End Sub

    Private Sub txtSearchMovies_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSearchMovies.KeyPress
        e.Handled = Not StringUtils.AlphaNumericOnly(e.KeyChar, True)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            dgvMovies.Focus()
        End If
    End Sub

    Private Sub txtSearchMovies_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearchMovies.TextChanged
        currTextSearch = txtSearchMovies.Text

        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        tmrSearchWait_Movies.Enabled = True
    End Sub

    Private Sub tmrSearch_Movies_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearch_Movies.Tick
        tmrSearchWait_Movies.Enabled = False
        tmrSearch_Movies.Enabled = False
        bDoingSearch = True

        If Not String.IsNullOrEmpty(txtSearchMovies.Text) Then
            FilterArray.Remove(filSearch)
            filSearch = String.Empty

            filSearch = String.Concat("Title LIKE '%", txtSearchMovies.Text, "%'")
            FilterArray.Add(filSearch)

            RunFilter_Movies()

        Else
            If Not String.IsNullOrEmpty(filSearch) Then
                FilterArray.Remove(filSearch)
                filSearch = String.Empty
            End If
            RunFilter_Movies()
        End If
    End Sub

    Private Sub tmrSearchWait_Movies_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrSearchWait_Movies.Tick
        tmrSearch_Movies.Enabled = False
        If prevTextSearch = currTextSearch Then
            tmrSearch_Movies.Enabled = True
        Else
            prevTextSearch = currTextSearch
        End If
    End Sub

    Private Sub dgvMovies_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMovies.CellPainting
        If Master.isWindows AndAlso e.RowIndex >= 0 AndAlso Not dgvMovies.Item(e.ColumnIndex, e.RowIndex).Displayed Then
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
        If dgvMovies.SelectedRows.Count > 0 Then
            btnMovieAdd.Enabled = True
        Else
            btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub dgvMovies_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMovies.SelectionChanged
        If dgvMovies.SelectedRows.Count > 0 Then
            btnMovieAdd.Enabled = True
        Else
            btnMovieAdd.Enabled = False
        End If
    End Sub

    Private Sub dgvMovies_DoubleClick(sender As Object, e As EventArgs) Handles dgvMovies.DoubleClick
        If dgvMovies.SelectedRows.Count = 1 Then
            AddMovieToSet()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class