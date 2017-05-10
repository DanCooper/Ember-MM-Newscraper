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

Public Class dlgHost

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    'backgroundworker used for JSON request(s) like Populate Sources/Check Connection in this form
    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker
    'current edited host
    Private _currentHost As KodiInterface.Host = Nothing
    'all sources of current host
    Private currentHostRemoteSources As New List(Of XBMCRPC.List.Items.SourcesItem)
    'JSONRPC version of host - may be retrieved manually if user hits "Check Connection" button
    Private JsonHostVersionInfo As Kodi.APIKodi.APIVersionInfo
    'List of all show and movie sources in Ember
    Private LocalSources As New Dictionary(Of String, Enums.ContentType)
    Private RemoteSources As New List(Of String)

#End Region 'Fields

#Region "Properties"

    Public Property Result As KodiInterface.Host
        Get
            Return _currentHost
        End Get
        Set(value As KodiInterface.Host)
            _currentHost = value
        End Set
    End Property

#End Region 'Properties

#Region "Constructors"

    Sub New(ByVal Host As KodiInterface.Host)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual

        _currentHost = Host
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Formload of host dialog
    ''' </summary>
    ''' <param name="sender">loading of host dialog</param>
    ''' <remarks>
    ''' - triggered whenever user hits Edit or Add buttons in frmSettingsHolder form
    ''' 2015/06/26 Cocotus - First implementation
    ''' </remarks>
    Private Sub dlgHost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Setup()

        If Not _currentHost Is Nothing Then
            txtLabel.Text = _currentHost.Label
            txtHostIP.Text = _currentHost.Address
            txtPort.Text = CStr(_currentHost.Port)
            txtUsername.Text = _currentHost.Username
            txtPassword.Text = _currentHost.Password
            chkHostRealTimeSync.Checked = _currentHost.RealTimeSync
            txtHostMoviesetPath.Text = _currentHost.MovieSetArtworksPath
        Else
            'new host entry (should not be possible)
            _currentHost = New KodiInterface.Host
        End If
        'load sources of selected host and display values in datagrid
        PopulateHostSources()
    End Sub
    ''' <summary>
    ''' Actions on module startup
    ''' </summary>
    ''' <param name="sender">startup of module</param>
    ''' <remarks>
    ''' - set labels/translation text
    ''' 2015/06/26 Cocotus - First implementation
    ''' </remarks>
    Private Sub Setup()
        lblLoading.Text = Master.eLang.GetString(326, "Loading. Please wait...")
        Text = Master.eLang.GetString(1422, "Kodi Interface")
        btnOK.Text = Master.eLang.GetString(179, "OK")
        btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        btnCustomRemotePath.Text = Master.eLang.GetString(28, "Add")
        btnHostConnectionCheck.Text = Master.eLang.GetString(1423, "Check Connection")
        btnHostPopulateSources.Text = Master.eLang.GetString(1424, "Populate Sources")

        gbHostDetails.Text = Master.eLang.GetString(1425, "Kodi Host")
        gbHostMoviesetPath.Text = "Kodi " & Master.eLang.GetString(986, "MovieSet Artwork Folder")

        chkHostRealTimeSync.Text = Master.eLang.GetString(1429, "Enable Real Time synchronization")
        lblCustomRemotePath.Text = String.Concat(Master.eLang.GetString(1074, "Add Custom Kodi Source"), ":")
        lblHostLabel.Text = String.Concat(Master.eLang.GetString(232, "Name"), ":")
        lblHostIP.Text = String.Concat(Master.eLang.GetString(1430, "Kodi IP"), ":")
        lblHostPassword.Text = Master.eLang.GetString(426, "Password:")
        lblHostUsername.Text = Master.eLang.GetString(425, "Username:")
        lblHostWebserverPort.Text = String.Concat(Master.eLang.GetString(1431, "Kodi Port"), ":")

        colHostEmberSource.HeaderText = Master.eLang.GetString(1432, "Ember Source")
        colHostRemoteSource.HeaderText = Master.eLang.GetString(1433, "Kodi Source")
        colHostContentType.HeaderText = Master.eLang.GetString(1288, "Type")

        Dim SourceType As New Dictionary(Of String, Enums.ContentType)
        SourceType.Add(Master.eLang.None, Enums.ContentType.None)
        SourceType.Add(Master.eLang.GetString(36, "Movies"), Enums.ContentType.Movie)
        SourceType.Add(Master.eLang.GetString(653, "TV Shows"), Enums.ContentType.TV)
        colHostContentType.DataSource = SourceType.ToList
        colHostContentType.DisplayMember = "Key"
        colHostContentType.ValueMember = "Value"
    End Sub

    ''' <summary>
    ''' Load sources (Ember + Kodi) of selected host and display values in datagrid
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' </remarks>
    Private Sub PopulateHostSources()
        dgvHostSources.SuspendLayout()
        dgvHostSources.Rows.Clear()
        Dim sPath As String

        'populate all library sources in Ember into embersources
        For Each moviesources As Database.DBSource In Master.MovieSources
            LocalSources.Add(moviesources.Path, Enums.ContentType.Movie)
        Next
        For Each showsources As Database.DBSource In Master.TVShowSources
            LocalSources.Add(showsources.Path, Enums.ContentType.TV)
        Next

        'add all remote sources from XML into list
        RemoteSources.Add(String.Empty)
        For Each rSource In _currentHost.Sources
            RemoteSources.Add(rSource.RemotePath)
        Next
        RemoteSources.Sort()
        colHostRemoteSource.DataSource = RemoteSources.ToArray

        For Each s As KeyValuePair(Of String, Enums.ContentType) In LocalSources
            sPath = s.Key
            Dim i As Integer = dgvHostSources.Rows.Add(sPath)
            Dim dcbRemotePaths As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(1), DataGridViewComboBoxCell)
            Dim dcbContentType As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(2), DataGridViewComboBoxCell)
            dcbContentType.Value = s.Value

            Dim l As New List(Of String)
            l.Add(String.Empty) 'Empty Entry for combobox
            If _currentHost.Sources.Count > 0 Then
                'don't add kodi music/picture paths to Ember (for now...)
                For Each kSource In _currentHost.Sources
                    If Not String.IsNullOrEmpty(kSource.RemotePath) AndAlso Not l.Contains(kSource.RemotePath) AndAlso (kSource.ContentType = Enums.ContentType.Movie OrElse kSource.ContentType = Enums.ContentType.TV) Then
                        l.Add(kSource.RemotePath)
                    End If
                Next
            End If
            'dcbRemotePaths.DataSource = l.ToArray

            'try to load corresponding remotepath and type for current Ember Source from host settings
            If _currentHost.Sources.Count > 0 Then
                'don't add kodi music/picture paths to Ember (for now...)
                For Each ksource In _currentHost.Sources
                    If Not String.IsNullOrEmpty(ksource.RemotePath) AndAlso s.Key = ksource.LocalPath AndAlso (ksource.ContentType = Enums.ContentType.Movie OrElse ksource.ContentType = Enums.ContentType.TV) Then
                        dcbRemotePaths.Value = ksource.RemotePath
                        dcbContentType.Value = ksource.ContentType
                        Exit For
                    End If
                Next
            End If
        Next
        dgvHostSources.ResumeLayout()
        dgvHostSources.Enabled = True
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub btnCustomRemotePath_Click(sender As Object, e As EventArgs) Handles btnCustomRemotePath.Click
        If Not String.IsNullOrEmpty(txtCustomRemotePath.Text) Then
            If Not RemoteSources.Contains(txtCustomRemotePath.Text) Then RemoteSources.Add(txtCustomRemotePath.Text)
            RemoteSources.Sort()
            colHostRemoteSource.DataSource = RemoteSources.ToArray
            txtCustomRemotePath.Text = String.Empty
            txtCustomRemotePath.Focus()
        End If
    End Sub
    ''' <summary>
    ''' API request: Check connection to current host
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Send JSON API request to Kodi to check if entered host data is correct
    ''' </remarks>
    Private Sub btnHostConnectionCheck_Click(sender As Object, e As EventArgs) Handles btnHostConnectionCheck.Click
        lblLoading.Text = String.Concat(Master.eLang.GetString(1423, "Check Connection"), " ...")
        SetControlsEnabled(False)
        SetInfo()

        JsonHostVersionInfo = New Kodi.APIKodi.APIVersionInfo
        'start backgroundworker: check for JSONversion
        bwLoadInfo.RunWorkerAsync(2)
        While bwLoadInfo.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        SetControlsEnabled(True)

        If JsonHostVersionInfo Is Nothing Then
            MessageBox.Show(Master.eLang.GetString(1434, "There was a problem communicating with host."), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            MessageBox.Show(Master.eLang.GetString(1435, "Connection to host successful!") & Environment.NewLine & "API-Version: " & JsonHostVersionInfo.ReadingFriendly, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    ''' <summary>
    ''' Get available sources of host
    ''' </summary>
    ''' <param name="sender">"Populate Sources" button</param>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Send JSON API request to Kodi to get all sources from host
    ''' request will be executed in backgroundworker
    ''' </remarks>
    Private Sub btnHostPopulateSources_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHostPopulateSources.Click
        lblLoading.Text = Master.eLang.GetString(326, "Loading. Please wait...")
        SetControlsEnabled(False)
        SetInfo()

        'start request in backgroundworker -> getSources
        bwLoadInfo.RunWorkerAsync(1)
        While bwLoadInfo.IsBusy
            Application.DoEvents()
        End While

        'add new sources from host
        If currentHostRemoteSources IsNot Nothing AndAlso currentHostRemoteSources.Count > 0 Then
            For Each source In currentHostRemoteSources
                If Not RemoteSources.Contains(source.file) Then RemoteSources.Add(source.file)
            Next
            RemoteSources.Sort()
            colHostRemoteSource.DataSource = RemoteSources.ToArray
        Else
            MessageBox.Show(Master.eLang.GetString(1022, "There was a problem communicating with host or there are no sources configured in Kodi"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        SetControlsEnabled(True)
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If String.IsNullOrEmpty(txtLabel.Text) Then
            MessageBox.Show(Master.eLang.GetString(1436, "Please enter a unique name for host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtHostIP.Text) Then
            MessageBox.Show(Master.eLang.GetString(1437, "You must enter an IP for this host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtPort.Text) Then
            MessageBox.Show(Master.eLang.GetString(1438, "You must enter a port for this host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        SetInfo()

        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        pnlLoading.Visible = Not isEnabled
        gbHostDetails.Enabled = isEnabled
        btnHostPopulateSources.Enabled = isEnabled
        btnOK.Enabled = isEnabled
        btnCancel.Enabled = isEnabled
        txtHostIP.Enabled = isEnabled
        txtLabel.Enabled = isEnabled
        txtPassword.Enabled = isEnabled
        txtPort.Enabled = isEnabled
        txtUsername.Enabled = isEnabled
        dgvHostSources.Enabled = isEnabled
    End Sub
    ''' <summary>
    ''' Handle the error if a RemotePath is no longer listed (removed in Kodi) after "Populate Source"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvHostSources_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvHostSources.DataError
        dgvHostSources.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = String.Empty
    End Sub

    ''' <summary>
    ''' Backgroundworker job(s): GetHostSources, check for JSONversion
    ''' </summary>
    ''' <param name="sender">backgroundworker</param>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Request will be executed in backgroundworker
    ''' </remarks>
    Private Sub bwLoadInfo_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        Select Case CInt(e.Argument)
            Case 1
                'API request: Get all sources of current host
                currentHostRemoteSources = Kodi.APIKodi.GetSources(_currentHost)
            Case 2
                'API request: Get JSONRPC version of host
                JsonHostVersionInfo = Kodi.APIKodi.GetHostJSONVersion(_currentHost)
        End Select
    End Sub

    Private Sub SetInfo()
        _currentHost.Address = txtHostIP.Text
        _currentHost.Label = txtLabel.Text
        _currentHost.MovieSetArtworksPath = txtHostMoviesetPath.Text
        _currentHost.Password = txtPassword.Text
        _currentHost.Port = CInt(txtPort.Text)
        _currentHost.RealTimeSync = chkHostRealTimeSync.Checked
        _currentHost.Username = txtUsername.Text
        _currentHost.Sources.Clear()
        If dgvHostSources.Rows.Count > 0 Then
            For i = 0 To dgvHostSources.Rows.Count - 1
                If Not String.IsNullOrEmpty(CStr(dgvHostSources.Rows(i).Cells(0).Value)) AndAlso Not String.IsNullOrEmpty(CStr(dgvHostSources.Rows(i).Cells(1).Value)) Then
                    Dim nSource As New KodiInterface.Source
                    nSource.ContentType = CType(dgvHostSources.Rows(i).Cells(2).Value, Enums.ContentType)
                    nSource.LocalPath = CStr(dgvHostSources.Rows(i).Cells(0).Value)
                    nSource.RemotePath = CStr(dgvHostSources.Rows(i).Cells(1).Value)
                    _currentHost.Sources.Add(nSource)
                End If
            Next
        End If
    End Sub

    Private Sub txtCustomRemotePath_TextChanged(sender As Object, e As EventArgs) Handles txtCustomRemotePath.TextChanged
        btnCustomRemotePath.Enabled = Not String.IsNullOrEmpty(txtCustomRemotePath.Text) AndAlso Not RemoteSources.Contains(txtCustomRemotePath.Text)
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class