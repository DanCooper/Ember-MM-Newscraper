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

Public Class SearchResults_Movie

#Region "Fields"

        Private _Matches As New List(Of MediaContainers.Movie)

#End Region 'Fields

#Region "Properties"

        Public Property Matches() As List(Of MediaContainers.Movie)
            Get
                Return _Matches
            End Get
            Set(ByVal value As List(Of MediaContainers.Movie))
                _Matches = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class SearchResults_MovieSet

#Region "Fields"

        Private _Matches As New List(Of MediaContainers.Movieset)

#End Region 'Fields

#Region "Properties"

        Public Property Matches() As List(Of MediaContainers.Movieset)
            Get
                Return _Matches
            End Get
            Set(ByVal value As List(Of MediaContainers.Movieset))
                _Matches = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class SearchResults_TVShow

#Region "Fields"

        Private _Matches As New List(Of MediaContainers.TVShow)

#End Region 'Fields

#Region "Properties"

        Public Property Matches() As List(Of MediaContainers.TVShow)
            Get
                Return _Matches
            End Get
            Set(ByVal value As List(Of MediaContainers.TVShow))
                _Matches = value
            End Set
        End Property

#End Region 'Properties

    End Class

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _client As TMDbLib.Client.TMDbClient  'preferred language
    Private _clientE As TMDbLib.Client.TMDbClient 'english language
    Private _addonSettings As TMDB_Data.SpecialSettings
    Private _sPoster As String = String.Empty

    Private _Fallback_Movie As TMDbLib.Objects.Movies.Movie = Nothing
    Private _Fallback_Movieset As TMDbLib.Objects.Collections.Collection = Nothing
    Private _Fallback_TVEpisode As TMDbLib.Objects.TvShows.TvEpisode = Nothing
    Private _Fallback_TVSeason As TMDbLib.Objects.TvShows.TvSeason = Nothing
    Private _Fallback_TVShow As TMDbLib.Objects.TvShows.TvShow = Nothing

    Friend WithEvents bwTMDB As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

    Public Property DefaultLanguage As String
        Get
            Return _client.DefaultLanguage
        End Get
        Set(value As String)
            _client.DefaultLanguage = value
        End Set
    End Property

    Public ReadOnly Property IsClientCreated As Boolean
        Get
            Return _client IsNot Nothing
        End Get
    End Property

#End Region 'Properties

#Region "Enumerations"

    Private Enum SearchType
        Movies = 0
        Details = 1
        SearchDetails_Movie = 2
        MovieSets = 3
        SearchDetails_MovieSet = 4
        TVShows = 5
        SearchDetails_TVShow = 6
    End Enum

#End Region 'Enumerations

#Region "Events"

    Public Event SearchInfoDownloaded_Movie(ByVal strPoster As String, ByVal sInfo As MediaContainers.Movie)
    Public Event SearchInfoDownloaded_MovieSet(ByVal strPoster As String, ByVal sInfo As MediaContainers.Movieset)
    Public Event SearchInfoDownloaded_TVShow(ByVal strPoster As String, ByVal sInfo As MediaContainers.TVShow)

    Public Event SearchResultsDownloaded_Movie(ByVal mResults As SearchResults_Movie)
    Public Event SearchResultsDownloaded_MovieSet(ByVal mResults As SearchResults_MovieSet)
    Public Event SearchResultsDownloaded_TVShow(ByVal mResults As SearchResults_TVShow)

#End Region 'Events

