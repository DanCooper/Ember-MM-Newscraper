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

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
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

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
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

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _TMDBApi As TMDbLib.Client.TMDbClient  'preferred language
        Private _TMDBApiE As TMDbLib.Client.TMDbClient 'english language
        Private _MySettings As sMySettings_ForScraper
        Private strPrivateAPIKey As String = String.Empty
        Private _sPoster As String

        Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

#End Region

#Region "Enumerations"

        Private Enum SearchType
            Movies = 0
            Details = 1
            SearchDetails = 2
            MovieSets = 3
            SearchDetails_MovieSet = 4
        End Enum

#End Region 'Enumerations

#Region "Events"

        Public Event SearchInfoDownloaded_Movie(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchInfoDownloaded_MovieSet(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchResultsDownloaded_Movie(ByVal mResults As TMDB.SearchResults_Movie)

        Public Event SearchResultsDownloaded_MovieSet(ByVal mResults As TMDB.SearchResults_MovieSet)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal Settings As sMySettings_ForScraper)
            Try
                _MySettings = Settings

                _TMDBApi = New TMDbLib.Client.TMDbClient(_MySettings.ApiKey)
                _TMDBApi.GetConfig()
                _TMDBApi.DefaultLanguage = _MySettings.PrefLanguage

                If _MySettings.FallBackEng Then
                    _TMDBApiE = New TMDbLib.Client.TMDbClient(_MySettings.ApiKey)
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

        Public Sub GetMovieID(ByRef DBMovie As Structures.DBMovie)
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Movie = _TMDBApi.GetMovie(DBMovie.Movie.ID)
            If Movie Is Nothing Then Return

            DBMovie.Movie.TMDBID = CStr(Movie.Id)
        End Sub

        Public Function GetMovieID(ByRef imdbID As String) As String
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Movie = _TMDBApi.GetMovie(imdbID)
            If Movie Is Nothing Then Return String.Empty

            Return CStr(Movie.Id)
        End Function

        Public Function GetMovieCollectionID(ByVal imdbID As String) As String
            Dim Movie As TMDbLib.Objects.Movies.Movie

            Movie = _TMDBApi.GetMovie(imdbID)
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
        ''' <param name="nMovie">Container of scraped movie data</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        Public Function GetMovieInfo(ByVal strID As String, ByRef nMovie As MediaContainers.Movie, ByVal FullCrew As Boolean, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_Movie, ByVal IsSearch As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            Dim Movie As TMDbLib.Objects.Movies.Movie
            Dim MovieE As TMDbLib.Objects.Movies.Movie

            'clear nMovie from search results
            nMovie.Clear()

            If bwTMDB.CancellationPending Then Return Nothing

            If strID.Substring(0, 2).ToLower = "tt" Then
                'search movie by IMDB ID
                Movie = _TMDBApi.GetMovie(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
                If _MySettings.FallBackEng Then
                    MovieE = _TMDBApiE.GetMovie(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
                Else
                    MovieE = Movie
                End If
            Else
                'search movie by TMDB ID
                Movie = _TMDBApi.GetMovie(CInt(strID), TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
                If _MySettings.FallBackEng Then
                    MovieE = _TMDBApiE.GetMovie(CInt(strID), TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
                Else
                    MovieE = Movie
                End If
            End If

            If (Movie Is Nothing AndAlso Not _MySettings.FallBackEng) OrElse (Movie Is Nothing AndAlso MovieE Is Nothing) OrElse _
                (Not Movie.Id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not Movie.Id > 0 AndAlso Not MovieE.Id > 0) Then
                logger.Error(String.Concat("Can't scrape or movie not found: ", strID))
                Return False
            End If

            nMovie.Scrapersource = "TMDB"
            nMovie.ID = Movie.ImdbId
            nMovie.TMDBID = CStr(Movie.Id)

            If bwTMDB.CancellationPending Or Movie Is Nothing Then Return Nothing

            'Cast (Actors)
            If Options.bCast Then
                If Movie.Credits IsNot Nothing AndAlso Movie.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.Movies.Cast In Movie.Credits.Cast
                        nMovie.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
                                                                           .Role = aCast.Character, _
                                                                           .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty), _
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Certifications
            If Options.bCert Then
                If Movie.Releases IsNot Nothing AndAlso Movie.Releases.Countries IsNot Nothing AndAlso Movie.Releases.Countries.Count > 0 Then
                    For Each cCountry In Movie.Releases.Countries
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
            If Options.bCollectionID Then
                If Movie.BelongsToCollection Is Nothing OrElse (Movie.BelongsToCollection IsNot Nothing AndAlso Movie.BelongsToCollection.Count = 0) Then
                    If _MySettings.FallBackEng AndAlso MovieE.BelongsToCollection IsNot Nothing AndAlso MovieE.BelongsToCollection.Count > 0 Then
                        nMovie.AddSet(Nothing, MovieE.BelongsToCollection.Item(0).Name, Nothing, CStr(MovieE.BelongsToCollection.Item(0).Id))
                        nMovie.TMDBColID = CStr(MovieE.BelongsToCollection.Item(0).Id)
                    End If
                Else
                    nMovie.AddSet(Nothing, Movie.BelongsToCollection.Item(0).Name, Nothing, CStr(MovieE.BelongsToCollection.Item(0).Id))
                    nMovie.TMDBColID = CStr(Movie.BelongsToCollection.Item(0).Id)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Countries
            If Options.bCountry Then
                If Movie.ProductionCountries IsNot Nothing AndAlso Movie.ProductionCountries.Count > 0 Then
                    For Each aContry As TMDbLib.Objects.Movies.ProductionCountry In Movie.ProductionCountries
                        nMovie.Countries.Add(aContry.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Director / Writer
            If Options.bDirector OrElse Options.bWriters OrElse Options.bFullCrew Then
                If Movie.Credits IsNot Nothing AndAlso Movie.Credits.Crew IsNot Nothing Then
                    For Each aCrew As TMDbLib.Objects.General.Crew In Movie.Credits.Crew
                        If Options.bDirector AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                            nMovie.Directors.Add(aCrew.Name)
                        End If
                        If Options.bWriters AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                            nMovie.Credits.Add(aCrew.Name)
                        End If
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Genres
            If Options.bGenre Then
                Dim aGenres As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre) = Nothing
                If Movie.Genres Is Nothing OrElse (Movie.Genres IsNot Nothing AndAlso Movie.Genres.Count = 0) Then
                    If _MySettings.FallBackEng AndAlso MovieE.Genres IsNot Nothing AndAlso MovieE.Genres.Count > 0 Then
                        aGenres = MovieE.Genres
                    End If
                Else
                    aGenres = Movie.Genres
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
                        nMovie.Genres.Add(tGenre.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'OriginalTitle
            If Options.bOriginalTitle Then
                If Movie.OriginalTitle Is Nothing OrElse (Movie.OriginalTitle IsNot Nothing AndAlso String.IsNullOrEmpty(Movie.OriginalTitle)) Then
                    If _MySettings.FallBackEng AndAlso MovieE.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.OriginalTitle) Then
                        nMovie.OriginalTitle = MovieE.OriginalTitle
                    End If
                Else
                    nMovie.OriginalTitle = Movie.OriginalTitle
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Plot
            If Options.bPlot Then
                If Movie.Overview Is Nothing OrElse (Movie.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Movie.Overview)) Then
                    If _MySettings.FallBackEng AndAlso MovieE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.Overview) Then
                        nMovie.Plot = MovieE.Overview
                    End If
                Else
                    nMovie.Plot = Movie.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                Dim Images As TMDbLib.Objects.General.Images
                Images = _TMDBApi.GetMovieImages(Movie.Id)
                If Images IsNot Nothing AndAlso Images.Posters IsNot Nothing Then
                    If Images.Posters.Count > 0 Then
                        _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                    Else
                        _sPoster = String.Empty
                    End If
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Rating
            If Options.bRating Then
                nMovie.Rating = CStr(Movie.VoteAverage)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'ReleaseDate
            If Options.bRelease Then
                Dim ScrapedDate As String = String.Empty
                If Movie.ReleaseDate Is Nothing OrElse (Movie.ReleaseDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Movie.ReleaseDate))) Then
                    If _MySettings.FallBackEng AndAlso MovieE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(MovieE.ReleaseDate)) Then
                        ScrapedDate = CStr(MovieE.ReleaseDate)
                    End If
                Else
                    ScrapedDate = CStr(Movie.ReleaseDate)
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
            If Options.bRuntime Then
                If Movie.Runtime Is Nothing OrElse Movie.Runtime = 0 Then
                    If _MySettings.FallBackEng AndAlso MovieE.Runtime IsNot Nothing Then
                        nMovie.Runtime = CStr(MovieE.Runtime)
                    End If
                Else
                    nMovie.Runtime = CStr(Movie.Runtime)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Studios
            If Options.bStudio Then
                If Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
                    For Each cStudio In Movie.ProductionCompanies
                        nMovie.Studios.Add(cStudio.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Tagline
            If Options.bTagline Then
                If Movie.Tagline Is Nothing OrElse (Movie.Tagline IsNot Nothing AndAlso String.IsNullOrEmpty(Movie.Tagline)) Then
                    If _MySettings.FallBackEng AndAlso MovieE.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.Tagline) Then
                        nMovie.Tagline = MovieE.Tagline
                    End If
                Else
                    nMovie.Tagline = Movie.Tagline
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Title
            If Options.bTitle Then
                If Movie.Title Is Nothing OrElse (Movie.Title IsNot Nothing AndAlso String.IsNullOrEmpty(Movie.Title)) Then
                    If _MySettings.FallBackEng AndAlso MovieE.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.Title) Then
                        nMovie.Title = MovieE.Title
                    End If
                Else
                    nMovie.Title = Movie.Title
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Trailer
            If Options.bTrailer Then
                Dim aTrailers As TMDbLib.Objects.Movies.Trailers = Nothing
                If Movie.Trailers Is Nothing OrElse (Movie.Trailers IsNot Nothing AndAlso Movie.Trailers.Youtube.Count = 0) Then
                    If _MySettings.FallBackEng AndAlso MovieE.Trailers IsNot Nothing AndAlso MovieE.Trailers.Youtube.Count > 0 Then
                        aTrailers = MovieE.Trailers
                    End If
                Else
                    aTrailers = Movie.Trailers
                End If

                If aTrailers IsNot Nothing AndAlso aTrailers.Youtube IsNot Nothing AndAlso aTrailers.Youtube.Count > 0 Then
                    nMovie.Trailer = "http://www.youtube.com/watch?hd=1&v=" & aTrailers.Youtube(0).Source
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Votes
            If Options.bVotes Then
                nMovie.Votes = CStr(Movie.VoteCount)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Year
            If Options.bYear Then
                If Movie.ReleaseDate Is Nothing OrElse (Movie.ReleaseDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Movie.ReleaseDate))) Then
                    If _MySettings.FallBackEng AndAlso MovieE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(MovieE.ReleaseDate)) Then
                        nMovie.Year = CStr(MovieE.ReleaseDate.Value.Year)
                    End If
                Else
                    nMovie.Year = CStr(Movie.ReleaseDate.Value.Year)
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            Return True
        End Function

        Public Function GetMovieSetInfo(ByVal strID As String, ByRef DBMovieSet As MediaContainers.MovieSet, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_MovieSet, ByVal IsSearch As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse Not Integer.TryParse(strID, 0) Then Return False

            Dim MovieSet As TMDbLib.Objects.Collections.Collection
            Dim MovieSetE As TMDbLib.Objects.Collections.Collection

            MovieSet = _TMDBApi.GetCollection(CInt(strID), _MySettings.PrefLanguage)
            If _MySettings.FallBackEng Then
                MovieSetE = _TMDBApiE.GetCollection(CInt(strID))
            Else
                MovieSetE = MovieSet
            End If

            If (Not MovieSet.Id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not MovieSet.Id > 0 AndAlso Not MovieSetE.Id > 0) Then
                Return False
            End If

            'nMovieSet.ID = CStr(MovieSet.Id)
            DBMovieSet.ID = CStr(MovieSet.Id)

            If bwTMDB.CancellationPending Or MovieSet Is Nothing Then Return Nothing

            'Plot
            If Options.bPlot Then
                If MovieSet.Overview Is Nothing OrElse (MovieSet.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(MovieSet.Overview)) Then
                    If _MySettings.FallBackEng AndAlso MovieSetE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieSetE.Overview) Then
                        'nMovieSet.Plot = MovieSetE.Overview
                        DBMovieSet.Plot = MovieSetE.Overview
                    End If
                Else
                    'nMovieSet.Plot = MovieSet.Overview
                    DBMovieSet.Plot = MovieSet.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                Dim Images As TMDbLib.Objects.General.Images
                Images = _TMDBApi.GetCollectionImages(MovieSet.Id, _MySettings.PrefLanguage)
                If Images IsNot Nothing AndAlso Images.Posters IsNot Nothing Then
                    If Images.Posters.Count > 0 Then
                        _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                    Else
                        _sPoster = String.Empty
                    End If
                End If
            End If

            'Title
            If Options.bTitle Then
                If MovieSet.Name Is Nothing OrElse (MovieSet.Name IsNot Nothing AndAlso String.IsNullOrEmpty(MovieSet.Name)) Then
                    If _MySettings.FallBackEng AndAlso MovieSetE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieSetE.Name) Then
                        'nMovieSet.Title = MovieSetE.Name
                        DBMovieSet.Title = MovieSetE.Name
                    End If
                Else
                    'nMovieSet.Title = MovieSet.Name
                    DBMovieSet.Title = MovieSet.Name
                End If
            End If

            'we need to move that in a separate class like we have done for movie infos
            If Not String.IsNullOrEmpty(DBMovieSet.Title) Then
                For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
                    DBMovieSet.Title = DBMovieSet.Title.Replace(sett.Name.Substring(21), sett.Value)
                Next
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            Return True
        End Function
        ''' <summary>
        '''  Scrape MovieDetails from TMDB
        ''' </summary>
        ''' <param name="strID">TMDBID or ID (IMDB ID starts with "tt") of movie to be scraped</param>
        ''' <param name="nMovie">Container of scraped movie data</param>
        ''' <param name="FullCrew">Module setting: Scrape full cast?</param>
        ''' <param name="GetPoster">Scrape posters for the movie?</param>
        ''' <param name="Options">Module settings<param>
        ''' <param name="IsSearch">Not used at moment</param>
        ''' <returns>True: success, false: no success</returns>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef nShow As MediaContainers.TVShow, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_TV, ByVal IsSearch As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            Dim Show As TMDbLib.Objects.TvShows.TvShow
            Dim ShowE As TMDbLib.Objects.TvShows.TvShow

            'clear nMovie from search results
            nShow.Clear()

            If bwTMDB.CancellationPending Then Return Nothing

            'If strID.Substring(0, 2).ToLower = "tt" Then
            '    'search movie by IMDB ID
            '    Dim tmdbID As Integer = _TMDBApi.Find(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbID).TvResults.Item(0).Id.ToString
            '    Show = _TMDBApi.GetTvShow(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
            '    If _MySettings.FallBackEng Then
            '        ShowE = _TMDBApiE.GetMovie(strID, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Trailers)
            '    Else
            '        ShowE = Show
            '    End If
            'Else
            'search movie by TMDB ID
            Show = _TMDBApi.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds)
            If _MySettings.FallBackEng Then
                ShowE = _TMDBApiE.GetTvShow(CInt(strID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds)
            Else
                ShowE = Show
            End If
            'End If

            If (Show Is Nothing AndAlso Not _MySettings.FallBackEng) OrElse (Show Is Nothing AndAlso ShowE Is Nothing) OrElse _
                (Not Show.Id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not Show.Id > 0 AndAlso Not ShowE.Id > 0) Then
                logger.Error(String.Concat("Can't scrape or movie not found: ", strID))
                Return False
            End If

            nShow.Scrapersource = "TMDB"
            nShow.ID = CStr(Show.ExternalIds.TvdbId)
            nShow.IMDB = CStr(Show.ExternalIds.ImdbId)
            nShow.TMDB = CStr(Show.Id)

            If bwTMDB.CancellationPending Or Show Is Nothing Then Return Nothing

            'Cast (Actors)
            If Options.bShowActors Then
                If Show.Credits IsNot Nothing AndAlso Show.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.TvShows.Cast In Show.Credits.Cast
                        nShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
                                                                           .Role = aCast.Character, _
                                                                           .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty), _
                                                                           .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Certifications
            If Options.bShowCert Then
                If Show.ContentRatings IsNot Nothing AndAlso Show.ContentRatings.Results IsNot Nothing AndAlso Show.ContentRatings.Results.Count > 0 Then
                    For Each cCountry In Show.ContentRatings.Results
                        If Not String.IsNullOrEmpty(cCountry.Rating) Then
                            Try
                                Dim tCountry As String = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = cCountry.Iso_3166_1.ToLower).name
                                nShow.Certifications.Add(String.Concat(tCountry, ":", cCountry.Rating))
                            Catch ex As Exception
                                logger.Warn("Unhandled certification language encountered: {0}", cCountry.Iso_3166_1.ToLower)
                            End Try
                        End If
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            ''Countries
            'If Options.bShowCountry Then
            '    If Show.ProductionCountries IsNot Nothing AndAlso Show.ProductionCountries.Count > 0 Then
            '        For Each aContry As TMDbLib.Objects.Movies.ProductionCountry In Show.ProductionCountries
            '            nShow.Countries.Add(aContry.Name)
            '        Next
            '    End If
            'End If

            If bwTMDB.CancellationPending Then Return Nothing

            ''Director / Writer
            'If Options.bDirector OrElse Options.bWriters OrElse Options.bFullCrew Then
            '    If Show.Credits IsNot Nothing AndAlso Show.Credits.Crew IsNot Nothing Then
            '        For Each aCrew As TMDbLib.Objects.General.Crew In Show.Credits.Crew
            '            If Options.bDirector AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
            '                nShow.Directors.Add(aCrew.Name)
            '            End If
            '            If Options.bWriters AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
            '                nShow.Credits.Add(aCrew.Name)
            '            End If
            '        Next
            '    End If
            'End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Genres
            If Options.bShowGenre Then
                Dim aGenres As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre) = Nothing
                If Show.Genres Is Nothing OrElse (Show.Genres IsNot Nothing AndAlso Show.Genres.Count = 0) Then
                    If _MySettings.FallBackEng AndAlso ShowE.Genres IsNot Nothing AndAlso ShowE.Genres.Count > 0 Then
                        aGenres = ShowE.Genres
                    End If
                Else
                    aGenres = Show.Genres
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
                        nShow.Genres.Add(tGenre.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'OriginalTitle
            If Options.bShowOriginalTitle Then
                If Show.OriginalName Is Nothing OrElse (Show.OriginalName IsNot Nothing AndAlso String.IsNullOrEmpty(Show.OriginalName)) Then
                    If _MySettings.FallBackEng AndAlso ShowE.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.OriginalName) Then
                        nShow.OriginalTitle = ShowE.OriginalName
                    End If
                Else
                    nShow.OriginalTitle = ShowE.OriginalName
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Plot
            If Options.bShowPlot Then
                If Show.Overview Is Nothing OrElse (Show.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Show.Overview)) Then
                    If _MySettings.FallBackEng AndAlso ShowE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.Overview) Then
                        nShow.Plot = ShowE.Overview
                    End If
                Else
                    nShow.Plot = Show.Overview
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
            If GetPoster Then
                Dim Images As TMDbLib.Objects.General.Images
                Images = _TMDBApi.GetTvShowImages(Show.Id)
                If Images IsNot Nothing AndAlso Images.Posters IsNot Nothing Then
                    If Images.Posters.Count > 0 Then
                        _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                    Else
                        _sPoster = String.Empty
                    End If
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Premiered
            If Options.bShowPremiered Then
                Dim ScrapedDate As String = String.Empty
                If Show.FirstAirDate Is Nothing OrElse (Show.FirstAirDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Show.FirstAirDate))) Then
                    If _MySettings.FallBackEng AndAlso ShowE.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ShowE.FirstAirDate)) Then
                        ScrapedDate = CStr(ShowE.FirstAirDate)
                    End If
                Else
                    ScrapedDate = CStr(Show.FirstAirDate)
                End If
                If Not String.IsNullOrEmpty(ScrapedDate) Then
                    Dim RelDate As Date
                    If Date.TryParse(ScrapedDate, RelDate) Then
                        'always save date in same date format not depending on users language setting!
                        nShow.Premiered = RelDate.ToString("yyyy-MM-dd")
                    Else
                        nShow.Premiered = ScrapedDate
                    End If
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Rating
            If Options.bShowRating Then
                nShow.Rating = CStr(Show.VoteAverage)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Runtime
            If Options.bShowRuntime Then
                If Show.EpisodeRunTime Is Nothing OrElse Show.EpisodeRunTime.Count = 0 Then
                    If _MySettings.FallBackEng AndAlso ShowE.EpisodeRunTime IsNot Nothing Then
                        nShow.Runtime = CStr(ShowE.EpisodeRunTime.Item(0))
                    End If
                Else
                    nShow.Runtime = CStr(Show.EpisodeRunTime.Item(0))
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Studios
            If Options.bShowStudio Then
                If Show.Networks IsNot Nothing AndAlso Show.Networks.Count > 0 Then
                    For Each cStudio In Show.Networks
                        nShow.Studios.Add(cStudio.Name)
                    Next
                End If
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            'Title
            If Options.bShowTitle Then
                If Show.Name Is Nothing OrElse (Show.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Show.Name)) Then
                    If _MySettings.FallBackEng AndAlso ShowE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ShowE.Name) Then
                        nShow.Title = ShowE.Name
                    End If
                Else
                    nShow.Title = Show.Name
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

            'Votes
            If Options.bShowVotes Then
                nShow.Votes = CStr(Show.VoteCount)
            End If

            If bwTMDB.CancellationPending Then Return Nothing

            If bwTMDB.CancellationPending Then Return Nothing

            Return True
        End Function

        Public Function GetTMDBbyTVDB(ByRef tvdbID As String) As String
            Dim tmdbID As String = String.Empty

            Try
                tmdbID = _TMDBApi.Find(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbID).TvResults.Item(0).Id.ToString

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return tmdbID
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            If String.IsNullOrEmpty(strID) OrElse strID.Length > 2 Then Return New List(Of String)

            Dim alStudio As New List(Of String)
            Dim Movie As TMDbLib.Objects.Movies.Movie

            If strID.Substring(0, 2).ToLower = "tt" Then
                Movie = _TMDBApi.GetMovie(strID)
            Else
                Movie = _TMDBApi.GetMovie(CInt(strID))
            End If

            If Movie IsNot Nothing AndAlso Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
                For Each cStudio In Movie.ProductionCompanies
                    alStudio.Add(cStudio.Name)
                Next
            End If

            Return alStudio
        End Function

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByVal iType As Enums.ScrapeType_Movie_MovieSet_TV, ByVal Options As Structures.ScrapeOptions_Movie) As MediaContainers.Movie
            Dim r As SearchResults_Movie = SearchMovie(sMovieName, CInt(If(Not String.IsNullOrEmpty(oDBMovie.Movie.Year), oDBMovie.Movie.Year, Nothing)))
            Dim b As Boolean = False

            Select Case iType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAsk, Enums.ScrapeType_Movie_MovieSet_TV.MissAsk, Enums.ScrapeType_Movie_MovieSet_TV.NewAsk, Enums.ScrapeType_Movie_MovieSet_TV.MarkAsk, Enums.ScrapeType_Movie_MovieSet_TV.FilterAsk, Enums.ScrapeType_Movie_MovieSet_TV.SingleField
                    If r.Matches.Count = 1 Then
                        b = GetMovieInfo(r.Matches.Item(0).TMDBID, nMovie, True, False, Options, True)
                    Else
                        nMovie.Clear()
                        Using dTMDB As New dlgTMDBSearchResults_Movie(_MySettings, Me)
                            If dTMDB.ShowDialog(nMovie, r, sMovieName, oDBMovie.Filename) = Windows.Forms.DialogResult.OK Then
                                If String.IsNullOrEmpty(nMovie.TMDBID) Then
                                    b = False
                                Else
                                    b = GetMovieInfo(nMovie.TMDBID, nMovie, True, False, Options, True)
                                End If
                            Else
                                b = False
                            End If
                        End Using
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterSkip, Enums.ScrapeType_Movie_MovieSet_TV.FullSkip, Enums.ScrapeType_Movie_MovieSet_TV.MarkSkip, Enums.ScrapeType_Movie_MovieSet_TV.NewSkip, Enums.ScrapeType_Movie_MovieSet_TV.MissSkip
                    If r.Matches.Count = 1 Then
                        b = GetMovieInfo(r.Matches.Item(0).TMDBID, nMovie, True, False, Options, True)
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape, Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto
                    Dim exactHaveYear As Integer = FindYear(oDBMovie.Filename, r.Matches)
                    If r.Matches.Count = 1 Then
                        b = GetMovieInfo(r.Matches.Item(0).TMDBID, nMovie, True, False, Options, True)
                    ElseIf r.Matches.Count > 1 Then
                        b = GetMovieInfo(r.Matches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).TMDBID, nMovie, True, False, Options, True)
                    End If
            End Select

            Return nMovie
        End Function

        Public Function GetSearchMovieSetInfo(ByVal sMovieSetName As String, ByRef DBMovieSet As Structures.DBMovieSet, ByVal iType As Enums.ScrapeType_Movie_MovieSet_TV, ByVal Options As Structures.ScrapeOptions_MovieSet) As MediaContainers.MovieSet
            Dim r As SearchResults_MovieSet = SearchMovieSet(sMovieSetName, Nothing)
            Dim b As Boolean = False
            Dim tmdbMovieSet As MediaContainers.MovieSet = DBMovieSet.MovieSet

            Select Case iType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAsk, Enums.ScrapeType_Movie_MovieSet_TV.MissAsk, Enums.ScrapeType_Movie_MovieSet_TV.NewAsk, Enums.ScrapeType_Movie_MovieSet_TV.MarkAsk, Enums.ScrapeType_Movie_MovieSet_TV.FilterAsk

                    If r.Matches.Count = 1 Then
                        b = GetMovieSetInfo(r.Matches.Item(0).ID, tmdbMovieSet, False, Options, True)
                    Else
                        Master.tmpMovieSet.Clear()
                        Using dTMDB As New dlgTMDBSearchResults_MovieSet(_MySettings, Me)
                            If dTMDB.ShowDialog(r, sMovieSetName) = Windows.Forms.DialogResult.OK Then
                                If String.IsNullOrEmpty(Master.tmpMovieSet.ID) Then
                                    b = False
                                Else
                                    b = GetMovieSetInfo(Master.tmpMovieSet.ID, tmdbMovieSet, False, Options, True)
                                End If
                            Else
                                b = False
                            End If
                        End Using
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterSkip, Enums.ScrapeType_Movie_MovieSet_TV.FullSkip, Enums.ScrapeType_Movie_MovieSet_TV.MarkSkip, Enums.ScrapeType_Movie_MovieSet_TV.NewSkip, Enums.ScrapeType_Movie_MovieSet_TV.MissSkip
                    If r.Matches.Count = 1 Then
                        b = GetMovieSetInfo(r.Matches.Item(0).ID, tmdbMovieSet, False, Options, True)
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape, Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto
                    If r.Matches.Count >= 1 Then
                        b = GetMovieSetInfo(r.Matches.Item(0).ID, tmdbMovieSet, False, Options, True)
                    End If
            End Select

            If b Then
                Return tmdbMovieSet
            Else
                Return tmdbMovieSet
            End If
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

        Public Sub GetSearchMovieInfoAsync(ByVal tmdbID As String, ByVal IMDBMovie As MediaContainers.Movie, ByVal Options As Structures.ScrapeOptions_Movie)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails, _
                  .Parameter = tmdbID, .Movie = IMDBMovie, .Options_Movie = Options})
            End If
        End Sub

        Public Sub GetSearchMovieSetInfoAsync(ByVal tmdbColID As String, ByVal IMDBMovieSet As MediaContainers.MovieSet, ByVal Options As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_MovieSet, _
                  .Parameter = tmdbColID, .MovieSet = IMDBMovieSet, .Options_movieset = Options})
            End If
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As Structures.ScrapeOptions_Movie, Optional ByVal sYear As String = "")
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Dim tYear As Integer = 0

            If Not String.IsNullOrEmpty(sYear) AndAlso Integer.TryParse(sYear, 0) Then
                tYear = CInt(sYear)
            End If

            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies, _
                  .Parameter = sMovie, .Options_Movie = filterOptions, .Year = tYear})
            End If
        End Sub

        Public Sub SearchMovieSetAsync(ByVal sMovieSet As String, ByVal filterOptions As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            If Not bwTMDB.IsBusy Then
                bwTMDB.WorkerReportsProgress = False
                bwTMDB.WorkerSupportsCancellation = True
                bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.MovieSets, _
                  .Parameter = sMovieSet, .Options_MovieSet = filterOptions})
            End If
        End Sub

        Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB

            Select Case Args.Search
                Case SearchType.Movies
                    Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
                    e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

                Case SearchType.SearchDetails
                    Dim s As Boolean = GetMovieInfo(Args.Parameter, Args.Movie, False, True, Args.Options_Movie, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails, .Success = s}

                Case SearchType.MovieSets
                    Dim r As SearchResults_MovieSet = SearchMovieSet(Args.Parameter)
                    e.Result = New Results With {.ResultType = SearchType.MovieSets, .Result = r}

                Case SearchType.SearchDetails_MovieSet
                    Dim s As Boolean = GetMovieSetInfo(Args.Parameter, Args.MovieSet, True, Args.Options_MovieSet, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_MovieSet, .Success = s}
            End Select
        End Sub

        Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Select Case Res.ResultType
                Case SearchType.Movies
                    RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

                Case SearchType.SearchDetails
                    Dim movieInfo As SearchResults_Movie = DirectCast(Res.Result, SearchResults_Movie)
                    RaiseEvent SearchInfoDownloaded_Movie(_sPoster, Res.Success)

                Case SearchType.MovieSets
                    RaiseEvent SearchResultsDownloaded_MovieSet(DirectCast(Res.Result, SearchResults_MovieSet))

                Case SearchType.SearchDetails_MovieSet
                    Dim moviesetInfo As SearchResults_MovieSet = DirectCast(Res.Result, SearchResults_MovieSet)
                    RaiseEvent SearchInfoDownloaded_MovieSet(_sPoster, Res.Success)
            End Select
        End Sub

        Private Function SearchMovie(ByVal sMovie As String, Optional ByVal sYear As Integer = 0) As SearchResults_Movie
            If String.IsNullOrEmpty(sMovie) Then Return New SearchResults_Movie

            Dim R As New SearchResults_Movie
            Dim Page As Integer = 1
            Dim Movies As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
            Dim TotP As Integer
            Dim aE As Boolean

            Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)

            If Movies.TotalResults = 0 AndAlso _MySettings.FallBackEng Then
                Movies = _TMDBApiE.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)
                aE = True
            End If

            If Movies.TotalResults > 0 Then
                Dim t1 As String = String.Empty
                Dim t2 As String = String.Empty
                TotP = Movies.TotalPages
                While Page <= TotP AndAlso Page <= 3
                    If Movies.Results IsNot Nothing Then
                        For Each aMovie In Movies.Results
                            If aMovie.Title Is Nothing OrElse (aMovie.Title IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(aMovie.Title))) Then
                                If aMovie.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.OriginalTitle)) Then
                                    t1 = CStr(aMovie.OriginalTitle)
                                End If
                            Else
                                t1 = CStr(aMovie.Title)
                            End If
                            If aMovie.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.ReleaseDate)) Then
                                t2 = CStr(aMovie.ReleaseDate.Value.Year)
                            End If
                            Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(String.Empty, t1, t2, 0)
                            lNewMovie.TMDBID = CStr(aMovie.Id)
                            R.Matches.Add(lNewMovie)
                        Next
                    End If
                    Page = Page + 1
                    If aE Then
                        Movies = _TMDBApiE.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)
                    Else
                        Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)
                    End If
                End While
            End If

            Return R
        End Function

        Private Function SearchMovieSet(ByVal sMovieSet As String, Optional ByVal sYear As Integer = 0) As SearchResults_MovieSet
            If String.IsNullOrEmpty(sMovieSet) Then Return New SearchResults_MovieSet

            Dim R As New SearchResults_MovieSet
            Dim Page As Integer = 1
            Dim MovieSets As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchResultCollection)
            Dim TotP As Integer
            Dim aE As Boolean

            MovieSets = _TMDBApi.SearchCollection(sMovieSet, Page)

            If MovieSets.TotalResults = 0 AndAlso _MySettings.FallBackEng Then
                MovieSets = _TMDBApiE.SearchCollection(sMovieSet, Page)
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
                        MovieSets = _TMDBApiE.SearchCollection(sMovieSet, Page)
                    Else
                        MovieSets = _TMDBApi.SearchCollection(sMovieSet, Page)
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
            Dim Options_Movie As Structures.ScrapeOptions_Movie
            Dim Options_MovieSet As Structures.ScrapeOptions_MovieSet
            Dim Parameter As String
            Dim Search As SearchType
            Dim Year As Integer
            'Dim TMDBConf As V3.TmdbConfiguration
            'Dim TMDBApi As V3.Tmdb
            'Dim FallBackEng As Boolean
            'Dim TMDBLang As String

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultType As SearchType
            Dim Success As Boolean

#End Region 'Fields

        End Structure

        Structure sMySettings_ForScraper

#Region "Fields"

            Dim ApiKey As String
            Dim FallBackEng As Boolean
            Dim GetAdultItems As Boolean
            Dim PrefLanguage As String

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

