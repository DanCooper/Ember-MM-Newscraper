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
Imports Trakttv
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class dlgTrakttvManager

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwSaveWatchedStateToEmber_Movies As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwSaveWatchedStateToEmber_TVEpisodes As New System.ComponentModel.BackgroundWorker

    Private _bGetShowProgress As Boolean
    Private _TraktAPI As clsAPITrakt

    Private _myWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched)
    Private _myRatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated)
    Private _myWatchedRatedMovies As New List(Of TraktAPI.Model.TraktMovieWatchedRated)

    Private _myWatchedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched)

    Private _myWatchedProgressTVShows As New List(Of TraktAPI.Model.TraktShowWatchedProgress)

    'datatable which contains all tags in Ember database
    Private dtMovieTags As New DataTable
    'datatable which contains all movies in Ember database
    Private dtMovies As New DataTable
    'datatable which contains all episodes in Ember database
    Private dtEpisodes As New DataTable

    'Tab: trakt.tv Sync Lists/Tags variables
    'reflects the current list collection which will be synced to trakt.tv
    Private traktLists As New List(Of TraktAPI.Model.TraktSyncList)
    'collection fo current tags in Ember database - will be modifified/updated during runtime of this module and then saved back to database
    Private alDBTags As New List(Of String)

    'Tab: trakt.tv Sync Watchlist variables
    'Helper: Saving 3 values in Dictionary style: TVDB, List of SeasonNumber|Episodenumber
    Private myWatchlistEpisodes As New Dictionary(Of String, List(Of KeyValuePair(Of Integer, List(Of TraktAPI.Model.TraktEpisodeWatched.Season.Episode))))
    Private myWatchlistMovies As New List(Of TraktAPI.Model.TraktMovieWatchList)

    'Tab: trakt.tv Comments variables
    Private myCommentsMovies As New List(Of TraktAPI.Model.TraktCommentItem)

    'Tab: trakt.tv Listviewer variables
    'fetched listnames
    Private userListTitle As New List(Of String)
    'fetched listURLs
    Private userListURL As New List(Of String)
    'scraped userlist from trakt.tv
    Private userList As New TraktAPI.Model.TraktListDetail
    'all lists from specific user
    Private userLists As New List(Of TraktAPI.Model.TraktListDetail)
    'scraped items of a userlist from trakt.tv
    Private userListItems As New List(Of TraktAPI.Model.TraktListItem)
    'binding for movie datagridview
    Private bsMovies As New BindingSource

    Private _SpecialSettings As New TraktInterface.KodiSettings

#End Region 'Fields

