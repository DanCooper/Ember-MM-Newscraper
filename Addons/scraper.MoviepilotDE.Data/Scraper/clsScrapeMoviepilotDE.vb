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
        Private originaltitle As String
        Private _fsk As String
        Private _outline As String
        Private _plot As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New(ByVal soriginaltitle As String)
            Clear()
            originaltitle = soriginaltitle
            'Main method in this class to retrieve Moviepilot information...
            GetMoviepilotDEDetails()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property FSK() As String
            Get
                Return _fsk
            End Get
            Set(ByVal value As String)
                _fsk = value
            End Set
        End Property

        Public Property Outline() As String
            Get
                Return _outline
            End Get
            Set(ByVal value As String)
                _outline = value
            End Set
        End Property

        Public Property Plot() As String
            Get
                Return _plot
            End Get
            Set(ByVal value As String)
                _plot = value
            End Set
        End Property


#End Region 'Properties

#Region "Methods"

        Private Sub Clear()
            _fsk = String.Empty
            _outline = String.Empty
            _plot = String.Empty
        End Sub

        ''' <summary>
        ''' Scrape MovieDetails from Moviepilot.de (German site)
        ''' </summary> 
        ''' <remarks>Main method to retrieve Moviepilot information - from here all other class methods gets called
        ''' 
        ''' 2013/12/21 Cocotus - First implementation
        ''' </remarks>
        Private Async Function GetMoviepilotDEDetails() As Threading.Tasks.Task

            'First step is to retrieve URL for the movie on Moviepilot.de
            Dim sURL As String = Await GetMoviePilotUrlFromOriginaltitle(originaltitle)

            Try
                If Not String.IsNullOrEmpty(sURL) Then
                    'Now download HTML-Code
                    Dim sHTTP As New HTTP
                    Dim HTML As String = Await sHTTP.DownloadData(sURL)
                    sHTTP = Nothing

                    '....and use result to get the wanted information
                    If Not String.IsNullOrEmpty(HTML) Then

                        'outline
                        _outline = GetPlotAndOutline(HTML, 1)


                        'full plot         
                        _plot = GetPlotAndOutline(HTML, 0)


                        'fsk       
                        _fsk = GetFSK(HTML)


                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Function


        ''' <summary>
        ''' Retrieve the URL for a specific movie on MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="originaltitle"><c>String</c> which contains the originaltitle of the movie</param>
        ''' <returns><c>String</c> which is URL of the movie on Moviepilot.de (or empty string if nothing was found)</returns>
        ''' <remarks>Retrieve the URL on Moviepilot.de here and use it to download the HTML source later on!
        ''' 
        ''' 2013/12/21 Cocotus - First implementation
        ''' </remarks>
        Private Async Function GetMoviePilotUrlFromOriginaltitle(ByVal originaltitle As String) As Threading.Tasks.Task(Of String)
            Dim MoviePilotURL As String = String.Empty
            Try

                Dim sHTTP As New HTTP
                Dim HTML As String = Await sHTTP.DownloadData(String.Concat("http://www.moviepilot.de/suche?q=", originaltitle, "&type=movie&sourceid=mozilla-search"))
                sHTTP = Nothing
                'Example:
                '    http://www.moviepilot.de/suche?q=Machete+Kills&type=movie
                If Not String.IsNullOrEmpty(HTML) Then
                    'Result is the search result page - now we need to get the correct URL!
                    'Example from HTML-SearchPage: <a href="/movies/machete-kills" class="h3">
                    Dim mcMoviePilotURL As MatchCollection = Regex.Matches(HTML, "<a href=""/movies/([^<]+)"" class=")
                    If mcMoviePilotURL.Count > 0 Then
                        'just use the first one if more are found
                        '  MoviePilotURL = String.Concat("http://www.moviepilot.de/", Regex.Match(mcMoviePilotURL(0).Value.ToString, """(movies/([^<]+))""").Groups(1).Value.ToString)
                        MoviePilotURL = String.Concat("http://www.moviepilot.de/movies/", mcMoviePilotURL(0).Value.ToString.Substring(17, mcMoviePilotURL(0).Value.ToString.IndexOf(" class") - 18))

                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return MoviePilotURL
        End Function


        ''' <summary>
        ''' Scrapes either plot or outline for one given movie from MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="HTML"><c>String</c> which contains downloaded HTMLcode of moviesite</param>
        ''' <param name="Switch"><c>Integer</c> used as switch to scrape/return either Plot (=0) or Outline(=1)</param>
        ''' <returns><c>String</c> that contains either Plot or Outline (or empty string if nothing was found)</returns>
        ''' <remarks>One method to scrape either plot or outline
        ''' 
        ''' 2013/12/21 Cocotus - First implementation
        ''' </remarks>
        Private Function GetPlotAndOutline(ByVal HTML As String, ByVal Switch As Integer) As String
            Dim strPlot As String = ""
            Dim strOutline As String = ""

            Try
                If Not String.IsNullOrEmpty(HTML) Then
                    Dim tempD As Integer
                    Dim tmpHTML As String
                    Dim Outline, Plot, B, dirt As Integer

                    tempD = If(HTML.IndexOf("<div class='expander' itemprop='description'>") > 0, HTML.IndexOf("<div class='expander' itemprop='description'>"), 0)
                    'Check if site contains any movie descriptions
                    If tempD > 0 Then
                        Dim L As Integer = HTML.Length
                        tmpHTML = HTML.Substring(tempD + 45, L - (tempD + 45)).Trim
                        Outline = tmpHTML.IndexOf("<p><strong>")

                        'check if outline exists
                        If Outline = 0 Then
                            B = tmpHTML.IndexOf("</strong>", Outline + 11)

                            If B > 11 Then

                                'Return Outline if Parameter is set
                                If Switch = 1 Then
                                    strOutline = Web.HttpUtility.HtmlDecode(tmpHTML.Substring(Outline + 11, B - (Outline + 11)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)
                                    Return strOutline
                                End If

                                L = tmpHTML.Length
                                tmpHTML = tmpHTML.Substring(tmpHTML.IndexOf("</strong>") + 9, L - (tmpHTML.IndexOf("</strong>") + 9)).Trim
                                Plot = tmpHTML.IndexOf("<p>")

                                'check if plot exists
                                If Plot > 0 AndAlso Plot < 7 Then
                                    'check if plot contains any headers and strip them if found
                                    dirt = If(tmpHTML.IndexOf("<strong>") > 0, tmpHTML.IndexOf("<strong>"), 0)
                                    If dirt > 0 AndAlso dirt < 100 Then
                                        tmpHTML = tmpHTML.Substring(dirt + 8, tmpHTML.IndexOf("</strong>", dirt + 8) - (dirt + 8))
                                    End If
                                    Plot = tmpHTML.IndexOf("<p>")
                                    B = tmpHTML.IndexOf("</p>", Plot + 3)
                                    strPlot = Web.HttpUtility.HtmlDecode(tmpHTML.Substring(Plot + 3, B - (Plot + 3)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)

                                End If

                            End If


                            'no outline 
                        Else
                            Plot = tmpHTML.IndexOf("<p>")

                            'check if plot exists
                            If Plot > 0 AndAlso Plot < 7 Then
                                'check if plot contains any headers and strip them if found
                                dirt = If(tmpHTML.IndexOf("<strong>") > 0, tmpHTML.IndexOf("<strong>"), 0)
                                If dirt > 0 Then
                                    tmpHTML = tmpHTML.Substring(dirt + 8, tmpHTML.IndexOf("</strong>", dirt + 8) - (dirt + 8))
                                End If

                                Plot = tmpHTML.IndexOf("<p>")
                                B = tmpHTML.IndexOf("</p>", Plot + 3)
                                strPlot = Web.HttpUtility.HtmlDecode(tmpHTML.Substring(Plot + 3, B - (Plot + 3)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)

                            End If
                        End If


                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return strPlot
        End Function


        ''' <summary>
        ''' Scrapes FSK rating for one given movie from MoviePilot.de (German site)
        ''' </summary>
        ''' <param name="HTML"><c>String</c> which contains downloaded HTMLcode of moviesite</param>  
        ''' <returns><c>String</c> that contains FSK number (or empty string if nothing was found)</returns>
        ''' <remarks>This one is used for scraping the FSK rating from Moviepilot.de - Moviepilot is a great source for that
        ''' 
        ''' 2013/12/21 Cocotus - First implementation
        ''' </remarks>
        Private Function GetFSK(ByVal HTML As String) As String
            Dim FSK As String = ""
            Try
                If Not String.IsNullOrEmpty(HTML) Then
                    Dim tempD As Integer

                    tempD = If(HTML.IndexOf(" FSK ") > 0, HTML.IndexOf(" FSK "), 0)
                    If tempD > 0 Then
                        FSK = Web.HttpUtility.HtmlDecode((HTML.Substring(tempD + 5, tempD + 2 - tempD))).Replace(",", "")
                        If IsNumeric(FSK) = False Then
                            FSK = ""
                        End If
                    End If
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
            Return FSK
        End Function

#End Region 'Methods

    End Class

End Namespace