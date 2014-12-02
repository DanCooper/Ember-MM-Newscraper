﻿' ################################################################################
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
Imports RestSharp
Imports WatTmdb
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
        Private _Cancelled As Boolean
        Private intHTTP As EmberAPI.HTTP = Nothing

        Private _TMDBConf As V3.TmdbConfiguration
        Private _TMDBConfE As V3.TmdbConfiguration
        Private _TMDBConfA As V3.TmdbConfiguration
        Private _TMDBApi As V3.Tmdb 'preferred language
        Private _TMDBApiE As V3.Tmdb 'english language
        Private _TMDBApiA As V3.Tmdb 'all languages
        Private _MySettings As sMySettings_ForScraper
        Private strPrivateAPIKey As String = String.Empty

        Private _sPoster As String

#End Region 'Fields

#Region "Properties"
        Public ReadOnly Property TMDBConf() As V3.TmdbConfiguration
            Get
                Return _TMDBConf
            End Get
        End Property
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
                _TMDBApi = New WatTmdb.V3.Tmdb(Settings.ApiKey, Settings.PrefLanguage)
                If IsNothing(_TMDBApi) Then
                    logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
                Else
                    If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                        logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
                    End If
                End If
                _TMDBConf = _TMDBApi.GetConfiguration()
                _TMDBApiE = New WatTmdb.V3.Tmdb(Settings.ApiKey)
                _TMDBConfE = _TMDBApiE.GetConfiguration()
                _TMDBApiA = New WatTmdb.V3.Tmdb(Settings.ApiKey, String.Empty)
                _TMDBConfA = _TMDBApiA.GetConfiguration()

                _MySettings = Settings
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub CancelAsync()
            If Not IsNothing(intHTTP) Then
                intHTTP.Cancel()
            End If
            _Cancelled = True
        End Sub

        Public Sub GetMovieID(ByRef DBMovie As Structures.DBMovie)
            Try
                Dim Movie As WatTmdb.V3.TmdbMovie
                Dim MovieE As WatTmdb.V3.TmdbMovie

                If _Cancelled Then Return

                Movie = _TMDBApi.GetMovieByIMDB(DBMovie.Movie.ID, _MySettings.PrefLanguage)
                MovieE = _TMDBApiE.GetMovieByIMDB(DBMovie.Movie.ID)
                If IsNothing(Movie) AndAlso Not _MySettings.FallBackEng Then
                    Return
                End If

                DBMovie.Movie.TMDBID = CStr(IIf(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Function GetMovieID(ByRef IMDBID As String) As String
            Try
                Dim Movie As WatTmdb.V3.TmdbMovie
                Dim MovieE As WatTmdb.V3.TmdbMovie

                If _Cancelled Then Return String.Empty

                Movie = _TMDBApi.GetMovieByIMDB(IMDBID, _MySettings.PrefLanguage)
                MovieE = _TMDBApiE.GetMovieByIMDB(IMDBID)
                If IsNothing(Movie) AndAlso Not _MySettings.FallBackEng Then
                    Return String.Empty
                End If

                Return CStr(IIf(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return String.Empty
            End Try
        End Function

        Public Function GetMovieCollectionID(ByVal IMDBID As String) As String
            Try
                Dim Movie As WatTmdb.V3.TmdbMovie

                If _Cancelled Then Return String.Empty

                Movie = _TMDBApiE.GetMovieByIMDB(IMDBID)
                If IsNothing(Movie) Then
                    Return String.Empty
                End If

                If Not IsNothing(Movie.belongs_to_collection.id) Then
                    Return CStr(Movie.belongs_to_collection.id)
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
        ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper module should NOT use global scraper settings/locks in Ember, just scraper options of module
        ''' Instead of directly saving scraped results into DBMovie we use empty nMovie movie container to store retrieved information of scraper</remarks>
        Public Function GetMovieInfo(ByVal strID As String, ByRef nMovie As MediaContainers.Movie, ByVal FullCrew As Boolean, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_Movie, ByVal IsSearch As Boolean) As Boolean
            Try
                Dim Movie As WatTmdb.V3.TmdbMovie
                Dim MovieE As WatTmdb.V3.TmdbMovie
                Dim tStr As String
                Dim scrapedresult As String = ""

                'clear nMovie from search results
                nMovie.Clear()

                If _Cancelled Then Return Nothing

                If Strings.Left(strID.ToLower(), 2) = "tt" Then
                    Movie = _TMDBApi.GetMovieByIMDB(strID)
                    MovieE = _TMDBApiE.GetMovieByIMDB(strID)
                Else
                    Movie = _TMDBApi.GetMovieInfo(CInt(strID))
                    MovieE = _TMDBApiE.GetMovieInfo(CInt(strID))
                End If

                If (Not Movie.id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not Movie.id > 0 AndAlso Not MovieE.id > 0) Then
                    Return False
                End If
                nMovie.Scrapersource = "TMDB"
                nMovie.ID = CStr(IIf(String.IsNullOrEmpty(Movie.imdb_id) AndAlso _MySettings.FallBackEng, MovieE.imdb_id, Movie.imdb_id))
                nMovie.TMDBID = CStr(IIf(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

                If _Cancelled Or IsNothing(Movie) Then Return Nothing

                Dim Keywords As WatTmdb.V3.TmdbMovieKeywords
                Keywords = _TMDBApi.GetMovieKeywords(Movie.id)
                If Not IsNothing(Keywords) AndAlso Not IsNothing(Keywords.keywords) Then
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
                    If String.IsNullOrEmpty(Movie.title) Then
                        If _MySettings.FallBackEng Then
                            If Not String.IsNullOrEmpty(MovieE.title) Then
                                nMovie.Title = MovieE.title
                            End If
                        End If
                    Else
                        nMovie.Title = Movie.title
                    End If
                End If

                If _Cancelled Then Return Nothing

                'OriginalTitle
                If Options.bOriginalTitle Then
                    If String.IsNullOrEmpty(Movie.original_title) Then
                        If _MySettings.FallBackEng Then
                            If Not String.IsNullOrEmpty(MovieE.original_title) Then
                                nMovie.OriginalTitle = MovieE.original_title
                            End If
                        End If
                    Else
                        nMovie.OriginalTitle = Movie.original_title
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Collection ID
                If Options.bCollectionID Then
                    'Get collection information
                    If IsNothing(Movie.belongs_to_collection) Then
                        If _MySettings.FallBackEng Then
                            If Not IsNothing(MovieE.belongs_to_collection) Then
                                nMovie.AddSet(Nothing, MovieE.belongs_to_collection.name, Nothing, MovieE.belongs_to_collection.id.ToString)
                                nMovie.TMDBColID = MovieE.belongs_to_collection.id.ToString
                            End If
                        End If
                    Else
                        nMovie.AddSet(Nothing, Movie.belongs_to_collection.name, Nothing, MovieE.belongs_to_collection.id.ToString)
                        nMovie.TMDBColID = Movie.belongs_to_collection.id.ToString
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Posters (only for SearchResult dialog)
                If GetPoster Then
                    ' I will add original always. to be updated if size, TMDBConf.images.poster_sizes(0) & 
                    Dim Images As WatTmdb.V3.TmdbMovieImages
                    Images = _TMDBApi.GetMovieImages(Movie.id)
                    If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
                        If Images.posters.Count = 0 Then
                            Images = _TMDBApiE.GetMovieImages(Movie.id)
                        End If
                    Else
                        Images = _TMDBApiE.GetMovieImages(Movie.id)
                    End If
                    If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
                        If Images.posters.Count > 0 Then
                            _sPoster = _TMDBConf.images.base_url & "w92" & Images.posters(0).file_path
                        Else
                            _sPoster = ""
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Year
                If Options.bYear Then
                    scrapedresult = Left(CStr(IIf(String.IsNullOrEmpty(Movie.release_date) AndAlso _MySettings.FallBackEng, MovieE.release_date, Movie.release_date)), 4)
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Year = scrapedresult
                    End If
                End If

                If _Cancelled Then Return Nothing

                Dim Releases As WatTmdb.V3.TmdbMovieReleases = Nothing

                'MPAA/Certification
                If Options.bCert Then
                    Releases = _TMDBApi.GetMovieReleases(Movie.id)
                    If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
                        If (Releases.countries.Count = 0) AndAlso _MySettings.FallBackEng Then
                            Releases = _TMDBApiE.GetMovieReleases(Movie.id)
                        End If
                    Else
                        If _MySettings.FallBackEng Then
                            Releases = _TMDBApiE.GetMovieReleases(Movie.id)
                        End If
                    End If

                    If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
                        'only update nMovie if scraped result is not empty/nothing!
                        If Releases.countries.Count > 0 Then
                            For Each Country In Releases.countries
                                If Country.iso_3166_1.ToLower = CStr(IIf(Master.eSettings.MovieScraperCertLang = "", "us", Master.eSettings.MovieScraperCertLang)) Then
                                    If Not String.IsNullOrEmpty(Country.certification) Then
                                        nMovie.MPAA = String.Concat(APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Country.iso_3166_1.ToLower).name, ":", Country.certification)
                                        nMovie.Certification = nMovie.MPAA
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                'ReleaseDate
                If Options.bRelease Then
                    scrapedresult = CStr(IIf(String.IsNullOrEmpty(Movie.release_date) AndAlso _MySettings.FallBackEng, MovieE.release_date, Movie.release_date))
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        Dim RelDate As Date
                        If Date.TryParse(scrapedresult, RelDate) Then
                            nMovie.ReleaseDate = Strings.FormatDateTime(RelDate, Microsoft.VisualBasic.DateFormat.ShortDate).ToString
                            'FormatDateTime interprets date according to the CurrentCulture of user -> different results depending on user country!
                            '  nMovie.ReleaseDate = Strings.FormatDateTime(RelDate, Microsoft.VisualBasic.DateFormat.ShortDate).ToString
                            'always save date in same date format not depending on users language setting!
                            nMovie.ReleaseDate = RelDate.ToString("yyyy-MM-dd")
                        Else
                            nMovie.ReleaseDate = scrapedresult
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Rating
                If Options.bRating Then
                    scrapedresult = CStr(IIf(IsNothing(Movie.vote_average) AndAlso Movie.vote_average = 0 AndAlso _MySettings.FallBackEng, MovieE.vote_average, Movie.vote_average))
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Rating = scrapedresult
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Trailer
                If Options.bTrailer Then
                    Dim Trailers As WatTmdb.V3.TmdbMovieTrailers
                    Trailers = _TMDBApi.GetMovieTrailers(Movie.id)
                    If Not IsNothing(Trailers) AndAlso Not IsNothing(Trailers.youtube) Then
                        If (Trailers.youtube.Count = 0) AndAlso _MySettings.FallBackEng Then
                            Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
                        End If
                    Else
                        If _MySettings.FallBackEng Then
                            Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
                        End If
                    End If

                    If Not IsNothing(Trailers) AndAlso Not IsNothing(Trailers.youtube) Then
                        If Trailers.youtube.Count > 0 Then
                            nMovie.Trailer = "http://www.youtube.com/watch?hd=1&v=" & Trailers.youtube(0).source
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Votes
                If Options.bVotes Then
                    scrapedresult = CStr(IIf(IsNothing(Movie.vote_count) AndAlso Movie.vote_count = 0 AndAlso _MySettings.FallBackEng, MovieE.vote_count.ToString(), Movie.vote_count.ToString()))
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Votes = scrapedresult
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Actors
                Dim aCast As WatTmdb.V3.TmdbMovieCast = Nothing
                If Options.bCast Then
                    aCast = _TMDBApi.GetMovieCast(Movie.id)
                    If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
                        If (aCast.cast.Count = 0) AndAlso _MySettings.FallBackEng Then
                            aCast = _TMDBApiE.GetMovieCast(Movie.id)
                        End If
                    Else
                        If _MySettings.FallBackEng Then
                            aCast = _TMDBApiE.GetMovieCast(Movie.id)
                        End If
                    End If

                    Dim Cast As New List(Of MediaContainers.Person)
                    If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
                        For Each aAc As WatTmdb.V3.Cast In aCast.cast
                            Dim aPer As New MediaContainers.Person
                            aPer.Name = aAc.name
                            aPer.Role = aAc.character
                            ' to be added / dialog to choose the size of the images
                            If Not String.IsNullOrEmpty(aAc.profile_path) Then
                                aPer.Thumb = _TMDBConf.images.base_url & "original" & aAc.profile_path
                            End If
                            Cast.Add(aPer)
                        Next
                    End If
                    'only update nMovie if scraped result is not empty/nothing!
                    If Cast.Count > 0 Then
                        nMovie.Actors = Cast
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Tagline
                If Options.bTagline Then
                    If String.IsNullOrEmpty(Movie.tagline) Then
                        If _MySettings.FallBackEng Then
                            If Not String.IsNullOrEmpty(MovieE.tagline) Then
                                nMovie.Tagline = MovieE.tagline
                            End If
                        End If
                    Else
                        nMovie.Tagline = Movie.tagline
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Countries
                If Options.bCountry Then
                    nMovie.Countries.Clear()
                    If Not IsNothing(Movie.production_countries) AndAlso Movie.production_countries.Count > 0 Then
                        For Each aCo As WatTmdb.V3.ProductionCountry In Movie.production_countries
                            nMovie.Countries.Add(aCo.name) 'XBMC use full names
                        Next
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Genres
                If Options.bGenre Then
                    nMovie.Genres.Clear()
                    Dim tGen As System.Collections.Generic.List(Of WatTmdb.V3.MovieGenre)
                    If Not IsNothing(Movie) AndAlso Not IsNothing(Movie.genres) Then
                        tGen = CType(IIf(Movie.genres.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.genres, Movie.genres), Global.System.Collections.Generic.List(Of Global.WatTmdb.V3.MovieGenre))
                    Else
                        tGen = CType(IIf(_MySettings.FallBackEng, MovieE.genres, Nothing), Global.System.Collections.Generic.List(Of Global.WatTmdb.V3.MovieGenre))
                    End If

                    If Not IsNothing(tGen) AndAlso tGen.Count > 0 Then
                        For Each aGen As WatTmdb.V3.MovieGenre In tGen
                            nMovie.Genres.Add(aGen.name)
                        Next
                    End If
                End If

                If _Cancelled Then Return Nothing

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

                If _Cancelled Then Return Nothing

                'Runtime
                If Options.bRuntime Then
                    scrapedresult = CStr(IIf(IsNothing(Movie.runtime) AndAlso Movie.runtime = 0 AndAlso _MySettings.FallBackEng, MovieE.runtime.ToString(), Movie.runtime.ToString()))
                    'only update nMovie if scraped result is not empty/nothing!
                    If Not String.IsNullOrEmpty(scrapedresult) Then
                        nMovie.Runtime = scrapedresult
                    End If
                End If

                'Studios
                If Options.bStudio Then
                    'Get Production Studio
                    tStr = ""
                    Dim tPC As System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany)
                    If Not IsNothing(Movie) AndAlso Not IsNothing(Movie.genres) Then
                        tPC = CType(IIf(Movie.production_companies.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.production_companies, Movie.production_companies), Global.System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany))
                    Else
                        tPC = CType(IIf(_MySettings.FallBackEng, MovieE.production_companies, Nothing), Global.System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany))
                    End If

                    If Not IsNothing(tPC) Then
                        For Each aPro As WatTmdb.V3.ProductionCompany In tPC
                            If Not String.IsNullOrEmpty(aPro.name) Then
                                nMovie.Studios.Add(aPro.name)
                            End If
                            tStr = tStr & " / " & aPro.name
                        Next
                    End If
                    'If Len(tStr) > 3 Then
                    '    tStr = Trim(Right(tStr, Len(tStr) - 3))
                    'End If
                    ''only update nMovie if scraped result is not empty/nothing!
                    'If Not String.IsNullOrEmpty(tStr) Then
                    '    nMovie.Studio = tStr
                    'End If
                End If

                If _Cancelled Then Return Nothing

                'Use TMDB other infos?
                If FullCrew Or Options.bWriters Or Options.bDirector Then
                    'Get All Other Info
                    If IsNothing(aCast) Then
                        aCast = _TMDBApi.GetMovieCast(Movie.id)
                        If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
                            If (aCast.crew.Count = 0) AndAlso _MySettings.FallBackEng Then
                                aCast = _TMDBApiE.GetMovieCast(Movie.id)
                            End If
                        Else
                            If _MySettings.FallBackEng Then
                                aCast = _TMDBApiE.GetMovieCast(Movie.id)
                            End If
                        End If
                    End If

                    If Not IsNothing(aCast.crew) Then
                        For Each aAc As WatTmdb.V3.Crew In aCast.crew

                            If Options.bWriters Then
                                If aAc.department = "Writing" AndAlso aAc.job = "Writer" Then
                                    nMovie.Credits.Add(aAc.name)
                                End If
                            End If
                            If Options.bDirector Then
                                If aAc.job = "Director" Then
                                    nMovie.Directors.Add(aAc.name)
                                End If
                            End If
                        Next
                    End If
                End If

                If _Cancelled Then Return Nothing

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

        Public Function GetMovieSetInfo(ByVal strID As String, ByRef DBMovieSet As MediaContainers.MovieSet, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_MovieSet, ByVal IsSearch As Boolean) As Boolean
            Try
                Dim MovieSet As WatTmdb.V3.TmdbCollection
                Dim MovieSetE As WatTmdb.V3.TmdbCollection
                Dim scrapedresult As String = ""

                If _Cancelled Then Return Nothing

                MovieSet = _TMDBApi.GetCollectionInfo(CInt(strID))
                MovieSetE = _TMDBApiE.GetCollectionInfo(CInt(strID))

                If (Not MovieSet.id > 0 AndAlso Not _MySettings.FallBackEng) OrElse (Not MovieSet.id > 0 AndAlso Not MovieSetE.id > 0) Then
                    Return False
                End If

                DBMovieSet.ID = CStr(IIf(Not MovieSet.id > 0 AndAlso _MySettings.FallBackEng, MovieSetE.id.ToString, MovieSet.id.ToString))

                If _Cancelled Or IsNothing(MovieSet) Then Return Nothing

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
                        DBMovieSet.Title = Replace(DBMovieSet.Title, sett.Name.Substring(21), sett.Value)
                    Next
                End If

                If _Cancelled Then Return Nothing

                'Posters (only for SearchResult dialog)
                If GetPoster Then
                    ' I will add original always. to be updated if size, TMDBConf.images.poster_sizes(0) & 
                    Dim Images As WatTmdb.V3.TmdbMovieImages
                    Images = _TMDBApi.GetMovieImages(MovieSet.id)
                    If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
                        If (Images.posters.Count = 0) Then
                            Images = _TMDBApiE.GetMovieImages(MovieSet.id)
                        End If
                    Else
                        Images = _TMDBApiE.GetMovieImages(MovieSetE.id)
                    End If
                    If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
                        If Images.posters.Count > 0 Then
                            _sPoster = _TMDBConf.images.base_url & "w92" & Images.posters(0).file_path
                        Else
                            _sPoster = ""
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                'Plot
                If Options.bPlot Then
                    If String.IsNullOrEmpty(DBMovieSet.Plot) OrElse Not Master.eSettings.MovieSetLockPlot Then
                        If String.IsNullOrEmpty(MovieSet.overview) Then
                            If _MySettings.FallBackEng Then
                                If Not String.IsNullOrEmpty(MovieSetE.overview) Then
                                    DBMovieSet.Plot = MovieSetE.overview
                                End If
                            End If
                        Else
                            DBMovieSet.Plot = MovieSet.overview
                        End If
                    End If
                End If

                If _Cancelled Then Return Nothing

                Return True
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return False
            End Try
        End Function

        Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
            Dim alStudio As New List(Of String)
            Dim Movie As WatTmdb.V3.TmdbMovie
            Movie = _TMDBApi.GetMovieByIMDB(strID, _MySettings.PrefLanguage)
            If Movie.production_companies.Count = 0 And _MySettings.FallBackEng Then
                Movie = _TMDBApi.GetMovieByIMDB(strID, "en")
            End If
            For Each aComp In Movie.production_companies
                alStudio.Add(aComp.name)
            Next

            Return alStudio

        End Function

        Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByVal iType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As MediaContainers.Movie
            Dim r As SearchResults_Movie = SearchMovie(sMovieName, CInt(IIf(Not String.IsNullOrEmpty(oDBMovie.Movie.Year), oDBMovie.Movie.Year, Nothing)))
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
                If IsNumeric(tmpyear) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
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

        Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal IMDBMovie As MediaContainers.Movie, ByVal Options As Structures.ScrapeOptions_Movie)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                Dim s As Boolean = GetMovieInfo(imdbID, IMDBMovie, False, True, Options, True)
                RaiseEvent SearchInfoDownloaded_Movie(_sPoster, s)

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub GetSearchMovieSetInfoAsync(ByVal tmdbColID As String, ByVal IMDBMovieSet As MediaContainers.MovieSet, ByVal Options As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                Dim s As Boolean = GetMovieSetInfo(tmdbColID, IMDBMovieSet, True, Options, True)
                RaiseEvent SearchInfoDownloaded_MovieSet(_sPoster, s)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As Structures.ScrapeOptions_Movie, Optional ByVal sYear As Integer = 0)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                Dim r As SearchResults_Movie = SearchMovie(sMovie, sYear)
                RaiseEvent SearchResultsDownloaded_Movie(r)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub SearchMovieSetAsync(ByVal sMovieSet As String, ByVal filterOptions As Structures.ScrapeOptions_MovieSet)
            '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
            Try
                Dim r As SearchResults_MovieSet
                r = SearchMovieSet(sMovieSet)
                RaiseEvent SearchResultsDownloaded_MovieSet(r)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        'Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDB.DoWork
        '    Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '    '' The rule is that if there is a tt is an IMDB otherwise is a TMDB
        '    Try
        '        Select Case Args.Search
        '            Case SearchType.Movies
        '                Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
        '                e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

        '            Case SearchType.SearchDetails
        '                Dim s As Boolean = GetMovieInfo(Args.Parameter, Args.Movie, False, True, Args.Options_Movie, True)
        '                e.Result = New Results With {.ResultType = SearchType.SearchDetails, .Success = s}

        '            Case SearchType.MovieSets
        '                Dim r As SearchResults_MovieSet = SearchMovieSet(Args.Parameter)
        '                e.Result = New Results With {.ResultType = SearchType.MovieSets, .Result = r}

        '            Case SearchType.SearchDetails_MovieSet
        '                Dim s As Boolean = GetMovieSetInfo(Args.Parameter, Args.MovieSet, True, Args.Options_MovieSet, True)
        '                e.Result = New Results With {.ResultType = SearchType.SearchDetails_MovieSet, .Success = s}
        '        End Select
        '    Catch ex As Exception
        '        logger.Error(New StackFrame().GetMethod().Name, ex)
        '    End Try
        'End Sub

        'Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDB.RunWorkerCompleted
        '    Dim Res As Results = DirectCast(e.Result, Results)

        '    Try
        '        Select Case Res.ResultType
        '            Case SearchType.Movies
        '                RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

        '            Case SearchType.SearchDetails
        '                Dim movieInfo As SearchResults_Movie = DirectCast(Res.Result, SearchResults_Movie)
        '                RaiseEvent SearchInfoDownloaded_Movie(_sPoster, Res.Success)

        '            Case SearchType.MovieSets
        '                RaiseEvent SearchResultsDownloaded_MovieSet(DirectCast(Res.Result, SearchResults_MovieSet))

        '            Case SearchType.SearchDetails_MovieSet
        '                Dim moviesetInfo As SearchResults_MovieSet = DirectCast(Res.Result, SearchResults_MovieSet)
        '                RaiseEvent SearchInfoDownloaded_MovieSet(_sPoster, Res.Success)
        '        End Select
        '    Catch ex As Exception
        '        logger.Error(New StackFrame().GetMethod().Name, ex)
        '    End Try
        'End Sub

        Private Function CleanTitle(ByVal sString As String) As String
            Dim CleanString As String = sString

            Try
                If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

                If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return CleanString
        End Function

        'Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String) As String
        '	Dim fTitle As String = oTitle
        '	Dim Movie As WatTmdb.V3.TmdbMovie

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
            Try
                Dim R As New SearchResults_Movie
                Dim Page As Integer = 1
                Dim Movies As WatTmdb.V3.TmdbMovieSearch
                Dim TotP As Integer
                Dim aE As Boolean
                'If sYear Is Nothing Then
                'Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
                'Else
                Movies = _TMDBApi.SearchMovie(sMovie, Page, , _MySettings.GetAdultItems, sYear)
                'End If
                If Movies.total_results = 0 And _MySettings.FallBackEng Then
                    'If sYear Is Nothing Then
                    ' Movies = _TMDBApiE.SearchMovie(sMovie, Page)
                    'Else
                    Movies = _TMDBApiE.SearchMovie(sMovie, Page, , _MySettings.GetAdultItems, sYear)
                    'End If
                    aE = True
                End If
                If Movies.total_results > 0 Then
                    Dim t1 As String
                    Dim t2 As String
                    Dim t3 As String
                    TotP = Movies.total_pages
                    While Page <= TotP And Page <= 3
                        For Each aMovie In Movies.results
                            Dim aMI As WatTmdb.V3.TmdbMovie
                            aMI = _TMDBApi.GetMovieInfo(aMovie.id)
                            If IsNothing(aMI) Then
                                aMI = _TMDBApiE.GetMovieInfo(aMovie.id)
                            End If
                            If IsNothing(aMI.imdb_id) Then
                                t1 = ""
                            Else
                                t1 = aMI.imdb_id.ToString
                            End If
                            t2 = CStr(IIf(String.IsNullOrEmpty(aMovie.title), "", aMovie.title))
                            t3 = Left(CStr(IIf(String.IsNullOrEmpty(aMovie.release_date), "", aMovie.release_date)), 4)
                            Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(t1, t2, t3, 0)
                            lNewMovie.TMDBID = aMI.id.ToString
                            R.Matches.Add(lNewMovie)
                        Next
                        Page = Page + 1
                        If aE Then
                            'If sYear Is Nothing Then
                            'Movies = _TMDBApiE.SearchMovie(sMovie, Page)
                            'Else
                            Movies = _TMDBApiE.SearchMovie(sMovie, Page, , _MySettings.GetAdultItems, sYear)
                            'End If
                        Else
                            'If sYear Is Nothing Then
                            'Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
                            'Else
                            Movies = _TMDBApi.SearchMovie(sMovie, Page, , _MySettings.GetAdultItems, sYear)
                            'End If
                        End If

                    End While
                End If

                Return R
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
        End Function

        Private Function SearchMovieSet(ByVal sMovieSet As String, Optional ByVal sYear As Integer = 0) As SearchResults_MovieSet
            Try
                Dim R As New SearchResults_MovieSet
                Dim Page As Integer = 1
                Dim MovieSets As WatTmdb.V3.TmdbCollectionSearch
                Dim TotP As Integer
                Dim aE As Boolean
                'If sYear Is Nothing Then
                'Movies = _TMDBApi_MovieSet.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
                'Else
                MovieSets = _TMDBApi.SearchCollection(sMovieSet, Page)
                'End If
                If MovieSets.total_results = 0 And _MySettings.FallBackEng Then
                    'If sYear Is Nothing Then
                    ' Movies = _TMDBApiE_MovieSet.SearchMovie(sMovie, Page)
                    'Else
                    MovieSets = _TMDBApiE.SearchCollection(sMovieSet, Page)
                    'End If
                    aE = True
                End If
                If MovieSets.total_results > 0 Then
                    Dim t1 As String = String.Empty
                    Dim t2 As String = String.Empty
                    Dim t3 As String = String.Empty
                    TotP = MovieSets.total_pages
                    While Page <= TotP And Page <= 3
                        For Each aMovieSet In MovieSets.results
                            Dim aMI As WatTmdb.V3.TmdbCollection
                            aMI = _TMDBApi.GetCollectionInfo(aMovieSet.id)
                            If IsNothing(aMI) Then
                                aMI = _TMDBApiE.GetCollectionInfo(aMovieSet.id)
                            End If
                            t1 = aMI.id.ToString
                            t2 = CStr(IIf(String.IsNullOrEmpty(aMI.name), "", aMI.name))
                            t3 = CStr(IIf(String.IsNullOrEmpty(aMI.overview), "", aMI.overview))
                            Dim lNewMovieSet As MediaContainers.MovieSet = New MediaContainers.MovieSet(t1, t2, t3)
                            lNewMovieSet.ID = aMI.id.ToString
                            R.Matches.Add(lNewMovieSet)
                        Next
                        Page = Page + 1
                        If aE Then
                            'If sYear Is Nothing Then
                            'Movies = _TMDBApiE_MovieSet.SearchMovie(sMovie, Page)
                            'Else
                            MovieSets = _TMDBApiE.SearchCollection(sMovieSet, Page)
                            'End If
                        Else
                            'If sYear Is Nothing Then
                            'Movies = _TMDBApi_MovieSet.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
                            'Else
                            MovieSets = _TMDBApi.SearchCollection(sMovieSet, Page)
                            'End If
                        End If
                    End While
                End If

                Return R
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return Nothing
            End Try
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

