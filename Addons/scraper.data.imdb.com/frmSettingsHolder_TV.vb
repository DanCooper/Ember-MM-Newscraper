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
    Public Event SetupNeedsRestart()
    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

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

#Region "Dialog Methods"

    Public Sub New()
        _api = String.Empty
        _language = String.Empty
        _getadultitems = Master.eAdvancedSettings.GetBooleanSetting("GetAdultItems", False)
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackworldwide.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        chkScraperEpActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperEpAired.Text = Master.eLang.GetString(728, "Aired")
        chkScraperEpCredits.Text = Master.eLang.GetString(394, "Credits (Writers)")
        chkScraperEpDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkScraperEpPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperEpRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperEpTitle.Text = Master.eLang.GetString(21, "Title")
        chkScraperShowActors.Text = Master.eLang.GetString(231, "Actors")
        chkScraperShowCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkScraperShowCountries.Text = Master.eLang.GetString(237, "Countries")
        chkScraperShowGenres.Text = Master.eLang.GetString(725, "Genres")
        chkScraperShowOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkScraperShowRating.Text = Master.eLang.GetString(245, "Rating")
        chkScraperShowPlot.Text = Master.eLang.GetString(65, "Plot")
        chkScraperShowPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkScraperShowRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkScraperShowStudios.Text = Master.eLang.GetString(226, "Studios")
        chkScraperShowTitle.Text = Master.eLang.GetString(21, "Title")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblForceTitleLanguage.Text = Master.eLang.GetString(710, "Force Title Language:")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If order < Addons.Instance.Data_Scrapers_TV.Count - 1 Then
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub cbForceTitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForceTitleLanguage.SelectedIndexChanged
        If cbForceTitleLanguage.SelectedIndex = -1 OrElse String.IsNullOrEmpty(cbForceTitleLanguage.Text) Then
            chkFallBackworldwide.Checked = False
            chkFallBackworldwide.Enabled = False
        Else
            chkFallBackworldwide.Enabled = True
        End If

        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowActors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowCertifications_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCertifications.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowCountries_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCountries.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowCreators_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowCreators.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowGenres_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowGenres.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowOriginalTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowOriginalTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowPremiered_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowPremiered.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowRuntime_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowRuntime.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowStudios_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowStudios.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperShowTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperShowTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpActors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpActors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpAired_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpAired.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpCredits_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpCredits.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpDirectors_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpDirectors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpPlot_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpRating_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkScraperEpTitle_CheckedChanged(sender As Object, e As EventArgs) Handles chkScraperEpTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackworldwide_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallBackworldwide.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Data_Scrapers_TV.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If Addons.Instance.Data_Scrapers_TV.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Data_Scrapers_TV.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class