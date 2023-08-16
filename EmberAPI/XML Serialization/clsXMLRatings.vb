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

Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("ratings")>
Public Class XmlRatings

#Region "Properties"

    <XmlArray("movies")>
    <XmlArrayItem("name")>
    Public Property Movies() As List(Of RatingProperty) = New List(Of RatingProperty)

    <XmlArray("tv")>
    <XmlArrayItem("name")>
    Public Property TV() As List(Of RatingProperty) = New List(Of RatingProperty)

#End Region 'Properties

End Class

<Serializable()>
Public Class RatingProperty

#Region "Properties"

    <XmlElement("country")>
    Public Property Country() As String = String.Empty

    <XmlElement("icon")>
    Public Property Icon() As String = String.Empty

    <XmlAttribute("searchstring")>
    Public Property Searchstring() As String = String.Empty

#End Region 'Properties

End Class