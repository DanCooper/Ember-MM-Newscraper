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
        Private _trailerlist As New List(Of Trailers)
        Private _Cancelled As Boolean
        Private intHTTP As HTTP = Nothing
#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Clear()
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

        Public Async Function Init(ByVal sIMDBID As String) As Threading.Tasks.Task
            IMDBID = sIMDBID
            _Cancelled = False
            Await GetMovieTrailers()
        End Function

        Public Sub CancelAsync()

            'If bwIMDB.IsBusy Then
            If Not IsNothing(intHTTP) Then
                intHTTP.Cancel()
            End If
            _Cancelled = True
            'bwIMDB.CancelAsync()
            'End If

            'While bwIMDB.IsBusy
            'Application.DoEvents()
            'Threading.Thread.Sleep(50)
            'End While
        End Sub

        Private Async Function GetMovieTrailers() As Threading.Tasks.Task

            Dim BaseURL As String = "http://www.imdb.com"
            Dim SearchURL As String

            Dim TrailerPageNumber As Integer = 0
            Dim Trailers As MatchCollection
            Dim TrailerPage As String
            Dim TrailerTitle As String
            Dim Link As Match
            Dim currPage As Integer = 0
            Dim _ImdbTrailerPage As String = String.Empty

            Try
                If Not String.IsNullOrEmpty(IMDBID) Then
                    Dim pPattern As String = "of [0-9]{1,3}"                            'Trailer page # of #
                    Dim tPattern As String = "imdb/(vi[0-9]+)/"                         'Specific trailer website
                    Dim nPattern As String = "<title>.*?\((?<TITLE>.*?)\).*?</title>"   'Trailer title inside brakets
                    Dim mPattern As String = "<title>(?<TITLE>.*?)</title>"             'Trailer title without brakets

                    SearchURL = String.Concat(BaseURL, "/title/tt", IMDBID, "/videogallery/content_type-Trailer") 'IMDb trailer website of a specific movie, filtered by trailers only

                    'download trailer website
                    intHTTP = New HTTP
                    _ImdbTrailerPage = Await intHTTP.DownloadData(SearchURL)
                    intHTTP.Dispose()
                    intHTTP = Nothing
                    If _Cancelled Then Return

                    If _ImdbTrailerPage.ToLower.Contains("page not found") Then
                        _ImdbTrailerPage = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(_ImdbTrailerPage) Then
                        'check if more than one page exist
                        Link = Regex.Match(_ImdbTrailerPage, pPattern)

                        If Link.Success Then
                            TrailerPageNumber = Convert.ToInt32(Link.Value.Substring(3))

                            If TrailerPageNumber > 0 Then
                                currPage = Convert.ToInt32(Math.Ceiling(TrailerPageNumber / 10))

                                For i As Integer = 1 To currPage
                                    If Not i = 1 Then
                                        intHTTP = New HTTP
                                        _ImdbTrailerPage = Await intHTTP.DownloadData(String.Concat(SearchURL, "?page=", i))
                                        intHTTP.Dispose()
                                        intHTTP = Nothing
                                        If _Cancelled Then Return
                                    End If

                                    'search all trailer on trailer website
                                    Trailers = Regex.Matches(_ImdbTrailerPage, tPattern)
                                    Dim linksCollection As String() = From m As Object In Trailers Select CType(m, Match).Value Distinct.ToArray()

                                    For Each trailer As String In linksCollection
                                        'go to specific trailer website
                                        Dim Website As String = String.Concat(BaseURL, "/video/", trailer)
                                        intHTTP = New HTTP
                                        TrailerPage = Await intHTTP.DownloadData(Website)
                                        intHTTP.Dispose()
                                        intHTTP = Nothing
                                        If _Cancelled Then Return
                                        TrailerTitle = Regex.Match(TrailerPage, nPattern).Groups(1).Value.ToString.Trim
                                        If String.IsNullOrEmpty(TrailerTitle) Then
                                            TrailerTitle = Regex.Match(TrailerPage, mPattern).Groups(1).Value.ToString.Trim
                                            TrailerTitle = TrailerTitle.Replace("- IMDb", String.Empty).Trim
                                        End If
                                        'try to get playtime
                                        Dim Details As String = String.Concat(Website, "imdb/single?format=480p")
                                        intHTTP = New HTTP
                                        Dim DetailsPage As String = Await intHTTP.DownloadData(Details)
                                        intHTTP.Dispose()
                                        intHTTP = Nothing
                                        If _Cancelled Then Return
                                        Dim trailerLenght As String = Regex.Match(DetailsPage, "duration title-hover"">\((?<LENGHT>.*?)\)</span>").Groups(1).Value.ToString
                                        _trailerlist.Add(New Trailers With {.URL = Website, .Description = TrailerTitle, .WebURL = Website, .Duration = trailerLenght, .Source = "IMDB"})
                                    Next
                                Next
                            End If

                        End If
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

        End Function

#End Region 'Methods

    End Class

End Namespace