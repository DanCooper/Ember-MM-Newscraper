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
Imports System.IO

Public Class dlgSearchResults_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadPic As New System.ComponentModel.BackgroundWorker
    Friend WithEvents tmrLoad As New Timer
    Friend WithEvents tmrWait As New Timer

    Private _OMDb As Scraper
    Private sHTTP As New HTTP
    Private _currnode As Integer = -1
    Private _prevnode As Integer = -2
    Private _AddonSettings As AddonSettings

    Private _InfoCache As New Dictionary(Of String, MediaContainers.MainDetails)
    Private _PosterCache As New Dictionary(Of String, Image)
    Private _filterOptions As Structures.ScrapeOptions

    Private _tmpTVShow As New MediaContainers.MainDetails

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.MainDetails
        Get
            Return _tmpTVShow
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New(ByVal AddonSettings As AddonSettings, TMDB As Scraper)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _AddonSettings = AddonSettings
        _OMDb = TMDB
    End Sub

    Public Overloads Function ShowDialog(ByVal sShowTitle As String, ByVal sShowPath As String, ByVal filterOptions As Structures.ScrapeOptions) As DialogResult
        tmrWait.Enabled = False
        tmrWait.Interval = 250
        tmrLoad.Enabled = False
        tmrLoad.Interval = 100

        _filterOptions = filterOptions

        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sShowTitle)
        txtSearch.Text = sShowTitle
        txtFileName.Text = sShowPath
        chkManual.Enabled = False

        _OMDb.SearchAsync_TVShow(sShowTitle, _filterOptions)

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal Res As SearchResults_TVShow, ByVal sShowTitle As String, ByVal sShowPath As String) As DialogResult
        tmrWait.Enabled = False
        tmrWait.Interval = 250
        tmrLoad.Enabled = False
        tmrLoad.Interval = 100

        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", sShowTitle)
        txtSearch.Text = sShowTitle
        txtFileName.Text = sShowPath
        SearchResultsDownloaded(Res)

        Return ShowDialog()
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        If Not String.IsNullOrEmpty(txtSearch.Text) Then
            OK_Button.Enabled = False
            pnlPicStatus.Visible = False
            _InfoCache.Clear()
            _PosterCache.Clear()
            ClearInfo()
            lblSearching.Text = String.Concat(Master.eLang.GetString(758, "Searching"), " ...")
            pnlLoading.Visible = True
            chkManual.Enabled = False
            _OMDb.CancelAsync()

            _OMDb.SearchAsync_TVShow(txtSearch.Text, _filterOptions)
        End If
    End Sub

    Private Sub btnVerify_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        _OMDb.GetSearchTVShowInfoAsync(txtTMDBID.Text, pOpt)

    End Sub

    Private Sub btnOpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        If Not String.IsNullOrEmpty(txtFileName.Text) Then
            Dim fPath As String = Directory.GetParent(txtFileName.Text).FullName
            If Not String.IsNullOrEmpty(fPath) Then
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "explorer.exe"
                    Explorer.StartInfo.Arguments = String.Format("/select,""{0}""", fPath)
                    Explorer.Start()
                End Using
            End If
        End If
    End Sub

    Private Sub bwDownloadPic_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPic.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        sHTTP.StartDownloadImage(Args.pURL)

        While sHTTP.IsDownloading
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        e.Result = New Results With {.Result = sHTTP.Image, .IMDBId = Args.UniqueID}
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
        If _OMDb.bwOMDb.IsBusy Then
            _OMDb.CancelAsync()
        End If
        _tmpTVShow = New MediaContainers.MainDetails

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
        lblCreator.Text = String.Empty
        lblGenre.Text = String.Empty
        txtPlot.Text = String.Empty
        lblTMDBID.Text = String.Empty
        pbPoster.Image = Nothing

        _tmpTVShow = New MediaContainers.MainDetails

        _OMDb.CancelAsync()
    End Sub

    Private Sub ControlsVisible(ByVal areVisible As Boolean)
        lblAiredHeader.Visible = areVisible
        lblCreatorsHeader.Visible = areVisible
        lblGenreHeader.Visible = areVisible
        lblPlotHeader.Visible = areVisible
        lblTMDBHeader.Visible = areVisible
        txtPlot.Visible = areVisible
        lblYear.Visible = areVisible
        lblTagline.Visible = areVisible
        lblTitle.Visible = areVisible
        lblCreator.Visible = areVisible
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

        AddHandler _OMDb.SearchInfoDownloaded_TVShow, AddressOf SearchInfoDownloaded
        AddHandler _OMDb.SearchResultsDownloaded_TVShow, AddressOf SearchResultsDownloaded

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
    End Sub

    Private Sub SearchInfoDownloaded(ByVal sPoster As String, ByVal sInfo As MediaContainers.MainDetails)
        pnlLoading.Visible = False
        OK_Button.Enabled = True

        If sInfo IsNot Nothing Then
            ControlsVisible(True)
            _tmpTVShow = sInfo
            lblTitle.Text = _tmpTVShow.Title
            lblTagline.Text = String.Empty '_nShow.Tagline
            lblYear.Text = _tmpTVShow.Premiered
            lblCreator.Text = String.Join(" / ", _tmpTVShow.Creators.ToArray)
            lblGenre.Text = String.Join(" / ", _tmpTVShow.Genres.ToArray)
            txtPlot.Text = StringUtils.ShortenOutline(_tmpTVShow.Plot, 410)
            lblTMDBID.Text = _tmpTVShow.UniqueIDs.TMDbId

            If _PosterCache.ContainsKey(_tmpTVShow.UniqueIDs.TMDbId) Then
                'just set it
                pbPoster.Image = _PosterCache(_tmpTVShow.UniqueIDs.TMDbId)
            Else
                'go download it, if available
                If Not String.IsNullOrEmpty(sPoster) Then
                    If bwDownloadPic.IsBusy Then
                        bwDownloadPic.CancelAsync()
                    End If
                    pnlPicStatus.Visible = True
                    bwDownloadPic = New System.ComponentModel.BackgroundWorker
                    bwDownloadPic.WorkerSupportsCancellation = True
                    bwDownloadPic.RunWorkerAsync(New Arguments With {.pURL = sPoster, .UniqueID = _tmpTVShow.UniqueIDs.TMDbId})
                End If

            End If

            'store clone of tmpshow
            If Not _InfoCache.ContainsKey(_tmpTVShow.UniqueIDs.TMDbId) Then
                _InfoCache.Add(_tmpTVShow.UniqueIDs.TMDbId, GetTVShowClone(_tmpTVShow))
            End If


            btnVerify.Enabled = False
        Else
            If chkManual.Checked Then
                MessageBox.Show(Master.eLang.GetString(825, "Unable to retrieve details for the entered ID. Please check your entry and try again."), Master.eLang.GetString(826, "Verification Failed"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                btnVerify.Enabled = True
            End If
        End If
    End Sub

    Private Sub SearchResultsDownloaded(ByVal M As SearchResults_TVShow)
        tvResults.Nodes.Clear()
        ClearInfo()
        If M IsNot Nothing AndAlso M.Matches.Count > 0 Then
            For Each Show As MediaContainers.MainDetails In M.Matches
                tvResults.Nodes.Add(New TreeNode() With {.Text = String.Concat(Show.Title), .Tag = Show.UniqueIDs.TMDbId})
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
        aOpt.Creators = True
        aOpt.Genres = True
        aOpt.Plot = True
        aOpt.Premiered = True
        aOpt.Title = True

        Return aOpt
    End Function

    Private Sub SetUp()
        OK_Button.Text = Master.eLang.OK
        Cancel_Button.Text = Master.eLang.Cancel
        Label2.Text = Master.eLang.GetString(951, "View details of each result to find the proper TV show.")
        Label1.Text = Master.eLang.GetString(948, "TV Search Results")
        chkManual.Text = String.Concat(Master.eLang.GetString(847, "Manual ID Entry"), " (IMDb / TMDb):")
        btnVerify.Text = Master.eLang.GetString(848, "Verify")
        lblAiredHeader.Text = String.Concat(Master.eLang.GetString(728, "Aired"), ":")
        lblCreatorsHeader.Text = String.Concat(Master.eLang.GetString(744, "Creators"), ":")
        lblGenreHeader.Text = Master.eLang.GetString(51, "Genre(s):")
        lblTMDBHeader.Text = String.Concat(Master.eLang.GetString(933, "TMDB ID"), ":")
        lblPlotHeader.Text = Master.eLang.GetString(242, "Plot Outline:")
        lblSearching.Text = String.Concat(Master.eLang.GetString(758, "Searching"), " ...")
    End Sub

    Private Sub tmrLoad_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrLoad.Tick
        Dim pOpt As New Structures.ScrapeOptions
        pOpt = SetPreviewOptions()

        tmrWait.Stop()
        tmrLoad.Stop()
        pnlLoading.Visible = True
        lblSearching.Text = Master.eLang.GetString(875, "Downloading details...")

        _OMDb.GetSearchTVShowInfoAsync(tvResults.SelectedNode.Tag.ToString, pOpt)
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

            'check if this tv show is in the cache already
            If _InfoCache.ContainsKey(tvResults.SelectedNode.Tag.ToString) Then
                _tmpTVShow = GetTVShowClone(_InfoCache(tvResults.SelectedNode.Tag.ToString))
                SearchInfoDownloaded(String.Empty, _tmpTVShow)
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

    Private Sub txtYear_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
        AcceptButton = btnSearch
    End Sub

    Private Function GetTVShowClone(ByVal original As MediaContainers.MainDetails) As MediaContainers.MainDetails
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
        Dim UniqueID As String

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