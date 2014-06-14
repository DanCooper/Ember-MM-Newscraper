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

Imports System.IO
Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class OFDB

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private imdbID As String
    Private OFDBMovie As MediaContainers.Movie
    Private _genre As String
    Private _outline As String
    Private _plot As String
    Private _title As String

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal sID As String, ByRef mMovie As MediaContainers.Movie)
        Clear()
        imdbID = sID
        OFDBMovie = mMovie

        GetOFDBDetails()
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property Genre() As String
        Get
            Return _genre
        End Get
        Set(ByVal value As String)
            _genre = value
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

    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Function CleanTitle(ByVal sString As String) As String
        Dim CleanString As String = sString

        Try
            If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

            If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
        Return CleanString
    End Function

    Private Sub Clear()
        _title = String.Empty
        _outline = String.Empty
        _plot = String.Empty
        _genre = String.Empty
    End Sub

    Private Function GetFullPlot(ByVal sURL As String) As String
        Dim FullPlot As String = String.Empty

        Try
            If Not String.IsNullOrEmpty(sURL) Then
                Dim sHTTP As New HTTP
                Dim HTML As String = sHTTP.DownloadData(sURL)
                sHTTP = Nothing

                Dim D, W, Wq, Wqq, B As Integer
                Dim tmpHTML As String

                D = HTML.IndexOf("Eine Inhaltsangabe von")
                If D > 0 Then
                    Dim L As Integer = HTML.Length
                    tmpHTML = HTML.Substring(D + 22, L - (D + 22)).Trim
                    W = tmpHTML.IndexOf("</b></b><br><br>")
                    Wq = tmpHTML.IndexOf("<b>Quelle:</b>")
                    If Wq > 0 Then
                        Wqq = tmpHTML.IndexOf("<br><br>", Wq)
                        B = tmpHTML.IndexOf("</font></p>", Wqq + 8)
                        FullPlot = Web.HttpUtility.HtmlDecode(tmpHTML.Substring(Wqq + 8, B - (Wqq + 8)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)
                    ElseIf W > 0 Then
                        B = tmpHTML.IndexOf("</font></p>", W + 16)
                        FullPlot = Web.HttpUtility.HtmlDecode(tmpHTML.Substring(W + 16, B - (W + 16)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)
                    End If
                End If
            End If

        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try

        Return FullPlot
    End Function

    Private Sub GetOFDBDetails()
        Dim sURL As String = GetOFDBUrlFromIMDBID()

        Try
            If Not String.IsNullOrEmpty(sURL) Then
                Dim sHTTP As New HTTP
                Dim HTML As String = sHTTP.DownloadData(sURL)
                sHTTP = Nothing

                If Not String.IsNullOrEmpty(HTML) Then
                    'title
                    If String.IsNullOrEmpty(OFDBMovie.Title) OrElse Not Master.eSettings.MovieLockTitle Then
                        Dim OFDBTitle As String = CleanTitle(Web.HttpUtility.HtmlDecode(Regex.Match(HTML, "<td width=""99\%""><h1 itemprop=""name""><font face=""Arial,Helvetica,sans-serif"" size=""3""><b>([^<]+)</b></font></h1></td>").Groups(1).Value.ToString))
                        If OFDBTitle.EndsWith(", Der") Then
                            _title = String.Concat("Der ", OFDBTitle.Replace(", Der", " ")).Trim
                        ElseIf OFDBTitle.EndsWith(", Die") Then
                            _title = String.Concat("Die ", OFDBTitle.Replace(", Die", " ")).Trim
                        ElseIf OFDBTitle.EndsWith(", Das") Then
                            _title = String.Concat("Das ", OFDBTitle.Replace(", Das", " ")).Trim
                        ElseIf OFDBTitle.EndsWith(", The") Then
                            _title = String.Concat("The ", OFDBTitle.Replace(", The", " ")).Trim
                        ElseIf OFDBTitle.EndsWith(", Ein") Then
                            _title = String.Concat("Ein ", OFDBTitle.Replace(", Ein", " ")).Trim
                        ElseIf OFDBTitle.EndsWith(", Eine") Then
                            _title = String.Concat("Eine ", OFDBTitle.Replace(", Eine", " ")).Trim
                        Else
                            _title = OFDBTitle
                        End If
                    End If

                    Dim D, Dq, W, Wq, B As Integer
                    Dim tmpHTML As String

                    'outline
                    If String.IsNullOrEmpty(OFDBMovie.Outline) OrElse Not Master.eSettings.MovieLockOutline Then
                        D = HTML.IndexOf("<b>Inhalt:</b>")
                        Dq = HTML.IndexOf("<b>Inhalt:</b> <font style=")

                        If Dq > 0 Then
                            Wq = HTML.IndexOf("<span itemprop=""description"">", Dq)
                            W = HTML.IndexOf("<a href=""plot", Wq + 29)
                            _outline = Web.HttpUtility.HtmlDecode(HTML.Substring(Wq + 29, W - (Wq + 29)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)
                        ElseIf D > 0 Then
                            W = HTML.IndexOf("<a href=""", D + 44)
                            _outline = Web.HttpUtility.HtmlDecode(HTML.Substring(D + 44, W - (D + 44)).Replace("<br />", String.Empty).Replace(vbCrLf, " ").Trim)
                        End If
                    End If

                    'full plot
                    D = 0 : Dq = 0 : W = 0
                    If String.IsNullOrEmpty(OFDBMovie.Plot) OrElse Not Master.eSettings.MovieLockPlot Then
                        D = HTML.IndexOf("<b>Inhalt:</b>")
                        Dq = HTML.IndexOf("<b>Inhalt:</b> <font style=")

                        If Dq > 0 Then
                            Dim L As Integer = HTML.Length
                            tmpHTML = HTML.Substring(D + 197, L - (D + 197)).Trim
                            W = tmpHTML.IndexOf("<a href=""")
                            If W > 0 Then
                                B = tmpHTML.IndexOf("""><b>[mehr]</b>", W + 9)
                                If B > 0 Then
                                    Dim FullPlot = GetFullPlot(String.Concat("http://www.ofdb.de/", tmpHTML.Substring(W + 9, B - (W + 9))))
                                    If Not String.IsNullOrEmpty(FullPlot) Then
                                        _plot = FullPlot
                                    End If
                                End If
                            End If
                        ElseIf D > 0 Then
                            Dim L As Integer = HTML.Length
                            tmpHTML = HTML.Substring(D + 44, L - (D + 44)).Trim
                            W = tmpHTML.IndexOf("<a href=""")
                            If W > 0 Then
                                B = tmpHTML.IndexOf("""><b>[mehr]</b>", W + 9)
                                If B > 0 Then
                                    Dim FullPlot = GetFullPlot(String.Concat("http://www.ofdb.de/", tmpHTML.Substring(W + 9, B - (W + 9))))
                                    If Not String.IsNullOrEmpty(FullPlot) Then
                                        _plot = FullPlot
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'genre
                    D = 0 : W = 0
                    If String.IsNullOrEmpty(OFDBMovie.Genre) OrElse Not Master.eSettings.MovieLockGenre Then
                        D = HTML.IndexOf("class=""Normal"">Genre(s):</font></td>")
                        If D > 0 Then
                            W = HTML.IndexOf("</table>", D)
                            If W > 0 Then
                                Dim rGenres As MatchCollection = Regex.Matches(HTML.Substring(D, W - D), "<a.*?href=[""'](?<url>.*?)[""'].*?><span itemprop=""genre"">(?<name>.*?)</span></a>")
                                Dim Gen = From M In rGenres _
                                      Select N = Web.HttpUtility.HtmlDecode(DirectCast(M, Match).Groups("name").ToString)
                                If Gen.Count > 0 Then
                                    Dim tGenre As String = Strings.Join(Gen.ToArray, "/").Trim
                                    tGenre = StringUtils.GenreFilter(tGenre)
                                    If Not String.IsNullOrEmpty(tGenre) Then
                                        _genre = Strings.Join(tGenre.Split(Convert.ToChar("/")), " / ").Trim
                                    End If
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name,ex)
        End Try
    End Sub

    Private Function GetOFDBUrlFromIMDBID() As String
        Dim ofdbURL As String = String.Empty
        Try

            Dim sHTTP As New HTTP
            Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.ofdb.de/view.php?SText=", imdbID, "&Kat=IMDb&page=suchergebnis&sourceid=mozilla-search"))
            sHTTP = Nothing


            If Not String.IsNullOrEmpty(HTML) Then
                Dim mcOFDBURL As MatchCollection = Regex.Matches(HTML, "<a href=""film/([^<]+)"" onmouseover")
                If mcOFDBURL.Count > 0 Then
                    'just use the first one if more are found
                    ofdbURL = String.Concat("http://www.ofdb.de/", Regex.Match(mcOFDBURL(0).Value.ToString, """(film/([^<]+))""").Groups(1).Value.ToString)
                End If
            Else
                logger.Warn( "OFDB Query returned no results for ID of <{0}>", imdbID)
            End If
        Catch ex As Exception
            logger.ErrorException(New StackFrame().GetMethod().Name & vbTab & "Error scraping ODFB (too many connections?):" & imdbID, ex)
        End Try

        Return ofdbURL
    End Function

#End Region 'Methods

End Class