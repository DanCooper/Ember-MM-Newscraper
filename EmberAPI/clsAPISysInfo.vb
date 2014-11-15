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

Public Class SysInfo

#Region "Fields"

    Private Const SPI_GETWORKAREA As Int32 = 48

#End Region 'Fields

#Region "Methods"

    Public Shared Function GetWorkArea() As RECT
        Dim rc As RECT
        Dim Result As Int32 = SystemParametersInfo(SPI_GETWORKAREA, 0, rc, 0)
        If Result <> 0 Then
            Return rc
        End If
    End Function

    Private Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" _
        (ByVal uiAction As Int32, _
         ByVal uiParam As Int32, _
         ByRef pvParam As RECT, _
         ByVal fWinIni As Int32) As Int32

#End Region 'Methods

#Region "Nested Types"
    Public Structure RECT
        Dim Left As Int32
        Dim Top As Int32
        Dim Right As Int32
        Dim Bottom As Int32
    End Structure

#End Region 'Nested Types

End Class
