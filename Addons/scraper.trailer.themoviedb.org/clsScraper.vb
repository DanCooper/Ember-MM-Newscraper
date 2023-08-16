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

Public Class Scraper

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _client As TMDbLib.Client.TMDbClient  'preferred language 
    Private _clientE As TMDbLib.Client.TMDbClient 'english language
    Private _addonSettings As Addon.AddonSettings

#End Region 'Fields

#Region "Properties"

    Public Property DefaultLanguage As String
        Get
            Return _client.DefaultLanguage
        End Get
        Set(value As String)
            _client.DefaultLanguage = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Async Function CreateAPI(ByVal addonSettings As Addon.AddonSettings) As Task
        Try
            _addonSettings = addonSettings

            _client = New TMDbLib.Client.TMDbClient(_addonSettings.APIKey)
            Await _client.GetConfigAsync()
            _client.MaxRetryCount = 2
            logger.Trace("[TMDB_Trailer] [CreateAPI] Client created")

            If _addonSettings.FallBackEng Then
                _clientE = New TMDbLib.Client.TMDbClient(_addonSettings.APIKey)
                Await _clientE.GetConfigAsync()
                _clientE.DefaultLanguage = "en-US"
                _clientE.MaxRetryCount = 2
                logger.Trace("[TMDB_Trailer] [CreateAPI] Client-EN created")
            Else
                _clientE = _client
                logger.Trace("[TMDB_Trailer] [CreateAPI] Client-EN = Client")
            End If
        Catch ex As Exception
            logger.Error(String.Format("[TMDB_Trailer] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Public Function GetTrailers(ByVal tmdbID As Integer) As List(Of MediaContainers.MediaFile)
        Dim alTrailers As New List(Of MediaContainers.MediaFile)
        Dim trailers As TMDbLib.Objects.General.ResultContainer(Of TMDbLib.Objects.General.Video)

        If tmdbID = -1 Then Return alTrailers

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        APIResult = Task.Run(Function() _client.GetMovieAsync(tmdbID, TMDbLib.Objects.Movies.MovieMethods.Videos))

        trailers = APIResult.Result.Videos
        If trailers Is Nothing OrElse trailers.Results Is Nothing OrElse trailers.Results.Count = 0 AndAlso _addonSettings.FallBackEng Then
            APIResult = Task.Run(Function() _clientE.GetMovieAsync(tmdbID, TMDbLib.Objects.Movies.MovieMethods.Videos))
            trailers = APIResult.Result.Videos
            If trailers Is Nothing OrElse trailers.Results Is Nothing OrElse trailers.Results.Count = 0 Then
                Return alTrailers
            End If
        End If
        If trailers IsNot Nothing AndAlso trailers.Results IsNot Nothing Then
            For Each Video As TMDbLib.Objects.General.Video In trailers.Results.Where(Function(f) f.Site = "YouTube")
                Dim nTrailer = YouTube.Scraper.GetVideoDetails(Video.Key)
                If nTrailer IsNot Nothing Then
                    nTrailer.LongLanguage = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Localization.Languages.Get_Name_By_Alpha2(Video.Iso_639_1))
                    nTrailer.Scraper = "TMDb"
                    nTrailer.Language = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Video.Iso_639_1)
                    nTrailer.VideoType = GetVideoType(Video.Type)
                    alTrailers.Add(nTrailer)
                End If
            Next
        End If

        Return alTrailers
    End Function

    Private Function GetVideoQuality(ByRef Size As Integer) As Enums.VideoResolution
        Select Case Size
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

    Private Function GetVideoType(ByRef Type As String) As Enums.VideoType
        Select Case Type.ToLower
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

#End Region 'Methods

End Class