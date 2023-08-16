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
Imports System.Net

Public Class frmOption_Connection
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
        ChildType = Enums.SettingsPanelType.OptionsConnection
        IsEnabled = True
        Image = Nothing
        ImageIndex = 1
        Order = 400
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(421, "Connection")
        ParentType = Enums.SettingsPanelType.Options
        UniqueId = "Option_Connection"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        chkCredentialsEnabled.Text = Master.eLang.GetString(677, "Enable Credentials")
        chkProxyEnabled.Text = Master.eLang.GetString(673, "Enable Proxy")
        gbCredentials.Text = Master.eLang.GetString(676, "Credentials")
        gbProxy.Text = Master.eLang.GetString(672, "Proxy")
        lblCredentialsDomain.Text = Master.eLang.GetString(678, "Domain:")
        lblCredentialsPassword.Text = Master.eLang.GetString(426, "Password:")
        lblCredentialsUsername.Text = Master.eLang.GetString(425, "Username:")
        lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
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
            Dim iPort As Integer
            If Not String.IsNullOrEmpty(txtProxyURI.Text) AndAlso Integer.TryParse(txtProxyPort.Text.Trim, iPort) Then
                .ProxyPort = iPort
                .ProxyURI = txtProxyURI.Text

                If Not String.IsNullOrEmpty(txtCredentialsUsername.Text) AndAlso Not String.IsNullOrEmpty(txtCredentialsPassword.Text) Then
                    .ProxyCredentials.Domain = txtCredentialsDomain.Text
                    .ProxyCredentials.Password = txtCredentialsPassword.Text
                    .ProxyCredentials.UserName = txtCredentialsUsername.Text
                Else
                    .ProxyCredentials = New NetworkCredential
                End If
            Else
                .ProxyURI = String.Empty
                .ProxyPort = -1
            End If
        End With
        'With Master.eSettings.Options.Connection
        '    Dim iPort As Integer
        '    If Not String.IsNullOrEmpty(txtProxyURI.Text) AndAlso Integer.TryParse(txtProxyPort.Text.Trim, iPort) Then
        '        .ProxyPort = iPort
        '        .ProxyURI = txtProxyURI.Text

        '        If Not String.IsNullOrEmpty(txtCredentialsUsername.Text) AndAlso Not String.IsNullOrEmpty(txtCredentialsPassword.Text) Then
        '            .ProxyCredentials.Domain = txtCredentialsDomain.Text
        '            .ProxyCredentials.Password = txtCredentialsPassword.Text
        '            .ProxyCredentials.UserName = txtCredentialsUsername.Text
        '        Else
        '            .ProxyCredentials = New NetworkCredential
        '        End If
        '    Else
        '        .ProxyURI = String.Empty
        '        .ProxyPort = -1
        '    End If
        'End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
                chkProxyEnabled.Checked = True
                txtProxyURI.Text = .ProxyURI
                txtProxyPort.Text = .ProxyPort.ToString

                If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
                    chkCredentialsEnabled.Checked = True
                    txtCredentialsUsername.Text = .ProxyCredentials.UserName
                    txtCredentialsPassword.Text = .ProxyCredentials.Password
                    txtCredentialsDomain.Text = .ProxyCredentials.Domain
                End If
            End If
        End With
        'With Master.eSettings.Options.Connection
        '    If Not String.IsNullOrEmpty(.ProxyURI) AndAlso .ProxyPort >= 0 Then
        '        chkProxyEnabled.Checked = True
        '        txtProxyURI.Text = .ProxyURI
        '        txtProxyPort.Text = .ProxyPort.ToString

        '        If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
        '            chkCredentialsEnabled.Checked = True
        '            txtCredentialsUsername.Text = .ProxyCredentials.UserName
        '            txtCredentialsPassword.Text = .ProxyCredentials.Password
        '            txtCredentialsDomain.Text = .ProxyCredentials.Domain
        '        End If
        '    End If
        'End With
    End Sub

    Private Sub Handle_SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        txtCredentialsDomain.TextChanged,
        txtCredentialsPassword.TextChanged,
        txtCredentialsUsername.TextChanged,
        txtProxyURI.TextChanged
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CredentialsEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCredentialsEnabled.CheckedChanged
        txtCredentialsUsername.Enabled = chkCredentialsEnabled.Checked
        txtCredentialsPassword.Enabled = chkCredentialsEnabled.Checked
        txtCredentialsDomain.Enabled = chkCredentialsEnabled.Checked

        If Not chkCredentialsEnabled.Checked Then
            txtCredentialsUsername.Text = String.Empty
            txtCredentialsPassword.Text = String.Empty
            txtCredentialsDomain.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ProxyEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkProxyEnabled.CheckedChanged
        txtProxyURI.Enabled = chkProxyEnabled.Checked
        txtProxyPort.Enabled = chkProxyEnabled.Checked
        gbCredentials.Enabled = chkProxyEnabled.Checked

        If Not chkProxyEnabled.Checked Then
            txtProxyURI.Text = String.Empty
            txtProxyPort.Text = String.Empty
            chkCredentialsEnabled.Checked = False
            txtCredentialsUsername.Text = String.Empty
            txtCredentialsPassword.Text = String.Empty
            txtCredentialsDomain.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtProxyPort.KeyPress
        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class