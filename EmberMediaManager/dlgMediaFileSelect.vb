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
Imports System.IO

Public Class dlgMediaFileSelect

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCompileList As New ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadMediaFile As New ComponentModel.BackgroundWorker

    Private _TmpDBElement As Database.DBElement
    Private _Array As New List(Of String)
    Private _URL As String = String.Empty
    Private _MediaFileList As New List(Of MediaContainers.MediaFile)
    Private _NoDownload As Boolean
    Private _Type As Enums.ModifierType

#End Region 'Fields

#Region "Properties"

    Public Property Result As MediaContainers.MediaFile

#End Region 'Properties

#Region "Dialog"

    Public Sub New(ByVal type As Enums.ModifierType)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _Type = type
    End Sub

    Private Sub Dialog_FormClosing(sender As Object, e As EventArgs) Handles Me.FormClosing
        RemoveHandler MediaFiles.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        AddHandler MediaFiles.ProgressUpdated, AddressOf DownloadProgressUpdated
        Setup()
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Overloads Function ShowDialog(ByRef dbElement As Database.DBElement,
                                         ByRef urlList As List(Of MediaContainers.MediaFile),
                                         Optional ByVal onlyToNFO As Boolean = False) As DialogResult
        _NoDownload = onlyToNFO

        'set ListView
        lvMediaFiles.MultiSelect = False
        lvMediaFiles.FullRowSelect = True
        lvMediaFiles.HideSelection = False

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                Select Case _Type
                    Case Enums.ModifierType.MainTheme
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.Movie.Title, Master.eSettings.MovieThemeDefaultSearch)
                        colVideoResolution.Width = 0
                    Case Enums.ModifierType.MainTrailer
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.Movie.Title, Master.eSettings.MovieTrailerDefaultSearch)
                End Select
            Case Enums.ContentType.TVShow
                Select Case _Type
                    Case Enums.ModifierType.MainTheme
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.TVShow.Title, Master.eSettings.TVShowThemeDefaultSearch)
                        colVideoResolution.Width = 0
                End Select
        End Select

        _TmpDBElement = dbElement

        ListView_AddMediaFiles(urlList)

        lblStatus.Visible = False
        pbStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()
        If lvMediaFiles.Items.Count = 1 Then
            lvMediaFiles.Select()
            lvMediaFiles.Items(0).Selected = True
        End If

        Return ShowDialog()
    End Function

    Private Sub Setup()
        Select Case _Type
            Case Enums.ModifierType.MainTheme
                Text = Master.eLang.GetString(916, "Select Theme to download")
            Case Enums.ModifierType.MainTrailer
                Text = Master.eLang.GetString(915, "Select Trailer to download")
        End Select
        btnOK.Text = Master.eLang.GetString(373, "Download")
        colAudioBitrate.Text = Master.eLang.GetString(1158, "Bitrate")
        colDescription.Text = Master.eLang.GetString(979, "Description")
        colDuration.Text = Master.eLang.GetString(609, "Duration")
        colVideoResolution.Text = Master.eLang.GetString(1138, "Quality")
        colSource.Text = Master.eLang.GetString(1173, "Source")
        btnOpenInBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        btnScrape.Text = Master.eLang.GetString(79, "Scrape")
        btnYouTubeSearch.Text = Master.eLang.GetString(977, "Search")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        gbCustom.Text = Master.eLang.GetString(1191, "Custom")
        gbYouTubeSearch.Text = Master.eLang.GetString(978, "Search On YouTube")
        lblCustomLocalFile.Text = String.Concat(Master.eLang.GetString(920, "Local File"), ":")
        lblCustomURL.Text = String.Concat(Master.eLang.GetString(917, "Direct Link or YouTube URL"), ":")
        lblStatus.Text = Master.eLang.GetString(918, "Compiling List...")
    End Sub

#End Region 'Dialog

