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

    Public Sub SaveSetup() Implements Interfaces.IMasterSettingsPanel.SaveSetup
        With Master.eSettings
            If Not String.IsNullOrEmpty(txtProxyURI.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPort.Text) Then
                .ProxyURI = txtProxyURI.Text
                .ProxyPort = Convert.ToInt32(txtProxyPort.Text)

                If Not String.IsNullOrEmpty(txtProxyUsername.Text) AndAlso Not String.IsNullOrEmpty(txtProxyPassword.Text) Then
                    .ProxyCredentials.UserName = txtProxyUsername.Text
                    .ProxyCredentials.Password = txtProxyPassword.Text
                    .ProxyCredentials.Domain = txtProxyDomain.Text
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
                chkProxyEnable.Checked = True
                txtProxyURI.Text = .ProxyURI
                txtProxyPort.Text = .ProxyPort.ToString

                If Not String.IsNullOrEmpty(.ProxyCredentials.UserName) Then
                    chkProxyCredsEnable.Checked = True
                    txtProxyUsername.Text = .ProxyCredentials.UserName
                    txtProxyPassword.Text = .ProxyCredentials.Password
                    txtProxyDomain.Text = .ProxyCredentials.Domain
                End If
            End If
        End With
    End Sub

    Private Sub Setup()
        chkProxyCredsEnable.Text = Master.eLang.GetString(677, "Enable Credentials")
        chkProxyEnable.Text = Master.eLang.GetString(673, "Enable Proxy")
        gbProxyCredsOpts.Text = Master.eLang.GetString(676, "Credentials")
        gbProxyOpts.Text = Master.eLang.GetString(672, "Proxy")
        lblProxyDomain.Text = Master.eLang.GetString(678, "Domain:")
        lblProxyPassword.Text = Master.eLang.GetString(426, "Password:")
        lblProxyPort.Text = Master.eLang.GetString(675, "Proxy Port:")
        lblProxyURI.Text = Master.eLang.GetString(674, "Proxy URL:")
        lblProxyUsername.Text = Master.eLang.GetString(425, "Username:")
    End Sub

    Private Sub chkProxyCredsEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        txtProxyUsername.Enabled = chkProxyCredsEnable.Checked
        txtProxyPassword.Enabled = chkProxyCredsEnable.Checked
        txtProxyDomain.Enabled = chkProxyCredsEnable.Checked

        If Not chkProxyCredsEnable.Checked Then
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

    Private Sub chkProxyEnable_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        txtProxyURI.Enabled = chkProxyEnable.Checked
        txtProxyPort.Enabled = chkProxyEnable.Checked
        gbProxyCredsOpts.Enabled = chkProxyEnable.Checked

        If Not chkProxyEnable.Checked Then
            txtProxyURI.Text = String.Empty
            txtProxyPort.Text = String.Empty
            chkProxyCredsEnable.Checked = False
            txtProxyUsername.Text = String.Empty
            txtProxyPassword.Text = String.Empty
            txtProxyDomain.Text = String.Empty
        End If
    End Sub

#End Region 'Methods

End Class