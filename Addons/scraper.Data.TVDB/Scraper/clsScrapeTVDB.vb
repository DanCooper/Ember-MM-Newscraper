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

Imports System.IO
Imports EmberAPI
Imports NLog
Imports System.Xml.Serialization

Namespace TVDBs

    Public Class SearchResults

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

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

        Private _TVDBApi As TVDB.Web.WebInterface
        Private _TVDBMirror As TVDB.Model.Mirror
        Private _SpecialSettings As TVDB_Data.SpecialSettings
        Private _sPoster As String


#End Region 'Fields

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

        Public Event SearchInfoDownloaded(ByVal strPoster As String, ByVal sInfo As MediaContainers.TVShow)

        Public Event SearchResultsDownloaded(ByVal mResults As SearchResults)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal Settings As TVDB_Data.SpecialSettings)
            Try
                _SpecialSettings = Settings

                If Not Directory.Exists(Path.Combine(Master.TempPath, "Shows")) Then Directory.CreateDirectory(Path.Combine(Master.TempPath, "Shows"))
                _TVDBApi = New TVDB.Web.WebInterface(_SpecialSettings.APIKey, Path.Combine(Master.TempPath, "Shows"))
                _TVDBMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

        Public Sub CancelAsync()
            If bwTVDB.IsBusy Then bwTVDB.CancelAsync()

            While bwTVDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        Public Function GetSearchTVShowInfo(ByVal sShowName As String, ByRef oDBTV As Database.DBElement, ByVal iType As Enums.ScrapeType, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.TVShow
            Dim r As SearchResults = SearchTVShowByName(sShowName)

            Select Case iType
                Case Enums.ScrapeType.AllAsk, Enums.ScrapeType.FilterAsk, Enums.ScrapeType.MarkedAsk, Enums.ScrapeType.MissingAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.SelectedAsk, Enums.ScrapeType.SingleField
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TVDB, ScrapeModifiers, FilteredOptions, False)
                    Else
                        Using dlgSearch As New dlgTVDBSearchResults(_SpecialSettings, Me)
                            If dlgSearch.ShowDialog(r, sShowName, oDBTV.ShowPath) = DialogResult.OK Then
                                If Not String.IsNullOrEmpty(dlgSearch.Result.TVDB) Then
                                    Return GetTVShowInfo(dlgSearch.Result.TVDB, ScrapeModifiers, FilteredOptions, False)
                                End If
                            End If
                        End Using
                    End If

                Case Enums.ScrapeType.AllSkip, Enums.ScrapeType.FilterSkip, Enums.ScrapeType.MarkedSkip, Enums.ScrapeType.MissingSkip, Enums.ScrapeType.NewSkip, Enums.ScrapeType.SelectedSkip
                    If r.Matches.Count = 1 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TVDB, ScrapeModifiers, FilteredOptions, False)
                    End If

                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto, Enums.ScrapeType.SingleScrape
                    If r.Matches.Count > 0 Then
                        Return GetTVShowInfo(r.Matches.Item(0).TVDB, ScrapeModifiers, FilteredOptions, False)
                    End If
            End Select

            Return Nothing
        End Function

        Private Function SearchTVShowByName(ByVal sShow As String) As SearchResults
            If String.IsNullOrEmpty(sShow) Then Return New SearchResults
            Dim R As New SearchResults
            Dim Shows As List(Of TVDB.Model.Series)

            Shows = _TVDBApi.GetSeriesByName(sShow, _SpecialSettings.Language, _TVDBMirror).Result
            If Shows Is Nothing Then
                Return Nothing
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
                        Dim lNewShow As MediaContainers.TVShow = New MediaContainers.TVShow(String.Empty, t1, t2)
                        lNewShow.TVDB = CStr(aShow.Id)
                        R.Matches.Add(lNewShow)
                    End If
                Next
            End If

            Return R
        End Function
        ''' <summary>
        ''' Workaround to fix the theTVDB bug
        ''' </summary>
        ''' <param name="tvdbID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Async Function GetFullSeriesById(ByVal tvdbID As Integer) As Task(Of TVDB.Model.SeriesDetails)
            Dim Result As TVDB.Model.SeriesDetails = Await _TVDBApi.GetFullSeriesById(tvdbID, _SpecialSettings.Language, _TVDBMirror)
            Return Result
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strID">TVDB ID</param>
        ''' <param name="GetPoster"></param>
        ''' <param name="FilteredOptions"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTVShowInfo(ByVal strID As String, ByVal ScrapeModifiers As Structures.ScrapeModifiers, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal GetPoster As Boolean) As MediaContainers.TVShow
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return Nothing

            Dim nTVShow As New MediaContainers.TVShow
            Dim strTVDBID As String = String.Empty

            If bwTVDB.CancellationPending Then Return Nothing

            If strID.StartsWith("tt") Then
                strTVDBID = GetTVDBbyIMDB(strID)
            Else
                strTVDBID = strID
            End If

            If String.IsNullOrEmpty(strTVDBID) Then Return Nothing

            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(strTVDBID)))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return Nothing
            End If
            Dim TVShowInfo = APIResult.Result

            nTVShow.Scrapersource = "TVDB"
            nTVShow.TVDB = CStr(TVShowInfo.Series.Id)
            nTVShow.IMDB = CStr(TVShowInfo.Series.IMDBId)

            'Actors
            If FilteredOptions.bMainActors Then
                If TVShowInfo.Actors IsNot Nothing Then
                    For Each aCast As TVDB.Model.Actor In TVShowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                        nTVShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                          .Order = aCast.SortOrder,
                                                                          .Role = aCast.Role,
                                                                          .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _TVDBMirror.Address, aCast.ImagePath), String.Empty),
                                                                          .TVDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'EpisodeGuideURL
            If FilteredOptions.bMainEpisodeGuide Then
                nTVShow.EpisodeGuide.URL = String.Concat(_TVDBMirror.Address, "/api/", _SpecialSettings.APIKey, "/series/", TVShowInfo.Series.Id, "/all/", TVShowInfo.Language, ".zip")
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Genres
            If FilteredOptions.bMainGenres Then
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

            If bwTVDB.CancellationPending Then Return Nothing

            'MPAA
            If FilteredOptions.bMainMPAA Then
                nTVShow.MPAA = TVShowInfo.Series.ContentRating
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Plot
            If FilteredOptions.bMainPlot Then
                If TVShowInfo.Series.Overview IsNot Nothing Then
                    nTVShow.Plot = TVShowInfo.Series.Overview
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TVDB)
            If GetPoster Then
                If TVShowInfo.Series.Poster IsNot Nothing AndAlso Not String.IsNullOrEmpty(TVShowInfo.Series.Poster) Then
                    _sPoster = String.Concat(_TVDBMirror.Address, "/banners/", TVShowInfo.Series.Poster)
                Else
                    _sPoster = String.Empty
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Premiered
            If FilteredOptions.bMainPremiered Then
                nTVShow.Premiered = CStr(TVShowInfo.Series.FirstAired)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Rating
            If FilteredOptions.bMainRating Then
                nTVShow.Rating = CStr(TVShowInfo.Series.Rating)
                nTVShow.Votes = CStr(TVShowInfo.Series.RatingCount)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Runtime
            If FilteredOptions.bMainRuntime Then
                nTVShow.Runtime = CStr(TVShowInfo.Series.Runtime)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Status
            If FilteredOptions.bMainStatus Then
                nTVShow.Status = TVShowInfo.Series.Status
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Studios
            If FilteredOptions.bMainStudios Then
                nTVShow.Studios.Add(TVShowInfo.Series.Network)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Title
            If FilteredOptions.bMainTitle Then
                nTVShow.Title = TVShowInfo.Series.Name
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Seasons and Episodes
            For Each aEpisode As TVDB.Model.Episode In TVShowInfo.Series.Episodes
                If ScrapeModifiers.withSeasons Then
                    'check if we have already saved season information for this scraped season
                    Dim lSeasonList = nTVShow.KnownSeasons.Where(Function(f) f.Season = aEpisode.SeasonNumber)

                    If lSeasonList.Count = 0 Then
                        nTVShow.KnownSeasons.Add(New MediaContainers.SeasonDetails With {.Season = aEpisode.SeasonNumber, .TVDB = CStr(aEpisode.SeasonId)})
                    End If
                End If

                If ScrapeModifiers.withEpisodes Then
                    Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(aEpisode, TVShowInfo, FilteredOptions)
                    nTVShow.KnownEpisodes.Add(nEpisode)
                End If
            Next

            Return nTVShow
        End Function

        Public Function GetTVEpisodeInfo(ByVal tvdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByVal tEpisodeOrdering As Enums.EpisodeOrdering, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Try
                Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(CInt(tvdbID)))
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
                    Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(EpisodeInfo, TVShowInfo, FilteredOptions)
                    Return nEpisode
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                logger.Error(String.Concat("TVDB Scraper: Can't get informations for TV Show with ID: ", tvdbID))
                Return Nothing
            End Try
        End Function

        Public Function GetTVEpisodeInfo(ByVal tvdbID As Integer, ByVal Aired As String, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim dAired As New Date
            If Not Date.TryParse(Aired, dAired) Then Return Nothing

            Dim APIResult As Task(Of TVDB.Model.SeriesDetails) = Task.Run(Function() GetFullSeriesById(tvdbID))
            If APIResult Is Nothing OrElse APIResult.Result Is Nothing Then
                Return Nothing
            End If
            Dim TVShowInfo = APIResult.Result

            Dim EpisodeList As IEnumerable(Of TVDB.Model.Episode) = TVShowInfo.Series.Episodes.Where(Function(f) f.FirstAired = dAired)
            If EpisodeList IsNot Nothing AndAlso EpisodeList.Count = 1 Then
                Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(EpisodeList(0), TVShowInfo, FilteredOptions)
                Return nEpisode
            Else
                Return Nothing
            End If

        End Function

        Public Function GetTVEpisodeInfo(ByRef EpisodeInfo As TVDB.Model.Episode, ByRef TVShowInfo As TVDB.Model.SeriesDetails, ByRef FilteredOptions As Structures.ScrapeOptions) As MediaContainers.EpisodeDetails
            Dim nEpisode As New MediaContainers.EpisodeDetails

            'IDs
            nEpisode.TVDB = CStr(EpisodeInfo.Id)
            If EpisodeInfo.IMDBId IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.IMDBId) Then nEpisode.IMDB = EpisodeInfo.IMDBId

            'Episode # Absolute
            If Not EpisodeInfo.AbsoluteNumber = -1 Then
                nEpisode.EpisodeAbsolute = EpisodeInfo.AbsoluteNumber
            End If

            'Episode # AirsBeforeEpisode (DisplayEpisode)
            If Not EpisodeInfo.AirsBeforeEpisode = -1 Then
                nEpisode.DisplayEpisode = EpisodeInfo.AirsBeforeEpisode
            End If

            'Episode # Combined
            If Not EpisodeInfo.CombinedEpisodeNumber = -1 Then
                nEpisode.EpisodeCombined = EpisodeInfo.CombinedEpisodeNumber
            End If

            'Episode # DVD
            If Not EpisodeInfo.DVDEpisodeNumber = -1 Then
                nEpisode.EpisodeDVD = EpisodeInfo.DVDEpisodeNumber
            End If

            'Episode # Standard
            If Not EpisodeInfo.Number = -1 Then
                nEpisode.Episode = EpisodeInfo.Number
            End If

            'Season # AirsBeforeSeason (DisplaySeason)
            If Not EpisodeInfo.AirsBeforeSeason = -1 Then
                nEpisode.DisplaySeason = EpisodeInfo.AirsBeforeSeason
            End If

            'Season # AirsAfterSeason (DisplaySeason, DisplayEpisode; Special handling like in Kodi)
            If Not CDbl(EpisodeInfo.AirsAfterSeason) = -1 Then
                nEpisode.DisplaySeason = CInt(EpisodeInfo.AirsAfterSeason)
                nEpisode.DisplayEpisode = 4096
            End If

            'Season # Combined
            If Not CInt(EpisodeInfo.CombinedSeason) = -1 Then
                nEpisode.SeasonCombined = EpisodeInfo.CombinedSeason
            End If

            'Season # DVD
            If Not CInt(EpisodeInfo.DVDSeason) = -1 Then
                nEpisode.SeasonDVD = EpisodeInfo.DVDSeason
            End If

            'Season # Standard
            If Not CInt(EpisodeInfo.SeasonNumber) = -1 Then
                nEpisode.Season = EpisodeInfo.SeasonNumber
            End If

            'Actors
            If FilteredOptions.bEpisodeActors Then
                If TVShowInfo.Actors IsNot Nothing Then
                    For Each aCast As TVDB.Model.Actor In TVShowInfo.Actors.Where(Function(f) f.Name IsNot Nothing AndAlso f.Role IsNot Nothing).OrderBy(Function(f) f.SortOrder)
                        nEpisode.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name,
                                                                          .Order = aCast.SortOrder,
                                                                          .Role = aCast.Role,
                                                                          .URLOriginal = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _TVDBMirror.Address, aCast.ImagePath), String.Empty),
                                                                          .TVDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            'Aired
            If FilteredOptions.bEpisodeAired Then
                Dim ScrapedDate As String = CStr(EpisodeInfo.FirstAired)
                If Not String.IsNullOrEmpty(ScrapedDate) Then
                    Dim RelDate As Date
                    If Date.TryParse(ScrapedDate, RelDate) Then
                        'always save date in same date format not depending on users language setting!
                        nEpisode.Aired = RelDate.ToString("yyyy-MM-dd")
                    Else
                        nEpisode.Aired = ScrapedDate
                    End If
                End If
            End If

            'Credits
            If FilteredOptions.bEpisodeCredits Then
                If EpisodeInfo.Writer IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.Writer) Then
                    Dim CreditsList As New List(Of String)
                    Dim charsToTrim() As Char = {"|"c, ","c}
                    CreditsList.AddRange(EpisodeInfo.Writer.Trim(charsToTrim).Split(charsToTrim))
                    For Each aCredits As String In CreditsList
                        nEpisode.Credits.Add(aCredits.Trim)
                    Next
                End If
            End If

            'Writer
            If FilteredOptions.bEpisodeDirectors Then
                If EpisodeInfo.Director IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.Director) Then
                    Dim DirectorsList As New List(Of String)
                    Dim charsToTrim() As Char = {"|"c, ","c}
                    DirectorsList.AddRange(EpisodeInfo.Director.Trim(charsToTrim).Split(charsToTrim))
                    For Each aDirector As String In DirectorsList
                        nEpisode.Directors.Add(aDirector.Trim)
                    Next
                End If
            End If

            'Guest Stars
            If FilteredOptions.bEpisodeGuestStars Then
                If EpisodeInfo.GuestStars IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.GuestStars) Then
                    nEpisode.GuestStars.AddRange(StringToListOfPerson(EpisodeInfo.GuestStars))
                End If
            End If

            'Plot
            If FilteredOptions.bEpisodePlot Then
                If EpisodeInfo.Overview IsNot Nothing Then
                    nEpisode.Plot = EpisodeInfo.Overview
                End If
            End If

            'Rating
            If FilteredOptions.bEpisodeRating Then
                nEpisode.Rating = CStr(EpisodeInfo.Rating)
                nEpisode.Votes = CStr(EpisodeInfo.RatingCount)
            End If

            'ThumbPoster
            If EpisodeInfo.PictureFilename IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.PictureFilename) Then
                nEpisode.ThumbPoster.URLOriginal = String.Concat(_TVDBMirror.Address, "/banners/", EpisodeInfo.PictureFilename)
            End If

            'Title
            If FilteredOptions.bEpisodeTitle Then
                If EpisodeInfo.Name IsNot Nothing Then
                    nEpisode.Title = EpisodeInfo.Name
                End If
            End If

            Return nEpisode
        End Function

        Public Function GetTVDBbyIMDB(ByVal strIMDB As String) As String
            Dim Shows As List(Of TVDB.Model.Series)

            Shows = _TVDBApi.GetSeriesByRemoteId(strIMDB, String.Empty, _TVDBMirror).Result
            If Shows Is Nothing Then
                Return String.Empty
            End If

            If Shows.Count = 1 Then
                Return Shows.Item(0).Id.ToString
            End If

            Return String.Empty
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

        Public Sub SearchTVShowAsync(ByVal sShow As String, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef FilteredOptions As Structures.ScrapeOptions)

            If Not bwTVDB.IsBusy Then
                bwTVDB.WorkerReportsProgress = False
                bwTVDB.WorkerSupportsCancellation = True
                bwTVDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows,
                  .Parameter = sShow, .FilteredOptions = FilteredOptions, .ScrapeModifiers = ScrapeModifiers})
            End If
        End Sub

        Public Sub GetSearchTVShowInfoAsync(ByVal tvdbID As String, ByVal Show As MediaContainers.TVShow, ByVal FilteredOptions As Structures.ScrapeOptions)
            '' The rule is that if there is a tt is an IMDB otherwise is a TVDB
            If Not bwTVDB.IsBusy Then
                bwTVDB.WorkerReportsProgress = False
                bwTVDB.WorkerSupportsCancellation = True
                bwTVDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow,
                  .Parameter = tvdbID, .TVShow = Show, .FilteredOptions = FilteredOptions})
            End If
        End Sub

        Private Sub bwTVDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            '' The rule is that if there is a tt is an IMDB otherwise is a TVDB

            Select Case Args.Search
                Case SearchType.TVShows
                    Dim r As SearchResults = SearchTVShowByName(Args.Parameter)
                    e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

                Case SearchType.SearchDetails_TVShow
                    Dim r As MediaContainers.TVShow = GetTVShowInfo(Args.Parameter, Args.ScrapeModifiers, Args.FilteredOptions, True)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Result = r}
            End Select
        End Sub

        Private Sub bwTVDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Select Case Res.ResultType
                Case SearchType.TVShows
                    RaiseEvent SearchResultsDownloaded(DirectCast(Res.Result, SearchResults))

                Case SearchType.SearchDetails_TVShow
                    Dim showInfo As MediaContainers.TVShow = DirectCast(Res.Result, MediaContainers.TVShow)
                    RaiseEvent SearchInfoDownloaded(_sPoster, showInfo)
            End Select
        End Sub


#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim FilteredOptions As Structures.ScrapeOptions
            Dim Parameter As String
            Dim ScrapeModifiers As Structures.ScrapeModifiers
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

