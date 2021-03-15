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

<Serializable()>
<XmlRoot("simplemapping")>
Public Class clsXMLSimpleMapping
    Implements ICloneable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    <XmlIgnore>
    Public ReadOnly Property FileNameFullPath As String = String.Empty

    <XmlElement("mapping")>
    Public Property Mappings() As List(Of SimpleMapping) = New List(Of SimpleMapping)

#End Region 'Properties

#Region "Constructors"
    ''' <summary>
    ''' Needed for Load Sub
    ''' </summary>
    Private Sub New()
        Clear()
    End Sub

    Public Sub New(ByVal fileNameFullPath As String)
        Me.FileNameFullPath = fileNameFullPath
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

    Public Sub Load()
        If File.Exists(FileNameFullPath) Then
            Try
                Dim objStreamReader = New StreamReader(FileNameFullPath)
                Mappings = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), clsXMLSimpleMapping).Mappings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                FileUtils.Common.CreateFileBackup(FileNameFullPath)
                Clear()
            End Try
        Else
            Clear()
        End If
    End Sub

    Public Function RunMapping(ByRef singleString As String, Optional ByVal addNewInputs As Boolean = True) As Boolean
        Dim nResult As String = String.Empty
        Dim bNewInputAdded As Boolean
        If Not String.IsNullOrEmpty(singleString) Then
            Dim strInput = singleString
            Dim existingInput As SimpleMapping = Mappings.FirstOrDefault(Function(f) f.Input = strInput)
            If existingInput IsNot Nothing Then
                nResult = existingInput.MappedTo
            ElseIf addNewInputs Then
                nResult = singleString
                Mappings.Add(New SimpleMapping With {
                             .Input = singleString,
                             .MappedTo = singleString
                             })
                bNewInputAdded = True
            End If
        End If

        'Cleanup Mappings list (not important but nice)
        If bNewInputAdded Then
            Mappings.Sort()
            Save()
        End If

        'Comparing (check if something has been changed)
        Dim bNoChanges = (singleString = nResult)

        'Set nResult as mapping result
        singleString = nResult

        'Return if the string has been changed or not
        Return Not bNoChanges
    End Function

    Public Function RunMapping(ByRef listToBeMapped As List(Of String), Optional ByVal addNewInputs As Boolean = True) As Boolean
        Dim nResult As New List(Of String)
        Dim bNewInputAdded As Boolean
        If listToBeMapped.Count > 0 Then
            For Each aInput In listToBeMapped
                Dim existingInput As SimpleMapping = Mappings.FirstOrDefault(Function(f) f.Input = aInput)
                If existingInput IsNot Nothing Then
                    nResult.Add(existingInput.MappedTo)
                ElseIf addNewInputs Then
                    nResult.Add(aInput)
                    Mappings.Add(New SimpleMapping With {
                                 .Input = aInput,
                                 .MappedTo = aInput
                                 })
                    bNewInputAdded = True
                End If
            Next
        End If

        'Cleanup Mappings list (not important but nice)
        If bNewInputAdded Then
            Mappings.Sort()
            Save()
        End If

        'Cleanup for comparing
        nResult = nResult.Distinct().ToList
        nResult.Sort()
        listToBeMapped.Sort()

        'Comparing (check if something has been changed)
        Dim bNoChanges = listToBeMapped.SequenceEqual(nResult)

        'Set nResult as mapping result
        listToBeMapped = nResult

        'Return if the list has been changed or not
        Return Not bNoChanges
    End Function

    Public Sub Save()
        Sort()
        Dim xmlSerial As New XmlSerializer(GetType(clsXMLSimpleMapping))
        Dim xmlWriter As New StreamWriter(FileNameFullPath)
        xmlSerial.Serialize(xmlWriter, Me)
        xmlWriter.Close()
    End Sub

    Public Sub Sort()
        Mappings.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class SimpleMapping
    Implements IComparable(Of SimpleMapping)

#Region "Properties"

    <XmlAttribute("input")>
    Public Property Input() As String = String.Empty

    <XmlText()>
    Public Property MappedTo() As String = String.Empty

#End Region 'Properties 

#Region "Methods"

    Public Function CompareTo(ByVal other As SimpleMapping) As Integer _
        Implements IComparable(Of SimpleMapping).CompareTo
        Try
            Dim retVal As Integer = (Input).CompareTo(other.Input)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class