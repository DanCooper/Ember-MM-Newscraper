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

Imports System.Windows.Forms
Imports EmberAPI

Public Class dlgCompatibility

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub dlgRestart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(888, "Database Compatibility")
        Me.lblHeader.Text = Master.eLang.GetString(889, "Database is not compatible!")
        Me.lblBody.Text = Master.eLang.GetString(890, "Your database is not compatible with this version of Ember!\n\nYour existing database will be saved as ""Media.emm_old"" and an empty database will be created.\n\nTo adapt your old Databank follow this tutorial.").Replace("\n", vbCrLf) 'other languages ​​are currently not loaded

        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        'Me.btnAbort.Text = Master.eLang.GetString(887, "Cancel")
    End Sub

    Private Sub lblTutorial_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblTutorial.LinkClicked
        Process.Start("http://forum.xbmc.org/showthread.php?tid=116941&pid=1410086#pid1410086")
    End Sub
End Class
