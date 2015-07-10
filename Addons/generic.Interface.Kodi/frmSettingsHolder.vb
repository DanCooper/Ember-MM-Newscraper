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
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    'reflects the current host(s) settings/setup configured in settings, will be filled at module startup from XML settings (and is used to write changes of settings back into XML)
    Public xmlHosts As New EmberAPI.clsXMLHosts
#End Region 'Fields

#Region "Events"

    Public Event ModuleEnabledChanged(ByVal State As Boolean)

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Constructors"
    Public Sub New()
        InitializeComponent()
        Me.SetUp()
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
        Me.gbSettingsGeneral.Text = Master.eLang.GetString(420, "Settings")
        Me.btnAddHost.Text = Master.eLang.GetString(28, "Add")
        Me.btnRemoveHost.Text = Master.eLang.GetString(30, "Remove")
        Me.chkEnabled.Text = Master.eLang.GetString(774, "Enabled")
        Me.btnEditHost.Text = Master.eLang.GetString(1440, "Edit")
        Me.chkNotification.Text = Master.eLang.GetString(1441, "Send Notifications")
    End Sub

    ''' <summary>
    ''' Enable/Disable module
    ''' </summary>
    ''' <param name="sender">"Enable"-checkbox in Form</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
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
        Me.cbPlayCountHost.Items.Clear()
        Me.lbHosts.Items.Clear()
        For Each host In Me.xmlHosts.host
            Me.lbHosts.Items.Add(host.name)
            Me.cbPlayCountHost.Items.Add(host.name)
        Next
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
        If Me.lbHosts.SelectedItems.Count > 0 Then
            Dim tmparray = xmlHosts.host.Where(Function(y) y.name <> Me.lbHosts.SelectedItem.ToString).ToArray()
            'xmlHosts.host.Where(Function(y) y.name <> Me.lbHosts.SelectedItem.ToString).ToArray()
            ReDim xmlHosts.host(tmparray.Length)
            xmlHosts.host = tmparray
            ReloadKodiHosts()
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    ''' <summary>
    ''' Open host dialog to enter a new host
    ''' </summary>
    ''' <param name="sender">"Add"-button in Form</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Open Host Edit window for user to enter new host data
    ''' Result (if hit "OK"): Add new host entry to global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub btnAddHost_Click(sender As Object, e As EventArgs) Handles btnAddHost.Click
        Using dlg As New dlgHost
            dlg.currentHost = Nothing
            dlg.lstAllHosts.Clear()
            dlg.lstAllHosts = xmlHosts.host.ToList
            If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim tmphost As EmberAPI.Host
                tmphost = New EmberAPI.Host With {.name = dlg.txtLabel.Text, .address = dlg.txtHostIP.Text, .port = CInt(dlg.txtWebPort.Text), .username = dlg.txtUsername.Text, .password = dlg.txtPassword.Text, .realtimesync = dlg.chkHostRealTimeSync.Checked}
                If dlg.rbHostWindows.Checked Then
                    tmphost.remotepathseparator = "\"
                Else
                    tmphost.remotepathseparator = "/"
                End If
                For i = 0 To dlg.dgvHostSources.Rows.Count - 1
                    If Not String.IsNullOrEmpty(CStr(dlg.dgvHostSources.Rows(i).Cells(0).Value)) AndAlso Not String.IsNullOrEmpty(CStr(dlg.dgvHostSources.Rows(i).Cells(1).Value)) Then
                        Dim tmpsource As New EmberAPI.HostSource
                        tmpsource.type = CStr(dlg.dgvHostSources.Rows(i).Cells(2).FormattedValue)
                        tmpsource.applicationpath = CStr(dlg.dgvHostSources.Rows(i).Cells(0).Value)
                        tmpsource.remotepath = CStr(dlg.dgvHostSources.Rows(i).Cells(1).Value)
                        ReDim Preserve tmphost.source(i)
                        tmphost.source(i) = tmpsource
                    End If
                Next
                ReDim Preserve xmlHosts.host(xmlHosts.host.Count)
                xmlHosts.host(xmlHosts.host.Count - 1) = tmphost
                RaiseEvent ModuleSettingsChanged()
                Me.ReloadKodiHosts()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Open host dialog to modify existing host data
    ''' </summary>
    ''' <param name="sender">"Edit"-button in Form</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Open Host Edit window for user to edit existing host data of selected host in list
    ''' Result (if hit "OK"): Submit changes of host entry to global xmlHosts and refresh view afterwards
    ''' </remarks>
    Private Sub btnEditHost_Click(sender As Object, e As EventArgs) Handles btnEditHost.Click
        Dim iSel As Integer = Me.lbHosts.SelectedIndex
        If Me.lbHosts.SelectedItems.Count > 0 Then
            Using dlg As New dlgHost
                dlg.lstAllHosts.Clear()
                dlg.currentHost = Nothing
                dlg.lstAllHosts = xmlHosts.host.Where(Function(y) y.name <> Me.lbHosts.SelectedItem.ToString).ToList
                dlg.currentHost = xmlHosts.host.FirstOrDefault(Function(y) y.name = Me.lbHosts.SelectedItem.ToString)
                If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim tmphost As EmberAPI.Host
                    tmphost = xmlHosts.host.FirstOrDefault(Function(y) y.name = Me.lbHosts.SelectedItem.ToString)
                    tmphost.name = dlg.txtLabel.Text
                    tmphost.address = dlg.txtHostIP.Text
                    tmphost.port = CInt(dlg.txtWebPort.Text)
                    tmphost.username = dlg.txtUsername.Text
                    tmphost.password = dlg.txtPassword.Text
                    tmphost.realtimesync = dlg.chkHostRealTimeSync.Checked
                    If dlg.rbHostWindows.Checked Then
                        tmphost.remotepathseparator = "\"
                    Else
                        tmphost.remotepathseparator = "/"
                    End If
                    'clear  all sources, we will add them again...
                    '' Array.Clear(tmphost.source, 0, tmphost.source.Length)
                    ReDim tmphost.source(0)
                    For i = 0 To dlg.dgvHostSources.Rows.Count - 1
                        If Not String.IsNullOrEmpty(CStr(dlg.dgvHostSources.Rows(i).Cells(0).Value)) AndAlso Not String.IsNullOrEmpty(CStr(dlg.dgvHostSources.Rows(i).Cells(1).Value)) Then
                            Dim tmpsource As New EmberAPI.HostSource
                            tmpsource.type = CStr(dlg.dgvHostSources.Rows(i).Cells(2).FormattedValue)
                            tmpsource.applicationpath = CStr(dlg.dgvHostSources.Rows(i).Cells(0).Value)
                            tmpsource.remotepath = CStr(dlg.dgvHostSources.Rows(i).Cells(1).Value)
                            ReDim Preserve tmphost.source(i)
                            tmphost.source(i) = tmpsource
                        End If
                    Next
                    RaiseEvent ModuleSettingsChanged()
                    Me.ReloadKodiHosts()
                End If
                btnEditHost.Enabled = False
            End Using
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
    Private Sub lbHosts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbHosts.SelectedIndexChanged
        If Me.lbHosts.SelectedItems.Count > 0 Then
            btnEditHost.Enabled = True
        Else
            btnEditHost.Enabled = False
        End If
    End Sub

    ''' <summary>
    '''  Setting "SendNotification" enabled/disabled
    ''' </summary>
    ''' <param name="sender">"Notification"-checkbox in Form</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' </remarks>
    Private Sub chkNotification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotification.CheckedChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    ''' <summary>
    '''  Setting "Sync Playcount" enabled/disabled
    ''' </summary>
    ''' <param name="sender">"Sync Playcount"-checkbox in Form</param>
    ''' <remarks>
    ''' 2015/07/08 Cocotus - First implementation
    ''' </remarks>
    Private Sub chkPlayCount_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlayCount.CheckedChanged
        cbPlayCountHost.Enabled = chkPlayCount.Checked
        RaiseEvent ModuleSettingsChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"
#End Region 'Nested Types


End Class