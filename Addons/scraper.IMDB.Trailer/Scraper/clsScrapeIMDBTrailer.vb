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

Namespace IMDB

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private IMDBID As String
        Private _trailerlist As New List(Of MediaContainers.Trailer)

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal sIMDBID As String)
            Clear()
            IMDBID = sIMDBID
            GetMovieTrailers()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property TrailerList() As List(Of MediaContainers.Trailer)
            Get
                Return _trailerlist
            End Get
            Set(ByVal value As List(Of MediaContainers.Trailer))
                _trailerlist = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _trailerlist = New List(Of MediaContainers.Trailer)
        End Sub

        Private Sub GetMovieTrailers()

            Dim BaseURL As String = "http://www.imdb.com"
            Dim SearchURL As String

            Dim ImdbTrailerPage As String = String.Empty
            Dim Trailers As MatchCollection
            Dim TrailerDetails As Match
            Dim TrailerDetailsPage As String
            Dim TrailerDuration As String
            Dim TrailerTitle As String
            Dim sHTTP As New HTTP

            Try
                If Not String.IsNullOrEmpty(IMDBID) Then
                    Dim aPattern As String = "<div class=""article"">.*?<div class=""article"" id=""see_also"">"                                'Video Gallery results
                    Dim tPattern As String = "imdb\/(vi[0-9]+)"                                                                                 'Specific trailer website
                    Dim dPattern As String = "class=""vp-video-name"">(?<TITLE>.*?)<.*?class=""duration title-hover"">\((?<DURATION>.*?)\)"     'Trailer title and duration

                    SearchURL = String.Concat(BaseURL, "/title/tt", IMDBID, "/videogallery/content_type-trailer") 'IMDb trailer website of a specific movie, filtered by trailers only

                    'download trailer website
                    ImdbTrailerPage = sHTTP.DownloadData(SearchURL)
                    sHTTP = Nothing

                    If ImdbTrailerPage.ToLower.Contains("page not found") Then
                        ImdbTrailerPage = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(ImdbTrailerPage) Then
                        'filter HTML to Video Gallery only
                        Dim VideoGallery = Regex.Match(ImdbTrailerPage, aPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)

                        If VideoGallery.Success Then
                            'search all trailer on trailer website
                            Dim test = VideoGallery.Groups.Item(0).ToString
                            Trailers = Regex.Matches(VideoGallery.Groups.Item(0).ToString, tPattern)
                            Dim linksCollection As String() = From m As Object In Trailers Select CType(m, Match).Value Distinct.ToArray()

                            For Each trailer As String In linksCollection
                                'go to specific trailer website
                                Dim URLWebsite As String = String.Concat(BaseURL, "/video/", trailer)
                                sHTTP = New HTTP
                                TrailerDetailsPage = sHTTP.DownloadData(String.Concat(URLWebsite, "/imdb/single"))
                                sHTTP = Nothing
                                TrailerDetails = Regex.Match(TrailerDetailsPage, dPattern, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                                TrailerTitle = TrailerDetails.Groups("TITLE").Value.ToString.Trim
                                TrailerDuration = TrailerDetails.Groups("DURATION").Value.ToString.Trim
                                _trailerlist.Add(New MediaContainers.Trailer With {.Title = TrailerTitle, .URLWebsite = URLWebsite, .Duration = TrailerDuration, .Scraper = "IMDB", .Source = "IMDB"})
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

End Namespace