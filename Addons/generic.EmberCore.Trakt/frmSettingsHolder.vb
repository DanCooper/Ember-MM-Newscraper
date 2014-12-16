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

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub chkTraktEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Private Sub SetUp()
        chkEnabled.Text = Master.eLang.GetString(778, "Use trakt.tv as source for ""Playcount""")
        Me.gbSettingsGeneral.Text = Master.eLang.GetString(38, "General Settings")
        lblUsername.Text = Master.eLang.GetString(425, "Username")
        lblPassword.Text = Master.eLang.GetString(426, "Password")

        txtUsername.Text = Master.eSettings.TraktUsername
        txtPassword.Text = Master.eSettings.TraktPassword
        txtPassword.PasswordChar = "*"c

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

    Public Sub SaveChanges()
        Master.eSettings.TraktUsername = txtUsername.Text
        Master.eSettings.TraktPassword = txtPassword.Text
        Master.eSettings.UseTrakt = chkEnabled.Checked
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class