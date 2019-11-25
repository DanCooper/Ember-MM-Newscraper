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
Imports System.Text.RegularExpressions

Public Class SearchResults_Movie

#Region "Properties"

    Public Property Matches() As New List(Of MediaContainers.Movie)

#End Region 'Properties

End Class

Public Class SearchResults_TVShow

#Region "Properties"

    Public Property Matches() As New List(Of MediaContainers.TVShow)

#End Region 'Properties

End Class

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwOMDb As New ComponentModel.BackgroundWorker

    Private _AddonSettings As AddonSettings
    Private _Client As OMDbSharp.OMDbClient
    Private _PosterURL As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property IsClientCreated As Boolean
        Get
            Return _Client IsNot Nothing
        End Get
    End Property

    Public Property PreferredLanguage As String = "en-EN"

#End Region 'Properties

#Region "Enumerations"

    Private Enum SearchType
        Movies
        Details
        SearchDetails_Movie
        MovieSets
        SearchDetails_MovieSet
        TVShows
        SearchDetails_TVShow
    End Enum

#End Region 'Enumerations

#Region "Events"

    Public Event SearchInfoDownloaded_Movie(ByVal PosterURL As String, ByVal Info As MediaContainers.Movie)
    Public Event SearchInfoDownloaded_TVShow(ByVal PosterURL As String, ByVal Info As MediaContainers.TVShow)

    Public Event SearchResultsDownloaded_Movie(ByVal Results As SearchResults_Movie)
    Public Event SearchResultsDownloaded_TVShow(ByVal Results As SearchResults_TVShow)

#End Region 'Events

