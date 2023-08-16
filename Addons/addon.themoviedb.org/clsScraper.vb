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

Public Class Scraper
    Implements Interfaces.ISearchResultsDialog

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _ApiKey As String = String.Empty

    Private _TMDbApi As TMDbLib.Client.TMDbClient  'preferred language
    Private _TMDbApiEn As TMDbLib.Client.TMDbClient 'english language
    Private _TMDbApiG As TMDbLib.Client.TMDbClient 'global, no language

    Private _Fallback_Movie As TMDbLib.Objects.Movies.Movie = Nothing
    Private _Fallback_Movieset As TMDbLib.Objects.Collections.Collection = Nothing
    Private _Fallback_TVEpisode As TMDbLib.Objects.TvShows.TvEpisode = Nothing
    Private _Fallback_TVSeason As TMDbLib.Objects.TvShows.TvSeason = Nothing
    Private _Fallback_TVShow As TMDbLib.Objects.TvShows.TvShow = Nothing

    Friend WithEvents _backgroundWorker As New ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property IsClientCreated As Boolean
        Get
            Return _TMDbApi IsNot Nothing
        End Get
    End Property

    Public Property PreferredLanguage As String
        Get
            Return _TMDbApi.DefaultLanguage
        End Get
        Set(value As String)
            _TMDbApi.DefaultLanguage = value
        End Set
    End Property

    Public ReadOnly Property SearchResults() As New List(Of MediaContainers.MainDetails) Implements Interfaces.ISearchResultsDialog.SearchResults

    Public Property ScraperSettings As New Addon.InternalSettings

#End Region 'Properties

#Region "Enumerations"

    Private Enum TaskType
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

#End Region 'Enumerations

#Region "Events"

    Public Event GetInfoFinished(ByVal mainInfo As MediaContainers.MainDetails) Implements Interfaces.ISearchResultsDialog.GetInfoFinished
    Public Event SearchFinished(ByVal searchResults As List(Of MediaContainers.MainDetails)) Implements Interfaces.ISearchResultsDialog.SearchFinished

#End Region 'Events

