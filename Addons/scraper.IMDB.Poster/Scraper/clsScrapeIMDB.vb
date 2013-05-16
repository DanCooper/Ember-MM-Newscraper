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
Imports System.Diagnostics
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI

Namespace IMDBg

    Public Class Scraper

#Region "Fields"

#End Region 'Fields

#Region "Events"


#End Region 'Events

#Region "Methods"

        Public Function GetIMDBPosters(ByVal imdbID As String) As List(Of MediaContainers.Image)
            Dim alPoster As New List(Of MediaContainers.Image)
            Dim aParentID As String = String.Empty

            Try
                Dim sHTTP As New HTTP
                Dim aStr As String = String.Empty
                Dim aPar As String()
                Dim aPar2 As String()
                Dim HTML As String = sHTTP.DownloadData(String.Concat("http://www.imdb.com/title/tt", imdbID, ""))
                sHTTP = Nothing

                ' check existence of a line like this
                '      <a href="/media/rm2995297536/tt0089218?ref_=tt_ov_i" > <img height="317"
                ' and then return this one
                '      src = "http://ia.media-imdb.com/images/M/MV5BMTY1Mzk3MTg0M15BMl5BanBnXkFtZTcwOTQzODYyMQ@@._V1_SY317_CR3,0,214,317_.jpg"
                Dim mcIMDB As MatchCollection = Regex.Matches(HTML, String.Concat("/media/[a-zA-Z0-9]{3,12}/tt", imdbID, "\?ref_=tt_ov_i"), RegexOptions.IgnoreCase)
                If mcIMDB.Count > 0 Then
                    'Debug.Print("GetIMDBPoster 1 - {0}", mcIMDB(0).Value)
                    'Dim sUrl1 As String = sHTTP.DownloadData(mcIMDB(0).Value)
                    mcIMDB = Regex.Matches(HTML, "http://ia.media-imdb.com/images/.{3,80}?.jpg")
                    If mcIMDB.Count > 0 Then
                        'just use the first one if more are found
                        'Debug.Print("GetIMDBPoster 2 - {0}", mcIMDB(0).Value)
                        aStr = mcIMDB(0).Value.Substring(mcIMDB(0).Value.LastIndexOf("/") + 1, mcIMDB(0).Value.Length - (mcIMDB(0).Value.LastIndexOf("/") + 1))
                        aPar = Split(aStr, ",")
                        aPar2 = Split(aPar(0), ".")
                        aParentID = aPar2(0)
                        aPar(3) = aPar(3).Substring(0, aPar(3).LastIndexOf("_"))
                        alPoster.Add(New MediaContainers.Image With {.Description = Master.eSize.poster_names(0).description, .URL = mcIMDB(0).Value, .Width = aPar(2), .Height = aPar(3), .ParentID = aParentID})
                    End If
                    Dim aSP As String() = Regex.Split(mcIMDB(0).Value, "._V\d+?_SY\d+?_CR\d+?,\d+?,\d+?,\d+?_")
                    Dim sUrl1 = aSP(0) + aSP(1)
                    'Debug.Print("GetIMDBPoster 3 - {0}", sUrl1)
                    alPoster.Add(New MediaContainers.Image With {.Description = Master.eSize.poster_names(5).description, .URL = sUrl1, .Width = "n/a", .Height = "n/a", .ParentID = aParentID})
                End If

            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try

            Return alPoster
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

