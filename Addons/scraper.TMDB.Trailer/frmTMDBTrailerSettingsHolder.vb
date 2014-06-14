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

Public Class frmTMDBTrailerSettingsHolder

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

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()
#End Region 'Events

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Trailer._AssemblyName).ScraperOrder
        If order < ModulesManager.Instance.externalTrailerScrapersModules.Count - 1 Then
            ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order + 1).ScraperOrder = order
            ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Trailer._AssemblyName).ScraperOrder = order + 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Trailer._AssemblyName).ScraperOrder
        If order > 0 Then
            ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.ScraperOrder = order - 1).ScraperOrder = order
            ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Trailer._AssemblyName).ScraperOrder = order - 1
            RaiseEvent SetupScraperChanged(cbEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(cbEnabled.Checked, 0)
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalTrailerScrapersModules.FirstOrDefault(Function(p) p.AssemblyName = TMDB_Trailer._AssemblyName).ScraperOrder
        If ModulesManager.Instance.externalTrailerScrapersModules.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalTrailerScrapersModules.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Sub SetUp()
        Me.Label3.Text = Master.eLang.GetString(168, "Scrape Order")
        Me.cbEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.Label1.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), vbCrLf)
        Me.Label18.Text = Master.eLang.GetString(870, "TMDB API Key")
        Me.gbSettings.Text = Master.eLang.GetString(937, "TMDB")
        Me.chkFallBackEng.Text = Master.eLang.GetString(922, "Fall back on english")
        Me.Label2.Text = Master.eLang.GetString(741, "Preferred Language:")
    End Sub

    Private Sub txtTMDBApiKey_TextEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBApiKey.Enter
        _api = txtTMDBApiKey.Text
    End Sub

    Private Sub txtTMDBApiKey_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub txtTMDBApiKey_TextValidated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTMDBApiKey.Validated
        If Not (_api = txtTMDBApiKey.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
    End Sub

    Private Sub cbTMDBPrefLanguage_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTMDBPrefLanguage.SelectedIndexChanged
        If Not (_language = cbTMDBPrefLanguage.Text) Then
            RaiseEvent SetupNeedsRestart()
        End If
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackEng_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFallBackEng.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


#End Region 'Methods

End Class