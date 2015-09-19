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

Imports System.Text.RegularExpressions
Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class dlgTMDBSearchResults_Movie

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents bwDownloadPic As New System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrLoad As New System.Windows.Forms.Timer
    Friend WithEvents tmrWait As New System.Windows.Forms.Timer

    Private _TMDB As TMDB.Scraper
    Private sHTTP As New HTTP
    Private _currnode As Integer = -1
    Private _prevnode As Integer = -2
    Private _SpecialSettings As TMDB_Data.SpecialSettings

    Private _InfoCache As New Dictionary(Of String, MediaContainers.Movie)
    Private _PosterCache As New Dictionary(Of String, System.Drawing.Image)
    Private _filterOptions As Structures.ScrapeOptions

    Private _nMovie As MediaContainers.Movie

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal SpecialSettings As TMDB_Data.SpecialSettings, ByRef TMDB As TMDB.Scraper)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
        _SpecialSettings = SpecialSettings
        _TMDB = TMDB
    End Sub

    Public Overloads Function ShowDialog(ByRef nMovie As MediaContainers.Movie, ByVal sMovieTitle As String, ByVal sMovieFilename As String, ByVal filterOptions As Structures.ScrapeOptions, ByVal sMovieYear As String) As Windows.Forms.DialogResult
        Me.tmrWait.Enabled = False
        Me.tmrWait.Interval = 250
        Me.tmrLoad.Enabled = False
        Me.tmrLoad.Interval = 100

        _filterOptions = filterOptions
        _nMovie = nMovie

        Me.Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        Me.txtSearch.Text = sMovieTitle
        Me.txtFileName.Text = sMovieFilename
        Me.txtYear.Text = sMovieYear
        chkManual.Enabled = False

        _TMDB.SearchMovieAsync(sMovieTitle, _filterOptions, sMovieYear)

        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByRef nMovie As MediaContainers.Movie, ByVal Res As TMDB.SearchResults_Movie, ByVal sMovieTitle As String, ByVal sMovieFilename As String) As Windows.Forms.DialogResult
        Me.tmrWait.Enabled = False
        Me.tmrWait.Interval = 250
        Me.tmrLoad.Enabled = False
        Me.tmrLoad.Interval = 100

        _nMovie = nMovie

        Me.Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        Me.txtSearch.Text = sMovieTitle
        Me.txtFileName.Text = sMovieFilename
        SearchResultsDownloaded(Res)

        Return MyBase.ShowDialog()
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not String.IsNullOrEmpty(Me.txtSearch.Text) AndAlso (String.IsNullOrEmpty(Me.txtYear.Text) OrElse Integer.TryParse(Me.txtYear.Text, 0)) Then
            Me.OK_Button.Enabled = False
            pnlPicStatus.Visible = False
            _InfoCache.Clear()
            _PosterCache.Clear()
            Me.ClearInfo()
            Me.Label3.Text = Master.eLang.GetString(934, "Searching TMDB...")
            Me.pnlLoading.Visible = True
            chkManual.Enabled = False
            _TMDB.CancelAsync()

            _TMDB.SearchMovieAsync(Me.txtSearch.Text, _filterOptions, Me.txtYear.Text)
        End If
    End Sub

    Private Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        _TMDB.GetSearchMovieInfoAsync(Me.txtTMDBID.Text, _nMovie, pOpt)

    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Dim fPath As String = Directory.GetParent(Me.txtFileName.Text).FullName

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

        Me.pbPoster.Image = Res.Result
        If Not _PosterCache.ContainsKey(Res.IMDBId) Then
            _PosterCache.Add(Res.IMDBId, CType(Res.Result.Clone, Image))
        End If

        pnlPicStatus.Visible = False
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If _TMDB.bwTMDB.IsBusy Then
            _TMDB.CancelAsync()
        End If
        _nMovie.Clear()

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chkManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManual.CheckedChanged
        Me.ClearInfo()
        Me.OK_Button.Enabled = False
        Me.txtTMDBID.Enabled = Me.chkManual.Checked
        Me.btnVerify.Enabled = Me.chkManual.Checked
        Me.tvResults.Enabled = Not Me.chkManual.Checked

        If Not Me.chkManual.Checked Then
            txtTMDBID.Text = String.Empty
        End If
    End Sub

    Private Sub ClearInfo()
        Me.ControlsVisible(False)
        Me.lblTitle.Text = String.Empty
        Me.lblTagline.Text = String.Empty
        Me.lblYear.Text = String.Empty
        Me.lblDirector.Text = String.Empty
        Me.lblGenre.Text = String.Empty
        Me.txtPlot.Text = String.Empty
        Me.lblTMDBID.Text = String.Empty
        Me.pbPoster.Image = Nothing

        _nMovie.Clear()

        _TMDB.CancelAsync()
    End Sub

    Private Sub ControlsVisible(ByVal areVisible As Boolean)
        Me.lblYearHeader.Visible = areVisible
        Me.lblDirectorHeader.Visible = areVisible
        Me.lblGenreHeader.Visible = areVisible
        Me.lblPlotHeader.Visible = areVisible
        Me.lblTMDBHeader.Visible = areVisible
        Me.txtPlot.Visible = areVisible
        Me.lblYear.Visible = areVisible
        Me.lblTagline.Visible = areVisible
        Me.lblTitle.Visible = areVisible
        Me.lblDirector.Visible = areVisible
        Me.lblGenre.Visible = areVisible
        Me.lblTMDBID.Visible = areVisible
        Me.pbPoster.Visible = areVisible
    End Sub

    Private Sub dlgIMDBSearchResults_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub dlgTMDBSearchResults_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        pnlPicStatus.Visible = False

        AddHandler _TMDB.SearchInfoDownloaded_Movie, AddressOf SearchMovieInfoDownloaded
        AddHandler _TMDB.SearchResultsDownloaded_Movie, AddressOf SearchResultsDownloaded

        Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        Using g As Graphics = Graphics.FromImage(iBackground)
            g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
            Me.pnlTop.BackgroundImage = iBackground
        End Using
    End Sub

    Private Sub dlgTMDBSearchResults_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
        Me.tvResults.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean)
        Me.pnlLoading.Visible = False
        Me.OK_Button.Enabled = True

        If bSuccess Then
            Me.ControlsVisible(True)
            Me.lblTitle.Text = _nMovie.Title
            Me.lblTagline.Text = _nMovie.Tagline
            Me.lblYear.Text = _nMovie.Year
            Me.lblDirector.Text = _nMovie.Director
            Me.lblGenre.Text = _nMovie.Genre
            Me.txtPlot.Text = StringUtils.ShortenOutline(_nMovie.Plot, 410)
            Me.lblTMDBID.Text = _nMovie.TMDBID

            If _PosterCache.ContainsKey(_nMovie.TMDBID) Then
                'just set it
                Me.pbPoster.Image = _PosterCache(_nMovie.TMDBID)
            Else
                'go download it, if available
                If Not String.IsNullOrEmpty(sPoster) Then
                    If Me.bwDownloadPic.IsBusy Then
                        Me.bwDownloadPic.CancelAsync()
                    End If
                    pnlPicStatus.Visible = True
                    Me.bwDownloadPic = New System.ComponentModel.BackgroundWorker
                    Me.bwDownloadPic.WorkerSupportsCancellation = True
                    Me.bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = sPoster, .IMDBId = _nMovie.TMDBID})
                End If

            End If

            'store clone of tmpmovie
            If Not _InfoCache.ContainsKey(_nMovie.TMDBID) Then
                _InfoCache.Add(_nMovie.TMDBID, GetMovieClone(_nMovie))
            End If


            Me.btnVerify.Enabled = False
        Else
            If Me.chkManual.Checked Then
                MessageBox.Show(Master.eLang.GetString(935, "Unable to retrieve movie details for the entered TMDB ID. Please check your entry and try again."), Master.eLang.GetString(826, "Verification Failed"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.btnVerify.Enabled = True
            End If
        End If
    End Sub

    Private Sub SearchResultsDownloaded(ByVal M As TMDB.SearchResults_Movie)
        Me.tvResults.Nodes.Clear()
        Me.ClearInfo()
        If M IsNot Nothing AndAlso M.Matches.Count > 0 Then
            For Each Movie As MediaContainers.Movie In M.Matches
                Me.tvResults.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.TMDBID})
            Next
            Me.tvResults.SelectedNode = Me.tvResults.Nodes(0)

            Me._prevnode = -2

            Me.tvResults.Focus()
        Else
            Me.tvResults.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(833, "No Matches Found")})
        End If
        Me.pnlLoading.Visible = False
        chkManual.Enabled = True
    End Sub

    Private Function SetPreviewOptions() As Structures.ScrapeOptions
        Dim aOpt As New Structures.ScrapeOptions
        aOpt.bMainActors = False
        aOpt.bMainCert = False
        aOpt.bMainCollectionID = False
        aOpt.bMainCountry = False
        aOpt.bMainDirector = True
        aOpt.bMainFullCrew = False
        aOpt.bMainGenre = True
        aOpt.bMainMPAA = False
        aOpt.bMainMusicBy = False
        aOpt.bMainOtherCrew = False
        aOpt.bMainOutline = True
        aOpt.bMainPlot = True
        aOpt.bMainProducers = False
        aOpt.bMainRating = False
        aOpt.bMainRuntime = False
        aOpt.bMainStudio = False
        aOpt.bMainTagline = True
        aOpt.bMainTitle = True
        aOpt.bMainTop250 = False
        aOpt.bMainTrailer = False
        aOpt.bMainWriters = False
        aOpt.bMainYear = True

        Return aOpt
    End Function

    Private Sub SetUp()
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.Label2.Text = Master.eLang.GetString(836, "View details of each result to find the proper movie.")
        Me.Label1.Text = Master.eLang.GetString(846, "Movie Search Results")
        Me.chkManual.Text = Master.eLang.GetString(926, "Manual TMDB Entry:")
        Me.btnVerify.Text = Master.eLang.GetString(848, "Verify")
        Me.lblYearHeader.Text = Master.eLang.GetString(49, "Year:")
        Me.lblDirectorHeader.Text = Master.eLang.GetString(239, "Director:")
        Me.lblGenreHeader.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblTMDBHeader.Text = String.Concat(Master.eLang.GetString(933, "TMDB ID"), ":")
        Me.lblPlotHeader.Text = Master.eLang.GetString(242, "Plot Outline:")
        Me.Label3.Text = Master.eLang.GetString(934, "Searching TMDB...")
    End Sub

    Private Sub tmrLoad_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoad.Tick
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()

        Me.tmrWait.Stop()
        Me.tmrLoad.Stop()
        Me.pnlLoading.Visible = True
        Me.Label3.Text = Master.eLang.GetString(875, "Downloading details...")

        _TMDB.GetSearchMovieInfoAsync(Me.tvResults.SelectedNode.Tag.ToString, _nMovie, pOpt)
    End Sub

    Private Sub tmrWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrWait.Tick
        If Not Me._prevnode = Me._currnode Then
            Me._prevnode = Me._currnode
            Me.tmrWait.Stop()
            Me.tmrLoad.Start()
        Else
            Me.tmrLoad.Stop()
            Me.tmrWait.Stop()
        End If
    End Sub

    Private Sub tvResults_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvResults.AfterSelect
        Me.tmrWait.Stop()
        Me.tmrLoad.Stop()

        Me.ClearInfo()
        Me.OK_Button.Enabled = False

        If Me.tvResults.SelectedNode.Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.tvResults.SelectedNode.Tag.ToString) Then
            Me._currnode = Me.tvResults.SelectedNode.Index

            'check if this movie is in the cache already
            If _InfoCache.ContainsKey(Me.tvResults.SelectedNode.Tag.ToString) Then
                _nMovie = GetMovieClone(_InfoCache(Me.tvResults.SelectedNode.Tag.ToString))
                SearchMovieInfoDownloaded(String.Empty, True)
                Return
            End If

            Me.pnlLoading.Visible = True
            Me.tmrWait.Start()
        Else
            Me.pnlLoading.Visible = False
        End If
    End Sub

    Private Sub tvResults_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvResults.GotFocus
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub txtTMDBID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTMDBID.GotFocus
        Me.AcceptButton = Me.btnVerify
    End Sub

    Private Sub txtTMDBID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBID.TextChanged
        If Me.chkManual.Checked Then
            Me.btnVerify.Enabled = True
            Me.OK_Button.Enabled = False
        End If
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        Me.AcceptButton = Me.btnSearch
    End Sub

    Private Sub txtYear_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtYear.GotFocus
        Me.AcceptButton = Me.btnSearch
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