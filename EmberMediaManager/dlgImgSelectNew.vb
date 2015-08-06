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

    Public Delegate Sub LoadImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

    Private SelSeason As Integer = -1

    'ImageList
    Private iImageList_DistanceLeft As Integer = 3
    Private iImageList_DistanceTop As Integer = 3
    Private iImageList_NextLeft As Integer = 3
    Private iImageList_NextTop As Integer = 3
    Private iImageList_Location_DiscType As Point = New Point(3, 150)
    Private iImageList_Location_Image As Point = New Point(3, 15)
    Private iImageList_Location_Language As Point = New Point(3, 180)
    Private iImageList_Location_Resolution As Point = New Point(3, 165)
    Private iImageList_Location_Scraper As Point = New Point(3, 0)
    Private iImageList_Size_DiscType As Size = New Size(174, 15)
    Private iImageList_Size_Image As Size = New Size(174, 135)
    Private iImageList_Size_Language As Size = New Size(174, 15)
    Private iImageList_Size_Panel As Size = New Size(180, 200)
    Private iImageList_Size_Resolution As Size = New Size(174, 15)
    Private iImageList_Size_Scraper As Size = New Size(174, 15)

    Private lblImageList_DiscType() As Label
    Private lblImageList_Language() As Label
    Private lblImageList_Resolution() As Label
    Private lblImageList_Scraper() As Label
    Private pbImageList_Image() As PictureBox
    Private pnlImageList_Panel() As Panel

    'SubImage
    Private iSubImage_DistanceLeft As Integer = 3
    Private iSubImage_DistanceTop As Integer = 3
    Private iSubImage_NextTop As Integer = 3

    Private lblSubImage_Resolution() As Label
    Private lblSubImage_Title() As Label
    Private pbSubImage_Image() As PictureBox
    Private pnlSubImage_Panel() As Panel

    'TopImage
    Private iTopImage_DistanceLeft As Integer = 3
    Private iTopImage_DistanceTop As Integer = 3
    Private iTopImage_NextLeft As Integer = 3
    Private iTopImage_Location_Image As Point = New Point(3, 15)
    Private iTopImage_Location_Resolution As Point = New Point(3, 155)
    Private iTopImage_Location_Title As Point = New Point(3, 0)
    Private iTopImage_Size_Image As Size = New Size(174, 135)
    Private iTopImage_Size_Panel As Size = New Size(180, 175)
    Private iTopImage_Size_Resolution As Size = New Size(174, 15)
    Private iTopImage_Size_Title As Size = New Size(174, 15)

    Private lblTopImage_Resolution() As Label
    Private lblTopImage_Title() As Label
    Private pbTopImage_Image() As PictureBox
    Private pnlTopImage_Panel() As Panel

    'Parameters
    Private DoEpisodeFanart As Boolean = False
    Private DoEpisodePoster As Boolean = False
    Private DoMainBanner As Boolean = False
    Private DoMainCharacterArt As Boolean = False
    Private DoMainClearArt As Boolean = False
    Private DoMainClearLogo As Boolean = False
    Private DoMainDiscArt As Boolean = False
    Private DoMainFanart As Boolean = False
    Private DoMainLandscape As Boolean = False
    Private DoMainPoster As Boolean = False
    Private DoSeasonBanner As Boolean = False
    Private DoSeasonFanart As Boolean = False
    Private DoSeasonLandscape As Boolean = False
    Private DoSeasonPoster As Boolean = False

    Private currListImage As New iTag
    Private currSubImage As New iTag
    Private currTopImage As New iTag

    Private tDefaultImagesContainer As New MediaContainers.ImagesContainer
    Private tDefaultEpisodeImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tDefaultSeasonImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tSearchResultsContainer As New MediaContainers.SearchResultsContainer
    Private tDBElementResult As New Database.DBElement

    Private tScrapeModifier As New Structures.ScrapeModifier
    Private tContentType As Enums.ContentType

#End Region 'Fields

