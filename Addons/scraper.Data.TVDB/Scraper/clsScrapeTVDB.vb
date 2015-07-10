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

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

        Private _sPoster As String

#End Region 'Fields

#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strID">TVDB ID</param>
        ''' <param name="nShow"></param>
        ''' <param name="GetPoster"></param>
        ''' <param name="Options"></param>
        ''' <param name="IsSearch"></param>
        ''' <param name="Settings"></param>
        ''' <param name="withEpisodes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTVShowInfo(ByVal strID As String, ByRef nShow As MediaContainers.TVShow, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions_TV, ByVal IsSearch As Boolean, ByRef Settings As MySettings, ByVal withEpisodes As Boolean) As Boolean
            If String.IsNullOrEmpty(strID) OrElse strID.Length < 2 Then Return False

            'clear nShow from search results
            nShow.Clear()

            Dim tvdbAPI = New TVDB.Web.WebInterface(Settings.ApiKey)
            Dim tvdbMirror As New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

            Dim Results As TVDB.Model.SeriesDetails = tvdbAPI.GetFullSeriesById(CInt(strID), Settings.Language, tvdbMirror).Result
            If Results Is Nothing Then
                Return Nothing
            End If

            nShow.Scrapersource = "TVDB"
            nShow.ID = CStr(Results.Series.Id)
            nShow.IMDB = CStr(Results.Series.IMDBId)

            'Actors
            If Options.bShowActors Then
                If Results.Actors IsNot Nothing Then
                    For Each aCast As TVDB.Model.Actor In Results.Actors.OrderBy(Function(f) f.SortOrder)
                        nShow.Actors.Add(New MediaContainers.Person With {.Name = aCast.Name, _
                                                                          .Order = aCast.SortOrder, _
                                                                          .Role = aCast.Role, _
                                                                          .ThumbURL = If(Not String.IsNullOrEmpty(aCast.ImagePath), String.Format("{0}/banners/{1}", tvdbMirror.Address, aCast.ImagePath), String.Empty), _
                                                                          .TMDB = CStr(aCast.Id)})
                    Next
                End If
            End If

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

            'MPAA
            If Options.bShowMPAA Then
                nShow.MPAA = Results.Series.ContentRating
            End If

            'Plot
            If Options.bShowPlot Then
                If Results.Series.Overview IsNot Nothing Then
                    nShow.Plot = Results.Series.Overview
                End If
            End If

            'Posters (only for SearchResult dialog, auto fallback to "en" by TVDB)
            If GetPoster Then
                If Results.Series.Poster IsNot Nothing AndAlso Not String.IsNullOrEmpty(Results.Series.Poster) Then
                    _sPoster = String.Concat(tvdbMirror.Address, "/banners/", Results.Series.Poster)
                Else
                    _sPoster = String.Empty
                End If
            End If

            'Premiered
            If Options.bShowPremiered Then
                nShow.Premiered = CStr(Results.Series.FirstAired)
            End If

            'Rating
            If Options.bShowRating Then
                nShow.Rating = CStr(Results.Series.Rating)
            End If

            'Runtime
            If Options.bShowRuntime Then
                nShow.Runtime = CStr(Results.Series.Runtime)
            End If

            'Status
            If Options.bShowStatus Then
                nShow.Status = Results.Series.Status
            End If

            'Studios
            If Options.bShowStudio Then
                nShow.Studios.Add(Results.Series.Network)
            End If

            'Title
            If Options.bShowTitle Then
                nShow.Title = Results.Series.Name
            End If

            'Votes
            If Options.bShowVotes Then
                nShow.Votes = CStr(Results.Series.RatingCount)
            End If

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

        Private Function GetTVEpisodeInfo(ByRef EpisodeInfo As TVDB.Model.Episode, ByRef Options As Structures.ScrapeOptions_TV) As MediaContainers.EpisodeDetails
            Dim nEpisode As New MediaContainers.EpisodeDetails

            'IDs
            nEpisode.TVDB = CStr(EpisodeInfo.Id)
            If EpisodeInfo.IMDBId IsNot Nothing AndAlso Not String.IsNullOrEmpty(EpisodeInfo.IMDBId) Then nEpisode.IMDB = EpisodeInfo.IMDBId

            'Episode # Absolute
            If Options.bEpEpisode Then
                If EpisodeInfo.AbsoluteNumber >= 0 Then
                    nEpisode.EpisodeAbsolute = EpisodeInfo.AbsoluteNumber
                End If
            End If

            'Episode # Combined
            If Options.bEpEpisode Then
                If EpisodeInfo.CombinedEpisodeNumber >= 0 Then
                    nEpisode.EpisodeCombined = EpisodeInfo.CombinedEpisodeNumber
                End If
            End If

            'Episode # DVD
            If Options.bEpEpisode Then
                If EpisodeInfo.DVDEpisodeNumber >= 0 Then
                    nEpisode.EpisodeDVD = EpisodeInfo.DVDEpisodeNumber
                End If
            End If

            'Episode # Standard
            If Options.bEpEpisode Then
                If EpisodeInfo.Number >= 0 Then
                    nEpisode.Episode = EpisodeInfo.Number
                End If
            End If

            'Season # Combined
            If Options.bEpSeason Then
                If CInt(EpisodeInfo.CombinedSeason) >= 0 Then
                    nEpisode.SeasonCombined = EpisodeInfo.CombinedSeason
                End If
            End If

            'Season # DVD
            If Options.bEpSeason Then
                If CInt(EpisodeInfo.DVDSeason) >= 0 Then
                    nEpisode.SeasonDVD = EpisodeInfo.DVDSeason
                End If
            End If

            'Season # Standard
            If Options.bEpSeason Then
                If CInt(EpisodeInfo.SeasonNumber) >= 0 Then
                    nEpisode.Season = EpisodeInfo.SeasonNumber
                End If
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


#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim sType As String

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultList As List(Of MediaContainers.Image)

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

