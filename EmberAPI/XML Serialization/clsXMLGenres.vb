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
<XmlRoot("core.genres")>
Public Class clsXMLGenres

#Region "Fields"

    Private _defaultimage As String
    Private _genres As New List(Of genreProperty)
    Private _mappingtable As New List(Of genreMapping)

#End Region 'Fields

#Region "Properties"

    <XmlElement("defaultimage")>
    Public Property DefaultImage() As String
        Get
            Return _defaultimage
        End Get
        Set(ByVal value As String)
            _defaultimage = value
        End Set
    End Property

    <XmlElement("genres")>
    Public Property Genres() As List(Of genreProperty)
        Get
            Return _genres
        End Get
        Set(ByVal value As List(Of genreProperty))
            _genres = value
        End Set
    End Property

    <XmlElement("mappingtable")>
    Public Property MappingTable() As List(Of genreMapping)
        Get
            Return _mappingtable
        End Get
        Set(ByVal value As List(Of genreMapping))
            _mappingtable = value
        End Set
    End Property

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Clear()
        _defaultimage = "default.jpg"
        _genres.Clear()
        _mappingtable.Clear()
    End Sub

#End Region 'Methods


End Class

<Serializable()>
Public Class genreMapping


#Region "Fields"

    Private _mappings As New List(Of String)
    Private _searchstring As String

#End Region 'Fields

#Region "Properties"

    <XmlElement("searchstring")>
    Public Property SearchString() As String
        Get
            Return _searchstring
        End Get
        Set(ByVal value As String)
            _searchstring = value
        End Set
    End Property

    <XmlElement("mappings")>
    Public Property Mappings() As List(Of String)
        Get
            Return _mappings
        End Get
        Set(ByVal value As List(Of String))
            _mappings = value
        End Set
    End Property

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Clear()
        _mappings.Clear()
        _searchstring = String.Empty
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class genreProperty

#Region "Fields"

    Private _image As String
    Private _isnew As Boolean
    Private _name As String

#End Region 'Fields

#Region "Properties"

    <XmlElement("name")>
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    <XmlElement("image")>
    Public Property Image() As String
        Get
            Return _image
        End Get
        Set(ByVal value As String)
            _image = value
        End Set
    End Property

    <XmlElement("isnew")>
    Public Property isNew() As Boolean
        Get
            Return _isnew
        End Get
        Set(ByVal value As Boolean)
            _isnew = value
        End Set
    End Property

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Clear()
        _image = String.Empty
        _name = String.Empty
    End Sub

#End Region 'Methods

End Class