#Region "Properties"

    Public Property Result As Database.DBElement
        Get
            Return tDBElementResult
        End Get
        Set(value As Database.DBElement)
            tDBElementResult = value
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

        'Set panel sizes based on "Fields" settings
        Me.pnlTopImages.Height = iTopImage_DistanceTop + iTopImage_Size_Panel.Height + 20
    End Sub

    Public Overloads Function ShowDialog(ByVal DBElement As Database.DBElement, ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal ContentType As Enums.ContentType, Optional ByVal _isEdit As Boolean = False) As DialogResult
        Me.tSearchResultsContainer = SearchResultsContainer
        Me.tDBElementResult = DBElement
        Me.tScrapeModifier = ScrapeModifier
        Me.tContentType = ContentType
        SetParameters()

        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler Me.MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(Me.pnlImageList)

        Me.SetUp()
    End Sub

    Private Sub dlgImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblStatus.Text = Master.eLang.GetString(953, "Loading Current Images...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.lblStatus.Visible = True
        Me.pbStatus.Visible = True

        Me.bwImgDefaults.WorkerReportsProgress = False
        Me.bwImgDefaults.WorkerSupportsCancellation = True
        Me.bwImgDefaults.RunWorkerAsync()
    End Sub

    Private Sub dlgImgSelectNew_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        tmrReorderMainList.Stop()
        tmrReorderMainList.Start()
    End Sub

    Private Sub tmrReorderMainList_Tick(sender As Object, e As EventArgs) Handles tmrReorderMainList.Tick
        tmrReorderMainList.Stop()
        ReorderMainList()
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        Me.SetDefaults()
        e.Cancel = Me.DownloadDefaultImages
    End Sub

    Private Sub bwImgDefaults_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDefaults.RunWorkerCompleted
        If Not e.Cancelled Then
            Me.GenerateTopImages()

            Me.lblStatus.Text = Master.eLang.GetString(954, "(Down)Loading New Images...")
            Me.pbStatus.Style = ProgressBarStyle.Continuous
            Me.bwImgDownload.WorkerReportsProgress = True
            Me.bwImgDownload.WorkerSupportsCancellation = True
            Me.bwImgDownload.RunWorkerAsync()
        End If
    End Sub

    Private Sub bwImgDownload_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDownload.DoWork
        e.Cancel = Me.DownloadAllImages()
    End Sub

    Private Sub bwImgDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwImgDownload.ProgressChanged
        If e.UserState.ToString = "progress" Then
            Me.pbStatus.Value = e.ProgressPercentage
        ElseIf e.UserState.ToString = "max" Then
            Me.pbStatus.Value = 0
            Me.pbStatus.Maximum = e.ProgressPercentage
        ElseIf DirectCast(e.UserState, Enums.ModifierType) = Me.currTopImage.ImageType Then
            CreateImageList(Me.currTopImage)
        End If
    End Sub

    Private Sub bwImgDownload_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDownload.RunWorkerCompleted
        Me.lblStatus.Visible = False
        Me.pbStatus.Visible = False
    End Sub

    Public Function SetDefaults() As Boolean
        Images.SetDefaultImages(tDBElementResult, tDefaultImagesContainer, tSearchResultsContainer, tScrapeModifier, tContentType, tDefaultSeasonImagesContainer, tDefaultEpisodeImagesContainer)
        Return False
    End Function

    Private Function DownloadDefaultImages() As Boolean

        'Banner
        If DoMainBanner AndAlso Me.tDBElementResult.ImagesContainer.Banner.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Banner, False)
        End If

        'CharacterArt
        If DoMainCharacterArt AndAlso Me.tDBElementResult.ImagesContainer.CharacterArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.CharacterArt, False)
        End If

        'ClearArt
        If DoMainClearArt AndAlso Me.tDBElementResult.ImagesContainer.ClearArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.ClearArt, False)
        End If

        'ClearLogo
        If DoMainClearLogo AndAlso Me.tDBElementResult.ImagesContainer.ClearLogo.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.ClearLogo, False)
        End If

        'DiscArt
        If DoMainDiscArt AndAlso Me.tDBElementResult.ImagesContainer.DiscArt.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.DiscArt, False)
        End If

        'Fanart
        If DoMainFanart AndAlso Me.tDBElementResult.ImagesContainer.Fanart.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Fanart, False)
        End If

        'Landscape
        If DoMainLandscape AndAlso Me.tDBElementResult.ImagesContainer.Landscape.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Landscape, False)
        End If

        'Poster
        If DoMainPoster AndAlso Me.tDBElementResult.ImagesContainer.Poster.WebImage.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Poster, False)
        End If
    End Function

    Private Function DownloadAllImages() As Boolean
        Dim iProgress As Integer = 1

        Me.bwImgDownload.ReportProgress(tSearchResultsContainer.EpisodeFanarts.Count + tSearchResultsContainer.EpisodePosters.Count + tSearchResultsContainer.SeasonBanners.Count + _
                                       tSearchResultsContainer.SeasonFanarts.Count + tSearchResultsContainer.SeasonLandscapes.Count + tSearchResultsContainer.SeasonPosters.Count + _
                                       tSearchResultsContainer.MainBanners.Count + tSearchResultsContainer.MainCharacterArts.Count + tSearchResultsContainer.MainClearArts.Count + _
                                       tSearchResultsContainer.MainClearLogos.Count + tSearchResultsContainer.MainDiscArts.Count + tSearchResultsContainer.MainFanarts.Count + _
                                       tSearchResultsContainer.MainLandscapes.Count + tSearchResultsContainer.MainPosters.Count, "max")

        'Episode Fanarts
        If DoEpisodeFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)
        End If

        'Episode Posters
        If DoEpisodePoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodePoster)
        End If

        'Main Banners
        If DoMainBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainBanners
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainBanner)
        End If

        'Main CharacterArts
        If DoMainCharacterArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainCharacterArt)
        End If

        'Main ClearArts
        If DoMainClearArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearArt)
        End If

        'Main ClearLogos
        If DoMainClearLogo Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearLogo)
        End If

        'Main Discarts
        If DoMainDiscArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainDiscArt)
        End If

        'Main Fanarts
        If DoMainFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainFanart)
        End If

        'Main Landscapes
        If DoMainLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainLandscape)
        End If

        'Main Posters
        If DoMainPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainPosters
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainPoster)
        End If

        'Season Banners
        If DoSeasonBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonBanner)
        End If

        'Season Fanarts
        If DoSeasonFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)
        End If

        'Season Landscapes
        If DoSeasonLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonLandscape)
        End If

        'Season Posters
        If DoSeasonPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonPoster)
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

    Private Sub GenerateTopImages()
        Dim iCount As Integer = 0
        If DoMainPoster Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.MainPoster)
            iCount += 1
        End If
        If DoMainFanart Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.MainFanart)
            iCount += 1
        End If
        If DoMainBanner Then
            AddTopImage(tDBElementResult.ImagesContainer.Banner, iCount, Enums.ModifierType.MainBanner)
            iCount += 1
        End If
        If DoMainCharacterArt Then
            AddTopImage(tDBElementResult.ImagesContainer.CharacterArt, iCount, Enums.ModifierType.MainCharacterArt)
            iCount += 1
        End If
        If DoMainClearArt Then
            AddTopImage(tDBElementResult.ImagesContainer.ClearArt, iCount, Enums.ModifierType.MainClearArt)
            iCount += 1
        End If
        If DoMainClearLogo Then
            AddTopImage(tDBElementResult.ImagesContainer.ClearLogo, iCount, Enums.ModifierType.MainClearLogo)
            iCount += 1
        End If
        If DoMainDiscArt Then
            AddTopImage(tDBElementResult.ImagesContainer.DiscArt, iCount, Enums.ModifierType.MainDiscArt)
            iCount += 1
        End If
        If DoMainLandscape Then
            AddTopImage(tDBElementResult.ImagesContainer.Landscape, iCount, Enums.ModifierType.MainLandscape)
            iCount += 1
        End If

        If tContentType = Enums.ContentType.Movie Then
            Me.cbSubImageType.Items.Add("Extrafanarts")
            Me.cbSubImageType.Items.Add("Extrathumbs")
            Me.cbSubImageType.SelectedIndex = 0
        End If
    End Sub

    Private Sub AddTopImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType)

        ReDim Preserve Me.pnlTopImage_Panel(iIndex)
        ReDim Preserve Me.pbTopImage_Image(iIndex)
        ReDim Preserve Me.lblTopImage_Resolution(iIndex)
        ReDim Preserve Me.lblTopImage_Title(iIndex)
        Me.pnlTopImage_Panel(iIndex) = New Panel()
        Me.pbTopImage_Image(iIndex) = New PictureBox()
        Me.lblTopImage_Resolution(iIndex) = New Label()
        Me.lblTopImage_Title(iIndex) = New Label()
        Me.pbTopImage_Image(iIndex).Name = iIndex.ToString
        Me.pnlTopImage_Panel(iIndex).Name = iIndex.ToString
        Me.lblTopImage_Resolution(iIndex).Name = iIndex.ToString
        Me.lblTopImage_Title(iIndex).Name = iIndex.ToString
        Me.pnlTopImage_Panel(iIndex).Size = iTopImage_Size_Panel
        Me.pbTopImage_Image(iIndex).Size = iTopImage_Size_Image
        Me.lblTopImage_Resolution(iIndex).Size = iTopImage_Size_Resolution
        Me.lblTopImage_Title(iIndex).Size = iTopImage_Size_Title
        Me.pnlTopImage_Panel(iIndex).BackColor = Color.White
        Me.pnlTopImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        Me.pbTopImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        Me.lblTopImage_Resolution(iIndex).AutoSize = False
        Me.lblTopImage_Resolution(iIndex).BackColor = Color.White
        Me.lblTopImage_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTopImage_Resolution(iIndex).Text = tTag.strResolution
        Me.lblTopImage_Title(iIndex).AutoSize = False
        Me.lblTopImage_Title(iIndex).BackColor = Color.White
        'Me.lblTopImageType(iIndex).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTopImage_Title(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTopImage_Title(iIndex).Text = tTag.strTitle
        Me.pbTopImage_Image(iIndex).Image = If(tImage.WebImage.Image IsNot Nothing, CType(tImage.WebImage.Image.Clone(), Image), _
                                         If(tImage.WebThumb.Image IsNot Nothing, CType(tImage.WebThumb.Image.Clone(), Image), Nothing))
        Me.pnlTopImage_Panel(iIndex).Left = iTopImage_NextLeft
        Me.pbTopImage_Image(iIndex).Location = iTopImage_Location_Image
        Me.lblTopImage_Resolution(iIndex).Location = iTopImage_Location_Resolution
        Me.lblTopImage_Title(iIndex).Location = iTopImage_Location_Title
        Me.pnlTopImage_Panel(iIndex).Top = iTopImage_DistanceTop
        Me.pnlTopImage_Panel(iIndex).Tag = tTag
        Me.pbTopImage_Image(iIndex).Tag = tTag
        Me.lblTopImage_Resolution(iIndex).Tag = tTag
        Me.lblTopImage_Title(iIndex).Tag = tTag
        Me.pnlTopImages.Controls.Add(Me.pnlTopImage_Panel(iIndex))
        Me.pnlTopImage_Panel(iIndex).Controls.Add(Me.pbTopImage_Image(iIndex))
        Me.pnlTopImage_Panel(iIndex).Controls.Add(Me.lblTopImage_Resolution(iIndex))
        Me.pnlTopImage_Panel(iIndex).Controls.Add(Me.lblTopImage_Title(iIndex))
        Me.pnlTopImage_Panel(iIndex).BringToFront()
        AddHandler pbTopImage_Image(iIndex).Click, AddressOf pbTopImage_Click
        AddHandler pbTopImage_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlTopImage_Panel(iIndex).Click, AddressOf pnlTopImage_Click
        AddHandler lblTopImage_Resolution(iIndex).Click, AddressOf lblTopImage_Click
        AddHandler lblTopImage_Title(iIndex).Click, AddressOf lblTopImage_Click

        Me.iTopImage_NextLeft = Me.iTopImage_NextLeft + Me.iTopImage_Size_Panel.Width + Me.iTopImage_DistanceLeft
    End Sub

    Private Sub AddListImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType)

        ReDim Preserve Me.pnlImageList_Panel(iIndex)
        ReDim Preserve Me.pbImageList_Image(iIndex)
        ReDim Preserve Me.lblImageList_DiscType(iIndex)
        ReDim Preserve Me.lblImageList_Language(iIndex)
        ReDim Preserve Me.lblImageList_Resolution(iIndex)
        ReDim Preserve Me.lblImageList_Scraper(iIndex)
        Me.pnlImageList_Panel(iIndex) = New Panel()
        Me.pbImageList_Image(iIndex) = New PictureBox()
        Me.lblImageList_DiscType(iIndex) = New Label()
        Me.lblImageList_Language(iIndex) = New Label()
        Me.lblImageList_Resolution(iIndex) = New Label()
        Me.lblImageList_Scraper(iIndex) = New Label()
        Me.pbImageList_Image(iIndex).Name = iIndex.ToString
        Me.pnlImageList_Panel(iIndex).Name = iIndex.ToString
        Me.lblImageList_DiscType(iIndex).Name = iIndex.ToString
        Me.lblImageList_Language(iIndex).Name = iIndex.ToString
        Me.lblImageList_Resolution(iIndex).Name = iIndex.ToString
        Me.lblImageList_Scraper(iIndex).Name = iIndex.ToString
        Me.pnlImageList_Panel(iIndex).Size = Me.iImageList_Size_Panel
        Me.pbImageList_Image(iIndex).Size = Me.iImageList_Size_Image
        Me.lblImageList_DiscType(iIndex).Size = Me.iImageList_Size_DiscType
        Me.lblImageList_Language(iIndex).Size = Me.iImageList_Size_Language
        Me.lblImageList_Resolution(iIndex).Size = Me.iImageList_Size_Resolution
        Me.lblImageList_Scraper(iIndex).Size = Me.iImageList_Size_Scraper
        Me.pnlImageList_Panel(iIndex).BackColor = Color.White
        Me.pnlImageList_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        Me.pbImageList_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        Me.lblImageList_DiscType(iIndex).AutoSize = False
        Me.lblImageList_DiscType(iIndex).BackColor = Color.White
        Me.lblImageList_DiscType(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblImageList_DiscType(iIndex).Text = tImage.DiscType
        Me.lblImageList_Language(iIndex).AutoSize = False
        Me.lblImageList_Language(iIndex).BackColor = Color.White
        Me.lblImageList_Language(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblImageList_Language(iIndex).Text = tTag.Image.LongLang
        Me.lblImageList_Resolution(iIndex).AutoSize = False
        Me.lblImageList_Resolution(iIndex).BackColor = Color.White
        Me.lblImageList_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblImageList_Resolution(iIndex).Text = tTag.strResolution
        Me.lblImageList_Scraper(iIndex).AutoSize = False
        Me.lblImageList_Scraper(iIndex).BackColor = Color.White
        Me.lblImageList_Scraper(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblImageList_Scraper(iIndex).Text = tTag.Image.Scraper
        Me.pbImageList_Image(iIndex).Image = If(tImage.WebImage.Image IsNot Nothing, CType(tImage.WebImage.Image.Clone(), Image), _
                                          If(tImage.WebThumb.Image IsNot Nothing, CType(tImage.WebThumb.Image.Clone(), Image), Nothing))
        Me.pnlImageList_Panel(iIndex).Left = iImageList_NextLeft
        Me.pnlImageList_Panel(iIndex).Top = iImageList_NextTop
        Me.pbImageList_Image(iIndex).Location = Me.iImageList_Location_Image
        Me.lblImageList_DiscType(iIndex).Location = Me.iImageList_Location_DiscType
        Me.lblImageList_Language(iIndex).Location = Me.iImageList_Location_Language
        Me.lblImageList_Resolution(iIndex).Location = Me.iImageList_Location_Resolution
        Me.lblImageList_Scraper(iIndex).Location = Me.iImageList_Location_Scraper
        Me.pnlImageList_Panel(iIndex).Tag = tTag
        Me.pbImageList_Image(iIndex).Tag = tTag
        Me.lblImageList_DiscType(iIndex).Tag = tTag
        Me.lblImageList_Language(iIndex).Tag = tTag
        Me.lblImageList_Resolution(iIndex).Tag = tTag
        Me.lblImageList_Scraper(iIndex).Tag = tTag
        Me.pnlImageList.Controls.Add(Me.pnlImageList_Panel(iIndex))
        Me.pnlImageList_Panel(iIndex).Controls.Add(Me.pbImageList_Image(iIndex))
        Me.pnlImageList_Panel(iIndex).Controls.Add(Me.lblImageList_DiscType(iIndex))
        Me.pnlImageList_Panel(iIndex).Controls.Add(Me.lblImageList_Language(iIndex))
        Me.pnlImageList_Panel(iIndex).Controls.Add(Me.lblImageList_Resolution(iIndex))
        Me.pnlImageList_Panel(iIndex).Controls.Add(Me.lblImageList_Scraper(iIndex))
        Me.pnlImageList_Panel(iIndex).BringToFront()
        AddHandler pbImageList_Image(iIndex).Click, AddressOf pbMainImage_Click
        AddHandler pbImageList_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlImageList_Panel(iIndex).Click, AddressOf pnlMainImage_Click
        AddHandler lblImageList_DiscType(iIndex).Click, AddressOf lblMainImage_Click
        AddHandler lblImageList_Language(iIndex).Click, AddressOf lblMainImage_Click
        AddHandler lblImageList_Resolution(iIndex).Click, AddressOf lblMainImage_Click
        AddHandler lblImageList_Scraper(iIndex).Click, AddressOf lblMainImage_Click

        If Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft + Me.iImageList_Size_Panel.Width > Me.pnlImageList.Width - 20 Then
            Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
            Me.iImageList_NextTop = Me.iImageList_NextTop + Me.iImageList_Size_Panel.Height + Me.iImageList_DistanceTop
        Else
            Me.iImageList_NextLeft = Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft
        End If
    End Sub

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim vScrollPosition As Integer = pnlImageList.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlImageList.VerticalScroll.Maximum)
        pnlImageList.AutoScrollPosition = New Point(pnlImageList.AutoScrollPosition.X, vScrollPosition)
        pnlImageList.Invalidate()
    End Sub

    Private Sub ReorderMainList()
        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
        Me.iImageList_NextTop = Me.iImageList_DistanceTop

        If Me.pnlImageList.Controls.Count > 0 Then
            Me.pnlImageList.SuspendLayout()
            Me.pnlImageList.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To Me.pnlImageList_Panel.Count - 1
                If Me.pnlImageList_Panel(iIndex) IsNot Nothing Then
                    Me.pnlImageList_Panel(iIndex).Left = iImageList_NextLeft
                    Me.pnlImageList_Panel(iIndex).Top = iImageList_NextTop

                    If Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft + Me.iImageList_Size_Panel.Width > Me.pnlImageList.Width - 20 Then
                        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
                        Me.iImageList_NextTop = Me.iImageList_NextTop + Me.iImageList_Size_Panel.Height + Me.iImageList_DistanceTop
                    Else
                        Me.iImageList_NextLeft = Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft
                    End If
                End If
            Next
            Me.pnlImageList.ResumeLayout()
            Me.pnlImageList.Update()
        End If

    End Sub

    Private Sub pbTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pbMainImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectListImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub Image_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).Image
        DownloadAndCache(tImage, False, True)

        If tImage.WebImage.Image IsNot Nothing Then
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.WebImage.Image)
        End If
    End Sub

    Private Sub pnlTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub pnlMainImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub lblTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub lblMainImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub DoSelectTopImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        For i As Integer = 0 To Me.pnlTopImage_Panel.Count - 1
            Me.pnlTopImage_Panel(i).BackColor = Color.White
            Me.lblTopImage_Resolution(i).BackColor = Color.White
            Me.lblTopImage_Resolution(i).ForeColor = Color.Black
            Me.lblTopImage_Title(i).BackColor = Color.White
            Me.lblTopImage_Title(i).ForeColor = Color.Black
        Next

        Me.pnlTopImage_Panel(iIndex).BackColor = Color.Gray
        Me.lblTopImage_Resolution(iIndex).BackColor = Color.Gray
        Me.lblTopImage_Resolution(iIndex).ForeColor = Color.White
        Me.lblTopImage_Title(iIndex).BackColor = Color.Gray
        Me.lblTopImage_Title(iIndex).ForeColor = Color.White

        If Not Me.currTopImage.ImageType = tTag.ImageType Then
            Me.currTopImage = tTag
            CreateImageList(tTag)
        End If
    End Sub

    Private Sub DoSelectListImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        For i As Integer = 0 To Me.pnlImageList_Panel.Count - 1
            Me.pnlImageList_Panel(i).BackColor = Color.White
            Me.lblImageList_DiscType(i).BackColor = Color.White
            Me.lblImageList_DiscType(i).ForeColor = Color.Black
            Me.lblImageList_Language(i).BackColor = Color.White
            Me.lblImageList_Language(i).ForeColor = Color.Black
            Me.lblImageList_Resolution(i).BackColor = Color.White
            Me.lblImageList_Resolution(i).ForeColor = Color.Black
            Me.lblImageList_Scraper(i).BackColor = Color.White
            Me.lblImageList_Scraper(i).ForeColor = Color.Black
        Next

        Me.pnlImageList_Panel(iIndex).BackColor = Color.Gray
        Me.lblImageList_DiscType(iIndex).BackColor = Color.Gray
        Me.lblImageList_DiscType(iIndex).ForeColor = Color.White
        Me.lblImageList_Language(iIndex).BackColor = Color.Gray
        Me.lblImageList_Language(iIndex).ForeColor = Color.White
        Me.lblImageList_Resolution(iIndex).BackColor = Color.Gray
        Me.lblImageList_Resolution(iIndex).ForeColor = Color.White
        Me.lblImageList_Scraper(iIndex).BackColor = Color.Gray
        Me.lblImageList_Scraper(iIndex).ForeColor = Color.White

        SetImage(tTag)
    End Sub

    Private Sub DeselectAllListImages()
        For i As Integer = 0 To Me.pnlImageList_Panel.Count - 1
            Me.pnlImageList_Panel(i).BackColor = Color.White
            Me.lblImageList_DiscType(i).BackColor = Color.White
            Me.lblImageList_DiscType(i).ForeColor = Color.Black
            Me.lblImageList_Language(i).BackColor = Color.White
            Me.lblImageList_Language(i).ForeColor = Color.Black
            Me.lblImageList_Resolution(i).BackColor = Color.White
            Me.lblImageList_Resolution(i).ForeColor = Color.Black
            Me.lblImageList_Scraper(i).BackColor = Color.White
            Me.lblImageList_Scraper(i).ForeColor = Color.Black
        Next
    End Sub

    Private Sub SetImage(ByVal tTag As iTag)
        If Me.SelSeason = -1 Then
            If tTag.ImageType = Enums.ModifierType.MainBanner Then
                tDBElementResult.ImagesContainer.Banner = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainCharacterArt Then
                tDBElementResult.ImagesContainer.CharacterArt = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainClearArt Then
                tDBElementResult.ImagesContainer.ClearArt = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainClearLogo Then
                tDBElementResult.ImagesContainer.ClearLogo = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainLandscape Then
                tDBElementResult.ImagesContainer.Landscape = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainFanart Then
                tDBElementResult.ImagesContainer.Fanart = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.MainPoster Then
                tDBElementResult.ImagesContainer.Poster = tTag.Image
            End If
            RefreshTopImage(tTag)
        Else
            If tTag.ImageType = Enums.ModifierType.AllSeasonsBanner OrElse tTag.ImageType = Enums.ModifierType.SeasonBanner Then
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsFanart OrElse tTag.ImageType = Enums.ModifierType.SeasonFanart Then
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsLandscape OrElse tTag.ImageType = Enums.ModifierType.SeasonLandscape Then
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape = tTag.Image
            ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsPoster OrElse tTag.ImageType = Enums.ModifierType.SeasonPoster Then
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster = tTag.Image
            End If
        End If
    End Sub

    Private Sub SetParameters()
        Select Case Me.tContentType
            Case Enums.ContentType.Movie
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                Me.DoMainDiscArt = Me.tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.MovieFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.MoviePosterAnyEnabled
            Case Enums.ContentType.MovieSet
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieSetBannerAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieSetClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieSetClearLogoAnyEnabled
                Me.DoMainDiscArt = Me.tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieSetDiscArtAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.MovieSetFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.MovieSetLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.MovieSetPosterAnyEnabled
            Case Enums.ContentType.TV
                Me.DoEpisodeFanart = Me.tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                Me.DoEpisodePoster = Me.tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                Me.DoMainCharacterArt = Me.tScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                Me.DoSeasonBanner = Me.tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                Me.DoSeasonFanart = Me.tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                Me.DoSeasonLandscape = Me.tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                Me.DoSeasonPoster = Me.tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select
    End Sub

    Private Sub RefreshTopImage(ByVal tTag As iTag)
        If Me.pnlTopImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlTopImage_Panel.Count - 1
                If DirectCast(Me.pnlTopImage_Panel(iIndex).Tag, iTag).ImageType = tTag.ImageType Then
                    If Me.pnlTopImage_Panel(iIndex) IsNot Nothing AndAlso Me.pnlTopImage_Panel(iIndex).Contains(Me.pbTopImage_Image(iIndex)) Then
                        'tTag = CreateTag(tTag.Image, tTag.ImageType)
                        Me.lblTopImage_Resolution(iIndex).Text = tTag.strResolution
                        Me.pnlTopImage_Panel(iIndex).Tag = tTag
                        Me.pbTopImage_Image(iIndex).Tag = tTag
                        Me.lblTopImage_Title(iIndex).Tag = tTag
                        Me.lblTopImage_Resolution(iIndex).Tag = tTag
                        Me.pbTopImage_Image(iIndex).Image = If(tTag.Image.WebImage.Image IsNot Nothing, CType(tTag.Image.WebImage.Image.Clone(), Image), _
                                                         If(tTag.Image.WebThumb.Image IsNot Nothing, CType(tTag.Image.WebThumb.Image.Clone(), Image), Nothing))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DownloadFullsizeImage(ByRef iTag As MediaContainers.Image, ByRef tImage As Images)
        If Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso File.Exists(iTag.LocalFile) Then
            tImage.FromFile(iTag.LocalFile)
        ElseIf Not String.IsNullOrEmpty(iTag.LocalFile) AndAlso Not String.IsNullOrEmpty(iTag.URL) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.lblStatus.Visible = True
            Me.pbStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URL)
            If tImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(iTag.LocalFile).FullName)
                tImage.Save(iTag.LocalFile)
            End If

            Me.lblStatus.Visible = False
            Me.pbStatus.Visible = False
        End If
    End Sub

    Private Sub CreateImageList(ByRef tTag As iTag)
        Dim iCount As Integer = 0

        ClearImageList()

        If tTag.ImageType = Enums.ModifierType.MainBanner Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainBanner)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainCharacterArt Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainCharacterArt)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainClearArt Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainClearArt)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainClearLogo Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainClearLogo)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainDiscArt Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainDiscArt)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainFanart Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainLandscape Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainPoster Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                iCount += 1
            Next
        End If
    End Sub

    Private Sub ClearImageList()
        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
        Me.iImageList_NextTop = Me.iImageList_DistanceTop

        If Me.pnlImageList.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlImageList_Panel.Count - 1
                If Me.pnlImageList_Panel(iIndex) IsNot Nothing Then
                    If Me.lblImageList_Resolution(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Resolution(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Resolution(iIndex))
                    If Me.pbImageList_Image(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.pbImageList_Image(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.pbImageList_Image(iIndex))
                    If Me.pnlImageList.Contains(Me.pnlImageList_Panel(iIndex)) Then Me.pnlImageList.Controls.Remove(Me.pnlImageList_Panel(iIndex))
                End If
            Next
        End If
    End Sub

    Private Function CreateImageTag(ByRef tImage As MediaContainers.Image, ByVal ModifierType As Enums.ModifierType) As iTag
        Dim nTag As New iTag

        nTag.Image = tImage
        nTag.ImageType = ModifierType

        'Description
        If tImage IsNot Nothing AndAlso tImage.WebImage IsNot Nothing AndAlso tImage.WebImage.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If String.IsNullOrEmpty(tImage.Width) OrElse String.IsNullOrEmpty(tImage.Height) Then
                nTag.strResolution = String.Format("{0}x{1}", tImage.WebImage.Image.Size.Width, tImage.WebImage.Image.Size.Height)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        ElseIf tImage IsNot Nothing AndAlso tImage.WebThumb IsNot Nothing AndAlso tImage.WebThumb.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                nTag.strResolution = String.Concat("unknown", Environment.NewLine, tImage.LongLang)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        End If

        'Title
        Select Case ModifierType
            Case Enums.ModifierType.MainBanner
                nTag.strTitle = Master.eLang.GetString(838, "Banner")
            Case Enums.ModifierType.MainCharacterArt
                nTag.strTitle = Master.eLang.GetString(1140, "CharacterArt")
            Case Enums.ModifierType.MainClearArt
                nTag.strTitle = Master.eLang.GetString(1096, "ClearArt")
            Case Enums.ModifierType.MainClearLogo
                nTag.strTitle = Master.eLang.GetString(1097, "ClearLogo")
            Case Enums.ModifierType.MainDiscArt
                nTag.strTitle = Master.eLang.GetString(1098, "DiscArt")
            Case Enums.ModifierType.MainFanart
                nTag.strTitle = Master.eLang.GetString(149, "Fanart")
            Case Enums.ModifierType.MainLandscape
                nTag.strTitle = Master.eLang.GetString(1035, "Landscape")
            Case Enums.ModifierType.MainPoster
                nTag.strTitle = Master.eLang.GetString(148, "Poster")
        End Select

        Return nTag
    End Function

    Private Sub btnRemoveTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTopImage.Click
        DeselectAllListImages()
        If Me.SelSeason = -1 Then
            If Me.currTopImage.ImageType = Enums.ModifierType.MainBanner Then
                tDBElementResult.ImagesContainer.Banner = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainBanner)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainCharacterArt Then
                tDBElementResult.ImagesContainer.CharacterArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainCharacterArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainClearArt Then
                tDBElementResult.ImagesContainer.ClearArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainClearArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainClearLogo Then
                tDBElementResult.ImagesContainer.ClearLogo = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainClearLogo)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainDiscArt Then
                tDBElementResult.ImagesContainer.DiscArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainDiscArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainFanart Then
                tDBElementResult.ImagesContainer.Fanart = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainFanart)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainLandscape Then
                tDBElementResult.ImagesContainer.Fanart = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainLandscape)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainPoster Then
                tDBElementResult.ImagesContainer.Poster = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, Enums.ModifierType.MainPoster)
                RefreshTopImage(Me.currTopImage)
            End If
        Else
            If Me.currSubImage.ImageType = Enums.ModifierType.SeasonBanner Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonFanart Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonLandscape Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonPoster Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster = sImg

            End If
        End If
    End Sub

    Private Sub btnRestoreTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreTopImage.Click
        DeselectAllListImages()
        If Me.SelSeason = -1 Then
            If Me.currTopImage.ImageType = Enums.ModifierType.MainBanner Then
                tDBElementResult.ImagesContainer.Banner = tDefaultImagesContainer.Banner
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Banner, Enums.ModifierType.MainBanner)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainCharacterArt Then
                tDBElementResult.ImagesContainer.CharacterArt = tDefaultImagesContainer.CharacterArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.CharacterArt, Enums.ModifierType.MainCharacterArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainClearArt Then
                tDBElementResult.ImagesContainer.ClearArt = tDefaultImagesContainer.ClearArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.ClearArt, Enums.ModifierType.MainClearArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainClearLogo Then
                tDBElementResult.ImagesContainer.ClearLogo = tDefaultImagesContainer.ClearLogo
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.ClearLogo, Enums.ModifierType.MainClearLogo)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainDiscArt Then
                tDBElementResult.ImagesContainer.DiscArt = tDefaultImagesContainer.DiscArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.DiscArt, Enums.ModifierType.MainDiscArt)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainFanart Then
                tDBElementResult.ImagesContainer.Fanart = tDefaultImagesContainer.Fanart
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Fanart, Enums.ModifierType.MainFanart)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainLandscape Then
                tDBElementResult.ImagesContainer.Fanart = tDefaultImagesContainer.Landscape
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Landscape, Enums.ModifierType.MainLandscape)
                RefreshTopImage(Me.currTopImage)
            ElseIf Me.currTopImage.ImageType = Enums.ModifierType.MainPoster Then
                tDBElementResult.ImagesContainer.Poster = tDefaultImagesContainer.Poster
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Poster, Enums.ModifierType.MainPoster)
                RefreshTopImage(Me.currTopImage)
            End If
        Else
            If Me.currSubImage.ImageType = Enums.ModifierType.SeasonBanner Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Banner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Banner = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonFanart Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Fanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Fanart = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonLandscape Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Landscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Landscape = sImg

            ElseIf Me.currSubImage.ImageType = Enums.ModifierType.SeasonPoster Then
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = Me.SelSeason).Poster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = Me.SelSeason).ImagesContainer.Poster = sImg

            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Me.bwImgDefaults.IsBusy Then Me.bwImgDefaults.CancelAsync()
        If Me.bwImgDownload.IsBusy Then Me.bwImgDownload.CancelAsync()

        Me.lblStatus.Text = Master.eLang.GetString(99, "Canceling All Processes...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.lblStatus.Visible = True
        Me.pbStatus.Visible = True

        While Me.bwImgDefaults.IsBusy OrElse Me.bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        DoneAndClose()
    End Sub
    ''' <summary>
    ''' Downloading fullsize images for preview in Edit Episode / Season / Show dialog
    ''' </summary>
    ''' <remarks>All other images will be downloaded while saving to DB</remarks>
    Private Sub DoneAndClose()
        Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.lblStatus.Visible = True
        Me.pbStatus.Visible = True

        If Me.bwImgDefaults.IsBusy Then Me.bwImgDefaults.CancelAsync()
        If Me.bwImgDownload.IsBusy Then Me.bwImgDownload.CancelAsync()

        While Me.bwImgDefaults.IsBusy OrElse Me.bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        'Banner
        If tDBElementResult.ImagesContainer.Banner.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Banner.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.Banner.LocalFile) Then
                tDBElementResult.ImagesContainer.Banner.WebImage.FromFile(tDBElementResult.ImagesContainer.Banner.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Banner.URL) Then
                tDBElementResult.ImagesContainer.Banner.WebImage.FromWeb(tDBElementResult.ImagesContainer.Banner.URL)
            End If
        End If

        'CharacterArt
        If tDBElementResult.ImagesContainer.CharacterArt.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.CharacterArt.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.CharacterArt.LocalFile) Then
                tDBElementResult.ImagesContainer.CharacterArt.WebImage.FromFile(tDBElementResult.ImagesContainer.CharacterArt.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.CharacterArt.URL) Then
                tDBElementResult.ImagesContainer.CharacterArt.WebImage.FromWeb(tDBElementResult.ImagesContainer.CharacterArt.URL)
            End If
        End If

        'ClearArt
        If tDBElementResult.ImagesContainer.ClearArt.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearArt.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.ClearArt.LocalFile) Then
                tDBElementResult.ImagesContainer.ClearArt.WebImage.FromFile(tDBElementResult.ImagesContainer.ClearArt.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearArt.URL) Then
                tDBElementResult.ImagesContainer.ClearArt.WebImage.FromWeb(tDBElementResult.ImagesContainer.ClearArt.URL)
            End If
        End If

        'ClearLogo
        If tDBElementResult.ImagesContainer.ClearLogo.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearLogo.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.ClearLogo.LocalFile) Then
                tDBElementResult.ImagesContainer.ClearLogo.WebImage.FromFile(tDBElementResult.ImagesContainer.ClearLogo.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearLogo.URL) Then
                tDBElementResult.ImagesContainer.ClearLogo.WebImage.FromWeb(tDBElementResult.ImagesContainer.ClearLogo.URL)
            End If
        End If

        'DiscArt
        If tDBElementResult.ImagesContainer.DiscArt.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.DiscArt.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.DiscArt.LocalFile) Then
                tDBElementResult.ImagesContainer.DiscArt.WebImage.FromFile(tDBElementResult.ImagesContainer.DiscArt.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.DiscArt.URL) Then
                tDBElementResult.ImagesContainer.DiscArt.WebImage.FromWeb(tDBElementResult.ImagesContainer.DiscArt.URL)
            End If
        End If

        'Fanart
        If tDBElementResult.ImagesContainer.Fanart.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Fanart.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.Fanart.LocalFile) Then
                tDBElementResult.ImagesContainer.Fanart.WebImage.FromFile(tDBElementResult.ImagesContainer.Fanart.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Fanart.URL) Then
                tDBElementResult.ImagesContainer.Fanart.WebImage.FromWeb(tDBElementResult.ImagesContainer.Fanart.URL)
            End If
        End If

        'Landscape
        If tDBElementResult.ImagesContainer.Landscape.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Landscape.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.Landscape.LocalFile) Then
                tDBElementResult.ImagesContainer.Landscape.WebImage.FromFile(tDBElementResult.ImagesContainer.Landscape.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Landscape.URL) Then
                tDBElementResult.ImagesContainer.Landscape.WebImage.FromWeb(tDBElementResult.ImagesContainer.Landscape.URL)
            End If
        End If

        'Poster
        If tDBElementResult.ImagesContainer.Poster.WebImage.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Poster.LocalFile) AndAlso File.Exists(tDBElementResult.ImagesContainer.Poster.LocalFile) Then
                tDBElementResult.ImagesContainer.Poster.WebImage.FromFile(tDBElementResult.ImagesContainer.Poster.LocalFile)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Poster.URL) Then
                tDBElementResult.ImagesContainer.Poster.WebImage.FromWeb(tDBElementResult.ImagesContainer.Poster.URL)
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure iTag

#Region "Fields"

        Dim Image As MediaContainers.Image
        Dim ImageType As Enums.ModifierType
        Dim iIndex As Integer
        Dim strResolution As String
        Dim strTitle As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class