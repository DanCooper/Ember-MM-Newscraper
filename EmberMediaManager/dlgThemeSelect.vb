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
Imports System.Net
Imports NLog

Public Class dlgThemeSelect

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadTheme As New System.ComponentModel.BackgroundWorker

    Private _UrlList As List(Of Themes)
    Private tURL As String = String.Empty
    Private sPath As String
    Private tTheme As New Themes
    Private _results As New MediaContainers.Theme

#End Region 'Fields

#Region "Properties"

    Public Property Results As MediaContainers.Theme
        Get
            Return _results
        End Get
        Set(value As MediaContainers.Theme)
            _results = value
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

    Private Sub dlgThemeSelect_FormClosing(sender As Object, e As System.EventArgs) Handles Me.FormClosing
        RemoveHandler Themes.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgThemeSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
        AddHandler Themes.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgThemeSelect_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Activate()
    End Sub

    Private Sub CreateTable(ByVal tURLList As List(Of Themes))
        'set ListView
        lvThemes.MultiSelect = False
        lvThemes.FullRowSelect = True
        lvThemes.HideSelection = False
        lvThemes.Columns.Add("#", -1, HorizontalAlignment.Right)
        lvThemes.Columns.Add("URL", 0, HorizontalAlignment.Left)
        lvThemes.Columns.Add("Title", -2, HorizontalAlignment.Left)
        lvThemes.Columns.Add(Master.eLang.GetString(979, "Description"), -2, HorizontalAlignment.Left)
        lvThemes.Columns.Add("Length", -2, HorizontalAlignment.Left)
        lvThemes.Columns.Add("Bitrate", -2, HorizontalAlignment.Left)
        lvThemes.Columns.Add("WebURL", 0, HorizontalAlignment.Left)

        'Me.txtYouTubeSearch.Text = DBMovie.Movie.Title & " Trailer"

        _UrlList = tURLList
        Dim ID As Integer = 1
        Dim str(7) As String
        For Each aUrl In _UrlList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aUrl.URL.ToString
            str(2) = aUrl.Title.ToString
            str(3) = aUrl.Description.ToString
            str(4) = aUrl.Duration.ToString
            str(5) = aUrl.Bitrate.ToString
            str(6) = aUrl.WebURL.ToString
            itm = New ListViewItem(str)
            lvThemes.Items.Add(itm)
            ID = ID + 1
        Next
        'Me.pnlStatus.Visible = False
        lvThemes.Enabled = True
        'Me.txtYouTube.Enabled = True
        'Me.txtManual.Enabled = True
        'Me.btnBrowse.Enabled = True
        'Me.SetEnabled(False)
        If _UrlList.Count = 1 Then
            lvThemes.Select()
            lvThemes.Items(0).Selected = True
        End If
    End Sub

    Public Overloads Function ShowDialog(ByRef DBElement As Database.DBElement, ByRef tURLList As List(Of Themes)) As DialogResult
        CreateTable(tURLList)

        Return ShowDialog()
    End Function

    Private Sub lvThemes_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvThemes.DoubleClick
        Dim tURL As String = lvThemes.SelectedItems(0).SubItems(1).Text.ToString
        Dim tWebURL As String = lvThemes.SelectedItems(0).SubItems(6).Text.ToString
        'Me.vlcPlayer.playlist.stop()

        If tURL.Contains("goear") Then
            'GoEar needs a existing connection to download files, otherwise you will be blocked
            Dim dummyclient As New WebClient
            dummyclient.OpenRead(tWebURL)
            dummyclient.Dispose()
        End If
        'Me.vlcPlayer.playlist.items.clear()
        'Me.vlcPlayer.playlist.add(tURL)
        'Me.vlcPlayer.playlist.play()
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Dim didCancel As Boolean = False

        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(1329, "Downloading selected theme...")
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pnlStatus.Visible = True
        Application.DoEvents()

        If lvThemes.SelectedItems.Count = 1 Then
            Dim selID As Integer = CInt(lvThemes.SelectedItems(0).SubItems(0).Text) - 1
            tTheme = _UrlList.Item(selID)

            bwDownloadTheme = New System.ComponentModel.BackgroundWorker
            bwDownloadTheme.WorkerReportsProgress = True
            bwDownloadTheme.WorkerSupportsCancellation = True
            bwDownloadTheme.RunWorkerAsync(New Arguments With {.Parameter = tTheme, .bType = True})
        Else
            DialogResult = System.Windows.Forms.DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        'Me.TrailerStop()
        'Me.vlcPlayer.playlist.stop()

        OK_Button.Enabled = isEnabled
        lvThemes.Enabled = isEnabled
    End Sub

    Private Sub bwDownloadTheme_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTheme.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Results.WebTheme.FromWeb(Args.Parameter.URL, Args.Parameter.WebURL)
            Results.URL = Args.Parameter.URL
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        e.Result = Args.bType

        If bwDownloadTheme.CancellationPending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub bwDownloadTheme_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDownloadTheme.ProgressChanged
        pbStatus.Value = e.ProgressPercentage
        Application.DoEvents()
    End Sub

    Private Sub bwDownloadTheme_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDownloadTheme.RunWorkerCompleted
        If Not e.Cancelled Then
            If Convert.ToBoolean(e.Result) Then
                DialogResult = System.Windows.Forms.DialogResult.OK
                Close()
            Else
                pnlStatus.Visible = False
                'Me.SetControlsEnabled(True)
                'Me.SetEnabled()
                'Me.btnTrailerPlay.Enabled = False
                'Me.btnTrailerStop.Enabled = False
            End If
        End If
    End Sub

    Private Sub DownloadProgressUpdated(ByVal iProgress As Integer)
        bwDownloadTheme.ReportProgress(iProgress)
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(1069, "Select Theme")
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If bwDownloadTheme.IsBusy Then bwDownloadTheme.CancelAsync()

        While bwDownloadTheme.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Results = Nothing
        Close()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim bType As Boolean
        Dim Parameter As Themes

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class