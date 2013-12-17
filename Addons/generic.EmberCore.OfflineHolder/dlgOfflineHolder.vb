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

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports EmberAPI
Imports EmberScraperModule

Public Class dlgOfflineHolder

#Region "Fields"

    Friend WithEvents bwCreateHolder As New System.ComponentModel.BackgroundWorker

    Private currNameText As String = String.Empty
    Private currText As String = Master.eLang.GetString(500, "Insert DVD")
    Private def_pbPreview_h As Integer
    Private def_pbPreview_w As Integer
    Private destPath As String
    Private drawFont As New Font("Arial", 22, FontStyle.Bold)
    Private WorkingPath As String = Path.Combine(Master.TempPath, "OfflineHolder")
    Private FileName As String = Path.Combine(WorkingPath, "PlaceHolder.avi")
    Private idxStsImage As Integer = -1
    Private idxStsMovie As Integer = -1
    Private idxStsSource As Integer = -1
    Private MovieName As String = String.Empty
    Private Overlay As New Images
    Private OverlayPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "Offlineoverlay.png")
    Private Preview As Bitmap
    Private PreviewPath As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "OfflineDefault.jpg")
    Private PreviewPathW As String = String.Concat(Functions.AppPath, "Images", Path.DirectorySeparatorChar, "OfflineDefaultWide.jpg")
    Private prevNameText As String = String.Empty
    Private prevText As String = String.Empty
    Private RealImage_H As Integer
    Private RealImage_ratio As Double
    Private RealImage_W As Integer
    Private textHeight As SizeF
    Private tMovie As New Structures.DBMovie
    Private txtTopPos As Integer
    Private Video_Height As Integer
    Private Video_Width As Integer
    Private Video_Aspect As String


#End Region 'Fields

