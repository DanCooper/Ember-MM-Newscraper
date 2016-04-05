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
Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog


Public Class frmMovieExtractor

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private PreviousFrameValue As Integer
    Private _strFilename As String

#End Region 'Fields

#Region "Events"

    Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object))

#End Region 'Events

#Region "Methods"

    Public Sub New(ByVal strFilename As String)
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
        _strFilename = strFilename
    End Sub

    Private Sub btnFrameLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameLoad.Click
        Try
            Using ffmpeg As New Process()

                ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
                ffmpeg.StartInfo.Arguments = String.Format("-ss 0 -i ""{0}"" -an -f rawvideo -vframes 1 -s 1280x720 -vcodec mjpeg -y ""{1}""", _strFilename, Path.Combine(Master.TempPath, "frame.jpg"))
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
                btnFrameSaveAsExtrafanart.Enabled = True
                btnFrameSaveAsExtrathumb.Enabled = True
                btnFrameSaveAsFanart.Enabled = True
            Else
                tbFrame.Maximum = 0
                tbFrame.Value = 0
                tbFrame.Enabled = False
                pbFrame.Image = Nothing
            End If
            PreviousFrameValue = 0

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            pbFrame.Image = Nothing
        End Try
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
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub GrabTheFrame()
        Try

            tbFrame.Enabled = False
            Dim ffmpeg As New Process()

            ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
            'ffmpeg.StartInfo.Arguments = String.Format("-ss {0} -i ""{1}"" -an -f rawvideo -vframes 1 -vcodec mjpeg -y -pix_fmt ""yuvj420p"" ""{2}""", tbFrame.Value, Master.currMovie.Filename, Path.Combine(Master.TempPath, "frame.jpg"))
            ffmpeg.StartInfo.Arguments = String.Format("-ss {0} -i ""{1}"" -vframes 1 -y ""{2}""", tbFrame.Value, _strFilename, Path.Combine(Master.TempPath, "frame.jpg"))
            ffmpeg.EnableRaisingEvents = False
            ffmpeg.StartInfo.UseShellExecute = False
            ffmpeg.StartInfo.CreateNoWindow = True
            ffmpeg.StartInfo.RedirectStandardOutput = False
            ffmpeg.StartInfo.RedirectStandardError = False

            pnlFrameProgress.Visible = True
            btnFrameSaveAsExtrafanart.Enabled = False
            btnFrameSaveAsExtrathumb.Enabled = False
            btnFrameSaveAsFanart.Enabled = False

            ffmpeg.Start()

            ffmpeg.WaitForExit()
            ffmpeg.Close()

            If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                Using fsFImage As FileStream = New FileStream(Path.Combine(Master.TempPath, "frame.jpg"), FileMode.Open, FileAccess.Read)
                    pbFrame.Image = Image.FromStream(fsFImage)
                End Using
                tbFrame.Enabled = True
                btnFrameSaveAsExtrafanart.Enabled = True
                btnFrameSaveAsExtrathumb.Enabled = True
                btnFrameSaveAsFanart.Enabled = True
                pnlFrameProgress.Visible = False
                PreviousFrameValue = tbFrame.Value
            Else
                lblTime.Text = String.Empty
                tbFrame.Maximum = 0
                tbFrame.Value = 0
                tbFrame.Enabled = False
                btnFrameSaveAsExtrafanart.Enabled = False
                btnFrameSaveAsExtrathumb.Enabled = False
                btnFrameSaveAsFanart.Enabled = False
                btnFrameLoad.Enabled = True
                pbFrame.Image = Nothing
                pnlFrameProgress.Visible = False
                PreviousFrameValue = tbFrame.Value
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            PreviousFrameValue = 0
            lblTime.Text = String.Empty
            tbFrame.Maximum = 0
            tbFrame.Value = 0
            tbFrame.Enabled = False
            btnFrameSaveAsExtrafanart.Enabled = False
            btnFrameSaveAsExtrathumb.Enabled = False
            btnFrameSaveAsFanart.Enabled = False
            btnFrameLoad.Enabled = True
            pbFrame.Image = Nothing
        End Try
    End Sub

    Private Sub btnFrameSaveAsExtrafanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameSaveAsExtrafanart.Click
        Try
            Dim tPath As String = Path.Combine(Master.TempPath, "extrafanarts")

            If Not Directory.Exists(tPath) Then
                Directory.CreateDirectory(tPath)
            End If

            Dim iMod As Integer = Functions.GetExtrathumbsModifier(tPath)

            Dim exImage As New Images
            Dim sPath As String = Path.Combine(tPath, String.Concat("thumb", (iMod + 1), ".jpg"))
            exImage.ResizeExtraFanart(Path.Combine(Master.TempPath, "frame.jpg"), sPath)
            exImage.Clear()

            RaiseEvent GenericEvent(Enums.ModuleEventType.FrameExtrator_Movie, New List(Of Object)(New Object() {"ExtrafanartToSave", sPath}))

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        btnFrameSaveAsExtrafanart.Enabled = False
    End Sub

    Private Sub btnFrameSaveAsExtrathumb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameSaveAsExtrathumb.Click
        Try
            Dim tPath As String = Path.Combine(Master.TempPath, "extrathumbs")

            If Not Directory.Exists(tPath) Then
                Directory.CreateDirectory(tPath)
            End If

            Dim iMod As Integer = Functions.GetExtrathumbsModifier(tPath)

            Dim exImage As New Images
            Dim sPath As String = Path.Combine(tPath, String.Concat("thumb", (iMod + 1), ".jpg"))
            exImage.ResizeExtraThumb(Path.Combine(Master.TempPath, "frame.jpg"), sPath)
            exImage.Clear()

            RaiseEvent GenericEvent(Enums.ModuleEventType.FrameExtrator_Movie, New List(Of Object)(New Object() {"ExtrathumbToSave", sPath}))

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        btnFrameSaveAsExtrathumb.Enabled = False
    End Sub

    Private Sub btnFrameSaveAsFanart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrameSaveAsFanart.Click
        Try
            If pbFrame.Image IsNot Nothing Then
                RaiseEvent GenericEvent(Enums.ModuleEventType.FrameExtrator_Movie, New List(Of Object)(New Object() {"FanartToSave"}))
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try

        btnFrameSaveAsFanart.Enabled = False
    End Sub

    Private Sub DelayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDelay.Tick
        tmrDelay.Stop()
        GrabTheFrame()
    End Sub

    Private Sub btnAutoGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoGen.Click
        Try
            'If Convert.ToInt32(txtThumbCount.Text) > 0 Then
            '    pnlFrameProgress.Visible = True
            '    Me.Refresh()
            '    ThumbGenerator.CreateRandomThumbs(Master.currMovie, Convert.ToInt32(txtThumbCount.Text), True)
            '    pnlFrameProgress.Visible = False
            '    RaiseEvent GenericEvent(Enums.ModuleEventType.MovieFrameExtrator, Nothing)
            'End If
            MessageBox.Show("This feature is currently unavailable", "No Beta Feature", MessageBoxButtons.OK, MessageBoxIcon.Information) 'TODO: re-add autothumbs
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub txtThumbCount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtThumbCount.GotFocus
        Me.AcceptButton = Me.btnAutoGen
    End Sub

    Private Sub txtThumbCount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThumbCount.TextChanged
        btnAutoGen.Enabled = Not String.IsNullOrEmpty(txtThumbCount.Text)
    End Sub

    Private Sub frmMovieExtrator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Master.eSettings.AutoThumbs > 0 Then
        '	txtThumbCount.Text = Master.eSettings.AutoThumbs.ToString
        'End If

    End Sub

    Public Sub SetUp()
        Me.gbAutoGenerate.Text = Master.eLang.GetString(296, "Auto-Generate")
        Me.lblToCreate.Text = Master.eLang.GetString(303, "# to Create:")
        Me.btnAutoGen.Text = Master.eLang.GetString(304, "Auto-Gen")
        Me.btnFrameSaveAsExtrafanart.Text = Master.eLang.GetString(1050, "Save as Extrafanart")
        Me.btnFrameSaveAsExtrathumb.Text = Master.eLang.GetString(305, "Save as Extrathumb")
        Me.btnFrameSaveAsFanart.Text = Master.eLang.GetString(1049, "Save as Fanart")
        Me.lblExtractingFrame.Text = Master.eLang.GetString(306, "Extracting Frame...")
        Me.btnFrameLoad.Text = Master.eLang.GetString(307, "Load Movie")

    End Sub

#End Region 'Methods

End Class
