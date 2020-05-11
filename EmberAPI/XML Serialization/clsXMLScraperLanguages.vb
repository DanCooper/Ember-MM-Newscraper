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
Public Class clsXMLScraperLanguages

#Region "Properties"

    <XmlElement("language")>
    Public Property Languages() As List(Of LanguageProperty) = New List(Of LanguageProperty)

#End Region 'Properties

#Region "Methods"

    Public Sub Save()
        Sort()
        Dim xmlSerial As New XmlSerializer(GetType(clsXMLScraperLanguages))
        Dim xmlWriter As New StreamWriter(Path.Combine(Master.SettingsPath, "Core.ScraperLanguages.xml"))
        xmlSerial.Serialize(xmlWriter, APIXML.ScraperLanguagesXML)
        xmlWriter.Close()
    End Sub

    Public Sub Sort()
        Languages.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class LanguageProperty
    Implements IComparable(Of LanguageProperty)

#Region "Properties"

    <XmlElement("name")>
    Public Property Name() As String = String.Empty

    <XmlElement("abbreviation")>
    Public Property Abbreviation() As String = String.Empty

    <XmlIgnore()>
    Public ReadOnly Property Description() As String
        Get
            Return String.Format("{0} ({1})", Name, Abbreviation)
        End Get
    End Property

    <XmlIgnore()>
    Public ReadOnly Property Abbrevation_MainLanguage() As String
        Get
            Return Regex.Replace(Abbreviation, "-.*", String.Empty).Trim
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Function CompareTo(ByVal other As LanguageProperty) As Integer _
        Implements IComparable(Of LanguageProperty).CompareTo
        Try
            Dim retVal As Integer = (Name).CompareTo(other.Name)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class