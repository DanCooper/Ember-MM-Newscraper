﻿' ################################################################################
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

Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace GoEar

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Private originaltitle As String
        Private listtitle As String
        Private _themelist As New List(Of MediaContainers.Theme)

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sOriginalTitle As String, ByVal sListTitle As String)
            Clear()
            originaltitle = sOriginalTitle
            listtitle = sListTitle
            GetMovieThemes()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ThemeList() As List(Of MediaContainers.Theme)
            Get
                Return _themelist
            End Get
            Set(ByVal value As List(Of MediaContainers.Theme))
                _themelist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _themelist = New List(Of MediaContainers.Theme)
        End Sub

        Private Sub GetMovieThemes()
            Dim BaseURL As String = "http://www.goear.com/search/"
            Dim DownloadURL As String = "http://www.goear.com/action/sound/get/"
            Dim SearchTitle As String
            Dim SearchURL As String

            If Not String.IsNullOrEmpty(originaltitle) Then
                SearchTitle = HttpUtility.UrlEncode(originaltitle)
                SearchURL = String.Concat(BaseURL, SearchTitle)
            ElseIf Not String.IsNullOrEmpty(listtitle) Then
                SearchTitle = HttpUtility.UrlEncode(listtitle)
                SearchURL = String.Concat(BaseURL, SearchTitle)
            Else
                SearchURL = String.Empty
            End If

            Try
                If Not String.IsNullOrEmpty(SearchURL) Then
                    Dim tTitle As String = String.Empty
                    Dim tID As String = String.Empty
                    Dim tWebURL As String = String.Empty
                    Dim tURL As String = String.Empty
                    Dim tDescription As String = String.Empty
                    Dim tDuration As String = String.Empty
                    Dim tBitrate As String = String.Empty

                    Dim sHTTP As New HTTP
                    Dim Html As String = sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    Dim strPatternSearchResultsArea As String = "<div class=""board search_board"">(?<RESULTS>.*?)</ol>"

                    Dim regResultResultsArea As MatchCollection = Regex.Matches(Html, strPatternSearchResultsArea, RegexOptions.Singleline)

                    If regResultResultsArea.Count > 0 Then
                        Dim strPatternSearchResults As String = "<li class=""board_item sound_item group"">(.*?)<\/div>.*?<\/li>"

                        Dim regSearchResults As MatchCollection = Regex.Matches(regResultResultsArea.Item(0).Groups("RESULTS").Value, strPatternSearchResults, RegexOptions.Singleline Or RegexOptions.IgnoreCase)
                        For ctr As Integer = 0 To regSearchResults.Count - 1
                            Dim strPatternDetails As String = "redir=""(?<URL>.*?)"">.*?soundid=""(?<ID>.*?)"" title=""(?<TITLE>.*?)"" artist=""(?<DESCRIPTION>.*?)"".*?length"" title="".*?>(?<DURATION>.*?)<.*?kbps"" title=""Kbps"".*?>(?<BITRATE>.*?)<"

                            Dim regDetails As Match = Regex.Match(regSearchResults.Item(ctr).Value, strPatternDetails, RegexOptions.Singleline Or RegexOptions.IgnoreCase)
                            tWebURL = HttpUtility.HtmlDecode(regDetails.Groups("URL").Value).Trim
                            tTitle = regDetails.Groups("TITLE").Value.Trim
                            tDescription = regDetails.Groups("DESCRIPTION").Value.Trim
                            tDuration = regDetails.Groups("DURATION").Value.Trim
                            tBitrate = regDetails.Groups("BITRATE").Value.Trim
                            tID = regDetails.Groups("ID").Value.Trim
                            tURL = String.Concat(DownloadURL, tID).Trim

                            If Not String.IsNullOrEmpty(tID) Then
                                _themelist.Add(New MediaContainers.Theme With {.URLAudioStream = tURL, .Description = tTitle, .Duration = tDuration, .Bitrate = tBitrate, .URLWebsite = tWebURL, .Scraper = "GoEar"})
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

        End Sub

        Private Function GetFileID(ByVal URL As String) As String
            If Not String.IsNullOrEmpty(URL) Then
                Dim fileID As String
                Dim startID As Integer
                Dim endID As Integer
                startID = URL.IndexOf("listen/") + 7
                endID = URL.IndexOf("/", startID)
                fileID = URL.Substring(startID, endID - startID)

                Return fileID
            End If
            Return String.Empty
        End Function

#End Region 'Methods

    End Class

End Namespace

