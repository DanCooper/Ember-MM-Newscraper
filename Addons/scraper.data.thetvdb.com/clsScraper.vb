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
Imports TvDbSharper

Public Class Scraper
    Implements Interfaces.IScraper_Search

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _client As TvDbClient  'preferred language
    Private _clientE As TvDbClient 'english language
    Private _addonSettings As Addon.AddonSettings

    Friend WithEvents _backgroundWorker As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

    Public Property DefaultLanguage As String

    Public ReadOnly Property IsClientCreated As Boolean
        Get
            Return _client IsNot Nothing AndAlso _client.AuthToken IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property SearchResults_Movie() As New List(Of MediaContainers.Movie) Implements Interfaces.IScraper_Search.SearchResults_Movie

    Public ReadOnly Property SearchResults_Movieset() As New List(Of MediaContainers.Movieset) Implements Interfaces.IScraper_Search.SearchResults_Movieset

    Public ReadOnly Property SearchResult_TVShow() As New List(Of MediaContainers.TVShow) Implements Interfaces.IScraper_Search.SearchResult_TVShow

#End Region 'Properties

#Region "Enumerations"
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/Artwork%20Types/getAllArtworkTypes
    ''' </summary>
    Private Enum ArtworkType As Integer
        ActorThumb = 13
        CompanyIcon = 19
        EpisodePoster = 11
        EpisodePoster_4_3 = 12
        MovieBanner = 16
        MovieCinemagraph = 21
        MovieClearArt = 24
        MovieClearLogo = 25
        MovieFanart = 15
        MovieIcon = 18
        MoviePoster = 14
        SeasonBanner = 6
        SeasonFanart = 8
        SeasonIcon = 10
        SeasonPoster = 7
        TVShowBanner = 1
        TVShowCinemagraph = 20
        TVShowClearArt = 22
        TVShowClearLogo = 23
        TVShowFanart = 3
        TVShowIcon = 5
        TVShowPoster = 2
    End Enum
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/Entity%20Types/getEntityTypes
    ''' </summary>
    Private Enum EntityType As Integer
        Artwork = 6
        Character = 7
        Company = 8
        Episode = 3
        List = 9
        Movie = 4
        Person = 5
        Season = 2
        Series = 1
    End Enum
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/People%20Types/getAllPeopleTypes
    ''' </summary>
    Private Enum PeopleTypes As Integer
        Actor = 3
        Crew = 5
        Creator = 6
        Director = 1
        Executive_Producer = 11
        Guest_Star = 4
        Host = 10
        Musical_Guest = 9
        Producer = 7
        Showrunner = 8
        Writer = 2
    End Enum
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/Seasons/getSeasonTypes
    ''' </summary>
    Private Enum SeasonType As Integer
        Aired = 1
        DVD = 2
        Absolute = 3
        Alternate = 4
        Regional = 5
        AlternateDVD = 6
    End Enum
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/Series%20Statuses/getAllSeriesStatuses
    ''' </summary>
    Private Enum SeriesStatuses As Integer
        Continuing = 1
        Ended = 2
        Upcoming = 3
    End Enum
    ''' <summary>
    ''' https://thetvdb.github.io/v4-api/#/Source%20Types/getAllSourceTypes
    ''' </summary>
    Private Enum SourceTypes As Integer
        TVDb = 1
        IMDb = 2
        Zap2It = 3
        Official_Website = 4
        Facebook = 5
        Twitter = 6
        Reddit = 7
        Fan_Site = 8
        Instagram = 9
        TMDb = 10
        YouTube = 11
        TMDb_TV = 12
        EIDR = 13
        EIDR_Party = 14
        TMDb_Person = 15
        IMDb_Person = 16
        IMDb_Company = 17
    End Enum

    Private Enum TaskType As Integer
        GetInfo_Movie
        GetInfo_Movieset
        GetInfo_TVShow
        Search_By_Title_Movie
        Search_By_Title_Movieset
        Search_By_Title_TVShow
        Search_By_UniqueId_Movie
        Search_By_UniqueId_Movieset
        Search_By_UniqueId_TVShow
    End Enum

    Private Enum TranslationType As Integer
        Aliaas
        Name
        Overview
    End Enum

#End Region 'Enumerations

#Region "Events"

    Public Event GetInfoFinished_Movie(ByVal mainInfo As MediaContainers.Movie) Implements Interfaces.IScraper_Search.GetInfoFinished_Movie
    Public Event GetInfoFinished_Movieset(ByVal mainInfo As MediaContainers.Movieset) Implements Interfaces.IScraper_Search.GetInfoFinished_Movieset
    Public Event GetInfoFinished_TVShow(ByVal mainInfo As MediaContainers.TVShow) Implements Interfaces.IScraper_Search.GetInfoFinished_TVShow

    Public Event SearchFinished_Movie(ByVal searchResults As List(Of MediaContainers.Movie)) Implements Interfaces.IScraper_Search.SearchFinished_Movie
    Public Event SearchFinished_Movieset(ByVal searchResults As List(Of MediaContainers.Movieset)) Implements Interfaces.IScraper_Search.SearchFinished_Movieset
    Public Event SearchFinished_TVShow(ByVal searchResults As List(Of MediaContainers.TVShow)) Implements Interfaces.IScraper_Search.SearchFinished_TVShow

#End Region 'Events

