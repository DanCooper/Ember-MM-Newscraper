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

Namespace TMDB

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

        Private _Matches As New List(Of MediaContainers.MovieSet)

#End Region 'Fields

#Region "Properties"

        Public Property Matches() As List(Of MediaContainers.MovieSet)
            Get
                Return _Matches
            End Get
            Set(ByVal value As List(Of MediaContainers.MovieSet))
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

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _TMDBApi As TMDbLib.Client.TMDbClient  'preferred language
        Private _TMDBApiE As TMDbLib.Client.TMDbClient 'english language
        Private _SpecialSettings As TMDB_Data.SpecialSettings
        Private _sPoster As String

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

#End Region

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
        Public Event SearchInfoDownloaded_MovieSet(ByVal strPoster As String, ByVal sInfo As MediaContainers.MovieSet)
        Public Event SearchInfoDownloaded_TVShow(ByVal strPoster As String, ByVal sInfo As MediaContainers.TVShow)

        Public Event SearchResultsDownloaded_Movie(ByVal mResults As SearchResults_Movie)
        Public Event SearchResultsDownloaded_MovieSet(ByVal mResults As SearchResults_MovieSet)
        Public Event SearchResultsDownloaded_TVShow(ByVal mResults As SearchResults_TVShow)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal SpecialSettings As TMDB_Data.SpecialSettings)
            Try
                _SpecialSettings = SpecialSettings

                _TMDBApi = New TMDbLib.Client.TMDbClient(_SpecialSettings.APIKey)
                _TMDBApi.GetConfig()
                _TMDBApi.DefaultLanguage = _SpecialSettings.PrefLanguage

                If _SpecialSettings.FallBackEng Then
                    _TMDBApiE = New TMDbLib.Client.TMDbClient(_SpecialSettings.APIKey)
                    _TMDBApiE.GetConfig()
                    _TMDBApiE.DefaultLanguage = "en"
                Else
                    _TMDBApiE = _TMDBApi
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub CancelAsync()
            If bwTMDB.IsBusy Then bwTMDB.CancelAsync()

            While bwTMDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        Public Sub GetMovieID(ByVal DBMovie As Database.DBElement)
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            APIResult = Task.Run(Function() _TMDBApi.GetMovie(DBMovie.Movie.ID))

            Movie = APIResult.Result
            If Movie Is Nothing Then Return

            DBMovie.Movie.TMDBID = CStr(Movie.Id)
        End Sub

        Public Function GetMovieID(ByVal imdbID As String) As String
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            APIResult = Task.Run(Function() _TMDBApi.GetMovie(imdbID))

            Movie = APIResult.Result
            If Movie Is Nothing Then Return String.Empty

            Return CStr(Movie.Id)
        End Function

        Public Function GetMovieCollectionID(ByVal imdbID As String) As String
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            APIResult = Task.Run(Function() _TMDBApi.GetMovie(imdbID))

            Movie = APIResult.Result
            If Movie Is Nothing Then Return String.Empty

            If Movie.BelongsToCollection IsNot Nothing AndAlso Movie.BelongsToCollection.Item(0).Id > 0 Then
                Return CStr(Movie.BelongsToCollection.Item(0).Id)
            Else
                Return String.Empty
            End If
        End Function
        ''' <summary>
        '''  Scrape MovieDetails from TMDB
        ''' </summary>
        ''' <param name="strID">TMDBID or ID (IMDB ID starts with "tt") of movie to be scraped</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        Public Function GetMovieInfo(ByVal strID As String, ByVal GetPoster As Boolean, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal IsSearch As Boolean) As MediaContainers.Movie
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nMovie As New MediaContainers.Movie

            If bwTMDB.CancellationPending Then Return Nothing

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
            Dim APIResultE As Task(Of TMDbLib.Objects.Movies.Movie)

            If strID.Substring(0, 2).ToLower = "tt" Then
                'search movie by IMDB ID
                APIResult = Task.Run(Function() _TMDBApi.GetMovie(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
                If _SpecialSettings.FallBackEng Then
                    APIResultE = Task.Run(Function() _TMDBApiE.GetMovie(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
                Else
                    APIResultE = APIResult
                End If
            Else
                'search movie by TMDB ID
                APIResult = Task.Run(Function() _TMDBApi.GetMovie(CInt(strID), TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
                If _SpecialSettings.FallBackEng Then
                    APIResultE = Task.Run(Function() _TMDBApiE.GetMovie(CInt(strID), TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
                Else
                    APIResultE = APIResult
                End If
            End If

            Dim Result As TMDbLib.Objects.Movies.Movie = APIResult.Result
            Dim ResultE As TMDbLib.Objects.Movies.Movie = APIResultE.Result

            If (Result Is Nothing AndAlso Not _SpecialSettings.FallBackEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                (Not Result.Id > 0 AndAlso Not _SpecialSettings.FallBackEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
                logger.Error(String.Format("Can't scrape or movie not found: [0]", strID))
                Return Nothing
            End If

            nMovie.Scrapersource = "TMDB"

            'IDs
            nMovie.TMDBID = CStr(Result.Id)
            If Result.ImdbId IsNot Nothing Then nMovie.ID = Result.ImdbId

            If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

            'Cast (Actors)
            If FilteredOptions.bMainActors Then
                If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.Movies.Cast In Result.Credits.Cast
                        nMovie.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                           .Role = aCast.Character,
                                                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Certifications
            If FilteredOptions.bMainCertifications Then
                If Result.Releases IsNot Nothing AndAlso Result.Releases.Countries IsNot Nothing AndAlso Result.Releases.Countries.Count > 0 Then
                    For Each cCountry In Result.Releases.Countries
                        If Not String.IsNullOrEmpty(cCountry.Certification) Then
                            Try
                                Dim tCountry As String = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = cCountry.Iso_3166_1.ToLower).name
                                nMovie.Certifications.Add(String.Concat(tCountry, ":", cCountry.Certification))
                            Catch ex As Exception
                                logger.Warn("Unhandled certification language encountered: {0}", cCountry.Iso_3166_1.ToLower)
                            End Try
                        End If
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Collection ID
            If FilteredOptions.bMainCollectionID Then
                If Result.BelongsToCollection Is Nothing OrElse (Result.BelongsToCollection IsNot Nothing AndAlso Result.BelongsToCollection.Count = 0) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.BelongsToCollection IsNot Nothing AndAlso ResultE.BelongsToCollection.Count > 0 Then
                        nMovie.AddSet(Nothing, ResultE.BelongsToCollection.Item(0).Name, Nothing, CStr(ResultE.BelongsToCollection.Item(0).Id))
                        nMovie.TMDBColID = CStr(ResultE.BelongsToCollection.Item(0).Id)
                    End If
                Else
                    nMovie.AddSet(Nothing, Result.BelongsToCollection.Item(0).Name, Nothing, CStr(ResultE.BelongsToCollection.Item(0).Id))
                    nMovie.TMDBColID = CStr(Result.BelongsToCollection.Item(0).Id)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Countries
            If FilteredOptions.bMainCountries Then
                If Result.ProductionCountries IsNot Nothing AndAlso Result.ProductionCountries.Count > 0 Then
                    For Each aContry As TMDbLib.Objects.Movies.ProductionCountry In Result.ProductionCountries
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
                Dim aGenres As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre) = Nothing
                If Result.Genres Is Nothing OrElse (Result.Genres IsNot Nothing AndAlso Result.Genres.Count = 0) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Genres IsNot Nothing AndAlso ResultE.Genres.Count > 0 Then
                        aGenres = ResultE.Genres
                    End If
                Else
                    aGenres = Result.Genres
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
                        nMovie.Genres.Add(tGenre.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'OriginalTitle
            If FilteredOptions.bMainOriginalTitle Then
                If Result.OriginalTitle Is Nothing OrElse (Result.OriginalTitle IsNot Nothing AndAlso String.IsNullOrEmpty(Result.OriginalTitle)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.OriginalTitle) Then
                        nMovie.OriginalTitle = ResultE.OriginalTitle
                    End If
                Else
                    nMovie.OriginalTitle = Result.OriginalTitle
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Plot
            If FilteredOptions.bMainPlot Then
                If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                        nMovie.Plot = ResultE.Overview
                    End If
                Else
                    nMovie.Plot = Result.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                    _sPoster = String.Concat(_TMDBApi.Config.Images.BaseUrl, "w92", Result.PosterPath)
                Else
                    _sPoster = String.Empty
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Rating
            If FilteredOptions.bMainRating Then
                nMovie.Rating = CStr(Result.VoteAverage)
                nMovie.Votes = CStr(Result.VoteCount)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'ReleaseDate
            If FilteredOptions.bMainRelease Then
                Dim ScrapedDate As String = String.Empty
                If Result.ReleaseDate Is Nothing OrElse (Result.ReleaseDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.ReleaseDate))) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.ReleaseDate)) Then
                        ScrapedDate = CStr(ResultE.ReleaseDate)
                    End If
                Else
                    ScrapedDate = CStr(Result.ReleaseDate)
                End If
                If Not String.IsNullOrEmpty(ScrapedDate) Then
                    Dim RelDate As Date
                    If Date.TryParse(ScrapedDate, RelDate) Then
                        'always save date in same date format not depending on users language setting!
                        nMovie.ReleaseDate = RelDate.ToString("yyyy-MM-dd")
                    Else
                        nMovie.ReleaseDate = ScrapedDate
                    End If
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Runtime
            If FilteredOptions.bMainRuntime Then
                If Result.Runtime Is Nothing OrElse Result.Runtime = 0 Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Runtime IsNot Nothing Then
                        nMovie.Runtime = CStr(ResultE.Runtime)
                    End If
                Else
                    nMovie.Runtime = CStr(Result.Runtime)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Studios
            If FilteredOptions.bMainStudios Then
                If Result.ProductionCompanies IsNot Nothing AndAlso Result.ProductionCompanies.Count > 0 Then
                    For Each cStudio In Result.ProductionCompanies
                        nMovie.Studios.Add(cStudio.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Tagline
            If FilteredOptions.bMainTagline Then
                If Result.Tagline Is Nothing OrElse (Result.Tagline IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Tagline)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Tagline) Then
                        nMovie.Tagline = ResultE.Tagline
                    End If
                Else
                    nMovie.Tagline = Result.Tagline
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Title
            If FilteredOptions.bMainTitle Then
                If Result.Title Is Nothing OrElse (Result.Title IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Title)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Title) Then
                        nMovie.Title = ResultE.Title
                    End If
                Else
                    nMovie.Title = Result.Title
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Trailer
            If FilteredOptions.bMainTrailer Then
                Dim aTrailers As List(Of TMDbLib.Objects.General.Video) = Nothing
                If Result.Videos Is Nothing OrElse (Result.Videos IsNot Nothing AndAlso Result.Videos.Results.Count = 0) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Videos IsNot Nothing AndAlso ResultE.Videos.Results.Count > 0 Then
                        aTrailers = ResultE.Videos.Results
                    End If
                Else
                    aTrailers = Result.Videos.Results
                End If

                If aTrailers IsNot Nothing AndAlso aTrailers.Count > 0 Then
                    For Each tTrailer In aTrailers
                        If YouTube.Scraper.IsAvailable("http://www.youtube.com/watch?hd=1&v=" & tTrailer.Key) Then
                            nMovie.Trailer = "http://www.youtube.com/watch?hd=1&v=" & tTrailer.Key
                            Exit For
                        End If
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Year
            If FilteredOptions.bMainYear Then
                If Result.ReleaseDate Is Nothing OrElse (Result.ReleaseDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.ReleaseDate))) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.ReleaseDate)) Then
                        nMovie.Year = CStr(ResultE.ReleaseDate.Value.Year)
                    End If
                Else
                    nMovie.Year = CStr(Result.ReleaseDate.Value.Year)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            Return nMovie
        End Function

        Public Function GetMovieSetInfo(ByVal strID As String, ByVal GetPoster As Boolean, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal IsSearch As Boolean) As MediaContainers.MovieSet
            If String.IsNullOrEmpty(strID) OrElse Not Integer.TryParse(strID, 0) Then Return Nothing

            Dim nMovieSet As New MediaContainers.MovieSet

            If bwTMDB.CancellationPending Then Return Nothing

            Dim APIResult As Task(Of TMDbLib.Objects.Collections.Collection)
            Dim APIResultE As Task(Of TMDbLib.Objects.Collections.Collection)

            APIResult = Task.Run(Function() _TMDBApi.GetCollection(CInt(strID), _SpecialSettings.PrefLanguage))
            If _SpecialSettings.FallBackEng Then
                APIResultE = Task.Run(Function() _TMDBApiE.GetCollection(CInt(strID)))
            Else
                APIResultE = APIResult
            End If

            If APIResult Is Nothing OrElse APIResultE Is Nothing Then
                Return Nothing
            End If

            Dim Result As TMDbLib.Objects.Collections.Collection = APIResult.Result
            Dim ResultE As TMDbLib.Objects.Collections.Collection = APIResultE.Result

            If (Result Is Nothing AndAlso Not _SpecialSettings.FallBackEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                (Not Result.Id > 0 AndAlso Not _SpecialSettings.FallBackEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
                logger.Warn(String.Format("[TMDB_Data] [Abort] No API result for TMDB Collection ID [{0}]", strID))
                Return Nothing
            End If

            nMovieSet.TMDB = CStr(Result.Id)

            If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

            'Plot
            If FilteredOptions.bMainPlot Then
                If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                        'nMovieSet.Plot = MovieSetE.Overview
                        nMovieSet.Plot = ResultE.Overview
                    End If
                Else
                    'nMovieSet.Plot = MovieSet.Overview
                    nMovieSet.Plot = Result.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                    _sPoster = String.Concat(_TMDBApi.Config.Images.BaseUrl, "w92", Result.PosterPath)
                Else
                    _sPoster = String.Empty
                End If
            End If

            'Title
            If FilteredOptions.bMainTitle Then
                If Result.Name Is Nothing OrElse (Result.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Name)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Name) Then
                        'nMovieSet.Title = MovieSetE.Name
                        nMovieSet.Title = ResultE.Name
                    End If
                Else
                    'nMovieSet.Title = MovieSet.Name
                    nMovieSet.Title = Result.Name
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            Return nMovieSet
        End Function
        ''' <summary>
        '''  Scrape TV Show details from TMDB
        ''' </summary>
        ''' <param name="strID">TMDB ID of tv show to be scraped</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <returns>True: success, false: no success</returns>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions, ByVal GetPoster As Boolean) As MediaContainers.TVShow
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow

            If bwTMDB.CancellationPending Then Return Nothing

            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
            Dim APIResultE As Task(Of TMDbLib.Objects.TvShows.TvShow)

            'search movie by TMDB ID
            APIResult = Task.Run(Function() _TMDBApi.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
            If _SpecialSettings.FallBackEng Then
                APIResultE = Task.Run(Function() _TMDBApiE.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
            Else
                APIResultE = APIResult
            End If

            If APIResult Is Nothing OrElse APIResultE Is Nothing Then
                Return Nothing
            End If

            Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result
            Dim ResultE As TMDbLib.Objects.TvShows.TvShow = APIResultE.Result

            If (Result Is Nothing AndAlso Not _SpecialSettings.FallBackEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                (Not Result.Id > 0 AndAlso Not _SpecialSettings.FallBackEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
                logger.Error(String.Format("Can't scrape or tv show not found: [{0}]", strID))
                Return Nothing
            End If

            nTVShow.Scrapersource = "TMDB"

            'IDs
            nTVShow.TMDB = CStr(Result.Id)
            If Result.ExternalIds.TvdbId IsNot Nothing Then nTVShow.TVDB = CStr(Result.ExternalIds.TvdbId)
            If Result.ExternalIds.ImdbId IsNot Nothing Then nTVShow.IMDB = Result.ExternalIds.ImdbId

            If bwTMDB.CancellationPending Or Result Is Nothing Then Return Nothing

            'Actors
            If FilteredOptions.bMainActors Then
                If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.Credits.Cast
                        nTVShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                           .Role = aCast.Character,
                                                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Certifications
            If FilteredOptions.bMainCertifications Then
                If Result.ContentRatings IsNot Nothing AndAlso Result.ContentRatings.Results IsNot Nothing AndAlso Result.ContentRatings.Results.Count > 0 Then
                    For Each aCountry In Result.ContentRatings.Results
                        If Not String.IsNullOrEmpty(aCountry.Rating) Then
                            Try
                                Dim tCountry As String = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = aCountry.Iso_3166_1.ToLower).name
                                nTVShow.Certifications.Add(String.Concat(tCountry, ":", aCountry.Rating))
                            Catch ex As Exception
                                logger.Warn("Unhandled certification language encountered: {0}", aCountry.Iso_3166_1.ToLower)
                            End Try
                        End If
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Countries 'TODO: Change from OriginCountry to ProductionCountries (not yet supported by API)
            'If FilteredOptions.bMainCountry Then
            '    If Show.OriginCountry IsNot Nothing AndAlso Show.OriginCountry.Count > 0 Then
            '        For Each aCountry As String In Show.OriginCountry
            '            nShow.Countries.Add(aCountry)
            '        Next
            '    End If
            'End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Creators
            If FilteredOptions.bMainCreators Then
                If Result.CreatedBy IsNot Nothing Then
                    For Each aCreator As TMDbLib.Objects.People.Person In Result.CreatedBy
                        nTVShow.Creators.Add(aCreator.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Genres
            If FilteredOptions.bMainGenres Then
                Dim aGenres As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre) = Nothing
                If Result.Genres Is Nothing OrElse (Result.Genres IsNot Nothing AndAlso Result.Genres.Count = 0) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Genres IsNot Nothing AndAlso ResultE.Genres.Count > 0 Then
                        aGenres = ResultE.Genres
                    End If
                Else
                    aGenres = Result.Genres
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
                        nTVShow.Genres.Add(tGenre.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'OriginalTitle
            If FilteredOptions.bMainOriginalTitle Then
                If Result.OriginalName Is Nothing OrElse (Result.OriginalName IsNot Nothing AndAlso String.IsNullOrEmpty(Result.OriginalName)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.OriginalName) Then
                        nTVShow.OriginalTitle = ResultE.OriginalName
                    End If
                Else
                    nTVShow.OriginalTitle = ResultE.OriginalName
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Plot
            If FilteredOptions.bMainPlot Then
                If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                        nTVShow.Plot = ResultE.Overview
                    End If
                Else
                    nTVShow.Plot = Result.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
                    _sPoster = String.Concat(_TMDBApi.Config.Images.BaseUrl, "w92", Result.PosterPath)
                Else
                    _sPoster = String.Empty
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Premiered
            If FilteredOptions.bMainPremiered Then
                Dim ScrapedDate As String = String.Empty
                If Result.FirstAirDate Is Nothing OrElse (Result.FirstAirDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.FirstAirDate))) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.FirstAirDate)) Then
                        ScrapedDate = CStr(ResultE.FirstAirDate)
                    End If
                Else
                    ScrapedDate = CStr(Result.FirstAirDate)
                End If
                If Not String.IsNullOrEmpty(ScrapedDate) Then
                    Dim RelDate As Date
                    If Date.TryParse(ScrapedDate, RelDate) Then
                        'always save date in same date format not depending on users language setting!
                        nTVShow.Premiered = RelDate.ToString("yyyy-MM-dd")
                    Else
                        nTVShow.Premiered = ScrapedDate
                    End If
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Rating
            If FilteredOptions.bMainRating Then
                nTVShow.Rating = CStr(Result.VoteAverage)
                nTVShow.Votes = CStr(Result.VoteCount)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Runtime
            If FilteredOptions.bMainRuntime Then
                If Result.EpisodeRunTime Is Nothing OrElse Result.EpisodeRunTime.Count = 0 Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.EpisodeRunTime IsNot Nothing Then
                        nTVShow.Runtime = CStr(ResultE.EpisodeRunTime.Item(0))
                    End If
                Else
                    nTVShow.Runtime = CStr(Result.EpisodeRunTime.Item(0))
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Status
            If FilteredOptions.bMainStatus Then
                If Result.Status Is Nothing OrElse (Result.Status IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Status)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Status IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Status) Then
                        nTVShow.Status = ResultE.Status
                    End If
                Else
                    nTVShow.Status = Result.Status
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Studios
            If FilteredOptions.bMainStudios Then
                If Result.Networks IsNot Nothing AndAlso Result.Networks.Count > 0 Then
                    For Each aStudio In Result.Networks
                        nTVShow.Studios.Add(aStudio.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Title
            If FilteredOptions.bMainTitle Then
                If Result.Name Is Nothing OrElse (Result.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Name)) Then
                    If _SpecialSettings.FallBackEng AndAlso ResultE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Name) Then
                        nTVShow.Title = ResultE.Name
                    End If
                Else
                    nTVShow.Title = Result.Name
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            ''Trailer
            'If Options.bTrailer Then
            '    Dim aTrailers As List(Of TMDbLib.Objects.TvShows.Video) = Nothing
            '    If Show.Videos Is Nothing OrElse (Show.Videos IsNot Nothing AndAlso Show.Videos.Results.Count = 0) Then
            '        If _MySettings.FallBackEng AndAlso ShowE.Videos IsNot Nothing AndAlso ShowE.Videos.Results.Count > 0 Then
            '            aTrailers = ShowE.Videos.Results
            '        End If
            '    Else
            '        aTrailers = Show.Videos.Results
            '    End If

            '    If aTrailers IsNot Nothing AndAlso aTrailers IsNot Nothing AndAlso aTrailers.Count > 0 Then
            '        nShow.Trailer = "http://www.youtube.com/watch?hd=1&v=" & aTrailers.Item(0).Key
            '    End If
            'End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Seasons and Episodes
            If ScrapeModifiers.withEpisodes OrElse ScrapeModifiers.withSeasons Then
                For Each aSeason As TMDbLib.Objects.TvShows.TvSeason In Result.Seasons
                    GetTVSeasonInfo(nTVShow, Result.Id, aSeason.SeasonNumber, ScrapeModifiers, FilteredOptions)
                Next
            End If

            Return nTVShow
        End Function

        Public Sub GetTVSeasonInfo(ByRef nTVShow As MediaContainers.TVShow, ByVal ShowID As Integer, ByVal SeasonNumber As Integer, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions)
            Dim nSeason As New MediaContainers.SeasonDetails

            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
            APIResult = Task.Run(Function() _TMDBApi.GetTvSeason(ShowID, SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

            nSeason.TMDB = CStr(SeasonInfo.Id)
            If SeasonInfo.ExternalIds IsNot Nothing AndAlso SeasonInfo.ExternalIds.TvdbId IsNot Nothing Then nSeason.TVDB = CStr(SeasonInfo.ExternalIds.TvdbId)

            If ScrapeModifiers.withSeasons Then

                'Aired
                If FilteredOptions.bSeasonAired Then
                    If SeasonInfo.AirDate IsNot Nothing Then
                        Dim ScrapedDate As String = CStr(SeasonInfo.AirDate)
                        If Not String.IsNullOrEmpty(ScrapedDate) Then
                            Dim RelDate As Date
                            If Date.TryParse(ScrapedDate, RelDate) Then
                                'always save date in same date format not depending on users language setting!
                                nSeason.Aired = RelDate.ToString("yyyy-MM-dd")
                            Else
                                nSeason.Aired = ScrapedDate
                            End If
                        End If
                    End If
                End If

                'Plot
                If FilteredOptions.bSeasonPlot Then
                    If SeasonInfo.Overview IsNot Nothing Then
                        nSeason.Plot = SeasonInfo.Overview
                    End If
                End If

                'Season #
                If CInt(SeasonInfo.SeasonNumber) >= 0 Then
                    nSeason.Season = CInt(SeasonInfo.SeasonNumber)
                End If

                'Title
                If SeasonInfo.Name IsNot Nothing Then
                    nSeason.Title = SeasonInfo.Name
                End If

                nTVShow.KnownSeasons.Add(nSeason)
            End If

            If ScrapeModifiers.withEpisodes AndAlso SeasonInfo.Episodes IsNot Nothing Then
                For Each aEpisode As TMDbLib.Objects.TvShows.TvEpisode In SeasonInfo.Episodes
                    nTVShow.KnownEpisodes.Add(GetTVEpisodeInfo(aEpisode, FilteredOptions))
                    'nShowContainer.KnownEpisodes.Add(GetTVEpisodeInfo(ShowID, SeasonNumber, aEpisode.EpisodeNumber, Options))
                Next
            End If
        End Sub

        Public Function GetTVEpisodeInfo(ByVal ShowID As Integer, ByVal Aired As String, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim nTVEpisode As New MediaContainers.EpisodeDetails
            Dim ShowInfo As TMDbLib.Objects.TvShows.TvShow

            Dim showAPIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
            showAPIResult = Task.Run(Function() _TMDBApi.GetTvShow(CInt(ShowID)))

            ShowInfo = showAPIResult.Result

            For Each aSeason As TMDbLib.Objects.TvShows.TvSeason In ShowInfo.Seasons
                Dim seasonAPIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
                seasonAPIResult = Task.Run(Function() _TMDBApi.GetTvSeason(ShowID, aSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

                Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = seasonAPIResult.Result
                For Each aEpisode As TMDbLib.Objects.TvShows.TvEpisode In SeasonInfo.Episodes.Where(Function(f) f.AirDate = CDate(Aired))
                    Return GetTVEpisodeInfo(aEpisode, FilteredOptions)
                    'Return GetTVEpisodeInfo(ShowID, season.SeasonNumber, episode.EpisodeNumber, Options)
                Next
            Next

            Return Nothing
        End Function

        Public Function GetTVEpisodeInfo(ByVal tmdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvEpisode)
            APIResult = Task.Run(Function() _TMDBApi.GetTvEpisode(tmdbID, SeasonNumber, EpisodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))

            Dim EpisodeInfo As TMDbLib.Objects.TvShows.TvEpisode = APIResult.Result

            If EpisodeInfo Is Nothing OrElse EpisodeInfo.Id Is Nothing OrElse Not EpisodeInfo.Id > 0 Then
                logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", tmdbID, SeasonNumber, EpisodeNumber))
                Return Nothing
            End If

            Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(EpisodeInfo, FilteredOptions)
            Return nEpisode
        End Function

        Public Function GetTVEpisodeInfo(ByRef EpisodeInfo As TMDbLib.Objects.TvShows.TvEpisode, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim nTVEpisode As New MediaContainers.EpisodeDetails

            nTVEpisode.Scrapersource = "TMDB"

            'IDs
            nTVEpisode.TMDB = CStr(EpisodeInfo.Id)
            If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.TvdbId IsNot Nothing Then nTVEpisode.TVDB = CStr(EpisodeInfo.ExternalIds.TvdbId)
            If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.ImdbId IsNot Nothing Then nTVEpisode.IMDB = EpisodeInfo.ExternalIds.ImdbId

            'Episode # Standard
            If EpisodeInfo.EpisodeNumber >= 0 Then
                nTVEpisode.Episode = EpisodeInfo.EpisodeNumber
            End If

            'Season # Standard
            If CInt(EpisodeInfo.SeasonNumber) >= 0 Then
                nTVEpisode.Season = CInt(EpisodeInfo.SeasonNumber)
            End If

            'Cast (Actors)
            If FilteredOptions.bEpisodeActors Then
                If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.Credits.Cast
                        nTVEpisode.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                           .Role = aCast.Character,
                                                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            'Aired
            If FilteredOptions.bEpisodeAired Then
                Dim ScrapedDate As String = CStr(EpisodeInfo.AirDate)
                If Not String.IsNullOrEmpty(ScrapedDate) AndAlso Not ScrapedDate = "00:00:00" Then
                    Dim RelDate As Date
                    If Date.TryParse(ScrapedDate, RelDate) Then
                        'always save date in same date format not depending on users language setting!
                        nTVEpisode.Aired = RelDate.ToString("yyyy-MM-dd")
                    Else
                        nTVEpisode.Aired = ScrapedDate
                    End If
                End If
            End If

            'Director / Writer
            If FilteredOptions.bEpisodeCredits OrElse FilteredOptions.bEpisodeDirectors Then
                If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Crew IsNot Nothing Then
                    For Each aCrew As TMDbLib.Objects.General.Crew In EpisodeInfo.Credits.Crew
                        If FilteredOptions.bEpisodeCredits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                            nTVEpisode.Credits.Add(aCrew.Name)
                        End If
                        If FilteredOptions.bEpisodeDirectors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                            nTVEpisode.Directors.Add(aCrew.Name)
                        End If
                    Next
                End If
            End If

            'Guest Stars
            If FilteredOptions.bEpisodeGuestStars Then
                If EpisodeInfo.GuestStars IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.GuestStars
                        nTVEpisode.GuestStars.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                           .Role = aCast.Character,
                                                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            'Plot
            If FilteredOptions.bEpisodePlot Then
                If EpisodeInfo.Overview IsNot Nothing Then
                    nTVEpisode.Plot = EpisodeInfo.Overview
                End If
            End If

            'Rating
            If FilteredOptions.bEpisodeRating Then
                nTVEpisode.Rating = CStr(EpisodeInfo.VoteAverage)
                nTVEpisode.Votes = CStr(EpisodeInfo.VoteCount)
            End If

            'ThumbPoster
            If EpisodeInfo.StillPath IsNot Nothing Then
                nTVEpisode.ThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & EpisodeInfo.StillPath
                nTVEpisode.ThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & EpisodeInfo.StillPath
            End If

            'Title
            If FilteredOptions.bEpisodeTitle Then
                If EpisodeInfo.Name IsNot Nothing Then
                    nTVEpisode.Title = EpisodeInfo.Name
                End If
            End If

            Return nTVEpisode
        End Function

        Public Function GetTVSeasonInfo(ByVal tmdbID As Integer, ByVal SeasonNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
            APIResult = Task.Run(Function() _TMDBApi.GetTvSeason(tmdbID, SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

            If SeasonInfo Is Nothing OrElse SeasonInfo.Id Is Nothing OrElse Not SeasonInfo.Id > 0 Then
                logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", tmdbID, SeasonNumber))
                Return Nothing
            End If

            Dim nTVSeason As MediaContainers.SeasonDetails = GetTVSeasonInfo(SeasonInfo, FilteredOptions)
            Return nTVSeason
        End Function

        Public Function GetTVSeasonInfo(ByRef SeasonInfo As TMDbLib.Objects.TvShows.TvSeason, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
            Dim nTVSeason As New MediaContainers.SeasonDetails

            nTVSeason.Scrapersource = "TMDB"

            'IDs
            nTVSeason.TMDB = CStr(SeasonInfo.Id)
            If SeasonInfo.ExternalIds IsNot Nothing AndAlso SeasonInfo.ExternalIds.TvdbId IsNot Nothing Then nTVSeason.TVDB = CStr(SeasonInfo.ExternalIds.TvdbId)

            'Season #
            If CInt(SeasonInfo.SeasonNumber) >= 0 Then
                nTVSeason.Season = CInt(SeasonInfo.SeasonNumber)
            End If

            'Aired
            If FilteredOptions.bSeasonAired Then
                If SeasonInfo.AirDate IsNot Nothing Then
                    Dim ScrapedDate As String = CStr(SeasonInfo.AirDate)
                    If Not String.IsNullOrEmpty(ScrapedDate) Then
                        Dim RelDate As Date
                        If Date.TryParse(ScrapedDate, RelDate) Then
                            'always save date in same date format not depending on users language setting!
                            nTVSeason.Aired = RelDate.ToString("yyyy-MM-dd")
                        Else
                            nTVSeason.Aired = ScrapedDate
                        End If
                    End If
                End If
            End If

            'Plot
            If FilteredOptions.bSeasonPlot Then
                If SeasonInfo.Overview IsNot Nothing Then
                    nTVSeason.Plot = SeasonInfo.Overview
                End If
            End If

            'Title
            If FilteredOptions.bSeasonTitle Then
                If SeasonInfo.Name IsNot Nothing Then
                    nTVSeason.Title = SeasonInfo.Name
                End If
            End If

            Return nTVSeason
        End Function

        Public Function GetTMDBbyIMDB(ByVal imdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
                APIResult = Task.Run(Function() _TMDBApi.Find(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbID))

                If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                    tmdbID = APIResult.Result.TvResults.Item(0).Id.ToString
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tmdbID
        End Function

        Public Function GetTMDBbyTVDB(ByVal tvdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
                APIResult = Task.Run(Function() _TMDBApi.Find(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbID))

                If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                    tmdbID = APIResult.Result.TvResults.Item(0).Id.ToString
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tmdbID
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            If String.IsNullOrEmpty(strID) OrElse strID.Length > 2 Then Return New List(Of String)

            Dim alStudio As New List(Of String)
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)

            If strID.Substring(0, 2).ToLower = "tt" Then
                APIResult = Task.Run(Function() _TMDBApi.GetMovie(strID))
            Else
                APIResult = Task.Run(Function() _TMDBApi.GetMovie(CInt(strID)))
            End If

            Movie = APIResult.Result

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
                        Return GetMovieInfo(r.Matches.Item(0).TMDBID, False, FilteredOptions, True)
                    Else
                        Using dlgSearch As New dlgTMDBSearchResults_Movie(_SpecialSettings, Me)
                            If dlgSearch.ShowDialog(r, strMovieName, oDBMovie.Filename) = DialogResult.OK Then
                                If Not String.IsNullOrEmpty(dlgSearch.Result.TMDBID) Then
                                    Return GetMovieInfo(dlgSearch.Result.TMDBID, False, FilteredOptions, True)
                                End If
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        Return GetMovieInfo(r.Matches.Item(0).TMDBID, False, FilteredOptions, True)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    Dim exactHaveYear As Integer = FindYear(oDBMovie.Filename, r.Matches)
                    If r.Matches.Count = 1 Then
                        Return GetMovieInfo(r.Matches.Item(0).TMDBID, False, FilteredOptions, True)
                    ElseIf r.Matches.Count > 1 Then
                        Return GetMovieInfo(r.Matches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).TMDBID, False, FilteredOptions, True)
                    End If
            End Select

            Return Nothing
        End Function

        Public Function GetSearchMovieSetInfo(ByVal strMovieSetName As String, ByRef oDBMovieSet As Database.DBElement, ByVal eType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MovieSet
            Dim r As SearchResults_MovieSet = SearchMovieSet(strMovieSetName)

            Select Case eType
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If r.Matches.Count = 1 Then
                        Return GetMovieSetInfo(r.Matches.Item(0).TMDB, False, FilteredOptions, True)
                    Else
                        Using dlgSearch As New dlgTMDBSearchResults_MovieSet(_SpecialSettings, Me)
                            If dlgSearch.ShowDialog(r, strMovieSetName) = DialogResult.OK Then
                                If Not String.IsNullOrEmpty(dlgSearch.Result.TMDB) Then
                                    Return GetMovieSetInfo(dlgSearch.Result.TMDB, False, FilteredOptions, True)
                                End If
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        Return GetMovieSetInfo(r.Matches.Item(0).TMDB, False, FilteredOptions, True)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    If r.Matches.Count > 0 Then
                        Return GetMovieSetInfo(r.Matches.Item(0).TMDB, False, FilteredOptions, True)
                    End If
            End Select

            Return Nothing
        End Function

        Public Function GetSearchTVShowInfo(ByVal strShowName As String, ByRef oDBTV As Database.DBElement, ByVal eType As Enums.ScrapeType, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.TVShow
            Dim r As SearchResults_TVShow = SearchTVShow(strShowName)

            Select Case eType
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TMDB, ScrapeModifiers, FilteredOptions, False)
                    Else
                        Using dlgSearch As New dlgTMDBSearchResults_TV(_SpecialSettings, Me)
                            If dlgSearch.ShowDialog(r, strShowName, oDBTV.ShowPath) = DialogResult.OK Then
                                If Not String.IsNullOrEmpty(dlgSearch.Result.TMDB) Then
                                    Return GetTVShowInfo(dlgSearch.Result.TMDB, ScrapeModifiers, FilteredOptions, False)
                                End If
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TMDB, ScrapeModifiers, FilteredOptions, False)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    If r.Matches.Count > 0 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TMDB, ScrapeModifiers, FilteredOptions, False)
                    End If
            End Select

            Return Nothing
        End Function

        Private Function FindYear(ByVal tmpname As String, ByVal lst As List(Of MediaContainers.Movie)) As Integer
            Dim tmpyear As String = ""
            Dim i As Integer
            Dim ret As Integer = -1
            tmpname = Path.GetFileNameWithoutExtension(tmpname)
            tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
            i = tmpname.LastIndexOf(" ")
            If i >= 0 Then
                tmpyear = tmpname.Substring(i + 1, tmpname.Length - i - 1)
                If Integer.TryParse(tmpyear, 0) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
                    For c = 0 To lst.Count - 1
                        If lst(c).Year = tmpyear Then
                            ret = c
                            Exit For
                        End If
                    Next
                End If
            End If
            Return ret
        End Function

        Public Sub GetSearchMovieInfoAsync(ByVal tmdbID As String, ByVal Movie As MediaContainers.Movie, ByRef FilteredOptions As Structures.ScrapeOptions)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_Movie,
                  .Parameter = tmdbID, .Movie = Movie, .ScrapeOptions = FilteredOptions})
            End If
        End Sub

        Public Sub GetSearchMovieSetInfoAsync(ByVal tmdbColID As String, ByVal MovieSet As MediaContainers.MovieSet, ByRef FilteredOptions As Structures.ScrapeOptions)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_MovieSet,
                  .Parameter = tmdbColID, .MovieSet = MovieSet, .ScrapeOptions = FilteredOptions})
            End If
        End Sub

        Public Sub GetSearchTVShowInfoAsync(ByVal tmdbID As String, ByVal Show As MediaContainers.TVShow, ByRef FilteredOptions As Structures.ScrapeOptions)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow,
                  .Parameter = tmdbID, .TVShow = Show, .ScrapeOptions = FilteredOptions})
            End If
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByRef filterOptions As Structures.ScrapeOptions, Optional ByVal sYear As String = "")
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Dim tYear As Integer = 0

            If Not String.IsNullOrEmpty(sYear) AndAlso Integer.TryParse(sYear, 0) Then
                tYear = CInt(sYear)
            End If

            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies,
                  .Parameter = sMovie, .ScrapeOptions = filterOptions, .Year = tYear})
            End If
        End Sub

        Public Sub SearchMovieSetAsync(ByVal sMovieSet As String, ByRef filterOptions As Structures.ScrapeOptions)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.MovieSets,
                  .Parameter = sMovieSet, .ScrapeOptions = filterOptions})
            End If
        End Sub

        Public Sub SearchTVShowAsync(ByVal sShow As String, ByRef filterOptions As Structures.ScrapeOptions)

            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows,
                  .Parameter = sShow, .ScrapeOptions = filterOptions})
            End If
        End Sub

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
                    Dim r As MediaContainers.Movie = GetMovieInfo(Args.Parameter, True, Args.ScrapeOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_Movie, .Result = r}

                Case SearchType.SearchDetails_MovieSet
                    Dim r As MediaContainers.MovieSet = GetMovieSetInfo(Args.Parameter, True, Args.ScrapeOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_MovieSet, .Result = r}

                Case SearchType.SearchDetails_TVShow
                    Dim r As MediaContainers.TVShow = GetTVShowInfo(Args.Parameter, Args.ScrapeModifiers, Args.ScrapeOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = r}
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
                    Dim moviesetInfo As MediaContainers.MovieSet = DirectCast(Res.Result, MediaContainers.MovieSet)
                    RaiseEvent SearchInfoDownloaded_MovieSet(_sPoster, moviesetInfo)

                Case SearchType.SearchDetails_TVShow
                    Dim showInfo As MediaContainers.TVShow = DirectCast(Res.Result, MediaContainers.TVShow)
                    RaiseEvent SearchInfoDownloaded_TVShow(_sPoster, showInfo)
            End Select
        End Sub

        Private Function SearchMovie(ByVal strMovie As String, Optional ByVal iYear As Integer = 0) As SearchResults_Movie
            If String.IsNullOrEmpty(strMovie) Then Return New SearchResults_Movie

            Dim R As New SearchResults_Movie
            Dim Page As Integer = 1
            Dim Movies As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
            Dim TotP As Integer
            Dim aE As Boolean

            Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie))
            APIResult = Task.Run(Function() _TMDBApi.SearchMovie(strMovie, Page, _SpecialSettings.GetAdultItems, iYear))

            Movies = APIResult.Result

            If Movies.TotalResults = 0 AndAlso _SpecialSettings.FallBackEng Then
                APIResult = Task.Run(Function() _TMDBApiE.SearchMovie(strMovie, Page, _SpecialSettings.GetAdultItems, iYear))
                Movies = APIResult.Result
                aE = True
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
                                tThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & aMovie.PosterPath
                                tThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & aMovie.PosterPath
                            End If
                            If aMovie.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.ReleaseDate)) Then tYear = CStr(aMovie.ReleaseDate.Value.Year)
                            If aMovie.Title IsNot Nothing Then tTitle = aMovie.Title

                            Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie With {.OriginalTitle = tOriginalTitle,
                                                                                                     .Plot = tPlot,
                                                                                                     .Title = tTitle,
                                                                                                     .ThumbPoster = tThumbPoster,
                                                                                                     .TMDBID = CStr(aMovie.Id),
                                                                                                     .Year = tYear}
                            R.Matches.Add(lNewMovie)
                        Next
                    End If
                    Page = Page + 1
                    If aE Then
                        APIResult = Task.Run(Function() _TMDBApiE.SearchMovie(strMovie, Page, _SpecialSettings.GetAdultItems, iYear))
                        Movies = APIResult.Result
                    Else
                        APIResult = Task.Run(Function() _TMDBApi.SearchMovie(strMovie, Page, _SpecialSettings.GetAdultItems, iYear))
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
            Dim MovieSets As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchResultCollection)
            Dim TotP As Integer
            Dim aE As Boolean

            Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchResultCollection))
            APIResult = Task.Run(Function() _TMDBApi.SearchCollection(strMovieSet, Page))

            MovieSets = APIResult.Result

            If MovieSets.TotalResults = 0 AndAlso _SpecialSettings.FallBackEng Then
                APIResult = Task.Run(Function() _TMDBApiE.SearchCollection(strMovieSet, Page))
                MovieSets = APIResult.Result
                aE = True
            End If

            If MovieSets.TotalResults > 0 Then
                Dim t1 As String = String.Empty
                Dim t2 As String = String.Empty
                Dim t3 As String = String.Empty
                TotP = MovieSets.TotalPages
                While Page <= TotP AndAlso Page <= 3
                    If MovieSets.Results IsNot Nothing Then
                        For Each aMovieSet In MovieSets.Results
                            t1 = CStr(aMovieSet.Id)
                            If aMovieSet.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(aMovieSet.Name) Then
                                t2 = aMovieSet.Name
                            End If
                            'If aMovieSet.overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(aMovieSet.overview) Then
                            '    t3 = aMovieSet.overview
                            'End If
                            R.Matches.Add(New MediaContainers.MovieSet(t1, t2, t3))
                        Next
                    End If
                    Page = Page + 1
                    If aE Then
                        APIResult = Task.Run(Function() _TMDBApiE.SearchCollection(strMovieSet, Page))
                        MovieSets = APIResult.Result
                    Else
                        APIResult = Task.Run(Function() _TMDBApi.SearchCollection(strMovieSet, Page))
                        MovieSets = APIResult.Result
                    End If
                End While
            End If

            Return R
        End Function

        Private Function SearchTVShow(ByVal strShow As String) As SearchResults_TVShow
            If String.IsNullOrEmpty(strShow) Then Return New SearchResults_TVShow

            Dim R As New SearchResults_TVShow
            Dim Page As Integer = 1
            Dim Shows As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv)
            Dim TotP As Integer
            Dim aE As Boolean

            Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv))
            APIResult = Task.Run(Function() _TMDBApi.SearchTvShow(strShow, Page))

            Shows = APIResult.Result

            If Shows.TotalResults = 0 AndAlso _SpecialSettings.FallBackEng Then
                APIResult = Task.Run(Function() _TMDBApiE.SearchTvShow(strShow, Page))
                Shows = APIResult.Result
                aE = True
            End If

            If Shows.TotalResults > 0 Then
                Dim t1 As String = String.Empty
                Dim t2 As String = String.Empty
                TotP = Shows.TotalPages
                While Page <= TotP AndAlso Page <= 3
                    If Shows.Results IsNot Nothing Then
                        For Each aShow In Shows.Results
                            If aShow.Name Is Nothing OrElse (aShow.Name IsNot Nothing AndAlso String.IsNullOrEmpty(aShow.Name)) Then
                                If aShow.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(aShow.OriginalName) Then
                                    t1 = aShow.OriginalName
                                End If
                            Else
                                t1 = aShow.Name
                            End If
                            If aShow.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aShow.FirstAirDate)) Then
                                t2 = CStr(aShow.FirstAirDate.Value.Year)
                            End If
                            Dim lNewShow As MediaContainers.TVShow = New MediaContainers.TVShow(String.Empty, t1, t2)
                            lNewShow.TMDB = CStr(aShow.Id)
                            R.Matches.Add(lNewShow)
                        Next
                    End If
                    Page = Page + 1
                    If aE Then
                        APIResult = Task.Run(Function() _TMDBApiE.SearchTvShow(strShow, Page))
                        Shows = APIResult.Result
                    Else
                        APIResult = Task.Run(Function() _TMDBApi.SearchTvShow(strShow, Page))
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
            Dim Movie As MediaContainers.Movie
            Dim MovieSet As MediaContainers.MovieSet
            Dim Parameter As String
            Dim ScrapeModifiers As Structures.ScrapeModifiers
            Dim ScrapeOptions As Structures.ScrapeOptions
            Dim Search As SearchType
            Dim TVShow As MediaContainers.TVShow
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

End Namespace

