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

Public Class dlgImgSelectNew


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwImgDefaults As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwImgDownload As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwImgLoading As New System.ComponentModel.BackgroundWorker

    Public Delegate Sub LoadImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

    'Private CachePath As String = String.Empty
    'Private DLType As Enums.ModifierType
    Private isWorkerDone As Boolean = False
    'Private ETHashes As New List(Of String)
    Private iImageCounter As Integer = 0
    Private iLeft As Integer = 3

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private iTop As Integer = 3
    Private lblTopImageType() As Label
    Private lblTopImageDetails() As Label

    'Private noImages As Boolean = False
    Private pbTopImage() As PictureBox
    Private pnlTopImage() As Panel
    'Private PreDL As Boolean = False
    Private selIndex As Integer = -1

    Private tIsMovie As Boolean
    Private tDBElement As New Database.DBElement
    Private tmpImage As New MediaContainers.Image
    Private tmpImageEF As New MediaContainers.Image
    Private tmpImageET As New MediaContainers.Image

    Private tDefaultImagesContainer As New MediaContainers.ImagesContainer
    Private tDefaultEpisodeImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tDefaultSeasonImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tSearchResultsContainer As New MediaContainers.SearchResultsContainer
    Private tmpResultDBElement As New Database.DBElement

    Private tScrapeModifier As New Structures.ScrapeModifier
    Private tContentType As Enums.ContentType

#End Region 'Fields

