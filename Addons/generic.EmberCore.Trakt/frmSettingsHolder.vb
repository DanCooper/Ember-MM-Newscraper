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
        chkGetWatchedState.Text = Master.eLang.GetString(1070, "Get Watched State")
        chkGetWatchedStateBeforeEdit_Movie.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateBeforeEdit_TVEpisode.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateScraperMulti_Movie.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperMulti_TVEpisode.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperSingle_Movie.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        chkGetWatchedStateScraperSingle_TVEpisode.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        gbGetWatchedState.Text = Master.eLang.GetString(1071, "Watched State")
        gbGetWatchedStateMovies.Text = Master.eLang.GetString(36, "Movies")
        gbGetWatchedStateTVEpisodes.Text = Master.eLang.GetString(682, "Episodes")
        gbSettingsGeneral.Text = Master.eLang.GetString(38, "General Settings")
        lblPassword.Text = Master.eLang.GetString(426, "Password")
        lblUsername.Text = Master.eLang.GetString(425, "Username")
        txtPassword.PasswordChar = "*"c
    End Sub

    Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
        RaiseEvent ModuleSettingsChanged()
        RaiseEvent AccountSettingsChanged()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        RaiseEvent ModuleSettingsChanged()
        RaiseEvent AccountSettingsChanged()
    End Sub

    Private Sub chkGetWatchedState_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetWatchedState.CheckedChanged
        gbGetWatchedStateMovies.Enabled = chkGetWatchedState.Checked
        gbGetWatchedStateTVEpisodes.Enabled = chkGetWatchedState.Checked
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub EnableApplyButton() Handles chkGetShowProgress.CheckedChanged,
        chkGetWatchedStateBeforeEdit_Movie.CheckedChanged,
        chkGetWatchedStateBeforeEdit_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperMulti_Movie.CheckedChanged,
        chkGetWatchedStateScraperMulti_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperSingle_Movie.CheckedChanged,
        chkGetWatchedStateScraperSingle_TVEpisode.CheckedChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class