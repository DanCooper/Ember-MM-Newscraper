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

Imports NLog
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("core.languages.scrapers")>
Public Class XmlScraperLanguages

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private Const _FileName As String = "core.languages.scrapers.xml"
    Private _UserFile As String = Path.Combine(Master.SettingsPath, _FileName)
    Private _DefaultFile As String = FileUtils.Common.ReturnSettingsFile("Defaults", _FileName)

#End Region 'Fields

#Region "Properties"

    <XmlElement("language")>
    Public Property Languages() As List(Of LanguageProperty) = New List(Of LanguageProperty)

#End Region 'Properties

#Region "Methods"

    Private Function CopyDefaultFile() As Boolean
        Try
            File.Copy(_DefaultFile, _UserFile, True)
            Return True
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return False
    End Function

    Public Sub Load()
        If File.Exists(_UserFile) Then
            Dim objStreamReader = New StreamReader(_UserFile)
            Try
                Languages = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), XmlScraperLanguages).Languages
                objStreamReader.Close()
                Languages.Sort()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                objStreamReader.Close()
                FileUtils.Common.CreateFileBackup(_UserFile)
                File.Delete(_UserFile)
                If CopyDefaultFile() Then Load()
            End Try
        ElseIf SearchOlderVersions() Then
            Load()
        Else
            If CopyDefaultFile() Then Load()
        End If
        If Languages.Count = 0 Then
            CopyDefaultFile()
            Load()
        End If
    End Sub

    Public Sub Save()
        Dim xmlSerial As New XmlSerializer([GetType])
        Dim xmlWriter As New StreamWriter(_UserFile)
        xmlSerial.Serialize(xmlWriter, Me)
        xmlWriter.Close()
    End Sub

    Private Function SearchOlderVersions() As Boolean
#Disable Warning BC40000 'The type or member is obsolete.
        Dim strVersion1 = Path.Combine(Master.SettingsPath, "Core.ScraperLanguages.xml")
        Select Case True
            Case File.Exists(strVersion1)
                Dim objStreamReader = New StreamReader(strVersion1)
                Try
                    Languages = CType(New XmlSerializer(GetType(XMLScraperLanguagesOld)).Deserialize(objStreamReader), XMLScraperLanguagesOld).Languages
                    objStreamReader.Close()
                    Save()
                    File.Delete(strVersion1)
                    Return True
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    objStreamReader.Close()
                    FileUtils.Common.CreateFileBackup(strVersion1)
                    File.Delete(strVersion1)
                    Return False
                End Try
            Case Else
                Return False
        End Select
#Enable Warning BC40000 'The type or member is obsolete.
    End Function

#End Region 'Methods

#Region "Nested Types"

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

#End Region 'Nested Types

End Class

<XmlRoot("core.scraperlanguages")>
<Obsolete("This class is only to load old versions of this XML")>
Public Class XMLScraperLanguagesOld

#Region "Properties"

    <XmlElement("language")>
    Public Property Languages() As List(Of XmlScraperLanguages.LanguageProperty) = New List(Of XmlScraperLanguages.LanguageProperty)

#End Region 'Properties

End Class