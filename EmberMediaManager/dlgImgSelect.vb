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

    'Private CachePath As String = String.Empty
    'Private chkImage() As CheckBox
    Private DLType As Enums.ImageType
    Private isWorkerDone As Boolean = False
    'Private ETHashes As New List(Of String)
    'Private iCounter As Integer = 0
    'Private iLeft As Integer = 5

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private iTop As Integer = 5
    Private lblImage() As Label

    'Private noImages As Boolean = False
    'Private pbImage() As PictureBox
    'Private pnlImage() As Panel
    'Private PreDL As Boolean = False
    Private _results As New MediaContainers.Image
    Private selIndex As Integer = -1

    Private tMovie As New Structures.DBMovie
    Private tmpImage As New MediaContainers.Image

    Private _ImageList As New List(Of MediaContainers.Image)

    Private aDes As String = String.Empty

#End Region 'Fields

#Region "Properties"
    Public Property Results As MediaContainers.Image
        Get
            Return _results
        End Get
        Set(value As MediaContainers.Image)
            _results = value
        End Set
    End Property
#End Region

    '#Region "Events"

    '    Private Event ImgDone()

    '#End Region 'Events

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ImageType, ByRef ImageList As List(Of MediaContainers.Image), Optional ByVal _isEdit As Boolean = False) As DialogResult
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
        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Application.DoEvents()
            Me.pnlDLStatus.Visible = True
            Me.pnlBG.Visible = True
            Me.lblSize.Visible = False
            Me.cbFilterSize.Visible = False

            Application.DoEvents()

            Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.Description = aDes)
            Me.pbDL1.Maximum = x.Count

            Me.bwImgDownload.WorkerSupportsCancellation = True
            Me.bwImgDownload.WorkerReportsProgress = True
            isWorkerDone = False
            Me.bwImgDownload.RunWorkerAsync() 'Me.TMDB.GetImagesAsync(tMovie.Movie.TMDBID, "backdrop")

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Public Overloads Function ShowDialog() As DialogResult
        'Me.isShown = True
        Return MyBase.ShowDialog()
    End Function

    Private Sub AddImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
        Try
            Dim ResImg As Image
            ResImg = CType(poster.WebImage.Image.Clone(), Image)
            ImageUtils.ResizeImage(ResImg, 250, 250, True, Color.White.ToArgb())
            Me.LargeImageList.Images.Add(ResImg)

            Me.lvImages.Items.Add(text, Me.LargeImageList.Images.Count - 1)
            Me.lvImages.Items(Me.lvImages.Items.Count - 1).Tag = poster
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        PreviewImage()
    End Sub

    Private Sub PreviewImage()
        Try
            Dim tImage As New Images
            Dim tImg As MediaContainers.Image = Nothing
            Me.pnlDLStatus.Visible = False
            Me.lblSize.Visible = True
            Me.cbFilterSize.Visible = True

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
            isWorkerDone = True
            FillListView(aDes)
        End If
    End Sub

    Private Sub FillListView(aDesc As String)
        Dim text As String = String.Empty
        Dim aParentID As String = String.Empty
        Dim i As Integer = 0
        Dim tImg As MediaContainers.Image = Nothing

        If Not isWorkerDone Then
            Exit Sub
        End If

        Me.LargeImageList.Images.Clear()
        Me.lvImages.Items.Clear()
        Me.LargeImageList.ImageSize = New Size(250, 250) 'Size(CInt(poster.Width), CInt(poster.Height))
        Me.LargeImageList.ColorDepth = ColorDepth.Depth32Bit

        For Each aImg In _ImageList.Where(Function(f) f.Description = aDesc)
            Try
                aParentID = aImg.ParentID
                Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.ParentID = aParentID)
                If x.Count > 1 Then

                    text = If(String.IsNullOrEmpty(aImg.LongLang), Master.eLang.GetString(896, "Multiple"), Master.eLang.GetString(896, "Multiple") & Environment.NewLine & aImg.LongLang) 'Master.eLang.GetString(896, "Multiple")
                    Dim y = From MI As MediaContainers.Image In _ImageList Where ((MI.ParentID = aParentID) And (MI.Description = aDes))
                    tImg = y(0)
                Else
                    text = String.Format("{0}x{1} ({2})", x(0).Width.ToString, x(0).Height.ToString, x(0).Description)
                    tImg = aImg
                End If
                AddImage(tImg.Description, i, tImg.isChecked, tImg, text)
                i = i + 1
            Catch
            End Try
        Next
        Me.pnlDLStatus.Visible = False
        Me.pnlBG.Visible = True
        Me.lblSize.Visible = True
        Me.cbFilterSize.Visible = True
        Me.ResumeLayout(True)
        Me.Activate()

    End Sub

    Private Sub cbFilterSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilterSize.SelectedIndexChanged
        If Me.cbFilterSize.SelectedIndex = 0 Then
            FillListView(aDes)
        Else
            Select Case DLType
                Case Enums.ImageType.Posters
                    FillListView(Master.eSize.poster_names(Me.cbFilterSize.SelectedIndex - 1).description)
                Case Enums.ImageType.Fanart
                    FillListView(Master.eSize.backdrop_names(Me.cbFilterSize.SelectedIndex - 1).description)
            End Select
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

    Private Sub DoSelect(ByVal iIndex As Integer, poster As MediaContainers.Image, isSelected As Boolean)
        Try
            Me.pnlSize.Visible = False

            If isSelected Then
                Me.selIndex = iIndex

                Dim x = From MI As MediaContainers.Image In _ImageList Where (MI.ParentID = poster.ParentID)
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
                    Me.tmpImage = poster
                End If
            Else
                Me.selIndex = -1
            End If
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
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

        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lvImages_DoubleClick(sender As Object, e As System.EventArgs) Handles lvImages.DoubleClick
        PreviewImage()
    End Sub 'Methods

    Private Sub lvImages_ItemSelectionChanged(sender As Object, e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvImages.ItemSelectionChanged
        Me.DoSelect(e.ItemIndex, DirectCast(DirectCast(e.Item, ListViewItem).Tag, MediaContainers.Image), e.IsSelected)
    End Sub

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
                Me.cbFilterSize.Items.Clear()
                Me.cbFilterSize.Items.AddRange(New String() {Master.eLang.GetString(569, "All"), Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small"), Master.eLang.GetString(558, "Wide")})
            Else
                Me.Text = String.Concat(Master.eLang.GetString(878, "Select Fanart"), " - ", If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle))
                Me.pnlDwld.Visible = True
                Me.cbFilterSize.Items.Clear()
                Me.cbFilterSize.Items.AddRange(New String() {Master.eLang.GetString(569, "All"), Master.eLang.GetString(322, "X-Large"), Master.eLang.GetString(323, "Large"), Master.eLang.GetString(324, "Medium"), Master.eLang.GetString(325, "Small")})
            End If

            Me.cbFilterSize.SelectedIndex = 0
            lblSize.Text = Master.eLang.GetString(957, "Show the ones with size:")

            'CachePath = String.Concat(Master.TempPath, Path.DirectorySeparatorChar, tMovie.Movie.IMDBID, Path.DirectorySeparatorChar, If(Me.DLType = Enums.ImageType.Posters, "posters", "fanart"))

            Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
            Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
            Me.btnPreview.Text = Master.eLang.GetString(180, "Preview")
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
            Me.rbXLarge.Checked = False
            Me.rbXLarge.Enabled = False
            Me.rbLarge.Checked = False
            Me.rbLarge.Enabled = False
            Me.rbMedium.Checked = False
            Me.rbMedium.Enabled = False
            Me.rbSmall.Checked = False
            Me.rbSmall.Enabled = False
            'If Me.DLType = Enums.ImageType.Fanart Then
            '    rbLarge.Text = Master.eSize.backdrop_names(2).description
            '    rbMedium.Text = Master.eSize.backdrop_names(1).description
            '    rbSmall.Text = Master.eSize.backdrop_names(0).description
            'Else
            Me.rbXLarge.Text = Master.eLang.GetString(897, "Original")
            Me.rbLarge.Text = Master.eLang.GetString(898, "Cover")
            Me.rbMedium.Text = Master.eLang.GetString(324, "Medium")
            Me.rbSmall.Text = Master.eLang.GetString(325, "Small")
            'End If

            For Each TMDBPoster As MediaContainers.Image In _ImageList.Where(Function(f) f.ParentID = ParentID)
                If Not (TMDBPoster.Width = "n/a") AndAlso Not (TMDBPoster.Height = "n/a") Then
                    If Me.DLType = Enums.ImageType.Posters Then
                        'Debug.Print("{0}  {1} {2}", TMDBPoster.Description, TMDBPoster.URL, TMDBPoster.ParentID)
                        Select Case TMDBPoster.Description
                            Case Master.eSize.poster_names(5).description
                                ' xlarge
                                Me.rbXLarge.Enabled = True
                                Me.rbXLarge.Tag = TMDBPoster
                                Me.rbXLarge.Text = String.Format(Master.eLang.GetString(901, "Original ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(4).description
                                ' large
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = TMDBPoster
                                Me.rbLarge.Text = String.Format(Master.eLang.GetString(902, "Cover ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(2).description
                                Me.rbMedium.Enabled = True
                                Me.rbMedium.Tag = TMDBPoster
                                Me.rbMedium.Text = String.Format(Master.eLang.GetString(903, "Medium ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.poster_names(0).description
                                ' small                        
                                Me.rbSmall.Enabled = True
                                Me.rbSmall.Tag = TMDBPoster
                                Me.rbSmall.Text = String.Format(Master.eLang.GetString(904, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                        End Select
                    Else
                        Select Case TMDBPoster.Description
                            Case Master.eSize.backdrop_names(3).description
                                ' xlarge
                                Me.rbXLarge.Enabled = True
                                Me.rbXLarge.Tag = TMDBPoster
                                Me.rbXLarge.Text = String.Format(Master.eLang.GetString(901, "Original ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(2).description
                                ' large
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = TMDBPoster
                                Me.rbLarge.Text = String.Format(Master.eLang.GetString(905, "Large ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(1).description
                                ' small                        
                                Me.rbMedium.Enabled = True
                                Me.rbMedium.Tag = TMDBPoster
                                Me.rbMedium.Text = String.Format(Master.eLang.GetString(903, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                            Case Master.eSize.backdrop_names(0).description
                                ' thumb
                                Me.rbSmall.Enabled = True
                                Me.rbSmall.Tag = TMDBPoster
                                Me.rbSmall.Text = String.Format(Master.eLang.GetString(904, "Small ({0}x{1})"), TMDBPoster.Width, TMDBPoster.Height)
                        End Select
                    End If
                Else
                    If Me.DLType = Enums.ImageType.Posters Then
                        Select Case TMDBPoster.Description
                            Case Master.eSize.poster_names(5).description
                                ' xlarge
                                Me.rbXLarge.Enabled = True
                                Me.rbXLarge.Tag = TMDBPoster
                            Case Master.eSize.poster_names(4).description
                                ' large
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = TMDBPoster
                            Case Master.eSize.poster_names(2).description
                                Me.rbMedium.Enabled = True
                                Me.rbMedium.Tag = TMDBPoster
                            Case Master.eSize.poster_names(0).description
                                ' small                        
                                Me.rbSmall.Enabled = True
                                Me.rbSmall.Tag = TMDBPoster
                        End Select
                    Else
                        Select Case TMDBPoster.Description
                            Case Master.eSize.backdrop_names(3).description
                                ' xlarge
                                Me.rbXLarge.Enabled = True
                                Me.rbXLarge.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(2).description
                                ' large
                                Me.rbLarge.Enabled = True
                                Me.rbLarge.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(1).description
                                ' small                        
                                Me.rbMedium.Enabled = True
                                Me.rbMedium.Tag = TMDBPoster
                            Case Master.eSize.backdrop_names(0).description
                                ' thumb
                                Me.rbSmall.Enabled = True
                                Me.rbSmall.Tag = TMDBPoster
                        End Select
                    End If
                End If
            Next

            If Me.DLType = Enums.ImageType.Fanart Then
                Select Case Master.eSettings.PreferredFanartSize
                    Case Enums.FanartSize.Small
                        Me.rbSmall.Checked = rbSmall.Enabled
                    Case Enums.FanartSize.Mid
                        Me.rbMedium.Checked = rbMedium.Enabled
                    Case Enums.FanartSize.Lrg
                        Me.rbLarge.Checked = rbLarge.Enabled
                    Case Enums.FanartSize.Xlrg
                        Me.rbXLarge.Checked = rbXLarge.Enabled
                End Select
            Else
                Select Case Master.eSettings.PreferredPosterSize
                    Case Enums.PosterSize.Small
                        Me.rbSmall.Checked = rbSmall.Enabled
                    Case Enums.PosterSize.Mid
                        Me.rbMedium.Checked = rbMedium.Enabled
                    Case Enums.PosterSize.Lrg
                        Me.rbLarge.Checked = rbLarge.Enabled
                    Case Enums.PosterSize.Xlrg
                        Me.rbXLarge.Checked = rbXLarge.Enabled
                End Select
            End If

            Me.pnlSize.Visible = True

            Invalidate()
        Catch ex As Exception
            Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        End Try
    End Sub

#End Region

End Class