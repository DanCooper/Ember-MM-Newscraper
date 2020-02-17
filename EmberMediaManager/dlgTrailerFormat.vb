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

Public Class dlgTrailerFormat

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Trailer As MediaContainers.Trailer

#End Region 'Fields

#Region "Properties"

    Public Property Result As New TrailerLinksContainer

#End Region

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()

        lbVideoFormats.DataSource = _Trailer.Streams.VideoStreams.ToList
        lbVideoFormats.DisplayMember = "Description"
        lbVideoFormats.ValueMember = "URL"

        If _Trailer.Streams.AudioStreams.Count > 0 Then
            lbAudioFormats.DataSource = _Trailer.Streams.AudioStreams.ToList
            lbAudioFormats.DisplayMember = "Description"
            lbAudioFormats.ValueMember = "URL"
        End If

        Dim prevQualLink = _Trailer.Streams.VideoStreams.Find(Function(f) f.FormatQuality = Master.eSettings.MovieTrailerPrefVideoQual)
        If prevQualLink IsNot Nothing Then
            lbVideoFormats.SelectedItem = prevQualLink
        ElseIf lbVideoFormats.Items.Count = 1 Then
            lbVideoFormats.SelectedIndex = 0
        End If
    End Sub

    Private Sub Dialog_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Activate()
    End Sub

    Public Overloads Function ShowDialog(ByVal trailer As MediaContainers.Trailer) As DialogResult
        _Trailer = trailer
        Return ShowDialog()
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub lbAudioFormats_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbAudioFormats.SelectedIndexChanged
        If Not lbAudioFormats.SelectedIndex = -1 Then
            Result.AudioURL = DirectCast(lbAudioFormats.SelectedItem, MediaContainers.Trailer.AudioStream).URL
        End If

        If Result.isDash AndAlso lbVideoFormats.SelectedItems.Count > 0 AndAlso lbAudioFormats.SelectedItems.Count > 0 Then
            OK_Button.Enabled = True
        ElseIf Not Result.isDash Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub lbVideoFormats_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbVideoFormats.SelectedIndexChanged
        Result.VideoURL = DirectCast(lbVideoFormats.SelectedItem, MediaContainers.Trailer.VideoStream).URL
        If Not DirectCast(lbVideoFormats.SelectedItem, MediaContainers.Trailer.VideoStream).IsDash Then
            Result.AudioURL = String.Empty
            lbAudioFormats.Enabled = False
            lbAudioFormats.SelectedIndex = -1
        End If

        If lbVideoFormats.SelectedItems.Count > 0 Then
            If DirectCast(lbVideoFormats.SelectedItem, MediaContainers.Trailer.VideoStream).IsDash Then
                lbAudioFormats.Enabled = True
                OK_Button.Enabled = lbAudioFormats.SelectedItems.Count > 0
            Else
                OK_Button.Enabled = True
            End If
        Else
            OK_Button.Enabled = False
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OK_Button.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        Text = Master.eLang.GetString(923, "Select Format")
        gbAudioFormats.Text = Master.eLang.GetString(1333, "Available Audio Formats")
        gbVideoFormats.Text = Master.eLang.GetString(925, "Available Video Formats")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

#End Region 'Methods

End Class