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

Namespace Apple

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Private _VideoLinks As VideoLinkItemCollection

        Private _TrailerLinks As List(Of Trailers)

#End Region 'Fields

#Region "Events"

        Public Event Exception(ByVal ex As Exception)

        Public Event VideoLinksRetrieved(ByVal bSuccess As Boolean)
        Public Event TrailerLinksRetrieved(ByVal bSuccess As Boolean)

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

        Public ReadOnly Property TrailerLinks() As List(Of Trailers)
            Get
                If _TrailerLinks Is Nothing Then
                    _TrailerLinks = New List(Of Trailers)
                End If
                Return _TrailerLinks
            End Get
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub GetTrailerLinks(ByVal url As String)
            Try
                _TrailerLinks = GetTrailerLinks(url, False)

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End Sub

        Private Function GetTrailerLinks(ByVal url As String, ByVal doProgress As Boolean) As List(Of Trailers)
            Dim TrailerLinks As New List(Of Trailers)

            Dim BaseURL As String = "http://www.google.ch/search?q=apple+trailer+"
            Dim DownloadURL As String = "http://trailers.apple.com/trailers/"
            Dim prevQual As String = clsAdvancedSettings.GetSetting("TrailerPrefQual", "1080p", "scraper.Apple.Trailer")
            Dim urlHD As String = "/includes/extralarge.html"
            Dim urlHQ As String = "/includes/large.html"

            Try
                If Not String.IsNullOrEmpty(url) Then
                    Dim tDownloadURL As String = String.Empty
                    Dim tDescription As New List(Of String)

                    Dim sHTTP As New HTTP

                    Dim tPattern As String = "<a href=""(?<URL>.*?)"">(?<TITLE>.*?)</a>"
                    Dim lPattern As String = "<li><a href=""includes/(?<URL>.*?)#"
                    Dim uPattern As String = "<a class=""movieLink"" href=""(?<URL>.*?)\?"
                    Dim zPattern As String = "<li.*?<a href=""(?<LINK>.*?)#.*?<h3 title="".*?>(?<TITLE>.*?)</h3>.*?duration"">(?<DURATION>.*?)</span>*.?</li>"

                    Dim TrailerBaseURL As String = If(url.EndsWith("/"), url.Remove(url.Length - 1, 1), url).Trim
                    Dim TrailerSiteURL = String.Concat(TrailerBaseURL, urlHD)

                    If Not String.IsNullOrEmpty(TrailerBaseURL) Then

                        sHTTP = New HTTP
                        Dim sHtml As String = sHTTP.DownloadData(TrailerSiteURL)
                        sHTTP = Nothing

                        Dim zResult As MatchCollection = Regex.Matches(sHtml, zPattern, RegexOptions.Singleline)

                        For ctr As Integer = 0 To zResult.Count - 1
                            TrailerLinks.Add(New Trailers With {.URL = zResult.Item(ctr).Groups(1).Value, .Description = zResult.Item(ctr).Groups(2).Value, .Duration = zResult.Item(ctr).Groups(3).Value})
                        Next

                        For Each trailer In TrailerLinks
                            Dim DLURL As String = String.Empty
                            Dim TrailerSiteLink As String = String.Concat(TrailerBaseURL, "/", trailer.URL)

                            sHTTP = New HTTP
                            Dim zHtml As String = sHTTP.DownloadData(TrailerSiteLink)
                            sHTTP = Nothing

                            If String.IsNullOrEmpty(zHtml) Then
                                sHTTP = New HTTP
                                zHtml = sHTTP.DownloadData(TrailerSiteLink.Replace("extralarge.html", "large.html"))
                                sHTTP = Nothing
                            End If

                            Dim yResult As MatchCollection = Regex.Matches(zHtml, uPattern, RegexOptions.Singleline)

                            If yResult.Count > 0 Then
                                tDownloadURL = Web.HttpUtility.HtmlDecode(yResult.Item(0).Groups(1).Value)
                                tDownloadURL = tDownloadURL.Replace("720p", prevQual)
                                trailer.WebURL = tDownloadURL
                                tDownloadURL = tDownloadURL.Replace("1080p", "h1080p")
                                tDownloadURL = tDownloadURL.Replace("720p", "h720p")
                                tDownloadURL = tDownloadURL.Replace("480p", "h480p")
                                trailer.URL = tDownloadURL
                                Select Case prevQual
                                    Case "1080p"
                                        trailer.Quality = Enums.TrailerVideoQuality.HD1080p
                                    Case "720p"
                                        trailer.Quality = Enums.TrailerVideoQuality.HD720p
                                    Case "480p"
                                        trailer.Quality = Enums.TrailerVideoQuality.HQ480p
                                End Select
                            End If
                        Next
                    End If
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return TrailerLinks

        End Function

#End Region 'Methods

    End Class

    Public Class VideoLinkItem

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As Enums.TrailerVideoCodec
        Private _FormatQuality As Enums.TrailerVideoQuality
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

        Friend Property FormatCodec() As Enums.TrailerVideoCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerVideoCodec)
                _FormatCodec = value
            End Set
        End Property

        Friend Property FormatQuality() As Enums.TrailerVideoQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class TrailerLinkItem

#Region "Fields"

        Private _Description As String
        Private _FormatCodec As Enums.TrailerVideoCodec
        Private _FormatQuality As Enums.TrailerVideoQuality
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

        Friend Property FormatCodec() As Enums.TrailerVideoCodec
            Get
                Return _FormatCodec
            End Get
            Set(ByVal value As Enums.TrailerVideoCodec)
                _FormatCodec = value
            End Set
        End Property

        Friend Property FormatQuality() As Enums.TrailerVideoQuality
            Get
                Return _FormatQuality
            End Get
            Set(ByVal value As Enums.TrailerVideoQuality)
                _FormatQuality = value
            End Set
        End Property

#End Region 'Properties

    End Class

    Public Class VideoLinkItemCollection
        Inherits Generic.SortedList(Of Enums.TrailerVideoQuality, VideoLinkItem)

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

    Public Class TrailerLinkItemCollection
        Inherits Generic.SortedList(Of Enums.TrailerVideoQuality, TrailerLinkItem)

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
        Public Shadows Sub Add(ByVal Link As TrailerLinkItem)
            If Link IsNot Nothing AndAlso Not MyBase.ContainsKey(Link.FormatQuality) Then
                MyBase.Add(Link.FormatQuality, Link)
            End If

        End Sub

#End Region 'Methods

    End Class

End Namespace
