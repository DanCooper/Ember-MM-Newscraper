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

Imports NLog
Imports System.IO

<Serializable()>
Public Class MediaFiles
    Implements IDisposable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Ext As String
    Private _MS As MemoryStream

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"
    ''' <summary>
    ''' File extension
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Extension() As String
        Get
            Return _Ext
        End Get
    End Property

    Public ReadOnly Property HasMemoryStream() As Boolean
        Get
            Return _MS IsNot Nothing
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal percent As Integer, ByVal info As String)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        If _MS IsNot Nothing Then
            Dispose(True)
            disposedValue = False    'Since this is not a real Dispose call...
        End If

        _Ext = String.Empty
    End Sub
    ''' <summary>
    ''' Delete the given arbitrary file
    ''' </summary>
    ''' <param name="filepath"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Public Shared Sub Delete(ByVal filepath As String)
        If Not String.IsNullOrEmpty(filepath) Then
            Try
                File.Delete(filepath)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "Param: <" & filepath & ">")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Delete the movie trailers
    ''' </summary>
    ''' <param name="tDBElement"><c>tDBElement</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_Movie(ByVal tDBElement As Database.DBElement, ByVal type As Enums.ModifierType, ByVal forceFileCleanup As Boolean)
        If String.IsNullOrEmpty(tDBElement.Filename) Then Return

        Try
            For Each a In FileUtils.FileNames.Movie(tDBElement, type, forceFileCleanup)
                Select Case type
                    Case Enums.ModifierType.MainTheme
                        For Each t As String In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                            If File.Exists(String.Concat(a, t)) Then
                                Delete(String.Concat(a, t))
                            End If
                        Next
                    Case Enums.ModifierType.MainTrailer
                        For Each t As String In Master.eSettings.Options.FileSystem.ValidVideoExtensions
                            If File.Exists(String.Concat(a, t)) Then
                                Delete(String.Concat(a, t))
                            End If
                        Next
                End Select
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & tDBElement.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the tv show themes
    ''' </summary>
    ''' <param name="tDBElement"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVShow(ByVal tDBElement As Database.DBElement, ByVal type As Enums.ModifierType) ', ByVal ForceFileCleanup As Boolean)
        If String.IsNullOrEmpty(tDBElement.ShowPath) Then Return

        Try
            For Each a In FileUtils.FileNames.TVShow(tDBElement, type) ', ForceFileCleanup)
                Select Case type
                    Case Enums.ModifierType.MainTheme
                        For Each t As String In Master.eSettings.Options.FileSystem.ValidThemeExtensions
                            If File.Exists(String.Concat(a, t)) Then
                                Delete(String.Concat(a, t))
                            End If
                        Next
                End Select
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & tDBElement.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="percent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal percent As Integer)
        RaiseEvent ProgressUpdated(percent, String.Empty)
    End Sub

    Public Shared Function GetPreferredTheme(ByRef themeList As List(Of MediaContainers.MediaFile), ByRef result As MediaContainers.MediaFile, ByVal type As Enums.ContentType) As Boolean
        If themeList.Count = 0 Then Return False
        result = Nothing

        result = themeList.Item(0)
        If Not result.UrlAudioStreamSpecified AndAlso result.StreamsSpecified AndAlso result.Streams.AudioStreams.Count > 0 Then
            result.UrlAudioStream = result.Streams.AudioStreams(0).StreamUrl
        End If

        If result IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredMovieTrailer(ByRef trailerList As List(Of MediaContainers.MediaFile), ByRef result As MediaContainers.MediaFile, ByVal type As Enums.ContentType) As Boolean
        If trailerList.Count = 0 Then Return False
        result = Nothing

        'If Any trailer quality, take the first one in TrailerList
        If Master.eSettings.MovieTrailerPrefVideoQual = Enums.VideoResolution.Any Then
            result = trailerList.FirstOrDefault
            If result IsNot Nothing Then
                result.SetVariant(result.Streams.Variants.FirstOrDefault)
            End If
        End If

        'Try to find first with PreferredQuality or save best quality stream URL to Trailer container
        If result Is Nothing Then
            result = trailerList.FirstOrDefault(Function(f) f.HasVariantWithVideoResolution(Master.eSettings.MovieTrailerPrefVideoQual))
            If result IsNot Nothing Then
                Dim nVariant = result.Streams.Variants.FirstOrDefault(Function(f) f.VideoResolution = Master.eSettings.MovieTrailerPrefVideoQual)
                If nVariant IsNot Nothing Then
                    result.SetVariant(nVariant)
                Else
                    result = Nothing
                End If
            End If
        End If

        'no preferred Trailer quality found, try to get one that has the minimum quality
        If result Is Nothing Then
            Select Case Master.eSettings.MovieTrailerMinVideoQual
                Case Enums.VideoResolution.HD2160p60fps
                    TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result)
                Case Enums.VideoResolution.HD2160p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) Then
                    End If
                Case Enums.VideoResolution.HD1440p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) Then
                    End If
                Case Enums.VideoResolution.HD1080p60fps
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) Then
                    End If
                Case Enums.VideoResolution.HD1080p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) Then
                    End If
                Case Enums.VideoResolution.HD720p60fps
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) Then
                    End If
                Case Enums.VideoResolution.HD720p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) Then
                    End If
                Case Enums.VideoResolution.HQ480p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HQ480p, result) Then
                    End If
                Case Enums.VideoResolution.SQ360p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HQ480p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ360p, result) Then
                    End If
                Case Enums.VideoResolution.SQ240p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HQ480p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ360p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ240p, result) Then
                    End If
                Case Enums.VideoResolution.SQ144p
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HQ480p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ360p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ240p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ144p, result) Then
                    End If
                Case Enums.VideoResolution.SQ144p15fps
                    If TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD2160p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1440p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD1080p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p60fps, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HD720p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.HQ480p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ360p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ240p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ144p, result) OrElse
                        TryGetItemWithVideoResolution(trailerList, Enums.VideoResolution.SQ144p15fps, result) Then
                    End If
            End Select
        End If

        If result IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Loads this trailer from the contents of the supplied file
    ''' </summary>
    ''' <param name="filePath">Path to the trailer file</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal filePath As String)
        If Not String.IsNullOrEmpty(filePath) AndAlso File.Exists(filePath) Then
            _MS = New MemoryStream()
            Using fsTrailer As FileStream = File.OpenRead(filePath)
                Dim memStream As New MemoryStream
                memStream.SetLength(fsTrailer.Length)
                fsTrailer.Read(memStream.GetBuffer, 0, CInt(Fix(fsTrailer.Length)))
                _MS.Write(memStream.GetBuffer, 0, CInt(Fix(fsTrailer.Length)))
                _MS.Flush()
            End Using
            _Ext = Path.GetExtension(filePath)
        Else
            _MS = New MemoryStream
        End If
    End Sub
    ''' <summary>
    ''' Loads this media file from the supplied URL
    ''' </summary>
    ''' <param name="mediafile">media file container</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal mediaFile As MediaContainers.MediaFile)
        If mediaFile.UrlVideoStreamSpecified Then
            Dim WebPage As New HTTP
            Dim tmpPath As String = Path.Combine(Master.TempPath, "DashMediaFile")
            Dim tURL As String = String.Empty
            Dim strAudioFilePath As String = String.Empty
            Dim strVideoFilePath As String = String.Empty
            Dim strFileName As String = String.Empty
            AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

            If mediaFile.IsAdaptive Then
                strFileName = Path.Combine(tmpPath, "output.mkv")
                If Directory.Exists(tmpPath) Then
                    Directory.Delete(tmpPath, True)
                End If
                Directory.CreateDirectory(tmpPath)
                RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1334, "Downloading Dash Audio..."))
                strAudioFilePath = WebPage.DownloadFile(mediaFile.UrlAudioStream, Path.Combine(tmpPath, "audiostream"), True, "trailer")
                RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1335, "Downloading Dash Video..."))
                strVideoFilePath = WebPage.DownloadFile(mediaFile.UrlVideoStream, Path.Combine(tmpPath, "videostream"), True, "trailer")
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
                    ffmpeg.StartInfo.Arguments = String.Format(" -i ""{0}"" -i ""{1}"" -vcodec copy -acodec copy ""{2}""", strVideoFilePath, strAudioFilePath, strFileName)
                    ffmpeg.Start()
                    ffmpeg.WaitForExit()
                    ffmpeg.Close()
                End Using

                If Not String.IsNullOrEmpty(strFileName) AndAlso File.Exists(strFileName) Then
                    LoadFromFile(strFileName)
                End If

                RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
            Else
                Try
                    strFileName = WebPage.DownloadFile(mediaFile.UrlVideoStream, String.Empty, True, "trailer")
                    If Not String.IsNullOrEmpty(strFileName) Then

                        If _MS IsNot Nothing Then
                            _MS.Dispose()
                        End If
                        _MS = New MemoryStream()

                        Dim retSave() As Byte
                        retSave = WebPage.ms.ToArray
                        _MS.Write(retSave, 0, retSave.Length)

                        _Ext = Path.GetExtension(strFileName)
                    Else
                        _Logger.Warn("Trailer NOT downloaded: " & mediaFile.UrlVideoStream)
                    End If

                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & mediaFile.UrlVideoStream & ">")
                End Try
            End If
        ElseIf mediaFile.UrlAudioStreamSpecified Then
            LoadFromWeb(mediaFile.UrlAudioStream)
        End If
    End Sub
    ''' <summary>
    ''' Loads this media file from the supplied URL
    ''' </summary>
    ''' <param name="url">URL to the media files</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal url As String)
        If String.IsNullOrEmpty(url) Then Return

        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim strMediaFile As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Try
            strMediaFile = WebPage.DownloadFile(url, String.Empty, True, "theme")
            If Not String.IsNullOrEmpty(strMediaFile) Then
                If _MS IsNot Nothing Then
                    _MS.Dispose()
                End If
                _MS = New MemoryStream()
                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                _MS.Write(retSave, 0, retSave.Length)
                _Ext = Path.GetExtension(strMediaFile)
            Else
                _Logger.Warn("MediaFile NOT downloaded: " & url)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & url & ">")
        End Try
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Public Function Save_Movie(ByVal tDBElement As Database.DBElement, ByVal type As Enums.ModifierType) As String
        Select Case type
            Case Enums.ModifierType.MainTheme
                If Not tDBElement.Theme.FileOriginal.HasMemoryStream Then Return String.Empty
            Case Enums.ModifierType.MainTrailer
                If Not tDBElement.Trailer.FileOriginal.HasMemoryStream Then Return String.Empty
        End Select
        Dim strReturn As String = String.Empty
        Try
            For Each a In FileUtils.FileNames.Movie(tDBElement, type)
                SaveToFile(String.Concat(a, _Ext))
                strReturn = (String.Concat(a, _Ext))
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Function Save_TVShow(ByVal tDBElement As Database.DBElement, ByVal type As Enums.ModifierType) As String
        Select Case type
            Case Enums.ModifierType.MainTheme
                If Not tDBElement.Theme.FileOriginal.HasMemoryStream Then Return String.Empty
            Case Enums.ModifierType.MainTrailer
                If Not tDBElement.Trailer.FileOriginal.HasMemoryStream Then Return String.Empty
        End Select
        Dim strReturn As String = String.Empty
        Try
            For Each a In FileUtils.FileNames.TVShow(tDBElement, type)
                SaveToFile(String.Concat(a, _Ext))
                strReturn = (String.Concat(a, _Ext))
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Sub SaveToFile(ByVal sPath As String)
        If _MS.Length > 0 Then
            Dim retSave() As Byte
            Try
                retSave = _MS.ToArray

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
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            Throw New ArgumentOutOfRangeException("Looks like MemoryStream is empty")
        End If
    End Sub

    Private Shared Function TryGetItemWithVideoResolution(ByRef trailerList As List(Of MediaContainers.MediaFile), ByVal resolution As Enums.VideoResolution, ByRef result As MediaContainers.MediaFile) As Boolean
        If trailerList Is Nothing OrElse trailerList.Count = 0 Then
            result = Nothing
            Return False
        Else
            Dim nList = trailerList.Where(Function(f) f.HasVariantWithVideoResolution(resolution))
            If nList IsNot Nothing Then
                For Each tItem In nList
                    If tItem.SetFirstVariantWithVideoStreamResolution(resolution) Then
                        result = tItem
                        Return True
                    End If
                Next
            End If
            result = Nothing
            Return False
        End If
    End Function


#End Region 'Methods

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                If _MS IsNot Nothing Then
                    _MS.Flush()
                    _MS.Close()
                    _MS.Dispose()
                End If
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' set large fields to null.
            _MS = Nothing
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