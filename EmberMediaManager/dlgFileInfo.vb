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

Public Class dlgFileInfo

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private NeedToRefresh As Boolean = False
    Private SettingDefaults As Boolean = False
    Private _FileInfo As MediaContainers.Fileinfo
    Private _isEpisode As Boolean = False
    Private _DBElement As Database.DBElement

#End Region 'Fields

#Region "Methods"

    Public Sub New(ByVal DBElement As Database.DBElement, ByVal isEpisode As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        'Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        'Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        'Me.StartPosition = FormStartPosition.Manual
        _DBElement = DBElement
        _isEpisode = isEpisode
    End Sub

    Public Overloads Function ShowDialog(ByVal fi As MediaContainers.Fileinfo, ByVal isEpisode As Boolean) As MediaContainers.Fileinfo
        SettingDefaults = True
        _FileInfo = fi
        _isEpisode = isEpisode
        If ShowDialog() = DialogResult.OK Then
            Return _FileInfo
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnEditSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSet.Click
        EditStream()
    End Sub

    Private Sub btnNewSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSet.Click
        Try
            If cbStreamType.SelectedIndex >= 0 Then
                Using dEditStream As New dlgFIStreamEditor
                    Dim stream As New Object
                    stream = dEditStream.ShowDialog(cbStreamType.SelectedItem.ToString, Nothing, 0)
                    If Not stream Is Nothing Then
                        If Not SettingDefaults Then
                            If _isEpisode Then
                                _DBElement.TVEpisode.FileInfo = _FileInfo
                            Else
                                _DBElement.Movie.FileInfo = _FileInfo
                            End If
                        End If
                        If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(595, "Video Streams") Then
                            _FileInfo.StreamDetails.Video.Add(DirectCast(stream, MediaContainers.Video))
                        End If
                        If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                            _FileInfo.StreamDetails.Audio.Add(DirectCast(stream, MediaContainers.Audio))
                        End If
                        If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                            _FileInfo.StreamDetails.Subtitle.Add(DirectCast(stream, MediaContainers.Subtitle))
                        End If
                        If btnClose.Visible = True AndAlso Not SettingDefaults Then 'Only Save imediatly when running stand alone
                            If _isEpisode Then
                                Master.DB.Save_TVEpisode(_DBElement, False, True, False, False, True)
                            Else
                                Master.DB.Save_Movie(_DBElement, False, True, False, True, False)
                            End If
                        End If
                        NeedToRefresh = True
                        LoadInfo()
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub btnRemoveSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSet.Click
        DeleteStream()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If NeedToRefresh Then
            DialogResult = DialogResult.OK
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub cbStreamType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStreamType.SelectedIndexChanged
        If cbStreamType.SelectedIndex <> -1 Then
            btnNewSet.Enabled = True
            btnEditSet.Enabled = False
            btnRemoveSet.Enabled = False
            lvStreams.SelectedItems.Clear()
        End If
    End Sub

    Private Sub DeleteStream()
        Try
            If lvStreams.SelectedItems.Count > 0 Then
                Dim i As ListViewItem = lvStreams.SelectedItems(0)
                If Not SettingDefaults Then
                    If _isEpisode Then
                        _DBElement.TVEpisode.FileInfo = _FileInfo
                    Else
                        _DBElement.Movie.FileInfo = _FileInfo
                    End If
                End If
                If i.Tag.ToString = Master.eLang.GetString(595, "Video Streams") Then
                    _FileInfo.StreamDetails.Video.RemoveAt(Convert.ToInt16(i.Text))
                End If
                If i.Tag.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                    _FileInfo.StreamDetails.Audio.RemoveAt(Convert.ToInt16(i.Text))
                End If
                If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                    _FileInfo.StreamDetails.Subtitle.RemoveAt(Convert.ToInt16(i.Text))
                End If
                If btnClose.Visible = True AndAlso Not SettingDefaults Then 'Only Save imediatly when running stand alone
                    If _isEpisode Then
                        Master.DB.Save_TVEpisode(_DBElement, False, True, False, False, True)
                    Else
                        Master.DB.Save_Movie(_DBElement, False, True, False, True, False)
                    End If
                End If
                NeedToRefresh = True
                LoadInfo()
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub dlgFileInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
        If Not SettingDefaults Then
            _FileInfo = If(_isEpisode, _DBElement.TVEpisode.FileInfo, _DBElement.Movie.FileInfo)
        End If
        LoadInfo()
    End Sub

    Private Sub dlgFileInfo_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub EditStream()
        Try
            If lvStreams.SelectedItems.Count > 0 Then
                Dim i As ListViewItem = lvStreams.SelectedItems(0)
                Using dEditStream As New dlgFIStreamEditor
                    Dim stream As Object = dEditStream.ShowDialog(i.Tag.ToString, _FileInfo, Convert.ToInt16(i.Text))
                    If Not stream Is Nothing Then
                        If Not SettingDefaults Then
                            If _isEpisode Then
                                _DBElement.TVEpisode.FileInfo = _FileInfo
                            Else
                                _DBElement.Movie.FileInfo = _FileInfo
                            End If
                        End If
                        If i.Tag.ToString = Master.eLang.GetString(595, "Video Streams") Then
                            _FileInfo.StreamDetails.Video(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Video)
                        End If
                        If i.Tag.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                            _FileInfo.StreamDetails.Audio(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Audio)
                        End If
                        If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                            _FileInfo.StreamDetails.Subtitle(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Subtitle)
                        End If
                        If btnClose.Visible = True AndAlso Not SettingDefaults Then 'Only Save imediatly when running stand alone
                            If _isEpisode Then
                                Master.DB.Save_TVEpisode(_DBElement, False, True, False, False, True)
                            Else
                                Master.DB.Save_Movie(_DBElement, False, True, False, True, False)
                            End If
                        End If
                        NeedToRefresh = True
                        LoadInfo()
                    End If
                End Using
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub LoadInfo()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvStreams.Groups.Clear()
        lvStreams.Items.Clear()
        Try
            If _FileInfo.StreamDetails.Video.Count > 0 Then
                g = New ListViewGroup
                g.Header = Master.eLang.GetString(595, "Video Streams")
                lvStreams.Groups.Add(g)
                c = 1
                ' Fake Group Header
                i = New ListViewItem
                'i.UseItemStyleForSubItems = False
                i.ForeColor = Color.DarkBlue
                i.Tag = "Header"
                i.Text = String.Empty
                i.SubItems.Add(Master.eLang.GetString(604, "Codec"))
                i.SubItems.Add(Master.eLang.GetString(605, "Scan Type"))
                i.SubItems.Add(Master.eLang.GetString(606, "Width"))
                i.SubItems.Add(Master.eLang.GetString(607, "Height"))
                i.SubItems.Add(Master.eLang.GetString(608, "Aspect"))
                i.SubItems.Add(Master.eLang.GetString(609, "Duration"))
                i.SubItems.Add(Master.eLang.GetString(1455, "Filesize [MB]"))
                i.SubItems.Add(Master.eLang.GetString(610, "Language"))
                i.SubItems.Add(Master.eLang.GetString(1158, "Bitrate"))
                i.SubItems.Add(Master.eLang.GetString(1156, "MultiView Count"))
                i.SubItems.Add(Master.eLang.GetString(1157, "MultiView Layout"))
                i.SubItems.Add(Master.eLang.GetString(1286, "StereoMode"))
                g.Items.Add(i)
                lvStreams.Items.Add(i)

                Dim v As MediaContainers.Video
                For c = 0 To _FileInfo.StreamDetails.Video.Count - 1
                    v = _FileInfo.StreamDetails.Video(c)
                    If Not v Is Nothing Then
                        i = New ListViewItem
                        i.Tag = Master.eLang.GetString(595, "Video Streams")
                        i.Text = c.ToString
                        i.SubItems.Add(v.Codec)
                        i.SubItems.Add(v.Scantype)
                        i.SubItems.Add(v.Width)
                        i.SubItems.Add(v.Height)
                        i.SubItems.Add(v.Aspect)
                        i.SubItems.Add(v.Duration)
                        i.SubItems.Add(CStr(NumUtils.ConvertBytesTo(CLng(v.Filesize), NumUtils.FileSizeUnit.Megabyte, 0)))
                        i.SubItems.Add(v.LongLanguage)
                        i.SubItems.Add(v.Bitrate)
                        i.SubItems.Add(v.MultiViewCount)
                        i.SubItems.Add(v.MultiViewLayout)
                        i.SubItems.Add(v.StereoMode)
                        g.Items.Add(i)
                        lvStreams.Items.Add(i)
                    End If
                Next
            End If
            If _FileInfo.StreamDetails.Audio.Count > 0 Then
                g = New ListViewGroup
                g.Header = Master.eLang.GetString(596, "Audio Streams")
                lvStreams.Groups.Add(g)
                c = 1
                ' Fake Group Header
                i = New ListViewItem
                'i.UseItemStyleForSubItems = False
                i.ForeColor = Color.DarkBlue
                i.Tag = "Header"
                i.Text = String.Empty
                i.SubItems.Add(Master.eLang.GetString(604, "Codec"))
                i.SubItems.Add(Master.eLang.GetString(610, "Language"))
                i.SubItems.Add(Master.eLang.GetString(611, "Channels"))
                i.SubItems.Add(Master.eLang.GetString(1158, "Bitrate"))

                g.Items.Add(i)
                lvStreams.Items.Add(i)
                Dim a As MediaContainers.Audio
                For c = 0 To _FileInfo.StreamDetails.Audio.Count - 1
                    a = _FileInfo.StreamDetails.Audio(c)
                    If Not a Is Nothing Then
                        i = New ListViewItem
                        i.Tag = Master.eLang.GetString(596, "Audio Streams")
                        i.Text = c.ToString
                        i.SubItems.Add(a.Codec)
                        i.SubItems.Add(a.LongLanguage)
                        i.SubItems.Add(a.Channels)
                        i.SubItems.Add(a.Bitrate)

                        g.Items.Add(i)
                        lvStreams.Items.Add(i)
                    End If
                Next
            End If
            If _FileInfo.StreamDetails.Subtitle.Count > 0 Then
                g = New ListViewGroup
                g.Header = Master.eLang.GetString(597, "Subtitle Streams")
                lvStreams.Groups.Add(g)
                c = 1
                ' Fake Group Header
                i = New ListViewItem
                'i.UseItemStyleForSubItems = False
                i.ForeColor = Color.DarkBlue
                i.Tag = "Header"
                i.Text = String.Empty
                i.SubItems.Add(Master.eLang.GetString(610, "Language"))
                i.SubItems.Add(Master.eLang.GetString(1288, "Type"))
                i.SubItems.Add(Master.eLang.GetString(1287, "Forced"))

                g.Items.Add(i)
                lvStreams.Items.Add(i)
                Dim s As MediaContainers.Subtitle
                For c = 0 To _FileInfo.StreamDetails.Subtitle.Count - 1
                    s = _FileInfo.StreamDetails.Subtitle(c)
                    If Not s Is Nothing Then
                        i = New ListViewItem
                        i.Tag = Master.eLang.GetString(597, "Subtitle Streams")
                        i.Text = c.ToString
                        i.SubItems.Add(s.LongLanguage)
                        i.SubItems.Add(s.SubsType)
                        i.SubItems.Add(If(s.SubsForced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                        g.Items.Add(i)
                        lvStreams.Items.Add(i)
                    End If
                Next
            End If


        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub lvStreams_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvStreams.DoubleClick
        If lvStreams.SelectedItems.Count > 0 Then
            If lvStreams.SelectedItems.Item(0).Tag.ToString <> "Header" Then
                EditStream()
            End If
        End If
    End Sub

    Private Sub lvStreams_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvStreams.KeyDown
        If e.KeyCode = Keys.Delete Then DeleteStream()
    End Sub

    Private Sub lvStreams_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStreams.SelectedIndexChanged
        If lvStreams.SelectedItems.Count > 0 Then
            If lvStreams.SelectedItems.Item(0).Tag.ToString = "Header" Then
                lvStreams.SelectedItems.Clear()
                btnNewSet.Enabled = True
                btnEditSet.Enabled = False
                btnRemoveSet.Enabled = False
            Else
                btnNewSet.Enabled = False
                btnEditSet.Enabled = True
                btnRemoveSet.Enabled = True
                cbStreamType.SelectedIndex = -1
            End If

        Else
            btnNewSet.Enabled = True
            btnEditSet.Enabled = False
            btnRemoveSet.Enabled = False
        End If
    End Sub

    Private Sub SetUp()
        cbStreamType.Items.Clear()
        cbStreamType.Items.Add(Master.eLang.GetString(595, "Video Streams"))
        cbStreamType.Items.Add(Master.eLang.GetString(596, "Audio Streams"))
        cbStreamType.Items.Add(Master.eLang.GetString(597, "Subtitle Streams"))
        Text = Master.eLang.GetString(594, "Meta Data Editor")
        lblStreamType.Text = Master.eLang.GetString(598, "Stream Type")
        btnClose.Text = Master.eLang.GetString(19, "Close")
    End Sub

#End Region 'Methods

End Class