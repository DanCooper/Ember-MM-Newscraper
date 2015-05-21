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

Public Class CommandLine

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

    'Singleton Instace for CommandLine manager .. allways use this one
    Private Shared Singleton As CommandLine = Nothing

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As CommandLine
        Get
            If (Singleton Is Nothing) Then
                Singleton = New CommandLine()
            End If
            Return Singleton
        End Get
    End Property

#End Region 'Properties

#Region "Methods"
    Public Sub RunCommandLine(ByVal appArgs As List(Of String))
        If appArgs.Count = 0 Then Return
        LoadMedia(New Structures.Scans With {.TV = True})
    End Sub

    Public Sub LoadMedia(ByVal Scan As Structures.Scans, Optional ByVal SourceName As String = "")
        RaiseEvent GenericEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"loadmedia", Scan, SourceName}))
    End Sub

#End Region 'Methods

End Class
