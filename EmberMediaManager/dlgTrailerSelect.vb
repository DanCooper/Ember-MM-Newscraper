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

Public Class dlgTrailerSelect

#Region "Fields"


    'Friend WithEvents bwCompileList As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwDownloadTrailer As New System.ComponentModel.BackgroundWorker

    Private tMovie As New Structures.DBMovie
    Private _UrlList As List(Of String)
    Private tArray As New List(Of String)
    Private tURL As String = String.Empty
    Private sPath As String

#End Region 'Fields

#Region "Methods"

    Private Sub dlgTrailerSelect_Leave(sender As Object, e As System.EventArgs) Handles Me.Leave
        RemoveHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
        'cTrailer.IMDBURL = IMDBURL
        AddHandler Trailers.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgTrailer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByRef tURLList As List(Of String)) As String
        Me.tMovie = DBMovie
        Me.sPath = DBMovie.Filename
        Me._UrlList = tURLList
        For Each aUrl In _UrlList
            Me.lbTrailers.Items.Add(aUrl)
        Next
        Me.pnlStatus.Visible = False
        Me.lbTrailers.Enabled = True
        Me.txtYouTube.Enabled = True
        Me.txtManual.Enabled = True
        Me.btnBrowse.Enabled = True
        Me.SetEnabled(False)
        If _UrlList.Count = 1 Then
            Me.lbTrailers.SetSelected(0, True)
        End If
        If MyBase.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False) Then
                Return Replace(Me.tURL, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
            Else
                Return Me.tURL
            End If
        Else
            Return String.Empty
        End If
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub BeginDownload(ByVal CloseDialog As Boolean)
        Dim didCancel As Boolean = False

        Me.OK_Button.Enabled = False
        Me.btnSetNfo.Enabled = False
        Me.btnPlayTrailer.Enabled = False
        Me.btnPlayBrowser.Enabled = False
        Me.lbTrailers.Enabled = False
        Me.txtYouTube.Enabled = False
        Me.txtManual.Enabled = False
        Me.btnBrowse.Enabled = False
        Me.lblStatus.Text = Master.eLang.GetString(906, "Downloading selected trailer...")
        Me.pbStatus.Style = ProgressBarStyle.Continuous
        Me.pbStatus.Value = 0
        Me.pnlStatus.Visible = True
        Application.DoEvents()

        If Me.txtManual.Text.Length > 0 Then
            Me.lblStatus.Text = Master.eLang.GetString(907, "Copying specified file to trailer...")
            If Master.eSettings.ValidExts.Contains(Path.GetExtension(Me.txtManual.Text)) AndAlso File.Exists(Me.txtManual.Text) Then
                If CloseDialog Then
                    If Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(Me.sPath) Then
                        Me.tURL = String.Concat(Directory.GetParent(Directory.GetParent(Me.sPath).FullName).FullName, "\", "index", If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text))
                    ElseIf Master.eSettings.MovieNameNFOStack Then
                        Dim sPathStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(Me.sPath))
                        Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(sPathStack), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text)))
                    Else
                        Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(Me.sPath), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text)))
                    End If

                    FileUtils.Common.MoveFileWithStream(Me.txtManual.Text, Me.tURL)

                    Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Me.Close()
                Else
                    System.Diagnostics.Process.Start(String.Concat("""", Me.txtManual.Text, """"))
                    didCancel = True
                End If
            Else
                MsgBox(Master.eLang.GetString(192, "File is not valid.", True), MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Master.eLang.GetString(194, "Not Valid", True))
                didCancel = True
            End If
        ElseIf Regex.IsMatch(Me.txtYouTube.Text, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
            Using dFormats As New dlgTrailerFormat
                Dim sFormat As String = dFormats.ShowDialog(Me.txtYouTube.Text)

                If Not String.IsNullOrEmpty(sFormat) Then
                    Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                    Me.bwDownloadTrailer.WorkerReportsProgress = True
                    Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                    Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                Else
                    didCancel = True
                End If
            End Using
        ElseIf StringUtils.isValidURL(Me.txtYouTube.Text) Then
            Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
            Me.bwDownloadTrailer.WorkerReportsProgress = True
            Me.bwDownloadTrailer.WorkerSupportsCancellation = True
            Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.parameter = Me.txtYouTube.Text, .bType = CloseDialog})
        Else
            If Regex.IsMatch(Me.lbTrailers.SelectedItem.ToString, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                Using dFormats As New dlgTrailerFormat
                    Dim sFormat As String = dFormats.ShowDialog(Me.lbTrailers.SelectedItem.ToString)

                    If Not String.IsNullOrEmpty(sFormat) Then
                        Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                        Me.bwDownloadTrailer.WorkerReportsProgress = True
                        Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                        Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.Parameter = sFormat, .bType = CloseDialog})
                    Else
                        didCancel = True
                    End If
                End Using
            Else
                Me.bwDownloadTrailer = New System.ComponentModel.BackgroundWorker
                Me.bwDownloadTrailer.WorkerReportsProgress = True
                Me.bwDownloadTrailer.WorkerSupportsCancellation = True
                Me.bwDownloadTrailer.RunWorkerAsync(New Arguments With {.parameter = lbTrailers.SelectedItem.ToString, .bType = CloseDialog})
            End If
        End If

        If didCancel Then
            Me.pnlStatus.Visible = False
            Me.lbTrailers.Enabled = True
            Me.txtYouTube.Enabled = True
            Me.txtManual.Enabled = True
            Me.btnBrowse.Enabled = True
            Me.SetEnabled(False)
        End If
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With ofdTrailer
                .InitialDirectory = Directory.GetParent(Master.currMovie.Filename).FullName
                .Filter = String.Concat("Supported Trailer Formats|*", Functions.ListToStringWithSeparator(Master.eSettings.ValidExts.ToArray(), ";*"))
                .FilterIndex = 0
            End With

            If ofdTrailer.ShowDialog() = DialogResult.OK Then
                txtManual.Text = ofdTrailer.FileName
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnGetTrailers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.OK_Button.Enabled = False
        Me.btnSetNfo.Enabled = False
        Me.btnPlayTrailer.Enabled = False
        Me.btnPlayBrowser.Enabled = False
        Me.lbTrailers.Enabled = False
        Me.txtYouTube.Enabled = False
        Me.txtManual.Enabled = False
        Me.btnBrowse.Enabled = False
        Me.pnlStatus.Visible = True
    End Sub

    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTrailer.Click
        Try
            Me.BeginDownload(False)
        Catch
            MsgBox(Master.eLang.GetString(908, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), MsgBoxStyle.Critical, Master.eLang.GetString(59, "Error Playing Trailer"))
            Me.pnlStatus.Visible = False
            Me.lbTrailers.Enabled = True
            Me.txtYouTube.Enabled = True
            Me.txtManual.Enabled = True
            Me.btnBrowse.Enabled = True
            Me.SetEnabled(False)
        End Try
    End Sub

    Private Sub btnPlayBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayBrowser.Click
        If Not String.IsNullOrEmpty(Me.txtYouTube.Text) Then
            If Master.isWindows Then
                Process.Start(Me.txtYouTube.Text)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = Me.txtYouTube.Text
                    Explorer.Start()
                End Using
            End If
        Else
            If Master.isWindows Then
                Process.Start(Me.lbTrailers.SelectedItem.ToString)
            Else
                Using Explorer As New Process
                    Explorer.StartInfo.FileName = "xdg-open"
                    Explorer.StartInfo.Arguments = Me.lbTrailers.SelectedItem.ToString
                    Explorer.Start()
                End Using
            End If
        End If
    End Sub

    Private Sub btnSetNfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetNfo.Click

        If Me.btnSetNfo.Text = Master.eLang.GetString(910, "Move") Then
            If Master.eSettings.ValidExts.Contains(Path.GetExtension(Me.txtManual.Text)) AndAlso File.Exists(Me.txtManual.Text) Then
                Me.OK_Button.Enabled = False
                Me.btnSetNfo.Enabled = False
                Me.btnPlayTrailer.Enabled = False
                Me.btnPlayBrowser.Enabled = False
                Me.lbTrailers.Enabled = False
                Me.txtYouTube.Enabled = False
                Me.txtManual.Enabled = False
                Me.btnBrowse.Enabled = False
                Me.lblStatus.Text = Master.eLang.GetString(912, "Moving specified file to trailer...")
                Me.pbStatus.Style = ProgressBarStyle.Continuous
                Me.pbStatus.Value = 0
                Me.pnlStatus.Visible = True
                Application.DoEvents()

                If Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(Me.sPath) Then
                    Me.tURL = String.Concat(Directory.GetParent(Directory.GetParent(Me.sPath).FullName).FullName, "\", "index", If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text))
                ElseIf Master.eSettings.MovieNameNFOStack Then
                    Dim sPathStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(Me.sPath))
                    Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(sPathStack), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text)))
                Else
                    Me.tURL = Path.Combine(Directory.GetParent(Me.sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(Me.sPath), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(Me.txtManual.Text)))
                End If
                File.Move(Me.txtManual.Text, Me.tURL)

                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MsgBox(Master.eLang.GetString(192, "File is not valid.", True), MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, Master.eLang.GetString(194, "Not Valid", True))
                Me.pnlStatus.Visible = False
                Me.lbTrailers.Enabled = True
                Me.txtYouTube.Enabled = True
                Me.txtManual.Enabled = True
                Me.btnBrowse.Enabled = True
                Me.SetEnabled(False)
            End If
        Else
            Dim didCancel As Boolean = False

            If StringUtils.isValidURL(Me.txtYouTube.Text) Then
                tURL = Me.txtYouTube.Text
            ElseIf Me.lbTrailers.SelectedItems.Count > 0 Then
                tURL = lbTrailers.SelectedItem.ToString
            Else
                didCancel = True
            End If

            If Not didCancel Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If

    End Sub

    Private Sub bwDownloadTrailer_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTrailer.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try

            Me.tURL = Trailers.DownloadTrailer(Me.sPath, Args.Parameter) ', Me.tMovie.Filename)
            
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
                Me.lbTrailers.Enabled = True
                Me.txtYouTube.Enabled = True
                Me.txtManual.Enabled = True
                Me.btnBrowse.Enabled = True
                Me.SetEnabled(False)
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        'Trailer.Cancel()

        If Me.bwDownloadTrailer.IsBusy Then Me.bwDownloadTrailer.CancelAsync()

        While Me.bwDownloadTrailer.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer)
        bwDownloadTrailer.ReportProgress(iProgress)
    End Sub

    Private Sub lbTrailers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbTrailers.SelectedIndexChanged
        Me.SetEnabled(True)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.BeginDownload(True)
    End Sub

    Private Sub SetEnabled(ByVal DeletePre As Boolean)
        If StringUtils.isValidURL(Me.txtYouTube.Text) OrElse Me.lbTrailers.SelectedItems.Count > 0 OrElse Me.txtManual.Text.Length > 0 Then
            Me.OK_Button.Enabled = True
            Me.btnSetNfo.Enabled = True
            Me.btnPlayTrailer.Enabled = True
            If Me.txtManual.Text.Length > 0 Then
                Me.btnPlayBrowser.Enabled = False
            Else
                Me.btnPlayBrowser.Enabled = True
            End If
            If Me.txtManual.Text.Length > 0 Then
                Me.OK_Button.Text = Master.eLang.GetString(911, "Copy")
                Me.btnSetNfo.Text = Master.eLang.GetString(910, "Move")
            Else
                Me.OK_Button.Text = Master.eLang.GetString(373, "Download", True)
                Me.btnSetNfo.Text = Master.eLang.GetString(913, "Set To Nfo")
            End If
        Else
            Me.OK_Button.Enabled = False
            Me.OK_Button.Text = Master.eLang.GetString(373, "Download", True)
            Me.btnPlayTrailer.Enabled = False
            Me.btnPlayBrowser.Enabled = False
            Me.btnSetNfo.Enabled = False
            Me.btnSetNfo.Text = Master.eLang.GetString(923, "Set To Nfo")
        End If
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(914, "Select Trailer")
        Me.OK_Button.Text = Master.eLang.GetString(373, "Download", True)
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel", True)
        Me.GroupBox1.Text = Master.eLang.GetString(915, "Select Trailer to Download")
        Me.GroupBox2.Text = Master.eLang.GetString(916, "Manual Trailer Entry")
        Me.Label1.Text = Master.eLang.GetString(917, "Direct Link or YouTube URL:")
        Me.lblStatus.Text = Master.eLang.GetString(918, "Compiling trailer list...")
        Me.btnPlayTrailer.Text = Master.eLang.GetString(919, "Preview Trailer")
        Me.btnPlayBrowser.Text = Master.eLang.GetString(931, "Open In Browser")
        Me.btnSetNfo.Text = Master.eLang.GetString(913, "Set To Nfo")
        Me.Label2.Text = Master.eLang.GetString(920, "Local Trailer:")
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtManual.TextChanged
        Me.SetEnabled(True)
    End Sub

    Private Sub txtYouTube_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYouTube.TextChanged
        Me.SetEnabled(True)
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