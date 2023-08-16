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
    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal diffOrder As Integer)
    Public Event SetupNeedsRestart()

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
        chkFallBackEng.Text = Master.eLang.GetString(922, "Fallback to english")
        chkGenres.Text = Master.eLang.GetString(725, "Genres")
        chkOriginalTitle.Text = Master.eLang.GetString(302, "Original Title")
        chkPlot.Text = Master.eLang.GetString(65, "Plot")
        chkPremiered.Text = Master.eLang.GetString(724, "Premiered")
        chkRuntime.Text = Master.eLang.GetString(238, "Runtime")
        chkStudios.Text = Master.eLang.GetString(226, "Studios")
        chkTagline.Text = Master.eLang.GetString(397, "Tagline")
        chkTitle.Text = Master.eLang.GetString(21, "Title")
        chkTrailer.Text = Master.eLang.GetString(151, "Trailer")
        chkWriters.Text = Master.eLang.GetString(394, "Writers")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        gbScraperOpts.Text = Master.eLang.GetString(1186, "Scraper Options")
        lblApiPin.Text = String.Concat(Addon.Localisation.GetString(1, "TVDb Api Pin"), ":")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub ApiPinInfo_Click(sender As Object, e As EventArgs) Handles pbApiPinInfo.Click
        Functions.Launch(My.Resources.urlApiPin)
    End Sub

    Private Sub Enabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Sub OrderChanged()
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If Addons.Instance.Data_Scrapers_Movie.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Data_Scrapers_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub Order_Down_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order < Addons.Instance.Data_Scrapers_Movie.Count - 1 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            OrderChanged()
        End If
    End Sub

    Private Sub Order_Up_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            OrderChanged()
        End If
    End Sub

    Private Sub SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkActors.CheckedChanged,
        chkCertifications.CheckedChanged,
        chkCountries.CheckedChanged,
        chkDirectors.CheckedChanged,
        chkFallBackEng.CheckedChanged,
        chkGenres.CheckedChanged,
        chkOriginalTitle.CheckedChanged,
        chkPlot.CheckedChanged,
        chkPremiered.CheckedChanged,
        chkRuntime.CheckedChanged,
        chkStudios.CheckedChanged,
        chkTagline.CheckedChanged,
        chkTitle.CheckedChanged,
        chkTrailer.CheckedChanged,
        chkWriters.CheckedChanged,
        txtApiPin.TextAlignChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class