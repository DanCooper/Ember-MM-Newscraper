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
Imports NLog

Namespace IMDB

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
#End Region 'Fields

#Region "Events"


#End Region 'Events

#Region "Methods"

        Public Function GetIMDBPosters(ByVal imdbID As String) As MediaContainers.ImagesContainer_Movie_MovieSet
            Dim alContainer As New MediaContainers.ImagesContainer_Movie_MovieSet
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
                    'Dim sUrl1 As String = sHTTP.DownloadData(mcIMDB(0).Value)
                    mcIMDB = Regex.Matches(HTML, "http://ia.media-imdb.com/images/.{3,80}?.jpg")
                    If mcIMDB.Count > 0 Then
                        'just use the first one if more are found
                        aStr = mcIMDB(0).Value.Substring(mcIMDB(0).Value.LastIndexOf("/") + 1, mcIMDB(0).Value.Length - (mcIMDB(0).Value.LastIndexOf("/") + 1))
                        aPar = aStr.Split(","c)
                        'URLs can now look like this as well:
                        'http://ia.media-imdb.com/images/M/MV5BMTI5MTgxMzIzMl5BMl5BanBnXkFtZTcwNDA5MTYyMQ@@._V1_SY295_SX197_.jpg
                        If aPar.Length = 1 Then
                            aPar2 = aPar(0).Split("."c)
                            aParentID = aPar2(0)
                            Dim mSYSX As Match = Regex.Match(aPar(0), "\._V\d+?_SY(\d+?)_SX(\d+?)_")
                            If mSYSX.Success Then
                                alContainer.Posters.Add(New MediaContainers.Image With {.URL = mcIMDB(0).Value, .Width = mSYSX.Groups(2).Value, .Height = mSYSX.Groups(1).Value})
                            Else
                                logger.Error("Unknown IMDB Poster URL")
                            End If
                        Else
                            aPar2 = aPar(0).Split("."c)
                            aParentID = aPar2(0)
                            aPar(3) = aPar(3).Substring(0, aPar(3).LastIndexOf("_"))
                            alContainer.Posters.Add(New MediaContainers.Image With {.URL = mcIMDB(0).Value, .Width = aPar(2), .Height = aPar(3)})
                        End If
                    End If
                    'The URLs can contain SY or SX in them like this:
                    'http://ia.media-imdb.com/images/M/MV5BMTk4OTQ1OTMwN15BMl5BanBnXkFtZTcwOTIwMzM3MQ@@._V1_SX214_CR0,0,214,317_.jpg
                    'or 
                    'http://ia.media-imdb.com/images/M/MV5BMjE3NDY0OTIwNl5BMl5BanBnXkFtZTcwMTY4MTU1MQ@@._V1_SY317_CR4,0,214,317_.jpg
                    Dim aSP As String() = Regex.Split(mcIMDB(0).Value, "._V\d+?_S(?:X|Y)\d+?_CR\d+?,\d+?,\d+?,\d+?_")
                    If aSP.Length > 1 Then
                        Dim sUrl1 = aSP(0) + aSP(1)
                        alContainer.Posters.Add(New MediaContainers.Image With {.URL = sUrl1, .Width = "n/a", .Height = "n/a"})
                    End If
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

