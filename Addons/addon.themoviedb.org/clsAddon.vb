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

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _ApiKey As String = String.Empty

    Private _TMDBApi As TMDbLib.Client.TMDbClient  'preferred language
    Private _TMDBApiEN As TMDbLib.Client.TMDbClient 'english language
    Private _TMDBApiG As TMDbLib.Client.TMDbClient 'global, no language

#End Region 'Fields

#Region "Properties"

    Public Property Settings As New AddonSettings

    Public Property PreferredLanguage As String
        Get
            Return _TMDBApi.DefaultLanguage
        End Get
        Set(value As String)
            _TMDBApi.DefaultLanguage = value
        End Set
    End Property

    Public ReadOnly Property IsClientCreated As Boolean
        Get
            Return _TMDBApi IsNot Nothing
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Async Function CreateAPI(ByVal privateApiKey As String) As Task
        If Not String.IsNullOrEmpty(privateApiKey) Then
            _ApiKey = privateApiKey
        Else
            _ApiKey = My.Resources.EmberAPIKey
        End If

        Try
            _TMDBApi = New TMDbLib.Client.TMDbClient(_ApiKey)
            Await _TMDBApi.GetConfigAsync()
            _TMDBApi.MaxRetryCount = 2
            _Logger.Trace("[TMDB] [CreateAPI] Client-PreferredLanguage created")

            _TMDBApiG = New TMDbLib.Client.TMDbClient(_ApiKey)
            Await _TMDBApiG.GetConfigAsync()
            _TMDBApi.MaxRetryCount = 2
            _Logger.Trace("[TMDB] [CreateAPI] Client-Global created")

            If Settings.FallBackToEng Then
                _TMDBApiEN = New TMDbLib.Client.TMDbClient(_ApiKey)
                Await _TMDBApiEN.GetConfigAsync()
                _TMDBApiEN.DefaultLanguage = "en-US"
                _TMDBApiEN.MaxRetryCount = 2
                _Logger.Trace("[TMDB] [CreateAPI] Client-en-US created")
            Else
                _TMDBApiEN = _TMDBApi
                _Logger.Trace("[TMDB] [CreateAPI] Client-en-US = Client-PreferredLanguage")
            End If
        Catch ex As Exception
            _Logger.Error(String.Format("[TMDB] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Private Function Convert_ReleaseDateToPremiered(ByVal releaseDate As Date?) As String
        If releaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(releaseDate.ToString) Then
            Dim dPremiered As Date
            If Date.TryParse(releaseDate.ToString, dPremiered) Then
                'always save date in same date format not depending on users language setting!
                Return dPremiered.ToString("yyyy-MM-dd")
            End If
        End If
        Return releaseDate.ToString
    End Function

    Private Function Convert_VideoQuality(ByRef size As Integer) As Enums.TrailerVideoQuality
        Select Case size
            Case 1080
                Return Enums.TrailerVideoQuality.HD1080p
            Case 720
                Return Enums.TrailerVideoQuality.HD720p
            Case 480
                Return Enums.TrailerVideoQuality.HQ480p
            Case Else
                Return Enums.TrailerVideoQuality.Any
        End Select
    End Function

    Private Function Convert_VideoType(ByRef type As String) As Enums.TrailerType
        Select Case type.ToLower
            Case "clip"
                Return Enums.TrailerType.Clip
            Case "featurette"
                Return Enums.TrailerType.Featurette
            Case "teaser"
                Return Enums.TrailerType.Teaser
            Case "trailer"
                Return Enums.TrailerType.Trailer
            Case Else
                Return Enums.TrailerType.Any
        End Select
    End Function

    Public Function GetImages_Movie_Movieset(ByVal tmdbID As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal type As Enums.ContentType) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim Results As TMDbLib.Objects.General.Images = Nothing
            Dim APIResult As Task(Of TMDbLib.Objects.General.ImagesWithId)

            If type = Enums.ContentType.Movie Then
                APIResult = Task.Run(Function() _TMDBApiG.GetMovieImagesAsync(tmdbID))
                Results = APIResult.Result
            ElseIf type = Enums.ContentType.Movieset Then
                APIResult = Task.Run(Function() _TMDBApiG.GetCollectionImagesAsync(tmdbID))
                Results = APIResult.Result
            End If

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'MainFanart
            If scrapeModifiers.MainFanart AndAlso Results.Backdrops IsNot Nothing Then
                For Each tImage In Results.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                            .Height = tImage.Height,
                            .Likes = 0,
                            .Scraper = "TMDB",
                            .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width}

                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster
            If scrapeModifiers.MainPoster AndAlso Results.Posters IsNot Nothing Then
                For Each tImage In Results.Posters
                    Dim newImage As New MediaContainers.Image With {
                                .Height = tImage.Height,
                                .Likes = 0,
                                .Scraper = "TMDB",
                                .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                .VoteAverage = tImage.VoteAverage,
                                .VoteCount = tImage.VoteCount,
                                .Width = tImage.Width}

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
            APIResult = Task.Run(Function() _TMDBApiG.GetTvShowAsync(tmdbID, TMDbLib.Objects.TvShows.TvShowMethods.Images))

            If APIResult Is Nothing Then
                Return alImagesContainer
            End If

            Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result

            'MainFanart
            If scrapeModifiers.MainFanart AndAlso Result.Images.Backdrops IsNot Nothing Then
                For Each tImage In Result.Images.Backdrops
                    Dim newImage As New MediaContainers.Image With {
                            .Height = tImage.Height,
                            .Likes = 0,
                            .Scraper = "TMDB",
                            .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w300" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width}

                    alImagesContainer.MainFanarts.Add(newImage)
                Next
            End If

            'MainPoster
            If scrapeModifiers.MainPoster AndAlso Result.Images.Posters IsNot Nothing Then
                For Each tImage In Result.Images.Posters
                    Dim newImage As New MediaContainers.Image With {
                                .Height = tImage.Height,
                                .Likes = 0,
                                .Scraper = "TMDB",
                                .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                .VoteAverage = tImage.VoteAverage,
                                .VoteCount = tImage.VoteCount,
                                .Width = tImage.Width}

                    alImagesContainer.MainPosters.Add(newImage)
                Next
            End If

            'SeasonPoster
            If (scrapeModifiers.SeasonPoster OrElse scrapeModifiers.EpisodePoster) AndAlso Result.Seasons IsNot Nothing Then
                For Each tSeason In Result.Seasons
                    Dim APIResult_Season As Task(Of TMDbLib.Objects.TvShows.TvSeason)
                    APIResult_Season = Task.Run(Function() _TMDBApiG.GetTvSeasonAsync(CInt(tmdbID), tSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Images))

                    If APIResult_Season IsNot Nothing Then
                        Dim Result_Season As TMDbLib.Objects.TvShows.TvSeason = APIResult_Season.Result

                        'SeasonPoster
                        If scrapeModifiers.SeasonPoster AndAlso Result_Season.Images.Posters IsNot Nothing Then
                            For Each tImage In Result_Season.Images.Posters
                                Dim newImage As New MediaContainers.Image With {
                                        .Height = tImage.Height,
                                        .Likes = 0,
                                        .Scraper = "TMDB",
                                        .Season = tSeason.SeasonNumber,
                                        .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                                        .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                                        .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                                        .VoteAverage = tImage.VoteAverage,
                                        .VoteCount = tImage.VoteCount,
                                        .Width = tImage.Width}

                                alImagesContainer.SeasonPosters.Add(newImage)
                            Next
                        End If

                        If scrapeModifiers.EpisodePoster AndAlso Result_Season.Episodes IsNot Nothing Then
                            For Each tEpisode In Result_Season.Episodes

                                'EpisodePoster
                                If scrapeModifiers.EpisodePoster AndAlso tEpisode.StillPath IsNot Nothing Then

                                    Dim newImage As New MediaContainers.Image With {
                                            .Episode = tEpisode.EpisodeNumber,
                                            .Scraper = "TMDB",
                                            .Season = CInt(tEpisode.SeasonNumber),
                                            .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tEpisode.StillPath,
                                            .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w185" & tEpisode.StillPath}

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
            APIResult = Task.Run(Function() _TMDBApiG.GetTvEpisodeImagesAsync(showID, season, episode))
            Results = APIResult.Result

            If Results Is Nothing Then
                Return alImagesContainer
            End If

            'EpisodePoster
            If scrapeModifiers.EpisodePoster AndAlso Results.Stills IsNot Nothing Then
                For Each tImage In Results.Stills
                    Dim newImage As New MediaContainers.Image With {
                            .Episode = episode,
                            .Height = tImage.Height,
                            .Likes = 0,
                            .Scraper = "TMDB",
                            .Season = season,
                            .Language = If(String.IsNullOrEmpty(tImage.Iso_639_1), String.Empty, tImage.Iso_639_1),
                            .URLOriginal = _TMDBApiG.Config.Images.BaseUrl & "original" & tImage.FilePath,
                            .URLThumb = _TMDBApiG.Config.Images.BaseUrl & "w185" & tImage.FilePath,
                            .VoteAverage = tImage.VoteAverage,
                            .VoteCount = tImage.VoteCount,
                            .Width = tImage.Width}

                    alImagesContainer.EpisodePosters.Add(newImage)
                Next
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetMovieCollectionID(ByVal IMDbID As String) As String
        Dim Movie As TMDbLib.Objects.Movies.Movie

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        APIResult = Task.Run(Function() _TMDBApi.GetMovieAsync(IMDbID))

        Movie = APIResult.Result
        If Movie Is Nothing Then Return String.Empty

        If Movie.BelongsToCollection IsNot Nothing AndAlso Movie.BelongsToCollection.Id > 0 Then
            Return CStr(Movie.BelongsToCollection.Id)
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetInfo_Movie(ByVal tmdbOrImdbID As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        If String.IsNullOrEmpty(tmdbOrImdbID) Then Return Nothing

        Dim nMainDetails As New MediaContainers.MainDetails
        Dim nTrailers As New List(Of MediaContainers.Trailer)

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        Dim APIResultE As Task(Of TMDbLib.Objects.Movies.Movie)

        If tmdbOrImdbID.ToLower.StartsWith("tt") Then
            'search movie by IMDB ID
            APIResult = Task.Run(Function() _TMDBApi.GetMovieAsync(
                                     tmdbOrImdbID,
                                     TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                     TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                     TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                     TMDbLib.Objects.Movies.MovieMethods.Videos
                                     ))
            If Settings.FallBackToEng Then
                APIResultE = Task.Run(Function() _TMDBApiEN.GetMovieAsync(
                                          tmdbOrImdbID,
                                          TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                          TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                          TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                          TMDbLib.Objects.Movies.MovieMethods.Videos
                                          ))
            Else
                APIResultE = APIResult
            End If
        Else
            'search movie by TMDB ID
            APIResult = Task.Run(Function() _TMDBApi.GetMovieAsync(
                                     CInt(tmdbOrImdbID),
                                     TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                     TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                     TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                     TMDbLib.Objects.Movies.MovieMethods.Videos
                                     ))
            If Settings.FallBackToEng Then
                APIResultE = Task.Run(Function() _TMDBApiEN.GetMovieAsync(
                                          CInt(tmdbOrImdbID),
                                          TMDbLib.Objects.Movies.MovieMethods.Credits Or
                                          TMDbLib.Objects.Movies.MovieMethods.Keywords Or
                                          TMDbLib.Objects.Movies.MovieMethods.Releases Or
                                          TMDbLib.Objects.Movies.MovieMethods.Videos
                                          ))
            Else
                APIResultE = APIResult
            End If
        End If

        Dim Result As TMDbLib.Objects.Movies.Movie = APIResult.Result
        Dim ResultE As TMDbLib.Objects.Movies.Movie = APIResultE.Result

        If (Result Is Nothing AndAlso Not Settings.FallBackToEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                (Not Result.Id > 0 AndAlso Not Settings.FallBackToEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
            _Logger.Error(String.Format("Can't scrape or movie not found: [0]", tmdbOrImdbID))
            Return Nothing
        End If

        nMainDetails.Scrapersource = "TMDB"

        'IDs
        nMainDetails.UniqueIDs.TMDbId = Result.Id
        If Result.ImdbId IsNot Nothing Then nMainDetails.UniqueIDs.IMDbId = Result.ImdbId

        'Cast (Actors)
        If scrapeOptions.Actors Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.Movies.Cast In Result.Credits.Cast
                    Dim nUniqueID As New MediaContainers.Uniqueid With {
                        .Type = "tmdb",
                        .Value = aCast.Id.ToString}
                    nMainDetails.Actors.Add(New MediaContainers.Person With {
                                      .Name = aCast.Name,
                                      .Role = aCast.Character,
                                      .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                      .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                      })
                Next
            End If
        End If

        'Certifications
        If scrapeOptions.Certifications Then
            If Result.Releases IsNot Nothing AndAlso Result.Releases.Countries IsNot Nothing AndAlso Result.Releases.Countries.Count > 0 Then
                For Each cCountry In Result.Releases.Countries
                    If Not String.IsNullOrEmpty(cCountry.Certification) Then
                        Dim CertificationLanguage = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = cCountry.Iso_3166_1.ToLower)
                        If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
                            nMainDetails.Certifications.Add(String.Concat(CertificationLanguage.name, ":", cCountry.Certification))
                        Else
                            _Logger.Warn("Unhandled certification language encountered: {0}", cCountry.Iso_3166_1.ToLower)
                        End If
                    End If
                Next
            End If
        End If

        'Collection ID
        If Result.BelongsToCollection Is Nothing Then
            If Settings.FallBackToEng AndAlso ResultE.BelongsToCollection IsNot Nothing Then
                Dim nFullMovieSetInfo = GetInfo_MovieSet(
                        ResultE.BelongsToCollection.Id,
                        New Structures.ScrapeOptions With {.Plot = True, .Title = True}
                        )
                nMainDetails.UniqueIDs.TMDbCollectionId = nFullMovieSetInfo.UniqueIDs.TMDbId
                If scrapeOptions.Collection Then
                    nMainDetails.AddSet(New MediaContainers.SetDetails With {
                                  .Plot = nFullMovieSetInfo.Plot,
                                  .Title = nFullMovieSetInfo.Title,
                                  .TMDbId = nFullMovieSetInfo.UniqueIDs.TMDbId
                                  })
                End If
            End If
        Else
            Dim nFullMovieSetInfo = GetInfo_MovieSet(
                    Result.BelongsToCollection.Id,
                    New Structures.ScrapeOptions With {.Plot = True, .Title = True}
                    )
            nMainDetails.UniqueIDs.TMDbCollectionId = nFullMovieSetInfo.UniqueIDs.TMDbId
            If scrapeOptions.Collection Then
                nMainDetails.AddSet(New MediaContainers.SetDetails With {
                              .Plot = nFullMovieSetInfo.Plot,
                              .Title = nFullMovieSetInfo.Title,
                              .TMDbId = nFullMovieSetInfo.UniqueIDs.TMDbId
                              })
            End If
        End If

        'Countries
        If scrapeOptions.Countries Then
            If Result.ProductionCountries IsNot Nothing AndAlso Result.ProductionCountries.Count > 0 Then
                For Each aContry As TMDbLib.Objects.Movies.ProductionCountry In Result.ProductionCountries
                    nMainDetails.Countries.Add(aContry.Name)
                Next
            End If
        End If

        'Director / Writer
        If scrapeOptions.Directors OrElse scrapeOptions.Credits Then
            If Result.Credits IsNot Nothing AndAlso Result.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In Result.Credits.Crew
                    If scrapeOptions.Directors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nMainDetails.Directors.Add(aCrew.Name)
                    End If
                    If scrapeOptions.Credits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nMainDetails.Credits.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        'Genres
        If scrapeOptions.Genres Then
            Dim aGenres As List(Of TMDbLib.Objects.General.Genre) = Nothing
            If Result.Genres Is Nothing OrElse (Result.Genres IsNot Nothing AndAlso Result.Genres.Count = 0) Then
                If Settings.FallBackToEng AndAlso ResultE.Genres IsNot Nothing AndAlso ResultE.Genres.Count > 0 Then
                    aGenres = ResultE.Genres
                End If
            Else
                aGenres = Result.Genres
            End If

            If aGenres IsNot Nothing Then
                For Each tGenre As TMDbLib.Objects.General.Genre In aGenres
                    nMainDetails.Genres.Add(tGenre.Name)
                Next
            End If
        End If

        'OriginalTitle
        If scrapeOptions.OriginalTitle Then
            If Result.OriginalTitle Is Nothing OrElse (Result.OriginalTitle IsNot Nothing AndAlso String.IsNullOrEmpty(Result.OriginalTitle)) Then
                If Settings.FallBackToEng AndAlso ResultE.OriginalTitle IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.OriginalTitle) Then
                    nMainDetails.OriginalTitle = ResultE.OriginalTitle
                End If
            Else
                nMainDetails.OriginalTitle = Result.OriginalTitle
            End If
        End If

        'Plot
        If scrapeOptions.Plot Then
            If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                If Settings.FallBackToEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                    nMainDetails.Plot = ResultE.Overview
                End If
            Else
                nMainDetails.Plot = Result.Overview
            End If
        End If

        'Premiered
        If scrapeOptions.Premiered Then
            Dim ScrapedDate As Date?
            If Result.ReleaseDate Is Nothing OrElse (Result.ReleaseDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.ReleaseDate))) Then
                If Settings.FallBackToEng AndAlso ResultE.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.ReleaseDate)) Then
                    ScrapedDate = ResultE.ReleaseDate
                End If
            Else
                ScrapedDate = Result.ReleaseDate
            End If
            nMainDetails.Premiered = Convert_ReleaseDateToPremiered(ScrapedDate)
        End If

        'Rating
        If scrapeOptions.Ratings Then
            nMainDetails.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "themoviedb", .Value = Result.VoteAverage, .Votes = Result.VoteCount})
        End If

        'Runtime
        If scrapeOptions.Runtime Then
            If Result.Runtime Is Nothing OrElse Result.Runtime = 0 Then
                If Settings.FallBackToEng AndAlso ResultE.Runtime IsNot Nothing Then
                    nMainDetails.Runtime = CStr(ResultE.Runtime)
                End If
            Else
                nMainDetails.Runtime = CStr(Result.Runtime)
            End If
        End If

        'Studios
        If scrapeOptions.Studios Then
            If Result.ProductionCompanies IsNot Nothing AndAlso Result.ProductionCompanies.Count > 0 Then
                For Each cStudio In Result.ProductionCompanies
                    nMainDetails.Studios.Add(cStudio.Name)
                Next
            End If
        End If

        'Tagline
        If scrapeOptions.Tagline Then
            If Result.Tagline Is Nothing OrElse (Result.Tagline IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Tagline)) Then
                If Settings.FallBackToEng AndAlso ResultE.Tagline IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Tagline) Then
                    nMainDetails.Tagline = ResultE.Tagline
                End If
            Else
                nMainDetails.Tagline = Result.Tagline
            End If
        End If


        'Tags
        If scrapeOptions.Tags Then
            If Result.Keywords IsNot Nothing AndAlso Result.Keywords.Keywords.Count > 0 Then
                For Each cTag In Result.Keywords.Keywords
                    nMainDetails.Tags.Add(cTag.Name)
                Next
            End If
        End If

        'Title
        If scrapeOptions.Title Then
            If Result.Title Is Nothing OrElse (Result.Title IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Title)) Then
                If Settings.FallBackToEng AndAlso ResultE.Title IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Title) Then
                    nMainDetails.Title = ResultE.Title
                End If
            Else
                nMainDetails.Title = Result.Title
            End If
        End If

        'Trailer
        If scrapeOptions.Trailer OrElse scrapeModifiers.MainTrailer Then
            Dim aTrailers As List(Of TMDbLib.Objects.General.Video) = Nothing
            If Result.Videos Is Nothing OrElse (Result.Videos IsNot Nothing AndAlso Result.Videos.Results.Count = 0) Then
                If Settings.FallBackToEng AndAlso ResultE.Videos IsNot Nothing AndAlso ResultE.Videos.Results.Count > 0 Then
                    aTrailers = ResultE.Videos.Results
                End If
            Else
                aTrailers = Result.Videos.Results
            End If

            If aTrailers IsNot Nothing AndAlso aTrailers.Count > 0 Then
                If scrapeModifiers.MainTrailer Then
                    For Each aVideo In aTrailers.Where(Function(f) f.Site = "YouTube")
                        Dim nTrailer = YouTube.Scraper.GetVideoDetails(aVideo.Key)
                        If nTrailer IsNot Nothing Then
                            nMainDetails.Trailer = String.Concat("http://www.youtube.com/watch?hd=1&v=", aVideo.Key)
                            Exit For
                        End If
                    Next
                End If

                If scrapeOptions.Trailer Then
                    For Each aVideo In aTrailers.Where(Function(f) f.Site = "YouTube")
                        Dim nTrailer = YouTube.Scraper.GetVideoDetails(aVideo.Key)
                        If nTrailer IsNot Nothing Then
                            nTrailer.LongLang = If(String.IsNullOrEmpty(aVideo.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(aVideo.Iso_639_1))
                            nTrailer.Scraper = "TMDb"
                            nTrailer.ShortLang = If(String.IsNullOrEmpty(aVideo.Iso_639_1), String.Empty, aVideo.Iso_639_1)
                            nTrailer.Type = Convert_VideoType(aVideo.Type)
                            nTrailers.Add(nTrailer)
                        End If
                    Next
                End If
            End If
        End If

        Return New Interfaces.AddonResult With {.ScraperResult_Data = nMainDetails, .ScraperResult_Trailers = nTrailers}
    End Function

    Public Function GetInfo_MovieSet(ByVal tmdbID As Integer, ByVal scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nMovieSet As New MediaContainers.MainDetails

        If Not tmdbID = -1 Then
            Dim APIResult As Task(Of TMDbLib.Objects.Collections.Collection)
            Dim APIResultE As Task(Of TMDbLib.Objects.Collections.Collection)

            APIResult = Task.Run(Function() _TMDBApi.GetCollectionAsync(tmdbID))
            If Settings.FallBackToEng Then
                APIResultE = Task.Run(Function() _TMDBApiEN.GetCollectionAsync(tmdbID))
            Else
                APIResultE = APIResult
            End If

            If APIResult Is Nothing OrElse APIResultE Is Nothing Then
                Return Nothing
            End If

            Dim Result As TMDbLib.Objects.Collections.Collection = APIResult.Result
            Dim ResultE As TMDbLib.Objects.Collections.Collection = APIResultE.Result

            If (Result Is Nothing AndAlso Not Settings.FallBackToEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                (Not Result.Id > 0 AndAlso Not Settings.FallBackToEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
                _Logger.Warn(String.Format("[TMDB_Data] [Abort] No API result for TMDB Collection ID [{0}]", tmdbID))
                Return Nothing
            End If

            nMovieSet.UniqueIDs.TMDbId = Result.Id

            'Plot
            If scrapeOptions.Plot Then
                If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                    If Settings.FallBackToEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                        nMovieSet.Plot = HttpUtility.HtmlDecode(ResultE.Overview)
                    End If
                Else
                    nMovieSet.Plot = HttpUtility.HtmlDecode(Result.Overview)
                End If
            End If

            'Title
            If scrapeOptions.Title Then
                If Result.Name Is Nothing OrElse (Result.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Name)) Then
                    If Settings.FallBackToEng AndAlso ResultE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Name) Then
                        'nMovieSet.Title = MovieSetE.Name
                        nMovieSet.Title = ResultE.Name
                    End If
                Else
                    'nMovieSet.Title = MovieSet.Name
                    nMovieSet.Title = Result.Name
                End If
            End If
        Else
            Return Nothing
        End If

        Return nMovieSet
    End Function

    Public Function GetInfo_TVEpisode(ByVal ShowID As Integer, ByVal Aired As String, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nTVEpisode As New MediaContainers.MainDetails
        Dim ShowInfo As TMDbLib.Objects.TvShows.TvShow

        Dim showAPIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
        showAPIResult = Task.Run(Function() _TMDBApi.GetTvShowAsync(ShowID))

        ShowInfo = showAPIResult.Result

        For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In ShowInfo.Seasons
            Dim seasonAPIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
            seasonAPIResult = Task.Run(Function() _TMDBApi.GetTvSeasonAsync(ShowID, aSeason.SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = seasonAPIResult.Result
            Dim EpisodeList As IEnumerable(Of TMDbLib.Objects.Search.TvSeasonEpisode) = SeasonInfo.Episodes.Where(Function(f) CBool(f.AirDate = CDate(Aired)))
            If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
                Return GetInfo_TVEpisode(ShowID, EpisodeList(0).SeasonNumber, EpisodeList(0).EpisodeNumber, FilteredOptions)
            ElseIf EpisodeList.Count > 0 Then
                Return Nothing
            End If
        Next

        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal tmdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvEpisode)
        APIResult = Task.Run(Function() _TMDBApi.GetTvEpisodeAsync(tmdbID, SeasonNumber, EpisodeNumber, TMDbLib.Objects.TvShows.TvEpisodeMethods.Credits Or TMDbLib.Objects.TvShows.TvEpisodeMethods.ExternalIds))

        If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
            Dim EpisodeInfo As TMDbLib.Objects.TvShows.TvEpisode = APIResult.Result

            If EpisodeInfo Is Nothing OrElse EpisodeInfo.Id Is Nothing OrElse Not EpisodeInfo.Id > 0 Then
                _Logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", tmdbID, SeasonNumber, EpisodeNumber))
                Return Nothing
            End If

            Dim nEpisode As MediaContainers.MainDetails = GetInfo_TVEpisode(EpisodeInfo, FilteredOptions)
            Return nEpisode
        Else
            _Logger.Error(String.Format("Can't scrape or episode not found: tmdbID={0}, Season{1}, Episode{2}", tmdbID, SeasonNumber, EpisodeNumber))
            Return Nothing
        End If
    End Function

    Public Function GetInfo_TVEpisode(ByRef EpisodeInfo As TMDbLib.Objects.TvShows.TvEpisode, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nTVEpisode As New MediaContainers.MainDetails

        nTVEpisode.Scrapersource = "TMDB"

        'IDs
        nTVEpisode.UniqueIDs.TMDbId = CInt(EpisodeInfo.Id)
        If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.TvdbId IsNot Nothing Then nTVEpisode.UniqueIDs.TVDbId = CInt(EpisodeInfo.ExternalIds.TvdbId)
        If EpisodeInfo.ExternalIds IsNot Nothing AndAlso EpisodeInfo.ExternalIds.ImdbId IsNot Nothing Then nTVEpisode.UniqueIDs.IMDbId = EpisodeInfo.ExternalIds.ImdbId

        'Episode # Standard
        If EpisodeInfo.EpisodeNumber >= 0 Then
            nTVEpisode.Episode = EpisodeInfo.EpisodeNumber
        End If

        'Season # Standard
        If EpisodeInfo.SeasonNumber >= 0 Then
            nTVEpisode.Season = CInt(EpisodeInfo.SeasonNumber)
        End If

        'Cast (Actors)
        If FilteredOptions.Episodes.Actors Then
            If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Cast IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.Credits.Cast
                    Dim nUniqueID As New MediaContainers.Uniqueid With {
                        .Type = "tmdb",
                        .Value = aCast.Id.ToString}
                    nTVEpisode.Actors.Add(New MediaContainers.Person With {
                                          .Name = aCast.Name,
                                          .Role = aCast.Character,
                                          .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                          .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                          })
                Next
            End If
        End If

        'Aired
        If FilteredOptions.Episodes.Aired Then
            If EpisodeInfo.AirDate IsNot Nothing Then
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
        End If

        'Director / Writer
        If FilteredOptions.Episodes.Credits OrElse FilteredOptions.Episodes.Directors Then
            If EpisodeInfo.Credits IsNot Nothing AndAlso EpisodeInfo.Credits.Crew IsNot Nothing Then
                For Each aCrew As TMDbLib.Objects.General.Crew In EpisodeInfo.Credits.Crew
                    If FilteredOptions.Episodes.Credits AndAlso aCrew.Department = "Writing" AndAlso (aCrew.Job = "Author" OrElse aCrew.Job = "Screenplay" OrElse aCrew.Job = "Writer") Then
                        nTVEpisode.Credits.Add(aCrew.Name)
                    End If
                    If FilteredOptions.Episodes.Directors AndAlso aCrew.Department = "Directing" AndAlso aCrew.Job = "Director" Then
                        nTVEpisode.Directors.Add(aCrew.Name)
                    End If
                Next
            End If
        End If

        'Guest Stars
        If FilteredOptions.Episodes.GuestStars Then
            If EpisodeInfo.GuestStars IsNot Nothing Then
                For Each aCast As TMDbLib.Objects.TvShows.Cast In EpisodeInfo.GuestStars
                    Dim nUniqueID As New MediaContainers.Uniqueid With {
                        .Type = "tmdb",
                        .Value = aCast.Id.ToString}
                    nTVEpisode.GuestStars.Add(New MediaContainers.Person With {
                                              .Name = aCast.Name,
                                              .Role = aCast.Character,
                                              .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                              .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                              })
                Next
            End If
        End If

        'OriginalTitle
        'TODO: implement
        'If FilteredOptions.bEpisodeOriginalTitle Then
        '    If EpisodeInfo.Name IsNot Nothing Then
        '        nTVEpisode.OriginalTitle = EpisodeInfo.Name
        '    End If
        'End If

        'Plot
        If FilteredOptions.Episodes.Plot Then
            If EpisodeInfo.Overview IsNot Nothing Then
                nTVEpisode.Plot = EpisodeInfo.Overview
            End If
        End If

        'Rating
        If FilteredOptions.Ratings Then
            nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "themoviedb", .Value = EpisodeInfo.VoteAverage, .Votes = EpisodeInfo.VoteCount})
        End If

        'ThumbPoster
        If EpisodeInfo.StillPath IsNot Nothing Then
            nTVEpisode.ThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & EpisodeInfo.StillPath
            nTVEpisode.ThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & EpisodeInfo.StillPath
        End If

        'Title
        If FilteredOptions.Episodes.Title Then
            If EpisodeInfo.Name IsNot Nothing Then
                nTVEpisode.Title = EpisodeInfo.Name
            End If
        End If

        Return nTVEpisode
    End Function

    Public Sub GetInfo_TVSeason(ByRef nTVShow As MediaContainers.MainDetails, ByVal ShowID As Integer, ByVal SeasonNumber As Integer, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions)
        Dim nSeason As New MediaContainers.MainDetails

        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
        APIResult = Task.Run(Function() _TMDBApi.GetTvSeasonAsync(ShowID, SeasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

            nSeason.UniqueIDs.TMDbId = CInt(SeasonInfo.Id)
            If SeasonInfo.ExternalIds IsNot Nothing AndAlso SeasonInfo.ExternalIds.TvdbId IsNot Nothing Then nSeason.UniqueIDs.TVDbId = CInt(SeasonInfo.ExternalIds.TvdbId)

            If ScrapeModifiers.withSeasons Then

                'Aired
                If FilteredOptions.Seasons.Aired AndAlso SeasonInfo.AirDate IsNot Nothing Then
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

                'Plot
                If FilteredOptions.Seasons.Plot AndAlso SeasonInfo.Overview IsNot Nothing Then
                    nSeason.Plot = SeasonInfo.Overview
                End If

                'Season #
                If SeasonInfo.SeasonNumber >= 0 Then
                    nSeason.Season = SeasonInfo.SeasonNumber
                End If

                'Title
                If FilteredOptions.Seasons.Title AndAlso SeasonInfo.Name IsNot Nothing Then
                    nSeason.Title = SeasonInfo.Name
                End If

                nTVShow.KnownSeasons.Add(nSeason)
            End If

            If ScrapeModifiers.withEpisodes AndAlso SeasonInfo.Episodes IsNot Nothing Then
                For Each aEpisode As TMDbLib.Objects.Search.TvSeasonEpisode In SeasonInfo.Episodes
                    Dim nEpisode = GetInfo_TVEpisode(ShowID, aEpisode.SeasonNumber, aEpisode.EpisodeNumber, FilteredOptions)
                    If nEpisode IsNot Nothing Then nTVShow.KnownEpisodes.Add(nEpisode)
                Next
            End If
        Else
            _Logger.Error(String.Format("Can't scrape or season not found: ShowID={0}, Season={1}", ShowID, SeasonNumber))
        End If
    End Sub

    Public Function GetInfo_TVSeason(ByVal tmdbID As Integer, ByVal seasonNumber As Integer, ByRef scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvSeason)
        APIResult = Task.Run(Function() _TMDBApi.GetTvSeasonAsync(tmdbID, seasonNumber, TMDbLib.Objects.TvShows.TvSeasonMethods.Credits Or TMDbLib.Objects.TvShows.TvSeasonMethods.ExternalIds))

        If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing Then
            Dim SeasonInfo As TMDbLib.Objects.TvShows.TvSeason = APIResult.Result

            If SeasonInfo Is Nothing OrElse SeasonInfo.Id Is Nothing OrElse Not SeasonInfo.Id > 0 Then
                _Logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", tmdbID, seasonNumber))
                Return Nothing
            End If

            Dim nTVSeason As MediaContainers.MainDetails = GetInfo_TVSeason(SeasonInfo, scrapeOptions)
            Return nTVSeason
        Else
            _Logger.Error(String.Format("Can't scrape or season not found: tmdbID={0}, Season={1}", tmdbID, seasonNumber))
            Return Nothing
        End If
    End Function

    Public Function GetInfo_TVSeason(ByRef seasonInfo As TMDbLib.Objects.TvShows.TvSeason, ByRef scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nTVSeason As New MediaContainers.MainDetails
        nTVSeason.Scrapersource = "TMDB"

        'IDs
        nTVSeason.UniqueIDs.TMDbId = CInt(seasonInfo.Id)
        If seasonInfo.ExternalIds IsNot Nothing AndAlso seasonInfo.ExternalIds.TvdbId IsNot Nothing Then nTVSeason.UniqueIDs.TVDbId = CInt(seasonInfo.ExternalIds.TvdbId)

        'Season #
        If seasonInfo.SeasonNumber >= 0 Then
            nTVSeason.Season = seasonInfo.SeasonNumber
        End If

        'Aired
        If scrapeOptions.Seasons.Aired AndAlso seasonInfo.AirDate IsNot Nothing Then
            Dim ScrapedDate As String = CStr(seasonInfo.AirDate)
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

        'Plot
        If scrapeOptions.Seasons.Plot AndAlso seasonInfo.Overview IsNot Nothing Then
            nTVSeason.Plot = seasonInfo.Overview
        End If

        'Title
        If scrapeOptions.Seasons.Title AndAlso seasonInfo.Name IsNot Nothing Then
            nTVSeason.Title = seasonInfo.Name
        End If

        Return nTVSeason
    End Function
    ''' <summary>
    '''  Scrape TV Show details from TMDB
    ''' </summary>
    ''' <param name="tmdbID">TMDB ID of tv show to be scraped</param>
    ''' <param name="GetPoster">Scrape posters for the movie?</param>
    ''' <returns>True: success, false: no success</returns>
    Public Function GetInfo_TVShow(ByVal tmdbID As Integer, ByRef scrapeModifiers As Structures.ScrapeModifiers, ByRef scrapeOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nTVShow As New MediaContainers.MainDetails

        If tmdbID = -1 Then
            Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
            Dim APIResultE As Task(Of TMDbLib.Objects.TvShows.TvShow)

            'search movie by TMDB ID
            APIResult = Task.Run(Function() _TMDBApi.GetTvShowAsync(tmdbID, TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds Or TMDbLib.Objects.TvShows.TvShowMethods.Keywords))
            If Settings.FallBackToEng Then
                APIResultE = Task.Run(Function() _TMDBApiEN.GetTvShowAsync(tmdbID, TMDbLib.Objects.TvShows.TvShowMethods.ContentRatings Or TMDbLib.Objects.TvShows.TvShowMethods.Credits Or TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds Or TMDbLib.Objects.TvShows.TvShowMethods.Keywords))
            Else
                APIResultE = APIResult
            End If

            If APIResult Is Nothing OrElse APIResultE Is Nothing Then
                Return Nothing
            End If

            Dim Result As TMDbLib.Objects.TvShows.TvShow = APIResult.Result
            Dim ResultE As TMDbLib.Objects.TvShows.TvShow = APIResultE.Result

            If (Result Is Nothing AndAlso Not Settings.FallBackToEng) OrElse (Result Is Nothing AndAlso ResultE Is Nothing) OrElse
                    (Not Result.Id > 0 AndAlso Not Settings.FallBackToEng) OrElse (Not Result.Id > 0 AndAlso Not ResultE.Id > 0) Then
                _Logger.Error(String.Format("Can't scrape or tv show not found: [{0}]", tmdbID))
                Return Nothing
            End If

            nTVShow.Scrapersource = "TMDB"

            'IDs
            nTVShow.UniqueIDs.TMDbId = Result.Id
            If Result.ExternalIds.TvdbId IsNot Nothing Then nTVShow.UniqueIDs.TVDbId = CInt(Result.ExternalIds.TvdbId)
            If Result.ExternalIds.ImdbId IsNot Nothing Then nTVShow.UniqueIDs.IMDbId = Result.ExternalIds.ImdbId

            'Actors
            If scrapeOptions.Actors Then
                If Result.Credits IsNot Nothing AndAlso Result.Credits.Cast IsNot Nothing Then
                    For Each aCast As TMDbLib.Objects.TvShows.Cast In Result.Credits.Cast
                        Dim nUniqueID As New MediaContainers.Uniqueid With {
                            .Type = "tmdb",
                            .Value = aCast.Id.ToString}
                        nTVShow.Actors.Add(New MediaContainers.Person With {
                                           .Name = aCast.Name,
                                           .Role = aCast.Character,
                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ProfilePath), String.Concat(_TMDBApi.Config.Images.BaseUrl, "original", aCast.ProfilePath), String.Empty),
                                           .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                           })
                    Next
                End If
            End If

            'Certifications
            If scrapeOptions.Certifications Then
                If Result.ContentRatings IsNot Nothing AndAlso Result.ContentRatings.Results IsNot Nothing AndAlso Result.ContentRatings.Results.Count > 0 Then
                    For Each aCountry In Result.ContentRatings.Results
                        If Not String.IsNullOrEmpty(aCountry.Rating) Then
                            Dim CertificationLanguage = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = aCountry.Iso_3166_1.ToLower)
                            If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
                                nTVShow.Certifications.Add(String.Concat(CertificationLanguage.name, ":", aCountry.Rating))
                            Else
                                _Logger.Warn("Unhandled certification language encountered: {0}", aCountry.Iso_3166_1.ToLower)
                            End If
                        End If
                    Next
                End If
            End If

            'Countries 'TODO: Change from OriginCountry to ProductionCountries (not yet supported by API)
            'If FilteredOptions.bMainCountry Then
            '    If Show.OriginCountry IsNot Nothing AndAlso Show.OriginCountry.Count > 0 Then
            '        For Each aCountry As String In Show.OriginCountry
            '            nShow.Countries.Add(aCountry)
            '        Next
            '    End If
            'End If

            'Creators
            If scrapeOptions.Creators Then
                If Result.CreatedBy IsNot Nothing Then
                    For Each aCreator As TMDbLib.Objects.TvShows.CreatedBy In Result.CreatedBy
                        nTVShow.Creators.Add(aCreator.Name)
                    Next
                End If
            End If

            'Genres
            If scrapeOptions.Genres Then
                Dim aGenres As List(Of TMDbLib.Objects.General.Genre) = Nothing
                If Result.Genres Is Nothing OrElse (Result.Genres IsNot Nothing AndAlso Result.Genres.Count = 0) Then
                    If Settings.FallBackToEng AndAlso ResultE.Genres IsNot Nothing AndAlso ResultE.Genres.Count > 0 Then
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

            'OriginalTitle
            If scrapeOptions.OriginalTitle Then
                If Result.OriginalName Is Nothing OrElse (Result.OriginalName IsNot Nothing AndAlso String.IsNullOrEmpty(Result.OriginalName)) Then
                    If Settings.FallBackToEng AndAlso ResultE.OriginalName IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.OriginalName) Then
                        nTVShow.OriginalTitle = ResultE.OriginalName
                    End If
                Else
                    nTVShow.OriginalTitle = ResultE.OriginalName
                End If
            End If

            'Plot
            If scrapeOptions.Plot Then
                If Result.Overview Is Nothing OrElse (Result.Overview IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Overview)) Then
                    If Settings.FallBackToEng AndAlso ResultE.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Overview) Then
                        nTVShow.Plot = ResultE.Overview
                    End If
                Else
                    nTVShow.Plot = Result.Overview
                End If
            End If

            'Premiered
            If scrapeOptions.Premiered Then
                Dim ScrapedDate As Date?
                If Result.FirstAirDate Is Nothing OrElse (Result.FirstAirDate IsNot Nothing AndAlso String.IsNullOrEmpty(CStr(Result.FirstAirDate))) Then
                    If Settings.FallBackToEng AndAlso ResultE.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(ResultE.FirstAirDate)) Then
                        ScrapedDate = ResultE.FirstAirDate
                    End If
                Else
                    ScrapedDate = Result.FirstAirDate
                End If
                nTVShow.Premiered = Convert_ReleaseDateToPremiered(ScrapedDate)
            End If

            'Rating
            If scrapeOptions.Ratings Then
                nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "themoviedb", .Value = Result.VoteAverage, .Votes = Result.VoteCount})
            End If

            'Runtime
            If scrapeOptions.Runtime Then
                If Result.EpisodeRunTime Is Nothing OrElse Result.EpisodeRunTime.Count = 0 Then
                    If Settings.FallBackToEng AndAlso ResultE.EpisodeRunTime IsNot Nothing AndAlso ResultE.EpisodeRunTime.Count > 0 Then
                        nTVShow.Runtime = CStr(ResultE.EpisodeRunTime.Item(0))
                    End If
                Else
                    nTVShow.Runtime = CStr(Result.EpisodeRunTime.Item(0))
                End If
            End If

            'Status
            If scrapeOptions.Status Then
                If Result.Status Is Nothing OrElse (Result.Status IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Status)) Then
                    If Settings.FallBackToEng AndAlso ResultE.Status IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Status) Then
                        nTVShow.Status = ResultE.Status
                    End If
                Else
                    nTVShow.Status = Result.Status
                End If
            End If

            'Studios
            If scrapeOptions.Studios Then
                If Result.Networks IsNot Nothing AndAlso Result.Networks.Count > 0 Then
                    For Each aStudio In Result.Networks
                        nTVShow.Studios.Add(aStudio.Name)
                    Next
                End If
            End If

            'Title
            If scrapeOptions.Title Then
                If Result.Name Is Nothing OrElse (Result.Name IsNot Nothing AndAlso String.IsNullOrEmpty(Result.Name)) Then
                    If Settings.FallBackToEng AndAlso ResultE.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(ResultE.Name) Then
                        nTVShow.Title = ResultE.Name
                    End If
                Else
                    nTVShow.Title = Result.Name
                End If
            End If

            'Seasons and Episodes
            If scrapeModifiers.withEpisodes OrElse scrapeModifiers.withSeasons Then
                For Each aSeason As TMDbLib.Objects.Search.SearchTvSeason In Result.Seasons
                    GetInfo_TVSeason(nTVShow, Result.Id, aSeason.SeasonNumber, scrapeModifiers, scrapeOptions)
                Next
            End If
        Else
            Return Nothing
        End If

        Return nTVShow
    End Function

    Public Function GetIMDbIdByTMDbID(ByVal tmdbId As Integer, ByVal contentType As Enums.ContentType) As String
        Try
            Select Case contentType
                Case Enums.ContentType.Movie
                    Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
                    APIResult = Task.Run(Function() _TMDBApiG.GetMovieAsync(tmdbId))
                    Dim Result = APIResult.Result
                    If Result IsNot Nothing AndAlso Result.ImdbId IsNot Nothing Then Return Result.ImdbId
                Case Enums.ContentType.TVShow
                    Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
                    APIResult = Task.Run(Function() _TMDBApiG.GetTvShowAsync(tmdbId, TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
                    Dim Result = APIResult.Result
                    If Result IsNot Nothing AndAlso Result.ExternalIds IsNot Nothing Then Return Result.ExternalIds.ImdbId
            End Select
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return String.Empty
    End Function

    Public Function GetTMDbIDbyIMDbID(ByVal imdbID As String) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _TMDBApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.Imdb, imdbID))

            If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing AndAlso APIResult.Result IsNot Nothing AndAlso
                    APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                Return APIResult.Result.TvResults.Item(0).Id
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetTMDbIDbyTVDbID(ByVal tvdbID As Integer, ByVal tContentType As Enums.ContentType) As Integer
        Try
            Dim APIResult As Task(Of TMDbLib.Objects.Find.FindContainer)
            APIResult = Task.Run(Function() _TMDBApi.FindAsync(TMDbLib.Objects.Find.FindExternalSource.TvDb, CStr(tvdbID)))

            If APIResult IsNot Nothing AndAlso APIResult.Exception Is Nothing Then
                Select Case tContentType
                    Case Enums.ContentType.Movie
                        If APIResult.Result.MovieResults IsNot Nothing AndAlso APIResult.Result.MovieResults.Count > 0 Then
                            Return APIResult.Result.MovieResults.Item(0).Id
                        End If
                    Case Enums.ContentType.TVEpisode
                        If APIResult.Result.TvEpisode IsNot Nothing AndAlso APIResult.Result.TvEpisode.Count > 0 Then
                            Return APIResult.Result.TvEpisode.Item(0).Id
                        End If
                    Case Enums.ContentType.TVSeason
                        If APIResult.Result.TvSeason IsNot Nothing AndAlso APIResult.Result.TvSeason.Count > 0 Then
                            Return APIResult.Result.TvSeason.Item(0).Id
                        End If
                    Case Enums.ContentType.TVShow
                        If APIResult.Result.TvResults IsNot Nothing AndAlso APIResult.Result.TvResults.Count > 0 Then
                            Return APIResult.Result.TvResults.Item(0).Id
                        End If
                End Select
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function GetTVDbIdByTMDbID(ByVal tmdbId As Integer, ByVal contentType As Enums.ContentType) As Integer
        Try
            Select Case contentType
                Case Enums.ContentType.TVShow
                    Dim APIResult As Task(Of TMDbLib.Objects.TvShows.TvShow)
                    APIResult = Task.Run(Function() _TMDBApiG.GetTvShowAsync(tmdbId, TMDbLib.Objects.TvShows.TvShowMethods.ExternalIds))
                    Dim Result = APIResult.Result
                    If Result IsNot Nothing AndAlso Result.ExternalIds IsNot Nothing AndAlso Integer.TryParse(Result.ExternalIds.TvdbId, 0) Then Return CInt(Result.ExternalIds.TvdbId)

            End Select
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return -1
    End Function

    Public Function Scrape_Movie(ByVal id As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Dim nAddonResult As New Interfaces.AddonResult
        nAddonResult = GetInfo_Movie(id, scrapeModifiers, scrapeOptions)
        nAddonResult.ScraperResult_ImageContainer = GetImages_Movie_Movieset(nAddonResult.ScraperResult_Data.UniqueIDs.TMDbId, scrapeModifiers, Enums.ContentType.Movie)
        Return nAddonResult
    End Function

    Public Function Scrape_Movieset(ByVal id As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_MovieSet(id, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_Movie_Movieset(id, scrapeModifiers, Enums.ContentType.Movieset)
        }
    End Function

    Public Function Scrape_TVEpisode(ByVal iShowID As Integer, ByVal strAired As String, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVEpisode(iShowID, strAired, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVEpisode(iShowID, .ScraperResult_Data.Season, .ScraperResult_Data.Episode, scrapeModifiers)
        }
    End Function

    Public Function Scrape_TVEpisode(ByVal showID As Integer, ByVal season As Integer, episode As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVEpisode(showID, season, episode, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVEpisode(showID, season, episode, scrapeModifiers)
        }
    End Function

    Public Function Scrape_TVSeason(ByVal iShowID As Integer, ByVal iSeason As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVSeason(iShowID, iSeason, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVShow(iShowID, scrapeModifiers)
        }
    End Function

    Public Function ScrapeTVShow(ByVal iShowID As Integer, ByVal scrapeModifiers As Structures.ScrapeModifiers, ByVal scrapeOptions As Structures.ScrapeOptions) As Interfaces.AddonResult
        Return New Interfaces.AddonResult With {
            .ScraperResult_Data = GetInfo_TVShow(iShowID, scrapeModifiers, scrapeOptions),
            .ScraperResult_ImageContainer = GetImages_TVShow(iShowID, scrapeModifiers)
        }
    End Function

    Public Function Search_Movie(ByVal strTitle As String, ByVal strYear As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(strTitle) Then Return New List(Of MediaContainers.MainDetails)

        Dim bFallbackToEng As Boolean
        Dim iCurrentPage As Integer = 1
        Dim iTotalPages As Integer
        Dim iYear As Integer = 0
        Dim nResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie)
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)

        Integer.TryParse(strYear, iYear)

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchMovie))
        APIResult = Task.Run(Function() _TMDBApi.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear))

        nResult = APIResult.Result

        If nResult.TotalResults = 0 AndAlso Settings.FallBackToEng Then
            APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear))
            nResult = APIResult.Result
            bFallbackToEng = True
        End If

        'try -1 year if no search result was found
        If nResult.TotalResults = 0 AndAlso iYear > 0 AndAlso Settings.SearchDeviant Then
            APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear - 1))
            nResult = APIResult.Result

            If nResult.TotalResults = 0 AndAlso Settings.FallBackToEng Then
                APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear - 1))
                nResult = APIResult.Result
                bFallbackToEng = True
            End If

            'still no search result, try +1 year
            If nResult.TotalResults = 0 Then
                APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear + 1))
                nResult = APIResult.Result

                If nResult.TotalResults = 0 AndAlso Settings.FallBackToEng Then
                    APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear + 1))
                    nResult = APIResult.Result
                    bFallbackToEng = True
                End If
            End If
        End If

        If nResult.TotalResults > 0 Then
            iTotalPages = nResult.TotalPages
            While iCurrentPage <= iTotalPages AndAlso iCurrentPage <= 3
                If nResult.Results IsNot Nothing Then
                    For Each nMovie In nResult.Results
                        Dim nThumbPoster As New MediaContainers.Image
                        Dim nYear As String = String.Empty
                        If nMovie.PosterPath IsNot Nothing Then
                            nThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & nMovie.PosterPath
                            nThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & nMovie.PosterPath
                        End If
                        If nMovie.ReleaseDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(nMovie.ReleaseDate)) Then nYear = CStr(nMovie.ReleaseDate.Value.Year)

                        nSearchResults.Add(New MediaContainers.MainDetails With {
                                           .OriginalTitle = nMovie.OriginalTitle,
                                           .Plot = nMovie.Overview,
                                           .Premiered = Convert_ReleaseDateToPremiered(nMovie.ReleaseDate),
                                           .Title = nMovie.Title,
                                           .ThumbPoster = nThumbPoster,
                                           .UniqueIDs = New MediaContainers.UniqueidContainer With {
                                           .IMDbId = GetIMDbIdByTMDbID(nMovie.Id, Enums.ContentType.Movie),
                                           .TMDbId = nMovie.Id
                                           }
                                           })
                    Next
                End If
                iCurrentPage = iCurrentPage + 1
                If bFallbackToEng Then
                    APIResult = Task.Run(Function() _TMDBApiEN.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear))
                    nResult = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _TMDBApi.SearchMovieAsync(strTitle, iCurrentPage, Settings.GetAdultItems, iYear))
                    nResult = APIResult.Result
                End If
            End While
        End If

        Return nSearchResults
    End Function

    Public Function Search_MovieSet(ByVal strTitle As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(strTitle) Then Return New List(Of MediaContainers.MainDetails)

        Dim bFallbackToEng As Boolean
        Dim iCurrentPage As Integer = 1
        Dim iTotalPages As Integer
        Dim nResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection)
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchCollection))
        APIResult = Task.Run(Function() _TMDBApi.SearchCollectionAsync(strTitle, iCurrentPage))

        nResult = APIResult.Result

        If nResult.TotalResults = 0 AndAlso Settings.FallBackToEng Then
            APIResult = Task.Run(Function() _TMDBApiEN.SearchCollectionAsync(strTitle, iCurrentPage))
            nResult = APIResult.Result
            bFallbackToEng = True
        End If

        If nResult.TotalResults > 0 Then
            iTotalPages = nResult.TotalPages
            While iCurrentPage <= iTotalPages AndAlso iCurrentPage <= 3
                If nResult.Results IsNot Nothing Then
                    For Each aMovieSet In nResult.Results
                        Dim tThumbPoster As New MediaContainers.Image
                        If aMovieSet.PosterPath IsNot Nothing Then
                            tThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & aMovieSet.PosterPath
                            tThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & aMovieSet.PosterPath
                        End If
                        nSearchResults.Add(New MediaContainers.MainDetails With {
                                               .ThumbPoster = tThumbPoster,
                                               .Title = aMovieSet.Name,
                                               .UniqueIDs = New MediaContainers.UniqueidContainer With {.TMDbId = aMovieSet.Id}
                                               })
                    Next
                End If
                iCurrentPage = iCurrentPage + 1
                If bFallbackToEng Then
                    APIResult = Task.Run(Function() _TMDBApiEN.SearchCollectionAsync(strTitle, iCurrentPage))
                    nResult = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _TMDBApi.SearchCollectionAsync(strTitle, iCurrentPage))
                    nResult = APIResult.Result
                End If
            End While
        End If

        Return nSearchResults
    End Function

    Public Function Search_TVShow(ByVal strTitle As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(strTitle) Then Return New List(Of MediaContainers.MainDetails)

        Dim bFallbackToEng As Boolean
        Dim iCurrentPage As Integer = 1
        Dim iTotalPages As Integer
        Dim nResult As TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv)
        Dim nSearchResults As New List(Of MediaContainers.MainDetails)

        Dim APIResult As Task(Of TMDbLib.Objects.General.SearchContainer(Of TMDbLib.Objects.Search.SearchTv))
        APIResult = Task.Run(Function() _TMDBApi.SearchTvShowAsync(strTitle, iCurrentPage))

        nResult = APIResult.Result

        If nResult.TotalResults = 0 AndAlso Settings.FallBackToEng Then
            APIResult = Task.Run(Function() _TMDBApiEN.SearchTvShowAsync(strTitle, iCurrentPage))
            nResult = APIResult.Result
            bFallbackToEng = True
        End If

        If nResult.TotalResults > 0 Then
            iTotalPages = nResult.TotalPages
            While iCurrentPage <= iTotalPages AndAlso iCurrentPage <= 3
                If nResult.Results IsNot Nothing Then
                    For Each nShow In nResult.Results
                        Dim nPremiered As String = String.Empty
                        Dim nThumbPoster As New MediaContainers.Image
                        If nShow.PosterPath IsNot Nothing Then
                            nThumbPoster.URLOriginal = _TMDBApi.Config.Images.BaseUrl & "original" & nShow.PosterPath
                            nThumbPoster.URLThumb = _TMDBApi.Config.Images.BaseUrl & "w185" & nShow.PosterPath
                        End If
                        If nShow.FirstAirDate IsNot Nothing AndAlso Not String.IsNullOrEmpty(CStr(nShow.FirstAirDate)) Then
                            nPremiered = CStr(nShow.FirstAirDate.Value.Year)
                        End If
                        nSearchResults.Add(New MediaContainers.MainDetails With {
                                           .OriginalTitle = nShow.OriginalName,
                                           .Plot = nShow.Overview,
                                           .Premiered = nPremiered,
                                           .ThumbPoster = nThumbPoster,
                                           .Title = nShow.Name,
                                           .UniqueIDs = New MediaContainers.UniqueidContainer With {
                                           .IMDbId = GetIMDbIdByTMDbID(nShow.Id, Enums.ContentType.TVShow),
                                           .TMDbId = nShow.Id,
                                           .TVDbId = GetTVDbIdByTMDbID(nShow.Id, Enums.ContentType.TVShow)
                                           }
                                           })
                    Next
                End If
                iCurrentPage = iCurrentPage + 1
                If bFallbackToEng Then
                    APIResult = Task.Run(Function() _TMDBApiEN.SearchTvShowAsync(strTitle, iCurrentPage))
                    nResult = APIResult.Result
                Else
                    APIResult = Task.Run(Function() _TMDBApi.SearchTvShowAsync(strTitle, iCurrentPage))
                    nResult = APIResult.Result
                End If
            End While
        End If

        Return nSearchResults
    End Function

#End Region 'Methods 

End Class