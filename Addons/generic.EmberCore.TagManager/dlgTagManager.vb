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
Imports System.Diagnostics
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class dlgTagManager

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'backgroundworker used for commandline scraping in this module
    Friend WithEvents bwLoad As New System.ComponentModel.BackgroundWorker

    'started Ember from commandline?
    Private isCL As Boolean = False

    'datatable which contains all tags in Ember database, loaded at module startup
    Private dtMovieTags As New DataTable
    'list of movies / movie view
    Private lstFilteredMovies As New List(Of Database.DBElement)

    'reflects the current(modified) tag collection which will be saved to database/NFO
    Private globalMovieTags As New List(Of SyncTag)

    'binding for movie datagridview
    Private bsMovies As New BindingSource

    'Not used at moment
    'Friend WithEvents bwLoadMovies As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Constructors"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        SetUp()
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
    Sub SetUp()
        lblTopTitle.Text = Text
        OK_Button.Text = Master.eLang.GetString(19, "Close")
        lblCompiling.Text = Master.eLang.GetString(326, "Loading...")
        lblCanceling.Text = Master.eLang.GetString(370, "Canceling Load...")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
        gbMovies.Text = Master.eLang.GetString(36, "Movies")
        lblTopDetails.Text = Master.eLang.GetString(1374, "Manage XBMC tags")
        gbTags.Text = Master.eLang.GetString(9999, "Tags")
        gbMoviesInTag.Text = Master.eLang.GetString(1375, "Movies in tag")
        rdMoviesAll.Text = Master.eLang.GetString(1461, "All movies")
        rdMoviesFiltered.Text = Master.eLang.GetString(1462, "Filtered movies")
        rdMoviesSelected.Text = Master.eLang.GetString(1463, "Selected movies")
        gbMoviesFilter.Text = Master.eLang.GetString(330, "Filter")

        'load current movielist-view/selection
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                lstFilteredMovies.Add(DBElement)
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
        globalMovieTags.Clear()
        dtMovieTags.Clear()
        pnlMain.Enabled = False

        'load existing tags from database into datatable
        Master.DB.FillDataTable(dtMovieTags, String.Concat("SELECT * FROM tag ",
                                                             "ORDER BY strTag COLLATE NOCASE;"))

        'fill movie datagridview
        If dgvMovies.Rows.Count = 0 Then
            dgvMovies.SuspendLayout()
            Me.bsMovies.DataSource = Nothing
            Me.dgvMovies.DataSource = Nothing
            If lstFilteredMovies.Count > 0 Then
                '  If Me.dtMovies.Rows.Count > 0 Then
                With Me
                    .bsMovies.DataSource = .lstFilteredMovies
                    .dgvMovies.DataSource = .bsMovies
                    For i As Integer = 0 To .dgvMovies.Columns.Count - 1
                        .dgvMovies.Columns(i).Visible = False
                    Next
                    .dgvMovies.Columns("ListTitle").Visible = True
                    .dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                    .dgvMovies.Columns("ListTitle").ReadOnly = True
                    .dgvMovies.Columns("ListTitle").MinimumWidth = 83
                    .dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                    .dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                    .dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")

                    .dgvMovies.Columns("ID").ValueType = GetType(Int64)
                End With
            End If
            dgvMovies.ResumeLayout()
        End If

        'fill listbox of tags
        If lbTags.Items.Count = 0 Then
            For Each sRow As DataRow In dtMovieTags.Rows
                If Not String.IsNullOrEmpty(sRow.Item("strTag").ToString) Then
                    Dim tmpnewTag As New SyncTag
                    tmpnewTag.ID = CInt(sRow.Item("idTag"))
                    tmpnewTag.Name = CStr(sRow.Item("strTag"))
                    Dim tmpTag = Master.DB.Load_Tag_Movie(CInt(sRow.Item("idTag")))
                    tmpnewTag.Movies = tmpTag.Movies
                    globalMovieTags.Add(tmpnewTag)
                    lbTags.Items.Add(sRow.Item("strTag").ToString)
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

        For Each movielist In globalMovieTags
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
        For Each tmptag In globalMovieTags
            If tmptag.IsDeleted = False Then
                lbTags.Items.Add(tmptag.Name)
            End If
        Next
        btnEditTag.Enabled = False
        btnRemoveTag.Enabled = False
        btnAddMovie.Enabled = False
        btnRemoveMovie.Enabled = False
        txtEditTag.Text = ""
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
                For Each _tag In globalMovieTags
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
                    For Each _tag In globalMovieTags
                        If _tag.Name = lbTags.SelectedItem.ToString Then
                            If Not _tag.Movies Is Nothing Then
                                Dim alreadyintag As Boolean = False
                                For Each movie In _tag.Movies
                                    If movie.Movie.Title = tmpMovie.Movie.Title AndAlso movie.Movie.IMDB = tmpMovie.Movie.IMDB Then
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
                                logger.Info("[" & _tag.Name & "] " & tmpMovie.Movie.Title & " is null! Error when trying to add movie to tag!")
                            End If
                        End If
                    Next

                End If
            Next
            dgvMovies.ClearSelection()
            Me.dgvMovies.CurrentCell = Nothing
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
            For Each movieinlist In globalMovieTags
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
                logger.Info("[" & lbTags.SelectedItem.ToString & "] No tag selected!")
                lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
                btnEditTag.Enabled = False
                btnRemoveTag.Enabled = False
                btnAddMovie.Enabled = False
                btnRemoveMovie.Enabled = False
                txtEditTag.Text = ""
            End If

            ' no tag selected, disable remove/edit tag buttons, reset label
        Else
            lblCurrentTag.Text = Master.eLang.GetString(368, "None Selected")
            btnEditTag.Enabled = False
            btnRemoveTag.Enabled = False
            btnAddMovie.Enabled = False
            btnRemoveMovie.Enabled = False
            txtEditTag.Text = ""
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
            For Each _tag In globalMovieTags
                If _tag.Name = strtag Then
                    _tag.IsDeleted = True
                    If _tag.IsNew = True Then
                        globalMovieTags.Remove(_tag)
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
                For Each _tag In globalMovieTags
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
            globalMovieTags.Add(newTag)
            'since globaltaglist was updated, we need to load globaltaglist again to reflect changes
            LoadLists()
        Else
            logger.Info("New tag could not be created!")
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
        For Each _list In globalMovieTags
            If _list.Name = listname Then
                Return Nothing
            End If
        Next
        Dim newtag As New SyncTag
        newtag.Clear()
        newtag.IsNew = True
        newtag.Name = listname
        Return newtag
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
        For Each list In globalMovieTags
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
                    Master.DB.Delete_Tag(tmpDBMovieTag.ID, 1, False)
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
                tmovie.Movie.Title = tmovie.Movie.Title.Replace("_TODELETE", "")
                Master.DB.Save_Movie(tmovie, True, True, False, False)
            Else
                tmovie.Movie.Tags.Add(tag.Name)
                Master.DB.Save_Movie(tmovie, True, True, False, False)
            End If
        Next
    End Sub

    Private Sub tbpglobalMovieTags_Enter(sender As Object, e As EventArgs)
        Activate()
    End Sub

