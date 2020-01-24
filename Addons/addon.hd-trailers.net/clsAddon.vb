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

Imports EmberAPI
Imports NLog
Imports System.Text.RegularExpressions

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields  

#Region "Methods"
    ''' <summary>
    ''' Scrapes available trailerlinks from HD-Trailer.net
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
    '  ''' <td class="bottomTableResolution"><a href="http://videos.hd-trailers.net/scarface-bluray-trailer-480p-HDTN.mov" rel="lightbox[res480p 852 480]" title="Scarface - Blu-ray Trailer - 480p">480p</a></td>
    '<td class="bottomTableResolution"><a href="http://videos.hd-trailers.net/scarface-bluray-trailer-720p-HDTN.mov" rel="lightbox[res720p 1280 720]" title="Scarface - Blu-ray Trailer - 720p">720p</a></td>
    '<td class="bottomTableResolution"><a href="http://videos.hd-trailers.net/scarface-bluray-trailer-1080p-HDTN.mov" rel="lightbox[res1080p 1920 1080]" title="Scarface - Blu-ray Trailer - 1080p">1080p</a></td>
    ''' </remarks>
    Public Shared Function GetMovieTrailers(ByVal OriginalTitle As String, ByVal Title As String) As List(Of MediaContainers.Trailer)
        Dim nTrailerList As New List(Of MediaContainers.Trailer)
        Try
            Dim SearchURL As String = String.Empty
            Dim TrailerWEBPageURL As String = String.Empty
            If Not String.IsNullOrEmpty(OriginalTitle) Then
                'Step 1: Build trailerlink for hd-trailer.net and analyze returned HTML!
                'originaltitle may contain not supported characters which must be filtered/cleaned first!
                Dim searchtitle As String = String.Empty
                searchtitle = StringUtils.RemovePunctuation(OriginalTitle).Replace("+", "-").Replace(" ", "-")
                Dim sHTTP As New HTTP
                Dim sHtml As String = String.Empty
                sHtml = sHTTP.DownloadData("http://www.hd-trailers.net/movie/" & searchtitle & "/")
                sHTTP = Nothing
                'Step 2: If trailerlink was found -> ok, else use Google to search for trailer on hd-trailer.net
                If Not String.IsNullOrEmpty(sHtml) Then
                    TrailerWEBPageURL = "http://www.hd-trailers.net/movie/" & StringUtils.RemovePunctuation(Title).Replace("+", "-").Replace(" ", "-") & "/"
                    _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Movie found on http://www.hd-trailers.net - URL: " & TrailerWEBPageURL)
                Else
                    'try with title instead of originaltitle!
                    sHTTP = New HTTP
                    sHtml = sHTTP.DownloadData("http://www.hd-trailers.net/movie/" & StringUtils.RemovePunctuation(Title).Replace("+", "-").Replace(" ", "-") & "/")
                    sHTTP = Nothing
                    If Not String.IsNullOrEmpty(sHtml) Then
                        TrailerWEBPageURL = "http://www.hd-trailers.net/movie/" & StringUtils.RemovePunctuation(Title).Replace("+", "-").Replace(" ", "-") & "/"
                        _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Movie found on http://www.hd-trailers.net - URL: " & TrailerWEBPageURL)
                    Else
                        _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net - URL: " & "http://www.hd-trailers.net/movie/" & searchtitle & "/")
                        'Google search
                        'Warning: will be blocked by Google when spamming webrequests!
                        Dim BaseURL As String = "http://www.google.ch/search?q="
                        Dim searchTerm As String = "site:http://www.hd-trailers.net/ " + """" + OriginalTitle + """"
                        searchTerm = HttpUtility.UrlEncode(searchTerm)
                        SearchURL = String.Concat(BaseURL, searchTerm)
                        '--> i.e "
                        'performing google search to find available links on hd-trailers.net
                        sHTTP = New HTTP
                        sHtml = sHTTP.DownloadData(SearchURL)
                        sHTTP = Nothing
                        If Not String.IsNullOrEmpty(sHtml) Then
                            'Now extract links from googleresults
                            Dim trailerWEBURLResults As MatchCollection = Nothing
                            Dim googleresultPattern As String = "<a href=""/url\?q=(?<RESULTS>.*?)\/&amp;" 'Google search results
                            trailerWEBURLResults = Regex.Matches(sHtml, googleresultPattern, RegexOptions.Singleline)
                            'Go through each searchresults from google and compare found URL with originaltitle using Levenshtein algorithm
                            'check if something was found
                            If Not trailerWEBURLResults Is Nothing AndAlso trailerWEBURLResults.Count > 0 Then
                                Dim compareresult As Integer = 100
                                Dim comparestring As String = String.Empty
                                For Each searchresult As Match In trailerWEBURLResults
                                    TrailerWEBPageURL = String.Empty
                                    TrailerWEBPageURL = searchresult.Groups(1).Value
                                    If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                                        If TrailerWEBPageURL.Contains("/") Then
                                            comparestring = TrailerWEBPageURL.Remove(0, TrailerWEBPageURL.LastIndexOf("/") + 1)
                                            compareresult = 100
                                            searchtitle = (StringUtils.RemovePunctuation(OriginalTitle.ToLower)).Replace(" ", String.Empty)
                                            comparestring = (StringUtils.RemovePunctuation(comparestring.ToLower)).Replace(" ", String.Empty)
                                            compareresult = StringUtils.ComputeLevenshtein(comparestring, searchtitle)
                                            If (OriginalTitle.Length <= 5 AndAlso compareresult = 0) OrElse (OriginalTitle.Length > 5 AndAlso compareresult <= 2) Then
                                                _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Google: Movie found! URL: " & TrailerWEBPageURL)
                                                Exit For
                                            Else
                                                _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Google: wrong result! URL: " & TrailerWEBPageURL)
                                                TrailerWEBPageURL = ""
                                            End If
                                            compareresult = StringUtils.ComputeLevenshtein(comparestring, (StringUtils.RemovePunctuation(Title.ToLower)).Replace(" ", ""))
                                            If (OriginalTitle.Length <= 5 AndAlso compareresult = 0) OrElse (OriginalTitle.Length > 5 AndAlso compareresult <= 2) Then
                                                _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Google: Movie found! URL: " & TrailerWEBPageURL)
                                                Exit For
                                            Else
                                                _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Google: wrong result! URL: " & TrailerWEBPageURL)
                                                TrailerWEBPageURL = ""
                                            End If
                                        End If
                                    End If
                                Next
                            Else
                                _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net using google! URL: " & SearchURL)
                            End If
                        Else
                            _Logger.Debug("[" & OriginalTitle & "] HD-Trailer.net - Movie NOT found on http://www.hd-trailers.net using google! URL: " & SearchURL)
                        End If
                    End If
                End If

                'Step 3: If URL to movie was found on hd-trailers.net, download HTML of moviepage and search for trailerlinks
                If Not String.IsNullOrEmpty(TrailerWEBPageURL) Then
                    _Logger.Info("[" & OriginalTitle & "] HD-Trailer.net - Movie found! Download URL: " & TrailerWEBPageURL)
                    'find available links on hd-trailers.net movie site
                    sHTTP = New HTTP
                    sHtml = sHTTP.DownloadData(TrailerWEBPageURL)
                    sHTTP = Nothing
                    'looking for trailerlinks
                    Dim trailerlinkPattern As String = "<a href=""(?<URL>.*?)"".*?title=""(?<TITLE>.*?)"""
                    Dim trailerURLResults As MatchCollection = Regex.Matches(sHtml, trailerlinkPattern, RegexOptions.Singleline)
                    'check if at least one trailer was found
                    If Not trailerURLResults Is Nothing AndAlso trailerURLResults.Count > 0 Then
                        'go through each result and check if it's a valid trailerlink...
                        For Each item As Match In trailerURLResults
                            If item.Groups.Count > 0 Then
                                'only accept mov and mp4 files for now
                                If Not item.Groups(1).Value Is Nothing AndAlso (item.Groups(1).Value.EndsWith(".mov") OrElse item.Groups(1).Value.EndsWith(".mp4")) Then
                                    Dim trailer As New MediaContainers.Trailer
                                    'trailer description
                                    If Not item.Groups(2).Value Is Nothing Then
                                        trailer.Title = item.Groups(2).Value
                                    End If
                                    'trailer URLs
                                    trailer.URLVideoStream = item.Groups(1).Value
                                    trailer.URLWebsite = item.Groups(1).Value
                                    'trailer extension
                                    trailer.TrailerOriginal.Extention = IO.Path.GetExtension(trailer.URLVideoStream)
                                    'trailer source
                                    trailer.Source = "HD-Trailers.net"
                                    '..and most important: trailer quality
                                    If item.Groups(1).Value.Contains("1080p") Then
                                        '1080p
                                        trailer.Quality = Enums.TrailerVideoQuality.HD1080p
                                    ElseIf item.Groups(1).Value.Contains("720p") Then
                                        '720p
                                        trailer.Quality = Enums.TrailerVideoQuality.HD720p
                                    Else
                                        'other                     
                                        trailer.Quality = Enums.TrailerVideoQuality.HQ480p
                                    End If
                                    ' Step 4: Add scraped trailer to global list
                                    nTrailerList.Add(trailer)
                                End If
                            End If
                        Next
                    Else
                        _Logger.Info("[" & OriginalTitle & "] HD-Trailer.net - NO trailers found on specific movie page! URL: " & TrailerWEBPageURL)
                    End If
                Else
                    _Logger.Info("[" & OriginalTitle & "] HD-Trailer.net - Movie NOT found")
                End If
            Else
                _Logger.Warn("[" & OriginalTitle & "] HD-Trailer.net - Original title is empty, no scraping of trailers possible!")
            End If

            'log scraperesults for user
            If nTrailerList.Count < 1 Then
                _Logger.Info("[" & OriginalTitle & "] HD-Trailer.net - NO trailer scraped!")
            Else
                _Logger.Info("[" & OriginalTitle & "] HD-Trailer.net - Scraped " & nTrailerList.Count & " trailer!")
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return nTrailerList
    End Function

#End Region 'Methods

End Class