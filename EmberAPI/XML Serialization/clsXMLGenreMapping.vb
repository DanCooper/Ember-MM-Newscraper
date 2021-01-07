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
<XmlRoot("core.genres")>
Public Class clsXMLGenreMapping
    Implements ICloneable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    <XmlElement("defaultimage")>
    Public Property DefaultImage() As String = "default.jpg"

    <XmlIgnore>
    Public ReadOnly Property FileNameFullPath As String = String.Empty

    <XmlElement("genre")>
    Public Property Genres() As List(Of GenreProperty) = New List(Of GenreProperty)

    <XmlElement("mapping")>
    Public Property Mappings() As List(Of GenreMapping) = New List(Of GenreMapping)

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
        DefaultImage = "default.jpg"
        Genres.Clear()
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
            Dim objStreamReader = New StreamReader(FileNameFullPath)
            Try
                Dim nXML = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), clsXMLGenreMapping)
                DefaultImage = nXML.DefaultImage
                Genres = nXML.Genres
                Mappings = nXML.Mappings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                objStreamReader.Close()
                FileUtils.Common.CreateFileBackup(FileNameFullPath)
                File.Delete(FileNameFullPath)
                Clear()
            End Try
        ElseIf SearchOlderVersions Then
            Load()
        Else
            Clear()
        End If
    End Sub

    Public Function RunMapping(ByRef listToBeMapped As List(Of String), Optional ByVal addNewInputs As Boolean = True) As Boolean
        Dim nResult As New List(Of String)
        Dim bNewInputAdded As Boolean
        If listToBeMapped.Count > 0 Then
            For Each aInput As String In listToBeMapped
                Dim existingInput As GenreMapping = Mappings.FirstOrDefault(Function(f) f.SearchString = aInput)
                If existingInput IsNot Nothing Then
                    nResult.AddRange(existingInput.MappedTo)
                ElseIf addNewInputs Then
                    'check if the tGenre is already existing in Gernes list
                    Dim gProperty As GenreProperty = Genres.FirstOrDefault(Function(f) f.Name = aInput)
                    If gProperty Is Nothing Then
                        Genres.Add(New GenreProperty With {
                                   .Name = aInput
                                   })
                    End If
                    'add a new mapping if tGenre is not in the Mappings list
                    Mappings.Add(New GenreMapping With {
                                 .MappedTo = New List(Of String) From {aInput},
                                 .SearchString = aInput
                                 })
                    nResult.Add(aInput)
                    bNewInputAdded = True
                End If
            Next
        End If

        'Cleanup Mappings list (not important but nice)
        If bNewInputAdded Then
            Genres.Sort()
            Mappings.Sort()
            Save()
        End If

        'Cleanup for comparing
        nResult = nResult.Distinct().ToList()
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
        Dim xmlSerial As New XmlSerializer(GetType(clsXMLGenreMapping))
        Dim xmlWriter As New StreamWriter(FileNameFullPath)
        xmlSerial.Serialize(xmlWriter, Me)
        xmlWriter.Close()
    End Sub

    Private Function SearchOlderVersions() As Boolean
#Disable Warning BC40000 'The type or member is obsolete.
        Dim strVersion1 = Path.Combine(Master.SettingsPath, "Core.Genres.xml")
        Select Case True
            Case File.Exists(strVersion1)
                Try
                    File.Move(strVersion1, FileNameFullPath)
                    Return True
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                    FileUtils.Common.CreateFileBackup(strVersion1)
                    File.Delete(strVersion1)
                    Return False
                End Try
            Case Else
                Return False
        End Select
#Enable Warning BC40000 'The type or member is obsolete.
    End Function

    Public Sub Sort()
        Genres.Sort()
        Mappings.Sort()
    End Sub

#End Region 'Methods

End Class

<Serializable()>
Public Class GenreMapping
    Implements IComparable(Of GenreMapping)

#Region "Properties"

    <XmlElement("searchstring")>
    Public Property SearchString() As String = String.Empty

    <XmlElement("mappedto")>
    Public Property MappedTo() As List(Of String) = New List(Of String)

    <XmlElement("isnew")>
    Public Property isNew() As Boolean = True

#End Region 'Properties 

#Region "Methods"

    Public Function CompareTo(ByVal other As GenreMapping) As Integer _
        Implements IComparable(Of GenreMapping).CompareTo
        Try
            Dim retVal As Integer = (SearchString).CompareTo(other.SearchString)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class

<Serializable()>
Public Class GenreProperty
    Implements IComparable(Of GenreProperty)

#Region "Properties"

    <XmlElement("name")>
    Public Property Name() As String = String.Empty

    <XmlElement("image")>
    Public Property Image() As String = String.Empty

    <XmlElement("isnew")>
    Public Property isNew() As Boolean = True

#End Region 'Properties 

#Region "Methods"

    Public Function CompareTo(ByVal other As GenreProperty) As Integer _
        Implements IComparable(Of GenreProperty).CompareTo
        Try
            Dim retVal As Integer = (Name).CompareTo(other.Name)
            Return retVal
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region 'Methods

End Class