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

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        RaiseEvent StateChanged(chkEnabled.Checked, 1)
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        RaiseEvent StateChanged(chkEnabled.Checked, -1)
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent StateChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub cbForceTitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForceTitleLanguage.SelectedIndexChanged
        If cbForceTitleLanguage.SelectedIndex = -1 OrElse cbForceTitleLanguage.Text = "" Then
            chkFallBackworldwide.Checked = False
            chkFallBackworldwide.Enabled = False
        Else
            chkFallBackworldwide.Enabled = True
        End If

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowActors.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowCertifications_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCertifications.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowCountries_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCountries.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowCreators_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCreators.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowGenres_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowGenres.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowOriginalTitle.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPlot.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowPremiered_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPremiered.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowRating.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowRuntime_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowRuntime.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowStudios_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowStudios.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperShowTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowTitle.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpActors.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpAired_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpAired.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpCredits_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpCredits.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpDirectors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpDirectors.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpPlot.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpRating.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkScraperEpTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpTitle.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub chkFallBackworldwide_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallBackworldwide.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState)
        btnDown.Enabled = OrderState.Position < OrderState.TotalCount - 1
        btnUp.Enabled = OrderState.Position > 0
    End Sub

    Private Sub Setup()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackworldwide.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        chkScraperEpActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperEpAired.Text = Master.eLang.GetString(728, "Aired")
        chkScraperEpCredits.Text = Master.eLang.GetString(394, "Credits (Writers)")
        chkScraperEpDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkScraperEpPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperEpRating.Text = Master.eLang.GetString(400, "Rating")
        chkScraperEpTitle.Text = Master.eLang.GetString(21, "Title")
        chkScraperShowActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperShowCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkScraperShowCountries.Text = Master.eLang.GetString(237, "Countries")
        chkScraperShowGenres.Text = Master.eLang.GetString(725, "Genres")
        chkScraperShowOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkScraperShowRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkScraperShowStudios.Text = Master.eLang.GetString(226, "Studios")
        chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblForceTitleLanguage.Text = Master.eLang.GetString(710, "Force Title Language:")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

End Class