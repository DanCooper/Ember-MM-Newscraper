﻿' ################################################################################
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
Imports NLog
Imports System.Runtime.Serialization
Imports System.Windows.Forms

Public Class Localization

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Shared help_logger As Logger = LogManager.GetLogger("HelpString")
    Shared lang_logger As Logger = LogManager.GetLogger("LanguageString")

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

            If _ISOLanguages.Language.Count = 0 Then
                logger.Fatal("Cannot load Language.xml." & Environment.NewLine & "Path: {0}", lPath)
                MessageBox.Show(String.Concat("Cannot load Language.xml.", Environment.NewLine, Environment.NewLine, "Path:", Environment.NewLine, lPath), "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            logger.Fatal("Cannot find Language.xml." & Environment.NewLine & "Expected path: {0}", lPath)
            MessageBox.Show(String.Concat("Cannot find Language.xml.", Environment.NewLine, Environment.NewLine, "Expected path:", Environment.NewLine, lPath), "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        If Not String.IsNullOrEmpty(code) AndAlso Not code = "00" AndAlso Not code.ToLower = "xx" Then
            Dim tLang = (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Alpha2 = code))(0)
            If tLang IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLang.Name) Then
                Return tLang.Name
            Else
                Return Master.eLang.GetString(138, "Unknown")
            End If
        Else
            Return Master.eLang.GetString(1168, "Blank")
        End If
    End Function

    Shared Function ISOGetLangByCode3(ByVal code As String) As String
        If Not String.IsNullOrEmpty(code) AndAlso Not code = "00" AndAlso Not code.ToLower = "xx" Then
            Dim tLang = (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Alpha3 = code))(0)
            If tLang IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLang.Name) Then
                Return tLang.Name
            Else
                Return Master.eLang.GetString(138, "Unknown")
            End If
        Else
            Return Master.eLang.GetString(1168, "Blank")
        End If
    End Function

    Public Shared Function ISOLangGetCode2ByLang(ByVal lang As String) As String
        If Not String.IsNullOrEmpty(lang) Then
            Dim tLang = (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Name = lang))(0)
            If tLang IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLang.Alpha2) Then
                Return tLang.Alpha2
            Else
                Return Master.eLang.GetString(138, "Unknown")
            End If
        Else
            Return Master.eLang.GetString(1168, "Blank")
        End If
    End Function

    Public Shared Function ISOLangGetCode2ByCode3(ByVal lang As String) As String
        If Not String.IsNullOrEmpty(lang) Then
            Dim tLang = (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Alpha3 = lang))(0)
            If tLang IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLang.Alpha3) Then
                Return tLang.Alpha2
            Else
                Return Master.eLang.GetString(138, "Unknown")
            End If
        Else
            Return Master.eLang.GetString(1168, "Blank")
        End If
    End Function

    Public Shared Function ISOLangGetCode3ByLang(ByVal lang As String) As String
        If Not String.IsNullOrEmpty(lang) Then
            Dim tLang = (From x As LanguagesLanguage In _ISOLanguages.Language Where (x.Name = lang))(0)
            If tLang IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLang.Alpha3) Then
                Return tLang.Alpha3
            Else
                Return Master.eLang.GetString(138, "Unknown")
            End If
        Else
            Return Master.eLang.GetString(1168, "Blank")
        End If
    End Function

    Public Shared Function ISOLangGetLanguagesList() As ArrayList
        Dim r As New ArrayList
        For Each x As LanguagesLanguage In _ISOLanguages.Language.Where(Function(y) Not String.IsNullOrEmpty(y.Name))
            r.Add(x.Name)
        Next
        Return r
    End Function

    Public Shared Function ISOLangGetLanguagesListAlpha2() As ArrayList
        Dim r As New ArrayList
        For Each x As LanguagesLanguage In _ISOLanguages.Language.Where(Function(y) Not String.IsNullOrEmpty(y.Alpha2))
            r.Add(x.Name)
        Next
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
            help_logger.Warn(String.Format("Missing language_help_string: {0}", ctrlName), New StackFrame().GetMethod().Name)
            aStr = String.Empty
        Else
            aStr = x1(0).Value
        End If

        Return aStr
    End Function

    Public Function GetString(ByVal ID As Integer, ByVal strDefault As String) As String
        Dim tStr As String
        Dim x1 As System.Collections.Generic.IEnumerable(Of LanguageString)

        Dim Assembly = "*EmberAPP"
        htStrings = htArrayStrings.FirstOrDefault(Function(x) x.AssenblyName = Assembly).htStrings
        If htStrings Is Nothing Then
            tStr = strDefault
        Else
            x1 = From x As LanguageString In htStrings.string Where (x.id = ID)
            If x1.Count = 0 Then
                lang_logger.Warn(String.Format("Missing language_string: {0} - {1} : '{2}'", Assembly, ID, strDefault), New StackFrame().GetMethod().Name)
                tStr = strDefault
            Else
                If Not String.IsNullOrEmpty(x1(0).Value) Then
                    tStr = x1(0).Value
                Else
                    tStr = strDefault
                End If
            End If
        End If

        Return tStr
    End Function

    Public Sub LoadAllLanguage(ByVal language As String, Optional ByVal force As Boolean = False)
        If force Then
            _all = "All"
            _none = "[none]"
            _disabled = "[Disabled]"

            htHelpStrings.string.Clear()
            htArrayStrings.Clear()
            htStrings.string.Clear()
        End If
        LoadLanguage(language)
    End Sub

    Public Sub LoadHelpStrings(ByVal hPath As String)
        If File.Exists(hPath) Then
            Dim objStreamReader As New StreamReader(hPath)
            Dim xHelpString As New XmlSerializer(htHelpStrings.GetType)

            htHelpStrings = CType(xHelpString.Deserialize(objStreamReader), clsXMLLanguageHelp)
            objStreamReader.Close()
        End If
    End Sub

    Public Sub LoadLanguage(ByVal Language As String, Optional ByVal rAssembly As String = "", Optional ByVal force As Boolean = False)
        Dim _old_all As String = _all
        Dim Assembly As String
        Dim lPath As String = String.Empty
        Dim lhPath As String = String.Empty

        If Not String.IsNullOrEmpty(Language) Then
            If rAssembly = String.Empty Then
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
                    File.WriteAllText(lPath, "<?xml version=""1.0"" encoding=""utf-8""?>" & vbCrLf &
                        "<strings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">" & vbCrLf &
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
                If tLocs.htStrings IsNot Nothing Then
                    tLocs.htStrings = htStrings
                Else
                    htArrayStrings.Add(New Locs With {.AssenblyName = Assembly, .htStrings = htStrings, .FileName = lPath})
                End If
            End If
        Else
            logger.Error("Cannot find {0}.xml." & Environment.NewLine & "Expected path: {1}", Language, lPath)
            MessageBox.Show(String.Concat(String.Format("Cannot find {0}.xml.", Language), Environment.NewLine, Environment.NewLine, "Expected path:", Environment.NewLine, lPath), "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
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