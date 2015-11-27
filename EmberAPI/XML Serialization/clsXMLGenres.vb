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
Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("core.genres")>
Public Class clsXMLGenres
    Implements ICloneable

#Region "Fields"

    Private _defaultimage As String
    Private _genres As New List(Of genreProperty)
    Private _mappings As New List(Of genreMapping)

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

    <XmlElement("genre")>
    Public Property Genres() As List(Of genreProperty)
        Get
            Return _genres
        End Get
        Set(ByVal value As List(Of genreProperty))
            _genres = value
        End Set
    End Property

    <XmlElement("mapping")>
    Public Property Mappings() As List(Of genreMapping)
        Get
            Return _mappings
        End Get
        Set(ByVal value As List(Of genreMapping))
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
        _defaultimage = "default.jpg"
        _genres.Clear()
        _mappings.Clear()
    End Sub

    Public Function CloneDeep() As Object _
        Implements ICloneable.Clone
        ' Gibt eine vollständige Kopie dieses Objekts zurück. 
        ' Voraussetzung ist die Serialisierbarkeit aller beteiligten 
        ' Objekte. 
        Dim Stream As New MemoryStream(50000)
        Dim Formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        ' Serialisierung über alle Objekte hinweg in einen Stream 
        Formatter.Serialize(Stream, Me)
        ' Zurück zum Anfang des Streams und... 
        Stream.Seek(0, SeekOrigin.Begin)
        ' ...aus dem Stream in ein Objekt deserialisieren 
        CloneDeep = Formatter.Deserialize(Stream)
        Stream.Close()
    End Function

    Public Sub Save()
        Sort()
        Dim xmlSerial As New XmlSerializer(GetType(clsXMLGenres))
        Dim xmlWriter As New StreamWriter(Path.Combine(Master.SettingsPath, "Core.Genres.xml"))
        xmlSerial.Serialize(xmlWriter, APIXML.GenreXML)
        xmlWriter.Close()
    End Sub

    Public Sub Sort()
        _genres.Sort()
        _mappings.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class genreMapping
    Implements IComparable(Of genreMapping)


#Region "Fields"

    Private _isnew As Boolean
    Private _mappedto As New List(Of String)
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

    <XmlElement("mappedto")>
    Public Property MappedTo() As List(Of String)
        Get
            Return _mappedto
        End Get
        Set(ByVal value As List(Of String))
            _mappedto = value
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
        _isnew = True
        _mappedto.Clear()
        _searchstring = String.Empty
    End Sub

    Public Function CompareTo(ByVal other As genreMapping) As Integer _
        Implements IComparable(Of genreMapping).CompareTo
        Try
            Dim retVal As Integer = (SearchString).CompareTo(other.SearchString)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class

<Serializable()>
Public Class genreProperty
    Implements IComparable(Of genreProperty)

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
        _isnew = True
        _name = String.Empty
    End Sub

    Public Function CompareTo(ByVal other As genreProperty) As Integer _
        Implements IComparable(Of genreProperty).CompareTo
        Try
            Dim retVal As Integer = (Name).CompareTo(other.Name)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class