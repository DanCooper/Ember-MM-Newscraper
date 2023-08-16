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
Imports System.IO

Public Class Scraper
    Implements Interfaces.IScraper_Search

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents _backgroundWorker As New ComponentModel.BackgroundWorker

    Private _addonSettings As Addon.AddonSettings
    Private _tvdbApi As TVDB.Web.WebInterface
    Private _tvdbMirror As TVDB.Model.Mirror


#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property SearchResults_Movie() As New List(Of MediaContainers.Movie) Implements Interfaces.IScraper_Search.SearchResults_Movie

    Public ReadOnly Property SearchResults_Movieset() As New List(Of MediaContainers.Movieset) Implements Interfaces.IScraper_Search.SearchResults_Movieset

    Public ReadOnly Property SearchResult_TVShow() As New List(Of MediaContainers.TVShow) Implements Interfaces.IScraper_Search.SearchResult_TVShow

#End Region 'Properties

#Region "Enumerations"

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

    Public Sub New(ByVal addonSettingsttings As Addon.AddonSettings)
        Try
            _addonSettings = addonSettingsttings

            If Not Directory.Exists(Path.Combine(Master.TempPath, "Shows")) Then Directory.CreateDirectory(Path.Combine(Master.TempPath, "Shows"))
            _tvdbApi = New TVDB.Web.WebInterface(_addonSettings.APIKey, Path.Combine(Master.TempPath, "Shows"))
            _tvdbMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles _backgroundWorker.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Select Case Args.TaskType
            Case TaskType.GetInfo_Movie
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_Movieset
                e.Result = New Results With {
                    .Result = Nothing,
                    .TaskType = Args.TaskType
                }

            Case TaskType.GetInfo_TVShow
                e.Result = New Results With {
                    .Result = GetInfo_TVShow(Args.Parameter, Args.ScrapeOptions, Args.ScrapeModifiers),
                    .TaskType = Args.TaskType
                }

            Case TaskType.Search_By_Title_Movie
                e.Result = New Results With {
                    .Result = Nothing,
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
                    .Result = Nothing,
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

    Public Function GetInfo_Movie(ByVal uniqueId As String,
                                  ByVal filteredOptions As Structures.ScrapeOptions
                                  ) As MediaContainers.Movie Implements Interfaces.IScraper_Search.GetInfo_Movie
        Return Nothing
    End Function

    Public Function GetInfo_Movieset(ByVal uniqueId As String,
                                     ByVal filteredOptions As Structures.ScrapeOptions
                                     ) As MediaContainers.Movieset Implements Interfaces.IScraper_Search.GetInfo_Movieset
        Return Nothing
    End Function

    Public Function GetInfo_TVEpisode(ByVal tvdbId As Integer,
                                      ByVal aired As String,
                                      ByRef filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.EpisodeDetails
        Dim dAired As New Date
        If Not Date.TryParse(aired, dAired) Then Return Nothing

        Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbId))
        If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
            Return Nothing
        End If
        Dim TVShowInfo = APIResult.Result

        Dim EpisodeList As IEnumerable(Of TVDB.Model.Episode) = TVShowInfo.Series.Episodes.Where(Function(f) f.FirstAired = dAired)
        If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
            Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(EpisodeList(0), TVShowInfo, filteredOptions)
            Return nEpisode
        Else
            Return Nothing
        End If
    End Function

    Public Function GetInfo_TVEpisode(ByVal tvdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByVal tEpisodeOrdering As Enums.EpisodeOrdering, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
        Try
            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbID))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return Nothing
            End If
            Dim TVShowInfo = APIResult.Result

            Dim EpisodeInfo As TVDB.Model.Episode = Nothing

            Select Case tEpisodeOrdering
                Case Enums.EpisodeOrdering.Absolute
                    EpisodeInfo = TVShowInfo.Series.Episodes.FirstOrDefault(Function(f) f.AbsoluteNumber = EpisodeNumber)
                Case Enums.EpisodeOrdering.DVD
                    EpisodeInfo = TVShowInfo.Series.Episodes.FirstOrDefault(Function(f) f.DVDEpisodeNumber = EpisodeNumber AndAlso f.DVDSeason = SeasonNumber)
                Case Enums.EpisodeOrdering.Standard
                    EpisodeInfo = TVShowInfo.Series.Episodes.FirstOrDefault(Function(f) f.Number = EpisodeNumber AndAlso f.SeasonNumber = SeasonNumber)
            End Select

            If Not EpisodeInfo Is Nothing Then
                Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(EpisodeInfo, TVShowInfo, FilteredOptions)
                Return nEpisode
            Else
                Return Nothing
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("TVDB Scraper: Can't get informations for TV Show with ID: ", tvdbID))
            Return Nothing
        End Try
    End Function

    Public Function GetInfo_TVEpisode(ByRef episodeInfo As TVDB.Model.Episode,
                                      ByRef tvshowInfo As TVDB.Model.SeriesDetails,
                                      ByRef filteredOptions As Structures.ScrapeOptions
                                      ) As MediaContainers.EpisodeDetails
        Dim nResult As New MediaContainers.EpisodeDetails

        'IDs
        nResult.UniqueIDs.TVDbId = episodeInfo.Id
        If episodeInfo.IMDBId IsNot Nothing AndAlso Not String.IsNullOrEmpty(episodeInfo.IMDBId) Then nResult.UniqueIDs.IMDbId = episodeInfo.IMDBId

        'Episode # Absolute
        If Not episodeInfo.AbsoluteNumber = -1 Then
            nResult.EpisodeAbsolute = episodeInfo.AbsoluteNumber
        End If

        'Episode # AirsBeforeEpisode (DisplayEpisode)
        If Not episodeInfo.AirsBeforeEpisode = -1 Then
            nResult.DisplayEpisode = episodeInfo.AirsBeforeEpisode
        End If

        'Episode # Combined
        If Not episodeInfo.CombinedEpisodeNumber = -1 Then
            nResult.EpisodeCombined = episodeInfo.CombinedEpisodeNumber
        End If

        'Episode # DVD
        If Not episodeInfo.DVDEpisodeNumber = -1 Then
            nResult.EpisodeDVD = episodeInfo.DVDEpisodeNumber
        End If

        'Episode # Standard
        If Not episodeInfo.Number = -1 Then
            nResult.Episode = episodeInfo.Number
        End If

        'Season # AirsBeforeSeason (DisplaySeason)
        If Not episodeInfo.AirsBeforeSeason = -1 Then
            nResult.DisplaySeason = episodeInfo.AirsBeforeSeason
        End If

        'Season # AirsAfterSeason (DisplaySeason, DisplayEpisode; Special handling like in Kodi)
        If Not CDbl(episodeInfo.AirsAfterSeason) = -1 Then
            nResult.DisplaySeason = episodeInfo.AirsAfterSeason
            nResult.DisplayEpisode = 4096
        End If

        'Season # Combined
        If Not episodeInfo.CombinedSeason = -1 Then
            nResult.SeasonCombined = episodeInfo.CombinedSeason
        End If

        'Season # DVD
        If Not episodeInfo.DVDSeason = -1 Then
            nResult.SeasonDVD = episodeInfo.DVDSeason
        End If

        'Season # Standard
        If Not episodeInfo.SeasonNumber = -1 Then
            nResult.Season = episodeInfo.SeasonNumber
        End If

        'Actors
        If filteredOptions.bEpisodeActors Then
            If tvshowInfo.Actors IsNot Nothing Then
                For Each aCast As TVDB.Model.Actor In tvshowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                    nResult.Actors.Add(New MediaContainers.Person With {
                                            .Name = aCast.Name,
                                            .Order = aCast.SortOrder,
                                            .Role = aCast.Role,
                                            .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _tvdbMirror.Address, aCast.ImagePath), String.Empty),
                                            .TVDbId = CStr(aCast.Id)
                                            })
                Next
            End If
        End If

        'Aired 
        If filteredOptions.bEpisodeAired Then
            If Not episodeInfo.FirstAired = Date.MinValue Then
                'always save date in same date format not depending on users language setting!
                nResult.Aired = episodeInfo.FirstAired.ToString("yyyy-MM-dd")
            End If
        End If

        'Credits
        If filteredOptions.bEpisodeCredits Then
            If episodeInfo.Writer IsNot Nothing AndAlso Not String.IsNullOrEmpty(episodeInfo.Writer) Then
                Dim CreditsList As New List(Of String)
                Dim charsToTrim() As Char = {"|"c, ","c}
                CreditsList.AddRange(episodeInfo.Writer.Trim(charsToTrim).Split(charsToTrim))
                For Each aCredits As String In CreditsList
                    nResult.Credits.Add(aCredits.Trim)
                Next
            End If
        End If

        'Writer
        If filteredOptions.bEpisodeDirectors Then
            If episodeInfo.Director IsNot Nothing AndAlso Not String.IsNullOrEmpty(episodeInfo.Director) Then
                Dim DirectorsList As New List(Of String)
                Dim charsToTrim() As Char = {"|"c, ","c}
                DirectorsList.AddRange(episodeInfo.Director.Trim(charsToTrim).Split(charsToTrim))
                For Each aDirector As String In DirectorsList
                    nResult.Directors.Add(aDirector.Trim)
                Next
            End If
        End If

        'Guest Stars
        If filteredOptions.bEpisodeGuestStars Then
            If episodeInfo.GuestStars IsNot Nothing AndAlso Not String.IsNullOrEmpty(episodeInfo.GuestStars) Then
                nResult.GuestStars.AddRange(StringToListOfPerson(episodeInfo.GuestStars))
            End If
        End If

        'Plot
        If filteredOptions.bEpisodePlot Then
            If episodeInfo.Overview IsNot Nothing Then
                nResult.Plot = episodeInfo.Overview
            ElseIf _addonSettings.FallBackEng Then
                Dim intTVShowId = tvshowInfo.Series.Id
                Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(intTVShowId, "en"))
                If APIResult IsNot Nothing AndAlso APIResult.Result IsNot Nothing Then
                    'find episode
                    Dim intEpisodeId = episodeInfo.Id
                    Dim nEpisodeInfoEN = APIResult.Result.Series.Episodes.FirstOrDefault(Function(f) f.Id = intEpisodeId)
                    If nEpisodeInfoEN IsNot Nothing AndAlso Not String.IsNullOrEmpty(nEpisodeInfoEN.Overview) Then
                        nResult.Plot = nEpisodeInfoEN.Overview
                    End If
                End If
            End If
        End If

        'Rating
        If filteredOptions.bMainRating Then
            If episodeInfo.Rating > 0 AndAlso episodeInfo.RatingCount > 0 Then
                nResult.Ratings.Add(New MediaContainers.RatingDetails With {
                                         .Max = 10,
                                         .Type = "tvdb",
                                         .Value = episodeInfo.Rating,
                                         .Votes = episodeInfo.RatingCount
                                         })
            End If
        End If

        'ThumbPoster
        If episodeInfo.PictureFilename IsNot Nothing AndAlso Not String.IsNullOrEmpty(episodeInfo.PictureFilename) Then
            nResult.ThumbPoster.URLOriginal = String.Concat(_tvdbMirror.Address, "/banners/", episodeInfo.PictureFilename)
        End If

        'Title
        If filteredOptions.bEpisodeTitle Then
            If episodeInfo.Name IsNot Nothing Then
                nResult.Title = episodeInfo.Name
            End If
        End If

        Return nResult
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tvdbOrImdbId">TVDB ID</param>
    ''' <param name="GetPoster"></param>
    ''' <param name="filteredOptions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInfo_TVShow(ByVal tvdbOrImdbId As String,
                                   ByVal filteredOptions As Structures.ScrapeOptions,
                                   ByVal scrapeModifiers As Structures.ScrapeModifiers
                                   ) As MediaContainers.TVShow Implements Interfaces.IScraper_Search.GetInfo_TVShow
        If String.IsNullOrEmpty(tvdbOrImdbId) OrElse tvdbOrImdbId.Length < 2 Then Return Nothing

        Dim nResult As New MediaContainers.TVShow
        Dim intTvdbId As Integer = -1

        If _backgroundWorker.CancellationPending Then Return Nothing

        If tvdbOrImdbId.StartsWith("tt") Then
            intTvdbId = GetTVDBbyIMDB(tvdbOrImdbId)
        ElseIf Integer.TryParse(tvdbOrImdbId, 0) Then
            intTvdbId = CInt(tvdbOrImdbId)
        End If

        If intTvdbId = -1 Then Return Nothing

        Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(intTvdbId))
        If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
            Return Nothing
        End If
        Dim TVShowInfo = APIResult.Result

        nResult.Scrapersource = "TVDB"
        nResult.UniqueIDs.TVDbId = TVShowInfo.Series.Id
        nResult.UniqueIDs.IMDbId = TVShowInfo.Series.IMDBId

        'Actors
        If filteredOptions.bMainActors Then
            If TVShowInfo.Actors IsNot Nothing Then
                For Each aCast As TVDB.Model.Actor In TVShowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                    nResult.Actors.Add(New MediaContainers.Person With {
                                           .Name = aCast.Name,
                                           .Order = aCast.SortOrder,
                                           .Role = aCast.Role,
                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _tvdbMirror.Address, aCast.ImagePath), String.Empty),
                                           .TVDbId = CStr(aCast.Id)
                                           })
                Next
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'EpisodeGuideURL
        If filteredOptions.bMainEpisodeGuide Then
            nResult.EpisodeGuide.URL = String.Concat(_tvdbMirror.Address, "/api/", _addonSettings.APIKey, "/series/", TVShowInfo.Series.Id, "/all/", TVShowInfo.Language, ".zip")
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Genres
        If filteredOptions.bMainGenres Then
            Dim aGenres As List(Of String) = Nothing
            If TVShowInfo.Series.Genre IsNot Nothing Then
                aGenres = TVShowInfo.Series.Genre.Split(CChar(",")).ToList
            End If

            If aGenres IsNot Nothing Then
                For Each tGenre As String In aGenres
                    nResult.Genres.Add(tGenre.Trim)
                Next
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'MPAA
        If filteredOptions.bMainMPAA Then
            nResult.MPAA = TVShowInfo.Series.ContentRating
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Plot
        If filteredOptions.bMainPlot Then
            If TVShowInfo.Series.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(TVShowInfo.Series.Overview) Then
                nResult.Plot = TVShowInfo.Series.Overview
            ElseIf _addonSettings.FallBackEng Then
                'looks like TVDb does an auto fallback to EN, so this is only used as backup
                Dim intTVShowId = TVShowInfo.Series.Id
                Dim APIResultEN As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(intTVShowId, "en"))
                If APIResultEN IsNot Nothing AndAlso APIResultEN.Result IsNot Nothing AndAlso
                        APIResultEN.Result.Series.Overview IsNot Nothing AndAlso Not String.IsNullOrEmpty(APIResultEN.Result.Series.Overview) Then
                    nResult.Plot = APIResultEN.Result.Series.Overview
                End If
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Poster (used only for SearchResults dialog, auto fallback to "en" by TVDb)
        If TVShowInfo.Series.Poster IsNot Nothing AndAlso Not String.IsNullOrEmpty(TVShowInfo.Series.Poster) Then
            nResult.ThumbPoster.URLOriginal = String.Concat(_tvdbMirror.Address, "/banners/", TVShowInfo.Series.Poster)
            nResult.ThumbPoster.URLThumb = nResult.ThumbPoster.URLOriginal
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Premiered
        If filteredOptions.bMainPremiered Then
            If Not TVShowInfo.Series.FirstAired = Date.MinValue Then
                'always save date in same date format not depending on users language setting!
                nResult.Premiered = TVShowInfo.Series.FirstAired.ToString("yyyy-MM-dd")
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Rating
        If filteredOptions.bMainRating Then
            If TVShowInfo.Series.Rating > 0 AndAlso TVShowInfo.Series.RatingCount > 0 Then
                nResult.Ratings.Add(New MediaContainers.RatingDetails With {
                                        .Max = 10,
                                        .Type = "tvdb",
                                        .Value = TVShowInfo.Series.Rating,
                                        .Votes = TVShowInfo.Series.RatingCount
                                        })
            End If
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Runtime
        If filteredOptions.bMainRuntime Then
            nResult.Runtime = CStr(TVShowInfo.Series.Runtime)
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Status
        If filteredOptions.bMainStatus Then
            nResult.Status = TVShowInfo.Series.Status
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Studios
        If filteredOptions.bMainStudios Then
            nResult.Studios.Add(TVShowInfo.Series.Network)
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Title
        If filteredOptions.bMainTitle Then
            nResult.Title = TVShowInfo.Series.Name
        End If

        If _backgroundWorker.CancellationPending Then Return Nothing

        'Seasons and Episodes
        For Each aEpisode As TVDB.Model.Episode In TVShowInfo.Series.Episodes
            If scrapeModifiers.withSeasons Then
                'check if we have already saved season information for this scraped season
                Dim lSeasonList = nResult.KnownSeasons.Where(Function(f) f.Season = aEpisode.SeasonNumber)

                If lSeasonList.Count = 0 Then
                    nResult.KnownSeasons.Add(New MediaContainers.SeasonDetails With {
                                                 .Season = aEpisode.SeasonNumber,
                                                 .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVSeason) With {.TVDbId = aEpisode.SeasonId}
                                                 })
                End If
            End If

            If scrapeModifiers.withEpisodes Then
                Dim nEpisode As MediaContainers.EpisodeDetails = GetInfo_TVEpisode(aEpisode, TVShowInfo, filteredOptions)
                nResult.KnownEpisodes.Add(nEpisode)
            End If
        Next

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

    Public Function Process_SearchResults_TVShow(ByVal title As String,
                                                 ByRef oDbElement As Database.DBElement,
                                                 ByVal type As Enums.ScrapeType,
                                                 ByRef filteredOptions As Structures.ScrapeOptions,
                                                 ByRef scrapeModifiers As Structures.ScrapeModifiers
                                                 ) As MediaContainers.TVShow
        Dim SearchResults = Search_TVShow(title)

        Select Case type
            Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId.ToString, filteredOptions, scrapeModifiers)
                Else
                    Using dlgSearch As New dlgSearchResults(Me, "tmdb", New List(Of String) From {"TVDb", "IMDb"}, Enums.ContentType.TVShow)
                        If dlgSearch.ShowDialog(title, oDbElement.ShowPath, SearchResults) = DialogResult.OK Then
                            If dlgSearch.Result_TVShow.UniqueIDs.TVDbIdSpecified Then
                                Return GetInfo_TVShow(dlgSearch.Result_TVShow.UniqueIDs.TVDbId.ToString, filteredOptions, scrapeModifiers)
                            End If
                        End If
                    End Using
                End If

            Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                If SearchResults.Count = 1 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId.ToString, filteredOptions, scrapeModifiers)
                End If

            Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                If SearchResults.Count > 0 Then
                    Return GetInfo_TVShow(SearchResults.Item(0).UniqueIDs.TVDbId.ToString, filteredOptions, scrapeModifiers)
                End If
        End Select

        Return Nothing
    End Function

    Private Function Search_TVShow(ByVal title As String) As List(Of MediaContainers.TVShow)
        If String.IsNullOrEmpty(title) Then Return New List(Of MediaContainers.TVShow)
        Dim SearchResults As New List(Of MediaContainers.TVShow)

        Dim ApiResult = _tvdbApi.GetSeriesByName(title, _addonSettings.Language, _tvdbMirror).Result
        If ApiResult Is Nothing Then
            Return Nothing
        End If

        If ApiResult.Count > 0 Then
            Dim strTitle As String = String.Empty
            Dim strPremiered As String = String.Empty
            For Each aShow In ApiResult
                If aShow.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(aShow.Name) Then
                    strTitle = aShow.Name
                    If Not String.IsNullOrEmpty(aShow.FirstAired.ToString) Then
                        strPremiered = aShow.FirstAired.Year.ToString
                    End If
                    SearchResults.Add(New MediaContainers.TVShow With {
                                      .Premiered = strPremiered,
                                      .Title = strTitle,
                                      .UniqueIDs = New MediaContainers.UniqueidContainer(Enums.ContentType.TVShow) With {.TVDbId = aShow.Id}
                                      })
                End If
            Next
        End If

        Return SearchResults
    End Function
    ''' <summary>
    ''' Workaround to fix the theTVDB bug
    ''' </summary>
    ''' <param name="tvdbID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetFullSeriesById(ByVal tvdbID As Integer, Optional overwriteLanguage As String = "") As Task(Of TVDB.Model.SeriesDetails)
        Dim strLanguage As String = String.Empty
        If Not String.IsNullOrEmpty(overwriteLanguage) Then
            strLanguage = overwriteLanguage
        Else
            strLanguage = _addonSettings.Language
        End If
        Return Await _tvdbApi.GetFullSeriesById(tvdbID, strLanguage, _tvdbMirror)
    End Function

    Public Function GetTVDBbyIMDB(ByVal imdbId As String) As Integer
        Dim Shows As List(Of TVDB.Model.Series)

        Shows = _tvdbApi.GetSeriesByRemoteId(imdbId, String.Empty, _tvdbMirror).Result
        If Shows Is Nothing Then
            Return -1
        End If

        If Shows.Count = 1 Then
            Return Shows.Item(0).Id
        End If

        Return -1
    End Function

    Private Function StringToListOfPerson(ByVal strActors As String) As List(Of MediaContainers.Person)
        Dim gActors As New List(Of MediaContainers.Person)
        Dim gRole As String = Master.eLang.GetString(947, "Guest Star")

        Dim GuestStarsList As New List(Of String)
        Dim charsToTrim() As Char = {"|"c, ","c}
        GuestStarsList.AddRange(strActors.Trim(charsToTrim).Split(charsToTrim))

        For Each aGuestStar As String In GuestStarsList
            gActors.Add(New MediaContainers.Person With {.Name = aGuestStar.Trim, .Role = gRole})
        Next

        Return gActors
    End Function

    Public Sub SearchAsync_By_Title_Movie(ByVal title As String,
                                          Optional ByVal year As Integer = Nothing
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
                                           Optional ByVal year As Integer = Nothing
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