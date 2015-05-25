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

Public Class Tasks

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared TaskList As New List(Of Task)

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

    'Singleton Instace for CommandLine manager .. allways use this one
    Private Shared Singleton As Tasks = Nothing

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As Tasks
        Get
            If (Singleton Is Nothing) Then
                Singleton = New Tasks()
            End If
            Return Singleton
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub AddTask(ByRef mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        TaskList.Add(New Task With {.mType = mType, .Params = _params})
    End Sub

    Public Sub RunTasks()
        While TaskList.Count > 0
            RaiseEvent GenericEvent(TaskList.Item(0).mType, TaskList.Item(0).Params)
            TaskList.RemoveAt(0)
        End While
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure Task

#Region "Fields"

        Dim mType As Enums.ModuleEventType
        Dim Params As List(Of Object)

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class
