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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports EmberAPI
Imports NLog

Public Class dlgExportMovies

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwCreateTemplate As New System.ComponentModel.BackgroundWorker

    Private TemplateName As String
    Private bCancelled As Boolean
    Private TempPath As String = Path.Combine(Master.TempPath, "Export")
    Private MovieList As New List(Of Database.DBElement)
    Private TVShowList As New List(Of Database.DBElement)

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub CopyDirectory(ByVal SourcePath As String, ByVal DestPath As String, Optional ByVal Overwrite As Boolean = False)
        Dim SourceDir As DirectoryInfo = New DirectoryInfo(SourcePath)
        Dim DestDir As DirectoryInfo = New DirectoryInfo(DestPath)
        Dim IsRoot As Boolean = False

        ' the source directory must exist, otherwise throw an exception
        If SourceDir.Exists Then

            'is this a root directory?
            If DestDir.Root.FullName = DestDir.FullName Then
                IsRoot = True
            End If

            ' if destination SubDir's parent SubDir does not exist throw an exception (also check it isn't the root)
            If Not IsRoot AndAlso Not DestDir.Parent.Exists Then
                Throw New DirectoryNotFoundException _
                    ("Destination directory does not exist: " + DestDir.Parent.FullName)
            End If

            If Not DestDir.Exists Then
                DestDir.Create()
            End If

            ' copy all the files of the current directory
            Dim ChildFile As FileInfo
            For Each ChildFile In SourceDir.GetFiles()
                If (ChildFile.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden OrElse Path.GetExtension(ChildFile.FullName) = ".html" Then Continue For
                If Overwrite Then
                    ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), True)
                Else
                    ' if Overwrite = false, copy the file only if it does not exist
                    ' this is done to avoid an IOException if a file already exists
                    ' this way the other files can be copied anyway...
                    If Not File.Exists(Path.Combine(DestDir.FullName, ChildFile.Name)) Then
                        ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), False)
                    End If
                End If
            Next

            ' copy all the sub-directories by recursively calling this same routine
            Dim SubDir As DirectoryInfo
            For Each SubDir In SourceDir.GetDirectories()
                If (SubDir.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
                CopyDirectory(SubDir.FullName, Path.Combine(DestDir.FullName,
                    SubDir.Name), Overwrite)
            Next
        Else
            Throw New DirectoryNotFoundException("Source directory does not exist: " + SourceDir.FullName)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If bwLoadInfo.IsBusy Then
            bwLoadInfo.CancelAsync()
        End If
        btnCancel.Enabled = False
    End Sub

    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        MovieList.Clear()
        ' Load nfo movies using path from DB
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim _tmpMovie As New Database.DBElement
            Dim _ID As Integer
            Dim iProg As Integer = 0
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idMovie) AS mcount FROM movie;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
            End Using
            SQLNewcommand.CommandText = String.Concat("SELECT idMovie FROM movie ORDER BY ListTitle COLLATE NOCASE;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _ID = Convert.ToInt32(SQLreader("idMovie"))
                        _tmpMovie = Master.DB.LoadMovieFromDB(_ID)
                        MovieList.Add(_tmpMovie)
                        bwLoadInfo.ReportProgress(iProg, _tmpMovie.ListTitle) '  show File
                        iProg += 1
                        If bwLoadInfo.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                    e.Result = True
                Else
                    e.Cancel = True
                End If
            End Using
        End Using

        TVShowList.Clear()
        ' Load nfo tv shows using path from DB
        Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            Dim _tmpTVShow As New Database.DBElement
            Dim _ID As Integer
            Dim iProg As Integer = 0
            SQLNewcommand.CommandText = String.Concat("SELECT COUNT(idShow) AS mcount FROM tvshow;")
            Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                SQLcount.Read()
                bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
            End Using
            SQLNewcommand.CommandText = String.Concat("SELECT idShow FROM tvshow ORDER BY ListTitle COLLATE NOCASE;")
            Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                If SQLreader.HasRows Then
                    While SQLreader.Read()
                        _ID = Convert.ToInt32(SQLreader("idShow"))
                        _tmpTVShow = Master.DB.LoadTVShowFromDB(_ID, True, True)
                        TVShowList.Add(_tmpTVShow)
                        bwLoadInfo.ReportProgress(iProg, _tmpTVShow.ListTitle) '  show File
                        iProg += 1
                        If bwLoadInfo.CancellationPending Then
                            e.Cancel = True
                            Return
                        End If
                    End While
                    e.Result = True
                Else
                    e.Cancel = True
                End If
            End Using
        End Using
    End Sub

    Private Sub bwLoadInfo_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadInfo.ProgressChanged
        If e.ProgressPercentage >= 0 Then
            pbCompile.Value = e.ProgressPercentage
            lblFile.Text = e.UserState.ToString
        Else
            pbCompile.Maximum = Convert.ToInt32(e.UserState)
        End If
    End Sub

    Private Sub bwLoadInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadInfo.RunWorkerCompleted
        bCancelled = e.Cancelled
        If Not e.Cancelled Then
            Warning(True, "Create Template...")
            bwCreateTemplate.WorkerReportsProgress = True
            bwCreateTemplate.WorkerSupportsCancellation = True
            bwCreateTemplate.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwCreateTemplate_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCreateTemplate.DoWork
        Dim MExporter As New MediaExporter
        Dim TempIndexFile As String = MExporter.CreateTemplate(TemplateName, MovieList, TVShowList, String.Empty, AddressOf ShowProgressCreateTemplate)
        e.Result = TempIndexFile
    End Sub

    Private Sub bwCreateTemplate_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwCreateTemplate.ProgressChanged
        Dim Info = DirectCast(e.UserState, KeyValuePair(Of String, String))
        lblFile.Text = If(Not String.IsNullOrEmpty(Info.Value.ToString), String.Concat(Info.Key.ToString, ": ", Info.Value.ToString), Info.Key.ToString)
    End Sub

    Private Sub bwCreateTemplate_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCreateTemplate.RunWorkerCompleted
        If Not String.IsNullOrEmpty(DirectCast(e.Result, String)) Then
            LoadHTML(DirectCast(e.Result, String))
        Else
            wbPreview.DocumentText = String.Concat("<center><h1 style=""color:Red;"">", Master.eLang.GetString(284, "Canceled"), "</h1></center>")
        End If
        pnlCancel.Visible = False
    End Sub

    Private Function ShowProgressCreateTemplate(ByVal title As String, ByVal operation As String) As Boolean
        Dim Info As New KeyValuePair(Of String, String)(title, operation)
        bwCreateTemplate.ReportProgress(0, Info)
        If bwCreateTemplate.CancellationPending Then Return False
        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If bwLoadInfo.IsBusy Then
            bwLoadInfo.CancelAsync()
        End If
        While bwLoadInfo.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Sub dlgExportMovies_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bwLoadInfo.IsBusy Then
            DoCancel()
            While bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
        FileUtils.Delete.DeleteDirectory(TempPath)
    End Sub

    Private Sub dlgExportMovies_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
        Dim di As DirectoryInfo = New DirectoryInfo(String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, "html"))
        If di.Exists Then
            For Each i As DirectoryInfo In di.GetDirectories
                If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    cbTemplate.Items.Add(i.Name)
                End If
            Next
            If cbTemplate.Items.Count > 0 Then
                cbTemplate.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub dlgMoviesReport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Show Cancel Panel
        btnCancel.Visible = True
        lblCompiling.Visible = True
        pbCompile.Visible = True
        pbCompile.Style = ProgressBarStyle.Continuous
        lblCanceling.Visible = False
        pnlCancel.Visible = True
        Application.DoEvents()

        Activate()

        'Start worker
        bwLoadInfo = New System.ComponentModel.BackgroundWorker
        bwLoadInfo.WorkerSupportsCancellation = True
        bwLoadInfo.WorkerReportsProgress = True
        bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub DoCancel()
        bwLoadInfo.CancelAsync()
        bwCreateTemplate.CancelAsync()
        btnCancel.Visible = False
        lblCompiling.Visible = False
        pbCompile.Style = ProgressBarStyle.Marquee
        pbCompile.MarqueeAnimationSpeed = 25
        lblCanceling.Visible = True
        lblFile.Visible = False
    End Sub

    Private Sub LoadHTML(ByVal TempIndexFile As String)
        Warning(True, Master.eLang.GetString(326, "Loading. Please wait..."))

        Try
            wbPreview.Navigate(TempIndexFile)
            btnSave.Enabled = True
        Catch ex As Exception
            btnSave.Enabled = False
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Cursor = Cursors.WaitCursor
        If Directory.Exists(clsAdvancedSettings.GetSetting("ExportPath", String.Empty)) Then
            CopyDirectory(TempPath, clsAdvancedSettings.GetSetting("ExportPath", String.Empty), True)
            btnSave.Enabled = False
            MessageBox.Show((Master.eLang.GetString(1003, "Template saved to:") & clsAdvancedSettings.GetSetting("ExportPath", String.Empty)), Master.eLang.GetString(361, "Finished!"), MessageBoxButtons.OK)
        Else
            btnSave.Enabled = False
            MessageBox.Show((Master.eLang.GetString(221, "Export Path is not valid:") & clsAdvancedSettings.GetSetting("ExportPath", String.Empty)), Master.eLang.GetString(816, "An Error Has Occurred"), MessageBoxButtons.OK)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(328, "Export Movies")
        btnSave.Text = Master.eLang.GetString(273, "Save")
        btnClose.Text = Master.eLang.GetString(19, "Close")

        lblCompiling.Text = Master.eLang.GetString(177, "Compiling Movie List...")
        lblCanceling.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        lblTemplate.Text = Master.eLang.GetString(334, "Template")
        btnBuild.Text = Master.eLang.GetString(1004, "Generate HTML...")
        btnSave.Enabled = False
    End Sub

    Private Sub Warning(ByVal show As Boolean, Optional ByVal txt As String = "")
        Try
            btnCancel.Visible = True
            btnCancel.Enabled = True
            lblCompiling.Visible = True
            pbCompile.Visible = True
            pbCompile.Style = ProgressBarStyle.Marquee
            pbCompile.MarqueeAnimationSpeed = 25
            lblCanceling.Visible = False
            pnlCancel.Visible = show
            lblFile.Visible = True
            lblCompiling.Text = txt
            Application.DoEvents()
            pnlCancel.BringToFront()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wbPreview_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbPreview.DocumentCompleted
        If Not bCancelled Then
            wbPreview.Visible = True
            If Not cbTemplate.Text = String.Empty Then btnSave.Enabled = True
        End If
        Warning(False)
    End Sub

    Private Sub btnBuild_Click(sender As Object, e As EventArgs) Handles btnBuild.Click
        btnBuild.Enabled = False
        bwLoadInfo.WorkerReportsProgress = True
        bwLoadInfo.WorkerSupportsCancellation = True
        bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub cbTemplate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTemplate.SelectedIndexChanged
        TemplateName = cbTemplate.SelectedItem.ToString
        btnBuild.Enabled = True
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class