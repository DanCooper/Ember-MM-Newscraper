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
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub dlgTrailerSelect_FormClosing(sender As Object, e As EventArgs) Handles Me.FormClosing
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()
        AddHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Database.DBElement, ByRef tURLList As List(Of MediaContainers.Trailer), ByVal OnlyToNFO As Boolean, Optional ByVal _isNew As Boolean = False) As DialogResult
        _NoDownload = OnlyToNFO

        'set ListView
        lvTrailers.MultiSelect = False
        lvTrailers.FullRowSelect = True
        lvTrailers.HideSelection = False

        txtYouTubeSearch.Text = String.Concat(DBMovie.MainDetails.Title, " ", Master.eSettings.Movie.TrailerSettings.DefaultSearchParameter)

        _TmpDBElement = DBMovie

        AddTrailersToList(tURLList)

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
        lblStatus.Visible = True
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pbStatus.Visible = True
        Application.DoEvents()

        If txtLocalTrailer.Text.Length > 0 Then
            lblStatus.Text = Master.eLang.GetString(907, "Copying specified file to trailer...")
            If Master.eSettings.Options.FileSystem.ValidVideoExtensions.Contains(Path.GetExtension(txtLocalTrailer.Text)) AndAlso File.Exists(txtLocalTrailer.Text) Then
                If CloseDialog Then
                    If _NoDownload Then
                        Result.URLWebsite = txtLocalTrailer.Text
                    Else
                        Result.TrailerOriginal.LoadFromFile(txtLocalTrailer.Text)
                    End If

                    DialogResult = DialogResult.OK
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
            If _NoDownload Then
                Result.URLWebsite = sFormat.VideoURL
                DialogResult = DialogResult.OK
            Else
                If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                    bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                        .WorkerReportsProgress = True,
                        .WorkerSupportsCancellation = True
                    }
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
            If _NoDownload Then
                Result.URLWebsite = txtManualTrailerLink.Text
                DialogResult = DialogResult.OK
            Else
                Dim ManualTrailer As New TrailerLinksContainer With {.VideoURL = txtManualTrailerLink.Text}
                bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                    .WorkerReportsProgress = True,
                    .WorkerSupportsCancellation = True
                }
                bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = ManualTrailer, .bType = CloseDialog})
            End If
        Else
            If YouTube.UrlUtils.IsYouTubeURL(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then
                If _NoDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    DialogResult = DialogResult.OK
                Else
                    Dim sFormat As New TrailerLinksContainer
                    Using dFormats As New dlgTrailerFormat
                        sFormat = dFormats.ShowDialog(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
                    End Using
                    If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                        bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                            .WorkerReportsProgress = True,
                            .WorkerSupportsCancellation = True
                        }
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
                If _NoDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(2).Text.ToString
                    DialogResult = DialogResult.OK
                Else
                    If sFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(sFormat.VideoURL) Then
                        bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                            .WorkerReportsProgress = True,
                            .WorkerSupportsCancellation = True
                        }
                        bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End If
            Else
                If _NoDownload Then
                    Result.URLWebsite = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString
                    DialogResult = DialogResult.OK
                Else
                    Dim SelectedTrailer As New TrailerLinksContainer With {.VideoURL = lvTrailers.SelectedItems(0).SubItems(1).Text.ToString}
                    bwDownloadTrailer = New ComponentModel.BackgroundWorker With {
                        .WorkerReportsProgress = True,
                        .WorkerSupportsCancellation = True
                    }
                    bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = SelectedTrailer, .bType = CloseDialog})
                End If
            End If
        End If

        If didCancel Then
            lblStatus.Visible = False
            pbStatus.Visible = False
            SetControlsEnabled(True)
            SetEnabled()
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBrowseLocalTrailer.Click
        Try
            With ofdTrailer
                .InitialDirectory = _TmpDBElement.FileItem.MainPath.FullName
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

    Private Sub btnClearLink_Click(sender As Object, e As EventArgs) Handles btnClearManualTrailerLink.Click
        txtManualTrailerLink.Text = String.Empty
    End Sub

    Private Sub btnPlayBrowser_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPlayInBrowser.Click
        If Not String.IsNullOrEmpty(txtManualTrailerLink.Text) Then
            Process.Start(txtManualTrailerLink.Text)
        Else
            If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then
                Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
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
        bwCompileList.RunWorkerAsync(New Arguments With {.bType = False})
    End Sub

    Private Sub bwCompileList_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCompileList.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Functions.SetScrapeModifiers(_TmpDBElement.ScrapeModifiers, Enums.ModifierType.MainTrailer, True)
            Dim nResult = Scraper.Run(_TmpDBElement)
            If nResult IsNot Nothing Then
                _TrailerList = nResult.lstTrailers
                Args.bType = True
            Else
                Args.bType = False
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        e.Result = Args.bType

        If bwCompileList.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwCompileList_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCompileList.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                If _TrailerList.Count > 0 Then
                    lvTrailers.Items.Clear()
                    AddTrailersToList(_TrailerList)
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
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Result.TrailerOriginal.LoadFromWeb(Args.Parameter)
            Result.URLWebsite = Args.Parameter.VideoURL
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
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
            Else
                lblStatus.Visible = False
                pbStatus.Visible = False
                SetControlsEnabled(True)
                SetEnabled()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSkip.Click
        If bwDownloadTrailer.IsBusy Then bwDownloadTrailer.CancelAsync()

        While bwDownloadTrailer.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Result = Nothing
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer, ByVal strInfo As String)
        bwDownloadTrailer.ReportProgress(iProgress, strInfo)
    End Sub

    Private Sub lvTrailers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lvTrailers.SelectedIndexChanged
        SetEnabled()
    End Sub

    Private Sub lvTrailers_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles lvTrailers.DoubleClick
        If Not String.IsNullOrEmpty(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString) Then Process.Start(lvTrailers.SelectedItems(0).SubItems(2).Text.ToString)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOK.Click
        BeginDownload(True)
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

    Private Sub SetUp()
        Text = Master.eLang.GetString(915, "Select Trailer to Download")
        btnOK.Text = Master.eLang.GetString(373, "Download")
        colDescription.Text = Master.eLang.GetString(979, "Description")
        colDuration.Text = Master.eLang.GetString(609, "Duration")
        colVideoQuality.Text = Master.eLang.GetString(1138, "Quality")
        colSource.Text = Master.eLang.GetString(1173, "Source")
        btnPlayInBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        btnTrailerScrape.Text = Master.eLang.GetString(1171, "Scrape Trailers")
        btnYouTubeSearch.Text = Master.eLang.GetString(977, "Search")
        btnSkip.Text = Master.eLang.Skip
        gbManualTrailer.Text = Master.eLang.GetString(916, "Manual Trailer Entry")
        gbYouTubeSearch.Text = Master.eLang.GetString(978, "Search On YouTube")
        lblLocalTrailer.Text = String.Concat(Master.eLang.GetString(920, "Local Trailer"), ":")
        lblManualTrailerLink.Text = String.Concat(Master.eLang.GetString(917, "Direct Link, YouTube, IMDB or Apple Trailer URL"), ":")
        lblStatus.Text = Master.eLang.GetString(918, "Compiling Trailer List")
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtLocalTrailer.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtManualTrailerLink_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtManualTrailerLink.TextChanged
        SetEnabled()
    End Sub

    Private Sub txtYouTubeSearch_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtYouTubeSearch.TextChanged
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