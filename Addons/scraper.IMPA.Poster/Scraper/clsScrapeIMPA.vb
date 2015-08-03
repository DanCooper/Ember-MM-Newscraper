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
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports NLog
Imports EmberAPI
Imports System.Diagnostics

Namespace IMPA

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Events"

#End Region 'Events

#Region "Methods"

        Public Function GetIMPAPosters(ByVal imdbID As String) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer
            Dim ParentID As String
            Dim oV As String = String.Empty

            Try
                Dim sURL As String = GetLink(imdbID)

                If Not String.IsNullOrEmpty(sURL) Then

                    Dim sHTTP As New HTTP
                    Dim HTML As String = sHTTP.DownloadData(sURL)

                    If HTML.Contains("equiv=""REFRESH""") Then
                        Dim newURL As String = Regex.Match(HTML, "URL=..*html").ToString
                        sURL = String.Concat("http://www.impawards.com", newURL.Replace("URL=..", String.Empty))
                        HTML = sHTTP.DownloadData(sURL)
                    End If

                    sHTTP = Nothing

                    Dim mcPoster As MatchCollection = Regex.Matches(HTML, "(thumbs/imp_([^>]*ver[^>]*.jpg))|(thumbs/imp_([^>]*.jpg))")

                    Dim ThumbURL As String

                    For Each mPoster As Match In mcPoster

                        ThumbURL = String.Format("{0}/{1}", sURL.Substring(0, sURL.LastIndexOf("/")), mPoster.Value.ToString())
                        ParentID = mPoster.Value
                        If Not ParentID = oV Then

                            Dim PosterURL = String.Format("{0}/{1}", sURL.Substring(0, sURL.LastIndexOf("/")), mPoster.Value.ToString()).Replace("thumbs", "posters").Replace("imp_", String.Empty)
                            alContainer.MainPosters.Add(New MediaContainers.Image With {.URL = PosterURL, .ThumbURL = ThumbURL, .Height = "n/a", .Width = "n/a"})
                            oV = ParentID
                        End If
                    Next
                End If
            Catch ex As Exception
                logger.Error("GetIMPAPosters", ex)
            End Try

            Return alContainer
        End Function

        Private Function GetLink(ByVal IMDBID As String) As String
            Try

                Dim sHTTP As New HTTP
                Dim sPoster As String
                Dim sURLRequest As HttpWebRequest

                Dim HTML As String = sHTTP.DownloadData(String.Concat("http://", Master.eSettings.MovieIMDBURL, "/title/tt", IMDBID, "/posters"))
                sHTTP = Nothing

                Dim mcIMPA As MatchCollection = Regex.Matches(HTML, "/offsite.*impawards.*""")
                If mcIMPA.Count > 0 Then
                    sPoster = mcIMPA(0).Value.ToString
                    sPoster = sPoster.Replace("""", String.Empty)
                    sPoster = String.Concat("http://", Master.eSettings.MovieIMDBURL, sPoster)
                    sURLRequest = DirectCast(WebRequest.Create(sPoster), HttpWebRequest)
                    Using sURLResponse As HttpWebResponse = DirectCast(sURLRequest.GetResponse(), HttpWebResponse)
                        Return sURLResponse.ResponseUri.ToString
                    End Using
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name,ex)
                Return String.Empty
            End Try
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String
            Dim sType As String

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultList As List(Of MediaContainers.Image)

#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

