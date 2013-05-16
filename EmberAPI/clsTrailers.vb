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

    Private _TrailerList As New List(Of String)

#End Region 'Fields

#Region "Constructors"

    Public Sub New()
        Me.Clear()
    End Sub

#End Region 'Constructors

#Region "Events"

    Public Shared Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Public Sub Cancel()
        'Me.WebPage.Cancel()
    End Sub

    Public Sub Clear()
        Me._TrailerList.Clear()
    End Sub

    Public Shared Sub DeleteTrailers(ByVal sPath As String, ByVal NewTrailer As String)
        Dim parPath As String = Directory.GetParent(sPath).FullName
        Dim tmpName As String = Path.Combine(parPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath)))
        Dim tmpNameNoStack As String = Path.Combine(parPath, Path.GetFileNameWithoutExtension(sPath))
        For Each t As String In Master.eSettings.ValidExts
            If File.Exists(String.Concat(tmpName, "-trailer", t)) AndAlso Not String.Concat(tmpName, "-trailer", t).ToLower = NewTrailer.ToLower Then
                File.Delete(String.Concat(tmpName, "-trailer", t))
            ElseIf File.Exists(String.Concat(tmpName, "[trailer]", t)) AndAlso Not String.Concat(tmpName, "[trailer]", t).ToLower = NewTrailer.ToLower Then
                File.Delete(String.Concat(tmpName, "[trailer]", t))
            ElseIf File.Exists(String.Concat(tmpNameNoStack, "-trailer", t)) AndAlso Not String.Concat(tmpNameNoStack, "-trailer", t).ToLower = NewTrailer.ToLower Then
                File.Delete(String.Concat(tmpNameNoStack, "-trailer", t))
            ElseIf File.Exists(String.Concat(tmpNameNoStack, "[trailer]", t)) AndAlso Not String.Concat(tmpNameNoStack, "[trailer]", t).ToLower = NewTrailer.ToLower Then
                File.Delete(String.Concat(tmpNameNoStack, "[trailer]", t))
            ElseIf Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(sPath) AndAlso File.Exists(String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, "\", "index-trailer", t)) AndAlso Not String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, "\", "index-trailer", t).ToLower = NewTrailer.ToLower Then
                File.Delete(String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, "\", "index-trailer", t))
            End If
        Next
    End Sub

    Public Shared Sub DownloadProgressUpdated(ByVal iPercent As Integer)
        RaiseEvent ProgressUpdated(iPercent)
    End Sub

    Public Shared Function PreferredTrailer(ByRef tUrl As String, ByRef UrlList As List(Of String), ByVal sPath As String, ByVal isSingle As Boolean) As Boolean
        PreferredTrailer = False
        Try
            If Not Master.eSettings.UpdaterTrailersNoDownload AndAlso IsAllowedToDownload(sPath, isSingle, True) Then
                For Each aUrl As String In UrlList
                    Dim tLink As String = String.Empty
                    If Regex.IsMatch(aUrl, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                        Dim YT As New YouTube.Scraper
                        YT.GetVideoLinks(aUrl)
                        If YT.VideoLinks.ContainsKey(Master.eSettings.PreferredTrailerQuality) Then
                            tLink = YT.VideoLinks(Master.eSettings.PreferredTrailerQuality).URL
                        Else
                            Select Case Master.eSettings.PreferredTrailerQuality
                                Case Enums.TrailerQuality.HD1080p
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HQFLV).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                    End If
                                Case Enums.TrailerQuality.HD1080pVP8
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720pVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HD720pVP8).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HQVP8).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                    End If
                                Case Enums.TrailerQuality.HD720p
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HQFLV).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                    End If
                                Case Enums.TrailerQuality.HD720pVP8
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.HQVP8).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                    End If
                                Case Enums.TrailerQuality.HQFLV
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                    ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                    End If
                                Case Enums.TrailerQuality.HQVP8
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                    End If
                                Case Enums.TrailerQuality.SQMP4
                                    If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                        tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                    End If
                                Case Enums.TrailerQuality.SQFLV
                                    tLink = String.Empty
                                Case Enums.TrailerQuality.SQVP8
                                    tLink = String.Empty
                            End Select
                        End If
                    Else
                        tLink = String.Empty 'Me._TrailerList.Item(0).ToString
                    End If

                    If Not String.IsNullOrEmpty(tLink) Then
                        Return True
                    End If
                Next
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Function

    Public Shared Function DownloadTrailer(ByVal sPath As String, ByVal sURL As String) As String
        Dim WebPage As New HTTP
        Dim tURL As String = String.Empty
        AddHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        ' filename is managed in the DownloadFile
        'If Master.eSettings.VideoTSParentXBMC AndAlso FileUtils.Common.isBDRip(sPath) Then
        '    tURL = String.Concat(Directory.GetParent(Directory.GetParent(sPath).FullName).FullName, "\", "index", If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(sURL))
        'ElseIf Master.eSettings.MovieNameNFOStack Then
        '    Dim sPathStack As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath))
        '    tURL = Path.Combine(Directory.GetParent(sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(sPathStack), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(sURL)))
        'Else
        '    tURL = Path.Combine(Directory.GetParent(sPath).FullName, String.Concat(Path.GetFileNameWithoutExtension(sPath), If(Master.eSettings.DashTrailer, "-trailer", "[trailer]"), Path.GetExtension(sURL)))
        'End If
        'If Not String.IsNullOrEmpty(sURL) Then
        tURL = WebPage.DownloadFile(sURL, Path.GetFileNameWithoutExtension(sPath), True, "trailer")

        If Not String.IsNullOrEmpty(tURL) Then
            'delete any other trailer if enabled in settings and download successful
            If Master.eSettings.DeleteAllTrailers Then
                DeleteTrailers(sPath, tURL)
            End If
        End If
        'End If
        RemoveHandler WebPage.ProgressUpdated, AddressOf DownloadProgressUpdated
        Return tURL
    End Function

    Public Shared Function IsAllowedToDownload(ByVal sPath As String, ByVal isDL As Boolean, Optional ByVal isSS As Boolean = False) As Boolean
        Dim fScanner As New Scanner

        If isDL Then
            If String.IsNullOrEmpty(fScanner.GetTrailerPath(sPath)) OrElse Master.eSettings.OverwriteTrailer Then
                Return True
            Else
                If isSS AndAlso String.IsNullOrEmpty(fScanner.GetTrailerPath(sPath)) Then
                    If Not Master.eSettings.LockTrailer Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            If Not Master.eSettings.LockTrailer Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

#End Region 'Methods

End Class