#Region "Methods"

    Public Sub CreateAPI(ByVal AddonSettings As AddonSettings)
        Try
            _AddonSettings = AddonSettings
            _Client = New OMDbSharp.OMDbClient(_AddonSettings.APIKey, True) ' _addonSettings.GetRottenTomatoesRating)
            _Logger.Trace("[OMDb_Data] [CreateAPI] Client created")
        Catch ex As Exception
            _Logger.Error(String.Format("[OMDb_Data] [CreateAPI] [Error] {0}", ex.Message))
            _Client = Nothing
        End Try
    End Sub

    Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwOMDb.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '' The rule is that if there is a tt is an IMDB otherwise is a TMDB

        Select Case Args.Search
            Case SearchType.Movies
                Dim r As SearchResults_Movie = SearchMovie(Args.Parameter, Args.Year)
                e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}

            Case SearchType.TVShows
                Dim r As SearchResults_TVShow = SearchTVShow(Args.Parameter)
                e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

            Case SearchType.SearchDetails_Movie
                Dim r As MediaContainers.Movie = GetInfo_Movie(Args.Parameter, Args.ScrapeOptions, True)
                e.Result = New Results With {.ResultType = SearchType.SearchDetails_Movie, .Result = r}

            Case SearchType.SearchDetails_TVShow
                Dim r As MediaContainers.TVShow = GetInfo_TVShow(Args.Parameter, Args.ScrapeModifiers, Args.ScrapeOptions, True)
                e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = r}
        End Select
    End Sub

    Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwOMDb.RunWorkerCompleted
        Dim Res As Results = DirectCast(e.Result, Results)

        Select Case Res.ResultType
            Case SearchType.Movies
                RaiseEvent SearchResultsDownloaded_Movie(DirectCast(Res.Result, SearchResults_Movie))

            Case SearchType.TVShows
                RaiseEvent SearchResultsDownloaded_TVShow(DirectCast(Res.Result, SearchResults_TVShow))

            Case SearchType.SearchDetails_Movie
                Dim movieInfo As MediaContainers.Movie = DirectCast(Res.Result, MediaContainers.Movie)
                RaiseEvent SearchInfoDownloaded_Movie(_PosterURL, movieInfo)

            Case SearchType.SearchDetails_TVShow
                Dim showInfo As MediaContainers.TVShow = DirectCast(Res.Result, MediaContainers.TVShow)
                RaiseEvent SearchInfoDownloaded_TVShow(_PosterURL, showInfo)
        End Select
    End Sub

    Public Sub CancelAsync()
        If bwOMDb.IsBusy Then bwOMDb.CancelAsync()

        While bwOMDb.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from OMDb
    ''' </summary>
    ''' <param name="strID">TMDb or IMDb ID (IMDB ID starts with "tt") of movie to be scraped</param>
    ''' <param name="isSearch">Scrape posters for the movie?</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_Movie(ByVal strID As String, ByVal filteredOptions As Structures.ScrapeOptions, ByVal isSearch As Boolean) As MediaContainers.Movie
        If String.IsNullOrEmpty(strID) Then Return Nothing

        Dim bIsScraperLanguage As Boolean = PreferredLanguage.ToLower.StartsWith("en")

        Dim nMovie As New MediaContainers.Movie
        Dim intTMDBID As Integer = -1

        If bwOMDb.CancellationPending Then Return Nothing

        Dim APIResult As Task(Of OMDbSharp.Item)

        'search movie by IMDB or TMDB ID
        APIResult = Task.Run(Function() _Client.GetItemByID(strID))

        Dim Result As OMDbSharp.Item = APIResult.Result

        If Result Is Nothing Then
            _Logger.Error(String.Format("Can't scrape or movie not found: [0]", strID))
            Return Nothing
        End If

        nMovie.Scrapersource = "OMDb"

        'IDs
        If Not String.IsNullOrEmpty(Result.imdbID) Then
            nMovie.UniqueIDs.Items.Add(New MediaContainers.Uniqueid With {
                                       .Type = "imdb",
                                       .Value = Result.imdbID
                                       })
        End If

        If bwOMDb.CancellationPending Or Result Is Nothing Then Return Nothing

        'Countries
        If filteredOptions.bMainCountries Then
            If Not String.IsNullOrEmpty(Result.Country) Then
                nMovie.Countries.AddRange(Regex.Replace(Result.Country, ", ", ",").Split(",".ToArray, StringSplitOptions.RemoveEmptyEntries))
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Director / Writer
        If filteredOptions.bMainDirectors OrElse filteredOptions.bMainWriters Then
            If Not String.IsNullOrEmpty(Result.Director) Then
                nMovie.Directors.AddRange(Regex.Replace(Result.Director, ", ", ",").Split(",".ToArray, StringSplitOptions.RemoveEmptyEntries))
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Genres
        If filteredOptions.bMainGenres Then
            If Not String.IsNullOrEmpty(Result.Genre) Then
                nMovie.Genres.AddRange(Regex.Replace(Result.Genre, ", ", ",").Split(",".ToArray, StringSplitOptions.RemoveEmptyEntries))
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Plot
        If filteredOptions.bMainPlot AndAlso bIsScraperLanguage OrElse isSearch Then
            If Not String.IsNullOrEmpty(Result.Plot) Then
                nMovie.Plot = Result.Plot
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
        If isSearch AndAlso Not String.IsNullOrEmpty(Result.Poster) Then
            _PosterURL = Result.Poster
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Rating
        If filteredOptions.bMainRating Then
            Dim dblRating As Double
            Dim iVotes As Integer
            If Not String.IsNullOrEmpty(Result.imdbRating) AndAlso (Double.TryParse(Result.imdbRating, dblRating)) AndAlso Not String.IsNullOrEmpty(Result.imdbVotes) AndAlso Integer.TryParse(Regex.Replace(Result.imdbVotes, "\D", String.Empty), iVotes) Then
                nMovie.Ratings.Add(New MediaContainers.RatingDetails With {
                                   .Max = 10,
                                   .Name = "imdb",
                                   .Value = dblRating,
                                   .Votes = iVotes
                                   })
            End If
            If Not String.IsNullOrEmpty(Result.Metascore) AndAlso Double.TryParse(Result.Metascore, dblRating) Then
                nMovie.Ratings.Add(New MediaContainers.RatingDetails With {
                                   .Max = 100,
                                   .Name = "metascore",
                                   .Value = dblRating
                                   })
            End If
            If Not String.IsNullOrEmpty(Result.tomatoMeter) AndAlso Not Result.tomatoMeter = "N/A" Then

            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'ReleaseDate
        If filteredOptions.bMainRelease Then
            If Not String.IsNullOrEmpty(Result.Released) Then
                Dim RelDate As Date
                If Date.TryParse(Result.Released, RelDate) Then
                    'always save date in same date format not depending on users language setting!
                    nMovie.ReleaseDate = RelDate.ToString("yyyy-MM-dd")
                End If
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Runtime
        If filteredOptions.bMainRuntime Then
            If Not String.IsNullOrEmpty(Result.Runtime) Then
                nMovie.Runtime = Result.Runtime
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Title
        If filteredOptions.bMainTitle Then
            If Not String.IsNullOrEmpty(Result.Title) Then
                nMovie.Title = Result.Title
            End If
        End If

        If bwOMDb.CancellationPending Then Return Nothing

        'Year
        If filteredOptions.bMainYear Then
            Dim iYear As Integer
            If Not String.IsNullOrEmpty(Result.Year) AndAlso Integer.TryParse(Result.Year, iYear) Then
                nMovie.Year = iYear
            End If
        End If

        Return nMovie
    End Function
    ''' <summary>
    '''  Scrape TV Show details from TMDB
    ''' </summary>
    ''' <param name="strID">TMDB ID of tv show to be scraped</param>
    ''' <param name="GetPoster">Scrape posters for the movie?</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_TVShow(ByVal strID As String, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions, ByVal GetPoster As Boolean) As MediaContainers.TVShow
        If String.IsNullOrEmpty(strID) Then Return Nothing

        Dim nTVShow As New MediaContainers.TVShow
        Dim intTMDBID As Integer = -1

        'If Integer.TryParse(strID, intTMDBID) Then
        '    If bwOMDb.CancellationPending Then Return Nothing

        '    Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
        '    Dim APIResultE As Task(Of TMDbLib.Objects.TvShows.TvShow)

        '    'search movie by TMDB ID
        '    APIResult = Task.Run(Function() _client.GetTvShowAsync(CInt(intTMDBID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
        '    If _addonSettings.FallBackEng Then
        '        APIResultE = Task.Run(Function() _clientE.GetTvShowAsync(CInt(intTMDBID), TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
        '    Else
        '        APIResultE = APIResult
        '    End If

        '    If APIResult Is Nothing OrElse APIResultE Is Nothing Then
        '        Return Nothing
        '    End If

        '    Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result
        '    Dim ResultE As TMDbLib.Objects.TvShows.TvShow = APIResultE.Result

        '    If (Result Is Nothing AndAlso Not _addonSettings.FallBackEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
        '            (Not Result.Id > 0 AndAlso Not _addonSettings.FallBackEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
        '        logger.Error(String.Format("Can't scrape or tv show not found: [{0}]", strID))
        '        Return Nothing
        '    End If

        '    nTVShow.Scrapersource = "TMDB"

        '    'IDs
        '    nTVShow.TMDB = CStr(Result.Id)
        '    If Result.ExternalIds.TvdbId IsNot Nothing Then nTVShow.TVDB = CStr(Result.ExternalIds.TvdbId)
        '    If Result.ExternalIds.ImdbId IsNot Nothing Then nTVShow.IMDB = Result.ExternalIds.ImdbId

        '    If bwOMDb.CancellationPending Or Result Is Nothing Then Return Nothing

        '    'Actors
        '    If FilteredOptions.bMainActors Then
        '        If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
        '            For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.Credits.Cast
        '                Dim nUniqueID As New MediaContainers.Uniqueid With {
        '                    .Type = "tmdb",
        '                    .Value = aCast.Id.ToString}
        '                nTVShow.Actors.Add(New MediaContainers.Person With {
        '                                   .Name = aCast.Name,
        '                                   .Role = aCast.Character,
        '                                   .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
        '                                   .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
        '                                   })
        '            Next
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Certifications
        '    If FilteredOptions.bMainCertifications Then
        '        If Result.ContentRatings IsNot Nothing AndAlso Result.ContentRatings.Results IsNot Nothing AndAlso Result.ContentRatings.Results.Count > 0 Then
        '            For Each aCountry In Result.ContentRatings.Results
        '                If Not String.IsNullOrEmpty(aCountry.Rating) Then
        '                    Dim CertificationLanguage = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = aCountry.Iso_3166_1.ToLower)
        '                    If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
        '                        nTVShow.Certifications.Add(String.Concat(CertificationLanguage.name, ":", aCountry.Rating))
        '                    Else
        '                        logger.Warn("Unhandled certification language encountered: {0}", aCountry.Iso_3166_1.ToLower)
        '                    End If
        '                End If
        '            Next
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Countries 'TODO: Change from OriginCountry to ProductionCountries (not yet supported by API)
        '    'If FilteredOptions.bMainCountry Then
        '    '    If Show.OriginCountry IsNot Nothing AndAlso Show.OriginCountry.Count > 0 Then
        '    '        For Each aCountry As String In Show.OriginCountry
        '    '            nShow.Countries.Add(aCountry)
        '    '        Next
        '    '    End If
        '    'End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Creators
        '    If FilteredOptions.bMainCreators Then
        '        If Result.CreatedBy IsNot Nothing Then
        '            For Each aCreator As TMDbLib.Objects.TvShows.CreatedBy In Result.CreatedBy
        '                nTVShow.Creators.Add(aCreator.Name)
        '            Next
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Genres
        '    If FilteredOptions.bMainGenres Then
        '        Dim aGenres As List(Of TMDbLib.Objects.General.Genre) = Nothing
        '        If Result.Genres Is Nothing OrElse (Result.Genres IsNot Nothing AndAlso Result.Genres.Count = 0) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.Genres IsNot Nothing AndAlso ResultE.Genres.Count > 0 Then
        '                aGenres = ResultE.Genres
        '            End If
        '        Else
        '            aGenres = Result.Genres
        '        End If

        '        If aGenres IsNot Nothing Then
        '            For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
        '                nTVShow.Genres.Add(tGenre.Name)
        '            Next
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'OriginalTitle
        '    If FilteredOptions.bMainOriginalTitle Then
        '        If Result.OriginalName Is Nothing OrElse (Result.OriginalName IsNot Nothing AndAlso String.IsNullOrEmpty(Result.OriginalName)) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.OriginalName) Then
        '                nTVShow.OriginalTitle = ResultE.OriginalName
        '            End If
        '        Else
        '            nTVShow.OriginalTitle = ResultE.OriginalName
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Plot
        '    If FilteredOptions.bMainPlot Then
        '        If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
        '                nTVShow.Plot = ResultE.Overview
        '            End If
        '        Else
        '            nTVShow.Plot = Result.Overview
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Posters (only for SearchResult dialog, auto fallback to "en" by TMDB)
        '    If GetPoster Then
        '        If Result.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.PosterPath) Then
        '            _urlPoster = String.Concat(_client.Config.Images.BaseUrl, "w92", Result.PosterPath)
        '        Else
        '            _urlPoster = String.Empty
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Premiered
        '    If FilteredOptions.bMainPremiered Then
        '        Dim ScrapedDate As String = String.Empty
        '        If Result.FirstAirDate Is Nothing OrElse (Result.FirstAirDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.FirstAirDate))) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.FirstAirDate)) Then
        '                ScrapedDate = CStr(ResultE.FirstAirDate)
        '            End If
        '        Else
        '            ScrapedDate = CStr(Result.FirstAirDate)
        '        End If
        '        If Not String.IsNullOrEmpty(ScrapedDate) Then
        '            Dim RelDate As Date
        '            If Date.TryParse(ScrapedDate, RelDate) Then
        '                'always save date in same date format not depending on users language setting!
        '                nTVShow.Premiered = RelDate.ToString("yyyy-MM-dd")
        '            Else
        '                nTVShow.Premiered = ScrapedDate
        '            End If
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Rating
        '    If FilteredOptions.bMainRating Then
        '        nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "themoviedb", .Value = Result.VoteAverage, .Votes = Result.VoteCount})
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Runtime
        '    If FilteredOptions.bMainRuntime Then
        '        If Result.EpisodeRunTime Is Nothing OrElse Result.EpisodeRunTime.Count = 0 Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.EpisodeRunTime IsNot Nothing AndAlso ResultE.EpisodeRunTime.Count > 0 Then
        '                nTVShow.Runtime = CStr(ResultE.EpisodeRunTime.Item(0))
        '            End If
        '        Else
        '            nTVShow.Runtime = CStr(Result.EpisodeRunTime.Item(0))
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Status
        '    If FilteredOptions.bMainStatus Then
        '        If Result.Status Is Nothing OrElse (Result.Status IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Status)) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.Status IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Status) Then
        '                nTVShow.Status = ResultE.Status
        '            End If
        '        Else
        '            nTVShow.Status = Result.Status
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Studios
        '    If FilteredOptions.bMainStudios Then
        '        If Result.Networks IsNot Nothing AndAlso Result.Networks.Count > 0 Then
        '            For Each aStudio In Result.Networks
        '                nTVShow.Studios.Add(aStudio.Name)
        '            Next
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Title
        '    If FilteredOptions.bMainTitle Then
        '        If Result.Name Is Nothing OrElse (Result.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Name)) Then
        '            If _addonSettings.FallBackEng AndAlso ResultE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Name) Then
        '                nTVShow.Title = ResultE.Name
        '            End If
        '        Else
        '            nTVShow.Title = Result.Name
        '        End If
        '    End If

        '    If bwOMDb.CancellationPending Then Return Nothing

        '    'Seasons and Episodes
        '    If ScrapeModifiers.withEpisodes OrElse ScrapeModifiers.withSeasons Then
        '        For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In Result.Seasons
        '            GetInfo_TVSeason(nTVShow, Result.Id, aSeason.SeasonNumber, ScrapeModifiers, FilteredOptions)
        '        Next
        '    End If
        'Else
        '    Return Nothing
        'End If

        Return nTVShow
    End Function

    Public Function GetInfo_TVEpisode(ByVal ShowID As Integer, ByVal Aired As String, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
        'Dim nTVEpisode As New MediaContainers.EpisodeDetails
        'Dim ShowInfo As TMDbLib.Objects.TvShows.TvShow

        'Dim showAPIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
        'showAPIResult = Task.Run(Function() _client.GetTvShowAsync(ShowID))

        'ShowInfo = showAPIResult.Result

        'For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In ShowInfo.Seasons
        '    Dim seasonAPIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
        '    seasonAPIResult = Task.Run(Function() _client.GetTvSeasonAsync(ShowID, aSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        '    Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = seasonAPIResult.Result
        '    Dim EpisodeList As IEnumerable(Of TMDbLib.Objects.Search.TvSeasonEpisode) = SeasonInfo.Episodes.Where(Function(f) CBool(f.AirDate = CDate(Aired)))
        '    If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
        '        Return GetInfo_TVEpisode(ShowID, EpisodeList(0).SeasonNumber, EpisodeList(0).EpisodeNumber, FilteredOptions)
        '    ElseIf EpisodeList.Count > 0 Then
        '        Return Nothing
        '    End If
        'Next

        Return Nothing
    End Function

    'Public Function GetInfo_TVEpisode(ByVal tmdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
    '    Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvEpisode)
    '    APIResult = Task.Run(Function() _client.GetTvEpisodeAsync(tmdbID, SeasonNumber, EpisodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))

    '    If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
    '        Dim EpisodeInfo As TMDbLib.Objects.TvShows.TvEpisode = APIResult.Result

    '        If EpisodeInfo Is Nothing OrElse EpisodeInfo.Id Is Nothing OrElse Not EpisodeInfo.Id > 0 Then
    '            logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", tmdbID, SeasonNumber, EpisodeNumber))
    '            Return Nothing
    '        End If

    '        Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(EpisodeInfo, FilteredOptions)
    '        Return nEpisode
    '    Else
    '        logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", tmdbID, SeasonNumber, EpisodeNumber))
    '        Return Nothing
    '    End If
    'End Function

    Public Function GetInfo_TVEpisode(ByRef EpisodeInfo As OMDbSharp.EpisodeDetails, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
        Dim nTVEpisode As New MediaContainers.EpisodeDetails

        'nTVEpisode.Scrapersource = "TMDB"

        ''IDs
        'nTVEpisode.TMDB = CStr(EpisodeInfo.Id)
        'If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.TvdbId IsNot Nothing Then nTVEpisode.TVDB = CStr(EpisodeInfo.ExternalIds.TvdbId)
        'If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.ImdbId IsNot Nothing Then nTVEpisode.IMDB = EpisodeInfo.ExternalIds.ImdbId

        ''Episode # Standard
        'If EpisodeInfo.EpisodeNumber >= 0 Then
        '    nTVEpisode.Episode = EpisodeInfo.EpisodeNumber
        'End If

        ''Season # Standard
        'If EpisodeInfo.SeasonNumber >= 0 Then
        '    nTVEpisode.Season = CInt(EpisodeInfo.SeasonNumber)
        'End If

        ''Cast (Actors)
        'If FilteredOptions.bEpisodeActors Then
        '    If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Cast IsNot Nothing Then
        '        For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.Credits.Cast
        '            Dim nUniqueID As New MediaContainers.Uniqueid With {
        '                .Type = "tmdb",
        '                .Value = aCast.Id.ToString}
        '            nTVEpisode.Actors.Add(New MediaContainers.Person With {
        '                                  .Name = aCast.Name,
        '                                  .Role = aCast.Character,
        '                                  .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
        '                                  .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
        '                                  })
        '        Next
        '    End If
        'End If

        ''Aired
        'If FilteredOptions.bEpisodeAired Then
        '    If EpisodeInfo.AirDate IsNot Nothing Then
        '        Dim ScrapedDate As String = CStr(EpisodeInfo.AirDate)
        '        If Not String.IsNullOrEmpty(ScrapedDate) AndAlso Not ScrapedDate = "00:00:00" Then
        '            Dim RelDate As Date
        '            If Date.TryParse(ScrapedDate, RelDate) Then
        '                'always save date in same date format not depending on users language setting!
        '                nTVEpisode.Aired = RelDate.ToString("yyyy-MM-dd")
        '            Else
        '                nTVEpisode.Aired = ScrapedDate
        '            End If
        '        End If
        '    End If
        'End If

        ''Director / Writer
        'If FilteredOptions.bEpisodeCredits OrElse FilteredOptions.bEpisodeDirectors Then
        '    If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Crew IsNot Nothing Then
        '        For Each aCrew As TMDbLib.Objects.General.Crew In EpisodeInfo.Credits.Crew
        '            If FilteredOptions.bEpisodeCredits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
        '                nTVEpisode.Credits.Add(aCrew.Name)
        '            End If
        '            If FilteredOptions.bEpisodeDirectors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
        '                nTVEpisode.Directors.Add(aCrew.Name)
        '            End If
        '        Next
        '    End If
        'End If

        ''Guest Stars
        'If FilteredOptions.bEpisodeGuestStars Then
        '    If EpisodeInfo.GuestStars IsNot Nothing Then
        '        For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.GuestStars
        '            Dim nUniqueID As New MediaContainers.Uniqueid With {
        '                .Type = "tmdb",
        '                .Value = aCast.Id.ToString}
        '            nTVEpisode.GuestStars.Add(New MediaContainers.Person With {
        '                                      .Name = aCast.Name,
        '                                      .Role = aCast.Character,
        '                                      .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_client.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
        '                                      .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
        '                                      })
        '        Next
        '    End If
        'End If

        ''Plot
        'If FilteredOptions.bEpisodePlot Then
        '    If EpisodeInfo.Overview IsNot Nothing Then
        '        nTVEpisode.Plot = EpisodeInfo.Overview
        '    End If
        'End If

        ''Rating
        'If FilteredOptions.bMainRating Then
        '    nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "themoviedb", .Value = EpisodeInfo.VoteAverage, .Votes = EpisodeInfo.VoteCount})
        'End If

        ''ThumbPoster
        'If EpisodeInfo.StillPath IsNot Nothing Then
        '    nTVEpisode.ThumbPoster.URLOriginal = _client.Config.Images.BaseUrl & "original" & EpisodeInfo.StillPath
        '    nTVEpisode.ThumbPoster.URLThumb = _client.Config.Images.BaseUrl & "w185" & EpisodeInfo.StillPath
        'End If

        ''Title
        'If FilteredOptions.bEpisodeTitle Then
        '    If EpisodeInfo.Name IsNot Nothing Then
        '        nTVEpisode.Title = EpisodeInfo.Name
        '    End If
        'End If

        Return nTVEpisode
    End Function

    Public Sub GetInfo_TVSeason(ByRef nTVShow As MediaContainers.TVShow, ByVal ShowID As Integer, ByVal SeasonNumber As Integer, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions)
        'Dim nSeason As New MediaContainers.SeasonDetails

        'Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
        'APIResult = Task.Run(Function() _client.GetTvSeasonAsync(ShowID, SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        'If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
        '    Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

        '    nSeason.TMDB = CStr(SeasonInfo.Id)
        '    If SeasonInfo.ExternalIds IsNot Nothing AndAlso SeasonInfo.ExternalIds.TvdbId IsNot Nothing Then nSeason.TVDB = CStr(SeasonInfo.ExternalIds.TvdbId)

        '    If ScrapeModifiers.withSeasons Then

        '        'Aired
        '        If FilteredOptions.bSeasonAired Then
        '            If SeasonInfo.AirDate IsNot Nothing Then
        '                Dim ScrapedDate As String = CStr(SeasonInfo.AirDate)
        '                If Not String.IsNullOrEmpty(ScrapedDate) Then
        '                    Dim RelDate As Date
        '                    If Date.TryParse(ScrapedDate, RelDate) Then
        '                        'always save date in same date format not depending on users language setting!
        '                        nSeason.Aired = RelDate.ToString("yyyy-MM-dd")
        '                    Else
        '                        nSeason.Aired = ScrapedDate
        '                    End If
        '                End If
        '            End If
        '        End If

        '        'Plot
        '        If FilteredOptions.bSeasonPlot Then
        '            If SeasonInfo.Overview IsNot Nothing Then
        '                nSeason.Plot = SeasonInfo.Overview
        '            End If
        '        End If

        '        'Season #
        '        If SeasonInfo.SeasonNumber >= 0 Then
        '            nSeason.Season = SeasonInfo.SeasonNumber
        '        End If

        '        'Title
        '        If SeasonInfo.Name IsNot Nothing Then
        '            nSeason.Title = SeasonInfo.Name
        '        End If

        '        nTVShow.KnownSeasons.Add(nSeason)
        '    End If

        '    If ScrapeModifiers.withEpisodes AndAlso SeasonInfo.Episodes IsNot Nothing Then
        '        For Each aEpisode As TMDbLib.Objects.Search.TvSeasonEpisode In SeasonInfo.Episodes
        '            nTVShow.KnownEpisodes.Add(GetInfo_TVEpisode(ShowID, aEpisode.SeasonNumber, aEpisode.EpisodeNumber, FilteredOptions))
        '        Next
        '    End If
        'Else
        '    logger.Error(String.Format("Can't scrape or season not found: ShowID={0}, Season={1}", ShowID, SeasonNumber))
        'End If
    End Sub

    'Public Function GetInfo_TVSeason(ByVal tmdbID As Integer, ByVal SeasonNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
    '    Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
    '    APIResult = Task.Run(Function() _client.GetTvSeasonAsync(tmdbID, SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

    '    If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
    '        Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

    '        If SeasonInfo Is Nothing OrElse SeasonInfo.Id Is Nothing OrElse Not SeasonInfo.Id > 0 Then
    '            logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", tmdbID, SeasonNumber))
    '            Return Nothing
    '        End If

    '        Dim nTVSeason As MediaContainers.SeasonDetails = GetInfo_TVSeason(SeasonInfo, FilteredOptions)
    '        Return nTVSeason
    '    Else
    '        logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", tmdbID, SeasonNumber))
    '        Return Nothing
    '    End If
    'End Function

    Public Function GetInfo_TVSeason(ByRef SeasonInfo As OMDbSharp.SeasonDetails, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.SeasonDetails
        Dim nTVSeason As New MediaContainers.SeasonDetails

        'nTVSeason.Scrapersource = "TMDB"

        ''IDs
        'nTVSeason.TMDB = CStr(SeasonInfo.Id)
        'If SeasonInfo.ExternalIds IsNot Nothing AndAlso SeasonInfo.ExternalIds.TvdbId IsNot Nothing Then nTVSeason.TVDB = CStr(SeasonInfo.ExternalIds.TvdbId)

        ''Season #
        'If SeasonInfo.SeasonNumber >= 0 Then
        '    nTVSeason.Season = SeasonInfo.SeasonNumber
        'End If

        ''Aired
        'If FilteredOptions.bSeasonAired Then
        '    If SeasonInfo.AirDate IsNot Nothing Then
        '        Dim ScrapedDate As String = CStr(SeasonInfo.AirDate)
        '        If Not String.IsNullOrEmpty(ScrapedDate) Then
        '            Dim RelDate As Date
        '            If Date.TryParse(ScrapedDate, RelDate) Then
        '                'always save date in same date format not depending on users language setting!
        '                nTVSeason.Aired = RelDate.ToString("yyyy-MM-dd")
        '            Else
        '                nTVSeason.Aired = ScrapedDate
        '            End If
        '        End If
        '    End If
        'End If

        ''Plot
        'If FilteredOptions.bSeasonPlot Then
        '    If SeasonInfo.Overview IsNot Nothing Then
        '        nTVSeason.Plot = SeasonInfo.Overview
        '    End If
        'End If

        ''Title
        'If FilteredOptions.bSeasonTitle Then
        '    If SeasonInfo.Name IsNot Nothing Then
        '        nTVSeason.Title = SeasonInfo.Name
        '    End If
        'End If

        Return nTVSeason
    End Function

    Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
        If String.IsNullOrEmpty(strID) Then Return New List(Of String)

        Dim alStudio As New List(Of String)
        'Dim Movie As TMDbLib.Objects.Movies.Movie = Nothing

        'Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie) = Nothing

        'If strID.ToLower.StartsWith("tt") Then
        '    APIResult = Task.Run(Function() _client.GetMovieAsync(strID))
        'ElseIf Integer.TryParse(strID, 0) Then
        '    APIResult = Task.Run(Function() _client.GetMovieAsync(CInt(strID)))
        'End If

        'If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing Then
        '    Movie = APIResult.Result
        'End If

        'If Movie IsNot Nothing AndAlso Movie.ProductionCompanies IsNot Nothing AndAlso Movie.ProductionCompanies.Count > 0 Then
        '    For Each cStudio In Movie.ProductionCompanies
        '        alStudio.Add(cStudio.Name)
        '    Next
        'End If

        Return alStudio
    End Function

    Public Function GetSearchMovieInfo(ByVal strMovieName As String, ByRef oDBMovie As Database.DBElement, ByVal eType As Enums.ScrapeType, ByVal FilteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
        Dim r As SearchResults_Movie = SearchMovie(strMovieName, oDBMovie.Movie.Year)

        Select Case eType
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
                Else
                    Using dlgSearch As New dlgSearchResults_Movie(_AddonSettings, Me)
                        If dlgSearch.ShowDialog(r, strMovieName, oDBMovie.FileItem.FirstPathFromStack) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_Movie(dlgSearch.Result.UniqueIDs.TMDbId, FilteredOptions, False)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If r.Matches.Count = 1 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If r.Matches.Count > 0 Then
                    Return GetInfo_Movie(r.Matches.Item(0).UniqueIDs.TMDbId, FilteredOptions, False)
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
                    Using dlgSearch As New dlgSearchResults_TV(_AddonSettings, Me)
                        If dlgSearch.ShowDialog(r, strShowName, oDBTV.ShowPath) = DialogResult.OK Then
                            If Not String.IsNullOrEmpty(dlgSearch.Result.UniqueIDs.TMDbId) Then
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
        If Not bwOMDb.IsBusy Then
            bwOMDb.WorkerReportsProgress = False
            bwOMDb.WorkerSupportsCancellation = True
            bwOMDb.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_Movie,
                  .Parameter = tmdbID, .ScrapeOptions = FilteredOptions})
        End If
    End Sub

    Public Sub GetSearchTVShowInfoAsync(ByVal tmdbID As String, ByRef FilteredOptions As Structures.ScrapeOptions)
        If Not bwOMDb.IsBusy Then
            bwOMDb.WorkerReportsProgress = False
            bwOMDb.WorkerSupportsCancellation = True
            bwOMDb.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow,
                  .Parameter = tmdbID, .ScrapeOptions = FilteredOptions})
        End If
    End Sub

    Public Sub SearchAsync_Movie(ByVal title As String, ByRef filterOptions As Structures.ScrapeOptions, ByVal year As Integer)
        If Not bwOMDb.IsBusy Then
            bwOMDb.WorkerReportsProgress = False
            bwOMDb.WorkerSupportsCancellation = True
            bwOMDb.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies,
                  .Parameter = title, .ScrapeOptions = filterOptions, .Year = year})
        End If
    End Sub

    Public Sub SearchAsync_TVShow(ByVal sShow As String, ByRef filterOptions As Structures.ScrapeOptions)

        If Not bwOMDb.IsBusy Then
            bwOMDb.WorkerReportsProgress = False
            bwOMDb.WorkerSupportsCancellation = True
            bwOMDb.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows,
                  .Parameter = sShow, .ScrapeOptions = filterOptions})
        End If
    End Sub

    Private Function SearchMovie(ByVal title As String, ByVal year As Integer) As SearchResults_Movie
        If String.IsNullOrEmpty(title) Then Return New SearchResults_Movie

        Dim R As New SearchResults_Movie
        Dim Movies As OMDbSharp.ItemList

        Dim APIResult As Task(Of OMDbSharp.ItemList)
        APIResult = Task.Run(Function() _Client.GetItemList(title))

        Movies = APIResult.Result

        If Movies.Search IsNot Nothing AndAlso Movies.Search.Count > 0 Then
            Dim lstResults As IEnumerable(Of OMDbSharp.Search)
            If year > 0 Then
                lstResults = Movies.Search.Where(Function(f) f.Type = "movie" And f.Year = year.ToString)
            Else
                lstResults = Movies.Search.Where(Function(f) f.Type = "movie")
            End If
            If lstResults IsNot Nothing Then
                For Each aMovie In lstResults
                    Dim tPlot As String = String.Empty
                    Dim tThumbPoster As MediaContainers.Image = New MediaContainers.Image
                    Dim strTitle As String = String.Empty
                    Dim iYear As Integer

                    Integer.TryParse(aMovie.Year, iYear)
                    If Not String.IsNullOrEmpty(aMovie.Title) Then strTitle = aMovie.Title

                    Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie With {
                        .Plot = tPlot,
                        .Title = strTitle,
                        .ThumbPoster = tThumbPoster,
                        .Year = iYear}
                    lNewMovie.UniqueIDs.Items.Add(New MediaContainers.Uniqueid With {
                                                  .Type = "imdb",
                                                  .Value = aMovie.imdbID})
                    R.Matches.Add(lNewMovie)
                Next
            End If
        End If

        Return R
    End Function

    Private Function SearchTVShow(ByVal tvshow As String) As SearchResults_TVShow
        If String.IsNullOrEmpty(tvshow) Then Return New SearchResults_TVShow

        Dim R As New SearchResults_TVShow
        'Dim Page As Integer = 1
        'Dim Shows As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv)
        'Dim TotP As Integer
        'Dim aE As Boolean

        'Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv))
        'APIResult = Task.Run(Function() _client.SearchTvShowAsync(strShow, Page))

        'Shows = APIResult.Result

        'If Shows.TotalResults = 0 AndAlso _addonSettings.FallBackEng Then
        '    APIResult = Task.Run(Function() _clientE.SearchTvShowAsync(strShow, Page))
        '    Shows = APIResult.Result
        '    aE = True
        'End If

        'If Shows.TotalResults > 0 Then
        '    Dim t1 As String = String.Empty
        '    Dim t2 As String = String.Empty
        '    TotP = Shows.TotalPages
        '    While Page <= TotP AndAlso Page <= 3
        '        If Shows.Results IsNot Nothing Then
        '            For Each aShow In Shows.Results
        '                If aShow.Name Is Nothing OrElse (aShow.Name IsNot Nothing AndAlso String.IsNullOrEmpty(aShow.Name)) Then
        '                    If aShow.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(aShow.OriginalName) Then
        '                        t1 = aShow.OriginalName
        '                    End If
        '                Else
        '                    t1 = aShow.Name
        '                End If
        '                If aShow.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(aShow.FirstAirDate)) Then
        '                    t2 = CStr(aShow.FirstAirDate.Value.Year)
        '                End If
        '                Dim lNewShow As MediaContainers.TVShow = New MediaContainers.TVShow(String.Empty, t1, t2)
        '                lNewShow.TMDB = CStr(aShow.Id)
        '                R.Matches.Add(lNewShow)
        '            Next
        '        End If
        '        Page = Page + 1
        '        If aE Then
        '            APIResult = Task.Run(Function() _clientE.SearchTvShowAsync(strShow, Page))
        '            Shows = APIResult.Result
        '        Else
        '            APIResult = Task.Run(Function() _client.SearchTvShowAsync(strShow, Page))
        '            Shows = APIResult.Result
        '        End If
        '    End While
        'End If

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