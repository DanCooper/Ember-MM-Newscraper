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

    Private tmpDBMovieSet As New Structures.DBMovieSet

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

    Public ReadOnly Property Result As Structures.DBMovieSet
        Get
            Return tmpDBMovieSet
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

    Public Overloads Function ShowDialog(ByVal DBMovieSet As Structures.DBMovieSet) As DialogResult
        Me.tmpDBMovieSet = DBMovieSet
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
                Me.tmpDBMovieSet.MovieSet.TMDB = newColID
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
                Dim tmpMovie As New Structures.DBMovie
                tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(sRow.Cells(0).Value))
                If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                    If String.IsNullOrEmpty(Me.txtCollectionID.Text) AndAlso tmpMovie.Movie.TMDBColIDSpecified Then
                        Dim result As DialogResult = MessageBox.Show(String.Format(Master.eLang.GetString(1264, "Should the Collection ID of the movie ""{0}"" be used as ID for this Collection?"), tmpMovie.Movie.Title), Master.eLang.GetString(1263, "TMDB Collection ID found"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                        If result = Windows.Forms.DialogResult.Yes Then
                            Me.txtCollectionID.Text = tmpMovie.Movie.TMDBColID
                            Me.tmpDBMovieSet.MovieSet.TMDB = tmpMovie.Movie.TMDBColID
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
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieBanner = tImage.WebImage
                    Me.pbMovieBanner.Image = MovieBanner.Image
                    Me.pbMovieBanner.Tag = MovieBanner

                    Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
                    Me.lblMovieBannerSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieBannerScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainBanner, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.Banners.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainBanner, aContainer.Banners, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMovieBanner.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieBanner.Image.Width, Me.pbMovieBanner.Image.Height)
                            Me.lblMovieBannerSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MovieBanner = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1363, "No Banners found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieBannerLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieBannerLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMovieClearArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieClearArt = tImage.WebImage
                    Me.pbMovieClearArt.Image = MovieClearArt.Image
                    Me.pbMovieClearArt.Tag = MovieClearArt

                    Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
                    Me.lblMovieClearArtSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieClearArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearArt, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.ClearArts.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainClearArt, aContainer.ClearArts, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMovieClearArt.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearArt.Image.Width, Me.pbMovieClearArt.Image.Height)
                            Me.lblMovieClearArtSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MovieClearArt = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1102, "No ClearArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieClearArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearArtLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMovieClearLogoDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieClearLogo = tImage.WebImage
                    Me.pbMovieClearLogo.Image = MovieClearLogo.Image
                    Me.pbMovieClearLogo.Tag = MovieClearLogo

                    Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
                    Me.lblMovieClearLogoSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieClearLogoScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainClearLogo, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.ClearLogos.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainClearLogo, aContainer.ClearLogos, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMovieClearLogo.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieClearLogo.Image.Width, Me.pbMovieClearLogo.Image.Height)
                            Me.lblMovieClearLogoSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MovieClearLogo = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1103, "No ClearLogos found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieClearLogoLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieClearLogoLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMovieDiscArtDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieDiscArt = tImage.WebImage
                    Me.pbMovieDiscArt.Image = MovieDiscArt.Image
                    Me.pbMovieDiscArt.Tag = MovieDiscArt

                    Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
                    Me.lblMovieDiscArtSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieDiscArtScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainDiscArt, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.DiscArts.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainDiscArt, aContainer.DiscArts, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMovieDiscArt.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieDiscArt.Image.Width, Me.pbMovieDiscArt.Image.Height)
                            Me.lblMovieDiscArtSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MovieDiscArt = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(1104, "No DiscArts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieDiscArtLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieDiscArtLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMovieFanartDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieFanart = tImage.WebImage
                    Me.pbMovieFanart.Image = MovieFanart.Image
                    Me.pbMovieFanart.Tag = MovieFanart

                    Me.lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieFanart.Image.Width, Me.pbMovieFanart.Image.Height)
                    Me.lblMovieFanartSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieFanartScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartScrape.Click
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim pResults As New MediaContainers.Image
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainFanart, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.Fanarts.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainFanart, aContainer.Fanarts, efList, etList, True) = DialogResult.OK Then
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
                MessageBox.Show(Master.eLang.GetString(970, "No Fanarts found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieFanartLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieFanartLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMovieLandscapeDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MovieLandscape = tImage.WebImage
                    Me.pbMovieLandscape.Image = MovieLandscape.Image
                    Me.pbMovieLandscape.Tag = MovieLandscape

                    Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
                    Me.lblMovieLandscapeSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMovieLandscapeScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainLandscape, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.Landscapes.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainLandscape, aContainer.Landscapes, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMovieLandscape.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMovieLandscape.Image.Width, Me.pbMovieLandscape.Image.Height)
                            Me.lblMovieLandscapeSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MovieLandscape = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Landscapes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMovieLandscapeLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMovieLandscapeLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub btnSetMoviePosterDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterDL.Click
        Using dImgManual As New dlgImgManual
            Dim tImage As MediaContainers.Image
            If dImgManual.ShowDialog() = DialogResult.OK Then
                tImage = dImgManual.Results
                If tImage.WebImage.Image IsNot Nothing Then
                    MoviePoster = tImage.WebImage
                    Me.pbMoviePoster.Image = MoviePoster.Image
                    Me.pbMoviePoster.Tag = MoviePoster

                    Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
                    Me.lblMoviePosterSize.Visible = True
                End If
            End If
        End Using
    End Sub

    Private Sub btnSetMoviePosterScrape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterScrape.Click
        Dim pResults As New MediaContainers.Image
        Dim dlgImgS As dlgImgSelect
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)

        Dim ScrapeModifier As New Structures.ScrapeModifier
        Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.MainPoster, True)
        If Not ModulesManager.Instance.ScrapeImage_MovieSet(Me.tmpDBMovieSet, aContainer, ScrapeModifier) Then
            If aContainer.Posters.Count > 0 Then
                dlgImgS = New dlgImgSelect()
                If dlgImgS.ShowDialog(Me.tmpDBMovieSet, Enums.ModifierType.MainPoster, aContainer.Posters, efList, etList, True) = Windows.Forms.DialogResult.OK Then
                    pResults = dlgImgS.Results
                    If Not String.IsNullOrEmpty(pResults.URL) Then
                        Cursor = Cursors.WaitCursor
                        pResults.WebImage.FromWeb(pResults.URL)
                        If pResults.WebImage.Image IsNot Nothing Then
                            pbMoviePoster.Image = CType(pResults.WebImage.Image.Clone(), Image)
                            Me.lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), Me.pbMoviePoster.Image.Width, Me.pbMoviePoster.Image.Height)
                            Me.lblMoviePosterSize.Visible = True
                        End If
                        Cursor = Cursors.Default
                    End If
                    MoviePoster = pResults.WebImage
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(972, "No Posters found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnSetMoviePosterLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetMoviePosterLocal.Click
        With ofdImage
            '.InitialDirectory = Directory.GetParent(Me.tmpDBMovieSet.Filename).FullName
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
    End Sub

    Private Sub bwLoadMoviesInSet_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadMoviesInSet.DoWork
        MoviesInSet.Clear()

        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim tmpMovie As New Structures.DBMovie
            Dim iProg As Integer = 0
            If Not (Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieYAMJCompatibleSets) Then
                If Me.tmpDBMovieSet.SortMethod = Enums.SortMethod_MovieSet.Year Then
                    SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets INNER JOIN movie ON (MoviesSets.MovieID = movie.idMovie) ", _
                                                           "WHERE SetID = ", Me.tmpDBMovieSet.ID, " ORDER BY movie.Year;")
                ElseIf Me.tmpDBMovieSet.SortMethod = Enums.SortMethod_MovieSet.Title Then
                    SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets INNER JOIN movielist ON (MoviesSets.MovieID = movielist.idMovie) ", _
                                                           "WHERE SetID = ", Me.tmpDBMovieSet.ID, " ORDER BY movielist.SortedTitle COLLATE NOCASE;")
                End If
            Else
                SQLcommand.CommandText = String.Concat("SELECT MovieID, SetOrder FROM MoviesSets ", _
                                                       "WHERE SetID = ", Me.tmpDBMovieSet.ID, " ORDER BY SetOrder;")
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
        Me.tmpDBMovieSet = Master.DB.LoadMovieSetFromDB(Me.tmpDBMovieSet.ID)
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
        Me.pbMovieBanner.AllowDrop = True
        Me.pbMovieClearArt.AllowDrop = True
        Me.pbMovieClearLogo.AllowDrop = True
        Me.pbMovieDiscArt.AllowDrop = True
        Me.pbMovieFanart.AllowDrop = True
        Me.pbMovieLandscape.AllowDrop = True
        Me.pbMoviePoster.AllowDrop = True

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

        If Me.tmpDBMovieSet.ID = -1 Then
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

    Private Sub FillInfo(Optional ByVal DoAll As Boolean = True)
        With Me

            Me.cbMovieSorting.SelectedIndex = Me.tmpDBMovieSet.SortMethod

            Me.chkMark.Checked = Me.tmpDBMovieSet.IsMark

            If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.MovieSet.Title) Then
                .txtTitle.Text = Me.tmpDBMovieSet.MovieSet.Title
            End If

            If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.MovieSet.Plot) Then
                .txtPlot.Text = Me.tmpDBMovieSet.MovieSet.Plot
            End If

            If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.MovieSet.TMDB) Then
                .txtCollectionID.Text = Me.tmpDBMovieSet.MovieSet.TMDB
            End If

            If DoAll Then

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.BannerPath) AndAlso Me.tmpDBMovieSet.BannerPath.Substring(0, 1) = ":" Then
                    MovieBanner.FromWeb(Me.tmpDBMovieSet.BannerPath.Substring(1, Me.tmpDBMovieSet.BannerPath.Length - 1))
                Else
                    MovieBanner.FromFile(Me.tmpDBMovieSet.BannerPath)
                End If
                If MovieBanner.Image IsNot Nothing Then
                    .pbMovieBanner.Image = MovieBanner.Image
                    .pbMovieBanner.Tag = MovieBanner

                    .lblMovieBannerSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieBanner.Image.Width, .pbMovieBanner.Image.Height)
                    .lblMovieBannerSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.ClearArtPath) AndAlso Me.tmpDBMovieSet.ClearArtPath.Substring(0, 1) = ":" Then
                    MovieClearArt.FromWeb(Me.tmpDBMovieSet.ClearArtPath.Substring(1, Me.tmpDBMovieSet.ClearArtPath.Length - 1))
                Else
                    MovieClearArt.FromFile(Me.tmpDBMovieSet.ClearArtPath)
                End If
                If MovieClearArt.Image IsNot Nothing Then
                    .pbMovieClearArt.Image = MovieClearArt.Image
                    .pbMovieClearArt.Tag = MovieClearArt

                    .lblMovieClearArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearArt.Image.Width, .pbMovieClearArt.Image.Height)
                    .lblMovieClearArtSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.ClearLogoPath) AndAlso Me.tmpDBMovieSet.ClearLogoPath.Substring(0, 1) = ":" Then
                    MovieClearLogo.FromWeb(Me.tmpDBMovieSet.ClearLogoPath.Substring(1, Me.tmpDBMovieSet.ClearLogoPath.Length - 1))
                Else
                    MovieClearLogo.FromFile(Me.tmpDBMovieSet.ClearLogoPath)
                End If
                If MovieClearLogo.Image IsNot Nothing Then
                    .pbMovieClearLogo.Image = MovieClearLogo.Image
                    .pbMovieClearLogo.Tag = MovieClearLogo

                    .lblMovieClearLogoSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieClearLogo.Image.Width, .pbMovieClearLogo.Image.Height)
                    .lblMovieClearLogoSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.DiscArtPath) AndAlso Me.tmpDBMovieSet.DiscArtPath.Substring(0, 1) = ":" Then
                    MovieDiscArt.FromWeb(Me.tmpDBMovieSet.DiscArtPath.Substring(1, Me.tmpDBMovieSet.DiscArtPath.Length - 1))
                Else
                    MovieDiscArt.FromFile(Me.tmpDBMovieSet.DiscArtPath)
                End If
                If MovieDiscArt.Image IsNot Nothing Then
                    .pbMovieDiscArt.Image = MovieDiscArt.Image
                    .pbMovieDiscArt.Tag = MovieDiscArt

                    .lblMovieDiscArtSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieDiscArt.Image.Width, .pbMovieDiscArt.Image.Height)
                    .lblMovieDiscArtSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.FanartPath) AndAlso Me.tmpDBMovieSet.FanartPath.Substring(0, 1) = ":" Then
                    MovieFanart.FromWeb(Me.tmpDBMovieSet.FanartPath.Substring(1, Me.tmpDBMovieSet.FanartPath.Length - 1))
                Else
                    MovieFanart.FromFile(Me.tmpDBMovieSet.FanartPath)
                End If
                If MovieFanart.Image IsNot Nothing Then
                    .pbMovieFanart.Image = MovieFanart.Image
                    .pbMovieFanart.Tag = MovieFanart

                    .lblMovieFanartSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieFanart.Image.Width, .pbMovieFanart.Image.Height)
                    .lblMovieFanartSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.LandscapePath) AndAlso Me.tmpDBMovieSet.LandscapePath.Substring(0, 1) = ":" Then
                    MovieLandscape.FromWeb(Me.tmpDBMovieSet.LandscapePath.Substring(1, Me.tmpDBMovieSet.LandscapePath.Length - 1))
                Else
                    MovieLandscape.FromFile(Me.tmpDBMovieSet.LandscapePath)
                End If
                If MovieLandscape.Image IsNot Nothing Then
                    .pbMovieLandscape.Image = MovieLandscape.Image
                    .pbMovieLandscape.Tag = MovieLandscape

                    .lblMovieLandscapeSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMovieLandscape.Image.Width, .pbMovieLandscape.Image.Height)
                    .lblMovieLandscapeSize.Visible = True
                End If

                If Not String.IsNullOrEmpty(Me.tmpDBMovieSet.PosterPath) AndAlso Me.tmpDBMovieSet.PosterPath.Substring(0, 1) = ":" Then
                    MoviePoster.FromWeb(Me.tmpDBMovieSet.PosterPath.Substring(1, Me.tmpDBMovieSet.PosterPath.Length - 1))
                Else
                    MoviePoster.FromFile(Me.tmpDBMovieSet.PosterPath)
                End If
                If MoviePoster.Image IsNot Nothing Then
                    .pbMoviePoster.Image = MoviePoster.Image
                    .pbMoviePoster.Tag = MoviePoster

                    .lblMoviePosterSize.Text = String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), .pbMoviePoster.Image.Width, .pbMoviePoster.Image.Height)
                    .lblMoviePosterSize.Visible = True
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainBanner) Then
                    .btnSetMovieBannerScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainClearArt) Then
                    .btnSetMovieClearArtScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainClearLogo) Then
                    .btnSetMovieClearLogoScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainDiscArt) Then
                    .btnSetMovieDiscArtScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainFanart) Then
                    .btnSetMovieFanartScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainLandscape) Then
                    .btnSetMovieLandscapeScrape.Enabled = False
                End If

                If Not ModulesManager.Instance.QueryScraperCapabilities_Image_MovieSet(Enums.ModifierType.MainPoster) Then
                    .btnSetMoviePosterScrape.Enabled = False
                End If
            End If

            If String.IsNullOrEmpty(Master.eSettings.MovieSetPathMSAA) Then
                tcEditMovie.TabPages.Remove(tpBanner)
                tcEditMovie.TabPages.Remove(tpClearArt)
                tcEditMovie.TabPages.Remove(tpClearLogo)
                tcEditMovie.TabPages.Remove(tpDiscArt)
                tcEditMovie.TabPages.Remove(tpFanart)
                tcEditMovie.TabPages.Remove(tpLandscape)
                tcEditMovie.TabPages.Remove(tpPoster)
            Else
                If Not Master.eSettings.MovieSetBannerAnyEnabled Then tcEditMovie.TabPages.Remove(tpBanner)
                If Not Master.eSettings.MovieSetClearArtAnyEnabled Then tcEditMovie.TabPages.Remove(tpClearArt)
                If Not Master.eSettings.MovieSetClearLogoAnyEnabled Then tcEditMovie.TabPages.Remove(tpClearLogo)
                If Not Master.eSettings.MovieSetDiscArtAnyEnabled Then tcEditMovie.TabPages.Remove(tpDiscArt)
                If Not Master.eSettings.MovieSetFanartAnyEnabled Then tcEditMovie.TabPages.Remove(tpFanart)
                If Not Master.eSettings.MovieSetLandscapeAnyEnabled Then tcEditMovie.TabPages.Remove(tpLandscape)
                If Not Master.eSettings.MovieSetPosterAnyEnabled Then tcEditMovie.TabPages.Remove(tpPoster)
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

    Private Sub pbMovieBanner_DragDrop(sender As Object, e As DragEventArgs) Handles pbMovieBanner.DragDrop
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieBanner = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieClearArt = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieClearLogo = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieDiscArt = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieFanart = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MovieLandscape = tImage.WebImage
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
        Dim tImage As MediaContainers.Image = FileUtils.DragAndDrop.GetDoppedImage(e)
        If tImage.WebImage.Image IsNot Nothing Then
            MoviePoster = tImage.WebImage
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
        Me.SetControlsEnabled(False)

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tMovie As MovieInSet In MoviesInSet
                'If Not Master.eSettings.MovieYAMJCompatibleSets Then
                '    tMovie.DBMovie.Movie.AddSet(mSet.Set, 0)
                'Else
                tMovie.DBMovie.Movie.AddSet(Me.tmpDBMovieSet.ID, Me.tmpDBMovieSet.MovieSet.Title, tMovie.Order, Me.tmpDBMovieSet.MovieSet.TMDB)
                'End If
                Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True)
            Next
            SQLtransaction.Commit()
        End Using

        Me.SetControlsEnabled(True)
    End Sub

    Private Sub RemoveSetFromMovies()
        Me.SetControlsEnabled(False)

        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            For Each tMovie As MovieInSet In MoviesToRemove
                tMovie.DBMovie.Movie.RemoveSet(Me.tmpDBMovieSet.ID)
                Master.DB.SaveMovieToDB(tMovie.DBMovie, False, True, True)
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
                If Me.tmpDBMovieSet.NfoPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.tmpDBMovieSet.NfoPath) Then
                    File.Delete(Me.tmpDBMovieSet.NfoPath)
                End If
            End If

            If Me.tmpDBMovieSet.RemoveBanner OrElse needsMovieUpdate Then
                Images.DeleteMovieSetBanner(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemoveClearArt OrElse needsMovieUpdate Then
                Images.DeleteMovieSetClearArt(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemoveClearLogo OrElse needsMovieUpdate Then
                Images.DeleteMovieSetClearLogo(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemoveDiscArt OrElse needsMovieUpdate Then
                Images.DeleteMovieSetDiscArt(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemoveFanart OrElse needsMovieUpdate Then
                Images.DeleteMovieSetFanart(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemoveLandscape OrElse needsMovieUpdate Then
                Images.DeleteMovieSetLandscape(Me.tmpDBMovieSet)
            End If

            If Me.tmpDBMovieSet.RemovePoster OrElse needsMovieUpdate Then
                Images.DeleteMovieSetPoster(Me.tmpDBMovieSet)
            End If

            Me.tmpDBMovieSet.IsMark = Me.chkMark.Checked
            Me.tmpDBMovieSet.SortMethod = DirectCast(Me.cbMovieSorting.SelectedIndex, Enums.SortMethod_MovieSet)

            If Not String.IsNullOrEmpty(.txtTitle.Text) Then
                Me.tmpDBMovieSet.ListTitle = StringUtils.SortTokens_MovieSet(.txtTitle.Text.Trim)
                Me.tmpDBMovieSet.MovieSet.Title = .txtTitle.Text.Trim
            End If

            Me.tmpDBMovieSet.MovieSet.TMDB = .txtCollectionID.Text.Trim
            Me.tmpDBMovieSet.MovieSet.Plot = .txtPlot.Text.Trim

            If .MovieBanner.Image IsNot Nothing Then
                Dim fPath As String = .MovieBanner.SaveAsMovieSetBanner(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.BannerPath = fPath
            Else
                Images.DeleteMovieSetBanner(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.BannerPath = String.Empty
            End If

            If .MovieClearArt.Image IsNot Nothing Then
                Dim fPath As String = .MovieClearArt.SaveAsMovieSetClearArt(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.ClearArtPath = fPath
            Else
                Images.DeleteMovieSetClearArt(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.ClearArtPath = String.Empty
            End If

            If .MovieClearLogo.Image IsNot Nothing Then
                Dim fPath As String = .MovieClearLogo.SaveAsMovieSetClearLogo(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.ClearLogoPath = fPath
            Else
                Images.DeleteMovieSetClearLogo(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.ClearLogoPath = String.Empty
            End If

            If .MovieDiscArt.Image IsNot Nothing Then
                Dim fPath As String = .MovieDiscArt.SaveAsMovieSetDiscArt(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.DiscArtPath = fPath
            Else
                Images.DeleteMovieSetDiscArt(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.DiscArtPath = String.Empty
            End If

            If .MovieFanart.Image IsNot Nothing Then
                Dim fPath As String = .MovieFanart.SaveAsMovieSetFanart(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.FanartPath = fPath
            Else
                Images.DeleteMovieSetFanart(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.FanartPath = String.Empty
            End If

            If .MovieLandscape.Image IsNot Nothing Then
                Dim fPath As String = .MovieLandscape.SaveAsMovieSetLandscape(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.LandscapePath = fPath
            Else
                Images.DeleteMovieSetLandscape(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.LandscapePath = String.Empty
            End If

            If .MoviePoster.Image IsNot Nothing Then
                Dim pPath As String = .MoviePoster.SaveAsMovieSetPoster(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.PosterPath = pPath
            Else
                Images.DeleteMovieSetPoster(Me.tmpDBMovieSet)
                Me.tmpDBMovieSet.PosterPath = String.Empty
            End If
        End With
    End Sub

    Private Sub SetUp()
        Dim mTitle As String = Me.tmpDBMovieSet.MovieSet.Title
        Dim sTitle As String = String.Concat(Master.eLang.GetString(1131, "Edit MovieSet"), If(String.IsNullOrEmpty(mTitle), String.Empty, String.Concat(" - ", mTitle)))
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
        Me.btnSearchMovie.Text = Master.eLang.GetString(528, "Search Movie")
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