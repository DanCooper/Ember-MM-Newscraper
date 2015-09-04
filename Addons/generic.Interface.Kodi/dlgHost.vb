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
Imports System.IO
Imports NLog

Public Class dlgHost

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    'backgroundworker used for JSON request(s) like Populate Sources/Check Connection in this form
    Friend WithEvents bwLoadInfo As New System.ComponentModel.BackgroundWorker
    'current edited host
    Public currentHost As New KodiInterface.Host
    'list of all configured KodiHosts (beside currentHost)
    Public lstAllHosts As New List(Of KodiInterface.Host)
    'all sources of current host
    Private currentHostSources As New List(Of XBMCRPC.List.Items.SourcesItem)
    'JSONRPC version of host - may be retrieved manually if user hits "Check Connection" button
    Private JsonHostversion As String = ""
    'List of all show and movie sources in Ember
    Private embersources As New List(Of String)

#End Region 'Fields

#Region "Constructors"
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
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
    Private Sub dlgHost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load labels/translations
        Setup()
        'check if existing host is edited ("Edit" button in setting form) or a new host is created("Add" button in setting form)
        If Not currentHost Is Nothing Then
            'load data of currently edited host into controls
            Me.txtLabel.Text = currentHost.Label
            Me.txtHostIP.Text = currentHost.address
            Me.txtWebPort.Text = CStr(currentHost.port)
            Me.txtUsername.Text = currentHost.username
            Me.txtPassword.Text = currentHost.password
            chkHostRealTimeSync.Checked = currentHost.realtimesync
            Me.txtHostMoviesetPath.Text = currentHost.MovieSetArtworksPath
        Else
            'new host entry
            currentHost = New KodiInterface.Host
        End If
        'load sources of selected host and display values in datagrid
        PopulateHostSources()
        dgvHostSources.Enabled = True
    End Sub
    ''' <summary>
    ''' Actions on module startup
    ''' </summary>
    ''' <param name="sender">startup of module</param>
    ''' <remarks>
    ''' - set labels/translation text
    ''' 2015/06/26 Cocotus - First implementation
    Private Sub Setup()
        Me.lblCompiling.Text = Master.eLang.GetString(326, "Loading...")
        Me.Text = Master.eLang.GetString(1422, "Kodi Interface")
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
        Me.btnHostCheck.Text = Master.eLang.GetString(1423, "Check Connection")
        Me.btnHostPopulateSources.Text = Master.eLang.GetString(1424, "Populate Sources")

        Me.gbHostDetails.Text = Master.eLang.GetString(1425, "Kodi Host")
        Me.gbHostMoviesetPath.Text = "Kodi " & Master.eLang.GetString(986, "MovieSet Artwork Folder")

        Me.chkHostRealTimeSync.Text = Master.eLang.GetString(1429, "Enable Real Time synchronization")
        Me.lblHostLabel.Text = Master.eLang.GetString(232, "Name") & ":"
        Me.lblHostIP.Text = Master.eLang.GetString(1430, "Kodi IP") & ":"
        Me.lblHostPassword.Text = Master.eLang.GetString(426, "Password:")
        Me.lblHostUsername.Text = Master.eLang.GetString(425, "Username:")
        Me.lblHostWebserverPort.Text = Master.eLang.GetString(1431, "Kodi Port") & ":"

        Me.colHostEmberSource.HeaderText = Master.eLang.GetString(1432, "Ember Source")
        Me.colHostSource.HeaderText = Master.eLang.GetString(1433, "Kodi Source")
        Me.colHostType.HeaderText = Master.eLang.GetString(1288, "Type")
    End Sub

    ''' <summary>
    ''' Load sources (Ember + Kodi) of selected host and display values in datagrid
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    Private Sub PopulateHostSources()
        dgvHostSources.Rows.Clear()
        Dim sPath As String

        'populate all library sources in Ember into embersources
        For Each moviesources As Structures.MovieSource In Master.MovieSources
            embersources.Add(moviesources.Path)
        Next
        For Each showsources As Structures.TVSource In Master.TVSources
            embersources.Add(showsources.Path)
        Next

        For Each s As String In embersources
            sPath = s
            Dim i As Integer = dgvHostSources.Rows.Add(sPath)
            Dim dcbRemotesource As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(1), DataGridViewComboBoxCell)
            Dim dcbSourceType As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(2), DataGridViewComboBoxCell)

            Dim items As New Dictionary(Of String, Enums.ContentType)
            items.Add(Master.eLang.None, Enums.ContentType.None)
            items.Add(Master.eLang.GetString(36, "Movies"), Enums.ContentType.Movie)
            items.Add(Master.eLang.GetString(653, "TV Shows"), Enums.ContentType.TV)
            'Me.cbMoviePosterPrefSize.DataSource = items.ToList
            'Me.cbMoviePosterPrefSize.DisplayMember = "Key"
            'Me.cbMoviePosterPrefSize.ValueMember = "Value"

            'Dim ltype As New List(Of String)
            'ltype.Add("movie")
            'ltype.Add("tvshow")
            Dim l As New List(Of String)
            l.Add(String.Empty) 'Empty Entry for combobox
            If currentHost.Sources IsNot Nothing AndAlso currentHost.Sources(0) IsNot Nothing Then
                'don't add kodi music/picture paths to Ember (for now...)
                For Each ksource In currentHost.Sources
                    If Not String.IsNullOrEmpty(ksource.RemotePath) AndAlso Not l.Contains(ksource.RemotePath) AndAlso (ksource.ContentType = Enums.ContentType.Movie OrElse ksource.ContentType = Enums.ContentType.TV) Then
                        l.Add(ksource.RemotePath)
                    End If
                Next
            End If
            dcbRemotesource.DataSource = l.ToArray
            'dcbSourceType.DataSource = ltype.ToArray
            'try to load corresponding remotepath and type for current Ember Source from host settings
            If currentHost.Sources IsNot Nothing AndAlso currentHost.Sources(0) IsNot Nothing Then
                'don't add kodi music/picture paths to Ember (for now...)
                For Each ksource In currentHost.Sources
                    If Not String.IsNullOrEmpty(ksource.RemotePath) AndAlso s = ksource.LocalPath AndAlso (ksource.ContentType = Enums.ContentType.Movie OrElse ksource.ContentType = Enums.ContentType.TV) Then
                        dcbRemotesource.Value = ksource.RemotePath
                        dcbSourceType.Value = ksource.ContentType
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Get avalaible sources of host
    ''' </summary>
    ''' <param name="sender">"Populate Sources" button</param>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Send JSON API request to Kodi to get all sources from host
    ''' request will be executed in backgroundworker
    Private Sub btnPopulateSources_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHostPopulateSources.Click
        'disable controls during request
        pnlLoading.Visible = True
        gbHostDetails.Enabled = False
        btnHostPopulateSources.Enabled = False
        btnOK.Enabled = False
        btnCancel.Enabled = False
        txtHostIP.Enabled = False
        txtLabel.Enabled = False
        txtPassword.Enabled = False
        txtWebPort.Enabled = False
        txtUsername.Enabled = False
        dgvHostSources.Enabled = False
        'set currentHost
        currentHost = New KodiInterface.Host With {.Label = txtLabel.Text, .Address = txtHostIP.Text, .Port = CInt(txtWebPort.Text), .Username = txtUsername.Text, .Password = txtPassword.Text, .RealTimeSync = chkHostRealTimeSync.Checked, .MovieSetArtworksPath = txtHostMoviesetPath.Text}
        'start request in backgroundworker -> getSources
        bwLoadInfo.RunWorkerAsync(1)
        While bwLoadInfo.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        'check if any sources were retrieved
        If Not currentHostSources Is Nothing AndAlso currentHostSources.Count > 0 Then
            dgvHostSources.Rows.Clear()
            Dim lstEmberSources As New List(Of String)
            Dim lstKodiSources As New List(Of String)
            lstKodiSources.Add("")
            For Each EmberSource In embersources
                lstEmberSources.Add(EmberSource)
            Next
            For Each source In currentHostSources
                lstKodiSources.Add(source.file)
            Next
            For Each s In embersources
                'new row
                Dim sPath As String = s
                Dim i As Integer = dgvHostSources.Rows.Add(sPath)
                'put Kodi Sources selection into datagrid cell
                Dim dcbKodi As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(1), DataGridViewComboBoxCell)
                Dim dcbSourceType As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(2), DataGridViewComboBoxCell)
                dcbSourceType.DataSource = Nothing
                dcbKodi.DataSource = Nothing
                Dim ltype As New List(Of String)
                ltype.Add("movie")
                ltype.Add("tvshow")
                dcbSourceType.DataSource = ltype.ToArray
                dcbKodi.DataSource = lstKodiSources.ToArray
                'put Ember Sources selection into datagrid cell
                'Dim dcbEmber As DataGridViewComboBoxCell = DirectCast(dgvHostSources.Rows(i).Cells(0), DataGridViewComboBoxCell)
                'dcbEmber.DataSource = lstEmberSources.ToArray
            Next
        Else
            MessageBox.Show(Master.eLang.GetString(1434, "There was a problem communicating with host."), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        'enable controls again
        pnlLoading.Visible = False
        btnHostPopulateSources.Enabled = True
        btnOK.Enabled = True
        btnCancel.Enabled = True
        txtHostIP.Enabled = True
        txtLabel.Enabled = True
        txtPassword.Enabled = True
        txtWebPort.Enabled = True
        txtUsername.Enabled = True
        dgvHostSources.Enabled = True
        gbHostDetails.Enabled = True
    End Sub

    ''' <summary>
    ''' Backgroundworker job(s): GetHostSources, check for JSONversion
    ''' </summary>
    ''' <param name="sender">backgroundworker</param>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Request will be executed in backgroundworker
    Private Sub bwLoadInfo_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwLoadInfo.DoWork
        Select Case CInt(e.Argument)
            Case 1
                'API request: Get all sources of current host
                currentHostSources = KodiInterface.GetSources(currentHost)
            Case 2
                'API request: Get JSONRPC version of host
                JsonHostversion = KodiInterface.GetJSONHostVersion(currentHost)
        End Select
    End Sub

    ''' <summary>
    ''' API request: Check connection to current host
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation
    ''' Send JSON API request to Kodi to check if entered host data is correct
    Private Sub btnHostCheck_Click(sender As Object, e As EventArgs) Handles btnHostCheck.Click

        JsonHostversion = ""
        'set currentHost
        currentHost = New KodiInterface.Host With {.Label = txtLabel.Text, .Address = txtHostIP.Text, .Port = CInt(txtWebPort.Text), .Username = txtUsername.Text, .Password = txtPassword.Text, .RealTimeSync = chkHostRealTimeSync.Checked, .MovieSetArtworksPath = txtHostMoviesetPath.Text}
        'start backgroundworker: check for JSONversion
        bwLoadInfo.RunWorkerAsync(2)
        While bwLoadInfo.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
        If JsonHostversion = String.Empty Then
            MessageBox.Show(Master.eLang.GetString(1434, "There was a problem communicating with host."), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            MessageBox.Show(Master.eLang.GetString(1435, "Connection to host successful!") & Environment.NewLine & "API-Version: " & JsonHostversion, "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ''' <summary>
    '''  Setting default value of combobox in datagridview
    ''' </summary>
    ''' <param name="sender">dgvHostSources.CellFormatting event</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' </remarks>
    Private Sub dgvHostSources_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvHostSources.CellFormatting
        If e.ColumnIndex = 2 AndAlso (e.Value Is Nothing OrElse e.Value.ToString = String.Empty) Then
            ' default value
            e.Value = "movie"
        End If
    End Sub


    ''' <summary>
    '''  Close dialog
    ''' </summary>
    ''' <param name="sender">"Cancel" button in dialog</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' </remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If String.IsNullOrEmpty(Me.txtLabel.Text) Then
            MessageBox.Show(Master.eLang.GetString(1436, "Please enter a unique name for host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.txtHostIP.Text) Then
            MessageBox.Show(Master.eLang.GetString(1437, "You must enter an IP for this host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If String.IsNullOrEmpty(Me.txtWebPort.Text) Then
            MessageBox.Show(Master.eLang.GetString(1438, "You must enter a port for this host!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each host In lstAllHosts
            If host.Label = txtLabel.Text Then
                MessageBox.Show(Master.eLang.GetString(1439, "The name you are attempting to use for this host is already in use. Please choose another!"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region 'Methods

#Region "Nested Types"
#End Region 'Nested Types


End Class