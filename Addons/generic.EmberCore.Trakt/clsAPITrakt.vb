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

Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports Trakttv

Public Class clsAPITrakt

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _SpecialSettings As New Trakt_Generic._MySettings
    Private _strToken As String = String.Empty

#End Region 'Fields

#Region "Delegates"

    Public Delegate Function ShowProgress(ByVal strTitle As String, ByVal iProgress As Integer) As Boolean

#End Region 'Delegates

#Region "Methods"

    Public Sub New(ByVal SpecialSettings As Trakt_Generic._MySettings)
        _SpecialSettings = SpecialSettings
        Try
            CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    '''  Trakt-Login process for using v2 API (Token based authentification) 
    ''' </summary>
    ''' <param name="strUsername">trakt.tv Username</param>
    ''' <param name="strUsername">trakt.tv Password</param>
    ''' <param name="strToken">trakt.tv Token, may be empty (then it will be generated)</param>
    ''' <returns>(new) trakt.tv Token</returns>
    ''' <remarks>
    ''' 2015/01/17 Cocotus - First implementation of new V2 Authentification process for trakt.tv API
    ''' </remarks>
    Private Sub CreateToken(ByVal strUsername As String, ByVal strPassword As String, ByVal strToken As String)
        ' Use Trakttv wrapper
        Dim account As New TraktAPI.Model.TraktAuthentication
        account.Username = strUsername
        account.Password = strPassword
        TraktSettings.Password = strPassword
        TraktSettings.Username = strUsername

        If String.IsNullOrEmpty(strToken) Then
            Dim response = TraktMethods.LoginToAccount(account)
            If response IsNot Nothing Then
                _strToken = response.Token
            Else
                _strToken = String.Empty
            End If
        Else
            TraktSettings.Token = _strToken
        End If
    End Sub

    Public Function GetRated_Movies() As IEnumerable(Of TraktAPI.Model.TraktMovieRated)
        If String.IsNullOrEmpty(_strToken) Then
            CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
        End If

        If String.IsNullOrEmpty(_strToken) Then Return Nothing

        Dim lRatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated) = TrakttvAPI.GetRatedMovies

        Return lRatedMovies
    End Function

    Public Function GetRated_TVEpisodes() As IEnumerable(Of TraktAPI.Model.TraktEpisodeRated)
        If String.IsNullOrEmpty(_strToken) Then
            CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
        End If

        If String.IsNullOrEmpty(_strToken) Then Return Nothing

        Dim lRatedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeRated) = TrakttvAPI.GetRatedEpisodes

        Return lRatedTVEpisodes
    End Function

    Public Function GetWatched_Movies() As IEnumerable(Of TraktAPI.Model.TraktMovieWatched)
        If String.IsNullOrEmpty(_strToken) Then
            CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
        End If

        If String.IsNullOrEmpty(_strToken) Then Return Nothing

        Dim lWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched) = TrakttvAPI.GetWatchedMovies

        Return lWatchedMovies
    End Function

    Public Function GetWatched_TVEpisodes() As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched)
        If String.IsNullOrEmpty(_strToken) Then
            CreateToken(_SpecialSettings.Username, _SpecialSettings.Password, String.Empty)
        End If

        If String.IsNullOrEmpty(_strToken) Then Return Nothing

        Dim lWatchedTVEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched) = TrakttvAPI.GetWatchedEpisodes

        Return lWatchedTVEpisodes
    End Function

    Public Function GetWatchedRated_Movies() As List(Of TraktAPI.Model.TraktMovieWatchedRated)
        Dim WatchedMovies = GetWatched_Movies()
        Dim RatedMovies = GetRated_Movies()
        Return GetWatchedRated_Movies(WatchedMovies, RatedMovies)
    End Function

    Public Function GetWatchedRated_Movies(ByVal WatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched), ByVal RatedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieRated)) As List(Of TraktAPI.Model.TraktMovieWatchedRated)
        If WatchedMovies Is Nothing Then Return Nothing

        Dim lWatchedRatedMovies As New List(Of TraktAPI.Model.TraktMovieWatchedRated)

        For Each tWatchedMovie In WatchedMovies
            Dim nWatchedRatedMovie As New TraktAPI.Model.TraktMovieWatchedRated
            nWatchedRatedMovie.LastWatchedAt = Functions.ConvertToProperDateTime(tWatchedMovie.LastWatchedAt)
            nWatchedRatedMovie.Movie = tWatchedMovie.Movie
            nWatchedRatedMovie.Plays = tWatchedMovie.Plays

            'get rating
            Dim nRatedMovie As TraktAPI.Model.TraktMovieRated = RatedMovies.FirstOrDefault(Function(f) (f.Movie.Ids.Imdb IsNot Nothing AndAlso f.Movie.Ids.Imdb = tWatchedMovie.Movie.Ids.Imdb) OrElse
                                                                                               (f.Movie.Ids.Tmdb IsNot Nothing AndAlso CInt(f.Movie.Ids.Tmdb) = CInt(tWatchedMovie.Movie.Ids.Tmdb)))
            If nRatedMovie IsNot Nothing Then
                nWatchedRatedMovie.RatedAt = Functions.ConvertToProperDateTime(nRatedMovie.RatedAt)
                nWatchedRatedMovie.Rating = nRatedMovie.Rating
            End If

            lWatchedRatedMovies.Add(nWatchedRatedMovie)
        Next

        Return lWatchedRatedMovies
    End Function

    Public Sub SaveWatchedStateToEmber_Movies(ByVal mywatchedmovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched), Optional ByVal sfunction As ShowProgress = Nothing)
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim i As Integer = 0
            'filter watched movies at trakt.tv to movies with an Unique ID only
            For Each watchedMovie In mywatchedmovies.Where(Function(f) f.Movie.Ids.Imdb IsNot Nothing OrElse
                                                                  f.Movie.Ids.Tmdb IsNot Nothing)
                Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    Dim DateTimeLastPlayedUnix As Double = -1
                    Try
                        Dim DateTimeLastPlayed As Date = Date.ParseExact(Functions.ConvertToProperDateTime(watchedMovie.LastWatchedAt), "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                        DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                    Catch ex As Exception
                        DateTimeLastPlayedUnix = -1
                    End Try

                    'build query, search only with known Unique IDs
                    Dim UniqueIDs As New List(Of String)
                    If watchedMovie.Movie.Ids.Imdb IsNot Nothing Then UniqueIDs.Add(String.Format("IMDB = {0}", Regex.Replace(watchedMovie.Movie.Ids.Imdb, "tt", String.Empty).Trim))
                    If watchedMovie.Movie.Ids.Tmdb IsNot Nothing Then UniqueIDs.Add(String.Format("TMDB = {0}", watchedMovie.Movie.Ids.Tmdb))

                    SQLCommand.CommandText = String.Format("SELECT DISTINCT idMovie FROM movie WHERE ((Playcount IS NULL OR NOT Playcount = {0}) OR (iLastPlayed IS NULL OR NOT iLastPlayed = {1})) AND ({2});", watchedMovie.Plays, DateTimeLastPlayedUnix, String.Join(" OR ", UniqueIDs.ToArray))
                    Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader()
                        While SQLreader.Read
                            Dim tmpMovie As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                            tmpMovie.Movie.PlayCount = watchedMovie.Plays
                            tmpMovie.Movie.LastPlayed = Functions.ConvertToProperDateTime(watchedMovie.LastWatchedAt)
                            Master.DB.Save_Movie(tmpMovie, True, True, False)
                        End While
                    End Using
                End Using
                i += 1
                If sfunction IsNot Nothing Then
                    sfunction(watchedMovie.Movie.Title, i)
                End If
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Public Sub SaveWatchedStateToEmber_TVEpisodes(ByVal myWatchedEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched), Optional ByVal sfunction As ShowProgress = Nothing)
        Using SQLtransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
            Dim i As Integer = 0
            'filter watched tv shows at trakt.tv to tv shows with an Unique ID only
            For Each watchedTVShow In myWatchedEpisodes.Where(Function(f) f.Show.Ids.Imdb IsNot Nothing OrElse
                                                                  f.Show.Ids.Tmdb IsNot Nothing OrElse
                                                                  f.Show.Ids.Tvdb IsNot Nothing)
                For Each watchedTVSeason In watchedTVShow.Seasons
                    For Each watchedTVEpisode In watchedTVSeason.Episodes
                        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            Dim DateTimeLastPlayedUnix As Double = -1
                            Try
                                Dim DateTimeLastPlayed As Date = Date.ParseExact(Functions.ConvertToProperDateTime(watchedTVEpisode.WatchedAt), "yyyy-MM-dd HH:mm:ss", Globalization.CultureInfo.InvariantCulture)
                                DateTimeLastPlayedUnix = Functions.ConvertToUnixTimestamp(DateTimeLastPlayed)
                            Catch ex As Exception
                                DateTimeLastPlayedUnix = -1
                            End Try

                            'build query, search only with known Unique IDs
                            Dim UniqueIDs As New List(Of String)
                            If watchedTVShow.Show.Ids.Tvdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.TVDB = {0}", watchedTVShow.Show.Ids.Tvdb))
                            If watchedTVShow.Show.Ids.Imdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.strIMDB = '{0}'", watchedTVShow.Show.Ids.Imdb))
                            If watchedTVShow.Show.Ids.Tmdb IsNot Nothing Then UniqueIDs.Add(String.Format("tvshow.strTMDB = {0}", watchedTVShow.Show.Ids.Tmdb))

                            SQLCommand.CommandText = String.Concat("SELECT DISTINCT episode.idEpisode FROM episode INNER JOIN tvshow ON (episode.idShow = tvshow.idShow) ",
                                                                   "WHERE NOT idFile = -1 ",
                                                                   "AND (episode.Season = ", watchedTVSeason.Number, " AND episode.Episode = ", watchedTVEpisode.Number, ") ",
                                                                   "AND ((episode.Playcount IS NULL OR NOT episode.Playcount = ", watchedTVEpisode.Plays, ") ",
                                                                   "OR (episode.iLastPlayed IS NULL OR NOT episode.iLastPlayed = ", DateTimeLastPlayedUnix, ")) ",
                                                                   "AND (", String.Join(" OR ", UniqueIDs.ToArray), ");")

                            Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader()
                                While SQLreader.Read
                                    Dim tmpTVEpisode As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), True)
                                    tmpTVEpisode.TVEpisode.Playcount = watchedTVEpisode.Plays
                                    tmpTVEpisode.TVEpisode.LastPlayed = Functions.ConvertToProperDateTime(watchedTVEpisode.WatchedAt)
                                    Master.DB.Save_TVEpisode(tmpTVEpisode, True, True, False, False, True)
                                End While
                            End Using
                        End Using
                    Next
                Next
                i += 1
                If sfunction IsNot Nothing Then
                    sfunction(watchedTVShow.Show.Title, i)
                End If
            Next
            SQLtransaction.Commit()
        End Using
    End Sub

    Public Sub SyncToEmber_All(Optional ByVal sfunction As ShowProgress = Nothing)
        SyncToEmber_Movies(sfunction)
        SyncToEmber_TVEpisodes(sfunction)
    End Sub

    Public Sub SyncToEmber_Movies(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim WatchedMovies = GetWatched_Movies()
        If WatchedMovies IsNot Nothing Then
            SaveWatchedStateToEmber_Movies(WatchedMovies, sfunction)
        End If
    End Sub

    Public Sub SyncToEmber_TVEpisodes(Optional ByVal sfunction As ShowProgress = Nothing)
        Dim WatchedTVEpisodes = GetWatched_TVEpisodes()
        If WatchedTVEpisodes IsNot Nothing Then
            SaveWatchedStateToEmber_TVEpisodes(WatchedTVEpisodes, sfunction)
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
