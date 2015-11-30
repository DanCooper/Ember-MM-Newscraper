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

Public Class frmSettingsHolder_TV

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()

#End Region 'Events

#Region "Fields"

    Private _api As String
    Private _language As String
    Private _getadultitems As Boolean

#End Region 'Fields

#Region "Properties"

    Public Property API() As String
        Get
            Return _api
        End Get
        Set(ByVal value As String)
            _api = value
        End Set
    End Property

    Public Property Lang() As String
        Get
            Return _language
        End Get
        Set(ByVal value As String)
            _language = value
        End Set
    End Property

    Public Property GetAdultItems() As Boolean
        Get
            Return _getadultitems
        End Get
        Set(ByVal value As Boolean)
            _getadultitems = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        _api = String.Empty
        _language = String.Empty
        _getadultitems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False)
        InitializeComponent()
        SetUp()
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Data_TV.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub
    Private Sub chkWriters_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkDirector_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCollectionID_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGetAdult_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOriginalTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRating_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTrailer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkYear_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackEng_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbTMDBPrefLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_TV.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Data_TV.Count > 1 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Data_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

    Private Sub chkScraperShowGenre_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowGenre.CheckedChanged

    End Sub

    Private Sub chkScraperShowMPAA_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCert.CheckedChanged

    End Sub

    Private Sub chkScraperShowPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPlot.CheckedChanged

    End Sub

    Private Sub chkScraperShowPremiered_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPremiered.CheckedChanged

    End Sub

    Private Sub chkScraperShowRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowRating.CheckedChanged

    End Sub

    Private Sub chkScraperShowStudio_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowStudio.CheckedChanged

    End Sub

    Private Sub chkScraperShowActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowActors.CheckedChanged

    End Sub

    Private Sub chkScraperShowStatus_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowStatus.CheckedChanged

    End Sub

    Private Sub chkScraperEpTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpTitle.CheckedChanged

    End Sub

    Private Sub chkScraperEpPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpPlot.CheckedChanged

    End Sub

    Private Sub chkScraperEpSeason_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkScraperEpEpisode_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub chkScraperEpDirector_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpDirector.CheckedChanged

    End Sub

    Private Sub chkScraperEpCredits_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpCredits.CheckedChanged

    End Sub

    Private Sub chkScraperEpActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpActors.CheckedChanged

    End Sub

    Private Sub chkScraperEpAired_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpAired.CheckedChanged

    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpRating.CheckedChanged

    End Sub

    Private Sub chkScraperEpGuestStars_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpGuestStars.CheckedChanged

    End Sub
End Class