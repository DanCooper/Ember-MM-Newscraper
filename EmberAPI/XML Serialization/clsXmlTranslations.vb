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
Imports System.Xml.Serialization

Public Class XmlTranslations

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"
    ''' <summary>
    ''' Provides common used words translated by main translation file like "OK", "Cancel" and so on
    ''' </summary>
    Public ReadOnly Property CommonWordsList As New CommonWords

    Public ReadOnly Property Translations As List(Of TranslationProperty) = New List(Of TranslationProperty)
    ''' <summary>
    ''' Provides words used in scrapers and scraper settings panels translated by main translation file like "Actor", "Studio" and so on
    ''' </summary>
    Public ReadOnly Property ScraperWordsList As New ScraperWords

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        For Each file In Directory.GetFiles(Path.Combine(Directory.GetParent(Reflection.Assembly.GetCallingAssembly().Location).FullName, "translations"))
            Translations.Add(New TranslationProperty(New FileInfo(file)))
        Next
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Function GetString(ByVal id As UInteger, ByVal defaultValue As String) As String
        Dim translation = Translations.FirstOrDefault(Function(f) f.Language = Master.eSettings.GeneralLanguage)
        If translation IsNot Nothing Then
            Dim str = translation.Strings.FirstOrDefault(Function(f) f.Id = id)
            If str IsNot Nothing AndAlso Not String.IsNullOrEmpty(str.Value) Then Return str.Value
        End If
        Return defaultValue
    End Function

#End Region 'Methods

#Region "Nested Types"
    ''' <summary>
    ''' Provides common used words translated by main translation file like "OK", "Cancel" and so on
    ''' </summary>
    Public Class CommonWords

#Region "Methods"

        Public Function Abort() As String
            Return Master.eLang.GetString(1171, "Abort")
        End Function

        Public Function Add() As String
            Return Master.eLang.GetString(28, "Add")
        End Function

        Public Function After_Edit() As String
            Return Master.eLang.GetString(1054, "After Edit")
        End Function

        Public Function All() As String
            Return String.Format("[{0}]", Master.eLang.GetString(569, "All"))
        End Function

        Public Function Apply() As String
            Return Master.eLang.GetString(276, "Apply")
        End Function

        Public Function Before_Edit() As String
            Return Master.eLang.GetString(1055, "Before Edit")
        End Function

        Public Function Cancel() As String
            Return Master.eLang.GetString(167, "Cancel")
        End Function

        Public Function Cancelled() As String
            Return Master.eLang.GetString(396, "Cancelled")
        End Function

        Public Function Close() As String
            Return Master.eLang.GetString(19, "Close")
        End Function

        Public Function During_Multi_Scraping() As String
            Return Master.eLang.GetString(1056, "During Multi-Scraping")
        End Function

        Public Function During_Single_Scraping() As String
            Return Master.eLang.GetString(1057, "During Single-Scraping")
        End Function

        Public Function Disabled() As String
            Return Master.eLang.GetString(571, "[Disabled]")
        End Function

        Public Function Done() As String
            Return Master.eLang.GetString(362, "Done")
        End Function

        Public Function Edit() As String
            Return Master.eLang.GetString(1440, "Edit")
        End Function

        Public Function Enabled() As String
            Return Master.eLang.GetString(774, "Enabled")
        End Function

        Public Function Episodes() As String
            Return Master.eLang.GetString(682, "Episodes")
        End Function

        Public Function [Error]() As String
            Return Master.eLang.GetString(1134, "Error")
        End Function

        Public Function Name() As String
            Return Master.eLang.GetString(232, "Name")
        End Function

        Public Function None() As String
            Return Master.eLang.GetString(570, "[none]")
        End Function

        Public Function Movies() As String
            Return Master.eLang.GetString(36, "Movies")
        End Function

        Public Function OK() As String
            Return Master.eLang.GetString(179, "OK")
        End Function

        Public Function Password() As String
            Return Master.eLang.GetString(426, "Password")
        End Function

        Public Function Remove() As String
            Return Master.eLang.GetString(30, "Remove")
        End Function

        Public Function Settings() As String
            Return Master.eLang.GetString(420, "Settings")
        End Function

        Public Function Skip() As String
            Return Master.eLang.GetString(1228, "Skip")
        End Function

        Public Function TV_Shows() As String
            Return Master.eLang.GetString(653, "TV Shows")
        End Function

        Public Function Username() As String
            Return Master.eLang.GetString(425, "Username")
        End Function

        Public Function Warning() As String
            Return Master.eLang.GetString(356, "Warning")
        End Function

#End Region 'Methods

    End Class
    ''' <summary>
    ''' Provides words used in scrapers and scraper settings panels translated by main translation file like "Actor", "Studio" and so on
    ''' </summary>
    Public Class ScraperWords

        Public Function Actors() As String
            Return Master.eLang.GetString(231, "Actors")
        End Function

        Public Function Certifications() As String
            Return Master.eLang.GetString(56, "Certifications")
        End Function

        Public Function Countries() As String
            Return Master.eLang.GetString(237, "Countries")
        End Function

        Public Function Creators() As String
            Return Master.eLang.GetString(744, "Creators")
        End Function

        Public Function Genres() As String
            Return Master.eLang.GetString(725, "Genres")
        End Function

        Public Function Original_Title() As String
            Return Master.eLang.GetString(302, "Original Title")
        End Function

        Public Function Plot() As String
            Return Master.eLang.GetString(65, "Plot")
        End Function

        Public Function Plot_Outline() As String
            Return Master.eLang.GetString(64, "Plot Outline")
        End Function

        Public Function Premiered() As String
            Return Master.eLang.GetString(724, "Premiered")
        End Function

        Public Function Scraper_Fields_Scraper_Specific() As String
            Return Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        End Function

        Public Function Scraper_Options() As String
            Return Master.eLang.GetString(1186, "Scraper Options")
        End Function

    End Class

    <Serializable()>
    <XmlRoot("resources")>
    Public Class TranslationProperty

