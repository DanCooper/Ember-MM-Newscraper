﻿' ################################################################################
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
Imports System.Diagnostics
Imports System.IO

Public Class dlgSearchResults

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadPic As New ComponentModel.BackgroundWorker
    Friend WithEvents tmrLoad As New Timer
    Friend WithEvents tmrWait As New Timer

    Private _TMDB As Scraper
    Private _HTTP As New HTTP
    Private _CurrNode As Integer = -1
    Private _PrevNode As Integer = -2
    Private _AddonSettings As AddonSettings

    Private _InfoCache As New Dictionary(Of String, MediaContainers.MainDetails)
    Private _PosterCache As New Dictionary(Of String, Image)
    Private _FilterOptions As Structures.ScrapeOptions

    Private _TmpMovie As New MediaContainers.MainDetails

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.MainDetails
        Get
            Return _TmpMovie
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New(ByVal AddonSettings As AddonSettings, ByRef TMDB As Scraper)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _AddonSettings = AddonSettings
        _TMDB = TMDB
    End Sub

    Public Overloads Function ShowDialog(ByVal sMovieTitle As String, ByVal sMovieFilename As String, ByVal filterOptions As Structures.ScrapeOptions, ByVal sMovieYear As Integer) As DialogResult
        tmrWait.Enabled = False
        tmrWait.Interval = 250
        tmrLoad.Enabled = False
        tmrLoad.Interval = 100

        _FilterOptions = filterOptions

        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        txtSearch.Text = sMovieTitle
        txtFileName.Text = sMovieFilename
        txtYear.Text = sMovieYear.ToString
        chkManual.Enabled = False

        _TMDB.SearchAsync_Movie(sMovieTitle, _FilterOptions, sMovieYear)

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal Res As SearchResults, ByVal sMovieTitle As String, ByVal sMovieFilename As String) As DialogResult
        tmrWait.Enabled = False
        tmrWait.Interval = 250
        tmrLoad.Enabled = False
        tmrLoad.Interval = 100

        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        txtSearch.Text = sMovieTitle
        txtFileName.Text = sMovieFilename
        SearchResultsDownloaded(Res)

        Return ShowDialog()
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim iYear As Integer
        If Not String.IsNullOrEmpty(txtSearch.Text) AndAlso (String.IsNullOrEmpty(txtYear.Text) OrElse Integer.TryParse(txtYear.Text, iYear)) Then
            btnOK.Enabled = False
            pnlPicStatus.Visible = False
            _InfoCache.Clear()
            _PosterCache.Clear()
            ClearInfo()
            lblSearching.Text = String.Concat(Master.eLang.GetString(758, "Searching"), " ...")
            pnlLoading.Visible = True
            chkManual.Enabled = False
            _TMDB.CancelAsync()

            _TMDB.SearchAsync_Movie(txtSearch.Text, _FilterOptions, iYear)
        End If
    End Sub

    Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        _TMDB.GetSearchMovieInfoAsync(txtTMDBID.Text, pOpt)
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Dim fPath As String = Directory.GetParent(txtFileName.Text).FullName

        If Not String.IsNullOrEmpty(fPath) Then
            Process.Start("Explorer.exe", fPath)
        End If
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        _HTTP.StartDownloadImage(Args.pURL)

        While _HTTP.IsDownloading
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        e.Result = New Results With {.Result = _HTTP.Image, .IMDBId = Args.TMDBId}
    End Sub

    Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPic.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)

        pbPoster.Image = Res.Result
        If Not _PosterCache.ContainsKey(Res.IMDBId) Then
            _PosterCache.Add(Res.IMDBId, CType(Res.Result.Clone, Image))
        End If

        pnlPicStatus.Visible = False
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If _TMDB.bwTMDB.IsBusy Then
            _TMDB.CancelAsync()
        End If

        _TmpMovie = New MediaContainers.MainDetails

        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub chkManual_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkManual.CheckedChanged
        ClearInfo()
        btnOK.Enabled = False
        txtTMDBID.Enabled = chkManual.Checked
        btnVerify.Enabled = chkManual.Checked
        tvResults.Enabled = Not chkManual.Checked

        If Not chkManual.Checked Then
            txtTMDBID.Text = String.Empty
        End If
    End Sub

    Private Sub ClearInfo()
        ControlsVisible(False)
        lblTitle.Text = String.Empty
        lblTagline.Text = String.Empty
        lblPremiered.Text = String.Empty
        lblDirectors.Text = String.Empty
        lblGenre.Text = String.Empty
        txtPlot.Text = String.Empty
        lblTMDBID.Text = String.Empty
        pbPoster.Image = Nothing

        _TmpMovie = New MediaContainers.MainDetails

        _TMDB.CancelAsync()
    End Sub

    Private Sub ControlsVisible(ByVal areVisible As Boolean)
        lblPremieredHeader.Visible = areVisible
        lblDirectorsHeader.Visible = areVisible
        lblGenreHeader.Visible = areVisible
        lblPlotHeader.Visible = areVisible
        lblTMDBHeader.Visible = areVisible
        txtPlot.Visible = areVisible
        lblPremiered.Visible = areVisible
        lblTagline.Visible = areVisible
        lblTitle.Visible = areVisible
        lblDirectors.Visible = areVisible
        lblGenre.Visible = areVisible
        lblTMDBID.Visible = areVisible
        pbPoster.Visible = areVisible
    End Sub

    Private Sub dlgIMDBSearchResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
        AcceptButton = btnOK
    End Sub

    Private Sub dlgTMDBSearchResults_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()
        pnlPicStatus.Visible = False

        AddHandler _TMDB.SearchInfoDownloaded_Movie, AddressOf SearchMovieInfoDownloaded
        AddHandler _TMDB.SearchResultsDownloaded_Movie, AddressOf SearchResultsDownloaded

        Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            pnlTop.BackgroundImage = iBackground
        End Using
    End Sub

    Private Sub dlgTMDBSearchResults_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        tvResults.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal sInfo As MediaContainers.MainDetails)
        pnlLoading.Visible = False
        btnOK.Enabled = True

        If sInfo IsNot Nothing Then
            ControlsVisible(True)
            _TmpMovie = sInfo
            lblTitle.Text = _TmpMovie.Title
            lblTagline.Text = _TmpMovie.Tagline
            lblPremiered.Text = _TmpMovie.Premiered
            lblDirectors.Text = String.Join(" / ", _TmpMovie.Directors.ToArray)
            lblGenre.Text = String.Join(" / ", _TmpMovie.Genres.ToArray)
            txtPlot.Text = StringUtils.ShortenOutline(_TmpMovie.Plot, 410)
            lblTMDBID.Text = _TmpMovie.UniqueIDs.TMDbId

            If _PosterCache.ContainsKey(_TmpMovie.UniqueIDs.TMDbId) Then
                'just set it
                pbPoster.Image = _PosterCache(_TmpMovie.UniqueIDs.TMDbId)
            Else
                'go download it, if available
                If Not String.IsNullOrEmpty(sPoster) Then
                    If bwDownloadPic.IsBusy Then
                        bwDownloadPic.CancelAsync()
                    End If
                    pnlPicStatus.Visible = True
                    bwDownloadPic = New System.ComponentModel.BackgroundWorker
                    bwDownloadPic.WorkerSupportsCancellation = True
                    bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = sPoster, .TMDBId = _TmpMovie.UniqueIDs.TMDbId})
                End If

            End If

            'store clone of tmpmovie
            If Not _InfoCache.ContainsKey(_TmpMovie.UniqueIDs.TMDbId) Then
                _InfoCache.Add(_TmpMovie.UniqueIDs.TMDbId, GetMovieClone(_TmpMovie))
            End If


            btnVerify.Enabled = False
        Else
            If chkManual.Checked Then
                MessageBox.Show(Master.eLang.GetString(825, "Unable to retrieve details for the entered ID. Please check your entry and try again."), Master.eLang.GetString(826, "Verification Failed"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                btnVerify.Enabled = True
            End If
        End If
    End Sub

    Private Sub SearchResultsDownloaded(ByVal M As SearchResults)
        tvResults.Nodes.Clear()
        ClearInfo()
        If M IsNot Nothing AndAlso M.Matches.Count > 0 Then
            For Each Movie As MediaContainers.MainDetails In M.Matches
                tvResults.Nodes.Add(New TreeNode() With {
                                    .Text = String.Concat(Movie.Title, If(Movie.PremieredSpecified, String.Format(" ({0})", StringUtils.GetYearFromString(Movie.Premiered)), String.Empty)),
                                    .Tag = Movie.UniqueIDs.TMDbId
                                    })
            Next
            tvResults.SelectedNode = tvResults.Nodes(0)

            _PrevNode = -2

            tvResults.Focus()
        Else
            tvResults.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(833, "No Matches Found")})
        End If
        pnlLoading.Visible = False
        chkManual.Enabled = True
    End Sub

    Private Function SetPreviewOptions() As Structures.ScrapeOptions
        Return New Structures.ScrapeOptions With {
            .Directors = True,
            .Genres = True,
            .Outline = True,
            .Plot = True,
            .Premiered = True,
            .Tagline = True,
            .Title = True
        }
    End Function

    Private Sub SetUp()
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        Label2.Text = Master.eLang.GetString(836, "View details of each result to find the proper movie.")
        Label1.Text = Master.eLang.GetString(846, "Movie Search Results")
        chkManual.Text = String.Concat(Master.eLang.GetString(847, "Manual ID Entry"), " (TMDb / IMDb):")
        btnVerify.Text = Master.eLang.GetString(848, "Verify")
        lblPremieredHeader.Text = String.Concat(Master.eLang.GetString(724, "Premiered"), ":")
        lblDirectorsHeader.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblGenreHeader.Text = Master.eLang.GetString(51, "Genre(s):")
        lblTMDBHeader.Text = String.Concat(Master.eLang.GetString(933, "TMDB ID"), ":")
        lblPlotHeader.Text = String.Concat(Master.eLang.GetString(64, "Plot Outline"), ":")
        lblSearching.Text = String.Concat(Master.eLang.GetString(758, "Searching"), " ...")
    End Sub

    Private Sub tmrLoad_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad.Tick
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()

        tmrWait.Stop()
        tmrLoad.Stop()
        pnlLoading.Visible = True
        lblSearching.Text = Master.eLang.GetString(875, "Downloading details...")

        _TMDB.GetSearchMovieInfoAsync(tvResults.SelectedNode.Tag.ToString, pOpt)
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait.Tick
        If Not _PrevNode = _CurrNode Then
            _PrevNode = _CurrNode
            tmrWait.Stop()
            tmrLoad.Start()
        Else
            tmrLoad.Stop()
            tmrWait.Stop()
        End If
    End Sub

    Private Sub tvResults_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvResults.AfterSelect
        tmrWait.Stop()
        tmrLoad.Stop()

        ClearInfo()
        btnOK.Enabled = False

        If tvResults.SelectedNode.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(tvResults.SelectedNode.Tag.ToString) Then
            _CurrNode = tvResults.SelectedNode.Index

            'check if this movie is in the cache already
            If _InfoCache.ContainsKey(tvResults.SelectedNode.Tag.ToString) Then
                _TmpMovie = GetMovieClone(_InfoCache(tvResults.SelectedNode.Tag.ToString))
                SearchMovieInfoDownloaded(String.Empty, _TmpMovie)
                Return
            End If

            pnlLoading.Visible = True
            tmrWait.Start()
        Else
            pnlLoading.Visible = False
        End If
    End Sub

    Private Sub tvResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles tvResults.GotFocus
        AcceptButton = btnOK
    End Sub

    Private Sub txtTMDBID_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtTMDBID.GotFocus
        AcceptButton = btnVerify
    End Sub

    Private Sub txtTMDBID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTMDBID.TextChanged
        If chkManual.Checked Then
            btnVerify.Enabled = True
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Sub txtYear_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Function GetMovieClone(ByVal original As MediaContainers.MainDetails) As MediaContainers.MainDetails
        Using mem As New IO.MemoryStream()
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(Nothing, New System.Runtime.Serialization.StreamingContext(Runtime.Serialization.StreamingContextStates.Clone))
            bin.Serialize(mem, original)
            mem.Seek(0, IO.SeekOrigin.Begin)
            Return DirectCast(bin.Deserialize(mem), MediaContainers.MainDetails)
        End Using

        Return Nothing
    End Function


#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim pURL As String
        Dim TMDBId As String

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim Result As Image
        Dim IMDBId As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class