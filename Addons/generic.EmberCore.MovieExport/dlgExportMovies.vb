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
Imports System.Text.RegularExpressions

Public Class dlgExportMovies

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCreateTemplate As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker

    Private bCancelled As Boolean
    Private bExportMissingEpisodes As Boolean
    Private bHasChangedList_Movies As Boolean = True
    Private bHasChangedList_TVShows As Boolean = True
    Private dicListViews_Movies As New Dictionary(Of String, String)
    Private dicListViews_TVShows As New Dictionary(Of String, String)
    Private lstMovieList As New List(Of Database.DBElement)
    Private lstTVShowList As New List(Of Database.DBElement)
    Private strCurrList_Movies As String = "movielist"
    Private strCurrList_TVShows As String = "tvshowlist"
    Private strTempPath As String = Path.Combine(Master.TempPath, "Export")
    Private strTemplatePath As String

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        If Not Directory.Exists(Path.Combine(Master.SettingsPath, "Templates")) Then
            Directory.CreateDirectory(Path.Combine(Master.SettingsPath, "Templates"))
        End If
    End Sub

    Private Sub btnBuild_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuild.Click
        ShowStatus(True)
        bwLoadInfo.WorkerReportsProgress = True
        bwLoadInfo.WorkerSupportsCancellation = True
        bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        DoCancel()
    End Sub

    Private Sub btnExportPath_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportPath.Click
        Using fbdDialog As New FolderBrowserDialog()
            fbdDialog.Description = "Select ExportPath"
            If fbdDialog.ShowDialog() = DialogResult.OK Then
                txtExportPath.Text = fbdDialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Cursor = Cursors.WaitCursor
        If Not String.IsNullOrEmpty(txtExportPath.Text) Then
            CopyDirectory(strTempPath, txtExportPath.Text, True)
            btnSave.Enabled = False
            MessageBox.Show(String.Concat(Master.eLang.GetString(1003, "Template saved to:"), " ", txtExportPath.Text), Master.eLang.GetString(361, "Finished!"), MessageBoxButtons.OK)
        Else
            btnSave.Enabled = False
            MessageBox.Show(Master.eLang.GetString(221, "Export Path is not valid"), Master.eLang.GetString(816, "An Error Has Occurred"), MessageBoxButtons.OK)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub bwLoadInfo_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        If bHasChangedList_Movies Then
            bHasChangedList_Movies = False
            lstMovieList.Clear()
            bwLoadInfo.ReportProgress(-2, Master.eLang.GetString(177, "Compiling Movie List..."))

            ' Load nfo movies using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim iProg As Integer = 0
                SQLNewcommand.CommandText = String.Format("SELECT COUNT(idMovie) AS mcount FROM '{0}';", strCurrList_Movies)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                End Using
                SQLNewcommand.CommandText = String.Format("SELECT idMovie FROM '{0}' ORDER BY SortedTitle COLLATE NOCASE;", strCurrList_Movies)
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            Dim _tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt32(SQLreader("idMovie")))
                            lstMovieList.Add(_tmpMovie)
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
        End If

        If bHasChangedList_TVShows Then
            bHasChangedList_TVShows = False
            lstTVShowList.Clear()
            bwLoadInfo.ReportProgress(-2, Master.eLang.GetString(277, "Compiling TV Show List..."))

            ' Load nfo tv shows using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim iProg As Integer = 0
                SQLNewcommand.CommandText = String.Format("SELECT COUNT(idShow) AS mcount FROM '{0}';", strCurrList_TVShows)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                End Using
                SQLNewcommand.CommandText = String.Format("SELECT idShow FROM '{0}' ORDER BY SortedTitle COLLATE NOCASE;", strCurrList_TVShows)
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            Dim _tmpTVShow As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt32(SQLreader("idShow")), True, True, bExportMissingEpisodes)
                            lstTVShowList.Add(_tmpTVShow)
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
        End If
    End Sub

    Private Sub bwLoadInfo_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadInfo.ProgressChanged
        If e.ProgressPercentage >= 0 Then
            tspbStatus.Value = e.ProgressPercentage
            tslblFile.Text = e.UserState.ToString
        ElseIf e.ProgressPercentage = -1 Then
            tspbStatus.Maximum = Convert.ToInt32(e.UserState)
        ElseIf e.ProgressPercentage = -2 Then
            tslblStatus.Text = e.UserState.ToString
        End If
    End Sub

    Private Sub bwLoadInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadInfo.RunWorkerCompleted
        bCancelled = e.Cancelled
        If Not e.Cancelled Then
            tslblStatus.Text = "Create Template..."
            bwCreateTemplate.WorkerReportsProgress = True
            bwCreateTemplate.WorkerSupportsCancellation = True
            bwCreateTemplate.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwCreateTemplate_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCreateTemplate.DoWork
        Dim MExporter As New MediaExporter
        Dim strTempIndexFile As String = MExporter.CreateTemplate(strTemplatePath, lstMovieList, lstTVShowList, String.Empty, AddressOf ShowProgressCreateTemplate)
        e.Result = strTempIndexFile
    End Sub

    Private Sub bwCreateTemplate_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwCreateTemplate.ProgressChanged
        Dim Info = DirectCast(e.UserState, KeyValuePair(Of String, String))
        tslblFile.Text = If(Not String.IsNullOrEmpty(Info.Value.ToString), String.Concat(Info.Key.ToString, ": ", Info.Value.ToString), Info.Key.ToString)
    End Sub

    Private Sub bwCreateTemplate_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCreateTemplate.RunWorkerCompleted
        If Not String.IsNullOrEmpty(DirectCast(e.Result, String)) Then
            LoadHTML(DirectCast(e.Result, String))
        Else
            wbPreview.DocumentText = String.Concat("<center><h1 style=""color:Red;"">", Master.eLang.GetString(284, "Canceled"), "</h1></center>")
        End If
    End Sub

    Private Sub cbList_Movies_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbList_Movies.SelectedIndexChanged
        If Not strCurrList_Movies = CType(cbList_Movies.SelectedItem, KeyValuePair(Of String, String)).Value Then
            strCurrList_Movies = CType(cbList_Movies.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovies = strCurrList_Movies
            bHasChangedList_Movies = True
            btnBuild.Enabled = True
        End If
    End Sub

    Private Sub cbList_TVShows_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbList_TVShows.SelectedIndexChanged
        If Not strCurrList_TVShows = CType(cbList_TVShows.SelectedItem, KeyValuePair(Of String, String)).Value Then
            strCurrList_TVShows = CType(cbList_TVShows.SelectedItem, KeyValuePair(Of String, String)).Value
            ModulesManager.Instance.RuntimeObjects.ListMovies = strCurrList_Movies
            bHasChangedList_TVShows = True
            btnBuild.Enabled = True
        End If
    End Sub

    Private Sub cbTemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTemplate.SelectedIndexChanged
        strTemplatePath = CType(cbTemplate.SelectedItem, KeyValuePair(Of String, String)).Value
        btnBuild.Enabled = True
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
                If (ChildFile.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then Continue For
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

    Private Sub dlgExportMovies_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If bwLoadInfo.IsBusy Then
            DoCancel()
            While bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
        If bwCreateTemplate.IsBusy Then
            DoCancel()
            While bwCreateTemplate.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
        FileUtils.Delete.DeleteDirectory(strTempPath)
    End Sub

    Private Sub dlgExportMovies_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()

        bExportMissingEpisodes = AdvancedSettings.GetBooleanSetting("ExportMissingEpisodes", False)
        txtExportPath.Text = AdvancedSettings.GetSetting("ExportPath", String.Empty)

        Dim items As New Dictionary(Of String, String)

        'loading Ember custom templates
        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Templates"))
        If diCustom.Exists Then
            For Each i As DirectoryInfo In diCustom.GetDirectories
                If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    items.Add(i.Name, i.FullName)
                End If
            Next
        End If

        'loading Ember default templates
        Dim diDefault As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Modules", "Templates"))
        If diDefault.Exists Then
            For Each i As DirectoryInfo In diDefault.GetDirectories
                If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                    items.Add(i.Name, i.FullName)
                End If
            Next
        End If

        'load all Templates to list
        cbTemplate.DataSource = items.ToList
        cbTemplate.DisplayMember = "Key"
        cbTemplate.ValueMember = "Value"
        If cbTemplate.Items.Count > 0 Then
            cbTemplate.SelectedIndex = 0
        End If

        dicListViews_Movies.Clear()
        dicListViews_Movies.Add(Master.eLang.GetString(786, "Default List"), "movielist")
        For Each cList As String In Master.DB.GetViewList(Enums.ContentType.Movie)
            dicListViews_Movies.Add(Regex.Replace(cList, "movie-", String.Empty).Trim, cList)
        Next
        cbList_Movies.DataSource = dicListViews_Movies.ToList
        cbList_Movies.DisplayMember = "Key"
        cbList_Movies.ValueMember = "Value"
        cbList_Movies.SelectedIndex = 0

        dicListViews_TVShows.Clear()
        dicListViews_TVShows.Add(Master.eLang.GetString(786, "Default List"), "tvshowlist")
        For Each cList As String In Master.DB.GetViewList(Enums.ContentType.TVShow)
            dicListViews_TVShows.Add(Regex.Replace(cList, "tvshow-", String.Empty).Trim, cList)
        Next
        cbList_TVShows.DataSource = dicListViews_TVShows.ToList
        cbList_TVShows.DisplayMember = "Key"
        cbList_TVShows.ValueMember = "Value"
        cbList_TVShows.SelectedIndex = 0
    End Sub

    Private Sub dlgExportMovies_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()

        'Start worker
        ShowStatus(True)
        bwLoadInfo = New System.ComponentModel.BackgroundWorker
        bwLoadInfo.WorkerSupportsCancellation = True
        bwLoadInfo.WorkerReportsProgress = True
        bwLoadInfo.RunWorkerAsync()
    End Sub

    Private Sub DoCancel()
        tslblStatus.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        tspbStatus.Style = ProgressBarStyle.Marquee
        tspbStatus.MarqueeAnimationSpeed = 25
        tslblFile.Visible = False

        If bwLoadInfo.IsBusy Then
            bwLoadInfo.CancelAsync()
            'if is canceled while loading from DB we have to proper reload from DB
            bHasChangedList_Movies = True
            bHasChangedList_TVShows = True
        End If
        If bwCreateTemplate.IsBusy Then
            bwCreateTemplate.CancelAsync()
        End If
        While bwLoadInfo.IsBusy OrElse bwCreateTemplate.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        If btnClose.Text = Master.eLang.GetString(167, "Cancel") Then
            ShowStatus(False)
            btnBuild.Enabled = True
        Else
            Close()
        End If
    End Sub

    Private Sub LoadHTML(ByVal TempIndexFile As String)
        tslblStatus.Text = Master.eLang.GetString(326, "Loading. Please wait...")

        Try
            wbPreview.Navigate(TempIndexFile)
        Catch ex As Exception
            btnSave.Enabled = False
        End Try
    End Sub

    Private Function ShowProgressCreateTemplate(ByVal title As String, ByVal operation As String) As Boolean
        Dim Info As New KeyValuePair(Of String, String)(title, operation)
        bwCreateTemplate.ReportProgress(0, Info)
        If bwCreateTemplate.CancellationPending Then Return False
        Return True
    End Function

    Private Sub SetUp()
        Text = Master.eLang.GetString(328, "Export Movies")
        btnBuild.Text = Master.eLang.GetString(1004, "Generate HTML...")
        btnClose.Text = Master.eLang.GetString(19, "Close")
        btnSave.Text = Master.eLang.GetString(273, "Save")
        lblExportPath.Text = Master.eLang.GetString(995, "Export Path")
        lblList_Movies.Text = Master.eLang.GetString(982, "Movie List")
        lblList_TVShows.Text = Master.eLang.GetString(983, "TV Show List")
        lblTemplate.Text = Master.eLang.GetString(334, "Template")
    End Sub

    Private Sub ShowStatus(ByVal bEnable As Boolean)
        If bEnable Then
            btnClose.Text = Master.eLang.GetString(167, "Cancel")
            btnBuild.Enabled = False
            btnExportPath.Enabled = False
            btnSave.Enabled = False
            cbList_Movies.Enabled = False
            cbList_TVShows.Enabled = False
            cbTemplate.Enabled = False
            wbPreview.Visible = False
            tslblFile.Visible = True
            tslblStatus.Visible = True
            tspbStatus.Visible = True
            txtExportPath.Enabled = False
        Else
            btnClose.Text = Master.eLang.GetString(19, "Close")
            btnExportPath.Enabled = True
            cbList_Movies.Enabled = True
            cbList_TVShows.Enabled = True
            cbTemplate.Enabled = True
            tslblFile.Visible = False
            tslblStatus.Visible = False
            tspbStatus.Visible = False
            txtExportPath.Enabled = True
        End If
    End Sub

    Private Sub txtExportPath_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtExportPath.TextChanged
        If Not String.IsNullOrEmpty(txtExportPath.Text) Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub wbPreview_DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs) Handles wbPreview.DocumentCompleted
        If Not bCancelled Then
            wbPreview.Visible = True
            If Not cbTemplate.Text = String.Empty AndAlso Not String.IsNullOrEmpty(txtExportPath.Text) Then
                btnSave.Enabled = True
            End If
        End If
        ShowStatus(False)
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class