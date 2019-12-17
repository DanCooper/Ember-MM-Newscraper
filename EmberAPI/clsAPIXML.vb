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

Public Class APIXML

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private Shared _ScraperLanguagesXML As New XMLScraperLanguages

#End Region 'Fields

#Region "Properties"

    Public Shared Property CertificationLanguages As XMLCertLanguages = New XMLCertLanguages

    Public Shared Property GenreMapping As XMLGenres = New XMLGenres

    Public Shared ReadOnly Property ScraperLanguages As XMLScraperLanguages
        Get
            Return _ScraperLanguagesXML
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Shared Sub CacheXMLs()
        Dim objStreamReader As StreamReader
        Try
            'Certifications languages
            Dim strCertificationLanguagesXMLPath As String = Path.Combine(Master.SettingsPath, "CertLanguages.xml")
            If File.Exists(strCertificationLanguagesXMLPath) Then
                objStreamReader = New StreamReader(strCertificationLanguagesXMLPath)
                Dim xCert As New XmlSerializer(CertificationLanguages.GetType)

                CertificationLanguages = CType(xCert.Deserialize(objStreamReader), XMLCertLanguages)
                objStreamReader.Close()
            Else
                Dim cPathD As String = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultCertLanguages.xml")
                objStreamReader = New StreamReader(cPathD)
                Dim xCert As New XmlSerializer(CertificationLanguages.GetType)

                CertificationLanguages = CType(xCert.Deserialize(objStreamReader), XMLCertLanguages)
                objStreamReader.Close()

                Try
                    File.Copy(cPathD, strCertificationLanguagesXMLPath)
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If

            'Genre mappings
            Dim strGenreMappingsXMLPath As String = Path.Combine(Master.SettingsPath, "Core.Genres.xml")
            If File.Exists(strGenreMappingsXMLPath) Then
                objStreamReader = New StreamReader(strGenreMappingsXMLPath)
                Dim xGenres As New XmlSerializer(GenreMapping.GetType)

                GenreMapping = CType(xGenres.Deserialize(objStreamReader), XMLGenres)
                objStreamReader.Close()
            End If

            'Scraper languages
            Dim strScraperLanguagesXMLPath As String = Path.Combine(Master.SettingsPath, "Core.ScraperLanguages.xml")
            If File.Exists(strScraperLanguagesXMLPath) Then
                objStreamReader = New StreamReader(strScraperLanguagesXMLPath)
                Dim xLang As New XmlSerializer(_ScraperLanguagesXML.GetType)

                _ScraperLanguagesXML = CType(xLang.Deserialize(objStreamReader), XMLScraperLanguages)
                objStreamReader.Close()
            Else
                Dim slPathD As String = FileUtils.Common.ReturnSettingsFile("Defaults", "Core.ScraperLanguages.xml")
                objStreamReader = New StreamReader(slPathD)
                Dim xLang As New XmlSerializer(_ScraperLanguagesXML.GetType)

                _ScraperLanguagesXML = CType(xLang.Deserialize(objStreamReader), XMLScraperLanguages)
                objStreamReader.Close()
                _ScraperLanguagesXML.Save()
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function GetGenreList() As String()
        Dim retGenre As New List(Of String)
        For Each mGenre In GenreMapping.Genres
            retGenre.Add(mGenre.Name)
        Next
        retGenre.Sort()
        Return retGenre.ToArray
    End Function

    Public Shared Function XMLToLowerCase(ByVal sXML As String) As String
        Dim sMatches As MatchCollection = Regex.Matches(sXML, "\<(.*?)\>", RegexOptions.IgnoreCase)
        For Each sMatch As Match In sMatches
            sXML = sXML.Replace(sMatch.Groups(1).Value, sMatch.Groups(1).Value.ToLower)
        Next
        Return sXML
    End Function

#End Region 'Methods

End Class