#Region "Properties"

        <XmlIgnore()>
        Public Property Description As String = String.Empty

        <XmlIgnore()>
        Public Property Language As String = String.Empty

        <XmlElement("string")>
        Public Property Strings As List(Of TranslationItemProperty) = New List(Of TranslationItemProperty)

#End Region 'Properties

#Region "Constructors"
        ''' <summary>
        ''' Needed for Load Sub
        ''' </summary>
        Private Sub New()
            Return
        End Sub

        Public Sub New(ByVal file As FileInfo)
            If Not file.Exists Then Return
            Language = Path.GetFileNameWithoutExtension(file.Name)
            Dim language_alpha2 = Path.GetFileNameWithoutExtension(file.Name).Substring(0, 2)
            Dim country_alpha2 = Path.GetFileNameWithoutExtension(file.Name).Substring(3, 2)
            If language_alpha2 IsNot Nothing AndAlso country_alpha2 IsNot Nothing Then
                Dim language_shortName = Localization.Languages.Get_Name_By_Alpha2(language_alpha2)
                Dim country_shortName = Localization.Countries.Get_Name_By_Alpha2(country_alpha2)
                If language_shortName IsNot Nothing AndAlso country_shortName IsNot Nothing Then
                    Description = String.Format("{0} ({1}) ({2})", language_shortName, country_shortName, Path.GetFileNameWithoutExtension(file.Name))
                Else
                    Description = String.Format("{0} ({1})", "Unknown", Path.GetFileNameWithoutExtension(file.Name))
                End If
            Else
                Description = "Unknown"
            End If
            Try
                Dim objStreamReader = New StreamReader(file.FullName)
                Strings = CType(New XmlSerializer(GetType(TranslationProperty)).Deserialize(objStreamReader), TranslationProperty).Strings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End Sub

#End Region 'Constructors

    End Class

    <Serializable()>
    Public Class TranslationItemProperty

#Region "Properties"

        <XmlAttribute("name")>
        Public Property Id As UInteger = 0

        <XmlText()>
        Public Property Value As String = String.Empty

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class