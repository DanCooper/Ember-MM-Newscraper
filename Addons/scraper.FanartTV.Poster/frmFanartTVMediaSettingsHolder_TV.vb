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

Public Class frmFanartTVMediaSettingsHolder_TV

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Fields"

    Private _api As String
    Private _language As String

#End Region 'Fields

#Region "Properties"

    Public Property API() As String
        Get
            Return Me._api
        End Get
        Set(ByVal value As String)
            Me._api = value
        End Set
    End Property

    Public Property Lang() As String
        Get
            Return Me._language
        End Get
        Set(ByVal value As String)
            Me._language = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Image_TV.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order - 1
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

    Private Sub chkGetBlankImages_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetBlankImages.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGetEnglishImages_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetEnglishImages.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPrefLanguageOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkPrefLanguageOnly.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        Me.chkGetBlankImages.Enabled = Me.chkPrefLanguageOnly.Checked
        Me.chkGetEnglishImages.Enabled = Me.chkPrefLanguageOnly.Checked

        If Not Me.chkPrefLanguageOnly.Checked Then
            Me.chkGetBlankImages.Checked = False
            Me.chkGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub chkScrapeSeasonBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowClearArt_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowClearArt.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        Me.chkScrapeShowClearArtOnlyHD.Enabled = Me.chkScrapeShowClearArt.Checked

        If Not Me.chkScrapeShowClearArt.Checked Then
            Me.chkScrapeShowClearArtOnlyHD.Checked = False
        End If
    End Sub

    Private Sub chkScrapeShowClearArtOnlyHD_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowClearArtOnlyHD.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowClearLogo_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowClearLogo.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        Me.chkScrapeShowClearLogoOnlyHD.Enabled = Me.chkScrapeShowClearLogo.Checked

        If Not Me.chkScrapeShowClearLogo.Checked Then
            Me.chkScrapeShowClearLogoOnlyHD.Checked = False
        End If
    End Sub

    Private Sub chkScrapeShowClearLogoOnlyHD_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowClearLogoOnlyHD.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowCharacterArt_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowCharacterArt.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeSeasonFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonFanart.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowFanart.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeSeasonLandscape_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonLandscape.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowLandscape_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowLandscape.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeSeasonPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeSeasonPoster.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeShowPoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeShowPoster.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_TV.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Image_TV.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Image_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        Me.chkGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        Me.chkPrefLanguageOnly.Text = Master.eLang.GetString(736, "Only Get Images for the Selected Language")
        Me.chkScrapeSeasonBanner.Text = Master.eLang.GetString(1051, "Get Banner")
        Me.chkScrapeSeasonFanart.Text = Master.eLang.GetString(940, "Get Fanart")
        Me.chkScrapeSeasonLandscape.Text = Master.eLang.GetString(1056, "Get Landscape")
        Me.chkScrapeSeasonPoster.Text = Master.eLang.GetString(939, "Get Poster")
        Me.chkScrapeShowBanner.Text = Master.eLang.GetString(1051, "Get Banner")
        Me.chkScrapeShowClearArt.Text = Master.eLang.GetString(1053, "Get ClearArt")
        Me.chkScrapeShowClearArtOnlyHD.Text = Master.eLang.GetString(1105, "Only HD")
        Me.chkScrapeShowClearLogo.Text = Master.eLang.GetString(1054, "Get ClearLogo")
        Me.chkScrapeShowClearLogoOnlyHD.Text = Me.chkScrapeShowClearArtOnlyHD.Text
        Me.chkScrapeShowCharacterArt.Text = Master.eLang.GetString(1052, "Get CharacterArt")
        Me.chkScrapeShowFanart.Text = Master.eLang.GetString(940, "Get Fanart")
        Me.chkScrapeShowLandscape.Text = Master.eLang.GetString(1056, "Get Landscape")
        Me.chkScrapeShowPoster.Text = Master.eLang.GetString(939, "Get Poster")
        Me.gbScraperImagesOpts.Text = Master.eLang.GetString(268, "Images - Scraper specific")
        Me.gbScraperImagesSeason.Text = Master.eLang.GetString(650, "Season")
        Me.gbScraperImagesTVShow.Text = Master.eLang.GetString(700, "TV Show")
        Me.gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        Me.lblAPIHint.Text = Master.eLang.GetString(1248, "Using a Personal API Key reduces the time you have to wait for new images to show up from 7 days to 48 hours.")
        Me.lblAPIKey.Text = String.Concat(Master.eLang.GetString(789, "Fanart.tv Personal API Key"), ":")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        Me.lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblPrefLanguage.Text = String.Concat(Master.eLang.GetString(741, "Preferred Language"), ":")
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

    Private Sub txtApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.Enter
        _api = txtApiKey.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbPrefLanguage_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPrefLanguage.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub pbApiKeyInfo_Click(sender As System.Object, e As System.EventArgs) Handles pbApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

#End Region 'Methods

End Class