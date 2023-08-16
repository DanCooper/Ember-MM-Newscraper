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

Public Class dlgTVChangeEp

#Region "Fields"

    Private _selectedEpisode As MediaContainers.MainDetails
    Private _result As New List(Of MediaContainers.MainDetails)
    Private _DBShow As Database.DBElement

#End Region 'Fields

#Region "Properties"

    Public Property Result As List(Of MediaContainers.MainDetails)
        Get
            Return _result
        End Get
        Set(value As List(Of MediaContainers.MainDetails))
            _result = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New(ByRef DBShow As Database.DBElement)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
        _DBShow = DBShow
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        If lvEpisodes.SelectedItems.Count > 0 Then
            For i As Integer = 0 To lvEpisodes.SelectedItems.Count - 1
                _result.Add(DirectCast(lvEpisodes.SelectedItems(i).Tag, MediaContainers.MainDetails))
            Next
        End If

        DialogResult = DialogResult.OK
    End Sub

    Private Sub ClearInfo()
        Me.pbPreview.Image = Nothing
        lblTitle.Text = String.Empty
        txtPlot.Text = String.Empty
    End Sub

    Private Sub dlgTVChangeEp_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()

        Dim lGroup As New ListViewGroup
        Dim lItem As New ListViewItem
        Dim tSeason As Integer = Nothing

        For Each Season As Integer In _DBShow.Episodes.GroupBy(Function(s) s.MainDetails.Season).Select(Function(group) group.Key)
            tSeason = Season
            lGroup = New ListViewGroup
            lGroup.Header = String.Format(Master.eLang.GetString(726, "Season {0}"), tSeason)
            lvEpisodes.Groups.Add(lGroup)
            For Each DBEpisode As Database.DBElement In _DBShow.Episodes.Where(Function(s) s.MainDetails.Season = tSeason).OrderBy(Function(s) s.MainDetails.Episode)
                lItem = lvEpisodes.Items.Add(DBEpisode.MainDetails.Episode.ToString)
                lItem.Tag = DBEpisode.MainDetails
                lItem.SubItems.Add(DBEpisode.MainDetails.Title)
                lGroup.Items.Add(lItem)
            Next
        Next
    End Sub

    Private Sub lvEpisodes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvEpisodes.SelectedIndexChanged
        ClearInfo()
        If lvEpisodes.SelectedItems.Count > 0 AndAlso lvEpisodes.SelectedItems(0).Tag IsNot Nothing Then
            _selectedEpisode = DirectCast(lvEpisodes.SelectedItems(0).Tag, MediaContainers.MainDetails)

            _selectedEpisode.ThumbPoster.LoadAndCache(Enums.ContentType.TV, False, True)

            If _selectedEpisode.ThumbPoster.ImageThumb.Image IsNot Nothing Then
                pbPreview.Image = _selectedEpisode.ThumbPoster.ImageThumb.Image
            ElseIf _selectedEpisode.ThumbPoster.ImageOriginal.Image IsNot Nothing Then
                pbPreview.Image = _selectedEpisode.ThumbPoster.ImageOriginal.Image
            End If

            lblTitle.Text = _selectedEpisode.Title
            txtPlot.Text = _selectedEpisode.Plot
        End If
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(772, "Change Episode")

        lvEpisodes.Columns(0).Text = Master.eLang.GetString(727, "Episode")
        lvEpisodes.Columns(1).Text = Master.eLang.GetString(21, "Title")

        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

#End Region 'Methods

End Class