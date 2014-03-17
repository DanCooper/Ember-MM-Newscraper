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

Public Class Trailers

#Region "Fields"

    Private _url As String
    Private _description As String

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Properties"

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

#End Region 'Properties

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub Clear()
        _url = String.Empty
        _description = String.Empty
    End Sub

    Public Sub Cancel()
        'Me.WebPage.Cancel()
    End Sub

    ''' <summary>
    ''' Remove existing trailers from the given path.
    ''' </summary>
    ''' <param name="sPath">Path to look for trailers</param>
    ''' <param name="NewTrailer"></param>
    ''' <remarks>
    ''' 2013/11/08 Dekker500 - Enclosed file accessors in Try block
    ''' </remarks>
    Public Shared Sub DeleteTrailers(ByVal sPath As String, ByVal NewTrailer As String)
        Dim parPath As String = Directory.GetParent(sPath).FullName
        Dim tmpName As String = Path.Combine(parPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath)))
        Dim tmpNameNoStack As String = Path.Combine(parPath, Path.GetFileNameWithoutExtension(sPath))

        Try
            For Each t As String In Master.eSettings.FileSystemValidExts
                If File.Exists(String.Concat(tmpName, "-trailer", t)) AndAlso Not String.Concat(tmpName, "-trailer", t).ToLower = NewTrailer.ToLower Then
                    File.Delete(String.Concat(tmpName, "-trailer", t))
                ElseIf File.Exists(String.Concat(tmpName, "[trailer]", t)) AndAlso Not String.Concat(tmpName, "[trailer]", t).ToLower = NewTrailer.ToLower Then
                    File.Delete(String.Concat(tmpName, "[trailer]", t))
                ElseIf File.Exists(String.Concat(tmpNameNoStack, "-trailer", t)) AndAlso Not String.Concat(tmpNameNoStack, "-trailer", t).ToLower = NewTrailer.ToLower Then
                    File.Delete(String.Concat(tmpNameNoStack, "-trailer", t))
                ElseIf File.Exists(String.Concat(tmpNameNoStack, "[trailer]", t)) AndAlso Not String.Concat(tmpNameNoStack, "[trailer]", t).ToLower = NewTrailer.ToLower Then
                    File.Delete(String.Concat(tmpNameNoStack, "[trailer]", t))
                ElseIf FileUtils.Common.isBDRip(sPath) AndAlso File.Exists(String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, Path.DirectorySeparatorChar, "index-trailer", t)) AndAlso Not String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, Path.DirectorySeparatorChar, "index-trailer", t).ToLower = NewTrailer.ToLower Then
                    File.Delete(String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, Path.DirectorySeparatorChar, "index-trailer", t))
                End If
            Next
        Catch ex As Exception
            Master.eLog.Error(GetType(Trailers), ex.Message, ex.StackTrace, "Error")
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
    ''' Given a list of Trailers, determine which one best matches the user's
    ''' configured preferred trailer format. Return that URL in the <paramref name="tUrl"/>
    ''' parameter, and returns <c>True</c>.
    ''' </summary>
    ''' <param name="tUrl"></param>
    ''' <param name="UrlList"><c>List</c> of <c>Trailer</c>s</param>
    ''' <param name="sPath"></param>
    ''' <param name="isSingle">Flag to indicate whether a scrape of a single item was requested (Enums.ScrapeType.SingleScrape), or whether this is part of a multi-item scrape</param>
    ''' <returns><c>True</c> if an appropriate trailer was found. The URL for the trailer is returned in
    ''' <paramref name="tUrl"/>. <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function PreferredTrailer(ByRef tUrl As String, ByRef UrlList As List(Of Trailers), ByVal sPath As String, ByVal isSingle As Boolean) As Boolean
        PreferredTrailer = False
        Try
            If IsAllowedToDownload(sPath, isSingle, True) Then 'TODO: check this
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
                    Else
                        tLink = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(tLink) Then
                        tUrl = tLink
                        Return True
                    End If
                Next
            End If
        Catch ex As Exception
            Master.eLog.Error(GetType(Trailers), ex.Message, ex.StackTrace, "Error")
        End Try
    End Function
    ''' <summary>
    ''' Downloads the trailer found at the supplied <paramref name="sURL"/> and places
    ''' it in the supplied <paramref name="sPath"/>. 
    ''' </summary>
    ''' <param name="sPath">Path into which the trailer should be saved</param>
    ''' <param name="sURL">URL from which to get the trailer</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DownloadTrailer(ByVal sPath As String, ByVal isSingle As Boolean, ByVal sURL As String) As String
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        Dim lhttp As New HTTP
        Dim tTrailer As String = String.Empty
        'AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated

        tTrailer = lhttp.DownloadFile(sURL, Path.Combine(Master.TempPath, "trailer"), False, "trailer")
        Dim fExt As String = Path.GetExtension(tTrailer)
        For Each a In FileUtils.GetFilenameList.Movie(sPath, isSingle, Enums.ModType.Trailer)
            If File.Exists(a & fExt) Then
                File.Delete(a & fExt)
            End If
            File.Copy(tTrailer, a & fExt)
            tURL = a & fExt
        Next

        '' filename is managed in DownloadFile()
        'tURL = WebPage.DownloadFile(sURL, sPath, False, "trailer") 'ReportUpdate needs to be fixed

        'If Not String.IsNullOrEmpty(tURL) Then
        '    'delete any other trailer if enabled in settings and download successful
        '    If Master.eSettings.DeleteAllTrailers Then
        '        DeleteTrailers(sPath, tURL)
        '    End If
        'End If

        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Return tURL
    End Function
    ''' <summary>
    ''' Determines whether a trailer is allowed to be downloaded. This is determined
    ''' by a combination of the Master.eSettings.LockTrailer settings,
    ''' whether the path is valid, and whether the Master.eSettings.OverwriteTrailer
    ''' flag is set. 
    ''' </summary>
    ''' <param name="sPath">The intended path to save the trailer</param>
    ''' <param name="isDL">Flag to indicate whether the file is intended to be saved to the file system or not</param>
    ''' <param name="isSS">Flag to indicate whether a scrape of a single item was requested (Enums.ScrapeType.SingleScrape), or whether this is part of a multi-item scrape</param>
    ''' <returns><c>True</c> if a download is allowed, <c>False</c> otherwise</returns>
    ''' <remarks></remarks>
    Public Shared Function IsAllowedToDownload(ByVal sPath As String, ByVal isDL As Boolean, Optional ByVal isSS As Boolean = False) As Boolean
        'TODO Dekker500 - MUST VALIDATE whether these parameters are correct! I believe isDL and isSS are reversed in meanings (at least from the calling method's perspective)!!!!

        Dim fScanner As New Scanner

        If isDL Then
            If String.IsNullOrEmpty(fScanner.GetMovieTrailerPath(sPath)) OrElse Master.eSettings.MovieTrailerOverwrite Then
                Return True
            Else
                If isSS AndAlso String.IsNullOrEmpty(fScanner.GetMovieTrailerPath(sPath)) Then
                    If Not Master.eSettings.MovieLockTrailer Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            If Not Master.eSettings.MovieLockTrailer Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

#End Region 'Methods

End Class