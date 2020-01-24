﻿' ################################################################################
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

Imports System.Drawing
Imports System.Windows.Forms

Public Class FormsUtils

    Public Shared Sub ResizeAndMoveDialog(ByRef Dialog As Control, ByRef Form As Form)
        'shrink dialog if bigger than the Windows working area
        Dim iWidth As Integer = Dialog.Width
        Dim iHeight As Integer = Dialog.Height
        If My.Computer.Screen.WorkingArea.Width < iWidth Then
            iWidth = My.Computer.Screen.WorkingArea.Width
        End If
        If My.Computer.Screen.WorkingArea.Height < iHeight Then
            iHeight = My.Computer.Screen.WorkingArea.Height
        End If
        Dialog.Size = New Size(iWidth, iHeight)
        'move the dialog to the center of Embers main dialog
        Dim pLeft As Integer
        Dim pTop As Integer
        pLeft = Master.AppPos.Left + (Master.AppPos.Width - Dialog.Width) \ 2
        pTop = Master.AppPos.Top + (Master.AppPos.Height - Dialog.Height) \ 2
        Dialog.Location = New Point(pLeft, pTop)
        Form.StartPosition = FormStartPosition.Manual
    End Sub

End Class