#Region "Methods"

    Public Async Function CreateAPI(ByVal addonSettings As Addon.AddonSettings) As Task
        Try
            _addonSettings = addonSettings

            If Not String.IsNullOrEmpty(_addonSettings.ApiPin) Then
                _client = New TvDbClient
                Await _client.Login(Addon._strApiKey, _addonSettings.ApiPin)
                '_client.MaxRetryCount = 2
                _Logger.Trace("[TVDbv4_Data] [CreateAPI] Client created")

                If _addonSettings.FallBackEng Then
                    '_clientE = New TMDbLib.Client.TMDbClient(_addonSettings.APIKey)
                    'Await _clientE.GetConfigAsync()
                    '_clientE.DefaultLanguage = "en-US"
                    '_clientE.MaxRetryCount = 2
                    _Logger.Trace("[TVDbv4_Data] [CreateAPI] Client-EN created")
                Else
                    _clientE = _client
                    _Logger.Trace("[TVDbv4_Data] [CreateAPI] Client-EN = Client")
                End If
            Else
                _Logger.Warn(String.Format("[TVDbv4_Data] [CreateAPI] [Warn] No API Pin available"))
            End If
        Catch ex As Exception
            _client.AuthToken = Nothing
            _Logger.Error(String.Format("[TVDbv4_Data] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Private Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles _backgroundWorker.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Select Case Args.TaskType
            Case TaskType.GetInfo_Movie
                Dim intTvdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTvdbId) Then
                    e.Result = New Results With {
                        .Result = GetInfo_Movie(intTvdbId, Args.ScrapeOptions),
                        .TaskType = Args.TaskType
                    }
                Else
                    e.Result = New Results With {
                        .Result = Nothing,
                        .TaskType = Args.TaskType
                    }
                End If

            Case TaskType.GetInfo_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_TVShow
                Dim intTvdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTvdbId) Then
                    e.Result = New Results With {
                        .Result = GetInfo_TVShow(intTvdbId, Args.ScrapeOptions, Args.ScrapeModifiers),
                        .TaskType = Args.TaskType
                    }
                Else
                    e.Result = New Results With {
                        .Result = Nothing,
                        .TaskType = Args.TaskType
                    }
                End If

            Case TaskType.Search_By_Title_Movie
                e.Result = New Results With {
                    .Result = Search_Movie(Args.Parameter, Args.Year),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_TVShow
                e.Result = New Results With {
                    .Result = Search_TVShow(Args.Parameter),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_UniqueId_Movie
                e.Result = New Results With {
                    .Result = Search_Movie_By_Unique_Id(Args.Parameter),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_UniqueId_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_UniqueId_TVShow
                e.Result = New Results With {
                    .Result = Search_TVShow(Args.Parameter),
                    .TaskType = Args.TaskType
                }
        End Select
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _backgroundWorker.RunWorkerCompleted
        Dim Result As Results = DirectCast(e.Result, Results)

        Select Case Result.TaskType
            Case TaskType.GetInfo_Movie
                RaiseEvent GetInfoFinished_Movie(DirectCast(Result.Result, MediaContainers.Movie))

            Case TaskType.GetInfo_Movieset
                RaiseEvent GetInfoFinished_Movieset(DirectCast(Result.Result, MediaContainers.Movieset))

            Case TaskType.GetInfo_TVShow
                RaiseEvent GetInfoFinished_TVShow(DirectCast(Result.Result, MediaContainers.TVShow))

            Case TaskType.Search_By_Title_Movie, TaskType.Search_By_UniqueId_Movie
                RaiseEvent SearchFinished_Movie(DirectCast(Result.Result, List(Of MediaContainers.Movie)))

            Case TaskType.Search_By_Title_Movieset, TaskType.Search_By_UniqueId_Movieset
                RaiseEvent SearchFinished_Movieset(DirectCast(Result.Result, List(Of MediaContainers.Movieset)))

            Case TaskType.Search_By_Title_TVShow, TaskType.Search_By_UniqueId_TVShow
                RaiseEvent SearchFinished_TVShow(DirectCast(Result.Result, List(Of MediaContainers.TVShow)))
        End Select
    End Sub

    Public Sub CancelAsync() Implements Interfaces.IScraper_Search.CancelAsync
        If _backgroundWorker.IsBusy Then _backgroundWorker.CancelAsync()

        While _backgroundWorker.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Function Api_GetEpisodes_By_Aired(ByVal showId As Integer, ByVal aired As String) As EpisodeBaseRecordDto()
        Try
            Dim ApiParameters As New SeriesEpisodesOptionalParams With {.AirDate = aired}
            Dim ApiResponse = Task.Run(Function() _client.SeriesEpisodes(showId, "default", ApiParameters))
            Return ApiResponse.Result.Data.Episodes
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return Nothing
    End Function

    Private Function Api_GetEpisodes_By_SeasonType(ByVal showId As Integer, ByVal seasonType As SeasonType) As EpisodeBaseRecordDto()
        Try
            Dim ApiResponse = Task.Run(Function() _client.SeriesEpisodes(showId, seasonType.ToString))
            Return ApiResponse.Result.Data.Episodes
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return Nothing
    End Function

    Private Function Api_Search_By_Title(ByVal title As String, year As Integer, ByVal type As EntityType) As SearchResultDto()
        Try
            Dim ApiParameters As New SearchOptionalParams With {.Query = title, .Type = type.ToString, .Year = year}
            Dim ApiResponse = Task.Run(Function() _client.Search(ApiParameters))
            Return ApiResponse.Result.Data
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return Nothing
    End Function

    Private Function Api_Search_By_Remote_Id(ByVal remoteId As String, ByVal type As EntityType) As SearchResultDto()
        Try
            Dim ApiParameters As New SearchOptionalParams With {.Remote_id = remoteId}
            Dim ApiResponse = Task.Run(Function() _client.Search(ApiParameters))
            Return ApiResponse.Result.Data
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Private Function GetAll_Episodes_By_SeasonType(ByVal showId As Integer, ByVal seasonType As SeasonType) As List(Of MediaContainers.EpisodeDetails)
        If Not showId > 0 Then Return New List(Of MediaContainers.EpisodeDetails)
        Dim Result As New List(Of MediaContainers.EpisodeDetails)

        Dim ApiResult = Api_GetEpisodes_By_SeasonType(showId, seasonType)
        If ApiResult IsNot Nothing Then

        End If

        Return Result
    End Function

    Public Function GetInfo_Movie(ByVal uniqueId As String,
                                  ByVal filteredOptions As Structures.ScrapeOptions
                                  ) As MediaContainers.Movie Implements Interfaces.IScraper_Search.GetInfo_Movie
        Dim intId As Integer
        If Integer.TryParse(uniqueId, intId) Then Return GetInfo_Movie(intId, filteredOptions)
        Return Nothing
    End Function
    ''' <summary>
    '''  Scrape movie details by TVDb ID
    ''' </summary>
    ''' <param name="tvdbId">TVDb ID</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_Movie(ByVal tvdbId As Integer,
                                  ByVal filteredOptions As Structures.ScrapeOptions
                                  ) As MediaContainers.Movie
        If tvdbId < 0 Then Return Nothing

        Dim ApiResponse As Task(Of TvDbApiResponse(Of MovieExtendedRecordDto))
        Dim parameters As New MovieExtendedOptionalParams With {.Meta = "translations"}

        ApiResponse = Task.Run(Function() _client.MovieExtended(tvdbId, parameters))

        If ApiResponse Is Nothing OrElse
            ApiResponse.Status = TaskStatus.Faulted OrElse
            ApiResponse.Exception IsNot Nothing OrElse
            ApiResponse.Result Is Nothing OrElse
            ApiResponse.Result.Data Is Nothing OrElse
            Not ApiResponse.Result.Data.Id > 0 Then
            _Logger.Error(String.Format("Can't scrape or movie not found by ID [0]", tvdbId))
            Return Nothing
        End If

        Dim ApiResult As MovieExtendedRecordDto = ApiResponse.Result.Data
        Dim nResult As New MediaContainers.Movie With {.Scrapersource = "TVDb"}

        'IDs
        nResult.UniqueIDs.TVDbId = CInt(ApiResult.Id)
        If ApiResult.RemoteIds IsNot Nothing Then
            Dim IMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.IMDb)
            If IMDbId IsNot Nothing Then nResult.UniqueIDs.IMDbId = IMDbId.Id
            Dim TMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.TMDb)
            If TMDbId IsNot Nothing AndAlso Integer.TryParse(TMDbId.Id, 0) Then nResult.UniqueIDs.TMDbId = CInt(TMDbId.Id)
        End If

        'Actors
        If filteredOptions.bMainActors Then nResult.Actors = Parse_Characters(ApiResult.Characters, PeopleTypes.Actor)

        'Certifications
        If filteredOptions.bMainCertifications Then nResult.Certifications = Parse_Certifications(ApiResult.ContentRatings)

        'Countries
        If filteredOptions.bMainCountries Then nResult.Countries = Parse_ProductionCountries(ApiResult.ProductionCountries)

        'Directors
        If filteredOptions.bMainDirectors Then nResult.Directors = Parse_Directors(ApiResult.Characters)

        'Genres
        If filteredOptions.bMainGenres Then nResult.Genres = Parse_Genres(ApiResult.Genres)

        'OriginalTitle
        If filteredOptions.bMainOriginalTitle Then nResult.OriginalTitle = ApiResult.Name

        'Plot
        If filteredOptions.bMainPlot Then nResult.Plot = Parse_Plot(ApiResult.Translations)

        'Poster (used only for SearchResults dialog) 
        nResult.ThumbPoster = Parse_ThumbPoster(ApiResult.Image)

        'Premiered
        If filteredOptions.bMainPremiered Then nResult.Premiered = Parse_Premiered(ApiResult.Releases)

        'Runtime
        If filteredOptions.bMainRuntime Then nResult.Runtime = Parse_Runtime(CInt(ApiResult.Runtime))

        'Studios
        If filteredOptions.bMainStudios Then nResult.Studios = Parse_Studios(ApiResult.Studios)

        'Tagline
        If filteredOptions.bMainTagline Then nResult.Tagline = Parse_Tagline(ApiResult.Translations)

        'Title
        If filteredOptions.bMainTitle Then nResult.Title = Parse_Title(ApiResult.Translations, ApiResult.Name)

        'Trailer
        If filteredOptions.bMainTrailer Then nResult.Trailer = Parse_Trailers(ApiResult.Trailers)

        'Writers
        If filteredOptions.bMainWriters Then nResult.Credits = Parse_Writers(ApiResult.Characters)

        If _backgroundWorker.CancellationPending Then Return Nothing
        Return nResult
    End Function

    Public Function GetInfo_Movie_By_IMDbId(ByVal imdbId As String, ByVal filteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
        Return Nothing
    End Function

    Public Function GetInfo_Movie_By_TMDbId(ByVal id As Integer, ByVal filteredOptions As Structures.ScrapeOptions) As MediaContainers.Movie
        Return Nothing
    End Function

    Public Function GetInfo_Movieset(ByVal uniqueId As String,
                                     ByVal filteredOptions As Structures.ScrapeOptions
                                     ) As MediaContainers.Movieset Implements Interfaces.IScraper_Search.GetInfo_Movieset
        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal tvdbId As Integer,
                                      ByVal filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.EpisodeDetails
        If tvdbId = -1 Then Return Nothing

        If _backgroundWorker.CancellationPending Then Return Nothing

        Dim ApiResponse As Task(Of TvDbApiResponse(Of EpisodeExtendedRecordDto))
        Dim parameters As New EpisodeExtendedOptionalParams With {.Meta = "translations"}

        ApiResponse = Task.Run(Function() _client.EpisodeExtended(tvdbId, parameters))

        If ApiResponse Is Nothing OrElse
            ApiResponse.Status = TaskStatus.Faulted OrElse
            ApiResponse.Exception IsNot Nothing OrElse
            ApiResponse.Result Is Nothing OrElse
            ApiResponse.Result.Data Is Nothing OrElse
            Not ApiResponse.Result.Data.Id > 0 Then
            _Logger.Error(String.Format("Can't scrape or episode not found by ID [0]", tvdbId))
            Return Nothing
        End If

        Dim ApiResult As EpisodeExtendedRecordDto = ApiResponse.Result.Data
        Dim nResult As New MediaContainers.EpisodeDetails With {.Scrapersource = "TVDb"}

        'IDs
        nResult.UniqueIDs.TVDbId = CInt(ApiResult.Id)
        If ApiResult.RemoteIds IsNot Nothing Then
            Dim IMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.IMDb)
            If IMDbId IsNot Nothing Then nResult.UniqueIDs.IMDbId = IMDbId.Id
            Dim TMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = 12)
            If TMDbId IsNot Nothing AndAlso Integer.TryParse(TMDbId.Id, 0) Then nResult.UniqueIDs.TMDbId = CInt(TMDbId.Id)
        End If

        'Episode # Absolute
        'nResult.EpisodeAbsolute = Parse_Numbers(ApiResult.Seasons)

        'Episode # AirsBeforeEpisode (DisplayEpisode)
        If ApiResult.AirsBeforeEpisode IsNot Nothing Then
            nResult.DisplayEpisode = CInt(ApiResult.AirsBeforeEpisode)
        End If

        'Episode # DVD
        'nResult.EpisodeDVD = ApiResult.Number

        'Episode # Standard
        nResult.Episode = ApiResult.Number

        'Season # AirsBeforeSeason (DisplaySeason)
        If ApiResult.AirsBeforeSeason IsNot Nothing Then
            nResult.DisplaySeason = CInt(ApiResult.AirsBeforeSeason)
        End If

        'Season # AirsAfterSeason (DisplaySeason, DisplayEpisode; Special handling like in Kodi)
        If ApiResult.AirsAfterSeason IsNot Nothing Then
            nResult.DisplaySeason = CInt(ApiResult.AirsAfterSeason)
            nResult.DisplayEpisode = 4096
        End If

        'Season # Standard
        nResult.Season = ApiResult.SeasonNumber

        'Actors
        If filteredOptions.bEpisodeActors Then nResult.Actors = Parse_Characters(ApiResult.Characters, PeopleTypes.Actor)

        'Aired
        If filteredOptions.bEpisodeAired Then nResult.Aired = Parse_Aired(ApiResult.Aired)

        'Directors
        If filteredOptions.bEpisodeDirectors Then nResult.Directors = Parse_Directors(ApiResult.Characters)

        'Guest Stars
        If filteredOptions.bEpisodeGuestStars Then nResult.GuestStars = Parse_Characters(ApiResult.Characters, PeopleTypes.Guest_Star)

        'OriginalTitle
        If filteredOptions.bEpisodeOriginalTitle Then nResult.OriginalTitle = ApiResult.Name

        'Plot
        'TODO: workaround until "translations" has been fixed in TVDbSharper
        If filteredOptions.bEpisodePlot Then nResult.Plot = GetTranslation(CInt(ApiResult.Id), TranslationType.Overview, Enums.ContentType.TVEpisode, ApiResult.Overview)
        'If filteredOptions.bEpisodePlot Then nResult.Plot = Parse_Plot(ApiResult.Translations)

        'Poster (used only for SearchResults dialog)
        nResult.ThumbPoster = Parse_ThumbPoster(ApiResult.Image)

        'Title
        'TODO: workaround until "translations" has been fixed in TVDbSharper
        If filteredOptions.bEpisodeTitle Then nResult.Title = GetTranslation(CInt(ApiResult.Id), TranslationType.Name, Enums.ContentType.TVEpisode, ApiResult.Name)
        'If filteredOptions.bEpisodeTitle Then nResult.Title = Parse_Title(ApiResult.Translations, ApiResult.Name)

        'Writers
        If filteredOptions.bEpisodeCredits Then nResult.Credits = Parse_Writers(ApiResult.Characters)

        If _backgroundWorker.CancellationPending Then Return Nothing
        Return nResult
    End Function

    Private Function GetId_By_Aired_TVEpisode(ByVal showId As Integer, ByVal aired As String) As Integer
        If Not showId > 0 Then Return -1
        Dim Result As Integer = -1

        Dim ApiResult = Api_GetEpisodes_By_Aired(showId, aired)
        If ApiResult IsNot Nothing Then
            If ApiResult.Count = 1 Then
                Return CInt(ApiResult(0).Id)
            ElseIf ApiResult.Count > 1 Then
                _Logger.Warn(String.Format("More than one episode with aired date ""{0}"" was found, the episode could not be clearly identified.", aired))
            Else
                _Logger.Error(String.Format("No episode with aired date ""{0}"" was found.", aired))
            End If
        End If

        Return Result
    End Function

    Public Function GetInfo_TVEpisode_By_Aired(ByVal showId As Integer,
                                               ByVal aired As String,
                                               ByVal filteredOptions As Structures.ScrapeOptions
                                               ) As MediaContainers.EpisodeDetails
        If showId = -1 Then Return Nothing
        Return GetInfo_TVEpisode(GetId_By_Aired_TVEpisode(showId, aired), filteredOptions)
    End Function

    Public Sub GetInfo_TVSeason(ByRef tvShow As MediaContainers.TVShow,
                                ByVal tvdbId As Integer,
                                ByVal filteredOptions As Structures.ScrapeOptions,
                                ByRef scrapeModifiers As Structures.ScrapeModifiers
                                )
        If tvdbId = -1 Then Return
        If _backgroundWorker.CancellationPending Then Return

        Dim ApiResponse As Task(Of TvDbApiResponse(Of SeasonExtendedRecordDto))

        ApiResponse = Task.Run(Function() _client.SeasonExtended(tvdbId))

        Try
            If ApiResponse Is Nothing OrElse
                ApiResponse.Status = TaskStatus.Faulted OrElse
                ApiResponse.Exception IsNot Nothing OrElse
                ApiResponse.Result Is Nothing OrElse
                ApiResponse.Result.Data Is Nothing OrElse
                Not ApiResponse.Result.Data.Id > 0 Then
                _Logger.Error(String.Format("Can't scrape or season not found by ID [0]", tvdbId))
                Return
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return
        End Try

        Dim ApiResult As SeasonExtendedRecordDto = ApiResponse.Result.Data
        If scrapeModifiers.withSeasons Then
            Dim nResult As New MediaContainers.SeasonDetails With {.Scrapersource = "TVDb"}

            'ID
            nResult.UniqueIDs.TVDbId = CInt(ApiResult.Id)

            'Season #
            nResult.Season = CInt(ApiResult.Number)

            'Aired
            If filteredOptions.bSeasonAired Then nResult.Aired = Parse_Aired(ApiResult.Episodes)

            'Plot
            If filteredOptions.bSeasonPlot Then nResult.Plot = GetTranslation(CInt(ApiResult.Id), TranslationType.Overview, Enums.ContentType.TVSeason)

            'Title
            If filteredOptions.bSeasonTitle Then nResult.Title = GetTranslation(CInt(ApiResult.Id), TranslationType.Name, Enums.ContentType.TVSeason, ApiResult.Name)

            tvShow.KnownSeasons.Add(nResult)
        End If

        If scrapeModifiers.withEpisodes AndAlso ApiResult.Episodes IsNot Nothing Then
            For Each aEpisode In ApiResult.Episodes
                tvShow.KnownEpisodes.Add(GetInfo_TVEpisode(CInt(aEpisode.Id), filteredOptions))
                If _backgroundWorker.CancellationPending Then Return
            Next
        End If
    End Sub

    Public Function GetInfo_TVShow(ByVal uniqueId As String,
                                   ByVal filteredOptions As Structures.ScrapeOptions,
                                   ByVal scrapeModifiers As Structures.ScrapeModifiers
                                   ) As MediaContainers.TVShow Implements Interfaces.IScraper_Search.GetInfo_TVShow
        Dim intId As Integer
        If Integer.TryParse(uniqueId, intId) Then Return GetInfo_TVShow(intId, filteredOptions, scrapeModifiers)
        Return Nothing
    End Function

    Public Function GetInfo_TVShow(ByVal tvdbId As Integer,
                                   ByRef filteredOptions As Structures.ScrapeOptions,
                                   ByRef scrapeModifiers As Structures.ScrapeModifiers
                                   ) As MediaContainers.TVShow
        If tvdbId = -1 Then Return Nothing

        If _backgroundWorker.CancellationPending Then Return Nothing

        Dim ApiResponse As Task(Of TvDbApiResponse(Of SeriesExtendedRecordDto))
        Dim parameters As New SeriesExtendedOptionalParams With {.Meta = "translations"}

        ApiResponse = Task.Run(Function() _client.SeriesExtended(tvdbId, parameters))

        If ApiResponse Is Nothing OrElse
            ApiResponse.Status = TaskStatus.Faulted OrElse
            ApiResponse.Exception IsNot Nothing OrElse
            ApiResponse.Result Is Nothing OrElse
            ApiResponse.Result.Data Is Nothing OrElse
            Not ApiResponse.Result.Data.Id > 0 Then
            _Logger.Error(String.Format("Can't scrape or tv show not found by ID [0]", tvdbId))
            Return Nothing
        End If

        Dim ApiResult As SeriesExtendedRecordDto = ApiResponse.Result.Data
        Dim nResult As New MediaContainers.TVShow With {.Scrapersource = "TVDb"}

        'IDs
        nResult.UniqueIDs.TVDbId = CInt(ApiResult.Id)
        If ApiResult.RemoteIds IsNot Nothing Then
            Dim IMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.IMDb)
            If IMDbId IsNot Nothing Then nResult.UniqueIDs.IMDbId = IMDbId.Id
            Dim TMDbId = ApiResult.RemoteIds.FirstOrDefault(Function(f) f.Type = 12)
            If TMDbId IsNot Nothing AndAlso Integer.TryParse(TMDbId.Id, 0) Then nResult.UniqueIDs.TMDbId = CInt(TMDbId.Id)
        End If

        'Actors
        If filteredOptions.bMainActors Then nResult.Actors = Parse_Characters(ApiResult.Characters, PeopleTypes.Actor)

        'Certifications
        If filteredOptions.bMainCertifications Then nResult.Certifications = Parse_Certifications(ApiResult.ContentRatings)

        'Countries
        If filteredOptions.bMainCountries Then nResult.Countries = Parse_Country(ApiResult.Country)

        'Creators
        If filteredOptions.bMainCreators Then nResult.Creators = Parse_Creators(ApiResult.Characters)

        'Directors
        If filteredOptions.bMainDirectors Then nResult.Directors = Parse_Directors(ApiResult.Characters)

        'Genres
        If filteredOptions.bMainGenres Then nResult.Genres = Parse_Genres(ApiResult.Genres)

        'OriginalTitle
        If filteredOptions.bMainOriginalTitle Then nResult.OriginalTitle = ApiResult.Name

        'Plot
        If filteredOptions.bMainPlot Then nResult.Plot = Parse_Plot(ApiResult.Translations)

        'Poster (used only for SearchResults dialog)
        nResult.ThumbPoster = Parse_ThumbPoster(ApiResult.Image)

        'Premiered
        If filteredOptions.bMainPremiered Then nResult.Premiered = Parse_FirstAired(ApiResult.FirstAired)

        'Runtime
        If filteredOptions.bMainRuntime Then nResult.Runtime = Parse_Runtime(ApiResult.AverageRuntime)

        'Status
        If filteredOptions.bMainStatus Then nResult.Status = Parse_Status(ApiResult.Status)

        'Studios
        If filteredOptions.bMainStudios Then nResult.Studios = Parse_Studios(ApiResult.OriginalNetwork)

        'Tagline
        If filteredOptions.bMainTagline Then nResult.Tagline = Parse_Tagline(ApiResult.Translations)

        'Title
        If filteredOptions.bMainTitle Then nResult.Title = Parse_Title(ApiResult.Translations, ApiResult.Name)

        'Seasons and Episodes
        If scrapeModifiers.withEpisodes OrElse scrapeModifiers.withSeasons Then
            For Each aSeason As SeasonBaseRecordDto In ApiResult.Seasons.Where(Function(f) f.Type.Id = SeasonType.Aired)
                GetInfo_TVSeason(nResult, CInt(aSeason.Id), filteredOptions, scrapeModifiers)
                If _backgroundWorker.CancellationPending Then Return Nothing
            Next
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing
        Return nResult
    End Function

    Public Sub GetInfoAsync_Movie(ByVal tvdbId As String,
                                  ByRef filteredOptions As Structures.ScrapeOptions
                                  ) Implements Interfaces.IScraper_Search.GetInfoAsync_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_Movie,
                                             .Parameter = tvdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Sub GetInfoAsync_Movieset(ByVal tvdbId As String,
                                     ByRef filteredOptions As Structures.ScrapeOptions
                                     ) Implements Interfaces.IScraper_Search.GetInfoAsync_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_Movieset,
                                             .Parameter = tvdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Sub GetInfoAsync_TVShow(ByVal tvdbId As String,
                                   ByRef filteredOptions As Structures.ScrapeOptions
                                   ) Implements Interfaces.IScraper_Search.GetInfoAsync_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.GetInfo_TVShow,
                                             .Parameter = tvdbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Private Function GetTranslation(ByVal id As Integer, ByVal translationType As TranslationType, ByVal contentType As Enums.ContentType, Optional fallbackValue As String = "") As String
        Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
        If alpha3 IsNot Nothing Then
            Try
                Dim ApiResponse As Task(Of TvDbApiResponse(Of TranslationDto)) = Nothing
                Select Case contentType
                    Case Enums.ContentType.Movie
                        ApiResponse = Task.Run(Function() _client.MovieTranslation(id, alpha3))
                    Case Enums.ContentType.TVEpisode
                        ApiResponse = Task.Run(Function() _client.EpisodeTranslation(id, alpha3))
                    Case Enums.ContentType.TVSeason
                        ApiResponse = Task.Run(Function() _client.SeasonTranslation(id, alpha3))
                    Case Enums.ContentType.TVShow
                        ApiResponse = Task.Run(Function() _client.SeriesTranslation(id, alpha3))
                End Select
                If ApiResponse IsNot Nothing Then
                    Dim ApiResult = ApiResponse.Result.Data
                    If ApiResult IsNot Nothing Then
                        Select Case translationType
                            Case TranslationType.Name
                                Return If(Not String.IsNullOrEmpty(ApiResult.Name), ApiResult.Name, fallbackValue)
                            Case TranslationType.Overview
                                Return If(Not String.IsNullOrEmpty(ApiResult.Overview), ApiResult.Overview, fallbackValue)
                        End Select
                    End If
                End If
            Catch ex As Exception
                _Logger.Trace(String.Format("No {0} translation found for ID {1} with language {2}", translationType.ToString, id, alpha3))
            End Try
        Else
            _Logger.Trace(String.Format("No Alpha3 code found for Alpha2 Code {0}", DefaultLanguage))
        End If
        Return fallbackValue
    End Function

    Private Function Parse_Aired(ByRef aired As String) As String
        If Not String.IsNullOrEmpty(aired) Then Return aired
        Return String.Empty
    End Function

    Private Function Parse_Aired(ByRef episodes As EpisodeBaseRecordDto()) As String
        If episodes IsNot Nothing AndAlso episodes.Count > 0 Then Return Parse_Aired(episodes(0).Aired)
        Return String.Empty
    End Function

    Private Function Parse_CharacterImage(ByRef character As CharacterDto) As String
        Dim baseImageUrl = "https://artworks.thetvdb.com"
        If Not String.IsNullOrEmpty(character.Image) Then
            Return character.Image
        ElseIf Not String.IsNullOrEmpty(character.PersonImgURL) Then
            Return String.Concat(baseImageUrl, character.PersonImgURL)
        End If
        Return String.Empty
    End Function

    Private Function Parse_Characters(ByRef characters As CharacterDto(), ByVal type As PeopleTypes) As List(Of MediaContainers.Person)
        Dim Result As New List(Of MediaContainers.Person)
        If characters IsNot Nothing Then
            For Each person As CharacterDto In characters.Where(Function(f) f.Type = type).OrderBy(Function(o) o.Sort)
                Result.Add(New MediaContainers.Person With {
                           .Name = person.PersonName,
                           .Role = If(Not String.IsNullOrEmpty(person.Name), person.Name, person.PeopleType),
                           .URLOriginal = Parse_CharacterImage(person),
                           .TVDbId = person.PeopleId.ToString
                           })
            Next
        End If
        Return Result
    End Function

    Private Function Parse_Certifications(ByRef contentRatings As ContentRatingDto()) As List(Of String)
        Dim Result As New List(Of String)
        If contentRatings IsNot Nothing AndAlso contentRatings.Count > 0 Then
            For Each rating In contentRatings
                If Not String.IsNullOrEmpty(rating.Name) Then
                    Dim Country = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha3 = rating.Country.ToUpper)
                    If Country IsNot Nothing AndAlso Not String.IsNullOrEmpty(Country.Name) Then
                        Result.Add(String.Concat(Country.Name, ":", rating.Name))
                    Else
                        _Logger.Warn("Unhandled certification country encountered: {0}", rating.Country.ToUpper)
                    End If
                End If
            Next
        End If
        Return Result
    End Function

    Private Function Parse_Country(ByRef country As String) As List(Of String)
        If Not String.IsNullOrEmpty(country) Then Return New List(Of String) From {country}
        Return New List(Of String)
    End Function

    Private Function Parse_Creators(ByRef characters As CharacterDto()) As List(Of String)
        Return Parse_Characters(characters, PeopleTypes.Creator).Select(Function(f) f.Name).ToList
    End Function

    Private Function Parse_Directors(ByRef characters As CharacterDto()) As List(Of String)
        Return Parse_Characters(characters, PeopleTypes.Director).Select(Function(f) f.Name).ToList
    End Function

    Private Function Parse_FirstAired(ByVal firstAired As String) As String
        If Not String.IsNullOrEmpty(firstAired) Then
            Dim Premiered As Date
            If Date.TryParse(firstAired, Premiered) Then
                'always save date in same date format not depending on users language setting!
                Return Premiered.ToString("yyyy-MM-dd")
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_Genres(ByRef genres As GenreBaseRecordDto()) As List(Of String)
        Dim Result As New List(Of String)
        If genres IsNot Nothing AndAlso genres.Count > 0 Then
            Result.AddRange(genres.Select(Function(f) f.Name))
        End If
        Return Result
    End Function

    Private Function Parse_Plot(ByRef translations As TranslationExtendedDto) As String
        If translations IsNot Nothing AndAlso translations.OverviewTranslations IsNot Nothing Then
            Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
            If alpha3 IsNot Nothing Then
                Dim translation = translations.OverviewTranslations.FirstOrDefault(Function(f) f.Language = alpha3)
                If translation IsNot Nothing AndAlso Not String.IsNullOrEmpty(translation.Overview) Then
                    Return translation.Overview
                End If
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_Premiered(ByVal releases As ReleaseDto()) As String
        If releases IsNot Nothing AndAlso releases.Count > 0 Then
            Dim release = releases.FirstOrDefault(Function(f) f.Country = "global")
            If release IsNot Nothing AndAlso Not String.IsNullOrEmpty(release.Date) Then
                Dim Premiered As Date
                If Date.TryParse(release.Date, Premiered) Then
                    'always save date in same date format not depending on users language setting!
                    Return Premiered.ToString("yyyy-MM-dd")
                End If
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_ProductionCountries(ByRef productionCountries As ProductionCountryDto()) As List(Of String)
        Dim result As New List(Of String)
        If productionCountries IsNot Nothing AndAlso productionCountries.Count > 0 Then
            For Each country As ProductionCountryDto In productionCountries
                result.Add(country.Name)
            Next
        End If
        Return result
    End Function

    Private Function Parse_Runtime(ByRef runtime As Integer) As String
        If runtime > 0 Then
            Return runtime.ToString
        End If
        Return String.Empty
    End Function

    Private Function Parse_Status(ByRef status As StatusDto) As String
        If status IsNot Nothing Then Return status.Name
        Return String.Empty
    End Function

    Private Function Parse_Studios(ByRef studios As StudioBaseRecordDto()) As List(Of String)
        If studios IsNot Nothing AndAlso studios.Count > 0 Then
            Return studios.Select(Function(f) f.Name).ToList
        End If
        Return New List(Of String)
    End Function

    Private Function Parse_Studios(ByRef originalNetwork As CompanyDto) As List(Of String)
        If originalNetwork IsNot Nothing AndAlso Not String.IsNullOrEmpty(originalNetwork.Name) Then Return New List(Of String) From {originalNetwork.Name}
        Return New List(Of String)
    End Function

    Private Function Parse_Tagline(ByRef translations As TranslationExtendedDto) As String
        If translations IsNot Nothing AndAlso translations.OverviewTranslations IsNot Nothing Then
            Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
            If alpha3 IsNot Nothing Then
                Dim translation = translations.OverviewTranslations.FirstOrDefault(Function(f) f.Language = alpha3)
                If translation IsNot Nothing AndAlso Not String.IsNullOrEmpty(translation.Tagline) Then
                    Return translation.Tagline
                End If
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_ThumbPoster(ByRef url As String) As MediaContainers.Image
        Return New MediaContainers.Image With {.URLOriginal = url, .URLThumb = url}
    End Function

    Private Function Parse_Title(ByRef translations As TranslationExtendedDto, ByVal originalName As String) As String
        If translations IsNot Nothing AndAlso translations.NameTranslations.Count > 0 Then
            Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
            If alpha3 IsNot Nothing Then
                Dim translation = translations.NameTranslations.FirstOrDefault(Function(f) f.Language = alpha3)
                If translation IsNot Nothing AndAlso Not String.IsNullOrEmpty(translation.Name) Then
                    Return translation.Name
                End If
            End If
        End If
        Return originalName
    End Function

    Private Function Parse_Title(ByRef nameTranslations As String(), ByVal originalName As String) As String
        If nameTranslations IsNot Nothing AndAlso nameTranslations.Count > 0 Then
            Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
            If alpha3 IsNot Nothing Then
                'Dim translation = nameTranslations.FirstOrDefault(Function(f) f.Language = alpha3)
                'If translation IsNot Nothing AndAlso Not String.IsNullOrEmpty(translation.Name) Then
                '    Return translation.Name
                'End If
            End If
        End If
        Return String.Empty
    End Function

    Private Function Parse_Trailers(ByRef trailers As TrailerDto()) As String
        If trailers IsNot Nothing Then
            'search a trailer with preffered language
            For Each aTrailer In trailers.Where(Function(f) f.Language = Localization.Languages.Get_Alpha3_B_By_Alpha2(DefaultLanguage))
                Dim nTrailer = YouTube.Scraper.GetVideoDetails(aTrailer.Url)
                If nTrailer IsNot Nothing Then
                    Return nTrailer.UrlForNfo
                    Exit For
                End If
            Next
            For Each aTrailer In trailers
                Dim nTrailer = YouTube.Scraper.GetVideoDetails(aTrailer.Url)
                If nTrailer IsNot Nothing Then
                    Return nTrailer.UrlForNfo
                    Exit For
                End If
            Next
        End If
        Return String.Empty
    End Function

    Private Function Parse_Writers(ByRef characters As CharacterDto()) As List(Of String)
        Return Parse_Characters(characters, PeopleTypes.Writer).Select(Function(f) f.Name).ToList
    End Function

    Public Function Process_SearchResults_Movie(ByVal title As String,
                                                ByRef oDbElement As Database.DBElement,
                                                ByVal type As Enums.ScrapeType,
                                                ByVal filteredOptions As Structures.ScrapeOptions
                                                ) As MediaContainers.Movie
        Dim SearchResults = Search_Movie(title, CInt(If(oDbElement.Movie.YearSpecified, oDbElement.Movie.Year, Nothing)))

        Select Case type
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tvdb", New List(Of String) From {"TVDb"}, Enums.ContentType.Movie)
                        If dlgSearch.ShowDialog(title, oDbElement.Filename, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result_Movie.UniqueIDs.TVDbIdSpecified Then
                                Return GetInfo_Movie(dlgSearch.Result_Movie.UniqueIDs.TVDbId, filteredOptions)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If SearchResults.Count > 0 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions)
                End If
        End Select

        Return Nothing
    End Function

    Public Function Process_SearchResults_TVShow(ByVal title As String,
                                                 ByRef oDbElement As Database.DBElement,
                                                 ByVal type As Enums.ScrapeType,
                                                 ByVal filteredOptions As Structures.ScrapeOptions,
                                                 ByRef scrapeModifiers As Structures.ScrapeModifiers
                                                 ) As MediaContainers.TVShow
        Dim SearchResults = Search_TVShow(title)

        Select Case type
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions, scrapeModifiers)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tvdb", New List(Of String) From {"TVDb", "IMDb"}, Enums.ContentType.TVShow)
                        If dlgSearch.ShowDialog(title, oDbElement.Filename, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result_TVShow.UniqueIDs.TVDbIdSpecified Then
                                Return GetInfo_TVShow(dlgSearch.Result_TVShow.UniqueIDs.TVDbId, filteredOptions, scrapeModifiers)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions, scrapeModifiers)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If SearchResults.Count > 0 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId, filteredOptions, scrapeModifiers)
                End If
        End Select

        Return Nothing
    End Function

    Private Function Search_Movie(ByVal title As String, Optional ByVal year As Integer = 0) As List(Of MediaContainers.Movie)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.Movie)

        Dim ApiResult = Api_Search_By_Title(title, year, EntityType.Movie)
        If ApiResult Is Nothing Then Return New List(Of MediaContainers.Movie)

        Dim SearchResults As New List(Of MediaContainers.Movie)

        If ApiResult.Count > 0 Then
            For Each item As SearchResultDto In ApiResult
                Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
                Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                Dim strOriginalTitle As String = String.Empty
                Dim strPlot As String = String.Empty
                Dim strTitle As String = String.Empty
                Dim strYear As String = String.Empty

                If item.Name IsNot Nothing Then strOriginalTitle = item.Name
                If item.Thumbnail IsNot Nothing Then
                    imgThumbPoster.URLOriginal = item.Thumbnail
                    imgThumbPoster.URLThumb = item.Thumbnail
                End If
                If item.Year IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Year) Then strYear = item.Year

                'IDs
                Dim nIds As New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {
                    .TVDbId = CInt(item.TvdbId)
                }
                If item.RemoteIds IsNot Nothing Then
                    Dim IMDbId = item.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.IMDb)
                    If IMDbId IsNot Nothing Then nIds.IMDbId = IMDbId.Id
                    Dim TMDbId = item.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.TMDb)
                    If TMDbId IsNot Nothing AndAlso Integer.TryParse(TMDbId.Id, 0) Then nIds.TMDbId = CInt(TMDbId.Id)
                End If

                'Plot
                If alpha3 IsNot Nothing AndAlso item.Overviews IsNot Nothing Then
                    Dim kvpTranslation = item.Overviews.FirstOrDefault(Function(f) f.Key = alpha3)
                    If Not String.IsNullOrEmpty(kvpTranslation.Value) Then
                        strPlot = kvpTranslation.Value
                    ElseIf item.Overview IsNot Nothing Then
                        strPlot = item.Overview
                    End If
                End If

                'Title
                If alpha3 IsNot Nothing AndAlso item.Translations IsNot Nothing Then
                    Dim kvpTitle = item.Translations.FirstOrDefault(Function(f) f.Key = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage))
                    If kvpTitle.Value IsNot Nothing Then
                        strTitle = kvpTitle.Value
                    End If
                End If

                SearchResults.Add(New MediaContainers.Movie With {
                                  .Directors = If(item.Director IsNot Nothing, New List(Of String) From {item.Director}, New List(Of String)),
                                  .Genres = If(item.Genres IsNot Nothing, item.Genres.ToList, New List(Of String)),
                                  .OriginalTitle = strOriginalTitle,
                                  .Plot = strPlot,
                                  .Title = If(Not String.IsNullOrEmpty(strTitle), strTitle, strOriginalTitle),
                                  .ThumbPoster = imgThumbPoster,
                                  .UniqueIDs = nIds,
                                  .Year = strYear
                                  })
            Next
        End If

        Return SearchResults
    End Function

    Private Function Search_Movie_By_Unique_Id(ByVal title As String, Optional ByVal year As Integer = 0) As List(Of MediaContainers.Movie)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.Movie)

        Dim ApiResult = Api_Search_By_Remote_Id(title, EntityType.Movie)
        If ApiResult Is Nothing Then Return New List(Of MediaContainers.Movie)

        Dim SearchResults As New List(Of MediaContainers.Movie)

        If ApiResult.Count > 0 Then
            For Each item As SearchResultDto In ApiResult
                Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
                Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                Dim strOriginalTitle As String = String.Empty
                Dim strPlot As String = String.Empty
                Dim strTitle As String = String.Empty
                Dim strYear As String = String.Empty

                If item.Name IsNot Nothing Then strOriginalTitle = item.Name
                If item.Thumbnail IsNot Nothing Then
                    imgThumbPoster.URLOriginal = item.Thumbnail
                    imgThumbPoster.URLThumb = item.Thumbnail
                End If
                If item.Year IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Year) Then strYear = item.Year

                'IDs
                Dim nIds As New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {
                    .TVDbId = CInt(item.TvdbId)
                }
                If item.RemoteIds IsNot Nothing Then
                    Dim IMDbId = item.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.IMDb)
                    If IMDbId IsNot Nothing Then nIds.IMDbId = IMDbId.Id
                    Dim TMDbId = item.RemoteIds.FirstOrDefault(Function(f) f.Type = SourceTypes.TMDb)
                    If TMDbId IsNot Nothing AndAlso Integer.TryParse(TMDbId.Id, 0) Then nIds.TMDbId = CInt(TMDbId.Id)
                End If

                'Plot
                If alpha3 IsNot Nothing AndAlso item.Overviews IsNot Nothing Then
                    Dim kvpTranslation = item.Overviews.FirstOrDefault(Function(f) f.Key = alpha3)
                    If Not String.IsNullOrEmpty(kvpTranslation.Value) Then
                        strPlot = kvpTranslation.Value
                    ElseIf item.Overview IsNot Nothing Then
                        strPlot = item.Overview
                    End If
                End If

                'Title
                If alpha3 IsNot Nothing AndAlso item.Translations IsNot Nothing Then
                    Dim kvpTitle = item.Translations.FirstOrDefault(Function(f) f.Key = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage))
                    If kvpTitle.Value IsNot Nothing Then
                        strTitle = kvpTitle.Value
                    End If
                End If

                SearchResults.Add(New MediaContainers.Movie With {
                                  .Directors = If(item.Director IsNot Nothing, New List(Of String) From {item.Director}, New List(Of String)),
                                  .Genres = If(item.Genres IsNot Nothing, item.Genres.ToList, New List(Of String)),
                                  .OriginalTitle = strOriginalTitle,
                                  .Plot = strPlot,
                                  .Title = If(Not String.IsNullOrEmpty(strTitle), strTitle, strOriginalTitle),
                                  .ThumbPoster = imgThumbPoster,
                                  .UniqueIDs = nIds,
                                  .Year = strYear
                                  })
            Next
        End If

        Return SearchResults
    End Function

    Private Function Search_TVShow(ByVal title As String, Optional ByVal year As Integer = 0) As List(Of MediaContainers.TVShow)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.TVShow)

        Dim ApiResult = Api_Search_By_Title(title, year, EntityType.Series)
        If ApiResult Is Nothing Then Return New List(Of MediaContainers.TVShow)

        Dim SearchResults As New List(Of MediaContainers.TVShow)

        If ApiResult.Count > 0 Then
            For Each item As SearchResultDto In ApiResult
                Dim alpha3 = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage)
                Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                Dim strOriginalTitle As String = String.Empty
                Dim strPlot As String = String.Empty
                Dim strTitle As String = String.Empty
                Dim strPremiered As String = String.Empty

                If item.Name IsNot Nothing Then strOriginalTitle = item.Name
                If item.Thumbnail IsNot Nothing Then
                    imgThumbPoster.URLOriginal = item.Thumbnail
                    imgThumbPoster.URLThumb = item.Thumbnail
                End If
                If item.Year IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Year) Then strPremiered = item.Year
                'Plot
                If alpha3 IsNot Nothing AndAlso item.Overviews IsNot Nothing Then
                    Dim kvpTranslation = item.Overviews.FirstOrDefault(Function(f) f.Key = alpha3)
                    If Not String.IsNullOrEmpty(kvpTranslation.Value) Then
                        strPlot = kvpTranslation.Value
                    ElseIf item.Overview IsNot Nothing Then
                        strPlot = item.Overview
                    End If
                End If

                If item.FirstAirTime IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.FirstAirTime) Then strPremiered = item.FirstAirTime

                'Title
                If alpha3 IsNot Nothing AndAlso item.Translations IsNot Nothing Then
                    Dim kvpTitle = item.Translations.FirstOrDefault(Function(f) f.Key = Localization.Languages.Get_Alpha3_T_By_Alpha2(DefaultLanguage))
                    If kvpTitle.Value IsNot Nothing Then
                        strTitle = kvpTitle.Value
                    End If
                End If

                SearchResults.Add(New MediaContainers.TVShow With {
                                  .Directors = If(item.Director IsNot Nothing, New List(Of String) From {item.Director}, New List(Of String)),
                                  .Genres = If(item.Genres IsNot Nothing, item.Genres.ToList, New List(Of String)),
                                  .OriginalTitle = strOriginalTitle,
                                  .Plot = strPlot,
                                  .Premiered = strPremiered,
                                  .Title = If(Not String.IsNullOrEmpty(strTitle), strTitle, strOriginalTitle),
                                  .ThumbPoster = imgThumbPoster,
                                  .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.TVDbId = CInt(item.TvdbId)}
                                  })
            Next
        End If

        Return SearchResults
    End Function

    Public Sub SearchAsync_By_Title_Movie(ByVal title As String,
                                          Optional ByVal year As Integer = 0
                                          ) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_Movie,
                                             .Parameter = title,
                                             .Year = year
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_Title_Movieset(ByVal title As String) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_Movieset,
                                             .Parameter = title
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_Title_TVShow(ByVal title As String,
                                           Optional ByVal year As Integer = 0
                                           ) Implements Interfaces.IScraper_Search.SearchAsync_By_Title_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_Title_TVShow,
                                             .Parameter = title,
                                             .Year = year
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_Movie(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_Movie
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_Movie,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_Movieset(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_Movieset
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_Movieset,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId_TVShow(ByVal uniqueId As String) Implements Interfaces.IScraper_Search.SearchAsync_By_UniqueId_TVShow
        If Not _backgroundWorker.IsBusy Then
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType.Search_By_UniqueId_TVShow,
                                             .Parameter = uniqueId
                                             })
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim Parameter As String
        Dim ScrapeModifiers As Structures.ScrapeModifiers
        Dim ScrapeOptions As Structures.ScrapeOptions
        Dim TaskType As TaskType
        Dim Year As Integer

#End Region 'Fields

    End Structure

    Private Structure Results

#Region "Fields"

        Dim Result As Object
        Dim TaskType As TaskType

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class