#Region "Constructors"

    Sub New(ByRef TraktAPI As clsAPITrakt, ByVal bGetShowProgress As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _bGetShowProgress = bGetShowProgress
        _TraktAPI = TraktAPI
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
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Sub SetUp()
        Try
            'if there's missing data we can't use any trakt.tv API calls -> block GUI
            If _TraktAPI Is Nothing OrElse _TraktAPI.Token Is Nothing Then tbTrakt.Enabled = False

            lblTopTitle.Text = Text
            Text = Master.eLang.GetString(871, "Trakt.tv Manager")
            OK_Button.Text = Master.eLang.GetString(19, "Close")
            tbptraktPlaycount.Text = Master.eLang.GetString(1303, "Sync Playcount")
            tbptraktListViewer.Text = Master.eLang.GetString(1304, "List Viewer")
            tbptraktListsSync.Text = Master.eLang.GetString(1305, "Sync Lists/Tags")
            lblCompiling.Text = Master.eLang.GetString(326, "Loading...")
            lblCanceling.Text = Master.eLang.GetString(370, "Canceling Load...")
            btnCancel.Text = Master.eLang.GetString(167, "Cancel")
            lblTopDetails.Text = Master.eLang.GetString(1306, "Sync lists and playcount with your trakt.tv account.")


            'Tab: Sync Playcount
            gbPlaycount.Text = Master.eLang.GetString(1303, "Sync Playcount")
            lblPlaycountDone.Visible = False
            prgPlaycount.Value = 0
            prgPlaycount.Maximum = _myWatchedRatedMovies.Count
            prgPlaycount.Minimum = 0
            prgPlaycount.Step = 1
            btnPlaycountGetList_Movies.Text = Master.eLang.GetString(779, "Get watched movies")
            btnSaveWatchedStateToEmber.Text = Master.eLang.GetString(780, "Save playcount to database/Nfo")
            btnPlaycountGetList_TVShows.Text = Master.eLang.GetString(781, "Get watched episodes")
            dgvPlaycount.DataSource = Nothing
            dgvPlaycount.Rows.Clear()
            colPlaycountTitle.HeaderText = Master.eLang.GetString(21, "Title")
            colPlaycountPlayed.HeaderText = Master.eLang.GetString(981, "Watched")
            colPlaycountRating.HeaderText = Master.eLang.GetString(400, "Rating")
            colPlaycountProgress.HeaderText = Master.eLang.GetString(1370, "Progress")
            colPlaycountLastWatched.HeaderText = Master.eLang.GetString(1369, "Last watched")
            btnPlaycountSyncRating.Text = Master.eLang.GetString(1371, "Submit ratings to trakt.tv")
            btnPlaycountSyncWatched_Movies.Text = Master.eLang.GetString(1405, "Submit watched movies to trakt.tv history")
            btnPlaycountSyncWatched_TVShows.Text = Master.eLang.GetString(1404, "Submit watched episodes to trakt.tv history")
            btnPlaycountSyncDeleteItem.Text = Master.eLang.GetString(1406, "Delete selected item(s) from trakt.tv history")

            'Tab: Sync Comments
            btntraktCommentsDetailsDelete.Text = Master.eLang.GetString(1410, "Delete comment from trakt.tv")
            btntraktCommentsDetailsUpdate.Text = Master.eLang.GetString(1409, "Update comment on trakt.tv")
            btntraktCommentsDetailsSend.Text = Master.eLang.GetString(1411, "Submit comment to trakt.tv")
            btntraktCommentsGet.Text = Master.eLang.GetString(1419, "Load your movie comments")
            lbltraktCommentsDetailsDate.Text = Master.eLang.GetString(601, "Adding Date")
            lbltraktCommentsDetailsDescription.Text = Master.eLang.GetString(1413, "Comment")
            lbltraktCommentsDetailsLikes.Text = Master.eLang.GetString(1416, "Likes")
            lbltraktCommentsDetailsRating.Text = Master.eLang.GetString(400, "Rating")
            lbltraktCommentsDetailsReplies.Text = Master.eLang.GetString(1415, "Replies")
            lbltraktCommentsDetailsType.Text = Master.eLang.GetString(1288, "Type")
            gbtraktCommentsList.Text = Master.eLang.GetString(1413, "Comment")
            gbtraktCommentsDetails.Text = Master.eLang.GetString(26, "Details")
            gbtraktCommentsGET.Text = Master.eLang.GetString(1421, "Load comments")
            gbtraktComments.Text = Master.eLang.GetString(1418, "Sync Comments")
            lbltraktCommentsNotice.Text = Master.eLang.GetString(1420, "!IMPORTANT RULES!\n\n\n\n1. Comments must be at least 5 words\n\n2. Comments 200 words or longer will be automatically marked as a review\n\n3. Correctly indicate if the comment contains spoilers\n\n4. Only write comments in English").Replace("\n", Environment.NewLine)
            coltraktCommentsMovie.HeaderText = Master.eLang.GetString(21, "Title")
            coltraktCommentsURL.HeaderText = Master.eLang.GetString(1323, "URL")
            coltraktCommentsReplies.HeaderText = Master.eLang.GetString(1415, "Replies")
            coltraktCommentsDate.HeaderText = Master.eLang.GetString(601, "Adding Date")

            'Tab: Sync Lists/Tags
            lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
            gbtraktListsGET.Text = Master.eLang.GetString(1307, "Load personal lists")
            gbtraktListsSYNC.Text = Master.eLang.GetString(1308, "Save personal lists")
            gbtraktLists.Text = Master.eLang.GetString(1309, "Personal trakt.tv Lists")
            gbtraktListsDetails.Text = Master.eLang.GetString(26, "Details")
            gbtraktListsMoviesInLists.Text = Master.eLang.GetString(1310, "Movies In List")
            gbtraktListsMovies.Text = Master.eLang.GetString(36, "Movies")
            btntraktListsGetDatabase.Text = Master.eLang.GetString(1311, "Add list to trakt.tv")
            btntraktListsGetPersonal.Text = Master.eLang.GetString(1312, "Load trakt.tv lists")
            btntraktListsSyncTrakt.Text = Master.eLang.GetString(1313, "Sync to trakt.tv")
            btntraktListsSaveToDatabase.Text = Master.eLang.GetString(1314, "Save tag to database/Nfo")
            btntraktListsDetailsUpdate.Text = Master.eLang.GetString(1315, "Update listdetails")
            lbltraktListsDetailsName.Text = Master.eLang.GetString(232, "Name")
            lbltraktListsDetailsDescription.Text = Master.eLang.GetString(979, "Description")
            chkltraktListsDetailsComments.Text = Master.eLang.GetString(1316, "Allow Comments")
            lbltraktListsDetailsPrivacy.Text = Master.eLang.GetString(1317, "Privacy Level")
            chktraktListsDetailsNumbers.Text = Master.eLang.GetString(1318, "Show Numbers")
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1319, "Public"))
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1320, "Friends"))
            cbotraktListsDetailsPrivacy.Items.Add(Master.eLang.GetString(1321, "Private"))
            lbltraktListsLink.Text = Master.eLang.GetString(1368, "Your trakt.tv dashboard")
            lbltraktListsNoticeSync.Text = Master.eLang.GetString(1367, "Edited existing list(s) will be saved with prefix NEWLIST_. Please change name of list in dashboard!")

            'Tab: List viewer
            lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ":"
            gbtraktListsViewer.Text = Master.eLang.GetString(1304, "List Viewer")
            lbltraktListsFavorites.Text = Master.eLang.GetString(1348, "Favorite lists") & ":"
            lbltraktListsScraped.Text = Master.eLang.GetString(1349, "Scraped lists") & ":"
            lbltraktListURL.Text = Master.eLang.GetString(1323, "URL:")
            btntraktListsGetPopular.Text = Master.eLang.GetString(1345, "Scrape popular lists")
            btntraktListsGetFollowers.Text = Master.eLang.GetString(1346, "Scrape lists of favorite users")
            btntraktListsGetFriends.Text = Master.eLang.GetString(1347, "Scrape lists of friends")
            btntraktListRemoveFavorite.Text = Master.eLang.GetString(1351, "Remove list from favorites")
            btntraktListSaveFavorite.Text = Master.eLang.GetString(1350, "Save list to favorites")
            btntraktListLoad.Text = Master.eLang.GetString(1325, "Load list")
            lbltraktListDescription.Text = Master.eLang.GetString(979, "Description")
            chktraktListsCompare.Text = Master.eLang.GetString(1326, "only show unknown movies")
            btntraktListsSaveList.Text = Master.eLang.GetString(1327, "Export complete list")
            btntraktListsSaveListCompare.Text = Master.eLang.GetString(1328, "Export unknown movies")
            coltraktListTitle.HeaderText = Master.eLang.GetString(21, "Title")
            coltraktListYear.HeaderText = Master.eLang.GetString(278, "Year")
            coltraktListGenres.HeaderText = Master.eLang.GetString(725, "Genres")
            coltraktListTrailer.HeaderText = Master.eLang.GetString(151, "Trailer")
            coltraktListRating.HeaderText = Master.eLang.GetString(400, "Rating")
            coltraktListIMDB.HeaderText = Master.eLang.GetString(1323, "URL")
            gbtraktListsViewerStep1.Text = Master.eLang.GetString(1352, "Step 1 (Optional): Load specific lists from trakt.tv")
            gbtraktListsViewerStep2.Text = Master.eLang.GetString(1353, "Step 2: Load selected list or type URL")
            btntraktListsSavePlaylist.Text = Master.eLang.GetString(1465, "Export to Kodi playlist")
            btntraktListsSendToKodi.Text = Master.eLang.GetString(1466, "Send list to Kodi")

            'Tab: Sync Watchlist 
            gbtraktWatchlist.Text = Master.eLang.GetString(1337, "Sync Watchlist")
            gbtraktWatchlistExpert.Text = Master.eLang.GetString(1175, "Optional Settings")
            lbltraktWatchliststate.Visible = False
            prgtraktWatchlist.Value = 0
            prgtraktWatchlist.Maximum = _myWatchedRatedMovies.Count
            prgtraktWatchlist.Minimum = 0
            prgtraktWatchlist.Step = 1
            btntraktWatchlistGetMovies.Text = Master.eLang.GetString(1338, "Get movies from trakt.tv watchlist")
            btntraktWatchlistSyncLibrary.Text = Master.eLang.GetString(1340, "Remove watched movies from trakt.tv watchlist")
            btntraktWatchlistGetSeries.Text = Master.eLang.GetString(1339, "Get episodes from trakt.tv watchlist")
            btntraktWatchlistClean.Text = Master.eLang.GetString(1341, "Clear trakt.tv watchlist")
            btntraktWatchlistSendEmberUnwatched.Text = Master.eLang.GetString(1342, "Send unwatched movies to trakt.tv watchlist")
            dgvtraktWatchlist.DataSource = Nothing
            dgvtraktWatchlist.Rows.Clear()
            coltraktWatchlistTitle.HeaderText = Master.eLang.GetString(21, "Title")
            coltraktWatchlistYear.HeaderText = Master.eLang.GetString(278, "Year")
            coltraktWatchlistListedAt.HeaderText = Master.eLang.GetString(601, "Date Added")
            coltraktWatchlistIMDB.HeaderText = Master.eLang.GetString(1323, "URL")

            'Tab: Cleaning
            gbtraktCleaning.Text = Master.eLang.GetString(1490, "Cleaning")
            gbtraktCleaningHistoryTimespan.Text = Master.eLang.GetString(1492, "Delete history for a particular timespan")
            gbtraktCleaningHistoryTimestamp.Text = Master.eLang.GetString(1491, "Delete history from a specific date")
            btntraktCleaningHistoryTimespan.Text = Master.eLang.GetString(1485, "Start cleaning movie history")
            btntraktCleaningHistoryTimestamp.Text = Master.eLang.GetString(1485, "Start cleaning movie history")
            lbltraktCleaningHistoryTimespan.Text = Master.eLang.GetString(1488, "Timespan [minutes]")
            lbltraktCleaningHistoryTimespanDesc.Text = Master.eLang.GetString(1487, "This will remove all plays in your watched movie history which were registered in a specific timespan (i.e. 3 plays for one movie within 5 minutes, will delete the 2 last plays)")
            lbltraktCleaningHistoryTimestampDesc.Text = Master.eLang.GetString(1486, "This will remove all plays in your watched movie history which were played on a specific time (i.e. 00:00:00)")
            lbltraktCleaningHistoryTimestamp.Text = Master.eLang.GetString(1489, "Timestamp [hh:mm:ss]")

            'load existing movies from database into datatable
            Master.DB.FillDataTable(dtMovies, String.Concat("SELECT * FROM movielist ",
                                                                "ORDER BY ListTitle COLLATE NOCASE;"))
            'load existing episodes from database into datatable
            Master.DB.FillDataTable(dtEpisodes, String.Concat("SELECT * FROM episodelist INNER JOIN tvshowlist ON (tvshowlist.idShow  = episodelist.idShow) WHERE Missing = 0;"))


            RemoveHandler cbotraktListsFavorites.SelectedIndexChanged, AddressOf cbotraktListsFavorites_SelectedIndexChanged
            cbotraktListsFavorites.Items.Clear()
            'Cocotus 2014/10/11 Automatically populate available videosources from user settings to sourcefilter instead of using hardcoded list here!
            Dim mylists As New List(Of AdvancedSettingsComplexSettingsTableItem)
            mylists = AdvancedSettings.GetComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
            If mylists IsNot Nothing Then
                cbotraktListsFavorites.Enabled = True
                For Each k In mylists
                    If Not cbotraktListsFavorites.Items.Contains(k.Value) Then
                        cbotraktListsFavorites.Items.Add(k.Name)
                    End If
                Next
                cbotraktListsFavorites.SelectedIndex = 0
                txttraktListURL.Text = mylists.Item(0).Value
            Else
                cbotraktListsFavorites.Enabled = False
            End If

            'set default sort order
            dgvPlaycount.Sort(colPlaycountLastWatched, System.ComponentModel.ListSortDirection.Descending)
            dgvtraktWatchlist.Sort(coltraktWatchlistListedAt, System.ComponentModel.ListSortDirection.Descending)
            dgvtraktComments.Sort(coltraktCommentsDate, System.ComponentModel.ListSortDirection.Descending)

            AddHandler cbotraktListsFavorites.SelectedIndexChanged, AddressOf cbotraktListsFavorites_SelectedIndexChanged

            'load kodi interface settings (if exists) since we will use the information of remote path to create a playlist for kodi
            If File.Exists(Path.Combine(Master.SettingsPath, "Interface.Kodi.xml")) Then
                Dim xmlSer As Xml.Serialization.XmlSerializer = Nothing
                Using xmlSR As StreamReader = New StreamReader(Path.Combine(Master.SettingsPath, "Interface.Kodi.xml"))
                    xmlSer = New Xml.Serialization.XmlSerializer(GetType(TraktInterface.KodiSettings))
                    _SpecialSettings = DirectCast(xmlSer.Deserialize(xmlSR), TraktInterface.KodiSettings)
                End Using
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Remove list from favorites 
    ''' </summary>
    ''' <param name="sender">"Remove favorite list"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub btntraktListRemoveFavorite_Click(sender As Object, e As EventArgs) Handles btntraktListRemoveFavorite.Click
        Dim mylists As New List(Of AdvancedSettingsComplexSettingsTableItem)
        mylists = AdvancedSettings.GetComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
        If mylists IsNot Nothing Then
            For i = mylists.Count - 1 To 0 Step -1
                If mylists(i).Value = txttraktListURL.Text Then
                    mylists.RemoveAt(i)
                    Exit For
                End If
            Next
            Dim setting_name As New List(Of String)
            Dim setting_value As New List(Of String)
            For Each sett As AdvancedSettingsComplexSettingsTableItem In mylists
                setting_name.Add(sett.Name)
                setting_value.Add(sett.Value)
            Next

            Using settings = New AdvancedSettings()
                settings.ClearComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
                Dim updatedsettings As New List(Of AdvancedSettingsComplexSettingsTableItem)
                For i = 0 To setting_name.Count - 1
                    updatedsettings.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = setting_name.Item(i), .Value = setting_value.Item(i)})
                Next
                settings.SetComplexSetting("TraktFavoriteLists", updatedsettings, "generic.EmberCore.Trakt")
                btntraktListRemoveFavorite.Enabled = False
                cbotraktListsFavorites.Enabled = True
                cbotraktListsFavorites.Items.Clear()
                cbotraktListsFavorites.Text = String.Empty
                cbotraktListsFavorites.SelectedText = String.Empty
                cbotraktListsFavorites.SelectedIndex = -1
                For Each k In updatedsettings
                    If Not cbotraktListsFavorites.Items.Contains(k.Value) Then
                        cbotraktListsFavorites.Items.Add(k.Name)
                    End If
                Next
                txttraktListURL.Text = String.Empty
                lbltraktListDescriptionText.Text = String.Empty
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Save list to favorites 
    ''' </summary>
    ''' <param name="sender">"Save favorite list"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub btntraktListSaveFavorite_Click(sender As Object, e As EventArgs) Handles btntraktListSaveFavorite.Click
        Dim mylists As New List(Of AdvancedSettingsComplexSettingsTableItem)
        mylists = AdvancedSettings.GetComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
        Using settings = New AdvancedSettings()
            If mylists IsNot Nothing Then
                If mylists.FindIndex(Function(f) f.Value = txttraktListURL.Text) = -1 Then
                    mylists.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = cbotraktListsScraped.SelectedItem.ToString, .Value = txttraktListURL.Text})

                    settings.SetComplexSetting("TraktFavoriteLists", mylists, "generic.EmberCore.Trakt")
                End If
            Else
                mylists = New List(Of AdvancedSettingsComplexSettingsTableItem)()
                mylists.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = cbotraktListsScraped.SelectedItem.ToString, .Value = txttraktListURL.Text})
                settings.SetComplexSetting("TraktFavoriteLists", mylists, "generic.EmberCore.Trakt")
            End If
        End Using
        cbotraktListsFavorites.Enabled = True
        btntraktListRemoveFavorite.Enabled = True
        cbotraktListsFavorites.Enabled = True
        cbotraktListsFavorites.Items.Clear()
        cbotraktListsFavorites.Text = String.Empty
        cbotraktListsFavorites.SelectedText = String.Empty
        cbotraktListsFavorites.SelectedIndex = -1
        For Each k In mylists
            If Not cbotraktListsFavorites.Items.Contains(k.Value) Then
                cbotraktListsFavorites.Items.Add(k.Name)
            End If
        Next
    End Sub
    ''' <summary>
    ''' Actions on module close event
    ''' </summary>
    ''' <param name="sender">Close button</param>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
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
        ' Use Trakttv wrapper
        Dim account As New TraktAPI.Model.TraktAuthentication
        account.Username = _traktuser
        account.Password = _traktpassword
        TraktSettings.Password = _traktpassword
        TraktSettings.Username = _traktuser

        If String.IsNullOrEmpty(_trakttoken) Then
            Dim response = TraktMethods.LoginToAccount(account)
            If response IsNot Nothing Then
                _trakttoken = response.Token
            Else
                _trakttoken = String.Empty
            End If
        Else
            TraktSettings.Token = _trakttoken
        End If
        Return _trakttoken
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
            If myWatchlistEpisodes IsNot Nothing Then
                myWatchlistEpisodes.Clear()
            End If
            dgvtraktWatchlist.DataSource = Nothing
            dgvtraktWatchlist.Rows.Clear()

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'GET movies of users watchlist
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 2 values in Dictionary style, we are using dictionary because later you can easily check with Dict.containsKey if entry already exists (not so easy to do with a complex list)
            Dim dictMovieWatchlist As New Dictionary(Of String, String)

            Dim traktWatchListMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatchList) = _TraktAPI.GetWatchList_Movies()
            If traktWatchListMovies IsNot Nothing Then
                For Each Item As TraktAPI.Model.TraktMovieWatchList In traktWatchListMovies
                    'Check if information is stored...
                    If Item.Movie.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(Item.Movie.Title) AndAlso Item.Movie.Ids.Imdb IsNot Nothing AndAlso Not String.IsNullOrEmpty(Item.Movie.Ids.Imdb) Then
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
                If myWatchlistMovies IsNot Nothing Then
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
                        Dim myDate As Date
                        Dim isDate As Boolean = Date.TryParse(myDateString, myDate)
                        If isDate Then
                            Item.ListedAt = myDate.ToString("yyyy-MM-dd HH:mm:ss")
                        End If
                        dgvtraktWatchlist.Rows.Add(New Object() {Item.Movie.Title, Item.Movie.Year, Item.ListedAt, "http://www.imdb.com/title/" & Item.Movie.Ids.Imdb})
                    Next
                Else
                    btntraktWatchlistSyncLibrary.Enabled = False
                    btntraktWatchlistClean.Enabled = False
                    btntraktWatchlistSendEmberUnwatched.Enabled = False
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            If myWatchlistEpisodes IsNot Nothing Then
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
            If myWatchlistMovies.Count > 0 AndAlso dtMovies.Rows.Count > 0 Then
                Dim lstmovietoremove As New List(Of TraktAPI.Model.TraktMovie)
                For Each sRow As DataRow In dtMovies.Rows
                    If sRow.Item("Playcount").ToString <> "0" AndAlso Not String.IsNullOrEmpty(sRow.Item("Playcount").ToString) Then
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
                    Dim deletemoviemodel As New TraktAPI.Model.TraktSyncMovies
                    deletemoviemodel.Movies = lstmovietoremove
                    Dim response = _TraktAPI.RemoveFromWatchlist_Movies(deletemoviemodel)
                    If response IsNot Nothing Then
                        If response.Added IsNot Nothing Then
                            logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Added movies: " & response.Added.Movies)
                        End If
                        If response.NotFound IsNot Nothing Then
                            logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                        End If
                        If response.Existing IsNot Nothing Then
                            logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                        End If
                        If response.Deleted IsNot Nothing Then
                            logger.Info("[btntraktWatchlistSyncLibrary_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                        End If
                    End If
                    If response IsNot Nothing AndAlso response.Deleted IsNot Nothing AndAlso response.Deleted.Movies > 0 Then
                        MessageBox.Show(response.Deleted.Movies.ToString & " " & Master.eLang.GetString(1364, "Movies removed from trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                        myWatchlistMovies.Clear()
                        If myWatchlistEpisodes IsNot Nothing Then
                            myWatchlistEpisodes.Clear()
                        End If
                        dgvtraktWatchlist.DataSource = Nothing
                        dgvtraktWatchlist.Rows.Clear()
                        btntraktWatchlistSyncLibrary.Enabled = False
                        btntraktWatchlistClean.Enabled = False
                        btntraktWatchlistSendEmberUnwatched.Enabled = False
                    Else
                        MessageBox.Show(Master.eLang.GetString(1365, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                    End If
                Else
                    logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies to remove from watchlist!")
                    MessageBox.Show(Master.eLang.GetString(1365, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                End If
            Else
                logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies in watchlist/Ember database - Abort process!")
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            For Each sRow As DataRow In dtMovies.Rows
                If sRow.Item("Playcount").ToString = "0" OrElse String.IsNullOrEmpty(sRow.Item("Playcount").ToString) Then
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
                        If Integer.TryParse(sRow.Item("Imdb").ToString, 0) Then
                            tmptraktbasemovie.Imdb = "tt" & sRow.Item("Imdb").ToString
                        Else
                            tmptraktbasemovie.Imdb = sRow.Item("Imdb").ToString
                            logger.Warn("[btntraktWatchlistSendEmberUnwatched_Click] IMDB of movie " & sRow.Item("Title").ToString & " was not regular (missing tt)!")
                        End If
                    End If
                    tmptraktbasemovie.Slug = TraktMethods.ConvertToSlug(sRow.Item("Title").ToString & sRow.Item("Year").ToString)
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
                Dim traktsyncmovies As New TraktAPI.Model.TraktSyncMovies
                traktsyncmovies.Movies = lstmovietoadd
                Dim response = _TraktAPI.AddToWatchlist_Movies(traktsyncmovies)
                If response IsNot Nothing AndAlso response.Added IsNot Nothing AndAlso response.Added.Movies > 0 Then
                    MessageBox.Show(response.Added.Movies.ToString & " " & Master.eLang.GetString(1366, "Movies added to trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                    myWatchlistMovies.Clear()
                    If myWatchlistEpisodes IsNot Nothing Then
                        myWatchlistEpisodes.Clear()
                    End If
                    dgvtraktWatchlist.DataSource = Nothing
                    dgvtraktWatchlist.Rows.Clear()
                    btntraktWatchlistSyncLibrary.Enabled = False
                    btntraktWatchlistClean.Enabled = False
                    btntraktWatchlistSendEmberUnwatched.Enabled = False
                Else
                    MessageBox.Show(Master.eLang.GetString(1365, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                End If
                If response IsNot Nothing Then
                    If response.Added IsNot Nothing Then
                        logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Added movies: " & response.Added.Movies)
                    End If
                    If response.NotFound IsNot Nothing Then
                        logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                    End If
                    If response.Existing IsNot Nothing Then
                        logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                    End If
                    If response.Deleted IsNot Nothing Then
                        logger.Info("[btntraktWatchlistSendEmberUnwatched_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                    End If
                End If
            Else
                logger.Info("[btntraktWatchlistSyncLibrary_Click] No movies to add to watchlist!")
                MessageBox.Show(Master.eLang.GetString(1365, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
                    Dim deletemoviemodel As New TraktAPI.Model.TraktSyncMovies
                    deletemoviemodel.Movies = lstmovietoremove
                    Dim response = _TraktAPI.RemoveFromWatchlist_Movies(deletemoviemodel)
                    If response IsNot Nothing Then
                        If response.Added IsNot Nothing Then
                            logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Added movies: " & response.Added.Movies)
                        End If
                        If response.NotFound IsNot Nothing Then
                            logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Not found movies: " & response.NotFound.Movies.Count)
                        End If
                        If response.Existing IsNot Nothing Then
                            logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Existing movies: " & response.Existing.Movies)
                        End If
                        If response.Deleted IsNot Nothing Then
                            logger.Info("[btntraktWatchlistClean_Click] Trakt Response. Removed movies: " & response.Deleted.Movies)
                        End If
                    End If
                    If response IsNot Nothing AndAlso response.Deleted IsNot Nothing AndAlso response.Deleted.Movies > 0 Then
                        MessageBox.Show(response.Deleted.Movies.ToString & " " & Master.eLang.GetString(1364, "Movies removed from trakt.tv watchlist!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                        myWatchlistMovies.Clear()
                        If myWatchlistEpisodes IsNot Nothing Then
                            myWatchlistEpisodes.Clear()
                        End If
                        dgvtraktWatchlist.DataSource = Nothing
                        dgvtraktWatchlist.Rows.Clear()
                        btntraktWatchlistSyncLibrary.Enabled = False
                        btntraktWatchlistClean.Enabled = False
                        btntraktWatchlistSendEmberUnwatched.Enabled = False
                    Else
                        MessageBox.Show(Master.eLang.GetString(1365, "No changes made!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
                    End If
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region

#Region "Trakt.tv Sync Playcount"
    ''' <summary>
    ''' GET watched movies of user and display in listbox
    ''' </summary>
    ''' <param name="sender">"Get watched movies"-Button in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btnPlaycountGetList_Movies_Movies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlaycountGetList_Movies.Click
        Cursor = Cursors.WaitCursor
        _myRatedMovies = Nothing
        _myWatchedMovies = Nothing
        _myWatchedRatedMovies = Nothing
        _myWatchedTVEpisodes = Nothing
        _myWatchedProgressTVShows = Nothing
        dgvPlaycount.DataSource = Nothing
        dgvPlaycount.Rows.Clear()
        btnPlaycountSyncWatched_TVShows.Enabled = False

        _myWatchedMovies = _TraktAPI.GetWatched_Movies()
        _myRatedMovies = _TraktAPI.GetRated_Movies()

        If _myWatchedMovies Is Nothing Then
            dgvPlaycount.DataSource = Nothing
            dgvPlaycount.Rows.Clear()
            btnSaveWatchedStateToEmber.Enabled = False
            btnPlaycountSyncWatched_Movies.Enabled = True
            logger.Info("No watched movies scraped from trakt.tv!")
            Exit Sub
        Else
            _myWatchedRatedMovies = _TraktAPI.GetWatchedRated_Movies(_myWatchedMovies, _myRatedMovies)
            dgvPlaycount.AutoGenerateColumns = True
            btnSaveWatchedStateToEmber.Enabled = True
            btnPlaycountSyncWatched_Movies.Enabled = True
            dgvPlaycount.Columns("colPlaycountProgress").Visible = False
            dgvPlaycount.Columns("colPlaycountRating").Visible = True
            'we map to dgv manually
            dgvPlaycount.AutoGenerateColumns = False
            'fill rows
            For Each tMovie As TraktAPI.Model.TraktMovieWatchedRated In _myWatchedRatedMovies
                dgvPlaycount.Rows.Add(New Object() {tMovie.Movie.Ids.Trakt, tMovie.Movie.Title, tMovie.Plays, tMovie.LastWatchedAt, String.Empty, tMovie.Rating})
            Next
            Cursor = Cursors.Default
            dgvPlaycount.Sort(colPlaycountLastWatched, System.ComponentModel.ListSortDirection.Descending)
        End If
    End Sub

    ''' <summary>
    ''' GET watched episodes of user and display in listbox
    ''' </summary>
    ''' <param name="sender">"Get watched episodes"-Button in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btnPlaycountGetList_TVShows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlaycountGetList_TVShows.Click
        Cursor = Cursors.WaitCursor
        _myRatedMovies = Nothing
        _myWatchedMovies = Nothing
        _myWatchedRatedMovies = Nothing
        _myWatchedProgressTVShows = Nothing
        _myWatchedTVEpisodes = Nothing
        dgvPlaycount.DataSource = Nothing
        dgvPlaycount.Rows.Clear()
        btnPlaycountSyncWatched_Movies.Enabled = False

        _myWatchedTVEpisodes = _TraktAPI.GetWatched_TVEpisodes()

        If _myWatchedTVEpisodes Is Nothing Then
            dgvPlaycount.DataSource = Nothing
            dgvPlaycount.Rows.Clear()
            btnSaveWatchedStateToEmber.Enabled = False
            btnPlaycountSyncWatched_Movies.Enabled = True
            logger.Info("No watched episodes scraped from trakt.tv!")
            Exit Sub
        Else
            _myWatchedProgressTVShows = _TraktAPI.GetWatchedProgress_TVShows(_myWatchedTVEpisodes)
            dgvPlaycount.AutoGenerateColumns = True
            btnSaveWatchedStateToEmber.Enabled = True
            btnPlaycountSyncWatched_TVShows.Enabled = True
            dgvPlaycount.Columns("colPlaycountProgress").Visible = _bGetShowProgress
            dgvPlaycount.Columns("colPlaycountRating").Visible = False
            'we map to dgv manually
            dgvPlaycount.AutoGenerateColumns = False
            'fill rows
            For Each tWatchedProgressTVShow In _myWatchedProgressTVShows
                If _bGetShowProgress Then
                    dgvPlaycount.Rows.Add(New Object() {tWatchedProgressTVShow.ShowID, tWatchedProgressTVShow.ShowTitle, tWatchedProgressTVShow.EpisodePlaycount, tWatchedProgressTVShow.LastWatchedEpisode, tWatchedProgressTVShow.EpisodesWatched.ToString & "/" & tWatchedProgressTVShow.EpisodesAired.ToString, String.Empty})
                Else
                    dgvPlaycount.Rows.Add(New Object() {tWatchedProgressTVShow.ShowID, tWatchedProgressTVShow.ShowTitle, tWatchedProgressTVShow.EpisodePlaycount, tWatchedProgressTVShow.LastWatchedEpisode, String.Empty, String.Empty})
                End If
            Next
            Cursor = Cursors.Default
            dgvPlaycount.Sort(colPlaycountLastWatched, System.ComponentModel.ListSortDirection.Descending)
        End If
    End Sub

    ''' <summary>
    '''  Send edited ratings in datagrid to trakt.tv
    ''' </summary>
    ''' <param name="sender">"Submit Rating to trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' This sub  handles submitting rating of movies to trakt.tv
    ''' </remarks>
    Private Sub btnPlaycountSyncRating_Click(sender As Object, e As EventArgs) Handles btnPlaycountSyncRating.Click
        Dim response As String = Master.eLang.GetString(1371, "Submit ratings to trakt.tv") & "? " & Master.eLang.GetString(36, "Movies") & ":" & Environment.NewLine
        Dim postRatings As Boolean = False
        If _myWatchedMovies IsNot Nothing Then
            'Add movies with rating > 0 
            Dim tmpAddRating As New TraktAPI.Model.TraktSyncMoviesRated With {.Movies = New List(Of TraktAPI.Model.TraktSyncMovieRated)}
            For Each tMovie In _myWatchedRatedMovies.Where(Function(f) f.Modified AndAlso f.Rating > 0)
                tmpAddRating.Movies.Add(New TraktAPI.Model.TraktSyncMovieRated With {
                                        .Ids = tMovie.Movie.Ids,
                                        .RatedAt = tMovie.RatedAt,
                                        .Rating = tMovie.Rating,
                                        .Title = tMovie.Movie.Title,
                                        .Year = tMovie.Movie.Year})
                postRatings = True
                response = response & tMovie.Movie.Title & Environment.NewLine
            Next
            'Remove movies with rating = 0 
            Dim tmpRemoveRating As New TraktAPI.Model.TraktSyncMovies With {.Movies = New List(Of TraktAPI.Model.TraktMovie)}
            For Each tMovie In _myWatchedRatedMovies.Where(Function(f) f.Modified AndAlso f.Rating = 0)
                tmpRemoveRating.Movies.Add(New TraktAPI.Model.TraktMovie With {
                                           .Ids = tMovie.Movie.Ids})
                postRatings = True
                response = response & tMovie.Movie.Title & Environment.NewLine
            Next
            If postRatings Then
                If MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    Dim iAdded As Integer
                    Dim iRemoved As Integer
                    If tmpAddRating.Movies.Count > 0 Then
                        Dim traktResponse = _TraktAPI.Rating_AddMovies(tmpAddRating)
                        If traktResponse IsNot Nothing Then
                            iAdded = traktResponse.Added.Movies
                        End If
                    End If
                    If tmpRemoveRating.Movies.Count > 0 Then
                        Dim traktResponse = _TraktAPI.Rating_RemoveMovies(tmpRemoveRating)
                        If traktResponse IsNot Nothing Then
                            iRemoved = traktResponse.Deleted.Movies
                        End If
                    End If
                    If iAdded > 0 OrElse iRemoved > 0 Then
                        logger.Info("Added Ratings to trakt.tv!")
                        response = Master.eLang.GetString(1372, "Added Ratings to trakt.tv!")
                    Else
                        logger.Info("No Ratings submitted!")
                        response = Master.eLang.GetString(1373, "No Ratings submitted!")
                    End If
                    MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    '''  Remove selected episode/movie from trakt.tv watch history
    ''' </summary>
    ''' <param name="sender">"Delete selected item from trakt.tv history"-Button in Form</param>
    ''' <remarks>
    ''' 2015/05/30 Cocotus - First implementation
    ''' This sub handles deleting movies/episodes from trakt.tv watch history
    ''' </remarks>
    Private Sub btnPlaycountSyncDeleteItem_Click(sender As Object, e As EventArgs) Handles btnPlaycountSyncDeleteItem.Click
        Dim response As String = String.Empty
        Dim IsMovie As Boolean = True
        If _myWatchedRatedMovies Is Nothing Then
            IsMovie = False
        End If
        Dim lstToRemove_Movies As New List(Of TraktAPI.Model.TraktMovie)
        Dim removeList_TVShows As New List(Of TraktAPI.Model.TraktSyncShowEx)

        'if isMovie=false then it's a single TvShow to delete, else a single movie
        If IsMovie Then
            If _myWatchedRatedMovies IsNot Nothing Then
                'search by TraktID
                response = Master.eLang.GetString(1406, "Delete selected item(s) from trakt.tv history") & "?" & Environment.NewLine
                For Each sCell As DataGridViewCell In dgvPlaycount.SelectedCells
                    Dim nMovie = _myWatchedRatedMovies.FirstOrDefault(Function(f) f.Movie.Ids.Trakt IsNot Nothing AndAlso
                                                                      CInt(f.Movie.Ids.Trakt) = CInt(dgvPlaycount.Rows(sCell.RowIndex).Cells("colPlaycountTraktID").Value))
                    If nMovie IsNot Nothing Then
                        response = String.Concat(response, nMovie.Movie.Title, Environment.NewLine)
                        lstToRemove_Movies.Add(nMovie.Movie)
                    End If
                Next
            End If
        Else
            If _myWatchedProgressTVShows IsNot Nothing Then
                'search by TraktID
                response = Master.eLang.GetString(1406, "Delete selected item(s) from trakt.tv history") & "?" & Environment.NewLine
                For Each sCell As DataGridViewCell In dgvPlaycount.SelectedCells
                    Dim nTVShow = _myWatchedProgressTVShows.FirstOrDefault(Function(f) Not String.IsNullOrEmpty(f.ShowID) AndAlso
                                                                               f.ShowID = dgvPlaycount.Rows(sCell.RowIndex).Cells("colPlaycountTraktID").Value.ToString)
                    If nTVShow IsNot Nothing Then
                        response = String.Concat(response, nTVShow.ShowTitle, Environment.NewLine)
                        Dim tmpShow As New TraktAPI.Model.TraktSyncShowEx
                        tmpShow.Ids = New TraktAPI.Model.TraktShowBase
                        tmpShow.Ids.Trakt = CInt(nTVShow.ShowID)
                        removeList_TVShows.Add(tmpShow)
                    End If
                Next
            End If
        End If

        If lstToRemove_Movies.Count > 0 OrElse removeList_TVShows.Count > 0 Then
            If MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                Dim tmpresponse As String = String.Empty
                If IsMovie Then
                    Dim tMovies As New TraktAPI.Model.TraktSyncMovies With {.Movies = lstToRemove_Movies}
                    Dim traktResponse = _TraktAPI.RemoveFromWatchedHistory_Movies(tMovies)
                    If traktResponse IsNot Nothing Then
                        logger.Info(String.Concat("Deleted Movies: ", traktResponse.Deleted.Movies))
                        tmpresponse = String.Concat(tmpresponse, Master.eLang.GetString(1407, "Deleted"), ": ", traktResponse.Deleted.Movies, " Movies", Environment.NewLine)
                        For Each tNotFound In traktResponse.NotFound.Movies
                            tmpresponse = String.Concat(tmpresponse, Master.eLang.GetString(1407, "Not Found"), ": ", tNotFound.Title, " / ", tNotFound.Ids.Imdb, Environment.NewLine)
                        Next
                        logger.Warn(String.Concat(tmpresponse, Master.eLang.GetString(1407, "Not Found"), ": ", traktResponse.NotFound.Movies.Count, " Movies", Environment.NewLine))
                    Else
                        response = Master.eLang.GetString(1134, "Error!")
                    End If
                Else
                    For Each tTVShow In removeList_TVShows
                        Dim traktResponse = TrakttvAPI.RemoveShowFromWatchedHistoryEx(tTVShow)
                        If traktResponse IsNot Nothing Then
                            If traktResponse.Deleted.Episodes > 0 Then
                                logger.Info("Deleted Item: " & tTVShow.Title)
                                tmpresponse = String.Concat(tmpresponse, Master.eLang.GetString(1407, "Deleted"), ": ", tTVShow.Title, Environment.NewLine)
                            Else
                                logger.Info("Nothing deleted!")
                            End If
                        Else
                            response = Master.eLang.GetString(1134, "Error!")
                        End If
                    Next

                    If Not String.IsNullOrEmpty(tmpresponse) Then
                        response = Master.eLang.GetString(1407, "Deleted") & ": " & Environment.NewLine & tmpresponse
                    Else
                        response = Master.eLang.GetString(1365, "No changes made!")
                    End If
                End If
            End If
        End If
    End Sub


    ''' <summary>
    '''  Send all watched movies in Ember database to trakt.tv
    ''' </summary>
    ''' <param name="sender">"Submit playcount of watched movies to trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' 2015/05/30 Cocotus - First implementation
    ''' This sub handles submitting playcount of movies to trakt.tv
    ''' </remarks>
    Private Sub btnPlaycountSyncWatched_Movies_Click(sender As Object, e As EventArgs) Handles btnPlaycountSyncWatched_Movies.Click
        If dtMovies.Rows.Count > 0 Then
            Dim tmpTraktSynchronize As New TraktAPI.Model.TraktSyncMoviesWatched
            Dim response As String = Master.eLang.GetString(1402, "Send to your watch history") & "? " & Master.eLang.GetString(36, "Movies") & ":" & Environment.NewLine
            tmpTraktSynchronize.Movies = New List(Of TraktAPI.Model.TraktSyncMovieWatched)
            Dim SyncThisItem As Boolean = True
            For Each sRow As DataRow In dtMovies.Rows
                If sRow.Item("Playcount").ToString <> "0" AndAlso Not String.IsNullOrEmpty(sRow.Item("Playcount").ToString) AndAlso Not String.IsNullOrEmpty(sRow.Item("Imdb").ToString) Then
                    SyncThisItem = True
                    If _myWatchedRatedMovies IsNot Nothing Then
                        For Each watchedMovieData In _myWatchedRatedMovies
                            If watchedMovieData.Movie.Ids.Imdb = sRow.Item("Imdb").ToString Then
                                SyncThisItem = False
                                Exit For
                            End If
                        Next
                    End If
                    If SyncThisItem Then
                        Dim tmpTraktWatchedSyncMovie As New TraktAPI.Model.TraktSyncMovieWatched
                        tmpTraktWatchedSyncMovie.Ids = New TraktAPI.Model.TraktMovieBase
                        If Not sRow.Item("Imdb").ToString.StartsWith("tt") Then
                            tmpTraktWatchedSyncMovie.Ids.Imdb = "tt" & sRow.Item("Imdb").ToString
                        Else
                            tmpTraktWatchedSyncMovie.Ids.Imdb = sRow.Item("Imdb").ToString
                        End If
                        If sRow.Table.Columns.Contains("iLastPlayed") AndAlso Not sRow.Item("iLastPlayed") Is DBNull.Value Then
                            Dim myDate As Date
                            myDate = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(sRow.Item("iLastPlayed").ToString))
                            '   Dim isDate As Boolean = DateTime.TryParse(sRow.Item("iLastPlayed").ToString, myDate)
                            tmpTraktWatchedSyncMovie.WatchedAt = myDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK")
                        End If
                        tmpTraktSynchronize.Movies.Add(tmpTraktWatchedSyncMovie)
                        response = response & sRow.Item("Title").ToString & Environment.NewLine
                    End If
                End If
            Next

            If tmpTraktSynchronize.Movies.Count > 0 Then
                Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If result = Windows.Forms.DialogResult.Yes Then
                    Dim traktResponse = _TraktAPI.AddToWatchedHistory_Movies(tmpTraktSynchronize)
                    If traktResponse IsNot Nothing Then
                        If traktResponse.Added.Movies > 0 Then
                            logger.Info("Added to watch history!")
                            response = Master.eLang.GetString(1403, "Added to watch history") & "!"
                        Else
                            logger.Info("No movies submitted!")
                            response = Master.eLang.GetString(1365, "No changes made!")
                        End If
                    Else
                        response = Master.eLang.GetString(1134, "Error!")
                    End If
                    MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                End If
            Else
                logger.Info("[btntraktPlaycountSyncWatchedMovies_Click] No unsynced watched movies in Ember database - Abort process!")
                response = Master.eLang.GetString(1365, "No changes made!")
                MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            End If
        Else
            logger.Info("[btntraktPlaycountSyncWatchedMovies_Click] No movies in Ember database - Abort process!")
        End If
    End Sub

    ''' <summary>
    '''  Send all watched episodes in Ember database to trakt.tv
    ''' </summary>
    ''' <param name="sender">"Submit playcount of watched episodes to trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' 2015/05/30 Cocotus - First implementation
    ''' This sub handles submitting playcount of episodes to trakt.tv
    ''' </remarks>
    Private Sub btnPlaycountSyncWatched_TVShows_Click(sender As Object, e As EventArgs) Handles btnPlaycountSyncWatched_TVShows.Click
        If dtEpisodes.Rows.Count > 0 Then
            Dim tmpTraktSynchronize As New TraktAPI.Model.TraktSyncShowsWatchedEx
            Dim response As String = Master.eLang.GetString(1402, "Send to your watch history") & "? " & Master.eLang.GetString(682, "Episodes") & ":" & Environment.NewLine
            tmpTraktSynchronize.Shows = New List(Of TraktAPI.Model.TraktSyncShowWatchedEx)
            Dim SyncThisItem As Boolean = True
            For Each sRow As DataRow In dtEpisodes.Rows
                If sRow.Item("Playcount").ToString <> "0" AndAlso Not String.IsNullOrEmpty(sRow.Item("Playcount").ToString) AndAlso Not String.IsNullOrEmpty(sRow.Item("TVDB").ToString) Then
                    SyncThisItem = True
                    If _myWatchedTVEpisodes IsNot Nothing Then
                        For Each watchedshow In _myWatchedTVEpisodes
                            If Not SyncThisItem Then Exit For
                            If watchedshow.Show.Ids.Tvdb.ToString = sRow.Item("TVDB").ToString Then
                                If Not SyncThisItem Then Exit For
                                'every season of watched show
                                For Each watchedseason In watchedshow.Seasons
                                    If Not SyncThisItem Then Exit For
                                    'every episode of watched season
                                    For Each watchedepisode In watchedseason.Episodes
                                        ' now go to every episode of current season
                                        If watchedseason.Number.ToString = sRow.Item("Season").ToString AndAlso watchedepisode.Number.ToString = sRow.Item("Episode").ToString Then
                                            SyncThisItem = False
                                            Exit For
                                        End If
                                    Next
                                Next
                            End If
                        Next
                    End If
                    If SyncThisItem Then
                        Dim tmpTraktWatchedSyncShowData As New TraktAPI.Model.TraktSyncShowWatchedEx
                        tmpTraktWatchedSyncShowData.Ids = New TraktAPI.Model.TraktShowBase
                        tmpTraktWatchedSyncShowData.Ids.Tvdb = CType(sRow.Item("TVDB").ToString, Integer?)

                        tmpTraktWatchedSyncShowData.Seasons = New List(Of TraktAPI.Model.TraktSyncShowWatchedEx.Season)
                        Dim tmpTraktSyncShowWatchedSeasonItem As New TraktAPI.Model.TraktSyncShowWatchedEx.Season
                        tmpTraktSyncShowWatchedSeasonItem.Number = CInt(sRow.Item("Season"))

                        tmpTraktSyncShowWatchedSeasonItem.Episodes = New List(Of TraktAPI.Model.TraktSyncShowWatchedEx.Season.Episode)
                        Dim tmpTraktSyncShowWatchedEpisodeItem As New TraktAPI.Model.TraktSyncShowWatchedEx.Season.Episode
                        tmpTraktSyncShowWatchedEpisodeItem.Number = CInt(sRow.Item("Episode"))
                        If sRow.Table.Columns.Contains("iLastPlayed") AndAlso Not sRow.Item("iLastPlayed") Is DBNull.Value Then
                            Dim myDate As Date
                            myDate = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(sRow.Item("iLastPlayed").ToString))
                            tmpTraktSyncShowWatchedEpisodeItem.WatchedAt = myDate.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK")
                        End If
                        tmpTraktSyncShowWatchedSeasonItem.Episodes.Add(tmpTraktSyncShowWatchedEpisodeItem)

                        tmpTraktWatchedSyncShowData.Seasons.Add(tmpTraktSyncShowWatchedSeasonItem)

                        tmpTraktSynchronize.Shows.Add(tmpTraktWatchedSyncShowData)
                        response = response & sRow.Item("ListTitle").ToString & " S" & sRow.Item("Season").ToString & "e" & sRow.Item("Episode").ToString & Environment.NewLine
                    End If
                End If
            Next

            If tmpTraktSynchronize.Shows.Count > 0 Then
                Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If result = Windows.Forms.DialogResult.Yes Then
                    Dim traktResponse = _TraktAPI.AddToWatchedHistoryEx_TVShows(tmpTraktSynchronize)
                    If traktResponse IsNot Nothing Then
                        If traktResponse.Added.Episodes > 0 Then
                            logger.Info("Added to watch history!")
                            response = Master.eLang.GetString(1403, "Added to watch history") & "!"
                        Else
                            logger.Info("No items submitted!")
                            response = Master.eLang.GetString(1365, "No changes made!")
                        End If
                    Else
                        response = Master.eLang.GetString(1134, "Error!")
                    End If
                    MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                End If
            Else
                logger.Info("[btntraktPlaycountSyncWatchedMovies_Click] No unsynced watched episodes in Ember database - Abort process!")
                response = Master.eLang.GetString(1365, "No changes made!")
                MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            End If
        Else
            logger.Info("[btntraktPlaycountSyncWatchedMovies_Click] No episodes in Ember database - Abort process!")
        End If
    End Sub
    ''' <summary>
    '''  Save either playcounts of movie or episodes to Ember database/Nfo
    ''' </summary>
    ''' <param name="sender">"Save playcount to database/Nfo"-Button in Form</param>
    ''' <remarks>
    ''' This sub  handles saving of movies and episodes playcount to nfo
    ''' </remarks>
    Private Sub btnSaveWatchedStateToEmber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveWatchedStateToEmber.Click

        'check if playcount(s) of movies or episodes should be saved - only one type will be done!
        If _myWatchedMovies IsNot Nothing Then
            'save movie playcount!
            prgPlaycount.Value = 0
            prgPlaycount.Maximum = _myWatchedMovies.Where(Function(f) f.Movie.Ids.Imdb IsNot Nothing OrElse
                                                                  f.Movie.Ids.Tmdb IsNot Nothing).Count
            prgPlaycount.Step = 1
            btnSaveWatchedStateToEmber.Enabled = False

            bwSaveWatchedStateToEmber_Movies = New System.ComponentModel.BackgroundWorker
            bwSaveWatchedStateToEmber_Movies.WorkerSupportsCancellation = True
            bwSaveWatchedStateToEmber_Movies.WorkerReportsProgress = True
            bwSaveWatchedStateToEmber_Movies.RunWorkerAsync()
        ElseIf _myWatchedTVEpisodes IsNot Nothing Then
            'save tvepisodes playcount!
            prgPlaycount.Value = 0
            prgPlaycount.Maximum = _myWatchedTVEpisodes.Where(Function(f) f.Show.Ids.Imdb IsNot Nothing OrElse
                                                                  f.Show.Ids.Tmdb IsNot Nothing OrElse
                                                                  f.Show.Ids.Tvdb IsNot Nothing).Count
            'start not with empty progressbar(no problem for movies) because it takes long to update for first tv show and user might think it hangs -> set value 1 to show something is going on
            If _myWatchedTVEpisodes.Count > 1 Then
                prgPlaycount.Value = 1
            End If
            prgPlaycount.Step = 1
            btnSaveWatchedStateToEmber.Enabled = False

            bwSaveWatchedStateToEmber_TVEpisodes = New System.ComponentModel.BackgroundWorker
            bwSaveWatchedStateToEmber_TVEpisodes.WorkerSupportsCancellation = True
            bwSaveWatchedStateToEmber_TVEpisodes.WorkerReportsProgress = True
            bwSaveWatchedStateToEmber_TVEpisodes.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwSaveWatchedStateToEmber_Movies_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwSaveWatchedStateToEmber_Movies.RunWorkerCompleted
        Return
    End Sub

    Private Sub bwSaveWatchedStateToEmber_Movies_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSaveWatchedStateToEmber_Movies.DoWork
        _TraktAPI.SaveWatchedStateToEmber_Movies(_myWatchedMovies, AddressOf ShowProgress_SaveWatchedStateToEmber_Movies)
    End Sub

    Private Sub bwSaveWatchedStateToEmber_Movies_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwSaveWatchedStateToEmber_Movies.ProgressChanged
        UpdateProgressBar(e.ProgressPercentage, e.UserState.ToString)
    End Sub

    Private Sub bwSaveWatchedStateToEmber_TVEpisodes_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwSaveWatchedStateToEmber_TVEpisodes.RunWorkerCompleted
        Return
    End Sub

    Private Sub bwSaveWatchedStateToEmber_TVEpisodes_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSaveWatchedStateToEmber_TVEpisodes.DoWork
        _TraktAPI.SaveWatchedStateToEmber_TVEpisodes(_myWatchedTVEpisodes, AddressOf ShowProgress_SaveWatchedStateToEmber_TVEpisodes)
    End Sub

    Private Sub bwSaveWatchedStateToEmber_TVEpisodes_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwSaveWatchedStateToEmber_TVEpisodes.ProgressChanged
        UpdateProgressBar(e.ProgressPercentage, e.UserState.ToString)
    End Sub
    ''' <summary>
    ''' Handles CellClick Event for Rating column
    ''' </summary>
    ''' <param name="sender">Cell click event of rating column</param>
    ''' <remarks>
    ''' 2015/02/21 Cocotus - First implementation
    ''' Edit rating and store new value in globalwatchedMovieData
    Private Sub dgvPlaycount_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPlaycount.CellEndEdit
        If e.RowIndex > -1 AndAlso e.ColumnIndex = 5 AndAlso dgvPlaycount.CurrentCell.RowIndex > -1 AndAlso _myWatchedRatedMovies IsNot Nothing Then
            Dim intNewRating As Integer = -1
            If Integer.TryParse(dgvPlaycount.Rows(dgvPlaycount.CurrentCell.RowIndex).Cells("colPlaycountRating").Value.ToString, intNewRating) AndAlso intNewRating >= 0 AndAlso intNewRating <= 10 Then
                'search by TraktID
                Dim nMovie = _myWatchedRatedMovies.FirstOrDefault(Function(f) f.Movie.Ids.Trakt IsNot Nothing AndAlso
                                                                      CInt(f.Movie.Ids.Trakt) = CInt(dgvPlaycount.Rows(dgvPlaycount.CurrentCell.RowIndex).Cells("colPlaycountTraktID").Value))
                If nMovie IsNot Nothing Then
                    nMovie.RatedAt = Functions.ConvertToProperDateTime(Date.Now.ToString)
                    nMovie.Rating = intNewRating
                    nMovie.Modified = True
                    btnPlaycountSyncRating.Enabled = True
                End If
                'rewrite the new value to change the object type from String to Integer
                dgvPlaycount.Rows(dgvPlaycount.CurrentCell.RowIndex).Cells("colPlaycountRating").Value = intNewRating
            Else
                dgvPlaycount.Rows(dgvPlaycount.CurrentCell.RowIndex).Cells("colPlaycountRating").Value = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles SelectedRow Changed-Event
    ''' </summary>
    ''' <param name="sender">Selection Changed event of Datagrid</param>
    ''' <remarks>
    ''' 2015/05/30 Cocotus - First implementation
    ''' enabled/disables DeleteItem Button 
    Private Sub dgvPlaycount_SelectionChanged(sender As Object, e As EventArgs) Handles dgvPlaycount.SelectionChanged
        If dgvPlaycount.CurrentRow.Index > -1 Then
            btnPlaycountSyncDeleteItem.Enabled = True
        Else
            btnPlaycountSyncDeleteItem.Enabled = False
        End If
    End Sub

    Private Function ShowProgress_SaveWatchedStateToEmber_Movies(ByVal iProgress As Integer, ByVal strMessage As String) As Boolean
        bwSaveWatchedStateToEmber_Movies.ReportProgress(iProgress, strMessage)
        'If CancelRename Then Return False
        Return True
    End Function

    Private Function ShowProgress_SaveWatchedStateToEmber_TVEpisodes(ByVal iProgress As Integer, ByVal strMessage As String) As Boolean
        bwSaveWatchedStateToEmber_TVEpisodes.ReportProgress(iProgress, strMessage)
        'If CancelRename Then Return False
        Return True
    End Function
    ''' <summary>
    '''  Playcount progressbar update
    ''' </summary>
    ''' <remarks>
    ''' Do all the ui thread updates here
    ''' Use progressbar to show user progress of saving, since it can take some time
    ''' </remarks>
    Private Sub UpdateProgressBar(ByVal iProgress As Integer, ByVal strMessage As String)
        prgPlaycount.Value = iProgress
        lblPlaycountMessage.Text = strMessage
        If iProgress = prgPlaycount.Maximum Then
            lblPlaycountDone.Visible = True
            lblPlaycountMessage.Text = "-"
            prgPlaycount.Value = 0
        Else
            lblPlaycountDone.Visible = False
        End If
    End Sub

#End Region 'Trakt.tv Sync Playcount

#Region "Trakt.tv Sync Lists/Tags"
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '1. GET and POST userlists from/to trakt.tv using trakt.tv API calls!
    '2. Update/Sync movie tags
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' GET personal lists/listitems of user and display in listbox
    ''' </summary>
    ''' <param name="sender">"Get List"-Button in Form</param>
    ''' <remarks>
    ''' Request list/items of userlist at trakt.tv, update (global) "traktLists" and refresh view of listbox 
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' A list will only be added/displayed in Ember if following is fulfilled:
    ''' List:      - Listname, Slug(identifier of a trakt.tv list) of list is available
    ''' ListItems: - list consist of only movies (episodes not supported right now!), movietitle, IMDB of movie available
    ''' </remarks>
    Private Sub btntraktListsGetPersonal_Click(sender As Object, e As EventArgs) Handles btntraktListsGetPersonal.Click
        'clear globalists, set back controls
        lbtraktListsMoviesinLists.Items.Clear()
        lbtraktLists.Items.Clear()
        traktLists.Clear()
        pnltraktLists.Enabled = False

        'GET all userlists from user
        Dim traktUserLists As IEnumerable(Of TraktAPI.Model.TraktListDetail) = _TraktAPI.UserList_GetLists()

        'check if something was scraped
        If traktUserLists IsNot Nothing Then
            'go through each userlist in scraped listcollection 
            For Each userlist As TraktAPI.Model.TraktListDetail In traktUserLists
                'check if name of list is stored...
                If userlist.Name IsNot Nothing Then
                    'check if slug(identifier of a trakt.tv list) is available
                    Dim listname As String = userlist.Ids.Slug
                    If Not String.IsNullOrEmpty(listname) Then
                        'all required information is there -> GET userlist items
                        Dim traktUserListItems As IEnumerable(Of TraktAPI.Model.TraktListItem) = _TraktAPI.UserList_GetItems(listname)
                        'check if items of userlist are valid and contain required data
                        If traktUserListItems IsNot Nothing AndAlso traktUserListItems.Count >= 0 Then
                            Dim addlist As Boolean = True
                            Dim tmpMovieList As New List(Of TraktAPI.Model.TraktMovie)
                            'if list contains movies (= not empty list)  check if there's only movies - Ember doesn't support episodes right now!
                            For Each item As TraktAPI.Model.TraktListItem In traktUserListItems
                                If item.Type IsNot Nothing AndAlso item.Type = "movie" AndAlso
                                    item.Movie.Title IsNot Nothing AndAlso
                                    Not String.IsNullOrEmpty(item.Movie.Title) AndAlso
                                    item.Movie.Year IsNot Nothing AndAlso
                                    item.Movie.Ids.Imdb IsNot Nothing AndAlso
                                    Not String.IsNullOrEmpty(item.Movie.Ids.Imdb) Then
                                    'valid movie!
                                    Dim tmplistmovie As New TraktAPI.Model.TraktMovie
                                    tmplistmovie.Ids = item.Movie.Ids
                                    tmplistmovie.Title = item.Movie.Title
                                    tmplistmovie.Year = item.Movie.Year
                                    tmpMovieList.Add(tmplistmovie)
                                Else
                                    addlist = False
                                    Exit For
                                End If
                            Next
                            If addlist Then
                                'add valid scraped (movie-)userlist to globallist(including its listitems)
                                Dim tmpTraktSyncList As New TraktAPI.Model.TraktSyncList
                                tmpTraktSyncList.Name = userlist.Name
                                tmpTraktSyncList.Privacy = userlist.Privacy
                                tmpTraktSyncList.Description = userlist.Description
                                tmpTraktSyncList.AllowComments = userlist.AllowComments
                                tmpTraktSyncList.DisplayNumbers = userlist.DisplayNumbers
                                tmpTraktSyncList.Ids = userlist.Ids
                                tmpTraktSyncList.UpdatedAt = userlist.UpdatedAt
                                tmpTraktSyncList.ItemCount = userlist.ItemCount
                                tmpTraktSyncList.Likes = userlist.Likes
                                tmpTraktSyncList.TraktListItems = New List(Of TraktAPI.Model.TraktListItem)
                                tmpTraktSyncList.TraktListItems.AddRange(traktUserListItems)
                                tmpTraktSyncList.Movies = tmpMovieList
                                traktLists.Add(tmpTraktSyncList)
                                logger.Info("[" & tmpTraktSyncList.Name & "] " & "Userlist added to globalist!")
                            Else
                                'invalid list, may contains episodes, invalid movie entrys!
                                logger.Info("Userlist contains invalid data (episodes, missing information(movietitel,IMDB, year is null))")
                            End If
                        Else
                            logger.Info("Invalid items of userlist can't be scraped from trakt.tv!")
                        End If
                    Else
                        logger.Info("Userlist without slug can't be scraped from trakt.tv!")
                    End If
                Else
                    logger.Info("Can't fetch userlist from trakt.tv - Name is empty!")
                End If
            Next
        Else
            logger.Info("Error fetching userlist from trakt.tv!")
        End If

        'if there's any list that could be scraped then fill other listboxes/datagridview and finish loading process
        If traktLists IsNot Nothing AndAlso traktLists.Count > 0 Then

            If dgvMovies.Rows.Count = 0 Then
                'fill movie datagridview
                dgvMovies.SuspendLayout()
                bsMovies.DataSource = Nothing
                dgvMovies.DataSource = Nothing

                If dtMovies.Rows.Count > 0 Then
                    bsMovies.DataSource = dtMovies
                    dgvMovies.DataSource = bsMovies

                    For i As Integer = 0 To dgvMovies.Columns.Count - 1
                        dgvMovies.Columns(i).Visible = False
                    Next

                    dgvMovies.Columns("ListTitle").AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    dgvMovies.Columns("ListTitle").Resizable = DataGridViewTriState.True
                    dgvMovies.Columns("ListTitle").ReadOnly = True
                    dgvMovies.Columns("ListTitle").MinimumWidth = 83
                    dgvMovies.Columns("ListTitle").SortMode = DataGridViewColumnSortMode.Automatic
                    dgvMovies.Columns("ListTitle").Visible = True
                    dgvMovies.Columns("ListTitle").ToolTipText = Master.eLang.GetString(21, "Title")
                    dgvMovies.Columns("ListTitle").HeaderText = Master.eLang.GetString(21, "Title")

                    dgvMovies.Columns("idMovie").ValueType = GetType(Int32)
                End If
                dgvMovies.ResumeLayout()
            End If


            'fill global lbtraktLists - all lists scraped from trakt.tv account!
            For Each list In traktLists
                lbtraktLists.Items.Add(list.Name)
            Next
            'enable filled traktlistbox + "new traktlist" button + movieinlist listbox
            lbtraktLists.Enabled = True
            btntraktListsNewList.Enabled = True
            lbtraktListsMoviesinLists.Enabled = True
            'select first item in listbox
            If lbtraktLists.Items.Count > 0 Then
                lbtraktLists.SelectedIndex = 0
            End If


            If lbDBLists.Items.Count = 0 Then
                'fill lbDBLists(Listbox) - all lists/tags from Ember database!
                Master.DB.FillDataTable(dtMovieTags, String.Concat("SELECT * FROM tag ",
                                                                  "ORDER BY strTag COLLATE NOCASE;"))


                For Each sRow As DataRow In dtMovieTags.Rows
                    If Not String.IsNullOrEmpty(sRow.Item("strTag").ToString) Then
                        lbDBLists.Items.Add(sRow.Item("strTag").ToString)
                    End If
                Next


                'enable filled DBList (if there's any tag/list in database)
                If lbDBLists.Items.Count > 0 Then
                    lbDBLists.Enabled = True
                Else
                    lbDBLists.Enabled = False
                    btntraktListsGetDatabase.Enabled = False
                End If
            End If
            'enable available movie datagridview
            dgvMovies.Enabled = True

        Else
            lbtraktLists.Enabled = False
            btntraktListsNewList.Enabled = False
            lbtraktListsMoviesinLists.Enabled = False
            logger.Info("No userlist(s) available!")
            txttraktListsDetailsName.Text = String.Empty
            txttraktListsDetailsDescription.Text = String.Empty
            chkltraktListsDetailsComments.Checked = False
            chktraktListsDetailsNumbers.Checked = False
        End If
        pnltraktLists.Enabled = True
    End Sub

    ''' <summary>
    ''' POST personal lists/listitems edited in Ember to trakt.tv
    ''' </summary>
    ''' <param name="sender">"Sync List"-Button in Form</param>
    ''' <remarks>
    ''' Send list/items of userlist to trakt.tv
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' 'Sync mechanism:
    '***************
    'Procedure for updating/adding lists on trakt.tv:
    '- 1. If attribute ListDelete = true of list, then delete list on trakt.tv!
    '- 2. If attribute ListDelete = false then POST either NEWLIST_<Listname>(edited existing list) or <Listname>(created list/renamed list) to trakt.tv and add every list which has been edited/created in Ember (Property "ListModified" or/and "ListItemsModified" of a list in traktlist = true)!
    '- 3. If Step 2. success -> POST ListItems to trakt.tv NEWLIST_<Listname>/<Listname> to add movies!
    '-[4. DISABLED FOR NOW! : If Step 3. success -> POST ListDelete <Listname> to delete existing list]
    '-[5. DISABLED FOR NOW! : If Step 4. success -> POST ListUpdate NEWLIST_<Listname> and change name of list to <Listname>]
    ''' </remarks>
    ''' 
    Private Sub btntraktListsSync_Click(sender As Object, e As EventArgs) Handles btntraktListsSyncTrakt.Click
        ' Go through each traktlist in globallists
        Dim response As String = "Do you really want to delete your precious list(s)?! List(s) to delete:" & Environment.NewLine
        Dim listdelete As Boolean = False
        '1. If attribute ListDelete = true of list, then delete list on trakt.tv!
        For i = traktLists.Count - 1 To 0 Step -1
            If traktLists(i).ListDelete AndAlso traktLists(i).Ids IsNot Nothing AndAlso Not String.IsNullOrEmpty(traktLists(i).Ids.Slug) Then
                listdelete = True
                response = response & traktLists(i).Name & Environment.NewLine
            End If
        Next
        If listdelete Then
            Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result = DialogResult.Yes Then
                For i = traktLists.Count - 1 To 0 Step -1
                    If traktLists(i).ListDelete AndAlso traktLists(i).Ids IsNot Nothing AndAlso Not String.IsNullOrEmpty(traktLists(i).Ids.Slug) Then
                        If _TraktAPI.UserList_RemoveList(traktLists(i).Ids.Slug) Then
                            logger.Info("[" & traktLists(i).Ids.Slug & "] " & "List on trakt.tv deleted!")
                            traktLists.RemoveAt(i)
                        Else
                            logger.Info("[" & traktLists(i).Ids.Slug & "] " & "Delete list on trakt.tv FAILED!")
                        End If
                    End If
                Next
            End If
        End If

        response = "Lists created (Please rename them directly in your trakt.tv dashboard!): " & Environment.NewLine
        For Each traktlist In traktLists
            'Check if list qualifies for posting on trak.tv (modified/edited, name must be filled)
            If traktlist.Name IsNot Nothing AndAlso (traktlist.ListItemsModified OrElse traktlist.ListModified OrElse traktlist.NewList) Then
                Dim NameOriginalList As String = traktlist.Name
                Dim SlugOriginalList As String = String.Empty
                SlugOriginalList = traktlist.Ids.Slug
                If String.IsNullOrEmpty(SlugOriginalList) Then
                    SlugOriginalList = TraktMethods.ConvertToSlug(traktlist.Name)
                End If

                '2.POST either NEWLIST_<Listname> (edited existing list) or <Listname> to trakt.tv and add every list which has been edited/created in Ember
                If Not traktlist.NewList Then
                    traktlist.Name = "NEWLIST_" & NameOriginalList
                Else
                    traktlist.Name = NameOriginalList
                End If
                logger.Info("[" & traktlist.Name & "] " & "Send list to trakt.tv!")
                'create traktlist object to store traktlist data in
                Dim tmpTraktList As New TraktAPI.Model.TraktList
                tmpTraktList.Name = traktlist.Name
                tmpTraktList.AllowComments = traktlist.AllowComments
                tmpTraktList.Description = traktlist.Description
                tmpTraktList.DisplayNumbers = traktlist.DisplayNumbers
                tmpTraktList.Privacy = traktlist.Privacy
                Dim traktResponseCreateCustomList = _TraktAPI.UserList_AddList(tmpTraktList)

                '3.POST ListItems to trakt.tv NEWLIST_<Listname> to add movies!
                If traktResponseCreateCustomList IsNot Nothing AndAlso traktResponseCreateCustomList.Ids IsNot Nothing Then
                    If Not String.IsNullOrEmpty(traktResponseCreateCustomList.Ids.Slug) Then
                        traktlist.Ids.Slug = traktResponseCreateCustomList.Ids.Slug
                        Dim traktResponseAddItemsToList As TraktAPI.Model.TraktResponse = Nothing
                        'check if list contains items to submit to trakt.tv
                        If traktlist.Movies.Count > 0 Then
                            logger.Info("[" & traktlist.Ids.Slug & "] " & "Add movies to list on trakt.tv!")
                            Dim tmpTraktSynchronize As New TraktAPI.Model.TraktSynchronize
                            tmpTraktSynchronize.Movies = New List(Of TraktAPI.Model.TraktMovie)
                            tmpTraktSynchronize.Movies = traktlist.Movies
                            traktResponseAddItemsToList = _TraktAPI.UserList_AddItems(traktlist.Ids.Slug, tmpTraktSynchronize)
                        Else
                            logger.Info("[" & traktlist.Ids.Slug & "] " & "No movies in list to add!")
                        End If
                        'Cocotus 2015/02/14: Don't delete existing list since in tests there were problems leading to losing previous list!
                        'so for now only create edits into "NEW_listname" list and leave it at that!
                        response = response & traktlist.Name & Environment.NewLine
                        'After list was posted to trakt.tv, set edited markers back to false (or else this list will get posted whenever user clicks Sync button again!)
                        traktlist.ListModified = False
                        traktlist.ListItemsModified = False
                        traktlist.NewList = False
                        btntraktListsSyncTrakt.Enabled = False

                        ''3. POST ListDelete <Listname> to delete existing list
                        'logger.Info("[" & SlugOriginalList & "] " & "Delete list on trakt.tv!")
                        'Dim traktResponseDeleteUserList = TrakttvAPI.DeleteUserList(_MySettings.Username, SlugOriginalList)
                        ''4. POST ListUpdate NEWLIST_<Listname> and change name of list to <Listname>
                        'If Not traktResponseDeleteUserList Then
                        '    logger.Info("[" & SlugOriginalList & "] " & "Delete list on trakt.tv FAILED!")
                        'End If
                        'Dim tmpTraktListDetail As New TraktAPI.Model.TraktListDetail
                        'tmpTraktListDetail.AllowComments = traktlist.AllowComments
                        'tmpTraktListDetail.Description = traktlist.Description
                        'tmpTraktListDetail.DisplayNumbers = traktlist.DisplayNumbers
                        'tmpTraktListDetail.Name = NameOriginalList
                        'tmpTraktListDetail.Privacy = traktlist.Privacy
                        'Dim traktResponseUpdateCustomList = TrakttvAPI.UpdateCustomList(tmpTraktListDetail, _MySettings.Username, traktlist.Ids.Slug)

                        'If traktResponseUpdateCustomList IsNot Nothing Then
                        '    logger.Info("[" & traktlist.Ids.Slug & "] " & "List updated on trakt.tv!")

                        '    'everything went as planned! Set Modified marker back
                        '    traktlist.ListModified = False
                        '    traktlist.ListItemsModified = False
                        '    traktlist.Ids.Slug = traktResponseUpdateCustomList.Ids.Slug
                        '    traktlist.Name = traktResponseUpdateCustomList.Name
                        'Else
                        '    logger.Info("[" & traktlist.Ids.Slug & "] " & "List updated on trakt.tv FAILED!")
                        'End If
                    Else
                        logger.Info("[" & traktlist.Name & "] " & "No SlugID for created list on trakt.tv!")
                    End If
                Else
                    logger.Info("[" & traktlist.Name & "] " & "Send list to trakt.tv FAILED!")
                End If
            Else
                logger.Info("List without name or not edited lists can't be send to trakt.tv!")
            End If
        Next

        MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK)
        lbtraktLists.Items.Clear()
        'fill global lbtraktLists - all lists scraped from trakt.tv account!
        For Each list In traktLists
            lbtraktLists.Items.Add(list.Name)
        Next
    End Sub

    ''' </remarks>
    ''' <summary>
    ''' Save a trakt.tv list to Ember database/Nfo of movies
    ''' </summary>
    ''' <param name="sender">"Save list to database/Nfo"-Button in Form</param>
    ''' <remarks>
    '''  This sub  handles saving of trakt.tv list into tag-structure of Ember
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' 2015/03/01 Cocotus - Switched to new tag model in Ember database
    ''' </remarks>
    ''' 
    Private Sub btntraktListsSaveToDatabase_Click(sender As Object, e As EventArgs) Handles btntraktListsSaveToDatabase.Click
        Dim listDBID As Integer = -1

        'check if list was selected
        If lbtraktLists.SelectedItems.Count = 1 Then
            'search selected list in globalist
            For Each list In traktLists
                listDBID = -1
                If list.Name = lbtraktLists.SelectedItem.ToString Then
                    'go through each Ember DBList/Tag and check if current traktlist is already in Ember DB. Get TagID of tag if thats the case
                    For Each sRow As DataRow In dtMovieTags.Rows
                        If Not String.IsNullOrEmpty(sRow.Item("strTag").ToString) AndAlso sRow.Item("strTag").ToString = list.Name Then
                            listDBID = CInt(sRow.Item("idTag"))
                            Exit For
                        End If
                    Next

                    'Step 2: create new DBTag object to store current trakt list in
                    Dim currMovieTag As New Structures.DBMovieTag
                    If listDBID = -1 Then
                        'if tag is new and doesn't exist in Ember, create new one with basic information!
                        currMovieTag.ID = -1
                        currMovieTag.Title = list.Name
                        currMovieTag.Movies = New List(Of Database.DBElement)
                    Else
                        'tag already in DB, just edit it! 
                        'load tag from database
                        currMovieTag = Master.DB.Load_Tag_Movie(listDBID)
                        'delete existing movies from tag (we will add again in next step!)
                        If currMovieTag.Movies IsNot Nothing AndAlso currMovieTag.Movies.Count > 0 Then
                            'clear all movies!
                            currMovieTag.Movies.Clear()
                        End If
                    End If

                    'Step 3: go through each movie in current traktlist and add movie to list
                    For Each listmovie In list.Movies
                        'If movie is part of DB in Ember (compare IMDB) add it - else ignore!
                        For Each sRow As DataRow In dtMovies.Rows
                            If Not String.IsNullOrEmpty(sRow.Item("idMovie").ToString) AndAlso "tt" & sRow.Item("Imdb").ToString = listmovie.Ids.Imdb Then
                                Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(CLng(sRow.Item("idMovie")))
                                currMovieTag.Movies.Add(tmpMovie)
                                Exit For
                            End If
                        Next
                    Next

                    'Step 4: save tag to DB
                    If listDBID > -1 Then
                        Master.DB.Save_Tag_Movie(currMovieTag, False, False, True, True)
                    Else
                        Master.DB.Save_Tag_Movie(currMovieTag, True, False, True, True)
                    End If
                    Exit For
                End If
            Next

        End If
    End Sub

    ''' <summary>
    ''' Remove selected movie(s) from selected list
    ''' </summary>
    ''' <param name="sender">"Remove Movie"-Button in Form</param>
    ''' <remarks>
    '''  'Removing a movie from a list means updating "traktLists" - then refresh view of listbox - multiselect in listbox is supported
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' </remarks>
    Private Sub btntraktListsRemove_Click(sender As Object, e As EventArgs) Handles btntraktListsRemove.Click
        If lbtraktListsMoviesinLists.SelectedItems.Count > 0 Then
            For Each selectedmovie In lbtraktListsMoviesinLists.SelectedItems
                'update globallist
                For Each list In traktLists
                    If list.Name = lbtraktLists.SelectedItem.ToString Then
                        For Each movie In list.Movies
                            If movie.Title = selectedmovie.ToString Then
                                list.ListItemsModified = True
                                list.Movies.Remove(movie)
                                btntraktListsSyncTrakt.Enabled = True
                                Exit For
                            End If
                        Next
                        For Each movie In list.TraktListItems
                            If movie.Movie.Title = selectedmovie.ToString Then
                                list.TraktListItems.Remove(movie)
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next

            Next
            LoadSelectedList()
        End If
    End Sub


    ''' <summary>
    ''' Add selected movie to selected list
    ''' </summary>
    ''' <param name="sender">"Add Movie"-Button in Form</param>
    ''' <remarks>
    '''  'Adding a movie to list means updating "traktLists"- then refresh view of listbox 
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' </remarks>
    Private Sub btntraktListsAddMovie_Click(sender As Object, e As EventArgs) Handles btntraktListsAddMovie.Click

        If dgvMovies.SelectedRows.Count > 0 Then
            For Each sRow As DataGridViewRow In dgvMovies.SelectedRows
                Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells(0).Value))
                If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) AndAlso Not lbtraktListsMoviesinLists.Items.Contains(tmpMovie.Movie.Title) Then
                    'create new traktlistitem of selected movie
                    Dim newTraktListItem As New TraktAPI.Model.TraktListItem
                    newTraktListItem = createTraktListItem(tmpMovie)
                    'add new movie to list (global & selectedlist)
                    If newTraktListItem IsNot Nothing Then
                        Dim selList = traktLists.FirstOrDefault(Function(f) f.Name = lbtraktLists.SelectedItem.ToString)
                        If selList IsNot Nothing Then
                            If selList.TraktListItems IsNot Nothing Then
                                selList.TraktListItems.Add(newTraktListItem)
                                'valid movie!
                                Dim tmplistmovie As New TraktAPI.Model.TraktMovie
                                tmplistmovie.Ids = newTraktListItem.Movie.Ids
                                tmplistmovie.Title = newTraktListItem.Movie.Title
                                tmplistmovie.Year = newTraktListItem.Movie.Year
                                selList.Movies.Add(tmplistmovie)
                                selList.ListItemsModified = True
                                btntraktListsSyncTrakt.Enabled = True
                                Exit For
                            Else
                                logger.Info("[" & selList.Name & "] " & tmpMovie.Movie.Title & " is null! Error when trying to add movie to list!")
                            End If
                        End If

                        'don't remove added movie from available movielist - movie can be part of multiple lists!
                        'Me.lbtraktListstMovies.Items.Remove(lbtraktListstMovies.SelectedItems(0))
                        ' bsMovies.Remove(sRow.DataBoundItem)
                    Else
                        logger.Info("[" & lbtraktLists.SelectedItem.ToString & "] " & tmpMovie.Movie.Title & " Created Listitem is invalid! Error when trying to add movie to list!")
                    End If

                End If
            Next

            dgvMovies.ClearSelection()
            dgvMovies.CurrentCell = Nothing
            LoadSelectedList()
        End If
    End Sub

    ''' <summary>
    ''' Create valid traktlistitem from movie container that can be added to existing trakt.tv list
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped</param>
    ''' <returns>New and valid traktlistitem which can be added to traktlist or nothing when valid item could not be created</returns>
    ''' <remarks>
    '''  'Each item needs a type and enough details to identify the item being added. If an item can't be found, it will be ignored. See the example below for each type of item that can be added.
    '''  "type": "movie",
    '''  "imdb_id": "tt0372784",
    '''   "title": "Batman Begins",
    '''   "year": 2005
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' </remarks>
    Private Function createTraktListItem(ByVal DBMovie As Database.DBElement) As TraktAPI.Model.TraktListItem

        Dim traktlistitem As New TraktAPI.Model.TraktListItem
        'now set necessary properties of new traktitem to create a valid list entry
        traktlistitem.Movie = New TraktAPI.Model.TraktMovieSummary
        'type
        traktlistitem.Type = "movie"
        'title
        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            traktlistitem.Movie.Title = DBMovie.Movie.Title
        Else
            Return Nothing
        End If
        'Imdbid
        If DBMovie.Movie.IMDBSpecified Then
            traktlistitem.Movie.Ids = New TraktAPI.Model.TraktMovieBase
            traktlistitem.Movie.Ids.Imdb = DBMovie.Movie.IMDB
        Else
            Return Nothing
        End If
        'year
        Dim nYear As Integer
        If DBMovie.Movie.YearSpecified AndAlso Integer.TryParse(DBMovie.Movie.Year, nYear) Then
            traktlistitem.Movie.Year = nYear
        End If
        Return traktlistitem
    End Function

    ''' <summary>
    ''' Add corresponding movies of selected list to movielistbox 
    ''' </summary>
    ''' <param name="sender">lbtraktLists Changed Event</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/02/09 Cocotus - Fixed for API v2
    ''' </remarks>
    Private Sub lbtraktLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbtraktLists.SelectedIndexChanged

        'clear movieinlist -listbox since we select another one
        lbtraktListsMoviesinLists.Items.Clear()

        'check if list was selected
        If lbtraktLists.SelectedItems.Count > 0 Then
            gbtraktListsDetails.Enabled = True

            'display listtitle in label
            lbltraktListsCurrentList.Text = lbtraktLists.SelectedItem.ToString
            Dim foundlist As Boolean = False
            'search selected list in globalist
            For Each movieinlist In traktLists
                If movieinlist.Name = lbtraktLists.SelectedItem.ToString Then

                    'add all movies from currlist into listbox
                    LoadSelectedList()

                    'Enable remove/edit list buttons
                    btntraktListsEditList.Enabled = True
                    btntraktListsRemoveList.Enabled = True
                    btntraktListsAddMovie.Enabled = True
                    btntraktListsSaveToDatabase.Enabled = True
                    txttraktListsEditList.Enabled = True
                    txttraktListsEditList.Text = lbtraktLists.SelectedItem.ToString
                    foundlist = True
                    Exit For
                End If
            Next

            If Not foundlist Then
                logger.Info("[" & lbtraktLists.SelectedItem.ToString & "] No list selected!")
                lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
                btntraktListsEditList.Enabled = False
                btntraktListsRemoveList.Enabled = False
                btntraktListsAddMovie.Enabled = False
                btntraktListsRemove.Enabled = False
                btntraktListsSaveToDatabase.Enabled = False
                txttraktListsEditList.Text = String.Empty
            End If

            ' no list selected, disable remove/edit list buttons, reset label
        Else
            lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
            btntraktListsEditList.Enabled = False
            btntraktListsRemoveList.Enabled = False
            btntraktListsAddMovie.Enabled = False
            btntraktListsRemove.Enabled = False
            txttraktListsEditList.Text = String.Empty
            gbtraktListsDetails.Enabled = False
            btntraktListsSaveToDatabase.Enabled = False
        End If


    End Sub

    ''' <summary>
    ''' Add corresponding movies of selected list to movielistbox 
    ''' </summary>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub LoadSelectedList()
        lbtraktListsMoviesinLists.SuspendLayout()
        lbtraktListsMoviesinLists.Items.Clear()

        For Each movielist In traktLists
            If movielist.Name = lbtraktLists.SelectedItem.ToString Then

                'Fill Detailview
                txttraktListsDetailsName.Text = movielist.Name
                txttraktListsDetailsDescription.Text = movielist.Description
                chkltraktListsDetailsComments.Checked = movielist.AllowComments
                chktraktListsDetailsNumbers.Checked = movielist.DisplayNumbers
                Select Case movielist.Privacy
                    Case "public"
                        cbotraktListsDetailsPrivacy.SelectedIndex = 0
                    Case "friends"
                        cbotraktListsDetailsPrivacy.SelectedIndex = 1
                    Case "private"
                        cbotraktListsDetailsPrivacy.SelectedIndex = 2
                End Select
                For Each tMovie In movielist.Movies
                    btntraktListsRemove.Enabled = True
                    lbtraktListsMoviesinLists.Items.Add(tMovie.Title)
                Next
                Exit For
            End If
        Next

        lbtraktListsMoviesinLists.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Delete selected list
    ''' </summary>
    ''' <param name="sender">"Remove List" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktListsRemoveList_Click(sender As Object, e As EventArgs) Handles btntraktListsRemoveList.Click
        If lbtraktLists.SelectedItems.Count > 0 Then
            Dim strList As String = lbtraktLists.SelectedItem.ToString
            For Each _list In traktLists
                If _list.Name = strList Then
                    'If list was not scraped but created in Ember(slug = String.Empty), just remove it 
                    If String.IsNullOrEmpty(_list.Ids.Slug) Then
                        traktLists.Remove(_list)
                        'list is already in trakt.tv -> mark as Delete on next Sync
                    Else
                        _list.Name = _list.Name & "_DELETE"
                        _list.ListDelete = True
                        btntraktListsSyncTrakt.Enabled = True
                    End If
                    Exit For
                End If
            Next
            LoadLists()
        End If
    End Sub


    ''' <summary>
    ''' Change name of existing/selected list
    ''' </summary>
    ''' <param name="sender">"Edit List" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktListsEditList_Click(sender As Object, e As EventArgs) Handles btntraktListsEditList.Click
        'check if list is selected (we need one to edit)
        If lbtraktLists.SelectedItems.Count > 0 Then
            'currentlistname
            Dim strList As String = lbtraktLists.SelectedItem.ToString
            'newlistname (from textbox)
            Dim newListname As String = txttraktListsEditList.Text
            'only update if both names(old and new) are available, also don't edit if newname is already a used listname
            If Not String.IsNullOrEmpty(strList) AndAlso Not String.IsNullOrEmpty(newListname) AndAlso Not lbtraktLists.Items.Contains(newListname) Then
                'update listname in globallist
                For Each _list In traktLists
                    If _list.Name = strList Then
                        _list.Name = newListname
                        _list.NewList = True
                        btntraktListsSyncTrakt.Enabled = True
                        Exit For
                    End If
                Next
                'since globallist is updated, we need to load globallist again to reflect changes
                LoadLists()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Add empty list
    ''' </summary>
    ''' <param name="sender">"New List" Button of Form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktListsNewList_Click(sender As Object, e As EventArgs) Handles btntraktListsNewList.Click
        Dim newList As New TraktAPI.Model.TraktSyncList
        'create new list
        newList = CreateNewEmberlist("NewList", "Created by EmberManager")
        If newList IsNot Nothing Then
            btntraktListsSyncTrakt.Enabled = True
            'add created list to existing globallist
            traktLists.Add(newList)
            'since globalist is updated, we need to load globallist again to reflect changes
            LoadLists()
        Else
            logger.Info("New list could not be created!")
        End If
    End Sub

    ''' <summary>
    ''' Transfer selected list of database to traktlists by creating newlist or editing existing one
    ''' </summary>
    ''' <param name="sender">Transfer to Traktlist Button</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/24 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktListsGetDatabase_Click(sender As Object, e As EventArgs) Handles btntraktListsGetDatabase.Click


        'check if list was selected
        If lbDBLists.SelectedItems.Count > 0 Then
            'Search for DBList in trakt.tv lists
            Dim newList As New TraktAPI.Model.TraktSyncList
            'create new list
            newList = CreateNewEmberlist(lbDBLists.SelectedItem.ToString, "Created by EmberManager")
            If newList IsNot Nothing Then
                btntraktListsSyncTrakt.Enabled = True
                'check if listname of DBList also exist on traktlist!
                For Each _list In traktLists
                    If _list.Name = lbDBLists.SelectedItem.ToString Then
                        newList = _list
                        Exit For
                    End If
                Next

                'go through each movie and look if it's part of selected list - if thats the case then create traktlistitem and add to newlist
                Dim TagID As Integer = -1
                For Each sRow As DataRow In dtMovieTags.Rows
                    If sRow.Item("strTag").ToString = lbDBLists.SelectedItem.ToString Then
                        TagID = CInt(sRow.Item("idTag"))
                        Exit For
                    End If
                Next
                If TagID > -1 Then
                    Dim iProg As Integer = 0
                    Dim tmpTag = Master.DB.Load_Tag_Movie(TagID)
                    For Each tmpMovie In tmpTag.Movies
                        If Not String.IsNullOrEmpty(tmpMovie.Movie.Title) Then
                            'create new traktlistitem of selected movie
                            Dim newTraktListItem As New TraktAPI.Model.TraktListItem
                            newTraktListItem = createTraktListItem(tmpMovie)
                            'add new movie to list
                            If newTraktListItem IsNot Nothing Then
                                Dim addmovietolist As Boolean = True
                                For Each movie In newList.Movies
                                    If movie.Title = tmpMovie.Movie.Title Then
                                        addmovietolist = False
                                        Exit For
                                    End If
                                Next
                                If addmovietolist Then
                                    newList.TraktListItems.Add(newTraktListItem)
                                    'valid movie!
                                    Dim tmplistmovie As New TraktAPI.Model.TraktMovie
                                    tmplistmovie.Ids = newTraktListItem.Movie.Ids
                                    tmplistmovie.Title = newTraktListItem.Movie.Title
                                    tmplistmovie.Year = newTraktListItem.Movie.Year
                                    newList.Movies.Add(tmplistmovie)
                                    newList.ListItemsModified = True
                                End If
                            Else
                                logger.Info("[" & lbDBLists.SelectedItem.ToString & "] " & tmpMovie.Movie.Title & " Created Listitem is invalid! Error when trying to add movie to list!")
                            End If
                        End If
                    Next
                End If

                'add created list to existing globallist, remove if already exist to avoid duplicate
                For Each _list In traktLists
                    If _list.Name = lbDBLists.SelectedItem.ToString Then
                        traktLists.Remove(_list)
                        Exit For
                    End If
                Next

                traktLists.Add(newList)
                'since globalist is updated, we need to load globallist again to reflect changes
                LoadLists()
            Else
                logger.Info("New list could not be created!")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Update list details
    ''' </summary>
    ''' <param name="sender">"Update list" Button</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktListsDetailsUpdate_Click(sender As Object, e As EventArgs) Handles btntraktListsDetailsUpdate.Click
        'check if one list is selected
        If lbtraktLists.SelectedItems.Count > 0 Then
            Try
                For Each movielist In traktLists
                    If movielist.Name = lbtraktLists.SelectedItem.ToString Then
                        'update listdetails
                        movielist.Description = txttraktListsDetailsDescription.Text
                        movielist.AllowComments = chkltraktListsDetailsComments.Checked
                        movielist.DisplayNumbers = chktraktListsDetailsNumbers.Checked
                        Select Case cbotraktListsDetailsPrivacy.SelectedIndex
                            Case 0
                                movielist.Privacy = "public"
                            Case 1
                                movielist.Privacy = "friends"
                            Case 2
                                movielist.Privacy = "private"
                        End Select
                        movielist.ListModified = True
                        btntraktListsSyncTrakt.Enabled = True
                        Exit For
                    End If
                Next
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Create empty Trakt.tv list
    ''' </summary>
    ''' <returns>New TraktList with no items(movies)</returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' Important values for new list, i.e of trakt.tv documentation (New lists have no slug information!):
    '"name": "Top 10 of 2011" (The list name. This must be unique.)
    '"description": "These movies and shows really defined 2011 for me." (Optional but recommended description of what the list contains.)
    '"privacy": "public" (Privacy level, set it to private, friends, or public.)
    '"show_numbers": true (optional, The list should show numbers for each item. This is useful for ranked lists. Set it to true or false.)
    '"allow_shouts":true (optional, The list allows discussion by users who have access. Set it to true or false)
    ''' </remarks>
    Private Function CreateNewEmberlist(ByVal listname As String, ByVal listdescription As String, Optional ByVal listprivacy As String = "private") As TraktAPI.Model.TraktSyncList
        Dim newtraktlist As New TraktAPI.Model.TraktSyncList

        'check if List already exists!
        For Each _list In traktLists
            If _list.Name = listname Then
                Return Nothing
            End If
        Next
        'valid movie!
        Dim tmplistmovie As New List(Of TraktAPI.Model.TraktMovie)
        newtraktlist.Movies = tmplistmovie
        newtraktlist.Ids = New TraktAPI.Model.TraktMovieBase

        Dim listslug As String = String.Empty ' TraktMethods.ConvertToSlug(listname)
        Dim listallowshouts As Boolean = False
        Dim listshownumbers As Boolean = False

        newtraktlist.Description = listdescription
        newtraktlist.Name = listname
        newtraktlist.Privacy = listprivacy
        ' newtraktlist.Ids.Slug = listslug
        newtraktlist.AllowComments = listallowshouts
        newtraktlist.DisplayNumbers = listshownumbers
        newtraktlist.NewList = True
        Dim traktlistitems As New List(Of TraktAPI.Model.TraktListItem)
        newtraktlist.TraktListItems = traktlistitems
        Return newtraktlist
    End Function

    ''' <summary>
    ''' Add all lists from globallist to listbox
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/12 Cocotus - First implementation
    ''' </remarks>
    Private Sub LoadLists()
        lbtraktLists.SuspendLayout()

        'first clear all listboxes before adding information again
        lbtraktLists.Items.Clear()
        lbtraktListsMoviesinLists.Items.Clear()
        lbltraktListsCurrentList.Text = Master.eLang.GetString(368, "None Selected")
        'add lists to listbox
        For Each tmptraktlist In traktLists
            lbtraktLists.Items.Add(tmptraktlist.Name)
        Next
        btntraktListsEditList.Enabled = False
        btntraktListsRemoveList.Enabled = False
        btntraktListsAddMovie.Enabled = False
        btntraktListsRemove.Enabled = False
        txttraktListsEditList.Text = String.Empty
        lbtraktLists.ResumeLayout()
    End Sub

    Private Sub tbptraktLists_Enter(sender As Object, e As EventArgs) Handles tbptraktListsSync.Enter
        Activate()
    End Sub

    Private Sub lbDBLists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbDBLists.SelectedIndexChanged
        If lbDBLists.SelectedItems.Count > 0 Then
            btntraktListsGetDatabase.Enabled = True
        Else
            btntraktListsGetDatabase.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Open link using default browser
    ''' </summary>
    ''' <param name="sender">Linklabel click event</param>
    ''' <remarks>
    ''' 2015/02/14 Cocotus - First implementation
    Private Sub lbltraktListsLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbltraktListsLink.LinkClicked
        lbltraktListsLink.LinkVisited = True
        Process.Start("http://trakt.tv/users/" & _TraktAPI.Username & "/lists")
    End Sub

#End Region

#Region "Trakt.tv Listviewer"
    ''' <summary>
    ''' Download trakttv list(s) of your friend(s) and populate combobox with the list(s) of your friend(s)
    ''' </summary>
    ''' <param name="sender">"Load lists of your friends"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub btntraktListsGetFriends_Click(sender As Object, e As EventArgs) Handles btntraktListsGetFriends.Click
        cbotraktListsScraped.Items.Clear()
        userLists.Clear()
        userListURL.Clear()
        userListTitle.Clear()
        Dim traktFriends As IEnumerable(Of TraktAPI.Model.TraktNetworkFriend) = _TraktAPI.GetNetworkFriends()
        If traktFriends IsNot Nothing Then
            For Each tmpfriend In traktFriends
                userLists.Clear()
                Dim traktList As IEnumerable(Of TraktAPI.Model.TraktListDetail) = _TraktAPI.UserList_GetLists(tmpfriend.User.Username)
                If traktLists IsNot Nothing Then
                    userLists.AddRange(traktList)
                    For Each tmplist In userLists
                        userListURL.Add("http://trakt.tv/users/" & tmpfriend.User.Username & "/lists/" & tmplist.Ids.Slug)
                        userListTitle.Add(tmplist.Name)
                    Next
                End If
            Next
            For Each title In userListTitle
                cbotraktListsScraped.Items.Add(title)
            Next
            If cbotraktListsScraped.Items.Count > 0 Then
                cbotraktListsScraped.SelectedIndex = 0
                cbotraktListsScraped.Enabled = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Download trakttv list(s) of users you follow and populate combobox with the list(s)
    ''' </summary>
    ''' <param name="sender">"Load lists of your favorite users"-Button in Form</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub btntraktListsGetFollowers_Click(sender As Object, e As EventArgs) Handles btntraktListsGetFollowers.Click
        cbotraktListsScraped.Items.Clear()
        userListURL.Clear()
        userListTitle.Clear()
        Dim traktFollwing As IEnumerable(Of TraktAPI.Model.TraktNetworkUser) = _TraktAPI.GetNetworkFollowing()
        If traktFollwing IsNot Nothing Then
            For Each tmpfollwing In traktFollwing
                userLists.Clear()
                Dim traktList As IEnumerable(Of TraktAPI.Model.TraktListDetail) = Nothing
                traktList = TrakttvAPI.GetUserLists(tmpfollwing.User.Username)
                If traktLists IsNot Nothing Then
                    userLists.AddRange(traktList)
                    For Each tmplist In userLists
                        userListURL.Add("http://trakt.tv/users/" & tmpfollwing.User.Username & "/lists/" & tmplist.Ids.Slug)
                        userListTitle.Add(tmplist.Name)
                    Next
                End If
            Next
            For Each title In userListTitle
                cbotraktListsScraped.Items.Add(title)
            Next
            If cbotraktListsScraped.Items.Count > 0 Then
                cbotraktListsScraped.SelectedIndex = 0
                cbotraktListsScraped.Enabled = True
            End If
        End If
    End Sub


    ''' <summary>
    ''' Download popular trakttv lists information and display URL and name of list for user
    ''' </summary>
    ''' <param name="sender">"Fetch lists"-Button in Form</param>
    ''' <remarks>
    ''' For now there's no official API support for getting general popular lists on trakt.tv. Therefore download HTML of URl and use Regex to get information needed
    ''' 2014/10/31 Cocotus - First implementation
    ''' 2015/01/01 Cocotus - Right now BROKEN!! Will be fixed when its available again on trak.tv!!
    ''' </remarks>
    Private Sub btntraktListsGetPopular_Click(sender As Object, e As EventArgs) Handles btntraktListsGetPopular.Click

        'Link to scrape list from
        Dim SearchURL As String = "https://trakt.tv/discover#lists"

        Dim sHTTP As New HTTP
        Dim Html As String = sHTTP.DownloadData(SearchURL)
        sHTTP = Nothing

        If Not String.IsNullOrEmpty(Html) Then
            cbotraktListsScraped.Items.Clear()
            userLists.Clear()
            userListTitle.Clear()
            userListURL.Clear()
            'use regexmatched to extract information
            'Example of list information in HTML:
            '<div class="title-overflow"></div>
            '<a href="/user/listr/lists/500-essential-cult-movies-the-ultimate-guide-by-jennifer-eiss">500 Essential Cult Movies: The Ultimate Guide By Jennifer Eiss</a>
            '</h3>

            Dim sPattern As String = "<div class=""title-overflow""></div>.*?href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
            Dim sResult As MatchCollection = Regex.Matches(Html, sPattern, RegexOptions.Singleline)
            For ctr As Integer = 0 To sResult.Count - 1
                If Not String.IsNullOrEmpty(sResult.Item(ctr).Groups(2).Value) AndAlso Not String.IsNullOrEmpty(sResult.Item(ctr).Groups(1).Value) Then
                    userListTitle.Add(sResult.Item(ctr).Groups(2).Value)
                    userListURL.Add(sResult.Item(ctr).Groups(1).Value)
                    cbotraktListsScraped.Items.Add(sResult.Item(ctr).Groups(2).Value)
                End If
            Next
            If cbotraktListsScraped.Items.Count > 0 Then
                cbotraktListsScraped.SelectedIndex = 0
                cbotraktListsScraped.Enabled = True
            End If
        Else
            logger.Debug("[http://trakt.tv/lists/personal/popular/weekly] HTML could not be downloaded!")
        End If


    End Sub
    ''' <summary>
    ''' Load selected traktlist into datagrid
    ''' </summary>
    ''' <param name="sender">"Load list" button of form</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' load userlist into datagrid
    ''' 2014/10/12 Cocotus - First implementation
    ''' 2015/01/25 Cocotus, Fixed for v2 API
    ''' </remarks>
    Private Sub btntraktListLoad_Click(sender As Object, e As EventArgs) Handles btntraktListLoad.Click
        'reset controls and labels
        chktraktListsCompare.Checked = False
        dgvtraktList.DataSource = Nothing
        dgvtraktList.Rows.Clear()
        btntraktListsSaveList.Enabled = False
        btntraktListsSavePlaylist.Enabled = False
        btntraktListsSendToKodi.Enabled = False
        btntraktListsSaveListCompare.Enabled = False
        chktraktListsCompare.Enabled = False
        lbltraktListsCount.Visible = False
        userListItems.Clear()
        userList = Nothing
        userLists.Clear()
        'display description of list
        lbltraktListDescriptionText.Text = String.Empty
        If Not String.IsNullOrEmpty(txttraktListURL.Text) Then
            'Check if url is valid, i.e http://trakt.tv/users/nielsz/lists/active-imdb-top-250
            Dim tmp As String = txttraktListURL.Text
            If tmp.IndexOf("users/") > 0 Then
                tmp = tmp.Remove(0, tmp.IndexOf("users/") + 6)
                Dim parts As String() = tmp.Split(New String() {"/"}, StringSplitOptions.None)
                If parts.Length = 3 Then
                    Dim traktListItems As IEnumerable(Of TraktAPI.Model.TraktListItem) = _TraktAPI.UserList_GetItems(parts(0), parts(2), "full")
                    If traktListItems IsNot Nothing Then
                        userListItems.AddRange(traktListItems)
                        Dim traktList As IEnumerable(Of TraktAPI.Model.TraktListDetail) = Nothing
                        traktList = TrakttvAPI.GetUserLists(parts(0))
                        If traktLists IsNot Nothing Then
                            userLists.AddRange(traktList)
                            For Each tmplist In userLists
                                If tmplist.Ids.Slug = parts(2) Then
                                    userList = tmplist
                                End If
                            Next
                        End If
                    End If
                Else
                    logger.Info("[" & txttraktListURL.Text & "] " & "Invalid URL!")
                End If
            Else
                logger.Info("[" & txttraktListURL.Text & "] " & "Invalid URL!")
            End If


            'Check if current userlist is valid and contains required data
            If userListItems IsNot Nothing AndAlso userList IsNot Nothing Then
                '  lbtraktLists.Items.Clear()

                'fill rows
                'we map to dgv manually
                dgvtraktList.AutoGenerateColumns = False
                Dim debugcount As Integer = userListItems.Count - 1
                For i = userListItems.Count - 1 To 0 Step -1
                    debugcount = debugcount - 1
                    'if list contains movies (= not empty list)  check if there's only movies - Ember doesn't support episodes right now! 'userListItems(i).Movie.Rating.ToString '
                    If userListItems(i).Type IsNot Nothing AndAlso userListItems(i).Type = "movie" AndAlso userListItems(i).Movie IsNot Nothing AndAlso userListItems(i).Movie.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(userListItems(i).Movie.Title) AndAlso userListItems(i).Movie.Ids IsNot Nothing AndAlso Not String.IsNullOrEmpty(userListItems(i).Movie.Ids.Imdb) Then
                        'valid movie!

                        'Dim traktShowProgress As TraktAPI.Model.TraktShowProgress = TrakttvAPI.GetProgressSh(watchedtvshow.Show.Ids.Trakt.ToString)
                        'If traktShowProgress IsNot Nothing Then
                        '    traktShowsProgress.Add(traktShowProgress)
                        'End If

                        If userListItems(i).Movie.Rating Is Nothing Then
                            If userListItems(i).Movie.Genres Is Nothing Then
                                dgvtraktList.Rows.Add(New Object() {userListItems(i).Movie.Title, userListItems(i).Movie.Year, String.Empty, String.Empty, userListItems(i).Movie.Ids.Imdb, userListItems(i).Movie.Trailer})
                            Else
                                Dim genrestring As String = String.Empty
                                For Each genre In userListItems(i).Movie.Genres
                                    If Not String.IsNullOrEmpty(genre) Then
                                        genrestring = genrestring & genre & "/"
                                    End If
                                Next
                                If genrestring.EndsWith("/") Then
                                    genrestring = genrestring.Remove(genrestring.Length - 1)
                                End If
                                dgvtraktList.Rows.Add(New Object() {userListItems(i).Movie.Title, userListItems(i).Movie.Year, String.Empty, genrestring, userListItems(i).Movie.Ids.Imdb, userListItems(i).Movie.Trailer})
                            End If
                        Else
                            userListItems(i).Movie.Rating = Math.Round(CDbl(userListItems(i).Movie.Rating), 1)
                            If userListItems(i).Movie.Genres Is Nothing Then
                                dgvtraktList.Rows.Add(New Object() {userListItems(i).Movie.Title, userListItems(i).Movie.Year, userListItems(i).Movie.Rating.ToString, String.Empty, userListItems(i).Movie.Ids.Imdb, userListItems(i).Movie.Trailer})
                            Else

                                Dim genrestring As String = String.Empty
                                For Each genre In userListItems(i).Movie.Genres
                                    If Not String.IsNullOrEmpty(genre) Then
                                        genrestring = genrestring & genre & "/"
                                    End If
                                Next
                                If genrestring.EndsWith("/") Then
                                    genrestring = genrestring.Remove(genrestring.Length - 1)
                                End If
                                dgvtraktList.Rows.Add(New Object() {userListItems(i).Movie.Title, userListItems(i).Movie.Year, userListItems(i).Movie.Rating.ToString, genrestring, userListItems(i).Movie.Ids.Imdb, userListItems(i).Movie.Trailer})
                            End If

                        End If

                    Else
                        logger.Info("[" & txttraktListURL.Text & "] Movie with Index: " & debugcount.ToString & " Invalid entry could not be added. Not a valid movie!")
                        userListItems.RemoveAt(i)
                    End If
                Next

                logger.Info("[" & txttraktListURL.Text & "] " & "Userlist loaded!")

                'after filling datagrid, enable controls
                btntraktListsSaveList.Enabled = True
                btntraktListsSaveListCompare.Enabled = True
                chktraktListsCompare.Enabled = True
                lbltraktListsCount.Visible = True
                lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ": " & dgvtraktList.Rows.Count

                'only enable creation of KODI playlist when necessary setup in KODI interface was done by user
                If _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count > 0 Then
                    btntraktListsSavePlaylist.Enabled = True
                    'TODO Currently offline until team Kodi fixes playlist sync!
                    btntraktListsSendToKodi.Enabled = False
                End If

                'display description of list
                If Not String.IsNullOrEmpty(userList.Description) Then
                    lbltraktListDescriptionText.Text = userList.Description
                End If
            Else
                'invalid list, may contains episodes, invalid movie entrys!
                logger.Info("[" & txttraktListURL.Text & "] " & "Invalid list! (episodes, invalid movie entries!)")
            End If

        Else
            logger.Info("Missing URL to list - no scraping possible!")
        End If
    End Sub

    ''' <summary>
    ''' Trigger creation of HTML page which list all movies of the traktlist
    ''' </summary>
    ''' <remarks>
    ''' 2014/10/24 Cocotus - First implementation
    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    ''' </remarks>
    Private Sub btntraktListsSaveList_Click(sender As Object, e As EventArgs) Handles btntraktListsSaveList.Click

        Dim HTMLPage As String = BuildHTML(userListItems, False)
        If Not String.IsNullOrEmpty(HTMLPage) Then
            File.WriteAllText(Path.Combine(Master.TempPath, "index.html"), HTMLPage)
            Functions.Launch(Path.Combine(Master.TempPath, "index.html"), True)
        Else
            logger.Info("HTMLPage could not be created!")
        End If

    End Sub

    ''' <summary>
    ''' Trigger creation of .m3u playlist which contains all movies from the loaded trakt.tv list that also exist in Ember database
    ''' </summary>
    ''' <remarks>
    ''' 2015/11/29 Cocotus - First implementation
    ''' button is only enabled when Kodi Interface settings were configured by user
    ''' We use the data in kodi interface settings to build the valid remotepath and a valid playlist for KODI
    ''' </remarks>
    Private Sub btntraktListsSavePlaylist_Click(sender As Object, e As EventArgs) Handles btntraktListsSavePlaylist.Click
        ' Load module settings of kodi interface to build remote paths
        If _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count > 0 Then
            Dim m3uString As String = "#EXTM3U" & Environment.NewLine
            Dim basedirectory As String = String.Empty
            For Each item In userListItems
                'search movie in global moviedatatable
                For Each sRow As DataRow In dtMovies.Rows
                    If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                        If item.Movie.Ids.Imdb = "tt" & sRow.Item("IMDB").ToString Then
                            basedirectory = GetRemotePath(Directory.GetParent(sRow.Item("MoviePath").ToString).FullName)
                            If basedirectory.LastIndexOf("/"c) > basedirectory.LastIndexOf("\"c) Then
                                m3uString = m3uString & "#EXTINF:0," & sRow.Item("Title").ToString & Environment.NewLine & basedirectory & "/" & (Path.GetFileName((sRow.Item("MoviePath").ToString)).Trim) & Environment.NewLine
                            Else
                                m3uString = m3uString & "#EXTINF:0," & sRow.Item("Title").ToString & Environment.NewLine & basedirectory & "\" & (Path.GetFileName((sRow.Item("MoviePath").ToString)).Trim) & Environment.NewLine
                            End If
                            Exit For
                        End If
                    Else
                        logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be compared against library!")
                    End If
                Next
            Next

            'If Not String.IsNullOrEmpty(m3uString) Then
            '    File.WriteAllText(Path.Combine(Master.TempPath, "traktplaylist.m3u"), m3uString)
            '    Functions.Launch(Path.Combine(Master.TempPath, "traktplaylist.m3u"), True)
            'End If
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.FileName = "TraktPlaylistForKODI" + ".m3u"
            saveFileDialog1.Filter = "m3u files (*.m3u)|*.m3u"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True
            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                File.WriteAllText(saveFileDialog1.FileName, m3uString)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Trigger creation of HTML page which list only unknown movies
    ''' </summary>
    ''' <returns>HTML page as string, if empty then there was an error during build process</returns>
    ''' <remarks>
    ''' 2014/10/24 Cocotus - First implementation
    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    ''' </remarks>
    Private Sub btntraktListsSaveListCompare_Click(sender As Object, e As EventArgs) Handles btntraktListsSaveListCompare.Click

        Dim HTMLPage As String = BuildHTML(userListItems, True)
        If Not String.IsNullOrEmpty(HTMLPage) Then
            File.WriteAllText(Path.Combine(Master.TempPath, "index.html"), HTMLPage)
            Functions.Launch(Path.Combine(Master.TempPath, "index.html"), True)
        Else
            logger.Info("HTMLPage could not be created!")
        End If

    End Sub

    ''' <summary>
    ''' Create HTML page to display traktlistitems (movies)
    ''' </summary>
    ''' <param name="list">loaded trakttv list</param>
    ''' <param name="exportonlyunknownmovies">true=only movies which are not in Ember database will be used, false=all movies from list will be used</param>
    ''' <returns>HTML page as string, if empty then there was an error during build process</returns>
    ''' <remarks>
    ''' 2014/10/24 Cocotus - First implementation
    ''' For now a simple HTML page with basic information (Title,Trailerlink,IMDBlink,Year,Rating,Overview,Genres)
    ''' </remarks>
    Private Function BuildHTML(ByVal userlistitem As List(Of TraktAPI.Model.TraktListItem), ByVal exportonlyunknownmovies As Boolean) As String

        Dim HTML As String = String.Empty
        Dim HTMLHEADER As String = "<html><body><table><thead><tr><tr><th>" & Master.eLang.GetString(21, "Title") & "</th><th>" & Master.eLang.GetString(278, "Year") & "</th><th>" & Master.eLang.GetString(400, "Rating") & "</th><th>" & Master.eLang.GetString(20, "Genre") & "</th><th>" & Master.eLang.GetString(64, "Overview") & "</th><th>" & Master.eLang.GetString(885, "IMDB") & "</th><th>" & Master.eLang.GetString(151, "Trailer") & "</th></tr></thead>"
        Dim HTMLDATAROW As String = String.Empty
        Dim HTMLFOOTER As String = "</table></body></html>"

        If exportonlyunknownmovies Then

            For Each item In userlistitem
                Dim foundmovieinlibrary As Boolean = False
                'search movie in global moviedatatable
                For Each sRow As DataRow In dtMovies.Rows
                    If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                        If item.Movie.Ids.Imdb = "tt" & sRow.Item("IMDB").ToString Then
                            foundmovieinlibrary = True
                            Exit For
                        End If
                    Else
                        logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be compared against library!")
                    End If
                Next
                If Not foundmovieinlibrary Then
                    If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                        If item.Movie.Rating Is Nothing Then
                            If item.Movie.Genres Is Nothing Then
                                HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & "" & "</td><td>" & "" & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                            Else
                                HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & "" & "</td><td>" & String.Join("/", item.Movie.Genres.ToArray).Trim & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                            End If
                        Else
                            If item.Movie.Genres Is Nothing Then
                                HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & item.Movie.Rating.ToString & "</td><td>" & "" & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                            Else
                                Dim genrestring As String = String.Empty
                                For Each genre In item.Movie.Genres
                                    If Not String.IsNullOrEmpty(genre) Then
                                        genrestring = genrestring & genre & "/"
                                    End If
                                Next
                                If genrestring.EndsWith("/") Then
                                    genrestring = genrestring.Remove(genrestring.Length - 1)
                                End If
                                HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & item.Movie.Rating.ToString & "</td><td>" & genrestring & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                            End If
                        End If
                    Else
                        logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be added to HTML!")
                    End If

                Else
                    logger.Info("[" & item.Movie.Title & "] " & "Movie already in library!")
                End If
            Next
        Else
            For Each item In userlistitem
                If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                    If item.Movie.Rating Is Nothing Then
                        If item.Movie.Genres Is Nothing Then
                            HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & "" & "</td><td>" & "" & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                        Else
                            HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & "" & "</td><td>" & String.Join("/", item.Movie.Genres.ToArray).Trim & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                        End If
                    Else
                        If item.Movie.Genres Is Nothing Then
                            HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & item.Movie.Rating.ToString & "</td><td>" & "" & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                        Else
                            Dim genrestring As String = String.Empty
                            For Each genre In item.Movie.Genres
                                If Not String.IsNullOrEmpty(genre) Then
                                    genrestring = genrestring & genre & "/"
                                End If
                            Next
                            If genrestring.EndsWith("/") Then
                                genrestring = genrestring.Remove(genrestring.Length - 1)
                            End If
                            HTMLDATAROW = HTMLDATAROW & "<tr><td>" & item.Movie.Title & "</td><td>" & item.Movie.Year & "</td><td>" & item.Movie.Rating.ToString & "</td><td>" & genrestring & "</td><td>" & item.Movie.Overview & "</td><td><a href=""http://www.imdb.com/title/" & item.Movie.Ids.Imdb & """>IMDB</a></td><td><a href=""" & item.Movie.Trailer & """>Trailer</a></td></tr>"
                        End If
                    End If
                Else
                    logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be added to HTML!")
                End If

            Next
        End If
        HTML = HTMLHEADER & HTMLDATAROW & HTMLFOOTER
        Return HTML
    End Function
    ''' <summary>
    ''' Update datagridview and show either all entries of loaded list or only unknown movies
    ''' </summary>
    ''' <param name="list">Checked state changed event of checkbox</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 2014/10/24 Cocotus - First implementation
    ''' </remarks>
    Private Sub chktraktListsCompare_CheckedChanged(sender As Object, e As EventArgs) Handles chktraktListsCompare.CheckedChanged

        dgvtraktList.DataSource = Nothing
        dgvtraktList.Rows.Clear()

        If chktraktListsCompare.Checked Then


            For Each item In userListItems
                Dim foundmovieinlibrary As Boolean = False
                'search movie in globalist
                If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                    For Each sRow As DataRow In dtMovies.Rows
                        If item.Movie.Ids.Imdb = "tt" & sRow.Item("IMDB").ToString Then
                            foundmovieinlibrary = True
                            Exit For
                        End If
                    Next
                Else
                    logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be compared against library!")
                End If

                If Not foundmovieinlibrary Then
                    If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                        'valid movie!
                        If item.Movie.Rating Is Nothing Then
                            If item.Movie.Genres Is Nothing Then
                                dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, String.Empty, String.Empty, item.Movie.Ids.Imdb, item.Movie.Trailer})
                            Else
                                dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, String.Empty, String.Join("/", item.Movie.Genres.ToArray).Trim, item.Movie.Ids.Imdb, item.Movie.Trailer})
                            End If
                        Else
                            If item.Movie.Genres Is Nothing Then
                                dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, item.Movie.Rating.ToString, String.Empty, item.Movie.Ids.Imdb, item.Movie.Trailer})
                            Else
                                Dim genrestring As String = String.Empty
                                For Each genre In item.Movie.Genres
                                    If Not String.IsNullOrEmpty(genre) Then
                                        genrestring = genrestring & genre & "/"
                                    End If
                                Next
                                If genrestring.EndsWith("/") Then
                                    genrestring = genrestring.Remove(genrestring.Length - 1)
                                End If
                                dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, item.Movie.Rating.ToString, genrestring, item.Movie.Ids.Imdb, item.Movie.Trailer})
                            End If

                        End If
                    Else
                        logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be added to datagrid!")
                    End If
                Else
                    logger.Info("[" & item.Movie.Title & "] " & "Movie already in library!")
                End If
            Next
        Else
            'fill rows
            For Each item In userListItems
                If item IsNot Nothing AndAlso item.Movie IsNot Nothing Then
                    'valid movie!
                    If item.Movie.Rating Is Nothing Then
                        If item.Movie.Genres Is Nothing Then
                            dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, String.Empty, String.Empty, item.Movie.Ids.Imdb, item.Movie.Trailer})
                        Else
                            dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, String.Empty, String.Join("/", item.Movie.Genres.ToArray).Trim, item.Movie.Ids.Imdb, item.Movie.Trailer})
                        End If
                    Else
                        If item.Movie.Genres Is Nothing Then
                            dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, item.Movie.Rating.ToString, String.Empty, item.Movie.Ids.Imdb, item.Movie.Trailer})
                        Else
                            Dim genrestring As String = String.Empty
                            For Each genre In item.Movie.Genres
                                If Not String.IsNullOrEmpty(genre) Then
                                    genrestring = genrestring & genre & "/"
                                End If
                            Next
                            If genrestring.EndsWith("/") Then
                                genrestring = genrestring.Remove(genrestring.Length - 1)
                            End If
                            dgvtraktList.Rows.Add(New Object() {item.Movie.Title, item.Movie.Year, item.Movie.Rating.ToString, genrestring, item.Movie.Ids.Imdb, item.Movie.Trailer})
                        End If

                    End If
                Else
                    logger.Info("[" & item.Movie.Title & "] " & "Movie Item is Nothing! Could not be added to datagrid!")
                End If
            Next
        End If

        lbltraktListsCount.Text = Master.eLang.GetString(794, "Search Results") & ": " & dgvtraktList.Rows.Count
    End Sub

    ''' <summary>
    ''' Enable/Disable Loadlist button logic
    ''' </summary>
    ''' <param name="sender">Textchanged Event of URL textbox</param>
    ''' <remarks>
    ''' 2014/10/31 Cocotus - First implementation
    Private Sub txttraktListsurl_TextChanged(sender As Object, e As EventArgs) Handles txttraktListURL.TextChanged
        btntraktListRemoveFavorite.Enabled = False
        If Not String.IsNullOrEmpty(txttraktListURL.Text) Then
            btntraktListLoad.Enabled = True

            If Not String.IsNullOrEmpty(cbotraktListsScraped.Text) Then
                btntraktListSaveFavorite.Enabled = True
            End If
            Dim mylists As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
            If mylists IsNot Nothing Then
                Using settings = New AdvancedSettings()
                    For Each sett In mylists
                        If sett.Value = txttraktListURL.Text Then
                            btntraktListRemoveFavorite.Enabled = True
                        End If
                    Next
                End Using
            End If
        Else
            btntraktListLoad.Enabled = False
            btntraktListSaveFavorite.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Open link in datagrid using default browser
    ''' </summary>
    ''' <param name="sender">Cell click event</param>
    ''' <remarks>
    ''' 2014/10/31 Cocotus - First implementation
    Private Sub dgvtraktList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvtraktList.CellContentClick
        If e.RowIndex > -1 AndAlso e.ColumnIndex = 4 AndAlso dgvtraktList.CurrentCell.RowIndex > -1 Then
            System.Diagnostics.Process.Start("http://www.imdb.com/title/" & dgvtraktList.CurrentCell.Value.ToString())
        ElseIf e.RowIndex > -1 AndAlso e.ColumnIndex = 5 AndAlso dgvtraktList.CurrentCell.RowIndex > -1 Then
            System.Diagnostics.Process.Start(dgvtraktList.CurrentCell.Value.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Populate URL Textbox with your selected saved trakt.tv list
    ''' </summary>
    ''' <param name="sender">SelectionChanged Event of favorite list combobox</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub cbotraktListsFavorites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotraktListsFavorites.SelectedIndexChanged
        Dim mylists As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("TraktFavoriteLists", "generic.EmberCore.Trakt")
        If mylists IsNot Nothing Then
            Using settings = New AdvancedSettings()
                For Each sett In mylists
                    If sett.Name = cbotraktListsFavorites.SelectedItem.ToString Then
                        txttraktListURL.Text = sett.Value
                        btntraktListRemoveFavorite.Enabled = True
                    End If
                Next
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Populate URL Textbox with your selected trakt.tv list
    ''' </summary>
    ''' <param name="sender">SelectionChanged Event of favorite list combobox</param>
    ''' <remarks>
    ''' 2015/01/01 Cocotus
    ''' </remarks>
    Private Sub cbotraktListsScraped_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbotraktListsScraped.SelectedIndexChanged
        If Not String.IsNullOrEmpty(cbotraktListsScraped.SelectedItem.ToString) Then
            txttraktListURL.Text = userListURL.Item(cbotraktListsScraped.SelectedIndex)
        End If
    End Sub

    ''' <summary>
    ''' Return remote/Kodi path to the base directory of a specific video file
    ''' </summary>
    ''' <param name="LocalPath">path to base directory as it is stored in Ember database</param>
    ''' <returns>remote path of videofile used in Kodi (base directory)</returns>
    ''' <remarks>
    ''' 2015/11/29 Basically a 1:1 copy of existing function in KodiAPI module
    ''' Used to get KODI path because we need it to build a valid playlist for Kodi
    ''' ATTENTION: It's not allowed to use "Remotepath.ToLower" (Kodi can't find UNC sources with wrong case)
    ''' </remarks>
    Function GetRemotePath(ByVal strLocalPath As String) As String
        Dim RemotePath As String = String.Empty
        Dim RemoteIsUNC As Boolean = False
        Dim pathfound As Boolean = False
        For Each host In _SpecialSettings.Hosts
            If pathfound Then
                Exit For
            End If
            For Each Source In host.Sources
                Dim tLocalSource As String = String.Empty
                'add a directory separator at the end of the path to distinguish between
                'D:\Movies
                'D:\Movies Shared
                '(needed for "LocalPath.ToLower.StartsWith(tLocalSource)"
                If Source.LocalPath.Contains(Path.DirectorySeparatorChar) Then
                    tLocalSource = If(Source.LocalPath.EndsWith(Path.DirectorySeparatorChar), Source.LocalPath, String.Concat(Source.LocalPath, Path.DirectorySeparatorChar)).Trim
                ElseIf Source.LocalPath.Contains(Path.AltDirectorySeparatorChar) Then
                    tLocalSource = If(Source.LocalPath.EndsWith(Path.AltDirectorySeparatorChar), Source.LocalPath, String.Concat(Source.LocalPath, Path.AltDirectorySeparatorChar)).Trim
                End If
                If strLocalPath.ToLower.StartsWith(tLocalSource.ToLower) Then
                    Dim tRemoteSource As String = String.Empty
                    If Source.RemotePath.Contains(Path.DirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.DirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.DirectorySeparatorChar)).Trim
                    ElseIf Source.RemotePath.Contains(Path.AltDirectorySeparatorChar) Then
                        tRemoteSource = If(Source.RemotePath.EndsWith(Path.AltDirectorySeparatorChar), Source.RemotePath, String.Concat(Source.RemotePath, Path.AltDirectorySeparatorChar)).Trim
                        RemoteIsUNC = True
                    End If
                    RemotePath = strLocalPath.Replace(tLocalSource, tRemoteSource)
                    If RemoteIsUNC Then
                        RemotePath = RemotePath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    Else
                        RemotePath = RemotePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
                    End If
                    Exit For
                    pathfound = True
                End If
            Next
            If String.IsNullOrEmpty(RemotePath) Then logger.Error(String.Format("[dlgTrakttvManager] [{0}] GetRemotePath: ""{1}"" | Source not mapped!", host.Label, strLocalPath))
        Next

        Return RemotePath
    End Function

#End Region 'Trakt.tv Listviewer

#Region "Trakt.tv Comments"

    ''' <summary>
    ''' GET comments of movies from trakt.tv and display in Datagridview
    ''' </summary>
    ''' <param name="sender">"Load your movie comments"-Button in Form</param>
    ''' <remarks>
    ''' 2015/06/01 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktCommentsGet_Click(sender As Object, e As EventArgs) Handles btntraktCommentsGet.Click
        Try
            If myCommentsMovies IsNot Nothing Then
                myCommentsMovies.Clear()
            End If
            dgvtraktComments.DataSource = Nothing
            dgvtraktComments.Rows.Clear()

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'GET movie comments of user
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
            Dim traktMovieComments As IEnumerable(Of TraktAPI.Model.TraktCommentItem) = _TraktAPI.Comment_GetComments("all", "movies")

            If traktMovieComments IsNot Nothing Then
                For Each Item As TraktAPI.Model.TraktCommentItem In traktMovieComments
                    'Check if information is stored...
                    If Item.Movie.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(Item.Movie.Title) AndAlso Item.Movie.Ids.Imdb IsNot Nothing AndAlso Not String.IsNullOrEmpty(Item.Movie.Ids.Imdb) Then
                        Dim myDateString As String = Item.Comment.CreatedAt
                        Dim myDate As Date
                        Dim isDate As Boolean = Date.TryParse(myDateString, myDate)
                        If isDate Then
                            Item.Comment.CreatedAt = myDate.ToString("yyyy-MM-dd HH:mm:ss")
                        End If
                        myCommentsMovies.Add(Item)
                    End If
                Next

                'Set up /load listofwatchedmovies into datagridview
                btntraktCommentsDetailsDelete.Enabled = False
                btntraktCommentsDetailsSend.Enabled = False
                btntraktCommentsDetailsUpdate.Enabled = False
                'we map to dgv manually
                dgvtraktWatchlist.AutoGenerateColumns = False
                Dim HasCommentOnTrakt As Boolean = False
                For Each sRow As DataRow In dtMovies.Rows
                    If Not String.IsNullOrEmpty(sRow.Item("Title").ToString) AndAlso Not String.IsNullOrEmpty(sRow.Item("Imdb").ToString) Then
                        HasCommentOnTrakt = False
                        If myCommentsMovies IsNot Nothing Then
                            For Each Item As TraktAPI.Model.TraktCommentItem In myCommentsMovies
                                If Item.Movie.Ids.Imdb = "tt" & sRow.Item("Imdb").ToString Then
                                    'listed-At is not user friendly formatted, so change format a bit
                                    '"listed_at": 2014-09-01T09:10:11.000Z (original)
                                    'new format here: 2014-09-01  09:10:11
                                    Dim myDateString As String = Item.Comment.CreatedAt
                                    Dim myDate As Date
                                    Dim isDate As Boolean = Date.TryParse(myDateString, myDate)
                                    If isDate Then
                                        'dgvtraktComments.Rows.Add(New Object() {sRow.Item("Title").ToString, myDate.ToString("yyyy-MM-dd hh:mm"), Item.Comment.Replies, Item.Comment.Likes, Item.Comment.Id, sRow.Item("Imdb").ToString})
                                        dgvtraktComments.Rows.Add(New Object() {sRow.Item("Title").ToString, Item.Comment.CreatedAt, Item.Comment.Replies, Item.Comment.Likes, Item.Comment.Id, sRow.Item("Imdb").ToString})
                                    Else
                                        dgvtraktComments.Rows.Add(New Object() {sRow.Item("Title").ToString, Item.Comment.CreatedAt, Item.Comment.Replies, Item.Comment.Likes, Item.Comment.Id, sRow.Item("Imdb").ToString})
                                    End If
                                    HasCommentOnTrakt = True
                                    Exit For
                                End If
                            Next
                        End If
                        If Not HasCommentOnTrakt Then
                            dgvtraktComments.Rows.Add(New Object() {sRow.Item("Title").ToString, "", "", "", "", sRow.Item("Imdb").ToString})
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            If myCommentsMovies IsNot Nothing Then
                myCommentsMovies.Clear()
            End If
            dgvtraktComments.DataSource = Nothing
            dgvtraktComments.Rows.Clear()
        End Try
    End Sub

    ''' <summary>
    '''  Send comment to trakt.tv
    ''' </summary>
    ''' <param name="sender">"Submit comment to trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' 2015/06/01 Cocotus - First implementation
    ''' This sub  handles submitting rcomments of movies to trakt.tv
    ''' </remarks>
    Private Sub btntraktCommentsDetailsSend_Click(sender As Object, e As EventArgs) Handles btntraktCommentsDetailsSend.Click
        Dim response As String = Master.eLang.GetString(1411, "Submit comment to trakt.tv") & "? " & Master.eLang.GetString(36, "Movies") & ":" & Environment.NewLine
        Dim tmpTraktSynchronize As New TraktAPI.Model.TraktCommentMovie
        tmpTraktSynchronize.Movie = New TraktAPI.Model.TraktMovie
        tmpTraktSynchronize.Movie.Ids = New TraktAPI.Model.TraktMovieBase
        tmpTraktSynchronize.Movie.Ids.Imdb = "tt" & dgvtraktComments.CurrentRow.Cells(5).Value.ToString
        tmpTraktSynchronize.Movie.Title = dgvtraktComments.CurrentRow.Cells(0).Value.ToString
        tmpTraktSynchronize.Text = txttraktCommentsDetailsComment.Text
        tmpTraktSynchronize.IsSpoiler = chktraktCommentsDetailsSpoiler.Checked
        response = response & dgvtraktComments.CurrentRow.Cells(0).Value.ToString & Environment.NewLine

        Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If result = Windows.Forms.DialogResult.Yes Then
            Dim traktResponse = _TraktAPI.Comment_AddMovie(tmpTraktSynchronize)
            If traktResponse IsNot Nothing Then
                If traktResponse.Id > 0 Then
                    logger.Info("Added comment to trakt.tv!")
                    response = Master.eLang.GetString(1412, "Added comment to trakt.tv!")
                    btntraktCommentsGet_Click(Nothing, Nothing)
                Else
                    logger.Info("No comment submitted!")
                    response = Master.eLang.GetString(1134, "Error!")
                End If
            Else
                response = Master.eLang.GetString(1134, "Error!")
            End If
            MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    ''' <summary>
    '''  Delete comment from trakt.tv
    ''' </summary>
    ''' <param name="sender">"Delete comment from trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' 2015/06/01 Cocotus - First implementation
    ''' This sub  handles deleting a comment from trakt.tv
    ''' </remarks>
    Private Sub btntraktCommentsDetailsDelete_Click(sender As Object, e As EventArgs) Handles btntraktCommentsDetailsDelete.Click
        Dim response As String = Master.eLang.GetString(1410, "Delete comment from trakt.tv") & "? " & Master.eLang.GetString(36, "Movies") & ":" & Environment.NewLine
        response = response & dgvtraktComments.CurrentRow.Cells(0).Value.ToString & Environment.NewLine

        Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If result = Windows.Forms.DialogResult.Yes Then
            Dim traktResponse = _TraktAPI.Comment_Remove(CInt(dgvtraktComments.CurrentRow.Cells(4).Value))
            If traktResponse Then
                logger.Info("Deleted comment from trakt.tv!")
                response = Master.eLang.GetString(1407, "Deleted") & "!"
                btntraktCommentsGet_Click(Nothing, Nothing)
            Else
                response = Master.eLang.GetString(1134, "Error!")
            End If
            MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    ''' <summary>
    '''  Delete comment from trakt.tv
    ''' </summary>
    ''' <param name="sender">"Delete comment from trakt.tv"-Button in Form</param>
    ''' <remarks>
    ''' 2015/06/01 Cocotus - First implementation
    ''' This sub  handles deleting a comment from trakt.tv
    ''' </remarks>
    Private Sub btntraktCommentsDetailsUpdate_Click(sender As Object, e As EventArgs) Handles btntraktCommentsDetailsUpdate.Click
        Dim response As String = Master.eLang.GetString(1409, "Update comment on trakt.tv") & "? " & Master.eLang.GetString(36, "Movies") & ":" & Environment.NewLine
        Dim tmpTraktSynchronize As New TraktAPI.Model.TraktCommentBase
        tmpTraktSynchronize.Text = txttraktCommentsDetailsComment.Text
        tmpTraktSynchronize.IsSpoiler = chktraktCommentsDetailsSpoiler.Checked
        response = response & dgvtraktComments.CurrentRow.Cells(0).Value.ToString & Environment.NewLine

        Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If result = Windows.Forms.DialogResult.Yes Then
            Dim traktResponse = _TraktAPI.Comment_Update(CStr(dgvtraktComments.CurrentRow.Cells(4).Value), tmpTraktSynchronize)
            If traktResponse IsNot Nothing Then
                If traktResponse.Id > 0 Then
                    logger.Info("Updated comment on trakt.tv!")
                    response = Master.eLang.GetString(1408, "Updated") & "!"
                    btntraktCommentsGet_Click(Nothing, Nothing)
                Else
                    logger.Info("No comment submitted!")
                    response = Master.eLang.GetString(1134, "Error!")
                End If
            Else
                response = Master.eLang.GetString(1134, "Error!")
            End If
            MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    ''' <summary>
    ''' Open link in datagrid using default browser
    ''' </summary>
    ''' <param name="sender">Cell click event</param>
    ''' <remarks>
    ''' 2015/06/01 Cocotus - First implementation
    Private Sub dgvtraktComments_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvtraktComments.CellContentClick
        If e.RowIndex > -1 AndAlso e.ColumnIndex = 4 AndAlso dgvtraktComments.CurrentCell.RowIndex > -1 Then
            'URL to comment on trakt.tv
            System.Diagnostics.Process.Start("http://trakt.tv/comments/" & dgvtraktComments.CurrentCell.Value.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Handles SelectedRow Changed-Event
    ''' </summary>
    ''' <param name="sender">Selection Changed event of Datagrid</param>
    ''' <remarks>
    ''' 2015/05/30 Cocotus - First implementation
    ''' enable/disable Send/Update/Delete Comment-Button 
    Private Sub dgvtraktComments_SelectionChanged(sender As Object, e As EventArgs) Handles dgvtraktComments.SelectionChanged
        If dgvtraktComments.CurrentRow.Index > -1 Then
            lbltraktCommentsDetailsDate2.Text = String.Empty
            lbltraktCommentsDetailsLikes2.Text = String.Empty
            lbltraktCommentsDetailsRating2.Text = String.Empty
            lbltraktCommentsDetailsReplies2.Text = String.Empty
            lbltraktCommentsDetailsType2.Text = String.Empty
            txttraktCommentsDetailsComment.Text = String.Empty

            For Each movie As DataRow In dtMovies.Rows
                If movie.Item("Imdb").ToString = dgvtraktComments.CurrentRow.Cells(5).Value.ToString AndAlso movie.Table.Columns.Contains("Comment") Then
                    txttraktCommentsDetailsComment.Text = movie.Item("Comment").ToString
                    Exit For
                End If
            Next
            If String.IsNullOrEmpty(txttraktCommentsDetailsComment.Text) Then
                For Each comment In myCommentsMovies
                    'Find current selected row 
                    If comment.Comment.Id.ToString = dgvtraktComments.CurrentRow.Cells(4).Value.ToString Then
                        lbltraktCommentsDetailsDate2.Text = dgvtraktComments.CurrentRow.Cells(1).Value.ToString
                        lbltraktCommentsDetailsLikes2.Text = comment.Comment.Likes.ToString
                        lbltraktCommentsDetailsRating2.Text = comment.Movie.Rating.ToString
                        lbltraktCommentsDetailsReplies2.Text = comment.Comment.Replies.ToString
                        If comment.Comment.IsReview Then
                            lbltraktCommentsDetailsType2.Text = "Review"
                        Else
                            lbltraktCommentsDetailsType2.Text = "Comment"
                        End If

                        txttraktCommentsDetailsComment.Text = comment.Comment.Text
                        Exit For
                    End If
                Next
            End If

            'If column CommentID is filled in datarow, then its a trakt.tv comment, otherwise there's no comment for this entry on trakt.tv
            If Not String.IsNullOrEmpty(dgvtraktComments.CurrentRow.Cells(4).Value.ToString) Then
                ' Dim myStartTime As DateTime = DateTime.ParseExact(dgvtraktComments.CurrentRow.Cells(1).Value.ToString, "yyyy-MM-dd hh:mm", Globalization.CultureInfo.InvariantCulture)
                Dim myStartTime As Date = If(Date.TryParse(dgvtraktComments.CurrentRow.Cells(1).Value.ToString, myStartTime), myStartTime, Nothing)
                Dim myEndTime As Date = If(Date.TryParse(Date.Now.ToString(), myEndTime), myEndTime, Nothing)
                Dim elapsedTime As TimeSpan = myEndTime.Subtract(myStartTime)
                'right now you can't edit a comment older than 60minutes!
                If elapsedTime.TotalMinutes < 60 Then
                    btntraktCommentsDetailsDelete.Enabled = True
                    btntraktCommentsDetailsUpdate.Enabled = True
                    btntraktCommentsDetailsSend.Enabled = False
                End If
                'no comment on trakt.tv yet
            Else
                btntraktCommentsDetailsDelete.Enabled = False
                btntraktCommentsDetailsUpdate.Enabled = False
                btntraktCommentsDetailsSend.Enabled = True
            End If
            'no row selected
        Else
            btntraktCommentsDetailsDelete.Enabled = False
            btntraktCommentsDetailsUpdate.Enabled = False
            btntraktCommentsDetailsSend.Enabled = False
        End If
    End Sub

#End Region

#Region "Trakt.tv Cleaning"

    ''' <summary>
    ''' Remove plays on trakt.tv movie history for a specific timespan
    ''' </summary>
    ''' <param name="sender">"Start cleaning movie history"-Button in Form</param>
    ''' <remarks>
    ''' 2016/03/06 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktCleaningHistoryTimespan_Click(sender As Object, e As EventArgs) Handles btntraktCleaningHistoryTimespan.Click
        Dim timespan As Integer
        If Int32.TryParse(cbotraktCleaningHistoryTimespan.Text, timespan) Then
            RemoveInvalidPlaysFromHistory(timespan, String.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Remove plays on trakt.tv movie history for a specific timestamp
    ''' </summary>
    ''' <param name="sender">"Start cleaning movie history"-Button in Form</param>
    ''' <remarks>
    ''' 2016/03/06 Cocotus - First implementation
    ''' </remarks>
    Private Sub btntraktCleaningHistoryTimestamp_Click(sender As Object, e As EventArgs) Handles btntraktCleaningHistoryTimestamp.Click
        RemoveInvalidPlaysFromHistory(Nothing, txttraktCleaningHistoryTimestamp.Text)
    End Sub


    ''' <summary>
    ''' Remove plays on trakt.tv movie history based on given timespan or timestamp
    ''' </summary>
    ''' <param name="playsTimeRange">Timeintervall in minute, i.e. "5" -> all plays from a movie within 5minute intervall will be deleted except first play from that intervall</param>
    ''' <param name="playsTimeStamp">String in format "hh:mm:ss", i.e. "00:00:00" -> all plays at midnight will be removed if the movie has more than one play</param>
    ''' <remarks>
    ''' 2016/03/06 Cocotus - First implementation
    ''' Idea: http://support.trakt.tv/forums/188762-general/suggestions/7134014-delete-history-from-a-specific-date
    ''' This is used to clean user watched movie history from duplicate plays (i.e. sent because of a buggy trakt app)
    ''' </remarks>
    Private Sub RemoveInvalidPlaysFromHistory(ByVal playsTimeRange As Integer, ByVal playsTimeStamp As String)
        Dim response As String = String.Empty
        Dim IDOfDuplicate As New Dictionary(Of String, String)
        Dim IDNameOfDuplicate As New Dictionary(Of String, String)

        Dim traktTraktMovieHistory As List(Of TraktAPI.Model.TraktMovieHistory) = _TraktAPI.GetWatchedHistory_Movies()
        If traktTraktMovieHistory IsNot Nothing Then
            Dim duplicates = traktTraktMovieHistory.GroupBy(Function(i) i.Movie.Ids.Trakt).Where(Function(x) x.Count() > 1).[Select](Function(x) x).ToList()
            Dim tmpdate As Date = Nothing
            For Each group In duplicates
                If group.Count >= 2 Then
                    'sort by oldest watchdate on top
                    Dim sortedWatchedTimes = group.OrderBy(Function(z) z.WatchedAt_DateTime).ToList()
                    logger.Info("Movie: {0} has been watched {1} times. First watched: {2}", CStr(sortedWatchedTimes(0).Movie.Title), group.Count, sortedWatchedTimes(0).WatchedAt_DateTime)
                    'compare timespan between each watched instance of movie and if timespan to short mark entry as duplicate
                    For i = 1 To sortedWatchedTimes.Count - 1
                        logger.Info("Movie: {0} , calculated timespan: {1}", CStr(sortedWatchedTimes(i).Movie.Title), (sortedWatchedTimes(i).WatchedAt_DateTime - sortedWatchedTimes(i - 1).WatchedAt_DateTime).TotalMinutes)
                        If Not String.IsNullOrEmpty(playsTimeStamp) Then
                            'Remove all plays on a specific timestamp
                            If Not IDOfDuplicate.ContainsKey(CStr(sortedWatchedTimes(i).Movie.Ids.Trakt)) AndAlso (sortedWatchedTimes(i).WatchedAt_DateTime.ToString.EndsWith(playsTimeStamp)) Then
                                IDOfDuplicate.Add(CStr(sortedWatchedTimes(i).Movie.Ids.Trakt), sortedWatchedTimes(i).HistoryID)
                                IDNameOfDuplicate.Add(CStr(sortedWatchedTimes(i).Movie.Title), sortedWatchedTimes(i).WatchedAt_DateTime.ToString)
                            End If
                        Else
                            'Timespan mode, i.e. remove all plays within 5minute range
                            If Not IDOfDuplicate.ContainsKey(CStr(sortedWatchedTimes(i).Movie.Ids.Trakt)) AndAlso ((sortedWatchedTimes(i).WatchedAt_DateTime - sortedWatchedTimes(i - 1).WatchedAt_DateTime).TotalMinutes < playsTimeRange) Then
                                IDOfDuplicate.Add(CStr(sortedWatchedTimes(i).Movie.Ids.Trakt), sortedWatchedTimes(i).HistoryID)
                                IDNameOfDuplicate.Add(CStr(sortedWatchedTimes(i).Movie.Title), sortedWatchedTimes(i).WatchedAt_DateTime.ToString)
                            End If
                        End If
                    Next
                End If
            Next
        Else
            logger.Info("Movie history could not be scraped from trakt.tv!")
            Exit Sub
        End If

        If IDOfDuplicate.Count > 0 Then
            response = String.Format("{0}{1}", Master.eLang.GetString(1484, "Remove following plays from watched history?"), Environment.NewLine)
            For Each entry In IDNameOfDuplicate
                response = String.Format("{0}{1}: {2}{3}{4}: {5}{6}", response, Master.eLang.GetString(1379, "Movie"), entry.Key, Environment.NewLine, Master.eLang.GetString(981, "Watched"), entry.Value, Environment.NewLine)
            Next
            Dim result As DialogResult = MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result = Windows.Forms.DialogResult.Yes Then
                Dim tmpresponse As String = String.Empty
                Dim index As Integer = -1
                For Each itemtodelete In IDOfDuplicate
                    index = index + 1
                    Dim tmpTraktItemToDELETE As New TraktAPI.Model.TraktSyncHistoryID
                    tmpTraktItemToDELETE.Ids = CType((itemtodelete.Value), Integer?)
                    Dim traktResponse = _TraktAPI.RemoveFromWatchedHistory_ByHistoryID(tmpTraktItemToDELETE)
                    logger.Info("Deleted Item: " & IDNameOfDuplicate.ElementAt(index).Key)
                    tmpresponse = String.Format("{0}{1}: {2}{3}{4}: {5}{6}", tmpresponse, Master.eLang.GetString(1379, "Movie"), IDNameOfDuplicate.ElementAt(index).Key, Environment.NewLine, Master.eLang.GetString(981, "Watched"), IDNameOfDuplicate.ElementAt(index).Value, Environment.NewLine)
                Next
                response = String.Format("{0}:{1}{2}", Master.eLang.GetString(1407, "Deleted"), Environment.NewLine, tmpresponse)
            Else
                Exit Sub
            End If
        Else
            response = Master.eLang.GetString(833, "No Matches Found")
        End If
        MessageBox.Show(response, Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
    End Sub
#End Region

#End Region 'Methods

End Class