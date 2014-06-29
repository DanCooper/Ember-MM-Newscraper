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

Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class AppleTrailer

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private originaltitle As String
    Private _trailerlist As New List(Of Trailers)

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal sOriginalTitle As String)
        Clear()
        originaltitle = sOriginalTitle
        GetMovieTrailers()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property TrailerList() As List(Of Trailers)
        Get
            Return _trailerlist
        End Get
        Set(ByVal value As List(Of Trailers))
            _trailerlist = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub Clear()
        _trailerlist = New List(Of Trailers)
    End Sub

    Private Sub GetMovieTrailers()
        Dim BaseURL As String = "http://www.google.ch/search?q=apple+trailer+"
        Dim DownloadURL As String = "http://trailers.apple.com/trailers/"
        Dim prevQual As String = AdvancedSettings.GetSetting("TrailerPrefQual", "1080p")
        Dim urlHD As String = "/includes/extralarge.html"
        Dim urlHQ As String = "/includes/large.html"
        Dim SearchTitle As String
        Dim SearchURL As String

        If Not String.IsNullOrEmpty(originaltitle) Then
            SearchTitle = Web.HttpUtility.UrlEncode(originaltitle)
            SearchURL = String.Concat(BaseURL, SearchTitle)
        Else
            SearchURL = String.Empty
        End If

        Try
            If Not String.IsNullOrEmpty(SearchURL) Then
                Dim tDownloadURL As String = String.Empty
                Dim tDescription As New List(Of String)

                'get the first search result from Google
                Dim sHTTP As New HTTP
                Dim Html As String = sHTTP.DownloadData(SearchURL)
                sHTTP = Nothing

                Dim rPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                Dim tPattern As String = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
                Dim lPattern As String = "<li><a href=""includes/(?<URL>.*?)#"
                Dim uPattern As String = "<a class=""movieLink"" href=""(?<URL>.*?)\?"
                Dim zPattern As String = "<li.*?<a href=""(?<LINK>.*?)#.*?<h3 title="".*?>(?<TITLE>.*?)</h3>.*?duration"">(?<DURATION>.*?)</span>*.?</li>"

                Dim rResult As MatchCollection = Regex.Matches(Html, rPattern, RegexOptions.Singleline)

                If rResult.Count > 0 Then
                    Dim TrailerBaseURL As String = rResult.Item(0).Groups(1).Value
                    Dim TrailerSiteURL = String.Concat(TrailerBaseURL, urlHD)

                    If Not String.IsNullOrEmpty(TrailerBaseURL) Then
                        sHTTP = New HTTP
                        Dim sHtml As String = sHTTP.DownloadData(TrailerSiteURL)
                        sHTTP = Nothing

                        Dim zResult As MatchCollection = Regex.Matches(sHtml, zPattern, RegexOptions.Singleline)

                        For ctr As Integer = 0 To zResult.Count - 1
                            _trailerlist.Add(New Trailers With {.URL = zResult.Item(ctr).Groups(1).Value, .Description = zResult.Item(ctr).Groups(2).Value, .Lenght = zResult.Item(ctr).Groups(3).Value})
                        Next

                        For Each trailer In _trailerlist
                            Dim DLURL As String = String.Empty
                            Dim TrailerSiteLink As String = String.Concat(TrailerBaseURL, "/", trailer.URL)

                            sHTTP = New HTTP
                            Dim zHtml As String = sHTTP.DownloadData(TrailerSiteLink)
                            sHTTP = Nothing

                            Dim yResult As MatchCollection = Regex.Matches(zHtml, uPattern, RegexOptions.Singleline)

                            If yResult.Count > 0 Then
                                tDownloadURL = Web.HttpUtility.HtmlDecode(yResult.Item(0).Groups(1).Value)
                                tDownloadURL = tDownloadURL.Replace("720p", prevQual)
                                trailer.WebURL = tDownloadURL
                                tDownloadURL = tDownloadURL.Replace("1080p", "h1080p")
                                tDownloadURL = tDownloadURL.Replace("720p", "h720p")
                                tDownloadURL = tDownloadURL.Replace("480p", "h480p")
                                trailer.URL = tDownloadURL
                                Select Case prevQual
                                    Case "1080p"
                                        trailer.Resolution = Enums.TrailerQuality.HD1080p
                                    Case "720p"
                                        trailer.Resolution = Enums.TrailerQuality.HD720p
                                    Case "480p"
                                        trailer.Resolution = Enums.TrailerQuality.HQ480p
                                End Select
                            End If
                        Next
                    End If
                End If
            End If


        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

#End Region 'Methods

End Class
