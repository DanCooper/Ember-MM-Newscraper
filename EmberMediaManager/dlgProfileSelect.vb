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

Public Class dlgProfileSelect

#Region "Fields"

    'Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private _folderlist As New List(Of String)
    Private _selectedprofile As String = String.Empty

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property SelectedProfileFullPath As String
        Get
            If Not String.IsNullOrEmpty(_selectedprofile) Then
                Return Path.Combine(Master.eProfiles.ProfilesFullPath, _selectedprofile)
            Else
                Return String.Empty
            End If
        End Get
    End Property

#End Region

#Region "Methods"

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If clbDirectories.CheckedItems.Count > 0 Then
            Master.eProfiles.Autoload = chkProfileAuto.Checked
            Master.eProfiles.DefaultProfile = clbDirectories.CheckedItems(0).ToString
            Master.eProfiles.SaveSettings()
        End If
    End Sub

    Private Sub clbDirectories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbDirectories.SelectedIndexChanged
        _selectedprofile = clbDirectories.SelectedItem.ToString
    End Sub

    Private Sub clbDirectories_ItemCheck(sender As Object, e As EventArgs) Handles clbDirectories.ItemCheck
        RemoveHandler clbDirectories.ItemCheck, AddressOf clbDirectories_ItemCheck

        Dim iCurrent As Integer = DirectCast(sender, CheckedListBox).SelectedIndex
        For i = 0 To clbDirectories.Items.Count - 1
            clbDirectories.SetItemChecked(i, i = iCurrent)
        Next

        AddHandler clbDirectories.ItemCheck, AddressOf clbDirectories_ItemCheck
    End Sub

    Private Sub dlgProfiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUp()
    End Sub

    Private Sub dlgProfileSelect_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Directory.Exists(Path.Combine(Functions.AppPath, "Settings")) Then
            Dim dInfoSettings As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Settings"))
            Dim dInfoDefaultProfile As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, String.Concat("Profiles\Default")))
            If Not dInfoDefaultProfile.Exists OrElse dInfoDefaultProfile.GetFiles.Count = 0 Then
                If MessageBox.Show(String.Format("Looks like your settings are saved in the old ""Settings"" directory.{0}Do you want move all that settings to the ""Default"" profile?", Environment.NewLine), "Old ""Settings"" directory found", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Directory.Delete(dInfoDefaultProfile.FullName)
                    dInfoSettings.MoveTo(dInfoDefaultProfile.FullName)
                End If
            End If
        End If
        _folderlist = LoadProfileFolders()
        PopulateDirectoryList()
    End Sub

    Private Function LoadProfileFolders() As List(Of String)
        Dim dList As New List(Of String)
        Dim dInfo As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Profiles"))
        Dim inList As IEnumerable(Of DirectoryInfo) = dInfo.GetDirectories

        dList.Add("Default")
        For Each sDirs As DirectoryInfo In inList.Where(Function(f) Not f.Name = "Default")
            dList.Add(sDirs.Name)
        Next

        Return dList
    End Function

    Private Sub PopulateDirectoryList()
        clbDirectories.Items.Clear()
        clbDirectories.Items.AddRange(_folderlist.ToArray)
        If Master.eProfiles.DefaultProfileSpecified Then
            clbDirectories.SelectedItem = Master.eProfiles.DefaultProfile
            clbDirectories.SetItemChecked(clbDirectories.SelectedIndex, True)
        Else
            clbDirectories.SelectedItem = "Default"
        End If
        chkProfileAuto.Checked = Master.eProfiles.Autoload
    End Sub

    Private Sub SetUp()
        'Can't use eLang, settings with language info is not loaded at this point
    End Sub

#End Region 'Methods

End Class