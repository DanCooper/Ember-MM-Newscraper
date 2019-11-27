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
    Implements Interfaces.IMasterSettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events  

#Region "Handles"

    Private Sub Handle_NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_Movie()
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        RaiseEvent NeedsDBClean_TV()
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie()
        RaiseEvent NeedsDBUpdate_Movie()
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV()
        RaiseEvent NeedsDBUpdate_TV()
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        RaiseEvent NeedsReload_Movie()
    End Sub

    Private Sub Handle_NeedsReload_MovieSet()
        RaiseEvent NeedsReload_MovieSet()
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        RaiseEvent NeedsReload_TVEpisode()
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        RaiseEvent NeedsReload_TVShow()
    End Sub

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Handles

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
            .Contains = Enums.SettingsPanelType.OptionsConnection,
            .ImageIndex = 1,
            .Order = 300,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_Connection",
            .Title = Master.eLang.GetString(421, "Connection"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
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

    Private Sub Enable_ApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        txtCredentialsDomain.TextChanged,
        txtCredentialsPassword.TextChanged,
        txtCredentialsUsername.TextChanged,
        txtProxyURI.TextChanged

        Handle_SettingsChanged()
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
        Handle_SettingsChanged()
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
        Handle_SettingsChanged()
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtProxyPort.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class