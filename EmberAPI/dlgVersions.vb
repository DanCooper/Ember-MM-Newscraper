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

Imports System.Text
Imports System.Windows.Forms

Public Class dlgVersions

#Region "Methods"

    Private Sub Button_Copy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy.Click
        Dim sVersions As New StringBuilder
        For Each lItem As ListViewItem In lstVersions.Items
            sVersions.AppendLine(String.Format("{0} (Revision: {1})", lItem.Text, lItem.SubItems(1).Text))
        Next
        Clipboard.SetText(sVersions.ToString)
    End Sub

    Private Sub Button_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

#End Region 'Methods

End Class