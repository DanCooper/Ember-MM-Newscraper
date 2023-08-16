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
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkRating.Text = Master.eLang.GetString(245, "Rating")
        chkUserRating.Text = Master.eLang.GetString(1467, "User Rating")
        gbScraperFieldsOpts.Text = Master.eLang.GetString(791, "Scraper Fields - Scraper specific")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order < Addons.Instance.Data_Scrapers_Movie.Count - 1 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub chkEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Private Sub SettingsChanged(sender As Object, e As EventArgs) Handles chkRating.CheckedChanged, chkUserRating.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Data_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
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