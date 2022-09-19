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
Imports HtmlAgilityPack
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"
    Private Shared Function BuildStream(ByVal url As String, ByVal width As String, ByVal height As String, ByVal codec As String) As MediaContainers.MediaFile.VideoStream
        Dim nVideoStream As New MediaContainers.MediaFile.VideoStream With {
        .Codec = ConvertVideoCodec(codec),
        .FileExtension = Path.GetExtension(url),
        .Resolution = ConvertVideoResolution(width, height),
        .StreamUrl = url
        }

        Return nVideoStream
    End Function

    Private Shared Function ConvertVideoCodec(ByVal codec As String) As Enums.VideoCodec
        Select Case codec.ToLower
            Case "video/mp4"
                Return Enums.VideoCodec.H264
            Case "video/webm"
                Return Enums.VideoCodec.VP8
            Case Else
                Return Enums.VideoCodec.UNKNOWN
        End Select
    End Function

    Private Shared Function ConvertVideoResolution(ByVal width As String, ByVal height As String) As Enums.VideoResolution
        Select Case width
            Case "426"
                Return Enums.VideoResolution.SQ240p
            Case "480"
                Return Enums.VideoResolution.SQ360p
            Case "640"
                Return Enums.VideoResolution.HQ480p
            Case "1280"
                Return Enums.VideoResolution.HD720p
            Case "1920"
                Return Enums.VideoResolution.HD1080p
            Case Else
                Select Case height
                    Case "240", "288"
                        Return Enums.VideoResolution.SQ240p
                    Case "320", "334", "356", "360", "362"
                        Return Enums.VideoResolution.SQ360p
                    Case "480"
                        Return Enums.VideoResolution.HQ480p
                    Case Else
                        Return Enums.VideoResolution.UNKNOWN
                End Select
        End Select
    End Function

    Public Shared Function GetTrailers(ByVal title As String) As List(Of MediaContainers.MediaFile)
        Dim nTrailerList As New List(Of MediaContainers.MediaFile)
        If String.IsNullOrEmpty(title) Then Return nTrailerList

        Try
            Dim strMainUrl = "https://www.videobuster.de"
            Dim strSearchUrl = String.Concat(strMainUrl,
                                             "/titlesearch.php?oldvalue_tab_search_content=movies&search_title=",
                                             HttpUtility.UrlEncode(title),
                                             "&tab_search_content=movies")

            Dim webParsing As New HtmlWeb
            Dim htmldocSearchResults As HtmlDocument = webParsing.Load(strSearchUrl)

            'target is to get all <div class="row result"> nodes of the <div id="movies" class="titles-list right-navi-filter"> node (all search results)
            Dim dnSearchResults = htmldocSearchResults.DocumentNode.SelectNodes("//div[@class=""row result""]")

            If dnSearchResults IsNot Nothing Then
                'take first search result and search the movie page in the attributes
                Dim strMovieUrl = dnSearchResults(0).SelectSingleNode(".//a[@title]").GetAttributeValue("href", String.Empty)
                If Not String.IsNullOrEmpty(strMovieUrl) Then
                    '-----------------------------------------------------------------------------------------
                    ' load the movie page
                    '-----------------------------------------------------------------------------------------
                    ' "HtmlWeb.Load" does not work because of Java script doesn't get finished before parsing
                    ' so we use HTTP.DownloadData to get the HTML and load that into a HtmlDocument
                    '-----------------------------------------------------------------------------------------

                    Dim sHTTP As New HTTP
                    Dim MoviePage As String = sHTTP.DownloadData(String.Concat(strMainUrl, strMovieUrl))
                    sHTTP = Nothing

                    Dim htmldocMoviePage As New HtmlDocument
                    If Not String.IsNullOrEmpty(MoviePage) Then htmldocMoviePage.LoadHtml(MoviePage)
                    If htmldocMoviePage IsNot Nothing Then
                        Dim dnTrailers = htmldocMoviePage.DocumentNode.SelectNodes("//div[@class=""item-outer""]//div[@class=""item-top""]")
                        If dnTrailers IsNot Nothing Then
                            For Each tTrailer In dnTrailers
                                'get trailer main information from first stream
                                Dim strTitle = tTrailer.ChildNodes(1).SelectSingleNode("meta[@itemprop=""name""]").Attributes(1).Value
                                Dim strDuration = tTrailer.ChildNodes(1).SelectSingleNode("meta[@itemprop=""duration""]").Attributes(1).Value
                                Dim strLanguage = tTrailer.ChildNodes(1).SelectSingleNode("meta[@itemprop=""inLanguage""]").Attributes(1).Value
                                If strLanguage = "ger" Then strLanguage = "deu"
                                Dim strUrlWebsite = tTrailer.ChildNodes(1).SelectSingleNode("meta[@itemprop=""contentUrl""]").Attributes(1).Value

                                Dim nTrailer As New MediaContainers.MediaFile With {
                                    .Duration = StringUtils.SecondsToDuration(Regex.Match(strDuration, "\d+").Value),
                                    .Language = If(Not String.IsNullOrEmpty(strLanguage), strLanguage, String.Empty),
                                    .LongLanguage = If(Not String.IsNullOrEmpty(strLanguage), Localization.Languages.Get_Name_By_Alpha3(strLanguage), String.Empty),
                                    .Scraper = "Videobuster.de",
                                    .Source = "Videobuster.de",
                                    .Title = strTitle,
                                    .UrlWebsite = strUrlWebsite,
                                    .VideoType = GetVideoType(strTitle)
                                }

                                'get all streams
                                For Each tStream In tTrailer.SelectNodes("span")
                                    Dim strUrl = tStream.SelectSingleNode("meta[@itemprop=""contentUrl""]").Attributes(1).Value
                                    Dim strExtension = tStream.SelectSingleNode("meta[@itemprop=""contentUrl""]").Attributes(1).Value
                                    Dim strWidth = tStream.SelectSingleNode("meta[@itemprop=""width""]").Attributes(1).Value
                                    Dim strHeight = tStream.SelectSingleNode("meta[@itemprop=""height""]").Attributes(1).Value
                                    Dim strCodec = tStream.SelectSingleNode("meta[@itemprop=""encodingFormat""]").Attributes(1).Value
                                    'build video stream information
                                    nTrailer.Streams.VideoStreams.Add(BuildStream(strUrl, strWidth, strHeight, strCodec))
                                Next

                                If nTrailer.StreamsSpecified Then
                                    nTrailerList.Add(nTrailer)
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        Return nTrailerList
    End Function

    Private Shared Function GetVideoType(ByVal title As String) As Enums.VideoType
        Select Case True
            Case title.ToLower.Contains("clip")
                Return Enums.VideoType.Clip
            Case title.ToLower.Contains("featurette")
                Return Enums.VideoType.Featurette
            Case title.ToLower.Contains("teaser")
                Return Enums.VideoType.Teaser
            Case title.ToLower.Contains("trailer")
                Return Enums.VideoType.Trailer
            Case Else
                Return Enums.VideoType.Any
        End Select
    End Function

#End Region 'Methods

End Class