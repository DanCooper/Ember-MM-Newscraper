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
Imports System.Net

Public Class dlgThemeSelect

#Region "Fields"

    Friend WithEvents bwDownloadTheme As New System.ComponentModel.BackgroundWorker

    Private tMovie As New Structures.DBMovie
    Private tShow As New Structures.DBTV
    Private _UrlList As List(Of Theme)
    Private tURL As String = String.Empty
    Private sPath As String

#End Region 'Fields

#Region "Methods"

    Private Sub dlgThemeSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetUp()
    End Sub

    Private Sub dlgThemeSelect_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Activate()
    End Sub

    Private Sub CreateTable(ByVal tURLList As List(Of Theme))
        'set ListView
        Me.lvThemes.MultiSelect = False
        Me.lvThemes.FullRowSelect = True
        Me.lvThemes.HideSelection = False
        Me.lvThemes.Columns.Add("#", -1, HorizontalAlignment.Right)
        Me.lvThemes.Columns.Add("URL", 0, HorizontalAlignment.Left)
        Me.lvThemes.Columns.Add("Title", -2, HorizontalAlignment.Left)
        Me.lvThemes.Columns.Add(Master.eLang.GetString(979, "Description"), -2, HorizontalAlignment.Left)
        Me.lvThemes.Columns.Add("Length", -2, HorizontalAlignment.Left)
        Me.lvThemes.Columns.Add("Bitrate", -2, HorizontalAlignment.Left)

        'Me.txtYouTubeSearch.Text = DBMovie.Movie.Title & " Trailer"

        Me._UrlList = tURLList
        Dim ID As Integer = 1
        Dim str(6) As String
        For Each aUrl In _UrlList
            Dim itm As ListViewItem
            str(0) = ID.ToString
            str(1) = aUrl.URL.ToString
            str(2) = aUrl.Title.ToString
            str(3) = aUrl.Description.ToString
            str(4) = aUrl.Length.ToString
            str(5) = aUrl.Bitrate.ToString
            itm = New ListViewItem(str)
            lvThemes.Items.Add(itm)
            ID = ID + 1
        Next
        'Me.pnlStatus.Visible = False
        Me.lvThemes.Enabled = True
        'Me.txtYouTube.Enabled = True
        'Me.txtManual.Enabled = True
        'Me.btnBrowse.Enabled = True
        'Me.SetEnabled(False)
        If _UrlList.Count = 1 Then
            Me.lvThemes.Select()
            Me.lvThemes.Items(0).Selected = True
        End If
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByRef tURLList As List(Of Theme)) As String
        CreateTable(tURLList)

        If MyBase.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return Me.tURL
        Else
            Return String.Empty
        End If
    End Function

    Public Overloads Function ShowDialog(ByRef DBTV As Structures.DBTV, ByRef tURLList As List(Of Theme)) As String
        CreateTable(tURLList)

        If MyBase.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return Me.tURL
        Else
            Return String.Empty
        End If
    End Function

    Private Sub lvThemes_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvThemes.DoubleClick
        Dim tURL As String = Me.lvThemes.SelectedItems(0).SubItems(1).Text.ToString

        If tURL.Contains("goear") Then
            Dim request As HttpWebRequest = CType(HttpWebRequest.Create("http://www.goear.com/listen/2b6ac87/avatar-avatar"), HttpWebRequest)
        End If
        Me.vlcPlayer.playlist.items.clear()
        Me.vlcPlayer.playlist.add(tURL)
        Me.vlcPlayer.playlist.play()
    End Sub
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Me.tURL = Me.lvThemes.SelectedItems(0).SubItems(1).Text.ToString
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(1069, "Select Theme")
    End Sub

#End Region

End Class