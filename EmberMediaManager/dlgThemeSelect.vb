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

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwDownloadTheme As New ComponentModel.BackgroundWorker

    Private _TmpDBElement As Database.DBElement
    Private _ThemeList As New List(Of MediaContainers.Theme)
    Private _WithPlayer As Boolean

    Private _UrlList As List(Of Themes)
    Private _URL As String = String.Empty
    Private _Theme As New Themes

#End Region 'Fields

#Region "Properties"

    Public Property Result As MediaContainers.Theme

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub dlgThemeSelect_FormClosing(sender As Object, e As EventArgs) Handles Me.FormClosing
        RemoveHandler Themes.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgThemeSelect_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Setup()
        AddHandler Themes.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Private Sub dlgThemeSelect_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog(ByRef tDBElement As Database.DBElement, ByRef tURLList As List(Of MediaContainers.Theme), Optional ByVal WithPlayer As Boolean = False) As DialogResult
        _WithPlayer = WithPlayer

        'set ListView
        lvThemes.MultiSelect = False
        lvThemes.FullRowSelect = True
        lvThemes.HideSelection = False

        _TmpDBElement = tDBElement

        AddThemesToList(tURLList)

        pnlStatus.Visible = False
        SetControlsEnabled(True)
        'SetEnabled()
        If lvThemes.Items.Count = 1 Then
            lvThemes.Select()
            lvThemes.Items(0).Selected = True
        End If

        Return ShowDialog()
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub AddThemesToList(ByVal tList As List(Of MediaContainers.Theme))
        Dim ID As Integer = lvThemes.Items.Count + 1
        Dim nList As List(Of MediaContainers.Theme) = tList

        Dim str(9) As String
        For Each aUrl In nList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aUrl.URLAudioStream.ToString
            str(2) = aUrl.URLWebsite.ToString
            str(3) = aUrl.Description.ToString
            str(4) = aUrl.Duration.ToString
            str(5) = aUrl.Bitrate.ToString
            str(6) = aUrl.ThemeOriginal.Extention.ToString
            str(7) = aUrl.Source.ToString
            str(8) = aUrl.Scraper.ToString
            itm = New ListViewItem(str)
            lvThemes.Items.Add(itm)
            ID = ID + 1
        Next
    End Sub

    Private Sub lvThemes_DoubleClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles lvThemes.DoubleClick
        Process.Start(lvThemes.SelectedItems(0).SubItems(2).Text.ToString)
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim didCancel As Boolean = False

        SetControlsEnabled(False)
        lblStatus.Text = Master.eLang.GetString(1329, "Downloading selected theme...")
        pbStatus.Style = ProgressBarStyle.Continuous
        pbStatus.Value = 0
        pnlStatus.Visible = True
        Application.DoEvents()

        If lvThemes.SelectedItems.Count = 1 Then
            Dim SelectedTheme As New MediaContainers.Theme With {
                .URLAudioStream = lvThemes.SelectedItems(0).SubItems(1).Text.ToString,
                .URLWebsite = lvThemes.SelectedItems(0).SubItems(2).Text.ToString}
            bwDownloadTheme = New ComponentModel.BackgroundWorker With {
                .WorkerReportsProgress = True,
                .WorkerSupportsCancellation = True
            }
            bwDownloadTheme.RunWorkerAsync(New Arguments With {.Parameter = SelectedTheme, .bType = True})
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        'Me.TrailerStop()
        'Me.vlcPlayer.playlist.stop()

        btnOK.Enabled = isEnabled
        lvThemes.Enabled = isEnabled
    End Sub

    Private Sub bwDownloadTheme_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDownloadTheme.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Try
            Result.ThemeOriginal.LoadFromWeb(Args.Parameter.URLAudioStream, Args.Parameter.URLWebsite)
            Result.URLAudioStream = Args.Parameter.URLAudioStream
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
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
                DialogResult = DialogResult.OK
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

    Private Sub Setup()
        Text = Master.eLang.GetString(1069, "Select Theme")
        btnOK.Text = Master.eLang.OK
        btnSkip.Text = Master.eLang.Skip
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSkip.Click
        If bwDownloadTheme.IsBusy Then bwDownloadTheme.CancelAsync()

        While bwDownloadTheme.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        DialogResult = DialogResult.Cancel
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim bType As Boolean
        Dim Parameter As MediaContainers.Theme

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class