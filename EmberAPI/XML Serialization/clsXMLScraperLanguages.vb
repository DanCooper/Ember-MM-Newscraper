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
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization


<Serializable()>
<XmlRoot("core.scraperlanguages")>
Public Class XMLScraperLanguages

#Region "Fields"

    Private _languages As New List(Of languageProperty)

#End Region 'Fields

#Region "Properties"

    <XmlElement("language")>
    Public Property Languages() As List(Of languageProperty)
        Get
            Return _languages
        End Get
        Set(ByVal value As List(Of languageProperty))
            _languages = value
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
        _languages.Clear()
    End Sub

    Public Sub Save()
        Sort()
        Dim xmlSerial As New XmlSerializer(GetType(XMLScraperLanguages))
        Dim xmlWriter As New StreamWriter(Path.Combine(Master.SettingsPath, "Core.ScraperLanguages.xml"))
        xmlSerial.Serialize(xmlWriter, APIXML.ScraperLanguagesXML)
        xmlWriter.Close()
    End Sub

    Public Sub Sort()
        _languages.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class languageProperty
    Implements IComparable(Of languageProperty)

#Region "Fields"

    Private _abbreviation As String
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

    <XmlElement("abbreviation")>
    Public Property Abbreviation() As String
        Get
            Return _abbreviation
        End Get
        Set(ByVal value As String)
            _abbreviation = value
        End Set
    End Property

    <XmlIgnore()>
    Public ReadOnly Property Description() As String
        Get
            Return String.Format("{0} ({1})", _name, _abbreviation)
        End Get
    End Property

    <XmlIgnore()>
    Public ReadOnly Property Abbrevation_MainLanguage() As String
        Get
            Return Regex.Replace(_abbreviation, "-.*", String.Empty).Trim
        End Get
    End Property

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        Clear()
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Clear()
        _abbreviation = String.Empty
        _name = String.Empty
    End Sub

    Public Function CompareTo(ByVal other As languageProperty) As Integer _
        Implements IComparable(Of languageProperty).CompareTo
        Try
            Dim retVal As Integer = (Name).CompareTo(other.Name)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class

