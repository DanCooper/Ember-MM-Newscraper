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

Public Class frmFanartTVMediaSettingsHolder_Movie

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
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Image_Movie.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
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

    Private Sub chkScrapeBanner_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeBanner.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScrapeCharacterArt_CheckedChanged(sender As Object, e As EventArgs) Handles chkScrapeCharacterArt.CheckedChanged
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
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Image_Movie.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Image._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Image_Movie.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Image_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        Me.Label1.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
        Me.Label3.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        Me.chkGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        Me.chkPrefLanguageOnly.Text = Master.eLang.GetString(736, "Only Get Images for the Selected Language")
        Me.chkScrapeBanner.Text = Master.eLang.GetString(1051, "Get Banner")
        Me.chkScrapeCharacterArt.Text = Master.eLang.GetString(1052, "Get CharacterArt")
        Me.chkScrapeClearArt.Text = Master.eLang.GetString(1053, "Get ClearArt")
        Me.chkScrapeClearArtOnlyHD.Text = Master.eLang.GetString(1105, "Only HD")
        Me.chkScrapeClearLogo.Text = Master.eLang.GetString(1054, "Get ClearLogo")
        Me.chkScrapeClearLogoOnlyHD.Text = Me.chkScrapeClearArtOnlyHD.Text
        Me.chkScrapeDiscArt.Text = Master.eLang.GetString(1055, "Get DiscArt")
        Me.chkScrapeFanart.Text = Master.eLang.GetString(940, "Get Fanart")
        Me.chkScrapeLandscape.Text = Master.eLang.GetString(1056, "Get Landscape")
        Me.chkScrapePoster.Text = Master.eLang.GetString(939, "Get Poster")
        Me.gbImages.Text = Master.eLang.GetString(497, "Images")
        Me.gbScraper.Text = Master.eLang.GetString(788, "Fanart.tv")
        Me.lblAPIHint.Text = Master.eLang.GetString(1248, "Using a Personal API Key reduces the time you have to wait for new images to show up from 7 days to 48 hours.")
        Me.lblAPIKey.Text = Master.eLang.GetString(789, "Fanart.tv Personal API Key:")
        Me.lblPrefLanguage.Text = Master.eLang.GetString(741, "Preferred Language:")
    End Sub

    Private Sub txtApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.Enter
        _api = txtApiKey.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbPrefLanguage_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPrefLanguage.SelectedIndexChanged
        If Not (_language = cbPrefLanguage.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub pbFANARTTV_Click(sender As System.Object, e As System.EventArgs) Handles pbFANARTTV.Click
        If Master.isWindows Then
            Process.Start("http://fanart.tv/get-an-api-key/")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://fanart.tv/get-an-api-key/"
                Explorer.Start()
            End Using
        End If
    End Sub

#End Region 'Methods

End Class