﻿' ################################################################################
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

Public Class frmSettingsHolder_MovieSet

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

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Image_MovieSet.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkScrapeBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeClearArt_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeClearArt.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        Me.chkScrapeClearArtOnlyHD.Enabled = Me.chkScrapeClearArt.Checked

        If Not Me.chkScrapeClearArt.Checked Then
            Me.chkScrapeClearArtOnlyHD.Checked = False
        End If
    End Sub

    Private Sub chkScrapeClearArtOnlyHD_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeClearArtOnlyHD.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeClearLogo_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeClearLogo.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        Me.chkScrapeClearLogoOnlyHD.Enabled = Me.chkScrapeClearLogo.Checked

        If Not Me.chkScrapeClearLogo.Checked Then
            Me.chkScrapeClearLogoOnlyHD.Checked = False
        End If
    End Sub

    Private Sub chkScrapeClearLogoOnlyHD_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeClearLogoOnlyHD.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeDiscArt_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeDiscArt.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeFanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeFanart.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeLandscape_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeLandscape.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapePoster_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapePoster.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_MovieSet.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Image_MovieSet.Count > 1 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Image_MovieSet.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkScrapeBanner.Text = Master.eLang.GetString(838, "Banner")
        Me.chkScrapeClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        Me.chkScrapeClearArtOnlyHD.Text = Master.eLang.GetString(1105, "Only HD")
        Me.chkScrapeClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        Me.chkScrapeClearLogoOnlyHD.Text = Me.chkScrapeClearArtOnlyHD.Text
        Me.chkScrapeDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        Me.chkScrapeFanart.Text = Master.eLang.GetString(149, "Fanart")
        Me.chkScrapeLandscape.Text = Master.eLang.GetString(1035, "Landscape")
        Me.chkScrapePoster.Text = Master.eLang.GetString(148, "Poster")
        Me.gbScraperImagesOpts.Text = Master.eLang.GetString(268, "Images - Scraper specific")
        Me.gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        Me.lblAPIHint.Text = Master.eLang.GetString(1248, "Using a Personal API Key reduces the time you have to wait for new images to show up from 7 days to 48 hours.")
        Me.lblAPIKey.Text = String.Concat(Master.eLang.GetString(789, "Fanart.tv Personal API Key"), ":")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        Me.lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

    Private Sub txtApiKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub pbApiKeyInfo_Click(sender As System.Object, e As System.EventArgs) Handles pbApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
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

#End Region 'Methods

End Class