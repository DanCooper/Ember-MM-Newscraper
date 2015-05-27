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

Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports EmberAPI.MediaContainers
Imports NLog
Imports System.Diagnostics

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

        Public Event Exception(ByVal ex As Exception)

        Public Event SearchInfoDownloaded_Movie(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchInfoDownloaded_MovieSet(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchResultsDownloaded_Movie(ByVal mResults As TMDB.SearchResults_Movie)

        Public Event SearchResultsDownloaded_MovieSet(ByVal mResults As TMDB.SearchResults_MovieSet)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal Settings As sMySettings_ForScraper)
            Try
                _TMDBApi = New TMDbLib.Client.TMDbClient(Settings.ApiKey)
                _TMDBApi.GetConfig()
                _TMDBApi.DefaultLanguage = Settings.PrefLanguage

                _TMDBApiE = New TMDbLib.Client.TMDbClient(Settings.ApiKey)
                _TMDBApiE.GetConfig()
                _TMDBApiE.DefaultLanguage = "en"

                _MySettings = Settings
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
            Try
                Dim Movie As TMDbLib.Objects.Movies.Movie
                Dim MovieE As TMDbLib.Objects.Movies.Movie

                If bwTMDB.CancellationPending Then Return

                Movie = _TMDBApi.GetMovie(DBMovie.Movie.ID) ', _MySettings.PrefLanguage)
                MovieE = _TMDBApiE.GetMovie(DBMovie.Movie.ID)
                If Movie Is Nothing AndAlso Not _MySettings.FallBackEng Then
                    Return
                End If

                DBMovie.Movie.TMDBID = CStr(If(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Function GetMovieID(ByRef IMDBID As String) As String
            Try
                Dim Movie As TMDbLib.Objects.Movies.Movie
                Dim MovieE As TMDbLib.Objects.Movies.Movie

                If bwTMDB.CancellationPending Then Return String.Empty

                Movie = _TMDBApi.GetMovie(IMDBID) ', _MySettings.PrefLanguage)
                MovieE = _TMDBApiE.GetMovie(IMDBID)
                If Movie Is Nothing AndAlso Not _MySettings.FallBackEng Then
                    Return String.Empty
                End If

                Return CStr(If(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return String.Empty
            End Try
        End Function

        Public Function GetMovieCollectionID(ByVal IMDBID As String) As String
            Try
                Dim Movie As TMDbLib.Objects.Movies.Movie
                If bwTMDB.CancellationPending Then Return ""

                Movie = _TMDBApiE.GetMovie(IMDBID)
                If Movie Is Nothing Then
                    Return String.Empty
                End If

                If Movie.BelongsToCollection IsNot Nothing AndAlso Not String.IsNullOrEmpty(Movie.BelongsToCollection.Item(0).Id.ToString) Then
                    Return CStr(Movie.BelongsToCollection.Item(0).Id)
                Else
                    Return String.Empty
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return String.Empty
            End Try
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
            Try
                Dim Movie As TMDbLib.Objects.Movies.Movie
                Dim MovieE As TMDbLib.Objects.Movies.Movie
                Dim scrapedresult As String = ""

                'clear nMovie from search results
                nMovie.Clear()

                If bwTMDB.CancellationPending Then Return Nothing

                If strID.Substring(0, 2).ToLower = "tt" Then
                    Movie = _TMDBApi.GetMovie(strID)
                    MovieE = _TMDBApiE.GetMovie(strID)
                Else
                    Movie = _TMDBApi.GetMovie(CInt(strID))
                    MovieE = _TMDBApiE.GetMovie(CInt(strID))
                End If

                If (Not Movie.id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not Movie.id > 0 AndAlso Not MovieE.id > 0) Then
                    Return False
                End If

                nMovie.Scrapersource = "TMDB"
                nMovie.ID = CStr(If(String.IsNullOrEmpty(Movie.ImdbId) AndAlso _MySettings.FallBackEng, MovieE.ImdbId, Movie.ImdbId))
                nMovie.TMDBID = CStr(If(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

                If bwTMDB.CancellationPending Or Movie Is Nothing Then Return Nothing

                Dim Keywords As TMDbLib.Objects.Movies.KeywordsContainer
                Keywords = _TMDBApi.GetMovieKeywords(Movie.id)
                If Keywords IsNot Nothing AndAlso Keywords.keywords IsNot Nothing Then
                    If Keywords.keywords.Count <> 0 AndAlso _MySettings.FallBackEng Then
                        Keywords = _TMDBApiE.GetMovieKeywords(Movie.id)
                    End If
                Else
                    If _MySettings.FallBackEng Then
                        Keywords = _TMDBApiE.GetMovieKeywords(Movie.id)
                    End If
                End If

                ' to be added the tags structure
                '' <movie>
                '' ...
                '' <tag>Name of the tag</tag>
                '' ...
                '' </movie>

                'Title
                If Options.bTitle Then
                    If Movie.title Is Nothing OrElse (Movie.title IsNot Nothing And String.IsNullOrEmpty(Movie.title)) Then
                        If _MySettings.FallBackEng AndAlso MovieE.title IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.title) Then
                            nMovie.Title = CStr(MovieE.title)
                        End If
                    Else
                        nMovie.Title = CStr(Movie.title)
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'OriginalTitle
                If Options.bOriginalTitle Then
                    If Movie.OriginalTitle Is Nothing OrElse (Movie.OriginalTitle IsNot Nothing And String.IsNullOrEmpty(Movie.OriginalTitle)) Then
                        If _MySettings.FallBackEng AndAlso MovieE.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(MovieE.OriginalTitle) Then
                            nMovie.OriginalTitle = CStr(MovieE.OriginalTitle)
                        End If
                    Else
                        nMovie.OriginalTitle = CStr(Movie.OriginalTitle)
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Collection ID
                If Options.bCollectionID Then
                    'Get collection information
                    If Movie.BelongsToCollection Is Nothing Then
                        If _MySettings.FallBackEng Then
                            If MovieE.BelongsToCollection IsNot Nothing Then
                                nMovie.AddSet(Nothing, MovieE.BelongsToCollection.Item(0).Name, Nothing, MovieE.BelongsToCollection.Item(0).Id.ToString)
                                nMovie.TMDBColID = MovieE.BelongsToCollection.Item(0).Id.ToString
                            End If
                        End If
                    Else
                        nMovie.AddSet(Nothing, Movie.BelongsToCollection.Item(0).Name, Nothing, MovieE.BelongsToCollection.Item(0).Id.ToString)
                        nMovie.TMDBColID = Movie.BelongsToCollection.Item(0).Id.ToString
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Posters (only for SearchResult dialog)
                If GetPoster Then
                    Dim Images As TMDbLib.Objects.General.Images
                    Images = _TMDBApi.GetMovieImages(Movie.id)
                    If Images IsNot Nothing AndAlso Images.posters IsNot Nothing Then
                        If Images.posters.Count = 0 Then
                            Images = _TMDBApiE.GetMovieImages(Movie.id)
                        End If
                    Else
                        Images = _TMDBApiE.GetMovieImages(Movie.id)
                    End If
                    If Images IsNot Nothing AndAlso Images.posters IsNot Nothing Then
                        If Images.posters.Count > 0 Then
                            _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                        Else
                            _sPoster = ""
                        End If
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Year
                If Options.bYear Then
                    If Movie.ReleaseDate Is Nothing OrElse (Movie.ReleaseDate IsNot Nothing And String.IsNullOrEmpty(CStr(Movie.ReleaseDate))) Then
                        If _MySettings.FallBackEng AndAlso MovieE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(MovieE.ReleaseDate)) Then
                            nMovie.Year = CStr(MovieE.ReleaseDate.Value.Year)
                        End If
                    Else
                        nMovie.Year = CStr(Movie.ReleaseDate.Value.Year)
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                Dim Releases As TMDbLib.Objects.Movies.Releases = Nothing

                'Certification
                If Options.bCert Then
                    Releases = _TMDBApi.GetMovieReleases(Movie.id)
                    If Releases IsNot Nothing AndAlso Releases.countries IsNot Nothing Then
                        If (Releases.countries.Count = 0) AndAlso _MySettings.FallBackEng Then
                            Releases = _TMDBApiE.GetMovieReleases(Movie.id)
                        End If
                    Else
                        If _MySettings.FallBackEng Then
                            Releases = _TMDBApiE.GetMovieReleases(Movie.id)
                        End If
                    End If

                    If Releases IsNot Nothing AndAlso Releases.countries IsNot Nothing Then
                        'only update nMovie if scraped result is not empty/nothing!
                        If Releases.countries.Count > 0 Then
                            For Each Country In Releases.countries
                                If Not String.IsNullOrEmpty(Country.certification) Then
                                    Dim tCountry As String = String.Empty
                                    Try
                                        tCountry = APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Country.iso_3166_1.ToLower).name
                                        nMovie.Certifications.Add(String.Concat(tCountry, ":", Country.certification))
                                    Catch ex As Exception
                                        logger.Warn("Unhandled certification language encountered: {0}", Country.iso_3166_1.ToLower)
                                    End Try
                                End If
                            Next
                        End If
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'ReleaseDate
                If Options.bRelease Then
                    Dim ScrapedDate As String = String.Empty
                    If Movie.ReleaseDate Is Nothing OrElse (Movie.ReleaseDate IsNot Nothing And String.IsNullOrEmpty(CStr(Movie.ReleaseDate))) Then
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
                            nMovie.ReleaseDate = scrapedresult
                        End If
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Rating
                If Options.bRating Then
                    If Movie.VoteAverage = 0 Then
                        If _MySettings.FallBackEng Then
                            nMovie.Rating = CStr(MovieE.VoteAverage)
                        End If
                    Else
                        nMovie.Rating = CStr(Movie.VoteAverage)
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Trailer
                If Options.bTrailer Then
                    Dim Trailers As TMDbLib.Objects.Movies.Trailers
                    Trailers = _TMDBApi.GetMovieTrailers(Movie.id)
                    If Trailers IsNot Nothing AndAlso Trailers.youtube IsNot Nothing Then
                        If (Trailers.youtube.Count = 0) AndAlso _MySettings.FallBackEng Then
                            Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
                        End If
                    Else
                        If _MySettings.FallBackEng Then
                            Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
                        End If
                    End If

                    If Trailers IsNot Nothing AndAlso Trailers.youtube IsNot Nothing Then
                        If Trailers.youtube.Count > 0 Then
                            nMovie.Trailer = "http://www.youtube.com/watch?hd=1&v=" & Trailers.youtube(0).source
                        End If
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Votes
                If Options.bVotes Then
                    If Movie.VoteCount = 0 Then
                        If _MySettings.FallBackEng Then
                            nMovie.Votes = CStr(MovieE.VoteCount)
                        End If
                    Else
                        nMovie.Votes = CStr(Movie.VoteCount)
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Dim aCast As TMDbLib.Objects.Movies.Cast = Nothing

                ''Actors
                'If Options.bCast Then
                '    aCast = _TMDBApi.GetMovieList(Movie.Id)
                '    If aCast IsNot Nothing AndAlso aCast.cast IsNot Nothing Then
                '        If (aCast.cast.Count = 0) AndAlso _MySettings.FallBackEng Then
                '            aCast = _TMDBApiE.GetMovieCast(Movie.id)
                '        End If
                '    Else
                '        If _MySettings.FallBackEng Then
                '            aCast = _TMDBApiE.GetMovieCast(Movie.id)
                '        End If
                '    End If

                '    Dim Cast As New List(Of MediaContainers.Person)
                '    If aCast IsNot Nothing AndAlso aCast.cast IsNot Nothing Then
                '        For Each aAc As WatTmdb.V3.Cast In aCast.cast
                '            Dim aPer As New MediaContainers.Person
                '            aPer.Name = aAc.name
                '            aPer.Role = aAc.character
                '            ' to be added / dialog to choose the size of the images
                '            If Not String.IsNullOrEmpty(aAc.profile_path) Then
                '                aPer.ThumbURL = _TMDBConf.images.base_url & "original" & aAc.profile_path
                '            End If
                '            Cast.Add(aPer)
                '        Next
                '    End If
                '    'only update nMovie if scraped result is not empty/nothing!
                '    If Cast.Count > 0 Then
                '        nMovie.Actors = Cast
                '    End If
                'End If

                'If bwTMDB.CancellationPending Then Return Nothing

                ''Use TMDB other infos?
                'If FullCrew Or Options.bWriters Or Options.bDirector Then
                '    'Get All Other Info
                '    If aCast Is Nothing Then
                '        aCast = _TMDBApi.GetMovieCast(Movie.id)
                '        If aCast IsNot Nothing AndAlso aCast.cast IsNot Nothing Then
                '            If (aCast.crew.Count = 0) AndAlso _MySettings.FallBackEng Then
                '                aCast = _TMDBApiE.GetMovieCast(Movie.id)
                '            End If
                '        Else
                '            If _MySettings.FallBackEng Then
                '                aCast = _TMDBApiE.GetMovieCast(Movie.id)
                '            End If
                '        End If
                '    End If

                '    If aCast.crew IsNot Nothing Then
                '        For Each aAc As WatTmdb.V3.Crew In aCast.crew
                '            If Options.bWriters Then
                '                If aAc.department = "Writing" AndAlso (aAc.job = "Author" OrElse aAc.job = "Screenplay" OrElse aAc.job = "Writer") Then
                '                    nMovie.Credits.Add(aAc.name)
                '                End If
                '            End If
                '            If Options.bDirector Then
                '                If aAc.department = "Directing" AndAlso aAc.job = "Director" Then
                '                    nMovie.Directors.Add(aAc.name)
                '                End If
                '            End If
                '        Next
                '    End If
                'End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Tagline
                If Options.bTagline Then
                    If String.IsNullOrEmpty(Movie.Tagline) Then
                        If _MySettings.FallBackEng Then
                            If Not String.IsNullOrEmpty(MovieE.Tagline) Then
                                nMovie.Tagline = MovieE.Tagline
                            End If
                        End If
                    Else
                        nMovie.Tagline = Movie.Tagline
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Countries
                If Options.bCountry Then
                    nMovie.Countries.Clear()
                    If Movie.ProductionCountries IsNot Nothing AndAlso Movie.ProductionCountries.Count > 0 Then
                        For Each aCo As TMDbLib.Objects.Movies.ProductionCountry In Movie.ProductionCountries
                            nMovie.Countries.Add(aCo.Name) 'XBMC use full names
                        Next
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Genres
                If Options.bGenre Then
                    nMovie.Genres.Clear()
                    Dim tGen As System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre)
                    If Movie IsNot Nothing AndAlso Movie.Genres IsNot Nothing Then
                        tGen = CType(If(Movie.Genres.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.Genres, Movie.Genres), Global.System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre))
                    Else
                        tGen = CType(If(_MySettings.FallBackEng, MovieE.Genres, Nothing), Global.System.Collections.Generic.List(Of TMDbLib.Objects.General.Genre))
                    End If

                    If tGen IsNot Nothing AndAlso tGen.Count > 0 Then
                        For Each aGen As TMDbLib.Objects.General.Genre In tGen
                            nMovie.Genres.Add(aGen.Name)
                        Next
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Plot
                If Options.bPlot Then
                    If String.IsNullOrEmpty(Movie.overview) Then
                        If _MySettings.FallBackEng Then
                            If Not String.IsNullOrEmpty(MovieE.overview) Then
                                nMovie.Plot = MovieE.overview
                            End If
                        End If
                    Else
                        nMovie.Plot = Movie.overview
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Runtime
                If Options.bRuntime Then
                    If Movie.Runtime = 0 Then
                        If _MySettings.FallBackEng Then
                            nMovie.Runtime = CStr(MovieE.Runtime)
                        End If
                    Else
                        nMovie.Runtime = CStr(Movie.Runtime)
                    End If
                End If

                'Studios
                If Options.bStudio Then
                    'Get Production Studio
                    Dim tPC As System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany)
                    If Movie IsNot Nothing AndAlso Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
                        tPC = CType(If(Movie.ProductionCompanies.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.ProductionCompanies, Movie.ProductionCompanies), Global.System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany))
                    Else
                        tPC = CType(If(_MySettings.FallBackEng, MovieE.ProductionCompanies, Nothing), Global.System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany))
                    End If

                    If tPC IsNot Nothing Then
                        For Each aPro As TMDbLib.Objects.Movies.ProductionCompany In tPC
                            If Not String.IsNullOrEmpty(aPro.Name) Then
                                nMovie.Studios.Add(aPro.Name)
                            End If
                        Next
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

        Public Function GetMovieSetInfo(ByVal strID As String, ByRef DBMovieSet As MediaContainers.MovieSet, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_MovieSet, ByVal IsSearch As Boolean) As Boolean
            Try
                Dim MovieSet As TMDbLib.Objects.Collections.Collection
                Dim MovieSetE As TMDbLib.Objects.Collections.Collection
                Dim scrapedresult As String = ""

                If bwTMDB.CancellationPending Then Return Nothing

                MovieSet = _TMDBApi.GetCollection(CInt(strID))
                MovieSetE = _TMDBApiE.GetCollection(CInt(strID))

                If (Not MovieSet.id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not MovieSet.id > 0 AndAlso Not MovieSetE.id > 0) Then
                    Return False
                End If

                DBMovieSet.ID = CStr(If(Not MovieSet.id > 0 AndAlso _MySettings.FallBackEng, MovieSetE.id.ToString, MovieSet.id.ToString))

                If bwTMDB.CancellationPending Or MovieSet Is Nothing Then Return Nothing

                'title
                If Options.bTitle Then
                    If String.IsNullOrEmpty(DBMovieSet.Title) OrElse Not Master.eSettings.MovieSetLockTitle Then
                        If String.IsNullOrEmpty(MovieSet.name) Then
                            If _MySettings.FallBackEng Then
                                If Not String.IsNullOrEmpty(MovieSetE.name) Then
                                    DBMovieSet.Title = MovieSetE.name
                                End If
                            End If
                        Else
                            DBMovieSet.Title = MovieSet.name
                        End If
                    End If
                End If

                'we need to move that in a separate class like we have done for movie infos
                If Not String.IsNullOrEmpty(DBMovieSet.Title) Then
                    For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
                        DBMovieSet.Title = DBMovieSet.Title.Replace(sett.Name.Substring(21), sett.Value)
                    Next
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Posters (only for SearchResult dialog)
                If GetPoster Then
                    ' I will add original always. to be updated if size, TMDBConf.images.poster_sizes(0) & 
                    Dim Images As TMDbLib.Objects.General.Images
                    Images = _TMDBApi.GetMovieImages(MovieSet.id)
                    If Images IsNot Nothing AndAlso Images.posters IsNot Nothing Then
                        If (Images.posters.Count = 0) Then
                            Images = _TMDBApiE.GetMovieImages(MovieSet.id)
                        End If
                    Else
                        Images = _TMDBApiE.GetMovieImages(MovieSetE.id)
                    End If
                    If Images IsNot Nothing AndAlso Images.posters IsNot Nothing Then
                        If Images.posters.Count > 0 Then
                            _sPoster = _TMDBApi.Config.Images.BaseUrl & "w92" & Images.Posters(0).FilePath
                        Else
                            _sPoster = ""
                        End If
                    End If
                End If

                If bwTMDB.CancellationPending Then Return Nothing

                'Plot
                'If Options.bPlot Then
                '    If String.IsNullOrEmpty(DBMovieSet.Plot) OrElse Not Master.eSettings.MovieSetLockPlot Then
                '        If String.IsNullOrEmpty(MovieSet.overview) Then
                '            If _MySettings.FallBackEng Then
                '                If Not String.IsNullOrEmpty(MovieSetE.overview) Then
                '                    DBMovieSet.Plot = MovieSetE.overview
                '                End If
                '            End If
                '        Else
                '            DBMovieSet.Plot = MovieSet.overview
                '        End If
                '    End If
                'End If

                If bwTMDB.CancellationPending Then Return Nothing

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            Dim alStudio As New List(Of String)
            Dim Movie As TMDbLib.Objects.Movies.Movie
            Dim MovieE As TMDbLib.Objects.Movies.Movie

            If strID.Substring(0, 2).ToLower = "tt" Then
                Movie = _TMDBApi.GetMovie(strID)
                MovieE = _TMDBApiE.GetMovie(strID)
            Else
                Movie = _TMDBApi.GetMovie(CInt(strID))
                MovieE = _TMDBApiE.GetMovie(CInt(strID))
            End If

            Dim tPC As System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany)
            If Movie IsNot Nothing AndAlso Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
                tPC = CType(If(Movie.ProductionCompanies.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.ProductionCompanies, Movie.ProductionCompanies), Global.System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany))
            Else
                tPC = CType(If(_MySettings.FallBackEng, MovieE.ProductionCompanies, Nothing), Global.System.Collections.Generic.List(Of TMDbLib.Objects.Movies.ProductionCompany))
            End If

            If tPC IsNot Nothing Then
                For Each aPro As TMDbLib.Objects.Movies.ProductionCompany In tPC
                    If Not String.IsNullOrEmpty(aPro.Name) Then
                        alStudio.Add(aPro.Name)
                    End If
                Next
            End If

            Return alStudio

        End Function

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByVal iType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As MediaContainers.Movie
            Dim r As SearchResults_Movie = SearchMovie(sMovieName, CInt(If(Not String.IsNullOrEmpty(oDBMovie.Movie.Year), oDBMovie.Movie.Year, Nothing)))
            Dim b As Boolean = False

            Try
                Select Case iType
                    Case Enums.ScrapeType.FullAsk, Enums.ScrapeType.MissAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.MarkAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.SingleField
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
                    Case Enums.ScrapeType.FilterSkip, Enums.ScrapeType.FullSkip, Enums.ScrapeType.MarkSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.MissSkip
                        If r.Matches.Count = 1 Then
                            b = GetMovieInfo(r.Matches.Item(0).TMDBID, nMovie, True, False, Options, True)
                        End If
                    Case Enums.ScrapeType.FullAuto, Enums.ScrapeType.MissAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.SingleScrape, Enums.ScrapeType.FilterAuto
                        Dim exactHaveYear As Integer = FindYear(oDBMovie.Filename, r.Matches)
                        If r.Matches.Count = 1 Then
                            b = GetMovieInfo(r.Matches.Item(0).TMDBID, nMovie, True, False, Options, True)
                        ElseIf r.Matches.Count > 1 Then
                            b = GetMovieInfo(r.Matches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).TMDBID, nMovie, True, False, Options, True)
                        End If
                End Select

                Return nMovie

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return New MediaContainers.Movie
            End Try
        End Function

        Public Function GetSearchMovieSetInfo(ByVal sMovieSetName As String, ByRef DBMovieSet As Structures.DBMovieSet, ByVal iType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_MovieSet) As MediaContainers.MovieSet
            Dim r As SearchResults_MovieSet = SearchMovieSet(sMovieSetName, Nothing)
            Dim b As Boolean = False
            Dim tmdbMovieSet As MediaContainers.MovieSet = DBMovieSet.MovieSet

            Try
                Select Case iType
                    Case Enums.ScrapeType.FullAsk, Enums.ScrapeType.MissAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.MarkAsk, Enums.ScrapeType.FilterAsk

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
                    Case Enums.ScrapeType.FilterSkip, Enums.ScrapeType.FullSkip, Enums.ScrapeType.MarkSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.MissSkip
                        If r.Matches.Count = 1 Then
                            b = GetMovieSetInfo(r.Matches.Item(0).ID, tmdbMovieSet, False, Options, True)
                        End If
                    Case Enums.ScrapeType.FullAuto, Enums.ScrapeType.MissAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.SingleScrape, Enums.ScrapeType.FilterAuto
                        If r.Matches.Count >= 1 Then
                            b = GetMovieSetInfo(r.Matches.Item(0).ID, tmdbMovieSet, False, Options, True)
                        End If
                End Select

                If b Then
                    Return tmdbMovieSet
                Else
                    Return tmdbMovieSet
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return New MediaContainers.MovieSet
            End Try
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
            Try
                If Not bwTMDB.IsBusy Then
                    bwTMDB.WorkerReportsProgress = False
                    bwTMDB.WorkerSupportsCancellation = True
                    bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails, _
                      .Parameter = tmdbID, .Movie = IMDBMovie, .Options_Movie = Options})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub GetSearchMovieSetInfoAsync(ByVal tmdbColID As String, ByVal IMDBMovieSet As MediaContainers.MovieSet, ByVal Options As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                If Not bwTMDB.IsBusy Then
                    bwTMDB.WorkerReportsProgress = False
                    bwTMDB.WorkerSupportsCancellation = True
                    bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_MovieSet, _
                      .Parameter = tmdbColID, .MovieSet = IMDBMovieSet, .Options_movieset = Options})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As Structures.ScrapeOptions_Movie, Optional ByVal sYear As String = "")
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
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
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieSetAsync(ByVal sMovieSet As String, ByVal filterOptions As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                If Not bwTMDB.IsBusy Then
                    bwTMDB.WorkerReportsProgress = False
                    bwTMDB.WorkerSupportsCancellation = True
                    bwTMDB.RunWorkerAsync(New Arguments With {.Search = SearchType.MovieSets, _
                      .Parameter = sMovieSet, .Options_MovieSet = filterOptions})
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
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

        Private Function CleanTitle(ByVal sString As String) As String
            Dim CleanString As String = sString

            If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)
            If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)

            Return CleanString
        End Function

        'Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String) As String
        '	Dim fTitle As String = oTitle
        '	Dim Movie As TMDbLib.Objects.Movies.Movie

        '	Try
        '		'' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        '		If Left(strID.ToLower(), 4) = "tt" Then
        '			Movie = _TMDBApi.GetMovieInfo(CInt(Right(strID, Len(strID) - 4)), _MySettings.TMDBLanguage)
        '			If IsNothing(Movie) And _MySettings.FallBackEng Then
        '				Movie = _TMDBApi.GetMovieInfo(CInt(Right(strID, Len(strID) - 4)), "en")
        '			End If
        '		Else
        '			Movie = _TMDBApi.GetMovieByIMDB(strID)
        '		End If

        '		fTitle = Movie.title

        '		Return fTitle
        '	Catch ex As Exception
        '		logger.Error(New StackFrame().GetMethod().Name, ex)
        '		Return fTitle
        '	End Try
        'End Function

        Private Function SearchMovie(ByVal sMovie As String, Optional ByVal sYear As Integer = 0) As SearchResults_Movie
            Dim R As New SearchResults_Movie
            Dim Page As Integer = 1
            Dim Movies As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
            Dim TotP As Integer
            Dim aE As Boolean

            Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)

            If Movies.TotalResults = 0 And _MySettings.FallBackEng Then
                Movies = _TMDBApiE.SearchMovie(sMovie, Page, _MySettings.GetAdultItems, sYear)
                aE = True
            End If

            If Movies.TotalResults > 0 Then
                Dim t1 As String = String.Empty
                Dim t2 As String = String.Empty
                Dim t3 As String = String.Empty
                TotP = Movies.TotalPages
                While Page <= TotP And Page <= 50
                    'If Movies.Results IsNot Nothing Then
                    For Each aMovie In Movies.Results
                        'Dim aMI As TMDbLib.Objects.Movies.Movie
                        'aMI = _TMDBApi.GetMovie(aMovie.Id)
                        'If aMI Is Nothing Then
                        '    aMI = _TMDBApiE.GetMovie(aMovie.Id)
                        'End If
                        'If aMI.ImdbId IsNot Nothing Then
                        '    t1 = CStr(aMI.ImdbId)
                        'End If
                        If aMovie.Title Is Nothing OrElse (aMovie.Title IsNot Nothing And String.IsNullOrEmpty(CStr(aMovie.Title))) Then
                            If aMovie.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.OriginalTitle)) Then
                                t2 = CStr(aMovie.OriginalTitle)
                            End If
                        Else
                            t2 = CStr(aMovie.Title)
                        End If
                        If aMovie.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMovie.ReleaseDate)) Then
                            t3 = CStr(aMovie.ReleaseDate.Value.Year)
                        End If
                        Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(t1, t2, t3, 0)
                        lNewMovie.TMDBID = CStr(aMovie.Id)
                        R.Matches.Add(lNewMovie)
                    Next
                    'End If
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
            Dim R As New SearchResults_MovieSet
            Dim Page As Integer = 1
            Dim MovieSets As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchResultCollection)
            Dim TotP As Integer
            Dim aE As Boolean

            MovieSets = _TMDBApi.SearchCollection(sMovieSet, Page)

            If MovieSets.TotalResults = 0 And _MySettings.FallBackEng Then
                MovieSets = _TMDBApiE.SearchCollection(sMovieSet, Page)
                aE = True
            End If

            If MovieSets.TotalResults > 0 Then
                Dim t1 As String = String.Empty
                Dim t2 As String = String.Empty
                Dim t3 As String = String.Empty
                TotP = MovieSets.TotalPages
                While Page <= TotP And Page <= 3
                    If MovieSets.Results IsNot Nothing Then
                        For Each aMovieSet In MovieSets.Results
                            Dim aMI As TMDbLib.Objects.Collections.Collection
                            aMI = _TMDBApi.GetCollection(aMovieSet.Id)
                            If aMI Is Nothing Then
                                aMI = _TMDBApiE.GetCollection(aMovieSet.Id)
                            End If
                            t1 = CStr(aMI.id)
                            If aMI.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMI.name)) Then
                                t2 = CStr(aMI.name)
                            End If
                            'If aMI.overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aMI.overview)) Then
                            '    t3 = CStr(aMI.overview)
                            'End If
                            Dim lNewMovieSet As MediaContainers.MovieSet = New MediaContainers.MovieSet(t1, t2, t3)
                            lNewMovieSet.ID = aMI.id.ToString
                            R.Matches.Add(lNewMovieSet)
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

