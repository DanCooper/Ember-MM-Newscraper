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

Imports System.IO

Public Class ErrorLogger


#Region "Events"

	Public Event ErrorOccurred()

#End Region	'Events

#Region "Methods"

	''' <summary>
	''' Write the error to our log file, if enabled in settings.
	''' </summary>
	''' <param name="msg">Error summary</param>
	''' <param name="stkTrace">Full stack trace</param>
	''' <param name="title">Error title</param>
	Public Sub WriteToErrorLog(ByVal msg As String, ByVal stkTrace As String, ByVal title As String, Optional ByVal Notify As Boolean = True)
        Master.logger.Debug("{0} {1}", msg, stkTrace)
        If Notify Then ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(816, "An Error Has Occurred"), msg, Nothing}))
        RaiseEvent ErrorOccurred()
    End Sub

#End Region	'Methods

End Class
