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
            cbVideoCodec.Items.AddRange((From vCo In MediaFlags.AudioVideoFlags Where vCo.Type = MediaFlags.Flag.FlagType.VideoCodec AndAlso Not vCo.Name = "defaultscreen" Select vCo.Name).ToArray)
            cbVideoLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
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
            cbAudioCodec.Items.AddRange((From aCo In MediaFlags.AudioVideoFlags Where aCo.Type = MediaFlags.Flag.FlagType.AudioCodec AndAlso Not aCo.Name = "defaultaudio" Select aCo.Name).ToArray)
            cbAudioLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
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

            cbSubtitleLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
            If Not movie Is Nothing Then
                cbSubtitleLanguage.Text = movie.StreamDetails.Subtitle(idx).LongLanguage
                chkSubtitleForced.Checked = movie.StreamDetails.Subtitle(idx).Forced
                txtSubtitlePath.Text = movie.StreamDetails.Subtitle(idx).Path
                txtSubtitleType.Text = movie.StreamDetails.Subtitle(idx).SubsType
            End If
        End If

        If ShowDialog() = Windows.Forms.DialogResult.OK Then
            If stream_type = Master.eLang.GetString(595, "Video Streams") Then
                stream_v.Codec = If(cbVideoCodec.SelectedItem Is Nothing, String.Empty, cbVideoCodec.SelectedItem.ToString)
                stream_v.Aspect = ConvertToDouble(txtVideoAspect.Text)
                stream_v.Width = ConvertToInteger(txtVideoWidth.Text)
                stream_v.Height = ConvertToInteger(txtVideoHeight.Text)
                'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                stream_v.Scantype = If(rbVideoProgressive.Checked, "Progressive", "Interlaced")
                stream_v.Duration = ConvertToInteger(txtVideoDuration.Text)
                stream_v.Bitrate = ConvertToInteger(txtVideoBitrate.Text)
                stream_v.MultiViewCount = ConvertToInteger(txtVideoMultiViewCount.Text)
                stream_v.MultiViewLayout = cbVideoMultiViewLayout.Text
                stream_v.StereoMode = txtVideoStereoMode.Text
                If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.LongLanguage = cbVideoLanguage.SelectedItem.ToString
                If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.Language = Localization.ISOLangGetCode3ByLang(cbVideoLanguage.SelectedItem.ToString)
                Return stream_v
            End If
            If stream_type = Master.eLang.GetString(596, "Audio Streams") Then
                stream_a.Codec = If(cbAudioCodec.SelectedItem Is Nothing, String.Empty, cbAudioCodec.SelectedItem.ToString)
                If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.LongLanguage = cbAudioLanguage.SelectedItem.ToString
                If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.Language = Localization.ISOLangGetCode3ByLang(cbAudioLanguage.SelectedItem.ToString)
                stream_a.Channels = If(cbAudioChannels.SelectedItem Is Nothing, 0, ConvertToInteger(cbAudioChannels.SelectedItem.ToString))
                stream_a.Bitrate = ConvertToInteger(txtAudioBitrate.Text)
                Return stream_a
            End If
            If stream_type = Master.eLang.GetString(597, "Subtitle Streams") Then
                If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.LongLanguage = If(cbSubtitleLanguage.SelectedItem Is Nothing, "", cbSubtitleLanguage.SelectedItem.ToString)
                If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.Language = Localization.ISOLangGetCode3ByLang(cbSubtitleLanguage.SelectedItem.ToString)
                stream_s.Forced = chkSubtitleForced.Checked
                stream_s.Path = txtSubtitlePath.Text
                Return stream_s
            End If
            Return Nothing
        Else
            Return Nothing
        End If
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub cbAudioCodec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAudioCodec.SelectedIndexChanged
        'If cbAudioCodec.SelectedIndex >= 0 Then
        'Dim xAChanFlag = From xAChan In XML.FlagsXML...<achan>...<name> Where Regex.IsMatch(cbAudioCodec.SelectedItem, Regex.Match(xAChan.@searchstring, "\{atype=([^\}]+)\}").Groups(1).Value.ToString) Select xAChan.@searchstring
        'End If
    End Sub

    Private Sub cbVideoMultiViewLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVideoMultiViewLayout.SelectedIndexChanged
        If Not cbVideoMultiViewLayout.Text = String.Empty Then
            txtVideoMultiViewCount.Text = "2"
            txtVideoStereoMode.Text = MediaInfo.ConvertVideoMultiViewLayoutToStereoMode(cbVideoMultiViewLayout.Text)
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


    Private Sub dlgFIStreamEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub SetUp()
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        Text = Master.eLang.GetString(613, "Stream Editor")
        chkSubtitleForced.Text = Master.eLang.GetString(1287, "Forced")
        gbAudioStreams.Text = Master.eLang.GetString(596, "Audio Streams")
        gbSubtitleStreams.Text = Master.eLang.GetString(597, "Subtitle  Streams")
        gbVideoStreams.Text = Master.eLang.GetString(595, "Video Streams")
        lblAudioChannels.Text = Master.eLang.GetString(611, "Channels")
        lblAudioCodec.Text = lblVideoCodec.Text
        lblAudioLanguage.Text = Master.eLang.GetString(610, "Language")
        lblSubtitleLanguage.Text = lblAudioLanguage.Text
        lblVideoAspect.Text = Master.eLang.GetString(614, "Aspect Ratio")
        lblVideoCodec.Text = Master.eLang.GetString(604, "Codec")
        lblVideoDuration.Text = Master.eLang.GetString(609, "Duration")
        lblVideoHeight.Text = Master.eLang.GetString(607, "Height")
        lblVideoLanguage.Text = lblAudioLanguage.Text
        lblVideoMultiViewCount.Text = Master.eLang.GetString(1156, "MultiView Count")
        lblVideoMultiViewLayout.Text = Master.eLang.GetString(1157, "MultiView Layout")
        lblVideoStereoMode.Text = Master.eLang.GetString(1286, "StereoMode")
        lblVideoWidth.Text = Master.eLang.GetString(606, "Width")
        rbVideoInterlaced.Text = Master.eLang.GetString(615, "Interlaced")
        rbVideoProgressive.Text = Master.eLang.GetString(616, "Progressive")
    End Sub

#End Region 'Methods

End Class