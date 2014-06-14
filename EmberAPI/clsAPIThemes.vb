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
Imports EmberAPI
Imports NLog

Public Class Themes
    Implements IDisposable
#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private _title As String
    Private _id As String
    Private _url As String
    Private _weburl As String
    Private _description As String
    Private _length As String
    Private _bitrate As String

    Private _ms As MemoryStream
    Private Ret As Byte()

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"

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

    Public Property Length() As String
        Get
            Return _length
        End Get
        Set(ByVal value As String)
            _length = value
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

        _title = String.Empty
        _id = String.Empty
        _url = String.Empty
        _weburl = String.Empty
        _description = String.Empty
        _length = String.Empty
        _bitrate = String.Empty
    End Sub

    Public Sub Cancel()
        'Me.WebPage.Cancel()
    End Sub

    ''' <summary>
    ''' Remove existing trailers from the given path.
    ''' </summary>
    ''' <param name="sPath">Path to look for trailers</param>
    ''' <param name="NewTheme"></param>
    ''' <remarks>
    ''' 2013/11/08 Dekker500 - Enclosed file accessors in Try block
    ''' </remarks>
    Public Shared Sub DeleteThemes(ByVal sPath As String, ByVal NewTheme As String)

    End Sub
    ''' <summary>
    ''' Downloads the theme found at the supplied <paramref name="sURL"/> and places
    ''' it in the supplied <paramref name="sPath"/>. 
    ''' </summary>
    ''' <param name="sPath">Path into which the theme should be saved</param>
    ''' <param name="sTheme">theme container</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadTheme(ByVal sPath As String, ByVal isSingle As Boolean, ByVal sTheme As Themes) As String
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim sURL As String = sTheme.URL
        Dim sWebURL As String = sTheme.WebURL
        Dim tTheme As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        tTheme = WebPage.DownloadFile(sURL, "", False, "theme", sWebURL)
        If Not String.IsNullOrEmpty(tTheme) Then
            If Not IsNothing(Me._ms) Then
                Me._ms.Dispose()
            End If
            Me._ms = New MemoryStream()

            Dim retSave() As Byte
            retSave = WebPage.ms.ToArray
            Me._ms.Write(retSave, 0, retSave.Length)


            Dim fExt As String = Path.GetExtension(tTheme)
            For Each a In FileUtils.GetFilenameList.Movie(sPath, isSingle, Enums.MovieModType.Theme)
                If Not File.Exists(a & fExt) OrElse Master.eSettings.MovieThemeOverwrite Then
                    If File.Exists(a & fExt) Then
                        File.Delete(a & fExt)
                    End If
                    Directory.CreateDirectory(Directory.GetParent(a & fExt).FullName)
                    SaveAsTheme(a & fExt)
                    tURL = a & fExt
                End If
            Next
            File.Delete(tTheme)
        End If
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Return tURL
    End Function

    Public Sub SaveAsTheme(filename As String)
        Dim retSave() As Byte
        retSave = Me._ms.ToArray

        Using FileStream As Stream = File.OpenWrite(filename)
            FileStream.Write(retSave, 0, retSave.Length) 'check if it works
        End Using
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
    Public Function IsAllowedToDownload(ByVal mMovie As Structures.DBMovie) As Boolean
        Try
            With Master.eSettings
                If (String.IsNullOrEmpty(mMovie.ThemePath) OrElse .MovieThemeOverwrite) AndAlso .MovieXBMCThemeEnable AndAlso _
                    (mMovie.isSingle AndAlso .MovieXBMCThemeMovie) OrElse _
                    (mMovie.isSingle AndAlso .MovieXBMCThemeSub AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeSubDir)) OrElse _
                    (.MovieXBMCThemeCustom AndAlso Not String.IsNullOrEmpty(.MovieXBMCThemeCustomPath)) Then
                    Return True
                Else
                    Return False
                End If
            End With
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name, ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Raises the ProgressUpdated event, passing the iPercent value to indicate percent completed.
    ''' </summary>
    ''' <param name="iPercent">Integer representing percentage completed</param>
    ''' <remarks></remarks>
    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"

#End Region 'Fields

    End Structure

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
