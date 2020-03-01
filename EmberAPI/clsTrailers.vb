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
Imports System.Text.RegularExpressions

<Serializable()> _
Public Class Trailers
    Implements IDisposable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

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

    Public ReadOnly Property HasMemoryStream() As Boolean
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
    ''' <summary>
    ''' Delete the movie trailers
    ''' </summary>
    ''' <param name="tDBElement"><c>tDBElement</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal tDBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        If Not tDBElement.FileItemSpecified Then Return

        Try
            For Each a In FileUtils.FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainTrailer, ForceFileCleanup)
                For Each t As String In Master.eSettings.Options.FileSystem.ValidVideoExtensions
                    If File.Exists(String.Concat(a, t)) Then
                        Delete(String.Concat(a, t))
                    End If
                Next
            Next
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub
    ''' <summary>
    ''' Delete the given arbitrary file
    ''' </summary>
    ''' <param name="sPath"></param>
    ''' <remarks>This version of Delete is wrapped in a try-catch block which 
    ''' will log errors before safely returning.</remarks>
    Private Shared Sub Delete(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) Then
            Try
                File.Delete(sPath)
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="iPercent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent, String.Empty)
    End Sub

    Public Shared Function GetPreferred(ByRef trailerList As List(Of MediaContainers.Trailer), ByRef trlResult As MediaContainers.Trailer, ByVal type As Enums.ContentType) As Boolean
        If trailerList.Count = 0 Then Return False
        trlResult = Nothing
        Dim nSettings As New TrailerSettings

        Select Case type
            Case Enums.ContentType.Movie
                nSettings = Master.eSettings.Movie.TrailerSettings
            Case Else
                Return False
        End Select

        'If Any trailer quality, take the first one in TrailerList
        If nSettings.PreferredVideoQuality = Enums.TrailerVideoQuality.Any Then
            trlResult = trailerList.FirstOrDefault
            If trlResult IsNot Nothing Then
                If Not trlResult.URLVideoStreamSpecified AndAlso trlResult.StreamsSpecified Then
                    Dim firstStream = trlResult.Streams.VideoStreams.FirstOrDefault
                    If firstStream IsNot Nothing Then
                        trlResult.URLVideoStream = firstStream.URL
                        If firstStream.IsDash AndAlso trlResult.Streams.AudioStreams.Count > 0 Then trlResult.URLAudioStream = trlResult.Streams.AudioStreams(0).URL
                    End If
                End If
            End If
        End If

        'Try to find first with PreferredQuality or save best quality stream URL to Trailer container
        If trlResult Is Nothing Then
            trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = nSettings.PreferredVideoQuality)
            If trlResult IsNot Nothing Then
                If Not trlResult.URLVideoStreamSpecified AndAlso trlResult.StreamsSpecified Then
                    Dim firstStream = trlResult.Streams.VideoStreams.FirstOrDefault(Function(f) f.FormatQuality = nSettings.PreferredVideoQuality)
                    If firstStream IsNot Nothing Then
                        trlResult.URLVideoStream = firstStream.URL
                        If firstStream.IsDash AndAlso trlResult.Streams.AudioStreams.Count > 0 Then trlResult.URLAudioStream = trlResult.Streams.AudioStreams(0).URL
                    End If
                End If
            End If
        End If

        'no preferred Trailer quality found, try to get one that has the minimum quality
        If trlResult Is Nothing Then
            Select Case nSettings.MinimumVideoQuality
                Case Enums.TrailerVideoQuality.HD2160p60fps
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD2160p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    End If
                Case Enums.TrailerVideoQuality.HD1440p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    End If
                Case Enums.TrailerVideoQuality.HD1080p60fps
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD1080p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    End If
                Case Enums.TrailerVideoQuality.HD720p60fps
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    End If
                Case Enums.TrailerVideoQuality.HD720p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    End If
                Case Enums.TrailerVideoQuality.HQ480p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    End If
                Case Enums.TrailerVideoQuality.SQ360p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    End If
                Case Enums.TrailerVideoQuality.SQ240p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    End If
                Case Enums.TrailerVideoQuality.SQ144p
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p)
                    End If
                Case Enums.TrailerVideoQuality.SQ144p15fps
                    If trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD2160p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1440p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD1080p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p60fps)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HD720p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.HQ480p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ360p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ240p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p)
                    ElseIf trailerList.FindAll(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p15fps).Count > 0 Then
                        trlResult = trailerList.FirstOrDefault(Function(f) f.Quality = Enums.TrailerVideoQuality.SQ144p15fps)
                    End If
            End Select
        End If

        If trlResult IsNot Nothing Then
            If Not trlResult.URLVideoStreamSpecified AndAlso trlResult.StreamsSpecified Then
                Dim firstStream = trlResult.Streams.VideoStreams.FirstOrDefault()
                If firstStream IsNot Nothing Then
                    trlResult.URLVideoStream = firstStream.URL
                    If firstStream.IsDash AndAlso trlResult.Streams.AudioStreams.Count > 0 Then trlResult.URLAudioStream = trlResult.Streams.AudioStreams(0).URL
                End If
            End If
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
    ''' <param name="trailer">Trailer container</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal trailer As MediaContainers.Trailer)
        If Not trailer.URLVideoStreamSpecified Then Return

        Dim WebPage As New HTTP
        Dim tmpPath As String = Path.Combine(Master.TempPath, "DashTrailer")
        Dim tURL As String = String.Empty
        Dim tTrailerAudio As String = String.Empty
        Dim tTrailerVideo As String = String.Empty
        Dim tTrailerOutput As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        If trailer.IsDash Then
            tTrailerOutput = Path.Combine(tmpPath, "output.mkv")
            If Directory.Exists(tmpPath) Then
                Directory.Delete(tmpPath, True)
            End If
            Directory.CreateDirectory(tmpPath)
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1334, "Downloading Dash Audio..."))
            tTrailerAudio = WebPage.DownloadFile(trailer.URLAudioStream, Path.Combine(tmpPath, "traileraudio"), True, "trailer")
            RaiseEvent ProgressUpdated(-1, Master.eLang.GetString(1335, "Downloading Dash Video..."))
            tTrailerVideo = WebPage.DownloadFile(trailer.URLVideoStream, Path.Combine(tmpPath, "trailervideo"), True, "trailer")
            RaiseEvent ProgressUpdated(-2, Master.eLang.GetString(1336, "Merging Trailer..."))
            Using ffmpeg As New Process()
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '                                                ffmpeg info                                                     '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' -i      = input file url                                                                                       '
                ' -vcodec = Set the video codec                                                                                  '
                ' -acodec = Set the audio codec                                                                                  '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ffmpeg.StartInfo.FileName = EmberAPI.FFmpeg.FFmpeg.GetFFMpeg
                ffmpeg.EnableRaisingEvents = False
                ffmpeg.StartInfo.UseShellExecute = False
                ffmpeg.StartInfo.CreateNoWindow = True
                ffmpeg.StartInfo.RedirectStandardOutput = True
                'ffmpeg.StartInfo.RedirectStandardError = False     <----- if enabled, ffmpeg can't finish the building process 
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
                tTrailerOutput = WebPage.DownloadFile(trailer.URLVideoStream, String.Empty, True, "trailer")
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
                    _Logger.Warn("Trailer NOT downloaded: " & trailer.URLVideoStream)
                End If

            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & trailer.URLVideoStream & ">")
            End Try
        End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub
    ''' <summary>
    ''' Loads this trailer from the supplied URL
    ''' </summary>
    ''' <param name="trailerUrl">URL to the trailer</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal trailerUrl As String)
        If String.IsNullOrEmpty(trailerUrl) Then Return

        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim tTrailer As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Try
            tTrailer = WebPage.DownloadFile(trailerUrl, String.Empty, True, "trailer")
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
                _Logger.Warn("Trailer NOT downloaded: " & trailerUrl)
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & trailerUrl & ">")
        End Try
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Public Function Save(ByVal tDBElement As Database.DBElement) As String
        If Not tDBElement.Trailer.TrailerOriginal.HasMemoryStream Then Return String.Empty

        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {tDBElement})
                Select Case tDBElement.ContentType
                    Case Enums.ContentType.Movie
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.OnTrailerSave_Movie, params, False)
                End Select
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            For Each a In FileUtils.FileNames.GetFileNames(tDBElement, Enums.ModifierType.MainTrailer)
                SaveToFile(String.Concat(a, _ext))
                strReturn = (String.Concat(a, _ext))
            Next

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
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
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        Else
            Throw New ArgumentOutOfRangeException("Looks like MemoryStream is empty")
        End If
    End Sub

#End Region 'Methods

#Region "IDisposable Support"

    Private disposedValue As Boolean 'To detect redundant calls

    'IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'dispose managed state (managed objects).
                If _ms IsNot Nothing Then
                    _ms.Flush()
                    _ms.Close()
                    _ms.Dispose()
                End If
            End If

            'free unmanaged resources (unmanaged objects) and override Finalize() below.
            'set large fields to null.
            _ms = Nothing
        End If
        disposedValue = True
    End Sub

    'TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    'Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    'This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        'Do not change this code. Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region 'IDisposable Support

End Class

Public Class TrailerLinksContainer

#Region "Properties"

    Public Property AudioURL() As String = String.Empty

    Public Property IsDash() As Boolean = False

    Public Property VideoURL() As String = String.Empty

#End Region 'Properties

End Class