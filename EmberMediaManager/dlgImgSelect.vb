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
Imports NLog

Public Class dlgImgSelect

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwImgDownload As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwImgLoading As New System.ComponentModel.BackgroundWorker

    Public Delegate Sub LoadImage(ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

    'Private CachePath As String = String.Empty
    Private chkImageET() As CheckBox
    Private chkImageEF() As CheckBox
    Private pnlImageET() As Panel
    Private pnlImageEF() As Panel
    Private DLType As Enums.ModifierType
    Private isWorkerDone As Boolean = False
    'Private ETHashes As New List(Of String)
    Private iCounter As Integer = 0
    Private iLeft As Integer = 5

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private iTop As Integer = 5
    Private lblImage() As Label

    'Private noImages As Boolean = False
    Private pbImage() As PictureBox
    Private pnlImage() As Panel
    'Private PreDL As Boolean = False
    Private _results As New MediaContainers.Image
    Private selIndex As Integer = -1

    Private tIsMovie As Boolean
    Private tMovie As New Structures.DBMovie
    Private tMovieSet As New Structures.DBMovieSet
    Private tmpImage As New MediaContainers.Image
    Private tmpImageEF As New MediaContainers.Image
    Private tmpImageET As New MediaContainers.Image

    Private _ImageList As New List(Of MediaContainers.Image)
    Private _efList As New List(Of String)
    Private _etList As New List(Of String)

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

    Public Property efList As List(Of String)
        Get
            Return _efList
        End Get
        Set(value As List(Of String))
            _efList = value
        End Set
    End Property

    Public Property etList As List(Of String)
        Get
            Return _etList
        End Get
        Set(value As List(Of String))
            _etList = value
        End Set
    End Property

#End Region 'Properties

    '#Region "Events"

    '    Private Event ImgDone()

    '#End Region 'Events

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DBMovie">Movie</param>
    ''' <param name="Type">Image type</param>
    ''' <param name="ImageList">Image list</param>
    ''' <param name="efList">Extrafanarts list</param>
    ''' <param name="etList">Extrathumbs list</param>
    ''' <param name="_isEdit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function ShowDialog(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ModifierType, ByRef ImageList As List(Of MediaContainers.Image), ByRef efList As List(Of String), ByRef etList As List(Of String), Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.tMovie = DBMovie
        Me.tIsMovie = True
        Me._ImageList = ImageList
        Me._efList = efList
        Me._etList = etList
        Me.DLType = Type
        Me.isEdit = _isEdit

        Me.SetUp()
        Return MyBase.ShowDialog()
    End Function

    Public Overloads Function ShowDialog(ByRef DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ModifierType, ByRef ImageList As List(Of MediaContainers.Image), ByRef efList As List(Of String), ByRef etList As List(Of String), Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.tMovieSet = DBMovieSet
        Me.tIsMovie = False
        Me._ImageList = ImageList
        Me._efList = efList
        Me._etList = etList
        Me.DLType = Type
        Me.isEdit = _isEdit

        Me.SetUp()
        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImgSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Application.DoEvents()
            Me.pnlDLStatus.Visible = True
            Me.pnlBG.Visible = True

            Application.DoEvents()

            Me.pbDL1.Maximum = _ImageList.Count

            Me.bwImgDownload.WorkerSupportsCancellation = True
            Me.bwImgDownload.WorkerReportsProgress = True
            isWorkerDone = False
            Me.bwImgDownload.RunWorkerAsync()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Overloads Function ShowDialog() As DialogResult
        Return MyBase.ShowDialog()
    End Function

    Private Sub AddImage(ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
        Try
            ReDim Preserve Me.pnlImage(iIndex)
            ReDim Preserve Me.pbImage(iIndex)
            Me.pnlImage(iIndex) = New Panel()
            Me.pbImage(iIndex) = New PictureBox()
            Me.pbImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Name = iIndex.ToString
            Me.pnlImage(iIndex).Size = New Size(256, 296)
            Me.pbImage(iIndex).Size = New Size(250, 250)
            Me.pnlImage(iIndex).BackColor = Color.White
            Me.pnlImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.pnlImage(iIndex).Tag = poster
            Me.pbImage(iIndex).Tag = poster
            Me.pbImage(iIndex).Image = If(poster.WebThumb.Image IsNot Nothing, CType(poster.WebThumb.Image.Clone(), Image), CType(poster.WebImage.Image.Clone(), Image))
            Me.pnlImage(iIndex).Left = iLeft
            Me.pbImage(iIndex).Left = 2
            Me.pnlImage(iIndex).Top = iTop
            Me.pbImage(iIndex).Top = 2
            Me.pnlBG.Controls.Add(Me.pnlImage(iIndex))
            Me.pnlImage(iIndex).Controls.Add(Me.pbImage(iIndex))
            Me.pnlImage(iIndex).BringToFront()
            AddHandler pbImage(iIndex).Click, AddressOf pbImage_Click
            AddHandler pbImage(iIndex).DoubleClick, AddressOf pbImage_DoubleClick
            AddHandler pnlImage(iIndex).Click, AddressOf pnlImage_Click

            AddHandler pbImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent

            If Me.DLType = Enums.ModifierType.Fanart Then
                ReDim Preserve Me.chkImageET(iIndex)
                ReDim Preserve Me.chkImageEF(iIndex)
                ReDim Preserve Me.pnlImageET(iIndex)
                ReDim Preserve Me.pnlImageEF(iIndex)
                ReDim Preserve Me.lblImage(iIndex)
                ' Label
                Me.lblImage(iIndex) = New Label()
                Me.lblImage(iIndex).Name = iIndex.ToString
                Me.lblImage(iIndex).Size = New Size(250, 15)
                Me.lblImage(iIndex).AutoSize = False
                Me.lblImage(iIndex).BackColor = Color.White
                Me.lblImage(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.lblImage(iIndex).Text = text
                Me.lblImage(iIndex).Tag = poster
                Me.lblImage(iIndex).Left = 2
                Me.lblImage(iIndex).Top = 250
                ' Extrathumbs Panel (fake button)
                Me.pnlImageET(iIndex) = New Panel()
                Me.pnlImageET(iIndex).Name = iIndex.ToString
                Me.pnlImageET(iIndex).Size = New System.Drawing.Size(124, 20)
                Me.pnlImageET(iIndex).BorderStyle = BorderStyle.FixedSingle
                Me.pnlImageET(iIndex).BackColor = Color.White
                Me.pnlImageET(iIndex).Left = 2
                Me.pnlImageET(iIndex).Top = 271
                ' Extrathumbs Checkbox
                Me.chkImageET(iIndex) = New CheckBox()
                Me.chkImageET(iIndex).Name = iIndex.ToString
                Me.chkImageET(iIndex).Size = New Size(122, 16)
                Me.chkImageET(iIndex).AutoSize = False
                Me.chkImageET(iIndex).BackColor = Color.White
                Me.chkImageET(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.chkImageET(iIndex).Text = "Extrathumb"
                Me.chkImageET(iIndex).Left = 2
                Me.chkImageET(iIndex).Top = 2
                Me.chkImageET(iIndex).Checked = isChecked
                ' Extrafanarts Panel (fake button)
                Me.pnlImageEF(iIndex) = New Panel()
                Me.pnlImageEF(iIndex).Name = iIndex.ToString
                Me.pnlImageEF(iIndex).Size = New System.Drawing.Size(124, 20)
                Me.pnlImageEF(iIndex).BorderStyle = BorderStyle.FixedSingle
                Me.pnlImageEF(iIndex).BackColor = Color.White
                Me.pnlImageEF(iIndex).Left = 128
                Me.pnlImageEF(iIndex).Top = 271
                ' Extrafanarts Checkbox
                Me.chkImageEF(iIndex) = New CheckBox()
                Me.chkImageEF(iIndex).Name = iIndex.ToString
                Me.chkImageEF(iIndex).Size = New Size(122, 16)
                Me.chkImageEF(iIndex).AutoSize = False
                Me.chkImageEF(iIndex).BackColor = Color.White
                Me.chkImageEF(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.chkImageEF(iIndex).Text = "Extrafanart"
                Me.chkImageEF(iIndex).Left = 2
                Me.chkImageEF(iIndex).Top = 2
                Me.chkImageEF(iIndex).Checked = isChecked
                ' Controls
                Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
                Me.pnlImageET(iIndex).Controls.Add(Me.chkImageET(iIndex))
                Me.pnlImageEF(iIndex).Controls.Add(Me.chkImageEF(iIndex))
                Me.pnlImage(iIndex).Controls.Add(Me.pnlImageET(iIndex))
                Me.pnlImage(iIndex).Controls.Add(Me.pnlImageEF(iIndex))
                AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            Else
                ReDim Preserve Me.lblImage(iIndex)
                Me.lblImage(iIndex) = New Label()
                Me.lblImage(iIndex).Name = iIndex.ToString
                Me.lblImage(iIndex).Size = New Size(250, 40)
                Me.lblImage(iIndex).AutoSize = False
                Me.lblImage(iIndex).BackColor = Color.White
                Me.lblImage(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                Me.lblImage(iIndex).Text = text
                Me.lblImage(iIndex).Tag = poster
                Me.lblImage(iIndex).Left = 2
                Me.lblImage(iIndex).Top = 254
                Me.pnlImage(iIndex).Controls.Add(Me.lblImage(iIndex))
                AddHandler lblImage(iIndex).Click, AddressOf lblImage_Click
                AddHandler lblImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iCounter += 1

        If Me.iCounter = 3 Then
            Me.iCounter = 0
            Me.iLeft = 5
            Me.iTop += 302
        Else
            Me.iLeft += 271
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PreviewImage()
    End Sub

    Private Sub PreviewImage()
        Try
            Dim tImage As New Images
            Dim tImg As MediaContainers.Image = Nothing
            Me.pnlDLStatus.Visible = False

            Application.DoEvents()

            If tmpImage.WebImage IsNot Nothing AndAlso tmpImage.WebImage.Image Is Nothing Then
                tmpImage.WebImage.FromWeb(tmpImage.URL)
            End If
            tImage = tmpImage.WebImage


            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.Image)

            'tImage.Dispose()
            tImage = Nothing

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwImgDownload_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDownload.DoWork
        Dim i As Integer = 0

        ' Only thumbs are downloaded to be shown - green internet optimization :)
        For Each aImg In _ImageList
            Try
                If Not String.IsNullOrEmpty(aImg.ThumbURL) Then
                    aImg.WebThumb.FromWeb(aImg.ThumbURL)
                    If Me.bwImgDownload.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If
                Else
                    aImg.WebImage.FromWeb(aImg.URL)
                    If Me.bwImgDownload.CancellationPending Then
                        e.Cancel = True
                        Return
                    End If
                End If
                Me.bwImgDownload.ReportProgress(i + 1, aImg.URL)
                i += 1
                Application.DoEvents()
            Catch
            End Try
        Next
    End Sub

    Private Sub bwImgDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDownload.ProgressChanged
        Try
            Dim sStatus As String = e.UserState.ToString
            Me.lblDL1Status.Text = String.Format(Master.eLang.GetString(886, "Downloading {0}"), If(sStatus.Length > 40, StringUtils.TruncateURL(sStatus, 40), sStatus))
            Me.pbDL1.Value = e.ProgressPercentage
            Me.Refresh()
            Application.DoEvents()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwImgDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDownload.RunWorkerCompleted
        If Not e.Cancelled Then
            isWorkerDone = True
            FillListView()
        End If
    End Sub

    Private Sub FillListView()
        Try
            Application.DoEvents()
            Me.pnlDLStatus.Visible = True
            Me.pnlBG.Visible = False

            Me.pbDL1.Maximum = _ImageList.Count

            Me.bwImgLoading.WorkerSupportsCancellation = True
            Me.bwImgLoading.WorkerReportsProgress = True
            Me.bwImgLoading.RunWorkerAsync()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

    End Sub

    Private Sub bwImgLoading_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgLoading.DoWork

        Dim text As String = String.Empty
        Dim aParentID As String = String.Empty
        Dim i As Integer = 0
        Dim tImg As MediaContainers.Image = Nothing

        If Not isWorkerDone Then
            Exit Sub
        End If

        Me.SuspendLayout()
        Me.pnlBG.AutoScroll = False

        For Each aImg In _ImageList.Where(Function(f) f.WebThumb.Image IsNot Nothing OrElse f.WebImage.Image IsNot Nothing)
            Try
                If Not aImg.Height = "n/a" AndAlso Not aImg.Width = "n/a" Then
                    text = String.Concat(String.Format("{0}x{1}", aImg.Width.ToString, aImg.Height.ToString), Environment.NewLine, aImg.LongLang)
                ElseIf aImg.WebThumb.Image Is Nothing AndAlso aImg.WebImage.Image IsNot Nothing Then
                    'If no WebThumb is present, the image is loaded in full resolution and sizes can be used from WebThumb.
                    text = String.Concat(String.Format("{0}x{1}", aImg.WebImage.Image.Width.ToString, aImg.WebImage.Image.Height.ToString), Environment.NewLine, aImg.LongLang)
                Else
                    text = String.Concat("n/a", Environment.NewLine, aImg.LongLang)
                End If
                tImg = aImg
                Me.Invoke(New LoadImage(AddressOf AddImage), i, tImg.IsChecked, tImg, text)
                If Me.bwImgLoading.CancellationPending Then
                    e.Cancel = True
                    Return
                End If
                Me.bwImgLoading.ReportProgress(i + 1)
                i = i + 1
                Application.DoEvents()
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        Next
    End Sub

    Private Sub bwImgLoading_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgLoading.ProgressChanged
        Try
            Me.lblDL1Status.Text = String.Format(Master.eLang.GetString(321, "Preparing preview. Please wait..."))
            Me.pbDL1.Value = e.ProgressPercentage
            Application.DoEvents()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub bwImgLoading_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgLoading.RunWorkerCompleted
        MeActivate()
    End Sub

    Private Sub MeActivate()
        If (Me.InvokeRequired) Then
            Me.Invoke(New Delegate_MeActivate(AddressOf MeActivate))
            Exit Sub
        End If
        Me.pnlBG.Visible = True
        Me.pnlDLStatus.Visible = False
        Application.DoEvents()
        Me.ResumeLayout(True)
        Me.pnlBG.AutoScroll = True
        Me.Activate()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If bwImgDownload.IsBusy Then bwImgDownload.CancelAsync()
        If bwImgLoading.IsBusy Then bwImgLoading.CancelAsync()

        While bwImgDownload.IsBusy OrElse bwImgLoading.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Results = Nothing
        Me.Close()
    End Sub

    Private Sub DoSelect(ByVal iIndex As Integer, poster As MediaContainers.Image)
        Try
            'set all pnl colors to white first
            'remove all the current genres
            For i As Integer = 0 To Me.pnlImage.Count - 1
                Me.pnlImage(i).BackColor = Color.White
                Me.lblImage(i).BackColor = Color.White
                Me.lblImage(i).ForeColor = Color.Black
            Next

            'set selected pnl color to blue
            Me.pnlImage(iIndex).BackColor = Color.Blue

            Me.lblImage(iIndex).BackColor = Color.Blue
            Me.lblImage(iIndex).ForeColor = Color.White

            Me.selIndex = iIndex
            Me.tmpImage = poster

            Me.OK_Button.Enabled = True
            Me.OK_Button.Focus()

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Function GetEThumbsURL(ByVal iIndex As Integer, poster As MediaContainers.Image) As String 'TODO: fix
        Dim eURL As String = String.Empty

        Select Case Master.eSettings.MovieEThumbsPrefSize
            Case Enums.MovieFanartSize.Any
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.HD1080
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.HD720
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.Thumb
                eURL = ":" & _ImageList.Item(iIndex).ThumbURL
        End Select

        Return eURL
    End Function

    Private Function GetEFanartsURL(ByVal iIndex As Integer, poster As MediaContainers.Image) As String 'TODO: fix
        Dim eURL As String = String.Empty

        Select Case Master.eSettings.MovieEFanartsPrefSize
            Case Enums.MovieFanartSize.Any
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.HD1080
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.HD720
                eURL = ":" & _ImageList.Item(iIndex).URL
            Case Enums.MovieFanartSize.Thumb
                eURL = ":" & _ImageList.Item(iIndex).ThumbURL
        End Select

        Return eURL
    End Function

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim vScrollPosition As Integer = pnlBG.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlBG.VerticalScroll.Maximum)
        pnlBG.AutoScrollPosition = New Point(pnlBG.AutoScrollPosition.X, vScrollPosition)
        pnlBG.Invalidate()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Me.tmpImage.WebImage.Image IsNot Nothing Then
                Me.pnlBG.Visible = False

                Me.Refresh()
                Application.DoEvents()
                Results = Me.tmpImage
                Results.WebImage.IsEdit = Me.isEdit
            Else
                Me.tmpImage.WebImage.FromWeb(Me.tmpImage.URL)
                Results = Me.tmpImage
                Results.WebImage.IsEdit = Me.isEdit
            End If

            If Me.DLType = Enums.ModifierType.Fanart Then
                Dim iMod As Integer = 0
                Dim iVal As Integer = 1
                Dim etPath As String = String.Empty
                Dim efPath As String = String.Empty

                If Me.chkImageEF.Where(Function(f) f.Checked).Count > 0 Then
                    For i As Integer = 0 To Me.chkImageEF.Count - 1
                        If Me.chkImageEF(i).Checked Then
                            efList.Add(GetEFanartsURL(i, CType(Me.pbImage(i).Tag, MediaContainers.Image)))
                        End If
                    Next
                End If
                If Me.chkImageET.Where(Function(f) f.Checked).Count > 0 Then
                    For i As Integer = 0 To Me.chkImageET.Count - 1
                        If Me.chkImageET(i).Checked Then
                            etList.Add(GetEThumbsURL(i, CType(Me.pbImage(i).Tag, MediaContainers.Image)))
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbImage_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PreviewImage()
    End Sub

    Private Sub pbImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pnlImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, Panel).Name), DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub lblImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelect(Convert.ToInt32(DirectCast(sender, Label).Name), DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image))
    End Sub

    Private Sub SetUp()
        Try
            AddHandler MyBase.MouseWheel, AddressOf MouseWheelEvent
            AddHandler pnlBG.MouseWheel, AddressOf MouseWheelEvent

            Functions.PNLDoubleBuffer(Me.pnlBG)

            Dim Title As String

            If tIsMovie Then
                Title = If(Not String.IsNullOrEmpty(Me.tMovie.Movie.Title), Me.tMovie.Movie.Title, Me.tMovie.ListTitle)
            Else
                Title = Me.tMovieSet.ListTitle
            End If

            If Me.DLType = Enums.ModifierType.Poster Then
                Me.Text = String.Concat(Master.eLang.GetString(877, "Select Poster"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.Banner Then
                Me.Text = String.Concat(Master.eLang.GetString(1064, "Select Banner"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.Landscape Then
                Me.Text = String.Concat(Master.eLang.GetString(1065, "Select Landscape"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.Fanart Then
                Me.Text = String.Concat(Master.eLang.GetString(878, "Select Fanart"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.ClearArt Then
                Me.Text = String.Concat(Master.eLang.GetString(1109, "Select ClearArt"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.ClearLogo Then
                Me.Text = String.Concat(Master.eLang.GetString(1110, "Select ClearLogo"), " - ", Title)
                Me.pnlDwld.Visible = True
            ElseIf Me.DLType = Enums.ModifierType.DiscArt Then
                Me.Text = String.Concat(Master.eLang.GetString(1111, "Select DiscArt"), " - ", Title)
                Me.pnlDwld.Visible = True
            End If

            Me.pnlDwld.Visible = True

            Me.OK_Button.Text = Master.eLang.GetString(179, "OK")
            Me.Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
            Me.lblDL1.Text = Master.eLang.GetString(894, "Performing Preliminary Tasks...")
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region 'Methods

End Class