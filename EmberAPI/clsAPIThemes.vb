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

<Serializable()>
Public Class Themes
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

    Public ReadOnly Property hasMemoryStream() As Boolean
        Get
            Return _ms IsNot Nothing
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

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
    ''' Delete the movie themes
    ''' </summary>
    ''' <param name="tDBElement"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_Movie(ByVal tDBElement As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        If String.IsNullOrEmpty(tDBElement.Filename) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTheme, ForceFileCleanup)
                For Each t As String In Master.eSettings.FileSystemValidThemeExts
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
    ''' Delete the tv show themes
    ''' </summary>
    ''' <param name="DBTVShow"><c>DBMovie</c> structure representing the movie on which we should operate</param>
    ''' <remarks></remarks>
    Public Shared Sub Delete_TVShow(ByVal DBTVShow As Database.DBElement) ', ByVal ForceFileCleanup As Boolean)
        If String.IsNullOrEmpty(DBTVShow.ShowPath) Then Return

        Try
            For Each a In FileUtils.GetFilenameList.TVShow(DBTVShow, Enums.ModifierType.MainTheme) ', ForceFileCleanup)
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

    Public Shared Function GetPreferredMovieTheme(ByRef ThemeList As List(Of MediaContainers.Theme), ByRef trlResult As MediaContainers.Theme) As Boolean
        If ThemeList.Count = 0 Then Return False
        trlResult = Nothing

        trlResult = ThemeList.Item(0)

        If trlResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetPreferredTVShowTheme(ByRef ThemeList As List(Of MediaContainers.Theme), ByRef trlResult As MediaContainers.Theme) As Boolean
        If ThemeList.Count = 0 Then Return False
        trlResult = Nothing

        trlResult = ThemeList.Item(0)

        If trlResult IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Loads this theme from the contents of the supplied file
    ''' </summary>
    ''' <param name="sPath">Path to the theme file</param>
    ''' <remarks></remarks>
    Public Sub LoadFromFile(ByVal sPath As String)
        If Not String.IsNullOrEmpty(sPath) AndAlso File.Exists(sPath) Then
            _ms = New MemoryStream()
            Using fsTheme As FileStream = File.OpenRead(sPath)
                Dim memStream As New MemoryStream
                memStream.SetLength(fsTheme.Length)
                fsTheme.Read(memStream.GetBuffer, 0, CInt(Fix(fsTheme.Length)))
                _ms.Write(memStream.GetBuffer, 0, CInt(Fix(fsTheme.Length)))
                _ms.Flush()
            End Using
            _ext = Path.GetExtension(sPath)
        Else
            _ms = New MemoryStream
        End If
    End Sub
    ''' <summary>
    ''' Loads this theme from the supplied URL
    ''' </summary>
    ''' <param name="sURL">URL to the theme file</param>
    ''' <remarks></remarks>
    Public Sub LoadFromWeb(ByVal sURL As String, Optional ByVal webURL As String = "")
        If String.IsNullOrEmpty(sURL) Then Return

        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim tTheme As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        Try
            tTheme = WebPage.DownloadFile(sURL, String.Empty, True, "theme", webURL)
            If Not String.IsNullOrEmpty(tTheme) Then
                If _ms IsNot Nothing Then
                    _ms.Dispose()
                End If
                _ms = New MemoryStream()
                Dim retSave() As Byte
                retSave = WebPage.ms.ToArray
                _ms.Write(retSave, 0, retSave.Length)
                _ext = Path.GetExtension(tTheme)
            Else
                logger.Warn("Theme NOT downloaded: " & sURL)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & sURL & ">")
        End Try

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
    End Sub

    Public Sub LoadFromWeb(ByVal sTheme As MediaContainers.Theme)
        LoadFromWeb(sTheme.URLAudioStream, sTheme.URLWebsite)
    End Sub

    Public Function Save_Movie(ByVal tDBElement As Database.DBElement) As String
        If Not tDBElement.Theme.ThemeOriginal.hasMemoryStream Then Return String.Empty

        Dim strReturn As String = String.Empty

        Try
            Try
                Dim params As New List(Of Object)(New Object() {tDBElement})
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnThemeSave_Movie, params, False)
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            For Each a In FileUtils.GetFilenameList.Movie(tDBElement, Enums.ModifierType.MainTheme)
                SaveToFile(String.Concat(a, _ext))
                strReturn = (String.Concat(a, _ext))
            Next

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Clear() 'Dispose to save memory
        Return strReturn
    End Function

    Public Function Save_TVShow(ByVal tDBElement As Database.DBElement) As String
        If Not tDBElement.Theme.ThemeOriginal.hasMemoryStream Then Return String.Empty

        Dim strReturn As String = String.Empty

        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {tDbElement})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnThemeSave_TVShow, params, False)
            'Catch ex As Exception
            'End Try

            For Each a In FileUtils.GetFilenameList.TVShow(tDBElement, Enums.ModifierType.MainTheme)
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
