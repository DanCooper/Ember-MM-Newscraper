Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports System.IO
Imports EmberAPI
Imports Trakttv


Public Class frmTrakt
    Public Event ModuleSettingsChanged()
    Dim myWatchedMovies As New Dictionary(Of String, KeyValuePair(Of String, Integer))
    Dim myWatchedEpisodes As New Dictionary(Of String, KeyValuePair(Of String, List(Of TraktAPI.Model.TraktWatchedEpisode.SeasonsWatched)))
    Dim bkWrk As New System.ComponentModel.BackgroundWorker()

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        SetUp()
    End Sub

    Sub SetUp()
        lblstate.Visible = False
        prgtrakt.Value = 0
        prgtrakt.Maximum = myWatchedMovies.Count
        prgtrakt.Minimum = 0
        prgtrakt.Step = 1
        chkUseTrakt.Text = Master.eLang.GetString(778, "Use trakt.tv as source for ""Playcount""")
        btGetMoviesTrakt.Text = Master.eLang.GetString(779, "Get watched movies")
        btSaveMoviesTrakt.Text = Master.eLang.GetString(780, "Save playcount to database/Nfo")
        txtTraktUser.Text = Master.eSettings.TraktUser
        lblTraktUser.Text = Master.eLang.GetString(425, "Username")
        txtTraktPassword.Text = Master.eSettings.TraktPassword
        lblTraktPassword.Text = Master.eLang.GetString(426, "Password")
        btGetSeriesTrakt.Text = Master.eLang.GetString(781, "Get watched episodes")
        btnSavetraktsettings.Text = Master.eLang.GetString(273, "Save")
        txtTraktPassword.PasswordChar = "*"c

        If Not String.IsNullOrEmpty(Master.eSettings.UseTrakt.ToString) Then
            chkUseTrakt.Checked = Master.eSettings.UseTrakt
        End If
        dgvTraktWatched.DataSource = Nothing
        dgvTraktWatched.Rows.Clear()

        If Master.eSettings.UseTrakt = True Then
            txtTraktUser.Enabled = True
            txtTraktPassword.Enabled = True
            If Not String.IsNullOrEmpty(Master.eSettings.TraktUser) AndAlso Not String.IsNullOrEmpty(Master.eSettings.TraktPassword) Then
                btGetMoviesTrakt.Enabled = True
                btGetSeriesTrakt.Enabled = True
            Else
                btGetMoviesTrakt.Enabled = False
                btGetSeriesTrakt.Enabled = False
            End If
        Else
            btGetMoviesTrakt.Enabled = False
            btGetSeriesTrakt.Enabled = False
            txtTraktUser.Enabled = False
        End If
    End Sub
    'Little Control over Form-Controls
    Private Sub chkUseTrakt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseTrakt.CheckedChanged
        If chkUseTrakt.Checked = True Then
            If Not String.IsNullOrEmpty(txtTraktUser.Text) AndAlso Not String.IsNullOrEmpty(txtTraktPassword.Text) Then
                btGetMoviesTrakt.Enabled = True
                btGetSeriesTrakt.Enabled = True
            End If
            txtTraktUser.Enabled = True
            txtTraktPassword.Enabled = True
        Else
            btGetMoviesTrakt.Enabled = False
            btGetSeriesTrakt.Enabled = False
            txtTraktUser.Enabled = False
            txtTraktPassword.Enabled = False
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTraktUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTraktUser.TextChanged
        RaiseEvent ModuleSettingsChanged()
        If Not String.IsNullOrEmpty(txtTraktUser.Text) AndAlso txtTraktUser.Enabled = True Then
            btGetMoviesTrakt.Enabled = True
            btGetSeriesTrakt.Enabled = True
        Else
            btGetMoviesTrakt.Enabled = False
            btGetSeriesTrakt.Enabled = False
        End If
    End Sub

    Private Sub txtTraktPassword_TextChanged(sender As Object, e As EventArgs) Handles txtTraktPassword.TextChanged
        RaiseEvent ModuleSettingsChanged()
        If Not String.IsNullOrEmpty(txtTraktPassword.Text) AndAlso txtTraktPassword.Enabled = True Then
            btGetMoviesTrakt.Enabled = True
            btGetSeriesTrakt.Enabled = True
        Else
            btGetMoviesTrakt.Enabled = False
            btGetSeriesTrakt.Enabled = False
        End If
    End Sub

    Private Sub btGetMoviesTrakt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetMoviesTrakt.Click



        myWatchedEpisodes = Nothing
        dgvTraktWatched.DataSource = Nothing
        dgvTraktWatched.Rows.Clear()
        ' myWatchedMovies.Clear()

        If Not String.IsNullOrEmpty(txtTraktUser.Text) AndAlso chkUseTrakt.Checked = True AndAlso Not String.IsNullOrEmpty(txtTraktPassword.Text) Then
            'Old method - not using Trakt.tv API wrapper
            ' myWatchedMovies = GetWatchedMoviesFromTrakt(txtTraktUser.Text, txtTraktPassword.Text)

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Some Sample VB CODE for getting watched movies from Trakt - calling from Ember Module!
            'Get watched movies of user
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 3 values in Dictionary style
            Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, Integer))

            ' Use new Trakttv wrapper class to get watched data!
            Trakttv.TraktSettings.Username = txtTraktUser.Text
            Trakttv.TraktSettings.Password = txtTraktPassword.Text
            Dim traktWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktWatchedMovie) = TraktAPI.TrakttvAPI.GetUserWatchedMovies(txtTraktUser.Text)

            ' Go through each item in collection	 
            For Each Item In traktWatchedMovies
                'Check if information is stored...
                If Not Item.Title Is Nothing AndAlso Item.Title <> "" AndAlso Not Item.IMDBID Is Nothing AndAlso Item.IMDBID <> "" Then
                    If Not dictMovieWatched.ContainsKey(Item.Title) Then
                        'Now store imdbid, title and playcount information into dictionary (for now no other info needed...)
                        If Item.IMDBID.Length > 2 AndAlso Item.IMDBID.Substring(0, 2) = "tt" Then
                            'IMDBID beginning with tt -> strip tt first and save only number!
                            dictMovieWatched.Add(Item.Title, New KeyValuePair(Of String, Integer)(Item.IMDBID.Substring(2), CInt(Item.Plays)))
                        Else
                            'IMDBID is alright
                            dictMovieWatched.Add(Item.Title, New KeyValuePair(Of String, Integer)(Item.IMDBID, CInt(Item.Plays)))
                        End If
                    End If
                End If
            Next
            myWatchedMovies = dictMovieWatched
        End If
        dgvTraktWatched.AutoGenerateColumns = True
        If Not myWatchedMovies Is Nothing Then
            btSaveMoviesTrakt.Enabled = True
            'we map to dgv manually
            dgvTraktWatched.AutoGenerateColumns = False
            'fill rows
            For Each Item In myWatchedMovies
                dgvTraktWatched.Rows.Add(New Object() {Item.Key, Item.Value.Value})
            Next
        Else
            btSaveMoviesTrakt.Enabled = False
        End If
    End Sub

    Private Sub btGetSeriesTrakt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetSeriesTrakt.Click
        myWatchedMovies = Nothing
        dgvTraktWatched.DataSource = Nothing
        dgvTraktWatched.Rows.Clear()
        '  myWatchedMovies.Clear()

        If Not String.IsNullOrEmpty(txtTraktUser.Text) AndAlso chkUseTrakt.Checked = True AndAlso Not String.IsNullOrEmpty(txtTraktPassword.Text) Then
            'Old method - not using Trakt.tv API wrapper
            '  myWatchedEpisodes = GetWatchedEpisodesFromTrakt(txtTraktUser.Text, txtTraktPassword.Text)

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Some Sample VB CODE for getting watched episode from Trakt - calling from Ember Module!
            ' Get all episodes on Trakt.tv that are marked as 'seen' or 'watched'
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            'Helper: Saving 3 values in Dictionary style
            Dim dictEpisodesWatched As New Dictionary(Of String, KeyValuePair(Of String, List(Of TraktAPI.Model.TraktWatchedEpisode.SeasonsWatched)))

            ' Use new Trakttv wrapper class to get watched data!
            Trakttv.TraktSettings.Username = txtTraktUser.Text
            Trakttv.TraktSettings.Password = txtTraktPassword.Text
            Dim traktWatchedEpisodes As IEnumerable(Of TraktAPI.Model.TraktWatchedEpisode) = TraktAPI.TrakttvAPI.GetUserWatchedEpisodes(txtTraktUser.Text)
            ' Go through each item in collection	
            If traktWatchedEpisodes Is Nothing = False Then
                For Each episode In traktWatchedEpisodes
                    If Not episode.Title Is Nothing AndAlso episode.Title <> "" AndAlso Not episode.SeriesId Is Nothing AndAlso episode.SeriesId <> "" Then
                        If Not dictEpisodesWatched.ContainsKey(episode.Title) Then
                            'Now store tvdbID, title and the season-episode-list in dictionary...
                            dictEpisodesWatched.Add(episode.Title, New KeyValuePair(Of String, List(Of TraktAPI.Model.TraktWatchedEpisode.SeasonsWatched))(episode.SeriesId, episode.Seasons))
                        End If
                    End If
                Next
            End If
            myWatchedEpisodes = dictEpisodesWatched
        End If
        dgvTraktWatched.AutoGenerateColumns = True
        If Not myWatchedEpisodes Is Nothing Then
            btSaveMoviesTrakt.Enabled = True
            'we map to dgv manually
            dgvTraktWatched.AutoGenerateColumns = False
            'fill rows
            For Each Item In myWatchedEpisodes
                'x = Episodes watched für specific tv show, use a loop to sum up the episodes of tvshow
                Dim x As Integer = 0
                For i = 0 To Item.Value.Value.Count - 1
                    x = x + Item.Value.Value.Item(i).episodes.Count
                Next

                dgvTraktWatched.Rows.Add(New Object() {Item.Key, x})
            Next
        Else
            btSaveMoviesTrakt.Enabled = False
        End If
    End Sub

    Private Sub btnSavetraktsettings_Click(sender As Object, e As EventArgs) Handles btnSavetraktsettings.Click
        SaveChanges()
    End Sub

    Public Sub SaveChanges()
        Master.eSettings.TraktUser = txtTraktUser.Text
        Master.eSettings.TraktPassword = txtTraktPassword.Text
        Master.eSettings.UseTrakt = chkUseTrakt.Checked
        If Not String.IsNullOrEmpty(Master.eSettings.TraktUser) AndAlso Master.eSettings.UseTrakt = True AndAlso Not String.IsNullOrEmpty(Master.eSettings.TraktPassword) Then
            btGetMoviesTrakt.Enabled = True
            btGetSeriesTrakt.Enabled = True
        Else
            btGetMoviesTrakt.Enabled = False
            btGetSeriesTrakt.Enabled = False
        End If
    End Sub

    Private Sub btSaveMoviesTrakt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveMoviesTrakt.Click
        If Not String.IsNullOrEmpty(txtTraktUser.Text) AndAlso chkUseTrakt.Checked = True AndAlso Not String.IsNullOrEmpty(txtTraktPassword.Text) Then
            'Movies
            If Not myWatchedMovies Is Nothing Then
                prgtrakt.Value = 0
                prgtrakt.Maximum = myWatchedMovies.Count
                prgtrakt.Minimum = 0
                prgtrakt.Step = 1
                btSaveMoviesTrakt.Enabled = False
                Dim traktthread As Threading.Thread
                traktthread = New Threading.Thread(AddressOf SaveMoviePlaycount)
                traktthread.IsBackground = True
                traktthread.Start()
                'Tv-Show
            ElseIf Not myWatchedEpisodes Is Nothing Then
                prgtrakt.Value = 0
                prgtrakt.Maximum = myWatchedEpisodes.Count
                'start not with empty progressbar(no problem for movies) because it takes long to update for first tv show and user might think it hangs -> set value 1 to show something is going on
                If myWatchedEpisodes.Count > 1 Then
                    prgtrakt.Value = 1
                End If
                prgtrakt.Minimum = 0
                prgtrakt.Step = 1
                btSaveMoviesTrakt.Enabled = False
                Dim traktthread As Threading.Thread
                traktthread = New Threading.Thread(AddressOf SaveEpisodePlaycount)
                traktthread.IsBackground = True
                traktthread.Start()
            End If

        End If
    End Sub


    'Save plays-information from trakt.tv to database/nfo - Movie Thread
    Private Sub SaveMoviePlaycount()
        Try

            Dim i As Integer = 0
            For Each watchedMovieData In myWatchedMovies
                i = i + 1
                Master.DB.SaveMoviePlayCountInDatabase(watchedMovieData)
                ' Invoke to update UI from thread...
                prgtrakt.Invoke(New UpdateProgressBarDelegate(AddressOf UpdateProgressBar), i)
                Threading.Thread.Sleep(10)
            Next

        Catch ex As Exception

        End Try

    End Sub

    'Save plays-information from trakt.tv to database/nfo - Tv Show Thread
    Private Sub SaveEpisodePlaycount()
        Try

            Dim i As Integer = 0
            For Each watchedEpisodeData In myWatchedEpisodes
                i = i + 1

                'loop through every season of certain tvshow
                For z = 0 To watchedEpisodeData.Value.Value.Count - 1
                    'now go to every episode of current season
                    For Each episode In watchedEpisodeData.Value.Value.Item(z).episodes
                        '..and save playcount of every episode to database
                        Master.DB.SaveEpisodePlayCountInDatabase(watchedEpisodeData.Value.Key, watchedEpisodeData.Value.Value.Item(z).season.ToString, episode.ToString)
                    Next
                Next
                ' Invoke to update UI from thread...
                prgtrakt.Invoke(New UpdateProgressBarDelegate(AddressOf UpdateProgressBar), i)
                Threading.Thread.Sleep(10)
            Next

        Catch ex As Exception

        End Try

    End Sub


    Private Delegate Sub UpdateProgressBarDelegate(ByVal i As Integer)
    ' Do all the ui thread updates here
    'Use progressbar to show user progress of saving, since it can take some time
    Private Sub UpdateProgressBar(ByVal i As Integer)
        If i = 1 Then
            lblstate.Visible = False
        End If

        prgtrakt.Value = i
        If Not myWatchedMovies Is Nothing Then
            If i = myWatchedMovies.Count - 1 Then
                lblstate.Text = "Done!"
                lblstate.Visible = True
            End If
        ElseIf Not myWatchedEpisodes Is Nothing Then
            If i = myWatchedEpisodes.Count - 1 Then
                lblstate.Text = "Done!"
                lblstate.Visible = True
            End If
        End If


    End Sub

