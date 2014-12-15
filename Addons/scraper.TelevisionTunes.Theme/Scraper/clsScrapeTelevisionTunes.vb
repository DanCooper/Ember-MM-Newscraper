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

'Imports System.Globalization
'Imports System.IO
'Imports System.IO.Compression
'Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace TelevisionTunes

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Private originaltitle As String
        Private _themelist As New List(Of Themes)

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sOriginalTitle As String)
            Clear()
            originaltitle = sOriginalTitle
            GetMovieThemes()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property ThemeList() As List(Of Themes)
            Get
                Return _themelist
            End Get
            Set(ByVal value As List(Of Themes))
                _themelist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _themelist = New List(Of Themes)
        End Sub

        Private Sub GetMovieThemes()
            Dim BaseURL As String = "http://www.televisiontunes.com/search.php?searWords={0}&Send=Search"
            Dim DownloadURL As String = "http://www.televisiontunes.com/download.php?f="
            Dim SearchTitle As String
            Dim SearchURL As String

            If Not String.IsNullOrEmpty(originaltitle) Then
                SearchTitle = Web.HttpUtility.UrlEncode(originaltitle)
                SearchURL = String.Format(BaseURL, SearchTitle)
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
                    Dim tLength As String = String.Empty
                    Dim tBitrate As String = String.Empty

                    Dim rPattern As String = "1\.&nbsp;(?<RESULTS>.*?)</b>"
                    Dim sPattern As String = "'<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>'"
                    Dim nPattern As String = "<\/a><br><a href=""(?<NEXTURL>.*?)""><b>Next<\/b><\/a>"

                    Dim sHTTP As New HTTP
                    Dim Html As String = sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    While Not String.IsNullOrEmpty(Html)
                        Dim rResult As MatchCollection = Regex.Matches(Html, rPattern, RegexOptions.Singleline)

                        If rResult.Count > 0 Then
                            Dim sHTML As String = rResult.Item(0).Groups(1).Value

                            Dim sResult As MatchCollection = Regex.Matches(sHTML, sPattern, RegexOptions.Singleline)

                            For ctr As Integer = 0 To sResult.Count - 1
                                tWebURL = Web.HttpUtility.HtmlDecode(sResult.Item(ctr).Groups(1).Value)
                                tTitle = sResult.Item(ctr).Groups(2).Value
                                tID = GetFileID(tWebURL)
                                tURL = String.Concat(DownloadURL, tID)

                                If Not String.IsNullOrEmpty(tID) Then
                                    _themelist.Add(New Themes With {.Title = tTitle, .ID = tID, .URL = tURL, .Description = tDescription, .Duration = tLength, .Bitrate = tBitrate, .WebURL = tWebURL})
                                End If
                            Next
                        End If

                        'check if there is a "next" page
                        If Regex.IsMatch(Html, nPattern) Then
                            sHTTP = New HTTP
                            Html = String.Empty
                            Html = sHTTP.DownloadData(String.Concat("http://www.televisiontunes.com/", Regex.Match(Html, nPattern).Groups(1).Value))
                            sHTTP = Nothing
                        Else
                            Html = String.Empty
                        End If
                    End While
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

        End Sub

        Private Function GetFileID(ByVal URL As String) As String
            If Not String.IsNullOrEmpty(URL) Then
                Dim fileID As String
                fileID = URL.Replace("http://www.televisiontunes.com/", String.Empty)
                fileID = fileID.Replace(".html", String.Empty).Trim

                Return fileID
            End If
            Return String.Empty
        End Function

#End Region 'Methods

    End Class

End Namespace

