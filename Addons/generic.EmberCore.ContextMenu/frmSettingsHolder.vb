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
Imports Microsoft.Win32

Public Class frmSettingsHolder

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private SettingsHasChanged As Boolean = False

#End Region 'Fields

    Public Event ModuleSettingsChanged()

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        SetUp()
        FillSettings()
    End Sub

    Private Sub SetUp()
        Me.chkCascade.Text = Master.eLang.GetString(1397, "Cascade context menu")
        Me.chkEnable.Text = Master.eLang.GetString(1396, "Integrate Ember Media Manager to shell context menu")
        Me.chkScanFolder.Text = Master.eLang.GetString(1399, "Scan folder for new content")
        Me.chkAddMovieSource.Text = String.Concat(Master.eLang.GetString(1400, "Add folder as a new movie source"), "...")
        Me.chkAddTVShowSource.Text = String.Concat(Master.eLang.GetString(1401, "Add folder as a new tv show source"), "...")
        Me.gbItems.Text = String.Concat(Master.eLang.GetString(1398, "Context menu items"), ":")
    End Sub

    Private Sub FillSettings()
        RemoveHandler Me.chkCascade.CheckedChanged, AddressOf chkCascade_CheckedChanged
        RemoveHandler Me.chkScanFolder.CheckedChanged, AddressOf chkScanFolder_CheckedChanged
        RemoveHandler Me.chkAddMovieSource.CheckedChanged, AddressOf chkAddMovieSource_CheckedChanged
        RemoveHandler Me.chkAddTVShowSource.CheckedChanged, AddressOf chkAddTVShowSource_CheckedChanged

        If RegistryCascadeGroupIsEnabled() Then Me.chkCascade.Checked = True
        If RegistryCascadeScanFolderIsEnabled() OrElse RegistryScanFolderIsEnabled() Then Me.chkScanFolder.Checked = True
        If RegistryCascadeAddMovieSourceIsEnabled() OrElse RegistryAddMovieSourceIsEnabled() Then Me.chkAddMovieSource.Checked = True
        If RegistryCascadeAddTVShowSourceIsEnabled() OrElse RegistryAddTVShowSourceIsEnabled() Then Me.chkAddTVShowSource.Checked = True

        AddHandler Me.chkCascade.CheckedChanged, AddressOf chkCascade_CheckedChanged
        AddHandler Me.chkScanFolder.CheckedChanged, AddressOf chkScanFolder_CheckedChanged
        AddHandler Me.chkAddMovieSource.CheckedChanged, AddressOf chkAddMovieSource_CheckedChanged
        AddHandler Me.chkAddTVShowSource.CheckedChanged, AddressOf chkAddTVShowSource_CheckedChanged

        If Me.chkCascade.Checked OrElse Me.chkScanFolder.Checked OrElse Me.chkAddMovieSource.Checked OrElse Me.chkAddTVShowSource.Checked Then Me.chkEnable.Checked = True
    End Sub

    Private Sub chkEnable_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnable.CheckedChanged
        If Not Me.chkEnable.Checked Then
            Me.chkCascade.Checked = False
            Me.chkScanFolder.Checked = False
            Me.chkAddMovieSource.Checked = False
            Me.chkAddTVShowSource.Checked = False
        End If
        Me.chkCascade.Enabled = Me.chkEnable.Checked
        Me.gbItems.Enabled = Me.chkEnable.Checked
    End Sub

    Private Sub chkCascade_CheckedChanged(sender As Object, e As EventArgs) Handles chkCascade.CheckedChanged
        If Me.chkCascade.Checked Then
            Cascade_Group_Add()
            Flat_AddMovieSource_Remove()
            Flat_AddTVShowSource_Remove()
            Flat_ScanFolder_Remove()
        Else
            Cascade_Group_Remove()
        End If
    End Sub

    Private Sub chkAddMovieSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddMovieSource.CheckedChanged
        If Me.chkCascade.Checked Then
            Cascade_Group_Add()
        ElseIf Me.chkAddMovieSource.Checked Then
            Flat_AddMovieSource_Add()
        Else
            Flat_AddMovieSource_Remove()
        End If
    End Sub

    Private Sub chkAddTVShowSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddTVShowSource.CheckedChanged
        If Me.chkCascade.Checked Then
            Cascade_Group_Add()
        ElseIf Me.chkAddTVShowSource.Checked Then
            Flat_AddTVShowSource_Add()
        Else
            Flat_AddTVShowSource_Remove()
        End If
    End Sub

    Private Sub chkScanFolder_CheckedChanged(sender As Object, e As EventArgs) Handles chkScanFolder.CheckedChanged
        If Me.chkCascade.Checked Then
            Cascade_Group_Add()
        ElseIf Me.chkScanFolder.Checked Then
            Flat_ScanFolder_Add()
        Else
            Flat_ScanFolder_Remove()
        End If
    End Sub

    Private Sub Cascade_Group_Add()
        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            Cascade_Group_Remove()
        End If

        If Me.chkAddMovieSource.Checked OrElse Me.chkAddTVShowSource.Checked OrElse Me.chkScanFolder.Checked Then
            regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager")
            regKey.SetValue("ExtendedSubCommandsKey", "Directory\shell\EmberMediaManager\menus")
            regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
            regKey.SetValue("MUIVerb", "Ember Media Manager")
            regKey.SetValue("Position", "Bottom")
            regKey.CreateSubKey("menus\\shell")

            'add a separator (empty SubKey with a name between two submenu names (ascending))
            If (Me.chkAddMovieSource.Checked OrElse Me.chkAddTVShowSource.Checked) AndAlso Me.chkScanFolder.Checked Then
                regKey.CreateSubKey("menus\\shell\s-separator")
            End If

            If Me.chkAddMovieSource.Checked Then
                Cascade_AddMovieSource_Add()
            End If
            If Me.chkAddTVShowSource.Checked Then
                Cascade_AddTVShowSource_Add()
            End If
            If Me.chkScanFolder.Checked Then
                Cascade_ScanFolder_Add()
            End If
        End If
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Cascade_Group_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Cascade_ScanFolder_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\ScanFolder", True)

        If regKey IsNot Nothing Then
            Cascade_ScanFolder_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\ScanFolder")
        regKey.SetValue("MUIVerb", Master.eLang.GetString(1399, "Scan folder for new content"))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -scanfolder ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Cascade_ScanFolder_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\ScanFolder", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell", True)
            regKey.DeleteSubKeyTree("ScanFolder", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Cascade_AddMovieSource_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddMovieSource", True)

        If regKey IsNot Nothing Then
            Cascade_AddMovieSource_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddMovieSource")
        regKey.SetValue("MUIVerb", String.Concat(Master.eLang.GetString(1400, "Add folder as a new movie source"), "..."))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addmoviesource ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Cascade_AddMovieSource_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddMovieSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell", True)
            regKey.DeleteSubKeyTree("AddMovieSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Cascade_AddTVShowSource_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Cascade_AddTVShowSource_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddTVShowSource")
        regKey.SetValue("MUIVerb", String.Concat(Master.eLang.GetString(1401, "Add folder as a new tv show source"), "..."))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addtvshowsource ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Cascade_AddTVShowSource_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddTVShowSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell", True)
            regKey.DeleteSubKeyTree("AddTVShowSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Flat_ScanFolder_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            Flat_ScanFolder_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.ScanFolder")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1399, "Scan folder for new content"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -scanfolder ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Flat_ScanFolder_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.ScanFolder", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Flat_AddMovieSource_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            Flat_AddMovieSource_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddMovieSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1400, "Add folder as a new movie source"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addmoviesource ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Flat_AddMovieSource_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddMovieSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub Flat_AddTVShowSource_Add()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Flat_AddMovieSource_Remove()
        End If

        regKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddTVShowSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1401, "Add folder as a new tv show source"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addtvshowsource ""%1""").Replace("\", "\\"))
        If regKey IsNot Nothing Then regKey.Close()
    End Sub

    Private Sub Flat_AddTVShowSource_Remove()
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddTVShowSource", True)
            regKey.Close()
        End If
    End Sub

    Private Function RegistryCascadeGroupIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeScanFolderIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\ScanFolder", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeAddMovieSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddMovieSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeAddTVShowSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager\\menus\\shell\\AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryScanFolderIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryAddMovieSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryAddTVShowSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Nested Types"

#End Region 'Nested Types

End Class
