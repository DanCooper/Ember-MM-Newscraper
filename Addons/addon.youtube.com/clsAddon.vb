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

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Public Shared Function GetTrailers(ByVal Title As String) As List(Of MediaContainers.Trailer)
        Dim alTrailers As New List(Of MediaContainers.Trailer)
        alTrailers = YouTube.Scraper.SearchOnYouTube(String.Concat(Title, " ", Master.eSettings.Movie.TrailerSettings.DefaultSearchParameter))
        For Each tTrailer In alTrailers
            tTrailer.Scraper = "YouTube"
        Next
        Return alTrailers
    End Function

#End Region 'Methods 

End Class