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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Diagnostics
Imports EmberAPI
Imports NLog

Namespace MPDB

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Events"

#End Region 'Events

#Region "Methods"

        Public Function GetMPDBPosters(ByVal imdbID As String) As MediaContainers.SearchResultsContainer
            Dim alContainer As New MediaContainers.SearchResultsContainer

            Try
                Dim sHTTP As New HTTP
                Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.movieposterdb.com/movie/", imdbID))
                sHTTP = Nothing
                ' is a string match so we have not to use the alias IMDB
                If Regex.IsMatch(HTML, String.Concat("http://www.imdb.com/title/tt", imdbID), RegexOptions.Singleline Or RegexOptions.IgnoreCase Or RegexOptions.Multiline) Then
                    Dim mcPoster As MatchCollection = Regex.Matches(HTML, "http://www.movieposterdb.com/posters/[0-9_](.*?)/[0-9](.*?)/[0-9](.*?)/[a-z0-9_](.*?).jpg")

                    Dim PosterURL As String = String.Empty
                    Dim ThumbURL As String = String.Empty
                    For Each mPoster As Match In mcPoster
                        ' there are a lot of duplicates in the page.
                        Dim x = From MI As MediaContainers.Image In alContainer.MainPosters
                        If x.Count > 0 Then
                            logger.Trace("Duplicate {0} ", PosterURL)
                        Else
                            ThumbURL = mPoster.Value
                            PosterURL = mPoster.Value.Remove(mPoster.Value.LastIndexOf("/") + 1, 1)
                            PosterURL = PosterURL.Insert(mPoster.Value.LastIndexOf("/") + 1, "l")
                            ' url are like> http://www.movieposterdb.com/posters/10_08/2009/499549/l_499549_43475538.jpg
                            'the parent id is the part AFTER the l_
                            ' all poster have the same size
                            alContainer.MainPosters.Add(New MediaContainers.Image With {.URL = PosterURL, .ThumbURL = ThumbURL, .Width = "n/a", .Height = "n/a", .Scraper = "MPDB"})
                        End If
                    Next
                End If

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            Return alContainer
        End Function

#End Region 'Methods

#Region "Nested Types"

        Private Structure Arguments

#Region "Fields"

            Dim Parameter As String

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

