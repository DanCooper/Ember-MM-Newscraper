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
Imports NLog

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public HostList As New List(Of KodiInterface.Host)

#End Region 'Fields

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)
    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        SetUp()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Actions on module startup
    ''' </summary>
    ''' <param name="sender">startup of module</param>
    ''' <remarks>
    ''' - set labels/translation text
    ''' 2015/06/26 Cocotus - First implementation
    ''' </remarks>
    Sub SetUp()
        btnAddHost.Text = Master.eLang.GetString(28, "Add")
        btnEditHost.Text = Master.eLang.GetString(1440, "Edit")
        btnRemoveHost.Text = Master.eLang.GetString(30, "Remove")
        chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        chkGetWatchedState.Text = String.Concat(Master.eLang.GetString(1454, "Get Watched State from"), ":")
        chkGetWatchedStateBeforeEdit_Movie.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateBeforeEdit_TVEpisode.Text = Master.eLang.GetString(1055, "Before Edit")
        chkGetWatchedStateScraperMulti_Movie.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperMulti_TVEpisode.Text = Master.eLang.GetString(1056, "During Multi-Scraping")
        chkGetWatchedStateScraperSingle_Movie.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        chkGetWatchedStateScraperSingle_TVEpisode.Text = Master.eLang.GetString(1057, "During Single-Scraping")
        chkNotification.Text = Master.eLang.GetString(1441, "Send Notifications")
        gbGetWatchedState.Text = Master.eLang.GetString(1071, "Watched State")
        gbGetWatchedStateMovies.Text = Master.eLang.GetString(36, "Movies")
        gbGetWatchedStateTVEpisodes.Text = Master.eLang.GetString(682, "Episodes")
        gbHosts.Text = Master.eLang.GetString(420, "Settings")
    End Sub

    ''' <summary>
    ''' Enable/Disable module
    ''' </summary>
    ''' <param name="sender">"Enable"-checkbox in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkEnabled.CheckedChanged
        RaiseEvent ModuleEnabledChanged(chkEnabled.Checked)
    End Sub

    ''' <summary>
    '''  Refresh view of hosts in settings page
    ''' </summary>
    ''' <remarks>
    ''' Used to refresh Kodi host data in controls (i.e. listbox)
    ''' Used whenever a new host is added/removed (=refresh view for user)
    ''' Instead of reloading XML Kodi settings we will use "xmlHosts" as it's always up to date
    ''' </remarks>
    Private Sub ReloadKodiHosts()
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
    ''' <param name="sender">"Remove"-button in Form</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Remove selected host in listbox from global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub btnRemoveHost_Click(sender As Object, e As EventArgs) Handles btnRemoveHost.Click
        RemoveKodiHost()
    End Sub
    ''' <summary>
    '''  Remove selected host
    ''' </summary>
    ''' <param name="sender">DEL-key on keyboard</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Remove selected host in listbox from global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub lbHosts_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lbHosts.KeyDown
        If e.KeyCode = Keys.Delete Then RemoveKodiHost()
    End Sub
    ''' <summary>
    '''  Remove selected host
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Remove selected host in listbox from global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub RemoveKodiHost()
        If lbHosts.SelectedItems.Count > 0 Then
            Dim HostToRemove As KodiInterface.Host = HostList.FirstOrDefault(Function(f) f.Label = lbHosts.SelectedItem.ToString)
            If HostToRemove IsNot Nothing Then
                HostList.Remove(HostToRemove)
                ReloadKodiHosts()
                RaiseEvent ModuleSettingsChanged()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Open host dialog to enter a new host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddHost_Click(sender As Object, e As EventArgs) Handles btnAddHost.Click
        Dim newHost As New KodiInterface.Host
        Dim dlgNew As New dlgHost(newHost)
        If dlgNew.ShowDialog = Windows.Forms.DialogResult.OK Then
            HostList.Add(dlgNew.Result)
            ReloadKodiHosts()
        End If
    End Sub
    ''' <summary>
    ''' Open host dialog to modify a host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditHost_Click(sender As Object, e As EventArgs) Handles btnEditHost.Click, lbHosts.DoubleClick
        If lbHosts.SelectedItems.Count > 0 Then
            Dim oldHost As KodiInterface.Host = HostList.FirstOrDefault(Function(f) f.Label = lbHosts.SelectedItem.ToString)
            If oldHost IsNot Nothing Then
                Dim dlgNew As New dlgHost(oldHost)
                If dlgNew.ShowDialog = Windows.Forms.DialogResult.OK Then
                    oldHost = dlgNew.Result
                    ReloadKodiHosts()
                End If
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
    Private Sub lbHosts_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbHosts.SelectedIndexChanged
        If lbHosts.SelectedItems.Count > 0 Then
            btnEditHost.Enabled = True
            btnRemoveHost.Enabled = True
        Else
            btnEditHost.Enabled = False
            btnRemoveHost.Enabled = False
        End If
    End Sub

    ''' <summary>
    '''  Setting "Sync Playcount" enabled/disabled
    ''' </summary>
    ''' <param name="sender">"Sync Playcount"-checkbox in Form</param>
    ''' <remarks>
    ''' 2015/07/08 Cocotus - First implementation
    ''' </remarks>
    Private Sub chkGetWatchedState_CheckedChanged(sender As Object, e As EventArgs) Handles chkGetWatchedState.CheckedChanged
        cbGetWatchedStateHost.Enabled = chkGetWatchedState.Checked
        gbGetWatchedStateMovies.Enabled = chkGetWatchedState.Checked
        gbGetWatchedStateTVEpisodes.Enabled = chkGetWatchedState.Checked
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub EnableApplyButton() Handles cbGetWatchedStateHost.SelectedIndexChanged,
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

#Region "Nested Types"
#End Region 'Nested Types

End Class