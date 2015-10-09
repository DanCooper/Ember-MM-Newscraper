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

    Private LoadedAllSeasonsBanner As Boolean = False
    Private LoadedAllSeasonsFanart As Boolean = False
    Private LoadedAllSeasonsLandscape As Boolean = False
    Private LoadedAllSeasonsPoster As Boolean = False
    Private LoadedEpisodeFanart As Boolean = False
    Private LoadedEpisodePoster As Boolean = False
    Private LoadedMainBanner As Boolean = False
    Private LoadedMainCharacterArt As Boolean = False
    Private LoadedMainClearArt As Boolean = False
    Private LoadedMainClearLogo As Boolean = False
    Private LoadedMainDiscArt As Boolean = False
    Private LoadedMainExtrafanarts As Boolean = False
    Private LoadedMainExtrathumbs As Boolean = False
    Private LoadedMainFanart As Boolean = False
    Private LoadedMainLandscape As Boolean = False
    Private LoadedMainPoster As Boolean = False
    Private LoadedSeasonBanner As Boolean = False
    Private LoadedSeasonFanart As Boolean = False
    Private LoadedSeasonLandscape As Boolean = False
    Private LoadedSeasonPoster As Boolean = False

    Private currListImageSelectedSeason As Integer = -1
    Private currListImageSelectedImageType As Enums.ModifierType
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

    Public Overloads Function ShowDialog(ByVal DBElement As Database.DBElement, ByRef SearchResultsContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifier As Structures.ScrapeModifier, ByVal ContentType As Enums.ContentType) As DialogResult
        Me.tSearchResultsContainer = SearchResultsContainer
        Me.tDBElementResult = CType(DBElement.CloneDeep, Database.DBElement)
        Me.tScrapeModifier = ScrapeModifier
        Me.tContentType = ContentType
        SetParameters()

        Return MyBase.ShowDialog()
    End Function

    Private Sub dlgImageSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler Me.MouseWheel, AddressOf MouseWheelEvent

        Functions.PNLDoubleBuffer(Me.pnlImgSelectMain)

        Me.SetUp()
    End Sub

    Private Sub dlgImageSelect_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Activate()
        Me.lblStatus.Text = Master.eLang.GetString(953, "(Down)Loading Default Images...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.lblStatus.Visible = True
        Me.pbStatus.Visible = True

        Me.bwImgDefaults.WorkerReportsProgress = False
        Me.bwImgDefaults.WorkerSupportsCancellation = True
        Me.bwImgDefaults.RunWorkerAsync()
    End Sub

    Private Sub dlgImgSelectNew_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.pnlLoading.Location = New Point(CInt((Me.pnlImgSelectMain.Left + (Me.pnlImgSelectMain.Width / 2)) - Me.pnlLoading.Width / 2), CInt((Me.pnlImgSelectMain.Top + (Me.pnlImgSelectMain.Height / 2)) - Me.pnlLoading.Height / 2))
        tmrReorderMainList.Stop()
        tmrReorderMainList.Start()
    End Sub

    Private Sub dlgImgSelect_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyValue = Keys.Delete Then
            If Me.btnRemoveSubImage.Enabled Then
                Me.btnRemoveSubImage.PerformClick()
            ElseIf Me.btnRemoveTopImage.Enabled Then
                Me.btnRemoveTopImage.PerformClick()
            End If
        End If
    End Sub

    Private Sub tmrReorderMainList_Tick(sender As Object, e As EventArgs) Handles tmrReorderMainList.Tick
        tmrReorderMainList.Stop()
        ReorderImageList()
    End Sub

    Private Sub bwImgDefaults_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwImgDefaults.DoWork
        Me.SetDefaults()
        Me.DownloadDefaultImages()
        e.Cancel = bwImgDefaults.CancellationPending
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
            For Each tImg As Database.DBElement In Me.tDBElementResult.Episodes.Where(Function(s) s.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                tImg.ImagesContainer.Fanart.LoadAndCache(tContentType)
            Next
        End If

        'Episode Poster
        If DoEpisodePoster Then
            For Each tImg As Database.DBElement In Me.tDBElementResult.Episodes.Where(Function(s) s.ImagesContainer.Poster.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVEpisode.Season).OrderBy(Function(f) f.TVEpisode.Episode)
                tImg.ImagesContainer.Poster.LoadAndCache(tContentType)
            Next
        End If

        'Main Banner
        If (DoMainBanner OrElse DoAllSeasonsBanner OrElse DoSeasonBanner) AndAlso Me.tDBElementResult.ImagesContainer.Banner.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.Banner.LoadAndCache(tContentType)
        End If

        'Main CharacterArt
        If DoMainCharacterArt AndAlso Me.tDBElementResult.ImagesContainer.CharacterArt.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.CharacterArt.LoadAndCache(tContentType)
        End If

        'Main ClearArt
        If DoMainClearArt AndAlso Me.tDBElementResult.ImagesContainer.ClearArt.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.ClearArt.LoadAndCache(tContentType)
        End If

        'Main ClearLogo
        If DoMainClearLogo AndAlso Me.tDBElementResult.ImagesContainer.ClearLogo.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.ClearLogo.LoadAndCache(tContentType)
        End If

        'Main DiscArt
        If DoMainDiscArt AndAlso Me.tDBElementResult.ImagesContainer.DiscArt.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.DiscArt.LoadAndCache(tContentType)
        End If

        'Main Extrafanarts
        If DoMainExtrafanarts AndAlso Me.tDBElementResult.ImagesContainer.Extrafanarts.Count > 0 Then
            For Each tImg In tDBElementResult.ImagesContainer.Extrafanarts
                tImg.LoadAndCache(tContentType)
            Next
        End If

        'Main Extrathumbs
        If DoMainExtrathumbs AndAlso Me.tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 Then
            For Each tImg In tDBElementResult.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                tImg.LoadAndCache(tContentType)
            Next
        End If

        'Main Fanart
        If (DoMainFanart OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart) AndAlso Me.tDBElementResult.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.Fanart.LoadAndCache(tContentType)
        End If

        'Main Landscape
        If (DoMainLandscape OrElse DoAllSeasonsLandscape OrElse DoSeasonLandscape) AndAlso Me.tDBElementResult.ImagesContainer.Landscape.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.Landscape.LoadAndCache(tContentType)
        End If

        'Main Poster
        If (DoMainPoster OrElse DoAllSeasonsPoster OrElse DoEpisodePoster OrElse DoSeasonPoster) AndAlso Me.tDBElementResult.ImagesContainer.Poster.ImageOriginal.Image Is Nothing Then
            Me.tDBElementResult.ImagesContainer.Poster.LoadAndCache(tContentType)
        End If

        'Season Banner
        If DoSeasonBanner Then
            For Each tImg As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Banner.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Banner.LoadAndCache(tContentType)
            Next
        End If

        'Season Fanart
        If DoSeasonFanart Then
            For Each tImg As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Fanart.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Fanart.LoadAndCache(tContentType)
            Next
        End If

        'Season Landscape
        If DoSeasonLandscape Then
            For Each tImg As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Landscape.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Landscape.LoadAndCache(tContentType)
            Next
        End If

        'Season Poster
        If DoSeasonPoster Then
            For Each tImg As Database.DBElement In Me.tDBElementResult.Seasons.Where(Function(s) s.ImagesContainer.Poster.ImageOriginal.Image Is Nothing).OrderBy(Function(s) s.TVSeason.Season)
                tImg.ImagesContainer.Poster.LoadAndCache(tContentType)
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

        'Main Posters
        If DoMainPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainPosters
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainPoster = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainPoster)

        'Main Banners
        If DoMainBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainBanners
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainBanner = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainBanner)

        'Main CharacterArts
        If DoMainCharacterArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainCharacterArt = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainCharacterArt)

        'Main ClearArts
        If DoMainClearArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainClearArt = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearArt)

        'Main ClearLogos
        If DoMainClearLogo Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainClearLogo = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainClearLogo)

        'Main Discarts
        If DoMainDiscArt Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainDiscArt = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainDiscArt)

        'Main Fanarts
        If DoMainFanart OrElse DoMainExtrafanarts OrElse DoMainExtrathumbs OrElse DoAllSeasonsFanart OrElse DoEpisodeFanart OrElse DoSeasonFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainFanart = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsFanart)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainExtrafanarts)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainExtrathumbs)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainFanart)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)

        'Main Landscapes
        If DoMainLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedMainLandscape = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.MainLandscape)

        'Season Banners
        If DoSeasonBanner OrElse DoAllSeasonsBanner Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonBanners
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedSeasonBanner = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsBanner)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonBanner)

        'Season Fanarts
        If DoSeasonFanart OrElse DoAllSeasonsFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedSeasonFanart = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsFanart)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonFanart)

        'Season Landscapes
        If DoSeasonLandscape OrElse DoAllSeasonsLandscape Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedSeasonLandscape = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsLandscape)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonLandscape)

        'Season Posters
        If DoSeasonPoster OrElse DoAllSeasonsPoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.SeasonPosters
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedSeasonPoster = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.AllSeasonsPoster)
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.SeasonPoster)

        'Episode Fanarts
        If DoEpisodeFanart Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedEpisodeFanart = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodeFanart)

        'Episode Posters
        If DoEpisodePoster Then
            For Each tImg As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                tImg.LoadAndCache(tContentType)
                If Me.bwImgDownload.CancellationPending Then
                    Return True
                End If
                Me.bwImgDownload.ReportProgress(iProgress, "progress")
                iProgress += 1
            Next
        End If
        Me.LoadedEpisodePoster = True
        Me.bwImgDownload.ReportProgress(iProgress, Enums.ModifierType.EpisodePoster)

        Return False
    End Function

    Private Sub SetUp()
        Me.btnOK.Text = Master.eLang.GetString(179, "OK")
        Me.btnCancel.Text = Master.eLang.GetString(167, "Cancel")
    End Sub

    Private Sub CreateImageList(ByVal tTag As iTag)
        ClearImageList()

        Me.currListImageSelectedSeason = tTag.iSeason
        Me.currListImageSelectedImageType = tTag.ImageType

        Me.pnlLoading.Visible = True

        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                If LoadedMainBanner AndAlso LoadedSeasonBanner Then FillImageList(tTag)
            Case Enums.ModifierType.AllSeasonsFanart
                If LoadedMainFanart AndAlso LoadedSeasonFanart Then FillImageList(tTag)
            Case Enums.ModifierType.AllSeasonsLandscape
                If LoadedMainLandscape AndAlso LoadedSeasonLandscape Then FillImageList(tTag)
            Case Enums.ModifierType.AllSeasonsPoster
                If LoadedMainPoster AndAlso LoadedSeasonPoster Then FillImageList(tTag)
            Case Enums.ModifierType.EpisodeFanart
                If LoadedMainFanart AndAlso LoadedEpisodeFanart Then FillImageList(tTag)
            Case Enums.ModifierType.EpisodePoster
                If LoadedEpisodePoster Then FillImageList(tTag)
            Case Enums.ModifierType.MainBanner
                If LoadedMainBanner Then FillImageList(tTag)
            Case Enums.ModifierType.MainCharacterArt
                If LoadedMainCharacterArt Then FillImageList(tTag)
            Case Enums.ModifierType.MainClearArt
                If LoadedMainClearArt Then FillImageList(tTag)
            Case Enums.ModifierType.MainClearLogo
                If LoadedMainClearLogo Then FillImageList(tTag)
            Case Enums.ModifierType.MainDiscArt
                If LoadedMainDiscArt Then FillImageList(tTag)
            Case Enums.ModifierType.MainExtrafanarts
                If LoadedMainFanart Then FillImageList(tTag)
            Case Enums.ModifierType.MainExtrathumbs
                If LoadedMainFanart Then FillImageList(tTag)
            Case Enums.ModifierType.MainFanart
                If LoadedMainFanart Then FillImageList(tTag)
            Case Enums.ModifierType.MainLandscape
                If LoadedMainLandscape Then FillImageList(tTag)
            Case Enums.ModifierType.MainPoster
                If LoadedMainPoster Then FillImageList(tTag)
            Case Enums.ModifierType.SeasonBanner
                If LoadedSeasonBanner Then FillImageList(tTag)
            Case Enums.ModifierType.SeasonFanart
                If LoadedMainFanart AndAlso LoadedSeasonFanart Then FillImageList(tTag)
            Case Enums.ModifierType.SeasonLandscape
                If LoadedSeasonLandscape Then FillImageList(tTag)
            Case Enums.ModifierType.SeasonPoster
                If LoadedSeasonPoster Then FillImageList(tTag)
        End Select
    End Sub

    Private Sub FillImageList(ByRef tTag As iTag)
        Dim iCount As Integer = 0

        Me.pnlLoading.Visible = False
        Application.DoEvents()

        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = 999)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsBanner, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonFanarts.Where(Function(f) f.Season = 999)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsFanart, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsLandscape
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = 999)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsLandscape, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.AllSeasonsPoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = 999)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.AllSeasonsPoster, 999)
                    iCount += 1
                Next
            Case Enums.ModifierType.EpisodeFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodeFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                    iCount += 1
                Next
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodeFanart)
                    iCount += 1
                Next
            Case Enums.ModifierType.EpisodePoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.EpisodePosters
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.EpisodePoster)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainBanner
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainBanners
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainBanner)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainCharacterArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainCharacterArts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainCharacterArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainClearArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearArts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainClearArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainClearLogo
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainClearLogos
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainClearLogo)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainDiscArt
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainDiscArts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainDiscArt)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainExtrafanarts
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainExtrafanarts)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainExtrathumbs
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainExtrathumbs)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainFanart
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainFanarts
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainFanart)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainLandscape
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainLandscapes
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainLandscape)
                    iCount += 1
                Next
            Case Enums.ModifierType.MainPoster
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.MainPosters
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.MainPoster)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonBanner
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonBanners.Where(Function(f) f.Season = iSeason)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonBanner)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonFanart
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
            Case Enums.ModifierType.SeasonLandscape
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonLandscapes.Where(Function(f) f.Season = iSeason)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonLandscape)
                    iCount += 1
                Next
            Case Enums.ModifierType.SeasonPoster
                Dim iSeason As Integer = tTag.iSeason
                iCount = 0
                For Each tImage As MediaContainers.Image In tSearchResultsContainer.SeasonPosters.Where(Function(f) f.Season = iSeason)
                    Me.AddListImage(tImage, iCount, Enums.ModifierType.SeasonPoster)
                    iCount += 1
                Next
        End Select
    End Sub

    Private Sub CreateSubImages()
        Dim iCount As Integer = 0

        ClearSubImages()

        If Me.currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts AndAlso DoMainExtrafanarts Then
            For Each img In tDBElementResult.ImagesContainer.Extrafanarts
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrafanarts, -1)
                iCount += 1
            Next
            If Me.tDefaultImagesContainer.Extrafanarts.Count > 0 Then Me.btnRestoreSubImage.Enabled = True
        ElseIf Me.currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs AndAlso DoMainExtrathumbs Then
            tDBElementResult.ImagesContainer.SortExtrathumbs()
            For Each img In tDBElementResult.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
                img.Index = iCount
                AddSubImage(img, iCount, Enums.ModifierType.MainExtrathumbs, -1)
                iCount += 1
            Next
            If Me.tDefaultImagesContainer.Extrathumbs.Count > 0 Then Me.btnRestoreSubImage.Enabled = True
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
        Dim noTopImages As Boolean = True
        Dim iCount As Integer = 0

        'While Movie / MovieSet / TV / TVShow scraping
        If DoMainPoster Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.MainPoster)
            iCount += 1
            noTopImages = False
        End If
        If DoMainFanart Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.MainFanart)
            iCount += 1
            noTopImages = False
        End If
        If DoMainBanner Then
            AddTopImage(tDBElementResult.ImagesContainer.Banner, iCount, Enums.ModifierType.MainBanner)
            iCount += 1
            noTopImages = False
        End If
        If DoMainCharacterArt Then
            AddTopImage(tDBElementResult.ImagesContainer.CharacterArt, iCount, Enums.ModifierType.MainCharacterArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainClearArt Then
            AddTopImage(tDBElementResult.ImagesContainer.ClearArt, iCount, Enums.ModifierType.MainClearArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainClearLogo Then
            AddTopImage(tDBElementResult.ImagesContainer.ClearLogo, iCount, Enums.ModifierType.MainClearLogo)
            iCount += 1
            noTopImages = False
        End If
        If DoMainDiscArt Then
            AddTopImage(tDBElementResult.ImagesContainer.DiscArt, iCount, Enums.ModifierType.MainDiscArt)
            iCount += 1
            noTopImages = False
        End If
        If DoMainLandscape Then
            AddTopImage(tDBElementResult.ImagesContainer.Landscape, iCount, Enums.ModifierType.MainLandscape)
            iCount += 1
            noTopImages = False
        End If

        'While TVEpisode scraping
        If DoEpisodePoster AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.EpisodePoster)
            iCount += 1
            noTopImages = False
        End If
        If DoEpisodeFanart AndAlso tContentType = Enums.ContentType.TVEpisode Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.EpisodeFanart)
            iCount += 1
            noTopImages = False
        End If

        'While TVSeason scraping
        If DoAllSeasonsPoster AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.AllSeasonsPoster, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsFanart AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.AllSeasonsFanart, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsBanner AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Banner, iCount, Enums.ModifierType.AllSeasonsBanner, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoAllSeasonsLandscape AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Landscape, iCount, Enums.ModifierType.AllSeasonsLandscape, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonPoster AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Poster, iCount, Enums.ModifierType.SeasonPoster, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonFanart AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Fanart, iCount, Enums.ModifierType.SeasonFanart, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonBanner AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Banner, iCount, Enums.ModifierType.SeasonBanner, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If
        If DoSeasonLandscape AndAlso tContentType = Enums.ContentType.TVSeason Then
            AddTopImage(tDBElementResult.ImagesContainer.Landscape, iCount, Enums.ModifierType.SeasonLandscape, tDBElementResult.TVSeason.Season)
            iCount += 1
            noTopImages = False
        End If

        Select Case tContentType
            Case Enums.ContentType.Movie
                Me.ComboBoxItems.Clear()
                If DoMainExtrafanarts Then Me.ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainExtrafanarts)
                If DoMainExtrathumbs Then Me.ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainExtrathumbs)
                Me.cbSubImageType.DataSource = Me.ComboBoxItems.ToList
                Me.cbSubImageType.DisplayMember = "Key"
                Me.cbSubImageType.ValueMember = "Value"
                If Me.cbSubImageType.Items.Count > 0 Then
                    Me.cbSubImageType.SelectedIndex = 0
                    Me.cbSubImageType.Enabled = True
                End If
            Case Is = Enums.ContentType.TV, Enums.ContentType.TVShow
                Me.ComboBoxItems.Clear()
                If DoMainExtrafanarts Then Me.ComboBoxItems.Add(Master.eLang.GetString(992, "Extrafanarts"), Enums.ModifierType.MainExtrafanarts)
                If DoMainExtrathumbs Then Me.ComboBoxItems.Add(Master.eLang.GetString(153, "Extrathumbs"), Enums.ModifierType.MainExtrathumbs)
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

        'If we don't have any TopImage we can hide the panel (this should only be True while Extrafanarts or Extrathumbs scraping)
        If Not noTopImages Then
            Me.DoSelectTopImage(0, CType(pnlTopImage_Panel(0).Tag, iTag))
        Else
            Me.pnlImgSelectTop.Visible = False
        End If
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
        Me.pnlImgSelectMain.Controls.Add(Me.pnlImageList_Panel(iIndex))
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

        If Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft + Me.iImageList_Size_Panel.Width > Me.pnlImgSelectMain.Width - 20 Then
            Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
            Me.iImageList_NextTop = Me.iImageList_NextTop + Me.iImageList_Size_Panel.Height + Me.iImageList_DistanceTop
        Else
            Me.iImageList_NextLeft = Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft
        End If
    End Sub

    Private Sub AddSubImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, ByVal iSeason As Integer)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason, iIndex)

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

    Private Sub AddTopImage(ByRef tImage As MediaContainers.Image, ByVal iIndex As Integer, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1)
        Dim tTag As iTag = CreateImageTag(tImage, ModifierType, iSeason)

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
        Dim vScrollPosition As Integer = pnlImgSelectMain.VerticalScroll.Value
        vScrollPosition -= Math.Sign(e.Delta) * 50
        vScrollPosition = Math.Max(0, vScrollPosition)
        vScrollPosition = Math.Min(vScrollPosition, pnlImgSelectMain.VerticalScroll.Maximum)
        pnlImgSelectMain.AutoScrollPosition = New Point(pnlImgSelectMain.AutoScrollPosition.X, vScrollPosition)
        pnlImgSelectMain.Invalidate()
    End Sub

    Private Sub ReorderImageList()
        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
        Me.iImageList_NextTop = Me.iImageList_DistanceTop

        If Me.pnlImgSelectMain.Controls.Count > 0 Then
            Me.pnlImgSelectMain.SuspendLayout()
            Me.pnlImgSelectMain.AutoScrollPosition = New Point With {.X = 0, .Y = 0}
            For iIndex As Integer = 0 To Me.pnlImageList_Panel.Count - 1
                If Me.pnlImageList_Panel(iIndex) IsNot Nothing Then
                    Me.pnlImageList_Panel(iIndex).Left = iImageList_NextLeft
                    Me.pnlImageList_Panel(iIndex).Top = iImageList_NextTop

                    If Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft + Me.iImageList_Size_Panel.Width > Me.pnlImgSelectMain.Width - 20 Then
                        Me.iImageList_NextLeft = Me.iImageList_DistanceLeft
                        Me.iImageList_NextTop = Me.iImageList_NextTop + Me.iImageList_Size_Panel.Height + Me.iImageList_DistanceTop
                    Else
                        Me.iImageList_NextLeft = Me.iImageList_NextLeft + Me.iImageList_Size_Panel.Width + Me.iImageList_DistanceLeft
                    End If
                End If
            Next
            Me.pnlImgSelectMain.ResumeLayout()
            Me.pnlImgSelectMain.Update()
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

    Private Sub cbSubImageType_MouseLeave(sender As Object, e As EventArgs) Handles cbSubImageType.MouseLeave
        Me.pnlImgSelectMain.Focus()
    End Sub

    Private Sub cbSubImageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSubImageType.SelectedIndexChanged
        If Not Me.currSubImageSelectedType = CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value Then
            Me.currSubImageSelectedType = CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            ClearImageList()
            CreateSubImages()
            If Me.currSubImageSelectedType = Enums.ModifierType.MainExtrafanarts OrElse Me.currSubImageSelectedType = Enums.ModifierType.MainExtrathumbs Then
                Me.currSubImage = New iTag With {.ImageType = Me.currSubImageSelectedType, .iSeason = -1}
                DeselectAllTopImages()
                CreateImageList(Me.currSubImage)
            End If
        End If
        Me.pnlImgSelectMain.Focus()
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
        tImage.LoadAndCache(tContentType, True)

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

        If tTag.ImageType = Enums.ModifierType.MainExtrathumbs Then
            If tTag.iIndex > 0 Then
                Me.btnSubImageUp.Enabled = True
            Else
                Me.btnSubImageUp.Enabled = False
            End If
            If tTag.iIndex < tDBElementResult.ImagesContainer.Extrathumbs.Count - 1 Then
                Me.btnSubImageDown.Enabled = True
            Else
                Me.btnSubImageDown.Enabled = False
            End If
        Else
            Me.btnSubImageDown.Enabled = False
            Me.btnSubImageUp.Enabled = False
        End If

        Me.currSubImage = tTag
        Me.currSubImageSelectedType = tTag.ImageType
        If Not Me.currListImageSelectedImageType = tTag.ImageType OrElse Not Me.currListImageSelectedSeason = tTag.iSeason Then
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

        Me.currTopImage = tTag
        If Not Me.currListImageSelectedImageType = tTag.ImageType Then
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
        Me.btnSubImageDown.Enabled = False
        Me.btnSubImageUp.Enabled = False
        If Not CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value = Enums.ModifierType.MainExtrafanarts OrElse _
            Not CType(Me.cbSubImageType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value = Enums.ModifierType.MainExtrathumbs Then
            Me.btnRestoreSubImage.Enabled = False
        End If
        Me.currSubImage = New iTag
        Me.currSubImageSelectedType = Enums.ModifierType.All
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
        Select Case tTag.ImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                If tContentType = Enums.ContentType.TV Then
                    tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Banner = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    tDBElementResult.ImagesContainer.Banner = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                If tContentType = Enums.ContentType.TV Then
                    tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Fanart = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    tDBElementResult.ImagesContainer.Fanart = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                If tContentType = Enums.ContentType.TV Then
                    tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Landscape = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    tDBElementResult.ImagesContainer.Landscape = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                If tContentType = Enums.ContentType.TV Then
                    tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = tTag.iSeason).ImagesContainer.Poster = tTag.Image
                    RefreshSubImage(tTag)
                ElseIf tContentType = Enums.ContentType.TVSeason Then
                    tDBElementResult.ImagesContainer.Poster = tTag.Image
                    RefreshTopImage(tTag)
                End If
            Case Enums.ModifierType.EpisodeFanart
                tDBElementResult.ImagesContainer.Fanart = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.EpisodePoster
                tDBElementResult.ImagesContainer.Poster = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainBanner
                tDBElementResult.ImagesContainer.Banner = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainCharacterArt
                tDBElementResult.ImagesContainer.CharacterArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainClearArt
                tDBElementResult.ImagesContainer.ClearArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainClearLogo
                tDBElementResult.ImagesContainer.ClearLogo = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainDiscArt
                tDBElementResult.ImagesContainer.DiscArt = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainExtrafanarts
                AddExtraImage(tTag)
            Case Enums.ModifierType.MainExtrathumbs
                AddExtraImage(tTag)
            Case Enums.ModifierType.MainFanart
                tDBElementResult.ImagesContainer.Fanart = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainLandscape
                tDBElementResult.ImagesContainer.Landscape = tTag.Image
                RefreshTopImage(tTag)
            Case Enums.ModifierType.MainPoster
                tDBElementResult.ImagesContainer.Poster = tTag.Image
                RefreshTopImage(tTag)
        End Select
    End Sub

    Private Sub SetParameters()
        Select Case Me.tContentType
            Case Enums.ContentType.Movie
                Me.DoMainBanner = Me.tScrapeModifier.MainBanner AndAlso Master.eSettings.MovieBannerAnyEnabled
                Me.DoMainClearArt = Me.tScrapeModifier.MainClearArt AndAlso Master.eSettings.MovieClearArtAnyEnabled
                Me.DoMainClearLogo = Me.tScrapeModifier.MainClearLogo AndAlso Master.eSettings.MovieClearLogoAnyEnabled
                Me.DoMainDiscArt = Me.tScrapeModifier.MainDiscArt AndAlso Master.eSettings.MovieDiscArtAnyEnabled
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.MovieExtrafanartsAnyEnabled
                Me.DoMainExtrathumbs = Me.tScrapeModifier.MainExtrathumbs AndAlso Master.eSettings.MovieExtrathumbsAnyEnabled
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
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
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
                Me.DoMainExtrafanarts = Me.tScrapeModifier.MainExtrafanarts AndAlso Master.eSettings.TVShowExtrafanartsAnyEnabled
                Me.DoMainFanart = Me.tScrapeModifier.MainFanart AndAlso Master.eSettings.TVShowFanartAnyEnabled
                Me.DoMainLandscape = Me.tScrapeModifier.MainLandscape AndAlso Master.eSettings.TVShowLandscapeAnyEnabled
                Me.DoMainPoster = Me.tScrapeModifier.MainPoster AndAlso Master.eSettings.TVShowPosterAnyEnabled
            Case Enums.ContentType.TVEpisode
                Me.DoEpisodeFanart = Me.tScrapeModifier.EpisodeFanart AndAlso Master.eSettings.TVEpisodeFanartAnyEnabled
                Me.DoEpisodePoster = Me.tScrapeModifier.EpisodePoster AndAlso Master.eSettings.TVEpisodePosterAnyEnabled
            Case Enums.ContentType.TVSeason
                Me.DoAllSeasonsBanner = Me.tScrapeModifier.AllSeasonsBanner AndAlso Master.eSettings.TVAllSeasonsBannerAnyEnabled
                Me.DoAllSeasonsFanart = Me.tScrapeModifier.AllSeasonsFanart AndAlso Master.eSettings.TVAllSeasonsFanartAnyEnabled
                Me.DoAllSeasonsLandscape = Me.tScrapeModifier.AllSeasonsLandscape AndAlso Master.eSettings.TVAllSeasonsLandscapeAnyEnabled
                Me.DoAllSeasonsPoster = Me.tScrapeModifier.AllSeasonsPoster AndAlso Master.eSettings.TVAllSeasonsPosterAnyEnabled
                Me.DoSeasonBanner = Me.tScrapeModifier.SeasonBanner AndAlso Master.eSettings.TVSeasonBannerAnyEnabled
                Me.DoSeasonFanart = Me.tScrapeModifier.SeasonFanart AndAlso Master.eSettings.TVSeasonFanartAnyEnabled
                Me.DoSeasonLandscape = Me.tScrapeModifier.SeasonLandscape AndAlso Master.eSettings.TVSeasonLandscapeAnyEnabled
                Me.DoSeasonPoster = Me.tScrapeModifier.SeasonPoster AndAlso Master.eSettings.TVSeasonPosterAnyEnabled
        End Select
    End Sub

    Private Sub AddExtraImage(ByVal tTag As iTag)
        Select Case tTag.ImageType
            Case Enums.ModifierType.MainExtrafanarts
                If tDBElementResult.ImagesContainer.Extrafanarts.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tDBElementResult.ImagesContainer.Extrafanarts.Add(tTag.Image)
                    AddSubImage(tTag.Image, Me.pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrafanarts, -1)
                    ReorderSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If tDBElementResult.ImagesContainer.Extrathumbs.Where(Function(f) f.URLOriginal = tTag.Image.URLOriginal).Count = 0 Then
                    tTag.Image.Index = tDBElementResult.ImagesContainer.Extrathumbs.Count
                    tDBElementResult.ImagesContainer.Extrathumbs.Add(tTag.Image)
                    AddSubImage(tTag.Image, Me.pnlSubImages.Controls.Count, Enums.ModifierType.MainExtrathumbs, -1)
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
            Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image(s)...")
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

        If Me.pnlImgSelectMain.Controls.Count > 0 Then
            For iIndex As Integer = 0 To Me.pnlImageList_Panel.Count - 1
                If Me.pnlImageList_Panel(iIndex) IsNot Nothing Then
                    If Me.lblImageList_DiscType(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_DiscType(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_DiscType(iIndex))
                    If Me.lblImageList_Language(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Language(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Language(iIndex))
                    If Me.lblImageList_Resolution(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Resolution(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Resolution(iIndex))
                    If Me.lblImageList_Scraper(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.lblImageList_Scraper(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.lblImageList_Scraper(iIndex))
                    If Me.pbImageList_Image(iIndex) IsNot Nothing AndAlso Me.pnlImageList_Panel(iIndex).Contains(Me.pbImageList_Image(iIndex)) Then Me.pnlImageList_Panel(iIndex).Controls.Remove(Me.pbImageList_Image(iIndex))
                    If Me.pnlImgSelectMain.Contains(Me.pnlImageList_Panel(iIndex)) Then Me.pnlImgSelectMain.Controls.Remove(Me.pnlImageList_Panel(iIndex))
                End If
            Next
        End If
    End Sub

    Private Sub ClearSubImages()
        Me.currSubImage = New iTag
        Me.btnRemoveSubImage.Enabled = False
        Me.btnRestoreSubImage.Enabled = False
        Me.btnSubImageDown.Enabled = False
        Me.btnSubImageUp.Enabled = False
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

    Private Function CreateImageTag(ByRef tImage As MediaContainers.Image, ByVal ModifierType As Enums.ModifierType, Optional ByVal iSeason As Integer = -1, Optional ByVal iIndex As Integer = -1) As iTag
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

        'Index (only needed for Extrathumbs)
        If ModifierType = Enums.ModifierType.MainExtrathumbs Then
            nTag.iIndex = iIndex
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
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                nTag.strTitle = Master.eLang.GetString(838, "Banner")
            Case Enums.ModifierType.MainCharacterArt
                nTag.strTitle = Master.eLang.GetString(1140, "CharacterArt")
            Case Enums.ModifierType.MainClearArt
                nTag.strTitle = Master.eLang.GetString(1096, "ClearArt")
            Case Enums.ModifierType.MainClearLogo
                nTag.strTitle = Master.eLang.GetString(1097, "ClearLogo")
            Case Enums.ModifierType.MainDiscArt
                nTag.strTitle = Master.eLang.GetString(1098, "DiscArt")
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                nTag.strTitle = Master.eLang.GetString(149, "Fanart")
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                nTag.strTitle = Master.eLang.GetString(1035, "Landscape")
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                nTag.strTitle = Master.eLang.GetString(148, "Poster")
        End Select

        Return nTag
    End Function

    Private Sub btnRemoveSubImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSubImage.Click
        Dim iSeason As Integer = Me.currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = Me.currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = New MediaContainers.Image
                Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = New MediaContainers.Image
                Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = New MediaContainers.Image
                Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = New MediaContainers.Image
                Me.currSubImage = CreateImageTag(New MediaContainers.Image, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                tDBElementResult.ImagesContainer.Extrafanarts.Remove(Me.currSubImage.Image)
                CreateSubImages()
            Case Enums.ModifierType.MainExtrathumbs
                tDBElementResult.ImagesContainer.Extrathumbs.Remove(Me.currSubImage.Image)
                CreateSubImages()
        End Select
    End Sub

    Private Sub btnRemoveTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTopImage.Click
        Dim eImageType As Enums.ModifierType = Me.currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
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
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = New MediaContainers.Image
                Me.currTopImage = CreateImageTag(New MediaContainers.Image, eImageType)
                RefreshTopImage(Me.currTopImage)
        End Select
    End Sub

    Private Sub btnRestoreSubImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreSubImage.Click
        Dim iSeason As Integer = Me.currSubImage.iSeason
        Dim eImageType As Enums.ModifierType = Me.currSubImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Banner
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Banner = sImg
                Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.SeasonFanart
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Fanart
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Fanart = sImg
                Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Landscape
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Landscape = sImg
                Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.SeasonPoster
                Dim sImg As MediaContainers.Image = tDefaultSeasonImagesContainer.FirstOrDefault(Function(s) s.Season = iSeason).Poster
                tDBElementResult.Seasons.FirstOrDefault(Function(s) s.TVSeason.Season = iSeason).ImagesContainer.Poster = sImg
                Me.currSubImage = CreateImageTag(sImg, eImageType, iSeason)
                RefreshSubImage(Me.currSubImage)
            Case Enums.ModifierType.MainExtrafanarts
                If MessageBox.Show(Master.eLang.GetString(265, "Are you sure you want to reset to the default list of Extrafanarts?"), Master.eLang.GetString(253, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
                    tDBElementResult.ImagesContainer.Extrafanarts = tDefaultImagesContainer.Extrafanarts
                    CreateSubImages()
                End If
            Case Enums.ModifierType.MainExtrathumbs
                If MessageBox.Show(Master.eLang.GetString(266, "Are you sure you want to reset to the default list of Extrathumbs?"), Master.eLang.GetString(253, "Reload default list"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
                    tDBElementResult.ImagesContainer.Extrathumbs = tDefaultImagesContainer.Extrathumbs
                    CreateSubImages()
                End If
        End Select
    End Sub

    Private Sub btnRestoreTopImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreTopImage.Click
        Dim eImageType As Enums.ModifierType = Me.currTopImage.ImageType

        DeselectAllListImages()

        Select Case eImageType
            Case Enums.ModifierType.MainBanner, Enums.ModifierType.AllSeasonsBanner, Enums.ModifierType.SeasonBanner
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
            Case Enums.ModifierType.MainFanart, Enums.ModifierType.AllSeasonsFanart, Enums.ModifierType.EpisodeFanart, Enums.ModifierType.SeasonFanart
                tDBElementResult.ImagesContainer.Fanart = tDefaultImagesContainer.Fanart
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Fanart, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainLandscape, Enums.ModifierType.AllSeasonsLandscape, Enums.ModifierType.SeasonLandscape
                tDBElementResult.ImagesContainer.Landscape = tDefaultImagesContainer.Landscape
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Landscape, eImageType)
                RefreshTopImage(Me.currTopImage)
            Case Enums.ModifierType.MainPoster, Enums.ModifierType.AllSeasonsPoster, Enums.ModifierType.EpisodePoster, Enums.ModifierType.SeasonPoster
                tDBElementResult.ImagesContainer.Poster = tDefaultImagesContainer.Poster
                Me.currTopImage = CreateImageTag(tDefaultImagesContainer.Poster, eImageType)
                RefreshTopImage(Me.currTopImage)
        End Select
    End Sub

    Private Sub btnSubImageDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubImageDown.Click
        If tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 AndAlso Me.currSubImage.iIndex < (tDBElementResult.ImagesContainer.Extrathumbs.Count - 1) Then
            Dim iIndex As Integer = Me.currSubImage.iIndex
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index + 1
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex + 1).Index - 1
            CreateSubImages()
            Me.DoSelectSubImage(iIndex + 1, CType(pnlSubImage_Panel(iIndex + 1).Tag, iTag))
        End If
    End Sub

    Private Sub btnSubImageUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubImageUp.Click
        If tDBElementResult.ImagesContainer.Extrathumbs.Count > 0 AndAlso Me.currSubImage.iIndex > 0 Then
            Dim iIndex As Integer = Me.currSubImage.iIndex
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex).Index - 1
            tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index = tDBElementResult.ImagesContainer.Extrathumbs.Item(iIndex - 1).Index + 1
            CreateSubImages()
            Me.DoSelectSubImage(iIndex - 1, CType(pnlSubImage_Panel(iIndex - 1).Tag, iTag))
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
        Me.btnOK.Enabled = False
        Me.DeselectAllListImages()
        Me.DeselectAllSubImages()
        Me.DeselectAllTopImages()
        Me.pnlImgSelectLeft.Enabled = False
        Me.pnlImgSelectMain.Enabled = False
        Me.pnlImgSelectTop.Enabled = False
        Me.pnlLoading.Visible = False

        If Me.bwImgDefaults.IsBusy Then Me.bwImgDefaults.CancelAsync()
        If Me.bwImgDownload.IsBusy Then Me.bwImgDownload.CancelAsync()

        While Me.bwImgDefaults.IsBusy OrElse Me.bwImgDownload.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While

        Me.lblStatus.Text = Master.eLang.GetString(952, "Downloading Fullsize Image(s)...")
        Me.pbStatus.Style = ProgressBarStyle.Marquee
        Me.lblStatus.Visible = True
        Me.pbStatus.Visible = True

        'Banner
        tDBElementResult.ImagesContainer.Banner.LoadAndCache(tContentType, True)

        'CharacterArt
        tDBElementResult.ImagesContainer.CharacterArt.LoadAndCache(tContentType, True)

        'ClearArt
        tDBElementResult.ImagesContainer.ClearArt.LoadAndCache(tContentType, True)

        'ClearLogo
        tDBElementResult.ImagesContainer.ClearLogo.LoadAndCache(tContentType, True)

        'DiscArt
        tDBElementResult.ImagesContainer.DiscArt.LoadAndCache(tContentType, True)

        'Extrafanarts
        For Each img As MediaContainers.Image In tDBElementResult.ImagesContainer.Extrafanarts
            img.LoadAndCache(tContentType, True)
        Next

        'Extrathumbs
        For Each img As MediaContainers.Image In tDBElementResult.ImagesContainer.Extrathumbs.OrderBy(Function(f) f.Index)
            img.LoadAndCache(tContentType, True)
        Next

        'Fanart
        tDBElementResult.ImagesContainer.Fanart.LoadAndCache(tContentType, True)

        'Landscape
        tDBElementResult.ImagesContainer.Landscape.LoadAndCache(tContentType, True)

        'Poster
        tDBElementResult.ImagesContainer.Poster.LoadAndCache(tContentType, True)

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