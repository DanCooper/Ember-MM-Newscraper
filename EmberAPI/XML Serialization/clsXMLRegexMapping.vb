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
<XmlRoot("regexmapping")>
Public Class XmlRegexMapping
    Implements ICloneable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    <XmlIgnore>
    Public ReadOnly Property DefaultsFileFullName As String
        Get
            Return FileUtils.Common.ReturnSettingsFile("Defaults", Name)
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property FullName As String = String.Empty

    <XmlElement("mapping")>
    Public Property Mappings() As List(Of RegexMapping) = New List(Of RegexMapping)

    <XmlIgnore>
    Public ReadOnly Property Name As String = String.Empty

#End Region 'Properties

#Region "Constructors"
    ''' <summary>
    ''' Needed for Load Sub
    ''' </summary>
    Private Sub New()
        Clear()
    End Sub

    Public Sub New(ByVal filename As String)
        Name = filename
        Dim CallingAssembly = Reflection.Assembly.GetCallingAssembly()
        If Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location).ToLower = "emberapi" Then
            FullName = Path.Combine(Master.SettingsPath, filename)
        Else
            FullName = Path.Combine(Directory.GetParent(Reflection.Assembly.GetCallingAssembly().Location).FullName, filename)
        End If
    End Sub

#End Region 'Constructors

#Region "Methods"

    Private Sub Clear()
        Mappings.Clear()
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

    Private Function CopyDefaultsFile() As Boolean
        If File.Exists(DefaultsFileFullName) Then
            Try
                File.Copy(DefaultsFileFullName, FullName, True)
                Return True
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        Return False
    End Function

    Public Function GetDefaults() As List(Of RegexMapping)
        Dim nResults As New List(Of RegexMapping)
        If File.Exists(DefaultsFileFullName) Then
            Try
                Dim objStreamReader = New StreamReader(DefaultsFileFullName)
                nResults = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), XmlRegexMapping).Mappings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
        Return nResults
    End Function

    Public Sub Load()
        If File.Exists(FullName) Then
            Try
                Dim objStreamReader = New StreamReader(FullName)
                Mappings = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), XmlRegexMapping).Mappings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                FileUtils.Common.CreateFileBackup(FullName)
                File.Delete(FullName)
                If CopyDefaultsFile() Then Load()
            End Try
        Else
            If CopyDefaultsFile() Then Load()
        End If
    End Sub

    Public Function RunRegex(ByVal fileName As String, ByRef result As String) As Boolean
        For Each regexp In Mappings
            Dim regResult = Regex.Match(fileName, regexp.RegExp)
            If regResult.Success Then
                result = regexp.Result
                Return True
            End If
        Next
        Return False
    End Function

    Public Sub Save()
        Sort()
        Dim xmlSerial As New XmlSerializer([GetType])
        Dim xmlWriter As New StreamWriter(FullName)
        xmlSerial.Serialize(xmlWriter, Me)
        xmlWriter.Close()
    End Sub

    Public Sub Sort()
        Mappings.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class RegexMapping
    Implements IComparable(Of RegexMapping)

#Region "Properties"

    <XmlAttribute("regexp")>
    Public Property RegExp() As String = String.Empty

    <XmlText()>
    Public Property Result() As String = String.Empty

#End Region 'Properties 

#Region "Methods"

    Public Function CompareTo(ByVal other As RegexMapping) As Integer _
        Implements IComparable(Of RegexMapping).CompareTo
        Try
            Dim retVal As Integer = (RegExp).CompareTo(other.RegExp)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class