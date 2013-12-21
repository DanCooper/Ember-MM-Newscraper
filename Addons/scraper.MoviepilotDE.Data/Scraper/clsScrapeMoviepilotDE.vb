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

Public Class MoviepilotDE

#Region "Fields"

    Private originaltitle As String
    Private MoviepilotDEMovie As MediaContainers.Movie
    Private _fsk As String
    Private _outline As String
    Private _plot As String

#End Region 'Fields

#Region "Constructors"

    Public Sub New(ByVal soriginaltitle As String, ByRef mMovie As MediaContainers.Movie)
        Clear()
        originaltitle = soriginaltitle
        MoviepilotDEMovie = mMovie

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

    Private Function CleanTitle(ByVal sString As String) As String
        Dim CleanString As String = sString

        Try
            If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

            If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
        Return CleanString
    End Function

    Private Sub Clear()
        _fsk = String.Empty
        _outline = String.Empty
        _plot = String.Empty
    End Sub

    Private Sub GetMoviepilotDEDetails()
        Dim sURL As String = GetMoviePilotUrlFromOriginaltitle(originaltitle)

        Try
            If Not String.IsNullOrEmpty(sURL) Then
                Dim sHTTP As New HTTP
                Dim HTML As String = sHTTP.DownloadData(sURL)
                sHTTP = Nothing

                If Not String.IsNullOrEmpty(HTML) Then

                    'outline
                    If String.IsNullOrEmpty(MoviepilotDEMovie.Outline) OrElse Not Master.eSettings.LockOutline Then
                        _outline = GetPlotAndOutline(HTML, 1)
                    End If

                    'full plot
                    If String.IsNullOrEmpty(MoviepilotDEMovie.Plot) OrElse Not Master.eSettings.LockPlot Then
                        _plot = GetPlotAndOutline(HTML, 0)
                    End If

                    'fsk
                    If String.IsNullOrEmpty(MoviepilotDEMovie.Certification) OrElse Not Master.eSettings.LockMPAA Then
                        _fsk = GetFSK(HTML)
                    End If

                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub





    Private Function GetMoviePilotUrlFromOriginaltitle(ByVal originaltitle As String) As String
        Dim MoviePilotURL As String = String.Empty
        Try

            Dim sHTTP As New HTTP
            Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.moviepilot.de/suche?q=", originaltitle, "&type=movie&sourceid=mozilla-search"))
            sHTTP = Nothing
            '    http://www.moviepilot.de/suche?q=Machete+Kills&type=movie
            If Not String.IsNullOrEmpty(HTML) Then

                'Example from HTML-SearchPage: <a href="/movies/machete-kills" class="h3">
                Dim mcMoviePilotURL As MatchCollection = Regex.Matches(HTML, "<a href=""/movies/([^<]+)"" class=")
                If mcMoviePilotURL.Count > 0 Then
                    'just use the first one if more are found
                    '  MoviePilotURL = String.Concat("http://www.moviepilot.de/", Regex.Match(mcMoviePilotURL(0).Value.ToString, """(movies/([^<]+))""").Groups(1).Value.ToString)
                    MoviePilotURL = String.Concat("http://www.moviepilot.de/movies/", mcMoviePilotURL(0).Value.ToString.Substring(17, mcMoviePilotURL(0).Value.ToString.IndexOf(" class") - 18))
                   
                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try

        Return MoviePilotURL
    End Function

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
                                strOutline = RemoveBracketsFromString(strOutline)
                                Return strOutline
                            End If

                            L = tmpHTML.Length
                            tmpHTML = tmpHTML.Substring(tmpHTML.IndexOf("</strong>") + 9, L - (tmpHTML.IndexOf("</strong>") + 9)).Trim
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
                                strPlot = RemoveBracketsFromString(strPlot)
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
                            strPlot = RemoveBracketsFromString(strPlot)
                        End If
                    End If


                End If
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
        Return strPlot
    End Function

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
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
        Return FSK
    End Function


    Private Function RemoveBracketsFromString(str As String) As String
        str = Regex.Replace(str, "\[.+?\]", "")
        str = Regex.Replace(str, "\(.+?\)", "")
        str = str.Replace("  ", " ")
        str = str.Replace(" , ", ", ")
        str = str.Replace(" .", ".")
        Return str.Trim()
    End Function

#End Region 'Methods

End Class