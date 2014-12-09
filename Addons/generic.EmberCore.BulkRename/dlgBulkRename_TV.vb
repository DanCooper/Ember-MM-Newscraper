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

Imports System.Windows.Forms
Imports EmberAPI
Imports NLog
Imports System.IO

Public Class dlgBulkRenamer_TV

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Friend WithEvents bwDoRename As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker

    Private bsEpisodes As New BindingSource
    Private CancelRename As Boolean = False
    Private DoneRename As Boolean = False
    Private FFRenamer As FileFolderRenamer
    Private isLoaded As Boolean = False
    Private run_once As Boolean = True
    Private _columnsize(9) As Integer
    Private dHelpTips As dlgHelpTips

    Public FilterShows As String = String.Empty
    Public FilterShowsSearch As String = String.Empty
    Public FilterShowsType As String = String.Empty

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

    Private Sub bwDoRename_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwDoRename.RunWorkerCompleted
        pnlCancel.Visible = False
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub bwDoRename_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwDoRename.DoWork
        FFRenamer.DoRename_Episodes(AddressOf ShowProgressRename)
    End Sub

    Private Sub bwDoRename_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwDoRename.ProgressChanged
        pbCompile.Value = e.ProgressPercentage
        lblFile.Text = e.UserState.ToString
    End Sub

    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        Try
            Dim EpisodeFile As New FileFolderRenamer.FileRename
            Dim _currShow As New Structures.DBTV
            Dim hasFilter As Boolean = False
            Dim dbFilter As String = String.Empty

            'If Not String.IsNullOrEmpty(Me.FilterShowsSearch) AndAlso Not String.IsNullOrEmpty(Me.FilterShowsType) Then
            '    Select Case Me.FilterShowsType
            '        Case Master.eLang.GetString(100, "Actor")
            '            dbFilter = String.Concat("ID IN (SELECT MovieID FROM MoviesActors WHERE ActorName LIKE '%", Me.FilterShowsSearch, "%')")
            '        Case Master.eLang.GetString(233, "Role")
            '            dbFilter = String.Concat("ID IN (SELECT MovieID FROM MoviesActors WHERE Role LIKE '%", Me.FilterShowsSearch, "%')")
            '    End Select
            '    hasFilter = True
            'End If

            If Not String.IsNullOrEmpty(Me.FilterShows) Then
                If hasFilter Then
                    dbFilter = String.Concat("(", Me.FilterShows, ") AND ", dbFilter).Trim
                Else
                    dbFilter = String.Concat(Me.FilterShows).Trim
                    hasFilter = True
                End If
            End If

            ' Load episodes using path from DB
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                Dim _tmpPath As String = String.Empty
                Dim iProg As Integer = 0
                If Not hasFilter Then
                    SQLNewcommand.CommandText = String.Concat("SELECT COUNT(id) AS mcount FROM TVEps WHERE Missing = 0;")
                Else
                    SQLNewcommand.CommandText = String.Format("SELECT COUNT(id) AS mcount FROM TVEps WHERE Missing = 0 AND {0};", dbFilter)
                End If
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLcount.HasRows AndAlso SQLcount.Read() Then
                        Me.bwLoadInfo.ReportProgress(-1, SQLcount("mcount")) ' set maximum
                    End If
                End Using
                If Not hasFilter Then
                    SQLNewcommand.CommandText = String.Concat("SELECT NfoPath, id FROM TVEps WHERE Missing = 0 ORDER BY Title ASC;")
                Else
                    SQLNewcommand.CommandText = String.Format("SELECT NfoPath, id FROM TVEps WHERE Missing = 0 AND {0} ORDER BY Title ASC;", dbFilter)
                End If
                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    If SQLreader.HasRows Then
                        While SQLreader.Read()
                            Try
                                If Not DBNull.Value.Equals(SQLreader("NfoPath")) AndAlso Not DBNull.Value.Equals(SQLreader("id")) Then
                                    _tmpPath = SQLreader("NfoPath").ToString
                                    If Not String.IsNullOrEmpty(_tmpPath) Then

                                        EpisodeFile = New FileFolderRenamer.FileRename
                                        _currShow = Master.DB.LoadTVEpFromDB(Convert.ToInt32(SQLreader("id")), True)

                                        If Not _currShow.EpID = -1 AndAlso Not _currShow.ShowID = -1 AndAlso Not String.IsNullOrEmpty(_currShow.Filename) Then
                                            EpisodeFile = FileFolderRenamer.GetInfo_Episode(_currShow)

                                            FFRenamer.AddEpisode(EpisodeFile)

                                            Me.bwLoadInfo.ReportProgress(iProg, _currShow.TVEp.Title)
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
            bsEpisodes.Filter = "IsRenamed = True AND IsLocked = False"
        Else
            bsEpisodes.RemoveFilter()
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

    Private Sub cmsEpisodeList_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsEpisodeList.Opening
        Dim count As Integer = FFRenamer.GetCount_Episodes
        Dim lockcount As Integer = FFRenamer.GetCountLocked_Episodes
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
        tsmLockEpisode.Visible = False
        tsmUnlockEpisode.Visible = False
        For Each row As DataGridViewRow In dgvEpisodesList.SelectedRows
            If Convert.ToBoolean(row.Cells(5).Value) Then
                tsmUnlockEpisode.Visible = True
            Else
                tsmLockEpisode.Visible = True
            End If
        Next
    End Sub

    Private Sub dgvEpisodesList_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvEpisodesList.CellPainting
        Try

            If (e.ColumnIndex = 3 OrElse e.ColumnIndex = 4) AndAlso e.RowIndex >= 0 Then
                If Convert.ToBoolean(dgvEpisodesList.Rows(e.RowIndex).Cells(5).Value) Then
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    e.CellStyle.ForeColor = Color.Red
                ElseIf Not IsNothing(e.Value) AndAlso Not dgvEpisodesList.Rows(e.RowIndex).Cells(e.ColumnIndex - 2).Value.ToString = e.Value.ToString Then
                    e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    If (Convert.ToBoolean(dgvEpisodesList.Rows(e.RowIndex).Cells(6).Value) AndAlso e.ColumnIndex = 3) OrElse (Convert.ToBoolean(dgvEpisodesList.Rows(e.RowIndex).Cells(7).Value) AndAlso e.ColumnIndex = 4) Then
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

    Private Sub dgvEpisodesList_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvEpisodesList.ColumnHeaderMouseClick
        Me.dgvEpisodesList.Sort(Me.dgvEpisodesList.Columns(e.ColumnIndex), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub dgvEpisodesList_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles dgvEpisodesList.ColumnWidthChanged
        If Not dgvEpisodesList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None OrElse dgvEpisodesList.Columns.Count < 9 OrElse Convert.ToBoolean(dgvEpisodesList.Tag) Then Return
        Dim sum As Integer = 0
        For Each c As DataGridViewColumn In dgvEpisodesList.Columns
            If c.Visible Then sum += c.Width
        Next
        If sum < dgvEpisodesList.Width Then
            e.Column.Width = dgvEpisodesList.Width - (sum - e.Column.Width)
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
        FFRenamer.ProccessFiles_Episodes(txtFolderPatternSeasons.Text, txtFolderPatternShows.Text, txtFilePatternEpisodes.Text)
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
        pbCompile.Maximum = FFRenamer.GetEpisodesCount
        pbCompile.Value = 0
        Application.DoEvents()
        'Start worker
        Me.bwDoRename = New System.ComponentModel.BackgroundWorker
        Me.bwDoRename.WorkerSupportsCancellation = True
        Me.bwDoRename.WorkerReportsProgress = True
        Me.bwDoRename.RunWorkerAsync()
    End Sub

    Sub setLock(ByVal lock As Boolean)
        For Each row As DataGridViewRow In dgvEpisodesList.SelectedRows
            FFRenamer.SetIsLocked(row.Cells(1).Value.ToString, row.Cells(2).Value.ToString, lock)
            row.Cells(5).Value = lock
        Next

        If Me.chkRenamedOnly.Checked AndAlso lock Then
            Me.dgvEpisodesList.ClearSelection()
            Me.dgvEpisodesList.CurrentCell = Nothing
        End If

        dgvEpisodesList.Refresh()
    End Sub

    Sub setLockAll(ByVal lock As Boolean)
        Try
            FFRenamer.SetIsLocked(String.Empty, String.Empty, False)
            For Each row As DataGridViewRow In dgvEpisodesList.Rows
                row.Cells(5).Value = lock
            Next

            If Me.chkRenamedOnly.Checked AndAlso lock Then
                Me.dgvEpisodesList.ClearSelection()
                Me.dgvEpisodesList.CurrentCell = Nothing
            End If

            dgvEpisodesList.Refresh()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(274, "TV Bulk Renamer")
        Me.Close_Button.Text = Master.eLang.GetString(19, "Close")
        Me.lblTopDetails.Text = Master.eLang.GetString(275, "Rename TV Shows and files")
        Me.lblTopTitle.Text = Me.Text
        Me.lblCompiling.Text = Master.eLang.GetString(277, "Compiling TV Shows List...")
        Me.lblCanceling.Text = Master.eLang.GetString(178, "Canceling Compilation...")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.Rename_Button.Text = Master.eLang.GetString(257, "Rename")
        Me.tsmLockEpisode.Text = Master.eLang.GetString(24, "Lock")
        Me.tsmUnlockEpisode.Text = Master.eLang.GetString(108, "Unlock")
        Me.tsmLockAll.Text = Master.eLang.GetString(169, "Lock All")
        Me.tsmUnlockAll.Text = Master.eLang.GetString(170, "Unlock All")
        Me.lblFolderPatternShows.Text = Master.eLang.GetString(258, "Folder Pattern (for Single movie in Folder)")
        Me.lblFilePatternEpisodes.Text = Master.eLang.GetString(259, "File Pattern")
        Me.lblFolderPatternSeasons.Text = Master.eLang.GetString(260, "Folder Pattern (for Multiple movies in Folder)")
        Me.chkRenamedOnly.Text = Master.eLang.GetString(261, "Display Only Movies That Will Be Renamed")

        Dim frmToolTip As New ToolTip()
        Dim s As String = String.Format(Master.eLang.GetString(262, "$1 = First Letter of the Title{0}$A = Audio Channels{0}$B = Base Path{0}$C = Director{0}$D = Directory{0}$E = Sort Title{0}$F = File Name{0}$G = Genre (Follow with a space, dot or hyphen to change separator){0}$H = Video Codec{0}$I = IMDB ID{0}$J = Audio Codec{0}$L = List Title{0}$M = MPAA{0}$N = Collection Name{0}$O = OriginalTitle{0}$P = Rating{0}$Q.E? = Episode Separator (. or _ or x), Episode Prefix{0}$R = Resolution{0}$S = Video Source{0}$T = Title{0}$V = 3D (If Multiview > 1){0}$W.S?.E? = Seasons Separator (. or _), Season Prefix, Episode Separator (. or _ or x), Episode Prefix{0}$Y = Year{0}$X. (Replace Space with .){0}$Z = Show Title{0}{{}} = Optional{0}$?aaa?bbb? = Replace aaa with bbb{0}$- = Remove previous char if next pattern does not have a value{0}$+ = Remove next char if previous pattern does not have a value{0}$^ = Remove previous and next char if next pattern does not have a value"), vbNewLine)
        frmToolTip.SetToolTip(Me.txtFilePatternEpisodes, s)
        frmToolTip.SetToolTip(Me.txtFolderPatternSeasons, s)
        frmToolTip.SetToolTip(Me.txtFolderPatternShows, s)
    End Sub

    Private Function ShowProgressRename(ByVal mov As String, ByVal iProg As Integer) As Boolean
        Me.bwDoRename.ReportProgress(iProg, mov.ToString)
        If CancelRename Then Return False
        Return True
    End Function

    Private Sub Simulate()
        Try
            With Me.dgvEpisodesList
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
                bsEpisodes.DataSource = FFRenamer.GetEpisodes
                .DataSource = bsEpisodes
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

    Private Sub tsmLockEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmLockEpisode.Click
        setLock(True)
    End Sub

    Private Sub tsmUnlockAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmUnlockAll.Click
        setLockAll(False)
    End Sub

    Private Sub tsmUnlockEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmUnlockEpisode.Click
        setLock(False)
    End Sub

    Private Sub txtFilePatternEpisodes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePatternEpisodes.TextChanged
        Try
            If String.IsNullOrEmpty(txtFilePatternEpisodes.Text) Then txtFilePatternEpisodes.Text = "$F"
            tmrSimul.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub txtFolderPatternSeasons_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderPatternSeasons.TextChanged
        Try
            If String.IsNullOrEmpty(txtFolderPatternSeasons.Text) Then txtFolderPatternSeasons.Text = "Season $K"
            tmrSimul.Enabled = True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub txtFolderPatternShows_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderPatternShows.TextChanged
        Try
            If String.IsNullOrEmpty(txtFolderPatternShows.Text) Then txtFolderPatternShows.Text = "$Z"
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

    Private Sub btnFilePatternHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilePatternHelp.Click
        LoadHelpTips()
    End Sub

#End Region 'Methods

End Class
