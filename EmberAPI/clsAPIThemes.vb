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
Imports NLog

Public Class Themes
    Implements IDisposable

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private _ext As String
    Private _title As String
    Private _id As String
    Private _isEdit As Boolean
    Private _url As String
    Private _weburl As String
    Private _description As String
    Private _duration As String
    Private _bitrate As String
    Private _toRemove As Boolean

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
    ''' theme extention
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

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
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

    Public Property URL() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property Duration() As String
        Get
            Return _duration
        End Get
        Set(ByVal value As String)
            _duration = value
        End Set
    End Property

    Public Property Bitrate() As String
        Get
            Return _bitrate
        End Get
        Set(ByVal value As String)
            _bitrate = value
        End Set
    End Property

    Public Property WebURL() As String
        Get
            Return _weburl
        End Get
        Set(ByVal value As String)
            _weburl = value
        End Set
    End Property
    ''' <summary>
    ''' trigger to remove theme
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

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        If _ms IsNot Nothing Then
            Me.Dispose(True)
            Me.disposedValue = False    'Since this is not a real Dispose call...
        End If

        _ext = String.Empty
        _title = String.Empty
        _id = String.Empty
        _isEdit = False
        _toRemove = False
        _url = String.Empty
        _weburl = String.Empty
        _description = String.Empty
        _duration = String.Empty
        _bitrate = String.Empty
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
    ''' Delete the movie themes
    ''' </summary>
    ''' <param name="DBMovie"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteMovieTheme(ByVal DBMovie As Database.DBElement)
        If String.IsNullOrEmpty(DBMovie.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(DBMovie, Enums.ModifierType.MainTheme)
                For Each t As String In Master.eSettings.FileSystemValidThemeExts
                    If File.Exists(String.Concat(a, t)) Then
                        Delete(String.Concat(a, t))
                    End If
                Next
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.Filename & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete the tv show themes
    ''' </summary>
    ''' <param name="DBTVShow"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTVShowTheme(ByVal DBTVShow As Database.DBElement)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow, Enums.ModifierType.MainTheme)
                For Each t As String In Master.eSettings.FileSystemValidThemeExts
                    If File.Exists(String.Concat(a, t)) Then
                        Delete(String.Concat(a, t))
                    End If
                Next
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBTVShow.Filename & ">")
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
    ''' Loads this theme from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the theme file</param>
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
                    Me._url = sPath
                End Using
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sPath & ">")
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Loads this theme from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the theme file</param>
    ''' <remarks></remarks>
    Public Sub FromWeb(ByVal sURL As String, Optional ByVal webURL As String = "")
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim tTheme As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        Try
            tTheme = WebPage.DownloadFile(sURL, "", True, "theme", webURL)
            If Not String.IsNullOrEmpty(tTheme) Then

                If Me._ms IsNot Nothing Then
                    Me._ms.Dispose()
                End If
                Me._ms = New MemoryStream()

                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                Me._ms.Write(retSave, 0, retSave.Length)

                Me._ext = Path.GetExtension(tTheme)
                Me._url = sURL
                logger.Debug("Theme downloaded: " & sURL)
            Else
                logger.Warn("Theme NOT downloaded: " & sURL)
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sURL & ">")
        End Try

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Public Function SaveAsMovieTheme(ByVal mMovie As Database.DBElement) As String
        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {mMovie})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnThemeSave_Movie, params, False)
            Catch ex As Exception
            End Try

            Dim fExt As String = Path.GetExtension(Me._ext)
            For Each a In FileUtils.GetFilenameList.Movie(mMovie, Enums.ModifierType.MainTheme)
                If Not File.Exists(String.Concat(a, fExt)) OrElse (isEdit OrElse Master.eSettings.MovieThemeKeepExisting) Then
                    Save(String.Concat(a, fExt))
                    strReturn = (String.Concat(a, fExt))
                End If
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Determines whether a theme is allowed to be downloaded. This is determined
    ''' by a combination of the Master.eSettings.LockTheme settings,
    ''' whether the path is valid, and whether the Master.eSettings.OverwriteTheme
    ''' flag is set. 
    ''' </summary>
    ''' <param name="mMovie">The intended path to save the theme</param>
    ''' <returns><c>True</c> if a download is allowed, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Function IsAllowedToDownload(ByVal mMovie As Database.DBElement) As Boolean
        Try
            With Master.eSettings
                If (String.IsNullOrEmpty(mMovie.ThemePath) OrElse .MovieThemeKeepExisting) AndAlso .MovieThemeTvTunesEnable AndAlso
                    (mMovie.IsSingle AndAlso .MovieThemeTvTunesMoviePath) OrElse
                    (mMovie.IsSingle AndAlso .MovieThemeTvTunesSub AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesSubDir)) OrElse
                    (.MovieThemeTvTunesCustom AndAlso Not String.IsNullOrEmpty(.MovieThemeTvTunesCustomPath)) Then
                    Return True
                Else
                    Return False
                End If
            End With
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
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
