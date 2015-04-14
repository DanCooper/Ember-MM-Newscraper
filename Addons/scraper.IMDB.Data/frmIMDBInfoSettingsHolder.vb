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

Public Class frmIMDBInfoSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

#End Region 'Events

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order + 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If order > 0 Then
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.ModuleOrder = order - 1).ModuleOrder = order
            ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub chkCast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCast.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCertification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCertification.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMPAA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMPAA.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkWriters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWriters.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If Me.chkWriters.Checked = False Then
            Me.chkFullCrew.Checked = False
            Me.chkCrew.Checked = False
            Me.chkMusicBy.Checked = False
            Me.chkProducers.Checked = False
            Me.gbScraperFieldsCredits.Enabled = False
        Else
            Me.chkFullCrew.Enabled = True
            Me.chkCrew.Enabled = True
            Me.chkMusicBy.Enabled = True
            Me.chkProducers.Enabled = True
            Me.gbScraperFieldsCredits.Enabled = True
        End If
    End Sub
    Private Sub chkCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCrew.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If Me.chkFullCrew.Checked AndAlso Me.chkCrew.Checked Then
            Me.chkFullCrew.Checked = False
        End If
    End Sub

    Private Sub chkDirector_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDirector.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkMusicBy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMusicBy.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If Me.chkFullCrew.Checked AndAlso Me.chkMusicBy.Checked Then
            Me.chkFullCrew.Checked = False
        End If
    End Sub
    Private Sub chkProducers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProducers.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
        If Me.chkFullCrew.Checked AndAlso Me.chkProducers.Checked Then
            Me.chkFullCrew.Checked = False
        End If
    End Sub

    Private Sub chkFullCrew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFullCrew.CheckedChanged
        RaiseEvent ModuleSettingsChanged()

        If Me.chkFullCrew.Checked Then
            Me.chkProducers.Checked = False
            Me.chkMusicBy.Checked = False
            Me.chkCrew.Checked = False
            Me.chkProducers.Enabled = False
            Me.chkMusicBy.Enabled = False
            Me.chkCrew.Enabled = False
        Else
            Me.chkProducers.Enabled = True
            Me.chkMusicBy.Enabled = True
            Me.chkCrew.Enabled = True
        End If
    End Sub

    Private Sub chkGenre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGenre.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOutline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOutline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPartialTitles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPartialTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPlot.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkPopularTitles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPopularTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Private Sub chkRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRelease_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRelease.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkRuntime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRuntime.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkStudio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStudio.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTagline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTagline.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkOriginalTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOriginalTitle.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTop250_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTop250.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTvTiles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTvTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCountry_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCountry.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkTrailer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrailer.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkVideoTitles_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVideoTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkShortTitles_CheckedChanged(sender As Object, e As EventArgs) Handles chkShortTitles.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkVotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVotes.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Private Sub chkYear_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkYear.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub chkFallBackworldwide_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallBackworldwide.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub cbForceTitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForceTitleLanguage.SelectedIndexChanged
        If cbForceTitleLanguage.SelectedIndex = -1 OrElse cbForceTitleLanguage.Text = "" Then
            Me.chkFallBackworldwide.Checked = False
            Me.chkFallBackworldwide.Enabled = False
        Else
            Me.chkFallBackworldwide.Enabled = True
        End If

        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkCountryAbbreviation_CheckedChanged(sender As Object, e As EventArgs) Handles chkCountryAbbreviation.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.SetUp()
    End Sub

    Sub orderChanged()
        Dim order As Integer = ModulesManager.Instance.externalScrapersModules_Data_Movie.FirstOrDefault(Function(p) p.AssemblyName = IMDB_Data._AssemblyName).ModuleOrder
        If ModulesManager.Instance.externalScrapersModules_Data_Movie.Count > 0 Then
            btnDown.Enabled = (order < ModulesManager.Instance.externalScrapersModules_Data_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        Me.chkCast.Text = Master.eLang.GetString(63, "Cast")
        Me.chkCertification.Text = Master.eLang.GetString(722, "Certification")
        Me.chkCountry.Text = Master.eLang.GetString(301, "Country")
        Me.chkCountryAbbreviation.Text = Master.eLang.GetString(1257, "Country-Tag: Save country abbreviation(s) instead of full name(s)")
        Me.chkCrew.Text = Master.eLang.GetString(391, "Other Crew")
        Me.chkDirector.Text = Master.eLang.GetString(62, "Director")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.chkFallBackworldwide.Text = Master.eLang.GetString(984, "Worldwide title as fallback")
        Me.chkFullCrew.Text = Master.eLang.GetString(513, "Scrape Full Crew")
        Me.chkGenre.Text = Master.eLang.GetString(20, "Genre")
        Me.chkMPAA.Text = Master.eLang.GetString(401, "MPAA")
        Me.chkMusicBy.Text = Master.eLang.GetString(392, "Music By")
        Me.chkOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        Me.chkOutline.Text = Master.eLang.GetString(64, "Plot Outline")
        Me.chkPlot.Text = Master.eLang.GetString(65, "Plot")
        Me.chkPartialTitles.Text = Master.eLang.GetString(1183, "Partial Titles")
        Me.chkPopularTitles.Text = Master.eLang.GetString(1182, "Popular Titles")
        Me.chkProducers.Text = Master.eLang.GetString(393, "Producers")
        Me.chkRating.Text = Master.eLang.GetString(1239, "IMDB Rating")
        Me.chkRelease.Text = Master.eLang.GetString(57, "Release Date")
        Me.chkRuntime.Text = Master.eLang.GetString(396, "Runtime")
        Me.chkShortTitles.Text = Master.eLang.GetString(837, "Short Titles")
        Me.chkStudio.Text = Master.eLang.GetString(395, "Studio")
        Me.chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        Me.chkTitle.Text = Master.eLang.GetString(21, "Title")
        Me.chkTop250.Text = Master.eLang.GetString(591, "Top250")
        Me.chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        Me.chkTvTitles.Text = Master.eLang.GetString(1184, "TV Movie Titles")
        Me.chkVideoTitles.Text = Master.eLang.GetString(1185, "Video Titles")
        Me.chkVotes.Text = Master.eLang.GetString(1252, "IMDB Votes")
        Me.chkWriters.Text = Master.eLang.GetString(394, "Writers")
        Me.chkYear.Text = Master.eLang.GetString(278, "Year")
        Me.gbScraperFieldsCredits.Text = Master.eLang.GetString(729, "Credits")
        Me.gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        Me.gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        Me.lblForceTitleLanguage.Text = Master.eLang.GetString(710, "Force Title Language:")
        Me.lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        Me.lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Methods

End Class