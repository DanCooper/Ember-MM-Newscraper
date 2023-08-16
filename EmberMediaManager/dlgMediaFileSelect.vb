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
    Private _disableOfd As Boolean = False
    Private _URL As String = String.Empty
    Private _MediaFileList As New List(Of MediaContainers.MediaFile)
    Private _NoDownload As Boolean
    Private _PreferredVideoResolution As Enums.VideoResolution = Enums.VideoResolution.Any
    Private _MediaType As Enums.ModifierType

#End Region 'Fields

#Region "Properties"

    Public Property Result As MediaContainers.MediaFile

#End Region 'Properties

#Region "Dialog"

    Public Sub New(ByVal type As Enums.ModifierType, Optional disableOfd As Boolean = False)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _disableOfd = disableOfd
        _MediaType = type
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

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                Select Case _MediaType
                    Case Enums.ModifierType.MainTheme
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.MainDetails.Title, Master.eSettings.MovieThemeDefaultSearch)
                    Case Enums.ModifierType.MainTrailer
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.MainDetails.Title, Master.eSettings.MovieTrailerDefaultSearch)
                        _PreferredVideoResolution = Master.eSettings.MovieTrailerPrefVideoQual
                End Select
            Case Enums.ContentType.TVShow
                Select Case _MediaType
                    Case Enums.ModifierType.MainTheme
                        txtYouTubeSearch.Text = String.Format("{0} {1}", dbElement.MainDetails.Title, Master.eSettings.TVShowThemeDefaultSearch)
                End Select
        End Select

        _TmpDBElement = dbElement

        DataGridView_AddMediaFiles(urlList)

        lblStatus.Visible = False
        pbStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()

        Return ShowDialog()
    End Function

    Private Sub Setup()
        Select Case _MediaType
            Case Enums.ModifierType.MainTheme
                Text = Master.eLang.GetString(916, "Select Theme to download")
            Case Enums.ModifierType.MainTrailer
                Text = Master.eLang.GetString(915, "Select Trailer to download")
        End Select
        If _disableOfd Then btnCustomLocalFile_Browse.Enabled = False
        btnOK.Text = Master.eLang.GetString(373, "Download")
        colMediaFileAddon.HeaderText = Master.eLang.GetString(1117, "Addon")
        colMediaFileDuration.HeaderText = Master.eLang.GetString(609, "Duration")
        colMediaFileLanguage.HeaderText = Master.eLang.GetString(610, "Language")
        colMediaFileSource.HeaderText = Master.eLang.GetString(1173, "Source")
        colMediaFileTitel.HeaderText = Master.eLang.GetString(21, "Title")
        colMediaFileVariant.HeaderText = Master.eLang.GetString(1136, "Variant")
        colMediaFileVideoType.HeaderText = Master.eLang.GetString(1143, "Type")
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
            Dim strFilter = String.Empty
            Select Case _MediaType
                Case Enums.ModifierType.MainTheme
                    strFilter = FileUtils.Common.GetOpenFileDialogFilter_Theme()
                Case Enums.ModifierType.MainTrailer
                    strFilter = FileUtils.Common.GetOpenFileDialogFilter_Video(Master.eLang.GetString(1195, "Trailers"))
            End Select

            With ofdFile
                .InitialDirectory = Directory.GetParent(_TmpDBElement.Filename).FullName
                .Filter = strFilter
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
            If Not String.IsNullOrEmpty(dgvMediaFiles.SelectedRows(0).Cells("colMediaFileUrlWebsite").Value.ToString) Then
                Process.Start(dgvMediaFiles.SelectedRows(0).Cells("colMediaFileUrlWebsite").Value.ToString)
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
        bwCompileList.RunWorkerAsync(New Arguments With {.Type = CompileType.Scrape})
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
        bwCompileList.RunWorkerAsync(New Arguments With {
                                     .SearchString = txtYouTubeSearch.Text,
                                     .Type = CompileType.YouTubeSearch})
    End Sub

    Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Try
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            Select Case Args.Type
                Case CompileType.Scrape
                    Functions.SetScrapeModifiers(_TmpDBElement.ScrapeModifiers, _MediaType, True)
                    Select Case _MediaType
                        Case Enums.ModifierType.MainTheme
                            _MediaFileList = Scraper.Run(_TmpDBElement).Themes
                            e.Result = _MediaFileList.Count > 0
                        Case Enums.ModifierType.MainTrailer
                            _MediaFileList = Scraper.Run(_TmpDBElement).Trailers
                            e.Result = _MediaFileList.Count > 0
                    End Select
                Case CompileType.YouTubeParse
                    If Not String.IsNullOrEmpty(Args.SearchString) Then
                        Dim nMediaFile = YouTube.Scraper.GetVideoDetails(Args.SearchString)
                        If nMediaFile IsNot Nothing Then
                            nMediaFile.Streams.BuildStreamVariants()
                            _MediaFileList.Add(nMediaFile)
                        End If
                    End If
                    e.Result = _MediaFileList.Count > 0
                Case CompileType.YouTubeSearch
                    _MediaFileList = YouTube.Scraper.SearchOnYouTube(Args.SearchString, _MediaType = Enums.ModifierType.MainTheme)
                    e.Result = _MediaFileList.Count > 0
            End Select
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        If bwCompileList.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCompileList.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) AndAlso _MediaFileList.Count > 0 Then
                dgvMediaFiles.Rows.Clear()
                DataGridView_AddMediaFiles(_MediaFileList)
            Else
                Select Case _MediaType
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

    Private Sub DataGridView_AddMediaFiles(ByVal mediafiles As List(Of MediaContainers.MediaFile))
        dgvMediaFiles.SuspendLayout()

        For Each nMediaFile In mediafiles
            Dim i As Integer = dgvMediaFiles.Rows.Add()
            dgvMediaFiles.Rows(i).Tag = nMediaFile
            dgvMediaFiles.Rows(i).Cells(colMediaFileAddon.Name).Value = nMediaFile.Scraper
            dgvMediaFiles.Rows(i).Cells(colMediaFileDuration.Name).Value = nMediaFile.Duration
            dgvMediaFiles.Rows(i).Cells(colMediaFileLanguage.Name).Value = nMediaFile.LongLanguage
            dgvMediaFiles.Rows(i).Cells(colMediaFileSource.Name).Value = nMediaFile.Source
            dgvMediaFiles.Rows(i).Cells(colMediaFileTitel.Name).Value = nMediaFile.Title
            dgvMediaFiles.Rows(i).Cells(colMediaFileVideoType.Name).Value = nMediaFile.VideoType
            dgvMediaFiles.Rows(i).Cells(colMediaFileUrlWebsite.Name).Value = nMediaFile.UrlWebsite
            Dim lstVariants As New List(Of VariantInformation)
            Select Case _MediaType
                Case Enums.ModifierType.MainTheme
                    For Each nVariant In nMediaFile.Streams.Variants.Where(Function(f) f.StreamType = MediaContainers.MediaFile.StreamCollection.StreamType.Audio)
                        lstVariants.Add(New VariantInformation With {
                                            .Description = nVariant.Description,
                                            .Stream = nVariant
                                            })
                    Next
                Case Enums.ModifierType.MainTrailer
                    For Each nVariant In nMediaFile.Streams.Variants.Where(Function(f) f.StreamType = MediaContainers.MediaFile.StreamCollection.StreamType.Video)
                        lstVariants.Add(New VariantInformation With {
                                            .Description = nVariant.Description,
                                            .Stream = nVariant
                                            })
                    Next
            End Select
            Dim nBindingSource As New BindingSource With {.DataSource = lstVariants.ToArray}
            Dim dcbVariants As New DataGridViewComboBoxCell With {
                .DataSource = nBindingSource,
                .DisplayMember = "Description",
                .ValueMember = "Stream"
            }
            Dim prefVariant As VariantInformation = Nothing
            Select Case _MediaType
                Case Enums.ModifierType.MainTheme
                    prefVariant = lstVariants.Find(Function(f) f.Stream.AudioBitrate = Enums.AudioBitrate.Q128kbps)
                Case Enums.ModifierType.MainTrailer
                    prefVariant = lstVariants.Find(Function(f) f.Stream.VideoResolution = _PreferredVideoResolution)
            End Select
            If prefVariant IsNot Nothing Then
                dcbVariants.Value = prefVariant.Stream
            ElseIf lstVariants.Count > 0 Then
                dcbVariants.Value = lstVariants(0).Stream
            End If
            dgvMediaFiles.Rows(i).Cells(colMediaFileVariant.Name) = dcbVariants
        Next

        dgvMediaFiles.ResumeLayout()
        dgvMediaFiles.Enabled = True
    End Sub

    Private Sub DataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvMediaFiles.DataError
        'do nothing
    End Sub

    Private Sub DataGridView_DoubleClick_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMediaFiles.CellDoubleClick
        'skip column header row, video and audio stream column
        If Not e.RowIndex = -1 AndAlso
            Not e.ColumnIndex = dgvMediaFiles.Columns(colMediaFileVariant.Name).Index Then
            If Not String.IsNullOrEmpty(dgvMediaFiles.SelectedRows(0).Cells(colMediaFileUrlWebsite.Name).Value.ToString) Then Process.Start(dgvMediaFiles.SelectedRows(0).Cells(colMediaFileUrlWebsite.Name).Value.ToString)
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMediaFiles.SelectionChanged
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
                'Local file
                '
                lblStatus.Text = String.Concat(Master.eLang.GetString(907, "Copying specified file"), "...")
                If Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(Path.GetExtension(txtCustomLocalFile.Text.Trim)) AndAlso File.Exists(txtCustomLocalFile.Text.Trim) Then
                    If _NoDownload Then
                        Result = New MediaContainers.MediaFile With {
                            .UrlForNfo = txtCustomLocalFile.Text.Trim
                        }
                    Else
                        Result = New MediaContainers.MediaFile
                        Result.FileOriginal.LoadFromFile(txtCustomLocalFile.Text.Trim)
                    End If
                    DialogResult = DialogResult.OK
                Else
                    MessageBox.Show(Master.eLang.GetString(192, "File is not valid."), Master.eLang.GetString(194, "Not Valid"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    didCancel = True
                End If
            Case YouTube.UrlUtils.IsYouTubeUrl(txtCustomURL.Text.Trim)
                '
                'Manual YouTube URL
                '
                Dim strYouTubeUrl = txtCustomURL.Text.Trim
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
                bwCompileList.RunWorkerAsync(New Arguments With {
                                     .SearchString = strYouTubeUrl,
                                     .Type = CompileType.YouTubeParse})
            Case StringUtils.IsValidURL(txtCustomURL.Text.Trim)
                '
                'Manual direct URL
                '
                If _NoDownload Then
                    Result = New MediaContainers.MediaFile With {
                        .UrlForNfo = txtCustomURL.Text.Trim
                    }
                    DialogResult = DialogResult.OK
                Else
                    Select Case _MediaType
                        Case Enums.ModifierType.MainTheme
                            Result = New MediaContainers.MediaFile With {.UrlAudioStream = txtCustomURL.Text.Trim}
                        Case Enums.ModifierType.MainTrailer
                            Result = New MediaContainers.MediaFile With {.UrlVideoStream = txtCustomURL.Text.Trim}
                    End Select
                    bwDownloadMediaFile = New ComponentModel.BackgroundWorker With {
                        .WorkerReportsProgress = True,
                        .WorkerSupportsCancellation = True
                    }
                    bwDownloadMediaFile.RunWorkerAsync()
                End If
            Case dgvMediaFiles.SelectedRows.Count = 1 AndAlso
                String.IsNullOrEmpty(txtCustomLocalFile.Text.Trim) AndAlso
                String.IsNullOrEmpty(txtCustomURL.Text.Trim)
                '
                'List item
                '
                Result = DirectCast(dgvMediaFiles.SelectedRows(0).Tag, MediaContainers.MediaFile)
                If Result IsNot Nothing Then
                    If Result.StreamsSpecified Then
                        Dim selStreamVaraint = DirectCast(dgvMediaFiles.SelectedRows(0).Cells(colMediaFileVariant.Name).Value, MediaContainers.MediaFile.StreamCollection.StreamVariant)
                        If selStreamVaraint IsNot Nothing Then
                            Result.SetVariant(selStreamVaraint)
                            If _NoDownload Then
                                DialogResult = DialogResult.OK
                            Else
                                StartDownload()
                            End If
                        Else
                            didCancel = True
                        End If
                    ElseIf Result.UrlVideoStreamSpecified OrElse Result.UrlAudioStreamSpecified Then
                        If _NoDownload Then
                            DialogResult = DialogResult.OK
                        Else
                            StartDownload()
                        End If
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
        btnCustomLocalFile_Browse.Enabled = isEnabled AndAlso Not _disableOfd
        btnCustomURL_Remove.Enabled = isEnabled
        btnOpenInBrowser.Enabled = isEnabled
        btnScrape.Enabled = isEnabled
        btnYouTubeSearch.Enabled = isEnabled
        dgvMediaFiles.Enabled = isEnabled
        txtCustomLocalFile.Enabled = isEnabled
        txtCustomURL.Enabled = isEnabled
        txtYouTubeSearch.Enabled = isEnabled
    End Sub

    Private Sub SetEnabled()
        If StringUtils.IsValidURL(txtCustomURL.Text) OrElse
            dgvMediaFiles.SelectedRows.Count > 0 OrElse
            txtCustomLocalFile.Text.Length > 0 Then
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

    Private Enum CompileType
        Scrape
        YouTubeParse
        YouTubeSearch
    End Enum

    Private Structure Arguments

#Region "Fields"

        Dim SearchString As String
        Dim Type As CompileType

#End Region 'Fields

    End Structure

    Public Class VariantInformation

        Public Property Description As String = String.Empty

        Public Property Stream As New MediaContainers.MediaFile.StreamCollection.StreamVariant

    End Class

#End Region 'Nested Types

End Class