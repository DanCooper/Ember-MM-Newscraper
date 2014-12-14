Imports EmberAPI

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

Public Class frmTraktSettingsHolder

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub chkTraktEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTraktEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkTraktEnabled.Checked)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Private Sub SetUp()
        chkTraktEnabled.Text = Master.eLang.GetString(778, "Use trakt.tv as source for ""Playcount""")
        Me.gpb_TraktGeneralSettings.Text = Master.eLang.GetString(38, "General Settings")
        btnTraktSaveSettings.Text = Master.eLang.GetString(273, "Save")
        lblTraktUsername.Text = Master.eLang.GetString(425, "Username")
        lblTraktPassword.Text = Master.eLang.GetString(426, "Password")

        txtTraktUsername.Text = Master.eSettings.TraktUsername
        txtTraktPassword.Text = Master.eSettings.TraktPassword
        txtTraktPassword.PasswordChar = "*"c

        'If Not String.IsNullOrEmpty(Master.eSettings.UseTrakt.ToString) Then
        '    chkUseTrakt.Checked = Master.eSettings.UseTrakt
        'End If

        'If Master.eSettings.UseTrakt = True Then
        '    txtTraktUsername.Enabled = True
        '    txtTraktPassword.Enabled = True
        'Else
        '    txtTraktUsername.Enabled = False
        '    txtTraktPassword.Enabled = False
        'End If
    End Sub

    Private Sub btnSavetraktsettings_Click(sender As Object, e As EventArgs) Handles btnTraktSaveSettings.Click
        '  SaveChanges()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Sub SaveChanges()
        Master.eSettings.TraktUsername = txtTraktUsername.Text
        Master.eSettings.TraktPassword = txtTraktPassword.Text
        Master.eSettings.UseTrakt = chkTraktEnabled.Checked
    End Sub

    Private Sub txtTraktUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTraktUsername.TextChanged
        If txtTraktUsername.Text <> "" AndAlso txtTraktPassword.Text <> "" Then
            btnTraktSaveSettings.Enabled = True
        Else
            btnTraktSaveSettings.Enabled = False
        End If
        'RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTraktPassword_TextChanged(sender As Object, e As EventArgs) Handles txtTraktPassword.TextChanged
        If txtTraktUsername.Text <> "" AndAlso txtTraktPassword.Text <> "" Then
            btnTraktSaveSettings.Enabled = True
        Else
            btnTraktSaveSettings.Enabled = False
        End If
    End Sub

#End Region 'Methods



End Class