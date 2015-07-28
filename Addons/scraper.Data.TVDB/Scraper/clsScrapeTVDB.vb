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
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

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

        Private _TVDBApi As TVDB.Web.WebInterface
        Private _TVDBMirror As TVDB.Model.Mirror
        Private _MySettings As MySettings
        Private strPrivateAPIKey As String = String.Empty
        Private _sPoster As String

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

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

        Public Event SearchInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean)

        Public Event SearchResultsDownloaded(ByVal mResults As TVDBs.SearchResults)

#End Region 'Events

#Region "Methods"

        Public Sub New(ByVal Settings As MySettings)
            Try
                _MySettings = Settings

                _TVDBApi = New TVDB.Web.WebInterface(_MySettings.ApiKey)
                _TVDBMirror = New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Public Sub CancelAsync()
            If bwTVDB.IsBusy Then bwTVDB.CancelAsync()

            While bwTVDB.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Sub

        Public Function GetSearchTVShowInfo(ByVal sShowName As String, ByRef oDBTV As Structures.DBTV, ByRef nShow As MediaContainers.TVShow, ByVal iType As Enums.ScrapeType_Movie_MovieSet_TV, ByVal Options As Structures.ScrapeOptions_TV) As MediaContainers.TVShow
            Dim r As SearchResults = SearchTVShow(sShowName)
            Dim b As Boolean = False

            Select Case iType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAsk, Enums.ScrapeType_Movie_MovieSet_TV.MissAsk, Enums.ScrapeType_Movie_MovieSet_TV.NewAsk, Enums.ScrapeType_Movie_MovieSet_TV.MarkAsk, Enums.ScrapeType_Movie_MovieSet_TV.FilterAsk, Enums.ScrapeType_Movie_MovieSet_TV.SingleField
                    If r.Matches.Count = 1 Then
                        b = GetTVShowInfo(r.Matches.Item(0).TVDB, nShow, False, Options, True, False)
                    Else
                        nShow.Clear()
                        Using dTVDB As New dlgTVDBSearchResults(_MySettings, Me)
                            If dTVDB.ShowDialog(nShow, r, sShowName, oDBTV.ShowPath) = Windows.Forms.DialogResult.OK Then
                                If String.IsNullOrEmpty(nShow.TVDB) Then
                                    b = False
                                Else
                                    b = GetTVShowInfo(r.Matches.Item(0).TVDB, nShow, False, Options, True, False)
                                End If
                            Else
                                b = False
                            End If
                        End Using
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterSkip, Enums.ScrapeType_Movie_MovieSet_TV.FullSkip, Enums.ScrapeType_Movie_MovieSet_TV.MarkSkip, Enums.ScrapeType_Movie_MovieSet_TV.NewSkip, Enums.ScrapeType_Movie_MovieSet_TV.MissSkip
                    If r.Matches.Count = 1 Then
                        b = GetTVShowInfo(r.Matches.Item(0).TVDB, nShow, False, Options, True, False)
                    End If
                Case Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape, Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto
                    If r.Matches.Count > 1 Then
                        b = GetTVShowInfo(r.Matches.Item(0).TVDB, nShow, False, Options, True, False)
                    End If
            End Select

            Return nShow
        End Function

        Private Function SearchTVShow(ByVal sShow As String) As SearchResults
            If String.IsNullOrEmpty(sShow) Then Return New SearchResults
            Dim R As New SearchResults
            Dim Shows As List(Of TVDB.Model.Series)

            Shows = _TVDBApi.GetSeriesByName(sShow, _MySettings.Language, _TVDBMirror).Result
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
        ''' 
        ''' </summary>
        ''' <param name="strID">TVDB ID</param>
        ''' <param name="nShow"></param>
        ''' <param name="GetPoster"></param>
        ''' <param name="Options"></param>
        ''' <param name="IsSearch"></param>
        ''' <param name="withEpisodes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef nShow As MediaContainers.TVShow, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_TV, ByVal IsSearch As Boolean, ByVal withEpisodes As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            'clear nShow from search results
            nShow.Clear()

            If bwTVDB.CancellationPending Then Return Nothing

            Dim Results As TVDB.Model.SeriesDetails = _TVDBApi.GetFullSeriesById(CInt(strID), _MySettings.Language, _TVDBMirror).Result
            If Results Is Nothing Then
                Return Nothing
            End If

            nShow.Scrapersource = "TVDB"
            nShow.TVDB = CStr(Results.Series.Id)
            nShow.IMDB = CStr(Results.Series.IMDBId)

            'Actors
            If Options.bShowActors Then
                If Results.Actors IsNot Nothing Then
                    For Each aCast As TVDB.Model.Actor In Results.Actors.OrderBy(Function(f) f.SortOrder)
                        nShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
                                                                          .Order = aCast.SortOrder, _
                                                                          .Role = aCast.Role, _
                                                                          .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", _TVDBMirror.Address, aCast.ImagePath), String.Empty), _
                                                                          .TVDB = CStr(aCast.Id)})
                    Next
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Genres
            If Options.bShowGenre Then
                Dim aGenres As List(Of String) = Nothing
                If Results.Series.Genre IsNot Nothing Then
                    aGenres = Results.Series.Genre.Split(CChar(",")).ToList
                End If

                If aGenres IsNot Nothing Then
                    For Each tGenre As String In aGenres
                        nShow.Genres.Add(tGenre.Trim)
                    Next
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'MPAA
            If Options.bShowMPAA Then
                nShow.MPAA = Results.Series.ContentRating
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Plot
            If Options.bShowPlot Then
                If Results.Series.Overview IsNot Nothing Then
                    nShow.Plot = Results.Series.Overview
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Posters (only for SearchResult dialog, auto fallback to "en" by TVDB)
            If GetPoster Then
                If Results.Series.Poster IsNot Nothing AndAlso Not String.IsNullOrEmpty(Results.Series.Poster) Then
                    _sPoster = String.Concat(_TVDBMirror.Address, "/banners/", Results.Series.Poster)
                Else
                    _sPoster = String.Empty
                End If
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Premiered
            If Options.bShowPremiered Then
                nShow.Premiered = CStr(Results.Series.FirstAired)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Rating
            If Options.bShowRating Then
                nShow.Rating = CStr(Results.Series.Rating)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Runtime
            If Options.bShowRuntime Then
                nShow.Runtime = CStr(Results.Series.Runtime)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Status
            If Options.bShowStatus Then
                nShow.Status = Results.Series.Status
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Studios
            If Options.bShowStudio Then
                nShow.Studios.Add(Results.Series.Network)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Title
            If Options.bShowTitle Then
                nShow.Title = Results.Series.Name
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Votes
            If Options.bShowVotes Then
                nShow.Votes = CStr(Results.Series.RatingCount)
            End If

            If bwTVDB.CancellationPending Then Return Nothing

            'Seasons and Episodes
            For Each aEpisode As TVDB.Model.Episode In Results.Series.Episodes
                'check if we have already saved season information for this scraped season
                Dim lSeasonList = nShow.KnownSeasons.Where(Function(f) f.Season = aEpisode.SeasonNumber)

                If lSeasonList.Count = 0 Then
                    nShow.KnownSeasons.Add(New MediaContainers.SeasonDetails With {.Season = aEpisode.SeasonNumber, .TVDB = CStr(aEpisode.SeasonId)})
                End If

                If withEpisodes Then
                    Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(aEpisode, Options)
                    nEpisode.Actors.AddRange(nShow.Actors)
                    nShow.KnownEpisodes.Add(nEpisode)
                End If
            Next

            Return True
        End Function

        Public Function GetTVEpisodeInfo(ByRef tvdbID As Integer, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer, ByRef Options As Structures.ScrapeOptions_TV) As MediaContainers.EpisodeDetails
            Try
                Dim Results As TVDB.Model.SeriesDetails = _TVDBApi.GetFullSeriesById(tvdbID, _MySettings.Language, _TVDBMirror).Result
                Dim EpisodeInfo As TVDB.Model.Episode = Results.Series.Episodes.FirstOrDefault(Function(f) f.Number = EpisodeNumber AndAlso f.SeasonNumber = SeasonNumber)
                If Not EpisodeInfo Is Nothing Then
                    Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(EpisodeInfo, Options)
                    Return nEpisode
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                logger.Error(String.Concat("TVDB Scraper: Can't get informations for TV Show with ID: ", tvdbID))
                Return Nothing
            End Try
        End Function

        Public Function GetTVEpisodeInfo(ByRef tvdbID As Integer, ByVal Aired As String, ByRef Options As Structures.ScrapeOptions_TV) As MediaContainers.EpisodeDetails
            Dim Results As TVDB.Model.SeriesDetails = _TVDBApi.GetFullSeriesById(tvdbID, _MySettings.Language, _TVDBMirror).Result
            If Results Is Nothing Then
                Return Nothing
            End If

            Dim EpisodeInfo As TVDB.Model.Episode = Results.Series.Episodes.FirstOrDefault(Function(f) f.FirstAired = CDate(Aired))
            Dim nEpisode As MediaContainers.EpisodeDetails = GetTVEpisodeInfo(EpisodeInfo, Options)

            Return nEpisode
        End Function

        Public Function GetTVEpisodeInfo(ByRef EpisodeInfo As TVDB.Model.Episode, ByRef Options As Structures.ScrapeOptions_TV) As MediaContainers.EpisodeDetails
            Dim nEpisode As New MediaContainers.EpisodeDetails

            'IDs
            nEpisode.TVDB = CStr(EpisodeInfo.Id)
            If EpisodeInfo.IMDBId IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.IMDBId) Then nEpisode.IMDB = EpisodeInfo.IMDBId

            'Episode # Absolute
            If EpisodeInfo.AbsoluteNumber >= 0 Then
                nEpisode.EpisodeAbsolute = EpisodeInfo.AbsoluteNumber
            End If

            'Episode # AirsBeforeEpisode (DisplayEpisode)
            If EpisodeInfo.AirsBeforeEpisode >= 0 Then
                nEpisode.DisplayEpisode = EpisodeInfo.AirsBeforeEpisode
            End If

            'Episode # Combined
            If EpisodeInfo.CombinedEpisodeNumber >= 0 Then
                nEpisode.EpisodeCombined = EpisodeInfo.CombinedEpisodeNumber
            End If

            'Episode # DVD
            If EpisodeInfo.DVDEpisodeNumber >= 0 Then
                nEpisode.EpisodeDVD = EpisodeInfo.DVDEpisodeNumber
            End If

            'Episode # Standard
            If EpisodeInfo.Number >= 0 Then
                nEpisode.Episode = EpisodeInfo.Number
            End If

            'Season # AirsBeforeSeason (DisplaySeason)
            If EpisodeInfo.AirsBeforeSeason >= 0 Then
                nEpisode.DisplaySeason = EpisodeInfo.AirsBeforeSeason
            End If

            'Season # Combined
            If CInt(EpisodeInfo.CombinedSeason) >= 0 Then
                nEpisode.SeasonCombined = EpisodeInfo.CombinedSeason
            End If

            'Season # DVD
            If CInt(EpisodeInfo.DVDSeason) >= 0 Then
                nEpisode.SeasonDVD = EpisodeInfo.DVDSeason
            End If

            'Season # Standard
            If CInt(EpisodeInfo.SeasonNumber) >= 0 Then
                nEpisode.Season = EpisodeInfo.SeasonNumber
            End If

            'Aired
            If Options.bEpAired Then
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
            If Options.bEpCredits Then
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
            If Options.bEpDirector Then
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
            If Options.bEpGuestStars Then
                If EpisodeInfo.GuestStars IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.GuestStars) Then
                    nEpisode.GuestStars.AddRange(StringToListOfPerson(EpisodeInfo.GuestStars))
                End If
            End If

            'Plot
            If Options.bEpPlot Then
                If EpisodeInfo.Overview IsNot Nothing Then
                    nEpisode.Plot = EpisodeInfo.Overview
                End If
            End If

            'Rating
            If Options.bEpRating Then
                nEpisode.Rating = CStr(EpisodeInfo.Rating)
            End If

            'Title
            If Options.bEpTitle Then
                If EpisodeInfo.Name IsNot Nothing Then
                    nEpisode.Title = EpisodeInfo.Name
                End If
            End If

            'Votes
            If Options.bEpVotes Then
                nEpisode.Votes = CStr(EpisodeInfo.RatingCount)
            End If

            Return nEpisode
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

        Public Sub SearchTVShowAsync(ByVal sShow As String, ByVal filterOptions As Structures.ScrapeOptions_TV)

            If Not bwTVDB.IsBusy Then
                bwTVDB.WorkerReportsProgress = False
                bwTVDB.WorkerSupportsCancellation = True
                bwTVDB.RunWorkerAsync(New Arguments With {.Search = SearchType.TVShows, _
                  .Parameter = sShow, .Options = filterOptions, .withEpisodes = False})
            End If
        End Sub

        Public Sub GetSearchTVShowInfoAsync(ByVal tvdbID As String, ByVal Show As MediaContainers.TVShow, ByVal Options As Structures.ScrapeOptions_TV)
            '' The rule is that if there is a tt is an IMDB otherwise is a TVDB
            If Not bwTVDB.IsBusy Then
                bwTVDB.WorkerReportsProgress = False
                bwTVDB.WorkerSupportsCancellation = True
                bwTVDB.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails_TVShow, _
                  .Parameter = tvdbID, .TVShow = Show, .Options = Options})
            End If
        End Sub

        Private Sub bwTVDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTVDB.DoWork
            Dim Args As Arguments = DirectCast(e.Argument, Arguments)
            '' The rule is that if there is a tt is an IMDB otherwise is a TVDB

            Select Case Args.Search
                Case SearchType.TVShows
                    Dim r As SearchResults = SearchTVShow(Args.Parameter)
                    e.Result = New Results With {.ResultType = SearchType.TVShows, .Result = r}

                Case SearchType.SearchDetails_TVShow
                    Dim s As Boolean = GetTVShowInfo(Args.Parameter, Args.TVShow, True, Args.Options, True, Args.withEpisodes)
                    e.Result = New Results With {.ResultType = SearchType.SearchDetails_TVShow, .Success = s}
            End Select
        End Sub

        Private Sub bwTVDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTVDB.RunWorkerCompleted
            Dim Res As Results = DirectCast(e.Result, Results)

            Select Case Res.ResultType
                Case SearchType.TVShows
                    RaiseEvent SearchResultsDownloaded(DirectCast(Res.Result, SearchResults))

                Case SearchType.SearchDetails_TVShow
                    Dim showInfo As SearchResults = DirectCast(Res.Result, SearchResults)
                    RaiseEvent SearchInfoDownloaded(_sPoster, Res.Success)
            End Select
        End Sub


#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim FullCast As Boolean
            Dim FullCrew As Boolean
            Dim Options As Structures.ScrapeOptions_TV
            Dim Parameter As String
            Dim Search As SearchType
            Dim TVShow As MediaContainers.TVShow
            Dim withEpisodes As Boolean
            Dim Year As Integer

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultType As SearchType
            Dim Success As Boolean

#End Region 'Fields

        End Structure

        Structure MySettings

#Region "Fields"

            Dim ApiKey As String
            Dim Language As String

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

