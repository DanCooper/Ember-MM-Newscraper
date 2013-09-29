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

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Some Sample VB CODE for creating new personal list on trakttv - calling from Ember Module!
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim list As New TraktList
        list.UserName = TraktSettings.Username
        list.Password = TraktSettings.Password
        list.Name = ""
        list.Privacy = "public"
        list.Description = "Cool movies!"
        Dim response As TraktAddListResponse = TrakttvAPI.SENDListAdd(list)
        If response.Status = "success" Then
            'everything fine!
        End If

		'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Some Sample VB CODE for sending data to personal list on trakttv - calling from Ember Module!
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Upload to User list different for videotype, Type can be = movie, season, episode, show

        'Movie to user list
        Dim movieitem As New TraktListItem() With { _
        .Type = "movie", _
        .ImdbId = "ImdbId", _
        .Title = "Title", _
        .Year = 2013 _
         }
        TrakttvAPI.AddRemoveItemInList("listname", movieitem, False)


        'Show to user list
        Dim showitem As New TraktListItem() With { _
        .Type = "show", _
            .TvdbId = "Tvdb", _
            .Title = "Title", _
             .Year = 2000 _
        }
        TrakttvAPI.AddRemoveItemInList("listname", showitem, False)


        'Season to user list
        Dim seasonitem As New TraktListItem() With { _
             .Type = "season", _
             .TvdbId = "Tvdb", _
        .Title = "Title", _
            .Year = 2011, _
             .Season = 2 _
        }
        TrakttvAPI.AddRemoveItemInList("listname", seasonitem, False)


        'Episode to user list
        Dim episodeitem As New TraktListItem() With { _
             .Type = "episode", _
             .TvdbId = "tvdbid", _
         .Title = "title", _
             .Year = 2004, _
         .Season = 1, _
            .Episode = 9 _
        }
        TrakttvAPI.AddRemoveItemInList("listname", episodeitem, False)


	   '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Some Sample VB CODE for geting content of user list on trakttv - calling from Ember Module!
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim traktUserListMovies As TraktUserList = TrakttvAPI.GETUserList(TraktSettings.Username, "traktUserList")
        For Each item In traktUserListMovies.Items
            Select Case item.Type
                Case "movie"
                    Exit Select
                Case "show"
                    Exit Select
                Case "season"
                    Exit Select
                Case "episode"
                    Exit Select
            End Select
        Next
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Some Sample VB CODE for sending rate movie data on trakttv - calling from Ember Module!
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim rateObject As New TraktSENDRatingMovie With { _
        .IMDBID = "IMDBID", _
        .TMDBID = "TMDBID", _
        .Title = "Title", _
        .Year = "2009", _
        .Rating = "10", _
        .UserName = TraktSettings.Username, _
        .Password = TraktSettings.Password _
        }
        Dim response As TraktAddListResponse = TrakttvAPI.SENDRatingMovie(rateObject)

        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Some Sample VB CODE for geting rate movie data on trakttv - calling from Ember Module!
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim traktRatedMovies = TrakttvAPI.GetUserRatedMovies(TraktSettings.Username)
        If traktRatedMovies Is Nothing Then
            'bad
        Else
            'good
            ' get the movies that we have rated/unrated
            'MovieList is collection of movies
            '    Dim RatedList = MovieList.Where(Function(m) m.RatingUser > 0.0).ToList()
            '   Dim UnRatedList = MovieList.Except(RatedList).ToList()
        End If