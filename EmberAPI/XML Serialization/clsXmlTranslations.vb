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

Public Class clsXmlTranslations

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Translations As List(Of TranslationProperty) = New List(Of TranslationProperty)

#End Region 'Properties

#Region "Constructors"

    Public Sub New()
        For Each file In Directory.GetFiles(Path.Combine(Directory.GetParent(Reflection.Assembly.GetCallingAssembly().Location).FullName, "Translations"))
            Translations.Add(New TranslationProperty(New FileInfo(file)))
        Next
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Function Abort() As String
        Return Master.eLang.GetString(1171, "Abort")
    End Function


    Public Function All() As String
        Return String.Format("[{0}]", Master.eLang.GetString(569, "All"))
    End Function

    Public Function Apply() As String
        Return Master.eLang.GetString(276, "Apply")
    End Function

    Public Function Cancel() As String
        Return Master.eLang.GetString(167, "Cancel")
    End Function

    Public Function Close() As String
        Return Master.eLang.GetString(19, "Close")
    End Function

    Public Function Disabled() As String
        Return Master.eLang.GetString(571, "[Disabled]")
    End Function

    Public Function None() As String
        Return Master.eLang.GetString(570, "[none]")
    End Function

    Public Function OK() As String
        Return Master.eLang.GetString(179, "OK")
    End Function

    Public Function Skip() As String
        Return Master.eLang.GetString(1228, "Skip")
    End Function

    Public Function GetString(ByVal id As UInteger, ByVal defaultValue As String) As String
        Dim translation = Translations.FirstOrDefault(Function(f) f.Language = Master.eSettings.GeneralLanguage)
        If translation IsNot Nothing Then
            Dim str = translation.Strings.FirstOrDefault(Function(f) f.Id = id)
            If str IsNot Nothing Then Return str.Value
        End If
        Return defaultValue
    End Function

#End Region 'Methods

#Region "Nested Types"

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