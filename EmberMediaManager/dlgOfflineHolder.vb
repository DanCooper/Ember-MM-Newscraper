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
Imports System.Xml.Serialization
Imports EmberAPI

Public Class dlgOfflineHolder

#Region "Fields"

    Friend WithEvents bwCreateHolder As New System.ComponentModel.BackgroundWorker

    Private currNameText As String = String.Empty
    Private currText As String = Master.eLang.GetString(500, "Insert DVD")
    Private cMovie As New DVDProfiler.cDVD
    Private def_pbPreview_h As Integer
    Private def_pbPreview_w As Integer
    Private destPath As String
    Private drawFont As New Font("Arial", 22, FontStyle.Bold)
    Private fPath As String = String.Empty
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

    Private Sub AddDVDProfilerMovie(ByRef cMovie As DVDProfiler.cDVD)
        txtDVDTitle.Text = cMovie.Title
        txtCaseType.Text = cMovie.CaseType
        txtLocation.Text = If(cMovie.Discs.Disc.Count > 0, cMovie.Discs.Disc(0).dLocation, String.Empty)
        txtSlot.Text = If(cMovie.Discs.Disc.Count > 0, cMovie.Discs.Disc(0).dSlot, String.Empty)
        Select Case True
            Case cMovie.MediaTypes.BluRay = True
                txtMediaType.Text = "BluRay"
            Case cMovie.MediaTypes.DVD = True
                txtMediaType.Text = "DVD"
            Case cMovie.MediaTypes.HDDVD = True
                txtMediaType.Text = "HDDVD"
        End Select
    End Sub

    Private Sub ResetManager()
        chkUseFanart.Checked = False
        chkUseFanart.Enabled = False
        CleanUp()
        tMovie.Movie.Clear()
        txtCaseType.Text = String.Empty
        txtDVDTitle.Text = String.Empty
        txtLocation.Text = String.Empty
        txtMediaType.Text = String.Empty
        txtMovieName.Text = String.Empty
        txtSlot.Text = String.Empty
    End Sub

    Private Sub btnLoadSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadSingle.Click
        Try
            With ofdLoadXML
                .Filter = "Single.xml (*.xml)|*.xml"
                .FilterIndex = 0
            End With

            If ofdLoadXML.ShowDialog() = DialogResult.OK Then
                ResetManager()
                LoadSingleXML(ofdLoadXML.FileName)
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnLoadCollection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadCollection.Click
        Try
            With ofdLoadXML
                .Filter = "Collection.xml (*.xml)|*.xml"
                .FilterIndex = 0
            End With

            If ofdLoadXML.ShowDialog() = DialogResult.OK Then
                Using dDVDProfilerSelect As New dlgDVDProfilerSelect
                    Select Case dDVDProfilerSelect.ShowDialog(ofdLoadXML.FileName)
                        Case Windows.Forms.DialogResult.OK
                            ResetManager()
                            cMovie = dDVDProfilerSelect.Results
                            AddDVDProfilerMovie(cMovie)
                        Case Windows.Forms.DialogResult.Abort
                    End Select
                End Using
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub LoadSingleXML(ByVal fPath As String)
        Dim xmlSer As XmlSerializer = Nothing

        Try
            If File.Exists(fPath) Then
                Using xmlSR As StreamReader = New StreamReader(fPath)
                    xmlSer = New XmlSerializer(GetType(DVDProfiler.cDVD))
                    cMovie = DirectCast(xmlSer.Deserialize(xmlSR), DVDProfiler.cDVD)
                    AddDVDProfilerMovie(cMovie)
                End Using
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
        Me.EditMovie()
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

    Private Sub CheckConditions()
        Dim Fanart As New Images
        Try
            If rbTypeTitle.Checked Then
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
            Else
                If txtDVDTitle.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
                    MovieName = FileUtils.Common.MakeValidFilename(txtDVDTitle.Text)
                    tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                Else
                    MovieName = FileUtils.Common.MakeValidFilename(txtDVDTitle.Text)
                    For Each Invalid As Char In Path.GetInvalidPathChars
                        MovieName = MovieName.Replace(Invalid, String.Empty)
                    Next
                    tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                End If
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
                            gbMode.Enabled = True
                        End If
                    End Using
                End Using
            Else
                lvStatus.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                lvStatus.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            End If


            If rbTypeTitle.Checked Then
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
            Else
                If Not txtDVDTitle.Text = String.Empty Then
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
            End If

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
                btnCreate.Enabled = True
                gbSearch.Enabled = True
                For Each i As ListViewItem In lvStatus.Items
                    If Not i.SubItems(1).ForeColor = Color.Green Then
                        btnCreate.Enabled = False
                        gbSearch.Enabled = False
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
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

    Private Sub SetControlsEnabled(ByVal isEnabled As Boolean)
        btnClose.Enabled = isEnabled
        btnCreate.Enabled = isEnabled
        gbDVDProfiler.Enabled = isEnabled
        gbHolderType.Enabled = isEnabled
        gbMode.Enabled = isEnabled
        gbMovieTitle.Enabled = isEnabled
        gbSearch.Enabled = isEnabled
        gbSettings.Enabled = isEnabled
        gbSource.Enabled = isEnabled
        gbType.Enabled = isEnabled
        tbTagLine.Enabled = isEnabled
    End Sub

    Private Sub SetControlsDummyMovie(ByVal isDummy As Boolean)
        gbSettings.Enabled = True
        gbPreview.Visible = isDummy
        tbTagLine.Visible = isDummy
        tbTagLine.Enabled = isDummy
        txtMovieTitle.Enabled = Not isDummy
        If isDummy Then
            txtMovieTitle.Text = String.Empty
            lblTagline.Text = Master.eLang.GetString(542, "Place Holder Video Tagline:")
        Else
            txtMovieTitle.Text = tMovie.Movie.Title
            lblTagline.Text = "Message"
        End If
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        SetControlsEnabled(False)

        If rbDummyMovie.Checked Then
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
        Else
            CreateMediaStub()
        End If
    End Sub

    Private Sub CreateMediaStub()
        Dim doesExist As Boolean = False
        Dim xmlSer As New XmlSerializer(GetType(MediaContainers.Discstub))
        Dim fAtt As New FileAttributes
        Dim fAttWritable As Boolean = True
        Dim StubFile As String = String.Empty
        Dim StubPath As String = String.Empty
        Dim DiscStub As New MediaContainers.Discstub

        DiscStub.Title = txtMovieTitle.Text
        DiscStub.Message = txtTagline.Text

        StubFile = String.Concat(destPath, Path.DirectorySeparatorChar, FileUtils.Common.MakeValidFilename(txtDVDTitle.Text), If(Not String.IsNullOrEmpty(txtMediaType.Text), String.Concat(".", txtMediaType.Text.ToLower), String.Empty), ".disc")
        StubPath = Directory.GetParent(StubFile).FullName
        tMovie.Filename = StubFile

        doesExist = File.Exists(StubFile)
        If Not doesExist OrElse (Not CBool(File.GetAttributes(StubFile) And FileAttributes.ReadOnly)) Then

            If Not Directory.Exists(StubPath) Then
                Directory.CreateDirectory(StubPath)
            End If

            If doesExist Then
                fAtt = File.GetAttributes(StubFile)
                Try
                    File.SetAttributes(StubFile, FileAttributes.Normal)
                Catch ex As Exception
                    fAttWritable = False
                End Try
            End If

            Using xmlSW As New StreamWriter(StubFile)
                xmlSer.Serialize(xmlSW, DiscStub)
            End Using

            If doesExist And fAttWritable Then File.SetAttributes(StubFile, fAtt)
        End If

        Me.EditMovie()
        Me.Close()
    End Sub

    Private Sub EditMovie()

        Master.currMovie = tMovie

        Using dEditMovie As New dlgEditMovie
            Select Case dEditMovie.ShowDialog()
                Case Windows.Forms.DialogResult.OK
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieScraperRDYtoSave, Nothing, Master.currMovie)
                    'Me.SetListItemAfterEdit(ID, indX)
                    'If Me.RefreshMovie(ID) Then
                    '    Me.FillList(0)
                    'End If
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.MovieSync, Nothing, Master.currMovie)
                Case Windows.Forms.DialogResult.Retry
                    Master.currMovie.ClearEThumbs = False
                    Master.currMovie.ClearEFanarts = False
                    Master.currMovie.ClearFanart = False
                    Master.currMovie.ClearPoster = False
                    Functions.SetScraperMod(Enums.ModType.All, True, True)
                    'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultOptions) ', ID)
                Case Windows.Forms.DialogResult.Abort
                    Master.currMovie.ClearEThumbs = False
                    Master.currMovie.ClearEFanarts = False
                    Master.currMovie.ClearFanart = False
                    Master.currMovie.ClearPoster = False
                    Functions.SetScraperMod(Enums.ModType.DoSearch, True)
                    Functions.SetScraperMod(Enums.ModType.All, True, False)
                    'Me.MovieScrapeData(True, Enums.ScrapeType.SingleScrape, Master.DefaultOptions) ', ID, True)
                Case Else
                    'If Me.InfoCleared Then Me.LoadInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
            End Select
        End Using
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
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
            cMovie = New DVDProfiler.cDVD
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub dlgOfflineHolder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Sub btnSearchMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If rbTypeTitle.Checked Then
            tMovie.Movie.Clear()
            tMovie.Movie.Title = txtMovieName.Text
            SearchMovie()
            gbHolderType.Enabled = True
        Else
            tMovie.Movie.Clear()
            MergeMovie()
            SearchMovie()
            gbHolderType.Enabled = True
        End If
    End Sub

    Private Sub MergeMovie()
        tMovie.Movie.Title = cMovie.Title
        tMovie.Movie.Year = cMovie.ProductionYear
        tMovie.FileSource = cMovie.MediaTypes.ToString
        Select Case True
            Case cMovie.MediaTypes.BluRay = True
                tMovie.FileSource = "bluray"
            Case cMovie.MediaTypes.DVD = True
                tMovie.FileSource = "dvd"
            Case cMovie.MediaTypes.HDDVD = True
                tMovie.FileSource = "hddvd"
        End Select

        If cMovie.Subtitles.Subtitle.Count > 0 Then
            For Each sStream In cMovie.Subtitles.Subtitle
                Dim stream_s As New MediaInfo.Subtitle
                stream_s.LongLanguage = sStream
                stream_s.Language = Localization.ISOLangGetCode3ByLang(sStream)
                stream_s.SubsType = "Embedded"
                tMovie.Movie.FileInfo.StreamDetails.Subtitle.Add(DirectCast(stream_s, MediaInfo.Subtitle))
            Next
        End If

        If cMovie.Audio.AudioTrack.Count > 0 Then
            For Each aStream In cMovie.Audio.AudioTrack
                Dim stream_a As New MediaInfo.Audio
                stream_a.Channels = DVDProfiler.ConvertAChannels(aStream.AudioChannels)
                stream_a.Codec = DVDProfiler.ConvertAFormat(aStream.AudioFormat).ToLower
                stream_a.LongLanguage = aStream.AudioContent
                stream_a.Language = Localization.ISOLangGetCode3ByLang(aStream.AudioContent)
                tMovie.Movie.FileInfo.StreamDetails.Audio.Add(DirectCast(stream_a, MediaInfo.Audio))
            Next
        End If

            ' TODO: audio & subtitles
    End Sub

    Private Sub SearchMovie()
        Dim Poster As New MediaContainers.Image
        Dim Fanart As New MediaContainers.Image
        Dim aList As New List(Of MediaContainers.Image)
        Dim aUrlList As New List(Of Trailers)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)
        Try
            chkUseFanart.Checked = False
            Me.CleanUp()
            fPath = String.Empty
            'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
            Functions.SetScraperMod(Enums.ModType.NFO, True, True)
            Functions.SetScraperMod(Enums.ModType.Poster, True, False)
            Functions.SetScraperMod(Enums.ModType.Fanart, True, False)
            Functions.SetScraperMod(Enums.ModType.EThumbs, True, False)
            Functions.SetScraperMod(Enums.ModType.EFanarts, True, False)
            Functions.SetScraperMod(Enums.ModType.Trailer, True, False)

            If Not ModulesManager.Instance.MovieScrapeOnly(tMovie, Enums.ScrapeType.SingleScrape, Master.DefaultOptions) Then
                If rbTypeTitle.Checked Then
                    Me.txtMovieName.Text = String.Format("{0} [OffLine]", tMovie.Movie.Title)
                Else
                    Me.txtDVDTitle.Text = String.Format("{0} [OffLine]", tMovie.Movie.Title)
                End If
            End If

            If Master.GlobalScrapeMod.Poster Then
                aList.Clear()
                If Poster.WebImage.IsAllowedToDownload(tMovie, Enums.ImageType.Posters) Then
                    If Not ModulesManager.Instance.MovieScrapeImages(tMovie, Enums.ScraperCapabilities.Poster, aList) Then
                        If aList.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(tMovie, Enums.ImageType.Posters, aList, etList, efList) = DialogResult.OK Then
                                    Poster = dImgSelect.Results
                                    tMovie.PosterPath = ":" & Poster.URL
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If Master.GlobalScrapeMod.Fanart Then
                Fanart.Clear()
                aList.Clear()
                efList.Clear()
                etList.Clear()
                If Fanart.WebImage.IsAllowedToDownload(tMovie, Enums.ImageType.Fanart) Then
                    If Not ModulesManager.Instance.MovieScrapeImages(tMovie, Enums.ScraperCapabilities.Fanart, aList) Then
                        If aList.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(tMovie, Enums.ImageType.Fanart, aList, efList, etList) = DialogResult.OK Then
                                    Fanart = dImgSelect.Results
                                    efList = dImgSelect.efList
                                    etList = dImgSelect.etList
                                    tMovie.FanartPath = ":" & Fanart.URL
                                    tMovie.efList = efList
                                    tMovie.etList = etList

                                    ' needs local fanart for dummy movie
                                    If Not String.IsNullOrEmpty(Fanart.URL) Then
                                        Fanart.WebImage.FromWeb(Fanart.URL)
                                    End If
                                    If Not IsNothing(Fanart.WebImage.Image) Then
                                        fPath = Fanart.WebImage.SaveAsFanart(tMovie)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If
            CheckConditions()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub lvStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStatus.SelectedIndexChanged
        'no selection in here please :)
        lvStatus.SelectedItems.Clear()
    End Sub

    Private Sub rbModeSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbModeSingle.CheckedChanged
        If gbMode.Enabled Then
            ResetManager()
            gbType.Enabled = True
        End If
        CheckConditions()
    End Sub

    Private Sub rbTypeTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTypeTitle.CheckedChanged
        If gbType.Enabled Then
            ResetManager()
            gbMovieTitle.Enabled = True
            gbDVDProfiler.Enabled = False
        End If
        CheckConditions()
    End Sub

    Private Sub rbTypeDVDProfiler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTypeDVDProfiler.CheckedChanged
        If gbType.Enabled Then
            ResetManager()
            gbMovieTitle.Enabled = False
            gbDVDProfiler.Enabled = True
            txtMovieName.Text = String.Empty
        End If
        CheckConditions()
    End Sub

    Private Sub rbDummyMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDummyMovie.CheckedChanged
        If gbHolderType.Enabled Then
            SetControlsDummyMovie(True)
        End If
        CheckConditions()
    End Sub

    Private Sub rbMediaStub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMediaStub.CheckedChanged
        If gbHolderType.Enabled Then
            SetControlsDummyMovie(False)
        End If
        CheckConditions()
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
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(524, "Offline Media Manager")
        Me.btnClose.Text = Master.eLang.GetString(19, "Close")
        Me.lblTopDetails.Text = Master.eLang.GetString(525, "Add Offline movie")
        Me.lblTopTitle.Text = Me.Text
        Me.lblMovie.Text = Master.eLang.GetString(527, "Place Holder Folder/Movie Name:")
        Me.btnSearch.Text = Master.eLang.GetString(528, "Search Movie")
        Me.colCondition.Text = Master.eLang.GetString(532, "Condition")
        Me.colStatus.Text = Master.eLang.GetString(215, "Status")
        Me.btnCreate.Text = Master.eLang.GetString(533, "Create")
        Me.chkUseFanart.Text = Master.eLang.GetString(541, "Use Fanart for Place Holder Video")
        Me.lblTagline.Text = Master.eLang.GetString(542, "Place Holder Video Tagline:")
        Me.txtTagline.Text = Master.eLang.GetString(500, "Insert DVD")
        Me.lblTextColor.Text = Master.eLang.GetString(543, "Text Color:")
        Me.gbPreview.Text = Master.eLang.GetString(180, "Preview")
        Me.lblVideoFormat.Text = Master.eLang.GetString(544, "Place Holder Video Format:")
        Me.chkBackground.Text = Master.eLang.GetString(545, "Use Tagline Background")
        Me.lblTaglineBGColor.Text = Master.eLang.GetString(546, "Tagline background Color:")
        Me.chkOverlay.Text = Master.eLang.GetString(547, "Use Ember Overlay")
        Me.btnFont.Text = Master.eLang.GetString(548, "Select Font...")
        Me.lblTaglineTop.Text = Master.eLang.GetString(550, "Tagline Top:")
        Me.gbInfo.Text = Master.eLang.GetString(551, "Information")
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

    Private Sub txtDVDTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDVDTitle.TextChanged
        Me.currNameText = Me.txtDVDTitle.Text
        Me.tmrNameWait.Enabled = False
        Me.tmrNameWait.Enabled = True
    End Sub

    Private Sub txtTagline_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTagline.TextChanged
        Me.currText = Me.txtTagline.Text
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