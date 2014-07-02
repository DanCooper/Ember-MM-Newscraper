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
Imports System.IO.IsolatedStorage
Imports System.Xml
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.Text
Imports NLog
Imports System.Runtime.Serialization

Public Class Localization

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Shared help_logger As Logger = NLog.LogManager.GetLogger("HelpString")
    Shared lang_logger As Logger = NLog.LogManager.GetLogger("LanguageString")

    Private Shared htArrayStrings As New List(Of Locs)
    Private Shared htHelpStrings As New clsXMLLanguageHelp
    Private Shared htStrings As New clsXMLLanguage
    Private Shared _ISOLanguages As New clsXMLLanguages

    Private _all As String
    Private _disabled As String
    Private _none As String

#If DEBUG Then
    Private Shared _loggingString As New Object
    Private Shared _loggingHelp As New Object
#End If

#End Region 'Fields

#Region "Constructors"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' 2013/10/31 Dekker500 - Fixed bug with missing directory separator between AppPath and Langs
    ''' </remarks>
    Public Sub New()
        Me.Clear()
        Dim lPath As String = FileUtils.Common.ReturnSettingsFile("Langs", "Languages.xml")
        If File.Exists(lPath) Then
            Dim objStreamReader As New StreamReader(lPath)

            Dim xLangsString As New XmlSerializer(_ISOLanguages.GetType)

            _ISOLanguages = CType(xLangsString.Deserialize(objStreamReader), clsXMLLanguages)
            objStreamReader.Close()
        Else
            logger.Error("Cannot find Language.xml." & vbNewLine & "Expected path: {0}", lPath)
            MsgBox(String.Concat("Cannot find Language.xml.", vbNewLine, vbNewLine, "Expected path:", vbNewLine, lPath), MsgBoxStyle.Critical, "File Not Found")
        End If
    End Sub

#End Region 'Constructors

#Region "Properties"

    Public Property All() As String
        Get
            Return _all
        End Get
        Set(ByVal value As String)
            _all = value
        End Set
    End Property

    Public Property Disabled() As String
        Get
            Return _disabled
        End Get
        Set(ByVal value As String)
            _disabled = value
        End Set
    End Property

    Public Property None() As String
        Get
            Return _none
        End Get
        Set(ByVal value As String)
            _none = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"
    ' ************************************************************************************************
    ' This are functions for country/Language codes under ISO639 Alpha-2 and Alpha-3(ie: Used by DVD/GoogleAPI)
    Shared Function ISOGetLangByCode2(ByVal code As String) As String
        Return (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Alpha2 = code))(0).Name
    End Function

    Shared Function ISOGetLangByCode3(ByVal code As String) As String
        Return (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Alpha3 = code))(0).Name
    End Function

    Public Shared Function ISOLangGetCode2ByLang(ByVal lang As String) As String
        Return (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Name = lang))(0).Alpha2
    End Function

    Public Shared Function ISOLangGetCode3ByLang(ByVal lang As String) As String
        Return (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Name = lang))(0).Alpha3
    End Function

    Public Shared Function ISOLangGetLanguagesList() As ArrayList
        Dim r As New ArrayList
        r.Add(_ISOLanguages.Language.FindAll(Function(f) Not String.IsNullOrEmpty(f.Name)).ToArray)
        Return r
    End Function

    Public Shared Function ISOLangGetLanguagesListAlpha2() As ArrayList
        Dim r As New ArrayList
        r.Add(_ISOLanguages.Language.FindAll(Function(f) Not String.IsNullOrEmpty(f.Alpha2)).ToArray)
        Return r
    End Function
    Public Shared Function ISOLangGetLanguagesListAlpha3() As ArrayList
        Dim r As New ArrayList
        For Each x As LanguagesLanguage In _ISOLanguages.Language.Where(Function(y) Not String.IsNullOrEmpty(y.Alpha3))
            r.Add(x.Name)
        Next
        Return r
    End Function

    Public Sub Clear()
        _ISOLanguages.Language.Clear()
        _all = "All"
        _none = "[none]"
        _disabled = "[Disabled]"
    End Sub

    Public Function GetHelpString(ByVal ctrlName As String) As String
        Dim aStr As String
        Dim x1 As System.Collections.Generic.IEnumerable(Of HelpString)

        x1 = From x As HelpString In htHelpStrings.string Where (x.control = ctrlName)
        If x1.Count = 0 Then
            logger.Trace(New StackFrame().GetMethod().Name, "Missing language_help_string: {0}", ctrlName)
            aStr = String.Empty
        Else
            aStr = x1(0).Value
        End If

        help_logger.Trace("helpstring : '{0}'", aStr)

        Return aStr
    End Function

    Public Function GetString(ByVal ID As Integer, ByVal strDefault As String) As String
        Dim tStr As String
        Dim x1 As System.Collections.Generic.IEnumerable(Of LanguageString)

        Dim Assembly = "*EmberAPP"
        htStrings = htArrayStrings.FirstOrDefault(Function(x) x.AssenblyName = Assembly).htStrings
        If IsNothing(htStrings) Then
            tStr = strDefault
        Else
            x1 = From x As LanguageString In htStrings.string Where (x.id = ID)
            If x1.Count = 0 Then
                logger.Error(New StackFrame().GetMethod().Name, "Missing language_string: {0} - {1} : '{2}'", Assembly, ID, strDefault)
                tStr = strDefault
            Else
                tStr = x1(0).Value
            End If
        End If

        lang_logger.Trace("language_string: {0} - {1} : '{2}'", Assembly, ID, tStr)

        Return tStr
    End Function

    Public Sub LoadAllLanguage(ByVal language As String, Optional ByVal force As Boolean = False)
        If force Then
            htHelpStrings.string.Clear()
            htArrayStrings.Clear()
            htStrings.string.Clear()
        End If
        LoadLanguage(language)
        ' no more module specific language files
        'For Each s As String In ModulesManager.VersionList.Select(Function(m) m.AssemblyFileName).Distinct
        '	LoadLanguage(language, s.Replace(".dll", String.Empty))
        'Next
    End Sub

    Public Sub LoadHelpStrings(ByVal hPath As String)
        Try
            If File.Exists(hPath) Then
                Dim objStreamReader As New StreamReader(hPath)
                Dim xHelpString As New XmlSerializer(htHelpStrings.GetType)

                htHelpStrings = CType(xHelpString.Deserialize(objStreamReader), clsXMLLanguageHelp)
                objStreamReader.Close()
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Sub LoadLanguage(ByVal Language As String, Optional ByVal rAssembly As String = "", Optional ByVal force As Boolean = False)
        Dim _old_all As String = _all
        Dim Assembly As String
        Dim lPath As String = String.Empty
        Dim lhPath As String = String.Empty
        Me.Clear()
        Try
            If Not String.IsNullOrEmpty(Language) Then
                If rAssembly = "" Then
                    Assembly = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetCallingAssembly().Location)
                Else
                    Assembly = rAssembly
                End If


                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" OrElse Assembly = "*EmberAPI" OrElse Assembly = "*EmberAPP" Then
                    Assembly = "*EmberAPP"
                    lPath = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, Language, ".xml")
                    lhPath = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, Language, "-Help.xml")
                Else
                    lPath = String.Concat(Functions.AppPath, "Modules", Path.DirectorySeparatorChar, "Langs", Path.DirectorySeparatorChar, Assembly, ".", Language, ".xml")
                    lhPath = String.Concat(Functions.AppPath, "Modules", Path.DirectorySeparatorChar, "Langs", Path.DirectorySeparatorChar, Assembly, ".", Language, "-Help.xml")
                    If Not File.Exists(lPath) Then 'Failback disabled, possible not need anymore
                        'lPath = String.Concat(Functions.AppPath, "Langs", Path.DirectorySeparatorChar, Language, ".xml")
                        File.WriteAllText(lPath, "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf & _
                            "<strings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & vbCrLf & _
                            "</strings>")
                    End If
                End If
                If Not force AndAlso Not htArrayStrings.FirstOrDefault(Function(h) h.AssenblyName = Assembly).AssenblyName Is Nothing Then Return

                LoadHelpStrings(lhPath)

                htStrings.string.Clear()

                If File.Exists(lPath) Then
                    Dim objStreamReader As New StreamReader(lPath)
                    Dim xLangString As New XmlSerializer(htStrings.GetType)

                    htStrings = CType(xLangString.Deserialize(objStreamReader), clsXMLLanguage)
                    objStreamReader.Close()

                    htArrayStrings.Remove(htArrayStrings.FirstOrDefault(Function(h) h.AssenblyName = Assembly))
                    htArrayStrings.Add(New Locs With {.AssenblyName = Assembly, .htStrings = htStrings, .FileName = lPath})
                    _all = String.Format("[{0}]", GetString(569, Master.eLang.All))
                    _none = GetString(570, Master.eLang.None)
                    _disabled = GetString(571, Master.eLang.Disabled)
                Else
                    Dim tLocs As Locs = htArrayStrings.FirstOrDefault(Function(h) h.AssenblyName = Assembly)
                    If Not IsNothing(tLocs) Then
                        tLocs.htStrings = htStrings
                    Else
                        htArrayStrings.Add(New Locs With {.AssenblyName = Assembly, .htStrings = htStrings, .FileName = lPath})
                    End If
                End If
            Else
                logger.Error("Cannot find {0}.xml." & vbNewLine & "Expected path: {1}", Language, lPath)
                MsgBox(String.Concat(String.Format("Cannot find {0}.xml.", Language), vbNewLine, vbNewLine, "Expected path:", vbNewLine, lPath), MsgBoxStyle.Critical, "File Not Found")
            End If

            ' Need to change Globaly Langs_all
            Master.eSettings.GenreFilter = Master.eSettings.GenreFilter.Replace(_old_all, _all)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub


#End Region 'Methods

#Region "Nested Types"

    ' ************************************************************************************************
    Structure Locs

#Region "Fields"

        Dim AssenblyName As String
        Dim FileName As String
        Dim htStrings As clsXMLLanguage

#End Region 'Fields

    End Structure

    Structure _ISOLanguage

#Region "Fields"

        Public Alpha2Code As String
        Public Alpha3Code As String
        Public Language As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class

<DataContract> _
Public Class [langHash]
    ' need a parameterless constructor for serialization
    Public Sub New()
        MyDictionary = New Dictionary(Of String, String)()
    End Sub
    <DataMember> _
    Public Property MyDictionary() As Dictionary(Of String, String)
        Get
            Return m_MyDictionary
        End Get
        Set(value As Dictionary(Of String, String))
            m_MyDictionary = value
        End Set
    End Property
    Private m_MyDictionary As Dictionary(Of String, String)
End Class