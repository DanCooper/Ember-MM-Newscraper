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
Imports System.Runtime.CompilerServices
Imports NLog

'The InternalsVisibleTo is required for unit testing the friend methods
<Assembly: InternalsVisibleTo("EmberAPI_Test")> 

Namespace IMDb

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _VideoLinks As VideoLinkItemCollection

#End Region 'Fields

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event VideoLinksRetrieved(ByVal bSuccess As Boolean)

#End Region 'Events

#Region "Properties"

        Public ReadOnly Property VideoLinks() As VideoLinkItemCollection
            Get
                If _VideoLinks Is Nothing Then
                    _VideoLinks = New VideoLinkItemCollection
                End If
                Return _VideoLinks
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query, like "http://www.imdb.com/video/imdb/vi3138759961/"</param>
        ''' <remarks>If the <paramref name="url">URL</paramref> leads to a IMDb video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.</remarks>
        Public Sub GetVideoLinks(ByVal url As String)
            Try
                _VideoLinks = ParseIMDbFormats(url, False)

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub
        ''' <summary>
        ''' Extract and return the title of the video from the supplied HTML.
        ''' </summary>
        ''' <param name="HTML">The text to parse for the title</param>
        ''' <returns><c>String</c> representing the title of the video, as extracted from the IMDb page.</returns>
        ''' <remarks>This method looks in the page's metadata, looking for the meta title tag, and 
        ''' fetching the content attribute.</remarks>
        Public Function GetVideoTitle(ByVal HTML As String) As String
            Dim result As String = ""
            Dim KeyPattern As String = "meta name=\""title\"" content=\s*\""([^']*?)\"""
            Dim nPattern As String = "<title>.*?\((?<TITLE>.*?)\).*?</title>"   'Trailer title inside brackets
            Dim mPattern As String = "<title>(?<TITLE>.*?)</title>"             'Trailer title without brackets
            If Regex.IsMatch(HTML, KeyPattern) Then
                result = Regex.Match(HTML, KeyPattern).Groups(1).Value
            End If

            Return result
        End Function
        ''' <summary>
        ''' Fetches the list of valid video links for the given URL
        ''' </summary>
        ''' <param name="url"><c>String</c> representation of the URL to query, like "http://www.imdb.com/video/imdb/vi3138759961/"</param>
        ''' <param name="doProgress"><c>Boolean</c> representing whether an event should be raised. Note that this functionality
        ''' has not yet been implemented.</param>
        ''' <remarks>Note that most callers should use <c>GetVideoLinks(ByVal url As String)</c> instead of this <c>Private</c> function.
        ''' If the <paramref name="url">URL</paramref> leads to a YouTube video page, this method will parse
        ''' the page to extract the various video stream links, and store them in the internal <c>VideoLinks</c> collection.
        ''' Note that only one link of each <c>Enums.TrailerQuality</c> will be kept.
        ''' </remarks>
        Private Function ParseIMDbFormats(ByVal url As String, ByVal doProgress As Boolean) As VideoLinkItemCollection
            Dim DownloadLinks As New VideoLinkItemCollection
            Dim sHTTP As New HTTP
            'Dim trailerTitle As String
            Dim Qualities As MatchCollection

            'Dim nPattern As String = "<title>.*?\((?<TITLE>.*?)\).*?</title>"   'Trailer title inside brakets
            'Dim mPattern As String = "<title>(?<TITLE>.*?)</title>"             'Trailer title without brakets
            Dim qPattern As String = "imdb/single\?format=([0-9]+)p"            'Trailer qualities

            Try
                Dim Html As String = sHTTP.DownloadData(url)

                If Html.ToLower.Contains("page not found") Then
                    Html = String.Empty
                End If

                If String.IsNullOrEmpty(Html.Trim) Then Return DownloadLinks

                ''go to specific trailer website
                'trailerTitle = Regex.Match(Html, nPattern).Groups(1).Value.ToString.Trim
                'If String.IsNullOrEmpty(trailerTitle) Then
                '    trailerTitle = Regex.Match(Html, mPattern).Groups(1).Value.ToString.Trim
                '    trailerTitle = trailerTitle.Replace("- IMDb", String.Empty).Trim
                'End If

                'get all qualities of a specific trailer
                Qualities = Regex.Matches(Html, String.Concat(url, qPattern))
                Dim trailerCollection As String() = From m As Object In Qualities Select CType(m, Match).Value Distinct.ToArray()

                'get all download URLs of a specific trailer
                For Each qual As String In trailerCollection
                    Dim Link As New VideoLinkItem

                    sHTTP = New HTTP
                    Dim QualityPage As String = sHTTP.DownloadData(qual)
                    sHTTP = Nothing
                    Dim QualLink As Match = Regex.Match(QualityPage, "videoPlayerObject.*?viconst")
                    Dim dowloadURL As MatchCollection = Regex.Matches(QualLink.Value, "ffname"":""(?<QUAL>.*?)"",""height.*?url"":""(?<LINK>.*?)""")
                    Dim Resolution As String = dowloadURL.Item(0).Groups(1).Value

                    Link.URL = dowloadURL.Item(0).Groups(2).Value

                    Select Case Resolution
                        Case "SD"
                            Link.Description = "240p (MP4)"
                            Link.FormatCodec = Enums.TrailerCodec.MP4
                            Link.FormatQuality = Enums.TrailerQuality.SQ240p
                        Case "480p"
                            Link.Description = "480p (MP4)"
                            Link.FormatCodec = Enums.TrailerCodec.MP4
                            Link.FormatQuality = Enums.TrailerQuality.HQ480p
                        Case "720p"
                            Link.Description = "720p (MP4)"
                            Link.FormatCodec = Enums.TrailerCodec.MP4
                            Link.FormatQuality = Enums.TrailerQuality.HD720p
                        Case Else
                            Link.FormatQuality = Enums.TrailerQuality.OTHERS
                    End Select

                    If Not String.IsNullOrEmpty(Link.URL) Then 'AndAlso sHTTP.IsValidURL(Link.URL) Then
                        DownloadLinks.Add(Link)
                    End If
                Next

                Return DownloadLinks

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
                Return New VideoLinkItemCollection
            Finally
                sHTTP = Nothing
            End Try
        End Function

#End Region 'Methods

    End Class

    Public Class VideoLinkItem

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As Enums.TrailerCodec
        Private _FormatQuality As Enums.TrailerQuality
        Private _URL As String

#End Region 'Fields

#Region "Properties"

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set(ByVal value As String)
                _URL = value
            End Set
        End Property

        Friend Property FormatCodec() As Enums.TrailerCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerCodec)
                _FormatCodec = value
            End Set
        End Property

        Friend Property FormatQuality() As Enums.TrailerQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class VideoLinkItemCollection
        Inherits Generic.SortedList(Of Enums.TrailerQuality, VideoLinkItem)

#Region "Methods"
        ''' <summary>
        ''' Adds the provided <c>VideoLinkItem</c> to the collection. 
        ''' </summary>
        ''' <param name="Link">The <c>VideoLinkItem</c> to be added. Nothing values or existing values will be ignored</param>
        ''' <remarks>NOTE that the collection is arranged to allow only one
        ''' of VideoLink of each <c>Enums.TrailerQuality</c>. This means that attempting
        ''' to add a second <c>VideoLinkItem</c> with with an identical <c>Enums.TrailerQuality</c>
        ''' will silently fail, while leaving the original untouched.
        ''' 
        ''' 2013/11/07 Dekker500 - Added parameter validation
        ''' </remarks>
        Public Shadows Sub Add(ByVal Link As VideoLinkItem)
            If Link IsNot Nothing AndAlso Not MyBase.ContainsKey(Link.FormatQuality) Then
                MyBase.Add(Link.FormatQuality, Link)
            End If

        End Sub

#End Region 'Methods

    End Class

End Namespace
