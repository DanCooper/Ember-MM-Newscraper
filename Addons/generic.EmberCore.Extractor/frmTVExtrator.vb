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
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog

Public Class frmTVExtrator

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private PreviousFrameValue As Integer

#End Region 'Fields

#Region "Events"

    Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object))

#End Region 'Events

#Region "Methods"

    Private Sub frmTVExtrator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUp()
    End Sub

    Private Sub tbFrame_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tbFrame.KeyUp
        If tbFrame.Value <> PreviousFrameValue Then
            GrabTheFrame()
        End If
    End Sub

    Private Sub tbFrame_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tbFrame.MouseUp
        If tbFrame.Value <> PreviousFrameValue Then
            GrabTheFrame()
        End If
    End Sub

    Private Sub tbFrame_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbFrame.Scroll
        Try
            Dim sec2Time As New TimeSpan(0, 0, tbFrame.Value)
            lblTime.Text = String.Format("{0}:{1:00}:{2:00}", sec2Time.Hours, sec2Time.Minutes, sec2Time.Seconds)

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnFrameSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameSave.Click
        If pbFrame.Image IsNot Nothing Then
            RaiseEvent GenericEvent(Enums.ModuleEventType.FrameExtrator_TVEpisode, Nothing)
        End If
    End Sub

    Private Sub btnFrameLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameLoad.Click
        Try
            Using ffmpeg As New Process()

                ffmpeg.StartInfo.FileName = FrameExtrator.GetFFMpeg
                ffmpeg.StartInfo.Arguments = String.Format("-ss 0 -i ""{0}"" -an -f rawvideo -vframes 1 -s 1280x720 -vcodec mjpeg -y ""{1}""", Master.currShow.Filename, Path.Combine(Master.TempPath, "frame.jpg"))
                ffmpeg.EnableRaisingEvents = False
                ffmpeg.StartInfo.UseShellExecute = False
                ffmpeg.StartInfo.CreateNoWindow = True
                ffmpeg.StartInfo.RedirectStandardOutput = True
                ffmpeg.StartInfo.RedirectStandardError = True
                ffmpeg.Start()
                Using d As StreamReader = ffmpeg.StandardError

                    Do
                        Dim s As String = d.ReadLine()
                        If s.Contains("Duration: ") Then
                            Dim sTime As String = Regex.Match(s, "Duration: (?<dur>.*?),").Groups("dur").ToString
                            If Not sTime = "N/A" Then
                                Dim ts As TimeSpan = CDate(CDate(String.Format("{0} {1}", DateTime.Today.ToString("d"), sTime))).Subtract(CDate(DateTime.Today))
                                Dim intSeconds As Integer = ((ts.Hours * 60) + ts.Minutes) * 60 + ts.Seconds
                                tbFrame.Maximum = intSeconds
                            Else
                                tbFrame.Maximum = 0
                            End If
                            tbFrame.Value = 0
                            tbFrame.Enabled = True
                        End If
                    Loop While Not d.EndOfStream
                End Using
                ffmpeg.WaitForExit()
                ffmpeg.Close()
            End Using

            If tbFrame.Maximum > 0 AndAlso File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                Using fsFImage As New FileStream(Path.Combine(Master.TempPath, "frame.jpg"), FileMode.Open, FileAccess.Read)
                    pbFrame.Image = Image.FromStream(fsFImage)
                End Using
                btnFrameLoad.Enabled = False
                btnFrameSave.Enabled = True
            Else
                tbFrame.Maximum = 0
                tbFrame.Value = 0
                tbFrame.Enabled = False
                pbFrame.Image = Nothing
            End If
            PreviousFrameValue = 0

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            pbFrame.Image = Nothing
        End Try
    End Sub

    Private Sub GrabTheFrame()
        Try

            tbFrame.Enabled = False
            Dim ffmpeg As New Process()

            ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
            'ffmpeg.StartInfo.Arguments = String.Format("-ss {0} -i ""{1}"" -an -f rawvideo -vframes 1 -vcodec mjpeg -y ""{2}""", tbFrame.Value, Master.currShow.Filename, Path.Combine(Master.TempPath, "frame.jpg"))
            ffmpeg.StartInfo.Arguments = String.Format("-ss {0} -i ""{1}"" -vframes 1 -y ""{2}""", tbFrame.Value, Master.currShow.Filename, Path.Combine(Master.TempPath, "frame.jpg"))
            ffmpeg.EnableRaisingEvents = False
            ffmpeg.StartInfo.UseShellExecute = False
            ffmpeg.StartInfo.CreateNoWindow = True
            ffmpeg.StartInfo.RedirectStandardOutput = False
            ffmpeg.StartInfo.RedirectStandardError = False

            pnlFrameProgress.Visible = True
            btnFrameSave.Enabled = False

            ffmpeg.Start()

            ffmpeg.WaitForExit()
            ffmpeg.Close()

            If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                Using fsFImage As FileStream = New FileStream(Path.Combine(Master.TempPath, "frame.jpg"), FileMode.Open, FileAccess.Read)
                    pbFrame.Image = Image.FromStream(fsFImage)
                End Using
                tbFrame.Enabled = True
                btnFrameSave.Enabled = True
                pnlFrameProgress.Visible = False
                PreviousFrameValue = tbFrame.Value
            Else
                lblTime.Text = String.Empty
                tbFrame.Maximum = 0
                tbFrame.Value = 0
                tbFrame.Enabled = False
                btnFrameSave.Enabled = False
                btnFrameLoad.Enabled = True
                pbFrame.Image = Nothing
                pnlFrameProgress.Visible = False
                PreviousFrameValue = tbFrame.Value
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            PreviousFrameValue = 0
            lblTime.Text = String.Empty
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            btnFrameSave.Enabled = False
            btnFrameLoad.Enabled = True
            pbFrame.Image = Nothing
        End Try
    End Sub

    Sub SetUp()
        Me.lblExtractingFrame.Text = Master.eLang.GetString(306, "Extracting Frame...")
        Me.btnFrameLoad.Text = Master.eLang.GetString(308, "Load Episode")
        Me.btnFrameSave.Text = Master.eLang.GetString(309, "Save as Poster")
    End Sub

#End Region 'Methods

End Class
