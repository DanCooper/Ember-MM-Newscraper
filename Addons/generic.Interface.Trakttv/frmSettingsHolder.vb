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
        SetUp()
    End Sub

    Private Sub SetUp()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkGetWatchedStateBeforeEdit_Movie.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateBeforeEdit_TVEpisode.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateScraperMulti_Movie.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperMulti_TVEpisode.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperSingle_Movie.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        chkGetWatchedStateScraperSingle_TVEpisode.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        gbGetWatchedState.Text = Master.eLang.GetString(1070, "Get Watched State")
        gbGetWatchedStateMovies.Text = Master.eLang.GetString(36, "Movies")
        gbGetWatchedStateTVEpisodes.Text = Master.eLang.GetString(682, "Episodes")
    End Sub

    Private Sub EnableApplyButton() Handles _
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