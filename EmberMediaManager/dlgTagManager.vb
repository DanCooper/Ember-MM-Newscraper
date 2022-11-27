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

Public Class dlgTagManager

#Region "Fields"

    Private _currTagState As TagState = Nothing
    Private _toProceed_Movies As New List(Of Database.DBElement)
    Private _toProceed_TVShows As New List(Of Database.DBElement)
    Private _tagList As New List(Of TagState)

    Private bsTags As New BindingSource
    Private bsTagged_Movies As New BindingSource
    Private bsTagged_TVShows As New BindingSource
    Private bsDatabase_Movies As New BindingSource
    Private bsDatabase_TVShows As New BindingSource

    Private dtDatabase_Movies As New DataTable
    Private dtDatabase_TVShows As New DataTable

    Friend WithEvents bwProcessTags As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Dialog"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FormUtils.Forms.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Setup()
        DataGridView_Fill_Tag()
        DataGridView_Fill_Movie()
        DataGridView_Fill_TVShow()
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
        btnOk.Enabled = False
        tspbStatus.Visible = True
        tsslStatus.Visible = True
        bwProcessTags.WorkerReportsProgress = True
        bwProcessTags.RunWorkerAsync()
    End Sub

    Private Sub Setup()
        With Master.eLang
            btnCancel.Text = .Cancel
            btnOk.Text = .OK
            gbMovies.Text = .GetString(36, "Movies")
            gbTagged.Text = .GetString(1257, "Tagged")
            gbTags.Text = .GetString(243, "Tags")
            gbTVShows.Text = .GetString(653, "TV Shows")
        End With
    End Sub

#End Region 'Dialog

