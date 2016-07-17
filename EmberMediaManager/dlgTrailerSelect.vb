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

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCompileList As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadTrailer As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwParseTrailer As New System.ComponentModel.BackgroundWorker

    Private tmpDBElement As Database.DBElement
    Private _result As New MediaContainers.Trailer
    Private tArray As New List(Of String)
    Private tURL As String = String.Empty
    Private sPath As String
    Private nList As New List(Of MediaContainers.Trailer)
    Private _noDownload As Boolean
    Private _withPlayer As Boolean

#End Region 'Fields

#Region "Properties"

    Public Property Result As MediaContainers.Trailer
        Get
            Return _result
        End Get
        Set(value As MediaContainers.Trailer)
            _result = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub dlgTrailerSelect_FormClosing(sender As Object, e As System.EventArgs) Handles Me.FormClosing
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _withPlayer Then
            Dim paramsTrailerPreview As New List(Of Object)(New Object() {New Panel})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayer_Video, paramsTrailerPreview, Nothing, True)
            pnlTrailerPreview.Controls.Add(DirectCast(paramsTrailerPreview(0), Panel))
            If Not String.IsNullOrEmpty(pnlTrailerPreview.Controls.Item(1).Name) Then
                pnlTrailerPreviewNoPlayer.Visible = False
            End If
        End If
        SetUp()
        AddHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Database.DBElement, ByRef tURLList As List(Of MediaContainers.Trailer), ByVal OnlyToNFO As Boolean, Optional ByVal _isNew As Boolean = False, Optional ByVal WithPlayer As Boolean = False) As DialogResult
        _noDownload = OnlyToNFO
        _withPlayer = WithPlayer

        'set ListView
        lvTrailers.MultiSelect = False
        lvTrailers.FullRowSelect = True
        lvTrailers.HideSelection = False

        txtYouTubeSearch.Text = String.Concat(DBMovie.Movie.Title, " ", Master.eSettings.MovieTrailerDefaultSearch)

        tmpDBElement = DBMovie
        sPath = DBMovie.Filename

        AddTrailersToList(tURLList)

        pnlStatus.Visible = False
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

    Private Sub AddTrailersToList(ByVal tList As List(Of MediaContainers.Trailer))
        Dim ID As Integer = lvTrailers.Items.Count + 1
        Dim nList As List(Of MediaContainers.Trailer) = tList

        Dim str(10) As String
        For Each aUrl In nList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aUrl.URLVideoStream.ToString
            str(2) = aUrl.URLWebsite.ToString
            str(3) = aUrl.Title.ToString
            str(4) = aUrl.Duration.ToString
            str(5) = aUrl.Quality.ToString
            str(6) = aUrl.Type.ToString
            str(7) = aUrl.Source.ToString
            str(8) = aUrl.Scraper.ToString
            str(9) = aUrl.TrailerOriginal.Extention.ToString
            itm = New ListViewItem(str)
            lvTrailers.Items.Add(itm)
            ID = ID + 1
        Next
    End Sub

    Private Sub BeginDownload(ByVal CloseDialog As Boolean)
        Dim didCancel As Boolean = False

        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(906, "Downloading selected trailer...")
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pnlStatus.Visible = True
        Application.DoEvents()

        If txtLocalTrailer.Text.Length > 0 Then
            lblStatus.Text = Master.eLang.GetString(907, "Copying specified file to trailer...")
            If Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(txtLocalTrailer.Text)) AndAlso File.Exists(txtLocalTrailer.Text) Then
                If CloseDialog Then
                    If _noDownload Then
                        Result.URLWebsite = txtLocalTrailer.Text
                    Else
                        Result.TrailerOriginal.LoadFromFile(txtLocalTrailer.Text)
                    End If

                    DialogResult = DialogResult.OK
                    Close()
                Else
                    Process.Start(String.Concat("""", txtLocalTrailer.Text, """"))
                    didCancel = True
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(192, "File is not valid."), Master.eLang.GetString(194, "Not Valid"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                didCancel = True
            End If
        ElseIf YouTube.UrlUtils.IsYouTubeURL(txtManualTrailerLink.Text) OrElse
            Regex.IsMatch(txtManualTrailerLink.Text, "https?:\/\/.*imdb.*\/video\/imdb\/.*") Then
            Dim sFormat As New TrailerLinksContainer
            Using dFormats As New dlgTrailerFormat
                sFormat = dFormats.ShowDialog(txtManualTrailerLink.Text)
            End Using
            If _noDownload Then
                Result.URLWebsite = sFormat.VideoURL
                DialogResult = DialogResult.OK
                Close()
            Else
                If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                    bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                    bwDownloadTrailer.WorkerReportsProgress = True
                    bwDownloadTrailer.WorkerSupportsCancellation = True
                    bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                Else
                    didCancel = True
                End If
            End If
        ElseIf Regex.IsMatch(txtManualTrailerLink.Text, "https?:\/\/.*trailers.apple.com.*") Then
            Dim Apple As New Apple.Scraper

            Apple.GetTrailerLinks(txtManualTrailerLink.Text)

            If Apple.TrailerLinks IsNot Nothing Then
                AddTrailersToList(Apple.TrailerLinks)
                txtManualTrailerLink.Text = String.Empty
            End If

            didCancel = True
        ElseIf StringUtils.isValidURL(txtManualTrailerLink.Text) Then
            If _noDownload Then
                Result.URLWebsite = txtManualTrailerLink.Text
                DialogResult = DialogResult.OK
                Close()
            Else
                Dim ManualTrailer As New TrailerLinksContainer With {.VideoURL = txtManualTrailerLink.Text}
                bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                bwDownloadTrailer.WorkerReportsProgress = True
                bwDownloadTrailer.WorkerSupportsCancellation = True
                bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = ManualTrailer, .bType = CloseDialog})
            End If
        Else
            If YouTube.UrlUtils.IsYouTubeURL(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then
                If _noDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    Dim sFormat As New TrailerLinksContainer
                    Using dFormats As New dlgTrailerFormat
                        sFormat = dFormats.ShowDialog(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
                    End Using
                    If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                        bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                        bwDownloadTrailer.WorkerReportsProgress = True
                        bwDownloadTrailer.WorkerSupportsCancellation = True
                        bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End If
            ElseIf Regex.IsMatch(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString, "https?:\/\/.*imdb.*") Then
                Dim sFormat As New TrailerLinksContainer
                Using dFormats As New dlgTrailerFormat
                    sFormat = dFormats.ShowDialog(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
                End Using
                If _noDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                        bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                        bwDownloadTrailer.WorkerReportsProgress = True
                        bwDownloadTrailer.WorkerSupportsCancellation = True
                        bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End If
            Else
                If _noDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    Dim SelectedTrailer As New TrailerLinksContainer With {.VideoURL = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString}
                    bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                    bwDownloadTrailer.WorkerReportsProgress = True
                    bwDownloadTrailer.WorkerSupportsCancellation = True
                    bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = SelectedTrailer, .bType = CloseDialog})
                End If
            End If
        End If

        If didCancel Then
            pnlStatus.Visible = False
            SetControlsEnabled(True)
            SetEnabled()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseLocalTrailer.Click
        Try
            With ofdTrailer
                .InitialDirectory = Directory.GetParent(tmpDBElement.Filename).FullName
                .Filter = String.Concat("Supported Trailer Formats|*", Functions.ListToStringWithSeparator(Master.eSettings.FileSystemValidExts.ToArray(), ";*"))
                .FilterIndex = 0
            End With

            If ofdTrailer.ShowDialog() = DialogResult.OK Then
                txtLocalTrailer.Text = ofdTrailer.FileName
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnClearLink_Click(sender As Object, e As EventArgs) Handles btnClearManualTrailerLink.Click
        txtManualTrailerLink.Text = String.Empty
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayLocalTrailer.Click
        Try
            If Not String.IsNullOrEmpty(txtLocalTrailer.Text) Then
                TrailerAddToPlayer(txtLocalTrailer.Text)
            ElseIf Not String.IsNullOrEmpty(txtManualTrailerLink.Text) Then
                TrailerAddToPlayer(txtManualTrailerLink.Text)
            End If
        Catch
            MessageBox.Show(Master.eLang.GetString(908, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), Master.eLang.GetString(59, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            SetControlsEnabled(True)
            SetEnabled()
        End Try
    End Sub

    Private Sub btnPlayBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayInBrowser.Click
        If Not String.IsNullOrEmpty(txtManualTrailerLink.Text) Then
            If Master.isWindows Then
                Process.Start(txtManualTrailerLink.Text)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = txtManualTrailerLink.Text
                    Explorer.Start()
                End Using
            End If
        Else
            If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then
                If Master.isWindows Then
                    Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
                Else
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "xdg-open"
                        Explorer.StartInfo.Arguments = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                        Explorer.Start()
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub btnYouTubeSearch_Click(sender As Object, e As EventArgs) Handles btnYouTubeSearch.Click
        Dim ID As Integer = lvTrailers.Items.Count + 1
        Dim nList As New List(Of MediaContainers.Trailer)

        nList = YouTube.Scraper.SearchOnYouTube(txtYouTubeSearch.Text)
        AddTrailersToList(nList)
    End Sub

    Private Sub btnTrailerScrape_Click(sender As Object, e As EventArgs) Handles btnTrailerScrape.Click
        Dim didCancel As Boolean = False
        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(918, "Compiling trailer list...")
        pbStatus.Style = ProgressBarStyle.Marquee
        pnlStatus.Visible = True
        Application.DoEvents()

        nList.Clear()

        bwCompileList = New System.ComponentModel.BackgroundWorker
        bwCompileList.WorkerReportsProgress = False
        bwCompileList.WorkerSupportsCancellation = True
        bwCompileList.RunWorkerAsync(New Arguments With {.bType = False})
    End Sub

    Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            If Not ModulesManager.Instance.ScrapeTrailer_Movie(tmpDBElement, Enums.ModifierType.MainTrailer, nList) Then
                Args.bType = True
            Else
                Args.bType = False
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        e.Result = Args.bType

        If bwCompileList.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCompileList.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                If nList.Count > 0 Then
                    lvTrailers.Items.Clear()
                    AddTrailersToList(nList)
                Else
                    MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show(Master.eLang.GetString(225, "No Trailers found"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        pnlStatus.Visible = False
        SetControlsEnabled(True)
        SetEnabled()
    End Sub

    Private Sub bwDownloadTrailer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTrailer.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Result.TrailerOriginal.LoadFromWeb(Args.Parameter)
            Result.URLWebsite = Args.Parameter.VideoURL
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        e.Result = Args.bType

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
                Close()
            Else
                pnlStatus.Visible = False
                SetControlsEnabled(True)
                SetEnabled()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If bwDownloadTrailer.IsBusy Then bwDownloadTrailer.CancelAsync()

        While bwDownloadTrailer.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        DialogResult = DialogResult.Cancel
        Me.Result = Nothing
        Close()
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer, ByVal strInfo As String)
        bwDownloadTrailer.ReportProgress(iProgress, strInfo)
    End Sub

    Private Sub lvTrailers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvTrailers.SelectedIndexChanged
        SetEnabled()
    End Sub

    Private Sub lvTrailers_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvTrailers.DoubleClick
        If Not _withPlayer OrElse pnlTrailerPreviewNoPlayer.Visible Then
            If Master.isWindows Then
                Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    Explorer.Start()
                End Using
            End If
        Else
            If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(1).Text.ToString) Then
                Dim vLink As String = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                If Regex.IsMatch(vLink, "https?:\/\/.*imdb.*\/video\/imdb\/.*") Then
                    Using dFormats As New dlgTrailerFormat
                        Dim sFormat As TrailerLinksContainer = dFormats.ShowDialog(vLink)
                        TrailerAddToPlayer(sFormat.VideoURL)
                    End Using
                ElseIf Regex.IsMatch(vLink, "https?:\/\/movietrailers\.apple\.com.*?") OrElse Regex.IsMatch(vLink, "https?:\/\/trailers.apple.com\*?") Then
                    MessageBox.Show(String.Format(Master.eLang.GetString(1169, "Please use the {0}{1}{0} button for this trailer"), """", Master.eLang.GetString(931, "Open In Browser")), Master.eLang.GetString(271, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    TrailerAddToPlayer(vLink)
                End If
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        BeginDownload(True)
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        TrailerStop()

        OK_Button.Enabled = isEnabled
        btnBrowseLocalTrailer.Enabled = isEnabled
        btnClearManualTrailerLink.Enabled = isEnabled
        btnPlayInBrowser.Enabled = isEnabled
        btnPlayLocalTrailer.Enabled = isEnabled
        btnTrailerScrape.Enabled = isEnabled
        btnYouTubeSearch.Enabled = isEnabled
        lvTrailers.Enabled = isEnabled
        txtLocalTrailer.Enabled = isEnabled
        txtManualTrailerLink.Enabled = isEnabled
        txtYouTubeSearch.Enabled = isEnabled
    End Sub

    Private Sub SetEnabled()
        If StringUtils.isValidURL(txtManualTrailerLink.Text) OrElse lvTrailers.SelectedItems.Count > 0 OrElse txtLocalTrailer.Text.Length > 0 Then
            OK_Button.Enabled = True
            btnPlayLocalTrailer.Enabled = True
            If txtLocalTrailer.Text.Length > 0 Then
                btnPlayInBrowser.Enabled = False
            Else
                btnPlayInBrowser.Enabled = True
            End If
            If txtLocalTrailer.Text.Length > 0 Then
                If _noDownload Then
                    OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    OK_Button.Text = Master.eLang.GetString(911, "Copy")
                End If
            Else
                If _noDownload Then
                    OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    OK_Button.Text = Master.eLang.GetString(373, "Download")
                End If
            End If
        Else
            OK_Button.Enabled = False
            If _noDownload Then
                OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
            Else
                OK_Button.Text = Master.eLang.GetString(373, "Download")
            End If
            btnPlayLocalTrailer.Enabled = False
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

    Private Sub SetUp()
        If _withPlayer Then
            lblTrailerPreviewNoPlayer.Text = "no modules enabled"
        Else
            lblTrailerPreviewNoPlayer.Text = "preview not possible"
        End If
        Text = Master.eLang.GetString(914, "Select Trailer")
        OK_Button.Text = Master.eLang.GetString(373, "Download")
        colDescription.Text = Master.eLang.GetString(979, "Description")
        colDuration.Text = Master.eLang.GetString(609, "Duration")
        colVideoQuality.Text = Master.eLang.GetString(1138, "Quality")
        colSource.Text = Master.eLang.GetString(1173, "Source")
        btnPlayInBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        btnPlayLocalTrailer.Text = Master.eLang.GetString(919, "Preview Trailer")
        btnTrailerScrape.Text = Master.eLang.GetString(1171, "Scrape Trailers")
        btnYouTubeSearch.Text = Master.eLang.GetString(977, "Search")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        gbPreview.Text = Master.eLang.GetString(180, "Preview")
        gbSelectTrailer.Text = Master.eLang.GetString(915, "Select Trailer to Download")
        gbManualTrailer.Text = Master.eLang.GetString(916, "Manual Trailer Entry")
        gbYouTubeSearch.Text = Master.eLang.GetString(978, "Search On YouTube")
        lblLocalTrailer.Text = Master.eLang.GetString(920, "Local Trailer:")
        lblManualTrailerLink.Text = Master.eLang.GetString(917, "Direct Link, YouTube, IMDB or Apple Trailer URL:")
        lblStatus.Text = Master.eLang.GetString(918, "Compiling trailer list...")
    End Sub

    Private Sub TrailerStart()

        'Cocotus 2014/10/09 Check if there's at least one video in playlist before attempt to change button...
        'If Me.axVLCTrailer.playlist.items.count > 0 Then
        '    If Me.axVLCTrailer.playlist.isPlaying Then
        '        Me.axVLCTrailer.playlist.togglePause()
        '        Me.btnTrailerPlay.Enabled = True
        '        Me.btnTrailerPlay.Text = "Play"
        '        Me.btnTrailerStop.Enabled = True
        '    Else
        '        Me.axVLCTrailer.playlist.play()
        '        Me.btnTrailerPlay.Enabled = True
        '        Me.btnTrailerPlay.Text = "Pause"
        '        Me.btnTrailerStop.Enabled = True
        '    End If
        'Else
        '    'nothing to play
        '    Me.axVLCTrailer.playlist.togglePause()
        '    Me.btnTrailerPlay.Enabled = False
        '    Me.btnTrailerPlay.Text = "Play"
        '    Me.btnTrailerStop.Enabled = False
        'End If
    End Sub

    Private Sub TrailerStop()
        'Me.axVLCTrailer.playlist.stop()
        'Me.btnTrailerPlay.Text = "Play"
        'Me.btnTrailerStop.Enabled = False
        'Me.btnTrailerPlay.Enabled = False
    End Sub

    Private Sub TrailerAddToPlayer(ByVal Trailer As String)
        If _withPlayer Then
            If pnlTrailerPreview.Controls.Item(1).Tag IsNot Nothing AndAlso pnlTrailerPreview.Controls.Item(1).Tag.ToString = "vPlayer" Then
                pnlTrailerPreview.Controls.RemoveAt(1)
            End If
            Dim paramsTrailerPreview As New List(Of Object)(New Object() {New Panel, Trailer})
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MediaPlayer_Video, paramsTrailerPreview, Nothing, True)
            pnlTrailerPreview.Controls.Add(DirectCast(paramsTrailerPreview(0), Panel))
            If Not String.IsNullOrEmpty(pnlTrailerPreview.Controls.Item(1).Name) Then
                pnlTrailerPreviewNoPlayer.Visible = False
            End If
        Else

        End If
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalTrailer.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtManualTrailerLink_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtManualTrailerLink.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtYouTubeSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYouTubeSearch.TextChanged
        SetEnabledSearch()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments
#Region "Fields"

        Dim bType As Boolean
        Dim Parameter As TrailerLinksContainer

#End Region 'Fields

    End Structure


#End Region 'Nested Types

End Class