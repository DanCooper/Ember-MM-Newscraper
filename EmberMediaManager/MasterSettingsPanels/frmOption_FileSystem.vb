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
Imports System.IO

Public Class frmOption_FileSystem
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

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
            .Contains = Enums.SettingsPanelType.OptionsFileSystem,
            .ImageIndex = 4,
            .Order = 300,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_FileSystem",
            .Title = Master.eLang.GetString(553, "File System"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.Options.FileSystem
            .VirtualDriveDriveLetter = cbVirtualDriveDriveLetter.Text
            .VirtualDriveBinPath = txtVirtualDrivePath.Text

            Save_ExcludedPaths()
            Save_ValidSubtitleExtensions()
            Save_ValidThemeExtensions()
            Save_ValidVideoExtensions()
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Options.FileSystem
            cbVirtualDriveDriveLetter.SelectedItem = .VirtualDriveDriveLetter
            txtVirtualDrivePath.Text = .VirtualDriveBinPath

            DataGridView_Fill_ExcludedPaths(Master.DB.GetExcludedPaths)
            DataGridView_Fill_ValidSubtitleExtensions(.ValidSubtitleExtensions)
            DataGridView_Fill_ValidThemeExtensions(.ValidThemeExtensions)
            DataGridView_Fill_ValidVideoExtensions(.ValidVideoExtensions)
        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            colExcludedPathsBrowse.HeaderText = .GetString(765, "Browse")
            colExcludedPathsPath.HeaderText = .GetString(926, "Full Path")
            gbExcludedPaths.Text = .GetString(1273, "Excluded Paths")
            gbValidSubtitlesExtensions.Text = .GetString(1284, "Valid Subtitles Extensions")
            gbValidThemeExtensions.Text = .GetString(1081, "Valid Theme Extensions")
            gbValidVideoExtensions.Text = .GetString(534, "Valid Video Extensions")
            gbVirtualDrive.Text = .GetString(1261, "Virtual Drive")
            lblVirtualDriveDriveLetter.Text = .GetString(989, "Driveletter")
            lblVirtualDriveDrivePath.Text = .GetString(990, "Path to VCDMount.exe (Virtual CloneDrive)")
        End With
    End Sub

    Private Sub Enable_ApplyButton() Handles _
        cbVirtualDriveDriveLetter.SelectedIndexChanged,
        txtVirtualDrivePath.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_ExcludedPaths_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvExcludedPaths.CellClick
        If e.ColumnIndex = 1 Then
            Try
                With fbdBrowse
                    If dgvExcludedPaths.Rows(e.RowIndex).Cells(0).Value IsNot Nothing AndAlso
                        Not String.IsNullOrEmpty(dgvExcludedPaths.Rows(e.RowIndex).Cells(0).Value.ToString) Then
                        .SelectedPath = dgvExcludedPaths.Rows(e.RowIndex).Cells(0).Value.ToString
                    Else
                        .SelectedPath = String.Empty
                    End If
                    If .ShowDialog = DialogResult.OK Then
                        If Not String.IsNullOrEmpty(.SelectedPath) Then
                            dgvExcludedPaths.Rows.Add(New Object() { .SelectedPath})
                        End If
                    End If
                End With
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End If
    End Sub

    Private Sub DataGridView_Fill_ExcludedPaths(ByVal List As List(Of String))
        dgvExcludedPaths.Rows.Clear()
        For Each path In List
            Dim i As Integer = dgvExcludedPaths.Rows.Add(New Object() {path})
        Next
        dgvExcludedPaths.ClearSelection()
    End Sub

    Private Sub DataGridView_ExcludedPaths_RowsAdded() Handles dgvExcludedPaths.RowsAdded
        RaiseEvent NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_TV()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_ExcludedPaths_RowsRemoved() Handles dgvExcludedPaths.RowsRemoved
        RaiseEvent NeedsDBUpdate_Movie(-1)
        RaiseEvent NeedsDBUpdate_TV(-1)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_Fill_ValidSubtitleExtensions(ByVal List As List(Of String))
        dgvValidSubtitleExtensions.Rows.Clear()
        For Each ext In List
            Dim i As Integer = dgvValidSubtitleExtensions.Rows.Add(New Object() {ext})
        Next
        dgvValidSubtitleExtensions.ClearSelection()
    End Sub

    Private Sub DataGridView_Fill_ValidThemeExtensions(ByVal List As List(Of String))
        dgvValidThemeExtensions.Rows.Clear()
        For Each ext In List
            Dim i As Integer = dgvValidThemeExtensions.Rows.Add(New Object() {ext})
        Next
        dgvValidThemeExtensions.ClearSelection()
    End Sub

    Private Sub DataGridView_Fill_ValidVideoExtensions(ByVal List As List(Of String))
        dgvValidVideoExtensions.Rows.Clear()
        For Each ext In List
            Dim i As Integer = dgvValidVideoExtensions.Rows.Add(New Object() {ext})
        Next
        dgvValidVideoExtensions.ClearSelection()
    End Sub

    Private Sub DataGridView_ValidExtensions_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles _
        dgvValidSubtitleExtensions.CellEndEdit,
        dgvValidThemeExtensions.CellEndEdit,
        dgvValidVideoExtensions.CellEndEdit

        Dim currDGV = DirectCast(sender, DataGridView)
        If currDGV.Rows(e.RowIndex).Cells(0).Value IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(currDGV.Rows(e.RowIndex).Cells(0).Value.ToString.Trim) AndAlso
            Not currDGV.Rows(e.RowIndex).Cells(0).Value.ToString.Trim.StartsWith(".") Then
            currDGV.Rows(e.RowIndex).Cells(0).Value = String.Concat(".", currDGV.Rows(e.RowIndex).Cells(0).Value.ToString.Trim)
        End If
    End Sub

    Private Sub DataGridView_ValidSubtitleExtensions_RowsAdded_RowsRemoved() Handles dgvValidSubtitleExtensions.RowsAdded, dgvValidSubtitleExtensions.RowsRemoved
        RaiseEvent NeedsReload_Movie()
        RaiseEvent NeedsReload_TVEpisode()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_ValidThemeExtensions_RowsAdded_RowsRemoved() Handles dgvValidThemeExtensions.RowsAdded, dgvValidThemeExtensions.RowsRemoved
        RaiseEvent NeedsReload_Movie()
        RaiseEvent NeedsReload_TVShow()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_ValidVideoExtensions_RowsAdded() Handles dgvValidVideoExtensions.RowsAdded
        RaiseEvent NeedsDBUpdate_Movie(-1)
        RaiseEvent NeedsDBUpdate_TV(-1)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_ValidVideoExtensions_RowsRemoved() Handles dgvValidVideoExtensions.RowsRemoved
        RaiseEvent NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_TV()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub LoadDefaults_ValidSubtitleExtensions() Handles btnValidSubtitleExtensionsDefaults.Click
        If MessageBox.Show(Master.eLang.GetString(1283, "Are you sure you want to reset to the default list of valid subtitle extensions?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_ValidSubtitleExtensions(Master.eSettings.Options.FileSystem.ValidSubtitleExtensions.GetDefaults(Enums.DefaultType.ValidSubtitleExts))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub LoadDefaults_ValidThemeExtensions() Handles btnValidThemeExtensionsDefaults.Click
        If MessageBox.Show(Master.eLang.GetString(1080, "Are you sure you want to reset to the default list of valid theme extensions?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_ValidThemeExtensions(Master.eSettings.Options.FileSystem.ValidThemeExtensions.GetDefaults(Enums.DefaultType.ValidThemeExts))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub LoadDefaults_ValidVideoExtensions() Handles btnValidVideoExtensionsDefaults.Click
        If MessageBox.Show(Master.eLang.GetString(843, "Are you sure you want to reset to the default list of valid video extensions?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_ValidVideoExtensions(Master.eSettings.Options.FileSystem.ValidVideoExtensions.GetDefaults(Enums.DefaultType.ValidVideoExts))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub Save_ExcludedPaths()
        For Each path In Master.DB.GetExcludedPaths
            Master.DB.RemoveExcludedPath(path, False)
        Next
        For Each r As DataGridViewRow In dgvExcludedPaths.Rows
            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                Master.DB.AddExcludedPath((r.Cells(0).Value.ToString.Trim))
            End If
        Next
    End Sub

    Private Sub Save_ValidSubtitleExtensions()
        With Master.eSettings.Options.FileSystem.ValidSubtitleExtensions
            .Clear()
            For Each r As DataGridViewRow In dgvValidSubtitleExtensions.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                    .Add(r.Cells(0).Value.ToString.Trim)
                End If
            Next
        End With
    End Sub

    Private Sub Save_ValidThemeExtensions()
        With Master.eSettings.Options.FileSystem.ValidThemeExtensions
            .Clear()
            For Each r As DataGridViewRow In dgvValidThemeExtensions.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                    .Add(r.Cells(0).Value.ToString.Trim)
                End If
            Next
        End With
    End Sub

    Private Sub Save_ValidVideoExtensions()
        With Master.eSettings.Options.FileSystem.ValidVideoExtensions
            .Clear()
            For Each r As DataGridViewRow In dgvValidVideoExtensions.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                    .Add(r.Cells(0).Value.ToString.Trim)
                End If
            Next
        End With
    End Sub

    Private Sub VirtualDrive_PathBrowse_Click(sender As Object, e As EventArgs) Handles btnVirtualDrivePathBrowse.Click
        Try
            With fileBrowse
                .Filter = "Virtual CloneDrive|VCDMount.exe"
                .InitialDirectory = "C:\Program Files (x86)\Elaborate Bytes\VirtualCloneDrive"
                If .ShowDialog = DialogResult.OK Then
                    If Not String.IsNullOrEmpty(.FileName) AndAlso File.Exists(.FileName) Then
                        txtVirtualDrivePath.Text = .FileName
                    End If
                End If
            End With
        Catch ex As Exception
            _Logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

End Class