#Region "Obsolete Methods - replaced by TVTrakt-Wrapper Calls!"
    ''' <summary>
    ''' cocotus 2013/02 Trakt.tv syncing: Movies
    ''' Connects with trakt.tv Website and gets Watched Movies from specific User and returns them in special Dictionary
    ''' More Info here: http://trakt.tv/api-docs/user-library-movies-watched
    ''' </summary>
    ''' <param name="traktID">Username</param>
    ''' <param name="traktPW">password</param>
    ''' <returns>3 values in dictionary: IMDBID (ex: tt0114746), Title, Playcount/Plays</returns>
    Public Shared Function GetWatchedMoviesFromTrakt(ByVal traktID As String, ByVal traktPW As String) As Dictionary(Of String, KeyValuePair(Of String, Integer))

        Dim wc As New Net.WebClient
        Try
            'Saving 3 values in Dictionary style
            Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, Integer))

            'The REQUEST String (includes API-ID + UserID)
            Dim URL As String = "http://api.trakt.tv/user/library/movies/watched.json/b59a24b6a3fb93fc2fb565a681bb8a1d/" & traktID

            Dim json As String = wc.DownloadString(URL)
            If Not String.IsNullOrEmpty(json) Then

                'Now we are using free  3rd party class/dll to make an easy parse of the json String
                Dim client = New RestSharp.RestClient(URL)
                'added basic authentification, to get even protected user information
                client.Authenticator = New RestSharp.HttpBasicAuthenticator(traktID, traktPW)
                Dim request = New RestSharp.RestRequest(RestSharp.Method.[GET])
                Dim response = client.Execute(Of List(Of TraktWatchedMovieData))(request)

                If Not response Is Nothing Then
                    'Now loop through to every entry
                    For Each Item As TraktWatchedMovieData In response.Data
                        'Check if information is stored...
                        If Not Item.title Is Nothing AndAlso Item.title <> "" AndAlso Not Item.imdb_id Is Nothing AndAlso Item.imdb_id <> "" Then
                            If Not dictMovieWatched.ContainsKey(Item.title) Then
                                'Now store imdbid, title and playcount information into dictionary (for now no other info needed...)
                                If Item.imdb_id.Length > 2 AndAlso Item.imdb_id.Substring(0, 2) = "tt" Then
                                    'IMDBID beginning with tt -> strip tt first and save only number!
                                    dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, Integer)(Item.imdb_id.Substring(2), CInt(Item.plays)))
                                Else
                                    'IMDBID is alright
                                    dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, Integer)(Item.imdb_id, CInt(Item.plays)))
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            wc.Dispose()
            Return dictMovieWatched

        Catch ex As Exception
            wc.Dispose()
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' cocotus 2013/03 Trakt.tv syncing: TV Shows
    ''' Connects with trakt.tv Website and gets Watched Episodes from specific User and returns them in a special Dictionary
    ''' More Info here: http://trakt.tv/api-docs/user-library-shows-watched
    ''' </summary>
    ''' <param name="traktID">Username</param>
    ''' <param name="traktPW">password</param>
    ''' <returns>3 values in dictionary: TvShowTitle, TVDBID, Special Season Class (Season + Watched Episodes in list)</returns>
    Public Shared Function GetWatchedEpisodesFromTrakt(ByVal traktID As String, ByVal traktPW As String) As Dictionary(Of String, KeyValuePair(Of String, List(Of SeasonClass)))

        Dim wc As New Net.WebClient
        Try
            'Saving 3 values in Dictionary style
            Dim dictMovieWatched As New Dictionary(Of String, KeyValuePair(Of String, List(Of SeasonClass)))

            'The REQUEST String (includes API-ID + UserID)
            Dim URL As String = "http://api.trakt.tv/user/library/shows/watched.json/b59a24b6a3fb93fc2fb565a681bb8a1d/" & traktID

            Dim json As String = wc.DownloadString(URL)
            If Not String.IsNullOrEmpty(json) Then

                'Now we are using free  3rd party class/dll to make an easy parse of the json String
                Dim client = New RestSharp.RestClient(URL)
                'added basic authentification, to get even protected user information
                client.Authenticator = New RestSharp.HttpBasicAuthenticator(traktID, traktPW)
                Dim request = New RestSharp.RestRequest(RestSharp.Method.[GET])
                Dim response = client.Execute(Of List(Of TraktWatchedEpisodeData))(request)

                If Not response Is Nothing Then
                    'Now loop through to every entry
                    For Each Item As TraktWatchedEpisodeData In response.Data
                        'Check if information is stored...
                        If Not Item.title Is Nothing AndAlso Item.title <> "" AndAlso Not Item.tvdb_id Is Nothing AndAlso Item.tvdb_id <> "" Then
                            If Not dictMovieWatched.ContainsKey(Item.title) Then
                                'Now store tvdbID, title and the season-episode-list in dictionary...
                                dictMovieWatched.Add(Item.title, New KeyValuePair(Of String, List(Of SeasonClass))(Item.tvdb_id, Item.seasons))
                            End If
                        End If
                    Next
                End If
            End If
            wc.Dispose()
            Return dictMovieWatched

        Catch ex As Exception
            wc.Dispose()
            Return Nothing
        End Try
    End Function

