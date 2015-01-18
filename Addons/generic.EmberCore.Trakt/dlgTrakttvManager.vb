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
Imports Trakttv
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class dlgTrakttvManager

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'backgroundworker used for commandline scraping in this module
    Friend WithEvents bwLoad As New System.ComponentModel.BackgroundWorker

    'started Ember from commandline?
    Private isCL As Boolean = False

    'datatable which contains all tags in Ember database
    Private dtMovieTags As New DataTable
    'datatable which contains all movies in Ember database
    Private dtMovies As New DataTable

    'trakt.tv authentification data, user settings
    Private traktUser As String = ""
    Private traktPassword As String = ""
    Private traktToken As String = ""

    'Tab: trakt.tv Sync Playcount variables
    Private myWatchedMovies As New Dictionary(Of String, KeyValuePair(Of String, Integer))
    ' Private myWatchedEpisodes As New Dictionary(Of String, KeyValuePair(Of String, List(Of TraktAPI.Model.TraktSyncEpisodeWatched)))
    'Helper: Saving 3 values in Dictionary style: TVDB, SeasonNumber|Episodenumber
    Private myWatchedEpisodes As New Dictionary(Of String, List(Of KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode))))

    'Tab: trakt.tv Sync Lists/Tags variables
    'reflects the current list collection which will be synced to trakt.tv
    Private traktLists As New List(Of TraktAPI.Model.TraktList)
    'collection fo current tags in Ember database - will be modifified/updated during runtime of this module and then saved back to database
    Private alDBTags As New List(Of String)

    'Tab: trakt.tv Sync Watchlist variables
    'Helper: Saving 3 values in Dictionary style: TVDB, List of SeasonNumber|Episodenumber
    Private myWatchlistEpisodes As New Dictionary(Of String, List(Of KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode))))
    Private myWatchlistMovies As New List(Of TraktAPI.Model.TraktMovieWatchList)

    'Tab: trakt.tv Listviewer variables
    'fetched listnames
    Private traktpopularlistTitle As New List(Of String)
    'fetched listURLs
    Private traktpopularlistURL As New List(Of String)
    'scraped userlist from trakt.tv
    Private favoriteList As New TraktAPI.Model.TraktList
    'binding for movie datagridview
    Private bsMovies As New BindingSource

    'Not used at moment
    'Friend WithEvents bwLoadMovies As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Constructors"
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
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
    ''' - set settings, check if necessary data is avalaible
    ''' - load existing movies in background
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Sub SetUp()
        Try
            'set trakt.tv authentification data on start
            traktUser = clsAdvancedSettings.GetSetting("Username", "")
            traktPassword = clsAdvancedSettings.GetSetting("Password", "")
            'if there's missing data we can't use any trakt.tv API calls -> block GUI
            If traktUser = "" OrElse traktPassword = "" Then
                tbTrakt.Enabled = False
            End If

            Me.lblTopTitle.Text = Me.Text
            Me.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
            Me.OK_Button.Text = Master.eLang.GetString(19, "Close")
            Me.tbptraktPlaycount.Text = Master.eLang.GetString(1303, "Sync Playcount")
            Me.tbptraktListViewer.Text = Master.eLang.GetString(1304, "List Viewer")
            Me.tbptraktListsSync.Text = Master.eLang.GetString(1305, "Sync Lists/Tags")
            Me.lblCompiling.Text = Master.eLang.GetString(326, "Loading...")
            Me.lblCanceling.Text = Master.eLang.GetString(370, "Canceling Load...")
            Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
            Me.lblTopDetails.Text = Master.eLang.GetString(1306, "Sync lists and playcount with your trakt.tv account.")


            'Tab: Sync Playcount
            gbtraktPlaycount.Text = Master.eLang.GetString(1303, "Sync Playcount")
            lbltraktPlaycountstate.Visible = False
            prgtraktPlaycount.Value = 0
            prgtraktPlaycount.Maximum = myWatchedMovies.Count
            prgtraktPlaycount.Minimum = 0
            prgtraktPlaycount.Step = 1
            btntraktPlaycountGetMovies.Text = Master.eLang.GetString(779, "Get watched movies")
            btntraktPlaycountSyncLibrary.Text = Master.eLang.GetString(780, "Save playcount to database/Nfo")
            btntraktPlaycountGetSeries.Text = Master.eLang.GetString(781, "Get watched episodes")
            dgvtraktPlaycount.DataSource = Nothing
            dgvtraktPlaycount.Rows.Clear()
            coltraktPlaycountTitle.Name = Master.eLang.GetString(21, "Title")
            coltraktPlaycountPlayed.Name = Master.eLang.GetString(981, "Watched")

            'Tab: Sync Lists/Tags
            Me.lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
            gbtraktListsGET.Text = Master.eLang.GetString(1307, "Load personal lists")
            gbtraktListsSYNC.Text = Master.eLang.GetString(1308, "Save personal lists")
            gbtraktLists.Text = Master.eLang.GetString(1309, "Personal trakt.tv Lists")
            gbtraktListsDetails.Text = Master.eLang.GetString(26, "Details")
            gbtraktListsMoviesInLists.Text = Master.eLang.GetString(1310, "Movies In List")
            gbtraktListsMovies.Text = Master.eLang.GetString(36, "Movies")
            btntraktListsGetDatabase.Text = Master.eLang.GetString(1311, "Add list to trakt.tv")
            btntraktListsGetPersonal.Text = Master.eLang.GetString(1312, "Load trakt.tv lists")
            btntraktListsSyncTrakt.Text = Master.eLang.GetString(1313, "Sync to trakt.tv")
            btntraktListsSyncLibrary.Text = Master.eLang.GetString(1314, "Save tags to database/Nfo")
            btntraktListsDetailsUpdate.Text = Master.eLang.GetString(1315, "Update listdetails")
            lbltraktListsDetailsName.Text = Master.eLang.GetString(232, "Name")
            lbltraktListsDetailsDescription.Text = Master.eLang.GetString(979, "Description")
            chkltraktListsDetailsComments.Text = Master.eLang.GetString(1316, "Allow Comments")
            lbltraktListsDetailsPrivacy.Text = Master.eLang.GetString(1317, "Privacy Level")
            chktraktListsDetailsNumbers.Text = Master.eLang.GetString(1318, "Show Numbers")
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1319, "Public"))
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1320, "Friends"))
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1321, "Private"))

            'Tab: List viewer
            lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ":"
            gbtraktListsViewer.Text = Master.eLang.GetString(1304, "List Viewer")
            lbltraktListsurlhelp.Text = Master.eLang.GetString(1322, "trakt.tv list:")
            lbltraktListsurl.Text = Master.eLang.GetString(1323, "URL:")
            btntraktListsfetch.Text = Master.eLang.GetString(1324, "Fetch lists")
            btntraktListsload.Text = Master.eLang.GetString(1325, "Load list")
            lbltraktListsdescription.Text = Master.eLang.GetString(979, "Description")
            chktraktListsCompare.Text = Master.eLang.GetString(1326, "only show unknown movies")
            btntraktListsSaveList.Text = Master.eLang.GetString(1327, "Export complete list")
            btntraktListsSaveListCompare.Text = Master.eLang.GetString(1328, "Export unknown movies")
            coltraktListTitle.Name = Master.eLang.GetString(21, "Title")
            coltraktListYear.Name = Master.eLang.GetString(278, "Year")
            coltraktListGenres.Name = Master.eLang.GetString(20, "Genres")
            coltraktListTrailer.Name = Master.eLang.GetString(151, "Trailer")
            coltraktListRating.Name = Master.eLang.GetString(400, "Rating")
            coltraktListIMDB.Name = Master.eLang.GetString(1323, "URL")

            'Tab: Sync Watchlist 
            gbtraktWatchlist.Text = Master.eLang.GetString(1337, "Sync Watchlist")
            gbtraktWatchlistExpert.Text = Master.eLang.GetString(1175, "Optional Settings")
            lbltraktWatchliststate.Visible = False
            prgtraktWatchlist.Value = 0
            prgtraktWatchlist.Maximum = myWatchedMovies.Count
            prgtraktWatchlist.Minimum = 0
            prgtraktWatchlist.Step = 1
            btntraktWatchlistGetMovies.Text = Master.eLang.GetString(1338, "Get movies from trakt.tv watchlist")
            btntraktWatchlistSyncLibrary.Text = Master.eLang.GetString(1340, "Remove watched movies from trakt.tv watchlist")
            btntraktWatchlistGetSeries.Text = Master.eLang.GetString(1339, "Get episodes from trakt.tv watchlist")
            btntraktWatchlistClean.Text = Master.eLang.GetString(1341, "Clear trakt.tv watchlist")
            btntraktWatchlistSendEmberUnwatched.Text = Master.eLang.GetString(1342, "Send unwatched movies to trakt.tv watchlist")
            dgvtraktWatchlist.DataSource = Nothing
            dgvtraktWatchlist.Rows.Clear()
            coltraktWatchlistTitle.Name = Master.eLang.GetString(21, "Title")
            coltraktWatchlistYear.Name = Master.eLang.GetString(278, "Year")
            coltraktWatchlistListedAt.Name = Master.eLang.GetString(601, "Date Added")
            coltraktWatchlistIMDB.Name = Master.eLang.GetString(1323, "URL")
            'load existing movies from database into datatable
            Master.DB.FillDataTable(Me.dtMovies, "SELECT * FROM movies ORDER BY ListTitle COLLATE NOCASE;")
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try 
    End Sub

    ''' <summary>
    ''' Actions on module close event
    ''' </summary>
    ''' <param name="sender">Close button</param>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    '''  Trakt-Login process for using v2 API (Token based authentification) 
    ''' </summary>
    ''' <param name="_traktuser">trakt.tv Username</param>
    ''' <param name="_traktuser">trakt.tv Password</param>
    ''' <param name="_trakttoken">trakt.tv Token, may be empty (then it will be generated)</param>
    ''' <returns>(new) trakt.tv Token</returns>
    ''' <remarks>
    ''' 2015/01/17 Cocotus - First implementation of new V2 Authentification process for trakt.tv API
    ''' </remarks>
    Private Function LoginToTrakt(ByVal _traktuser As String, ByVal _traktpassword As String, ByVal _trakttoken As String) As String
        Try
            ' Use Trakttv wrapper
            Dim account As New TraktAPI.Model.TraktAuthentication
            account.Username = _traktuser
            account.Password = _traktpassword
            TraktSettings.Password = _traktpassword
            TraktSettings.Username = _traktuser

            If String.IsNullOrEmpty(_trakttoken) Then
                Dim response = Trakttv.TraktMethods.LoginToAccount(account)
                _trakttoken = response.Token
            Else
                TraktSettings.Token = _trakttoken
            End If
            Return _trakttoken
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return ""
        End Try
    End Function

#Region "Trakt.tv Sync Watchlist"
    ''' <summary>
    ''' GET movies of users watchlist and display in Datagridview
    ''' </summary>
    ''' <param name="sender">"Get movies"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/17 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktWatchlistGetMovies_Click(sender As Object, e As EventArgs) Handles btntraktWatchlistGetMovies.Click
        Try
            If Not myWatchlistEpisodes Is Nothing Then
                myWatchlistEpisodes.Clear()
            End If
            dgvtraktWatchlist.DataSource = Nothing
            dgvtraktWatchlist.Rows.Clear()

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'GET movies of users watchlist
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 2 values in Dictionary style, we are using dictionary because later you can easily check with Dict.containsKey if entry already exists (not so easy to do with a complex list)
            Dim dictMovieWatchlist As New Dictionary(Of String, String)

            traktToken = LoginToTrakt(traktUser, traktPassword, traktToken)

            If Not String.IsNullOrEmpty(traktToken) Then
                Dim traktWatchListMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatchList) = TrakttvAPI.GetWatchListMovies(traktUser)

                For Each Item As TraktAPI.Model.TraktMovieWatchList In traktWatchListMovies
                    'Check if information is stored...
                    If Not Item.Movie.Title Is Nothing AndAlso Item.Movie.Title <> "" AndAlso Not Item.Movie.Ids.Imdb Is Nothing AndAlso Item.Movie.Ids.Imdb <> "" Then
                        'check if movie is already in dictionary!
                        If Not dictMovieWatchlist.ContainsKey(Item.Movie.Title) Then
                            'Now store imdbid, title
                            If Item.Movie.Ids.Imdb.Length > 2 AndAlso Item.Movie.Ids.Imdb.Substring(0, 2) = "tt" Then
                                'IMDBID beginning with tt -> strip tt first and save only number!
                                dictMovieWatchlist.Add(Item.Movie.Ids.Imdb.Substring(2), Item.Movie.Title)
                            Else
                                'IMDBID is alright
                                dictMovieWatchlist.Add(Item.Movie.Ids.Imdb, Item.Movie.Title)
                            End If
                            'add movie to list of WatchlistMovies
                            myWatchlistMovies.Add(Item)
                        End If
                    End If
                Next

                'Set up /load listofwatchedmovies into datagridview
                dgvtraktWatchlist.AutoGenerateColumns = True
                If Not myWatchlistMovies Is Nothing Then
                    btntraktWatchlistSyncLibrary.Enabled = True
                    btntraktWatchlistClean.Enabled = True
                    btntraktWatchlistSendEmberUnwatched.Enabled = True
                    'we map to dgv manually
                    dgvtraktWatchlist.AutoGenerateColumns = False
                    'fill rows
                    ' Go through each item in collection, set columncontent of datagridview	
                    For Each Item As TraktAPI.Model.TraktMovieWatchList In traktWatchListMovies

                        'listed-At is not user friendly formatted, so change format a bit
                        '"listed_at": 2014-09-01T09:10:11.000Z (original)
                        'new format here: 2014-09-01  09:10:11
                        Dim myDateString As String = Item.ListedAt
                        Dim myDate As DateTime
                        Dim isDate As Boolean = DateTime.TryParse(myDateString, myDate)
                        If isDate Then
                            dgvtraktWatchlist.Rows.Add(New Object() {Item.Movie.Title, Item.Movie.Year, myDate.ToString("yyyy-MM-dd hh:mm"), "http://www.imdb.com/title/" & Item.Movie.Ids.Imdb})
                        Else
                            dgvtraktWatchlist.Rows.Add(New Object() {Item.Movie.Title, Item.Movie.Year, Item.ListedAt, "http://www.imdb.com/title/" & Item.Movie.Ids.Imdb})
                        End If
                    Next
                Else
                    btntraktWatchlistSyncLibrary.Enabled = False
                    btntraktWatchlistClean.Enabled = False
                    btntraktWatchlistSendEmberUnwatched.Enabled = False
                End If
            Else
                logger.Warn("[btntraktWatchlistGetMovies_Click] No token!")
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            If Not myWatchlistEpisodes Is Nothing Then
                myWatchlistEpisodes.Clear()
            End If
            dgvtraktWatchlist.DataSource = Nothing
            dgvtraktWatchlist.Rows.Clear()
            btntraktWatchlistSyncLibrary.Enabled = False
            btntraktWatchlistClean.Enabled = False
            btntraktWatchlistSendEmberUnwatched.Enabled = False
        End Try

    End Sub

    ''' <summary>
    '''  Remove watched movies/shows/episodes from trakt.tv watchlist
    ''' </summary>
    ''' <param name="sender">"Remove watched movies from trakt.tv Watchlist"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/18 Cocotus - First implementation, for now only movie support!
    ''' This sub  handles removing watched Ember movies/episodes/shows from trakt.tv wachtlist
    ''' </remarks>
    Private Sub btntraktWatchlistSyncLibrary_Click(sender As Object, e As EventArgs) Handles btntraktWatchlistSyncLibrary.Click

        Try
            'Check if theres at least one movie in trakt.tv watchlist and in Ember database - otherwise abort
            If myWatchlistMovies.Count > 0 AndAlso Me.dtMovies.Rows.Count > 0 Then
                Dim lstmovietoremove As New List(Of TraktAPI.Model.TraktMovie)
                For Each sRow As DataRow In Me.dtMovies.Rows
                    If sRow.Item("Playcount").ToString <> "0" AndAlso sRow.Item("Playcount").ToString <> "" Then
                        For Each Item As TraktAPI.Model.TraktMovieWatchList In myWatchlistMovies
                            If Item.Movie.Ids.Imdb.Contains(sRow.Item("Imdb").ToString) Then
                                Dim tmptraktbasemovie As New TraktAPI.Model.TraktMovie
                                tmptraktbasemovie.Title = Item.Movie.Title
                                tmptraktbasemovie.Year = Item.Movie.Year
                                tmptraktbasemovie.Ids = Item.Movie.Ids
                                lstmovietoremove.Add(tmptraktbasemovie)
                                Exit For
                            End If
                        Next
                    End If
                Next
                If lstmovietoremove.Count > 0 Then

                    traktToken = LoginToTrakt(traktUser, traktPassword, traktToken)
                    If Not String.IsNullOrEmpty(traktToken) Then
                        Dim deletemoviemodel As New TraktAPI.Model.TraktSyncMovies
                        deletemoviemodel.Movies = lstmovietoremove
                        Dim response = TrakttvAPI.RemoveMoviesFromWatchlist(deletemoviemodel)
                        If Not response Is Nothing Then
                            If Not response.Added Is Nothing Then
                                logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Added movies: " & response.Added.Movies)
                            End If
                            If Not response.NotFound Is Nothing Then
                                logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                            End If
                            If Not response.Existing Is Nothing Then
                                logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                            End If
                            If Not response.Deleted Is Nothing Then
                                logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                            End If
                        End If
                        If Not response Is Nothing AndAlso Not response.Deleted Is Nothing AndAlso response.Deleted.Movies > 0 Then
                            MessageBox.Show(response.Deleted.Movies.ToString & " " & Master.eLang.GetString(9999, "Movies removed from trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                            myWatchlistMovies.Clear()
                            If Not myWatchlistEpisodes Is Nothing Then
                                myWatchlistEpisodes.Clear()
                            End If
                            dgvtraktWatchlist.DataSource = Nothing
                            dgvtraktWatchlist.Rows.Clear()
                            btntraktWatchlistSyncLibrary.Enabled = False
                            btntraktWatchlistClean.Enabled = False
                            btntraktWatchlistSendEmberUnwatched.Enabled = False
                        Else
                            MessageBox.Show(Master.eLang.GetString(9999, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                        End If
                    Else
                        logger.Warn("[btntraktWatchlistSyncLibrary_Click] No token!")
                    End If
                Else
                    logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies to remove from watchlist!")
                    MessageBox.Show(Master.eLang.GetString(9999, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                End If
            Else
                logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies in watchlist/Ember database - Abort process!")
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    ''' <summary>
    '''  Send all unwatched movies/shows/episodes from trakt.tv to watchlist
    ''' </summary>
    ''' <param name="sender">"Send unwatched movies to trakt.tv Watchlist"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/18 Cocotus - First implementation, for now only movie support!
    ''' This sub  handles adding ALL unwatched Ember movies/episodes/shows from trakt.tv wachtlist
    ''' </remarks>
    Private Sub btntraktWatchlistSendEmberUnwatched_Click(sender As Object, e As EventArgs) Handles btntraktWatchlistSendEmberUnwatched.Click
        Try
            Dim lstmovietoadd As New List(Of TraktAPI.Model.TraktMovie)
            For Each sRow As DataRow In Me.dtMovies.Rows
                If sRow.Item("Playcount").ToString = "0" OrElse sRow.Item("Playcount").ToString = "" Then
                    Dim tmptraktmovie As New TraktAPI.Model.TraktMovie
                    Dim tmptraktbasemovie As New TraktAPI.Model.TraktMovieBase
                    tmptraktmovie.Title = sRow.Item("Title").ToString
                    'If IsNumeric(sRow.Item("Year")) Then
                    '    tmptraktbasemovie.Year = CType(sRow.Item("Year"), Integer?)
                    'Else
                    '    logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] Year of movie " & sRow.Item("Title").ToString & " is not valid, can't add that information!")
                    'End If
                    If String.IsNullOrEmpty(sRow.Item("Imdb").ToString) Then
                        logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] IMDB of movie " & sRow.Item("Title").ToString & " is not valid, can't add that information!")
                    Else
                        If IsNumeric(sRow.Item("Imdb").ToString) Then
                            tmptraktbasemovie.Imdb = "tt" & sRow.Item("Imdb").ToString
                        Else
                            tmptraktbasemovie.Imdb = sRow.Item("Imdb").ToString
                            logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] IMDB of movie " & sRow.Item("Title").ToString & " was not regular (missing tt)!")
                        End If
                    End If
                    tmptraktbasemovie.Slug = Trakttv.TraktMethods.ConvertToSlug(sRow.Item("Title").ToString & sRow.Item("Year").ToString)
                    'If IsNumeric(sRow.Item("TMDB").ToString) Then
                    '    tmptraktbasemovie.Tmdb = CType(sRow.Item("TMDB"), Integer?)
                    'Else
                    '    logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] TMDB of movie " & sRow.Item("Title").ToString & " is not valid, can't add that information!")
                    'End If
                    tmptraktmovie.Ids = tmptraktbasemovie
                    'add movie to list of moviewatchlist to add
                    lstmovietoadd.Add(tmptraktmovie)
                End If
            Next
            'if there are movies to add to watchlist, start post process
            If lstmovietoadd.Count > 0 Then
                traktToken = LoginToTrakt(traktUser, traktPassword, traktToken)

                If Not String.IsNullOrEmpty(traktToken) Then
                    Dim traktsyncmovies As New TraktAPI.Model.TraktSyncMovies
                    traktsyncmovies.Movies = lstmovietoadd
                    Dim response = TrakttvAPI.AddMoviesToWatchlist(traktsyncmovies)
                    If Not response Is Nothing AndAlso Not response.Added Is Nothing AndAlso response.Added.Movies > 0 Then
                        MessageBox.Show(response.Added.Movies.ToString & " " & Master.eLang.GetString(9999, "Movies added to trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                        myWatchlistMovies.Clear()
                        If Not myWatchlistEpisodes Is Nothing Then
                            myWatchlistEpisodes.Clear()
                        End If
                        dgvtraktWatchlist.DataSource = Nothing
                        dgvtraktWatchlist.Rows.Clear()
                        btntraktWatchlistSyncLibrary.Enabled = False
                        btntraktWatchlistClean.Enabled = False
                        btntraktWatchlistSendEmberUnwatched.Enabled = False
                    Else
                        MessageBox.Show(Master.eLang.GetString(9999, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                    End If
                    If Not response Is Nothing Then
                        If Not response.Added Is Nothing Then
                            logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Added movies: " & response.Added.Movies)
                        End If
                        If Not response.NotFound Is Nothing Then
                            logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                        End If
                        If Not response.Existing Is Nothing Then
                            logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                        End If
                        If Not response.Deleted Is Nothing Then
                            logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                        End If
                    End If
                Else
                    logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] No token!")
                End If
            Else
                logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies to add to watchlist!")
                MessageBox.Show(Master.eLang.GetString(9999, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    ''' <summary>
    '''  Clear all movies from trakt.tv watchlist (Delete all movie entries!)
    ''' </summary>
    ''' <param name="sender">"Clear trak.tv watchlist"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/18 Cocotus - First implementation
    ''' This sub  handles deleting ALL movies/episodes/shows from trakt.tv wachtlist
    ''' For now only movies will be deleted!
    ''' </remarks>
    Private Sub btntraktWatchlistClean_Click(sender As Object, e As EventArgs) Handles btntraktWatchlistClean.Click
        Try
            If myWatchlistMovies.Count > 0 Then
                Dim lstmovietoremove As New List(Of TraktAPI.Model.TraktMovie)
                'need to convert all movies from watchlist-object to general base list of TraktAPI.Model.TraktMovie
                For Each Item As TraktAPI.Model.TraktMovieWatchList In myWatchlistMovies
                    Dim tmptraktbasemovie As New TraktAPI.Model.TraktMovie
                    tmptraktbasemovie.Title = Item.Movie.Title
                    tmptraktbasemovie.Year = Item.Movie.Year
                    tmptraktbasemovie.Ids = Item.Movie.Ids
                    lstmovietoremove.Add(tmptraktbasemovie)
                Next

                If lstmovietoremove.Count > 0 Then
                    traktToken = LoginToTrakt(traktUser, traktPassword, traktToken)

                    If Not String.IsNullOrEmpty(traktToken) Then
                        Dim deletemoviemodel As New TraktAPI.Model.TraktSyncMovies
                        deletemoviemodel.Movies = lstmovietoremove
                        Dim response = TrakttvAPI.RemoveMoviesFromWatchlist(deletemoviemodel)
                        If Not response Is Nothing Then
                            If Not response.Added Is Nothing Then
                                logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Added movies: " & response.Added.Movies)
                            End If
                            If Not response.NotFound Is Nothing Then
                                logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                            End If
                            If Not response.Existing Is Nothing Then
                                logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                            End If
                            If Not response.Deleted Is Nothing Then
                                logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                            End If
                        End If
                        If Not response Is Nothing AndAlso Not response.Deleted Is Nothing AndAlso response.Deleted.Movies > 0 Then
                            MessageBox.Show(response.Deleted.Movies.ToString & " " & Master.eLang.GetString(9999, "Movies removed from trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                            myWatchlistMovies.Clear()
                            If Not myWatchlistEpisodes Is Nothing Then
                                myWatchlistEpisodes.Clear()
                            End If
                            dgvtraktWatchlist.DataSource = Nothing
                            dgvtraktWatchlist.Rows.Clear()
                            btntraktWatchlistSyncLibrary.Enabled = False
                            btntraktWatchlistClean.Enabled = False
                            btntraktWatchlistSendEmberUnwatched.Enabled = False
                        Else
                            MessageBox.Show(Master.eLang.GetString(9999, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                        End If
                    Else
                        logger.Warn("[btntraktWatchlistClean_Click] No token!")
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Open link in datagrid using default browser
    ''' </summary>
    ''' <param name="sender">Cell click event</param>
    ''' <remarks>
    ''' 2015/01/18 Cocotus - First implementation
    Private Sub dgvtraktWatchList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvtraktWatchlist.CellContentClick
        Try
            If e.RowIndex > -1 Then
                If dgvtraktWatchlist(e.ColumnIndex, e.RowIndex).Value.ToString().StartsWith("http") Then
                    System.Diagnostics.Process.Start(dgvtraktWatchlist.CurrentCell.Value.ToString())
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region

#Region "Trakt.tv Sync Playcount"

    ''' <summary>
    ''' Commandline call: Update/Sync playcounts of movies and episodes
    ''' </summary>
    ''' <param name="sender">"Get watched movies"-Button in Form</param>
    ''' <remarks>
    ''' TODO: Needs some testing (error handling..)!? Idea: Can be executed via commandline to update/sync playcounts of movies and episodes
    ''' </remarks>
    Public Shared Sub CLSyncPlaycount()
        Try
            Dim MySelf As New dlgTrakttvManager
            If Master.eSettings.UseTrakt = False Then
                Return
            End If
            MySelf.isCL = True
            MySelf.bwLoad = New System.ComponentModel.BackgroundWorker
            MySelf.bwLoad.WorkerSupportsCancellation = True
            MySelf.bwLoad.WorkerReportsProgress = True
            MySelf.bwLoad.RunWorkerAsync()
            While MySelf.bwLoad.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
            'Sync movie playcounts
            MySelf.btntraktPlaycountGetMovies_Click(Nothing, Nothing)
            MySelf.btntraktPlaycountSyncLibrary_Click(Nothing, Nothing)
            'Sync episodes playcounts
            MySelf.btntraktPlaycountGetSeries_Click(Nothing, Nothing)
            MySelf.btntraktPlaycountSyncLibrary_Click(Nothing, Nothing)
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub



    ''' <summary>
    ''' GET watched movies of user and display in listbox
    ''' </summary>
    ''' <param name="sender">"Get watched movies"-Button in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btntraktPlaycountGetMovies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntraktPlaycountGetMovies.Click
        Try
            myWatchedEpisodes = Nothing
            dgvtraktPlaycount.DataSource = Nothing
            dgvtraktPlaycount.Rows.Clear()

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Get watched movies of user
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 3 values in Dictionary style
            Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, Integer))

            ' Use new Trakttv wrapper class to get watched data!
            Dim account As New TraktAPI.Model.TraktAuthentication
            account.Username = traktUser
            account.Password = traktPassword
            TraktSettings.Password = traktPassword
            TraktSettings.Username = traktUser

            If String.IsNullOrEmpty(traktToken) Then
                Dim response = Trakttv.TraktMethods.LoginToAccount(account)
                traktToken = response.Token
            Else
                TraktSettings.Token = traktToken
            End If

            If Not String.IsNullOrEmpty(traktToken) Then
                Dim traktWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched) = TrakttvAPI.GetWatchedMovies
                ' Go through each item in collection	 
                For Each Item As TraktAPI.Model.TraktMovieWatched In traktWatchedMovies
                    'Check if information is stored...
                    If Not Item.Movie.Title Is Nothing AndAlso Item.Movie.Title <> "" AndAlso Not Item.Movie.Ids.Imdb Is Nothing AndAlso Item.Movie.Ids.Imdb <> "" Then
                        If Not dictMovieWatched.ContainsKey(Item.Movie.Title) Then
                            'Now store imdbid, title and playcount information into dictionary (for now no other info needed...)
                            If Item.Movie.Ids.Imdb.Length > 2 AndAlso Item.Movie.Ids.Imdb.Substring(0, 2) = "tt" Then
                                'IMDBID beginning with tt -> strip tt first and save only number!
                                dictMovieWatched.Add(Item.Movie.Title, New KeyValuePair(Of String, Integer)(Item.Movie.Ids.Imdb.Substring(2), CInt(Item.Plays)))
                            Else
                                'IMDBID is alright
                                dictMovieWatched.Add(Item.Movie.Title, New KeyValuePair(Of String, Integer)(Item.Movie.Ids.Imdb, CInt(Item.Plays)))
                            End If
                        End If
                    End If
                Next
            Else

            End If
            myWatchedMovies = dictMovieWatched

            dgvtraktPlaycount.AutoGenerateColumns = True
            If Not myWatchedMovies Is Nothing Then
                btntraktPlaycountSyncLibrary.Enabled = True
                'we map to dgv manually
                dgvtraktPlaycount.AutoGenerateColumns = False
                'fill rows
                For Each Item In myWatchedMovies
                    dgvtraktPlaycount.Rows.Add(New Object() {Item.Key, Item.Value.Value})
                Next
            Else
                btntraktPlaycountSyncLibrary.Enabled = False
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            myWatchedEpisodes = Nothing
            dgvtraktPlaycount.DataSource = Nothing
            dgvtraktPlaycount.Rows.Clear()
            btntraktPlaycountSyncLibrary.Enabled = False
        End Try

    End Sub

    ''' <summary>
    ''' GET watched episodes of user and display in listbox
    ''' </summary>
    ''' <param name="sender">"Get watched episodes"-Button in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btntraktPlaycountGetSeries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntraktPlaycountGetSeries.Click
        Try
            myWatchedMovies = Nothing
            dgvtraktPlaycount.DataSource = Nothing
            dgvtraktPlaycount.Rows.Clear()

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ' Get all episodes on Trakt.tv that are marked as 'seen' or 'watched'
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 3 values in Dictionary style
            ' Dim dictEpisodesWatched As New Dictionary(Of String, KeyValuePair(Of String, List(Of TraktAPI.Model.TraktSyncEpisodeWatched)))
            'Helper: Saving 3 values in Dictionary style: TVDB, SeasonNumber|Episodenumber
            Dim dictEpisodesWatched As New Dictionary(Of String, List(Of KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode))))

            ' Use new Trakttv wrapper class to get watched data!
            Dim account As New TraktAPI.Model.TraktAuthentication
            account.Username = traktUser
            account.Password = traktPassword
            TraktSettings.Password = traktPassword
            TraktSettings.Username = traktUser

            If String.IsNullOrEmpty(traktToken) Then
                Dim response = Trakttv.TraktMethods.LoginToAccount(account)
                traktToken = response.Token
            Else
                TraktSettings.Token = traktToken
            End If

            Dim traktWatchedEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched) = TrakttvAPI.GetWatchedEpisodes
            Dim dictWatchedTVShows As New Dictionary(Of String, Integer)
            ' Go through each item in collection	
            If traktWatchedEpisodes Is Nothing = False Then
                For Each watchedtvshow In traktWatchedEpisodes
                    If Not watchedtvshow.Show.Title Is Nothing AndAlso watchedtvshow.Show.Title <> "" AndAlso Not watchedtvshow.Show.Ids.Tvdb Is Nothing AndAlso watchedtvshow.Show.Ids.Tvdb.ToString <> "" Then
                        If Not dictEpisodesWatched.ContainsKey(CStr(watchedtvshow.Show.Ids.Tvdb)) Then
                            dictWatchedTVShows.Add(watchedtvshow.Show.Title, watchedtvshow.Plays)
                            'Now store tvdbID, title and the season-episode-list in dictionary...
                            Dim listWatchedEpisodes As New List(Of KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode)))
                            For Each watchedseason In watchedtvshow.Seasons
                                Dim episodesinseason As New KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode))(watchedseason.Number, watchedseason.Episodes)
                                listWatchedEpisodes.Add(episodesinseason)
                            Next
                            dictEpisodesWatched.Add(CStr(watchedtvshow.Show.Ids.Tvdb), listWatchedEpisodes)
                        End If
                    End If
                Next
            End If
            myWatchedEpisodes = dictEpisodesWatched

            dgvtraktPlaycount.AutoGenerateColumns = True
            If Not myWatchedEpisodes Is Nothing Then
                btntraktPlaycountSyncLibrary.Enabled = True
                'we map to dgv manually
                dgvtraktPlaycount.AutoGenerateColumns = False
                'fill rows
                For Each Item In dictWatchedTVShows
                    dgvtraktPlaycount.Rows.Add(New Object() {Item.Key, Item.Value})
                Next
                'For Each Item In myWatchedEpisodes
                '    'x = Episodes watched für specific tv show, use a loop to sum up the episodes of tvshow
                '    Dim x As Integer = 0
                '    For i = 0 To Item.Value.Value.Count - 1
                '        '     x = x + Item.Value.Value.Item(i).Episodes.Count
                '    Next

                '    dgvtraktPlaycount.Rows.Add(New Object() {Item.Key, x})
                'Next
            Else
                btntraktPlaycountSyncLibrary.Enabled = False
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            myWatchedMovies = Nothing
            dgvtraktPlaycount.DataSource = Nothing
            dgvtraktPlaycount.Rows.Clear()
            btntraktPlaycountSyncLibrary.Enabled = False
        End Try

    End Sub

    ''' <summary>
    '''  Save either playcounts of movie or episodes to Ember database/Nfo
    ''' </summary>
    ''' <param name="sender">"Save playcount to database/Nfo"-Button in Form</param>
    ''' <remarks>
    ''' This sub  handles saving of movies and episodes playcount to nfo
    ''' </remarks>
    Private Sub btntraktPlaycountSyncLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntraktPlaycountSyncLibrary.Click

        Try
            'check if playcount(s) of movies or episodes should be saved - only one type will be done!
            If Not myWatchedMovies Is Nothing Then
                'Save movie playcount!
                prgtraktPlaycount.Value = 0
                prgtraktPlaycount.Maximum = myWatchedMovies.Count
                prgtraktPlaycount.Minimum = 0
                prgtraktPlaycount.Step = 1
                btntraktPlaycountSyncLibrary.Enabled = False
                Dim traktthread As Threading.Thread
                traktthread = New Threading.Thread(AddressOf SaveMoviePlaycount)
                traktthread.IsBackground = True
                traktthread.Start()
            ElseIf Not myWatchedEpisodes Is Nothing Then
                'save episodes playcount!
                prgtraktPlaycount.Value = 0
                prgtraktPlaycount.Maximum = myWatchedEpisodes.Count
                'start not with empty progressbar(no problem for movies) because it takes long to update for first tv show and user might think it hangs -> set value 1 to show something is going on
                If myWatchedEpisodes.Count > 1 Then
                    prgtraktPlaycount.Value = 1
                End If
                prgtraktPlaycount.Minimum = 0
                prgtraktPlaycount.Step = 1
                btntraktPlaycountSyncLibrary.Enabled = False
                Dim traktthread As Threading.Thread
                traktthread = New Threading.Thread(AddressOf SaveEpisodePlaycount)
                traktthread.IsBackground = True
                traktthread.Start()
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    '''  Save playcounts of movie(s) to Ember database/Nfo
    ''' </summary>
    ''' <remarks>
    ''' Used in thread: Saves playcount to database
    ''' </remarks>
    Private Sub SaveMoviePlaycount()
        Try
            Dim i As Integer = 0
            For Each watchedMovieData In myWatchedMovies
                i = i + 1
                '  logger.Info("[SaveMoviePlaycount] MovieID" & watchedMovieData.Value.Key & " Playcount: " & watchedMovieData.Value.Value.ToString)
                Master.DB.SaveMoviePlayCountInDatabase(watchedMovieData)
                ' Invoke to update UI from thread...
                prgtraktPlaycount.Invoke(New UpdateProgressBarDelegate(AddressOf UpdateProgressBar), i)
                Threading.Thread.Sleep(10)
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    ''' <summary>
    '''  Save playcounts of episode(s) to Ember database/Nfo
    ''' </summary>
    ''' <remarks>
    ''' Used in thread: Saves playcount to database
    ''' </remarks>
    Private Sub SaveEpisodePlaycount()
        Try

            Dim i As Integer = 0
            For Each watchedShowData In myWatchedEpisodes
                i = i + 1

                '  loop through every season of certain tvshow
                For z = 0 To watchedShowData.Value.Count - 1
                    ' now go to every episode of current season
                    For Each episode In watchedShowData.Value(z).Value
                        '..and save playcount of every episode to database
                        '    logger.Info("[SaveEpisodePlaycount] ShowID" & watchedShowData.Key & " Season: " & watchedShowData.Value(z).Key.ToString & " Episode: " & episode.Number.ToString)
                        Master.DB.SaveEpisodePlayCountInDatabase(watchedShowData.Key, watchedShowData.Value(z).Key.ToString, episode.Number.ToString)
                    Next
                Next
                ' Invoke to update UI from thread...
                prgtraktPlaycount.Invoke(New UpdateProgressBarDelegate(AddressOf UpdateProgressBar), i)
                Threading.Thread.Sleep(10)
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub


    Private Delegate Sub UpdateProgressBarDelegate(ByVal i As Integer)
    ''' <summary>
    '''  Playcount progressbar update
    ''' </summary>
    ''' <remarks>
    ''' Do all the ui thread updates here
    ''' Use progressbar to show user progress of saving, since it can take some time
    ''' </remarks>
    Private Sub UpdateProgressBar(ByVal i As Integer)
        Try
            If i = 1 Then
                lbltraktPlaycountstate.Visible = False
            End If

            prgtraktPlaycount.Value = i
            If Not myWatchedMovies Is Nothing Then
                If i = myWatchedMovies.Count - 1 Then
                    lbltraktPlaycountstate.Text = "Done!"
                    lbltraktPlaycountstate.Visible = True
                End If
            ElseIf Not myWatchedEpisodes Is Nothing Then
                If i = myWatchedEpisodes.Count - 1 Then
                    lbltraktPlaycountstate.Text = "Done!"
                    lbltraktPlaycountstate.Visible = True
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

#End Region 'Trakt.tv Sync Playcount

    '#Region "Trakt.tv Sync Lists/Tags"
    '    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '    '1. GET and POST userlists from/to trakt.tv using trakt.tv API calls!
    '    '2. Update/Sync movie tags (if part of list: "trakt.tv_listname" in tag)
    '    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '    ''' <summary>
    '    ''' GET personal lists of user and display in listbox
    '    ''' </summary>
    '    ''' <param name="sender">"Get List"-Button in Form</param>
    '    ''' <remarks>
    '    '''  'Adding a movie to list means updating "traktLists" and "currList" - then refresh view of listbox 
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsGetPersonal_Click(sender As Object, e As EventArgs) Handles btntraktListsGetPersonal.Click
    '        Try
    '            lbtraktLists.Items.Clear()
    '            lbtraktListsMoviesinLists.Items.Clear()
    '            lbtraktLists.Items.Clear()
    '            pnltraktLists.Enabled = False

    '            ' start backgroundworker
    '            'btnCancel.Visible = True
    '            'lblCompiling.Visible = True
    '            'prbCompile.Visible = True
    '            'prbCompile.Style = ProgressBarStyle.Continuous
    '            'lblCanceling.Visible = False
    '            'pnlCancel.Visible = True
    '            'Application.DoEvents()
    '            'Me.bwLoad.WorkerSupportsCancellation = True
    '            'Me.bwLoad.WorkerReportsProgress = True
    '            'Me.bwLoad.RunWorkerAsync()

    '            'MOVED CODE To Backgroundworker!!
    '            ' Use new Trakttv wrapper class to get userlists!
    '            Trakttv.TraktSettings.Username = traktUser
    '            Trakttv.TraktSettings.Password = traktPassword
    '            'GET ALL userlists from user
    '            Dim traktUserLists As IEnumerable(Of TraktAPI.Model.TraktUserList) = TraktAPI.TrakttvAPI.GetUserLists(traktUser)

    '            If Not traktUserLists Is Nothing Then
    '                ' Go through each userlist in collection 
    '                For Each userlist In traktUserLists
    '                    'Check if information is stored...
    '                    If Not userlist.Name Is Nothing Then
    '                        'slug is the name of a list
    '                        Dim listname As String = userlist.Slug
    '                        If Not String.IsNullOrEmpty(listname) Then

    '                            'GET userlist information
    '                            Dim traktUserList As TraktAPI.Model.TraktUserList = TraktAPI.TrakttvAPI.GetUserList(traktUser, listname)

    '                            'Check if current userlist is valid and contains required data
    '                            If Not traktUserList Is Nothing AndAlso Not traktUserList.Name Is Nothing AndAlso Not traktUserList.Items Is Nothing Then
    '                                'fill rows
    '                                Dim addlist As Boolean = True
    '                                'if list contains movies (= not empty list)  check if there's only movies - Ember doesn't support episodes right now!
    '                                For Each item In traktUserList.Items
    '                                    If Not item.Type Is Nothing AndAlso item.Type = "movie" AndAlso Not item.Title Is Nothing AndAlso item.Title <> "" AndAlso Not item.ImdbId Is Nothing AndAlso item.ImdbId <> "" Then
    '                                        'valid movie!
    '                                    Else
    '                                        addlist = False
    '                                        Exit For
    '                                    End If
    '                                Next
    '                                If addlist = True Then
    '                                    Dim tmptraktlist As TraktAPI.Model.TraktList = Nothing
    '                                    tmptraktlist = ConvertTraktlist(traktUserList)
    '                                    If Not tmptraktlist Is Nothing Then
    '                                        traktLists.Add(tmptraktlist)
    '                                        logger.Info("[" & tmptraktlist.Name & "] " & "Userlist added to globalist!")
    '                                    Else
    '                                        'convert of userlist to traktlist failed
    '                                        logger.Info("Userlist could not be converted to traktlist")
    '                                    End If
    '                                Else
    '                                    'invalid list, may contains episodes, invalid movie entrys!
    '                                    logger.Info("Userlist contains invalid data (episodes, missing information)")
    '                                End If
    '                            Else
    '                                logger.Info("invalid userlist can't be scraped from trakt.tv!")
    '                            End If
    '                        Else
    '                            logger.Info("userlist without name can't be scraped from trakt.tv!")
    '                        End If
    '                    Else
    '                        logger.Info("Can't fetch userlist from trakt.tv!")
    '                    End If
    '                Next
    '            Else
    '                logger.Info("Can't fetch userlist from trakt.tv!")
    '            End If

    '            'if there's any list that could be scraped then fill other listboxes/datagridview and finish loading process
    '            If Not traktLists Is Nothing AndAlso traktLists.Count > 0 Then

    '                If dgvMovies.Rows.Count = 0 Then
    '                    'fill movie datagridview
    '                    Me.dgvMovies.SuspendLayout()
    '                    Me.bsMovies.DataSource = Nothing
    '                    Me.dgvMovies.DataSource = Nothing
    '                    If Me.dtMovies.Rows.Count > 0 Then
    '                        With Me
    '                            .bsMovies.DataSource = .dtMovies
    '                            .dgvMovies.DataSource = .bsMovies

    '                            .dgvMovies.Columns(0).Visible = False
    '                            .dgvMovies.Columns(1).Visible = False
    '                            .dgvMovies.Columns(2).Visible = False
    '                            .dgvMovies.Columns(3).Resizable = DataGridViewTriState.True
    '                            .dgvMovies.Columns(3).ReadOnly = True
    '                            .dgvMovies.Columns(3).MinimumWidth = 83
    '                            .dgvMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
    '                            .dgvMovies.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
    '                            .dgvMovies.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")

    '                            For i As Integer = 4 To .dgvMovies.Columns.Count - 1
    '                                .dgvMovies.Columns(i).Visible = False
    '                            Next

    '                            .dgvMovies.Columns(0).ValueType = GetType(Int32)
    '                        End With
    '                    End If
    '                    Me.dgvMovies.ResumeLayout()
    '                End If


    '                'fill global lbtraktLists - all lists scraped from trakt.tv account!
    '                For Each list In traktLists
    '                    lbtraktLists.Items.Add(list.Name)
    '                Next
    '                'enable filled traktlistbox + "new traktlist" button + movieinlist listbox
    '                lbtraktLists.Enabled = True
    '                btntraktListsNewList.Enabled = True
    '                Me.lbtraktListsMoviesinLists.Enabled = True
    '                'select first item in listbox
    '                If Me.lbtraktLists.Items.Count > 0 Then
    '                    lbtraktLists.SelectedIndex = 0
    '                    'Enable Sync Button when there's at least one list!
    '                    btntraktListsSyncTrakt.Enabled = True
    '                End If


    '                If lbDBLists.Items.Count = 0 Then
    '                    'TODO: Change from SET to TAG once tag support is here!! 
    '                    'fill lbDBLists(Listbox) - all lists/tags from Ember database!
    '                    Master.DB.FillDataTable(Me.dtMovieTags, String.Concat("SELECT Sets.ID, Sets.ListTitle, Sets.HasNfo, Sets.NfoPath, Sets.HasPoster, Sets.PosterPath, Sets.HasFanart, ", _
    '                                                                               "Sets.FanartPath, Sets.HasBanner, Sets.BannerPath, Sets.HasLandscape, Sets.LandscapePath, Sets.HasDiscArt, ", _
    '                                                                               "Sets.DiscArtPath, Sets.HasClearLogo, Sets.ClearLogoPath, Sets.HasClearArt, Sets.ClearArtPath, Sets.TMDBColID, ", _
    '                                                                               "Sets.Plot, Sets.SetName, Sets.New, Sets.Mark, Sets.Lock, COUNT(MoviesSets.MovieID) AS 'Count' FROM Sets ", _
    '                                                                               "LEFT OUTER JOIN MoviesSets ON Sets.ID = MoviesSets.SetID GROUP BY Sets.ID ORDER BY Sets.ListTitle COLLATE NOCASE;"))

    '                    For Each sRow As DataRow In dtMovieTags.Rows
    '                        If Not String.IsNullOrEmpty(sRow.Item("ListTitle").ToString) Then
    '                            lbDBLists.Items.Add(sRow.Item("ListTitle").ToString)
    '                        End If
    '                    Next


    '                    'enable filled DBList (if there's any tag/list in database)
    '                    If lbDBLists.Items.Count > 0 Then
    '                        Me.lbDBLists.Enabled = True
    '                    Else
    '                        Me.lbDBLists.Enabled = False
    '                        Me.btntraktListsGetDatabase.Enabled = False
    '                    End If
    '                End If
    '                'enable avalaible movie datagridview
    '                dgvMovies.Enabled = True

    '            Else
    '                lbtraktLists.Enabled = False
    '                btntraktListsNewList.Enabled = False
    '                Me.lbtraktListsMoviesinLists.Enabled = False
    '                logger.Info("No userlist(s) available!")
    '            End If
    '            pnltraktLists.Enabled = True
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            pnltraktLists.Enabled = True
    '        End Try
    '    End Sub

    '    Private Sub bwLoad_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoad.DoWork
    '        '    '//
    '        '    ' Start thread
    '        '    '\\
    '        Try

    '            ' Use new Trakttv wrapper class to get userlists!
    '            Trakttv.TraktSettings.Username = traktUser
    '            Trakttv.TraktSettings.Password = traktPassword
    '            'GET ALL userlists from user
    '            Dim traktUserLists As IEnumerable(Of TraktAPI.Model.TraktUserList) = TraktAPI.TrakttvAPI.GetUserLists(traktUser)

    '            If Not traktUserLists Is Nothing Then
    '                ' Go through each userlist in collection 
    '                For Each userlist In traktUserLists
    '                    'Check if information is stored...
    '                    If Not userlist.Name Is Nothing Then
    '                        'slug is the name of a list
    '                        Dim listname As String = userlist.Slug
    '                        If Not String.IsNullOrEmpty(listname) Then

    '                            'GET userlist information
    '                            Dim traktUserList As TraktAPI.Model.TraktUserList = TraktAPI.TrakttvAPI.GetUserList(traktUser, listname)

    '                            'Check if current userlist is valid and contains required data
    '                            If Not traktUserList Is Nothing AndAlso Not traktUserList.Name Is Nothing AndAlso Not traktUserList.Items Is Nothing Then
    '                                'fill rows
    '                                Dim addlist As Boolean = True
    '                                'if list contains movies (= not empty list)  check if there's only movies - Ember doesn't support episodes right now!
    '                                For Each item In traktUserList.Items
    '                                    If Not item.Type Is Nothing AndAlso item.Type = "movie" AndAlso Not item.Title Is Nothing AndAlso item.Title <> "" AndAlso Not item.ImdbId Is Nothing AndAlso item.ImdbId <> "" Then
    '                                        'valid movie!
    '                                    Else
    '                                        addlist = False
    '                                        Exit For
    '                                    End If
    '                                Next
    '                                If addlist = True Then
    '                                    Dim tmptraktlist As TraktAPI.Model.TraktList = Nothing
    '                                    tmptraktlist = ConvertTraktlist(traktUserList)
    '                                    If Not tmptraktlist Is Nothing Then
    '                                        traktLists.Add(tmptraktlist)
    '                                        logger.Info("[" & tmptraktlist.Name & "] " & "Userlist added to globalist!")
    '                                    Else
    '                                        'convert of userlist to traktlist failed
    '                                        logger.Info("Userlist could not be converted to traktlist")
    '                                    End If
    '                                Else
    '                                    'invalid list, may contains episodes, invalid movie entrys!
    '                                    logger.Info("Userlist contains invalid data (episodes, missing information)")
    '                                End If
    '                            Else
    '                                logger.Info("invalid userlist can't be scraped from trakt.tv!")
    '                            End If
    '                        Else
    '                            logger.Info("userlist without name can't be scraped from trakt.tv!")
    '                        End If
    '                    Else
    '                        logger.Info("Can't fetch userlist from trakt.tv!")
    '                    End If
    '                Next
    '            Else
    '                logger.Info("Can't fetch userlist from trakt.tv!")
    '            End If

    '            'if there's any list that could be scraped then fill other listboxes/datagridview and finish loading process
    '            If Not traktLists Is Nothing AndAlso traktLists.Count > 0 Then

    '                If dgvMovies.Rows.Count = 0 Then
    '                    'fill movie datagridview
    '                    Me.dgvMovies.SuspendLayout()
    '                    Me.bsMovies.DataSource = Nothing
    '                    Me.dgvMovies.DataSource = Nothing
    '                    If Me.dtMovies.Rows.Count > 0 Then
    '                        With Me
    '                            .bsMovies.DataSource = .dtMovies
    '                            .dgvMovies.DataSource = .bsMovies

    '                            .dgvMovies.Columns(0).Visible = False
    '                            .dgvMovies.Columns(1).Visible = False
    '                            .dgvMovies.Columns(2).Visible = False
    '                            .dgvMovies.Columns(3).Resizable = DataGridViewTriState.True
    '                            .dgvMovies.Columns(3).ReadOnly = True
    '                            .dgvMovies.Columns(3).MinimumWidth = 83
    '                            .dgvMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
    '                            .dgvMovies.Columns(3).ToolTipText = Master.eLang.GetString(21, "Title")
    '                            .dgvMovies.Columns(3).HeaderText = Master.eLang.GetString(21, "Title")

    '                            For i As Integer = 4 To .dgvMovies.Columns.Count - 1
    '                                .dgvMovies.Columns(i).Visible = False
    '                            Next

    '                            .dgvMovies.Columns(0).ValueType = GetType(Int32)
    '                        End With
    '                    End If
    '                    Me.dgvMovies.ResumeLayout()
    '                End If


    '                'fill global lbtraktLists - all lists scraped from trakt.tv account!
    '                For Each list In traktLists
    '                    lbtraktLists.Items.Add(list.Name)
    '                Next
    '                'enable filled traktlistbox + "new traktlist" button + movieinlist listbox
    '                lbtraktLists.Enabled = True
    '                btntraktListsNewList.Enabled = True
    '                Me.lbtraktListsMoviesinLists.Enabled = True
    '                'select first item in listbox
    '                If Me.lbtraktLists.Items.Count > 0 Then
    '                    lbtraktLists.SelectedIndex = 0
    '                    'Enable Sync Button when there's at least one list!
    '                    btntraktListsSyncTrakt.Enabled = True
    '                End If


    '                If lbDBLists.Items.Count = 0 Then
    '                    'TODO: Change from SET to TAG once tag support is here!! 
    '                    'fill lbDBLists(Listbox) - all lists/tags from Ember database!
    '                    Master.DB.FillDataTable(Me.dtMovieTags, String.Concat("SELECT Sets.ID, Sets.ListTitle, Sets.HasNfo, Sets.NfoPath, Sets.HasPoster, Sets.PosterPath, Sets.HasFanart, ", _
    '                                                                               "Sets.FanartPath, Sets.HasBanner, Sets.BannerPath, Sets.HasLandscape, Sets.LandscapePath, Sets.HasDiscArt, ", _
    '                                                                               "Sets.DiscArtPath, Sets.HasClearLogo, Sets.ClearLogoPath, Sets.HasClearArt, Sets.ClearArtPath, Sets.TMDBColID, ", _
    '                                                                               "Sets.Plot, Sets.SetName, Sets.New, Sets.Mark, Sets.Lock, COUNT(MoviesSets.MovieID) AS 'Count' FROM Sets ", _
    '                                                                               "LEFT OUTER JOIN MoviesSets ON Sets.ID = MoviesSets.SetID GROUP BY Sets.ID ORDER BY Sets.ListTitle COLLATE NOCASE;"))

    '                    For Each sRow As DataRow In dtMovieTags.Rows
    '                        If Not String.IsNullOrEmpty(sRow.Item("ListTitle").ToString) Then
    '                            lbDBLists.Items.Add(sRow.Item("ListTitle").ToString)
    '                        End If
    '                    Next


    '                    'enable filled DBList (if there's any tag/list in database)
    '                    If lbDBLists.Items.Count > 0 Then
    '                        Me.lbDBLists.Enabled = True
    '                    Else
    '                        Me.lbDBLists.Enabled = False
    '                        Me.btntraktListsGetDatabase.Enabled = False
    '                    End If
    '                End If
    '                'enable avalaible movie datagridview
    '                dgvMovies.Enabled = True

    '            Else
    '                lbtraktLists.Enabled = False
    '                btntraktListsNewList.Enabled = False
    '                Me.lbtraktListsMoviesinLists.Enabled = False
    '                logger.Info("No userlist(s) available!")
    '            End If


    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub
    '    Private Sub bwLoadMovies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoad.ProgressChanged
    '        If e.ProgressPercentage >= 0 Then
    '            Me.prbCompile.Value = e.ProgressPercentage
    '            Me.lblFile.Text = e.UserState.ToString
    '        Else
    '            Me.prbCompile.Maximum = Convert.ToInt32(e.UserState)
    '        End If
    '    End Sub
    '    Private Sub bwLoad_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoad.RunWorkerCompleted
    '        '//
    '        ' Thread finished: fill movie and sets lists
    '        '\\
    '        Me.pnlCancel.Visible = False
    '        pnltraktLists.Enabled = True
    '    End Sub

    '    ''' <summary>
    '    ''' GET personal lists of user and display in listbox
    '    ''' </summary>
    '    ''' <param name="sender">"Get List"-Button in Form</param>
    '    ''' <remarks>
    '    '''  'Adding a movie to list means updating "traktLists" and "currList" - then refresh view of listbox 
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    ''' 
    '    Private Sub btntraktListsSync_Click(sender As Object, e As EventArgs) Handles btntraktListsSyncTrakt.Click
    '        Try
    '            If Not String.IsNullOrEmpty(traktUser) AndAlso Not String.IsNullOrEmpty(traktPassword) Then


    '                'Sync mechanism:
    '                '***************
    '                'Precedure for adding new created lists:
    '                '- 1. POST ListAdd <Listname> to trakt.tv and add every list which slug = Empty! (Those are new lists created in Ember!) 
    '                '- 2. If Step 1. success -> POST ListItemsAdd <Listname> and add movies!

    '                'Procedure for updating existing lists on trakt.tv:
    '                '- 1. POST ListAdd <Listname>new to trakt.tv and add every list which slug <> empty!  (Those are existing lists on trakt.tv!) 
    '                '- 2. If Step 1. success -> POST ListItemsAdd <Listname>new and add movies!
    '                '- 3. If Step 2. success -> POST ListDelete <Listname> to delete existing list(s)
    '                '- 4. If Step 3. success -> POST ListUpdate <Listname>new and change name of list to <Listname>

    '                ' Use new Trakttv wrapper class to get userlists!
    '                Trakttv.TraktSettings.Username = traktUser
    '                Trakttv.TraktSettings.Password = traktPassword
    '                ' Go through each traktlist in globallists
    '                For Each traktlist In traktLists
    '                    'Check if information is stored...
    '                    If Not traktlist.Name Is Nothing Then
    '                        Dim slugname As String = traktlist.Slug
    '                        Dim listname As String = traktlist.Name
    '                        Dim returnmessage As String = ""
    '                        If String.IsNullOrEmpty(slugname) Then
    '                            'new list!
    '                            logger.Info("will add new list to trakt.tv!")
    '                        End If
    '                        '1.POST ListAdd <Listname>new to trakt.tv
    '                        traktlist.Name = listname & "new"
    '                        logger.Info("[" & traktlist.Name & "] " & "Send list to trakt.tv!")
    '                        Dim traktResponse1 As TraktAPI.Model.TraktAddListResponse = TraktAPI.TrakttvAPI.SendListAdd(traktlist)
    '                        'Dim traktResponse33 As TraktAPI.Model.TraktResponse = TraktAPI.TrakttvAPI.SendListDelete(traktlist)

    '                        'returnmessage = returnmessage & traktResponse33.Status & " | " & traktResponse33.Message & " | " & traktResponse33.Error & " | " & traktResponse33.ToJSON & " | " & traktResponse33.ToString
    '                        '2.POST ListItemsAdd <Listname>new and add movies!
    '                        If Not traktResponse1 Is Nothing Then
    '                            If traktResponse1.Status = "success" Then
    '                                traktlist.Slug = slugname & "new"
    '                                logger.Info("[" & traktlist.Slug & "] " & "Added movies to list on trakt.tv!")
    '                                Dim traktResponse2 As TraktAPI.Model.TraktSyncResponse = TraktAPI.TrakttvAPI.SendListAddItems(traktlist)
    '                                If Not traktResponse2 Is Nothing Then
    '                                    '3. POST ListDelete <Listname> to delete existing list(s)
    '                                    If traktResponse2.Status = "success" Then
    '                                        traktlist.Slug = slugname
    '                                        logger.Info("[" & traktlist.Slug & "] " & "Delete list on trakt.tv!")
    '                                        Dim traktResponse3 As TraktAPI.Model.TraktResponse = TraktAPI.TrakttvAPI.SendListDelete(traktlist)
    '                                        '4. POST ListUpdate <Listname>new and change name of list to <Listname>
    '                                        If traktResponse3 Is Nothing Then
    '                                            logger.Info("[" & traktlist.Slug & "] " & "Delete list on trakt.tv FAILED!")
    '                                        ElseIf traktResponse3.Status <> "success" Then
    '                                            returnmessage = traktResponse3.Error & " | " & traktResponse3.Message
    '                                            logger.Info("[" & traktlist.Slug & "] " & "Delete list on trakt.tv FAILED!" & returnmessage)
    '                                        End If
    '                                        traktlist.Slug = slugname & "new"
    '                                        traktlist.Name = listname
    '                                        Dim traktResponse4 As TraktAPI.Model.TraktResponse = TraktAPI.TrakttvAPI.SendListUpdate(traktlist)
    '                                        If Not traktResponse4 Is Nothing Then
    '                                            If traktResponse4.Status = "success" Then
    '                                                traktlist.Slug = slugname
    '                                                logger.Info("[" & traktlist.Slug & "] " & "List updated on trakt.tv!")
    '                                            Else
    '                                                traktlist.Slug = slugname
    '                                                returnmessage = traktResponse4.Error & " | " & traktResponse4.Message
    '                                                logger.Info("[" & traktlist.Slug & "] " & "List updated on trakt.tv FAILED!" & returnmessage)
    '                                            End If
    '                                        Else
    '                                            traktlist.Slug = slugname
    '                                            logger.Info("[" & traktlist.Slug & "] " & "List updated on trakt.tv FAILED!")
    '                                        End If
    '                                    Else
    '                                        traktlist.Slug = slugname
    '                                        returnmessage = traktResponse2.Error & " | " & traktResponse2.Message
    '                                        logger.Info("[" & traktlist.Slug & "] " & "Added movies to list on trakt.tv FAILED!" & returnmessage)
    '                                    End If
    '                                Else
    '                                    traktlist.Slug = slugname
    '                                    logger.Info("[" & traktlist.Slug & "] " & "Added movies to list on trakt.tv FAILED!")
    '                                End If
    '                            Else
    '                                traktlist.Slug = slugname
    '                                returnmessage = traktResponse1.Error & " | " & traktResponse1.Message
    '                                logger.Info("[" & traktlist.Name & "] " & "Send list to trakt.tv FAILED! " & returnmessage)
    '                            End If
    '                        Else
    '                            traktlist.Slug = slugname
    '                            logger.Info("[" & traktlist.Name & "] " & "Send list to trakt.tv FAILED!")
    '                        End If
    '                    Else
    '                        logger.Info("List without name can't be send to trakt.tv!")
    '                    End If
    '                Next
    '            Else
    '                logger.Info("Missing authentification informaion for trakt.tv - no scraping possible!")
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' </remarks>
    '    ''' <summary>
    '    ''' Save trakt.tv list configuration to Ember database/Nfo of movies
    '    ''' </summary>
    '    ''' <param name="sender">"Save list to database/Nfo"-Button in Form</param>
    '    ''' <remarks>
    '    ''' TODO needs to be adapted to TAG instead of SETs!!!!!!!!!!!!!!!!!!!!!!!!!!!
    '    '''  This sub  handles saving of trakt.tv listconfiguration into tag-strcuture of movies
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    ''' 
    '    Private Sub btntraktListsSyncLibrary_Click(sender As Object, e As EventArgs) Handles btntraktListsSyncLibrary.Click
    '        Try
    '            Dim listDBID As String = ""
    '            Dim listdescription As String = ""
    '            Dim listname As String = ""
    '            'Contents of trakt.traklist will be written to Ember database/NFO
    '            For Each traktlist In traktLists
    '                listdescription = ""
    '                listname = ""
    '                listDBID = ""
    '                'Check if information is stored...
    '                If Not traktlist.Name Is Nothing Then

    '                    'Step 1: First retrieve important list information (name,description,ID (if already exists)) before creating/editing existing tag/list of EmberDB
    '                    listname = traktlist.Name
    '                    'check if there's any description, if yes then use trakt.tv description for plot/list description for Ember
    '                    If Not traktlist.Name Is Nothing Then
    '                        listdescription = traktlist.Description
    '                    End If
    '                    'go through each Ember DBList/Tag and check if current traktlist is already in Ember DB. Get TagID of tag if thats the case
    '                    For Each sRow As DataRow In dtMovieTags.Rows
    '                        If Not String.IsNullOrEmpty(sRow.Item("ListTitle").ToString) AndAlso sRow.Item("ListTitle").ToString = listname Then
    '                            listDBID = sRow.Item("ID").ToString
    '                            Exit For
    '                        End If
    '                    Next

    '                    'Step 2: create new DBTag object to store current trakt list in
    '                    Dim currMovieTag As New Structures.DBMovieSet
    '                    If String.IsNullOrEmpty(listDBID) Then
    '                        'if tag is new and doesn't exist in Ember, create new one with basic information!
    '                        currMovieTag.MovieSet = New MediaContainers.MovieSet
    '                        currMovieTag.ID = -1
    '                        currMovieTag.MovieSet.Title = listname
    '                        currMovieTag.MovieSet.Plot = listdescription
    '                    Else
    '                        'tag already in DB, just edit it! 
    '                        'load tag from database
    '                        currMovieTag = Master.DB.LoadMovieSetFromDB(CLng(listDBID))
    '                        '..and update description!
    '                        currMovieTag.MovieSet.Plot = listdescription
    '                        'delete existing movies from tag (we will add again in next step!)
    '                        If Not IsNothing(currMovieTag.Movies) AndAlso currMovieTag.Movies.Count > 0 Then
    '                            'clear all movies!
    '                            currMovieTag.Movies.Clear()
    '                        End If
    '                    End If

    '                    'Step 3: go through each movie in current traktlist and add movie to list
    '                    For Each listmovie In traktlist.Items
    '                        'If movie is part of DB in Ember (compare IMDB) add it - else ignore!
    '                        For Each sRow As DataRow In dtMovies.Rows
    '                            If Not String.IsNullOrEmpty(sRow.Item("ID").ToString) AndAlso sRow.Item("IMDBID").ToString = listmovie.ImdbId Then
    '                                Dim tmpMovie As New Structures.DBMovie
    '                                tmpMovie = Master.DB.LoadMovieFromDB(sRow.Item("ID").ToString)
    '                                currMovieTag.Movies.Add(tmpMovie)
    '                                Exit For
    '                            End If
    '                        Next
    '                    Next

    '                    'Step 4: save tag to DB
    '                    If String.IsNullOrEmpty(listDBID) = False Then
    '                        Master.DB.SaveMovieSetToDB(currMovieTag, False, False, True, True)
    '                    Else
    '                        Master.DB.SaveMovieSetToDB(currMovieTag, True, False, True, True)
    '                    End If
    '                Else
    '                    'no name !
    '                End If
    '            Next
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Converts traktuserlist to generic traktlist
    '    ''' </summary>
    '    ''' <param name="userlist">userlist from trakt.tv</param>
    '    ''' <returns>TraktList</returns>
    '    ''' <remarks>
    '    '''  Since POST methods of Trakt.API expects lists of type "traktlist" instead of "traktuserlist", we convert scraped userlists to traktlists - maybe move this code to wrapper class?!
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Function ConvertTraktlist(ByVal userlist As TraktAPI.Model.TraktUserList) As TraktAPI.Model.TraktList
    '        Try
    '            Dim traktlist As New TraktAPI.Model.TraktList
    '            Dim desc As String = userlist.Description
    '            Dim name As String = userlist.Name
    '            Dim privacy As String = userlist.Privacy
    '            Dim slug As String = userlist.Slug
    '            Dim allowshouts As Boolean = userlist.AllowShouts
    '            Dim shownumbers As Boolean = userlist.ShowNumbers

    '            traktlist.Description = desc
    '            traktlist.Name = name
    '            traktlist.Privacy = privacy
    '            traktlist.Slug = slug
    '            traktlist.AllowShouts = allowshouts
    '            traktlist.ShowNumbers = shownumbers
    '            traktlist.Password = StringUtils.EncryptToSHA1(traktPassword)
    '            traktlist.UserName = traktUser

    '            Dim traktlistitems As New List(Of TraktAPI.Model.TraktListItem)
    '            For Each element In userlist.Items
    '                Dim traktlistitem As New TraktAPI.Model.TraktListItem
    '                traktlistitem.Type = "movie"
    '                Dim year As Integer = CInt(element.Movie.Year)
    '                Dim imdbid As String = element.Movie.IMDBID
    '                Dim title As String = element.Movie.Title
    '                traktlistitem.Year = year
    '                traktlistitem.ImdbId = imdbid
    '                traktlistitem.Title = title
    '                traktlistitem.Episode = 0
    '                traktlistitem.Season = 0
    '                traktlistitem.TvdbId = ""
    '                traktlistitems.Add(traktlistitem)
    '            Next
    '            traktlist.Items = traktlistitems
    '            Return traktlist
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return Nothing
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Remove selected movie(s) from selected list
    '    ''' </summary>
    '    ''' <param name="sender">"Remove Movie"-Button in Form</param>
    '    ''' <remarks>
    '    '''  'Removing a movie from a list means updating "traktLists" and "currList" - then refresh view of listbox - multiselect in listbox is supported
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsRemove_Click(sender As Object, e As EventArgs) Handles btntraktListsRemove.Click
    '        Try
    '            If Me.lbtraktListsMoviesinLists.SelectedItems.Count > 0 Then
    '                For Each selectedmovie In Me.lbtraktListsMoviesinLists.SelectedItems
    '                    'update globallist
    '                    For Each list In traktLists
    '                        If list.Name = Me.lbtraktLists.SelectedItem.ToString Then
    '                            For Each movie In list.Items
    '                                If movie.Title = selectedmovie.ToString Then
    '                                    list.Items.Remove(movie)
    '                                    Exit For
    '                                End If
    '                            Next
    '                            Exit For
    '                        End If
    '                    Next

    '                Next
    '                Me.LoadSelectedList()
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub


    '    ''' <summary>
    '    ''' Add selected movie to selected list
    '    ''' </summary>
    '    ''' <param name="sender">"Add Movie"-Button in Form</param>
    '    ''' <remarks>
    '    '''  'Adding a movie to list means updating "traktLists" and "currList" - then refresh view of listbox 
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsAddMovie_Click(sender As Object, e As EventArgs) Handles btntraktListsAddMovie.Click
    '        Try
    '            If Me.dgvMovies.SelectedRows.Count > 0 Then
    '                For Each sRow As DataGridViewRow In Me.dgvMovies.SelectedRows
    '                    Dim tmpMovie As New Structures.DBMovie
    '                    tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(sRow.Cells(0).Value))
    '                    If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) AndAlso Not Me.lbtraktListsMoviesinLists.Items.Contains(tmpMovie.Movie.Title) Then
    '                        'create new traktlistitem of selected movie
    '                        Dim newTraktListItem As New TraktAPI.Model.TraktListItem
    '                        newTraktListItem = createTraktListItem(tmpMovie)
    '                        'add new movie to list (global & selectedlist)
    '                        If Not newTraktListItem Is Nothing Then
    '                            For Each movieinlist In traktLists
    '                                If movieinlist.Name = Me.lbtraktLists.SelectedItem.ToString Then
    '                                    If Not movieinlist.Items Is Nothing Then
    '                                        movieinlist.Items.Add(newTraktListItem)
    '                                        Exit For
    '                                    Else
    '                                        logger.Info("[" & movieinlist.Name & "] " & tmpMovie.Movie.Title & " is null! Error when trying to add movie to list!")
    '                                    End If
    '                                End If
    '                            Next

    '                            'don't remove added movie from avalaible movielist - movie can be part of multiple lists!
    '                            'Me.lbtraktListstMovies.Items.Remove(lbtraktListstMovies.SelectedItems(0))
    '                            ' bsMovies.Remove(sRow.DataBoundItem)
    '                        Else
    '                            logger.Info("[" & Me.lbtraktLists.SelectedItem.ToString & "] " & tmpMovie.Movie.Title & " Created Listitem is invalid! Error when trying to add movie to list!")
    '                        End If

    '                    End If
    '                Next

    '                Me.dgvMovies.ClearSelection()
    '                Me.dgvMovies.CurrentCell = Nothing
    '                Me.LoadSelectedList()
    '            End If

    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Create valid traktlistitem from movie container that can be added to existing trakt.tv list
    '    ''' </summary>
    '    ''' <param name="DBMovie">Movie to be scraped</param>
    '    ''' <returns>New and valid traktlistitem which can be added to traktlist or nothing when valid item could not be created</returns>
    '    ''' <remarks>
    '    '''  'Each item needs a type and enough details to identify the item being added. If an item can't be found, it will be ignored. See the example below for each type of item that can be added.
    '    '''  "type": "movie",
    '    '''  "imdb_id": "tt0372784",
    '    '''   "title": "Batman Begins",
    '    '''   "year": 2005
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Function createTraktListItem(ByVal DBMovie As Structures.DBMovie) As TraktAPI.Model.TraktListItem

    '        Try
    '            Dim traktlistitem As New TraktAPI.Model.TraktListItem
    '            'now set necessary properties of new traktitem to create a valid list entry

    '            'type
    '            traktlistitem.Type = "movie"
    '            'title
    '            If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
    '                traktlistitem.Title = DBMovie.Movie.Title
    '            Else
    '                Return Nothing
    '            End If
    '            'Imdbid
    '            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso IsNumeric(DBMovie.Movie.IMDBID) Then
    '                If Not DBMovie.Movie.IMDBID.StartsWith("tt") Then
    '                    traktlistitem.ImdbId = "tt" & DBMovie.Movie.IMDBID
    '                Else
    '                    traktlistitem.ImdbId = DBMovie.Movie.IMDBID
    '                End If
    '            Else
    '                Return Nothing
    '            End If
    '            'year
    '            If Not String.IsNullOrEmpty(DBMovie.Movie.Year) AndAlso IsNumeric(DBMovie.Movie.Year) Then
    '                traktlistitem.Year = CInt(DBMovie.Movie.Year)
    '            Else
    '                Return Nothing
    '            End If
    '            Return traktlistitem
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return Nothing
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Create valid traktlistitem from movie container that can be added to existing trakt.tv list
    '    ''' </summary>
    '    ''' <param name="DBMovie">Movie to be scraped</param>
    '    ''' <returns>New and valid traktlistitem which can be added to traktlist or nothing when valid item could not be created</returns>
    '    ''' <remarks>
    '    '''  'Each item needs a type and enough details to identify the item being added. If an item can't be found, it will be ignored. See the example below for each type of item that can be added.
    '    '''  "type": "movie",
    '    '''  "imdb_id": "tt0372784",
    '    '''   "title": "Batman Begins",
    '    '''   "year": 2005
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Function createTraktListItem(ByVal DBMovie As DataRow) As TraktAPI.Model.TraktListItem
    '        Try
    '            Dim traktlistitem As New TraktAPI.Model.TraktListItem
    '            'now set necessary properties of new traktitem to create a valid list entry

    '            'type
    '            traktlistitem.Type = "movie"
    '            'title
    '            If Not String.IsNullOrEmpty(DBMovie.Item("Title").ToString) Then
    '                traktlistitem.Title = DBMovie.Item("Title").ToString
    '            Else
    '                Return Nothing
    '            End If
    '            'Imdbid
    '            If Not String.IsNullOrEmpty(DBMovie.Item("IMDBID").ToString) AndAlso IsNumeric(DBMovie.Item("IMDBID").ToString) Then
    '                If Not DBMovie.Item("IMDBID").ToString.StartsWith("tt") Then
    '                    traktlistitem.ImdbId = "tt" & DBMovie.Item("IMDBID").ToString
    '                Else
    '                    traktlistitem.ImdbId = DBMovie.Item("IMDBID").ToString
    '                End If
    '            Else
    '                Return Nothing
    '            End If
    '            'year
    '            If Not String.IsNullOrEmpty(DBMovie.Item("Year").ToString) AndAlso IsNumeric(DBMovie.Item("Year").ToString) Then
    '                traktlistitem.Year = CInt(DBMovie.Item("Year").ToString)
    '            Else
    '                Return Nothing
    '            End If
    '            Return traktlistitem
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return Nothing
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Add corresponding movies of selected list to movielistbox 
    '    ''' </summary>
    '    ''' <param name="sender">lbtraktLists Changed Event</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub lbtraktLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbtraktLists.SelectedIndexChanged
    '        Try

    '            'clear movieinlist -listbox since we select another one
    '            Me.lbtraktListsMoviesinLists.Items.Clear()

    '            'check if list was selected
    '            If Me.lbtraktLists.SelectedItems.Count > 0 Then
    '                gbtraktListsDetails.Enabled = True

    '                'display listtitle in label
    '                Me.lbltraktListsCurrentList.Text = Me.lbtraktLists.SelectedItem.ToString
    '                Dim foundlist As Boolean = False
    '                'search selected list in globalist
    '                For Each movieinlist In traktLists
    '                    If movieinlist.Name = Me.lbtraktLists.SelectedItem.ToString Then
    '                        If movieinlist.Items.Count > 0 Then
    '                            'add all movies from currlist into listbox
    '                            Me.LoadSelectedList()
    '                        End If
    '                        'Enable remove/edit list buttons
    '                        Me.btntraktListsEditList.Enabled = True
    '                        Me.btntraktListsRemoveList.Enabled = True
    '                        Me.btntraktListsAddMovie.Enabled = True
    '                        txttraktListsEditList.Enabled = True
    '                        txttraktListsEditList.Text = Me.lbtraktLists.SelectedItem.ToString
    '                        foundlist = True
    '                        Exit For
    '                    End If
    '                Next

    '                If foundlist = False Then
    '                    logger.Info("[" & Me.lbtraktLists.SelectedItem.ToString & "] No list selected!")
    '                    Me.lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
    '                    Me.btntraktListsEditList.Enabled = False
    '                    Me.btntraktListsRemoveList.Enabled = False
    '                    Me.btntraktListsAddMovie.Enabled = False
    '                    Me.btntraktListsRemove.Enabled = False
    '                    txttraktListsEditList.Text = ""
    '                End If

    '                ' no list selected, disable remove/edit list buttons, reset label
    '            Else
    '                Me.lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
    '                Me.btntraktListsEditList.Enabled = False
    '                Me.btntraktListsRemoveList.Enabled = False
    '                Me.btntraktListsAddMovie.Enabled = False
    '                Me.btntraktListsRemove.Enabled = False
    '                txttraktListsEditList.Text = ""
    '                gbtraktListsDetails.Enabled = False
    '            End If

    '            If Me.lbtraktLists.Items.Count = 0 Then
    '                Me.btntraktListsSyncTrakt.Enabled = False
    '            Else
    '                Me.btntraktListsSyncTrakt.Enabled = True
    '            End If


    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub
    '    ''' <summary>
    '    ''' Copy selected userlist to another one - no correlation between source and copy!
    '    ''' </summary>
    '    ''' <param name="userlist">userlist to make a copy from</param>
    '    ''' <returns>Copy of inputlist, if error occurs nothing will be returned</returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Function Copylist(ByVal userlist As TraktAPI.Model.TraktList) As TraktAPI.Model.TraktList
    '        Try
    '            Dim traktlist As New TraktAPI.Model.TraktList
    '            Dim desc As String = userlist.Description
    '            Dim name As String = userlist.Name
    '            Dim privacy As String = userlist.Privacy
    '            Dim slug As String = userlist.Slug
    '            Dim allowshouts As Boolean = userlist.AllowShouts
    '            Dim shownumbers As Boolean = userlist.ShowNumbers

    '            traktlist.Description = desc
    '            traktlist.Name = name
    '            traktlist.Privacy = privacy
    '            traktlist.Slug = slug
    '            traktlist.AllowShouts = allowshouts
    '            traktlist.ShowNumbers = shownumbers
    '            traktlist.Password = StringUtils.EncryptToSHA1(traktPassword)
    '            traktlist.UserName = traktUser

    '            Dim traktlistitems As New List(Of TraktAPI.Model.TraktListItem)
    '            For Each element In userlist.Items
    '                Dim traktlistitem As New TraktAPI.Model.TraktListItem
    '                traktlistitem.Type = "movie"
    '                Dim year As Integer = element.Year
    '                Dim imdbid As String = element.ImdbId
    '                Dim title As String = element.Title
    '                traktlistitem.Year = year
    '                traktlistitem.ImdbId = imdbid
    '                traktlistitem.Title = title
    '                traktlistitem.Episode = 0
    '                traktlistitem.Season = 0
    '                traktlistitem.TvdbId = ""
    '                traktlistitems.Add(traktlistitem)
    '            Next
    '            traktlist.Items = traktlistitems
    '            Return traktlist
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return Nothing
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Add corresponding movies of selected list to movielistbox 
    '    ''' </summary>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub LoadSelectedList()
    '        Try
    '            Me.lbtraktListsMoviesinLists.SuspendLayout()

    '            Me.lbtraktListsMoviesinLists.Items.Clear()

    '            For Each movielist In traktLists
    '                If movielist.Name = Me.lbtraktLists.SelectedItem.ToString Then

    '                    'Fill Detailview
    '                    txttraktListsDetailsName.Text = movielist.Name
    '                    txttraktListsDetailsDescription.Text = movielist.Description
    '                    chkltraktListsDetailsComments.Checked = movielist.AllowShouts
    '                    chktraktListsDetailsNumbers.Checked = movielist.ShowNumbers
    '                    Select Case movielist.Privacy
    '                        Case "public"
    '                            cbotraktListsDetailsPrivacy.SelectedIndex = 0
    '                        Case "friends"
    '                            cbotraktListsDetailsPrivacy.SelectedIndex = 1
    '                        Case "private"
    '                            cbotraktListsDetailsPrivacy.SelectedIndex = 2
    '                    End Select
    '                    For Each tMovie In movielist.Items
    '                        Me.btntraktListsRemove.Enabled = True
    '                        Me.lbtraktListsMoviesinLists.Items.Add(tMovie.Title)
    '                    Next
    '                    Exit For
    '                End If
    '            Next

    '            Me.lbtraktListsMoviesinLists.ResumeLayout()

    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Delete selected list
    '    ''' </summary>
    '    ''' <param name="sender">"Remove List" Button of Form</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsRemoveList_Click(sender As Object, e As EventArgs) Handles btntraktListsRemoveList.Click
    '        Try
    '            If Me.lbtraktLists.SelectedItems.Count > 0 Then
    '                Dim strList As String = Me.lbtraktLists.SelectedItem.ToString
    '                For Each _list In traktLists
    '                    If _list.Name = strList Then
    '                        traktLists.Remove(_list)
    '                        Exit For
    '                    End If
    '                Next
    '                Me.LoadLists()
    '            End If

    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub


    '    ''' <summary>
    '    ''' Change name of existing/selected list
    '    ''' </summary>
    '    ''' <param name="sender">"Edit List" Button of Form</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsEditList_Click(sender As Object, e As EventArgs) Handles btntraktListsEditList.Click
    '        Try
    '            'check if list is selected (we need one to edit)
    '            If Me.lbtraktLists.SelectedItems.Count > 0 Then
    '                'currentlistname
    '                Dim strList As String = Me.lbtraktLists.SelectedItem.ToString
    '                'newlistname (from textbox)
    '                Dim newListname As String = Me.txttraktListsEditList.Text
    '                'only update if both names(old and new) are avalaible, also don't edit if newname is already a used listname
    '                If Not String.IsNullOrEmpty(strList) AndAlso Not String.IsNullOrEmpty(newListname) AndAlso Not Me.lbtraktLists.Items.Contains(newListname) Then
    '                    'update listname in globallist
    '                    For Each _list In traktLists
    '                        If _list.Name = strList Then
    '                            _list.Name = newListname
    '                            Exit For
    '                        End If
    '                    Next
    '                    'since globallist is updated, we need to load globallist again to reflect changes
    '                    Me.LoadLists()
    '                End If
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Add empty list
    '    ''' </summary>
    '    ''' <param name="sender">"New List" Button of Form</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsNewList_Click(sender As Object, e As EventArgs) Handles btntraktListsNewList.Click
    '        Try
    '            Dim newList As New TraktAPI.Model.TraktList
    '            'create new list
    '            newList = CreateNewEmberlist("NewList", "Created by EmberManager")
    '            If Not newList Is Nothing Then
    '                'add created list to existing globallist
    '                traktLists.Add(newList)
    '                'since globalist is updated, we need to load globallist again to reflect changes
    '                LoadLists()
    '            Else
    '                logger.Info("New list could not be created!")
    '            End If

    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Transfer selected list of database to traktlists by creating newlist or editing existing one
    '    ''' </summary>
    '    ''' <param name="sender">Transfer to Traktlist Button</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/24 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsGetDatabase_Click(sender As Object, e As EventArgs) Handles btntraktListsGetDatabase.Click


    '        'check if list was selected
    '        If Me.lbDBLists.SelectedItems.Count > 0 Then
    '            Try

    '                'Search for DBList in trakt.tv lists
    '                Dim newList As New TraktAPI.Model.TraktList
    '                'create new list
    '                newList = CreateNewEmberlist(Me.lbDBLists.SelectedItem.ToString, "Created by EmberManager")
    '                If Not newList Is Nothing Then
    '                    'check if listname of DBList also exist on traktlist!
    '                    For Each _list In traktLists
    '                        If _list.Name = Me.lbDBLists.SelectedItem.ToString Then
    '                            newList = _list
    '                            Exit For
    '                        End If
    '                    Next


    '                    'TODO Has to be changed fot TAG!!!
    '                    'go through each movie and look if it's part of selected list - if thats the case then create traktlistitem and add to newlist
    '                    Dim TagID As String = ""
    '                    For Each sRow As DataRow In dtMovieTags.Rows
    '                        If sRow.Item("ListTitle").ToString = Me.lbDBLists.SelectedItem.ToString Then
    '                            TagID = sRow.Item("ID").ToString
    '                            Exit For
    '                        End If
    '                    Next
    '                    If Not String.IsNullOrEmpty(TagID) Then
    '                        Using SQLcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
    '                            Dim tmpMovie As New Structures.DBMovie
    '                            Dim iProg As Integer = 0
    '                            SQLcommand.CommandText = String.Concat("SELECT MovieID, SetID, SetOrder FROM MoviesSets WHERE SetID = ", TagID, " ORDER BY SetOrder ASC;")
    '                            Using SQLreader As SQLite.SQLiteDataReader = SQLcommand.ExecuteReader()
    '                                If SQLreader.HasRows Then
    '                                    While SQLreader.Read()
    '                                        tmpMovie = Master.DB.LoadMovieFromDB(Convert.ToInt64(SQLreader("MovieID")))
    '                                        If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
    '                                            'create new traktlistitem of selected movie
    '                                            Dim newTraktListItem As New TraktAPI.Model.TraktListItem
    '                                            newTraktListItem = createTraktListItem(tmpMovie)
    '                                            'add new movie to list
    '                                            If Not newTraktListItem Is Nothing Then
    '                                                Dim addmovietolist As Boolean = True
    '                                                For Each movie In newList.Items
    '                                                    If movie.Title = tmpMovie.Movie.Title Then
    '                                                        addmovietolist = False
    '                                                        Exit For
    '                                                    End If
    '                                                Next
    '                                                If addmovietolist = True Then
    '                                                    newList.Items.Add(newTraktListItem)
    '                                                End If
    '                                            Else
    '                                                logger.Info("[" & Me.lbDBLists.SelectedItem.ToString & "] " & tmpMovie.Movie.Title & " Created Listitem is invalid! Error when trying to add movie to list!")
    '                                            End If
    '                                        End If
    '                                    End While
    '                                End If
    '                            End Using
    '                        End Using
    '                    End If

    '                    'add created list to existing globallist, remove if already exist to avoid duplicate
    '                    For Each _list In traktLists
    '                        If _list.Name = Me.lbDBLists.SelectedItem.ToString Then
    '                            traktLists.Remove(_list)
    '                            Exit For
    '                        End If
    '                    Next

    '                    traktLists.Add(newList)
    '                    'since globalist is updated, we need to load globallist again to reflect changes
    '                    LoadLists()
    '                Else
    '                    logger.Info("New list could not be created!")
    '                End If

    '            Catch ex As Exception
    '                logger.Error(New StackFrame().GetMethod().Name, ex)
    '            End Try
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Update list details
    '    ''' </summary>
    '    ''' <param name="sender">"Update list" Button</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsDetailsUpdate_Click(sender As Object, e As EventArgs) Handles btntraktListsDetailsUpdate.Click
    '        'check if one list is selected
    '        If Me.lbtraktLists.SelectedItems.Count > 0 Then
    '            Try
    '                For Each movielist In traktLists
    '                    If movielist.Name = Me.lbtraktLists.SelectedItem.ToString Then
    '                        'update listdetails
    '                        movielist.Description = txttraktListsDetailsDescription.Text
    '                        movielist.AllowShouts = chkltraktListsDetailsComments.Checked
    '                        movielist.ShowNumbers = chktraktListsDetailsNumbers.Checked
    '                        Select Case cbotraktListsDetailsPrivacy.SelectedIndex
    '                            Case 0
    '                                movielist.Privacy = "public"
    '                            Case 1
    '                                movielist.Privacy = "friends"
    '                            Case 2
    '                                movielist.Privacy = "private"
    '                        End Select
    '                        Exit For
    '                    End If
    '                Next
    '            Catch ex As Exception
    '                logger.Error(New StackFrame().GetMethod().Name, ex)
    '            End Try
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Create empty Trakt.tv list
    '    ''' </summary>
    '    ''' <returns>New TraktList with no items(movies)</returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' Important values for new list, i.e of trakt.tv documentation (New lists have no slug information!):
    '    '"username": "username",
    '    '"password": "sha1hash",
    '    '"name": "Top 10 of 2011" (The list name. This must be unique.)
    '    '"description": "These movies and shows really defined 2011 for me." (Optional but recommended description of what the list contains.)
    '    '"privacy": "public" (Privacy level, set it to private, friends, or public.)
    '    '"show_numbers": true (optional, The list should show numbers for each item. This is useful for ranked lists. Set it to true or false.)
    '    '"allow_shouts":true (optional, The list allows discussion by users who have access. Set it to true or false)
    '    ''' </remarks>
    '    Private Function CreateNewEmberlist(ByVal listname As String, ByVal listdescription As String, Optional ByVal listprivacy As String = "private") As TraktAPI.Model.TraktList
    '        Try
    '            Dim newtraktlist As New TraktAPI.Model.TraktList

    '            'check if List already exists!
    '            For Each _list In traktLists
    '                If _list.Name = listname Then
    '                    Return Nothing
    '                End If
    '            Next

    '            Dim listslug As String = listname.ToLower.Replace(" ", "-")
    '            Dim listallowshouts As Boolean = False
    '            Dim listshownumbers As Boolean = False

    '            newtraktlist.Description = listdescription
    '            newtraktlist.Name = listname
    '            newtraktlist.Privacy = listprivacy
    '            newtraktlist.Slug = listslug
    '            newtraktlist.AllowShouts = listallowshouts
    '            newtraktlist.ShowNumbers = listshownumbers
    '            newtraktlist.Password = StringUtils.EncryptToSHA1(traktPassword)
    '            newtraktlist.UserName = traktUser

    '            Dim traktlistitems As New List(Of TraktAPI.Model.TraktListItem)
    '            newtraktlist.Items = traktlistitems
    '            Return newtraktlist
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return Nothing
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Add all lists from globallist to listbox
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub LoadLists()
    '        Try
    '            Me.lbtraktLists.SuspendLayout()

    '            'first clear all listboxes before adding information again
    '            Me.lbtraktLists.Items.Clear()
    '            Me.lbtraktListsMoviesinLists.Items.Clear()
    '            Me.lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
    '            'add lists to listbox
    '            For Each tmptraktlist In traktLists
    '                lbtraktLists.Items.Add(tmptraktlist.Name)
    '            Next
    '            Me.btntraktListsEditList.Enabled = False
    '            Me.btntraktListsRemoveList.Enabled = False
    '            Me.btntraktListsAddMovie.Enabled = False
    '            Me.btntraktListsRemove.Enabled = False
    '            txttraktListsEditList.Text = ""
    '            Me.lbtraktLists.ResumeLayout()
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    Private Sub tbptraktLists_Enter(sender As Object, e As EventArgs) Handles tbptraktListsSync.Enter
    '        Me.Activate()
    '    End Sub

    '    Private Sub lbDBLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbDBLists.SelectedIndexChanged
    '        If Me.lbDBLists.SelectedItems.Count > 0 Then
    '            Me.btntraktListsGetDatabase.Enabled = True
    '        Else
    '            Me.btntraktListsGetDatabase.Enabled = False
    '        End If
    '    End Sub

    '#End Region 'Trakt.tv Sync Lists/Tags

    '#Region "Trakt.tv Listviewer"
    '    ''' <summary>
    '    ''' Download popular trakttv lists information and display URL and name of list for user
    '    ''' </summary>
    '    ''' <param name="sender">"Fetch lists"-Button in Form</param>
    '    ''' <remarks>
    '    ''' For now there's no official API support for getting general popular lists on trakt.tv. Therefore download HTML of URl and use Regex to get information needed
    '    ''' 2014/10/31 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsfetch_Click(sender As Object, e As EventArgs) Handles btntraktListsfetch.Click

    '        Try
    '            'Check if lists need to be scraped -  maybe lists are loaded already?!
    '            If traktpopularlistTitle.Count = 0 AndAlso traktpopularlistURL.Count = 0 Then
    '                'Link to scrape list from
    '                Dim SearchURL As String = "http://trakt.tv/lists/personal/popular/weekly"

    '                Dim sHTTP As New HTTP
    '                Dim Html As String = sHTTP.DownloadData(SearchURL)
    '                sHTTP = Nothing

    '                If Not String.IsNullOrEmpty(Html) Then
    '                    'use regexmatched to extract information
    '                    'Example of list information in HTML:
    '                    '<div class="title-overflow"></div>
    '                    '<a href="/user/listr/lists/500-essential-cult-movies-the-ultimate-guide-by-jennifer-eiss">500 Essential Cult Movies: The Ultimate Guide By Jennifer Eiss</a>
    '                    '</h3>

    '                    Dim sPattern As String = "<div class=""title-overflow""></div>.*?href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
    '                    Dim sResult As MatchCollection = Regex.Matches(Html, sPattern, RegexOptions.Singleline)
    '                    For ctr As Integer = 0 To sResult.Count - 1
    '                        If Not String.IsNullOrEmpty(sResult.Item(ctr).Groups(2).Value) AndAlso Not String.IsNullOrEmpty(sResult.Item(ctr).Groups(1).Value) Then
    '                            traktpopularlistTitle.Add(sResult.Item(ctr).Groups(2).Value)
    '                            traktpopularlistURL.Add(sResult.Item(ctr).Groups(1).Value)
    '                            cbotraktListsfetch.Items.Add(sResult.Item(ctr).Groups(2).Value)
    '                        End If
    '                    Next
    '                Else
    '                    logger.Debug("[http://trakt.tv/lists/personal/popular/weekly] HTML could not be downloaded!")
    '                End If
    '            End If
    '            cbotraktListsfetch.Enabled = True

    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            cbotraktListsfetch.Enabled = False
    '        End Try

    '    End Sub
    '    ''' <summary>
    '    ''' Load selected traktlist into datagrid
    '    ''' </summary>
    '    ''' <param name="sender">"Load list" button of form</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' load userlist into datagrid
    '    ''' 2014/10/12 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub btntraktListsload_Click(sender As Object, e As EventArgs) Handles btntraktListsload.Click
    '        'reset controls and labels
    '        dgvtraktList.DataSource = Nothing
    '        dgvtraktList.Rows.Clear()
    '        chktraktListsCompare.Checked = False
    '        btntraktListsSaveList.Enabled = False
    '        btntraktListsSaveListCompare.Enabled = False
    '        chktraktListsCompare.Enabled = False
    '        lbltraktListsCount.Visible = False
    '        lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ": " & dgvtraktList.Rows.Count
    '        'display description of list
    '        lbltraktListsdescriptionloaded.Text = ""

    '        Try
    '            If Not String.IsNullOrEmpty(txttraktListsurl.Text) Then

    '                favoriteList = Nothing

    '                'Check if url is valid, i.e http://trakt.tv/user/codermike/lists/100-greatest-scifi-movies
    '                Dim tmp As String = txttraktListsurl.Text
    '                If tmp.IndexOf("user/") > 0 Then
    '                    tmp = tmp.Remove(0, tmp.IndexOf("user/") + 5)
    '                    Dim parts As String() = tmp.Split(New String() {"/"}, StringSplitOptions.None)
    '                    If parts.Length = 3 Then
    '                        favoriteList = TraktAPI.TrakttvAPI.GetUserList(parts(0), parts(2))
    '                    Else
    '                        logger.Info("[" & txttraktListsurl.Text & "] " & "Invalid URL!")
    '                    End If
    '                Else
    '                    logger.Info("[" & txttraktListsurl.Text & "] " & "Invalid URL!")
    '                End If


    '                'Check if current userlist is valid and contains required data
    '                If Not favoriteList Is Nothing AndAlso Not favoriteList.Name Is Nothing AndAlso Not favoriteList.Items Is Nothing Then
    '                    '  lbtraktLists.Items.Clear()

    '                    'fill rows
    '                    'we map to dgv manually
    '                    dgvtraktList.AutoGenerateColumns = False
    '                    Dim debugcount As Integer = favoriteList.Items.Count - 1
    '                    For i = favoriteList.Items.Count - 1 To 0 Step -1
    '                        debugcount = debugcount - 1
    '                        'if list contains movies (= not empty list)  check if there's only movies - Ember doesn't support episodes right now!
    '                        If Not favoriteList.Items(i).Type Is Nothing AndAlso favoriteList.Items(i).Type = "movie" AndAlso Not favoriteList.Items(i).Title Is Nothing AndAlso favoriteList.Items(i).Title <> "" AndAlso Not favoriteList.Items(i).ImdbId Is Nothing AndAlso favoriteList.Items(i).ImdbId <> "" Then
    '                            'valid movie!
    '                            dgvtraktList.Rows.Add(New Object() {favoriteList.Items(i).Title, favoriteList.Items(i).Year, favoriteList.Items(i).Movie.Ratings.Percentage, Strings.Join(favoriteList.Items(i).Movie.Genres.ToArray, "/").Trim, "http://www.imdb.com/title/" & favoriteList.Items(i).Movie.IMDBID, favoriteList.Items(i).Movie.Trailer})
    '                        Else
    '                            logger.Info("[" & txttraktListsurl.Text & "] Movie with Index: " & debugcount.ToString & " Invalid entry could not be added. Not a valid movie!")
    '                            favoriteList.Items.RemoveAt(i)
    '                        End If
    '                    Next

    '                    logger.Info("[" & txttraktListsurl.Text & "] " & "Userlist loaded!")

    '                    'after filling datagrid, enable controls
    '                    chktraktListsCompare.Checked = False
    '                    btntraktListsSaveList.Enabled = True
    '                    btntraktListsSaveListCompare.Enabled = True
    '                    chktraktListsCompare.Enabled = True
    '                    lbltraktListsCount.Visible = True
    '                    lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ": " & dgvtraktList.Rows.Count
    '                    'display description of list
    '                    lbltraktListsdescriptionloaded.Text = favoriteList.Description
    '                Else
    '                    'invalid list, may contains episodes, invalid movie entrys!
    '                    logger.Info("[" & txttraktListsurl.Text & "] " & "Invalid list! (episodes, invalid movie entries!)")
    '                End If

    '            Else
    '                logger.Info("Missing URL to list - no scraping possible!")
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Trigger creation of HTML page which list all movies of the traktlist
    '    ''' </summary>
    '    ''' <remarks>
    '    ''' 2014/10/24 Cocotus - First implementation
    '    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    '    ''' </remarks>
    '    Private Sub btntraktListsSaveList_Click(sender As Object, e As EventArgs) Handles btntraktListsSaveList.Click
    '        Try
    '            Dim HTMLPage As String = BuildHTML(favoriteList, False)
    '            If Not String.IsNullOrEmpty(HTMLPage) Then
    '                File.WriteAllText(Path.Combine(Master.TempPath, "index.html"), HTMLPage)
    '                Functions.Launch(Path.Combine(Master.TempPath, "index.html"), True)
    '            Else
    '                logger.Info("HTMLPage could not be created!")
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub
    '    ''' <summary>
    '    ''' Trigger creation of HTML page which list only unknown movies
    '    ''' </summary>
    '    ''' <returns>HTML page as string, if empty then there was an error during build process</returns>
    '    ''' <remarks>
    '    ''' 2014/10/24 Cocotus - First implementation
    '    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    '    ''' </remarks>
    '    Private Sub btntraktListsSaveListCompare_Click(sender As Object, e As EventArgs) Handles btntraktListsSaveListCompare.Click
    '        Try
    '            Dim HTMLPage As String = BuildHTML(favoriteList, True)
    '            If Not String.IsNullOrEmpty(HTMLPage) Then
    '                File.WriteAllText(Path.Combine(Master.TempPath, "index.html"), HTMLPage)
    '                Functions.Launch(Path.Combine(Master.TempPath, "index.html"), True)
    '            Else
    '                logger.Info("HTMLPage could not be created!")
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Create HTML page to display traktlistitems (movies)
    '    ''' </summary>
    '    ''' <param name="list">loaded trakttv list</param>
    '    ''' <param name="exportonlyunknownmovies">true=only movies which are not in Ember database will be used, false=all movies from list will be used</param>
    '    ''' <returns>HTML page as string, if empty then there was an error during build process</returns>
    '    ''' <remarks>
    '    ''' 2014/10/24 Cocotus - First implementation
    '    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    '    ''' </remarks>
    '    Private Function BuildHTML(ByVal list As TraktAPI.Model.TraktUserList, ByVal exportonlyunknownmovies As Boolean) As String
    '        Try
    '            Dim HTML As String = ""
    '            Dim HTMLHEADER As String = "<html><body><table><thead><tr><tr><th>" & Master.eLang.GetString(21, "Title") & "</th><th>" & Master.eLang.GetString(278, "Year") & "</th><th>" & Master.eLang.GetString(400, "Rating") & "</th><th>" & Master.eLang.GetString(20, "Genre") & "</th><th>" & Master.eLang.GetString(64, "Overview") & "</th><th>" & Master.eLang.GetString(885, "IMDB") & "</th><th>" & Master.eLang.GetString(151, "Trailer") & "</th></tr></thead>"
    '            Dim HTMLDATAROW As String = ""
    '            Dim HTMLFOOTER As String = "</table></body></html>"

    '            If exportonlyunknownmovies = True Then
    '                Dim foundmovieinlibrary As Boolean = False
    '                For Each item In favoriteList.Items
    '                    'search movie in global moviedatatable
    '                    For Each sRow As DataRow In dtMovies.Rows
    '                        If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                            If item.Movie.IMDBID = "tt" & sRow.Item("IMDB").ToString Then
    '                                foundmovieinlibrary = True
    '                                Exit For
    '                            End If
    '                        Else
    '                            logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be compared against library!")
    '                        End If
    '                    Next
    '                    If foundmovieinlibrary = False Then
    '                        If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                            HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Title & "</td><td>" & item.Year & "</td><td>" & item.Movie.Ratings.Percentage & "</td><td>" & Strings.Join(item.Movie.Genres.ToArray, "/").Trim & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.IMDBID & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
    '                        Else
    '                            logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be added to HTML!")
    '                        End If

    '                    Else
    '                        logger.Info("[" & item.Title & "] " & "Movie already in library!")
    '                    End If
    '                Next
    '            Else
    '                For Each item In list.Items
    '                    If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                        HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Title & "</td><td>" & item.Year & "</td><td>" & item.Movie.Ratings.Percentage & "</td><td>" & Strings.Join(item.Movie.Genres.ToArray, "/").Trim & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.IMDBID & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
    '                    Else
    '                        logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be added to HTML!")
    '                    End If
    '                Next
    '            End If
    '            HTML = HTMLHEADER & HTMLDATAROW & HTMLFOOTER
    '            Return HTML
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '            Return ""
    '        End Try
    '    End Function
    '    ''' <summary>
    '    ''' Update datagridview and show either all entries of loaded list or only unknown movies
    '    ''' </summary>
    '    ''' <param name="list">Checked state changed event of checkbox</param>
    '    ''' <returns></returns>
    '    ''' <remarks>
    '    ''' 2014/10/24 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub chktraktListsCompare_CheckedChanged(sender As Object, e As EventArgs) Handles chktraktListsCompare.CheckedChanged
    '        Try
    '            dgvtraktList.DataSource = Nothing
    '            dgvtraktList.Rows.Clear()

    '            If chktraktListsCompare.Checked = True Then
    '                Dim foundmovieinlibrary As Boolean = False

    '                For Each item In favoriteList.Items
    '                    'search movie in globalist
    '                    If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                        For Each sRow As DataRow In dtMovies.Rows
    '                            If item.Movie.IMDBID = "tt" & sRow.Item("IMDB").ToString Then
    '                                foundmovieinlibrary = True
    '                                Exit For
    '                            End If
    '                        Next
    '                    Else
    '                        logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be compared against library!")
    '                    End If

    '                    If foundmovieinlibrary = False Then
    '                        If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                            dgvtraktList.Rows.Add(New Object() {item.Title, item.Year, item.Movie.Ratings.Percentage, Strings.Join(item.Movie.Genres.ToArray, "/").Trim, "http://www.imdb.com/title/" & item.Movie.IMDBID, item.Movie.Trailer})
    '                        Else
    '                            logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be added to datagrid!")
    '                        End If
    '                    Else
    '                        logger.Info("[" & item.Title & "] " & "Movie already in library!")
    '                    End If
    '                Next
    '            Else
    '                'fill rows
    '                For Each item In favoriteList.Items
    '                    If Not item Is Nothing AndAlso Not item.Movie Is Nothing Then
    '                        dgvtraktList.Rows.Add(New Object() {item.Title, item.Year, item.Movie.Ratings.Percentage, Strings.Join(item.Movie.Genres.ToArray, "/").Trim, "http://www.imdb.com/title/" & item.Movie.IMDBID, item.Movie.Trailer})
    '                    Else
    '                        logger.Info("[" & item.Title & "] " & "Movie Item is Nothing! Could not be added to datagrid!")
    '                    End If
    '                Next
    '            End If

    '            lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ": " & dgvtraktList.Rows.Count
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Enable/Disable Loadlist button logic
    '    ''' </summary>
    '    ''' <param name="sender">Textchanged Event of URL textbox</param>
    '    ''' <remarks>
    '    ''' 2014/10/31 Cocotus - First implementation
    '    Private Sub txttraktListsurl_TextChanged(sender As Object, e As EventArgs) Handles txttraktListsurl.TextChanged
    '        If Not String.IsNullOrEmpty(txttraktListsurl.Text) Then
    '            btntraktListsload.Enabled = True
    '        Else
    '            btntraktListsload.Enabled = False
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Open link in datagrid using default browser
    '    ''' </summary>
    '    ''' <param name="sender">Cell click event</param>
    '    ''' <remarks>
    '    ''' 2014/10/31 Cocotus - First implementation
    '    Private Sub dgvtraktList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvtraktList.CellContentClick
    '        If e.RowIndex > -1 Then
    '            If dgvtraktList(e.ColumnIndex, e.RowIndex).Value.ToString().StartsWith("http") Then
    '                System.Diagnostics.Process.Start(dgvtraktList.CurrentCell.Value.ToString())
    '            End If
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Set URL of selected popular list into URL-Textbox
    '    ''' </summary>
    '    ''' <param name="sender">Combobox Index Changed Event</param>
    '    ''' <remarks>
    '    ''' 2014/10/31 Cocotus - First implementation
    '    ''' </remarks>
    '    Private Sub cbotraktListsfetch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotraktListsfetch.SelectedIndexChanged
    '        Try
    '            If Not String.IsNullOrEmpty(Me.cbotraktListsfetch.SelectedItem.ToString) Then
    '                txttraktListsurl.Text = traktpopularlistURL.Item(cbotraktListsfetch.SelectedIndex)
    '            End If
    '        Catch ex As Exception
    '            logger.Error(New StackFrame().GetMethod().Name, ex)
    '        End Try
    '    End Sub

    '#End Region 'Trakt.tv Listviewer

#End Region 'Methods


End Class

#Region "Obsolete Methods (replacedTVTrakt-Wrapper Calls, backgroundworker)"


'Private Sub StartLoading()
'    start backgroundworker, load movies in right list and fill listboxes! 
'    btnCancel.Visible = True
'    lblCompiling.Visible = True
'    prbCompile.Visible = True
'    prbCompile.Style = ProgressBarStyle.Continuous
'    lblCanceling.Visible = False
'    pnlCancel.Visible = True
'    Application.DoEvents()

'    Me.bwLoad.WorkerSupportsCancellation = True
'    Me.bwLoad.WorkerReportsProgress = True
'    Me.bwLoad.RunWorkerAsync()
'End Sub



'Private Sub bwLoadMovies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwLoadMovies.RunWorkerCompleted
'    '//
'    ' Thread finished: fill movie and sets lists
'    '\\

'    'Since trakt.tv userlist needs to be scraped and at this point its not guarented to be loaded - dont use that
'    '   Me.LoadLists()
'    Me.pnlCancel.Visible = False

'End Sub

'Private Sub bwLoadMovies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwLoadMovies.ProgressChanged
'    If e.ProgressPercentage >= 0 Then
'        Me.prbCompile.Value = e.ProgressPercentage
'        Me.lblFile.Text = e.UserState.ToString
'    Else
'        Me.prbCompile.Maximum = Convert.ToInt32(e.UserState)
'    End If
'End Sub


'Private Sub tbptraktLists_Leave(sender As Object, e As EventArgs) Handles tbptraktListsSync.Leave
'    If Me.bwLoadMovies.IsBusy Then
'        Me.DoCancel()
'        While Me.bwLoadMovies.IsBusy
'            Application.DoEvents()
'            Threading.Thread.Sleep(50)
'        End While
'    End If
'End Sub
'Private Sub DoCancel()
'    Me.bwLoadMovies.CancelAsync()
'    btnCancel.Visible = False
'    lblCompiling.Visible = False
'    prbCompile.Style = ProgressBarStyle.Marquee
'    prbCompile.MarqueeAnimationSpeed = 25
'    lblCanceling.Visible = True
'    lblFile.Visible = False
'End Sub

' ''' <summary>
' ''' cocotus 2013/02 Trakt.tv syncing: Movies
' ''' Connects with trakt.tv Website and gets Watched Movies from specific User and returns them in special Dictionary
' ''' More Info here: http://trakt.tv/api-docs/user-library-movies-watched
' ''' </summary>
' ''' <param name="traktID">Username</param>
' ''' <param name="traktPW">password</param>
' ''' <returns>3 values in dictionary: IMDBID (ex: tt0114746), Title, Playcount/Plays</returns>
'Public Shared Function GetWatchedMoviesFromTrakt(ByVal traktID As String, ByVal traktPW As String) As Dictionary(Of String, KeyValuePair(Of String, Integer))

'    Dim wc As New Net.WebClient
'    Try
'        'Saving 3 values in Dictionary style
'        Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, Integer))

'        'The REQUEST String (includes API-ID + UserID)
'        Dim URL As String = "http://api.trakt.tv/user/library/movies/watched.json/b59a24b6a3fb93fc2fb565a681bb8a1d/" & traktID

'        Dim json As String = wc.DownloadString(URL)
'        If Not String.IsNullOrEmpty(json) Then

'            'Now we are using free  3rd party class/dll to make an easy parse of the json String
'            Dim client = New RestSharp.RestClient(URL)
'            'added basic authentification, to get even protected user information
'            client.Authenticator = New RestSharp.HttpBasicAuthenticator(traktID, traktPW)
'            Dim request = New RestSharp.RestRequest(RestSharp.Method.[GET])
'            Dim response = client.Execute(Of List(Of TraktWatchedMovieData))(request)

'            If Not response Is Nothing Then
'                'Now loop through to every entry
'                For Each Item As TraktWatchedMovieData In response.Data
'                    'Check if information is stored...
'                    If Not Item.title Is Nothing AndAlso Item.title <> "" AndAlso Not Item.imdb_id Is Nothing AndAlso Item.imdb_id <> "" Then
'                        If Not dictMovieWatched.ContainsKey(Item.title) Then
'                            'Now store imdbid, title and playcount information into dictionary (for now no other info needed...)
'                            If Item.imdb_id.Length > 2 AndAlso Item.imdb_id.Substring(0, 2) = "tt" Then
'                                'IMDBID beginning with tt -> strip tt first and save only number!
'                                dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, Integer)(Item.imdb_id.Substring(2), CInt(Item.plays)))
'                            Else
'                                'IMDBID is alright
'                                dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, Integer)(Item.imdb_id, CInt(Item.plays)))
'                            End If
'                        End If
'                    End If
'                Next
'            End If
'        End If
'        wc.Dispose()
'        Return dictMovieWatched

'    Catch ex As Exception
'        wc.Dispose()
'        Return Nothing
'    End Try
'End Function

' ''' <summary>
' ''' cocotus 2013/03 Trakt.tv syncing: TV Shows
' ''' Connects with trakt.tv Website and gets Watched Episodes from specific User and returns them in a special Dictionary
' ''' More Info here: http://trakt.tv/api-docs/user-library-shows-watched
' ''' </summary>
' ''' <param name="traktID">Username</param>
' ''' <param name="traktPW">password</param>
' ''' <returns>3 values in dictionary: TvShowTitle, TVDBID, Special Season Class (Season + Watched Episodes in list)</returns>
'Public Shared Function GetWatchedEpisodesFromTrakt(ByVal traktID As String, ByVal traktPW As String) As Dictionary(Of String, KeyValuePair(Of String, List(Of SeasonClass)))

'    Dim wc As New Net.WebClient
'    Try
'        'Saving 3 values in Dictionary style
'        Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, List(Of SeasonClass)))

'        'The REQUEST String (includes API-ID + UserID)
'        Dim URL As String = "http://api.trakt.tv/user/library/shows/watched.json/b59a24b6a3fb93fc2fb565a681bb8a1d/" & traktID

'        Dim json As String = wc.DownloadString(URL)
'        If Not String.IsNullOrEmpty(json) Then

'            'Now we are using free  3rd party class/dll to make an easy parse of the json String
'            Dim client = New RestSharp.RestClient(URL)
'            'added basic authentification, to get even protected user information
'            client.Authenticator = New RestSharp.HttpBasicAuthenticator(traktID, traktPW)
'            Dim request = New RestSharp.RestRequest(RestSharp.Method.[GET])
'            Dim response = client.Execute(Of List(Of TraktWatchedEpisodeData))(request)

'            If Not response Is Nothing Then
'                'Now loop through to every entry
'                For Each Item As TraktWatchedEpisodeData In response.Data
'                    'Check if information is stored...
'                    If Not Item.title Is Nothing AndAlso Item.title <> "" AndAlso Not Item.tvdb_id Is Nothing AndAlso Item.tvdb_id <> "" Then
'                        If Not dictMovieWatched.ContainsKey(Item.title) Then
'                            'Now store tvdbID, title and the season-episode-list in dictionary...
'                            dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, List(Of SeasonClass))(Item.tvdb_id, Item.seasons))
'                        End If
'                    End If
'                Next
'            End If
'        End If
'        wc.Dispose()
'        Return dictMovieWatched

'    Catch ex As Exception
'        wc.Dispose()
'        Return Nothing
'    End Try
'End Function

#End Region

