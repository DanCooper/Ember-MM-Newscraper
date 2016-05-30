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
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal stream_type As String, ByVal movie As MediaContainers.Fileinfo, ByVal idx As Integer) As Object
        Try

            gbVideoStreams.Visible = False
            gbAudioStreams.Visible = False
            gbAudioStreams.Visible = False

            If stream_type = Master.eLang.GetString(595, "Video Streams") Then
                gbVideoStreams.Visible = True
                cbVideoCodec.Items.AddRange((From vCo In APIXML.lFlags Where vCo.Type = APIXML.FlagType.VideoCodec AndAlso Not vCo.Name = "defaultscreen" Select vCo.Name).ToArray)
                cbVideoLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
                If Not movie Is Nothing Then
                    cbVideoCodec.Text = movie.StreamDetails.Video(idx).Codec
                    txtVideoAspect.Text = movie.StreamDetails.Video(idx).Aspect
                    txtVideoWidth.Text = movie.StreamDetails.Video(idx).Width
                    txtVideoHeight.Text = movie.StreamDetails.Video(idx).Height
                    'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                    'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                    If movie.StreamDetails.Video(idx).Scantype = "Progressive" OrElse movie.StreamDetails.Video(idx).Scantype = Master.eLang.GetString(616, "Progressive") Then
                        rbVideoProgressive.Checked = True
                    Else
                        rbVideoInterlaced.Checked = True
                    End If
                    txtVideoDuration.Text = movie.StreamDetails.Video(idx).Duration
                    cbVideoLanguage.Text = movie.StreamDetails.Video(idx).LongLanguage
                    txtVideoMultiViewCount.Text = movie.StreamDetails.Video(idx).MultiViewCount
                    cbVideoMultiViewLayout.Text = movie.StreamDetails.Video(idx).MultiViewLayout
                    txtVideoBitrate.Text = movie.StreamDetails.Video(idx).Bitrate
                    'for now return filesize in mbytes instead of bytes(default)
                    txtVideoFileSize.Text = CStr(NumUtils.ConvertBytesTo(CLng(movie.StreamDetails.Video(idx).Filesize), NumUtils.FileSizeUnit.Megabyte, 0))
                    txtVideoStereoMode.Text = movie.StreamDetails.Video(idx).StereoMode
                End If
            End If
            If stream_type = Master.eLang.GetString(596, "Audio Streams") Then
                gbAudioStreams.Visible = True
                cbAudioCodec.Items.AddRange((From aCo In APIXML.lFlags Where aCo.Type = APIXML.FlagType.AudioCodec AndAlso Not aCo.Name = "defaultaudio" Select aCo.Name).ToArray)
                cbAudioLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
                cbAudioChannels.Items.AddRange(New String() {"8", "7", "6", "2", "1"})
                If Not movie Is Nothing Then
                    cbAudioCodec.Text = movie.StreamDetails.Audio(idx).Codec
                    cbAudioLanguage.Text = movie.StreamDetails.Audio(idx).LongLanguage
                    cbAudioChannels.Text = movie.StreamDetails.Audio(idx).Channels
                    txtAudioBitrate.Text = movie.StreamDetails.Audio(idx).Bitrate
                End If
            End If
            If stream_type = Master.eLang.GetString(597, "Subtitle Streams") Then
                gbSubtitleStreams.Visible = True

                cbSubtitleLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
                If Not movie Is Nothing Then
                    cbSubtitleLanguage.Text = movie.StreamDetails.Subtitle(idx).LongLanguage
                    chkSubtitleForced.Checked = movie.StreamDetails.Subtitle(idx).SubsForced
                    txtSubtitlePath.Text = movie.StreamDetails.Subtitle(idx).SubsPath
                    txtSubtitleType.Text = movie.StreamDetails.Subtitle(idx).SubsType
                End If
            End If

            If ShowDialog() = Windows.Forms.DialogResult.OK Then
                If stream_type = Master.eLang.GetString(595, "Video Streams") Then
                    stream_v.Codec = If(cbVideoCodec.SelectedItem Is Nothing, "", cbVideoCodec.SelectedItem.ToString)
                    stream_v.Aspect = txtVideoAspect.Text
                    stream_v.Width = txtVideoWidth.Text
                    stream_v.Height = txtVideoHeight.Text
                    'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                    'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                    stream_v.Scantype = If(rbVideoProgressive.Checked, "Progressive", "Interlaced")
                    stream_v.Duration = txtVideoDuration.Text
                    stream_v.Bitrate = txtVideoBitrate.Text
                    stream_v.MultiViewCount = txtVideoMultiViewCount.Text
                    stream_v.MultiViewLayout = cbVideoMultiViewLayout.Text
                    stream_v.StereoMode = txtVideoStereoMode.Text
                    'since textfield is unit MB we need to convert to Bytes
                    stream_v.Filesize = If(Double.TryParse(txtVideoFileSize.Text, 0), CDbl(txtVideoFileSize.Text) * 1024 * 1024, 0)
                    If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.LongLanguage = cbVideoLanguage.SelectedItem.ToString
                    If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.Language = Localization.ISOLangGetCode3ByLang(cbVideoLanguage.SelectedItem.ToString)
                    Return stream_v
                End If
                If stream_type = Master.eLang.GetString(596, "Audio Streams") Then
                    stream_a.Codec = If(cbAudioCodec.SelectedItem Is Nothing, "", cbAudioCodec.SelectedItem.ToString)
                    If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.LongLanguage = cbAudioLanguage.SelectedItem.ToString
                    If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.Language = Localization.ISOLangGetCode3ByLang(cbAudioLanguage.SelectedItem.ToString)
                    stream_a.Channels = cbAudioChannels.SelectedItem.ToString
                    stream_a.Bitrate = txtAudioBitrate.Text
                    Return stream_a
                End If
                If stream_type = Master.eLang.GetString(597, "Subtitle Streams") Then
                    If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.LongLanguage = If(cbSubtitleLanguage.SelectedItem Is Nothing, "", cbSubtitleLanguage.SelectedItem.ToString)
                    If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.Language = Localization.ISOLangGetCode3ByLang(cbSubtitleLanguage.SelectedItem.ToString)
                    stream_s.SubsType = If(txtSubtitleType.Text.ToLower = "external" OrElse Not String.IsNullOrEmpty(stream_s.SubsPath), "External", "Embedded")
                    stream_s.SubsForced = chkSubtitleForced.Checked
                    stream_s.SubsPath = txtSubtitlePath.Text
                    Return stream_s
                End If
                Return Nothing
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub

    Private Sub cbAudioCodec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAudioCodec.SelectedIndexChanged
        'If cbAudioCodec.SelectedIndex >= 0 Then
        'Dim xAChanFlag = From xAChan In XML.FlagsXML...<achan>...<name> Where Regex.IsMatch(cbAudioCodec.SelectedItem, Regex.Match(xAChan.@searchstring, "\{atype=([^\}]+)\}").Groups(1).Value.ToString) Select xAChan.@searchstring
        'End If
    End Sub
    Private Sub cbVideoMultiViewLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVideoMultiViewLayout.SelectedIndexChanged
        If Not cbVideoMultiViewLayout.Text = String.Empty Then
            txtVideoMultiViewCount.Text = "2"
            txtVideoStereoMode.Text = MediaInfo.ConvertVStereoMode(cbVideoMultiViewLayout.Text)
        End If
    End Sub

    Private Sub dlgFIStreamEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub SetUp()
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
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
        lblVideoFileSize.Text = Master.eLang.GetString(1455, "Filesize [MB]")
    End Sub

#End Region 'Methods

End Class