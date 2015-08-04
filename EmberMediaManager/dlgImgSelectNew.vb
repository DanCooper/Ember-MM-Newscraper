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

    'MainImageList
    Private iMainImage_Left As Integer = 5
    Private iMainImage_Top As Integer = 5
    Private iMainImage_Counter As Integer = 0
    Private lblMainImageDetails() As Label
    Private pbMainImage() As PictureBox
    Private pnlMainImage() As Panel

    'TopImage
    Private iTopImage_Left As Integer = 3

    Private isEdit As Boolean = False
    'Private isShown As Boolean = False
    Private lblTopImageType() As Label
    Private lblTopImageDetails() As Label

    'Private noImages As Boolean = False
    Private pbTopImage() As PictureBox
    Private pnlTopImage() As Panel
    'Private PreDL As Boolean = False
    Private selIndex As Integer = -1
    Private currListImageType As Enums.ModifierType = Enums.ModifierType.All
    Private currSubImageType As Enums.ModifierType = Enums.ModifierType.All
    Private currTopImageType As Enums.ModifierType = Enums.ModifierType.All

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

    Private Sub dlgImgSelectNew_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        tmrReorderMainList.Stop()
        tmrReorderMainList.Start()
        Debug.WriteLine(String.Concat(Me.Size.Width, "x", Me.Size.Height))
    End Sub

    Private Sub tmrReorderMainList_Tick(sender As Object, e As EventArgs) Handles tmrReorderMainList.Tick
        tmrReorderMainList.Stop()
        ReorderMainList()
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

    Private Sub bwImgDownload_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDownload.DoWork
        e.Cancel = Me.DownloadAllImages()
    End Sub

    Private Sub bwImgDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDownload.ProgressChanged
        'If e.UserState.ToString = "progress" Then
        '    Me.pbStatus.Value = e.ProgressPercentage
        'ElseIf e.UserState.ToString = "defaults" Then
        '    Me.lblStatus.Text = Master.eLang.GetString(955, "Setting Defaults...")
        '    Me.pbStatus.Value = 0
        '    Me.pbStatus.Maximum = e.ProgressPercentage
        'Else
        '    Me.pbStatus.Value = 0
        '    Me.pbStatus.Maximum = e.ProgressPercentage
        'End If
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

    Private Function DownloadAllImages() As Boolean

        'Episode Fanarts
        If Me.tScrapeModifier.EpisodeFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Episode Posters
        If Me.tScrapeModifier.EpisodePoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Season Poster / AllSeasons Poster
        If Me.tScrapeModifier.SeasonPoster OrElse Me.tScrapeModifier.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Season Banner / AllSeasons Banner
        If Me.tScrapeModifier.SeasonBanner OrElse Me.tScrapeModifier.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Season Fanart  /AllSeasons Fanart
        If Me.tScrapeModifier.SeasonFanart OrElse Me.tScrapeModifier.AllSeasonsFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Season Landscape  /AllSeasons Landscape
        If Me.tScrapeModifier.SeasonLandscape OrElse Me.tScrapeModifier.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show Poster / AllSeasons Poster
        If Me.tScrapeModifier.MainPoster OrElse Me.tScrapeModifier.AllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainPosters
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show Banner / AllSeasons Banner
        If Me.tScrapeModifier.MainBanner OrElse Me.tScrapeModifier.AllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainBanners
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show CharacterArt
        If Me.tScrapeModifier.MainCharacterArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show ClearArt
        If Me.tScrapeModifier.MainClearArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show ClearLogo
        If Me.tScrapeModifier.MainClearLogo Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show Landscape / AllSeasons Landscape
        If Me.tScrapeModifier.MainLandscape OrElse Me.tScrapeModifier.AllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        'Show Fanart / AllSeasons Fanart / Season Fanart / Episode Fanart
        If Me.tScrapeModifier.MainFanart OrElse Me.tScrapeModifier.AllSeasonsFanart OrElse Me.tScrapeModifier.SeasonFanart OrElse Me.tScrapeModifier.EpisodeFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                DownloadAndCache(tImg, False)
                'If Me.bwLoadImages.CancellationPending Then
                '    Return True
                'End If
                'Me.bwLoadImages.ReportProgress(IProgress, "progress")
                'IProgress += 1
            Next
        End If

        Return False
    End Function

    Private Sub DownloadAndCache(ByRef tImage As MediaContainers.Image, ByVal doCache As Boolean, Optional needFullsize As Boolean = False)
        If tImage.WebImage.Image Is Nothing Then
            If File.Exists(tImage.LocalFile) Then
                tImage.WebImage.FromFile(tImage.LocalFile)
            ElseIf File.Exists(tImage.LocalThumb) AndAlso Not needFullsize Then
                tImage.WebThumb.FromFile(tImage.LocalThumb)
            Else
                If Not String.IsNullOrEmpty(tImage.ThumbURL) AndAlso Not needFullsize Then
                    tImage.WebThumb.FromWeb(tImage.ThumbURL)
                    If tImage.WebThumb.Image IsNot Nothing AndAlso doCache Then
                        Directory.CreateDirectory(Directory.GetParent(tImage.LocalThumb).FullName)
                        tImage.WebThumb.Save(tImage.LocalThumb)
                    End If
                ElseIf Not String.IsNullOrEmpty(tImage.URL) Then
                    tImage.WebImage.FromWeb(tImage.URL)
                    If tImage.WebImage.Image IsNot Nothing AndAlso doCache Then
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

    Private Sub AddTopImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType)
        Dim strImageType As String = String.Empty
        Dim strDescription As String = String.Empty
        Dim tTag As New iTag

        Select Case ModifierType
            Case Enums.ModifierType.MainBanner
                strImageType = "Banner"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainBanner}
            Case Enums.ModifierType.MainCharacterArt
                strImageType = "CharacterArt"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainCharacterArt}
            Case Enums.ModifierType.MainClearArt
                strImageType = "ClearArt"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainClearArt}
            Case Enums.ModifierType.MainClearLogo
                strImageType = "ClearLogo"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainClearLogo}
            Case Enums.ModifierType.MainDiscArt
                strImageType = "DiscArt"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainDiscArt}
            Case Enums.ModifierType.MainFanart
                strImageType = "Fanart"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainFanart}
            Case Enums.ModifierType.MainLandscape
                strImageType = "Landscape"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainLandscape}
            Case Enums.ModifierType.MainPoster
                strImageType = "Poster"
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainPoster}
        End Select

        If tImage IsNot Nothing AndAlso tImage.WebImage IsNot Nothing AndAlso tImage.WebImage.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If String.IsNullOrEmpty(tImage.Width) OrElse String.IsNullOrEmpty(tImage.Height) Then
                strDescription = String.Format("{0}x{1}", tImage.WebImage.Image.Size.Width, tImage.WebImage.Image.Size.Height)
            Else
                strDescription = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        ElseIf tImage IsNot Nothing AndAlso tImage.WebThumb IsNot Nothing AndAlso tImage.WebThumb.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                strDescription = String.Concat("unknown", Environment.NewLine, tImage.LongLang)
            Else
                strDescription = String.Format("{0}x{1}", tImage.Width, tImage.Height)
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
        Me.pbTopImage(iIndex).Image = If(tImage.WebImage.Image IsNot Nothing, CType(tImage.WebImage.Image.Clone(), Image), _
                                         If(tImage.WebThumb.Image IsNot Nothing, CType(tImage.WebThumb.Image.Clone(), Image), Nothing))
        Me.pnlTopImage(iIndex).Left = iTopImage_Left
        Me.pbTopImage(iIndex).Left = 3
        Me.lblTopImageType(iIndex).Left = 3
        Me.lblTopImageDetails(iIndex).Left = 3
        Me.pnlTopImage(iIndex).Top = 3
        Me.pbTopImage(iIndex).Top = 23
        Me.lblTopImageType(iIndex).Top = 0
        Me.lblTopImageDetails(iIndex).Top = 128
        Me.pnlTopImage(iIndex).Tag = tTag
        Me.pbTopImage(iIndex).Tag = tTag
        Me.lblTopImageType(iIndex).Tag = tTag
        Me.lblTopImageDetails(iIndex).Tag = tTag
        Me.pnlTopImages.Controls.Add(Me.pnlTopImage(iIndex))
        Me.pnlTopImage(iIndex).Controls.Add(Me.pbTopImage(iIndex))
        Me.pnlTopImage(iIndex).Controls.Add(Me.lblTopImageType(iIndex))
        Me.pnlTopImage(iIndex).Controls.Add(Me.lblTopImageDetails(iIndex))
        Me.pnlTopImage(iIndex).BringToFront()
        AddHandler pbTopImage(iIndex).Click, AddressOf pbTopImage_Click
        AddHandler pbTopImage(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlTopImage(iIndex).Click, AddressOf pnlTopImage_Click
        AddHandler lblTopImageType(iIndex).Click, AddressOf lblTopImage_Click
        AddHandler lblTopImageDetails(iIndex).Click, AddressOf lblTopImage_Click

        'AddHandler pbImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
        'AddHandler pnlImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
        'AddHandler lblImage(iIndex).MouseWheel, AddressOf MouseWheelEvent

        Me.iTopImage_Left += 156
    End Sub

    Private Sub AddMainImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType)
        Dim sDescription As String = String.Empty
        Dim tTag As New iTag

        Select Case ModifierType
            Case Enums.ModifierType.MainBanner
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainBanner}
            Case Enums.ModifierType.MainCharacterArt
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainCharacterArt}
            Case Enums.ModifierType.MainClearArt
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainClearArt}
            Case Enums.ModifierType.MainClearLogo
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainClearLogo}
            Case Enums.ModifierType.MainDiscArt
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainDiscArt}
            Case Enums.ModifierType.MainFanart
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainFanart}
            Case Enums.ModifierType.MainLandscape
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainLandscape}
            Case Enums.ModifierType.MainPoster
                tTag = New iTag With {.Image = tImage, .ImageType = Enums.ModifierType.MainPoster}
        End Select

        If tImage IsNot Nothing AndAlso tImage.WebImage IsNot Nothing AndAlso tImage.WebImage.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                sDescription = String.Format("{0}x{1}", tImage.WebImage.Image.Size.Width, tImage.WebImage.Image.Size.Height & Environment.NewLine & tImage.LongLang)
            Else
                sDescription = String.Format("{0}x{1}", tImage.Width, tImage.Height & Environment.NewLine & tImage.LongLang)
            End If
        ElseIf tImage IsNot Nothing AndAlso tImage.WebThumb IsNot Nothing AndAlso tImage.WebThumb.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                sDescription = String.Concat("unknown", Environment.NewLine, tImage.LongLang)
            Else
                sDescription = String.Format("{0}x{1}", tImage.Width, tImage.Height & Environment.NewLine & tImage.LongLang)
            End If
        End If

        ReDim Preserve Me.pnlMainImage(iIndex)
        ReDim Preserve Me.pbMainImage(iIndex)
        ReDim Preserve Me.lblMainImageDetails(iIndex)
        Me.pnlMainImage(iIndex) = New Panel()
        Me.pbMainImage(iIndex) = New PictureBox()
        Me.lblMainImageDetails(iIndex) = New Label()
        Me.pbMainImage(iIndex).Name = iIndex.ToString
        Me.pnlMainImage(iIndex).Name = iIndex.ToString
        Me.lblMainImageDetails(iIndex).Name = iIndex.ToString
        Me.pnlMainImage(iIndex).Size = New Size(187, 187)
        Me.pbMainImage(iIndex).Size = New Size(181, 151)
        Me.lblMainImageDetails(iIndex).Size = New Size(181, 30)
        Me.pnlMainImage(iIndex).BackColor = Color.White
        Me.pnlMainImage(iIndex).BorderStyle = BorderStyle.FixedSingle
        Me.pbMainImage(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        Me.lblMainImageDetails(iIndex).AutoSize = False
        Me.lblMainImageDetails(iIndex).BackColor = Color.White
        Me.lblMainImageDetails(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMainImageDetails(iIndex).Text = sDescription
        Me.pbMainImage(iIndex).Image = If(tImage.WebImage.Image IsNot Nothing, CType(tImage.WebImage.Image.Clone(), Image), _
                                          If(tImage.WebThumb.Image IsNot Nothing, CType(tImage.WebThumb.Image.Clone(), Image), Nothing))
        Me.pnlMainImage(iIndex).Left = iMainImage_Left
        Me.pbMainImage(iIndex).Left = 3
        Me.lblMainImageDetails(iIndex).Left = 0
        Me.pnlMainImage(iIndex).Top = iMainImage_Top
        Me.pbMainImage(iIndex).Top = 3
        Me.lblMainImageDetails(iIndex).Top = 151
        Me.pnlMainImage(iIndex).Tag = tTag
        Me.pbMainImage(iIndex).Tag = tTag
        Me.lblMainImageDetails(iIndex).Tag = tTag
        Me.pnlImgSelectMain.Controls.Add(Me.pnlMainImage(iIndex))
        Me.pnlMainImage(iIndex).Controls.Add(Me.pbMainImage(iIndex))
        Me.pnlMainImage(iIndex).Controls.Add(Me.lblMainImageDetails(iIndex))
        Me.pnlMainImage(iIndex).BringToFront()
        'AddHandler pbMainImage(iIndex).Click, AddressOf pbImage_Click
        AddHandler pbMainImage(iIndex).DoubleClick, AddressOf Image_DoubleClick
        'AddHandler pnlMainImage(iIndex).Click, AddressOf pnlImage_Click
        'AddHandler lblMainImageDetails(iIndex).Click, AddressOf lblImage_Click

        'AddHandler pbMainImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
        'AddHandler pnlMainImage(iIndex).MouseWheel, AddressOf MouseWheelEvent
        'AddHandler lblMainImageDetails(iIndex).MouseWheel, AddressOf MouseWheelEvent

        Me.iMainImage_Counter += 1

        If Me.iMainImage_Left + 192 + 187 > Me.pnlImgSelectMain.Width - 20 Then
            Me.iMainImage_Counter = 0
            Me.iMainImage_Left = 5
            Me.iMainImage_Top += 192
        Else
            Me.iMainImage_Left += 192
        End If
    End Sub

    Private Sub ReorderMainList()
        Me.iMainImage_Counter = 0
        Me.iMainImage_Left = 5
        Me.iMainImage_Top = 5

        If Me.pnlImgSelectMain.Controls.Count > 0 Then
            Me.pnlImgSelectMain.SuspendLayout()
            Me.pnlImgSelectMain.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To Me.pnlMainImage.Count - 1
                If Me.pnlMainImage(iIndex) IsNot Nothing Then
                    Me.pnlMainImage(iIndex).Left = iMainImage_Left
                    Me.pnlMainImage(iIndex).Top = iMainImage_Top

                    Me.iMainImage_Counter += 1

                    If Me.iMainImage_Left + 192 + 187 > Me.pnlImgSelectMain.Width - 20 Then
                        Me.iMainImage_Counter = 0
                        Me.iMainImage_Left = 5
                        Me.iMainImage_Top += 192
                    Else
                        Me.iMainImage_Left += 192
                    End If
                End If
            Next
            Me.pnlImgSelectMain.ResumeLayout()
            Me.pnlImgSelectMain.Update()
        End If

    End Sub

    Private Sub pbTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub Image_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).Image
        DownloadAndCache(tImage, False, True)

        ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.WebImage.Image)
    End Sub

    Private Sub pnlTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub lblTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub DoSelectTopImage(ByVal iIndex As Integer, ByVal tTag As iTag)
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

        Me.currTopImageType = tTag.ImageType

        ShowImageList(tTag)

        'Me.CheckCurrentImage()
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

    Private Sub ShowImageList(ByRef tTag As iTag)
        If Not Me.currListImageType = tTag.ImageType Then
            Dim iCount As Integer = 0

            ClearMainImages()

            If tTag.ImageType = Enums.ModifierType.MainBanner Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainBanner)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainCharacterArt Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainCharacterArt)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainClearArt Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainClearArt)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainClearLogo Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainClearLogo)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainDiscArt Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainDiscArt)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainFanart Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainFanart)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainLandscape Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                    iCount += 1
                Next
            ElseIf tTag.ImageType = Enums.ModifierType.MainPoster Then
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    Me.AddMainImage(tImage, iCount, Enums.ModifierType.MainPoster)
                    iCount += 1
                Next
            End If
        End If
    End Sub

    Private Sub ClearMainImages()
        Me.iMainImage_Counter = 0
        Me.iMainImage_Left = 5
        Me.iMainImage_Top = 5

        If Me.pnlImgSelectMain.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlMainImage.Count - 1
                If Me.pnlMainImage(iIndex) IsNot Nothing Then
                    If Me.lblMainImageDetails(iIndex) IsNot Nothing AndAlso Me.pnlMainImage(iIndex).Contains(Me.lblMainImageDetails(iIndex)) Then Me.pnlMainImage(iIndex).Controls.Remove(Me.lblMainImageDetails(iIndex))
                    If Me.pbMainImage(iIndex) IsNot Nothing AndAlso Me.pnlMainImage(iIndex).Contains(Me.pbMainImage(iIndex)) Then Me.pnlMainImage(iIndex).Controls.Remove(Me.pbMainImage(iIndex))
                    If Me.pnlImgSelectMain.Contains(Me.pnlMainImage(iIndex)) Then Me.pnlImgSelectMain.Controls.Remove(Me.pnlMainImage(iIndex))
                End If
            Next
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure iTag

#Region "Fields"

        Dim Image As MediaContainers.Image
        Dim ImageType As Enums.ModifierType

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class