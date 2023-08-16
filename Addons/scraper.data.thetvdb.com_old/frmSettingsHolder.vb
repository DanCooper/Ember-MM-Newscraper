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

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Fields"

#End Region 'Fields

#Region "Properties"

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        SetUp()
    End Sub

    Private Sub pbApiKeyInfo_Click(sender As Object, e As EventArgs) Handles pbApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order < Addons.Instance.Data_Scrapers_TV.Count - 1 Then
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUnlockAPI_Click(sender As Object, e As EventArgs) Handles btnUnlockAPI.Click
        If btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key") Then
            btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            lblEMMAPI.Visible = False
            txtApiKey.Enabled = True
        Else
            btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
            chkScraperShowEpisodeGuide.Checked = False
            lblEMMAPI.Visible = True
            txtApiKey.Enabled = False
            txtApiKey.Text = String.Empty
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkScraperShowEpisodeGuide_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkScraperShowEpisodeGuide.CheckedChanged
        If String.IsNullOrEmpty(txtApiKey.Text) AndAlso chkScraperShowEpisodeGuide.Checked Then
            MessageBox.Show(Master.eLang.GetString(1133, "You need your own API key for that"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Information)
            chkScraperShowEpisodeGuide.Checked = False
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTMDBApiKey_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkFallBackEng.CheckedChanged,
        chkScraperEpisodeActors.CheckedChanged,
        chkScraperEpisodeAired.CheckedChanged,
        chkScraperEpisodeCredits.CheckedChanged,
        chkScraperEpisodeDirectors.CheckedChanged,
        chkScraperEpisodeGuestStars.CheckedChanged,
        chkScraperEpisodePlot.CheckedChanged,
        chkScraperEpisodeRating.CheckedChanged,
        chkScraperEpisodeTitle.CheckedChanged,
        chkScraperShowActors.CheckedChanged,
        chkScraperShowGenres.CheckedChanged,
        chkScraperShowMPAA.CheckedChanged,
        chkScraperShowPlot.CheckedChanged,
        chkScraperShowPremiered.CheckedChanged,
        chkScraperShowRating.CheckedChanged,
        chkScraperShowRuntime.CheckedChanged,
        chkScraperShowStatus.CheckedChanged,
        chkScraperShowStudios.CheckedChanged,
        chkScraperShowTitle.CheckedChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If Addons.Instance.Data_Scrapers_TV.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Data_Scrapers_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackEng.Text = Master.eLang.GetString(922, "Fallback to english")
        chkScraperEpisodeActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperEpisodeAired.Text = Master.eLang.GetString(728, "Aired")
        chkScraperEpisodeCredits.Text = Master.eLang.GetString(394, "Credits (Writers)")
        chkScraperEpisodeDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkScraperEpisodeGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        chkScraperEpisodePlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperEpisodeRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperEpisodeTitle.Text = Master.eLang.GetString(21, "Title")
        chkScraperShowActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperShowEpisodeGuide.Text = Master.eLang.GetString(723, "Episode Guide URL")
        chkScraperShowGenres.Text = Master.eLang.GetString(725, "Genres")
        chkScraperShowMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkScraperShowRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperShowRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkScraperShowStatus.Text = Master.eLang.GetString(215, "Status")
        chkScraperShowStudios.Text = Master.eLang.GetString(226, "Studios")
        chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblApiKey.Text = String.Concat(Master.eLang.GetString(932, "TVDB API Key"), ":")
        lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

End Class