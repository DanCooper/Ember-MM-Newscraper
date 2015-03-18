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

Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports EmberAPI
Imports NLog

Public Class Trailers
    Implements IDisposable

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private _audiourl As String
    Private _ext As String
    Private _description As String
    Private _isDash As Boolean
    Private _isEdit As Boolean
    Private _duration As String
    Private _quality As Enums.TrailerVideoQuality
    Private _source As String
    Private _toRemove As Boolean
    Private _videourl As String
    Private _weburl As String

    Private _ms As MemoryStream
    Private Ret As Byte()

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"
    ''' <summary>
    ''' download audio URL of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AudioURL() As String
        Get
            Return _audiourl
        End Get
        Set(ByVal value As String)
            _audiourl = value
        End Set
    End Property
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
    ''' <summary>
    ''' description or title of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property
    ''' <summary>
    ''' If is a Dash video, we need also an audio URL to merge video and audio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isDash() As Boolean
        Get
            Return _isDash
        End Get
        Set(ByVal value As Boolean)
            _isDash = value
        End Set
    End Property

    Public Property isEdit() As Boolean
        Get
            Return _isEdit
        End Get
        Set(ByVal value As Boolean)
            _isEdit = value
        End Set
    End Property
    ''' <summary>
    ''' lenght of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property
    ''' <summary>
    ''' resolution/quality of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Quality() As Enums.TrailerVideoQuality
        Get
            Return _quality
        End Get
        Set(ByVal value As Enums.TrailerVideoQuality)
            _quality = value
        End Set
    End Property
    ''' <summary>
    ''' trailer source (YouTube, IMDB, Apple, TMDB...)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Source() As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
        End Set
    End Property
    ''' <summary>
    ''' trigger to remove trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property toRemove() As Boolean
        Get
            Return _toRemove
        End Get
        Set(ByVal value As Boolean)
            _toRemove = value
        End Set
    End Property
    ''' <summary>
    ''' download video URL of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VideoURL() As String
        Get
            Return _videourl
        End Get
        Set(ByVal value As String)
            _videourl = value
        End Set
    End Property
    ''' <summary>
    ''' website URL of the trailer for preview in browser
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WebURL() As String
        Get
            Return _weburl
        End Get
        Set(ByVal value As String)
            _weburl = value
        End Set
    End Property

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer, ByVal strInfo As String)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        If _ms IsNot Nothing Then
            Me.Dispose(True)
            Me.disposedValue = False    'Since this is not a real Dispose call...
        End If

        _audiourl = String.Empty
        _ext = String.Empty
        _description = String.Empty
        _isDash = False
        _isEdit = False
        _duration = String.Empty
        _quality = Enums.TrailerVideoQuality.UNKNOWN
        _source = String.Empty
        _toRemove = False
        _videourl = String.Empty
        _weburl = String.Empty
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
    Public Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Param: <" & sPath & ">", ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete the movie trailers
    ''' </summary>
    ''' <param name="mMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Sub DeleteMovieTrailer(ByVal mMovie As Structures.DBMovie)
        If String.IsNullOrEmpty(mMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModType_Movie.Trailer)
                For Each t As String In Master.eSettings.FileSystemValidExts
                    If File.Exists(String.Concat(a, t)) Then
                        Delete(String.Concat(a, t))
                    End If
                Next
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mMovie.Filename & ">", ex)
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
    ''' <summary>
    ''' Loads this trailer from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the trailer file</param>
    ''' <remarks></remarks>
    Public Sub FromFile(ByVal sPath As String)
        If Me._ms IsNot Nothing Then
            Me._ms.Dispose()
        End If
        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            Try
                Me._ms = New MemoryStream()
                Using fsImage As New FileStream(sPath, FileMode.Open, FileAccess.Read)
                    Dim StreamBuffer(Convert.ToInt32(fsImage.Length - 1)) As Byte

                    fsImage.Read(StreamBuffer, 0, StreamBuffer.Length)
                    Me._ms.Write(StreamBuffer, 0, StreamBuffer.Length)

                    StreamBuffer = Nothing
                    Me._ms.Flush()

                    Me._ext = Path.GetExtension(sPath)
                    Me._videourl = sPath
                End Using
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sPath & ">", ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sTrailerLinksContainer">TrailerLinksContainer</param>
    ''' <remarks></remarks>
    Public Sub FromWeb(ByVal sTrailerLinksContainer As TrailerLinksContainer)
        Dim WebPage As New HTTP
        Dim tmpPath As String = Path.Combine(Master.TempPath, "DashTrailer")
        Dim tURL As String = String.Empty
        Dim tTrailerAudio As String = String.Empty
        Dim tTrailerVideo As String = String.Empty
        Dim tTrailerOutput As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        If sTrailerLinksContainer.isDash Then
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
                Me.FromFile(tTrailerOutput)
            End If
        Else
            Try
                tTrailerOutput = WebPage.DownloadFile(sTrailerLinksContainer.VideoURL, "", True, "trailer")
                If Not String.IsNullOrEmpty(tTrailerOutput) Then
                    If Me._ms IsNot Nothing Then
                        Me._ms.Dispose()
                    End If
                    Me._ms = New MemoryStream()

                    Dim retSave() As Byte
                    retSave = WebPage.ms.ToArray
                    Me._ms.Write(retSave, 0, retSave.Length)

                    Me._ext = Path.GetExtension(tTrailerOutput)
                    Me._videourl = sTrailerLinksContainer.VideoURL
                    logger.Debug("Trailer downloaded: " & sTrailerLinksContainer.VideoURL)
                Else
                    logger.Warn("Trailer NOT downloaded: " & sTrailerLinksContainer.VideoURL)
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sTrailerLinksContainer.VideoURL & ">", ex)
            End Try
        End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sTrailer">Trailer container</param>
    ''' <remarks></remarks>
    Public Sub FromWeb(ByVal sTrailer As Trailers)
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
            tTrailerAudio = WebPage.DownloadFile(sTrailer.AudioURL, Path.Combine(tmpPath, "traileraudio"), True, "trailer")
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1335, "Downloading Dash Video..."))
            tTrailerVideo = WebPage.DownloadFile(sTrailer.VideoURL, Path.Combine(tmpPath, "trailervideo"), True, "trailer")
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
                Me.FromFile(tTrailerOutput)
            End If
        Else
            Try
                tTrailerOutput = WebPage.DownloadFile(sTrailer.VideoURL, "", True, "trailer")
                If Not String.IsNullOrEmpty(tTrailerOutput) Then

                    If Me._ms IsNot Nothing Then
                        Me._ms.Dispose()
                    End If
                    Me._ms = New MemoryStream()

                    Dim retSave() As Byte
                    retSave = WebPage.ms.ToArray
                    Me._ms.Write(retSave, 0, retSave.Length)

                    Me._ext = Path.GetExtension(tTrailerOutput)
                    Me._videourl = sTrailer.VideoURL
                    logger.Debug("Trailer downloaded: " & sTrailer.VideoURL)
                Else
                    logger.Warn("Trailer NOT downloaded: " & sTrailer.VideoURL)
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sTrailer.VideoURL & ">", ex)
            End Try
        End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the trailer file</param>
    ''' <remarks></remarks>
    Public Sub FromWeb(ByVal sURL As String)
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim tTrailer As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Try
            tTrailer = WebPage.DownloadFile(sURL, "", True, "trailer")
            If Not String.IsNullOrEmpty(tTrailer) Then
                If Me._ms IsNot Nothing Then
                    Me._ms.Dispose()
                End If
                Me._ms = New MemoryStream()
                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                Me._ms.Write(retSave, 0, retSave.Length)
                Me._ext = Path.GetExtension(tTrailer)
                Me._videourl = sURL
                logger.Debug("Trailer downloaded: " & sURL)
            Else
                logger.Warn("Trailer NOT downloaded: " & sURL)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sURL & ">", ex)
        End Try
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Given a list of Trailers, determine which one best matches the user's
    ''' configured preferred trailer format. Return that URL in the <paramref name="tUrl"/>
    ''' parameter, and returns <c>True</c>.
    ''' </summary>
    ''' <param name="UrlList"><c>List</c> of <c>Trailer</c>s</param>
    ''' <returns><c>True</c> if an appropriate trailer was found. The URL for the trailer is returned in
    ''' <paramref name="tUrl"/>. <c>False</c> otherwise</returns>
    ''' <remarks>
    ''' This is used to determine the result of trailer scraping by going through all scraperesults of every trailer scraper and applying global scraper settings here (PrefQuality, MinQuality)!
    ''' Note: Only one trailerresult will be used for downloading
    ''' 2014/09/26 Cocotus - Modified this method a bit: Now trailer module order set by user will be considered, also do some filtering here (trailerdescription must contain string "trailer" or else the trailer wont be used (no more making ofs...))
    ''' </remarks>
    Public Shared Function GetPreferredTrailer(ByRef UrlList As List(Of Trailers), ByRef trlResult As MediaContainers.Trailer) As Boolean
        If UrlList.Count = 0 Then Return False
        Dim tLink As String = String.Empty

        Try

            'Check 1 Youtube/IMDB handling: At this point trailers from Youtube and IMDB don't have quality property set, so let's analyze the quality of given trailer and set correct trailerurl/quality of each trailerlink in list
            For Each aUrl As Trailers In UrlList
                If Regex.IsMatch(aUrl.VideoURL, "https?:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                    Dim YT As New YouTube.Scraper
                    Dim Trailer As YouTube.VideoLinkItem
                    YT.GetVideoLinks(aUrl.VideoURL)

                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Master.eSettings.MovieTrailerPrefVideoQual)

                    If Trailer Is Nothing Then
                        Select Case Master.eSettings.MovieTrailerMinVideoQual
                            Case Enums.TrailerVideoQuality.All
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN)
                                End If
                            Case Enums.TrailerVideoQuality.HD1080p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                End If
                            Case Enums.TrailerVideoQuality.HD720p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                End If
                            Case Enums.TrailerVideoQuality.HQ480p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                End If
                            Case Enums.TrailerVideoQuality.SQ360p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                                End If
                            Case Enums.TrailerVideoQuality.SQ240p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                                End If
                            Case Enums.TrailerVideoQuality.SQ144p
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p)
                                End If
                            Case Enums.TrailerVideoQuality.UNKNOWN
                                If YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD1080p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HD720p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.HQ480p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ360p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ240p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.SQ144p)
                                ElseIf YT.YouTubeLinks.VideoLinks.FindAll(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN).Count > 0 Then
                                    Trailer = YT.YouTubeLinks.VideoLinks.Find(Function(f) f.FormatQuality = Enums.TrailerVideoQuality.UNKNOWN)
                                End If
                        End Select
                    End If

                    If Trailer IsNot Nothing Then
                        aUrl.isDash = Trailer.isDash
                        aUrl.Quality = Trailer.FormatQuality
                        aUrl.VideoURL = Trailer.URL
                        If aUrl.isDash Then
                            Dim TrailerAudio As YouTube.AudioLinkItem = YT.YouTubeLinks.AudioLinks.Item(0)
                            If TrailerAudio IsNot Nothing Then
                                aUrl.AudioURL = TrailerAudio.URL
                            Else
                                'If no audio stream could be found we only download the video stream.
                                aUrl.isDash = False
                            End If
                        End If
                    End If

                    ' It's not possible to get extension from YouTube URL. For this reason, it is not yet determined.

                    ''set trailer extension
                    'aUrl.Extention = Path.GetExtension(aUrl.URL)
                    'Dim tmpInvalidChar As Integer = aUrl.Extention.IndexOf("?")
                    'If tmpInvalidChar > -1 Then
                    '    Dim correctextension As String = aUrl.Extention
                    '    aUrl.Extention = correctextension.Remove(tmpInvalidChar)
                    'End If

                ElseIf Regex.IsMatch(aUrl.VideoURL, "https?:\/\/.*imdb.*") Then
                    Dim IMDb As New IMDb.Scraper
                    IMDb.GetVideoLinks(aUrl.VideoURL)
                    If IMDb.VideoLinks.ContainsKey(Master.eSettings.MovieTrailerPrefVideoQual) Then
                        tLink = IMDb.VideoLinks(Master.eSettings.MovieTrailerPrefVideoQual).URL
                        aUrl.VideoURL = tLink
                        aUrl.Quality = Master.eSettings.MovieTrailerPrefVideoQual
                    Else
                        Select Case Master.eSettings.MovieTrailerMinVideoQual
                            Case Enums.TrailerVideoQuality.All
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ360p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ240p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ144p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.UNKNOWN) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.UNKNOWN).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.UNKNOWN
                                End If
                            Case Enums.TrailerVideoQuality.HD1080p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                End If
                            Case Enums.TrailerVideoQuality.HD720p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                End If
                            Case Enums.TrailerVideoQuality.HQ480p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                End If
                            Case Enums.TrailerVideoQuality.SQ360p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ360p
                                End If
                            Case Enums.TrailerVideoQuality.SQ240p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ360p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ240p
                                End If
                            Case Enums.TrailerVideoQuality.SQ144p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ360p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ240p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ144p
                                End If
                            Case Enums.TrailerVideoQuality.UNKNOWN
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD1080p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD1080p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HD720p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HD720p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.HQ480p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.HQ480p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ360p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ360p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ240p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ240p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.SQ144p).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.SQ144p
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerVideoQuality.UNKNOWN) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerVideoQuality.UNKNOWN).URL
                                    aUrl.VideoURL = tLink
                                    aUrl.Quality = Enums.TrailerVideoQuality.UNKNOWN
                                End If
                        End Select
                    End If
                    'set trailer extension
                    aUrl.Extention = Path.GetExtension(aUrl.VideoURL)
                    Dim tmpInvalidChar As Integer = aUrl.Extention.IndexOf("?")
                    If tmpInvalidChar > -1 Then
                        Dim correctextension As String = aUrl.Extention
                        aUrl.Extention = correctextension.Remove(tmpInvalidChar)
                    End If
                End If
            Next

            'At this point every trailer in UrlList will have correct quality property set - we can now decide which trailer to use
            Dim lsttrailerresults As New List(Of Trailers)
            lsttrailerresults.AddRange(UrlList)
            'Check 2: Clean Up -> first remove all movies which don't have preferred quality and check if there's at least one left!
            For i = lsttrailerresults.Count - 1 To 0 Step -1
                If (lsttrailerresults(i).Quality <> Master.eSettings.MovieTrailerPrefVideoQual) OrElse lsttrailerresults(i).Description.ToLower.Contains("trailer") = False Then
                    lsttrailerresults.RemoveAt(i)
                End If
            Next

            'Check 3: If there isnt any preferred trailer left after step 1, create list of MinPref-trailers
            If lsttrailerresults.Count < 1 Then
                lsttrailerresults.Clear()
                lsttrailerresults.AddRange(UrlList)
                'Defaultvalue: all trailers with equal/better quality than 480p
                Dim tqualities As String = "HD720pHD1080pHQ480p"
                Select Case Master.eSettings.MovieTrailerMinVideoQual
                    Case Enums.TrailerVideoQuality.HD1080p
                        tqualities = "HD1080p"
                    Case Enums.TrailerVideoQuality.HD720p
                        tqualities = "HD720pHD1080p"
                    Case Enums.TrailerVideoQuality.HQ480p
                        tqualities = "HD720pHD1080pHQ480p"
                    Case Enums.TrailerVideoQuality.SQ360p
                        tqualities = "HD720pHD1080pHQ480pSQ360p"
                    Case Enums.TrailerVideoQuality.SQ240p
                        tqualities = "HD720pHD1080pHQ480pSQ360pSQ240p"
                    Case Enums.TrailerVideoQuality.SQ144p
                        tqualities = "HD720pHD1080pHQ480pSQ360pSQ240pSQ144p"
                    Case Enums.TrailerVideoQuality.UNKNOWN
                        tqualities = "HD720pHD1080pHQ480pSQ360pSQ240pSQ144pOTHERS"
                End Select
                'Build up alternative list
                For i = lsttrailerresults.Count - 1 To 0 Step -1
                    'Clean Up -> first remove all movies which don't have min quality and check if theres at least one left!
                    If tqualities.Contains(lsttrailerresults(i).Quality.ToString) = False OrElse lsttrailerresults(i).Description.ToLower.Contains("trailer") = False Then
                        lsttrailerresults.RemoveAt(i)
                    End If
                Next
            End If
            UrlList.Clear()
            UrlList.AddRange(lsttrailerresults)
            tLink = String.Empty

            For Each aUrl As Trailers In UrlList

                If aUrl.Quality = Master.eSettings.MovieTrailerPrefVideoQual Then
                    tLink = aUrl.VideoURL
                Else
                    Select Case Master.eSettings.MovieTrailerMinVideoQual
                        Case Enums.TrailerVideoQuality.All
                            tLink = aUrl.VideoURL
                        Case Enums.TrailerVideoQuality.HD1080p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.HD720p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.HQ480p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HQ480p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.SQ360p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HQ480p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ360p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.SQ240p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HQ480p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ360p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ240p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.SQ144p
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HQ480p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ360p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ240p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ144p Then
                                tLink = aUrl.VideoURL
                            End If
                        Case Enums.TrailerVideoQuality.UNKNOWN
                            If aUrl.Quality = Enums.TrailerVideoQuality.HD1080p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HD720p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.HQ480p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ360p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ240p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.SQ144p Then
                                tLink = aUrl.VideoURL
                            ElseIf aUrl.Quality = Enums.TrailerVideoQuality.UNKNOWN Then
                                tLink = aUrl.VideoURL
                            End If
                    End Select
                End If
            Next
            Return True

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
    End Function

    Public Function SaveAsMovieTrailer(ByVal mMovie As Structures.DBMovie) As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnTrailerSave_Movie, params, False)
            Catch ex As Exception
            End Try

            Dim fExt As String = Path.GetExtension(Me._ext)
            '2014/09/26 before saving current trailer, check if we should delete existing trailer(s) - do this only if new trailer to save exists!
            If Master.eSettings.MovieTrailerDeleteExisting AndAlso fExt <> "" Then
                DeleteMovieTrailer(Master.currMovie)
            End If


            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.ModType_Movie.Trailer)
                If Not File.Exists(String.Concat(a, fExt)) OrElse (isEdit OrElse Master.eSettings.MovieTrailerOverwrite) Then
                    Save(String.Concat(a, fExt))
                    strReturn = (String.Concat(a, fExt))
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return strReturn
    End Function

    Public Sub Save(ByVal sPath As String)
        Dim retSave() As Byte
        Try
            retSave = Me._ms.ToArray

            'make sure directory exists
            Directory.CreateDirectory(Directory.GetParent(sPath).FullName)
            Using FileStream As Stream = File.OpenWrite(sPath)
                FileStream.Write(retSave, 0, retSave.Length)
            End Using

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Determines whether a trailer is allowed to be downloaded. This is determined
    ''' by a combination of the Master.eSettings.LockTrailer settings,
    ''' whether the path is valid, and whether the Master.eSettings.OverwriteTrailer
    ''' flag is set. 
    ''' </summary>
    ''' <param name="mMovie">The intended path to save the trailer</param>
    ''' <returns><c>True</c> if a download is allowed, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Function IsAllowedToDownload(ByVal mMovie As Structures.DBMovie) As Boolean
        Try
            With Master.eSettings
                If (String.IsNullOrEmpty(mMovie.TrailerPath) OrElse .MovieTrailerOverwrite) AndAlso .MovieTrailerEnable AndAlso _
                    (.MovieTrailerEden OrElse .MovieTrailerFrodo OrElse .MovieTrailerNMJ OrElse .MovieTrailerYAMJ) OrElse _
                    (.MovieUseExpert AndAlso (Not String.IsNullOrEmpty(.MovieTrailerExpertBDMV) OrElse Not String.IsNullOrEmpty(.MovieTrailerExpertMulti) OrElse _
                            Not String.IsNullOrEmpty(.MovieTrailerExpertMulti) OrElse Not String.IsNullOrEmpty(.MovieTrailerExpertSingle))) Then
                    Return True
                Else
                    Return False
                End If
            End With
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
    End Function

#End Region 'Methods

#Region "Nested Types"


#End Region 'Nested Types

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
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
        Me.disposedValue = True
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
            Return Me._audiourl
        End Get
        Set(ByVal value As String)
            Me._audiourl = value
        End Set
    End Property

    Public Property isDash() As Boolean
        Get
            Return Me._isDash
        End Get
        Set(ByVal value As Boolean)
            Me._isDash = value
        End Set
    End Property

    Public Property VideoURL() As String
        Get
            Return Me._videourl
        End Get
        Set(ByVal value As String)
            Me._videourl = value
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