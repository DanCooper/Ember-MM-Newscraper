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

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports EmberAPI
Imports NLog

Public Class dlgBulkRenamer_Movie

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents bwDoRename As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker

    Private bsMovies As New BindingSource
    Private CancelRename As Boolean = False
    Private DoneRename As Boolean = False
    Private FFRenamer As FileFolderRenamer
    Private isLoaded As Boolean = False
    Private run_once As Boolean = True
    Private _columnsize(9) As Integer
    Private dHelpTips As dlgHelpTips

    Public FilterMovies As String = String.Empty
    Public FilterMoviesSearch As String = String.Empty
    Public FilterMoviesType As String = String.Empty

#End Region 'Fields

#Region "Delegates"

    Delegate Sub MyFinish()

    Delegate Sub MyStart()

#End Region 'Delegates

#Region "Methods"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If DoneRename Then
            CancelRename = True
        Else
            DoCancel()
        End If
    End Sub

    Private Sub bwbwDoRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDoRename.RunWorkerCompleted
        pnlCancel.Visible = False
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub bwDoRename_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDoRename.DoWork
        FFRenamer.DoRename_Movies(AddressOf ShowProgressRename)
    End Sub

    Private Sub bwDoRename_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDoRename.ProgressChanged
        pbCompile.Value = e.ProgressPercentage
        lblFile.Text = e.UserState.ToString
    End Sub

    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        Try
            Dim MovieFile As New FileFolderRenamer.FileRename
            Dim _currMovie As New Structures.DBMovie
            Dim hasFilter As Boolean = False
            Dim dbFilter As String = String.Empty

            If Not String.IsNullOrEmpty(Me.FilterMoviesSearch) AndAlso Not String.IsNullOrEmpty(Me.FilterMoviesType) Then
                Select Case Me.FilterMoviesType
                    Case Master.eLang.GetString(100, "Actor")
                        dbFilter = String.Concat("ID IN (SELECT MovieID FROM MoviesActors WHERE ActorName LIKE '%", Me.FilterMoviesSearch, "%')")
                    Case Master.eLang.GetString(233, "Role")
                        dbFilter = String.Concat("ID IN (SELECT MovieID FROM MoviesActors WHERE Role LIKE '%", Me.FilterMoviesSearch, "%')")
                End Select
                hasFilter = True
            End If

            If Not String.IsNullOrEmpty(Me.FilterMovies) Then
                If hasFilter Then
                    dbFilter = String.Concat("(", Me.FilterMovies, ") AND ", dbFilter).Trim
                Else
                    dbFilter = String.Concat(Me.FilterMovies).Trim
                    hasFilter = True
                End If
            End If

            ' Load movies using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim _tmpPath As String = String.Empty
                Dim iProg As Integer = 0
                If Not hasFilter Then
                    SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM movies;")
                Else
                    SQLNewcommand.CommandText = String.Format("SELECT COUNT(id) AS mcount FROM movies WHERE {0};", dbFilter)
                End If
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLcount.HasRows AndAlso SQLcount.Read() Then
                        Me.bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                    End If
                End Using
                If Not hasFilter Then
                    SQLNewcommand.CommandText = String.Concat("SELECT NfoPath, id FROM movies ORDER BY ListTitle ASC;")
                Else
                    SQLNewcommand.CommandText = String.Format("SELECT NfoPath, id FROM movies WHERE {0} ORDER BY ListTitle ASC;", dbFilter)
                End If
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            Try
                                If Not DBNull.Value.Equals(SQLreader("NfoPath")) AndAlso Not DBNull.Value.Equals(SQLreader("id")) Then
                                    _tmpPath = SQLreader("NfoPath").ToString
                                    If Not String.IsNullOrEmpty(_tmpPath) Then

                                        MovieFile = New FileFolderRenamer.FileRename
                                        _currMovie = Master.DB.LoadMovieFromDB(Convert.ToInt32(SQLreader("id")))

                                        If Not _currMovie.ID = -1 AndAlso Not String.IsNullOrEmpty(_currMovie.Filename) Then
                                            MovieFile = FileFolderRenamer.GetInfo_Movie(_currMovie)

                                            For Each i As String In FFRenamer.MovieFolders
                                                If _currMovie.Filename.StartsWith(i, StringComparison.OrdinalIgnoreCase) Then
                                                    MovieFile.BasePath = If(i.EndsWith(Path.DirectorySeparatorChar.ToString), i.Substring(0, i.Length - 1), i)
                                                    If FileUtils.Common.isVideoTS(_currMovie.Filename) Then
                                                        MovieFile.Parent = Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).Name
                                                        If MovieFile.BasePath = Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName Then
                                                            MovieFile.OldPath = String.Empty
                                                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                                                        Else
                                                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName).FullName.Replace(MovieFile.BasePath, String.Empty)
                                                        End If
                                                        MovieFile.IsVideo_TS = True
                                                    ElseIf FileUtils.Common.isBDRip(_currMovie.Filename) Then
                                                        MovieFile.Parent = Directory.GetParent(Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName).Name
                                                        If MovieFile.BasePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName).FullName Then
                                                            MovieFile.OldPath = String.Empty
                                                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                                                        Else
                                                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName).FullName).FullName.Replace(MovieFile.BasePath, String.Empty)
                                                        End If
                                                        MovieFile.IsBDMV = True
                                                    Else
                                                        MovieFile.Parent = Directory.GetParent(_currMovie.Filename).Name
                                                        If MovieFile.BasePath = Directory.GetParent(_currMovie.Filename).FullName Then
                                                            MovieFile.OldPath = String.Empty
                                                            MovieFile.BasePath = Directory.GetParent(MovieFile.BasePath).FullName
                                                        Else
                                                            MovieFile.OldPath = Directory.GetParent(Directory.GetParent(_currMovie.Filename).FullName).FullName.Replace(MovieFile.BasePath, String.Empty)
                                                        End If
                                                    End If
                                                End If
                                            Next

                                            If Not MovieFile.IsVideo_TS AndAlso Not MovieFile.IsBDMV Then
                                                MovieFile.FileName = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(_currMovie.Filename))
                                                Dim stackMark As String = Path.GetFileNameWithoutExtension(_currMovie.Filename).Replace(MovieFile.FileName, String.Empty).ToLower
                                                If Not stackMark = String.Empty AndAlso _currMovie.Movie.Title.ToLower.EndsWith(stackMark) Then
                                                    MovieFile.FileName = Path.GetFileNameWithoutExtension(_currMovie.Filename)
                                                End If
                                            ElseIf MovieFile.IsBDMV Then
                                                MovieFile.FileName = String.Concat("BDMV", Path.DirectorySeparatorChar, "STREAM")
                                            Else
                                                MovieFile.FileName = "VIDEO_TS"
                                            End If

                                            MovieFile.Extension = Path.GetExtension(_currMovie.Filename)

                                            FFRenamer.AddMovie(MovieFile)

                                            Me.bwLoadInfo.ReportProgress(iProg, _currMovie.ListTitle)
                                        End If
                                    End If
                                End If
                            Catch ex As Exception
                                logger.Error(New StackFrame().GetMethod().Name, ex)
                            End Try
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
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwLoadInfo_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadInfo.ProgressChanged
        If e.ProgressPercentage >= 0 Then
            Me.pbCompile.Value = e.ProgressPercentage
            Me.lblFile.Text = e.UserState.ToString
        Else
            Me.pbCompile.Maximum = Convert.ToInt32(e.UserState)
        End If
    End Sub

    Private Sub bwLoadInfo_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadInfo.RunWorkerCompleted
        Try
            If Not e.Cancelled Then
                Rename_Button.Enabled = True
                isLoaded = True
                tmrSimul.Enabled = True
            Else
            End If
            Me.pnlCancel.Visible = False
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub chkRenamedOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRenamedOnly.CheckedChanged
        If chkRenamedOnly.Checked Then
            bsMovies.Filter = "IsRenamed = True AND IsLocked = False"
        Else
            bsMovies.RemoveFilter()
        End If
    End Sub

    Private Sub Close_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Close_Button.Click
        If DoneRename Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If

        Me.Close()
    End Sub

    Private Sub cmsMovieList_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsMovieList.Opening
        Dim count As Integer = FFRenamer.GetCount_Movies
        Dim lockcount As Integer = FFRenamer.GetCountLocked_Movies
        If count > 0 Then
            If lockcount > 0 Then
                tsmUnlockAll.Visible = True
                If lockcount < count Then
                    tsmLockAll.Visible = True
                Else
                    tsmLockAll.Visible = False
                End If
                If lockcount = count Then
                    tsmLockAll.Visible = False
                End If

            Else
                tsmLockAll.Visible = True
                tsmUnlockAll.Visible = False
            End If
        Else
            tsmUnlockAll.Visible = False
            tsmLockAll.Visible = False
        End If
        tsmLockMovie.Visible = False
        tsmUnlockMovie.Visible = False
        For Each row As DataGridViewRow In dgvMoviesList.SelectedRows
            If Convert.ToBoolean(row.Cells(5).Value) Then
                tsmUnlockMovie.Visible = True
            Else
                tsmLockMovie.Visible = True
            End If
        Next
    End Sub

    Private Sub dgvMoviesList_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMoviesList.CellPainting
        Try

            If (e.ColumnIndex = 3 OrElse e.ColumnIndex = 4) AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(dgvMoviesList.Rows(e.RowIndex).Cells(5).Value) Then
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.ForeColor = Color.Red
                ElseIf Not IsNothing(e.Value) AndAlso Not dgvMoviesList.Rows(e.RowIndex).Cells(e.ColumnIndex - 2).Value.ToString = e.Value.ToString Then
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    If (Convert.ToBoolean(dgvMoviesList.Rows(e.RowIndex).Cells(6).Value) AndAlso e.ColumnIndex = 3) OrElse (Convert.ToBoolean(dgvMoviesList.Rows(e.RowIndex).Cells(7).Value) AndAlso e.ColumnIndex = 4) Then
                        e.CellStyle.ForeColor = Color.Purple
                    Else
                        e.CellStyle.ForeColor = Color.Blue
                    End If
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dgvMoviesList_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMoviesList.ColumnHeaderMouseClick
        Me.dgvMoviesList.Sort(Me.dgvMoviesList.Columns(e.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub dgvMoviesList_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvMoviesList.ColumnWidthChanged
        If Not dgvMoviesList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None OrElse dgvMoviesList.Columns.Count < 9 OrElse Convert.ToBoolean(dgvMoviesList.Tag) Then Return
        Dim sum As Integer = 0
        For Each c As DataGridViewColumn In dgvMoviesList.Columns
            If c.Visible Then sum += c.Width
        Next
        If sum < dgvMoviesList.Width Then
            e.Column.Width = dgvMoviesList.Width - (sum - e.Column.Width)
        End If
    End Sub

    Private Sub dlgBulkRename_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.bwLoadInfo.IsBusy Then
            Me.DoCancel()
            While Me.bwLoadInfo.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End If
    End Sub

    Private Sub dlgBulkRename_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SetUp()

        FFRenamer = New FileFolderRenamer
        ' temporarily disabled - TODO: autoresizing
        'Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
        'Using g As Graphics = Graphics.FromImage(iBackground)
        '    g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
        '    Me.pnlTop.BackgroundImage = iBackground
        'End Using

    End Sub

    Private Sub dlgBulkRename_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()

        Try
            ' Show Cancel Panel
            btnCancel.Visible = True
            lblCompiling.Visible = True
            pbCompile.Visible = True
            pbCompile.Style = ProgressBarStyle.Continuous
            lblCanceling.Visible = False
            pnlCancel.Visible = True
            Application.DoEvents()

            'Start worker
            Me.bwLoadInfo = New System.ComponentModel.BackgroundWorker
            Me.bwLoadInfo.WorkerSupportsCancellation = True
            Me.bwLoadInfo.WorkerReportsProgress = True
            Me.bwLoadInfo.RunWorkerAsync()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DoCancel()
        Try
            Me.bwLoadInfo.CancelAsync()
            btnCancel.Visible = False
            lblCompiling.Visible = False
            pbCompile.Style = ProgressBarStyle.Marquee
            pbCompile.MarqueeAnimationSpeed = 25
            lblCanceling.Visible = True
            lblFile.Visible = False
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DoProcess()
        Dim Sta As MyStart = New MyStart(AddressOf Start)
        Dim Fin As MyFinish = New MyFinish(AddressOf Finish)
        Me.Invoke(Sta)
        FFRenamer.ProccessFiles_Movies(txtFolderPattern.Text, txtFilePattern.Text, txtFolderPatternNotSingle.Text)
        Me.Invoke(Fin)
    End Sub

    Private Sub Finish()
        Simulate()
        Me.pnlCancel.Visible = False
    End Sub

    Private Sub Rename_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rename_Button.Click
        DoneRename = True
        pnlCancel.Visible = True
        lblCompiling.Text = Master.eLang.GetString(173, "Renaming...")
        lblFile.Visible = True
        pbCompile.Style = ProgressBarStyle.Continuous
        pbCompile.Maximum = FFRenamer.GetMoviesCount
        pbCompile.Value = 0
        Application.DoEvents()
        'Start worker
        Me.bwDoRename = New System.ComponentModel.BackgroundWorker
        Me.bwDoRename.WorkerSupportsCancellation = True
        Me.bwDoRename.WorkerReportsProgress = True
        Me.bwDoRename.RunWorkerAsync()
    End Sub

    Sub setLock(ByVal lock As Boolean)
        For Each row As DataGridViewRow In dgvMoviesList.SelectedRows
            FFRenamer.SetIsLocked(row.Cells(1).Value.ToString, row.Cells(2).Value.ToString, lock)
            row.Cells(5).Value = lock
        Next

        If Me.chkRenamedOnly.Checked AndAlso lock Then
            Me.dgvMoviesList.ClearSelection()
            Me.dgvMoviesList.CurrentCell = Nothing
        End If

        dgvMoviesList.Refresh()
    End Sub

    Sub setLockAll(ByVal lock As Boolean)
        Try
            FFRenamer.SetIsLocked(String.Empty, String.Empty, False)
            For Each row As DataGridViewRow In dgvMoviesList.Rows
                row.Cells(5).Value = lock
            Next

            If Me.chkRenamedOnly.Checked AndAlso lock Then
                Me.dgvMoviesList.ClearSelection()
                Me.dgvMoviesList.CurrentCell = Nothing
            End If

            dgvMoviesList.Refresh()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(174, "Bulk Renamer")
        Me.Close_Button.Text = Master.eLang.GetString(19, "Close")
        Me.lblTopDetails.Text = Master.eLang.GetString(175, "Rename movies and files")
        Me.lblTopTitle.Text = Me.Text
        Me.lblCompiling.Text = Master.eLang.GetString(177, "Compiling Movie List...")
        Me.lblCanceling.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.Rename_Button.Text = Master.eLang.GetString(257, "Rename")
        Me.tsmLockMovie.Text = Master.eLang.GetString(24, "Lock")
        Me.tsmUnlockMovie.Text = Master.eLang.GetString(108, "Unlock")
        Me.tsmLockAll.Text = Master.eLang.GetString(169, "Lock All")
        Me.tsmUnlockAll.Text = Master.eLang.GetString(170, "Unlock All")
        Me.lblFolderPattern.Text = Master.eLang.GetString(258, "Folder Pattern (for Single movie in Folder)")
        Me.lblFilePattern.Text = Master.eLang.GetString(259, "File Pattern")
        Me.lblFolderPatternNotSingle.Text = Master.eLang.GetString(260, "Folder Pattern (for Multiple movies in Folder)")
        Me.chkRenamedOnly.Text = Master.eLang.GetString(261, "Display Only Movies That Will Be Renamed")

        Dim frmToolTip As New ToolTip()
        Dim s As String = String.Format(Master.eLang.GetString(262, "$1 = First Letter of the Title{0}$A = Audio Channels{0}$B = Base Path{0}$C = Director{0}$D = Directory{0}$E = Sort Title{0}$F = File Name{0}$G = Genre (Follow with a space, dot or hyphen to change separator){0}$H = Video Codec{0}$I = IMDB ID{0}$J = Audio Codec{0}$L = List Title{0}$M = MPAA{0}$N = Collection Name{0}$O = OriginalTitle{0}$P = Rating{0}$Q.E? = Episode Separator (. or _ or x), Episode Prefix{0}$R = Resolution{0}$S = Video Source{0}$T = Title{0}$V = 3D (If Multiview > 1){0}$W.S?.E? = Seasons Separator (. or _), Season Prefix, Episode Separator (. or _ or x), Episode Prefix{0}$Y = Year{0}$X. (Replace Space with .){0}$Z = Show Title{0}{{}} = Optional{0}$?aaa?bbb? = Replace aaa with bbb{0}$- = Remove previous char if next pattern does not have a value{0}$+ = Remove next char if previous pattern does not have a value{0}$^ = Remove previous and next char if next pattern does not have a value"), vbNewLine)
        frmToolTip.SetToolTip(Me.txtFolderPattern, s)
        frmToolTip.SetToolTip(Me.txtFilePattern, s)
        frmToolTip.SetToolTip(Me.txtFolderPatternNotSingle, s)
    End Sub

    Private Function ShowProgressRename(ByVal mov As String, ByVal iProg As Integer) As Boolean
        Me.bwDoRename.ReportProgress(iProg, mov.ToString)
        If CancelRename Then Return False
        Return True
    End Function

    Private Sub Simulate()
        Try
            With Me.dgvMoviesList
                If Not run_once Then
                    For Each c As DataGridViewColumn In .Columns
                        _columnsize(c.Index) = c.Width
                    Next
                End If
                .DataSource = Nothing
                .Rows.Clear()
                .AutoGenerateColumns = True
                If run_once Then
                    .Tag = False
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End If
                bsMovies.DataSource = FFRenamer.GetMovies
                .DataSource = bsMovies
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
                If run_once Then
                    For Each c As DataGridViewColumn In .Columns
                        c.MinimumWidth = Convert.ToInt32(.Width / 5)
                    Next
                    .AutoResizeColumns()
                    .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                    For Each c As DataGridViewColumn In .Columns
                        c.MinimumWidth = 20
                    Next
                    run_once = False
                Else
                    .Tag = True
                    For Each c As DataGridViewColumn In .Columns
                        c.Width = _columnsize(c.Index)
                    Next
                    .Tag = False
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub Start()
        Me.btnCancel.Visible = False
        Me.lblFile.Visible = False
        Me.pbCompile.Style = ProgressBarStyle.Marquee
        Me.pnlCancel.Visible = True
    End Sub

    Private Sub tmrSimul_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSimul.Tick
        Try
            'Need to make simulate thread safe
            tmrSimul.Enabled = False
            If isLoaded Then
                Dim tThread As Threading.Thread = New Threading.Thread(AddressOf DoProcess)
                tThread.Start()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub tsmLockAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmLockAll.Click
        setLockAll(True)
    End Sub

    Private Sub tsmLockMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmLockMovie.Click
        setLock(True)
    End Sub

    Private Sub tsmUnlockAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmUnlockAll.Click
        setLockAll(False)
    End Sub

    Private Sub tsmUnlockMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmUnlockMovie.Click
        setLock(False)
    End Sub

    Private Sub txtFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePattern.TextChanged
        Try
            If String.IsNullOrEmpty(txtFilePattern.Text) Then txtFilePattern.Text = "$F"
            tmrSimul.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub txtFolderNotSingle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderPatternNotSingle.TextChanged
        Try
            If String.IsNullOrEmpty(txtFolderPatternNotSingle.Text) Then txtFolderPatternNotSingle.Text = "$D"
            tmrSimul.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub txtFolder_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderPattern.TextChanged
        Try
            If String.IsNullOrEmpty(txtFolderPattern.Text) Then txtFolderPattern.Text = "$D"
            tmrSimul.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Sub LoadHelpTips()
        If dHelpTips Is Nothing OrElse dHelpTips.IsDisposed Then
            dHelpTips = New dlgHelpTips
        End If
        Dim s As String = String.Format(Master.eLang.GetString(262, "$1 = First Letter of the Title{0}$A = Audio Channels{0}$B = Base Path{0}$C = Director{0}$D = Directory{0}$E = Sort Title{0}$F = File Name{0}$G = Genre (Follow with a space, dot or hyphen to change separator){0}$H = Video Codec{0}$I = IMDB ID{0}$J = Audio Codec{0}$L = List Title{0}$M = MPAA{0}$N = Collection Name{0}$O = OriginalTitle{0}$P = Rating{0}$Q.E? = Episode Separator (. or _ or x), Episode Prefix{0}$R = Resolution{0}$S = Video Source{0}$T = Title{0}$V = 3D (If Multiview > 1){0}$W.S?.E? = Seasons Separator (. or _), Season Prefix, Episode Separator (. or _ or x), Episode Prefix{0}$Y = Year{0}$X. (Replace Space with .){0}$Z = Show Title{0}{{}} = Optional{0}$?aaa?bbb? = Replace aaa with bbb{0}$- = Remove previous char if next pattern does not have a value{0}$+ = Remove next char if previous pattern does not have a value{0}$^ = Remove previous and next char if next pattern does not have a value"), vbNewLine)
        dHelpTips.lblTips.Text = s
        dHelpTips.Width = dHelpTips.lblTips.Width + 5
        dHelpTips.Height = dHelpTips.lblTips.Height + 35
        dHelpTips.Top = Me.Top + 10
        dHelpTips.Left = Me.Right - dHelpTips.Width - 10
        If dHelpTips.Visible Then
            dHelpTips.Hide()
        Else
            dHelpTips.Show(Me)
        End If
    End Sub

    Private Sub btnFolderPatternHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderPatternHelp.Click
        LoadHelpTips()
    End Sub

    Private Sub btnFolderPatternNotSingleHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFolderPatternNotSingleHelp.Click
        LoadHelpTips()
    End Sub

    Private Sub btnFilePatternHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilePatternHelp.Click
        LoadHelpTips()
    End Sub

#End Region 'Methods

End Class