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

#Region "Properties"

    <XmlElement("defaultimage")>
    Public Property DefaultImage() As String = "default.jpg"

    <XmlElement("genre")>
    Public Property Genres() As List(Of genreProperty) = New List(Of genreProperty)

    <XmlElement("mapping")>
    Public Property Mappings() As List(Of genreMapping) = New List(Of genreMapping)

#End Region 'Properties

#Region "Methods"

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
        Genres.Sort()
        Mappings.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class genreMapping
    Implements IComparable(Of genreMapping)

#Region "Properties"

    <XmlElement("searchstring")>
    Public Property SearchString() As String = String.Empty

    <XmlElement("mappedto")>
    Public Property MappedTo() As List(Of String) = New List(Of String)

    <XmlElement("isnew")>
    Public Property isNew() As Boolean = True

#End Region 'Properties 

#Region "Methods"

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

#Region "Properties"

    <XmlElement("name")>
    Public Property Name() As String = String.Empty

    <XmlElement("image")>
    Public Property Image() As String = String.Empty

    <XmlElement("isnew")>
    Public Property isNew() As Boolean = True

#End Region 'Properties 

#Region "Methods"

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