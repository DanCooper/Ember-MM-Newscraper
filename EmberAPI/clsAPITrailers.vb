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

    Private _ext As String
    Private _description As String
    Private _isNew As Boolean
    Private _length As String
    Private _quality As Enums.TrailerQuality
    Private _url As String
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

    Public Property isNew() As Boolean
        Get
            Return _isNew
        End Get
        Set(ByVal value As Boolean)
            _isNew = value
        End Set
    End Property
    ''' <summary>
    ''' lenght of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Lenght() As String
        Get
            Return _length
        End Get
        Set(ByVal value As String)
            _length = value
        End Set
    End Property
    ''' <summary>
    ''' resolution/quality of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Quality() As Enums.TrailerQuality
        Get
            Return _quality
        End Get
        Set(ByVal value As Enums.TrailerQuality)
            _quality = value
        End Set
    End Property
    ''' <summary>
    ''' download URL of the trailer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property URL() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
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

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        If Not IsNothing(_ms) Then
            Me.Dispose(True)
            Me.disposedValue = False    'Since this is not a real Dispose call...
        End If

        _ext = String.Empty
        _description = String.Empty
        _isNew = False
        _length = String.Empty
        _quality = Enums.TrailerQuality.OTHERS
        _url = String.Empty
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
                logger.Error(New StackFrame().GetMethod().Name & vbTab & "Param: <" & sPath & ">", ex)
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
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.MovieModType.Trailer)
                For Each t As String In Master.eSettings.FileSystemValidExts
                    If File.Exists(a & t) Then
                        Delete(a & t)
                    End If
                Next
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "<" & mMovie.Filename & ">", ex)
        End Try
    End Sub
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="iPercent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent)
    End Sub
    ''' <summary>
    ''' Loads this trailer from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the trailer file</param>
    ''' <remarks></remarks>
    Public Sub FromFile(ByVal sPath As String)
        If Not IsNothing(Me._ms) Then
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
                    '_ms.SetLength(fsImage.Length)
                    'fsImage.Read(_ms.GetBuffer(), 0, Convert.ToInt32(fsImage.Length))
                    Me._ms.Flush()

                    Me._ext = Path.GetExtension(sPath)
                    Me._url = sPath
                End Using
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name & vbTab & "<" & sPath & ">", ex)
            End Try
        End If
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

                If Not IsNothing(Me._ms) Then
                    Me._ms.Dispose()
                End If
                Me._ms = New MemoryStream()

                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                Me._ms.Write(retSave, 0, retSave.Length)

                Me._ext = Path.GetExtension(tTrailer)
                Me._url = sURL
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name & vbTab & "<" & sURL & ">", ex)
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
    ''' <remarks></remarks>
    Public Shared Function GetPreferredTrailer(ByRef UrlList As List(Of Trailers), ByRef trlResult As MediaContainers.Trailer) As Boolean
        If UrlList.Count = 0 Then Return False

        Try
            For Each aUrl As Trailers In UrlList
                Dim tLink As String = String.Empty
                If Regex.IsMatch(aUrl.URL, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                    Dim YT As New YouTube.Scraper
                    YT.GetVideoLinks(aUrl.URL)
                    If YT.VideoLinks.ContainsKey(Master.eSettings.MovieTrailerPrefQual) Then
                        tLink = YT.VideoLinks(Master.eSettings.MovieTrailerPrefQual).URL
                    Else
                        Select Case Master.eSettings.MovieTrailerMinQual
                            Case Enums.TrailerQuality.All
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.OTHERS) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.OTHERS).URL
                                End If
                            Case Enums.TrailerQuality.HD1080p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                End If
                            Case Enums.TrailerQuality.HD720p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                End If
                            Case Enums.TrailerQuality.HQ480p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                End If
                            Case Enums.TrailerQuality.SQ360p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                End If
                            Case Enums.TrailerQuality.SQ240p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                End If
                            Case Enums.TrailerQuality.SQ144p
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                End If
                            Case Enums.TrailerQuality.OTHERS
                                If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.OTHERS) Then
                                    tLink = YT.VideoLinks(Enums.TrailerQuality.OTHERS).URL
                                End If
                        End Select
                    End If
                ElseIf Regex.IsMatch(aUrl.URL, "http:\/\/.*imdb.*") Then
                    Dim IMDb As New IMDb.Scraper
                    IMDb.GetVideoLinks(aUrl.URL)
                    If IMDb.VideoLinks.ContainsKey(Master.eSettings.MovieTrailerPrefQual) Then
                        tLink = IMDb.VideoLinks(Master.eSettings.MovieTrailerPrefQual).URL
                    Else
                        Select Case Master.eSettings.MovieTrailerMinQual
                            Case Enums.TrailerQuality.All
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.OTHERS) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.OTHERS).URL
                                End If
                            Case Enums.TrailerQuality.HD1080p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                End If
                            Case Enums.TrailerQuality.HD720p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                End If
                            Case Enums.TrailerQuality.HQ480p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                End If
                            Case Enums.TrailerQuality.SQ360p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                End If
                            Case Enums.TrailerQuality.SQ240p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                End If
                            Case Enums.TrailerQuality.SQ144p
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                End If
                            Case Enums.TrailerQuality.OTHERS
                                If IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD1080p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD1080p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.HQ480p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.HQ480p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ360p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ360p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ240p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ240p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.SQ144p) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.SQ144p).URL
                                ElseIf IMDb.VideoLinks.ContainsKey(Enums.TrailerQuality.OTHERS) Then
                                    tLink = IMDb.VideoLinks(Enums.TrailerQuality.OTHERS).URL
                                End If
                        End Select

                    End If
                Else
                    If aUrl.Quality = Master.eSettings.MovieTrailerPrefQual Then
                        tLink = aUrl.URL
                    Else
                        Select Case Master.eSettings.MovieTrailerMinQual
                            Case Enums.TrailerQuality.All
                                tLink = aUrl.URL
                            Case Enums.TrailerQuality.HD1080p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.HD720p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.HQ480p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HQ480p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.SQ360p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HQ480p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ360p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.SQ240p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HQ480p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ360p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ240p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.SQ144p
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HQ480p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ360p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ240p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ144p Then
                                    tLink = aUrl.URL
                                End If
                            Case Enums.TrailerQuality.OTHERS
                                If aUrl.Quality = Enums.TrailerQuality.HD1080p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HD720p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.HQ480p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ360p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ240p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.SQ144p Then
                                    tLink = aUrl.URL
                                ElseIf aUrl.Quality = Enums.TrailerQuality.OTHERS Then
                                    tLink = aUrl.URL
                                End If
                        End Select
                    End If
                End If

                If Not String.IsNullOrEmpty(tLink) Then
                    trlResult.WebTrailer.FromWeb(tLink)
                    Return True
                End If
            Next
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Function

    Public Function SaveAsMovieTrailer(ByVal mMovie As Structures.DBMovie) As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieTrailerSave, params, False)
            Catch ex As Exception
            End Try

            Dim fExt As String = Path.GetExtension(Me._ext)
            For Each a In FileUtils.GetFilenameList.Movie(mMovie.Filename, mMovie.IsSingle, Enums.MovieModType.Trailer)
                If Not File.Exists(a) OrElse (isNew OrElse Master.eSettings.MovieTrailerOverwrite) Then
                    Save(a & fExt)
                    strReturn = (a & fExt)
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