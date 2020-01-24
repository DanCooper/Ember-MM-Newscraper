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

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As AddonSettings
    Private _Poster As String
    Private _TVDBApi As TVDB.Web.WebInterface
    Private _TVDBMirror As TVDB.Model.Mirror

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal AddonSettings As AddonSettings)
        Try
            _AddonSettings = AddonSettings

            If Not Directory.Exists(Path.Combine(Master.TempPath, "Shows")) Then Directory.CreateDirectory(Path.Combine(Master.TempPath, "Shows"))
            _TVDBApi = New TVDB.Web.WebInterface(_AddonSettings.APIKey, Path.Combine(Master.TempPath, "Shows"))
            _TVDBMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id">TVDB ID</param>
    ''' <param name="GetPoster"></param>
    ''' <param name="FilteredOptions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetData_TV(ByVal id As Integer, ByVal ScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal GetPoster As Boolean) As MediaContainers.MainDetails
        Dim nTVShow As New MediaContainers.MainDetails

        Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(id))
        If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
            Return Nothing
        End If
        Dim TVShowInfo = APIResult.Result

        nTVShow.Scrapersource = "TVDB"
        nTVShow.UniqueIDs.TVDbId = TVShowInfo.Series.Id
        nTVShow.UniqueIDs.IMDbId = TVShowInfo.Series.IMDBId

        'Actors
        If FilteredOptions.Actors Then
            If TVShowInfo.Actors IsNot Nothing Then
                For Each aCast As TVDB.Model.Actor In TVShowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                    Dim nUniqueID As New MediaContainers.Uniqueid With {
                            .Type = "tvdb",
                            .Value = aCast.Id.ToString}
                    nTVShow.Actors.Add(New MediaContainers.Person With {
                                           .Name = aCast.Name,
                                           .Order = aCast.SortOrder,
                                           .Role = aCast.Role,
                                           .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _TVDBMirror.Address, aCast.ImagePath), String.Empty),
                                           .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                           })
                Next
            End If
        End If

        'Certifications
        If FilteredOptions.Certifications Then
            If Not String.IsNullOrEmpty(TVShowInfo.Series.ContentRating) Then
                nTVShow.Certifications.Add(String.Concat("USA:", TVShowInfo.Series.ContentRating))
            End If
        End If

        'EpisodeGuideURL
        If FilteredOptions.EpisodeGuideURL Then
            nTVShow.EpisodeGuideURL.URL = String.Concat(_TVDBMirror.Address, "/api/", _AddonSettings.APIKey, "/series/", TVShowInfo.Series.Id, "/all/", TVShowInfo.Language, ".zip")
        End If

        'Genres
        If FilteredOptions.Genres Then
            Dim aGenres As List(Of String) = Nothing
            If TVShowInfo.Series.Genre IsNot Nothing Then
                aGenres = TVShowInfo.Series.Genre.Split(CChar(",")).ToList
            End If

            If aGenres IsNot Nothing Then
                For Each tGenre As String In aGenres
                    nTVShow.Genres.Add(tGenre.Trim)
                Next
            End If
        End If

        'Plot
        If FilteredOptions.Plot Then
            If TVShowInfo.Series.Overview IsNot Nothing Then
                nTVShow.Plot = TVShowInfo.Series.Overview
            End If
        End If

        'Posters (only for SearchResult dialog, auto fallback to "en" by TVDB)
        If GetPoster Then
            If TVShowInfo.Series.Poster IsNot Nothing AndAlso Not String.IsNullOrEmpty(TVShowInfo.Series.Poster) Then
                _Poster = String.Concat(_TVDBMirror.Address, "/banners/", TVShowInfo.Series.Poster)
            Else
                _Poster = String.Empty
            End If
        End If

        'Premiered
        If FilteredOptions.Premiered Then
            nTVShow.Premiered = CStr(TVShowInfo.Series.FirstAired)
        End If

        'Rating
        If FilteredOptions.Ratings Then
            nTVShow.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "thetvdb", .Value = TVShowInfo.Series.Rating, .Votes = TVShowInfo.Series.RatingCount})
        End If

        'Runtime
        If FilteredOptions.Runtime Then
            nTVShow.Runtime = CStr(TVShowInfo.Series.Runtime)
        End If

        'Status
        If FilteredOptions.Status Then
            nTVShow.Status = TVShowInfo.Series.Status
        End If

        'Studios
        If FilteredOptions.Studios Then
            nTVShow.Studios.Add(TVShowInfo.Series.Network)
        End If

        'Title
        If FilteredOptions.Title Then
            nTVShow.Title = TVShowInfo.Series.Name
        End If

        'Seasons and Episodes
        For Each aEpisode As TVDB.Model.Episode In TVShowInfo.Series.Episodes
            If ScrapeModifiers.withSeasons Then
                'check if we have already saved season information for this scraped season
                Dim lSeasonList = nTVShow.KnownSeasons.Where(Function(f) f.Season = aEpisode.SeasonNumber)

                If lSeasonList.Count = 0 Then
                    nTVShow.KnownSeasons.Add(New MediaContainers.MainDetails With {
                                             .Season = aEpisode.SeasonNumber,
                                             .UniqueIDs = New MediaContainers.UniqueidContainer With {.TVDbId = aEpisode.SeasonId}
                                             })
                End If
            End If

            If ScrapeModifiers.withEpisodes Then
                Dim nEpisode As MediaContainers.MainDetails = GetData_TVEpisode(aEpisode, TVShowInfo, FilteredOptions)
                nTVShow.KnownEpisodes.Add(nEpisode)
            End If
        Next

        Return nTVShow
    End Function

    Public Function GetData_TVEpisode(ByVal tvdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByVal tEpisodeOrdering As Enums.EpisodeOrdering, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
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
                Dim nEpisode As MediaContainers.MainDetails = GetData_TVEpisode(EpisodeInfo, TVShowInfo, FilteredOptions)
                Return nEpisode
            Else
                Return Nothing
            End If
        Catch ex As Exception
            _Logger.Error(String.Concat("TVDB Scraper: Can't get informations for TV Show with ID: ", tvdbID))
            Return Nothing
        End Try
    End Function

    Public Function GetData_TVEpisode(ByVal tvdbID As Integer, ByVal Aired As String, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim dAired As New Date
        If Not Date.TryParse(Aired, dAired) Then Return Nothing

        Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbID))
        If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
            Return Nothing
        End If
        Dim TVShowInfo = APIResult.Result

        Dim EpisodeList As IEnumerable(Of TVDB.Model.Episode) = TVShowInfo.Series.Episodes.Where(Function(f) f.FirstAired = dAired)
        If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
            Dim nEpisode As MediaContainers.MainDetails = GetData_TVEpisode(EpisodeList(0), TVShowInfo, FilteredOptions)
            Return nEpisode
        Else
            Return Nothing
        End If

    End Function

    Public Function GetData_TVEpisode(ByRef EpisodeInfo As TVDB.Model.Episode, ByRef TVShowInfo As TVDB.Model.SeriesDetails, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.MainDetails
        Dim nTVEpisode As New MediaContainers.MainDetails

        'IDs
        nTVEpisode.UniqueIDs.TVDbId = EpisodeInfo.Id
        If EpisodeInfo.IMDBId IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.IMDBId) Then nTVEpisode.UniqueIDs.IMDbId = EpisodeInfo.IMDBId

        'Episode # Absolute
        If Not EpisodeInfo.AbsoluteNumber = -1 Then
            nTVEpisode.EpisodeAbsolute = EpisodeInfo.AbsoluteNumber
        End If

        'Episode # AirsBeforeEpisode (DisplayEpisode)
        If Not EpisodeInfo.AirsBeforeEpisode = -1 Then
            nTVEpisode.DisplayEpisode = EpisodeInfo.AirsBeforeEpisode
        End If

        'Episode # Combined
        If Not EpisodeInfo.CombinedEpisodeNumber = -1 Then
            nTVEpisode.EpisodeCombined = EpisodeInfo.CombinedEpisodeNumber
        End If

        'Episode # DVD
        If Not EpisodeInfo.DVDEpisodeNumber = -1 Then
            nTVEpisode.EpisodeDVD = EpisodeInfo.DVDEpisodeNumber
        End If

        'Episode # Standard
        If Not EpisodeInfo.Number = -1 Then
            nTVEpisode.Episode = EpisodeInfo.Number
        End If

        'Season # AirsBeforeSeason (DisplaySeason)
        If Not EpisodeInfo.AirsBeforeSeason = -1 Then
            nTVEpisode.DisplaySeason = EpisodeInfo.AirsBeforeSeason
        End If

        'Season # AirsAfterSeason (DisplaySeason, DisplayEpisode; Special handling like in Kodi)
        If Not CDbl(EpisodeInfo.AirsAfterSeason) = -1 Then
            nTVEpisode.DisplaySeason = EpisodeInfo.AirsAfterSeason
            nTVEpisode.DisplayEpisode = 4096
        End If

        'Season # Combined
        If Not EpisodeInfo.CombinedSeason = -1 Then
            nTVEpisode.SeasonCombined = EpisodeInfo.CombinedSeason
        End If

        'Season # DVD
        If Not EpisodeInfo.DVDSeason = -1 Then
            nTVEpisode.SeasonDVD = EpisodeInfo.DVDSeason
        End If

        'Season # Standard
        If Not EpisodeInfo.SeasonNumber = -1 Then
            nTVEpisode.Season = EpisodeInfo.SeasonNumber
        End If

        'Actors
        If FilteredOptions.Episodes.Actors Then
            If TVShowInfo.Actors IsNot Nothing Then
                For Each aCast As TVDB.Model.Actor In TVShowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                    Dim nUniqueID As New MediaContainers.Uniqueid With {
                            .Type = "tvdb",
                            .Value = aCast.Id.ToString}
                    nTVEpisode.Actors.Add(New MediaContainers.Person With {
                                              .Name = aCast.Name,
                                              .Order = aCast.SortOrder,
                                              .Role = aCast.Role,
                                              .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _TVDBMirror.Address, aCast.ImagePath), String.Empty),
                                              .UniqueIDs = New MediaContainers.UniqueidContainer With {.Items = New List(Of MediaContainers.Uniqueid)(New MediaContainers.Uniqueid() {nUniqueID})}
                                              })
                Next
            End If
        End If

        'Aired
        If FilteredOptions.Episodes.Aired Then
            Dim ScrapedDate As String = CStr(EpisodeInfo.FirstAired)
            If Not String.IsNullOrEmpty(ScrapedDate) Then
                Dim RelDate As Date
                If Date.TryParse(ScrapedDate, RelDate) Then
                    'always save date in same date format not depending on users language setting!
                    nTVEpisode.Aired = RelDate.ToString("yyyy-MM-dd")
                Else
                    nTVEpisode.Aired = ScrapedDate
                End If
            End If
        End If

        'Credits
        If FilteredOptions.Episodes.Credits Then
            If EpisodeInfo.Writer IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.Writer) Then
                Dim CreditsList As New List(Of String)
                Dim charsToTrim() As Char = {"|"c, ","c}
                CreditsList.AddRange(EpisodeInfo.Writer.Trim(charsToTrim).Split(charsToTrim))
                For Each aCredits As String In CreditsList
                    nTVEpisode.Credits.Add(aCredits.Trim)
                Next
            End If
        End If

        'Writer
        If FilteredOptions.Episodes.Directors Then
            If EpisodeInfo.Director IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.Director) Then
                Dim DirectorsList As New List(Of String)
                Dim charsToTrim() As Char = {"|"c, ","c}
                DirectorsList.AddRange(EpisodeInfo.Director.Trim(charsToTrim).Split(charsToTrim))
                For Each aDirector As String In DirectorsList
                    nTVEpisode.Directors.Add(aDirector.Trim)
                Next
            End If
        End If

        'Guest Stars
        If FilteredOptions.Episodes.GuestStars Then
            If EpisodeInfo.GuestStars IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.GuestStars) Then
                nTVEpisode.GuestStars.AddRange(StringToListOfPerson(EpisodeInfo.GuestStars))
            End If
        End If

        'Plot
        If FilteredOptions.Episodes.Plot Then
            If EpisodeInfo.Overview IsNot Nothing Then
                nTVEpisode.Plot = EpisodeInfo.Overview
            End If
        End If

        'Rating
        If FilteredOptions.Episodes.Ratings Then
            nTVEpisode.Ratings.Add(New MediaContainers.RatingDetails With {.Max = 10, .Name = "thetvdb", .Value = EpisodeInfo.Rating, .Votes = EpisodeInfo.RatingCount})
        End If

        'ThumbPoster
        If EpisodeInfo.PictureFilename IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.PictureFilename) Then
            nTVEpisode.ThumbPoster.URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", EpisodeInfo.PictureFilename)
        End If

        'Title
        If FilteredOptions.Episodes.Title Then
            If EpisodeInfo.Name IsNot Nothing Then
                nTVEpisode.Title = EpisodeInfo.Name
            End If
        End If

        Return nTVEpisode
    End Function
    ''' <summary>
    ''' Workaround to fix the theTVDB bug
    ''' </summary>
    ''' <param name="tvdbID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetFullSeriesById(ByVal tvdbID As Integer) As Task(Of TVDB.Model.SeriesDetails)
        Dim Result As TVDB.Model.SeriesDetails = Await _TVDBApi.GetFullSeriesById(tvdbID, _TVDBMirror)
        Return Result
    End Function

    Public Function GetImages_TV(ByVal tvdbID As String, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(tvdbID)))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return alImagesContainer
            End If
            Dim Results = APIResult.Result

            If bwTVDB.CancellationPending Then Return alImagesContainer

            If Results.Banners IsNot Nothing Then

                'EpisodePoster
                If FilteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                    For Each tEpisode As TVDB.Model.Episode In Results.Series.Episodes.Where(Function(f) f.PictureFilename IsNot Nothing)
                        Dim img As New MediaContainers.Image With {
                            .Episode = tEpisode.Number,
                            .Height = tEpisode.ThumbHeight,
                            .Scraper = "TVDB",
                            .Season = tEpisode.SeasonNumber,
                            .Language = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                            .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                            .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                            .Width = tEpisode.ThumbWidth}

                        alImagesContainer.EpisodePosters.Add(img)
                    Next
                End If

                'MainBanner
                If FilteredModifiers.MainBanner Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.series)
                        Dim img As New MediaContainers.Image With {.Height = 140,
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .Language = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = image.Rating,
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = 758}
                        alImagesContainer.MainBanners.Add(img)
                    Next
                End If

                'SeasonBanner
                If FilteredModifiers.SeasonBanner Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {.Height = 140,
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .Language = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = image.Rating,
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = 758}
                        alImagesContainer.SeasonBanners.Add(img)
                    Next
                End If

                'MainFanart
                If FilteredModifiers.MainFanart Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.fanart)
                        alImagesContainer.MainFanarts.Add(New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height,
                                                                                        .Scraper = "TVDB",
                                                                                        .Season = image.Season,
                                                                                        .Language = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                                        .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                                        .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                                        .VoteAverage = image.Rating,
                                                                                        .VoteCount = image.RatingCount,
                                                                                        .Width = StringUtils.StringToSize(image.Dimension).Width})
                    Next
                End If

                'MainPoster
                If FilteredModifiers.MainPoster Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.poster)
                        Dim img As New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height,
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .Language = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .VoteAverage = image.Rating,
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = StringUtils.StringToSize(image.Dimension).Width}
                        alImagesContainer.MainPosters.Add(img)
                    Next
                End If

                'SeasonPoster
                If FilteredModifiers.SeasonPoster Then
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso Not f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {.Height = 578,
                                                                       .Scraper = "TVDB",
                                                                       .Season = image.Season,
                                                                       .Language = If(image.Language IsNot Nothing, image.Language, String.Empty),
                                                                       .URLThumb = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(_TVDBMirror.Address, "/banners/", image.ThumbnailPath), String.Empty),
                                                                       .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", image.BannerPath),
                                                                       .VoteAverage = image.Rating,
                                                                       .VoteCount = image.RatingCount,
                                                                       .Width = 400}
                        alImagesContainer.SeasonPosters.Add(img)
                    Next
                End If

            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function GetImages_TVEpisode(ByVal tvdbID As String, ByVal iSeason As Integer, ByVal iEpisode As Integer, ByVal tEpisodeOrdering As Enums.EpisodeOrdering, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
        Dim alImagesContainer As New MediaContainers.SearchResultsContainer

        Try
            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(tvdbID)))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return alImagesContainer
            End If
            Dim Results = APIResult.Result

            If bwTVDB.CancellationPending Then Return alImagesContainer

            'EpisodePoster
            If FilteredModifiers.EpisodePoster AndAlso Results.Series.Episodes IsNot Nothing Then
                Dim ieEpisodes As IEnumerable(Of TVDB.Model.Episode) = Nothing
                Select Case tEpisodeOrdering
                    Case Enums.EpisodeOrdering.Absolute
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.AbsoluteNumber = iEpisode)
                    Case Enums.EpisodeOrdering.DVD
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.DVDSeason = iSeason And f.DVDEpisodeNumber = iEpisode)
                    Case Enums.EpisodeOrdering.Standard
                        ieEpisodes = Results.Series.Episodes.Where(Function(f) f.SeasonNumber = iSeason And f.Number = iEpisode)
                End Select

                If ieEpisodes IsNot Nothing Then
                    For Each tEpisode As TVDB.Model.Episode In ieEpisodes.Where(Function(f) f.PictureFilename IsNot Nothing)
                        Dim img As New MediaContainers.Image With {
                                .Episode = iEpisode,
                                .Height = tEpisode.ThumbHeight,
                                .Scraper = "TVDB",
                                .Season = iSeason,
                                .Language = If(tEpisode.Language IsNot Nothing, tEpisode.Language, String.Empty),
                                .URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", tEpisode.PictureFilename),
                                .URLThumb = If(Not String.IsNullOrEmpty(tEpisode.PictureFilename), String.Concat(_TVDBMirror.Address, "/banners/_cache/", tEpisode.PictureFilename), String.Empty),
                                .Width = tEpisode.ThumbWidth}

                        alImagesContainer.EpisodePosters.Add(img)
                    Next
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return alImagesContainer
    End Function

    Public Function Get_TVDbID_By_IMDbID(ByVal imdbID As String) As Integer
        Dim Shows As List(Of TVDB.Model.Series)
        Shows = _TVDBApi.GetSeriesByRemoteId(imdbID, String.Empty, _TVDBMirror).Result
        If Shows IsNot Nothing AndAlso Shows.Count = 1 Then
            Return Shows.Item(0).Id
        End If
        Return -1
    End Function

    Private Function SearchTVShowByName(ByVal tvshowTitle As String) As List(Of MediaContainers.MainDetails)
        If String.IsNullOrEmpty(tvshowTitle) Then Return New List(Of MediaContainers.MainDetails)
        Dim R As New List(Of MediaContainers.MainDetails)
        Dim Shows As List(Of TVDB.Model.Series)

        Shows = _TVDBApi.GetSeriesByName(tvshowTitle, _AddonSettings.Language, _TVDBMirror).Result

        If Shows.Count = 0 Then
            'Fallback to Eng
            Shows = _TVDBApi.GetSeriesByName(tvshowTitle, "en", _TVDBMirror).Result
        End If

        If Shows.Count > 0 Then
            Dim t1 As String = String.Empty
            Dim t2 As String = String.Empty
            For Each aShow In Shows
                If aShow.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(aShow.Name) Then
                    t1 = aShow.Name
                    If Not String.IsNullOrEmpty(CStr(aShow.FirstAired)) Then
                        t2 = CStr(aShow.FirstAired.Year)
                    End If
                    Dim lNewShow As MediaContainers.MainDetails = New MediaContainers.MainDetails With {
                            .Premiered = t2,
                            .Title = t1}
                    lNewShow.UniqueIDs.TVDbId = aShow.Id
                    R.Add(lNewShow)
                End If
            Next
        End If

        Return R
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


#End Region 'Methods

End Class