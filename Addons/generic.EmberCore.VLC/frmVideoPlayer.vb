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


Public Class frmVideoPlayer

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

#End Region

#Region "Events"

#End Region

#Region "Constructors"

#End Region

#Region "Methods"

    Public Sub SetUp()

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.SetUp()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal URL As String)

        ' This call is required by the designer.
        InitializeComponent()
        Me.SetUp()
        PlaylistAdd(URL)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub PlayerPlay()
        Me.AxVLCPlayer.playlist.play()
    End Sub

    Public Sub PlayerStop()
        Me.AxVLCPlayer.playlist.stop()
    End Sub

    Public Sub PlaylistAdd(ByVal URL As String)
        If Not String.IsNullOrEmpty(URL) Then
            Me.AxVLCPlayer.playlist.items.clear()
            If Regex.IsMatch(URL, "http:\/\/.*?") Then
                Me.AxVLCPlayer.playlist.add(URL)
            Else
                Me.AxVLCPlayer.playlist.add(String.Concat("file:///", URL))
            End If
        End If
    End Sub

    Public Sub PlaylistClear()
        Me.AxVLCPlayer.playlist.items.clear()
    End Sub

#End Region

End Class
