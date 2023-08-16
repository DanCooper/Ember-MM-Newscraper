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

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

    Public Event SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)

    Public Event SetupNeedsRestart()
#End Region 'Events

#Region "Methods"
    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
        Dim order As Integer = Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order < Addons.Instance.Trailer_Scrapers_Movie.Count - 1 Then
            Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order + 1).Order = order
            Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order + 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, 1)
            orderChanged()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
        Dim order As Integer = Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If order > 0 Then
            Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.Order = order - 1).Order = order
            Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order = order - 1
            RaiseEvent SetupScraperChanged(chkEnabled.Checked, -1)
            orderChanged()
        End If
    End Sub

    Private Sub cbEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent SetupScraperChanged(chkEnabled.Checked, 0)
    End Sub

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Sub orderChanged()
        Dim order As Integer = Addons.Instance.Trailer_Scrapers_Movie.FirstOrDefault(Function(p) p.AssemblyFileName = Addon._AssemblyName).Order
        If Addons.Instance.Trailer_Scrapers_Movie.Count > 1 Then
            btnDown.Enabled = (order < Addons.Instance.Trailer_Scrapers_Movie.Count - 1)
            btnUp.Enabled = (order > 0)
        Else
            btnDown.Enabled = False
            btnUp.Enabled = False
        End If
    End Sub

    Private Sub pbApiKeyInfo_Click(sender As Object, e As EventArgs) Handles pbApiKeyInfo.Click
        Functions.Launch(My.Resources.urlAPIKey)
    End Sub

    Sub Setup()
        lblApiKey.Text = Master.eLang.GetString(870, "TMDB API Key")
        btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkFallBackEng.Text = Master.eLang.GetString(922, "Fallback to english")
        gbScraperTrailerOpts.Text = Master.eLang.GetString(283, "Trailers - Scraper specific")
        lblEMMAPI.Text = Master.eLang.GetString(1189, "Ember Media Manager Embedded API Key")
        lblInfoBottom.Text = String.Format(Master.eLang.GetString(790, "These settings are specific to this module.{0}Please refer to the global settings for more options."), Environment.NewLine)
        lblScraperOrder.Text = Master.eLang.GetString(168, "Scrape Order")
    End Sub

    Private Sub txtApiKey_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtApiKey.TextChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkFallBackEng_CheckedChanged(sender As Object, e As EventArgs) Handles chkFallBackEng.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnUnlockAPI_Click(sender As Object, e As EventArgs) Handles btnUnlockAPI.Click
        If btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key") Then
            btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            lblEMMAPI.Visible = False
            txtApiKey.Enabled = True
        Else
            btnUnlockAPI.Text = Master.eLang.GetString(1188, "Use my own API key")
            lblEMMAPI.Visible = True
            txtApiKey.Enabled = False
            txtApiKey.Text = String.Empty
        End If
    End Sub

#End Region 'Methods

End Class