#Region "Properties"

    Public Property Result As Database.DBElement
        Get
            Return tmpResultDBElement
        End Get
        Set(value As Database.DBElement)
            tmpResultDBElement = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Me.Left = Master.AppPos.Left + (Master.AppPos.Width - Me.Width) \ 2
        Me.Top = Master.AppPos.Top + (Master.AppPos.Height - Me.Height) \ 2
        Me.StartPosition = FormStartPosition.Manual
    End Sub

    Public Overloads Function ShowDialog(ByVal DBElement As Database.DBElement, ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal ContentType As Enums.ContentType, Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.tSearchResultsContainer = SearchResultsContainer
        Me.tDBElement = DBElement
        Me.tScrapeModifier = ScrapeModifier
        Me.tContentType = ContentType

        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler pnlImages.MouseWheel, AddressOf MouseWheelEvent
        'AddHandler MyBase.MouseWheel, AddressOf MouseWheelEvent
        'AddHandler tvList.MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(Me.pnlImgSelectMain)

        Me.SetUp()
    End Sub

    Private Sub dlgImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.bwImgDefaults.WorkerReportsProgress = True
        Me.bwImgDefaults.WorkerSupportsCancellation = True
        Me.bwImgDefaults.RunWorkerAsync()
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        Me.SetDefaults()
        e.Cancel = Me.DownloadDefaultImages
    End Sub

    Private Sub bwImgDefaults_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDefaults.ProgressChanged
        If e.UserState.ToString = "progress" Then
            'Me.pbStatus.Value = e.ProgressPercentage
        ElseIf e.UserState.ToString = "current" Then
            'Me.lblStatus.Text = Master.eLang.GetString(953, "Loading Current Images...")
            'Me.pbStatus.Value = 0
            'Me.pbStatus.Maximum = e.ProgressPercentage
        Else
            'Me.pbStatus.Value = 0
            'Me.pbStatus.Maximum = e.ProgressPercentage
        End If
    End Sub

    Private Sub bwImgDefaults_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDefaults.RunWorkerCompleted
        If Not e.Cancelled Then
            Me.GenerateList()

            'Me.lblStatus.Text = Master.eLang.GetString(954, "(Down)Loading New Images...")
            Me.bwImgDownload.WorkerReportsProgress = True
            Me.bwImgDownload.WorkerSupportsCancellation = True
            Me.bwImgDownload.RunWorkerAsync()
        End If
    End Sub

    Public Function SetDefaults() As Boolean
        Images.SetDefaultImages(tDBElement, tDefaultImagesContainer, tSearchResultsContainer, tScrapeModifier, tContentType, tDefaultSeasonImagesContainer, tDefaultEpisodeImagesContainer)
        Return False
    End Function

    Private Function DownloadDefaultImages() As Boolean

        'Banner
        If Me.tScrapeModifier.MainBanner AndAlso Me.tDBElement.ImagesContainer.Banner.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.Banner, False)
        End If

        'CharacterArt
        If Me.tScrapeModifier.MainCharacterArt AndAlso Me.tDBElement.ImagesContainer.CharacterArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.CharacterArt, False)
        End If

        'ClearArt
        If Me.tScrapeModifier.MainClearArt AndAlso Me.tDBElement.ImagesContainer.ClearArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.ClearArt, False)
        End If

        'ClearLogo
        If Me.tScrapeModifier.MainClearLogo AndAlso Me.tDBElement.ImagesContainer.ClearLogo.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.ClearLogo, False)
        End If

        'DiscArt
        If Me.tScrapeModifier.MainDiscArt AndAlso Me.tDBElement.ImagesContainer.DiscArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.DiscArt, False)
        End If

        'Fanart
        If Me.tScrapeModifier.MainFanart AndAlso Me.tDBElement.ImagesContainer.Fanart.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.Fanart, False)
        End If

        'Landscape
        If Me.tScrapeModifier.MainLandscape AndAlso Me.tDBElement.ImagesContainer.Landscape.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.Landscape, False)
        End If

        'Poster
        If Me.tScrapeModifier.MainPoster AndAlso Me.tDBElement.ImagesContainer.Poster.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElement.ImagesContainer.Poster, False)
        End If
    End Function

    Private Sub DownloadAndCache(ByRef tImage As MediaContainers.Image, ByVal withCache As Boolean)
        If tImage.WebImage.Image Is Nothing Then
            If File.Exists(tImage.LocalFile) Then
                tImage.WebImage.FromFile(tImage.LocalFile)
            ElseIf File.Exists(tImage.LocalThumb) Then
                tImage.WebThumb.FromFile(tImage.LocalThumb)
            Else
                If Not String.IsNullOrEmpty(tImage.ThumbURL) Then
                    tImage.WebThumb.FromWeb(tImage.ThumbURL)
                    If tImage.WebThumb.Image IsNot Nothing AndAlso withCache Then
                        Directory.CreateDirectory(Directory.GetParent(tImage.LocalThumb).FullName)
                        tImage.WebThumb.Save(tImage.LocalThumb)
                    End If
                ElseIf Not String.IsNullOrEmpty(tImage.URL) Then
                    tImage.WebImage.FromWeb(tImage.URL)
                    If tImage.WebImage.Image IsNot Nothing AndAlso withCache Then
                        Directory.CreateDirectory(Directory.GetParent(tImage.LocalFile).FullName)
                        tImage.WebImage.Save(tImage.LocalFile)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SetUp()
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

    Private Sub GenerateList()
        Dim iCount As Integer = 0
        If tScrapeModifier.MainPoster AndAlso Master.eSettings.MoviePosterAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.Poster, iCount, Enums.ModifierType.MainPoster)
            iCount += 1
        End If
        If tScrapeModifier.MainFanart AndAlso Master.eSettings.MovieFanartAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.Fanart, iCount, Enums.ModifierType.MainFanart)
            iCount += 1
        End If
        If tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.Banner, iCount, Enums.ModifierType.MainBanner)
            iCount += 1
        End If
        If tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.ClearArt, iCount, Enums.ModifierType.MainClearArt)
            iCount += 1
        End If
        If tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.ClearLogo, iCount, Enums.ModifierType.MainClearLogo)
            iCount += 1
        End If
        If tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.DiscArt, iCount, Enums.ModifierType.MainDiscArt)
            iCount += 1
        End If
        If tScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled Then
            AddTopImage(tDBElement.ImagesContainer.Landscape, iCount, Enums.ModifierType.MainLandscape)
            iCount += 1
        End If
    End Sub

    Private Sub AddTopImage(ByRef fTag As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType)
        Try
            Dim strImageType As String = String.Empty
            Dim strDescription As String = String.Empty

            Select Case ModifierType
                Case Enums.ModifierType.MainBanner
                    strImageType = "Banner"
                Case Enums.ModifierType.MainCharacterArt
                    strImageType = "CharacterArt"
                Case Enums.ModifierType.MainClearArt
                    strImageType = "ClearArt"
                Case Enums.ModifierType.MainClearLogo
                    strImageType = "ClearLogo"
                Case Enums.ModifierType.MainDiscArt
                    strImageType = "DiscArt"
                Case Enums.ModifierType.MainFanart
                    strImageType = "Fanart"
                Case Enums.ModifierType.MainLandscape
                    strImageType = "Landscape"
                Case Enums.ModifierType.MainPoster
                    strImageType = "Poster"
            End Select

            If fTag IsNot Nothing AndAlso fTag.WebImage IsNot Nothing AndAlso fTag.WebImage.Image IsNot Nothing Then
                Dim imgText As String = String.Empty
                If String.IsNullOrEmpty(fTag.Width) OrElse String.IsNullOrEmpty(fTag.Height) Then
                    strDescription = String.Format("{0}x{1}", fTag.WebImage.Image.Size.Width, fTag.WebImage.Image.Size.Height & Environment.NewLine & fTag.LongLang)
                Else
                    strDescription = String.Format("{0}x{1}", fTag.Width, fTag.Height & Environment.NewLine & fTag.LongLang)
                End If
            ElseIf fTag IsNot Nothing AndAlso fTag.WebThumb IsNot Nothing AndAlso fTag.WebThumb.Image IsNot Nothing Then
                Dim imgText As String = String.Empty
                If CDbl(fTag.Width) = 0 OrElse CDbl(fTag.Height) = 0 Then
                    strDescription = String.Concat("unknown", Environment.NewLine, fTag.LongLang)
                Else
                    strDescription = String.Format("{0}x{1}", fTag.Width, fTag.Height & Environment.NewLine & fTag.LongLang)
                End If
            End If

            ReDim Preserve Me.pnlTopImage(iIndex)
            ReDim Preserve Me.pbTopImage(iIndex)
            ReDim Preserve Me.lblTopImageType(iIndex)
            ReDim Preserve Me.lblTopImageDetails(iIndex)
            Me.pnlTopImage(iIndex) = New Panel()
            Me.pbTopImage(iIndex) = New PictureBox()
            Me.lblTopImageType(iIndex) = New Label()
            Me.lblTopImageDetails(iIndex) = New Label()
            Me.pbTopImage(iIndex).Name = iIndex.ToString
            Me.pnlTopImage(iIndex).Name = iIndex.ToString
            Me.lblTopImageType(iIndex).Name = iIndex.ToString
            Me.lblTopImageDetails(iIndex).Name = iIndex.ToString
            Me.pnlTopImage(iIndex).Size = New Size(150, 150)
            Me.pbTopImage(iIndex).Size = New Size(142, 102)
            Me.lblTopImageType(iIndex).Size = New Size(142, 20)
            Me.lblTopImageDetails(iIndex).Size = New Size(142, 20)
            Me.pnlTopImage(iIndex).BackColor = Color.White
            Me.pnlTopImage(iIndex).BorderStyle = BorderStyle.FixedSingle
            Me.pbTopImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
            Me.lblTopImageType(iIndex).AutoSize = False
            Me.lblTopImageType(iIndex).BackColor = Color.White
            'Me.lblTopImageType(iIndex).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblTopImageType(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblTopImageType(iIndex).Text = strImageType
            Me.lblTopImageDetails(iIndex).AutoSize = False
            Me.lblTopImageDetails(iIndex).BackColor = Color.White
            Me.lblTopImageDetails(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.lblTopImageDetails(iIndex).Text = strDescription
            Me.pbTopImage(iIndex).Image = If(fTag.WebImage.Image IsNot Nothing, CType(fTag.WebImage.Image.Clone(), Image), _
                                             If(fTag.WebThumb.Image IsNot Nothing, CType(fTag.WebThumb.Image.Clone(), Image), Nothing))
            Me.pnlTopImage(iIndex).Left = iLeft
            Me.pbTopImage(iIndex).Left = 3
            Me.lblTopImageType(iIndex).Left = 3
            Me.lblTopImageDetails(iIndex).Left = 3
            Me.pnlTopImage(iIndex).Top = 3
            Me.pbTopImage(iIndex).Top = 23
            Me.lblTopImageType(iIndex).Top = 0
            Me.lblTopImageDetails(iIndex).Top = 128
            Me.pnlTopImage(iIndex).Tag = fTag
            Me.pbTopImage(iIndex).Tag = fTag
            Me.lblTopImageType(iIndex).Tag = fTag
            Me.lblTopImageDetails(iIndex).Tag = fTag
            Me.pnlTopImages.Controls.Add(Me.pnlTopImage(iIndex))
            Me.pnlTopImage(iIndex).Controls.Add(Me.pbTopImage(iIndex))
            Me.pnlTopImage(iIndex).Controls.Add(Me.lblTopImageType(iIndex))
            Me.pnlTopImage(iIndex).Controls.Add(Me.lblTopImageDetails(iIndex))
            Me.pnlTopImage(iIndex).BringToFront()
            AddHandler pbTopImage(iIndex).Click, AddressOf pbTopImage_Click
            AddHandler pbTopImage(iIndex).DoubleClick, AddressOf pbTopImage_DoubleClick
            AddHandler pnlTopImage(iIndex).Click, AddressOf pnlTopImage_Click
            AddHandler lblTopImageType(iIndex).Click, AddressOf lblTopImage_Click
            AddHandler lblTopImageDetails(iIndex).Click, AddressOf lblTopImage_Click

            'AddHandler pbImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            'AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
            'AddHandler lblImage(iIndex).MouseWheel, AddressOf MouseWheelEvent

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Me.iLeft += 156
    End Sub

    Private Sub pbTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image))
    End Sub

    Private Sub pbTopImage_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImages As New Images
        Dim iTag As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, MediaContainers.Image)
        DownloadFullsizeImage(iTag, tImages)

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImages.Image)
    End Sub

    Private Sub pnlTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, MediaContainers.Image))
    End Sub

    Private Sub lblTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, MediaContainers.Image))
    End Sub

    Private Sub DoSelectTopImage(ByVal iIndex As Integer, ByVal SelTag As MediaContainers.Image)
        Try
            For i As Integer = 0 To Me.pnlTopImage.Count - 1
                Me.pnlTopImage(i).BackColor = Color.White
                Me.lblTopImageDetails(i).BackColor = Color.White
                Me.lblTopImageDetails(i).ForeColor = Color.Black
                Me.lblTopImageType(i).BackColor = Color.White
                Me.lblTopImageType(i).ForeColor = Color.Black
            Next

            Me.pnlTopImage(iIndex).BackColor = Color.Blue
            Me.lblTopImageDetails(iIndex).BackColor = Color.Blue
            Me.lblTopImageDetails(iIndex).ForeColor = Color.White
            Me.lblTopImageType(iIndex).BackColor = Color.Blue
            Me.lblTopImageType(iIndex).ForeColor = Color.White

            'SetImage(SelTag)

            'Me.CheckCurrentImage()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Sub DownloadFullsizeImage(ByRef iTag As MediaContainers.Image, ByRef tImage As Images)
        If Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso File.Exists(iTag.LocalFile) Then
            tImage.FromFile(iTag.LocalFile)
        ElseIf Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso Not String.IsNullOrEmpty(iTag.URL) Then
            'Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            'Me.pbStatus.Style = ProgressBarStyle.Marquee
            'Me.pnlStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URL)
            If tImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(iTag.LocalFile).FullName)
                tImage.Save(iTag.LocalFile)
            End If

            'Me.pnlStatus.Visible = False
        End If

    End Sub

#End Region 'Methods

End Class