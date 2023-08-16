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

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Properties"

    Public ReadOnly Property Result As MediaContainers.Fileinfo

#End Region 'Properties

#Region "Dialog"

    Public Sub New(ByVal FileInfo As MediaContainers.Fileinfo)
        ' This call is required by the designer.
        InitializeComponent()
        'no "ResizeAndMoveDialog" here, dialog can be loaded into another dialog

        Result = CType(FileInfo.CloneDeep, MediaContainers.Fileinfo)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Setup()
        ListView_Load()
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub Setup()
        btnOK.Text = Master.eLang.CommonWordsList.OK
        btnCancel.Text = Master.eLang.CommonWordsList.Cancel
        cbStreamType.Items.Clear()
        cbStreamType.Items.Add(Master.eLang.GetString(595, "Video Streams"))
        cbStreamType.Items.Add(Master.eLang.GetString(596, "Audio Streams"))
        cbStreamType.Items.Add(Master.eLang.GetString(597, "Subtitle Streams"))
        lblStreamType.Text = Master.eLang.GetString(598, "Stream Type")
        Text = Master.eLang.GetString(594, "Meta Data Editor")
    End Sub

    Public Overloads Function ShowDialog() As DialogResult
        'only if the dialog has used a "standalone" dialog
        FormsUtils.ResizeAndMoveDialog(Me, Me)
        btnCancel.Visible = True
        btnOK.Visible = True
        ssBottom.Visible = True
        Return MyBase.ShowDialog
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub ListView_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvStreams.DoubleClick
        If lvStreams.SelectedItems.Count > 0 Then
            If Not lvStreams.SelectedItems.Item(0).Tag.ToString = "header" Then
                Stream_Edit()
            End If
        End If
    End Sub

    Private Sub ListView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lvStreams.KeyDown
        If e.KeyCode = Keys.Delete Then Stream_Remove()
    End Sub

    Private Sub ListView_Load()
        Dim c As Integer
        Dim g As New ListViewGroup
        Dim i As New ListViewItem
        lvStreams.Groups.Clear()
        lvStreams.Items.Clear()
        If Result.StreamDetails.Video.Count > 0 Then
            g = New ListViewGroup With {
                .Header = Master.eLang.GetString(595, "Video Streams")
            }
            lvStreams.Groups.Add(g)
            c = 1
            ' Fake Group Header
            i = New ListViewItem With {
                .ForeColor = Color.DarkBlue,
                .Tag = "header",
                .Text = String.Empty
            }
            i.SubItems.Add(Master.eLang.GetString(604, "Codec"))
            i.SubItems.Add(Master.eLang.GetString(605, "Scan Type"))
            i.SubItems.Add(Master.eLang.GetString(606, "Width"))
            i.SubItems.Add(Master.eLang.GetString(607, "Height"))
            i.SubItems.Add(Master.eLang.GetString(608, "Aspect"))
            i.SubItems.Add(Master.eLang.GetString(609, "Duration"))
            i.SubItems.Add(Master.eLang.GetString(610, "Language"))
            i.SubItems.Add(Master.eLang.GetString(1158, "Bitrate"))
            i.SubItems.Add(Master.eLang.GetString(1156, "MultiView Count"))
            i.SubItems.Add(Master.eLang.GetString(1157, "MultiView Layout"))
            i.SubItems.Add(Master.eLang.GetString(1286, "StereoMode"))
            g.Items.Add(i)
            lvStreams.Items.Add(i)

            Dim v As MediaContainers.Video
            For c = 0 To Result.StreamDetails.Video.Count - 1
                v = Result.StreamDetails.Video(c)
                If Not v Is Nothing Then
                    i = New ListViewItem With {
                        .Tag = Master.eLang.GetString(595, "Video Streams"),
                        .Text = c.ToString
                    }
                    i.SubItems.Add(v.Codec)
                    i.SubItems.Add(v.Scantype)
                    i.SubItems.Add(v.Width.ToString)
                    i.SubItems.Add(v.Height.ToString)
                    i.SubItems.Add(v.Aspect.ToString)
                    i.SubItems.Add(v.Duration.ToString)
                    i.SubItems.Add(v.LongLanguage)
                    i.SubItems.Add(v.Bitrate.ToString)
                    i.SubItems.Add(v.MultiViewCount.ToString)
                    i.SubItems.Add(v.MultiViewLayout)
                    i.SubItems.Add(v.StereoMode)
                    g.Items.Add(i)
                    lvStreams.Items.Add(i)
                End If
            Next
        End If
        If Result.StreamDetails.Audio.Count > 0 Then
            g = New ListViewGroup With {
                .Header = Master.eLang.GetString(596, "Audio Streams")
            }
            lvStreams.Groups.Add(g)
            c = 1
            ' Fake Group Header
            i = New ListViewItem With {
                .ForeColor = Color.DarkBlue,
                .Tag = "header",
                .Text = String.Empty
            }
            i.SubItems.Add(Master.eLang.GetString(604, "Codec"))
            i.SubItems.Add(Master.eLang.GetString(610, "Language"))
            i.SubItems.Add(Master.eLang.GetString(611, "Channels"))
            i.SubItems.Add(Master.eLang.GetString(1158, "Bitrate"))

            g.Items.Add(i)
            lvStreams.Items.Add(i)
            Dim a As MediaContainers.Audio
            For c = 0 To Result.StreamDetails.Audio.Count - 1
                a = Result.StreamDetails.Audio(c)
                If Not a Is Nothing Then
                    i = New ListViewItem With {
                        .Tag = Master.eLang.GetString(596, "Audio Streams"),
                        .Text = c.ToString
                    }
                    i.SubItems.Add(a.Codec)
                    i.SubItems.Add(a.LongLanguage)
                    i.SubItems.Add(a.Channels.ToString)
                    i.SubItems.Add(a.Bitrate.ToString)

                    g.Items.Add(i)
                    lvStreams.Items.Add(i)
                End If
            Next
        End If
        If Result.StreamDetails.Subtitle.Count > 0 Then
            g = New ListViewGroup With {
                .Header = Master.eLang.GetString(597, "Subtitle Streams")
            }
            lvStreams.Groups.Add(g)
            c = 1
            ' Fake Group Header
            i = New ListViewItem With {
                .ForeColor = Color.DarkBlue,
                .Tag = "header",
                .Text = String.Empty
            }
            i.SubItems.Add(Master.eLang.GetString(610, "Language"))
            i.SubItems.Add(Master.eLang.GetString(1288, "Type"))
            i.SubItems.Add(Master.eLang.GetString(1287, "Forced"))

            g.Items.Add(i)
            lvStreams.Items.Add(i)
            Dim s As MediaContainers.Subtitle
            For c = 0 To Result.StreamDetails.Subtitle.Count - 1
                s = Result.StreamDetails.Subtitle(c)
                If Not s Is Nothing Then
                    i = New ListViewItem With {
                        .Tag = Master.eLang.GetString(597, "Subtitle Streams"),
                        .Text = c.ToString
                    }
                    i.SubItems.Add(s.LongLanguage)
                    i.SubItems.Add(s.Type)
                    i.SubItems.Add(If(s.Forced, Master.eLang.GetString(300, "Yes"), Master.eLang.GetString(720, "No")))

                    g.Items.Add(i)
                    lvStreams.Items.Add(i)
                End If
            Next
        End If
    End Sub

    Private Sub ListView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lvStreams.SelectedIndexChanged
        If lvStreams.SelectedItems.Count > 0 Then
            If lvStreams.SelectedItems.Item(0).Tag.ToString = "header" Then
                lvStreams.SelectedItems.Clear()
                btnStreamNew.Enabled = True
                btnStreamEdit.Enabled = False
                btnStreamRemove.Enabled = False
            Else
                btnStreamNew.Enabled = False
                btnStreamEdit.Enabled = True
                btnStreamRemove.Enabled = True
                cbStreamType.SelectedIndex = -1
            End If
        Else
            btnStreamNew.Enabled = True
            btnStreamEdit.Enabled = False
            btnStreamRemove.Enabled = False
        End If
    End Sub

    Private Sub Stream_Remove()
        If lvStreams.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvStreams.SelectedItems(0)
            If i.Tag.ToString = Master.eLang.GetString(595, "Video Streams") Then
                Result.StreamDetails.Video.RemoveAt(Convert.ToInt16(i.Text))
            End If
            If i.Tag.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                Result.StreamDetails.Audio.RemoveAt(Convert.ToInt16(i.Text))
            End If
            If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                Result.StreamDetails.Subtitle.RemoveAt(Convert.ToInt16(i.Text))
            End If
            ListView_Load()
        End If
    End Sub

    Private Sub Stream_Remove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnStreamRemove.Click
        Stream_Remove()
    End Sub

    Private Sub Stream_Edit()
        If lvStreams.SelectedItems.Count > 0 Then
            Dim i As ListViewItem = lvStreams.SelectedItems(0)
            Using dEditStream As New dlgFIStreamEditor
                Dim stream As Object = dEditStream.ShowDialog(i.Tag.ToString, Result, Convert.ToInt16(i.Text))
                If Not stream Is Nothing Then
                    If i.Tag.ToString = Master.eLang.GetString(595, "Video Streams") Then
                        Result.StreamDetails.Video(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Video)
                    End If
                    If i.Tag.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                        Result.StreamDetails.Audio(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Audio)
                    End If
                    If i.Tag.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                        Result.StreamDetails.Subtitle(Convert.ToInt16(i.Text)) = DirectCast(stream, MediaContainers.Subtitle)
                    End If
                    ListView_Load()
                End If
            End Using
        End If
    End Sub

    Private Sub Stream_Edit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnStreamEdit.Click
        Stream_Edit()
    End Sub

    Private Sub Stream_New__Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnStreamNew.Click
        If cbStreamType.SelectedIndex >= 0 Then
            Using dEditStream As New dlgFIStreamEditor
                Dim stream As New Object
                stream = dEditStream.ShowDialog(cbStreamType.SelectedItem.ToString, Nothing, 0)
                If Not stream Is Nothing Then
                    If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(595, "Video Streams") Then
                        Result.StreamDetails.Video.Add(DirectCast(stream, MediaContainers.Video))
                    End If
                    If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(596, "Audio Streams") Then
                        Result.StreamDetails.Audio.Add(DirectCast(stream, MediaContainers.Audio))
                    End If
                    If cbStreamType.SelectedItem.ToString = Master.eLang.GetString(597, "Subtitle Streams") Then
                        Result.StreamDetails.Subtitle.Add(DirectCast(stream, MediaContainers.Subtitle))
                    End If
                    ListView_Load()
                End If
            End Using
        End If
    End Sub

    Private Sub Stream_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbStreamType.SelectedIndexChanged
        If cbStreamType.SelectedIndex <> -1 Then
            btnStreamNew.Enabled = True
            btnStreamEdit.Enabled = False
            btnStreamRemove.Enabled = False
            lvStreams.SelectedItems.Clear()
        End If
    End Sub

#End Region 'Methods

End Class