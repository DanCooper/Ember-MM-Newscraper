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
            RegistryCascadeGroupAdd()
            If Me.chkAddMovieSource.Checked Then
                RegistryCascadeAddMovieSourceAdd()
                RegistryAddMovieSourceRemove()
            End If
            If Me.chkAddTVShowSource.Checked Then
                RegistryCascadeAddTVShowSourceAdd()
                RegistryAddTVShowSourceRemove()
            End If
            If Me.chkScanFolder.Checked Then
                RegistryCascadeScanFolderAdd()
                RegistryScanFolderRemove()
            End If
        Else
            RegistryCascadeGroupRemove()
            If Me.chkAddMovieSource.Checked Then
                RegistryCascadeAddMovieSourceRemove()
                RegistryAddMovieSourceAdd()
            End If
            If Me.chkAddTVShowSource.Checked Then
                RegistryCascadeAddTVShowSourceRemove()
                RegistryAddTVShowSourceAdd()
            End If
            If Me.chkScanFolder.Checked Then
                RegistryCascadeScanFolderRemove()
                RegistryScanFolderAdd()
            End If
        End If
    End Sub

    Private Sub chkAddMovieSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddMovieSource.CheckedChanged
        If Me.chkAddMovieSource.Checked AndAlso Me.chkCascade.Checked Then
            RegistryCascadeAddMovieSourceAdd()
            RegistryAddMovieSourceRemove()
            RegistryCascadeGroupAdd()
        ElseIf Me.chkAddMovieSource.Checked AndAlso Not Me.chkCascade.Checked Then
            RegistryCascadeAddMovieSourceRemove()
            RegistryAddMovieSourceAdd()
            RegistryCascadeGroupAdd()
        Else
            RegistryCascadeAddMovieSourceRemove()
            RegistryAddMovieSourceRemove()
            RegistryCascadeGroupAdd()
        End If
    End Sub

    Private Sub chkAddTVShowSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkAddTVShowSource.CheckedChanged
        If Me.chkAddTVShowSource.Checked AndAlso Me.chkCascade.Checked Then
            RegistryCascadeAddTVShowSourceAdd()
            RegistryAddTVShowSourceRemove()
            RegistryCascadeGroupAdd()
        ElseIf Me.chkAddTVShowSource.Checked AndAlso Not Me.chkCascade.Checked Then
            RegistryCascadeAddTVShowSourceRemove()
            RegistryAddTVShowSourceAdd()
            RegistryCascadeGroupAdd()
        Else
            RegistryCascadeAddTVShowSourceRemove()
            RegistryAddTVShowSourceRemove()
            RegistryCascadeGroupAdd()
        End If
    End Sub

    Private Sub chkScanFolder_CheckedChanged(sender As Object, e As EventArgs) Handles chkScanFolder.CheckedChanged
        If Me.chkScanFolder.Checked AndAlso Me.chkCascade.Checked Then
            RegistryCascadeScanFolderAdd()
            RegistryScanFolderRemove()
            RegistryCascadeGroupAdd()
        ElseIf Me.chkScanFolder.Checked AndAlso Not Me.chkCascade.Checked Then
            RegistryCascadeScanFolderRemove()
            RegistryAddMovieSourceAdd()
            RegistryCascadeGroupAdd()
        Else
            RegistryCascadeScanFolderRemove()
            RegistryScanFolderRemove()
            RegistryCascadeGroupAdd()
        End If
    End Sub

    Private Sub RegistryCascadeGroupAdd()
        Dim regKey As RegistryKey
        regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            RegistryCascadeGroupRemove()
        End If

        Dim Items As New List(Of String)
        If Me.chkAddMovieSource.Checked Then Items.Add("EmberMediaManager.AddMovieSource")
        If Me.chkAddTVShowSource.Checked Then Items.Add("EmberMediaManager.AddTVShowSource")
        If Me.chkScanFolder.Checked Then Items.Add("EmberMediaManager.ScanFolder")

        If Items.Count > 0 Then
            regKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\EmberMediaManager")
            regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
            regKey.SetValue("MUIVerb", "Ember Media Manager")
            regKey.SetValue("Position", "Bottom")
            regKey.SetValue("SubCommands", Strings.Join(Items.ToArray, ";"))
        End If
        regKey.Close()
    End Sub

    Private Sub RegistryCascadeGroupRemove()
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell", True)
            regKey.DeleteSubKey("EmberMediaManager", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryCascadeScanFolderAdd()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            RegistryCascadeScanFolderRemove()
        End If

        regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.ScanFolder")
        regKey.SetValue(String.Empty, Master.eLang.GetString(1399, "Scan folder for new content"))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -scanfolder ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryCascadeScanFolderRemove()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.ScanFolder", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryCascadeAddMovieSourceAdd()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            RegistryCascadeAddMovieSourceRemove()
        End If

        regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddMovieSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1400, "Add folder as a new movie source"), "..."))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addmoviesource ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryCascadeAddMovieSourceRemove()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddMovieSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryCascadeAddTVShowSourceAdd()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            RegistryCascadeAddTVShowSourceRemove()
        End If

        regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddTVShowSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1401, "Add folder as a new tv show source"), "..."))
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addtvshowsource ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryCascadeAddTVShowSourceRemove()
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddTVShowSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryScanFolderAdd()
        Dim regKey As RegistryKey
        regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            RegistryScanFolderRemove()
        End If

        regKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\EmberMediaManager.ScanFolder")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1399, "Scan folder for new content"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -scanfolder ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryScanFolderRemove()
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.ScanFolder", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryAddMovieSourceAdd()
        Dim regKey As RegistryKey
        regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            RegistryAddMovieSourceRemove()
        End If

        regKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\EmberMediaManager.AddMovieSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1400, "Add folder as a new movie source"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addmoviesource ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryAddMovieSourceRemove()
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddMovieSource", True)
            regKey.Close()
        End If
    End Sub

    Private Sub RegistryAddTVShowSourceAdd()
        Dim regKey As RegistryKey
        regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            RegistryAddMovieSourceRemove()
        End If

        regKey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\EmberMediaManager.AddTVShowSource")
        regKey.SetValue(String.Empty, String.Concat(Master.eLang.GetString(1401, "Add folder as a new tv show source"), "..."))
        regKey.SetValue("Icon", String.Concat(Application.ExecutablePath, ",0").Replace("\", "\\"))
        regKey.SetValue("Position", "Bottom")
        regKey = regKey.CreateSubKey("command")
        regKey.SetValue(String.Empty, String.Concat("""", Application.ExecutablePath, """ -addtvshowsource ""%1""").Replace("\", "\\"))
        regKey.Close()
    End Sub

    Private Sub RegistryAddTVShowSourceRemove()
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            regKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell", True)
            regKey.DeleteSubKeyTree("EmberMediaManager.AddTVShowSource", True)
            regKey.Close()
        End If
    End Sub

    Private Function RegistryCascadeGroupIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeScanFolderIsEnabled() As Boolean
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeAddMovieSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryCascadeAddTVShowSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryScanFolderIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.ScanFolder", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryAddMovieSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddMovieSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function RegistryAddTVShowSourceIsEnabled() As Boolean
        Dim regKey As RegistryKey = Registry.ClassesRoot.OpenSubKey("Directory\\shell\\EmberMediaManager.AddTVShowSource", True)

        If regKey IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Nested Types"

#End Region 'Nested Types

End Class