#Region "Methods"

    Private Sub btnBackgroundColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackgroundColor.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnBackgroundColor.BackColor = .Color
                    Me.CreatePreview()
                End If
            End If
        End With
    End Sub

    Private Sub btnFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFont.Click
        With Me.fdFont
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not IsNothing(.Font) Then
                    Me.drawFont = .Font
                    Me.CreatePreview()
                End If
            End If
        End With
    End Sub

    Private Sub btnTextColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTextColor.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnTextColor.BackColor = .Color
                    Me.CreatePreview()
                End If
            End If
        End With
    End Sub

    Private Sub bwCreateHolder_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCreateHolder.DoWork
        Dim buildPath As String = Path.Combine(WorkingPath, "Temp")
        Dim imgTemp As Bitmap
        Dim imgFinal As Bitmap
        Dim newGraphics As Graphics
        Me.bwCreateHolder.ReportProgress(0, Master.eLang.GetString(501, "Preparing Data"))
        If Directory.Exists(buildPath) Then
            FileUtils.Delete.DeleteDirectory(buildPath)
        End If
        Directory.CreateDirectory(buildPath)

        If Not IsNothing(Preview) Then
            imgTemp = Preview
        Else
            imgTemp = New Bitmap(Video_Width, Video_Height)
        End If

        'First let's resize it (Mantain aspect ratio)
        imgFinal = New Bitmap(Video_Width, Convert.ToInt32(Video_Width / RealImage_ratio))
        newGraphics = Graphics.FromImage(imgFinal)
        newGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        newGraphics.DrawImage(imgTemp, New Rectangle(0, 0, Video_Width, Convert.ToInt32(Video_Width / (imgTemp.Width / imgTemp.Height))), New Rectangle(0, 0, imgTemp.Width, imgTemp.Height), GraphicsUnit.Pixel)
        'Dont need this one anymore
        imgTemp.Dispose()
        'Save Master Image
        imgFinal.Save(Path.Combine(buildPath, "master.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg)
        ' Create string to draw.
        Dim drawString As String = txtTagline.Text
        Dim drawBrush As New SolidBrush(btnTextColor.BackColor)
        Dim backgroundBrush As New SolidBrush(Color.FromArgb(180, btnBackgroundColor.BackColor.R, btnBackgroundColor.BackColor.G, btnBackgroundColor.BackColor.B))
        Dim drawPoint As New PointF(0.0F, txtTopPos)
        Dim stringSize As SizeF = newGraphics.MeasureString(drawString, drawFont)
        newGraphics.Dispose()
        Me.bwCreateHolder.ReportProgress(1, Master.eLang.GetString(502, "Creating Movie"))
        'Let's cycle
        Dim f As Integer = 1
        For c As Integer = Video_Width To Convert.ToInt32(-stringSize.Width) Step -2
            imgTemp = New Bitmap(imgFinal)
            newGraphics = Graphics.FromImage(imgTemp)
            drawPoint.X = c
            newGraphics.Save()

            If chkBackground.Checked AndAlso chkUseFanart.Checked Then
                newGraphics.FillRectangle(backgroundBrush, 0, txtTopPos - 5, imgTemp.Width, stringSize.Height + 10)
            End If
            newGraphics.DrawString(drawString, drawFont, drawBrush, drawPoint)
            If chkOverlay.Checked AndAlso chkUseFanart.Checked Then
                newGraphics.DrawImage(Overlay.Image, 0, Convert.ToUInt16(txtTopPos - 65 / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Width / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Height / (1920 / Video_Width)))
            End If

            imgTemp.Save(Path.Combine(buildPath, String.Format("image{0}.jpg", f)), System.Drawing.Imaging.ImageFormat.Jpeg)
            newGraphics.Dispose()
            imgTemp.Dispose()
            f += 1
        Next
        imgFinal.Dispose()
        If Me.bwCreateHolder.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateHolder.ReportProgress(2, Master.eLang.GetString(503, "Building Movie"))
        Using ffmpeg As New Process()

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                                                ffmpeg info                                                     '
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ' -r      = fps                                                                                                  '
            ' -an     = disable audio recording                                                                              '
            ' -i      = creating a video from many images                                                                    '
            ' -q:v n  = constant qualitiy(:video) (but a variable bitrate), "n" 1 (excellent quality) and 31 (worst quality) '
            ' -b:v n  = bitrate(:video)                                                                                      '
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            ffmpeg.StartInfo.FileName = Functions.GetFFMpeg
            ffmpeg.EnableRaisingEvents = False
            ffmpeg.StartInfo.UseShellExecute = False
            ffmpeg.StartInfo.CreateNoWindow = True
            ffmpeg.StartInfo.RedirectStandardOutput = True
            'ffmpeg.StartInfo.RedirectStandardError = True     <----- if activated, ffmpeg can not finish the building process 
            ffmpeg.StartInfo.Arguments = String.Format(" -r 25 -an -i ""{0}\image%d.jpg"" -q:v 1 -b:v 40000k ""{1}""", buildPath, tMovie.Filename)
            ffmpeg.Start()
            ffmpeg.WaitForExit()
            ffmpeg.Close()
        End Using

        If Me.bwCreateHolder.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateHolder.ReportProgress(4, Master.eLang.GetString(505, "Moving Files"))
        If Directory.Exists(buildPath) Then
            FileUtils.Delete.DeleteDirectory(buildPath)
        End If

        DirectoryCopy(WorkingPath, destPath)

        tMovie.Filename = Path.Combine(destPath, String.Concat(MovieName, ".avi"))

        If Not String.IsNullOrEmpty(tMovie.Movie.Title) Then
            Dim tTitle As String = StringUtils.FilterTokens(tMovie.Movie.Title)
            tMovie.Movie.SortTitle = tTitle
            If Master.eSettings.DisplayYear AndAlso Not String.IsNullOrEmpty(tMovie.Movie.Year) Then
                tMovie.ListTitle = String.Format("{0} ({1})", tTitle, tMovie.Movie.Year)
            Else
                tMovie.ListTitle = tTitle
            End If
        Else
            tMovie.ListTitle = MovieName.Replace("[Offline]", String.Empty).Trim
        End If

        tMovie.Movie.SortTitle = tMovie.ListTitle
        If Not String.IsNullOrEmpty(tMovie.PosterPath) Then
            'File.Copy(tMovie.PosterPath, Path.Combine(destPath, Path.GetFileName(tMovie.PosterPath).ToString))
            Dim poster As New EmberAPI.Images
            poster.FromFile(tMovie.PosterPath)
            tMovie.PosterPath = poster.SaveAsPoster(tMovie)
            'tMovie.PosterPath = Path.Combine(destPath, Path.GetFileName(tMovie.PosterPath).ToString)
        End If

        If Not String.IsNullOrEmpty(tMovie.FanartPath) Then
            'File.Copy(tMovie.PosterPath, Path.Combine(destPath, Path.GetFileName(tMovie.FanartPath).ToString))
            Dim fanart As New EmberAPI.Images
            fanart.FromFile(tMovie.FanartPath)
            tMovie.FanartPath = fanart.SaveAsFanart(tMovie)
            'tMovie.FanartPath = Path.Combine(destPath, Path.GetFileName(tMovie.FanartPath).ToString)
        End If

        If Not String.IsNullOrEmpty(tMovie.EThumbsPath) Then
            DirectoryCopy(Directory.GetParent(tMovie.EThumbsPath).FullName, Path.Combine(destPath, "extrathumbs"))
        End If

        If Not String.IsNullOrEmpty(tMovie.EFanartsPath) Then
            DirectoryCopy(Directory.GetParent(tMovie.EFanartsPath).FullName, Path.Combine(destPath, "extrafanart"))
        End If

        If Master.eSettings.ScraperActorThumbs Then
            Me.bwCreateHolder.ReportProgress(5, Master.eLang.GetString(31, "Generating Actor Thumbs"))
            For Each act As MediaContainers.Person In tMovie.Movie.Actors
                Dim img As New Images
                img.FromWeb(act.Thumb)
                If Not IsNothing(img.Image) Then
                    img.SaveAsActorThumb(act, Directory.GetParent(tMovie.Filename).FullName, tMovie)
                End If
            Next
        End If
        Dim tPath As String = Directory.GetParent(tMovie.Filename).FullName
        Dim tmpName As String = Path.Combine(tPath, StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(tMovie.Filename))).ToLower
        Dim fList As New List(Of String)

        Try
            fList.AddRange(Directory.GetFiles(Directory.GetParent(tMovie.Filename).FullName))
        Catch
        End Try
        For Each fFile As String In fList
            For Each t As String In Master.eSettings.ValidExts
                Select Case True
                    Case fFile.ToLower = String.Concat(tmpName, "-trailer", t.ToLower)
                        tMovie.TrailerPath = fFile
                        Exit For
                    Case fFile.ToLower = String.Concat(tmpName, "[trailer]", t.ToLower)
                        tMovie.TrailerPath = fFile
                        Exit For
                End Select
            Next
        Next

        tMovie = Master.DB.SaveMovieToDB(tMovie, True, False, True)

        Me.bwCreateHolder.ReportProgress(4, Master.eLang.GetString(506, "Renaming Files"))
        If Directory.Exists(buildPath) Then
            FileUtils.Delete.DeleteDirectory(buildPath)
        End If
        Try
            ' ##** FileFolderRenamer.RenameSingle(tMovie, Master.eSettings.FoldersPattern, Master.eSettings.FilesPattern, False, False, False)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.RenameMovie, New List(Of Object)(New Object() {False}), tMovie)
        Catch ex As Exception
        End Try

        If Me.bwCreateHolder.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateHolder.ReportProgress(5, Master.eLang.GetString(361, "Finished"))
    End Sub

    Private Sub CleanUp()
        Try
            If File.Exists(Path.Combine(Master.TempPath, "poster.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "poster.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "fanart.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "fanart.jpg"))
            End If

            If File.Exists(Path.Combine(Master.TempPath, "frame.jpg")) Then
                File.Delete(Path.Combine(Master.TempPath, "frame.jpg"))
            End If

            If Directory.Exists(Path.Combine(Master.TempPath, "extrathumbs")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "extrathumbs"))
            End If

            If Directory.Exists(Path.Combine(Master.TempPath, "OfflineHolder")) Then
                FileUtils.Delete.DeleteDirectory(Path.Combine(Master.TempPath, "OfflineHolder"))
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub bwCreateHolder_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwCreateHolder.ProgressChanged
        If lvStatus.Items.Count > 0 Then
            lvStatus.Items(lvStatus.Items.Count - 1).SubItems.Add(Master.eLang.GetString(362, "Done"))
        End If
        lvStatus.Items.Add(e.UserState.ToString)
    End Sub

    Private Sub bwCreateHolder_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCreateHolder.RunWorkerCompleted
        Me.pbProgress.Visible = False
        If Not e.Cancelled Then
            MsgBox(Master.eLang.GetString(507, "Offline movie place holder created!"), MsgBoxStyle.OkOnly, Me.Text)
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
        Me.Close()
    End Sub

    Private Sub cbFormat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFormat.SelectedIndexChanged
        Select Case cbFormat.SelectedIndex
            Case 0
                Video_Width = 1920
                Video_Height = 1080
                Video_Aspect = "Wide"
            Case 1
                Video_Width = 1280
                Video_Height = 720
                Video_Aspect = "Wide"
            Case 2
                Video_Width = 1024
                Video_Height = 576
                Video_Aspect = "Wide"
            Case 3
                Video_Width = 720
                Video_Height = 576
                Video_Aspect = "NotWide"
        End Select
        Dim fPath = tMovie.FanartPath
        SetPreview(Not chkUseFanart.Checked, fPath)
        If Not chkUseFanart.Checked AndAlso Not IsNothing(Preview) Then
            If Video_Aspect = "Wide" Then
                txtTop.Text = Convert.ToUInt16(Preview.Height - (167 / (1920 / Video_Width)) - (textHeight.Height / 2)).ToString
            Else
                txtTop.Text = Convert.ToUInt16(Preview.Height - 90 - (textHeight.Height / 2)).ToString
            End If
        End If
        CreatePreview()
    End Sub

    Private Sub cbSources_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSources.SelectedIndexChanged
        If Not String.IsNullOrEmpty(cbSources.Text) Then
            tMovie.Source = cbSources.Text
            CheckConditions()
        End If
    End Sub

    Sub CheckConditions()
        Dim Fanart As New Images
        Try
            If txtMovieName.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
                MovieName = FileUtils.Common.MakeValidFilename(txtMovieName.Text)
                tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            Else
                MovieName = FileUtils.Common.MakeValidFilename(txtMovieName.Text)
                For Each Invalid As Char In Path.GetInvalidPathChars
                    MovieName = MovieName.Replace(Invalid, String.Empty)
                Next
                tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            End If

            If cbSources.SelectedIndex >= 0 Then
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources WHERE Name = """, cbSources.SelectedItem.ToString, """;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        If SQLReader.Read Then
                            If Directory.GetDirectories(SQLReader("Path").ToString).Count = 0 Then
                                destPath = SQLReader("Path").ToString
                            Else
                                destPath = Path.Combine(SQLReader("Path").ToString, MovieName)
                            End If
                            lvStatus.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                            lvStatus.Items(idxStsSource).SubItems(1).ForeColor = Color.Green
                        End If
                    End Using
                End Using
            Else
                lvStatus.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                lvStatus.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            End If
            If Not txtMovieName.Text = String.Empty Then
                If Directory.Exists(destPath) Then
                    lvStatus.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
                    lvStatus.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
                Else
                    lvStatus.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                    lvStatus.Items(idxStsMovie).SubItems(1).ForeColor = Color.Green
                End If
            Else
                lvStatus.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                lvStatus.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            End If

            Dim fPath As String = tMovie.FanartPath

            If Not String.IsNullOrEmpty(fPath) Then
                chkUseFanart.Enabled = True
            Else
                chkUseFanart.Checked = False
                chkUseFanart.Enabled = False
            End If

            If chkUseFanart.Checked Then
                If Not String.IsNullOrEmpty(fPath) Then
                    SetPreview(False, fPath)
                    lvStatus.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                    lvStatus.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
                Else
                    lvStatus.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                    lvStatus.Items(idxStsImage).SubItems(1).ForeColor = Color.Red
                End If
            Else
                lvStatus.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                lvStatus.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
            End If

            Me.CreatePreview()
            If Not Me.pbProgress.Visible Then
                Create_Button.Enabled = True
                For Each i As ListViewItem In lvStatus.Items
                    If Not i.SubItems(1).ForeColor = Color.Green Then
                        Create_Button.Enabled = False
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        End Try

        Fanart.Dispose()
        Fanart = Nothing
    End Sub

    Private Sub chkBackground_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBackground.CheckedChanged
        Me.CreatePreview()
    End Sub

    Private Sub chkOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverlay.CheckedChanged
        If chkOverlay.Checked Then
            Overlay.FromFile(OverlayPath)
        End If
        Me.CreatePreview()
    End Sub

    Private Sub chkUseFanart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseFanart.CheckedChanged
        If Not chkUseFanart.Checked Then
            If File.Exists(PreviewPath) Then
                SetPreview(True, PreviewPath)
                txtTop.Text = (Preview.Height - 150 / (1920 / Video_Width)).ToString
            End If
        Else
            Dim fPath As String = tMovie.FanartPath
            If Not fPath = String.Empty Then
                SetPreview(False, fPath)
            End If
        End If
        chkOverlay.Enabled = chkUseFanart.Checked
        chkBackground.Enabled = chkUseFanart.Checked
        If chkOverlay.Enabled Then
            chkOverlay.CheckState = CheckState.Checked
            chkBackground.CheckState = CheckState.Checked
        End If
        CreatePreview()
    End Sub

    Private Sub Close_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CLOSE_Button.Click
        If bwCreateHolder.IsBusy Then
            bwCreateHolder.CancelAsync()
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CleanUp()
            Me.Close()
        End If
    End Sub

    Private Sub CreatePreview()
        If Preview Is Nothing Then Return
        Dim bmCloneOriginal As Bitmap = New Bitmap(Preview)

        If chkUseFanart.Checked AndAlso chkOverlay.Checked Then
            tbTagLine.Minimum = Convert.ToInt32(textHeight.Height) ' - 3
            tbTagLine.Maximum = Convert.ToInt32(bmCloneOriginal.Height - (65 / (1920 / Video_Width)))
        Else
            tbTagLine.Minimum = Convert.ToInt32(textHeight.Height) ' - 3
            tbTagLine.Maximum = Convert.ToInt32(bmCloneOriginal.Height)
        End If

        If txtTopPos < bmCloneOriginal.Height - tbTagLine.Maximum Then
            txtTopPos = bmCloneOriginal.Height - tbTagLine.Maximum
            txtTop.Text = txtTopPos.ToString
        End If

        If txtTopPos > bmCloneOriginal.Height - tbTagLine.Minimum Then
            txtTopPos = bmCloneOriginal.Height - tbTagLine.Minimum
            txtTop.Text = txtTopPos.ToString
        End If

        tbTagLine.Value = bmCloneOriginal.Height - txtTopPos

        pbPreview.Width = def_pbPreview_w
        pbPreview.Height = def_pbPreview_h
        Dim w, h As Integer
        w = pbPreview.Width
        h = Convert.ToInt16(pbPreview.Width / RealImage_ratio)
        If h > def_pbPreview_h Then
            h = pbPreview.Height
            w = Convert.ToInt16(pbPreview.Height * RealImage_ratio)
        End If

        pbPreview.Image = New Bitmap(w, h)

        Dim grOriginal As Graphics = Graphics.FromImage(bmCloneOriginal)
        Dim grPreview As Graphics = Graphics.FromImage(pbPreview.Image)
        Dim drawBrush As New SolidBrush(btnTextColor.BackColor)
        Dim backgroundBrush As New SolidBrush(Color.FromArgb(180, btnBackgroundColor.BackColor.R, btnBackgroundColor.BackColor.G, btnBackgroundColor.BackColor.B))

        Dim iLeft As Integer = Convert.ToInt32((bmCloneOriginal.Width - textHeight.Width) / 2)
        If chkBackground.Checked AndAlso chkUseFanart.Checked Then
            grOriginal.FillRectangle(backgroundBrush, 0, txtTopPos - 5, bmCloneOriginal.Width, drawFont.Height + 10)
        End If
        grOriginal.DrawString(txtTagline.Text, drawFont, drawBrush, iLeft, txtTopPos)
        If chkOverlay.Checked AndAlso chkUseFanart.Checked Then
            grOriginal.DrawImage(Overlay.Image, 0, Convert.ToUInt16(txtTopPos - 65 / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Width / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Height / (1920 / Video_Width)))
        End If
        grPreview.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        grPreview.DrawImage(bmCloneOriginal, New Rectangle(0, 0, pbPreview.Width, pbPreview.Height), New Rectangle(0, 0, bmCloneOriginal.Width, bmCloneOriginal.Height), GraphicsUnit.Pixel)

        If chkUseFanart.Checked AndAlso chkOverlay.Checked Then
            tbTagLine.Location = New Point(tbTagLine.Location.X, Convert.ToInt32((gbPreview.Location.Y + pbPreview.Location.Y) - 13 + ((textHeight.Height / (RealImage_H / pbPreview.Height)) / 2) + (65 / (1920 / Video_Width) / (RealImage_H / pbPreview.Height))))
            tbTagLine.Height = Convert.ToInt32(pbPreview.Height + 26 - (textHeight.Height / (RealImage_H / pbPreview.Height)) - (65 / (1920 / Video_Width) / (RealImage_H / pbPreview.Height)))
        Else
            tbTagLine.Location = New Point(tbTagLine.Location.X, Convert.ToInt32((gbPreview.Location.Y + pbPreview.Location.Y) - 13 + ((textHeight.Height / (RealImage_H / pbPreview.Height)) / 2)))
            tbTagLine.Height = Convert.ToInt32(pbPreview.Height + 26 - (textHeight.Height / (RealImage_H / pbPreview.Height)))
        End If
    End Sub

    Private Sub Create_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Create_Button.Click
        cbSources.Enabled = False
        Create_Button.Enabled = False
        txtMovieName.Enabled = False
        txtTagline.Enabled = False
        chkUseFanart.Enabled = False
        CLOSE_Button.Enabled = False
        'Need to avoid cross thread in BackgroundWorker
        'txtTopPos = Video_Width / (pbPreview.Image.Width / Convert.ToSingle(currTopText)) ' ... and Scale it
        Me.pbProgress.Value = 100
        Me.pbProgress.Style = ProgressBarStyle.Marquee
        Me.pbProgress.MarqueeAnimationSpeed = 25
        Me.pbProgress.Visible = True

        lvStatus.Items.Clear()
        Me.bwCreateHolder = New System.ComponentModel.BackgroundWorker
        Me.bwCreateHolder.WorkerReportsProgress = True
        Me.bwCreateHolder.WorkerSupportsCancellation = True
        Me.bwCreateHolder.RunWorkerAsync()
    End Sub

    Private Sub DirectoryCopy(ByVal sourceDirName As String, ByVal destDirName As String)
        Try

            Dim dir As New DirectoryInfo(sourceDirName)
            ' If the source directory does not exist, throw an exception.
            If Not dir.Exists Then
                Throw New DirectoryNotFoundException( _
                    Master.eLang.GetString(509, "Source directory does not exist or could not be found: ") _
                    + sourceDirName)
            End If
            ' If the destination directory does not exist, create it.
            If Not Directory.Exists(destDirName) Then
                Directory.CreateDirectory(destDirName)
            End If
            ' Get the file contents of the directory to copy.
            Dim Files As New List(Of FileInfo)

            Try
                Files.AddRange(dir.GetFiles())
            Catch
            End Try

            For Each sFile As FileInfo In Files
                FileUtils.Common.MoveFileWithStream(sFile.FullName, Path.Combine(destDirName, sFile.Name))
            Next

            Files = Nothing
            dir = Nothing
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgOfflineHolder_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Directory.Exists(WorkingPath) Then
            FileUtils.Delete.DeleteDirectory(WorkingPath)
        End If
    End Sub

    Private Sub dlgOfflineHolder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.SetUp()
            def_pbPreview_w = pbPreview.Width
            def_pbPreview_h = pbPreview.Height
            Video_Width = 1920
            Video_Height = 1080
            Video_Aspect = "Wide"
            cbFormat.SelectedIndex = 0
            Dim iBackground As New Bitmap(Me.pnlTop.Width, Me.pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(Me.pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                Me.pnlTop.BackgroundImage = iBackground
            End Using

            'load all the movie folders from settings
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MediaDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Concat("SELECT Name FROM Sources;")
                Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    While SQLReader.Read
                        Me.cbSources.Items.Add(SQLReader("Name"))
                    End While
                End Using
            End Using

            If Directory.Exists(WorkingPath) Then
                FileUtils.Delete.DeleteDirectory(WorkingPath)
            End If
            Directory.CreateDirectory(WorkingPath)

            CreatePreview()
            tMovie.Movie = New MediaContainers.Movie
            tMovie.isSingle = True
            idxStsSource = lvStatus.Items.Add(Master.eLang.GetString(318, "Source Folder")).Index
            lvStatus.Items(idxStsSource).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
            lvStatus.Items(idxStsSource).UseItemStyleForSubItems = False
            lvStatus.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            idxStsMovie = lvStatus.Items.Add(Master.eLang.GetString(519, "Movie (Folder Name)")).Index
            lvStatus.Items(idxStsMovie).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
            lvStatus.Items(idxStsMovie).UseItemStyleForSubItems = False
            lvStatus.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            idxStsImage = lvStatus.Items.Add(Master.eLang.GetString(523, "Place Holder Image")).Index
            lvStatus.Items(idxStsImage).SubItems.Add(Master.eLang.GetString(195, "Valid"))
            lvStatus.Items(idxStsImage).UseItemStyleForSubItems = False
            lvStatus.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgOfflineHolder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub GetIMDB_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetIMDB_Button.Click
        Dim Poster As New MediaContainers.Image
        Dim tUrl As String = String.Empty
        Try
            Me.CleanUp()
            tMovie.Movie.Clear()
            tMovie.Movie.Title = txtMovieName.Text
            Dim tempExtraPath As String = Path.Combine(Directory.GetParent(Directory.GetParent(tMovie.Filename).FullName).FullName, "extrathumbs")
            'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
            Functions.SetScraperMod(Enums.ModType.NFO, True, True)
            Functions.SetScraperMod(Enums.ModType.Poster, True, False)
            Functions.SetScraperMod(Enums.ModType.Fanart, True, False)
            Functions.SetScraperMod(Enums.ModType.EThumbs, True, False)
            Functions.SetScraperMod(Enums.ModType.EFanarts, True, False)
            Functions.SetScraperMod(Enums.ModType.Trailer, True, False)

            '****** original part of 1.3.x ********
            'If Not ModulesManager.Instance.MovieScrapeOnly(tMovie, Enums.ScrapeType.SingleScrape, Master.DefaultOptions) Then
            '    Me.txtMovieName.Text = String.Format("{0} [OffLine]", tMovie.Movie.Title)
            '    Dim fResults = New Containers.ImgResult
            '    ModulesManager.Instance.ScraperSelectImageOfType(tMovie, Enums.ImageType.Posters, fResults, True)
            '    If Not String.IsNullOrEmpty(fResults.ImagePath) Then
            '        tMovie.PosterPath = fResults.ImagePath
            '    End If
            '    fResults = New Containers.ImgResult
            '    ModulesManager.Instance.ScraperSelectImageOfType(tMovie, Enums.ImageType.Fanart, fResults, True)
            '    If Not String.IsNullOrEmpty(fResults.ImagePath) Then
            '        tMovie.FanartPath = fResults.ImagePath
            '        If Directory.Exists(tempEThumbsPath) Then
            '            tMovie.ExtraPath = Path.Combine(tempEThumbsPath, "thumb1.jpg")
            '        End If
            '        If Not Master.eSettings.NoSaveImagesToNfo Then tMovie.Movie.Fanart = fResults.Fanart
            '    End If
            '    If Master.eSettings.DownloadTrailers Then
            '        If Not Directory.Exists(Directory.GetParent(tMovie.Filename).FullName) Then
            '            Directory.CreateDirectory(Directory.GetParent(tMovie.Filename).FullName)
            '        End If
            '        ModulesManager.Instance.ScraperDownloadTrailer(tMovie)
            '    End If
            'End If
            If Not ModulesManager.Instance.MovieScrapeOnly(tMovie, Enums.ScrapeType.SingleScrape, Master.DefaultOptions) Then
                Me.txtMovieName.Text = String.Format("{0} [OffLine]", tMovie.Movie.Title)
                'Dim sPath As String = Path.Combine(Master.TempPath, "fanart.jpg")
                Dim fResults As New Containers.ImgResult
                Dim tList As New List(Of MediaContainers.Image)
                If ModulesManager.Instance.MovieScrapeImages(tMovie, Enums.ScraperCapabilities.Fanart, tList) Then
                    Poster.Clear()
                    If Images.GetPreferredFanart(tList, Poster) Then
                        If IsNothing(Poster.WebImage.Image) Then
                            Poster.WebImage.FromWeb(Poster.URL)
                        End If
                        If Not IsNothing(Poster.WebImage.Image) Then
                            tUrl = Poster.WebImage.SaveAsFanart(tMovie)
                            If Not String.IsNullOrEmpty(tUrl) Then
                                tMovie.FanartPath = tUrl
                                If Not Master.eSettings.NoSaveImagesToNfo Then
                                    tMovie.Movie.Fanart.URL = Poster.URL
                                End If
                            End If
                        End If
                    End If
                End If

                If ModulesManager.Instance.MovieScrapeImages(tMovie, Enums.ScraperCapabilities.Poster, tList) Then
                    Poster.Clear()
                    If Images.GetPreferredPoster(tList, Poster) Then
                        If IsNothing(Poster.WebImage.Image) Then
                            Poster.WebImage.FromWeb(Poster.URL)
                        End If
                        If Not IsNothing(Poster.WebImage.Image) Then
                            tUrl = Poster.WebImage.SaveAsPoster(tMovie)
                            If Not String.IsNullOrEmpty(tUrl) Then
                                tMovie.PosterPath = tUrl
                            End If
                        End If
                    End If
                End If
            End If
            CheckConditions()
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lvStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStatus.SelectedIndexChanged
        'no selection in here please :)
        lvStatus.SelectedItems.Clear()
    End Sub

    Sub SetPreview(ByVal bDefault As Boolean, Optional ByVal path As String = "")
        Try

            If bDefault Then
                If Video_Aspect = "Wide" Then
                    path = PreviewPathW
                Else
                    path = PreviewPath
                End If
            End If
            If path <> "" Then
                'First let's resize it (Mantain aspect ratio)
                Dim tmpImg As Bitmap = New Bitmap(path)
                RealImage_W = Video_Width
                RealImage_ratio = tmpImg.Width / tmpImg.Height
                RealImage_H = Convert.ToInt32(RealImage_W / RealImage_ratio)
                Preview = New Bitmap(RealImage_W, RealImage_H)
                Dim newGraphics = Graphics.FromImage(Preview)
                newGraphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                newGraphics.DrawImage(tmpImg, New Rectangle(0, 0, RealImage_W, RealImage_H), New Rectangle(0, 0, tmpImg.Width, tmpImg.Height), GraphicsUnit.Pixel)
                'Dont need this one anymore
                tmpImg.Dispose()
                If Not IsNothing(drawFont) Then
                    textHeight = newGraphics.MeasureString(txtTagline.Text, drawFont)
                Else
                    drawFont = New Font("Arial", Convert.ToUInt16(22 / (1920 / Video_Width)), FontStyle.Bold)
                    textHeight = newGraphics.MeasureString(txtTagline.Text, drawFont)
                End If
            Else
                RealImage_W = Video_Width
                RealImage_ratio = Preview.Width / Preview.Height
                RealImage_H = Convert.ToInt32(RealImage_W / RealImage_ratio)
            End If
            If txtTopPos > RealImage_H Then
                txtTop.Text = (RealImage_H - 30).ToString
            End If
        Catch ex As Exception
            Master.eLog.Error(Me.GetType(), ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(524, "Offline Media Manager")
        Me.CLOSE_Button.Text = Master.eLang.GetString(19, "Close")
        Me.Label2.Text = Master.eLang.GetString(525, "Add Offline movie")
        Me.Label4.Text = Me.Text
        Me.lblSources.Text = Master.eLang.GetString(526, "Add to Source:")
        Me.lblMovie.Text = Master.eLang.GetString(527, "Place Holder Folder/Movie Name:")
        Me.GetIMDB_Button.Text = Master.eLang.GetString(528, "Search IMDB")
        Me.Bulk_Button.Text = Master.eLang.GetString(531, "Bulk Creator")
        Me.colCondition.Text = Master.eLang.GetString(532, "Condition")
        Me.colStatus.Text = Master.eLang.GetString(215, "Status")
        Me.Create_Button.Text = Master.eLang.GetString(533, "Create")
        Me.chkUseFanart.Text = Master.eLang.GetString(541, "Use Fanart for Place Holder Video")
        Me.lblTagline.Text = Master.eLang.GetString(542, "Place Holder Video Tagline:")
        Me.txtTagline.Text = Master.eLang.GetString(500, "Insert DVD")
        Me.Label1.Text = Master.eLang.GetString(543, "Text Color:")
        Me.gbPreview.Text = Master.eLang.GetString(180, "Preview")
        Me.Label6.Text = Master.eLang.GetString(544, "Place Holder Video Format:")
        Me.chkBackground.Text = Master.eLang.GetString(545, "Use Tagline Background")
        Me.Label5.Text = Master.eLang.GetString(546, "Tagline background Color:")
        Me.chkOverlay.Text = Master.eLang.GetString(547, "Use Ember Overlay")
        Me.btnFont.Text = Master.eLang.GetString(548, "Select Font...")
        Me.Label3.Text = Master.eLang.GetString(550, "Tagline Top:")
        Me.GroupBox2.Text = Master.eLang.GetString(551, "Information")
        Me.cbFormat.Items.AddRange(New Object() {"1080", "720", "DV PAL Wide", "DV PAL 4:3"})
    End Sub

    Private Sub tbTagLine_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTagLine.Scroll
        txtTop.Text = (RealImage_H - tbTagLine.Value).ToString
    End Sub

    Private Sub tmrNameWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrNameWait.Tick
        If Me.prevNameText = Me.currNameText Then
            Me.tmrName.Enabled = True
        Else
            Me.prevNameText = Me.currNameText
        End If
    End Sub

    Private Sub tmrName_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrName.Tick
        Me.tmrNameWait.Enabled = False
        Me.CheckConditions()
        Me.tmrName.Enabled = False
    End Sub

    Private Sub txtMovieName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMovieName.TextChanged
        Me.currNameText = Me.txtMovieName.Text
        Me.tmrNameWait.Enabled = False
        Me.tmrNameWait.Enabled = True
    End Sub

    Private Sub txtTagline_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTagline.TextChanged
        Me.currText = Me.txtTagline.Text
        'Dim fPath As String = tMovie.FanartPath
        'If Not fPath = String.Empty AndAlso chkUseFanart.Checked Then
        '    SetPreview(False, fPath)
        'Else
        '    SetPreview(True, PreviewPath)
        'End If
    End Sub

    Private Sub txtTop_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTop.KeyPress
        If StringUtils.NumericOnly(e.KeyChar) Then
            e.Handled = True
            Me.CheckConditions()
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTop.TextChanged
        Try
            txtTopPos = Convert.ToUInt16(txtTop.Text)
            CreatePreview()
        Catch ex As Exception
        End Try
    End Sub

#End Region 'Methods

End Class