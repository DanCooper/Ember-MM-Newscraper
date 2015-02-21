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

Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Namespace MoviepilotDE

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"
        ''' <summary>
        ''' Scrapes FSK rating for one given movie from MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="HTML"><c>String</c> which contains downloaded HTMLcode of moviesite</param>  
        ''' <returns><c>String</c> that contains FSK number (or empty string if nothing was found)</returns>
        ''' <remarks>This one is used for scraping the FSK rating from Moviepilot.de - Moviepilot is a great source for that</remarks>
        Private Function GetCertification(ByVal HTML As String) As String
            Dim FSK As String = String.Empty

            Try
                If Not String.IsNullOrEmpty(HTML) Then
                    Dim strFSKPattern As String = "FSK (?<FSK>\d?\d)"
                    FSK = Web.HttpUtility.HtmlDecode(Regex.Match(HTML, strFSKPattern, RegexOptions.Singleline).Groups(1).Value).Trim

                    If Not IsNumeric(FSK) Then
                        FSK = String.Empty
                    Else
                        FSK = String.Concat("Germany:", FSK)
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return FSK
        End Function

        Public Function GetMovieInfo(ByVal strOriginalTitle As String, ByVal strTitle As String, ByVal strYear As String, ByRef nMovie As MediaContainers.Movie, ByVal Options As Structures.ScrapeOptions_Movie) As Boolean
            Try
                nMovie.Clear()
                nMovie.Scrapersource = "MOVIEPILOT"

                Dim sURL As String = SearchMovie(strOriginalTitle, strYear)
                'if theres no link with originaltitle, try with title
                If String.IsNullOrEmpty(sURL) AndAlso Not strOriginalTitle = strTitle Then
                    sURL = SearchMovie(strTitle, strYear)
                End If

                If Not String.IsNullOrEmpty(sURL) Then
                    'Now download HTML-Code
                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(sURL)
                    sHTTP = Nothing

                    '....and use result to get the wanted information
                    If Not String.IsNullOrEmpty(HTML) Then
                        If Options.bCert Then
                            nMovie.Certifications.Add(GetCertification(HTML))
                        End If

                        If Options.bOutline OrElse Options.bPlot Then
                            Dim aResult As Results = GetPlotAndOutline(HTML)
                            If Options.bOutline Then
                                nMovie.Outline = aResult.strOutline
                            End If
                            If Options.bPlot Then
                                nMovie.Plot = aResult.strPlot
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Function
        ''' <summary>
        ''' Scrapes either plot or outline for one given movie from MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="HTML"><c>String</c> which contains downloaded HTMLcode of moviesite</param>
        ''' <param name="Switch"><c>Integer</c> used as switch to scrape/return either Plot (=0) or Outline(=1)</param>
        ''' <returns><c>String</c> that contains either Plot or Outline (or empty string if nothing was found)</returns>
        ''' <remarks></remarks>
        Private Function GetPlotAndOutline(ByVal HTML As String) As Results
            Dim aResults As Results
            Dim strPlot As String = String.Empty
            Dim strOutline As String = String.Empty

            Try
                If Not String.IsNullOrEmpty(HTML) Then
                    Dim strDescription As String = String.Empty

                    'Get the entire description
                    Dim descPattern As String = "<div class='expander' itemprop='description'>(?<DESCRIPTION>.*?)<\/div>"
                    Dim descResult As MatchCollection = Regex.Matches(HTML, descPattern, RegexOptions.Singleline)

                    If descResult.Count > 0 Then
                        strDescription = descResult.Item(0).Groups(1).Value.Trim
                    End If

                    If Not String.IsNullOrEmpty(strDescription) Then
                        'vPattern if website has Outline, heading title like "Handlung von..." and Plot
                        'like http://www.moviepilot.de/movies/james-bond-casino-royale
                        Dim vPattern As String = "<p><strong>(?<OUTLINE>.*?)<\/strong><\/p>.<p><strong>(?<HEADER>.*?)<\/strong><br \/>.(?<PLOT>.*?)<p><strong>"
                        Dim vResult As MatchCollection = Regex.Matches(strDescription, vPattern, RegexOptions.Singleline)

                        If vResult.Count > 0 Then
                            strOutline = Web.HttpUtility.HtmlDecode(vResult.Item(0).Groups(1).Value)
                            strPlot = Web.HttpUtility.HtmlDecode(vResult.Item(0).Groups(3).Value)
                        Else
                            'mPattern if website has Outline and Plot
                            'like http://www.moviepilot.de/movies/hellboy-ii-die-goldene-armee
                            Dim mPattern As String = "<p><strong>(?<OUTLINE>.*?)<\/strong><\/p>.<p>(?<PLOT>.*?)<\/p>"
                            Dim mResult As MatchCollection = Regex.Matches(strDescription, mPattern, RegexOptions.Singleline)

                            If mResult.Count > 0 Then
                                strOutline = Web.HttpUtility.HtmlDecode(mResult.Item(0).Groups(1).Value)
                                strPlot = Web.HttpUtility.HtmlDecode(mResult.Item(0).Groups(2).Value)
                            Else
                                'sPattern if website has only Plot
                                'like http://www.moviepilot.de/movies/mission-impossible
                                Dim sPattern As String = "<p>(?<PLOT>.*?)<\/p>"
                                Dim sResult As MatchCollection = Regex.Matches(strDescription, sPattern, RegexOptions.Singleline)

                                If sResult.Count > 0 Then
                                    strPlot = Web.HttpUtility.HtmlDecode(sResult.Item(0).Groups(1).Value)
                                End If
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            aResults.strOutline = StringUtils.CleanPlotOutline(strOutline)
            aResults.strPlot = StringUtils.CleanPlotOutline(strPlot)

            Return aResults
        End Function
        ''' <summary>
        ''' Retrieve the URL for a specific movie on MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="strTitle"><c>String</c> which contains the originaltitle or title of the movie</param>
        ''' <param name="strYear"><c>String</c> which contains the movie year</param>
        ''' <returns><c>String</c> which is URL of the movie on Moviepilot.de (or empty string if nothing was found)</returns>
        ''' <remarks>Retrieve the URL on Moviepilot.de here and use it to download the HTML source later on!</remarks>
        Private Function SearchMovie(ByVal strTitle As String, ByVal strYear As String) As String
            Dim strURL As String = String.Empty
            Try
                If Not String.IsNullOrEmpty(strTitle) Then

                    'search movie on website
                    'like http://www.moviepilot.de/suche?q=Machete+Kills&type=movie
                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.moviepilot.de/suche?q=", strTitle, "&type=movie&sourceid=mozilla-search"))
                    sHTTP = Nothing

                    If Not String.IsNullOrEmpty(HTML) Then
                        Dim strSearchResults As String = String.Empty

                        'reduce HTML to search results only
                        Dim filterPattern As String = "<\/h2>.<ul>(?<RESULTS>.*?)<\/ul>"
                        Dim filterResult As MatchCollection = Regex.Matches(HTML, filterPattern, RegexOptions.Singleline)

                        If filterResult.Count = 1 Then
                            strSearchResults = filterResult.Item(0).Groups(1).Value
                        End If

                        If Not String.IsNullOrEmpty(strSearchResults) Then
                            'get search results


                            Dim resPattern As String = "<div class='trackable' data-track-position=.*?<\/span>.<a href=""(?<URL>.*?)"".*?>(?<TITLE>.*?)<\/a>.*?(?<YEAR>\d{4}).*?<\/li>"
                            Dim resResult As MatchCollection = Regex.Matches(strSearchResults, resPattern, RegexOptions.Singleline)

                            If resResult.Count = 0 Then
                                resPattern = "www.moviepilot.de/movies/(?<URL>.*?)"""
                                resResult = Regex.Matches(strSearchResults, resPattern, RegexOptions.Singleline)
                                strURL = String.Concat("http://www.moviepilot.de/movies/", resResult.Item(0).Groups(1).Value).Trim
                            End If

                            'Only one search result or no Year to filter
                            If resResult.Count = 1 OrElse (filterResult.Count > 0 AndAlso String.IsNullOrEmpty(strYear)) Then
                                If strURL = String.Empty Then
                                    strURL = String.Concat("http://www.moviepilot.de", resResult.Item(0).Groups(1).Value).Trim
                                End If
                            ElseIf resResult.Count > 0 Then
                                ' Try to find a search result with same Year
                                For ctr As Integer = 0 To resResult.Count - 1
                                    If resResult.Item(ctr).Groups(3).Value = strYear Then
                                        strURL = String.Concat("http://www.moviepilot.de", resResult.Item(ctr).Groups(1).Value).Trim
                                        Return strURL
                                    End If
                                Next
                                'no match found -> use first search result
                                strURL = String.Concat("http://www.moviepilot.de", resResult.Item(0).Groups(1).Value).Trim
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return strURL
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Results

#Region "Fields"

            Dim strOutline As String
            Dim strPlot As String

#End Region 'Fields
        End Structure

#End Region 'Nested Types

    End Class

End Namespace