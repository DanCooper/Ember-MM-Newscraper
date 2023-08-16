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

Imports NLog
Imports System.IO

Public Class dlgSearchResults

#Region "Fields"

    Shared _logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadPoster As New ComponentModel.BackgroundWorker

    Private _contentType As Enums.ContentType
    Private _scraper As Interfaces.ISearchResultsDialog
    Private _searchResults As New Dictionary(Of String, MediaContainers.MainDetails)
    Private _tmpResult As New MediaContainers.MainDetails
    Private _uniqueIdType As String = String.Empty
    Private _uniqueIds As New List(Of String)

    Private _verifyOptions As Structures.ScrapeOptions = New Structures.ScrapeOptions With {
        .Directors = True,
        .Genres = True,
        .OriginalTitle = True,
        .Plot = True,
        .Premiered = True,
        .Tagline = True,
        .Title = True
    }

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.MainDetails
        Get
            Return _tmpResult
        End Get
    End Property

#End Region 'Properties

#Region "Dialog"
    ''' <summary>
    ''' Creates a new search results dialog for movies
    ''' </summary>
    ''' <param name="scraper"></param>
    ''' <param name="uniqueIdType">The type of the unique ID that have to use to clearly assign the search results based on the scraper (e.g. "imdb", "tmdb", "tvdb")</param>
    Public Sub New(ByVal scraper As Interfaces.ISearchResultsDialog, ByVal uniqueIdType As String, ByVal possibleUniqueIds As List(Of String), ByVal contentType As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _contentType = contentType
        _scraper = scraper
        _uniqueIdType = uniqueIdType
        _uniqueIds = possibleUniqueIds
    End Sub

    Private Sub Dialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RemoveHandler _scraper.GetInfoFinished, AddressOf Preview_LoadInfo
        RemoveHandler _scraper.SearchFinished, AddressOf SearchResults_Load
    End Sub

    Private Sub Dialog_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.GotFocus
        AcceptButton = btnOk
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Setup()
        AddHandler _scraper.GetInfoFinished, AddressOf Preview_LoadInfo
        AddHandler _scraper.SearchFinished, AddressOf SearchResults_Load
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
        dgvSearchResults.Focus()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        _scraper.CancelAsync()
        _tmpResult = New MediaContainers.MainDetails
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_Ok_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub DialogResult_NextScraper_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNextScraper.Click
        _scraper.CancelAsync()
        _tmpResult = New MediaContainers.MainDetails
        DialogResult = DialogResult.Retry
    End Sub

    Public Overloads Function ShowDialog(ByVal title As String, Optional ByVal fileName As String = "", Optional ByVal year As String = "") As DialogResult
        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", title)
        txtSearch.Text = title
        txtFileName.Text = fileName
        txtYear.Text = year
        chkUniqueId.Enabled = False

        Dim intYear As Integer
        Integer.TryParse(year, intYear)

        _scraper.SearchAsync_By_Title(title, _contentType, intYear)

        Return ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByVal title As String, ByVal fileName As String, ByVal searchResults As List(Of MediaContainers.MainDetails)) As DialogResult
        Text = String.Concat(Master.eLang.GetString(794, "Search Results"), " - ", title)
        txtSearch.Text = title
        txtFileName.Text = fileName
        SearchResults_Load(searchResults)

        Return ShowDialog()
    End Function

    Private Sub Setup()
        btnOk.Text = Master.eLang.CommonWordsList.OK
        btnCancel.Text = Master.eLang.CommonWordsList.Cancel
        'Label2.Text = Master.eLang.GetString(836, "View details of each result to find the proper movie.")
        'Label1.Text = Master.eLang.GetString(846, "Movie Search Results")
        chkUniqueId.Text = Master.eLang.GetString(926, "Manual TMDB Entry:")
        lblDirectorsHeader.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblGenresHeader.Text = String.Concat(Master.eLang.GetString(725, "Genres"), ":")
        lblPremieredHeader.Text = String.Concat(Master.eLang.GetString(724, "Premiered"), ":")
        lblUniqueIdHeader.Text = String.Concat(Master.eLang.GetString(933, "TMDB ID"), ":")
        lblPlotHeader.Text = Master.eLang.GetString(242, "Plot Outline:")
        tsslStatus.Text = String.Concat(Master.eLang.GetString(758, "Please wait"), "...")
    End Sub

#End Region 'Dialog

#Region "Methods"

    Private Sub DownloadPoster_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadPoster.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Args.Poster.LoadAndCache(_contentType, False, True)
        e.Result = New Arguments With {.Poster = Args.Poster}
    End Sub

    Private Sub DownloadPoster_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadPoster.RunWorkerCompleted
        tspbStatus.Visible = False
        tsslStatus.Visible = False
        If Not bwDownloadPoster.CancellationPending Then
            If _tmpResult.ThumbPoster.ImageThumb.HasMemoryStream Then pbPoster.Image = _tmpResult.ThumbPoster.ImageThumb.Image
        End If
    End Sub

    Private Sub OpenFolder_Click(sender As Object, e As EventArgs) Handles btnOpenFolder.Click
        Dim strPath As String = Directory.GetParent(txtFileName.Text).FullName

        If Not String.IsNullOrEmpty(strPath) Then
            Process.Start("Explorer.exe", strPath)
        End If
    End Sub

    Private Sub Preview_Clear()
        lblDirectors.Text = String.Empty
        lblGenres.Text = String.Empty
        lblOriginalTitle.Text = String.Empty
        lblTagline.Text = String.Empty
        lblTitle.Text = String.Empty
        lblUniqueId.Text = String.Empty
        lblPremiered.Text = String.Empty
        pbPoster.Image = Nothing
        txtPlot.Text = String.Empty
        _tmpResult = New MediaContainers.MainDetails
        _scraper.CancelAsync()
    End Sub

    Private Sub Preview_LoadInfo(ByVal mainInfo As MediaContainers.MainDetails)
        If bwDownloadPoster.IsBusy Then
            bwDownloadPoster.CancelAsync()
        End If
        If mainInfo IsNot Nothing Then
            lblDirectors.Text = String.Join(" / ", mainInfo.Directors.ToArray)
            lblGenres.Text = String.Join(" / ", mainInfo.Genres.ToArray)
            lblOriginalTitle.Text = mainInfo.OriginalTitle
            lblTagline.Text = mainInfo.Tagline
            lblTitle.Text = If(mainInfo.YearSpecified, String.Format("{0} ({1})", mainInfo.Title, mainInfo.Year), mainInfo.Title)
            lblPremiered.Text = mainInfo.Year
            txtPlot.Text = StringUtils.ShortenOutline(mainInfo.Plot, 410)
            lblUniqueId.Text = mainInfo.UniqueIDs.GetIdByType(_uniqueIdType).ToString
            'Poster
            If mainInfo.ThumbPoster.ImageThumb.HasMemoryStream Then
                'just set it
                pbPoster.Image = mainInfo.ThumbPoster.ImageThumb.Image
            Else
                'go download it, if available
                If mainInfo.ThumbPoster.URLThumbSpecified Then
                    tspbStatus.Visible = True
                    tsslStatus.Text = "Downloading Image"
                    tsslStatus.Visible = True
                    bwDownloadPoster = New ComponentModel.BackgroundWorker With {
                        .WorkerSupportsCancellation = True
                    }
                    bwDownloadPoster.RunWorkerAsync(New Arguments With {
                                                    .Poster = mainInfo.ThumbPoster
                                                    })
                End If
            End If
            _tmpResult = mainInfo
            btnOk.Enabled = True
        Else
            If chkUniqueId.Checked Then
                MessageBox.Show(Master.eLang.GetString(825, "Unable to retrieve details for the entered unique ID. Please check your entry and try again."), Master.eLang.GetString(826, "Verification Failed"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            btnOk.Enabled = False
        End If
    End Sub

    Private Sub Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Preview_Clear()
        If Not chkUniqueId.Checked Then
            If Not String.IsNullOrEmpty(txtSearch.Text.Trim) AndAlso (String.IsNullOrEmpty(txtYear.Text.Trim) OrElse Integer.TryParse(txtYear.Text.Trim, 0)) Then
                dgvSearchResults.Rows.Clear()
                dgvSearchResults.ClearSelection()
                btnOk.Enabled = False
                _searchResults.Clear()
                tsslStatus.Text = String.Concat(Master.eLang.GetString(758, "Please wait"), "...")
                tspbStatus.Visible = True
                tsslStatus.Visible = True
                chkUniqueId.Enabled = False
                _scraper.CancelAsync()

                Dim intYear As Integer
                Integer.TryParse(txtYear.Text.Trim, intYear)

                _scraper.SearchAsync_By_Title(txtSearch.Text.Trim, _contentType, intYear)
            End If
        Else
            _scraper.SearchAsync_By_UniqueId(txtUniqueId.Text.Trim, _contentType)
        End If
    End Sub

    Private Sub SearchField_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtSearch.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Sub SearchResults_Fill()
        dgvSearchResults.Rows.Clear()
        dgvSearchResults.SuspendLayout()
        Select Case _contentType
            Case Enums.ContentType.Movie
                For Each tResult In _searchResults
                    Dim i As Integer = dgvSearchResults.Rows.Add
                    dgvSearchResults.Rows(i).Tag = tResult.Value.UniqueIDs.GetIdByType(_uniqueIdType)
                    dgvSearchResults.Rows(i).Cells(colTitle.Name).Value = tResult.Value.Title
                    dgvSearchResults.Rows(i).Cells(colYear.Name).Value = tResult.Value.Year
                Next
            Case Enums.ContentType.MovieSet
                For Each tResult In _searchResults
                    Dim i As Integer = dgvSearchResults.Rows.Add
                    dgvSearchResults.Rows(i).Tag = tResult.Value.UniqueIDs.GetIdByType(_uniqueIdType)
                    dgvSearchResults.Rows(i).Cells(colTitle.Name).Value = tResult.Value.Title
                Next
            Case Enums.ContentType.TVShow
                For Each tResult In _searchResults
                    Dim i As Integer = dgvSearchResults.Rows.Add
                    dgvSearchResults.Rows(i).Tag = tResult.Value.UniqueIDs.GetIdByType(_uniqueIdType)
                    dgvSearchResults.Rows(i).Cells(colTitle.Name).Value = tResult.Value.Title
                    dgvSearchResults.Rows(i).Cells(colYear.Name).Value = tResult.Value.Premiered
                Next

        End Select
        dgvSearchResults.ResumeLayout()
        dgvSearchResults.ClearSelection()
        If dgvSearchResults.Rows.Count > 0 Then dgvSearchResults.Rows(0).Selected = True
        dgvSearchResults.Focus()
    End Sub

    Private Sub SearchResults_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles dgvSearchResults.GotFocus
        AcceptButton = btnOk
    End Sub

    Private Sub SearchResults_Load(ByVal searchResults As List(Of MediaContainers.MainDetails))
        dgvSearchResults.Rows.Clear()
        Preview_Clear()
        If searchResults IsNot Nothing AndAlso searchResults.Count > 0 Then
            For Each item In searchResults
                _searchResults.Add(item.UniqueIDs.GetIdByType(_uniqueIdType).ToString, item)
            Next
            SearchResults_Fill()
        Else
            Dim i As Integer = dgvSearchResults.Rows.Add
            dgvSearchResults.Rows(i).Cells(colTitle.Name).Value = Master.eLang.GetString(833, "No Matches Found")
        End If
        tspbStatus.Visible = False
        tsslStatus.Visible = False
        chkUniqueId.Enabled = True
    End Sub

    Private Sub SearchResults_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSearchResults.SelectionChanged
        Preview_Clear()
        If dgvSearchResults.SelectedRows.Count > 0 AndAlso
            dgvSearchResults.SelectedRows(0).Tag IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(dgvSearchResults.SelectedRows(0).Tag.ToString) Then
            If _searchResults.ContainsKey(dgvSearchResults.SelectedRows(0).Tag.ToString) Then
                Preview_LoadInfo(_searchResults(dgvSearchResults.SelectedRows(0).Tag.ToString))
            End If
        End If
    End Sub

    Private Sub UniqueId_Enabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUniqueId.CheckedChanged
        Preview_Clear()
        btnOk.Enabled = False
        dgvSearchResults.Enabled = Not chkUniqueId.Checked
        txtSearch.Enabled = Not chkUniqueId.Checked
        txtUniqueId.Enabled = chkUniqueId.Checked
        txtYear.Enabled = Not chkUniqueId.Checked

        If Not chkUniqueId.Checked Then
            If dgvSearchResults.Rows.Count > 0 Then dgvSearchResults.Rows(0).Selected = True
            txtUniqueId.Text = String.Empty
        Else
            dgvSearchResults.ClearSelection()
            txtUniqueId.Focus()
        End If
    End Sub

    Private Sub UniqueId_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtUniqueId.GotFocus
        AcceptButton = btnSearch
    End Sub

    Private Sub UniqueId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtUniqueId.TextChanged
        If chkUniqueId.Checked Then
            btnSearch.Enabled = Not String.IsNullOrEmpty(txtUniqueId.Text.Trim)
            btnOk.Enabled = False
        End If
    End Sub

    Private Sub YearField_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles txtYear.GotFocus
        AcceptButton = btnSearch
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Poster As MediaContainers.Image

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class