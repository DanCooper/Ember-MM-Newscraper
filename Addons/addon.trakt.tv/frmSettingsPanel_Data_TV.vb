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

Public Class frmSettingsPanel_Data_TV

#Region "Events"

    Public Event NeedsRestart()
    Public Event SettingsChanged()
    Public Event StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)

#End Region 'Events

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        RaiseEvent StateChanged(chkEnabled.Checked, 1)
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        RaiseEvent StateChanged(chkEnabled.Checked, -1)
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent StateChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub ChangedSetting(sender As Object, e As EventArgs) Handles _
        chkScraperEpisodeRating.CheckedChanged,
        chkScraperEpisodeUserRating.CheckedChanged,
        chkScraperShowRating.CheckedChanged,
        chkScraperShowUserRating.CheckedChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpisodeRating.CheckedChanged, chkScraperEpisodeUserRating.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)
        btnDown.Enabled = OrderState.Position < OrderState.TotalCount - 1
        btnUp.Enabled = OrderState.Position > 0
    End Sub

    Private Sub Setup()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkScraperEpisodeRating.Text = Master.eLang.GetString(400, "Rating")
        chkScraperEpisodeUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        chkScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        chkScraperShowUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        gbScraperFieldsEpisode.Text = Master.eLang.GetString(727, "Episode")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperFieldsShow.Text = Master.eLang.GetString(743, "Show")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

End Class