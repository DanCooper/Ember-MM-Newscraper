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

Public Class frmFanartTVMediaSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Poster._AssemblyName).ScraperOrder
        If order < ModulesManager.Instance.externalPosterScrapersModules.Count - 1 Then
            ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order + 1).ScraperOrder = order
            ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Poster._AssemblyName).ScraperOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Poster._AssemblyName).ScraperOrder
        If order > 0 Then
            ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order - 1).ScraperOrder = order
            ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Poster._AssemblyName).ScraperOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalPosterScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = FanartTV_Poster._AssemblyName).ScraperOrder
        If ModulesManager.Instance.externalPosterScrapersModules.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalPosterScrapersModules.Count - 1)
            btnUp.Enabled = (order > 1)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        Me.Label5.Text = Master.eLang.GetString(124, "Fanart.tv API Key:")
        Me.Label3.Text = Master.eLang.GetString(168, "Scrape Order", True)
        Me.GroupBox1.Text = Master.eLang.GetString(125, "Fanart.tv")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled", True)
        Me.Label1.Text = String.Format(Master.eLang.GetString(103, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
    End Sub

    Private Sub txtFANARTTVApiKey_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFANARTTVApiKey.TextChanged
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