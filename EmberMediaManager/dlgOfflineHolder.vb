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
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports EmberAPI
Imports NLog

Public Class dlgOfflineHolder

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCreateDummyFile As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwCreateMediaStub As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwMovieScraper As New System.ComponentModel.BackgroundWorker

    Private currNameText As String = String.Empty
    Private currText As String = Master.eLang.GetString(500, "Insert DVD")
    Private cMovieList As New List(Of DVDProfiler.cDVD)
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
    Private tMovieList As New List(Of Database.DBElement)
    Private txtTopPos As Integer
    Private Video_Height As Integer
    Private Video_Width As Integer
    Private Video_Aspect As String


#End Region 'Fields

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Private Sub AddDVDProfilerCollectionMovie(ByVal cMovieList As List(Of DVDProfiler.cDVD))
        txtFolderNameDVDProfiler.Text = String.Concat(cMovieList.Item(0).Title, " [Offline]")
        txtCaseType.Text = cMovieList.Item(0).CaseType
        txtDVDTitle.Text = cMovieList.Item(0).Title
        txtLocation.Text = If(cMovieList.Item(0).Discs.Disc.Count > 0, cMovieList.Item(0).Discs.Disc(0).dLocation, String.Empty)
        txtSlot.Text = If(cMovieList.Item(0).Discs.Disc.Count > 0, cMovieList.Item(0).Discs.Disc(0).dSlot, String.Empty)
        Select Case True
            Case cMovieList.Item(0).MediaTypes.BluRay = True
                txtMediaType.Text = "BluRay"
            Case cMovieList.Item(0).MediaTypes.DVD = True
                txtMediaType.Text = "DVD"
            Case cMovieList.Item(0).MediaTypes.HDDVD = True
                txtMediaType.Text = "HDDVD"
        End Select
    End Sub

    Private Sub AddDVDProfilerMovie(ByVal tMovie As Database.DBElement)
        txtCaseType.Text = tMovie.DVDProfilerCaseType
        txtDVDTitle.Text = tMovie.DVDProfilerTitle
        txtFolderNameDVDProfiler.Text = String.Concat(tMovie.DVDProfilerTitle, " [Offline]")
        txtLocation.Text = tMovie.DVDProfilerLocation
        txtMediaType.Text = tMovie.DVDProfilerMediaType
        txtSlot.Text = tMovie.DVDProfilerSlot
        tMovieList.Add(tMovie)
    End Sub

    Private Sub DisplaySelectedMovie(ByVal tMovie As Database.DBElement)
        txtCaseType.Text = tMovie.DVDProfilerCaseType
        txtDVDTitle.Text = tMovie.DVDProfilerTitle
        txtFolderNameDVDProfiler.Text = tMovie.OfflineHolderFoldername
        txtLocation.Text = tMovie.DVDProfilerLocation
        txtMediaType.Text = tMovie.DVDProfilerMediaType
        txtSlot.Text = tMovie.DVDProfilerSlot
    End Sub

    Private Function ApplyPattern(ByVal pattern As String, ByVal flag As String, ByVal v As String) As String
        pattern = pattern.Replace(String.Concat("$", flag), v)
        If Not v = String.Empty Then
            pattern = pattern.Replace(String.Concat("$-", flag), v)
            pattern = pattern.Replace(String.Concat("$+", flag), v)
            pattern = pattern.Replace(String.Concat("$^", flag), v)

        Else
            Dim pos = -1
            Dim size = 3
            Dim nextC = pattern.IndexOf(String.Concat("$+", flag))
            If nextC >= 0 Then
                If nextC + 3 < pattern.Length Then size += 1
                pos = nextC
            End If
            Dim prevC = pattern.IndexOf(String.Concat("$-", flag))
            If prevC >= 0 Then
                If prevC + 3 < pattern.Length Then size += 1
                If prevC > 0 Then
                    prevC -= 1
                End If
                pos = prevC
            End If
            Dim bothC = pattern.IndexOf(String.Concat("$^", flag))
            If bothC >= 0 Then
                If bothC + 3 < pattern.Length Then size += 1
                If bothC > 0 Then
                    size += 1
                    bothC -= 1
                End If
                pos = bothC
            End If

            If Not pos = -1 Then pattern = pattern.Remove(pos, size)
        End If
        Return pattern
    End Function

    Private Sub btnBackgroundColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackgroundColor.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnBackgroundColor.BackColor = .Color
                    Me.CreateDummyMoviePreview()
                End If
            End If
        End With
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If bwCreateDummyFile.IsBusy OrElse bwCreateMediaStub.IsBusy OrElse bwMovieScraper.IsBusy Then
            bwCreateDummyFile.CancelAsync()
            bwCreateMediaStub.CancelAsync()
            bwMovieScraper.CancelAsync()
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CleanUp()
            Me.Close()
        End If
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        SetControlsEnabled(False)

        If rbHolderTypeDummyMovie.Checked Then
            Dim Movie As Database.DBElement = tMovieList.Item(0)
            'Need to avoid cross thread in BackgroundWorker
            'txtTopPos = Video_Width / (pbPreview.Image.Width / Convert.ToSingle(currTopText)) ' ... and Scale it
            Me.pbProgressSingle.Value = 100
            Me.pbProgressSingle.Style = ProgressBarStyle.Marquee
            Me.pbProgressSingle.MarqueeAnimationSpeed = 25
            Me.pbProgressSingle.Visible = True
            lvStatusSingle.Items.Clear()
            Me.bwCreateDummyFile = New System.ComponentModel.BackgroundWorker
            Me.bwCreateDummyFile.WorkerReportsProgress = True
            Me.bwCreateDummyFile.WorkerSupportsCancellation = True
            Me.bwCreateDummyFile.RunWorkerAsync(New Arguments With {.dMovie = Movie})
        ElseIf rbHolderTypeMediaStub.Checked AndAlso rbModeSingle.Checked Then
            CreateMediaStub(tMovieList.Item(0))
        ElseIf rbHolderTypeMediaStub.Checked AndAlso rbModeBatch.Checked Then
            For Each Movie In tMovieList
                CreateMediaStub(Movie)
            Next
        End If
    End Sub

    Private Sub btnDefaultsLoad_Click(ByVal sender As Object, e As EventArgs) Handles btnDefaultsLoad.Click
        DefaultsLoad()
    End Sub

    Private Sub btnDefaultsSave_Click(sender As Object, e As EventArgs) Handles btnDefaultsSave.Click
        DefaultsSave()
    End Sub

    Private Sub btnFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFont.Click
        With Me.fdFont
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If .Font IsNot Nothing Then
                    Me.drawFont = .Font
                    Me.CreateDummyMoviePreview()
                End If
            End If
        End With
    End Sub

    Private Sub btnLoadCollectionXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadCollectionXML.Click
        Dim isMulti As Boolean = False

        Try
            Select Case True
                Case rbModeBatch.Checked
                    isMulti = True
                Case rbModeSingle.Checked
                    isMulti = False
            End Select

            With ofdLoadXML
                .Filter = "Collection.xml (*.xml)|*.xml"
                .FilterIndex = 0
            End With

            If ofdLoadXML.ShowDialog() = DialogResult.OK Then
                Using dDVDProfilerSelect As New dlgDVDProfilerSelect
                    Select Case dDVDProfilerSelect.ShowDialog(ofdLoadXML.FileName, isMulti)
                        Case Windows.Forms.DialogResult.OK
                            ResetManager()
                            cMovieList = dDVDProfilerSelect.Results
                            If isMulti Then
                                For Each Movie In cMovieList
                                    tMovieList.Add(EmberAPI.DVDProfiler.MergeToDBMovie(Movie))
                                Next
                                AddMoviesToStatusList()
                                CheckConditionsBatch()
                            Else
                                AddDVDProfilerCollectionMovie(cMovieList)
                            End If
                        Case Windows.Forms.DialogResult.Abort
                    End Select
                End Using
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub AddMoviesToStatusList()

        For Each listItem As ListViewItem In lvStatusBatch.Items
            listItem.Remove()
        Next

        Dim index As Integer = 0
        For Each Movie As Database.DBElement In tMovieList
            Dim Item As New ListViewItem
            index = lvStatusBatch.Items.Add(Movie.Movie.Title.ToString).Index
            lvStatusBatch.Items(index).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
            lvStatusBatch.Items(index).UseItemStyleForSubItems = False
            lvStatusBatch.Items(index).SubItems(1).ForeColor = Color.Red
            index = index + 1
        Next

        'idxStsSource = lvStatusSingle.Items.Add(Master.eLang.GetString(318, "Source Folder")).Index
        'lvStatusSingle.Items(idxStsSource).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
        'lvStatusSingle.Items(idxStsSource).UseItemStyleForSubItems = False
        'lvStatusSingle.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
        'idxStsMovie = lvStatusSingle.Items.Add(Master.eLang.GetString(519, "Movie (Folder Name)")).Index
        'lvStatusSingle.Items(idxStsMovie).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
        'lvStatusSingle.Items(idxStsMovie).UseItemStyleForSubItems = False
        'lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
        'idxStsImage = lvStatusSingle.Items.Add(Master.eLang.GetString(523, "Place Holder Image")).Index
        'lvStatusSingle.Items(idxStsImage).SubItems.Add(Master.eLang.GetString(195, "Valid"))
        'lvStatusSingle.Items(idxStsImage).UseItemStyleForSubItems = False
        'lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
    End Sub

    Private Sub btnLoadSingleXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadSingleXML.Click
        Dim tMovie As Database.DBElement

        Try
            With ofdLoadXML
                .Filter = "Single.xml (*.xml)|*.xml"
                .FilterIndex = 0
            End With

            If ofdLoadXML.ShowDialog() = DialogResult.OK Then
                ResetManager()
                tMovie = LoadSingleXML(ofdLoadXML.FileName)
                AddDVDProfilerMovie(tMovie)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub btnSaveOfflineFolderName_Click(sender As Object, e As EventArgs) Handles btnSaveOfflineFolderName.Click
        Dim index As Integer
        Dim ChangedItem As New Database.DBElement
        If Not lvStatusBatch.SelectedItems.Count = 0 Then
            index = lvStatusBatch.SelectedItems.Item(0).Index
            ChangedItem = tMovieList.Item(index)
            ChangedItem.OfflineHolderFoldername = Me.txtFolderNameDVDProfiler.Text
            tMovieList.Item(index) = ChangedItem
            CheckConditionsBatch()
        End If
    End Sub
    Private Sub btnSearchBatch_Click(sender As Object, e As EventArgs) Handles btnSearchBatch.Click
        If rbScraperTypeManually.Checked Then
            For index As Integer = 0 To tMovieList.Count - 1
                Dim ScrapedMovie As New Database.DBElement
                ScrapedMovie = tMovieList.Item(index)
                ScrapedMovie.Movie.ClearForOfflineHolder()
                ScrapedMovie = SearchMovieManually(ScrapedMovie)
                tMovieList.Item(index) = ScrapedMovie
            Next
            gbHolderType.Enabled = True
            rbHolderTypeMediaStub.Checked = True
        ElseIf rbScraperTypeAsk.Checked Then

            Me.gbScraperType.Enabled = False

            Me.pbProgressBatch.Value = 0
            Me.pbProgressBatch.Visible = True

            If tMovieList.Count > 1 Then
                Me.pbProgressBatch.Style = ProgressBarStyle.Continuous
                Me.pbProgressBatch.Maximum = tMovieList.Count
                Me.pbProgressBatch.Value = 1
            Else
                Me.pbProgressBatch.Maximum = 100
                Me.pbProgressBatch.Style = ProgressBarStyle.Marquee
            End If

            Me.bwMovieScraper = New System.ComponentModel.BackgroundWorker
            Me.bwMovieScraper.WorkerReportsProgress = True
            Me.bwMovieScraper.WorkerSupportsCancellation = True
            Me.bwMovieScraper.RunWorkerAsync()
        End If
    End Sub

    Private Sub btnSearchSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSingle.Click

        If rbTypeMovieTitle.Checked Then
            tMovieList.Item(0).Movie.Clear()
            tMovieList.Item(0).Movie.Title = txtFolderNameMovieTitle.Text
            tMovieList.Item(0) = SearchMovieManually(tMovieList.Item(0))
            gbHolderType.Enabled = True
        Else
            tMovieList.Item(0).Movie.Clear()
            tMovieList.Item(0).Movie.Title = tMovieList.Item(0).DVDProfilerTitle
            tMovieList.Item(0) = SearchMovieManually(tMovieList.Item(0))
            gbHolderType.Enabled = True
        End If
    End Sub

    Private Sub btnTextColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTextColor.Click
        With Me.cdColor
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                If Not .Color = Nothing Then
                    Me.btnTextColor.BackColor = .Color
                    Me.CreateDummyMoviePreview()
                End If
            End If
        End With
    End Sub

    Private Sub bwCreateDummyFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCreateDummyFile.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)

        Dim buildPath As String = Path.Combine(WorkingPath, "Temp")
        Dim imgTemp As Bitmap
        Dim imgFinal As Bitmap
        Dim newGraphics As Graphics
        Me.bwCreateDummyFile.ReportProgress(0, Master.eLang.GetString(501, "Preparing Data"))
        If Directory.Exists(buildPath) Then
            FileUtils.Delete.DeleteDirectory(buildPath)
        End If
        Directory.CreateDirectory(buildPath)

        If Preview IsNot Nothing Then
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
        Me.bwCreateDummyFile.ReportProgress(1, Master.eLang.GetString(502, "Creating Movie"))
        'Let's cycle
        Dim f As Integer = 1
        For c As Integer = Video_Width To Convert.ToInt32(-stringSize.Width) Step -2
            imgTemp = New Bitmap(imgFinal)
            newGraphics = Graphics.FromImage(imgTemp)
            drawPoint.X = c
            newGraphics.Save()

            If chkUseBackground.Checked AndAlso chkUseFanart.Checked Then
                newGraphics.FillRectangle(backgroundBrush, 0, txtTopPos - 5, imgTemp.Width, stringSize.Height + 10)
            End If
            newGraphics.DrawString(drawString, drawFont, drawBrush, drawPoint)
            If chkUseOverlay.Checked AndAlso chkUseFanart.Checked Then
                newGraphics.DrawImage(Overlay.Image, 0, Convert.ToUInt16(txtTopPos - 65 / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Width / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Height / (1920 / Video_Width)))
            End If

            imgTemp.Save(Path.Combine(buildPath, String.Format("image{0}.jpg", f)), System.Drawing.Imaging.ImageFormat.Jpeg)
            newGraphics.Dispose()
            imgTemp.Dispose()
            f += 1
        Next
        imgFinal.Dispose()
        If Me.bwCreateDummyFile.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateDummyFile.ReportProgress(2, Master.eLang.GetString(503, "Building Movie"))
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
            ffmpeg.StartInfo.Arguments = String.Format(" -r 25 -an -i ""{0}\image%d.jpg"" -q:v 1 -b:v 40000k ""{1}""", buildPath, Args.dMovie.Filename)
            ffmpeg.Start()
            ffmpeg.WaitForExit()
            ffmpeg.Close()
        End Using

        If Me.bwCreateDummyFile.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateDummyFile.ReportProgress(4, Master.eLang.GetString(505, "Moving Files"))
        If Directory.Exists(buildPath) Then
            FileUtils.Delete.DeleteDirectory(buildPath)
        End If

        DirectoryCopy(WorkingPath, destPath)

        Args.dMovie.Filename = Path.Combine(destPath, String.Concat(MovieName, ".avi"))

        If Not String.IsNullOrEmpty(Args.dMovie.Movie.Title) Then
            Dim tTitle As String = StringUtils.SortTokens_Movie(Args.dMovie.Movie.Title)

            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(Args.dMovie.Movie.Year) Then
                Args.dMovie.ListTitle = String.Format("{0} ({1})", tTitle, Args.dMovie.Movie.Year)
            Else
                Args.dMovie.ListTitle = tTitle
            End If
        Else
            Args.dMovie.ListTitle = MovieName.Replace("[Offline]", String.Empty).Trim
        End If


        If Me.bwCreateDummyFile.CancellationPending Then
            e.Cancel = True
            Return
        End If
        Me.bwCreateDummyFile.ReportProgress(5, Master.eLang.GetString(361, "Finished"))
    End Sub

    Private Sub bwCreateDummyFile_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwCreateDummyFile.ProgressChanged
        If lvStatusSingle.Items.Count > 0 Then
            lvStatusSingle.Items(lvStatusSingle.Items.Count - 1).SubItems.Add(Master.eLang.GetString(362, "Done"))
        End If
        lvStatusSingle.Items.Add(e.UserState.ToString)
    End Sub

    Private Sub bwCreateDummyFile_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwCreateDummyFile.RunWorkerCompleted
        Me.pbProgressSingle.Visible = False
        'Me.EditMovie()
        Me.Close()
    End Sub

    Private Sub bwMovieScraper_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwMovieScraper.DoWork

        For index As Integer = 0 To tMovieList.Count - 1
            Dim ScrapedMovie As New Database.DBElement
            ScrapedMovie = tMovieList.Item(index)
            ScrapedMovie.Movie.ClearForOfflineHolder()

            Dim Banner As New MediaContainers.Image
            Dim ClearArt As New MediaContainers.Image
            Dim ClearLogo As New MediaContainers.Image
            Dim DiscArt As New MediaContainers.Image
            Dim Fanart As New MediaContainers.Image
            Dim Landscape As New MediaContainers.Image
            Dim Poster As New MediaContainers.Image
            Dim aUrlList As New List(Of Trailers)
            Dim efList As New List(Of String)
            Dim etList As New List(Of String)
            Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet

            Try
                chkUseFanart.Checked = False
                Me.CleanUp()
                fPath = String.Empty
                'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
                Dim ScrapeModifier As New Structures.ScrapeModifier
                Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)

                If Not ModulesManager.Instance.ScrapeData_Movie(ScrapedMovie, ScrapeModifier, Enums.ScrapeType.FullAsk, Master.DefaultOptions_Movie, False) Then
                    If rbTypeMovieTitle.Checked Then
                        Me.txtFolderNameMovieTitle.Text = String.Format("{0} [OffLine]", ScrapedMovie.Movie.Title)
                    End If
                End If

                If ScrapeModifier.MainPoster Then
                    Poster.Clear()
                    If Poster.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainPoster) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.Posters.Count > 0 AndAlso Images.GetPreferredMoviePoster(aContainer.Posters, Poster) Then
                                If Not String.IsNullOrEmpty(Poster.URL) Then
                                    ScrapedMovie.PosterPath = ":" & Poster.URL
                                End If
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainFanart Then
                    Fanart.Clear()
                    efList.Clear()
                    etList.Clear()
                    If Fanart.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainFanart) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.Fanarts.Count > 0 AndAlso Images.GetPreferredMovieFanart(aContainer.Fanarts, Fanart) Then
                                If Not String.IsNullOrEmpty(Fanart.URL) Then
                                    ScrapedMovie.FanartPath = ":" & Fanart.URL
                                End If
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainBanner Then
                    Banner.Clear()
                    If Banner.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainBanner) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.Banners.Count > 0 Then Banner = aContainer.Banners.Item(0) 'AndAlso Images.GetPreferredBanner(aList, Banner) Then
                            If Not String.IsNullOrEmpty(Banner.URL) Then
                                ScrapedMovie.BannerPath = ":" & Banner.URL
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainLandscape Then
                    Landscape.Clear()
                    If Landscape.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainLandscape) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.Landscapes.Count > 0 Then Landscape = aContainer.Landscapes.Item(0) 'AndAlso Images.GetPreferredLandscape(aList, Landscape) Then
                            If Not String.IsNullOrEmpty(Landscape.URL) Then
                                ScrapedMovie.LandscapePath = ":" & Landscape.URL
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainClearArt Then
                    ClearArt.Clear()
                    If ClearArt.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainClearArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.ClearArts.Count > 0 Then ClearArt = aContainer.ClearArts.Item(0) 'AndAlso Images.GetPreferredClearArt(aList, ClearArt) Then
                            If Not String.IsNullOrEmpty(ClearArt.URL) Then
                                ScrapedMovie.ClearArtPath = ":" & ClearArt.URL
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainClearLogo Then
                    ClearLogo.Clear()
                    If ClearLogo.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainClearLogo) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.ClearLogos.Count > 0 Then ClearLogo = aContainer.ClearLogos.Item(0) 'AndAlso Images.GetPreferredClearLogo(aList, ClearLogo) Then
                            If Not String.IsNullOrEmpty(ClearLogo.URL) Then
                                ScrapedMovie.ClearLogoPath = ":" & ClearLogo.URL
                            End If
                        End If
                    End If
                End If

                If ScrapeModifier.MainDiscArt Then
                    DiscArt.Clear()
                    If DiscArt.WebImage.IsAllowedToDownload_Movie(ScrapedMovie, Enums.ModifierType.MainDiscArt) Then
                        If Not ModulesManager.Instance.ScrapeImage_Movie(ScrapedMovie, aContainer, ScrapeModifier, False) Then
                            If aContainer.DiscArts.Count > 0 Then DiscArt = aContainer.DiscArts.Item(0) 'AndAlso Images.GetPreferredDiscArt(aList, DiscArt) Then
                            If Not String.IsNullOrEmpty(DiscArt.URL) Then
                                ScrapedMovie.DiscArtPath = ":" & DiscArt.URL
                            End If
                        End If
                    End If
                End If

                tMovieList.Item(index) = ScrapedMovie
                Me.bwMovieScraper.ReportProgress(index + 1, Master.eLang.GetString(1153, "Scraped"))

            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
    End Sub

    Private Sub bwMovieScraper_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwMovieScraper.ProgressChanged
        lvStatusBatch.Items(e.ProgressPercentage - 1).SubItems(1).Text = Master.eLang.GetString(1153, "Scraped")
        lvStatusBatch.Items(e.ProgressPercentage - 1).SubItems(1).ForeColor = Color.Green

        Me.pbProgressBatch.Value = e.ProgressPercentage

        'If lvStatusBatch.Items.Count > 0 Then
        '    lvStatusBatch.Items(lvStatusBatch.Items.Count - 1).SubItems.Add(Master.eLang.GetString(362, "Done"))
        'End If
        'lvStatusBatch.Items(1).Text = ""
    End Sub

    Private Sub bwMovieScraper_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwMovieScraper.RunWorkerCompleted
        Me.pbProgressBatch.Visible = False

        Me.gbHolderType.Enabled = True
        Me.rbHolderTypeMediaStub.Checked = True
        Me.gbScraperType.Enabled = True
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
        If Not chkUseFanart.Checked AndAlso Preview IsNot Nothing Then
            If Video_Aspect = "Wide" Then
                txtTop.Text = Convert.ToUInt16(Preview.Height - (167 / (1920 / Video_Width)) - (textHeight.Height / 2)).ToString
            Else
                txtTop.Text = Convert.ToUInt16(Preview.Height - 90 - (textHeight.Height / 2)).ToString
            End If
        End If
        CreateDummyMoviePreview()
    End Sub

    Private Sub cbSources_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSources.SelectedIndexChanged
        If Not String.IsNullOrEmpty(cbSources.Text) Then
            For Each Movie In tMovieList
                Movie.Source = cbSources.Text
            Next
            If cbSources.SelectedIndex >= 0 Then
                gbMode.Enabled = True
            Else
                gbMode.Enabled = False
            End If
        End If
    End Sub

    Private Sub CheckConditionsBatch()
        Dim Fanart As New Images
        Try

            Dim index As Integer = 0
            Dim NewMovieFoldername As String = String.Empty
            For Each Movie As Database.DBElement In tMovieList
                If Movie.OfflineHolderFoldername.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
                    NewMovieFoldername = FileUtils.Common.MakeValidFilename(Movie.OfflineHolderFoldername)
                Else
                    NewMovieFoldername = FileUtils.Common.MakeValidFilename(Movie.OfflineHolderFoldername)
                    For Each Invalid As Char In Path.GetInvalidPathChars
                        NewMovieFoldername = NewMovieFoldername.Replace(Invalid, String.Empty)
                    Next
                End If

                If cbSources.SelectedIndex >= 0 Then
                    Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources WHERE Name = """, cbSources.SelectedItem.ToString, """;")
                        Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                            If SQLReader.Read Then
                                If Directory.GetDirectories(SQLReader("Path").ToString).Count = 0 Then
                                    destPath = SQLReader("Path").ToString
                                Else
                                    destPath = Path.Combine(SQLReader("Path").ToString, NewMovieFoldername)
                                End If
                            End If
                        End Using
                    End Using
                End If

                If Not NewMovieFoldername = String.Empty Then
                    If Directory.Exists(destPath) Then
                        lvStatusBatch.Items(index).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
                        lvStatusBatch.Items(index).SubItems(1).ForeColor = Color.Red
                    Else
                        lvStatusBatch.Items(index).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                        lvStatusBatch.Items(index).SubItems(1).ForeColor = Color.Green
                    End If
                Else
                    lvStatusBatch.Items(index).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                    lvStatusBatch.Items(index).SubItems(1).ForeColor = Color.Red
                End If
                index = index + 1
            Next

            'If rbTypeMovieTitle.Checked Then
            '    If txtFolderNameMovieTitle.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
            '        MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameMovieTitle.Text)
            '        'tMovieList.Item(0).Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            '    Else
            '        MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameMovieTitle.Text)
            '        For Each Invalid As Char In Path.GetInvalidPathChars
            '            MovieName = MovieName.Replace(Invalid, String.Empty)
            '        Next
            '        'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            '    End If
            'Else
            '    If txtFolderNameDVDProfiler.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
            '        MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameDVDProfiler.Text)
            '        'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            '    Else
            '        MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameDVDProfiler.Text)
            '        For Each Invalid As Char In Path.GetInvalidPathChars
            '            MovieName = MovieName.Replace(Invalid, String.Empty)
            '        Next
            '        'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
            '    End If
            'End If

            'If cbSources.SelectedIndex >= 0 Then
            '    Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
            '        SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources WHERE Name = """, cbSources.SelectedItem.ToString, """;")
            '        Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
            '            If SQLReader.Read Then
            '                If Directory.GetDirectories(SQLReader("Path").ToString).Count = 0 Then
            '                    destPath = SQLReader("Path").ToString
            '                Else
            '                    destPath = Path.Combine(SQLReader("Path").ToString, MovieName)
            '                End If
            '                lvStatusBatch.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
            '                lvStatusBatch.Items(idxStsSource).SubItems(1).ForeColor = Color.Green
            '            End If
            '        End Using
            '    End Using
            'Else
            '    lvStatusSingle.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
            '    lvStatusSingle.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            'End If


            'If rbTypeMovieTitle.Checked Then
            '    If Not txtFolderNameMovieTitle.Text = String.Empty Then
            '        If Directory.Exists(destPath) Then
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            '        Else
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Green
            '        End If
            '    Else
            '        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
            '        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            '    End If
            'Else
            '    If Not txtFolderNameDVDProfiler.Text = String.Empty Then
            '        If Directory.Exists(destPath) Then
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            '        Else
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
            '            lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Green
            '        End If
            '    Else
            '        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
            '        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            '    End If
            'End If

            'If Not String.IsNullOrEmpty(fPath) AndAlso rbHolderTypeDummyMovie.Checked Then
            '    chkUseFanart.Enabled = True
            'Else
            '    chkUseFanart.Checked = False
            '    chkUseFanart.Enabled = False
            'End If

            'If chkUseFanart.Checked Then
            '    If Not String.IsNullOrEmpty(fPath) Then
            '        SetPreview(False, fPath)
            '        lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
            '        lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
            '    Else
            '        lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
            '        lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Red
            '    End If
            'Else
            '    lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
            '    lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
            'End If

            'If rbHolderTypeDummyMovie.Checked Then
            '    Me.CreateDummyMoviePreview()
            'End If

            If Not Me.pbProgressBatch.Visible Then
                'btnCreate.Enabled = TrueThen
                gbScraperType.Enabled = True
                If Not lvStatusBatch.Items.Count > 0 Then
                    gbScraperType.Enabled = False
                End If
                For Each i As ListViewItem In lvStatusBatch.Items
                    If Not i.SubItems(1).ForeColor = Color.Green Then
                        'btnCreate.Enabled = False
                        gbScraperType.Enabled = False
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
        End Try

        Fanart.Dispose()
        Fanart = Nothing
    End Sub

    Private Sub CheckConditionsSingle()
        Dim Fanart As New Images
        Try
            If rbTypeMovieTitle.Checked Then
                If txtFolderNameMovieTitle.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
                    MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameMovieTitle.Text)
                    'tMovieList.Item(0).Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                Else
                    MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameMovieTitle.Text)
                    For Each Invalid As Char In Path.GetInvalidPathChars
                        MovieName = MovieName.Replace(Invalid, String.Empty)
                    Next
                    'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                End If
            Else
                If txtFolderNameDVDProfiler.Text.IndexOfAny(Path.GetInvalidPathChars) <= 0 Then
                    MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameDVDProfiler.Text)
                    'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                Else
                    MovieName = FileUtils.Common.MakeValidFilename(txtFolderNameDVDProfiler.Text)
                    For Each Invalid As Char In Path.GetInvalidPathChars
                        MovieName = MovieName.Replace(Invalid, String.Empty)
                    Next
                    'tMovie.Filename = Path.Combine(WorkingPath, String.Concat(MovieName, ".avi"))
                End If
            End If

            If cbSources.SelectedIndex >= 0 Then
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT Path FROM Sources WHERE Name = """, cbSources.SelectedItem.ToString, """;")
                    Using SQLReader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        If SQLReader.Read Then
                            If Directory.GetDirectories(SQLReader("Path").ToString).Count = 0 Then
                                destPath = SQLReader("Path").ToString
                            Else
                                destPath = Path.Combine(SQLReader("Path").ToString, MovieName)
                            End If
                            lvStatusSingle.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                            lvStatusSingle.Items(idxStsSource).SubItems(1).ForeColor = Color.Green
                        End If
                    End Using
                End Using
            Else
                lvStatusSingle.Items(idxStsSource).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                lvStatusSingle.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            End If


            If rbTypeMovieTitle.Checked Then
                If Not txtFolderNameMovieTitle.Text = String.Empty Then
                    If Directory.Exists(destPath) Then
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
                    Else
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Green
                    End If
                Else
                    lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                    lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
                End If
            Else
                If Not txtFolderNameDVDProfiler.Text = String.Empty Then
                    If Directory.Exists(destPath) Then
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(355, "Exists")
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
                    Else
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                        lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Green
                    End If
                Else
                    lvStatusSingle.Items(idxStsMovie).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                    lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
                End If
            End If

            If Not String.IsNullOrEmpty(fPath) AndAlso rbHolderTypeDummyMovie.Checked Then
                chkUseFanart.Enabled = True
            Else
                chkUseFanart.Checked = False
                chkUseFanart.Enabled = False
            End If

            If chkUseFanart.Checked Then
                If Not String.IsNullOrEmpty(fPath) Then
                    SetPreview(False, fPath)
                    lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                    lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
                Else
                    lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(194, "Not Valid")
                    lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Red
                End If
            Else
                lvStatusSingle.Items(idxStsImage).SubItems(1).Text = Master.eLang.GetString(195, "Valid")
                lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green
            End If

            If rbHolderTypeDummyMovie.Checked Then
                Me.CreateDummyMoviePreview()
            End If

            If Not Me.pbProgressSingle.Visible Then
                'btnCreate.Enabled = True
                gbSearch.Enabled = True
                For Each i As ListViewItem In lvStatusSingle.Items
                    If Not i.SubItems(1).ForeColor = Color.Green Then
                        'btnCreate.Enabled = False
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

    Private Sub chkBackground_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseBackground.CheckedChanged
        Me.CreateDummyMoviePreview()
    End Sub

    Private Sub chkOverlay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseOverlay.CheckedChanged
        If chkUseOverlay.Checked Then
            Overlay.FromFile(OverlayPath)
        End If
        Me.CreateDummyMoviePreview()
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
        chkUseOverlay.Enabled = chkUseFanart.Checked
        chkUseBackground.Enabled = chkUseFanart.Checked
        If chkUseOverlay.Enabled Then
            chkUseOverlay.CheckState = CheckState.Checked
            chkUseBackground.CheckState = CheckState.Checked
        End If
        CreateDummyMoviePreview()
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub CreateMediaStub(ByVal d As Database.DBElement, Optional ByVal mTitle As String = "")
        Dim StubFile As String = String.Empty
        Dim StubPath As String = String.Empty
        Dim Title As String = String.Empty
        Dim Message As String = String.Empty

        Title = mTitle
        Message = txtTagline.Text

        Message = ProccessPattern(d, Message)

        StubFile = String.Concat(destPath, Path.DirectorySeparatorChar, FileUtils.Common.MakeValidFilename(d.DVDProfilerTitle), If(Not String.IsNullOrEmpty(d.VideoSource), String.Concat(".", d.VideoSource.ToLower), String.Empty), ".disc")
        d.Filename = StubFile

        MediaStub.SaveDiscStub(StubFile, Title, Message)

        Me.EditMovie(d)
        Me.Close()
    End Sub

    Private Sub CreateDummyMoviePreview()
        If Preview Is Nothing Then Return
        Dim bmCloneOriginal As Bitmap = New Bitmap(Preview)

        If chkUseFanart.Checked AndAlso chkUseOverlay.Checked Then
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
        If chkUseBackground.Checked AndAlso chkUseFanart.Checked Then
            grOriginal.FillRectangle(backgroundBrush, 0, txtTopPos - 5, bmCloneOriginal.Width, drawFont.Height + 10)
        End If
        grOriginal.DrawString(txtTagline.Text, drawFont, drawBrush, iLeft, txtTopPos)
        If chkUseOverlay.Checked AndAlso chkUseFanart.Checked Then
            grOriginal.DrawImage(Overlay.Image, 0, Convert.ToUInt16(txtTopPos - 65 / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Width / (1920 / Video_Width)), Convert.ToUInt16(Overlay.Image.Height / (1920 / Video_Width)))
        End If
        grPreview.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        grPreview.DrawImage(bmCloneOriginal, New Rectangle(0, 0, pbPreview.Width, pbPreview.Height), New Rectangle(0, 0, bmCloneOriginal.Width, bmCloneOriginal.Height), GraphicsUnit.Pixel)

        If chkUseFanart.Checked AndAlso chkUseOverlay.Checked Then
            tbTagLine.Location = New Point(tbTagLine.Location.X, Convert.ToInt32((gbPreview.Location.Y + pbPreview.Location.Y) - 13 + ((textHeight.Height / (RealImage_H / pbPreview.Height)) / 2) + (65 / (1920 / Video_Width) / (RealImage_H / pbPreview.Height))))
            tbTagLine.Height = Convert.ToInt32(pbPreview.Height + 26 - (textHeight.Height / (RealImage_H / pbPreview.Height)) - (65 / (1920 / Video_Width) / (RealImage_H / pbPreview.Height)))
        Else
            tbTagLine.Location = New Point(tbTagLine.Location.X, Convert.ToInt32((gbPreview.Location.Y + pbPreview.Location.Y) - 13 + ((textHeight.Height / (RealImage_H / pbPreview.Height)) / 2)))
            tbTagLine.Height = Convert.ToInt32(pbPreview.Height + 26 - (textHeight.Height / (RealImage_H / pbPreview.Height)))
        End If
    End Sub

    Private Sub EditMovie(ByRef tMovie As Database.DBElement)

        Master.currMovie = tMovie

        Using dEditMovie As New dlgEditMovie
            Select Case dEditMovie.ShowDialog()
                Case Windows.Forms.DialogResult.OK
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterEdit_Movie, Nothing, Master.currMovie)
                Case Windows.Forms.DialogResult.Retry
                Case Windows.Forms.DialogResult.Abort
                Case Else
                    'If Me.InfoCleared Then Me.LoadInfo(ID, Me.dgvMovies.Item(1, indX).Value.ToString, True, False)
            End Select
        End Using
    End Sub

    Private Sub DefaultsLoad()
        If rbHolderTypeDummyMovie.Checked Then
            cbFormat.SelectedIndex = Master.eSettings.OMMDummyFormat
            txtTagline.Text = Master.eSettings.OMMDummyTagline
            txtTop.Text = Master.eSettings.OMMDummyTop
            chkUseFanart.Checked = Master.eSettings.OMMDummyUseFanart
            chkUseBackground.Checked = Master.eSettings.OMMDummyUseBackground
            chkUseOverlay.Checked = Master.eSettings.OMMDummyUseOverlay
        ElseIf rbHolderTypeMediaStub.Checked Then
            txtTagline.Text = Master.eSettings.OMMMediaStubTagline
        End If
    End Sub

    Private Sub DefaultsSave()
        If rbHolderTypeDummyMovie.Checked Then
            Master.eSettings.OMMDummyFormat = cbFormat.SelectedIndex
            Master.eSettings.OMMDummyTagline = txtTagline.Text
            Master.eSettings.OMMDummyTop = txtTop.Text
            Master.eSettings.OMMDummyUseFanart = chkUseFanart.Checked
            Master.eSettings.OMMDummyUseBackground = chkUseBackground.Checked
            Master.eSettings.OMMDummyUseOverlay = chkUseOverlay.Checked
            Master.eSettings.Save()
        ElseIf rbHolderTypeMediaStub.Checked Then
            Master.eSettings.OMMMediaStubTagline = txtTagline.Text
            Master.eSettings.Save()
        End If
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
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

            'load all the movie sources from settings
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
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

            'CreateDummyMoviePreview()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub dlgOfflineHolder_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
    End Sub

    Private Function LoadSingleXML(ByVal fPath As String) As Database.DBElement
        Dim xmlSer As XmlSerializer = Nothing
        Dim cMovie As DVDProfiler.cDVD
        Dim tMovie As New Database.DBElement

        Try
            If File.Exists(fPath) Then
                Using xmlSR As StreamReader = New StreamReader(fPath)
                    xmlSer = New XmlSerializer(GetType(DVDProfiler.cDVD))
                    cMovie = DirectCast(xmlSer.Deserialize(xmlSR), DVDProfiler.cDVD)
                    tMovie = DVDProfiler.MergeToDBMovie(cMovie)
                End Using
            End If
            Return tMovie
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return Nothing
    End Function

    Private Sub lvStatusBatch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStatusBatch.SelectedIndexChanged
        Dim index As Integer
        If Not lvStatusBatch.SelectedItems.Count = 0 Then
            index = lvStatusBatch.SelectedItems.Item(0).Index
            DisplaySelectedMovie(tMovieList.Item(index))
        End If
    End Sub

    Private Sub lvStatusSingle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvStatusSingle.SelectedIndexChanged
        'no selection in here please :)
        lvStatusSingle.SelectedItems.Clear()
    End Sub

    Public Function ProccessPattern(ByVal d As Database.DBElement, ByVal sMessage As String) As String
        Try
            Dim pattern As String = sMessage
            Dim message As String = sMessage

            Dim nextC = pattern.IndexOf("$")
            Dim nextIB = pattern.IndexOf("{")
            Dim nextEB = pattern.IndexOf("}")
            Dim strCond As String
            Dim strBase As String
            Dim strNoFlags As String
            While Not nextC = -1
                If nextC > nextIB AndAlso nextC < nextEB AndAlso Not nextC = -1 AndAlso Not nextIB = -1 AndAlso Not nextEB = -1 Then
                    strCond = pattern.Substring(nextIB, nextEB - nextIB + 1)
                    strNoFlags = strCond
                    strBase = strCond
                    strCond = ApplyPattern(strCond, "C", d.DVDProfilerCaseType)
                    strCond = ApplyPattern(strCond, "L", d.DVDProfilerLocation)
                    strCond = ApplyPattern(strCond, "M", d.DVDProfilerMediaType)
                    strCond = ApplyPattern(strCond, "S", d.DVDProfilerSlot)
                    strCond = ApplyPattern(strCond, "T", d.DVDProfilerTitle)
                    strNoFlags = Regex.Replace(strNoFlags, "\$((?:[CLMST]?))", String.Empty)
                    If strCond.Trim = strNoFlags.Trim Then
                        strCond = String.Empty
                    Else
                        strCond = strCond.Substring(1, strCond.Length - 2)
                    End If
                    pattern = pattern.Replace(strBase, strCond)
                    nextC = pattern.IndexOf("$")
                Else
                    nextC = pattern.IndexOf("$", nextC + 1)
                End If
                nextIB = pattern.IndexOf("{")
                nextEB = pattern.IndexOf("}")
            End While
            pattern = ApplyPattern(pattern, "C", d.DVDProfilerCaseType)
            pattern = ApplyPattern(pattern, "L", d.DVDProfilerLocation)
            pattern = ApplyPattern(pattern, "M", d.DVDProfilerMediaType)
            pattern = ApplyPattern(pattern, "S", d.DVDProfilerSlot)
            pattern = ApplyPattern(pattern, "T", d.DVDProfilerTitle)

            Return pattern.Trim
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return String.Empty
        End Try
    End Function

    Private Sub rbHolderTypeDummyMovie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHolderTypeDummyMovie.CheckedChanged
        If gbHolderType.Enabled Then
            SetControlsDummyMovie(True)
            DefaultsLoad()
        End If
        CheckConditionsSingle()
    End Sub

    Private Sub rbHolderTypeMediaStub_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHolderTypeMediaStub.CheckedChanged
        If gbHolderType.Enabled Then
            SetControlsDummyMovie(False)
            DefaultsLoad()
        End If
        CheckConditionsSingle()
    End Sub

    Private Sub rbModeBatch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbModeBatch.CheckedChanged
        If gbMode.Enabled AndAlso rbModeBatch.Checked Then
            ResetManager()
            gbInfoBatch.Visible = True
            gbInfoSingle.Visible = False
            gbScraperType.Visible = True
            gbSearch.Visible = False
            gbType.Enabled = True
            rbTypeDVDProfiler.Enabled = True
            rbTypeMovieTitle.Enabled = False
            rbTypeDVDProfiler.Checked = True

            For Each listItem As ListViewItem In lvStatusBatch.Items
                listItem.Remove()
            Next

            CheckConditionsBatch()
        End If
    End Sub

    Private Sub rbModeSingle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbModeSingle.CheckedChanged
        If gbMode.Enabled AndAlso rbModeSingle.Checked Then
            ResetManager()
            gbInfoSingle.Visible = True
            gbInfoBatch.Visible = False
            gbSearch.Visible = True
            gbScraperType.Visible = False
            gbType.Enabled = True
            rbTypeDVDProfiler.Enabled = True
            rbTypeMovieTitle.Enabled = True

            For Each listItem As ListViewItem In lvStatusSingle.Items
                listItem.Remove()
            Next

            idxStsSource = lvStatusSingle.Items.Add(Master.eLang.GetString(318, "Source Folder")).Index
            lvStatusSingle.Items(idxStsSource).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
            lvStatusSingle.Items(idxStsSource).UseItemStyleForSubItems = False
            lvStatusSingle.Items(idxStsSource).SubItems(1).ForeColor = Color.Red
            idxStsMovie = lvStatusSingle.Items.Add(Master.eLang.GetString(519, "Movie (Folder Name)")).Index
            lvStatusSingle.Items(idxStsMovie).SubItems.Add(Master.eLang.GetString(194, "Not Valid"))
            lvStatusSingle.Items(idxStsMovie).UseItemStyleForSubItems = False
            lvStatusSingle.Items(idxStsMovie).SubItems(1).ForeColor = Color.Red
            idxStsImage = lvStatusSingle.Items.Add(Master.eLang.GetString(523, "Place Holder Image")).Index
            lvStatusSingle.Items(idxStsImage).SubItems.Add(Master.eLang.GetString(195, "Valid"))
            lvStatusSingle.Items(idxStsImage).UseItemStyleForSubItems = False
            lvStatusSingle.Items(idxStsImage).SubItems(1).ForeColor = Color.Green


            CheckConditionsSingle()

        End If
    End Sub

    Private Sub rbTypeDVDProfiler_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTypeDVDProfiler.CheckedChanged
        If gbType.Enabled Then
            ResetManager()
            gbMovieTitle.Enabled = False
            gbDVDProfiler.Enabled = True
            txtFolderNameMovieTitle.Text = String.Empty
        End If
        CheckConditionsSingle()
    End Sub

    Private Sub rbTypeMovieTitle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTypeMovieTitle.CheckedChanged
        If gbType.Enabled Then
            ResetManager()
            gbMovieTitle.Enabled = True
            gbDVDProfiler.Enabled = False
        End If
        CheckConditionsSingle()
    End Sub
    Private Sub rbScraperTypeAuto_CheckedChanged(sender As Object, e As EventArgs) Handles rbScraperTypeAsk.CheckedChanged
        If gbScraperType.Enabled AndAlso rbScraperTypeAsk.Checked Then
            btnSearchBatch.Enabled = True
            gbHolderType.Enabled = False
        End If
    End Sub
    Private Sub rbScraperTypeManually_CheckedChanged(sender As Object, e As EventArgs) Handles rbScraperTypeManually.CheckedChanged
        If gbScraperType.Enabled AndAlso rbScraperTypeManually.Checked Then
            btnSearchBatch.Enabled = True
            gbHolderType.Enabled = False
        End If
    End Sub

    Private Sub ResetManager()
        chkUseFanart.Checked = False
        chkUseFanart.Enabled = False
        CleanUp()
        tMovieList.Clear()
        cMovieList.Clear()
        txtCaseType.Text = String.Empty
        txtDVDTitle.Text = String.Empty
        txtFolderNameDVDProfiler.Text = String.Empty
        txtFolderNameMovieTitle.Text = String.Empty
        txtLocation.Text = String.Empty
        txtMediaType.Text = String.Empty
        txtSlot.Text = String.Empty
    End Sub

    Private Function SearchMovieAsk(ByVal tMovie As Database.DBElement) As Database.DBElement
        Dim sMovie As Database.DBElement
        Dim Banner As New MediaContainers.Image
        Dim ClearArt As New MediaContainers.Image
        Dim ClearLogo As New MediaContainers.Image
        Dim DiscArt As New MediaContainers.Image
        Dim Fanart As New MediaContainers.Image
        Dim Landscape As New MediaContainers.Image
        Dim Poster As New MediaContainers.Image
        Dim aUrlList As New List(Of Trailers)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet

        sMovie = tMovie

        Try
            chkUseFanart.Checked = False
            Me.CleanUp()
            fPath = String.Empty
            'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)

            If Not ModulesManager.Instance.ScrapeData_Movie(sMovie, ScrapeModifier, Enums.ScrapeType.FullAsk, Master.DefaultOptions_Movie, False) Then
                If rbTypeMovieTitle.Checked Then
                    Me.txtFolderNameMovieTitle.Text = String.Format("{0} [OffLine]", sMovie.Movie.Title)
                End If
            End If

            If ScrapeModifier.MainPoster Then
                Poster.Clear()
                If Poster.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainPoster) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Posters.Count > 0 AndAlso Images.GetPreferredMoviePoster(aContainer.Posters, Poster) Then
                            If Not String.IsNullOrEmpty(Poster.URL) Then
                                sMovie.PosterPath = ":" & Poster.URL
                            End If
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainFanart Then
                Fanart.Clear()
                efList.Clear()
                etList.Clear()
                If Fanart.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainFanart) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Fanarts.Count > 0 AndAlso Images.GetPreferredMovieFanart(aContainer.Fanarts, Fanart) Then
                            If Not String.IsNullOrEmpty(Fanart.URL) Then
                                sMovie.FanartPath = ":" & Fanart.URL
                            End If
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainBanner Then
                Banner.Clear()
                If Banner.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainBanner) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Banners.Count > 0 Then Banner = aContainer.Banners.Item(0) 'AndAlso Images.GetPreferredBanner(aList, Banner) Then
                        If Not String.IsNullOrEmpty(Banner.URL) Then
                            sMovie.BannerPath = ":" & Banner.URL
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainLandscape Then
                Landscape.Clear()
                If Landscape.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainLandscape) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Landscapes.Count > 0 Then Landscape = aContainer.Landscapes.Item(0) 'AndAlso Images.GetPreferredLandscape(aList, Landscape) Then
                        If Not String.IsNullOrEmpty(Landscape.URL) Then
                            sMovie.LandscapePath = ":" & Landscape.URL
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainClearArt Then
                ClearArt.Clear()
                If ClearArt.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainClearArt) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.ClearArts.Count > 0 Then ClearArt = aContainer.ClearArts.Item(0) 'AndAlso Images.GetPreferredClearArt(aList, ClearArt) Then
                        If Not String.IsNullOrEmpty(ClearArt.URL) Then
                            sMovie.ClearArtPath = ":" & ClearArt.URL
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainClearLogo Then
                ClearLogo.Clear()
                If ClearLogo.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainClearLogo) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.ClearLogos.Count > 0 Then ClearLogo = aContainer.ClearLogos.Item(0) 'AndAlso Images.GetPreferredClearLogo(aList, ClearLogo) Then
                        If Not String.IsNullOrEmpty(ClearLogo.URL) Then
                            sMovie.ClearLogoPath = ":" & ClearLogo.URL
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainDiscArt Then
                DiscArt.Clear()
                If DiscArt.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainDiscArt) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.DiscArts.Count > 0 Then DiscArt = aContainer.DiscArts.Item(0) 'AndAlso Images.GetPreferredDiscArt(aList, DiscArt) Then
                        If Not String.IsNullOrEmpty(DiscArt.URL) Then
                            sMovie.DiscArtPath = ":" & DiscArt.URL
                        End If
                    End If
                End If
            End If

            Return sMovie

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return tMovie
    End Function

    Private Function SearchMovieManually(ByVal tMovie As Database.DBElement) As Database.DBElement
        Dim sMovie As Database.DBElement
        Dim Banner As New MediaContainers.Image
        Dim ClearArt As New MediaContainers.Image
        Dim ClearLogo As New MediaContainers.Image
        Dim DiscArt As New MediaContainers.Image
        Dim Fanart As New MediaContainers.Image
        Dim Landscape As New MediaContainers.Image
        Dim Poster As New MediaContainers.Image
        Dim aUrlList As New List(Of Trailers)
        Dim efList As New List(Of String)
        Dim etList As New List(Of String)
        Dim aContainer As New MediaContainers.SearchResultsContainer_Movie_MovieSet

        sMovie = tMovie

        Try
            chkUseFanart.Checked = False
            Me.CleanUp()
            fPath = String.Empty
            'Functions.SetScraperMod(Enums.ModType.DoSearch, True)
            Dim ScrapeModifier As New Structures.ScrapeModifier
            Functions.SetScrapeModifier(ScrapeModifier, Enums.ModifierType.All, True)

            If Not ModulesManager.Instance.ScrapeData_Movie(sMovie, ScrapeModifier, Enums.ScrapeType.SingleScrape, Master.DefaultOptions_Movie, False) Then
                If rbTypeMovieTitle.Checked Then
                    Me.txtFolderNameMovieTitle.Text = String.Format("{0} [OffLine]", sMovie.Movie.Title)
                End If
            End If

            If ScrapeModifier.MainPoster Then
                Poster.Clear()
                If Poster.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainPoster) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Posters.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainPoster, aContainer.Posters, etList, efList) = DialogResult.OK Then
                                    Poster = dImgSelect.Results
                                    sMovie.PosterPath = ":" & Poster.URL
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainFanart Then
                Fanart.Clear()
                efList.Clear()
                etList.Clear()
                If Fanart.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainFanart) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Fanarts.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainFanart, aContainer.Fanarts, efList, etList) = DialogResult.OK Then
                                    Fanart = dImgSelect.Results
                                    efList = dImgSelect.efList
                                    etList = dImgSelect.etList
                                    sMovie.FanartPath = ":" & Fanart.URL
                                    'sMovie.efList = efList
                                    'sMovie.etList = etList

                                    If Not String.IsNullOrEmpty(Fanart.URL) Then
                                        Fanart.WebImage.FromWeb(Fanart.URL)
                                    End If
                                    ' needs local fanart for dummy movie
                                    'If Fanart.WebImage.Image IsNot Nothing Then
                                    '    fPath = Fanart.WebImage.SaveAsMovieFanart(sMovie)
                                    'End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainBanner Then
                Banner.Clear()
                If Banner.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainBanner) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Banners.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainBanner, aContainer.Banners, efList, etList) = DialogResult.OK Then
                                    Banner = dImgSelect.Results
                                    sMovie.BannerPath = ":" & Banner.URL

                                    If Not String.IsNullOrEmpty(Banner.URL) Then
                                        Banner.WebImage.FromWeb(Banner.URL)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainLandscape Then
                Landscape.Clear()
                If Landscape.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainLandscape) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.Landscapes.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainLandscape, aContainer.Landscapes, efList, etList) = DialogResult.OK Then
                                    Landscape = dImgSelect.Results
                                    sMovie.LandscapePath = ":" & Landscape.URL

                                    If Not String.IsNullOrEmpty(Landscape.URL) Then
                                        Landscape.WebImage.FromWeb(Landscape.URL)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainClearArt Then
                ClearArt.Clear()
                If ClearArt.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainClearArt) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.ClearArts.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainClearArt, aContainer.ClearArts, efList, etList) = DialogResult.OK Then
                                    ClearArt = dImgSelect.Results
                                    sMovie.ClearArtPath = ":" & ClearArt.URL

                                    If Not String.IsNullOrEmpty(ClearArt.URL) Then
                                        ClearArt.WebImage.FromWeb(ClearArt.URL)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainClearLogo Then
                ClearLogo.Clear()
                If ClearLogo.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainClearLogo) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.ClearLogos.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainClearLogo, aContainer.ClearLogos, efList, etList) = DialogResult.OK Then
                                    ClearLogo = dImgSelect.Results
                                    sMovie.ClearLogoPath = ":" & ClearLogo.URL

                                    If Not String.IsNullOrEmpty(ClearLogo.URL) Then
                                        ClearLogo.WebImage.FromWeb(ClearLogo.URL)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            If ScrapeModifier.MainDiscArt Then
                DiscArt.Clear()
                If DiscArt.WebImage.IsAllowedToDownload_Movie(sMovie, Enums.ModifierType.MainDiscArt) Then
                    If Not ModulesManager.Instance.ScrapeImage_Movie(sMovie, aContainer, ScrapeModifier, False) Then
                        If aContainer.DiscArts.Count > 0 Then
                            Using dImgSelect As New dlgImgSelect()
                                If dImgSelect.ShowDialog(sMovie, Enums.ModifierType.MainDiscArt, aContainer.DiscArts, efList, etList) = DialogResult.OK Then
                                    DiscArt = dImgSelect.Results
                                    sMovie.DiscArtPath = ":" & DiscArt.URL

                                    If Not String.IsNullOrEmpty(DiscArt.URL) Then
                                        DiscArt.WebImage.FromWeb(DiscArt.URL)
                                    End If
                                End If
                            End Using
                        End If
                    End If
                End If
            End If

            Return sMovie

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return tMovie
    End Function

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
        btnFont.Enabled = isDummy
        btnTextColor.Enabled = isDummy
        txtTop.Enabled = isDummy
        btnBackgroundColor.Enabled = isDummy
        cbFormat.Enabled = isDummy
        chkUseFanart.Enabled = isDummy
        chkUseOverlay.Enabled = isDummy
        chkUseBackground.Enabled = isDummy

        If isDummy Then
            txtMovieTitle.Text = String.Empty
            lblTagline.Text = Master.eLang.GetString(542, "Place Holder Video Tagline:")
        Else
            txtMovieTitle.Text = tMovieList.Item(0).Movie.Title
            lblTagline.Text = "Message"
        End If
    End Sub

    Private Sub SetPreview(ByVal bDefault As Boolean, Optional ByVal path As String = "")
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
                If drawFont IsNot Nothing Then
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub SetUp()
        Me.Text = Master.eLang.GetString(524, "Offline Media Manager")
        Me.btnClose.Text = Master.eLang.GetString(19, "Close")
        Me.lblTopDetails.Text = Master.eLang.GetString(525, "Add Offline movie")
        Me.lblTopTitle.Text = Me.Text
        Me.lblMovie.Text = Master.eLang.GetString(527, "Place Holder Folder/Movie Name:")
        Me.btnSearchSingle.Text = Master.eLang.GetString(528, "Search Movie")
        Me.colCondition.Text = Master.eLang.GetString(532, "Condition")
        Me.colStatus.Text = Master.eLang.GetString(215, "Status")
        Me.btnCreate.Text = Master.eLang.GetString(533, "Create")
        Me.chkUseFanart.Text = Master.eLang.GetString(541, "Use Fanart for Place Holder Video")
        Me.lblTagline.Text = Master.eLang.GetString(542, "Place Holder Video Tagline:")
        Me.txtTagline.Text = Master.eLang.GetString(500, "Insert DVD")
        Me.lblTextColor.Text = Master.eLang.GetString(543, "Text Color:")
        Me.gbPreview.Text = Master.eLang.GetString(180, "Preview")
        Me.lblVideoFormat.Text = Master.eLang.GetString(544, "Place Holder Video Format:")
        Me.chkUseBackground.Text = Master.eLang.GetString(545, "Use Tagline Background")
        Me.lblTaglineBGColor.Text = Master.eLang.GetString(546, "Tagline background Color:")
        Me.chkUseOverlay.Text = Master.eLang.GetString(547, "Use Ember Overlay")
        Me.btnFont.Text = Master.eLang.GetString(548, "Select Font...")
        Me.lblTaglineTop.Text = Master.eLang.GetString(550, "Tagline Top:")
        Me.gbInfoSingle.Text = Master.eLang.GetString(551, "Information")
        Me.cbFormat.Items.AddRange(New Object() {"1080", "720", "DV PAL Wide", "DV PAL 4:3"})
    End Sub

    Private Sub tbTagLine_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTagLine.Scroll
        txtTop.Text = (RealImage_H - tbTagLine.Value).ToString
    End Sub

    Private Sub tmrBatchWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrBatchWait.Tick
        If Me.prevNameText = Me.currNameText Then
            Me.tmrBatch.Enabled = True
        Else
            Me.prevNameText = Me.currNameText
        End If
    End Sub

    Private Sub tmrBatch_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrBatch.Tick
        Me.tmrBatchWait.Enabled = False
        Me.CheckConditionsBatch()
        Me.tmrBatch.Enabled = False
    End Sub

    Private Sub tmrSingleWait_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSingleWait.Tick
        If Me.prevNameText = Me.currNameText Then
            Me.tmrSingle.Enabled = True
        Else
            Me.prevNameText = Me.currNameText
        End If
    End Sub

    Private Sub tmrSingle_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSingle.Tick
        Me.tmrSingleWait.Enabled = False
        Me.CheckConditionsSingle()
        Me.tmrSingle.Enabled = False
    End Sub

    Private Sub txtFolderNameMovieTitle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderNameMovieTitle.TextChanged
        Me.currNameText = Me.txtFolderNameMovieTitle.Text
        If rbModeSingle.Checked Then
            Me.tmrSingleWait.Enabled = False
            Me.tmrSingleWait.Enabled = True
        End If
    End Sub

    Private Sub txtFolderNameDVDProfiler_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolderNameDVDProfiler.TextChanged
        Me.currNameText = Me.txtFolderNameDVDProfiler.Text
        If rbModeSingle.Checked Then
            Me.tmrSingleWait.Enabled = False
            Me.tmrSingleWait.Enabled = True
        End If
    End Sub

    Private Sub txtTagline_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTagline.TextChanged
        Me.currText = Me.txtTagline.Text
    End Sub

    Private Sub txtTop_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTop.KeyPress
        If StringUtils.NumericOnly(e.KeyChar) Then
            e.Handled = True
            Me.CheckConditionsSingle()
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTop.TextChanged
        Try
            txtTopPos = Convert.ToUInt16(txtTop.Text)
            CreateDummyMoviePreview()
        Catch ex As Exception
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim dMovie As Database.DBElement

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class