#Region "Methods"

    Private Sub bwProcessTags_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwProcessTags.RunWorkerCompleted
        tspbStatus.Visible = False
        tsslStatus.Visible = False
        MessageBox.Show(String.Format("{0} item(s) changed", _toProceed_Movies.Count + _toProceed_TVShows.Count), Master.eLang.GetString(362, "Done"), MessageBoxButtons.OK, MessageBoxIcon.Information)
        DialogResult = DialogResult.OK
    End Sub

    Private Sub bwProcessTags_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwProcessTags.ProgressChanged
        If e.ProgressPercentage = -2 Then
            'set max to count of tags
            tspbStatus.Value = 0
            tspbStatus.Maximum = _tagList.Count
        ElseIf e.ProgressPercentage = -1 Then
            'set max to count of movies and tvshows to change
            tspbStatus.Value = 0
            tspbStatus.Maximum = _toProceed_Movies.Count + _toProceed_TVShows.Count
        Else
            tspbStatus.Value = e.ProgressPercentage
            tsslStatus.Text = e.UserState.ToString
        End If
    End Sub

    Private Sub bwProcessTags_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwProcessTags.DoWork
        Process_Tags()
        SaveToDatabase()
    End Sub

    Private Sub Change_Tag(ByVal ids As IEnumerable(Of Long), ByVal contentType As Enums.ContentType, ByVal removeTag As String, ByVal addTag As String)
        For Each id In ids
            Change_Tag(id, contentType, removeTag, addTag)
        Next
    End Sub

    Private Sub Change_Tag(ByVal id As Long, ByVal contentType As Enums.ContentType, ByVal removeTag As String, ByVal addTag As String)
        Select Case contentType
            Case Enums.ContentType.Movie
                Dim dbelement = _toProceed_Movies.FirstOrDefault(Function(f) f.ID = id)
                If dbelement Is Nothing Then
                    dbelement = Master.DB.Load_Movie(id)
                    _toProceed_Movies.Add(dbelement)
                End If
                If Not String.IsNullOrEmpty(removeTag) Then dbelement.Movie.Tags.Remove(removeTag)
                If Not String.IsNullOrEmpty(addTag) Then dbelement.Movie.Tags.Add(addTag)
            Case Enums.ContentType.TVShow
                Dim dbelement = _toProceed_TVShows.FirstOrDefault(Function(f) f.ID = id)
                If dbelement Is Nothing Then
                    dbelement = Master.DB.Load_TVShow(id, False, False, False)
                    _toProceed_TVShows.Add(dbelement)
                End If
                If Not String.IsNullOrEmpty(removeTag) Then dbelement.TVShow.Tags.Remove(removeTag)
                If Not String.IsNullOrEmpty(addTag) Then dbelement.TVShow.Tags.Add(addTag)
        End Select
    End Sub

    Private Sub DataGridView_Add_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles _
        dgvMovie.CellDoubleClick,
        dgvTVShow.CellDoubleClick
        If _currTagState IsNot Nothing Then
            Select Case True
                Case sender Is dgvMovie
                    Set_State(dgvMovie.SelectedRows(0), _currTagState.Movies, Operation.Add)
                Case sender Is dgvTVShow
                    Set_State(dgvTVShow.SelectedRows(0), _currTagState.TVShows, Operation.Add)
            End Select
            dgvTags.Refresh()
        End If
    End Sub

    Private Sub DataGridView_Remove_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles _
        dgvTagged_Movie.CellDoubleClick,
        dgvTagged_TVShow.CellDoubleClick
        If _currTagState IsNot Nothing Then
            Select Case True
                Case sender Is dgvTagged_Movie
                    Set_State(dgvTagged_Movie.SelectedRows(0), _currTagState.Movies, Operation.Remove)
                Case sender Is dgvTagged_TVShow
                    Set_State(dgvTagged_TVShow.SelectedRows(0), _currTagState.TVShows, Operation.Remove)
            End Select
            dgvTags.Refresh()
        End If
    End Sub

    Private Sub DataGridView_Fill_Movie()
        dgvMovie.SuspendLayout()
        Master.DB.FillDataTable(dtDatabase_Movies, "SELECT idMovie AS id, Title FROM movie")
        bsDatabase_Movies.DataSource = dtDatabase_Movies
        dgvMovie.DataSource = bsDatabase_Movies
        For i As Integer = 0 To dgvMovie.Columns.Count - 1
            dgvMovie.Columns(i).Visible = False
        Next
        dgvMovie.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvMovie.Columns("Title").MinimumWidth = 60
        dgvMovie.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")
        dgvMovie.Columns("Title").Visible = True
        dgvMovie.Sort(dgvMovie.Columns("Title"), ComponentModel.ListSortDirection.Ascending)
        dgvMovie.ResumeLayout()
    End Sub

    Private Sub DataGridView_Fill_Tag()
        dgvTags.SuspendLayout()
        Dim dbTags = Master.DB.LoadAll_Tags
        For Each item In dbTags
            _tagList.Add(New TagState With {
                         .Id = item.Id,
                         .Movies = item.Movies,
                         .Name = item.Name,
                         .NewName = item.Name,
                         .TVShows = item.TVShows
                         })
        Next
        bsTags.DataSource = _tagList
        dgvTags.DataSource = bsTags
        For i As Integer = 0 To dgvTags.Columns.Count - 1
            dgvTags.Columns(i).Visible = False
        Next
        dgvTags.Columns("ToDelete").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvTags.Columns("ToDelete").HeaderText = "Delete"
        dgvTags.Columns("ToDelete").DisplayIndex = 0
        dgvTags.Columns("ToDelete").Visible = True
        dgvTags.Columns("NewName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvTags.Columns("NewName").MinimumWidth = 60
        dgvTags.Columns("NewName").DisplayIndex = 1
        dgvTags.Columns("NewName").HeaderText = "Name"
        dgvTags.Columns("NewName").Visible = True
        dgvTags.Columns("MovieCount").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvTags.Columns("MovieCount").DisplayIndex = 2
        dgvTags.Columns("MovieCount").HeaderText = Master.eLang.GetString(36, "Movies")
        dgvTags.Columns("MovieCount").Visible = True
        dgvTags.Columns("TVShowCount").AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
        dgvTags.Columns("TVShowCount").DisplayIndex = 3
        dgvTags.Columns("TVShowCount").HeaderText = Master.eLang.GetString(653, "TV Shows")
        dgvTags.Columns("TVShowCount").Visible = True
        dgvTags.Columns("Id").ReadOnly = True
        dgvTags.Columns("Name").ReadOnly = True
        dgvTags.ResumeLayout()
    End Sub

    Private Sub DataGridView_Fill_Tagged()
        If _currTagState IsNot Nothing Then
            'Movies
            dgvTagged_Movie.SuspendLayout()
            bsTagged_Movies.DataSource = _currTagState.Movies
            dgvTagged_Movie.DataSource = bsTagged_Movies
            'add default filter to hide entries with state "ToRemove"
            bsTagged_Movies.Filter = "State <> 'ToRemove'"
            'hide all columns
            For i As Integer = 0 To dgvTagged_Movie.Columns.Count - 1
                dgvTagged_Movie.Columns(i).Visible = False
            Next
            dgvTagged_Movie.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvTagged_Movie.Columns("Title").Visible = True
            dgvTagged_Movie.Columns("Title").ReadOnly = True
            dgvTagged_Movie.Columns("Title").MinimumWidth = 83
            dgvTagged_Movie.Columns("Title").SortMode = DataGridViewColumnSortMode.Automatic
            dgvTagged_Movie.Columns("Title").ToolTipText = Master.eLang.GetString(21, "Title")
            dgvTagged_Movie.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")
            'set default sorting if user hasn't changed
            If dgvTagged_Movie.SortedColumn Is Nothing Then
                dgvTagged_Movie.Sort(dgvTagged_Movie.Columns("Title"), ComponentModel.ListSortDirection.Ascending)
            End If
            dgvTagged_Movie.ResumeLayout()

            'TVShows
            dgvTagged_TVShow.SuspendLayout()
            bsTagged_TVShows.DataSource = _currTagState.TVShows
            dgvTagged_TVShow.DataSource = bsTagged_TVShows
            'add default filter to hide entries with state "ToRemove"
            bsTagged_TVShows.Filter = "State <> 'ToRemove'"
            'hide all columns
            For i As Integer = 0 To dgvTagged_TVShow.Columns.Count - 1
                dgvTagged_TVShow.Columns(i).Visible = False
            Next
            dgvTagged_TVShow.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvTagged_TVShow.Columns("Title").Visible = True
            dgvTagged_TVShow.Columns("Title").ReadOnly = True
            dgvTagged_TVShow.Columns("Title").MinimumWidth = 83
            dgvTagged_TVShow.Columns("Title").SortMode = DataGridViewColumnSortMode.Automatic
            dgvTagged_TVShow.Columns("Title").ToolTipText = Master.eLang.GetString(21, "Title")
            dgvTagged_TVShow.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")
            'set default sorting if user hasn't changed
            If dgvTagged_TVShow.SortedColumn Is Nothing Then
                dgvTagged_TVShow.Sort(dgvTagged_TVShow.Columns("Title"), ComponentModel.ListSortDirection.Ascending)
            End If
            dgvTagged_TVShow.ResumeLayout()
        Else
            bsTagged_Movies.DataSource = Nothing
            bsTagged_TVShows.DataSource = Nothing
        End If
    End Sub

    Private Sub DataGridView_Fill_TVShow()
        dgvTVShow.SuspendLayout()
        Master.DB.FillDataTable(dtDatabase_TVShows, "SELECT idShow AS id, Title FROM tvshow")
        bsDatabase_TVShows.DataSource = dtDatabase_TVShows
        dgvTVShow.DataSource = bsDatabase_TVShows
        For i As Integer = 0 To dgvTVShow.Columns.Count - 1
            dgvTVShow.Columns(i).Visible = False
        Next
        dgvTVShow.Columns("Title").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvTVShow.Columns("Title").MinimumWidth = 60
        dgvTVShow.Columns("Title").HeaderText = Master.eLang.GetString(21, "Title")
        dgvTVShow.Columns("Title").Visible = True
        dgvTVShow.Sort(dgvTVShow.Columns("Title"), ComponentModel.ListSortDirection.Ascending)
        dgvTVShow.ResumeLayout()
    End Sub

    Private Sub DataGridView_KeyDown(sender As Object, e As KeyEventArgs) Handles _
        dgvMovie.KeyDown,
        dgvTagged_Movie.KeyDown,
        dgvTagged_TVShow.KeyDown,
        dgvTags.KeyDown,
        dgvTVShow.KeyDown
        If _currTagState IsNot Nothing Then
            If e.KeyValue = Keys.Delete OrElse e.KeyValue = Keys.Back Then
                Select Case True
                    Case sender Is dgvTagged_Movie
                        For Each row As DataGridViewRow In DirectCast(sender, DataGridView).SelectedRows
                            Set_State(row, _currTagState.Movies, Operation.Remove)
                        Next
                    Case sender Is dgvTagged_TVShow
                        For Each row As DataGridViewRow In DirectCast(sender, DataGridView).SelectedRows
                            Set_State(row, _currTagState.TVShows, Operation.Remove)
                        Next
                    Case sender Is dgvTags
                        For Each row As DataGridViewRow In DirectCast(sender, DataGridView).SelectedRows
                            row.Cells("ToDelete").Value = True
                        Next
                End Select
                e.Handled = True
                dgvTags.Refresh()
            ElseIf e.KeyValue = Keys.Enter Then
                Select Case True
                    Case sender Is dgvMovie
                        For Each row As DataGridViewRow In DirectCast(sender, DataGridView).SelectedRows
                            Set_State(row, _currTagState.Movies, Operation.Add)
                        Next
                    Case sender Is dgvTVShow
                        For Each row As DataGridViewRow In DirectCast(sender, DataGridView).SelectedRows
                            Set_State(row, _currTagState.TVShows, Operation.Add)
                        Next
                End Select
                e.Handled = True
                dgvTags.Refresh()
            End If
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTags.SelectionChanged
        If dgvTags.SelectedRows.Count > 0 Then
            _currTagState = DirectCast(dgvTags.SelectedRows(0).DataBoundItem, TagState)
            DataGridView_Fill_Tagged()
        End If
    End Sub

    Private Sub DataGridView_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgvTags.UserAddedRow
        If dgvTags.SelectedRows.Count > 0 Then
            _currTagState = DirectCast(dgvTags.SelectedRows(0).DataBoundItem, TagState)
            DataGridView_Fill_Tagged()
        End If
    End Sub

    Private Sub Process_Tags()
        Dim processCount As Integer = 0
        bwProcessTags.ReportProgress(-2)
        '1. remove no longer wanted tags (except "new tags to delete" because they never existed)
        For Each mTag In _tagList.Where(Function(f) f.ToDelete AndAlso Not f.IsNew)
            'report progress to BackgroundWorker
            bwProcessTags.ReportProgress(processCount, mTag.NewName)
            'movies
            Dim movieIds = From row In mTag.Movies.Rows Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(movieIds, Enums.ContentType.Movie, mTag.Name, String.Empty)
            'tvshows
            Dim tvshowIds = From row In mTag.TVShows.Rows Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(tvshowIds, Enums.ContentType.TVShow, mTag.Name, String.Empty)
            processCount += 1
        Next
        '2. proceed all renamed tags (except removed tags)
        For Each mTag In _tagList.Where(Function(f) f.IsModified AndAlso Not f.ToDelete)
            'report progress to BackgroundWorker
            bwProcessTags.ReportProgress(processCount, mTag.NewName)
            'movies
            Dim movieIds = From row In mTag.Movies.Rows Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(movieIds, Enums.ContentType.Movie, mTag.Name, mTag.NewName)
            'tvshows
            Dim tvshowIds = From row In mTag.TVShows.Rows Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(tvshowIds, Enums.ContentType.TVShow, mTag.Name, mTag.NewName)
            'report progress to BackgroundWorker
            processCount += 1
        Next
        '3. process all other tags (except modified and removed tags)
        For Each mTag In _tagList.Where(Function(f) Not f.IsModified AndAlso Not f.ToDelete)
            'report progress to BackgroundWorker
            bwProcessTags.ReportProgress(processCount, mTag.NewName)
            'movies where the tag has to be removed
            Dim movieIds = From row In mTag.Movies.Rows Where DirectCast(row, DataRow).Item("State").ToString = "ToRemove" Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(movieIds, Enums.ContentType.Movie, mTag.Name, String.Empty)
            'movies where this tag is newly added
            movieIds = From row In mTag.Movies.Rows Where Not DirectCast(row, DataRow).Item("State").ToString = "Unchanged" Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(movieIds, Enums.ContentType.Movie, String.Empty, mTag.NewName)
            'tvshows where the tag has to be removed
            Dim tvshowIds = From row In mTag.TVShows.Rows Where DirectCast(row, DataRow).Item("State").ToString = "ToRemove" Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(tvshowIds, Enums.ContentType.TVShow, mTag.Name, String.Empty)
            'tvshows where this tag is newly added
            tvshowIds = From row In mTag.TVShows.Rows Where Not DirectCast(row, DataRow).Item("State").ToString = "Unchanged" Select Convert.ToInt64(DirectCast(row, DataRow).Item(0))
            Change_Tag(tvshowIds, Enums.ContentType.TVShow, String.Empty, mTag.NewName)
            processCount += 1
        Next
    End Sub

    Private Sub SaveToDatabase()
        Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim processCount As Integer = 0
            bwProcessTags.ReportProgress(-1)
            For Each entry In _toProceed_Movies
                'report progress to BackgroundWorker
                bwProcessTags.ReportProgress(processCount, entry.Movie.Title)
                'save to db
                Master.DB.Save_Movie(entry, True, True, False, True, False)
                processCount += 1
            Next
            For Each entry In _toProceed_TVShows
                'report progress to BackgroundWorker
                bwProcessTags.ReportProgress(processCount, entry.TVShow.Title)
                'save to db
                Master.DB.Save_TVShow(entry, True, True, False, False)
                processCount += 1
            Next
            SQLTransaction.Commit()
        End Using
    End Sub

    Private Sub Set_State(ByVal selection As DataGridViewRow, ByVal target As DataTable, ByVal operation As Operation)
        Dim id As Long = CLng(selection.Cells("id").Value)
        Dim title As String = selection.Cells("Title").Value.ToString
        Dim state As String = String.Empty
        Dim existingRow = target.Rows.Find(id)
        If existingRow IsNot Nothing Then
            Dim currState As String = existingRow.Item("State").ToString
            'row already exists, check state
            Select Case currState
                Case "Unchanged"
                    Select Case operation
                        Case Operation.Add
                            'do nothing
                        Case Operation.Remove
                            existingRow.Item(2) = "ToRemove"
                    End Select
                Case "ToRemove"
                    Select Case operation
                        Case Operation.Add
                            'set back to "unchanged"
                            existingRow.Item(2) = "Unchanged"
                        Case Operation.Remove
                            'do nothing, this state should not even be possible
                    End Select
                Case "New"
                    Select Case operation
                        Case Operation.Add
                            'do nothing
                        Case Operation.Remove
                            'remove it, no changes in database needed
                            target.Rows.Remove(existingRow)
                    End Select
            End Select
        Else
            Select Case operation
                Case Operation.Add
                    target.Rows.Add(id, title, "New")
                Case Operation.Remove
                    'do nothing
            End Select
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Enum Operation
        Add
        Remove
    End Enum

    Private Class TagState
        Inherits Database.TagContainer

#Region "Properties"
        ''' <summary>
        ''' Name has been modified and tag is not new
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsModified As Boolean
            Get
                Return Not IsNew AndAlso Not NewName = Name
            End Get
        End Property
        ''' <summary>
        ''' New tag
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsNew As Boolean
            Get
                Return String.IsNullOrEmpty(Name)
            End Get
        End Property

        Public ReadOnly Property MovieCount As Integer
            Get
                Dim r = From rows In Movies.Rows Where Not DirectCast(rows, DataRow).RowState = DataRowState.Deleted AndAlso Not DirectCast(rows, DataRow).Item("State").ToString = "ToRemove"
                Return r.Count
            End Get
        End Property

        Public Property NewName As String = String.Empty

        Public ReadOnly Property NewNameSpecified As Boolean
            Get
                Return Not String.IsNullOrEmpty(NewName)
            End Get
        End Property
        ''' <summary>
        ''' Has been marked to delete
        ''' </summary>
        ''' <returns></returns>
        Public Property ToDelete As Boolean = False

        Public ReadOnly Property TVShowCount As Integer
            Get
                Dim r = From rows In TVShows.Rows Where Not DirectCast(rows, DataRow).RowState = DataRowState.Deleted AndAlso Not DirectCast(rows, DataRow).Item("State").ToString = "ToRemove"
                Return r.Count
            End Get
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class