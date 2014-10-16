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

Imports System.Text.RegularExpressions
Imports EmberAPI

Public Class dlgFIStreamEditor

#Region "Fields"

    Private stream_a As New MediaInfo.Audio
    Private stream_s As New MediaInfo.Subtitle
    Private stream_v As New MediaInfo.Video

#End Region 'Fields

#Region "Methods"

    Public Overloads Function ShowDialog(ByVal stream_type As String, ByVal movie As MediaInfo.Fileinfo, ByVal idx As Integer) As Object
        Try

            gbVideoStreams.Visible = False
            gbAudioStreams.Visible = False
            gbAudioStreams.Visible = False

            If stream_type = Master.eLang.GetString(595, "Video Stream") Then
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
                        'old
                        'If movie.StreamDetails.Video(idx).Scantype = Master.eLang.GetString(616, "Progressive") Then
                        rbVideoProgressive.Checked = True
                    Else
                        rbVideoInterlaced.Checked = True
                    End If
                    txtVideoDuration.Text = movie.StreamDetails.Video(idx).Duration
                    cbVideoLanguage.Text = movie.StreamDetails.Video(idx).LongLanguage
                    txtVideoMultiViewCount.Text = movie.StreamDetails.Video(idx).MultiViewCount
                    cbVideoMultiViewLayout.Text = movie.StreamDetails.Video(idx).MultiViewLayout
                    txtVideoBitrate.Text = movie.StreamDetails.Video(idx).Bitrate
                    txtEncodingSettings.Text = movie.StreamDetails.Video(idx).EncodedSettings
                    txtVideoStereoMode.Text = movie.StreamDetails.Video(idx).StereoMode
                End If
            End If
            If stream_type = Master.eLang.GetString(596, "Audio Stream") Then
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
            If stream_type = Master.eLang.GetString(597, "Subtitle Stream") Then
                gbSubtitleStreams.Visible = True

                cbSubtitleLanguage.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
                If Not movie Is Nothing Then
                    cbSubtitleLanguage.Text = movie.StreamDetails.Subtitle(idx).LongLanguage
                End If

            End If

            If MyBase.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If stream_type = Master.eLang.GetString(595, "Video Stream") Then
                    stream_v.Codec = If(cbVideoCodec.SelectedItem Is Nothing, "", cbVideoCodec.SelectedItem.ToString)
                    stream_v.Aspect = txtVideoAspect.Text
                    stream_v.Width = txtVideoWidth.Text
                    stream_v.Height = txtVideoHeight.Text
                    'cocotus, 2013/09 Fix for Progressive setting in metadata - don't use language specific name!!
                    'see thread: http://forum.xbmc.org/showthread.php?tid=172326
                    stream_v.Scantype = If(rbVideoProgressive.Checked, "Progressive", "Interlaced")
                    'old
                    'stream_v.Scantype = If(rbProgressive.Checked, Master.eLang.GetString(616, "Progressive"), Master.eLang.GetString(615, "Interlaced"))
                    stream_v.Duration = txtVideoDuration.Text
                    stream_v.Bitrate = txtVideoBitrate.Text
                    stream_v.MultiViewCount = txtVideoMultiViewCount.Text
                    stream_v.MultiViewLayout = cbVideoMultiViewLayout.Text
                    stream_v.StereoMode = txtVideoStereoMode.Text

                    If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.LongLanguage = cbVideoLanguage.SelectedItem.ToString
                    If Not cbVideoLanguage.SelectedItem Is Nothing Then stream_v.Language = Localization.ISOLangGetCode3ByLang(cbVideoLanguage.SelectedItem.ToString)
                    Return stream_v
                End If
                If stream_type = Master.eLang.GetString(596, "Audio Stream") Then
                    stream_a.Codec = If(cbAudioCodec.SelectedItem Is Nothing, "", cbAudioCodec.SelectedItem.ToString)
                    If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.LongLanguage = cbAudioLanguage.SelectedItem.ToString
                    If Not cbAudioLanguage.SelectedItem Is Nothing Then stream_a.Language = Localization.ISOLangGetCode3ByLang(cbAudioLanguage.SelectedItem.ToString)
                    If Not cbAudioChannels.SelectedItem Is Nothing Then stream_a.Channels = cbAudioChannels.SelectedItem.ToString
                    stream_a.Bitrate = txtAudioBitrate.Text
                    Return stream_a
                End If
                If stream_type = Master.eLang.GetString(597, "Subtitle Stream") Then
                    If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.LongLanguage = If(cbSubtitleLanguage.SelectedItem Is Nothing, "", cbSubtitleLanguage.SelectedItem.ToString)
                    If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.Language = Localization.ISOLangGetCode3ByLang(cbSubtitleLanguage.SelectedItem.ToString)
                    If Not cbSubtitleLanguage.SelectedItem Is Nothing Then stream_s.SubsType = "Embedded"
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
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cbAudioCodec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAudioCodec.SelectedIndexChanged
        'If cbAudioCodec.SelectedIndex >= 0 Then
        'Dim xAChanFlag = From xAChan In XML.FlagsXML...<achan>...<name> Where Regex.IsMatch(cbAudioCodec.SelectedItem, Regex.Match(xAChan.@searchstring, "\{atype=([^\}]+)\}").Groups(1).Value.ToString) Select xAChan.@searchstring
        'End If
    End Sub
    Private Sub cbVideoMultiViewLayout_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVideoMultiViewLayout.SelectedIndexChanged
        If Not Me.cbVideoMultiViewLayout.Text = String.Empty Then
            Me.txtVideoMultiViewCount.Text = "2"
            Me.txtVideoStereoMode.Text = MediaInfo.ConvertVStereoMode(cbVideoMultiViewLayout.Text)
        End If
    End Sub

    Private Sub dlgFIStreamEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SetUp()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetUp()
        Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
        Me.Text = Master.eLang.GetString(613, "Stream Editor")
        Me.gbAudioStreams.Text = Master.eLang.GetString(596, "Audio Streams")
        Me.gbSubtitleStreams.Text = Master.eLang.GetString(597, "Subtitle  Streams")
        Me.gbVideoStreams.Text = Master.eLang.GetString(595, "Video Streams")
        Me.lblAudioChannels.Text = Master.eLang.GetString(611, "Channels")
        Me.lblAudioCodec.Text = Me.lblVideoCodec.Text
        Me.lblAudioLanguage.Text = Master.eLang.GetString(610, "Language")
        Me.lblSubtitleLanguage.Text = Me.lblAudioLanguage.Text
        Me.lblVideoAspect.Text = Master.eLang.GetString(614, "Aspect Ratio")
        Me.lblVideoCodec.Text = Master.eLang.GetString(604, "Codec")
        Me.lblVideoDuration.Text = Master.eLang.GetString(609, "Duration")
        Me.lblVideoHeight.Text = Master.eLang.GetString(607, "Height")
        Me.lblVideoLanguage.Text = Me.lblAudioLanguage.Text
        Me.lblVideoMultiViewCount.Text = Master.eLang.GetString(1156, "MultiView Count")
        Me.lblVideoMultiViewLayout.Text = Master.eLang.GetString(1157, "MultiView Layout")
        Me.lblVideoStereoMode.Text = Master.eLang.GetString(1286, "StereoMode")
        Me.lblVideoWidth.Text = Master.eLang.GetString(606, "Width")
        Me.rbVideoInterlaced.Text = Master.eLang.GetString(615, "Interlaced")
        Me.rbVideoProgressive.Text = Master.eLang.GetString(616, "Progressive")
    End Sub

#End Region 'Methods

End Class