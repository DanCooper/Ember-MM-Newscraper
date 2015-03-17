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

Public Class frmTVMediaSettingsHolder

#Region "Fields"

    Private _api As String

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupPostScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Methods"

    Public Sub New()
        _api = String.Empty
        InitializeComponent()
        Me.SetUp()
        orderChanged()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsScraper).Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupPostScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupPostScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupPostScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub cbTVLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTVScraperLanguage.SelectedIndexChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGetEnglishImages_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGetEnglishImages.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOnlyTVImagesLanguage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnlyTVImagesLanguage.CheckedChanged
        Me.chkGetEnglishImages.Enabled = Me.chkOnlyTVImagesLanguage.Checked
        If Not Me.chkOnlyTVImagesLanguage.Checked Then Me.chkGetEnglishImages.Checked = False
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_TV.FirstOrDefault(Function(p) p.AssemblyName = TVDB_Data_Poster._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_TV.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_TV.Where(Function(y) y.ProcessorModule.IsPostScraper).Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub pbTVDB_Click(sender As System.Object, e As System.EventArgs) Handles pbTVDB.Click
        If Master.isWindows Then
            Process.Start("http://thetvdb.com/?tab=apiregister")
        Else
            Using Explorer As New Process
                Explorer.StartInfo.FileName = "xdg-open"
                Explorer.StartInfo.Arguments = "http://thetvdb.com/?tab=apiregister"
                Explorer.Start()
            End Using
        End If
    End Sub

    Private Sub SetUp()
        Me.btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        Me.cbTVScraperLanguage.Items.AddRange((From lLang In Master.eSettings.TVGeneralLanguages.Language Select lLang.name).ToArray)
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        Me.chkOnlyTVImagesLanguage.Text = Master.eLang.GetString(736, "Only Get Images for the Selected Language")
        Me.gbLanguage.Text = Master.eLang.GetString(610, "Language")
        Me.lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager API key")
        Me.lblModuleInfo.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.lblTVDBApiKey.Text = Master.eLang.GetString(932, "TVDB API Key")
        Me.lblTVDBMirror.Text = Master.eLang.GetString(801, "TVDB Mirror")
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
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.Enter
        _api = txtApiKey.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtApiKey_TestValidated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApiKey.Validated
        If Not (_api = txtApiKey.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTVDBMirror_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTVDBMirror.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class