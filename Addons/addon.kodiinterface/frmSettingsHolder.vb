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

Imports NLog

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public HostList As New List(Of Addon.Host)

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()
    Public Event ModuleSetupChanged(ByVal state As Boolean)

#End Region 'Events

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

    Sub Setup()
        With Addon.Localisation
            btnAddHost.Text = .CommonWordsList.Add
            btnEditHost.Text = .CommonWordsList.Edit
            btnRemoveHost.Text = .CommonWordsList.Remove
            chkEnabled.Text = .CommonWordsList.Enabled
            chkGetWatchedState.Text = String.Concat(.GetString(35, "Get Watched-State from"), ":")
            chkGetWatchedStateBeforeEdit_Movie.Text = .CommonWordsList.Before_Edit
            chkGetWatchedStateBeforeEdit_TVEpisode.Text = .CommonWordsList.Before_Edit
            chkGetWatchedStateScraperMulti_Movie.Text = .CommonWordsList.During_Multi_Scraping
            chkGetWatchedStateScraperMulti_TVEpisode.Text = .CommonWordsList.During_Multi_Scraping
            chkGetWatchedStateScraperSingle_Movie.Text = .CommonWordsList.During_Single_Scraping
            chkGetWatchedStateScraperSingle_TVEpisode.Text = .CommonWordsList.During_Single_Scraping
            chkNotification.Text = .GetString(36, "Send Notifications")
            gbGetWatchedState.Text = .GetString(37, "Watched-State")
            gbGetWatchedStateMovies.Text = .CommonWordsList.Movies
            gbGetWatchedStateTVEpisodes.Text = .CommonWordsList.Episodes
            gbHosts.Text = .CommonWordsList.Settings
        End With
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    ''' <summary>
    ''' Enable/Disable module
    ''' </summary>
    ''' <param name="sender">"Enable"-checkbox in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub Enabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleSetupChanged(chkEnabled.Checked)
    End Sub
    ''' <summary>
    '''  Setting "Sync Playcount" enabled/disabled
    ''' </summary>
    ''' <param name="sender">"Sync Playcount"-checkbox in Form</param>
    ''' <remarks>
    ''' 2015/07/08 Cocotus - First implementation
    ''' </remarks>
    Private Sub GetWatchedState_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetWatchedState.CheckedChanged
        cbGetWatchedStateHost.Enabled = chkGetWatchedState.Checked
        gbGetWatchedStateMovies.Enabled = chkGetWatchedState.Checked
        gbGetWatchedStateTVEpisodes.Enabled = chkGetWatchedState.Checked
        RaiseEvent ModuleSettingsChanged()
    End Sub
    ''' <summary>
    ''' Open host dialog to enter a new host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Hosts_Add(sender As Object, e As EventArgs) Handles btnAddHost.Click
        Dim newHost As New Addon.Host
        Dim dlgNew As New dlgHost(newHost)
        If dlgNew.ShowDialog = Windows.Forms.DialogResult.OK Then
            HostList.Add(dlgNew.Result)
            Hosts_Reload_HostList()
        End If
    End Sub
    ''' <summary>
    ''' Open host dialog to modify a host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Hosts_Edit(sender As Object, e As EventArgs) Handles btnEditHost.Click, lbHosts.DoubleClick
        If lbHosts.SelectedItems.Count > 0 Then
            Dim oldHost As Addon.Host = HostList.FirstOrDefault(Function(f) f.Label = lbHosts.SelectedItem.ToString)
            If oldHost IsNot Nothing Then
                Dim dlgNew As New dlgHost(oldHost)
                If dlgNew.ShowDialog = DialogResult.OK Then
                    oldHost = dlgNew.Result
                    Hosts_Reload_HostList()
                End If
            End If
        End If
    End Sub

    Private Sub Hosts_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lbHosts.KeyDown
        If e.KeyCode = Keys.Delete Then Hosts_Remove()
    End Sub
    ''' <summary>
    '''  Refresh view of hosts in settings page
    ''' </summary>
    ''' <remarks>
    ''' Used to refresh Kodi host data in controls (i.e. listbox)
    ''' Used whenever a new host is added/removed (=refresh view for user)
    ''' Instead of reloading XML Kodi settings we will use "xmlHosts" as it's always up to date
    ''' </remarks>
    Private Sub Hosts_Reload_HostList()
        Dim oldPlayCountHost As String = String.Empty
        If cbGetWatchedStateHost.Items.Count > 0 AndAlso cbGetWatchedStateHost.SelectedItem IsNot Nothing Then
            oldPlayCountHost = cbGetWatchedStateHost.SelectedItem.ToString
        End If
        btnEditHost.Enabled = False
        btnRemoveHost.Enabled = False
        cbGetWatchedStateHost.Items.Clear()
        lbHosts.Items.Clear()
        For Each host In HostList
            lbHosts.Items.Add(host.Label)
            cbGetWatchedStateHost.Items.Add(host.Label)
        Next
        If cbGetWatchedStateHost.Items.Contains(oldPlayCountHost) Then
            cbGetWatchedStateHost.SelectedItem = oldPlayCountHost
        End If
    End Sub
    ''' <summary>
    '''  Remove selected host
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Remove selected host in listbox from global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub Hosts_Remove() Handles btnRemoveHost.Click
        If lbHosts.SelectedItems.Count > 0 Then
            Dim HostToRemove As Addon.Host = HostList.FirstOrDefault(Function(f) f.Label = lbHosts.SelectedItem.ToString)
            If HostToRemove IsNot Nothing Then
                HostList.Remove(HostToRemove)
                Hosts_Reload_HostList()
                RaiseEvent ModuleSettingsChanged()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Enable/Disable Edit Button logic
    ''' </summary>
    ''' <param name="sender">listbox selection changed</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Only enable edit button if host is selected
    ''' </remarks>
    Private Sub Hosts_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbHosts.SelectedIndexChanged
        If lbHosts.SelectedItems.Count > 0 Then
            btnEditHost.Enabled = True
            btnRemoveHost.Enabled = True
        Else
            btnEditHost.Enabled = False
            btnRemoveHost.Enabled = False
        End If
    End Sub

    Private Sub SettingsChanged() Handles _
        cbGetWatchedStateHost.SelectedIndexChanged,
        chkGetWatchedStateBeforeEdit_Movie.CheckedChanged,
        chkGetWatchedStateBeforeEdit_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperMulti_Movie.CheckedChanged,
        chkGetWatchedStateScraperMulti_TVEpisode.CheckedChanged,
        chkGetWatchedStateScraperSingle_Movie.CheckedChanged,
        chkGetWatchedStateScraperSingle_TVEpisode.CheckedChanged,
        chkNotification.CheckedChanged

        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

End Class