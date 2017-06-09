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
Imports System.Text.RegularExpressions
Imports NLog

<Serializable()> _
Public Class Trailers
    Implements IDisposable

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _ext As String
    Private _ms As MemoryStream

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"
    ''' <summary>
    ''' trailer extention
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Extention() As String
        Get
            Return _ext
        End Get
        Set(ByVal value As String)
            _ext = value
        End Set
    End Property

    Public ReadOnly Property hasMemoryStream() As Boolean
        Get
            Return _ms IsNot Nothing
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer, ByVal strInfo As String)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        If _ms IsNot Nothing Then
            Dispose(True)
            disposedValue = False    'Since this is not a real Dispose call...
        End If

        _ext = String.Empty
    End Sub

    Public Sub Cancel()
        'Me.WebPage.Cancel()
    End Sub
    ''' <summary>
    ''' Delete the given arbitrary file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Public Shared Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Param: <" & sPath & ">")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete the movie trailers
    ''' </summary>
    ''' <param name="tDBElement"><c>tDBElement</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_Movie(ByVal tDBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        If String.IsNullOrEmpty(tDBElement.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTrailer, ForceFileCleanup)
                For Each t As String In Master.eSettings.FileSystemValidExts
                    If File.Exists(String.Concat(a, t)) Then
                        Delete(String.Concat(a, t))
                    End If
                Next
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & tDBElement.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="iPercent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent, String.Empty)
    End Sub

    Public Shared Function GetPreferredMovieTrailer(ByRef TrailerList As List(Of MediaContainers.Trailer), ByRef trlResult As MediaContainers.Trailer) As Boolean
        If TrailerList.Count = 0 Then Return False
        trlResult = Nothing

        'If Any trailer quality, take the first one in TrailerList
        If Master.eSettings.MovieTrailerPrefVideoQual = Enums.TrailerVideoQuality.Any Then
            trlResult = TrailerList.First
            If YouTube.UrlUtils.IsYouTubeURL(trlResult.URLWebsite) Then
                Dim sYouTube As New YouTube.Scraper
                sYouTube.GetVideoLinks(trlResult.URLWebsite)

                Dim Trailer As New YouTube.VideoLinkItem
                If sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1440p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps)
                ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN).Count > 0 Then
                    Trailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN)
                End If

                If Trailer IsNot Nothing Then
                    trlResult.isDash = Trailer.isDash
                    trlResult.Quality = Trailer.FormatQuality
                    trlResult.URLVideoStream = Trailer.URL
                    If trlResult.isDash Then
                        Dim TrailerAudio As YouTube.AudioLinkItem = sYouTube.YouTubeLinks.AudioLinks.Item(0)
                        If TrailerAudio IsNot Nothing Then
                            trlResult.URLAudioStream = TrailerAudio.URL
                        Else
                            'If no audio stream could be found we only download the video stream.
                            trlResult.isDash = False
                        End If
                    End If
                End If

            ElseIf Regex.IsMatch(trlResult.URLWebsite, "https?:\/\/.*imdb.*") Then
                Dim sIMDb As New IMDb.Scraper
                sIMDb.GetVideoLinks(trlResult.URLWebsite)

                If sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD2160p60fps) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD2160p60fps).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD2160p60fps
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD2160p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD2160p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD2160p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1440p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1440p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD1440p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p60fps) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p60fps).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD1080p60fps
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD1080p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p60fps) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p60fps).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD720p60fps
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HD720p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.HQ480p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.SQ360p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.SQ240p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.SQ144p
                ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p15fps) Then
                    trlResult.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p15fps).URL
                    trlResult.Quality = Enums.TrailerVideoQuality.SQ144p15fps
                End If
            End If
        End If

        'Try to find first with PreferredQuality or save best quality stream URL to Trailer container
        If trlResult Is Nothing Then
            For Each nTrailer As MediaContainers.Trailer In TrailerList
                If YouTube.UrlUtils.IsYouTubeURL(nTrailer.URLWebsite) Then
                    Dim sYouTube As New YouTube.Scraper
                    Dim ytTrailer As YouTube.VideoLinkItem

                    'get all qualities for this trailer
                    sYouTube.GetVideoLinks(nTrailer.URLWebsite)

                    'try to get preferred quality
                    ytTrailer = sYouTube.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Master.eSettings.MovieTrailerPrefVideoQual)

                    'try to get the best quality for search a Trailer that satisfies the minimum quality
                    If ytTrailer Is Nothing Then
                        If sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p60fps)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD2160p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1440p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p60fps)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p60fps)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p15fps)
                        ElseIf sYouTube.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN).Count > 0 Then
                            ytTrailer = sYouTube.YouTubeLinks.VideoLinks.FirstOrDefault(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN)
                        End If
                    End If

                    If ytTrailer IsNot Nothing Then
                        nTrailer.isDash = ytTrailer.isDash
                        nTrailer.Quality = ytTrailer.FormatQuality
                        nTrailer.URLVideoStream = ytTrailer.URL
                        If nTrailer.isDash Then
                            Dim TrailerAudio As YouTube.AudioLinkItem = sYouTube.YouTubeLinks.AudioLinks.Item(0)
                            If TrailerAudio IsNot Nothing Then
                                nTrailer.URLAudioStream = TrailerAudio.URL
                            Else
                                'If no audio stream could be found we only download the video stream.
                                nTrailer.isDash = False
                            End If
                        End If
                    End If

                ElseIf Regex.IsMatch(nTrailer.URLWebsite, "https?:\/\/.*imdb.*") Then
                    Dim sIMDb As New IMDb.Scraper

                    'get all qualities for this trailer
                    sIMDb.GetVideoLinks(nTrailer.URLWebsite)

                    'try to get preferred quality
                    If sIMDb.VideoLinks.ContainsKey(Master.eSettings.MovieTrailerPrefVideoQual) Then
                        nTrailer.URLVideoStream = sIMDb.VideoLinks(Master.eSettings.MovieTrailerPrefVideoQual).URL
                        nTrailer.Quality = Master.eSettings.MovieTrailerPrefVideoQual
                    Else
                        If sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD2160p60fps) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD2160p60fps).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD2160p60fps
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD2160p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD2160p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD2160p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1440p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1440p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD1440p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p60fps) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p60fps).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD1080p60fps
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD1080p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p60fps) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p60fps).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD720p60fps
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HD720p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.HQ480p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.SQ360p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.SQ240p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.SQ144p
                        ElseIf sIMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p15fps) Then
                            nTrailer.URLVideoStream = sIMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p15fps).URL
                            nTrailer.Quality = Enums.TrailerVideoQuality.SQ144p15fps
                        End If
                    End If

                    'set trailer extension
                    nTrailer.TrailerOriginal.Extention = Path.GetExtension(nTrailer.URLVideoStream)
                    Dim tmpInvalidChar As Integer = nTrailer.TrailerOriginal.Extention.IndexOf("?")
                    If tmpInvalidChar > -1 Then
                        Dim correctextension As String = nTrailer.TrailerOriginal.Extention
                        nTrailer.TrailerOriginal.Extention = correctextension.Remove(tmpInvalidChar)
                    End If
                End If

                If nTrailer.Quality = Master.eSettings.MovieTrailerPrefVideoQual Then
                    trlResult = nTrailer
                    Exit For
                End If
            Next
        End If

        'no preferred Trailer quality found, try to get one that has the minimum quality
        If trlResult Is Nothing Then
            Select Case Master.eSettings.MovieTrailerMinVideoQual
                Case Enums.TrailerVideoQuality.HD2160p60fps
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD2160p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    End If
                Case Enums.TrailerVideoQuality.HD1440p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    End If
                Case Enums.TrailerVideoQuality.HD1080p60fps
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD1080p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    End If
                Case Enums.TrailerVideoQuality.HD720p60fps
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD720p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    End If
                Case Enums.TrailerVideoQuality.HQ480p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    End If
                Case Enums.TrailerVideoQuality.SQ360p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    End If
                Case Enums.TrailerVideoQuality.SQ240p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    End If
                Case Enums.TrailerVideoQuality.SQ144p
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p)
                    End If
                Case Enums.TrailerVideoQuality.SQ144p15fps
                    If TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p)
                    ElseIf TrailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p15fps).Count > 0 Then
                        trlResult = TrailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p15fps)
                    End If
            End Select
        End If

        If trlResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Loads this trailer from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the trailer file</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            _ms = New MemoryStream()
            Using fsTrailer As FileStream = File.OpenRead(sPath)
                Dim memStream As New MemoryStream
                memStream.SetLength(fsTrailer.Length)
                fsTrailer.Read(memStream.GetBuffer, 0, CInt(Fix(fsTrailer.Length)))
                _ms.Write(memStream.GetBuffer, 0, CInt(Fix(fsTrailer.Length)))
                _ms.Flush()
            End Using
            _ext = Path.GetExtension(sPath)
        Else
            _ms = New MemoryStream
        End If
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sTrailerLinksContainer">TrailerLinksContainer</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal sTrailerLinksContainer As TrailerLinksContainer)
        If String.IsNullOrEmpty(sTrailerLinksContainer.VideoURL) Then Return

        Dim WebPage As New HTTP
        Dim tmpPath As String = Path.Combine(Master.TempPath, "DashTrailer")
        Dim tURL As String = String.Empty
        Dim tTrailerAudio As String = String.Empty
        Dim tTrailerVideo As String = String.Empty
        Dim tTrailerOutput As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        If sTrailerLinksContainer.isDash AndAlso Not String.IsNullOrEmpty(sTrailerLinksContainer.AudioURL) Then
            tTrailerOutput = Path.Combine(tmpPath, "output.mkv")
            If Directory.Exists(tmpPath) Then
                Directory.Delete(tmpPath, True)
            End If
            Directory.CreateDirectory(tmpPath)
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1334, "Downloading Dash Audio..."))
            tTrailerAudio = WebPage.DownloadFile(sTrailerLinksContainer.AudioURL, Path.Combine(tmpPath, "traileraudio"), True, "trailer")
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1335, "Downloading Dash Video..."))
            tTrailerVideo = WebPage.DownloadFile(sTrailerLinksContainer.VideoURL, Path.Combine(tmpPath, "trailervideo"), True, "trailer")
            RaiseEvent ProgressUpdated(-2, Master.eLang.GetString(1336, "Merging Trailer..."))
            Using ffmpeg As New Process()
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '                                                ffmpeg info                                                     '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' -r      = fps                                                                                                  '
                ' -an     = disable audio recording                                                                              '
                ' -i      = creating a video from many images                                                                    '
                ' -q:v n  = constant qualitiy(:video) (but a variable bitrate), "n" 1 (excellent quality) and 31 (worst quality) '
                ' -b:v n  = bitrate(:video)                                                                                      '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
                ffmpeg.EnableRaisingEvents = False
                ffmpeg.StartInfo.UseShellExecute = False
                ffmpeg.StartInfo.CreateNoWindow = True
                ffmpeg.StartInfo.RedirectStandardOutput = True
                'ffmpeg.StartInfo.RedirectStandardError = True     <----- if activated, ffmpeg can not finish the building process 
                ffmpeg.StartInfo.Arguments = String.Format(" -i ""{0}"" -i ""{1}"" -vcodec copy -acodec copy ""{2}""", tTrailerVideo, tTrailerAudio, tTrailerOutput)
                ffmpeg.Start()
                ffmpeg.WaitForExit()
                ffmpeg.Close()
            End Using

            If Not String.IsNullOrEmpty(tTrailerVideo) AndAlso File.Exists(tTrailerOutput) Then
                LoadFromFile(tTrailerOutput)
            End If
        Else
            Try
                tTrailerOutput = WebPage.DownloadFile(sTrailerLinksContainer.VideoURL, String.Empty, True, "trailer")
                If Not String.IsNullOrEmpty(tTrailerOutput) Then
                    If _ms IsNot Nothing Then
                        _ms.Dispose()
                    End If
                    _ms = New MemoryStream()

                    Dim retSave() As Byte
                    retSave = WebPage.ms.ToArray
                    _ms.Write(retSave, 0, retSave.Length)

                    _ext = Path.GetExtension(tTrailerOutput)
                Else
                    logger.Warn("Trailer NOT downloaded: " & sTrailerLinksContainer.VideoURL)
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sTrailerLinksContainer.VideoURL & ">")
            End Try
        End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sTrailer">Trailer container</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal sTrailer As MediaContainers.Trailer)
        If Not sTrailer.URLVideoStreamSpecified Then Return

        Dim WebPage As New HTTP
        Dim tmpPath As String = Path.Combine(Master.TempPath, "DashTrailer")
        Dim tURL As String = String.Empty
        Dim tTrailerAudio As String = String.Empty
        Dim tTrailerVideo As String = String.Empty
        Dim tTrailerOutput As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        If sTrailer.isDash Then
            tTrailerOutput = Path.Combine(tmpPath, "output.mkv")
            If Directory.Exists(tmpPath) Then
                Directory.Delete(tmpPath, True)
            End If
            Directory.CreateDirectory(tmpPath)
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1334, "Downloading Dash Audio..."))
            tTrailerAudio = WebPage.DownloadFile(sTrailer.URLAudioStream, Path.Combine(tmpPath, "traileraudio"), True, "trailer")
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1335, "Downloading Dash Video..."))
            tTrailerVideo = WebPage.DownloadFile(sTrailer.URLVideoStream, Path.Combine(tmpPath, "trailervideo"), True, "trailer")
            RaiseEvent ProgressUpdated(-2, Master.eLang.GetString(1336, "Merging Trailer..."))
            Using ffmpeg As New Process()
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '                                                ffmpeg info                                                     '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' -r      = fps                                                                                                  '
                ' -an     = disable audio recording                                                                              '
                ' -i      = creating a video from many images                                                                    '
                ' -q:v n  = constant qualitiy(:video) (but a variable bitrate), "n" 1 (excellent quality) and 31 (worst quality) '
                ' -b:v n  = bitrate(:video)                                                                                      '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
                ffmpeg.EnableRaisingEvents = False
                ffmpeg.StartInfo.UseShellExecute = False
                ffmpeg.StartInfo.CreateNoWindow = True
                ffmpeg.StartInfo.RedirectStandardOutput = True
                'ffmpeg.StartInfo.RedirectStandardError = True     <----- if activated, ffmpeg can not finish the building process 
                ffmpeg.StartInfo.Arguments = String.Format(" -i ""{0}"" -i ""{1}"" -vcodec copy -acodec copy ""{2}""", tTrailerVideo, tTrailerAudio, tTrailerOutput)
                ffmpeg.Start()
                ffmpeg.WaitForExit()
                ffmpeg.Close()
            End Using

            If Not String.IsNullOrEmpty(tTrailerOutput) AndAlso File.Exists(tTrailerOutput) Then
                LoadFromFile(tTrailerOutput)
            End If
        Else
            Try
                tTrailerOutput = WebPage.DownloadFile(sTrailer.URLVideoStream, String.Empty, True, "trailer")
                If Not String.IsNullOrEmpty(tTrailerOutput) Then

                    If _ms IsNot Nothing Then
                        _ms.Dispose()
                    End If
                    _ms = New MemoryStream()

                    Dim retSave() As Byte
                    retSave = WebPage.ms.ToArray
                    _ms.Write(retSave, 0, retSave.Length)

                    _ext = Path.GetExtension(tTrailerOutput)
                Else
                    logger.Warn("Trailer NOT downloaded: " & sTrailer.URLVideoStream)
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sTrailer.URLVideoStream & ">")
            End Try
        End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the trailer</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal sURL As String)
        If String.IsNullOrEmpty(sURL) Then Return

        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim tTrailer As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Try
            tTrailer = WebPage.DownloadFile(sURL, String.Empty, True, "trailer")
            If Not String.IsNullOrEmpty(tTrailer) Then
                If _ms IsNot Nothing Then
                    _ms.Dispose()
                End If
                _ms = New MemoryStream()
                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                _ms.Write(retSave, 0, retSave.Length)
                _ext = Path.GetExtension(tTrailer)
            Else
                logger.Warn("Trailer NOT downloaded: " & sURL)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sURL & ">")
        End Try
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Public Function Save_Movie(ByVal tDBElement As Database.DBElement) As String
        If Not tDBElement.Trailer.TrailerOriginal.hasMemoryStream Then Return String.Empty

        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {tDBElement})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnTrailerSave_Movie, params, False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTrailer)
                SaveToFile(String.Concat(a, _ext))
                strReturn = (String.Concat(a, _ext))
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Sub SaveToFile(ByVal sPath As String)
        If _ms.Length > 0 Then
            Dim retSave() As Byte
            Try
                retSave = _ms.ToArray

                'make sure directory exists
                Directory.CreateDirectory(Directory.GetParent(sPath).FullName)
                If sPath.Length <= 260 Then
                    Using fs As New FileStream(sPath, FileMode.Create, FileAccess.Write)
                        fs.Write(retSave, 0, retSave.Length)
                        fs.Flush()
                        fs.Close()
                    End Using
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            Throw New ArgumentOutOfRangeException("Looks like MemoryStream is empty")
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"


#End Region 'Nested Types

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                If _ms IsNot Nothing Then
                    _ms.Flush()
                    _ms.Close()
                    _ms.Dispose()
                End If
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' set large fields to null.
            _ms = Nothing
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Class TrailerLinksContainer

#Region "Fields"

    Private _audiourl As String
    Private _isDash As Boolean
    Private _videourl As String

#End Region 'Fields

#Region "Properties"

    Public Property AudioURL() As String
        Get
            Return _audiourl
        End Get
        Set(ByVal value As String)
            _audiourl = value
        End Set
    End Property

    Public Property isDash() As Boolean
        Get
            Return _isDash
        End Get
        Set(ByVal value As Boolean)
            _isDash = value
        End Set
    End Property

    Public Property VideoURL() As String
        Get
            Return _videourl
        End Get
        Set(ByVal value As String)
            _videourl = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        Clear()
    End Sub

    Public Sub Clear()
        _audiourl = String.Empty
        _isDash = False
        _videourl = String.Empty
    End Sub

#End Region 'Methods

End Class