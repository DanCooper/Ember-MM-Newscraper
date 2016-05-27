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
    Public Event AccountSettingsChanged()

#End Region 'Events

#Region "Methods"

    Private Sub chkTraktEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked)
    End Sub

    Public Sub New()
        InitializeComponent()
        SetUp()
    End Sub

    Private Sub SetUp()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkGetShowProgress.Text = Master.eLang.GetString(1388, "Display watched progress for shows (Time consuming!)")
        gbSettingsGeneral.Text = Master.eLang.GetString(38, "General Settings")
        lblPassword.Text = Master.eLang.GetString(426, "Password")
        lblUsername.Text = Master.eLang.GetString(425, "Username")
        txtPassword.PasswordChar = "*"c
        'LastPlayed
        gbSettingsLastPlayed.Text = Master.eLang.GetString(1369, "Last watched")
        chkSyncLastPlayedEditMovies.Text = Master.eLang.GetString(1469, "Automatically Sync Movies Before Edit")
        chkSyncLastPlayedEditEpisodes.Text = Master.eLang.GetString(1472, "Automatically Sync Episodes Before Edit")
        chkSyncLastPlayedMultiEpisodes.Text = Master.eLang.GetString(1470, "Automatically Sync Episodes During Multi-Scraper")
        chkSyncLastPlayedMultiMovies.Text = Master.eLang.GetString(1467, "Automatically Sync Movies During Multi-Scraper")
        chkSyncLastPlayedSingleEpisodes.Text = Master.eLang.GetString(1471, "Automatically Sync Episodes During Single-Scraper")
        chkSyncLastPlayedSingleMovies.Text = Master.eLang.GetString(1468, "Automatically Sync Movies During Single-Scraper")
        'Playcount
        gbSettingsPlaycount.Text = Master.eLang.GetString(1452, "Playcount")
        chkSyncPlaycountEditMovies.Text = Master.eLang.GetString(1469, "Automatically Sync Movies Before Edit")
        chkSyncPlaycountEditEpisodes.Text = Master.eLang.GetString(1472, "Automatically Sync Episodes Before Edit")
        chkSyncPlaycountMultiEpisodes.Text = Master.eLang.GetString(1470, "Automatically Sync Episodes During Multi-Scraper")
        chkSyncPlaycountMultiMovies.Text = Master.eLang.GetString(1467, "Automatically Sync Movies During Multi-Scraper")
        chkSyncPlaycountSingleEpisodes.Text = Master.eLang.GetString(1471, "Automatically Sync Episodes During Single-Scraper")
        chkSyncPlaycountSingleMovies.Text = Master.eLang.GetString(1468, "Automatically Sync Movies During Single-Scraper")
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        RaiseEvent ModuleSettingsChanged()
        RaiseEvent AccountSettingsChanged()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        RaiseEvent ModuleSettingsChanged()
        RaiseEvent AccountSettingsChanged()
    End Sub

    Private Sub chkGetShowProgress_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetShowProgress.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    'LastPlayed
    Private Sub chkSyncLastPlayedEditMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedEditMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncLastPlayedEditEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedEditEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncLastPlayedMultiEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedMultiEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncLastPlayedMultiMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedMultiMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncLastPlayedSingleEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedSingleEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncLastPlayedSingleMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncLastPlayedSingleMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    'Playcount
    Private Sub chkSyncPlaycountEditMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountEditMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncPlaycountEditEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountEditEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncPlaycountMultiEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountMultiEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncPlaycountMultiMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountMultiMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncPlaycountSingleEpisodes_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountSingleEpisodes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkSyncPlaycountSingleMovies_CheckedChanged(sender As Object, e As EventArgs) Handles chkSyncPlaycountSingleMovies.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class