#Region "Methods"

    Public Async Function CreateAPI(ByVal addonSettings As TMDB_Data.SpecialSettings) As Task
        Try
            _addonSettings = addonSettings

            _client = New TMDbLib.Client.TMDbClient(_addonSettings.APIKey)
            Await _client.GetConfigAsync()
            _client.MaxRetryCount = 2
            _Logger.Trace("[TMDB_Data] [CreateAPI] Client created")

            If _addonSettings.FallBackEng Then
                _clientE = New TMDbLib.Client.TMDbClient(_addonSettings.APIKey)
                Await _clientE.GetConfigAsync()
                _clientE.DefaultLanguage = "en-US"
                _clientE.MaxRetryCount = 2
                _Logger.Trace("[TMDB_Data] [CreateAPI] Client-EN created")
            Else
                _clientE = _client
                _Logger.Trace("[TMDB_Data] [CreateAPI] Client-EN = Client")
            End If
        Catch ex As Exception
            _Logger.Error(String.Format("[TMDB_Data] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDB.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB

        Select Case Args.Search
            Case SearchType.Movies
                Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
                e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

            Case SearchType.MovieSets
                Dim r As SearchResults_MovieSet = SearchMovieSet(Args.Parameter)
                e.Result = New Results With {.ResultType = SearchType.MovieSets, .Result = r}

            Case SearchType.TVShows
                Dim r As SearchResults_TVShow = SearchTVShow(Args.Parameter)
                e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

            Case SearchType.SearchDetails_Movie
                Dim r As MediaContainers.Movie = GetInfo_Movie(Args.Parameter, Args.ScrapeOptions, True)
                e.Result = New Results With {.ResultType = SearchType.SearchDetails_Movie, .Result = r}

            Case SearchType.SearchDetails_MovieSet
                Dim intTmdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTmdbId) Then
                    Dim r As MediaContainers.Movieset = GetInfo_Movieset(intTmdbId, Args.ScrapeOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_MovieSet, .Result = r}
                Else
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_MovieSet, .Result = Nothing}
                End If

            Case SearchType.SearchDetails_TVShow
                Dim intTmdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTmdbId) Then
                    Dim r As MediaContainers.TVShow = GetInfo_TVShow(intTmdbId, Args.ScrapeModifiers, Args.ScrapeOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = r}
                Else
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = Nothing}
                End If
        End Select
    End Sub

    Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDB.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)

        Select Case Res.ResultType
            Case SearchType.Movies
                RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

            Case SearchType.MovieSets
                RaiseEvent SearchResultsDownloaded_MovieSet(DirectCast(Res.Result, SearchResults_MovieSet))

            Case SearchType.TVShows
                RaiseEvent SearchResultsDownloaded_TVShow(DirectCast(Res.Result, SearchResults_TVShow))

            Case SearchType.SearchDetails_Movie
                Dim movieInfo As MediaContainers.Movie = DirectCast(Res.Result, MediaContainers.Movie)
                RaiseEvent SearchInfoDownloaded_Movie(_sPoster, movieInfo)

            Case SearchType.SearchDetails_MovieSet
                Dim moviesetInfo As MediaContainers.Movieset = DirectCast(Res.Result, MediaContainers.Movieset)
                RaiseEvent SearchInfoDownloaded_MovieSet(_sPoster, moviesetInfo)

            Case SearchType.SearchDetails_TVShow
                Dim showInfo As MediaContainers.TVShow = DirectCast(Res.Result, MediaContainers.TVShow)
                RaiseEvent SearchInfoDownloaded_TVShow(_sPoster, showInfo)
        End Select
    End Sub

    Public Sub CancelAsync()
        If bwTMDB.IsBusy Then bwTMDB.CancelAsync()

        While bwTMDB.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Public Sub GetMovieID(ByVal DBMovie As Database.DBElement)
        Dim strUniqueID As String = String.Empty
        If DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            strUniqueID = DBMovie.Movie.UniqueIDs.TMDbId.ToString
        ElseIf DBMovie.Movie.UniqueIDs.IMDbIdSpecified Then
            strUniqueID = DBMovie.Movie.UniqueIDs.IMDbId
        End If

        If Not String.IsNullOrEmpty(strUniqueID) Then
            Dim Movie As TMDbLib.Objects.Movies.Movie
            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            APIResult = Task.Run(Function() _client.GetMovieAsync(strUniqueID))

            Movie = APIResult.Result
            If Movie Is Nothing OrElse Movie.Id = 0 Then Return

            DBMovie.Movie.UniqueIDs.TMDbId = Movie.Id
        End If
    End Sub

    Public Function GetMovieID(ByVal imdbId As String) As Integer
        Dim Movie As TMDbLib.Objects.Movies.Movie

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        APIResult = Task.Run(Function() _client.GetMovieAsync(imdbId))

        Movie = APIResult.Result
        If Movie Is Nothing OrElse Movie.Id = 0 Then Return -1

        Return Movie.Id
    End Function

    Public Function GetMovieCollectionID(ByVal imdbId As String) As Integer
        Dim Movie As TMDbLib.Objects.Movies.Movie

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        APIResult = Task.Run(Function() _client.GetMovieAsync(imdbId))

        Movie = APIResult.Result
        If Movie Is Nothing Then Return -1

        If Movie.BelongsToCollection IsNot Nothing AndAlso Movie.BelongsToCollection.Id > 0 Then
            Return Movie.BelongsToCollection.Id
        Else
            Return -1
        End If
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="tmdbIdOrImdbId">TMDBID or ID (IMDB ID starts with "tt") of movie to be scraped</param>
    ''' <param name="GetPoster">Scrape posters for the movie?</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_Movie(ByVal tmdbIdOrImdbId As String, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal GetPoster As Boolean) As MediaContainers.Movie
        _Fallback_Movie = Nothing
        If String.IsNullOrEmpty(tmdbIdOrImdbId) Then Return Nothing

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        Dim intTMDBID As Integer = -1

        If tmdbIdOrImdbId.ToLower.StartsWith("tt") Then
            'search movie by IMDB ID
            APIResult = Task.Run(Function() _client.GetMovieAsync(tmdbIdOrImdbId, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
        ElseIf Integer.TryParse(tmdbIdOrImdbId, intTMDBID) Then
            'search movie by TMDB ID
            APIResult = Task.Run(Function() _client.GetMovieAsync(intTMDBID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
        Else
            _Logger.Error(String.Format("Can't scrape or movie not found: [0]", tmdbIdOrImdbId))
            Return Nothing
        End If

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse Not APIResult.Result.Id > 0 OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or movie not found: [0]", tmdbIdOrImdbId))
            Return Nothing
        End If

        Dim Result As TMDbLib.Objects.Movies.Movie = APIResult.Result
        Dim nMovie As New MediaContainers.Movie With {.Scrapersource = "TMDB"}

        'IDs
        nMovie.UniqueIDs.TMDbId = Result.Id
        If Result.ImdbId IsNot Nothing Then nMovie.UniqueIDs.IMDbId = Result.ImdbId

        If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

        'Cast (Actors)
        If FilteredOptions.bMainActors Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.Movies.Cast In Result.Credits.Cast
                    nMovie.Actors.Add(New MediaContainers.Person With {
                                      .Name = aCast.Name,
                                      .Role = aCast.Character,
                                      .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                      .TMDB = CStr(aCast.Id)
                                      })
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Certifications
        If FilteredOptions.bMainCertifications Then
            If Result.Releases IsNot Nothing AndAlso Result.Releases.Countries IsNot Nothing AndAlso Result.Releases.Countries.Count > 0 Then
                For Each cCountry In Result.Releases.Countries
                    If Not String.IsNullOrEmpty(cCountry.Certification) Then
                        Dim CertificationLanguage = APIXML.CertificationLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = cCountry.Iso_3166_1.ToLower)
                        If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.Name) Then
                            nMovie.Certifications.Add(String.Concat(CertificationLanguage.Name, ":", cCountry.Certification))
                        Else
                            _Logger.Warn("Unhandled certification language encountered: {0}", cCountry.Iso_3166_1.ToLower)
                        End If
                    End If
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Collection ID
        If FilteredOptions.bMainCollectionID Then
            If Result.BelongsToCollection IsNot Nothing Then
                Dim nFullMovieSetInfo = GetInfo_Movieset(Result.BelongsToCollection.Id,
                                                         New Structures.ScrapeOptions With {.bMainPlot = True, .bMainTitle = True},
                                                         False)
                If nFullMovieSetInfo IsNot Nothing Then
                    nMovie.AddSet(New MediaContainers.SetDetails With {
                                  .ID = -1,
                                  .Order = -1,
                                  .Plot = nFullMovieSetInfo.Plot,
                                  .Title = nFullMovieSetInfo.Title,
                                  .UniqueIDs = nFullMovieSetInfo.UniqueIDs
                                  })
                    nMovie.UniqueIDs.TMDbCollectionId = nFullMovieSetInfo.UniqueIDs.TMDbId
                End If
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Countries
        If FilteredOptions.bMainCountries Then
            If Result.ProductionCountries IsNot Nothing AndAlso Result.ProductionCountries.Count > 0 Then
                For Each aContry As TMDbLib.Objects.General.ProductionCountry In Result.ProductionCountries
                    nMovie.Countries.Add(aContry.Name)
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Director / Writer
        If FilteredOptions.bMainDirectors OrElse FilteredOptions.bMainWriters Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In Result.Credits.Crew
                    If FilteredOptions.bMainDirectors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nMovie.Directors.Add(aCrew.Name)
                    End If
                    If FilteredOptions.bMainWriters AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nMovie.Credits.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Genres
        If FilteredOptions.bMainGenres Then
            If Result.Genres.Count > 0 Then
                nMovie.Genres.AddRange(Result.Genres.Select(Function(f) f.Name))
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.Genres.Count > 0 Then
                nMovie.Genres.AddRange(_Fallback_Movie.Genres.Select(Function(f) f.Name))
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'OriginalTitle
        If FilteredOptions.bMainOriginalTitle Then
            nMovie.OriginalTitle = Result.OriginalTitle
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Plot
        If FilteredOptions.bMainPlot Then
            If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                nMovie.Plot = Result.Overview
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Overview) Then
                nMovie.Plot = _Fallback_Movie.Overview
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
        If GetPoster Then
            If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                _sPoster = String.Concat(_client.Config.Images.BaseUrl, "w92", Result.PosterPath)
            Else
                _sPoster = String.Empty
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Premiered
        If FilteredOptions.bMainPremiered Then
            Dim nDate As Date? = Nothing
            If Result.ReleaseDate.HasValue Then
                nDate = Result.ReleaseDate.Value
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.ReleaseDate.HasValue Then
                nDate = _Fallback_Movie.ReleaseDate.Value
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nMovie.Premiered = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Rating
        If FilteredOptions.bMainRating Then
            nMovie.Ratings.Add(New MediaContainers.RatingDetails With {
                               .Max = 10,
                               .Type = "themoviedb",
                               .Value = Result.VoteAverage,
                               .Votes = Result.VoteCount
                               })
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Runtime
        If FilteredOptions.bMainRuntime Then
            If Result.Runtime IsNot Nothing Then
                nMovie.Runtime = CStr(Result.Runtime)
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.Runtime IsNot Nothing Then
                nMovie.Runtime = CStr(_Fallback_Movie.Runtime)
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Studios
        If FilteredOptions.bMainStudios Then
            If Result.ProductionCompanies.Count > 0 Then
                nMovie.Studios.AddRange(Result.ProductionCompanies.Select(Function(f) f.Name))
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Tagline
        If FilteredOptions.bMainTagline Then
            If Result.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Tagline) Then
                nMovie.Tagline = Result.Tagline
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Tagline) Then
                nMovie.Tagline = _Fallback_Movie.Tagline
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Title
        If FilteredOptions.bMainTitle Then
            If Not String.IsNullOrEmpty(Result.Title) Then
                nMovie.Title = Result.Title
            ElseIf RunFallback_Movie(Result.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Title) Then
                nMovie.Title = _Fallback_Movie.Title
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Trailer
        If FilteredOptions.bMainTrailer Then
            Dim nTrailers As New List(Of TMDbLib.Objects.General.Video)
            If Result.Videos IsNot Nothing AndAlso Result.Videos.Results.Count > 0 Then
                nTrailers = Result.Videos.Results
            ElseIf RunFallback_Movie(Result.Id) AndAlso _Fallback_Movie.Videos Is Nothing AndAlso _Fallback_Movie.Videos.Results.Count > 0 Then
                nTrailers = _Fallback_Movie.Videos.Results
            End If

            For Each aTrailer In nTrailers
                Dim nTrailer = YouTube.Scraper.GetVideoDetails(aTrailer.Key)
                If nTrailer IsNot Nothing Then
                    nMovie.Trailer = nTrailer.UrlForNfo
                    Exit For
                End If
            Next
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        _Fallback_Movie = Nothing
        Return nMovie
    End Function

    Public Function GetInfo_Movieset(ByVal tmdbId As Integer, ByVal filteredOptions As Structures.ScrapeOptions, ByVal getPoster As Boolean) As MediaContainers.Movieset
        _Fallback_Movieset = Nothing
        If tmdbId = -1 Then Return Nothing
        Dim APIResult As Task(Of TMDbLib.Objects.Collections.Collection) = Task.Run(Function() _client.GetCollectionAsync(tmdbId))

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse Not APIResult.Result.Id > 0 OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Warn(String.Format("[TMDB_Data] [Abort] No API result for TMDB Collection ID [{0}]", tmdbId))
            Return Nothing
        End If

        Dim Result As TMDbLib.Objects.Collections.Collection = APIResult.Result
        Dim nMovieSet As New MediaContainers.Movieset With {
                .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.MovieSet) With {.TMDbId = Result.Id}
            }

        If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

        'Plot
        If filteredOptions.bMainPlot Then
            If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                nMovieSet.Plot = HttpUtility.HtmlDecode(Result.Overview)
            ElseIf RunFallback_Movieset(Result.Id) AndAlso _Fallback_Movieset.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movieset.Overview) Then
                nMovieSet.Plot = HttpUtility.HtmlDecode(_Fallback_Movieset.Overview)
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
        If getPoster Then
            If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                _sPoster = String.Concat(_client.Config.Images.BaseUrl, "w92", Result.PosterPath)
            Else
                _sPoster = String.Empty
            End If
        End If

        'Title
        If filteredOptions.bMainTitle Then
            If Not String.IsNullOrEmpty(Result.Name) Then
                nMovieSet.Title = Result.Name
            ElseIf RunFallback_Movieset(Result.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_Movieset.Name) Then
                nMovieSet.Title = _Fallback_Movieset.Name
            End If
        End If

        _Fallback_Movieset = Nothing
        Return nMovieSet

        _Fallback_Movieset = Nothing
        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal showId As Integer, ByVal aired As String, ByRef filteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
        Dim nTVEpisode As New MediaContainers.EpisodeDetails
        Dim ShowInfo As TMDbLib.Objects.TvShows.TvShow

        Dim showAPIResult As Task(Of TMDbLib.Objects.TvShows.TvShow) = Task.Run(Function() _client.GetTvShowAsync(showId))

        ShowInfo = showAPIResult.Result

        For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In ShowInfo.Seasons
            Dim seasonAPIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
            seasonAPIResult = Task.Run(Function() _client.GetTvSeasonAsync(showId, aSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = seasonAPIResult.Result
            Dim EpisodeList As IEnumerable(Of TMDbLib.Objects.Search.TvSeasonEpisode) = SeasonInfo.Episodes.Where(Function(f) CBool(f.AirDate = CDate(aired)))
            If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
                Return GetInfo_TVEpisode(showId, EpisodeList(0).SeasonNumber, EpisodeList(0).EpisodeNumber, filteredOptions)
            ElseIf EpisodeList.Count > 0 Then
                Return Nothing
            End If
        Next

        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal showId As Integer, ByVal seasonNumber As Integer, ByVal episodeNumber As Integer, ByRef filteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
        _Fallback_TVEpisode = Nothing
        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvEpisode) = Task.Run(Function() _client.GetTvEpisodeAsync(showId, seasonNumber, episodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse APIResult.Result.Id Is Nothing OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", showId, seasonNumber, episodeNumber))
            Return Nothing
        End If

        Dim Result As TMDbLib.Objects.TvShows.TvEpisode = APIResult.Result
        Dim nTVEpisode As New MediaContainers.EpisodeDetails With {.Scrapersource = "TMDB"}

        'IDs
        nTVEpisode.UniqueIDs.TMDbId = CInt(Result.Id)
        If Result.ExternalIds IsNot Nothing AndAlso Result.ExternalIds.TvdbId IsNot Nothing Then nTVEpisode.UniqueIDs.TVDbId = CInt(Result.ExternalIds.TvdbId)
        If Result.ExternalIds IsNot Nothing AndAlso Result.ExternalIds.ImdbId IsNot Nothing Then nTVEpisode.UniqueIDs.IMDbId = Result.ExternalIds.ImdbId

        'Episode # Standard
        If Result.EpisodeNumber >= 0 Then
            nTVEpisode.Episode = Result.EpisodeNumber
        End If

        'Season # Standard
        If Result.SeasonNumber >= 0 Then
            nTVEpisode.Season = Result.SeasonNumber
        End If

        'Cast (Actors)
        If filteredOptions.bEpisodeActors Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.Credits.Cast
                    nTVEpisode.Actors.Add(New MediaContainers.Person With {
                                          .Name = aCast.Name,
                                          .Role = aCast.Character,
                                          .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                          .TMDB = CStr(aCast.Id)})
                Next
            End If
        End If

        'Aired
        If filteredOptions.bEpisodeAired Then
            Dim nDate As Date? = Nothing
            If Result.AirDate.HasValue Then
                nDate = Result.AirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nTVEpisode.Aired = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        'Director / Writer
        If filteredOptions.bEpisodeCredits OrElse filteredOptions.bEpisodeDirectors Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In Result.Credits.Crew
                    If filteredOptions.bEpisodeCredits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nTVEpisode.Credits.Add(aCrew.Name)
                    End If
                    If filteredOptions.bEpisodeDirectors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nTVEpisode.Directors.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        'Guest Stars
        If filteredOptions.bEpisodeGuestStars Then
            If Result.GuestStars IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.GuestStars
                    nTVEpisode.GuestStars.Add(New MediaContainers.Person With {
                                              .Name = aCast.Name,
                                              .Role = aCast.Character,
                                              .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                              .TMDB = CStr(aCast.Id)
                                              })
                Next
            End If
        End If

        'Plot
        If filteredOptions.bEpisodePlot Then
            If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                nTVEpisode.Plot = Result.Overview
            ElseIf RunFallback_TVEpisode(showId, seasonNumber, episodeNumber) AndAlso _Fallback_TVEpisode.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVEpisode.Overview) Then
                nTVEpisode.Plot = _Fallback_TVEpisode.Overview
            End If
        End If

        'Rating
        'VoteAverage is rounded to get a comparable result as with other reviews 
        If filteredOptions.bEpisodeRating Then
            nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {
                                   .Max = 10,
                                   .Type = "themoviedb",
                                   .Value = Math.Round(Result.VoteAverage, 1),
                                   .Votes = Result.VoteCount
                                   })
        End If

        'ThumbPoster
        If Result.StillPath IsNot Nothing Then
            nTVEpisode.ThumbPoster.URLOriginal = String.Concat(_client.Config.Images.BaseUrl, "original", Result.StillPath)
            nTVEpisode.ThumbPoster.URLThumb = String.Concat(_client.Config.Images.BaseUrl, "w185", Result.StillPath)
        End If

        'Title
        If filteredOptions.bEpisodeTitle Then
            If Not String.IsNullOrEmpty(Result.Name) Then
                nTVEpisode.Title = Result.Name
            ElseIf RunFallback_TVEpisode(showId, seasonNumber, episodeNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVEpisode.Name) Then
                nTVEpisode.Title = _Fallback_TVEpisode.Name
            End If
        End If

        _Fallback_TVEpisode = Nothing
        Return nTVEpisode
    End Function

    Public Sub GetInfo_TVSeason(ByRef tvShow As MediaContainers.TVShow, ByVal showId As Integer, ByVal seasonNumber As Integer, ByRef scrapeModifiers As Structures.ScrapeModifiers, ByRef filteredOptions As Structures.ScrapeOptions)
        _Fallback_TVSeason = Nothing
        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason) = Task.Run(Function() _client.GetTvSeasonAsync(showId, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse APIResult.Result.Id Is Nothing OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or season not found: ShowID={0}, Season={1}", showId, seasonNumber))
            Return
        End If

        Dim Result As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

        If scrapeModifiers.withSeasons Then
            Dim nTVSeason As New MediaContainers.SeasonDetails With {.Scrapersource = "TMDB"}

            'IDs
            nTVSeason.UniqueIDs.TMDbId = CInt(Result.Id)
            If Result.ExternalIds IsNot Nothing AndAlso Result.ExternalIds.TvdbId IsNot Nothing Then nTVSeason.UniqueIDs.TVDbId = CInt(Result.ExternalIds.TvdbId)

            'Season #
            If Result.SeasonNumber >= 0 Then
                nTVSeason.Season = Result.SeasonNumber
            End If

            'Aired
            If filteredOptions.bSeasonAired Then
                Dim nDate As Date? = Nothing
                If Result.AirDate.HasValue Then
                    nDate = Result.AirDate
                End If
                If nDate.HasValue Then
                    'always save date in same date format not depending on users language setting!
                    nTVSeason.Aired = nDate.Value.ToString("yyyy-MM-dd")
                End If
            End If

            'Plot
            If filteredOptions.bSeasonPlot Then
                If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                    nTVSeason.Plot = Result.Overview
                ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso _Fallback_TVSeason.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Overview) Then
                    nTVSeason.Plot = _Fallback_TVSeason.Overview
                End If
            End If

            'Title
            If filteredOptions.bSeasonTitle Then
                If Not String.IsNullOrEmpty(Result.Name) Then
                    nTVSeason.Title = Result.Name
                ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Name) Then
                    nTVSeason.Title = _Fallback_TVSeason.Name
                End If
            End If

            tvShow.KnownSeasons.Add(nTVSeason)
        End If

        If scrapeModifiers.withEpisodes AndAlso Result.Episodes IsNot Nothing Then
            For Each aEpisode As TMDbLib.Objects.Search.TvSeasonEpisode In Result.Episodes
                tvShow.KnownEpisodes.Add(GetInfo_TVEpisode(showId, aEpisode.SeasonNumber, aEpisode.EpisodeNumber, filteredOptions))
            Next
        End If

        _Fallback_TVSeason = Nothing
    End Sub

    Public Function GetInfo_TVSeason(ByVal showId As Integer, ByVal seasonNumber As Integer, ByRef filteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
        _Fallback_TVSeason = Nothing
        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason) = Task.Run(Function() _client.GetTvSeasonAsync(showId, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse APIResult.Result.Id Is Nothing OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", showId, seasonNumber))
            Return Nothing
        End If

        Dim Result As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result
        Dim nTVSeason As New MediaContainers.SeasonDetails With {.Scrapersource = "TMDB"}

        'IDs
        nTVSeason.UniqueIDs.TMDbId = CInt(Result.Id)
        If Result.ExternalIds IsNot Nothing AndAlso Result.ExternalIds.TvdbId IsNot Nothing Then nTVSeason.UniqueIDs.TVDbId = CInt(Result.ExternalIds.TvdbId)

        'Season #
        If Result.SeasonNumber >= 0 Then
            nTVSeason.Season = Result.SeasonNumber
        End If

        'Aired
        If filteredOptions.bSeasonAired Then
            Dim nDate As Date? = Nothing
            If Result.AirDate.HasValue Then
                nDate = Result.AirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nTVSeason.Aired = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        'Plot
        If filteredOptions.bSeasonPlot Then
            If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                nTVSeason.Plot = Result.Overview
            ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso _Fallback_TVSeason.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Overview) Then
                nTVSeason.Plot = _Fallback_TVSeason.Overview
            End If
        End If

        'Title
        If filteredOptions.bSeasonTitle Then
            If Not String.IsNullOrEmpty(Result.Name) Then
                nTVSeason.Title = Result.Name
            ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Name) Then
                nTVSeason.Title = _Fallback_TVSeason.Name
            End If
        End If

        _Fallback_TVSeason = Nothing
        Return nTVSeason
    End Function
    ''' <summary>
    '''  Scrape TV Show details from TMDB
    ''' </summary>
    ''' <param name="showId">TMDB ID of tv show to be scraped</param>
    ''' <param name="getPoster">Scrape posters for the movie?</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_TVShow(ByVal showId As Integer, ByRef scrapeModifiers As Structures.ScrapeModifiers, ByRef filteredOptions As Structures.ScrapeOptions, ByVal getPoster As Boolean) As MediaContainers.TVShow
        If showId = -1 Then Return Nothing
        _Fallback_TVShow = Nothing

        If bwTMDB.CancellationPending Then Return Nothing

        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow) = Task.Run(Function() _client.GetTvShowAsync(showId, TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))

        If APIResult Is Nothing OrElse APIResult.Result Is Nothing OrElse Not APIResult.Result.Id > 0 OrElse APIResult.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or tv show not found: [{0}]", showId))
            Return Nothing
        End If

        Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result
        Dim nTVShow As New MediaContainers.TVShow With {.Scrapersource = "TMDB"}

        'IDs
        nTVShow.UniqueIDs.TMDbId = Result.Id
        If Result.ExternalIds.TvdbId IsNot Nothing AndAlso Integer.TryParse(Result.ExternalIds.TvdbId, 0) Then nTVShow.UniqueIDs.TVDbId = CInt(Result.ExternalIds.TvdbId)
        If Result.ExternalIds.ImdbId IsNot Nothing Then nTVShow.UniqueIDs.IMDbId = Result.ExternalIds.ImdbId

        If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

        'Actors
        If filteredOptions.bMainActors Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.Credits.Cast
                    nTVShow.Actors.Add(New MediaContainers.Person With {
                                           .Name = aCast.Name,
                                           .Role = aCast.Character,
                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                           .TMDB = CStr(aCast.Id)
                                           })
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Certifications
        If filteredOptions.bMainCertifications Then
            If Result.ContentRatings IsNot Nothing AndAlso Result.ContentRatings.Results IsNot Nothing AndAlso Result.ContentRatings.Results.Count > 0 Then
                For Each aCountry In Result.ContentRatings.Results
                    If Not String.IsNullOrEmpty(aCountry.Rating) Then
                        Dim CertificationLanguage = APIXML.CertificationLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = aCountry.Iso_3166_1.ToLower)
                        If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.Name) Then
                            nTVShow.Certifications.Add(String.Concat(CertificationLanguage.Name, ":", aCountry.Rating))
                        Else
                            _Logger.Warn("Unhandled certification language encountered: {0}", aCountry.Iso_3166_1.ToLower)
                        End If
                    End If
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Countries 
        If filteredOptions.bMainCountries Then
            If Result.ProductionCountries IsNot Nothing AndAlso Result.ProductionCountries.Count > 0 Then
                For Each aCountry In Result.ProductionCountries
                    nTVShow.Countries.Add(aCountry.Name)
                Next
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Creators
        If filteredOptions.bMainCreators Then
            nTVShow.Creators.AddRange(Result.CreatedBy.Select(Function(f) f.Name))
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Genres
        If filteredOptions.bMainGenres Then
            If Result.Genres.Count > 0 Then
                nTVShow.Genres.AddRange(Result.Genres.Select(Function(f) f.Name))
            ElseIf RunFallback_TVShow(Result.Id) AndAlso _Fallback_TVShow.Genres.Count > 0 Then
                nTVShow.Genres.AddRange(_Fallback_TVShow.Genres.Select(Function(f) f.Name))
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'OriginalTitle
        If filteredOptions.bMainOriginalTitle Then
            nTVShow.OriginalTitle = Result.OriginalName
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Plot
        If filteredOptions.bMainPlot Then
            If Result.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Overview) Then
                nTVShow.Plot = Result.Overview
            ElseIf RunFallback_TVShow(Result.Id) AndAlso _Fallback_TVShow.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Overview) Then
                nTVShow.Plot = _Fallback_TVShow.Overview
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
        If getPoster Then
            If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                _sPoster = String.Concat(_client.Config.Images.BaseUrl, "w92", Result.PosterPath)
            Else
                _sPoster = String.Empty
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Premiered
        If filteredOptions.bMainPremiered Then
            Dim nDate As Date? = Nothing
            If Result.FirstAirDate.HasValue Then
                nDate = Result.FirstAirDate
            ElseIf RunFallback_TVShow(Result.Id) AndAlso _Fallback_TVShow.FirstAirDate.HasValue Then
                nDate = _Fallback_TVShow.FirstAirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nTVShow.Premiered = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Rating
        If filteredOptions.bMainRating Then
            nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {
                                    .Max = 10,
                                    .Type = "themoviedb",
                                    .Value = Result.VoteAverage,
                                    .Votes = Result.VoteCount
                                    })
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Runtime
        If filteredOptions.bMainRuntime Then
            If Result.EpisodeRunTime IsNot Nothing AndAlso Result.EpisodeRunTime.Count > 0 Then
                nTVShow.Runtime = CStr(Result.EpisodeRunTime.Item(0))
            ElseIf RunFallback_TVShow(Result.Id) AndAlso _Fallback_TVShow.EpisodeRunTime IsNot Nothing AndAlso _Fallback_TVShow.EpisodeRunTime.Count > 0 Then
                nTVShow.Runtime = CStr(_Fallback_TVShow.EpisodeRunTime.Item(0))
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Status
        If filteredOptions.bMainStatus Then
            If Not String.IsNullOrEmpty(Result.Status) Then
                nTVShow.Status = Result.Status
            ElseIf RunFallback_TVShow(Result.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Status) Then
                nTVShow.Status = _Fallback_TVShow.Status
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Studios
        If filteredOptions.bMainStudios Then
            If Result.Networks.Count > 0 Then
                nTVShow.Studios.AddRange(Result.Networks.Select(Function(f) f.Name))
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Tagline
        If filteredOptions.bMainTagline Then
            If Result.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.Tagline) Then
                nTVShow.Tagline = Result.Tagline
            ElseIf RunFallback_TVShow(Result.Id) AndAlso _Fallback_TVShow.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Tagline) Then
                nTVShow.Tagline = _Fallback_TVShow.Tagline
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Title
        If filteredOptions.bMainTitle Then
            If Not String.IsNullOrEmpty(Result.Name) Then
                nTVShow.Title = Result.Name
            ElseIf RunFallback_TVShow(Result.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Name) Then
                nTVShow.Title = _Fallback_TVShow.Name
            End If
        End If

        If bwTMDB.CancellationPending Then Return Nothing

        'Seasons and Episodes
        If scrapeModifiers.withEpisodes OrElse scrapeModifiers.withSeasons Then
            For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In Result.Seasons
                GetInfo_TVSeason(nTVShow, Result.Id, aSeason.SeasonNumber, scrapeModifiers, filteredOptions)
            Next
        End If
        _Fallback_TVShow = Nothing
        Return nTVShow
    End Function

    Public Function GetTMDBbyIMDB(ByVal imdbId As String) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _client.FindAsync(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbId))

            If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                Return APIResult.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetTMDBbyTVDB(ByVal tvdbId As Integer) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _client.FindAsync(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbId.ToString))

            If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                Return APIResult.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetMovieStudios(ByVal imdbIdOrTmdbId As String) As List(Of String)
        If String.IsNullOrEmpty(imdbIdOrTmdbId) Then Return New List(Of String)

        Dim alStudio As New List(Of String)
        Dim Movie As TMDbLib.Objects.Movies.Movie = Nothing

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie) = Nothing

        If imdbIdOrTmdbId.ToLower.StartsWith("tt") Then
            APIResult = Task.Run(Function() _client.GetMovieAsync(imdbIdOrTmdbId))
        ElseIf Integer.TryParse(imdbIdOrTmdbId, 0) Then
            APIResult = Task.Run(Function() _client.GetMovieAsync(CInt(imdbIdOrTmdbId)))
        End If

        If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing Then
            Movie = APIResult.Result
        End If

        If Movie IsNot Nothing AndAlso Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
            For Each cStudio In Movie.ProductionCompanies
                alStudio.Add(cStudio.Name)
            Next
        End If

        Return alStudio
    End Function

    Public Function GetSearchMovieInfo(ByVal strMovieName As String, ByRef oDBMovie As Database.DBElement, ByVal eType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
        Dim r As SearchResults_Movie = SearchMovie(strMovieName, CInt(If(Not String.IsNullOrEmpty(oDBMovie.Movie.Year), oDBMovie.Movie.Year, Nothing)))

        Select Case eType
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId.ToString, FilteredOptions, False)
                Else
                    Using dlgSearch As New dlgTMDBSearchResults_Movie(_addonSettings, Me)
                        If dlgSearch.ShowDialog(r, strMovieName, oDBMovie.Filename) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_Movie(dlgSearch.Result.UniqueIDs.TMDbId.ToString, FilteredOptions, False)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId.ToString, FilteredOptions, False)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If r.Matches.Count > 0 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId.ToString, FilteredOptions, False)
                End If
        End Select

        Return Nothing
    End Function

    Public Function GetSearchMovieSetInfo(ByVal title As String, ByRef oDBMovieSet As Database.DBElement, ByVal eType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.Movieset
        Dim r As SearchResults_MovieSet = SearchMovieSet(title)

        Select Case eType
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movieset(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
                Else
                    Using dlgSearch As New dlgTMDBSearchResults_MovieSet(_addonSettings, Me)
                        If dlgSearch.ShowDialog(r, title) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_Movieset(dlgSearch.Result.UniqueIDs.TMDbId, FilteredOptions, False)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movieset(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If r.Matches.Count > 0 Then
                    Return GetInfo_Movieset(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
                End If
        End Select

        Return Nothing
    End Function

    Public Function GetSearchTVShowInfo(ByVal strShowName As String, ByRef oDBTV As Database.DBElement, ByVal eType As Enums.ScrapeType, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.TVShow
        Dim r As SearchResults_TVShow = SearchTVShow(strShowName)

        Select Case eType
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If r.Matches.Count = 1 Then
                    Return GetInfo_TVShow(r.Matches.Item(0).UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                Else
                    Using dlgSearch As New dlgTMDBSearchResults_TV(_addonSettings, Me)
                        If dlgSearch.ShowDialog(r, strShowName, oDBTV.ShowPath) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_TVShow(dlgSearch.Result.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If r.Matches.Count = 1 Then
                    Return GetInfo_TVShow(r.Matches.Item(0).UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If r.Matches.Count > 0 Then
                    Return GetInfo_TVShow(r.Matches.Item(0).UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                End If
        End Select

        Return Nothing
    End Function

    Public Sub GetSearchMovieInfoAsync(ByVal tmdbID As String, ByRef FilteredOptions As Structures.ScrapeOptions)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_Movie,
                  .Parameter = tmdbID, .ScrapeOptions = FilteredOptions})
        End If
    End Sub

    Public Sub GetSearchMovieSetInfoAsync(ByVal tmdbColID As String, ByRef FilteredOptions As Structures.ScrapeOptions)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_MovieSet,
                  .Parameter = tmdbColID, .ScrapeOptions = FilteredOptions})
        End If
    End Sub

    Public Sub GetSearchTVShowInfoAsync(ByVal tmdbID As String, ByRef FilteredOptions As Structures.ScrapeOptions)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow,
                  .Parameter = tmdbID, .ScrapeOptions = FilteredOptions})
        End If
    End Sub

    Private Function RunFallback_Movie(ByVal tmdbId As Integer) As Boolean
        If Not _addonSettings.FallBackEng Then Return False
        If _Fallback_Movie Is Nothing Then
            Dim APIResultE = Task.Run(Function() _clientE.GetMovieAsync(tmdbId, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
            _Fallback_Movie = APIResultE.Result
            Return _Fallback_Movie IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_Movieset(ByVal tmdbId As Integer) As Boolean
        If Not _addonSettings.FallBackEng Then Return False
        If _Fallback_Movieset Is Nothing Then
            Dim APIResultE = Task.Run(Function() _clientE.GetCollectionAsync(tmdbId))
            _Fallback_Movieset = APIResultE.Result
            Return _Fallback_Movieset IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVEpisode(ByVal showId As Integer, ByVal seasonNumber As Integer, ByVal episodeNumber As Integer) As Boolean
        If Not _addonSettings.FallBackEng Then Return False
        If _Fallback_TVEpisode Is Nothing Then
            Dim APIResultE = Task.Run(Function() _clientE.GetTvEpisodeAsync(showId, seasonNumber, episodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))
            _Fallback_TVEpisode = APIResultE.Result
            Return _Fallback_TVEpisode IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVSeason(ByVal showId As Integer, ByVal seasonNumber As Integer) As Boolean
        If Not _addonSettings.FallBackEng Then Return False
        If _Fallback_TVSeason Is Nothing Then
            Dim APIResultE = Task.Run(Function() _clientE.GetTvSeasonAsync(showId, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))
            _Fallback_TVSeason = APIResultE.Result
            Return _Fallback_TVSeason IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVShow(ByVal showId As Integer) As Boolean
        If Not _addonSettings.FallBackEng Then Return False
        If _Fallback_TVShow Is Nothing Then
            Dim APIResultE = Task.Run(Function() _clientE.GetTvShowAsync(showId, TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
            _Fallback_TVShow = APIResultE.Result
            Return _Fallback_TVShow IsNot Nothing
        Else
            Return True
        End If
    End Function

    Public Sub SearchAsync_Movie(ByVal sMovie As String, ByRef filterOptions As Structures.ScrapeOptions, Optional ByVal sYear As String = "")
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        Dim intYear As Integer

        Integer.TryParse(sYear, intYear)

        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies,
                  .Parameter = sMovie, .ScrapeOptions = filterOptions, .Year = intYear})
        End If
    End Sub

    Public Sub SearchAsync_MovieSet(ByVal sMovieSet As String, ByRef filterOptions As Structures.ScrapeOptions)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.MovieSets,
                  .Parameter = sMovieSet, .ScrapeOptions = filterOptions})
        End If
    End Sub

    Public Sub SearchAsync_TVShow(ByVal sShow As String, ByRef filterOptions As Structures.ScrapeOptions)

        If Not bwTMDB.IsBusy Then
            bwTMDB.WorkerReportsProgress = False
            bwTMDB.WorkerSupportsCancellation = True
            bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows,
                  .Parameter = sShow, .ScrapeOptions = filterOptions})
        End If
    End Sub

    Private Function SearchMovie(ByVal strMovie As String, Optional ByVal iYear As Integer = 0) As SearchResults_Movie
        If String.IsNullOrEmpty(strMovie) Then Return New SearchResults_Movie

        Dim R As New SearchResults_Movie
        Dim Page As Integer = 1
        Dim Movies As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
        Dim TotP As Integer
        Dim aE As Boolean

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie))
        APIResult = Task.Run(Function() _client.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear))

        Movies = APIResult.Result

        If Movies.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
            APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear))
            Movies = APIResult.Result
            aE = True
        End If

        'try -1 year if no search result was found
        If Movies.TotalResults = 0 AndAlso iYear > 0 AndAlso _addonSettings.SearchDeviant Then
            APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear - 1))
            Movies = APIResult.Result

            If Movies.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
                APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear - 1))
                Movies = APIResult.Result
                aE = True
            End If

            'still no search result, try +1 year
            If Movies.TotalResults = 0 Then
                APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear + 1))
                Movies = APIResult.Result

                If Movies.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
                    APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear + 1))
                    Movies = APIResult.Result
                    aE = True
                End If
            End If
        End If

        If Movies.TotalResults > 0 Then
            TotP = Movies.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If Movies.Results IsNot Nothing Then
                    For Each aMovie In Movies.Results
                        Dim tOriginalTitle As String = String.Empty
                        Dim tPlot As String = String.Empty
                        Dim tThumbPoster As MediaContainers.Image = New MediaContainers.Image
                        Dim tTitle As String = String.Empty
                        Dim tYear As String = String.Empty

                        If aMovie.OriginalTitle IsNot Nothing Then tOriginalTitle = aMovie.OriginalTitle
                        If aMovie.Overview IsNot Nothing Then tPlot = aMovie.Overview
                        If aMovie.PosterPath IsNot Nothing Then
                            tThumbPoster.URLOriginal = _client.Config.Images.BaseUrl & "original" & aMovie.PosterPath
                            tThumbPoster.URLThumb = _client.Config.Images.BaseUrl & "w185" & aMovie.PosterPath
                        End If
                        If aMovie.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.ReleaseDate)) Then tYear = CStr(aMovie.ReleaseDate.Value.Year)
                        If aMovie.Title IsNot Nothing Then tTitle = aMovie.Title

                        Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie With {
                        .OriginalTitle = tOriginalTitle,
                        .Plot = tPlot,
                        .Title = tTitle,
                        .ThumbPoster = tThumbPoster,
                        .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.TMDbId = aMovie.Id},
                        .Year = tYear
                        }
                        R.Matches.Add(lNewMovie)
                    Next
                End If
                Page = Page + 1
                If aE Then
                    APIResult = Task.Run(Function() _clientE.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear))
                    Movies = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _client.SearchMovieAsync(strMovie, Page, _addonSettings.GetAdultItems, iYear))
                    Movies = APIResult.Result
                End If
            End While
        End If

        Return R
    End Function

    Private Function SearchMovieSet(ByVal strMovieSet As String) As SearchResults_MovieSet
        If String.IsNullOrEmpty(strMovieSet) Then Return New SearchResults_MovieSet

        Dim R As New SearchResults_MovieSet
        Dim Page As Integer = 1
        Dim MovieSets As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection)
        Dim TotP As Integer
        Dim aE As Boolean

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection))
        APIResult = Task.Run(Function() _client.SearchCollectionAsync(strMovieSet, Page))

        MovieSets = APIResult.Result

        If MovieSets.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
            APIResult = Task.Run(Function() _clientE.SearchCollectionAsync(strMovieSet, Page))
            MovieSets = APIResult.Result
            aE = True
        End If

        If MovieSets.TotalResults > 0 Then
            Dim strTitle As String = String.Empty
            Dim strPlot As String = String.Empty
            TotP = MovieSets.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If MovieSets.Results IsNot Nothing Then
                    For Each aMovieSet In MovieSets.Results
                        If aMovieSet.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(aMovieSet.Name) Then
                            strTitle = aMovieSet.Name
                        End If
                        'If aMovieSet.overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(aMovieSet.overview) Then
                        '    strPlot = aMovieSet.overview
                        'End If
                        R.Matches.Add(New MediaContainers.Movieset With {
                                      .Title = strTitle,
                                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.MovieSet) With {.TMDbId = aMovieSet.Id}
                                      })
                    Next
                End If
                Page = Page + 1
                If aE Then
                    APIResult = Task.Run(Function() _clientE.SearchCollectionAsync(strMovieSet, Page))
                    MovieSets = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _client.SearchCollectionAsync(strMovieSet, Page))
                    MovieSets = APIResult.Result
                End If
            End While
        End If

        Return R
    End Function

    Private Function SearchTVShow(ByVal showName As String) As SearchResults_TVShow
        If String.IsNullOrEmpty(showName) Then Return New SearchResults_TVShow

        Dim R As New SearchResults_TVShow
        Dim Page As Integer = 1
        Dim Shows As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv)
        Dim TotP As Integer
        Dim aE As Boolean

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv))
        APIResult = Task.Run(Function() _client.SearchTvShowAsync(showName, Page))

        Shows = APIResult.Result

        If Shows.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
            APIResult = Task.Run(Function() _clientE.SearchTvShowAsync(showName, Page))
            Shows = APIResult.Result
            aE = True
        End If

        If Shows.TotalResults > 0 Then
            Dim strTitle As String = String.Empty
            Dim strYear As String = String.Empty
            TotP = Shows.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If Shows.Results IsNot Nothing Then
                    For Each aShow In Shows.Results
                        If aShow.Name Is Nothing OrElse (aShow.Name IsNot Nothing AndAlso String.IsNullOrEmpty(aShow.Name)) Then
                            If aShow.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(aShow.OriginalName) Then
                                strTitle = aShow.OriginalName
                            End If
                        Else
                            strTitle = aShow.Name
                        End If
                        If aShow.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aShow.FirstAirDate)) Then
                            strYear = CStr(aShow.FirstAirDate.Value.Year)
                        End If
                        R.Matches.Add(New MediaContainers.TVShow With {
                                      .Premiered = strYear,
                                      .Title = strTitle,
                                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVShow) With {.TMDbId = aShow.Id}
                                      })
                    Next
                End If
                Page = Page + 1
                If aE Then
                    APIResult = Task.Run(Function() _clientE.SearchTvShowAsync(showName, Page))
                    Shows = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _client.SearchTvShowAsync(showName, Page))
                    Shows = APIResult.Result
                End If
            End While
        End If

        Return R
    End Function

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim FullCast As Boolean
        Dim FullCrew As Boolean
        Dim Parameter As String
        Dim ScrapeModifiers As Structures.ScrapeModifiers
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim Search As SearchType
        Dim Year As Integer

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim Result As Object
        Dim ResultType As SearchType

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class