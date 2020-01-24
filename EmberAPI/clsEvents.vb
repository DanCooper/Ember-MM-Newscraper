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

Public Class Events

#Region "Events"

    Public Event ProgressUpdate(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))

#End Region 'Events

#Region "Methods"

    Public Sub SendEvent(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object))
        If mType = Enums.AddonEventType.Notification Then

        End If
        RaiseEvent ProgressUpdate(mType, _params)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Enum EventType As Integer
        Addon
        Database
        Generic
        Logger
        TaskManager
    End Enum

    Public Class Notification

#Region "Fields"

        Public ReadOnly TypeOfNotification As NotificationType

#End Region 'Fields

#Region "Properties"

#End Region 'Properties

        Public Property Message As String = String.Empty

#Region "Constructors"

        Public Sub New(ByVal type As NotificationType)
            TypeOfNotification = type
        End Sub

#End Region 'Constructors

#Region "Nested Types"

        Public Enum NotificationType As Integer
            [Error]
            Info
            Message
            Trace
            Warn
        End Enum

#End Region 'Nested Types

    End Class

#End Region 'Nested Types

End Class