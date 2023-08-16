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

Public Class dlgFIStreamEditor

#Region "Fields"

    Private stream_a As New MediaContainers.Audio
    Private stream_s As New MediaContainers.Subtitle
    Private stream_v As New MediaContainers.Video

#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Public Overloads Function ShowDialog(ByVal stream_type As String, ByVal movie As MediaContainers.Fileinfo, ByVal idx As Integer) As Object
        gbVideoStreams.Visible = False
        gbAudioStreams.Visible = False
        gbAudioStreams.Visible = False

        If stream_type = Master.eLang.GetString(595, "Video Streams") Then
            gbVideoStreams.Visible = True
            cbVideoCodec.Items.AddRange((From vCo In APIXML.Flags Where vCo.Type = APIXML.FlagType.VideoCodec AndAlso Not vCo.Name = "defaultscreen" Select vCo.Name).ToArray)
            cbVideoLanguage.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
            If Not movie Is Nothing Then
                cbVideoCodec.Text = movie.StreamDetails.Video(idx).Codec
                txtVideoAspect.Text = movie.StreamDetails.Video(idx).Aspect.ToString
                txtVideoWidth.Text = movie.StreamDetails.Video(idx).Width.ToString
                txtVideoHeight.Text = movie.StreamDetails.Video(idx).Height.ToString
                'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                If movie.StreamDetails.Video(idx).Scantype = "Progressive" OrElse movie.StreamDetails.Video(idx).Scantype = Master.eLang.GetString(616, "Progressive") Then
                    rbVideoProgressive.Checked = True
                Else
                    rbVideoInterlaced.Checked = True
                End If
                txtVideoDuration.Text = movie.StreamDetails.Video(idx).Duration.ToString
                cbVideoLanguage.Text = movie.StreamDetails.Video(idx).LongLanguage
                txtVideoMultiViewCount.Text = movie.StreamDetails.Video(idx).MultiViewCount.ToString
                cbVideoMultiViewLayout.Text = movie.StreamDetails.Video(idx).MultiViewLayout
                txtVideoBitrate.Text = movie.StreamDetails.Video(idx).Bitrate.ToString
                txtVideoStereoMode.Text = movie.StreamDetails.Video(idx).StereoMode
            End If
        End If
        If stream_type = Master.eLang.GetString(596, "Audio Streams") Then
            gbAudioStreams.Visible = True
            cbAudioCodec.Items.AddRange((From aCo In APIXML.Flags Where aCo.Type = APIXML.FlagType.AudioCodec AndAlso Not aCo.Name = "defaultaudio" Select aCo.Name).ToArray)
            cbAudioLanguage.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
            cbAudioChannels.Items.AddRange(New String() {"8", "7", "6", "2", "1"})
            If Not movie Is Nothing Then
                cbAudioCodec.Text = movie.StreamDetails.Audio(idx).Codec
                cbAudioLanguage.Text = movie.StreamDetails.Audio(idx).LongLanguage
                cbAudioChannels.Text = movie.StreamDetails.Audio(idx).Channels.ToString
                txtAudioBitrate.Text = movie.StreamDetails.Audio(idx).Bitrate.ToString
            End If
        End If
        If stream_type = Master.eLang.GetString(597, "Subtitle Streams") Then
            gbSubtitleStreams.Visible = True

            cbSubtitleLanguage.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
            If Not movie Is Nothing Then
                cbSubtitleLanguage.Text = movie.StreamDetails.Subtitle(idx).LongLanguage
                chkSubtitleForced.Checked = movie.StreamDetails.Subtitle(idx).Forced
                txtSubtitlePath.Text = movie.StreamDetails.Subtitle(idx).Path
                txtSubtitleType.Text = movie.StreamDetails.Subtitle(idx).Type
            End If
        End If

        If ShowDialog() = Windows.Forms.DialogResult.OK Then
            If stream_type = Master.eLang.GetString(595, "Video Streams") Then
                stream_v.Codec = If(cbVideoCodec.SelectedItem Is Nothing, String.Empty, cbVideoCodec.SelectedItem.ToString)
                stream_v.Aspect = CDbl(txtVideoAspect.Text.Trim)
                stream_v.Width = CInt(txtVideoWidth.Text.Trim)
                stream_v.Height = CInt(txtVideoHeight.Text.Trim)
                'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                stream_v.Scantype = If(rbVideoProgressive.Checked, "Progressive", "Interlaced")
                stream_v.Duration = CInt(txtVideoDuration.Text.Trim)
                stream_v.Bitrate = CInt(txtVideoBitrate.Text.Trim)
                stream_v.MultiViewCount = CInt(txtVideoMultiViewCount.Text.Trim)
                stream_v.MultiViewLayout = cbVideoMultiViewLayout.Text
                stream_v.StereoMode = txtVideoStereoMode.Text
                If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.LongLanguage = cbVideoLanguage.SelectedItem.ToString
                If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.Language = Localization.Languages.Get_Alpha3_T_By_Name(cbVideoLanguage.SelectedItem.ToString)
                Return stream_v
            End If
            If stream_type = Master.eLang.GetString(596, "Audio Streams") Then
                stream_a.Codec = If(cbAudioCodec.SelectedItem Is Nothing, String.Empty, cbAudioCodec.SelectedItem.ToString)
                If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.LongLanguage = cbAudioLanguage.SelectedItem.ToString
                If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.Language = Localization.Languages.Get_Alpha3_T_By_Name(cbAudioLanguage.SelectedItem.ToString)
                stream_a.Channels = If(cbAudioChannels.SelectedItem Is Nothing, 0, CInt(cbAudioChannels.SelectedItem.ToString.Trim))
                stream_a.Bitrate = CInt(txtAudioBitrate.Text.Trim)
                Return stream_a
            End If
            If stream_type = Master.eLang.GetString(597, "Subtitle Streams") Then
                If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.LongLanguage = If(cbSubtitleLanguage.SelectedItem Is Nothing, String.Empty, cbSubtitleLanguage.SelectedItem.ToString)
                If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.Language = Localization.Languages.Get_Alpha3_T_By_Name(cbSubtitleLanguage.SelectedItem.ToString)
                stream_s.Forced = chkSubtitleForced.Checked
                stream_s.Path = txtSubtitlePath.Text
                Return stream_s
            End If
            Return Nothing
        Else
            Return Nothing
        End If
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub cbAudioCodec_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbAudioCodec.SelectedIndexChanged
        'If cbAudioCodec.SelectedIndex >= 0 Then
        'Dim xAChanFlag = From xAChan In XML.FlagsXML...<achan>...<name> Where Regex.IsMatch(cbAudioCodec.SelectedItem, Regex.Match(xAChan.@searchstring, "\{atype=([^\}]+)\}").Groups(1).Value.ToString) Select xAChan.@searchstring
        'End If
    End Sub

    Private Sub cbVideoMultiViewLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVideoMultiViewLayout.SelectedIndexChanged
        If Not cbVideoMultiViewLayout.Text = String.Empty Then
            txtVideoMultiViewCount.Text = "2"
            txtVideoStereoMode.Text = MediaInfo.Convert_VideoMultiViewLayout_To_StereoMode(cbVideoMultiViewLayout.Text)
        Else
            txtVideoStereoMode.Text = String.Empty
        End If
    End Sub

    Private Function ConvertToDouble(ByVal value As String) As Double
        Dim dblValue As Double
        Double.TryParse(value, dblValue)
        Return dblValue
    End Function

    Private Function ConvertToInteger(ByVal value As String) As Integer
        Dim iValue As Integer
        Integer.TryParse(value, iValue)
        Return iValue
    End Function


    Private Sub dlgFIStreamEditor_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SetUp()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        With Master.eLang
            btnCancel.Text = .CommonWordsList.Cancel
            btnOK.Text = .CommonWordsList.OK
            Text = .GetString(613, "Stream Editor")
            chkSubtitleForced.Text = .GetString(1287, "Forced")
            gbAudioStreams.Text = .GetString(596, "Audio Streams")
            gbSubtitleStreams.Text = .GetString(597, "Subtitle  Streams")
            gbVideoStreams.Text = .GetString(595, "Video Streams")
            lblAudioChannels.Text = .GetString(611, "Channels")
            lblAudioCodec.Text = lblVideoCodec.Text
            lblAudioLanguage.Text = .GetString(610, "Language")
            lblSubtitleLanguage.Text = lblAudioLanguage.Text
            lblVideoAspect.Text = .GetString(614, "Aspect Ratio")
            lblVideoCodec.Text = .GetString(604, "Codec")
            lblVideoDuration.Text = .GetString(609, "Duration")
            lblVideoHeight.Text = .GetString(607, "Height")
            lblVideoLanguage.Text = lblAudioLanguage.Text
            lblVideoMultiViewCount.Text = .GetString(1156, "MultiView Count")
            lblVideoMultiViewLayout.Text = .GetString(1157, "MultiView Layout")
            lblVideoStereoMode.Text = .GetString(1286, "StereoMode")
            lblVideoWidth.Text = .GetString(606, "Width")
            rbVideoInterlaced.Text = .GetString(615, "Interlaced")
            rbVideoProgressive.Text = .GetString(616, "Progressive")
        End With
    End Sub

#End Region 'Methods

End Class