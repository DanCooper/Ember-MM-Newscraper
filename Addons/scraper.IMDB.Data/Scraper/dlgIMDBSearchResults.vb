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
Imports EmberAPI.Interfaces

Public Class dlgIMDBSearchResults

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents tmrLoad As New System.Windows.Forms.Timer
    Friend WithEvents tmrWait As New System.Windows.Forms.Timer

    Private IMDB As New IMDB.Scraper
    Private sHTTP As New HTTP
    Dim UseOFDBGenre As Boolean
    Dim UseOFDBOutline As Boolean
    Dim UseOFDBPlot As Boolean
    Dim UseOFDBTitle As Boolean
    Private _currnode As Integer = -1
    Private _prevnode As Integer = -2

    Private _InfoCache As New Dictionary(Of String, MediaContainers.Movie)
    Private _PosterCache As New Dictionary(Of String, System.Drawing.Image)
    Private _filterOptions As Structures.ScrapeOptions_Movie

    Public _nMovie As MediaContainers.Movie

#End Region 'Fields

#Region "Methods"

    Public Overloads Async Function ShowDialog(ByVal nMovie As MediaContainers.Movie, ByVal sMovieTitle As String, ByVal sMovieFilename As String, ByVal filterOptions As Structures.ScrapeOptions_Movie) As Threading.Tasks.Task(Of Windows.Forms.DialogResult)
        Me.tmrWait.Enabled = False
        Me.tmrWait.Interval = 250
        Me.tmrLoad.Enabled = False

        Me.tmrLoad.Interval = 100

        _filterOptions = filterOptions
        _nMovie = nMovie

        Me.Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sMovieTitle)
        Me.txtSearch.Text = sMovieTitle
        Me.txtFileName.Text = sMovieFilename

        ' fix for Enhancement #91
        'chkManual.Enabled = False
        chkManual.Enabled = True

        Await IMDB.SearchMovieAsync(sMovieTitle, _filterOptions)

        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByRef nMovie As MediaContainers.Movie, ByVal Res As IMDB.MovieSearchResults, ByVal sMovieTitle As String, ByVal sMovieFilename As String) As Windows.Forms.DialogResult
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

    Private Async Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Not String.IsNullOrEmpty(Me.txtSearch.Text) Then
            Me.OK_Button.Enabled = False
            pnlPicStatus.Visible = False
            _InfoCache.Clear()
            _PosterCache.Clear()
            Me.ClearInfo()
            Me.Label3.Text = Master.eLang.GetString(798, "Searching IMDB...")
            Me.pnlLoading.Visible = True

            chkManual.Enabled = False

            IMDB.CancelAsync()
            Await IMDB.SearchMovieAsync(Me.txtSearch.Text, _filterOptions)
        End If
    End Sub

    Private Async Sub btnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerify.Click
        Dim pOpt As New Structures.ScrapeOptions_Movie
        Dim ret As New ModuleResult
        pOpt = SetPreviewOptions()
        If Regex.IsMatch(Me.txtIMDBID.Text.Replace("tt", String.Empty), "\d\d\d\d\d\d\d") Then
            Me.pnlLoading.Visible = True
            IMDB.CancelAsync()
            ret = Await IMDB.GetSearchMovieInfoAsync(Me.txtIMDBID.Text.Replace("tt", String.Empty), _nMovie, pOpt)
            ' return object
            ' nMovie
            _nMovie = CType(ret.ReturnObj(0), MediaContainers.Movie)
        Else
            MsgBox(Master.eLang.GetString(799, "The ID you entered is not a valid IMDB ID."), MsgBoxStyle.Exclamation, Master.eLang.GetString(292, "Invalid Entry"))
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        'If IMDB.bwIMDB.IsBusy Then
        IMDB.CancelAsync()
        ' End If

        _nMovie.Clear()

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Dim fPath As String = Directory.GetParent(Me.txtFileName.Text).FullName

        If Not String.IsNullOrEmpty(fPath) Then
            Shell("Explorer.exe " & fPath, vbNormalFocus)
        End If
    End Sub

    Private Sub chkManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManual.CheckedChanged
        Me.ClearInfo()
        If Me.chkManual.Enabled Then
            Me.pnlLoading.Visible = False
            IMDB.CancelAsync()
        End If
        Me.OK_Button.Enabled = False
        Me.txtIMDBID.Enabled = Me.chkManual.Checked
        Me.btnVerify.Enabled = Me.chkManual.Checked
        Me.tvResults.Enabled = Not Me.chkManual.Checked

        If Not Me.chkManual.Checked Then
            txtIMDBID.Text = String.Empty
        End If
    End Sub

    Private Sub ClearInfo()
        Me.ControlsVisible(False)
        Me.lblTitle.Text = String.Empty
        Me.lblTagline.Text = String.Empty
        Me.lblYear.Text = String.Empty
        Me.lblDirector.Text = String.Empty
        Me.lblGenre.Text = String.Empty
        Me.txtOutline.Text = String.Empty
        Me.lblIMDBID.Text = String.Empty
        Me.pbPoster.Image = Nothing

        _nMovie.Clear()

        IMDB.CancelAsync()
    End Sub

    Private Sub ControlsVisible(ByVal areVisible As Boolean)
        Me.lblYearHeader.Visible = areVisible
        Me.lblDirectorHeader.Visible = areVisible
        Me.lblGenreHeader.Visible = areVisible
        Me.lblPlotHeader.Visible = areVisible
        Me.lblIMDBHeader.Visible = areVisible
        Me.txtOutline.Visible = areVisible
        Me.lblYear.Visible = areVisible
        Me.lblTagline.Visible = areVisible
        Me.lblTitle.Visible = areVisible
        Me.lblDirector.Visible = areVisible
        Me.lblGenre.Visible = areVisible
        Me.lblIMDBID.Visible = areVisible
        Me.pbPoster.Visible = areVisible
    End Sub

    Private Sub dlgIMDBSearchResults_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub dlgIMDBSearchResults_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        pnlPicStatus.Visible = False
        AddHandler IMDB.SearchMovieInfoDownloaded, AddressOf SearchMovieInfoDownloaded
        AddHandler IMDB.SearchResultsDownloaded, AddressOf SearchResultsDownloaded

        Try
            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgIMDBSearchResults_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
        Me.tvResults.Focus()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Me.chkManual.Checked AndAlso Me.btnVerify.Enabled Then
                If Not Regex.IsMatch(Me.txtIMDBID.Text.Replace("tt", String.Empty), "\d\d\d\d\d\d\d") Then
                    MsgBox(Master.eLang.GetString(799, "The ID you entered is not a valid IMDB ID."), MsgBoxStyle.Exclamation, Master.eLang.GetString(292, "Invalid Entry"))
                    Exit Sub
                Else
                    If MsgBox(String.Concat(Master.eLang.GetString(821, "You have manually entered an IMDB ID but have not verified it is correct."), vbNewLine, vbNewLine, Master.eLang.GetString(101, "Are you sure you want to continue?")), MsgBoxStyle.YesNo, Master.eLang.GetString(823, "Continue without verification?")) = MsgBoxResult.No Then
                        Exit Sub
                    Else
                        _nMovie.IMDBID = Me.txtIMDBID.Text.Replace("tt", String.Empty)
                    End If
                End If
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.Close()
    End Sub

    Private Async Function SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean) As Threading.Tasks.Task
        '//
        ' Info downloaded... fill form with data
        '\\

        Me.pnlLoading.Visible = False
        Me.OK_Button.Enabled = True

        Try
            If bSuccess Then
                Me.ControlsVisible(True)
                Me.lblTitle.Text = _nMovie.Title
                Me.lblTagline.Text = _nMovie.Tagline
                Me.lblYear.Text = _nMovie.Year
                Me.lblDirector.Text = _nMovie.Director
                Me.lblGenre.Text = _nMovie.Genre
                Me.txtOutline.Text = _nMovie.Outline
                Me.lblIMDBID.Text = _nMovie.IMDBID

                If _PosterCache.ContainsKey(_nMovie.IMDBID) Then
                    'just set it
                    Me.pbPoster.Image = _PosterCache(_nMovie.IMDBID)
                Else
                    'go download it, if available
                    If Not String.IsNullOrEmpty(sPoster) Then
                        'If Me.bwDownloadPic.IsBusy Then
                        '    Me.bwDownloadPic.CancelAsync()
                        'End If
                        pnlPicStatus.Visible = True

                        Await sHTTP.DownloadImage(sPoster)

                        Me.pbPoster.Image = sHTTP.Image
                        If Not _PosterCache.ContainsKey(_nMovie.IMDBID) Then
                            _PosterCache.Add(_nMovie.IMDBID, CType(sHTTP.Image, Image))
                        End If
                        pnlPicStatus.Visible = False

                    End If

                End If

                'store clone of tmpmovie
                If Not _InfoCache.ContainsKey(_nMovie.IMDBID) Then
                    _InfoCache.Add(_nMovie.IMDBID, GetMovieClone(_nMovie))
                End If


                Me.btnVerify.Enabled = False
            Else
                If Me.chkManual.Checked Then
                    MsgBox(Master.eLang.GetString(825, "Unable to retrieve movie details for the entered IMDB ID. Please check your entry and try again."), MsgBoxStyle.Exclamation, Master.eLang.GetString(826, "Verification Failed"))
                    Me.btnVerify.Enabled = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Function

    Private Sub SearchResultsDownloaded(ByVal M As IMDB.MovieSearchResults)
        '//
        ' Process the results that IMDB gave us
        '\\

        Try
            Me.tvResults.Nodes.Clear()
            Me.ClearInfo()
            If Not IsNothing(M) Then
                If M.PartialMatches.Count > 0 OrElse M.PopularTitles.Count > 0 OrElse M.TvTitles.Count > 0 OrElse M.ExactMatches.Count > 0 Then
                    Dim TnP As New TreeNode(String.Format(Master.eLang.GetString(827, "Partial Matches ({0})"), M.PartialMatches.Count))
                    Dim selNode As New TreeNode

                    If M.PartialMatches.Count > 0 Then
                        M.PartialMatches.Sort()
                        For Each Movie As MediaContainers.Movie In M.PartialMatches
                            TnP.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.IMDBID})
                        Next
                        TnP.Expand()
                        Me.tvResults.Nodes.Add(TnP)
                        selNode = TnP.FirstNode
                    End If

                    If M.TvTitles.Count > 0 Then
                        M.TvTitles.Sort()
                        If M.PartialMatches.Count > 0 Then
                            Me.tvResults.Nodes(TnP.Index).Collapse()
                        End If
                        TnP = New TreeNode(String.Format(Master.eLang.GetString(1006, "TV Movie Titles ({0})"), M.TvTitles.Count))
                        For Each Movie As MediaContainers.Movie In M.TvTitles
                            TnP.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.IMDBID})
                        Next
                        TnP.Expand()
                        Me.tvResults.Nodes.Add(TnP)
                        selNode = TnP.FirstNode
                    End If

                    If M.VideoTitles.Count > 0 Then
                        M.VideoTitles.Sort()
                        If M.PartialMatches.Count > 0 Then
                            Me.tvResults.Nodes(TnP.Index).Collapse()
                        End If
                        TnP = New TreeNode(String.Format(Master.eLang.GetString(1083, "Video Titles ({0})"), M.VideoTitles.Count))
                        For Each Movie As MediaContainers.Movie In M.VideoTitles
                            TnP.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.IMDBID})
                        Next
                        TnP.Expand()
                        Me.tvResults.Nodes.Add(TnP)
                        selNode = TnP.FirstNode
                    End If

                    If M.PopularTitles.Count > 0 Then
                        M.PopularTitles.Sort()
                        If M.PartialMatches.Count > 0 OrElse M.TvTitles.Count > 0 Then
                            Me.tvResults.Nodes(TnP.Index).Collapse()
                        End If
                        TnP = New TreeNode(String.Format(Master.eLang.GetString(829, "Popular Titles ({0})"), M.PopularTitles.Count))
                        For Each Movie As MediaContainers.Movie In M.PopularTitles
                            TnP.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.IMDBID})
                        Next
                        TnP.Expand()
                        Me.tvResults.Nodes.Add(TnP)
                        selNode = TnP.FirstNode
                    End If

                    If M.ExactMatches.Count > 0 Then
                        M.ExactMatches.Sort()
                        If M.PartialMatches.Count > 0 OrElse M.TvTitles.Count > 0 OrElse M.PopularTitles.Count > 0 Then
                            Me.tvResults.Nodes(TnP.Index).Collapse()
                        End If
                        TnP = New TreeNode(String.Format(Master.eLang.GetString(831, "Exact Matches ({0})"), M.ExactMatches.Count))
                        For Each Movie As MediaContainers.Movie In M.ExactMatches
                            TnP.Nodes.Add(New TreeNode() With {.Text = String.Concat(Movie.Title, If(Not String.IsNullOrEmpty(Movie.Year), String.Format(" ({0})", Movie.Year), String.Empty)), .Tag = Movie.IMDBID})
                        Next
                        TnP.Expand()
                        Me.tvResults.Nodes.Add(TnP)
                        selNode = TnP.FirstNode
                    End If
                    Me._prevnode = -2

                    'determine if we automatically start downloading info for selected node
                    If M.ExactMatches.Count > 0 Then
                        Me.tvResults.SelectedNode = selNode
                    ElseIf M.PopularTitles.Count > 0 Then
                        Me.tvResults.SelectedNode = selNode
                    ElseIf M.TvTitles.Count > 0 Then
                        Me.tvResults.SelectedNode = selNode
                    ElseIf M.PartialMatches.Count > 0 Then
                        Me.tvResults.SelectedNode = selNode
                    Else
                        Me.tvResults.SelectedNode = Nothing
                    End If
                    Me.tvResults.Focus()
                Else
                    Me.tvResults.Nodes.Add(New TreeNode With {.Text = Master.eLang.GetString(833, "No Matches Found")})
                End If
            End If
            Me.pnlLoading.Visible = False
            chkManual.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function SetPreviewOptions() As Structures.ScrapeOptions_Movie
        Dim aOpt As New Structures.ScrapeOptions_Movie
        aOpt.bCast = False
        aOpt.bCert = False
        aOpt.bCollectionID = False
        aOpt.bCountry = False
        aOpt.bDirector = True
        aOpt.bFullCrew = False
        aOpt.bGenre = True
        aOpt.bMPAA = False
        aOpt.bMusicBy = False
        aOpt.bOtherCrew = False
        aOpt.bOutline = True
        aOpt.bPlot = True
        aOpt.bProducers = False
        aOpt.bRating = False
        aOpt.bRuntime = False
        aOpt.bStudio = False
        aOpt.bTagline = True
        aOpt.bTitle = True
        aOpt.bTop250 = False
        aOpt.bTrailer = False
        aOpt.bVotes = False
        aOpt.bWriters = False
        aOpt.bYear = True

        Return aOpt
    End Function

    Private Sub SetUp()
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.Label2.Text = Master.eLang.GetString(836, "View details of each result to find the proper movie.")
        Me.Label1.Text = Master.eLang.GetString(846, "Movie Search Results")
        Me.chkManual.Text = Master.eLang.GetString(847, "Manual IMDB Entry:")
        Me.btnVerify.Text = Master.eLang.GetString(848, "Verify")
        Me.lblYearHeader.Text = Master.eLang.GetString(49, "Year:")
        Me.lblDirectorHeader.Text = Master.eLang.GetString(239, "Director:")
        Me.lblGenreHeader.Text = Master.eLang.GetString(51, "Genre(s):")
        Me.lblIMDBHeader.Text = Master.eLang.GetString(873, "IMDB ID:")
        Me.lblPlotHeader.Text = Master.eLang.GetString(242, "Plot Outline:")
        Me.Label3.Text = Master.eLang.GetString(798, "Searching IMDB...")
    End Sub

    Private Async Sub tmrLoad_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrLoad.Tick
        Dim pOpt As New Structures.ScrapeOptions_Movie
        Dim ret As New ModuleResult
        pOpt = SetPreviewOptions()

        Me.tmrWait.Stop()
        Me.tmrLoad.Stop()
        Me.pnlLoading.Visible = True
        Me.Label3.Text = Master.eLang.GetString(875, "Downloading details...")

        ret = Await IMDB.GetSearchMovieInfoAsync(Me.tvResults.SelectedNode.Tag.ToString, _nMovie, pOpt)
        _nMovie = CType(ret.ReturnObj(0), MediaContainers.Movie)
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
    Private Async Sub tvResults_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvResults.AfterSelect
        Try
            Me.tmrWait.Stop()
            Me.tmrLoad.Stop()

            Me.ClearInfo()
            Me.OK_Button.Enabled = False

            If Not IsNothing(Me.tvResults.SelectedNode.Tag) AndAlso Not String.IsNullOrEmpty(Me.tvResults.SelectedNode.Tag.ToString) Then
                Me._currnode = Me.tvResults.SelectedNode.Index

                'check if this movie is in the cache already
                If _InfoCache.ContainsKey(Me.tvResults.SelectedNode.Tag.ToString) Then
                    _nMovie = GetMovieClone(_InfoCache(Me.tvResults.SelectedNode.Tag.ToString))
                    Await SearchMovieInfoDownloaded(String.Empty, True)
                    Return
                End If

                Me.pnlLoading.Visible = True
                Me.tmrWait.Start()
            Else
                Me.pnlLoading.Visible = False
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub tvResults_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvResults.GotFocus
        Me.AcceptButton = Me.OK_Button
    End Sub

    Private Sub txtIMDBID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtIMDBID.GotFocus
        Me.AcceptButton = Me.btnVerify
    End Sub

    Private Sub txtIMDBID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIMDBID.TextChanged
        If Me.chkManual.Checked Then
            Me.btnVerify.Enabled = True
            Me.OK_Button.Enabled = False
        End If
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        Me.AcceptButton = Me.btnSearch
    End Sub

    Private Function GetMovieClone(ByVal original As MediaContainers.Movie) As MediaContainers.Movie
        Try
            Using mem As New IO.MemoryStream()
                Dim bin As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(Nothing, New System.Runtime.Serialization.StreamingContext(Runtime.Serialization.StreamingContextStates.Clone))
                bin.Serialize(mem, original)
                mem.Seek(0, IO.SeekOrigin.Begin)
                Return DirectCast(bin.Deserialize(mem), MediaContainers.Movie)
            End Using
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

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