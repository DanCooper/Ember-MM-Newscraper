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

Public Class frmSettingsPanel_TV_Data

#Region "Events"

    Public Event NeedsRestart()
    Public Event SettingsChanged()

#End Region 'Events  

#Region "Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Private Sub ForceTitleLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbForceTitleLanguage.SelectedIndexChanged
        If cbForceTitleLanguage.SelectedIndex = -1 OrElse cbForceTitleLanguage.Text = String.Empty Then
            chkForceTitleLanguageFallback.Checked = False
            chkForceTitleLanguageFallback.Enabled = False
        Else
            chkForceTitleLanguageFallback.Enabled = True
        End If

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ForceTitleLanguageFallback_CheckedChanged(sender As Object, e As EventArgs) Handles chkForceTitleLanguageFallback.CheckedChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkForceTitleLanguageFallback.Text = .GetString(984, "Worldwide title as fallback")
            gbScraper.Text = .GetString(1186, "Scraper specific settings")
            lblForceTitleLanguage.Text = .GetString(710, "Force Title Language:")
        End With
    End Sub

#End Region 'Methods

End Class