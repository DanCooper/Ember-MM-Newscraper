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

Public Class HDTrailersNetTrailer

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


    ''' <summary>
    ''' Scrapes avalaible trailerlinks from HD-Trailer.net
    ''' </summary>
    ''' <remarks>Try to find trailerlinks for selected movie and add results to global trailerlist
    ''' 
    ''' 2014/09/26 Cocotus - First implementation
    ''' </remarks>
    Private Sub GetMovieTrailers()
        Try
            Dim SearchURL As String
            If Not String.IsNullOrEmpty(originaltitle) Then
                'first put the searchurl for google together
                Dim BaseURL As String = "http://www.google.ch/search?q="
                Dim searchTerm As String = "site:http://www.hd-trailers.net/ " + """" + originaltitle + """"
                searchTerm = Web.HttpUtility.UrlEncode(searchTerm)
                SearchURL = String.Concat(BaseURL, searchTerm)
                '--> i.e http://www.google.ch/search?q=site%3Ahttp%3A%2F%2Fwww.hd-trailers.net%2F+"Transformers%3A+Age+of+Extinction"

                'performing google search to find avalaible links on hd-trailers.net
                Dim sHTTP As New HTTP
                Dim Html As String = sHTTP.DownloadData(SearchURL)
                sHTTP = Nothing

                'Now extract links from googleresults
                Dim googleresultPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                Dim trailerWEBURLResults As MatchCollection = Regex.Matches(Html, googleresultPattern, RegexOptions.Singleline)

                'check if something was found
                If trailerWEBURLResults.Count > 0 Then
                    'First result from google search should be the right thing
                    Dim TrailerWEBPageURL As String = trailerWEBURLResults.Item(0).Groups(1).Value
                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                        'find avalaible links on hd-trailers.net movie site
                        sHTTP = New HTTP
                        Dim sHtml As String = sHTTP.DownloadData(TrailerWEBPageURL)
                        sHTTP = Nothing

                        'search for trailerlinks in downloaded HTML code, i.e.
                        '<td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h480p.mov" rel="lightbox[res480p 852 480]" title="Terminator Salvation - Trailer 1 - 480p">480p</a></td>
                        '<td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h720p.mov" rel="lightbox[res720p 1280 720]" title="Terminator Salvation - Trailer 1 - 720p">720p</a></td>
                        '<td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h1080p.mov" rel="lightbox[res1080p 1920 1080]" title="Terminator Salvation - Trailer 1 - 1080p">1080p</a></td>
                        '--> we can extract URL and title
                        Dim trailerlinkPattern As String = "<a href=""(?<URL>.*?)"".*?title=""(?<TITLE>.*?)"""
                        Dim trailerURLResults As MatchCollection = Regex.Matches(sHtml, trailerlinkPattern, RegexOptions.Singleline)
                        'check if something was found
                        If trailerURLResults.Count > 0 Then
                            'go to each result and check if its a valid trailerlink...
                            For Each item As Match In trailerURLResults
                                If item.Groups.Count > 0 Then
                                    'only accept mov and mp4 files for now
                                    If Not item.Groups(1).Value Is Nothing AndAlso (item.Groups(1).Value.EndsWith("p.mov") OrElse item.Groups(1).Value.EndsWith("p.mp4")) Then
                                        Dim trailer As New Trailers
                                        'trailer description
                                        If Not item.Groups(2).Value Is Nothing Then
                                            trailer.Description = item.Groups(2).Value
                                        End If
                                        'trailer URLs
                                        trailer.URL = item.Groups(1).Value
                                        trailer.WebURL = item.Groups(1).Value
                                        'trailer extension
                                        trailer.Extention = IO.Path.GetExtension(trailer.URL)
                                        'trailer source
                                        trailer.Source = "HD-Trailers.net"
                                        '..and most important: trailer quality
                                        If item.Groups(1).Value.Contains("1080p") Then
                                            '1080p
                                            trailer.Quality = Enums.TrailerQuality.HD1080p
                                        ElseIf item.Groups(1).Value.Contains("720p") Then
                                            '720p
                                            trailer.Quality = Enums.TrailerQuality.HD720p
                                        Else
                                            'other                     
                                            trailer.Quality = Enums.TrailerQuality.HQ480p
                                        End If
                                        'add scraped trailer to global list if Quality is
                                        _trailerlist.Add(trailer)
                                    End If
                                End If
                            Next
                        Else
                            logger.Debug("[" & Master.currMovie.Movie.Title & "] No trailers found on specific movie page of HD-Trailer.net! SearchURL: " & TrailerWEBPageURL)
                        End If
                    Else
                        logger.Debug("[" & Master.currMovie.Movie.Title & "] No results for trailers on HD-Trailer.net! SearchURL: " & SearchURL)
                    End If
                Else
                    logger.Debug("[" & Master.currMovie.Movie.Title & "] No results for trailers on HD-Trailer.net! SearchURL: " & SearchURL)
                End If
            Else
                logger.Warn("[" & Master.currMovie.Movie.Title & "] Originaltitle which is needed for HD-Trailer.net scraping is empty - no scraping of trailers possible!")
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region 'Methods

End Class
