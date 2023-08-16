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

Public Class frmSettingsHolder_Movie

#Region "Events"

    Public Event ModuleSettingsChanged()
    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub Setup()
        chkActors.Text = Master.eLang.GetString(231, "Actors")
        chkCertifications.Text = Master.eLang.GetString(56, "Certifications")
        chkCountries.Text = Master.eLang.GetString(237, "Countries")
        chkDirectors.Text = Master.eLang.GetString(940, "Directors")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackworldwide.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        chkGenres.Text = Master.eLang.GetString(725, "Genres")
        chkMPAA.Text = Master.eLang.GetString(401, "MPAA")
        chkMPAADescription.Text = String.Format("{0} *", Master.eLang.GetString(1088, "include Description"))
        chkOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        chkPlot.Text = Master.eLang.GetString(65, "Plot")
        chkPartialTitles.Text = Master.eLang.GetString(1183, "Partial Titles")
        chkPopularTitles.Text = Master.eLang.GetString(1182, "Popular Titles")
        chkPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkRating.Text = Master.eLang.GetString(245, "Rating")
        chkRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkShortTitles.Text = Master.eLang.GetString(837, "Short Titles")
        chkStudios.Text = Master.eLang.GetString(226, "Studios")
        chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkTitle.Text = Master.eLang.GetString(21, "Title")
        chkTop250.Text = Master.eLang.GetString(591, "Top250")
        chkTvTitles.Text = Master.eLang.GetString(1184, "TV Movie Titles")
        chkVideoTitles.Text = Master.eLang.GetString(1185, "Video Titles")
        chkWriters.Text = Master.eLang.GetString(394, "Writers")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblForceTitleLanguage.Text = Master.eLang.GetString(710, "Force Title Language:")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblInfoParsing.Text = String.Format("* {0}", Master.eLang.GetString(1380, "additional page(s) to parse, needs longer to scrape"))
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
        chkStudiowithDistributors.Text = Master.eLang.GetString(1456, "Include Distributors")
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If order < Addons.Instance.Data_Scrapers_Movie.Count - 1 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkCast_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkActors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCertification_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCertifications.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMPAA.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If chkMPAA.Checked Then
            chkMPAADescription.Enabled = True
        Else
            chkMPAADescription.Checked = False
            chkMPAADescription.Enabled = False
        End If
    End Sub

    Private Sub ModuleSettingsChanged(sender As Object, e As EventArgs) Handles chkMPAADescription.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkWriters_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkWriters.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkDirector_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkDirectors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGenres.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOutline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPartialTitles_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPartialTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPopularTitles_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPopularTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRating_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPremiered.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRuntime.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkStudio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkStudios.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If chkStudios.Checked Then
            chkStudiowithDistributors.Enabled = True
        Else
            chkStudiowithDistributors.Checked = False
            chkStudiowithDistributors.Enabled = False
        End If
    End Sub

    Private Sub chkStudiowithDistributors_CheckedChanged(sender As Object, e As EventArgs) Handles chkStudiowithDistributors.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTagline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOriginalTitle_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOriginalTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTop250_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTop250.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTvTiles_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTvTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCountries.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkVideoTitles_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkVideoTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkShortTitles_CheckedChanged(sender As Object, e As EventArgs) Handles chkShortTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackworldwide_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallBackworldwide.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub cbForceTitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForceTitleLanguage.SelectedIndexChanged
        If cbForceTitleLanguage.SelectedIndex = -1 OrElse cbForceTitleLanguage.Text = String.Empty Then
            chkFallBackworldwide.Checked = False
            chkFallBackworldwide.Enabled = False
        Else
            chkFallBackworldwide.Enabled = True
        End If

        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyName = Addon._AssemblyName).Order
        If Addons.Instance.Data_Scrapers_Movie.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Data_Scrapers_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class