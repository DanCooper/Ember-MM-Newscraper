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

Public Class frmSettingsPanel

#Region "Events"

    Public Event NeedsRestart()
    Public Event SettingsChanged()

#End Region 'Events

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        lblApiKey.Text = String.Concat(Master.eLang.GetString(870, "TMDB API Key"), ":")
    End Sub

    Private Sub ApiKey_Click(sender As Object, e As EventArgs) Handles pbApiKey.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

    Private Sub ApiKey_TextChanged(sender As Object, e As EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Logo_Click(sender As Object, e As EventArgs) Handles pbLogo.Click
        Functions.Launch(My.Resources.urlWebsite)
    End Sub

#End Region 'Methods

End Class