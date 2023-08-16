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

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order < Addons.Instance.Image_Scrapers_TV.Count - 1 Then
            Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order - 1
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
            lblEMMAPI.Visible = True
            txtApiKey.Enabled = False
            txtApiKey.Text = String.Empty
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkScrapeSeasonBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowFanart.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeSeasonPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonPoster.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowPoster.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Image_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If Addons.Instance.Image_Scrapers_TV.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Image_Scrapers_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkScrapeEpisodePoster.Text = Master.eLang.GetString(148, "Poster")
        chkScrapeSeasonBanner.Text = Master.eLang.GetString(838, "Banner")
        chkScrapeSeasonPoster.Text = Master.eLang.GetString(148, "Poster")
        chkScrapeShowBanner.Text = Master.eLang.GetString(838, "Banner")
        chkScrapeShowFanart.Text = Master.eLang.GetString(149, "Fanart")
        chkScrapeShowPoster.Text = Master.eLang.GetString(148, "Poster")
        gbScraperImagesOpts.Text = Master.eLang.GetString(268, "Images - Scraper specific")
        gbScraperImagesEpisode.Text = Master.eLang.GetString(727, "Episode")
        gbScraperImagesSeason.Text = Master.eLang.GetString(650, "Season")
        gbScraperImagesTVShow.Text = Master.eLang.GetString(700, "TV Show")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblAPIKey.Text = String.Concat(Master.eLang.GetString(932, "TVDB API Key"), ":")
        lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

    Private Sub txtApiKey_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub pbApiKeyInfo_Click(sender As Object, e As EventArgs) Handles pbApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

#End Region 'Methods

End Class