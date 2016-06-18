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
Imports System.Web
Imports System.Text.RegularExpressions

Namespace VideobusterDE

    Public Class Scraper

#Region "Fields"

        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

        Public Function GetTrailers(ByVal strTitle As String) As List(Of MediaContainers.Trailer)
            Dim alTrailers As New List(Of MediaContainers.Trailer)

            Dim pSearchResultsArea As String = "<div class=""main left"">(?<RESULTS>.*?)sidebar"
            Dim pSearchResults As String = "<div class=""infos""><a href=""(?<URL>.*?)"" class=""title"">(?<TITLE>.*?)<"
            Dim pPlaylistArea As String = "<div class=""playlist"">(?<PLAYLIST>.*?)<\/div><div id="""
            Dim pTrailers As String = "<div class=""item"">.*?<small>(?<DURATION>.*?) min.*?<a href=""(?<VIDEOURL>.*?)"".*?title=""(?<TITLE>.*?)"".*?<meta itemprop=""height"" content=""(?<RESOLUTION>.*?)"".*?inLanguage"" content=""(?<LANGUAGE>.*?)"""

            Dim SearchURL = String.Concat("https://www.videobuster.de/titlesearch.php?oldvalue_tab_search_content=movies&search_title=", _
                                          HttpUtility.UrlEncode(strTitle), _
                                          "&tab_search_content=movies")

            Dim sHTTP As New HTTP
            Dim sHtml As String = String.Empty
            sHtml = sHTTP.DownloadData(SearchURL)
            sHTTP = Nothing

            Try
                Dim SearchResultsArea As Match = Regex.Match(sHtml, pSearchResultsArea, RegexOptions.Singleline Or RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1))

                If SearchResultsArea.Success Then
                    Dim SearchResults As MatchCollection = Regex.Matches(SearchResultsArea.Value.ToString, pSearchResults, RegexOptions.Singleline Or RegexOptions.IgnoreCase, TimeSpan.FromDays(1))
                    If SearchResults.Count > 0 Then
                        sHTTP = New HTTP
                        Dim MoviePageURL As String = String.Concat("https://www.videobuster.de", SearchResults.Item(0).Groups("URL").Value.ToString)
                        Dim MoviePage As String = sHTTP.DownloadData(MoviePageURL)
                        sHTTP = Nothing
                        Dim PlaylistArea As Match = Regex.Match(MoviePage, pPlaylistArea, RegexOptions.Singleline Or RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1))
                        If PlaylistArea.Success Then
                            Dim Trailers As MatchCollection = Regex.Matches(PlaylistArea.Value.ToString, pTrailers, RegexOptions.Singleline Or RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1))
                            For Each tTrailer As Match In Trailers
                                Dim nTrailer As New MediaContainers.Trailer
                                Dim strLangCode3 As String = String.Empty
                                If tTrailer.Groups("LANGUAGE").Value.ToString = "ger" Then
                                    strLangCode3 = "deu"
                                Else
                                    strLangCode3 = tTrailer.Groups("LANGUAGE").Value.ToString
                                End If
                                nTrailer.Duration = tTrailer.Groups("DURATION").Value.ToString
                                nTrailer.LongLang = Localization.ISOGetLangByCode3(strLangCode3)
                                nTrailer.Quality = GetVideoQuality(tTrailer.Groups("RESOLUTION").Value.ToString)
                                nTrailer.Scraper = "Videobuster.de"
                                nTrailer.ShortLang = Localization.ISOLangGetCode2ByCode3(strLangCode3)
                                nTrailer.Source = "Videobuster.de"
                                nTrailer.Title = tTrailer.Groups("TITLE").Value.ToString
                                nTrailer.Type = GetTrailerType(tTrailer.Groups("TITLE").Value.ToString)
                                nTrailer.URLVideoStream = tTrailer.Groups("VIDEOURL").Value.ToString
                                nTrailer.URLWebsite = tTrailer.Groups("VIDEOURL").Value.ToString
                                alTrailers.Add(nTrailer)
                            Next
                        End If
                    End If
                End If
            Catch ex As TimeoutException
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alTrailers
        End Function

        Public Function GetVideoQuality(ByVal strHeight As String) As Enums.TrailerVideoQuality
            Select Case strHeight
                Case "1080"
                    Return Enums.TrailerVideoQuality.HD1080p
                Case "720"
                    Return Enums.TrailerVideoQuality.HD720p
                Case "480", "576"
                    Return Enums.TrailerVideoQuality.HQ480p
                Case "360"
                    Return Enums.TrailerVideoQuality.SQ360p
                Case "240", "270"
                    Return Enums.TrailerVideoQuality.SQ240p
                Case Else
                    Return Enums.TrailerVideoQuality.UNKNOWN
            End Select
        End Function

        Public Function GetTrailerType(ByVal strTitle As String) As Enums.TrailerType
            If strTitle.ToLower.Contains("teaser") Then
                Return Enums.TrailerType.Teaser
            Else
                Return Enums.TrailerType.Trailer
            End If
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim Type As Enums.ModifierType

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace