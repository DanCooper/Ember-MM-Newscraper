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
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCompileList As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadTrailer As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwParseTrailer As New System.ComponentModel.BackgroundWorker

    Private tMovie As New Structures.DBMovie
    Private _results As New MediaContainers.Trailer
    Private tArray As New List(Of String)
    Private tURL As String = String.Empty
    Private sPath As String
    Private nList As New List(Of Trailers)
    Private noDownload As Boolean

#End Region 'Fields

#Region "Properties"
    Public Property Results As MediaContainers.Trailer
        Get
            Return _results
        End Get
        Set(value As MediaContainers.Trailer)
            _results = value
        End Set
    End Property
#End Region 'Properties

#Region "Methods"

    Private Sub dlgTrailerSelect_Leave(sender As Object, e As System.EventArgs) Handles Me.Leave
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        AddHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByRef tURLList As List(Of Trailers), ByVal OnlyToNFO As Boolean, Optional ByVal _isNew As Boolean = False) As DialogResult
        Me.noDownload = OnlyToNFO

        'set ListView
        Me.lvTrailers.MultiSelect = False
        Me.lvTrailers.FullRowSelect = True
        Me.lvTrailers.HideSelection = False

        Me.txtYouTubeSearch.Text = String.Concat(DBMovie.Movie.Title, " ", Master.eSettings.MovieTrailerDefaultSearch)

        Me.tMovie = DBMovie
        Me.sPath = DBMovie.Filename

        AddTrailersToList(tURLList)

        Me.pnlStatus.Visible = False
        Me.SetControlsEnabled(True)
        Me.SetEnabled()
        If Me.lvTrailers.Items.Count = 1 Then
            Me.lvTrailers.Select()
            Me.lvTrailers.Items(0).Selected = True
        End If
        Me.btnTrailerPlay.Enabled = False
        Me.btnTrailerStop.Enabled = False
        Return MyBase.ShowDialog()
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub AddTrailersToList(ByVal tList As List(Of Trailers))
        Dim ID As Integer = Me.lvTrailers.Items.Count + 1
        Dim nList As List(Of Trailers) = tList

        Dim str(7) As String
        For Each aUrl In nList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aUrl.URL.ToString
            str(2) = aUrl.WebURL.ToString
            str(3) = aUrl.Description.ToString
            str(4) = aUrl.Duration.ToString
            str(5) = aUrl.Quality.ToString
            str(6) = aUrl.Source.ToString
            str(7) = aUrl.Extention.ToString
            itm = New ListViewItem(str)
            lvTrailers.Items.Add(itm)
            ID = ID + 1
        Next
    End Sub

    Private Sub BeginDownload(ByVal CloseDialog As Boolean)
        Dim didCancel As Boolean = False

        Me.SetControlsEnabled(False)
        Me.lblStatus.Text = Master.eLang.GetString(906, "Downloading selected trailer...")
        Me.pbStatus.Style = ProgressBarStyle.Continuous
        Me.pbStatus.Value = 0
        Me.pnlStatus.Visible = True
        Application.DoEvents()

        If Me.txtLocalTrailer.Text.Length > 0 Then
            Me.lblStatus.Text = Master.eLang.GetString(907, "Copying specified file to trailer...")
            If Master.eSettings.FileSystemValidExts.Contains(Path.GetExtension(Me.txtLocalTrailer.Text)) AndAlso File.Exists(Me.txtLocalTrailer.Text) Then
                If CloseDialog Then
                    If Me.noDownload Then
                        Results.URL = Me.txtLocalTrailer.Text
                    Else
                        Results.WebTrailer.FromFile(Me.txtLocalTrailer.Text)
                    End If

                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    System.Diagnostics.Process.Start(String.Concat("""", Me.txtLocalTrailer.Text, """"))
                    didCancel = True
                End If
            Else
                MsgBox(Master.eLang.GetString(192, "File is not valid."), MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Master.eLang.GetString(194, "Not Valid"))
                didCancel = True
            End If
        ElseIf Regex.IsMatch(Me.txtManualTrailerLink.Text, "https?:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") OrElse _
            Regex.IsMatch(Me.txtManualTrailerLink.Text, "https?:\/\/.*imdb.*\/video\/imdb\/.*") Then
            Dim sFormat As String = String.Empty
            Using dFormats As New dlgTrailerFormat
                sFormat = dFormats.ShowDialog(Me.txtManualTrailerLink.Text)
            End Using
            If Me.noDownload Then
                Results.URL = sFormat
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                If Not String.IsNullOrEmpty(sFormat) Then
                    Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                    Me.bwDownloadTrailer.WorkerReportsProgress = True
                    Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                    Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                Else
                    didCancel = True
                End If
            End If
        ElseIf Regex.IsMatch(Me.txtManualTrailerLink.Text, "https?:\/\/.*trailers.apple.com.*") Then
            Dim Apple As New Apple.Scraper

            Apple.GetTrailerLinks(Me.txtManualTrailerLink.Text)

            If Not IsNothing(Apple.TrailerLinks) Then
                AddTrailersToList(Apple.TrailerLinks)
                Me.txtManualTrailerLink.Text = String.Empty
            End If

            didCancel = True
        ElseIf StringUtils.isValidURL(Me.txtManualTrailerLink.Text) Then
            If Me.noDownload Then
                Results.URL = Me.txtManualTrailerLink.Text
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                Me.bwDownloadTrailer.WorkerReportsProgress = True
                Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.parameter = Me.txtManualTrailerLink.Text, .bType = CloseDialog})
            End If
        Else
            If Regex.IsMatch(Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString, "https?:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                If Me.noDownload Then
                    Results.URL = Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    Dim sFormat As String = String.Empty
                    Using dFormats As New dlgTrailerFormat
                        sFormat = dFormats.ShowDialog(Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString)
                    End Using
                    If Not String.IsNullOrEmpty(sFormat) Then
                        Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                        Me.bwDownloadTrailer.WorkerReportsProgress = True
                        Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                        Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End If
            ElseIf Regex.IsMatch(Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString, "https?:\/\/.*imdb.*") Then
                Dim sFormat As String = String.Empty
                Using dFormats As New dlgTrailerFormat
                    sFormat = dFormats.ShowDialog(Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString)
                End Using
                If Me.noDownload Then
                    Results.URL = Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    If Not String.IsNullOrEmpty(sFormat) Then
                        Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                        Me.bwDownloadTrailer.WorkerReportsProgress = True
                        Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                        Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End If
            Else
                If Me.noDownload Then
                    Results.URL = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                    Me.bwDownloadTrailer.WorkerReportsProgress = True
                    Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                    Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.parameter = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString, .bType = CloseDialog})
                End If
            End If
        End If

        If didCancel Then
            Me.pnlStatus.Visible = False
            Me.SetControlsEnabled(True)
            Me.SetEnabled()
            Me.btnTrailerPlay.Enabled = False
            Me.btnTrailerStop.Enabled = False
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseLocalTrailer.Click
        Try
            With ofdTrailer
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = String.Concat("Supported Trailer Formats|*", Functions.ListToStringWithSeparator(Master.eSettings.FileSystemValidExts.ToArray(), ";*"))
                .FilterIndex = 0
            End With

            If ofdTrailer.ShowDialog() = DialogResult.OK Then
                txtLocalTrailer.Text = ofdTrailer.FileName
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnClearLink_Click(sender As Object, e As EventArgs) Handles btnClearManualTrailerLink.Click
        Me.txtManualTrailerLink.Text = String.Empty
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayLocalTrailer.Click
        Try
            If Not String.IsNullOrEmpty(Me.txtLocalTrailer.Text) Then
                TrailerAddToPlayer(Me.txtLocalTrailer.Text)
            End If
        Catch
            MsgBox(Master.eLang.GetString(908, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), MsgBoxStyle.Critical, Master.eLang.GetString(59, "Error Playing Trailer"))
            Me.SetControlsEnabled(True)
            Me.SetEnabled()
            Me.btnTrailerPlay.Enabled = False
            Me.btnTrailerStop.Enabled = False
        End Try
    End Sub

    Private Sub btnPlayBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayInBrowser.Click
        If Not String.IsNullOrEmpty(Me.txtManualTrailerLink.Text) Then
            If Master.isWindows Then
                Process.Start(Me.txtManualTrailerLink.Text)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = Me.txtManualTrailerLink.Text
                    Explorer.Start()
                End Using
            End If
        Else
            If Master.isWindows Then
                Process.Start(Me.lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = Me.lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    Explorer.Start()
                End Using
            End If
        End If
    End Sub

    Private Async Sub btnYouTubeSearch_Click(sender As Object, e As EventArgs) Handles btnYouTubeSearch.Click
        Dim ID As Integer = Me.lvTrailers.Items.Count + 1
        Dim nList As New List(Of Trailers)

        nList = Await YouTube.Scraper.SearchOnYouTube(txtYouTubeSearch.Text)
        AddTrailersToList(nList)
    End Sub

    Private Sub btnTrailerScrape_Click(sender As Object, e As EventArgs) Handles btnTrailerScrape.Click
        Dim didCancel As Boolean = False
        '     Me.lvTrailers.Clear()
        Me.SetControlsEnabled(False)
        Me.lblStatus.Text = Master.eLang.GetString(918, "Compiling trailer list...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.pnlStatus.Visible = True
        Application.DoEvents()

        Me.nList.Clear()

        Me.bwCompileList = New System.ComponentModel.BackgroundWorker
        Me.bwCompileList.WorkerReportsProgress = False
        Me.bwCompileList.WorkerSupportsCancellation = True
        Me.bwCompileList.RunWorkerAsync(New Arguments With {.bType = False})
    End Sub

    Private Sub btnTrailerPlay_Click(sender As Object, e As EventArgs) Handles btnTrailerPlay.Click
        Me.TrailerStart()
    End Sub

    Private Sub btnTrailerStop_Click(sender As Object, e As EventArgs) Handles btnTrailerStop.Click
        Me.TrailerStop()
    End Sub

    Private Sub btnTrailerMute_Click(sender As Object, e As EventArgs) Handles btnTrailerMute.Click
        Me.axVLCTrailer.audio.toggleMute()
    End Sub

    Private Async Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim ret As Interfaces.ModuleResult
        Try
            ret = Await ModulesManager.Instance.ScrapeTrailer_Movie(Master.currMovie, Enums.ScraperCapabilities.Trailer, nList)
            ' return object
            ' DBMovie
            ' URLList
            Master.currMovie = CType(ret.ReturnObj(0), Structures.DBMovie)
            nList = CType(ret.ReturnObj(1), Global.System.Collections.Generic.List(Of Global.EmberAPI.Trailers))
            If Not ret.Cancelled Then
                Args.bType = True
            Else
                Args.bType = False
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        e.Result = Args.bType

        If Me.bwCompileList.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCompileList.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                If Me.nList.Count > 0 Then
                    AddTrailersToList(nList)
                Else
                    MsgBox(Master.eLang.GetString(1161, "No trailers could be found. Please check to see if any trailer scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(225, "No Trailers Found"))
                End If
            Else
                MsgBox(Master.eLang.GetString(1161, "No trailers could be found. Please check to see if any trailer scrapers are enabled."), MsgBoxStyle.Information, Master.eLang.GetString(225, "No Trailers Found"))
            End If
        End If
        Me.pnlStatus.Visible = False
        Me.SetControlsEnabled(True)
        Me.SetEnabled()
        Me.btnTrailerPlay.Enabled = False
        Me.btnTrailerStop.Enabled = False
    End Sub

    Private Sub bwDownloadTrailer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTrailer.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Using Trailer As New Trailers()
                Results.WebTrailer.FromWeb(Args.Parameter)
                Results.URL = Args.Parameter
            End Using

        Catch
        End Try

        e.Result = Args.bType

        If Me.bwDownloadTrailer.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwDownloadTrailer_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDownloadTrailer.ProgressChanged
        pbStatus.Value = e.ProgressPercentage
        Application.DoEvents()
    End Sub

    Private Sub bwDownloadTrailer_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadTrailer.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Me.pnlStatus.Visible = False
                Me.SetControlsEnabled(True)
                Me.SetEnabled()
                Me.btnTrailerPlay.Enabled = False
                Me.btnTrailerStop.Enabled = False
            End If
        End If
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If Me.bwDownloadTrailer.IsBusy Then Me.bwDownloadTrailer.CancelAsync()

        While Me.bwDownloadTrailer.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Results = Nothing
        Me.Close()
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer)
        bwDownloadTrailer.ReportProgress(iProgress)
    End Sub

    Private Sub lvTrailers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvTrailers.SelectedIndexChanged
        Me.SetEnabled()
    End Sub

    Private Sub lvTrailers_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvTrailers.DoubleClick
        If Not String.IsNullOrEmpty(Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString) Then
            Dim vLink As String = Me.lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
            If Regex.IsMatch(vLink, "https?:\/\/.*imdb.*\/video\/imdb\/.*") Then
                Using dFormats As New dlgTrailerFormat
                    Dim sFormat As String = dFormats.ShowDialog(vLink)
                    Me.TrailerAddToPlayer(sFormat)
                End Using
            ElseIf Regex.IsMatch(vLink, "https?:\/\/movietrailers\.apple\.com.*?") OrElse Regex.IsMatch(vLink, "https?:\/\/trailers.apple.com\*?") Then
                MsgBox(String.Format(Master.eLang.GetString(1169, "Please use the {0}{1}{0} button for this trailer"), """", Master.eLang.GetString(931, "Open In Browser")), MsgBoxStyle.Information, Master.eLang.GetString(271, "Error Playing Trailer"))
            Else
                Me.TrailerAddToPlayer(vLink)
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.BeginDownload(True)
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        Me.TrailerStop()

        Me.OK_Button.Enabled = isEnabled
        Me.btnBrowseLocalTrailer.Enabled = isEnabled
        Me.btnClearManualTrailerLink.Enabled = isEnabled
        Me.btnPlayInBrowser.Enabled = isEnabled
        Me.btnPlayLocalTrailer.Enabled = isEnabled
        Me.btnTrailerMute.Enabled = isEnabled
        Me.btnTrailerPlay.Enabled = isEnabled
        Me.btnTrailerScrape.Enabled = isEnabled
        Me.btnTrailerStop.Enabled = isEnabled
        Me.btnYouTubeSearch.Enabled = isEnabled
        Me.lvTrailers.Enabled = isEnabled
        Me.txtLocalTrailer.Enabled = isEnabled
        Me.txtManualTrailerLink.Enabled = isEnabled
        Me.txtYouTubeSearch.Enabled = isEnabled
    End Sub

    Private Sub SetEnabled()
        If StringUtils.isValidURL(Me.txtManualTrailerLink.Text) OrElse Me.lvTrailers.SelectedItems.Count > 0 OrElse Me.txtLocalTrailer.Text.Length > 0 Then
            Me.OK_Button.Enabled = True
            Me.btnPlayLocalTrailer.Enabled = True
            If Me.txtLocalTrailer.Text.Length > 0 Then
                Me.btnPlayInBrowser.Enabled = False
            Else
                Me.btnPlayInBrowser.Enabled = True
            End If
            If Me.txtLocalTrailer.Text.Length > 0 Then
                If Me.noDownload Then
                    Me.OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    Me.OK_Button.Text = Master.eLang.GetString(911, "Copy")
                End If
            Else
                If Me.noDownload Then
                    Me.OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
                Else
                    Me.OK_Button.Text = Master.eLang.GetString(373, "Download")
                End If
            End If
        Else
            Me.OK_Button.Enabled = False
            If Me.noDownload Then
                Me.OK_Button.Text = Master.eLang.GetString(913, "Set To Nfo")
            Else
                Me.OK_Button.Text = Master.eLang.GetString(373, "Download")
            End If
            Me.btnPlayLocalTrailer.Enabled = False
            Me.btnPlayInBrowser.Enabled = False
        End If
        Me.SetEnabledSearch()
    End Sub

    Private Sub SetEnabledSearch()
        If Not String.IsNullOrEmpty(txtYouTubeSearch.Text) Then
            Me.btnYouTubeSearch.Enabled = True
        Else
            Me.btnYouTubeSearch.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(914, "Select Trailer")
        Me.OK_Button.Text = Master.eLang.GetString(373, "Download")
        Me.colDescription.Text = Master.eLang.GetString(979, "Description")
        Me.colDuration.Text = Master.eLang.GetString(609, "Duration")
        Me.colQuality.Text = Master.eLang.GetString(1138, "Quality")
        Me.colSource.Text = Master.eLang.GetString(1173, "Source")
        Me.btnPlayInBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        Me.btnPlayLocalTrailer.Text = Master.eLang.GetString(919, "Preview Trailer")
        Me.btnTrailerScrape.Text = Master.eLang.GetString(1171, "Scrape Trailers")
        Me.btnYouTubeSearch.Text = Master.eLang.GetString(977, "Search")
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.gbPreview.Text = Master.eLang.GetString(180, "Preview")
        Me.gbSelectTrailer.Text = Master.eLang.GetString(915, "Select Trailer to Download")
        Me.gbManualTrailer.Text = Master.eLang.GetString(916, "Manual Trailer Entry")
        Me.gbYouTubeSearch.Text = Master.eLang.GetString(978, "Search On YouTube")
        Me.lblLocalTrailer.Text = Master.eLang.GetString(920, "Local Trailer:")
        Me.lblManualTrailerLink.Text = Master.eLang.GetString(917, "Direct Link, YouTube, IMDB or Apple Trailer URL:")
        Me.lblStatus.Text = Master.eLang.GetString(918, "Compiling trailer list...")
    End Sub

    Private Sub TrailerStart()

        'Cocotus 2014/10/09 Check if there's at least one video in playlist before attempt to change button...
        If Me.axVLCTrailer.playlist.items.count > 0 Then
            If Me.axVLCTrailer.playlist.isPlaying Then
                Me.axVLCTrailer.playlist.togglePause()
                Me.btnTrailerPlay.Enabled = True
                Me.btnTrailerPlay.Text = "Play"
                Me.btnTrailerStop.Enabled = True
            Else
                Me.axVLCTrailer.playlist.play()
                Me.btnTrailerPlay.Enabled = True
                Me.btnTrailerPlay.Text = "Pause"
                Me.btnTrailerStop.Enabled = True
            End If
        Else
            'nothing to play
            Me.axVLCTrailer.playlist.togglePause()
            Me.btnTrailerPlay.Enabled = False
            Me.btnTrailerPlay.Text = "Play"
            Me.btnTrailerStop.Enabled = False
        End If
    End Sub

    Private Sub TrailerStop()
        Me.axVLCTrailer.playlist.stop()
        Me.btnTrailerPlay.Text = "Play"
        Me.btnTrailerStop.Enabled = False
        Me.btnTrailerPlay.Enabled = False
    End Sub

    Private Sub TrailerAddToPlayer(ByVal Trailer As String)
        Me.axVLCTrailer.playlist.stop()
        Me.axVLCTrailer.playlist.items.clear()

        If Not String.IsNullOrEmpty(Trailer) Then
            If Regex.IsMatch(Trailer, "http:\/\/.*?") Then
                Me.axVLCTrailer.playlist.add(Trailer)
                Me.TrailerStart()
            Else
                Me.axVLCTrailer.playlist.add(String.Concat("file:///", Trailer))
                Me.TrailerStart()
            End If
        End If
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocalTrailer.TextChanged
        Me.SetEnabled()
    End Sub

    Private Sub txtManualTrailerLink_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtManualTrailerLink.TextChanged
        Me.SetEnabled()
    End Sub

    Private Sub txtYouTubeSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYouTubeSearch.TextChanged
        Me.SetEnabledSearch()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim bType As Boolean
        Dim Parameter As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class