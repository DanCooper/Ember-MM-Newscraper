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

Public Class clsVLC

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Public Shared Function DoTest(Optional ByVal withDialog As Boolean = False) As Boolean
        Try
            Dim VLCPlayer As New AXVLC.VLCPlugin2
            If withDialog Then MsgBox("Looking good", MsgBoxStyle.OkOnly, "OK")
            Return True
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            MsgBox("VLC ActiveX plugin is not registred", MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

#End Region 'Methods

End Class
