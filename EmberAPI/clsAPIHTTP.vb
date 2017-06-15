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
Imports System.Net
Imports System.Drawing
Imports NLog

''' <summary>
''' 
''' </summary>
''' <remarks>
''' 2013/10/31 Dekker500 - Formally implemented iDisposable
''' </remarks>
Public Class HTTP
    Implements IDisposable


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private dThread As New Threading.Thread(AddressOf DownloadImage)
    Private wrRequest As HttpWebRequest
    Private _cancelRequested As Boolean
    Private _image As Image
    Private _ms As MemoryStream = New MemoryStream()
    Private _responseuri As String
    Private _URL As String = String.Empty
    Private _isJPG As Boolean = False
    Private _isPNG As Boolean = False
    Private _defaultRequestTimeout As Integer = 20000  'request timeout in milliseconds

#End Region 'Fields

#Region "Constructors"

    ''' <summary>
    ''' Base constructor
    ''' </summary>
    Public Sub New()
        Clear()
    End Sub


#End Region 'Constructors

#Region "Events"

    Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property Image() As Image
        Get
            Return _image
        End Get
    End Property

    Public ReadOnly Property isJPG() As Boolean
        Get
            Return _isJPG
        End Get
    End Property

    Public ReadOnly Property isPNG() As Boolean
        Get
            Return _isPNG
        End Get
    End Property

    Public ReadOnly Property ms() As MemoryStream
        Get
            Return _ms
        End Get
    End Property

    Public Property ResponseUri() As String
        Get
            Return _responseuri
        End Get
        Set(ByVal value As String)
            _responseuri = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"
    ''' <summary>
    ''' Cancel any pending request
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Cancel()
        _cancelRequested = True
        If wrRequest IsNot Nothing Then wrRequest.Abort()
    End Sub
    ''' <summary>
    ''' Clears this instance and makes it ready for re-use
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clear()
        _responseuri = String.Empty
        If _image IsNot Nothing Then _image.Dispose()
        _image = Nothing
        Cancel()    'Explicitely stop any in-progress requests
        _cancelRequested = False
    End Sub
    ''' <summary>
    ''' Download the data from the given <paramref name="URL"/>
    ''' and return it as a <c>String</c>
    ''' </summary>
    ''' <param name="URL"><c>URL</c> from which to download the data</param>
    ''' <returns><c>String</c> representing the data retrieved from the <paramref name="URL"/>, or <c>String.Empty</c> on error.</returns>
    ''' <remarks></remarks>
    Public Function DownloadData(ByVal URL As String) As String
        Dim sResponse As String = String.Empty
        Dim cEncoding As System.Text.Encoding

        Clear()

        Try
            wrRequest = DirectCast(WebRequest.Create(URL), HttpWebRequest)
            wrRequest.Timeout = _defaultRequestTimeout
            wrRequest.Headers.Add("Accept-Encoding", "gzip,deflate")
            wrRequest.KeepAlive = False

            PrepareProxy()

            Using wrResponse As HttpWebResponse = DirectCast(wrRequest.GetResponse(), HttpWebResponse)
                Select Case True
                    'for our purposes I think it's safe to assume that all xmls we will be dealing with will be UTF-8 encoded
                    Case wrResponse.ContentType.ToLower.Contains("/xml") OrElse wrResponse.ContentType.ToLower.Contains("charset=utf-8")
                        cEncoding = System.Text.Encoding.UTF8
                    Case Else
                        cEncoding = System.Text.Encoding.GetEncoding(28591)
                End Select
                Using Ms As Stream = wrResponse.GetResponseStream
                    If wrResponse.ContentEncoding.ToLower = "gzip" Then
                        sResponse = New StreamReader(New GZipStream(Ms, CompressionMode.Decompress), cEncoding, True).ReadToEnd
                    ElseIf wrResponse.ContentEncoding.ToLower = "deflate" Then
                        sResponse = New StreamReader(New DeflateStream(Ms, CompressionMode.Decompress), cEncoding, True).ReadToEnd
                    Else
                        sResponse = New StreamReader(Ms, cEncoding, True).ReadToEnd
                    End If
                End Using
                _responseuri = wrResponse.ResponseUri.ToString
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & URL & ">")
        End Try

        Return sResponse
    End Function
    ''' <summary>
    ''' Download file from the supplied <paramref name="URL"/> and store it in the appropriate location relative to <paramref name="LocalFile"/>
    ''' </summary>
    ''' <param name="URL"><c>String</c> URL to retrieve data from. If the file retrieved has an extension, this will be preserved even if the file name is modified.</param>
    ''' <param name="LocalFile">The local file to which the downloaded file is related. This could be the main video file, whereas the URL is the trailer.</param>
    ''' <param name="ReportUpdate">If <c>True</c>, the <c>ProgressUpdated</c> event is raised</param>
    ''' <param name="Type">This can be "<c>Trailer</c>", "<c>Other</c>", or left blank. This helps determine how the file name will be combined with 
    ''' the <paramref name="LocalFile"/>'s filename to generate the save file's name</param>
    ''' <returns><c>String</c> representing the path to the saved file</returns>
    ''' <remarks>
    ''' 2013/11/08 Dekker500 - Refactored the main Case statement to simplify the conditions and improve performance
    ''' </remarks>
    Public Function DownloadFile(ByVal URL As String, ByVal LocalFile As String, ByVal ReportUpdate As Boolean, ByVal Type As String, Optional WebURL As String = "") As String
        Dim outFile As String = String.Empty
        Dim urlExt As String = String.Empty
        Dim urlExtWeb As String = String.Empty

        _cancelRequested = False
        Try
            If URL.Contains("goear") Then
                'GoEar needs a existing connection to download files, otherwise you will be blocked
                If Not String.IsNullOrEmpty(WebURL) Then
                    Dim dummyclient As New WebClient
                    dummyclient.OpenRead(WebURL)
                    dummyclient.Dispose()
                Else
                    Return outFile
                End If
            End If

            wrRequest = DirectCast(WebRequest.Create(URL), HttpWebRequest)
            wrRequest.Timeout = _defaultRequestTimeout

            'needed for apple trailer website
            If URL.Contains("apple") Then
                wrRequest.UserAgent = "QuickTime/7.2 (qtver=7.2;os=Windows NT 5.1Service Pack 3)"
            End If

            PrepareProxy()

            Using wrResponse As HttpWebResponse = DirectCast(wrRequest.GetResponse(), HttpWebResponse)

                Try
                    urlExt = Path.GetExtension(URL)
                    urlExtWeb = String.Concat(".", wrResponse.ContentType.Replace("video/", String.Empty).Trim)
                    urlExtWeb = urlExtWeb.Replace("audio/", String.Empty).Trim
                Catch
                End Try

                If Type = "trailer" Then
                    If urlExt = ".mov" Then
                        outFile = LocalFile & urlExt
                    ElseIf urlExtWeb = ".x-flv" Then
                        outFile = LocalFile & ".flv"
                    Else
                        outFile = LocalFile & urlExtWeb
                    End If
                ElseIf Type = "theme" Then
                    If urlExtWeb = ".mpeg" Then
                        outFile = LocalFile & ".mp3"
                    End If
                ElseIf Type = "other" Then
                    outFile = LocalFile
                End If


                If Not String.IsNullOrEmpty(outFile) AndAlso Not wrResponse.ContentLength = 0 Then

                    Using Ms As Stream = wrResponse.GetResponseStream
                        If LocalFile.Length > 0 Then

                            If File.Exists(outFile) Then File.Delete(outFile)

                            'save to real file
                            Using mStream As New FileStream(outFile, FileMode.Create, FileAccess.Write)
                                'use a larger buffer/blocksize because files are often large, before NET 4.5x : StreamBuffer(4096) = default
                                Dim StreamBuffer(81920) As Byte
                                Dim BlockSize As Integer
                                Dim iProgress As Integer
                                Dim iCurrent As Integer
                                Do
                                    BlockSize = Ms.Read(StreamBuffer, 0, 81920)
                                    iCurrent += BlockSize
                                    If BlockSize > 0 Then
                                        mStream.Write(StreamBuffer, 0, BlockSize)
                                        If ReportUpdate Then
                                            iProgress = Convert.ToInt32((iCurrent / wrResponse.ContentLength) * 100)
                                            RaiseEvent ProgressUpdated(iProgress)
                                        End If
                                    End If
                                Loop While BlockSize > 0 AndAlso Not _cancelRequested
                                'this is not necessary because of using block
                                'StreamBuffer = Nothing
                                'mStream.Close()
                            End Using
                        Else
                            ' no real file specified, let's work with memory streams
                            outFile = "dummy" & outFile 'used to return the correct extension. localfile is empty so outfile is just .ext

                            Dim count = Convert.ToInt32(wrResponse.ContentLength)
                            Dim buffer = New Byte(count) {}
                            Dim bytesRead As Integer
                            Dim iProgress As Integer
                            Dim iCurrent As Integer
                            Do
                                bytesRead += Ms.Read(buffer, bytesRead, count - bytesRead)
                                iCurrent = bytesRead
                                If ReportUpdate Then
                                    iProgress = Convert.ToInt32((iCurrent / wrResponse.ContentLength) * 100)
                                    RaiseEvent ProgressUpdated(iProgress)
                                End If
                            Loop Until bytesRead = count
                            Ms.Close()
                            _ms.Close()
                            _ms = New MemoryStream()

                            _ms.Write(buffer, 0, bytesRead)
                            _ms.Flush()

                        End If
                        Ms.Close()
                    End Using
                End If
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & URL & ">")
            outFile = String.Empty
        End Try

        Return outFile
    End Function
    ''' <summary>
    ''' Download the image pointed to by the <see cref="_URL"/> and
    ''' store it in <see cref="_image"/>
    ''' </summary>
    ''' <remarks>This method is intended to be called in its own thread</remarks>
    Public Sub DownloadImage()
        Try
            _isJPG = False
            _isPNG = False
            If StringUtils.isValidURL(_URL) Then
                wrRequest = DirectCast(WebRequest.Create(_URL), HttpWebRequest)
                wrRequest.Timeout = _defaultRequestTimeout

                If _cancelRequested Then Return

                PrepareProxy()

                If _cancelRequested Then Return

                Using wrResponse As WebResponse = wrRequest.GetResponse()
                    If _cancelRequested Then Return
                    If wrResponse.ContentType.ToLower.Contains("image") Then
                        If _cancelRequested Then Return
                        If wrResponse.ContentType.ToLower.Contains("jpeg") Then
                            _isJPG = True
                        ElseIf wrResponse.ContentType.ToLower.Contains("png") Then
                            _isPNG = True
                        End If
                        Dim SourceStream As Stream = wrResponse.GetResponseStream()
                        _ms.Close()
                        _ms = New MemoryStream()
                        SourceStream.CopyTo(_ms)
                        _image = New Bitmap(_ms)
                        SourceStream.Close()
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & _URL & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Convenience flag to indicate whether the thread is in fact still doing something
    ''' </summary>
    ''' <returns><c>True</c> if the thread is still working</returns>
    ''' <remarks></remarks>
    Public Function IsDownloading() As Boolean
        Return dThread.IsAlive
    End Function
    ''' <summary>
    ''' Convenience function to prepare the <see cref="wrRequest"/>.Proxy if so defined.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PrepareProxy()
        If Not String.IsNullOrEmpty(Master.eSettings.ProxyURI) AndAlso Master.eSettings.ProxyPort >= 0 Then
            Dim wProxy As New WebProxy(Master.eSettings.ProxyURI, Master.eSettings.ProxyPort)
            wProxy.BypassProxyOnLocal = True
            'TODO Dekker500 - Verify if this Password/empty clause is required. Proxies can have usernames but blank passwords, no?
            If Not String.IsNullOrEmpty(Master.eSettings.ProxyCredentials.UserName) AndAlso
            Not String.IsNullOrEmpty(Master.eSettings.ProxyCredentials.Password) Then
                wProxy.Credentials = Master.eSettings.ProxyCredentials
            Else
                wProxy.Credentials = CredentialCache.DefaultCredentials
            End If
            wrRequest.Proxy = wProxy
        End If
    End Sub
    ''' <summary>
    ''' Commands a thread to be spawned to download the image contained at the given URL
    ''' </summary>
    ''' <param name="sURL">URL containing the desired image</param>
    ''' <remarks>Once the download is complete, the url will be stored in <see cref="_image"/></remarks>
    Public Sub StartDownloadImage(ByVal sURL As String)
        Clear()
        _URL = sURL
        dThread = New Threading.Thread(AddressOf DownloadImage)
        dThread.IsBackground = True
        dThread.Start()
    End Sub

#End Region 'Methods

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _image IsNot Nothing Then _image.Dispose()
                If wrRequest IsNot Nothing Then wrRequest.Abort() 'Explicitely cancel any pending requests
            End If

            If _ms IsNot Nothing Then
                'Make sure the stream is indeed closed so it can be disposed of properly
                _ms.Close()
            End If
            _image = Nothing
            wrRequest = Nothing

            disposedValue = True
        End If
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class