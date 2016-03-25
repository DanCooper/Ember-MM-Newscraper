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

Imports EmberAPI
Imports Microsoft.VisualBasic
Imports System.IO

Public Class frmSettingsHolder

#Region "Fields"

#End Region 'Fields

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean, ByVal difforder As Integer)
    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkUseAsAudioPlayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseAsAudioPlayer.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkUseAsVideoPlayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseAsVideoPlayer.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtVLCPath_TextChanged(sender As Object, e As EventArgs) Handles txtVLCPath.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Private Sub SetUp()
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.gbGeneralOpts.Text = Master.eLang.GetString(38, "General Settings")
        If Environment.Is64BitOperatingSystem Then
            If Environment.Is64BitProcess Then
                Me.lblVLCPath.Text = Master.eLang.GetString(1495, "VLC x64 Path")
            Else
                Me.lblVLCPath.Text = Master.eLang.GetString(1478, "VLC x86 Path")
            End If
        Else
            Me.lblVLCPath.Text = Master.eLang.GetString(1478, "VLC x86 Path")
        End If

        Me.chkUseAsAudioPlayer.Text = Master.eLang.GetString(1480, "Use as Video Player")
        Me.chkUseAsVideoPlayer.Text = Master.eLang.GetString(1479, "Use as Audio Player")
    End Sub

    Private Sub btnVLCPath_Click(sender As Object, e As EventArgs) Handles btnVLCPath.Click
        Using fbdDialog As New FolderBrowserDialog()
            fbdDialog.Description = Master.eLang.GetString(1482, "Select VLC Path")
            fbdDialog.SelectedPath = txtVLCPath.Text

            If fbdDialog.ShowDialog() = DialogResult.OK Then
                txtVLCPath.Text = fbdDialog.SelectedPath
            End If
            If Not File.Exists(Path.Combine(fbdDialog.SelectedPath, "libvlc.dll")) Then
                MsgBox(Master.eLang.GetString(1483, "libvlc.dll not found in path"), MsgBoxStyle.Information)
            End If
        End Using

    End Sub

    Private Sub tblSettingsMain_Paint(sender As Object, e As PaintEventArgs) Handles tblSettingsMain.Paint

    End Sub

#End Region 'Methods

End Class