#Region "Methods"

    Private Sub btnCustomLocalFile_Browse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCustomLocalFile_Browse.Click
        Try
            With ofdFile
                .InitialDirectory = Directory.GetParent(_TmpDBElement.Filename).FullName
                .Filter = FileUtils.Common.GetOpenFileDialogFilter_Video(Master.eLang.GetString(1195, "Trailers"))
                .FilterIndex = 0
            End With

            If ofdFile.ShowDialog() = DialogResult.OK Then
                txtCustomLocalFile.Text = ofdFile.FileName
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If bwDownloadMediaFile.IsBusy Then bwDownloadMediaFile.CancelAsync()

        While bwDownloadMediaFile.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Result = Nothing
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnClearLink_Click(sender As Object, e As EventArgs) Handles btnCustomURL_Remove.Click
        txtCustomURL.Text = String.Empty
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        ProcessSelection()
    End Sub

    Private Sub btnOpenInBrowser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenInBrowser.Click
        If Not String.IsNullOrEmpty(txtCustomURL.Text) Then
            Process.Start(txtCustomURL.Text)
        Else
            If Not String.IsNullOrEmpty(lvMediaFiles.SelectedItems(0).SubItems(2).Text.ToString) Then
                Process.Start(lvMediaFiles.SelectedItems(0).SubItems(2).Text.ToString)
            End If
        End If
    End Sub

    Private Sub btnScrape_Click(sender As Object, e As EventArgs) Handles btnScrape.Click
        Dim didCancel As Boolean = False
        txtCustomLocalFile.Text = String.Empty
        txtCustomURL.Text = String.Empty
        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(918, "Compiling list...")
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Marquee
        pbStatus.Visible = True
        Application.DoEvents()

        _MediaFileList.Clear()

        bwCompileList = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = False,
            .WorkerSupportsCancellation = True
        }
        bwCompileList.RunWorkerAsync(New Arguments With {.IsSearch = False})
    End Sub

    Private Sub btnYouTubeSearch_Click(sender As Object, e As EventArgs) Handles btnYouTubeSearch.Click
        Dim didCancel As Boolean = False
        txtCustomLocalFile.Text = String.Empty
        txtCustomURL.Text = String.Empty
        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(918, "Compiling list...")
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Marquee
        pbStatus.Visible = True
        Application.DoEvents()

        _MediaFileList.Clear()

        bwCompileList = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = False,
            .WorkerSupportsCancellation = True
        }
        bwCompileList.RunWorkerAsync(New Arguments With {.IsSearch = True, .SearchString = txtYouTubeSearch.Text})
    End Sub

    Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Try
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            If Args.IsSearch Then
                _MediaFileList = YouTube.Scraper.SearchOnYouTube(Args.SearchString, _Type)
                e.Result = _MediaFileList.Count > 0
            Else
                Select Case _Type
                    Case Enums.ModifierType.MainTheme
                        Select Case _TmpDBElement.ContentType
                            Case Enums.ContentType.Movie
                                e.Result = Not ModulesManager.Instance.ScrapeTheme_Movie(_TmpDBElement, Enums.ModifierType.MainTheme, _MediaFileList)
                            Case Enums.ContentType.TVShow
                                e.Result = Not ModulesManager.Instance.ScrapeTheme_TVShow(_TmpDBElement, Enums.ModifierType.MainTrailer, _MediaFileList)
                        End Select
                    Case Enums.ModifierType.MainTrailer
                        Select Case _TmpDBElement.ContentType
                            Case Enums.ContentType.Movie
                                e.Result = Not ModulesManager.Instance.ScrapeTrailer_Movie(_TmpDBElement, Enums.ModifierType.MainTrailer, _MediaFileList)
                        End Select
                End Select
            End If
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
                If _MediaFileList.Count > 0 Then
                    lvMediaFiles.Items.Clear()
                    ListView_AddMediaFiles(_MediaFileList)
                Else
                    Select Case _Type
                        Case Enums.ModifierType.MainTheme
                            MessageBox.Show(Master.eLang.GetString(1163, "No Themes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Case Enums.ModifierType.MainTrailer
                            MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Select
                End If
            Else
                Select Case _Type
                    Case Enums.ModifierType.MainTheme
                        MessageBox.Show(Master.eLang.GetString(1163, "No Themes found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Case Enums.ModifierType.MainTrailer
                        MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Select
            End If
        End If
        lblStatus.Visible = False
        pbStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()
    End Sub

    Private Sub bwDownloadMediaFile_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadMediaFile.DoWork
        Try
            Result.FileOriginal.LoadFromWeb(Result)
            e.Result = True
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            e.Result = False
        End Try

        If bwDownloadMediaFile.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwDownloadMediaFile_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDownloadMediaFile.ProgressChanged
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

    Private Sub bwDownloadMediaFile_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadMediaFile.RunWorkerCompleted
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
        bwDownloadMediaFile.ReportProgress(iProgress, strInfo)
    End Sub

    Private Sub ListView_AddMediaFiles(ByVal tList As List(Of MediaContainers.MediaFile))
        Dim ID As Integer = lvMediaFiles.Items.Count + 1
        Dim nList As List(Of MediaContainers.MediaFile) = tList

        Dim str(10) As String
        For Each aMediaFile In nList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aMediaFile.URLVideoStream.ToString
            str(2) = aMediaFile.URLWebsite.ToString
            str(3) = aMediaFile.Title.ToString
            str(4) = aMediaFile.Duration.ToString
            str(5) = aMediaFile.VideoResolution.ToString
            str(6) = aMediaFile.AudioBitrate.ToString
            str(7) = aMediaFile.VideoType.ToString
            str(8) = aMediaFile.Source.ToString
            str(9) = aMediaFile.Scraper.ToString
            str(10) = aMediaFile.FileOriginal.Extension.ToString
            itm = New ListViewItem(str) With {
                .Tag = aMediaFile
            }
            lvMediaFiles.Items.Add(itm)
            ID = ID + 1
        Next
    End Sub

    Private Sub ListView_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvMediaFiles.DoubleClick
        If Not String.IsNullOrEmpty(lvMediaFiles.SelectedItems(0).SubItems(2).Text.ToString) Then Process.Start(lvMediaFiles.SelectedItems(0).SubItems(2).Text.ToString)
    End Sub

    Private Sub ListView_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvMediaFiles.SelectedIndexChanged
        SetEnabled()
    End Sub

    Private Sub ProcessSelection()
        Dim didCancel As Boolean = False

        SetControlsEnabled(False)
        lblStatus.Text = String.Concat(Master.eLang.GetString(906, "Downloading selected file"), "...")
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pbStatus.Visible = True
        Application.DoEvents()

        Select Case True
            Case txtCustomLocalFile.Text.Length > 0
                '
                'Local trailer
                '
                lblStatus.Text = String.Concat(Master.eLang.GetString(907, "Copying specified file"), "...")
                If Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(txtCustomLocalFile.Text)) AndAlso File.Exists(txtCustomLocalFile.Text) Then
                    If _NoDownload Then
                        Result = New MediaContainers.MediaFile(Enums.ModifierType.MainTrailer) With {
                            .URLWebsite = txtCustomLocalFile.Text
                        }
                    Else
                        Result = New MediaContainers.MediaFile(Enums.ModifierType.MainTrailer)
                        Result.FileOriginal.LoadFromFile(txtCustomLocalFile.Text)
                    End If
                    DialogResult = DialogResult.OK
                Else
                    MessageBox.Show(Master.eLang.GetString(192, "File is not valid."), Master.eLang.GetString(194, "Not Valid"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    didCancel = True
                End If
            Case YouTube.UrlUtils.IsYouTubeUrl(txtCustomURL.Text)
                '
                'Manual YouTube Link
                '
                Result = YouTube.Scraper.GetVideoDetails(txtCustomURL.Text, _Type)
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
            Case StringUtils.isValidURL(txtCustomURL.Text)
                '
                'Manual direct URL
                '
                If _NoDownload Then
                    Result.URLWebsite = txtCustomURL.Text
                    DialogResult = DialogResult.OK
                Else
                    Dim ManualTrailer As New MediaFileLinkContainer With {.VideoURL = txtCustomURL.Text}
                    bwDownloadMediaFile = New ComponentModel.BackgroundWorker With {
                        .WorkerReportsProgress = True,
                        .WorkerSupportsCancellation = True
                    }
                    bwDownloadMediaFile.RunWorkerAsync()
                End If
            Case lvMediaFiles.SelectedItems.Count = 1 AndAlso String.IsNullOrEmpty(txtCustomLocalFile.Text.Trim) AndAlso String.IsNullOrEmpty(txtCustomURL.Text.Trim)
                '
                'List trailer
                '
                Result = DirectCast(lvMediaFiles.SelectedItems(0).Tag, MediaContainers.MediaFile)
                If Result IsNot Nothing Then
                    If _NoDownload Then DialogResult = DialogResult.OK
                    If Result.StreamsSpecified Then
                        If SetFormat() Then
                            StartDownload()
                        Else
                            didCancel = True
                        End If
                    ElseIf Result.URLVideoStreamSpecified OrElse Result.URLAudioStreamSpecified Then
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
        btnCustomLocalFile_Browse.Enabled = isEnabled
        btnCustomURL_Remove.Enabled = isEnabled
        btnOpenInBrowser.Enabled = isEnabled
        btnScrape.Enabled = isEnabled
        btnYouTubeSearch.Enabled = isEnabled
        lvMediaFiles.Enabled = isEnabled
        txtCustomLocalFile.Enabled = isEnabled
        txtCustomURL.Enabled = isEnabled
        txtYouTubeSearch.Enabled = isEnabled
    End Sub

    Private Sub SetEnabled()
        If StringUtils.isValidURL(txtCustomURL.Text) OrElse lvMediaFiles.SelectedItems.Count > 0 OrElse txtCustomLocalFile.Text.Length > 0 Then
            btnOK.Enabled = True
            If txtCustomLocalFile.Text.Length > 0 Then
                btnOpenInBrowser.Enabled = False
            Else
                btnOpenInBrowser.Enabled = True
            End If
            If txtCustomLocalFile.Text.Length > 0 Then
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
            btnOpenInBrowser.Enabled = False
        End If
        SetEnabledSearch()
    End Sub

    Private Sub SetEnabledSearch()
        If Not String.IsNullOrEmpty(txtYouTubeSearch.Text.Trim) Then
            btnYouTubeSearch.Enabled = True
        Else
            btnYouTubeSearch.Enabled = False
        End If
    End Sub

    Private Function SetFormat() As Boolean
        Using dFormats As New dlgMediaFileSelectFormat
            If dFormats.ShowDialog(Result) = DialogResult.OK Then
                Result.URLAudioStream = dFormats.Result.AudioURL
                Result.URLVideoStream = dFormats.Result.VideoURL
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Private Sub txtManual_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCustomLocalFile.TextChanged
        SetEnabled()
    End Sub

    Private Sub StartDownload()
        bwDownloadMediaFile = New ComponentModel.BackgroundWorker With {
            .WorkerReportsProgress = True,
            .WorkerSupportsCancellation = True
        }
        bwDownloadMediaFile.RunWorkerAsync()
    End Sub

    Private Sub txtCustomURL_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtCustomURL.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtYouTubeSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtYouTubeSearch.TextChanged
        SetEnabledSearch()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim IsSearch As Boolean
        Dim SearchString As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class