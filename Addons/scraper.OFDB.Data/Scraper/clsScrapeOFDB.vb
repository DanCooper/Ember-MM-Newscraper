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

Namespace OFDB

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Private Function CleanTitle(ByVal sString As String) As String
            Dim CleanString As String = sString

            Try
                If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

                If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)

                If sString.EndsWith(", Der") Then
                    CleanString = String.Concat("Der ", sString.Replace(", Der", " ")).Trim
                ElseIf sString.EndsWith(", Die") Then
                    CleanString = String.Concat("Die ", sString.Replace(", Die", " ")).Trim
                ElseIf sString.EndsWith(", Das") Then
                    CleanString = String.Concat("Das ", sString.Replace(", Das", " ")).Trim
                ElseIf sString.EndsWith(", The") Then
                    CleanString = String.Concat("The ", sString.Replace(", The", " ")).Trim
                ElseIf sString.EndsWith(", Ein") Then
                    CleanString = String.Concat("Ein ", sString.Replace(", Ein", " ")).Trim
                ElseIf sString.EndsWith(", Eine") Then
                    CleanString = String.Concat("Eine ", sString.Replace(", Eine", " ")).Trim
                Else
                    CleanString = sString
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            Return CleanString
        End Function
        ''' <summary>
        ''' Scrapes FSK rating for one given movie from ODFB
        ''' </summary>
        ''' <param name="HTML"><c>String</c> which contains downloaded HTMLcode of moviesite</param>  
        ''' <returns><c>String</c> that contains FSK number (or empty string if nothing was found)</returns>
        ''' <remarks>This one is used for scraping the FSK rating from ODFB
        ''' 
        ''' 2014/06/23 Cocotus - First implementation
        ''' </remarks>
        Private Function GetCertification(ByVal HTML As String) As String
            Dim FSK As String = String.Empty
            Try
                If Not String.IsNullOrEmpty(HTML) Then
                    Dim strFSKPattern As String = "Freigabe: FSK (?<FSK>.*?)"""
                    FSK = HttpUtility.HtmlDecode(Regex.Match(HTML, strFSKPattern, RegexOptions.Singleline).Groups(1).Value).Trim

                    If FSK.Contains("18") Then
                        FSK = "18"
                    ElseIf FSK.Contains("o.A") Then
                        FSK = "0"
                    End If
                    If Not Integer.TryParse(FSK, 0) Then
                        FSK = String.Empty
                    End If

                    If Not String.IsNullOrEmpty(FSK) Then
                        FSK = String.Concat("Germany:", FSK)
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return FSK
        End Function

        Private Function GetFullPlot(ByVal sURL As String) As String
            Dim FullPlot As String = String.Empty

            Try
                If Not String.IsNullOrEmpty(sURL) Then
                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(sURL, Text.Encoding.UTF8)
                    sHTTP = Nothing

                    If Not String.IsNullOrEmpty(HTML) Then
                        Dim strPlot As String = String.Empty
                        Dim strPlotPattern = "Eine Inhaltsangabe.*?<br><br>(?<PLOT>.*?)<\/font>"
                        strPlot = HttpUtility.HtmlDecode(Regex.Match(HTML, strPlotPattern, RegexOptions.Singleline).Groups(1).Value).Trim
                        FullPlot = StringUtils.CleanPlotOutline(strPlot)
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return FullPlot
        End Function

        Public Function GetMovieInfo(ByVal strIMDBID As String, ByVal FilteredOptions As Structures.ScrapeOptions, ByVal strLanguage As String) As MediaContainers.Movie
            Try
                Dim bIsScraperLanguage As Boolean = strLanguage.ToLower.StartsWith("de")

                Dim nMovie As New MediaContainers.Movie
                nMovie.Scrapersource = "OFDB"

                Dim sURL As String = SearchMovie(strIMDBID)

                If Not String.IsNullOrEmpty(sURL) Then
                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(sURL, Text.Encoding.UTF8)
                    sHTTP = Nothing

                    If Not String.IsNullOrEmpty(HTML) Then

                        'Certification
                        If FilteredOptions.bMainCertifications Then
                            nMovie.Certifications.Add(GetCertification(HTML))
                        End If

                        'Genres
                        If FilteredOptions.bMainGenres Then
                            Dim strGenrePattern As String = "itemprop=""genre"">(?<GENRE>.*?)<\/span>"
                            Dim gResult As MatchCollection = Regex.Matches(HTML, strGenrePattern, RegexOptions.Singleline)
                            For ctr As Integer = 0 To gResult.Count - 1
                                nMovie.Genres.Add(HttpUtility.HtmlDecode(gResult.Item(ctr).Groups(1).Value.Trim))
                            Next
                        End If

                        'Original Title
                        If FilteredOptions.bMainOriginalTitle Then
                            Dim strOriginalTitlePattern As String = "Originaltitel:.*?<b>(?<OTITLE>.*?)<\/b>"
                            nMovie.OriginalTitle = CleanTitle(HttpUtility.HtmlDecode(Regex.Match(HTML, strOriginalTitlePattern, RegexOptions.Singleline).Groups(1).Value.ToString.Trim))
                        End If

                        'Plot
                        If FilteredOptions.bMainPlot AndAlso bIsScraperLanguage Then
                            Dim strPlotPattern As String = "<a href=""(?<URL>plot.*?)"""
                            nMovie.Plot = GetFullPlot(String.Concat("http://www.ofdb.de/", Regex.Match(HTML, strPlotPattern, RegexOptions.Singleline).Groups(1).Value))
                        End If

                        'Title
                        If FilteredOptions.bMainTitle AndAlso bIsScraperLanguage Then
                            Dim strTitlePattern As String = "<td width=""99\%""><h1 itemprop=""name""><font face=""Arial,Helvetica,sans-serif"" size=""3""><b>([^<]+)</b></font></h1></td>"
                            nMovie.Title = CleanTitle(HttpUtility.HtmlDecode(Regex.Match(HTML, strTitlePattern, RegexOptions.Singleline).Groups(1).Value.ToString.Trim))
                        End If
                    End If
                End If

                Return nMovie
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                Return Nothing
            End Try
        End Function

        Private Function SearchMovie(ByVal strIMDBID As String) As String
            Dim strURL As String = String.Empty
            Try
                If Not String.IsNullOrEmpty(strIMDBID) Then
                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.ofdb.de/view.php?SText=", strIMDBID, "&Kat=IMDb&page=suchergebnis&sourceid=mozilla-search"), Text.Encoding.UTF8)
                    sHTTP = Nothing

                    If Not String.IsNullOrEmpty(HTML) Then
                        Dim resPattern As String = "<a href=""(?<URL>film.*?)"""
                        Dim resResult As MatchCollection = Regex.Matches(HTML, resPattern, RegexOptions.Singleline)

                        'use first search result
                        If resResult.Count > 0 Then
                            strURL = String.Concat("http://www.ofdb.de/", resResult.Item(0).Groups(1).Value).Trim
                        End If
                    Else
                        logger.Warn("OFDB Query returned no results for ID of <{0}>", strIMDBID)
                    End If
                End If
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Keys.Tab) & "Error scraping ODFB (too many connections?):" & strIMDBID)
            End Try

            Return strURL
        End Function

#End Region 'Methods

    End Class

End Namespace