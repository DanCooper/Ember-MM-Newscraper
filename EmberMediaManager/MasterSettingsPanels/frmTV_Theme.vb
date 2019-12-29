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

Public Class frmTV_Theme
    Implements Interfaces.IMasterSettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

#End Region 'Constructors 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.IMasterSettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IMasterSettingsPanel.InjectSettingsPanel
        Settings_Load()

        Return New Containers.SettingsPanel With {
            .Contains = Enums.SettingsPanelType.TVTheme,
            .ImageIndex = 11,
            .Order = 700,
            .Panel = pnlSettings,
            .SettingsPanelID = "TV_Theme",
            .Title = Master.eLang.GetString(1285, "Themes"),
            .Type = Enums.SettingsPanelType.TV
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.TVShow.ThemeSettings
            .DefaultSearchParameter = txtDefaultSearchParameter.Text.Trim
            .KeepExisting = chkKeepExisting.Checked
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.TVShow.ThemeSettings
            chkKeepExisting.Checked = .KeepExisting
            txtDefaultSearchParameter.Text = .DefaultSearchParameter
        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkKeepExisting.Text = .GetString(971, "Keep existing")
            gbThemes.Text = .GetString(1285, "Themes")
            lblDefaultSearchParameter.Text = String.Concat(.GetString(1172, "Default Search Parameter"), ":")
        End With
    End Sub

    Private Sub EnableApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkKeepExisting.CheckedChanged,
        txtDefaultSearchParameter.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Methods

End Class