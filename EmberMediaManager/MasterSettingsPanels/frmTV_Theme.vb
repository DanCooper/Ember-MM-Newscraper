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
    Implements Interfaces.ISettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.ISettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.ISettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.ISettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.ISettingsPanel.NeedsReload_Movieset
    Public Event NeedsReload_TVEpisode() Implements Interfaces.ISettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.ISettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.ISettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.ISettingsPanel.SettingsChanged
    Public Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.ISettingsPanel.StateChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ChildType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ChildType

    Public Property Image As Image Implements Interfaces.ISettingsPanel.Image

    Public Property ImageIndex As Integer Implements Interfaces.ISettingsPanel.ImageIndex

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Master Panel Data
        ChildType = Enums.SettingsPanelType.TVTheme
        IsEnabled = True
        Image = Nothing
        ImageIndex = 11
        Order = 700
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(1285, "Themes")
        ParentType = Enums.SettingsPanelType.TV
        UniqueId = "TV_Theme"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkKeepExisting.Text = .GetString(971, "Keep existing")
            gbThemes.Text = .GetString(1285, "Themes")
            lblDefaultSearchParameter.Text = String.Concat(.GetString(1172, "Default Search Parameter"), ":")
        End With
    End Sub

#End Region 'Dialog Methods 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        With Master.eSettings
            .TVShowThemeDefaultSearch = txtDefaultSearchParameter.Text.Trim
            .TVShowThemeKeepExisting = chkKeepExisting.Checked
        End With
        'With Master.eSettings.TVShow.ThemeSettings
        '    .DefaultSearchParameter = txtDefaultSearchParameter.Text.Trim
        '    .KeepExisting = chkKeepExisting.Checked
        'End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub Handle_SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkKeepExisting.CheckedChanged,
        txtDefaultSearchParameter.TextChanged
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        With Master.eSettings
            chkKeepExisting.Checked = .TVShowThemeKeepExisting
            txtDefaultSearchParameter.Text = .TVShowThemeDefaultSearch
        End With
        'With Master.eSettings.TVShow.ThemeSettings
        '    chkKeepExisting.Checked = .KeepExisting
        '    txtDefaultSearchParameter.Text = .DefaultSearchParameter
        'End With
    End Sub

#End Region 'Methods

End Class