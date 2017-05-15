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

Public Class AdvancedSettings
    Implements IDisposable

#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private Shared _AdvancedSettings As New clsXMLAdvancedSettings

    Private Shared _DoNotSave As Boolean = False

    Private _disposed As Boolean = False

#End Region 'Fields

#Region "Properties"
    Public Shared Property AdvancedSettings As clsXMLAdvancedSettings
        Get
            Return _AdvancedSettings
        End Get
        Set(value As clsXMLAdvancedSettings)
            _AdvancedSettings = value
        End Set
    End Property
#End Region 'Properties

#Region "Constructors"

    Public Shared Sub Start()
        Try
            Dim configpath As String = Path.Combine(Master.SettingsPath, "AdvancedSettings.xml")
            Load(configpath)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function GetAllSettings() As List(Of AdvancedSettingsSetting)
        Return _AdvancedSettings.Setting
    End Function

#End Region 'Constructors

#Region "Methods"

    Private Overloads Sub Dispose() Implements IDisposable.Dispose
        Disposing(True)
    End Sub

    Private Sub Disposing(disposing As Boolean)
        If disposing Then
            If Not _DoNotSave Then
                Save()
            End If
            _disposed = True
        End If
    End Sub

    Public Sub Close()
        Disposing(True)
    End Sub

    Public Shared Function GetBooleanSetting(ByVal key As String, ByVal defvalue As Boolean, Optional ByVal cAssembly As String = "", Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = From e In _AdvancedSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
                Return If(v(0) Is Nothing, defvalue, Convert.ToBoolean(v(0).Value.ToString))
            Else
                Dim v = From e In _AdvancedSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                Return If(v(0) Is Nothing, defvalue, Convert.ToBoolean(v(0).Value.ToString))
            End If

        Catch ex As Exception
            logger.Info(ex, New StackFrame().GetMethod().Name)
            Return defvalue
        End Try
    End Function

    Public Shared Function GetSetting(ByVal key As String, ByVal defvalue As String, Optional ByVal cAssembly As String = "", Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As String
        Dim Assembly As String = cAssembly
        Try
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = From e In _AdvancedSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
                Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defvalue, v(0).Value.ToString)
            Else
                Dim v = From e In _AdvancedSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                Return If(v(0) Is Nothing OrElse v(0).Value Is Nothing, defvalue, v(0).Value.ToString)
            End If

        Catch ex As Exception
            logger.Info(ex, "Key: " & key & " DefValue: " & defvalue & "  Assembly: " & Assembly & New StackFrame().GetMethod().Name)
            Return defvalue
        End Try
    End Function

    Public Sub CleanSetting(ByVal key As String, Optional ByVal cAssembly As String = "")
        If _disposed Then
            logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.CleanSetting on disposed object")
        End If
        Dim Assembly As String = cAssembly
        If Assembly = String.Empty Then
            Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
            If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                Assembly = "*EmberAPP"
            End If
        End If
        Dim v = From e In _AdvancedSettings.Setting.Where(Function(f) f.Name = key AndAlso f.Section = Assembly)
        If Not v(0) Is Nothing Then
            _AdvancedSettings.Setting.Remove(v(0))
            'If Not _DoNotSave Then Save()
        End If
    End Sub

    Public Sub ClearComplexSetting(ByVal key As String, Optional ByVal cAssembly As String = "")
        If _disposed Then
            logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.CleanComplexSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = _AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            If Not v Is Nothing Then v.Table.Item.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function GetComplexSetting(ByVal key As String, Optional ByVal cAssembly As String = "") As List(Of AdvancedSettingsComplexSettingsTableItem)
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = _AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            Return If(v Is Nothing, Nothing, v.Table.Item)
        Catch ex As Exception
            logger.Info(ex, New StackFrame().GetMethod().Name)
            Return Nothing
        End Try
    End Function

    Public Function SetComplexSetting(ByVal key As String, ByVal value As List(Of AdvancedSettingsComplexSettingsTableItem), Optional ByVal cAssembly As String = "") As Boolean
        If _disposed Then
            logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.SetComplexSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If
            Dim v = _AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly)
            If v Is Nothing Then
                _AdvancedSettings.ComplexSettings.Add(New AdvancedSettingsComplexSettings With {.Table = New AdvancedSettingsComplexSettingsTable With {.Section = Assembly, .Name = key, .Item = value}})
            Else
                _AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = key AndAlso f.Table.Section = Assembly).Table.Item = value
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

    Private Shared Sub Load(ByVal fname As String)
        _DoNotSave = True
        Try
            If File.Exists(fname) Then
                Dim objStreamReader As New StreamReader(fname)
                Dim xAdvancedSettings As New XmlSerializer(_AdvancedSettings.GetType)
                _AdvancedSettings = CType(xAdvancedSettings.Deserialize(objStreamReader), clsXMLAdvancedSettings)
                objStreamReader.Close()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            logger.Info("An attempt is made to repair the AdvancedSettings.xml")
            Try
                Using srAdvancedSettings As New StreamReader(fname)
                    Dim sAdvancedSettings As String = srAdvancedSettings.ReadToEnd
                    'old ContentTypes
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Episode""", "Content=""TVEpisode""")
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Season""", "Content=""TVSeason""")
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Show""", "Content=""TVShow""")

                    Dim xXMLSettings As New XmlSerializer(_AdvancedSettings.GetType)
                    Using reader As TextReader = New StringReader(sAdvancedSettings)
                        _AdvancedSettings = CType(xXMLSettings.Deserialize(reader), clsXMLAdvancedSettings)
                    End Using
                End Using
                logger.Info("AdvancedSettings.xml successfully repaired")
            Catch ex2 As Exception
                logger.Error(ex2, New StackFrame().GetMethod().Name)
                File.Copy(fname, String.Concat(fname, "_backup"), True)
                _AdvancedSettings = New clsXMLAdvancedSettings
            End Try
        End Try

        'Add complex settings to general advancedsettings.xml if those settings don't exist
        Dim formatconversions As List(Of AdvancedSettingsComplexSettingsTableItem) = GetComplexSetting("VideoFormatConverts", "*EmberAPP")
        If formatconversions Is Nothing Then
            Using settings = New AdvancedSettings()
                settings.SetDefaults("VideoFormatConverts")
            End Using
        End If
        formatconversions = GetComplexSetting("AudioFormatConverts", "*EmberAPP")
        If formatconversions Is Nothing Then
            Using settings = New AdvancedSettings()
                settings.SetDefaults("AudioFormatConverts")
            End Using
        End If
        formatconversions = GetComplexSetting("VideoSourceMapping", "*EmberAPP")
        If formatconversions Is Nothing Then
            Using settings = New AdvancedSettings()
                settings.SetDefaults("VideoSourceMapping")
            End Using
        End If
        _DoNotSave = False
    End Sub

    Private Sub Save()
        If _disposed Then
            logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.Save on disposed object")
        End If
        Try
            If _DoNotSave Then
                _DoNotSave = False
                Return
            End If
            'Cocotus All XML-config files in new Setting-folder!
            Dim configpath As String = Path.Combine(Master.SettingsPath, "AdvancedSettings.xml")
            If File.Exists(configpath) Then
                File.Delete(configpath)
            End If
            If File.Exists(Path.Combine(Functions.AppPath, "AdvancedSettings.xml")) Then
                File.Delete(Path.Combine(Functions.AppPath, "AdvancedSettings.xml"))
            End If

            Dim writer As New FileStream(configpath, FileMode.Create)
            Dim xAdvancedSettings As New XmlSerializer(_AdvancedSettings.GetType)
            ' Serialize the object, and close the TextWriter
            xAdvancedSettings.Serialize(writer, _AdvancedSettings)
            writer.Close()


        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function SetBooleanSetting(ByVal key As String, ByVal value As Boolean, Optional ByVal cAssembly As String = "", Optional ByVal isDefault As Boolean = False, Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        If _disposed Then
            Throw New ObjectDisposedException("AdvancedSettings.SetBooleanSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If

            If cContent = Enums.ContentType.None Then
                Dim v = _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    _AdvancedSettings.Setting.Add(New AdvancedSettingsSetting With {.Section = Assembly, .Name = key, .Value = Convert.ToString(value),
                                                                                    .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty)})
                Else
                    _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly).Value = Convert.ToString(value)
                End If
            Else
                Dim v = _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Content = cContent AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    _AdvancedSettings.Setting.Add(New AdvancedSettingsSetting With {.Section = Assembly, .Name = key, .Value = Convert.ToString(value),
                                                                                    .DefaultValue = If(isDefault, Convert.ToString(value), String.Empty), .Content = cContent})
                Else
                    _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent).Value = Convert.ToString(value)
                End If
            End If

            'If Not _DoNotSave Then Save()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

    Public Function SetSetting(ByVal key As String, ByVal value As String, Optional ByVal cAssembly As String = "", Optional ByVal isDefault As Boolean = False, Optional ByVal cContent As Enums.ContentType = Enums.ContentType.None) As Boolean
        If _disposed Then
            Throw New ObjectDisposedException("AdvancedSettings.SetSetting on disposed object")
        End If
        Try
            Dim Assembly As String = cAssembly
            If Assembly = String.Empty Then
                Assembly = Path.GetFileNameWithoutExtension(Reflection.Assembly.GetCallingAssembly().Location)
                If Assembly = "Ember Media Manager" OrElse Assembly = "EmberAPI" Then
                    Assembly = "*EmberAPP"
                End If
            End If

            If cContent = Enums.ContentType.None Then

                Dim v = _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly)
                If v Is Nothing Then
                    _AdvancedSettings.Setting.Add(New AdvancedSettingsSetting With {.Section = Assembly, .Name = key, .Value = value,
                                                                                    .DefaultValue = If(isDefault, value, String.Empty)})
                Else
                    _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly).Value = value
                End If
            Else
                Dim v = _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent)
                If v Is Nothing Then
                    _AdvancedSettings.Setting.Add(New AdvancedSettingsSetting With {.Section = Assembly, .Name = key, .Value = value,
                                                                                    .DefaultValue = If(isDefault, value, String.Empty), .Content = cContent})
                Else
                    _AdvancedSettings.Setting.FirstOrDefault(Function(f) f.Name = key AndAlso f.Section = Assembly AndAlso f.Content = cContent).Value = value
                End If
            End If

            'If Not _DoNotSave Then Save()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
        Return True
    End Function

    Public Sub SetDefaults(ByVal section As String)
        If String.IsNullOrEmpty(section) Then Return

        Dim aPath As String
        If _disposed Then
            logger.Error(New StackFrame().GetMethod().Name, "AdvancedSettings.SetDefaults on disposed object")
            Throw New ObjectDisposedException("AdvancedSettings.SetDefaults on disposed object")
        End If
        _DoNotSave = True
        aPath = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultAdvancedSettings - " & section & ".xml")
        Dim objStreamReader As New StreamReader(aPath)
        Dim aAdvancedSettings As New clsXMLAdvancedSettings
        Dim xAdvancedSettings As New XmlSerializer(aAdvancedSettings.GetType)

        aAdvancedSettings = CType(xAdvancedSettings.Deserialize(objStreamReader), clsXMLAdvancedSettings)
        objStreamReader.Close()
        _AdvancedSettings.Setting.AddRange(aAdvancedSettings.Setting)
        While _AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = section) IsNot Nothing
            _AdvancedSettings.ComplexSettings.Remove(_AdvancedSettings.ComplexSettings.FirstOrDefault(Function(f) f.Table.Name = section))
        End While
        _AdvancedSettings.ComplexSettings.AddRange(aAdvancedSettings.ComplexSettings)

        _DoNotSave = False
    End Sub

#End Region 'Methods

End Class
