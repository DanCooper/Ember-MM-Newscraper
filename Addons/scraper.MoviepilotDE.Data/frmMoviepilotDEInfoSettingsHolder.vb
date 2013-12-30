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

Public Class frmMoviepilotDEInfoSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = MoviepilotDE_Data._AssemblyName).ScraperOrder
        If order < ModulesManager.Instance.externalDataScrapersModules.Count - 1 Then
            ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order + 1).ScraperOrder = order
            ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = MoviepilotDE_Data._AssemblyName).ScraperOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = MoviepilotDE_Data._AssemblyName).ScraperOrder
        If order > 0 Then
            ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order - 1).ScraperOrder = order
            ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = MoviepilotDE_Data._AssemblyName).ScraperOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub bEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Private Sub chkMoviepilotGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMoviepilotOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviepilotOutline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMoviepilotPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviepilotPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkkMoviepilotRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoviepilotRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMoviepilotPlotCleanPlotOutline_CheckedChanged(sender As Object, e As EventArgs) Handles chkMoviepilotCleanPlotOutline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalDataScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = MoviepilotDE_Data._AssemblyName).ScraperOrder
        If ModulesManager.Instance.externalDataScrapersModules.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalDataScrapersModules.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.chkMoviepilotPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkMoviepilotOutline.Text = Master.eLang.GetString(64, "Outline")
        Me.chkMoviepilotRating.Text = Master.eLang.GetString(722, "Rating")
        Me.chkMoviepilotCleanPlotOutline.Text = Master.eLang.GetString(985, "Clean Plot/Outline")

        Me.gbOptions.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        Me.Label2.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.Label1.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
    End Sub

#End Region 'Methods


End Class