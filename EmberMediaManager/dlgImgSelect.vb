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

Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI

Public Class dlgImgSelect

#Region "Fields"

    Friend WithEvents bwImgDownload As New System.ComponentModel.BackgroundWorker

    Private CachePath As String = String.Empty
    Private chkImage() As CheckBox
    Private DLType As Enums.ImageType
    Private ETHashes As New List(Of String)
    Private iCounter As Integer = 0
    Private iLeft As Integer = 5

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private iTop As Integer = 5
    Private lblImage() As Label

    Private noImages As Boolean = False
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    Private PreDL As Boolean = False
    Private Results As New MediaContainers.Image
    Private selIndex As Integer = -1

    Private tMovie As New Structures.DBMovie
    Private tmpImage As New MediaContainers.Image

    Private _ImageList As New List(Of MediaContainers.Image)

    Private aDes As String = String.Empty

#End Region 'Fields

    '#Region "Events"

    '    Private Event ImgDone()

    '#End Region 'Events

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ImageType, ByRef ImageList As List(Of MediaContainers.Image), Optional ByVal _isEdit As Boolean = False) As MediaContainers.Image
        '//
        ' Overload to pass data
        '\\

        Me.tMovie = DBMovie
        Me._ImageList = ImageList
        Me.DLType = Type
        Me.isEdit = _isEdit
        'Me.isShown = True
        Select Case DLType
            Case Enums.ImageType.Posters
                aDes = Master.eSize.poster_names(0).description
            Case Enums.ImageType.Fanart
                aDes = Master.eSize.backdrop_names(0).description
        End Select

        Me.SetUp()
        MyBase.ShowDialog()
        Return Results
    End Function

    Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Application.DoEvents()
            Me.pnlDLStatus.Visible = True
            Me.pnlBG.Visible = True
            Application.DoEvents()

            Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.Description = aDes)
            Me.pbDL1.Maximum = x.Count


            Me.bwImgDownload.WorkerSupportsCancellation = True
            Me.bwImgDownload.WorkerReportsProgress = True
            Me.bwImgDownload.RunWorkerAsync() 'Me.TMDB.GetImagesAsync(tMovie.Movie.TMDBID, "backdrop")

            'If Not PreDL Then
            '    StartDownload()
            'ElseIf noImages Then
            '    If Me.DLType = Enums.ImageType.Fanart Then
            '        MsgBox(Master.eLang.GetString(28, "No Fanart found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(29, "No Fanart Found"))
            '    Else
            '        MsgBox(Master.eLang.GetString(30, "No Posters found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(31, "No Posters Found"))
            '    End If
            '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            '    Me.Close()
            'End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub


    Public Overloads Function ShowDialog() As MediaContainers.Image
        'Me.isShown = True
        MyBase.ShowDialog()

        Return Results
    End Function

    Private Sub AddImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
        Try
            ReDim Preserve Me.pnlImage(iIndex)
            ReDim Preserve Me.pbImage(iIndex)
            Me.pnlImage(iIndex) = New Panel()
            Me.pbImage(iIndex) = New PictureBox()
            Me.pbImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Size = New Size(256, 286)
            Me.pbImage(iIndex).Size = New Size(250, 250)
            Me.pnlImage(iIndex).BackColor = Color.White
            Me.pnlImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlImage(iIndex).Tag = poster
            Me.pbImage(iIndex).Tag = poster
            Me.pbImage(iIndex).Image = poster.WebImage.Image
            Me.pnlImage(iIndex).Left = iLeft
            Me.pbImage(iIndex).Left = 3
            Me.pnlImage(iIndex).Top = iTop
            Me.pbImage(iIndex).Top = 3
            Me.pnlBG.Controls.Add(Me.pnlImage(iIndex))
            Me.pnlImage(iIndex).Controls.Add(Me.pbImage(iIndex))
            Me.pnlImage(iIndex).BringToFront()
            AddHandler pbImage(iIndex).Click, AddressOf pbImage_Click
            AddHandler pbImage(iIndex).DoubleClick, AddressOf pbImage_DoubleClick
            AddHandler pnlImage(iIndex).Click, AddressOf pnlImage_Click

            AddHandler pbImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent

            If Me.DLType = Enums.ImageType.Fanart Then
                ReDim Preserve Me.chkImage(iIndex)
                Me.chkImage(iIndex) = New CheckBox()
                Me.chkImage(iIndex).Name = iIndex.ToString
                Me.chkImage(iIndex).Size = New Size(250, 30)
                Me.chkImage(iIndex).AutoSize = False
                Me.chkImage(iIndex).BackColor = Color.White
                Me.chkImage(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.chkImage(iIndex).Text = text
                'Me.chkImage(iIndex).Text = String.Format("{0}x{1} ({2})", Me.pbImage(iIndex).Image.Width.ToString, Me.pbImage(iIndex).Image.Height.ToString, sDescription)
                Me.chkImage(iIndex).Left = 0
                Me.chkImage(iIndex).Top = 250
                Me.chkImage(iIndex).Checked = isChecked
                Me.pnlImage(iIndex).Controls.Add(Me.chkImage(iIndex))
                AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            Else
                ReDim Preserve Me.lblImage(iIndex)
                Me.lblImage(iIndex) = New Label()
                Me.lblImage(iIndex).Name = iIndex.ToString
                Me.lblImage(iIndex).Size = New Size(250, 30)
                Me.lblImage(iIndex).AutoSize = False
                Me.lblImage(iIndex).BackColor = Color.White
                Me.lblImage(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.lblImage(iIndex).Text = text
                Me.lblImage(iIndex).Tag = poster
                Me.lblImage(iIndex).Left = 0
                Me.lblImage(iIndex).Top = 250
                Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
                AddHandler lblImage(iIndex).Click, AddressOf lblImage_Click
                AddHandler lblImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Me.iCounter += 1

        If Me.iCounter = 3 Then
            Me.iCounter = 0
            Me.iLeft = 5
            Me.iTop += 301
        Else
            Me.iLeft += 271
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        PreviewImage()
    End Sub

    Private Sub PreviewImage()
        Try
            Dim tImage As New Images
            Dim tImg As MediaContainers.Image = Nothing
            pnlDLStatus.Visible = False

            Application.DoEvents()

            Select Case True
                Case rbXLarge.Checked
                    tImg = CType(Me.rbXLarge.Tag, MediaContainers.Image)
                Case rbLarge.Checked
                    tImg = CType(Me.rbLarge.Tag, MediaContainers.Image)
                Case rbMedium.Checked
                    tImg = CType(Me.rbMedium.Tag, MediaContainers.Image)
                Case rbSmall.Checked
                    tImg = CType(Me.rbSmall.Tag, MediaContainers.Image)
            End Select

            If Not IsNothing(tImg.WebImage) AndAlso IsNothing(tImg.WebImage.Image) Then
                tImg.WebImage.FromWeb(tImg.URL)
            End If
            tImage = tImg.WebImage

            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.Image)

            'tImage.Dispose()
            tImage = Nothing

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub bwImgDownload_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDownload.DoWork
        '//
        ' Thread to download images from the internet (multi-threaded because sometimes
        ' the web server is slow to respond or not reachable, hanging the GUI)
        '\\
        Dim i As Integer = 0

        ' Only thumbs are downloaded to be shown - green internet optimization :)
        For Each aImg In _ImageList.Where(Function(f) f.Description = aDes)
            Try
                aImg.WebImage.FromWeb(aImg.URL)
                If Me.bwImgDownload.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                Me.bwImgDownload.ReportProgress(i + 1, aImg.URL)
                i = i + 1
                Application.DoEvents()
                'Try
                '    Me.IMDBPosters.Item(i).WebImage.FromWeb(Me.IMDBPosters.Item(i).URL)
                '    If Not Master.eSettings.NoSaveImagesToNfo Then Me.Results.Posters.Add(Me.IMDBPosters.Item(i).URL)
                '    If Master.eSettings.UseImgCache Then
                '        Try
                '            Me.IMDBPosters.Item(i).URL = StringUtils.CleanURL(Me.IMDBPosters.Item(i).URL)
                '            Me.IMDBPosters.Item(i).WebImage.Save(Path.Combine(CachePath, String.Concat("poster_(", Me.IMDBPosters.Item(i).Description, ")_(url=", Me.IMDBPosters.Item(i).URL, ").jpg")), , , False)
                '        Catch
                '        End Try
                '    End If
                'Catch
                'End Try
            Catch
            End Try
        Next
    End Sub

    Private Sub bwImgDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDownload.ProgressChanged
        '//
        ' Update the status bar with the name of the current media name and increase progress bar
        '\\
        Try
            Dim sStatus As String = e.UserState.ToString
            'Debug.Print("{0} {1}", e.ProgressPercentage, sStatus)
            Me.lblDL1Status.Text = String.Format(Master.eLang.GetString(886, "Downloading {0}"), If(sStatus.Length > 40, StringUtils.TruncateURL(sStatus, 40), sStatus))
            Me.pbDL1.Value = e.ProgressPercentage
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub bwImgDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDownload.RunWorkerCompleted
        '//
        ' Thread finished: process the pics
        '\\
        Me.SuspendLayout()

        If Not e.Cancelled Then
            Dim text As String = String.Empty
            Dim aParentID As String = String.Empty
            Dim i As Integer = 0

            For Each aImg In _ImageList.Where(Function(f) f.Description = aDes)
                Try
                    aParentID = aImg.ParentID
                    Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.ParentID = aParentID)
                    If x.Count > 1 Then
                        text = Master.eLang.GetString(896, "Multiple")
                    Else
                        text = String.Format("{0}x{1} ({2})", x(0).Width.ToString, x(0).Height.ToString, x(0).Description)
                    End If
                    AddImage(aImg.Description, i, aImg.isChecked, aImg, text)
                    i = i + 1
                Catch
                End Try
            Next
            Me.pnlDLStatus.Visible = False
            Me.pnlBG.Visible = True
            Me.ResumeLayout(True)
            Me.Activate()
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If bwImgDownload.IsBusy Then bwImgDownload.CancelAsync()

        While bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Results = Nothing
        Me.Close()
    End Sub

    Private Sub CheckAll(ByVal sType As String, ByVal Checked As Boolean)
        For i As Integer = 0 To UBound(Me.chkImage)
            If Me.chkImage(i).Text.ToLower.Contains(sType) Then
                Me.chkImage(i).Checked = Checked
            End If
        Next
    End Sub

    Private Sub chkMid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMid.CheckedChanged
        Me.CheckAll("(poster)", chkMid.Checked)
    End Sub

    Private Sub chkOriginal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOriginal.CheckedChanged
        Me.CheckAll("(original)", chkOriginal.Checked)
    End Sub

    Private Sub chkThumb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkThumb.CheckedChanged
        Me.CheckAll("(thumb)", chkThumb.Checked)
    End Sub

    'Private Sub dlgImgSelect_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    'cache is local to dialog and deleted when it exits?!?
    'If Master.eSettings.AutoET AndAlso Not Master.eSettings.UseImgCache Then FileUtils.Delete.DeleteDirectory(Me.CachePath)
    'End Sub

    'Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Try
    '        Application.DoEvents()
    '        If Not PreDL Then
    '            StartDownload()
    '        ElseIf noImages Then
    '            If Me.DLType = Enums.ImageType.Fanart Then
    '                MsgBox(Master.eLang.GetString(28, "No Fanart found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(29, "No Fanart Found"))
    '            Else
    '                MsgBox(Master.eLang.GetString(30, "No Posters found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(31, "No Posters Found"))
    '            End If
    '            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '            Me.Close()
    '        End If

    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    Private Sub DoSelect(ByVal iIndex As Integer, poster As MediaContainers.Image)
        Try
            'set all pnl colors to white first
            'remove all the current genres
            For i As Integer = 0 To UBound(Me.pnlImage)
                Me.pnlImage(i).BackColor = Color.White

                If DLType = Enums.ImageType.Fanart Then
                    Me.chkImage(i).BackColor = Color.White
                    Me.chkImage(i).ForeColor = Color.Black
                Else
                    Me.lblImage(i).BackColor = Color.White
                    Me.lblImage(i).ForeColor = Color.Black
                End If
            Next

            'set selected pnl color to blue
            Me.pnlImage(iIndex).BackColor = Color.Blue

            If DLType = Enums.ImageType.Fanart Then
                Me.chkImage(iIndex).BackColor = Color.Blue
                Me.chkImage(iIndex).ForeColor = Color.White
            Else
                Me.lblImage(iIndex).BackColor = Color.Blue
                Me.lblImage(iIndex).ForeColor = Color.White
            End If

            Me.selIndex = iIndex

            Me.pnlSize.Visible = False

            Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.ParentID = CType(Me.pbImage(iIndex).Tag, MediaContainers.Image).ParentID)
            If x.Count > 1 Then
                Me.SetupSizes(poster.ParentID)
                If Not rbLarge.Checked AndAlso Not rbMedium.Checked AndAlso Not rbSmall.Checked AndAlso Not rbXLarge.Checked Then
                    Me.OK_Button.Enabled = False
                Else
                    Me.OK_Button.Focus()
                End If
                Me.tmpImage.Clear()
            Else
                Me.rbXLarge.Checked = False
                Me.rbLarge.Checked = False
                Me.rbMedium.Checked = False
                Me.rbSmall.Checked = False
                Me.OK_Button.Enabled = True
                Me.OK_Button.Focus()
                Me.tmpImage = CType(Me.pbImage(iIndex).Tag, MediaContainers.Image)
            End If

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    'Private Sub GetFanart()
    '    Try
    '        Dim NoneFound As Boolean = True

    '        If Master.eSettings.UseImgCache Then
    '            Dim di As New DirectoryInfo(CachePath)
    '            Dim lFi As New List(Of FileInfo)

    '            If Not Directory.Exists(CachePath) Then
    '                Directory.CreateDirectory(CachePath)
    '            Else
    '                Try
    '                    lFi.AddRange(di.GetFiles("*.jpg"))
    '                Catch
    '                End Try
    '            End If

    '            If lFi.Count > 0 Then
    '                Me.pnlDLStatus.Visible = True
    '                Application.DoEvents()
    '                NoneFound = False
    '                Dim tImage As MediaContainers.Image
    '                For Each sFile As FileInfo In lFi
    '                    tImage = New MediaContainers.Image
    '                    tImage.WebImage.FromFile(sFile.FullName)
    '                    If Not IsNothing(tImage.WebImage.Image) Then
    '                        Select Case True
    '                            Case sFile.Name.Contains("(original)")
    '                                tImage.Description = "original"
    '                                If Master.eSettings.AutoET AndAlso Master.eSettings.AutoETSize = Enums.FanartSize.Lrg Then
    '                                    If Not ETHashes.Contains(HashFile.HashCalcFile(sFile.FullName)) Then
    '                                        tImage.isChecked = True
    '                                    End If
    '                                End If
    '                                'Case sFile.Name.Contains("(mid)")
    '                                ' tImage.Description = "mid"
    '                            Case sFile.Name.Contains("(poster)")
    '                                tImage.Description = "poster"

    '                                If Master.eSettings.AutoET AndAlso Master.eSettings.AutoETSize = Enums.FanartSize.Mid Then
    '                                    If Not ETHashes.Contains(HashFile.HashCalcFile(sFile.FullName)) Then
    '                                        tImage.isChecked = True
    '                                    End If
    '                                End If
    '                            Case sFile.Name.Contains("(thumb)")
    '                                tImage.Description = "thumb"
    '                                If Master.eSettings.AutoET AndAlso Master.eSettings.AutoETSize = Enums.FanartSize.Small Then
    '                                    If Not ETHashes.Contains(HashFile.HashCalcFile(sFile.FullName)) Then
    '                                        tImage.isChecked = True
    '                                    End If
    '                                End If
    '                        End Select
    '                        tImage.URL = Regex.Match(sFile.Name, "\(url=(.*?)\)").Groups(1).ToString
    '                        Me.TMDBPosters.Add(tImage)
    '                    End If
    '                Next
    '                Me.ProcessPics(TMDBPosters)
    '                Me.pnlDLStatus.Visible = False
    '                Me.pnlBG.Visible = True
    '                'Me.pnlFanart.Visible = True
    '                'Me.lblInfo.Visible = True
    '            End If

    '            lFi = Nothing
    '            di = Nothing
    '        End If

    '        If Master.eSettings.AutoET AndAlso Not Directory.Exists(CachePath) Then
    '            Directory.CreateDirectory(CachePath)
    '        End If

    '        Me.lblDL2.Text = Master.eLang.GetString(32, "Retrieving data from TheMovieDB.com...")
    '        Me.lblDL1Status.Text = String.Empty
    '        Me.pbDL1.Maximum = 3
    '        Me.pnlDLStatus.Visible = True
    '        Me.Refresh()

    '        Me._tmdbDone = False

    '        Me.TMDB.GetImagesAsync(tMovie.Movie.TMDBID, "backdrop")

    '        If _MySettings.UseFANARTTV Then
    '            Me.lblDL3.Text = Master.eLang.GetString(120, "Retrieving data from Fanart.tv...")
    '            Me.lblDL3Status.Text = String.Empty
    '            Me.pbDL3.Maximum = 3
    '            Me.pnlDLStatus.Visible = True
    '            Me.Refresh()

    '            Me._fanarttvDone = False

    '            Me.FANARTTVs.GetImagesAsync(tMovie.Movie.ID)
    '        Else
    '            Me.lblDL3.Text = Master.eLang.GetString(121, "Fanart.tv is not enabled")
    '        End If


    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    'Private Sub GetPosters()
    '    Try
    '        Dim NoneFound As Boolean = True

    '        'If Master.eSettings.UseImgCache Then
    '        '    Dim lFi As New List(Of FileInfo)
    '        '    Dim di As New DirectoryInfo(CachePath)

    '        '    If Not Directory.Exists(CachePath) Then
    '        '        Directory.CreateDirectory(CachePath)
    '        '    Else
    '        '        Try
    '        '            lFi.AddRange(di.GetFiles("*.jpg"))
    '        '        Catch
    '        '        End Try
    '        '    End If

    '        '    If lFi.Count > 0 Then
    '        '        Me.pnlDLStatus.Height = 165
    '        '        Me.pnlDLStatus.Top = 207
    '        '        Me.pnlDLStatus.Visible = True
    '        '        Application.DoEvents()
    '        '        NoneFound = False
    '        '        Dim tImage As MediaContainers.Image
    '        '        For Each sFile As FileInfo In lFi
    '        '            tImage = New MediaContainers.Image
    '        '            tImage.WebImage.FromFile(sFile.FullName)
    '        '            Select Case True
    '        '                Case sFile.Name.Contains("(original)")
    '        '                    tImage.Description = "original"
    '        '                Case sFile.Name.Contains("(mid)")
    '        '                    tImage.Description = "mid"
    '        '                Case sFile.Name.Contains("(cover)")
    '        '                    tImage.Description = "cover"
    '        '                Case sFile.Name.Contains("(thumb)")
    '        '                    tImage.Description = "thumb"
    '        '                Case sFile.Name.Contains("(poster)")
    '        '                    tImage.Description = "poster"
    '        '            End Select
    '        '            tImage.URL = Regex.Match(sFile.Name, "\(url=(.*?)\)").Groups(1).ToString
    '        '            Me.TMDBPosters.Add(tImage)
    '        '        Next
    '        '        Me.ProcessPics(TMDBPosters)
    '        '        Me.pnlDLStatus.Visible = False
    '        '        Me.pnlBG.Visible = True
    '        '    End If

    '        '    lFi = Nothing
    '        '    di = Nothing
    '        'End If

    '        If NoneFound Then
    '            Me.lblDL1.Text = Master.eLang.GetString(32, "Retrieving data from TheMovieDB.com...")
    '            Me.lblDL1Status.Text = String.Empty
    '            Me.pbDL1.Maximum = 3
    '            Me.pnlDLStatus.Visible = True
    '            Me.Refresh()

    '            Me._tmdbDone = False

    '            Me.TMDB.GetImagesAsync(tMovie.Movie.TMDBID, "poster")

    '            If _MySettings.UseIMDB Then
    '                Me.lblDL6.Text = Master.eLang.GetString(117, "Retrieving data from IMDB.com...")
    '                Me.lblDL6Status.Text = String.Empty
    '                Me.pbDL6.Maximum = 3
    '                Me.pnlDLStatus.Visible = True
    '                Me.Refresh()

    '                Me._imdbDone = False

    '                Me.IMDB.GetImagesAsync(tMovie.Movie.IMDBID)
    '            Else
    '                Me.lblDL6.Text = Master.eLang.GetString(118, "IMDB.com is not enabled")
    '            End If

    '            If _MySettings.UseIMPA Then
    '                Me.lblDL4.Text = Master.eLang.GetString(34, "Retrieving data from IMPAwards.com...")
    '                Me.lblDL4Status.Text = String.Empty
    '                Me.pbDL4.Maximum = 3
    '                Me.pnlDLStatus.Visible = True
    '                Me.Refresh()

    '                Me._impaDone = False

    '                Me.IMPA.GetImagesAsync(tMovie.Movie.IMDBID)
    '            Else
    '                Me.lblDL4.Text = Master.eLang.GetString(35, "IMPAwards.com is not enabled")
    '            End If

    '            If _MySettings.UseMPDB Then
    '                Me.lblDL5.Text = Master.eLang.GetString(36, "Retrieving data from MoviePosterDB.com...")
    '                Me.lblDL5Status.Text = String.Empty
    '                Me.pbDL5.Maximum = 3
    '                Me.pnlDLStatus.Visible = True
    '                Me.Refresh()

    '                Me._mpdbDone = False

    '                Me.MPDB.GetImagesAsync(tMovie.Movie.IMDBID)
    '            Else
    '                Me.lblDL5.Text = Master.eLang.GetString(37, "MoviePostersDB.com is not enabled")
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub


    Private Sub lblImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image))
    End Sub

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Delta < 0 Then
            If (pnlBG.VerticalScroll.Value + 50) <= pnlBG.VerticalScroll.Maximum Then
                pnlBG.VerticalScroll.Value += 50
            Else
                pnlBG.VerticalScroll.Value = pnlBG.VerticalScroll.Maximum
            End If
        Else
            If (pnlBG.VerticalScroll.Value - 50) >= pnlBG.VerticalScroll.Minimum Then
                pnlBG.VerticalScroll.Value -= 50
            Else
                pnlBG.VerticalScroll.Value = pnlBG.VerticalScroll.Minimum
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If IsNothing(Me.tmpImage.WebImage.Image) Then
                Me.pnlBG.Visible = False

                Me.Refresh()
                Application.DoEvents()
                Select Case True
                    Case Me.rbXLarge.Checked
                        Results = CType(Me.rbXLarge.Tag, MediaContainers.Image)
                    Case Me.rbLarge.Checked
                        Results = CType(Me.rbLarge.Tag, MediaContainers.Image)
                    Case Me.rbMedium.Checked
                        Results = CType(Me.rbMedium.Tag, MediaContainers.Image)
                    Case Me.rbSmall.Checked
                        Results = CType(Me.rbSmall.Tag, MediaContainers.Image)
                End Select
            End If

            'Extrathumb management to be fixed / checked
            'If Me.DLType = Enums.ImageType.Fanart Then
            '    Dim iMod As Integer = 0
            '    Dim iVal As Integer = 1
            '    Dim extraPath As String = String.Empty
            '    Dim isChecked As Boolean = False

            '    For i As Integer = 0 To UBound(Me.chkImage)
            '        If Me.chkImage(i).Checked Then
            '            isChecked = True
            '            Exit For
            '        End If
            '    Next

            '    If isChecked Then
            '        Dim extrathumbsFolderName As String = AdvancedSettings.GetSetting("ExtraThumbsFolderName", "extrathumbs")
            '        If isEdit Then
            '            extraPath = Path.Combine(Master.TempPath, extrathumbsFolderName)
            '        Else
            '            If Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isVideoTS(Me.tMovie.Filename) Then
            '                extraPath = Path.Combine(Directory.GetParent(Directory.GetParent(Me.tMovie.Filename).FullName).FullName, extrathumbsFolderName)
            '            ElseIf Master.eSettings.VideoTSParent AndAlso FileUtils.Common.isBDRip(Me.tMovie.Filename) Then
            '                extraPath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Me.tMovie.Filename).FullName).FullName).FullName, extrathumbsFolderName)
            '            Else
            '                extraPath = Path.Combine(Directory.GetParent(Me.tMovie.Filename).FullName, extrathumbsFolderName)
            '            End If
            '            iMod = Functions.GetExtraModifier(extraPath)
            '            iVal = iMod + 1
            '        End If

            '        If Not Directory.Exists(extraPath) Then
            '            Directory.CreateDirectory(extraPath)
            '        End If

            '        Dim fsET As FileStream
            '        For i As Integer = 0 To UBound(Me.chkImage)
            '            If Me.chkImage(i).Checked Then
            '                fsET = New FileStream(Path.Combine(extraPath, String.Concat("thumb", iVal, ".jpg")), FileMode.Create, FileAccess.ReadWrite)
            '                Me.pbImage(i).Image.Save(fsET, System.Drawing.Imaging.ImageFormat.Jpeg)
            '                fsET.Close()
            '                iVal += 1
            '            End If
            '        Next
            '        fsET = Nothing
            '    End If
            'End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbImage_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'If Me.DLType = Enums.ImageType.Fanart OrElse Not IsTMDBURL(DirectCast(sender, PictureBox).Tag.ToString) Then
            'ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(DirectCast(sender, PictureBox).Image)
            'Else
            PreviewImage()
            'End If
        Catch
        End Try
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    'Private Sub ProcessPics(ByVal posters As List(Of MediaContainers.Image))
    '    Try
    '        Dim iIndex As Integer = 0

    '        'remove all entries with invalid images
    '        If Master.eSettings.UseImgCache Then
    '            For i As Integer = posters.Count - 1 To 0 Step -1
    '                If IsNothing(posters.Item(i).WebImage.Image) Then
    '                    posters.RemoveAt(i)
    '                End If
    '            Next
    '        End If

    '        If posters.Count > 0 Then
    '            For Each xPoster As MediaContainers.Image In posters.OrderBy(Function(p) RemoveServerURL(p.URL))
    '                If Not IsNothing(xPoster.WebImage.Image) AndAlso (Me.DLType = Enums.ImageType.Fanart OrElse Not (IsTMDBURL(xPoster.URL) AndAlso Not xPoster.Description = "cover")) Then
    '                    Me.AddImage(xPoster.Description, iIndex, xPoster.isChecked, xPoster)
    '                    iIndex += 1
    '                End If
    '            Next
    '        Else
    '            If Not Me.PreDL OrElse isShown Then
    '                If Me.DLType = Enums.ImageType.Fanart Then
    '                    MsgBox(Master.eLang.GetString(28, "No Fanart found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(29, "No Fanart Found"))
    '                Else
    '                    MsgBox(Master.eLang.GetString(30, "No Posters found for this movie."), MsgBoxStyle.Information, Master.eLang.GetString(31, "No Posters Found"))
    '                End If
    '                Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '                Me.Close()
    '            Else
    '                noImages = True
    '            End If
    '        End If

    '        Me.Activate()

    '    Catch ex As Exception
    '        Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
    '    End Try
    'End Sub

    Private Sub rbLarge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbLarge.CheckedChanged
        Me.OK_Button.Enabled = True
        Me.btnPreview.Enabled = True
    End Sub

    Private Sub rbMedium_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMedium.CheckedChanged
        Me.OK_Button.Enabled = True
        Me.btnPreview.Enabled = True
    End Sub

    Private Sub rbSmall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSmall.CheckedChanged
        Me.OK_Button.Enabled = True
        Me.btnPreview.Enabled = True
    End Sub

    Private Sub rbXLarge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbXLarge.CheckedChanged
        Me.OK_Button.Enabled = True
        Me.btnPreview.Enabled = True
    End Sub

    Private Sub SetUp()
        Try
            AddHandler MyBase.MouseWheel, AddressOf MouseWheelEvent
            AddHandler pnlBG.MouseWheel, AddressOf MouseWheelEvent

            Functions.PNLDoubleBuffer(Me.pnlBG)

            If Me.DLType = Enums.ImageType.Posters Then
                Me.Text = String.Concat(Master.eLang.GetString(877, "Select Poster"), " - ", If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle))
                Me.pnlDwld.Visible = True
                'Me.pnlDLStatus.Height = 328
                'Me.pnlDLStatus.Top = 82
            Else
                Me.Text = String.Concat(Master.eLang.GetString(878, "Select Fanart"), " - ", If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle))
                Me.pnlDwld.Visible = True
                'Me.pnlDLStatus.Height = 165
                'Me.pnlDLStatus.Top = 129
            End If

            CachePath = String.Concat(Master.TempPath, Path.DirectorySeparatorChar, tMovie.Movie.IMDBID, Path.DirectorySeparatorChar, If(Me.DLType = Enums.ImageType.Posters, "posters", "fanart"))

            Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
            Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
            Me.btnPreview.Text = Master.eLang.GetString(180, "Preview")
            Me.chkThumb.Text = Master.eLang.GetString(890, "Check All Thumb")
            Me.chkMid.Text = Master.eLang.GetString(891, "Check All Mid")
            Me.chkOriginal.Text = Master.eLang.GetString(892, "Check All Original")
            Me.lblInfo.Text = Master.eLang.GetString(893, "Selected item will be set as fanart. All checked items will be saved to \extrathumbs.")
            Me.lblDL1.Text = Master.eLang.GetString(894, "Performing Preliminary Tasks...")
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Function GetServerURL(ByVal sURL As String) As String
        If sURL.StartsWith("http://") Then
            Dim s As Integer = sURL.IndexOf("/", 7)
            If s >= 0 Then Return sURL.Substring(0, sURL.IndexOf("/", 7))
        End If
        Return sURL
    End Function

    Private Function RemoveServerURL(ByVal sURL As String) As String
        If sURL.StartsWith("http://") Then
            Dim s As Integer = sURL.IndexOf("/", 7)
            If s >= 0 Then Return sURL.Substring(sURL.IndexOf("/", 7))
        End If
        Return sURL
    End Function

    Private Sub SetupSizes(ByVal ParentID As String)
        Try
            rbXLarge.Checked = False
            rbXLarge.Enabled = False
            rbLarge.Checked = False
            rbLarge.Enabled = False
            rbMedium.Checked = False
            rbMedium.Enabled = False
            rbSmall.Checked = False
            rbSmall.Enabled = False
            'If Me.DLType = Enums.ImageType.Fanart Then
            '    rbLarge.Text = Master.eSize.backdrop_names(2).description
            '    rbMedium.Text = Master.eSize.backdrop_names(1).description
            '    rbSmall.Text = Master.eSize.backdrop_names(0).description
            'Else
            rbXLarge.Text = Master.eLang.GetString(897, "Original")
            rbLarge.Text = Master.eLang.GetString(898, "Cover")
            rbMedium.Text = Master.eLang.GetString(899, "Medium")
            rbSmall.Text = Master.eLang.GetString(900, "Small")
            'End If

            For Each TMDBPoster As MediaContainers.Image In _ImageList.Where(Function(f) f.ParentID = ParentID)
                If Not (TMDBPoster.Width = "n/a") AndAlso Not (TMDBPoster.Height = "n/a") Then
                    If Me.DLType = Enums.ImageType.Posters Then
                        'Debug.Print("{0}  {1} {2}", TMDBPoster.Description, TMDBPoster.URL, TMDBPoster.ParentID)
                        Select Case TMDBPoster.Description
                            Case Master.eSize.poster_names(5).description
                                ' xlarge
                                rbXLarge.Enabled = True
                                rbXLarge.Tag = TMDBPoster
                                    rbXLarge.Text = String.Format(Master.eLang.GetString(901, "Original ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(4).description
                                ' large
                                rbLarge.Enabled = True
                                rbLarge.Tag = TMDBPoster
                                    rbLarge.Text = String.Format(Master.eLang.GetString(902, "Cover ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(2).description
                                rbMedium.Enabled = True
                                rbMedium.Tag = TMDBPoster
                                    rbMedium.Text = String.Format(Master.eLang.GetString(903, "Medium ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(0).description
                                ' small                        
                                rbSmall.Enabled = True
                                rbSmall.Tag = TMDBPoster
                                    rbSmall.Text = String.Format(Master.eLang.GetString(904, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                        End Select
                    Else
                        Select Case TMDBPoster.Description
                            Case Master.eSize.backdrop_names(3).description
                                ' xlarge
                                rbXLarge.Enabled = True
                                rbXLarge.Tag = TMDBPoster
                                    rbXLarge.Text = String.Format(Master.eLang.GetString(901, "Original ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(2).description
                                ' large
                                rbLarge.Enabled = True
                                rbLarge.Tag = TMDBPoster
                                rbLarge.Text = String.Format(Master.eLang.GetString(905, "Large ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(1).description
                                ' small                        
                                rbMedium.Enabled = True
                                rbMedium.Tag = TMDBPoster
                                rbMedium.Text = String.Format(Master.eLang.GetString(903, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(0).description
                                ' thumb
                                rbSmall.Enabled = True
                                rbSmall.Tag = TMDBPoster
                                rbSmall.Text = String.Format(Master.eLang.GetString(904, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                        End Select
                    End If
                Else
                    If Me.DLType = Enums.ImageType.Posters Then
                        Select Case TMDBPoster.Description
                            Case Master.eSize.poster_names(5).description
                                ' xlarge
                                rbXLarge.Enabled = True
                                rbXLarge.Tag = TMDBPoster
                            Case Master.eSize.poster_names(4).description
                                ' large
                                rbLarge.Enabled = True
                                rbLarge.Tag = TMDBPoster
                            Case Master.eSize.poster_names(2).description
                                rbMedium.Enabled = True
                                rbMedium.Tag = TMDBPoster
                            Case Master.eSize.poster_names(0).description
                                ' small                        
                                rbSmall.Enabled = True
                                rbSmall.Tag = TMDBPoster
                        End Select
                    Else
                        Select Case TMDBPoster.Description
                            Case Master.eSize.backdrop_names(3).description
                                ' xlarge
                                rbXLarge.Enabled = True
                                rbXLarge.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(2).description
                                ' large
                                rbLarge.Enabled = True
                                rbLarge.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(1).description
                                ' small                        
                                rbMedium.Enabled = True
                                rbMedium.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(0).description
                                ' thumb
                                rbSmall.Enabled = True
                                rbSmall.Tag = TMDBPoster
                        End Select
                    End If
                End If
            Next

            If Me.DLType = Enums.ImageType.Fanart Then
                Select Case Master.eSettings.PreferredFanartSize
                    Case Enums.FanartSize.Small
                        rbSmall.Checked = rbSmall.Enabled
                    Case Enums.FanartSize.Mid
                        rbMedium.Checked = rbMedium.Enabled
                    Case Enums.FanartSize.Lrg
                        rbLarge.Checked = rbLarge.Enabled
                    Case Enums.FanartSize.Xlrg
                        rbXLarge.Checked = rbXLarge.Enabled
                End Select
            Else
                Select Case Master.eSettings.PreferredPosterSize
                    Case Enums.PosterSize.Small
                        rbSmall.Checked = rbSmall.Enabled
                    Case Enums.PosterSize.Mid
                        rbMedium.Checked = rbMedium.Enabled
                    Case Enums.PosterSize.Lrg
                        rbLarge.Checked = rbLarge.Enabled
                    Case Enums.PosterSize.Xlrg
                        rbXLarge.Checked = rbXLarge.Enabled
                End Select
            End If

            pnlSize.Visible = True

            Invalidate()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

#End Region 'Methods

End Class