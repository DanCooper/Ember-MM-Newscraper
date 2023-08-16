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

#Region "Fields"

    Shared _logger As Logger = LogManager.GetCurrentClassLogger()
    Private Shared _AdvancedSettings As New XmlAdvancedSettings(Path.Combine(Master.SettingsPath, "AdvancedSettings.xml"))

    Private _disposed As Boolean = False

#End Region 'Fields

#Region "Properties"

    Public Shared Property AdvancedSettings As XmlAdvancedSettings
        Get
            Return _AdvancedSettings
        End Get
        Set(value As XmlAdvancedSettings)
            _AdvancedSettings = value
        End Set
    End Property

#End Region 'Properties

#Region "Constructors"

    'Public Shared Sub Start()
    '    Try
    '        Dim configpath As String = Path.Combine(Master.SettingsPath, "AdvancedSettings.xml")
    '        Load(configpath)
    '    Catch ex As Exception
    '        logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    'End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub Load()
        Dim configpath As String = Path.Combine(Master.SettingsPath, "AdvancedSettings.xml")
        Try
            If File.Exists(configpath) Then
                Dim objStreamReader As New StreamReader(configpath)
                Dim xAdvancedSettings As New XmlSerializer(_AdvancedSettings.GetType)
                _AdvancedSettings = CType(xAdvancedSettings.Deserialize(objStreamReader), XmlAdvancedSettings)
                objStreamReader.Close()
            End If
        Catch ex As Exception
            _logger.Error(ex, New StackFrame().GetMethod().Name)
            _logger.Info("An attempt is made to repair the AdvancedSettings.xml")
            Try
                Using srAdvancedSettings As New StreamReader(configpath)
                    Dim sAdvancedSettings As String = srAdvancedSettings.ReadToEnd
                    'old ContentTypes
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Episode""", "Content=""TVEpisode""")
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Season""", "Content=""TVSeason""")
                    sAdvancedSettings = Regex.Replace(sAdvancedSettings, "Content=""Show""", "Content=""TVShow""")

                    Dim xXMLSettings As New XmlSerializer(_AdvancedSettings.GetType)
                    Using reader As TextReader = New StringReader(sAdvancedSettings)
                        _AdvancedSettings = CType(xXMLSettings.Deserialize(reader), XmlAdvancedSettings)
                    End Using
                End Using
                _logger.Info("AdvancedSettings.xml successfully repaired")
            Catch ex2 As Exception
                _logger.Error(ex2, New StackFrame().GetMethod().Name)
                FileUtils.Common.CreateFileBackup(configpath)
                _AdvancedSettings = New XmlAdvancedSettings(Path.Combine(Master.SettingsPath, "AdvancedSettings.xml"))
            End Try
        End Try
    End Sub

    Public Sub Save()
        If _disposed Then
            _logger.Fatal(New StackFrame().GetMethod().Name, "AdvancedSettings.Save on disposed object")
        End If
        Try
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
            _logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "AdvancedSettings upgrade Methods"

#End Region 'AdvancedSettings upgrade Methods

End Class