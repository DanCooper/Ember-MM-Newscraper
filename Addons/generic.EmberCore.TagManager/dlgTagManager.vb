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
Imports System.Windows.Forms

Public Class dlgTagManager

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()
    ''' <summary>
    ''' started Ember from commandline?
    ''' </summary>
    Private _IsCommandLine As Boolean = False
    ''' <summary>
    ''' datatable which contains all tags In Ember database, loaded at Module startup
    ''' </summary>
    Private _DTMovieTags As New DataTable
    ''' <summary>
    ''' list of movies / movie view
    ''' </summary>
    Private _LstFilteredMovies As New List(Of Database.DBElement)
    ''' <summary>
    ''' reflects the current(modified) tag collection which will be saved to database/NFO
    ''' </summary>
    Private _GlobalMovieTags As New List(Of SyncTag)
    ''' <summary>
    ''' binding for movie datagridview
    ''' </summary>
    Private _BSsMovies As New BindingSource

#End Region 'Fields

#Region "Constructors"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
        Setup()
    End Sub

#End Region 'Constructors

#Region "Methods"

    ''' <summary>
    ''' Actions on module startup
    ''' </summary>
    ''' <param name="sender">startup of module</param>
    ''' <remarks>
    ''' - set labels/translation text
    ''' - set settings, check if necessary data is available
    ''' - load existing movies in background
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub Setup()
        lblTopTitle.Text = Text
        OK_Button.Text = Master.eLang.Close
        lblCompiling.Text = Master.eLang.GetString(326, "Loading...")
        lblCanceling.Text = Master.eLang.GetString(370, "Canceling Load...")
        btnCancel.Text = Master.eLang.Cancel
        lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
        gbMovies.Text = Master.eLang.GetString(36, "Movies")
        lblTopDetails.Text = Master.eLang.GetString(1374, "Manage tags")
        gbTags.Text = Master.eLang.GetString(9999, "Tags")
        gbMoviesInTag.Text = Master.eLang.GetString(1375, "Movies in tag")
        rdMoviesAll.Text = Master.eLang.GetString(1461, "All movies")
        rdMoviesFiltered.Text = Master.eLang.GetString(1462, "Filtered movies")
        rdMoviesSelected.Text = Master.eLang.GetString(1463, "Selected movies")
        gbMoviesFilter.Text = Master.eLang.GetString(330, "Filter")

        'load current movielist-view/selection
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                _LstFilteredMovies.Add(DBElement)
            End If
        Next
        RefreshModule()
    End Sub


    ''' <summary>
    ''' Actions on module close event
    ''' </summary>
    ''' <param name="sender">Close button</param>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
    End Sub
    ''' <summary>
    ''' Fill listboxes/datagrid (tags and movies from database) and display in listbox
    ''' </summary>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub RefreshModule()
        'clear globals, set back controls
        lbMoviesInTag.Items.Clear()
        lbTags.Items.Clear()
        _GlobalMovieTags.Clear()
        _DTMovieTags.Clear()
        pnlMain.Enabled = False

        'load existing tags from database into datatable
        _DTMovieTags = Master.DB.GetTags

        'fill movie datagridview
        If dgvMovies.Rows.Count = 0 Then
            dgvMovies.SuspendLayout()
            _BSsMovies.DataSource = Nothing
            dgvMovies.DataSource = Nothing
            If _LstFilteredMovies.Count > 0 Then
                'If Me.dtMovies.Rows.Count > 0 Then
                _BSsMovies.DataSource = _LstFilteredMovies
                dgvMovies.DataSource = _BSsMovies
                For i As Integer = 0 To dgvMovies.Columns.Count - 1
                    dgvMovies.Columns(i).Visible = False
                Next
                dgvMovies.Columns("ListTitle").Visible = True
                dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                dgvMovies.Columns("ListTitle").ReadOnly = True
                dgvMovies.Columns("ListTitle").MinimumWidth = 83
                dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")

                dgvMovies.Columns("ID").ValueType = GetType(Long)
            End If
            dgvMovies.ResumeLayout()
        End If

        'fill listbox of tags
        If lbTags.Items.Count = 0 Then
            For Each sRow As DataRow In _DTMovieTags.Rows
                If Not String.IsNullOrEmpty(sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Name)).ToString) Then
                    Dim tmpnewTag As New SyncTag
                    tmpnewTag.ID = CInt(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.tag)))
                    tmpnewTag.Name = CStr(sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Name)))
                    Dim tmpTag = Master.DB.Load_Tag_Movie(CLng(sRow.Item(Database.Helpers.GetMainIdName(Database.TableName.tag))))
                    tmpnewTag.Movies = tmpTag.Movies
                    _GlobalMovieTags.Add(tmpnewTag)
                    lbTags.Items.Add(sRow.Item(Database.Helpers.GetColumnName(Database.ColumnName.Name)).ToString)
                End If
            Next
            'select first item in listbox
            If lbTags.Items.Count > 0 Then
                lbTags.SelectedIndex = 0
            End If
        End If

        'enable controls again
        dgvMovies.Enabled = True
        pnlMain.Enabled = True
        lbTags.Enabled = True
        btnNewTag.Enabled = True
        lbMoviesInTag.Enabled = True
    End Sub
    ''' <summary>
    ''' Add corresponding movies of selected tag to movielistbox 
    ''' </summary>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub LoadMoviesOfSelectedTag()
        lbMoviesInTag.SuspendLayout()
        lbMoviesInTag.Items.Clear()

        For Each movielist In _GlobalMovieTags
            If movielist.Name = lbTags.SelectedItem.ToString Then
                For Each tMovie In movielist.Movies
                    lbMoviesInTag.Items.Add(tMovie.Movie.Title)
                Next
                If lbMoviesInTag.Items.Count > 0 Then
                    btnRemoveMovie.Enabled = True
                End If
                Exit For
            End If
        Next

        lbMoviesInTag.ResumeLayout()
    End Sub
    ''' <summary>
    ''' Add all tags from globaltaglist to listbox
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub LoadLists()
        lbTags.SuspendLayout()
        'first clear all listboxes before adding information again
        lbTags.Items.Clear()
        lbMoviesInTag.Items.Clear()
        lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
        'add tag to listbox (if its not marked for delete!)
        For Each tmptag In _GlobalMovieTags
            If tmptag.IsDeleted = False Then
                lbTags.Items.Add(tmptag.Name)
            End If
        Next
        btnEditTag.Enabled = False
        btnRemoveTag.Enabled = False
        btnAddMovie.Enabled = False
        btnRemoveMovie.Enabled = False
        txtEditTag.Text = String.Empty
        lbTags.ResumeLayout()
    End Sub
    ''' <summary>
    ''' Remove selected movie(s) from selected tag
    ''' </summary>
    ''' <param name="sender">"Remove Movie"-Button in Form</param>
    ''' <remarks>
    '''  'Removing a movie from a tag means updating "globalMovieTags" - then refresh view of listbox - multiselect in listbox is supported
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnRemoveMovie_Click(sender As Object, e As EventArgs) Handles btnRemoveMovie.Click
        If lbMoviesInTag.SelectedItems.Count > 0 Then
            For Each selectedmovie In lbMoviesInTag.SelectedItems
                'update globaltaglist
                For Each _tag In _GlobalMovieTags
                    If _tag.Name = lbTags.SelectedItem.ToString Then
                        For Each movie In _tag.Movies
                            If movie.Movie.Title = selectedmovie.ToString Then
                                _tag.IsModified = True
                                _tag.Movies.Remove(movie)
                            End If
                        Next
                    End If
                Next
            Next
            LoadMoviesOfSelectedTag()
        End If
    End Sub
    ''' <summary>
    ''' Add selected movie to selected tag
    ''' </summary>
    ''' <param name="sender">"Add Movie"-Button in Form</param>
    ''' <remarks>
    '''  'Adding a movie to a tag means updating "globalMovieTags"- then refresh view of listbox 
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnAddMovie_Click(sender As Object, e As EventArgs) Handles btnAddMovie.Click
        If dgvMovies.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("ID").Value))


                If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                    'add new movie to tag in globaltaglist
                    For Each _tag In _GlobalMovieTags
                        If _tag.Name = lbTags.SelectedItem.ToString Then
                            If Not _tag.Movies Is Nothing Then
                                Dim alreadyintag As Boolean = False
                                For Each movie In _tag.Movies
                                    If movie.Movie.Title = tmpMovie.Movie.Title AndAlso movie.Movie.UniqueIDs.IMDbId = tmpMovie.Movie.UniqueIDs.IMDbId Then
                                        alreadyintag = True
                                    End If
                                Next
                                If alreadyintag = False Then
                                    'valid movie!
                                    _tag.Movies.Add(tmpMovie)
                                    _tag.IsModified = True
                                    btnglobalTagsSync.Enabled = True
                                End If
                                Exit For
                            Else
                                _Logger.Info("[" & _tag.Name & "] " & tmpMovie.Movie.Title & " is null! Error when trying to add movie to tag!")
                            End If
                        End If
                    Next

                End If
            Next
            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            LoadMoviesOfSelectedTag()
        End If
    End Sub
    ''' <summary>
    ''' Add corresponding movies of selected tag to movielistbox 
    ''' </summary>
    ''' <param name="sender">lbTags IndexChanged Event</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub lbTags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbTags.SelectedIndexChanged
        'clear movieintag-listbox since we select another tag
        lbMoviesInTag.Items.Clear()

        'check if tag was selected
        If lbTags.SelectedItems.Count > 0 Then
            'display tagtitle in label
            lblCurrentTag.Text = lbTags.SelectedItem.ToString
            Dim foundlist As Boolean = False
            'search selected tag in globaltags
            For Each movieinlist In _GlobalMovieTags
                If movieinlist.Name = lbTags.SelectedItem.ToString Then

                    'add all movies from tag into listbox
                    LoadMoviesOfSelectedTag()

                    'Enable remove/edit list buttons
                    btnEditTag.Enabled = True
                    btnRemoveTag.Enabled = True
                    btnAddMovie.Enabled = True
                    txtEditTag.Enabled = True
                    txtEditTag.Text = lbTags.SelectedItem.ToString
                    foundlist = True
                    Exit For
                End If
            Next

            If foundlist = False Then
                _Logger.Info("[" & lbTags.SelectedItem.ToString & "] No tag selected!")
                lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
                btnEditTag.Enabled = False
                btnRemoveTag.Enabled = False
                btnAddMovie.Enabled = False
                btnRemoveMovie.Enabled = False
                txtEditTag.Text = String.Empty
            End If

            ' no tag selected, disable remove/edit tag buttons, reset label
        Else
            lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
            btnEditTag.Enabled = False
            btnRemoveTag.Enabled = False
            btnAddMovie.Enabled = False
            btnRemoveMovie.Enabled = False
            txtEditTag.Text = String.Empty
        End If
    End Sub
    ''' <summary>
    ''' Delete selected tag
    ''' </summary>
    ''' <param name="sender">"Remove Tag" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnRemoveTag_Click(sender As Object, e As EventArgs) Handles btnRemoveTag.Click
        If lbTags.SelectedItems.Count > 0 Then
            Dim strtag As String = lbTags.SelectedItem.ToString
            For Each _tag In _GlobalMovieTags
                If _tag.Name = strtag Then
                    _tag.IsDeleted = True
                    If _tag.IsNew = True Then
                        _GlobalMovieTags.Remove(_tag)
                    End If
                    btnglobalTagsSync.Enabled = True
                    Exit For
                End If
            Next
            'since globaltaglist was updated, we need to load globaltaglist again to reflect changes
            LoadLists()
        End If
    End Sub
    ''' <summary>
    ''' Change name of existing/selected tag
    ''' </summary>
    ''' <param name="sender">"Edit Tag" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnEditTag_Click(sender As Object, e As EventArgs) Handles btnEditTag.Click
        'check if tag is selected (we need one to edit)
        If lbTags.SelectedItems.Count > 0 Then
            'currenttagname
            Dim strtag As String = lbTags.SelectedItem.ToString
            'newtagname (from textbox)
            Dim newtagname As String = txtEditTag.Text
            'only update if both names(old and new) are available, also don't edit if newname is already a used tagname
            If Not String.IsNullOrEmpty(strtag) AndAlso Not String.IsNullOrEmpty(newtagname) AndAlso Not lbTags.Items.Contains(newtagname) Then
                'update listname in globallist
                For Each _tag In _GlobalMovieTags
                    If _tag.Name = strtag Then
                        _tag.Name = newtagname
                        _tag.IsModified = True
                        btnglobalTagsSync.Enabled = True
                        Exit For
                    End If
                Next
                'since globaltaglist was updated, we need to load globaltaglist again to reflect changes
                LoadLists()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Add empty tag
    ''' </summary>
    ''' <param name="sender">"New Tag" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnNewTag_Click(sender As Object, e As EventArgs) Handles btnNewTag.Click
        Dim newTag As New SyncTag
        'create new tag
        If Not String.IsNullOrEmpty(txtEditTag.Text) Then
            newTag = CreateNewTag(txtEditTag.Text)
        Else
            newTag = CreateNewTag("NewTag")
        End If
        If Not newTag Is Nothing Then
            btnglobalTagsSync.Enabled = True
            'add created tag to existing globaltaglist
            _GlobalMovieTags.Add(newTag)
            'since globaltaglist was updated, we need to load globaltaglist again to reflect changes
            LoadLists()
        Else
            _Logger.Info("New tag could not be created!")
        End If
    End Sub
    ''' <summary>
    ''' Create empty tag object
    ''' </summary>
    ''' <returns>New tag object with no items(movies) or nothing if tag could not be created</returns>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    Private Function CreateNewTag(ByVal listname As String) As SyncTag
        'check if tag already exists
        For Each _list In _GlobalMovieTags
            If _list.Name = listname Then
                Return Nothing
            End If
        Next

        Return New SyncTag With {
            .IsNew = True,
            .Name = listname
        }
    End Function
    ''' <summary>
    ''' Save tag state to Ember database/Nfo of movies
    ''' </summary>
    ''' <param name="sender">"Save tags to database/Nfo"-Button in Form</param>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' </remarks>
    ''' 
    Private Sub btnglobalMovieTagsSync_Click(sender As Object, e As EventArgs) Handles btnglobalTagsSync.Click
        'tag object will be written to Ember database/NFO (if it was edited)
        For Each list In _GlobalMovieTags
            'Check if information is stored...
            If Not list.Name Is Nothing Then

                'Step 1: create new DBTag object to store current tag in
                Dim tmpDBMovieTag As New Structures.DBMovieTag
                If list.IsNew = True Then
                    'if tag is new and doesn't exist in Ember, create new one with basic information!
                    tmpDBMovieTag.ID = -1
                    tmpDBMovieTag.Title = list.Name
                    tmpDBMovieTag.Movies = list.Movies
                Else
                    'tag already in DB, just edit it! 
                    tmpDBMovieTag.ID = CInt(list.ID)
                    tmpDBMovieTag.Title = list.Name
                    tmpDBMovieTag.Movies = list.Movies
                End If

                'Step 2: save tag to DB
                If list.IsNew = True Then
                    'save tag to database
                    Master.DB.Save_Tag_Movie(tmpDBMovieTag, True, False, True, True)
                ElseIf list.IsDeleted = True Then
                    'remove tag from database/nfo
                    Master.DB.Delete_Tag(tmpDBMovieTag.ID, False)
                ElseIf list.IsModified = True Then
                    'save tag to database
                    Master.DB.Save_Tag_Movie(tmpDBMovieTag, False, False, True, True)
                End If
            Else
                'no name !
            End If
        Next
        RefreshModule()
        MessageBox.Show(Master.eLang.GetString(1376, "Saved changes") & "!", Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
    End Sub
    ''' <summary>
    ''' Save changes in tag to movie nfo
    ''' </summary>
    ''' <param name="tag">The tag which contains movies to check</param>
    ''' <remarks>
    ''' 2015/03/01 Cocotus - First implementation
    ''' Basically we need to figure out the movies which don't have specific tag anymore - in this module those movies are marked as "TODELETE" in title
    ''' </remarks>
    Private Sub SyncTagToMovies(ByVal tag As SyncTag)
        For Each tmovie As Database.DBElement In tag.Movies
            If tmovie.Movie.Title.EndsWith("_TODELETE") Then
                tmovie.Movie.Tags.Remove(tag.Name)
                tmovie.Movie.Title = tmovie.Movie.Title.Replace("_TODELETE", String.Empty)
                Master.DB.Save_Movie(tmovie, True, True, False, True, False)
            Else
                tmovie.Movie.Tags.Add(tag.Name)
                Master.DB.Save_Movie(tmovie, True, True, False, True, False)
            End If
        Next
    End Sub

    Private Sub tbpglobalMovieTags_Enter(sender As Object, e As EventArgs)
        Activate()
    End Sub

#End Region 'Methods


    Private Sub rdMoviesFiltered_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesFiltered.CheckedChanged
        If rdMoviesFiltered.Checked Then
            _LstFilteredMovies.Clear()
            'load current movielist-view/selection
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    _LstFilteredMovies.Add(DBElement)
                End If
            Next
            dgvMovies.DataSource = Nothing
            dgvMovies.Rows.Clear()
            RefreshModule()
        End If
    End Sub

    Private Sub rdMoviesSelected_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesSelected.CheckedChanged
        If rdMoviesSelected.Checked Then
            _LstFilteredMovies.Clear()
            'load current movielist-view/selection
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(Database.Helpers.GetMainIdName(Database.TableName.movie)).Value))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    _LstFilteredMovies.Add(DBElement)
                End If
            Next
            dgvMovies.DataSource = Nothing
            dgvMovies.Rows.Clear()
            RefreshModule()
        End If
    End Sub

    Private Sub rdMoviesAll_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesAll.CheckedChanged
        If rdMoviesAll.Checked Then
            _LstFilteredMovies.Clear()
            'load current movielist-view/selection
            Dim dtmovies = Master.DB.GetMovies
            For Each sRow As DataRow In dtmovies.Rows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow(Database.Helpers.GetMainIdName(Database.TableName.movie))))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    _LstFilteredMovies.Add(DBElement)
                End If
            Next
            dgvMovies.DataSource = Nothing
            dgvMovies.Rows.Clear()
            RefreshModule()
        End If
    End Sub

End Class

#Region "Nested Types"

Friend Class SyncTag
    Implements IComparable(Of SyncTag)

#Region "Properties"

    Public Property Movies() As List(Of Database.DBElement) = New List(Of Database.DBElement)

    Public Property Name() As String = String.Empty

    Public Property ID() As Long = -1

    Public Property IsNew() As Boolean = False

    Public Property IsModified() As Boolean = False

    Public Property IsDeleted() As Boolean = False

#End Region

#Region "Methods"

    Public Function CompareTo(ByVal other As SyncTag) As Integer Implements IComparable(Of SyncTag).CompareTo
        Return (ID).CompareTo(other.ID)
    End Function

#End Region 'Methods

End Class

#End Region 'Nested Types