#End Region 'Methods


    Private Sub rdMoviesFiltered_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesFiltered.CheckedChanged
        If rdMoviesFiltered.Checked Then
            lstFilteredMovies.Clear()
            'load current movielist-view/selection
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.Rows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    lstFilteredMovies.Add(DBElement)
                End If
            Next
            dgvMovies.DataSource = Nothing
            dgvMovies.Rows.Clear()
            RefreshModule()
        End If
    End Sub

    Private Sub rdMoviesSelected_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesSelected.CheckedChanged
        If rdMoviesSelected.Checked Then
            lstFilteredMovies.Clear()
            'load current movielist-view/selection
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    lstFilteredMovies.Add(DBElement)
                End If
            Next
            dgvMovies.DataSource = Nothing
            dgvMovies.Rows.Clear()
            RefreshModule()
        End If
    End Sub

    Private Sub rdMoviesAll_CheckedChanged(sender As Object, e As EventArgs) Handles rdMoviesAll.CheckedChanged
        If rdMoviesAll.Checked Then
            lstFilteredMovies.Clear()
            'load current movielist-view/selection
            Dim dtmovies As New DataTable
            Master.DB.FillDataTable(dtmovies, String.Concat("SELECT * FROM movielist ",
                                                                "ORDER BY ListTitle COLLATE NOCASE;"))
            For Each sRow As DataRow In dtmovies.Rows
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow("idMovie")))
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    lstFilteredMovies.Add(DBElement)
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

#Region "Fields"

    Private _idTag As Long
    Private _strTag As String
    Private _newTag As Boolean
    Private _modifiedTag As Boolean
    Private _deletedTag As Boolean
    Private _movies As New List(Of Database.DBElement)

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region

#Region "Properties"

    Public Property Movies() As List(Of Database.DBElement)
        Get
            Return _movies
        End Get
        Set(ByVal value As List(Of Database.DBElement))
            _movies = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _strTag
        End Get
        Set(ByVal value As String)
            _strTag = value
        End Set
    End Property

    Public Property ID() As Long
        Get
            Return _idTag
        End Get
        Set(ByVal value As Long)
            _idTag = value
        End Set
    End Property

    Public Property IsNew() As Boolean
        Get
            Return _newTag
        End Get
        Set(ByVal value As Boolean)
            _newTag = value
        End Set
    End Property

    Public Property IsModified() As Boolean
        Get
            Return _modifiedTag
        End Get
        Set(ByVal value As Boolean)
            _modifiedTag = value
        End Set
    End Property

    Public Property IsDeleted() As Boolean
        Get
            Return _deletedTag
        End Get
        Set(ByVal value As Boolean)
            _deletedTag = value
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Clear()
        _movies = New List(Of Database.DBElement)
        _idTag = -1
        _strTag = String.Empty
        _newTag = False
        _deletedTag = False
        _modifiedTag = False
    End Sub

    Public Function CompareTo(ByVal other As SyncTag) As Integer Implements IComparable(Of SyncTag).CompareTo
        Return (ID).CompareTo(other.ID)
    End Function

#End Region 'Methods

End Class

#End Region

