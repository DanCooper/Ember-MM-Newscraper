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
Imports System.Text.RegularExpressions

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Private Shared Function ConvertQuality(ByVal quality As String) As Enums.TrailerVideoQuality
        Select Case quality
            Case "480P"
                Return Enums.TrailerVideoQuality.HQ480p
            Case "720P"
                Return Enums.TrailerVideoQuality.HD720p
            Case "1080P"
                Return Enums.TrailerVideoQuality.HD1080p
            Case Else
                Return Enums.TrailerVideoQuality.Any 
        End Select
    End Function

    Public Shared Function GetTrailers(ByVal originalTitle As String, ByVal imdbId As String) As List(Of MediaContainers.Trailer)
        If String.IsNullOrEmpty(originalTitle) Then Return New List(Of MediaContainers.Trailer)

        Dim nTrailerlist As New List(Of MediaContainers.Trailer)

        Dim strBaseUrl As String = "http://www.davestrailerpage.co.uk/trailers_"
        Dim strSubPagePath As String = String.Empty

        'clean title to search 
        Dim strSearchTitle As String = Regex.Replace(originalTitle.ToLower.Trim, "(^the |^a |^an )", String.Empty, RegexOptions.IgnoreCase)

        'select subpage URL
        'subpages are splitted into "0-9", "XYZ" and "A", "B", "C" and so on
        If Regex.IsMatch(strSearchTitle, "^\d") Then
            strSubPagePath = "0to9.html"
        ElseIf Regex.IsMatch(strSearchTitle, "^(x|y|z)") Then
            strSubPagePath = "xyz.html"
        Else
            strSubPagePath = String.Concat(strSearchTitle(0), ".html")
        End If

        If Not String.IsNullOrEmpty(strSubPagePath) Then
            Dim strSubPageUrl As String = String.Concat(strBaseUrl, strSubPagePath)
            Dim webParsing As New HtmlWeb
            Dim htmldocSubPage As HtmlDocument = webParsing.Load(strSubPageUrl)
            If htmldocSubPage IsNot Nothing Then
                'target is to get the <tr> node of a movie entry:
                '
                '<tr>
                '   <td>
                '       <ul>
                '           <li>
                '               <b>Thor: Ragnarok</b>
                '               <ul>
                '                   <li>
                '                       <a href="http://www.imdb.com/title/tt3501632/" target="_blank">IMDB</a>
                '                   </li>
                '                   <li>
                '                       <b>Trailer 1</b>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-1_h480p.mov"> 480P</a>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-1_h720p.mov"> 720P</a>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-1_h1080p.mov"> 1080P</a>
                '                   </li>
                '                   <li>
                '                       <b>Trailer 2</b>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-2_h480p.mov"> 480P</a>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-2_h720p.mov"> 720P</a>
                '                       <a href="http://trailers.apple.com/movies/marvel/thor-ragnarok/thor-ragnarok-trailer-2_h1080p.mov"> 1080P</a>
                '                   </li>
                '               </ul>
                '           </li>
                '       </ul>
                '   </td>
                '</tr>
                Dim ndMovie As HtmlNode = Nothing
                '
                'try to search by IMDb ID
                '
                If Not String.IsNullOrEmpty(imdbId) Then
                    'search for the node that includes the IMDb ID like:
                    '<a href="http://www.imdb.com/title/tt3501632/" target="_blank">IMDB</a>
                    Dim ndIMDbID = htmldocSubPage.DocumentNode.SelectSingleNode(String.Format("//*[@*[contains(., '{0}')]]", imdbId))
                    If ndIMDbID IsNot Nothing Then                        '
                        'select the Ancestor <tr> node
                        ndMovie = ndIMDbID.Ancestors("tr").FirstOrDefault
                    End If
                End If
                '
                'if no result try to search by title
                '
                If ndMovie Is Nothing Then
                    'search for the node that contains the title like:
                    '<b>Thor: Ragnarok</b>
                    Dim ndTitle = htmldocSubPage.DocumentNode.SelectSingleNode(String.Format("//b[.='{0}']", originalTitle))
                    If ndTitle IsNot Nothing Then
                        'select the Ancestor <tr> node
                        ndMovie = ndTitle.Ancestors("tr").FirstOrDefault
                    End If
                End If

                If ndMovie IsNot Nothing Then
                    nTrailerlist.AddRange(GetTrailers(ndMovie))
                End If
            End If
        End If

        Return nTrailerlist
    End Function

    Private Shared Function GetTrailers(ByVal node As HtmlNode) As List(Of MediaContainers.Trailer)
        If node Is Nothing Then Return New List(Of MediaContainers.Trailer)
        Dim nTrailerList As New List(Of MediaContainers.Trailer)

        'search all <b> nodes
        'first node is usually the movie title node, the other nodes should be trailer title nodes
        Dim ndNodeWithB = node.Descendants("b")
        For Each aNode In ndNodeWithB
            'switch to the parent <li> node and search for <a> nodes
            'the <li> node that contains the movie title node should not have any <a> node so only the trailer nodes should be listed as result
            Dim ndTrailerGroup = aNode.ParentNode.SelectNodes("a")
            If ndTrailerGroup IsNot Nothing Then
                Dim nTrailer As New MediaContainers.Trailer With {
                    .Title = aNode.InnerText,
                    .Scraper = "DavesTrailerPage"
                }
                For Each aStream In ndTrailerGroup
                    nTrailer.Streams.VideoStreams.Add(New MediaContainers.Trailer.VideoStream With {
                                                      .FormatQuality = ConvertQuality(aStream.InnerText),
                                                      .URL = aStream.Attributes(0).Value
                                                      })
                Next
                If nTrailer.Streams.HasStreams Then
                    nTrailer.Streams.VideoStreams.Sort()
                    nTrailer.Quality = nTrailer.Streams.VideoStreams(0).FormatQuality
                    nTrailer.URLWebsite = nTrailer.Streams.VideoStreams(0).URL
                    nTrailer.Source = If(nTrailer.URLWebsite.ToLower.Contains("apple"), "Apple", String.Empty)
                    nTrailerList.Add(nTrailer)
                End If
            End If
        Next

        Return nTrailerList
    End Function

#End Region 'Methods

End Class