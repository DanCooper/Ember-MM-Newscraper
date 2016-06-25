﻿' ################################################################################
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

Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports Vlc.DotNet.Forms


Public Class frmVideoPlayer

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Dim PlayList As List(Of Uri)

#End Region

#Region "Events"

#End Region

#Region "Constructors"

#End Region

#Region "Methods"

    Public Sub SetUp()
        PlayList = New List(Of Uri)
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        ' Me.myVlcControl.VlcLibDirectory = New DirectoryInfo(clsAdvancedSettings.GetSetting("VLCPath", String.Empty, "generic.EmberCore.VLCPlayer"))
        InitializeComponent()
        Me.SetUp()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal URL As String)

        ' This call is required by the designer.
        'Me.myVlcControl.VlcLibDirectory = New DirectoryInfo(clsAdvancedSettings.GetSetting("VLCPath", String.Empty, "generic.EmberCore.VLCPlayer"))
        InitializeComponent()
        Me.SetUp()
        PlaylistAdd(URL)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub PlayerPlay()
        For Each aPath In PlayList
            'Me.myVlcControl.Play(aPath)
        Next
    End Sub

    Public Sub PlayerStop()
        'Me.myVlcControl.Stop()
    End Sub

    Public Sub PlaylistAdd(ByVal URL As String)
        If Not String.IsNullOrEmpty(URL) Then
            If Regex.IsMatch(URL, "http:\/\/.*?") Then
                PlayList.Add(New Uri(URL))
            Else
                PlayList.Add(New Uri(String.Concat("file:///", URL)))
            End If
        End If
    End Sub

    Public Sub PlaylistClear()
        PlayList.Clear()
    End Sub

    Private Sub OnVlcControlNeedLibDirectory(sender As Object, e As Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs)
        Dim aP As String

        If Environment.Is64BitOperatingSystem Then
            aP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "VideoLAN\VLC")
        Else
            aP = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "VideoLAN\VLC")
        End If

        If Not File.Exists(Path.Combine(aP, "libvlc.dll")) Then
            Using fbdDialog As New FolderBrowserDialog()
                fbdDialog.Description = Master.eLang.GetString(1477, "Select VLC Path")
                fbdDialog.SelectedPath = AdvancedSettings.GetSetting("VLCPath", String.Empty, "generic.EmberCore.VLCPlayer")

                If fbdDialog.ShowDialog() = DialogResult.OK Then
                    e.VlcLibDirectory = New DirectoryInfo(fbdDialog.SelectedPath)
                End If
            End Using
        Else
            e.VlcLibDirectory = New DirectoryInfo(aP)
        End If
    End Sub

#End Region

End Class
