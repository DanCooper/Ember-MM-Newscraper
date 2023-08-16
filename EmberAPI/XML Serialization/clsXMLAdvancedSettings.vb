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
<XmlRoot("AdvancedSettings")>
Public Class XmlAdvancedSettings
    Implements ICloneable
    Implements IDisposable

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _disposed As Boolean = False

#End Region 'Fields

#Region "Properties"

    <XmlIgnore>
    Public ReadOnly Property FullName As String = String.Empty

    <XmlElement("Setting")>
    Public Property Settings As List(Of SingleSetting) = New List(Of SingleSetting)

    <XmlElement("ComplexSettings")>
    Public Property ComplexSettings As List(Of ComplexSettings) = New List(Of ComplexSettings)

#End Region 'Properties

#Region "Constructors"
    ''' <summary>
    ''' Needed for Load Sub
    ''' </summary>
    Public Sub New()
        Return
    End Sub

    Public Sub New(ByVal fileFullName As String)
        FullName = fileFullName
    End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Clear()
        Settings.Clear()
        ComplexSettings.Clear()
    End Sub

    Public Sub ClearComplexSetting(ByVal key As String, Optional ByVal cAssembly As String = "")
        If _disposed Then
            _logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.CleanComplexSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            If Not v Is Nothing Then v.Table.Item.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Close()
        Disposing(True)
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

    Private Overloads Sub Dispose() Implements IDisposable.Dispose
        Disposing(True)
    End Sub

    Private Sub Disposing(disposing As Boolean)
        If disposing Then
            Save()
            _disposed = True
        End If
    End Sub

    Public Function GetBooleanSetting(ByVal key As String, ByVal defaultValue As Boolean, Optional ByVal cAssembly As String = "", Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = From e In Settings.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
                Return If(v(0) Is Nothing, defaultValue, Convert.ToBoolean(v(0).Value.ToString))
            Else
                Dim v = From e In Settings.Where(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                Return If(v(0) Is Nothing, defaultValue, Convert.ToBoolean(v(0).Value.ToString))
            End If

        Catch ex As Exception
            _Logger.Info(ex, New StackFrame().GetMethod().Name)
            Return defaultValue
        End Try
    End Function

    Public Function GetComplexSetting(ByVal key As String, Optional ByVal cAssembly As String = "") As List(Of TableItem)
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            Return If(v Is Nothing, Nothing, v.Table.Item)
        Catch ex As Exception
            _logger.Info(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Function GetStringSetting(ByVal key As String, ByVal defaultValue As String, Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As String
        Dim Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
        Try
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = From e In Settings.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
                Return If(v(0) Is Nothing, defaultValue, If(v(0).Value Is Nothing, String.Empty, v(0).Value.ToString))
            Else
                Dim v = From e In Settings.Where(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                Return If(v(0) Is Nothing, defaultValue, If(v(0).Value Is Nothing, String.Empty, v(0).Value.ToString))
            End If

        Catch ex As Exception
            _Logger.Info(ex, "Key: " & key & " DefValue: " & defaultValue & "  Assembly: " & Assembly & New StackFrame().GetMethod().Name)
            Return defaultValue
        End Try
    End Function

    Public Overridable Sub Load()
        If File.Exists(FullName) Then
            Try
                Dim objStreamReader = New StreamReader(FullName)
                Dim nAdvancedSettings = CType(New XmlSerializer([GetType]).Deserialize(objStreamReader), XmlAdvancedSettings)
                ComplexSettings = nAdvancedSettings.ComplexSettings
                Settings = nAdvancedSettings.Settings
                objStreamReader.Close()
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                FileUtils.Common.CreateFileBackup(FullName)
                Clear()
            End Try
        Else
            Clear()
        End If
    End Sub

    Public Sub RemoveComplexSetting(ByVal key As String, Optional ByVal cAssembly As String = "")
        If _disposed Then
            _logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.CleanComplexSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            If Not v Is Nothing Then ComplexSettings.Remove(v)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub RemoveStringSetting(ByVal key As String, Optional ByVal cAssembly As String = "")
        If _disposed Then
            _logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.CleanSetting on disposed object")
        End If
        Dim Assembly As String = cAssembly
        If Assembly = String.Empty Then
            Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If
        End If
        Dim v = From e In Settings.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
        If Not v(0) Is Nothing Then
            'todo: why not remove all?
            Settings.Remove(v(0))
            'If Not _DoNotSave Then Save()
        End If
    End Sub

    Public Sub Save()
        Try
            If Not Directory.Exists(Directory.GetParent(FullName).FullName) Then
                Directory.CreateDirectory(Directory.GetParent(FullName).FullName)
            End If

            Dim xmlSerial As New XmlSerializer([GetType])
            Dim xmlWriter As New StreamWriter(FullName)
            xmlSerial.Serialize(xmlWriter, Me)
            xmlWriter.Close()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function SetBooleanSetting(ByVal key As String, ByVal value As Boolean, Optional ByVal isDefault As Boolean = False, Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        If _disposed Then
            Throw New ObjectDisposedException("AdvancedSettings.SetBooleanSetting on disposed object")
        End If
        Try
            Dim Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    Settings.Add(New SingleSetting With {.Section = Assembly, .Name = key, .Value = Convert.ToString(value),
                                                                                    .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty)})
                Else
                    Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly).Value = Convert.ToString(value)
                End If
            Else
                Dim v = Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = cContent AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    Settings.Add(New SingleSetting With {.Section = Assembly, .Name = key, .Value = Convert.ToString(value),
                                                                                    .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty), .Content = cContent})
                Else
                    Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent).Value = Convert.ToString(value)
                End If
            End If

            'If Not _DoNotSave Then Save()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

    Public Function SetComplexSetting(ByVal key As String, ByVal value As List(Of TableItem)) As Boolean
        If _disposed Then
            _Logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.SetComplexSetting on disposed object")
        End If
        Try
            Dim Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If
            Dim v = ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            If v Is Nothing Then
                ComplexSettings.Add(New ComplexSettings With {.Table = New Table With {.Section = Assembly, .Name = key, .Item = value}})
            Else
                ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly).Table.Item = value
            End If

        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

    Public Function SetStringSetting(ByVal key As String, ByVal value As String, Optional ByVal isDefault As Boolean = False, Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        If _disposed Then
            Throw New ObjectDisposedException("AdvancedSettings.SetSetting on disposed object")
        End If
        Try
            Dim Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If

            If cContent = Enums.ContentType.None Then

                Dim v = Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    Settings.Add(New SingleSetting With {.Section = Assembly, .Name = key, .Value = value,
                                                                                    .DefaultValue = If(isDefault, value, String.Empty)})
                Else
                    Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly).Value = value
                End If
            Else
                Dim v = Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                If v Is Nothing Then
                    Settings.Add(New SingleSetting With {.Section = Assembly, .Name = key, .Value = value,
                                                                                    .DefaultValue = If(isDefault, value, String.Empty), .Content = cContent})
                Else
                    Settings.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent).Value = value
                End If
            End If

            'If Not _DoNotSave Then Save()
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

#End Region 'Methods

End Class

<Serializable()>
Partial Public Class SingleSetting

#Region "Properties"

    <XmlAttribute("Section")>
    Public Property Section() As String = String.Empty

    <XmlAttribute("Content")>
    Public Property Content() As Enums.ContentType

    <XmlAttribute("Name")>
    Public Property Name() As String = String.Empty

    <XmlAttribute("DefaultValue")>
    Public Property DefaultValue() As String = String.Empty

    <XmlText()>
    Public Property Value() As String = String.Empty

#End Region 'Properties

End Class

<Serializable()>
Partial Public Class ComplexSettings

#Region "Properties"

    <XmlElement("Table")>
    Public Property Table() As Table = New Table

#End Region 'Properties

End Class

<Serializable()>
Partial Public Class Table

#Region "Properties"

    <XmlAttribute("Section")>
    Public Property Section() As String = String.Empty

    <XmlAttribute("Name")>
    Public Property Name() As String = String.Empty

    <XmlElement("Item")>
    Public Property Item() As List(Of TableItem) = New List(Of TableItem)

#End Region 'Properties

End Class

<Serializable()>
Partial Public Class TableItem

#Region "Properties"

    <XmlAttribute("Name")>
    Public Property Name() As String = String.Empty

    <XmlText()>
    Public Property Value() As String = String.Empty

#End Region 'Properties

End Class