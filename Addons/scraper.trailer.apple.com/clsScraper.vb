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
Imports System.Web.Script.Serialization

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields 

#Region "Methods"
    ''' <summary>
    ''' Scrapes available trailerlinks from Apple
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Shared Function GetMovieTrailers(ByVal originalTitle As String) As List(Of MediaContainers.MediaFile)
        Dim nTrailerList As New List(Of MediaContainers.MediaFile)
        Try
            If Not String.IsNullOrEmpty(originalTitle) Then
                Dim sHTTP As New HTTP
                Dim strDataPageJson As String = "data/page.json"
                Dim strMovieTrailersPage As String = String.Empty
                Dim strSearchBaseURL As String = "https://trailers.apple.com/trailers/home/scripts/quickfind.php?q="
                Dim strTrailersBaseURL As String = "https://trailers.apple.com"

                ' Step 1: Use Apple quicksearch which will return json response! 
                Dim strSearchURL = String.Concat(strSearchBaseURL, HttpUtility.UrlEncode(originalTitle))
                Dim strJsonSearchResults = sHTTP.DownloadData(strSearchURL)
                sHTTP = Nothing

                ' Step 2: If json returned -> parse Json response and get Url to moviepage, else use google to search for trailer on Apple
                If Not String.IsNullOrEmpty(strJsonSearchResults.Trim) Then
                    Dim nSerializer = New JavaScriptSerializer
                    Dim nSearchResults = nSerializer.Deserialize(Of SearchResult)(HttpUtility.HtmlDecode(strJsonSearchResults))
                    If nSearchResults IsNot Nothing AndAlso Not nSearchResults.error AndAlso nSearchResults.results IsNot Nothing AndAlso nSearchResults.results.Count > 0 Then
                        strMovieTrailersPage = String.Concat(strTrailersBaseURL, nSearchResults.results(0).location)
                    End If
                Else
                    'TODO
                End If

                'Step 3: call the "/data/page.json" page to get the trailers and qualities list
                If Not String.IsNullOrEmpty(strMovieTrailersPage) Then
                    Dim strDataPageJsonURL = String.Concat(strMovieTrailersPage, strDataPageJson)
                    sHTTP = New HTTP
                    Dim strJsonTrailerPage = sHTTP.DownloadData(strDataPageJsonURL)
                    sHTTP = Nothing
                    If Not String.IsNullOrEmpty(strJsonTrailerPage.Trim) Then
                        Dim nSerializer = New JavaScriptSerializer
                        Dim nDataPageResults = nSerializer.Deserialize(Of TrailerResult)(HttpUtility.HtmlDecode(strJsonTrailerPage))
                        If nDataPageResults IsNot Nothing AndAlso nDataPageResults.clips IsNot Nothing AndAlso nDataPageResults.clips.Count > 0 Then
                            Dim lstTrailers = From trailers In nDataPageResults.clips.Where(Function(f) f.versions IsNot Nothing AndAlso f.versions.enus IsNot Nothing AndAlso f.versions.enus.sizes IsNot Nothing)
                            For Each clip As ClipItem In lstTrailers
                                Dim nTrailer As New MediaContainers.MediaFile With {
                                    .Duration = clip.runtime,
                                    .Source = "Apple",
                                    .Scraper = "Apple",
                                    .Title = clip.title,
                                    .UrlWebsite = strMovieTrailersPage,
                                    .VideoType = GetVideoType(clip.title)
                                }
                                If clip.versions.enus.sizes.hd1080 IsNot Nothing AndAlso Not String.IsNullOrEmpty(clip.versions.enus.sizes.hd1080.src) Then
                                    nTrailer.Streams.VideoStreams.Add(New MediaContainers.MediaFile.VideoStream With {
                                                                      .Codec = Enums.VideoCodec.H264,
                                                                      .FileExtension = ".mov",
                                                                      .Resolution = Enums.VideoResolution.HD1080p,
                                                                      .StreamUrl = clip.versions.enus.sizes.hd1080.src.Replace("1080p", "h1080p")
                                                                      })
                                End If
                                If clip.versions.enus.sizes.hd720 IsNot Nothing AndAlso Not String.IsNullOrEmpty(clip.versions.enus.sizes.hd720.src) Then
                                    nTrailer.Streams.VideoStreams.Add(New MediaContainers.MediaFile.VideoStream With {
                                                                      .Codec = Enums.VideoCodec.H264,
                                                                      .FileExtension = ".mov",
                                                                      .Resolution = Enums.VideoResolution.HD720p,
                                                                      .StreamUrl = clip.versions.enus.sizes.hd720.src.Replace("720p", "h720p")
                                                                      })
                                End If
                                If clip.versions.enus.sizes.sd IsNot Nothing AndAlso Not String.IsNullOrEmpty(clip.versions.enus.sizes.sd.src) Then
                                    nTrailer.Streams.VideoStreams.Add(New MediaContainers.MediaFile.VideoStream With {
                                                                      .Codec = Enums.VideoCodec.H264,
                                                                      .FileExtension = ".mov",
                                                                      .Resolution = Enums.VideoResolution.HQ480p,
                                                                      .StreamUrl = clip.versions.enus.sizes.sd.src.Replace("480p", "h480p")
                                                                      })
                                End If

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

#Region "Nested Types"

    Public Class SearchResult

#Region "Properties"

        Public Property [error] As Boolean

        Public Property results As SearchResultItem()

#End Region 'Properties

    End Class

    Public Class SearchResultItem

#Region "Properties"

        Public Property location As String
        Public Property title As String

#End Region 'Properties

    End Class

    Public Class TrailerResult

#Region "Properties"

        Public Property clips As ClipItem()

#End Region 'Properties

    End Class

    Public Class ClipItem

#Region "Properties"

        Public Property runtime As String

        Public Property title As String

        Public Property versions As VersionItem

#End Region 'Properties

    End Class

    Public Class VersionItem

#Region "Properties"

        Public Property enus As EnusItem

#End Region 'Properties

    End Class

    Public Class EnusItem

#Region "Properties"

        Public Property sizes As SizeItem

#End Region 'Properties

    End Class

    Public Class SizeItem

#Region "Properties"

        Public Property sd As TrailerItem
        Public Property hd720 As TrailerItem
        Public Property hd1080 As TrailerItem

#End Region 'Properties

    End Class

    Public Class TrailerItem

#Region "Properties"

        Public Property src As String

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class