#Region "Methods"

    Public Async Function CreateAPI(ByVal privateApiKey As String) As Task
        If Not String.IsNullOrEmpty(privateApiKey) Then
            _ApiKey = privateApiKey
        Else
            _ApiKey = My.Resources.ApiKey
        End If

        Try
            _TMDbApi = New TMDbLib.Client.TMDbClient(_ApiKey)
            Await _TMDbApi.GetConfigAsync()
            _TMDbApi.MaxRetryCount = 2
            _Logger.Trace("[TMDb] [CreateAPI] Client-PreferredLanguage created")

            _TMDbApiG = New TMDbLib.Client.TMDbClient(_ApiKey)
            Await _TMDbApiG.GetConfigAsync()
            _TMDbApi.MaxRetryCount = 2
            _Logger.Trace("[TMDb] [CreateAPI] Client-Global created")

            If ScraperSettings.FallBackToEng Then
                _TMDbApiEn = New TMDbLib.Client.TMDbClient(_ApiKey)
                Await _TMDbApiEn.GetConfigAsync()
                _TMDbApiEn.DefaultLanguage = "en-US"
                _TMDbApiEn.MaxRetryCount = 2
                _Logger.Trace("[TMDb] [CreateAPI] Client-en-US created")
            Else
                _TMDbApiEn = _TMDbApi
                _Logger.Trace("[TMDb] [CreateAPI] Client-en-US = Client-PreferredLanguage")
            End If
        Catch ex As Exception
            _Logger.Error(String.Format("[TMDb] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Private Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles _backgroundWorker.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Select Case Args.TaskType
            Case TaskType.GetInfo_Movie
                e.Result = New Results With {
                    .Result = GetInfo_Movie(Args.Parameter, Args.ScrapeModifiers, Args.ScrapeOptions),
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_Movieset
                Dim intTmdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTmdbId) Then
                    e.Result = New Results With {
                        .Result = GetInfo_Movieset(intTmdbId, Args.ScrapeOptions),
                        .TaskType = Args.TaskType
                    }
                Else
                    e.Result = New Results With {
                        .Result = Nothing,
                        .TaskType = Args.TaskType
                    }
                End If

            Case TaskType.GetInfo_TVShow
                Dim intTmdbId As Integer = -1
                If Integer.TryParse(Args.Parameter, intTmdbId) Then
                    e.Result = New Results With {
                        .Result = GetInfo_TVShow(intTmdbId, Args.ScrapeOptions, Args.ScrapeModifiers),
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
                    .Result = Search_By_Title_Movie(Args.Parameter, Args.Year),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_Movieset
                e.Result = New Results With {
                    .Result = Search_By_Title_Movieset(Args.Parameter),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_TVShow
                e.Result = New Results With {
                    .Result = Search_By_Title_TVShow(Args.Parameter),
                    .TaskType = Args.TaskType
                }
        End Select
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _backgroundWorker.RunWorkerCompleted
        Dim Result As Results = DirectCast(e.Result, Results)

        Select Case Result.TaskType
            Case TaskType.GetInfo_Movie
                RaiseEvent GetInfoFinished(DirectCast(Result.Result, MediaContainers.MainDetails))

            Case TaskType.Search_By_Title_Movie, TaskType.Search_By_UniqueId_Movie
                RaiseEvent SearchFinished(DirectCast(Result.Result, List(Of MediaContainers.MainDetails)))
        End Select
    End Sub

    Public Sub CancelAsync() Implements Interfaces.ISearchResultsDialog.CancelAsync
        If _backgroundWorker.IsBusy Then _backgroundWorker.CancelAsync()

        While _backgroundWorker.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Private Function Convert_VideoQuality(ByRef size As Integer) As Enums.VideoResolution
        Select Case size
            Case 1080
                Return Enums.VideoResolution.HD1080p
            Case 720
                Return Enums.VideoResolution.HD720p
            Case 480
                Return Enums.VideoResolution.HQ480p
            Case Else
                Return Enums.VideoResolution.Any
        End Select
    End Function

    Private Function Convert_VideoType(ByRef type As String) As Enums.VideoType
        Select Case type.ToLower
            Case "clip"
                Return Enums.VideoType.Clip
            Case "featurette"
                Return Enums.VideoType.Featurette
            Case "teaser"
                Return Enums.VideoType.Teaser
            Case "trailer"
                Return Enums.VideoType.Trailer
            Case Else
                Return Enums.VideoType.Any
        End Select
    End Function

    Public Function GetCollectionIdByMovieId(ByVal tmdbOrImdbId As String) As Integer
        Dim ApiResponse As Task(Of TMDbLib.Objects.Movies.Movie)
        ApiResponse = Task.Run(Function() _TMDbApi.GetMovieAsync(tmdbOrImdbId))

        Dim Movie = ApiResponse.Result
        If Movie Is Nothing Then Return -1

        If Movie.BelongsToCollection IsNot Nothing AndAlso Movie.BelongsToCollection.Id > 0 Then
            Return Movie.BelongsToCollection.Id
        Else
            Return -1
        End If
    End Function

    Public Sub GetId_Movie(ByVal dbMovie As Database.DBElement)
        Dim strUniqueID As String = String.Empty
        If dbMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            strUniqueID = dbMovie.MainDetails.UniqueIDs.TMDbId.ToString
        ElseIf dbMovie.MainDetails.UniqueIDs.IMDbIdSpecified Then
            strUniqueID = dbMovie.MainDetails.UniqueIDs.IMDbId
        End If

        If Not String.IsNullOrEmpty(strUniqueID) Then
            Dim Movie As TMDbLib.Objects.Movies.Movie
            Dim ApiResponse As Task(Of TMDbLib.Objects.Movies.Movie)
            ApiResponse = Task.Run(Function() _TMDbApi.GetMovieAsync(strUniqueID))

            Movie = ApiResponse.Result
            If Movie Is Nothing OrElse Movie.Id = 0 Then Return

            dbMovie.MainDetails.UniqueIDs.TMDbId = Movie.Id
        End If
    End Sub

    Public Function GetId_Movie(ByVal imdbId As String) As Integer
        Dim ApiResponse As Task(Of TMDbLib.Objects.Movies.Movie)
        ApiResponse = Task.Run(Function() _TMDbApi.GetMovieAsync(imdbId))

        Dim Movie = ApiResponse.Result
        If Movie Is Nothing OrElse Movie.Id = 0 Then Return -1

        Return Movie.Id
    End Function

    Public Function GetImages_Movie_Movieset(ByVal tmdbID As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal type As Enums.ContentType) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim Results As TMDbLib.Objects.General.Images = Nothing
            Dim ApiResponse As Task(Of TMDbLib.Objects.General.ImagesWithId)

            If type = Enums.ContentType.Movie Then
                ApiResponse = Task.Run(Function() _TMDbApiG.GetMovieImagesAsync(tmdbID))
                Results = ApiResponse.Result
            ElseIf type = Enums.ContentType.MovieSet Then
                ApiResponse = Task.Run(Function() _TMDbApiG.GetCollectionImagesAsync(tmdbID))
                Results = ApiResponse.Result
            End If

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'MainFanart
            If scrapeModifiers.Fanart AndAlso Results.Backdrops IsNot Nothing Then
                For Each tImage In Results.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height,
                        .Likes = 0,
                        .Scraper = "TMDB",
                        .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                        .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w300", tImage.FilePath),
                        .VoteAverage = tImage.VoteAverage,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width
                    }

                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster
            If scrapeModifiers.Poster AndAlso Results.Posters IsNot Nothing Then
                For Each tImage In Results.Posters
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height,
                        .Likes = 0,
                        .Scraper = "TMDB",
                        .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                        .URLThumb = _TMDbApiG.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                        .VoteAverage = tImage.VoteAverage,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width
                    }

                    alImagesContainer.MainPosters.Add(newImage)
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVShow(ByVal tmdbID As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
            APIResult = Task.Run(Function() _TMDbApiG.GetTvShowAsync(tmdbID, TMDbLib.Objects.TvShows.TvShowMethods.Images))

            If APIResult Is Nothing Then
                Return alImagesContainer
            End If

            Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result

            'MainFanart
            If scrapeModifiers.Fanart AndAlso Result.Images.Backdrops IsNot Nothing Then
                For Each tImage In Result.Images.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height,
                        .Likes = 0,
                        .Scraper = "TMDB",
                        .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                        .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w300", tImage.FilePath),
                        .VoteAverage = tImage.VoteAverage,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width
                    }

                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster
            If scrapeModifiers.Poster AndAlso Result.Images.Posters IsNot Nothing Then
                For Each tImage In Result.Images.Posters
                    Dim newImage As New MediaContainers.Image With {
                        .Height = tImage.Height,
                        .Likes = 0,
                        .Scraper = "TMDB",
                        .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                        .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w185", tImage.FilePath),
                        .VoteAverage = tImage.VoteAverage,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width
                    }

                    alImagesContainer.MainPosters.Add(newImage)
                Next
            End If

            'SeasonPoster
            If (scrapeModifiers.Seasons.Poster OrElse scrapeModifiers.Episodes.Poster) AndAlso Result.Seasons IsNot Nothing Then
                For Each tSeason In Result.Seasons
                    Dim APIResult_Season As Task(Of TMDbLib.Objects.TvShows.TvSeason)
                    APIResult_Season = Task.Run(Function() _TMDbApiG.GetTvSeasonAsync(CInt(tmdbID), tSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Images))

                    If APIResult_Season IsNot Nothing Then
                        Dim Result_Season As TMDbLib.Objects.TvShows.TvSeason = APIResult_Season.Result

                        'SeasonPoster
                        If scrapeModifiers.Seasons.Poster AndAlso Result_Season.Images.Posters IsNot Nothing Then
                            For Each tImage In Result_Season.Images.Posters
                                Dim newImage As New MediaContainers.Image With {
                                    .Height = tImage.Height,
                                    .Likes = 0,
                                    .Scraper = "TMDB",
                                    .Season = tSeason.SeasonNumber,
                                    .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                    .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                                    .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w185", tImage.FilePath),
                                    .VoteAverage = tImage.VoteAverage,
                                    .VoteCount = tImage.VoteCount,
                                    .Width = tImage.Width
                                }

                                alImagesContainer.SeasonPosters.Add(newImage)
                            Next
                        End If

                        If scrapeModifiers.Episodes.Poster AndAlso Result_Season.Episodes IsNot Nothing Then
                            For Each tEpisode In Result_Season.Episodes

                                'EpisodePoster
                                If scrapeModifiers.Episodes.Poster AndAlso tEpisode.StillPath IsNot Nothing Then

                                    Dim newImage As New MediaContainers.Image With {
                                        .Episode = tEpisode.EpisodeNumber,
                                        .Scraper = "TMDB",
                                        .Season = tEpisode.SeasonNumber,
                                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tEpisode.StillPath),
                                        .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w185", tEpisode.StillPath)
                                    }

                                    alImagesContainer.EpisodePosters.Add(newImage)
                                End If
                            Next
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVEpisode(ByVal showID As Integer, ByVal season As Integer, ByVal episode As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim Results As TMDbLib.Objects.General.StillImages = Nothing
            Dim APIResult As Task(Of TMDbLib.Objects.General.StillImages)
            APIResult = Task.Run(Function() _TMDbApiG.GetTvEpisodeImagesAsync(showID, season, episode))
            Results = APIResult.Result

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'EpisodePoster
            If scrapeModifiers.Episodes.Poster AndAlso Results.Stills IsNot Nothing Then
                For Each tImage In Results.Stills
                    Dim newImage As New MediaContainers.Image With {
                        .Episode = episode,
                        .Height = tImage.Height,
                        .Likes = 0,
                        .Scraper = "TMDB",
                        .Season = season,
                        .LongLanguage = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                        .URLOriginal = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "original", tImage.FilePath),
                        .URLThumb = String.Concat(_TMDbApiG.Config.Images.BaseUrl, "w185", tImage.FilePath),
                        .VoteAverage = tImage.VoteAverage,
                        .VoteCount = tImage.VoteCount,
                        .Width = tImage.Width
                    }

                    alImagesContainer.EpisodePosters.Add(newImage)
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function
    ''' <summary>
    '''  Scrape movie details by TMDb or IMDb ID
    ''' </summary>
    ''' <param name="tmdbOrImdbId">TMDb or IMDb ID</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_Movie(ByVal tmdbOrImdbId As String,
                                  ByVal scrapeModifiers As Structures.ScrapeModifiers,
                                  ByVal scrapeOptions As Structures.ScrapeOptions
                                  ) As Interfaces.AddonResult
        If String.IsNullOrEmpty(tmdbOrImdbId) Then Return Nothing

        _Fallback_Movie = Nothing

        Dim ApiResponse As Task(Of TMDbLib.Objects.Movies.Movie)
        Dim intTMDbId As Integer = -1

        If tmdbOrImdbId.ToLower.StartsWith("tt") Then
            'search movie by IMDB ID
            ApiResponse = Task.Run(Function() _TMDbApi.GetMovieAsync(
                                       tmdbOrImdbId,
                                       TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                       TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                       TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                       TMDbLib.Objects.Movies.MovieMethods.Videos
                                       ))
        ElseIf Integer.TryParse(tmdbOrImdbId, intTMDbId) Then
            'search movie by TMDB ID
            ApiResponse = Task.Run(Function() _TMDbApi.GetMovieAsync(
                                       intTMDbId, TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                       TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                       TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                       TMDbLib.Objects.Movies.MovieMethods.Videos))
        Else
            _Logger.Error(String.Format("Can't scrape or movie not found: [0]", tmdbOrImdbId))
            Return Nothing
        End If

        Try
            If ApiResponse Is Nothing OrElse
                ApiResponse.Result Is Nothing OrElse
                Not ApiResponse.Result.Id > 0 OrElse
                ApiResponse.Exception IsNot Nothing Then
                _Logger.Error(String.Format("Can't scrape or movie not found: [0]", tmdbOrImdbId))
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try

        Dim ApiResult As TMDbLib.Objects.Movies.Movie = ApiResponse.Result
        Dim nMainDetails As New MediaContainers.MainDetails With {.Scrapersource = Addon._AssemblyName}
        Dim nTrailers As New List(Of MediaContainers.MediaFile)

        'IDs
        nMainDetails.UniqueIDs.TMDbId = ApiResult.Id
        If ApiResult.ImdbId IsNot Nothing Then nMainDetails.UniqueIDs.IMDbId = ApiResult.ImdbId

        'Cast (Actors)
        If scrapeOptions.Actors Then nMainDetails.Actors = Parse_Actors(ApiResult.Credits)

        'Certifications
        If scrapeOptions.Certifications Then nMainDetails.Certifications = Parse_Certifications(ApiResult.Releases)

        'Collection ID
        If scrapeOptions.Collection Then
            If ApiResult.BelongsToCollection IsNot Nothing Then
                Dim nFullMovieSetInfo = GetInfo_Movieset(ApiResult.BelongsToCollection.Id,
                                                         New Structures.ScrapeOptions With {
                                                         .Plot = True,
                                                         .Title = True
                                                         })

                If nFullMovieSetInfo IsNot Nothing Then
                    nMainDetails.Sets.Add(New MediaContainers.MoviesetDetails With {
                                    .Plot = nFullMovieSetInfo.Plot,
                                    .Title = nFullMovieSetInfo.Title,
                                    .UniqueIDs = nFullMovieSetInfo.UniqueIDs
                                    })
                    nMainDetails.UniqueIDs.TMDbCollectionId = nFullMovieSetInfo.UniqueIDs.TMDbId
                End If
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Countries
        If scrapeOptions.Countries Then nMainDetails.Countries = Parse_Countries(ApiResult.ProductionCountries)

        'Director / Writer
        If scrapeOptions.Directors OrElse scrapeOptions.Credits Then
            If ApiResult.Credits IsNot Nothing AndAlso ApiResult.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In ApiResult.Credits.Crew
                    If scrapeOptions.Directors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nMainDetails.Directors.Add(aCrew.Name)
                    End If
                    If scrapeOptions.Credits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nMainDetails.Credits.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Genres
        If scrapeOptions.Genres Then
            If ApiResult.Genres.Count > 0 Then
                nMainDetails.Genres.AddRange(ApiResult.Genres.Select(Function(f) f.Name))
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.Genres.Count > 0 Then
                nMainDetails.Genres.AddRange(_Fallback_Movie.Genres.Select(Function(f) f.Name))
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'OriginalTitle
        If scrapeOptions.OriginalTitle Then
            nMainDetails.OriginalTitle = ApiResult.OriginalTitle
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Plot
        If scrapeOptions.Plot Then
            If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                nMainDetails.Plot = ApiResult.Overview
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Overview) Then
                nMainDetails.Plot = _Fallback_Movie.Overview
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Poster (used only for SearchResults dialog, auto fallback to "en" by TMDb)
        If ApiResult.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.PosterPath) Then
            nMainDetails.ThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
            nMainDetails.ThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Premiered
        If scrapeOptions.Premiered Then
            Dim nDate As Date? = Nothing
            If ApiResult.ReleaseDate.HasValue Then
                nDate = ApiResult.ReleaseDate.Value
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.ReleaseDate.HasValue Then
                nDate = _Fallback_Movie.ReleaseDate.Value
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nMainDetails.Premiered = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Rating
        If scrapeOptions.Ratings Then
            nMainDetails.Ratings.Add(New MediaContainers.RatingDetails With {
                               .Max = 10,
                               .Type = "themoviedb",
                               .Value = ApiResult.VoteAverage,
                               .Votes = ApiResult.VoteCount
                               })
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Runtime
        If scrapeOptions.Runtime Then
            If ApiResult.Runtime IsNot Nothing Then
                nMainDetails.Runtime = CStr(ApiResult.Runtime)
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.Runtime IsNot Nothing Then
                nMainDetails.Runtime = CStr(_Fallback_Movie.Runtime)
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Studios
        If scrapeOptions.Studios Then
            If ApiResult.ProductionCompanies.Count > 0 Then
                nMainDetails.Studios.AddRange(ApiResult.ProductionCompanies.Select(Function(f) f.Name))
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Tagline
        If scrapeOptions.Tagline Then
            If ApiResult.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Tagline) Then
                nMainDetails.Tagline = ApiResult.Tagline
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Tagline) Then
                nMainDetails.Tagline = _Fallback_Movie.Tagline
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Title
        If scrapeOptions.Title Then
            If Not String.IsNullOrEmpty(ApiResult.Title) Then
                nMainDetails.Title = ApiResult.Title
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_Movie.Title) Then
                nMainDetails.Title = _Fallback_Movie.Title
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Trailer
        If scrapeOptions.TrailerLink OrElse scrapeModifiers.Trailer Then
            Dim aTrailers As List(Of TMDbLib.Objects.General.Video) = Nothing
            If ApiResult.Videos IsNot Nothing AndAlso ApiResult.Videos.Results.Count > 0 Then
                aTrailers = ApiResult.Videos.Results
            ElseIf RunFallback_Movie(ApiResult.Id) AndAlso _Fallback_Movie.Videos Is Nothing AndAlso _Fallback_Movie.Videos.Results.Count > 0 Then
                aTrailers = _Fallback_Movie.Videos.Results
            End If

            If aTrailers IsNot Nothing Then
                For Each aTrailer In aTrailers
                    Dim nTrailer = YouTube.Scraper.GetVideoDetails(aTrailer.Key)
                    If nTrailer IsNot Nothing Then
                        nMainDetails.Trailer = nTrailer.UrlForNfo
                        Exit For
                    End If
                Next

                If scrapeOptions.TrailerLink Then
                    For Each aVideo In aTrailers.Where(Function(f) f.Site = "YouTube")
                        Dim nTrailer = YouTube.Scraper.GetVideoDetails(aVideo.Key)
                        If nTrailer IsNot Nothing Then
                            nTrailer.Language = aVideo.Iso_639_1
                            nTrailer.Scraper = "TMDb"
                            nTrailer.VideoType = Convert_VideoType(aVideo.Type)
                            nTrailers.Add(nTrailer)
                        End If
                    Next
                End If

            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        _Fallback_Movie = Nothing
        Return New Interfaces.AddonResult With {.ScraperResult_Data = nMainDetails, .ScraperResult_Trailers = nTrailers}
    End Function

    Public Function GetInfo_Movieset(ByVal tmdbId As Integer,
                                     ByVal filteredOptions As Structures.ScrapeOptions
                                     ) As MediaContainers.MainDetails
        _Fallback_Movieset = Nothing
        If tmdbId = -1 Then Return Nothing
        Dim ApiResponse As Task(Of TMDbLib.Objects.Collections.Collection) = Task.Run(Function() _TMDbApi.GetCollectionAsync(tmdbId))

        If ApiResponse Is Nothing OrElse ApiResponse.Result Is Nothing OrElse Not ApiResponse.Result.Id > 0 OrElse ApiResponse.Exception IsNot Nothing Then
            _Logger.Warn(String.Format("[TMDB_Data] [Abort] No API result for TMDB Collection ID [{0}]", tmdbId))
            Return Nothing
        End If

        Dim ApiResult As TMDbLib.Objects.Collections.Collection = ApiResponse.Result
        Dim nMainDetails As New MediaContainers.MainDetails With {
            .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.MovieSet) With {.TMDbId = ApiResult.Id}
        }

        If _backgroundWorker.CancellationPending Or ApiResult Is Nothing Then Return Nothing

        'Plot
        If filteredOptions.Plot Then
            If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                nMainDetails.Plot = HttpUtility.HtmlDecode(ApiResult.Overview)
            ElseIf RunFallback_Movieset(ApiResult.Id) AndAlso _Fallback_Movieset.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_Movieset.Overview) Then
                nMainDetails.Plot = HttpUtility.HtmlDecode(_Fallback_Movieset.Overview)
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Poster (used only for SearchResult dialog, auto fallback to "en" by TMDb)
        If ApiResult.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.PosterPath) Then
            nMainDetails.ThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
            nMainDetails.ThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
        End If

        'Title
        If filteredOptions.Title Then
            If Not String.IsNullOrEmpty(ApiResult.Name) Then
                nMainDetails.Title = ApiResult.Name
            ElseIf RunFallback_Movieset(ApiResult.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_Movieset.Name) Then
                nMainDetails.Title = _Fallback_Movieset.Name
            End If
        End If

        _Fallback_Movieset = Nothing
        Return nMainDetails

        _Fallback_Movieset = Nothing
        Return Nothing
    End Function

    Public Function GetInfo_Movieset(ByVal uniqueId As String,
                                     ByVal filteredOptions As Structures.ScrapeOptions,
                                     ByVal scrapeModifiers As Structures.ScrapeModifiers,
                                     ByVal type As Enums.ContentType
                                     ) As MediaContainers.MainDetails Implements Interfaces.ISearchResultsDialog.GetInfo
        Select Case type
            Case Enums.ContentType.Movie
                GetInfo_Movie(uniqueId, scrapeModifiers, filteredOptions)
            Case Enums.ContentType.MovieSet
                Dim intId As Integer
                If Integer.TryParse(uniqueId, intId) Then
                    Return GetInfo_Movieset(intId, filteredOptions)
                End If
                Return Nothing
            Case Enums.ContentType.TVShow
                Dim intId As Integer
                If Integer.TryParse(uniqueId, intId) Then Return GetInfo_TVShow(intId, filteredOptions, scrapeModifiers)
                Return Nothing
        End Select
        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal showId As Integer,
                                      ByVal aired As String,
                                      ByRef filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.MainDetails
        Dim ShowInfo As TMDbLib.Objects.TvShows.TvShow

        Dim ApiResponse_TVShow As Task(Of TMDbLib.Objects.TvShows.TvShow) = Task.Run(Function() _TMDbApi.GetTvShowAsync(showId))

        ShowInfo = ApiResponse_TVShow.Result

        For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In ShowInfo.Seasons
            Dim seasonAPIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
            seasonAPIResult = Task.Run(Function() _TMDbApi.GetTvSeasonAsync(showId, aSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

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

    Public Function GetInfo_TVEpisode(ByVal showId As Integer,
                                      ByVal seasonNumber As Integer,
                                      ByVal episodeNumber As Integer,
                                      ByRef filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.MainDetails
        _Fallback_TVEpisode = Nothing
        Dim ApiResponse As Task(Of TMDbLib.Objects.TvShows.TvEpisode) = Task.Run(Function() _TMDbApi.GetTvEpisodeAsync(showId, seasonNumber, episodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))

        If ApiResponse Is Nothing OrElse ApiResponse.Result Is Nothing OrElse ApiResponse.Result.Id Is Nothing OrElse ApiResponse.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", showId, seasonNumber, episodeNumber))
            Return Nothing
        End If

        Dim ApiResult As TMDbLib.Objects.TvShows.TvEpisode = ApiResponse.Result
        Dim nMainDetails As New MediaContainers.MainDetails With {.Scrapersource = "TMDB"}

        'IDs
        nMainDetails.UniqueIDs.TMDbId = CInt(ApiResult.Id)
        If ApiResult.ExternalIds IsNot Nothing AndAlso ApiResult.ExternalIds.TvdbId IsNot Nothing Then nMainDetails.UniqueIDs.TVDbId = CInt(ApiResult.ExternalIds.TvdbId)
        If ApiResult.ExternalIds IsNot Nothing AndAlso ApiResult.ExternalIds.ImdbId IsNot Nothing Then nMainDetails.UniqueIDs.IMDbId = ApiResult.ExternalIds.ImdbId

        'Episode # Standard
        If ApiResult.EpisodeNumber >= 0 Then
            nMainDetails.Episode = ApiResult.EpisodeNumber
        End If

        'Season # Standard
        If ApiResult.SeasonNumber >= 0 Then
            nMainDetails.Season = ApiResult.SeasonNumber
        End If

        'Cast (Actors)
        If filteredOptions.Episodes.Actors Then nMainDetails.Actors = Parse_Actors(ApiResult.Credits)

        'Aired
        If filteredOptions.Episodes.Aired Then
            Dim nDate As Date? = Nothing
            If ApiResult.AirDate.HasValue Then
                nDate = ApiResult.AirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nMainDetails.Aired = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        'Director / Writer
        If filteredOptions.Episodes.Credits OrElse filteredOptions.Episodes.Directors Then
            If ApiResult.Credits IsNot Nothing AndAlso ApiResult.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In ApiResult.Credits.Crew
                    If filteredOptions.Episodes.Credits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nMainDetails.Credits.Add(aCrew.Name)
                    End If
                    If filteredOptions.Episodes.Directors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nMainDetails.Directors.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        'Guest Stars
        If filteredOptions.Episodes.GuestStars Then
            If ApiResult.GuestStars IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In ApiResult.GuestStars
                    nMainDetails.GuestStars.Add(New MediaContainers.Person With {
                                              .Name = aCast.Name,
                                              .Role = aCast.Character,
                                              .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                              .TMDbId = CStr(aCast.Id)
                                              })
                Next
            End If
        End If

        'Plot
        If filteredOptions.Episodes.Plot Then
            If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                nMainDetails.Plot = ApiResult.Overview
            ElseIf RunFallback_TVEpisode(showId, seasonNumber, episodeNumber) AndAlso _Fallback_TVEpisode.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVEpisode.Overview) Then
                nMainDetails.Plot = _Fallback_TVEpisode.Overview
            End If
        End If

        'Rating
        'VoteAverage is rounded to get a comparable result as with other reviews 
        If filteredOptions.Episodes.Ratings Then
            nMainDetails.Ratings.Add(New MediaContainers.RatingDetails With {
                                   .Max = 10,
                                   .Type = "themoviedb",
                                   .Value = Math.Round(ApiResult.VoteAverage, 1),
                                   .Votes = ApiResult.VoteCount
                                   })
        End If

        'Poster (used only for SearchResult dialog, auto fallback to "en" by TMDb)
        If ApiResult.StillPath IsNot Nothing Then
            nMainDetails.ThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", ApiResult.StillPath)
            nMainDetails.ThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w185", ApiResult.StillPath)
        End If

        'Title
        If filteredOptions.Episodes.Title Then
            If Not String.IsNullOrEmpty(ApiResult.Name) Then
                nMainDetails.Title = ApiResult.Name
            ElseIf RunFallback_TVEpisode(showId, seasonNumber, episodeNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVEpisode.Name) Then
                nMainDetails.Title = _Fallback_TVEpisode.Name
            End If
        End If

        _Fallback_TVEpisode = Nothing
        Return nMainDetails
    End Function

    Public Sub GetInfo_TVSeason(ByRef tvShow As MediaContainers.MainDetails,
                                ByVal showId As Integer,
                                ByVal seasonNumber As Integer,
                                ByRef filteredOptions As Structures.ScrapeOptions,
                                ByRef scrapeModifiers As Structures.ScrapeModifiers
                                )
        _Fallback_TVSeason = Nothing
        Dim ApiResponse As Task(Of TMDbLib.Objects.TvShows.TvSeason) = Task.Run(Function() _TMDbApi.GetTvSeasonAsync(showId, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If ApiResponse Is Nothing OrElse ApiResponse.Result Is Nothing OrElse ApiResponse.Result.Id Is Nothing OrElse ApiResponse.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or season not found: ShowID={0}, Season={1}", showId, seasonNumber))
            Return
        End If

        Dim ApiResult As TMDbLib.Objects.TvShows.TvSeason = ApiResponse.Result

        If scrapeModifiers.withSeasons Then
            Dim nMainDetails As New MediaContainers.MainDetails With {.Scrapersource = "TMDB"}

            'IDs
            nMainDetails.UniqueIDs.TMDbId = CInt(ApiResult.Id)
            If ApiResult.ExternalIds IsNot Nothing AndAlso ApiResult.ExternalIds.TvdbId IsNot Nothing Then nMainDetails.UniqueIDs.TVDbId = CInt(ApiResult.ExternalIds.TvdbId)

            'Season #
            If ApiResult.SeasonNumber >= 0 Then
                nMainDetails.Season = ApiResult.SeasonNumber
            End If

            'Aired
            If filteredOptions.Seasons.Aired Then
                Dim nDate As Date? = Nothing
                If ApiResult.AirDate.HasValue Then
                    nDate = ApiResult.AirDate
                End If
                If nDate.HasValue Then
                    'always save date in same date format not depending on users language setting!
                    nMainDetails.Aired = nDate.Value.ToString("yyyy-MM-dd")
                End If
            End If

            'Plot
            If filteredOptions.Seasons.Plot Then
                If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                    nMainDetails.Plot = ApiResult.Overview
                ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso _Fallback_TVSeason.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Overview) Then
                    nMainDetails.Plot = _Fallback_TVSeason.Overview
                End If
            End If

            'Title
            If filteredOptions.Seasons.Title Then
                If Not String.IsNullOrEmpty(ApiResult.Name) Then
                    nMainDetails.Title = ApiResult.Name
                ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Name) Then
                    nMainDetails.Title = _Fallback_TVSeason.Name
                End If
            End If

            tvShow.KnownSeasons.Add(nMainDetails)
        End If

        If scrapeModifiers.withEpisodes AndAlso ApiResult.Episodes IsNot Nothing Then
            For Each aEpisode As TMDbLib.Objects.Search.TvSeasonEpisode In ApiResult.Episodes
                tvShow.KnownEpisodes.Add(GetInfo_TVEpisode(showId, aEpisode.SeasonNumber, aEpisode.EpisodeNumber, filteredOptions))
            Next
        End If

        _Fallback_TVSeason = Nothing
    End Sub

    Public Function GetInfo_TVSeason(ByVal showId As Integer,
                                     ByVal seasonNumber As Integer,
                                     ByRef filteredOptions As Structures.ScrapeOptions
                                     ) As MediaContainers.MainDetails
        _Fallback_TVSeason = Nothing
        Dim ApiResponse As Task(Of TMDbLib.Objects.TvShows.TvSeason) = Task.Run(Function() _TMDbApi.GetTvSeasonAsync(showId, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If ApiResponse Is Nothing OrElse ApiResponse.Result Is Nothing OrElse ApiResponse.Result.Id Is Nothing OrElse ApiResponse.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", showId, seasonNumber))
            Return Nothing
        End If

        Dim ApiResult As TMDbLib.Objects.TvShows.TvSeason = ApiResponse.Result
        Dim nMainDetails As New MediaContainers.MainDetails With {.Scrapersource = "TMDB"}

        'IDs
        nMainDetails.UniqueIDs.TMDbId = CInt(ApiResult.Id)
        If ApiResult.ExternalIds IsNot Nothing AndAlso ApiResult.ExternalIds.TvdbId IsNot Nothing Then nMainDetails.UniqueIDs.TVDbId = CInt(ApiResult.ExternalIds.TvdbId)

        'Season #
        If ApiResult.SeasonNumber >= 0 Then
            nMainDetails.Season = ApiResult.SeasonNumber
        End If

        'Aired
        If filteredOptions.Seasons.Aired Then
            Dim nDate As Date? = Nothing
            If ApiResult.AirDate.HasValue Then
                nDate = ApiResult.AirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nMainDetails.Aired = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        'Plot
        If filteredOptions.Seasons.Plot Then
            If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                nMainDetails.Plot = ApiResult.Overview
            ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso _Fallback_TVSeason.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Overview) Then
                nMainDetails.Plot = _Fallback_TVSeason.Overview
            End If
        End If

        'Title
        If filteredOptions.Seasons.Title Then
            If Not String.IsNullOrEmpty(ApiResult.Name) Then
                nMainDetails.Title = ApiResult.Name
            ElseIf RunFallback_TVSeason(showId, seasonNumber) AndAlso Not String.IsNullOrEmpty(_Fallback_TVSeason.Name) Then
                nMainDetails.Title = _Fallback_TVSeason.Name
            End If
        End If

        _Fallback_TVSeason = Nothing
        Return nMainDetails
    End Function
    ''' <summary>
    '''  Scrape TV Show details from TMDB
    ''' </summary>
    ''' <param name="showId">TMDB ID of tv show to be scraped</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_TVShow(ByVal showId As Integer,
                                   ByRef filteredOptions As Structures.ScrapeOptions,
                                   ByRef scrapeModifiers As Structures.ScrapeModifiers
                                   ) As MediaContainers.MainDetails
        If showId = -1 Then Return Nothing
        _Fallback_TVShow = Nothing

        If _backgroundWorker.CancellationPending Then Return Nothing

        Dim ApiResponse As Task(Of TMDbLib.Objects.TvShows.TvShow) = Task.Run(Function() _TMDbApi.GetTvShowAsync(showId, TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))

        If ApiResponse Is Nothing OrElse ApiResponse.Result Is Nothing OrElse Not ApiResponse.Result.Id > 0 OrElse ApiResponse.Exception IsNot Nothing Then
            _Logger.Error(String.Format("Can't scrape or tv show not found: [{0}]", showId))
            Return Nothing
        End If

        Dim ApiResult As TMDbLib.Objects.TvShows.TvShow = ApiResponse.Result
        Dim nResult As New MediaContainers.MainDetails With {.Scrapersource = "TMDB"}

        'IDs
        nResult.UniqueIDs.TMDbId = ApiResult.Id
        If ApiResult.ExternalIds.TvdbId IsNot Nothing AndAlso Integer.TryParse(ApiResult.ExternalIds.TvdbId, 0) Then nResult.UniqueIDs.TVDbId = CInt(ApiResult.ExternalIds.TvdbId)
        If ApiResult.ExternalIds.ImdbId IsNot Nothing Then nResult.UniqueIDs.IMDbId = ApiResult.ExternalIds.ImdbId

        If _backgroundWorker.CancellationPending Or ApiResult Is Nothing Then Return Nothing

        'Actors
        If filteredOptions.Actors Then nResult.Actors = Parse_Actors(ApiResult.Credits)

        'Certifications
        If filteredOptions.Certifications Then nResult.Certifications = Parse_Certifications(ApiResult.ContentRatings)

        'Countries 
        If filteredOptions.Countries Then nResult.Countries = Parse_Countries(ApiResult.ProductionCountries)

        'Creators
        If filteredOptions.Creators Then
            nResult.Creators.AddRange(ApiResult.CreatedBy.Select(Function(f) f.Name))
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Genres
        If filteredOptions.Genres Then
            If ApiResult.Genres.Count > 0 Then
                nResult.Genres.AddRange(ApiResult.Genres.Select(Function(f) f.Name))
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso _Fallback_TVShow.Genres.Count > 0 Then
                nResult.Genres.AddRange(_Fallback_TVShow.Genres.Select(Function(f) f.Name))
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'OriginalTitle
        If filteredOptions.OriginalTitle Then
            nResult.OriginalTitle = ApiResult.OriginalName
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Plot
        If filteredOptions.Plot Then
            If ApiResult.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Overview) Then
                nResult.Plot = ApiResult.Overview
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso _Fallback_TVShow.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Overview) Then
                nResult.Plot = _Fallback_TVShow.Overview
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Poster (only for SearchResult dialog, auto fallback to "en" by TMDb)
        If ApiResult.PosterPath IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.PosterPath) Then
            nResult.ThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
            nResult.ThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w92", ApiResult.PosterPath)
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Premiered
        If filteredOptions.Premiered Then
            Dim nDate As Date? = Nothing
            If ApiResult.FirstAirDate.HasValue Then
                nDate = ApiResult.FirstAirDate
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso _Fallback_TVShow.FirstAirDate.HasValue Then
                nDate = _Fallback_TVShow.FirstAirDate
            End If
            If nDate.HasValue Then
                'always save date in same date format not depending on users language setting!
                nResult.Premiered = nDate.Value.ToString("yyyy-MM-dd")
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Rating
        If filteredOptions.Ratings Then
            nResult.Ratings.Add(New MediaContainers.RatingDetails With {
                                    .Max = 10,
                                    .Type = "themoviedb",
                                    .Value = ApiResult.VoteAverage,
                                    .Votes = ApiResult.VoteCount
                                    })
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Runtime
        If filteredOptions.Runtime Then
            If ApiResult.EpisodeRunTime IsNot Nothing AndAlso ApiResult.EpisodeRunTime.Count > 0 Then
                nResult.Runtime = CStr(ApiResult.EpisodeRunTime.Item(0))
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso _Fallback_TVShow.EpisodeRunTime IsNot Nothing AndAlso _Fallback_TVShow.EpisodeRunTime.Count > 0 Then
                nResult.Runtime = CStr(_Fallback_TVShow.EpisodeRunTime.Item(0))
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Status
        If filteredOptions.Status Then
            If Not String.IsNullOrEmpty(ApiResult.Status) Then
                nResult.Status = ApiResult.Status
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Status) Then
                nResult.Status = _Fallback_TVShow.Status
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Studios
        If filteredOptions.Studios Then
            If ApiResult.Networks.Count > 0 Then
                nResult.Studios.AddRange(ApiResult.Networks.Select(Function(f) f.Name))
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Tagline
        If filteredOptions.Tagline Then
            If ApiResult.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(ApiResult.Tagline) Then
                nResult.Tagline = ApiResult.Tagline
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso _Fallback_TVShow.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Tagline) Then
                nResult.Tagline = _Fallback_TVShow.Tagline
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Title
        If filteredOptions.Title Then
            If Not String.IsNullOrEmpty(ApiResult.Name) Then
                nResult.Title = ApiResult.Name
            ElseIf RunFallback_TVShow(ApiResult.Id) AndAlso Not String.IsNullOrEmpty(_Fallback_TVShow.Name) Then
                nResult.Title = _Fallback_TVShow.Name
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Seasons and Episodes
        If scrapeModifiers.withEpisodes OrElse scrapeModifiers.withSeasons Then
            For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In ApiResult.Seasons
                GetInfo_TVSeason(nResult, ApiResult.Id, aSeason.SeasonNumber, filteredOptions, scrapeModifiers)
            Next
        End If
        _Fallback_TVShow = Nothing
        Return nResult
    End Function

    Public Sub GetInfoAsync(ByVal tmdbOrIMDbId As String,
                            ByRef filteredOptions As Structures.ScrapeOptions,
                            ByVal type As Enums.ContentType
                            ) Implements Interfaces.ISearchResultsDialog.GetInfoAsync
        If Not _backgroundWorker.IsBusy Then
            Dim TaskType As TaskType
            Select Case type
                Case Enums.ContentType.Movie
                    TaskType = TaskType.GetInfo_Movie
                Case Enums.ContentType.MovieSet
                    TaskType = TaskType.GetInfo_Movieset
                Case Enums.ContentType.TVShow
                    TaskType = TaskType.GetInfo_TVShow
            End Select
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType,
                                             .Parameter = tmdbOrIMDbId,
                                             .ScrapeOptions = filteredOptions
                                             })
        End If
    End Sub

    Public Function GetTMDbByIMDb(ByVal imdbId As String) As Integer
        Try
            Dim ApiResponse As Task(Of TMDbLib.Objects.Find.FindContainer)
            ApiResponse = Task.Run(Function() _TMDbApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbId))

            If ApiResponse IsNot Nothing AndAlso ApiResponse.Exception Is Nothing AndAlso ApiResponse.Result IsNot Nothing AndAlso
                    ApiResponse.Result.TvResults IsNot Nothing AndAlso ApiResponse.Result.TvResults.Count > 0 Then
                Return ApiResponse.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetTMDbByTVDb(ByVal tvdbId As Integer) As Integer
        Try
            Dim ApiResponse As Task(Of TMDbLib.Objects.Find.FindContainer)
            ApiResponse = Task.Run(Function() _TMDbApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.TvDb, tvdbId.ToString))

            If ApiResponse IsNot Nothing AndAlso ApiResponse.Exception Is Nothing AndAlso ApiResponse.Result IsNot Nothing AndAlso
                    ApiResponse.Result.TvResults IsNot Nothing AndAlso ApiResponse.Result.TvResults.Count > 0 Then
                Return ApiResponse.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Private Function Parse_Actors(ByRef credits As TMDbLib.Objects.Movies.Credits) As List(Of MediaContainers.Person)
        Dim Result As New List(Of MediaContainers.Person)
        If credits IsNot Nothing AndAlso credits.Cast IsNot Nothing Then
            For Each item As TMDbLib.Objects.Movies.Cast In credits.Cast
                Result.Add(Parse_Actors_Create_Person(item.Name, item.Character, item.ProfilePath, item.Id))
            Next
        End If
        Return Result
    End Function

    Private Function Parse_Actors(ByRef credits As TMDbLib.Objects.TvShows.Credits) As List(Of MediaContainers.Person)
        Dim Result As New List(Of MediaContainers.Person)
        If credits IsNot Nothing AndAlso credits.Cast IsNot Nothing Then
            For Each item As TMDbLib.Objects.TvShows.Cast In credits.Cast
                Result.Add(Parse_Actors_Create_Person(item.Name, item.Character, item.ProfilePath, item.Id))
            Next
        End If
        Return Result
    End Function

    Private Function Parse_Actors(ByRef credits As TMDbLib.Objects.TvShows.CreditsWithGuestStars) As List(Of MediaContainers.Person)
        Dim Result As New List(Of MediaContainers.Person)
        If credits IsNot Nothing AndAlso credits.Cast IsNot Nothing Then
            For Each item As TMDbLib.Objects.TvShows.Cast In credits.Cast
                Result.Add(Parse_Actors_Create_Person(item.Name, item.Character, item.ProfilePath, item.Id))
            Next
        End If
        Return Result
    End Function

    Private Function Parse_Actors_Create_Person(ByVal name As String, ByVal character As String, ByVal profilePath As String, ByVal id As Integer) As MediaContainers.Person
        Return New MediaContainers.Person With {
            .Name = name,
            .Role = character,
            .URLOriginal = If(Not String.IsNullOrEmpty(profilePath), String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", profilePath), String.Empty),
            .TMDbId = id.ToString
        }
    End Function

    Private Function Parse_Certifications(ByRef releases As TMDbLib.Objects.Movies.Releases) As List(Of String)
        Dim Result As New List(Of String)
        If releases IsNot Nothing AndAlso releases.Countries IsNot Nothing Then
            For Each item In releases.Countries.Where(Function(f) Not String.IsNullOrEmpty(f.Certification))
                Result.Add(Parse_Certifications_Create_Certification(item.Iso_3166_1, item.Certification))
            Next
        End If
        Result.RemoveAll(Function(f) String.IsNullOrEmpty(f))
        Return Result
    End Function

    Private Function Parse_Certifications(ByRef contentRatings As TMDbLib.Objects.General.ResultContainer(Of TMDbLib.Objects.TvShows.ContentRating)) As List(Of String)
        Dim Result As New List(Of String)
        If contentRatings IsNot Nothing AndAlso contentRatings.Results IsNot Nothing Then
            For Each item In contentRatings.Results
                Result.Add(Parse_Certifications_Create_Certification(item.Iso_3166_1, item.Rating))
            Next
        End If
        Result.RemoveAll(Function(f) String.IsNullOrEmpty(f))
        Return Result
    End Function

    Private Function Parse_Certifications_Create_Certification(ByVal alpha2 As String, ByVal certification As String) As String
        Dim Country = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = alpha2)
        If Country IsNot Nothing AndAlso Country.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(Country.Name) Then
            Return String.Concat(Country.Name, ":", certification)
        Else
            _Logger.Warn("Unhandled certification country encountered: {0}", alpha2)
        End If
        Return String.Empty
    End Function

    Private Function Parse_Countries(ByRef countries As List(Of TMDbLib.Objects.General.ProductionCountry)) As List(Of String)
        If countries IsNot Nothing Then
            Return (From c In countries Select c.Name).ToList
        End If
        Return New List(Of String)
    End Function

    Public Function Process_SearchResults_Movie(ByVal title As String,
                                                ByRef oDbElement As Database.DBElement,
                                                ByVal type As Enums.ScrapeType,
                                                ByVal scrapeModifiers As Structures.ScrapeModifiers,
                                                ByVal filteredOptions As Structures.ScrapeOptions
                                                ) As MediaContainers.MainDetails
        Dim SearchResults = Search_By_Title_Movie(title, CInt(If(oDbElement.MainDetails.YearSpecified, oDbElement.MainDetails.Year, Nothing)))

        Select Case type
            Case Enums.ScrapeType.Ask
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TMDbId.ToString, scrapeModifiers, filteredOptions).ScraperResult_Data
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tmdb", New List(Of String) From {"TMDb", "IMDb"}, Enums.ContentType.Movie)
                        Select Case dlgSearch.ShowDialog(title, oDbElement.Filename, SearchResults)
                            Case DialogResult.OK
                                If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                    Return GetInfo_Movie(dlgSearch.Result.UniqueIDs.TMDbId.ToString, scrapeModifiers, filteredOptions).ScraperResult_Data
                                End If
                            Case DialogResult.Retry
                            Case DialogResult.Cancel
                        End Select
                    End Using
                End If

            Case Enums.ScrapeType.Skip
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TMDbId.ToString, scrapeModifiers, filteredOptions).ScraperResult_Data
                End If

            Case Enums.ScrapeType.Auto
                If SearchResults.Count > 0 Then
                    Return GetInfo_Movie(SearchResults.Item(0).UniqueIDs.TMDbId.ToString, scrapeModifiers, filteredOptions).ScraperResult_Data
                End If
        End Select

        Return Nothing
    End Function

    Public Function Process_SearchResults_Movieset(ByVal title As String,
                                                   ByRef oDbElement As Database.DBElement,
                                                   ByVal type As Enums.ScrapeType,
                                                   ByVal scrapeModifiers As Structures.ScrapeModifiers,
                                                   ByVal filteredOptions As Structures.ScrapeOptions
                                                   ) As MediaContainers.MainDetails
        Dim SearchResults = Search_By_Title_Movieset(title)

        Select Case type
            Case Enums.ScrapeType.Ask
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movieset(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tmdb", New List(Of String) From {"TMDb"}, Enums.ContentType.MovieSet)
                        If dlgSearch.ShowDialog(title, String.Empty, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_Movieset(dlgSearch.Result.UniqueIDs.TMDbId, filteredOptions)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.Skip
                If SearchResults.Count = 1 Then
                    Return GetInfo_Movieset(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions)
                End If

            Case Enums.ScrapeType.Auto
                If SearchResults.Count > 0 Then
                    Return GetInfo_Movieset(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions)
                End If
        End Select

        Return Nothing
    End Function

    Public Function Process_SearchResults_TVShow(ByVal title As String,
                                                 ByRef oDbElement As Database.DBElement,
                                                 ByVal type As Enums.ScrapeType,
                                                 ByRef filteredOptions As Structures.ScrapeOptions,
                                                 ByRef scrapeModifiers As Structures.ScrapeModifiers
                                                 ) As MediaContainers.MainDetails
        Dim SearchResults = Search_By_Title_TVShow(title)

        Select Case type
            Case Enums.ScrapeType.Ask
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions, scrapeModifiers)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tmdb", New List(Of String) From {"TMDb", "TVDb", "IMDb"}, Enums.ContentType.TVShow)
                        If dlgSearch.ShowDialog(title, oDbElement.ShowPath, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result.UniqueIDs.TMDbIdSpecified Then
                                Return GetInfo_TVShow(dlgSearch.Result.UniqueIDs.TMDbId, filteredOptions, scrapeModifiers)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.Skip
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions, scrapeModifiers)
                End If

            Case Enums.ScrapeType.Auto
                If SearchResults.Count > 0 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TMDbId, filteredOptions, scrapeModifiers)
                End If
        End Select

        Return Nothing
    End Function

    Private Function RunFallback_Movie(ByVal tmdbId As Integer) As Boolean
        If Not ScraperSettings.FallBackToEng Then Return False
        If _Fallback_Movie Is Nothing Then
            Dim ApiResponse_E = Task.Run(Function() _TMDbApiEn.GetMovieAsync(tmdbId, TMDbLib.Objects.Movies.MovieMethods.Credits Or TMDbLib.Objects.Movies.MovieMethods.Releases Or TMDbLib.Objects.Movies.MovieMethods.Videos))
            _Fallback_Movie = ApiResponse_E.Result
            Return _Fallback_Movie IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_Movieset(ByVal tmdbId As Integer) As Boolean
        If Not ScraperSettings.FallBackToEng Then Return False
        If _Fallback_Movieset Is Nothing Then
            Dim ApiResponse_E = Task.Run(Function() _TMDbApiEn.GetCollectionAsync(tmdbId))
            _Fallback_Movieset = ApiResponse_E.Result
            Return _Fallback_Movieset IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVEpisode(ByVal showId As Integer,
                                           ByVal seasonNumber As Integer,
                                           ByVal episodeNumber As Integer
                                           ) As Boolean
        If Not ScraperSettings.FallBackToEng Then Return False
        If _Fallback_TVEpisode Is Nothing Then
            Dim ApiResponse_E = Task.Run(Function() _TMDbApiEn.GetTvEpisodeAsync(showId,
                                                                               seasonNumber,
                                                                               episodeNumber,
                                                                               TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or
                                                                               TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds
                                                                               ))
            _Fallback_TVEpisode = ApiResponse_E.Result
            Return _Fallback_TVEpisode IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVSeason(ByVal showId As Integer,
                                          ByVal seasonNumber As Integer
                                          ) As Boolean
        If Not ScraperSettings.FallBackToEng Then Return False
        If _Fallback_TVSeason Is Nothing Then
            Dim ApiResponse_E = Task.Run(Function() _TMDbApiEn.GetTvSeasonAsync(showId,
                                                                              seasonNumber,
                                                                              TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or
                                                                              TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds
                                                                              ))
            _Fallback_TVSeason = ApiResponse_E.Result
            Return _Fallback_TVSeason IsNot Nothing
        Else
            Return True
        End If
    End Function

    Private Function RunFallback_TVShow(ByVal tmdbId As Integer) As Boolean
        If Not ScraperSettings.FallBackToEng Then Return False
        If _Fallback_TVShow Is Nothing Then
            Dim ApiResponse_E = Task.Run(Function() _TMDbApiEn.GetTvShowAsync(tmdbId,
                                                                            TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or
                                                                            TMDbLib.Objects.TvShows.TvShowMethods.Credits Or
                                                                            TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds
                                                                            ))
            _Fallback_TVShow = ApiResponse_E.Result
            Return _Fallback_TVShow IsNot Nothing
        Else
            Return True
        End If
    End Function

    Public Function Scrape_Movie(ByVal id As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Dim nAddonResult As New Interfaces.AddonResult
        nAddonResult = GetInfo_Movie(id, scrapeModifiers, scrapeOptions)
        nAddonResult.ScraperResult_ImageContainer = GetImages_Movie_Movieset(nAddonResult.ScraperResult_Data.UniqueIDs.TMDbId, scrapeModifiers, Enums.ContentType.Movie)
        Return nAddonResult
    End Function

    Public Function Scrape_Movieset(ByVal id As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_Movieset(id, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_Movie_Movieset(id, scrapeModifiers, Enums.ContentType.MovieSet)
        }
    End Function

    Public Function Scrape_TVEpisode(ByVal showId As Integer, ByVal aired As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVEpisode(showId, aired, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVEpisode(showId, .ScraperResult_Data.Season, .ScraperResult_Data.Episode, scrapeModifiers)
        }
    End Function

    Public Function Scrape_TVEpisode(ByVal showId As Integer, ByVal season As Integer, episode As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVEpisode(showId, season, episode, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVEpisode(showId, season, episode, scrapeModifiers)
        }
    End Function

    Public Function Scrape_TVSeason(ByVal showId As Integer, ByVal season As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVSeason(showId, season, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVShow(showId, scrapeModifiers)
        }
    End Function

    Public Function ScrapeTVShow(ByVal showId As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVShow(showId, scrapeOptions, scrapeModifiers),
            .ScraperResult_ImageContainer = GetImages_TVShow(showId, scrapeModifiers)
        }
    End Function

    Private Function Search_By_Title_Movie(ByVal title As String, Optional ByVal year As Integer = 0) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.MainDetails)

        Dim ApiResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
        Dim SearchResults As New List(Of MediaContainers.MainDetails)

        Dim Page As Integer = 1
        Dim TotP As Integer
        Dim aE As Boolean

        Dim ApiResponse As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie))
        ApiResponse = Task.Run(Function() _TMDbApi.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year))

        ApiResult = ApiResponse.Result

        If ApiResult.TotalResults = 0 AndAlso ScraperSettings.FallBackToEng Then
            ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year))
            ApiResult = ApiResponse.Result
            aE = True
        End If

        'try -1 year if no search result was found
        If ApiResult.TotalResults = 0 AndAlso year > 0 AndAlso ScraperSettings.SearchDeviant Then
            ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year - 1))
            ApiResult = ApiResponse.Result

            If ApiResult.TotalResults = 0 AndAlso ScraperSettings.FallBackToEng Then
                ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year - 1))
                ApiResult = ApiResponse.Result
                aE = True
            End If

            'still no search result, try +1 year
            If ApiResult.TotalResults = 0 Then
                ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year + 1))
                ApiResult = ApiResponse.Result

                If ApiResult.TotalResults = 0 AndAlso ScraperSettings.FallBackToEng Then
                    ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year + 1))
                    ApiResult = ApiResponse.Result
                    aE = True
                End If
            End If
        End If

        If ApiResult.TotalResults > 0 Then
            TotP = ApiResult.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If ApiResult.Results IsNot Nothing Then
                    For Each item In ApiResult.Results
                        Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                        Dim strOriginalTitle As String = String.Empty
                        Dim strPlot As String = String.Empty
                        Dim strTitle As String = String.Empty
                        Dim strYear As String = String.Empty

                        If item.OriginalTitle IsNot Nothing Then strOriginalTitle = item.OriginalTitle
                        If item.Overview IsNot Nothing Then strPlot = item.Overview
                        If item.PosterPath IsNot Nothing Then
                            imgThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", item.PosterPath)
                            imgThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w185", item.PosterPath)
                        End If
                        If item.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(item.ReleaseDate)) Then strYear = item.ReleaseDate.Value.Year.ToString
                        If item.Title IsNot Nothing Then strTitle = item.Title

                        SearchResults.Add(New MediaContainers.MainDetails With {
                                          .OriginalTitle = strOriginalTitle,
                                          .Plot = strPlot,
                                          .Title = strTitle,
                                          .ThumbPoster = imgThumbPoster,
                                          .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.Movie) With {.TMDbId = item.Id},
                                          .Year = strYear
                                          })
                    Next
                End If
                Page = Page + 1
                If aE Then
                    ApiResponse = Task.Run(Function() _TMDbApiEn.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year))
                    ApiResult = ApiResponse.Result
                Else
                    ApiResponse = Task.Run(Function() _TMDbApi.SearchMovieAsync(title, Page, ScraperSettings.GetAdultItems, year))
                    ApiResult = ApiResponse.Result
                End If
            End While
        End If

        Return SearchResults
    End Function

    Private Function Search_By_Title_Movieset(ByVal title As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.MainDetails)

        Dim ApiResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection)
        Dim SearchResults As New List(Of MediaContainers.MainDetails)

        Dim Page As Integer = 1
        Dim TotP As Integer
        Dim aE As Boolean

        Dim ApiResponse As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection))
        ApiResponse = Task.Run(Function() _TMDbApi.SearchCollectionAsync(title, Page))

        ApiResult = ApiResponse.Result

        If ApiResult.TotalResults = 0 AndAlso ScraperSettings.FallBackToEng Then
            ApiResponse = Task.Run(Function() _TMDbApiEn.SearchCollectionAsync(title, Page))
            ApiResult = ApiResponse.Result
            aE = True
        End If

        If ApiResult.TotalResults > 0 Then
            TotP = ApiResult.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If ApiResult.Results IsNot Nothing Then
                    For Each item In ApiResult.Results
                        Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                        Dim strTitle As String = String.Empty
                        Dim strPlot As String = String.Empty

                        If item.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Name) Then
                            strTitle = item.Name
                        End If
                        If item.PosterPath IsNot Nothing Then
                            imgThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", item.PosterPath)
                            imgThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w185", item.PosterPath)
                        End If

                        SearchResults.Add(New MediaContainers.MainDetails With {
                                          .Title = strTitle,
                                          .ThumbPoster = imgThumbPoster,
                                          .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.MovieSet) With {.TMDbId = item.Id}
                                          })
                    Next
                End If
                Page = Page + 1
                If aE Then
                    ApiResponse = Task.Run(Function() _TMDbApiEn.SearchCollectionAsync(title, Page))
                    ApiResult = ApiResponse.Result
                Else
                    ApiResponse = Task.Run(Function() _TMDbApi.SearchCollectionAsync(title, Page))
                    ApiResult = ApiResponse.Result
                End If
            End While
        End If

        Return SearchResults
    End Function

    Private Function Search_By_Title_TVShow(ByVal title As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.MainDetails)

        Dim ApiResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv)
        Dim SearchResults As New List(Of MediaContainers.MainDetails)

        Dim Page As Integer = 1
        Dim TotP As Integer
        Dim aE As Boolean

        Dim ApiResponse As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv))
        ApiResponse = Task.Run(Function() _TMDbApi.SearchTvShowAsync(title, Page))

        ApiResult = ApiResponse.Result

        If ApiResult.TotalResults = 0 AndAlso ScraperSettings.FallBackToEng Then
            ApiResponse = Task.Run(Function() _TMDbApiEn.SearchTvShowAsync(title, Page))
            ApiResult = ApiResponse.Result
            aE = True
        End If

        If ApiResult.TotalResults > 0 Then
            TotP = ApiResult.TotalPages
            While Page <= TotP AndAlso Page <= 3
                If ApiResult.Results IsNot Nothing Then
                    For Each item In ApiResult.Results
                        Dim imgThumbPoster As MediaContainers.Image = New MediaContainers.Image
                        Dim strOriginalTitle As String = String.Empty
                        Dim strPlot As String = String.Empty
                        Dim strTitle As String = String.Empty
                        Dim strYear As String = String.Empty

                        If item.Name Is Nothing OrElse (item.Name IsNot Nothing AndAlso String.IsNullOrEmpty(item.Name)) Then
                            If item.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.OriginalName) Then
                                strTitle = item.OriginalName
                            End If
                        Else
                            strTitle = item.Name
                        End If
                        If item.OriginalName IsNot Nothing Then strOriginalTitle = item.OriginalName
                        If item.Overview IsNot Nothing Then strPlot = item.Overview
                        If item.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(item.FirstAirDate)) Then
                            strYear = CStr(item.FirstAirDate.Value.Year)
                        End If
                        If item.PosterPath IsNot Nothing Then
                            imgThumbPoster.URLOriginal = String.Concat(_TMDbApi.Config.Images.BaseUrl, "original", item.PosterPath)
                            imgThumbPoster.URLThumb = String.Concat(_TMDbApi.Config.Images.BaseUrl, "w185", item.PosterPath)
                        End If

                        SearchResults.Add(New MediaContainers.MainDetails With {
                                          .Plot = strPlot,
                                          .Premiered = strYear,
                                          .ThumbPoster = imgThumbPoster,
                                          .Title = strTitle,
                                          .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVShow) With {.TMDbId = item.Id}
                                          })
                    Next
                End If
                Page = Page + 1
                If aE Then
                    ApiResponse = Task.Run(Function() _TMDbApiEn.SearchTvShowAsync(title, Page))
                    ApiResult = ApiResponse.Result
                Else
                    ApiResponse = Task.Run(Function() _TMDbApi.SearchTvShowAsync(title, Page))
                    ApiResult = ApiResponse.Result
                End If
            End While
        End If

        Return SearchResults
    End Function

    Public Sub SearchAsync_By_Title(ByVal title As String,
                                    ByVal type As Enums.ContentType,
                                    Optional ByVal year As Integer = Nothing
                                    ) Implements Interfaces.ISearchResultsDialog.SearchAsync_By_Title
        If Not _backgroundWorker.IsBusy Then
            Dim TaskType As TaskType
            Select Case type
                Case Enums.ContentType.Movie
                    TaskType = TaskType.Search_By_Title_Movie
                Case Enums.ContentType.MovieSet
                    TaskType = TaskType.Search_By_Title_Movieset
                Case Enums.ContentType.TVShow
                    TaskType = TaskType.Search_By_Title_TVShow
            End Select
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType,
                                             .Parameter = title,
                                             .Year = year
                                             })
        End If
    End Sub

    Public Sub SearchAsync_By_UniqueId(ByVal uniqueId As String, ByVal type As Enums.ContentType) Implements Interfaces.ISearchResultsDialog.SearchAsync_By_UniqueId
        If Not _backgroundWorker.IsBusy Then
            Dim TaskType As TaskType
            Select Case type
                Case Enums.ContentType.Movie
                    TaskType = TaskType.Search_By_UniqueId_Movie
                Case Enums.ContentType.MovieSet
                    TaskType = TaskType.Search_By_UniqueId_Movieset
                Case Enums.ContentType.TVShow
                    TaskType = TaskType.Search_By_UniqueId_TVShow
            End Select
            _backgroundWorker.WorkerReportsProgress = False
            _backgroundWorker.WorkerSupportsCancellation = True
            _backgroundWorker.RunWorkerAsync(New Arguments With {
                                             .TaskType = TaskType,
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