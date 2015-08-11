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

    Friend WithEvents bwImgDefaults As New System.ComponentModel.BackgroundWorker
    Friend WithEvents bwImgDownload As New System.ComponentModel.BackgroundWorker

    Public Delegate Sub LoadImage(ByVal sDescription As String, ByVal iIndex As Integer, ByVal isChecked As Boolean, poster As MediaContainers.Image, ByVal text As String)
    Public Delegate Sub Delegate_MeActivate()

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
    Private iSubImage_Location_Image As Point = New Point(3, 15)
    Private iSubImage_Location_Resolution As Point = New Point(3, 155)
    Private iSubImage_Location_Title As Point = New Point(3, 0)
    Private iSubImage_Size_Image As Size = New Size(174, 135)
    Private iSubImage_Size_Panel As Size = New Size(180, 175)
    Private iSubImage_Size_Resolution As Size = New Size(174, 15)
    Private iSubImage_Size_Title As Size = New Size(174, 15)

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
    Private DoAllSeasonsBanner As Boolean = False
    Private DoAllSeasonsFanart As Boolean = False
    Private DoAllSeasonsLandscape As Boolean = False
    Private DoAllSeasonsPoster As Boolean = False
    Private DoEpisodeFanart As Boolean = False
    Private DoEpisodePoster As Boolean = False
    Private DoMainBanner As Boolean = False
    Private DoMainCharacterArt As Boolean = False
    Private DoMainClearArt As Boolean = False
    Private DoMainClearLogo As Boolean = False
    Private DoMainDiscArt As Boolean = False
    Private DoMainExtrafanarts As Boolean = False
    Private DoMainExtrathumbs As Boolean = False
    Private DoMainFanart As Boolean = False
    Private DoMainLandscape As Boolean = False
    Private DoMainPoster As Boolean = False
    Private DoSeasonBanner As Boolean = False
    Private DoSeasonFanart As Boolean = False
    Private DoSeasonLandscape As Boolean = False
    Private DoSeasonPoster As Boolean = False

    Private currListImage As New iTag
    Private currSubImage As New iTag
    Private currSubImageSelectedType As Enums.ModifierType
    Private currTopImage As New iTag

    Private tDefaultImagesContainer As New MediaContainers.ImagesContainer
    Private tDefaultEpisodeImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tDefaultSeasonImagesContainer As New List(Of MediaContainers.EpisodeOrSeasonImagesContainer)
    Private tSearchResultsContainer As New MediaContainers.SearchResultsContainer
    Private tDBElementResult As New Database.DBElement

    Private tScrapeModifier As New Structures.ScrapeModifier
    Private tContentType As Enums.ContentType

    Private ComboBoxItems As New Dictionary(Of String, Enums.ModifierType)

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
        Me.pnlSubImages.Width = iSubImage_DistanceLeft + iSubImage_Size_Panel.Width + 20
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
        Me.lblStatus.Text = Master.eLang.GetString(953, "(Down)Loading Default Images...")
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
        ReorderImageList()
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        Me.SetDefaults()
        e.Cancel = Me.DownloadDefaultImages
    End Sub

    Private Sub bwImgDefaults_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwImgDefaults.RunWorkerCompleted
        If Not e.Cancelled Then
            Me.CreateTopImages()

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
        ElseIf DirectCast(e.UserState, Enums.ModifierType) = Me.currSubImage.ImageType Then
            CreateImageList(Me.currSubImage)
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

        'Episode Fanart
        If DoEpisodeFanart Then
            For Each sEpisode As Database.DBElement In Me.tDBElementResult.Episodes.Where(Function(s) s.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                DownloadAndCache(sEpisode.ImagesContainer.Fanart, False)
            Next
        End If

        'Episode Poster
        If DoEpisodePoster Then
            For Each sEpisode As Database.DBElement In Me.tDBElementResult.Episodes.Where(Function(s) s.ImagesContainer.Poster.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                DownloadAndCache(sEpisode.ImagesContainer.Poster, False)
            Next
        End If

        'Main Banner
        If (DoMainBanner OrElse DoSeasonBanner) AndAlso Me.tDBElementResult.ImagesContainer.Banner.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Banner, False)
        End If

        'Main CharacterArt
        If DoMainCharacterArt AndAlso Me.tDBElementResult.ImagesContainer.CharacterArt.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.CharacterArt, False)
        End If

        'Main ClearArt
        If DoMainClearArt AndAlso Me.tDBElementResult.ImagesContainer.ClearArt.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.ClearArt, False)
        End If

        'Main ClearLogo
        If DoMainClearLogo AndAlso Me.tDBElementResult.ImagesContainer.ClearLogo.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.ClearLogo, False)
        End If

        'Main DiscArt
        If DoMainDiscArt AndAlso Me.tDBElementResult.ImagesContainer.DiscArt.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.DiscArt, False)
        End If

        'Main Extrafanarts
        If DoMainExtrafanarts AndAlso Me.tDBElementResult.ImagesContainer.ExtraFanarts.Count > 0 Then
            For Each img In tDBElementResult.ImagesContainer.ExtraFanarts
                DownloadAndCache(img, False)
            Next
        End If

        'Main Extrathumbs
        If DoMainExtrathumbs AndAlso Me.tDBElementResult.ImagesContainer.ExtraThumbs.Count > 0 Then
            For Each img In tDBElementResult.ImagesContainer.ExtraThumbs
                DownloadAndCache(img, False)
            Next
        End If

        'Main Fanart
        If (DoMainFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart) AndAlso Me.tDBElementResult.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Fanart, False)
        End If

        'Main Landscape
        If (DoMainLandscape OrElse DoSeasonLandscape) AndAlso Me.tDBElementResult.ImagesContainer.Landscape.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Landscape, False)
        End If

        'Main Poster
        If (DoMainPoster OrElse DoEpisodePoster OrElse DoSeasonPoster) AndAlso Me.tDBElementResult.ImagesContainer.Poster.ImageOriginal.Image Is Nothing Then
            DownloadAndCache(Me.tDBElementResult.ImagesContainer.Poster, False)
        End If

        'Season Banner
        If DoSeasonBanner Then
            For Each sSeason As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Banner.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                DownloadAndCache(sSeason.ImagesContainer.Banner, False)
            Next
        End If

        'Season Fanart
        If DoSeasonFanart Then
            For Each sSeason As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                DownloadAndCache(sSeason.ImagesContainer.Fanart, False)
            Next
        End If

        'Season Landscape
        If DoSeasonLandscape Then
            For Each sSeason As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Landscape.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                DownloadAndCache(sSeason.ImagesContainer.Landscape, False)
            Next
        End If

        'Season Poster
        If DoSeasonPoster Then
            For Each sSeason As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Poster.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                DownloadAndCache(sSeason.ImagesContainer.Poster, False)
            Next
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
        If DoMainBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainBanners
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
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
        If DoMainFanart OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainEFanarts)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainEThumbs)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainFanart)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)
        End If

        'Main Landscapes
        If DoMainLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainLandscape)
        End If

        'Main Posters
        If DoMainPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainPosters
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainPoster)
        End If

        'Season Banners
        If DoSeasonBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonBanner)
        End If

        'Season Fanarts
        If DoSeasonFanart OrElse DoAllSeasonsFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsFanart)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)
        End If

        'Season Landscapes
        If DoSeasonLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonLandscape)
        End If

        'Season Posters
        If DoSeasonPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters
                DownloadAndCache(tImg, False)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
            Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonPoster)
        End If

        Return False
    End Function

    Private Sub DownloadAndCache(ByRef tImage As MediaContainers.Image, ByVal doCache As Boolean, Optional needFullsize As Boolean = False)
        If tImage.ImageOriginal.Image Is Nothing Then
            If File.Exists(tImage.LocalFilePath) Then
                tImage.ImageOriginal.FromFile(tImage.LocalFilePath)
            ElseIf File.Exists(tImage.CacheThumbPath) AndAlso Not needFullsize Then
                tImage.ImageThumb.FromFile(tImage.CacheThumbPath)
            Else
                If Not String.IsNullOrEmpty(tImage.URLThumb) AndAlso Not needFullsize Then
                    tImage.ImageThumb.FromWeb(tImage.URLThumb)
                    If tImage.ImageThumb.Image IsNot Nothing AndAlso doCache Then
                        Directory.CreateDirectory(Directory.GetParent(tImage.CacheThumbPath).FullName)
                        tImage.ImageThumb.Save(tImage.CacheThumbPath)
                    End If
                ElseIf Not String.IsNullOrEmpty(tImage.URLOriginal) Then
                    tImage.ImageOriginal.FromWeb(tImage.URLOriginal)
                    If tImage.ImageOriginal.Image IsNot Nothing AndAlso doCache Then
                        Directory.CreateDirectory(Directory.GetParent(tImage.LocalFilePath).FullName)
                        tImage.ImageOriginal.Save(tImage.LocalFilePath)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub SetUp()
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

    Private Sub CreateImageList(ByRef tTag As iTag)
        Dim iCount As Integer = 0

        ClearImageList()

        If tTag.ImageType = Enums.ModifierType.AllSeasonsBanner Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = 999)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner, 999)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsFanart Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = 999)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart, 999)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsLandscape Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = 999)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape, 999)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsPoster Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = 999)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster, 999)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.EpisodePoster Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodePoster)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.EpisodeFanart Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainBanner Then
            iCount = 0
            Select Case Me.tContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet, Enums.ContentType.TV, Enums.ContentType.TVShow
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainBanner)
                        iCount += 1
                    Next
                Case Enums.ContentType.TVSeason
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainBanner)
                        iCount += 1
                    Next
            End Select
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
        ElseIf tTag.ImageType = Enums.ModifierType.MainEFanarts Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainEFanarts)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainEThumbs Then
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.MainEThumbs)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.MainFanart Then
            iCount = 0
            Select Me.tContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet, Enums.ContentType.TV, Enums.ContentType.TVShow
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                        iCount += 1
                    Next
                Case Enums.ContentType.TVSeason
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                        iCount += 1
                    Next
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                        iCount += 1
                    Next
            End Select
        ElseIf tTag.ImageType = Enums.ModifierType.MainLandscape Then
            iCount = 0
            Select Case Me.tContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet, Enums.ContentType.TV, Enums.ContentType.TVShow
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                        iCount += 1
                    Next
                Case Enums.ContentType.TVSeason
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                        iCount += 1
                    Next
            End Select
        ElseIf tTag.ImageType = Enums.ModifierType.MainPoster Then
            iCount = 0
            Select Case Me.tContentType
                Case Enums.ContentType.Movie, Enums.ContentType.MovieSet, Enums.ContentType.TV, Enums.ContentType.TVShow
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                        iCount += 1
                    Next
                Case Enums.ContentType.TVEpisode
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                        iCount += 1
                    Next
                Case Enums.ContentType.TVSeason
                    For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters
                        Me.AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                        iCount += 1
                    Next
            End Select
        ElseIf tTag.ImageType = Enums.ModifierType.SeasonBanner Then
            Dim iSeason As Integer = tTag.iSeason
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = iSeason)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonBanner)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.SeasonFanart Then
            Dim iSeason As Integer = tTag.iSeason
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = iSeason)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonFanart)
                iCount += 1
            Next
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonFanart, iSeason)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.SeasonLandscape Then
            Dim iSeason As Integer = tTag.iSeason
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = iSeason)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonLandscape)
                iCount += 1
            Next
        ElseIf tTag.ImageType = Enums.ModifierType.SeasonPoster Then
            Dim iSeason As Integer = tTag.iSeason
            iCount = 0
            For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = iSeason)
                Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonPoster)
                iCount += 1
            Next
        End If
    End Sub

    Private Sub CreateSubImages()
        Dim iCount As Integer = 0

        ClearSubImages()

        If Me.currSubImageSelectedType = Enums.ModifierType.MainEFanarts AndAlso DoMainExtrafanarts Then
            For Each img In tDBElementResult.ImagesContainer.ExtraFanarts
                AddSubImage(img, iCount, Enums.ModifierType.MainEFanarts, -1)
                iCount += 1
            Next
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.MainEThumbs AndAlso DoMainExtrathumbs Then
            For Each img In tDBElementResult.ImagesContainer.ExtraThumbs
                AddSubImage(img, iCount, Enums.ModifierType.MainEThumbs, -1)
                iCount += 1
            Next
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.SeasonBanner AndAlso DoSeasonBanner Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Banner, iCount, Enums.ModifierType.AllSeasonsBanner, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Banner, iCount, Enums.ModifierType.SeasonBanner, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.SeasonFanart AndAlso DoSeasonFanart Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Fanart, iCount, Enums.ModifierType.AllSeasonsFanart, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Fanart, iCount, Enums.ModifierType.SeasonFanart, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.SeasonLandscape AndAlso DoSeasonLandscape Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Landscape, iCount, Enums.ModifierType.AllSeasonsLandscape, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Landscape, iCount, Enums.ModifierType.SeasonLandscape, sSeason.TVSeason.Season)
                iCount += 1
            Next
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.SeasonPoster AndAlso DoSeasonPoster Then
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) f.TVSeason.Season = 999)
                AddSubImage(sSeason.ImagesContainer.Poster, iCount, Enums.ModifierType.AllSeasonsPoster, sSeason.TVSeason.Season)
                iCount += 1
            Next
            For Each sSeason As Database.DBElement In tDBElementResult.Seasons.Where(Function(f) Not f.TVSeason.Season = 999).OrderBy(Function(f) f.TVSeason.Season)
                AddSubImage(sSeason.ImagesContainer.Poster, iCount, Enums.ModifierType.SeasonPoster, sSeason.TVSeason.Season)
                iCount += 1
            Next
        End If
    End Sub

    Private Sub CreateTopImages()
        Dim iCount As Integer = 0

        'While Movie / MovieSet / TV / TVShow scraping
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

        'While TVEpisode scraping
        If DoEpisodePoster AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.EpisodePoster)
            iCount += 1
        End If
        If DoEpisodeFanart AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.EpisodeFanart)
            iCount += 1
        End If

        'While TVSeason scraping
        If DoSeasonPoster AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.SeasonPoster)
            iCount += 1
        End If
        If DoSeasonFanart AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.SeasonFanart)
            iCount += 1
        End If
        If DoSeasonBanner AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Banner, iCount, Enums.ModifierType.SeasonBanner)
            iCount += 1
        End If
        If DoSeasonLandscape AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Landscape, iCount, Enums.ModifierType.SeasonLandscape)
            iCount += 1
        End If

        Select Case tContentType
            Case Enums.ContentType.Movie
                Me.ComboBoxItems.Clear()
                If DoMainExtrafanarts Then Me.ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainEFanarts)
                If DoMainExtrathumbs Then Me.ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainEThumbs)
                Me.cbSubImageType.DataSource = Me.ComboBoxItems.ToList
                Me.cbSubImageType.DisplayMember = "Key"
                Me.cbSubImageType.ValueMember = "Value"
                If Me.cbSubImageType.Items.Count > 0 Then
                    Me.cbSubImageType.SelectedIndex = 0
                    Me.cbSubImageType.Enabled = True
                End If
            Case Is = Enums.ContentType.TV, Enums.ContentType.TVShow
                Me.ComboBoxItems.Clear()
                If DoMainExtrafanarts Then Me.ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainEFanarts)
                If DoMainExtrathumbs Then Me.ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainEThumbs)
                If DoSeasonBanner Then Me.ComboBoxItems.Add(Master.eLang.GetString(1017, "Season Banner"), Enums.ModifierType.SeasonBanner)
                If DoSeasonFanart Then Me.ComboBoxItems.Add(Master.eLang.GetString(686, "Season Fanart"), Enums.ModifierType.SeasonFanart)
                If DoSeasonLandscape Then Me.ComboBoxItems.Add(Master.eLang.GetString(1018, "Season Landscape"), Enums.ModifierType.SeasonLandscape)
                If DoSeasonPoster Then Me.ComboBoxItems.Add(Master.eLang.GetString(685, "Season Poster"), Enums.ModifierType.SeasonPoster)
                Me.cbSubImageType.DataSource = Me.ComboBoxItems.ToList
                Me.cbSubImageType.DisplayMember = "Key"
                Me.cbSubImageType.ValueMember = "Value"
                If Me.cbSubImageType.Items.Count > 0 Then
                    Me.cbSubImageType.SelectedIndex = 0
                    Me.cbSubImageType.Enabled = True
                End If
        End Select
    End Sub

    Private Sub AddListImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

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
        Me.pbImageList_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image), _
                                          If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
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
        AddHandler pbImageList_Image(iIndex).Click, AddressOf pbImageList_Click
        AddHandler pbImageList_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlImageList_Panel(iIndex).Click, AddressOf pnlImageList_Click
        AddHandler lblImageList_DiscType(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Language(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Resolution(iIndex).Click, AddressOf lblImageList_Click
        AddHandler lblImageList_Scraper(iIndex).Click, AddressOf lblImageList_Click

        If Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft + Me.iImageList_Size_Panel.Width > Me.pnlImageList.Width - 20 Then
            Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
            Me.iImageList_NextTop = Me.iImageList_NextTop + Me.iImageList_Size_Panel.Height + Me.iImageList_DistanceTop
        Else
            Me.iImageList_NextLeft = Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft
        End If
    End Sub

    Private Sub AddSubImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, ByVal iSeason As Integer)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

        ReDim Preserve Me.pnlSubImage_Panel(iIndex)
        ReDim Preserve Me.pbSubImage_Image(iIndex)
        ReDim Preserve Me.lblSubImage_Resolution(iIndex)
        ReDim Preserve Me.lblSubImage_Title(iIndex)
        Me.pnlSubImage_Panel(iIndex) = New Panel()
        Me.pbSubImage_Image(iIndex) = New PictureBox()
        Me.lblSubImage_Resolution(iIndex) = New Label()
        Me.lblSubImage_Title(iIndex) = New Label()
        Me.pbSubImage_Image(iIndex).Name = iIndex.ToString
        Me.pnlSubImage_Panel(iIndex).Name = iIndex.ToString
        Me.lblSubImage_Resolution(iIndex).Name = iIndex.ToString
        Me.lblSubImage_Title(iIndex).Name = iIndex.ToString
        Me.pnlSubImage_Panel(iIndex).Size = iSubImage_Size_Panel
        Me.pbSubImage_Image(iIndex).Size = iSubImage_Size_Image
        Me.lblSubImage_Resolution(iIndex).Size = iSubImage_Size_Resolution
        Me.lblSubImage_Title(iIndex).Size = iSubImage_Size_Title
        Me.pnlSubImage_Panel(iIndex).BackColor = Color.White
        Me.pnlSubImage_Panel(iIndex).BorderStyle = BorderStyle.FixedSingle
        Me.pbSubImage_Image(iIndex).SizeMode = PictureBoxSizeMode.Zoom
        Me.lblSubImage_Resolution(iIndex).AutoSize = False
        Me.lblSubImage_Resolution(iIndex).BackColor = Color.White
        Me.lblSubImage_Resolution(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSubImage_Resolution(iIndex).Text = tTag.strResolution
        Me.lblSubImage_Title(iIndex).AutoSize = False
        Me.lblSubImage_Title(iIndex).BackColor = Color.White
        'Me.lblSubImageType(iIndex).Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubImage_Title(iIndex).TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSubImage_Title(iIndex).Text = tTag.strSeason
        Me.pbSubImage_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image), _
                                         If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
        Me.pnlSubImage_Panel(iIndex).Left = iSubImage_DistanceLeft
        Me.pbSubImage_Image(iIndex).Location = iSubImage_Location_Image
        Me.lblSubImage_Resolution(iIndex).Location = iSubImage_Location_Resolution
        Me.lblSubImage_Title(iIndex).Location = iSubImage_Location_Title
        Me.pnlSubImage_Panel(iIndex).Top = iSubImage_NextTop
        Me.pnlSubImage_Panel(iIndex).Tag = tTag
        Me.pbSubImage_Image(iIndex).Tag = tTag
        Me.lblSubImage_Resolution(iIndex).Tag = tTag
        Me.lblSubImage_Title(iIndex).Tag = tTag
        Me.pnlSubImages.Controls.Add(Me.pnlSubImage_Panel(iIndex))
        Me.pnlSubImage_Panel(iIndex).Controls.Add(Me.pbSubImage_Image(iIndex))
        Me.pnlSubImage_Panel(iIndex).Controls.Add(Me.lblSubImage_Resolution(iIndex))
        Me.pnlSubImage_Panel(iIndex).Controls.Add(Me.lblSubImage_Title(iIndex))
        Me.pnlSubImage_Panel(iIndex).BringToFront()
        AddHandler pbSubImage_Image(iIndex).Click, AddressOf pbSubImage_Click
        AddHandler pbSubImage_Image(iIndex).DoubleClick, AddressOf Image_DoubleClick
        AddHandler pnlSubImage_Panel(iIndex).Click, AddressOf pnlSubImage_Click
        AddHandler lblSubImage_Resolution(iIndex).Click, AddressOf lblSubImage_Click
        AddHandler lblSubImage_Title(iIndex).Click, AddressOf lblSubImage_Click

        Me.iSubImage_NextTop = Me.iSubImage_NextTop + Me.iSubImage_Size_Panel.Height + Me.iSubImage_DistanceTop
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
        Me.pbTopImage_Image(iIndex).Image = If(tImage.ImageOriginal.Image IsNot Nothing, CType(tImage.ImageOriginal.Image.Clone(), Image), _
                                         If(tImage.ImageThumb.Image IsNot Nothing, CType(tImage.ImageThumb.Image.Clone(), Image), Nothing))
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

    Private Sub MouseWheelEvent(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim vScrollPosition As Integer = pnlImageList.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlImageList.VerticalScroll.Maximum)
        pnlImageList.AutoScrollPosition = New Point(pnlImageList.AutoScrollPosition.X, vScrollPosition)
        pnlImageList.Invalidate()
    End Sub

    Private Sub ReorderImageList()
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

    Private Sub ReorderSubImages()
        Me.iSubImage_NextTop = Me.iSubImage_DistanceTop

        If Me.pnlSubImages.Controls.Count > 0 Then
            Me.pnlSubImages.SuspendLayout()
            Me.pnlSubImages.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To Me.pnlSubImage_Panel.Count - 1
                If Me.pnlSubImage_Panel(iIndex) IsNot Nothing Then
                    Me.pnlSubImage_Panel(iIndex).Left = iSubImage_DistanceLeft
                    Me.pnlSubImage_Panel(iIndex).Top = iSubImage_NextTop

                    Me.iSubImage_NextTop = Me.iSubImage_NextTop + Me.iSubImage_Size_Panel.Height + Me.iSubImage_DistanceTop
                End If
            Next
            Me.pnlSubImages.ResumeLayout()
            Me.pnlSubImages.Update()
        End If
    End Sub

    Private Sub cbSubImageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSubImageType.SelectedIndexChanged
        If Not Me.currSubImageSelectedType = CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value Then
            Me.currSubImageSelectedType = CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            ClearImageList()
            CreateSubImages()
            If Me.currSubImageSelectedType = Enums.ModifierType.MainEFanarts OrElse Me.currSubImageSelectedType = Enums.ModifierType.MainEThumbs Then
                Me.currSubImage = New iTag With {.ImageType = Me.currSubImageSelectedType, .iSeason = -1}
                DeselectAllTopImages()
                CreateImageList(Me.currSubImage)
            End If
        End If
        Me.pnlImageList.Focus()
    End Sub

    Private Sub pbImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectListImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pbSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectSubImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub pbTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DoSelectTopImage(Convert.ToInt32(DirectCast(sender, PictureBox).Name), DirectCast(DirectCast(sender, PictureBox).Tag, iTag))
    End Sub

    Private Sub Image_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tImage As MediaContainers.Image = DirectCast(DirectCast(sender, PictureBox).Tag, iTag).Image
        DownloadAndCache(tImage, False, True)

        If tImage.ImageOriginal.Image IsNot Nothing Then
            ModulesManager.Instance.RuntimeObjects.InvokeOpenImageViewer(tImage.ImageOriginal.Image)
        End If
    End Sub

    Private Sub pnlImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub pnlSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectSubImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub pnlTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Panel).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Panel).Tag, iTag))
    End Sub

    Private Sub lblImageList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectListImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub lblSubImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectSubImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
    End Sub

    Private Sub lblTopImage_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iIndex As Integer = Convert.ToInt32(DirectCast(sender, Label).Name)
        Me.DoSelectTopImage(iIndex, DirectCast(DirectCast(sender, Label).Tag, iTag))
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

    Private Sub DoSelectSubImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        DeselectAllTopImages()
        For i As Integer = 0 To Me.pnlSubImage_Panel.Count - 1
            Me.pnlSubImage_Panel(i).BackColor = Color.White
            Me.lblSubImage_Resolution(i).BackColor = Color.White
            Me.lblSubImage_Resolution(i).ForeColor = Color.Black
            Me.lblSubImage_Title(i).BackColor = Color.White
            Me.lblSubImage_Title(i).ForeColor = Color.Black
        Next

        Me.pnlSubImage_Panel(iIndex).BackColor = Color.Gray
        Me.lblSubImage_Resolution(iIndex).BackColor = Color.Gray
        Me.lblSubImage_Resolution(iIndex).ForeColor = Color.White
        Me.lblSubImage_Title(iIndex).BackColor = Color.Gray
        Me.lblSubImage_Title(iIndex).ForeColor = Color.White

        Me.btnRemoveSubImage.Enabled = True
        Me.btnRestoreSubImage.Enabled = True

        If Not Me.currSubImage.ImageType = tTag.ImageType OrElse Not Me.currSubImage.iSeason = tTag.iSeason Then
            Me.currSubImage = tTag
            CreateImageList(tTag)
        End If
    End Sub

    Private Sub DoSelectTopImage(ByVal iIndex As Integer, ByVal tTag As iTag)
        DeselectAllSubImages()
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

        Me.btnRemoveTopImage.Enabled = True
        Me.btnRestoreTopImage.Enabled = True

        If Not Me.currTopImage.ImageType = tTag.ImageType Then
            Me.currTopImage = tTag
            CreateImageList(tTag)
        End If
    End Sub

    Private Sub DeselectAllListImages()
        If Me.pnlImageList_Panel IsNot Nothing Then
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
        End If
    End Sub

    Private Sub DeselectAllSubImages()
        Me.btnRemoveSubImage.Enabled = False
        Me.btnRestoreSubImage.Enabled = False
        Me.currSubImage = New iTag
        If Me.pnlSubImage_Panel IsNot Nothing Then
            For i As Integer = 0 To Me.pnlSubImage_Panel.Count - 1
                Me.pnlSubImage_Panel(i).BackColor = Color.White
                Me.lblSubImage_Resolution(i).BackColor = Color.White
                Me.lblSubImage_Resolution(i).ForeColor = Color.Black
                Me.lblSubImage_Title(i).BackColor = Color.White
                Me.lblSubImage_Title(i).ForeColor = Color.Black
            Next
        End If
    End Sub

    Private Sub DeselectAllTopImages()
        Me.btnRemoveTopImage.Enabled = False
        Me.btnRestoreTopImage.Enabled = False
        Me.currTopImage = New iTag
        If Me.pnlTopImage_Panel IsNot Nothing Then
            For i As Integer = 0 To Me.pnlTopImage_Panel.Count - 1
                Me.pnlTopImage_Panel(i).BackColor = Color.White
                Me.lblTopImage_Resolution(i).BackColor = Color.White
                Me.lblTopImage_Resolution(i).ForeColor = Color.Black
                Me.lblTopImage_Title(i).BackColor = Color.White
                Me.lblTopImage_Title(i).ForeColor = Color.Black
            Next
        End If
    End Sub

    Private Sub SetImage(ByVal tTag As iTag)
        If tTag.ImageType = Enums.ModifierType.EpisodeFanart AndAlso tContentType = Enums.ContentType.TVEpisode Then
            tDBElementResult.ImagesContainer.Fanart = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.EpisodePoster AndAlso tContentType = Enums.ContentType.TVEpisode Then
            tDBElementResult.ImagesContainer.Poster = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainBanner Then
            tDBElementResult.ImagesContainer.Banner = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainCharacterArt Then
            tDBElementResult.ImagesContainer.CharacterArt = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainClearArt Then
            tDBElementResult.ImagesContainer.ClearArt = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainClearLogo Then
            tDBElementResult.ImagesContainer.ClearLogo = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainLandscape Then
            tDBElementResult.ImagesContainer.Landscape = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainFanart Then
            tDBElementResult.ImagesContainer.Fanart = tTag.Image
            RefreshTopImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainEFanarts Then
            AddExtraImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.MainEThumbs Then
            AddExtraImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsBanner OrElse tTag.ImageType = Enums.ModifierType.SeasonBanner Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Banner = tTag.Image
            RefreshSubImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsFanart OrElse tTag.ImageType = Enums.ModifierType.SeasonFanart Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Fanart = tTag.Image
            RefreshSubImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsLandscape OrElse tTag.ImageType = Enums.ModifierType.SeasonLandscape Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Landscape = tTag.Image
            RefreshSubImage(tTag)
        ElseIf tTag.ImageType = Enums.ModifierType.AllSeasonsPoster OrElse tTag.ImageType = Enums.ModifierType.SeasonPoster Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Poster = tTag.Image
            RefreshSubImage(tTag)
        End If
    End Sub

    Private Sub SetParameters()
        Select Case Me.tContentType
            Case Enums.ContentType.Movie
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                Me.DoMainDiscArt = Me.tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainEFanarts AndAlso Master.eSettings.MovieEFanartsAnyEnabled
                Me.DoMainExtrathumbs = Me.tScrapeModifier.MainEThumbs AndAlso Master.eSettings.MovieEThumbsAnyEnabled
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
                Me.DoAllSeasonsBanner = Me.tScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                Me.DoAllSeasonsFanart = Me.tScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                Me.DoAllSeasonsLandscape = Me.tScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                Me.DoAllSeasonsPoster = Me.tScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                Me.DoEpisodeFanart = Me.tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                Me.DoEpisodePoster = Me.tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                Me.DoMainCharacterArt = Me.tScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainEFanarts AndAlso Master.eSettings.TVShowEFanartsAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
                Me.DoSeasonBanner = Me.tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                Me.DoSeasonFanart = Me.tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                Me.DoSeasonLandscape = Me.tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                Me.DoSeasonPoster = Me.tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
            Case Enums.ContentType.TVShow
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.TVShowBannerAnyEnabled
                Me.DoMainCharacterArt = Me.tScrapeModifier.MainCharacterArt AndAlso Master.eSettings.TVShowCharacterArtAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.TVShowClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.TVShowClearLogoAnyEnabled
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainEFanarts AndAlso Master.eSettings.TVShowEFanartsAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
            Case Enums.ContentType.TVEpisode
                Me.DoEpisodeFanart = Me.tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                Me.DoEpisodePoster = Me.tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
            Case Enums.ContentType.TVSeason
                Me.DoSeasonBanner = Me.tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                Me.DoSeasonFanart = Me.tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                Me.DoSeasonLandscape = Me.tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                Me.DoSeasonPoster = Me.tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select
    End Sub

    Private Sub AddExtraImage(ByVal tTag As iTag)
        Select Case tTag.ImageType
            Case Enums.ModifierType.MainEFanarts
                If tDBElementResult.ImagesContainer.ExtraFanarts.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tDBElementResult.ImagesContainer.ExtraFanarts.Add(tTag.Image)
                    AddSubImage(tTag.Image, Me.pnlSubImages.Controls.Count, Enums.ModifierType.MainEFanarts, -1)
                    ReorderSubImages()
                End If
            Case Enums.ModifierType.MainEThumbs
                If tDBElementResult.ImagesContainer.ExtraThumbs.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tDBElementResult.ImagesContainer.ExtraThumbs.Add(tTag.Image)
                    AddSubImage(tTag.Image, Me.pnlSubImages.Controls.Count, Enums.ModifierType.MainEThumbs, -1)
                    ReorderSubImages()
                End If
        End Select
    End Sub

    Private Sub RefreshSubImage(ByVal tTag As iTag)
        If Me.pnlSubImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlSubImage_Panel.Count - 1
                If DirectCast(Me.pnlSubImage_Panel(iIndex).Tag, iTag).ImageType = tTag.ImageType AndAlso DirectCast(Me.pnlSubImage_Panel(iIndex).Tag, iTag).iSeason = tTag.iSeason Then
                    If Me.pnlSubImage_Panel(iIndex) IsNot Nothing AndAlso Me.pnlSubImage_Panel(iIndex).Contains(Me.pbSubImage_Image(iIndex)) Then
                        'tTag = CreateTag(tTag.Image, tTag.ImageType)
                        Me.lblSubImage_Resolution(iIndex).Text = tTag.strResolution
                        Me.pnlSubImage_Panel(iIndex).Tag = tTag
                        Me.pbSubImage_Image(iIndex).Tag = tTag
                        Me.lblSubImage_Title(iIndex).Tag = tTag
                        Me.lblSubImage_Resolution(iIndex).Tag = tTag
                        Me.pbSubImage_Image(iIndex).Image = If(tTag.Image.ImageOriginal.Image IsNot Nothing, CType(tTag.Image.ImageOriginal.Image.Clone(), Image), _
                                                         If(tTag.Image.ImageThumb.Image IsNot Nothing, CType(tTag.Image.ImageThumb.Image.Clone(), Image), Nothing))
                    End If
                End If
            Next
        End If
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
                        Me.pbTopImage_Image(iIndex).Image = If(tTag.Image.ImageOriginal.Image IsNot Nothing, CType(tTag.Image.ImageOriginal.Image.Clone(), Image), _
                                                         If(tTag.Image.ImageThumb.Image IsNot Nothing, CType(tTag.Image.ImageThumb.Image.Clone(), Image), Nothing))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DownloadFullsizeImage(ByRef iTag As MediaContainers.Image, ByRef tImage As Images)
        If Not String.IsNullOrEmpty(iTag.LocalFilePath) AndAlso File.Exists(iTag.LocalFilePath) Then
            tImage.FromFile(iTag.LocalFilePath)
        ElseIf Not String.IsNullOrEmpty(iTag.LocalFilePath) AndAlso Not String.IsNullOrEmpty(iTag.URLOriginal) Then
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image...")
            Me.pbStatus.Style = ProgressBarStyle.Marquee
            Me.lblStatus.Visible = True
            Me.pbStatus.Visible = True

            Application.DoEvents()

            tImage.FromWeb(iTag.URLOriginal)
            If tImage.Image IsNot Nothing Then
                Directory.CreateDirectory(Directory.GetParent(iTag.LocalFilePath).FullName)
                tImage.Save(iTag.LocalFilePath)
            End If

            Me.lblStatus.Visible = False
            Me.pbStatus.Visible = False
        End If
    End Sub

    Private Sub ClearImageList()
        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
        Me.iImageList_NextTop = Me.iImageList_DistanceTop

        If Me.pnlImageList.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlImageList_Panel.Count - 1
                If Me.pnlImageList_Panel(iIndex) IsNot Nothing Then
                    If Me.lblImageList_DiscType(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_DiscType(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_DiscType(iIndex))
                    If Me.lblImageList_Language(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Language(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Language(iIndex))
                    If Me.lblImageList_Resolution(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Resolution(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Resolution(iIndex))
                    If Me.lblImageList_Scraper(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Scraper(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Scraper(iIndex))
                    If Me.pbImageList_Image(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.pbImageList_Image(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.pbImageList_Image(iIndex))
                    If Me.pnlImageList.Contains(Me.pnlImageList_Panel(iIndex)) Then Me.pnlImageList.Controls.Remove(Me.pnlImageList_Panel(iIndex))
                End If
            Next
        End If
    End Sub

    Private Sub ClearSubImages()
        Me.currSubImage = New iTag
        Me.btnRemoveSubImage.Enabled = False
        Me.btnRestoreSubImage.Enabled = False
        Me.iSubImage_NextTop = Me.iSubImage_DistanceTop

        If Me.pnlSubImages.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlSubImage_Panel.Count - 1
                If Me.pnlSubImage_Panel(iIndex) IsNot Nothing Then
                    If Me.lblSubImage_Resolution(iIndex) IsNot Nothing AndAlso Me.pnlSubImage_Panel(iIndex).Contains(Me.lblSubImage_Resolution(iIndex)) Then Me.pnlSubImage_Panel(iIndex).Controls.Remove(Me.lblSubImage_Resolution(iIndex))
                    If Me.pbSubImage_Image(iIndex) IsNot Nothing AndAlso Me.pnlSubImage_Panel(iIndex).Contains(Me.pbSubImage_Image(iIndex)) Then Me.pnlSubImage_Panel(iIndex).Controls.Remove(Me.pbSubImage_Image(iIndex))
                    If Me.pnlSubImages.Contains(Me.pnlSubImage_Panel(iIndex)) Then Me.pnlSubImages.Controls.Remove(Me.pnlSubImage_Panel(iIndex))
                End If
            Next
        End If
    End Sub

    Private Function CreateImageTag(ByRef tImage As MediaContainers.Image, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1) As iTag
        Dim nTag As New iTag

        nTag.Image = tImage
        nTag.ImageType = ModifierType

        'Description
        If tImage IsNot Nothing AndAlso tImage.ImageOriginal IsNot Nothing AndAlso tImage.ImageOriginal.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If String.IsNullOrEmpty(tImage.Width) OrElse String.IsNullOrEmpty(tImage.Height) Then
                nTag.strResolution = String.Format("{0}x{1}", tImage.ImageOriginal.Image.Size.Width, tImage.ImageOriginal.Image.Size.Height)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        ElseIf tImage IsNot Nothing AndAlso tImage.ImageThumb IsNot Nothing AndAlso tImage.ImageThumb.Image IsNot Nothing Then
            Dim imgText As String = String.Empty
            If CDbl(tImage.Width) = 0 OrElse CDbl(tImage.Height) = 0 Then
                nTag.strResolution = String.Concat("unknown", Environment.NewLine, tImage.LongLang)
            Else
                nTag.strResolution = String.Format("{0}x{1}", tImage.Width, tImage.Height)
            End If
        End If

        'Season
        If iSeason = 999 Then
            nTag.iSeason = 999
            nTag.strSeason = Master.eLang.GetString(1256, "* All Seasons")
        ElseIf iSeason = 0 Then
            nTag.iSeason = 0
            nTag.strSeason = Master.eLang.GetString(655, "Season Specials")
        ElseIf Not iSeason = -1 Then
            nTag.iSeason = iSeason
            nTag.strSeason = String.Format(Master.eLang.GetString(726, "Season {0}"), iSeason)
        Else
            nTag.iSeason = tImage.Season
            nTag.strSeason = String.Empty
        End If

        'Title
        Select Case ModifierType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.SeasonBanner
                nTag.strTitle = Master.eLang.GetString(838, "Banner")
            Case Enums.ModifierType.MainCharacterArt
                nTag.strTitle = Master.eLang.GetString(1140, "CharacterArt")
            Case Enums.ModifierType.MainClearArt
                nTag.strTitle = Master.eLang.GetString(1096, "ClearArt")
            Case Enums.ModifierType.MainClearLogo
                nTag.strTitle = Master.eLang.GetString(1097, "ClearLogo")
            Case Enums.ModifierType.MainDiscArt
                nTag.strTitle = Master.eLang.GetString(1098, "DiscArt")
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                nTag.strTitle = Master.eLang.GetString(149, "Fanart")
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.SeasonLandscape
                nTag.strTitle = Master.eLang.GetString(1035, "Landscape")
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                nTag.strTitle = Master.eLang.GetString(148, "Poster")
        End Select

        Return nTag
    End Function

    Private Sub btnRemoveSubImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSubImage.Click
        Dim iSeason As Integer = Me.currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = Me.currSubImage.ImageType

        DeselectAllListImages()
        If eImageType = Enums.ModifierType.AllSeasonsBanner OrElse eImageType = Enums.ModifierType.SeasonBanner Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = New MediaContainers.Image
            Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsFanart OrElse eImageType = Enums.ModifierType.SeasonFanart Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = New MediaContainers.Image
            Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsLandscape OrElse eImageType = Enums.ModifierType.SeasonLandscape Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = New MediaContainers.Image
            Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsPoster OrElse eImageType = Enums.ModifierType.SeasonPoster Then
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = New MediaContainers.Image
            Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.MainEFanarts Then
            tDBElementResult.ImagesContainer.ExtraFanarts.Remove(Me.currSubImage.Image)
            CreateSubImages()
        ElseIf eImageType = Enums.ModifierType.MainEThumbs Then
            tDBElementResult.ImagesContainer.ExtraThumbs.Remove(Me.currSubImage.Image)
            CreateSubImages()
        End If
    End Sub

    Private Sub btnRemoveTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTopImage.Click
        Dim eImageType As Enums.ModifierType = Me.currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.ImagesContainer.Banner = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                tDBElementResult.ImagesContainer.CharacterArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainClearArt
                tDBElementResult.ImagesContainer.ClearArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainClearLogo
                tDBElementResult.ImagesContainer.ClearLogo = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainDiscArt
                tDBElementResult.ImagesContainer.DiscArt = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
        End Select
    End Sub

    Private Sub btnRestoreSubImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreSubImage.Click
        Dim iSeason As Integer = Me.currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = Me.currSubImage.ImageType

        DeselectAllListImages()
        If eImageType = Enums.ModifierType.AllSeasonsBanner OrElse eImageType = Enums.ModifierType.SeasonBanner Then
            Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Banner
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = sImg
            Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsFanart OrElse eImageType = Enums.ModifierType.SeasonFanart Then
            Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Fanart
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = sImg
            Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsLandscape OrElse eImageType = Enums.ModifierType.SeasonLandscape Then
            Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Landscape
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = sImg
            Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.AllSeasonsPoster OrElse eImageType = Enums.ModifierType.SeasonPoster Then
            Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Poster
            tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = sImg
            Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
            RefreshSubImage(Me.currSubImage)
        ElseIf eImageType = Enums.ModifierType.MainEFanarts Then
            If MessageBox.Show("Text", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
                tDBElementResult.ImagesContainer.ExtraFanarts = tDefaultImagesContainer.ExtraFanarts
                CreateSubImages()
            End If
        ElseIf eImageType = Enums.ModifierType.MainEThumbs Then
            If MessageBox.Show("Text", "Title", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
                tDBElementResult.ImagesContainer.ExtraThumbs = tDefaultImagesContainer.ExtraThumbs
                CreateSubImages()
            End If
        End If
    End Sub

    Private Sub btnRestoreTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreTopImage.Click
        Dim eImageType As Enums.ModifierType = Me.currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.ImagesContainer.Banner = tDefaultImagesContainer.Banner
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Banner, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainCharacterArt
                tDBElementResult.ImagesContainer.CharacterArt = tDefaultImagesContainer.CharacterArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.CharacterArt, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainClearArt
                tDBElementResult.ImagesContainer.ClearArt = tDefaultImagesContainer.ClearArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.ClearArt, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainClearLogo
                tDBElementResult.ImagesContainer.ClearLogo = tDefaultImagesContainer.ClearLogo
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.ClearLogo, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainDiscArt
                tDBElementResult.ImagesContainer.DiscArt = tDefaultImagesContainer.DiscArt
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.DiscArt, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = tDefaultImagesContainer.Fanart
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Fanart, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = tDefaultImagesContainer.Landscape
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Landscape, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = tDefaultImagesContainer.Poster
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Poster, eImageType)
                RefreshTopImage(Me.currTopImage)
        End Select
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
        Me.btnOK.Enabled = False
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
        If tDBElementResult.ImagesContainer.Banner.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Banner.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.Banner.LocalFilePath) Then
                tDBElementResult.ImagesContainer.Banner.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.Banner.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Banner.URLOriginal) Then
                tDBElementResult.ImagesContainer.Banner.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.Banner.URLOriginal)
            End If
        End If

        'CharacterArt
        If tDBElementResult.ImagesContainer.CharacterArt.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.CharacterArt.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.CharacterArt.LocalFilePath) Then
                tDBElementResult.ImagesContainer.CharacterArt.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.CharacterArt.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.CharacterArt.URLOriginal) Then
                tDBElementResult.ImagesContainer.CharacterArt.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.CharacterArt.URLOriginal)
            End If
        End If

        'ClearArt
        If tDBElementResult.ImagesContainer.ClearArt.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearArt.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.ClearArt.LocalFilePath) Then
                tDBElementResult.ImagesContainer.ClearArt.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.ClearArt.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearArt.URLOriginal) Then
                tDBElementResult.ImagesContainer.ClearArt.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.ClearArt.URLOriginal)
            End If
        End If

        'ClearLogo
        If tDBElementResult.ImagesContainer.ClearLogo.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearLogo.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.ClearLogo.LocalFilePath) Then
                tDBElementResult.ImagesContainer.ClearLogo.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.ClearLogo.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.ClearLogo.URLOriginal) Then
                tDBElementResult.ImagesContainer.ClearLogo.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.ClearLogo.URLOriginal)
            End If
        End If

        'DiscArt
        If tDBElementResult.ImagesContainer.DiscArt.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.DiscArt.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.DiscArt.LocalFilePath) Then
                tDBElementResult.ImagesContainer.DiscArt.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.DiscArt.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.DiscArt.URLOriginal) Then
                tDBElementResult.ImagesContainer.DiscArt.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.DiscArt.URLOriginal)
            End If
        End If

        'Extrafanarts
        For Each img As MediaContainers.Image In tDBElementResult.ImagesContainer.ExtraFanarts
            If img.ImageOriginal.Image Is Nothing Then
                If Not String.IsNullOrEmpty(img.LocalFilePath) AndAlso File.Exists(img.LocalFilePath) Then
                    img.ImageOriginal.FromFile(img.LocalFilePath)
                ElseIf Not String.IsNullOrEmpty(img.URLOriginal) Then
                    img.ImageOriginal.FromWeb(img.URLOriginal)
                End If
            End If
        Next

        'Extrathumbs
        For Each img As MediaContainers.Image In tDBElementResult.ImagesContainer.ExtraThumbs
            If img.ImageOriginal.Image Is Nothing Then
                If Not String.IsNullOrEmpty(img.LocalFilePath) AndAlso File.Exists(img.LocalFilePath) Then
                    img.ImageOriginal.FromFile(img.LocalFilePath)
                ElseIf Not String.IsNullOrEmpty(img.URLOriginal) Then
                    img.ImageOriginal.FromWeb(img.URLOriginal)
                End If
            End If
        Next

        'Fanart
        If tDBElementResult.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Fanart.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.Fanart.LocalFilePath) Then
                tDBElementResult.ImagesContainer.Fanart.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.Fanart.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Fanart.URLOriginal) Then
                tDBElementResult.ImagesContainer.Fanart.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.Fanart.URLOriginal)
            End If
        End If

        'Landscape
        If tDBElementResult.ImagesContainer.Landscape.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Landscape.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.Landscape.LocalFilePath) Then
                tDBElementResult.ImagesContainer.Landscape.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.Landscape.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Landscape.URLOriginal) Then
                tDBElementResult.ImagesContainer.Landscape.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.Landscape.URLOriginal)
            End If
        End If

        'Poster
        If tDBElementResult.ImagesContainer.Poster.ImageOriginal.Image Is Nothing Then
            If Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Poster.LocalFilePath) AndAlso File.Exists(tDBElementResult.ImagesContainer.Poster.LocalFilePath) Then
                tDBElementResult.ImagesContainer.Poster.ImageOriginal.FromFile(tDBElementResult.ImagesContainer.Poster.LocalFilePath)
            ElseIf Not String.IsNullOrEmpty(tDBElementResult.ImagesContainer.Poster.URLOriginal) Then
                tDBElementResult.ImagesContainer.Poster.ImageOriginal.FromWeb(tDBElementResult.ImagesContainer.Poster.URLOriginal)
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
        Dim iSeason As Integer
        Dim strResolution As String
        Dim strSeason As String
        Dim strTitle As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class