#End Region
End Class

'New Class which holds/described an item of WatcheMovie on trakt.tv
'Todo Expand class move to seperate project and build wrapper around
Public Class TraktWatchedMovieData
    Private m_title As String
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property

    Private m_url As String
    Public Property url() As String
        Get
            Return m_url
        End Get
        Set(value As String)
            m_url = value
        End Set
    End Property

    Private m_imdb_id As String
    Public Property imdb_id() As String
        Get
            Return m_imdb_id
        End Get
        Set(value As String)
            m_imdb_id = value
        End Set
    End Property

    Private m_plays As String
    Public Property plays() As String
        Get
            Return m_plays
        End Get
        Set(value As String)
            m_plays = value
        End Set
    End Property

End Class

'New Class which holds/described an item of WatchedEpsiode on trakt.tv
'Todo Expand class move to seperate project and build wrapper around
Public Class TraktWatchedEpisodeData
    Private m_title As String
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property

    Private m_tvdb_id As String
    Public Property tvdb_id() As String
        Get
            Return m_tvdb_id
        End Get
        Set(value As String)
            m_tvdb_id = value
        End Set
    End Property

    Private m_seasons As List(Of SeasonClass)
    Public Property seasons() As List(Of SeasonClass)
        Get
            Return m_seasons
        End Get
        Set(value As List(Of SeasonClass))
            m_seasons = value
        End Set
    End Property

End Class
'Child-Class of Episode Class, to store watched episodes
Public Class SeasonClass
    Private m_season As Integer
    Public Property season() As Integer
        Get
            Return m_season
        End Get
        Set(value As Integer)
            m_season = value
        End Set
    End Property

    Private m_episode As List(Of Integer)
    Public Property episodes() As List(Of Integer)
        Get
            Return m_episode
        End Get
        Set(value As List(Of Integer))
            m_episode = value
        End Set
    End Property

End Class


