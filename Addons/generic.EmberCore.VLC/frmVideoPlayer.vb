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

Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports Vlc.DotNet


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
        Dim aVlcControl As Forms.VlcControl
        Dim aPath As String

        ' This call is required by the designer.
        InitializeComponent()
        Me.SetUp()
        ' Add any initialization after the InitializeComponent() call.
        If Environment.Is64BitOperatingSystem Then
            If Environment.Is64BitProcess Then
                aPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "VideoLAN\VLC")
            Else
                aPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "VideoLAN\VLC")
            End If
        Else
            aPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "VideoLAN\VLC")
        End If
        If Directory.Exists(aPath) Then
            aVlcControl = New Forms.VlcControl
            aVlcControl.BeginInit()
            aVlcControl.Name = "VlcControl"
            aVlcControl.VlcLibDirectory = New DirectoryInfo(aPath)
            aVlcControl.Parent = pnlPlayer
            aVlcControl.Dock = DockStyle.Fill
            AddHandler aVlcControl.VlcLibDirectoryNeeded, AddressOf checkVLCDir

            pnlPlayer.Controls.Add(aVlcControl)

            aVlcControl.EndInit()
        Else

        End If
    End Sub

    Public Sub New(aFile As String)
        MyBase.New()
        PlaylistAdd(aFile)
    End Sub
    Public Sub PlayerPlay()
        Dim aVlcControl As Vlc.DotNet.Forms.VlcControl
        aVlcControl = CType(pnlPlayer.Controls.Find("VlcControl", True)(0), Vlc.DotNet.Forms.VlcControl)
        For Each aPath In PlayList
            aVlcControl.Play(aPath)
        Next
    End Sub

    Public Sub PlayerStop()
        Dim aVlcControl As Vlc.DotNet.Forms.VlcControl
        aVlcControl = CType(pnlPlayer.Controls.Find("VlcControl", True)(0), Vlc.DotNet.Forms.VlcControl)
        aVlcControl.Stop()
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

    Private Sub checkVLCDir(sender As Object, e As Forms.VlcLibDirectoryNeededEventArgs)
        Dim aPath As String
        Dim aTitle As String

        If Environment.Is64BitOperatingSystem Then
            If Environment.Is64BitProcess Then
                aPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                aTitle = Master.eLang.GetString(1477, "Select VLC x64 bit Path")
            Else
                aPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                aTitle = Master.eLang.GetString(1477, "Select VLC x86 bit Path")
            End If
        Else
            aPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            aTitle = Master.eLang.GetString(1477, "Select VLC x86 bit Path")
        End If
        If Not File.Exists(Path.Combine(Path.Combine(aPath, "VideoLAN\VLC"), "libvlc.dll")) Then
            Using fbdDialog As New FolderBrowserDialog()
                fbdDialog.Description = aTitle
                fbdDialog.SelectedPath = aPath

                If fbdDialog.ShowDialog() = DialogResult.OK Then
                    e.VlcLibDirectory = New DirectoryInfo(fbdDialog.SelectedPath)
                End If
            End Using
        Else
            e.VlcLibDirectory = New DirectoryInfo(Path.Combine(aPath, "VideoLAN\VLC"))
        End If
        e.VlcLibDirectory = Nothing
    End Sub

#End Region

End Class
