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
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class dlgTrailerSelect

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCompileList As New ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadTrailer As New ComponentModel.BackgroundWorker
    Friend WithEvents bwParseTrailer As New ComponentModel.BackgroundWorker

    Private _TmpDBElement As Database.DBElement
    Private _Array As New List(Of String)
    Private _URL As String = String.Empty
    Private _TrailerList As New List(Of MediaContainers.Trailer)
    Private _NoDownload As Boolean

#End Region 'Fields

#Region "Properties"

    Public Property Result As New MediaContainers.Trailer

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub Dialog_FormClosing(sender As Object, e As EventArgs) Handles Me.FormClosing
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        AddHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
        Setup()
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Database.DBElement,
                                         ByRef tURLList As List(Of MediaContainers.Trailer),
                                         ByVal OnlyToNFO As Boolean,
                                         Optional ByVal _isNew As Boolean = False) As DialogResult
        _NoDownload = OnlyToNFO

        'set ListView
        lvTrailers.MultiSelect = False
        lvTrailers.FullRowSelect = True
        lvTrailers.HideSelection = False

        txtYouTubeSearch.Text = String.Concat(DBMovie.Movie.Title, " ", Master.eSettings.MovieTrailerDefaultSearch)

        _TmpDBElement = DBMovie

        ListView_AddTrailers(tURLList)

        lblStatus.Visible = False
        pbStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()
        If lvTrailers.Items.Count = 1 Then
            lvTrailers.Select()
            lvTrailers.Items(0).Selected = True
        End If

        Return ShowDialog()
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseLocalTrailer.Click
        Try
            With ofdTrailer
                .InitialDirectory = Directory.GetParent(_TmpDBElement.Filename).FullName
                .Filter = FileUtils.Common.GetOpenFileDialogFilter_Video(Master.eLang.GetString(1195, "Trailers"))
                .FilterIndex = 0
            End With

            If ofdTrailer.ShowDialog() = DialogResult.OK Then
                txtLocalTrailer.Text = ofdTrailer.FileName
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If bwDownloadTrailer.IsBusy Then bwDownloadTrailer.CancelAsync()

        While bwDownloadTrailer.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Result = Nothing
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnClearLink_Click(sender As Object, e As EventArgs) Handles btnClearManualTrailerLink.Click
        txtManualTrailerLink.Text = String.Empty
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        ProcessSelection()
    End Sub

    Private Sub btnPlayInBrowser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPlayInBrowser.Click
        If Not String.IsNullOrEmpty(txtManualTrailerLink.Text) Then
            Process.Start(txtManualTrailerLink.Text)
        Else
            If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then
                Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
            End If
        End If
    End Sub

    Private Sub btnTrailerScrape_Click(sender As Object, e As EventArgs) Handles btnTrailerScrape.Click
        Dim didCancel As Boolean = False
        txtLocalTrailer.Text = String.Empty
        txtManualTrailerLink.Text = String.Empty
        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(918, "Compiling Trailer List")
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Marquee
        pbStatus.Visible = True
        Application.DoEvents()

        _TrailerList.Clear()

        bwCompileList = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = False,
            .WorkerSupportsCancellation = True
        }
        bwCompileList.RunWorkerAsync()
    End Sub

    Private Sub btnYouTubeSearch_Click(sender As Object, e As EventArgs) Handles btnYouTubeSearch.Click
        Dim ID As Integer = lvTrailers.Items.Count + 1
        Dim nList As New List(Of MediaContainers.Trailer)

        nList = YouTube.Scraper.SearchOnYouTube(txtYouTubeSearch.Text)
        ListView_AddTrailers(nList)
    End Sub

    Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Try
            e.Result = Not ModulesManager.Instance.ScrapeTrailer_Movie(_TmpDBElement, Enums.ModifierType.MainTrailer, _TrailerList)
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        If bwCompileList.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCompileList.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                If _TrailerList.Count > 0 Then
                    lvTrailers.Items.Clear()
                    ListView_AddTrailers(_TrailerList)
                Else
                    MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        lblStatus.Visible = False
        pbStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()
    End Sub

    Private Sub bwDownloadTrailer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTrailer.DoWork
        Try
            Result.TrailerOriginal.LoadFromWeb(Result)
            e.Result = True
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            e.Result = False
        End Try

        If bwDownloadTrailer.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwDownloadTrailer_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDownloadTrailer.ProgressChanged
        If e.ProgressPercentage = -1 Then
            lblStatus.Text = e.UserState.ToString
            pbStatus.Value = 0
        ElseIf e.ProgressPercentage = -2 Then
            lblStatus.Text = e.UserState.ToString
            pbStatus.Style = ProgressBarStyle.Marquee
        Else
            pbStatus.Value = e.ProgressPercentage
        End If
        Application.DoEvents()
    End Sub

    Private Sub bwDownloadTrailer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadTrailer.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                DialogResult = DialogResult.OK
            Else
                lblStatus.Visible = False
                pbStatus.Visible = False
                SetControlsEnabled(True)
                SetEnabled()
            End If
        End If
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer, ByVal strInfo As String)
        bwDownloadTrailer.ReportProgress(iProgress, strInfo)
    End Sub

    Private Sub ListView_AddTrailers(ByVal tList As List(Of MediaContainers.Trailer))
        Dim ID As Integer = lvTrailers.Items.Count + 1
        Dim nList As List(Of MediaContainers.Trailer) = tList

        Dim str(10) As String
        For Each aTrailer In nList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aTrailer.URLVideoStream.ToString
            str(2) = aTrailer.URLWebsite.ToString
            str(3) = aTrailer.Title.ToString
            str(4) = aTrailer.Duration.ToString
            str(5) = aTrailer.Quality.ToString
            str(6) = aTrailer.Type.ToString
            str(7) = aTrailer.Source.ToString
            str(8) = aTrailer.Scraper.ToString
            str(9) = aTrailer.TrailerOriginal.Extention.ToString
            itm = New ListViewItem(str) With {
                .Tag = aTrailer
            }
            lvTrailers.Items.Add(itm)
            ID = ID + 1
        Next
    End Sub

    Private Sub ListView_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvTrailers.DoubleClick
        If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
    End Sub

    Private Sub ListView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvTrailers.SelectedIndexChanged
        SetEnabled()
    End Sub

    Private Sub ProcessSelection()
        Dim didCancel As Boolean = False

        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(906, "Downloading selected trailer...")
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pbStatus.Visible = True
        Application.DoEvents()

        Select Case True
            Case txtLocalTrailer.Text.Length > 0
                '
                'Local trailer
                '
                lblStatus.Text = Master.eLang.GetString(907, "Copying specified file to trailer...")
                If Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(txtLocalTrailer.Text)) AndAlso File.Exists(txtLocalTrailer.Text) Then
                    If _NoDownload Then
                        Result = New MediaContainers.Trailer With {
                            .URLWebsite = txtLocalTrailer.Text
                        }
                    Else
                        Result = New MediaContainers.Trailer
                        Result.TrailerOriginal.LoadFromFile(txtLocalTrailer.Text)
                    End If
                    DialogResult = DialogResult.OK
                Else
                    MessageBox.Show(Master.eLang.GetString(192, "File is not valid."), Master.eLang.GetString(194, "Not Valid"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    didCancel = True
                End If
            Case YouTube.UrlUtils.IsYouTubeUrl(txtManualTrailerLink.Text)
                '
                'Manual YouTube Link
                '
                Result = YouTube.Scraper.GetVideoDetails(txtManualTrailerLink.Text)
                If Result IsNot Nothing Then
                    If _NoDownload Then
                        DialogResult = DialogResult.OK
                    Else
                        If Result.StreamsSpecified Then
                            If SetFormat() Then
                                StartDownload()
                            Else
                                didCancel = True
                            End If
                        ElseIf Result.URLVideoStreamSpecified Then
                            StartDownload()
                        Else
                            didCancel = True
                        End If
                    End If
                Else
                    MessageBox.Show(Master.eLang.GetString(1170, "Trailer could not be parsed"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    didCancel = True
                End If
            Case StringUtils.isValidURL(txtManualTrailerLink.Text)
                '
                'Manual direct URL
                '
                If _NoDownload Then
                    Result.URLWebsite = txtManualTrailerLink.Text
                    DialogResult = DialogResult.OK
                Else
                    Dim ManualTrailer As New TrailerLinksContainer With {.VideoURL = txtManualTrailerLink.Text}
                    bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                        .WorkerReportsProgress = True,
                        .WorkerSupportsCancellation = True
                    }
                    bwDownloadTrailer.RunWorkerAsync()
                End If
            Case lvTrailers.SelectedItems.Count = 1 AndAlso String.IsNullOrEmpty(txtLocalTrailer.Text.Trim) AndAlso String.IsNullOrEmpty(txtManualTrailerLink.Text.Trim)
                '
                'List trailer
                '
                Result = DirectCast(lvTrailers.SelectedItems(0).Tag, MediaContainers.Trailer)
                If Result IsNot Nothing Then
                    If _NoDownload Then DialogResult = DialogResult.OK
                    If Result.StreamsSpecified Then
                        If SetFormat() Then
                            StartDownload()
                        Else
                            didCancel = True
                        End If
                    ElseIf Result.URLVideoStreamSpecified Then
                        StartDownload()
                    Else
                        didCancel = True
                    End If
                Else
                    MessageBox.Show(Master.eLang.GetString(1170, "Trailer could not be parsed"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                    didCancel = True
                End If
        End Select

        If didCancel Then
            lblStatus.Visible = False
            pbStatus.Visible = False
            SetControlsEnabled(True)
            SetEnabled()
        End If
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        btnOK.Enabled = isEnabled
        btnBrowseLocalTrailer.Enabled = isEnabled
        btnClearManualTrailerLink.Enabled = isEnabled
        btnPlayInBrowser.Enabled = isEnabled
        btnTrailerScrape.Enabled = isEnabled
        btnYouTubeSearch.Enabled = isEnabled
        lvTrailers.Enabled = isEnabled
        txtLocalTrailer.Enabled = isEnabled
        txtManualTrailerLink.Enabled = isEnabled
        txtYouTubeSearch.Enabled = isEnabled
    End Sub

    Private Sub SetEnabled()
        If StringUtils.isValidURL(txtManualTrailerLink.Text) OrElse lvTrailers.SelectedItems.Count > 0 OrElse txtLocalTrailer.Text.Length > 0 Then
            btnOK.Enabled = True
            If txtLocalTrailer.Text.Length > 0 Then
                btnPlayInBrowser.Enabled = False
            Else
                btnPlayInBrowser.Enabled = True
            End If
            If txtLocalTrailer.Text.Length > 0 Then
                If _NoDownload Then
                    btnOK.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    btnOK.Text = Master.eLang.GetString(911, "Copy")
                End If
            Else
                If _NoDownload Then
                    btnOK.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    btnOK.Text = Master.eLang.GetString(373, "Download")
                End If
            End If
        Else
            btnOK.Enabled = False
            If _NoDownload Then
                btnOK.Text = Master.eLang.GetString(913, "Set To Nfo")
            Else
                btnOK.Text = Master.eLang.GetString(373, "Download")
            End If
            btnPlayInBrowser.Enabled = False
        End If
        SetEnabledSearch()
    End Sub

    Private Sub SetEnabledSearch()
        If Not String.IsNullOrEmpty(txtYouTubeSearch.Text) Then
            btnYouTubeSearch.Enabled = True
        Else
            btnYouTubeSearch.Enabled = False
        End If
    End Sub

    Private Function SetFormat() As Boolean
        Using dFormats As New dlgTrailerFormat
            If dFormats.ShowDialog(Result) = DialogResult.OK Then
                Result.URLAudioStream = dFormats.Result.AudioURL
                Result.URLVideoStream = dFormats.Result.VideoURL
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Private Sub Setup()
        Text = Master.eLang.GetString(915, "Select Trailer to Download")
        btnOK.Text = Master.eLang.GetString(373, "Download")
        colDescription.Text = Master.eLang.GetString(979, "Description")
        colDuration.Text = Master.eLang.GetString(609, "Duration")
        colVideoQuality.Text = Master.eLang.GetString(1138, "Quality")
        colSource.Text = Master.eLang.GetString(1173, "Source")
        btnPlayInBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        btnTrailerScrape.Text = Master.eLang.GetString(1171, "Scrape Trailers")
        btnYouTubeSearch.Text = Master.eLang.GetString(977, "Search")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        gbManualTrailer.Text = Master.eLang.GetString(916, "Manual Trailer Entry")
        gbYouTubeSearch.Text = Master.eLang.GetString(978, "Search On YouTube")
        lblLocalTrailer.Text = String.Concat(Master.eLang.GetString(920, "Local Trailer"), ":")
        lblManualTrailerLink.Text = String.Concat(Master.eLang.GetString(917, "Direct Link or YouTube URL"), ":")
        lblStatus.Text = Master.eLang.GetString(918, "Compiling Trailer List")
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtLocalTrailer.TextChanged
        SetEnabled()
    End Sub

    Private Sub StartDownload()
        bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        bwDownloadTrailer.RunWorkerAsync()
    End Sub

    Private Sub txtManualTrailerLink_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtManualTrailerLink.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtYouTubeSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtYouTubeSearch.TextChanged
        SetEnabledSearch()
    End Sub

#End Region 'Methods

End Class