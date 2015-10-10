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

Imports System.IO
Imports EmberAPI
Imports System.Diagnostics

Public Class frmSettingsHolder_TV

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
        Me.SetUp()
    End Sub

    Private Sub pbTMDBApiKeyInfo_Click(sender As System.Object, e As System.EventArgs) Handles pbTMDBApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Data_TV.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUnlockAPI_Click(sender As Object, e As EventArgs) Handles btnUnlockAPI.Click
        If Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key") Then
            Me.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            Me.lblEMMAPI.Visible = False
            Me.txtApiKey.Enabled = True
        Else
            Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
            Me.lblEMMAPI.Visible = True
            Me.txtApiKey.Enabled = False
            Me.txtApiKey.Text = String.Empty
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub SettingsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        chkFallBackEng.CheckedChanged, _
        chkGetAdultItems.CheckedChanged, _
        chkScraperEpisodeActors.CheckedChanged, _
        chkScraperEpisodeAired.CheckedChanged, _
        chkScraperEpisodeCredits.CheckedChanged, _
        chkScraperEpisodeDirector.CheckedChanged, _
        chkScraperEpisodeGuestStars.CheckedChanged, _
        chkScraperEpisodePlot.CheckedChanged, _
        chkScraperEpisodeRating.CheckedChanged, _
        chkScraperEpisodeTitle.CheckedChanged, _
        chkScraperSeasonAired.CheckedChanged, _
        chkScraperSeasonPlot.CheckedChanged, _
        chkScraperSeasonTitle.CheckedChanged, _
        chkScraperShowActors.CheckedChanged, _
        chkScraperShowCert.CheckedChanged, _
        chkScraperShowCountry.CheckedChanged, _
        chkScraperShowCreators.CheckedChanged, _
        chkScraperShowGenre.CheckedChanged, _
        chkScraperShowOriginalTitle.CheckedChanged, _
        chkScraperShowPlot.CheckedChanged, _
        chkScraperShowPremiered.CheckedChanged, _
        chkScraperShowRating.CheckedChanged, _
        chkScraperShowRuntime.CheckedChanged, _
        chkScraperShowStatus.CheckedChanged, _
        chkScraperShowStudio.CheckedChanged, _
        chkScraperShowTitle.CheckedChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTMDBApiKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Data._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Data_TV.Count > 1 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Data_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkFallBackEng.Text = Master.eLang.GetString(922, "Fallback to english")
        Me.chkGetAdultItems.Text = Master.eLang.GetString(1046, "Include Adult Items")
        Me.chkScraperEpisodeActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkScraperEpisodeAired.Text = Master.eLang.GetString(728, "Aired")
        Me.chkScraperEpisodeCredits.Text = Master.eLang.GetString(394, "Credits (Writers)")
        Me.chkScraperEpisodeDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkScraperEpisodeGuestStars.Text = Master.eLang.GetString(508, "Guest Stars")
        Me.chkScraperEpisodePlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperEpisodeRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperEpisodeTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperSeasonAired.Text = Master.eLang.GetString(728, "Aired")
        Me.chkScraperSeasonPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperSeasonTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkScraperShowActors.Text = Master.eLang.GetString(725, "Actors")
        Me.chkScraperShowCert.Text = Master.eLang.GetString(56, "Certification(s)")
        Me.chkScraperShowCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkScraperShowCreators.Text = Master.eLang.GetString(744, "Creator(s)")
        Me.chkScraperShowGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkScraperShowOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        Me.chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        Me.chkScraperShowRating.Text = Master.eLang.GetString(400, "Rating")
        Me.chkScraperShowRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkScraperShowStatus.Text = Master.eLang.GetString(215, "Status")
        Me.chkScraperShowStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        Me.gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        Me.gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        Me.lblApiKey.Text = String.Concat(Master.eLang.GetString(870, "TMDB API Key"), ":")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        Me.lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

End Class