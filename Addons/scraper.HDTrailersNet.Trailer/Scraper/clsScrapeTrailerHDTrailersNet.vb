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

Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace HDTrailersNet

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private originaltitle As String
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
        Public Async Function Init(ByVal sOriginalTitle As String) As Threading.Tasks.Task
            originaltitle = sOriginalTitle
            _Cancelled = False
            Await GetMovieTrailers()
        End Function

        Private Sub Clear()
            _trailerlist = New List(Of Trailers)
        End Sub

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


        ''' <summary>
        ''' Scrapes avalaible trailerlinks from HD-Trailer.net
        ''' </summary>
        ''' <remarks>Try to find trailerlinks for selected movie and add results to global trailerlist
        ''' 
        ''' 2014/09/26 Cocotus - First implementation
        ''' 2014/10/04 Cocotus - Instead of using google to search for trailers (google might block webrequest due to spam!) we try to build url directly
        ''' Steps: 
        ''' 1. Build trailerlink for hd-trailer.net using originaltitle and download HTML of site! Example: http://www.hd-trailers.net/movie/transformers-age-of-extinction/
        ''' 2. If no site was downloaded, use Google to search for trailer on hd-trailer.net! Example of searchstring: "site:http://www.hd-trailers.net/ "Transformers: Age of Extinction""
        ''' 3. If URL to movie was found on hd-trailers.net, download HTML and search for trailerlinks! Example:
        ''''   <td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h480p.mov" rel="lightbox[res480p 852 480]" title="Terminator Salvation - Trailer 1 - 480p">480p</a></td>
        ''''   <td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h720p.mov" rel="lightbox[res720p 1280 720]" title="Terminator Salvation - Trailer 1 - 720p">720p</a></td>
        ''''   <td class="bottomTableResolution"><a href="http://trailers.apple.com/movies/wb/terminatorsalvation/terminatorsalvation-tlr1_h1080p.mov" rel="lightbox[res1080p 1920 1080]" title="Terminator Salvation - Trailer 1 - 1080p">1080p</a></td>
        ''''   --> we can extract URL and title
        ''' 4. Add found trailerlink to global list
        ''' </remarks>
        Private Async Function GetMovieTrailers() As Task
            Try
                Dim SearchURL As String = ""
                Dim TrailerWEBPageURL As String = ""
                If Not String.IsNullOrEmpty(originaltitle) Then
                    'Step 1: Build trailerlink for hd-trailer.net and analyze returned HTML!
                    'originaltitle may contain not supported characters which must be filtered/cleaned first!
                    Dim searchtitle As String = ""
                    searchtitle = StringUtils.RemovePunctuation(originaltitle).Replace("+", "-").Replace(" ", "-")
                    Dim sHtml As String = ""
                    sHtml = ""
                    intHTTP = New HTTP
                    sHtml = Await intHTTP.DownloadData("http://www.hd-trailers.net/movie/" & searchtitle & "/")
                    intHTTP.Dispose()
                    intHTTP = Nothing
                    If _Cancelled Then Return

                    'Step 2: If trailerlink was found -> ok, else use Google to search for trailer on hd-trailer.net
                    If sHtml <> "" Then
                        TrailerWEBPageURL = "http://www.hd-trailers.net/movie/" & searchtitle & "/"
                        logger.Debug("[" & originaltitle & "] HD-Trailer.net - Movie found on http://www.hd-trailers.net - URL: " & TrailerWEBPageURL)
                    Else
                        logger.Debug("[" & originaltitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net - URL: " & "http://www.hd-trailers.net/movie/" & searchtitle & "/")
                        'Google search
                        'Warning: will be blocked by Google when spamming webrequests!
                        Dim BaseURL As String = "http://www.google.ch/search?q="
                        Dim searchTerm As String = "site:http://www.hd-trailers.net/ " + """" + originaltitle + """"
                        searchTerm = Web.HttpUtility.UrlEncode(searchTerm)
                        SearchURL = String.Concat(BaseURL, searchTerm)
                        '--> i.e "
                        'performing google search to find avalaible links on hd-trailers.net
                        intHTTP = New HTTP
                        sHtml = Await intHTTP.DownloadData(SearchURL)
                        intHTTP.Dispose()
                        intHTTP = Nothing
                        If _Cancelled Then Return

                        If sHtml <> "" Then
                            'Now extract links from googleresults
                            Dim trailerWEBURLResults As MatchCollection = Nothing
                            Dim googleresultPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                            trailerWEBURLResults = Regex.Matches(sHtml, googleresultPattern, RegexOptions.Singleline)
                            'Go through each searchresults from google and compare found URL with originaltitle using Levenshtein algorithm
                            'check if something was found
                            If Not trailerWEBURLResults Is Nothing AndAlso trailerWEBURLResults.Count > 0 Then
                                Dim compareresult As Integer = 100
                                Dim comparestring As String = ""
                                For Each searchresult As Match In trailerWEBURLResults
                                    TrailerWEBPageURL = ""
                                    TrailerWEBPageURL = searchresult.Groups(1).Value
                                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                                        If TrailerWEBPageURL.Contains("/") Then
                                            comparestring = TrailerWEBPageURL.Remove(0, TrailerWEBPageURL.LastIndexOf("/") + 1)
                                            compareresult = 100
                                            searchtitle = (StringUtils.RemovePunctuation(originaltitle.ToLower)).Replace(" ", "")
                                            comparestring = (StringUtils.RemovePunctuation(comparestring.ToLower)).Replace(" ", "")
                                            compareresult = StringUtils.ComputeLevenshtein(comparestring, searchtitle)
                                            If (originaltitle.Length <= 5 AndAlso compareresult = 0) OrElse (originaltitle.Length > 5 AndAlso compareresult <= 2) Then
                                                logger.Debug("[" & originaltitle & "] HD-Trailer.net - Google: Movie found! URL: " & TrailerWEBPageURL)
                                                Exit For
                                            Else
                                                logger.Debug("[" & originaltitle & "] HD-Trailer.net - Google: wrong result! URL: " & TrailerWEBPageURL)
                                                TrailerWEBPageURL = ""
                                            End If
                                        End If
                                    End If
                                Next
                            Else
                                logger.Debug("[" & originaltitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net using google! URL: " & SearchURL)
                            End If
                        Else
                            logger.Debug("[" & originaltitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net using google! URL: " & SearchURL)
                        End If
                    End If

                    'Step 3: If URL to movie was found on hd-trailers.net, download HTML of moviepage and search for trailerlinks
                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                        logger.Info("[" & originaltitle & "] HD-Trailer.net - Movie found! Download URL: " & TrailerWEBPageURL)
                        'find avalaible links on hd-trailers.net movie site
                        intHTTP = New HTTP
                        sHtml = Await intHTTP.DownloadData(TrailerWEBPageURL)
                        intHTTP.Dispose()
                        intHTTP = Nothing
                        If _Cancelled Then Return
                        'looking for trailerlinks
                        Dim trailerlinkPattern As String = "<a href=""(?<URL>.*?)"".*?title=""(?<TITLE>.*?)"""
                        Dim trailerURLResults As MatchCollection = Regex.Matches(sHtml, trailerlinkPattern, RegexOptions.Singleline)
                        'check if at least one trailer was found
                        If Not trailerURLResults Is Nothing AndAlso trailerURLResults.Count > 0 Then
                            'go through each result and check if it's a valid trailerlink...
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
                                        ' Step 4: Add scraped trailer to global list
                                        _trailerlist.Add(trailer)
                                    End If
                                End If
                            Next
                        Else
                            logger.Info("[" & originaltitle & "] HD-Trailer.net - NO trailers found on specific movie page! URL: " & TrailerWEBPageURL)
                        End If
                    Else
                        logger.Info("[" & originaltitle & "] HD-Trailer.net - Movie NOT found")
                    End If
                Else
                    logger.Warn("[" & originaltitle & "] HD-Trailer.net - Original title is empty, no scraping of trailers possible!")
                End If

                'log scraperesults for user
                If _trailerlist.Count < 1 Then
                    logger.Info("[" & originaltitle & "] HD-Trailer.net - NO trailer scraped!")
                Else
                    logger.Info("[" & originaltitle & "] HD-Trailer.net - Scraped " & _trailerlist.Count & " trailer!")
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Function

#End Region 'Methods

    End Class

End Namespace
