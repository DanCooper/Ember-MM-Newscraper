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
Imports System.Xml
Imports System.Xml.Serialization


<Serializable()>
<XmlRoot("profiles")>
Public Class Profiles

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared strProfilesPath As String = Path.Combine(Functions.AppPath, "Profiles\profiles.xml")

#End Region 'Fields

#Region "Properties"

    <XmlElement("default")>
    Public Property DefaultProfile() As String

    <XmlIgnore>
    Public ReadOnly Property DefaultProfileSpecified() As Boolean
        Get
            Return Not String.IsNullOrEmpty(DefaultProfile)
        End Get
    End Property

    <XmlElement("autoload")>
    Public Property Autoload() As Boolean

#End Region 'Properties

#Region "Constructors"

    'Public Sub New()
    '    LoadSettings()
    'End Sub

#End Region 'Constructors

#Region "Methods"

    Public Sub LoadSettings()
        Autoload = False
        DefaultProfile = String.Empty
        If File.Exists(strProfilesPath) Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As StreamReader = New StreamReader(strProfilesPath)
                xmlSer = New XmlSerializer(GetType(Profiles))
                Dim nSettings = DirectCast(xmlSer.Deserialize(xmlSR), Profiles)
                Autoload = nSettings.Autoload
                DefaultProfile = nSettings.DefaultProfile
            End Using
        End If
    End Sub

    Public Sub SaveSettings()
        If Not File.Exists(strProfilesPath) OrElse (Not CBool(File.GetAttributes(strProfilesPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(strProfilesPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(strProfilesPath)
                Try
                    File.SetAttributes(strProfilesPath, FileAttributes.Normal)
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
            Using xmlSW As New StreamWriter(strProfilesPath)
                Dim xmlSer As New XmlSerializer(GetType(Profiles))
                xmlSer.Serialize(xmlSW, Me)
            End Using
        End If
    End Sub

#End Region 'Methods

End Class
