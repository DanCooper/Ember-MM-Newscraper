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
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class dlgTMDBSearchResults_Movie

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadPic As New System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrLoad As New Timer
    Friend WithEvents tmrWait As New Timer

    Private _TMDB As TMDB.Scraper
    Private sHTTP As New HTTP
    Private _currnode As Integer = -1
    Private _prevnode As Integer = -2
    Private _SpecialSettings As TMDB_Data.SpecialSettings

    Private _InfoCache As New Dictionary(Of String, MediaContainers.Movie)
    Private _PosterCache As New Dictionary(Of String, Image)
    Private _filterOptions As Structures.ScrapeOptions

    Private _tmpMovie As New MediaContainers.Movie

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.Movie
        Get
            Return _tmpMovie
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New(ByVal SpecialSettings As TMDB_Data.SpecialSettings, ByRef TMDB As TMDB.Scraper)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _SpecialSettings = SpecialSettings
        _TMDB = TMDB
    End Sub

    Public Overloads Function ShowDialog(ByVal sMovieTitle As String, ByVal sMovieFilename As String, ByVal filterOptions As Structures.ScrapeOptions, ByVal sMovieYear As String) As DialogResult
        tmrWait.Enabled = False
        tmrWait.Interval = 250
        tmrLoad.Enabled = False
        tmrLoad.Interval = 100

        _filterOptions = filterOptions

        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        txtSearch.Text = sMovieTitle
        txtFileName.Text = sMovieFilename
        txtYear.Text = sMovieYear
        chkManual.Enabled = False

        _TMDB.SearchMovieAsync(sMovieTitle, _filterOptions, sMovieYear)

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal Res As TMDB.SearchResults_Movie, ByVal sMovieTitle As String, ByVal sMovieFilename As String) As DialogResult
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
        If Not String.IsNullOrEmpty(txtSearch.Text) AndAlso (String.IsNullOrEmpty(txtYear.Text) OrElse Integer.TryParse(txtYear.Text, 0)) Then
            OK_Button.Enabled = False
            pnlPicStatus.Visible = False
            _InfoCache.Clear()
            _PosterCache.Clear()
            ClearInfo()
            Label3.Text = Master.eLang.GetString(934, "Searching TMDB...")
            pnlLoading.Visible = True
            chkManual.Enabled = False
            _TMDB.CancelAsync()

            _TMDB.SearchMovieAsync(txtSearch.Text, _filterOptions, txtYear.Text)
        End If
    End Sub

    Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        _TMDB.GetSearchMovieInfoAsync(txtTMDBID.Text, _tmpMovie, pOpt)
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Dim fPath As String = Directory.GetParent(txtFileName.Text).FullName

        If Not String.IsNullOrEmpty(fPath) Then
            Process.Start("Explorer.exe", fPath)
        End If
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        sHTTP.StartDownloadImage(Args.pURL)

        While sHTTP.IsDownloading
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        e.Result = New Results With {.Result = sHTTP.Image, .IMDBId = Args.IMDBId}
    End Sub

    Private Sub bwDownloadPic_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPic.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)

        pbPoster.Image = Res.Result
        If Not _PosterCache.ContainsKey(Res.IMDBId) Then
            _PosterCache.Add(Res.IMDBId, CType(Res.Result.Clone, Image))
        End If

        pnlPicStatus.Visible = False
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        If _TMDB.bwTMDB.IsBusy Then
            _TMDB.CancelAsync()
        End If

        _tmpMovie = Nothing

        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub chkManual_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkManual.CheckedChanged
        ClearInfo()
        OK_Button.Enabled = False
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
        lblYear.Text = String.Empty
        lblDirectors.Text = String.Empty
        lblGenre.Text = String.Empty
        txtPlot.Text = String.Empty
        lblTMDBID.Text = String.Empty
        pbPoster.Image = Nothing

        _tmpMovie.Clear()

        _TMDB.CancelAsync()
    End Sub

    Private Sub ControlsVisible(ByVal areVisible As Boolean)
        lblYearHeader.Visible = areVisible
        lblDirectorsHeader.Visible = areVisible
        lblGenreHeader.Visible = areVisible
        lblPlotHeader.Visible = areVisible
        lblTMDBHeader.Visible = areVisible
        txtPlot.Visible = areVisible
        lblYear.Visible = areVisible
        lblTagline.Visible = areVisible
        lblTitle.Visible = areVisible
        lblDirectors.Visible = areVisible
        lblGenre.Visible = areVisible
        lblTMDBID.Visible = areVisible
        pbPoster.Visible = areVisible
    End Sub

    Private Sub dlgIMDBSearchResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
        AcceptButton = OK_Button
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

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal sInfo As MediaContainers.Movie)
        pnlLoading.Visible = False
        OK_Button.Enabled = True

        If sInfo IsNot Nothing Then
            ControlsVisible(True)
            _tmpMovie = sInfo
            lblTitle.Text = _tmpMovie.Title
            lblTagline.Text = _tmpMovie.Tagline
            lblYear.Text = _tmpMovie.Year
            lblDirectors.Text = String.Join(" / ", _tmpMovie.Directors.ToArray)
            lblGenre.Text = String.Join(" / ", _tmpMovie.Genres.ToArray)
            txtPlot.Text = StringUtils.ShortenOutline(_tmpMovie.Plot, 410)
            lblTMDBID.Text = _tmpMovie.TMDB

            If _PosterCache.ContainsKey(_tmpMovie.TMDB) Then
                'just set it
                pbPoster.Image = _PosterCache(_tmpMovie.TMDB)
            Else
                'go download it, if available
                If Not String.IsNullOrEmpty(sPoster) Then
                    If bwDownloadPic.IsBusy Then
                        bwDownloadPic.CancelAsync()
                    End If
                    pnlPicStatus.Visible = True
                    bwDownloadPic = New System.ComponentModel.BackgroundWorker
                    bwDownloadPic.WorkerSupportsCancellation = True
                    bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = sPoster, .IMDBId = _tmpMovie.TMDB})
                End If

            End If

            'store clone of tmpmovie
            If Not _InfoCache.ContainsKey(_tmpMovie.TMDB) Then
                _InfoCache.Add(_tmpMovie.TMDB, GetMovieClone(_tmpMovie))
            End If


            btnVerify.Enabled = False
        Else
            If chkManual.Checked Then
                MessageBox.Show(Master.eLang.GetString(935, "Unable to retrieve movie details for the entered TMDB ID. Please check your entry and try again."), Master.eLang.GetString(826, "Verification Failed"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                btnVerify.Enabled = True
            End If
        End If
    End Sub

    Private Sub SearchResultsDownloaded(ByVal M As TMDB.SearchResults_Movie)
        tvResults.Nodes.Clear()
        ClearInfo()
        If M IsNot Nothing AndAlso M.Matches.Count > 0 Then
            For Each Movie As MediaContainers.Movie In M.Matches
                tvResults.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.TMDB})
            Next
            tvResults.SelectedNode = tvResults.Nodes(0)

            _prevnode = -2

            tvResults.Focus()
        Else
            tvResults.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(833, "No Matches Found")})
        End If
        pnlLoading.Visible = False
        chkManual.Enabled = True
    End Sub

    Private Function SetPreviewOptions() As Structures.ScrapeOptions
        Dim aOpt As New Structures.ScrapeOptions
        aOpt.bMainDirectors = True
        aOpt.bMainGenres = True
        aOpt.bMainOutline = True
        aOpt.bMainPlot = True
        aOpt.bMainTagline = True
        aOpt.bMainTitle = True
        aOpt.bMainYear = True

        Return aOpt
    End Function

    Private Sub SetUp()
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Label2.Text = Master.eLang.GetString(836, "View details of each result to find the proper movie.")
        Label1.Text = Master.eLang.GetString(846, "Movie Search Results")
        chkManual.Text = Master.eLang.GetString(926, "Manual TMDB Entry:")
        btnVerify.Text = Master.eLang.GetString(848, "Verify")
        lblYearHeader.Text = Master.eLang.GetString(49, "Year:")
        lblDirectorsHeader.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblGenreHeader.Text = Master.eLang.GetString(51, "Genre(s):")
        lblTMDBHeader.Text = String.Concat(Master.eLang.GetString(933, "TMDB ID"), ":")
        lblPlotHeader.Text = Master.eLang.GetString(242, "Plot Outline:")
        Label3.Text = Master.eLang.GetString(934, "Searching TMDB...")
    End Sub

    Private Sub tmrLoad_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad.Tick
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()

        tmrWait.Stop()
        tmrLoad.Stop()
        pnlLoading.Visible = True
        Label3.Text = Master.eLang.GetString(875, "Downloading details...")

        _TMDB.GetSearchMovieInfoAsync(tvResults.SelectedNode.Tag.ToString, _tmpMovie, pOpt)
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrWait.Tick
        If Not _prevnode = _currnode Then
            _prevnode = _currnode
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
        OK_Button.Enabled = False

        If tvResults.SelectedNode.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(tvResults.SelectedNode.Tag.ToString) Then
            _currnode = tvResults.SelectedNode.Index

            'check if this movie is in the cache already
            If _InfoCache.ContainsKey(tvResults.SelectedNode.Tag.ToString) Then
                _tmpMovie = GetMovieClone(_InfoCache(tvResults.SelectedNode.Tag.ToString))
                SearchMovieInfoDownloaded(String.Empty, _tmpMovie)
                Return
            End If

            pnlLoading.Visible = True
            tmrWait.Start()
        Else
            pnlLoading.Visible = False
        End If
    End Sub

    Private Sub tvResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles tvResults.GotFocus
        AcceptButton = OK_Button
    End Sub

    Private Sub txtTMDBID_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtTMDBID.GotFocus
        AcceptButton = btnVerify
    End Sub

    Private Sub txtTMDBID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTMDBID.TextChanged
        If chkManual.Checked Then
            btnVerify.Enabled = True
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Sub txtYear_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Function GetMovieClone(ByVal original As MediaContainers.Movie) As MediaContainers.Movie
        Using mem As New IO.MemoryStream()
            Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(Nothing, New System.Runtime.Serialization.StreamingContext(Runtime.Serialization.StreamingContextStates.Clone))
            bin.Serialize(mem, original)
            mem.Seek(0, IO.SeekOrigin.Begin)
            Return DirectCast(bin.Deserialize(mem), MediaContainers.Movie)
        End Using

        Return Nothing
    End Function


#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim pURL As String
        Dim IMDBId As String

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